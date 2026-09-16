
namespace WindowsFormsApp1
{
    partial class TSSendOrderForm
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
            this.tabPageStockOrder = new System.Windows.Forms.TabPage();
            this.labelTradeType = new System.Windows.Forms.Label();
            this.labelFlag = new System.Windows.Forms.Label();
            this.labelSpecialTradeType = new System.Windows.Forms.Label();
            this.buttonSendStockOrder = new System.Windows.Forms.Button();
            this.labelBuySell = new System.Windows.Forms.Label();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.comboBoxPeriod = new System.Windows.Forms.ComboBox();
            this.labelPrime = new System.Windows.Forms.Label();
            this.comboBoxPrime = new System.Windows.Forms.ComboBox();
            this.comboBoxnSpecialTradeType = new System.Windows.Forms.ComboBox();
            this.comboBoxnTradeType = new System.Windows.Forms.ComboBox();
            this.textBoxnQty = new System.Windows.Forms.TextBox();
            this.labelnQty = new System.Windows.Forms.Label();
            this.textBoxbstrPrice = new System.Windows.Forms.TextBox();
            this.labelbstrPrice = new System.Windows.Forms.Label();
            this.comboBoxBuySell = new System.Windows.Forms.ComboBox();
            this.comboBoxFlag = new System.Windows.Forms.ComboBox();
            this.textBoxStockID = new System.Windows.Forms.TextBox();
            this.labelStockID = new System.Windows.Forms.Label();
            this.checkBoxAsyncOrder = new System.Windows.Forms.CheckBox();
            this.richTextBoxMethodMessage = new System.Windows.Forms.RichTextBox();
            this.panelSendOrderForm = new System.Windows.Forms.Panel();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.buttonSendStockOddLotOrder = new System.Windows.Forms.Button();
            this.tabControlOrder.SuspendLayout();
            this.tabPageStockOrder.SuspendLayout();
            this.panelSendOrderForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlOrder
            // 
            this.tabControlOrder.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlOrder.Controls.Add(this.tabPageStockOrder);
            this.tabControlOrder.Font = new System.Drawing.Font("DFKai-SB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlOrder.Location = new System.Drawing.Point(7, 123);
            this.tabControlOrder.Name = "tabControlOrder";
            this.tabControlOrder.SelectedIndex = 0;
            this.tabControlOrder.Size = new System.Drawing.Size(364, 420);
            this.tabControlOrder.TabIndex = 0;
            // 
            // tabPageStockOrder
            // 
            this.tabPageStockOrder.AutoScroll = true;
            this.tabPageStockOrder.Controls.Add(this.buttonSendStockOddLotOrder);
            this.tabPageStockOrder.Controls.Add(this.labelTradeType);
            this.tabPageStockOrder.Controls.Add(this.labelFlag);
            this.tabPageStockOrder.Controls.Add(this.labelSpecialTradeType);
            this.tabPageStockOrder.Controls.Add(this.buttonSendStockOrder);
            this.tabPageStockOrder.Controls.Add(this.labelBuySell);
            this.tabPageStockOrder.Controls.Add(this.labelPeriod);
            this.tabPageStockOrder.Controls.Add(this.comboBoxPeriod);
            this.tabPageStockOrder.Controls.Add(this.labelPrime);
            this.tabPageStockOrder.Controls.Add(this.comboBoxPrime);
            this.tabPageStockOrder.Controls.Add(this.comboBoxnSpecialTradeType);
            this.tabPageStockOrder.Controls.Add(this.comboBoxnTradeType);
            this.tabPageStockOrder.Controls.Add(this.textBoxnQty);
            this.tabPageStockOrder.Controls.Add(this.labelnQty);
            this.tabPageStockOrder.Controls.Add(this.textBoxbstrPrice);
            this.tabPageStockOrder.Controls.Add(this.labelbstrPrice);
            this.tabPageStockOrder.Controls.Add(this.comboBoxBuySell);
            this.tabPageStockOrder.Controls.Add(this.comboBoxFlag);
            this.tabPageStockOrder.Controls.Add(this.textBoxStockID);
            this.tabPageStockOrder.Controls.Add(this.labelStockID);
            this.tabPageStockOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageStockOrder.Name = "tabPageStockOrder";
            this.tabPageStockOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStockOrder.Size = new System.Drawing.Size(356, 384);
            this.tabPageStockOrder.TabIndex = 0;
            this.tabPageStockOrder.Text = "證券";
            this.tabPageStockOrder.UseVisualStyleBackColor = true;
            // 
            // labelTradeType
            // 
            this.labelTradeType.AutoSize = true;
            this.labelTradeType.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTradeType.Location = new System.Drawing.Point(3, 208);
            this.labelTradeType.Name = "labelTradeType";
            this.labelTradeType.Size = new System.Drawing.Size(145, 22);
            this.labelTradeType.TabIndex = 53;
            this.labelTradeType.Text = "ROD/IOC/FOK:";
            // 
            // labelFlag
            // 
            this.labelFlag.AutoSize = true;
            this.labelFlag.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelFlag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelFlag.Location = new System.Drawing.Point(3, 168);
            this.labelFlag.Name = "labelFlag";
            this.labelFlag.Size = new System.Drawing.Size(191, 24);
            this.labelFlag.TabIndex = 52;
            this.labelFlag.Text = "現股/融資/融券/無券:";
            // 
            // labelSpecialTradeType
            // 
            this.labelSpecialTradeType.AutoSize = true;
            this.labelSpecialTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelSpecialTradeType.Location = new System.Drawing.Point(3, 244);
            this.labelSpecialTradeType.Name = "labelSpecialTradeType";
            this.labelSpecialTradeType.Size = new System.Drawing.Size(99, 24);
            this.labelSpecialTradeType.TabIndex = 51;
            this.labelSpecialTradeType.Text = "市價/限價:";
            // 
            // buttonSendStockOrder
            // 
            this.buttonSendStockOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendStockOrder.Location = new System.Drawing.Point(231, 349);
            this.buttonSendStockOrder.Name = "buttonSendStockOrder";
            this.buttonSendStockOrder.Size = new System.Drawing.Size(121, 32);
            this.buttonSendStockOrder.TabIndex = 45;
            this.buttonSendStockOrder.Text = "送出";
            this.buttonSendStockOrder.UseVisualStyleBackColor = true;
            this.buttonSendStockOrder.Click += new System.EventHandler(this.buttonSendStockOrder_Click);
            // 
            // labelBuySell
            // 
            this.labelBuySell.AutoSize = true;
            this.labelBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelBuySell.ForeColor = System.Drawing.Color.Maroon;
            this.labelBuySell.Location = new System.Drawing.Point(3, 321);
            this.labelBuySell.Name = "labelBuySell";
            this.labelBuySell.Size = new System.Drawing.Size(99, 24);
            this.labelBuySell.TabIndex = 50;
            this.labelBuySell.Text = "買進/賣出:";
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelPeriod.Location = new System.Drawing.Point(3, 91);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(145, 24);
            this.labelPeriod.TabIndex = 49;
            this.labelPeriod.Text = "盤中/盤後/零股:";
            // 
            // comboBoxPeriod
            // 
            this.comboBoxPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPeriod.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxPeriod.FormattingEnabled = true;
            this.comboBoxPeriod.Location = new System.Drawing.Point(231, 83);
            this.comboBoxPeriod.Name = "comboBoxPeriod";
            this.comboBoxPeriod.Size = new System.Drawing.Size(121, 32);
            this.comboBoxPeriod.TabIndex = 48;
            // 
            // labelPrime
            // 
            this.labelPrime.AutoSize = true;
            this.labelPrime.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelPrime.Location = new System.Drawing.Point(3, 53);
            this.labelPrime.Name = "labelPrime";
            this.labelPrime.Size = new System.Drawing.Size(137, 24);
            this.labelPrime.TabIndex = 47;
            this.labelPrime.Text = "上市上櫃/興櫃:";
            // 
            // comboBoxPrime
            // 
            this.comboBoxPrime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrime.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxPrime.FormattingEnabled = true;
            this.comboBoxPrime.Location = new System.Drawing.Point(231, 45);
            this.comboBoxPrime.Name = "comboBoxPrime";
            this.comboBoxPrime.Size = new System.Drawing.Size(121, 32);
            this.comboBoxPrime.TabIndex = 46;
            // 
            // comboBoxnSpecialTradeType
            // 
            this.comboBoxnSpecialTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxnSpecialTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxnSpecialTradeType.FormattingEnabled = true;
            this.comboBoxnSpecialTradeType.Location = new System.Drawing.Point(231, 236);
            this.comboBoxnSpecialTradeType.Name = "comboBoxnSpecialTradeType";
            this.comboBoxnSpecialTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxnSpecialTradeType.TabIndex = 44;
            this.comboBoxnSpecialTradeType.SelectedIndexChanged += new System.EventHandler(this.comboBoxnSpecialTradeType_SelectedIndexChanged);
            // 
            // comboBoxnTradeType
            // 
            this.comboBoxnTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxnTradeType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxnTradeType.FormattingEnabled = true;
            this.comboBoxnTradeType.Location = new System.Drawing.Point(231, 198);
            this.comboBoxnTradeType.Name = "comboBoxnTradeType";
            this.comboBoxnTradeType.Size = new System.Drawing.Size(121, 32);
            this.comboBoxnTradeType.TabIndex = 43;
            // 
            // textBoxnQty
            // 
            this.textBoxnQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxnQty.Location = new System.Drawing.Point(231, 121);
            this.textBoxnQty.Name = "textBoxnQty";
            this.textBoxnQty.Size = new System.Drawing.Size(121, 33);
            this.textBoxnQty.TabIndex = 42;
            // 
            // labelnQty
            // 
            this.labelnQty.AutoSize = true;
            this.labelnQty.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelnQty.ForeColor = System.Drawing.Color.Maroon;
            this.labelnQty.Location = new System.Drawing.Point(3, 130);
            this.labelnQty.Name = "labelnQty";
            this.labelnQty.Size = new System.Drawing.Size(184, 24);
            this.labelnQty.TabIndex = 41;
            this.labelnQty.Text = "整股(張)/零股(股數):";
            // 
            // textBoxbstrPrice
            // 
            this.textBoxbstrPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxbstrPrice.Location = new System.Drawing.Point(231, 274);
            this.textBoxbstrPrice.Name = "textBoxbstrPrice";
            this.textBoxbstrPrice.Size = new System.Drawing.Size(121, 33);
            this.textBoxbstrPrice.TabIndex = 40;
            // 
            // labelbstrPrice
            // 
            this.labelbstrPrice.AutoSize = true;
            this.labelbstrPrice.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelbstrPrice.Location = new System.Drawing.Point(3, 283);
            this.labelbstrPrice.Name = "labelbstrPrice";
            this.labelbstrPrice.Size = new System.Drawing.Size(72, 24);
            this.labelbstrPrice.TabIndex = 39;
            this.labelbstrPrice.Text = "委託價:";
            // 
            // comboBoxBuySell
            // 
            this.comboBoxBuySell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuySell.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxBuySell.FormattingEnabled = true;
            this.comboBoxBuySell.Location = new System.Drawing.Point(231, 313);
            this.comboBoxBuySell.Name = "comboBoxBuySell";
            this.comboBoxBuySell.Size = new System.Drawing.Size(121, 32);
            this.comboBoxBuySell.TabIndex = 38;
            // 
            // comboBoxFlag
            // 
            this.comboBoxFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFlag.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxFlag.FormattingEnabled = true;
            this.comboBoxFlag.Location = new System.Drawing.Point(231, 160);
            this.comboBoxFlag.Name = "comboBoxFlag";
            this.comboBoxFlag.Size = new System.Drawing.Size(121, 32);
            this.comboBoxFlag.TabIndex = 37;
            // 
            // textBoxStockID
            // 
            this.textBoxStockID.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxStockID.Location = new System.Drawing.Point(231, 6);
            this.textBoxStockID.Name = "textBoxStockID";
            this.textBoxStockID.Size = new System.Drawing.Size(121, 33);
            this.textBoxStockID.TabIndex = 35;
            // 
            // labelStockID
            // 
            this.labelStockID.AutoSize = true;
            this.labelStockID.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelStockID.ForeColor = System.Drawing.Color.Maroon;
            this.labelStockID.Location = new System.Drawing.Point(3, 15);
            this.labelStockID.Name = "labelStockID";
            this.labelStockID.Size = new System.Drawing.Size(91, 24);
            this.labelStockID.TabIndex = 34;
            this.labelStockID.Text = "股票代號:";
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
            this.panelSendOrderForm.Size = new System.Drawing.Size(371, 590);
            this.panelSendOrderForm.TabIndex = 110;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Location = new System.Drawing.Point(7, 545);
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
            // buttonSendStockOddLotOrder
            // 
            this.buttonSendStockOddLotOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendStockOddLotOrder.ForeColor = System.Drawing.Color.Maroon;
            this.buttonSendStockOddLotOrder.Location = new System.Drawing.Point(19, 349);
            this.buttonSendStockOddLotOrder.Name = "buttonSendStockOddLotOrder";
            this.buttonSendStockOddLotOrder.Size = new System.Drawing.Size(121, 32);
            this.buttonSendStockOddLotOrder.TabIndex = 54;
            this.buttonSendStockOddLotOrder.Text = "盤零送出";
            this.buttonSendStockOddLotOrder.UseVisualStyleBackColor = true;
            this.buttonSendStockOddLotOrder.Click += new System.EventHandler(this.buttonSendStockOddLotOrder_Click);
            // 
            // TSSendOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 590);
            this.Controls.Add(this.panelSendOrderForm);
            this.Name = "TSSendOrderForm";
            this.Text = "TSSendOrderForm";
            this.Load += new System.EventHandler(this.SendOrderForm_Load);
            this.tabControlOrder.ResumeLayout(false);
            this.tabPageStockOrder.ResumeLayout(false);
            this.tabPageStockOrder.PerformLayout();
            this.panelSendOrderForm.ResumeLayout(false);
            this.panelSendOrderForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlOrder;
        private System.Windows.Forms.TabPage tabPageStockOrder;
        private System.Windows.Forms.Label labelStockID;
        private System.Windows.Forms.TextBox textBoxStockID;
        private System.Windows.Forms.ComboBox comboBoxFlag;
        private System.Windows.Forms.ComboBox comboBoxBuySell;
        private System.Windows.Forms.Label labelbstrPrice;
        private System.Windows.Forms.TextBox textBoxbstrPrice;
        private System.Windows.Forms.Label labelnQty;
        private System.Windows.Forms.TextBox textBoxnQty;
        private System.Windows.Forms.ComboBox comboBoxnTradeType;
        private System.Windows.Forms.ComboBox comboBoxnSpecialTradeType;
        private System.Windows.Forms.Button buttonSendStockOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.ComboBox comboBoxPrime;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.ComboBox comboBoxPeriod;
        private System.Windows.Forms.Label labelPrime;
        private System.Windows.Forms.Label labelSpecialTradeType;
        private System.Windows.Forms.Label labelBuySell;
        private System.Windows.Forms.Label labelTradeType;
        private System.Windows.Forms.Label labelFlag;
        private System.Windows.Forms.CheckBox checkBoxAsyncOrder;
        private System.Windows.Forms.Panel panelSendOrderForm;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Button buttonSendStockOddLotOrder;
    }
}