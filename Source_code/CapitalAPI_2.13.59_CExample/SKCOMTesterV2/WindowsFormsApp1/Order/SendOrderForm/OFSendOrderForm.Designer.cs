
namespace WindowsFormsApp1
{
    partial class OFSendOrderForm
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
            this.tabControlOrder = new System.Windows.Forms.TabControl();
            this.tabPageOverseaFutureOrder = new System.Windows.Forms.TabPage();
            this.textBoxOFYearMonth2 = new System.Windows.Forms.TextBox();
            this.labelOFYearMonth2 = new System.Windows.Forms.Label();
            this.buttonSendOverSeaFutureOrder = new System.Windows.Forms.Button();
            this.textBoxOFQty = new System.Windows.Forms.TextBox();
            this.comboBoxOFSpecialTradeType = new System.Windows.Forms.ComboBox();
            this.comboBoxOFTradeType = new System.Windows.Forms.ComboBox();
            this.comboBoxOFDayTrade = new System.Windows.Forms.ComboBox();
            this.comboBoxOFBuySell = new System.Windows.Forms.ComboBox();
            this.textBoxOFTriggerNumerator = new System.Windows.Forms.TextBox();
            this.textBoxOFTrigger = new System.Windows.Forms.TextBox();
            this.textBoxOFOrderNumerator = new System.Windows.Forms.TextBox();
            this.textBoxOFOrder = new System.Windows.Forms.TextBox();
            this.textBoxOFYearMonth = new System.Windows.Forms.TextBox();
            this.textBoxOFStockNo = new System.Windows.Forms.TextBox();
            this.textBoxOFExchangeNo = new System.Windows.Forms.TextBox();
            this.labelOFQty = new System.Windows.Forms.Label();
            this.labelOFTradeType = new System.Windows.Forms.Label();
            this.labelOFDayTrade = new System.Windows.Forms.Label();
            this.labelOFBuySell = new System.Windows.Forms.Label();
            this.labelOFTriggerNumerator = new System.Windows.Forms.Label();
            this.labelOFTrigger = new System.Windows.Forms.Label();
            this.labelOFOrder = new System.Windows.Forms.Label();
            this.labelOFYearMonth = new System.Windows.Forms.Label();
            this.labelOFOrderNumerator = new System.Windows.Forms.Label();
            this.labelOFSpecialTradeType = new System.Windows.Forms.Label();
            this.labelOFStockNo = new System.Windows.Forms.Label();
            this.labelOFExchangeNo = new System.Windows.Forms.Label();
            this.tabPageOverSeaOptionOrder = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxbstrOrderDenominator = new System.Windows.Forms.TextBox();
            this.buttonSendOverseaOptionOrder = new System.Windows.Forms.Button();
            this.textBoxOOQty = new System.Windows.Forms.TextBox();
            this.labelOOQty = new System.Windows.Forms.Label();
            this.comboBoxOOCallPut = new System.Windows.Forms.ComboBox();
            this.labelOOCallPut = new System.Windows.Forms.Label();
            this.labelOOStrikePrice = new System.Windows.Forms.Label();
            this.textBoxOOStrikePrice = new System.Windows.Forms.TextBox();
            this.comboBoxOOSpecialTradeType = new System.Windows.Forms.ComboBox();
            this.labelOOSpecialTradeType = new System.Windows.Forms.Label();
            this.comboBoxOODayTrade = new System.Windows.Forms.ComboBox();
            this.labelOODayTrade = new System.Windows.Forms.Label();
            this.comboBoxOONewClose = new System.Windows.Forms.ComboBox();
            this.labelOONewClose = new System.Windows.Forms.Label();
            this.comboBoxOOBuySell = new System.Windows.Forms.ComboBox();
            this.labelOOBuySell = new System.Windows.Forms.Label();
            this.textBoxOOTriggerNumerator = new System.Windows.Forms.TextBox();
            this.labelOOTriggerNumerator = new System.Windows.Forms.Label();
            this.textBoxOOTrigger = new System.Windows.Forms.TextBox();
            this.labelOOTrigger = new System.Windows.Forms.Label();
            this.textBoxOOOrderNumerator = new System.Windows.Forms.TextBox();
            this.labelOOOrderNumerator = new System.Windows.Forms.Label();
            this.textBoxOOOrder = new System.Windows.Forms.TextBox();
            this.labelOOOrder = new System.Windows.Forms.Label();
            this.textBoxOOYearMonth = new System.Windows.Forms.TextBox();
            this.labelOOYearMonth = new System.Windows.Forms.Label();
            this.textBoxOOStockNo = new System.Windows.Forms.TextBox();
            this.labelOOStockNo = new System.Windows.Forms.Label();
            this.textBoxOOExchangeNo = new System.Windows.Forms.TextBox();
            this.labelOOExchangeNo = new System.Windows.Forms.Label();
            this.checkBoxSpread = new System.Windows.Forms.CheckBox();
            this.checkBoxAsyncOrder = new System.Windows.Forms.CheckBox();
            this.richTextBoxMethodMessage = new System.Windows.Forms.RichTextBox();
            this.panelSendOrderForm = new System.Windows.Forms.Panel();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.tabControlOrder.SuspendLayout();
            this.tabPageOverseaFutureOrder.SuspendLayout();
            this.tabPageOverSeaOptionOrder.SuspendLayout();
            this.panelSendOrderForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlOrder
            // 
            this.tabControlOrder.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlOrder.Controls.Add(this.tabPageOverseaFutureOrder);
            this.tabControlOrder.Controls.Add(this.tabPageOverSeaOptionOrder);
            this.tabControlOrder.Font = new System.Drawing.Font("DFKai-SB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlOrder.Location = new System.Drawing.Point(7, 123);
            this.tabControlOrder.Name = "tabControlOrder";
            this.tabControlOrder.SelectedIndex = 0;
            this.tabControlOrder.Size = new System.Drawing.Size(364, 620);
            this.tabControlOrder.TabIndex = 0;
            // 
            // tabPageOverseaFutureOrder
            // 
            this.tabPageOverseaFutureOrder.AutoScroll = true;
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFYearMonth2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFYearMonth2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.buttonSendOverSeaFutureOrder);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFQty);
            this.tabPageOverseaFutureOrder.Controls.Add(this.comboBoxOFSpecialTradeType);
            this.tabPageOverseaFutureOrder.Controls.Add(this.comboBoxOFTradeType);
            this.tabPageOverseaFutureOrder.Controls.Add(this.comboBoxOFDayTrade);
            this.tabPageOverseaFutureOrder.Controls.Add(this.comboBoxOFBuySell);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFTriggerNumerator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFTrigger);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFOrderNumerator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFOrder);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFYearMonth);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFStockNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFExchangeNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFQty);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFTradeType);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFDayTrade);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFBuySell);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFTriggerNumerator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFTrigger);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFOrder);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFYearMonth);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFOrderNumerator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFSpecialTradeType);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFStockNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFExchangeNo);
            this.tabPageOverseaFutureOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageOverseaFutureOrder.Name = "tabPageOverseaFutureOrder";
            this.tabPageOverseaFutureOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverseaFutureOrder.Size = new System.Drawing.Size(356, 584);
            this.tabPageOverseaFutureOrder.TabIndex = 4;
            this.tabPageOverseaFutureOrder.Text = "海期";
            this.tabPageOverseaFutureOrder.UseVisualStyleBackColor = true;
            // 
            // textBoxOFYearMonth2
            // 
            this.textBoxOFYearMonth2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFYearMonth2.Location = new System.Drawing.Point(231, 123);
            this.textBoxOFYearMonth2.Name = "textBoxOFYearMonth2";
            this.textBoxOFYearMonth2.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFYearMonth2.TabIndex = 102;
            this.textBoxOFYearMonth2.Text = "202409";
            this.textBoxOFYearMonth2.Visible = false;
            // 
            // labelOFYearMonth2
            // 
            this.labelOFYearMonth2.AutoSize = true;
            this.labelOFYearMonth2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFYearMonth2.Location = new System.Drawing.Point(3, 132);
            this.labelOFYearMonth2.Name = "labelOFYearMonth2";
            this.labelOFYearMonth2.Size = new System.Drawing.Size(224, 24);
            this.labelOFYearMonth2.TabIndex = 101;
            this.labelOFYearMonth2.Text = "遠月商品年月(YYYYMM)";
            this.labelOFYearMonth2.Visible = false;
            // 
            // buttonSendOverSeaFutureOrder
            // 
            this.buttonSendOverSeaFutureOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendOverSeaFutureOrder.Location = new System.Drawing.Point(111, 509);
            this.buttonSendOverSeaFutureOrder.Name = "buttonSendOverSeaFutureOrder";
            this.buttonSendOverSeaFutureOrder.Size = new System.Drawing.Size(121, 32);
            this.buttonSendOverSeaFutureOrder.TabIndex = 99;
            this.buttonSendOverSeaFutureOrder.Text = "送出";
            this.buttonSendOverSeaFutureOrder.UseVisualStyleBackColor = true;
            this.buttonSendOverSeaFutureOrder.Click += new System.EventHandler(this.buttonSendOverSeaFutureOrder_Click);
            // 
            // textBoxOFQty
            // 
            this.textBoxOFQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFQty.Location = new System.Drawing.Point(231, 470);
            this.textBoxOFQty.Name = "textBoxOFQty";
            this.textBoxOFQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFQty.TabIndex = 98;
            this.textBoxOFQty.Text = "1";
            // 
            // comboBoxOFSpecialTradeType
            // 
            this.comboBoxOFSpecialTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOFSpecialTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOFSpecialTradeType.FormattingEnabled = true;
            this.comboBoxOFSpecialTradeType.Location = new System.Drawing.Point(231, 432);
            this.comboBoxOFSpecialTradeType.Name = "comboBoxOFSpecialTradeType";
            this.comboBoxOFSpecialTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOFSpecialTradeType.TabIndex = 97;
            // 
            // comboBoxOFTradeType
            // 
            this.comboBoxOFTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOFTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOFTradeType.FormattingEnabled = true;
            this.comboBoxOFTradeType.Location = new System.Drawing.Point(231, 394);
            this.comboBoxOFTradeType.Name = "comboBoxOFTradeType";
            this.comboBoxOFTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOFTradeType.TabIndex = 96;
            // 
            // comboBoxOFDayTrade
            // 
            this.comboBoxOFDayTrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOFDayTrade.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOFDayTrade.FormattingEnabled = true;
            this.comboBoxOFDayTrade.Location = new System.Drawing.Point(231, 356);
            this.comboBoxOFDayTrade.Name = "comboBoxOFDayTrade";
            this.comboBoxOFDayTrade.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOFDayTrade.TabIndex = 95;
            // 
            // comboBoxOFBuySell
            // 
            this.comboBoxOFBuySell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOFBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOFBuySell.FormattingEnabled = true;
            this.comboBoxOFBuySell.Location = new System.Drawing.Point(231, 318);
            this.comboBoxOFBuySell.Name = "comboBoxOFBuySell";
            this.comboBoxOFBuySell.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOFBuySell.TabIndex = 94;
            // 
            // textBoxOFTriggerNumerator
            // 
            this.textBoxOFTriggerNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFTriggerNumerator.Location = new System.Drawing.Point(231, 279);
            this.textBoxOFTriggerNumerator.Name = "textBoxOFTriggerNumerator";
            this.textBoxOFTriggerNumerator.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFTriggerNumerator.TabIndex = 66;
            this.textBoxOFTriggerNumerator.Text = "0";
            // 
            // textBoxOFTrigger
            // 
            this.textBoxOFTrigger.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFTrigger.Location = new System.Drawing.Point(231, 240);
            this.textBoxOFTrigger.Name = "textBoxOFTrigger";
            this.textBoxOFTrigger.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFTrigger.TabIndex = 65;
            this.textBoxOFTrigger.Text = "0";
            // 
            // textBoxOFOrderNumerator
            // 
            this.textBoxOFOrderNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFOrderNumerator.Location = new System.Drawing.Point(231, 201);
            this.textBoxOFOrderNumerator.Name = "textBoxOFOrderNumerator";
            this.textBoxOFOrderNumerator.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFOrderNumerator.TabIndex = 64;
            this.textBoxOFOrderNumerator.Text = "0";
            // 
            // textBoxOFOrder
            // 
            this.textBoxOFOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFOrder.Location = new System.Drawing.Point(231, 162);
            this.textBoxOFOrder.Name = "textBoxOFOrder";
            this.textBoxOFOrder.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFOrder.TabIndex = 63;
            this.textBoxOFOrder.Text = "4425";
            // 
            // textBoxOFYearMonth
            // 
            this.textBoxOFYearMonth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFYearMonth.Location = new System.Drawing.Point(231, 84);
            this.textBoxOFYearMonth.Name = "textBoxOFYearMonth";
            this.textBoxOFYearMonth.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFYearMonth.TabIndex = 62;
            this.textBoxOFYearMonth.Text = "202406";
            // 
            // textBoxOFStockNo
            // 
            this.textBoxOFStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFStockNo.Location = new System.Drawing.Point(231, 45);
            this.textBoxOFStockNo.Name = "textBoxOFStockNo";
            this.textBoxOFStockNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFStockNo.TabIndex = 61;
            this.textBoxOFStockNo.Text = "ES";
            // 
            // textBoxOFExchangeNo
            // 
            this.textBoxOFExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFExchangeNo.Location = new System.Drawing.Point(231, 6);
            this.textBoxOFExchangeNo.Name = "textBoxOFExchangeNo";
            this.textBoxOFExchangeNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFExchangeNo.TabIndex = 60;
            this.textBoxOFExchangeNo.Text = "CME";
            // 
            // labelOFQty
            // 
            this.labelOFQty.AutoSize = true;
            this.labelOFQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFQty.Location = new System.Drawing.Point(3, 479);
            this.labelOFQty.Name = "labelOFQty";
            this.labelOFQty.Size = new System.Drawing.Size(86, 24);
            this.labelOFQty.TabIndex = 59;
            this.labelOFQty.Text = "交易口數";
            // 
            // labelOFTradeType
            // 
            this.labelOFTradeType.AutoSize = true;
            this.labelOFTradeType.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOFTradeType.Location = new System.Drawing.Point(3, 404);
            this.labelOFTradeType.Name = "labelOFTradeType";
            this.labelOFTradeType.Size = new System.Drawing.Size(140, 22);
            this.labelOFTradeType.TabIndex = 58;
            this.labelOFTradeType.Text = "ROD/FOK/IOC";
            // 
            // labelOFDayTrade
            // 
            this.labelOFDayTrade.AutoSize = true;
            this.labelOFDayTrade.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFDayTrade.Location = new System.Drawing.Point(3, 364);
            this.labelOFDayTrade.Name = "labelOFDayTrade";
            this.labelOFDayTrade.Size = new System.Drawing.Size(48, 24);
            this.labelOFDayTrade.TabIndex = 57;
            this.labelOFDayTrade.Text = "當沖";
            // 
            // labelOFBuySell
            // 
            this.labelOFBuySell.AutoSize = true;
            this.labelOFBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFBuySell.Location = new System.Drawing.Point(3, 326);
            this.labelOFBuySell.Name = "labelOFBuySell";
            this.labelOFBuySell.Size = new System.Drawing.Size(94, 24);
            this.labelOFBuySell.TabIndex = 56;
            this.labelOFBuySell.Text = "買進/賣出";
            // 
            // labelOFTriggerNumerator
            // 
            this.labelOFTriggerNumerator.AutoSize = true;
            this.labelOFTriggerNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFTriggerNumerator.Location = new System.Drawing.Point(3, 288);
            this.labelOFTriggerNumerator.Name = "labelOFTriggerNumerator";
            this.labelOFTriggerNumerator.Size = new System.Drawing.Size(105, 24);
            this.labelOFTriggerNumerator.TabIndex = 55;
            this.labelOFTriggerNumerator.Text = "觸發價分子";
            // 
            // labelOFTrigger
            // 
            this.labelOFTrigger.AutoSize = true;
            this.labelOFTrigger.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFTrigger.Location = new System.Drawing.Point(3, 249);
            this.labelOFTrigger.Name = "labelOFTrigger";
            this.labelOFTrigger.Size = new System.Drawing.Size(67, 24);
            this.labelOFTrigger.TabIndex = 54;
            this.labelOFTrigger.Text = "觸發價";
            // 
            // labelOFOrder
            // 
            this.labelOFOrder.AutoSize = true;
            this.labelOFOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFOrder.Location = new System.Drawing.Point(3, 171);
            this.labelOFOrder.Name = "labelOFOrder";
            this.labelOFOrder.Size = new System.Drawing.Size(67, 24);
            this.labelOFOrder.TabIndex = 53;
            this.labelOFOrder.Text = "委託價";
            // 
            // labelOFYearMonth
            // 
            this.labelOFYearMonth.AutoSize = true;
            this.labelOFYearMonth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFYearMonth.Location = new System.Drawing.Point(3, 93);
            this.labelOFYearMonth.Name = "labelOFYearMonth";
            this.labelOFYearMonth.Size = new System.Drawing.Size(224, 24);
            this.labelOFYearMonth.TabIndex = 52;
            this.labelOFYearMonth.Text = "近月商品年月(YYYYMM)";
            // 
            // labelOFOrderNumerator
            // 
            this.labelOFOrderNumerator.AutoSize = true;
            this.labelOFOrderNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFOrderNumerator.Location = new System.Drawing.Point(3, 210);
            this.labelOFOrderNumerator.Name = "labelOFOrderNumerator";
            this.labelOFOrderNumerator.Size = new System.Drawing.Size(105, 24);
            this.labelOFOrderNumerator.TabIndex = 51;
            this.labelOFOrderNumerator.Text = "委託價分子";
            // 
            // labelOFSpecialTradeType
            // 
            this.labelOFSpecialTradeType.AutoSize = true;
            this.labelOFSpecialTradeType.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOFSpecialTradeType.Location = new System.Drawing.Point(3, 442);
            this.labelOFSpecialTradeType.Name = "labelOFSpecialTradeType";
            this.labelOFSpecialTradeType.Size = new System.Drawing.Size(185, 22);
            this.labelOFSpecialTradeType.TabIndex = 50;
            this.labelOFSpecialTradeType.Text = "LMT/MKT/STL/STP";
            // 
            // labelOFStockNo
            // 
            this.labelOFStockNo.AutoSize = true;
            this.labelOFStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFStockNo.Location = new System.Drawing.Point(3, 54);
            this.labelOFStockNo.Name = "labelOFStockNo";
            this.labelOFStockNo.Size = new System.Drawing.Size(124, 24);
            this.labelOFStockNo.TabIndex = 49;
            this.labelOFStockNo.Text = "海外期貨代號";
            // 
            // labelOFExchangeNo
            // 
            this.labelOFExchangeNo.AutoSize = true;
            this.labelOFExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFExchangeNo.Location = new System.Drawing.Point(3, 15);
            this.labelOFExchangeNo.Name = "labelOFExchangeNo";
            this.labelOFExchangeNo.Size = new System.Drawing.Size(105, 24);
            this.labelOFExchangeNo.TabIndex = 48;
            this.labelOFExchangeNo.Text = "交易所代號";
            // 
            // tabPageOverSeaOptionOrder
            // 
            this.tabPageOverSeaOptionOrder.AutoScroll = true;
            this.tabPageOverSeaOptionOrder.Controls.Add(this.label1);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxbstrOrderDenominator);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.buttonSendOverseaOptionOrder);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOQty);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOQty);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.comboBoxOOCallPut);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOCallPut);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOStrikePrice);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOStrikePrice);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.comboBoxOOSpecialTradeType);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOSpecialTradeType);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.comboBoxOODayTrade);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOODayTrade);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.comboBoxOONewClose);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOONewClose);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.comboBoxOOBuySell);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOBuySell);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOTriggerNumerator);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOTriggerNumerator);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOTrigger);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOTrigger);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOOrderNumerator);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOOrderNumerator);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOOrder);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOOrder);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOYearMonth);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOYearMonth);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOStockNo);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOStockNo);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.textBoxOOExchangeNo);
            this.tabPageOverSeaOptionOrder.Controls.Add(this.labelOOExchangeNo);
            this.tabPageOverSeaOptionOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageOverSeaOptionOrder.Name = "tabPageOverSeaOptionOrder";
            this.tabPageOverSeaOptionOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverSeaOptionOrder.Size = new System.Drawing.Size(356, 584);
            this.tabPageOverSeaOptionOrder.TabIndex = 5;
            this.tabPageOverSeaOptionOrder.Text = "海選";
            this.tabPageOverSeaOptionOrder.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(173, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 110;
            this.label1.Text = "分母";
            // 
            // textBoxbstrOrderDenominator
            // 
            this.textBoxbstrOrderDenominator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxbstrOrderDenominator.Location = new System.Drawing.Point(231, 163);
            this.textBoxbstrOrderDenominator.Name = "textBoxbstrOrderDenominator";
            this.textBoxbstrOrderDenominator.Size = new System.Drawing.Size(68, 33);
            this.textBoxbstrOrderDenominator.TabIndex = 109;
            this.textBoxbstrOrderDenominator.Text = "64";
            // 
            // buttonSendOverseaOptionOrder
            // 
            this.buttonSendOverseaOptionOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSendOverseaOptionOrder.Location = new System.Drawing.Point(111, 546);
            this.buttonSendOverseaOptionOrder.Name = "buttonSendOverseaOptionOrder";
            this.buttonSendOverseaOptionOrder.Size = new System.Drawing.Size(121, 32);
            this.buttonSendOverseaOptionOrder.TabIndex = 108;
            this.buttonSendOverseaOptionOrder.Text = "送出";
            this.buttonSendOverseaOptionOrder.UseVisualStyleBackColor = true;
            this.buttonSendOverseaOptionOrder.Click += new System.EventHandler(this.buttonSendOverseaOptionOrder_Click);
            // 
            // textBoxOOQty
            // 
            this.textBoxOOQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOQty.Location = new System.Drawing.Point(231, 513);
            this.textBoxOOQty.Name = "textBoxOOQty";
            this.textBoxOOQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOQty.TabIndex = 107;
            this.textBoxOOQty.Text = "1";
            // 
            // labelOOQty
            // 
            this.labelOOQty.AutoSize = true;
            this.labelOOQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOQty.Location = new System.Drawing.Point(3, 522);
            this.labelOOQty.Name = "labelOOQty";
            this.labelOOQty.Size = new System.Drawing.Size(86, 24);
            this.labelOOQty.TabIndex = 106;
            this.labelOOQty.Text = "交易口數";
            // 
            // comboBoxOOCallPut
            // 
            this.comboBoxOOCallPut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOOCallPut.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOOCallPut.FormattingEnabled = true;
            this.comboBoxOOCallPut.Location = new System.Drawing.Point(231, 475);
            this.comboBoxOOCallPut.Name = "comboBoxOOCallPut";
            this.comboBoxOOCallPut.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOOCallPut.TabIndex = 105;
            // 
            // labelOOCallPut
            // 
            this.labelOOCallPut.AutoSize = true;
            this.labelOOCallPut.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOOCallPut.Location = new System.Drawing.Point(3, 485);
            this.labelOOCallPut.Name = "labelOOCallPut";
            this.labelOOCallPut.Size = new System.Drawing.Size(104, 22);
            this.labelOOCallPut.TabIndex = 104;
            this.labelOOCallPut.Text = "CALL/PUT";
            // 
            // labelOOStrikePrice
            // 
            this.labelOOStrikePrice.AutoSize = true;
            this.labelOOStrikePrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOStrikePrice.Location = new System.Drawing.Point(3, 445);
            this.labelOOStrikePrice.Name = "labelOOStrikePrice";
            this.labelOOStrikePrice.Size = new System.Drawing.Size(67, 24);
            this.labelOOStrikePrice.TabIndex = 103;
            this.labelOOStrikePrice.Text = "履約價";
            // 
            // textBoxOOStrikePrice
            // 
            this.textBoxOOStrikePrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOStrikePrice.Location = new System.Drawing.Point(231, 436);
            this.textBoxOOStrikePrice.Name = "textBoxOOStrikePrice";
            this.textBoxOOStrikePrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOStrikePrice.TabIndex = 102;
            this.textBoxOOStrikePrice.Text = "100";
            // 
            // comboBoxOOSpecialTradeType
            // 
            this.comboBoxOOSpecialTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOOSpecialTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOOSpecialTradeType.FormattingEnabled = true;
            this.comboBoxOOSpecialTradeType.Location = new System.Drawing.Point(231, 398);
            this.comboBoxOOSpecialTradeType.Name = "comboBoxOOSpecialTradeType";
            this.comboBoxOOSpecialTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOOSpecialTradeType.TabIndex = 101;
            // 
            // labelOOSpecialTradeType
            // 
            this.labelOOSpecialTradeType.AutoSize = true;
            this.labelOOSpecialTradeType.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOOSpecialTradeType.Location = new System.Drawing.Point(3, 408);
            this.labelOOSpecialTradeType.Name = "labelOOSpecialTradeType";
            this.labelOOSpecialTradeType.Size = new System.Drawing.Size(185, 22);
            this.labelOOSpecialTradeType.TabIndex = 100;
            this.labelOOSpecialTradeType.Text = "LMT/MKT/STL/STP";
            // 
            // comboBoxOODayTrade
            // 
            this.comboBoxOODayTrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOODayTrade.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOODayTrade.FormattingEnabled = true;
            this.comboBoxOODayTrade.Location = new System.Drawing.Point(231, 360);
            this.comboBoxOODayTrade.Name = "comboBoxOODayTrade";
            this.comboBoxOODayTrade.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOODayTrade.TabIndex = 99;
            // 
            // labelOODayTrade
            // 
            this.labelOODayTrade.AutoSize = true;
            this.labelOODayTrade.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOODayTrade.Location = new System.Drawing.Point(3, 368);
            this.labelOODayTrade.Name = "labelOODayTrade";
            this.labelOODayTrade.Size = new System.Drawing.Size(48, 24);
            this.labelOODayTrade.TabIndex = 98;
            this.labelOODayTrade.Text = "當沖";
            // 
            // comboBoxOONewClose
            // 
            this.comboBoxOONewClose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOONewClose.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOONewClose.FormattingEnabled = true;
            this.comboBoxOONewClose.Location = new System.Drawing.Point(231, 322);
            this.comboBoxOONewClose.Name = "comboBoxOONewClose";
            this.comboBoxOONewClose.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOONewClose.TabIndex = 97;
            // 
            // labelOONewClose
            // 
            this.labelOONewClose.AutoSize = true;
            this.labelOONewClose.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOONewClose.Location = new System.Drawing.Point(3, 330);
            this.labelOONewClose.Name = "labelOONewClose";
            this.labelOONewClose.Size = new System.Drawing.Size(67, 24);
            this.labelOONewClose.TabIndex = 96;
            this.labelOONewClose.Text = "新平倉";
            // 
            // comboBoxOOBuySell
            // 
            this.comboBoxOOBuySell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOOBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOOBuySell.FormattingEnabled = true;
            this.comboBoxOOBuySell.Location = new System.Drawing.Point(231, 284);
            this.comboBoxOOBuySell.Name = "comboBoxOOBuySell";
            this.comboBoxOOBuySell.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOOBuySell.TabIndex = 95;
            // 
            // labelOOBuySell
            // 
            this.labelOOBuySell.AutoSize = true;
            this.labelOOBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOBuySell.Location = new System.Drawing.Point(3, 292);
            this.labelOOBuySell.Name = "labelOOBuySell";
            this.labelOOBuySell.Size = new System.Drawing.Size(94, 24);
            this.labelOOBuySell.TabIndex = 74;
            this.labelOOBuySell.Text = "買進/賣出";
            // 
            // textBoxOOTriggerNumerator
            // 
            this.textBoxOOTriggerNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOTriggerNumerator.Location = new System.Drawing.Point(231, 241);
            this.textBoxOOTriggerNumerator.Name = "textBoxOOTriggerNumerator";
            this.textBoxOOTriggerNumerator.Size = new System.Drawing.Size(122, 33);
            this.textBoxOOTriggerNumerator.TabIndex = 73;
            this.textBoxOOTriggerNumerator.Text = "0";
            // 
            // labelOOTriggerNumerator
            // 
            this.labelOOTriggerNumerator.AutoSize = true;
            this.labelOOTriggerNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOTriggerNumerator.Location = new System.Drawing.Point(3, 250);
            this.labelOOTriggerNumerator.Name = "labelOOTriggerNumerator";
            this.labelOOTriggerNumerator.Size = new System.Drawing.Size(105, 24);
            this.labelOOTriggerNumerator.TabIndex = 72;
            this.labelOOTriggerNumerator.Text = "觸發價分子";
            // 
            // textBoxOOTrigger
            // 
            this.textBoxOOTrigger.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOTrigger.Location = new System.Drawing.Point(231, 202);
            this.textBoxOOTrigger.Name = "textBoxOOTrigger";
            this.textBoxOOTrigger.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOTrigger.TabIndex = 71;
            this.textBoxOOTrigger.Text = "0";
            // 
            // labelOOTrigger
            // 
            this.labelOOTrigger.AutoSize = true;
            this.labelOOTrigger.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOTrigger.Location = new System.Drawing.Point(3, 211);
            this.labelOOTrigger.Name = "labelOOTrigger";
            this.labelOOTrigger.Size = new System.Drawing.Size(67, 24);
            this.labelOOTrigger.TabIndex = 70;
            this.labelOOTrigger.Text = "觸發價";
            // 
            // textBoxOOOrderNumerator
            // 
            this.textBoxOOOrderNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOOrderNumerator.Location = new System.Drawing.Point(111, 163);
            this.textBoxOOOrderNumerator.Name = "textBoxOOOrderNumerator";
            this.textBoxOOOrderNumerator.Size = new System.Drawing.Size(56, 33);
            this.textBoxOOOrderNumerator.TabIndex = 69;
            this.textBoxOOOrderNumerator.Text = "40";
            // 
            // labelOOOrderNumerator
            // 
            this.labelOOOrderNumerator.AutoSize = true;
            this.labelOOOrderNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOOrderNumerator.Location = new System.Drawing.Point(3, 170);
            this.labelOOOrderNumerator.Name = "labelOOOrderNumerator";
            this.labelOOOrderNumerator.Size = new System.Drawing.Size(105, 24);
            this.labelOOOrderNumerator.TabIndex = 68;
            this.labelOOOrderNumerator.Text = "委託價分子";
            // 
            // textBoxOOOrder
            // 
            this.textBoxOOOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOOrder.Location = new System.Drawing.Point(231, 122);
            this.textBoxOOOrder.Name = "textBoxOOOrder";
            this.textBoxOOOrder.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOOrder.TabIndex = 67;
            this.textBoxOOOrder.Text = "2";
            // 
            // labelOOOrder
            // 
            this.labelOOOrder.AutoSize = true;
            this.labelOOOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOOrder.Location = new System.Drawing.Point(3, 131);
            this.labelOOOrder.Name = "labelOOOrder";
            this.labelOOOrder.Size = new System.Drawing.Size(67, 24);
            this.labelOOOrder.TabIndex = 66;
            this.labelOOOrder.Text = "委託價";
            // 
            // textBoxOOYearMonth
            // 
            this.textBoxOOYearMonth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOYearMonth.Location = new System.Drawing.Point(231, 84);
            this.textBoxOOYearMonth.Name = "textBoxOOYearMonth";
            this.textBoxOOYearMonth.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOYearMonth.TabIndex = 65;
            this.textBoxOOYearMonth.Text = "202406";
            // 
            // labelOOYearMonth
            // 
            this.labelOOYearMonth.AutoSize = true;
            this.labelOOYearMonth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOYearMonth.Location = new System.Drawing.Point(3, 93);
            this.labelOOYearMonth.Name = "labelOOYearMonth";
            this.labelOOYearMonth.Size = new System.Drawing.Size(224, 24);
            this.labelOOYearMonth.TabIndex = 64;
            this.labelOOYearMonth.Text = "近月商品年月(YYYYMM)";
            // 
            // textBoxOOStockNo
            // 
            this.textBoxOOStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOStockNo.Location = new System.Drawing.Point(231, 45);
            this.textBoxOOStockNo.Name = "textBoxOOStockNo";
            this.textBoxOOStockNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOStockNo.TabIndex = 63;
            this.textBoxOOStockNo.Text = "US";
            // 
            // labelOOStockNo
            // 
            this.labelOOStockNo.AutoSize = true;
            this.labelOOStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOStockNo.Location = new System.Drawing.Point(3, 54);
            this.labelOOStockNo.Name = "labelOOStockNo";
            this.labelOOStockNo.Size = new System.Drawing.Size(143, 24);
            this.labelOOStockNo.TabIndex = 62;
            this.labelOOStockNo.Text = "海外選擇權代號";
            // 
            // textBoxOOExchangeNo
            // 
            this.textBoxOOExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOExchangeNo.Location = new System.Drawing.Point(231, 6);
            this.textBoxOOExchangeNo.Name = "textBoxOOExchangeNo";
            this.textBoxOOExchangeNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOExchangeNo.TabIndex = 61;
            this.textBoxOOExchangeNo.Text = "CBT";
            // 
            // labelOOExchangeNo
            // 
            this.labelOOExchangeNo.AutoSize = true;
            this.labelOOExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOExchangeNo.Location = new System.Drawing.Point(3, 15);
            this.labelOOExchangeNo.Name = "labelOOExchangeNo";
            this.labelOOExchangeNo.Size = new System.Drawing.Size(105, 24);
            this.labelOOExchangeNo.TabIndex = 49;
            this.labelOOExchangeNo.Text = "交易所代號";
            // 
            // checkBoxSpread
            // 
            this.checkBoxSpread.AutoSize = true;
            this.checkBoxSpread.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxSpread.Location = new System.Drawing.Point(174, 12);
            this.checkBoxSpread.Name = "checkBoxSpread";
            this.checkBoxSpread.Size = new System.Drawing.Size(162, 28);
            this.checkBoxSpread.TabIndex = 103;
            this.checkBoxSpread.Text = "是否為價差交易";
            this.checkBoxSpread.UseVisualStyleBackColor = true;
            this.checkBoxSpread.CheckedChanged += new System.EventHandler(this.checkBoxSpread_CheckedChanged);
            // 
            // checkBoxAsyncOrder
            // 
            this.checkBoxAsyncOrder.AutoSize = true;
            this.checkBoxAsyncOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxAsyncOrder.Location = new System.Drawing.Point(174, 42);
            this.checkBoxAsyncOrder.Name = "checkBoxAsyncOrder";
            this.checkBoxAsyncOrder.Size = new System.Drawing.Size(124, 28);
            this.checkBoxAsyncOrder.TabIndex = 109;
            this.checkBoxAsyncOrder.Text = "非同步委託";
            this.checkBoxAsyncOrder.UseVisualStyleBackColor = true;
            this.checkBoxAsyncOrder.CheckedChanged += new System.EventHandler(this.checkBoxAsyncOrder_CheckedChanged);
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(7, 75);
            this.richTextBoxMethodMessage.Name = "richTextBoxMethodMessage";
            this.richTextBoxMethodMessage.ReadOnly = true;
            this.richTextBoxMethodMessage.Size = new System.Drawing.Size(356, 39);
            this.richTextBoxMethodMessage.TabIndex = 32;
            this.richTextBoxMethodMessage.Text = "";
            // 
            // panelSendOrderForm
            // 
            this.panelSendOrderForm.AutoScroll = true;
            this.panelSendOrderForm.Controls.Add(this.checkBoxSpread);
            this.panelSendOrderForm.Controls.Add(this.richTextBoxMessage);
            this.panelSendOrderForm.Controls.Add(this.comboBoxAccount);
            this.panelSendOrderForm.Controls.Add(this.checkBoxAsyncOrder);
            this.panelSendOrderForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelSendOrderForm.Controls.Add(this.comboBoxUserID);
            this.panelSendOrderForm.Controls.Add(this.tabControlOrder);
            this.panelSendOrderForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSendOrderForm.Location = new System.Drawing.Point(0, 0);
            this.panelSendOrderForm.Name = "panelSendOrderForm";
            this.panelSendOrderForm.Size = new System.Drawing.Size(375, 791);
            this.panelSendOrderForm.TabIndex = 110;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Location = new System.Drawing.Point(7, 749);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(356, 39);
            this.richTextBoxMessage.TabIndex = 110;
            this.richTextBoxMessage.Text = "";
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccount.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAccount.FormattingEnabled = true;
            this.comboBoxAccount.Location = new System.Drawing.Point(3, 40);
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.Size = new System.Drawing.Size(165, 30);
            this.comboBoxAccount.TabIndex = 58;
            // 
            // comboBoxUserID
            // 
            this.comboBoxUserID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUserID.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUserID.FormattingEnabled = true;
            this.comboBoxUserID.Location = new System.Drawing.Point(3, 4);
            this.comboBoxUserID.Name = "comboBoxUserID";
            this.comboBoxUserID.Size = new System.Drawing.Size(165, 30);
            this.comboBoxUserID.TabIndex = 56;
            this.comboBoxUserID.DropDown += new System.EventHandler(this.comboBoxUserID_DropDown);
            // 
            // OFSendOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 791);
            this.Controls.Add(this.panelSendOrderForm);
            this.Name = "OFSendOrderForm";
            this.Text = "OFSendOrderForm";
            this.Load += new System.EventHandler(this.SendOrderForm_Load);
            this.tabControlOrder.ResumeLayout(false);
            this.tabPageOverseaFutureOrder.ResumeLayout(false);
            this.tabPageOverseaFutureOrder.PerformLayout();
            this.tabPageOverSeaOptionOrder.ResumeLayout(false);
            this.tabPageOverSeaOptionOrder.PerformLayout();
            this.panelSendOrderForm.ResumeLayout(false);
            this.panelSendOrderForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.TabPage tabPageOverseaFutureOrder;
        private System.Windows.Forms.TabPage tabPageOverSeaOptionOrder;
        private System.Windows.Forms.Label labelOFOrderNumerator;
        private System.Windows.Forms.Label labelOFSpecialTradeType;
        private System.Windows.Forms.Label labelOFStockNo;
        private System.Windows.Forms.Label labelOFExchangeNo;
        private System.Windows.Forms.Label labelOFOrder;
        private System.Windows.Forms.Label labelOFYearMonth;
        private System.Windows.Forms.Label labelOFTradeType;
        private System.Windows.Forms.Label labelOFDayTrade;
        private System.Windows.Forms.Label labelOFBuySell;
        private System.Windows.Forms.Label labelOFTriggerNumerator;
        private System.Windows.Forms.Label labelOFTrigger;
        private System.Windows.Forms.Button buttonSendOverSeaFutureOrder;
        private System.Windows.Forms.TextBox textBoxOFQty;
        private System.Windows.Forms.ComboBox comboBoxOFSpecialTradeType;
        private System.Windows.Forms.ComboBox comboBoxOFTradeType;
        private System.Windows.Forms.ComboBox comboBoxOFDayTrade;
        private System.Windows.Forms.ComboBox comboBoxOFBuySell;
        private System.Windows.Forms.TextBox textBoxOFTriggerNumerator;
        private System.Windows.Forms.TextBox textBoxOFTrigger;
        private System.Windows.Forms.TextBox textBoxOFOrderNumerator;
        private System.Windows.Forms.TextBox textBoxOFOrder;
        private System.Windows.Forms.TextBox textBoxOFYearMonth;
        private System.Windows.Forms.TextBox textBoxOFStockNo;
        private System.Windows.Forms.TextBox textBoxOFExchangeNo;
        private System.Windows.Forms.Label labelOFQty;
        private System.Windows.Forms.ComboBox comboBoxOOBuySell;
        private System.Windows.Forms.Label labelOOBuySell;
        private System.Windows.Forms.TextBox textBoxOOTriggerNumerator;
        private System.Windows.Forms.Label labelOOTriggerNumerator;
        private System.Windows.Forms.TextBox textBoxOOTrigger;
        private System.Windows.Forms.Label labelOOTrigger;
        private System.Windows.Forms.TextBox textBoxOOOrderNumerator;
        private System.Windows.Forms.Label labelOOOrderNumerator;
        private System.Windows.Forms.TextBox textBoxOOOrder;
        private System.Windows.Forms.Label labelOOOrder;
        private System.Windows.Forms.TextBox textBoxOOYearMonth;
        private System.Windows.Forms.Label labelOOYearMonth;
        private System.Windows.Forms.TextBox textBoxOOStockNo;
        private System.Windows.Forms.Label labelOOStockNo;
        private System.Windows.Forms.TextBox textBoxOOExchangeNo;
        private System.Windows.Forms.Label labelOOExchangeNo;
        private System.Windows.Forms.Button buttonSendOverseaOptionOrder;
        private System.Windows.Forms.TextBox textBoxOOQty;
        private System.Windows.Forms.Label labelOOQty;
        private System.Windows.Forms.ComboBox comboBoxOOCallPut;
        private System.Windows.Forms.Label labelOOCallPut;
        private System.Windows.Forms.Label labelOOStrikePrice;
        private System.Windows.Forms.TextBox textBoxOOStrikePrice;
        private System.Windows.Forms.ComboBox comboBoxOOSpecialTradeType;
        private System.Windows.Forms.Label labelOOSpecialTradeType;
        private System.Windows.Forms.ComboBox comboBoxOODayTrade;
        private System.Windows.Forms.Label labelOODayTrade;
        private System.Windows.Forms.ComboBox comboBoxOONewClose;
        private System.Windows.Forms.Label labelOONewClose;
        private System.Windows.Forms.CheckBox checkBoxAsyncOrder;
        private System.Windows.Forms.TextBox textBoxOFYearMonth2;
        private System.Windows.Forms.Label labelOFYearMonth2;
        private System.Windows.Forms.CheckBox checkBoxSpread;
        private System.Windows.Forms.Panel panelSendOrderForm;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxbstrOrderDenominator;
    }
}