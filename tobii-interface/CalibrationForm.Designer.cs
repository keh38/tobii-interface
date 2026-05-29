namespace tobii_interface
{
    partial class CalibrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationForm));
            pictureBox = new PictureBox();
            cancelButton = new Button();
            button1 = new Button();
            statusStrip1 = new StatusStrip();
            connectionLabel = new ToolStripStatusLabel();
            statusLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.Black;
            pictureBox.Location = new Point(1, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(587, 338);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(594, 12);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(594, 47);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "Apply";
            button1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { connectionLabel, statusLabel });
            statusStrip1.Location = new Point(0, 368);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(714, 26);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // connectionLabel
            // 
            connectionLabel.Name = "connectionLabel";
            connectionLabel.Size = new Size(84, 20);
            connectionLabel.Text = "Connection";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(49, 20);
            statusLabel.Text = "Status";
            // 
            // CalibrationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 394);
            Controls.Add(statusStrip1);
            Controls.Add(button1);
            Controls.Add(cancelButton);
            Controls.Add(pictureBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CalibrationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Calibration";
            FormClosing += CalibrationForm_FormClosing;
            Shown += CalibrationForm_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Button cancelButton;
        private Button button1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel connectionLabel;
        private ToolStripStatusLabel statusLabel;
    }
}