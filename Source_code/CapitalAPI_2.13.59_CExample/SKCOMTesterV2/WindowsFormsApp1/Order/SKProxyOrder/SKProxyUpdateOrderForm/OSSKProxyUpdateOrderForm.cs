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
    public partial class OSSKProxyUpdateOrderForm : Form
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
        public OSSKProxyUpdateOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //comboBox
                {
                    // comboBoxUpdateTFOrder
                    {
                        comboBoxUpdateTFOrder.Items.Add("刪單");
                        comboBoxUpdateTFOrder.Items.Add("減量");
                        comboBoxUpdateTFOrder.Items.Add("改價");
                        comboBoxUpdateTFOrder.SelectedIndex = 0;
                    }
                }
            }
        }         
        private void buttonSendForeignStockProxyCancel_Click(object sender, EventArgs e)
        {
            OSSTOCKPROXYORDER pOrder = new OSSTOCKPROXYORDER();
            {
                pOrder.bstrFullAccount = comboBoxAccount.Text; // 複委託帳號
                pOrder.bstrSeqNo = textBoxCancelForeignStockOrderbstrSeqNo.Text; // 序號
                pOrder.bstrBookNo = textBoxCancelForeignStockOrderbstrBookNo.Text; // 書號
                pOrder.nOrderType = 4; // 4:刪單
                pOrder.bstrExchangeNo = textBoxbstrExchangeNo.Text;// 交易所代碼，美股：US
                pOrder.bstrStockNo = textBoxbstrStockNo.Text; // 委託股票代號
            }

            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因

            // 經由proxy server送出複委託刪單
            int nCode = m_pSKOrder.SendForeignStockProxyCancel(comboBoxUserID.Text, ref pOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【SendForeignStockProxyCancel】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void comboBoxUserID_DropDown(object sender, EventArgs e)
        {
            m_dictUserID.Clear(); //清空之前的帳號
            // 取回可交易的所有帳號
            {
                int nCode = m_pSKOrder.GetUserAccount();
                // 取得回傳訊息
                string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n"); 
            }
        }
        private void OrderControlForm_Load(object sender, EventArgs e)
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
