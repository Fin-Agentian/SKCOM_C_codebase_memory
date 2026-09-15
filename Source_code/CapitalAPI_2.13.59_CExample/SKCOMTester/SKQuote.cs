using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;




namespace SKCOMTester
{
    public partial class SKQuote : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        static class Constants
        {
            public const int _C2 = 1;	/* 0 if not 2's complement */
            public const int INT32_MIN = (-0x7fffffff - _C2);
        }


        private int kMarketPrice = 0;//Constants.INT32_MIN + 1;//-2147483647;

        private bool m_bfirst = true;
        private int m_nCode;
        private int m_nSimulateStock;
        private int m_nCount = 0;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        SKCOMLib.SKQuoteLib m_SKQuoteLib = null;
        SKCOMLib.SKQuoteLib m_SKQuoteLib2 = null;
        public SKQuoteLib SKQuoteLib
        {
            get { return m_SKQuoteLib; }
            set { m_SKQuoteLib = value;}
        }
        public SKQuoteLib SKQuoteLib2
        {
            get { return m_SKQuoteLib2; }
            set { m_SKQuoteLib2 = value;}
        }

        public string m_strLoginID = "";
        public string LoginID
        {
            get { return m_strLoginID; }
            set
            {
                m_strLoginID = value;
            }
        }

        public string m_strLoginID2 = "";
        public string LoginID2
        {
            get { return m_strLoginID2; }
            set
            {
                m_strLoginID2 = value;
            }
        }

        private DataTable m_dtStocks;
        private DataTable m_dtBest5Ask;
        private DataTable m_dtBest5Bid;

        
       
        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public SKQuote()
        {
            InitializeComponent();
            
        }

        private void SKQuote_Load(object sender, EventArgs e)
        {
            m_dtStocks = CreateStocksDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();

            SetDoubleBuffered(gridStocks);
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        void SendReturnMessage(string strType, int nCode, string strMessage)
        {
            if (GetMessage != null)
            {
                GetMessage(strType, nCode, strMessage);
            }
        }
        #endregion


        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_bfirst == true)
            {
                m_SKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib_OnConnection);
                m_SKQuoteLib.OnNotifyQuoteLONG += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(m_SKQuoteLib_OnNotifyQuote);
                m_SKQuoteLib.OnNotifyHistoryTicksLONG += new _ISKQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(m_SKQuoteLib_OnNotifyHistoryTicks); 
                m_SKQuoteLib.OnNotifyTicksLONG += new _ISKQuoteLibEvents_OnNotifyTicksLONGEventHandler(m_SKQuoteLib_OnNotifyTicks);
                m_SKQuoteLib.OnNotifyBest5LONG += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(m_SKQuoteLib_OnNotifyBest5);
                m_SKQuoteLib.OnNotifyKLineData += new _ISKQuoteLibEvents_OnNotifyKLineDataEventHandler(m_SKQuoteLib_OnNotifyKLineData);
                m_SKQuoteLib.OnNotifyServerTime += new _ISKQuoteLibEvents_OnNotifyServerTimeEventHandler(m_SKQuoteLib_OnNotifyServerTime);
                m_SKQuoteLib.OnNotifyMarketTot += new _ISKQuoteLibEvents_OnNotifyMarketTotEventHandler(m_SKQuoteLib_OnNotifyMarketTot);
                m_SKQuoteLib.OnNotifyMarketBuySell += new _ISKQuoteLibEvents_OnNotifyMarketBuySellEventHandler(m_SKQuoteLib_OnNotifyMarketBuySell);
                //m_SKQuoteLib.OnNotifyMarketHighLow += new _ISKQuoteLibEvents_OnNotifyMarketHighLowEventHandler(m_SKQuoteLib_OnNotifyMarketHighLow);
                m_SKQuoteLib.OnNotifyMACDLONG += new _ISKQuoteLibEvents_OnNotifyMACDLONGEventHandler(m_SKQuoteLib_OnNotifyMACD);
                m_SKQuoteLib.OnNotifyBoolTunelLONG += new _ISKQuoteLibEvents_OnNotifyBoolTunelLONGEventHandler(m_SKQuoteLib_OnNotifyBoolTunel);
                m_SKQuoteLib.OnNotifyFutureTradeInfoLONG += new _ISKQuoteLibEvents_OnNotifyFutureTradeInfoLONGEventHandler(m_SKQuoteLib_OnNotifyFutureTradeInfo);
                m_SKQuoteLib.OnNotifyStrikePrices += new _ISKQuoteLibEvents_OnNotifyStrikePricesEventHandler(m_SKQuoteLib_OnNotifyStrikePrices);
                //m_SKQuoteLib.OnNotifyStockList += new _ISKQuoteLibEvents_OnNotifyStockListEventHandler(m_SKQuoteLib_OnNotifyStockList);

                m_SKQuoteLib.OnNotifyMarketHighLowNoWarrant += new _ISKQuoteLibEvents_OnNotifyMarketHighLowNoWarrantEventHandler(m_SKQuoteLib_OnNotifyMarketHighLowNoWarrant);

