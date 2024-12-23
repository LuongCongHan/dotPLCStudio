using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotPLC.CustomControl
{
    public partial class frmListClients : Form
    {
        public frmListClients()
        {
            InitializeComponent();
            this.MaximizeBox = false;//Ngăn không cho phóng to form khi nhấn đúp vào thanh điêu đề ( title bar)
            KeyPreview = true;
            this.KeyDown += FrmListClients_KeyDown;
        }

        private void FrmListClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Close();
        }
    }
}
