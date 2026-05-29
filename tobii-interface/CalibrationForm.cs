using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Serilog;
using Tobii.Research;

using C462.Shared.Protocol.DTOs;
using KLib.Net;

namespace tobii_interface
{
    internal partial class CalibrationForm : Form
    {
        IEyeTracker _eyeTracker;
        Network _network;
        IPEndPoint _htsEndPoint;

        private int _tabletWidth = 0;
        private int _tabletHeight = 0;

        private bool _isRunning = false;
        private bool _stopCalibration;

        private NormalizedPoint2D _targetPoint = new NormalizedPoint2D(-1, -1);
        private CalibrationResult _calibrationResult;

        public CalibrationForm(IEyeTracker eyeTracker, Network network, IPEndPoint htsEndPoint)
        {
            _eyeTracker = eyeTracker;
            _network = network;
            _htsEndPoint = htsEndPoint;

            InitializeComponent();
        }

        private async void CalibrationForm_Shown(object sender, EventArgs e)
        {
            _network.OnStopCalibration += HandleStopCalibration;
            connectionLabel.Text = $"Connected to {_htsEndPoint.Address}:{_htsEndPoint.Port}";
            statusLabel.Text = "Initializing...";

            await Task.Run(() => Initialize());

            statusLabel.Text = "Ready.";
        }

        private void CalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isRunning)
            {
                e.Cancel = true;
                return;
            }

