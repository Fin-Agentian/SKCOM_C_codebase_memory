
namespace WindowsFormsApp1
{
    partial class TFReadOrderForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.richTextBoxMethodMessage = new System.Windows.Forms.RichTextBox();
            this.comboBoxCoinType = new System.Windows.Forms.ComboBox();
            this.dataGridViewOnFutureRights = new System.Windows.Forms.DataGridView();
            this.panelReadOrder = new System.Windows.Forms.Panel();
            this.tabControlReadOrder = new System.Windows.Forms.TabControl();
            this.tabPageTF = new System.Windows.Forms.TabPage();
            this.tabControlFuture = new System.Windows.Forms.TabControl();
            this.tabPageGetFutureRights = new System.Windows.Forms.TabPage();
            this.tabPageGetOpenInterestGW = new System.Windows.Forms.TabPage();
            this.dataGridViewOnOpenInterest = new System.Windows.Forms.DataGridView();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxUserID = new System.Windows.Forms.ComboBox();
            this.buttonGetOpenInterestGW = new System.Windows.Forms.Button();
            this.buttonGetFutureRights = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnFutureRights)).BeginInit();
            this.panelReadOrder.SuspendLayout();
            this.tabControlReadOrder.SuspendLayout();
            this.tabPageTF.SuspendLayout();
            this.tabControlFuture.SuspendLayout();
            this.tabPageGetFutureRights.SuspendLayout();
            this.tabPageGetOpenInterestGW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnOpenInterest)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(3, 76);
            this.richTextBoxMethodMessage.Name = "richTextBoxMethodMessage";
            this.richTextBoxMethodMessage.ReadOnly = true;
            this.richTextBoxMethodMessage.Size = new System.Drawing.Size(165, 309);
            this.richTextBoxMethodMessage.TabIndex = 53;
            this.richTextBoxMethodMessage.Text = "";
            // 
            // comboBoxCoinType
            // 
            this.comboBoxCoinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCoinType.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxCoinType.FormattingEnabled = true;
            this.comboBoxCoinType.Location = new System.Drawing.Point(163, 8);
            this.comboBoxCoinType.Name = "comboBoxCoinType";
            this.comboBoxCoinType.Size = new System.Drawing.Size(151, 32);
            this.comboBoxCoinType.TabIndex = 68;
            // 
            // dataGridViewOnFutureRights
            // 
            this.dataGridViewOnFutureRights.AllowUserToAddRows = false;
            this.dataGridViewOnFutureRights.AllowUserToDeleteRows = false;
            this.dataGridViewOnFutureRights.AllowUserToOrderColumns = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOnFutureRights.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewOnFutureRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOnFutureRights.Location = new System.Drawing.Point(6, 44);
            this.dataGridViewOnFutureRights.Name = "dataGridViewOnFutureRights";
            this.dataGridViewOnFutureRights.ReadOnly = true;
            this.dataGridViewOnFutureRights.RowTemplate.Height = 24;
            this.dataGridViewOnFutureRights.Size = new System.Drawing.Size(868, 249);
            this.dataGridViewOnFutureRights.TabIndex = 69;
            // 
            // panelReadOrder
            // 
            this.panelReadOrder.AutoScroll = true;
            this.panelReadOrder.Controls.Add(this.tabControlReadOrder);
            this.panelReadOrder.Controls.Add(this.comboBoxAccount);
            this.panelReadOrder.Controls.Add(this.comboBoxUserID);
            this.panelReadOrder.Controls.Add(this.richTextBoxMethodMessage);
            this.panelReadOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReadOrder.Location = new System.Drawing.Point(0, 0);
            this.panelReadOrder.Name = "panelReadOrder";
            this.panelReadOrder.Size = new System.Drawing.Size(1080, 389);
            this.panelReadOrder.TabIndex = 70;
            // 
            // tabControlReadOrder
            // 
            this.tabControlReadOrder.Controls.Add(this.tabPageTF);
            this.tabControlReadOrder.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlReadOrder.Location = new System.Drawing.Point(174, 4);
            this.tabControlReadOrder.Name = "tabControlReadOrder";
            this.tabControlReadOrder.SelectedIndex = 0;
            this.tabControlReadOrder.Size = new System.Drawing.Size(903, 385);
            this.tabControlReadOrder.TabIndex = 120;
            // 
            // tabPageTF
            // 
            this.tabPageTF.AutoScroll = true;
            this.tabPageTF.Controls.Add(this.tabControlFuture);
            this.tabPageTF.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageTF.Location = new System.Drawing.Point(4, 33);
            this.tabPageTF.Name = "tabPageTF";
            this.tabPageTF.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTF.Size = new System.Drawing.Size(895, 348);
            this.tabPageTF.TabIndex = 5;
            this.tabPageTF.Text = "期貨";
            this.tabPageTF.UseVisualStyleBackColor = true;
            // 
            // tabControlFuture
            // 
            this.tabControlFuture.Controls.Add(this.tabPageGetOpenInterestGW);
            this.tabControlFuture.Controls.Add(this.tabPageGetFutureRights);
            this.tabControlFuture.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlFuture.Location = new System.Drawing.Point(5, 6);
            this.tabControlFuture.Name = "tabControlFuture";
            this.tabControlFuture.SelectedIndex = 0;
            this.tabControlFuture.Size = new System.Drawing.Size(888, 336);
            this.tabControlFuture.TabIndex = 122;
            // 
            // tabPageGetFutureRights
            // 
            this.tabPageGetFutureRights.AutoScroll = true;
            this.tabPageGetFutureRights.Controls.Add(this.buttonGetFutureRights);
            this.tabPageGetFutureRights.Controls.Add(this.comboBoxCoinType);
            this.tabPageGetFutureRights.Controls.Add(this.dataGridViewOnFutureRights);
            this.tabPageGetFutureRights.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageGetFutureRights.Location = new System.Drawing.Point(4, 33);
            this.tabPageGetFutureRights.Name = "tabPageGetFutureRights";
            this.tabPageGetFutureRights.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGetFutureRights.Size = new System.Drawing.Size(880, 299);
            this.tabPageGetFutureRights.TabIndex = 0;
            this.tabPageGetFutureRights.Text = "國內權益數";
            this.tabPageGetFutureRights.UseVisualStyleBackColor = true;
            // 
            // tabPageGetOpenInterestGW
            // 
            this.tabPageGetOpenInterestGW.AutoScroll = true;
            this.tabPageGetOpenInterestGW.Controls.Add(this.buttonGetOpenInterestGW);
            this.tabPageGetOpenInterestGW.Controls.Add(this.dataGridViewOnOpenInterest);
            this.tabPageGetOpenInterestGW.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageGetOpenInterestGW.Location = new System.Drawing.Point(4, 33);
            this.tabPageGetOpenInterestGW.Name = "tabPageGetOpenInterestGW";
            this.tabPageGetOpenInterestGW.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGetOpenInterestGW.Size = new System.Drawing.Size(880, 299);
            this.tabPageGetOpenInterestGW.TabIndex = 5;
            this.tabPageGetOpenInterestGW.Text = "期貨未平倉";
            this.tabPageGetOpenInterestGW.UseVisualStyleBackColor = true;
            // 
            // dataGridViewOnOpenInterest
            // 
            this.dataGridViewOnOpenInterest.AllowUserToAddRows = false;
            this.dataGridViewOnOpenInterest.AllowUserToDeleteRows = false;
            this.dataGridViewOnOpenInterest.AllowUserToOrderColumns = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOnOpenInterest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewOnOpenInterest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOnOpenInterest.Location = new System.Drawing.Point(6, 44);
            this.dataGridViewOnOpenInterest.Name = "dataGridViewOnOpenInterest";
            this.dataGridViewOnOpenInterest.ReadOnly = true;
            this.dataGridViewOnOpenInterest.RowTemplate.Height = 24;
            this.dataGridViewOnOpenInterest.Size = new System.Drawing.Size(868, 249);
            this.dataGridViewOnOpenInterest.TabIndex = 124;
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccount.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAccount.FormattingEnabled = true;
            this.comboBoxAccount.Location = new System.Drawing.Point(3, 40);
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.Size = new System.Drawing.Size(165, 30);
            this.comboBoxAccount.TabIndex = 117;
            // 
            // comboBoxUserID
            // 
            this.comboBoxUserID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUserID.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUserID.FormattingEnabled = true;
            this.comboBoxUserID.Location = new System.Drawing.Point(3, 4);
            this.comboBoxUserID.Name = "comboBoxUserID";
            this.comboBoxUserID.Size = new System.Drawing.Size(165, 30);
            this.comboBoxUserID.TabIndex = 116;
            this.comboBoxUserID.DropDown += new System.EventHandler(this.comboBoxUserID_DropDown);
            // 
            // buttonGetOpenInterestGW
            // 
            this.buttonGetOpenInterestGW.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonGetOpenInterestGW.Location = new System.Drawing.Point(6, 6);
            this.buttonGetOpenInterestGW.Name = "buttonGetOpenInterestGW";
            this.buttonGetOpenInterestGW.Size = new System.Drawing.Size(151, 34);
            this.buttonGetOpenInterestGW.TabIndex = 125;
            this.buttonGetOpenInterestGW.Text = "送出";
            this.buttonGetOpenInterestGW.UseVisualStyleBackColor = true;
            this.buttonGetOpenInterestGW.Click += new System.EventHandler(this.buttonGetOpenInterestGW_Click);
            // 
            // buttonGetFutureRights
            // 
            this.buttonGetFutureRights.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonGetFutureRights.Location = new System.Drawing.Point(6, 6);
            this.buttonGetFutureRights.Name = "buttonGetFutureRights";
            this.buttonGetFutureRights.Size = new System.Drawing.Size(151, 34);
            this.buttonGetFutureRights.TabIndex = 126;
            this.buttonGetFutureRights.Text = "送出";
            this.buttonGetFutureRights.UseVisualStyleBackColor = true;
            this.buttonGetFutureRights.Click += new System.EventHandler(this.buttonGetFutureRights_Click);
            // 
            // TFReadOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 389);
            this.Controls.Add(this.panelReadOrder);
            this.Name = "TFReadOrderForm";
            this.Text = "TFReadOrderForm";
            this.Load += new System.EventHandler(this.ReadOrderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnFutureRights)).EndInit();
            this.panelReadOrder.ResumeLayout(false);
            this.tabControlReadOrder.ResumeLayout(false);
            this.tabPageTF.ResumeLayout(false);
            this.tabControlFuture.ResumeLayout(false);
            this.tabPageGetFutureRights.ResumeLayout(false);
            this.tabPageGetOpenInterestGW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnOpenInterest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.ComboBox comboBoxCoinType;
        private System.Windows.Forms.DataGridView dataGridViewOnFutureRights;
        private System.Windows.Forms.Panel panelReadOrder;
        private System.Windows.Forms.ComboBox comboBoxUserID;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.DataGridView dataGridViewOnOpenInterest;
        private System.Windows.Forms.TabControl tabControlReadOrder;
        private System.Windows.Forms.TabPage tabPageTF;
        private System.Windows.Forms.TabControl tabControlFuture;
        private System.Windows.Forms.TabPage tabPageGetFutureRights;
        private System.Windows.Forms.TabPage tabPageGetOpenInterestGW;
        private System.Windows.Forms.Button buttonGetOpenInterestGW;
        private System.Windows.Forms.Button buttonGetFutureRights;
    }
}