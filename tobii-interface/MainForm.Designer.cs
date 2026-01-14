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
            statusStrip = new StatusStrip();
            trackerStatusLabel = new ToolStripStatusLabel();
            imageList = new ImageList(components);
            connectionTimer = new System.Windows.Forms.Timer(components);
            calibrateButton = new Button();
            runButton = new Button();
            stopButton = new Button();
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
            // 
            // stopButton
            // 
            stopButton.Location = new Point(25, 142);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(128, 42);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 432);
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
    }
}
