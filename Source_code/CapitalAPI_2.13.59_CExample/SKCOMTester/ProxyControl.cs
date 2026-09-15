using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SKCOMLib;

namespace SKOrderTester
{
    public partial class ProxyControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------



        public delegate void OFSpreadProxyOrderHandler(string strLogInID, OVERSEAFUTUREORDER pStock);//[20231129]
        public event OFSpreadProxyOrderHandler OnOverseaFutureProxyOrderSpreadSignal;

        public delegate void OFSpreadProxyOrder2Handler(string strLogInID, OVERSEAFUTUREORDER pStock);//[20231129]
        public event OFSpreadProxyOrder2Handler OnOverseaFutureProxyOrderSpread2Signal;

        public delegate void OFProxyAlterHandler(string strLogInID, OVERSEAFUTUREORDER pStock); //[20231129]
        public event OFProxyAlterHandler OnOverseaFutureProxyAlterSignal;

        public delegate void OFProxyOrderHandler(string strLogInID, OVERSEAFUTUREORDER pStock);
        public event OFProxyOrderHandler OnOverseaFutureProxyOrderSignal;

        //public delegate void OOProxyOrderHandler(string strLogInID, OSFUTUREPROXYORDER pStock);
        // public event OOProxyOrderHandler OnOverseaOptionProxyOrderSignal;

        // public delegate void OOProxyAlterHandler(string strLogInID, OSFUTUREPROXYORDER pStock);
        // public event OOProxyAlterHandler OnOverseaOptionProxyAlterSignal;

        public delegate void TSProxyOrderHandler(string strLogInID, STOCKPROXYORDER pStock); //[20231123]
        public event TSProxyOrderHandler OnStockProxyOrderSignal;

        public delegate void TSProxyAlterHandler(string strLogInID, STOCKPROXYORDER pStock); //[20231123]
        public event TSProxyAlterHandler OnStockProxyAlterSignal;

        public delegate void TSProxyPreAlterHandler(string strLogInID, STOCKPROXYORDER pStock); //20250115
        public event TSProxyPreAlterHandler OnStockProxyPreAlterSignal;

        public delegate void TFProxyOrderHandler(string strLogInID, FUTUREPROXYORDER pStock); //[20231123]
        public event TFProxyOrderHandler OnFutureProxyOrderSignal;

        public delegate void TFProxyAlterHandler(string strLogInID, FUTUREPROXYORDER pStock); //[20231128]
        public event TFProxyAlterHandler OnFutureProxyAlterSignal;

        public delegate void ForeignStockProxyOrderHandler(string strLogInID, OSSTOCKPROXYORDER pStock); //[20231127]
        public event ForeignStockProxyOrderHandler OnForeignProxyOrderSignal;

        public delegate void ForeignStockProxyCancelHandler(string strLogInID, OSSTOCKPROXYORDER pStock); //[20231127]
        public event ForeignStockProxyCancelHandler OnForeignProxyCancelSignal;

        public delegate void OptionProxyOrderHandler(string strLogInID, FUTUREPROXYORDER pStock); //[20231128]
        public event OptionProxyOrderHandler OnOptionProxyOrderSignal;

        public delegate void OptionProxyAlterHandler(string strLogInID, FUTUREPROXYORDER pStock); //[20231128]
        public event OptionProxyAlterHandler OnOptionProxyAlterSignal;

        public delegate void DuplexProxyOrderHandler(string strLogInID, FUTUREPROXYORDER pStock); //[20231128]
        public event DuplexProxyOrderHandler OnDuplexProxyOrderSignal;

        public delegate void OOProxyOrderHandler(string strLogInID, OVERSEAFUTUREORDER pStock);//[20231130]
        public event OOProxyOrderHandler OnOverseaOptionProxyOrderSignal;

        private int m_nCode;
        public string m_strMessage;

        private string m_UserID = "";
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private string m_UserStockAccount = "";
        public string UserStockAccount
        {
            get { return m_UserStockAccount; }
            set { m_UserStockAccount = value; }
        }

        private string m_UserFutureAccount = "";
        public string UserFutureAccount
        {
            get { return m_UserFutureAccount; }
            set { m_UserFutureAccount = value; }
        }

        private string m_UserOSAccount = "";
        public string UserOSAccount
        {
            get { return m_UserOSAccount; }
            set { m_UserOSAccount = value; }
        }

        private string m_UserForeignStockAccount = "";
        public string UserForeignStockAccount
        {
            get { return m_UserForeignStockAccount; }
            set { m_UserForeignStockAccount = value; }
        }

        #endregion
        public ProxyControl()
        {
            InitializeComponent();
        }

