using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;

namespace SKOrderTester
{
    public partial class OverseaStrategyControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        private int m_nCode;
        public string m_strMessage;


        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void OverseaFutureOCOOrderHandler(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pOrder);
        public event OverseaFutureOCOOrderHandler OnOverseaFutureOCOOrderSignal;

        public delegate void CancelOFStrategyOrderHandler(string strLogInID, CANCELSTRATEGYORDER pOrder);
        public event CancelOFStrategyOrderHandler OnCancelOFStrategyOrderSignal;

        public delegate void OFSmartStrategyReportHandler(string strLogInID, string strAccount, string strMarketType, int nReportStatus, string strKind, string strDate);
        public event OFSmartStrategyReportHandler OnOFSmartStrategyReportSignal;

        public delegate void OverseaFutureABOrderHandler(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pOrder);
        public event OverseaFutureABOrderHandler OnOverseaFutureABOrderSignal;


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
        public OverseaStrategyControl()
        {
            InitializeComponent();
        }

        private void btnSendOverseaFutureOCOOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strFutureNo = "", strExchangeNo = "", strSettlementMonth = "", strUpPriceNum = "", strUpPriceDen = "", strDownPriceNum = "", strDownPriceDen = "";
            string strUpTriggerNum = "", strUpTriggerDen = "", strDownTriggerNum = "", strDownTriggerDen = "";

            int nFlag = 0, nReservedFlag = 0, nPriceType = 0;
            int nUpBidAsk;
            int nPeriod;
            string strUpPrice;
            string strUpTrigger;
            int nDownBidAsk = 0;
            string strDownPrice;
            string strDownTrigger;

            int nQty;
            int nReserved;

            int nLAFlag = 0, nLAType = 0;//是否為長效單、長效單觸發條件[-20231218-Add]
            string strLongEndDate = "";//長效單結束日期[-20231218-Add]

            if (ExchangeNoOCO.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代碼");
                return;
            }
            strExchangeNo = ExchangeNoOCO.Text.Trim();

