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
    public partial class OSSKProxySendOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
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
        public OSSKProxySendOrderForm()
        {
            // Init
            {
                InitializeComponent();
                // comboBox
                {
                    // OS
                    {
                        //nAccountType
                        comboBoxForeignAccountType.Items.Add("外幣專戶");
                        comboBoxForeignAccountType.Items.Add("台幣專戶");

                        //nOrderType
                        comboBoxForeignOrderType.Items.Add("買");
                        comboBoxForeignOrderType.Items.Add("賣");

                        //nTradeType
                        comboBoxForeignTradeType.Items.Add("一般/定股(CITI)");
                        comboBoxForeignTradeType.Items.Add("定額(VIEWTRADE)");

                        //comboBoxbstrExchangeNo
                        comboBoxbstrExchangeNo.Items.Add("US：美股");
                        comboBoxbstrExchangeNo.Items.Add("HK：港股");
                        comboBoxbstrExchangeNo.Items.Add("JP：日股");
                        comboBoxbstrExchangeNo.Items.Add("SP：新加坡");
                        comboBoxbstrExchangeNo.Items.Add("SG：新(幣)坡股");
                        comboBoxbstrExchangeNo.Items.Add("SA: 滬股");
                        comboBoxbstrExchangeNo.Items.Add("HA: 深股");
                    }
                }
            } 
        }
        private void buttonSendForeignStockProxyOrder_Click(object sender, EventArgs e)
        {
            OSSTOCKPROXYORDER pAsyncOrder = new OSSTOCKPROXYORDER();
            string bstrMessage;

            pAsyncOrder.bstrFullAccount = comboBoxAccount.Text; //複委託帳號，分公司＋帳號7碼

            pAsyncOrder.bstrStockNo = textBoxForeignStockID.Text; //委託股票代號

            //交易所代碼，美股：US， HK：港股，JP：日股， SP：新加坡，SG：新(幣)坡股，SA: 滬股，HA: 深股
            if (comboBoxbstrExchangeNo.Text == "US：美股") pAsyncOrder.bstrExchangeNo = "US";
            else if (comboBoxbstrExchangeNo.Text == "HK：港股") pAsyncOrder.bstrExchangeNo = "HK";
            else if (comboBoxbstrExchangeNo.Text == "JP：日股") pAsyncOrder.bstrExchangeNo = "JP";
            else if (comboBoxbstrExchangeNo.Text == "SP：新加坡") pAsyncOrder.bstrExchangeNo = "SP";
            else if (comboBoxbstrExchangeNo.Text == "SG：新(幣)坡股") pAsyncOrder.bstrExchangeNo = "SG";
            else if (comboBoxbstrExchangeNo.Text == "SA: 滬股") pAsyncOrder.bstrExchangeNo = "SA";
            else if (comboBoxbstrExchangeNo.Text == "HA: 深股") pAsyncOrder.bstrExchangeNo = "HA";

            string selectedValue = comboBoxForeignAccountType.Text;//專戶別種類，1:外幣專戶 2:台幣專戶
            if (selectedValue == "外幣專戶") pAsyncOrder.nAccountType = 1;
            else if (selectedValue == "台幣專戶") pAsyncOrder.nAccountType = 2;

            pAsyncOrder.bstrCurrency1 = textBoxForeignCurrency1.Text; //扣款幣別，幣別順序1
            pAsyncOrder.bstrCurrency2 = textBoxForeignCurrency2.Text; //扣款幣別，幣別順序2
            pAsyncOrder.bstrCurrency3 = textBoxForeignCurrency3.Text; //扣款幣別，幣別順序3
                                                                        //
            pAsyncOrder.bstrProxyQty = textBoxForeignQty.Text; //委託量

            pAsyncOrder.bstrPrice = textBoxForeignPrice.Text; //委託價格

            selectedValue = comboBoxForeignOrderType.Text;//1:買 2:賣  //4:刪單
            if (selectedValue == "買") pAsyncOrder.nOrderType = 1;
            else if (selectedValue == "賣") pAsyncOrder.nOrderType = 2;

            selectedValue = comboBoxForeignTradeType.Text; //1:[美股]一般 / 定股(CITI) 2:定額(VIEWTRADE)
            if (selectedValue == "一般/定股(CITI)") pAsyncOrder.nTradeType = 1;
            else if (selectedValue == "定額(VIEWTRADE)") pAsyncOrder.nTradeType = 2;

            // 經由proxy server送出複委託下單
            int nCode = m_pSKOrder.SendForeignStockProxyOrder(comboBoxUserID.Text, ref pAsyncOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【SendForeignStockProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            msg = "【同步委託結果】" + bstrMessage;
            richTextBoxMessage.AppendText(msg + "\n");
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
    }
}