                m_SKQuoteLib.OnNotifyCommodityListWithTypeNo += new _ISKQuoteLibEvents_OnNotifyCommodityListWithTypeNoEventHandler(m_SKQuoteLib_OnNotifyCommodityListWithTypeNo);
                m_SKQuoteLib.OnNotifyOddLotSpreadDeal    += new _ISKQuoteLibEvents_OnNotifyOddLotSpreadDealEventHandler(m_SKQuoteLib_OnNotifyOddLotSpreadDeal);
                m_SKQuoteLib.OnNotifyLiveKLineData += new _ISKQuoteLibEvents_OnNotifyLiveKLineDataEventHandler(m_SKQuoteLib_OnNotifyLiveKLineData);
                m_bfirst = false;
            }
            //kMarketPrice = m_SKQuoteLib.SKQuoteLib_GetMarketPriceTS();
            int nMarketType = int.Parse(boxMarketType.SelectedIndex.ToString());
            if (nMarketType == 0 || nMarketType == 1)
            {
                m_nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitorLONGByMarket(nMarketType);
            }
            else
            {
                m_nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitorLONG();
            }
            //m_nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitor();
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_EnterMonitorLONG");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_LeaveMonitor();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_LeaveMonitor");
        }

        private void btnTicks_Click(object sender, EventArgs e)
        {
            short sPage;
            
            if (short.TryParse(txtTickPageNo.Text, out sPage) == false)
                return;
            listTicks.Items.Clear();
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            GridBest5Ask.DataSource = m_dtBest5Ask;
            GridBest5Bid.DataSource = m_dtBest5Bid;

            GridBest5Ask.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Ask.Columns["m_nAskQty"].Width = 60;
            GridBest5Ask.Columns["m_nAsk"].HeaderText = "賣價";
            GridBest5Ask.Columns["m_nAsk"].Width = 60;

            GridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Bid.Columns["m_nAskQty"].Width = 60;
            GridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            GridBest5Bid.Columns["m_nAsk"].Width = 60;
            if (txtTick_MarketNo.Text.Trim() == "")
            {
                m_nCode = m_SKQuoteLib.SKQuoteLib_RequestTicks(ref sPage, txtTick.Text.Trim());

                txtTickPageNo.Text = sPage.ToString();

                SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestTicks");
            }
            if (txtTick_MarketNo.Text.Trim() != "")
            {
                short sMarketNo;

                if (short.TryParse(txtTick_MarketNo.Text, out sMarketNo) == false)
                    return;
                m_nCode = SKQuoteLib.SKQuoteLib_RequestTicksWithMarketNo(ref sPage, sMarketNo,txtTick.Text.Trim());
                txtTickPageNo.Text = sPage.ToString();

                SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestTicksWithMarketNo");
            }
        }

        private void btnQueryStocks_Click(object sender, EventArgs e)
        {
            short sPage;

            if (short.TryParse(txtPageNo.Text, out sPage) == false)
                return;


            m_dtStocks.Clear();
            gridStocks.ClearSelection();

            gridStocks.DataSource = m_dtStocks;

            gridStocks.Columns["m_nStockidx"].Visible = false;
            gridStocks.Columns["m_sDecimal"].Visible = false;
            gridStocks.Columns["m_sTypeNo"].Visible = false;
            gridStocks.Columns["m_cMarketNo"].Visible = false;
            gridStocks.Columns["m_caStockNo"].HeaderText = "代碼";
            gridStocks.Columns["m_caName"].HeaderText = "名稱";
            gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
            //gridStocks.Columns["m_nHigh"].Visible = false;
            gridStocks.Columns["m_nHigh"].HeaderText = "最高";
            //gridStocks.Columns["m_nLow"].Visible = false;
            gridStocks.Columns["m_nLow"].HeaderText = "最低";
            gridStocks.Columns["m_nClose"].HeaderText = "成交價";
            gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
            gridStocks.Columns["m_nRef"].HeaderText = "昨收價";
            gridStocks.Columns["m_nBid"].HeaderText = "買價";
            //gridStocks.Columns["m_nBc"].Visible = false;
            gridStocks.Columns["m_nBc"].HeaderText = "買量";
            gridStocks.Columns["m_nAsk"].HeaderText = "賣價";
            //gridStocks.Columns["m_nAc"].Visible = false;
            gridStocks.Columns["m_nAc"].HeaderText = "賣量";
            //gridStocks.Columns["m_nTBc"].Visible = false;
            gridStocks.Columns["m_nTBc"].HeaderText = "買盤量";
            //gridStocks.Columns["m_nTAc"].Visible = false;
            gridStocks.Columns["m_nTAc"].HeaderText = "賣盤量";
            gridStocks.Columns["m_nFutureOI"].Visible = false;
            //gridStocks.Columns["m_nTQty"].Visible = false;
            gridStocks.Columns["m_nTQty"].HeaderText = "總量";
            //gridStocks.Columns["m_nYQty"].Visible = false;
            gridStocks.Columns["m_nYQty"].HeaderText = "昨量";
            //gridStocks.Columns["m_nUp"].Visible = false;
            gridStocks.Columns["m_nUp"].HeaderText = "漲停";
            //gridStocks.Columns["m_nDown"].Visible = false;
            gridStocks.Columns["m_nDown"].HeaderText = "跌停";
            gridStocks.Columns["m_nDealTime"].HeaderText = "成交時間";//[-20240508-Add]

            gridStocks.Columns[0].Frozen = true;

            string[] Stocks = txtStocks.Text.Trim().Split(new Char[] { ',' });

            foreach (string s in Stocks)
            {
                SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

                int nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(s.Trim(), ref pSKStockLONG);

                //[debug]int nCode = m_SKQuoteLib.SKQuoteLib_GetStockByMarketAndNo(5,s.Trim(), ref pSKStockLONG);

                OnUpDateDataRow(pSKStockLONG);

                if (nCode == 0)
                {
                    OnUpDateDataRow(pSKStockLONG);
                }
            }

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestStocks(ref sPage, txtStocks.Text.Trim());

            txtPageNo.Text = sPage.ToString();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestStocks");
        }

        private void btnTickStop_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_CancelRequestTicks(txtTick.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelRequestTicks");
        }

        private void btnServerTime_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestServerTime();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestServerTime");
        }

        private void btnGetBest5_Click(object sender, EventArgs e)
        {
            short sMarket;
            int nStockidx;

            if (short.TryParse(txtBestMarket.Text, out sMarket) == false)
                return;

            if (int.TryParse(txtBestStockidx.Text, out nStockidx) == false)
                return;

            SKBEST5 skBest5 = new SKBEST5();

            m_nCode = m_SKQuoteLib.SKQuoteLib_GetBest5LONG(sMarket, nStockidx, ref skBest5);

            lblBest5Bid.Text = "Best5_Buy..........................................................";
            lblBest5Ask.Text = "Best5_Sell..........................................................";

            if (m_nCode == 0)
            {
                lblBest5Bid.Text = skBest5.nBid1.ToString() + "/" + skBest5.nBidQty1.ToString() + " " + skBest5.nBid2.ToString() + "/" + skBest5.nBidQty2.ToString() + " " + skBest5.nBid3.ToString() + "/" + skBest5.nBidQty3.ToString() + " ...";

                lblBest5Ask.Text = skBest5.nAsk1.ToString() + "/" + skBest5.nAskQty1.ToString() + " " + skBest5.nAsk2.ToString() + "/" + skBest5.nAskQty2.ToString() + " " + skBest5.nAsk3.ToString() + "/" + skBest5.nAskQty3.ToString() + " ...";
            }

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_GetBest5LONG");

        }
        private void btnKLine_Click(object sender, EventArgs e)
        {
            listKLine.Items.Clear();
            
            short sKLineType = short.Parse(boxKLine.SelectedIndex.ToString());
            short sOutType = short.Parse(boxOutType.SelectedIndex.ToString());

            if (sKLineType < 0)
            {
                MessageBox.Show("請選擇KLine類型");
                return;
            }
            if (sOutType < 0)
            {
                MessageBox.Show("請選擇輸出格式類型");
                return;
            }

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestKLine(txtKLine.Text.Trim(), sKLineType, sOutType);

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestKLine");
        }

        private void gridStocks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            gridStocks.Columns[0].Frozen = true;
            if (e.RowIndex >= 0)
            {
                try
                {
                    e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);

                    if (e.Value != null)
                    {
                        string strHeaderText = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText.ToString();

                        if (strHeaderText == "名稱")
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.SkyBlue, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else if (strHeaderText == "買價" || strHeaderText == "賣價" || strHeaderText == "成交價" || strHeaderText == "開盤價" || strHeaderText == "最高" || strHeaderText == "最低")
                        {
                            double dPrc = double.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells["m_nRef"].Value.ToString());

                            double dValue = double.Parse(e.Value.ToString());

                            if (m_nSimulateStock == 1 && strHeaderText != "開盤價")            //盤前/後揭示為深灰色;
                                e.Graphics.FillRectangle(Brushes.SlateGray, e.CellBounds);

                            if (dValue > dPrc)
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Red, e.CellBounds.X, e.CellBounds.Y);
                            }
                            else if (dValue < dPrc)
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Lime, e.CellBounds.X, e.CellBounds.Y);
                            }
                            else
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                            }
                        }
                        else if (strHeaderText == "單量")
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Yellow, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                        }
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        #region COM EVENT
        //----------------------------------------------------------------------
        // COM EVENT
        //----------------------------------------------------------------------
        void m_SKQuoteLib_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {

                if (nCode == 0)
                {
                    lblSignal.ForeColor = Color.Yellow;
                }
            }
            else if (nKind == 3002)
            {
                lblSignal.ForeColor = Color.Red;
            }
            else if (nKind == 3003)//[-20240711-]移除3036判斷
            {
                lblSignal.ForeColor = Color.Green;
            }
            else if (nKind == 3021)//網路斷線
            {
                lblSignal.ForeColor = Color.DarkRed;                 
            }
            else if (nKind == 3033)//異常
            {
                lblSignal.ForeColor = Color.DimGray;
            }
        }

       /* void m_SKQuoteLib2_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {

                if (nCode == 0)
                {
                    label_2.ForeColor = Color.Yellow;
                }
            }
            else if (nKind == 3002)
            {
                label_2.ForeColor = Color.Red;
            }
            else if (nKind == 3003)
            {
                label_2.ForeColor = Color.Green;
            }
            else if (nKind == 3021)//網路斷線
            {
                label_2.ForeColor = Color.DarkRed;
            }
        }*/

        void m_SKQuoteLib_OnNotifyQuote(short sMarketNo, int nStockIdx)
        {
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

            m_SKQuoteLib.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, ref pSKStockLONG);

            OnUpDateDataRow(pSKStockLONG);
        }


        void m_SKQuoteLib_OnNotifyTicks(short sMarketNo, int nStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            //M價轉換與TS是否含msns一致//
            string strData = "";
            //string strTimeNoMsMs = "";
            //int nlength = lTime.ToString().Length;
            //if (nlength >6)
            //    strTimeNoMsMs = lTime.ToString().Substring(0, nlength - 6);
            //[-1020-add for h:m:s'millissecond''microsecond][-0219-add Qty-]
            //string strData = nPtr.ToString() + "," + nTime.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            //kMarketPrice = m_SKQuoteLib.SKQuoteLib_GetMarketPriceTS();
            int nMarketPrice = kMarketPrice;// m_SKQuoteLib.SKQuoteLib_GetMarketPriceTS();
            
            if (chkbox_msms.Checked == true)
                strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            else
                strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + " " + lTimemillismicros.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();     
            if (Box_M.Checked == true)
            {
                if (nBid == kMarketPrice)
                    strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + "M" + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
                else if (nAsk == kMarketPrice)
                    strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + "M" + "," + nClose.ToString() + "," + nQty.ToString();
                else
                    strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            }
            
            
            //[揭示]//0:一般;1:試算揭示
            
            if (strData != "" &&((chkBoxSimulate.Checked) || (!chkBoxSimulate.Checked && nSimulate == 0)))
                listTicks.Items.Add("[OnNotifyTicksLONG]" + strData);

            //if (listTicks.Items.Count < 200)
            //    listTicks.SelectedIndex = listTicks.Items.Count - 1;
            //else
            //    listTicks.Items.Clear();
        }

        void m_SKQuoteLib_OnNotifyHistoryTicks(short sMarketNo, int nStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            //[-0219-add Qty-]
            string strData = "";
            //M價轉換與TS是否含msns一致//
            if (chkbox_msms.Checked == true )
                strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            else if (chkbox_msms.Checked == false)
                strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + " " + lTimemillismicros.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            
            if (Box_M.Checked == true)
            {
                if (nBid == kMarketPrice)
                    strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + "M" + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
                else if (nAsk == kMarketPrice)
                    strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + "M" + "," + nClose.ToString() + "," + nQty.ToString();
                else
                    strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            }
           
                        
            //[揭示]//0:一般;1:試算揭示// 0331-會無歷史tick
            if (strData != "")// &&( (chkBoxSimulate.Checked) || (!chkBoxSimulate.Checked && nSimulate == 0)))
                listTicks.Items.Add("[OnNotifyHistoryTickLONG]" + strData);

            //if (listTicks.Items.Count < 200)
            //    listTicks.SelectedIndex = listTicks.Items.Count - 1;
            //else
            //    listTicks.Items.Clear();
        }

        void m_SKQuoteLib_OnNotifyBest5(short sMarketNo, int nStockIdx , int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
        {
            //0:一般;1:試算揭示
            if (nSimulate == 0)
            {
                GridBest5Ask.ForeColor = Color.Black;
                GridBest5Bid.ForeColor = Color.Black;
            }
            else
            {
                GridBest5Ask.ForeColor = Color.Gray;
                GridBest5Bid.ForeColor = Color.Gray;
            }

            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();
            double dDigitNum = 0.000;
            string strStockNoTick = txtTick.Text.Trim();

            int nCode = 0;
            //[--]//
            if (sMarketNo == 6 || sMarketNo == 5)
                nCode = m_SKQuoteLib.SKQuoteLib_GetStockByMarketAndNo(sMarketNo, strStockNoTick,ref pSKStockLONG);
            else 
                nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(strStockNoTick, ref pSKStockLONG);
            //[-1022-a-]
            if (nCode == 0)
                dDigitNum = (Math.Pow(10, pSKStockLONG.sDecimal));
            else
                dDigitNum = 100.00;//default value

            if (m_dtBest5Ask.Rows.Count == 0 && m_dtBest5Bid.Rows.Count == 0)
            {
                DataRow myDataRow;

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty1;
                if (nBestAsk1 == kMarketPrice)
                   myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestAsk1 / dDigitNum).ToString();///100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty2;

                if (nBestAsk2 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestAsk2 / dDigitNum).ToString();//100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty3;

                if (nBestAsk3 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestAsk3 / dDigitNum).ToString();//100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty4;

                if (nBestAsk4 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestAsk4 / dDigitNum).ToString();// 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty5;

                if (nBestAsk5 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestAsk5 / dDigitNum).ToString();// 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);



                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty1;

                if (nBestBid1 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else myDataRow["m_nAsk"] = (nBestBid1 / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty2;
                if (nBestBid2 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else myDataRow["m_nAsk"] = (nBestBid2 / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty3;
                if (nBestBid3 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestBid3 / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty4;
                if (nBestBid4 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestBid4 / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty5;
                if (nBestBid5 == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (nBestBid5 / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

            }
            else
            {
                m_dtBest5Ask.Rows[0]["m_nAskQty"] = nBestAskQty1;
                if (nBestAsk1 == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[0]["m_nAsk"] = (nBestAsk1 / dDigitNum).ToString();

                m_dtBest5Ask.Rows[1]["m_nAskQty"] = nBestAskQty2;
                if (nBestAsk2 == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[1]["m_nAsk"] = (nBestAsk2 / dDigitNum).ToString();

                m_dtBest5Ask.Rows[2]["m_nAskQty"] = nBestAskQty3;
                if (nBestAsk3 == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[2]["m_nAsk"] = (nBestAsk3 / dDigitNum).ToString();

                m_dtBest5Ask.Rows[3]["m_nAskQty"] = nBestAskQty4;
                if (nBestAsk4 == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[3]["m_nAsk"] = (nBestAsk4 / dDigitNum).ToString();

                m_dtBest5Ask.Rows[4]["m_nAskQty"] = nBestAskQty5;
                if (nBestAsk5 == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[4]["m_nAsk"] = (nBestAsk5 / dDigitNum).ToString();


                m_dtBest5Bid.Rows[0]["m_nAskQty"] = nBestBidQty1;
                if (nBestBid1 == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[0]["m_nAsk"] = (nBestBid1 / dDigitNum).ToString();

                m_dtBest5Bid.Rows[1]["m_nAskQty"] = nBestBidQty2;
                if (nBestBid2 == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[1]["m_nAsk"] = (nBestBid2 / dDigitNum).ToString();

                m_dtBest5Bid.Rows[2]["m_nAskQty"] = nBestBidQty3;
                if (nBestBid3 == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[2]["m_nAsk"] = (nBestBid3 / dDigitNum).ToString();

                m_dtBest5Bid.Rows[3]["m_nAskQty"] = nBestBidQty4;
                if (nBestBid4 == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[3]["m_nAsk"] = (nBestBid4 / dDigitNum).ToString();

                m_dtBest5Bid.Rows[4]["m_nAskQty"] = nBestBidQty5;
                if (nBestBid5 == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[4]["m_nAsk"] = (nBestBid5 / dDigitNum).ToString();
            }

        }

        void m_SKQuoteLib_OnNotifyServerTime(short sHour, short sMinute, short sSecond, int nTotal)
        {
            lblServerTime.Text = sHour.ToString("D2") + ":" + sMinute.ToString("D2") + ":" + sSecond.ToString("D2");
        }

        void m_SKQuoteLib_OnNotifyKLineData(string bstrStockNo, string bstrData)
        {
            listKLine.Items.Add("[OnNotifyKLineData]" + bstrData);
        }

        void m_SKQuoteLib_OnNotifyMarketTot(short sMarketNo, short sPtr, int nTime, int nTotv, int nTots, int nTotc)
        {
            double dTotv = nTotv / 100.0;

            if (sMarketNo == 0)
            {
                lblTotv.Text = dTotv.ToString() + "(億)";
                lblTots.Text = nTots.ToString() + "(張)";
                lblTotc.Text = nTotc.ToString() + "(筆)";
            }
            else
            {
                lblTotv2.Text = dTotv.ToString() + "(億)";
                lblTots2.Text = nTots.ToString() + "(張)";
                lblTotc2.Text = nTotc.ToString() + "(筆)";
            }
        }

        void m_SKQuoteLib_OnNotifyMarketBuySell(short sMarketNo, short sPtr, int nTime, int nBc, int nSc, int nBs, int nSs)
        {
            if (sMarketNo == 0)
            {
                lbllBc.Text = nBc.ToString() + "(筆)";
                lbllBs.Text = nBs.ToString() + "(張)";
                lbllSc.Text = nSc.ToString() + "(筆)";
                lbllSs.Text = nSs.ToString() + "(張)";
            }
            else
            {
                lbllBc2.Text = nBc.ToString() + "(筆)";
                lbllBs2.Text = nBs.ToString() + "(張)";
                lbllSc2.Text = nSc.ToString() + "(筆)";
                lbllSs2.Text = nSs.ToString() + "(張)";
            }
        }
        
        void m_SKQuoteLib_OnNotifyMarketHighLow(short sMarketNo, short sPtr, int nTime, short sUp, short sDown, short sHigh, short sLow, short sNoChange)
        {
            if (sMarketNo == 0)
            {
                lblsUp.Text = sUp.ToString();
                lblsDown.Text = sDown.ToString();
                lblsHigh.Text = sHigh.ToString();
                lblsLow.Text = sLow.ToString();
                lblsNoChange.Text = sNoChange.ToString();

            }
            else
            {
                lblsUp2.Text = sUp.ToString();
                lblsDown2.Text = sDown.ToString();
                lblsHigh2.Text = sHigh.ToString();
                lblsLow2.Text = sLow.ToString();
                lblsNoChange2.Text = sNoChange.ToString();


            }
        }

        void m_SKQuoteLib_OnNotifyMarketHighLowNoWarrant(short sMarketNo, int sPtr, int nTime, int sUp, int sDown, int sHigh, int sLow, int sNoChange, int sUpNoW, int sDownNoW, int sHighNoW, int sLowNoW, int sNoChangeNoW)
        {
            if (sMarketNo == 0)
            {
                lblsUp.Text = sUp.ToString();
                lblsDown.Text = sDown.ToString();
                lblsHigh.Text = sHigh.ToString();
                lblsLow.Text = sLow.ToString();
                lblsNoChange.Text = sNoChange.ToString();
                lblUpNoW.Text = sUpNoW.ToString();
                lblDownNoW.Text = sDownNoW.ToString();
                lblHighNoW.Text = sHighNoW.ToString();
                lblLowNoW.Text = sLowNoW.ToString();
                lblNoChangeNoW.Text = sNoChangeNoW.ToString();


            }
            else
            {
                lblsUp2.Text = sUp.ToString();
                lblsDown2.Text = sDown.ToString();
                lblsHigh2.Text = sHigh.ToString();
                lblsLow2.Text = sLow.ToString();
                lblsNoChange2.Text = sNoChange.ToString();

                lblUp2NoW.Text = sUpNoW.ToString();
                lblDown2NoW.Text = sDownNoW.ToString();
                lblHigh2NoW.Text = sHighNoW.ToString();
                lblLow2NoW.Text = sLowNoW.ToString();
                lblNoChange2NoW.Text = sNoChangeNoW.ToString();
            }
        }

        void m_SKQuoteLib_OnNotifyMACD(short sMarketNo, int nStockIdx, string bstrMACD, string bstrDIF, string bstrOSC)
        {
            lblMACD.Text = bstrMACD;

            lblDIF.Text = bstrDIF;
            lblOSC.Text = bstrOSC;
        }

        void m_SKQuoteLib_OnNotifyBoolTunel(short sMarketNo, int nStockIdx, string bstrAVG, string bstrUBT, string bstrLBT)
        {
            lblAVG.Text = bstrAVG;
            lblUBT.Text = bstrUBT;
            lblLBT.Text = bstrLBT;
        }

        void m_SKQuoteLib_OnNotifyFutureTradeInfo(string bstrStockNo, short sMarketNo, int nStockIdx, int nBuyTotalCount, int  nSellTotalCount, int  nBuyTotalQty, int nSellTotalQty, int  nBuyDealTotalCount,int  nSellDealTotalCount)
        {
            lblMarketNo.Text = "MarketNo";
            lblStockIdx.Text = "StockIndex";
            lblFTIBc.Text = "TotalBc";
            lblFTISc.Text = "TotalSc";
            lblFTIBq.Text = "TotalBq";
            lblFTISq.Text = "TotalSq";
            lblFTIBDC.Text = "TotalBDC";
            lblFTISDC.Text = "TotalSDC";

            lblMarketNo.Text = sMarketNo.ToString();
            lblStockIdx.Text = nStockIdx.ToString();
            lblFTIBc.Text = nBuyTotalCount.ToString() ;
            lblFTISc.Text = nSellTotalCount.ToString() ;
            lblFTIBq.Text = nBuyTotalQty.ToString() ;
            lblFTISq.Text = nSellTotalQty.ToString() ;
            lblFTIBDC.Text = nBuyDealTotalCount.ToString() ;
            lblFTISDC.Text =  nSellDealTotalCount.ToString();
        }

        void m_SKQuoteLib_OnNotifyStrikePrices(string bstrOptionData)
        {
            //[-0119-]
            string strData = "";
            strData = "[OnNotifyStrikePrices]" + bstrOptionData;
            
            listStrikePrices.Items.Add(strData);
            m_nCount++;
            listStrikePrices.SelectedIndex = listStrikePrices.Items.Count - 1;

            if(bstrOptionData.Substring(0,2) != "##")   //開頭##表結束，不計商品數量
                txt_StrikePriceCount.Text = m_nCount.ToString();
        }
        void m_SKQuoteLib_OnNotifyStockList(short sMarketNo,string bstrStockListData)
        {
            
            string strData = "";
            strData = "[OnNotifyStockList]" + bstrStockListData;

            StockList.Items.Add(strData);
            m_nCount++;
            if (StockList.Items.Count < 200)
                StockList.SelectedIndex = listStrikePrices.Items.Count - 1;
            else
                StockList.Items.Clear();

            Size size = TextRenderer.MeasureText(bstrStockListData, StockList.Font);
            if (StockList.HorizontalExtent < size.Width )
                StockList.HorizontalExtent = size.Width + 200;
        }

        void m_SKQuoteLib_OnNotifyCommodityListWithTypeNo(short sMarketNo, string bstrStockListData)
        {

            string strData = "";
            strData = "[OnNotifyCommodityList]" + bstrStockListData;

            StockList.Items.Add(strData);
            m_nCount++;
            if (StockList.Items.Count < 200)
                StockList.SelectedIndex = listStrikePrices.Items.Count - 1;
            else
                StockList.Items.Clear();

            Size size = TextRenderer.MeasureText(bstrStockListData, StockList.Font);
            if (StockList.HorizontalExtent < size.Width)
                StockList.HorizontalExtent = size.Width + 200;
        }

        void m_SKQuoteLib_OnNotifyOddLotSpreadDeal(short sMarketNo, string bstrStockNo,  int nDealPrice, short sDigit)
        {
            if (sMarketNo == 5 || sMarketNo ==6)
            {

                //m_SKQuoteLib.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, ref pSKStockLONG);

                OnUpDateDataRow2(sMarketNo, bstrStockNo,    nDealPrice,  sDigit);


            }

        }

        public class KBar
        {
            public string StockNo { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public int Open { get; set; }
            public int High { get; set; }
            public int Low { get; set; }
            public int Close { get; set; }
            public int Qty { get; set; }
        }
        private Dictionary<string, KBar> m_5MinBars = new Dictionary<string, KBar>();
        string FormatBar(KBar bar)
        {
            return bar.StockNo + " " +
                   bar.Date + " " +
                   bar.Time.ToString("D4") + " " +
                   bar.Open + " " +
                   bar.High + " " +
                   bar.Low + " " +
                   bar.Close + " " +
                   bar.Qty;
        }
        int Get5MinTimeRightAligned(int hhmm)
        {
            int hh = hhmm / 100;
            int mm = hhmm % 100;

            int total = hh * 60 + mm;

            int bucket = ((total - 1) / 5 + 1) * 5;

            int bH = bucket / 60;
            int bM = bucket % 60;

            return bH * 100 + bM;
        }
        void m_SKQuoteLib_OnNotifyLiveKLineData(int nType, string strStockNo, int nDate, int nTimehm, int nOpen, int nHigh, int nLow, int nClose, int nQty)
        {
            string strData = nType.ToString() + " " + strStockNo + " " + nDate.ToString() + " " + nTimehm.ToString() + " " + nOpen.ToString() + " " + nHigh.ToString() + " " + nLow.ToString() + " " + nClose.ToString() + " " + nQty.ToString();
            listBoxLiveKLine.Items.Add("[OnNotifyLiveKLineData]" + strData);

            if (nType == 1)
            {
                int barTime = Get5MinTimeRightAligned(nTimehm);
                if (!m_5MinBars.ContainsKey(strStockNo))
                {
                    m_5MinBars[strStockNo] = new KBar()
                    {
                        StockNo = strStockNo,
                        Date = nDate,
                        Time = barTime,
                        Open = nOpen,
                        High = nHigh,
                        Low = nLow,
                        Close = nClose,
                        Qty = nQty
                    };

                    return;
                }
                KBar bar = m_5MinBars[strStockNo];
                // 同一根5分K
                if (bar.Date == nDate && bar.Time == barTime)
                {
                    bar.High = Math.Max(bar.High, nHigh);
                    bar.Low = Math.Min(bar.Low, nLow);
                    bar.Close = nClose;
                    bar.Qty += nQty;
                }
                else
                {
                    // 完成上一根5分K
                    string strData2 = bar.StockNo + " " + bar.Date.ToString() + " " + bar.Time.ToString() + " " + bar.Open.ToString() + " " + bar.High.ToString() + " " + bar.Low.ToString() + " " + bar.Close.ToString() + " " + bar.Qty.ToString();
                    listBoxLiveKLine_5.Items.Add("[自組5分K]" + strData2);

                    // 開新的一根
                    m_5MinBars[strStockNo] = new KBar()
                    {
                        StockNo = strStockNo,
                        Date = nDate,
                        Time = barTime,
                        Open = nOpen,
                        High = nHigh,
                        Low = nLow,
                        Close = nClose,
                        Qty = nQty
                    };
                }
            }
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;

            System.Reflection.PropertyInfo aProp =
                        typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        private DataTable CreateStocksDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sTypeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nFutureOI";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nYQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nUp";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nDown";
            myDataTable.Columns.Add(myDataColumn);

            //[-20210719-add-]//
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nCloseS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQtyS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nBidS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nAskS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOddLotPer";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();//[-20240508-Add]
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nDealTime";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_caStockNo"] };

            return myDataTable;
        }

        private void OnUpDateDataRow(SKSTOCKLONG pStockLONG)
        {

            string strStockNo = pStockLONG.bstrStockNo;

            DataRow drFind = m_dtStocks.Rows.Find(strStockNo);
            //SendReturnMessage("Quote", m_nCode, "SKQuoteLib: " + (pStockLONG.nTradingLotFlag).ToString());
            if (drFind == null)
            {
                try
                {
                    DataRow myDataRow = m_dtStocks.NewRow();

                    myDataRow["m_nStockidx"] = pStockLONG.nStockIdx;
                    myDataRow["m_sDecimal"] = pStockLONG.sDecimal;
                    myDataRow["m_sTypeNo"] = pStockLONG.sTypeNo;
                    myDataRow["m_cMarketNo"] = pStockLONG.bstrMarketNo;
                    myDataRow["m_caStockNo"] = pStockLONG.bstrStockNo;
                    myDataRow["m_caName"] = pStockLONG.bstrStockName;
                    myDataRow["m_nOpen"] = pStockLONG.nOpen / (Math.Pow(10, pStockLONG.sDecimal));
                    myDataRow["m_nHigh"] = pStockLONG.nHigh / (Math.Pow(10, pStockLONG.sDecimal));
                    myDataRow["m_nLow"] = pStockLONG.nLow / (Math.Pow(10, pStockLONG.sDecimal));
                    myDataRow["m_nClose"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.sDecimal));
                    myDataRow["m_nTickQty"] = pStockLONG.nTickQty;
                    myDataRow["m_nRef"] = pStockLONG.nRef / (Math.Pow(10, pStockLONG.sDecimal));
                    
                    if (pStockLONG.nBid == kMarketPrice)
                        myDataRow["m_nBid"] = "市價";
                    else
                        myDataRow["m_nBid"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.sDecimal))).ToString();
                    
                    
                    myDataRow["m_nBc"] = pStockLONG.nBc;
                    
                    if (pStockLONG.nAsk == kMarketPrice)
                        myDataRow["m_nAsk"] = "市價";
                    else
                        myDataRow["m_nAsk"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.sDecimal))).ToString();
                    
                    
                    m_nSimulateStock = pStockLONG.nSimulate;                 //成交價/買價/賣價;揭示
                    myDataRow["m_nAc"] = pStockLONG.nAc;
                    myDataRow["m_nTBc"] = pStockLONG.nTBc;
                    myDataRow["m_nTAc"] = pStockLONG.nTAc;
                    myDataRow["m_nFutureOI"] = pStockLONG.nFutureOI;
                    myDataRow["m_nTQty"] = pStockLONG.nTQty;
                    myDataRow["m_nYQty"] = pStockLONG.nYQty;
                    myDataRow["m_nUp"] = pStockLONG.nUp / (Math.Pow(10, pStockLONG.sDecimal));
                    myDataRow["m_nDown"] = pStockLONG.nDown / (Math.Pow(10, pStockLONG.sDecimal));
                    myDataRow["m_nDealTime"] = pStockLONG.nDealTime;//[-20240508-Add]
                    if (pStockLONG.bstrMarketNo == "5" || pStockLONG.bstrMarketNo =="6")
                    {
                        if (m_nSimulateStock == 1) //試算揭示//
                        { 
                            myDataRow["m_nCloseS"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.sDecimal));//"試撮成交價";
                            myDataRow["m_nTickQtyS"] = pStockLONG.nTickQty;//"試撮單量";
                            myDataRow["m_nBidS"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.sDecimal))).ToString();//"試撮買價";
                            myDataRow["m_nAskS"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.sDecimal))).ToString();//"試撮賣價";
                        }
                    }
                    m_dtStocks.Rows.Add(myDataRow);

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            else
            {
                drFind["m_nStockidx"] = pStockLONG.nStockIdx;
                drFind["m_sDecimal"] = pStockLONG.sDecimal;
                drFind["m_sTypeNo"] = pStockLONG.sTypeNo;
                drFind["m_cMarketNo"] = pStockLONG.bstrMarketNo;
                drFind["m_caStockNo"] = pStockLONG.bstrStockNo;
                drFind["m_caName"] = pStockLONG.bstrStockName;
                drFind["m_nOpen"] = pStockLONG.nOpen / (Math.Pow(10, pStockLONG.sDecimal));
                drFind["m_nHigh"] = pStockLONG.nHigh / (Math.Pow(10, pStockLONG.sDecimal));
                drFind["m_nLow"] = pStockLONG.nLow / (Math.Pow(10, pStockLONG.sDecimal));
                drFind["m_nClose"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.sDecimal));
                drFind["m_nTickQty"] = pStockLONG.nTickQty;
                drFind["m_nRef"] = pStockLONG.nRef / (Math.Pow(10, pStockLONG.sDecimal));

                if (pStockLONG.nBid == kMarketPrice)
                    drFind["m_nBid"] = "市價";
                else
                    drFind["m_nBid"] =(pStockLONG.nBid / (Math.Pow(10, pStockLONG.sDecimal))).ToString();
                
                
                drFind["m_nBc"] = pStockLONG.nBc;

                if (pStockLONG.nAsk == kMarketPrice)
                    drFind["m_nAsk"] = "市價";
                else
                    drFind["m_nAsk"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.sDecimal))).ToString();
                
                
                drFind["m_nAc"] = pStockLONG.nAc;
                drFind["m_nTBc"] = pStockLONG.nTBc;
                drFind["m_nTAc"] = pStockLONG.nTAc;
                drFind["m_nFutureOI"] = pStockLONG.nFutureOI;
                drFind["m_nTQty"] = pStockLONG.nTQty;
                drFind["m_nYQty"] = pStockLONG.nYQty;
                drFind["m_nUp"] = pStockLONG.nUp / (Math.Pow(10, pStockLONG.sDecimal));
                drFind["m_nDown"] = pStockLONG.nDown / (Math.Pow(10, pStockLONG.sDecimal));
                m_nSimulateStock = pStockLONG.nSimulate;                 //成交價/買價/賣價;揭示
                drFind["m_nDealTime"] = pStockLONG.nDealTime;//[-20240508-Add]

                if (pStockLONG.bstrMarketNo == "5" || pStockLONG.bstrMarketNo == "6")
                {
                    if (m_nSimulateStock == 1) //試算揭示//
                    {
                        drFind["m_nCloseS"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.sDecimal));//"試撮成交價";
                        drFind["m_nTickQtyS"] = pStockLONG.nTickQty;//"試撮單量";
                        drFind["m_nBidS"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.sDecimal))).ToString();//"試撮買價";
                        drFind["m_nAskS"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.sDecimal))).ToString();//"試撮賣價";
                    }
                }


            }
        }

        private void OnUpDateDataRow2(short sMarketNo, string bstrStockNo, int nDealPrice, short sDigit)
        {

            string strStockNo = bstrStockNo;
            
            DataRow drFind = m_dtStocks.Rows.Find(strStockNo);
            //SendReturnMessage("Quote", m_nCode, "SKQuoteLib: " + (pStockLONG.nTradingLotFlag).ToString());
            if (drFind == null)
            {
                try
                {
                    DataRow myDataRow = m_dtStocks.NewRow();

                    
                    myDataRow["m_nOddLotPer"] =  (nDealPrice / (Math.Pow(10, sDigit))).ToString();
                    

                    m_dtStocks.Rows.Add(myDataRow);

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            else
            {
                string strTemp = drFind["m_nOddLotPer"].ToString();
                if (drFind["m_nOddLotPer"].ToString() == "")//[-20210729-]//
                    drFind["m_nOddLotPer"] =  (nDealPrice / (Math.Pow(10, sDigit))).ToString();



            }
        }

        private DataTable CreateBest5AskTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAskQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;

        }

        #endregion

        private void update_Click(object sender, EventArgs e)
        {
            //m_nCode = m_SKQuoteLib.sk

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_MarketTrading");
        }

        private void btnGetTick_Click(object sender, EventArgs e)
        {
            short sMarket;
            int nStockidx;
            int nPtr;

            if (short.TryParse(txtTickMarket.Text, out sMarket) == false)
                return;

            if (int.TryParse(txtTickStockidx.Text, out nStockidx) == false)
                return;

            if (int.TryParse(txtTickPtr.Text, out nPtr) == false)
                return;

            SKTICK skHistoryTick = new SKTICK();
            m_nCode = m_SKQuoteLib.SKQuoteLib_GetTickLONG(sMarket, nStockidx, nPtr, ref skHistoryTick);
            lblGetTick.Text = "HistoryTick..........................................................";


            if (m_nCode == 0)
            {
                //lblGetTick.Text = skHistoryTick.nTime.ToString() + "/" + skHistoryTick.nBid.ToString() + " " + skHistoryTick.nAsk.ToString() + "/" + skHistoryTick.nClose.ToString() + " ...";
                lblGetTick.Text = skHistoryTick.nTimehms.ToString() + "/" + skHistoryTick.nBid.ToString() + " " + skHistoryTick.nAsk.ToString() + "/" + skHistoryTick.nClose.ToString() + "/"+　skHistoryTick.nQty.ToString()　+ "...";

            }

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_GetOneHistoryTick");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            m_nCode = m_SKQuoteLib.SKQuoteLib_GetMarketBuySellUpDown();
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestMarketBuySellUpDown");
        }

        private void btnGetBool_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestBoolTunel(0, textBool.Text.Trim());
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestBoolTunel");
        }

        private void btnGetMACD_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestMACD(0, textMACD.Text.Trim());
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestMACD");
        }

        private void btnCancelBool_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestBoolTunel(50, textBool.Text.Trim());
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelRequstBoolTunel");
        }

        private void btnCancelMACD_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestMACD(50, textMACD.Text.Trim());
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelRequstMACD");
        }

        private void Btn_RequestFutureTradeInfo_Click(object sender, EventArgs e)
        {

            lblFTIBc.Text = "TotalBc";
            lblFTISc.Text = "TotalSc";
            lblFTIBq.Text = "TotalBq";
            lblFTISq.Text = "TotalSq";
            lblFTIBDC.Text = "TotalBDC";
            lblFTISDC.Text = "TotalSDC";
            
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestFutureTradeInfo( 0, text_StockNo.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestFutureTradeInfo");
        }

        private void btn_cancelFTI_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestFutureTradeInfo(50, text_StockNo.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelRequestFutureTradeInfo");
        }

        private void lblSignal_Paint(object sender, PaintEventArgs e)
        {
           /*if ( lblSignal.ForeColor == Color.DimGray)
                btnDisconnect_Click(this, null);     //session down*///[-20240508-Delete]
        }

        private void GetStrikePrices_Click(object sender, EventArgs e)
        {            
            listStrikePrices.Items.Clear();            

            m_nCode = m_SKQuoteLib.SKQuoteLib_GetStrikePrices();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_GetStrikePrice");
        }

        private void btnKLineAM_Click(object sender, EventArgs e)
        {
            listKLine.Items.Clear();

            short sKLineType = short.Parse(boxKLine.SelectedIndex.ToString());
            short sOutType = short.Parse(boxOutType.SelectedIndex.ToString());
            short sTradeSession = short.Parse(boxTradeSession.SelectedIndex.ToString());

            if (sKLineType < 0)
            {
                MessageBox.Show("請選擇KLine類型");
                return;
            }
            if (sOutType < 0)
            {
                MessageBox.Show("請選擇輸出格式類型");
                return;
            }
            if (sTradeSession < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestKLineAM(txtKLine.Text.Trim(), sKLineType, sOutType, sTradeSession);

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestKLineAM");
            //boxTradeSession
        }

        private void btnKLineAMByDate_Click(object sender, EventArgs e)
        {
            listKLine.Items.Clear();

            short sKLineType = short.Parse(boxKLine_ByDate.SelectedIndex.ToString());
            short sOutType = short.Parse(boxOutType_ByDate.SelectedIndex.ToString());
            short sTradeSession = short.Parse(boxTradeSession_ByDate.SelectedIndex.ToString());
            short sMinuteNumber;

            if (sKLineType < 0)
            {
                MessageBox.Show("請選擇KLine類型");
                return;
            }
            if (sOutType < 0)
            {
                MessageBox.Show("請選擇輸出格式類型");
                return;
            }
            if (sTradeSession < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            if (short.TryParse(txtMinuteNumber.Text, out sMinuteNumber) == false)
            {
                sMinuteNumber = 0;
            }
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestKLineAMByDate(txtKLine_ByDate.Text.Trim(), sKLineType, sOutType, sTradeSession, txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), sMinuteNumber);

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestKLineAMByDate");
        }

        private void RequestStockListBtn_Click(object sender, EventArgs e)
        {
            StockList.Items.Clear();

            if (MarketNo_txt.Text.Trim() == "")
            {
                MessageBox.Show("請輸入市場代碼");
                return;
            }
            short sMarketNo = Convert.ToInt16(MarketNo_txt.Text.Trim());

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestStockList(sMarketNo);
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestStockList");
        }

        private void btnLiveTick_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();
            short sPage;

            if (short.TryParse(txtTickPageNo.Text, out sPage) == false)
                return;

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestLiveTick(ref sPage, txtTick.Text.Trim());

            txtTickPageNo.Text = sPage.ToString();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestLiveTick");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*m_SKQuoteLib2.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib2_OnConnection);

            m_nCode = m_SKQuoteLib2.SKQuoteLib_EnterMonitor();

            nConnected*/
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnIsConnected_Click(object sender, EventArgs e)
        {
            int nConnected = m_SKQuoteLib.SKQuoteLib_IsConnected();

            if (nConnected == 0)
            {
                ConnectedLabel.Text = "False_"+nConnected.ToString();
                ConnectedLabel.BackColor = Color.Red;
            }
            else if (nConnected == 1)
            {
                ConnectedLabel.Text = "True_" + nConnected.ToString();
                ConnectedLabel.BackColor = Color.Green;
            }
            else if (nConnected == 2)
            {
                ConnectedLabel.Text = "False_" + nConnected.ToString();
                ConnectedLabel.BackColor = Color.Yellow;
            }
            else
            {
                ConnectedLabel.Text = "False_" + nConnected.ToString();
                ConnectedLabel.BackColor = Color.DarkRed;
            }
            SendReturnMessage("Quote", nConnected, "SKQuoteLib_IsConnected");
        }

        private void btnCancelStocks_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_CancelRequestStocks(txtStocks.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelRequestStocks");
        }

        private void btn_GetQuoteStatus_Click(object sender, EventArgs e)
        {
            bool bIsFull = false;
            int nCount = 0;
            m_nCode = m_SKQuoteLib.SKQuoteLib_GetQuoteStatus(ref nCount,ref  bIsFull);
            
            
            Status_Lbl.Text = nCount.ToString();
            if (bIsFull == true)
                    Status_Lbl.BackColor = Color.Red;
            else
                Status_Lbl.BackColor = Color.Green;
            
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_GetQuoteStatus");
        }
        private void Btn_GetStockValue_Click(object sender, EventArgs e)
        {
            string[] MarketStocks = txt_TradingLot.Text.Trim().Split(new Char[] { ',' });
            short nMarketNo = -1;
            if (MarketStocks[0] != "")
            { 
                short.TryParse(MarketStocks[0].Trim(), out nMarketNo);

                SKSTOCKLONG pStock = new SKSTOCKLONG();
                string strStockNo = txt_TradingLot.Text;
                int nCode = -1;
                nCode = m_SKQuoteLib.SKQuoteLib_GetStockByMarketAndNo(nMarketNo, MarketStocks[1], ref pStock);
                if (nCode == 0)
                    lbl_StockTradingFlag.Text = pStock.nTradingLotFlag.ToString();
            }
        }

        private void btnQueryOddLot_Click(object sender, EventArgs e)
        {
            short sPage;

            if (short.TryParse(txtPageNo.Text, out sPage) == false)
                return;


            m_dtStocks.Clear();
            gridStocks.ClearSelection();

            gridStocks.DataSource = m_dtStocks;

            gridStocks.Columns["m_nStockidx"].Visible = false;
            gridStocks.Columns["m_sDecimal"].Visible = false;
            gridStocks.Columns["m_sTypeNo"].Visible = false;
            gridStocks.Columns["m_cMarketNo"].Visible = false;
            gridStocks.Columns["m_caStockNo"].HeaderText = "代碼";
            gridStocks.Columns["m_caName"].HeaderText = "名稱";
            gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
            //gridStocks.Columns["m_nHigh"].Visible = false;
            gridStocks.Columns["m_nHigh"].HeaderText = "最高";
            //gridStocks.Columns["m_nLow"].Visible = false;
            gridStocks.Columns["m_nLow"].HeaderText = "最低";
            gridStocks.Columns["m_nClose"].HeaderText = "成交價";
            gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
            gridStocks.Columns["m_nRef"].HeaderText = "昨收價";
            gridStocks.Columns["m_nBid"].HeaderText = "買價";
            //gridStocks.Columns["m_nBc"].Visible = false;
            gridStocks.Columns["m_nBc"].HeaderText = "買量";
            gridStocks.Columns["m_nAsk"].HeaderText = "賣價";
            //gridStocks.Columns["m_nAc"].Visible = false;
            gridStocks.Columns["m_nAc"].HeaderText = "賣量";
            //gridStocks.Columns["m_nTBc"].Visible = false;
            gridStocks.Columns["m_nTBc"].HeaderText = "買盤量";
            //gridStocks.Columns["m_nTAc"].Visible = false;
            gridStocks.Columns["m_nTAc"].HeaderText = "賣盤量";
            gridStocks.Columns["m_nFutureOI"].Visible = false;
            //gridStocks.Columns["m_nTQty"].Visible = false;
            gridStocks.Columns["m_nTQty"].HeaderText = "總量";
            //gridStocks.Columns["m_nYQty"].Visible = false;
            gridStocks.Columns["m_nYQty"].HeaderText = "昨量";
            //gridStocks.Columns["m_nUp"].Visible = false;
            gridStocks.Columns["m_nUp"].HeaderText = "漲停";
            //gridStocks.Columns["m_nDown"].Visible = false;
            gridStocks.Columns["m_nDown"].HeaderText = "跌停";

            gridStocks.Columns["m_nCloseS"].HeaderText = "試撮成交價";
            gridStocks.Columns["m_nTickQtyS"].HeaderText = "試撮單量";            
            gridStocks.Columns["m_nBidS"].HeaderText = "試撮買價";
            gridStocks.Columns["m_nAskS"].HeaderText = "試撮賣價";
            
            gridStocks.Columns["m_nOddLotPer"].HeaderText = "整零價差";
            gridStocks.Columns["m_nDealTime"].HeaderText = "成交時間";//[-20240508Add]

            string[] Stocks = txtStocks.Text.Trim().Split(new Char[] { ',' });
            short sMarketNo = Convert.ToInt16(txtMarketNo.Text);
            foreach (string s in Stocks)
            {
                SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

                //int nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(s.Trim(), ref pSKStockLONG);
               
                int nCode = m_SKQuoteLib.SKQuoteLib_GetStockByMarketAndNo(sMarketNo, s.Trim(), ref pSKStockLONG);

                OnUpDateDataRow(pSKStockLONG);

                if (nCode == 0)
                {
                    OnUpDateDataRow(pSKStockLONG);
                }
            }

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestStocksWithMarketNo(ref sPage, sMarketNo, txtStocks.Text.Trim());

            txtPageNo.Text = sPage.ToString();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestStocksWithMarketNo");
        }

        private void Box_M_CheckedChanged(object sender, EventArgs e)
        {//市價轉換與含msns一致
            if (Box_M.Checked == true)
                chkbox_msms.Checked = true;
            else
                chkbox_msms.Checked = false;
        }

        private void chkbox_msms_CheckedChanged(object sender, EventArgs e)
        {//市價轉換與含msns一致
            if (chkbox_msms.Checked == true)
                Box_M.Checked = true;
            else
                Box_M.Checked = false;
        }

        private void btn_GetSpreadNo_Click(object sender, EventArgs e)
        {
            SKSTOCKLONG pStock = new SKSTOCKLONG();
            string strStockNo = Txt_SpreadNo.Text;
            int nCode = -1;
            nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(strStockNo, ref pStock);
            if (nCode == 0)
                lbl_spreadno.Text = pStock.bstrStockNoSpread.ToString();
        }

        private void buttonGetLiveKLine_Click(object sender, EventArgs e)
        {
            if (txtLiveKLineStockNo.Text == "")
            {
                return;
            }
            string strStockNo = txtLiveKLineStockNo.Text;

            SKKLINE pSKKLine = new SKKLINE();

            m_nCode = m_SKQuoteLib.SKQuoteLib_GetLiveKLineLONG(strStockNo, ref pSKKLine);

            if (m_nCode == 0)
            {
                decimal open = pSKKLine.nOpen / 100m;
                decimal high = pSKKLine.nHigh / 100m;
                decimal low = pSKKLine.nLow / 100m;
                decimal close = pSKKLine.nClose / 100m;

                lblLiveKLineDate.Text = "交易日/成交時間：" + pSKKLine.nDate.ToString() + " " + pSKKLine.nTimehm.ToString("D4");
                lblLiveKLineOHLC.Text = "開高低收：" + open.ToString("") + " " + high.ToString("") + " " + low.ToString("") + " " + close.ToString("") + " ";
                lblLiveKLineQty.Text = "目前量：" + pSKKLine.nQty.ToString("");
            }

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_GetLiveKLineLONG");
        }
    }   
}