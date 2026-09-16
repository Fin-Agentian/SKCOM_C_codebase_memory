
namespace SKCOMTester
{
    partial class ViewDataGrid_FutureR
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
            this.dataGridView_FutureR = new System.Windows.Forms.DataGridView();
            this.BTN_ClearRows = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FutureR)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_FutureR
            // 
            this.dataGridView_FutureR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_FutureR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_FutureR.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView_FutureR.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView_FutureR.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_FutureR.Name = "dataGridView_FutureR";
            this.dataGridView_FutureR.RowTemplate.Height = 24;
            this.dataGridView_FutureR.Size = new System.Drawing.Size(1027, 141);
            this.dataGridView_FutureR.TabIndex = 0;
            // 
            // BTN_ClearRows
            // 
            this.BTN_ClearRows.Location = new System.Drawing.Point(943, 168);
            this.BTN_ClearRows.Name = "BTN_ClearRows";
            this.BTN_ClearRows.Size = new System.Drawing.Size(85, 19);
            this.BTN_ClearRows.TabIndex = 1;
            this.BTN_ClearRows.Text = "ClearRows";
            this.BTN_ClearRows.UseMnemonic = false;
            this.BTN_ClearRows.UseVisualStyleBackColor = true;
            this.BTN_ClearRows.Click += new System.EventHandler(this.BTN_ClearRows_Click);
            // 
            // ViewDataGrid_FutureR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1051, 202);
            this.Controls.Add(this.BTN_ClearRows);
            this.Controls.Add(this.dataGridView_FutureR);
            this.Name = "ViewDataGrid_FutureR";
            this.Text = "DataGrid_FutureR表格化OnFutureRights";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewDataGrid_FutureR_FormClosing);
            this.Load += new System.EventHandler(this.ViewDataGrid_FutureR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FutureR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_FutureR;
        private System.Windows.Forms.Button BTN_ClearRows;
    }
}