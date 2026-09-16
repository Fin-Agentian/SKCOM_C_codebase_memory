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
    public partial class StockStrategyOrderControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        private int m_nCode;
        public string m_strMessage;


        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void StockDayTradeOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder);
        public event StockDayTradeOrderHandler OnStockStrategyDayTradeSignal;

        public delegate void StockAllCleanOutOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDEROUT pOrder);
        public event StockAllCleanOutOrderHandler OnStockStrategyAllCleanOutSignal;

        public delegate void TSStrategyReportHandler(string strLogInID, string strAccount, string strMarketType, int nReportStatus, string strKind, string strDate);
        public event TSStrategyReportHandler OnTSStrategyReportSignal;

        public delegate void CancelTSStrategyOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, int nTradeType);
        public event CancelTSStrategyOrderHandler OnCancelTSStrategyOrderSignal;

        public delegate void StockMITOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder);
        public event StockMITOrderHandler OnStockStrategyMITOrderSignal;

        public delegate void StockOCOOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDEROCO pOrder);
        public event StockOCOOrderHandler OnStockStrategyOCOOrderSignal;

        public delegate void StockMIOCOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIOC pOrder);
        public event StockMIOCOrderHandler OnStockStrategyMIOCOrderSignal;

        public delegate void StockMSTOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder);
        public event StockMSTOrderHandler OnStockStrategyMSTOrderSignal;

        public delegate void CancelTSStrategyOrderV1Handler(string strLogInID, CANCELSTRATEGYORDER pOrder);
        public event CancelTSStrategyOrderV1Handler OnCancelTSStrategyOrderV1Signal;

        /*public delegate void StockMMITOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder); //[-20231213-Add]
        public event StockMMITOrderHandler OnStockStrategyMMITOrderSignal;*/

        public delegate void StockABOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder); //[-20231214-Add]
        public event StockABOrderHandler OnStockStrategyABOrderSignal;

        public delegate void StockCBOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder); //[-20231214-Add]
        public event StockCBOrderHandler OnStockStrategyCBOrderSignal;

        /*public delegate void StockMBAOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder); //[-20231215-Add]
        public event StockMBAOrderHandler OnStockStrategyMBAOrderSignal;*/

        /*public delegate void StockLLSOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder); //[-20231218-Add]
        public event StockLLSOrderHandler OnStockStrategyLLSOrderSignal;*/

        /*public delegate void StockFTLDayTradeOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder); //[-20240130-Add]
        public event StockFTLDayTradeOrderHandler OnStockStrategyFTLDayTradeOrderSignal;*/

        public delegate void CancelStrategyListHandler(string strLogInID, CANCELSTRATEGYORDER pOrder);//[-20240229-Add]
        public event CancelStrategyListHandler OnCancelStrategyListSignal;

        public delegate void StockFTLCBOrderHandler(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder); //[-20231214-Add]
        public event StockFTLCBOrderHandler OnStockStrategyFTLCBOrderSignal;

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

        public StockStrategyOrderControl()
        {
            InitializeComponent();
            boxKindReport.SelectedIndex = 0;
            KeyTypeBox.SelectedIndex = 0;
            StartDateBox.Text = DateTime.Now.ToString("yyyyMMdd");
        }


        private void btnDayTrade_Click(object sender, EventArgs e)
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;              //委託股票代號
                                            //int     nTradeType=0;		    //交易種類,一律為整股
                                            //int     nOrderType=0;			//委託-0:現;1:資;2:券
            int nBidAsk = 0;			    //0買-1賣出-2無券賣出;出清現賣-資賣-券買	
            int nQty = 0;
            //int     nIsWarrant=0;			//權證
            int nClearAllFlag = 0;		//*clear all flag:
            string strClearCancelTime = "";	//*clear time
            int nFinalClearFlag = 0;		//*Final Clear flag
            string strClearAllOrderPrice = "";  //出清委託價	
                                                //當沖條件//
            string strOrderPrice = "";			//委託價
            int nTakeProfitFlag = 0;			//停利Flag
            int nRDOTPPercent = 0;				//停利類型
            string strTPPercent = "";			//停利百分比
            string strTPTrigger = "";           //停利觸發價
            int nRDTPMarketPriceType = 0;		//停利委託價方式=1:市價 2:限價 3:限價委託價
            string strTPOrderPrice = "";		//停利委託價
            int nStopLossFlag = 0;				//停損Flag
            int nRDOSLPercent = 0;				//停損類型
            string strSLPercent = "";			//停損百分比值
            string strSLTrigger = "";			//停損觸發價
            int nRDSLMarketPriceType = 0;       //停損委託價方式=1:市價 2:限價 3:限價委託價
            string strSLOrderPrice = "";			//停損委託價	
            int nClearAllPriceType = 0;			//出清委託價方式=1:市價 2:限價
            int nOrderPriceCond = 0, nOrderPriceType = 0, nSLCond = 0, nTPCond = 0, nClear = 0;

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

            double dOrderPrice = 0.0;
            if (double.TryParse(txtOrderPrice.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = txtOrderPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }
            if (Box_OrderCon.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場委託價條件(R/I/F)");
                return;
            }
            //nOrderPriceCond = Box_OrderCon.SelectedIndex;
            switch (Box_OrderCon.SelectedIndex)
            {
                case 0:
                    nOrderPriceCond = 0;
                    break;
                case 1:
                    nOrderPriceCond = 3;
                    break;
                case 2:
                    nOrderPriceCond = 4;
                    break;
            }

            if (Box_OrderPriceType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場委託價類別(1市價/2限價)");
                return;
            }

            switch (Box_OrderPriceType.SelectedIndex) //[-20200820-]
            {
                case 0:
                    nOrderPriceType = 1;
                    break;
                case 1:
                    nOrderPriceType = 2;
                    break;
            }

            int nDT_MITFlag = 0;
            int nDT_MITDir = 0;
            double dDT_TriggerPrice = 0.0;
            string strDTTrigger = "";
            string strDTBase = "";
            if (DT_MITFlagBox.Checked)
            {
                nDT_MITFlag = 1;
                if (double.TryParse(DT_MIT_TriggerPrice.Text.Trim(), out dDT_TriggerPrice) == false)
                {
                    MessageBox.Show("進場MIT觸發價請輸入數字");
                    return;
                }
                strDTTrigger = DT_MIT_TriggerPrice.Text.Trim();

                if (DT_MIT_TDirBox.SelectedIndex < 0)
                {
                    MessageBox.Show("進場MIT觸價方向請選擇至少一個");
                    return;
                }
                nDT_MITDir = DT_MIT_TDirBox.SelectedIndex + 1;

                if (double.TryParse(DT_MIT_BASEPrice.Text.Trim(), out dDT_TriggerPrice) == false)
                {
                    MessageBox.Show("進場MIT市價請輸入數字");
                    return;
                }
                strDTBase = DT_MIT_BASEPrice.Text.Trim();

            }
            else if (!(DT_MITFlagBox.Checked))
            {
                nDT_MITFlag = 0;
                strDTTrigger = "0";
                nDT_MITDir = 0;
                strDTBase = "0";

            }

            if (ckBoxLoss.Checked)
            {

                double dLossPercent = 0.0;
                double dTriggerLoss = 0.0;
                nStopLossFlag = 1;

                if (Box_SLCond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇停損委託條件(R/I/F)");
                    return;
                }
                //nSLCond = Box_SLCond.SelectedIndex;
                switch (Box_SLCond.SelectedIndex)
                {
                    case 0:
                        nSLCond = 0;
                        break;
                    case 1:
                        nSLCond = 3;
                        break;
                    case 2:
                        nSLCond = 4;
                        break;
                }


                if (radioLoss.Checked)
                {// 1:漲跌幅 0:觸發價
                    if (double.TryParse(txtLossPercent.Text.Trim(), out dLossPercent) == false)
                    {
                        MessageBox.Show("停損跌幅請輸入數字");
                        return;
                    }
                    nRDOSLPercent = 1;
                    strSLPercent = txtLossPercent.Text.Trim();
                }
                else if (radioTriggerLoss.Checked)
                {
                    nRDOSLPercent = 0;
                    if (double.TryParse(txtTriggerLoss.Text.Trim(), out dTriggerLoss) == false)
                    {
                        MessageBox.Show("停損觸發價請輸入數字");
                        return;
                    }
                    strSLTrigger = txtTriggerLoss.Text.Trim();
                }
                if (radioOrderLoss.Checked)
                {//flag 0:None 1:市價 2:限價              
                    nRDSLMarketPriceType = 2;

                    if (double.TryParse(txtLossOrderPrice.Text.Trim(), out dTriggerLoss) == false)
                    {
                        MessageBox.Show("停損委託限價請輸入數字");
                        return;
                    }
                    strSLOrderPrice = txtLossOrderPrice.Text.Trim();
                }
                else if (radioOrderTypeLossM.Checked)
                {
                    nRDSLMarketPriceType = 1;
                }
            }
            else
                nStopLossFlag = 0;

            double dProfitPercent = 0.0;
            double dTriggerProfit = 0.0;
            if (ckboxProfit.Checked)
            {
                nTakeProfitFlag = 1;

                if (Box_TPCond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇停利委託條件(R/I/F)");
                    return;
                }
                //nTPCond = Box_TPCond.SelectedIndex;
                switch (Box_TPCond.SelectedIndex)
                {
                    case 0:
                        nTPCond = 0;
                        break;
                    case 1:
                        nTPCond = 3;
                        break;
                    case 2:
                        nTPCond = 4;
                        break;
                }


                if (radioProfit.Checked)
                {// 1:漲跌幅 0:觸發價
                    nRDOTPPercent = 1;

                    if (double.TryParse(txtProfitPercent.Text.Trim(), out dProfitPercent) == false)
                    {
                        MessageBox.Show("停利漲幅請輸入數字");
                        return;
                    }
                    strTPPercent = txtProfitPercent.Text.Trim();
                }
                else if (radioTriggerProfit.Checked)
                {
                    nRDOTPPercent = 0;
                    if (double.TryParse(txtTriggerProfit.Text.Trim(), out dTriggerProfit) == false)
                    {
                        MessageBox.Show("停利觸發價請輸入數字");
                        return;
                    }
                    strTPTrigger = txtTriggerProfit.Text.Trim();
                }
                if (radioOrderProfit.Checked)
                {//flag 0:None 1:市價 2:限價(以選定漲跌停為主)               
                    nRDTPMarketPriceType = 2;

                    if (double.TryParse(txtProfitOrderPrice.Text.Trim(), out dTriggerProfit) == false)
                    {
                        MessageBox.Show("停利委託限價請輸入數字");
                        return;
                    }
                    strTPOrderPrice = txtProfitOrderPrice.Text.Trim();
                }
                else if (RadioOrderTypeProfitM.Checked)
                {
                    nRDTPMarketPriceType = 1;
                }
            }
            else
                nTakeProfitFlag = 0;



            if (CkBoxClear1.Checked)
            {
                nClearAllFlag = 1;
                strClearCancelTime = boxHr1.Text.Trim() + boxMin1.Text.Trim();

                if (Box_ClearCond1.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇出清委託條件(R/I/F)");
                    return;
                }
                //nClear = Box_ClearCond1.SelectedIndex;

                switch (Box_ClearCond1.SelectedIndex)
                {
                    case 0:
                        nClear = 0;
                        break;
                    case 1:
                        nClear = 3;
                        break;
                    case 2:
                        nClear = 4;
                        break;
                }


                if (radioOrderClear1.Checked)
                {
                    nClearAllPriceType = 2;
                    if (double.TryParse(txtClearOrder.Text.Trim(), out dTriggerProfit) == false)
                    {
                        MessageBox.Show("出清委託限價請輸入數字");
                        return;
                    }
                    strClearAllOrderPrice = txtClearOrder.Text.Trim();
                }
                else if (radioClearTypeM.Checked)
                {
                    nClearAllPriceType = 1;
                }
            }
            else
                nClearAllFlag = 0;

            if (ckBoxFinal.Checked)
                nFinalClearFlag = 1;
            else
                nFinalClearFlag = 0;



            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();

            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nQty = nQty;

            //pStockOrder.sIsWarrant = (short)nIsWarrant;
            pStockOrder.bstrOrderPrice = strOrderPrice;

            pStockOrder.nInnerOrderIsMIT = nDT_MITFlag;

            pStockOrder.nInnerOrderIsMIT = nDT_MITFlag;
            pStockOrder.bstrMITTriggerPrice = strDTTrigger;
            pStockOrder.nMITDir = nDT_MITDir;
            pStockOrder.bstrMITDealPrice = strDTBase;


            pStockOrder.nTakeProfitFlag = nTakeProfitFlag;
            pStockOrder.nRDOTPPercent = nRDOTPPercent;
            pStockOrder.bstrTPPercent = strTPPercent;
            pStockOrder.bstrTPTrigger = strTPTrigger;
            pStockOrder.nRDTPMarketPriceType = nRDTPMarketPriceType;
            pStockOrder.bstrTPOrderPrice = strTPOrderPrice;

            pStockOrder.nStopLossFlag = nStopLossFlag;
            pStockOrder.nRDOSLPercent = nRDOSLPercent;
            pStockOrder.bstrSLPercent = strSLPercent;
            pStockOrder.bstrSLTrigger = strSLTrigger;
            pStockOrder.nRDSLMarketPriceType = nRDSLMarketPriceType;
            pStockOrder.bstrSLOrderPrice = strSLOrderPrice;

            pStockOrder.nClearAllFlag = nClearAllFlag;
            pStockOrder.bstrClearCancelTime = strClearCancelTime;
            pStockOrder.nClearAllPriceType = nClearAllPriceType;
            pStockOrder.bstrClearAllOrderPrice = strClearAllOrderPrice;

            pStockOrder.nFinalClearFlag = nFinalClearFlag;

            //進場委託價條件;類別
            pStockOrder.nOrderPriceCond = nOrderPriceCond;
            pStockOrder.nOrderPriceType = nOrderPriceType;

            //nSLCond, nTPCond, nClear;
            pStockOrder.nStopLossOrderCond = nSLCond;
            pStockOrder.nTakeProfitOrderCond = nTPCond;
            pStockOrder.nClearOrderCond = nClear;

            if (OnStockStrategyDayTradeSignal != null)
            {
                OnStockStrategyDayTradeSignal(m_UserID, false, pStockOrder);
            }
        }

        private void btnAllCleanOut_Click(object sender, EventArgs e)
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;		//委託股票代號
            //int nTradeType;		    //交易種類,一律為整股
            int nOrderType;			//委託-0:現;1:資;2:券
            int nBidAsk;            //0買-1賣出-2無券賣出;出清現賣-資賣-券買	

            int nQty;

            int nClearAllFlag;			//*clear all flag
            string strClearCancelTime = "";	//*clear time
            int nFinalClearFlag;		//*Final Clear flag
            string strClearAllOrderPrice = "";	//出清委託價	
            //出清條件//
            int nLTEFlag;					//LTE flag
            string strLTETriggerPrice = "";		//LTE
            string strLTEOrderPrice = "";		//LTE
            int nLTEMarketPrice = 0;			//Market Price flag
            int nGTEFlag;					//BTE flag
            string strGTETriggerPrice = "";		//BTE
            string strGTEOrderPrice = "";		//BTE
            int nGTEMarketPrice = 0;			//BTE Market Price flag

            int nTimeClearMarketPrice = 0;		//Time Clear Market Price flag1:市價 2:限價
            double dOrderPrice = 0.0;
            int nGTECond = 0, nLTECond = 0, nClear = 0;

            if (txtStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo2.Text.Trim();

            if (boxBidAsk2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk2.SelectedIndex;

            if (boxOrderType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別");
                return;
            }

            if (boxOrderType.SelectedIndex == 0)
                nOrderType = boxOrderType.SelectedIndex;
            else
                nOrderType = boxOrderType.SelectedIndex + 2;
            //nOrderType = boxOrderType.SelectedIndex;

            if (int.TryParse(txtQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxGTE.Checked)
            {

                double dOrderGTE = 0.0;

                nGTEFlag = 1;

                if (Box_GTECond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇大於委託條件(R/I/F)");
                    return;
                }
                //nGTECond = Box_GTECond.SelectedIndex;
                switch (Box_GTECond.SelectedIndex)
                {
                    case 0:
                        nGTECond = 0;
                        break;
                    case 1:
                        nGTECond = 3;
                        break;
                    case 2:
                        nGTECond = 4;
                        break;
                }


                if (double.TryParse(txtGTEPrice.Text.Trim(), out dOrderGTE) == false)
                {
                    MessageBox.Show("請輸入成交價大於條件-數字");
                    return;
                }
                strGTETriggerPrice = txtGTEPrice.Text.Trim();

                if (radioGTEOrder.Checked)
                {// 1:市價; 2:限價

                    nGTEMarketPrice = 2;

                    if (double.TryParse(txtGTEOrder.Text.Trim(), out dOrderGTE) == false)
                    {
                        MessageBox.Show("請輸入成交價大於條件-委託價");
                        return;
                    }

                    strGTEOrderPrice = txtGTEOrder.Text.Trim();
                }
                else if (radioGTEOrderTypeM.Checked)
                {
                    nGTEMarketPrice = 1;
                }
            }
            else
            {
                nGTEFlag = 0;
            }



            double dLTEPrice = 0.0;
            double dTriggerPrice = 0.0;
            if (boxLTE.Checked)
            {

                nLTEFlag = 1;

                if (Box_LTECond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇小於委託條件(R/I/F)");
                    return;
                }
                //nLTECond = Box_LTECond.SelectedIndex;
                switch (Box_LTECond.SelectedIndex)
                {
                    case 0:
                        nLTECond = 0;
                        break;
                    case 1:
                        nLTECond = 3;
                        break;
                    case 2:
                        nLTECond = 4;
                        break;
                }
                if (double.TryParse(txtLTEPrice.Text.Trim(), out dLTEPrice) == false)
                {
                    MessageBox.Show("請輸入成交價小於條件-數字");
                    return;
                }
                strLTETriggerPrice = txtLTEPrice.Text.Trim();

                if (radioLTEOrder.Checked)
                {// 1:市價; 2:限價

                    nLTEMarketPrice = 2;

                    if (double.TryParse(txtLTEOrder.Text.Trim(), out dTriggerPrice) == false)
                    {
                        MessageBox.Show("請輸入成交價小於條件-委託價");
                        return;
                    }

                    strLTEOrderPrice = txtLTEOrder.Text.Trim();

                }

                else if (radioLTEOrderTypeM.Checked)
                {
                    nLTEMarketPrice = 1;
                }

            }
            else
            {
                nLTEFlag = 0;
            }




            if (boxClearOut2.Checked)
            {
                nClearAllFlag = 1;
                strClearCancelTime = boxHr2.Text.Trim() + boxMin2.Text.Trim();
                string strHr2 = boxHr2.Text.Trim();
                int nHrCancelTime = int.Parse(strHr2) * 10000;
                string strMin2 = boxMin2.Text.Trim();
                int nMinCancelTime = int.Parse(strMin2) * 100;
                if (nHrCancelTime + nMinCancelTime > 132000)
                {
                    MessageBox.Show("選擇出清時間已超過13:20,請重新選擇");
                    return;
                }

                if (Box_ClearCond2.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇出清委託條件(R/I/F)");
                    return;
                }
                //nClear = Box_ClearCond2.SelectedIndex;
                switch (Box_ClearCond2.SelectedIndex)
                {
                    case 0:
                        nClear = 0;
                        break;
                    case 1:
                        nClear = 3;
                        break;
                    case 2:
                        nClear = 4;
                        break;
                }
                if (radioClearOrder2.Checked)
                {
                    nTimeClearMarketPrice = 2;
                    if (double.TryParse(txtClearOrder2.Text.Trim(), out dTriggerPrice) == false)
                    {
                        MessageBox.Show("出清委託價請輸入數字");
                        return;
                    }
                    strClearAllOrderPrice = txtClearOrder2.Text.Trim();
                }
                else if (radioClearType2M.Checked)
                {
                    nTimeClearMarketPrice = 1;
                }
            }
            else
            {
                nClearAllFlag = 0;
            }



            if (ckBoxFinal2.Checked)
            {
                nFinalClearFlag = 1;
            }
            else
            {
                nFinalClearFlag = 0;
            }

            STOCKSTRATEGYORDEROUT pStockOrder = new STOCKSTRATEGYORDEROUT();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderType = nOrderType;

            pStockOrder.nQty = nQty;

            pStockOrder.nLTEFlag = nLTEFlag;
            pStockOrder.bstrLTETriggerPrice = strLTETriggerPrice;
            pStockOrder.nLTEMarketPrice = nLTEMarketPrice;
            pStockOrder.bstrLTEOrderPrice = strLTEOrderPrice;

            pStockOrder.nGTEFlag = nGTEFlag;
            pStockOrder.bstrGTETriggerPrice = strGTETriggerPrice;
            pStockOrder.nGTEMarketPrice = nGTEMarketPrice;
            pStockOrder.bstrGTEOrderPrice = strGTEOrderPrice;

            pStockOrder.nClearAllFlag = nClearAllFlag;
            pStockOrder.bstrClearCancelTime = strClearCancelTime;
            pStockOrder.nClearAllPriceType = nTimeClearMarketPrice;
            pStockOrder.bstrClearAllOrderPrice = strClearAllOrderPrice;

            pStockOrder.nGTEOrderCond = nGTECond;
            pStockOrder.nLTEOrderCond = nLTECond;
            pStockOrder.nClearOrderCond = nClear;

            pStockOrder.nFinalClearFlag = nFinalClearFlag;

            if (OnStockStrategyAllCleanOutSignal != null)
            {
                OnStockStrategyAllCleanOutSignal(m_UserID, false, pStockOrder);
            }

        }

        private void btnDayTradeAsync_Click(object sender, EventArgs e)
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;          //委託股票代號

            int nBidAsk = 0;			//0買-1賣出-2無券賣出;出清現賣-資賣-券買	
            int nQty = 0;

            int nClearAllFlag = 0;			//*clear all flag
            string strClearCancelTime = "";	//*clear time
            int nFinalClearFlag = 0;		//*Final Clear flag
            string strClearAllOrderPrice = "";	//出清委託價	
            //當沖條件//
            string strOrderPrice = "";			//委託價
            int nTakeProfitFlag = 0;			//停利Flag
            int nRDOTPPercent = 0;				//停利類型
            string strTPPercent = "";			//停利百分比
            string strTPTrigger = "";			//停利觸發價
            int nRDTPMarketPriceType = 0;		//停利委託價方式=1:市價 2:限價 3:限價委託價
            string strTPOrderPrice = "";		//停利委託價
            int nStopLossFlag = 0;				//停損Flag
            int nRDOSLPercent = 0;				//停損類型
            string strSLPercent = "";			//停損百分比值
            string strSLTrigger = "";			//停損觸發價
            int nRDSLMarketPriceType = 0;		//停損委託價方式=1:市價 2:限價 3:限價委託價
            string strSLOrderPrice = "";		//停損委託價	
            int nClearAllPriceType = 0;			//出清委託價方式=1:市價 2:限價

            int nOrderPriceCond = 0, nOrderPriceType = 0, nSLCond = 0, nTPCond = 0, nClear = 0;

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

            double dOrderPrice = 0.0;
            if (double.TryParse(txtOrderPrice.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = txtOrderPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (Box_OrderCon.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場委託價條件(R/I/F)");
                return;
            }
            switch (Box_OrderCon.SelectedIndex)
            {
                case 0:
                    nOrderPriceCond = 0;
                    break;
                case 1:
                    nOrderPriceCond = 3;
                    break;
                case 2:
                    nOrderPriceCond = 4;
                    break;
            }

            if (Box_OrderPriceType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場委託價類別(1市價/2限價)");
                return;
            }
            switch (Box_OrderPriceType.SelectedIndex) //[-20200820-]
            {
                case 0:
                    nOrderPriceType = 1;
                    break;
                case 1:
                    nOrderPriceType = 2;
                    break;
            }

            bool bAsync = false;
            bAsync = DayTradeAsyncFlag.SelectedIndex == 0 ? true : false;

            int nDT_MITFlag = 0;
            int nDT_MITDir = 0;
            double dDT_TriggerPrice = 0.0;
            string strDTTrigger = "";
            string strDTBase = "";
            if (DT_MITFlagBox.Checked)
            {
                nDT_MITFlag = 1;
                if (double.TryParse(DT_MIT_TriggerPrice.Text.Trim(), out dDT_TriggerPrice) == false)
                {
                    MessageBox.Show("進場MIT觸發價請輸入數字");
                    return;
                }
                strDTTrigger = DT_MIT_TriggerPrice.Text.Trim();

                if (DT_MIT_TDirBox.SelectedIndex < 0)
                {
                    MessageBox.Show("進場MIT觸價方向請選擇至少一個");
                    return;
                }
                nDT_MITDir = DT_MIT_TDirBox.SelectedIndex + 1;

                if (double.TryParse(DT_MIT_BASEPrice.Text.Trim(), out dDT_TriggerPrice) == false)
                {
                    MessageBox.Show("進場MIT市價請輸入數字");
                    return;
                }
                strDTBase = DT_MIT_BASEPrice.Text.Trim();

            }
            else if (!(DT_MITFlagBox.Checked))
            {
                nDT_MITFlag = 0;
                strDTTrigger = "0";
                nDT_MITDir = 0;
                strDTBase = "0";

            }

            if (ckBoxLoss.Checked)
            {

                double dLossPercent = 0.0;
                double dTriggerLoss = 0.0;
                nStopLossFlag = 1;

                if (Box_SLCond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇停損委託條件(R/I/F)");
                    return;
                }
                switch (Box_SLCond.SelectedIndex)
                {
                    case 0:
                        nSLCond = 0;
                        break;
                    case 1:
                        nSLCond = 3;
                        break;
                    case 2:
                        nSLCond = 4;
                        break;
                }

                if (radioLoss.Checked)
                {// 1:漲跌幅 0:觸發價
                    if (double.TryParse(txtLossPercent.Text.Trim(), out dLossPercent) == false)
                    {
                        MessageBox.Show("停損跌幅請輸入數字");
                        return;
                    }
                    nRDOSLPercent = 1;
                    strSLPercent = txtLossPercent.Text.Trim();
                }
                else if (radioTriggerLoss.Checked)
                {
                    nRDOSLPercent = 0;
                    if (double.TryParse(txtTriggerLoss.Text.Trim(), out dTriggerLoss) == false)
                    {
                        MessageBox.Show("停損觸發價請輸入數字");
                        return;
                    }
                    strSLTrigger = txtTriggerLoss.Text.Trim();
                }
                if (radioOrderLoss.Checked)
                {//flag 0:None 1:市價 2:限價(以選定漲跌停為主)    8:市價           

                    nRDSLMarketPriceType = 2;
                    if (double.TryParse(txtLossOrderPrice.Text.Trim(), out dTriggerLoss) == false)
                    {
                        MessageBox.Show("停損委託限價請輸入數字");
                        return;
                    }
                    strSLOrderPrice = txtLossOrderPrice.Text.Trim();
                }
                else if (radioOrderTypeLossM.Checked)
                {
                    nRDSLMarketPriceType = 1;
                }
            }
            else
                nStopLossFlag = 0;


            double dProfitPercent = 0.0;
            double dTriggerProfit = 0.0;
            if (ckboxProfit.Checked)
            {
                nTakeProfitFlag = 1;

                if (Box_TPCond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇停利委託條件(R/I/F)");
                    return;
                }
                switch (Box_TPCond.SelectedIndex)
                {
                    case 0:
                        nTPCond = 0;
                        break;
                    case 1:
                        nTPCond = 3;
                        break;
                    case 2:
                        nTPCond = 4;
                        break;
                }

                if (radioProfit.Checked)
                {// 1:漲跌幅 0:觸發價
                    nRDOTPPercent = 1;

                    if (double.TryParse(txtProfitPercent.Text.Trim(), out dProfitPercent) == false)
                    {
                        MessageBox.Show("停利漲幅請輸入數字");
                        return;
                    }
                    strTPPercent = txtProfitPercent.Text.Trim();
                }
                else if (radioTriggerProfit.Checked)
                {
                    nRDOTPPercent = 0;
                    if (double.TryParse(txtTriggerProfit.Text.Trim(), out dTriggerProfit) == false)
                    {
                        MessageBox.Show("停利觸發價請輸入數字");
                        return;
                    }
                    strTPTrigger = txtTriggerProfit.Text.Trim();
                }
                if (radioOrderProfit.Checked)
                {//flag 0:None 8:市價 2:限價(以選定漲跌停為主)               
                    nRDTPMarketPriceType = 2;

                    if (double.TryParse(txtProfitOrderPrice.Text.Trim(), out dTriggerProfit) == false)
                    {
                        MessageBox.Show("停利委託限價請輸入數字");
                        return;
                    }
                    strTPOrderPrice = txtProfitOrderPrice.Text.Trim();
                }
                else if (RadioOrderTypeProfitM.Checked)
                {
                    nRDTPMarketPriceType = 1;
                }
            }
            else
                nTakeProfitFlag = 0;



            if (CkBoxClear1.Checked)
            {
                nClearAllFlag = 1;
                strClearCancelTime = boxHr1.Text.Trim() + boxMin1.Text.Trim();

                if (Box_ClearCond1.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇出清委託條件(R/I/F)");
                    return;
                }
                switch (Box_ClearCond1.SelectedIndex)
                {
                    case 0:
                        nClear = 0;
                        break;
                    case 1:
                        nClear = 3;
                        break;
                    case 2:
                        nClear = 4;
                        break;
                }

                if (radioOrderClear1.Checked)
                {
                    nClearAllPriceType = 2;

                    if (double.TryParse(txtClearOrder.Text.Trim(), out dTriggerProfit) == false)
                    {
                        MessageBox.Show("出清委託限價請輸入數字");
                        return;
                    }
                    strClearAllOrderPrice = txtClearOrder.Text.Trim();
                }
                else if (radioClearTypeM.Checked)
                {
                    nClearAllPriceType = 1;
                }
                //pStockOrder.sTimeClearMarketPrice = (short)nClearAllPriceType;
            }
            else
                nClearAllFlag = 0;



            if (ckBoxFinal.Checked)
                nFinalClearFlag = 1;
            else
                nFinalClearFlag = 0;

            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nQty = nQty;

            pStockOrder.bstrOrderPrice = strOrderPrice;

            pStockOrder.nInnerOrderIsMIT = nDT_MITFlag;
            pStockOrder.bstrMITTriggerPrice = strDTTrigger;
            pStockOrder.nMITDir = nDT_MITDir;
            pStockOrder.bstrMITDealPrice = strDTBase;

            pStockOrder.nTakeProfitFlag = nTakeProfitFlag;
            pStockOrder.nRDOTPPercent = nRDOTPPercent;
            pStockOrder.bstrTPPercent = strTPPercent;
            pStockOrder.bstrTPTrigger = strTPTrigger;
            pStockOrder.nRDTPMarketPriceType = nRDTPMarketPriceType;
            pStockOrder.bstrTPOrderPrice = strTPOrderPrice;

            pStockOrder.nStopLossFlag = nStopLossFlag;
            pStockOrder.nRDOSLPercent = nRDOSLPercent;
            pStockOrder.bstrSLPercent = strSLPercent;
            pStockOrder.bstrSLTrigger = strSLTrigger;
            pStockOrder.nRDSLMarketPriceType = nRDSLMarketPriceType;
            pStockOrder.bstrSLOrderPrice = strSLOrderPrice;

            pStockOrder.nClearAllFlag = nClearAllFlag;
            pStockOrder.bstrClearCancelTime = strClearCancelTime;
            pStockOrder.nClearAllPriceType = nClearAllPriceType;
            pStockOrder.bstrClearAllOrderPrice = strClearAllOrderPrice;


            //進場委託價條件;類別
            pStockOrder.nOrderPriceCond = nOrderPriceCond;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            //nSLCond, nTPCond, nClear;
            pStockOrder.nStopLossOrderCond = nSLCond;
            pStockOrder.nTakeProfitOrderCond = nTPCond;
            pStockOrder.nClearOrderCond = nClear;

            pStockOrder.nFinalClearFlag = nFinalClearFlag;

            if (OnStockStrategyDayTradeSignal != null)
            {
                OnStockStrategyDayTradeSignal(m_UserID, bAsync, pStockOrder);
            }
        }

        private void btnAllCleanOutAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;		//委託股票代號
            //int nTradeType;		    //交易種類,一律為整股
            int nOrderType;			//委託-0:現;1:資;2:券
            int nBidAsk;			//0買-1賣出-2無券賣出;出清現賣-資賣-券買	
            //string strOrderPrice = "";   //
            int nQty;
            //int nIsWarrant;			//權證
            int nClearAllFlag;			//*clear all flag
            string strClearCancelTime = "";	//*clear time
            int nFinalClearFlag;		//*Final Clear flag
            string strClearAllOrderPrice = "";	//出清委託價	
            //出清條件//
            int nLTEFlag;					//LTE flag
            string strLTETriggerPrice = "";		//LTE
            string strLTEOrderPrice = "";		//LTE
            int nLTEMarketPrice = 0;			//Market Price flag
            int nGTEFlag;					//BTE flag
            string strGTETriggerPrice = "";		//BTE
            string strGTEOrderPrice = "";		//BTE
            int nGTEMarketPrice = 0;			//BTE Market Price flag

            int nTimeClearMarketPrice = 0;		//Time Clear Market Price flag1:市價 2:限價

            int nGTECond = 0, nLTECond = 0, nClear = 0;

            if (txtStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo2.Text.Trim();

            if (boxBidAsk2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk2.SelectedIndex;

            if (boxOrderType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別");
                return;
            }

            if (boxOrderType.SelectedIndex == 0)
                nOrderType = boxOrderType.SelectedIndex;
            else
                nOrderType = boxOrderType.SelectedIndex + 2;


            //double dOrderPrice = 0.0;


            if (int.TryParse(txtQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            bool bAsync = false;
            bAsync = ClearAsyncFlag.SelectedIndex == 0 ? true : false;

            if (boxGTE.Checked)
            {

                double dOrderGTE = 0.0;

                nGTEFlag = 1;

                if (Box_GTECond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇大於委託條件(R/I/F)");
                    return;
                }
                switch (Box_GTECond.SelectedIndex)
                {
                    case 0:
                        nGTECond = 0;
                        break;
                    case 1:
                        nGTECond = 3;
                        break;
                    case 2:
                        nGTECond = 4;
                        break;
                }

                if (double.TryParse(txtGTEPrice.Text.Trim(), out dOrderGTE) == false)
                {
                    MessageBox.Show("請輸入成交價大於條件-數字");
                    return;
                }
                strGTETriggerPrice = txtGTEPrice.Text.Trim();

                if (radioGTEOrder.Checked)
                {// 1:市價 2:限價

                    nGTEMarketPrice = 2;

                    if (double.TryParse(txtGTEOrder.Text.Trim(), out dOrderGTE) == false)
                    {
                        MessageBox.Show("請輸入成交價大於條件-委託價");
                        return;
                    }

                    strGTEOrderPrice = txtGTEOrder.Text.Trim();
                }
                else if (radioGTEOrderTypeM.Checked)
                {
                    nGTEMarketPrice = 1;
                }
            }
            else
            {
                nGTEFlag = 0;
            }



            double dLTEPrice = 0.0;
            double dTriggerPrice = 0.0;
            if (boxLTE.Checked)
            {

                nLTEFlag = 1;

                if (Box_LTECond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇大於委託條件(R/I/F)");
                    return;
                }
                //nLTECond = Box_LTECond.SelectedIndex;
                switch (Box_LTECond.SelectedIndex)
                {
                    case 0:
                        nLTECond = 0;
                        break;
                    case 1:
                        nLTECond = 3;
                        break;
                    case 2:
                        nLTECond = 4;
                        break;
                }
                if (double.TryParse(txtLTEPrice.Text.Trim(), out dLTEPrice) == false)
                {
                    MessageBox.Show("請輸入成交價小於條件-數字");
                    return;
                }
                strLTETriggerPrice = txtLTEPrice.Text.Trim();

                if (radioLTEOrder.Checked)
                {

                    nLTEMarketPrice = 2;

                    if (double.TryParse(txtLTEOrder.Text.Trim(), out dTriggerPrice) == false)
                    {
                        MessageBox.Show("請輸入成交價小於條件-委託價");
                        return;
                    }

                    strLTEOrderPrice = txtLTEOrder.Text.Trim();

                }
                else if (radioLTEOrderTypeM.Checked)
                {
                    nLTEMarketPrice = 1;
                }

            }
            else
            {
                nLTEFlag = 0;
            }



            if (boxClearOut2.Checked)
            {
                nClearAllFlag = 1;
                strClearCancelTime = boxHr2.Text.Trim() + boxMin2.Text.Trim();
                string strHr2 = boxHr2.Text.Trim();
                int nHrCancelTime = int.Parse(strHr2) * 10000;
                string strMin2 = boxMin2.Text.Trim();
                int nMinCancelTime = int.Parse(strMin2) * 100;
                if (nHrCancelTime + nMinCancelTime > 132000)
                {
                    MessageBox.Show("選擇出清時間已超過13:20,請重新選擇");
                    return;
                }

                if (Box_ClearCond2.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇出清委託條件(R/I/F)");
                    return;
                }
                //nClear = Box_ClearCond2.SelectedIndex;

                switch (Box_ClearCond2.SelectedIndex)
                {
                    case 0:
                        nClear = 0;
                        break;
                    case 1:
                        nClear = 3;
                        break;
                    case 2:
                        nClear = 4;
                        break;
                }

                if (radioClearOrder2.Checked)
                {
                    nTimeClearMarketPrice = 2;
                    if (double.TryParse(txtClearOrder2.Text.Trim(), out dTriggerPrice) == false)
                    {
                        MessageBox.Show("出清委託價請輸入數字");
                        return;
                    }
                    strClearAllOrderPrice = txtClearOrder2.Text.Trim();
                }
                else if (radioClearType2M.Checked)
                {
                    nTimeClearMarketPrice = 1;
                }
            }
            else
            {
                nClearAllFlag = 0;
            }



            if (ckBoxFinal2.Checked)
            {
                nFinalClearFlag = 1;
            }
            else
            {
                nFinalClearFlag = 0;
            }

            STOCKSTRATEGYORDEROUT pStockOrder = new STOCKSTRATEGYORDEROUT();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderType = nOrderType;
            //pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.nQty = nQty;
            //pStockOrder.sIsWarrant = (short)nIsWarrant;



            pStockOrder.nLTEFlag = nLTEFlag;
            pStockOrder.bstrLTETriggerPrice = strLTETriggerPrice;
            pStockOrder.nLTEMarketPrice = nLTEMarketPrice;
            pStockOrder.bstrLTEOrderPrice = strLTEOrderPrice;

            pStockOrder.nGTEFlag = nGTEFlag;
            pStockOrder.bstrGTETriggerPrice = strGTETriggerPrice;
            pStockOrder.nGTEMarketPrice = nGTEMarketPrice;
            pStockOrder.bstrGTEOrderPrice = strGTEOrderPrice;

            pStockOrder.nClearAllFlag = nClearAllFlag;
            pStockOrder.bstrClearCancelTime = strClearCancelTime;
            pStockOrder.nClearAllPriceType = nTimeClearMarketPrice;//[-0306-]
            pStockOrder.bstrClearAllOrderPrice = strClearAllOrderPrice;

            pStockOrder.nGTEOrderCond = nGTECond;
            pStockOrder.nLTEOrderCond = nLTECond;
            pStockOrder.nClearOrderCond = nClear;

            pStockOrder.nFinalClearFlag = nFinalClearFlag;

            if (OnStockStrategyAllCleanOutSignal != null)
            {
                OnStockStrategyAllCleanOutSignal(m_UserID, bAsync, pStockOrder);
            }
        }

        private void btn_GetTSStategy_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }
            int nTypeReport;
            string strMarketType;
            string strKindReport;
            string strStartDate;

            if (txtMarketType.Text == "")
            {
                MessageBox.Show("請輸入市場類型");
                return;
            }
            strMarketType = txtMarketType.Text;
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

            if (OnTSStrategyReportSignal != null)
            {

                OnTSStrategyReportSignal(m_UserID, m_UserAccount, strMarketType, nTypeReport, strKindReport, strStartDate);
            }
        }

        private void radioLoss_CheckedChanged(object sender, EventArgs e)
        {
            if (radioLoss.Checked == true)
            {
                radioTriggerLoss.Checked = false;

            }

        }

        private void radioProfit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioProfit.Checked == true)
            {
                radioTriggerProfit.Checked = false;

            }

        }

        private void radioTriggerLoss_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTriggerLoss.Checked == true)
            {
                radioLoss.Checked = false;
                txtLossPercent.Text = "";
            }

        }

        private void radioOrderLoss_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioOrderTypeLoss_CheckedChanged(object sender, EventArgs e)
        {


        }





        private void radioTriggerProfit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTriggerProfit.Checked == true)
            {
                radioProfit.Checked = false;
            }

        }

        private void radioGTEOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (radioGTEOrder.Checked == true)
            {
                radioGTEOrderTypeM.Checked = false;

            }
        }

        private void radioGTEOrderType_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioLTEOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (radioLTEOrder.Checked == true)
            {
                radioLTEOrderTypeM.Checked = false;
            }
        }



        private void radioClearOrder_CheckedChanged(object sender, EventArgs e)
        {


        }



        private void boxBidAsk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void boxOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CancelTSOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }
            string strMarket;
            string strKeyNo;
            int nTradeType;


            if (txtSmartNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單序號");
                return;
            }
            strKeyNo = txtSmartNo.Text.Trim();

            if (KeyTypeBox.SelectedIndex == -1 || KeyTypeBox.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單類別(1當沖母單/2當沖未成交入場單/3當沖已進場單/4出清)");
                return;
            }

            int nTradeTypeOfDel = KeyTypeBox.SelectedIndex + 1;

            /* if ((nTradeTypeOfDel > 5))
             {
                 if (nTradeTypeOfDel < 9)           //[-20231212 -]
                 {
                     if (OnCancelTSStrategyOrderSignal != null)
                     {
                         OnCancelTSStrategyOrderSignal(m_UserID, true, m_UserAccount, strKeyNo, nTradeTypeOfDel);
                     }
                 }
             }*/
            if ((nTradeTypeOfDel == 6))//|| (nTradeTypeOfDel == 7)|| (nTradeTypeOfDel == 8)
            {

                if (OnCancelTSStrategyOrderSignal != null)
                {
                    OnCancelTSStrategyOrderSignal(m_UserID, true, m_UserAccount, strKeyNo, nTradeTypeOfDel);
                }

            }

            CANCELSTRATEGYORDER pOrder = new CANCELSTRATEGYORDER();
            pOrder.bstrFullAccount = m_UserAccount;

            if (KeyTypeBox.SelectedIndex >= 0 & KeyTypeBox.SelectedIndex <= 2)
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 11;
                pOrder.nDeleteType = nTradeTypeOfDel;
            }
            else if (KeyTypeBox.SelectedIndex == 3)
            {//ClearOut
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 17;
            }
            else if (KeyTypeBox.SelectedIndex == 4)//OCO//
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 3;
            }
            else if (KeyTypeBox.SelectedIndex == 6)//[-20240514-Add] MST
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 9;
            }
            else if (KeyTypeBox.SelectedIndex == 7)//[-20240514-Add] MIT
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 8;
            }
            else if (KeyTypeBox.SelectedIndex == 8)//[-20231212-Add] MIT長效單
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 8;
            }
            else if (KeyTypeBox.SelectedIndex == 9)//[-20231212-Add] MST長效單
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 9;
            }
            else if (KeyTypeBox.SelectedIndex == 10)//[-20231213-Add] AB單
            {
                if (box_MarketNoABDel.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇刪單市場別");
                    return;
                }
                pOrder.nMarket = (box_MarketNoABDel.SelectedIndex + 1);
                pOrder.nTradeKind = 10;
            }
            else if (KeyTypeBox.SelectedIndex == 11)//[-20231215-Add] CB單
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 27;
            }
            /*else if (KeyTypeBox.SelectedIndex == 12)//[-20231215-Add] MBA單
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 30;
            }
            else if (KeyTypeBox.SelectedIndex == 13)//[-20231218-Add] LLS單
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 31;
            }
            else if (KeyTypeBox.SelectedIndex == 14)//[-20240130-Add] 快速當沖OCO單
            {
                pOrder.nMarket = 1;
                pOrder.nTradeKind = 38;
            }*/

            pOrder.bstrParentSmartKey = txtSmartNo.Text;
            pOrder.bstrSeqNo = SeqNo.Text;
            pOrder.bstrOrderNo = OrderNo.Text;
            if (nTradeTypeOfDel == 3)
                pOrder.bstrSmartKey = SmartKeyOut.Text;
            else
                pOrder.bstrSmartKey = txtSmartNo.Text;
            pOrder.bstrSmartKeyOut = SmartKeyOut.Text;

            pOrder.bstrLongActionKey = LongActionKey.Text; //[-20231212-Add] 長效單


            /*if ((nTradeTypeOfDel > 0) && (nTradeTypeOfDel <= 14)) //[-20231212-]
             {
                 if (OnCancelTSStrategyOrderV1Signal != null)
                 {
                     OnCancelTSStrategyOrderV1Signal(m_UserID, pOrder);
                 }
            }*/
            if ((nTradeTypeOfDel != 6)) //[-20231212-]&& (nTradeTypeOfDel != 7)&& (nTradeTypeOfDel != 8)
            {
                if (OnCancelTSStrategyOrderV1Signal != null)
                {
                    OnCancelTSStrategyOrderV1Signal(m_UserID, pOrder);
                }
            }
        }


        private void btnSendFutureMITOrderAsync_Click(object sender, EventArgs e)
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;              //委託股票代號

            int nBidAsk = 0;			    //0買-1賣出-
            int nQty = 0;

            string strOrderPrice = "";			//委託價
            string strTrigger = "";
            string strDeal = "";
            int nOrderType = 0;
            int nOrderPriceCond = 0;
            int nOrderPriceType = 0;
            int nTriggerDir = 0;
            int nPreRisk = 0;
            int nLAFlag = 0, nLAType = 0;//是否為長效單、長效單觸發條件[-20231212-Add]
            string strLongEndDate = "";//長效單結束日期[-20231212-Add]

            if (TXT_MITStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = TXT_MITStockNo.Text.Trim();

            if (BoxBidAsk_MIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = BoxBidAsk_MIT.SelectedIndex;//[20210309]


            if (box_OrderTypeMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別");
                return;
            }

            if (box_OrderTypeMIT.SelectedIndex == 0)
                nOrderType = box_OrderTypeMIT.SelectedIndex;
            else
                nOrderType = box_OrderTypeMIT.SelectedIndex + 2;


            if (box_OrderTypeMIT.SelectedIndex == 3)
            {

                nOrderType = 8;
            }
            double dOrderPrice = 0.0, dTrigger = 0.0, dDeal = 0.0;
            if (double.TryParse(MITTxt_OrderPrice.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = MITTxt_OrderPrice.Text.Trim();

            if (double.TryParse(txtMITTrigger.Text.Trim(), out dTrigger) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = txtMITTrigger.Text.Trim();



            if (MITDirBox.SelectedIndex < 0)
            {
                MessageBox.Show("請輸入觸價方向");
                return;
            }
            nTriggerDir = MITDirBox.SelectedIndex + 1;



            if (MIT_OrderCond.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場委託價條件(R/I/F)");
                return;
            }
            switch (MIT_OrderCond.SelectedIndex)
            {
                case 0:
                    nOrderPriceCond = 0;
                    break;
                case 1:
                    nOrderPriceCond = 3;
                    break;
                case 2:
                    nOrderPriceCond = 4;
                    break;
            }
            if (MIT_OrderCond.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場委託價條件(R/I/F)");
                return;
            }
            switch (PreRiskFlag.SelectedIndex)
            {
                case 0:
                    nPreRisk = 0;
                    break;
                case 1:
                    nPreRisk = 1;
                    break;
            }

            if (boxPriceTypeMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceType = boxPriceTypeMIT.SelectedIndex + 1;

            if (int.TryParse(MITTxt_Qty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(Txt_MIT_DEAL.Text.Trim(), out dDeal) == false)
            {
                MessageBox.Show("當下市價請輸入數字");
                return;
            }
            strDeal = Txt_MIT_DEAL.Text.Trim();

            //[-20231212-Add]長效單條件
            if (boxLAFlag_MIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為長效單");
                return;
            }
            nLAFlag = boxLAFlag_MIT.SelectedIndex;

            if (boxLAFlag_MIT.SelectedIndex == 1)
            {
                if (txtLongEndDate_MIT.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入長效單結束日期");
                    return;
                }
            }
            strLongEndDate = txtLongEndDate_MIT.Text.Trim();

            if (boxLAFlag_MIT.SelectedIndex == 1)
            {
                if (boxLAType_MIT.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入長效單結束條件");
                    return;
                }
                else if (boxLAType_MIT.SelectedIndex == 0)
                    nLAType = (boxLAType_MIT.SelectedIndex + 1);
                else if (boxLAType_MIT.SelectedIndex == 1)
                    nLAType = (boxLAType_MIT.SelectedIndex + 2);
            }

            STOCKSTRATEGYORDERMIT pStockOrder = new STOCKSTRATEGYORDERMIT();

            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nQty = nQty;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.bstrTriggerPrice = strTrigger;
            pStockOrder.bstrDealPrice = strDeal;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderCond = nOrderPriceCond;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.nTriggerDir = nTriggerDir;
            pStockOrder.nPreRiskFlag = nPreRisk;
            pStockOrder.nLongActionFlag = nLAFlag;//[-20231212-Add]
            pStockOrder.bstrLongEndDate = strLongEndDate;//[-20231212-Add]
            pStockOrder.nLAType = nLAType;//[-20231212-Add]


            if (OnStockStrategyMITOrderSignal != null)
            {
                OnStockStrategyMITOrderSignal(m_UserID, true, pStockOrder);
            }

        }

        private void btnSendOCOOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;              //委託股票代號

            int nBidAskUp, nBidAskDown = 0;			    //0買-1賣出-
            int nQty = 0;

            string strOrderPriceUp = "";			//委託價
            string strOrderPriceDown = "";
            string strTriggerUp = "";
            string strTriggerDown = "";
            string strDeal = "";
            int nOrderTypeUp = 0, nOrderTypeDown = 0;
            int nOrderPriceCondUp = 0, nOrderPriceCondDown = 0;
            int nOrderPriceTypeUp = 0, nOrderPriceTypeDown = 0;

            if (txtStockNoOCO.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNoOCO.Text.Trim();

            if (boxBidAskOCO1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第一腳買賣別");
                return;
            }
            nBidAskUp = boxBidAskOCO1.SelectedIndex;

            if (boxBidAskOCO2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第二腳買賣別");
                return;
            }
            nBidAskDown = boxBidAskOCO2.SelectedIndex;

            if (BoxOrderTypeOCO1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第一腳委託別");
                return;
            }

            switch (BoxOrderTypeOCO1.SelectedIndex)//[-20240318-Add] 新增無券普賣
            {
                case 0:
                    nOrderTypeUp = 0;
                    break;
                case 1:
                    nOrderTypeUp = 3;
                    break;
                case 2:
                    nOrderTypeUp = 4;
                    break;
                case 3:
                    nOrderTypeUp = 8;
                    break;
            }


            if (BoxOrderTypeOCO2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第二腳委託別");
                return;
            }

            switch (BoxOrderTypeOCO2.SelectedIndex)//[-20240318-Add] 新增無券普賣
            {
                case 0:
                    nOrderTypeDown = 0;
                    break;
                case 1:
                    nOrderTypeDown = 3;
                    break;
                case 2:
                    nOrderTypeDown = 4;
                    break;
                case 3:
                    nOrderTypeDown = 8;
                    break;
            }

            if (boxPeriodOCO1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第一腳委託價條件(R/I/F)");
                return;
            }
            //nOrderPriceCondUp = boxPeriodOCO1.SelectedIndex;

            switch (boxPeriodOCO1.SelectedIndex)
            {
                case 0:
                    nOrderPriceCondUp = 0;
                    break;
                case 1:
                    nOrderPriceCondUp = 3;
                    break;
                case 2:
                    nOrderPriceCondUp = 4;
                    break;
            }

            if (boxPeriodOCO2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第二腳委託價條件(R/I/F)");
                return;
            }
            //nOrderPriceCondDown = boxPeriodOCO2.SelectedIndex;
            switch (boxPeriodOCO2.SelectedIndex)
            {
                case 0:
                    nOrderPriceCondDown = 0;
                    break;
                case 1:
                    nOrderPriceCondDown = 3;
                    break;
                case 2:
                    nOrderPriceCondDown = 4;
                    break;
            }

            if (BoxPriceTypeOCO1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第一腳委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceTypeUp = BoxPriceTypeOCO1.SelectedIndex + 1;

            if (BoxPriceTypeOCO2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇第二腳委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceTypeDown = BoxPriceTypeOCO2.SelectedIndex + 1;



            double dOrderPrice1, dOrderPrice2 = 0.0;
            if (double.TryParse(txtPriceOCO1.Text.Trim(), out dOrderPrice1) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPriceUp = txtPriceOCO1.Text.Trim();

            if (double.TryParse(txtPriceOCO2.Text.Trim(), out dOrderPrice2) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPriceDown = txtPriceOCO2.Text.Trim();

            strTriggerUp = txtTriggerOCO1.Text.Trim();
            strTriggerDown = txtTriggerOCO2.Text.Trim();



            if (int.TryParse(txtQtyOCO.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            STOCKSTRATEGYORDEROCO pStockOrder = new STOCKSTRATEGYORDEROCO();

            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySellUp = nBidAskUp;
            pStockOrder.nBuySellDown = nBidAskDown;
            pStockOrder.nQty = nQty;

            pStockOrder.bstrOrderPrice = strOrderPriceUp;
            pStockOrder.bstrOrderPrice2 = strOrderPriceDown;
            pStockOrder.bstrTriggerUp = strTriggerUp;
            pStockOrder.bstrTriggerDown = strTriggerDown;

            pStockOrder.nOrderTypeUp = nOrderTypeUp;
            pStockOrder.nOrderCondUp = nOrderPriceCondUp;
            pStockOrder.nOrderPriceTypeUp = nOrderPriceTypeUp;
            pStockOrder.nOrderTypeDown = nOrderTypeDown;
            pStockOrder.nOrderCondDown = nOrderPriceCondDown;
            pStockOrder.nOrderPriceTypeDown = nOrderPriceTypeDown;


            if (OnStockStrategyOCOOrderSignal != null)
            {
                OnStockStrategyOCOOrderSignal(m_UserID, true, pStockOrder);
            }
        }

        private void BoxPriceTypeOCO1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxPriceTypeOCO1.SelectedIndex == 0)
                txtPriceOCO1.Text = "0";
            else
            {
                if (txtPriceOCO1.Text == "0")
                    txtPriceOCO1.Text = "";
            }
        }

        private void BoxPriceTypeOCO2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxPriceTypeOCO2.SelectedIndex == 0)
                txtPriceOCO2.Text = "0";
            else
            {
                if (txtPriceOCO2.Text == "0")
                    txtPriceOCO2.Text = "";
            }
        }

        private void btnMSTOrderAsync_Click(object sender, EventArgs e)
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;		        //委託股票代號

            int nBidAsk, nPrime = 0;			    //0買-1賣出-
            int nAmt = 0;
            string strDealPrice = "", strOrderPrice = "", strMovePoint = "", strStartPrice = "";
            int nOrderType = 0, nPriceCond = 0, nOrderPriceType = 0, nMovePoint = 0, nTriggerMethod = 0, nTriggerDir = 0;
            int nLAFlag = 0, nLAType = 0;//是否為長效單、長效單觸發條件[-20231211-Add]
            string strLongEndDate = "";//長效單結束日期[-20231211-Add]

            if (txtStockNoMST.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNoMST.Text.Trim();

            if (boxPrimeMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市上櫃興櫃");
                return;
            }
            nPrime = boxPrimeMST.SelectedIndex;


            if (boxBuySellMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBuySellMST.SelectedIndex;


            //0現;1融資;2融券;3無券賣
            if (boxOrderTypeMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }


            if (boxOrderTypeMST.SelectedIndex == 0)
                nOrderType = boxOrderTypeMST.SelectedIndex;
            else
                nOrderType = boxOrderTypeMST.SelectedIndex + 2;



            if (boxOrderTypeMST.SelectedIndex == 3)
                nOrderType = 8;

            if (boxPeriodMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(R/I/F)");
                return;
            }
            switch (boxPeriodMST.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;

                case 1:
                    nPriceCond = 3;
                    break;
                case 2:
                    nPriceCond = 4;
                    break;

            }

            if (boxFlagMST.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceType = boxFlagMST.SelectedIndex + 1;

            double dDealPrice = 0.0, dOrderPrice = 0.0, dStartPrice = 0.0;


            if (double.TryParse(txtPriceMST.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = txtPriceMST.Text.Trim();

            if (int.TryParse(txtMovingPoint.Text.Trim(), out nMovePoint) == false)
            {
                MessageBox.Show("移動點數請輸入數字");
                return;
            }
            strMovePoint = txtMovingPoint.Text.Trim();

            if (int.TryParse(txtQtyMST.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }
            //[add-202105-s]
            if (MST_TriggerMethod.SelectedIndex == 0)
            {
                if (double.TryParse(txtBaseMST.Text.Trim(), out dDealPrice) == false)
                {
                    MessageBox.Show("選擇由市價啟動,觸價基準請輸入市價");
                    return;
                }
                strDealPrice = txtBaseMST.Text.Trim();
                strStartPrice = "0";
            }
            else if (MST_TriggerMethod.SelectedIndex == 1)
            {
                if (double.TryParse(txtBaseMST.Text.Trim(), out dStartPrice) == false)
                {
                    MessageBox.Show("選擇由自訂啟動,請輸入啟動價格");
                    return;
                }
                strDealPrice = "0";
                strStartPrice = txtBaseMST.Text.Trim();
            }

            if (MST_TriggerMethod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸價啟動方式:0由市價啟動或1自訂啟動");
                return;
            }
            nTriggerMethod = MST_TriggerMethod.SelectedIndex;

            if (MST_TriggerMethod.SelectedIndex == 1)
            {
                if (MST_Dir.SelectedIndex < 0)
                {
                    MessageBox.Show("自訂啟動,請選擇觸價方向");
                    return;
                }
                nTriggerDir = MST_Dir.SelectedIndex + 1;
            }
            else { nTriggerDir = 0; }

            //[-20231211-Add]長效單條件
            if (boxLAFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為長效單");
                return;
            }
            nLAFlag = boxLAFlag.SelectedIndex;

            if (boxLAFlag.SelectedIndex == 1)
            {
                if (txtLongEndDate.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入長效單結束日期");
                    return;
                }
            }
            strLongEndDate = txtLongEndDate.Text.Trim();

            if (boxLAFlag.SelectedIndex == 1)
            {
                if (boxLAType.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入長效單結束條件");
                    return;
                }
            }
            nLAType = (boxLAType.SelectedIndex + 1);


            //[add-202105-e]
            //STOCKSTRATEGYORDERMIT pMSTOrder = new STOCKSTRATEGYORDERMIT();
            STOCKSTRATEGYORDERMIT pStockOrder = new STOCKSTRATEGYORDERMIT();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nPrime = nPrime;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderCond = nPriceCond;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.bstrDealPrice = strDealPrice;
            pStockOrder.bstrMovePoint = strMovePoint;
            pStockOrder.nQty = nAmt;
            pStockOrder.bstrStartPrice = strStartPrice;//[add-202105]
            pStockOrder.nTriggerDir = nTriggerDir;//[add-202105]
            pStockOrder.nTriggerMethod = nTriggerMethod;//[add-202105]
            pStockOrder.nLongActionFlag = nLAFlag;//[add-20231211]
            pStockOrder.bstrLongEndDate = strLongEndDate;//[add-20231211]
            pStockOrder.nLAType = nLAType;//[add-20231211]

            if (OnStockStrategyMSTOrderSignal != null)
            {
                OnStockStrategyMSTOrderSignal(m_UserID, true, pStockOrder);
            }

        }

        private void MultiIOC_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;              //委託股票代號

            int nBidAsk, nPrime = 0;			    //0買-1賣出-
            int nTotQty, nOneAmt = 0;

            string strOrderPriceUp = "";			//委託價
            string strOrderPriceDown = "";

            int nOrderType = 0, nPriceCond = 0, nOrderPriceType = 0;

            if (txtStockNoMIOC.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNoMIOC.Text.Trim();

            if (boxPrimeMIOC.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市上櫃興櫃");
                return;
            }
            nPrime = boxPrimeMIOC.SelectedIndex;


            if (boxBuySellMIOC.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBuySellMIOC.SelectedIndex;


            //0現;1融資;2融券;3無券賣
            if (boxOrderTypeMIOC.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別");
                return;
            }

            if (boxOrderTypeMIOC.SelectedIndex == 0)
                nOrderType = boxOrderTypeMIOC.SelectedIndex;
            else
                nOrderType = boxOrderTypeMIOC.SelectedIndex + 2;

            if (boxOrderTypeMIOC.SelectedIndex == 3)
                nOrderType = 8;//無券//
                               /*if (boxPriceCondMIOC.SelectedIndex < 0)
                               {
                                   MessageBox.Show("請選擇委託價條件(R/I/F)");
                                   return;
                               }


                               switch (boxPriceCondMIOC.SelectedIndex)
                               {
                                   case 0:
                                      nPriceCond = 3;
                                       break;

                               }  */

            if (boxPriceTypeMIOC.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2委賣_委買價)");
                return;
            }
            nOrderPriceType = boxPriceTypeMIOC.SelectedIndex + 1;




            double dOrderPrice1, dOrderPrice2 = 0.0;
            if (double.TryParse(txtPriceUpMIOC.Text.Trim(), out dOrderPrice1) == false)
            {
                MessageBox.Show("委託價上限請輸入數字");
                return;
            }
            strOrderPriceUp = txtPriceUpMIOC.Text.Trim();

            if (double.TryParse(txtPriceDownMIOC.Text.Trim(), out dOrderPrice2) == false)
            {
                MessageBox.Show("委託價下限請輸入數字");
                return;
            }
            strOrderPriceDown = txtPriceDownMIOC.Text.Trim();

            if (int.TryParse(txtOneAmountMIOC.Text.Trim(), out nOneAmt) == false)
            {
                MessageBox.Show("單次委託量上限請輸入數字");
                return;
            }

            if (int.TryParse(txtTotQtyMIOC.Text.Trim(), out nTotQty) == false)
            {
                MessageBox.Show("委託量總量請輸入數字");
                return;
            }


            STOCKSTRATEGYORDERMIOC pStockOrder = new STOCKSTRATEGYORDERMIOC();

            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nPrime = nPrime;
            pStockOrder.nOneceQtyLimit = nOneAmt;
            pStockOrder.nTotalQty = nTotQty;

            pStockOrder.bstrOrderPriceUp = strOrderPriceUp;
            pStockOrder.bstrOrderPriceDown = strOrderPriceDown;


            pStockOrder.nOrderType = nOrderType;
            //pStockOrder.nOrderCond = nPriceCond;
            pStockOrder.nOrderPriceType = nOrderPriceType;



            if (OnStockStrategyMIOCOrderSignal != null)
            {
                OnStockStrategyMIOCOrderSignal(m_UserID, true, pStockOrder);
            }

        }

        private void boxBuySellMIOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxBuySellMIOC.SelectedIndex == 0)
            {
                boxPriceTypeMIOC.Items.Clear();
                boxPriceTypeMIOC.Items.Add("市價");
                boxPriceTypeMIOC.Items.Add("委賣價");
            }
            else if (boxBuySellMIOC.SelectedIndex == 1)
            {
                boxPriceTypeMIOC.Items.Clear();
                boxPriceTypeMIOC.Items.Add("市價");
                boxPriceTypeMIOC.Items.Add("委買價");

            }

        }

        private void MST_TriggerMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MST_TriggerMethod.SelectedIndex == 0)
            {
                MST_Dir.Enabled = false;

            }
            else
            {
                MST_Dir.Enabled = true;
                txtBaseMST.Enabled = true;

            }

        }

        private void DT_MIT_CKBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DT_MITFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DT_MITFlagBox.Checked == false)
            {
                DT_MIT_BASEPrice.Enabled = false;
                DT_MIT_TDirBox.Enabled = false;
                DT_MIT_TriggerPrice.Enabled = false;

            }
            if (DT_MITFlagBox.Checked == true)
            {
                DT_MIT_BASEPrice.Enabled = true;
                DT_MIT_TDirBox.Enabled = true;
                DT_MIT_TriggerPrice.Enabled = true;

            }

        }

        /*private void btnSendStockMMITOrderAsync_Click(object sender, EventArgs e)
        {

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;		        //委託股票代號

            int nBidAsk;	    //0買-1賣出-
            int nAmt = 0;
            string strOrderPrice = "", strStartPrice = "", strTrigger = "", strDeal = "";
            int nOrderType = 0, nPriceCond = 0, nOrderPriceType = 0,  nTriggerMethod = 0, nTriggerDir = 0;
            int nLAFlag = 0, nLAType = 0;//是否為長效單、長效單觸發條件
            string strLongEndDate = "";//長效單結束日期
            int nBestQtyFlag = 0, nBestQtyDir = 0, nBestQty = 0;//內外盤委託條件
            double dTrigger = 0.0, dDeal = 0.0;

            if (StockNoMMIT.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNoMMIT.Text.Trim();


            if (box_BidAskMMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = box_BidAskMMIT.SelectedIndex;


            //0現;1融資;2融券;3無券賣
            if (box_OrderTypeMMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }
            if (box_OrderTypeMMIT.SelectedIndex == 0)
                nOrderType = box_OrderTypeMMIT.SelectedIndex;
            else
                nOrderType = box_OrderTypeMMIT.SelectedIndex + 2;

            if (box_OrderTypeMMIT.SelectedIndex == 3)
                nOrderType = 8;


            if (box_OrderCondMMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(R/I/F)");
                return;
            }
            switch (box_OrderCondMMIT.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;

                case 1:
                    nPriceCond = 3;
                    break;
                case 2:
                    nPriceCond = 4;
                    break;

            }

            if (box_PriceTypeMMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceType = box_PriceTypeMMIT.SelectedIndex + 1;

            if (double.TryParse(TriggerMMIT.Text.Trim(), out dTrigger) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = TriggerMMIT.Text.Trim();

            if (box_DirMMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請輸入觸價方向");
                return;
            }
            nTriggerDir = box_DirMMIT.SelectedIndex + 1;


            double dOrderPrice = 0.0;

            if (double.TryParse(OrderPriceMMIT.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = OrderPriceMMIT.Text.Trim();


            if (int.TryParse(QtyMMIT.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (double.TryParse(DealMMIT.Text.Trim(), out dDeal) == false)
            {
                MessageBox.Show("當下市價請輸入數字");
                return;
            }
            strDeal = DealMMIT.Text.Trim();


            //長效單條件
            if (box_LAFlagMMIT.SelectedIndex < 0)  
            {
                MessageBox.Show("請選擇是否為長效單");
                return;
            }
            nLAFlag = box_LAFlagMMIT.SelectedIndex;

            if (boxLAFlag.SelectedIndex == 1)
            {
                if (LongEndDateMMIT.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入長效單結束日期");
                    return;
                }
            }
            strLongEndDate = LongEndDateMMIT.Text.Trim();

            if (boxLAFlag.SelectedIndex == 1)
            {
                if (box_LATypeMMIT.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入長效單結束條件");
                    return;
                }
            }
            if (box_LATypeMMIT.SelectedIndex == 0)
                nLAType = (box_LATypeMMIT.SelectedIndex + 1);
            else if (box_LATypeMMIT.SelectedIndex == 1)
                nLAType = (box_LATypeMMIT.SelectedIndex + 2);

            //內外盤委託量條件
            if (box_BestQtyFlagMMIT.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為內外盤委託");
                return;
            }
            nBestQtyFlag = box_BestQtyFlagMMIT.SelectedIndex;

            if (box_BestQtyFlagMMIT.SelectedIndex == 1)
            {
                if (box_BestQtyDirMMIT.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入內外盤委託觸發方向");
                    return;
                }
            }
            nBestQtyDir = (box_BestQtyDirMMIT.SelectedIndex + 1);

            if (box_BestQtyFlagMMIT.SelectedIndex == 1)
            {
                if (int.TryParse(BestQtyMMIT.Text.Trim(), out nBestQty) == false)
                {
                    MessageBox.Show("內外盤委託量請輸入數字");
                    return;
                }
            }


            STOCKSTRATEGYORDERMIT pStockOrder = new STOCKSTRATEGYORDERMIT();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderCond = nPriceCond;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.bstrTriggerPrice = strTrigger;
            pStockOrder.bstrDealPrice = strDeal;
            pStockOrder.nQty = nAmt;
            pStockOrder.nTriggerDir = nTriggerDir;//[add-202105]
            pStockOrder.nLongActionFlag = nLAFlag;//[add-20231211]
            pStockOrder.bstrLongEndDate = strLongEndDate;//[add-20231211]
            pStockOrder.nLAType = nLAType;//[add-20231211]
            pStockOrder.nBestQtyFlag = nBestQtyFlag;
            pStockOrder.nBestQtyDir = nBestQtyDir;
            pStockOrder.nBestQty = nBestQty;


            if (OnStockStrategyMMITOrderSignal != null)
            {
                OnStockStrategyMMITOrderSignal(m_UserID, true, pStockOrder);
            }

        }*/

        private void btnSendStockABOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNoAB = "", strExchangeNo = "", strDeal = "", strStockNo2AB = "";	//委託股票代號_A、A商品交易所代碼、成交價、股票代號_B
            string strOrderPrice = "";                      //委託價
            int nPriceCond = 0, nOrderType = 0, nMarketNo = 0, nPrime = 0;                   //RIF、現資券、市場編號、上市櫃
            double dDeal = 0.0;                             //市價_A
            int nOrderPriceType = 0, nAmt = 0, nBidAsk = 0;	    //價格別、委託輛、0買-1賣出-
            int nTriggerDir = 0, nReserved = 0;  //是否為長效單、長效單觸發條件nLAFlag = 0, nLAType = 0, 
            string strTrigger = "";    //長效單結束日期strLongEndDate = "",
            double dTrigger = 0.0;

            //A商品
            if (StockNoAB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入A商品代碼");
                return;
            }
            strStockNoAB = StockNoAB.Text.Trim();

            if (box_MarketNoAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇市場編號");
                return;
            }
            nMarketNo = (box_MarketNoAB.SelectedIndex + 1);

            if (ExchangeNoAB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入A商品交易所代碼");
                return;
            }
            strExchangeNo = ExchangeNoAB.Text.Trim();

            if (double.TryParse(DealPriceAB.Text.Trim(), out dDeal) == false)
            {
                MessageBox.Show("市價請輸入數字");
                return;
            }
            strDeal = DealPriceAB.Text.Trim();

            if (double.TryParse(TriggerAB.Text.Trim(), out dTrigger) == false)
            {
                MessageBox.Show("觸發價請輸入數字");
                return;
            }
            strTrigger = TriggerAB.Text.Trim();

            if (box_DirAB.SelectedIndex < 0)
            {
                MessageBox.Show("請輸入觸價方向");
                return;
            }
            nTriggerDir = (box_DirAB.SelectedIndex + 1);

            //B商品
            if (StockNo2AB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入B商品代碼");
                return;
            }
            strStockNo2AB = StockNo2AB.Text.Trim();

            if (PrimeAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市上櫃");
                return;
            }
            nPrime = PrimeAB.SelectedIndex;

            //0現;1融資;2融券;3無券賣
            if (box_OrderTypeAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }
            if (box_OrderTypeAB.SelectedIndex == 0)
                nOrderType = box_OrderTypeAB.SelectedIndex;
            else
                nOrderType = box_OrderTypeAB.SelectedIndex + 2;

            if (box_OrderTypeAB.SelectedIndex == 3)
                nOrderType = 8;


            if (box_OrderCondAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(R/I/F)");
                return;
            }
            switch (box_OrderCondAB.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;

                case 1:
                    nPriceCond = 3;
                    break;
                case 2:
                    nPriceCond = 4;
                    break;

            }

            if (box_BidAskAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = box_BidAskAB.SelectedIndex;

            if (int.TryParse(QtyAB.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (box_PriceFlagAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceType = (box_PriceFlagAB.SelectedIndex + 1);


            double dOrderPrice = 0.0;

            if (double.TryParse(OrderPriceAB.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = OrderPriceAB.Text.Trim();
            /*
            //長效單條件
            if (box_LAFlagAB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否為長效單");
                return;
            }
            nLAFlag = box_LAFlagAB.SelectedIndex;

            if (box_LAFlagAB.SelectedIndex == 1)
            {
                if (LongEndDateAB.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入長效單結束日期");
                    return;
                }
            }
            strLongEndDate = LongEndDateAB.Text.Trim();

            if (box_LAFlagAB.SelectedIndex == 1)
            {
                if (box_LATypeAB.SelectedIndex < 0)
                {
                    MessageBox.Show("請輸入長效單結束條件");
                    return;
                }
            }
            if (box_LATypeAB.SelectedIndex == 0)
                nLAType = (box_LATypeAB.SelectedIndex + 1);
            else if (box_LATypeAB.SelectedIndex == 1)
                nLAType = (box_LATypeAB.SelectedIndex + 2);

            if (box_MarketNoAB.SelectedIndex == 1)
                nReserved = box_ReservedAB.SelectedIndex;
            else
                nReserved = 0;
            */


            STOCKSTRATEGYORDERMIT pStockOrder = new STOCKSTRATEGYORDERMIT();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo2 = strStockNoAB;
            pStockOrder.nMarketNo = nMarketNo;
            pStockOrder.bstrExchangeNo = strExchangeNo;
            pStockOrder.bstrStartPrice = strDeal;
            pStockOrder.bstrTriggerPrice = strTrigger;
            pStockOrder.nTriggerDir = nTriggerDir;
            pStockOrder.bstrStockNo = strStockNo2AB;
            pStockOrder.nPrime = nPrime;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderCond = nPriceCond;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nQty = nAmt;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            /*
            pStockOrder.nLongActionFlag = nLAFlag;
            pStockOrder.bstrLongEndDate = strLongEndDate;
            pStockOrder.nLAType = nLAType;
            */
            pStockOrder.nReserved = nReserved;


            if (OnStockStrategyABOrderSignal != null)
            {
                OnStockStrategyABOrderSignal(m_UserID, true, pStockOrder);
            }
        }

        private void btnSendStockCBOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo = "", strOrderPrice = "", strDealPrice = "", strBuyPrice = "", strSellPrice = "";
            string strTick = "", strUpDown = "", strSumQty = "", strPreQty = "", strTime = "";
            int nOrderPriceType = 0, nPriceCond = 0, nOrderType = 0, nBidAsk = 0;
            int nAmt = 0, nDealPriceFlag = 0, nDealPriceDir = 0, nBuyPriceFlag = 0, nBuyPriceDir = 0, nSellPriceFlag = 0, nSellPriceDir = 0, nClearFlag = 0;
            int nTickFlag = 0, nTickDir = 0, nUpDownFlag = 0, nUpDownDir = 0, nSumQtyFlag = 0, nSumQtyDir = 0, nPreQtyFlag = 0, nPreQtyDir = 0, nTimeFlag = 0;





            //商品
            if (StockNoCB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNoCB.Text.Trim();

            //0現;1融資;2融券;3無券賣
            if (box_OrderTypeCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }
            if (box_OrderTypeCB.SelectedIndex == 0)
                nOrderType = box_OrderTypeCB.SelectedIndex;
            else
                nOrderType = box_OrderTypeCB.SelectedIndex + 2;

            if (box_OrderTypeCB.SelectedIndex == 3)
                nOrderType = 8;

            if (box_OrderCondCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(R/I/F)");
                return;
            }
            switch (box_OrderCondCB.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;

                case 1:
                    nPriceCond = 3;
                    break;
                case 2:
                    nPriceCond = 4;
                    break;

            }

            if (box_BidAskCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = box_BidAskCB.SelectedIndex;

            if (box_PriceFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceType = (box_PriceFlagCB.SelectedIndex + 1);

            double dOrderPrice = 0.0;

            if (double.TryParse(OrderPriceCB.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = OrderPriceCB.Text.Trim();

            if (int.TryParse(QtyCB.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (box_DealPriceFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為成交價");
                return;
            }
            nDealPriceFlag = box_DealPriceFlagCB.SelectedIndex;

            if (box_DealPriceDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇成交價觸發方向");
                return;
            }
            nDealPriceDir = box_DealPriceDirCB.SelectedIndex;

            double dDealPrice = 0.0;
            if (double.TryParse(DealPriceCB.Text.Trim(), out dDealPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = DealPriceCB.Text.Trim();

            if (box_BuyPriceFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為委買價");
                return;
            }
            nBuyPriceFlag = box_BuyPriceFlagCB.SelectedIndex;

            if (box_BuyPriceDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委買價觸發方向");
                return;
            }
            nBuyPriceDir = box_BuyPriceDirCB.SelectedIndex;

            double dBuyPrice = 0.0;
            if (double.TryParse(BuyPriceCB.Text.Trim(), out dBuyPrice) == false)
            {
                MessageBox.Show("委買價請輸入數字");
                return;
            }
            strBuyPrice = BuyPriceCB.Text.Trim();

            if (box_SellPriceFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為委賣價");
                return;
            }
            nSellPriceFlag = box_SellPriceFlagCB.SelectedIndex;

            if (box_SellPriceDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委賣價觸發方向");
                return;
            }
            nSellPriceDir = box_SellPriceDirCB.SelectedIndex;

            double dSellPrice = 0.0;
            if (double.TryParse(SellPriceCB.Text.Trim(), out dSellPrice) == false)
            {
                MessageBox.Show("委賣價請輸入數字");
                return;
            }
            strSellPrice = SellPriceCB.Text.Trim();

            if (box_TickFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為Tick");
                return;
            }
            nTickFlag = box_TickFlagCB.SelectedIndex;

            if (box_TickDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇Tick觸發方向");
                return;
            }

            if (box_TickDirCB.SelectedIndex > 2)
            {
                nTickDir = (box_TickDirCB.SelectedIndex - 2);
            }
            else
                nTickDir = box_TickDirCB.SelectedIndex;


            double dTick = 0.0;
            if (double.TryParse(TickCB.Text.Trim(), out dTick) == false)
            {
                MessageBox.Show("Tick請輸入數字");
                return;
            }
            strTick = TickCB.Text.Trim();

            if (box_UDFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為漲跌幅");
                return;
            }
            nUpDownFlag = box_UDFlagCB.SelectedIndex;

            if (box_UDDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇漲跌幅觸發方向");
                return;
            }

            if (box_UDDirCB.SelectedIndex > 2)
            {
                nUpDownDir = (box_UDDirCB.SelectedIndex - 2);
            }
            else
            {
                nUpDownDir = box_UDDirCB.SelectedIndex;
            }


            double dUpDown = 0.0;
            if (double.TryParse(UDCB.Text.Trim(), out dUpDown) == false)
            {
                MessageBox.Show("漲跌幅請輸入數字");
                return;
            }
            strUpDown = UDCB.Text.Trim();

            if (box_PreQtyFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為單量");
                return;
            }
            nPreQtyFlag = box_PreQtyFlagCB.SelectedIndex;

            if (box_PreQtyDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇單量觸發方向");
                return;
            }
            nPreQtyDir = box_PreQtyDirCB.SelectedIndex;

            double dPreQty = 0.0;
            if (double.TryParse(PreQtyCB.Text.Trim(), out dPreQty) == false)
            {
                MessageBox.Show("單量請輸入數字");
                return;
            }
            strPreQty = PreQtyCB.Text.Trim();

            if (box_SumQFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為總量");
                return;
            }
            nSumQtyFlag = box_SumQFlagCB.SelectedIndex;

            if (box_SumQDirCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇總量觸發方向");
                return;
            }
            nSumQtyDir = box_SumQDirCB.SelectedIndex;

            double dSumQty = 0.0;
            if (double.TryParse(SumQCB.Text.Trim(), out dSumQty) == false)
            {
                MessageBox.Show("總量請輸入數字");
                return;
            }
            strSumQty = SumQCB.Text.Trim();

            if (box_TimeFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為自訂啟動時間");
                return;
            }
            nTimeFlag = box_TimeFlagCB.SelectedIndex;

            if (box_TimeFlagCB.SelectedIndex == 1)
            {
                if (TimeCB.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入自訂啟動時間");
                    return;
                }
            }
            strTime = TimeCB.Text.Trim();

            if (box_ClearFlagCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為全部成立");
                return;
            }
            nClearFlag = box_ClearFlagCB.SelectedIndex;

            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderPriceCond = nPriceCond;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nQty = nAmt;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.nInnerOrderIsMIT = nDealPriceFlag;
            pStockOrder.nMITDir = nDealPriceDir;
            pStockOrder.bstrMITTriggerPrice = strDealPrice;
            pStockOrder.nTakeProfitFlag = nBuyPriceFlag;
            pStockOrder.nTPDir = nBuyPriceDir;
            pStockOrder.bstrTPOrderPrice = strBuyPrice;
            pStockOrder.nStopLossFlag = nSellPriceFlag;
            pStockOrder.nSLDir = nSellPriceDir;
            pStockOrder.bstrSLOrderPrice = strSellPrice;
            pStockOrder.nTickFlag = nTickFlag;
            pStockOrder.nTickDir = nTickDir;
            pStockOrder.bstrTick = strTick;
            pStockOrder.nUpDownFlag = nUpDownFlag;
            pStockOrder.nUpDownDir = nUpDownDir;
            pStockOrder.bstrSLPercent = strUpDown;
            pStockOrder.nPreQtyFlag = nPreQtyFlag;
            pStockOrder.nPreQtyDir = nPreQtyDir;
            pStockOrder.bstrPreQty = strPreQty;
            pStockOrder.nSumQtyFlag = nSumQtyFlag;
            pStockOrder.nSumQtyDir = nSumQtyDir;
            pStockOrder.bstrSumQty = strSumQty;
            pStockOrder.nClearAllFlag = nTimeFlag;
            pStockOrder.bstrClearCancelTime = strTime;
            pStockOrder.nFinalClearFlag = nClearFlag;


            if (OnStockStrategyCBOrderSignal != null)
            {
                OnStockStrategyCBOrderSignal(m_UserID, true, pStockOrder);
            }
        }

        /*private void btnSendStockMBAOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo = "", strSellPrice = "";
           
            int nSumQtyDir = 0, nPreQtyFlag = 0, nPreQtyDir = 0, nAmt = 0, nSellPriceFlag = 0, nSellPriceDir = 0, nClearFlag = 0;

            int nQtyDir = 0, nIsForceOrderFlag = 0, nIsVolumeFlag = 0, nIsDealCntFlag = 0, nOrderType = 0, nPriceCond = 0;
            string strdVolume = "", strDealCnt = "", strPreQty = "", strQtyPercent = "", strQtyLimit = "", strLimitPreQty = "";
            int nOrderPriceType = 0, nBidAsk = 0, nVolumeDuration = 0, nDealCntDuration = 0;
            //商品
            if (StockNoMBA.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNoMBA.Text.Trim();

            //0現;1融資;2融券;3無券賣
            if (box_OrderTypeMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }
            if (box_OrderTypeMBA.SelectedIndex == 0)
                nOrderType = box_OrderTypeMBA.SelectedIndex;
            else
                nOrderType = box_OrderTypeMBA.SelectedIndex + 2;

            if (box_OrderTypeMBA.SelectedIndex == 3)
                nOrderType = 8;

            if (box_OrderCondMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(ROD)");
                return;
            }
            switch (box_OrderCondMBA.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;
            }

            if (box_BidAskMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = box_BidAskMBA.SelectedIndex;

            if (box_PriceFlagMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價)");
                return;
            }
            nOrderPriceType = (box_PriceFlagMBA.SelectedIndex + 1);

            if (box_QtyDir.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託數量類別");
                return;
            }
            nQtyDir = (box_QtyDir.SelectedIndex + 1);

            if (int.TryParse(QtyMBA.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託總數請輸入數字");
                return;
            }
            if (box_QtyDir.SelectedIndex == 0)
            {
                if (QtyMBA.Text.Trim() == "")
                {
                    MessageBox.Show("委託數量類別為1，必須填委託總數");
                    return;
                }
            }

            double dQtyPercent = 0.0;
            if (double.TryParse(QtyPercentMBA.Text.Trim(), out dQtyPercent) == false)
            {
                MessageBox.Show("最佳委買賣量(%)請輸入數字");
                return;
            }
            if (box_QtyDir.SelectedIndex == 1)
            {
                if (QtyPercentMBA.Text.Trim() == "")
                {
                    MessageBox.Show("委託數量類別為2，必須填最佳委買賣量(%)");
                    return;
                }
                else
                    strQtyPercent = QtyPercentMBA.Text.Trim();
            }
            else
                strQtyPercent = QtyPercentMBA.Text.Trim();

            double dQtyLimit = 0.0;
            if (double.TryParse(QtyLimitMBA.Text.Trim(), out dQtyLimit) == false)
            {
                MessageBox.Show("委託量上限請輸入數字");
                return;
            }
            if (box_QtyDir.SelectedIndex == 1)
            {
                if (QtyLimitMBA.Text.Trim() == "")
                {
                    MessageBox.Show("委託數量類別為2，必須填委託量上限");
                    return;
                }
                else
                    strQtyLimit = QtyLimitMBA.Text.Trim();
            }
            else
                strQtyLimit = QtyLimitMBA.Text.Trim();


            double dLimitPreQtyMBA = 0.0;
            if (double.TryParse(LimitPreQtyMBA.Text.Trim(), out dLimitPreQtyMBA) == false)
            {
                MessageBox.Show("單次委託量最高上限請輸入數字");
                return;
            }
            if (box_QtyDir.SelectedIndex == 1)
            {
                if (LimitPreQtyMBA.Text.Trim() == "")
                {
                    MessageBox.Show("委託數量類別為2，必須填單次委託量最高上限");
                    return;
                }
                else
                    strLimitPreQty = LimitPreQtyMBA.Text.Trim();
            }
            else
                strLimitPreQty = LimitPreQtyMBA.Text.Trim();


            if (box_IsForceOrderMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為漲停鎖死即下單");
                return;
            }
            nIsForceOrderFlag = box_IsForceOrderMBA.SelectedIndex;

            if (box_IsVolumeMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為累積張數");
                return;
            }
            nIsVolumeFlag = box_IsVolumeMBA.SelectedIndex;


            if (int.TryParse(VolumeDurationMBA.Text.Trim(), out nVolumeDuration) == false)
            {
                MessageBox.Show("張數累積時間請輸入數字");
                return;
            }

            double dVolume = 0.0;
            if (double.TryParse(VolumeMBA.Text.Trim(), out dVolume) == false)
            {
                MessageBox.Show("累積張數請輸入數字");
                return;
            }
            strdVolume = VolumeMBA.Text.Trim();

            if (box_IsDealCntMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為成交筆數");
                return;
            }
            nIsDealCntFlag = box_IsDealCntMBA.SelectedIndex;

            if (int.TryParse(DealCntDurationMBA.Text.Trim(), out nDealCntDuration) == false)
            {
                MessageBox.Show("成交筆數累積時間請輸入數字");
                return;
            }

            double dDealCnt = 0.0;
            if (double.TryParse(DealCnt.Text.Trim(), out dDealCnt) == false)
            {
                MessageBox.Show("成交筆數請輸入數字");
                return;
            }
            strDealCnt = DealCnt.Text.Trim();

            if (box_PreQtyFlagMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為單量");
                return;
            }
            nPreQtyFlag = box_PreQtyFlagMBA.SelectedIndex;

            if (box_PreQtyDirMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇單量觸發方向");
                return;
            }
            nPreQtyDir = box_PreQtyDirMBA.SelectedIndex;

            double dPreQty = 0.0;
            if (double.TryParse(PreQtyMBA.Text.Trim(), out dPreQty) == false)
            {
                MessageBox.Show("單量請輸入數字");
                return;
            }
            strPreQty = PreQtyMBA.Text.Trim();

            if (box_IsAskQtyMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為最佳委賣量");
                return;
            }
            nSellPriceFlag = box_IsAskQtyMBA.SelectedIndex;

            if (box_AskQtyDirMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇最佳委賣量觸發方向");
                return;
            }
            nSellPriceDir = box_AskQtyDirMBA.SelectedIndex;

            double dAskQty = 0.0;
            if (double.TryParse(AskQtyMBA.Text.Trim(), out dAskQty) == false)
            {
                MessageBox.Show("最佳委賣量請輸入數字");
                return;
            }
            strSellPrice = AskQtyMBA.Text.Trim();

            if (box_ClearFlagMBA.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為全部成立");
                return;
            }
            nClearFlag = box_ClearFlagMBA.SelectedIndex;



            

            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderPriceCond = nPriceCond;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.nMITDir = nQtyDir;
            pStockOrder.nQty = nAmt;
            pStockOrder.bstrSLPercent = strQtyPercent;
            pStockOrder.bstrMITDealPrice = strQtyLimit;
            pStockOrder.bstrMITTriggerPrice = strLimitPreQty;
            pStockOrder.nClearAllFlag = nIsForceOrderFlag;
            pStockOrder.nSumQtyFlag = nIsVolumeFlag;
            pStockOrder.nSumQtyDir = nVolumeDuration;
            pStockOrder.bstrSumQty = strdVolume;
            pStockOrder.nTickFlag = nIsDealCntFlag;
            pStockOrder.nTickDir = nDealCntDuration;
            pStockOrder.bstrTick = strDealCnt;
            pStockOrder.nPreQtyFlag = nPreQtyFlag;
            pStockOrder.nPreQtyDir = nPreQtyDir;
            pStockOrder.bstrPreQty = strPreQty;
            pStockOrder.nStopLossFlag = nSellPriceFlag;
            pStockOrder.nSLDir = nSellPriceDir;
            pStockOrder.bstrSLOrderPrice = strSellPrice;
            pStockOrder.nFinalClearFlag = nClearFlag;


            if (OnStockStrategyMBAOrderSignal != null)
            {
                OnStockStrategyMBAOrderSignal(m_UserID, true, pStockOrder);
            }
        }*/

        /*private void btnSendStockLLSOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo = "", strSellPrice = "";

            int nSumQtyDir = 0, nPreQtyFlag = 0, nPreQtyDir = 0, nAmt = 0, nSellPriceFlag = 0, nSellPriceDir = 0, nClearFlag = 0;

            int nQtyDir = 0, nIsForceOrderFlag = 0, nIsVolumeFlag = 0, nIsDealCntFlag = 0, nOrderType = 0, nPriceCond = 0;
            string strdVolume = "", strDealCnt = "", strPreQty = "", strQtyPercent = "", strQtyLimit = "", strLimitPreQty = "", strMarketPrice = "", strOrderPrice = "";
            int nOrderPriceType = 0, nBidAsk = 0, nVolumeDuration = 0, nDealCntDuration = 0;


            //商品
            if (StockNoLLS.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNoLLS.Text.Trim();

            //0現;1融資;2融券;3無券賣
            if (box_OrderTypeLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }
            if (box_OrderTypeLLS.SelectedIndex == 0)
                nOrderType = box_OrderTypeLLS.SelectedIndex;
            else
                nOrderType = box_OrderTypeLLS.SelectedIndex + 2;

            if (box_OrderTypeLLS.SelectedIndex == 3)
                nOrderType = 8;            
          
            if (box_OrderCondLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(R/I/F)");
                return;
            }
            switch (box_OrderCondLLS.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;

                case 1:
                    nPriceCond = 3;
                    break;
                case 2:
                    nPriceCond = 4;
                    break;
            }

            if (box_BidAskLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = box_BidAskLLS.SelectedIndex;

            if (box_PriceFlagLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(市價/限價)");
                return;
            }
            nOrderPriceType = (box_PriceFlagLLS.SelectedIndex + 1);

            double dMarketPrice = 0.0;
            if (double.TryParse(MarketPriceLLS.Text.Trim(), out dMarketPrice) == false)
            {
                MessageBox.Show("市價請輸入數字");
                return;
            }
            strMarketPrice = MarketPriceLLS.Text.Trim();

            double dOrderPrice = 0.0;
            if (double.TryParse(OrderPriceLLS.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = OrderPriceLLS.Text.Trim();

            if (int.TryParse(QtyLLS.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託總數請輸入數字");
                return;
            }

            double dLimitPreQtyLLS = 0.0;
            if (double.TryParse(LimitPreQtyLLS.Text.Trim(), out dLimitPreQtyLLS) == false)
            {
                MessageBox.Show("單次委託量最高上限請輸入數字");
                return;
            }
            else
                strLimitPreQty = LimitPreQtyLLS.Text.Trim();

            //觸發條件
            if (box_IsVolumeLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為累積張數");
                return;
            }
            nIsVolumeFlag = box_IsVolumeLLS.SelectedIndex;


            if (int.TryParse(VolumeDurationLLS.Text.Trim(), out nVolumeDuration) == false)
            {
                MessageBox.Show("張數累積時間請輸入數字");
                return;
            }

            double dVolume = 0.0;
            if (double.TryParse(VolumeLLS.Text.Trim(), out dVolume) == false)
            {
                MessageBox.Show("累積張數請輸入數字");
                return;
            }
            strdVolume = VolumeLLS.Text.Trim();

            if (box_IsDealCntLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為成交筆數");
                return;
            }
            nIsDealCntFlag = box_IsDealCntLLS.SelectedIndex;

            if (int.TryParse(DealCntDurationLLS.Text.Trim(), out nDealCntDuration) == false)
            {
                MessageBox.Show("成交筆數累積時間請輸入數字");
                return;
            }

            double dDealCnt = 0.0;
            if (double.TryParse(DealCntLLS.Text.Trim(), out dDealCnt) == false)
            {
                MessageBox.Show("成交筆數請輸入數字");
                return;
            }
            strDealCnt = DealCntLLS.Text.Trim();

            if (box_PreQtyFlagLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為單量");
                return;
            }
            nPreQtyFlag = box_PreQtyFlagLLS.SelectedIndex;

            if (box_PreQtyDirLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇單量觸發方向");
                return;
            }
            nPreQtyDir = box_PreQtyDirLLS.SelectedIndex;

            double dPreQty = 0.0;
            if (double.TryParse(PreQtyLLS.Text.Trim(), out dPreQty) == false)
            {
                MessageBox.Show("單量請輸入數字");
                return;
            }
            strPreQty = PreQtyLLS.Text.Trim();

            if (box_IsAskQtyLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為最佳委賣量");
                return;
            }
            nSellPriceFlag = box_IsAskQtyLLS.SelectedIndex;

            if (box_AskQtyDirLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇最佳委賣量觸發方向");
                return;
            }
            nSellPriceDir = box_AskQtyDirLLS.SelectedIndex;

            double dAskQty = 0.0;
            if (double.TryParse(AskQtyLLS.Text.Trim(), out dAskQty) == false)
            {
                MessageBox.Show("最佳委賣量請輸入數字");
                return;
            }
            strSellPrice = AskQtyLLS.Text.Trim();

            if (box_ClearFlagLLS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為全部成立");
                return;
            }
            nClearFlag = box_ClearFlagLLS.SelectedIndex;



            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderPriceCond = nPriceCond;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strMarketPrice;
            pStockOrder.bstrTPOrderPrice = strOrderPrice;
            pStockOrder.nQty = nAmt;
            pStockOrder.bstrMITTriggerPrice = strLimitPreQty;
            pStockOrder.nSumQtyFlag = nIsVolumeFlag;
            pStockOrder.nSumQtyDir = nVolumeDuration;
            pStockOrder.bstrSumQty = strdVolume;
            pStockOrder.nTickFlag = nIsDealCntFlag;
            pStockOrder.nTickDir = nDealCntDuration;
            pStockOrder.bstrTick = strDealCnt;
            pStockOrder.nPreQtyFlag = nPreQtyFlag;
            pStockOrder.nPreQtyDir = nPreQtyDir;
            pStockOrder.bstrPreQty = strPreQty;
            pStockOrder.nStopLossFlag = nSellPriceFlag;
            pStockOrder.nSLDir = nSellPriceDir;
            pStockOrder.bstrSLOrderPrice = strSellPrice;
            pStockOrder.nFinalClearFlag = nClearFlag;


            if (OnStockStrategyLLSOrderSignal != null)
            {
                OnStockStrategyLLSOrderSignal(m_UserID, true, pStockOrder);
            }
        }*/

        /*private void btnSendStockStrategyFTLDayTrade_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strSellPrice = "", strBuyPrice = "", strDealPrice = "", strStockNo = "", strOrderPrice = "", strPreQty = "", strSumQty = "", strBidQty = "", strAskQty = "";
            int nBuyPriceFlag = 0, nDealPriceFlag = 0, nAmt = 0, nBidAsk = 0, nPriceCond = 0, nOrderPriceType = 0, nOrderType = 0, nClearFlag = 0, nBuyPriceDir = 0;
            int nSellPriceFlag = 0, nSellPriceDir = 0, nPreQtyDir = 0, nPreQtyFlag = 0, nSumQtyFlag = 0, nSumQtyDir = 0, nBidDir = 0, nBidFlag = 0, nAskDir = 0, nAskFlag = 0;

            string strOrderPrice2 = "", strOrderPrice3 = "", strTriggerPrice = "";
            int nOrderPriceType2 = 0, nPriceCond2 = 0, nOrderPriceType3 = 0, nPriceCond3 = 0, nTriggerDir = 0;
            int nBuyType = 0, nSellType = 0;
            int nDealPriceDir2 = 0;

            //商品
            if (StockNumber_FTL.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNumber_FTL.Text.Trim();

            if (int.TryParse(Qty_FTL.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (BuyType_FTL.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇進場策略");
                return;
            }
            nBuyType = BuyType_FTL.SelectedIndex;

            if (SellType_FTL.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇出場策略");
                return;
            }
            nSellType = SellType_FTL.SelectedIndex;

            //進場策略//
            if (BuyType_FTL.SelectedIndex == 0)
            {
                if (BuySell_Order.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別(進場策略 - 一般單)");
                    return;
                }
                nBidAsk = BuySell_Order.SelectedIndex;
            }
            else
            {
                if (BuySell_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別(進場策略 - 自組單)");
                    return;
                }
                nBidAsk = BuySell_FTL.SelectedIndex;
            }

            if (BuyType_FTL.SelectedIndex == 0)
            {
                //0現;1無券賣
                if (OrderType_Order.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託別:現資券(進場策略 - 一般單)");
                    return;
                }
                switch (OrderType_Order.SelectedIndex)
                {
                    case 0:
                        nOrderType = OrderType_Order.SelectedIndex;
                        break;
                    case 1:
                        nOrderType = 8;
                        break;
                }
            }
            else
            {
                //0現;1無券賣
                if (OrderType_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託別:現資券(進場策略 - 自組單)");
                    return;
                }
                switch (OrderCond_FTL.SelectedIndex)
                {
                    case 0:
                        nOrderType = OrderType_FTL.SelectedIndex;
                        break;
                    case 1:
                        nOrderType = 8;
                        break;
                }
            }

            if (BuyType_FTL.SelectedIndex == 0)
            {
                if (OrderPriceType_Order.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價類別(1市價/2限價)(進場策略 - 一般單)");
                    return;
                }
                nOrderPriceType = (OrderPriceType_Order.SelectedIndex + 1);
            }
            else
            {
                if (OrderPriceType_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價類別(1市價/2限價)(進場策略 - 自組單)");
                    return;
                }
                nOrderPriceType = (OrderPriceType_FTL.SelectedIndex + 1);
            }

            if (BuyType_FTL.SelectedIndex == 0)
            {
                double dOrderPrice = 0.0;
                if (double.TryParse(OrderPrice_Order.Text.Trim(), out dOrderPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字(進場策略 - 一般單)");
                    return;
                }
                strOrderPrice = OrderPrice_Order.Text.Trim();
            }
            else
            {
                double dOrderPrice = 0.0;
                if (double.TryParse(OrderPrice_FTL.Text.Trim(), out dOrderPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字(進場策略 - 自組單)");
                    return;
                }
                strOrderPrice = OrderPrice_FTL.Text.Trim();
            }

            if (BuyType_FTL.SelectedIndex == 0)
            {
                if (OrderCond_Order.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價條件(R/I/F)(進場策略 - 一般單)");
                    return;
                }
                switch (OrderCond_Order.SelectedIndex)
                {
                    case 0:
                        nPriceCond = 0;
                        break;
                    case 1:
                        nPriceCond = 3;
                        break;
                    case 2:
                        nPriceCond = 4;
                        break;
                }
            }
            else
            {
                if (OrderCond_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價條件(R/I/F)(進場策略 - 自組單)");
                    return;
                }
                switch (OrderCond_FTL.SelectedIndex)
                {
                    case 0:
                        nPriceCond = 0;
                        break;
                    case 1:
                        nPriceCond = 3;
                        break;
                    case 2:
                        nPriceCond = 4;
                        break;
                }
            }

            if (BuyType_FTL.SelectedIndex == 1)
            {
                if (IsAnd_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為全部成立");
                    return;
                }
                nClearFlag = IsAnd_FTL.SelectedIndex;

                if (IsDeal_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為成交價");
                    return;
                }
                nDealPriceFlag = IsDeal_FTL.SelectedIndex;
                //

                if (DealDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇成交價觸發方向");
                    return;
                }
                nDealPriceDir2 = DealDir_FTL.SelectedIndex;


                double dDealPrice = 0.0;
                if (double.TryParse(DealPrice_FTL.Text.Trim(), out dDealPrice) == false)
                {
                    MessageBox.Show("成交價請輸入數字");
                    return;
                }
                strDealPrice = DealPrice_FTL.Text.Trim();

                if (IsBuy_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為委買價");
                    return;
                }
                nBuyPriceFlag = IsBuy_FTL.SelectedIndex;

                if (BuyDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委買價觸發方向");
                    return;
                }
                nBuyPriceDir = BuyDir_FTL.SelectedIndex;

                double dBuyPrice = 0.0;
                if (double.TryParse(BuyPrice_FTL.Text.Trim(), out dBuyPrice) == false)
                {
                    MessageBox.Show("委買價請輸入數字");
                    return;
                }
                strBuyPrice = BuyPrice_FTL.Text.Trim();

                if (IsSell_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為委賣價");
                    return;
                }
                nSellPriceFlag = IsSell_FTL.SelectedIndex;

                if (SellDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委賣價觸發方向");
                    return;
                }
                nSellPriceDir = SellDir_FTL.SelectedIndex;

                double dSellPrice = 0.0;
                if (double.TryParse(SellPrice_FTL.Text.Trim(), out dSellPrice) == false)
                {
                    MessageBox.Show("委賣價請輸入數字");
                    return;
                }
                strSellPrice = SellPrice_FTL.Text.Trim();

                if (IsSumQty_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為總量");
                    return;
                }
                nSumQtyFlag = IsSumQty_FTL.SelectedIndex;

                if (SumQtyDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇總量觸發方向");
                    return;
                }
                nSumQtyDir = SumQtyDir_FTL.SelectedIndex;

                double dSumQty = 0.0;
                if (double.TryParse(SumQty_FTL.Text.Trim(), out dSumQty) == false)
                {
                    MessageBox.Show("總量請輸入數字");
                    return;
                }
                strSumQty = SumQty_FTL.Text.Trim();

                if (IsPreQty_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為單量");
                    return;
                }
                nPreQtyFlag = IsPreQty_FTL.SelectedIndex;

                if (PreQtyDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇單量觸發方向");
                    return;
                }
                nPreQtyDir = PreQtyDir_FTL.SelectedIndex;

                double dPreQty = 0.0;
                if (double.TryParse(PreQty_FTL.Text.Trim(), out dPreQty) == false)
                {
                    MessageBox.Show("單量請輸入數字");
                    return;
                }
                strPreQty = PreQty_FTL.Text.Trim();

                if (IsBid_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為最佳一檔委買量");
                    return;
                }
                nBidFlag = IsBid_FTL.SelectedIndex;

                if (BidDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇最佳一檔委買量觸發方向");
                    return;
                }
                nBidDir = BidDir_FTL.SelectedIndex;

                double dBidQty = 0.0;
                if (double.TryParse(Bid_FTL.Text.Trim(), out dBidQty) == false)
                {
                    MessageBox.Show("最佳一檔委買量請輸入數字");
                    return;
                }
                strBidQty = Bid_FTL.Text.Trim();

                if (IsAsk_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇觸發條件-是否為最佳一檔委賣量");
                    return;
                }
                nAskFlag = IsAsk_FTL.SelectedIndex;

                if (IsAskDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇最佳一檔委賣量觸發方向");
                    return;
                }
                nAskDir = IsAskDir_FTL.SelectedIndex;

                double dAskty = 0.0;
                if (double.TryParse(Ask_FTL.Text.Trim(), out dAskty) == false)
                {
                    MessageBox.Show("最佳一檔委賣量請輸入數字");
                    return;
                }
                strAskQty = Ask_FTL.Text.Trim();
            }

            //出場策略//
            int nBidAskSell = 0;
            if (SellType_FTL.SelectedIndex == 0)
            {
                if (BuySell_OCO.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別(出場策略 - 快速OCO)");
                    return;
                }
                nBidAskSell = BuySell_OCO.SelectedIndex;
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                if (BuySell_MIT.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別(出場策略 - 觸價單)");
                    return;
                }
                nBidAskSell = BuySell_MIT.SelectedIndex;
            }
            else
            {
                if (BuySell_Order2.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣別(出場策略 - 一般單)");
                    return;
                }
                nBidAskSell = BuySell_Order2.SelectedIndex;
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                if (OrderPriceTypeO1_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇第一腳委託價類別(2限價)(出場策略 - 快速OCO)");
                    return;
                }
                nOrderPriceType2 = ((OrderPriceTypeO1_FTL.SelectedIndex) + 2);
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                if (OrderPriceType_MIT.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價類別(1市價；2限價)(出場策略 - 觸價單)");
                    return;
                }
                nOrderPriceType2 = ((OrderPriceType_MIT.SelectedIndex) + 1);
            }
            else
            {
                if (OrderPriceType_Order2.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價類別(1市價；2限價)(出場策略 - 一般單)");
                    return;
                }
                nOrderPriceType2 = ((OrderPriceType_Order2.SelectedIndex) + 1);
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                double dOrderPrice2 = 0.0;
                if (double.TryParse(OrderPriceO1_FTL.Text.Trim(), out dOrderPrice2) == false)
                {
                    MessageBox.Show("第一腳委託價請輸入數字(出場策略 - 快速OCO)");
                    return;
                }
                strOrderPrice2 = OrderPriceO1_FTL.Text.Trim();
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                double dOrderPrice2 = 0.0;
                if (double.TryParse(OrderPrice_MIT.Text.Trim(), out dOrderPrice2) == false)
                {
                    MessageBox.Show("委託價請輸入數字(出場策略 - 觸價單)");
                    return;
                }
                strOrderPrice2 = OrderPrice_MIT.Text.Trim();
            }
            else
            {
                double dOrderPrice2 = 0.0;
                if (double.TryParse(OrderPrice_Order2.Text.Trim(), out dOrderPrice2) == false)
                {
                    MessageBox.Show("委託價請輸入數字(出場策略 - 一般單)");
                    return;
                }
                strOrderPrice2 = OrderPrice_Order2.Text.Trim();
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                if (OrderCondO1_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇第一腳委託價條件(R)(出場策略 - 快速OCO)");
                    return;
                }
                else
                {
                    switch (OrderCondO1_FTL.SelectedIndex)
                    {
                        case 0:
                            nPriceCond2 = 0;
                            break;
                    }
                }
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                if (OrderCond_MIT.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價條件(R/I/F)(出場策略 - 觸價單)");
                    return;
                }
                else
                {
                    switch (OrderCond_MIT.SelectedIndex)
                    {
                        case 0:
                            nPriceCond2 = 0;
                            break;
                        case 1:
                            nPriceCond2 = 3;
                            break;
                        case 2:
                            nPriceCond2 = 4;
                            break;
                    }
                }
            }
            else
            {
                if (OrderCond_Order2.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價條件(R/I/F)(出場策略 - 一般單)");
                    return;
                }
                else
                {
                    switch (OrderCond_Order2.SelectedIndex)
                    {
                        case 0:
                            nPriceCond2 = 0;
                            break;
                        case 1:
                            nPriceCond2 = 3;
                            break;
                        case 2:
                            nPriceCond2 = 4;
                            break;
                    }
                }
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                if (OrderPriceTypeO2_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇第二腳委託價類別(1市價 / 2限價)(出場策略 - 快速OCO)");
                    return;
                }
                nOrderPriceType3 = (OrderPriceTypeO2_FTL.SelectedIndex + 1);
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                double dOrderPrice3 = 0.0;
                if (double.TryParse(OrderPriceO2_FTL.Text.Trim(), out dOrderPrice3) == false)
                {
                    MessageBox.Show("第二腳委託價請輸入數字(出場策略 - 快速OCO)");
                    return;
                }
                strOrderPrice3 = OrderPriceO2_FTL.Text.Trim();
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                double dOrderPrice3 = 0.0;
                if (double.TryParse(BasePrice_MIT.Text.Trim(), out dOrderPrice3) == false)
                {
                    MessageBox.Show("市價請輸入數字(出場策略 - 觸價單)");
                    return;
                }
                strOrderPrice3 = BasePrice_MIT.Text.Trim();
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                if (OrderCondO2_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇第二腳委託價條件(R/I/F)");
                    return;
                }
                switch (OrderCondO2_FTL.SelectedIndex)
                {
                    case 0:
                        nPriceCond3 = 0;
                        break;
                    case 1:
                        nPriceCond3 = 3;
                        break;
                    case 2:
                        nPriceCond3 = 4;
                        break;
                }
            }


            if (SellType_FTL.SelectedIndex == 0)
            {
                if (TriggerDir_FTL.SelectedIndex < 0)
                {
                    MessageBox.Show("第二腳觸發價方向(出場策略 - 快速OCO)");
                    return;
                }
                nTriggerDir = (TriggerDir_FTL.SelectedIndex) + 1;
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                if (TriggerDir_MIT.SelectedIndex < 0)
                {
                    MessageBox.Show("觸發價方向(出場策略 - 觸價單)");
                    return;
                }
                nTriggerDir = (TriggerDir_MIT.SelectedIndex) + 1;
            }

            if (SellType_FTL.SelectedIndex == 0)
            {
                double dTriggerPrice = 0.0;
                if (double.TryParse(TriggerPrice_FTL.Text.Trim(), out dTriggerPrice) == false)
                {
                    MessageBox.Show("第二腳觸發價請輸入數字(出場策略 - 快速OCO)");
                    return;
                }
                strTriggerPrice = TriggerPrice_FTL.Text.Trim();
            }
            else if (SellType_FTL.SelectedIndex == 1)
            {
                double dTriggerPrice = 0.0;
                if (double.TryParse(TriggerPrice_MIT.Text.Trim(), out dTriggerPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字(出場策略 - 觸價單)");
                    return;
                }
                strTriggerPrice = TriggerPrice_MIT.Text.Trim();
            }


            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nQty = nAmt;
            pStockOrder.nBuyType = nBuyType;
            pStockOrder.nSellType = nSellType;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.nOrderPriceCond = nPriceCond;

            pStockOrder.nRDTPMarketPriceType = (nDealPriceDir2);

            pStockOrder.nFinalClearFlag = nClearFlag;
            pStockOrder.nInnerOrderIsMIT = nDealPriceFlag;

            pStockOrder.bstrMITTriggerPrice = strDealPrice;
            pStockOrder.nTakeProfitFlag = nBuyPriceFlag;
            pStockOrder.nTPDir = nBuyPriceDir;
            pStockOrder.bstrTPOrderPrice = strBuyPrice;
            pStockOrder.nStopLossFlag = nSellPriceFlag;
            pStockOrder.nSLDir = nSellPriceDir;
            pStockOrder.bstrSLOrderPrice = strSellPrice;
            pStockOrder.nSumQtyFlag = nSumQtyFlag;
            pStockOrder.nSumQtyDir = nSumQtyDir;
            pStockOrder.bstrSumQty = strSumQty;
            pStockOrder.nPreQtyFlag = nPreQtyFlag;
            pStockOrder.nPreQtyDir = nPreQtyDir;
            pStockOrder.bstrPreQty = strPreQty;
            pStockOrder.nClearAllFlag = nBidFlag;
            pStockOrder.nTickFlag = nBidDir;
            pStockOrder.bstrClearAllOrderPrice = strBidQty;
            pStockOrder.nUpDownFlag = nAskFlag;
            pStockOrder.nUpDownDir = nAskDir;
            pStockOrder.bstrSLPercent = strAskQty;
            pStockOrder.nOrderPriceType2 = nOrderPriceType2;
            pStockOrder.bstrOrderPrice2 = strOrderPrice2;
            pStockOrder.nOrderPriceCond2 = nPriceCond2;
            pStockOrder.nOrderPriceType3 = nOrderPriceType3;
            pStockOrder.bstrOrderPrice3 = strOrderPrice3;
            pStockOrder.nOrderPriceCond3 = nPriceCond3;
            pStockOrder.bstrTick = strTriggerPrice;
            pStockOrder.nTickDir = nTriggerDir;


            if (OnStockStrategyFTLDayTradeOrderSignal != null)
            {
                OnStockStrategyFTLDayTradeOrderSignal(m_UserID, true, pStockOrder);
            }
        }*/

        private void btn_CancelStrategyList_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strKeyNo;
            int nMarket = 0;


            if (SmartKeys.Text.Trim() == "")
            {
                MessageBox.Show("請輸入智慧單序號");
                return;
            }
            strKeyNo = SmartKeys.Text.Trim();

            if (MarketNo_List.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇刪單市場別");
                return;
            }
            nMarket = (MarketNo_List.SelectedIndex + 1);

            CANCELSTRATEGYORDER pOrder = new CANCELSTRATEGYORDER();
            pOrder.bstrFullAccount = m_UserAccount;
            pOrder.bstrSmartKey = strKeyNo;
            pOrder.nMarket = nMarket;


            if (OnCancelStrategyListSignal != null)
            {
                OnCancelStrategyListSignal(m_UserID, pOrder);
            }
        }

        /*private void FTLCB_btn_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo = "", strOrderPrice = "", strDealPrice = "", strBuyPrice = "", strSellPrice = "";
            string strSumQty = "", strPreQty = "", strUserDef = "", strBest5SP = "", strBest5BP = "", strBidQty = "", strAskQty = "", strExchangeIDAB = "", strStockNoAB = "";
            int nOrderPriceType = 0, nPriceCond = 0, nOrderType = 0, nBidAsk = 0;
            int nAmt = 0, nDealPriceFlag = 0, nDealPriceDir = 0, nBuyPriceFlag = 0, nBuyPriceDir = 0, nSellPriceFlag = 0, nSellPriceDir = 0, nClearFlag = 0;
            int nSumQtyFlag = 0, nSumQtyDir = 0, nPreQtyFlag = 0, nPreQtyDir = 0;
            int nIsTradeThruRule = 0, nPreRiskFlag = 0, nBidFlag = 0, nBidDir = 0, nAskFlag = 0, nAskDir = 0, nIsABOrderFlag = 0, nMarketTypeAB = 0, nMarketType=0;




            //商品
            if (StockNo_FTLCB.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNo_FTLCB.Text.Trim();

            //市場別 1:國內證券 5:盤中零股
            if (MarketType_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇市場別");
                return;
            }
            if (MarketType_FTLCB.SelectedIndex == 0)
                nMarketType = 1;
            else if (MarketType_FTLCB.SelectedIndex == 1)
                nMarketType = 5;


            //0現;1融資;2融券;3無券賣
            if (OrderType_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託別:現資券");
                return;
            }
            if (OrderType_FTLCB.SelectedIndex == 0)
                nOrderType = OrderType_FTLCB.SelectedIndex;
            else
                nOrderType = OrderType_FTLCB.SelectedIndex + 2;

            if (OrderType_FTLCB.SelectedIndex == 3)
                nOrderType = 8;

            if (OrderCond_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價條件(R/I/F)");
                return;
            }
            switch (OrderCond_FTLCB.SelectedIndex)
            {
                case 0:
                    nPriceCond = 0;
                    break;

                case 1:
                    nPriceCond = 3;
                    break;
                case 2:
                    nPriceCond = 4;
                    break;

            }

            if (BidAsk_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = BidAsk_FTLCB.SelectedIndex;

            if (PriceFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(1市價/2限價)");
                return;
            }
            nOrderPriceType = (PriceFlag_FTLCB.SelectedIndex + 1);

            double dOrderPrice = 0.0;

            if (double.TryParse(OrderPrice_FTLCB.Text.Trim(), out dOrderPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrderPrice = OrderPrice_FTLCB.Text.Trim();

            if (int.TryParse(Qty_FTLCB.Text.Trim(), out nAmt) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (DealPriceFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為成交價");
                return;
            }
            nDealPriceFlag = DealPriceFlag_FTLCB.SelectedIndex;

            if (DealPriceDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇成交價觸發方向");
                return;
            }
            nDealPriceDir = DealPriceDir_FTLCB.SelectedIndex;

            double dDealPrice = 0.0;
            if (double.TryParse(DealPrice_FTLCB.Text.Trim(), out dDealPrice) == false)
            {
                MessageBox.Show("成交價請輸入數字");
                return;
            }
            strDealPrice = DealPrice_FTLCB.Text.Trim();

            if (BuyPriceFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為委買價");
                return;
            }
            nBuyPriceFlag = BuyPriceFlag_FTLCB.SelectedIndex;

            if (BuyPriceDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委買價觸發方向");
                return;
            }
            nBuyPriceDir = BuyPriceDir_FTLCB.SelectedIndex;

            double dBuyPrice = 0.0;
            if (double.TryParse(BuyPrice_FTLCB.Text.Trim(), out dBuyPrice) == false)
            {
                MessageBox.Show("委買價請輸入數字");
                return;
            }
            strBuyPrice = BuyPrice_FTLCB.Text.Trim();

            if (SellPriceFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為委賣價");
                return;
            }
            nSellPriceFlag = SellPriceFlag_FTLCB.SelectedIndex;

            if (SellPriceDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委賣價觸發方向");
                return;
            }
            nSellPriceDir = SellPriceDir_FTLCB.SelectedIndex;

            double dSellPrice = 0.0;
            if (double.TryParse(SellPrice_FTLCB.Text.Trim(), out dSellPrice) == false)
            {
                MessageBox.Show("委賣價請輸入數字");
                return;
            }
            strSellPrice = SellPrice_FTLCB.Text.Trim();



            if (PreQtyFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為單量");
                return;
            }
            nPreQtyFlag = PreQtyFlag_FTLCB.SelectedIndex;

            if (PreQtyDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇單量觸發方向");
                return;
            }
            nPreQtyDir = PreQtyDir_FTLCB.SelectedIndex;

            double dPreQty = 0.0;
            if (double.TryParse(PreQty_FTLCB.Text.Trim(), out dPreQty) == false)
            {
                MessageBox.Show("單量請輸入數字");
                return;
            }
            strPreQty = PreQty_FTLCB.Text.Trim();

            if (SumQFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為總量");
                return;
            }
            nSumQtyFlag = SumQFlag_FTLCB.SelectedIndex;

            if (SumQDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇總量觸發方向");
                return;
            }
            nSumQtyDir = SumQDir_FTLCB.SelectedIndex;

            double dSumQty = 0.0;
            if (double.TryParse(SumQ_FTLCB.Text.Trim(), out dSumQty) == false)
            {
                MessageBox.Show("總量請輸入數字");
                return;
            }
            strSumQty = SumQ_FTLCB.Text.Trim();


            if (ClearFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為全部成立");
                return;
            }
            nClearFlag = ClearFlag_FTLCB.SelectedIndex;


            if (Best5SP_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇洗第幾檔委賣價");
                return;
            }
            else
            {
                switch (Best5SP_FTLCB.SelectedIndex)
                {
                    case 0:
                        strBest5SP = "0";
                        break;
                    case 1:
                        strBest5SP = "1";
                        break;
                    case 2:
                        strBest5SP = "2";
                        break;
                    case 3:
                        strBest5SP = "3";
                        break;
                    case 4:
                        strBest5SP = "4";
                        break;
                    case 5:
                        strBest5SP = "5";
                        break;
                    case 6:
                        strBest5SP = "99";
                        break;
                }
            }

            if (Best5BP_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇洗第幾檔委買價");
                return;
            }
            else
            {
                switch (Best5BP_FTLCB.SelectedIndex)
                {
                    case 0:
                        strBest5BP = "0";
                        break;
                    case 1:
                        strBest5BP = "1";
                        break;
                    case 2:
                        strBest5BP = "2";
                        break;
                    case 3:
                        strBest5BP = "3";
                        break;
                    case 4:
                        strBest5BP = "4";
                        break;
                    case 5:
                        strBest5BP = "5";
                        break;
                    case 6:
                        strBest5BP = "99";
                        break;
                }
            }

            if (IsTradeThruRule_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否執行穿價忽略張數");
                return;
            }
            nIsTradeThruRule = IsTradeThruRule_FTLCB.SelectedIndex;

            if (IsBid_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為最佳一檔委買量");
                return;
            }
            nBidFlag = IsBid_FTLCB.SelectedIndex;

            if (BidDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇最佳一檔委買量觸發方向");
                return;
            }
            nBidDir = BidDir_FTLCB.SelectedIndex;

            double dBidQty = 0.0;
            if (double.TryParse(Bid_FTLCB.Text.Trim(), out dBidQty) == false)
            {
                MessageBox.Show("最佳一檔委買量請輸入數字");
                return;
            }
            strBidQty = Bid_FTLCB.Text.Trim();

            if (IsAsk_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇觸發條件-是否為最佳一檔委賣量");
                return;
            }
            nAskFlag = IsAsk_FTLCB.SelectedIndex;

            if (IsAskDir_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇最佳一檔委賣量觸發方向");
                return;
            }
            nAskDir = IsAskDir_FTLCB.SelectedIndex;

            double dAskty = 0.0;
            if (double.TryParse(Ask_FTLCB.Text.Trim(), out dAskty) == false)
            {
                MessageBox.Show("最佳一檔委賣量請輸入數字");
                return;
            }
            strAskQty = Ask_FTLCB.Text.Trim();

            if (PreRiskFlag_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否需要預風控");
                return;
            }
            nPreRiskFlag = PreRiskFlag_FTLCB.SelectedIndex;


            if (ABOrder_FTLCB.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否要看A下B功能");
                return;
            }
            else if (ABOrder_FTLCB.SelectedIndex == 1)
            {
                nIsABOrderFlag = ABOrder_FTLCB.SelectedIndex;

                if (StockNo2_FTLCB.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入商品代碼_看A下B");
                    return;
                }
                strStockNoAB = StockNo2_FTLCB.Text.Trim();

                if (MarketNo2_FTLCB.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇市場編號_看A下B");
                    return;
                }
                else
                {
                    if (MarketNo2_FTLCB.Text == "1：國內證")
                        nMarketTypeAB = 1;
                    else if (MarketNo2_FTLCB.Text == "5：盤中零股")
                        nMarketTypeAB = 5;
                }

                if (ExchangeID_FTLCB.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇交易所代碼_看A下B");
                    return;
                }
                else if (ExchangeID_FTLCB.SelectedIndex == 0)
                    strExchangeIDAB = "TSE";
                else if (ExchangeID_FTLCB.SelectedIndex == 1)
                    strExchangeIDAB = "OTC";
            }

            strUserDef = UserDef_FTLCB.Text;

            STOCKSTRATEGYORDER pStockOrder = new STOCKSTRATEGYORDER();
            pStockOrder.bstrFullAccount = m_UserAccount;
            pStockOrder.bstrStockNo = strStockNo;
            pStockOrder.nOrderType = nOrderType;
            pStockOrder.nOrderPriceCond = nPriceCond;
            pStockOrder.nBuySell = nBidAsk;
            pStockOrder.nQty = nAmt;
            pStockOrder.nOrderPriceType = nOrderPriceType;
            pStockOrder.bstrOrderPrice = strOrderPrice;
            pStockOrder.nInnerOrderIsMIT = nDealPriceFlag;
            pStockOrder.nMITDir = nDealPriceDir;
            pStockOrder.bstrMITTriggerPrice = strDealPrice;
            pStockOrder.nTakeProfitFlag = nBuyPriceFlag;
            pStockOrder.nTPDir = nBuyPriceDir;
            pStockOrder.bstrTPOrderPrice = strBuyPrice;
            pStockOrder.nStopLossFlag = nSellPriceFlag;
            pStockOrder.nSLDir = nSellPriceDir;
            pStockOrder.bstrSLOrderPrice = strSellPrice;
            //pStockOrder.nTickFlag = nTickFlag;
            //pStockOrder.nTickDir = nTickDir;
            //pStockOrder.bstrTick = strTick;
            //pStockOrder.nUpDownFlag = nUpDownFlag;
            //pStockOrder.nUpDownDir = nUpDownDir;
            //pStockOrder.bstrSLPercent = strUpDown;
            pStockOrder.nPreQtyFlag = nPreQtyFlag;
            pStockOrder.nPreQtyDir = nPreQtyDir;
            pStockOrder.bstrPreQty = strPreQty;
            pStockOrder.nSumQtyFlag = nSumQtyFlag;
            pStockOrder.nSumQtyDir = nSumQtyDir;
            pStockOrder.bstrSumQty = strSumQty;
            //pStockOrder.nClearAllFlag = nTimeFlag;
            //pStockOrder.bstrClearCancelTime = strTime;
            pStockOrder.nFinalClearFlag = nClearFlag;
            pStockOrder.bstrUserDef = strUserDef;
            pStockOrder.nTradeThru = nIsTradeThruRule;
            pStockOrder.bstrBest5BP = strBest5BP;
            pStockOrder.bstrBest5SP = strBest5SP;
            pStockOrder.nPreRiskFlag = nPreRiskFlag;

            pStockOrder.nIsBidQty = nBidFlag;
            pStockOrder.nBidQtyDirection = nBidDir;
            pStockOrder.bstrBidQty = strBidQty;
            pStockOrder.nIsAskQty = nAskFlag;
            pStockOrder.nAskQtyDirection = nAskDir;
            pStockOrder.bstrAskQty = strAskQty;

            pStockOrder.nIsABOrder = nIsABOrderFlag;
            pStockOrder.nMarketType_B = nMarketTypeAB;
            pStockOrder.bstrExchangeID_B = strExchangeIDAB;
            pStockOrder.bstrCID = strStockNoAB;
            pStockOrder.nMarketType = nMarketType;

            if (OnStockStrategyCBOrderSignal != null)
            {
                OnStockStrategyFTLCBOrderSignal(m_UserID, true, pStockOrder);
            }

        }*/
    }
}

