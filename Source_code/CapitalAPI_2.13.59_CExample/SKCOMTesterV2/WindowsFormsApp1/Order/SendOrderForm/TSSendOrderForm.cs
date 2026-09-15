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
    public partial class TSSendOrderForm : Form
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
        public TSSendOrderForm()
        {
            // Init
            {
                InitializeComponent();

                // comboBox
                {
                    //STOCKORDER物件
                    {
                        // comboBoxPrime
                        comboBoxPrime.Items.Add("上市上櫃");
                        comboBoxPrime.Items.Add("興櫃");

                        // comboBoxPeriod
                        comboBoxPeriod.Items.Add("盤中");
                        comboBoxPeriod.Items.Add("盤後");
                        comboBoxPeriod.Items.Add("零股");

                        // comboBoxFlag
                        comboBoxFlag.Items.Add("現股");
                        comboBoxFlag.Items.Add("融資");
                        comboBoxFlag.Items.Add("融券");
                        comboBoxFlag.Items.Add("無券");

                        // comboBoxBuySell
                        comboBoxBuySell.Items.Add("買進");
                        comboBoxBuySell.Items.Add("賣出");

                        // comboBoxnTradeType
                        comboBoxnTradeType.Items.Add("ROD");
                        comboBoxnTradeType.Items.Add("IOC");
                        comboBoxnTradeType.Items.Add("FOK");

                        // comboBoxnSpecialTradeType
                        comboBoxnSpecialTradeType.Items.Add("市價");
                        comboBoxnSpecialTradeType.Items.Add("限價");
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
                STOCKORDER pOrder = new STOCKORDER();
                string bstrMessage;

                pOrder.bstrFullAccount = comboBoxAccount.Text;

                pOrder.bstrStockNo = textBoxStockID.Text;

                string selectedValue = comboBoxPrime.Text;
                if (selectedValue == "上市上櫃") pOrder.sPrime = 0;
                else if (selectedValue == "興櫃") pOrder.sPrime = 1;

                selectedValue = comboBoxPeriod.Text;
                if (selectedValue == "盤中") pOrder.sPeriod = 0;
                else if (selectedValue == "盤後") pOrder.sPeriod = 1;
                else if (selectedValue == "零股") pOrder.sPeriod = 2;

                selectedValue = comboBoxFlag.Text;
                if (selectedValue == "現股") pOrder.sFlag = 0;
                else if (selectedValue == "融資") pOrder.sFlag = 1;
                else if (selectedValue == "融券") pOrder.sFlag = 2;
                else if (selectedValue == "無券") pOrder.sFlag = 3;

                selectedValue = comboBoxBuySell.Text;
                if (selectedValue == "買進") pOrder.sBuySell = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell = 1;

                pOrder.bstrPrice = textBoxbstrPrice.Text;
                pOrder.nQty = int.Parse(textBoxnQty.Text);

                selectedValue = comboBoxnTradeType.Text;
                if (selectedValue == "ROD") pOrder.nTradeType = 0;
                else if (selectedValue == "IOC") pOrder.nTradeType = 1;
                else if (selectedValue == "FOK") pOrder.nTradeType = 2;

                selectedValue = comboBoxnSpecialTradeType.Text;
                if (selectedValue == "市價") pOrder.nSpecialTradeType = 1;
                else if (selectedValue == "限價") pOrder.nSpecialTradeType = 2;

                string msg;
                // 送出證券委託
                int nCode = m_pSKOrder.SendStockOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                msg = "【SendStockOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);

                // 取得回傳訊息(bstrMessage)
                if (bAsyncOrder == false)
                {
                    msg = "【同步委託結果】" + bstrMessage;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
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

        private void buttonSendStockOddLotOrder_Click(object sender, EventArgs e)
        {
            STOCKORDER pOrder = new STOCKORDER();
            string bstrMessage;

            pOrder.bstrFullAccount = comboBoxAccount.Text;//證券帳號，分公司代碼＋帳號7碼
            pOrder.bstrStockNo = textBoxStockID.Text;//委託股票代號
            pOrder.sPeriod = 4;// 4:盤中零股
            pOrder.sFlag = 0; //0:現股 

            string selectedValue = comboBoxBuySell.Text;
            if (selectedValue == "買進") pOrder.sBuySell = 0;
            else if (selectedValue == "賣出") pOrder.sBuySell = 1;//0:買進 1:賣出

            pOrder.bstrPrice = textBoxbstrPrice.Text;//委託價格 
            pOrder.nQty = int.Parse(textBoxnQty.Text);//零股數量為股數1~999股

            string msg;
            // 送出證券盤中零股委託
            int nCode = m_pSKOrder.SendStockOddLotOrder(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
            // 取得回傳訊息
            msg = "【SendStockOddLotOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            // 取得回傳訊息(bstrMessage)
            if (bAsyncOrder == false)
            {
                msg = "【同步委託結果】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
            }
        }
    }
}
