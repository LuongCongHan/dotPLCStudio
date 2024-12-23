using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using dotPLC.CustomControl.DataGridViewTextAndImage;
using dotPLC.SocketServer;
using dotPLC.CustomControl;
using System.Net;
using System.Drawing.Drawing2D;
using System.Media;
using dotPLC.CustomControl.ControlExtenders;
using dotPLC.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;

namespace dotPLC
{
    public partial class frmMain : Form
    {
        private Bitmap bmpDevice = Properties.Resources.edit;
        private Bitmap bmpNon = new Bitmap(16, 16);
        private Bitmap bmpNon_temp = new Bitmap(16, 16);
        private bool IsMonitor = false;
        bool isblockrow = false;
        List<int> dsIndexs = new List<int>();
        private IPAddress ipAddress;
        string[] cpuNames = new string[]{
"FX5U-32MR/ES",
"FX5U-64MR/ES",
"FX5U-80MR/ES",
"FX5U-32MT/ES",
"FX5U-64MT/ES",
"FX5U-80MT/ES",
"FX5U-32MT/ESS",
"FX5U-64MT/ESS",
"FX5U-80MT/ESS",
"FX5U-32MR/DS",
"FX5U-64MR/DS",
"FX5U-80MR/DS",
"FX5U-32MT/DS",
"FX5U-64MT/DS",
"FX5U-80MT/DS",
"FX5U-32MT/DSS",
"FX5U-64MT/DSS",
"FX5U-80MT/DSS",
"FX5UC-32MT/D",
"FX5UC-64MT/D",
"FX5UC-96MT/D",
"FX5UC-32MT/DSS",
"FX5UC-64MT/DSS",
"FX5UC-96MT/DSS",
"FX5UC-32MR/DS-TS",
"FX5UC-32MT/DS-TS",
"FX5UC-32MT/DSS-TS",
"FX5UJ-24MR/ES",
"FX5UJ-40MR/ES",
"FX5UJ-60MR/ES",
"FX5UJ-24MT/ES",
"FX5UJ-40MT/ES",
"FX5UJ-60MT/ES",
"FX5UJ-24MT/ESS",
"FX5UJ-40MT/ESS",
"FX5UJ-60MT/ESS",
"FX5S-30MR/ES",
"FX5S-40MR/ES",
"FX5S-60MR/ES",
"FX5S-80MR/ES",
"FX5S-30MT/ES",
"FX5S-40MT/ES",
"FX5S-60MT/ES",
"FX5S-80MT/ESS",
"FX5S-30MT/ESS",
"FX5S-40MT/ESS",
"FX5S-60MT/ESS",
"FX5S-80MT/ESS"
};
        UpdateVersion updateVersion = new UpdateVersion();
        private List<DvgWatch> lsDvgWatch = new List<DvgWatch>();
        private Setting settingJson = new Setting()
        { IPAddess = "127.0.0.1", Port = 502, AnyAddress = true, CpuName = "FX5U-32MT/ES" };
        private int port;
        bool[] IO = new bool[16];
        char splitTab = '\t';
        char[] splitNewLine = new char[] { '\r', '\n' };
        string strEmptyRow = "\t\t\t";
        Dictionary<string, Tuple<System.Windows.Forms.Timer, bool>> DsTimers2 = new Dictionary<string, Tuple<System.Windows.Forms.Timer, bool>>();
        SLMPServer PLC = new SLMPServer();
        static readonly Regex sWhitespace = new Regex(@"\s+");
        internal static int ConvertOctalToDecimal(int oct)
        {
            var dec = 0;
            var i = 0;
            while (oct > 0)
            {
                dec += oct % 10 * (int)Math.Pow(8, i);
                oct /= 10;
                i++;
            }
            return dec;

        }
        private bool SettingDevice(string label, out string device, out int num)
        {
            bool isDevice = false;
            //   label = sWhitespace.Replace(label, "").ToUpper();
            if (label.Length < 2)
            {
                device = label;
                num = 0;
                return false;
            }
            if (label[0] == 'S' && label[1] == 'B')
            {
                label = label.Substring(2);
                device = "SB";
                //SB
                isDevice = int.TryParse(label, System.Globalization.NumberStyles.HexNumber, null, out num);
                return isDevice && IsSizeMatch(device, num);
            }
            else if (label[0] == 'S' && label[1] == 'W')
            {
                label = label.Substring(2);
                device = "SW";
                //SW
                isDevice = int.TryParse(label, System.Globalization.NumberStyles.HexNumber, null, out num);
                return isDevice && IsSizeMatch(device, num);

            }
            else if (label[0] == 'B' && label[1] != 'L')
            {
                label = label.Substring(1);
                device = "B";
                //B
                isDevice = int.TryParse(label, System.Globalization.NumberStyles.HexNumber, null, out num);
                return isDevice && IsSizeMatch(device, num);

            }
            else if (label[0] == 'W')
            {
                label = label.Substring(1);
                device = "W";
                //W
                isDevice = int.TryParse(label, System.Globalization.NumberStyles.HexNumber, null, out num);
                return isDevice && IsSizeMatch(device, num);

            }
            else if (label[0] == 'X')
            {
                label = label.Substring(1);
                device = "X";
                //X
                isDevice = int.TryParse(label, out num);
                if (isOctal(num))
                {
                    num = ConvertOctalToDecimal(num); // X20 => *0x16
                    return isDevice && IsSizeMatch(device, num);
                }
                else
                {
                    return false;
                }

            }
            else if (label[0] == 'Y')
            {
                label = label.Substring(1);
                device = "Y";
                //Y
                isDevice = int.TryParse(label, out num);
                // X20 => *0x16
                if (isOctal(num))
                {
                    num = ConvertOctalToDecimal(num); // X20 => *0x16
                    return isDevice && IsSizeMatch(device, num);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int index = 0;
                for (int i = 0; i < label.Length; i++)
                {
                    if (label[i] >= 0x30 && label[i] <= 0x39)
                    {
                        index = i;
                        break;
                    }
                }
                device = label.Substring(0, index);
                isDevice = int.TryParse(label.Substring(index), out num); //D100 => 0x0064 
                return isDevice && IsSizeMatch(device, num);
            }
        }
        private bool IsSizeMatch(string device, int num)
        {
            switch (device)
            {
                //Register
                case "D":
                    if (num > 7999) return false;
                    else return true;//Decimal
                case "SW":
                    if (num > 0x7fff) return false;
                    else return true;//Hexadecimal
                case "W":
                    if (num > 0x7fff) return false;
                    else return true; //Hexadecimal
                case "TN":
                    if (num > 1023) return false;
                    else return true; //Timer word //Decimal 
                case "SD":
                    if (num > 11999) return false;
                    else return true; //Decimal
                case "R":
                    if (num > 32767) return false;
                    else return true; // Decimal
                case "Z":
                    if (num > 19) return false;
                    else return true;  // Decimal
                case "LZ":
                    if (num > 1) return false;
                    else return true;  // Decimal
                case "CN":
                    if (num > 1023) return false;
                    else return true; //Counter
                case "LCN":
                    if (num > 1023) return false;
                    else return true;     //Long counter  // Decimal
                case "SN":
                    if (num > 1023) return false;
                    else return true;  // Retentive timer-Timer có nhớ ST0.. //Decimal
                case "STN":
                    if (num > 1023) return false;
                    else return true; // Giống ở trên //Decimal

                //Bit:
                case "X":
                    if (num > 1777) return false;
                    else return true; //OCtal
                case "Y":
                    if (num > 1777) return false;
                    else return true; //OCtal
                case "M":
                    if (num > 32767) return false;
                    else return true; //Decimal
                case "L":
                    if (num > 32767) return false;
                    else return true; //Decimal
                case "F":
                    if (num > 32767) return false;
                    else return true;  //Decimal
                case "B":
                    if (num > 0x7fff) return false; //255
                    else return true; //Hexca
                case "S":
                    if (num > 4095) return false;
                    else return true;   //Step relay //Decimal
                case "SS":
                    if (num > 1023) return false;
                    else return true;  //Retentive timer  ST0 //Decimal Bit
                case "SC":
                    if (num > 1023) return false;
                    else return true;   //Retentive timer ST0 //Decimal Bit
                case "TC":
                    if (num > 1023) return false;
                    else return true;   //Timer T0 (bật) Decimal
                case "TS":
                    if (num > 1023) return false;
                    else return true;  // Timer T0 Decimal
                case "CS":
                    if (num > 1023) return false;
                    else return true;   //Counter  C0  Decimal
                case "CC":
                    if (num > 1023) return false;
                    else return true;   //Counter C0 Decimal
                case "SB":
                    if (num > 0x7fff) return false;
                    else return true;  //Link special relay Hex
                case "SM":
                    if (num > 9999) return false;
                    else return true; //Special relay Decmal
                case "BL":
                    if (num > 31) return false;
                    else return true;   //SFC block device Decimal
                default:
                    return false;
            }
        }
        private bool IsLastWord(string device, int num)
        {
            switch (device)
            {
                //Register
                case "D":
                    if (num == 7999) return true;
                    else return false;//Decimal
                case "SW":
                    if (num == 0x7fff) return true;
                    else return false;//Hexadecimal
                case "W":
                    if (num == 0x7fff) return true;
                    else return false; //Hexadecimal
                case "TN":
                    if (num == 1023) return true;
                    else return false; //Timer word //Decimal 
                case "SD":
                    if (num == 11999) return true;
                    else return false; //Decimal
                case "R":
                    if (num == 32767) return true;
                    else return false; // Decimal
                case "Z":
                    if (num == 19) return true;
                    else return false;  // Decimal
                case "LZ":
                    if (num == 1) return true;
                    else return false;  // Decimal
                case "CN":
                    if (num == 1023) return true;
                    else return false; //Counter
                case "LCN":
                    if (num == 1023) return true;
                    else return false;     //Long counter  // Decimal
                case "SN":
                    if (num == 1023) return true;
                    else return false;  // Retentive timer-Timer có nhớ ST0.. //Decimal
                case "STN":
                    if (num == 1023) return true;
                    else return false; // Giống ở trên //Decimal
                default:
                    return false;
            }
        }
        private bool IsContainsWordDevice(string device)
        {
            switch (device)
            {
                //Register
                case "D":
                    return true;  //Decimal
                case "SW":
                    return true; //Hexadecimal
                case "W":
                    return true; //Hexadecimal
                case "TN":
                    return true; //Timer word //Decimal 
                case "SD":
                    return true; //Decimal
                case "R":
                    return true; // Decimal
                case "Z":
                    return true;  // Decimal
                case "LZ":
                    return true;  // Decimal
                case "CN":
                    return true; //Counter
                case "LCN":
                    return true;     //Long counter  // Decimal
                case "SN":
                    return true;   // Retentive timer-Timer có nhớ ST0.. //Decimal
                case "STN":
                    return true;  // Giống ở trên //Decimal
                default:
                    return false;
            }
        }
        private bool IsContainsBitDevice(string device)
        {
            switch (device)
            {
                //Bit:
                case "X":
                    return true; //OCtal
                case "Y":
                    return true;  //OCtal
                case "M":
                    return true; //Decimal
                case "L":
                    return true;  //Decimal
                case "F":
                    return true;  //Decimal
                case "V":
                    return true;  //Decimal
                case "B":
                    return true;  //Hexca
                case "S":
                    return true;    //Step relay //Decimal
                case "SS":
                    return true;    //Retentive timer  ST0 //Decimal Bit
                case "SC":
                    return true;    //Retentive timer ST0 //Decimal Bit
                case "TC":
                    return true;    //Timer T0 (bật) Decimal
                case "TS":
                    return true;   // Timer T0 Decimal
                case "CS":
                    return true;    //Counter  C0  Decimal
                case "CC":
                    return true;    //Counter C0 Decimal
                case "SB":
                    return true;  //Link special relay Hex
                case "SM":
                    return true; //Special relay Decmal
                case "BL":
                    return true;    //SFC block device Decimal
                default:
                    return false;
            }
        }
        bool isOctal(int n)
        {
            while (n > 0)
            {
                if ((n % 10) >= 8)
                    return false;
                else
                    n = n / 10;
            }
            return true;
        }
        DockExtender dockExtender;
        IFloaty floaty;
        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string pathSetting;
        string pathWatch;
        public frmMain()
        {
            InitializeComponent();
            try
            {
                // Để không bị lỗi: WebException: The request was aborted: Could not create SSL/TLS secure channel.
                ServicePointManager.SecurityProtocol |= (SecurityProtocolType)192 |
                                                        (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            }
            catch (NotSupportedException)
            { }
            //start docking here
            dockExtender = new DockExtender(this);
            floaty = dockExtender.Attach(panelBottom, lblBottom, splitterBottom);
            floaty.Docking += new EventHandler(floaty_Docking);
            btnCloseBottom.Click += BtnCloseBottom_Click;
            lblBottom.VisibleChanged += LblBottom_VisibleChanged;

            this.KeyDown += FrmMain_KeyDown;
            this.KeyPreview = true;

            #region Các nút nhấn Contexmenu ctmnuClipboardDvgPLC
            copyToolStripMenuItemDvgPLC.Click += CopyToolStripMenuItemDvgPLC_Click;
            pastToolStripMenuItemDvgPLC.Click += PastToolStripMenuItemDvgPLC_Click;
            cutToolStripMenuItemDvgPLC.Click += CutToolStripMenuItemDvgPLC_Click;
            deleteToolStripMenuItemDvgPLC.Click += DeleteToolStripMenuItemDvgPLC_Click;
            selectAllToolStripMenuItemDvgPLC.Click += SelectAllToolStripMenuItemDvgPLC_Click;
            networkToolStripButton.Click += NetworkToolStripButton_Click;
            startserverToolStripButton.Click += StartserverToolStripButton_Click;
            stopserverToolStripButton.Click += StopserverToolStripButton_Click;
            deviceMemoryToolStripButton.Click += DeviceMemoryToolStripButton_Click;
            optionToolStripButton.Click += OptionToolStripButton_Click;
            clearToolStripButton.Click += ClearToolStripButton_Click;
            listClientsToolStripButton.Click += ListClientsToolStripButton_Click;
            aboutToolStripButton.Click += AboutToolStripButton_Click;
            watchToolStripButton.Click += WatchToolStripButton_Click;

            ctmnuClipboardDvgPLC.Opened += CtmnuClipboardDvgPLC_Opened;
            dvgPLC.CellMouseDown += DvgPLC_CellMouseDown;
            dvgPLC.CellPainting += DvgPLC_CellPainting;
            dvgPLC.KeyDown += DvgPLC_KeyDown;
            timerStartMonitor.Tick += TimerStartMonitor_Tick;
            timerLAN.Tick += TimerLAN_Tick;
            timerIO.Tick += TimerIO_Tick;
            timerLedRUN.Tick += TimerLedRUN_Tick;
            timerErr.Tick += TimerErr_Tick;
            incrementToolStripMenuItemDvgPLC.Click += IncrementToolStripMenuItemDvgPLC_Click;
            decrementToolStripMenuItemDvgPLC.Click += DecrementToolStripMenuItemDvgPLC_Click;
            stopToolStripMenuItemDvgPLC.Click += StopToolStripMenuItemDvgPLC_Click;
            settimeToolStripMenuItemDvgPLC.DropDownOpening += SettimeToolStripMenuItemDvgPLC_DropDownOpening;
            setTimeToolStripSplitButton.DropDownOpening += SetTimeToolStripSplitButton_DropDownOpening;
            setTimeToolStripSplitButton.Click += SetTimeToolStripSplitButton_Click;
            for (int i = 0; i < cpuNames.Length; i++)
            {
                CpuTypeToolStripComboBox.Items.Add(cpuNames[i]);
            }


            pathSetting = Path.Combine(localAppData, this.CompanyName, this.ProductName, this.ProductVersion, "settings.json");
            pathWatch = Path.Combine(localAppData, this.CompanyName, this.ProductName, this.ProductVersion, "watch.json");
            if (!File.Exists(pathSetting))
                CreateJson(settingJson, pathSetting);//Tạo file Setting Json khi chưa được tạo, nếu có rồi thì thôi
            if (!File.Exists(pathWatch))
                CreateJson(lsDvgWatch, pathWatch);//Tạo file Setting Json khi chưa được tạo, nếu có rồi thì thôi
            using (StreamReader r = new StreamReader(pathSetting))
            {
                string json = r.ReadToEnd();
                settingJson = JsonConvert.DeserializeObject<Setting>(json);
            }
            //CpuTypeToolStripComboBox.Text = Properties.Settings.Default.cpuName;

            CpuTypeToolStripComboBox.Text = settingJson.CpuName;
            dvgPLC.CellBeginEdit += DvgPLC_CellBeginEdit;

            undoToolStripMenuItemTextBox.Click += UndoToolStripMenuItemTextBox_Click;
            cutToolStripMenuItemTextBox.Click += CutToolStripMenuItemTextBox_Click;
            copyToolStripMenuItemTextBox.Click += CopyToolStripMenuItemTextBox_Click;
            pasteToolStripMenuItemTextBox.Click += PasteToolStripMenuItemTextBox_Click;
            deleteToolStripMenuItemTextBox.Click += DeleteToolStripMenuItemTextBox_Click;
            selectAllToolStripMenuItemTextBox.Click += SelectAllToolStripMenuItemTextBox_Click;

            ctmnuTextBox.Opening += CtmnuTextBox_Opening;
            incrementToolStripButton.Click += IncrementToolStripButton_Click;
            decrementToolStripButton.Click += DecrementToolStripButton_Click;
            stopcountToolStripButton.Click += StopcountToolStripButton_Click;

            onBitToolStripButton.Click += OnBitToolStripButton_Click;
            offBitToolStripButton.Click += OffBitToolStripButton_Click;
            toggleBitToolStripButton.Click += ToggleBitToolStripButton_Click;

            cutToolStripButton.Click += CutToolStripButton_Click;
            copyToolStripButton.Click += CopyToolStripButton_Click;
            pasteToolStripButton.Click += PasteToolStripButton_Click;
            deleteToolStripButton.Click += DeleteToolStripButton_Click;
            this.FormClosing += FrmMain_FormClosing;
            PLC.LostConnect += PLC_LostConnection;
            PLC.NewConnect += PLC_NewConnection;
            PLC.ChangeStatus += PLC_ChangeStatus;
            PLC.Error += PLC_Error;

            //Contextmenustrip của Notifyicon
            openToolStripMenuItemNotify.Click += OpenToolStripMenuItemNotify_Click;
            startRuntimeServiceToolStripMenuItemNotify.Click += StartRuntimeServiceToolStripMenuItemNotify_Click;
            stopRuntimeServiceToolStripMenuItemNotify.Click += StopRuntimeServiceToolStripMenuItemNotify_Click;
            aboutToolStripMenuItemNotify.Click += AboutToolStripMenuItemNotify_Click;
            optionsToolStripMenuItemNotify.Click += OptionsToolStripMenuItemNotify_Click;
            exitToolStripMenuItemNotify.Click += ExitToolStripMenuItemNotify_Click;
            notifyIconPLC.MouseDoubleClick += NotifyIconPLC_MouseDoubleClick;
            notifyIconPLC.MouseClick += NotifyIconPLC_MouseClick;
            ctmnuTray.Opening += CtmnuTray_Opening;
            networkConfigurationToolStripMenuItemNotify.Click += NetworkConfigurationToolStripMenuItemNotify_Click;

            this.Resize += FrmMain_Resize;
            ChangeTextNorifyStop();
            listOfClientsToolStripMenuItemNotify.Click += ListOfClientsToolStripMenuItemNotify_Click;
            deviceBufferMemoryToolStripMenuItemNotify.Click += DeviceBufferMemoryToolStripMenuItemNotify_Click;

            //ctmnuCPU
            runToolStripMenuItemFX5U.Click += RunToolStripMenuItemFX5U_Click;
            pauseToolStripMenuItemFX5U.Click += PauseToolStripMenuItemFX5U_Click;
            stopToolStripMenuItemFX5U.Click += StopToolStripMenuItemFX5U_Click;
            resetToolStripMenuItemFX5U.Click += ResetToolStripMenuItemFX5U_Click;
            setErrorToolStripMenuItemFX5U.Click += SetErrorToolStripMenuItemFX5U_Click;

            ctmnuCPU.Opening += CtmnuCPU_Opening;

            SettingStatusStrip(); //Enable các option thanh label bên dưới cùng
            clientsTssl.Visible = false;
            splitTssl.Visible = false;
            statusTssl.Text = "Not running";
            notifyIconPLC.Visible = settingJson.StatusOptions[2];
            this.TopMost = settingJson.StatusOptions[0];
            #endregion Các nút nhấn Contexmenu ctmnuClipboardDvgPLC

            #region DataGridview

            dvgPLC.AllowUserToAddRows = false;
            dvgPLC.RowTemplate.Height = 19;
            dvgPLC.RowTemplate.DefaultCellStyle.Font = new Font("Arial", 9);

            dvgPLC.Rows.Add();
            for (int i = 1; i < 4; i++)
            {
                dvgPLC.Rows[0].Cells[i].ReadOnly = true;
            }
            DataGridViewTextBoxImageCell boxImageCell = new DataGridViewTextBoxImageCell();
            boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, 0];
            boxImageCell.Image = bmpNon;


            using (StreamReader r = new StreamReader(pathWatch))
            {
                string json = r.ReadToEnd();
                lsDvgWatch = JsonConvert.DeserializeObject<List<DvgWatch>>(json);
            }
            PasteWatch(lsDvgWatch); // Paste dữ liệu cho dvgPLC

            //Tắt chức năng sort:
            foreach (DataGridViewColumn column in dvgPLC.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dvgPLC.CellEndEdit += DvgPLC_CellEndEdit;
            dvgPLC.RowEnter += DvgPLC_RowEnter;
            dvgPLC.RowLeave += DvgPLC_RowLeave;
            dvgPLC.EditingControlShowing += DvgPLC_EditingControlShowing;
            dvgPLC.RowsAdded += DvgPLC_RowsAdded;
            dvgPLC.RowsRemoved += DvgPLC_RowsRemoved;
            dvgPLC.UserDeletingRow += DvgPLC_UserDeletingRow;
            dvgPLC.CellEndEdit += DvgPLC_CellEndEditEX;
            dvgPLC.Scroll += DvgPLC_Scroll;

            //Test Timer
            dvgPLC.MouseClick += DvgPLC_MouseClick_Timer;
            ctmnuClipboardDvgPLC.Opening += CtmnuClipboardDvgPLC_Opening;

            //Ngan khong nhập văn bản
            settimetoolStripComboBoxDvgPLC.IntegralHeight = false; //để  MaxDropDownItems thực hiện
            settimetoolStripComboBoxDvgPLC.FlatStyle = FlatStyle.System;
            settimetoolStripComboBoxDvgPLC.DropDownStyle = ComboBoxStyle.DropDownList;
            settimetoolStripComboBoxDvgPLC.MaxDropDownItems = 10;

            settimeToolStripComboBox.IntegralHeight = false; //để  MaxDropDownItems thực hiện
            settimeToolStripComboBox.FlatStyle = FlatStyle.System;
            settimeToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            settimeToolStripComboBox.MaxDropDownItems = 10;
            double timevalue = 0.1d;
            for (int i = 0; i < 50; i++)
            {
                settimetoolStripComboBoxDvgPLC.Items.Add(timevalue.ToString("N1"));
                settimeToolStripComboBox.Items.Add(timevalue.ToString("N1"));
                timevalue += 0.1;
            }
            settimetoolStripComboBoxDvgPLC.Text = "1.0";
            settimeToolStripComboBox.Text = "1.0";
            settimetoolStripComboBoxDvgPLC.SelectedIndexChanged += SettimetoolStripComboBoxDvgPLC_SelectedIndexChanged;
            settimeToolStripComboBox.SelectedIndexChanged += SettimeToolStripComboBox_SelectedIndexChanged;
            #endregion
            //Phần update
            _frmAbout.btnCheckUpdate.Click += BtnCheckUpdate_Click;
            tempTimer.Tick += TempTimer_Tick;
            tempTimerDelay.Tick += TempTimerDelay_Tick;
            // this.Load += FrmMain_Load; //cập nhật phiên bản khi khởi động
            _frmUpdateVersion.btnUpdate.Click += BtnUpdate_Click;

            _floaty = floaty as Floaty;
            _floaty.TopMost = settingJson.StatusOptions[0];
            _frmOptions.mainForm = this;
            _frmOptions._floaty = _floaty;

            lsChildForms = new List<Form>() { _frmAbout , _frmOptions, _frmListClients, _frmDeviceMemory, _frmNetworkSetting };

            foreach (var f in lsChildForms)
            {
                f.FormClosing += frmChild_FormClosing;
                f.Load += frmChild_Load;
                f.Activated += frmChild_Activated;
                f.ResizeBegin += frmChild_ResizeBegin;
                f.ResizeEnd += frmChild_ResizeEnd;
            }

            _floaty.FormClosing += frmChild_FormClosing;
            _floaty.ResizeBegin += frmChild_ResizeBegin;
            _floaty.ResizeEnd += frmChild_ResizeEnd;
            splitterBottom.SplitterMoving += SplitterBottom_SplitterMoving;
            splitterBottom.SplitterMoved += SplitterBottom_SplitterMoved;

            // Nếu di chuột vào các nút ToolStripSplitButtonEx thì các form con sẽ mất forcus   
            //Khi form được mở hay nhấn vào form thì kích hoạt sự kiện giống kiểu -BringToFront
            _floaty.Activated += frmChild_Activated;
       
            DicChildFormShow.Add(_frmAbout, 1);
            DicChildFormShow.Add(_frmOptions, 2);
            DicChildFormShow.Add(_frmListClients, 3);
            DicChildFormShow.Add(_frmDeviceMemory, 4);
            DicChildFormShow.Add(_frmNetworkSetting, 5);
            DicChildFormShow.Add(_floaty, 6);

            //Thông báo có phiên bản mới thông qua notifyicon
            _httpClient.Timeout = TimeSpan.FromMilliseconds(5000);
            timerCheckUpdateNotifyIcon.Tick += TimerCheckUpdateNotifyIcon_Tick;
            timerEnableUpdate.Tick += TimerEnableUpdate_Tick;
            if (settingJson.StatusOptions[4])
                timerCheckUpdateNotifyIcon.Start();
            notifyIconPLC.BalloonTipText = "A new version of dotPLC Studio is available.";
            notifyIconPLC.BalloonTipTitle = "Update Software";

            //Khi đang nhấn vào form hoặc thu phóng form (cả form chính và form con)mà form update mở lên thì nó sẽ đơ 
            // và vị trí, và kích thước lúc đầu vẫn giữ nguyên
            this.ResizeBegin += frmChild_ResizeBegin; //Khi đang nhấn vào form mà form update mở lên thì nó sẽ đơ form update
            this.ResizeEnd += frmChild_ResizeEnd;
            timerShowUpdateFormAfterResize.Tick += TimerShowUpdateFormAfterResize_Tick;
            //Click vào BalloonTip thông báo update của NotifyIcon khi thông báo update
            notifyIconPLC.BalloonTipClicked += NotifyIconPLC_BalloonTipClicked;
        }

        private void NotifyIconPLC_BalloonTipClicked(object sender, EventArgs e)
        {
            openToolStripMenuItemNotify.PerformClick();
        }

        private void SplitterBottom_SplitterMoved(object sender, SplitterEventArgs e)
        {
            IsFormResize = false;
            timerShowUpdateFormAfterResize.Start();
        }

        private void SplitterBottom_SplitterMoving(object sender, SplitterEventArgs e)
        {
            IsFormResize = true;
        }

        private void TimerShowUpdateFormAfterResize_Tick(object sender, EventArgs e)
        {
            timerShowUpdateFormAfterResize.Stop();
            _frmUpdateVersion.StartPosition = FormStartPosition.CenterParent;
            if (IsShowUpdateFormAfterResize)
            {
                IsUpdateFormShow = true;
                _frmUpdateVersion.ShowDialog(this);
                IsUpdateFormShow = false;
            }
            IsShowUpdateFormAfterResize = false;
        }

        private void frmChild_ResizeBegin(object sender, EventArgs e)
        {
            IsFormResize = true;
        }

        Timer timerShowUpdateFormAfterResize = new Timer() { Interval=1};

        private void frmChild_ResizeEnd(object sender, EventArgs e)
        {
            IsFormResize = false;
            timerShowUpdateFormAfterResize.Start();
        }

        bool IsFormResize = false;
        bool IsShowUpdateFormAfterResize = false;
        List<Form> lsChildForms;
        private void TimerEnableUpdate_Tick(object sender, EventArgs e)
        {
            _isEnableUpdate = false;
            timerEnableUpdate.Stop();

            _frmUpdateVersion.StartPosition = FormStartPosition.CenterParent;
            if (!IsDownloadFile&&!IsFormResize)
            {
                IsUpdateFormShow = true;
                _frmUpdateVersion.ShowDialog(this);
                IsUpdateFormShow = false;
            }
        }
        bool _isEnableUpdate = false;
        private async void TimerCheckUpdateNotifyIcon_Tick(object sender, EventArgs e)
        {
            timerCheckUpdateNotifyIcon.Stop();
            try
            {
                bool isUpdate = await _frmUpdateVersion.CheckUpdateAsync();
                if (isUpdate)
                {
                    
                    if (isHiden)
                    {
                        notifyIconPLC.ShowBalloonTip(3000);
                        _isEnableUpdate = true;
                    }
                    else if (this.WindowState == FormWindowState.Minimized)
                    {
                        // Định vị trung tâm form chính, tại vì bình thường lỗi
                        _isEnableUpdate = true;
                    }
                    else
                    {
                        if (IsFormResize)
                        {
                            IsShowUpdateFormAfterResize = true;
                        }
                        else if (!IsDownloadFile)
                        {
                            _frmUpdateVersion.StartPosition = FormStartPosition.CenterParent;
                            IsUpdateFormShow = true;
                            _frmUpdateVersion.ShowDialog(this);
                            IsUpdateFormShow = false;
                        }
                        else
                            _isEnableUpdate = false;
                    }
                }
            }
            catch {}
        }
        public Timer timerCheckUpdateNotifyIcon = new Timer() { Interval = 3000 };
        public Timer timerEnableUpdate = new Timer() { Interval = 1 };
        bool isOptionOk = false;
        private void frmChild_Activated(object sender, EventArgs e)
        {
            // this.Text = (sender as Form).Text;
            Form f = sender as Form;

            //Nếu Active do topmost cuẩ Floaty ở đây
            if (f is Floaty && f.TopMost && isOptionOk && _frmOptions._DsChildFormShowTemp != null)
            {
                if (_frmOptions._DsChildFormShowTemp.FirstOrDefault(x => x.Value == 5).Key is Floaty)
                {
                    isOptionOk = false;
                    return;
                }
                //DsChildFormShow = _frmOptions._DsChildFormShowTemp;
                foreach (var item in DicChildFormShow.ToArray())
                {
                    if (item.Value != 6)
                        DicChildFormShow[item.Key]++;
                }
                DicChildFormShow[_frmOptions] = 1;
                var myKey = DicChildFormShow.FirstOrDefault(x => x.Value == 6).Key;

                if (!myKey.Visible)
                {
                    this.BringToFront();
                }
                else
                {
                    myKey.BringToFront();
                }
                // f.TopMost = false;
                isOptionOk = false;

                return;
            }

            if (DicChildFormShow.ContainsKey(f))
            {
                foreach (var item in DicChildFormShow.ToArray())
                {
                    if (item.Value > DicChildFormShow[f])
                        DicChildFormShow[item.Key]--;
                }
                DicChildFormShow[f] = 6;
            }
            if (f is frmOptions && !f.Visible)
                isOptionOk = false;
        }
        Floaty _floaty;

        public Dictionary<Form, int> DicChildFormShow = new Dictionary<Form, int>();

        private bool BeforeStartForm(Form form)
        {
            if (form.Visible)
            {
                form.BringToFront();
                return false;
            }
            if (isHiden || this.WindowState == FormWindowState.Minimized) form.StartPosition = FormStartPosition.CenterScreen;
            return true;
        }

        private void frmChild_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();
            Form form = sender as Form;
            Form mainForm = form.Owner;
            //Định vị trung tâm form chính, tại vì bình thường lỗi
            if (!isHiden)
                form.Location = new Point(mainForm.Location.X + mainForm.Width / 2 - form.Width / 2,
                    mainForm.Location.Y + mainForm.Height / 2 - form.Height / 2);
            else
            {
                form.StartPosition = FormStartPosition.CenterScreen;
            }
            this.ResumeLayout(false);
        }

        private void frmChild_FormClosing(object sender, FormClosingEventArgs e)
        {

            //settingJson.StatusOptions[2]
            if (!IsAltF4)
                e.Cancel = !(e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing
                    || (e.CloseReason == CloseReason.FormOwnerClosing & !settingJson.StatusOptions[2])); //Khi nhấn exit từ tray thì cho phép đóng form con trước
            else
                e.Cancel = false;
            Form f = sender as Form;
            if (f is frmOptions)
            { //Nếu cài đặt xong thì xác nhận--Nhưng mà cái này làm form cuối cùng đóng lại k đúng mà đóng luôn form chính
              //Đưa nó vào sự kiện FormClosed của _frmOptions
              //Nếu Active do topmost cuẩ Floaty ở đây
              //if (_floaty.TopMost)
              //{
              //    DsChildFormShow = _frmOptions._DsChildFormShowTemp;
              //}
                isOptionOk = true;
            }
            f.Hide();

            foreach (var item in DicChildFormShow.ToArray())
            {
                if (item.Value != 6)
                    DicChildFormShow[item.Key]++;
            }
            DicChildFormShow[f] = 1;
            var myKey = DicChildFormShow.FirstOrDefault(x => x.Value == 6).Key;

            if (!myKey.Visible)
            {
                this.BringToFront();
            }
            else
            {
                myKey.BringToFront();
            }

            // if (!f.Visible)
            //{
            // var myKey = DsChildFormShow.FirstOrDefault(x => x.Value == 4).Key;
            //    myKey.BringToFront();
            // }

            //if(DsChildFormShow[f]!=5)
            //{
            //    foreach (var item in DsChildFormShow.ToArray())
            //    {
            //        if (item.Value > DsChildFormShow[f])
            //            DsChildFormShow[item.Key]--;
            //    }
            //    DsChildFormShow[f] = 5;
            //}

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            if (this.WindowState == FormWindowState.Minimized && isHiden)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = _formWindowState;
                this.ShowIcon = true;
                this.ShowInTaskbar = true;
                isHiden = false;
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            this.Activate();//Đưa ứng dụng lên trên hết

        }
       
        private async void FrmMain_Load(object sender, EventArgs e)
        {
            // this.Enabled = false;
            if (!settingJson.StatusOptions[4]) return;//Nếu không autocheck update thì thoát

            //Kiểm tra nếu có form con đang mở thì thoát
            //if (Application.OpenForms.Cast<Form>().Where(form => form != this).Any(form => form.Visible))
            //{
            //    return;
            //}
            _frmUpdateVersion.enableCheckUpdateByClick = false;
            try
            {
                this.Enabled = false;
                bool isUpdate = await _frmUpdateVersion.CheckUpdateAsync();
                this.Enabled = true;
                // this.Enabled = true;
                if (isUpdate)
                {
                    //using (HttpResponseMessage headResponse = await _httpClient.GetAsync(_frmUpdateVersion._updateVersion.Url, HttpCompletionOption.ResponseHeadersRead))
                    //{
                    //    _frmUpdateVersion._totalDownloadSize = headResponse.Content.Headers.ContentLength;
                    //}
                    // _frmUpdateVersion.NewVersion();
                    if (isHiden || this.WindowState == FormWindowState.Minimized) _frmUpdateVersion.StartPosition = FormStartPosition.CenterScreen;
                    else _frmUpdateVersion.StartPosition = FormStartPosition.CenterParent;
                    _frmUpdateVersion.ShowDialog(this);
                }
            }
            catch { }

        }

        HttpClient _httpClient = new HttpClient();
        public long? _totalDownloadSize = 0;

        bool IsAltF4 = false;
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
            {
                IsAltF4 = true;
            }
        }

