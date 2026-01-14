using Serilog;
using System.Diagnostics;
using System.Reflection;

using Tobii.Research;

namespace tobii_interface
{
    public partial class MainForm : Form
    {
        private Settings _settings;
        private string _logPath = "";
        private IEyeTracker? _eyeTracker = null;

        public MainForm()
        {
            InitializeComponent();
            _settings = Settings.Restore();

            if (!_settings.LastPosition.IsEmpty)
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point(_settings.LastPosition.X, _settings.LastPosition.Y);
                Width = _settings.LastPosition.Width;
                Height = _settings.LastPosition.Height;
            }
        }

        private async Task StartLogging()
        {
            _logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "EPL",
                "Logs",
                $"TobiiInterface-{DateTime.Now:yyyyMMdd}.txt");

            await Task.Run(() =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File(path: Path.Combine(_logPath),
                              retainedFileCountLimit: 30,
                              flushToDiskInterval: TimeSpan.FromSeconds(5),
                              buffered: true)
                .CreateLogger()
                );

            var listener = new SerilogTraceListener.SerilogTraceListener();
            Trace.Listeners.Add(listener);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            await StartLogging();

            Log.Information($"Tobii Interface v{Assembly.GetExecutingAssembly().GetName().Version} started");

            trackerStatusLabel.Image = imageList.Images[0];
            trackerStatusLabel.Text = "No tracker found";

            connectionTimer.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.LastPosition = new Rectangle(Location.X, Location.Y, Width, Height);
            _settings.Save();

            Log.Information("Exit");
            Log.CloseAndFlush();
        }

        private void connectionTimer_Tick(object sender, EventArgs e)
        {
            connectionTimer.Interval = 1000;
            var list = EyeTrackingOperations.FindAllEyeTrackers();

            if (list.Count > 0)
            {
                _eyeTracker = EyeTrackingOperations.GetEyeTracker(list[0].Address);
                //_eyeTracker.GazeDataReceived += GazeDataReceived;

                trackerStatusLabel.Image = imageList.Images[1];
                trackerStatusLabel.Text = _eyeTracker.DeviceName;

                connectionTimer.Stop();
            }
        }

        private void GazeDataReceived(object? sender, GazeDataEventArgs args)
        {
            pupilChart.AddValue((decimal)args.LeftEye.GazePoint.PositionOnDisplayArea.Y);
            //var pt = args.LeftEye.GazePoint;
            //Invoke(new Action(() => textBox1.Text = $"{pt.PositionOnDisplayArea.X}, {pt.PositionOnDisplayArea.X}"));
        }

        private void Calibrate()
        {
            if (_eyeTracker == null)
            {
                return;
            }

            Log.Information("launching calibration");
            string? localAppData = Environment.GetEnvironmentVariable("LocalAppData");
            if (string.IsNullOrEmpty(localAppData))
            {
                throw new InvalidOperationException("The environment variable 'LocalAppData' is not set.");
            }

            string etmBasePath = Path.GetFullPath(Path.Combine(localAppData, "Programs", "TobiiProEyeTrackerManager"));
            string etmStartupMode = "usercalibration";
            string executablePath = Path.GetFullPath(Path.Combine(etmBasePath,
                                                                    "TobiiProEyeTrackerManager.exe"));

            string arguments = $"--device-sn={_eyeTracker.SerialNumber} --mode={etmStartupMode} --screen=1";

            try
            {
                Process etmProcess = new Process();
                // Redirect the output stream of the child process.
                etmProcess.StartInfo.UseShellExecute = false;
                etmProcess.StartInfo.RedirectStandardError = true;
                etmProcess.StartInfo.RedirectStandardOutput = true;
                etmProcess.StartInfo.FileName = executablePath;
                etmProcess.StartInfo.Arguments = arguments;
                etmProcess.Start();

                string stdOutput = etmProcess.StandardOutput.ReadToEnd();

                etmProcess.WaitForExit();
                int exitCode = etmProcess.ExitCode;

                if (exitCode == 0)
                {
                    Debug.WriteLine("Eye Tracker Manager was called successfully!");
                }
                else
                {
                    Log.Error("Eye Tracker Manager call returned the error code: {0}", exitCode);

                    foreach (string line in stdOutput.Split(Environment.NewLine.ToCharArray()))
                    {
                        if (line.StartsWith("ETM Error:"))
                        {
                            Log.Information(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calibrate();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            _eyeTracker.GazeDataReceived += GazeDataReceived;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _eyeTracker.GazeDataReceived -= GazeDataReceived;
        }
    }
}
