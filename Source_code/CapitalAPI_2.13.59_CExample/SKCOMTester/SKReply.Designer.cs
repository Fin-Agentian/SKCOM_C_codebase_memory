namespace SKCOMTester
{
    partial class SKReply
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSignal = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listMessage = new System.Windows.Forms.ListBox();
            this.lblConnectID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listNewMessage = new System.Windows.Forms.ListBox();
            this.lblSignalReplySolace = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.listNewMessage2 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listMessage2 = new System.Windows.Forms.ListBox();
            this.btnSolaceDisconnect = new System.Windows.Forms.Button();
            this.lblSignal2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSignalReplySolace2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConnect2 = new System.Windows.Forms.Button();
            this.btnDisconnect2 = new System.Windows.Forms.Button();
            this.lblConnectID2 = new System.Windows.Forms.Label();
            this.btnSolaceDisconnect2 = new System.Windows.Forms.Button();
            this.ConnectedLabel = new System.Windows.Forms.Label();
            this.btnIsConnected = new System.Windows.Forms.Button();
            this.ConnectedLabel2 = new System.Windows.Forms.Label();
            this.btnIsConnected2 = new System.Windows.Forms.Button();
            this.IDs_Box = new System.Windows.Forms.ComboBox();
            this.IDs_Box2 = new System.Windows.Forms.ComboBox();
            this.buttonRegisterOnNewDataCallback = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listNewMessageOnNewDataCallback = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSignal);
            this.groupBox1.Location = new System.Drawing.Point(297, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 46);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Old_ReplySignal";
            this.groupBox1.Visible = false;
            // 
            // lblSignal
            // 
            this.lblSignal.AutoSize = true;
            this.lblSignal.Font = new System.Drawing.Font("PMingLiU", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignal.ForeColor = System.Drawing.Color.Red;
            this.lblSignal.Location = new System.Drawing.Point(16, 18);
            this.lblSignal.Name = "lblSignal";
            this.lblSignal.Size = new System.Drawing.Size(32, 22);
            this.lblSignal.TabIndex = 0;
            this.lblSignal.Text = "●";
            this.lblSignal.Visible = false;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(108, 29);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(81, 27);
            this.btnDisconnect.TabIndex = 10;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Visible = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(21, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 26);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // listMessage
            // 
            this.listMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMessage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMessage.FormattingEnabled = true;
            this.listMessage.HorizontalExtent = 20000;
            this.listMessage.HorizontalScrollbar = true;
            this.listMessage.ItemHeight = 16;
            this.listMessage.Location = new System.Drawing.Point(3, 74);
            this.listMessage.Name = "listMessage";
            this.listMessage.ScrollAlwaysVisible = true;
            this.listMessage.Size = new System.Drawing.Size(851, 100);
            this.listMessage.TabIndex = 12;
            // 
            // lblConnectID
            // 
            this.lblConnectID.AutoSize = true;
            this.lblConnectID.Location = new System.Drawing.Point(-2, 3);
            this.lblConnectID.Name = "lblConnectID";
            this.lblConnectID.Size = new System.Drawing.Size(17, 12);
            this.lblConnectID.TabIndex = 13;
            this.lblConnectID.Text = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "OnData格式 / OnMessage格式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "OnNewData格式";
            // 
            // listNewMessage
            // 
            this.listNewMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listNewMessage.CausesValidation = false;
            this.listNewMessage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listNewMessage.FormattingEnabled = true;
            this.listNewMessage.HorizontalExtent = 20000;
            this.listNewMessage.HorizontalScrollbar = true;
            this.listNewMessage.ItemHeight = 16;
            this.listNewMessage.Location = new System.Drawing.Point(3, 192);
            this.listNewMessage.Name = "listNewMessage";
            this.listNewMessage.ScrollAlwaysVisible = true;
            this.listNewMessage.Size = new System.Drawing.Size(849, 84);
            this.listNewMessage.TabIndex = 16;
            // 
            // lblSignalReplySolace
            // 
            this.lblSignalReplySolace.AutoSize = true;
            this.lblSignalReplySolace.Font = new System.Drawing.Font("PMingLiU", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignalReplySolace.ForeColor = System.Drawing.Color.Red;
            this.lblSignalReplySolace.Location = new System.Drawing.Point(15, 18);
            this.lblSignalReplySolace.Name = "lblSignalReplySolace";
            this.lblSignalReplySolace.Size = new System.Drawing.Size(32, 22);
            this.lblSignalReplySolace.TabIndex = 0;
            this.lblSignalReplySolace.Text = "●";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblSignalReplySolace);
            this.groupBox8.Location = new System.Drawing.Point(419, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(126, 46);
            this.groupBox8.TabIndex = 49;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Solace_ReplySignal";
            // 
            // listNewMessage2
            // 
            this.listNewMessage2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listNewMessage2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listNewMessage2.FormattingEnabled = true;
            this.listNewMessage2.HorizontalExtent = 200000;
            this.listNewMessage2.HorizontalScrollbar = true;
            this.listNewMessage2.ItemHeight = 16;
            this.listNewMessage2.Location = new System.Drawing.Point(3, 622);
            this.listNewMessage2.Name = "listNewMessage2";
            this.listNewMessage2.Size = new System.Drawing.Size(849, 164);
            this.listNewMessage2.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 607);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "OnNewData格式_2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 489);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 12);
            this.label8.TabIndex = 59;
            this.label8.Text = "OnData格式_2 / OnMessage格式_2";
            // 
            // listMessage2
            // 
            this.listMessage2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMessage2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMessage2.FormattingEnabled = true;
            this.listMessage2.HorizontalExtent = 20000;
            this.listMessage2.HorizontalScrollbar = true;
            this.listMessage2.ItemHeight = 16;
            this.listMessage2.Location = new System.Drawing.Point(3, 504);
            this.listMessage2.Name = "listMessage2";
            this.listMessage2.ScrollAlwaysVisible = true;
            this.listMessage2.Size = new System.Drawing.Size(851, 100);
            this.listMessage2.TabIndex = 58;
            // 
            // btnSolaceDisconnect
            // 
            this.btnSolaceDisconnect.Location = new System.Drawing.Point(195, 29);
            this.btnSolaceDisconnect.Name = "btnSolaceDisconnect";
            this.btnSolaceDisconnect.Size = new System.Drawing.Size(96, 27);
            this.btnSolaceDisconnect.TabIndex = 60;
            this.btnSolaceDisconnect.Text = "SolaceDisconnect";
            this.btnSolaceDisconnect.UseVisualStyleBackColor = true;
            this.btnSolaceDisconnect.Click += new System.EventHandler(this.btnSolaceDisconnect_Click);
            // 
            // lblSignal2
            // 
            this.lblSignal2.AutoSize = true;
            this.lblSignal2.Font = new System.Drawing.Font("PMingLiU", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignal2.ForeColor = System.Drawing.Color.Red;
            this.lblSignal2.Location = new System.Drawing.Point(16, 17);
            this.lblSignal2.Name = "lblSignal2";
            this.lblSignal2.Size = new System.Drawing.Size(32, 22);
            this.lblSignal2.TabIndex = 0;
            this.lblSignal2.Text = "●";
            this.lblSignal2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSignal2);
            this.groupBox3.Location = new System.Drawing.Point(283, 416);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 46);
            this.groupBox3.TabIndex = 55;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Old_ReplySignal";
            this.groupBox3.Visible = false;
            // 
            // lblSignalReplySolace2
            // 
            this.lblSignalReplySolace2.AutoSize = true;
            this.lblSignalReplySolace2.Font = new System.Drawing.Font("PMingLiU", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignalReplySolace2.ForeColor = System.Drawing.Color.Red;
            this.lblSignalReplySolace2.Location = new System.Drawing.Point(15, 17);
            this.lblSignalReplySolace2.Name = "lblSignalReplySolace2";
            this.lblSignalReplySolace2.Size = new System.Drawing.Size(32, 22);
            this.lblSignalReplySolace2.TabIndex = 0;
            this.lblSignalReplySolace2.Text = "●";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSignalReplySolace2);
            this.groupBox2.Location = new System.Drawing.Point(405, 416);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 46);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solace_ReplySignal";
            // 
            // btnConnect2
            // 
            this.btnConnect2.Location = new System.Drawing.Point(7, 425);
            this.btnConnect2.Name = "btnConnect2";
            this.btnConnect2.Size = new System.Drawing.Size(81, 37);
            this.btnConnect2.TabIndex = 53;
            this.btnConnect2.Text = "Connect";
            this.btnConnect2.UseVisualStyleBackColor = true;
            this.btnConnect2.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDisconnect2
            // 
            this.btnDisconnect2.Location = new System.Drawing.Point(94, 425);
            this.btnDisconnect2.Name = "btnDisconnect2";
            this.btnDisconnect2.Size = new System.Drawing.Size(81, 37);
            this.btnDisconnect2.TabIndex = 54;
            this.btnDisconnect2.Text = "Disconnect";
            this.btnDisconnect2.UseVisualStyleBackColor = true;
            this.btnDisconnect2.Visible = false;
            this.btnDisconnect2.Click += new System.EventHandler(this.btnDisconnect2_Click);
            // 
            // lblConnectID2
            // 
            this.lblConnectID2.AutoSize = true;
            this.lblConnectID2.Location = new System.Drawing.Point(3, 399);
            this.lblConnectID2.Name = "lblConnectID2";
            this.lblConnectID2.Size = new System.Drawing.Size(17, 12);
            this.lblConnectID2.TabIndex = 56;
            this.lblConnectID2.Text = "ID";
            // 
            // btnSolaceDisconnect2
            // 
            this.btnSolaceDisconnect2.Location = new System.Drawing.Point(181, 425);
            this.btnSolaceDisconnect2.Name = "btnSolaceDisconnect2";
            this.btnSolaceDisconnect2.Size = new System.Drawing.Size(96, 37);
            this.btnSolaceDisconnect2.TabIndex = 61;
            this.btnSolaceDisconnect2.Text = "SolaceDisconnect";
            this.btnSolaceDisconnect2.UseVisualStyleBackColor = true;
            this.btnSolaceDisconnect2.Click += new System.EventHandler(this.btnSolaceDisconnect2_Click);
            // 
            // ConnectedLabel
            // 
            this.ConnectedLabel.AutoSize = true;
            this.ConnectedLabel.Location = new System.Drawing.Point(646, 29);
            this.ConnectedLabel.Name = "ConnectedLabel";
            this.ConnectedLabel.Size = new System.Drawing.Size(11, 12);
            this.ConnectedLabel.TabIndex = 63;
            this.ConnectedLabel.Text = "0";
            // 
            // btnIsConnected
            // 
            this.btnIsConnected.Location = new System.Drawing.Point(551, 17);
            this.btnIsConnected.Name = "btnIsConnected";
            this.btnIsConnected.Size = new System.Drawing.Size(75, 29);
            this.btnIsConnected.TabIndex = 62;
            this.btnIsConnected.Text = "IsConnected";
            this.btnIsConnected.UseVisualStyleBackColor = true;
            this.btnIsConnected.Click += new System.EventHandler(this.btnIsConnected_Click);
            // 
            // ConnectedLabel2
            // 
            this.ConnectedLabel2.AutoSize = true;
            this.ConnectedLabel2.Location = new System.Drawing.Point(632, 437);
            this.ConnectedLabel2.Name = "ConnectedLabel2";
            this.ConnectedLabel2.Size = new System.Drawing.Size(11, 12);
            this.ConnectedLabel2.TabIndex = 65;
            this.ConnectedLabel2.Text = "0";
            // 
            // btnIsConnected2
            // 
            this.btnIsConnected2.Location = new System.Drawing.Point(537, 425);
            this.btnIsConnected2.Name = "btnIsConnected2";
            this.btnIsConnected2.Size = new System.Drawing.Size(75, 29);
            this.btnIsConnected2.TabIndex = 64;
            this.btnIsConnected2.Text = "IsConnected";
            this.btnIsConnected2.UseVisualStyleBackColor = true;
            this.btnIsConnected2.Click += new System.EventHandler(this.btnIsConnected2_Click);
            // 
            // IDs_Box
            // 
            this.IDs_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IDs_Box.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IDs_Box.FormattingEnabled = true;
            this.IDs_Box.Location = new System.Drawing.Point(21, 3);
            this.IDs_Box.Name = "IDs_Box";
            this.IDs_Box.Size = new System.Drawing.Size(97, 20);
            this.IDs_Box.TabIndex = 66;
            // 
            // IDs_Box2
            // 
            this.IDs_Box2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IDs_Box2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IDs_Box2.FormattingEnabled = true;
            this.IDs_Box2.Location = new System.Drawing.Point(26, 396);
            this.IDs_Box2.Name = "IDs_Box2";
            this.IDs_Box2.Size = new System.Drawing.Size(97, 20);
            this.IDs_Box2.TabIndex = 67;
            // 
            // buttonRegisterOnNewDataCallback
            // 
            this.buttonRegisterOnNewDataCallback.Location = new System.Drawing.Point(124, 0);
            this.buttonRegisterOnNewDataCallback.Name = "buttonRegisterOnNewDataCallback";
            this.buttonRegisterOnNewDataCallback.Size = new System.Drawing.Size(167, 26);
            this.buttonRegisterOnNewDataCallback.TabIndex = 69;
            this.buttonRegisterOnNewDataCallback.Text = "RegisterOnNewDataCallback";
            this.buttonRegisterOnNewDataCallback.UseVisualStyleBackColor = true;
            this.buttonRegisterOnNewDataCallback.Click += new System.EventHandler(this.buttonRegisterOnNewDataCallback_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 12);
            this.label3.TabIndex = 72;
            this.label3.Text = "OnNewData格式_Callback版";
            // 
            // listNewMessageOnNewDataCallback
            // 
            this.listNewMessageOnNewDataCallback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listNewMessageOnNewDataCallback.CausesValidation = false;
            this.listNewMessageOnNewDataCallback.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listNewMessageOnNewDataCallback.FormattingEnabled = true;
            this.listNewMessageOnNewDataCallback.HorizontalExtent = 20000;
            this.listNewMessageOnNewDataCallback.HorizontalScrollbar = true;
            this.listNewMessageOnNewDataCallback.ItemHeight = 16;
            this.listNewMessageOnNewDataCallback.Location = new System.Drawing.Point(3, 294);
            this.listNewMessageOnNewDataCallback.Name = "listNewMessageOnNewDataCallback";
            this.listNewMessageOnNewDataCallback.ScrollAlwaysVisible = true;
            this.listNewMessageOnNewDataCallback.Size = new System.Drawing.Size(849, 84);
            this.listNewMessageOnNewDataCallback.TabIndex = 71;
            // 
            // SKReply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listNewMessageOnNewDataCallback);
            this.Controls.Add(this.buttonRegisterOnNewDataCallback);
            this.Controls.Add(this.IDs_Box2);
            this.Controls.Add(this.IDs_Box);
            this.Controls.Add(this.ConnectedLabel2);
            this.Controls.Add(this.btnIsConnected2);
            this.Controls.Add(this.ConnectedLabel);
            this.Controls.Add(this.btnIsConnected);
            this.Controls.Add(this.btnSolaceDisconnect2);
            this.Controls.Add(this.btnSolaceDisconnect);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listMessage2);
            this.Controls.Add(this.lblConnectID2);
            this.Controls.Add(this.btnDisconnect2);
            this.Controls.Add(this.btnConnect2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listNewMessage2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.listNewMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblConnectID);
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "SKReply";
            this.Size = new System.Drawing.Size(862, 894);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSignal;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox listMessage;
        private System.Windows.Forms.Label lblConnectID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listNewMessage;
        private System.Windows.Forms.Label lblSignalReplySolace;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListBox listNewMessage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listMessage2;
        private System.Windows.Forms.Button btnSolaceDisconnect;
        private System.Windows.Forms.Label lblSignal2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSignalReplySolace2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConnect2;
        private System.Windows.Forms.Button btnDisconnect2;
        private System.Windows.Forms.Label lblConnectID2;
        private System.Windows.Forms.Button btnSolaceDisconnect2;
        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.Button btnIsConnected;
        private System.Windows.Forms.Label ConnectedLabel2;
        private System.Windows.Forms.Button btnIsConnected2;
        private System.Windows.Forms.ComboBox IDs_Box;
        private System.Windows.Forms.ComboBox IDs_Box2;
        private System.Windows.Forms.Button buttonRegisterOnNewDataCallback;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listNewMessageOnNewDataCallback;
    }
}
