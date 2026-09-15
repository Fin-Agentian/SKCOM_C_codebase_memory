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
    public partial class TSSKProxyUpdateOrderForm : Form
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
        public TSSKProxyUpdateOrderForm()
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
                    // comboBoxbstrOrderType
                    {
                        comboBoxbstrOrderType.Items.Add("刪單");
                        comboBoxbstrOrderType.Items.Add("改量");
                        comboBoxbstrOrderType.Items.Add("改價");                       
                    }
                    // nSpecialTradeType
                    {
                        comboBoxnSpecialTradeType.Items.Add("市價");
                        comboBoxnSpecialTradeType.Items.Add("限價");
                    }
                    // sPeriod
                    {
                        comboBoxPeriod.Items.Add("盤中");
                        comboBoxPeriod.Items.Add("零股");
                        comboBoxPeriod.Items.Add("盤後交易");
                        comboBoxPeriod.Items.Add("盤中零股");
                    }
                    // comboBoxnPriceMark
                    {
                        comboBoxnPriceMark.Items.Add("一般定價");
                        comboBoxnPriceMark.Items.Add("前日收盤價");
                        comboBoxnPriceMark.Items.Add("漲停");
                        comboBoxnPriceMark.Items.Add("跌停");
                    }
                }
            }

        } 
        private void buttonSendStockProxyAlter_Click(object sender, EventArgs e)
        {
            if (textBoxStockDecreaseQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKPROXYORDER pSTOCKPROXYORDER = new STOCKPROXYORDER();
                pSTOCKPROXYORDER.bstrStockNo = textBoxCancelOrderByStockNo.Text;
                pSTOCKPROXYORDER.bstrBookNo = textBoxBookNo.Text;
                pSTOCKPROXYORDER.bstrSeqNo = textBoxSeqNo.Text;
                pSTOCKPROXYORDER.bstrFullAccount = comboBoxAccount.Text;

                if (comboBoxbstrOrderType.Text == "刪單") pSTOCKPROXYORDER.bstrOrderType = "0";
                else if (comboBoxbstrOrderType.Text == "改量") pSTOCKPROXYORDER.bstrOrderType = "1";
                else if (comboBoxbstrOrderType.Text == "改價") pSTOCKPROXYORDER.bstrOrderType = "2";

                if (comboBoxnSpecialTradeType.Text == "市價") pSTOCKPROXYORDER.nSpecialTradeType = 1;
                else if (comboBoxnSpecialTradeType.Text == "限價") pSTOCKPROXYORDER.nSpecialTradeType = 2;

                if (comboBoxPeriod.Text == "盤中") pSTOCKPROXYORDER.nPeriod = 0;
                else if (comboBoxPeriod.Text == "零股") pSTOCKPROXYORDER.nPeriod = 1;
                else if (comboBoxPeriod.Text == "盤後交易") pSTOCKPROXYORDER.nPeriod = 2;
                else pSTOCKPROXYORDER.nPeriod = 3;

                pSTOCKPROXYORDER.bstrPrice = textBoxPrice.Text; // 委託價格

                pSTOCKPROXYORDER.nQty = int.Parse(textBoxStockDecreaseQty.Text); // 股數

                if (comboBoxTradeType.Text == "ROD") pSTOCKPROXYORDER.nTradeType = 0;
                else if (comboBoxTradeType.Text == "IOC") pSTOCKPROXYORDER.nTradeType = 1;
                else if (comboBoxTradeType.Text == "FOK") pSTOCKPROXYORDER.nTradeType = 2;

                if (comboBoxnPriceMark.Text == "一般定價") pSTOCKPROXYORDER.nPriceMark = 0;
                else if (comboBoxnPriceMark.Text == "前日收盤價") pSTOCKPROXYORDER.nPriceMark = 1;
                else if (comboBoxnPriceMark.Text == "漲停") pSTOCKPROXYORDER.nPriceMark = 2;
                else if (comboBoxnPriceMark.Text == "跌停") pSTOCKPROXYORDER.nPriceMark = 3;

                string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。

                // 經由proxy server送出證券刪改單      
                int nCode = m_pSKOrder.SendStockProxyAlter(comboBoxUserID.Text, ref pSTOCKPROXYORDER, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockProxyAlter】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
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
            m_pSKOrder.OnAsyncOrder += new _ISKOrderLibEvents_OnAsyncOrderEventHandler(OnAsyncOrder);
            void OnAsyncOrder(int nStampID, int nCode, string bstrMessage)
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
