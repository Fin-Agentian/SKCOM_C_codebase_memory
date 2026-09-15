
namespace WindowsFormsApp1
{
    partial class TSSKProxyUpdateOrderForm
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
            this.labelnPriceMark = new System.Windows.Forms.Label();
            this.comboBoxnPriceMark = new System.Windows.Forms.ComboBox();
            this.comboBoxPeriod = new System.Windows.Forms.ComboBox();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.comboBoxnSpecialTradeType = new System.Windows.Forms.ComboBox();
            this.labelSpecialTradeType = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxSeqNo = new System.Windows.Forms.TextBox();
            this.labelCorrectPriceBySeqNo = new System.Windows.Forms.Label();
            this.buttonSendStockProxyAlter = new System.Windows.Forms.Button();
            this.labelCancelOrderBySeqNo = new System.Windows.Forms.Label();
            this.textBoxStockDecreaseQty = new System.Windows.Forms.TextBox();
            this.labelDecreaseOrderBySeqNo = new System.Windows.Forms.Label();
            this.textBoxCancelOrderByStockNo = new System.Windows.Forms.TextBox();
            this.textBoxBookNo = new System.Windows.Forms.TextBox();
            this.labelTradeType = new System.Windows.Forms.Label();
            this.labelCancelOrderByBookNo = new System.Windows.Forms.Label();
            this.comboBoxTradeType = new System.Windows.Forms.ComboBox();
            this.labelCancelOrderByStockNo = new System.Windows.Forms.Label();
            this.comboBoxbstrOrderType = new System.Windows.Forms.ComboBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
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
            this.panelOrderControlForm.Controls.Add(this.comboBoxbstrOrderType);
            this.panelOrderControlForm.Controls.Add(this.comboBoxAccount);
            this.panelOrderControlForm.Controls.Add(this.comboBoxUserID);
            this.panelOrderControlForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelOrderControlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderControlForm.Location = new System.Drawing.Point(0, 0);
            this.panelOrderControlForm.Name = "panelOrderControlForm";
            this.panelOrderControlForm.Size = new System.Drawing.Size(379, 589);
            this.panelOrderControlForm.TabIndex = 103;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(3, 548);
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
            this.tabControlUpdateOrder.Size = new System.Drawing.Size(374, 426);
            this.tabControlUpdateOrder.TabIndex = 107;
            // 
            // tabPageStockUpdateOrder
            // 
            this.tabPageStockUpdateOrder.AutoScroll = true;
            this.tabPageStockUpdateOrder.Controls.Add(this.labelnPriceMark);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxnPriceMark);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxPeriod);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelPeriod);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxnSpecialTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelSpecialTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxPrice);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxSeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCorrectPriceBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonSendStockProxyAlter);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxStockDecreaseQty);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelDecreaseOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxCancelOrderByStockNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderByBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderByStockNo);
            this.tabPageStockUpdateOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageStockUpdateOrder.Name = "tabPageStockUpdateOrder";
            this.tabPageStockUpdateOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStockUpdateOrder.Size = new System.Drawing.Size(366, 390);
            this.tabPageStockUpdateOrder.TabIndex = 0;
            this.tabPageStockUpdateOrder.Text = "證券";
            this.tabPageStockUpdateOrder.UseVisualStyleBackColor = true;
            // 
            // labelnPriceMark
            // 
            this.labelnPriceMark.AutoSize = true;
            this.labelnPriceMark.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelnPriceMark.Location = new System.Drawing.Point(3, 323);
            this.labelnPriceMark.Name = "labelnPriceMark";
            this.labelnPriceMark.Size = new System.Drawing.Size(91, 24);
            this.labelnPriceMark.TabIndex = 68;
            this.labelnPriceMark.Text = "價格旗標:";
            // 
            // comboBoxnPriceMark
            // 
            this.comboBoxnPriceMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxnPriceMark.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxnPriceMark.FormattingEnabled = true;
            this.comboBoxnPriceMark.Location = new System.Drawing.Point(231, 315);
            this.comboBoxnPriceMark.Name = "comboBoxnPriceMark";
            this.comboBoxnPriceMark.Size = new System.Drawing.Size(121, 32);
            this.comboBoxnPriceMark.TabIndex = 67;
            // 
            // comboBoxPeriod
            // 
            this.comboBoxPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPeriod.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxPeriod.FormattingEnabled = true;
            this.comboBoxPeriod.Location = new System.Drawing.Point(231, 161);
            this.comboBoxPeriod.Name = "comboBoxPeriod";
            this.comboBoxPeriod.Size = new System.Drawing.Size(121, 32);
            this.comboBoxPeriod.TabIndex = 66;
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelPeriod.Location = new System.Drawing.Point(6, 155);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(145, 24);
            this.labelPeriod.TabIndex = 65;
            this.labelPeriod.Text = "盤中/盤後/零股:";
            // 
            // comboBoxnSpecialTradeType
            // 
            this.comboBoxnSpecialTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxnSpecialTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxnSpecialTradeType.FormattingEnabled = true;
            this.comboBoxnSpecialTradeType.Location = new System.Drawing.Point(231, 123);
            this.comboBoxnSpecialTradeType.Name = "comboBoxnSpecialTradeType";
            this.comboBoxnSpecialTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxnSpecialTradeType.TabIndex = 64;
            // 
            // labelSpecialTradeType
            // 
            this.labelSpecialTradeType.AutoSize = true;
            this.labelSpecialTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelSpecialTradeType.Location = new System.Drawing.Point(6, 131);
            this.labelSpecialTradeType.Name = "labelSpecialTradeType";
            this.labelSpecialTradeType.Size = new System.Drawing.Size(99, 24);
            this.labelSpecialTradeType.TabIndex = 63;
            this.labelSpecialTradeType.Text = "市價/限價:";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPrice.Location = new System.Drawing.Point(231, 238);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxPrice.TabIndex = 62;
            // 
            // textBoxSeqNo
            // 
            this.textBoxSeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxSeqNo.Location = new System.Drawing.Point(231, 3);
            this.textBoxSeqNo.Name = "textBoxSeqNo";
            this.textBoxSeqNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxSeqNo.TabIndex = 42;
            // 
            // labelCorrectPriceBySeqNo
            // 
            this.labelCorrectPriceBySeqNo.AutoSize = true;
            this.labelCorrectPriceBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCorrectPriceBySeqNo.Location = new System.Drawing.Point(6, 247);
            this.labelCorrectPriceBySeqNo.Name = "labelCorrectPriceBySeqNo";
            this.labelCorrectPriceBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelCorrectPriceBySeqNo.TabIndex = 53;
            this.labelCorrectPriceBySeqNo.Text = "請輸入修改價格:";
            // 
            // buttonSendStockProxyAlter
            // 
            this.buttonSendStockProxyAlter.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendStockProxyAlter.Location = new System.Drawing.Point(116, 353);
            this.buttonSendStockProxyAlter.Name = "buttonSendStockProxyAlter";
            this.buttonSendStockProxyAlter.Size = new System.Drawing.Size(121, 32);
            this.buttonSendStockProxyAlter.TabIndex = 57;
            this.buttonSendStockProxyAlter.Text = "送出";
            this.buttonSendStockProxyAlter.UseVisualStyleBackColor = true;
            this.buttonSendStockProxyAlter.Click += new System.EventHandler(this.buttonSendStockProxyAlter_Click);
            // 
            // labelCancelOrderBySeqNo
            // 
            this.labelCancelOrderBySeqNo.AutoSize = true;
            this.labelCancelOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelOrderBySeqNo.Location = new System.Drawing.Point(6, 12);
            this.labelCancelOrderBySeqNo.Name = "labelCancelOrderBySeqNo";
            this.labelCancelOrderBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelCancelOrderBySeqNo.TabIndex = 47;
            this.labelCancelOrderBySeqNo.Text = "請輸入委託序號:";
            // 
            // textBoxStockDecreaseQty
            // 
            this.textBoxStockDecreaseQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxStockDecreaseQty.Location = new System.Drawing.Point(231, 199);
            this.textBoxStockDecreaseQty.Name = "textBoxStockDecreaseQty";
            this.textBoxStockDecreaseQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxStockDecreaseQty.TabIndex = 60;
            // 
            // labelDecreaseOrderBySeqNo
            // 
            this.labelDecreaseOrderBySeqNo.AutoSize = true;
            this.labelDecreaseOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelDecreaseOrderBySeqNo.Location = new System.Drawing.Point(3, 202);
            this.labelDecreaseOrderBySeqNo.Name = "labelDecreaseOrderBySeqNo";
            this.labelDecreaseOrderBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelDecreaseOrderBySeqNo.TabIndex = 52;
            this.labelDecreaseOrderBySeqNo.Text = "請輸入減少數量:";
            // 
            // textBoxCancelOrderByStockNo
            // 
            this.textBoxCancelOrderByStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxCancelOrderByStockNo.Location = new System.Drawing.Point(231, 84);
            this.textBoxCancelOrderByStockNo.Name = "textBoxCancelOrderByStockNo";
            this.textBoxCancelOrderByStockNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxCancelOrderByStockNo.TabIndex = 56;
            // 
            // textBoxBookNo
            // 
            this.textBoxBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxBookNo.Location = new System.Drawing.Point(231, 45);
            this.textBoxBookNo.Name = "textBoxBookNo";
            this.textBoxBookNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxBookNo.TabIndex = 55;
            // 
            // labelTradeType
            // 
            this.labelTradeType.AutoSize = true;
            this.labelTradeType.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTradeType.Location = new System.Drawing.Point(6, 287);
            this.labelTradeType.Name = "labelTradeType";
            this.labelTradeType.Size = new System.Drawing.Size(145, 22);
            this.labelTradeType.TabIndex = 51;
            this.labelTradeType.Text = "ROD/IOC/FOK:";
            // 
            // labelCancelOrderByBookNo
            // 
            this.labelCancelOrderByBookNo.AutoSize = true;
            this.labelCancelOrderByBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelOrderByBookNo.Location = new System.Drawing.Point(3, 54);
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
            this.comboBoxTradeType.Location = new System.Drawing.Point(231, 277);
            this.comboBoxTradeType.Name = "comboBoxTradeType";
            this.comboBoxTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxTradeType.TabIndex = 44;
            // 
            // labelCancelOrderByStockNo
            // 
            this.labelCancelOrderByStockNo.AutoSize = true;
            this.labelCancelOrderByStockNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelOrderByStockNo.Location = new System.Drawing.Point(3, 93);
            this.labelCancelOrderByStockNo.Name = "labelCancelOrderByStockNo";
            this.labelCancelOrderByStockNo.Size = new System.Drawing.Size(129, 24);
            this.labelCancelOrderByStockNo.TabIndex = 41;
            this.labelCancelOrderByStockNo.Text = "委託股票代號:";
            // 
            // comboBoxbstrOrderType
            // 
            this.comboBoxbstrOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxbstrOrderType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxbstrOrderType.FormattingEnabled = true;
            this.comboBoxbstrOrderType.Location = new System.Drawing.Point(174, 38);
            this.comboBoxbstrOrderType.Name = "comboBoxbstrOrderType";
            this.comboBoxbstrOrderType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxbstrOrderType.TabIndex = 125;
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
            // TSSKProxyUpdateOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(379, 589);
            this.Controls.Add(this.panelOrderControlForm);
            this.Name = "TSSKProxyUpdateOrderForm";
            this.Text = "TSSKProxyUpdateOrderForm";
            this.Load += new System.EventHandler(this.OrderControlForm_Load);
            this.panelOrderControlForm.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.TextBox textBoxStockDecreaseQty;
        private System.Windows.Forms.Button buttonSendStockProxyAlter;
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
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxbstrOrderType;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Label labelSpecialTradeType;
        private System.Windows.Forms.ComboBox comboBoxnSpecialTradeType;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.ComboBox comboBoxPeriod;
        private System.Windows.Forms.ComboBox comboBoxnPriceMark;
        private System.Windows.Forms.Label labelnPriceMark;
    }
}