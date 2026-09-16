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
    public partial class FutureStopLossControl : UserControl
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
        public event OrderHandler OnFutureStopLossOrderSignal;


        public delegate void MovingOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event MovingOrderHandler OnMovingStopLossOrderSignal;
        
        public delegate void OptionOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event OptionOrderHandler OnOptionStopLossOrderSignal;

        public delegate void CancelFutrueOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, string strTradeType);
        public event CancelFutrueOrderHandler OnCancelFutureStopLossOrderSignal;

        public delegate void CancelMovingOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, string strTradeType);
        public event CancelMovingOrderHandler OnCancelMovingStopLossOrderSignal;


        public delegate void CancelOptionOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, string strTradeType);
        public event CancelOptionOrderHandler OnCancelOptionStopLossOrderSignal;

        public delegate void StopLossReportHandler(string strLogInID, string strAccount, int nReportStatus, string strKind, string strDate);
        public event StopLossReportHandler OnStopLossReportSignal;

        public delegate void FutureOCOOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREOCOORDER pOrder);
        public event FutureOCOOrderHandler OnFutureOCOOrderSignal;
        
        public delegate void CancelFutrueOCOOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, string strTradeType);
        public event CancelFutrueOCOOrderHandler OnCancelFutureOCOOrderSignal;

        public delegate void CancelFutrueMITOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, string strTradeType);
        public event CancelFutrueMITOrderHandler OnCancelFutureMITOrderSignal;

        public delegate void CancelOptionMITOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, string strTradeType);
        public event CancelOptionMITOrderHandler OnCancelOptionMITOrderSignal;

        public delegate void OnCancelStrategyTFOrderHandler(CANCELSTRATEGYORDER pOrder,bool bAsyncOrder);
        public event OnCancelStrategyTFOrderHandler OnCancelStrategyTFOrderSignal;

        public delegate void MovingOrderV1Handler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event MovingOrderV1Handler OnMovingStopLossOrderV1Signal;


        public delegate void STPOrderV1Handler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event STPOrderV1Handler OnFutureStopLossOrderV1Signal;

        public delegate void FutureOCOOrderV1Handler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event FutureOCOOrderV1Handler OnFutureOCOOrderV1Signal;


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
        public FutureStopLossControl()
        {
            InitializeComponent();
            StartDateBox.Text = DateTime.Now.ToString("yyyyMMdd");

            boxBidAsk.SelectedIndex = 0;
            boxBidAsk2.SelectedIndex = 0;
            boxBidAsk3.SelectedIndex = 0;
            boxBidAsk5.SelectedIndex = 0;
            boxBidAsk6.SelectedIndex = 0;
            boxPeriod.SelectedIndex = 0;
            boxPeriod2.SelectedIndex = 1;
            boxPeriod3.SelectedIndex = 0;
            boxPeriod4.SelectedIndex = 0;
            //2~4
            boxNewCloseSTP.SelectedIndex = 0;
            boxNewCloseMST.SelectedIndex = 0;
            boxNewCloseOST.SelectedIndex = 0;
            boxNewCloseOCO.SelectedIndex = 0;
            boxFlag.SelectedIndex = 0;
            boxFlag2.SelectedIndex = 0;
            boxFlag3.SelectedIndex = 0;

            ReservedBox1.SelectedIndex = 0;
            ReservedBox2.SelectedIndex = 0;
            ReservedBox3.SelectedIndex = 0;
            ReservedBox4.SelectedIndex = 0;

            Box_CP.SelectedIndex = 1;
            TimeFlagSTP.SelectedIndex = 1;
            TimeFlagMST.SelectedIndex = 1;
            TimeFlagTOSTP.SelectedIndex = 1;
            TimeFlagOCO.SelectedIndex = 1;

            OCOPriceType.SelectedIndex = 1;
            OCOPriceType2.SelectedIndex = 1;


        }

        #endregion

        private void btnSendFutureStopOrder_Click(object sender, EventArgs e)
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
            string strTrigger;
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

            if (boxNewCloseSTP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewCloseSTP.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M")
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

            if (double.TryParse(txtTrigger.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = txtTrigger.Text.Trim();

            if (ReservedBox1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox1.SelectedIndex;


            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.sReserved = (short)nReserved;

            //pFutureOrder.bstrTrigger = "";
            pFutureOrder.bstrDealPrice = "";
            pFutureOrder.bstrMovingPoint = "";
            //pFutureOrder.bstrPrice = "";

            if (OnFutureStopLossOrderSignal != null)
            {
                OnFutureStopLossOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendFutureStopLossOrderAsync_Click(object sender, EventArgs e)
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
            string strTigger = "", strLongEndDate = "";
            int nQty;
            int nNewClose;
            int nReserved = 0, nLAType = 0;

            
            string strYM;
            string strCID;

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

            if (boxNewCloseSTP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewCloseSTP.SelectedIndex;

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
            //if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M")
            // {
            //     MessageBox.Show("委託價請輸入數字");
            //    return;
            //}
            strPrice = txtPrice.Text.Trim();

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

            if (ReservedBox1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox1.SelectedIndex;

            if (TimeFlagSTP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為T盤");
                return;
            }
            int nTimeFlag = TimeFlagSTP.SelectedIndex;

           /* if (STPCID.Text.Trim() == "")
            {
                MessageBox.Show("請輸入後台商品代碼");
                return;
            }
            strCID = STPCID.Text.Trim();
           */
           // if (STPYM.Text.Trim() == "")
            //{
                //MessageBox.Show("請輸入年月YYYYMM");
            //    return;
           //}
            strYM = STPYM.Text.Trim();

            
            if (STPPriceType1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            int nPriceType = STPPriceType1.SelectedIndex + 1;

            if (box_LAFlagSTP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為長效單");
                return;
            }
            int nLongActionFlag = box_LAFlagSTP.SelectedIndex;

            if (box_LAFlagSTP.SelectedIndex == 1)
            {
                if (LongEndDateSTP.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入長效單結束日期");
                    return;
                }
            }
            strLongEndDate = LongEndDateSTP.Text.Trim();

            if (box_LAFlagSTP.SelectedIndex == 1)
            {
                if (LATypeSTP.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入長效單結束條件");
                    return;
                }
                else if (LATypeSTP.SelectedIndex == 0)
                    nLAType = (LATypeSTP.SelectedIndex + 1);
                else if (LATypeSTP.SelectedIndex == 1)
                    nLAType = (LATypeSTP.SelectedIndex + 2);
            }

            bool bAsync = AsyncSTP.SelectedIndex == 0 ? false : true;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;
            pFutureOrder.sReserved = (short)nReserved;

            //pFutureOrder.bstrCIDTandem = strCID;
            // pFutureOrder.bstrOrderSign = "+";
            pFutureOrder.bstrSettlementMonth = strYM;
            pFutureOrder.nTimeFlag = nTimeFlag;
            pFutureOrder.nOrderPriceType = nPriceType;

            pFutureOrder.bstrDealPrice = "";
            pFutureOrder.bstrMovingPoint = "";
            pFutureOrder.nLongActionFlag = nLongActionFlag;
            pFutureOrder.bstrLongEndDate = strLongEndDate;
            pFutureOrder.nLAType = nLAType;

            if (VerSTP.SelectedIndex == 0)
            {
                if (OnFutureStopLossOrderV1Signal != null)
                {
                    OnFutureStopLossOrderV1Signal(m_UserID, bAsync, pFutureOrder);
                }

            }
            else {
                        if (OnFutureStopLossOrderSignal != null)
                        {
                            OnFutureStopLossOrderSignal(m_UserID, bAsync, pFutureOrder);
                        }
                    }
        }


        private void btnMovingStopLossOrder_Click(object sender, EventArgs e)
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
            string strMovingPint;
            string strTrigger;
            int nQty;
            int nNewClose;
            int nReserved;


            if (txtStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo2.Text.Trim();

            if (boxBidAsk2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk2.SelectedIndex;

            if (boxPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }

            nPeriod = boxPeriod2.SelectedIndex;

            if (boxNewCloseMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            
            nNewClose = boxNewCloseMST.SelectedIndex;            

            if (boxFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtMovingPoint.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("移動點數請輸入數字");
                return;
            }
            strMovingPint = txtMovingPoint.Text.Trim();

            if (int.TryParse(txtQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTrigger2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = txtTrigger2.Text.Trim();

            if (ReservedBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox2.SelectedIndex;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            //pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.bstrMovingPoint = strMovingPint;
            pFutureOrder.sReserved = (short)nReserved;


            //pFutureOrder.bstrTrigger = "";
            pFutureOrder.bstrDealPrice = "";
            //pFutureOrder.bstrMovingPoint = "";
            pFutureOrder.bstrPrice = "";

            if (OnMovingStopLossOrderSignal != null)
            {
                OnMovingStopLossOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnMovingStopLossOrderAsync_Click(object sender, EventArgs e)
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
            string strMovingPint;
            string strTrigger;
            int nQty;
            int nNewClose;
            int nReserved;

            int nTimeFlag;

            string strYM;
            string strCID;

            if (txtStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo2.Text.Trim();

            if (boxBidAsk2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk2.SelectedIndex;

            if (boxPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            if (boxPeriod2.SelectedIndex == 0)
                nPeriod = boxPeriod2.SelectedIndex;
            else
                nPeriod = boxPeriod2.SelectedIndex + 2;

            //nPeriod = boxPeriod2.SelectedIndex;

            if (boxNewCloseMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }

            nNewClose = boxNewCloseMST.SelectedIndex;

            if (boxFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtMovingPoint.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("移動點數請輸入數字");
                return;
            }
            strMovingPint = txtMovingPoint.Text.Trim();

            if (int.TryParse(txtQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTrigger2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = txtTrigger2.Text.Trim();

            if (ReservedBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox2.SelectedIndex;

            if (TimeFlagMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為T盤");
                return;
            }
            nTimeFlag = TimeFlagMST.SelectedIndex;

           // if (MSTCID.Text.Trim() == "")
           //{
           //     MessageBox.Show("請輸入後台商品代碼");
          //      return;
          //  }
            strCID = MSTCID.Text.Trim();

            //if (MSTYM.Text.Trim() == "")
            //{
            //    MessageBox.Show("請輸入年月YYYYMM");
            //    return;
            //}
            strYM = MSTYM.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            //pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.bstrMovingPoint = strMovingPint;
            pFutureOrder.sReserved = (short)nReserved;
            pFutureOrder.bstrCIDTandem = strCID;
            //pFutureOrder.bstrOrderSign = "+";
            pFutureOrder.bstrSettlementMonth = strYM;
            pFutureOrder.nTimeFlag = nTimeFlag;

            //pFutureOrder.bstrTrigger = "";
            pFutureOrder.bstrDealPrice = "";
            //pFutureOrder.bstrMovingPoint = "";
            pFutureOrder.bstrPrice = "";

            if (VerMST.SelectedIndex == 0)
            {
                if (OnMovingStopLossOrderV1Signal != null)
                {
                    OnMovingStopLossOrderV1Signal(m_UserID, true, pFutureOrder);

                }
            }
            else
            {
                if (OnMovingStopLossOrderSignal != null)
                {
                    OnMovingStopLossOrderSignal(m_UserID, true, pFutureOrder);
                }
            }
        }

        private void btnSendOptionOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nNewClose;
            string strPrice;
            string strTrigger;
            int nQty;
            int nReserved;


            if (txtStockNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo3.Text.Trim();

            if (boxBidAsk3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk3.SelectedIndex;

            if (boxPeriod3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod3.SelectedIndex;

            if (boxNewCloseOST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉位");
                return;
            }
            nNewClose = boxNewCloseOST.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice3.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice3.Text.Trim();

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

            if (ReservedBox3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox3.SelectedIndex;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sNewClose = (short)nNewClose;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.sReserved = (short)nReserved;


            //pFutureOrder.bstrTrigger = "";
            pFutureOrder.bstrDealPrice = "";
            pFutureOrder.bstrMovingPoint = "";
            //pFutureOrder.bstrPrice = "";

            if (OnOptionStopLossOrderSignal != null)
            {
                OnOptionStopLossOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendOptionOrderAsync_Click(object sender, EventArgs e)
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
            string strTrigger;
            int nQty;
            int nReserved;

            if (txtStockNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo3.Text.Trim();

            if (boxBidAsk3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
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
           // nPeriod = boxPeriod3.SelectedIndex;

            if (boxNewCloseOST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉位");
                return;
            }
            nFlag = boxNewCloseOST.SelectedIndex;

            double dPrice = 0.0;
            //if (double.TryParse(txtPrice3.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "P")
            //{
            //    MessageBox.Show("委託價請輸入數字");
           //     return;
           // }
            strPrice = txtPrice3.Text.Trim();

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

            if (ReservedBox3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox3.SelectedIndex;

            if (TimeFlagTOSTP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為T盤");
                return;
            }
            int nTimeFlag = TimeFlagTOSTP.SelectedIndex;

            if (STPPriceType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            int nPriceType = STPPriceType2.SelectedIndex + 1;

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sNewClose = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTrigger;
            pFutureOrder.sReserved = (short)nReserved;
            pFutureOrder.nTimeFlag = nTimeFlag;
            pFutureOrder.nOrderPriceType = nPriceType;
            //pFutureOrder.bstrTrigger = "";
            pFutureOrder.bstrDealPrice = "";
            pFutureOrder.bstrMovingPoint = "";
            //pFutureOrder.bstrPrice = "";

            if (OnOptionStopLossOrderSignal != null)
            {
                OnOptionStopLossOrderSignal(m_UserID, true, pFutureOrder);
            }
        }

        private void btnCancelTFStrategyOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            string strSmartNo;
            string strSeqNo;
            string strBookNo;
            string strSymbol;
            int nMarketNo = 0;

            if (txtCancelSmartNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單序號");
                return;
            }
            strSmartNo = txtCancelSmartNo.Text.Trim();
           
            if (TypeBox.SelectedIndex == -1 || TypeBox.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單類別");
                return;
            }

            int nTrade=0;
            if (TypeBox.SelectedIndex == 0)
            {//OCO//
                nTrade = 3;
                nMarketNo = 2;
            }
            else if (TypeBox.SelectedIndex == 1)
            {//STP
                nTrade = 5;
                nMarketNo = 2;
            }
            else if (TypeBox.SelectedIndex == 2)
            {//MIT
                nTrade =8;
                nMarketNo = 2;
            }
            else if (TypeBox.SelectedIndex == 3)
            {//MST
                nTrade = 9;
                nMarketNo = 2;
            }
            else if (TypeBox.SelectedIndex == 4)
            {//AB
                nTrade = 10;
                if (box_MarketNoABDel.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇刪單市場別");
                    return;
                }
                nMarketNo = (box_MarketNoABDel.SelectedIndex + 1);
                
            }

            strSeqNo = cancelSeqno.Text.Trim();
            strBookNo = CancelBookno.Text.Trim();
            CANCELSTRATEGYORDER pOrder = new CANCELSTRATEGYORDER();
            pOrder.bstrFullAccount = m_UserAccount;
            pOrder.bstrLogInID = m_UserID;
            pOrder.bstrSmartKey = strSmartNo;
            pOrder.bstrSeqNo = strSeqNo;
            pOrder.bstrOrderNo = strBookNo;
            pOrder.nTradeKind = nTrade;
            pOrder.bstrLongActionKey = LongActionKey.Text; //[-20231218-Add] 長效單
            pOrder.nMarket = nMarketNo;//[-20231219-Add] AB單

            //pOrder.bstrTradeKind = strSymbol;
            bool bAsyncOrder = true;
            if (OnCancelStrategyTFOrderSignal != null)
            {
                OnCancelStrategyTFOrderSignal(pOrder, bAsyncOrder);

            }
            //if (OnCancelFutureStopLossOrderSignal != null)
            //{
            //   OnCancelFutureStopLossOrderSignal(m_UserID, true, m_UserAccount, strBookNo, strSymbol);
            //}
        }

       

        private void btnGetStopLossReport_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            int nTypeReport;
            string strKindReport;
            string strStartDate;

            if (boxTypeReport.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇類型");
                return;
            }
            nTypeReport = boxTypeReport.SelectedIndex;

            if (boxKindReport.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇種類");
                return;
            }
            strKindReport = boxKindReport.Text.Trim();

            if (StartDateBox.Text.Trim() == "" || StartDateBox.Text.Trim() == "YYYYMMDD")
            {
                MessageBox.Show("請輸入查詢日期");
                return;
            }
            strStartDate = StartDateBox.Text.Trim();

            if (OnStopLossReportSignal != null)
            {

                OnStopLossReportSignal(m_UserID, m_UserAccount, nTypeReport, strKindReport, strStartDate);
            }
        }

       
        private void btnSendOCOOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            
            
            int nNewClose;
            int nFlag;
            int nUpBidAsk;
            int nPeriod;
            string strUpPrice;
            string strUpTrigger;
            int nDownBidAsk=0;
            string strDownPrice;
            string strDownTrigger;

            int nQty;
            int nReserved;
            int nLAFlag = 0, nLAType = 0;//是否為長效單、長效單觸發條件[-20231218-Add]
            string strLongEndDate = "";//長效單結束日期[-20231218-Add]


            if (txtStockNo4.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo4.Text.Trim();

            if (boxBidAsk5.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別1");
                return;
            }
            nUpBidAsk = boxBidAsk5.SelectedIndex;

            if (boxBidAsk6.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            if (boxBidAsk6.SelectedIndex == 0)
                nDownBidAsk = 0;
            else if (boxBidAsk6.SelectedIndex == 1)
                nDownBidAsk = 1; 

            if (boxPeriod4.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }

            if (boxPeriod4.SelectedIndex == 0)
                nPeriod = boxPeriod4.SelectedIndex;
            else
                nPeriod = boxPeriod4.SelectedIndex + 2;

            if (boxNewCloseOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉位");
                return;
            }
            nNewClose = boxNewCloseOCO.SelectedIndex;

            if (boxFlag3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag3.SelectedIndex;

            double dUpPrice = 0.0;
            if (double.TryParse(txtPrice4.Text.Trim(), out dUpPrice) == false)
            {
                MessageBox.Show("委託價1請輸入數字");
               return;
            }
            strUpPrice = txtPrice4.Text.Trim();

            double dDownPrice = 0.0;
            if (double.TryParse(txtPrice5.Text.Trim(), out dDownPrice) == false)
            {
                MessageBox.Show("委託價2請輸入數字");
                return;
           }
            strDownPrice = txtPrice5.Text.Trim();

            nQty = 0;
            if (int.TryParse(txtQty4.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTriggerLarger.Text.Trim(), out dUpPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strUpTrigger = txtTriggerLarger.Text.Trim();

            if (double.TryParse(txtTriggerLess.Text.Trim(), out dUpPrice) == false)
            {
                MessageBox.Show("觸發價2請輸入數字");
                return;
            }
            strDownTrigger = txtTriggerLess.Text.Trim();

            if (ReservedBox4.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox4.SelectedIndex;

            int nPriceType1,nTimeFlag;

            //if (int.TryParse(OCOPriceType.Text.Trim(), out nQty2) == false)
            // {
            //     MessageBox.Show("委託量請輸入數字");
            //     return;
            //}
            nPriceType1 = OCOPriceType.SelectedIndex+1;
          
           
            nTimeFlag = TimeFlagOCO.SelectedIndex;
            //if (OCOYM.Text.Trim() == "")
            //{
            //    MessageBox.Show("請輸入商品年月");
            //     return;
            //}
            string strOCOYM = "";
            strOCOYM = OCOYM.Text.Trim();

            //OCOPriceType2//txtQty4_2//boxNewCloseOCO2//TimeFlagOCO//            VerOCO

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

            FUTUREOCOORDER pFutureOCOOrder = new FUTUREOCOORDER();
            
            pFutureOCOOrder.bstrFullAccount = m_UserAccount;
            pFutureOCOOrder.sDayTrade = (short)nFlag;
            pFutureOCOOrder.sNewClose = (short)nNewClose;
            pFutureOCOOrder.sTradeType = (short)nPeriod;
            pFutureOCOOrder.bstrStockNo = strFutureNo;
            pFutureOCOOrder.bstrPrice = strUpPrice;
            pFutureOCOOrder.bstrTrigger = strUpTrigger;
            pFutureOCOOrder.bstrPrice2 = strDownPrice;
            pFutureOCOOrder.bstrTrigger2 = strDownTrigger;
            //pFutureOCOOrder.bstrSettlementMonth = strOCOYM;
            pFutureOCOOrder.nQty = nQty;
            pFutureOCOOrder.sBuySell = (short)nUpBidAsk; 
            pFutureOCOOrder.sBuySell2 = (short)nDownBidAsk;
            pFutureOCOOrder.sReserved = (short)nReserved;            
            //pFutureOCOOrder.sTimeFlag = (short)nTimeFlag;            
            pFutureOCOOrder.nOrderPriceType1 = nPriceType1;//ignore

            FUTUREORDER pFutureOCOOrder2 = new FUTUREORDER();
            pFutureOCOOrder2.bstrFullAccount = m_UserAccount;
            pFutureOCOOrder.sDayTrade = (short)nFlag;
            pFutureOCOOrder2.sNewClose = (short)nNewClose;
            pFutureOCOOrder2.sTradeType = (short)nPeriod;
            pFutureOCOOrder2.bstrStockNo = strFutureNo;
            pFutureOCOOrder2.bstrPrice = strUpPrice;
            pFutureOCOOrder2.bstrTrigger = strUpTrigger;
            pFutureOCOOrder2.bstrPrice2 = strDownPrice;
            pFutureOCOOrder2.bstrTrigger2 = strDownTrigger;
            pFutureOCOOrder2.bstrSettlementMonth = strOCOYM;
            pFutureOCOOrder2.nQty = nQty;
            pFutureOCOOrder2.sBuySell = (short)nUpBidAsk;
            pFutureOCOOrder2.sBuySell2 = (short)nDownBidAsk;
            pFutureOCOOrder2.sReserved = (short)nReserved;
            pFutureOCOOrder2.nTimeFlag = nTimeFlag;
            pFutureOCOOrder2.nOrderPriceType = nPriceType1;//ignore
            pFutureOCOOrder2.nLongActionFlag = nLAFlag;//[-20231218-Add]
            pFutureOCOOrder2.bstrLongEndDate = strLongEndDate;//[-20231218-Add]
            pFutureOCOOrder2.nLAType = nLAType;//[-20231218-Add]

            if (VerOCO.SelectedIndex == 0)
            {
                if (OnFutureOCOOrderV1Signal != null)
                {
                    OnFutureOCOOrderV1Signal(m_UserID, true, pFutureOCOOrder2);
                }
            }
            else
            {
                if (OnFutureOCOOrderSignal != null)
                {
                    OnFutureOCOOrderSignal(m_UserID, true, pFutureOCOOrder);
                }
            }
            
        }

        private void btnSendOCOOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            

            int nNewClose;
            int nUpBidAsk;
            int nPeriod;
            string strPrice;
            string strUpTrigger;
            int nDownBidAsk;
            string strPrice2;
            string strDownTrigger;

            int nQty;
            int nReserved;


            if (txtStockNo4.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo4.Text.Trim();

            if (boxBidAsk5.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別1");
                return;
            }
            nUpBidAsk = boxBidAsk5.SelectedIndex;

            if (boxBidAsk6.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            nDownBidAsk = boxBidAsk6.SelectedIndex;


            if (boxPeriod4.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod4.SelectedIndex;

            if (boxNewCloseOCO.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉位");
                return;
            }
            nNewClose = boxNewCloseOCO.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice4.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M")
            {
                MessageBox.Show("委託價1請輸入數字");
                return;
            }
            strPrice = txtPrice4.Text.Trim();


            if (double.TryParse(txtPrice5.Text.Trim(), out dPrice) == false && txtPrice.Text.Trim() != "M")
            {
                MessageBox.Show("委託價2請輸入數字");
                return;
            }
            strPrice2 = txtPrice5.Text.Trim();

            if (int.TryParse(txtQty4.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(txtTriggerLarger.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strUpTrigger = txtTriggerLarger.Text.Trim();

            if (double.TryParse(txtTriggerLess.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("觸發價2請輸入數字");
                return;
            }
            strDownTrigger = txtTriggerLess.Text.Trim();

            if (ReservedBox4.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nReserved = ReservedBox4.SelectedIndex;

            FUTUREOCOORDER pFutureOCOOrder = new FUTUREOCOORDER();

            pFutureOCOOrder.bstrFullAccount = m_UserAccount;
            pFutureOCOOrder.sNewClose = (short)nNewClose;
            pFutureOCOOrder.sTradeType = (short)nPeriod;
            pFutureOCOOrder.bstrStockNo = strFutureNo;
            pFutureOCOOrder.nQty = nQty;
            pFutureOCOOrder.bstrPrice = strPrice;
            pFutureOCOOrder.sBuySell = (short)nUpBidAsk;
            pFutureOCOOrder.bstrTrigger = strUpTrigger;
            pFutureOCOOrder.bstrPrice2 = strPrice2;
            pFutureOCOOrder.sBuySell2 = (short)nDownBidAsk;
            pFutureOCOOrder.bstrTrigger2 = strDownTrigger;
            pFutureOCOOrder.sReserved = (short)nReserved;

            if (OnFutureOCOOrderSignal != null)
            {
                OnFutureOCOOrderSignal(m_UserID, false, pFutureOCOOrder);
            }
        }

        private void btnCancelTFStrategy_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            string strSmartNo;
            string strSymbol;

            if (txtCancelSmartNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單序號");
                return;
            }
            strSmartNo = txtCancelSmartNo.Text.Trim();

            if (TypeBox.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單類別");
                return;
            }
            strSymbol = TypeBox.Text.Trim().Substring(1);
            // 3OCO 5STP 8MIT 9MST

            if (MarketCancelBox.SelectedIndex == -1)
            {
                MessageBox.Show("請選擇市場TF/TO");
                return;
            }

            if (strSymbol == "MIT")
            {
                if (MarketCancelBox.SelectedIndex == 1)
                {
                    if (OnCancelOptionMITOrderSignal != null)
                    {

                        OnCancelOptionMITOrderSignal(m_UserID, true, m_UserAccount, strSmartNo, strSymbol);
                    }
                }
                if (MarketCancelBox.SelectedIndex == 0)
                {
                    if (OnCancelFutureMITOrderSignal != null)
                    {

                        OnCancelFutureMITOrderSignal(m_UserID, true, m_UserAccount, strSmartNo, strSymbol);
                    }
                }
            }

            if (strSymbol == "STP")
            {
                if (MarketCancelBox.SelectedIndex == 0)
                {
                    if (OnCancelFutureStopLossOrderSignal != null)
                    {

                        OnCancelFutureStopLossOrderSignal(m_UserID, true, m_UserAccount, strSmartNo, strSymbol);
                    }
                }

                if (MarketCancelBox.SelectedIndex == 1)
                {
                 if (OnCancelOptionStopLossOrderSignal != null)
                 {

                     OnCancelOptionStopLossOrderSignal(m_UserID, true, m_UserAccount, strSmartNo, strSymbol);
                 }//OnCancelOptionStopLossOrderSignal != null)
                }
            }

            if (strSymbol == "MST")
            {
                if (OnCancelMovingStopLossOrderSignal != null)
                {

                    OnCancelMovingStopLossOrderSignal(m_UserID, true, m_UserAccount, strSmartNo, strSymbol);
                }
            }

            if (strSymbol == "OCO")
            {
                if (OnCancelFutureOCOOrderSignal != null)
                {

                    OnCancelFutureOCOOrderSignal(m_UserID, true, m_UserAccount, strSmartNo, strSymbol);
                }
            }

        }

        private void boxNewCloseOCO_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxNewCloseOCO2.SelectedIndex = boxNewCloseOCO.SelectedIndex;
            boxNewCloseOCO2.Enabled = false;
        }

        private void txtQty4_KeyDown(object sender, KeyEventArgs e)
        {
            txtQty4_2.Text = txtQty4.Text;
            txtQty4_2.Enabled = false;
        }

        private void OCOPriceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OCOPriceType2.SelectedIndex = OCOPriceType.SelectedIndex;
            OCOPriceType2.Enabled = false;
        }

        private void txtQty4_KeyUp(object sender, KeyEventArgs e)
        {
            txtQty4_2.Text = txtQty4.Text;
            txtQty4_2.Enabled = false;
        }
    }
}
