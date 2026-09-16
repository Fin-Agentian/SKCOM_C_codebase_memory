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
    public partial class OFReadOrderForm : Form
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
        public OFReadOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //dataGridView
                {
                    //dataGridViewOnOverseaFuture
                    {
                        dataGridViewOnOverseaFuture.Columns.Add("Column1", "交易所代碼");
                        dataGridViewOnOverseaFuture.Columns.Add("Column2", "交易所名稱");
                        dataGridViewOnOverseaFuture.Columns.Add("Column3", "商品代碼");
                        dataGridViewOnOverseaFuture.Columns.Add("Column4", "商品名稱");
                        dataGridViewOnOverseaFuture.Columns.Add("Column5", "年月");
                        dataGridViewOnOverseaFuture.Columns.Add("Column6", "跳動點");
                        dataGridViewOnOverseaFuture.Columns.Add("Column7", "分母");
                        dataGridViewOnOverseaFuture.Columns.Add("Column8", "可接受交易種類");
                        dataGridViewOnOverseaFuture.Columns.Add("Column9", "可當沖");
                        dataGridViewOnOverseaFuture.Columns.Add("Column10", "委託時效(ROD;FOK;IOC)");
                    }
                    //dataGridViewOnOverseaOption
                    {
                        dataGridViewOnOverseaOption.Columns.Add("Column1", "交易所代碼");
                        dataGridViewOnOverseaOption.Columns.Add("Column2", "交易所名稱");
                        dataGridViewOnOverseaOption.Columns.Add("Column3", "商品代碼");
                        dataGridViewOnOverseaOption.Columns.Add("Column4", "商品名稱");
                        dataGridViewOnOverseaOption.Columns.Add("Column5", "年月");
                        dataGridViewOnOverseaOption.Columns.Add("Column6", "跳動點");
                        dataGridViewOnOverseaOption.Columns.Add("Column7", "履約價最小跳動點");
                        dataGridViewOnOverseaOption.Columns.Add("Column8", "基準履約價");
                        dataGridViewOnOverseaOption.Columns.Add("Column9", "最低履約價");
                        dataGridViewOnOverseaOption.Columns.Add("Column10", "最高履約價");
                        dataGridViewOnOverseaOption.Columns.Add("Column11", "履約價除數");
                        dataGridViewOnOverseaOption.Columns.Add("Column12", "分母");
                        dataGridViewOnOverseaOption.Columns.Add("Column13", "可委託類型");
                        dataGridViewOnOverseaOption.Columns.Add("Column14", "當沖減收保證金");
                        dataGridViewOnOverseaOption.Columns.Add("Column15", "商品年月");
                    }
                    //dataGridViewOnFutureRights
                    {
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column1", "期貨商代碼");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column2", "IB代號");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column3", "帳號");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column4", "幣別");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column5", "風險指標/維持率");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column6", "足額維持率");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column7", "可動用(出金)保證金");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column8", "委託保證金");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column9", "維持保證金");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column10", "原始保證金");

                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column11", "淨值權益數、權益總值");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column12", "預扣權利金");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column13", "未沖銷買方選擇權市值");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column14", "未沖銷賣方選擇權市值");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column15", "帳戶權益、權益數");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column16", "盤中未實現");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column17", "未沖銷期貨浮動損益");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column18", "今日權利金支付");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column19", "本日期貨平倉損益淨額");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column20", "已實現費用、手續費");

                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column21", "今日存提款");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column22", "前日帳戶餘額");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column23", "參考匯率");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column24", "選擇權到期差益");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column25", "選擇權到期差損");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column26", "期貨交割損益");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column27", "超額/追繳保證金");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column28", "到期履約損益");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column29", "帳戶餘額");
                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column30", "固定保證金");

                        dataGridViewOnOverSeaFutureRight.Columns.Add("Column31", "變動保證金");
                    }
                    //dataGridViewOnOverseaFutureOpenInterestGW (dataGridViewOnOFOpenInterestGWReport1) 彙總
                    {
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column1", "帳號");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column2", "交易所代號");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column3", "交易所中文名稱");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column4", "含年月-商品代號/履約價、C/P");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column5", "不含年月-商品代碼");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column6", "商品年月");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column7", "商品中文名稱");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column8", "買賣別");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column9", "未平口數");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column10", "成交均價");

                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column11", "(非即時)現價又稱市價");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column12", "未平損益");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column13", "委託買");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column14", "買方委託口數");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column15", "委託賣");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column16", "賣方委託口數");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column17", "賣方委託新倉口數");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column18", "昨日結算價");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column19", "當沖未平口數");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column20", "原始保證金");

                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column21", "是否為選擇權(0->期貨 1->選擇權)");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column22", "履約價");
                        dataGridViewOnOFOpenInterestGWReport1.Columns.Add("Column23", "C/P (C:Call/P:Put)");
                    }
                    //dataGridViewOnOverseaFutureOpenInterestGW (dataGridViewOnOFOpenInterestGWReport2) 彙總
                    {
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column1", "帳號");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column2", "成交日期");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column3", "交易所代碼");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column4", "交易所中文名稱");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column5", "商品代碼不含年月/含C/P ");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column6", "商品年月");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column7", "商品中文名稱");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column8", "買賣別");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column9", "成交口數");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column10", "未平倉口數");

                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column11", "成交價");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column12", "現價");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column13", "未平損益");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column14", "委託書序號(前2碼櫃號)");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column15", "當沖註記");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column16", "是否為選擇權(0->期貨 1->選擇權)");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column17", "原始保證金");
                        dataGridViewOnOFOpenInterestGWReport2.Columns.Add("Column18", "履約價");
                    }
                }

                //comboBox
                {
                    // comboBoxnFormat
                    {
                        comboBoxnFormat.Items.Add("1.彙總");
                        comboBoxnFormat.Items.Add("2.明細");
                    }
                }
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
  
        private void ReadOrderForm_Load(object sender, EventArgs e)
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
            //查詢海外權益數
            m_pSKOrder.OnOverSeaFutureRight += new _ISKOrderLibEvents_OnOverSeaFutureRightEventHandler(OnOverSeaFutureRight);
            void OnOverSeaFutureRight(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "000") // 成功
                {
                    //dataGridViewOnOverSeaFutureRight.Rows.Add(bstrData);
                }
                else if (values[0] == "103" || values[0] == "980") // 帳號非期貨帳號 或 後台問題
                {
                    dataGridViewOnOverSeaFutureRight.Rows.Add(bstrData);
                }
                else
                {
                    dataGridViewOnOverSeaFutureRight.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30]);
                }
            }
            //海外期貨未平倉彙總資料。透過呼叫GetOverSeaFutureOpenInterestGW 後，資訊由該事件回傳
            m_pSKOrder.OnOFOpenInterestGWReport += new _ISKOrderLibEvents_OnOFOpenInterestGWReportEventHandler(OnOFOpenInterestGWReport);
            void OnOFOpenInterestGWReport(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "@@") // 回傳成功
                {
                    //dataGridViewOnOFOpenInterestGWReport.Rows.Add(bstrData);
                }
                else
                {
                    if (tabControlGetOverSeaFutureOpenInterestGW.SelectedTab == tabPageGetOverSeaFutureOpenInterestGWnFormat1) // 彙總
                    {
                        // 海期 
                        if (values[20] == "0") dataGridViewOnOFOpenInterestGWReport1.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21]);
                        // 海選
                        else dataGridViewOnOFOpenInterestGWReport1.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22]);
                    }
                    else if (tabControlGetOverSeaFutureOpenInterestGW.SelectedTab == tabPageGetOverSeaFutureOpenInterestGWnFormat2) // 明細
                    {
                        // 海期
                        if (values[16] == "0") dataGridViewOnOFOpenInterestGWReport2.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17]);
                        // 海選
                        else dataGridViewOnOFOpenInterestGWReport2.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17]);
                    }
                }
            }
            //查詢海外期貨下單商品
            m_pSKOrder.OnOverseaFuture += new _ISKOrderLibEvents_OnOverseaFutureEventHandler(OnOverseaFuture);
            void OnOverseaFuture(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(';');
                dataGridViewOnOverseaFuture.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9]);
            }
            //查詢海選期貨下單商品
            m_pSKOrder.OnOverseaOption += new _ISKOrderLibEvents_OnOverseaOptionEventHandler(OnOverseaOption);
            void OnOverseaOption(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                dataGridViewOnOverseaOption.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14]);
            }
            // 取回可交易的所有帳號
            {
                int nCode = m_pSKOrder.GetUserAccount();
                // 取得回傳訊息
                string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void buttonGetOverseaFutures_Click(object sender, EventArgs e)
        {
            dataGridViewOnOverseaFuture.Rows.Clear(); // 每次查詢前，清除上一次查詢結果

            // 查詢海外期貨下單商品           
            int nCode = m_pSKOrder.GetOverseaFutures();
            // 取得回傳訊息
            string msg = "【GetOverseaFutures】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonGetOverseaOptions_Click(object sender, EventArgs e)
        {
            dataGridViewOnOverseaOption.Rows.Clear(); // 每次查詢前，清除上一次查詢結果

            // 查詢海選期貨下單商品           
            int nCode = m_pSKOrder.GetOverseaOptions();
            // 取得回傳訊息
            string msg = "【GetOverseaOptions】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonGetRequestOverSeaFutureRight_Click(object sender, EventArgs e)
        {
            string bstrLogInID = comboBoxUserID.Text; // 登入ID
            string bstrAccount = comboBoxAccount.Text; // 委託帳號 ( IB＋帳號)

            dataGridViewOnOverSeaFutureRight.Rows.Clear(); //清空

            // 查詢海外期貨權益數
            int nCode = m_pSKOrder.GetRequestOverSeaFutureRight(bstrLogInID, bstrAccount);
            // 取得回傳訊息
            string msg = "【GetRequestOverSeaFutureRight】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonGetOverseaFutureOpenInterestGW_Click(object sender, EventArgs e)
        {
            string bstrLogInID = comboBoxUserID.Text; // 登入ID
            string bstrAccount = comboBoxAccount.Text; // 委託帳號 (IB＋帳號)
            int nFormat = 1; // 查詢格式(1: 彙總;2: 明細)
            if (comboBoxnFormat.Text == "1.彙總") // 海期未平倉-彙總
            {
                nFormat = 1;
                dataGridViewOnOFOpenInterestGWReport1.Rows.Clear(); //清空   
            }
            else if (comboBoxnFormat.Text == "2.明細") // 海期未平倉-明細
            {
                nFormat = 2;
                dataGridViewOnOFOpenInterestGWReport2.Rows.Clear(); //清空
            }
            // 查詢海外期貨未平倉GW。(含格式彙總及明細)
            int nCode = m_pSKOrder.GetOverseaFutureOpenInterestGW(bstrLogInID, bstrAccount, nFormat);
            // 取得回傳訊息
            string msg = "【GetOverseaFutureOpenInterestGW】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
