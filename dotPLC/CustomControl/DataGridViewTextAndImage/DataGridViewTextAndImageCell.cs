using System.Drawing;
using System.Windows.Forms;

namespace dotPLC.CustomControl.DataGridViewTextAndImage
{
    public class DataGridViewTextBoxImageCell : DataGridViewTextBoxCell
    {
        private Image imageValue;
        private Size imageSize;
        public int PointImageX { get; set; } = 0;
        public override object Clone()
        {
            DataGridViewTextBoxImageCell c = base.Clone() as DataGridViewTextBoxImageCell;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get
            {
                if (this.OwningColumn == null ||
            this.OwningTextBoxImageColumn == null)
                {

                    return imageValue;
                }
                else if (this.imageValue != null)
                {
                    return this.imageValue;
                }
                else
                {
                    return this.OwningTextBoxImageColumn.Image;
                }
            }
            set
            {
                if (this.imageValue != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;
                    Padding inheritedPadding = this.InheritedStyle.Padding;
                    // imageSize.Width + PointImageX khoảng cách giữa Text và Ảnh
                    this.Style.Padding = new Padding(imageSize.Width + PointImageX,
                    inheritedPadding.Top, inheritedPadding.Right,
                    inheritedPadding.Bottom);
                }
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
        Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
        object value, object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            if (this.Image != null)
            {
                //Khoảng cách giữa Lề trái và ảnh
                cellBounds.X = PointImageX;

                // Draw the image clipped to the cell.
                System.Drawing.Drawing2D.GraphicsContainer container =
                graphics.BeginContainer();
                graphics.SetClip(cellBounds);
                graphics.DrawImageUnscaled(this.Image, cellBounds.Location);

                graphics.EndContainer(container);
            }
        }

        private DataGridViewTextBoxImageColumn OwningTextBoxImageColumn
        {
            get { return this.OwningColumn as DataGridViewTextBoxImageColumn; }
        }
    }
}
