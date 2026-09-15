
namespace WindowsFormsApp1
{
    partial class TFSKProxyUpdateOrderForm
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
            this.comboBoxFutureReserved = new System.Windows.Forms.ComboBox();
            this.labelFutureReserved = new System.Windows.Forms.Label();
            this.buttonSendOptionProxyAlter = new System.Windows.Forms.Button();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxSeqNo = new System.Windows.Forms.TextBox();
            this.labelCorrectPriceBySeqNo = new System.Windows.Forms.Label();
            this.buttonSendFutureProxyAlter = new System.Windows.Forms.Button();
            this.labelCancelOrderBySeqNo = new System.Windows.Forms.Label();
            this.textBoxStockDecreaseQty = new System.Windows.Forms.TextBox();
            this.labelDecreaseOrderBySeqNo = new System.Windows.Forms.Label();
            this.textBoxBookNo = new System.Windows.Forms.TextBox();
            this.labelTradeType = new System.Windows.Forms.Label();
            this.labelCancelOrderByBookNo = new System.Windows.Forms.Label();
            this.comboBoxTradeType = new System.Windows.Forms.ComboBox();
            this.comboBoxUpdateTFOrder = new System.Windows.Forms.ComboBox();
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
            this.panelOrderControlForm.Controls.Add(this.comboBoxUpdateTFOrder);
            this.panelOrderControlForm.Controls.Add(this.comboBoxAccount);
            this.panelOrderControlForm.Controls.Add(this.comboBoxUserID);
            this.panelOrderControlForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelOrderControlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderControlForm.Location = new System.Drawing.Point(0, 0);
            this.panelOrderControlForm.Name = "panelOrderControlForm";
            this.panelOrderControlForm.Size = new System.Drawing.Size(379, 473);
            this.panelOrderControlForm.TabIndex = 103;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(3, 431);
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
            this.tabControlUpdateOrder.Size = new System.Drawing.Size(374, 309);
            this.tabControlUpdateOrder.TabIndex = 107;
            // 
            // tabPageStockUpdateOrder
            // 
            this.tabPageStockUpdateOrder.AutoScroll = true;
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxFutureReserved);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelFutureReserved);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonSendOptionProxyAlter);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxPrice);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxSeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCorrectPriceBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.buttonSendFutureProxyAlter);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxStockDecreaseQty);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelDecreaseOrderBySeqNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.textBoxBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelTradeType);
            this.tabPageStockUpdateOrder.Controls.Add(this.labelCancelOrderByBookNo);
            this.tabPageStockUpdateOrder.Controls.Add(this.comboBoxTradeType);
            this.tabPageStockUpdateOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageStockUpdateOrder.Name = "tabPageStockUpdateOrder";
            this.tabPageStockUpdateOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStockUpdateOrder.Size = new System.Drawing.Size(366, 273);
            this.tabPageStockUpdateOrder.TabIndex = 0;
            this.tabPageStockUpdateOrder.Text = "期選";
            this.tabPageStockUpdateOrder.UseVisualStyleBackColor = true;
            // 
            // comboBoxFutureReserved
            // 
            this.comboBoxFutureReserved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFutureReserved.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxFutureReserved.FormattingEnabled = true;
            this.comboBoxFutureReserved.Location = new System.Drawing.Point(231, 164);
            this.comboBoxFutureReserved.Name = "comboBoxFutureReserved";
            this.comboBoxFutureReserved.Size = new System.Drawing.Size(121, 32);
            this.comboBoxFutureReserved.TabIndex = 71;
            // 
            // labelFutureReserved
            // 
            this.labelFutureReserved.AutoSize = true;
            this.labelFutureReserved.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelFutureReserved.Location = new System.Drawing.Point(6, 172);
            this.labelFutureReserved.Name = "labelFutureReserved";
            this.labelFutureReserved.Size = new System.Drawing.Size(53, 24);
            this.labelFutureReserved.TabIndex = 70;
            this.labelFutureReserved.Text = "盤別:";
            // 
            // buttonSendOptionProxyAlter
            // 
            this.buttonSendOptionProxyAlter.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendOptionProxyAlter.Location = new System.Drawing.Point(185, 238);
            this.buttonSendOptionProxyAlter.Name = "buttonSendOptionProxyAlter";
            this.buttonSendOptionProxyAlter.Size = new System.Drawing.Size(130, 32);
            this.buttonSendOptionProxyAlter.TabIndex = 63;
            this.buttonSendOptionProxyAlter.Text = "送出(選擇權)";
            this.buttonSendOptionProxyAlter.UseVisualStyleBackColor = true;
            this.buttonSendOptionProxyAlter.Click += new System.EventHandler(this.buttonSendOptionProxyAlter_Click);
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPrice.Location = new System.Drawing.Point(231, 125);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxPrice.TabIndex = 62;
            this.textBoxPrice.Text = "0";
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
            this.labelCorrectPriceBySeqNo.Location = new System.Drawing.Point(6, 134);
            this.labelCorrectPriceBySeqNo.Name = "labelCorrectPriceBySeqNo";
            this.labelCorrectPriceBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelCorrectPriceBySeqNo.TabIndex = 53;
            this.labelCorrectPriceBySeqNo.Text = "請輸入修改價格:";
            // 
            // buttonSendFutureProxyAlter
            // 
            this.buttonSendFutureProxyAlter.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendFutureProxyAlter.Location = new System.Drawing.Point(46, 238);
            this.buttonSendFutureProxyAlter.Name = "buttonSendFutureProxyAlter";
            this.buttonSendFutureProxyAlter.Size = new System.Drawing.Size(108, 32);
            this.buttonSendFutureProxyAlter.TabIndex = 57;
            this.buttonSendFutureProxyAlter.Text = "送出(期貨)";
            this.buttonSendFutureProxyAlter.UseVisualStyleBackColor = true;
            this.buttonSendFutureProxyAlter.Click += new System.EventHandler(this.buttonSendFutureProxyAlter_Click);
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
            this.textBoxStockDecreaseQty.Location = new System.Drawing.Point(231, 86);
            this.textBoxStockDecreaseQty.Name = "textBoxStockDecreaseQty";
            this.textBoxStockDecreaseQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxStockDecreaseQty.TabIndex = 60;
            this.textBoxStockDecreaseQty.Text = "0";
            // 
            // labelDecreaseOrderBySeqNo
            // 
            this.labelDecreaseOrderBySeqNo.AutoSize = true;
            this.labelDecreaseOrderBySeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelDecreaseOrderBySeqNo.Location = new System.Drawing.Point(3, 95);
            this.labelDecreaseOrderBySeqNo.Name = "labelDecreaseOrderBySeqNo";
            this.labelDecreaseOrderBySeqNo.Size = new System.Drawing.Size(148, 24);
            this.labelDecreaseOrderBySeqNo.TabIndex = 52;
            this.labelDecreaseOrderBySeqNo.Text = "請輸入減少數量:";
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
            this.labelTradeType.Location = new System.Drawing.Point(6, 212);
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
            this.comboBoxTradeType.Location = new System.Drawing.Point(231, 202);
            this.comboBoxTradeType.Name = "comboBoxTradeType";
            this.comboBoxTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxTradeType.TabIndex = 44;
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
            // TFSKProxyUpdateOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(379, 473);
            this.Controls.Add(this.panelOrderControlForm);
            this.Name = "TFSKProxyUpdateOrderForm";
            this.Text = "TFSKProxyUpdateOrderForm";
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
        private System.Windows.Forms.Button buttonSendFutureProxyAlter;
        private System.Windows.Forms.TextBox textBoxBookNo;
        private System.Windows.Forms.Label labelCorrectPriceBySeqNo;
        private System.Windows.Forms.Label labelDecreaseOrderBySeqNo;
        private System.Windows.Forms.Label labelTradeType;
        private System.Windows.Forms.Label labelCancelOrderByBookNo;
        private System.Windows.Forms.Label labelCancelOrderBySeqNo;
        private System.Windows.Forms.ComboBox comboBoxTradeType;
        private System.Windows.Forms.TextBox textBoxSeqNo;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUpdateTFOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Button buttonSendOptionProxyAlter;
        private System.Windows.Forms.Label labelFutureReserved;
        private System.Windows.Forms.ComboBox comboBoxFutureReserved;
    }
}