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
    public partial class ForeignStockOrderControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        private int m_nCode;
        public string m_strMessage;

        public delegate void CancelForeignStockOrderHandler(string strLogInID, bool bAsyncOrder, FOREIGNORDER pStock);
        public event CancelForeignStockOrderHandler OnCancelForeignStockOrderSignal;

        public delegate void ForeignStockOrderHandler(string strLogInID, bool bAsyncOrder, FOREIGNORDER pStock);
        public event ForeignStockOrderHandler OnForeignStockOrderSignal;

        public delegate void ForeignStockOrderOLIDHandler(string strLogInID, bool bAsyncOrder, FOREIGNORDER pStock, string strOrderLinkedID);
        public event ForeignStockOrderOLIDHandler OnForeignStockOrderOLIDSignal;

        private string m_UserID = "";
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private string m_UserAccount = "";
        public string UserAccount
        {
            get { return m_UserAccount; }
            set { m_UserAccount = value; }
        }

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public ForeignStockOrderControl()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        
        private void btnSendForeignStockOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strStockNo;
            string strExchangeNo = "";
            string strPrice;
            string strCurrency1;
            string strCurrency2;
            string strCurrency3;
            int nBidAsk;
            int nAccountType;
            int nQty;

            if (boxAccountType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇專戶別");
                return;
            }
            nAccountType = boxAccountType.SelectedIndex + 1;

            if (boxBidAsk.SelectedIndex == 0 && boxCurrency1.SelectedIndex < 0)
            {
                MessageBox.Show("買單請至少選擇扣款幣別 1");
                return;
            }
            strCurrency1 = boxCurrency1.Text;
            strCurrency2 = boxCurrency2.Text;
            strCurrency3 = boxCurrency3.Text;

            if (boxExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;
            int nOrderType = boxBidAsk.SelectedIndex + 1;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (box_inventory.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇庫存別(賣出必填)");
                return;
            }
            int nTradeType = box_inventory.SelectedIndex+1;
            bool bAsync = false;//[-20240311-Add]
            bAsync = AsyncForeignStockOrder.SelectedIndex == 1 ? true : false;
            FOREIGNORDER pForeignOrder = new FOREIGNORDER();

            pForeignOrder.bstrFullAccount = m_UserAccount;
            pForeignOrder.bstrStockNo = strStockNo;
            pForeignOrder.bstrExchangeNo = strExchangeNo;
            pForeignOrder.bstrPrice = strPrice;
            pForeignOrder.bstrCurrency1 = strCurrency1;
            pForeignOrder.bstrCurrency2 = strCurrency2;
            pForeignOrder.bstrCurrency3 = strCurrency3;
            //pForeignOrder.sBuySell = (short)nBidAsk;
            pForeignOrder.nAccountType = nAccountType;
            pForeignOrder.nQty = nQty;
            pForeignOrder.nOrderType = nOrderType;
            pForeignOrder.nTradeType = nTradeType;
            if (OnForeignStockOrderSignal != null)
            {
                OnForeignStockOrderSignal(m_UserID, bAsync, pForeignOrder);
            }
        }

        
        #endregion

        private void btn_CancelForeignOrder_Click(object sender, EventArgs e)
        {
            FOREIGNORDER pOrder = new FOREIGNORDER();
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strExchangeNo = "";
            if (boxCancelExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxCancelExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxCancelExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxCancelExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxCancelExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxCancelExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxCancelExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxCancelExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }

            string strStockNo,strSeqNo, strBookNo;
            strStockNo = txtCancelStockNo.Text.Trim();
            strSeqNo = txtCancelSeqNo.Text.Trim();
            strBookNo = txtCancelBookNo.Text.Trim();


            pOrder.bstrFullAccount = m_UserAccount;

            pOrder.bstrStockNo = strStockNo;
            pOrder.bstrExchangeNo = strExchangeNo;
            pOrder.bstrSeqNo = strSeqNo;
            pOrder.bstrBookNo = strBookNo;
            pOrder.nOrderType = 4;

            if (OnCancelForeignStockOrderSignal != null)
            {
                     OnCancelForeignStockOrderSignal(m_UserID, true, pOrder);
            }
        }

        private void boxExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxExchange.SelectedIndex == 0)//US
            {
                boxCurrency1.SelectedIndex =2;
            }
            
        }

        private void boxAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxAccountType.SelectedIndex == 1)
                boxCurrency2.SelectedIndex = 1;
        }

        private void btnSendForeignStockOrderOLID_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strStockNo;
            string strExchangeNo = "";
            string strPrice;
            string strCurrency1;
            string strCurrency2;
            string strCurrency3;
            int nBidAsk;
            int nAccountType;
            int nQty;

            if (boxAccountType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇專戶別");
                return;
            }
            nAccountType = boxAccountType.SelectedIndex + 1;

            if (boxBidAsk.SelectedIndex == 0 && boxCurrency1.SelectedIndex < 0)
            {
                MessageBox.Show("買單請至少選擇扣款幣別 1");
                return;
            }
            strCurrency1 = boxCurrency1.Text;
            strCurrency2 = boxCurrency2.Text;
            strCurrency3 = boxCurrency3.Text;

            if (boxExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;
            int nOrderType = boxBidAsk.SelectedIndex + 1;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (box_inventory.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇庫存別(賣出必填)");
                return;
            }
            int nTradeType = box_inventory.SelectedIndex + 1;

            bool bAsync = false;
            bAsync = AsyncForeignStockOrder.SelectedIndex == 1 ? true : false;

            FOREIGNORDER pForeignOrder = new FOREIGNORDER();

            pForeignOrder.bstrFullAccount = m_UserAccount;
            pForeignOrder.bstrStockNo = strStockNo;
            pForeignOrder.bstrExchangeNo = strExchangeNo;
            pForeignOrder.bstrPrice = strPrice;
            pForeignOrder.bstrCurrency1 = strCurrency1;
            pForeignOrder.bstrCurrency2 = strCurrency2;
            pForeignOrder.bstrCurrency3 = strCurrency3;
            //pForeignOrder.sBuySell = (short)nBidAsk;
            pForeignOrder.nAccountType = nAccountType;
            pForeignOrder.nQty = nQty;
            pForeignOrder.nOrderType = nOrderType;
            pForeignOrder.nTradeType = nTradeType;
            if (OnForeignStockOrderOLIDSignal != null)
            {
                OnForeignStockOrderOLIDSignal(m_UserID, bAsync, pForeignOrder, txtOrderLinkedID.Text);
            }
        }
    
    }
}
