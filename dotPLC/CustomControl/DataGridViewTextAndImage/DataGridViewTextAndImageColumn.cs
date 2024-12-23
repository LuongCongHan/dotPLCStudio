using System.Drawing;
using System.Windows.Forms;

namespace dotPLC.CustomControl.DataGridViewTextAndImage
{
    public class DataGridViewTextBoxImageColumn : DataGridViewTextBoxColumn
    {
        private Image imageValue;
        private Size imageSize;

        public DataGridViewTextBoxImageColumn()
        {
            this.CellTemplate = new DataGridViewTextBoxImageCell();
        }

        public override object Clone()
        {
            DataGridViewTextBoxImageColumn c = base.Clone() as DataGridViewTextBoxImageColumn;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get { return this.imageValue; }
            set
            {
                if (this.Image != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                     inheritedPadding.Top, inheritedPadding.Right,
                     inheritedPadding.Bottom);
                    }
                }
            }
        }
        private DataGridViewTextBoxImageCell TextBoxImageCellTemplate
        {
            get { return this.CellTemplate as DataGridViewTextBoxImageCell; }
        }
        internal Size ImageSize
        {
            get { return imageSize; }
        }
    }
}
