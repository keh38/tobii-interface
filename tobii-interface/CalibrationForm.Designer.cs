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
            gazePicture = new PictureBox();
            cancelButton = new Button();
            statusStrip1 = new StatusStrip();
            connectionLabel = new ToolStripStatusLabel();
            statusLabel = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            applyButton = new Button();
            startButton = new Button();
            ((System.ComponentModel.ISupportInitialize)gazePicture).BeginInit();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // gazePicture
            // 
            gazePicture.BackColor = SystemColors.Control;
            gazePicture.BorderStyle = BorderStyle.FixedSingle;
            gazePicture.Dock = DockStyle.Fill;
            gazePicture.Location = new Point(3, 2);
            gazePicture.Margin = new Padding(3, 2, 3, 2);
            gazePicture.Name = "gazePicture";
            gazePicture.Size = new Size(428, 333);
            gazePicture.TabIndex = 0;
            gazePicture.TabStop = false;
            gazePicture.Paint += gazePicture_Paint;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(3, 2);
            cancelButton.Margin = new Padding(3, 2, 3, 2);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(82, 22);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { connectionLabel, statusLabel });
            statusStrip1.Location = new Point(0, 337);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(534, 24);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // connectionLabel
            // 
            connectionLabel.BorderSides = ToolStripStatusLabelBorderSides.Right;
            connectionLabel.Name = "connectionLabel";
            connectionLabel.Size = new Size(73, 19);
            connectionLabel.Text = "Connection";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 19);
            statusLabel.Text = "Status";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(gazePicture, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(534, 337);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(applyButton);
            panel1.Controls.Add(startButton);
            panel1.Controls.Add(cancelButton);
            panel1.Location = new Point(437, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(88, 52);
            panel1.TabIndex = 5;
            // 
            // applyButton
            // 
            applyButton.Enabled = false;
            applyButton.Location = new Point(3, 28);
            applyButton.Margin = new Padding(3, 2, 3, 2);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(82, 22);
            applyButton.TabIndex = 4;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // startButton
            // 
            startButton.Location = new Point(3, 2);
            startButton.Margin = new Padding(3, 2, 3, 2);
            startButton.Name = "startButton";
            startButton.Size = new Size(82, 22);
            startButton.TabIndex = 3;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // CalibrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 361);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(550, 400);
            Name = "CalibrationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Calibration";
            FormClosing += CalibrationForm_FormClosing;
            Shown += CalibrationForm_Shown;
            ((System.ComponentModel.ISupportInitialize)gazePicture).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox gazePicture;
        private Button cancelButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel connectionLabel;
        private ToolStripStatusLabel statusLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button startButton;
        private Button applyButton;
    }
}