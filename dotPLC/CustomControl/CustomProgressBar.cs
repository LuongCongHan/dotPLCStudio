using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotPLC.CustomControl
{
    public class CustomProgressBar : ProgressBar
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;

            // Draw the background (optional)
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);

            // Fill the progress area with a custom color (e.g., red)
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(Brushes.Black, 2, 2, rec.Width, rec.Height);
        }
    }
}
