using dotPLC.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace dotPLC.CustomControl
{
    public partial class frmUpdateVersion : Form
    {
        public UpdateVersion _updateVersion = new UpdateVersion();
        public bool enableCheckUpdateByClick = false;
        public long? _totalDownloadSize = 0;
        public frmUpdateVersion()
        {
            InitializeComponent();
          //  _httpClient.Timeout = TimeSpan.FromMilliseconds(5000);
            this.MaximizeBox = false;//Ngăn không cho phóng to form khi nhấn đúp vào thanh điêu đề ( title bar)
            //  VisibleLoading(false, true, false, false);
            //Tự động xuống dòng khi đến giới hạn form
            lbLinkUpdate.MaximumSize = new Size(this.Size.Width - lbLinkUpdate.Location.X - 30, 0);
            lbLinkUpdate.AutoSize = true;
            this.Load += FrmUpdateVersion_Load;
            this.btnCancel.Click += BtnCancel_Click;
            timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            // VisibleLoading(false, true,false, false, false);
            if (!enableCheckUpdateByClick)
            {
                string info = this.ProductName;
                //Convert Bytes to Megabytes (Hệ nhị phân)
                lblCopyright.Text = "A new version of dotPLC Studio is available.";
                lbLinkUpdate.Text = string.Format("{0} is now available. Would you like to download it now?", info);
                lbLinkUpdate.LinkArea = new LinkArea(0, info.Length);
                lbLinkUpdate.LinkClicked += LbLinkUpdate_LinkClicked;
                // lbSize.Text = string.Format("{0} MB", mb.ToString("F2"));
                lbVersion.Text = _updateVersion.Version;
               // VisibleLoading(true, false, false, true, true, false);
                try
                {
                    using (HttpResponseMessage headResponse = await _httpClient.GetAsync(_updateVersion.UrlDownload, HttpCompletionOption.ResponseHeadersRead))
                    {
                        _totalDownloadSize = headResponse.Content.Headers.ContentLength;
                    }
                }
                catch
                {
                    await Task.Delay(500);
                    picLoad.Visible = false;
                    lblCopyright.Visible = true;
                    btnUpdate.Visible = false;
                    lblCopyright.Text = "Failed to download version information.";
                    lbSize.Text = "Failed to load.";
                    lbSize.Visible = true;
                    picSizeLoad.Visible = false;
                    return;
                }
                double mb = (double)(_totalDownloadSize / Math.Pow(2, 20));
                lbSize.Text = string.Format("{0} MB", mb.ToString("F2"));
                btnUpdate.Visible = true;
                picSizeLoad.Visible = false;
                lbSize.Visible = true;
                btnUpdate.Focus();
                return;
            }
            try
            {
                // VisibleLoading(false, true, true, false, false, false);
                bool isUpdate = await CheckUpdateAsync();
                if (isUpdate)
                {
                    
                    string info = this.ProductName;
                    //Convert Bytes to Megabytes (Hệ nhị phân)
                    lblCopyright.Text = "A new version of dotPLC Studio is available.";
                    lbLinkUpdate.Text = string.Format("{0} is now available. Would you like to download it now?", info);
                    lbLinkUpdate.LinkArea = new LinkArea(0, info.Length);
                    lbLinkUpdate.LinkClicked += LbLinkUpdate_LinkClicked;
                    // lbSize.Text = string.Format("{0} MB", mb.ToString("F2"));
                    lbVersion.Text = _updateVersion.Version;
                    // VisibleLoading(true, false, false, true, true, false);

                    VisibleLoading(true, false, false, true, true, false);
                    //Lấy kích thước file download (Đây là nguyên nhân gây ra sự tăng bộ nhớ lên đến 430.4MB)
                    //using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, _updateVersion.Url))
                    //using (HttpResponseMessage headResponse = await _httpClient.SendAsync(httpRequestMessage))
                    //{
                    //    var data = httpRequestMessage.Content;
                    //    _totalDownloadSize = headResponse.Content.Headers.ContentLength;
                    //}

                    // Giải quyết vấn đề bộ nhớ tăng
                    // HttpCompletionOption.ResponseContentRead Cũng sẽ làm tăng bộ nhớ lên vì đọc cả content 480.2MB
                    // using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, _updateVersion.Url))
                    using (HttpResponseMessage headResponse = await _httpClient.GetAsync(_updateVersion.UrlDownload, HttpCompletionOption.ResponseHeadersRead))
                    {
                        _totalDownloadSize = headResponse.Content.Headers.ContentLength;
                    }
                    //Giải quyết vấn đề bộ nhớ tăng
                    double mb = (double)(_totalDownloadSize / Math.Pow(2, 20));
                    lbSize.Text = string.Format("{0} MB", mb.ToString("F2"));
                    btnUpdate.Visible = true;
                    picSizeLoad.Visible = false;
                    lbSize.Visible = true;
                    btnUpdate.Focus();
                    //NewVersion();
                }
                else
                {
                    picLoad.Visible = false;
                    lblCopyright.Visible = true;
                    btnUpdate.Visible = false;
                    lblCopyright.Text = "There is no new version available.";
                }
            }
            catch
            {
                await Task.Delay(500);
                picLoad.Visible = false;
                lblCopyright.Visible = true;
                btnUpdate.Visible = false;
                lblCopyright.Text = "Failed to download version information.";
            }
        }

        public void NewVersion()
        {
            VisibleLoading(true, true, false, false, true, true);
            btnUpdate.Focus();
            string info = this.ProductName;
            //Convert Bytes to Megabytes (Hệ nhị phân)
            double mb = (double)(_totalDownloadSize / Math.Pow(2, 20));
            lblCopyright.Text = "A new version of dotPLC Studio is available.";
            lbLinkUpdate.Text = string.Format("{0} is now available. Would you like to download it now?", info);
            lbLinkUpdate.LinkArea = new LinkArea(0, info.Length);
            lbLinkUpdate.LinkClicked += LbLinkUpdate_LinkClicked;
            lbSize.Text = string.Format("{0} MB", mb.ToString("F2"));
            lbVersion.Text = _updateVersion.Version;
        }


        private void VisibleLoading(bool value, bool lblsize, bool pic, bool picSize, bool lbl, bool update)
        {
            lbLinkUpdate.Visible = value;
            lbVersion.Visible = value;
            lbSize.Visible = lblsize;
            label1.Visible = value;
            label3.Visible = value;
            picLoad.Visible = pic;
            lblCopyright.Visible = lbl;
            btnUpdate.Visible = update;
            picSizeLoad.Visible = picSize;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Timer timer = new Timer() { Interval = 1 };
        private void FrmUpdateVersion_Load(object sender, EventArgs e)
        {
            if (!enableCheckUpdateByClick)
                VisibleLoading(true, false, false, true, true, false);
            else
            {
                 VisibleLoading(false, false, true, false, false, false);
               // VisibleLoading(true, false, false, true, true, false);
            }
            timer.Enabled = true;
        }

        private void LbLinkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("down");
        }
        HttpClient _httpClient = new HttpClient();
        public async Task<bool> CheckUpdateAsync()
        {
            string updateJson = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/LuongCongHan/UpdateMyApp/refs/heads/master/UpdateMyApp/update.json");
            //Chuyển đổi Version về dạng dữ liệu đối tượng
            _updateVersion = JsonConvert.DeserializeObject<UpdateVersion>(updateJson);
          //  _updateVersion.Url = "https://github.com/LuongCongHan/MyAppUpdate/releases/download/File/AllFile.zip";
            // _updateVersion.Url = "https://github.com/LuongCongHan/TestUpdateUngDung/releases/download/newVd/LenhDk.zip";
            var currentVersion = new Version(this.ProductVersion);
            var jsonVersion = new Version(_updateVersion.Version);
            //So sánh Version
            int result = currentVersion.CompareTo(jsonVersion); //Icomparea
            if (result < 0)
                return true;
            else
                return false;
        }

    }
}
