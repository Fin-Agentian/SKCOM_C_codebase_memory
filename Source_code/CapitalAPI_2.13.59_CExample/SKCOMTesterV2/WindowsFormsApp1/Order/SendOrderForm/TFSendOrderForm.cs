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
    public partial class TFSendOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        // 是否為非同步委託
        bool bAsyncOrder = false;
        // 是否為複式單
        bool isDuplexOrder = false;
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
        public TFSendOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    // TF
                    {
                        //comboBoxFutureTradeType
                        comboBoxFutureTradeType.Items.Add("ROD");
                        comboBoxFutureTradeType.Items.Add("IOC");
                        comboBoxFutureTradeType.Items.Add("FOK");

                        //comboBoxFutureBuySell
                        comboBoxFutureBuySell.Items.Add("買進");
                        comboBoxFutureBuySell.Items.Add("賣出");

                        //comboBoxFutureDayTrade
                        comboBoxFutureDayTrade.Items.Add("否");
                        comboBoxFutureDayTrade.Items.Add("是");

                        //comboBoxFutureNewClose
                        comboBoxFutureNewClose.Items.Add("新倉");
                        comboBoxFutureNewClose.Items.Add("平倉");
                        comboBoxFutureNewClose.Items.Add("自動");

                        //comboBoxFutureReserved
                        comboBoxFutureReserved.Items.Add("盤中(T盤及T+1盤)");
                        comboBoxFutureReserved.Items.Add("T盤預約");
                    }
                    // TO
                    {
                        //comboBoxOptionTradeType
                        comboBoxOptionTradeType.Items.Add("ROD");
                        comboBoxOptionTradeType.Items.Add("IOC");
                        comboBoxOptionTradeType.Items.Add("FOK");

                        //comboBoxOptionBuySell
                        comboBoxOptionBuySell.Items.Add("買進");
                        comboBoxOptionBuySell.Items.Add("賣出");

                        //comboBoxOptionBuySell2
                        comboBoxOptionBuySell2.Items.Add("買進");
                        comboBoxOptionBuySell2.Items.Add("賣出");

                        //comboBoxOptionDayTrade
                        comboBoxOptionDayTrade.Items.Add("否");
                        comboBoxOptionDayTrade.Items.Add("是");

                        //comboBoxOptionNewClose
                        comboBoxOptionNewClose.Items.Add("新倉");
                        comboBoxOptionNewClose.Items.Add("平倉");
                        comboBoxOptionNewClose.Items.Add("自動");

                        //comboBoxOptionReserved
                        comboBoxOptionReserved.Items.Add("盤中(T盤及T+1盤)");
                        comboBoxOptionReserved.Items.Add("T盤預約");
                    }
                }
            }        
        }
        private void buttonSendFutureOrderCLR_Click(object sender, EventArgs e)
        {
            if (textBoxFutureQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();
                string bstrMessage;
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxFutureID.Text;

                string selectedValue = comboBoxFutureTradeType.Text;
                if (selectedValue == "ROD")  pOrder.sTradeType = 0;
                else if (selectedValue == "IOC") pOrder.sTradeType = 1;
                else if (selectedValue == "FOK") pOrder.sTradeType = 2;

                selectedValue = comboBoxFutureBuySell.Text;
                if (selectedValue == "買進") pOrder.sBuySell = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell = 1;

                selectedValue = comboBoxFutureDayTrade.Text;
                if (selectedValue == "否") pOrder.sDayTrade = 0;
                else if (selectedValue == "是") pOrder.sDayTrade = 1;

                selectedValue = comboBoxFutureNewClose.Text;
                if (selectedValue == "新倉") pOrder.sNewClose = 0;
                else if (selectedValue == "平倉") pOrder.sNewClose = 1;
                else if (selectedValue == "自動") pOrder.sNewClose = 2;

                pOrder.bstrPrice = textBoxFuturePrice.Text;
                pOrder.nQty = int.Parse(textBoxFutureQty.Text);

                selectedValue = comboBoxFutureReserved.Text;
                if (selectedValue == "盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else if (selectedValue == "T盤預約") pOrder.sReserved = 1;

                // 送出期貨委託
                int nCode = m_pSKOrder.SendFutureOrderCLR(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureOrderCLR】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
            }
        }
        private void buttonSendOptionOrder_Click(object sender, EventArgs e)
        {
            if (textBoxOptionQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREORDER pOrder = new FUTUREORDER();
                string bstrMessage;
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxOptionID.Text;
                string selectedValue = comboBoxOptionTradeType.Text;
                if (selectedValue == "ROD") pOrder.sTradeType = 0;
                else if (selectedValue == "IOC") pOrder.sTradeType = 1;
                else if (selectedValue == "FOK") pOrder.sTradeType = 2;

                selectedValue = comboBoxOptionBuySell.Text;
                if (selectedValue == "買進") pOrder.sBuySell = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell = 1;

                selectedValue = comboBoxOptionDayTrade.Text;
                if (selectedValue == "否") pOrder.sDayTrade = 0;
                else if (selectedValue == "是") pOrder.sDayTrade = 1;

                selectedValue = comboBoxOptionNewClose.Text;
                if (selectedValue == "新倉") pOrder.sNewClose = 0;
                else if (selectedValue == "平倉") pOrder.sNewClose = 1;
                else if (selectedValue == "自動") pOrder.sNewClose = 2;

                pOrder.bstrPrice = textBoxOptionPrice.Text;

                pOrder.nQty = int.Parse(textBoxOptionQty.Text);

                selectedValue = comboBoxOptionReserved.Text;
                if (selectedValue == "盤中(T盤及T+1盤)") pOrder.sReserved = 0;
                else if (selectedValue == "T盤預約") pOrder.sReserved = 1;

                if (isDuplexOrder != true)
                {
                    // 送出選擇權委託
                    int nCode = m_pSKOrder.SendOptionOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                    // 取得回傳訊息
                    string msg = "【SendOptionOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
                else
                {
                    pOrder.bstrStockNo2 = textBoxOptionID2.Text; //委託選擇權代號2{複式單}
                    // 0:買進 1:賣出{ 複式單}
                    if (comboBoxOptionBuySell2.Text == "買進") pOrder.sBuySell2 = 0;
                    else if (comboBoxOptionBuySell2.Text == "賣出") pOrder.sBuySell2 = 1;

                    // 送出國內選擇權複式單委託
                    int nCode = m_pSKOrder.SendDuplexOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                    // 取得回傳訊息
                    string msg = "【SendDuplexOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                }
                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    string msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMessage.AppendText(msg + "\n");
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
        }
        private void checkBoxSendDuplexOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSendDuplexOrder.Checked)
            {
                labelOptionID2.Visible = true;
                textBoxOptionID2.Visible = true;
                labelOptionBuySell2.Visible = true;
                comboBoxOptionBuySell2.Visible = true;

                isDuplexOrder = true;
            }
            else
            {
                labelOptionID2.Visible = false;
                textBoxOptionID2.Visible = false;
                labelOptionBuySell2.Visible = false;
                comboBoxOptionBuySell2.Visible = false;

                isDuplexOrder = false;
            }
        }
    }
}
