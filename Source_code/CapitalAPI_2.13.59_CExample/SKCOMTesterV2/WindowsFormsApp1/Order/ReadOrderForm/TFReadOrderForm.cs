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
    public partial class TFReadOrderForm : Form
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
        public TFReadOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //dataGridView
                {
                    //dataGridViewOnFutureRights
                    {
                        dataGridViewOnFutureRights.Columns.Add("Column1", "帳戶餘額");
                        dataGridViewOnFutureRights.Columns.Add("Column2", "浮動損益");
                        dataGridViewOnFutureRights.Columns.Add("Column3", "已實現費用");
                        dataGridViewOnFutureRights.Columns.Add("Column4", "交易稅");
                        dataGridViewOnFutureRights.Columns.Add("Column5", "預扣權利金");
                        dataGridViewOnFutureRights.Columns.Add("Column6", "權利金收付");
                        dataGridViewOnFutureRights.Columns.Add("Column7", "權益數");
                        dataGridViewOnFutureRights.Columns.Add("Column8", "超額保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column9", "存提款");
                        dataGridViewOnFutureRights.Columns.Add("Column10", "買方市值");

                        dataGridViewOnFutureRights.Columns.Add("Column11", "賣方市值");
                        dataGridViewOnFutureRights.Columns.Add("Column12", "期貨平倉損益");
                        dataGridViewOnFutureRights.Columns.Add("Column13", "盤中未實現");
                        dataGridViewOnFutureRights.Columns.Add("Column14", "原始保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column15", "維持保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column16", "部位原始保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column17", "部位維持保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column18", "委託保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column19", "超額最佳保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column20", "權利總值");

                        dataGridViewOnFutureRights.Columns.Add("Column21", "預扣費用");
                        dataGridViewOnFutureRights.Columns.Add("Column22", "原始保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column23", "昨日餘額");
                        dataGridViewOnFutureRights.Columns.Add("Column24", "選擇權組合單加不加收保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column25", "維持率");
                        dataGridViewOnFutureRights.Columns.Add("Column26", "幣別");
                        dataGridViewOnFutureRights.Columns.Add("Column27", "足額原始保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column28", "足額維持保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column29", "足額可用");
                        dataGridViewOnFutureRights.Columns.Add("Column30", "抵繳金額");

                        dataGridViewOnFutureRights.Columns.Add("Column31", "有價可用");
                        dataGridViewOnFutureRights.Columns.Add("Column32", "可用餘額");
                        dataGridViewOnFutureRights.Columns.Add("Column33", "足額現金可用");
                        dataGridViewOnFutureRights.Columns.Add("Column34", "有價價值");
                        dataGridViewOnFutureRights.Columns.Add("Column35", "風險指標");
                        dataGridViewOnFutureRights.Columns.Add("Column36", "選擇權到期差異");
                        dataGridViewOnFutureRights.Columns.Add("Column37", "選擇權到期差損");
                        dataGridViewOnFutureRights.Columns.Add("Column38", "期貨到期損益");
                        dataGridViewOnFutureRights.Columns.Add("Column39", "加收保證金");
                        dataGridViewOnFutureRights.Columns.Add("Column40", "LOGIN_ID");

                        dataGridViewOnFutureRights.Columns.Add("Column41", "ACCOUNT_NO");
                    }
                    // dataGridViewOnOpenInterest
                    {
                        dataGridViewOnOpenInterest.Columns.Add("Column1", "市場別");
                        dataGridViewOnOpenInterest.Columns.Add("Column2", "帳號");
                        dataGridViewOnOpenInterest.Columns.Add("Column3", "商品");
                        dataGridViewOnOpenInterest.Columns.Add("Column4", "買賣別");
                        dataGridViewOnOpenInterest.Columns.Add("Column5", "未平倉部位");
                        dataGridViewOnOpenInterest.Columns.Add("Column6", "當沖未平倉部位");
                        dataGridViewOnOpenInterest.Columns.Add("Column7", "平均成本(小數部分已處理)");
                        dataGridViewOnOpenInterest.Columns.Add("Column8", "單口手續費");
                        dataGridViewOnOpenInterest.Columns.Add("Column9", "交易稅(萬分之X)");
                        dataGridViewOnOpenInterest.Columns.Add("Column10", "LOGIN_ID");
                    }
                }
                //comboBox
                {
                    // comboBoxCoinType
                    {
                        comboBoxCoinType.Items.Add("0:全幣別");
                        comboBoxCoinType.Items.Add("1:基幣(台幣TWD)");
                        comboBoxCoinType.Items.Add("2:人民幣RMB");
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
            //查詢國內權益數
            m_pSKOrder.OnFutureRights += new _ISKOrderLibEvents_OnFutureRightsEventHandler(OnFutureRights);
            void OnFutureRights(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "980" || values[0] == "970") // 後台的問題
                {
                    dataGridViewOnFutureRights.Rows.Add(bstrData);
                }
                else
                {
                    dataGridViewOnFutureRights.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40]);
                }
            }
            //查詢期貨未平倉－可指定回傳格式
            m_pSKOrder.OnOpenInterest += new _ISKOrderLibEvents_OnOpenInterestEventHandler(OnOpenInterest);
            void OnOpenInterest(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "001" || values[0] == "970") // 查無資料 或 後台的問題
                {
                    dataGridViewOnOpenInterest.Rows.Add(bstrData);
                }
                else
                {
                    dataGridViewOnOpenInterest.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9]);
                }
            }
            // 取回可交易的所有帳號
            {
                int nCode = m_pSKOrder.GetUserAccount();
                // 取得回傳訊息
                string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void buttonGetOpenInterestGW_Click(object sender, EventArgs e)
        {
            string bstrLogInID = comboBoxUserID.Text; // 登入ID
            string bstrAccount = comboBoxAccount.Text; // 委託帳號 (IB＋帳號) 
            int nFormat = 1; // 回傳格式：1
            dataGridViewOnOpenInterest.Rows.Clear(); //清空

            // 查詢期貨未平倉－可指定回傳格式
            int nCode = m_pSKOrder.GetOpenInterestGW(bstrLogInID, bstrAccount, nFormat);
            // 取得回傳訊息
            string msg = "【GetOpenInterestGW】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonGetFutureRights_Click(object sender, EventArgs e)
        {
            dataGridViewOnFutureRights.Rows.Clear(); // 每次查詢前，清除上一次查詢結果

            short sCoinType = 0; //0:全幣別，1:基幣(台幣TWD)，2:人民幣RMB
            string bstrAccount = comboBoxAccount.Text;
            string selectedValue = comboBoxCoinType.SelectedItem.ToString(); //0:全幣別，1:基幣(台幣TWD)，2:人民幣RMB
            if (selectedValue == "0:全幣別") sCoinType = 0;
            else if (selectedValue == "1:基幣(台幣TWD)") sCoinType = 1;
            else if (selectedValue == "2:人民幣RMB") sCoinType = 2;

            // 查詢國內權益數       
            int nCode = m_pSKOrder.GetFutureRights(comboBoxUserID.Text, bstrAccount, sCoinType);
            // 取得回傳訊息
            string msg = "【GetFutureRights】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
