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
    public partial class OFUpdateOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        // 是否為價差改價
        bool Spread = false;
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
        public OFUpdateOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //comboBox
                {
                    //comboBoxsCallPut
                    {
                        comboBoxOOCallPut.Items.Add("CALL");
                        comboBoxOOCallPut.Items.Add("PUT");
                    }
                    // comboBoxUpdateTFOrder
                    {
                        comboBoxUpdateTFOrder.Items.Add("刪單");
                        comboBoxUpdateTFOrder.Items.Add("減量");
                        comboBoxUpdateTFOrder.Items.Add("改價");
                    }
                }
            }
        }     
        private void checkBoxAsyncOrder_CheckedChanged(object sender, EventArgs e)
        {
            // 是否為非同步委託
            if (checkBoxAsyncOrder.Checked == true) bAsyncOrder = true;
            else bAsyncOrder = false;
        }  
        private void buttonOverSeaCancelOrderBySeqNo_Click(object sender, EventArgs e)
        {
            string bstrSeqNo = textBoxOverSeaCancelOrderBySeqNo.Text;
            string bstrAccount = comboBoxAccount.Text; //委託帳號(IB 4碼＋帳號 7碼)
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 海外期貨委託删單(By委託序號)
            int nCode = m_pSKOrder.OverSeaCancelOrderBySeqNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrSeqNo, out bstrMessage);
            // 取得回傳訊息
            string msg = "【OverSeaCancelOrderBySeqNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonOverSeaCancelOrderByBookNo_Click(object sender, EventArgs e)
        {
            string bstrBookNo = textBoxOverSeaCancelOrderByBookNo.Text;
            string bstrAccount = comboBoxAccount.Text; //委託帳號(IB＋帳號)
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 海外期貨委託删單(By委託書號)
            int nCode = m_pSKOrder.OverSeaCancelOrderByBookNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrBookNo, out bstrMessage);
            // 取得回傳訊息
            string msg = "【OverSeaCancelOrderByBookNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonOverSeaDecreaseOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (textBoxOverseaFutureDecreaseQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrSeqNo = textBoxOverSeaCancelOrderBySeqNo.Text;
                string bstrAccount = comboBoxAccount.Text;
                int nDecreaseQty = int.Parse(textBoxOverseaFutureDecreaseQty.Text);
                string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

                // 海期委託減量(By委託序號)          
                int nCode = m_pSKOrder.OverSeaDecreaseOrderBySeqNo(comboBoxUserID.Text, bAsyncOrder, bstrAccount, bstrSeqNo, nDecreaseQty, out bstrMessage);
                // 取得回傳訊息
                string msg = "【OverSeaDecreaseOrderBySeqNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void checkBoxSpread_CheckedChanged(object sender, EventArgs e)
        {
            // 是否為價差交易
            if (checkBoxSpread.Checked) Spread = true;
            else Spread = false;
        }      
        private void buttonOverSeaCorrectPriceByBookNo_Click(object sender, EventArgs e)
        {
            OVERSEAFUTUREORDERFORGW pOrder = new OVERSEAFUTUREORDERFORGW(); // SKCOM元件中的 OVRESEAFUTUREORDERFORGW物件，將下單條件填入該物件後，再帶入此欄位中
            string bstrMessage; //同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。非同步刪單：參照4 - 2 - b OnAsyncOrder。

            // 填入該物件OVERSEAFUTUREORDERFORGW pOrder
            {
                pOrder.bstrBookNo = textBoxOverSeaCancelOrderByBookNo.Text; // //委託書號{改價使用}
                pOrder.bstrFullAccount = comboBoxAccount.Text; //海期帳號，分公司四碼＋帳號7碼{改價使用}
                pOrder.bstrExchangeNo = textBoxOFExchangeNo.Text; //交易所代碼。{改價使用}
                pOrder.bstrStockNo = textBoxOFStockNo.Text; //期權代號。{改價使用}
                pOrder.bstrStockNo2 = textBoxOFStockNo2.Text; // 期價差商品代號{價差改價使用}
                pOrder.bstrYearMonth = textBoxOFYearMonth.Text; // 近月商品年月( YYYYMM) 6碼{改價使用}
                pOrder.bstrYearMonth2 = textBoxOFYearMonth2.Text; //遠月商品年月( YYYYMM) 6碼 {價差改價使用}
                pOrder.bstrOrderPrice = textBoxOFOrder.Text; //新委託價。{改價使用}
                pOrder.bstrOrderNumerator = textBoxOFOrderNumerator.Text; //新委託價分子。{改價使用}
                pOrder.bstrOrderDenominator = textBoxOFOrderDenominator.Text; //新委託價分母。{改價使用}
                pOrder.bstrStrikePrice = textBoxOOStrikePrice.Text; //履約價。{選擇權改價使用}
                pOrder.nTradeType = 0;//0:ROD  3:IOC  4:FOK { { 改價使用} 目前海期、海選均固定ROD}
                pOrder.nSpecialTradeType = 0; // 0:LMT {{改價使用}目前僅提供限價單改限價}

                string selectedValue = comboBoxOOCallPut.Text;//0:CALL  1:PUT {選擇權使用} 
                if (selectedValue == "CALL") pOrder.nCallPut = 0;
                else if (selectedValue == "PUT") pOrder.nCallPut = 1;
            }

            int nCode;
            string msg;
            if (textBoxOOStrikePrice.Text != "0") // 履約價不為0 => 選擇權
            {
                // 海選改價 (By 委託書號)         
                nCode = m_pSKOrder.OverSeaOptionCorrectPriceByBookNo(comboBoxUserID.Text, bAsyncOrder, pOrder, out bstrMessage);
                // 取得回傳訊息
                msg = "【OverSeaOptionCorrectPriceByBookNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
            }
            else // 海期
            {
                if (Spread == true) // 為價差改價
                {
                    // 海選價差改價 (By 委託書號)         
                    nCode = m_pSKOrder.OverSeaCorrectPriceSpreadByBookNo(comboBoxUserID.Text, bAsyncOrder, pOrder, out bstrMessage);
                    // 取得回傳訊息
                    msg = "【OverSeaCorrectPriceSpreadByBookNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
                }
                else
                {
                    // 海選改價 (By 委託書號)         
                    nCode = m_pSKOrder.OverSeaCorrectPriceByBookNo(comboBoxUserID.Text, bAsyncOrder, pOrder, out bstrMessage);
                    // 取得回傳訊息
                    msg = "【OverSeaCorrectPriceByBookNo】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + bstrMessage;
                }
            }
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
        private void comboBoxUpdateTFOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUpdateTFOrder.SelectedItem.ToString() == "刪單")
            {
                // 刪單
                {                   
                    // OverseaFuture
                    {
                        buttonOverSeaCancelOrderBySeqNo.Visible = true;
                        buttonOverSeaCancelOrderByBookNo.Visible = true;
                    }             
                }
                // 減量
                {
                    // OverseaFuture
                    {
                        labelDecreaseOrderBySeqNo2.Visible = false;
                        textBoxOverseaFutureDecreaseQty.Visible = false;
                        buttonOverSeaDecreaseOrderBySeqNo.Visible = false;
                    }
                }
                // 改價
                {
                    // OverseaFuture
                    {
                        labelOFExchangeNo.Visible = false;
                        textBoxOFExchangeNo.Visible = false;
                        labelOFStockNo.Visible = false;
                        textBoxOFStockNo.Visible = false;
                        labelOFStockNo2.Visible = false;
                        textBoxOFStockNo2.Visible = false;
                        labelOFYearMonth.Visible = false;
                        textBoxOFYearMonth.Visible = false;
                        labelOFYearMonth2.Visible = false;
                        textBoxOFYearMonth2.Visible = false;
                        labelOFOrder.Visible = false;
                        textBoxOFOrder.Visible = false;
                        labelOFOrderNumerator.Visible = false;
                        textBoxOFOrderNumerator.Visible = false;
                        labelOFOrderDenominator.Visible = false;
                        textBoxOFOrderDenominator.Visible = false;
                        labelOOStrikePrice.Visible = false;
                        textBoxOOStrikePrice.Visible = false;
                        labelOOCallPut.Visible = false;
                        comboBoxOOCallPut.Visible = false;
                        checkBoxSpread.Visible = false;
                        buttonOverSeaCorrectPriceByBookNo.Visible = false;
                    }
                }
            }
            else if (comboBoxUpdateTFOrder.SelectedItem.ToString() == "減量")
            {
                // 刪單
                {
                    // OverseaFuture
                    {
                        buttonOverSeaCancelOrderBySeqNo.Visible = false;
                        buttonOverSeaCancelOrderByBookNo.Visible = false;
                    }
                }
                // 減量
                {
                    // OverseaFuture
                    {
                        labelDecreaseOrderBySeqNo2.Visible = true;
                        textBoxOverseaFutureDecreaseQty.Visible = true;
                        buttonOverSeaDecreaseOrderBySeqNo.Visible = true;
                    }
                }
                // 改價
                {
                    // OverseaFuture
                    {
                        labelOFExchangeNo.Visible = false;
                        textBoxOFExchangeNo.Visible = false;
                        labelOFStockNo.Visible = false;
                        textBoxOFStockNo.Visible = false;
                        labelOFStockNo2.Visible = false;
                        textBoxOFStockNo2.Visible = false;
                        labelOFYearMonth.Visible = false;
                        textBoxOFYearMonth.Visible = false;
                        labelOFYearMonth2.Visible = false;
                        textBoxOFYearMonth2.Visible = false;
                        labelOFOrder.Visible = false;
                        textBoxOFOrder.Visible = false;
                        labelOFOrderNumerator.Visible = false;
                        textBoxOFOrderNumerator.Visible = false;
                        labelOFOrderDenominator.Visible = false;
                        textBoxOFOrderDenominator.Visible = false;
                        labelOOStrikePrice.Visible = false;
                        textBoxOOStrikePrice.Visible = false;
                        labelOOCallPut.Visible = false;
                        comboBoxOOCallPut.Visible = false;
                        checkBoxSpread.Visible = false;
                        buttonOverSeaCorrectPriceByBookNo.Visible = false;
                    }
                }
            }
            else if (comboBoxUpdateTFOrder.SelectedItem.ToString() == "改價")
            {
                // 刪單
                {
                    // OverseaFuture
                    {
                        buttonOverSeaCancelOrderBySeqNo.Visible = false;
                        buttonOverSeaCancelOrderByBookNo.Visible = false;
                    }
                }
                // 減量
                {
                    // OverseaFuture
                    {
                        labelDecreaseOrderBySeqNo2.Visible = false;
                        textBoxOverseaFutureDecreaseQty.Visible = false;
                        buttonOverSeaDecreaseOrderBySeqNo.Visible = false;
                    }
                }
                // 改價
                {
                    // OverseaFuture
                    {
                        labelOFExchangeNo.Visible = true;
                        textBoxOFExchangeNo.Visible = true;
                        labelOFStockNo.Visible = true;
                        textBoxOFStockNo.Visible = true;
                        labelOFStockNo2.Visible = true;
                        textBoxOFStockNo2.Visible = true;
                        labelOFYearMonth.Visible = true;
                        textBoxOFYearMonth.Visible = true;
                        labelOFYearMonth2.Visible = true;
                        textBoxOFYearMonth2.Visible = true;
                        labelOFOrder.Visible = true;
                        textBoxOFOrder.Visible = true;
                        labelOFOrderNumerator.Visible = true;
                        textBoxOFOrderNumerator.Visible = true;
                        labelOFOrderDenominator.Visible = true;
                        textBoxOFOrderDenominator.Visible = true;
                        labelOOStrikePrice.Visible = true;
                        textBoxOOStrikePrice.Visible = true;
                        labelOOCallPut.Visible = true;
                        comboBoxOOCallPut.Visible = true;
                        checkBoxSpread.Visible = true;
                        buttonOverSeaCorrectPriceByBookNo.Visible = true;
                    }
                }
            }
        }    
    }
}
