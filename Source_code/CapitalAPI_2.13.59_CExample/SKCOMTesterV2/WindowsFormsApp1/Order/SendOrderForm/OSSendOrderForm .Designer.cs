
namespace WindowsFormsApp1
{
    partial class OSSendOrderForm
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
            this.tabPageForeignStockOrder = new System.Windows.Forms.TabPage();
            this.comboBoxForeignTradeType = new System.Windows.Forms.ComboBox();
            this.labelForeignTradeType = new System.Windows.Forms.Label();
            this.textBoxForeignCurrency1 = new System.Windows.Forms.TextBox();
            this.textBoxForeignCurrency3 = new System.Windows.Forms.TextBox();
            this.textBoxForeignCurrency2 = new System.Windows.Forms.TextBox();
            this.comboBoxForeignAccountType = new System.Windows.Forms.ComboBox();
            this.labelForeignAccountType = new System.Windows.Forms.Label();
            this.labelForeignCurrency1 = new System.Windows.Forms.Label();
            this.labelForeignCurrency2 = new System.Windows.Forms.Label();
            this.labelForeignCurrency3 = new System.Windows.Forms.Label();
            this.textBoxForeignQty = new System.Windows.Forms.TextBox();
            this.labelForeignQty = new System.Windows.Forms.Label();
            this.comboBoxForeignOrderType = new System.Windows.Forms.ComboBox();
            this.labelForeignOrderType = new System.Windows.Forms.Label();
            this.textBoxForeignPrice = new System.Windows.Forms.TextBox();
            this.labelForeignPrice = new System.Windows.Forms.Label();
            this.textBoxForeignExchangeNo = new System.Windows.Forms.TextBox();
            this.labelForeignExchangeNo = new System.Windows.Forms.Label();
            this.textBoxForeignStockID = new System.Windows.Forms.TextBox();
            this.labelForeignStock = new System.Windows.Forms.Label();
            this.buttonSendForeignStockOrder = new System.Windows.Forms.Button();
            this.checkBoxAsyncOrder = new System.Windows.Forms.CheckBox();
            this.richTextBoxMethodMessage = new System.Windows.Forms.RichTextBox();
            this.panelSendOrderForm = new System.Windows.Forms.Panel();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.tabControlOrder.SuspendLayout();
            this.tabPageForeignStockOrder.SuspendLayout();
            this.panelSendOrderForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlOrder
            // 
            this.tabControlOrder.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlOrder.Controls.Add(this.tabPageForeignStockOrder);
            this.tabControlOrder.Font = new System.Drawing.Font("DFKai-SB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlOrder.Location = new System.Drawing.Point(7, 123);
            this.tabControlOrder.Name = "tabControlOrder";
            this.tabControlOrder.SelectedIndex = 0;
            this.tabControlOrder.Size = new System.Drawing.Size(364, 467);
            this.tabControlOrder.TabIndex = 0;
            // 
            // tabPageForeignStockOrder
            // 
            this.tabPageForeignStockOrder.AutoScroll = true;
            this.tabPageForeignStockOrder.Controls.Add(this.comboBoxForeignTradeType);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignTradeType);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignCurrency1);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignCurrency3);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignCurrency2);
            this.tabPageForeignStockOrder.Controls.Add(this.comboBoxForeignAccountType);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignAccountType);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignCurrency1);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignCurrency2);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignCurrency3);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignQty);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignQty);
            this.tabPageForeignStockOrder.Controls.Add(this.comboBoxForeignOrderType);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignOrderType);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignPrice);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignPrice);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignExchangeNo);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignExchangeNo);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignStockID);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignStock);
            this.tabPageForeignStockOrder.Controls.Add(this.buttonSendForeignStockOrder);
            this.tabPageForeignStockOrder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageForeignStockOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageForeignStockOrder.Name = "tabPageForeignStockOrder";
            this.tabPageForeignStockOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForeignStockOrder.Size = new System.Drawing.Size(356, 431);
            this.tabPageForeignStockOrder.TabIndex = 3;
            this.tabPageForeignStockOrder.Text = "複委託";
            this.tabPageForeignStockOrder.UseVisualStyleBackColor = true;
            // 
            // comboBoxForeignTradeType
            // 
            this.comboBoxForeignTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxForeignTradeType.FormattingEnabled = true;
            this.comboBoxForeignTradeType.Location = new System.Drawing.Point(231, 355);
            this.comboBoxForeignTradeType.Name = "comboBoxForeignTradeType";
            this.comboBoxForeignTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxForeignTradeType.TabIndex = 98;
            // 
            // labelForeignTradeType
            // 
            this.labelForeignTradeType.AutoSize = true;
            this.labelForeignTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignTradeType.Location = new System.Drawing.Point(3, 363);
            this.labelForeignTradeType.Name = "labelForeignTradeType";
            this.labelForeignTradeType.Size = new System.Drawing.Size(67, 24);
            this.labelForeignTradeType.TabIndex = 97;
            this.labelForeignTradeType.Text = "庫存別";
            // 
            // textBoxForeignCurrency1
            // 
            this.textBoxForeignCurrency1.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignCurrency1.Location = new System.Drawing.Point(231, 122);
            this.textBoxForeignCurrency1.Name = "textBoxForeignCurrency1";
            this.textBoxForeignCurrency1.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignCurrency1.TabIndex = 96;
            // 
            // textBoxForeignCurrency3
            // 
            this.textBoxForeignCurrency3.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignCurrency3.Location = new System.Drawing.Point(231, 200);
            this.textBoxForeignCurrency3.Name = "textBoxForeignCurrency3";
            this.textBoxForeignCurrency3.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignCurrency3.TabIndex = 95;
            // 
            // textBoxForeignCurrency2
            // 
            this.textBoxForeignCurrency2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignCurrency2.Location = new System.Drawing.Point(231, 161);
            this.textBoxForeignCurrency2.Name = "textBoxForeignCurrency2";
            this.textBoxForeignCurrency2.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignCurrency2.TabIndex = 94;
            // 
            // comboBoxForeignAccountType
            // 
            this.comboBoxForeignAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignAccountType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxForeignAccountType.FormattingEnabled = true;
            this.comboBoxForeignAccountType.Location = new System.Drawing.Point(231, 84);
            this.comboBoxForeignAccountType.Name = "comboBoxForeignAccountType";
            this.comboBoxForeignAccountType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxForeignAccountType.TabIndex = 93;
            // 
            // labelForeignAccountType
            // 
            this.labelForeignAccountType.AutoSize = true;
            this.labelForeignAccountType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignAccountType.Location = new System.Drawing.Point(3, 92);
            this.labelForeignAccountType.Name = "labelForeignAccountType";
            this.labelForeignAccountType.Size = new System.Drawing.Size(105, 24);
            this.labelForeignAccountType.TabIndex = 92;
            this.labelForeignAccountType.Text = "專戶別種類";
            // 
            // labelForeignCurrency1
            // 
            this.labelForeignCurrency1.AutoSize = true;
            this.labelForeignCurrency1.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignCurrency1.Location = new System.Drawing.Point(3, 131);
            this.labelForeignCurrency1.Name = "labelForeignCurrency1";
            this.labelForeignCurrency1.Size = new System.Drawing.Size(97, 24);
            this.labelForeignCurrency1.TabIndex = 91;
            this.labelForeignCurrency1.Text = "扣款幣別1";
            // 
            // labelForeignCurrency2
            // 
            this.labelForeignCurrency2.AutoSize = true;
            this.labelForeignCurrency2.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignCurrency2.Location = new System.Drawing.Point(3, 170);
            this.labelForeignCurrency2.Name = "labelForeignCurrency2";
            this.labelForeignCurrency2.Size = new System.Drawing.Size(97, 24);
            this.labelForeignCurrency2.TabIndex = 90;
            this.labelForeignCurrency2.Text = "扣款幣別2";
            // 
            // labelForeignCurrency3
            // 
            this.labelForeignCurrency3.AutoSize = true;
            this.labelForeignCurrency3.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignCurrency3.Location = new System.Drawing.Point(3, 209);
            this.labelForeignCurrency3.Name = "labelForeignCurrency3";
            this.labelForeignCurrency3.Size = new System.Drawing.Size(97, 24);
            this.labelForeignCurrency3.TabIndex = 89;
            this.labelForeignCurrency3.Text = "扣款幣別3";
            // 
            // textBoxForeignQty
            // 
            this.textBoxForeignQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignQty.Location = new System.Drawing.Point(231, 239);
            this.textBoxForeignQty.Name = "textBoxForeignQty";
            this.textBoxForeignQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignQty.TabIndex = 88;
            // 
            // labelForeignQty
            // 
            this.labelForeignQty.AutoSize = true;
            this.labelForeignQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignQty.Location = new System.Drawing.Point(3, 248);
            this.labelForeignQty.Name = "labelForeignQty";
            this.labelForeignQty.Size = new System.Drawing.Size(67, 24);
            this.labelForeignQty.TabIndex = 87;
            this.labelForeignQty.Text = "委託量";
            // 
            // comboBoxForeignOrderType
            // 
            this.comboBoxForeignOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignOrderType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxForeignOrderType.FormattingEnabled = true;
            this.comboBoxForeignOrderType.Location = new System.Drawing.Point(231, 317);
            this.comboBoxForeignOrderType.Name = "comboBoxForeignOrderType";
            this.comboBoxForeignOrderType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxForeignOrderType.TabIndex = 86;
            // 
            // labelForeignOrderType
            // 
            this.labelForeignOrderType.AutoSize = true;
            this.labelForeignOrderType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignOrderType.Location = new System.Drawing.Point(3, 325);
            this.labelForeignOrderType.Name = "labelForeignOrderType";
            this.labelForeignOrderType.Size = new System.Drawing.Size(94, 24);
            this.labelForeignOrderType.TabIndex = 85;
            this.labelForeignOrderType.Text = "買進/賣出";
            // 
            // textBoxForeignPrice
            // 
            this.textBoxForeignPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignPrice.Location = new System.Drawing.Point(231, 278);
            this.textBoxForeignPrice.Name = "textBoxForeignPrice";
            this.textBoxForeignPrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignPrice.TabIndex = 84;
            // 
            // labelForeignPrice
            // 
            this.labelForeignPrice.AutoSize = true;
            this.labelForeignPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignPrice.Location = new System.Drawing.Point(3, 287);
            this.labelForeignPrice.Name = "labelForeignPrice";
            this.labelForeignPrice.Size = new System.Drawing.Size(67, 24);
            this.labelForeignPrice.TabIndex = 83;
            this.labelForeignPrice.Text = "委託價";
            // 
            // textBoxForeignExchangeNo
            // 
            this.textBoxForeignExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignExchangeNo.Location = new System.Drawing.Point(231, 45);
            this.textBoxForeignExchangeNo.Name = "textBoxForeignExchangeNo";
            this.textBoxForeignExchangeNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignExchangeNo.TabIndex = 82;
            // 
            // labelForeignExchangeNo
            // 
            this.labelForeignExchangeNo.AutoSize = true;
            this.labelForeignExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignExchangeNo.Location = new System.Drawing.Point(3, 54);
            this.labelForeignExchangeNo.Name = "labelForeignExchangeNo";
            this.labelForeignExchangeNo.Size = new System.Drawing.Size(206, 24);
            this.labelForeignExchangeNo.TabIndex = 76;
            this.labelForeignExchangeNo.Text = "交易所代碼，美股：US";
            // 
            // textBoxForeignStockID
            // 
            this.textBoxForeignStockID.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxForeignStockID.Location = new System.Drawing.Point(231, 6);
            this.textBoxForeignStockID.Name = "textBoxForeignStockID";
            this.textBoxForeignStockID.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignStockID.TabIndex = 48;
            // 
            // labelForeignStock
            // 
            this.labelForeignStock.AutoSize = true;
            this.labelForeignStock.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignStock.Location = new System.Drawing.Point(3, 15);
            this.labelForeignStock.Name = "labelForeignStock";
            this.labelForeignStock.Size = new System.Drawing.Size(86, 24);
            this.labelForeignStock.TabIndex = 47;
            this.labelForeignStock.Text = "股票代號";
            // 
            // buttonSendForeignStockOrder
            // 
            this.buttonSendForeignStockOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendForeignStockOrder.Location = new System.Drawing.Point(111, 394);
            this.buttonSendForeignStockOrder.Name = "buttonSendForeignStockOrder";
            this.buttonSendForeignStockOrder.Size = new System.Drawing.Size(121, 32);
            this.buttonSendForeignStockOrder.TabIndex = 46;
            this.buttonSendForeignStockOrder.Text = "送出";
            this.buttonSendForeignStockOrder.UseVisualStyleBackColor = true;
            this.buttonSendForeignStockOrder.Click += new System.EventHandler(this.buttonSendForeignStockOrder_Click);
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
            this.panelSendOrderForm.Controls.Add(this.richTextBoxMessage);
            this.panelSendOrderForm.Controls.Add(this.comboBoxAccount);
            this.panelSendOrderForm.Controls.Add(this.checkBoxAsyncOrder);
            this.panelSendOrderForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelSendOrderForm.Controls.Add(this.comboBoxUserID);
            this.panelSendOrderForm.Controls.Add(this.tabControlOrder);
            this.panelSendOrderForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSendOrderForm.Location = new System.Drawing.Point(0, 0);
            this.panelSendOrderForm.Name = "panelSendOrderForm";
            this.panelSendOrderForm.Size = new System.Drawing.Size(374, 638);
            this.panelSendOrderForm.TabIndex = 110;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Location = new System.Drawing.Point(7, 592);
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
            // OSSendOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 638);
            this.Controls.Add(this.panelSendOrderForm);
            this.Name = "OSSendOrderForm";
            this.Text = "OSSendOrderForm";
            this.Load += new System.EventHandler(this.SendOrderForm_Load);
            this.tabControlOrder.ResumeLayout(false);
            this.tabPageForeignStockOrder.ResumeLayout(false);
            this.tabPageForeignStockOrder.PerformLayout();
            this.panelSendOrderForm.ResumeLayout(false);
            this.panelSendOrderForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.TabPage tabPageForeignStockOrder;
        private System.Windows.Forms.TextBox textBoxForeignCurrency1;
        private System.Windows.Forms.TextBox textBoxForeignCurrency3;
        private System.Windows.Forms.TextBox textBoxForeignCurrency2;
        private System.Windows.Forms.ComboBox comboBoxForeignAccountType;
        private System.Windows.Forms.Label labelForeignAccountType;
        private System.Windows.Forms.Label labelForeignCurrency1;
        private System.Windows.Forms.Label labelForeignCurrency2;
        private System.Windows.Forms.Label labelForeignCurrency3;
        private System.Windows.Forms.TextBox textBoxForeignQty;
        private System.Windows.Forms.Label labelForeignQty;
        private System.Windows.Forms.ComboBox comboBoxForeignOrderType;
        private System.Windows.Forms.Label labelForeignOrderType;
        private System.Windows.Forms.TextBox textBoxForeignPrice;
        private System.Windows.Forms.Label labelForeignPrice;
        private System.Windows.Forms.TextBox textBoxForeignExchangeNo;
        private System.Windows.Forms.Label labelForeignExchangeNo;
        private System.Windows.Forms.TextBox textBoxForeignStockID;
        private System.Windows.Forms.Label labelForeignStock;
        private System.Windows.Forms.Button buttonSendForeignStockOrder;
        private System.Windows.Forms.ComboBox comboBoxForeignTradeType;
        private System.Windows.Forms.Label labelForeignTradeType;
        private System.Windows.Forms.CheckBox checkBoxAsyncOrder;
        private System.Windows.Forms.Panel panelSendOrderForm;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
    }
}