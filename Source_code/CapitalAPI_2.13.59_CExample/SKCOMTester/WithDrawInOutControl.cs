using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;

namespace SKOrderTester
{
    public partial class WithDrawInOutControl : UserControl
    {

        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        public delegate void WithDrawSignalHandler(string strLogInID, string strAccountOut, int nInType, string strAccountIn, int nOutType, int nCurrency, string strDollars, string strPWD);
        public event WithDrawSignalHandler OnWithDrawSignal;

        public delegate void BalanceSignalHandler(string strLogInID);
        public event BalanceSignalHandler OnBalanceSignal;

        public delegate void GetT3DueAmtSignalHandler(string strLogInID, string strAccount);
        public event GetT3DueAmtSignalHandler OnGetT3DueAmtSignal;

        public delegate void GetIPAAmtSignalHandler(string strLogInID, string strAccount, int nMarket);
        public event GetIPAAmtSignalHandler OnGetIPAAmtSignal;

        public delegate void GetBankBlockSignalHandler(string strLogInID, string strAccount);
        public event GetBankBlockSignalHandler OnGetBankBlockSignal;

        public delegate void GetNTDBankBlockSignalHandler(string strLogInID, string strAccount);
        public event GetNTDBankBlockSignalHandler OnGetNTDBankBlockSignal;

        public delegate void CapitalPayWithDrawSignalHandler(string strLogInID, string strBankCode, string strSwiftCode, string strBankAcno, string strDollars);
        public event CapitalPayWithDrawSignalHandler OnCapitalPayWithDrawSignal;

        private int m_nCode;
        public string m_strMessage;
        
        private string m_UserID = "";
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }
        
        //string strInfo = boxOSFutureAccount.Text;
        private string m_UserAccountTF = "";
        public string UserAccountTF
        {
            get { return m_UserAccountTF; }
            set { m_UserAccountTF = value; }
        }

        private string m_UserAccountOF = "";
        public string UserAccountOF
        {
            get { return m_UserAccountOF; }
            set { m_UserAccountOF = value; }
        }

        private string m_UserAccountTS = "";
        public string UserAccountTS
        {
            get { return m_UserAccountTS; }
            set { m_UserAccountTS = value; }
        }

        private string m_UserAccountOS = "";
        public string UserAccountOS //複委託
        {
            get { return m_UserAccountOS; }
            set { m_UserAccountOS = value; }
        }

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------


        public WithDrawInOutControl()
        {
            InitializeComponent();
        }

