namespace SKOrderTester
{
    partial class WithDrawInOutControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.boxCurrency = new System.Windows.Forms.ComboBox();
            this.boxTypeIn = new System.Windows.Forms.ComboBox();
            this.boxTypeOut = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.txtDollars = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.boxAccountIn = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.boxAccountOut = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.boxBankBlock = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.GetBankBlock_btn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.boxMarket = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnIPAAmtSend = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAccountT3DueAmt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnT3DueAmtSend = new System.Windows.Forms.Button();
            this.txtLoginIDT3DueAmt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBalanceSend = new System.Windows.Forms.Button();
            this.txtLoginIDBalance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtSwiftCode_capitalpay = new System.Windows.Forms.TextBox();
            this.txtBankAcno_capitalpay = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBankCode_capitalpay = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCapitalPayWithDraw = new System.Windows.Forms.Button();
            this.txtDollars_capitalpay = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxCurrency
            // 
            this.boxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxCurrency.FormattingEnabled = true;
            this.boxCurrency.Items.AddRange(new object[] {
            "AUD",
            "EUR",
            "GBP",
            "HKD",
            "JPY",
            "NTD",
            "NZD",
            "RMB",
            "USD",
            "ZAR"});
            this.boxCurrency.Location = new System.Drawing.Point(398, 33);
            this.boxCurrency.Name = "boxCurrency";
            this.boxCurrency.Size = new System.Drawing.Size(62, 20);
            this.boxCurrency.TabIndex = 8;
            // 
            // boxTypeIn
            // 
            this.boxTypeIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxTypeIn.FormattingEnabled = true;
            this.boxTypeIn.Items.AddRange(new object[] {
            "0:國內",
            "1:國外"});
            this.boxTypeIn.Location = new System.Drawing.Point(206, 33);
            this.boxTypeIn.Name = "boxTypeIn";
            this.boxTypeIn.Size = new System.Drawing.Size(54, 20);
            this.boxTypeIn.TabIndex = 7;
            this.boxTypeIn.SelectedIndexChanged += new System.EventHandler(this.boxTypeIn_SelectedIndexChanged);
            // 
            // boxTypeOut
            // 
            this.boxTypeOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxTypeOut.FormattingEnabled = true;
            this.boxTypeOut.Items.AddRange(new object[] {
            "0:國內",
            "1:國外"});
            this.boxTypeOut.Location = new System.Drawing.Point(6, 33);
            this.boxTypeOut.Name = "boxTypeOut";
            this.boxTypeOut.Size = new System.Drawing.Size(54, 20);
            this.boxTypeOut.TabIndex = 6;
            this.boxTypeOut.SelectedIndexChanged += new System.EventHandler(this.boxTypeOut_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.txtPWD);
            this.groupBox1.Controls.Add(this.txtDollars);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.boxAccountIn);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.boxCurrency);
            this.groupBox1.Controls.Add(this.boxTypeIn);
            this.groupBox1.Controls.Add(this.boxTypeOut);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.boxAccountOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "國內外保證金互轉申請";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(734, 30);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(65, 23);
            this.btnSend.TabIndex = 20;
            this.btnSend.Text = "執行互轉";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(603, 31);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(107, 22);
            this.txtPWD.TabIndex = 18;
            // 
            // txtDollars
            // 
            this.txtDollars.Location = new System.Drawing.Point(499, 33);
            this.txtDollars.Name = "txtDollars";
            this.txtDollars.Size = new System.Drawing.Size(84, 22);
            this.txtDollars.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(607, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "出金密碼檢核：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(497, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "轉帳金額";
            // 
            // boxAccountIn
            // 
            this.boxAccountIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxAccountIn.FormattingEnabled = true;
            this.boxAccountIn.Location = new System.Drawing.Point(266, 33);
            this.boxAccountIn.Name = "boxAccountIn";
            this.boxAccountIn.Size = new System.Drawing.Size(115, 20);
            this.boxAccountIn.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "轉入帳號：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(405, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "幣別：";
            // 
            // boxAccountOut
            // 
            this.boxAccountOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxAccountOut.FormattingEnabled = true;
            this.boxAccountOut.Location = new System.Drawing.Point(66, 33);
            this.boxAccountOut.Name = "boxAccountOut";
            this.boxAccountOut.Size = new System.Drawing.Size(120, 20);
            this.boxAccountOut.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "轉出帳號：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.boxBankBlock);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.GetBankBlock_btn);
            this.groupBox5.Location = new System.Drawing.Point(14, 295);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(805, 53);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "複委託外幣圈存帳戶資料查詢";
            // 
            // boxBankBlock
            // 
            this.boxBankBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxBankBlock.FormattingEnabled = true;
            this.boxBankBlock.Items.AddRange(new object[] {
            "台幣",
            "外幣"});
            this.boxBankBlock.Location = new System.Drawing.Point(57, 21);
            this.boxBankBlock.Name = "boxBankBlock";
            this.boxBankBlock.Size = new System.Drawing.Size(62, 20);
            this.boxBankBlock.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "幣別";
            // 
            // GetBankBlock_btn
            // 
            this.GetBankBlock_btn.Location = new System.Drawing.Point(144, 19);
            this.GetBankBlock_btn.Name = "GetBankBlock_btn";
            this.GetBankBlock_btn.Size = new System.Drawing.Size(65, 23);
            this.GetBankBlock_btn.TabIndex = 20;
            this.GetBankBlock_btn.Text = "查詢";
            this.GetBankBlock_btn.UseVisualStyleBackColor = true;
            this.GetBankBlock_btn.Click += new System.EventHandler(this.GetBankBlock_btn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.boxMarket);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btnIPAAmtSend);
            this.groupBox4.Location = new System.Drawing.Point(14, 243);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(805, 46);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "已圈存金額查詢";
            // 
            // boxMarket
            // 
            this.boxMarket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxMarket.FormattingEnabled = true;
            this.boxMarket.Items.AddRange(new object[] {
            "0：上市櫃",
            "1：興櫃",
            "3"});
            this.boxMarket.Location = new System.Drawing.Point(57, 13);
            this.boxMarket.Name = "boxMarket";
            this.boxMarket.Size = new System.Drawing.Size(62, 20);
            this.boxMarket.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "市場別";
            // 
            // btnIPAAmtSend
            // 
            this.btnIPAAmtSend.Location = new System.Drawing.Point(144, 13);
            this.btnIPAAmtSend.Name = "btnIPAAmtSend";
            this.btnIPAAmtSend.Size = new System.Drawing.Size(65, 23);
            this.btnIPAAmtSend.TabIndex = 20;
            this.btnIPAAmtSend.Text = "查詢";
            this.btnIPAAmtSend.UseVisualStyleBackColor = true;
            this.btnIPAAmtSend.Click += new System.EventHandler(this.BtnIPAAmtSend_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAccountT3DueAmt);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnT3DueAmtSend);
            this.groupBox3.Controls.Add(this.txtLoginIDT3DueAmt);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(14, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(805, 46);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "近三日交割款查詢";
            // 
            // txtAccountT3DueAmt
            // 
            this.txtAccountT3DueAmt.Location = new System.Drawing.Point(221, 13);
            this.txtAccountT3DueAmt.Name = "txtAccountT3DueAmt";
            this.txtAccountT3DueAmt.Size = new System.Drawing.Size(84, 22);
            this.txtAccountT3DueAmt.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(162, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "證券帳號";
            // 
            // btnT3DueAmtSend
            // 
            this.btnT3DueAmtSend.Location = new System.Drawing.Point(316, 13);
            this.btnT3DueAmtSend.Name = "btnT3DueAmtSend";
            this.btnT3DueAmtSend.Size = new System.Drawing.Size(65, 23);
            this.btnT3DueAmtSend.TabIndex = 20;
            this.btnT3DueAmtSend.Text = "查詢";
            this.btnT3DueAmtSend.UseVisualStyleBackColor = true;
            this.btnT3DueAmtSend.Click += new System.EventHandler(this.BtnT3DueAmtSend_Click);
            // 
            // txtLoginIDT3DueAmt
            // 
            this.txtLoginIDT3DueAmt.Location = new System.Drawing.Point(66, 13);
            this.txtLoginIDT3DueAmt.Name = "txtLoginIDT3DueAmt";
            this.txtLoginIDT3DueAmt.Size = new System.Drawing.Size(84, 22);
            this.txtLoginIDT3DueAmt.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "登入帳號";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBalanceSend);
            this.groupBox2.Controls.Add(this.txtLoginIDBalance);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(14, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(805, 46);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "一戶通查詢";
            // 
            // btnBalanceSend
            // 
            this.btnBalanceSend.Location = new System.Drawing.Point(156, 13);
            this.btnBalanceSend.Name = "btnBalanceSend";
            this.btnBalanceSend.Size = new System.Drawing.Size(65, 23);
            this.btnBalanceSend.TabIndex = 20;
            this.btnBalanceSend.Text = "查詢";
            this.btnBalanceSend.UseVisualStyleBackColor = true;
            this.btnBalanceSend.Click += new System.EventHandler(this.BtnBalanceSend_Click);
            // 
            // txtLoginIDBalance
            // 
            this.txtLoginIDBalance.Location = new System.Drawing.Point(66, 13);
            this.txtLoginIDBalance.Name = "txtLoginIDBalance";
            this.txtLoginIDBalance.Size = new System.Drawing.Size(84, 22);
            this.txtLoginIDBalance.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "登入帳號";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtSwiftCode_capitalpay);
            this.groupBox6.Controls.Add(this.txtBankAcno_capitalpay);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.txtBankCode_capitalpay);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.btnCapitalPayWithDraw);
            this.groupBox6.Controls.Add(this.txtDollars_capitalpay);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Location = new System.Drawing.Point(14, 354);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(805, 80);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "一戶通出金";
            // 
            // txtSwiftCode_capitalpay
            // 
            this.txtSwiftCode_capitalpay.Location = new System.Drawing.Point(111, 45);
            this.txtSwiftCode_capitalpay.Name = "txtSwiftCode_capitalpay";
            this.txtSwiftCode_capitalpay.Size = new System.Drawing.Size(66, 22);
            this.txtSwiftCode_capitalpay.TabIndex = 27;
            // 
            // txtBankAcno_capitalpay
            // 
            this.txtBankAcno_capitalpay.Location = new System.Drawing.Point(201, 45);
            this.txtBankAcno_capitalpay.Name = "txtBankAcno_capitalpay";
            this.txtBankAcno_capitalpay.Size = new System.Drawing.Size(159, 22);
            this.txtBankAcno_capitalpay.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(199, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "銀行帳號";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(109, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "分行代碼";
            // 
            // txtBankCode_capitalpay
            // 
            this.txtBankCode_capitalpay.Location = new System.Drawing.Point(21, 45);
            this.txtBankCode_capitalpay.Name = "txtBankCode_capitalpay";
            this.txtBankCode_capitalpay.Size = new System.Drawing.Size(66, 22);
            this.txtBankCode_capitalpay.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 21;
            this.label13.Text = "銀行代碼";
            // 
            // btnCapitalPayWithDraw
            // 
            this.btnCapitalPayWithDraw.Location = new System.Drawing.Point(486, 43);
            this.btnCapitalPayWithDraw.Name = "btnCapitalPayWithDraw";
            this.btnCapitalPayWithDraw.Size = new System.Drawing.Size(73, 23);
            this.btnCapitalPayWithDraw.TabIndex = 20;
            this.btnCapitalPayWithDraw.Text = "一戶通出金";
            this.btnCapitalPayWithDraw.UseVisualStyleBackColor = true;
            this.btnCapitalPayWithDraw.Click += new System.EventHandler(this.BtnCapitalPayWithDraw_Click);
            // 
            // txtDollars_capitalpay
            // 
            this.txtDollars_capitalpay.Location = new System.Drawing.Point(387, 45);
            this.txtDollars_capitalpay.Name = "txtDollars_capitalpay";
            this.txtDollars_capitalpay.Size = new System.Drawing.Size(84, 22);
            this.txtDollars_capitalpay.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(385, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 14;
            this.label14.Text = "出金金額";
            // 
            // WithDrawInOutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WithDrawInOutControl";
            this.Size = new System.Drawing.Size(877, 547);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox boxCurrency;
        private System.Windows.Forms.ComboBox boxTypeIn;
        private System.Windows.Forms.ComboBox boxTypeOut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox boxAccountIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox boxAccountOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.TextBox txtDollars;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox boxBankBlock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button GetBankBlock_btn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox boxMarket;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnIPAAmtSend;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAccountT3DueAmt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnT3DueAmtSend;
        private System.Windows.Forms.TextBox txtLoginIDT3DueAmt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBalanceSend;
        private System.Windows.Forms.TextBox txtLoginIDBalance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtSwiftCode_capitalpay;
        private System.Windows.Forms.TextBox txtBankAcno_capitalpay;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBankCode_capitalpay;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCapitalPayWithDraw;
        private System.Windows.Forms.TextBox txtDollars_capitalpay;
        private System.Windows.Forms.Label label14;
    }
}
