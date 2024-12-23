using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dotPLC.CustomControl.ControlExtenders;
using dotPLC.Settings;
using Newtonsoft.Json;

namespace dotPLC.CustomControl
{
    public partial class frmOptions : Form
    {
        public Dictionary<Form, int> _DsChildFormShowTemp;
        public string _localAppData;
        public string _pathSetting;
        public Setting _settingJson;
        public frmMain mainForm;
        internal Floaty _floaty;
        public frmOptions()
        {
            InitializeComponent();
            this.MaximizeBox = false;//Ngăn không cho phóng to form khi nhấn đúp vào thanh điêu đề ( title bar)
            this.Load += FrmOptions_Load;
            btnOK.Click += BtnOK_Click;
            this.KeyDown += FrmOptions_KeyDown;
            this.KeyPreview = true;
            btnCancel.Click += BtnCancel_Click;
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            var toggles = groupBoxOptions.Controls.OfType<ToggleButton>().OrderBy(x => x.TabIndex).ToArray();
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].Checked = _settingJson.StatusOptions[i];
            }
        }

        private bool CreateJson(Setting setting, string path)
        {
            string json = JsonConvert.SerializeObject(setting, Formatting.Indented);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, json); //Tạo file mới, nếu có sẽ ghi đè
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var toggles = groupBoxOptions.Controls.OfType<ToggleButton>().OrderBy(x => x.TabIndex).ToArray();
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].Checked = _settingJson.StatusOptions[i];
            }
            this.Close();
        }

        private void FrmOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var toggles = groupBoxOptions.Controls.OfType<ToggleButton>().OrderBy(x => x.TabIndex).ToArray();
                for (int i = 0; i < toggles.Length; i++)
                {
                    _settingJson.StatusOptions[i] = toggles[i].Checked;
                }
                mainForm.noticeTssl.Enabled = _settingJson.StatusOptions[1]; //notice
                mainForm.trayTssl.Enabled = _settingJson.StatusOptions[2];//System tray
                mainForm.saveWatchTssl.Enabled = _settingJson.StatusOptions[3]; //Save watch
                mainForm.notifyIconPLC.Visible = _settingJson.StatusOptions[2];
                if (mainForm.TopMost != _settingJson.StatusOptions[0]) //Khi Topmost thay đổi làm thay đổi thứ tự form con
                {
                    _DsChildFormShowTemp = new Dictionary<Form, int>(mainForm.DicChildFormShow); //Copy chứ không tham chiếu
                    _floaty.TopMost = _settingJson.StatusOptions[0];
                    mainForm.TopMost = _settingJson.StatusOptions[0];
                    //_floaty.TopMost = false;
                }
                CreateJson(_settingJson, _pathSetting);//Tạo file Setting Json khi chưa được tạo, nếu có rồi thì thôi
                if (_settingJson.StatusOptions[4])
                    mainForm.timerCheckUpdateNotifyIcon.Start();
                this.Close();
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            var toggles = groupBoxOptions.Controls.OfType<ToggleButton>().OrderBy(x => x.TabIndex).ToArray();
            for (int i = 0; i < toggles.Length; i++)
            {
                _settingJson.StatusOptions[i] = toggles[i].Checked;
            }
            mainForm.noticeTssl.Enabled = _settingJson.StatusOptions[1]; //notice
            mainForm.trayTssl.Enabled = _settingJson.StatusOptions[2];//System tray
            mainForm.saveWatchTssl.Enabled = _settingJson.StatusOptions[3]; //Save watch
            mainForm.notifyIconPLC.Visible = _settingJson.StatusOptions[2];
            if (mainForm.TopMost != _settingJson.StatusOptions[0]) //Khi Topmost thay đổi làm thay đổi thứ tự form con
            {
                _DsChildFormShowTemp = new Dictionary<Form, int>(mainForm.DicChildFormShow); //Copy chứ không tham chiếu
                _floaty.TopMost = _settingJson.StatusOptions[0];
                mainForm.TopMost = _settingJson.StatusOptions[0];
                //_floaty.TopMost = false;
            }
            CreateJson(_settingJson, _pathSetting);//Tạo file Setting Json khi chưa được tạo, nếu có rồi thì thôi
            if (_settingJson.StatusOptions[4])
                mainForm.timerCheckUpdateNotifyIcon.Start();
            this.Close();
        }
    }
}
