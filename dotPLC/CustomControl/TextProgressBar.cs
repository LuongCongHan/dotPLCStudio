using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotPLC.CustomControl
{
    public enum ProgressBarDisplayMode
    {
        NoText,
        Percentage,
        CurrProgress,
        CustomText,
        TextAndPercentage,
        TextAndCurrProgress
    }

    public class TextProgressBar : ProgressBar
    {
        [Description("Font of the text on ProgressBar"), Category("Additional Options")]
        public Font TextFont { get; set; } = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

        private SolidBrush _textColourBrush = (SolidBrush)Brushes.Black;
        [Category("Additional Options")]
        public Color TextColor
        {
            get
            {
                return _textColourBrush.Color;
            }
            set
            {
                _textColourBrush.Dispose();
                _textColourBrush = new SolidBrush(value);
            }
        }

        private SolidBrush _progressColourBrush = (SolidBrush)Brushes.LightGreen;
        [Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ProgressColor
        {
            get
            {
                return _progressColourBrush.Color;
            }
            set
            {
                _progressColourBrush.Dispose();
                _progressColourBrush = new SolidBrush(value);
            }
        }

        private ProgressBarDisplayMode _visualMode = ProgressBarDisplayMode.CurrProgress;
        [Category("Additional Options"), Browsable(true)]
        public ProgressBarDisplayMode VisualMode
        {
            get
            {
                return _visualMode;
            }
            set
            {
                _visualMode = value;
                Invalidate();//redraw component after change value from VS Properties section
            }
        }

        [Category("Additional Options"), Browsable(true)]
        public bool GradientMode
        {
            get; set;
        }

        private LinearGradientBrush _linearGradient;
        [Category("Additional Options"), Browsable(true)]
        public Color GradientTopColor { get; set; } = Color.FromArgb(99, 174, 233);
        [Category("Additional Options"), Browsable(true)]
        public Color GradientBotColor { get; set; } = Color.FromArgb(123, 195, 253);
        private string _text = string.Empty;

        [Description("If it's empty, % will be shown"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string CustomText
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                Invalidate();//redraw component after change value from VS Properties section
            }
        }

        private string _textToDraw
        {
            get
            {
                string text = CustomText;

                switch (VisualMode)
                {
                    case (ProgressBarDisplayMode.Percentage):
                        text = _percentageIntStr;
                        break;
                    case (ProgressBarDisplayMode.CurrProgress):
                        text = _currProgressStr;
                        break;
                    case (ProgressBarDisplayMode.TextAndCurrProgress):
                        text = $"{CustomText}: {_currProgressStr}";
                        break;
                    case (ProgressBarDisplayMode.TextAndPercentage):
                        text = $"{CustomText}: {_percentageIntStr}";
                        break;
                }

                return text;
            }
            set { }
        }

        private string _percentageStr { get => $"{(int)((float)Value - Minimum) / ((float)Maximum - Minimum) * 100 } %"; }
        private string _percentageIntStr { get => $"{(uint)(((float)Value - Minimum) / ((float)Maximum - Minimum) * 100) } %"; }

        private string _currProgressStr
        {
            get
            {
                return $"{Value}/{Maximum}";
            }
        }

        public TextProgressBar()
        {
            Value = Minimum;
            FixComponentBlinking();
        }

        private void FixComponentBlinking()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawProgressBar(g);

            DrawStringIfNeeded(g);
        }

        private void DrawProgressBar(Graphics g)
        {
            Rectangle rect = ClientRectangle;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);

            rect.Inflate(-1, -1);

            if (Value > 0)
            {
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);

                if (GradientMode)
                {
                    _linearGradient = new LinearGradientBrush(rect, GradientTopColor,
                        GradientBotColor, LinearGradientMode.Vertical);
                    g.FillRectangle(_linearGradient, clip);
                }
                else
                    g.FillRectangle(_progressColourBrush, clip);
            }
        }

        private void DrawStringIfNeeded(Graphics g)
        {
            if (VisualMode != ProgressBarDisplayMode.NoText)
            {

                string text = _textToDraw;

                SizeF len = g.MeasureString(text, TextFont);

                PointF location = new PointF((Width / 2) - (int)len.Width / 2, ((Height / 2) - (int)len.Height / 2));

                g.DrawString(text, TextFont, _textColourBrush, location);
            }
        }

        public new void Dispose()
        {
            _textColourBrush.Dispose();
            _progressColourBrush.Dispose();
            _linearGradient.Dispose();
            base.Dispose();
        }
    }
}