            if (StockNoOCO.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = StockNoOCO.Text.Trim();

            if (SettlementMonthOCO.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strSettlementMonth = SettlementMonthOCO.Text.Trim();

            if (box_BidAsk1OCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別1");
                return;
            }
            nUpBidAsk = box_BidAsk1OCO.SelectedIndex;

            if (box_BidAsk2OCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            nDownBidAsk = box_BidAsk2OCO.SelectedIndex;
                

            if (box_PeriodOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            if (box_PeriodOCO.SelectedIndex == 0)
                nPeriod = box_PeriodOCO.SelectedIndex;
            else
                nPeriod = box_PeriodOCO.SelectedIndex + 2;

            if (box_PriceType1OCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價格別");
                return;
            }
            nPriceType = (box_PriceType1OCO.SelectedIndex + 1);

            double dUpPrice = 0.0;
            if (double.TryParse(Price1OCO.Text.Trim(), out dUpPrice) == false)
            {
                MessageBox.Show("委託價1請輸入數字");
                return;
            }
            strUpPrice = Price1OCO.Text.Trim();

            double dDownPrice = 0.0;
            if (double.TryParse(Price2OCO.Text.Trim(), out dDownPrice) == false)
            {
                MessageBox.Show("委託價2請輸入數字");
                return;
            }
            strDownPrice = Price2OCO.Text.Trim();

            double dUpPriceNum = 0.0;
            if (double.TryParse(Price1Numerator.Text.Trim(), out dUpPriceNum) == false)
            {
                MessageBox.Show("委託價1分子請輸入數字");
                return;
            }
            strUpPriceNum = Price1Numerator.Text.Trim();

            double dUpPriceDen = 0.0;
            if (double.TryParse(Price1Den.Text.Trim(), out dUpPriceDen) == false)
            {
                MessageBox.Show("委託價1分母請輸入數字");
                return;
            }
            strUpPriceDen = Price1Den.Text.Trim();

            double dDownPriceNum = 0.0;
            if (double.TryParse(Price2Numerator.Text.Trim(), out dDownPriceNum) == false)
            {
                MessageBox.Show("委託價2分子請輸入數字");
                return;
            }
            strDownPriceNum = Price2Numerator.Text.Trim();

            double dDownPriceDen = 0.0;
            if (double.TryParse(Price2Den.Text.Trim(), out dDownPriceDen) == false)
            {
                MessageBox.Show("委託價2分母請輸入數字");
                return;
            }
            strDownPriceDen = Price2Den.Text.Trim();

            nQty = 0;
            if (int.TryParse(QtyOCO.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            double dUpTrigger = 0.0;
            if (double.TryParse(TriggerPrice1OCO.Text.Trim(), out dUpTrigger) == false)
            {
                MessageBox.Show("觸發價1請輸入數字");
                return;
            }
            strUpTrigger = TriggerPrice1OCO.Text.Trim();

            double dDownTrigger = 0.0;
            if (double.TryParse(TriggerPrice2OCO.Text.Trim(), out dDownTrigger) == false)
            {
                MessageBox.Show("觸發價2請輸入數字");
                return;
            }
            strDownTrigger = TriggerPrice2OCO.Text.Trim();

            double dUpTriggerNum = 0.0;
            if (double.TryParse(Trigger1Numerator.Text.Trim(), out dUpTriggerNum) == false)
            {
                MessageBox.Show("觸發價1分子請輸入數字");
                return;
            }
            strUpTriggerNum = Trigger1Numerator.Text.Trim();

            double dUpTriggerDen = 0.0;
            if (double.TryParse(Trigger1Den.Text.Trim(), out dUpTriggerDen) == false)
            {
                MessageBox.Show("觸發價1分母請輸入數字");
                return;
            }
            strUpTriggerDen = Trigger1Den.Text.Trim();

            double dDownTriggerNum = 0.0;
            if (double.TryParse(Trigger2Numerator.Text.Trim(), out dDownTriggerNum) == false)
            {
                MessageBox.Show("觸發價2分子請輸入數字");
                return;
            }
            strDownTriggerNum = Trigger2Numerator.Text.Trim();

            double dDownTriggerDen = 0.0;
            if (double.TryParse(Trigger2Den.Text.Trim(), out dDownTriggerDen) == false)
            {
                MessageBox.Show("觸發價2分母請輸入數字");
                return;
            }
            strDownTriggerDen = Trigger2Den.Text.Trim();

            if (box_ReservedOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為預約單");
                return;
            }
            nReservedFlag = box_ReservedOCO.SelectedIndex;

            if (box_TimeFlagOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = box_TimeFlagOCO.SelectedIndex;

            if (box_DayTradeOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = box_DayTradeOCO.SelectedIndex;

            //[-20231218-Add]長效單條件
            if (box_LAFlagOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為長效單");
                return;
            }
            nLAFlag = box_LAFlagOCO.SelectedIndex;

            if (box_LAFlagOCO.SelectedIndex == 1)
            {
                if (LongEndDateOCO.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入長效單結束日期");
                    return;
                }
            }
            strLongEndDate = LongEndDateOCO.Text.Trim();

            if (box_LAFlagOCO.SelectedIndex == 1)
            {
                if (box_LAFlagOCO.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入長效單結束條件");
                    return;
                }
                else if (box_LAFlagOCO.SelectedIndex == 0)
                    nLAType = (box_LAFlagOCO.SelectedIndex + 1);
                else if (box_LAFlagOCO.SelectedIndex == 1)
                    nLAType = (box_LAFlagOCO.SelectedIndex + 2);
            }

            OVERSEAFUTUREORDER pFutureOCOOrder = new OVERSEAFUTUREORDER();

            pFutureOCOOrder.bstrFullAccount = m_UserAccount;

            pFutureOCOOrder.bstrExchangeNo = strExchangeNo;
            pFutureOCOOrder.bstrStockNo = strFutureNo;
            pFutureOCOOrder.bstrYearMonth = strSettlementMonth;
            pFutureOCOOrder.sBuySell = (short)nUpBidAsk;
            pFutureOCOOrder.nBuySell2 = nDownBidAsk;
            pFutureOCOOrder.sTradeType = (short)nPeriod;
            pFutureOCOOrder.nOrderPriceType = nPriceType;
            pFutureOCOOrder.bstrOrder = strUpPrice;
            pFutureOCOOrder.bstrOrder2 = strDownPrice;
            pFutureOCOOrder.bstrOrderNumerator = strUpPriceNum;
            pFutureOCOOrder.bstrOrderDenominator = strUpPriceDen;
            pFutureOCOOrder.bstrOrderNumerator2 = strDownPriceNum;
            pFutureOCOOrder.bstrOrderDenominator2 = strDownPriceDen;
            pFutureOCOOrder.nQty = nQty;
            pFutureOCOOrder.bstrTrigger = strUpTrigger;
            pFutureOCOOrder.bstrTrigger2 = strDownTrigger;
            pFutureOCOOrder.bstrTriggerNumerator = strUpTriggerNum;
            pFutureOCOOrder.bstrTriggerDenominator = strUpTriggerDen;
            pFutureOCOOrder.bstrTriggerNumerator2 = strDownTriggerNum;
            pFutureOCOOrder.bstrTriggerDenominator2 = strDownTriggerDen;
            pFutureOCOOrder.nReserved = nReservedFlag;
            pFutureOCOOrder.nTimeFlag = nReserved;
            pFutureOCOOrder.sDayTrade = (short)nFlag;
            pFutureOCOOrder.nLongActionFlag = nLAFlag;
            pFutureOCOOrder.bstrLongEndDate = strLongEndDate;
            pFutureOCOOrder.nLAType = nLAType;

            if (OnOverseaFutureOCOOrderSignal != null)
            {
                OnOverseaFutureOCOOrderSignal(m_UserID, true, pFutureOCOOrder);
            }
            

        }

        private void btnCancelOFStrategyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strKeyNo;


            if (SmartNoOF.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單序號");
                return;
            }
            strKeyNo = SmartNoOF.Text.Trim();

            if (box_KeyTypeOF.SelectedIndex == -1 || box_KeyTypeOF.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單類別");
                return;
            }

            int nTradeTypeOfDel = box_KeyTypeOF.SelectedIndex + 1;

            CANCELSTRATEGYORDER pOrder = new CANCELSTRATEGYORDER();
            pOrder.bstrFullAccount = m_UserAccount;
            
            if (box_KeyTypeOF.SelectedIndex == 0)
            {//ClearOut
                pOrder.nMarket = 4;
                pOrder.nTradeKind = 3;
            }
            else if (box_KeyTypeOF.SelectedIndex == 1)//[-20231213-Add] AB單
            {
                if (box_MarketNoABOF.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇刪單市場別");
                    return;
                }
                pOrder.nMarket = (box_MarketNoABOF.SelectedIndex + 1);
                pOrder.nTradeKind = 10;
            }

            pOrder.bstrSmartKey = SmartNoOF.Text;
            pOrder.bstrParentSmartKey = SmartNoOF.Text;
            pOrder.bstrSeqNo = SeqNoOF.Text;
            pOrder.bstrOrderNo = OrderNoOF.Text;
            pOrder.bstrSmartKeyOut = SmartKeyOutOF.Text;
            pOrder.bstrLongActionKey = LongActionKeyOF.Text; // 長效單
            
            if (OnCancelOFStrategyOrderSignal != null)
            {
                OnCancelOFStrategyOrderSignal(m_UserID, pOrder);
            
            }
        }

        private void btnGetOFStategy_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            int nTypeReport;
            string strMarketType;
            string strKindReport;
            string strStartDate;

            if (MarketTypeOF.Text == "")
            {
                MessageBox.Show("請輸入市場類型");
                return;
            }
            strMarketType = MarketTypeOF.Text;
            if (box_TypeReportOF.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇類型");
                return;
            }
            nTypeReport = box_TypeReportOF.SelectedIndex;

            if (box_KindReportOF.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇種類");
                return;
            }
            strKindReport = box_KindReportOF.Text.Trim();

            if (StartDateBoxOF.Text.Trim() == "" || StartDateBoxOF.Text.Trim() == "YYYYMMDD")
            {
                MessageBox.Show("請輸入查詢日期");
                return;
            }
            strStartDate = StartDateBoxOF.Text.Trim();

            if (OnOFSmartStrategyReportSignal != null)
            {

                OnOFSmartStrategyReportSignal(m_UserID, m_UserAccount, strMarketType, nTypeReport, strKindReport, strStartDate);
            }
        }

        private void btnSendOverseaFutureABOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }


            string strStockNo = "", strExchangeNo = "", strTrigger = "", strFutureNo = "", strStrikePrice = "", strSettlementMonth = "", strSettlementMonth2 = "", strOrderPrice = "", strDealPrice = "";
            string strTriggerNum = "", strTriggerDen = "", strExchangeNo2 = "", strdOrderPrice = "", strPriceNum = "", strPriceDen = "";
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
                MessageBox.Show("市價請輸入數字");
                return;
            }
            strDealPrice = DealPriceAB.Text.Trim();

            if (box_DirAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸價方向");
                return;
            }
            nDir = (box_DirAB.SelectedIndex + 1);

            double dTrigger = 0.0;
            if (double.TryParse(TriggerAB.Text.Trim(), out dTrigger) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = TriggerAB.Text.Trim();

            double dTriggerNum = 0.0;
            if (double.TryParse(TriggerNumAB.Text.Trim(), out dTriggerNum) == false)
            {
                MessageBox.Show("觸發價分子請輸入數字");
                return;
            }
            strTriggerNum = TriggerNumAB.Text.Trim();

            double dTriggerDen = 0.0;
            if (double.TryParse(TriggerDenAB.Text.Trim(), out dTriggerDen) == false)
            {
                MessageBox.Show("觸發價分母請輸入數字");
                return;
            }
            strTriggerDen = TriggerDenAB.Text.Trim();

            if (StockNo2AB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入B商品代碼");
                return;
            }
            strFutureNo = StockNo2AB.Text.Trim();

            if (ExchangeNo2AB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入B商品交易所代碼");
                return;
            }
            strExchangeNo2 = ExchangeNo2AB.Text.Trim();

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
            if (box_IsOptionAB.SelectedIndex == 0)
            {
                if (box_NewCloseAB.SelectedIndex == 1)
                {
                    MessageBox.Show("期貨僅新倉");
                    return;
                }
                else
                    nNewClose = box_NewCloseAB.SelectedIndex;
            }
            else
                nNewClose = box_NewCloseAB.SelectedIndex;


            if (box_PeriodAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            if (box_PeriodAB.SelectedIndex == 0)
                nPeriod = box_PeriodAB.SelectedIndex;
            else
            {
                if (box_IsOptionAB.SelectedIndex == 1)
                {
                    MessageBox.Show("選擇權僅ROD");
                    return;
                }
                else
                    nPeriod = box_PeriodAB.SelectedIndex + 2;
            }

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
            if (box_BidAsk2AB.SelectedIndex > 0)
                nBidAsk2 = (box_BidAsk2AB.SelectedIndex - 1);
            else
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
            nPriceType = (box_PriceTypeAB.SelectedIndex + 1);

            double dOrderPrice = 0.0;
            if (double.TryParse(OrderPriceAB.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strdOrderPrice = OrderPriceAB.Text.Trim();

            double dPriceNum = 0.0;
            if (double.TryParse(PriceNumAB.Text.Trim(), out dPriceNum) == false)
            {
                MessageBox.Show("委託價分子請輸入數字");
                return;
            }
            strPriceNum = PriceNumAB.Text.Trim();

            double dPriceDen = 0.0;
            if (double.TryParse(PriceDenAB.Text.Trim(), out dPriceDen) == false)
            {
                MessageBox.Show("委託價分母請輸入數字");
                return;
            }
            strPriceDen = PriceDenAB.Text.Trim();


            if (box_PreOrderAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為預約單");
                return;
            }
            nFlag = box_PreOrderAB.SelectedIndex;




            OVERSEAFUTUREORDER pFutureABOrder = new OVERSEAFUTUREORDER();

            pFutureABOrder.bstrFullAccount = m_UserAccount;
            pFutureABOrder.bstrStockNo2 = strStockNo;
            pFutureABOrder.nMarketNo = nMarketNo;
            pFutureABOrder.bstrExchangeNo = strExchangeNo;
            pFutureABOrder.sSpecialTradeType = (short)nPrime;
            pFutureABOrder.bstrOrder2 = strDealPrice;
            pFutureABOrder.nTriggerDirection = nDir;
            pFutureABOrder.bstrTrigger = strTrigger;
            pFutureABOrder.bstrTriggerNumerator = strTriggerNum;
            pFutureABOrder.bstrTriggerDenominator = strTriggerDen;
            pFutureABOrder.bstrStockNo = strFutureNo;
            pFutureABOrder.bstrExchangeNo2 = strExchangeNo2;
            pFutureABOrder.sCallPut = (short)nIsOption;
            pFutureABOrder.bstrStrikePrice = strStrikePrice;
            pFutureABOrder.nSpreadFlag = nIsSpread;
            pFutureABOrder.bstrYearMonth = strSettlementMonth;
            pFutureABOrder.bstrYearMonth2 = strSettlementMonth2;
            pFutureABOrder.sDayTrade = (short)nIsDayTrade;
            pFutureABOrder.sNewClose = (short)nNewClose;
            pFutureABOrder.sTradeType = (short)nPeriod;
            pFutureABOrder.sBuySell = (short)nBidAsk;
            pFutureABOrder.nBuySell2 = nBidAsk2;
            pFutureABOrder.nQty = nQty;
            pFutureABOrder.nOrderPriceType = nPriceType;
            pFutureABOrder.bstrOrder = strdOrderPrice;
            pFutureABOrder.bstrOrderNumerator = strPriceNum;
            pFutureABOrder.bstrOrderDenominator = strPriceDen;
            pFutureABOrder.nTimeFlag = nFlag;


            if (OnOverseaFutureABOrderSignal != null)
            {
                OnOverseaFutureABOrderSignal(m_UserID, true, pFutureABOrder);
            }

        }
    }
}
