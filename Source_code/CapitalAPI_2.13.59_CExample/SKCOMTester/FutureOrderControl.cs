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
    public partial class FutureOrderControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        private int m_nCode;
        public string m_strMessage;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderHandler OnFutureOrderSignal;

        public delegate void OrderCLRHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderCLRHandler OnFutureOrderCLRSignal;

        public delegate void DecreaseOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty);
        public event DecreaseOrderHandler OnDecreaseOrderSignal;

        public delegate void CancelOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo);
        public event CancelOrderHandler OnCancelOrderSignal;

        public delegate void CancelOrderByStockHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo);
        public event CancelOrderByStockHandler OnCancelOrderByStockSignal;


        public delegate void CorrectPriceBySeqNoHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strPrice, int nTradeType);
        public event CorrectPriceBySeqNoHandler OnCorrectPriceBySeqNo;

        public delegate void CorrectPriceByBookNoHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSymbol, string strSeqNo, string strPrice, int nTradeType);
        public event CorrectPriceByBookNoHandler OnCorrectPriceByBookNo;


        public delegate void OpenInterestHandler(string strLogInID, string strAccount, int nFormat);
        public event OpenInterestHandler OnOpenInterestSignal;

        public delegate void FutureRightsHandler(string strLogInID, string strAccount, int nCoinType);
        public event FutureRightsHandler OnFutureRightsSignal;
        
        public delegate void CancelOrderByBookHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo);
        public event CancelOrderByBookHandler OnCancelOrderByBookSignal;

        public delegate void SendTXOffsetSignalHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strYearMonth, int nBuySell, int nQty);
        public event SendTXOffsetSignalHandler OnSendTXOffsetSignal;

        public delegate void OpenInterestWithFormatHandler(string strLogInID, string strAccount, int nFormat);
        public event OpenInterestWithFormatHandler OnOpenInterestWithFormatSignal;

        public delegate void SendTFOffsetSignalHandler(string strLogInID, bool bAsyncOrder, string strAccount, int nCommodity, string strYearMonth, int nBuySell, int nQty);
        public event SendTFOffsetSignalHandler OnSendTFOffsetSignal;

        public delegate void SendTFOffsetNewSignalHandler(string strLogInID, bool bAsyncOrder, string strAccount, int nCommodity, string strYearMonth, int nBuySell, int nQty, int nQty_2, int nQty_3);
        public event SendTFOffsetNewSignalHandler OnSendTFOffsetNewSignal;

        public delegate void OpenInterestOldHandler(string strLogInID, string strAccount);
        public event OpenInterestOldHandler OnOpenInterestOldSignal;

        public delegate void AlterOrderSignalHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event AlterOrderSignalHandler OnAlterOrderSignal;

        public delegate void OrderGWHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderGWHandler OnFutureOrderGWSignal;

        public delegate void DecreaseOrderBookHandler(string strLogInID, bool bAsyncOrder, string strAccount, int nMarketType, string strBookNo, int nDecreaseQty);
        public event DecreaseOrderBookHandler OnDecreaseOrderBookSignal;

        public delegate void OrderReportHandler(string strLogInID, string strAccount, long lFormat);
        public event OrderReportHandler OnOrderReportGWSignal;

        public delegate void FulfillReportHandler(string strLogInID, string strAccount, long lFormat);
        public event FulfillReportHandler OnFulfillReportGWSignal;

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
        public FutureOrderControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private void btnSendFutureOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            int nQty;


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo.Text.Trim();

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;         

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            //if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false)
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M" && txtPrice.Text.Trim() != "P")
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

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount    = m_UserAccount;
            pFutureOrder.bstrPrice          = strPrice;
            pFutureOrder.bstrStockNo        = strFutureNo;
            pFutureOrder.nQty               = nQty;
            pFutureOrder.sBuySell           = (short)nBidAsk;
            pFutureOrder.sDayTrade          = (short)nFlag;
            pFutureOrder.sTradeType         = (short)nPeriod;            

            if (OnFutureOrderSignal != null)
            {
                OnFutureOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendFutureOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            int nQty;

            string strCIDTandem;
            string strSettlementMonth;
            string strSettlementMonth2;

            if (txtStockNo.Text.Trim() == "" && txtCIDTandem.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo.Text.Trim();
            strCIDTandem = txtCIDTandem.Text.Trim();

            if(txtCIDTandem.Text.Trim() != "")
            {
                if (txtSettlementMonth.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入商品年月");
                    return;
                }
            }
            strSettlementMonth = txtSettlementMonth.Text.Trim();
            strSettlementMonth2 = txtSettlementMonth2.Text.Trim();


            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;            

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            //if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false)
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M" && txtPrice.Text.Trim() != "P")
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

            bool bAsync = false;
            bAsync = TFAsyncFlag.SelectedIndex == 1 ? true : false;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrCIDTandem = strCIDTandem;
            pFutureOrder.bstrSettlementMonth = strSettlementMonth;
            pFutureOrder.bstrSettlementMonth2 = strSettlementMonth2;

            if (OnFutureOrderSignal != null)
            {
                OnFutureOrderSignal(m_UserID, bAsync, pFutureOrder);
            }

        }

        private void btnSendFutureOrderCLR_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            int nQty;
            int nNewClose;
            int nReserved;


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo.Text.Trim();

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;

            if (boxNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewClose.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M" && txtPrice.Text.Trim() != "P")
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

            if (boxReserved.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = boxReserved.SelectedIndex;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sReserved = (short)nReserved;

            if (OnFutureOrderCLRSignal != null)
            {
                OnFutureOrderCLRSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendFutureOrderCLRAsync_Click(object sender, EventArgs e)
        {
            if (GW_Box.Text != "GW" || GW_Box.SelectedIndex == -1)
            {
                if (m_UserAccount == "")
                {
                    MessageBox.Show("請選擇期貨帳號");
                    return;
                }

                string strFutureNo;
                int nBidAsk;
                int nPeriod;
                int nFlag;
                string strPrice;
                int nQty;
                int nNewClose;
                int nReserved;

                if (txtStockNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入商品代碼");
                    return;
                }
                strFutureNo = txtStockNo.Text.Trim();

                if (boxBidAsk.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別");
                    return;
                }
                nBidAsk = boxBidAsk.SelectedIndex;

                if (boxPeriod.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託條件");
                    return;
                }
                nPeriod = boxPeriod.SelectedIndex;

                if (boxNewClose.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇倉別");
                    return;
                }
                nNewClose = boxNewClose.SelectedIndex;

                if (boxFlag.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇當沖與否");
                    return;
                }
                nFlag = boxFlag.SelectedIndex;

                double dPrice = 0.0;
                if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M" && txtPrice.Text.Trim() != "P")
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

                if (boxReserved.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇盤別");
                    return;
                }
                nReserved = boxReserved.SelectedIndex;

                bool bAsyncFlag = false;
                if (TFAsyncFlag.SelectedIndex == 0)
                    bAsyncFlag = false;
                else if (TFAsyncFlag.SelectedIndex == 1)
                    bAsyncFlag = true;


                FUTUREORDER pFutureOrder = new FUTUREORDER();

                pFutureOrder.bstrFullAccount = m_UserAccount;
                pFutureOrder.bstrPrice = strPrice;
                pFutureOrder.bstrStockNo = strFutureNo;
                pFutureOrder.nQty = nQty;
                pFutureOrder.sBuySell = (short)nBidAsk;
                pFutureOrder.sDayTrade = (short)nFlag;
                pFutureOrder.sTradeType = (short)nPeriod;
                pFutureOrder.sNewClose = (short)nNewClose;
                pFutureOrder.sReserved = (short)nReserved;

                if (OnFutureOrderCLRSignal != null)
                {
                    OnFutureOrderCLRSignal(m_UserID, bAsyncFlag, pFutureOrder);
                }
            }
            else
            {
                SendFutureOrderGW();
            }
        }
        #endregion

        private void btnDecreaseQty_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            int nQty = 0;
            string strSeqNo;

            if (txtDecreaseSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtDecreaseSeqNo.Text.Trim();

            if (int.TryParse(txtDecreaseQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("改量請輸入數字");
                return;
            }
            if (OnDecreaseOrderSignal != null)
            {
                OnDecreaseOrderSignal(m_UserID, true, m_UserAccount, strSeqNo, nQty);
            }
        }

        private void btnCancelOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            string strSeqNo;

            if (txtCancelSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtCancelSeqNo.Text.Trim();
            if (OnCancelOrderSignal != null)
            {
                OnCancelOrderSignal(m_UserID, true, m_UserAccount, strSeqNo);
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            string strStockNo;

            if (txtCancelStockNo.Text.Trim() == "")
            {
                if (MessageBox.Show("未輸入商品代碼會刪除所有委託單，是否刪單?", "委託全部刪單", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("已取消本次操作");
                    return;
                }
            }
            strStockNo = txtCancelStockNo.Text.Trim();
            if (OnCancelOrderByStockSignal != null)
            {
                OnCancelOrderByStockSignal(m_UserID, true, m_UserAccount, strStockNo);
            }
        }

        private void btnCorrectPriceBySeqNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            if (OnCorrectPriceBySeqNo != null)
            {
                int nTradeType;
                string strSeqNo;
                string strPrice;

                if (txtCorrectSeqNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入委託序號");
                    return;
                }
                strSeqNo = txtCorrectSeqNo.Text.Trim();

                double dPrice = 0.0;
                if (double.TryParse(txtCorrectPrice.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("修改價格請輸入數字");
                    return;
                }
                strPrice = txtCorrectPrice.Text.Trim();

                if (boxCorrectTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託條件");
                    return;
                }
                nTradeType = boxCorrectTradeType.SelectedIndex;

                OnCorrectPriceBySeqNo(m_UserID, true, m_UserAccount, strSeqNo, strPrice, nTradeType);
            }
        }

        private void btnCorrectPriceByBookNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            if (OnCorrectPriceByBookNo != null)
            {
                int nTradeType;
                string strBookNo;
                string strPrice;

                if (txtCorrectBookNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入委託書號");
                    return;
                }
                strBookNo = txtCorrectBookNo.Text.Trim();

                double dPrice = 0.0;
                if (double.TryParse(txtCorrectPrice.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("修改價格請輸入數字");
                    return;
                }
                strPrice = txtCorrectPrice.Text.Trim();

                if (boxCorrectSymbol.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇市場簡稱");
                    return;
                }
                nTradeType = boxCorrectTradeType.SelectedIndex;

                if (boxCorrectTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託條件");
                    return;
                }

                OnCorrectPriceByBookNo(m_UserID, true, m_UserAccount, boxCorrectSymbol.Text.Trim(), strBookNo, strPrice, nTradeType);
            }
        }

        private void btnGetOpenInterestFormat_Click(object sender, EventArgs e)
        {
            int nFormat;
            if (FormatBox.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇格式");
                    return;
                }
             nFormat = (FormatBox.SelectedIndex)+1;


             if (OnOpenInterestWithFormatSignal != null)
            {
                OnOpenInterestWithFormatSignal(m_UserID, m_UserAccount, nFormat);
            }
        }

        private void btnGetFutureRights_Click(object sender, EventArgs e)
        {
            int nCoinType;
            if (comBox_CoinType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇幣別");
                return;
            }
            nCoinType = comBox_CoinType.SelectedIndex;
            
            if (OnFutureRightsSignal != null)
            {

                OnFutureRightsSignal(m_UserID, m_UserAccount, nCoinType);
            }
        }

        private void btnCancelOrderByBookNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            string strBookNo;

            if (txtCancelBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strBookNo = txtCancelBookNo.Text.Trim();
            if (OnCancelOrderByBookSignal != null)
            {
                OnCancelOrderByBookSignal(m_UserID, true, m_UserAccount, strBookNo);
            }
        }

        private void btnSendTXOffset_Click(object sender, EventArgs e)
        {
            int nBidAsk, nQty;
            string strYearMonth;

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            if (txtOffsetYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOffsetYearMonth.Text.Trim();

            if (boxOffsetBuySell.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxOffsetBuySell.SelectedIndex;

            if (int.TryParse(txtOffsetNewQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (OnSendTXOffsetSignal != null)
            {
                OnSendTXOffsetSignal(m_UserID, true, m_UserAccount, strYearMonth, nBidAsk, nQty);
            }
        }

        private void GetOpenInterestGW_Click(object sender, EventArgs e)
        {            

            if (OnOpenInterestSignal != null)
            {
                OnOpenInterestSignal(m_UserID, m_UserAccount,1);
            }
        }
        private void TFOffset_Click(object sender, EventArgs e)
        {
            int nBidAsk, nQty, nCommodity;
            string strYearMonth;

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            if (txtOffsetYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOffsetYearMonth.Text.Trim();

            if (CommodityBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇互抵商品");
                return;
            }
            nCommodity = CommodityBox.SelectedIndex;

            if (boxOffsetBuySell.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxOffsetBuySell.SelectedIndex;

            if (int.TryParse(txtOffsetQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (OnSendTFOffsetSignal != null)
            {
                OnSendTFOffsetSignal(m_UserID, true, m_UserAccount, nCommodity, strYearMonth, nBidAsk, nQty);
            }
        }

        
        private void GetOpenInterest_Click(object sender, EventArgs e)
        {
            if (OnOpenInterestOldSignal != null)
            {
                OnOpenInterestOldSignal(m_UserID, m_UserAccount);
            }
        }
        private void SendFutureOrderGW()
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nBidAsk2;
            int nPeriod;
            int nFlag;
            string strPrice;
            int nQty;
            int nNewClose;
            int nReserved;

            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo.Text.Trim();

            bool bSpred = false;//GW//
            if (strFutureNo.Contains("/") == true)
            {
                bSpred = true;

            }//GW//

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (bSpred && boxBidAsk2.SelectedIndex < 0)
            {//GW//
                MessageBox.Show("請選擇第二腳買賣別");
                return;
            }//GW//
            nBidAsk2 = boxBidAsk2.SelectedIndex;//GW//

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;

            if (boxNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewClose.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M" && txtPrice.Text.Trim() != "P")
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

            if (boxReserved.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = boxReserved.SelectedIndex;

            bool bAsyncFlag = false;
            if (TFAsyncFlag.SelectedIndex == 0)
                bAsyncFlag = false;
            else if (TFAsyncFlag.SelectedIndex == 1)
                bAsyncFlag = true;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sReserved = (short)nReserved;
            if (bSpred)//GW//
                pFutureOrder.sBuySell2 = (short)nBidAsk2;

            if (OnFutureOrderGWSignal != null)
            {
                OnFutureOrderGWSignal(m_UserID, bAsyncFlag, pFutureOrder);
            }
        }
        private void AlterOrder_Click(object sender, EventArgs e)
        {

            int nPreOrder;
            string strSeqNo;
            string strBookNo;
            string strAlterType;
            string strCorrectPrice;
            int nQty;
            int nCond;

            if (AlterPreOrderBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為預約單");
                return;
            }
            nPreOrder = AlterPreOrderBox.SelectedIndex;

            if (AlterSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入序號");
                return;
            }
            strSeqNo = AlterSeqNo.Text.Trim();

            //if (AlterBookNo.Text.Trim() == "")
            //{
            //MessageBox.Show("請輸入書號");
            //return;
            //}
            strBookNo = AlterBookNo.Text.Trim();


            int.TryParse(AlterQty.Text.Trim(), out nQty);
            //if( int.TryParse(txtDecreaseQty.Text.Trim(),out nQty) == false)
            //減量//


            strCorrectPrice = AlterCorrectPrice.Text.Trim();

            if (TFAlterTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇刪除/減量/改價");
                return;
            }
            if (TFAlterTypeBox.SelectedIndex == 0)
                strAlterType = "D";
            else if (TFAlterTypeBox.SelectedIndex == 1)
                strAlterType = "C";
            else if (TFAlterTypeBox.SelectedIndex == 2)
                strAlterType = "P";
            else
            {
                MessageBox.Show("請重新選擇!");
                return;
            }

            //boxMarket
            if (AlterCondBox.SelectedIndex < 0 && AlterCondBox.SelectedIndex <= 2)
            {
                MessageBox.Show("請選擇COND");
                return;
            }
            nCond = AlterCondBox.SelectedIndex;

            bool bAsync = false;
            bAsync = (AlterAsyncBox.SelectedIndex == 0) ? false : true;

            SKCOMLib.FUTUREORDER pOrder = new FUTUREORDER();
            pOrder.bstrFullAccount = m_UserAccount;

            pOrder.nTimeFlag = nPreOrder;
            pOrder.bstrSeqNo = strSeqNo;
            pOrder.bstrBookNo = strBookNo;
            pOrder.bstrOrderType = strAlterType;
            pOrder.sTradeType = (short)nCond;

            pOrder.nQty = nQty;
            pOrder.bstrPrice = strCorrectPrice;

            if (OnAlterOrderSignal != null)
            {
                OnAlterOrderSignal(m_UserID, bAsync, pOrder);
            }
        }

        private void btnBookDecreaseQty_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            int nMarket = 0;

            int nQty = 0;
            string strBookNo;

            if (MarketTypeBox.SelectedIndex == -1)
            {
                MessageBox.Show("請選擇市場");
                return;
            }
            nMarket = MarketTypeBox.SelectedIndex;


            if (DBookNoTxt.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strBookNo = DBookNoTxt.Text.Trim();

            if (int.TryParse(txtDecreaseQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("改量請輸入數字");
                return;
            }
            if (OnDecreaseOrderBookSignal != null)
            {
                OnDecreaseOrderBookSignal(m_UserID, true, m_UserAccount,nMarket, strBookNo, nQty);
            }
        }

        private void BtnSendTFOffsetNewNew_Click(object sender, EventArgs e)
        {
            int nBidAsk, nQty, nQty_2, nQty_3, nCommodity;
            string strYearMonth;

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            if (txtOffsetNewYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOffsetNewYearMonth.Text.Trim();

            if (CommodityBoxNew.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇互抵商品");
                return;
            }
            nCommodity = CommodityBoxNew.SelectedIndex;

            if (boxOffsetNewBuySell.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxOffsetNewBuySell.SelectedIndex;

            if (int.TryParse(txtOffsetNewQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量1請輸入數字");
                return;
            }
            if (int.TryParse(txtOffsetNewQty_2.Text.Trim(), out nQty_2) == false)
            {
                MessageBox.Show("委託量2請輸入數字");
                return;
            }
            if (int.TryParse(txtOffsetNewQty_3.Text.Trim(), out nQty_3) == false)
            {
                MessageBox.Show("委託量3請輸入數字");
                return;
            }

            if (OnSendTFOffsetNewSignal != null)
            {
                OnSendTFOffsetNewSignal(m_UserID, true, m_UserAccount, nCommodity, strYearMonth, nBidAsk, nQty, nQty_2, nQty_3);
            }
        }

        private void buttonOrderReport_Click(object sender, EventArgs e)
        {
            if (boxFormat.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託回報類別");
                return;
            }
            int nFormat = (boxFormat.SelectedIndex + 1);
            if (OnOrderReportGWSignal != null)
            {
                OnOrderReportGWSignal(m_UserID, m_UserAccount, nFormat);
            }
        }

        private void buttonFullfillReport_Click(object sender, EventArgs e)
        {
            if (boxFormat2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇成交回報類別");
                return;
            }
            int nFormat = (boxFormat2.SelectedIndex + 1);
            if (OnFulfillReportGWSignal != null)
            {
                OnFulfillReportGWSignal(m_UserID, m_UserAccount, nFormat);
            }
        }
    }
}