        public Guid GetGuid(Assembly assembly)
        {
            var guidAttribute = (GuidAttribute)assembly?.GetCustomAttributes(typeof(GuidAttribute), false).SingleOrDefault();
            if (Guid.TryParse(guidAttribute?.Value, out Guid guid)) { return guid; }
            return Guid.Empty;
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

        private bool CreateJson(List<DvgWatch> dvgWatches, string path)
        {
            string json = JsonConvert.SerializeObject(dvgWatches, Formatting.Indented);
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

        private void WatchToolStripButton_Click(object sender, EventArgs e)
        {
            floaty.Show();
        }

        private void LblBottom_VisibleChanged(object sender, EventArgs e)
        {
            //smart handle, will show/hide the x-button accoordingly
            btnCloseBottom.Visible = lblBottom.Visible;
            // if(lblBottom.Visible) (floaty as Floaty).BringToFront();
        }

        private void BtnCloseBottom_Click(object sender, EventArgs e)
        {
            dockExtender.Hide(panelBottom);
        }

        // make sure the ZOrder of the other main (non-dockable) controls remain
        void floaty_Docking(object sender, EventArgs e)
        {
            panelMain.BringToFront();
            //Nếu dock floaty vào thì các form con sẽ không còn đúng nữa
            foreach (var item in DicChildFormShow.ToArray())
            {
                if (item.Value != 6)
                    DicChildFormShow[item.Key]++;
            }
            DicChildFormShow[_floaty] = 1;
            var myKey = DicChildFormShow.FirstOrDefault(x => x.Value == 6).Key;

            if (!myKey.Visible)
            {
                this.BringToFront();
            }
            else
            {
                myKey.BringToFront();
            }
        }
        private void CtmnuCPU_Opening(object sender, CancelEventArgs e)
        {
            if (timerErr.Enabled) setErrorToolStripMenuItemFX5U.Text = "Clear Error";
            else setErrorToolStripMenuItemFX5U.Text = "Set Error";
        }

        frmAbout _frmAbout = new frmAbout();
        private void AboutToolStripButton_Click(object sender, EventArgs e)
        {
            if (BeforeStartForm(_frmAbout))
            {
                _frmAbout.Show(this);
                _frmAbout.BringToFront();
            }
        }
        frmUpdateVersion _frmUpdateVersion = new frmUpdateVersion();
        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            _isEnableUpdate = false;
           // if (isHiden) _frmUpdateVersion.StartPosition = FormStartPosition.CenterScreen;
           // else _frmUpdateVersion.StartPosition = FormStartPosition.CenterParent;
            _frmUpdateVersion.StartPosition = FormStartPosition.CenterParent;
            _frmUpdateVersion.enableCheckUpdateByClick = true;
            IsUpdateFormShow = true;
            _frmUpdateVersion.ShowDialog(_frmAbout); //Khi đến đây thì bộ nhớ tăng từ 38.67MB lên 430.4MB-Đã giải quyết xong
            IsUpdateFormShow = false;
        }

        frmDownloadFile _frmDownloadFile = new frmDownloadFile();
        Timer tempTimer = new Timer();
        Timer tempTimerDelay = new Timer() { Interval = 200 };
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            _frmUpdateVersion.Close();
            tempTimer.Start();

        }

