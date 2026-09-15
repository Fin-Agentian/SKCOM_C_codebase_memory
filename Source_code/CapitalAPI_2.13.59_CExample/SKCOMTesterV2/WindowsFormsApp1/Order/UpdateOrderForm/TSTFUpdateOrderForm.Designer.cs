
namespace WindowsFormsApp1
{
    partial class TSTFUpdateOrderForm
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
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.tabControlUpdateOrder = new System.Windows.Forms.TabControl();
            this.tabPageStockUpdateOrder = new System.Windows.Forms.TabPage();
            this.buttonCancelOrderByStockNo = new System.Windows.Forms.Button();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxSeqNo = new System.Windows.Forms.TextBox();
            this.labelCorrectPriceBySeqNo = new System.Windows.Forms.Label();
            this.buttonDecreaseOrderBySeqNo = new System.Windows.Forms.Button();
            this.buttonCancelOrderBySeqNo = new System.Windows.Forms.Button();
            this.labelCancelOrderBySeqNo = new System.Windows.Forms.Label();
            this.textBoxStockDecreaseQty = new System.Windows.Forms.TextBox();
            this.buttonCorrectPriceByBookNo = new System.Windows.Forms.Button();
            this.buttonCorrectPriceBySeqNo = new System.Windows.Forms.Button();
            this.labelDecreaseOrderBySeqNo = new System.Windows.Forms.Label();
            this.buttonCancelOrderByBookNo = new System.Windows.Forms.Button();
            this.textBoxCancelOrderByStockNo = new System.Windows.Forms.TextBox();
            this.textBoxBookNo = new System.Windows.Forms.TextBox();
            this.labelTradeType = new System.Windows.Forms.Label();
            this.labelCancelOrderByBookNo = new System.Windows.Forms.Label();
            this.comboBoxTradeType = new System.Windows.Forms.ComboBox();
            this.labelCancelOrderByStockNo = new System.Windows.Forms.Label();
            this.labelCorrectPriceByBookNoMarketSymbol = new System.Windows.Forms.Label();
            this.comboBoxMarketSymbol = new System.Windows.Forms.ComboBox();
            this.comboBoxUpdateTFOrder = new System.Windows.Forms.ComboBox();
            this.checkBoxAsyncOrder = new System.Windows.Forms.CheckBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.comboBoxCancelOrderByStockNoAdvanceBuySell = new System.Windows.Forms.ComboBox();
            this.labelFutureBuySell = new System.Windows.Forms.Label();
            this.textBoxCancelOrderByStockNoAdvancePrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelOrderControlForm.SuspendLayout();
            this.tabControlUpdateOrder.SuspendLayout();
            this.tabPageStockUpdateOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(3, 76);
            this.richTextBoxMethodMessage.Name = "richTextBoxMethodMessage";
            this.richTextBoxMethodMessage.ReadOnly = true;
            this.richTextBoxMethodMessage.Size = new System.Drawing.Size(370, 38);
            this.richTextBoxMethodMessage.TabIndex = 31;
            this.richTextBoxMethodMessage.Text = "";
            // 
            // panelOrderControlForm
            // 
            this.panelOrderControlForm.AutoScroll = true;
            this.panelOrderControlForm.Controls.Add(this.richTextBoxMessage);
            this.panelOrderControlForm.Controls.Add(this.tabControlUpdateOrder);
            this.panelOrderControlForm.Controls.Add(this.comboBoxUpdateTFOrder);
            this.panelOrderControlForm.Controls.Add(this.checkBoxAsyncOrder);
            this.panelOrderControlForm.Controls.Add(this.comboBoxAccount);
            this.panelOrderControlForm.Controls.Add(this.comboBoxUserID);
            this.panelOrderControlForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelOrderControlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderControlForm.Location = new System.Drawing.Point(0, 0);
            this.panelOrderControlForm.Name = "panelOrderControlForm";
            this.panelOrderControlForm.Size = new System.Drawing.Size(379, 605);
            this.panelOrderControlForm.TabIndex = 103;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(3, 564);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(370, 38);
            this.richTextBoxMessage.TabIndex = 126;
            this.richTextBoxMessage.Text = "";
            // 
            // tabControlUpdateOrder
            // 
            this.tabControlUpdateOrder.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlUpdateOrder.Controls.Add(this.tabPageStockUpdateOrder);
            this.tabControlUpdateOrder.Font = new System.Drawing.Font("DFKai-SB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlUpdateOrder.Location = new System.Drawing.Point(3, 120);
            this.tabControlUpdateOrder.Name = "tabControlUpdateOrder";
            this.tabControlUpdateOrder.SelectedIndex = 0;
            this.tabControlUpdateOrder.Size = new System.Drawing.Size(374, 449);
            this.tabControlUpdateOrder.TabIndex = 107;
            // 
            // tabPageStockUpdateOrder
            // 
            this.tabPageStockUpdateOrder.AutoScroll = true;
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxCancelOrderByStockNoAdvanceBuySell);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelFutureBuySell);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxCancelOrderByStockNoAdvancePrice);
            this.tabPageStockUpdateOrder.Controls.Add(this.label1);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonCancelOrderByStockNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxPrice);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxSeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCorrectPriceBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonDecreaseOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonCancelOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxStockDecreaseQty);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonCorrectPriceByBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonCorrectPriceBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelDecreaseOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonCancelOrderByBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxCancelOrderByStockNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderByBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderByStockNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCorrectPriceByBookNoMarketSymbol);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxMarketSymbol);
            this.tabPageStockUpdateOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageStockUpdateOrder.Name = "tabPageStockUpdateOrder";
            this.tabPageStockUpdateOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStockUpdateOrder.Size = new System.Drawing.Size(366, 413);
            this.tabPageStockUpdateOrder.TabIndex = 0;
            this.tabPageStockUpdateOrder.Text = "證期選";
            this.tabPageStockUpdateOrder.UseVisualStyleBackColor = true;
            // 
            // buttonCancelOrderByStockNo
            // 
            this.buttonCancelOrderByStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancelOrderByStockNo.Location = new System.Drawing.Point(239, 147);
            this.buttonCancelOrderByStockNo.Name = "buttonCancelOrderByStockNo";
            this.buttonCancelOrderByStockNo.Size = new System.Drawing.Size(108, 32);
            this.buttonCancelOrderByStockNo.TabIndex = 59;
            this.buttonCancelOrderByStockNo.Text = "刪單(代號)";
            this.buttonCancelOrderByStockNo.UseVisualStyleBackColor = true;
            this.buttonCancelOrderByStockNo.Visible = false;
            this.buttonCancelOrderByStockNo.Click += new System.EventHandler(this.buttonCancelOrderByStockNo_Click);
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPrice.Location = new System.Drawing.Point(239, 262);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxPrice.TabIndex = 62;
            this.textBoxPrice.Visible = false;
            // 
            // textBoxSeqNo
            // 
            this.textBoxSeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxSeqNo.Location = new System.Drawing.Point(239, 6);
            this.textBoxSeqNo.Name = "textBoxSeqNo";
            this.textBoxSeqNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxSeqNo.TabIndex = 42;
            // 
            // labelCorrectPriceBySeqNo
            // 
            this.labelCorrectPriceBySeqNo.AutoSize = true;
            this.labelCorrectPriceBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCorrectPriceBySeqNo.Location = new System.Drawing.Point(6, 265);
            this.labelCorrectPriceBySeqNo.Name = "labelCorrectPriceBySeqNo";
            this.labelCorrectPriceBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelCorrectPriceBySeqNo.TabIndex = 53;
            this.labelCorrectPriceBySeqNo.Text = "請輸入修改價格:";
            this.labelCorrectPriceBySeqNo.Visible = false;
            // 
            // buttonDecreaseOrderBySeqNo
            // 
            this.buttonDecreaseOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonDecreaseOrderBySeqNo.Location = new System.Drawing.Point(7, 215);
            this.buttonDecreaseOrderBySeqNo.Name = "buttonDecreaseOrderBySeqNo";
            this.buttonDecreaseOrderBySeqNo.Size = new System.Drawing.Size(108, 32);
            this.buttonDecreaseOrderBySeqNo.TabIndex = 61;
            this.buttonDecreaseOrderBySeqNo.Text = "減量(序號)";
            this.buttonDecreaseOrderBySeqNo.UseVisualStyleBackColor = true;
            this.buttonDecreaseOrderBySeqNo.Visible = false;
            this.buttonDecreaseOrderBySeqNo.Click += new System.EventHandler(this.buttonDecreaseOrderBySeqNo_Click);
            // 
            // buttonCancelOrderBySeqNo
            // 
            this.buttonCancelOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancelOrderBySeqNo.Location = new System.Drawing.Point(7, 147);
            this.buttonCancelOrderBySeqNo.Name = "buttonCancelOrderBySeqNo";
            this.buttonCancelOrderBySeqNo.Size = new System.Drawing.Size(108, 32);
            this.buttonCancelOrderBySeqNo.TabIndex = 57;
            this.buttonCancelOrderBySeqNo.Text = "刪單(序號)";
            this.buttonCancelOrderBySeqNo.UseVisualStyleBackColor = true;
            this.buttonCancelOrderBySeqNo.Visible = false;
            this.buttonCancelOrderBySeqNo.Click += new System.EventHandler(this.buttonCancelOrderBySeqNo_Click);
            // 
            // labelCancelOrderBySeqNo
            // 
            this.labelCancelOrderBySeqNo.AutoSize = true;
            this.labelCancelOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelOrderBySeqNo.Location = new System.Drawing.Point(3, 9);
            this.labelCancelOrderBySeqNo.Name = "labelCancelOrderBySeqNo";
            this.labelCancelOrderBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelCancelOrderBySeqNo.TabIndex = 47;
            this.labelCancelOrderBySeqNo.Text = "請輸入委託序號:";
            // 
            // textBoxStockDecreaseQty
            // 
            this.textBoxStockDecreaseQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxStockDecreaseQty.Location = new System.Drawing.Point(239, 185);
            this.textBoxStockDecreaseQty.Name = "textBoxStockDecreaseQty";
            this.textBoxStockDecreaseQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxStockDecreaseQty.TabIndex = 60;
            this.textBoxStockDecreaseQty.Visible = false;
            // 
            // buttonCorrectPriceByBookNo
            // 
            this.buttonCorrectPriceByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCorrectPriceByBookNo.Location = new System.Drawing.Point(121, 371);
            this.buttonCorrectPriceByBookNo.Name = "buttonCorrectPriceByBookNo";
            this.buttonCorrectPriceByBookNo.Size = new System.Drawing.Size(108, 32);
            this.buttonCorrectPriceByBookNo.TabIndex = 64;
            this.buttonCorrectPriceByBookNo.Text = "改價(書號)";
            this.buttonCorrectPriceByBookNo.UseVisualStyleBackColor = true;
            this.buttonCorrectPriceByBookNo.Visible = false;
            this.buttonCorrectPriceByBookNo.Click += new System.EventHandler(this.buttonCorrectPriceByBookNo_Click);
            // 
            // buttonCorrectPriceBySeqNo
            // 
            this.buttonCorrectPriceBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCorrectPriceBySeqNo.Location = new System.Drawing.Point(7, 369);
            this.buttonCorrectPriceBySeqNo.Name = "buttonCorrectPriceBySeqNo";
            this.buttonCorrectPriceBySeqNo.Size = new System.Drawing.Size(108, 32);
            this.buttonCorrectPriceBySeqNo.TabIndex = 63;
            this.buttonCorrectPriceBySeqNo.Text = "改價(序號)";
            this.buttonCorrectPriceBySeqNo.UseVisualStyleBackColor = true;
            this.buttonCorrectPriceBySeqNo.Visible = false;
            this.buttonCorrectPriceBySeqNo.Click += new System.EventHandler(this.buttonCorrectPriceBySeqNo_Click);
            // 
            // labelDecreaseOrderBySeqNo
            // 
            this.labelDecreaseOrderBySeqNo.AutoSize = true;
            this.labelDecreaseOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelDecreaseOrderBySeqNo.Location = new System.Drawing.Point(3, 188);
            this.labelDecreaseOrderBySeqNo.Name = "labelDecreaseOrderBySeqNo";
            this.labelDecreaseOrderBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelDecreaseOrderBySeqNo.TabIndex = 52;
            this.labelDecreaseOrderBySeqNo.Text = "請輸入減少數量:";
            this.labelDecreaseOrderBySeqNo.Visible = false;
            // 
            // buttonCancelOrderByBookNo
            // 
            this.buttonCancelOrderByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancelOrderByBookNo.Location = new System.Drawing.Point(121, 147);
            this.buttonCancelOrderByBookNo.Name = "buttonCancelOrderByBookNo";
            this.buttonCancelOrderByBookNo.Size = new System.Drawing.Size(108, 32);
            this.buttonCancelOrderByBookNo.TabIndex = 58;
            this.buttonCancelOrderByBookNo.Text = "刪單(書號)";
            this.buttonCancelOrderByBookNo.UseVisualStyleBackColor = true;
            this.buttonCancelOrderByBookNo.Visible = false;
            this.buttonCancelOrderByBookNo.Click += new System.EventHandler(this.buttonCancelOrderByBookNo_Click);
            // 
            // textBoxCancelOrderByStockNo
            // 
            this.textBoxCancelOrderByStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxCancelOrderByStockNo.Location = new System.Drawing.Point(239, 84);
            this.textBoxCancelOrderByStockNo.Name = "textBoxCancelOrderByStockNo";
            this.textBoxCancelOrderByStockNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxCancelOrderByStockNo.TabIndex = 56;
            // 
            // textBoxBookNo
            // 
            this.textBoxBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxBookNo.Location = new System.Drawing.Point(239, 45);
            this.textBoxBookNo.Name = "textBoxBookNo";
            this.textBoxBookNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxBookNo.TabIndex = 55;
            // 
            // labelTradeType
            // 
            this.labelTradeType.AutoSize = true;
            this.labelTradeType.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTradeType.Location = new System.Drawing.Point(6, 306);
            this.labelTradeType.Name = "labelTradeType";
            this.labelTradeType.Size = new System.Drawing.Size(145, 22);
            this.labelTradeType.TabIndex = 51;
            this.labelTradeType.Text = "ROD/IOC/FOK:";
            this.labelTradeType.Visible = false;
            // 
            // labelCancelOrderByBookNo
            // 
            this.labelCancelOrderByBookNo.AutoSize = true;
            this.labelCancelOrderByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelOrderByBookNo.Location = new System.Drawing.Point(3, 48);
            this.labelCancelOrderByBookNo.Name = "labelCancelOrderByBookNo";
            this.labelCancelOrderByBookNo.Size = new System.Drawing.Size(148, 24);
            this.labelCancelOrderByBookNo.TabIndex = 49;
            this.labelCancelOrderByBookNo.Text = "請輸入委託書號:";
            // 
            // comboBoxTradeType
            // 
            this.comboBoxTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxTradeType.FormattingEnabled = true;
            this.comboBoxTradeType.Location = new System.Drawing.Point(239, 301);
            this.comboBoxTradeType.Name = "comboBoxTradeType";
            this.comboBoxTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxTradeType.TabIndex = 44;
            this.comboBoxTradeType.Visible = false;
            // 
            // labelCancelOrderByStockNo
            // 
            this.labelCancelOrderByStockNo.AutoSize = true;
            this.labelCancelOrderByStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelOrderByStockNo.Location = new System.Drawing.Point(3, 87);
            this.labelCancelOrderByStockNo.Name = "labelCancelOrderByStockNo";
            this.labelCancelOrderByStockNo.Size = new System.Drawing.Size(148, 24);
            this.labelCancelOrderByStockNo.TabIndex = 41;
            this.labelCancelOrderByStockNo.Text = "請輸入商品代號:";
            // 
            // labelCorrectPriceByBookNoMarketSymbol
            // 
            this.labelCorrectPriceByBookNoMarketSymbol.AutoSize = true;
            this.labelCorrectPriceByBookNoMarketSymbol.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCorrectPriceByBookNoMarketSymbol.Location = new System.Drawing.Point(3, 342);
            this.labelCorrectPriceByBookNoMarketSymbol.Name = "labelCorrectPriceByBookNoMarketSymbol";
            this.labelCorrectPriceByBookNoMarketSymbol.Size = new System.Drawing.Size(178, 24);
            this.labelCorrectPriceByBookNoMarketSymbol.TabIndex = 39;
            this.labelCorrectPriceByBookNoMarketSymbol.Text = "請輸入市場(※書號):";
            this.labelCorrectPriceByBookNoMarketSymbol.Visible = false;
            // 
            // comboBoxMarketSymbol
            // 
            this.comboBoxMarketSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMarketSymbol.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxMarketSymbol.FormattingEnabled = true;
            this.comboBoxMarketSymbol.Location = new System.Drawing.Point(239, 339);
            this.comboBoxMarketSymbol.Name = "comboBoxMarketSymbol";
            this.comboBoxMarketSymbol.Size = new System.Drawing.Size(121, 32);
            this.comboBoxMarketSymbol.TabIndex = 38;
            this.comboBoxMarketSymbol.Visible = false;
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
            // comboBoxCancelOrderByStockNoAdvanceBuySell
            // 
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.FormattingEnabled = true;
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.Location = new System.Drawing.Point(99, 118);
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.Name = "comboBoxCancelOrderByStockNoAdvanceBuySell";
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.Size = new System.Drawing.Size(46, 32);
            this.comboBoxCancelOrderByStockNoAdvanceBuySell.TabIndex = 72;
            // 
            // labelFutureBuySell
            // 
            this.labelFutureBuySell.AutoSize = true;
            this.labelFutureBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelFutureBuySell.Location = new System.Drawing.Point(7, 123);
            this.labelFutureBuySell.Name = "labelFutureBuySell";
            this.labelFutureBuySell.Size = new System.Drawing.Size(88, 24);
            this.labelFutureBuySell.TabIndex = 71;
            this.labelFutureBuySell.Text = "買/賣/無:";
            // 
            // textBoxCancelOrderByStockNoAdvancePrice
            // 
            this.textBoxCancelOrderByStockNoAdvancePrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxCancelOrderByStockNoAdvancePrice.Location = new System.Drawing.Point(239, 117);
            this.textBoxCancelOrderByStockNoAdvancePrice.Name = "textBoxCancelOrderByStockNoAdvancePrice";
            this.textBoxCancelOrderByStockNoAdvancePrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxCancelOrderByStockNoAdvancePrice.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(151, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 24);
            this.label1.TabIndex = 69;
            this.label1.Text = "委託價格:";
            // 
            // TSTFUpdateOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(379, 605);
            this.Controls.Add(this.panelOrderControlForm);
            this.Name = "TSTFUpdateOrderForm";
            this.Text = "TSTFUpdateOrderForm";
            this.Load += new System.EventHandler(this.OrderControlForm_Load);
            this.panelOrderControlForm.ResumeLayout(false);
            this.panelOrderControlForm.PerformLayout();
            this.tabControlUpdateOrder.ResumeLayout(false);
            this.tabPageStockUpdateOrder.ResumeLayout(false);
            this.tabPageStockUpdateOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.Panel panelOrderControlForm;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.TabControl tabControlUpdateOrder;
        private System.Windows.Forms.TabPage tabPageStockUpdateOrder;
        private System.Windows.Forms.Button buttonCorrectPriceByBookNo;
        private System.Windows.Forms.Button buttonCorrectPriceBySeqNo;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Button buttonDecreaseOrderBySeqNo;
        private System.Windows.Forms.TextBox textBoxStockDecreaseQty;
        private System.Windows.Forms.Button buttonCancelOrderByStockNo;
        private System.Windows.Forms.Button buttonCancelOrderByBookNo;
        private System.Windows.Forms.Button buttonCancelOrderBySeqNo;
        private System.Windows.Forms.TextBox textBoxCancelOrderByStockNo;
        private System.Windows.Forms.TextBox textBoxBookNo;
        private System.Windows.Forms.Label labelCorrectPriceBySeqNo;
        private System.Windows.Forms.Label labelDecreaseOrderBySeqNo;
        private System.Windows.Forms.Label labelTradeType;
        private System.Windows.Forms.Label labelCancelOrderByBookNo;
        private System.Windows.Forms.Label labelCancelOrderBySeqNo;
        private System.Windows.Forms.ComboBox comboBoxTradeType;
        private System.Windows.Forms.TextBox textBoxSeqNo;
        private System.Windows.Forms.Label labelCancelOrderByStockNo;
        private System.Windows.Forms.Label labelCorrectPriceByBookNoMarketSymbol;
        private System.Windows.Forms.ComboBox comboBoxMarketSymbol;
        private System.Windows.Forms.CheckBox checkBoxAsyncOrder;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUpdateTFOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.ComboBox comboBoxCancelOrderByStockNoAdvanceBuySell;
        private System.Windows.Forms.Label labelFutureBuySell;
        private System.Windows.Forms.TextBox textBoxCancelOrderByStockNoAdvancePrice;
        private System.Windows.Forms.Label label1;
    }
}