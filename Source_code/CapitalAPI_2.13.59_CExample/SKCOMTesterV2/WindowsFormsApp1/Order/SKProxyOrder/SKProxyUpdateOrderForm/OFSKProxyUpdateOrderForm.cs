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
    public partial class OFSKProxyUpdateOrderForm : Form
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
        public OFSKProxyUpdateOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //comboBox
                {
                    //comboBoxOONewClose
                    {
                        comboBoxOONewClose.Items.Add("新倉");
                        comboBoxOONewClose.Items.Add("平倉");
                    }
                    //sSpecialTradeType
                    {
                        comboBoxOOSpecialTradeType.Items.Add("LMT限價單");
                        comboBoxOOSpecialTradeType.Items.Add("MKT市價單");
                        comboBoxOOSpecialTradeType.Items.Add("STL停損限價");
                        comboBoxOOSpecialTradeType.Items.Add("STP停損市價");
                    }
                    //comboBoxnSpreadFlag
                    {
                        comboBoxnSpreadFlag.Items.Add("0 :OF海期");
                        comboBoxnSpreadFlag.Items.Add("1: OF-spread 海期價差");
                        comboBoxnSpreadFlag.Items.Add("2: OO 海選");
                    }
                    //comboBoxnAlterType
                    {
                        comboBoxnAlterType.Items.Add("0: Cancel 刪單");
                        comboBoxnAlterType.Items.Add("1: Decrease 減量");
                        comboBoxnAlterType.Items.Add("2: Correct 改價");
                    }
                    //comboBoxsCallPut
                    {
                        comboBoxOOCallPut.Items.Add("CALL");
                        comboBoxOOCallPut.Items.Add("PUT");
                    }
                }
            }
        }     
        private void buttonOverSeaDecreaseOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (textBoxOverseaFutureDecreaseQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                OVERSEAFUTUREORDER pSKProxyOrder = new OVERSEAFUTUREORDER();
                pSKProxyOrder.bstrFullAccount = comboBoxAccount.Text;
                pSKProxyOrder.bstrExchangeNo = textBoxOFExchangeNo.Text;
                pSKProxyOrder.bstrStockNo = textBoxOFStockNo.Text;
                pSKProxyOrder.bstrYearMonth = textBoxOFYearMonth.Text;
                pSKProxyOrder.bstrYearMonth2 = textBoxOFYearMonth2.Text;
                pSKProxyOrder.bstrOrder = textBoxOFOrder.Text;
                pSKProxyOrder.bstrOrderNumerator = textBoxOFOrderNumerator.Text;
                pSKProxyOrder.bstrOrderDenominator = textBoxOFOrderDenominator.Text;
                pSKProxyOrder.bstrStrikePrice = textBoxOOStrikePrice.Text; //履約價。{選擇權改價使用}

                string selectedValue = comboBoxOOCallPut.Text;//0:CALL  1:PUT {選擇權使用} 
                if (selectedValue == "CALL") pSKProxyOrder.sCallPut = 0;
                else if (selectedValue == "PUT") pSKProxyOrder.sCallPut = 1;

                if (comboBoxOONewClose.Text == "新倉") pSKProxyOrder.sNewClose = 0;
                else if (comboBoxOONewClose.Text == "平倉") pSKProxyOrder.sNewClose = 1;

                pSKProxyOrder.sTradeType = 0;

                //0:LMT限價單 1:MKT市價單 2:STL停損限價 3.STP停損市價
                if (comboBoxOOSpecialTradeType.Text == "LMT限價單") pSKProxyOrder.sSpecialTradeType = 0;
                else if (comboBoxOOSpecialTradeType.Text == "MKT市價單") pSKProxyOrder.sSpecialTradeType = 1;
                else if (comboBoxOOSpecialTradeType.Text == "STL停損限價") pSKProxyOrder.sSpecialTradeType = 2;
                else if (comboBoxOOSpecialTradeType.Text == "STP停損市價") pSKProxyOrder.sSpecialTradeType = 3;

                pSKProxyOrder.nQty = int.Parse(textBoxOverseaFutureDecreaseQty.Text);

                pSKProxyOrder.bstrBookNo = textBoxOverSeaCancelOrderByBookNo.Text;
                pSKProxyOrder.bstrSeqNo = textBoxOverSeaCancelOrderBySeqNo.Text;

                if (comboBoxnSpreadFlag.Text == "0 :OF海期") pSKProxyOrder.nSpreadFlag = 0;
                else if (comboBoxnSpreadFlag.Text == "1: OF-spread 海期價差") pSKProxyOrder.nSpreadFlag = 1;
                else if (comboBoxnSpreadFlag.Text == "2: OO 海選") pSKProxyOrder.nSpreadFlag = 2;

                if (comboBoxnAlterType.Text == "0: Cancel 刪單") pSKProxyOrder.nAlterType = 0;
                else if (comboBoxnAlterType.Text == "1: Decrease 減量") pSKProxyOrder.nAlterType = 1;
                else if (comboBoxnAlterType.Text == "2: Correct 改價") pSKProxyOrder.nAlterType = 2;

                string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照 OnAsyncOrder。

                // 經由proxy server送出海期選刪改單
                int nCode = m_pSKOrder.SendOverseaFutureProxyAlter(comboBoxUserID.Text, ref pSKProxyOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendOverseaFutureProxyAlter】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void checkBoxSpread_CheckedChanged(object sender, EventArgs e)
        {
       
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
        }
    }
}
