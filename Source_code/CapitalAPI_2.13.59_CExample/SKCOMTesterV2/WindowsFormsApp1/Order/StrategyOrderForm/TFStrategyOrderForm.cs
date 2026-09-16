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
    public partial class TFStrategyOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        // 是否為非同步委託
        bool bAsyncOrder = false;
        // 存[UserID]對應-交易帳號
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
        public TFStrategyOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    //STP
                    {
                        //comboBoxsTradeType
                        comboBoxsTradeType.Items.Add("ROD");
                        comboBoxsTradeType.Items.Add("IOC");
                        comboBoxsTradeType.Items.Add("FOK");

                        //comboBoxsBuySell
                        comboBoxsBuySell.Items.Add("0:買進");
                        comboBoxsBuySell.Items.Add("1:賣出");

                        //comboBoxsDayTrade
                        comboBoxsDayTrade.Items.Add("0:否");
                        comboBoxsDayTrade.Items.Add("1:是");

                        //comboBoxsNewClose
                        comboBoxsNewClose.Items.Add("0:新倉");
                        comboBoxsNewClose.Items.Add("1:平倉");
                        comboBoxsNewClose.Items.Add("2:自動");

                        // comboBoxnOrderPriceType
                        {
                            comboBoxnOrderPriceType.Items.Add("2: 限價");
                            comboBoxnOrderPriceType.Items.Add("3:範圍市價");
                        }

                        //comboBoxsReserved
                        comboBoxsReserved.Items.Add("0:盤中(T盤及T+1盤)");
                        comboBoxsReserved.Items.Add("1:T盤預約");

                        // comboBoxnLongActionFlag
                        {
                            comboBoxnLongActionFlag.Items.Add("0:否");
                            comboBoxnLongActionFlag.Items.Add("1:是");
                        }

                        // comboBoxnLAType
                        {
                            comboBoxnLAType.Items.Add("1:效期內觸發即失效");
                            comboBoxnLAType.Items.Add("3:效期內完全成交即失效");
                        }
                    }

                    //MST
                    {
                        //comboBoxsTradeTypeMST
                        comboBoxsTradeTypeMST.Items.Add("IOC");
                        comboBoxsTradeTypeMST.Items.Add("FOK");

                        //comboBoxsBuySellMST
                        comboBoxsBuySellMST.Items.Add("0:買進");
                        comboBoxsBuySellMST.Items.Add("1:賣出");

                        //comboBoxsDayTradeMST
                        comboBoxsDayTradeMST.Items.Add("0:否");
                        comboBoxsDayTradeMST.Items.Add("1:是");

                        //comboBoxsNewCloseMST
                        comboBoxsNewCloseMST.Items.Add("0:新倉");
                        comboBoxsNewCloseMST.Items.Add("1:平倉");
                        comboBoxsNewCloseMST.Items.Add("2:自動");

                        // comboBoxnOrderPriceTypeMST
                        {
                            comboBoxnOrderPriceTypeMST.Items.Add("2: 限價");
                            comboBoxnOrderPriceTypeMST.Items.Add("3:範圍市價");
                        }

                        //comboBoxsReservedMST
                        comboBoxsReservedMST.Items.Add("0:盤中(T盤及T+1盤)");
                        comboBoxsReservedMST.Items.Add("1:T盤預約");
                    }

                    //MIT
                    {
                        //comboBoxsTradeTypeMIT
                        comboBoxsTradeTypeMIT.Items.Add("ROD");
                        comboBoxsTradeTypeMIT.Items.Add("IOC");
                        comboBoxsTradeTypeMIT.Items.Add("FOK");

                        //comboBoxsBuySellMIT
                        comboBoxsBuySellMIT.Items.Add("0:買進");
                        comboBoxsBuySellMIT.Items.Add("1:賣出");

                        //comboBoxsDayTradeMIT
                        comboBoxsDayTradeMIT.Items.Add("0:否");
                        comboBoxsDayTradeMIT.Items.Add("1:是");

                        //comboBoxsNewCloseMIT
                        comboBoxsNewCloseMIT.Items.Add("0:新倉");
                        comboBoxsNewCloseMIT.Items.Add("1:平倉");
                        comboBoxsNewCloseMIT.Items.Add("2:自動");

                        // comboBoxnOrderPriceTypeMIT
                        {
                            comboBoxnOrderPriceTypeMIT.Items.Add("2: 限價");
                            comboBoxnOrderPriceTypeMIT.Items.Add("3:範圍市價");
                        }

                        // comboBoxnTriggerDirectionMIT
                        {
                            comboBoxnTriggerDirectionMIT.Items.Add("1:GTE大於等於");
                            comboBoxnTriggerDirectionMIT.Items.Add("2:LTE小於等於");
                        }
                    }

                    //OCO
                    {
                        //comboBoxsTradeTypeOCO
                        comboBoxsTradeTypeOCO.Items.Add("ROD");
                        comboBoxsTradeTypeOCO.Items.Add("IOC");
                        comboBoxsTradeTypeOCO.Items.Add("FOK");

                        //comboBoxsBuySellOCO
                        comboBoxsBuySellOCO.Items.Add("0:買進");
                        comboBoxsBuySellOCO.Items.Add("1:賣出");

                        //comboBoxsDayTradeOCO
                        comboBoxsDayTradeOCO.Items.Add("0:否");
                        comboBoxsDayTradeOCO.Items.Add("1:是");

                        //comboBoxsNewCloseOCO
                        comboBoxsNewCloseOCO.Items.Add("0:新倉");
                        comboBoxsNewCloseOCO.Items.Add("1:平倉");
                        comboBoxsNewCloseOCO.Items.Add("2:自動");

                        // comboBoxnOrderPriceTypeOCO
                        {
                            comboBoxnOrderPriceTypeOCO.Items.Add("2: 限價");
                            comboBoxnOrderPriceTypeOCO.Items.Add("3:範圍市價");
                        }

                        //comboBoxsReservedOCO
                        comboBoxsReservedOCO.Items.Add("0:盤中(T盤及T+1盤)");
                        comboBoxsReservedOCO.Items.Add("1:T盤預約");

                        // comboBoxnLongActionFlagOCO
                        {
                            comboBoxnLongActionFlagOCO.Items.Add("0:否");
                            comboBoxnLongActionFlagOCO.Items.Add("1:是");
                        }

                        // comboBoxnLATypeOCO
                        {
                            comboBoxnLATypeOCO.Items.Add("1:效期內觸發即失效");
                            comboBoxnLATypeOCO.Items.Add("3:效期內完全成交即失效");
                        }

                        // comboBoxsBuySell2OCO
                        {
                            comboBoxsBuySell2OCO.Items.Add("0:買進");
                            comboBoxsBuySell2OCO.Items.Add("1:賣出");
                        }
                    }

                    //AB
                    {
                        //comboBoxsTradeTypeAB
                        comboBoxsTradeTypeAB.Items.Add("ROD");
                        comboBoxsTradeTypeAB.Items.Add("IOC");
                        comboBoxsTradeTypeAB.Items.Add("FOK");

                        //comboBoxsBuySellAB
                        comboBoxsBuySellAB.Items.Add("0:買進");
                        comboBoxsBuySellAB.Items.Add("1:賣出");

                        //comboBoxsDayTradeAB
                        comboBoxsDayTradeAB.Items.Add("0:否");
                        comboBoxsDayTradeAB.Items.Add("1:是");

                        //comboBoxsNewCloseAB
                        comboBoxsNewCloseAB.Items.Add("0:新倉");
                        comboBoxsNewCloseAB.Items.Add("1:平倉");
                        comboBoxsNewCloseAB.Items.Add("2:自動");

                        // comboBoxnOrderPriceTypeAB
                        {
                            comboBoxnOrderPriceTypeAB.Items.Add("2: 限價");
                            comboBoxnOrderPriceTypeAB.Items.Add("3:範圍市價");
                        }

                        //comboBoxsReservedAB
                        comboBoxsReservedAB.Items.Add("0:盤中(T盤及T+1盤)");
                        comboBoxsReservedAB.Items.Add("1:T盤預約");

                        // comboBoxnTriggerDirectionAB
                        {
                            comboBoxnTriggerDirectionAB.Items.Add("1:GTE大於等於");
                            comboBoxnTriggerDirectionAB.Items.Add("2:LTE小於等於");
                        }

                        // comboBoxsBuySell2AB
                        {
                            comboBoxsBuySell2AB.Items.Add("0:買進");
                            comboBoxsBuySell2AB.Items.Add("1:賣出");
                        }

                        // comboBoxnMarketNo
                        {
                            comboBoxnMarketNo.Items.Add("1:國內證");
                            comboBoxnMarketNo.Items.Add("2:國內期");
                            comboBoxnMarketNo.Items.Add("3:國外證");
                            comboBoxnMarketNo.Items.Add("4:國外期");
                        }
                        // comboBoxnTimeFlag
                        {
                            comboBoxnTimeFlag.Items.Add("1:上市");
                            comboBoxnTimeFlag.Items.Add("2:上櫃(不開放權證、興櫃商品)");
                            comboBoxnTimeFlag.Items.Add("非證券商品請填0");
                        }
                        // comboBoxnCallPut
                        {
                            comboBoxnCallPut.Items.Add("0:否");
                            comboBoxnCallPut.Items.Add("1:Call");
                            comboBoxnCallPut.Items.Add("2:Put");
                        }
                        // comboBoxnFlag
                        {
                            comboBoxnFlag.Items.Add("0:否");
                            comboBoxnFlag.Items.Add("1:是");
                        }
                    }

                    //刪單
                    {
                        // comboBoxnTradeKind
                        {
                            comboBoxnTradeKind.Items.Add("3:OCO");
                            comboBoxnTradeKind.Items.Add("5:STP");
                            comboBoxnTradeKind.Items.Add("8:MIT");
                            comboBoxnTradeKind.Items.Add("9:MST");
                            comboBoxnTradeKind.Items.Add("10：AB");
                        }

                        // comboBoxnMarket
                        {
                            comboBoxnMarket.Items.Add("1：國內證");
                            comboBoxnMarket.Items.Add("2：國內期");
                            comboBoxnMarket.Items.Add("3：國外證");
                            comboBoxnMarket.Items.Add("4：國外期");
                        }
                    }

                    //查詢
                    {
                        // comboBoxbstrKind
                        {
                            comboBoxbstrKind.Items.Add("STP:一般停損（含選擇權停損）(長效單)");
                            comboBoxbstrKind.Items.Add("MST:移動停損");
                            comboBoxbstrKind.Items.Add("OCO:二擇一(長效單)");
                            comboBoxbstrKind.Items.Add("MIT(含選擇權MIT)");
                            comboBoxbstrKind.Items.Add("AB：AB單");
                        }
                    }                                    
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
                if (values[0] == "TF")
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
            //新版期貨智慧單(包含停損單、移動停損、二擇一、觸價單)被動回報查詢。透過呼叫 GetStopLossReport 後，資訊由該事件回傳。
            m_pSKOrder.OnStopLossReport += new _ISKOrderLibEvents_OnStopLossReportEventHandler(OnStopLossReport);
            void OnStopLossReport(string bstrData)
            {
                richTextBoxOnStopLossReport.AppendText(bstrData + '\n');
            }
        }
        private void buttonGetOFSmartStrategyReport_Click(object sender, EventArgs e)
        {
            string bstrKind; 
            string bstrDate = textBoxbstrDate.Text; // 查詢日期(ex:20220601)

            if (comboBoxbstrKind.Text == "STP:一般停損（含選擇權停損）(長效單)") bstrKind = "STP";
            else if (comboBoxbstrKind.Text == "MST:移動停損") bstrKind = "MST";
            else if (comboBoxbstrKind.Text == "OCO:二擇一(長效單)") bstrKind = "OCO";
            else if (comboBoxbstrKind.Text == "MIT(含選擇權MIT)") bstrKind = "MIT";
            else bstrKind = "AB";

            // 新版期貨停損委託單查詢
            int nCode = m_pSKOrder.GetStopLossReport(comboBoxUserID.Text, comboBoxAccount.Text, 0, bstrKind, bstrDate);
            // 取得回傳訊息
            string msg = "【GetStopLossReport】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonCancelTFStrategyOrderV1_Click(object sender, EventArgs e)
        {
            CANCELSTRATEGYORDER pCancelOrder = new CANCELSTRATEGYORDER();
            string bstrMessage;

            pCancelOrder.bstrLogInID = comboBoxUserID.Text; //登入ID
            pCancelOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼

            //市場別 1：國內證、2：國內期、3：國外證、4：國外期
            if (comboBoxnMarket.Text == "1：國內證") pCancelOrder.nMarket = 1;
            else if (comboBoxnMarket.Text == "2：國內期") pCancelOrder.nMarket = 2;
            else if (comboBoxnMarket.Text == "3：國外證") pCancelOrder.nMarket = 3;
            else if (comboBoxnMarket.Text == "4：國外期") pCancelOrder.nMarket = 4;

            pCancelOrder.bstrSmartKey = textBoxbstrSmartKey.Text;// 智慧單號

            if (comboBoxnTradeKind.Text == "3:OCO") pCancelOrder.nTradeKind = 3;
            else if (comboBoxnTradeKind.Text == "5:STP") pCancelOrder.nTradeKind = 5;
            else if (comboBoxnTradeKind.Text == "8:MIT") pCancelOrder.nTradeKind = 8;
            else if (comboBoxnTradeKind.Text == "9:MST") pCancelOrder.nTradeKind = 9;
            else pCancelOrder.nTradeKind = 10;

            pCancelOrder.bstrSeqNo = textBoxbstrSeqNo.Text; // //委託序號 (預約單可忽略)
            pCancelOrder.bstrOrderNo = textBoxbstrOrderNo.Text; // 委託書號（若欲刪除之委託已產生書號則需填入；預約單可忽略）
            pCancelOrder.bstrLongActionKey = textBoxbstrLongActionKey.Text; // 長效單號(非長效單可忽略)

            // 新版—取消期貨智慧單委託。已產生書號之委託，請填入書號，否則可能影響解除保證金風控。
            int nCode = m_pSKOrder.CancelTFStrategyOrderV1(ref pCancelOrder, bAsyncOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelTFStrategyOrderV1】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
            // 取得回傳訊息(bstrMessage)
            if (bAsyncOrder == false)
            {
                msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendFutureSTPOrderV1_Click(object sender, EventArgs e)
        {
            if (textBoxnQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNo.Text; //委託期權商品代號
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonth.Text;// 委託商品年月，YYYYMM共6碼(EX: 202206)

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewClose.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewClose.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //0:買進 1:賣出
                if (comboBoxsBuySell.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //0:ROD 3:IOC 4:FOK 
                if (comboBoxsTradeType.Text == "ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeType.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTrade.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //委託價格，(指定限價時，需填此欄) 請設sOrderPriceType代表特殊價格「P」範圍市價
                pOrder.bstrPrice = textBoxbstrPrice.Text;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQty.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTrigger.Text;

                //盤別，0:盤中(T盤及T+1盤)；1:T盤預約
                if (comboBoxsReserved.Text == "0:盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else pOrder.sReserved = 1;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceType.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                //是否為長效單(0:否, 1:是)
                if (comboBoxnLongActionFlag.Text == "0:否") pOrder.nLongActionFlag = 0;
                else pOrder.nLongActionFlag = 1;

                //長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
                pOrder.bstrLongEndDate = textBoxbstrLongEndDate.Text;

                //觸發結束條件(1:效期內觸發即失效, 3:效期內完全成交即失效)
                if (comboBoxnLAType.Text == "1:效期內觸發即失效") pOrder.nLAType = 1;
                else pOrder.nLAType = 3;

                string bstrMessage;
                // (指定月份需填商品契約年月)新版—送出期貨停損委託。
                int nCode = m_pSKOrder.SendFutureSTPOrderV1(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureSTPOrderV1】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSendFutureMSTOrderV1_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyMST.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNoMST.Text; //委託期權商品代號

                //3:IOC 4:FOK 
                if (comboBoxsTradeTypeMST.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //0:買進 1:賣出
                if (comboBoxsBuySellMST.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTradeMST.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewCloseMST.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewCloseMST.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQtyMST.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTriggerMST.Text;

                //盤別，0:盤中(T盤及T+1盤)；1:T盤預約
                if (comboBoxsReservedMST.Text == "0:盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else pOrder.sReserved = 1;

                // 委託商品年月，YYYYMM共6碼(EX: 202206)
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonthMST.Text;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceTypeMST.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                //移動點數。{僅移動停損下單使用}
                pOrder.bstrMovingPoint = textBoxbstrMovingPoint.Text;

                string bstrMessage;
                // (指定月份需填商品契約年月)新版—送出移動停損委託。
                int nCode = m_pSKOrder.SendFutureMSTOrderV1(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureMSTOrderV1】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSendFutureMITOrderV1_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyMIT.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNoMIT.Text; //委託期權商品代號
                                                              //委託價格，(指定限價時，需填此欄) 請設sOrderPriceType代表特殊價格「P」範圍市價
                pOrder.bstrPrice = textBoxbstrPriceMIT.Text;

                //0:ROD 3:IOC 4:FOK 
                if (comboBoxsTradeTypeMIT.Text == "ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeTypeMIT.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //0:買進 1:賣出
                if (comboBoxsBuySellMIT.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTradeMIT.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewCloseMIT.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewCloseMIT.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQtyMIT.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTriggerMIT.Text;

                // 委託商品年月，YYYYMM共6碼(EX: 202206)
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonthMIT.Text;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceTypeMIT.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                //成交價 {限MIT下單使用：不可0、不可給特殊價代碼}
                pOrder.bstrDealPrice = textBoxbstrDealPriceMIT.Text;

                //觸發方向1:GTE大於等於, 2:LTE小於等於
                if (comboBoxnTriggerDirectionMIT.Text == "1:GTE大於等於") pOrder.nTriggerDirection = 1;
                else pOrder.nTriggerDirection = 2;

                string bstrMessage;
                // (指定月份需填商品契約年月)新版—送出期貨MIT委託。
                int nCode = m_pSKOrder.SendFutureMITOrderV1(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureMITOrderV1】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSendFutureOCOOrderV1_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyOCO.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNoOCO.Text; //委託期權商品代號

                //0:ROD 3:IOC 4:FOK 
                if (comboBoxsTradeTypeOCO.Text == "ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeTypeOCO.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //0:買進 1:賣出
                if (comboBoxsBuySellOCO.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //買賣別2(0:買進, 1:賣出)，非價差商品請填0
                if (comboBoxsBuySell2OCO.Text == "0:買進") pOrder.sBuySell2 = 0;
                else pOrder.sBuySell2 = 1;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTradeOCO.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewCloseOCO.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewCloseOCO.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQtyOCO.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTriggerOCO.Text;

                //盤別，0:盤中(T盤及T+1盤)；1:T盤預約
                if (comboBoxsReservedOCO.Text == "0:盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else pOrder.sReserved = 1;

                // 委託商品年月，YYYYMM共6碼(EX: 202206)
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonthOCO.Text;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceTypeOCO.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                //是否為長效單(0:否, 1:是)
                if (comboBoxnLongActionFlagOCO.Text == "0:否") pOrder.nLongActionFlag = 0;
                else pOrder.nLongActionFlag = 1;

                //長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
                pOrder.bstrLongEndDate = textBoxbstrLongEndDateOCO.Text;

                //觸發結束條件(1:效期內觸發即失效, 3:效期內完全成交即失效)
                if (comboBoxnLATypeOCO.Text == "1:效期內觸發即失效") pOrder.nLAType = 1;
                else pOrder.nLAType = 3;

                //第一腳委託價格
                pOrder.bstrPrice = textBoxbstrPriceOCO.Text;

                //第二腳委託價格
                pOrder.bstrPrice2 = textBoxbstrPrice2.Text;

                //第二腳觸發價，當市價小於觸發價2時觸發
                pOrder.bstrTrigger2 = textBoxbstrTrigger2.Text;

                //預約單盤別，預設為盤中1:T 	
                pOrder.nTimeFlag = 1;

                string bstrMessage;
                // (指定月份需填商品契約年月)新版—送出期貨二擇一委託。
                int nCode = m_pSKOrder.SendFutureOCOOrderV1(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureOCOOrderV1】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSendFutureABOrder_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyAB.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNoAB.Text; //委託期權商品代號
                pOrder.bstrStockNo2 = textBoxbstrStockNo2.Text; //B商品代號 

                //成交價
                pOrder.bstrDealPrice = textBoxbstrDealPriceAB.Text;

                //觸發方向1:GTE大於等於, 2:LTE小於等於
                if (comboBoxnTriggerDirectionAB.Text == "1:GTE大於等於") pOrder.nTriggerDirection = 1;
                else pOrder.nTriggerDirection = 2;

                //市場編號(1:國內證, 2:國內期, 3:國外證, 4:國外期) ，請輸入A商品市場別
                if (comboBoxnMarketNo.Text == "1:國內證") pOrder.nMarketNo = 1;
                else if (comboBoxnMarketNo.Text == "2:國內期") pOrder.nMarketNo = 2;
                else if (comboBoxnMarketNo.Text == "3:國外證") pOrder.nMarketNo = 3;
                else pOrder.nMarketNo = 4;



                //交易所代碼(EX: TSE、TAIFEX、CME)
                pOrder.bstrCIDTandem = textBoxbstrCIDTandem.Text;

                //1:上市, 2:上櫃(不開放權證、興櫃商品)，非證券商品請填0
                if (comboBoxnTimeFlag.Text == "1:上市") pOrder.nTimeFlag = 1;
                else if (comboBoxnTimeFlag.Text == "2:上櫃(不開放權證、興櫃商品)") pOrder.nTimeFlag = 2;
                else pOrder.nTimeFlag = 0;

                //是否為選擇權(0:否, 1:Call, 2:Put)
                if (comboBoxnCallPut.Text == "0:否") pOrder.nCallPut = 0;
                else if (comboBoxnCallPut.Text == "1:Call") pOrder.nCallPut = 1;
                else pOrder.nCallPut = 2;

                //履約價(非選擇權商品請填0)
                pOrder.bstrStrikePrice = textBoxbstrStrikePrice.Text;

                //是否委託價差商品(0:否, 1:是)
                if (comboBoxnFlag.Text == "0:否") pOrder.nFlag = 0;
                else pOrder.nFlag = 1;

                //商品契約月份2(YYYYMM共6碼, EX: 202206)，非價差商品請填0
                pOrder.bstrSettlementMonth2 = textBoxbstrSettlementMonth2.Text;

                //0:ROD 3:IOC 4:FOK 
                if (comboBoxsTradeTypeAB.Text == "ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeTypeAB.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //0:買進 1:賣出
                if (comboBoxsBuySellAB.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //買賣別2(0:買進, 1:賣出)，非價差商品請填0
                if (comboBoxsBuySell2AB.Text == "0:買進") pOrder.sBuySell2 = 0;
                else pOrder.sBuySell2 = 1;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTradeAB.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewCloseAB.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewCloseAB.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //委託價格，(指定限價時，需填此欄) 請設sOrderPriceType代表特殊價格「P」範圍市價
                pOrder.bstrPrice = textBoxbstrPriceAB.Text;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQtyAB.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTriggerAB.Text;

                //盤別，0:盤中(T盤及T+1盤)；1:T盤預約
                if (comboBoxsReservedAB.Text == "0:盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else pOrder.sReserved = 1;

                // 委託商品年月，YYYYMM共6碼(EX: 202206)
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonthAB.Text;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceTypeAB.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                string bstrMessage;
                // (指定月份需填商品契約年月)新版—送出期貨看A下B委託。
                int nCode = m_pSKOrder.SendFutureABOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureABOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }

        private void buttonSendOptionStopLossOrder_Click(object sender, EventArgs e)
        {
            if (textBoxnQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNo.Text; //委託期權商品代號
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonth.Text;// 委託商品年月，YYYYMM共6碼(EX: 202206)

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewClose.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewClose.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //0:買進 1:賣出
                if (comboBoxsBuySell.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //0:ROD 3:IOC 4:FOK 
                if (comboBoxsTradeType.Text == "ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeType.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTrade.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //委託價格，(指定限價時，需填此欄) 請設sOrderPriceType代表特殊價格「P」範圍市價
                pOrder.bstrPrice = textBoxbstrPrice.Text;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQty.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTrigger.Text;

                //盤別，0:盤中(T盤及T+1盤)；1:T盤預約
                if (comboBoxsReserved.Text == "0:盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else pOrder.sReserved = 1;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceType.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                //是否為長效單(0:否, 1:是)
                if (comboBoxnLongActionFlag.Text == "0:否") pOrder.nLongActionFlag = 0;
                else pOrder.nLongActionFlag = 1;

                //長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
                pOrder.bstrLongEndDate = textBoxbstrLongEndDate.Text;

                //觸發結束條件(1:效期內觸發即失效, 3:效期內完全成交即失效)
                if (comboBoxnLAType.Text == "1:效期內觸發即失效") pOrder.nLAType = 1;
                else pOrder.nLAType = 3;

                string bstrMessage;
                // 送出選擇權停損委託
                int nCode = m_pSKOrder.SendOptionStopLossOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendOptionStopLossOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }

        private void buttonSendOptionMITOrder_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyMIT.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();

                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNoMIT.Text; //委託期權商品代號
                                                                 //委託價格，(指定限價時，需填此欄) 請設sOrderPriceType代表特殊價格「P」範圍市價
                pOrder.bstrPrice = textBoxbstrPriceMIT.Text;

                //0:ROD 3:IOC 4:FOK 
                if (comboBoxsTradeTypeMIT.Text == "ROD") pOrder.sTradeType = 0;
                else if (comboBoxsTradeTypeMIT.Text == "IOC") pOrder.sTradeType = 3;
                else pOrder.sTradeType = 4;

                //0:買進 1:賣出
                if (comboBoxsBuySellMIT.Text == "0:買進") pOrder.sBuySell = 0;
                else pOrder.sBuySell = 1;

                //當沖0:否 1:是，可當沖商品請參考交易所規定。
                if (comboBoxsDayTradeMIT.Text == "0:否") pOrder.sDayTrade = 0;
                else pOrder.sDayTrade = 1;

                //新平倉，0:新倉 1:平倉 2:自動
                if (comboBoxsNewCloseMIT.Text == "0:新倉") pOrder.sNewClose = 0;
                else if (comboBoxsNewCloseMIT.Text == "1:平倉") pOrder.sNewClose = 1;
                else pOrder.sNewClose = 2;

                //交易口數
                pOrder.nQty = int.Parse(textBoxnQtyMIT.Text);

                //觸發價，觸發基準價{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
                pOrder.bstrTrigger = textBoxbstrTriggerMIT.Text;

                // 委託商品年月，YYYYMM共6碼(EX: 202206)
                pOrder.bstrSettlementMonth = textBoxbstrSettlementMonthMIT.Text;

                // 2: 限價; 3:範圍市價 （不支援市價）    
                if (comboBoxnOrderPriceTypeMIT.Text == "2: 限價") pOrder.nOrderPriceType = 2;
                else pOrder.nOrderPriceType = 3;

                //成交價 {限MIT下單使用：不可0、不可給特殊價代碼}
                pOrder.bstrDealPrice = textBoxbstrDealPriceMIT.Text;

                //觸發方向1:GTE大於等於, 2:LTE小於等於
                if (comboBoxnTriggerDirectionMIT.Text == "1:GTE大於等於") pOrder.nTriggerDirection = 1;
                else pOrder.nTriggerDirection = 2;

                string bstrMessage;
                // 送出選擇權MIT委託
                int nCode = m_pSKOrder.SendOptionMITOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendOptionMITOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }

        private void buttonCancelStrategyList_Click(object sender, EventArgs e)
        {
            CANCELSTRATEGYORDER pCancelOrder = new CANCELSTRATEGYORDER(); // 智慧單刪單物件
            pCancelOrder.bstrLogInID = comboBoxUserID.Text; //登入ID
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
