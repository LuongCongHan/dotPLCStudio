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
    public partial class frmNetworkSetting : Form
    {
        public frmNetworkSetting()
        {
            InitializeComponent();
            this.MaximizeBox = false;//Ngăn không cho phóng to form khi nhấn đúp vào thanh điêu đề ( title bar)
            btnCancel.Click += BtnCancel_Click;

            txtIP.Text = Properties.Settings.Default.ipaddress;
            txtPort.Text = Properties.Settings.Default.port.ToString();
            chkboxAnyAddress.Checked = Properties.Settings.Default.anyaddress;
            txtIP.Enabled =!Properties.Settings.Default.anyaddress;
            chkboxAnyAddress.CheckedChanged += ChkboxAnyAddress_CheckedChanged;
            btnOK.Click += BtnOK_Click;
            this.KeyPreview = true;
            this.KeyDown += FrmNetworkSetting_KeyDown;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNetworkSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Properties.Settings.Default.ipaddress= txtIP.Text ;
                int port;
                if (int.TryParse(txtPort.Text, out port))
                    Properties.Settings.Default.port = port;
                else
                { MessageBox.Show("The port number is invalid.", "Network Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                Properties.Settings.Default.anyaddress = chkboxAnyAddress.Checked;
                Properties.Settings.Default.Save();
                this.Close();
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ipaddress = txtIP.Text;
            int port;
            if (int.TryParse(txtPort.Text, out port))
                Properties.Settings.Default.port = port;
            else
            { MessageBox.Show("The port number is invalid.", "Network Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            Properties.Settings.Default.anyaddress = chkboxAnyAddress.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void ChkboxAnyAddress_CheckedChanged(object sender, EventArgs e)
        {
            txtIP.Enabled = !chkboxAnyAddress.Checked;
        }
    }
}
