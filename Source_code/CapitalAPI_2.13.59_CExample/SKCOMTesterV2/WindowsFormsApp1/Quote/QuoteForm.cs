using System;
using SKCOMLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class QuoteForm : Form
    {
        // 關閉標誌
        bool isClosing = false;
        string m_UserID;
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
        SKQuoteLib m_pSKQuote = new SKQuoteLib(); // 國內報價物件
        public QuoteForm(string UserID)
        {
            //Init
            {
                InitializeComponent();
                m_UserID = UserID;
                //dataGridView
                {
                    // Ticks五檔初始化
                    {
                        dataGridViewTicks.Columns.Add("Column1", "買量");
                        dataGridViewTicks.Columns.Add("Column2", "買價");
                        dataGridViewTicks.Columns.Add("Column3", "賣價");
                        dataGridViewTicks.Columns.Add("Column4", "賣量");

                        for (int i = 0; i < 5; i++)
                            dataGridViewTicks.Rows.Add();
                    }
                    // dataGridViewOnNotifyTicksLONG
                    {
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column1", "交易日期");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column2", "時：分：秒");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column3", "毫秒’微秒");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column4", "買價");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column5", "賣價");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column6", "成交價");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column7", "成交量");

                        for (int i = 0; i < 1; i++)
                            dataGridViewOnNotifyTicksLONG.Rows.Add();
                    }
                    // dataGridViewStocks
                    {
                        dataGridViewStocks.Columns.Add("Column1", "代碼");
                        dataGridViewStocks.Columns.Add("Column2", "名稱");
                        dataGridViewStocks.Columns.Add("Column3", "開盤價");
                        dataGridViewStocks.Columns.Add("Column4", "成交價");
                        dataGridViewStocks.Columns.Add("Column5", "最高");
                        dataGridViewStocks.Columns.Add("Column6", "最低");
                        dataGridViewStocks.Columns.Add("Column7", "漲停價");
                        dataGridViewStocks.Columns.Add("Column8", "跌停價");
                        dataGridViewStocks.Columns.Add("Column9", "買盤量(外盤)");
                        dataGridViewStocks.Columns.Add("Column10", "賣盤量(內盤)");
                        dataGridViewStocks.Columns.Add("Column11", "總量");
                        dataGridViewStocks.Columns.Add("Column12", "昨收(參考價)");
                        dataGridViewStocks.Columns.Add("Column13", "昨量");
                        dataGridViewStocks.Columns.Add("Column14", "買價");
                        dataGridViewStocks.Columns.Add("Column15", "賣價");
                    }
                    // dataGridViewKLine
                    {
                        dataGridViewKLine.Columns.Add("Column1", "月/日/年 時:分");
                        dataGridViewKLine.Columns.Add("Column2", "開盤價");
                        dataGridViewKLine.Columns.Add("Column3", "最高價");
                        dataGridViewKLine.Columns.Add("Column4", "最低價");
                        dataGridViewKLine.Columns.Add("Column5", "收盤價");
                        dataGridViewKLine.Columns.Add("Column6", "成交量");
                    }
                    // dataGridViewOnNotifyCommodityListWithTypeNo
                    {
                        dataGridViewOnNotifyCommodityListWithTypeNo.Columns.Add("Column1", "商品代碼");
                        dataGridViewOnNotifyCommodityListWithTypeNo.Columns.Add("Column2", "商名名稱");
                        dataGridViewOnNotifyCommodityListWithTypeNo.Columns.Add("Column3", "最後交易日(僅期權)");
                        dataGridViewOnNotifyCommodityListWithTypeNo.Columns.Add("Column4", "交易所商品代碼(僅期權)");
                    }
                    // OnNotifyFutureTradeInfoLONG
                    {
                        dataGridViewOnNotifyFutureTradeInfoLONG.Columns.Add("Column1", "總委託買進筆數");
                        dataGridViewOnNotifyFutureTradeInfoLONG.Columns.Add("Column2", "總委託賣出筆數");
                        dataGridViewOnNotifyFutureTradeInfoLONG.Columns.Add("Column3", "總委託買進口數");
                        dataGridViewOnNotifyFutureTradeInfoLONG.Columns.Add("Column4", "總委託賣出口數");
                        dataGridViewOnNotifyFutureTradeInfoLONG.Columns.Add("Column5", "總成交買進筆數");
                        dataGridViewOnNotifyFutureTradeInfoLONG.Columns.Add("Column6", "總成交賣出筆數");
                        for (int i = 0; i < 1; i++)
                        {
                            dataGridViewOnNotifyFutureTradeInfoLONG.Rows.Add();
                        }
                    }
                    //dataGridViewOnNotifyStrikePrices
                    {
                        {
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column1", "選擇權商品代碼");
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column2", "中文名稱");
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column3", "Call 商品買賣權代碼");
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column4", "Put 商品買賣權代碼");
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column5", "履約價");
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column6", "年＋月");
                            dataGridViewOnNotifyStrikePrices.Columns.Add("Column7", "最後交易日");
                        }
                    }
                    //dataGridViewOnNotifyOddLotSpreadDeal
                    {
                        {
                            dataGridViewOnNotifyOddLotSpreadDeal.Columns.Add("Column1", "市場別代號");
                            dataGridViewOnNotifyOddLotSpreadDeal.Columns.Add("Column2", "商品代碼");
                            dataGridViewOnNotifyOddLotSpreadDeal.Columns.Add("Column3", "整零成交價差(負數則含-負號)");
                        }
                    }
                    // dataGridViewSKQuoteLib_GetStockByNoLONG
                    {
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column1", "類股別");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column2", "市埸代碼");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column3", "商品代碼");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column4", "商品名稱");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column5", "最高價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column6", "開盤價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column7", "最低價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column8", "成交價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column9", "單量");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column10", "昨收、參考價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column11", "買價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column12", "買量");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column13", "賣價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column14", "賣量");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column15", "買盤量(即外盤量)");

                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column16", "賣盤量(即內盤量)");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column17", "總量");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column18", "昨量");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column19", "漲停價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column20", "跌停價");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column21", "揭示 0:一般 1:試算(試撮)");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column22", "[限證券整股商品]可否當沖 0:一般 1:可先買後賣現股當沖2:可先買後賣和先賣後買現股當沖");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column23", "交易日(YYYYMMDD)");
                        dataGridViewSKQuoteLib_GetStockByNoLONG.Columns.Add("Column24", "[證券] 整股、盤中零股揭示註記   0:現股 ; 1:盤中零股");
                    }
                }
                //comboBox
                {
                    // KLine
                    {
                        //comboBoxsKLineType
                        {
                            comboBoxsKLineType.Items.Add("分線");
                            comboBoxsKLineType.Items.Add("日線");
                            comboBoxsKLineType.Items.Add("週線");
                            comboBoxsKLineType.Items.Add("月線");
                        }

                        //comboBoxsOutType
                        {
                            comboBoxsOutType.Items.Add("舊版");
                            comboBoxsOutType.Items.Add("新版");
                        }

                        //sTradeSession
                        {
                            comboBoxsTradeSession.Items.Add("全盤");
                            comboBoxsTradeSession.Items.Add("AM盤");
                        }
                    }
                    
                    //comboBoxSKQuoteLib_RequestStockList
                    {
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("上市");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("上櫃");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("期貨");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("選擇權");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("興櫃");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("盤中零股-上市");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("盤中零股-上櫃");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("客製化期貨");
                        comboBoxSKQuoteLib_RequestStockList.Items.Add("客製化選擇權");
                    }
                    //comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo
                    {
                        comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Items.Add("盤中零股-上市(5)");
                        comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Items.Add("盤中零股-上櫃(6)");
                        comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Items.Add("客製化期貨-9");
                        comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Items.Add("客製化選擇權-10");
                    }
                    //comboBoxSKQuoteLib_RequestStocksWithMarketNo
                    {
                        comboBoxSKQuoteLib_RequestStocksWithMarketNo.Items.Add("盤中零股-上市(5)");
                        comboBoxSKQuoteLib_RequestStocksWithMarketNo.Items.Add("盤中零股-上櫃(6)");
                        comboBoxSKQuoteLib_RequestStocksWithMarketNo.Items.Add("客製化期貨-9");
                        comboBoxSKQuoteLib_RequestStocksWithMarketNo.Items.Add("客製化選擇權-10");
                    }
                    // comboBoxnCallPut
                    {
                        comboBoxnCallPut.Items.Add("Call");
                        comboBoxnCallPut.Items.Add("Put");
                    }
                }
            }
        }
        private void QuoteForm_Load(object sender, EventArgs e)
        {
            labelUserID.Text = m_UserID;

            //事件回傳查詢主機時間的結果
            {
                m_pSKQuote.OnNotifyServerTime += new _ISKQuoteLibEvents_OnNotifyServerTimeEventHandler(OnNotifyServerTime);
                void OnNotifyServerTime(short sHour, short sMinute, short sSecond, int nTotal)
                {
                    if (isClosing != true)
                        labelOnNotifyServerTime2.Text = sHour + ":" + sMinute + ":" + sSecond + " Total:" + nTotal;
                }
            }
            // 事件回傳證券市場－整零價差即時行情
            {
                m_pSKQuote.OnNotifyOddLotSpreadDeal += new _ISKQuoteLibEvents_OnNotifyOddLotSpreadDealEventHandler(OnNotifyOddLotSpreadDeal);
                void OnNotifyOddLotSpreadDeal(short sMarketNo, string bstrStockNo, int nDealPrice, short sDigit)
                {
                    if (isClosing != true)
                    {
                        double dResult = (double)nDealPrice;
                        while (sDigit-- != 0)
                        {
                            dResult /= 10.0;
                        }

                        bool found = false; // 找不到就新增一筆資料
                        foreach (DataGridViewRow row in dataGridViewOnNotifyOddLotSpreadDeal.Rows)
                        {
                            if (bstrStockNo == row.Cells[1].Value.ToString()) // 找到就更新價差
                            {
                                row.Cells[2].Value = nDealPrice;
                                found = true;
                                break;
                            }
                        }
                        if (found == false)
                            dataGridViewOnNotifyOddLotSpreadDeal.Rows.Add(sMarketNo, bstrStockNo, dResult);
                    }
                }
            }
            //當連線成功或失敗，會透過此事件函式告知連線結果
            {
                m_pSKQuote.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(OnConnection);
                void OnConnection(int nKind, int nCode)
                {
                    if (isClosing != true)
                    {
                        // 取得回傳訊息
                        string msg = "【OnConnection】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nKind);
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            //事件最佳五檔(國內報價)
            {
                m_pSKQuote.OnNotifyBest5LONG += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(OnNotifyBest5LONG);
                void OnNotifyBest5LONG(short sMarketNo, int nStockidx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
                {
                    if (isClosing != true)
                    {
                        if (nSimulate == 0) labelnSimulate.Text = "一般揭示";
                        else if (nSimulate == 1) labelnSimulate.Text = "試算揭示";

                        if (dataGridViewTicks.Rows.Count > 0)
                        {
                            DataGridViewRow Row1 = dataGridViewTicks.Rows[0];
                            Row1.Cells[0].Value = nBestBidQty1; //買量
                            Row1.Cells[1].Value = nBestBid1 / 100.0; //買價
                            Row1.Cells[2].Value = nBestAsk1 / 100.0; //賣價
                            Row1.Cells[3].Value = nBestAskQty1; //賣量

                            DataGridViewRow Row2 = dataGridViewTicks.Rows[1];
                            Row2.Cells[0].Value = nBestBidQty2; //買量
                            Row2.Cells[1].Value = nBestBid2 / 100.0; //買價
                            Row2.Cells[2].Value = nBestAsk2 / 100.0; //賣價
                            Row2.Cells[3].Value = nBestAskQty2; //賣量

                            DataGridViewRow Row3 = dataGridViewTicks.Rows[2];
                            Row3.Cells[0].Value = nBestBidQty3; //買量
                            Row3.Cells[1].Value = nBestBid3 / 100.0; //買價
                            Row3.Cells[2].Value = nBestAsk3 / 100.0; //賣價
                            Row3.Cells[3].Value = nBestAskQty3; //賣量

                            DataGridViewRow Row4 = dataGridViewTicks.Rows[3];
                            Row4.Cells[0].Value = nBestBidQty4; //買量
                            Row4.Cells[1].Value = nBestBid4 / 100.0; //買價
                            Row4.Cells[2].Value = nBestAsk4 / 100.0; //賣價
                            Row4.Cells[3].Value = nBestAskQty4; //賣量

                            DataGridViewRow Row5 = dataGridViewTicks.Rows[4];
                            Row5.Cells[0].Value = nBestBidQty5; //買量
                            Row5.Cells[1].Value = nBestBid5 / 100.0; //買價
                            Row5.Cells[2].Value = nBestAsk5 / 100.0; //賣價
                            Row5.Cells[3].Value = nBestAskQty5; //賣量
                        }
                    }
                }
            }
            //(LONG index)當有索取的個股成交明細有所異動，即透過向此註冊事件回傳所異動的個股成交明細
            {
                m_pSKQuote.OnNotifyTicksLONG += new _ISKQuoteLibEvents_OnNotifyTicksLONGEventHandler(OnNotifyTicksLONG);
                void OnNotifyTicksLONG(short sMarketNo, int nIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
                {
                    if (isClosing != true)
                    {
                        if (dataGridViewOnNotifyTicksLONG.Rows.Count > 0)
                        {
                            DataGridViewRow Row1 = dataGridViewOnNotifyTicksLONG.Rows[0];
                            Row1.Cells[0].Value = nDate; // 交易日期。(YYYYMMDD)
                            Row1.Cells[1].Value = nTimehms; // 時間1。(時：分：秒)
                            Row1.Cells[2].Value = nTimemillismicros; // 時間2。(‘毫秒"微秒)
                            Row1.Cells[3].Value = nBid / 100.0; // 買價
                            Row1.Cells[4].Value = nAsk / 100.0; // 賣價
                            Row1.Cells[5].Value = nClose / 100.0; // 成交價
                            Row1.Cells[6].Value = nQty; // 成交量
                        }
                    }
                }
            }
            //(LONG index)當首次索取個股成交明細，此事件會回補當天Tick 【沒用到喔】
            {
                //m_pSKQuote.OnNotifyHistoryTicksLONG += new _ISKQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(OnNotifyHistoryTicksLONG);
                void OnNotifyHistoryTicksLONG(short sMarketNo, int nIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
                {
                    if (isClosing != true)
                    {
                        if (dataGridViewOnNotifyTicksLONG.Rows.Count > 0)
                        {
                            DataGridViewRow Row1 = dataGridViewOnNotifyTicksLONG.Rows[0];
                            Row1.Cells[0].Value = nDate; // 交易日期。(YYYYMMDD)
                            Row1.Cells[1].Value = nTimehms; // 時間1。(時：分：秒)
                            Row1.Cells[2].Value = nTimemillismicros; // 時間2。(‘毫秒"微秒)
                            Row1.Cells[3].Value = nBid / 100.0; // 買價
                            Row1.Cells[4].Value = nAsk / 100.0; // 賣價
                            Row1.Cells[5].Value = nClose / 100.0; // 成交價
                            Row1.Cells[6].Value = nQty; // 成交量
                        }
                    }
                }
            }
            //20241120 最後一筆歷史KLine
            m_pSKQuote.OnKLineComplete += new _ISKQuoteLibEvents_OnKLineCompleteEventHandler(OnKLineComplete);
            void OnKLineComplete(string bstrEndString)
            {
                string msg = "【OnKLineComplete】" + bstrEndString;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
            //事件報價通知
            {
                m_pSKQuote.OnNotifyQuoteLONG += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(OnNotifyQuoteLONG);
                void OnNotifyQuoteLONG(short sMarketNo, int nIndex)
                {
                    if (isClosing != true)
                    {
                        bool stockFound = false;
                        SKSTOCKLONG pSKStock = new SKSTOCKLONG();
                        int nCode = m_pSKQuote.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nIndex, ref pSKStock);
                        
                        foreach (DataGridViewRow row in dataGridViewStocks.Rows)
                        {
                            if (row.Cells[0].Value.ToString() == pSKStock.bstrStockNo)
                            {
                                row.Cells[2].Value = pSKStock.nOpen / 100.0;
                                row.Cells[3].Value = pSKStock.nClose / 100.0;
                                row.Cells[4].Value = pSKStock.nHigh / 100.0;
                                row.Cells[5].Value = pSKStock.nLow / 100.0;
                                row.Cells[6].Value = pSKStock.nUp / 100.0;
                                row.Cells[7].Value = pSKStock.nDown / 100.0;
                                row.Cells[8].Value = pSKStock.nTBc.ToString();
                                row.Cells[9].Value = pSKStock.nTAc;
                                row.Cells[10].Value = pSKStock.nTQty;
                                row.Cells[11].Value = pSKStock.nRef / 100.0;
                                row.Cells[12].Value = pSKStock.nYQty;

                                if (pSKStock.nBid == m_pSKQuote.SKQuoteLib_GetMarketPriceTS()) // (買價) 取得證券市場逐筆交易價格欄位為市價時之特殊值
                                {
                                    row.Cells[13].Value = "市價";
                                }
                                else
                                {
                                    row.Cells[13].Value = pSKStock.nBid / 100.0;
                                }

                                if (pSKStock.nBid == m_pSKQuote.SKQuoteLib_GetMarketPriceTS()) // (賣價) 取得證券市場逐筆交易價格欄位為市價時之特殊值
                                {
                                    row.Cells[14].Value = "市價";
                                }
                                else
                                {
                                    row.Cells[14].Value = pSKStock.nAsk / 100.0;
                                }
                                stockFound = true;
                                break;
                            }
                        }

                        if (stockFound == false)
                        {
                            if (pSKStock.nBid == m_pSKQuote.SKQuoteLib_GetMarketPriceTS() && pSKStock.nAsk == m_pSKQuote.SKQuoteLib_GetMarketPriceTS())
                            {
                                dataGridViewStocks.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.nOpen / 100.0, pSKStock.nClose / 100.0, pSKStock.nHigh / 100.0, pSKStock.nLow / 100.0, pSKStock.nUp / 100.0, pSKStock.nDown / 100.0, pSKStock.nTBc.ToString(), pSKStock.nTAc, pSKStock.nTQty, pSKStock.nRef / 100.0, pSKStock.nYQty, "市價", "市價");
                            }
                            else if (pSKStock.nBid == m_pSKQuote.SKQuoteLib_GetMarketPriceTS() && pSKStock.nAsk != m_pSKQuote.SKQuoteLib_GetMarketPriceTS())
                            {
                                dataGridViewStocks.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.nOpen / 100.0, pSKStock.nClose / 100.0, pSKStock.nHigh / 100.0, pSKStock.nLow / 100.0, pSKStock.nUp / 100.0, pSKStock.nDown / 100.0, pSKStock.nTBc.ToString(), pSKStock.nTAc, pSKStock.nTQty, pSKStock.nRef / 100.0, pSKStock.nYQty, "市價", pSKStock.nAsk / 100.0);

                            }
                            else if (pSKStock.nBid != m_pSKQuote.SKQuoteLib_GetMarketPriceTS() && pSKStock.nAsk == m_pSKQuote.SKQuoteLib_GetMarketPriceTS())
                            {
                                dataGridViewStocks.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.nOpen / 100.0, pSKStock.nClose / 100.0, pSKStock.nHigh / 100.0, pSKStock.nLow / 100.0, pSKStock.nUp / 100.0, pSKStock.nDown / 100.0, pSKStock.nTBc.ToString(), pSKStock.nTAc, pSKStock.nTQty, pSKStock.nRef / 100.0, pSKStock.nYQty, pSKStock.nBid / 100.0, "市價");
                            }
                            else
                            {
                                dataGridViewStocks.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.nOpen / 100.0, pSKStock.nClose / 100.0, pSKStock.nHigh / 100.0, pSKStock.nLow / 100.0, pSKStock.nUp / 100.0, pSKStock.nDown / 100.0, pSKStock.nTBc.ToString(), pSKStock.nTAc, pSKStock.nTQty, pSKStock.nRef / 100.0, pSKStock.nYQty, pSKStock.nBid / 100.0, pSKStock.nAsk / 100.0);

                            }

                        }
                    }
                }
            }
            //透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後，事件回傳大盤成交張筆資料
            {
                m_pSKQuote.OnNotifyMarketTot += new _ISKQuoteLibEvents_OnNotifyMarketTotEventHandler(OnNotifyMarketTot);
                void OnNotifyMarketTot(short sMarketNo, short sPtr, int nTime, int nTotv, int nTots, int nTotc)
                {
                    if (isClosing != true)
                    {
                        if (sMarketNo == 0) // 上市
                        {
                            labelMarketTotPtr.Text = "目前第" + sPtr + "筆資料";
                            labelMarketTotTime.Text = "大盤成交時間:" + nTime / 10000 + "點" + (nTime % 10000) / 100 + "分" + nTime % 100 + "秒";
                            labelnTotv.Text = "成交量(億):" + (nTotv / 100.00);
                            labelnTots.Text = "成交張數:" + nTots;
                            labelnTotc.Text = "成交筆數:" + nTotc;
                        }
                        else // 上櫃2
                        {
                            labelMarketTotPtr2.Text = "目前第" + sPtr + "筆資料";
                            labelMarketTotTime2.Text = "大盤成交時間:" + nTime / 10000 + "點" + (nTime % 10000) / 100 + "分" + nTime % 100 + "秒";
                            labelnTotv2.Text = "成交量(億):" + (nTotv / 100.00);
                            labelnTots2.Text = "成交張數:" + nTots;
                            labelnTotc2.Text = "成交筆數:" + nTotc;
                        }
                    }
                }
            }
            //透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後，事件回傳大盤成交買賣張筆數資料
            {
                m_pSKQuote.OnNotifyMarketBuySell += new _ISKQuoteLibEvents_OnNotifyMarketBuySellEventHandler(OnNotifyMarketBuySell);
                void OnNotifyMarketBuySell(short sMarketNo, short sPtr, int nTime, int nBc, int nSc, int nBs, int nSs)
                {
                    if (isClosing != true)
                    {
                        //labelMarketBuySellPtr.Text = "目前第" + sPtr + "筆資料";
                        //labelMarketBuySellTime.Text = "大盤成交時間:" + nTime / 10000 + "點" + (nTime % 10000) / 100 + "分" + nTime % 100 + "秒";
                        if (sMarketNo == 0) // 上市
                        {
                            labelnBs.Text = "成交買進張數:" + nBs;
                            labelnSs.Text = "成交賣出張數:" + nSs;
                            labelnBc.Text = "成交買進筆數:" + nBc;
                            labelnSc.Text = "成交賣出筆數:" + nSc;
                        }
                        else // 上櫃2
                        {
                            labelnBs2.Text = "成交買進張數:" + nBs;
                            labelnSs2.Text = "成交賣出張數:" + nSs;
                            labelnBc2.Text = "成交買進筆數:" + nBc;
                            labelnSc2.Text = "成交賣出筆數:" + nSc;
                        }
                    }
                }
            }
            //透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後，事件回傳大盤成交上漲下跌家數資料(包含『含權證家數』、 『不含權證家數』)
            {
                m_pSKQuote.OnNotifyMarketHighLowNoWarrant += new _ISKQuoteLibEvents_OnNotifyMarketHighLowNoWarrantEventHandler(OnNotifyMarketHighLowNoWarrant);
                void OnNotifyMarketHighLowNoWarrant(short sMarketNo, int nPtr, int nTime, int nUp, int nDown, int nHigh, int nLow, int nNoChange, int nUpNoW, int nDownNoW, int nHighNoW, int nLowNoW, int nNoChangeNoW)
                {
                    if (isClosing != true)
                    {
                        if (sMarketNo == 0) // 上市
                        {
                            labelnUp.Text = "成交上漲家數:" + nUp;
                            labelnDown.Text = "成交下跌家數:" + nDown;
                            labelnHigh.Text = "成交漲停家數:" + nHigh;
                            labelnLow.Text = "成交跌停家數:" + nLow;
                            labelnNoChange.Text = "平盤家數:" + nNoChange;

                            labelnUpNoW.Text = "(不含權證)成交上漲家數:" + nUpNoW;
                            labelnDownNoW.Text = "(不含權證)成交下跌家數:" + nDownNoW;
                            labelnHighNoW.Text = "(不含權證)成交漲停家數:" + nHighNoW;
                            labelnLowNoW.Text = "(不含權證)成交跌停家數:" + nLowNoW;
                            labelnNoChangeNoW.Text = "(不含權證)平盤家數:" + nNoChangeNoW;
                        }
                        else // 上櫃2
                        {
                            labelnUp2.Text = "成交上漲家數:" + nUp;
                            labelnDown2.Text = "成交下跌家數:" + nDown;
                            labelnHigh2.Text = "成交漲停家數:" + nHigh;
                            labelnLow2.Text = "成交跌停家數:" + nLow;
                            labelnNoChange2.Text = "平盤家數:" + nNoChange;

                            labelnUpNoW2.Text = "(不含權證)成交上漲家數:" + nUpNoW;
                            labelnDownNoW2.Text = "(不含權證)成交下跌家數:" + nDownNoW;
                            labelnHighNoW2.Text = "(不含權證)成交漲停家數:" + nHighNoW;
                            labelnLowNoW2.Text = "(不含權證)成交跌停家數:" + nLowNoW;
                            labelnNoChangeNoW2.Text = "(不含權證)平盤家數:" + nNoChangeNoW;
                        }
                    }
                }
            }
            //(LONG index)事件回傳證券市場－技術分析平滑異同平均線MACD數值。（日線－完整）
            {
                m_pSKQuote.OnNotifyMACDLONG += new _ISKQuoteLibEvents_OnNotifyMACDLONGEventHandler(OnNotifyMACDLONG);
                void OnNotifyMACDLONG(short sMarketNo, int nStockidx, string bstrMACD, string bstrDIF, string bstrOSC)
                {
                    // (LONG index)取得商品技術指標MACD資訊。(平滑異同平均線)
                    SKMACD pSKMACD = new SKMACD();
                    pSKMACD.bstrStockNo = textBoxbstrStockNo.Text;
                    pSKMACD.bstrMACD = bstrMACD;
                    pSKMACD.bstrDIF = bstrDIF;
                    pSKMACD.bstrOSC = bstrOSC;

                    int nCode = m_pSKQuote.SKQuoteLib_GetMACDLONG(sMarketNo, nStockidx, ref pSKMACD);

                    // 取得回傳訊息
                    string msg = "【SKQuoteLib_GetMACDLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMessage.AppendText(msg + "\n");

                    richTextBoxMACD.AppendText("【OnNotifyMACDLONG】" + "市場別代號:" + sMarketNo + "系統所編的索引代碼" + nStockidx + "MACD平滑異同平均線:" + pSKMACD.bstrMACD + "DIF:" + pSKMACD.bstrDIF + "OSC:" + pSKMACD.bstrOSC);
                }
            }
            //(LONG index)事件回傳技術分析－布林通道。（日線－完整）
            {
                m_pSKQuote.OnNotifyBoolTunelLONG += new _ISKQuoteLibEvents_OnNotifyBoolTunelLONGEventHandler(OnNotifyBoolTunelLONG);
                void OnNotifyBoolTunelLONG(short sMarketNo, int nStockidx, string bstrAVG, string bstrUBT, string bstrLBT)
                {
                    // (LONG index)取得商品BoolTunel資訊
                    SKBoolTunel pBoolTunel = new SKBoolTunel();
                    pBoolTunel.bstrStockNo = textBoxbstrStockNo.Text;
                    pBoolTunel.bstrAVG = bstrAVG;
                    pBoolTunel.bstrUBT = bstrUBT;
                    pBoolTunel.bstrLBT = bstrLBT;

                    int nCode = m_pSKQuote.SKQuoteLib_GetBoolTunelLONG(sMarketNo, nStockidx, ref pBoolTunel);

                    // 取得回傳訊息
                    string msg = "【SKQuoteLib_GetBoolTunelLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMessage.AppendText(msg + "\n");

                    richTextBoxBoolTunel.AppendText("【OnNotifyBoolTunelLONG】" + "市場別代號:" + sMarketNo + "系統所編的索引代碼" + nStockidx + "均線:" + pBoolTunel.bstrAVG + "通道上端:" + pBoolTunel.bstrUBT + "通道下端:" + pBoolTunel.bstrLBT);
                }
            }
            //事件回傳技術分析資訊
            {
                m_pSKQuote.OnNotifyKLineData += new _ISKQuoteLibEvents_OnNotifyKLineDataEventHandler(OnNotifyKLineData);
                void OnNotifyKLineData(string bstrStockNo, string bstrData)
                {
                    // 回傳字串，技術分析資料
                    string[] values = new string[6];
                    values = bstrData.Split(',');
                    dataGridViewKLine.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5]);

                }
            }
            //事件回傳指定國內市場－各類股商品清單
            {
                m_pSKQuote.OnNotifyCommodityListWithTypeNo += new _ISKQuoteLibEvents_OnNotifyCommodityListWithTypeNoEventHandler(OnNotifyCommodityListWithTypeNo);
                void OnNotifyCommodityListWithTypeNo(short sMarketNo, string bstrStockData)
                {
                    // 先把類別代碼及類別中文名稱取出
                    string[] valueClass = bstrStockData.Split('%');
                    if (bstrStockData != "##,,;")
                    {
                        dataGridViewOnNotifyCommodityListWithTypeNo.Rows.Add("類別代碼:" + valueClass[1] + valueClass[2]);
                        bstrStockData = valueClass[3];
                    }

                    // 先分成各筆資料 ';'
                    string[] valuesSemicolon = bstrStockData.Split(';');
                    for (int i = 0; i < valuesSemicolon.Length - 1; i++) // 切割會把最後一個;也算進去，所以真實長度要-1
                    {
                        string[] valuesComma = valuesSemicolon[i].Split(',');
                        if (bstrStockData != "##,,;") dataGridViewOnNotifyCommodityListWithTypeNo.Rows.Add(valuesComma[0], valuesComma[1], valuesComma[2], valuesComma[3]);
                    }
                }
            }
            //(LONG index)事件回傳接收期貨商品的交易資訊
            {
                m_pSKQuote.OnNotifyFutureTradeInfoLONG += new _ISKQuoteLibEvents_OnNotifyFutureTradeInfoLONGEventHandler(OnNotifyFutureTradeInfoLONG);
                void OnNotifyFutureTradeInfoLONG(string bstrStockNo, short sMarketNo, int nStockidx, int nBuyTotalCount, int nSellTotalCount, int nBuyTotalQty, int nSellTotalQty, int nBuyDealTotalCount, int nSellDealTotalCount)
                {
                    if (dataGridViewOnNotifyFutureTradeInfoLONG.Rows.Count > 0)
                    {
                        DataGridViewRow Row1 = dataGridViewOnNotifyFutureTradeInfoLONG.Rows[0];
                        Row1.Cells[0].Value = nBuyTotalCount; //總委託買進筆數
                        Row1.Cells[1].Value = nSellTotalCount; //總委託賣出筆數
                        Row1.Cells[2].Value = nBuyTotalQty; //總委託買進口數
                        Row1.Cells[3].Value = nSellTotalQty; //總委託賣出口數
                        Row1.Cells[4].Value = nBuyDealTotalCount; //總成交買進筆數
                        Row1.Cells[5].Value = nSellDealTotalCount; //總成交賣出筆數
                    }
                }
            }
            //選擇權資訊。透過呼叫 GetStrikePrices 後，資訊由該事件回傳
            {
                m_pSKQuote.OnNotifyStrikePrices += new _ISKQuoteLibEvents_OnNotifyStrikePricesEventHandler(OnNotifyStrikePrices);
                void OnNotifyStrikePrices(string bstrOptionData)
                {
                    string[] values = bstrOptionData.Split(',');
                    dataGridViewOnNotifyStrikePrices.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                }
            }
        }
        private void QuoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            // 中斷所有Solace伺服器連線
            m_pSKQuote.SKQuoteLib_LeaveMonitor();
        }
        private void checkBoxSKQuoteLib_RequestTicksWithMarketNos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSKQuoteLib_RequestTicksWithMarketNos.Checked)
            {
                comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Visible = true;
                buttonSKQuoteLib_RequestTicksWithMarketNo.Visible = true;
                MessageBox.Show("(含/不含盤中零股)目前使用同物件,因應檔數限制,連線後只能使用其一,重新連線後,即還原限制與設定");
            }
            else
            {
                comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Visible = false;
                buttonSKQuoteLib_RequestTicksWithMarketNo.Visible = false;
            }
        }
        private void checkBoxSKQuoteLib_RequestStocksWithMarketNo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSKQuoteLib_RequestStocksWithMarketNo.Checked)
            {
                comboBoxSKQuoteLib_RequestStocksWithMarketNo.Visible = true;
                buttonSKQuoteLib_RequestStocksWithMarketNo.Visible = true;
                MessageBox.Show("(含/不含盤中零股)目前使用同物件,因應檔數限制,連線後只能使用其一,重新連線後,即還原限制與設定");
            }
            else
            {
                comboBoxSKQuoteLib_RequestStocksWithMarketNo.Visible = false;
                buttonSKQuoteLib_RequestStocksWithMarketNo.Visible = false;
            }
        }
        private void comboBoxSKQuoteLib_RequestStockList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSKQuoteLib_RequestStockList.Text != null) 
                buttonSKQuoteLib_RequestStockList.Enabled = true;
        }
        private void comboBoxsOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxsOutType.SelectedItem != null)
                comboBoxsTradeSession.Enabled = true;
        }
        private void comboBoxsTradeSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxsTradeSession.SelectedItem != null)
                buttonSKQuoteLib_RequestKLineAMByDate.Enabled = true;
        }
        private void comboBoxsKLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxsKLineType.Text == "分線")
            {
                textBoxsMinuteNumber.Enabled = true;
            }
            else
            {
                textBoxsMinuteNumber.Enabled = false;
            }

            if (comboBoxsKLineType.SelectedItem != null)
                comboBoxsOutType.Enabled = true;
        }
        private void buttonEnterMonitor_Click(object sender, EventArgs e)
        {
            // 與報價伺服器連線
            int nCode = m_pSKQuote.SKQuoteLib_EnterMonitorLONG();
            // 取得回傳訊息
            string msg = "【SKQuoteLib_EnterMonitorLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_LeaveMonitor_Click(object sender, EventArgs e)
        {
            // 中斷所有Solace伺服器連線
            int nCode = m_pSKQuote.SKQuoteLib_LeaveMonitor();
            // 取得回傳訊息
            string msg = "【SKQuoteLib_LeaveMonitor】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_IsConnected_Click(object sender, EventArgs e)
        {
            string msg;
            // 檢查目前報價的連線狀態
            int nCode = m_pSKQuote.SKQuoteLib_IsConnected();
            // 取得回傳訊息
            switch (nCode)
            {
                case 0:
                    msg = "斷線";
                    break;
                case 1:
                    msg = "連線中";
                    break;
                case 2:
                    msg = "下載中";
                    break;
                default:
                    msg = "出錯啦";
                    break;
            }
            msg = "【SKQuoteLib_IsConnected】" + msg;
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_GetQuoteStatus_Click(object sender, EventArgs e)
        {
            int pnConnectionCount = 0; //當pnConnectionCount 帶入任一數值，函式庫會回傳連線數，並回傳給呼叫端
            bool pbIsOutLimit = false; //當pbIsOutLimit  帶入false，函式庫會回傳是否超過連線數布林值，並回傳給呼叫端

            // 查詢報價連線狀態(是否超過報價連線限制,連線數資訊)
            int nCode = m_pSKQuote.SKQuoteLib_GetQuoteStatus(ref pnConnectionCount, ref pbIsOutLimit);
            // 取得回傳訊息
            // 當pbIsOutLimit連線數超過限制為false, 連線數為先前已使用連線數(不含當次新連線)
            string msg = "【SKQuoteLib_GetQuoteStatus】" + "連線數:" + pnConnectionCount + "超過限制:" + pbIsOutLimit;
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_RequestStocks_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo2.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            } 
            else
            {
                short psPageNo = short.Parse(textBoxpsPageNo2.Text);
                // 訂閱指定商品即時報價(註冊)
                int nCode = m_pSKQuote.SKQuoteLib_RequestStocks(psPageNo, textBoxStockNos.Text);

                // 取得回傳訊息
                string msg = "【SKQuoteLib_RequestStocks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKQuoteLib_RequestTicks_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 此功能不支援盤中零股。訂閱要求傳送成交明細以及五檔
                short psPageNo = short.Parse(textBoxpsPageNo.Text);
                int nCode = m_pSKQuote.SKQuoteLib_RequestTicks(psPageNo, textBoxTicks.Text);

                // 取得回傳訊息
                string msg = "【SKQuoteLib_RequestTicks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKQuoteLib_Gamma_Click(object sender, EventArgs e)
        {
            if (comboBoxnCallPut.SelectedItem == null || textBoxS.Text =="" || textBoxR.Text == "" || textBoxsigma.Text == "" || textBoxK.Text == "" || textBoxT.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                double Gamma;
                // 輸入1~5得到 Gamma值
                int nCode = m_pSKQuote.SKQuoteLib_Gamma(double.Parse(textBoxS.Text), double.Parse(textBoxK.Text), double.Parse(textBoxR.Text), double.Parse(textBoxT.Text), double.Parse(textBoxsigma.Text), out Gamma);
                labelGamma.Text = "Gamma:" + Gamma;
                // 取得回傳訊息
                string msg = "【SKQuoteLib_Gamma】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                double Vega;
                // 輸入1~5得到 Vega值
                nCode = m_pSKQuote.SKQuoteLib_Vega(double.Parse(textBoxS.Text), double.Parse(textBoxK.Text), double.Parse(textBoxR.Text), double.Parse(textBoxT.Text), double.Parse(textBoxsigma.Text), out Vega);
                labelVega.Text = "Vega:" + Vega;
                // 取得回傳訊息
                msg = "【SKQuoteLib_Vega】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                short nCallPut = 0; // 商品買賣權別( Call: 0 , Put: 1)
                if (comboBoxnCallPut.Text == "Call") nCallPut = 0;
                else if (comboBoxnCallPut.Text == "Put") nCallPut = 1;

                if (comboBoxnCallPut.SelectedItem != null)
                {
                    double Delta;
                    // 輸入1~6得到 Delta值
                    nCode = m_pSKQuote.SKQuoteLib_Delta(nCallPut, double.Parse(textBoxS.Text), double.Parse(textBoxK.Text), double.Parse(textBoxR.Text), double.Parse(textBoxT.Text), double.Parse(textBoxsigma.Text), out Delta);
                    labelDelta.Text = "Delta:" + Delta;
                    // 取得回傳訊息
                    msg = "【SKQuoteLib_Delta】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    double Theta;
                    // 輸入1~6得到 Theta值
                    nCode = m_pSKQuote.SKQuoteLib_Theta(nCallPut, double.Parse(textBoxS.Text), double.Parse(textBoxK.Text), double.Parse(textBoxR.Text), double.Parse(textBoxT.Text), double.Parse(textBoxsigma.Text), out Theta);
                    labelTheta.Text = "Theta:" + Theta;
                    // 取得回傳訊息
                    msg = "【SKQuoteLib_Theta】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    double Rho;
                    // 輸入1~6得到 Rho值
                    nCode = m_pSKQuote.SKQuoteLib_Rho(nCallPut, double.Parse(textBoxS.Text), double.Parse(textBoxK.Text), double.Parse(textBoxR.Text), double.Parse(textBoxT.Text), double.Parse(textBoxsigma.Text), out Rho);
                    labelRho.Text = "Rho:" + Rho;
                    // 取得回傳訊息
                    msg = "【SKQuoteLib_Rho】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSKQuoteLib_RequestKLineAMByDate_Click(object sender, EventArgs e)
        {
            dataGridViewKLine.Rows.Clear();

            short sKLineType = 0;
            short sOutType = 0;
            short sTradeSession = 0;
            string bstrStartDate = textBoxbstrStartDate.Text;
            string bstrEndDate = textBoxbstrEndDate.Text;
            short sMinuteNumber = short.Parse(textBoxsMinuteNumber.Text);

            string selectValue = comboBoxsKLineType.SelectedItem.ToString();
            if (selectValue == "分線") sKLineType = 0;
            else if (selectValue == "日線") sKLineType = 4;
            else if (selectValue == "週線") sKLineType = 5;
            else if (selectValue == "月線") sKLineType = 6;

            selectValue = comboBoxsOutType.SelectedItem.ToString();
            if (selectValue == "舊版") sOutType = 0;
            else if (selectValue == "新版") sOutType = 1;

            selectValue = comboBoxsTradeSession.SelectedItem.ToString();
            if (selectValue == "全盤") sTradeSession = 0;
            else if (selectValue == "AM盤") sTradeSession = 1;

            // （僅提供歷史資料）向報價伺服器提出，取得單一商品技術分析資訊需求，可選AM盤或全盤，可指定日期區間，分K時可指定幾分K
            int nCode = m_pSKQuote.SKQuoteLib_RequestKLineAMByDate(textBoxbstrStockNo.Text, sKLineType, sOutType, sTradeSession, bstrStartDate, bstrEndDate, sMinuteNumber);

            // 取得回傳訊息
            string msg = "【SKQuoteLib_RequestKLineAMByDate】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_RequestStockList_Click(object sender, EventArgs e)
        {
            dataGridViewOnNotifyCommodityListWithTypeNo.Rows.Clear();

            short sMarketNo = 0;

            string selectValue = comboBoxSKQuoteLib_RequestStockList.Text;
            if (selectValue == "上市") sMarketNo = 0;
            else if (selectValue == "上櫃") sMarketNo = 1;
            else if (selectValue == "期貨") sMarketNo = 2;
            else if (selectValue == "選擇權") sMarketNo = 3;
            else if (selectValue == "興櫃") sMarketNo = 4;
            else if (selectValue == "盤中零股-上市") sMarketNo = 5;
            else if (selectValue == "盤中零股-上櫃") sMarketNo = 6;
            else if (selectValue == "客製化期貨") sMarketNo = 9;
            else if (selectValue == "客製化選擇權") sMarketNo = 10;
            // 根據市場別編號，取得國內各市場代碼所包含的商品基本資料相關資訊
            int nCode = m_pSKQuote.SKQuoteLib_RequestStockList(sMarketNo);

            // 取得回傳訊息
            string msg = "【SKQuoteLib_RequestStockList】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_CancelRequestTicks_Click(object sender, EventArgs e)
        {
            // 取消訂閱RequestTicks的成交明細及五檔
            int nCode = m_pSKQuote.SKQuoteLib_CancelRequestTicks(textBoxTicks.Text);

            // 取得回傳訊息
            string msg = "【SKQuoteLib_CancelRequestTicks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_RequestTicksWithMarketNo_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 適用盤中零股，訂閱要求傳送成交明細以及五檔
                short psPageNo = short.Parse(textBoxpsPageNo.Text);
                short sMarketNo = 5; //盤中零股-上市(5)、盤中零股- 上櫃(6)市場代碼
                string selectedValue = comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo.Text;
                if (selectedValue == "盤中零股-上市(5)") sMarketNo = 5;
                else if (selectedValue == "盤中零股-上櫃(6)") sMarketNo = 6;
                else if (selectedValue == "客製化期貨-9") sMarketNo = 9;
                else if (selectedValue == "客製化選擇權-10") sMarketNo = 10;
                int nCode = m_pSKQuote.SKQuoteLib_RequestTicksWithMarketNo(psPageNo, sMarketNo, textBoxTicks.Text);
                // 取得回傳訊息
                string msg = "【SKQuoteLib_RequestTicksWithMarketNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKQuoteLib_CancelRequestStocks_Click(object sender, EventArgs e)
        {
            // 取消訂閱SKQuoteLib_RequestStocks的報價通知，並停止更新商品報價
            int nCode = m_pSKQuote.SKQuoteLib_CancelRequestStocks(textBoxStockNos.Text);

            string[] values = (textBoxStockNos.Text).Split(',');
            List<string> listValues = values.ToList();

            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridViewStocks.Rows)
            {
                foreach(string value in listValues)
                {
                    if (row.Cells[0].Value.ToString() == value)
                    {
                        rowsToRemove.Add(row); // 找到要刪除的Rows
                    }
                }
            }

            // 刪除標記的Rows
            foreach (DataGridViewRow rowToRemove in rowsToRemove)
            {
                dataGridViewStocks.Rows.Remove(rowToRemove);
            }

            // 取得回傳訊息
            string msg = "【SKQuoteLib_CancelRequestStocks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_RequestStocksWithMarketNo_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo2.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 訂閱指定市場別及指定商品即時報價
                // 要求伺服器針對sMarketNo市場別、 bstrStockNos 內的商品代號訂閱商品報價通知動作

                short psPageNo = short.Parse(textBoxpsPageNo2.Text);
                short sMarketNo = 5; //盤中零股-上市(5)、盤中零股- 上櫃(6)市場代碼
                string selectedValue = comboBoxSKQuoteLib_RequestStocksWithMarketNo.Text;
                if (selectedValue == "盤中零股-上市(5)") sMarketNo = 5;
                else if (selectedValue == "盤中零股-上櫃(6)") sMarketNo = 6;
                else if (selectedValue == "客製化期貨-9") sMarketNo = 9;
                else if (selectedValue == "客製化選擇權-10") sMarketNo = 10;
                int nCode = m_pSKQuote.SKQuoteLib_RequestStocksWithMarketNo(psPageNo, sMarketNo, textBoxStockNos.Text);
                // 取得回傳訊息
                string msg = "【SKQuoteLib_RequestStocksWithMarketNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKQuoteLib_RequestFutureTradeInfo_Click(object sender, EventArgs e)
        {
            if (textBoxSKQuoteLib_RequestFutureTradeInfopsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                short psPageNo = short.Parse(textBoxSKQuoteLib_RequestFutureTradeInfopsPageNo.Text);
                string bstrStockNo = textBoxSKQuoteLib_RequestFutureTradeInfobstrStockNo.Text;
                // 取得報價函式庫註冊接收期貨商品的交易資訊
                int nCode = m_pSKQuote.SKQuoteLib_RequestFutureTradeInfo(psPageNo, bstrStockNo);

                // 取得回傳訊息
                string msg = "【SKQuoteLib_RequestFutureTradeInfo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKQuoteLib_GetStrikePrices_Click(object sender, EventArgs e)
        {
            // 取得報價函式庫選擇權交易商品資訊
            int nCode = m_pSKQuote.SKQuoteLib_GetStrikePrices();

            // 取得回傳訊息
            string msg = "【SKQuoteLib_GetStrikePrices】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_GetStockByNoLONG_Click(object sender, EventArgs e)
        {
            string bstrStockNo = textBoxSKQuoteLib_GetStockByNoLONG.Text;

            SKSTOCKLONG pSKStock = new SKSTOCKLONG();
            // (LONG index)根據商品代號，取回商品報價的相關資訊
            int nCode = m_pSKQuote.SKQuoteLib_GetStockByNoLONG(bstrStockNo, ref pSKStock);

            // 取得回傳訊息
            string msg = "【SKQuoteLib_GetStockByNoLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            // SKSTOCKLONG
            bool found = false; // 找到就不新增資料
            {
                foreach (DataGridViewRow row in dataGridViewSKQuoteLib_GetStockByNoLONG.Rows)
                {
                    if (row.IsNewRow)
                        continue;
                    if (row.Cells[2].Value != null)
                    {
                        if (bstrStockNo == row.Cells[2].Value.ToString())// 搜尋過的商品代號
                        {
                            found = true;
                            // Update existing row with new stock data
                            row.Cells[0].Value = pSKStock.sTypeNo;
                            row.Cells[1].Value = pSKStock.bstrMarketNo;
                            row.Cells[2].Value = pSKStock.bstrStockNo;
                            row.Cells[3].Value = pSKStock.bstrStockName;
                            row.Cells[4].Value = pSKStock.nHigh / 100.0;
                            row.Cells[5].Value = pSKStock.nOpen / 100.0;
                            row.Cells[6].Value = pSKStock.nLow / 100.0;
                            row.Cells[7].Value = pSKStock.nClose / 100.0;
                            row.Cells[8].Value = pSKStock.nTickQty;
                            row.Cells[9].Value = pSKStock.nRef / 100.0;
                            row.Cells[10].Value = pSKStock.nBid / 100.0;
                            row.Cells[11].Value = pSKStock.nBc;
                            row.Cells[12].Value = pSKStock.nAsk / 100.0;
                            row.Cells[13].Value = pSKStock.nAc;
                            row.Cells[14].Value = pSKStock.nTBc;
                            row.Cells[15].Value = pSKStock.nTAc;
                            row.Cells[16].Value = pSKStock.nTQty;
                            row.Cells[17].Value = pSKStock.nYQty;
                            row.Cells[18].Value = pSKStock.nUp / 100.0;
                            row.Cells[19].Value = pSKStock.nDown / 100.0;
                            row.Cells[20].Value = pSKStock.nSimulate;
                            row.Cells[21].Value = pSKStock.nDayTrade;
                            row.Cells[22].Value = pSKStock.nTradingDay;
                            row.Cells[23].Value = pSKStock.nTradingLotFlag;
                            break;
                        }
                    }
                    
                }
                if (found == false)
                    dataGridViewSKQuoteLib_GetStockByNoLONG.Rows.Add(pSKStock.sTypeNo, pSKStock.bstrMarketNo, pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.nHigh / 100.0, pSKStock.nOpen / 100.0, pSKStock.nLow / 100.0, pSKStock.nClose / 100.0, pSKStock.nTickQty, pSKStock.nRef / 100.0, pSKStock.nBid / 100.0, pSKStock.nBc, pSKStock.nAsk / 100.0, pSKStock.nAc, pSKStock.nTBc, pSKStock.nTAc, pSKStock.nTQty, pSKStock.nYQty, pSKStock.nUp / 100.0, pSKStock.nDown / 100.0, pSKStock.nSimulate, pSKStock.nDayTrade, pSKStock.nTradingDay, pSKStock.nTradingLotFlag);
            }
        }
        private void buttonSKQuoteLib_RequestServerTime_Click(object sender, EventArgs e)
        {
            // 要求報價主機傳送目前時間。
            int nCode = m_pSKQuote.SKQuoteLib_RequestServerTime();
            // 取得回傳訊息
            string msg = "【SKQuoteLib_RequestServerTime】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_GetMarketBuySellUpDown_Click(object sender, EventArgs e)
        {
            // 要求傳送上市與上櫃大盤資訊(成交數,買賣數,漲跌家數)
            int nCode = m_pSKQuote.SKQuoteLib_GetMarketBuySellUpDown();
            // 取得回傳訊息
            string msg = "【SKQuoteLib_GetMarketBuySellUpDown】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_RequestMACD_Click(object sender, EventArgs e)
        {
            short psPageNo = short.Parse(textBoxSKQuoteLib_RequestMACDpsPageNo.Text);

            string bstrStockNo = textBoxbstrStockNo.Text;

            // 要求傳送商品技術指標MACD。(平滑異同平均線)
            int nCode = m_pSKQuote.SKQuoteLib_RequestMACD(psPageNo, bstrStockNo);

            // 取得回傳訊息
            string msg = "【SKQuoteLib_RequestMACD】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKQuoteLib_RequestBoolTunel_Click(object sender, EventArgs e)
        {
            short psPageNo = short.Parse(textBoxSKQuoteLib_RequestMACDpsPageNo.Text);

            string bstrStockNo = textBoxbstrStockNo.Text;

            // 要求傳送商品布林通道BoolTunel
            int nCode = m_pSKQuote.SKQuoteLib_RequestBoolTunel(psPageNo, bstrStockNo);

            // 取得回傳訊息
            string msg = "【SKQuoteLib_RequestBoolTunel】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
