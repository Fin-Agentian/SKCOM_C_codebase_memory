
namespace WindowsFormsApp1
{
    partial class OSSKProxySendOrderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelbstrCurrency123 = new System.Windows.Forms.Label();
            this.comboBoxbstrExchangeNo = new System.Windows.Forms.ComboBox();
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
            this.labelForeignExchangeNo = new System.Windows.Forms.Label();
            this.textBoxForeignStockID = new System.Windows.Forms.TextBox();
            this.labelForeignStock = new System.Windows.Forms.Label();
            this.buttonSendForeignStockProxyOrder = new System.Windows.Forms.Button();
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
            this.tabControlOrder.Size = new System.Drawing.Size(380, 537);
            this.tabControlOrder.TabIndex = 0;
            // 
            // tabPageForeignStockOrder
            // 
            this.tabPageForeignStockOrder.AutoScroll = true;
            this.tabPageForeignStockOrder.Controls.Add(this.label1);
            this.tabPageForeignStockOrder.Controls.Add(this.labelbstrCurrency123);
            this.tabPageForeignStockOrder.Controls.Add(this.comboBoxbstrExchangeNo);
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
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignExchangeNo);
            this.tabPageForeignStockOrder.Controls.Add(this.textBoxForeignStockID);
            this.tabPageForeignStockOrder.Controls.Add(this.labelForeignStock);
            this.tabPageForeignStockOrder.Controls.Add(this.buttonSendForeignStockProxyOrder);
            this.tabPageForeignStockOrder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageForeignStockOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageForeignStockOrder.Name = "tabPageForeignStockOrder";
            this.tabPageForeignStockOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForeignStockOrder.Size = new System.Drawing.Size(372, 501);
            this.tabPageForeignStockOrder.TabIndex = 3;
            this.tabPageForeignStockOrder.Text = "複委託";
            this.tabPageForeignStockOrder.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(1, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 15);
            this.label1.TabIndex = 101;
            this.label1.Text = "賣出美股且庫存別為VIEWTRADE時，股數才有小數位數";
            // 
            // labelbstrCurrency123
            // 
            this.labelbstrCurrency123.AutoSize = true;
            this.labelbstrCurrency123.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelbstrCurrency123.Location = new System.Drawing.Point(1, 250);
            this.labelbstrCurrency123.Name = "labelbstrCurrency123";
            this.labelbstrCurrency123.Size = new System.Drawing.Size(359, 15);
            this.labelbstrCurrency123.TabIndex = 100;
            this.labelbstrCurrency123.Text = "幣別可輸入 : HKD、NTD、USD、JPY、SGD、EUR、AUD、CNY、GBP";
            // 
            // comboBoxbstrExchangeNo
            // 
            this.comboBoxbstrExchangeNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxbstrExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxbstrExchangeNo.FormattingEnabled = true;
            this.comboBoxbstrExchangeNo.Location = new System.Drawing.Point(231, 45);
            this.comboBoxbstrExchangeNo.Name = "comboBoxbstrExchangeNo";
            this.comboBoxbstrExchangeNo.Size = new System.Drawing.Size(121, 32);
            this.comboBoxbstrExchangeNo.TabIndex = 99;
            // 
            // comboBoxForeignTradeType
            // 
            this.comboBoxForeignTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxForeignTradeType.FormattingEnabled = true;
            this.comboBoxForeignTradeType.Location = new System.Drawing.Point(231, 427);
            this.comboBoxForeignTradeType.Name = "comboBoxForeignTradeType";
            this.comboBoxForeignTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxForeignTradeType.TabIndex = 98;
            // 
            // labelForeignTradeType
            // 
            this.labelForeignTradeType.AutoSize = true;
            this.labelForeignTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignTradeType.Location = new System.Drawing.Point(3, 435);
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
            this.textBoxForeignQty.Location = new System.Drawing.Point(231, 276);
            this.textBoxForeignQty.Name = "textBoxForeignQty";
            this.textBoxForeignQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignQty.TabIndex = 88;
            // 
            // labelForeignQty
            // 
            this.labelForeignQty.AutoSize = true;
            this.labelForeignQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignQty.Location = new System.Drawing.Point(3, 285);
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
            this.comboBoxForeignOrderType.Location = new System.Drawing.Point(231, 389);
            this.comboBoxForeignOrderType.Name = "comboBoxForeignOrderType";
            this.comboBoxForeignOrderType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxForeignOrderType.TabIndex = 86;
            // 
            // labelForeignOrderType
            // 
            this.labelForeignOrderType.AutoSize = true;
            this.labelForeignOrderType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignOrderType.Location = new System.Drawing.Point(3, 397);
            this.labelForeignOrderType.Name = "labelForeignOrderType";
            this.labelForeignOrderType.Size = new System.Drawing.Size(94, 24);
            this.labelForeignOrderType.TabIndex = 85;
            this.labelForeignOrderType.Text = "買進/賣出";
            // 
            // textBoxForeignPrice
            // 
            this.textBoxForeignPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxForeignPrice.Location = new System.Drawing.Point(231, 350);
            this.textBoxForeignPrice.Name = "textBoxForeignPrice";
            this.textBoxForeignPrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignPrice.TabIndex = 84;
            // 
            // labelForeignPrice
            // 
            this.labelForeignPrice.AutoSize = true;
            this.labelForeignPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignPrice.Location = new System.Drawing.Point(3, 359);
            this.labelForeignPrice.Name = "labelForeignPrice";
            this.labelForeignPrice.Size = new System.Drawing.Size(67, 24);
            this.labelForeignPrice.TabIndex = 83;
            this.labelForeignPrice.Text = "委託價";
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
            // buttonSendForeignStockProxyOrder
            // 
            this.buttonSendForeignStockProxyOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendForeignStockProxyOrder.Location = new System.Drawing.Point(231, 465);
            this.buttonSendForeignStockProxyOrder.Name = "buttonSendForeignStockProxyOrder";
            this.buttonSendForeignStockProxyOrder.Size = new System.Drawing.Size(121, 32);
            this.buttonSendForeignStockProxyOrder.TabIndex = 46;
            this.buttonSendForeignStockProxyOrder.Text = "送出";
            this.buttonSendForeignStockProxyOrder.UseVisualStyleBackColor = true;
            this.buttonSendForeignStockProxyOrder.Click += new System.EventHandler(this.buttonSendForeignStockProxyOrder_Click);
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(7, 75);
            this.richTextBoxMethodMessage.Name = "richTextBoxMethodMessage";
            this.richTextBoxMethodMessage.ReadOnly = true;
            this.richTextBoxMethodMessage.Size = new System.Drawing.Size(376, 39);
            this.richTextBoxMethodMessage.TabIndex = 32;
            this.richTextBoxMethodMessage.Text = "";
            // 
            // panelSendOrderForm
            // 
            this.panelSendOrderForm.AutoScroll = true;
            this.panelSendOrderForm.Controls.Add(this.richTextBoxMessage);
            this.panelSendOrderForm.Controls.Add(this.comboBoxAccount);
            this.panelSendOrderForm.Controls.Add(this.richTextBoxMethodMessage);
            this.panelSendOrderForm.Controls.Add(this.comboBoxUserID);
            this.panelSendOrderForm.Controls.Add(this.tabControlOrder);
            this.panelSendOrderForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSendOrderForm.Location = new System.Drawing.Point(0, 0);
            this.panelSendOrderForm.Name = "panelSendOrderForm";
            this.panelSendOrderForm.Size = new System.Drawing.Size(391, 704);
            this.panelSendOrderForm.TabIndex = 110;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Location = new System.Drawing.Point(11, 662);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(372, 96);
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
            // OSSKProxySendOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 704);
            this.Controls.Add(this.panelSendOrderForm);
            this.Name = "OSSKProxySendOrderForm";
            this.Text = "OSSKProxySendOrderForm";
            this.Load += new System.EventHandler(this.SendOrderForm_Load);
            this.tabControlOrder.ResumeLayout(false);
            this.tabPageForeignStockOrder.ResumeLayout(false);
            this.tabPageForeignStockOrder.PerformLayout();
            this.panelSendOrderForm.ResumeLayout(false);
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
        private System.Windows.Forms.Label labelForeignExchangeNo;
        private System.Windows.Forms.TextBox textBoxForeignStockID;
        private System.Windows.Forms.Label labelForeignStock;
        private System.Windows.Forms.Button buttonSendForeignStockProxyOrder;
        private System.Windows.Forms.ComboBox comboBoxForeignTradeType;
        private System.Windows.Forms.Label labelForeignTradeType;
        private System.Windows.Forms.Panel panelSendOrderForm;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.ComboBox comboBoxbstrExchangeNo;
        private System.Windows.Forms.Label labelbstrCurrency123;
        private System.Windows.Forms.Label label1;
    }
}