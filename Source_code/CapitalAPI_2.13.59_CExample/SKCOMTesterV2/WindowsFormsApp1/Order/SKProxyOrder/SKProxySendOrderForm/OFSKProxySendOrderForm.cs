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
    public partial class OFSKProxySendOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        SKOSQuoteLib m_pSKOSQuote = new SKOSQuoteLib(); //海期報價物件
        SKOOQuoteLib m_pSKOOQuote = new SKOOQuoteLib(); //海選報價物件
        // 是否為價差交易
        bool Spread = false;
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
        public OFSKProxySendOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    // OF
                    {
                        //sBuySell
                        comboBoxOFBuySell.Items.Add("買進");
                        comboBoxOFBuySell.Items.Add("賣出");

                        //sTradeType
                        comboBoxOFTradeType.Items.Add("ROD");
                        comboBoxOFTradeType.Items.Add("FOK");
                        comboBoxOFTradeType.Items.Add("IOC");

                        //sDayTrade
                        comboBoxOFDayTrade.Items.Add("否");
                        comboBoxOFDayTrade.Items.Add("是");

                        //sSpecialTradeType
                        comboBoxOFSpecialTradeType.Items.Add("LMT限價單");
                        comboBoxOFSpecialTradeType.Items.Add("MKT市價單");
                        comboBoxOFSpecialTradeType.Items.Add("STL停損限價");
                        comboBoxOFSpecialTradeType.Items.Add("STP停損市價");
                    }

                    // OO
                    {
                        //sBuySell
                        comboBoxOOBuySell.Items.Add("買進");
                        comboBoxOOBuySell.Items.Add("賣出");

                        //sNewClose
                        comboBoxOONewClose.Items.Add("新倉");
                        comboBoxOONewClose.Items.Add("平倉");

                        //sDayTrade
                        comboBoxOODayTrade.Items.Add("否");
                        comboBoxOODayTrade.Items.Add("是");

                        //sSpecialTradeType
                        comboBoxOOSpecialTradeType.Items.Add("LMT限價單");
                        comboBoxOOSpecialTradeType.Items.Add("MKT市價單");
                        comboBoxOOSpecialTradeType.Items.Add("STL停損限價");
                        comboBoxOOSpecialTradeType.Items.Add("STP停損市價");

                        //sCallPut
                        comboBoxOOCallPut.Items.Add("CALL");
                        comboBoxOOCallPut.Items.Add("PUT");
                    }
                }
            }          
        }      
        private void buttonSendOverSeaFutureOrder_Click(object sender, EventArgs e)
        {
            if (textBoxOFQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                OVERSEAFUTUREORDER pOrder = new OVERSEAFUTUREORDER();

                string bstrMessage;
                pOrder.bstrFullAccount = comboBoxAccount.Text;// 海期帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxOFStockNo.Text;//海外期權代號
                pOrder.bstrExchangeNo = textBoxOFExchangeNo.Text;//交易所代碼
                pOrder.bstrYearMonth = textBoxOFYearMonth.Text;//近月商品年月( YYYYMM) 6碼
                pOrder.bstrYearMonth2 = textBoxOFYearMonth2.Text;//遠月商品年月( YYYYMM) 6碼
                pOrder.bstrOrder = textBoxOFOrder.Text;//委託價
                pOrder.bstrOrderNumerator = textBoxOFOrderNumerator.Text;//委託價分子
                pOrder.bstrTrigger = textBoxOFTrigger.Text;//觸發價
                pOrder.bstrTriggerNumerator = textBoxOFTriggerNumerator.Text;//觸發價分子

                string selectedValue = comboBoxOFTradeType.Text;//{限價單LMT可選ROD/IOC/FOK，市價單依交易所實際提供為主，其餘單別則固定ROD}
                if (selectedValue == "ROD") pOrder.sTradeType = 0;
                else if (selectedValue == "IOC")  pOrder.sTradeType = 1;
                else if (selectedValue == "FOK")  pOrder.sTradeType = 2;

                selectedValue = comboBoxOFBuySell.Text; //0:買進 1:賣出 { 價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
                if (selectedValue == "買進") pOrder.sBuySell = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell = 1;

                pOrder.sNewClose = 0; //新平倉，0:新倉  {目前海期僅新倉可選}

                selectedValue = comboBoxOFDayTrade.Text; //當沖0:否 1:是；{海期價差單不提供當沖}
                if (selectedValue == "否") pOrder.sDayTrade = 0;
                else if (selectedValue == "是") pOrder.sDayTrade = 1;

                selectedValue = comboBoxOFSpecialTradeType.Text;//0:LMT限價單 1:MKT市價單 2:STL停損限價 3.STP停損市價
                if (selectedValue == "LMT限價單") pOrder.sSpecialTradeType = 0;
                else if (selectedValue == "MKT市價單") pOrder.sSpecialTradeType = 1;
                else if (selectedValue == "STL停損限價") pOrder.sSpecialTradeType = 2;
                else if (selectedValue == "STP停損市價") pOrder.sSpecialTradeType = 3;

                pOrder.nQty = int.Parse(textBoxOFQty.Text);

                if (Spread == false) //不是價差交易
                {
                    // 送出海期委託
                    int nCode = m_pSKOrder.SendOverseaFutureProxyOrder(comboBoxUserID.Text, ref pOrder, out bstrMessage);
                    // 取得回傳訊息
                    string msg = "【SendOverseaFutureProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                }
                else
                {
                    // 送出海期委託
                    int nCode = m_pSKOrder.SendOverseaFutureSpreadProxyOrder(comboBoxUserID.Text, ref pOrder, out bstrMessage);
                    // 取得回傳訊息
                    string msg = "【SendOverseaFutureSpreadProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSendOverseaOptionOrder_Click(object sender, EventArgs e)
        {
            if (textBoxOOQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                OVERSEAFUTUREORDER pOrder = new OVERSEAFUTUREORDER();
                string bstrMessage;

                pOrder.bstrFullAccount = comboBoxAccount.Text;// 海期帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxOOStockNo.Text;//海外期權代號
                pOrder.bstrExchangeNo = textBoxOOExchangeNo.Text;//交易所代碼
                pOrder.bstrYearMonth = textBoxOOYearMonth.Text;//近月商品年月( YYYYMM) 6碼
                pOrder.bstrOrder = textBoxOOOrder.Text;//委託價
                pOrder.bstrOrderNumerator = textBoxOOOrderNumerator.Text;//委託價分子
                pOrder.bstrOrderDenominator = textBoxbstrOrderDenominator.Text; // 委託價分母
                pOrder.bstrTrigger = textBoxOOTrigger.Text;//觸發價
                pOrder.bstrTriggerNumerator = textBoxOOTriggerNumerator.Text;//觸發價分子
                pOrder.sTradeType = 0;//0:ROD  {目前海選均固定ROD}

                string selectedValue = comboBoxOOBuySell.Text; //0:買進 1:賣出 { 價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
                if (selectedValue == "買進") pOrder.sBuySell = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell = 1;

                selectedValue = comboBoxOONewClose.Text; //新平倉，0:新倉 1:平倉 {目前海選可使用新、平倉}
                if (selectedValue == "新倉") pOrder.sNewClose = 0;
                else if (selectedValue == "平倉") pOrder.sNewClose = 1;

                selectedValue = comboBoxOODayTrade.Text; //當沖0:否 1:是；{海期價差單不提供當沖}
                if (selectedValue == "否") pOrder.sDayTrade = 0;
                else if (selectedValue == "是") pOrder.sDayTrade = 1;

                selectedValue = comboBoxOOSpecialTradeType.Text;//0:LMT限價單 1:MKT市價單 2:STL停損限價 3.STP停損市價
                if (selectedValue == "LMT限價單") pOrder.sSpecialTradeType = 0;
                else if (selectedValue == "MKT市價單") pOrder.sSpecialTradeType = 1;
                else if (selectedValue == "STL停損限價") pOrder.sSpecialTradeType = 2;
                else if (selectedValue == "STP停損市價") pOrder.sSpecialTradeType = 3;

                pOrder.bstrStrikePrice = textBoxOOStrikePrice.Text;//履約價。{選擇權使用}


                selectedValue = comboBoxOOCallPut.Text;//0:CALL  1:PUT {選擇權使用} 
                if (selectedValue == "CALL") pOrder.sCallPut = 0;
                else if (selectedValue == "PUT") pOrder.sCallPut = 1;

                pOrder.nQty = int.Parse(textBoxOOQty.Text);//交易口數

                // 送出海選委託
                int nCode = m_pSKOrder.SendOverseaOptionProxyOrder(comboBoxUserID.Text, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendOverseaOptionProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
            }
        }
        private void checkBoxSpread_CheckedChanged(object sender, EventArgs e)
        {
            // 是否為價差交易
            if (checkBoxSpread.Checked)
            {
                Spread = true;

                labelOFYearMonth2.Visible = true;
                textBoxOFYearMonth2.Visible = true;
            }
            else
            { 
                Spread = false;

                labelOFYearMonth2.Visible = false;
                textBoxOFYearMonth2.Visible = false;
            }
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
            // Proxy委託結果
            m_pSKOrder.OnProxyOrder += new _ISKOrderLibEvents_OnProxyOrderEventHandler(OnProxyOrder);
            void OnProxyOrder(int nStampID, int nCode, string bstrMessage)
            {
                // 取得回傳訊息
                string msg = "Time Stamp:" + nStampID + "收單訊息:" + bstrMessage;
                msg = "【Proxy委託結果】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + msg;
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
        }
    }
}