        /*
        private void btnSendForeignStockProxyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserForeignStockAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strStockNo;
            string strPrice;
            string strCurrency1;
            string strCurrency2;
            string strCurrency3;
            string strExchangeNo = "";
            int nBidAsk;
            int nAccountType;
            int nQty=0;
            int nTradeType = 0;
            string strProxyQty="";
            if (boxProxyAccountType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇專戶別");
                return;
            }
            nAccountType = boxProxyAccountType.SelectedIndex + 1;

            if (boxProxyExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxProxyExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxProxyExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxProxyExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxProxyExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxProxyExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxProxyExchange.SelectedIndex == 5)
            {
                strExchangeNo = "HA";
            }
            else if (boxProxyExchange.SelectedIndex == 6)
            {
                strExchangeNo = "SA";
            }

            if (boxProxyBidAsk.SelectedIndex == 0 && boxProxyCurrency1.SelectedIndex < 0)
            {
                MessageBox.Show("買單請至少選擇扣款幣別 1");
                return;
            }
            strCurrency1 = boxProxyCurrency1.Text;
            strCurrency2 = boxProxyCurrency2.Text;
            strCurrency3 = boxProxyCurrency3.Text;

            if (txtProxyOStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyOStockNo.Text.Trim();

            if (boxProxyBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxProxyBidAsk.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtProxyPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyPrice.Text.Trim();

            if (nBidAsk == 1 && strExchangeNo == "US")
            {
                if(boxProxyTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇庫存別");
                    return;
                }
                nTradeType = boxProxyTradeType.SelectedIndex + 1;
            }
            else 
            {
                if (boxProxyTradeType.SelectedIndex < 0)
                    nTradeType = 0;
                else 
                {
                    nTradeType = boxProxyTradeType.SelectedIndex + 1;
                }
            }

            double dQty = 0.0;
            if (nBidAsk == 1 && strExchangeNo == "US" && nTradeType == 2)
            {
                if (double.TryParse(txtProxyQty.Text.Trim(), out dQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }
            else
            {
                if (int.TryParse(txtProxyQty.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入整數數字");
                    return;
                }
            }
            strProxyQty = txtProxyQty.Text.Trim();

            OSSTOCKPROXYORDER pForeignOrder = new OSSTOCKPROXYORDER();

            pForeignOrder.bstrFullAccount = m_UserForeignStockAccount;
            pForeignOrder.bstrStockNo = strStockNo;
            pForeignOrder.bstrPrice = strPrice;
            pForeignOrder.bstrExchangeNo = strExchangeNo;
            pForeignOrder.bstrCurrency1 = strCurrency1;
            pForeignOrder.bstrCurrency2 = strCurrency2;
            pForeignOrder.bstrCurrency3 = strCurrency3;
            pForeignOrder.nOrderType = nBidAsk+1;
            pForeignOrder.nAccountType = nAccountType;
            pForeignOrder.bstrProxyQty = strProxyQty;
            pForeignOrder.nTradeType = nTradeType;

            if (OnForeignProxyOrderSignal != null)
            {
                OnForeignProxyOrderSignal(m_UserID, pForeignOrder);
            }
        }

        private void btnProxyCancelForeignOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (m_UserForeignStockAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strExchangeNo = "";
            string strSeqNo = "";
            string strOrderNo = "";
            string strStockNo = "";
            if (boxProxyCancelExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxProxyCancelExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 5)
            {
                strExchangeNo = "HA";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 6)
            {
                strExchangeNo = "SA";
            }

            if (txtProxyOStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyOStockNo2.Text.Trim();

            if (txtProxyCancelSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxyCancelSeqNo.Text.Trim();

            if (txtProxyCancelBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyCancelBookNo.Text.Trim();

            OSSTOCKPROXYORDER pForeignOrder = new OSSTOCKPROXYORDER();
            pForeignOrder.bstrFullAccount = m_UserForeignStockAccount;
            pForeignOrder.bstrExchangeNo = strExchangeNo;
            pForeignOrder.bstrBookNo = strOrderNo;
            pForeignOrder.bstrSeqNo = strSeqNo;
            pForeignOrder.bstrStockNo = strStockNo;
            if (OnForeignProxyCancelSignal != null)
            {
                OnForeignProxyCancelSignal(m_UserID, pForeignOrder);
            }
        }

        private void btnSendOverseaFutureSpreadProxyOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnSendOverseaFutureProxyOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnSendStockProxyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserStockAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            string strPrice;
            string strOrderType = "";
            int nQty;
            int nORDERType, nPriceType, nTimeInForce, nMarketType, nPriceMark;

            if (txtProxyStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyStockNo.Text.Trim();


            if (ProxyOrderTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox.SelectedIndex + 1;

            if (ProxyPriceTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            nPriceType = ProxyPriceTypeBox.SelectedIndex + 1;

            if (ProxyTimeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託時效");
                return;
            }
            nTimeInForce = ProxyTimeBox.SelectedIndex;

            if (ProxyMarketBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nMarketType = ProxyMarketBox.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyStockPrice.Text.Trim();

            if (int.TryParse(txtStockQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (ProxyPriceMarkBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格旗標");
                return;
            }
            nPriceMark = ProxyPriceMarkBox.SelectedIndex;
          
            switch(nORDERType)
            {
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;            
                case 3: strOrderType = "3"; break;
                case 4: strOrderType = "4"; break;
                case 5: strOrderType = "5"; break;
                case 6: strOrderType = "6"; break;
                case 7: strOrderType = "7"; break;
                default: strOrderType = ""; break;
            }

            SKCOMLib.STOCKPROXYORDER pOrder = new STOCKPROXYORDER();

            pOrder.bstrFullAccount = m_UserStockAccount;
            pOrder.bstrStockNo = strStockNo;
            pOrder.bstrOrderType = strOrderType;
            pOrder.nSpecialTradeType = nPriceType;
            pOrder.nTradeType = nTimeInForce;
            pOrder.nPeriod = nMarketType;
            pOrder.bstrPrice = strPrice;
            pOrder.nQty = nQty;
            pOrder.nPriceMark = nPriceMark;

            if (OnStockProxyOrderSignal != null)
            {
                OnStockProxyOrderSignal(m_UserID, pOrder);
            }
        }

        private void btnSendStockProxyAlter_Click(object sender, EventArgs e)
        {
            if (m_UserStockAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            string strPrice, strOrderNo, strSeqNo, strOrderType="";
            int nQty;
            int nPriceType, nTimeInForce, nMarketType, nPriceMark, nORDERType;

            if (txtProxyStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyStockNo2.Text.Trim();

            if (ProxyOrderTypeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox2.SelectedIndex;


            if (ProxyPriceTypeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            nPriceType = ProxyPriceTypeBox2.SelectedIndex + 1;

            if (ProxyTimeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託時效");
                return;
            }
            nTimeInForce = ProxyTimeBox2.SelectedIndex;

            if (ProxyMarketBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nMarketType = ProxyMarketBox2.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyStockPrice2.Text.Trim();

            if (int.TryParse(txtStockQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (ProxyPriceMarkBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格旗標");
                return;
            }
            nPriceMark = ProxyPriceMarkBox2.SelectedIndex;

            if (txtStockBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtStockBookNo.Text.Trim();

            if (txtStockSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtStockSeqNo.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            SKCOMLib.STOCKPROXYORDER pOrder = new STOCKPROXYORDER();

            pOrder.bstrFullAccount = m_UserStockAccount;
            pOrder.bstrStockNo = strStockNo;
            pOrder.nSpecialTradeType = nPriceType;
            pOrder.nTradeType = nTimeInForce;
            pOrder.nPeriod = nMarketType;
            pOrder.bstrPrice = strPrice;
            pOrder.nQty = nQty;
            pOrder.nPriceMark = nPriceMark;
            pOrder.bstrBookNo = strOrderNo;
            pOrder.bstrSeqNo = strSeqNo;
            pOrder.bstrOrderType = strOrderType;

            if (OnStockProxyAlterSignal != null)
            {
                OnStockProxyAlterSignal(m_UserID, pOrder);
            }
        }

        private void btnSendProxyFutureOrder_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            string strYM;
            int nBS;
            int nCOND;
            int nDayTrade;
            string strPrice;
            int nQty;
            int nPriceFlag;

            if (txtFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtFutureNo.Text.Trim();

            if (txtProxyYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM.Text.Trim();

            if (boxProxyBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS.SelectedIndex;

            if (boxProxyCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND.SelectedIndex;

            if (boxDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxDayTrade.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtFuturePrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtFuturePrice.Text.Trim();

            if (int.TryParse(txtFutureQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxProxyPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxProxyPriceFlag.SelectedIndex;

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.nBuySell = nBS;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.nPriceFlag = nPriceFlag;

            if (OnFutureProxyOrderSignal != null)
            {
                OnFutureProxyOrderSignal(m_UserID, pFutureOrder);
            }
        }

        private void btnSendProxyFutureOrderCLR_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            string strYM;
            int nBS;
            int nCOND;
            int nDayTrade;
            string strPrice;
            int nQty;
            int nPriceFlag;
            int nORDERType;
            int nPreOrder;
            string strOrderType;

            if (txtFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtFutureNo.Text.Trim();

            if (txtProxyYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM.Text.Trim();

            if (boxProxyBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS.SelectedIndex;

            if (boxProxyCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND.SelectedIndex;

            if (boxDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxDayTrade.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtFuturePrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtFuturePrice.Text.Trim();

            if (int.TryParse(txtFutureQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxProxyPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxProxyPriceFlag.SelectedIndex;

            if (boxProxyORDERType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxProxyORDERType.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            if (boxProxyPreOrder.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder.SelectedIndex;

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.nBuySell = nBS;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.bstrOrderType = strOrderType;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.nPriceFlag = nPriceFlag;
            pFutureOrder.nReserved = nPreOrder;

            if (OnFutureProxyOrderCLRSignal != null)
            {
                OnFutureProxyOrderCLRSignal(m_UserID, pFutureOrder);
            }
        }

        private void btnProxyFutureAlter_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            int nCOND;
            string strPrice = "";
            int nQty = 0;
            int nPreOrder;
            string strOrderNo;
            string strSeqNo, strOrderType="";
            int nORDERType;

            if (boxProxyCOND2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND2.SelectedIndex;


            if (boxProxyPreOrder2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder2.SelectedIndex;

            if (boxProxyORDERType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = boxProxyORDERType2.SelectedIndex;
            if (nORDERType == 2)
            {
                double dPrice = 0.0;
                if (double.TryParse(txtProxyPrice2.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
                strPrice = txtProxyPrice2.Text.Trim();
            }

            if (nORDERType == 0 || nORDERType == 1)
            {
                if (int.TryParse(txtProxyQty2.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }

            if (txtProxyOrderNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyOrderNo.Text.Trim();

            if (txtProxySeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxySeqNo.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.nQty = nQty;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.bstrOrderType = strOrderType;
            pFutureOrder.nReserved = nPreOrder;
            pFutureOrder.bstrBookNo = strOrderNo;
            pFutureOrder.bstrSeqNo = strSeqNo;

            if (OnFutureProxyAlterSignal != null)
            {
                OnFutureProxyAlterSignal(m_UserID, pFutureOrder);
            }
        }

        private void btnSendProxyOptionOrder_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo, strYM, strStrike, strOrderType="";
            int nBS;
            string strPrice;
            int nQty, nPriceFlag;
            int nCP, nORDERType, nCOND, nDayTrade, nPreOrder;

            if (txtOptionNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOptionNo.Text.Trim();

            if (txtOptionYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtOptionYM.Text.Trim();

            if (txtProxyStrike.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrike = txtProxyStrike.Text.Trim();

            if (boxProxyCP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCP = boxProxyCP.SelectedIndex;

            if (boxOptionBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxOptionBS.SelectedIndex;

            if (boxOptionCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxOptionCOND.SelectedIndex;

            if (boxORDERType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxORDERType.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtOptionPrice.Text.Trim(), out dPrice) == false && txtOptionPrice.Text.Trim() != "M" && txtOptionPrice.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtOptionPrice.Text.Trim();

            if (int.TryParse(txtOptionQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOptionDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否當沖");
                return;
            }
            nDayTrade = boxOptionDayTrade.SelectedIndex;

            if (boxPreOrder.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxPreOrder.SelectedIndex;

            if (boxPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxPriceFlag.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.bstrStrike = strStrike;
            pFutureOrder.nBuySell =nBS;
            pFutureOrder.nCP = nCP;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.nPriceFlag = nPriceFlag;
            pFutureOrder.nReserved = nPreOrder;
            pFutureOrder.bstrOrderType = strOrderType;

            if (OnOptionProxyOrderSignal != null)
            {
                OnOptionProxyOrderSignal(m_UserID, pFutureOrder);
            }
        }

        private void btnSendProxyDuplexOrder_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo, strFutureNo2, strYM, strYM2, strStrike, strStrike2, strOrderType="";
            int nBS, nBS2;
            string strPrice;
            int nQty, nPriceFlag;
            int nCP, nCP2, nORDERType, nCOND, nDayTrade, nPreOrder;

            if (txtOptionNo1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOptionNo1.Text.Trim();

            if (txtOptionNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼2");
                return;
            }
            strFutureNo2 = txtOptionNo2.Text.Trim();

            if (txtProxyYM1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM1.Text.Trim();

            if (txtProxyYM2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月2");
                return;
            }
            strYM2 = txtProxyYM2.Text.Trim();

            if (txtProxyStrike1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrike = txtProxyStrike1.Text.Trim();

            if (txtProxyStrike2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價2");
                return;
            }
            strStrike2 = txtProxyStrike2.Text.Trim();

            if (boxProxyCP1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCP = boxProxyCP1.SelectedIndex;

            if (boxProxyCP2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權2");
                return;
            }
            nCP2 = boxProxyCP2.SelectedIndex;

            if (boxProxyBS1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS1.SelectedIndex;

            if (boxProxyBS2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            nBS2 = boxProxyBS2.SelectedIndex;

            if (boxOptionCOND2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxOptionCOND2.SelectedIndex;

            if (boxORDERType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxORDERType2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtOptionPrice2.Text.Trim(), out dPrice) == false && txtOptionPrice2.Text.Trim() != "M" && txtOptionPrice2.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtOptionPrice2.Text.Trim();

            if (int.TryParse(txtOptionQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxDayTrade2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否當沖");
                return;
            }
            nDayTrade = boxDayTrade2.SelectedIndex;

            if (boxPreOrder2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxPreOrder2.SelectedIndex;

            if (boxPriceFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxPriceFlag2.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.bstrStockNo2 = strFutureNo2;
            pFutureOrder.nQty = nQty;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.bstrSettleYM2 = strYM2;
            pFutureOrder.bstrStrike = strStrike;
            pFutureOrder.bstrStrike2 = strStrike2;
            pFutureOrder.nBuySell = nBS;
            pFutureOrder.nBuySell2 = nBS2;
            pFutureOrder.nCP = nCP;
            pFutureOrder.nCP2 = nCP2;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.nPriceFlag = nPriceFlag;
            pFutureOrder.nReserved = nPreOrder;
            pFutureOrder.bstrOrderType = strOrderType;

            if (OnDuplexProxyOrderSignal != null)
            {
                OnDuplexProxyOrderSignal(m_UserID, pFutureOrder);
            }
        }

        private void btnProxyOptionAlter_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            int nCOND;
            string strPrice = "";
            int nQty = 0;
            int nPreOrder;
            string strOrderNo, strOrderType="";
            string strSeqNo;
            int nORDERType;

            if (boxProxyCOND3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND3.SelectedIndex;

            if (boxProxyPreOrder3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder3.SelectedIndex;

            if (boxProxyORDERType3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = boxProxyORDERType3.SelectedIndex;

            if (nORDERType == 2)
            {
                double dPrice = 0.0;
                if (double.TryParse(txtProxyPrice3.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
                strPrice = txtProxyPrice3.Text.Trim();
            }

            if (nORDERType == 0 || nORDERType == 1)
            {
                if (int.TryParse(txtProxyQty3.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }
            if (txtProxyOrderNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyOrderNo3.Text.Trim();

            if (txtProxySeqNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxySeqNo3.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pOptionOrder = new FUTUREPROXYORDER();

            pOptionOrder.bstrFullAccount = m_UserFutureAccount;
            pOptionOrder.bstrPrice = strPrice;
            pOptionOrder.nQty = nQty;
            pOptionOrder.nTradeType = nCOND;
            pOptionOrder.bstrOrderType = strOrderType;
            pOptionOrder.nReserved = nPreOrder;
            pOptionOrder.bstrBookNo = strOrderNo;
            pOptionOrder.bstrSeqNo = strSeqNo;

            if (OnOptionProxyAlterSignal != null)
            {
                OnOptionProxyAlterSignal(m_UserID, pOptionOrder);
            }
        }
        */
        private void btnSendOverseaFutureProxyOrder_Click_1(object sender, EventArgs e)
        {
            if (m_UserOSAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtProxyTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo.Text.Trim();

            if (txtOFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo.Text.Trim();

            if (txtProxyYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth.Text.Trim();

            double dPrice = 0.0;

            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrder.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
            }
            strOrder = txtProxyOrder.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrderNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價分子請輸入數字");
                    return;
                }
            }
            strOrderNumerator = txtProxyOrderNumerator.Text.Trim();

            //[20231113]//委託價分母
            string strOrderDeno = "";
            strOrderDeno = txtProxyOrderDeno.Text.Trim();
            //[20231113]//委託價分母

            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTrigger.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字");
                    return;
                }
            }
            strTrigger = txtProxyTrigger.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTriggerNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價分子請輸入數字");
                    return;
                }
            }
            strTriggerNumerator = txtProxyTriggerNumerator.Text.Trim();


            if (int.TryParse(txtOFQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOFBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxOFBS.SelectedIndex;

            if (boxProxyNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose.SelectedIndex;

            if (boxProxyFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxProxyFlag.SelectedIndex;

            if (boxProxyPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod.SelectedIndex;

            if (boxProxySpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType.SelectedIndex;

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserOSAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrOrderDenominator = strOrderDeno;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrTrigger = strTrigger;
            pOSOrder.bstrTriggerNumerator = strTriggerNumerator;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.nQty = nQty;
            pOSOrder.sBuySell = (short)nBuySell;
            pOSOrder.sDayTrade = (short)nDayTrade;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;
            pOSOrder.bstrOrderDenominator = strOrderDeno;

            if (OnOverseaFutureProxyOrderSignal != null)
            {
                OnOverseaFutureProxyOrderSignal(m_UserID, pOSOrder);
            }
        }

        private void btnSendOverseaFutureSpreadProxyOrder_Click_1(object sender, EventArgs e)
        {
            if (m_UserOSAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth, strYearMonth2;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtProxyTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo.Text.Trim();

            if (txtOFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo.Text.Trim();

            if (txtProxyYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth.Text.Trim();

            double dPrice = 0.0;

            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrder.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
            }
            strOrder = txtProxyOrder.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrderNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價分子請輸入數字");
                    return;
                }
            }
            strOrderNumerator = txtProxyOrderNumerator.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTrigger.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字");
                    return;
                }
            }
            strTrigger = txtProxyTrigger.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTriggerNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價分子請輸入數字");
                    return;
                }
            }
            strTriggerNumerator = txtProxyTriggerNumerator.Text.Trim();


            if (int.TryParse(txtOFQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOFBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxOFBS.SelectedIndex;

            if (boxProxyNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose.SelectedIndex;

            if (boxProxyFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxProxyFlag.SelectedIndex;

            if (boxProxyPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod.SelectedIndex;

            if (boxProxySpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType.SelectedIndex;

            if (txtProxyYearMonth2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月(遠月)");
                return;
            }
            strYearMonth2 = txtProxyYearMonth2.Text.Trim();

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserOSAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrTrigger = strTrigger;
            pOSOrder.bstrTriggerNumerator = strTriggerNumerator;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.bstrYearMonth2 = strYearMonth2;
            pOSOrder.nQty = nQty;
            pOSOrder.sBuySell = (short)nBuySell;
            pOSOrder.sDayTrade = (short)nDayTrade;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;

            if (OnOverseaFutureProxyOrderSpreadSignal != null)
            {
                OnOverseaFutureProxyOrderSpreadSignal(m_UserID, pOSOrder);
            }
        }
        /*
        private void btnSendOverseaFutureProxyAlter_Click(object sender, EventArgs e)
        {
            if (m_UserOSAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo, strOrderNo;
            string strYearMonth, strYearMonth2;
            int nNewClose;
            int nTradeType;
            int nSpecialTradeType, nFunCode;
            string strOrder, strSeqNo;
            string strOrderNumerator, strOrderD, strOrderType;
            int nQty;

            if (txtProxyTradeNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo2.Text.Trim();

            if (txtOFutureNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo2.Text.Trim();

            if (txtProxyYearMonth3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth3.Text.Trim();
            strYearMonth2 = txtProxyYearMonth4.Text.Trim();

            double dPrice = 0.0;

            strOrder = txtProxyOrder2.Text.Trim();

            strOrderNumerator = txtProxyOrderNumerator2.Text.Trim();

            strOrderD = txtProxyPriceD.Text.Trim();

            int.TryParse(txtOFQty2.Text.Trim(), out nQty);

            if (boxProxyNewClose2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose2.SelectedIndex;


            if (boxProxyPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod2.SelectedIndex;

            if (boxProxySpecialTradeType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType2.SelectedIndex;

            if (boxFunCode.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託項目");
                return;
            }
            nFunCode = boxFunCode.SelectedIndex;
            switch (nFunCode)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            if (txtOFBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtOFBookNo.Text.Trim();

            if (txtSeqNo4.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtSeqNo4.Text.Trim();

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserOSAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrOrderDenominator = strOrderD;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.bstrYearMonth2 = strYearMonth2;
            pOSOrder.nQty = nQty;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;
            pOSOrder.bstrBookNo = strOrderNo;
            //pOSOrder.nAlterType = nFunCode;
            pOSOrder.bstrSeqNo = strSeqNo;

            //if (OnOverseaFutureProxyAlterSignal != null)
            {
            //    OnOverseaFutureProxyAlterSignal(m_UserID, pOSOrder);
            }
        }*/

        private void btnSendOverseaOptionProxyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserOSAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            int nCallPut;
            string strStrikePrice;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtOOTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtOOTradeNo.Text.Trim();

            if (txtOONo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOONo.Text.Trim();

            if (txtOOYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOOYM.Text.Trim();

            double dPrice = 0.0;
            if (double.TryParse(txtOOPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrder = txtOOPrice.Text.Trim();

            if (double.TryParse(txtOONumerator.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價分子請輸入數字");
                return;
            }
            strOrderNumerator = txtOONumerator.Text.Trim();
            string strOrderDeno = txtOODeno.Text.Trim();
            strTrigger = txtOOTrigger.Text.Trim();

            strTriggerNumerator = txtOOTriggerNumerator.Text.Trim();

            strStrikePrice = txtProxyStrikePrice.Text.Trim();

            if (int.TryParse(txtOOQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOOBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxOOBS.SelectedIndex;

            if (boxOONewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxOONewClose.SelectedIndex;

            if (boxOOFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxOOFlag.SelectedIndex;

            if (boxOOPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxOOPeriod.SelectedIndex;

            if (boxOOSpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxOOSpecialTradeType.SelectedIndex;

            if (boxProxyCallPut.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇CALL PUT");
                return;
            }
            nCallPut = boxProxyCallPut.SelectedIndex;

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserOSAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrTrigger = strTrigger;
            pOSOrder.bstrTriggerNumerator = strTriggerNumerator;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.bstrStrikePrice = strStrikePrice;
            pOSOrder.nQty = nQty;
            pOSOrder.sBuySell = (short)nBuySell;
            pOSOrder.sDayTrade = (short)nDayTrade;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;
            pOSOrder.sCallPut = (short)nCallPut;

            pOSOrder.bstrOrderDenominator = strOrderDeno;//[20231120]//

            if (OnOverseaOptionProxyOrderSignal != null)
            {
                OnOverseaOptionProxyOrderSignal(m_UserID, pOSOrder);
            }
        }

        /*private void btnSendOverseaOptionProxyAlter_Click(object sender, EventArgs e)
        {
            if (m_UserOSAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo, strOrderNo,strSeqNo;
            string strYearMonth, strStrikePrice;
            int nNewClose;
            int nTradeType;
            int nSpecialTradeType, nFunCode;
            string strOrder, strOrderType="";
            string strOrderNumerator, strOrderD;
            int nQty, nCallPut;

            if (txtOOTradeNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtOOTradeNo2.Text.Trim();

            if (txtOONo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOONo2.Text.Trim();

            if (txtOOYM2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOOYM2.Text.Trim();

            if (txtProxyStrikePrice2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrikePrice = txtProxyStrikePrice2.Text.Trim();

            double dPrice = 0.0;

            strOrder = txtOOPrice2.Text.Trim();

            strOrderNumerator = txtOONumerator2.Text.Trim();

            strOrderD = txtOOPriceD.Text.Trim();

            int.TryParse(txtOOQty2.Text.Trim(), out nQty);

            if (boxOONewClose2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxOONewClose2.SelectedIndex;


            if (boxOOPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxOOPeriod2.SelectedIndex;

            if (boxOOSpecialTradeType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxOOSpecialTradeType2.SelectedIndex;

            if (boxOOFunCode.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託項目");
                return;
            }
            nFunCode = boxOOFunCode.SelectedIndex;

            //switch (nFunCode)
           // {
          //      case 0: strOrderType = "0"; break;
           //     case 1: strOrderType = "1"; break;
         //       case 2: strOrderType = "2"; break;
         //       default: strOrderType = ""; break;
          //  }

            if (boxProxyCallPut2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCallPut = boxProxyCallPut2.SelectedIndex;

            if (txtOOBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtOOBookNo.Text.Trim();


            if (txtSeqNo5.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtSeqNo5.Text.Trim();

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserOSAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrOrderDenominator = strOrderD;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.nQty = nQty;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;
            pOSOrder.bstrBookNo = strOrderNo;
            //pOSOrder.nAlterType = nFunCode;
            pOSOrder.bstrStrikePrice = strStrikePrice;
            pOSOrder.sCallPut = (short)nCallPut;
            pOSOrder.bstrSeqNo = strSeqNo;

            //if (OnOverseaOptionProxyAlterSignal != null)
            {
            //    OnOverseaOptionProxyAlterSignal(m_UserID, pOSOrder);
            }
        }*/

        //[20231123]
        private void btnSendStockProxyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserStockAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            string strPrice;
            string strOrderType = "";
            int nQty;
            int nORDERType, nPriceType, nTimeInForce, nMarketType, nPriceMark, nPrime;

            if (txtProxyStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyStockNo.Text.Trim();


            if (ProxyOrderTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox.SelectedIndex + 1;

            if (ProxyPriceTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            nPriceType = ProxyPriceTypeBox.SelectedIndex + 1;

            if (ProxyTimeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託時效");
                return;
            }
            nTimeInForce = ProxyTimeBox.SelectedIndex;

            if (ProxyMarketBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nMarketType = ProxyMarketBox.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyStockPrice.Text.Trim();

            if (int.TryParse(txtStockQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (ProxyPriceMarkBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格旗標");
                return;
            }
            nPriceMark = ProxyPriceMarkBox.SelectedIndex;

            switch (nORDERType)
            {
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                case 3: strOrderType = "3"; break;
                case 4: strOrderType = "4"; break;
                case 5: strOrderType = "5"; break;
                case 6: strOrderType = "6"; break;
                case 7: strOrderType = "7"; break;
                default: strOrderType = ""; break;
            }

            if (ProxyPrime.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市/上櫃/興櫃");
                return;
            }
            nPrime = ProxyPrime.SelectedIndex;

            SKCOMLib.STOCKPROXYORDER pOrder = new STOCKPROXYORDER();

            pOrder.bstrFullAccount = m_UserStockAccount;
            pOrder.bstrStockNo = strStockNo;
            pOrder.bstrOrderType = strOrderType;
            pOrder.nSpecialTradeType = nPriceType;
            pOrder.nTradeType = nTimeInForce;
            pOrder.nPeriod = nMarketType;
            pOrder.bstrPrice = strPrice;
            pOrder.nQty = nQty;
            pOrder.nPriceMark = nPriceMark;
            pOrder.nPrime = nPrime;

            if (OnStockProxyOrderSignal != null)
            {
                OnStockProxyOrderSignal(m_UserID, pOrder);
            }
        }

        //[20231123]
        private void btnSendStockProxyAlter_Click(object sender, EventArgs e)
        {
            if (m_UserStockAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            string strPrice, strOrderNo, strSeqNo, strOrderType = "", strOrderType2;
            int nQty, nPrime;
            int nPriceType, nTimeInForce, nMarketType, nPriceMark, nORDERType, nORDERType2;

            if (txtProxyStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyStockNo2.Text.Trim();

            if (ProxyOrderTypeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox2.SelectedIndex;

            if (ProxyOrderTypeBoxAlter.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType2 = ProxyOrderTypeBoxAlter.SelectedIndex + 1;

            if (ProxyPriceTypeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            nPriceType = ProxyPriceTypeBox2.SelectedIndex + 1;

            if (ProxyTimeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託時效");
                return;
            }
            nTimeInForce = ProxyTimeBox2.SelectedIndex;

            if (ProxyMarketBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nMarketType = ProxyMarketBox2.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyStockPrice2.Text.Trim();

            if (int.TryParse(txtStockQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (ProxyPriceMarkBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格旗標");
                return;
            }
            nPriceMark = ProxyPriceMarkBox2.SelectedIndex;

            if (txtStockBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtStockBookNo.Text.Trim();

            if (txtStockSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtStockSeqNo.Text.Trim();

            if (ProxyPrimeAlter.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市/上櫃/興櫃");
                return;
            }
            nPrime = ProxyPrimeAlter.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            switch (nORDERType2)
            {
                case 1: strOrderType2 = "1"; break;
                case 2: strOrderType2 = "2"; break;
                case 3: strOrderType2 = "3"; break;
                case 4: strOrderType2 = "4"; break;
                case 5: strOrderType2 = "5"; break;
                case 6: strOrderType2 = "6"; break;
                case 7: strOrderType2 = "7"; break;
                default: strOrderType2 = ""; break;
            }

            SKCOMLib.STOCKPROXYORDER pOrder = new STOCKPROXYORDER();

            pOrder.bstrFullAccount = m_UserStockAccount;
            pOrder.bstrStockNo = strStockNo;
            pOrder.nSpecialTradeType = nPriceType;
            pOrder.nTradeType = nTimeInForce;
            pOrder.nPeriod = nMarketType;
            pOrder.bstrPrice = strPrice;
            pOrder.nQty = nQty;
            pOrder.nPriceMark = nPriceMark;
            pOrder.bstrBookNo = strOrderNo;
            pOrder.bstrSeqNo = strSeqNo;
            pOrder.bstrOrderType = strOrderType;
            pOrder.bstrOrderType2 = strOrderType2;
            pOrder.nPrime = nPrime;

            if (OnStockProxyAlterSignal != null)
            {
                OnStockProxyAlterSignal(m_UserID, pOrder);
            }
        }

        //[20231124]
        private void btnSendProxyFutureOrderCLR_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            string strYM;
            int nBS;
            int nCOND;
            int nDayTrade;
            string strPrice;
            int nQty;
            int nPriceFlag;
            int nORDERType;
            int nPreOrder;
            string strOrderType;

            if (txtFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtFutureNo.Text.Trim();

            if (txtProxyYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM.Text.Trim();

            if (boxProxyBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS.SelectedIndex;

            if (boxProxyCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND.SelectedIndex;

            if (boxDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxDayTrade.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtFuturePrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtFuturePrice.Text.Trim();

            if (int.TryParse(txtFutureQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxProxyPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxProxyPriceFlag.SelectedIndex;

            if (boxProxyORDERType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxProxyORDERType.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            if (boxProxyPreOrder.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder.SelectedIndex;

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.nBuySell = nBS;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.bstrOrderType = strOrderType;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.nPriceFlag = nPriceFlag;
            pFutureOrder.nReserved = nPreOrder;

            if (OnFutureProxyOrderSignal != null)
            {
                OnFutureProxyOrderSignal(m_UserID, pFutureOrder);
            }
        }

        //[20231127]
        private void btnSendForeignStockProxyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserForeignStockAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strStockNo;
            string strPrice;
            string strCurrency1;
            string strCurrency2;
            string strCurrency3;
            string strExchangeNo = "";
            int nBidAsk;
            int nAccountType;
            int nQty = 0;
            int nTradeType = 0;
            string strProxyQty = "";
            if (boxProxyAccountType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇專戶別");
                return;
            }
            nAccountType = boxProxyAccountType.SelectedIndex + 1;

            if (boxProxyExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxProxyExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxProxyExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxProxyExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxProxyExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxProxyExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxProxyExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxProxyExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }

            if (boxProxyBidAsk.SelectedIndex == 0 && boxProxyCurrency1.SelectedIndex < 0)
            {
                MessageBox.Show("買單請至少選擇扣款幣別 1");
                return;
            }
            strCurrency1 = boxProxyCurrency1.Text;
            strCurrency2 = boxProxyCurrency2.Text;
            strCurrency3 = boxProxyCurrency3.Text;

            if (txtProxyOStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyOStockNo.Text.Trim();

            if (boxProxyBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxProxyBidAsk.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtProxyPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyPrice.Text.Trim();

            if (nBidAsk == 1 && strExchangeNo == "US")
            {
                if (boxProxyTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇庫存別");
                    return;
                }
                nTradeType = boxProxyTradeType.SelectedIndex + 1;
            }
            else
            {
                if (boxProxyTradeType.SelectedIndex < 0)
                    nTradeType = 0;
                else
                {
                    nTradeType = boxProxyTradeType.SelectedIndex + 1;
                }
            }

            double dQty = 0.0;
            if (nBidAsk == 1 && strExchangeNo == "US" && nTradeType == 2)
            {
                if (double.TryParse(txtProxyQty.Text.Trim(), out dQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }
            else
            {
                if (int.TryParse(txtProxyQty.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入整數數字");
                    return;
                }
            }
            strProxyQty = txtProxyQty.Text.Trim();

            OSSTOCKPROXYORDER pForeignOrder = new OSSTOCKPROXYORDER();

            pForeignOrder.bstrFullAccount = m_UserForeignStockAccount;
            pForeignOrder.bstrStockNo = strStockNo;
            pForeignOrder.bstrPrice = strPrice;
            pForeignOrder.bstrExchangeNo = strExchangeNo;
            pForeignOrder.bstrCurrency1 = strCurrency1;
            pForeignOrder.bstrCurrency2 = strCurrency2;
            pForeignOrder.bstrCurrency3 = strCurrency3;
            pForeignOrder.nOrderType = nBidAsk + 1;
            pForeignOrder.nAccountType = nAccountType;
            pForeignOrder.bstrProxyQty = strProxyQty;
            pForeignOrder.nTradeType = nTradeType;

            if (OnForeignProxyOrderSignal != null)
            {
                OnForeignProxyOrderSignal(m_UserID, pForeignOrder);
            }
        }

        //[20231127]
        private void btnProxyCancelForeignOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (m_UserForeignStockAccount == "")
            {
                MessageBox.Show("請選擇複委託帳號");
                return;
            }

            string strExchangeNo = "";
            string strSeqNo = "";
            string strOrderNo = "";
            string strStockNo = "";
            if (boxProxyCancelExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxProxyCancelExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }

            if (txtProxyOStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyOStockNo2.Text.Trim();

            if (txtProxyCancelSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxyCancelSeqNo.Text.Trim();

            if (txtProxyCancelBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyCancelBookNo.Text.Trim();

            OSSTOCKPROXYORDER pForeignOrder = new OSSTOCKPROXYORDER();
            pForeignOrder.bstrFullAccount = m_UserForeignStockAccount;
            pForeignOrder.bstrExchangeNo = strExchangeNo;
            pForeignOrder.bstrBookNo = strOrderNo;
            pForeignOrder.bstrSeqNo = strSeqNo;
            pForeignOrder.bstrStockNo = strStockNo;
            if (OnForeignProxyCancelSignal != null)
            {
                OnForeignProxyCancelSignal(m_UserID, pForeignOrder);
            }
        }

        //[20231128]
        private void btnProxyFutureAlter_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            int nCOND;
            string strPrice = "";
            int nQty = 0;
            int nPreOrder;
            string strOrderNo;
            string strSeqNo, strOrderType = "";
            int nORDERType;

            if (boxProxyCOND2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND2.SelectedIndex;


            if (boxProxyPreOrder2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder2.SelectedIndex;

            if (boxProxyORDERType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = boxProxyORDERType2.SelectedIndex;
            if (nORDERType == 2)
            {
                double dPrice = 0.0;
                if (double.TryParse(txtProxyPrice2.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
                strPrice = txtProxyPrice2.Text.Trim();
            }

            if (nORDERType == 0 || nORDERType == 1)
            {
                if (int.TryParse(txtProxyQty2.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }

            if (txtProxyOrderNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyOrderNo.Text.Trim();

            if (txtProxySeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxySeqNo.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.nQty = nQty;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.bstrOrderType = strOrderType;
            pFutureOrder.nReserved = nPreOrder;
            pFutureOrder.bstrBookNo = strOrderNo;
            pFutureOrder.bstrSeqNo = strSeqNo;

            if (OnFutureProxyAlterSignal != null)
            {
                OnFutureProxyAlterSignal(m_UserID, pFutureOrder);
            }
        }

        //[20231128]
        private void btnSendProxyOptionOrder_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo, strYM, strStrike, strOrderType = "";
            int nBS;
            string strPrice;
            int nQty, nPriceFlag;
            int nCP, nORDERType, nCOND, nDayTrade, nPreOrder;

            if (txtOptionNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOptionNo.Text.Trim();

            if (txtOptionYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtOptionYM.Text.Trim();

            if (txtProxyStrike.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrike = txtProxyStrike.Text.Trim();

            if (boxProxyCP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCP = boxProxyCP.SelectedIndex;

            if (boxOptionBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxOptionBS.SelectedIndex;

            if (boxOptionCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxOptionCOND.SelectedIndex;

            if (boxORDERType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxORDERType.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtOptionPrice.Text.Trim(), out dPrice) == false && txtOptionPrice.Text.Trim() != "M" && txtOptionPrice.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtOptionPrice.Text.Trim();

            if (int.TryParse(txtOptionQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOptionDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否當沖");
                return;
            }
            nDayTrade = boxOptionDayTrade.SelectedIndex;

            if (boxPreOrder.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxPreOrder.SelectedIndex;

            if (boxPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxPriceFlag.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.bstrStrike = strStrike;
            pFutureOrder.nBuySell = nBS;
            pFutureOrder.nCP = nCP;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.nPriceFlag = nPriceFlag;
            pFutureOrder.nReserved = nPreOrder;
            pFutureOrder.bstrOrderType = strOrderType;

            if (OnOptionProxyOrderSignal != null)
            {
                OnOptionProxyOrderSignal(m_UserID, pFutureOrder);
            }
        }

        //[20231128]
        private void btnProxyOptionAlter_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            int nCOND;
            string strPrice = "";
            int nQty = 0;
            int nPreOrder;
            string strOrderNo, strOrderType = "";
            string strSeqNo;
            int nORDERType;

            if (boxProxyCOND3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND3.SelectedIndex;

            if (boxProxyPreOrder3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder3.SelectedIndex;

            if (boxProxyORDERType3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = boxProxyORDERType3.SelectedIndex;

            if (nORDERType == 2)
            {
                double dPrice = 0.0;
                if (double.TryParse(txtProxyPrice3.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
                strPrice = txtProxyPrice3.Text.Trim();
            }

            if (nORDERType == 0 || nORDERType == 1)
            {
                if (int.TryParse(txtProxyQty3.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }
            if (txtProxyOrderNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyOrderNo3.Text.Trim();

            if (txtProxySeqNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxySeqNo3.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pOptionOrder = new FUTUREPROXYORDER();

            pOptionOrder.bstrFullAccount = m_UserFutureAccount;
            pOptionOrder.bstrPrice = strPrice;
            pOptionOrder.nQty = nQty;
            pOptionOrder.nTradeType = nCOND;
            pOptionOrder.bstrOrderType = strOrderType;
            pOptionOrder.nReserved = nPreOrder;
            pOptionOrder.bstrBookNo = strOrderNo;
            pOptionOrder.bstrSeqNo = strSeqNo;

            if (OnOptionProxyAlterSignal != null)
            {
                OnOptionProxyAlterSignal(m_UserID, pOptionOrder);
            }
        }

        //[20231128]
        private void btnSendProxyDuplexOrder_Click(object sender, EventArgs e)
        {
            if (m_UserFutureAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo, strFutureNo2, strYM, strYM2, strStrike, strStrike2, strOrderType = "";
            int nBS, nBS2;
            string strPrice;
            int nQty, nPriceFlag;
            int nCP, nCP2, nORDERType, nCOND, nDayTrade, nPreOrder;

            if (txtOptionNo1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOptionNo1.Text.Trim();

            if (txtOptionNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼2");
                return;
            }
            strFutureNo2 = txtOptionNo2.Text.Trim();

            if (txtProxyYM1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM1.Text.Trim();

            if (txtProxyYM2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月2");
                return;
            }
            strYM2 = txtProxyYM2.Text.Trim();

            if (txtProxyStrike1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrike = txtProxyStrike1.Text.Trim();

            if (txtProxyStrike2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價2");
                return;
            }
            strStrike2 = txtProxyStrike2.Text.Trim();

            if (boxProxyCP1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCP = boxProxyCP1.SelectedIndex;

            if (boxProxyCP2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權2");
                return;
            }
            nCP2 = boxProxyCP2.SelectedIndex;

            if (boxProxyBS1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS1.SelectedIndex;

            if (boxProxyBS2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            nBS2 = boxProxyBS2.SelectedIndex;

            if (boxOptionCOND2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxOptionCOND2.SelectedIndex;

            if (boxORDERType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxORDERType2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtOptionPrice2.Text.Trim(), out dPrice) == false && txtOptionPrice2.Text.Trim() != "M" && txtOptionPrice2.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtOptionPrice2.Text.Trim();

            if (int.TryParse(txtOptionQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxDayTrade2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否當沖");
                return;
            }
            nDayTrade = boxDayTrade2.SelectedIndex;

            if (boxPreOrder2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxPreOrder2.SelectedIndex;

            if (boxPriceFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxPriceFlag2.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            FUTUREPROXYORDER pFutureOrder = new FUTUREPROXYORDER();

            pFutureOrder.bstrFullAccount = m_UserFutureAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.bstrStockNo2 = strFutureNo2;
            pFutureOrder.nQty = nQty;
            pFutureOrder.bstrSettleYM = strYM;
            pFutureOrder.bstrSettleYM2 = strYM2;
            pFutureOrder.bstrStrike = strStrike;
            pFutureOrder.bstrStrike2 = strStrike2;
            pFutureOrder.nBuySell = nBS;
            pFutureOrder.nBuySell2 = nBS2;
            pFutureOrder.nCP = nCP;
            pFutureOrder.nCP2 = nCP2;
            pFutureOrder.nTradeType = nCOND;
            pFutureOrder.nDayTrade = nDayTrade;
            pFutureOrder.nPriceFlag = nPriceFlag;
            pFutureOrder.nReserved = nPreOrder;
            pFutureOrder.bstrOrderType = strOrderType;

            if (OnDuplexProxyOrderSignal != null)
            {
                OnDuplexProxyOrderSignal(m_UserID, pFutureOrder);
            }
        }

        //[20231129]
        private void btnSendOverseaFutureProxyAlter_Click(object sender, EventArgs e)
        {
            if (m_UserOSAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo, strOrderNo;
            string strYearMonth, strYearMonth2;
            int nNewClose;
            int nTradeType;
            int nSpecialTradeType, nFunCode;
            string strOrder, strSeqNo;
            string strOrderNumerator, strOrderD;

            int nQty;

            if (txtProxyTradeNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo2.Text.Trim();

            if (txtOFutureNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo2.Text.Trim();

            if (txtProxyYearMonth3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth3.Text.Trim();
            strYearMonth2 = txtProxyYearMonth4.Text.Trim();

            double dPrice = 0.0;

            strOrder = txtProxyOrder2.Text.Trim();

            strOrderNumerator = txtProxyOrderNumerator2.Text.Trim();

            strOrderD = txtProxyPriceD.Text.Trim();

            int.TryParse(txtOFQty2.Text.Trim(), out nQty);

            if (boxProxyNewClose2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose2.SelectedIndex;


            if (boxProxyPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod2.SelectedIndex;

            if (boxProxySpecialTradeType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType2.SelectedIndex;

            if (boxFunCode.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇異動功能(刪單/減量/改價)");
                return;
            }
            nFunCode = boxFunCode.SelectedIndex;



            if (txtOFBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtOFBookNo.Text.Trim();

            if (txtSeqNo4.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtSeqNo4.Text.Trim();

            int nSpread;
            nSpread = BoxAlterSpread.SelectedIndex;

            if (nSpread == 2)
            {
                if (txt_Strike_proxy.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入履約價");
                    return;
                }
            }
            string strStrikePrice = txt_Strike_proxy.Text.Trim();

            if (nSpread == 2)
            {
                if (boxCallPutAlter.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣權");
                    return;
                }
            }
            int nCallPut = boxCallPutAlter.SelectedIndex;






            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserOSAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrOrderDenominator = strOrderD;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.bstrYearMonth2 = strYearMonth2;
            pOSOrder.nQty = nQty;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;
            pOSOrder.bstrBookNo = strOrderNo;
            pOSOrder.nAlterType = nFunCode;
            pOSOrder.bstrSeqNo = strSeqNo;
            pOSOrder.nSpreadFlag = nSpread;

            pOSOrder.bstrStrikePrice = strStrikePrice;
            pOSOrder.sCallPut = (short)nCallPut;

            if (OnOverseaFutureProxyAlterSignal != null)
            {
                OnOverseaFutureProxyAlterSignal(m_UserID, pOSOrder);
            }
        }

        private void label147_Click(object sender, EventArgs e)
        {

        }
        //20250115
        private void btnSendStockProxyAlterPre_Click(object sender, EventArgs e)
        {
            if (m_UserStockAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strPrice, strOrderNo, strSeqNo, strOrderType = "", strStockNo;
            int nQty;
            int nORDERType;

            strStockNo = txtProxyStockNo2Pre.Text.Trim();

            int nPriceMark = ProxyPriceMarkBox2Pre.SelectedIndex;

            if (ProxyOrderTypeBox2Pre.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox2Pre.SelectedIndex;

            /*double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice2Pre.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }*/
            strPrice = txtProxyStockPrice2Pre.Text.Trim();

            if (int.TryParse(txtStockQty2Pre.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }
            nQty = int.Parse(txtStockQty2Pre.Text.Trim());
            /*if (txtStockBookNoPre.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }*/
            strOrderNo = txtStockBookNoPre.Text.Trim();

            /*if (txtStockSeqNoPre.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }*/
            strSeqNo = txtStockSeqNoPre.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            SKCOMLib.STOCKPROXYORDER pOrder = new STOCKPROXYORDER();

            pOrder.bstrFullAccount = m_UserStockAccount;
            pOrder.bstrOrderType = strOrderType;

            pOrder.bstrPrice = strPrice; // 價格
            pOrder.nPriceMark = nPriceMark;//價格旗標(改價可用) 0:一般定價 1:前日收盤價 2:漲停 3:跌停

            pOrder.nQty = nQty; //股數
            pOrder.bstrBookNo = strOrderNo; // 書號
            pOrder.bstrSeqNo = strSeqNo; // 序號

            pOrder.bstrStockNo = strStockNo; // 股票代號
            pOrder.bstrPrice_forD = txtPriceForCancel.Text.Trim(); // 原委託單價格(用於商品代號刪單情境)

            pOrder.bstrBuySell_forD = txtBuySellBoxForCancel.Text.Trim(); // 原委託單買賣別(用於商品代號刪單情境) 0是BUY ; 1 是 SELL

            if (OnStockProxyPreAlterSignal != null)
            {
                OnStockProxyPreAlterSignal(m_UserID, pOrder);
            }
        }

        private void btnSendOverseaFutureSpreadProxyOrder2_Click(object sender, EventArgs e)
        {

            {
                if (m_UserOSAccount == "")
                {
                    MessageBox.Show("請選擇海期帳號");
                    return;
                }

                string strTradeName;
                string strStockNo;
                string strYearMonth, strYearMonth2;
                int nBuySell;
                int nNewClose;
                int nDayTrade;
                int nTradeType;
                int nSpecialTradeType;
                string strOrder;
                string strOrderNumerator;
                string strTrigger;
                string strTriggerNumerator;
                int nQty;

                if (txtProxyTradeNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入交易所代號");
                    return;
                }
                strTradeName = txtProxyTradeNo.Text.Trim();

                if (txtOFutureNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入商品代碼");
                    return;
                }
                strStockNo = txtOFutureNo.Text.Trim();

                if (txtProxyYearMonth.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入年月");
                    return;
                }
                strYearMonth = txtProxyYearMonth.Text.Trim();

                double dPrice = 0.0;

                if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
                {
                    if (double.TryParse(txtProxyOrder.Text.Trim(), out dPrice) == false)
                    {
                        MessageBox.Show("委託價請輸入數字");
                        return;
                    }
                }
                strOrder = txtProxyOrder.Text.Trim();


                if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
                {
                    if (double.TryParse(txtProxyOrderNumerator.Text.Trim(), out dPrice) == false)
                    {
                        MessageBox.Show("委託價分子請輸入數字");
                        return;
                    }
                }
                strOrderNumerator = txtProxyOrderNumerator.Text.Trim();


                if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
                {
                    if (double.TryParse(txtProxyTrigger.Text.Trim(), out dPrice) == false)
                    {
                        MessageBox.Show("觸發價請輸入數字");
                        return;
                    }
                }
                strTrigger = txtProxyTrigger.Text.Trim();


                if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
                {
                    if (double.TryParse(txtProxyTriggerNumerator.Text.Trim(), out dPrice) == false)
                    {
                        MessageBox.Show("觸發價分子請輸入數字");
                        return;
                    }
                }
                strTriggerNumerator = txtProxyTriggerNumerator.Text.Trim();


                if (int.TryParse(txtOFQty.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }

                if (boxOFBS.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別");
                    return;
                }
                nBuySell = boxOFBS.SelectedIndex;

                if (boxProxyNewClose.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇倉別");
                    return;
                }
                nNewClose = boxProxyNewClose.SelectedIndex;

                if (boxProxyFlag.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇當沖與否");
                    return;
                }
                nDayTrade = boxProxyFlag.SelectedIndex;

                if (boxProxyPeriod.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託條件");
                    return;
                }
                nTradeType = boxProxyPeriod.SelectedIndex;

                if (boxProxySpecialTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託類型");
                    return;
                }
                nSpecialTradeType = boxProxySpecialTradeType.SelectedIndex;

                if (txtProxyYearMonth2.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入年月(遠月)");
                    return;
                }
                strYearMonth2 = txtProxyYearMonth2.Text.Trim();

                OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

                pOSOrder.bstrFullAccount = m_UserOSAccount;
                pOSOrder.bstrExchangeNo = strTradeName;
                pOSOrder.bstrOrder = strOrder;
                pOSOrder.bstrOrderNumerator = strOrderNumerator;
                pOSOrder.bstrStockNo = strStockNo;
                pOSOrder.bstrTrigger = strTrigger;
                pOSOrder.bstrTriggerNumerator = strTriggerNumerator;
                pOSOrder.bstrYearMonth = strYearMonth;
                pOSOrder.bstrYearMonth2 = strYearMonth2;
                pOSOrder.nQty = nQty;
                pOSOrder.sBuySell = (short)nBuySell;
                pOSOrder.sDayTrade = (short)nDayTrade;
                pOSOrder.sNewClose = (short)nNewClose;
                pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
                pOSOrder.sTradeType = (short)nTradeType;

                if (OnOverseaFutureProxyOrderSpread2Signal != null)
                {
                    OnOverseaFutureProxyOrderSpread2Signal(m_UserID, pOSOrder);
                }
            }
        }
    }
}
