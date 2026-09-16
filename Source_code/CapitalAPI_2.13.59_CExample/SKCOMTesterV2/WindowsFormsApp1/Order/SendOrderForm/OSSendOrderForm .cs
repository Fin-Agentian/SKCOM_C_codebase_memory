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
    public partial class OSSendOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
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
        public OSSendOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    // OS
                    {
                        //comboBoxForeignAccountType
                        {
                            comboBoxForeignAccountType.Items.Add("外幣專戶");
                            comboBoxForeignAccountType.Items.Add("台幣專戶");
                        }
                        //comboBoxForeignOrderType
                        {
                            comboBoxForeignOrderType.Items.Add("買");
                            comboBoxForeignOrderType.Items.Add("賣");
                        }
                        //comboBoxForeignTradeType
                        {
                            comboBoxForeignTradeType.Items.Add("一般/定股(CITI)");
                            comboBoxForeignTradeType.Items.Add("定額(VIEWTRADE)");
                        }
                    }
                }
            } 
        }
        private void buttonSendForeignStockOrder_Click(object sender, EventArgs e)
        {
            if (textBoxForeignQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FOREIGNORDER pOrder = new FOREIGNORDER();
                {
                    pOrder.bstrFullAccount = comboBoxAccount.Text; //複委託帳號，分公司＋帳號7碼
                    pOrder.bstrStockNo = textBoxForeignStockID.Text; //委託股票代號
                    pOrder.bstrExchangeNo = textBoxForeignExchangeNo.Text; //交易所代碼，US：美股， HK：港股，JP：日股， SP：新加坡，SG：新(幣)加坡股，HA: 滬股，SA: 深股
                    pOrder.bstrPrice = textBoxForeignPrice.Text; //委託價格
                    pOrder.bstrCurrency1 = textBoxForeignCurrency1.Text; //扣款幣別，幣別順序1
                    pOrder.bstrCurrency2 = textBoxForeignCurrency2.Text; //扣款幣別，幣別順序2
                    pOrder.bstrCurrency3 = textBoxForeignCurrency3.Text; //扣款幣別，幣別順序3
                                                                         //(幣別可輸入 : HKD、NTD、USD、JPY、SGD、EUR、AUD、CNY、GBP)
                                                                         //專戶別種類，1:外幣專戶 2:台幣專戶
                    if (comboBoxForeignAccountType.Text == "外幣專戶") pOrder.nAccountType = 1;
                    else if (comboBoxForeignAccountType.Text == "台幣專戶") pOrder.nAccountType = 2;
                    pOrder.nQty = int.Parse(textBoxForeignQty.Text); //委託量
                                                                     //1:買 2:賣
                    if (comboBoxForeignOrderType.Text == "買") pOrder.nOrderType = 1;
                    else if (comboBoxForeignOrderType.Text == "賣") pOrder.nOrderType = 2;
                    //庫存別 ，賣出美股時必填
                    //1:[美股]一般/定股(CITI) 2:定額(VIEWTRADE)
                    if (comboBoxForeignTradeType.Text == "一般/定股(CITI)") pOrder.nTradeType = 1;
                    else if (comboBoxForeignTradeType.Text == "定額(VIEWTRADE)") pOrder.nTradeType = 2;
                }

                string bstrMessage;
                // 送出複委託委託
                int nCode = m_pSKOrder.SendForeignStockOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendForeignStockOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
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
                if (values[0] == "OS")
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
    }
}
