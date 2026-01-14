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
            SpPerfChart.ChartPen chartPen16 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen17 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen18 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen19 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen20 = new SpPerfChart.ChartPen();
            statusStrip = new StatusStrip();
            trackerStatusLabel = new ToolStripStatusLabel();
            imageList = new ImageList(components);
            connectionTimer = new System.Windows.Forms.Timer(components);
            calibrateButton = new Button();
            runButton = new Button();
            stopButton = new Button();
            pupilChart = new SpPerfChart.PerfChart();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { trackerStatusLabel });
            statusStrip.Location = new Point(0, 408);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(840, 24);
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
            calibrateButton.Location = new Point(25, 32);
            calibrateButton.Name = "calibrateButton";
            calibrateButton.Size = new Size(128, 42);
            calibrateButton.TabIndex = 1;
            calibrateButton.Text = "Calibrate";
            calibrateButton.UseVisualStyleBackColor = true;
            calibrateButton.Click += button1_Click;
            // 
            // runButton
            // 
            runButton.Location = new Point(25, 94);
            runButton.Name = "runButton";
            runButton.Size = new Size(128, 42);
            runButton.TabIndex = 2;
            runButton.Text = "Run";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += runButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(25, 142);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(128, 42);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // pupilChart
            // 
            pupilChart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.World);
            pupilChart.Format = "F1";
            pupilChart.Location = new Point(186, 32);
            pupilChart.Margin = new Padding(4, 3, 4, 3);
            pupilChart.Max = new decimal(new int[] { 100, 0, 0, 0 });
            pupilChart.MaxValueCount = 10000;
            pupilChart.Name = "pupilChart";
            pupilChart.PerfChartStyle.AntiAliasing = true;
            chartPen16.Color = Color.Black;
            chartPen16.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen16.Width = 1F;
            pupilChart.PerfChartStyle.AvgLinePen = chartPen16;
            pupilChart.PerfChartStyle.BackgroundColorBottom = Color.DarkGreen;
            pupilChart.PerfChartStyle.BackgroundColorTop = Color.DarkGreen;
            chartPen17.Color = Color.Black;
            chartPen17.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen17.Width = 1F;
            pupilChart.PerfChartStyle.ChartLinePen = chartPen17;
            chartPen18.Color = Color.Black;
            chartPen18.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen18.Width = 1F;
            pupilChart.PerfChartStyle.HorizontalGridPen = chartPen18;
            chartPen19.Color = Color.Black;
            chartPen19.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen19.Width = 1F;
            pupilChart.PerfChartStyle.RefLinePen = chartPen19;
            pupilChart.PerfChartStyle.ShowAverageLine = true;
            pupilChart.PerfChartStyle.ShowHorizontalGridLines = true;
            pupilChart.PerfChartStyle.ShowVerticalGridLines = true;
            chartPen20.Color = Color.Black;
            chartPen20.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen20.Width = 1F;
            pupilChart.PerfChartStyle.VerticalGridPen = chartPen20;
            pupilChart.ScaleMode = SpPerfChart.ScaleMode.Absolute;
            pupilChart.Size = new Size(622, 152);
            pupilChart.TabIndex = 4;
            pupilChart.TimerInterval = 100;
            pupilChart.TimerMode = SpPerfChart.TimerMode.Disabled;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 432);
            Controls.Add(pupilChart);
            Controls.Add(stopButton);
            Controls.Add(runButton);
            Controls.Add(calibrateButton);
            Controls.Add(statusStrip);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tobii Interface";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
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
    }
}
