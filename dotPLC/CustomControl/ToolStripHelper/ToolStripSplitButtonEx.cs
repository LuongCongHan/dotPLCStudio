using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotPLC.CustomControl.ToolStripHelper
{
    class ToolStripSplitButtonEx : ToolStripSplitButton
    {
        protected override void OnMouseHover(EventArgs e)
        {
            if (this.Parent != null)
                Parent.Focus();
            base.OnMouseHover(e);
        }
    }
}
