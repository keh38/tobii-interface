using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SpPerfChart
{
    /// <summary>
    /// Scale mode for value aspect ratio
    /// </summary>
    public enum ScaleMode
    {
        /// <summary>
        /// Absolute Scale Mode: Values from 0 to 100 are accepted and displayed
        /// </summary>
        Absolute,
        /// <summary>
        /// Relative Scale Mode: All values are allowed and displayed in a proper relation
        /// </summary>
        Relative
    }


    /// <summary>
    /// Chart Refresh Mode Timer Control Mode
    /// </summary>
    public enum TimerMode
    {
        /// <summary>
        /// Chart is refreshed when a value is added
        /// </summary>
        Disabled,
        /// <summary>
        /// Chart is refreshed every <c>TimerInterval</c> milliseconds, adding all values
        /// in the queue to the chart. If there are no values in the queue, a 0 (zero) is added
        /// </summary>
        Simple,
        /// <summary>
        /// Chart is refreshed every <c>TimerInterval</c> milliseconds, adding an average of
        /// all values in the queue to the chart. If there are no values in the queue,
        /// 0 (zero) is added
        /// </summary>
        SynchronizedAverage,
        /// <summary>
        /// Chart is refreshed every <c>TimerInterval</c> milliseconds, adding the sum of
        /// all values in the queue to the chart. If there are no values in the queue,
        /// 0 (zero) is added
        /// </summary>
        SynchronizedSum
    }


    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class PerfChart : UserControl
    {
        #region *** Constants ***

        // Keep only a maximum MAX_VALUE_COUNT amount of values; This will allow
        //        private const int MAX_VALUE_COUNT = 512;
        // Draw a background grid with a fixed line spacing
        //private const int GRID_SPACING = 16;
        private float _gridSpacing = 16;
        private int _vertGridSpacing = 16;

        private int _maxValueCount = 10000;

        #endregion


        #region *** Member Variables ***

        // Amount of currently visible values (calculated from control width and value spacing)
        private int visibleValues = 0;
        // Horizontal value space in Pixels
        private int valueSpacing = 1;
        private float fineValueSpacing = 0.5f; // pixels/frame
        private float lastX;
        // The currently highest displayed value, required for Relative Scale Mode
        private double _currentMaxValue = 0;
        private double _currentMinValue = 0;
        // Offset value for the scrolling grid
        private int gridScrollOffset = 0;
        private float fineGridScrollOffset = 0;
        private float _fineGridOrigin = 0;
        // The current average value
        private double averageValue = 0;
        // Border Style
        private Border3DStyle b3dstyle = Border3DStyle.Sunken;
        // Scale mode for value aspect ratio
        private ScaleMode scaleMode = ScaleMode.Absolute;
        // Timer Mode
        private TimerMode timerMode;
        // List of stored values
        private List<double[]> drawValues;// = new List<decimal>(MAX_VALUE_COUNT);
        // Value queue for Timer Modes
        private Queue<double[]> waitingValues = new Queue<double[]>();
        // Style and Design
        private PerfChartStyle perfChartStyle;

        private double _maxValue = 100;
        private double _minValue = -100;
        private string _maxFormat = "F1";

        #endregion


        #region *** Constructors ***

        public PerfChart() {
            InitializeComponent();

            // Initialize Variables
            perfChartStyle = new PerfChartStyle();

            // Set Optimized Double Buffer to reduce flickering
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Redraw when resized
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.Font = SystemInformation.MenuFont;

            drawValues = new List<double[]>(_maxValueCount);
    }

    #endregion


    #region *** Properties ***

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), Description("Appearance and Style")]
        public PerfChartStyle PerfChartStyle {
            get { return perfChartStyle; }
            set { perfChartStyle = value; }
        }


        [DefaultValue(typeof(Border3DStyle), "Sunken"), Description("BorderStyle"), Category("Appearance")]
        new public Border3DStyle BorderStyle {
            get {
                return b3dstyle;
            }
            set {
                b3dstyle = value;
                Invalidate();
            }
        }

        public ScaleMode ScaleMode {
            get { return scaleMode; }
            set { scaleMode = value; }
        }

        public int MaxValueCount
        {
            get { return _maxValueCount; }
            set
            {
                _maxValueCount = value;
                drawValues = new List<double[]>(_maxValueCount);
            }
        }

        public int NumberOfLines { get; set; } = 1;

        public double Max
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public double Min
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public double CurrentRange
        {
            get { return _currentMaxValue - _currentMinValue; }
        }

        public string Format
        {
            get { return _maxFormat; }
            set { _maxFormat = value; }
        }

        public float FrameRate
        {
            set
            {
                _gridSpacing = value * fineValueSpacing * 0.5f;
            }
        }

        public float NumberOfFrames
        {
            get { return Width * fineValueSpacing; }
        }

        public TimerMode TimerMode {
            get { return timerMode; }
            set {
                if (value == TimerMode.Disabled) {
                    // Stop and append only when changed
                    if (timerMode != TimerMode.Disabled) {
                        timerMode = value;

                        tmrRefresh.Stop();
                        // If there are any values in the queue, append them
                        ChartAppendFromQueue();
                    }
                }
                else {
                    timerMode = value;
                    tmrRefresh.Start();
                }
            }
        }

        public int TimerInterval {
            get { return tmrRefresh.Interval; }
            set {
                if (value < 15)
                    throw new ArgumentOutOfRangeException("TimerInterval", value, "The Timer interval must be greater then 15");
                else
                    tmrRefresh.Interval = value;
            }
        }

        #endregion


        #region *** Public Methods ***

        /// <summary>
        /// Clears the whole chart
        /// </summary>
        public void Clear() {
            drawValues.Clear();
            Invalidate();
        }


        /// <summary>
        /// Adds a value to the Chart Line
        /// </summary>
        /// <param name="value">progress value</param>
        public void AddValue(params double[] value) {
            //if (scaleMode == ScaleMode.Absolute && value > 100M)
            //    throw new Exception(String.Format("Values greater then 100 not allowed in ScaleMode: Absolute ({0})", value));
            if (scaleMode == ScaleMode.Absolute)
            {
                for (int k = 0; k < value.Length; k++)
                    if (value[k] < _minValue)
                        value[k] = _minValue;
                    else if (value[k] > _maxValue)
                        value[k] = _maxValue;
            }

            switch (timerMode) {
                case TimerMode.Disabled:
                    ChartAppend(value);
                    Invalidate();
                    break;
                case TimerMode.Simple:
                case TimerMode.SynchronizedAverage:
                case TimerMode.SynchronizedSum:
                    // For all Timer Modes, the Values are stored in the Queue
                    AddValueToQueue(value);
                    break;
                default:
                    throw new Exception(String.Format("Unsupported TimerMode: {0}", timerMode));
            }
        }

        public void ResetGrid()
        {
            _fineGridOrigin = fineGridScrollOffset;
        }

        #endregion


        #region *** Private Methods: Common ***

        /// <summary>
        /// Add value to the queue for a timed refresh
        /// </summary>
        /// <param name="value"></param>
        private void AddValueToQueue(params double[] value) 
        {
            waitingValues.Enqueue(value);
        }


        /// <summary>
        /// Appends value <paramref name="value"/> to the chart (without redrawing)
        /// </summary>
        /// <param name="value">performance value</param>
        private void ChartAppend(double[] value) {
            // Insert at first position; Negative values are flatten to 0 (zero)
//            drawValues.Insert(0, Math.Max(value, 0));
            drawValues.Insert(0, value);

            // Remove last item if maximum value count is reached
            //if (drawValues.Count > MAX_VALUE_COUNT)
            //    drawValues.RemoveAt(MAX_VALUE_COUNT);
            if (drawValues.Count > _maxValueCount)
                drawValues.RemoveAt(_maxValueCount);

            // Calculate horizontal grid offset for "scrolling" effect
            //            gridScrollOffset += valueSpacing;
            fineGridScrollOffset += fineValueSpacing;

            if (fineGridScrollOffset > _gridSpacing)
            {
                fineGridScrollOffset -= _gridSpacing;
            }

            //gridScrollOffset = (int)Math.Floor(fineGridScrollOffset - _fineGridOrigin);

            //if (gridScrollOffset > _gridSpacing)
            //    gridScrollOffset = gridScrollOffset % _gridSpacing;
        }


        /// <summary>
        /// Appends Values from queue
        /// </summary>
        private void ChartAppendFromQueue() {
            // Proceed only if there are values at all
            if (waitingValues.Count > 0) {
                if (timerMode == TimerMode.Simple) {
                    while (waitingValues.Count > 0)
                        ChartAppend(waitingValues.Dequeue());
                }
                else if (timerMode == TimerMode.SynchronizedAverage ||
                         timerMode == TimerMode.SynchronizedSum) {
                    // appendValue variable is used for calculating the average or sum value
                    double appendValue = 0;
                    int valueCount = waitingValues.Count;

                    //while (waitingValues.Count > 0)
                    //    appendValue += waitingValues.Dequeue();

                    //// Calculate Average value in SynchronizedAverage Mode
                    //if (timerMode == TimerMode.SynchronizedAverage)
                    //    appendValue = appendValue / (double)valueCount;

                    //// Finally append the value
                    //ChartAppend(appendValue);
                }
            }
            else {
                // Always add 0 (Zero) if there are no values in the queue
             //   ChartAppend(Decimal.Zero);
            }

            // Refresh the Chart
            Invalidate();
        }

        /// <summary>
        /// Calculates the vertical Position of a value in relation the chart size,
        /// Scale Mode and, if ScaleMode is Relative, to the current maximum value
        /// </summary>
        /// <param name="value">performance value</param>
        /// <returns>vertical Point position in Pixels</returns>
        private int CalcVerticalPosition(double value) 
        {
            if (double.IsNaN(value))
            {
                return -1;
            }    

            double result = -1;
            int offset = 5;

            if (scaleMode == ScaleMode.Absolute)
            {
                result = value * (this.Height - offset) / _maxValue;
            }
            else if (scaleMode == ScaleMode.Relative)
            {
                result = (_currentMaxValue > _currentMinValue) ? ((value - _currentMinValue) * (this.Height - offset) / (_currentMaxValue - _currentMinValue)) : 0;
            }

            if (result < 0) return -1;

            result = this.Height - result;

            return Convert.ToInt32(Math.Round(result)) - offset;
        }


        /// <summary>
        /// Returns the currently highest (displayed) value, for Relative ScaleMode
        /// </summary>
        /// <returns></returns>
        private double GetMaximumValueForRelativeMode()
        {
            double maxValue = double.NegativeInfinity;

            for (int i = 0; i < visibleValues; i++)
            {
                for (int k = 0; k < NumberOfLines; k++)
                {
                    // Set if higher then previous max value
                    if (drawValues[i] != null && drawValues[i][k] > maxValue)
                        maxValue = drawValues[i][k];
                }
            }

            return maxValue;
        }

        private double GetMinimumValueForRelativeMode()
        {
            double minValue = double.PositiveInfinity;

            for (int i = 0; i < visibleValues; i++)
            {
                for (int k = 0; k < NumberOfLines; k++)
                {
                    if (drawValues[i][k] < minValue)
                        minValue = drawValues[i][k];
                }
            }

            return minValue;
        }

        #endregion


        #region *** Private Methods: Drawing ***

        /// <summary>
        /// Draws the chart (w/o background or grid, but with border) to the Graphics canvas
        /// </summary>
        /// <param name="g">Graphics</param>
        private void DrawChart(Graphics g)
        {
//            visibleValues = Math.Min(this.Width / valueSpacing, drawValues.Count);
            visibleValues = Math.Min(this.Width * (int)(1 / fineValueSpacing), drawValues.Count);

            if (scaleMode == ScaleMode.Relative || _maxValue < 100)
            {
                _currentMaxValue = GetMaximumValueForRelativeMode();
                _currentMinValue = GetMinimumValueForRelativeMode();
            }

            // Only draw average line when possible (visibleValues) and needed (style setting)
            //if (visibleValues > 0 && perfChartStyle.ShowAverageLine) {
            //    averageValue = 0;
            //    DrawAverageLine(g);
            //}

            // Connect all visible values with lines
            for (int k = 0; k < NumberOfLines; k++)
            {
                // Dirty little "trick": initialize the first previous Point outside the bounds
                Point previousPoint = new Point(Width + valueSpacing, Height);
                lastX = Width + fineValueSpacing;
                Point currentPoint = new Point();

                var pen = perfChartStyle.ChartLinePen.Pen;
                pen.Color = (k == 0) ? Color.Blue : Color.Red;

                for (int i = 0; i < visibleValues; i++)
                {
                    lastX -= fineValueSpacing;
                    currentPoint.X = (int)Math.Ceiling(lastX);
                    currentPoint.Y = CalcVerticalPosition(drawValues[i][k]);

                    // Actually draw the line
                    if (currentPoint.Y > 0)
                    {
                        if (previousPoint.Y > 0)
                        {
                            g.DrawLine(pen, previousPoint, currentPoint);
                        }
                        else
                        {
                            g.DrawLine(pen, new Point(currentPoint.X+1, currentPoint.Y) , currentPoint);
                        }
                    }

                    //if (drawValues[i] < 0)
                    //{
                    //    g.DrawLine(perfChartStyle.RefLinePen.Pen, currentPoint.X, 0, currentPoint.X, this.Height);
                    //}

                    previousPoint = currentPoint;
                }
            }
            // Draw current relative maximum value stirng
            //if (scaleMode == ScaleMode.Relative || _maxValue < 100) {
            //    SolidBrush sb = new SolidBrush(perfChartStyle.ChartLinePen.Color);
            //    g.DrawString(currentMaxValue.ToString(_maxFormat), this.Font, sb, 4.0f, 2.0f);
            //}

            // Draw Border on top
            ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, b3dstyle);
        }


        private void DrawAverageLine(Graphics g) 
        {
            //for (int i = 0; i < visibleValues; i++)
            //    averageValue += drawValues[i];

            //averageValue = averageValue / visibleValues;

            //int verticalPosition = CalcVerticalPosition(averageValue);
            //g.DrawLine(perfChartStyle.AvgLinePen.Pen, 0, verticalPosition, Width, verticalPosition);
        }

        /// <summary>
        /// Draws the background gradient and the grid into Graphics <paramref name="g"/>
        /// </summary>
        /// <param name="g">Graphic</param>
        private void DrawBackgroundAndGrid(Graphics g) {
            // Draw the background Gradient rectangle
            Rectangle baseRectangle = new Rectangle(0, 0, this.Width, this.Height);
            using (Brush gradientBrush = new LinearGradientBrush(baseRectangle, perfChartStyle.BackgroundColorTop, perfChartStyle.BackgroundColorBottom, LinearGradientMode.Vertical)) {
                g.FillRectangle(gradientBrush, baseRectangle);
            }

            // Draw all visible, vertical gridlines (if wanted)
            if (perfChartStyle.ShowVerticalGridLines)
            {
                for (float x = Width - (fineGridScrollOffset - _fineGridOrigin); x>=0; x-=_gridSpacing)
                {
                    g.DrawLine(perfChartStyle.VerticalGridPen.Pen, (int)x, 0, (int)x, Height);
                }

                //for (int i = Width - gridScrollOffset; i >= 0; i -= (int)_gridSpacing) {
                //    g.DrawLine(perfChartStyle.VerticalGridPen.Pen, i, 0, i, Height);
                //}
            }

            // Draw all visible, horizontal gridlines (if wanted)
            if (perfChartStyle.ShowHorizontalGridLines) {
                for (int i = 0; i < Height; i += _vertGridSpacing) {
                    g.DrawLine(perfChartStyle.HorizontalGridPen.Pen, 0, i, Width, i);
                }
            }
        }

        #endregion


        #region *** Overrides ***

        /// Override OnPaint method
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            // Enable AntiAliasing, if needed
            if (perfChartStyle.AntiAliasing)
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawBackgroundAndGrid(e.Graphics);
            DrawChart(e.Graphics);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            Invalidate();
        }

        #endregion


        #region *** Event Handlers ***

        private void colorSet_ColorSetChanged(object sender, EventArgs e) {
            //Refresh Chart on Resize
            Invalidate();
        }

        private void tmrRefresh_Tick(object sender, EventArgs e) {
            // Don't execute event if running in design time
            if (this.DesignMode) return;

            ChartAppendFromQueue();
        }

        #endregion
    }
}
