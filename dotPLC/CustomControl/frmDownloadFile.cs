using dotPLC.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotPLC.CustomControl
{
    public partial class frmDownloadFile : Form
    {
        //
        public bool isStartNewUpdate = false;
        public UpdateVersion _updateVersion;
        public FileInfo _fileInfo;
        int progressbarValue = 0;
        long _lengthTemp = 0;
        long _currentIndexLength = 0;
        bool isStopRefresh = false;
        MethodInvoker methodInvokerProgressBar;
        string _downloadZipPath;
        string pathFolder = string.Empty;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();


        public frmDownloadFile()
        {
            InitializeComponent();
            //label1.MaximumSize = new Size(this.Size.Width - label1.Location.X - 30, 0);
            label1.TextAlign = ContentAlignment.MiddleLeft;

            textProgressBar1.Location = new Point((this.Size.Width / 2) - (textProgressBar1.Size.Width / 2) - 8, textProgressBar1.Location.Y);

            this.MaximizeBox = false;//Ngăn không cho phóng to form khi nhấn đúp vào thanh điêu đề ( title bar)
            methodInvokerProgressBar = new MethodInvoker(() => textProgressBar1.Value = progressbarValue);
            btnCancel.Select();
            textProgressBar1.Maximum = int.MaxValue;
            btnCancel.Click += BtnCancel_Click;
            this.Load += FrmDownloadFile_Load;
            this.FormClosing += FrmDownloadFile_FormClosing;
            this.timerCheckInternet.Tick += TimerCheckInternet_Tick;
        }

        private void TimerCheckInternet_Tick(object sender, EventArgs e)
        {
            if (_checkLengthTemp != _lengthTemp) //ok k lỗi- dữ liệu cập nhật liên tục
            {
                _checkLengthTemp = _lengthTemp; // cập nhật lại dữ liệu
            }
            else if (!CheckForInternetConnection(500, "http://www.google.com")) //Nếu dữ liệu cập nhật không đổi thì check internet thử luôn
            {
                timerCheckInternet.Stop();
                if (streamTest != null)
                    streamTest.Dispose(); //đóng thằng này làm dừng chương trình
            }

        }

        private void FrmDownloadFile_FormClosing(object sender, FormClosingEventArgs e)
        { //Chưa giải quyết được khi nhấn cancel hoặc nút x thì hàm AutoRefreshAsync vẫn còn hoạt động 
            isClickCancelOrClose = true;
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Cancel();
            //if (!CheckForInternetConnection(1000, "http://www.google.com") && streamTest != null)
            if (streamTest != null)
                streamTest.Dispose(); //Không cần thằng này vì đã đặt tcs.SetResult(Sentinel); rồi
            if (fileStream != null)
                fileStream.Dispose();

            isStopRefresh = true; //Khi nhấn huỷ thì báo cho AutoRefreshAsync đóng lại
                                  //   this.Close(); // Không gọi nó ở đây mà đợi các task khác đóng lại hết đã rồi mới Close ở vị trí khác
                                  //Đợi các task đang thực hiện- Nói cách khác là huỷ các task khác thì ct tự động đóng
                                  //test
            if (threadExtect != null)
            {
                //khoa = 1;
                threadExtect.Abort(); //Phải huỷ Thread thì mới được - Vì Thread này chưa huỷ do - streamTest.CopyTo(fileStream);
                //chưa đóng lại khi nhấn cancel

                if (tcs != null && tcs.Task.Status != TaskStatus.RanToCompletion)
                    try { tcs.SetResult(Sentinel); } catch { }
                //threadExtect = null;
            }

        }


        long _checkLengthTemp = -1;
        bool isClickCancelOrClose = false;
        private async void FrmDownloadFile_Load(object sender, EventArgs e)
        {
            textProgressBar1.Visible = true;
            _checkLengthTemp = -1;
            isClickCancelOrClose = false;

            if (_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();//Phải tạo mới nếu như lần trước đó huỷ thì khi mở lại thì các task không hoàn thành
            isStopRefresh = false; //Đặt lại giá trị cho hàm Autofresh
            isStartNewUpdate = false; //Thông báo là đang cập nhật phiên bản

            //Căn giữa 
            string txt_temp = "Downloading...";
            Size textSize = TextRenderer.MeasureText(txt_temp, label1.Font);
            int textWidth = textSize.Width;
            label1.Location = new Point(((this.Size.Width - textWidth) / 2) - 8, label1.Location.Y);
            label1.Text = txt_temp;


            textProgressBar1.Value = 0;
            _currentIndexLength = 0;
            Guid guid = GetGuid(Assembly.GetExecutingAssembly());
            string path = Path.Combine(Path.GetTempPath(), "{" + guid.ToString().ToUpper() + "}");
            //Đợi download xong
            bool isDownloaded = await DownloadNewVersion(path);

            _currentIndexLength = 0; //Đặt lại số tệp tin trong file

            if (isDownloaded)
            {
                isStopRefresh = false;
                //Giải nén file
                _toTalLengthUnzip = 0;//Đặt lại dung lượng tổng trước khi extract
                await Task.Delay(500);// Delay 0.5s rồi tiến hành Extract

                //Căn giữa 
                txt_temp = "Uncompressing..."; ;
                textSize = TextRenderer.MeasureText(txt_temp, label1.Font);
                textWidth = textSize.Width;
                label1.Location = new Point(((this.Size.Width - textWidth) / 2) - 8, label1.Location.Y);
                label1.Text = txt_temp;

                //Extract
                bool isExtracted = await ExtractAsync(_cancellationTokenSource); //Trong này đã kèm quá trình thanh tiến trình
                if (isExtracted)
                {

                    await Task.Delay(100); //Đợi tý rồi kích hoạt
                    var exeFiles = Directory.EnumerateFiles(pathFolder, _updateVersion.FileExtension, SearchOption.TopDirectoryOnly).ToArray();
                    if (exeFiles != null && exeFiles.Length > 0 &&
                       Path.GetFileNameWithoutExtension(exeFiles[0]) == _updateVersion.FileName && !isClickCancelOrClose)
                    {

                        Process.Start(exeFiles[0]);
                        isStartNewUpdate = true; //Bắt đầu chạy version mới
                        this.Close();
                    }
                    else
                    {
                        //Không tìm thấy tệp tin để cài hoặc danh sách rỗng
                        //MessageBox.Show("The new version file was not found.", "Install");
                        label1.Location = lbPointErr;
                        label1.MaximumSize = new Size(this.Size.Width - label1.Location.X - 30, 0);
                        label1.Text = "An error occurred while preparing for installation. Please try again later.";
                        textProgressBar1.Visible = false;
                    }
                }
                else //Nếu giải nén thất bại
                {
                    label1.Location = lbPointErr;
                    label1.MaximumSize = new Size(this.Size.Width - label1.Location.X - 30, 0);
                    label1.Text = "An error occurred while extracting the file. Please try again later.";
                    //lbPercent.Location = new Point((this.ClientSize.Width - label1.Width) / 2, label1.Height);
                    textProgressBar1.Visible = false;
                    //this.Close();
                }
            }
            else if (!isClickCancelOrClose) //Nếu nhấn đóng form đang tải thì nhảy luôn vào đây- phải giải quyết ở đây
            {
                //label1.Text = "The installation failed.";
                label1.Location = lbPointErr;
                label1.MaximumSize = new Size(this.Size.Width - label1.Location.X - 30, 0);
                label1.Text = "An error occurred in retrieving update information; are you connected to the internet? Please try again later.";
                textProgressBar1.Visible = false;
            }
        }
        Point lbPointErr = new Point(21, 21);
        Stream streamTest;
        FileStream fileStream;
        public long? _toTalLengthUnzip = 0;
        private async Task<bool> DownloadNewVersion(string path)
        {
            try
            {
                Directory.CreateDirectory(path);  // Tạo thư mục temp/{4542C1F4-61C7-4C0D-A6EC-E805411B8184}
                // Đặt timeout sau 5s k tải được do mất mạng thì xuất ra lỗi
                //Nếu không nó cứ đứng yên suốt
                //Nếu sau 10s từ lúc tắt mạng trước khi bật tải lên thì bắt được lỗi tại "catch (TaskCanceledException taskEx)" 
                using (HttpClient _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(2) })
                    streamTest = await _httpClient.GetStreamAsync(_updateVersion.UrlDownload);

                //Tạo đường link thư mục  temp/{4542C1F4-61C7-4C0D-A6EC-E805411B8184}/download.zip
                _downloadZipPath = Path.Combine(path, Path.GetFileName(_updateVersion.UrlDownload));
                fileStream = File.Create(_downloadZipPath); //Tạo file download.zip

                tcs = new TaskCompletionSource<object>();
                _fileInfo = new FileInfo(_downloadZipPath);
                //File đã được tạo ở trên nên không lỗi được ở AutoRefresh => File.Create(_downloadZipPath))
                Task taskRefreshFile = AutoRefreshAsync(_fileInfo, _toTalLengthUnzip); //Bắt đầu đọc dung lượng hiện tại luôn ở đây
                                                                                       // await streamTest.CopyToAsync(fileStream, 4096, cancellationTokenSource.Token);

                threadExtect = new Thread(() =>
                {
                    try
                    {
                        streamTest.CopyTo(fileStream); //Try catch ở đây để khi lỗi do Inner- abort thread
                    }
                    catch //Lỗi do DNS chẳng hạn- khi bật tắt ứng dụng 1.1.1.1 trong quá trình tải thì đều bị
                    {
                        timerCheckInternet.Stop();
                    }
                    finally
                    {
                        if (streamTest != null)
                            streamTest.Dispose(); //Không cần thằng này vì đã đặt tcs.SetResult(Sentinel); rồi
                        if (fileStream != null)
                            fileStream.Dispose();

                        if (tcs != null && tcs.Task.Status != TaskStatus.RanToCompletion)
                            tcs.SetResult(Sentinel);
                        isStopRefresh = true; //Đóng cái await Task.WhenAny(taskRefreshFile); lại luôn
                    }

                });
                threadExtect.Start();

                timerCheckInternet.Start();
                await Task.WhenAny(tcs.Task);

                await Task.WhenAny(taskRefreshFile);
                timerCheckInternet.Stop();
                if (_lengthTemp == _toTalLengthUnzip)
                    return true; //2 thằng bắt lỗi ở trên nhưng ở đây vẫn trả về TRUE-------------------phải xem lại
                else
                    return false;
            }
            catch
            {
                timerCheckInternet.Stop();
                return false;
            }
        }

        Thread threadExtect;
        TaskCompletionSource<object> tcs;
        private static readonly object Sentinel = new object();
        private async Task<bool> ExtractAsync(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                using (ZipArchive zip = await Task.Run(() => ZipFile.OpenRead(_downloadZipPath), cancellationTokenSource.Token))
                {
                    //Tạo  thư mục theo tên filezip  temp/{4542C1F4-61C7-4C0D-A6EC-E805411B8184}/download
                    pathFolder = Path.Combine(Path.GetDirectoryName(_downloadZipPath),
                        Path.GetFileNameWithoutExtension(_downloadZipPath));
                    Directory.CreateDirectory(pathFolder);

                    //Tính dung lượng lưu trữ của các file bên trong file zip (khi unzip xong)
                    foreach (ZipArchiveEntry item in zip.Entries)
                    {
                        _toTalLengthUnzip += item.Length;
                    }
                    long? lengthTemp = 0;
                    foreach (ZipArchiveEntry item in zip.Entries)
                    {
                        tcs = new TaskCompletionSource<object>();
                        Task taskRefreshFile;
                        string path = Path.Combine(pathFolder, item.Name); //Tạo đường dẫn cho từng item  temp/{4542C1F4-61C7-4C0D-A6EC-E805411B8184}/download/item1.abc
                        _fileInfo = new FileInfo(path); //Tạo file info để theo dõi item1
                        lengthTemp += item.Length; //Dung lượng cộng dồn từng item
                        try
                        {
                            //Tạo file luôn để khỏi lỗi được ở AutoRefresh (hoặc dùng khối using) và dispose nó luôn
                            File.Create(path).Dispose(); //Tạo file luôn để  lỗi được ở AutoRefresh (hoặc dùng khối using)
                            taskRefreshFile = AutoRefreshAsync(_fileInfo, lengthTemp);  //Bắt đầu ghi vào thanh tiến trình

                            //Không thể huỷ ExtractToFile khi nó đang làm việc được=> đợi nó chạy xong mới huỷ được
                            //Bắt đầu giải nén từng item
                            //Task taskExtract = Task.Run(() => item.ExtractToFile(path, true), cancellationTokenSource.Token);
                            threadExtect = new Thread(() =>
                            {
                                item.ExtractToFile(path, true);
                                tcs.SetResult(Sentinel);
                            });
                            threadExtect.Start();

                            await tcs.Task;

                            //Đợi ghi xong thanh extract- Thằng này đang chạy ở trên rồi
                            await Task.WhenAny(taskRefreshFile); //Đợi giải nén xong

                            _currentIndexLength += item.Length;

                            //Khi nhấn huỷ
                            if (isClickCancelOrClose) return false;
                        }

                        catch
                        {
                            // MessageBox.Show(ex.Message);
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task AutoRefreshAsync(FileInfo file, long? value)
        {
            //await Task.Delay(10); //Đợi file được tạo ra rồi mới đọc
            await Task.Run((Action)(() =>
            {
                while (true)
                {
                    try
                    {
                        file.Refresh(); //Làm mới file
                        _lengthTemp = _fileInfo.Length + _currentIndexLength;
                        progressbarValue = (int)(_lengthTemp * int.MaxValue / this._toTalLengthUnzip);
                        // Object name: 'frmDownloadAndExtract'.'
                        if (progressbarValue < 0)
                        {
                            break;
                        }
                        this.Invoke(methodInvokerProgressBar); //Xuất thông báo ra Progressbar - Lỗi ở đây khi nhấn nút tắt cửa sổ
                                                               //System.ObjectDisposedException: 'Cannot access a disposed object.
                        if (_lengthTemp == value || isStopRefresh)
                            break;
                    }
                    catch
                    {
                        break; //Nếu lỗi do nhấn cancel- hay đóng form download thì lỗi  
                        //System.ObjectDisposedException: 'Cannot access a disposed object. sẽ xuất hiện thì ta thoát ra
                        //chứ không là nó mắc kẹt không thoát được mặc dù đã bật isStopRefresh=true
                    }
                }
            }));
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Guid GetGuid(Assembly assembly)
        {
            var guidAttribute = (GuidAttribute)assembly?.GetCustomAttributes(typeof(GuidAttribute), false).SingleOrDefault();
            if (Guid.TryParse(guidAttribute?.Value, out Guid guid)) { return guid; }
            return Guid.Empty;
        }

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
                if (cultureInfo.Name.StartsWith("fa")) // Iran
                    url = "http://www.aparat.com";
                else if (cultureInfo.Name.StartsWith("zh"))  // China
                    url = "http://www.baidu.com";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
