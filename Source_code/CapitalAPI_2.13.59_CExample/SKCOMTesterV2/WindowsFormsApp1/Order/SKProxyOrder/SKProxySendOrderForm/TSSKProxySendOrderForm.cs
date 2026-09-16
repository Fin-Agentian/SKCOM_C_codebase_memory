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
    public partial class TSSKProxySendOrderForm : Form
    {
        // 委託實例
        private _ISKOrderLibEvents_OnProxyOrderEventHandler proxyOrderHandler;

        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        // 存[UserID]對應-交易帳號
        Dictionary<string, List<string>> m_dictUserID = new Dictionary<string, List<string>>();
        List<string> allkeys;       
        static void AddUserID(Dictionary<string, List<string>> dictUserID, string UserID, string AccountData)
        {
            string[] values = AccountData.Split(',');

            // broker ID (IB)4碼 + 帳號7碼
            string Account = values[1] + values[3];

            if (dictUserID.ContainsKey(UserID))
            {
                // 如果已經存在，添加到現有的 List<string>
                dictUserID[UserID].Add(Account);
            }
            else
            {
                // 如果不存在，創建一個新的 List<string>，並添加到字典中
                dictUserID[UserID] = new List<string> { Account };
            }
        }
        public TSSKProxySendOrderForm()
        {
            // Init
            {
                InitializeComponent();

                // comboBox
                {
                    //STOCKORDER物件
                    {
                        // sPeriod
                        comboBoxPeriod.Items.Add("盤中");
                        comboBoxPeriod.Items.Add("零股");
                        comboBoxPeriod.Items.Add("盤後交易");
                        comboBoxPeriod.Items.Add("盤中零股");

                        // comboBoxbstrOrderType
                        comboBoxbstrOrderType.Items.Add("現股買進");
                        comboBoxbstrOrderType.Items.Add("現股賣出");
                        comboBoxbstrOrderType.Items.Add("融資買進");
                        comboBoxbstrOrderType.Items.Add("融資賣出");
                        comboBoxbstrOrderType.Items.Add("融券買進");
                        comboBoxbstrOrderType.Items.Add("融券賣出");
                        comboBoxbstrOrderType.Items.Add("無券賣出");

                        // nTradeType
                        comboBoxnTradeType.Items.Add("ROD");
                        comboBoxnTradeType.Items.Add("IOC");
                        comboBoxnTradeType.Items.Add("FOK");

                        // nSpecialTradeType
                        comboBoxnSpecialTradeType.Items.Add("市價");
                        comboBoxnSpecialTradeType.Items.Add("限價");

                        // comboBoxnPriceMark
                        comboBoxnPriceMark.Items.Add("一般定價");
                        comboBoxnPriceMark.Items.Add("前日收盤價");
                        comboBoxnPriceMark.Items.Add("漲停");
                        comboBoxnPriceMark.Items.Add("跌停");
                    }
                }
            }
        }
        private void buttonSendStockOrder_Click(object sender, EventArgs e)
        {
            if (textBoxnQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKPROXYORDER pSTOCKPROXYORDER = new STOCKPROXYORDER();
                string bstrMessage;

                pSTOCKPROXYORDER.bstrFullAccount = comboBoxAccount.Text;
                pSTOCKPROXYORDER.bstrStockNo = textBoxStockID.Text;

                if (comboBoxPeriod.Text == "盤中") pSTOCKPROXYORDER.nPeriod = 0;
                else if (comboBoxPeriod.Text == "零股") pSTOCKPROXYORDER.nPeriod = 1;
                else if (comboBoxPeriod.Text == "盤後交易") pSTOCKPROXYORDER.nPeriod = 2;
                else if (comboBoxPeriod.Text == "盤中零股") pSTOCKPROXYORDER.nPeriod = 3;

                if (comboBoxbstrOrderType.Text == "現股買進") pSTOCKPROXYORDER.bstrOrderType = "1";
                else if (comboBoxbstrOrderType.Text == "現股賣出") pSTOCKPROXYORDER.bstrOrderType = "2";
                else if (comboBoxbstrOrderType.Text == "融資買進") pSTOCKPROXYORDER.bstrOrderType = "3";
                else if (comboBoxbstrOrderType.Text == "融資賣出") pSTOCKPROXYORDER.bstrOrderType = "4";
                else if (comboBoxbstrOrderType.Text == "融券買進") pSTOCKPROXYORDER.bstrOrderType = "5";
                else if (comboBoxbstrOrderType.Text == "融券賣出") pSTOCKPROXYORDER.bstrOrderType = "6";
                else if (comboBoxbstrOrderType.Text == "無券賣出") pSTOCKPROXYORDER.bstrOrderType = "7";

                pSTOCKPROXYORDER.bstrPrice = textBoxbstrPrice.Text;
                pSTOCKPROXYORDER.nQty = int.Parse(textBoxnQty.Text);

                if (comboBoxnTradeType.Text == "ROD") pSTOCKPROXYORDER.nTradeType = 0;
                else if (comboBoxnTradeType.Text == "IOC") pSTOCKPROXYORDER.nTradeType = 1;
                else if (comboBoxnTradeType.Text == "FOK") pSTOCKPROXYORDER.nTradeType = 2;

                if (comboBoxnSpecialTradeType.Text == "市價")  pSTOCKPROXYORDER.nSpecialTradeType = 1;
                else if (comboBoxnSpecialTradeType.Text == "限價") pSTOCKPROXYORDER.nSpecialTradeType = 2;

                if (comboBoxnPriceMark.Text == "一般定價") pSTOCKPROXYORDER.nPriceMark = 0;
                else if (comboBoxnPriceMark.Text == "前日收盤價") pSTOCKPROXYORDER.nPriceMark = 1;
                else if (comboBoxnPriceMark.Text == "漲停")  pSTOCKPROXYORDER.nPriceMark = 2;
                else if (comboBoxnPriceMark.Text == "跌停") pSTOCKPROXYORDER.nPriceMark = 3;

                // 送出證券委託
                int nCode = m_pSKOrder.SendStockProxyOrder(comboBoxUserID.Text, ref pSTOCKPROXYORDER, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockProxyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");

                // 取得回傳訊息(bstrMessage)
                msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
            }
        }
        private void comboBoxnSpecialTradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBoxnSpecialTradeType.Text;
            if (selectedValue == "市價")
            {
                textBoxbstrPrice.Text = "0";
                textBoxbstrPrice.ReadOnly = true;
            }
            else if (selectedValue == "限價")
            {
                textBoxbstrPrice.ReadOnly = false;
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
                if (values[0] == "TS")
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
            proxyOrderHandler = new _ISKOrderLibEvents_OnProxyOrderEventHandler(OnProxyOrder);
            m_pSKOrder.OnProxyOrder += proxyOrderHandler;
            //m_pSKOrder.OnProxyOrder += new _ISKOrderLibEvents_OnProxyOrderEventHandler(OnProxyOrder);
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

        private void TSSKProxySendOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_pSKOrder.OnProxyOrder -= proxyOrderHandler;
        }
    }
}
