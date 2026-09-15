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
    public partial class TSTFUpdateOrderForm : Form
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
        public TSTFUpdateOrderForm()
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
                    // comboBoxCorrectPriceByBookNoMarketSymbol
                    {
                        comboBoxMarketSymbol.Items.Add("TS:證券");
                        comboBoxMarketSymbol.Items.Add("TF:期貨");
                        comboBoxMarketSymbol.Items.Add("TO:選擇權");
                    }
                    // comboBoxUpdateTFOrder
                    {
                        comboBoxUpdateTFOrder.Items.Add("刪單");
                        comboBoxUpdateTFOrder.Items.Add("減量");
                        comboBoxUpdateTFOrder.Items.Add("改價");
                    }

                    //comboBoxCancelOrderByStockNoAdvanceBuySell
                    comboBoxCancelOrderByStockNoAdvanceBuySell.Items.Add("買");
                    comboBoxCancelOrderByStockNoAdvanceBuySell.Items.Add("賣");
                    comboBoxCancelOrderByStockNoAdvanceBuySell.Items.Add("無");
                }
            }

        } 
        private void checkBoxAsyncOrder_CheckedChanged(object sender, EventArgs e)
        {
            // 是否為非同步委託
            if (checkBoxAsyncOrder.Checked == true) bAsyncOrder = true;
            else bAsyncOrder = false;
        }
        private void buttonCancelOrderBySeqNo_Click(object sender, EventArgs e)
        {
            string bstrSeqNo = textBoxSeqNo.Text;
            string bstrAccount = comboBoxAccount.Text;

            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 國內委託删單(By委託序號)          
            int nCode = m_pSKOrder.CancelOrderBySeqNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrSeqNo, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelOrderBySeqNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonCancelOrderByBookNo_Click(object sender, EventArgs e)
        {
            string bstrBookNo = textBoxBookNo.Text;
            string bstrAccount = comboBoxAccount.Text;
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 國內委託删單(By委託書號)          
            int nCode = m_pSKOrder.CancelOrderByBookNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrBookNo, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelOrderByBookNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonCancelOrderByStockNo_Click(object sender, EventArgs e)
        {
            string bstrStockNo = textBoxCancelOrderByStockNo.Text;
            string bstrAccount = comboBoxAccount.Text;
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            string msg = "";

            if (comboBoxCancelOrderByStockNoAdvanceBuySell.Text == "買" || comboBoxCancelOrderByStockNoAdvanceBuySell.Text == "賣")
            {
                int nBuySell = 0;
                if (comboBoxCancelOrderByStockNoAdvanceBuySell.Text == "買") nBuySell = 0;
                else nBuySell = 1;

                // 國內委託删單(By帳號所屬登入ID+商品代號+買賣別+委託價格)        
                int nCode = m_pSKOrder.CancelOrderByStockNoAdvance(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrStockNo, nBuySell, textBoxCancelOrderByStockNoAdvancePrice.Text, out bstrMessage);
                // 取得回傳訊息
                msg = "【CancelOrderByStockNoAdvance】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            }
            else
            {
                // 國內委託删單(By ID+商品代號)          
                int nCode = m_pSKOrder.CancelOrderByStockNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrStockNo, out bstrMessage);
                // 取得回傳訊息
                msg = "【CancelOrderByStockNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            }

            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonDecreaseOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (textBoxStockDecreaseQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrSeqNo = textBoxSeqNo.Text;
                string bstrAccount = comboBoxAccount.Text;
                int nDecreaseQty = int.Parse(textBoxStockDecreaseQty.Text);
                string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

                // 國內委託減量(By委託序號)          
                int nCode = m_pSKOrder.DecreaseOrderBySeqNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrSeqNo, nDecreaseQty, out bstrMessage);
                // 取得回傳訊息
                string msg = "【DecreaseOrderBySeqNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonCorrectPriceBySeqNo_Click(object sender, EventArgs e)
        {
            string bstrAccount = comboBoxAccount.Text;
            string bstrSeqNo = textBoxSeqNo.Text; // 欲改量的委託序號
            string bstrPrice = textBoxPrice.Text; // 修改價格
            int nTradeType = 0; // 證券0:ROD 期選0: ROD  1:IOC  2:FOK

            string selectedValue2 = comboBoxTradeType.Text;
            if (selectedValue2 == "0:ROD") nTradeType = 0;
            else if (selectedValue2 == "1:IOC") nTradeType = 1;
            else if (selectedValue2 == "2:FOK") nTradeType = 2;
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 證期權依序號改價(By委託序號)          
            int nCode = m_pSKOrder.CorrectPriceBySeqNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrSeqNo, bstrPrice, nTradeType, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CorrectPriceBySeqNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonCorrectPriceByBookNo_Click(object sender, EventArgs e)
        {
            string bstrAccount = comboBoxAccount.Text;
            string bstrMarketSymbol = ""; // 市場類別 TS: 證券TF:期貨 TO:選擇權
            string selectedValue3 = comboBoxMarketSymbol.Text;
            if (selectedValue3 == "TS:證券") bstrMarketSymbol = "TS";
            else if (selectedValue3 == "TF:期貨") bstrMarketSymbol = "TF";
            else if (selectedValue3 == "TO:選擇權") bstrMarketSymbol = "TO";

            string bstrBookNo = textBoxBookNo.Text; // 欲改量的委託書號
            string bstrPrice = textBoxPrice.Text; // 修改價格
            int nTradeType = 0; // 證券0:ROD 期選0: ROD  1:IOC  2:FOK

            string selectedValue2 = comboBoxTradeType.Text;
            if (selectedValue2 == "0:ROD") nTradeType = 0;
            else if (selectedValue2 == "1:IOC") nTradeType = 1;
            else if (selectedValue2 == "2:FOK") nTradeType = 2;

            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 證期權依書號改價(By委託書號)          
            int nCode = m_pSKOrder.CorrectPriceByBookNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrMarketSymbol, bstrBookNo, bstrPrice, nTradeType, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CorrectPriceByBookNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
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
        private void comboBoxUpdateTFOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUpdateTFOrder.Text == "刪單")
            {
                // 刪單
                {
                    // Stock
                    {
                        buttonCancelOrderBySeqNo.Visible = true;
                        buttonCancelOrderByBookNo.Visible = true;
                        buttonCancelOrderByStockNo.Visible = true;
                    }
                }
                // 減量
                {
                    // Stock
                    {
                        labelDecreaseOrderBySeqNo.Visible = false;
                        textBoxStockDecreaseQty.Visible = false;
                        buttonDecreaseOrderBySeqNo.Visible = false;
                    }
                }
                // 改價
                {
                    // Stock
                    {
                        labelCorrectPriceBySeqNo.Visible = false;
                        textBoxPrice.Visible = false;
                        labelTradeType.Visible = false;
                        comboBoxTradeType.Visible = false;
                        labelCorrectPriceByBookNoMarketSymbol.Visible = false;
                        comboBoxMarketSymbol.Visible = false;
                        buttonCorrectPriceBySeqNo.Visible = false;
                        buttonCorrectPriceByBookNo.Visible = false;
                    }
                }
            }
            else if (comboBoxUpdateTFOrder.Text == "減量")
            {
                // 刪單
                {
                    // Stock
                    {
                        buttonCancelOrderBySeqNo.Visible = false;
                        buttonCancelOrderByBookNo.Visible = false;
                        buttonCancelOrderByStockNo.Visible = false;
                    }
                }
                // 減量
                {
                    // Stock
                    {
                        labelDecreaseOrderBySeqNo.Visible = true;
                        textBoxStockDecreaseQty.Visible = true;
                        buttonDecreaseOrderBySeqNo.Visible = true;
                    }
                }
                // 改價
                {
                    // Stock
                    {
                        labelCorrectPriceBySeqNo.Visible = false;
                        textBoxPrice.Visible = false;
                        labelTradeType.Visible = false;
                        comboBoxTradeType.Visible = false;
                        labelCorrectPriceByBookNoMarketSymbol.Visible = false;
                        comboBoxMarketSymbol.Visible = false;
                        buttonCorrectPriceBySeqNo.Visible = false;
                        buttonCorrectPriceByBookNo.Visible = false;
                    }
                }
            }
            else if (comboBoxUpdateTFOrder.Text == "改價")
            {
                // 刪單
                {
                    // Stock
                    {
                        buttonCancelOrderBySeqNo.Visible = false;
                        buttonCancelOrderByBookNo.Visible = false;
                        buttonCancelOrderByStockNo.Visible = false;
                    }
                }
                // 減量
                {
                    // Stock
                    {
                        labelDecreaseOrderBySeqNo.Visible = false;
                        textBoxStockDecreaseQty.Visible = false;
                        buttonDecreaseOrderBySeqNo.Visible = false;
                    }
                }
                // 改價
                {
                    // Stock
                    {
                        labelCorrectPriceBySeqNo.Visible = true;
                        textBoxPrice.Visible = true;
                        labelTradeType.Visible = true;
                        comboBoxTradeType.Visible = true;
                        labelCorrectPriceByBookNoMarketSymbol.Visible = true;
                        comboBoxMarketSymbol.Visible = true;
                        buttonCorrectPriceBySeqNo.Visible = true;
                        buttonCorrectPriceByBookNo.Visible = true;
                    }
                }
            }
        }
        private void OrderControlForm_Load(object sender, EventArgs e)
        {
            //下單帳號資訊
            m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(OnAccount);
            void OnAccount(string bstrLogInID, string bstrAccountData)
            {
                string[] values = bstrAccountData.Split(',');
                if (values[0] == "TS" || values[0] == "TF")
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
