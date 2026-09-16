
namespace WindowsFormsApp1
{
    partial class OSUpdateOrderForm
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
            this.tabPageCancelForeignStockOrder = new System.Windows.Forms.TabPage();
            this.textBoxbstrExchangeNo = new System.Windows.Forms.TextBox();
            this.labelForeignExchangeNo = new System.Windows.Forms.Label();
            this.textBoxForeignStockID = new System.Windows.Forms.TextBox();
            this.labelForeignStock = new System.Windows.Forms.Label();
            this.buttonCancelForeignStockOrder = new System.Windows.Forms.Button();
            this.textBoxCancelForeignStockOrderbstrBookNo = new System.Windows.Forms.TextBox();
            this.labelCancelForeignStockOrderbstrBookNo = new System.Windows.Forms.Label();
            this.textBoxCancelForeignStockOrderbstrSeqNo = new System.Windows.Forms.TextBox();
            this.labelCancelForeignStockOrderbstrSeqNo = new System.Windows.Forms.Label();
            this.comboBoxUpdateTFOrder = new System.Windows.Forms.ComboBox();
            this.checkBoxAsyncOrder = new System.Windows.Forms.CheckBox();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.panelOrderControlForm.SuspendLayout();
            this.tabControlUpdateOrder.SuspendLayout();
            this.tabPageCancelForeignStockOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(0, 76);
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
            this.panelOrderControlForm.Size = new System.Drawing.Size(374, 400);
            this.panelOrderControlForm.TabIndex = 103;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(1, 354);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(370, 38);
            this.richTextBoxMessage.TabIndex = 126;
            this.richTextBoxMessage.Text = "";
            // 
            // tabControlUpdateOrder
            // 
            this.tabControlUpdateOrder.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlUpdateOrder.Controls.Add(this.tabPageCancelForeignStockOrder);
            this.tabControlUpdateOrder.Font = new System.Drawing.Font("DFKai-SB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlUpdateOrder.Location = new System.Drawing.Point(3, 120);
            this.tabControlUpdateOrder.Name = "tabControlUpdateOrder";
            this.tabControlUpdateOrder.SelectedIndex = 0;
            this.tabControlUpdateOrder.Size = new System.Drawing.Size(367, 232);
            this.tabControlUpdateOrder.TabIndex = 107;
            // 
            // tabPageCancelForeignStockOrder
            // 
            this.tabPageCancelForeignStockOrder.AutoScroll = true;
            this.tabPageCancelForeignStockOrder.Controls.Add(this.textBoxbstrExchangeNo);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.labelForeignExchangeNo);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.textBoxForeignStockID);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.labelForeignStock);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.buttonCancelForeignStockOrder);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.textBoxCancelForeignStockOrderbstrBookNo);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.labelCancelForeignStockOrderbstrBookNo);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.textBoxCancelForeignStockOrderbstrSeqNo);
            this.tabPageCancelForeignStockOrder.Controls.Add(this.labelCancelForeignStockOrderbstrSeqNo);
            this.tabPageCancelForeignStockOrder.Location = new System.Drawing.Point(4, 32);
            this.tabPageCancelForeignStockOrder.Name = "tabPageCancelForeignStockOrder";
            this.tabPageCancelForeignStockOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCancelForeignStockOrder.Size = new System.Drawing.Size(359, 196);
            this.tabPageCancelForeignStockOrder.TabIndex = 5;
            this.tabPageCancelForeignStockOrder.Text = "複委託";
            this.tabPageCancelForeignStockOrder.UseVisualStyleBackColor = true;
            // 
            // textBoxbstrExchangeNo
            // 
            this.textBoxbstrExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxbstrExchangeNo.Location = new System.Drawing.Point(232, 120);
            this.textBoxbstrExchangeNo.Name = "textBoxbstrExchangeNo";
            this.textBoxbstrExchangeNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxbstrExchangeNo.TabIndex = 114;
            // 
            // labelForeignExchangeNo
            // 
            this.labelForeignExchangeNo.AutoSize = true;
            this.labelForeignExchangeNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignExchangeNo.Location = new System.Drawing.Point(4, 129);
            this.labelForeignExchangeNo.Name = "labelForeignExchangeNo";
            this.labelForeignExchangeNo.Size = new System.Drawing.Size(206, 24);
            this.labelForeignExchangeNo.TabIndex = 113;
            this.labelForeignExchangeNo.Text = "交易所代碼，美股：US";
            // 
            // textBoxForeignStockID
            // 
            this.textBoxForeignStockID.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxForeignStockID.Location = new System.Drawing.Point(232, 81);
            this.textBoxForeignStockID.Name = "textBoxForeignStockID";
            this.textBoxForeignStockID.Size = new System.Drawing.Size(121, 33);
            this.textBoxForeignStockID.TabIndex = 112;
            // 
            // labelForeignStock
            // 
            this.labelForeignStock.AutoSize = true;
            this.labelForeignStock.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelForeignStock.Location = new System.Drawing.Point(4, 90);
            this.labelForeignStock.Name = "labelForeignStock";
            this.labelForeignStock.Size = new System.Drawing.Size(86, 24);
            this.labelForeignStock.TabIndex = 111;
            this.labelForeignStock.Text = "股票代號";
            // 
            // buttonCancelForeignStockOrder
            // 
            this.buttonCancelForeignStockOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancelForeignStockOrder.Location = new System.Drawing.Point(90, 159);
            this.buttonCancelForeignStockOrder.Name = "buttonCancelForeignStockOrder";
            this.buttonCancelForeignStockOrder.Size = new System.Drawing.Size(163, 32);
            this.buttonCancelForeignStockOrder.TabIndex = 110;
            this.buttonCancelForeignStockOrder.Text = "刪單(序號+書號)";
            this.buttonCancelForeignStockOrder.UseVisualStyleBackColor = true;
            this.buttonCancelForeignStockOrder.Visible = false;
            this.buttonCancelForeignStockOrder.Click += new System.EventHandler(this.buttonCancelForeignStockOrder_Click);
            // 
            // textBoxCancelForeignStockOrderbstrBookNo
            // 
            this.textBoxCancelForeignStockOrderbstrBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxCancelForeignStockOrderbstrBookNo.Location = new System.Drawing.Point(232, 42);
            this.textBoxCancelForeignStockOrderbstrBookNo.Name = "textBoxCancelForeignStockOrderbstrBookNo";
            this.textBoxCancelForeignStockOrderbstrBookNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxCancelForeignStockOrderbstrBookNo.TabIndex = 109;
            // 
            // labelCancelForeignStockOrderbstrBookNo
            // 
            this.labelCancelForeignStockOrderbstrBookNo.AutoSize = true;
            this.labelCancelForeignStockOrderbstrBookNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelForeignStockOrderbstrBookNo.Location = new System.Drawing.Point(3, 45);
            this.labelCancelForeignStockOrderbstrBookNo.Name = "labelCancelForeignStockOrderbstrBookNo";
            this.labelCancelForeignStockOrderbstrBookNo.Size = new System.Drawing.Size(143, 24);
            this.labelCancelForeignStockOrderbstrBookNo.TabIndex = 108;
            this.labelCancelForeignStockOrderbstrBookNo.Text = "請輸入委託書號";
            // 
            // textBoxCancelForeignStockOrderbstrSeqNo
            // 
            this.textBoxCancelForeignStockOrderbstrSeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.textBoxCancelForeignStockOrderbstrSeqNo.Location = new System.Drawing.Point(232, 3);
            this.textBoxCancelForeignStockOrderbstrSeqNo.Name = "textBoxCancelForeignStockOrderbstrSeqNo";
            this.textBoxCancelForeignStockOrderbstrSeqNo.Size = new System.Drawing.Size(121, 33);
            this.textBoxCancelForeignStockOrderbstrSeqNo.TabIndex = 107;
            // 
            // labelCancelForeignStockOrderbstrSeqNo
            // 
            this.labelCancelForeignStockOrderbstrSeqNo.AutoSize = true;
            this.labelCancelForeignStockOrderbstrSeqNo.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelCancelForeignStockOrderbstrSeqNo.Location = new System.Drawing.Point(3, 6);
            this.labelCancelForeignStockOrderbstrSeqNo.Name = "labelCancelForeignStockOrderbstrSeqNo";
            this.labelCancelForeignStockOrderbstrSeqNo.Size = new System.Drawing.Size(143, 24);
            this.labelCancelForeignStockOrderbstrSeqNo.TabIndex = 105;
            this.labelCancelForeignStockOrderbstrSeqNo.Text = "請輸入委託序號";
            // 
            // comboBoxUpdateTFOrder
            // 
            this.comboBoxUpdateTFOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpdateTFOrder.Enabled = false;
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
            // OSUpdateOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(374, 400);
            this.Controls.Add(this.panelOrderControlForm);
            this.Name = "OSUpdateOrderForm";
            this.Text = "OSUpdateOrderForm";
            this.Load += new System.EventHandler(this.OrderControlForm_Load);
            this.panelOrderControlForm.ResumeLayout(false);
            this.panelOrderControlForm.PerformLayout();
            this.tabControlUpdateOrder.ResumeLayout(false);
            this.tabPageCancelForeignStockOrder.ResumeLayout(false);
            this.tabPageCancelForeignStockOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.Panel panelOrderControlForm;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.TabControl tabControlUpdateOrder;
        private System.Windows.Forms.TabPage tabPageCancelForeignStockOrder;
        private System.Windows.Forms.Button buttonCancelForeignStockOrder;
        private System.Windows.Forms.TextBox textBoxCancelForeignStockOrderbstrBookNo;
        private System.Windows.Forms.Label labelCancelForeignStockOrderbstrBookNo;
        private System.Windows.Forms.TextBox textBoxCancelForeignStockOrderbstrSeqNo;
        private System.Windows.Forms.Label labelCancelForeignStockOrderbstrSeqNo;
        private System.Windows.Forms.CheckBox checkBoxAsyncOrder;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.ComboBox comboBoxUpdateTFOrder;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.TextBox textBoxbstrExchangeNo;
        private System.Windows.Forms.Label labelForeignExchangeNo;
        private System.Windows.Forms.TextBox textBoxForeignStockID;
        private System.Windows.Forms.Label labelForeignStock;
    }
}