        private void TempTimer_Tick(object sender, EventArgs e)
        {
            tempTimer.Stop();
            tempTimerDelay.Start();
        }
        bool IsDownloadFile = false;
        bool IsUpdateFormShow = false;
        private void TempTimerDelay_Tick(object sender, EventArgs e)
        {
            tempTimerDelay.Stop();
            if (IsMonitor) stopserverToolStripButton.PerformClick(); //Nếu đang monitor thì dừng lại chờ download xong đã
            _frmDownloadFile._updateVersion = _frmUpdateVersion._updateVersion;
            _frmDownloadFile._toTalLengthUnzip = _frmUpdateVersion._totalDownloadSize;
            ctmnuTray.Enabled = false;
            IsDownloadFile = true;
            if (_frmUpdateVersion.enableCheckUpdateByClick)
            {
                _frmDownloadFile.StartPosition = FormStartPosition.CenterParent;
                _frmDownloadFile.ShowDialog(_frmAbout);
            }
            else if (this.WindowState == FormWindowState.Minimized || isHiden)
            {
                _frmDownloadFile.StartPosition = FormStartPosition.CenterScreen;
                _frmDownloadFile.ShowDialog(this);
            }
            else

            {
                _frmDownloadFile.StartPosition = FormStartPosition.CenterParent;
                _frmDownloadFile.ShowDialog(this);
            }
            IsDownloadFile = false;
            ctmnuTray.Enabled = true;
            if (_frmDownloadFile.isStartNewUpdate)
            {
                _frmAbout.Close();
                enableClose = true;
                this.Close();
            }
        }

        private void DeviceBufferMemoryToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            deviceMemoryToolStripButton.PerformClick();
        }