        private void boxTypeOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxTypeOut.SelectedIndex == 0)
            {
                if (!boxAccountOut.Items.Contains(m_UserAccountTF))
                    boxAccountOut.Items.Add(m_UserAccountTF);
            }
            else if (boxTypeOut.SelectedIndex == 1)
            {
                if (!boxAccountOut.Items.Contains(m_UserAccountOF))
                    boxAccountOut.Items.Add(m_UserAccountOF);
            }
        }
        #endregion

        private void boxTypeIn_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (boxTypeIn.SelectedIndex == 0)
            {
                if (!boxAccountIn.Items.Contains(m_UserAccountTF))
                { 
                    boxAccountIn.Items.Add(m_UserAccountTF);
                }

            }
            else if (boxTypeIn.SelectedIndex == 1)
            {
                if (!boxAccountIn.Items.Contains(m_UserAccountOF))
                {
                    boxAccountIn.Items.Add(m_UserAccountOF);
                }
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strAccountOut;
            string strAccountIn;
            int nTypeOut;
            int nTypeIn;
            int nCurrency;
            string strDollars;
            string strPWD;
            
            if (boxTypeOut.SelectedIndex <0)
            {
                MessageBox.Show("請選擇轉出類別");
                return;
            }
            nTypeOut = boxTypeOut.SelectedIndex;

            if (boxAccountOut.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇轉出帳號");
                return;
            }
            strAccountOut = boxAccountOut.Text;


            if (boxTypeIn.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇轉入類別");
                return;
            }
            nTypeIn = boxTypeIn.SelectedIndex;

            if (boxAccountIn.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇轉入帳號");
                return;
            }
            strAccountIn = boxAccountIn.Text;
            
            if (boxCurrency.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇幣別");
                return;
            }
            nCurrency = boxCurrency.SelectedIndex;

            if (txtDollars.Text == "")
            {
                MessageBox.Show("請輸入互轉金額");
                return;
            }
            strDollars = txtDollars.Text;

            if (txtPWD.Text == "")
            {
                MessageBox.Show("請輸入出金密碼");
                return;
            }
            strPWD = txtPWD.Text;

            if (OnWithDrawSignal != null)
            {
                OnWithDrawSignal(m_UserID, strAccountOut, nTypeOut, strAccountIn, nTypeIn, nCurrency, strDollars, strPWD);
            }

            
        }

        private void BtnBalanceSend_Click(object sender, EventArgs e)
        {
            if (txtLoginIDBalance.Text == "")
            {
                MessageBox.Show("請輸入登入帳號");
                return;
            }
            string strLoginIDBalance = txtLoginIDBalance.Text;

            if (OnBalanceSignal != null)
            {
                OnBalanceSignal(strLoginIDBalance);
            }
        }

        private void BtnT3DueAmtSend_Click(object sender, EventArgs e)
        {
            if (txtLoginIDT3DueAmt.Text == "")
            {
                MessageBox.Show("請輸入登入帳號");
                return;
            }
            if (txtAccountT3DueAmt.Text == "")
            {
                MessageBox.Show("請輸入證券帳號");
                return;
            }
            string strLoginIDT3DueAmt = txtLoginIDT3DueAmt.Text;
            string strAccountT3DueAmt = txtAccountT3DueAmt.Text;

            if (OnGetT3DueAmtSignal != null)
            {
                OnGetT3DueAmtSignal(strLoginIDT3DueAmt, strAccountT3DueAmt);
            }
        }

        private void BtnIPAAmtSend_Click(object sender, EventArgs e)
        {
            if (boxMarket.Text == "")
            {
                MessageBox.Show("請輸入市場別");
                return;
            }

            int nMarket = boxMarket.SelectedIndex;

            if (OnGetIPAAmtSignal != null)
            {
                OnGetIPAAmtSignal(m_UserID, UserAccountTS, nMarket);
            }
        }

        private void GetBankBlock_btn_Click(object sender, EventArgs e)
        {
            if (boxBankBlock.SelectedIndex == 0)
            {
                if (OnGetNTDBankBlockSignal != null)
                {
                    OnGetNTDBankBlockSignal(m_UserID, UserAccountOS);
                }
            }
            else if (boxBankBlock.SelectedIndex == 1)
            {
                if (OnGetBankBlockSignal != null)
                {
                    OnGetBankBlockSignal(m_UserID, UserAccountOS);
                }
            }
        }

        private void BtnCapitalPayWithDraw_Click(object sender, EventArgs e)
        {
            string strBankCode;
            string strSwiftCode;
            string strBankAcno;
            string strDollars;

            if (txtBankCode_capitalpay.Text == "")
            {
                MessageBox.Show("請輸入銀行代碼");
                return;
            }
            strBankCode = txtBankCode_capitalpay.Text;

            if (txtSwiftCode_capitalpay.Text == "")
            {
                MessageBox.Show("請輸入分行代碼");
                return;
            }
            strSwiftCode = txtSwiftCode_capitalpay.Text;

            if (txtBankAcno_capitalpay.Text == "")
            {
                MessageBox.Show("請輸入銀行帳號");
                return;
            }
            strBankAcno = txtBankAcno_capitalpay.Text;

            if (txtDollars_capitalpay.Text == "")
            {
                MessageBox.Show("請輸入出金金額");
                return;
            }
            strDollars = txtDollars_capitalpay.Text;


            if (OnCapitalPayWithDrawSignal != null)
            {
                OnCapitalPayWithDrawSignal(m_UserID, strBankCode, strSwiftCode, strBankAcno, strDollars);
            }
        }
    }
}