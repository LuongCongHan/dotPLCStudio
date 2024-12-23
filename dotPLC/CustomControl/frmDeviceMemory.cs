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
    public partial class frmDeviceMemory : Form
    {
        public bool isMainFormHidden=false;
        public Form mainForm;
        public frmDeviceMemory()
        {
            InitializeComponent();
            AddDataListView("Mitsubishi FX5U - Binary Mode (Ethernet)");
           // this.Load += FrmDeviceMemory_Load;
            this.KeyDown += FrmOptions_KeyDown;
            this.KeyPreview = true;
        }

        //private void FrmDeviceMemory_Load(object sender, EventArgs e)
        //{
        //    //Định vị trung tâm form chính, tại vì bình thường lỗi
        //    if (!isMainFormHidden)
        //        Location = new Point(mainForm.Location.X + mainForm.Width / 2 - Width / 2,
        //            mainForm.Location.Y + mainForm.Height / 2 - Height / 2);
        //    else
        //    {
        //        this.StartPosition = FormStartPosition.CenterScreen;
        //    }
        //}

        private void FrmOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Close();
        }

        private void AddDataListView(string device)
        {
            lswAddress.Items.Clear();
            lswAddress.FullRowSelect = true;
            lswAddress.GridLines = true;
            lswAddress.View = View.Details;
            switch (device)
            {
                case "Mitsubishi FX5U - Binary Mode (Ethernet)":
                    lswAddress.Columns.Add("Address type").Width = 100;
                    lswAddress.Columns.Add("Bit/Word").Width = 70;
                    lswAddress.Columns.Add("Address Format").Width = 100;
                    lswAddress.Columns.Add("Max. address").Width = 70;
                    lswAddress.Columns.Add("Min. address").Width = 70;
                    // lswAddress.Columns.Add("Shared from").Width = 70;
                    //  lswAddress.Columns.Add("Description").Width = 300;

                    //Word
                    ListViewItem item = new ListViewItem("LCN");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("LZ");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("D");
                    item.SubItems.Add("1");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SD");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDDD");
                    item.SubItems.Add("11999");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("D");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("7999");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("R");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDDD");
                    item.SubItems.Add("32767");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("W");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("HHHH");
                    item.SubItems.Add("7FFF");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("TN");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SN");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("CN");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SW");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("HHHH");
                    item.SubItems.Add("7FFF");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("Z");
                    item.SubItems.Add("Word");
                    item.SubItems.Add("DD");
                    item.SubItems.Add("19");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    //Bit

                    item = new ListViewItem("LCS");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("LCC");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SM");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("9999");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("X");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("OOOO");
                    item.SubItems.Add("1777");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("Y");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("OOOO");
                    item.SubItems.Add("1777");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("M");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDDD");
                    item.SubItems.Add("32767");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("L");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDDD");
                    item.SubItems.Add("32767");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("F");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDDD");
                    item.SubItems.Add("32767");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("B");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("HHHH");
                    item.SubItems.Add("7FFF");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("TS");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("TS");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("TC");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SS");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SC");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("CS");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("CC");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("1023");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("SB");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("HHHH");
                    item.SubItems.Add("7FFF");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    item = new ListViewItem("S");
                    item.SubItems.Add("Bit");
                    item.SubItems.Add("DDDD");
                    item.SubItems.Add("4095");
                    item.SubItems.Add("0");
                    lswAddress.Items.Add(item);

                    break;
            }
        }
    }
}
