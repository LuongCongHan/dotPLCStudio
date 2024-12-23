namespace dotPLC
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStripBottom = new System.Windows.Forms.StatusStrip();
            this.noticeTssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.trayTssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveWatchTssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.clientsTssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitTssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.dvgPLC = new System.Windows.Forms.DataGridView();
            this.clmName = new dotPLC.CustomControl.DataGridViewTextAndImage.DataGridViewTextBoxImageColumn();
            this.clmCurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmnuClipboardDvgPLC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.pastToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.incrementToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.decrementToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.settimeToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.settimetoolStripComboBoxDvgPLC = new System.Windows.Forms.ToolStripComboBox();
            this.stopToolStripMenuItemDvgPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripWatch = new dotPLC.CustomControl.ToolStripHelper.ToolStripEx();
            this.onBitToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.offBitToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleBitToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.updateToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.setTimeToolStripSplitButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripSplitButtonEx();
            this.settimeToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.stopcountToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.decrementToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.incrementToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCloseBottom = new System.Windows.Forms.Button();
            this.lblBottom = new System.Windows.Forms.Label();
            this.splitterBottom = new System.Windows.Forms.Splitter();
            this.panelMain = new System.Windows.Forms.Panel();
            this.fx5uCpu1 = new dotPLC.CustomControl.Fx5uCpu();
            this.ctmnuCPU = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runToolStripMenuItemFX5U = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItemFX5U = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItemFX5U = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItemFX5U = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.setErrorToolStripMenuItemFX5U = new System.Windows.Forms.ToolStripMenuItem();
            this.timerStartMonitor = new System.Windows.Forms.Timer(this.components);
            this.timerLAN = new System.Windows.Forms.Timer(this.components);
            this.timerIO = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxImageColumn1 = new dotPLC.CustomControl.DataGridViewTextAndImage.DataGridViewTextBoxImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripTop = new dotPLC.CustomControl.ToolStripHelper.ToolStripEx();
            this.cutToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.copyToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.pasteToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.deleteToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.optionToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.stopserverToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.startserverToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CpuTypeToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.clearToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.networkToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.deviceMemoryToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.watchToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.listClientsToolStripButton = new dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmnuTextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoToolStripMenuItemTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItemTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItemTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItemTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItemTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItemTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.startRuntimeServiceToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRuntimeServiceToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMemoryToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.listOfClientsToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceBufferMemoryToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.networkConfigurationToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItemNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconPLC = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerLedRUN = new System.Windows.Forms.Timer(this.components);
            this.timerErr = new System.Windows.Forms.Timer(this.components);
            this.statusStripBottom.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPLC)).BeginInit();
            this.ctmnuClipboardDvgPLC.SuspendLayout();
            this.toolStripWatch.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ctmnuCPU.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.ctmnuTextBox.SuspendLayout();
            this.ctmnuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripBottom
            // 
            this.statusStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noticeTssl,
            this.toolStripStatusLabel2,
            this.trayTssl,
            this.toolStripStatusLabel4,
            this.saveWatchTssl,
            this.toolStripStatusLabel6,
            this.clientsTssl,
            this.splitTssl,
            this.statusTssl});
            this.statusStripBottom.Location = new System.Drawing.Point(0, 649);
            this.statusStripBottom.Name = "statusStripBottom";
            this.statusStripBottom.Size = new System.Drawing.Size(551, 22);
            this.statusStripBottom.TabIndex = 1;
            this.statusStripBottom.Text = "statusStrip1";
            // 
            // noticeTssl
            // 
            this.noticeTssl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.noticeTssl.Name = "noticeTssl";
            this.noticeTssl.Size = new System.Drawing.Size(105, 17);
            this.noticeTssl.Text = "Notice connection";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // trayTssl
            // 
            this.trayTssl.Name = "trayTssl";
            this.trayTssl.Size = new System.Drawing.Size(93, 17);
            this.trayTssl.Text = "Minimize to tray";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // saveWatchTssl
            // 
            this.saveWatchTssl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveWatchTssl.Name = "saveWatchTssl";
            this.saveWatchTssl.Size = new System.Drawing.Size(66, 17);
            this.saveWatchTssl.Text = "Save watch";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabel6.Spring = true;
            // 
            // clientsTssl
            // 
            this.clientsTssl.Name = "clientsTssl";
            this.clientsTssl.Size = new System.Drawing.Size(55, 17);
            this.clientsTssl.Text = "Clients: 2";
            // 
            // splitTssl
            // 
            this.splitTssl.Name = "splitTssl";
            this.splitTssl.Size = new System.Drawing.Size(10, 17);
            this.splitTssl.Text = "|";
            // 
            // statusTssl
            // 
            this.statusTssl.Name = "statusTssl";
            this.statusTssl.Size = new System.Drawing.Size(136, 17);
            this.statusTssl.Text = "IP = 127.0.0.1, Port = 502";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.dvgPLC);
            this.panelBottom.Controls.Add(this.toolStripWatch);
            this.panelBottom.Controls.Add(this.btnCloseBottom);
            this.panelBottom.Controls.Add(this.lblBottom);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 374);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(551, 275);
            this.panelBottom.TabIndex = 2;
            // 
            // dvgPLC
            // 
            this.dvgPLC.AllowUserToAddRows = false;
            this.dvgPLC.AllowUserToResizeRows = false;
            this.dvgPLC.BackgroundColor = System.Drawing.Color.White;
            this.dvgPLC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgPLC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgPLC.ColumnHeadersHeight = 26;
            this.dvgPLC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dvgPLC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmName,
            this.clmCurrentValue,
            this.clmDisplay,
            this.clmDataType});
            this.dvgPLC.ContextMenuStrip = this.ctmnuClipboardDvgPLC;
            this.dvgPLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgPLC.EnableHeadersVisualStyles = false;
            this.dvgPLC.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.dvgPLC.Location = new System.Drawing.Point(0, 47);
            this.dvgPLC.Name = "dvgPLC";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgPLC.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dvgPLC.RowHeadersVisible = false;
            this.dvgPLC.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dvgPLC.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F);
            this.dvgPLC.RowTemplate.Height = 19;
            this.dvgPLC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgPLC.Size = new System.Drawing.Size(551, 228);
            this.dvgPLC.TabIndex = 7;
            // 
            // clmName
            // 
            this.clmName.HeaderText = "Name";
            this.clmName.Image = null;
            this.clmName.Name = "clmName";
            this.clmName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmCurrentValue
            // 
            this.clmCurrentValue.HeaderText = "Current Value";
            this.clmCurrentValue.Name = "clmCurrentValue";
            this.clmCurrentValue.Width = 150;
            // 
            // clmDisplay
            // 
            this.clmDisplay.HeaderText = "Display Format";
            this.clmDisplay.Name = "clmDisplay";
            // 
            // clmDataType
            // 
            this.clmDataType.HeaderText = "Data Type";
            this.clmDataType.Name = "clmDataType";
            this.clmDataType.Width = 200;
            // 
            // ctmnuClipboardDvgPLC
            // 
            this.ctmnuClipboardDvgPLC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItemDvgPLC,
            this.copyToolStripMenuItemDvgPLC,
            this.pastToolStripMenuItemDvgPLC,
            this.deleteToolStripMenuItemDvgPLC,
            this.toolStripMenuItem4,
            this.selectAllToolStripMenuItemDvgPLC,
            this.toolStripMenuItem2,
            this.incrementToolStripMenuItemDvgPLC,
            this.decrementToolStripMenuItemDvgPLC,
            this.settimeToolStripMenuItemDvgPLC,
            this.stopToolStripMenuItemDvgPLC});
            this.ctmnuClipboardDvgPLC.Name = "ctmnuClipboardDvgPLC";
            this.ctmnuClipboardDvgPLC.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ctmnuClipboardDvgPLC.Size = new System.Drawing.Size(165, 214);
            // 
            // cutToolStripMenuItemDvgPLC
            // 
            this.cutToolStripMenuItemDvgPLC.Name = "cutToolStripMenuItemDvgPLC";
            this.cutToolStripMenuItemDvgPLC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.cutToolStripMenuItemDvgPLC.Text = "Cut";
            // 
            // copyToolStripMenuItemDvgPLC
            // 
            this.copyToolStripMenuItemDvgPLC.Name = "copyToolStripMenuItemDvgPLC";
            this.copyToolStripMenuItemDvgPLC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.copyToolStripMenuItemDvgPLC.Text = "&Copy";
            // 
            // pastToolStripMenuItemDvgPLC
            // 
            this.pastToolStripMenuItemDvgPLC.Name = "pastToolStripMenuItemDvgPLC";
            this.pastToolStripMenuItemDvgPLC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pastToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.pastToolStripMenuItemDvgPLC.Text = "Paste";
            // 
            // deleteToolStripMenuItemDvgPLC
            // 
            this.deleteToolStripMenuItemDvgPLC.Name = "deleteToolStripMenuItemDvgPLC";
            this.deleteToolStripMenuItemDvgPLC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItemDvgPLC.Text = "&Delete";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(161, 6);
            // 
            // selectAllToolStripMenuItemDvgPLC
            // 
            this.selectAllToolStripMenuItemDvgPLC.Name = "selectAllToolStripMenuItemDvgPLC";
            this.selectAllToolStripMenuItemDvgPLC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItemDvgPLC.Text = "Select &All";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 6);
            // 
            // incrementToolStripMenuItemDvgPLC
            // 
            this.incrementToolStripMenuItemDvgPLC.Name = "incrementToolStripMenuItemDvgPLC";
            this.incrementToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.incrementToolStripMenuItemDvgPLC.Text = "Increment";
            // 
            // decrementToolStripMenuItemDvgPLC
            // 
            this.decrementToolStripMenuItemDvgPLC.Name = "decrementToolStripMenuItemDvgPLC";
            this.decrementToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.decrementToolStripMenuItemDvgPLC.Text = "Decrement";
            // 
            // settimeToolStripMenuItemDvgPLC
            // 
            this.settimeToolStripMenuItemDvgPLC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settimetoolStripComboBoxDvgPLC});
            this.settimeToolStripMenuItemDvgPLC.Name = "settimeToolStripMenuItemDvgPLC";
            this.settimeToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.settimeToolStripMenuItemDvgPLC.Text = "Set Interval";
            // 
            // settimetoolStripComboBoxDvgPLC
            // 
            this.settimetoolStripComboBoxDvgPLC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settimetoolStripComboBoxDvgPLC.DropDownWidth = 75;
            this.settimetoolStripComboBoxDvgPLC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.settimetoolStripComboBoxDvgPLC.IntegralHeight = false;
            this.settimetoolStripComboBoxDvgPLC.Name = "settimetoolStripComboBoxDvgPLC";
            this.settimetoolStripComboBoxDvgPLC.Size = new System.Drawing.Size(75, 23);
            // 
            // stopToolStripMenuItemDvgPLC
            // 
            this.stopToolStripMenuItemDvgPLC.Name = "stopToolStripMenuItemDvgPLC";
            this.stopToolStripMenuItemDvgPLC.Size = new System.Drawing.Size(164, 22);
            this.stopToolStripMenuItemDvgPLC.Text = "Stop";
            // 
            // toolStripWatch
            // 
            this.toolStripWatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.toolStripWatch.ClickThrough = true;
            this.toolStripWatch.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripWatch.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripWatch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onBitToolStripButton,
            this.toolStripSeparator8,
            this.offBitToolStripButton,
            this.toolStripSeparator9,
            this.toggleBitToolStripButton,
            this.toolStripSeparator10,
            this.updateToolStripButton,
            this.setTimeToolStripSplitButton,
            this.toolStripSeparator4,
            this.stopcountToolStripButton,
            this.toolStripSeparator6,
            this.decrementToolStripButton,
            this.toolStripSeparator7,
            this.incrementToolStripButton,
            this.toolStripSeparator5});
            this.toolStripWatch.Location = new System.Drawing.Point(0, 22);
            this.toolStripWatch.Name = "toolStripWatch";
            this.toolStripWatch.Size = new System.Drawing.Size(551, 25);
            this.toolStripWatch.Stretch = true;
            this.toolStripWatch.TabIndex = 6;
            // 
            // onBitToolStripButton
            // 
            this.onBitToolStripButton.Enabled = false;
            this.onBitToolStripButton.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onBitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("onBitToolStripButton.Image")));
            this.onBitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onBitToolStripButton.Name = "onBitToolStripButton";
            this.onBitToolStripButton.Size = new System.Drawing.Size(46, 22);
            this.onBitToolStripButton.Text = "ON";
            this.onBitToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // offBitToolStripButton
            // 
            this.offBitToolStripButton.Enabled = false;
            this.offBitToolStripButton.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offBitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("offBitToolStripButton.Image")));
            this.offBitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.offBitToolStripButton.Name = "offBitToolStripButton";
            this.offBitToolStripButton.Size = new System.Drawing.Size(49, 22);
            this.offBitToolStripButton.Text = "OFF";
            this.offBitToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toggleBitToolStripButton
            // 
            this.toggleBitToolStripButton.Enabled = false;
            this.toggleBitToolStripButton.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleBitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("toggleBitToolStripButton.Image")));
            this.toggleBitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleBitToolStripButton.Name = "toggleBitToolStripButton";
            this.toggleBitToolStripButton.Size = new System.Drawing.Size(109, 22);
            this.toggleBitToolStripButton.Text = "ON/OFF toggle";
            this.toggleBitToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // updateToolStripButton
            // 
            this.updateToolStripButton.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("updateToolStripButton.Image")));
            this.updateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateToolStripButton.Name = "updateToolStripButton";
            this.updateToolStripButton.Size = new System.Drawing.Size(66, 22);
            this.updateToolStripButton.Text = "Update";
            this.updateToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // setTimeToolStripSplitButton
            // 
            this.setTimeToolStripSplitButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.setTimeToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settimeToolStripComboBox});
            this.setTimeToolStripSplitButton.Enabled = false;
            this.setTimeToolStripSplitButton.Image = global::dotPLC.Properties.Resources.settime;
            this.setTimeToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setTimeToolStripSplitButton.Name = "setTimeToolStripSplitButton";
            this.setTimeToolStripSplitButton.Size = new System.Drawing.Size(79, 22);
            this.setTimeToolStripSplitButton.Text = "Interval";
            this.setTimeToolStripSplitButton.ToolTipText = "Set Interval";
            // 
            // settimeToolStripComboBox
            // 
            this.settimeToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settimeToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.settimeToolStripComboBox.Name = "settimeToolStripComboBox";
            this.settimeToolStripComboBox.Size = new System.Drawing.Size(75, 23);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // stopcountToolStripButton
            // 
            this.stopcountToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stopcountToolStripButton.Enabled = false;
            this.stopcountToolStripButton.Image = global::dotPLC.Properties.Resources.stopcount;
            this.stopcountToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopcountToolStripButton.Name = "stopcountToolStripButton";
            this.stopcountToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.stopcountToolStripButton.Text = "Stop";
            this.stopcountToolStripButton.ToolTipText = "Stop Count";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // decrementToolStripButton
            // 
            this.decrementToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.decrementToolStripButton.Enabled = false;
            this.decrementToolStripButton.Image = global::dotPLC.Properties.Resources.down;
            this.decrementToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.decrementToolStripButton.Name = "decrementToolStripButton";
            this.decrementToolStripButton.Size = new System.Drawing.Size(39, 22);
            this.decrementToolStripButton.Text = "-1";
            this.decrementToolStripButton.ToolTipText = "Decrement -1";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // incrementToolStripButton
            // 
            this.incrementToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.incrementToolStripButton.Enabled = false;
            this.incrementToolStripButton.Image = global::dotPLC.Properties.Resources.up;
            this.incrementToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.incrementToolStripButton.Name = "incrementToolStripButton";
            this.incrementToolStripButton.Size = new System.Drawing.Size(42, 22);
            this.incrementToolStripButton.Text = "+1";
            this.incrementToolStripButton.ToolTipText = "Increment +1";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCloseBottom
            // 
            this.btnCloseBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.btnCloseBottom.FlatAppearance.BorderSize = 0;
            this.btnCloseBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseBottom.ForeColor = System.Drawing.Color.White;
            this.btnCloseBottom.Location = new System.Drawing.Point(527, 0);
            this.btnCloseBottom.Name = "btnCloseBottom";
            this.btnCloseBottom.Size = new System.Drawing.Size(21, 19);
            this.btnCloseBottom.TabIndex = 4;
            this.btnCloseBottom.Text = "X";
            this.btnCloseBottom.UseVisualStyleBackColor = false;
            // 
            // lblBottom
            // 
            this.lblBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBottom.ForeColor = System.Drawing.Color.White;
            this.lblBottom.Location = new System.Drawing.Point(0, 0);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(551, 22);
            this.lblBottom.TabIndex = 2;
            this.lblBottom.Text = "Watch";
            this.lblBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitterBottom
            // 
            this.splitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterBottom.Location = new System.Drawing.Point(0, 371);
            this.splitterBottom.Name = "splitterBottom";
            this.splitterBottom.Size = new System.Drawing.Size(551, 3);
            this.splitterBottom.TabIndex = 3;
            this.splitterBottom.TabStop = false;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(153)))), ((int)(((byte)(174)))));
            this.panelMain.Controls.Add(this.fx5uCpu1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(551, 346);
            this.panelMain.TabIndex = 4;
            // 
            // fx5uCpu1
            // 
            this.fx5uCpu1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fx5uCpu1.BackColor = System.Drawing.Color.Transparent;
            this.fx5uCpu1.BackgroundImage = global::dotPLC.Properties.Resources.fx5u_Transparent;
            this.fx5uCpu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fx5uCpu1.BAT = false;
            this.fx5uCpu1.ContextMenuStrip = this.ctmnuCPU;
            this.fx5uCpu1.ERR = false;
            this.fx5uCpu1.LAN = false;
            this.fx5uCpu1.Location = new System.Drawing.Point(11, 10);
            this.fx5uCpu1.mDoubleBuffered = true;
            this.fx5uCpu1.Name = "fx5uCpu1";
            this.fx5uCpu1.PWR = false;
            this.fx5uCpu1.RUN = false;
            this.fx5uCpu1.Size = new System.Drawing.Size(526, 329);
            this.fx5uCpu1.TabIndex = 0;
            this.fx5uCpu1.X = new bool[] {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false};
            this.fx5uCpu1.Y = new bool[] {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false};
            // 
            // ctmnuCPU
            // 
            this.ctmnuCPU.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItemFX5U,
            this.pauseToolStripMenuItemFX5U,
            this.stopToolStripMenuItemFX5U,
            this.resetToolStripMenuItemFX5U,
            this.toolStripMenuItem9,
            this.setErrorToolStripMenuItemFX5U});
            this.ctmnuCPU.Name = "ctmnuCPU";
            this.ctmnuCPU.Size = new System.Drawing.Size(119, 120);
            // 
            // runToolStripMenuItemFX5U
            // 
            this.runToolStripMenuItemFX5U.Name = "runToolStripMenuItemFX5U";
            this.runToolStripMenuItemFX5U.Size = new System.Drawing.Size(118, 22);
            this.runToolStripMenuItemFX5U.Text = "Run";
            // 
            // pauseToolStripMenuItemFX5U
            // 
            this.pauseToolStripMenuItemFX5U.Name = "pauseToolStripMenuItemFX5U";
            this.pauseToolStripMenuItemFX5U.Size = new System.Drawing.Size(118, 22);
            this.pauseToolStripMenuItemFX5U.Text = "Pause";
            // 
            // stopToolStripMenuItemFX5U
            // 
            this.stopToolStripMenuItemFX5U.Name = "stopToolStripMenuItemFX5U";
            this.stopToolStripMenuItemFX5U.Size = new System.Drawing.Size(118, 22);
            this.stopToolStripMenuItemFX5U.Text = "Stop";
            // 
            // resetToolStripMenuItemFX5U
            // 
            this.resetToolStripMenuItemFX5U.Name = "resetToolStripMenuItemFX5U";
            this.resetToolStripMenuItemFX5U.Size = new System.Drawing.Size(118, 22);
            this.resetToolStripMenuItemFX5U.Text = "Reset";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(115, 6);
            // 
            // setErrorToolStripMenuItemFX5U
            // 
            this.setErrorToolStripMenuItemFX5U.Name = "setErrorToolStripMenuItemFX5U";
            this.setErrorToolStripMenuItemFX5U.Size = new System.Drawing.Size(118, 22);
            this.setErrorToolStripMenuItemFX5U.Text = "Set Error";
            // 
            // timerStartMonitor
            // 
            this.timerStartMonitor.Interval = 10;
            // 
            // timerLAN
            // 
            this.timerLAN.Interval = 200;
            // 
            // timerIO
            // 
            this.timerIO.Interval = 50;
            // 
            // dataGridViewTextBoxImageColumn1
            // 
            this.dataGridViewTextBoxImageColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxImageColumn1.Image = null;
            this.dataGridViewTextBoxImageColumn1.Name = "dataGridViewTextBoxImageColumn1";
            this.dataGridViewTextBoxImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Current Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Display Format";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTop.ClickThrough = true;
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator1,
            this.aboutToolStripButton,
            this.optionToolStripButton,
            this.toolStripLabel1,
            this.stopserverToolStripButton,
            this.startserverToolStripButton,
            this.toolStripSeparator2,
            this.CpuTypeToolStripComboBox,
            this.toolStripLabel4,
            this.clearToolStripButton,
            this.networkToolStripButton,
            this.toolStripSeparator11,
            this.deviceMemoryToolStripButton,
            this.watchToolStripButton,
            this.listClientsToolStripButton});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(551, 25);
            this.toolStripTop.TabIndex = 0;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = global::dotPLC.Properties.Resources.cut;
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = global::dotPLC.Properties.Resources.copy;
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = global::dotPLC.Properties.Resources.paste;
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "Paste";
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // aboutToolStripButton
            // 
            this.aboutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aboutToolStripButton.Image = global::dotPLC.Properties.Resources.About;
            this.aboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripButton.Name = "aboutToolStripButton";
            this.aboutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.aboutToolStripButton.Text = "About";
            // 
            // optionToolStripButton
            // 
            this.optionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optionToolStripButton.Image = global::dotPLC.Properties.Resources.option;
            this.optionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionToolStripButton.Name = "optionToolStripButton";
            this.optionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optionToolStripButton.Text = "Options";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Enabled = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.toolStripLabel1.Text = " ";
            // 
            // stopserverToolStripButton
            // 
            this.stopserverToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stopserverToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopserverToolStripButton.Enabled = false;
            this.stopserverToolStripButton.Image = global::dotPLC.Properties.Resources.stop;
            this.stopserverToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopserverToolStripButton.Name = "stopserverToolStripButton";
            this.stopserverToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.stopserverToolStripButton.Text = "Stop Monitoring";
            // 
            // startserverToolStripButton
            // 
            this.startserverToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.startserverToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startserverToolStripButton.Image = global::dotPLC.Properties.Resources.start;
            this.startserverToolStripButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.startserverToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startserverToolStripButton.Name = "startserverToolStripButton";
            this.startserverToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.startserverToolStripButton.Text = "Start Monitoring";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // CpuTypeToolStripComboBox
            // 
            this.CpuTypeToolStripComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CpuTypeToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CpuTypeToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.CpuTypeToolStripComboBox.Name = "CpuTypeToolStripComboBox";
            this.CpuTypeToolStripComboBox.Size = new System.Drawing.Size(135, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(10, 22);
            this.toolStripLabel4.Text = " ";
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearToolStripButton.Image = global::dotPLC.Properties.Resources.cleardata;
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.clearToolStripButton.Text = "Clear Memory";
            // 
            // networkToolStripButton
            // 
            this.networkToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.networkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.networkToolStripButton.Image = global::dotPLC.Properties.Resources.net;
            this.networkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.networkToolStripButton.Name = "networkToolStripButton";
            this.networkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.networkToolStripButton.Text = "Network Configuration";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // deviceMemoryToolStripButton
            // 
            this.deviceMemoryToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.deviceMemoryToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deviceMemoryToolStripButton.Image = global::dotPLC.Properties.Resources.devicelist;
            this.deviceMemoryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deviceMemoryToolStripButton.Name = "deviceMemoryToolStripButton";
            this.deviceMemoryToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deviceMemoryToolStripButton.Text = "Device/Buffer Memory";
            // 
            // watchToolStripButton
            // 
            this.watchToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.watchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.watchToolStripButton.Image = global::dotPLC.Properties.Resources.watch;
            this.watchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.watchToolStripButton.Name = "watchToolStripButton";
            this.watchToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.watchToolStripButton.Text = "Watch";
            // 
            // listClientsToolStripButton
            // 
            this.listClientsToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.listClientsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.listClientsToolStripButton.Image = global::dotPLC.Properties.Resources.Iplist;
            this.listClientsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.listClientsToolStripButton.Name = "listClientsToolStripButton";
            this.listClientsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.listClientsToolStripButton.Text = "List of Clients";
            this.listClientsToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Data Type";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // ctmnuTextBox
            // 
            this.ctmnuTextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItemTextBox,
            this.toolStripMenuItem1,
            this.cutToolStripMenuItemTextBox,
            this.copyToolStripMenuItemTextBox,
            this.pasteToolStripMenuItemTextBox,
            this.deleteToolStripMenuItemTextBox,
            this.toolStripMenuItem3,
            this.selectAllToolStripMenuItemTextBox});
            this.ctmnuTextBox.Name = "ctmnuTextBox";
            this.ctmnuTextBox.Size = new System.Drawing.Size(165, 148);
            // 
            // undoToolStripMenuItemTextBox
            // 
            this.undoToolStripMenuItemTextBox.Name = "undoToolStripMenuItemTextBox";
            this.undoToolStripMenuItemTextBox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItemTextBox.Size = new System.Drawing.Size(164, 22);
            this.undoToolStripMenuItemTextBox.Text = "&Undo";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // cutToolStripMenuItemTextBox
            // 
            this.cutToolStripMenuItemTextBox.Name = "cutToolStripMenuItemTextBox";
            this.cutToolStripMenuItemTextBox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItemTextBox.Size = new System.Drawing.Size(164, 22);
            this.cutToolStripMenuItemTextBox.Text = "Cut";
            // 
            // copyToolStripMenuItemTextBox
            // 
            this.copyToolStripMenuItemTextBox.Name = "copyToolStripMenuItemTextBox";
            this.copyToolStripMenuItemTextBox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItemTextBox.Size = new System.Drawing.Size(164, 22);
            this.copyToolStripMenuItemTextBox.Text = "&Copy";
            // 
            // pasteToolStripMenuItemTextBox
            // 
            this.pasteToolStripMenuItemTextBox.Name = "pasteToolStripMenuItemTextBox";
            this.pasteToolStripMenuItemTextBox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItemTextBox.Size = new System.Drawing.Size(164, 22);
            this.pasteToolStripMenuItemTextBox.Text = "Paste";
            // 
            // deleteToolStripMenuItemTextBox
            // 
            this.deleteToolStripMenuItemTextBox.Name = "deleteToolStripMenuItemTextBox";
            this.deleteToolStripMenuItemTextBox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteToolStripMenuItemTextBox.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItemTextBox.Text = "&Delete";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(161, 6);
            // 
            // selectAllToolStripMenuItemTextBox
            // 
            this.selectAllToolStripMenuItemTextBox.Name = "selectAllToolStripMenuItemTextBox";
            this.selectAllToolStripMenuItemTextBox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItemTextBox.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItemTextBox.Text = "Select &All";
            // 
            // ctmnuTray
            // 
            this.ctmnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItemNotify,
            this.toolStripMenuItem6,
            this.startRuntimeServiceToolStripMenuItemNotify,
            this.stopRuntimeServiceToolStripMenuItemNotify,
            this.clearMemoryToolStripMenuItemNotify,
            this.toolStripMenuItem5,
            this.listOfClientsToolStripMenuItemNotify,
            this.deviceBufferMemoryToolStripMenuItemNotify,
            this.networkConfigurationToolStripMenuItemNotify,
            this.toolStripMenuItem8,
            this.optionsToolStripMenuItemNotify,
            this.aboutToolStripMenuItemNotify,
            this.toolStripMenuItem7,
            this.exitToolStripMenuItemNotify});
            this.ctmnuTray.Name = "ctmnuTray";
            this.ctmnuTray.Size = new System.Drawing.Size(197, 248);
            // 
            // openToolStripMenuItemNotify
            // 
            this.openToolStripMenuItemNotify.Name = "openToolStripMenuItemNotify";
            this.openToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.openToolStripMenuItemNotify.Text = "Open";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(193, 6);
            // 
            // startRuntimeServiceToolStripMenuItemNotify
            // 
            this.startRuntimeServiceToolStripMenuItemNotify.Name = "startRuntimeServiceToolStripMenuItemNotify";
            this.startRuntimeServiceToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.startRuntimeServiceToolStripMenuItemNotify.Text = "Start Runtime Service";
            // 
            // stopRuntimeServiceToolStripMenuItemNotify
            // 
            this.stopRuntimeServiceToolStripMenuItemNotify.Enabled = false;
            this.stopRuntimeServiceToolStripMenuItemNotify.Name = "stopRuntimeServiceToolStripMenuItemNotify";
            this.stopRuntimeServiceToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.stopRuntimeServiceToolStripMenuItemNotify.Text = "Stop Runtime Service";
            // 
            // clearMemoryToolStripMenuItemNotify
            // 
            this.clearMemoryToolStripMenuItemNotify.Name = "clearMemoryToolStripMenuItemNotify";
            this.clearMemoryToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.clearMemoryToolStripMenuItemNotify.Text = "Clear Memory";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(193, 6);
            // 
            // listOfClientsToolStripMenuItemNotify
            // 
            this.listOfClientsToolStripMenuItemNotify.Name = "listOfClientsToolStripMenuItemNotify";
            this.listOfClientsToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.listOfClientsToolStripMenuItemNotify.Text = "List of Clients";
            // 
            // deviceBufferMemoryToolStripMenuItemNotify
            // 
            this.deviceBufferMemoryToolStripMenuItemNotify.Name = "deviceBufferMemoryToolStripMenuItemNotify";
            this.deviceBufferMemoryToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.deviceBufferMemoryToolStripMenuItemNotify.Text = "Device/Buffer Memory";
            // 
            // networkConfigurationToolStripMenuItemNotify
            // 
            this.networkConfigurationToolStripMenuItemNotify.Name = "networkConfigurationToolStripMenuItemNotify";
            this.networkConfigurationToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.networkConfigurationToolStripMenuItemNotify.Text = "Network Configuration";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(193, 6);
            // 
            // optionsToolStripMenuItemNotify
            // 
            this.optionsToolStripMenuItemNotify.Name = "optionsToolStripMenuItemNotify";
            this.optionsToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.optionsToolStripMenuItemNotify.Text = "Options";
            // 
            // aboutToolStripMenuItemNotify
            // 
            this.aboutToolStripMenuItemNotify.Name = "aboutToolStripMenuItemNotify";
            this.aboutToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.aboutToolStripMenuItemNotify.Text = "About";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(193, 6);
            // 
            // exitToolStripMenuItemNotify
            // 
            this.exitToolStripMenuItemNotify.Name = "exitToolStripMenuItemNotify";
            this.exitToolStripMenuItemNotify.Size = new System.Drawing.Size(196, 22);
            this.exitToolStripMenuItemNotify.Text = "Exit";
            // 
            // notifyIconPLC
            // 
            this.notifyIconPLC.ContextMenuStrip = this.ctmnuTray;
            this.notifyIconPLC.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconPLC.Icon")));
            this.notifyIconPLC.Text = "notifyIcon1";
            // 
            // timerLedRUN
            // 
            this.timerLedRUN.Interval = 400;
            // 
            // timerErr
            // 
            this.timerErr.Interval = 300;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 671);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.splitterBottom);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.statusStripBottom);
            this.Controls.Add(this.toolStripTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dotPLC Studio";
            this.statusStripBottom.ResumeLayout(false);
            this.statusStripBottom.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPLC)).EndInit();
            this.ctmnuClipboardDvgPLC.ResumeLayout(false);
            this.toolStripWatch.ResumeLayout(false);
            this.toolStripWatch.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ctmnuCPU.ResumeLayout(false);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.ctmnuTextBox.ResumeLayout(false);
            this.ctmnuTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStripBottom;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Splitter splitterBottom;
        private System.Windows.Forms.Panel panelMain;
        private CustomControl.Fx5uCpu fx5uCpu1;
        private System.Windows.Forms.Button btnCloseBottom;
        private System.Windows.Forms.Label lblBottom;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripEx toolStripWatch;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx onBitToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx offBitToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx toggleBitToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx updateToolStripButton;
        private System.Windows.Forms.DataGridView dvgPLC;
        private System.Windows.Forms.ContextMenuStrip ctmnuClipboardDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem pastToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem incrementToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem decrementToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem settimeToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripComboBox settimetoolStripComboBoxDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItemDvgPLC;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx cutToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx copyToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx pasteToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx deleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx aboutToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx stopserverToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx startserverToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx watchToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx deviceMemoryToolStripButton;
        private System.Windows.Forms.ToolStripComboBox CpuTypeToolStripComboBox;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx networkToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripEx toolStripTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx optionToolStripButton;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx clearToolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private CustomControl.DataGridViewTextAndImage.DataGridViewTextBoxImageColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCurrentValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDataType;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemDvgPLC;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItemDvgPLC;
        private CustomControl.DataGridViewTextAndImage.DataGridViewTextBoxImageColumn dataGridViewTextBoxImageColumn1;
        private System.Windows.Forms.Timer timerStartMonitor;
        private System.Windows.Forms.Timer timerLAN;
        private System.Windows.Forms.Timer timerIO;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ContextMenuStrip ctmnuTextBox;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItemTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItemTextBox;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItemTextBox;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItemTextBox;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItemTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx incrementToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx decrementToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripButtonEx stopcountToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private dotPLC.CustomControl.ToolStripHelper.ToolStripSplitButtonEx setTimeToolStripSplitButton;
        private System.Windows.Forms.ToolStripComboBox settimeToolStripComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ContextMenuStrip ctmnuCPU;
        private System.Windows.Forms.ContextMenuStrip ctmnuTray;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem startRuntimeServiceToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripMenuItem stopRuntimeServiceToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripMenuItem clearMemoryToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItemNotify;
        private CustomControl.ToolStripHelper.ToolStripButtonEx listClientsToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem networkConfigurationToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem listOfClientsToolStripMenuItemNotify;
        private System.Windows.Forms.ToolStripMenuItem deviceBufferMemoryToolStripMenuItemNotify;
        private System.Windows.Forms.Timer timerLedRUN;
        private System.Windows.Forms.Timer timerErr;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItemFX5U;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItemFX5U;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItemFX5U;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItemFX5U;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem setErrorToolStripMenuItemFX5U;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel clientsTssl;
        private System.Windows.Forms.ToolStripStatusLabel splitTssl;
        private System.Windows.Forms.ToolStripStatusLabel statusTssl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        public System.Windows.Forms.ToolStripStatusLabel noticeTssl;
        public System.Windows.Forms.ToolStripStatusLabel trayTssl;
        public System.Windows.Forms.ToolStripStatusLabel saveWatchTssl;
        public System.Windows.Forms.NotifyIcon notifyIconPLC;
    }
}

