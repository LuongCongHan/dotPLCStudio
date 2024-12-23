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
    public partial class frmAbout : Form
    {
        StringBuilder strName = new StringBuilder();
        public frmAbout()
        {
            InitializeComponent();
            this.MaximizeBox = false;//Ngăn không cho phóng to form khi nhấn đúp vào thanh điêu đề ( title bar)
            strName.Clear();
            strName.Append(this.ProductName);
            strName.Append(" V");
            strName.Append(this.ProductVersion);
            lblName.Text = strName.ToString();
            KeyPreview = true;
            this.KeyDown += FrmAbout_KeyDown;
            lblinkyoutube1.LinkClicked += Lblinkyoutube1_LinkClicked;
            lblinkGit.LinkClicked += LblinkGit_LinkClicked;
            lblinkBlog.LinkClicked += LblinkBlog_LinkClicked;
            txtEmail.SelectionStart= txtEmail.Text.Length;
            this.btnOK.Click += BtnOK_Click;
        }

        private void LblinkGit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Diagnostics.Process.Start(lblinkGit.Text);
            }
        }

        private void LblinkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Diagnostics.Process.Start(lblinkBlog.Text);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lblinkyoutube1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                System.Diagnostics.Process.Start(lblinkyoutube1.Text);
            }
        }

        private void FrmAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Close();
        }
    }
}
