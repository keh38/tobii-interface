using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Text;

using Tobii.Research;

using KLib.KGraphics;

namespace tobii_interface
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class MainForm : Form
    {
        private Settings _settings;
        private string _logPath = "";
        private IEyeTracker? _eyeTracker = null;

        private readonly float _minDistance = 35;
        private readonly float _maxDistance = 65;

        private Color _inRangeColor = Color.FromArgb(128, 255, 128);
        private Color _outOfRangeColor = Color.FromArgb(255, 128, 128);

        private bool _isRunning = false;
        private bool _isRecording = false;
        private bool _isRemote = false;
        private int _framesAcquired = 0;
        private string _filePath = "";

        private Network _network;

        internal class CombinedEyeData
        {
            public List<EyeData> eyeDataList = new List<EyeData>();
            public long deviceTimeStamp;
            public long systemTimeStamp;
            public CombinedEyeData(long deviceTimeStamp, long systemTimeStamp, EyeData left, EyeData right)
            {
                this.deviceTimeStamp = deviceTimeStamp;
                this.systemTimeStamp = systemTimeStamp;
                eyeDataList.Add(left);
                eyeDataList.Add(right);
            }
        }

        private Queue<CombinedEyeData> _dataQueue = new Queue<CombinedEyeData>();
        private CancellationTokenSource _queueCancellationToken = new CancellationTokenSource();

        public int Status { get { return _isRecording ? 1 : 0; } }

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

            _network = new Network(this);

            distanceLabel.Text = "---";
            fileLabel.Text = "";
            scaleLabel.Text = "---";
            framesLabel.Text = "---";

            runButton.Enabled = false;
            recordButton.Enabled = false;
        }

        private async Task StartLogging()
        {
            _logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "EPL",
                "Logs",
                $"TobiiInterface-{DateTime.Now:yyyyMMdd}.log");

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

        private void MainForm_Load(object sender, EventArgs e) { }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            await StartLogging();

            Log.Information($"Tobii Interface v{Assembly.GetExecutingAssembly().GetName().Version} started");

            trackerStatusLabel.Image = imageList.Images[0];
            trackerStatusLabel.Text = "No tracker found";

            Log.Information("Starting discovery server");
            _network.StartDiscoveryServer();

            connectionTimer.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _network.Disconnect();

            _settings.LastPosition = new Rectangle(Location.X, Location.Y, Width, Height);
            _settings.Save();

            Log.Information("Exit");
            Log.CloseAndFlush();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (_isRecording && !string.IsNullOrEmpty(_filePath))
            {
                CompactPath(fileLabel, _filePath);
            }
        }

        private void connectionTimer_Tick(object sender, EventArgs e)
        {
            connectionTimer.Interval = 1000;
            var list = EyeTrackingOperations.FindAllEyeTrackers();

            if (list.Count > 0)
            {
                _eyeTracker = EyeTrackingOperations.GetEyeTracker(list[0].Address);

                trackerStatusLabel.Image = imageList.Images[1];
                trackerStatusLabel.Text = _eyeTracker.DeviceName;

                runButton.Enabled = true;

                connectionTimer.Stop();
            }
        }

        private void GazeDataReceived(object? sender, GazeDataEventArgs args)
        {
            double left = args.LeftEye.Pupil.Validity == Validity.Valid ? args.LeftEye.Pupil.PupilDiameter : double.NaN;
            double right = args.RightEye.Pupil.Validity == Validity.Valid ? args.RightEye.Pupil.PupilDiameter : double.NaN;

            pupilChart.AddValue(left, right);
            if (_isRecording)
            {
                var data = new CombinedEyeData(args.DeviceTimeStamp, args.SystemTimeStamp, args.LeftEye, args.RightEye);
                _dataQueue.Enqueue(data);
            }
            try
            {
                Invoke(new Action(() => UpdateScaleLabel()));
            }
            catch { }
        }

        private void UserPositionGuideReceived(object? sender, UserPositionGuideEventArgs args)
        {
            float left = args.LeftEye.Validity == Validity.Valid ? args.LeftEye.UserPosition.Z : float.NaN;
            float right = args.RightEye.Validity == Validity.Valid ? args.RightEye.UserPosition.Z : float.NaN;

            try
            {
                Invoke(new Action(() => UpdateUserPositionDisplay(left, right)));
            }
            catch { }
        }

        private void UpdateUserPositionDisplay(float left, float right)
        {
            float distance = float.NaN;
            if (!float.IsNaN(left) && !float.IsNaN(right))
            {
                distance = (left + right) / 2.0f;
            }
            else if (!float.IsNaN(left))
            {
                distance = left;
            }
            else if (!float.IsNaN(right))
            {
                distance = right;
            }
            distance *= 100;

            if (!float.IsNaN(distance))
            {
                distanceLabel.Text = $"{distance:F0} cm";

                Color color = _outOfRangeColor;
                if (distance >= _minDistance && distance <= _maxDistance)
                {
                    color = _inRangeColor;
                }
                distanceLabel.BackColor = color;
            }
        }

        private void UpdateScaleLabel()
        {
            if (_eyeTracker == null) return;

            float fps = _eyeTracker.GetGazeOutputFrequency();
            float plotWidth = pupilChart.NumberOfFrames / fps;
            double plotHeight = pupilChart.CurrentRange;

            scaleLabel.Text = $"{plotHeight:F1}mm x {plotWidth:F2}s";
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

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            Calibrate();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (_eyeTracker == null) return;

            _eyeTracker.GazeDataReceived += GazeDataReceived;
            _eyeTracker.UserPositionGuideReceived += UserPositionGuideReceived;

            _isRunning = true;
            recordButton.Enabled = true;
            runButton.Visible = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (_eyeTracker == null) return;

            _eyeTracker.GazeDataReceived -= GazeDataReceived;
            _eyeTracker.UserPositionGuideReceived -= UserPositionGuideReceived;

            if (_isRecording)
            {
                recordButton.Checked = false;
            }

            _isRunning = false;
            runButton.Visible = true;
            recordButton.Enabled = false;
        }

        private void recordButton_CheckedChanged(object sender, EventArgs e)
        {
            if (recordButton.Checked)
            {
                if (_eyeTracker == null)
                {
                    recordButton.Checked = false;
                    return;
                }

                recordButton.BackColor = Color.FromArgb(228, 127, 127);

                if (!_isRemote)
                {
                    StartRecordingLocal();
                }
            }
            else
            {
                recordButton.BackColor = SystemColors.Control;

                Debug.WriteLine("stopping recording");
                StopRecording();
            }
        }

        private void StartRecordingLocal()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "TobiiRecordings");

            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(
                folder,
                $"TobiiRecording-{DateTime.Now:yyyyMMdd-HHmmss}.tsr");

            StartRecording(filePath);
        }

        public void StartRecordingRemote(string filePath)
        {
            _isRemote = true;
            if (!_isRunning)
            {
                Invoke(new Action(() => runButton_Click(null, null)));
            }

            Invoke(new Action(() => recordButton.Checked = true));

            StartRecording(filePath);
        }   

        private void StartRecording(string filePath)
        {
            _filePath = filePath;
            CompactPath(fileLabel, filePath);
            Log.Information($"saving data to {filePath}");

            _dataQueue.Clear();
            _framesAcquired = 0;
            framesLabel.Text = "0 frames";

            _queueCancellationToken = new CancellationTokenSource();
            Task.Run(() =>
            {
                ProcessDataQueue(filePath, _queueCancellationToken.Token);
            }, _queueCancellationToken.Token);

            _isRecording = true;
        }

        public void StopRecordingRemote()
        {
            Invoke(new Action(() => recordButton.Checked = false));
        }

        private void StopRecording()
        {
            if (!_isRecording) return;

            _isRecording = false;
            _isRemote = false;

            _queueCancellationToken.Cancel();

            fileLabel.Text = "";
            framesLabel.Text = "";

            Debug.WriteLine($"recording stopped, {_framesAcquired} frames acquired");
        }

        private void CompactPath(ToolStripStatusLabel pathLabel, string filePath)
        {
            string compactPath = filePath;
            string filename = Path.GetFileName(filePath);
            string folder = Path.GetDirectoryName(filePath) ?? "";

            var sub = folder.Split(Path.DirectorySeparatorChar);

            int lastSubIndex = sub.Length - 2;
            int firstRemovedIndex = lastSubIndex;

            using (Graphics g = pathLabel.GetCurrentParent().CreateGraphics())
            {
                while (true)
                {
                    var size = TextRenderer.MeasureText(g, compactPath, pathLabel.Font);

                    if (size.Width <= pathLabel.Width)
                    {
                        break;
                    }

                    string toRemove = "";
                    for (int k = firstRemovedIndex; k <= lastSubIndex; k++)
                    {
                        toRemove += sub[k] + Path.DirectorySeparatorChar;
                    }

                    compactPath = filePath.Replace(toRemove, "..." + Path.DirectorySeparatorChar);

                    if (firstRemovedIndex > 1)
                    {
                        firstRemovedIndex--;
                    }
                    else if (lastSubIndex < sub.Length - 1)
                    {
                        lastSubIndex++;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            pathLabel.Text = compactPath;
        }

        private void ProcessDataQueue(string filePath, CancellationToken ct)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                while (true)
                {
                    if (_dataQueue.Count == 0)
                    {
                        if (ct.IsCancellationRequested) break;
                        Thread.Sleep(50);
                    }
                    else
                    {
                        _framesAcquired++;
                        Invoke(new Action(() => framesLabel.Text = $"{_framesAcquired} frames"));

                        var data = _dataQueue.Dequeue();
                        writer.Write(data.deviceTimeStamp);
                        writer.Write(data.systemTimeStamp);

                        foreach (var eye in data.eyeDataList)
                        {
                            bool gazeValidity = eye.GazePoint.Validity == Validity.Valid;
                            float gazeX = eye.GazePoint.PositionOnDisplayArea.X;
                            float gazeY = eye.GazePoint.PositionOnDisplayArea.Y;
                            float pupilDiameter = eye.Pupil.Validity == Validity.Valid ? (float)eye.Pupil.PupilDiameter : float.NaN;

                            writer.Write(gazeValidity);
                            writer.Write(gazeX);
                            writer.Write(gazeY);
                            writer.Write(pupilDiameter);
                        }
                    }
                }
            }
        }

    }
}
