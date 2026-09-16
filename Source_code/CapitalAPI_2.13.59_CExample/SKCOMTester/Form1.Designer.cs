namespace SKCOMTester
{
    partial class Form1
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
            this.btnInitialize = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EnvLable = new System.Windows.Forms.Label();
            this.SetSource = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.AuthorityBox = new System.Windows.Forms.ComboBox();
            this.OFAsyncFlag = new System.Windows.Forms.ComboBox();
            this.AP_CKBOX = new System.Windows.Forms.CheckBox();
            this.Center_Box = new System.Windows.Forms.ComboBox();
            this.BTN_GENKEY = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SUB_ID1 = new System.Windows.Forms.TextBox();
            this.Group_ID1 = new System.Windows.Forms.TextBox();
            this.lbl_SKAPI = new System.Windows.Forms.Label();
            this.btnRequestAgreement = new System.Windows.Forms.Button();
            this.btnInitializeQuote = new System.Windows.Forms.Button();
            this.checkQuoteFlag = new System.Windows.Forms.CheckBox();
            this.checkSGXDMA = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSignalSGXAPI = new System.Windows.Forms.Label();
            this.txtPassWord2 = new System.Windows.Forms.TextBox();
            this.txtAccount2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listInformation = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Center_Log = new System.Windows.Forms.Button();
            this.txt_Center_LogPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.skQuote1 = new SKCOMTester.SKQuote();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.skReply1 = new SKCOMTester.SKReply();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.skOrder1 = new SKCOMTester.SKOrder();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitialize
            // 
            this.btnInitialize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInitialize.Location = new System.Drawing.Point(485, 77);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(60, 22);
            this.btnInitialize.TabIndex = 21;
            this.btnInitialize.Text = "LogIn1";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPassword.Location = new System.Drawing.Point(243, 96);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(52, 15);
            this.lblPassword.TabIndex = 20;
            this.lblPassword.Text = "密碼：";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(323, 74);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(132, 22);
            this.txtPassWord.TabIndex = 19;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAccount.Location = new System.Drawing.Point(14, 100);
            this.lblAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(97, 15);
            this.lblAccount.TabIndex = 18;
            this.lblAccount.Text = "身份證字號：";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAccount.Location = new System.Drawing.Point(103, 73);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(132, 25);
            this.txtAccount.TabIndex = 17;
            this.txtAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccount_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.EnvLable);
            this.groupBox1.Controls.Add(this.SetSource);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.AuthorityBox);
            this.groupBox1.Controls.Add(this.OFAsyncFlag);
            this.groupBox1.Controls.Add(this.AP_CKBOX);
            this.groupBox1.Controls.Add(this.Center_Box);
            this.groupBox1.Controls.Add(this.BTN_GENKEY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.SUB_ID1);
            this.groupBox1.Controls.Add(this.Group_ID1);
            this.groupBox1.Controls.Add(this.lbl_SKAPI);
            this.groupBox1.Controls.Add(this.btnRequestAgreement);
            this.groupBox1.Controls.Add(this.btnInitializeQuote);
            this.groupBox1.Controls.Add(this.checkQuoteFlag);
            this.groupBox1.Controls.Add(this.checkSGXDMA);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txtPassWord2);
            this.groupBox1.Controls.Add(this.txtAccount2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listInformation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_Center_Log);
            this.groupBox1.Controls.Add(this.txt_Center_LogPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnInitialize);
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblAccount);
            this.groupBox1.Controls.Add(this.txtPassWord);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1189, 1020);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. SKCenterLib相關";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label4.Font = new System.Drawing.Font("新細明體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(414, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 11);
            this.label4.TabIndex = 70;
            this.label4.Text = "來源別";
            // 
            // EnvLable
            // 
            this.EnvLable.AutoSize = true;
            this.EnvLable.BackColor = System.Drawing.Color.Yellow;
            this.EnvLable.Font = new System.Drawing.Font("新細明體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EnvLable.Location = new System.Drawing.Point(960, 19);
            this.EnvLable.Name = "EnvLable";
            this.EnvLable.Size = new System.Drawing.Size(49, 11);
            this.EnvLable.TabIndex = 69;
            this.EnvLable.Text = "正式環境";
            // 
            // SetSource
            // 
            this.SetSource.FormattingEnabled = true;
            this.SetSource.Items.AddRange(new object[] {
            "None",
            "ICE",
            "MC",
            "MCWhite"});
            this.SetSource.Location = new System.Drawing.Point(414, 25);
            this.SetSource.Name = "SetSource";
            this.SetSource.Size = new System.Drawing.Size(53, 20);
            this.SetSource.TabIndex = 68;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "正_NOSGX",
            "正_SGXDMA",
            "D_NOSGX",
            "D_SGX"});
            this.comboBox1.Location = new System.Drawing.Point(555, 514);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(79, 20);
            this.comboBox1.TabIndex = 67;
            // 
            // AuthorityBox
            // 
            this.AuthorityBox.Font = new System.Drawing.Font("新細明體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AuthorityBox.FormattingEnabled = true;
            this.AuthorityBox.Items.AddRange(new object[] {
            "正_NOSGX",
            "正_SGXDMA",
            "D_NOSGX",
            "D_SGX"});
            this.AuthorityBox.Location = new System.Drawing.Point(1047, 11);
            this.AuthorityBox.Name = "AuthorityBox";
            this.AuthorityBox.Size = new System.Drawing.Size(79, 19);
            this.AuthorityBox.TabIndex = 66;
            this.AuthorityBox.SelectedIndexChanged += new System.EventHandler(this.AuthorityBox_SelectedIndexChanged);
            // 
            // OFAsyncFlag
            // 
            this.OFAsyncFlag.FormattingEnabled = true;
            this.OFAsyncFlag.Items.AddRange(new object[] {
            "同步",
            "非同步"});
            this.OFAsyncFlag.Location = new System.Drawing.Point(566, 428);
            this.OFAsyncFlag.Name = "OFAsyncFlag";
            this.OFAsyncFlag.Size = new System.Drawing.Size(56, 20);
            this.OFAsyncFlag.TabIndex = 65;
            this.OFAsyncFlag.Text = "同步";
            // 
            // AP_CKBOX
            // 
            this.AP_CKBOX.AutoSize = true;
            this.AP_CKBOX.Location = new System.Drawing.Point(544, 15);
            this.AP_CKBOX.Name = "AP_CKBOX";
            this.AP_CKBOX.Size = new System.Drawing.Size(90, 16);
            this.AP_CKBOX.TabIndex = 64;
            this.AP_CKBOX.Text = "AP_APH帳號";
            this.AP_CKBOX.UseVisualStyleBackColor = true;
            this.AP_CKBOX.CheckedChanged += new System.EventHandler(this.AP_CKBOX_CheckedChanged);
            // 
            // Center_Box
            // 
            this.Center_Box.FormattingEnabled = true;
            this.Center_Box.Items.AddRange(new object[] {
            "SKCenter",
            "SKCenter2"});
            this.Center_Box.Location = new System.Drawing.Point(548, 37);
            this.Center_Box.Name = "Center_Box";
            this.Center_Box.Size = new System.Drawing.Size(73, 20);
            this.Center_Box.TabIndex = 63;
            // 
            // BTN_GENKEY
            // 
            this.BTN_GENKEY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BTN_GENKEY.Location = new System.Drawing.Point(819, 36);
            this.BTN_GENKEY.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_GENKEY.Name = "BTN_GENKEY";
            this.BTN_GENKEY.Size = new System.Drawing.Size(93, 22);
            this.BTN_GENKEY.TabIndex = 62;
            this.BTN_GENKEY.Text = "GenKey1";
            this.BTN_GENKEY.UseVisualStyleBackColor = true;
            this.BTN_GENKEY.Click += new System.EventHandler(this.BTN_GENKEY_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(644, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "2-1.AP_雙因子key,請先確認是否有附屬帳號之憑證";
            // 
            // SUB_ID1
            // 
            this.SUB_ID1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SUB_ID1.Location = new System.Drawing.Point(724, 34);
            this.SUB_ID1.Margin = new System.Windows.Forms.Padding(4);
            this.SUB_ID1.Name = "SUB_ID1";
            this.SUB_ID1.Size = new System.Drawing.Size(87, 25);
            this.SUB_ID1.TabIndex = 60;
            this.SUB_ID1.Text = "SUBID1";
            // 
            // Group_ID1
            // 
            this.Group_ID1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Group_ID1.Location = new System.Drawing.Point(628, 35);
            this.Group_ID1.Margin = new System.Windows.Forms.Padding(4);
            this.Group_ID1.Name = "Group_ID1";
            this.Group_ID1.Size = new System.Drawing.Size(88, 25);
            this.Group_ID1.TabIndex = 59;
            this.Group_ID1.Text = "GroupID1";
            // 
            // lbl_SKAPI
            // 
            this.lbl_SKAPI.AutoSize = true;
            this.lbl_SKAPI.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SKAPI.Location = new System.Drawing.Point(916, 111);
            this.lbl_SKAPI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SKAPI.Name = "lbl_SKAPI";
            this.lbl_SKAPI.Size = new System.Drawing.Size(63, 15);
            this.lbl_SKAPI.TabIndex = 58;
            this.lbl_SKAPI.Text = "SKAPI：";
            // 
            // btnRequestAgreement
            // 
            this.btnRequestAgreement.Location = new System.Drawing.Point(714, 92);
            this.btnRequestAgreement.Name = "btnRequestAgreement";
            this.btnRequestAgreement.Size = new System.Drawing.Size(104, 23);
            this.btnRequestAgreement.TabIndex = 55;
            this.btnRequestAgreement.Text = "RequestAgreement";
            this.btnRequestAgreement.UseVisualStyleBackColor = true;
            this.btnRequestAgreement.Click += new System.EventHandler(this.btnRequestAgreement_Click);
            // 
            // btnInitializeQuote
            // 
            this.btnInitializeQuote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInitializeQuote.Location = new System.Drawing.Point(825, 89);
            this.btnInitializeQuote.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitializeQuote.Name = "btnInitializeQuote";
            this.btnInitializeQuote.Size = new System.Drawing.Size(87, 28);
            this.btnInitializeQuote.TabIndex = 57;
            this.btnInitializeQuote.Text = "LogInSetQuote";
            this.btnInitializeQuote.UseVisualStyleBackColor = true;
            this.btnInitializeQuote.Click += new System.EventHandler(this.btnInitializeQuote_Click);
            // 
            // checkQuoteFlag
            // 
            this.checkQuoteFlag.AutoSize = true;
            this.checkQuoteFlag.Location = new System.Drawing.Point(825, 73);
            this.checkQuoteFlag.Name = "checkQuoteFlag";
            this.checkQuoteFlag.Size = new System.Drawing.Size(72, 16);
            this.checkQuoteFlag.TabIndex = 56;
            this.checkQuoteFlag.Text = "報價啟用";
            this.checkQuoteFlag.UseVisualStyleBackColor = true;
            // 
            // checkSGXDMA
            // 
            this.checkSGXDMA.AutoSize = true;
            this.checkSGXDMA.Font = new System.Drawing.Font("新細明體", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkSGXDMA.Location = new System.Drawing.Point(1058, 15);
            this.checkSGXDMA.Name = "checkSGXDMA";
            this.checkSGXDMA.Size = new System.Drawing.Size(88, 14);
            this.checkSGXDMA.TabIndex = 53;
            this.checkSGXDMA.Text = "停用SGX DMA";
            this.checkSGXDMA.UseVisualStyleBackColor = true;
            this.checkSGXDMA.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSignalSGXAPI);
            this.groupBox3.Location = new System.Drawing.Point(1028, 37);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(70, 54);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SGX_API";
            // 
            // lblSignalSGXAPI
            // 
            this.lblSignalSGXAPI.AutoSize = true;
            this.lblSignalSGXAPI.Font = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignalSGXAPI.ForeColor = System.Drawing.Color.Red;
            this.lblSignalSGXAPI.Location = new System.Drawing.Point(15, 18);
            this.lblSignalSGXAPI.Name = "lblSignalSGXAPI";
            this.lblSignalSGXAPI.Size = new System.Drawing.Size(32, 22);
            this.lblSignalSGXAPI.TabIndex = 0;
            this.lblSignalSGXAPI.Text = "●";
            // 
            // txtPassWord2
            // 
            this.txtPassWord2.Location = new System.Drawing.Point(323, 100);
            this.txtPassWord2.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassWord2.Name = "txtPassWord2";
            this.txtPassWord2.PasswordChar = '*';
            this.txtPassWord2.Size = new System.Drawing.Size(132, 22);
            this.txtPassWord2.TabIndex = 28;
            // 
            // txtAccount2
            // 
            this.txtAccount2.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAccount2.Location = new System.Drawing.Point(103, 100);
            this.txtAccount2.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccount2.Name = "txtAccount2";
            this.txtAccount2.Size = new System.Drawing.Size(132, 25);
            this.txtAccount2.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(485, 100);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 20);
            this.button1.TabIndex = 29;
            this.button1.Text = "LogIn2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listInformation
            // 
            this.listInformation.FormattingEnabled = true;
            this.listInformation.HorizontalExtent = 2;
            this.listInformation.HorizontalScrollbar = true;
            this.listInformation.ItemHeight = 12;
            this.listInformation.Location = new System.Drawing.Point(8, 132);
            this.listInformation.Name = "listInformation";
            this.listInformation.ScrollAlwaysVisible = true;
            this.listInformation.Size = new System.Drawing.Size(1090, 100);
            this.listInformation.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "2. 雙因子登入";
            // 
            // btn_Center_Log
            // 
            this.btn_Center_Log.Location = new System.Drawing.Point(281, 34);
            this.btn_Center_Log.Name = "btn_Center_Log";
            this.btn_Center_Log.Size = new System.Drawing.Size(83, 23);
            this.btn_Center_Log.TabIndex = 24;
            this.btn_Center_Log.Text = "變更LOG路徑";
            this.btn_Center_Log.UseVisualStyleBackColor = true;
            this.btn_Center_Log.Click += new System.EventHandler(this.btn_Center_Log_Click);
            // 
            // txt_Center_LogPath
            // 
            this.txt_Center_LogPath.Location = new System.Drawing.Point(8, 36);
            this.txt_Center_LogPath.Name = "txt_Center_LogPath";
            this.txt_Center_LogPath.Size = new System.Drawing.Size(246, 22);
            this.txt_Center_LogPath.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "1. 變更Log儲存路徑-預設存於執行檔目錄下，LOG產生後中途變更路徑無效";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.skQuote1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1186, 801);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "報價";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // skQuote1
            // 
            this.skQuote1.AutoScroll = true;
            this.skQuote1.Location = new System.Drawing.Point(37, 6);
            this.skQuote1.LoginID = "";
            this.skQuote1.LoginID2 = "";
            this.skQuote1.Name = "skQuote1";
            this.skQuote1.Size = new System.Drawing.Size(907, 487);
            this.skQuote1.SKQuoteLib = null;
            this.skQuote1.SKQuoteLib2 = null;
            this.skQuote1.TabIndex = 0;
            this.skQuote1.GetMessage += new SKCOMTester.SKQuote.MyMessageHandler(this.GetMessage);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.skReply1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1186, 801);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "回報";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // skReply1
            // 
            this.skReply1.AutoScroll = true;
            this.skReply1.AutoSize = true;
            this.skReply1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.skReply1.Location = new System.Drawing.Point(3, 6);
            this.skReply1.LoginID = "";
            this.skReply1.LoginID2 = "";
            this.skReply1.Name = "skReply1";
            this.skReply1.OrderM = false;
            this.skReply1.Size = new System.Drawing.Size(1000, 866);
            this.skReply1.SKReplyLib = null;
            this.skReply1.SKReplyLib2 = null;
            this.skReply1.TabIndex = 0;
            this.skReply1.GetMessage += new SKCOMTester.SKReply.MyMessageHandler(this.GetMessage);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.skOrder1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1186, 801);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "下單";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // skOrder1
            // 
            this.skOrder1.AutoScroll = true;
            this.skOrder1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.skOrder1.Location = new System.Drawing.Point(10, 32);
            this.skOrder1.LoginID = "";
            this.skOrder1.LoginID2 = "";
            this.skOrder1.Name = "skOrder1";
            this.skOrder1.OrderObj = null;
            this.skOrder1.OrderObj2 = null;
            this.skOrder1.SGXDMA = false;
            this.skOrder1.Size = new System.Drawing.Size(1144, 755);
            this.skOrder1.TabIndex = 0;
            this.skOrder1.GetMessage += new SKCOMTester.SKOrder.MyMessageHandler(this.GetMessage);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(11, 239);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1194, 827);
            this.tabControl1.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1217, 1020);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SKCOMTesster";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Center_Log;
        private System.Windows.Forms.TextBox txt_Center_LogPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listInformation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPassWord2;
        private System.Windows.Forms.TextBox txtAccount2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSignalSGXAPI;
        private System.Windows.Forms.CheckBox checkSGXDMA;
        private System.Windows.Forms.Button btnRequestAgreement;
        private System.Windows.Forms.Button btnInitializeQuote;
        private System.Windows.Forms.CheckBox checkQuoteFlag;
        private System.Windows.Forms.Label lbl_SKAPI;
        private System.Windows.Forms.TabPage tabPage3;
        private SKQuote skQuote1;
        private System.Windows.Forms.TabPage tabPage1;
        private SKReply skReply1;
        private System.Windows.Forms.TabPage tabPage2;
        private SKOrder skOrder1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button BTN_GENKEY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SUB_ID1;
        private System.Windows.Forms.TextBox Group_ID1;
        private System.Windows.Forms.ComboBox Center_Box;
        private System.Windows.Forms.CheckBox AP_CKBOX;
        private System.Windows.Forms.ComboBox AuthorityBox;
        private System.Windows.Forms.ComboBox OFAsyncFlag;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox SetSource;
        private System.Windows.Forms.Label EnvLable;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.Button StopMTimer;
        //private System.Windows.Forms.Button button2;
        //private System.Windows.Forms.Button StartMTimer;
    }
}

