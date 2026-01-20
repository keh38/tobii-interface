namespace tobii_interface
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _eyeTracker != null)
            {
                _eyeTracker.GazeDataReceived -= GazeDataReceived;
                _eyeTracker.UserPositionGuideReceived -= UserPositionGuideReceived;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            SpPerfChart.ChartPen chartPen1 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen2 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen3 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen4 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen5 = new SpPerfChart.ChartPen();
            statusStrip = new StatusStrip();
            trackerStatusLabel = new ToolStripStatusLabel();
            distanceLabel = new ToolStripStatusLabel();
            fileLabel = new ToolStripStatusLabel();
            imageList = new ImageList(components);
            connectionTimer = new System.Windows.Forms.Timer(components);
            calibrateButton = new Button();
            runButton = new Button();
            stopButton = new Button();
            pupilChart = new SpPerfChart.PerfChart();
            splitContainer1 = new SplitContainer();
            recordButton = new CheckBox();
            framesLabel = new ToolStripStatusLabel();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { trackerStatusLabel, distanceLabel, framesLabel, fileLabel });
            statusStrip.Location = new Point(0, 197);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(692, 24);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // trackerStatusLabel
            // 
            trackerStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.Right;
            trackerStatusLabel.Name = "trackerStatusLabel";
            trackerStatusLabel.Size = new Size(101, 19);
            trackerStatusLabel.Text = "No tracker found";
            // 
            // distanceLabel
            // 
            distanceLabel.AutoSize = false;
            distanceLabel.BorderSides = ToolStripStatusLabelBorderSides.Right;
            distanceLabel.Name = "distanceLabel";
            distanceLabel.Size = new Size(75, 19);
            distanceLabel.Text = "35 cm";
            // 
            // fileLabel
            // 
            fileLabel.ForeColor = Color.FromArgb(228, 0, 0);
            fileLabel.Name = "fileLabel";
            fileLabel.Size = new Size(34, 19);
            fileLabel.Text = "File...";
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "nav_plain_red.png");
            imageList.Images.SetKeyName(1, "nav_plain_green.png");
            // 
            // connectionTimer
            // 
            connectionTimer.Tick += connectionTimer_Tick;
            // 
            // calibrateButton
            // 
            calibrateButton.Location = new Point(21, 17);
            calibrateButton.Name = "calibrateButton";
            calibrateButton.Size = new Size(93, 42);
            calibrateButton.TabIndex = 1;
            calibrateButton.Text = "Calibrate";
            calibrateButton.UseVisualStyleBackColor = true;
            calibrateButton.Click += calibrateButton_Click;
            // 
            // runButton
            // 
            runButton.Location = new Point(21, 69);
            runButton.Name = "runButton";
            runButton.Size = new Size(93, 42);
            runButton.TabIndex = 2;
            runButton.Text = "Run";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += runButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(21, 69);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(93, 42);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // pupilChart
            // 
            pupilChart.Dock = DockStyle.Fill;
            pupilChart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.World);
            pupilChart.Format = "F1";
            pupilChart.Location = new Point(0, 0);
            pupilChart.Margin = new Padding(4, 3, 4, 3);
            pupilChart.Max = 100D;
            pupilChart.MaxValueCount = 10000;
            pupilChart.Min = -100D;
            pupilChart.Name = "pupilChart";
            pupilChart.NumberOfLines = 2;
            pupilChart.PerfChartStyle.AntiAliasing = true;
            chartPen1.Color = Color.Black;
            chartPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen1.Width = 1F;
            pupilChart.PerfChartStyle.AvgLinePen = chartPen1;
            pupilChart.PerfChartStyle.BackgroundColorBottom = Color.Black;
            pupilChart.PerfChartStyle.BackgroundColorTop = Color.Black;
            chartPen2.Color = Color.Red;
            chartPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen2.Width = 1F;
            pupilChart.PerfChartStyle.ChartLinePen = chartPen2;
            chartPen3.Color = Color.FromArgb(32, 32, 32);
            chartPen3.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen3.Width = 1F;
            pupilChart.PerfChartStyle.HorizontalGridPen = chartPen3;
            chartPen4.Color = Color.Black;
            chartPen4.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen4.Width = 1F;
            pupilChart.PerfChartStyle.RefLinePen = chartPen4;
            pupilChart.PerfChartStyle.ShowAverageLine = true;
            pupilChart.PerfChartStyle.ShowHorizontalGridLines = true;
            pupilChart.PerfChartStyle.ShowVerticalGridLines = true;
            chartPen5.Color = Color.FromArgb(32, 32, 32);
            chartPen5.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen5.Width = 1F;
            pupilChart.PerfChartStyle.VerticalGridPen = chartPen5;
            pupilChart.ScaleMode = SpPerfChart.ScaleMode.Relative;
            pupilChart.Size = new Size(555, 197);
            pupilChart.TabIndex = 4;
            pupilChart.TimerInterval = 100;
            pupilChart.TimerMode = SpPerfChart.TimerMode.Simple;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(recordButton);
            splitContainer1.Panel1.Controls.Add(calibrateButton);
            splitContainer1.Panel1.Controls.Add(runButton);
            splitContainer1.Panel1.Controls.Add(stopButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pupilChart);
            splitContainer1.Size = new Size(692, 197);
            splitContainer1.SplitterDistance = 133;
            splitContainer1.TabIndex = 6;
            // 
            // recordButton
            // 
            recordButton.Appearance = Appearance.Button;
            recordButton.Location = new Point(21, 135);
            recordButton.Name = "recordButton";
            recordButton.Size = new Size(93, 40);
            recordButton.TabIndex = 5;
            recordButton.Text = "Record";
            recordButton.TextAlign = ContentAlignment.MiddleCenter;
            recordButton.UseVisualStyleBackColor = true;
            recordButton.CheckedChanged += recordButton_CheckedChanged;
            // 
            // framesLabel
            // 
            framesLabel.AutoSize = false;
            framesLabel.BorderSides = ToolStripStatusLabelBorderSides.Right;
            framesLabel.Name = "framesLabel";
            framesLabel.Size = new Size(80, 19);
            framesLabel.Text = "00000 frames";
            framesLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 221);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip);
            MinimumSize = new Size(0, 260);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tobii Interface";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStripStatusLabel trackerStatusLabel;
        private ImageList imageList;
        private System.Windows.Forms.Timer connectionTimer;
        private Button calibrateButton;
        private Button runButton;
        private Button stopButton;
        private SpPerfChart.PerfChart pupilChart;
        private SplitContainer splitContainer1;
        private ToolStripStatusLabel distanceLabel;
        private CheckBox recordButton;
        private ToolStripStatusLabel fileLabel;
        private ToolStripStatusLabel framesLabel;
    }
}
