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
    public partial class TFTOMIT : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        private int m_nCode;
        public string m_strMessage;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event OrderHandler OnFutureMITOrderSignal;

        public delegate void OrderV1Handler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event OrderV1Handler OnFutureMITOrderV1Signal;

        public delegate void OptionOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event OptionOrderHandler OnOptionMITOrderSignal;

        public delegate void FutureABOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder); //[-20231219-Add]
        public event FutureABOrderHandler OnFutureABOrderSignal;

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

        public TFTOMIT()
        {
            InitializeComponent();
            MITDir1.SelectedIndex =1;
                MITDir2.SelectedIndex = 1;
                boxBidAsk.SelectedIndex = 0;
                boxBidAsk3.SelectedIndex = 0;
                boxFlag.SelectedIndex = 0;
                boxNewCloseOP.SelectedIndex = 0;
                boxPeriod.SelectedIndex = 1;
                boxPeriod3.SelectedIndex = 1;
                boxNewCloseMIT.SelectedIndex = 0;
        }

        private void btnSendFutureMITOrder_Click(object sender, EventArgs e)
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
            string strDealPrice;
            string strTigger;
            int nQty;
            int nNewClose;            

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

            if (boxNewCloseMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewCloseMIT.SelectedIndex;

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
            if (double.TryParse(txtDealPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = txtDealPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTrigger.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTigger = txtTrigger.Text.Trim();



            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;
            pFutureOrder.bstrDealPrice = strDealPrice;

            pFutureOrder.bstrMovingPoint = "";
            pFutureOrder.bstrPrice = "";

            if (OnFutureMITOrderSignal != null)
            {
                OnFutureMITOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendOptionMITOrder_Click(object sender, EventArgs e)
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
            string strDealPrice;
            string strTigger;
            int nQty;
            int nNewClose;

            if (txtOPStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOPStockNo.Text.Trim();

            if (boxBidAsk3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }

            if (boxNewCloseOP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewCloseOP.SelectedIndex;

            nBidAsk = boxBidAsk3.SelectedIndex;

            if (boxPeriod3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod3.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtDealPrice2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = txtDealPrice2.Text.Trim();

            if (int.TryParse(txtQty3.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTrigger3.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTigger = txtTrigger3.Text.Trim();



            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;
            pFutureOrder.bstrDealPrice = strDealPrice;

            pFutureOrder.bstrMovingPoint = "";
            pFutureOrder.bstrPrice = "";

            if (OnOptionMITOrderSignal != null)
            {
                OnOptionMITOrderSignal(m_UserID, false, pFutureOrder);
            }
        
        }

        private void btnSendOptionMITOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            
            string strDealPrice;
            string strTrigger;
            int nQty;
            int nNewClose;

            if (txtOPStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOPStockNo.Text.Trim();

            if (boxBidAsk3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }

            if (boxNewCloseOP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewCloseOP.SelectedIndex;

            nBidAsk = boxBidAsk3.SelectedIndex;

            if (boxPeriod3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            
            if (boxPeriod3.SelectedIndex == 0)
                nPeriod = boxPeriod3.SelectedIndex;
            else
                nPeriod = boxPeriod3.SelectedIndex + 2;

            double dPrice = 0.0;
            if (double.TryParse(txtDealPrice2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = txtDealPrice2.Text.Trim();

            if (int.TryParse(txtQty3.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTrigger3.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = txtTrigger3.Text.Trim();

            string strOrderPrice = MITTOOrderPrice.Text.Trim();
            
            string strCID = TOMITCID.Text.Trim();
            
            string strYM = TOYM.Text.Trim();

            string strStrike = TOStrike.Text.Trim();

            int nPriceType = MITTOPriceType.SelectedIndex;

            int nDir = MITDir2.SelectedIndex;

            int nCP = Box_CP.SelectedIndex;
            bool Async = Async2.SelectedIndex==0? false :true;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;

            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;

            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.bstrDealPrice = strDealPrice;

            pFutureOrder.nOrderPriceType = nPriceType;
            pFutureOrder.nTriggerDirection = nDir;
            pFutureOrder.nCallPut = nCP;
            pFutureOrder.bstrPrice = strOrderPrice;
            pFutureOrder.bstrCIDTandem = strCID;
            pFutureOrder.bstrSettlementMonth = strYM;
            pFutureOrder.bstrStrikePrice = strStrike;
            pFutureOrder.bstrOrderSign = "+";

            if (OnOptionMITOrderSignal != null)
            {
                OnOptionMITOrderSignal(m_UserID, Async, pFutureOrder);
            }
        }

        private void btnSendFutureMITOrderAsync_Click(object sender, EventArgs e)
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
            string strDealPrice;
            string strTrigger;
            int nQty;
            int nNewClose;

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

            if (boxNewCloseMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewCloseMIT.SelectedIndex;

            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            if (boxPeriod.SelectedIndex == 0)
                nPeriod = boxPeriod.SelectedIndex;
            else
                nPeriod = boxPeriod.SelectedIndex + 2;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtDealPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = txtDealPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTrigger.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = txtTrigger.Text.Trim();

            if (MITPriceType1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(2或3)");
                return;
            }
            int nPriceType = MITPriceType1.SelectedIndex + 1;

            if (MITDir1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸價方向1/2");
                return;
            }
            int nDir = MITDir1.SelectedIndex;


            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;

            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.nOrderPriceType = nPriceType;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.bstrDealPrice = strDealPrice;
            pFutureOrder.nTriggerDirection =nDir;
            pFutureOrder.bstrCIDTandem = MITCID.Text.Trim();
            pFutureOrder.bstrSettlementMonth = TFYM.Text.Trim();
            pFutureOrder.bstrMovingPoint = "";
            pFutureOrder.bstrPrice = MITOrderPrice1.Text.Trim();
            pFutureOrder.bstrOrderSign = "+";

            int nVer = VerBox.SelectedIndex;
            if (nVer == 0)
            {
                if (OnFutureMITOrderV1Signal != null)
                {
                    OnFutureMITOrderV1Signal(m_UserID, false, pFutureOrder);
                }
            }
            else
            {
                if (OnFutureMITOrderSignal != null)
                {
                    OnFutureMITOrderSignal(m_UserID, false, pFutureOrder);
                }
            }
        }

        private void boxPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxPeriod.SelectedIndex == 0)
            {

                MITPriceType1.SelectedIndex = 1;
                MITPriceType1.Enabled = false;
            }
            else
                MITPriceType1.Enabled = true;
        }

        private void btnSendFutureABOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strStockNo = "", strExchangeNo = "", strTrigger = "", strFutureNo = "", strStrikePrice = "", strSettlementMonth = "", strSettlementMonth2 = "" , strOrderPrice = "", strDealPrice = "";
            int nMarketNo = 0, nPrime = 0, nDir = 0, nIsOption = 0, nIsSpread = 0, nIsDayTrade = 0, nBidAsk2 = 0, nPriceType = 0;
            int nBidAsk = 0, nPeriod = 0, nFlag = 0, nQty = 0, nNewClose = 0;
            

            if (StockNo1AB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入A商品代碼");
                return;
            }
            strStockNo = StockNo1AB.Text.Trim();

            if (box_MarketNoAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇市場編號");
                return;
            }
            nMarketNo = (box_MarketNoAB.SelectedIndex + 1);

            if (ExchangeNoAB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代碼");
                return;
            }
            strExchangeNo = ExchangeNoAB.Text.Trim();

            if (box_PrimeAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市櫃");
                return;
            }
            nPrime = box_PrimeAB.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(DealPriceAB.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = DealPriceAB.Text.Trim();

            if (box_DirAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸價方向");
                return;
            }
            nDir = box_DirAB.SelectedIndex;

            double dTrigger = 0.0;
            if (double.TryParse(TriggerAB.Text.Trim(), out dTrigger) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = TriggerAB.Text.Trim();

            if (StockNo2AB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入B商品代碼");
                return;
            }
            strFutureNo = StockNo2AB.Text.Trim();

            if (box_IsOptionAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為選擇權");
                return;
            }
            nIsOption = box_IsOptionAB.SelectedIndex;

            double dSrikePrice = 0.0;
            if (double.TryParse(StrikePriceAB.Text.Trim(), out dSrikePrice) == false)
            {
                MessageBox.Show("履約價請輸入數字");
                return;
            }
            strStrikePrice = StrikePriceAB.Text.Trim();

            if (box_IsSpreadAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為價差商品");
                return;
            }
            nIsSpread = box_IsSpreadAB.SelectedIndex;

            if (SettlementMonth1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品契約月份");
                return;
            }
            strSettlementMonth = SettlementMonth1.Text.Trim();

            double dSettlementMonth2 = 0.0;
            if (double.TryParse(SettlementMonth2.Text.Trim(), out dSettlementMonth2) == false)
            {
                MessageBox.Show("商品契約月份2請輸入數字");
                return;
            }
            strSettlementMonth2 = SettlementMonth2.Text.Trim();

            if (box_DayTradeAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為當沖");
                return;
            }
            nIsDayTrade = box_DayTradeAB.SelectedIndex;

            if (box_NewCloseAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = box_NewCloseAB.SelectedIndex;

            if (box_PeriodAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            if (box_PeriodAB.SelectedIndex == 0)
                nPeriod = box_PeriodAB.SelectedIndex;
            else
                nPeriod = box_PeriodAB.SelectedIndex + 2;

            if (box_BidAskAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = box_BidAskAB.SelectedIndex;

            if (box_BidAsk2AB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            nBidAsk2 = box_BidAsk2AB.SelectedIndex;

            if (int.TryParse(QtyAB.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (box_PriceTypeAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別");
                return;
            }
            nPriceType = (box_PriceTypeAB.SelectedIndex + 2);

            double dOrderPricee = 0.0;
            if (double.TryParse(OrderPriceAB.Text.Trim(), out dOrderPricee) == false)
            {
                MessageBox.Show("履約價請輸入數字");
                return;
            }
            strOrderPrice = OrderPriceAB.Text.Trim();

            if (box_PreOrderAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為預約單");
                return;
            }
            nFlag = box_PreOrderAB.SelectedIndex;

            


            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrStockNo2 = strStockNo;
            pFutureOrder.nMarketNo = nMarketNo;
            pFutureOrder.bstrCIDTandem = strExchangeNo;
            pFutureOrder.nTimeFlag = nPrime;
            pFutureOrder.bstrDealPrice = strDealPrice;
            pFutureOrder.nTriggerDirection = nDir;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nCallPut = nIsOption;
            pFutureOrder.bstrStrikePrice = strStrikePrice;
            pFutureOrder.nFlag = nIsSpread;
            pFutureOrder.bstrSettlementMonth = strSettlementMonth;
            pFutureOrder.bstrSettlementMonth2 = strSettlementMonth2;
            pFutureOrder.sDayTrade = (short)nIsDayTrade;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sBuySell2 = (short)nBidAsk2;
            pFutureOrder.nQty = nQty;
            pFutureOrder.nOrderPriceType = nPriceType;
            pFutureOrder.bstrPrice = strOrderPrice;
            pFutureOrder.sReserved = (short)nFlag;

            
            if (OnFutureABOrderSignal != null)
            {
                OnFutureABOrderSignal(m_UserID, false, pFutureOrder);
            }
            
        }
    }
}
