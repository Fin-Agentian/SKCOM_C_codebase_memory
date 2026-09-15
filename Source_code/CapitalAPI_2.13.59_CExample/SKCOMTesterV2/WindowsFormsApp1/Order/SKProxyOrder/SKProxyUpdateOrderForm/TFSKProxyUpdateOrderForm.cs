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
    public partial class TFSKProxyUpdateOrderForm : Form
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
        public TFSKProxyUpdateOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //comboBox
                {
                    // comboBoxTradeType
                    {
                        comboBoxTradeType.Items.Add("0:ROD");
                        comboBoxTradeType.Items.Add("1:IOC");
                        comboBoxTradeType.Items.Add("2:FOK");
                    }
                    // comboBoxUpdateTFOrder
                    {
                        comboBoxUpdateTFOrder.Items.Add("刪單");
                        comboBoxUpdateTFOrder.Items.Add("減量");
                        comboBoxUpdateTFOrder.Items.Add("改價");
                    }
                    //comboBoxFutureReserved
                    {
                        comboBoxFutureReserved.Items.Add("盤中單");
                        comboBoxFutureReserved.Items.Add("預約單");
                    }
                }
            }

        } 
        private void buttonSendFutureProxyAlter_Click(object sender, EventArgs e)
        {
            if (textBoxStockDecreaseQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                FUTUREPROXYORDER pFUTUREPROXYORDER = new FUTUREPROXYORDER();
                string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因
                pFUTUREPROXYORDER.bstrFullAccount = comboBoxAccount.Text;

                if (comboBoxUpdateTFOrder.Text == "刪單") pFUTUREPROXYORDER.bstrOrderType = "0";
                else if (comboBoxUpdateTFOrder.Text == "減量") pFUTUREPROXYORDER.bstrOrderType = "1";
                else if (comboBoxUpdateTFOrder.Text == "改價") pFUTUREPROXYORDER.bstrOrderType = "2";

                pFUTUREPROXYORDER.bstrPrice = textBoxPrice.Text;

                if (comboBoxFutureReserved.Text == "盤中單") pFUTUREPROXYORDER.nReserved = 0;
                else if (comboBoxFutureReserved.Text == "預約單") pFUTUREPROXYORDER.nReserved = 1;

                pFUTUREPROXYORDER.nQty = int.Parse(textBoxStockDecreaseQty.Text);

                if (comboBoxTradeType.Text == "ROD") pFUTUREPROXYORDER.nTradeType = 0;
                else if (comboBoxTradeType.Text == "IOC") pFUTUREPROXYORDER.nTradeType = 1;
                else if (comboBoxTradeType.Text == "FOK") pFUTUREPROXYORDER.nTradeType = 2;

                pFUTUREPROXYORDER.bstrBookNo = textBoxBookNo.Text;
                pFUTUREPROXYORDER.bstrSeqNo = textBoxSeqNo.Text;

                // 經由proxy server送出期貨刪改單 
                int nCode = m_pSKOrder.SendFutureProxyAlter(comboBoxUserID.Text, ref pFUTUREPROXYORDER, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendFutureProxyAlter】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
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

        private void buttonSendOptionProxyAlter_Click(object sender, EventArgs e)
        {
            FUTUREPROXYORDER pFUTUREPROXYORDER = new FUTUREPROXYORDER();
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因
            pFUTUREPROXYORDER.bstrFullAccount = comboBoxAccount.Text;

            if (comboBoxUpdateTFOrder.Text == "刪單") pFUTUREPROXYORDER.bstrOrderType = "0";
            else if (comboBoxUpdateTFOrder.Text == "減量") pFUTUREPROXYORDER.bstrOrderType = "1";
            else if (comboBoxUpdateTFOrder.Text == "改價") pFUTUREPROXYORDER.bstrOrderType = "2";

            pFUTUREPROXYORDER.bstrPrice = textBoxPrice.Text;

            if (comboBoxFutureReserved.Text == "盤中單") pFUTUREPROXYORDER.nReserved = 0;
            else if (comboBoxFutureReserved.Text == "預約單") pFUTUREPROXYORDER.nReserved = 1;

            pFUTUREPROXYORDER.nQty = int.Parse(textBoxStockDecreaseQty.Text);

            if (comboBoxTradeType.Text == "ROD") pFUTUREPROXYORDER.nTradeType = 0;
            else if (comboBoxTradeType.Text == "IOC") pFUTUREPROXYORDER.nTradeType = 1;
            else if (comboBoxTradeType.Text == "FOK") pFUTUREPROXYORDER.nTradeType = 2;

            pFUTUREPROXYORDER.bstrBookNo = textBoxBookNo.Text;
            pFUTUREPROXYORDER.bstrSeqNo = textBoxSeqNo.Text;

            // 經由proxy server送出選擇權刪改單
            int nCode = m_pSKOrder.SendOptionProxyAlter(comboBoxUserID.Text, ref pFUTUREPROXYORDER, out bstrMessage);
            // 取得回傳訊息
            string msg = "【SendOptionProxyAlter】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
