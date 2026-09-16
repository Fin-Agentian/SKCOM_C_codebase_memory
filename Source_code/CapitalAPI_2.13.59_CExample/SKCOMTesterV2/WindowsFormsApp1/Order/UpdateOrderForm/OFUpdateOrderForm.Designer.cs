
namespace WindowsFormsApp1
{
    partial class OFUpdateOrderForm
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
            this.richTextBoxMethodMessage = new System.Windows.Forms.RichTextBox();
            this.panelOrderControlForm = new System.Windows.Forms.Panel();
            this.tabControlUpdateOrder = new System.Windows.Forms.TabControl();
            this.tabPageOverseaFutureOrder = new System.Windows.Forms.TabPage();
            this.buttonOverSeaCorrectPriceByBookNo = new System.Windows.Forms.Button();
            this.comboBoxOOCallPut = new System.Windows.Forms.ComboBox();
            this.labelOOCallPut = new System.Windows.Forms.Label();
            this.textBoxOOStrikePrice = new System.Windows.Forms.TextBox();
            this.labelOOStrikePrice = new System.Windows.Forms.Label();
            this.textBoxOFOrderDenominator = new System.Windows.Forms.TextBox();
            this.textBoxOFOrderNumerator = new System.Windows.Forms.TextBox();
            this.textBoxOFOrder = new System.Windows.Forms.TextBox();
            this.labelOFOrderDenominator = new System.Windows.Forms.Label();
            this.textBoxOFYearMonth2 = new System.Windows.Forms.TextBox();
            this.textBoxOFStockNo2 = new System.Windows.Forms.TextBox();
            this.labelOFStockNo2 = new System.Windows.Forms.Label();
            this.buttonOverSeaDecreaseOrderBySeqNo = new System.Windows.Forms.Button();
            this.textBoxOverseaFutureDecreaseQty = new System.Windows.Forms.TextBox();
            this.labelDecreaseOrderBySeqNo2 = new System.Windows.Forms.Label();
            this.buttonOverSeaCancelOrderByBookNo = new System.Windows.Forms.Button();
            this.buttonOverSeaCancelOrderBySeqNo = new System.Windows.Forms.Button();
            this.textBoxOverSeaCancelOrderByBookNo = new System.Windows.Forms.TextBox();
            this.textBoxOverSeaCancelOrderBySeqNo = new System.Windows.Forms.TextBox();
            this.labelOverSeaCancelOrderByBookNo = new System.Windows.Forms.Label();
            this.labelOverSeaCancelOrderBySeqNo = new System.Windows.Forms.Label();
            this.checkBoxSpread = new System.Windows.Forms.CheckBox();
            this.labelOFYearMonth2 = new System.Windows.Forms.Label();
            this.textBoxOFYearMonth = new System.Windows.Forms.TextBox();
            this.textBoxOFStockNo = new System.Windows.Forms.TextBox();
            this.textBoxOFExchangeNo = new System.Windows.Forms.TextBox();
            this.labelOFOrder = new System.Windows.Forms.Label();
            this.labelOFYearMonth = new System.Windows.Forms.Label();
            this.labelOFOrderNumerator = new System.Windows.Forms.Label();
            this.labelOFStockNo = new System.Windows.Forms.Label();
            this.labelOFExchangeNo = new System.Windows.Forms.Label();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.comboBoxUpdateTFOrder = new System.Windows.Forms.ComboBox();
            this.checkBoxAsyncOrder = new System.Windows.Forms.CheckBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.panelOrderControlForm.SuspendLayout();
            this.tabControlUpdateOrder.SuspendLayout();
            this.tabPageOverseaFutureOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(3, 76);
            this.richTextBoxMethodMessage.Name = "richTextBoxMethodMessage";
            this.richTextBoxMethodMessage.ReadOnly = true;
            this.richTextBoxMethodMessage.Size = new System.Drawing.Size(374, 37);
            this.richTextBoxMethodMessage.TabIndex = 31;
            this.richTextBoxMethodMessage.Text = "";
            // 
            // panelOrderControlForm
            // 
            this.panelOrderControlForm.AutoScroll = true;
            this.panelOrderControlForm.Controls.Add(this.tabControlUpdateOrder);
            this.panelOrderControlForm.Controls.Add(this.richTextBoxMessage);
            this.panelOrderControlForm.Controls.Add(this.comboBoxUpdateTFOrder);
            this.panelOrderControlForm.Controls.Add(this.checkBoxAsyncOrder);
            this.panelOrderControlForm.Controls.Add(this.comboBoxAccount);
            this.panelOrderControlForm.Controls.Add(this.comboBoxUserID);
            this.panelOrderControlForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelOrderControlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderControlForm.Location = new System.Drawing.Point(0, 0);
            this.panelOrderControlForm.Name = "panelOrderControlForm";
            this.panelOrderControlForm.Size = new System.Drawing.Size(381, 870);
            this.panelOrderControlForm.TabIndex = 103;
            // 
            // tabControlUpdateOrder
            // 
            this.tabControlUpdateOrder.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlUpdateOrder.Controls.Add(this.tabPageOverseaFutureOrder);
            this.tabControlUpdateOrder.Font = new System.Drawing.Font("DFKai-SB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlUpdateOrder.Location = new System.Drawing.Point(3, 119);
            this.tabControlUpdateOrder.Name = "tabControlUpdateOrder";
            this.tabControlUpdateOrder.SelectedIndex = 0;
            this.tabControlUpdateOrder.Size = new System.Drawing.Size(374, 703);
            this.tabControlUpdateOrder.TabIndex = 107;
            // 
            // tabPageOverseaFutureOrder
            // 
            this.tabPageOverseaFutureOrder.AutoScroll = true;
            this.tabPageOverseaFutureOrder.Controls.Add(this.buttonOverSeaCorrectPriceByBookNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.comboBoxOOCallPut);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOOCallPut);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOOStrikePrice);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOOStrikePrice);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFOrderDenominator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFOrderNumerator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFOrder);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFOrderDenominator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFYearMonth2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFStockNo2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFStockNo2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.buttonOverSeaDecreaseOrderBySeqNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOverseaFutureDecreaseQty);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelDecreaseOrderBySeqNo2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.buttonOverSeaCancelOrderByBookNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.buttonOverSeaCancelOrderBySeqNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOverSeaCancelOrderByBookNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOverSeaCancelOrderBySeqNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOverSeaCancelOrderByBookNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOverSeaCancelOrderBySeqNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.checkBoxSpread);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFYearMonth2);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFYearMonth);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFStockNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.textBoxOFExchangeNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFOrder);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFYearMonth);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFOrderNumerator);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFStockNo);
            this.tabPageOverseaFutureOrder.Controls.Add(this.labelOFExchangeNo);
            this.tabPageOverseaFutureOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageOverseaFutureOrder.Name = "tabPageOverseaFutureOrder";
            this.tabPageOverseaFutureOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverseaFutureOrder.Size = new System.Drawing.Size(366, 667);
            this.tabPageOverseaFutureOrder.TabIndex = 4;
            this.tabPageOverseaFutureOrder.Text = "海期權";
            this.tabPageOverseaFutureOrder.UseVisualStyleBackColor = true;
            // 
            // buttonOverSeaCorrectPriceByBookNo
            // 
            this.buttonOverSeaCorrectPriceByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonOverSeaCorrectPriceByBookNo.Location = new System.Drawing.Point(7, 619);
            this.buttonOverSeaCorrectPriceByBookNo.Name = "buttonOverSeaCorrectPriceByBookNo";
            this.buttonOverSeaCorrectPriceByBookNo.Size = new System.Drawing.Size(108, 32);
            this.buttonOverSeaCorrectPriceByBookNo.TabIndex = 124;
            this.buttonOverSeaCorrectPriceByBookNo.Text = "改價(書號)";
            this.buttonOverSeaCorrectPriceByBookNo.UseVisualStyleBackColor = true;
            this.buttonOverSeaCorrectPriceByBookNo.Visible = false;
            this.buttonOverSeaCorrectPriceByBookNo.Click += new System.EventHandler(this.buttonOverSeaCorrectPriceByBookNo_Click);
            // 
            // comboBoxOOCallPut
            // 
            this.comboBoxOOCallPut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOOCallPut.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxOOCallPut.FormattingEnabled = true;
            this.comboBoxOOCallPut.Location = new System.Drawing.Point(231, 541);
            this.comboBoxOOCallPut.Name = "comboBoxOOCallPut";
            this.comboBoxOOCallPut.Size = new System.Drawing.Size(121, 32);
            this.comboBoxOOCallPut.TabIndex = 123;
            this.comboBoxOOCallPut.Visible = false;
            // 
            // labelOOCallPut
            // 
            this.labelOOCallPut.AutoSize = true;
            this.labelOOCallPut.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOOCallPut.Location = new System.Drawing.Point(3, 551);
            this.labelOOCallPut.Name = "labelOOCallPut";
            this.labelOOCallPut.Size = new System.Drawing.Size(104, 22);
            this.labelOOCallPut.TabIndex = 122;
            this.labelOOCallPut.Text = "CALL/PUT";
            this.labelOOCallPut.Visible = false;
            // 
            // textBoxOOStrikePrice
            // 
            this.textBoxOOStrikePrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOOStrikePrice.Location = new System.Drawing.Point(231, 502);
            this.textBoxOOStrikePrice.Name = "textBoxOOStrikePrice";
            this.textBoxOOStrikePrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxOOStrikePrice.TabIndex = 121;
            this.textBoxOOStrikePrice.Visible = false;
            // 
            // labelOOStrikePrice
            // 
            this.labelOOStrikePrice.AutoSize = true;
            this.labelOOStrikePrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOOStrikePrice.Location = new System.Drawing.Point(3, 511);
            this.labelOOStrikePrice.Name = "labelOOStrikePrice";
            this.labelOOStrikePrice.Size = new System.Drawing.Size(168, 24);
            this.labelOOStrikePrice.TabIndex = 120;
            this.labelOOStrikePrice.Text = "履約價(改期貨帶0)";
            this.labelOOStrikePrice.Visible = false;
            // 
            // textBoxOFOrderDenominator
            // 
            this.textBoxOFOrderDenominator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFOrderDenominator.Location = new System.Drawing.Point(231, 463);
            this.textBoxOFOrderDenominator.Name = "textBoxOFOrderDenominator";
            this.textBoxOFOrderDenominator.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFOrderDenominator.TabIndex = 119;
            this.textBoxOFOrderDenominator.Visible = false;
            // 
            // textBoxOFOrderNumerator
            // 
            this.textBoxOFOrderNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFOrderNumerator.Location = new System.Drawing.Point(231, 424);
            this.textBoxOFOrderNumerator.Name = "textBoxOFOrderNumerator";
            this.textBoxOFOrderNumerator.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFOrderNumerator.TabIndex = 118;
            this.textBoxOFOrderNumerator.Visible = false;
            // 
            // textBoxOFOrder
            // 
            this.textBoxOFOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFOrder.Location = new System.Drawing.Point(231, 385);
            this.textBoxOFOrder.Name = "textBoxOFOrder";
            this.textBoxOFOrder.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFOrder.TabIndex = 117;
            this.textBoxOFOrder.Visible = false;
            // 
            // labelOFOrderDenominator
            // 
            this.labelOFOrderDenominator.AutoSize = true;
            this.labelOFOrderDenominator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFOrderDenominator.Location = new System.Drawing.Point(3, 472);
            this.labelOFOrderDenominator.Name = "labelOFOrderDenominator";
            this.labelOFOrderDenominator.Size = new System.Drawing.Size(124, 24);
            this.labelOFOrderDenominator.TabIndex = 116;
            this.labelOFOrderDenominator.Text = "新委託價分母";
            this.labelOFOrderDenominator.Visible = false;
            // 
            // textBoxOFYearMonth2
            // 
            this.textBoxOFYearMonth2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFYearMonth2.Location = new System.Drawing.Point(231, 346);
            this.textBoxOFYearMonth2.Name = "textBoxOFYearMonth2";
            this.textBoxOFYearMonth2.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFYearMonth2.TabIndex = 115;
            this.textBoxOFYearMonth2.Visible = false;
            // 
            // textBoxOFStockNo2
            // 
            this.textBoxOFStockNo2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFStockNo2.Location = new System.Drawing.Point(231, 268);
            this.textBoxOFStockNo2.Name = "textBoxOFStockNo2";
            this.textBoxOFStockNo2.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFStockNo2.TabIndex = 114;
            this.textBoxOFStockNo2.Visible = false;
            // 
            // labelOFStockNo2
            // 
            this.labelOFStockNo2.AutoSize = true;
            this.labelOFStockNo2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFStockNo2.Location = new System.Drawing.Point(3, 277);
            this.labelOFStockNo2.Name = "labelOFStockNo2";
            this.labelOFStockNo2.Size = new System.Drawing.Size(143, 24);
            this.labelOFStockNo2.TabIndex = 113;
            this.labelOFStockNo2.Text = "海外期價差代號";
            this.labelOFStockNo2.Visible = false;
            // 
            // buttonOverSeaDecreaseOrderBySeqNo
            // 
            this.buttonOverSeaDecreaseOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonOverSeaDecreaseOrderBySeqNo.Location = new System.Drawing.Point(7, 148);
            this.buttonOverSeaDecreaseOrderBySeqNo.Name = "buttonOverSeaDecreaseOrderBySeqNo";
            this.buttonOverSeaDecreaseOrderBySeqNo.Size = new System.Drawing.Size(108, 32);
            this.buttonOverSeaDecreaseOrderBySeqNo.TabIndex = 112;
            this.buttonOverSeaDecreaseOrderBySeqNo.Text = "減量(序號)";
            this.buttonOverSeaDecreaseOrderBySeqNo.UseVisualStyleBackColor = true;
            this.buttonOverSeaDecreaseOrderBySeqNo.Visible = false;
            this.buttonOverSeaDecreaseOrderBySeqNo.Click += new System.EventHandler(this.buttonOverSeaDecreaseOrderBySeqNo_Click);
            // 
            // textBoxOverseaFutureDecreaseQty
            // 
            this.textBoxOverseaFutureDecreaseQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOverseaFutureDecreaseQty.Location = new System.Drawing.Point(231, 112);
            this.textBoxOverseaFutureDecreaseQty.Name = "textBoxOverseaFutureDecreaseQty";
            this.textBoxOverseaFutureDecreaseQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxOverseaFutureDecreaseQty.TabIndex = 111;
            this.textBoxOverseaFutureDecreaseQty.Visible = false;
            // 
            // labelDecreaseOrderBySeqNo2
            // 
            this.labelDecreaseOrderBySeqNo2.AutoSize = true;
            this.labelDecreaseOrderBySeqNo2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelDecreaseOrderBySeqNo2.Location = new System.Drawing.Point(3, 121);
            this.labelDecreaseOrderBySeqNo2.Name = "labelDecreaseOrderBySeqNo2";
            this.labelDecreaseOrderBySeqNo2.Size = new System.Drawing.Size(143, 24);
            this.labelDecreaseOrderBySeqNo2.TabIndex = 110;
            this.labelDecreaseOrderBySeqNo2.Text = "請輸入減少數量";
            this.labelDecreaseOrderBySeqNo2.Visible = false;
            // 
            // buttonOverSeaCancelOrderByBookNo
            // 
            this.buttonOverSeaCancelOrderByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonOverSeaCancelOrderByBookNo.Location = new System.Drawing.Point(121, 80);
            this.buttonOverSeaCancelOrderByBookNo.Name = "buttonOverSeaCancelOrderByBookNo";
            this.buttonOverSeaCancelOrderByBookNo.Size = new System.Drawing.Size(108, 32);
            this.buttonOverSeaCancelOrderByBookNo.TabIndex = 109;
            this.buttonOverSeaCancelOrderByBookNo.Text = "刪單(書號)";
            this.buttonOverSeaCancelOrderByBookNo.UseVisualStyleBackColor = true;
            this.buttonOverSeaCancelOrderByBookNo.Visible = false;
            this.buttonOverSeaCancelOrderByBookNo.Click += new System.EventHandler(this.buttonOverSeaCancelOrderByBookNo_Click);
            // 
            // buttonOverSeaCancelOrderBySeqNo
            // 
            this.buttonOverSeaCancelOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonOverSeaCancelOrderBySeqNo.Location = new System.Drawing.Point(7, 80);
            this.buttonOverSeaCancelOrderBySeqNo.Name = "buttonOverSeaCancelOrderBySeqNo";
            this.buttonOverSeaCancelOrderBySeqNo.Size = new System.Drawing.Size(108, 32);
            this.buttonOverSeaCancelOrderBySeqNo.TabIndex = 108;
            this.buttonOverSeaCancelOrderBySeqNo.Text = "刪單(序號)";
            this.buttonOverSeaCancelOrderBySeqNo.UseVisualStyleBackColor = true;
            this.buttonOverSeaCancelOrderBySeqNo.Visible = false;
            this.buttonOverSeaCancelOrderBySeqNo.Click += new System.EventHandler(this.buttonOverSeaCancelOrderBySeqNo_Click);
            // 
            // textBoxOverSeaCancelOrderByBookNo
            // 
            this.textBoxOverSeaCancelOrderByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOverSeaCancelOrderByBookNo.Location = new System.Drawing.Point(231, 44);
            this.textBoxOverSeaCancelOrderByBookNo.Name = "textBoxOverSeaCancelOrderByBookNo";
            this.textBoxOverSeaCancelOrderByBookNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOverSeaCancelOrderByBookNo.TabIndex = 107;
            // 
            // textBoxOverSeaCancelOrderBySeqNo
            // 
            this.textBoxOverSeaCancelOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOverSeaCancelOrderBySeqNo.Location = new System.Drawing.Point(231, 6);
            this.textBoxOverSeaCancelOrderBySeqNo.Name = "textBoxOverSeaCancelOrderBySeqNo";
            this.textBoxOverSeaCancelOrderBySeqNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOverSeaCancelOrderBySeqNo.TabIndex = 106;
            // 
            // labelOverSeaCancelOrderByBookNo
            // 
            this.labelOverSeaCancelOrderByBookNo.AutoSize = true;
            this.labelOverSeaCancelOrderByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOverSeaCancelOrderByBookNo.Location = new System.Drawing.Point(3, 53);
            this.labelOverSeaCancelOrderByBookNo.Name = "labelOverSeaCancelOrderByBookNo";
            this.labelOverSeaCancelOrderByBookNo.Size = new System.Drawing.Size(143, 24);
            this.labelOverSeaCancelOrderByBookNo.TabIndex = 105;
            this.labelOverSeaCancelOrderByBookNo.Text = "請輸入委託書號";
            // 
            // labelOverSeaCancelOrderBySeqNo
            // 
            this.labelOverSeaCancelOrderBySeqNo.AutoSize = true;
            this.labelOverSeaCancelOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOverSeaCancelOrderBySeqNo.Location = new System.Drawing.Point(3, 15);
            this.labelOverSeaCancelOrderBySeqNo.Name = "labelOverSeaCancelOrderBySeqNo";
            this.labelOverSeaCancelOrderBySeqNo.Size = new System.Drawing.Size(143, 24);
            this.labelOverSeaCancelOrderBySeqNo.TabIndex = 104;
            this.labelOverSeaCancelOrderBySeqNo.Text = "請輸入委託序號";
            // 
            // checkBoxSpread
            // 
            this.checkBoxSpread.AutoSize = true;
            this.checkBoxSpread.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxSpread.Location = new System.Drawing.Point(7, 585);
            this.checkBoxSpread.Name = "checkBoxSpread";
            this.checkBoxSpread.Size = new System.Drawing.Size(162, 28);
            this.checkBoxSpread.TabIndex = 103;
            this.checkBoxSpread.Text = "是否為價差改價";
            this.checkBoxSpread.UseVisualStyleBackColor = true;
            this.checkBoxSpread.Visible = false;
            this.checkBoxSpread.CheckedChanged += new System.EventHandler(this.checkBoxSpread_CheckedChanged);
            // 
            // labelOFYearMonth2
            // 
            this.labelOFYearMonth2.AutoSize = true;
            this.labelOFYearMonth2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFYearMonth2.Location = new System.Drawing.Point(3, 355);
            this.labelOFYearMonth2.Name = "labelOFYearMonth2";
            this.labelOFYearMonth2.Size = new System.Drawing.Size(224, 24);
            this.labelOFYearMonth2.TabIndex = 101;
            this.labelOFYearMonth2.Text = "遠月商品年月(YYYYMM)";
            this.labelOFYearMonth2.Visible = false;
            // 
            // textBoxOFYearMonth
            // 
            this.textBoxOFYearMonth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFYearMonth.Location = new System.Drawing.Point(231, 307);
            this.textBoxOFYearMonth.Name = "textBoxOFYearMonth";
            this.textBoxOFYearMonth.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFYearMonth.TabIndex = 66;
            this.textBoxOFYearMonth.Visible = false;
            // 
            // textBoxOFStockNo
            // 
            this.textBoxOFStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFStockNo.Location = new System.Drawing.Point(231, 229);
            this.textBoxOFStockNo.Name = "textBoxOFStockNo";
            this.textBoxOFStockNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFStockNo.TabIndex = 61;
            this.textBoxOFStockNo.Visible = false;
            // 
            // textBoxOFExchangeNo
            // 
            this.textBoxOFExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxOFExchangeNo.Location = new System.Drawing.Point(231, 190);
            this.textBoxOFExchangeNo.Name = "textBoxOFExchangeNo";
            this.textBoxOFExchangeNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxOFExchangeNo.TabIndex = 60;
            this.textBoxOFExchangeNo.Visible = false;
            // 
            // labelOFOrder
            // 
            this.labelOFOrder.AutoSize = true;
            this.labelOFOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFOrder.Location = new System.Drawing.Point(3, 394);
            this.labelOFOrder.Name = "labelOFOrder";
            this.labelOFOrder.Size = new System.Drawing.Size(86, 24);
            this.labelOFOrder.TabIndex = 53;
            this.labelOFOrder.Text = "新委託價";
            this.labelOFOrder.Visible = false;
            // 
            // labelOFYearMonth
            // 
            this.labelOFYearMonth.AutoSize = true;
            this.labelOFYearMonth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFYearMonth.Location = new System.Drawing.Point(3, 315);
            this.labelOFYearMonth.Name = "labelOFYearMonth";
            this.labelOFYearMonth.Size = new System.Drawing.Size(224, 24);
            this.labelOFYearMonth.TabIndex = 52;
            this.labelOFYearMonth.Text = "近月商品年月(YYYYMM)";
            this.labelOFYearMonth.Visible = false;
            // 
            // labelOFOrderNumerator
            // 
            this.labelOFOrderNumerator.AutoSize = true;
            this.labelOFOrderNumerator.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFOrderNumerator.Location = new System.Drawing.Point(3, 433);
            this.labelOFOrderNumerator.Name = "labelOFOrderNumerator";
            this.labelOFOrderNumerator.Size = new System.Drawing.Size(124, 24);
            this.labelOFOrderNumerator.TabIndex = 51;
            this.labelOFOrderNumerator.Text = "新委託價分子";
            this.labelOFOrderNumerator.Visible = false;
            // 
            // labelOFStockNo
            // 
            this.labelOFStockNo.AutoSize = true;
            this.labelOFStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFStockNo.Location = new System.Drawing.Point(3, 238);
            this.labelOFStockNo.Name = "labelOFStockNo";
            this.labelOFStockNo.Size = new System.Drawing.Size(124, 24);
            this.labelOFStockNo.TabIndex = 49;
            this.labelOFStockNo.Text = "海外期權代號";
            this.labelOFStockNo.Visible = false;
            // 
            // labelOFExchangeNo
            // 
            this.labelOFExchangeNo.AutoSize = true;
            this.labelOFExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelOFExchangeNo.Location = new System.Drawing.Point(3, 199);
            this.labelOFExchangeNo.Name = "labelOFExchangeNo";
            this.labelOFExchangeNo.Size = new System.Drawing.Size(105, 24);
            this.labelOFExchangeNo.TabIndex = 48;
            this.labelOFExchangeNo.Text = "交易所代號";
            this.labelOFExchangeNo.Visible = false;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(7, 828);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(366, 37);
            this.richTextBoxMessage.TabIndex = 126;
            this.richTextBoxMessage.Text = "";
            // 
            // comboBoxUpdateTFOrder
            // 
            this.comboBoxUpdateTFOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpdateTFOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxUpdateTFOrder.FormattingEnabled = true;
            this.comboBoxUpdateTFOrder.Location = new System.Drawing.Point(174, 38);
            this.comboBoxUpdateTFOrder.Name = "comboBoxUpdateTFOrder";
            this.comboBoxUpdateTFOrder.Size = new System.Drawing.Size(121, 32);
            this.comboBoxUpdateTFOrder.TabIndex = 125;
            this.comboBoxUpdateTFOrder.SelectedIndexChanged += new System.EventHandler(this.comboBoxUpdateTFOrder_SelectedIndexChanged);
            // 
            // checkBoxAsyncOrder
            // 
            this.checkBoxAsyncOrder.AutoSize = true;
            this.checkBoxAsyncOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxAsyncOrder.Location = new System.Drawing.Point(174, 7);
            this.checkBoxAsyncOrder.Name = "checkBoxAsyncOrder";
            this.checkBoxAsyncOrder.Size = new System.Drawing.Size(109, 25);
            this.checkBoxAsyncOrder.TabIndex = 111;
            this.checkBoxAsyncOrder.Text = "非同步委託";
            this.checkBoxAsyncOrder.UseVisualStyleBackColor = true;
            this.checkBoxAsyncOrder.CheckedChanged += new System.EventHandler(this.checkBoxAsyncOrder_CheckedChanged);
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccount.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAccount.FormattingEnabled = true;
            this.comboBoxAccount.Location = new System.Drawing.Point(3, 40);
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.Size = new System.Drawing.Size(165, 30);
            this.comboBoxAccount.TabIndex = 114;
            // 
            // comboBoxUserID
            // 
            this.comboBoxUserID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUserID.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUserID.FormattingEnabled = true;
            this.comboBoxUserID.Location = new System.Drawing.Point(3, 4);
            this.comboBoxUserID.Name = "comboBoxUserID";
            this.comboBoxUserID.Size = new System.Drawing.Size(165, 30);
            this.comboBoxUserID.TabIndex = 104;
            this.comboBoxUserID.DropDown += new System.EventHandler(this.comboBoxUserID_DropDown);
            // 
            // OFUpdateOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(381, 870);
            this.Controls.Add(this.panelOrderControlForm);
            this.Name = "OFUpdateOrderForm";
            this.Text = "OFUpdateOrderForm";
            this.Load += new System.EventHandler(this.OrderControlForm_Load);
            this.panelOrderControlForm.ResumeLayout(false);
            this.panelOrderControlForm.PerformLayout();
            this.tabControlUpdateOrder.ResumeLayout(false);
            this.tabPageOverseaFutureOrder.ResumeLayout(false);
            this.tabPageOverseaFutureOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.Panel panelOrderControlForm;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.TabControl tabControlUpdateOrder;
        private System.Windows.Forms.TabPage tabPageOverseaFutureOrder;
        private System.Windows.Forms.Button buttonOverSeaCorrectPriceByBookNo;
        private System.Windows.Forms.ComboBox comboBoxOOCallPut;
        private System.Windows.Forms.Label labelOOCallPut;
        private System.Windows.Forms.TextBox textBoxOOStrikePrice;
        private System.Windows.Forms.Label labelOOStrikePrice;
        private System.Windows.Forms.TextBox textBoxOFOrderDenominator;
        private System.Windows.Forms.TextBox textBoxOFOrderNumerator;
        private System.Windows.Forms.TextBox textBoxOFOrder;
        private System.Windows.Forms.Label labelOFOrderDenominator;
        private System.Windows.Forms.TextBox textBoxOFYearMonth2;
        private System.Windows.Forms.TextBox textBoxOFStockNo2;
        private System.Windows.Forms.Label labelOFStockNo2;
        private System.Windows.Forms.Button buttonOverSeaDecreaseOrderBySeqNo;
        private System.Windows.Forms.TextBox textBoxOverseaFutureDecreaseQty;
        private System.Windows.Forms.Label labelDecreaseOrderBySeqNo2;
        private System.Windows.Forms.Button buttonOverSeaCancelOrderByBookNo;
        private System.Windows.Forms.Button buttonOverSeaCancelOrderBySeqNo;
        private System.Windows.Forms.TextBox textBoxOverSeaCancelOrderByBookNo;
        private System.Windows.Forms.TextBox textBoxOverSeaCancelOrderBySeqNo;
        private System.Windows.Forms.Label labelOverSeaCancelOrderByBookNo;
        private System.Windows.Forms.Label labelOverSeaCancelOrderBySeqNo;
        private System.Windows.Forms.CheckBox checkBoxSpread;
        private System.Windows.Forms.Label labelOFYearMonth2;
        private System.Windows.Forms.TextBox textBoxOFYearMonth;
        private System.Windows.Forms.TextBox textBoxOFStockNo;
        private System.Windows.Forms.TextBox textBoxOFExchangeNo;
        private System.Windows.Forms.Label labelOFOrder;
        private System.Windows.Forms.Label labelOFYearMonth;
        private System.Windows.Forms.Label labelOFOrderNumerator;
        private System.Windows.Forms.Label labelOFStockNo;
        private System.Windows.Forms.Label labelOFExchangeNo;
        private System.Windows.Forms.CheckBox checkBoxAsyncOrder;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUpdateTFOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
    }
}