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
    public partial class StockOrderControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        
        private int m_nCode;
        public string m_strMessage;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, STOCKORDER pStock);
        public event OrderHandler OnOrderSignal;

        public delegate void DecreaseOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty );
        public event DecreaseOrderHandler OnDecreaseOrderSignal;

        public delegate void CancelOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo);
        public event CancelOrderHandler OnCancelOrderSignal;

        public delegate void CancelOrderByStockHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo);
        public event CancelOrderByStockHandler OnCancelOrderByStockSignal;

        public delegate void CancelOrderByStockNoAdvanceHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo, int nBuySell, string bstrPrice);
        public event CancelOrderByStockNoAdvanceHandler OnCancelOrderByStockNoAdvanceSignal;

        public delegate void RealBalanceHandler(string strLogInID, string strAccount);
        public event RealBalanceHandler OnRealBalanceSignal;

        public delegate void RequestProfitReportHandler(string strLogInID, string strAccount);
        public event RequestProfitReportHandler OnRequestProfitReportSignal;

        public delegate void RequestAmountLimitHandler(string strLogInID, string strAccount, string strStockNo);
        public event RequestAmountLimitHandler OnRequestAmountLimitSignal;

        public delegate void RequestBalanceQueryHandler(string strLogInID, string strAccount, string strStockNo);
        public event RequestBalanceQueryHandler OnRequestBalanceQuerySignal;

        public delegate void CancelOrderByBookHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo);
        public event CancelOrderByBookHandler OnCancelOrderByBookSignal;

        public delegate void CorrectPriceBySeqNoHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strPrice, int nTradeType);
        public event CorrectPriceBySeqNoHandler OnCorrectPriceBySeqNo;

        public delegate void CorrectPriceByBookNoHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSymbol, string strSeqNo, string strPrice, int nTradeType);
        public event CorrectPriceByBookNoHandler OnCorrectPriceByBookNo;


        public delegate void ProfitGWReportHandler(string strLogInID, TSPROFITLOSSGWQUERY pGWQuery);
        public event ProfitGWReportHandler OnProfitGWReportSignal;

        public delegate void OddOrderHandler(string strLogInID, bool bAsyncOrder, STOCKORDER pStock);
        public event OddOrderHandler OnOddOrderSignal;

        public delegate void OrderGWHandler(string strLogInID, bool bAsyncOrder, STOCKORDER pStock);
        public event OrderGWHandler OnOrderGWSignal;

        public delegate void AlterTSOrderGWHandler(string strLogInID, bool bAsyncOrder, STOCKORDER pStock);
        public event AlterTSOrderGWHandler OnAlterTSOrderGWSignal;


        public delegate void DecreaseOrderBookHandler(string strLogInID, bool bAsyncOrder, string strAccount,int nMarket, string strBookNo, int nDecreaseQty);
        public event DecreaseOrderBookHandler OnDecreaseOrderBookSignal;

        public delegate void SpecialRequestHandler(string strLogInID, string strTradeType, string strApplyDate, string strStockID, string strQty, string strAmt, string strBrokerID, string strAcno, string strPaymentDate);
        public event SpecialRequestHandler OnSpecialRequestSignal;

        public delegate void OrderReportHandler(string strLogInID, string strAccount, long lFormat);
        public event OrderReportHandler OnOrderReportGWSignal;

        public delegate void FulfillReportHandler(string strLogInID, string strAccount, long lFormat);
        public event FulfillReportHandler OnFulfillReportGWSignal;

        public delegate void GetAvgCostHandler(string strLogInID, SKAVGCOST pGWQuery);
        public event GetAvgCostHandler OnGetAvgCostSignal;

        /*public delegate void GetUpdateTradeDataHandler(string strLogInID, string strFunction, string strTradeType, string strStockID, string strSDate, string strEDate);
        public event GetUpdateTradeDataHandler OnGetUpdateTradeDataSignal;*///[-20240517-Delete]特殊功能

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
        private bool m_bOrderM = false;
        public bool ContinuousTrading
        {
            get { return m_bOrderM; }
            set { m_bOrderM = value; }
        }

        private string m_UserBrokerID = "";
        public string UserBrokerID
        {
            get { return m_UserBrokerID; }
            set { m_UserBrokerID = value; }
        }

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public StockOrderControl()
        {
            InitializeComponent();
            txt_ProfitLossYMStart.Text = DateTime.Now.ToString("yyyyMMdd");
            txt_ProfitLossYMEnd.Text = DateTime.Now.ToString("yyyyMMdd");
        }

        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private void btnSendStockOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            
            string strStockNo;
            int nPrime;
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
            strStockNo = txtStockNo.Text.Trim();

            if (boxPrime.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市櫃-興櫃");
                return;
            }
            nPrime = boxPrime.SelectedIndex;

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
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false
                && txtPrice.Text.Trim() != "M"
                && txtPrice.Text.Trim() != "H"
                && txtPrice.Text.Trim() != "h"
                && txtPrice.Text.Trim() != "C"
                && txtPrice.Text.Trim() != "c"
                && txtPrice.Text.Trim() != "L"
                && txtPrice.Text.Trim() != "l")
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

            if (m_bOrderM)
            {

                int nCond, nSpecTradeType;

                if (boxCond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託條件(R/I/F)");
                    return;
                }
                nCond = boxCond.SelectedIndex;

                if (boxSpecialTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價格類型(1市價/0_2限價)");
                    return;
                }
                
                //if (boxSpecialTradeType.SelectedIndex == 1)
                    nSpecTradeType = boxSpecialTradeType.SelectedIndex+1;   
                //else
                //    nSpecTradeType = boxSpecialTradeType.SelectedIndex+2; 



                SKCOMLib.STOCKORDER pOrder = new STOCKORDER();

                pOrder.bstrFullAccount = m_UserAccount;
                pOrder.bstrPrice = strPrice;
                pOrder.bstrStockNo = strStockNo;
                pOrder.nQty = nQty;
                pOrder.sPrime = (short)nPrime;
                pOrder.sBuySell = (short)nBidAsk;
                pOrder.sFlag = (short)nFlag;
                pOrder.sPeriod = (short)nPeriod;
                pOrder.nTradeType = nCond;
                pOrder.nSpecialTradeType = nSpecTradeType;

                if (OnOrderSignal != null)
                {
                    OnOrderSignal(m_UserID, false, pOrder);
                }
            
            
            }
            else
            {
                SKCOMLib.STOCKORDER pOrder = new STOCKORDER();
                int nCond, nSpecTradeType;

                if (boxCond.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託條件(R/I/F)");
                    return;
                }
                nCond = boxCond.SelectedIndex;

                if (boxSpecialTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇委託價格類型(1市價/2限價)");
                    return;
                }
                //if (boxSpecialTradeType.SelectedIndex == 1)
                nSpecTradeType = boxSpecialTradeType.SelectedIndex+1;
                //else
                //    nSpecTradeType = boxSpecialTradeType.SelectedIndex + 2;

                pOrder.bstrFullAccount = m_UserAccount;
                pOrder.bstrPrice = strPrice;
                pOrder.bstrStockNo = strStockNo;
                pOrder.nQty = nQty;
                pOrder.sPrime = (short)nPrime;
                pOrder.sBuySell = (short)nBidAsk;
                pOrder.sFlag = (short)nFlag;
                pOrder.sPeriod = (short)nPeriod;
                pOrder.nTradeType = nCond;
                pOrder.nSpecialTradeType = nSpecTradeType;

                if (OnOrderSignal != null)
                {
                    OnOrderSignal(m_UserID, false, pOrder);
                }
            }
        }

        private void btnSendStockOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            //int nPrime;
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
            strStockNo = txtStockNo.Text.Trim();

            //if (boxPrime.SelectedIndex < 0)
            //{
           //     MessageBox.Show("請選擇上市櫃-興櫃");
           //     return;
          // }
            //nPrime = boxPrime.SelectedIndex;

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
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false
                && txtPrice.Text.Trim() != "M"
                && txtPrice.Text.Trim() != "H"
                && txtPrice.Text.Trim() != "h"
                && txtPrice.Text.Trim() != "C"
                && txtPrice.Text.Trim() != "c"
                && txtPrice.Text.Trim() != "L"
                && txtPrice.Text.Trim() != "l")
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

            if (TSAsyncFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇同步或非同步");
                return;
            }
            bool bAsync = false;
            bAsync = TSAsyncFlag.SelectedIndex == 1 ? true : false;


            SKCOMLib.STOCKORDER pOrder = new STOCKORDER();

            int nCond, nSpecTradeType;

            if (boxCond.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件(R/I/F)");
                return;
            }
            nCond = boxCond.SelectedIndex;

            if (boxSpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價格類型(1市價/2限價)");
                return;
            }
            //if (boxSpecialTradeType.SelectedIndex == 0)
                nSpecTradeType = boxSpecialTradeType.SelectedIndex+1;
           // else
            //    nSpecTradeType = boxSpecialTradeType.SelectedIndex + 2;

            pOrder.bstrFullAccount = m_UserAccount;
            pOrder.bstrPrice = strPrice;
            pOrder.bstrStockNo = strStockNo;
            
            if (GW_Box.Text == "GW" || GW_Box.SelectedIndex == 0)
                pOrder.nUnitQty = nQty;
           else
                pOrder.nQty = nQty;

            //pOrder.sPrime = (short)nPrime;

            pOrder.sBuySell = (short)nBidAsk;
            pOrder.sFlag = (short)nFlag;
            pOrder.sPeriod = (short)nPeriod;

            pOrder.nTradeType = nCond;
            pOrder.nSpecialTradeType = nSpecTradeType;

            if (GW_Box.Text =="GW" || GW_Box.SelectedIndex == 0)
            {
                if (OnOrderGWSignal != null)
                {
                    OnOrderGWSignal(m_UserID, bAsync, pOrder);
                }
            }
            else {
                if (OnOrderSignal != null)
                {
                    OnOrderSignal(m_UserID, bAsync, pOrder);
                }
            }
        }

        #endregion

        private void btnDecreaseQty_Click(object sender, EventArgs e)
        {
            int nQty = 0;
            
            if( int.TryParse(txtDecreaseQty.Text.Trim(),out nQty) == false)
            {
                MessageBox.Show("改量請輸入數字");
            }


            
            
            if (OnDecreaseOrderSignal != null)
            {
            	OnDecreaseOrderSignal(m_UserID, true, m_UserAccount, txtDecreaseSeqNo.Text.Trim(), nQty);
            }
            
        }

        private void btnCancelOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (txtCancelSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;

            }

            //if (m_bOrderM)
            //{
            //    if (OnCancelOrderBySeqNoContinuousTradingSignal != null)
            //    {
            //        OnCancelOrderBySeqNoContinuousTradingSignal(m_UserID, true, m_UserAccount, txtCancelSeqNo.Text.Trim());
            //    }

            //}
            //else
            {
                if (OnCancelOrderSignal != null)
                {
                    OnCancelOrderSignal(m_UserID, true, m_UserAccount, txtCancelSeqNo.Text.Trim());
                }
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (txtCancelStockNo.Text.Trim() == "")
            {
                if (MessageBox.Show("未輸入商品代碼會刪除所有委託單，是否刪單?", "委託全部刪單", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("已取消本次操作");
                    return;
                }
            }

            if (OnCancelOrderByStockSignal != null)
            {
                OnCancelOrderByStockSignal(m_UserID, true, m_UserAccount, txtCancelStockNo.Text.Trim());
            }
        }

        private void btnGetRealBalanceReport_Click(object sender, EventArgs e)
        {
            if (OnRealBalanceSignal != null)
            {
                OnRealBalanceSignal(m_UserID, m_UserAccount);
            }
        }

        private void btnGetRequestProfitReport_Click(object sender, EventArgs e)
        {
            if (OnRequestProfitReportSignal != null)
            {
                OnRequestProfitReportSignal(m_UserID, m_UserAccount);
            }
        }

        private void btnGetAmountLimit_Click(object sender, EventArgs e)
        {
            string strStockNo = txtAmountLimitStockNo.Text;
            if (OnRequestAmountLimitSignal != null)
            {
                OnRequestAmountLimitSignal(m_UserID, m_UserAccount, strStockNo);
            }
        }

        private void GetBalanceQueryReport_Click(object sender, EventArgs e)
        {
            string strStockNo = txtBalanceQueryStockNo.Text;
            if (OnRequestBalanceQuerySignal != null)
            {
                OnRequestBalanceQuerySignal(m_UserID, m_UserAccount, strStockNo);
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

        private void btnCorrectPriceBySeqNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }
            
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

               
           
            
            //if (m_bOrderM)
            //{
            if (OnCorrectPriceBySeqNo != null)
            {
                OnCorrectPriceBySeqNo(m_UserID, true, m_UserAccount,  strSeqNo, strPrice, nTradeType);
            }

            //}
            //else
            //{
                //
            //}
            
        }

        private void btnCorrectPriceByBookNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }
            
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

            //
            
            //if (m_bOrderM)
            //{
            if (OnCorrectPriceByBookNo != null)
            {
                OnCorrectPriceByBookNo(m_UserID, true, m_UserAccount, boxCorrectSymbol.Text.Trim(), strBookNo, strPrice, nTradeType);
            }

            //}
            //else
            //{ 
            //
            //}
        }

        private void btn_GetProfitLossGW_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }
            
            SKCOMLib.TSPROFITLOSSGWQUERY pGWQuery = new TSPROFITLOSSGWQUERY();

            
            pGWQuery.bstrFullAccount = m_UserAccount;

            if (box_QueryType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇查詢損益類別");
                return;
            }
            pGWQuery.nTPQueryType = box_QueryType.SelectedIndex;

            int nFormat;

            if (box_format.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇查詢損益格式(0彙總:1明細)");
                return;
            }
            nFormat = box_format.SelectedIndex;
            pGWQuery.bstrFullAccount = m_UserAccount;
            pGWQuery.nFunc = nFormat;       

            switch (box_QueryType.SelectedIndex)
            {             

                case 0:
                    {
                        string strStockNo, strTradeType;    
                      
                        if (nFormat == 1)
                        {
                            
                            strStockNo = txt_ProfitLossStock.Text;
                            if (box_format.SelectedIndex < 0)
                            {
                                MessageBox.Show("請選擇查詢損益格式(0彙總:1明細)");
                                return;
                            }
                            strTradeType = box_format.SelectedIndex.ToString();
                            if (box_TradeType.SelectedIndex == 7 || box_TradeType.SelectedIndex == 8)
                                strTradeType = (box_format.SelectedIndex+1).ToString();


                            if (box_TradeType.SelectedIndex == -1 || box_TradeType.SelectedIndex ==9)
                                strTradeType = " ";
                            
                            
                            pGWQuery.bstrStockNo = strStockNo;
                            pGWQuery.bstrTradeType = strTradeType;
                        }
                        
                        if (OnProfitGWReportSignal != null)
                        {
                            OnProfitGWReportSignal(m_UserID, pGWQuery);
                        }
                    }
                break;

                case 1:
                {
                    string strTradeType;
                    pGWQuery.bstrStartDate = txt_ProfitLossYMStart.Text;
                    
                    pGWQuery.bstrEndDate = txt_ProfitLossYMEnd.Text;
                    
                    if (nFormat == 1)
                    { 
                        
                        pGWQuery.bstrBookNo = txt_ProfitLossBookNo.Text;
                        pGWQuery.bstrSeqNo = txt_ProfitLossSeqNo.Text;
                        if (box_TradeType.SelectedIndex == -1 || box_TradeType.SelectedIndex == 9)
                            strTradeType = " ";
                        else if (box_TradeType.SelectedIndex == 7 || box_TradeType.SelectedIndex == 8)
                            strTradeType = (box_format.SelectedIndex + 1).ToString();
                        else
                            strTradeType = box_TradeType.SelectedItem.ToString();

                        
                        
                                        
                        pGWQuery.bstrTradeType = strTradeType;
                        pGWQuery.bstrStockNo = txt_ProfitLossStock.Text;
                        pGWQuery.bstrEndDate = " ";
                    }
                    if (nFormat == 3)
                    {
                        pGWQuery.bstrStockNo = txt_ProfitLossStock.Text;
                    }
                    if (OnProfitGWReportSignal != null)
                    {
                        OnProfitGWReportSignal(m_UserID, pGWQuery);
                    }
                
                }
                break;
                case 2:
                {//summary:1 //detail:2
                    nFormat = box_format.SelectedIndex+1;
                   
                    pGWQuery.nFunc = nFormat;      
                    if (nFormat == 2)
                    {
                        pGWQuery.bstrStockNo = txt_ProfitLossStock.Text;
                        
                    }
                    if (OnProfitGWReportSignal != null)
                    {
                        OnProfitGWReportSignal(m_UserID, pGWQuery);
                    }
                }
                break;
                default:                                     
                break;
                

            }
        }

        private void box_format_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (box_QueryType.SelectedIndex == 1) //已實現損益
            {
                if (box_format.SelectedIndex == 1)
                {
                    txt_ProfitLossYMStart.Text = DateTime.Now.AddYears(-1911).ToString("yyyMMdd");
                    txt_ProfitLossYMEnd.Text = DateTime.Now.AddYears(-1911).ToString("yyyMMdd");
                }
                else if (box_format.SelectedIndex == 0)
                {
                    txt_ProfitLossYMStart.Text = DateTime.Now.ToString("yyyyMMdd");
                    txt_ProfitLossYMEnd.Text = DateTime.Now.ToString("yyyyMMdd");
                }
            }

        }

        

        private void StockOddOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            
            int nBidAsk;
            int nPeriod;
            
            string strPrice;
            int nQty;

            if (StockNoOdd.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = StockNoOdd.Text.Trim();


            if (BuySellBoxOdd.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = BuySellBoxOdd.SelectedIndex;

            if (BoxOddPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = BoxOddPeriod.SelectedIndex + 4;
            //4:盤中零股//

            int nSpecTradeType = OddBoxSpecialTradeType.SelectedIndex + 1;

            double dPrice = 0.0;
            if (double.TryParse(PriceOdd.Text.Trim(), out dPrice) == false
                && PriceOdd.Text.Trim() != "M"
                && PriceOdd.Text.Trim() != "H"
                && PriceOdd.Text.Trim() != "h"
                && PriceOdd.Text.Trim() != "C"
                && PriceOdd.Text.Trim() != "c"
                && PriceOdd.Text.Trim() != "L"
                && PriceOdd.Text.Trim() != "l")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = PriceOdd.Text.Trim();

            if (int.TryParse(QtyOdd.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            int nFlag = OddboxFlag.SelectedIndex+1;

            int nCond = OddboxCond.SelectedIndex;

            if (TCAsyncFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇同步或非同步");
                return;
            }
            bool bAsync = false;
            bAsync = TCAsyncFlag.SelectedIndex == 1 ? true : false;


            SKCOMLib.STOCKORDER pOrder = new STOCKORDER();

            pOrder.bstrFullAccount = m_UserAccount;
            pOrder.bstrPrice = strPrice;
            pOrder.bstrStockNo = strStockNo;
            pOrder.nQty = nQty;
            pOrder.sPrime = 0;
            pOrder.sBuySell = (short)nBidAsk;
            pOrder.sFlag = (short)nFlag;
            pOrder.nTradeType = nCond;
            pOrder.sPeriod = (short)nPeriod;
            
            if (OddBoxGW.SelectedIndex == 0 || OddBoxGW.Text == "GW")
            {
                pOrder.nSpecialTradeType = nSpecTradeType;
                pOrder.nUnitQty = nQty;
                if (OnOrderGWSignal != null)
                {
                    OnOrderGWSignal(m_UserID, bAsync, pOrder);
                }


            }
            else { 
                        if (OnOddOrderSignal != null)
                        {
                            OnOddOrderSignal(m_UserID, bAsync, pOrder);
                        }
             }
            
            
        }

        private void AlterStockOrder_Click(object sender, EventArgs e)
        {
            

                string strStockNo;
                string strSeqNo;
                string strBookNo;
                string strAlterType;
                int nQty;
                int nPeriod;
                int nSpecType;

                if (AlterStockNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入商品代碼");
                    return;
                }
                strStockNo = AlterStockNo.Text.Trim();

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

            if (box_alterSpecType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託價類別(市價/限價)");
                return;
            }
            nSpecType = box_alterSpecType.SelectedIndex + 1;

                string strCorrectPrice = AlterCorrectPrice.Text.Trim();
                if (AlterTypeBox.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇刪除/減量/改價");
                    return;
                }
                if (AlterTypeBox.SelectedIndex == 0)
                    strAlterType = "D";
                else if (AlterTypeBox.SelectedIndex == 1)
                    strAlterType = "C";
                else if (AlterTypeBox.SelectedIndex == 2)
                    strAlterType = "P";
                else
                {
                    MessageBox.Show("請重新選擇!");
                    return;
                }

                //boxMarket
                if (boxMarket.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇市場盤別(盤中/零股/盤後/盤中零股)");
                    return;
                }

                //if ((boxMarket.SelectedIndex == 0) || (boxMarket.SelectedIndex == 2) || (boxMarket.SelectedIndex == 3))
                    nPeriod = boxMarket.SelectedIndex;
                //else if (boxMarket.SelectedIndex == 4)
                //    nPeriod = 12;
                //else
               // {
                //    MessageBox.Show("請重新選擇!");
               //     return;
                //}
                bool bAsync = false;
                bAsync = (AlterAsyncBox.SelectedIndex == 0) ? false : true;

                SKCOMLib.STOCKORDER pOrder = new STOCKORDER();
                pOrder.bstrFullAccount = m_UserAccount;

                pOrder.bstrStockNo = strStockNo;
                pOrder.bstrSeqNo = strSeqNo;
                pOrder.bstrBookNo = strBookNo;
                pOrder.bstrOrderType = strAlterType;
                pOrder.sPeriod = (short)nPeriod;
                pOrder.nQty = nQty;
                pOrder.bstrPrice = strCorrectPrice;
                pOrder.nSpecialTradeType = nSpecType;

            if (OnAlterTSOrderGWSignal != null)
            {//OnAlterTSOrderGWSignal
                OnAlterTSOrderGWSignal(m_UserID, bAsync, pOrder);
            }

            }

            private void DecreaseAmtStockOrder_Click(object sender, EventArgs e)
            {


                string strStockNo;
                string strSeqNo;
                string strBookNo;
                int nQty;

                string strAlterType;
                int nPeriod;

                if (AlterStockNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入商品代碼");
                    return;
                }
                strStockNo = AlterStockNo.Text.Trim();

                if (AlterSeqNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入序號");
                    return;
                }
                strSeqNo = AlterSeqNo.Text.Trim();

                if (AlterBookNo.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入書號");
                    return;
                }
                strBookNo = AlterBookNo.Text.Trim();


                if (int.TryParse(AlterQty.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("減量張(股)數請輸入數字");
                    return;
                }

                string strCorrectPrice = AlterCorrectPrice.Text.Trim();

                if (AlterTypeBox.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇刪除/減量/改價");
                    return;
                }
                if (AlterTypeBox.SelectedIndex == 0)
                    strAlterType = "D";
                else if (AlterTypeBox.SelectedIndex == 1)
                    strAlterType = "C";
                else if (AlterTypeBox.SelectedIndex == 2)
                    strAlterType = "P";
                else
                {
                    MessageBox.Show("請重新選擇!");
                    return;
                }

                //boxMarket
                if (boxMarket.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇市場盤別(盤中/零股/盤後/盤中零股)");
                    return;
                }

                if ((boxMarket.SelectedIndex == 0) || (boxMarket.SelectedIndex == 2) || (boxMarket.SelectedIndex == 3))
                    nPeriod = boxMarket.SelectedIndex;
                else if (boxMarket.SelectedIndex == 4)
                    nPeriod = 12;
                else
                {
                    MessageBox.Show("請重新選擇!");
                    return;
                }


                SKCOMLib.STOCKORDER pOrder = new STOCKORDER();
                pOrder.bstrFullAccount = m_UserAccount;

                pOrder.bstrStockNo = strStockNo;
                pOrder.bstrSeqNo = strSeqNo;
                pOrder.bstrBookNo = strBookNo;
                pOrder.nQty = nQty;
                pOrder.bstrOrderType = strAlterType;
                pOrder.sPeriod = (short)nPeriod;
                pOrder.bstrPrice = strCorrectPrice;

                pOrder.nSpecialTradeType = 2;

                if (OnAlterTSOrderGWSignal != null)
                {
                    OnAlterTSOrderGWSignal(m_UserID, true, pOrder);
                }
            
        }

        private void btnBookDecreaseQty_Click(object sender, EventArgs e)
        {
            int nQty = 0;

            if (int.TryParse(txtDecreaseQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("改量請輸入數字");
            }
            int nMarket = MarketTypeBox.SelectedIndex;
            string strBookNo = DBookNoTxt.Text.Trim();



            if (OnDecreaseOrderBookSignal != null)
            {
                OnDecreaseOrderBookSignal(m_UserID, true, m_UserAccount,nMarket, strBookNo, nQty);
            }
        }

        /*private void buttonSend_Click(object sender, EventArgs e)//[-20240417-Delete]
        {
            if (OnSpecialRequestSignal != null)
            {
                OnSpecialRequestSignal(m_UserID, textTradeType.Text, textApplyDate.Text, textStockID.Text, textQty.Text, textAmt.Text, textBrokerId.Text, textAcno.Text, textPaymentDate.Text);
            }
        }*/

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

        private void BtnAvgCost_Click(object sender, EventArgs e)
        {
            SKCOMLib.SKAVGCOST pGWQuery = new SKAVGCOST();

            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }
            pGWQuery.bstrAccount = m_UserAccount;

            pGWQuery.bstrStockNo = AvgCostStockNo.Text;//空白:所有股票代號


            if (IsHandlingFee_AvgCost.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否需要包含手續費");
                return;
            }

            if (IsHandlingFee_AvgCost.SelectedIndex == 0)
                pGWQuery.bstrFunc = "0";


            if (OnGetAvgCostSignal != null)
            {
                OnGetAvgCostSignal(m_UserID, pGWQuery);
            }

        }
        private void btnCancelOrderByStockNoAdvance_Click(object sender, EventArgs e)
        {
            if (txtCancelStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }

            if (boxBuySellBoxForCancel.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }

            if (OnCancelOrderByStockNoAdvanceSignal != null)
            {
                OnCancelOrderByStockNoAdvanceSignal(m_UserID, true, m_UserAccount, txtCancelStockNo.Text.Trim(), boxBuySellBoxForCancel.SelectedIndex, txtPriceForCancel.Text.Trim());
            }
        }

        /*private void btnGetUpdateTradeData_Click(object sender, EventArgs e)
        {
            string strTradeType = "";
            if (TradeType_YS.SelectedIndex == 0)
                strTradeType = "A";
            else if (TradeType_YS.SelectedIndex == 1)
                strTradeType = "B";
            else if (TradeType_YS.SelectedIndex == 2)
                strTradeType = "C";
            else if (TradeType_YS.SelectedIndex == 3)
                strTradeType = "E";
            else if (TradeType_YS.SelectedIndex == 4)
                strTradeType = "F";
            else if (TradeType_YS.SelectedIndex == 5)
                strTradeType = "D";

            if (OnGetUpdateTradeDataSignal != null)
            {
                OnGetUpdateTradeDataSignal(m_UserID, Function_YS.Text, strTradeType, StockID_YS.Text, SDate_YS.Text, EDate_YS.Text);
            }
        }*/
    }
}
