using System;
using System.Collections.Generic;
using SKCOMLib;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        // 關閉標誌，關閉From後，不讓事件將訊息傳遞至控件上
        bool isClosing = false;
        // 宣告SK物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
        SKReplyLib m_pSKReply = new SKReplyLib(); // 回報物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); // 下單物件
        // 是否為非同步委託
        bool bAsyncOrder = false;
        // 存[UserID]對應 交易帳號
        Dictionary<string, List<string>> m_dictUserID = new Dictionary<string, List<string>>();
        List<string> allkeys;
        static void AddUserID(Dictionary<string, List<string>> dictUserID, string UserID, string AccountData)
        {

            string[] values = AccountData.Split(',');

            // broker ID (IB)4碼 + 帳號7碼
            string Account = values[1] + values[3];

            if (dictUserID.ContainsKey(UserID))
            {
                bool accountExists = false;
                foreach (string existingAccount in dictUserID[UserID])
                {
                    if (existingAccount == Account)
                    {
                        accountExists = true;
                        break;
                    }
                }

                if (!accountExists)
                {
                    dictUserID[UserID].Add(Account);
                }
            }
            else
            {
                // 如果不存在，創建一個新的 List<string>，並添加到字典中
                dictUserID[UserID] = new List<string> { Account };
            }

        }
        public MainForm()
        {
            //Init
            {
                InitializeComponent();
                // comboBox
                {
                    // comboBoxSKCenterLib_SetAuthority
                    {
                        comboBoxSKCenterLib_SetAuthority.Items.Add("正式環境");
                        comboBoxSKCenterLib_SetAuthority.Items.Add("正式環境SGX");
                        comboBoxSKCenterLib_SetAuthority.Items.Add("測試環境");
                        comboBoxSKCenterLib_SetAuthority.Items.Add("測試環境SGX");
                    }
                    // 其它>>>限制
                    {
                        //comboBoxSelectMarketType
                        {
                            comboBoxSelectMarketType.Items.Add("TS(證券)");
                            comboBoxSelectMarketType.Items.Add("TF(期貨)");
                            comboBoxSelectMarketType.Items.Add("TO(選擇權)");
                            comboBoxSelectMarketType.Items.Add("OS(複委託)");
                            comboBoxSelectMarketType.Items.Add("OF(海外期貨)");
                            comboBoxSelectMarketType.Items.Add("OO(海外選擇權)");
                        }
                    }
                    // 出入金互轉
                    {
                        //comboBoxWithDrawnTypeOut
                        {
                            comboBoxWithDrawnTypeOut.Items.Add("國內");
                            comboBoxWithDrawnTypeOut.Items.Add("國外");
                        }
                        //comboBoxWithDrawnTypeIn
                        {
                            comboBoxWithDrawnTypeIn.Items.Add("國內");
                            comboBoxWithDrawnTypeIn.Items.Add("國外");
                        }
                        //comboBoxWithDrawnCurrency
                        {
                            comboBoxWithDrawnCurrency.Items.Add("澳幣");
                            comboBoxWithDrawnCurrency.Items.Add("歐元");
                            comboBoxWithDrawnCurrency.Items.Add("英鎊");
                            comboBoxWithDrawnCurrency.Items.Add("港幣");
                            comboBoxWithDrawnCurrency.Items.Add("日元");
                            comboBoxWithDrawnCurrency.Items.Add("台幣");
                            comboBoxWithDrawnCurrency.Items.Add("紐幣");
                            comboBoxWithDrawnCurrency.Items.Add("人民幣");
                            comboBoxWithDrawnCurrency.Items.Add("美元");
                            comboBoxWithDrawnCurrency.Items.Add("南非幣");
                        }
                    }
                    // 選擇權部位
                    {
                        // comboBoxAssembleOptions
                        {
                            comboBoxAssembleOptions.Items.Add("組合");
                            comboBoxAssembleOptions.Items.Add("複式單拆解");
                            comboBoxAssembleOptions.Items.Add("雙邊了結");
                        }
                        //comboBoxsBuySell
                        {
                            comboBoxsBuySell.Items.Add("買進");
                            comboBoxsBuySell.Items.Add("賣出");
                        }
                        //comboBoxsBuySell2
                        {
                            comboBoxsBuySell2.Items.Add("買進");
                            comboBoxsBuySell2.Items.Add("賣出");
                        }
                    }
                    // 大小台電金互抵
                    {
                        //comboBoxSendTFOffSetnCommodity
                        {
                            comboBoxSendTFOffSetnCommodity.Items.Add("大小台");
                            comboBoxSendTFOffSetnCommodity.Items.Add("大小電");
                            comboBoxSendTFOffSetnCommodity.Items.Add("大小金");
                        }
                        //comboBoxSendTFOffSetnBuySell
                        {
                            comboBoxSendTFOffSetnBuySell.Items.Add("多方(買)");
                            comboBoxSendTFOffSetnBuySell.Items.Add("空方(賣)");
                        }
                    }
                    // 指定大、小、微台互抵
                    {
                        //comboBoxSendTFOffsetNewnCommodity
                        {
                            comboBoxSendTFOffsetNewnCommodity.Items.Add("0:大抵微");
                            comboBoxSendTFOffsetNewnCommodity.Items.Add("1:小抵微");
                            comboBoxSendTFOffsetNewnCommodity.Items.Add("2:大小抵微");
                            comboBoxSendTFOffsetNewnCommodity.Items.Add("3:大抵小微");
                            comboBoxSendTFOffsetNewnCommodity.Items.Add("4:小抵大微");
                            comboBoxSendTFOffsetNewnCommodity.Items.Add("5:大抵小");
                        }
                        //comboBoxSendTFOffsetNewnBuySell
                        {
                            comboBoxSendTFOffsetNewnBuySell.Items.Add("多方(買)");
                            comboBoxSendTFOffsetNewnBuySell.Items.Add("空方(賣)");
                        }
                    }
                    // comboBoxGetOrderReport
                    {
                        comboBoxGetOrderReport.Items.Add("1:全部");
                        comboBoxGetOrderReport.Items.Add("2:有效");
                        comboBoxGetOrderReport.Items.Add("3:可消");
                        comboBoxGetOrderReport.Items.Add("4:已消");
                        comboBoxGetOrderReport.Items.Add("5:已成");
                        comboBoxGetOrderReport.Items.Add("6:失敗");
                        comboBoxGetOrderReport.Items.Add("7:合併同價格");
                        comboBoxGetOrderReport.Items.Add("8:合併同商品");
                        comboBoxGetOrderReport.Items.Add("9:預約");
                    }
                    // comboBoxGetFulfillReport
                    {
                        comboBoxGetFulfillReport.Items.Add("1:完整");
                        comboBoxGetFulfillReport.Items.Add("2:合併同書號");
                        comboBoxGetFulfillReport.Items.Add("3:合併同價格");
                        comboBoxGetFulfillReport.Items.Add("4:合併同商品");
                        comboBoxGetFulfillReport.Items.Add("5:T+1成交回報");
                    }
                }
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBoxSKCenterLib_SetAuthority.SelectedIndex = 0; // 正式環境
            textBoxPassword.PasswordChar = '*'; // 輸入密碼顯示為****

            // 註冊公告(必要)
            {
                m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(OnAnnouncement);
                void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
                {
                    nConfirmCode = -1;
                    // 取得回傳訊息
                    string msg = "【註冊公告OnReplyMessage】" + strUserID + "_" + bstrMessage;
                    if (isClosing != true)
                        richTextBoxMessage.AppendText(msg + "\n");
                }
            }
            // 同意書狀態通知
            {
                m_pSKCenter.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(OnShowAgreement);
                void OnShowAgreement(string bstrData)
                {
                    // 取得回傳訊息
                    string msg = "【OnShowAgreement】" + bstrData;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
            }
            // 定時Timer通知。每分鐘會由該函式得到一個時間
            {
                m_pSKCenter.OnTimer += new _ISKCenterLibEvents_OnTimerEventHandler(OnTimer);
                void OnTimer(int nTime)
                {
                    // 取得回傳訊息
                    string msg = "【OnTimer】" + nTime;
                    if (isClosing != true)
                        richTextBoxMessage.AppendText(msg + "\n");
                }
            }
            // SGX API DMA專線下單連線狀態
            {
                m_pSKCenter.OnNotifySGXAPIOrderStatus += new _ISKCenterLibEvents_OnNotifySGXAPIOrderStatusEventHandler(OnNotifySGXAPIOrderStatus);
                void OnNotifySGXAPIOrderStatus(int nStatus, string bstrOFAccount)
                {
                    // 取得回傳訊息
                    string msg = "【OnNotifySGXAPIOrderStatus】" + "【連線狀態】" + nStatus + "【連線SGX帳號】" + bstrOFAccount;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
            }
            // 取得目前註冊SKAPI 版本及位元
            {
                labelSKCenterLib_GetSKAPIVersionAndBit.Text = m_pSKCenter.SKCenterLib_GetSKAPIVersionAndBit("xxxxx");
            }
            //下單帳號資訊
            {
                m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(OnAccount);
                void OnAccount(string bstrLogInID, string bstrAccountData)
                {
                    richTextBoxMessage.AppendText("使用者:" + bstrLogInID + "帳號資訊:" + bstrAccountData + '\n');

                    AddUserID(m_dictUserID, bstrLogInID, bstrAccountData);
                }
            }
            //一個使用者id會與proxy server建一條連線，此事件回傳此條連線的連線狀態。透過呼叫 SKOrderLib_InitialProxyByID 後，資訊由該事件回傳。
            {
                m_pSKOrder.OnProxyStatus += new _ISKOrderLibEvents_OnProxyStatusEventHandler(OnProxyStatus);
                void OnProxyStatus(string bstrUserId, int nCode)
                {
                    string msg = "";
                    if (nCode == 5001) msg = "SK_PROXY_SERVER_LOGIN_SUCCESS";
                    else if (nCode == 5002) msg = "SK_PROXY_SERVER_LOGIN_FAIL";
                    else if (nCode == 5003) msg = "SK_PROXY_SERVER_LOGIN_DISCONNECT";
                    else if (nCode == 5004) msg = "SK_PROXY_SERVER_SCHEDULE_DIALY_DISCONNECT";
                    else if (nCode == 5005) msg = "SK_PROXY_SERVER_SWITCH_MODE";
                    else if (nCode == 5006) msg = "SK_PROXY_SERVER_CONNECTION_IS_EXIST";
                    else if (nCode == 5011) msg = "SK_PROXY_SERVER_CONNECT_FAIL";
                    richTextBoxMessage.AppendText("使用者:" + bstrUserId + "," + msg + "\n");
                }
            }
            //透過呼叫 SKOrderLib_TelnetTest 後，資訊由該事件回傳
            {
                m_pSKOrder.OnTelnetTest += new _ISKOrderLibEvents_OnTelnetTestEventHandler(OnTelnetTest);
                void OnTelnetTest(string bstrData)
                {
                    richTextBoxMessage.AppendText("【OnTelnetTest】" + bstrData + '\n');
                }
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 顯示確認對話框，詢問用戶是否確定要關閉應用程式
            DialogResult result = MessageBox.Show("確定要關閉範例程式嗎？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // 如果用戶選擇 "No"，取消關閉操作
            if (result == DialogResult.No) e.Cancel = true;
            isClosing = true;
        }
        private void checkBoxisAP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxisAP.Checked)
            {
                labelCustCertID.Visible = true;
                textBoxCustCertID.Visible = true;
                buttonSKCenterLib_GenerateKeyCert.Visible = true;
            }
            else
            {
                labelCustCertID.Visible = false;
                textBoxCustCertID.Visible = false;
                buttonSKCenterLib_GenerateKeyCert.Visible = false;
            }
        }
        private void comboBoxSKCenterLib_SetAuthority_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nAuthorityFlag = 0; //SGX 專線屬性：bit 0:關閉／開啟(0／1)；bit 1 環境切換
            if (comboBoxSKCenterLib_SetAuthority.Text == "正式環境") nAuthorityFlag = 0;
            else if (comboBoxSKCenterLib_SetAuthority.Text == "正式環境SGX") nAuthorityFlag = 1;
            else if (comboBoxSKCenterLib_SetAuthority.Text == "測試環境") nAuthorityFlag = 2;
            else  nAuthorityFlag = 3; // 測試環境SGX

            // (SGX 專線only)手動設定特殊功能屬性開啟或關閉
            int nCode = m_pSKCenter.SKCenterLib_SetAuthority(nAuthorityFlag);
            // 取得回傳訊息
            string msg = "【SKCenterLib_SetAuthority】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void comboBoxAssembleOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAssembleOptions.Text == "複式單拆解")
            {
                // 代號2
                labelbstrStockNo2.Visible = false;
                textBoxbstrStockNo2.Visible = false;

                // 買/賣1
                labelTFsBuySell.Visible = true;
                comboBoxsBuySell.Visible = true;

                // 買/賣2
                labelsBuySell2.Visible = false;
                comboBoxsBuySell2.Visible = false;
            }
            else if (comboBoxAssembleOptions.Text == "雙邊了結")
            {
                // 代號2
                labelbstrStockNo2.Visible = false;
                textBoxbstrStockNo2.Visible = false;

                // 買/賣1
                labelTFsBuySell.Visible = false;
                comboBoxsBuySell.Visible = false;

                // 買/賣2
                labelsBuySell2.Visible = false;
                comboBoxsBuySell2.Visible = false;
            }
            else if (comboBoxAssembleOptions.Text == "組合")
            {
                // 代號2
                labelbstrStockNo2.Visible = true;
                textBoxbstrStockNo2.Visible = true;

                // 買/賣1
                labelTFsBuySell.Visible = true;
                comboBoxsBuySell.Visible = true;

                // 買/賣2
                labelsBuySell2.Visible = true;
                comboBoxsBuySell2.Visible = true;
            }
        }
        private void checkBoxAsyncOrder_CheckedChanged(object sender, EventArgs e)
        {
            // 是否為非同步委託
            if (checkBoxAsyncOrder.Checked == true) bAsyncOrder = true;
            else bAsyncOrder = false;
        }
        private void buttonSKCenterLib_RequestAgreement_Click(object sender, EventArgs e)
        {
            // 取得所有聲明書及同意書簽署狀態
            int nCode = m_pSKCenter.SKCenterLib_RequestAgreement(textBoxUserID.Text);
            // 取得回傳訊息
            string msg = "【SKCenterLib_RequestAgreement】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKCenterLib_SetLogPath_Click(object sender, EventArgs e)
        {
            string bstrPath = ""; // LOG檔存放路徑
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // 設定對話方塊的標題
                folderBrowserDialog.Description = "選擇資料夾";

                // 如果使用者選擇了資料夾，則取得資料夾路徑
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    bstrPath = folderBrowserDialog.SelectedPath; // LOG檔存放路徑
                    MessageBox.Show("選擇的資料夾:" + bstrPath);
                }
            }

            if (bstrPath == "")
            {
                MessageBox.Show("未選擇資料夾!");
            }
            else
            {
                // 設定LOG檔存放路徑。預設LOG存放於執行之應用程式下，如要變更LOG路徑，此函式需最先呼叫。
                int nCode = m_pSKCenter.SKCenterLib_SetLogPath(bstrPath);

                // 取得回傳訊息
                string msg = "【SKCenterLib_SetLogPath】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKCenterLib_GetLastLogInfo_Click(object sender, EventArgs e)
        {
            // 取得最後一筆LOG內容
            string msg = m_pSKCenter.SKCenterLib_GetLastLogInfo();
            // 取得回傳訊息
            msg = "【SKCenterLib_GetLastLogInfo】" + msg;
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonSKCenterLib_GenerateKeyCert_Click(object sender, EventArgs e)
        {
            // 僅適用AP及APH無憑證身份
            // 請在登入前，安裝附屬帳號ID有效憑證，再透過此函式產生雙因子登入憑證資訊
            // 雙因子登入必須透過憑證，使用群組的帳號登入，必須自行選擇群組內其一附屬帳號，以進行驗證憑證相關程序

            string bstrLogInID = textBoxUserID.Text; //(群組)登入帳號
            string bstrCustCertID = textBoxCustCertID.Text; //綁定在該群組之下且已安裝憑證之附屬帳號ID
            int nCode = m_pSKCenter.SKCenterLib_GenerateKeyCert(bstrLogInID, bstrCustCertID);

            // 取得回傳訊息
            string msg = "【SKCenterLib_GenerateKeyCert】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKOrderLib_LoadOSCommodity_Click(object sender, EventArgs e)
        {
            // 下載海期商品檔
            int nCode = m_pSKOrder.SKOrderLib_LoadOSCommodity();
            // 取得回傳訊息
            string msg = "【SKOrderLib_LoadOSCommodity】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKOrderLib_LoadOOCommodity_Click(object sender, EventArgs e)
        {
            // 下載海選商品檔
            int nCode = m_pSKOrder.SKOrderLib_LoadOOCommodity();
            // 取得回傳訊息
            string msg = "【SKOrderLib_LoadOOCommodity】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonOrder_Click(object sender, EventArgs e)// 切換到選擇市場
        {
            tabControlMain.SelectedTab = tabPageMarketOrder;
        }
        private void buttonUnlockOrder_Click(object sender, EventArgs e)
        {
            if (comboBoxSelectMarketType.SelectedItem == null) // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                int nMarketType = 0;
                string selectedValue = comboBoxSelectMarketType.Text; //0：TS (證券)1：TF(期貨)2：TO(選擇權)3：OS(複委託)4：OF(海外期貨)5：OO(海外選擇權)
                if (selectedValue == "TS(證券)") nMarketType = 0;
                else if (selectedValue == "TF(期貨)") nMarketType = 1;
                else if (selectedValue == "TO(選擇權)") nMarketType = 2;
                else if (selectedValue == "OS(複委託)") nMarketType = 3;
                else if (selectedValue == "OF(海外期貨)") nMarketType = 4;
                else if (selectedValue == "OO(海外選擇權)") nMarketType = 5;
                // 下單解鎖。下單函式上鎖後需經由此函式解鎖才可繼續下單。
                int nCode = m_pSKOrder.UnlockOrder(nMarketType);
                // 取得回傳訊息
                string msg = "【UnlockOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSetMaxQty_Click(object sender, EventArgs e)
        {
            if (comboBoxSelectMarketType.SelectedItem == null || textBoxMaxQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                int nMarketType = 0, nMaxQty;
                string selectedValue = comboBoxSelectMarketType.Text; //0：TS (證券)1：TF(期貨)2：TO(選擇權)3：OS(複委託)4：OF(海外期貨)5：OO(海外選擇權)
                if (selectedValue == "TS(證券)") nMarketType = 0;
                else if (selectedValue == "TF(期貨)") nMarketType = 1;
                else if (selectedValue == "TO(選擇權)") nMarketType = 2;
                else if (selectedValue == "OS(複委託)") nMarketType = 3;
                else if (selectedValue == "OF(海外期貨)") nMarketType = 4;
                else if (selectedValue == "OO(海外選擇權)") nMarketType = 5;
                nMaxQty = int.Parse(textBoxMaxQty.Text);
                // 設定每秒委託「量」限制。一秒內下單超過設定值時下該類型下單將被鎖定，需進行解鎖才可繼續下單
                int nCode = m_pSKOrder.SetMaxQty(nMarketType, nMaxQty);
                // 取得回傳訊息
                string msg = "【SetMaxQty】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSetMaxCount_Click(object sender, EventArgs e)
        {
            if (comboBoxSelectMarketType.SelectedItem == null || textBoxMaxCount.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                int nMarketType = 0, nMaxCount;
                string selectedValue = comboBoxSelectMarketType.Text; //0：TS (證券)1：TF(期貨)2：TO(選擇權)3：OS(複委託)4：OF(海外期貨)5：OO(海外選擇權)
                if (selectedValue == "TS(證券)") nMarketType = 0;
                else if (selectedValue == "TF(期貨)") nMarketType = 1;
                else if (selectedValue == "TO(選擇權)") nMarketType = 2;
                else if (selectedValue == "OS(複委託)") nMarketType = 3;
                else if (selectedValue == "OF(海外期貨)") nMarketType = 4;
                else if (selectedValue == "OO(海外選擇權)") nMarketType = 5;
                nMaxCount = int.Parse(textBoxMaxCount.Text);
                // 設定每秒委託「筆數」限制。一秒內下單超過設定值時下該類型下單將被鎖定，需進行解鎖才可繼續下單
                int nCode = m_pSKOrder.SetMaxCount(nMarketType, nMaxCount);
                // 取得回傳訊息
                string msg = "【SetMaxCount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKOrderLib_GetLoginType_Click(object sender, EventArgs e)
        {
            // 查詢登入帳號類型          
            int nCode = m_pSKOrder.SKOrderLib_GetLoginType(comboBoxUserID.Text);
            // 取得回傳訊息
            string msg;
            msg = (nCode == 0) ? "一般帳號" : "VIP帳號";
            msg = "【SKOrderLib_GetLoginType】" + msg;
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonSKOrderLib_GetSpeedyType_Click(object sender, EventArgs e)
        {
            // 查詢登入帳號下單線路       
            int nCode = m_pSKOrder.SKOrderLib_GetSpeedyType(comboBoxUserID.Text);
            // 取得回傳訊息
            string msg;
            msg = (nCode == 0) ? "一般線路" : "Speedy線路";
            msg = "【SKOrderLib_GetSpeedyType】" + msg;
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonSKOrderLib_TelnetTest_Click(object sender, EventArgs e)
        {
            // 用Telnet指令測試與API使用到的主機連線是否正常
            int nCode = m_pSKOrder.SKOrderLib_TelnetTest();
            // 取得回傳訊息
            string msg = "【SKOrderLib_TelnetTest】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonWithDraw_Click(object sender, EventArgs e)
        {
            if (comboBoxUserID.SelectedItem == null ||comboBoxWithDrawnTypeOut.SelectedItem == null || comboBoxWithDrawnTypeIn.SelectedItem == null || comboBoxWithDrawnCurrency.SelectedItem == null || textBoxWithDrawbstrFullAccountOut.Text == "" || textBoxWithDrawbstrFullAccountIn.Text == "" || textBoxWithDrawbstrDollars.Text == "" || textBoxWithDrawbstrPassword.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrLogInID = comboBoxUserID.Text; // 登入ID
                string bstrFullAccountOut = textBoxWithDrawbstrFullAccountOut.Text; // 轉出期貨帳號(分公司代碼＋帳號7碼)
                int nTypeOut = 0; // 轉出類別(0:國內；1:國外)
                string selectedValue = comboBoxWithDrawnTypeOut.Text;
                if (selectedValue == "國內") nTypeOut = 0;
                else if (selectedValue == "國外") nTypeOut = 1;
                string bstrFullAccountIn = textBoxWithDrawbstrFullAccountIn.Text; // 轉入期貨帳號(分公司代碼＋帳號7碼)
                int nTypeIn = 0; // 轉入類別(0:國內；1:國外)
                selectedValue = comboBoxWithDrawnTypeIn.Text;
                if (selectedValue == "國內") nTypeIn = 0;
                else if (selectedValue == "國外") nTypeIn = 1;
                int nCurrency = 0; // 幣別(0~9；詳閱－備註)
                selectedValue = comboBoxWithDrawnCurrency.Text;
                if (selectedValue == "澳幣") nCurrency = 0;
                else if (selectedValue == "歐元") nCurrency = 1;
                else if (selectedValue == "英鎊") nCurrency = 2;
                else if (selectedValue == "港幣") nCurrency = 3;
                else if (selectedValue == "日元") nCurrency = 4;
                else if (selectedValue == "台幣") nCurrency = 5;
                else if (selectedValue == "紐幣") nCurrency = 6;
                else if (selectedValue == "人民幣") nCurrency = 7;
                else if (selectedValue == "美元") nCurrency = 8;
                else if (selectedValue == "南非幣") nCurrency = 9;
                string bstrDollars = textBoxWithDrawbstrDollars.Text; // 金額
                string bstrPassword = textBoxWithDrawbstrPassword.Text; // 出入金密碼
                string bstrMessage = ""; // 非同步：如果回傳值為 0表示互轉成功，訊息內容則為送單之Thread ID，表示互轉已送出。回傳值非0表示互轉失敗，訊息內容為失敗原因。

                // 國內外出入金互轉
                int nCode = m_pSKOrder.WithDraw(bstrLogInID, bstrFullAccountOut, nTypeOut, bstrFullAccountIn, nTypeIn, nCurrency, bstrDollars, bstrPassword, out bstrMessage);
                // 取得回傳訊息
                string msg = "【WithDraw】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得bstrMessage
                msg = "【WithDraw】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
            }
        }
        private void buttonAssembleOptions_Click(object sender, EventArgs e)
        {
                string bstrLogInID = comboBoxUserID.Text; // 登入ID
                bool bAsyncOrder = true; // 可忽略，限同步委託
                FUTUREORDER pOrder = new FUTUREORDER(); //SKCOM元件中的 FUTUREORDER物件，將組合條件填入該物件後，再帶入此欄位中
                pOrder.bstrFullAccount = comboBoxAccount.Text;//期貨帳號，分公司代碼＋帳號7碼
                pOrder.bstrStockNo = textBoxbstrStockNo.Text;//委託選擇權代號1{組合部位}
                pOrder.bstrStockNo2 = textBoxbstrStockNo2.Text;//委託選擇權代號2{組合部位}
                pOrder.sBuySell = 0;//0:買進 1:賣出{組合部位}
                string selectedValue = comboBoxsBuySell.Text;
                if (selectedValue == "買進") pOrder.sBuySell = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell = 1;
                pOrder.sBuySell2 = 0;//0:買進 1:賣出{組合部位}
                selectedValue = comboBoxsBuySell2.Text;
                if (selectedValue == "買進") pOrder.sBuySell2 = 0;
                else if (selectedValue == "賣出") pOrder.sBuySell2 = 1;
                pOrder.nQty = int.Parse(textBoxnQty.Text);//交易口數{組合部位}	
                string bstrMessage = ""; // 同步委託：如果回傳值為 0表示委託成功，表示委託已送出。回傳值非0表示委託失敗，訊息內容為失敗原因。

                if (comboBoxAssembleOptions.Text == "組合")
                {
                    // 國內選擇權組合部位
                    int nCode = m_pSKOrder.AssembleOptions(bstrLogInID, bAsyncOrder, ref pOrder, out bstrMessage);
                    string msg = "【AssembleOptions】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                    // 取得bstrMessage
                    msg = "【AssembleOptions】" + bstrMessage;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
                else if (comboBoxAssembleOptions.Text == "複式單拆解")
                {
                    // 國內選擇權複式單拆解
                    int nCode = m_pSKOrder.DisassembleOptions(bstrLogInID, bAsyncOrder, ref pOrder, out bstrMessage);
                    // 取得回傳訊息
                    string msg = "【DisassembleOptions】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                    // 取得bstrMessage
                    msg = "【DisassembleOptions】" + bstrMessage;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
                else if (comboBoxAssembleOptions.Text == "雙邊了結")
                {
                    // 國內選擇權雙邊部位了結
                    int nCode = m_pSKOrder.CoverAllProduct(bstrLogInID, bAsyncOrder, ref pOrder, out bstrMessage);
                    // 取得回傳訊息
                    string msg = "【CoverAllProduct】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                    // 取得bstrMessage
                    msg = "【CoverAllProduct】" + bstrMessage;
                    richTextBoxMessage.AppendText(msg + "\n");
                }
        }
        private void buttonSendTFOffSet_Click(object sender, EventArgs e)
        {
            if (comboBoxUserID.SelectedItem == null || comboBoxAccount.SelectedItem == null ||comboBoxSendTFOffSetnCommodity.SelectedItem == null || comboBoxSendTFOffSetnBuySell.SelectedItem == null || textBoxSendTFOffSetbstrYearMonth.Text == "" || textBoxSendTFOffSetnQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrLogInID = comboBoxUserID.Text; // 登入ID
                string bstrAccount = comboBoxAccount.Text; // 委託帳號 ( IB＋帳號)
                int nCommodity = 0; // 指定商品0:大小台; 1:大小電; 2:大小金
                string selectedValue = comboBoxSendTFOffSetnCommodity.Text;
                if (selectedValue == "大小台") nCommodity = 0;
                else if (selectedValue == "大小電") nCommodity = 1;
                else if (selectedValue == "大小金") nCommodity = 2;
                string bstrYearMonth = textBoxSendTFOffSetbstrYearMonth.Text; // 大小台(電、金)年月(YYYYMM)
                int nBuySell = 0; // 大台(電、金)買賣別。0:多方(買) 1:空方(賣)
                selectedValue = comboBoxSendTFOffSetnBuySell.Text;
                if (selectedValue == "多方(買)") nBuySell = 0;
                else if (selectedValue == "空方(賣)") nBuySell = 1;
                int nQty = int.Parse(textBoxSendTFOffSetnQty.Text); // 互抵口數，以大台(電、金)口數為基本單位
                string bstrMessage = ""; // 同步：如果回傳值為 0表示委託成功，訊息內容則為互抵訊息。回傳值非0表示互抵失敗，訊息內容為失敗原因。非同步：參照4 - 2 - b OnAsyncOrder。

                // 指定大小台/電/金互抵
                int nCode = m_pSKOrder.SendTFOffset(bstrLogInID, bAsyncOrder, bstrAccount, nCommodity, bstrYearMonth, nBuySell, nQty, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendTFOffSet】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得bstrMessage
                msg = "【SendTFOffSet】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
            }
        }
        private void buttonGetOrderReport_Click(object sender, EventArgs e)
        {
            if (comboBoxGetOrderReport.SelectedItem == null || comboBoxUserID.SelectedItem == null || comboBoxAccount.SelectedItem == null) // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrResult = "";
                int nFormat = 0; // 查詢種類 1:全部 2:有效 3:可消 4:已消 5:已成 6:失敗 7:合併同價格 8:合併同商品 9:預約

                string SelectedValue = comboBoxGetOrderReport.Text;
                if (SelectedValue == "1:全部") nFormat = 1;
                else if (SelectedValue == "2:有效") nFormat = 2;
                else if (SelectedValue == "3:可消") nFormat = 3;
                else if (SelectedValue == "4:已消") nFormat = 4;
                else if (SelectedValue == "5:已成") nFormat = 5;
                else if (SelectedValue == "6:失敗") nFormat = 6;
                else if (SelectedValue == "7:合併同價格") nFormat = 7;
                else if (SelectedValue == "8:合併同商品") nFormat = 8;
                else if (SelectedValue == "9:預約") nFormat = 9;

                // 委託回報查詢
                bstrResult = m_pSKOrder.GetOrderReport(comboBoxUserID.Text, comboBoxAccount.Text, nFormat);
                richTextBoxGetOrderReport.AppendText(bstrResult + "\n");
            }
        }
        private void buttonGetFulfillReport_Click(object sender, EventArgs e)
        {
            if (comboBoxGetFulfillReport.SelectedItem == null || comboBoxUserID.SelectedItem == null || comboBoxAccount.SelectedItem == null) // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrResult = "";
                int nFormat = 0; // 查詢種類 1:完整 2:合併同書號 3:合併同價格 4:合併同商品 5:T+1成交回報

                string SelectedValue = comboBoxGetFulfillReport.Text;
                if (SelectedValue == "1:完整") nFormat = 1;
                else if (SelectedValue == "2:合併同書號") nFormat = 2;
                else if (SelectedValue == "3:合併同價格") nFormat = 3;
                else if (SelectedValue == "4:合併同商品") nFormat = 4;
                else if (SelectedValue == "5:T+1成交回報") nFormat = 5;

                // 成交回報查詢
                bstrResult = m_pSKOrder.GetFulfillReport(comboBoxUserID.Text, comboBoxAccount.Text, nFormat);
                richTextBoxGetFulfillReport.AppendText(bstrResult + "\n");
            }
        }
        private void buttonSKOrderLib_InitialProxyByID_Click(object sender, EventArgs e)
        {
            // 以使用者的id，初使化proxy 下單連線
            int nCode = m_pSKOrder.SKOrderLib_InitialProxyByID(comboBoxUserID.Text);
            // 取得回傳訊息
            string msg = "【SKOrderLib_InitialProxyByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonProxyDisconnectByID_Click(object sender, EventArgs e)
        {
            // 以使用者的id，主動斷線proxy server的連線
            int nCode = m_pSKOrder.ProxyDisconnectByID(comboBoxUserID.Text);
            // 取得回傳訊息
            string msg = "【ProxyDisconnectByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonProxyReconnectByID_Click(object sender, EventArgs e)
        {
            // 以使用者的id，重新連線之前主動斷線的proxy server連線，並做login
            int nCode = m_pSKOrder.ProxyReconnectByID(comboBoxUserID.Text);
            // 取得回傳訊息
            string msg = "【ProxyReconnectByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }       
        private void buttonTSReadOrderForm_Click(object sender, EventArgs e)
        {
            TSReadOrderForm tsReadOrderForm = new TSReadOrderForm();
            tsReadOrderForm.Show();
        }
        private void buttonTFReadOrderForm_Click(object sender, EventArgs e)
        {
            TFReadOrderForm tfReadOrderForm = new TFReadOrderForm();
            tfReadOrderForm.Show();
        }
        private void buttonOFReadOrderForm_Click(object sender, EventArgs e)
        {
            OFReadOrderForm ofReadOrderForm = new OFReadOrderForm();
            ofReadOrderForm.Show();
        }
        private void buttonTSSendOrderForm_Click(object sender, EventArgs e)
        {
            TSSendOrderForm tsSendOrderForm = new TSSendOrderForm();
            tsSendOrderForm.Show();
        }
        private void buttonTFSendOrderForm_Click(object sender, EventArgs e)
        {
            TFSendOrderForm tfSendOrderForm = new TFSendOrderForm();
            tfSendOrderForm.Show();
        }
        private void buttonOSSendOrderForm_Click(object sender, EventArgs e)
        {
            OSSendOrderForm osSendOrderForm = new OSSendOrderForm();
            osSendOrderForm.Show();
        }
        private void buttonOFSendOrderForm_Click(object sender, EventArgs e)
        {
            OFSendOrderForm ofSendOrderForm = new OFSendOrderForm();
            ofSendOrderForm.Show();
        }
        private void buttonTSSKProxySendOrderForm_Click(object sender, EventArgs e)
        {
            TSSKProxySendOrderForm tsSKProxySendOrderForm = new TSSKProxySendOrderForm();
            tsSKProxySendOrderForm.Show();
        }
        private void buttonTFSKProxySendOrderForm_Click(object sender, EventArgs e)
        {
            TFSKProxySendOrderForm tfSKProxySendOrderForm = new TFSKProxySendOrderForm();
            tfSKProxySendOrderForm.Show();
        }
        private void buttonOSSKProxySendOrderForm_Click(object sender, EventArgs e)
        {
            OSSKProxySendOrderForm osSKProxySendOrderForm = new OSSKProxySendOrderForm();
            osSKProxySendOrderForm.Show();
        }
        private void buttonOFSKProxySendOrderForm_Click(object sender, EventArgs e)
        {
            OFSKProxySendOrderForm ofSKProxySendOrderForm = new OFSKProxySendOrderForm();
            ofSKProxySendOrderForm.Show();
        }
        private void buttonTSSKProxyUpdateOrderForm_Click(object sender, EventArgs e)
        {
            TSSKProxyUpdateOrderForm tsSKProxyUpdateOrderForm = new TSSKProxyUpdateOrderForm();
            tsSKProxyUpdateOrderForm.Show();
        }
        private void buttonTFSKProxyUpdateOrderForm_Click(object sender, EventArgs e)
        {
            TFSKProxyUpdateOrderForm tfSKProxyUpdateOrderForm = new TFSKProxyUpdateOrderForm();
            tfSKProxyUpdateOrderForm.Show();
        }
        private void buttonOFSKProxyUpdateOrderForm_Click(object sender, EventArgs e)
        {
            OFSKProxyUpdateOrderForm ofSKProxyUpdateOrderForm = new OFSKProxyUpdateOrderForm();
            ofSKProxyUpdateOrderForm.Show();
        }
        private void buttonOSSKProxyUpdateOrderForm_Click(object sender, EventArgs e)
        {
            OSSKProxyUpdateOrderForm osSKProxyUpdateOrderForm = new OSSKProxyUpdateOrderForm();
            osSKProxyUpdateOrderForm.Show();
        }
        private void buttonOFStrategyOrderForm_Click(object sender, EventArgs e)
        {
            OFStrategyOrderForm ofStrategyOrderForm = new OFStrategyOrderForm();
            ofStrategyOrderForm.Show();
        }
        private void buttonTFStrategyOrderForm_Click(object sender, EventArgs e)
        {
            TFStrategyOrderForm tfStrategyOrderForm = new TFStrategyOrderForm();
            tfStrategyOrderForm.Show();
        }
        private void buttonTSStrategyOrderForm_Click(object sender, EventArgs e)
        {
            TSStrategyOrderForm tsStrategyOrderForm = new TSStrategyOrderForm();
            tsStrategyOrderForm.Show();
        }
        private void buttonTSTFUpdateOrderForm_Click(object sender, EventArgs e)
        {
            TSTFUpdateOrderForm tstfUpdateOrderForm = new TSTFUpdateOrderForm();
            tstfUpdateOrderForm.Show();
        }
        private void buttonOSUpdateOrderForm_Click(object sender, EventArgs e)
        {
            OSUpdateOrderForm osUpdateOrderForm = new OSUpdateOrderForm();
            osUpdateOrderForm.Show();
        }
        private void buttonOFUpdateOrderForm_Click(object sender, EventArgs e)
        {
            OFUpdateOrderForm ofUpdateOrderForm = new OFUpdateOrderForm();
            ofUpdateOrderForm.Show();
        }
        private void buttonQuoteForm_Click(object sender, EventArgs e)
        {
            QuoteForm quoteForm = new QuoteForm(comboBoxUserID.Text);
            quoteForm.Show();       
        }
        private void buttonReplyForm_Click(object sender, EventArgs e)
        {
            ReplyForm replyform = new ReplyForm();
            replyform.Show();
        }
        private void buttonSKOrderLib_LogUpload_Click(object sender, EventArgs e)
        {
            // LOG上傳
            int nCode = m_pSKOrder.SKOrderLib_LogUpload();
            // 取得回傳訊息
            string msg = "【SKOrderLib_LogUpload】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKCenterLib_Login_Click(object sender, EventArgs e)
        {
            string UserID = textBoxUserID.Text;
            string Password = textBoxPassword.Text;
            // 元件初始登入
            int nCode = m_pSKCenter.SKCenterLib_Login(UserID, Password);
            // 取得回傳訊息
            string msg = "【SKCenterLib_Login】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            if (nCode == 0) // 登入成功
            {
                // Component
                {
                    buttonSKCenterLib_SetLogPath.Enabled = false;
                }

                // 下單物件初始化
                {
                    // 下單物件初始化
                    nCode = m_pSKOrder.SKOrderLib_Initialize();
                    // 取得回傳訊息
                    msg = "【SKOrderLib_Initialize】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
                // 取回可交易的所有帳號
                {
                    // 取回可交易的所有帳號
                    nCode = m_pSKOrder.GetUserAccount();
                    // 取得回傳訊息
                    msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }
                // 讀取憑證
                {
                    // 讀取憑證
                    nCode = m_pSKOrder.ReadCertByID(textBoxUserID.Text);
                    // 取得回傳訊息
                    msg = "【ReadCertByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");
                }

                comboBoxUserID.Enabled = true;

            }
        }
        private void buttonSKOrderLib_PingandTracertTest_Click(object sender, EventArgs e)
        {
            // 測試API使用到的主機是否正常
            int nCode = m_pSKOrder.SKOrderLib_PingandTracertTest();
            // 取得回傳訊息
            string msg = "【SKOrderLib_PingandTracertTest】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonAddSGXAPIOrderSocket_Click(object sender, EventArgs e)
        {
            // 建立SGX API專線。注意，SGX API DMA專線需先向交易後台申請，方可使用。
            int nCode = m_pSKOrder.AddSGXAPIOrderSocket(comboBoxUserID.Text, comboBoxAccount.Text);
            // 取得回傳訊息
            string msg = "【AddSGXAPIOrderSocket】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void comboBoxUserID_Click(object sender, EventArgs e)
        {
            //獲得所有key
            if (allkeys != null) allkeys.Clear();

            allkeys = new List<string>(m_dictUserID.Keys);
            if (comboBoxUserID.DataSource != null) comboBoxUserID.DataSource = null;
            comboBoxUserID.DataSource = allkeys;
        }
        private void comboBoxUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 確保所選的索引有效
            if (comboBoxUserID.SelectedIndex != -1)
            {
                comboBoxAccount.DataSource = m_dictUserID[comboBoxUserID.Text];
            }
        }

        private void buttonSendTFOffsetNew_Click(object sender, EventArgs e)
        {
            if (comboBoxUserID.SelectedItem == null || comboBoxAccount.SelectedItem == null || comboBoxSendTFOffsetNewnCommodity.SelectedItem == null || comboBoxSendTFOffsetNewnBuySell.SelectedItem == null || textBoxSendTFOffsetNewbstrYearMonth.Text == "" || textBoxSendTFOffsetNewnQty.Text == "" || textBoxSendTFOffsetNewnQty_2.Text == "" || textBoxSendTFOffsetNewnQty_3.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                string bstrLogInID = comboBoxUserID.Text; // 登入ID
                string bstrAccount = comboBoxAccount.Text; // 委託帳號 ( IB＋帳號)
                int nCommodity = 0; // 指定商品0:大抵微; 1:小抵微; 2:大小抵微; 3:大抵小微; 4:小抵大微; 5:大抵小
                string selectedValue = comboBoxSendTFOffsetNewnCommodity.Text;
                if (selectedValue == "0:大抵微") nCommodity = 0;
                else if (selectedValue == "1:小抵微") nCommodity = 1;
                else if (selectedValue == "2:大小抵微") nCommodity = 2;
                else if (selectedValue == "3:大抵小微") nCommodity = 3;
                else if (selectedValue == "4:小抵大微") nCommodity = 4;
                else if (selectedValue == "5:大抵小") nCommodity = 5;
                string bstrYearMonth = textBoxSendTFOffsetNewbstrYearMonth.Text; // 年月(YYYYMM)
                int nBuySell = 0; // 買賣別。0:多方(買) 1:空方(賣)
                selectedValue = comboBoxSendTFOffsetNewnBuySell.Text;
                if (selectedValue == "多方(買)") nBuySell = 0;
                else if (selectedValue == "空方(賣)") nBuySell = 1;
                int nQty = int.Parse(textBoxSendTFOffsetNewnQty.Text); // 互抵口數，大台
                int nQty_2 = int.Parse(textBoxSendTFOffsetNewnQty_2.Text); // 互抵口數，小台
                int nQty_3 = int.Parse(textBoxSendTFOffsetNewnQty_3.Text); // 互抵口數，微台
                string bstrMessage = ""; // 同步：如果回傳值為 0表示委託成功，訊息內容則為互抵訊息。回傳值非0表示互抵失敗，訊息內容為失敗原因。非同步：參照4 - 2 - b OnAsyncOrder。

                // 指定大、小、微台互抵
                int nCode = m_pSKOrder.SendTFOffsetNew(bstrLogInID, bAsyncOrder, bstrAccount, nCommodity, bstrYearMonth, nBuySell, nQty, nQty_2, nQty_3, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendTFOffsetNew】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
                // 取得bstrMessage
                msg = "【SendTFOffsetNew】" + bstrMessage;
                richTextBoxMessage.AppendText(msg + "\n");
            }
        }
    }
}
