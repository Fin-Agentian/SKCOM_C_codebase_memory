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
    public partial class OFStrategyOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        SKOSQuoteLib m_pSKOSQuote = new SKOSQuoteLib(); //海期報價物件
        SKOOQuoteLib m_pSKOOQuote = new SKOOQuoteLib(); //海選報價物件
        // 是否為非同步委託
        bool bAsyncOrder = false;
        // 存[UserID]對應 交易帳號
        Dictionary<string, List<string>> m_dictUserID = new Dictionary<string, List<string>>();
        List<string> allkeys;
        static void AddUserID(Dictionary<string, List<string>> dictUserID, string UserID, string AccountData)
        {
            string[] values = AccountData.Split(',');
            string Account = values[1] + values[3]; // broker ID (IB)4碼 + 帳號7碼
            if (dictUserID.ContainsKey(UserID))
            {
                dictUserID[UserID].Add(Account);
            }
            else
            {
                dictUserID[UserID] = new List<string> { Account };
            }
        }
        public OFStrategyOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    // OCO
                    {
                        //comboBoxnReserved
                        {
                            comboBoxnReserved.Items.Add("0:否");
                            comboBoxnReserved.Items.Add("1:是");
                        }

                        //comboBoxnLongActionFlag
                        {
                            comboBoxnLongActionFlag.Items.Add("0:否");
                            comboBoxnLongActionFlag.Items.Add("1:是");
                        }

                        //comboBoxnLAType
                        {
                            comboBoxnLAType.Items.Add("1:效期內觸發即失效");
                            comboBoxnLAType.Items.Add("3:效期內完全成交即失效");
                        }

                        //comboBoxsBuySell
                        {
                            comboBoxsBuySell.Items.Add("0:買進");
                            comboBoxsBuySell.Items.Add("1:賣出");
                        }

                        // comboBoxnOrderPriceType
                        {
                            comboBoxnOrderPriceType.Items.Add("1:市價");
                            comboBoxnOrderPriceType.Items.Add("2:限價");
                        }

                        //comboBoxsTradeType
                        {
                            comboBoxsTradeType.Items.Add("0:ROD");
                            comboBoxsTradeType.Items.Add("3:IOC");
                            comboBoxsTradeType.Items.Add("4:FOK");
                        }

                        //comboBoxnBuySell2
                        {
                            comboBoxnBuySell2.Items.Add("0:買進");
                            comboBoxnBuySell2.Items.Add("1:賣出");
                        }

                        //comboBoxsDayTrade
                        {
                            comboBoxsDayTrade.Items.Add("0:否");
                            comboBoxsDayTrade.Items.Add("1:是");
                        }

                        // comboBoxnTimeFlag
                        {
                            comboBoxnTimeFlag.Items.Add("1:T盤");
                            comboBoxnTimeFlag.Items.Add("2:T+1盤");
                        }
                    }
                    // AB
                    {
                        // comboBoxnTimeFlagAB
                        {
                            comboBoxnTimeFlagAB.Items.Add("0:否");
                            comboBoxnTimeFlagAB.Items.Add("1:是");
                        }

                        //comboBoxsDayTradeAB
                        {
                            comboBoxsDayTradeAB.Items.Add("0:否");
                            comboBoxsDayTradeAB.Items.Add("1:是");
                        }

                        //comboBoxnBuySell2AB
                        {
                            comboBoxnBuySell2AB.Items.Add("0:買進");
                            comboBoxnBuySell2AB.Items.Add("1:賣出");
                        }

                        //comboBoxsBuySellAB
                        {
                            comboBoxsBuySellAB.Items.Add("0:買進");
                            comboBoxsBuySellAB.Items.Add("1:賣出");
                        }

                        //comboBoxnOrderPriceTypeAB
                        {
                            comboBoxnOrderPriceTypeAB.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeAB.Items.Add("2:限價");
                        }

                        //comboBoxsTradeTypeAB
                        {
                            comboBoxsTradeTypeAB.Items.Add("0:ROD");
                            comboBoxsTradeTypeAB.Items.Add("3:IOC");
                            comboBoxsTradeTypeAB.Items.Add("4:FOK");
                        }

                        // comboBoxnMarketNo
                        {
                            comboBoxnMarketNo.Items.Add("1:國內證");
                            comboBoxnMarketNo.Items.Add("2:國內期");
                            comboBoxnMarketNo.Items.Add("3:國外證");
                            comboBoxnMarketNo.Items.Add("4:國外期");
                        }

                        //comboBoxOFSpecialTradeType
                        {
                            comboBoxOFSpecialTradeType.Items.Add("0:非證券");
                            comboBoxOFSpecialTradeType.Items.Add("1:上市");
                            comboBoxOFSpecialTradeType.Items.Add("2:上櫃");
                        }

                        // comboBoxnTriggerDirection
                        {
                            comboBoxnTriggerDirection.Items.Add("1:GTE大於等於");
                            comboBoxnTriggerDirection.Items.Add("2:LTE小於等於");
                        }

                        // comboBoxsCallPut
                        {
                            comboBoxsCallPut.Items.Add("0:否");
                            comboBoxsCallPut.Items.Add("1:Call");
                            comboBoxsCallPut.Items.Add("2:Put");
                        }

                        // comboBoxnSpreadFlag
                        {
                            comboBoxnSpreadFlag.Items.Add("0:否");
                            comboBoxnSpreadFlag.Items.Add("1:是");
                        }

                        // comboBoxsNewClose
                        {
                            comboBoxsNewClose.Items.Add("0:新倉");
                            comboBoxsNewClose.Items.Add("1:平倉");
                        }
                    }
                    // 刪單
                    {
                        // comboBoxnTradeKind
                        {
                            comboBoxnTradeKind.Items.Add("3：OCO");
                            comboBoxnTradeKind.Items.Add("10：AB單");
                        }

                        // comboBoxnMarket
                        {
                            comboBoxnMarket.Items.Add("1：國內證");
                            comboBoxnMarket.Items.Add("2：國內期");
                            comboBoxnMarket.Items.Add("3：國外證");
                            comboBoxnMarket.Items.Add("4：國外期");
                        }
                    }
                    // 查詢
                    {
                        // comboBoxbstrKind
                        {
                            comboBoxbstrKind.Items.Add("OCO：二擇一");
                            comboBoxbstrKind.Items.Add("AB：AB單");
                        }
                    }
                }
            }
        }
        private void buttonSendOverSeaFutureOCOOrder_Click(object sender, EventArgs e)
        {
            if (textBoxnQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                OVERSEAFUTUREORDER pOrder = new OVERSEAFUTUREORDER();

                string bstrMessage;

                pOrder.bstrFullAccount = comboBoxAccount.Text;// 海期帳號，分公司代碼＋帳號7碼
                pOrder.bstrExchangeNo = textBoxExchangeNo.Text;//交易所代碼
                pOrder.bstrStockNo = textBoxStockNo.Text;//海外期權代號
                pOrder.bstrYearMonth = textBoxbstrYearMonth.Text;//近月商品年月( YYYYMM) 6碼
                pOrder.bstrTrigger = textBoxbstrTrigger.Text;//觸發價
                pOrder.bstrTriggerNumerator = textBoxTriggerNumerator.Text;//觸發價分子
                pOrder.bstrTriggerDenominator = textBoxbstrTriggerDenominator.Text;// 第一隻腳觸發價格分母
                pOrder.bstrOrder = textBoxbstrOrder.Text;//委託價
                pOrder.bstrOrderNumerator = textBoxbstrOrderNumerator.Text;//委託價分子
                pOrder.bstrOrderDenominator = textBoxbstrOrderDenominator.Text; // //第一隻腳委託價格分母

                //0:買進 1:賣出 { 價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
                if (comboBoxsBuySell.Text == "0:買進") pOrder.sBuySell = 0;
                else if (comboBoxsBuySell.Text == "1:賣出") pOrder.sBuySell = 1;

                pOrder.bstrOrder2 = textBoxbstrOrder2.Text;//委託價2 / A商品市價
                pOrder.bstrOrderNumerator2 = textBoxbstrOrderNumerator2.Text;//委託價分子
                pOrder.bstrOrderDenominator2 = textBoxbstrOrderDenominator2.Text; // //第2隻腳委託價格分母
                pOrder.bstrTrigger2 = textBoxbstrTrigger2.Text;//觸發價
                pOrder.bstrTriggerNumerator2 = textBoxbstrTriggerNumerator2.Text;//觸發價分子
                pOrder.bstrTriggerDenominator2 = textBoxbstrTriggerDenominator2.Text;// 第2隻腳觸發價格分母

                //0:買進 1:賣出 { 價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
                if (comboBoxnBuySell2.Text == "0:買進") pOrder.nBuySell2 = 0;
                else if (comboBoxnBuySell2.Text == "1:賣出") pOrder.nBuySell2 = 1;

                // 第一隻腳委託時效(0:ROD, 3:IOC, 4:FOK)
                if (comboBoxsTradeType.Text == "0:ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeType.Text == "3:IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                // 預約單(0:否, 1:是)
                if (comboBoxnReserved.Text == "0:否") pOrder.nReserved = 0;
                else pOrder.nReserved = 1;

                // 預約盤別(1:T盤, 2:T+1盤)
                if (comboBoxnTimeFlag.Text == "1:T盤") pOrder.nTimeFlag = 1;
                else pOrder.nTimeFlag = 2;

                // 是否為長效單(0:否, 1:是)
                if (comboBoxnLongActionFlag.Text == "0:否") pOrder.nLongActionFlag = 0;
                else pOrder.nLongActionFlag = 1;

                // //長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
                pOrder.bstrLongEndDate = textBoxbstrLongEndDate.Text;

                // 觸發結束條件(1:效期內觸發即失效, 3:效期內完全成交即失效)
                if (comboBoxnLAType.Text == "1:效期內觸發即失效") pOrder.nLAType = 1;
                else pOrder.nLAType = 3;

                if (comboBoxnOrderPriceType.Text == "1:市價") pOrder.nOrderPriceType = 1; // 第一隻腳價格委託方式(1:市價,  2:限價)
                else pOrder.nOrderPriceType = 2;

                //當沖0:否 1:是；{海期價差單不提供當沖}
                if (comboBoxsDayTrade.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                pOrder.nQty = int.Parse(textBoxnQty.Text);

                // 送出海外期貨OCO(含長效)委託
                int nCode = m_pSKOrder.SendOverSeaFutureOCOOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendOverSeaFutureOCOOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void checkBoxAsyncOrder_CheckedChanged(object sender, EventArgs e)
        {
            // 是否為非同步委託
            if (checkBoxAsyncOrder.Checked == true) bAsyncOrder = true;
            else bAsyncOrder = false;
        }
        private void comboBoxUserID_DropDown(object sender, EventArgs e)
        {
            m_dictUserID.Clear(); //清空之前的帳號
            // 取回可交易的所有帳號
            int nCode = m_pSKOrder.GetUserAccount();
            // 取得回傳訊息
            string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void SendOrderForm_Load(object sender, EventArgs e)
        {
            tabControlStrategyClass.SelectTab(1);
            //下單帳號資訊
            m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(OnAccount);
            void OnAccount(string bstrLogInID, string bstrAccountData)
            {
                string[] values = bstrAccountData.Split(',');
                if (values[0] == "OF")
                {
                    AddUserID(m_dictUserID, bstrLogInID, bstrAccountData);
                    //獲得所有key
                    if (allkeys != null) allkeys.Clear();
                    allkeys = new List<string>(m_dictUserID.Keys);

                    if (comboBoxUserID.DataSource != null) comboBoxUserID.DataSource = null;
                    comboBoxUserID.DataSource = allkeys;

                    if (comboBoxAccount.DataSource != null) comboBoxAccount.DataSource = null;
                    comboBoxAccount.DataSource = m_dictUserID[comboBoxUserID.Text];
                }
            }
            // 非同步委託結果
            m_pSKOrder.OnAsyncOrder += new _ISKOrderLibEvents_OnAsyncOrderEventHandler(OnAsyncOrder);
            void OnAsyncOrder(int nThreadID, int nCode, string bstrMessage)
            {
                // 取得回傳訊息
                string msg = "TID:" + nThreadID + "收單訊息:" + bstrMessage;
                msg = "【非同步委託結果】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + msg;
                richTextBoxMessage.AppendText(msg + "\n");
            }
            // 取回可交易的所有帳號
            {
                int nCode = m_pSKOrder.GetUserAccount();
                // 取得回傳訊息
                string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
            //與報價伺服器建立連線(海期) + (海選)
            {
                int nCode = m_pSKOSQuote.SKOSQuoteLib_EnterMonitorLONG();
                // 取得回傳訊息
                string msg = "【SKOSQuoteLib_EnterMonitorLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                nCode = m_pSKOOQuote.SKOOQuoteLib_EnterMonitorLONG();
                // 取得回傳訊息
                msg = "【SKOOQuoteLib_EnterMonitorLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
            //海期智慧單查詢。透過呼叫 GetOFSmartStrategyReport 後，資訊由該事件回傳
            m_pSKOrder.OnOFSmartStrategyReport += new _ISKOrderLibEvents_OnOFSmartStrategyReportEventHandler(OnOFSmartStrategyReport);
            void OnOFSmartStrategyReport(string bstrData)
            {
                richTextBoxOnOFSmartStrategyReport.AppendText(bstrData + '\n');
            }
        }
        private void buttonendOverSeaFutureABOrder_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyAB.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                OVERSEAFUTUREORDER pOrder = new OVERSEAFUTUREORDER();

                string bstrMessage;
                pOrder.bstrFullAccount = comboBoxAccount.Text;// 海期帳號，分公司代碼＋帳號7碼
                pOrder.bstrExchangeNo = textBoxExchangeNoAB.Text;//交易所代碼
                pOrder.bstrStockNo = textBoxStockNoAB.Text;//海外期權代號
                pOrder.bstrYearMonth = textBoxbstrYearMonthAB.Text;//近月商品年月( YYYYMM) 6碼
                pOrder.bstrYearMonth2 = textBoxbstrYearMonth2.Text;//遠月商品年月( YYYYMM) 6碼

                pOrder.bstrOrder = textBoxbstrOrderAB.Text;//委託價
                pOrder.bstrOrderNumerator = textBoxbstrOrderNumeratorAB.Text;//委託價分子
                pOrder.bstrOrderDenominator = textBoxbstrOrderDenominatorAB.Text; // //第一隻腳委託價格分母

                pOrder.bstrTrigger = textBoxbstrTriggerAB.Text;//觸發價
                pOrder.bstrTriggerNumerator = textBoxTriggerNumeratorAB.Text;//觸發價分子
                pOrder.bstrTriggerDenominator = textBoxbstrTriggerDenominatorAB.Text;// 第一隻腳觸發價格分母

                //0:買進 1:賣出 { 價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
                if (comboBoxsBuySellAB.Text == "0:買進")  pOrder.sBuySell = 0;
                else if (comboBoxsBuySellAB.Text == "1:賣出") pOrder.sBuySell = 1;

                pOrder.bstrOrder2 = textBoxbstrOrder2AB.Text;//委託價2

                //0:買進 1:賣出 { 價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
                if (comboBoxnBuySell2AB.Text == "0:買進") pOrder.nBuySell2 = 0;
                else if (comboBoxnBuySell2AB.Text == "1:賣出") pOrder.nBuySell2 = 1;

                // 第一隻腳委託時效(0:ROD, 3:IOC, 4:FOK)
                if (comboBoxsTradeTypeAB.Text == "0:ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeTypeAB.Text == "3:IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                // 觸價方向(1:GTE大於等於,2:LTE小於等於)
                if (comboBoxnTriggerDirection.Text == "1:GTE大於等於") pOrder.nTriggerDirection = 1;
                else pOrder.nTriggerDirection = 2;

                // 是否為預約單(0:否, 1:是)A商品為國內期選市場時可選擇預約單
                if (comboBoxnTimeFlagAB.Text == "0:否") pOrder.nTimeFlag = 0;
                else pOrder.nTimeFlag = 1;


                if (comboBoxnOrderPriceTypeAB.Text == "1:市價") pOrder.nOrderPriceType = 1; // 價格委託方式(1:市價,  2:限價)
                else pOrder.nOrderPriceType = 2;

                //當沖0:否 1:是；{海期價差單不提供當沖}
                if (comboBoxsDayTradeAB.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                pOrder.nQty = int.Parse(textBoxnQtyAB.Text);

                pOrder.bstrStockNo2 = textBoxbstrStockNo2.Text; // A商品商品代號

                // 市場編號(1:國內證, 2:國內期, 3:國外證, 4:國外期)
                if (comboBoxnMarketNo.Text == "1:國內證") pOrder.nMarketNo = 1;
                else if (comboBoxnMarketNo.Text == "2:國內期") pOrder.nMarketNo = 2;
                else if (comboBoxnMarketNo.Text == "3:國外證") pOrder.nMarketNo = 3;
                else pOrder.nMarketNo = 4;

                // 0:非證券,1:上市, 2:上櫃
                if (comboBoxOFSpecialTradeType.Text == "0:非證券") pOrder.sSpecialTradeType = 0;
                else if (comboBoxOFSpecialTradeType.Text == "1:上市") pOrder.sSpecialTradeType = 1;
                else pOrder.sSpecialTradeType = 2;

                pOrder.bstrExchangeNo2 = textBoxbstrExchangeNo2.Text; // B商品交易所代碼 (EX: CME)

                // 是否為選擇權(0:否, 1:Call, 2:Put)
                if (comboBoxsCallPut.Text == "0:否") pOrder.sCallPut = 0;
                else if (comboBoxsCallPut.Text == "1:Call") pOrder.sCallPut = 1;
                else pOrder.sCallPut = 2;

                pOrder.bstrStrikePrice = textBoxbstrStrikePrice.Text; // 履約價格(非選擇權商品請填0)

                // 是否委託價差商品(0:否, 1:是)
                if (comboBoxnSpreadFlag.Text == "0:否") pOrder.nSpreadFlag = 0;
                else pOrder.nSpreadFlag = 1;

                pOrder.bstrYearMonth2 = textBoxbstrYearMonth2.Text; // 商品契約月份2(YYYYMM共6碼, EX: 202206)，非價差商品請填0

                // //新平倉(0:新倉, 1:平倉) 選擇權僅新、平倉，期貨商品僅新倉
                if (comboBoxsNewClose.Text == "0:新倉") pOrder.sNewClose = 0;
                else pOrder.sNewClose = 1;

                // 送出海外期貨AB單委託
                int nCode = m_pSKOrder.SendOverSeaFutureABOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendOverSeaFutureABOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }

        private void buttonCancelOFStrategyOrder_Click(object sender, EventArgs e)
        {
            CANCELSTRATEGYORDER pOrder = new CANCELSTRATEGYORDER();

            pOrder.bstrFullAccount = comboBoxAccount.Text;// 海期帳號，分公司代碼＋帳號7碼
            pOrder.bstrSmartKey = textBoxbstrSmartKey.Text; // 智慧單號 
            // 3：OCO、10：AB單
            if (comboBoxnTradeKind.Text == "3：OCO") pOrder.nTradeKind = 3;
            else pOrder.nTradeKind = 10;
            pOrder.bstrSeqNo = textBoxbstrSeqNo.Text; // 委託序號 (預約單可忽略)
            pOrder.bstrOrderNo = textBoxbstrOrderNo.Text; // 委託書號（若觸發，需給書號）
            pOrder.bstrLongActionKey = textBoxbstrLongActionKey.Text; // 長效單號(非長效單可忽略)

            //市場別 1：國內證、2：國內期、3：國外證、4：國外期
            if (comboBoxnMarket.Text == "1：國內證") pOrder.nMarket = 1;
            else if (comboBoxnMarket.Text == "2：國內期") pOrder.nMarket = 2;
            else if (comboBoxnMarket.Text == "3：國外證") pOrder.nMarket = 3;
            else if (comboBoxnMarket.Text == "4：國外期") pOrder.nMarket = 4;
            string bstrMessage;
            // 取消海期智慧單委託。刪單欄位請參考GetOFStrategyOrder 回傳的內容
            int nCode = m_pSKOrder.CancelOFStrategyOrder(comboBoxUserID.Text, ref pOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelOFStrategyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            // 取得回傳訊息(bstrMessage)
            if (bAsyncOrder == false)
            {
                msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void buttonGetOFSmartStrategyReport_Click(object sender, EventArgs e)
        {
            string bstrKind; // 智慧單類型(OCO：二擇一、AB：AB單)
            string bstrDate = textBoxbstrDate.Text; // 查詢日期(ex:20201001)

            if (comboBoxbstrKind.Text == "OCO：二擇一") bstrKind = "OCO";
            else bstrKind = "AB";

            // 查詢海期智慧單
            int nCode = m_pSKOrder.GetOFSmartStrategyReport(comboBoxUserID.Text, comboBoxAccount.Text, "OF", 0, bstrKind, bstrDate);
            // 取得回傳訊息
            string msg = "【GetOFSmartStrategyReport】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonCancelStrategyList_Click(object sender, EventArgs e)
        {
            CANCELSTRATEGYORDER pCancelOrder = new CANCELSTRATEGYORDER(); // 智慧單刪單物件
            pCancelOrder.bstrLogInID = comboBoxUserID.Text;
            pCancelOrder.bstrFullAccount = comboBoxAccount.Text;
            //市場別 1：國內證、2：國內期、3：國外證、4：國外期
            if (comboBoxnMarket.Text == "1：國內證") pCancelOrder.nMarket = 1;
            else if (comboBoxnMarket.Text == "2：國內期") pCancelOrder.nMarket = 2;
            else if (comboBoxnMarket.Text == "3：國外證") pCancelOrder.nMarket = 3;
            else if (comboBoxnMarket.Text == "4：國外期") pCancelOrder.nMarket = 4;

            pCancelOrder.bstrSmartKey = textBoxbstrSmartKey.Text; // 智慧單號

            string bstrMessage;
            // 取消多筆智慧單委託。刪單欄位請參考智慧單被動回報回傳的內容。
            int nCode = m_pSKOrder.CancelStrategyList(comboBoxUserID.Text, pCancelOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelStrategyList】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