        private void ListOfClientsToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            if (!BeforeStartForm(_frmListClients)) return;
            // listClientsToolStripButton.PerformClick(); Để khỏi có viền xanh trên thanh công cụ
            stringBuilderConnect.Clear();
            foreach (var item in PLC.ListClients)
            {
                stringBuilderConnect.AppendLine(item.Client.RemoteEndPoint.ToString());
            }
            _frmListClients.Text = "List of connected clients: #" + PLC.ListClients.Count;
            _frmListClients.txtListClients.Text = stringBuilderConnect.ToString().TrimEnd('\n');
            _frmListClients.txtListClients.SelectionStart = _frmListClients.txtListClients.Text.Length;
            // TrayEnabled(false);
            _frmListClients.Show(this);
            //TrayEnabled(true);
        }

        private void NetworkConfigurationToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            networkToolStripButton.PerformClick();
        }

        private void CtmnuTray_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = IsDownloadFile||IsUpdateFormShow;
        }

        private void NotifyIconPLC_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsDownloadFile)
                {
                    _frmDownloadFile.Activate();
                    _frmDownloadFile.WindowState = FormWindowState.Normal;//Show lại _frmDownloadFile nếu không khi nhấn thu nhỏ rồi không mở lại được
                }
                else if (IsUpdateFormShow)
                {
                    _frmUpdateVersion.Activate();
                }
                
            }
        }

        private void NotifyIconPLC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsDownloadFile)
                {
                   // _frmDownloadFile.Activate();
                   // _frmDownloadFile.WindowState = FormWindowState.Normal;//Show lại _frmDownloadFile nếu không khi nhấn thu nhỏ rồi không mở lại được
                }
                else if(IsUpdateFormShow)
                {
                  //  _frmUpdateVersion.Activate();
                }
                else
                {
                    openToolStripMenuItemNotify.PerformClick();
                }
            }
        }

        FormWindowState _formWindowState;
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                _formWindowState = FormWindowState.Normal;
                if (_isEnableUpdate)
                {
                    timerEnableUpdate.Start();
                }
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                _formWindowState = FormWindowState.Maximized;
                if (_isEnableUpdate)
                {
                    timerEnableUpdate.Start();
                }
            }
        }

        bool enableClose = false;
        private void ExitToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            this.enableClose = true;
            this.Close();
        }

        private void OptionsToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            optionToolStripButton.PerformClick();
        }

        private void AboutToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            aboutToolStripButton.PerformClick();
        }

        private void StopRuntimeServiceToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            StopserverToolStripButton_Click(sender, e);
        }

        private void StartRuntimeServiceToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            StartserverToolStripButton_Click(sender, e);
        }

        private void OpenToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            if (_frmUpdateVersion.Visible || _frmDownloadFile.Visible) //Khi cửa sổ tự động update hiện lên không cho bật formmain từ tray nữa
                return;

            #region  Mở lại form main và form con mà form main sẽ nằm phía sau 
            //this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.WindowState = _formWindowState;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ShowIcon = true; //Để ở cuối mới hiện icon lên
            isHiden = false;
            this.Activate();
            if (_floaty.Visible)
                _floaty.BringToFront();
            #endregion
            //
            //this.Activate();//Đưa ứng dụng lên trên hết
        }

        private void SetTimeToolStripSplitButton_Click(object sender, EventArgs e)
        {
            setTimeToolStripSplitButton.ShowDropDown();
        }

        StringBuilder stringBuilderConnect = new StringBuilder();
        frmListClients _frmListClients = new frmListClients();
        private void ListClientsToolStripButton_Click(object sender, EventArgs e)
        {
            if (_frmListClients.Visible) return;
            stringBuilderConnect.Clear();
            foreach (var item in PLC.ListClients)
            {
                stringBuilderConnect.AppendLine(item.Client.RemoteEndPoint.ToString());
            }
            if (isHiden || this.WindowState == FormWindowState.Minimized) _frmListClients.StartPosition = FormStartPosition.CenterScreen;
            else _frmListClients.StartPosition = FormStartPosition.CenterParent;
            _frmListClients.Text = "List of connected clients: #" + PLC.ListClients.Count;
            _frmListClients.txtListClients.Text = stringBuilderConnect.ToString().TrimEnd('\n');
            _frmListClients.txtListClients.SelectionStart = _frmListClients.txtListClients.Text.Length;
            // TrayEnabled(false);
            _frmListClients.Show(this);
            _frmListClients.BringToFront();
            //TrayEnabled(true);
        }

        bool isHiden = false;
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                //Đóng ứng dụng do TaskManager hoặc do đóng window
                enableClose = true;
            }
            if (!enableClose && settingJson.StatusOptions[2] && !IsAltF4)
            {
                this.SuspendLayout();
                var forms = Application.
                  OpenForms.Cast<Form>().
                  Where(form => form != this && !(form is frmAlert) && !(form is Floaty) && !(form is Overlay));
                foreach (var item in forms.ToArray())
                {
                    item.Close();
                }
                this.WindowState = FormWindowState.Minimized;
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                e.Cancel = true;
                isHiden = true;
                notifyIconPLC.Visible = true;
                this.ResumeLayout(false);
                return;
            }
            if (IsAltF4)
            {
                var forms = Application.
                   OpenForms.Cast<Form>().
                   Where(form => form != this && !(form is frmAlert) && !(form is Floaty) && !(form is Overlay));
                foreach (var item in forms.ToArray())
                {
                    item.Close();
                }
            }
            if (IsMonitor) PLC.Stop();
            if (settingJson.StatusOptions[3])
                try
                {
                    //SaveDvg();
                    SaveDvgWatch();
                }
                catch { }
            notifyIconPLC.Visible = false;
            settingJson.CpuName = CpuTypeToolStripComboBox.Text;
            //Properties.Settings.Default.Save();
            CreateJson(settingJson, pathSetting);//Tạo file Setting Json khi chưa được tạo, nếu có rồi thì thôi
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                return Params;
            }
        }

        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            DeleteToolStripMenuItemDvgPLC_Click(sender, e);
        }

        private void PasteToolStripButton_Click(object sender, EventArgs e)
        {
            PastToolStripMenuItemDvgPLC_Click(sender, e);
        }

        private void CopyToolStripButton_Click(object sender, EventArgs e)
        {
            CopyToolStripMenuItemDvgPLC_Click(sender, e);
        }

        private void CutToolStripButton_Click(object sender, EventArgs e)
        {
            CutToolStripMenuItemDvgPLC_Click(sender, e); ;
        }

        private void ToggleBitToolStripButton_Click(object sender, EventArgs e)
        {
            string label = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[0].Value.ToString();
            string device;
            int num = 0;
            bool isDigit = SettingDevice(label, out device, out num);
            bool[] coils = PLC.GetCoils(device);
            coils[num] = !coils[num];
        }

        private void OffBitToolStripButton_Click(object sender, EventArgs e)
        {
            string label = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[0].Value.ToString();
            string device;
            int num = 0;
            bool isDigit = SettingDevice(label, out device, out num);
            bool[] coils = PLC.GetCoils(device);
            coils[num] = false;
        }

        private void OnBitToolStripButton_Click(object sender, EventArgs e)
        {
            string label = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[0].Value.ToString();
            string device;
            int num = 0;
            bool isDigit = SettingDevice(label, out device, out num);
            bool[] coils = PLC.GetCoils(device);
            coils[num] = true;
        }

        private void ClearToolStripButton_Click(object sender, EventArgs e)
        {
            PLC.Memory.Clear();
        }

        private void StopcountToolStripButton_Click(object sender, EventArgs e)
        {
            StopToolStripMenuItemDvgPLC_Click(sender, e);
        }

        private void DecrementToolStripButton_Click(object sender, EventArgs e)
        {
            DecrementToolStripMenuItemDvgPLC_Click(sender, e);
        }

        private void IncrementToolStripButton_Click(object sender, EventArgs e)
        {
            IncrementToolStripMenuItemDvgPLC_Click(sender, e);

        }

        frmOptions _frmOptions = new frmOptions();
        private void OptionToolStripButton_Click(object sender, EventArgs e)
        {
            if (!BeforeStartForm(_frmOptions)) return;
            _frmOptions._pathSetting = pathSetting;
            _frmOptions._localAppData = localAppData;
            _frmOptions._settingJson = settingJson;
            _frmOptions.Show(this);
            _frmOptions.BringToFront();

            //this.TopMost =  settingJson.StatusOptions[0];
            //_floaty.TopMost = this.TopMost;
            //(floaty as Floaty).TopMost= settingJson.StatusOptions[0];
            // SettingStatusStrip();
            //TrayEnabled(true);
        }
        frmDeviceMemory _frmDeviceMemory = new frmDeviceMemory();
        private void DeviceMemoryToolStripButton_Click(object sender, EventArgs e)
        {
            if (BeforeStartForm(_frmDeviceMemory))
            {
                _frmDeviceMemory.Show(this);
                _frmDeviceMemory.BringToFront();
            }
        }

        private void CtmnuTextBox_Opening(object sender, CancelEventArgs e)
        {
            if (txtBox_Cell.SelectionLength > 0)
            {
                int length = ctmnuTextBox.Items.Count;
                for (int i = 1; i < length; i++)
                {
                    ctmnuTextBox.Items[i].Enabled = true;
                }
            }
            else
            {
                int length = ctmnuTextBox.Items.Count;
                for (int i = 1; i < length; i++)
                {
                    ctmnuTextBox.Items[i].Enabled = false;
                }
                selectAllToolStripMenuItemTextBox.Enabled = true;
                pasteToolStripMenuItemTextBox.Enabled = true;
            }
            undoToolStripMenuItemTextBox.Enabled = txtBox_Cell.CanUndo;
        }

        private void SelectAllToolStripMenuItemTextBox_Click(object sender, EventArgs e)
        {
            try
            {
                txtBox_Cell.SelectAll();
            }
            catch { }
        }

        private void DeleteToolStripMenuItemTextBox_Click(object sender, EventArgs e)
        {
            try
            {
                string text = Clipboard.GetText();
                //txtBox_Cell.Text= txtBox_Cell.Text.Remove(txtBox_Cell.SelectionStart, txtBox_Cell.SelectionLength);
                txtBox_Cell.Cut();
                Clipboard.SetText(text);
            }
            catch { }
        }

        private void UndoToolStripMenuItemTextBox_Click(object sender, EventArgs e)
        {
            if (txtBox_Cell.CanUndo)
                txtBox_Cell.Undo();
        }

        private void PasteToolStripMenuItemTextBox_Click(object sender, EventArgs e)
        {
            txtBox_Cell.Paste();
        }

        private void CopyToolStripMenuItemTextBox_Click(object sender, EventArgs e)
        {
            txtBox_Cell.Copy();
        }

        private void CutToolStripMenuItemTextBox_Click(object sender, EventArgs e)
        {
            txtBox_Cell.Cut();
        }

        bool _isSettimeDropDownOpening = false;
        private void SettimeToolStripMenuItemDvgPLC_DropDownOpening(object sender, EventArgs e)
        {
            _isSettimeDropDownOpening = true;
            int interval = DsTimers2[deviceCtmnuClipboardDvgPLC].Item1.Interval;
            settimetoolStripComboBoxDvgPLC.Text = (interval * 0.001d).ToString("N1");
            _isSettimeDropDownOpening = false;
        }

        private void SetTimeToolStripSplitButton_DropDownOpening(object sender, EventArgs e)
        {
            deviceCtmnuClipboardDvgPLC = dvgPLC[0, dvgPLC.CurrentRow.Index].Value.ToString();
            int interval = DsTimers2[deviceCtmnuClipboardDvgPLC].Item1.Interval;
            settimeToolStripComboBox.Text = (interval * 0.001d).ToString("N1");
        }

        private bool isTextBoxEdit = false;
        private void DvgPLC_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
                isTextBoxEdit = true;
        }

        private void StopToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            int index = dvgPLC.CurrentRow.Index;
            string device = dvgPLC[0, index].Value.ToString();
            if (DsTimers2.ContainsKey(device))
            {
                Tuple<System.Windows.Forms.Timer, bool> tuple;
                DsTimers2.TryGetValue(device, out tuple);
                tuple.Item1.Stop();
                tuple.Item1.Dispose();
                DsTimers2.Remove(device);
            }
            incrementToolStripButton.Enabled = true;
            decrementToolStripButton.Enabled = true;
            setTimeToolStripSplitButton.Enabled = false;
            stopcountToolStripButton.Enabled = false;
        }

        private void SettimeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = dvgPLC.CurrentRow.Index;
            string device = dvgPLC[0, index].Value.ToString();
            if (DsTimers2.ContainsKey(device))
            {
                int index_time = settimeToolStripComboBox.SelectedIndex;
                double time;
                if (double.TryParse(settimeToolStripComboBox.Items[index_time].ToString(), out time))
                {
                    DsTimers2[device].Item1.Interval = (int)(time * 1000);
                }
            }
            setTimeToolStripSplitButton.HideDropDown();
        }

        private void SettimetoolStripComboBoxDvgPLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = dvgPLC.CurrentRow.Index;
            string device = dvgPLC[0, index].Value.ToString();
            if (DsTimers2.ContainsKey(device))
            {
                int index_time = settimetoolStripComboBoxDvgPLC.SelectedIndex;
                double time;
                if (double.TryParse(settimetoolStripComboBoxDvgPLC.Items[index_time].ToString(), out time))
                {
                    DsTimers2[device].Item1.Interval = (int)(time * 1000);
                }
            }
            if (!_isSettimeDropDownOpening)
                ctmnuClipboardDvgPLC.Close();
        }

        private void DecrementToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            int index = dvgPLC.CurrentRow.Index;
            string label = dvgPLC[0, index].Value.ToString();

            // Lấy devide để xử lý
            //Tuỳ chỉnh label X8=> X,8
            string device;
            int num = 0;
            bool isDigit = SettingDevice(label, out device, out num);
            if (!DsTimers2.ContainsKey(label))
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 1000 };
                Tuple<System.Windows.Forms.Timer, bool> tuple = Tuple.Create(timer, false);
                DsTimers2.Add(label, tuple);
            }
            else
            {
                Tuple<System.Windows.Forms.Timer, bool> tuple;
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer()
                { Interval = DsTimers2[label].Item1.Interval };
                DsTimers2[label].Item1.Stop();
                DsTimers2[label].Item1.Dispose();

                tuple = new Tuple<System.Windows.Forms.Timer, bool>
                    (timer, false);
                DsTimers2[label] = tuple;
            }


            DsTimers2[label].Item1.Start(); //timer la Item1 , Item 2 la bool chỉ ra là
                                            // đang tăng hay giảm
            if (dvgPLC[3, index].Value.ToString() == "Bit")
            {
                DsTimers2[label].Item1.Tick += (timer_sender, timer_e) =>
                {
                    bool[] coils = PLC.GetCoils(device);
                    coils[num] = !coils[num];
                };
            }
            else //Word
            {
                byte[] buffer = PLC.GetRegisters(device);
                DsTimers2[label].Item1.Tick += (timer_sender, timer_e) =>
                {
                    short value = buffer.GetWord(num);
                    value--;
                    buffer.SetWord(value, num);
                };
            }
            incrementToolStripButton.Enabled = true;
            decrementToolStripButton.Enabled = false;
            setTimeToolStripSplitButton.Enabled = true;
            stopcountToolStripButton.Enabled = true;
        }

        private void IncrementToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            int index = dvgPLC.CurrentRow.Index;
            string label = dvgPLC[0, index].Value.ToString();
            // Lấy devide để xử lý
            //Tuỳ chỉnh label X8=> X,8
            string device;
            int num = 0;
            SettingDevice(label, out device, out num);
            if (!DsTimers2.ContainsKey(label))
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 1000 };
                Tuple<System.Windows.Forms.Timer, bool> tuple = Tuple.Create(timer, true);
                DsTimers2.Add(label, tuple);
            }
            else
            {
                Tuple<System.Windows.Forms.Timer, bool> tuple;
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = DsTimers2[label].Item1.Interval };
                DsTimers2[label].Item1.Stop();
                DsTimers2[label].Item1.Dispose();

                tuple = new Tuple<System.Windows.Forms.Timer, bool>
                    (timer, true);
                DsTimers2[label] = tuple;
            }

            DsTimers2[label].Item1.Start(); //timer la Item1 , Item 2 la bool chỉ ra là
                                            // đang tăng hay giảm
            if (dvgPLC[3, index].Value.ToString() == "Bit")
            {
                DsTimers2[label].Item1.Tick += (timer_sender, timer_e) =>
                {
                    bool[] coils = PLC.GetCoils(device);
                    coils[num] = !coils[num];
                };
            }
            else //Word
            {
                byte[] buffer = PLC.GetRegisters(device);

                DsTimers2[label].Item1.Tick += (timer_sender, timer_e) =>
                {
                    short value = buffer.GetWord(num);
                    value++;
                    buffer.SetWord(value, num);
                };
            }
            incrementToolStripButton.Enabled = false;
            decrementToolStripButton.Enabled = true;
            setTimeToolStripSplitButton.Enabled = true;
            stopcountToolStripButton.Enabled = true;
        }

        private void DvgPLC_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {//Vẽ 3D Dòng đầu tiên
            if (e.RowIndex == -1)
            {
                Rectangle rectangle = e.CellBounds;
                Color blue = Color.FromArgb(201, 223, 255);
                LinearGradientBrush linearGradient = new LinearGradientBrush(rectangle
                    , Color.White, blue, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(linearGradient, rectangle);
                e.PaintContent(e.ClipBounds);
                e.Paint(e.ClipBounds,
                    DataGridViewPaintParts.Border);
                e.Handled = true;
            }
        }

        private void TimerIO_Tick(object sender, EventArgs e)
        {
            Array.Copy(PLC.Memory.X, 0, IO, 0, 16);
            fx5uCpu1.X = IO;
            Array.Copy(PLC.Memory.Y, 0, IO, 0, 16);
            fx5uCpu1.Y = IO;
        }

        private void TimerLAN_Tick(object sender, EventArgs e)
        {
            fx5uCpu1.LAN = !fx5uCpu1.LAN;
        }

        StringBuilder strbin = new StringBuilder();
        private void TimerStartMonitor_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dvgPLC.RowCount; i++)
            {
                if (dvgPLC.Rows[i].Cells[0].Value != null)
                {
                    string display = dvgPLC.Rows[i].Cells[2].Value.ToString();
                    string label = dvgPLC.Rows[i].Cells[0].Value.ToString();
                    //Tuỳ chỉnh label X8=> X,8
                    label = sWhitespace.Replace(label, "").ToUpper();
                    string device;
                    int num = 0;
                    bool isDigit = SettingDevice(label, out device, out num);
                    if (IsContainsWordDevice(device) && isDigit)
                    {
                        byte[] buffer = PLC.GetRegisters(device);
                        string dataType = dvgPLC.Rows[i].Cells[3].Value.ToString();
                        switch (dataType)
                        {
                            case DataConvertExtendsion.U_WORD:
                                if (display == DataConvertExtendsion.Decimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = ((ushort)buffer.GetWord(num)).ToString();
                                }
                                else if (display == DataConvertExtendsion.BIN)
                                {
                                    strbin.Clear();
                                    string str = Convert.ToString((ushort)buffer.GetWord(num), 2).PadLeft(16, '0');
                                    for (int j = 0; j <= 12; j += 4)
                                    {
                                        strbin.Append(str.Substring(j, 4));
                                        if (j != 12) strbin.Append(' ');
                                    }
                                    dvgPLC.Rows[i].Cells[1].Value = strbin.ToString();
                                }
                                else if (display == DataConvertExtendsion.Hexadecimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = "H" + ((ushort)buffer.GetWord(num)).ToString("X2").PadLeft(4, '0');
                                }
                                break;
                            case DataConvertExtendsion.U_DOUBLEWORD:
                                if (display == DataConvertExtendsion.Decimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = ((uint)buffer.GetDoubleWord(num)).ToString();
                                }
                                else if (display == DataConvertExtendsion.BIN)
                                {
                                    strbin.Clear();
                                    string str = Convert.ToString((uint)buffer.GetDoubleWord(num), 2).PadLeft(32, '0');
                                    for (int j = 0; j <= 28; j += 4)
                                    {
                                        strbin.Append(str.Substring(j, 4));
                                        if (j != 28) strbin.Append(' ');
                                    }
                                    dvgPLC.Rows[i].Cells[1].Value = strbin.ToString();
                                }
                                else if (display == DataConvertExtendsion.Hexadecimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = "H" + ((uint)buffer.GetDoubleWord(num)).ToString("X2").PadLeft(8, '0');
                                }
                                break;
                            case DataConvertExtendsion.WORD:
                                if (display == DataConvertExtendsion.Decimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = buffer.GetWord(num).ToString();
                                }
                                else if (display == DataConvertExtendsion.BIN)
                                {
                                    strbin.Clear();
                                    string str = Convert.ToString(buffer.GetWord(num), 2).PadLeft(16, '0');
                                    for (int j = 0; j <= 12; j += 4)
                                    {
                                        strbin.Append(str.Substring(j, 4));
                                        if (j != 12) strbin.Append(' ');
                                    }

                                    dvgPLC.Rows[i].Cells[1].Value = strbin.ToString();
                                }
                                else if (display == DataConvertExtendsion.Hexadecimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = "H" + buffer.GetWord(num).ToString("X2").PadLeft(4, '0');
                                }
                                break;
                            case DataConvertExtendsion.DOUBLEWORD:
                                if (display == DataConvertExtendsion.Decimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = buffer.GetDoubleWord(num).ToString();
                                }
                                else if (display == DataConvertExtendsion.BIN)
                                {
                                    strbin.Clear();
                                    string str = Convert.ToString(buffer.GetDoubleWord(num), 2).PadLeft(32, '0');
                                    for (int j = 0; j <= 28; j += 4)
                                    {
                                        strbin.Append(str.Substring(j, 4));
                                        if (j != 28) strbin.Append(' ');
                                    }
                                    dvgPLC.Rows[i].Cells[1].Value = strbin.ToString();
                                }
                                else if (display == DataConvertExtendsion.Hexadecimal)
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = "H" + buffer.GetDoubleWord(num).ToString("X2").PadLeft(8, '0');
                                }
                                break;
                            case DataConvertExtendsion.FLOAT:
                                float value = buffer.GetFloat(num);
                                long intPart = (long)value;
                                // float fractionalPart = value - intPart;
                                if (float.IsNaN(value))
                                {
                                    dvgPLC.Rows[i].Cells[1].Value = float.NaN.ToString();
                                }
                                else if (value > 100000 || (value > 0 && value < 0.00001f))
                                    dvgPLC.Rows[i].Cells[1].Value = buffer.GetFloat(num).ToString("E6");
                                else
                                    dvgPLC.Rows[i].Cells[1].Value = buffer.GetFloat(num).ToString("N6");
                                break;
                            case DataConvertExtendsion.STRING:
                                dvgPLC.Rows[i].Cells[1].Value = buffer.GetString(num);
                                break;
                            case DataConvertExtendsion.TIME:
                                dvgPLC.Rows[i].Cells[1].Value = buffer.GetTime(num);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (IsContainsBitDevice(device) && isDigit)
                    {
                        bool[] coils = PLC.GetCoils(device);
                        //Set color if true(blue) or false(xanh nhat or white)

                        if (display == DataConvertExtendsion.BIN)
                        {
                            dvgPLC.Rows[i].Cells[1].Value = coils[num].ToString().ToUpper();

                        }
                        else if (display == DataConvertExtendsion.Decimal)
                        {
                            if (coils[num])
                            {
                                dvgPLC.Rows[i].Cells[1].Value = "1";
                            }
                            else
                                dvgPLC.Rows[i].Cells[1].Value = "0";
                        }
                        else if (display == DataConvertExtendsion.Hexadecimal)
                        {
                            if (coils[num])
                            {
                                dvgPLC.Rows[i].Cells[1].Value = "H01";
                            }
                            else
                                dvgPLC.Rows[i].Cells[1].Value = "H00";

                        }
                        bool islessthan1 = false; //Giảm lag cho dvg khi thay đổi màu liên tục khi time <1
                        if (DsTimers2.ContainsKey(label))
                        {
                            if (DsTimers2[label].Item1.Interval < 1000)
                            {
                                islessthan1 = true;
                            }
                        }
                        if (coils[num] && !islessthan1)
                        {
                            if (dvgPLC.CurrentRow.Index == i)
                            {
                                if (dvgPLC.Rows[i].DefaultCellStyle.BackColor != Color.White)
                                {
                                    dvgPLC.Rows[i].DefaultCellStyle.BackColor = Color.White;
                                    dvgPLC.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                }
                            }
                            else
                            {

                                if (dvgPLC.Rows[i].DefaultCellStyle.BackColor != Color.Blue)
                                {
                                    dvgPLC.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
                                    dvgPLC.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                        else if (!islessthan1)
                        {
                            if (i % 2 != 0)
                            {
                                if (dvgPLC.Rows[i].DefaultCellStyle.BackColor != Color.FromArgb(227, 234, 255))
                                {
                                    dvgPLC.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                                    dvgPLC.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                }
                            }
                            else
                            {
                                if (dvgPLC.Rows[i].DefaultCellStyle.BackColor != Color.White)
                                {
                                    dvgPLC.Rows[i].DefaultCellStyle.BackColor = Color.White;
                                    dvgPLC.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void StartserverToolStripButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(_frmNetworkSetting.txtPort.Text, out port))
            {
                MessageBox.Show("The port number is invalid.", "Network Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_frmNetworkSetting.chkboxAnyAddress.Checked)
            {
                ipAddress = IPAddress.Any;
            }
            else
            {
                if (!IPAddress.TryParse(_frmNetworkSetting.txtIP.Text, out ipAddress))
                {
                    MessageBox.Show("The ip-address is invalid.", "Network Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            PLC.IPAddress = ipAddress;
            PLC.Port = port;
            PLC.CPUName = CpuTypeToolStripComboBox.Text;

            try
            {
                PLC.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("The specified port is in use.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            startserverToolStripButton.Enabled = false;
            stopserverToolStripButton.Enabled = true;
            IsMonitor = true;
            CpuTypeToolStripComboBox.Enabled = false;
            //timer
            timerStartMonitor.Start();
            timerIO.Start();
            timerLAN.Start();
            SetReadOnlyCell(false);
            fx5uCpu1.PWR = true;
            fx5uCpu1.RUN = true;
            //
            int currentIndex = dvgPLC.CurrentRow.Index;
            if (currentIndex == dvgPLC.RowCount - 1)
            {
                incrementToolStripButton.Enabled = false;
                decrementToolStripButton.Enabled = false;
            }
            else
            {
                string label = dvgPLC.Rows[currentIndex].Cells[0].Value.ToString();
                //Tuỳ chỉnh label X8=> X,8
                label = sWhitespace.Replace(label, "").ToUpper();
                string device;
                int num = 0;
                bool isDigit = SettingDevice(label, out device, out num);
                if ((IsContainsWordDevice(device) || IsContainsBitDevice(device)) && isDigit)
                {
                    incrementToolStripButton.Enabled = true;
                    decrementToolStripButton.Enabled = true;
                }
                else
                {
                    incrementToolStripButton.Enabled = false;
                    decrementToolStripButton.Enabled = false;
                }
            }
            setTimeToolStripSplitButton.Enabled = false;
            stopcountToolStripButton.Enabled = false;
            startRuntimeServiceToolStripMenuItemNotify.Enabled = false;
            stopRuntimeServiceToolStripMenuItemNotify.Enabled = true;
            clientsTssl.Visible = true;
            clientsTssl.Text = "Clients: " + PLC.ListClients.Count;
            splitTssl.Visible = true;
            statusTssl.Text = $"Host-{PLC.IPAddress}, {PLC.Port}";
            ChangeTextNorifyStart(0);
        }
        StringBuilder _stringBuilderNotify = new StringBuilder();
        private void StopserverToolStripButton_Click(object sender, EventArgs e)
        {
            startserverToolStripButton.Enabled = true;
            stopserverToolStripButton.Enabled = false;
            PLC.Stop(); IsMonitor = false;

            CpuTypeToolStripComboBox.Enabled = true;
            timerStartMonitor.Stop();
            timerIO.Stop();
            timerLAN.Stop();
            timerLedRUN.Stop();
            timerErr.Stop();
            SetReadOnlyCell(true);
            fx5uCpu1.PWR = false;
            fx5uCpu1.RUN = false;
            fx5uCpu1.LAN = false;
            fx5uCpu1.ERR = false;
            onBitToolStripButton.Enabled = false;
            offBitToolStripButton.Enabled = false;
            toggleBitToolStripButton.Enabled = false;
            incrementToolStripButton.Enabled = false;
            decrementToolStripButton.Enabled = false;
            setTimeToolStripSplitButton.Enabled = false;
            stopcountToolStripButton.Enabled = false;
            Array.Clear(IO, 0, IO.Length);
            fx5uCpu1.X = fx5uCpu1.Y = IO;
            foreach (var item in DsTimers2)
            {
                item.Value.Item1.Stop();
            }
            DsTimers2.Clear();

            startRuntimeServiceToolStripMenuItemNotify.Enabled = true;
            stopRuntimeServiceToolStripMenuItemNotify.Enabled = false;
            clientsTssl.Visible = false;
            splitTssl.Visible = false;
            statusTssl.Text = "Not running";
            ChangeTextNorifyStop();
            ChangeRowColor();
        }
        private void ChangeTextNorifyStart(int clients)
        {
            _stringBuilderNotify.Clear();
            _stringBuilderNotify.Append(this.ProductName);
            _stringBuilderNotify.Append(" ");
            _stringBuilderNotify.AppendLine(this.ProductVersion);

            _stringBuilderNotify.Append("Clients: ");
            _stringBuilderNotify.AppendLine(clients.ToString());
            _stringBuilderNotify.Append("IPAddress: ");
            if (settingJson.AnyAddress)
            {
                _stringBuilderNotify.AppendLine("Any");
            }
            else
            {
                _stringBuilderNotify.AppendLine(PLC.IPAddress.ToString());
            }
            _stringBuilderNotify.Append("Port: ");
            _stringBuilderNotify.Append(PLC.Port);
            notifyIconPLC.Text = _stringBuilderNotify.ToString();

        }
        private void ChangeTextNorifyStop()
        {
            _stringBuilderNotify.Clear();
            _stringBuilderNotify.Append(this.ProductName);
            _stringBuilderNotify.Append(" ");
            _stringBuilderNotify.AppendLine(this.ProductVersion);
            _stringBuilderNotify.Append("Not running");
            notifyIconPLC.Text = _stringBuilderNotify.ToString();

        }

        frmNetworkSetting _frmNetworkSetting = new frmNetworkSetting();
        private void NetworkToolStripButton_Click(object sender, EventArgs e)
        {
            if (BeforeStartForm(_frmNetworkSetting))
            {
                _frmNetworkSetting.Show(this);
                _frmNetworkSetting.BringToFront();
            }
        }

        private void TrayEnabled(bool value)
        {
            networkConfigurationToolStripMenuItemNotify.Enabled = value;
            listOfClientsToolStripMenuItemNotify.Enabled = value;
            optionsToolStripMenuItemNotify.Enabled = value;
            aboutToolStripMenuItemNotify.Enabled = value;
            deviceBufferMemoryToolStripMenuItemNotify.Enabled = value;
        }

        private void SelectAllToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            if (isTextBoxEdit)
            {
                selectAllToolStripMenuItemTextBox.PerformClick();
                return;
            }
            dvgPLC.SelectAll();
        }

        private void DeleteToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            if (isTextBoxEdit)
            {
                deleteToolStripMenuItemTextBox.PerformClick();
                return;
            }
            try
            {
                foreach (DataGridViewRow item in dvgPLC.SelectedRows)
                {
                    if (item.Index != dvgPLC.Rows.Count - 1)
                    {
                        dvgPLC.Rows.Remove(item);
                    }
                }
            }
            catch { }
        }

        private void CutToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            if (isTextBoxEdit)
            {
                cutToolStripMenuItemTextBox.PerformClick();
                return;
            }
            try
            {
                Clipboard.SetDataObject(dvgPLC.GetClipboardContent());
                foreach (DataGridViewRow item in dvgPLC.SelectedRows)
                {
                    if (item.Cells[0].Value != null)
                        dvgPLC.Rows.Remove(item);
                }
            }
            catch { }
        }

        //Nhấn shift+enter = đặt giá trị true/false cho bit
        private void DvgPLC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter)
            {
                if (onBitToolStripButton.Enabled)
                {
                    string label = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[0].Value.ToString();
                    string device;
                    int num = 0;
                    bool isDigit = SettingDevice(label, out device, out num);
                    bool[] coils = PLC.GetCoils(device);
                    coils[num] = !coils[num];
                }
                e.Handled = true;
            }
        }


        private void DvgPLC_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int length = dvgPLC.SelectedRows.Count;
                int currentRow = e.RowIndex;
                isblockrow = false;
                dsIndexs.Clear();

                for (int i = 0; i < length; i++)
                {
                    dsIndexs.Add(dvgPLC.SelectedRows[i].Index);
                    if (currentRow == dvgPLC.SelectedRows[i].Index) { isblockrow = true; }
                }
            }
        }

        private void CtmnuClipboardDvgPLC_Opened(object sender, EventArgs e)
        {
            if (!isblockrow) return;
            foreach (var item in dsIndexs)
            {
                dvgPLC.Rows[item].Selected = true;
            }
        }

        private void CopyToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            if (isTextBoxEdit)
            {
                copyToolStripMenuItemTextBox.PerformClick();
                return;
            }
            Clipboard.SetDataObject(dvgPLC.GetClipboardContent());
        }

        private void PastToolStripMenuItemDvgPLC_Click(object sender, EventArgs e)
        {
            if (isTextBoxEdit)
            {
                pasteToolStripMenuItemTextBox.PerformClick();
                return;
            }
            //Lấy từng row strEmptyRow(tức là cách nhau dòng)
            var ArrStrRow = Clipboard.GetText().Split(splitNewLine);
            for (int i = 0; i < ArrStrRow.Length; i++)
            {
                if (ArrStrRow[i] == "Name\tCurrent Value\tDisplay Format\tData Type")
                {
                    ArrStrRow[i] = strEmptyRow;
                }
            }
            //Xoá bỏ các hàng chứa
            var ArrStrRow_Filter = (from str in ArrStrRow
                                    where (str != "") && (str != strEmptyRow)
                                    select str).ToArray();
            try //Nếu copy paste hàng bình thường
            {
                for (int i = 0; i < ArrStrRow_Filter.Length; i++)
                {
                    CustomGridView_PasteData(ArrStrRow_Filter[i].Split(splitTab));
                }
            }
            catch //Lỗi là vì dữ liệu copy là 1 ô thôi thì add vào giống cellendit;
            {
                CustomGridView_PasteDataNewRow(Clipboard.GetText());
            }
        }

        private void CustomGridView_PasteData(string[] RowDevie)
        {
            int new_row_index = dvgPLC.RowCount - 1;
            if (new_row_index == -1) new_row_index = 0;
            string device;
            int digit = 0;
            bool isDigit = SettingDevice(RowDevie[0], out device, out digit);
            if (isDigit && IsContainsBitDevice(device))
            {
                //Bit
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpDevice;
                for (int i = 0; i < dvgPLC.ColumnCount; i++)
                {
                    if (i == 2)
                    {
                        dvgPLC[i, new_row_index] = AddDisplayFormat(RowDevie[2]);
                        dvgPLC[i, new_row_index].ReadOnly = false;
                    }
                    else
                    {
                        dvgPLC[i, new_row_index].Value = RowDevie[i];
                    }
                }
                dvgPLC[1, new_row_index].ReadOnly = !IsMonitor;
            }
            else if (isDigit && IsContainsWordDevice(device)) //Word
            {
                //Hìnha ảnh cell
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpDevice;
                //Word
                for (int i = 0; i < dvgPLC.ColumnCount; i++)
                {
                    if (i == 2 && RowDevie[3] != DataConvertExtendsion.FLOAT
                        && RowDevie[3] != DataConvertExtendsion.TIME
                        && RowDevie[3] != DataConvertExtendsion.STRING)
                    {
                        dvgPLC[i, new_row_index] = AddDisplayFormat(RowDevie[2]);
                        dvgPLC[i, new_row_index].ReadOnly = false;

                    }
                    else if (i == 3)
                    {
                        dvgPLC[i, new_row_index] = AddDataType(RowDevie[0], RowDevie[3]);
                        dvgPLC[i, new_row_index].ReadOnly = false;
                    }
                    else
                    {
                        dvgPLC[i, new_row_index].Value = RowDevie[i];
                    }
                }
                dvgPLC[1, new_row_index].ReadOnly = !IsMonitor;
            }
            else
            {
                //None
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpNon_temp;
                dvgPLC[0, new_row_index].Value = RowDevie[0];
                for (int i = 1; i < dvgPLC.ColumnCount; i++)
                {
                    dvgPLC[i, new_row_index].Value = "--";
                }
            }

            //Thêm dòng mới
            dvgPLC.Rows.Add();
            new_row_index = dvgPLC.RowCount - 1;
            DataGridViewTextBoxImageCell boxImageCell1 = new DataGridViewTextBoxImageCell();
            boxImageCell1 = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
            boxImageCell1.PointImageX = 0;
            boxImageCell1.Image = bmpNon;
            for (int i = 1; i < 4; i++)
            {
                dvgPLC.Rows[new_row_index].Cells[i].ReadOnly = true;
            }

            if (new_row_index % 2 != 0) // Hàng lẻ
            {
                dvgPLC.Rows[new_row_index].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                dvgPLC.InvalidateRow(new_row_index);
            }
        }
        private void CustomGridView_PasteDataWatch(DvgWatch watch)
        {
            int new_row_index = dvgPLC.RowCount - 1;
            if (new_row_index == -1) new_row_index = 0;
            string device;
            int digit = 0;
            bool isDigit = SettingDevice(watch.Name, out device, out digit);
            if (isDigit && IsContainsBitDevice(device))
            {
                //Bit
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpDevice;
                dvgPLC[0, new_row_index].Value = watch.Name;
                dvgPLC[1, new_row_index].Value = watch.CurrentValue;
                dvgPLC[2, new_row_index] = AddDisplayFormat(watch.DisplayFormat);
                dvgPLC[3, new_row_index].Value = watch.DataType;
                dvgPLC[1, new_row_index].ReadOnly = !IsMonitor;
            }
            else if (isDigit && IsContainsWordDevice(device)) //Word
            {
                //Hìnha ảnh cell
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpDevice;
                //Word
                dvgPLC[0, new_row_index].Value = watch.Name;
                dvgPLC[1, new_row_index].Value = watch.CurrentValue;
                if (watch.DataType != DataConvertExtendsion.FLOAT
                        && watch.DataType != DataConvertExtendsion.TIME
                        && watch.DataType != DataConvertExtendsion.STRING)
                {
                    dvgPLC[2, new_row_index] = AddDisplayFormat(watch.DisplayFormat);
                    dvgPLC[2, new_row_index].ReadOnly = false;
                }
                else
                    dvgPLC[2, new_row_index].Value = watch.DisplayFormat;
                dvgPLC[3, new_row_index] = AddDataType(watch.Name, watch.DataType);
                dvgPLC[3, new_row_index].ReadOnly = false;
                dvgPLC[1, new_row_index].ReadOnly = !IsMonitor;
            }
            else
            {
                //None
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpNon_temp;
                dvgPLC[0, new_row_index].Value = watch.Name;
                for (int i = 1; i < dvgPLC.ColumnCount; i++)
                {
                    dvgPLC[i, new_row_index].Value = "--";
                }
            }

            //Thêm dòng mới
            dvgPLC.Rows.Add();
            new_row_index = dvgPLC.RowCount - 1;
            DataGridViewTextBoxImageCell boxImageCell1 = new DataGridViewTextBoxImageCell();
            boxImageCell1 = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
            boxImageCell1.PointImageX = 0;
            boxImageCell1.Image = bmpNon;
            for (int i = 1; i < 4; i++)
            {
                dvgPLC.Rows[new_row_index].Cells[i].ReadOnly = true;
            }

            if (new_row_index % 2 != 0) // Hàng lẻ
            {
                dvgPLC.Rows[new_row_index].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                dvgPLC.InvalidateRow(new_row_index);
            }
        }
        private DataGridViewComboBoxCell AddDisplayFormat(string displayValue)
        {
            DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
            cbocell.Items.Add("BIN");
            cbocell.Items.Add("Decimal");
            cbocell.Items.Add("Hexadecimal");
            cbocell.Value = displayValue;
            //cbocell.Style.SelectionBackColor = Color.Red;
            cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            return cbocell;
        }
        private DataGridViewComboBoxCell AddDataType(string device, string displayValue)
        {
            DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
            if (device == "LZ")
            {
                cbocell.Items.Add("Double Word [Unsigned]/Bit String [32-bit]");
                cbocell.Items.Add("Double Word [Signed]");
                cbocell.Items.Add("FLOAT [Single Precision]");
                cbocell.Items.Add("Time");
                cbocell.Value = displayValue;
            }
            else
            {
                cbocell.Items.Add("Word [Unsigned]/Bit String [16-bit]");
                cbocell.Items.Add("Double Word [Unsigned]/Bit String [32-bit]");
                cbocell.Items.Add("Word [Signed]");
                cbocell.Items.Add("Double Word [Signed]");
                cbocell.Items.Add("FLOAT [Single Precision]");
                cbocell.Items.Add("String");
                cbocell.Items.Add("Time");
                cbocell.Value = displayValue;
            }
            cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            cbocell.FlatStyle = FlatStyle.Popup;
            return cbocell;
        }
        private void CustomGridView_PasteDataNewRow(string dataDevice)
        {
            int new_row_index = dvgPLC.RowCount - 1;
            string label = dataDevice;
            //Tuỳ chỉnh label X8=> X,8
            label = sWhitespace.Replace(label, "").ToUpper();
            string device;
            int digit = 0;
            bool isDigit = SettingDevice(label, out device, out digit);

            if (IsContainsWordDevice(device) && isDigit) //Word => cell 2,3=ComboBox
            {
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpDevice;
                //Word
                dvgPLC[0, new_row_index].Value = label;
                for (int i = 1; i < dvgPLC.ColumnCount; i++)
                {
                    if (i == 2)
                    {
                        dvgPLC[i, new_row_index] = AddDisplayFormat(DataConvertExtendsion.Decimal);
                        dvgPLC[i, new_row_index].ReadOnly = false;

                    }
                    else if (i == 3)
                    {
                        if (device == "LZ")
                        {
                            dvgPLC[i, new_row_index] = AddDataType(device, DataConvertExtendsion.DOUBLEWORD);
                        }
                        else
                        {
                            dvgPLC[i, new_row_index] = AddDataType(device, DataConvertExtendsion.WORD);
                        }
                        dvgPLC[i, new_row_index].ReadOnly = false;
                    }
                    else
                    {
                        dvgPLC[i, new_row_index].Value = "--";
                    }
                }
                dvgPLC[1, new_row_index].ReadOnly = !IsMonitor;
            }
            else if (IsContainsBitDevice(device) && isDigit)
            {

                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpDevice;
                dvgPLC[0, new_row_index].Value = label;
                for (int i = 1; i < dvgPLC.ColumnCount; i++)
                {
                    if (i == 2)
                    {
                        dvgPLC[i, new_row_index] = AddDisplayFormat(DataConvertExtendsion.BIN);
                        dvgPLC[i, new_row_index].ReadOnly = false;
                    }
                    else
                    {
                        dvgPLC[i, new_row_index].Value = "--";
                    }
                }
                dvgPLC[1, new_row_index].ReadOnly = !IsMonitor;
            }
            else
            {
                DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
                boxImageCell.PointImageX = 20;
                boxImageCell.Image = bmpNon_temp;
                dvgPLC[0, new_row_index].Value = dataDevice;
                for (int i = 1; i < dvgPLC.ColumnCount; i++)
                {
                    dvgPLC[i, new_row_index].Value = "--";
                }
            }

            //Thêm dòng mới
            dvgPLC.Rows.Add();
            new_row_index = dvgPLC.RowCount - 1;
            DataGridViewTextBoxImageCell boxImageCell1 = new DataGridViewTextBoxImageCell();
            boxImageCell1 = (DataGridViewTextBoxImageCell)dvgPLC[0, new_row_index];
            boxImageCell1.PointImageX = 0;
            boxImageCell1.Image = bmpNon;
            for (int i = 1; i < 4; i++)
            {
                dvgPLC.Rows[new_row_index].Cells[i].ReadOnly = true;
            }

            if (new_row_index % 2 != 0) // Hàng lẻ
            {
                dvgPLC.Rows[new_row_index].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                dvgPLC.InvalidateRow(new_row_index);
            }
        }
        string deviceCtmnuClipboardDvgPLC = " ";
        private void CtmnuClipboardDvgPLC_Opening(object sender, CancelEventArgs e)
        {
            dvgPLC.EndEdit();
            //Di chuyen hang theo nhan chuot phai
            // Sự kiện mouse click của dvg mà nhấn chuột phải sẽ không được vì nó xử lý contextmenustrip rồi
            Point pt = dvgPLC.PointToClient(Cursor.Position);
            if (pt.Y <= 26) { e.Cancel = true; return; }
            DataGridView.HitTestInfo hitTestInfo;
            hitTestInfo = dvgPLC.HitTest(pt.X, pt.Y);
            #region Khi nhấn chuột phải vào hàng nào thì sẽ trỏ đến hàng đấy
            //Khi nhấn vào row
            // if (hitTestInfo.RowIndex == -1) e.Cancel=true;
            if (hitTestInfo.RowIndex > -1)
            {
                try
                {
                    dvgPLC.CurrentCell = dvgPLC.Rows[hitTestInfo.RowIndex].Cells[0];
                }
                catch { }
                ctmnuClipboardDvgPLC.Items["cutToolStripMenuItemDvgPLC"].Enabled = true;
                ctmnuClipboardDvgPLC.Items["copyToolStripMenuItemDvgPLC"].Enabled = true;
                ctmnuClipboardDvgPLC.Items["pastToolStripMenuItemDvgPLC"].Enabled = true;
                ctmnuClipboardDvgPLC.Items["deleteToolStripMenuItemDvgPLC"].Enabled = true;
                ctmnuClipboardDvgPLC.Items["selectAllToolStripMenuItemDvgPLC"].Enabled = true;

                // 6-Increment 7-Decrement 8-SetTime 9-Stop
                if (IsMonitor)
                {
                    //Cho phep hoặc không cho nhấn nút 6-7-8-9

                    if (dvgPLC[1, hitTestInfo.RowIndex].Value != null)
                    {
                        if (dvgPLC[1, hitTestInfo.RowIndex].Value.ToString() != "--")
                        {
                            deviceCtmnuClipboardDvgPLC = dvgPLC[0, hitTestInfo.RowIndex].Value.ToString();
                            if (DsTimers2.ContainsKey(deviceCtmnuClipboardDvgPLC))
                            {
                                bool isIncrement = DsTimers2[deviceCtmnuClipboardDvgPLC].Item2;

                                ctmnuClipboardDvgPLC.Items["incrementToolStripMenuItemDvgPLC"].Enabled = !isIncrement;
                                ctmnuClipboardDvgPLC.Items["decrementToolStripMenuItemDvgPLC"].Enabled = isIncrement;
                                ctmnuClipboardDvgPLC.Items["settimeToolStripMenuItemDvgPLC"].Enabled = true;
                                ctmnuClipboardDvgPLC.Items["stopToolStripMenuItemDvgPLC"].Enabled = true;
                            }
                            else
                            {
                                ctmnuClipboardDvgPLC.Items["incrementToolStripMenuItemDvgPLC"].Enabled = true;
                                ctmnuClipboardDvgPLC.Items["decrementToolStripMenuItemDvgPLC"].Enabled = true;
                                ctmnuClipboardDvgPLC.Items["settimeToolStripMenuItemDvgPLC"].Enabled = false;
                                ctmnuClipboardDvgPLC.Items["stopToolStripMenuItemDvgPLC"].Enabled = false;
                            }

                        }
                        else
                        {

                            ctmnuClipboardDvgPLC.Items["incrementToolStripMenuItemDvgPLC"].Enabled = false;
                            ctmnuClipboardDvgPLC.Items["decrementToolStripMenuItemDvgPLC"].Enabled = false;
                            ctmnuClipboardDvgPLC.Items["settimeToolStripMenuItemDvgPLC"].Enabled = false;
                            ctmnuClipboardDvgPLC.Items["stopToolStripMenuItemDvgPLC"].Enabled = false;
                        }
                    }
                    else
                    {
                        ctmnuClipboardDvgPLC.Items["incrementToolStripMenuItemDvgPLC"].Enabled = false;
                        ctmnuClipboardDvgPLC.Items["decrementToolStripMenuItemDvgPLC"].Enabled = false;
                        ctmnuClipboardDvgPLC.Items["settimeToolStripMenuItemDvgPLC"].Enabled = false;
                        ctmnuClipboardDvgPLC.Items["stopToolStripMenuItemDvgPLC"].Enabled = false;
                    }

                    return;
                }
                else
                {
                    ctmnuClipboardDvgPLC.Items["incrementToolStripMenuItemDvgPLC"].Enabled = false;
                    ctmnuClipboardDvgPLC.Items["decrementToolStripMenuItemDvgPLC"].Enabled = false;
                    ctmnuClipboardDvgPLC.Items["settimeToolStripMenuItemDvgPLC"].Enabled = false;
                    ctmnuClipboardDvgPLC.Items["stopToolStripMenuItemDvgPLC"].Enabled = false;
                    return;
                }
            }
            else
            {
                for (int i = 0; i < ctmnuClipboardDvgPLC.Items.Count; i++)
                {
                    ctmnuClipboardDvgPLC.Items[i].Enabled = false;
                }
                ctmnuClipboardDvgPLC.Items["pastToolStripMenuItemDvgPLC"].Enabled = true;
            }
            #endregion Khi nhấn chuột phải vào hàng nào thì sẽ trỏ đến hàng đấy

        }
        private void DvgPLC_MouseClick_Timer(object sender, MouseEventArgs e)
        {
            //if(e.Button==MouseButtons.Right)
            // {
            //    
            // }
        }
        private void DvgPLC_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (txtBox_Cell.Text == "")
                    dvgPLC[0, e.RowIndex].Value = "";
                CustomGridView(e);
            }
            isTextBoxEdit = false;
            if (!IsMonitor) ChangeRowColor();
        }
        private void CustomGridView(DataGridViewCellEventArgs e)
        {
            //Label
            bool isEmpty = dvgPLC.Rows[e.RowIndex].Cells[0].Value.ToString() == "";
            //if (dvgPLC.Rows[e.RowIndex].Cells[0].Value != null)
            if (!isEmpty)
            {
                string label = dvgPLC.Rows[e.RowIndex].Cells[0].Value.ToString();
                //Tuỳ chỉnh label X8=> X,8
                label = sWhitespace.Replace(label, "").ToUpper();
                string device;
                int digit = 0;
                bool isDigit = SettingDevice(label, out device, out digit);
                bool isCboCell_2 = dvgPLC.Rows[e.RowIndex].Cells[2] is DataGridViewComboBoxCell;
                bool isCboCell_3 = dvgPLC.Rows[e.RowIndex].Cells[3] is DataGridViewComboBoxCell;
                bool isTxtCell_2 = dvgPLC.Rows[e.RowIndex].Cells[2] is DataGridViewTextBoxCell;
                bool isTxtCell_3 = dvgPLC.Rows[e.RowIndex].Cells[3] is DataGridViewTextBoxCell;

                if (IsContainsWordDevice(device) && isDigit) //Word => cell 2,3=ComboBox
                {
                    //Viết hoa
                    dvgPLC.Rows[e.RowIndex].Cells[0].Value = label.ToUpper();
                    //((DataGridViewTextBoxImageCell)dvgPLC.Rows[e.RowIndex].Cells[0]).Image = (Image)bmpDevice;
                    DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, e.RowIndex];
                    boxImageCell.PointImageX = 20;
                    boxImageCell.Image = bmpDevice;

                    if (dvgPLC.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        string dataType = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[3].Value.ToString();
                        if (dataType == DataConvertExtendsion.FLOAT ||
                dataType == DataConvertExtendsion.STRING ||
                dataType == DataConvertExtendsion.TIME)
                        {
                            DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
                            textBoxCell.Value = "--";
                            dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2] = textBoxCell;
                        }
                        else
                        {
                            if (!isCboCell_2)
                            {
                                DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
                                cbocell.Items.Add("BIN");
                                cbocell.Items.Add("Decimal");
                                cbocell.Items.Add("Hexadecimal");
                                cbocell.Value = "Decimal";
                                //cbocell.Style.SelectionBackColor = Color.Red;
                                cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                dvgPLC.Rows[e.RowIndex].Cells[2] = cbocell;
                            }
                            else
                            {
                                //Khi sua tu Bit sang Word phải chuyển BIN sang Decimal
                                DataGridViewComboBoxCell cbocell = dvgPLC.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell;
                                cbocell.Value = "Decimal";
                            }
                        }
                    }
                    else
                    {
                        if (!isCboCell_2)
                        {
                            DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
                            cbocell.Items.Add("BIN");
                            cbocell.Items.Add("Decimal");
                            cbocell.Items.Add("Hexadecimal");
                            cbocell.Value = "Decimal";
                            //  cbocell.Style.SelectionBackColor = Color.Red;
                            cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            dvgPLC.Rows[e.RowIndex].Cells[2] = cbocell;
                        }
                    }

                    if (!isCboCell_3)
                    {
                        DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
                        // cbocell.ValueType = typeof(DataType);

                        if (device == "LZ")
                        {
                            cbocell.Items.Add("Double Word [Unsigned]/Bit String [32-bit]");
                            cbocell.Items.Add("Double Word [Signed]");
                            cbocell.Items.Add("FLOAT [Single Precision]");
                            cbocell.Items.Add("Time");
                            cbocell.Value = "Double Word [Signed]";
                        }
                        else if (IsLastWord(device, digit))//D7999... Thanh ghi cuối cùng
                        {
                            cbocell.Items.Add("Word [Unsigned]/Bit String [16-bit]");
                            cbocell.Items.Add("Word [Signed]");
                            cbocell.Items.Add("String");
                            cbocell.Value = "Word [Signed]";
                        }
                        else
                        {
                            cbocell.Items.Add("Word [Unsigned]/Bit String [16-bit]");
                            cbocell.Items.Add("Double Word [Unsigned]/Bit String [32-bit]");
                            cbocell.Items.Add("Word [Signed]");
                            cbocell.Items.Add("Double Word [Signed]");
                            cbocell.Items.Add("FLOAT [Single Precision]");
                            cbocell.Items.Add("String");
                            cbocell.Items.Add("Time");
                            cbocell.Value = "Word [Signed]";
                        }

                        //cbocell.DropDownWidth
                        cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        cbocell.FlatStyle = FlatStyle.Popup;
                        dvgPLC.Rows[e.RowIndex].Cells[3] = cbocell;
                    }
                    if (!IsMonitor)
                    {
                        dvgPLC.Rows[e.RowIndex].Cells[1].Value = "--";
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        if (!(dvgPLC.Rows[e.RowIndex].Cells[i] is DataGridViewComboBoxCell))
                        {
                            dvgPLC.Rows[e.RowIndex].Cells[i].ReadOnly = true;
                        }
                        else
                        {
                            dvgPLC.Rows[e.RowIndex].Cells[i].ReadOnly = false;
                        }
                    }
                    dvgPLC.Rows[e.RowIndex].Cells[1].ReadOnly = !IsMonitor;

                }
                else if (IsContainsBitDevice(device) && isDigit) //Bit => cell 2=ComboBox,cell 3=Textbox
                {
                    //Viết hoa
                    dvgPLC.Rows[e.RowIndex].Cells[0].Value = label.ToUpper();
                    //((DataGridViewTextBoxImageCell)dvgPLC.Rows[e.RowIndex].Cells[0]).Image = bmpDevice;
                    DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, e.RowIndex];
                    boxImageCell.PointImageX = 20;
                    boxImageCell.Image = bmpDevice;

                    if (!isCboCell_2)
                    {
                        DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
                        cbocell.Items.Add("BIN");
                        cbocell.Items.Add("Decimal");
                        cbocell.Items.Add("Hexadecimal");
                        cbocell.Value = "BIN";
                        //cbocell.Style.SelectionBackColor = Color.Red;
                        cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        dvgPLC.Rows[e.RowIndex].Cells[2] = cbocell;
                    }
                    else
                    {
                        //Khi sua tu Word sang Bit phải chuyển Decimal  sang BIN
                        DataGridViewComboBoxCell cbocell = dvgPLC.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell;
                        cbocell.Value = "BIN";
                    }
                    if (!isTxtCell_3)
                    {
                        DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
                        txtCell.Value = "Bit";
                        dvgPLC.Rows[e.RowIndex].Cells[3] = txtCell;
                    }
                    dvgPLC.Rows[e.RowIndex].Cells[3].Value = "Bit";

                    dvgPLC.Rows[e.RowIndex].Cells[1].Value = "--";
                    for (int i = 1; i < 4; i++)
                    {
                        if (i != 2)
                        {
                            dvgPLC.Rows[e.RowIndex].Cells[i].ReadOnly = true;
                        }
                        else
                        {
                            dvgPLC.Rows[e.RowIndex].Cells[i].ReadOnly = false;
                        }
                    }
                    dvgPLC.Rows[e.RowIndex].Cells[1].ReadOnly = !IsMonitor;

                }
                else //Không phải Device
                {
                    //((DataGridViewTextBoxImageCell)dvgPLC.Rows[e.RowIndex].Cells[0]).Image = bmpNon;
                    //dvgPLC.Rows[e.RowIndex].Cells[0].Value = label;
                    DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, e.RowIndex];
                    boxImageCell.PointImageX = 20;
                    boxImageCell.Image = bmpNon_temp;

                    if (isCboCell_2)
                    {
                        DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
                        txtCell.Value = "-";
                        dvgPLC.Rows[e.RowIndex].Cells[2] = txtCell;
                    }
                    if (isCboCell_3)
                    {
                        DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
                        txtCell.Value = "--";
                        dvgPLC.Rows[e.RowIndex].Cells[3] = txtCell;
                    }
                    if (isTxtCell_3)
                    {
                        dvgPLC.Rows[e.RowIndex].Cells[3].Value = "----";
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        dvgPLC.Rows[e.RowIndex].Cells[i].Value = "--";
                        dvgPLC.Rows[e.RowIndex].Cells[i].ReadOnly = true;
                    }
                }

            }

            //Chỉnh màu sắc

            if (dvgPLC.Rows[e.RowIndex].Cells[0].Value != null && e.RowIndex == dvgPLC.Rows.Count - 1)
            {

                dvgPLC.Rows.Add();
                DataGridViewTextBoxImageCell boxImageCell = new DataGridViewTextBoxImageCell();
                boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, e.RowIndex + 1];
                boxImageCell.PointImageX = 0;
                boxImageCell.Image = bmpNon;
                for (int i = 1; i < 4; i++)
                {
                    dvgPLC.Rows[e.RowIndex + 1].Cells[i].ReadOnly = true;
                }

            }
            //if (dvgPLC.Rows[e.RowIndex].Cells[0].Value == null && e.RowIndex != dvgPLC.Rows.Count - 1)
            if (isEmpty && e.RowIndex != dvgPLC.Rows.Count - 1)
            {
                try
                {
                    dvgPLC.Rows.RemoveAt(e.RowIndex);
                }
                catch
                {
                    dvgPLC.BeginInvoke(new MethodInvoker(() => { dvgPLC.Rows.RemoveAt(e.RowIndex); }));
                }

            }
            if (e.RowIndex % 2 != 0) // Hàng lẻ
            {
                dvgPLC.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                dvgPLC.InvalidateRow(e.RowIndex);
            }
        }
        private void SetReadOnlyCell(bool enable)
        {
            int currentIndex = dvgPLC.CurrentRow.Index;
            for (int i = 0; i < dvgPLC.RowCount; i++)
            {
                if (dvgPLC.Rows[i].Cells[0].Value != null)
                {
                    string label = dvgPLC.Rows[i].Cells[0].Value.ToString();
                    //Tuỳ chỉnh label X8=> X,8
                    label = sWhitespace.Replace(label, "").ToUpper();
                    string device;
                    int num = 0;
                    bool isDigit = SettingDevice(label, out device, out num);
                    if (IsContainsWordDevice(device) && isDigit)
                    {
                        dvgPLC.Rows[i].Cells[1].ReadOnly = enable;

                    }
                    else if (IsContainsBitDevice(device) && isDigit)
                    {

                        dvgPLC.Rows[i].Cells[1].ReadOnly = enable;
                        if (currentIndex == i && !enable)
                        {
                            onBitToolStripButton.Enabled = !enable;
                            offBitToolStripButton.Enabled = !enable;
                            toggleBitToolStripButton.Enabled = !enable;

                        }
                        else if (currentIndex == i)
                        {
                            onBitToolStripButton.Enabled = enable;
                            offBitToolStripButton.Enabled = enable;
                            toggleBitToolStripButton.Enabled = enable;
                        }
                    }
                }
            }
        }

        private void DvgPLC_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //Khoi nhap nhay khi roi khoi row bit dang ON mau xanh duong (mau trang => xanh duong)
            //cai nay khoi qua buoc mau trang
            try
            {
                if (IsMonitor && dvgPLC.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    string label = dvgPLC.Rows[e.RowIndex].Cells[0].Value.ToString();
                    //Tuỳ chỉnh label X8=> X,8
                    label = sWhitespace.Replace(label, "").ToUpper();
                    string device;
                    int num = 0;
                    bool isDigit = SettingDevice(label, out device, out num);
                    if (IsContainsBitDevice(device) && isDigit)
                    {
                        bool[] coils = PLC.GetCoils(device);
                        if (coils[num])
                        {
                            dvgPLC.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Blue;
                            dvgPLC.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        }
                        else
                        {
                            if (e.RowIndex % 2 != 0)
                            {
                                dvgPLC.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                                dvgPLC.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                            }
                            else
                            {
                                dvgPLC.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                                dvgPLC.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }

                    }
                }
            }
            catch { }
        }

        private void DvgPLC_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (IsMonitor && dvgPLC.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    string label = dvgPLC.Rows[e.RowIndex].Cells[0].Value.ToString();
                    //Tuỳ chỉnh label X8=> X,8
                    label = sWhitespace.Replace(label, "").ToUpper();
                    string device;
                    int num = 0;
                    bool isDigit = SettingDevice(label, out device, out num);
                    if (IsContainsBitDevice(device) && isDigit)
                    {
                        deviceCtmnuClipboardDvgPLC = label;
                        onBitToolStripButton.Enabled = true;
                        offBitToolStripButton.Enabled = true;
                        toggleBitToolStripButton.Enabled = true;
                        if (DsTimers2.ContainsKey(label))
                        {
                            bool isIncrement = DsTimers2[deviceCtmnuClipboardDvgPLC].Item2;
                            incrementToolStripButton.Enabled = !isIncrement;
                            decrementToolStripButton.Enabled = isIncrement;
                            setTimeToolStripSplitButton.Enabled = true;
                            stopcountToolStripButton.Enabled = true;
                        }
                        else
                        {
                            incrementToolStripButton.Enabled = true;
                            decrementToolStripButton.Enabled = true;
                            setTimeToolStripSplitButton.Enabled = false;
                            stopcountToolStripButton.Enabled = false;
                        }
                        return;
                    }
                    else if (IsContainsWordDevice(device) && isDigit)
                    {
                        deviceCtmnuClipboardDvgPLC = label;
                        onBitToolStripButton.Enabled = false;
                        offBitToolStripButton.Enabled = false;
                        toggleBitToolStripButton.Enabled = false;

                        if (DsTimers2.ContainsKey(label))
                        {
                            bool isIncrement = DsTimers2[deviceCtmnuClipboardDvgPLC].Item2;
                            incrementToolStripButton.Enabled = !isIncrement;
                            decrementToolStripButton.Enabled = isIncrement;
                            setTimeToolStripSplitButton.Enabled = true;
                            stopcountToolStripButton.Enabled = true;
                        }
                        else
                        {
                            incrementToolStripButton.Enabled = true;
                            decrementToolStripButton.Enabled = true;
                            setTimeToolStripSplitButton.Enabled = false;
                            stopcountToolStripButton.Enabled = false;
                        }
                        return;
                    }
                    else
                    {
                        incrementToolStripButton.Enabled = false;
                        decrementToolStripButton.Enabled = false;
                        setTimeToolStripSplitButton.Enabled = false;
                        stopcountToolStripButton.Enabled = false;
                        onBitToolStripButton.Enabled = false;
                        offBitToolStripButton.Enabled = false;
                        toggleBitToolStripButton.Enabled = false;
                        return;
                    }
                }
                else
                {
                    onBitToolStripButton.Enabled = false;
                    offBitToolStripButton.Enabled = false;
                    toggleBitToolStripButton.Enabled = false;
                    incrementToolStripButton.Enabled = false;
                    decrementToolStripButton.Enabled = false;
                    setTimeToolStripSplitButton.Enabled = false;
                    stopcountToolStripButton.Enabled = false;
                }
            }
            catch { }
        }
        private void DvgPLC_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            //e.Control.ContextMenuStrip.Items[2].Enabled = false;
            ComboBox cb = e.Control as ComboBox;
            if (cb != null)
            {
                cb.DropDown -= Cb_DropDown;
                cb.DropDown += Cb_DropDown;
                cb.SelectedValueChanged -= Cb_SelectedValueChanged;
                cb.SelectedValueChanged += Cb_SelectedValueChanged;
                return;
            }
            TextBox txtbox = e.Control as TextBox;
            if (txtbox != null)
            {
                txtBox_Cell = txtbox;
            }
        }
        TextBox txtBox_Cell;

        string DisplayFotmart;

        private void Cb_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            dvgPLC.CurrentCell.Value = cb.Text.ToString();
            //
            //String, Float, Time thi ngăn Displayformat lại

            //Nhớ giá trị trước đó
            if (dvgPLC.CurrentCell.ColumnIndex == 3)
            {
                string format = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2].Value.ToString();
                if (format == DataConvertExtendsion.BIN || format == DataConvertExtendsion.Decimal || format == DataConvertExtendsion.Hexadecimal)
                {
                    DisplayFotmart = format;
                }
                else if (DisplayFotmart == null)
                {
                    DisplayFotmart = DataConvertExtendsion.Decimal;
                }
                if (dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[3].Value != null)
                {
                    string dataType = dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[3].Value.ToString();
                    if (dataType == DataConvertExtendsion.FLOAT ||
                        dataType == DataConvertExtendsion.STRING ||
                        dataType == DataConvertExtendsion.TIME)
                    {

                        DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
                        textBoxCell.Value = "--";
                        dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2] = textBoxCell;
                        //cb.DropDown -= Cb_DropDown;
                        // cb.SelectedValueChanged -= Cb_SelectedValueChanged;
                        dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2].ReadOnly = true;
                    }
                    else if (!(dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2] is DataGridViewComboBoxCell))
                    {
                        DataGridViewComboBoxCell cbocell = new DataGridViewComboBoxCell();
                        cbocell.Items.Add("BIN");
                        cbocell.Items.Add("Decimal");
                        cbocell.Items.Add("Hexadecimal");
                        cbocell.Value = DisplayFotmart;
                        //cbocell.Style.SelectionBackColor = Color.Red;
                        cbocell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2] = cbocell;
                        dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[2].ReadOnly = false;

                    }
                }
            }
            //if((dvgPLC.CurrentCell.ColumnIndex == 3 || dvgPLC.CurrentCell.ColumnIndex == 2)&&!IsMonitor)
            //{
            //    dvgPLC.Rows[dvgPLC.CurrentRow.Index].Cells[1].Value = "--";
            //}
        }
        private void Cb_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            senderComboBox.BackColor = Color.White;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in senderComboBox.Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
            senderComboBox.ForeColor = Color.Black;
        }
        private void DvgPLC_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Không được phép xoá dòng cuối cùng <> dòng mới chưa có dữ liệu
            if (e.Row.Index == dvgPLC.Rows.Count - 1)
            {
                e.Cancel = true;
            }
        }

        private void DvgPLC_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ChangeRowColor();
        }

        private void DvgPLC_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                try
                {
                    dvgPLC.CurrentCell = dvgPLC.Rows[e.RowIndex].Cells[0];
                    ChangeRowColor();
                }
                catch { }
            }
        }
        private void DvgPLC_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll && e.NewValue < 38)
            {
                if (e.Type == ScrollEventType.SmallIncrement)
                {
                    for (int i = 0; i < dvgPLC.RowCount; i++)
                    {
                        DataGridViewTextBoxImageCell boxImageCell = (DataGridViewTextBoxImageCell)dvgPLC[0, i];
                        boxImageCell.PointImageX = 20 - e.NewValue;
                        dvgPLC.InvalidateCell(boxImageCell);
                    }
                }
            }
        }
        private void ChangeRowColor()
        {
            for (int i = 0; i < dvgPLC.Rows.Count; i++)
            {
                if (i % 2 != 0) // Hàng lẻ
                {
                    dvgPLC.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(227, 234, 255);
                    dvgPLC.Rows[i].DefaultCellStyle.ForeColor = Color.Black;

                }
                else
                {
                    dvgPLC.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                    dvgPLC.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void DvgPLC_CellEndEditEX(object sender, DataGridViewCellEventArgs e)
        {
            //Xác định loại Device trên mỗi hàng của datagridview
            if (IsMonitor && e.ColumnIndex == 1 && dvgPLC.Rows[e.RowIndex].Cells[0].Value != null && dvgPLC.Rows[e.RowIndex].Cells[1].Value != null)
            {
                string display = dvgPLC.Rows[e.RowIndex].Cells[2].Value.ToString();
                string label = dvgPLC.Rows[e.RowIndex].Cells[0].Value.ToString();
                //Tuỳ chỉnh label X8=> X,8
                label = sWhitespace.Replace(label, "").ToUpper();
                string device;
                int num = 0;
                bool isDigit = SettingDevice(label, out device, out num);
                if (IsContainsWordDevice(device) && isDigit)
                {
                    byte[] buffer = PLC.GetRegisters(device);
                    string dataType = dvgPLC.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string value = dvgPLC.Rows[e.RowIndex].Cells[1].Value.ToString();

                    switch (dataType)
                    {
                        case DataConvertExtendsion.U_WORD:
                            if (display == DataConvertExtendsion.Decimal)
                            {
                                buffer.SetWord<ushort>(value, num);
                            }
                            else if (display == DataConvertExtendsion.BIN)
                            {
                                DataConvertExtendsion.SetBinWord(buffer, value, num);

                            }
                            else if (display == DataConvertExtendsion.Hexadecimal)
                            {
                                DataConvertExtendsion.SetHexWord(buffer, value, num);
                            }
                            break;
                        case DataConvertExtendsion.U_DOUBLEWORD:
                            if (display == DataConvertExtendsion.Decimal)
                            {
                                buffer.SetDoubleWord<uint>(value, num);
                            }
                            else if (display == DataConvertExtendsion.BIN)
                            {
                                buffer.SetBinDWord(value, num);

                            }
                            else if (display == DataConvertExtendsion.Hexadecimal)
                            {
                                buffer.SetHexDWord(value, num);
                            }
                            break;
                        case DataConvertExtendsion.WORD:
                            if (display == DataConvertExtendsion.Decimal)
                            {
                                buffer.SetWord<short>(value, num);
                            }
                            else if (display == DataConvertExtendsion.BIN)
                            {
                                DataConvertExtendsion.SetBinWord(buffer, value, num);

                            }
                            else if (display == DataConvertExtendsion.Hexadecimal)
                            {
                                DataConvertExtendsion.SetHexWord(buffer, value, num);
                            }
                            break;
                        case DataConvertExtendsion.DOUBLEWORD:
                            if (display == DataConvertExtendsion.Decimal)
                            {
                                buffer.SetDoubleWord<int>(value, num);
                            }
                            else if (display == DataConvertExtendsion.BIN)
                            {
                                buffer.SetBinDWord(value, num);

                            }
                            else if (display == DataConvertExtendsion.Hexadecimal)
                            {
                                buffer.SetHexDWord(value, num);
                            }
                            break;
                        case DataConvertExtendsion.FLOAT:
                            buffer.SetDoubleWord<float>(value, num);
                            break;
                        case DataConvertExtendsion.STRING:
                            buffer.SetString(value, num);
                            break;
                        case DataConvertExtendsion.TIME:
                            buffer.SetDatetime(value, num);
                            break;
                        default:
                            break;
                    }
                }
                else if (IsContainsBitDevice(device) && isDigit)
                {
                    bool[] coils = PLC.GetCoils(device);
                    string value = dvgPLC.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (display == DataConvertExtendsion.BIN || display == DataConvertExtendsion.Decimal)
                    {
                        bool coil;
                        int dec_coil = 0;
                        value = sWhitespace.Replace(value, "").ToUpper();
                        if (bool.TryParse(value, out coil))
                        {
                            coils[num] = coil;
                            return;
                        }
                        else if (int.TryParse(value, out dec_coil))
                        {
                            if (dec_coil == 0)
                                coils[num] = false;
                            else if (dec_coil == 1)
                                coils[num] = true;
                        }
                        return;
                    }
                    else if (display == DataConvertExtendsion.Hexadecimal)
                    {
                        int dec_coil = 0;
                        value = sWhitespace.Replace(value, "").ToUpper();
                        if (value[0] == 'H')
                        {
                            if (int.TryParse(value.Substring(1, value.Length - 1), out dec_coil))
                            {
                                if (dec_coil == 0)
                                    coils[num] = false;
                                else if (dec_coil == 1)
                                    coils[num] = true;
                                return;
                            }
                        }
                        if (int.TryParse(value, out dec_coil))
                        {
                            if (dec_coil == 0)
                                coils[num] = false;
                            else if (dec_coil == 1)
                                coils[num] = true;
                        }
                    }
                }
            }

        }
        private StringBuilder stringBuilder = new StringBuilder();
        private void SaveDvg()
        {
            if (Properties.Settings.Default.dvgPLC == null) Properties.Settings.Default.dvgPLC = new System.Collections.Specialized.StringCollection();
            var dvg = Properties.Settings.Default.dvgPLC;
            dvg.Clear();
            int rows = dvgPLC.Rows.Count;
            if (rows != 1)
            {
                for (int i = 0; i < rows - 1; i++)
                {
                    stringBuilder.Clear();
                    for (int j = 0; j < 4; j++)
                    {
                        if (j == 1)
                        {
                            stringBuilder.Append("--" + ',');
                        }
                        else
                        {
                            stringBuilder.Append(dvgPLC[j, i].Value.ToString() + ',');
                        }
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    dvg.Add(stringBuilder.ToString());
                }
            }
            Properties.Settings.Default.Save();
        }
        private void SaveDvgWatch()
        {
            lsDvgWatch.Clear();
            int rows = dvgPLC.Rows.Count;
            if (rows > 1)
            {
                for (int i = 0; i < rows - 1; i++)
                {
                    DvgWatch watch = new DvgWatch()
                    {
                        Name = dvgPLC[0, i].Value.ToString(),
                        CurrentValue = dvgPLC[1, i].Value.ToString(),
                        DisplayFormat = dvgPLC[2, i].Value.ToString(),
                        DataType = dvgPLC[3, i].Value.ToString()
                    };
                    lsDvgWatch.Add(watch);
                }
            }

            CreateJson(lsDvgWatch, pathWatch);//Tạo file Setting Json khi chưa được tạo, nếu có rồi thì thôi
        }
        private void PasteDvg()
        {
            var dvg = Properties.Settings.Default.dvgPLC;
            if (dvg == null) return;
            int length = dvg.Count;
            string[] ArrStrRow_Filter = new string[length];
            for (int i = 0; i < length; i++)
            {
                ArrStrRow_Filter[i] = dvg[i].ToString();
            }
            try //Nếu copy paste hàng bình thường
            {
                for (int i = 0; i < ArrStrRow_Filter.Length; i++)
                {
                    CustomGridView_PasteData(ArrStrRow_Filter[i].Split(','));
                }
            }
            catch { }
        }

        private void PasteWatch(List<DvgWatch> watches)
        {
            if (watches == null || watches.Count == 0) return;
            foreach (DvgWatch watch in watches)
            {
                try
                {
                    CustomGridView_PasteDataWatch(watch);
                }
                catch { }
            }
        }

        public void Alert(string msg, frmAlert.enmType type)
        {
            frmAlert frm = new frmAlert();
            frm.showAlert(msg, type);
        }

        private void TimerLedRUN_Tick(object sender, EventArgs e)
        {
            fx5uCpu1.RUN = !fx5uCpu1.RUN; //Pause
        }

        private void SetErrorToolStripMenuItemFX5U_Click(object sender, EventArgs e)
        {
            if (!IsMonitor) return;
            timerErr.Enabled = !timerErr.Enabled;
            if (!timerErr.Enabled)
            {
                fx5uCpu1.ERR = false;
            }
        }

        private void ResetToolStripMenuItemFX5U_Click(object sender, EventArgs e)
        {
            if (!IsMonitor) return;
            PLC.Memory.Clear();
        }

        private void StopToolStripMenuItemFX5U_Click(object sender, EventArgs e)
        {
            if (!IsMonitor) return;
            timerLedRUN.Stop();
            fx5uCpu1.RUN = false;
        }

        private void PauseToolStripMenuItemFX5U_Click(object sender, EventArgs e)
        {
            if (!IsMonitor) return;
            timerLedRUN.Start();
        }

        private void RunToolStripMenuItemFX5U_Click(object sender, EventArgs e)
        {
            if (!IsMonitor) return;
            timerLedRUN.Stop();
            fx5uCpu1.RUN = true;
        }

        private void TimerErr_Tick(object sender, EventArgs e)
        {
            fx5uCpu1.ERR = !fx5uCpu1.ERR;
        }

        private void PLC_NewConnection(object sender, ConnectEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                try
                {
                    ChangeTextNorifyStart(PLC.ListClients.Count);
                    clientsTssl.Text = $"Clients: {PLC.ListClients.Count}";
                    if (settingJson.StatusOptions[1])
                    {
                        Alert(e.SocketClient.Client.RemoteEndPoint.ToString(), frmAlert.enmType.Success);
                        SystemSounds.Exclamation.Play();
                    }
                    // Thêm đối client kết nối vào danh sách _frmListClients

                    stringBuilderConnect.AppendLine(e.SocketClient.Client.RemoteEndPoint.ToString());
                    _frmListClients.Text = "List of connected clients: #" + PLC.ListClients.Count;
                    _frmListClients.txtListClients.Text = stringBuilderConnect.ToString().TrimEnd('\n');
                    _frmListClients.txtListClients.SelectionStart = _frmListClients.txtListClients.Text.Length;
                }
                catch { }
            }));
        }

        private void PLC_LostConnection(object sender, ConnectEventArgs e)
        {
            //Vòng try catch ngoài cùng khi nhấn exit ở tray trong khi các frmAlert còn show thì nó sẽ báo lỗi
            try
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        if (IsMonitor)
                        {
                            ChangeTextNorifyStart(PLC.ListClients.Count - 1); //Vì lệnh này kết thúc thì mới xoá client liện tại khỏi danh sách trong PLC
                            clientsTssl.Text = $"Clients: {PLC.ListClients.Count - 1}";
                            // xoá đối tượng client kết nối vào danh sách _frmListClients
                            stringBuilderConnect.Replace(e.SocketClient.Client.RemoteEndPoint.ToString() + Environment.NewLine, "");
                            _frmListClients.Text = "List of connected clients: #" + (PLC.ListClients.Count - 1);
                            _frmListClients.txtListClients.Text = stringBuilderConnect.ToString().TrimEnd('\n');
                            _frmListClients.txtListClients.SelectionStart = stringBuilderConnect.Length;
                        }
                        else
                            ChangeTextNorifyStop();
                        if (settingJson.StatusOptions[1])
                        {
                            Alert(e.SocketClient.Client.RemoteEndPoint.ToString(), frmAlert.enmType.Error);
                            SystemSounds.Hand.Play();
                        }

                    }
                    catch { }
                }));
            }
            catch { }
        }

        private void PLC_ChangeStatus(object sender, StatusEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                switch (e.Command)
                {
                    case Command.REMOTE_RUN:
                        timerLedRUN.Stop();
                        fx5uCpu1.RUN = true;
                        break;
                    case Command.REMOTE_PAUSE:
                        timerLedRUN.Start();
                        break;
                    case Command.REMOTE_STOP:
                        timerLedRUN.Stop();
                        fx5uCpu1.RUN = false;
                        break;
                    case Command.REMOTE_RESET:
                        PLC.Memory.Clear();
                        break;
                    case Command.CLEAR_ERROR:
                        timerErr.Enabled = false;
                        fx5uCpu1.ERR = false;
                        break;
                }
            }));
        }
        private void PLC_Error(object sender, ErrorCommEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                timerErr.Enabled = true;
            }));
        }

        public void SettingStatusStrip()
        {
            noticeTssl.Enabled = settingJson.StatusOptions[1]; //notice
            trayTssl.Enabled = settingJson.StatusOptions[2];//System tray
            saveWatchTssl.Enabled = settingJson.StatusOptions[3]; //Save watch
        }

    }
}
