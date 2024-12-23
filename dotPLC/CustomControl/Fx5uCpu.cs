using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace dotPLC.CustomControl
{
    public partial class Fx5uCpu : UserControl
    {
        private PictureBox[] picX = new PictureBox[16];
        private PictureBox[] picY = new PictureBox[16];
        private bool[] Input = new bool[16];
        private bool[] Output = new bool[16];

        public bool[] X
        {
            get
            {
                for (int i = 0; i < 16; i++)
                {
                    Input[i] = picX[i].Visible;
                }
                return Input;
            }
            set
            {
                Input = value;
                for (int i = 0; i < 16; i++)
                {
                    picX[i].Visible = value[i];
                }
            }
        }
        public bool[] Y
        {
            get
            {
                for (int i = 0; i < 16; i++)
                {
                    Output[i] = picY[i].Visible;
                }
                return Output;
            }
            set
            {
                Output = value;
                for (int i = 0; i < 16; i++)
                {
                    picY[i].Visible = value[i];
                }
            }
        }

        private PictureBox pic_lan;
        private PictureBox[] pic_MODE = new PictureBox[4];

        Bitmap bmp_Led_On;
        Bitmap bmp_Mode_On;
        Bitmap bmp_Mode_Err;

        Size szLed = new Size(9, 9);
        Size szLedMode = new Size(18, 9);

        public bool mDoubleBuffered
        {
            get { return DoubleBuffered; }
            set
            {
                if (DoubleBuffered != value)
                {
                    DoubleBuffered = value;
                }
            }
        }

        public Fx5uCpu()
        {
            InitializeComponent();
            // DoubleBuffered = true;
            bmp_Led_On = new Bitmap(9, 9);
            bmp_Mode_On = new Bitmap(18, 9);
            bmp_Mode_Err = new Bitmap(18, 9);
            Color ledOn = Color.FromArgb(151, 212, 52);


            using (LinearGradientBrush lin_LedMode = new LinearGradientBrush(new Rectangle(0, 0, 7, 7), ledOn, Color.LightGreen, LinearGradientMode.BackwardDiagonal))
            using (Graphics g = Graphics.FromImage(bmp_Led_On))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.FillEllipse(lin_LedMode, 0f, 0f, 7, 7);
                bmp_Led_On.MakeTransparent();
            }

            pic_lan = new PictureBox();
            pic_lan.Size = szLed;
            pic_lan.Image = bmp_Led_On;
            pic_lan.BackColor = Color.Transparent;
            pic_lan.Location = new Point(200, 137);
            this.Controls.Add(pic_lan);

            for (int i = 0; i < picX.Length; i++)
            {
                picX[i] = new PictureBox();
                picX[i].Size = szLed;
                picX[i].Image = bmp_Led_On;
                picX[i].BackColor = Color.Transparent;
                if (i < 8)
                {
                    picX[i].Location = new Point(383 + i * 11, 81);
                }
                else
                {
                    picX[i].Location = new Point(383 + (i - 8) * 11, 97);
                }
                picX[i].Visible = false;
                this.Controls.Add(picX[i]);
            }
            for (int i = 0; i < picY.Length; i++)
            {
                picY[i] = new PictureBox();
                picY[i].Size = szLed;
                picY[i].Image = bmp_Led_On;
                picY[i].BackColor = Color.Transparent;
                if (i < 8)
                {
                    picY[i].Location = new Point(383 + i * 11, 217);
                }
                else
                {
                    picY[i].Location = new Point(383 + (i - 8) * 11, 233);
                }
                picY[i].Visible = false;
                this.Controls.Add(picY[i]);
            }

            Rectangle rect = new Rectangle(0, 0, 13, 6);

            using (LinearGradientBrush lin_LedMode = new LinearGradientBrush(new Rectangle(0, 0, 18, 9), ledOn, Color.LightGreen, LinearGradientMode.BackwardDiagonal))
            using (Graphics gMode = Graphics.FromImage(bmp_Mode_On))
            {
                gMode.SmoothingMode = SmoothingMode.HighQuality;
                FillRoundedRectangle(gMode, lin_LedMode, rect, 3);
            }

            using (LinearGradientBrush lin_LedMode_Err = new LinearGradientBrush(new Rectangle(0, 0, 18, 9), Color.OrangeRed, Color.Red, LinearGradientMode.BackwardDiagonal))
            using (Graphics gMode = Graphics.FromImage(bmp_Mode_Err))
            {
                gMode.SmoothingMode = SmoothingMode.HighQuality;
                FillRoundedRectangle(gMode, lin_LedMode_Err, rect, 3);
            }

            for (int i = 0; i < pic_MODE.Length; i++)
            {
                pic_MODE[i] = new PictureBox();
                pic_MODE[i].Size = szLedMode;
                if (i == 1)
                {
                    pic_MODE[i].Image = bmp_Mode_Err;

                }
                else
                {
                    pic_MODE[i].Image = bmp_Mode_On;
                }
                pic_MODE[i].BackColor = Color.Transparent;
                if (i == 3)
                    pic_MODE[i].Location = new Point(383, 151 + 27);
                else
                {
                    pic_MODE[i].Location = new Point(383, 138 + i * 13);
                }

                pic_MODE[i].Visible = false;
                this.Controls.Add(pic_MODE[i]);
            }
        }

        public bool LAN
        {
            get
            {
                return pic_lan.Visible;
            }
            set
            {
                if (pic_lan.Visible != value)
                {
                    pic_lan.Visible = value;
                }
            }
        }

        public bool PWR
        {
            get
            {
                return pic_MODE[0].Visible;
            }
            set
            {
                if (pic_MODE[0].Visible != value)
                {
                    pic_MODE[0].Visible = value;
                }
            }
        }

        public bool ERR
        {
            get
            {
                return pic_MODE[1].Visible;
            }
            set
            {
                if (pic_MODE[1].Visible != value)
                {
                    pic_MODE[1].Visible = value;
                }
            }
        }

        public bool RUN
        {
            get
            {
                return pic_MODE[2].Visible;
            }
            set
            {
                if (pic_MODE[2].Visible != value)
                {
                    pic_MODE[2].Visible = value;
                }
            }
        }

        public bool BAT
        {
            get
            {
                return pic_MODE[3].Visible;
            }
            set
            {
                if (pic_MODE[3].Visible != value)
                {
                    pic_MODE[3].Visible = value;
                }
            }
        }

        private static int ConvertOctalToDecimal(int oct)
        {
            var dec = 0;
            var i = 0;
            while (oct > 0)
            {
                dec += oct % 10 * (int)Math.Pow(8, i);
                oct /= 10;
                i++;
            }
            return dec;

        }

        public void SetInput(int index, bool x)
        {
            int temp = ConvertOctalToDecimal(index);
            if (picX[temp].Visible != x)
            {
                picX[temp].Visible = x;
            }
        }

        public void SetOutput(int index, bool y)
        {
            int temp = ConvertOctalToDecimal(index);
            if (picY[temp].Visible != y)
            {
                picY[temp].Visible = y;
            }
        }

        /// <summary>
        /// Fills a rounded rectangle specified by a bounding <see cref="Rectangle"/> and a common corner radius value for each corners.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> instance to draw on.</param>
        /// <param name="brush">The <see cref="Brush"/> instance to be used for the drawing.</param>
        /// <param name="bounds">A <see cref="Rectangle"/> that bounds the rounded rectangle.</param>
        /// <param name="cornerRadius">Size of the corner radius for each corners.</param>
        public static void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics), new ArgumentNullException());
            if (brush == null)
                throw new ArgumentNullException(nameof(brush), new ArgumentNullException());

            using (GraphicsPath path = CreateRoundedRectangle(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }
        /// <summary>
        /// Returns the path for a rounded rectangle specified by a bounding <see cref="Rectangle"/> structure and a common corner radius value for each corners.
        /// </summary>
        /// <param name="bounds">A <see cref="Rectangle"/> structure that bounds the rounded rectangle.</param>
        /// <param name="radius">Size of the corner radius for each corners.</param>
        private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);

            // top left arc
            path.AddArc(arc, 180, 90);

            // top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

    }
}
