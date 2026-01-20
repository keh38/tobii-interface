namespace SimplePerfChart
{
    partial class FrmTestingForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            SpPerfChart.ChartPen chartPen1 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen2 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen3 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen4 = new SpPerfChart.ChartPen();
            SpPerfChart.ChartPen chartPen5 = new SpPerfChart.ChartPen();
            grpBxChart = new System.Windows.Forms.GroupBox();
            perfChart = new SpPerfChart.PerfChart();
            grpBxRandVal = new System.Windows.Forms.GroupBox();
            chkBxTimerEnabled = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            numUpDnToInterval = new System.Windows.Forms.NumericUpDown();
            numUpDnFromInterval = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            grpBxValueGen = new System.Windows.Forms.GroupBox();
            numUpDnValTo = new System.Windows.Forms.NumericUpDown();
            numUpDnValFrom = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label11 = new System.Windows.Forms.Label();
            numUpDnTimerInterval = new System.Windows.Forms.NumericUpDown();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            cmbBxTimerMode = new System.Windows.Forms.ComboBox();
            label8 = new System.Windows.Forms.Label();
            cmbBxScaleMode = new System.Windows.Forms.ComboBox();
            label7 = new System.Windows.Forms.Label();
            cmbBxBorder = new System.Windows.Forms.ComboBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            propGrid = new System.Windows.Forms.PropertyGrid();
            bgWrkTimer = new System.ComponentModel.BackgroundWorker();
            btnApply = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            grpBxChart.SuspendLayout();
            grpBxRandVal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDnToInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUpDnFromInterval).BeginInit();
            grpBxValueGen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDnValTo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUpDnValFrom).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDnTimerInterval).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // grpBxChart
            // 
            grpBxChart.Controls.Add(perfChart);
            grpBxChart.Location = new System.Drawing.Point(14, 14);
            grpBxChart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpBxChart.Name = "grpBxChart";
            grpBxChart.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            grpBxChart.Size = new System.Drawing.Size(302, 163);
            grpBxChart.TabIndex = 0;
            grpBxChart.TabStop = false;
            grpBxChart.Text = "Chart";
            // 
            // perfChart
            // 
            perfChart.Dock = System.Windows.Forms.DockStyle.Fill;
            perfChart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            perfChart.Format = "F1";
            perfChart.Location = new System.Drawing.Point(7, 23);
            perfChart.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            perfChart.Max = 100D;
            perfChart.MaxValueCount = 10000;
            perfChart.Min = -100D;
            perfChart.Name = "perfChart";
            perfChart.NumberOfLines = 1;
            perfChart.PerfChartStyle.AntiAliasing = true;
            chartPen1.Color = System.Drawing.Color.LightGreen;
            chartPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            chartPen1.Width = 1F;
            perfChart.PerfChartStyle.AvgLinePen = chartPen1;
            perfChart.PerfChartStyle.BackgroundColorBottom = System.Drawing.Color.DarkOliveGreen;
            perfChart.PerfChartStyle.BackgroundColorTop = System.Drawing.Color.YellowGreen;
            chartPen2.Color = System.Drawing.Color.Gold;
            chartPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen2.Width = 1F;
            perfChart.PerfChartStyle.ChartLinePen = chartPen2;
            chartPen3.Color = System.Drawing.Color.DarkOliveGreen;
            chartPen3.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            chartPen3.Width = 1F;
            perfChart.PerfChartStyle.HorizontalGridPen = chartPen3;
            chartPen4.Color = System.Drawing.Color.Black;
            chartPen4.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen4.Width = 1F;
            perfChart.PerfChartStyle.RefLinePen = chartPen4;
            perfChart.PerfChartStyle.ShowAverageLine = true;
            perfChart.PerfChartStyle.ShowHorizontalGridLines = true;
            perfChart.PerfChartStyle.ShowVerticalGridLines = true;
            chartPen5.Color = System.Drawing.Color.DarkOliveGreen;
            chartPen5.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            chartPen5.Width = 1F;
            perfChart.PerfChartStyle.VerticalGridPen = chartPen5;
            perfChart.ScaleMode = SpPerfChart.ScaleMode.Relative;
            perfChart.Size = new System.Drawing.Size(288, 133);
            perfChart.TabIndex = 0;
            perfChart.TimerInterval = 100;
            perfChart.TimerMode = SpPerfChart.TimerMode.Disabled;
            // 
            // grpBxRandVal
            // 
            grpBxRandVal.Controls.Add(chkBxTimerEnabled);
            grpBxRandVal.Controls.Add(label5);
            grpBxRandVal.Controls.Add(numUpDnToInterval);
            grpBxRandVal.Controls.Add(numUpDnFromInterval);
            grpBxRandVal.Controls.Add(label3);
            grpBxRandVal.Controls.Add(label2);
            grpBxRandVal.Location = new System.Drawing.Point(14, 183);
            grpBxRandVal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpBxRandVal.Name = "grpBxRandVal";
            grpBxRandVal.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpBxRandVal.Size = new System.Drawing.Size(302, 104);
            grpBxRandVal.TabIndex = 1;
            grpBxRandVal.TabStop = false;
            grpBxRandVal.Text = "Value Generator Timer";
            // 
            // chkBxTimerEnabled
            // 
            chkBxTimerEnabled.AutoSize = true;
            chkBxTimerEnabled.Location = new System.Drawing.Point(19, 55);
            chkBxTimerEnabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkBxTimerEnabled.Name = "chkBxTimerEnabled";
            chkBxTimerEnabled.Size = new System.Drawing.Size(101, 19);
            chkBxTimerEnabled.TabIndex = 7;
            chkBxTimerEnabled.Text = "Timer Enabled";
            chkBxTimerEnabled.UseVisualStyleBackColor = true;
            chkBxTimerEnabled.CheckedChanged += chkBxTimerEnabled_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(245, 28);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(23, 15);
            label5.TabIndex = 6;
            label5.Text = "ms";
            // 
            // numUpDnToInterval
            // 
            numUpDnToInterval.Location = new System.Drawing.Point(162, 25);
            numUpDnToInterval.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numUpDnToInterval.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numUpDnToInterval.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            numUpDnToInterval.Name = "numUpDnToInterval";
            numUpDnToInterval.Size = new System.Drawing.Size(76, 23);
            numUpDnToInterval.TabIndex = 5;
            numUpDnToInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // numUpDnFromInterval
            // 
            numUpDnFromInterval.Location = new System.Drawing.Point(54, 25);
            numUpDnFromInterval.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numUpDnFromInterval.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numUpDnFromInterval.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            numUpDnFromInterval.Name = "numUpDnFromInterval";
            numUpDnFromInterval.Size = new System.Drawing.Size(76, 23);
            numUpDnFromInterval.TabIndex = 3;
            numUpDnFromInterval.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(136, 28);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(18, 15);
            label3.TabIndex = 2;
            label3.Text = "to";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(15, 28);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(33, 15);
            label2.TabIndex = 1;
            label2.Text = "from";
            // 
            // grpBxValueGen
            // 
            grpBxValueGen.Controls.Add(numUpDnValTo);
            grpBxValueGen.Controls.Add(numUpDnValFrom);
            grpBxValueGen.Controls.Add(label4);
            grpBxValueGen.Controls.Add(label6);
            grpBxValueGen.Location = new System.Drawing.Point(14, 294);
            grpBxValueGen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpBxValueGen.Name = "grpBxValueGen";
            grpBxValueGen.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpBxValueGen.Size = new System.Drawing.Size(302, 78);
            grpBxValueGen.TabIndex = 2;
            grpBxValueGen.TabStop = false;
            grpBxValueGen.Text = "Generated Values";
            // 
            // numUpDnValTo
            // 
            numUpDnValTo.Location = new System.Drawing.Point(162, 25);
            numUpDnValTo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numUpDnValTo.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numUpDnValTo.Name = "numUpDnValTo";
            numUpDnValTo.Size = new System.Drawing.Size(76, 23);
            numUpDnValTo.TabIndex = 10;
            numUpDnValTo.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numUpDnValFrom
            // 
            numUpDnValFrom.Location = new System.Drawing.Point(54, 25);
            numUpDnValFrom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numUpDnValFrom.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numUpDnValFrom.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numUpDnValFrom.Name = "numUpDnValFrom";
            numUpDnValFrom.Size = new System.Drawing.Size(76, 23);
            numUpDnValFrom.TabIndex = 9;
            numUpDnValFrom.Value = new decimal(new int[] { 100, 0, 0, int.MinValue });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(136, 28);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(18, 15);
            label4.TabIndex = 8;
            label4.Text = "to";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(15, 28);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(33, 15);
            label6.TabIndex = 7;
            label6.Text = "from";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(numUpDnTimerInterval);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(cmbBxTimerMode);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(cmbBxScaleMode);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(cmbBxBorder);
            groupBox1.Location = new System.Drawing.Point(323, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(302, 163);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Engine Properties";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(189, 117);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(23, 15);
            label11.TabIndex = 8;
            label11.Text = "ms";
            // 
            // numUpDnTimerInterval
            // 
            numUpDnTimerInterval.Location = new System.Drawing.Point(106, 114);
            numUpDnTimerInterval.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numUpDnTimerInterval.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numUpDnTimerInterval.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            numUpDnTimerInterval.Name = "numUpDnTimerInterval";
            numUpDnTimerInterval.Size = new System.Drawing.Size(76, 23);
            numUpDnTimerInterval.TabIndex = 7;
            numUpDnTimerInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            numUpDnTimerInterval.ValueChanged += numUpDnTimerInterval_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(8, 117);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(76, 15);
            label10.TabIndex = 6;
            label10.Text = "TimerInterval";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(7, 87);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(68, 15);
            label9.TabIndex = 5;
            label9.Text = "TimerMode";
            // 
            // cmbBxTimerMode
            // 
            cmbBxTimerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBxTimerMode.FormattingEnabled = true;
            cmbBxTimerMode.Location = new System.Drawing.Point(106, 83);
            cmbBxTimerMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbBxTimerMode.Name = "cmbBxTimerMode";
            cmbBxTimerMode.Size = new System.Drawing.Size(188, 23);
            cmbBxTimerMode.TabIndex = 4;
            cmbBxTimerMode.SelectedIndexChanged += cmbBxTimerMode_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(7, 55);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 15);
            label8.TabIndex = 3;
            label8.Text = "ScaleMode";
            // 
            // cmbBxScaleMode
            // 
            cmbBxScaleMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBxScaleMode.FormattingEnabled = true;
            cmbBxScaleMode.Location = new System.Drawing.Point(106, 52);
            cmbBxScaleMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbBxScaleMode.Name = "cmbBxScaleMode";
            cmbBxScaleMode.Size = new System.Drawing.Size(188, 23);
            cmbBxScaleMode.TabIndex = 2;
            cmbBxScaleMode.SelectedIndexChanged += cmbBxScaleMode_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(7, 24);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(67, 15);
            label7.TabIndex = 1;
            label7.Text = "BorderStyle";
            // 
            // cmbBxBorder
            // 
            cmbBxBorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBxBorder.FormattingEnabled = true;
            cmbBxBorder.Location = new System.Drawing.Point(106, 21);
            cmbBxBorder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbBxBorder.Name = "cmbBxBorder";
            cmbBxBorder.Size = new System.Drawing.Size(188, 23);
            cmbBxBorder.TabIndex = 0;
            cmbBxBorder.SelectedIndexChanged += cmbBxBorder_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(propGrid);
            groupBox2.Location = new System.Drawing.Point(323, 183);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            groupBox2.Size = new System.Drawing.Size(302, 189);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Style Properties";
            // 
            // propGrid
            // 
            propGrid.CommandsVisibleIfAvailable = false;
            propGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            propGrid.HelpVisible = false;
            propGrid.Location = new System.Drawing.Point(7, 23);
            propGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            propGrid.Name = "propGrid";
            propGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            propGrid.SelectedObject = perfChart;
            propGrid.Size = new System.Drawing.Size(288, 159);
            propGrid.TabIndex = 0;
            propGrid.ToolbarVisible = false;
            // 
            // bgWrkTimer
            // 
            bgWrkTimer.DoWork += bgWrkTimer_DoWork;
            bgWrkTimer.RunWorkerCompleted += bgWrkTimer_RunWorkerCompleted;
            // 
            // btnApply
            // 
            btnApply.Location = new System.Drawing.Point(538, 380);
            btnApply.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnApply.Name = "btnApply";
            btnApply.Size = new System.Drawing.Size(88, 27);
            btnApply.TabIndex = 5;
            btnApply.Text = "&Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(448, 380);
            btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(88, 27);
            btnClear.TabIndex = 6;
            btnClear.Text = "&Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // FrmTestingForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(646, 442);
            Controls.Add(btnClear);
            Controls.Add(btnApply);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(grpBxValueGen);
            Controls.Add(grpBxRandVal);
            Controls.Add(grpBxChart);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FrmTestingForm";
            Text = "PerfChart Testing Application";
            grpBxChart.ResumeLayout(false);
            grpBxRandVal.ResumeLayout(false);
            grpBxRandVal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDnToInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUpDnFromInterval).EndInit();
            grpBxValueGen.ResumeLayout(false);
            grpBxValueGen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDnValTo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUpDnValFrom).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDnTimerInterval).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBxChart;
        private SpPerfChart.PerfChart perfChart;
        private System.Windows.Forms.GroupBox grpBxRandVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numUpDnToInterval;
        private System.Windows.Forms.NumericUpDown numUpDnFromInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBxValueGen;
        private System.Windows.Forms.NumericUpDown numUpDnValTo;
        private System.Windows.Forms.NumericUpDown numUpDnValFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid propGrid;
        private System.Windows.Forms.CheckBox chkBxTimerEnabled;
        private System.ComponentModel.BackgroundWorker bgWrkTimer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numUpDnTimerInterval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbBxTimerMode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBxScaleMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBxBorder;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClear;
    }
}