            _network.OnStopCalibration -= HandleStopCalibration;
        }

        private void HandleStopCalibration()
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleStopCalibration());
                return;
            }

            _stopCalibration = true;

            SendMessage("Finish");
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _stopCalibration = true;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            SendMessage("Finish");
            Close();
        }

        // --- Calibration logic --------------------------------------------------------------------------------
        private void startButton_Click(object sender, EventArgs e)
        {
            Task.Run(() => RunCalibration());
        }

        private void RunCalibration()
        {
            _calibrationResult = null;
            _isRunning = true;
            _stopCalibration = false;

            Invoke(() =>
            {
                startButton.Visible = false;
                applyButton.Enabled = false;
                cancelButton.Enabled = true;
            });

            // Create a calibration object.
            var calibration = new ScreenBasedCalibration(_eyeTracker);

            // Enter calibration mode.
            calibration.EnterCalibrationMode();

            // The coordinates are normalized, i.e. (0.0f, 0.0f) is the upper left corner and (1.0f, 1.0f) is the lower right corner.
            var pointsToCalibrate = new NormalizedPoint2D[] {
                new NormalizedPoint2D(0.5f, 0.5f),
                new NormalizedPoint2D(0.1f, 0.1f),
                new NormalizedPoint2D(0.1f, 0.9f),
                new NormalizedPoint2D(0.9f, 0.1f),
                new NormalizedPoint2D(0.9f, 0.9f),
            };

            foreach (var point in pointsToCalibrate)
            {
                _targetPoint = point;

                SendMessage("SetLocationNormalized", new TargetPointPayload() { X = point.X, Y = point.Y });

                // Show an image on screen where you want to calibrate.
                statusLabel.Text = $"Target point = ({point.X}, {point.Y})";
                Invoke(() => gazePicture.Refresh());

                // Wait a little for user to focus.
                Thread.Sleep(1000);

                // Collect data.
                CalibrationStatus status = calibration.CollectData(point);
                if (status != CalibrationStatus.Success)
                {
                    // Try again if it didn't go well the first time.
                    // Not all eye tracker models will fail at this point, as some instead fail on ComputeAndApply.
                    calibration.CollectData(point);
                }

                if (_stopCalibration) break;
            }

            SendMessage("SetLocationNormalized", new TargetPointPayload() { X = -1, Y = -1 });
            _targetPoint = new NormalizedPoint2D(-1, -1);
            Invoke(() => gazePicture.Refresh());


            if (!_stopCalibration)
            {
                // Compute and apply the calibration.
                _calibrationResult = calibration.ComputeAndApply();

                Log.Information("Compute and apply returned {0} and collected at {1} points.",
                    _calibrationResult.Status, _calibrationResult.CalibrationPoints.Count);
                statusLabel.Text = $"{_calibrationResult.Status} at {_calibrationResult.CalibrationPoints.Count} points.";

                Invoke(() => gazePicture.Refresh());
            }

            // The calibration is done. Leave calibration mode.
            calibration.LeaveCalibrationMode();

            _isRunning = false;

            Invoke(() =>
            {
                startButton.Visible = true;
                applyButton.Enabled = true;
            });
        }

        // --- Display ----------------------------------------------------------------------------------------

        private void Initialize()
        {
            var screenSize = GetTabletScreenSize();
            if (screenSize.IsEmpty)
            {
                MessageBox.Show("Failed to get screen size from HTS. Calibration cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HandleStopCalibration();
                return;
            }

            _tabletHeight = screenSize.Height;
            _tabletWidth = screenSize.Width;

            Invoke(() => gazePicture.Refresh());
        }

        private Size GetTabletScreenSize()
        {
            try
            {
                var response = SendRequest<string>("GetScreenSize");
                var parts = response.Split(',');
                if (parts.Length == 2)
                {
                    return new Size()
                    {
                        Width = int.Parse(parts[0]),
                        Height = int.Parse(parts[1])
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Error($"GetScreenSize failed: {ex.Message}");
            }

            return Size.Empty;
        }

        private void gazePicture_Paint(object sender, PaintEventArgs e)
        {
            if (_tabletWidth == 0) return;

            float aspectRatio = (float)_tabletWidth / _tabletHeight;

            float width = gazePicture.Width;
            float height = gazePicture.Width / aspectRatio;
            if (height > gazePicture.Height)
            {
                height = gazePicture.Height;
                width = height * aspectRatio;
            }

            float xoff = (gazePicture.Width - width) / 2;
            float yoff = (gazePicture.Height - height) / 2;

            var rect = new RectangleF(xoff, yoff, width, height);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);

            if (_targetPoint.X >= 0)
            {
                float x = xoff + (float)_targetPoint.X * width;
                float y = yoff + (float)_targetPoint.Y * height;
                float size = 10;

                rect = new RectangleF(x - size / 2, y - size / 2, size, size);
                e.Graphics.FillEllipse(new SolidBrush(Color.Gray), rect);
            }

            if (_calibrationResult == null)
                return;

            foreach (var point in _calibrationResult.CalibrationPoints)
            {
                foreach (var sample in point.CalibrationSamples)
                {
                    float x1 = xoff + (float)point.PositionOnDisplayArea.X * width;
                    float y1 = yoff + (float)point.PositionOnDisplayArea.Y * height;
                    float size = 6;

                    rect = new RectangleF(x1 - size / 2, y1 - size / 2, size, size);
                    e.Graphics.FillEllipse(new SolidBrush(Color.Gray), rect);

                    Debug.WriteLine($"Sample for point ({point.PositionOnDisplayArea.X}, {point.PositionOnDisplayArea.Y}): " +
                        $"LeftEye=({sample.LeftEye.PositionOnDisplayArea.X}, {sample.LeftEye.PositionOnDisplayArea.Y}), " +
                        $"RightEye=({sample.RightEye.PositionOnDisplayArea.X}, {sample.RightEye.PositionOnDisplayArea.Y})");

                    float x2 = xoff + (float)sample.LeftEye.PositionOnDisplayArea.X * width;
                    float y2 = yoff + (float)sample.LeftEye.PositionOnDisplayArea.Y * height;

                    e.Graphics.DrawLine(new Pen(Color.Blue), x1, y1, x2, y2);

                    x2 = xoff + (float)sample.RightEye.PositionOnDisplayArea.X * width;
                    y2 = yoff + (float)sample.RightEye.PositionOnDisplayArea.Y * height;

                    e.Graphics.DrawLine(new Pen(Color.Red), x1, y1, x2, y2);
                }
            }

        }

        // --- TCP helpers ------------------------------------------------------------------------------------

        public bool SendMessage(string command)
        {
            if (_htsEndPoint == null) return false;
            return KTcpClient.SendRequest(_htsEndPoint, TcpMessage.Request(command)).IsOk;
        }

        public bool SendMessage(string command, object payload)
        {
            if (_htsEndPoint == null) return false;
            return KTcpClient.SendRequest(_htsEndPoint, TcpMessage.Request(command, payload)).IsOk;
        }

        /// <summary>Sends a request and returns the typed payload from the response.</summary>
        public T SendRequest<T>(string command, object payload = null)
        {
            if (_htsEndPoint == null) return default;
            var request = payload != null
                ? TcpMessage.Request(command, payload)
                : TcpMessage.Request(command);
            var response = KTcpClient.SendRequest(_htsEndPoint, request);
            return response.IsOk ? response.GetPayload<T>() : default;
        }


    }
}
