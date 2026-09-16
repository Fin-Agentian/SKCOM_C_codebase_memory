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
    public partial class TFSKProxySendOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
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
        public TFSKProxySendOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    // TF
                    {
                        //sTradeType
                        comboBoxFutureTradeType.Items.Add("ROD");
                        comboBoxFutureTradeType.Items.Add("IOC");
                        comboBoxFutureTradeType.Items.Add("FOK");

                        //sBuySell
                        comboBoxFutureBuySell.Items.Add("買進");
                        comboBoxFutureBuySell.Items.Add("賣出");

                        //sDayTrade
                        comboBoxFutureDayTrade.Items.Add("否");
                        comboBoxFutureDayTrade.Items.Add("是");

                        //sNewClose
                        comboBoxFutureNewClose.Items.Add("0:新倉");
                        comboBoxFutureNewClose.Items.Add("1:平倉");
                        comboBoxFutureNewClose.Items.Add("2:自動");

                        //comboBoxFutureReserved
                        comboBoxFutureReserved.Items.Add("盤中單");
                        comboBoxFutureReserved.Items.Add("預約單");

                        //comboBoxTFnPriceFlag
                        {
                            comboBoxTFnPriceFlag.Items.Add("市價");
                            comboBoxTFnPriceFlag.Items.Add("限價");
                            comboBoxTFnPriceFlag.Items.Add("範圍市價");
                        }
                    }
                    // TO
                    {
                        //sTradeType
                        comboBoxOptionTradeType.Items.Add("ROD");
                        comboBoxOptionTradeType.Items.Add("IOC");
                        comboBoxOptionTradeType.Items.Add("FOK");

                        //sBuySell
                        comboBoxOptionBuySell.Items.Add("買進");
                        comboBoxOptionBuySell.Items.Add("賣出");

                        //sBuySell2
                        comboBoxOptionBuySell2.Items.Add("買進");
                        comboBoxOptionBuySell2.Items.Add("賣出");

                        //sDayTrade
                        comboBoxOptionDayTrade.Items.Add("否");
                        comboBoxOptionDayTrade.Items.Add("是");

                        //comboBoxOptionbstrOrderType
                        comboBoxOptionbstrOrderType.Items.Add("0:新倉");
                        comboBoxOptionbstrOrderType.Items.Add("1:平倉");
                        comboBoxOptionbstrOrderType.Items.Add("2:自動");

                        //sReserved
                        comboBoxOptionReserved.Items.Add("盤中單");
                        comboBoxOptionReserved.Items.Add("預約單");

                        //comboBoxnCP
                        comboBoxnCP.Items.Add("CALL");
                        comboBoxnCP.Items.Add("PUT");

                        //comboBoxnCP2
                        comboBoxnCP2.Items.Add("CALL");
                        comboBoxnCP2.Items.Add("PUT");

                        // comboBoxTOnPriceFlag
                        {
                            comboBoxTOnPriceFlag.Items.Add("市價");
                            comboBoxTOnPriceFlag.Items.Add("限價");
                            comboBoxTOnPriceFlag.Items.Add("範圍市價");
                        }
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
                FUTUREPROXYORDER pFUTUREPROXYORDER = new FUTUREPROXYORDER();
                string bstrMessage;

                pFUTUREPROXYORDER.bstrFullAccount = comboBoxAccount.Text;
                pFUTUREPROXYORDER.bstrStockNo = textBoxFutureID.Text;
                pFUTUREPROXYORDER.bstrSettleYM = textBoxTFbstrSettleYM.Text;
                string selectedValue = comboBoxTFnPriceFlag.Text;
                if (selectedValue == "市價") pFUTUREPROXYORDER.nPriceFlag = 0;
                else if (selectedValue == "限價") pFUTUREPROXYORDER.nPriceFlag = 1;
                else if (selectedValue == "範圍市價") pFUTUREPROXYORDER.nPriceFlag = 2;

                selectedValue = comboBoxFutureTradeType.Text;
                if (selectedValue == "ROD") pFUTUREPROXYORDER.nTradeType = 0;
                else if (selectedValue == "IOC") pFUTUREPROXYORDER.nTradeType = 1;
                else if (selectedValue == "FOK") pFUTUREPROXYORDER.nTradeType = 2;

                selectedValue = comboBoxFutureBuySell.Text;
                if (selectedValue == "買進") pFUTUREPROXYORDER.nBuySell = 0;
                else if (selectedValue == "賣出") pFUTUREPROXYORDER.nBuySell = 1;

                selectedValue = comboBoxFutureDayTrade.Text;
                if (selectedValue == "否") pFUTUREPROXYORDER.nDayTrade = 0;
                else if (selectedValue == "是") pFUTUREPROXYORDER.nDayTrade = 1;

                if (comboBoxFutureNewClose.Text == "0:新倉") pFUTUREPROXYORDER.bstrOrderType = "0";
                else if (comboBoxFutureNewClose.Text == "1:平倉") pFUTUREPROXYORDER.bstrOrderType = "1";
                else if (comboBoxFutureNewClose.Text == "2:自動") pFUTUREPROXYORDER.bstrOrderType = "2";

                pFUTUREPROXYORDER.bstrPrice = textBoxFuturePrice.Text;
                pFUTUREPROXYORDER.nQty = int.Parse(textBoxFutureQty.Text);

                selectedValue = comboBoxFutureReserved.Text;
                if (selectedValue == "盤中單") pFUTUREPROXYORDER.nReserved = 0;
                else if (selectedValue == "預約單") pFUTUREPROXYORDER.nReserved = 1;

                // 經由proxy server送出期貨委託，需設倉別與盤別
                int nCode = m_pSKOrder.SendFutureProxyOrderCLR(comboBoxUserID.Text, ref pFUTUREPROXYORDER, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureProxyOrderCLR】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
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
                FUTUREPROXYORDER pFUTUREPROXYORDER = new FUTUREPROXYORDER();
                string bstrMessage;

                pFUTUREPROXYORDER.bstrFullAccount = comboBoxAccount.Text;
                pFUTUREPROXYORDER.bstrStockNo = textBoxOptionID.Text;
                pFUTUREPROXYORDER.bstrSettleYM = textBoxTObstrSettleYM.Text;
                pFUTUREPROXYORDER.bstrStrike = textBoxbstrStrike.Text;
                string selectedValue = comboBoxOptionTradeType.Text;
                if (selectedValue == "ROD") pFUTUREPROXYORDER.nTradeType = 0;
                else if (selectedValue == "IOC") pFUTUREPROXYORDER.nTradeType = 1;
                else if (selectedValue == "FOK") pFUTUREPROXYORDER.nTradeType = 2;

                selectedValue = comboBoxOptionBuySell.Text;
                if (selectedValue == "買進") pFUTUREPROXYORDER.nBuySell = 0;
                else if (selectedValue == "賣出") pFUTUREPROXYORDER.nBuySell = 1;

                selectedValue = comboBoxOptionDayTrade.Text;
                if (selectedValue == "否") pFUTUREPROXYORDER.nDayTrade = 0;
                else if (selectedValue == "是") pFUTUREPROXYORDER.nDayTrade = 1;

                if (comboBoxOptionbstrOrderType.Text == "0:新倉") pFUTUREPROXYORDER.bstrOrderType = "0";
                else if (comboBoxOptionbstrOrderType.Text == "1:平倉") pFUTUREPROXYORDER.bstrOrderType = "1";
                else if (comboBoxOptionbstrOrderType.Text == "2:自動") pFUTUREPROXYORDER.bstrOrderType = "2";

                pFUTUREPROXYORDER.bstrPrice = textBoxOptionPrice.Text;
                pFUTUREPROXYORDER.nQty = int.Parse(textBoxOptionQty.Text);

                selectedValue = comboBoxOptionReserved.Text;
                if (selectedValue == "盤中單") pFUTUREPROXYORDER.nReserved = 0;
                else if (selectedValue == "預約單") pFUTUREPROXYORDER.nReserved = 1;

                selectedValue = comboBoxnCP.Text;
                if (selectedValue == "CALL") pFUTUREPROXYORDER.nCP = 0;
                else if (selectedValue == "PUT") pFUTUREPROXYORDER.nCP = 1;

                selectedValue = comboBoxTOnPriceFlag.Text;
                if (selectedValue == "市價") pFUTUREPROXYORDER.nPriceFlag = 0;
                else if (selectedValue == "限價") pFUTUREPROXYORDER.nPriceFlag = 1;
                else if (selectedValue == "範圍市價") pFUTUREPROXYORDER.nPriceFlag = 2;

                if (isDuplexOrder != true)
                {
                    // 送出選擇權委託
                    int nCode = m_pSKOrder.SendOptionProxyOrder(comboBoxUserID.Text, ref pFUTUREPROXYORDER, out bstrMessage);
                    // 取得回傳訊息
                    string msg1 = "【SendOptionProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg1 + "\n");
                }
                else
                {
                    pFUTUREPROXYORDER.bstrStockNo2 = textBoxOptionID2.Text; // 委託選擇權代號2{複式單}
                    pFUTUREPROXYORDER.bstrSettleYM2 = textBoxTObstrSettleYM2.Text; // 指定月份商品契約年月2，共6碼EX:202301
                    pFUTUREPROXYORDER.bstrStrike2 = textBoxbstrStrike2.Text; // 履約價2

                    selectedValue = comboBoxnCP2.Text;
                    if (selectedValue == "CALL") pFUTUREPROXYORDER.nCP2 = 0;
                    else if (selectedValue == "PUT") pFUTUREPROXYORDER.nCP2 = 1;
                    // 0:買進 1:賣出{複式單} 
                    if (comboBoxOptionBuySell2.Text == "買進") pFUTUREPROXYORDER.nBuySell2 = 0;
                    else if (comboBoxOptionBuySell2.Text == "賣出") pFUTUREPROXYORDER.nBuySell2 = 1;
                    // 經由proxy server送出選擇權複式下單
                    int nCode = m_pSKOrder.SendDuplexProxyOrder(comboBoxUserID.Text, ref pFUTUREPROXYORDER, out bstrMessage);
                    // 取得回傳訊息
                    string msg2 = "【SendDuplexProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg2 + "\n");
                }
                string msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
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
        }
        private void checkBoxSendDuplexOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSendDuplexOrder.Checked)
            {
                labelOptionID2.Visible = true;
                textBoxOptionID2.Visible = true;
                labelOptionBuySell2.Visible = true;
                comboBoxOptionBuySell2.Visible = true;

                labelTObstrSettleYM2.Visible = true;
                textBoxTObstrSettleYM2.Visible = true;
                labelbstrStrike2.Visible = true;
                textBoxbstrStrike2.Visible = true;
                labelnCP2.Visible = true;
                comboBoxnCP2.Visible = true;


                isDuplexOrder = true;
            }
            else
            {
                labelOptionID2.Visible = false;
                textBoxOptionID2.Visible = false;
                labelOptionBuySell2.Visible = false;
                comboBoxOptionBuySell2.Visible = false;

                labelTObstrSettleYM2.Visible = false;
                textBoxTObstrSettleYM2.Visible = false;
                labelbstrStrike2.Visible = false;
                textBoxbstrStrike2.Visible = false;
                labelnCP2.Visible = false;
                comboBoxnCP2.Visible = false;

                isDuplexOrder = false;
            }
        }
    }
}
