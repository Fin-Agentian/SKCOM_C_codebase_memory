using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using TimersTimer = System.Timers.Timer;
using System.Windows.Forms;

using SKCOMLib;

namespace SKCOMTester
{
    public partial class SKOrder : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        private bool m_bfirst = true;
        private int m_nCode;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        SKCOMLib.SKOrderLib m_pSKOrder = null;
        SKCOMLib.SKOrderLib m_pSKOrder2 = null;

        ViewDataGrid_FutureR pVDG_FR = new ViewDataGrid_FutureR();

        public SKOrderLib OrderObj
        {
            get { return m_pSKOrder; }
            set { m_pSKOrder = value; }
        }

        public SKOrderLib OrderObj2
        {
            get { return m_pSKOrder2; }
            set { m_pSKOrder2 = value; }
        }

        public string m_strLoginID = "", m_strLoginID2 = "";
        public string LoginID
        {
            get { return m_strLoginID; }
            set { m_strLoginID = value; }
        }
        public string LoginID2
        {
            get { return m_strLoginID2; }
            set { m_strLoginID2 = value; }
        }
        private bool m_bSGX = false;
        public bool SGXDMA
        {
            get { return m_bSGX; }
            set { m_bSGX = value; }
        }
        private static TimersTimer _TimerTimer = null;
        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public SKOrder()
        {
            InitializeComponent();
        }

        private void SKOrder_Load(object sender, EventArgs e)
        {
            overseaFutureOrderControl1.SGXDMA = m_bSGX;
        }

        #endregion

        #region API Event
        //----------------------------------------------------------------------
        // API Event
        //----------------------------------------------------------------------

        void m_OrderObj_OnAccount(string bstrLogInID, string bstrAccountData)
        {
            string[] strValues;
            string strAccount;

            strValues = bstrAccountData.Split(',');
            strAccount = bstrLogInID + " " + strValues[1] + strValues[3];

            if (strValues[0] == "TS")
            {
                if (!boxStockAccount.Items.Contains(strAccount))
                    boxStockAccount.Items.Add(strAccount);

                withDrawInOutControl1.UserAccountTS = strValues[1] + strValues[3];
                //boxExecutionAccount.Items.Add("證券 " + strAccount);
            }
            else if (strValues[0] == "TF")
            {
                if (!boxFutureAccount.Items.Contains(strAccount))
                    boxFutureAccount.Items.Add(strAccount);

                withDrawInOutControl1.UserAccountTF = strValues[1] + strValues[3];
                //boxExecutionAccount.Items.Add("期貨 " + strAccount);
            }
            else if (strValues[0] == "OF")
            {
                if (!boxOSFutureAccount.Items.Contains(strAccount))
                    boxOSFutureAccount.Items.Add(strAccount);

                withDrawInOutControl1.UserAccountOF = strValues[1] + strValues[3];
            }
            else if (strValues[0] == "OS")
            {
                if (!boxOSFutureAccount.Items.Contains(strAccount))
                    boxOSStockAccount.Items.Add(strAccount);

                withDrawInOutControl1.UserAccountOS = strValues[1] + strValues[3];
            }


            if (boxStockAccount.Items.Count > 0)
                boxStockAccount.SelectedIndex = 0;
            if (boxFutureAccount.Items.Count > 0)
                boxFutureAccount.SelectedIndex = 0;
            if (boxOSFutureAccount.Items.Count > 0)
                boxOSFutureAccount.SelectedIndex = 0;
            if (boxOSStockAccount.Items.Count > 0)
                boxOSStockAccount.SelectedIndex = 0;

        }

        void m_pSKOrder_OnAsyncOrder(int nThreaID, int nCode, string bstrMessage)
        {
            WriteMessage("[OnAsyncOrder]Thread ID:" + nThreaID.ToString() + " Code:" + nCode.ToString() + " Message:" + bstrMessage);
        }

        void m_pSKOrder_OnAsyncOrderOLID(int nThreaID, int nCode, string bstrMessage, string bstrOrderLinkedID)
        {
            WriteMessage("[OnAsyncOrderOLID]Thread ID:" + nThreaID.ToString() + " OLID:" + bstrOrderLinkedID + " Code:" + nCode.ToString() + " Message:" + bstrMessage);
        }

        void m_pSKOrder_OnPasswordUpdateToken(int nStatus, string bstrLoginID)
        {
            string strStatus = "";
            if (nStatus == 2031)
                strStatus = "SK_WARNING_VERIFYING_TOKEN_AUTO_UPDATING";
            else if (nStatus == 3038)
                strStatus = "SK_SUBJECT_TOKEN_AUTO_UPDATE_DONE";
            else if (nStatus == 3039)
                strStatus = "SK_SUBJECT_AUTO_UPDATE_TOKEN_FAIL";
            else if (nStatus == 3040)
                strStatus = "SK_SUBJECT_TOKEN_STILL_VALID";
            else if (nStatus == 1109)
                strStatus = "SK_ERROR_THERE_IS_NO_TOKEN";

            WriteMessage("[OnPasswordUpdateToken]:" + /*nStatus.ToString()*/strStatus + "," + "ID:" + bstrLoginID);
        }

        void m_pSKOrder_OnAsyncOrderGW(int nThreaID, int nCode, string bstrMessage)
        {
            WriteMessage("[OnAsyncOrderGW]Thread ID:" + nThreaID.ToString() + " Code:" + nCode.ToString() + " Message:" + bstrMessage);
        }

        void m_pSKOrder_OnRealBalanceReport(string bstrData)
        {
            WriteMessage("[OnRealBalanceReport]" + bstrData);
        }

        void m_pSKOrder_OnOpenInterest(string bstrData)
        {
            WriteMessage("[OnOpenInterest]" + bstrData);
        }

        void m_pSKOrder_OnOpenInterestJson(string bstrData)
        {
            WriteMessage("[OnOpenInterestJson]" + bstrData);

            if (bstrData.Contains("001,查無資料") || string.IsNullOrWhiteSpace(bstrData))
            {
                WriteMessage("[Info] 無資料，略過解析。");
                return;
            }

            try
            {
                string json = bstrData.Trim();

                // 2️ 確認是否以 [ 開頭 ] 結尾（確保是 JSON 陣列）
                if (!(json.StartsWith("[") && json.EndsWith("]")))
                {
                    WriteMessage("[Warn] 格式非 JSON 陣列，略過。");
                    return;
                }

                // 3️ 去掉外層中括號
                json = json.Substring(1, json.Length - 2);

                // 4️ 逐個擷取被雙引號包起來的字串
                List<string> items = new List<string>();
                int start = 0;
                while (true)
                {
                    int q1 = json.IndexOf('"', start);
                    if (q1 == -1) break;

                    int q2 = json.IndexOf('"', q1 + 1);
                    if (q2 == -1) break;

                    string item = json.Substring(q1 + 1, q2 - q1 - 1);
                    items.Add(item);
                    start = q2 + 1;
                }

                // 5️ 檢查是否有內容
                if (items.Count == 0)
                {
                    WriteMessage("[Info] 沒有可解析的項目。");
                    return;
                }

                // 6️ 逐筆處理
                for (int i = 0; i < items.Count; i++)
                {
                    string record = items[i];
                    WriteMessage($"[Parsed {i + 1}] {record}");
                }
            }
            catch (Exception ex)
            {
                WriteMessage("[Error] 解析失敗：" + ex.Message);
            }

        }

        void m_pSKOrder_OnOverseaFutureOpenInterest(string bstrData)
        {
            WriteMessage("[OnOverseaFutureOpenInterest]" + bstrData);
        }

        void m_pSKOrder_OnStopLossReport(string bstrData)
        {
            WriteMessage("[OnStopLossReport]" + bstrData);
        }

        void m_pSKOrder_OnOverseaFuture(string bstrData)
        {
            WriteMessage("[OnOverseaFuture]" + bstrData);
        }

        void m_pSKOrder_OnOverseaOption(string bstrData)
        {
            WriteMessage("[OnOverseaOption]" + bstrData);
        }

        void m_pSKOrder_OnFutureRights(string bstrData)
        {
            WriteMessage("[OnFutureRights]" + bstrData);
            //pVDG_FR.OnFutureRights = bstrData;
            if (pVDG_FR != null && pVDG_FR.Validate())
                pVDG_FR.GetOnFutureRights(bstrData);

        }

        void m_pSKOrder_OnRequestProfitReport(string bstrData)
        {
            WriteMessage("[OnRequestProfitReport]" + bstrData);
        }

        void m_pSKOrder_OnOverSeaFutureRight(string bstrData)
        {
            WriteMessage("[OnOverSeaFutureRight]" + bstrData);
        }

        void m_pSKOrder_OnMarginPurchaseAmountLimit(string bstrData)
        {
            WriteMessage("[OnMarginPurchaseAmountLimit]" + bstrData);
        }

        void m_pSKOrder_OnBalanceQueryReport(string bstrData)
        {
            WriteMessage("[OnBalanceQuery]" + bstrData);
        }
        void m_pSKOrder_OnTSStrategyReport(string bstrData)
        {
            WriteMessage("[OnTSStrategyReport]" + bstrData);
        }

        void m_pSKOrder_OnTSProfitLossGWReport(string bstrData)
        {
            WriteMessage("[OnTSProfitLossGWReport]" + bstrData);
        }
        void m_pSKOrder_OnOFOpenInterestGW(string bstrData)
        {
            WriteMessage("[OnOFOpenInterestGW]" + bstrData);
        }
        void m_pSKOrder_OnTelnetTest(string bstrData)
        {
            WriteMessage("[OnTelnetTest]" + bstrData);
        }
        void m_pSKOrder_OnProxyStatus(string strUserID, int nCode)
        {

            if (strUserID == m_strLoginID)
            {
                lblProxyLoginID1.Text = nCode.ToString();
                if (nCode == 5010)// if (nCode == 0)
                {

                    WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 已連線");
                }
                else if (nCode == 5002)//(nCode == 1)
                {

                    WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 連線失敗");
                }
                else if (nCode == 5003)//(nCode == 4)
                {

                    WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 已斷線");
                }
            }
            else if (strUserID == m_strLoginID2)
            {
                lblProxyLoginID2.Text = nCode.ToString();

                if (nCode == 5010)//(nCode == 0)
                {

                    WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 連線成功");
                }
                else if (nCode == 5011)//(nCode == 1)
                {

                    WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 連線失敗 ");
                }
                else if (nCode == 5003)//(nCode == 4)
                {
                    lblProxyLoginID2.ForeColor = Color.Red;
                    WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 已斷線");
                }
            }

            if (nCode == 5001)//(2)
            {
                WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 登入成功");
            }
            else if (nCode == 5002)//(3)
            {
                WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 登入失敗");
            }
            else if (nCode == 5004)//(5)
            {
                WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 即將斷線-每日斷線");
            }
            else if (nCode == 5005)//(6)
            {
                WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 即將斷線-server 轉切");
            }
            else if (nCode == 5016)//(nCode == 7)
            {
                WriteMessage("[OnProxyStatus]UserID=" + strUserID + " 此id已建立過連線，無法重複連線");
            }
        }

        void m_pSKOrder_OnProxyOrder(int nStampID, int nCode, string bstrMessage)
        {
            WriteMessage("[OnProxyOrder]StampID:" + nStampID.ToString() + " Code:" + nCode.ToString() + " Message:" + bstrMessage);
        }

        void m_pSKOrder_OnOFStrategyReport(string bstrData)
        {
            WriteMessage("[OnOFStrategyReport]" + bstrData);
        }

        void m_pSKOrder_OnOpenInterestGWStatus(int nQueryStatus, string bstrErrorMsg)
        {
            WriteMessage("[OnOpenInterestGWStatus]: QueryStatus:" + nQueryStatus.ToString() + " ErrorMsg:" + bstrErrorMsg);
        }

        void m_pSKOrder_OnOverseaFutureOpenInterestGWStatus(int nQueryStatus, string bstrErrorMsg)
        {
            WriteMessage("[OnOverseaFutureOpenInterestGWStatus]: QueryStatus:" + nQueryStatus.ToString() + " ErrorMsg:" + bstrErrorMsg);
        }

        void m_pSKOrder_OnFutureRightsStatus(int nQueryStatus, string bstrErrorMsg)
        {
            WriteMessage("[OnFutureRightsStatus]: QueryStatus:" + nQueryStatus.ToString() + " ErrorMsg:" + bstrErrorMsg);
        }

        void m_pSKOrder_OnOverseaFutureRightsStatus(int nQueryStatus, string bstrErrorMsg)
        {
            WriteMessage("[OnOverseaFutureRightsStatus]: QueryStatus:" + nQueryStatus.ToString() + " ErrorMsg:" + bstrErrorMsg);
        }
        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //if (m_strLoginID != IDs_Box.Text || m_strLoginID2 != IDs_Box.Text)
            IDs_Box.Items.Clear();
            if (m_strLoginID != "")
            {
                IDs_Box.Items.Add(m_strLoginID);
            }
            if (m_strLoginID2 != "")
            {
                IDs_Box.Items.Add(m_strLoginID2);
            }

            if (m_bfirst == true)
            {




                m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(m_OrderObj_OnAccount);
                m_pSKOrder.OnAsyncOrder += new _ISKOrderLibEvents_OnAsyncOrderEventHandler(m_pSKOrder_OnAsyncOrder);
                m_pSKOrder.OnAsyncOrderOLID += new _ISKOrderLibEvents_OnAsyncOrderOLIDEventHandler(m_pSKOrder_OnAsyncOrderOLID);
                m_pSKOrder.OnRealBalanceReport += new _ISKOrderLibEvents_OnRealBalanceReportEventHandler(m_pSKOrder_OnRealBalanceReport);
                m_pSKOrder.OnOpenInterest += new _ISKOrderLibEvents_OnOpenInterestEventHandler(m_pSKOrder_OnOpenInterest);
                m_pSKOrder.OnOverseaFutureOpenInterest += new _ISKOrderLibEvents_OnOverseaFutureOpenInterestEventHandler(m_pSKOrder_OnOverseaFutureOpenInterest);
                m_pSKOrder.OnStopLossReport += new _ISKOrderLibEvents_OnStopLossReportEventHandler(m_pSKOrder_OnStopLossReport);
                m_pSKOrder.OnOverseaFuture += new _ISKOrderLibEvents_OnOverseaFutureEventHandler(m_pSKOrder_OnOverseaFuture);
                m_pSKOrder.OnOverseaOption += new _ISKOrderLibEvents_OnOverseaOptionEventHandler(m_pSKOrder_OnOverseaOption);
                m_pSKOrder.OnFutureRights += new _ISKOrderLibEvents_OnFutureRightsEventHandler(m_pSKOrder_OnFutureRights);
                m_pSKOrder.OnRequestProfitReport += new _ISKOrderLibEvents_OnRequestProfitReportEventHandler(m_pSKOrder_OnRequestProfitReport);
                m_pSKOrder.OnOverSeaFutureRight += new _ISKOrderLibEvents_OnOverSeaFutureRightEventHandler(m_pSKOrder_OnOverSeaFutureRight);
                m_pSKOrder.OnMarginPurchaseAmountLimit += new _ISKOrderLibEvents_OnMarginPurchaseAmountLimitEventHandler(m_pSKOrder_OnMarginPurchaseAmountLimit);
                m_pSKOrder.OnBalanceQuery += new _ISKOrderLibEvents_OnBalanceQueryEventHandler(m_pSKOrder_OnBalanceQueryReport);
                m_pSKOrder.OnTSSmartStrategyReport += new _ISKOrderLibEvents_OnTSSmartStrategyReportEventHandler(m_pSKOrder_OnTSStrategyReport);
                m_pSKOrder.OnProfitLossGWReport += new _ISKOrderLibEvents_OnProfitLossGWReportEventHandler(m_pSKOrder_OnTSProfitLossGWReport);
                m_pSKOrder.OnOFOpenInterestGWReport += new _ISKOrderLibEvents_OnOFOpenInterestGWReportEventHandler(m_pSKOrder_OnOFOpenInterestGW);
                m_pSKOrder.OnTelnetTest += new _ISKOrderLibEvents_OnTelnetTestEventHandler(m_pSKOrder_OnTelnetTest);
                m_pSKOrder.OnPasswordUpdateToken += new _ISKOrderLibEvents_OnPasswordUpdateTokenEventHandler(m_pSKOrder_OnPasswordUpdateToken);
                m_pSKOrder.OnAsyncOrderGW += new _ISKOrderLibEvents_OnAsyncOrderGWEventHandler(m_pSKOrder_OnAsyncOrderGW);
                m_pSKOrder.OnProxyStatus += new _ISKOrderLibEvents_OnProxyStatusEventHandler(m_pSKOrder_OnProxyStatus);
                m_pSKOrder.OnProxyOrder += new _ISKOrderLibEvents_OnProxyOrderEventHandler(m_pSKOrder_OnProxyOrder);
                m_pSKOrder.OnOFSmartStrategyReport += new _ISKOrderLibEvents_OnOFSmartStrategyReportEventHandler(m_pSKOrder_OnOFStrategyReport);
                m_pSKOrder.OnOpenInterestGWStatus += new _ISKOrderLibEvents_OnOpenInterestGWStatusEventHandler(m_pSKOrder_OnOpenInterestGWStatus);
                m_pSKOrder.OnOverseaFutureOpenInterestGWStatus += new _ISKOrderLibEvents_OnOverseaFutureOpenInterestGWStatusEventHandler(m_pSKOrder_OnOverseaFutureOpenInterestGWStatus);
                m_pSKOrder.OnFutureRightsStatus += new _ISKOrderLibEvents_OnFutureRightsStatusEventHandler(m_pSKOrder_OnFutureRightsStatus);
                m_pSKOrder.OnOverseaFutureRightsStatus += new _ISKOrderLibEvents_OnOverseaFutureRightsStatusEventHandler(m_pSKOrder_OnOverseaFutureRightsStatus);
                m_pSKOrder.OnOpenInterestJson += new _ISKOrderLibEvents_OnOpenInterestJsonEventHandler(m_pSKOrder_OnOpenInterestJson);

                m_bfirst = false;
            }

            m_nCode = m_pSKOrder.SKOrderLib_Initialize();



            SendReturnMessage("Order", m_nCode, "SKOrderLib_Initialize");

        }

        private void btnReadCert_Click(object sender, EventArgs e)
        {
            if (IDs_Box.Items.Count == 0)
            {
                m_nCode = m_pSKOrder.ReadCertByID(m_strLoginID);
                m_nCode = m_pSKOrder.ReadCertByID(m_strLoginID2);
            }
            else
            {
                m_nCode = m_pSKOrder.ReadCertByID(IDs_Box.Text);
            }
            SendReturnMessage("Order", m_nCode, "ReadCertByID:" + IDs_Box.Text);
        }

        private void btnGetAccount_Click(object sender, EventArgs e)
        {
            boxStockAccount.Items.Clear();
            boxFutureAccount.Items.Clear();
            boxOSFutureAccount.Items.Clear();
            boxOSStockAccount.Items.Clear();

            m_nCode = m_pSKOrder.GetUserAccount();

            SendReturnMessage("Order", m_nCode, "GetUserAccount");
        }

        private void btnSetMaxQty_Click(object sender, EventArgs e)
        {
            int nQty = 0;
            int.TryParse(txtMaxQty.Text.Trim(), out nQty);

            m_nCode = m_pSKOrder.SetMaxQty(boxOrderLimit.SelectedIndex, nQty);

            SendReturnMessage("Order", m_nCode, "SetMaxQty");
        }

        private void btnSetMaxCount_Click(object sender, EventArgs e)
        {
            int nCount = 0;
            int.TryParse(txtMaxCount.Text.Trim(), out nCount);

            m_nCode = m_pSKOrder.SetMaxCount(boxOrderLimit.SelectedIndex, nCount);
            SendReturnMessage("Order", m_nCode, "SetMaxCount");
        }

        private void btnOrderUnlock_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.UnlockOrder(boxOrderLimit.SelectedIndex);
            SendReturnMessage("Order", m_nCode, "UnlockOrder");
        }

        private void btnDownloadOS_Click(object sender, EventArgs e)
        {

            m_nCode = m_pSKOrder.SKOrderLib_LoadOSCommodity();
            SendReturnMessage("Order", m_nCode, "SKOrderLib_LoadOSCommodity");
            overseaFutureOrderControl1.SGXDMA = m_bSGX;
        }

        private void btnDownloadOO_Click(object sender, EventArgs e)
        {

            m_nCode = m_pSKOrder.SKOrderLib_LoadOOCommodity();
            SendReturnMessage("Order", m_nCode, "SKOrderLib_LoadOOCommodity");
        }

        /////////////////////////////////////////////////////////////////////////////////
        private void boxStockAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strInfo = boxStockAccount.Text;

            string[] strValues;
            strValues = strInfo.Split(' ');

            stockOrderControl1.UserID = strValues[0];
            stockOrderControl1.UserAccount = strValues[1];
            stockOrderControl1.UserBrokerID = strValues[1].Substring(3);
            stockStrategyOrderControl1.UserID = strValues[0];
            stockStrategyOrderControl1.UserAccount = strValues[1];
            ProxyControl1.UserID = strValues[0];
            ProxyControl1.UserStockAccount = strValues[1];

            withDrawInOutControl1.UserID = strValues[0];
            withDrawInOutControl1.UserAccountTS = strValues[1];
        }

        private void boxOSStockAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strInfo = boxOSStockAccount.Text;

            string[] strValues;
            strValues = strInfo.Split(' ');

            foreignStockOrderControl1.UserID = strValues[0];
            foreignStockOrderControl1.UserAccount = strValues[1];
            ProxyControl1.UserID = strValues[0];
            ProxyControl1.UserForeignStockAccount = strValues[1];

            withDrawInOutControl1.UserID = strValues[0];
            withDrawInOutControl1.UserAccountOS = strValues[1];

        }

        private void boxOSFutureAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strInfo = boxOSFutureAccount.Text;

            string[] strValues;
            strValues = strInfo.Split(' ');

            overseaFutureOrderControl1.UserID = strValues[0];
            overseaFutureOrderControl1.UserAccount = strValues[1];

            overseaOptionOrderControl1.UserID = strValues[0];
            overseaOptionOrderControl1.UserAccount = strValues[1];

            withDrawInOutControl1.UserID = strValues[0];
            //withDrawInOut1.UserAccountOF = strValues[1];
            overseaFutureOrderControl1.SGXDMA = m_bSGX;
            ProxyControl1.UserID = strValues[0];
            ProxyControl1.UserOSAccount = strValues[1];

            OverseaStrategy1.UserID = strValues[0];  //[20231220]
            OverseaStrategy1.UserAccount = strValues[1];
        }

        private void boxFutureAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strInfo = boxFutureAccount.Text;

            string[] strValues;
            strValues = strInfo.Split(' ');

            futureOrderControl1.UserID = strValues[0];
            futureOrderControl1.UserAccount = strValues[1];

            optionOrderControl1.UserID = strValues[0];
            optionOrderControl1.UserAccount = strValues[1];

            futureStopLossControl1.UserID = strValues[0];
            futureStopLossControl1.UserAccount = strValues[1];

            tftomit1.UserID = strValues[0];
            tftomit1.UserAccount = strValues[1];

            withDrawInOutControl1.UserID = strValues[0];
            //withDrawInOut1.UserAccountTF = strValues[1];
            ProxyControl1.UserID = strValues[0];
            ProxyControl1.UserFutureAccount = strValues[1];


        }


        private void stockOrderControl1_OnOrderSignal(string strLogInID, bool bAsyncOrder, STOCKORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("證券委託 :" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendStockOrder");
        }

        private void stockOrderControl1_OnOddOrderSignal(string strLogInID, bool bAsyncOrder, STOCKORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockOddLotOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("證券盤中零股委託 :" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendStockOddOrder");
        }

        private void stockOrderControl1_OnGWOrderSignal(string strLogInID, bool bAsyncOrder, STOCKORDER pStock)
        {
            string strMessage = "";
            //m_nCode = m_pSKOrder.SendStockOrderGW(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("證券委託GW :" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendStockOrderGW");
        }

        private void stockOrderControl1_OnAlterTSOrderGWSignal(string strLogInID, bool bAsyncOrder, STOCKORDER pStock)
        {

            string strMessage = "";
            //m_nCode = m_pSKOrder.AlterStockOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("異動證券委託GW :" + strMessage);
            SendReturnMessage("Order", m_nCode, "AlterStockOrder");
        }

        private void futureOrderControl1_OnFutureOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("期貨委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureOrder");
        }

        private void futureOrderControl1_OnFutureOrderCLRSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureOrderCLR(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("期貨委託含倉位盤別：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureOrderCLR");
        }

        private void futureOrderControl1_OnFutureOrderGWSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            //m_nCode = m_pSKOrder.SendFutureOrderGW(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("期貨委託GW：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureOrderGW");
        }

        private void futureOrderControl1_OnAlterFutureOrderGWSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            //m_nCode = m_pSKOrder.AlterTFTOOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("期貨委託異動GW：" + strMessage);
            SendReturnMessage("Order", m_nCode, "AlterFutureOrderGW");
        }

        private void optionOrderControl1_OnOptionOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOptionOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("選擇權委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOptionOrder");
        }

        private void optionOrderControl1_OnOptionOrderGWSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            //m_nCode = m_pSKOrder.SendOptionOrderGW(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("選擇權委託GW：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOptionOrderGW");
        }

        private void optionOrderControl1_OnDuplexOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendDuplexOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("選擇權複式單委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendDuplexOrder");
        }

        private void optionOrderControl1_OnAssembleOptionSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pFutureOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.AssembleOptions(strLogInID, bAsyncOrder, pFutureOrder, out strMessage);

            WriteMessage("選擇權組合單委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendAssemleOption");
        }

        private void optionOrderControl1_OnTwoOrderToDisassemblySignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pFutureOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.DisassembleOptions(strLogInID, bAsyncOrder, pFutureOrder, out strMessage);

            WriteMessage("複式單拆解：" + strMessage);
            SendReturnMessage("Order", m_nCode, "TwoOrderToDisassembly");
        }

        private void optionOrderControl1_OnCoverAllProductSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pFutureOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CoverAllProduct(strLogInID, bAsyncOrder, pFutureOrder, out strMessage);

            WriteMessage("雙邊部位了結：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CoverAllProduct");
        }

        private void overseaFutureOrderControl1_OnOverseaFutureOrderSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            overseaFutureOrderControl1.SGXDMA = m_bSGX;

            m_nCode = m_pSKOrder.SendOverseaFutureOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            if (m_bSGX == true)
                WriteMessage("SGX DMA海期委託：" + strMessage);
            else
                WriteMessage("海期委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureOrder");
        }
        private void overseaFutureOrderControl1_OnOverseaFutureOrderOLIDSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock, string strOrderLinkedID)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureOrderOLID(strLogInID, bAsyncOrder, pStock, strOrderLinkedID, out strMessage);

            if (m_bSGX == true)
                WriteMessage("SGX DMA海期委託OLID：" + strMessage);
            else
                WriteMessage("海期委託by OrderLinkedID：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureOrderOLID");
        }

        private void overseaFutureOrderControl1_OnOverseaOptionOrderOLIDSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock, string strOrderLinkedID)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaOptionOrderOLID(strLogInID, bAsyncOrder, pStock, strOrderLinkedID, out strMessage);

            WriteMessage("海選委託by OrderLinkedID：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaOptionOrderOLID");
        }

        private void overseaFutureOrderControl1_OnOverseaFutureOrderSpreadSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureSpreadOrder(strLogInID, bAsyncOrder, pStock, out strMessage);
            if (m_bSGX == true)
                WriteMessage("SGX DMA海期價差委託：" + strMessage);
            else
                WriteMessage("海期價差委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureSpreadOrder");
        }

        private void overseaFutureOrderControl1_OnOverseaFutureOrderSpread2Signal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureSpreadOrder2(strLogInID, bAsyncOrder, pStock, out strMessage);
            if (m_bSGX == true)
                WriteMessage("SGX DMA海期價差委託2：" + strMessage);
            else
                WriteMessage("海期價差委託2：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureSpreadOrder2");
        }

        private void overseaFutureOrderControl1_OnOverseaFutureOrderSpreadOLIDSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock, string strOrderLinkedID)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureSpreadOrderOLID(strLogInID, bAsyncOrder, pStock, strOrderLinkedID, out strMessage);
            if (m_bSGX == true)
                WriteMessage("SGX DMA海期價差委託OLID：" + strMessage);
            else
                WriteMessage("海期價差委託by OrderLinkedID：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureSpreadOrderOLID");
        }

        private void overseaFutureOrderControl1_OnOverseaFutureOrder2OLIDSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock, string strOrderLinkedID)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureSpreadOrder2OLID(strLogInID, bAsyncOrder, pStock, strOrderLinkedID, out strMessage);
            if (m_bSGX == true)
                WriteMessage("SGX DMA海期價差委託OLID2：" + strMessage);
            else
                WriteMessage("海期價差委託by OrderLinkedID2：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureSpreadOrderOLID2");
        }

        private void overseaFutureOrderControl1_OnCorrectOrderByBookSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDERFORGW pOrder, string strOrderLinkedID)
        {
            string strMessage = "";
            if (strOrderLinkedID == "")
            {
                m_nCode = m_pSKOrder.OverSeaCorrectPriceByBookNo(strLogInID, bAsyncOrder, pOrder, out strMessage);
                WriteMessage("海期依書號改價：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaCorrectPriceOrderByBookNo");
            }
            else
            {
                m_nCode = m_pSKOrder.OverSeaCorrectPriceByBookNoOLID(strLogInID, bAsyncOrder, pOrder, strOrderLinkedID, out strMessage);
                WriteMessage("海期依書號改價OLID：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaCorrectPriceByBookNoOLID");
            }
        }

        private void overseaOptionOrderControl1_OnOverseaOptionOrderSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaOptionOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("海選委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaOptionOrder");
        }
        private void overseaFutureOrderControl1_OnOverseaFutureOrderGWSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDERFORGW pStock, string strOLID)
        {
            string strMessage = "";
            overseaFutureOrderControl1.SGXDMA = m_bSGX;

            // m_nCode = m_pSKOrder.OverSeaFutureOrderGW(strLogInID, bAsyncOrder, pStock, strOLID,out strMessage);

            if (m_bSGX == true)
                WriteMessage("SGX DMA海期委託：" + strMessage);
            else
                WriteMessage("海期委託GW：" + strMessage);
            SendReturnMessage("Order", m_nCode, "OverseaFutureOrderGW");
        }
        private void overseaFutureOrderControl1_OnAlterOverseaFutureOrderGWSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDERFORGW pStock)
        {
            string strMessage = "";


            //m_nCode = m_pSKOrder.AlterOverSeaFutureOrder(strLogInID, bAsyncOrder, pStock, out strMessage);


            WriteMessage("異動海期委託GW：" + strMessage);
            SendReturnMessage("Order", m_nCode, "AlterOverseaFutureOrderGW");
        }
        private void futureStopLossControl1_OnFutureStopLossOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureStopLossOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨停損委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureStopLossOrder");
        }

        private void futureStopLossControl1_OnFutureStopLossOrderV1Signal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureSTPOrderV1(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨停損委託V1：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureSTPOrderV1");
        }

        private void futureStopLossControl1_OnMovingStopLossOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendMovingStopLossOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨移動停損委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendMovingStopLossOrder");
        }

        private void futureStopLossControl1_OnMovingStopLossOrderV1Signal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureMSTOrderV1(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨移動停損委託V1：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureMSTOrderV1");
        }

        private void futureStopLossControl1_OnCancelStrategyTFOrderSignal(CANCELSTRATEGYORDER pOrder, bool bAsyncOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelTFStrategyOrderV1(pOrder, bAsyncOrder, out strMessage);

            WriteMessage("期貨智慧單刪單V1：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelTFStrategyOrderV1");
        }

        private void futureStopLossControl1_OnOptionStopLossOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOptionStopLossOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("選擇權停損委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOptionStopLossOrder");
        }

        private void futureStopLossControl1_OnFutureOCOOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREOCOORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureOCOOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨OCO委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureOCOOrder");
        }

        private void futureStopLossControl1_OnFutureOCOOrderV1Signal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureOCOOrderV1(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨OCO委託V1：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureOCOOrderV1");
        }

        private void ForeignOrderControl1_OnCancelForeignStockOrderSignal(string strLogInID, bool bAsyncOrder, FOREIGNORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelForeignStockOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("複委託-刪單(序號及書號)：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelForeignStockOrder");
        }

        private void ForeignOrderControl1_OnForeignStockOrderSignal(string strLogInID, bool bAsyncOrder, FOREIGNORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendForeignStockOrder(strLogInID, bAsyncOrder, pStock, out strMessage);

            WriteMessage("複委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "ForeignStockOrder");
        }

        private void stockOrderControl1_OnDecreaseOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.DecreaseOrderBySeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, nDecreaseQty, out strMessage);

            WriteMessage("SeqNo改量：" + strMessage);
            SendReturnMessage("Order", m_nCode, "DecreaseOrderBySeqNo");
        }

        private void stockOrderControl1_OnDecreaseOrderBookSignal(string strLogInID, bool bAsyncOrder, string strAccount, int nMarketType, string strBookNo, int nDecreaseQty)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.DecreaseOrderByBookNo(strLogInID, bAsyncOrder, strAccount, nMarketType, strBookNo, nDecreaseQty, out strMessage);

            WriteMessage("BookNo改量：" + strMessage);
            SendReturnMessage("Order", m_nCode, "DecreaseOrderByBookNo");
        }


        private void stockOrderControl1_OnCorrectPriceBySeqNo(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strPrice, int nTradeType)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CorrectPriceBySeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, strPrice, nTradeType, out strMessage);

            WriteMessage("證期權依序號改價：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CorrectPriceBySeqNo");
        }
        private void stockOrderControl1_OnCorrectPriceByBookNo(string strLogInID, bool bAsyncOrder, string strAccount, string strSymbol, string strBookNo, string strPrice, int nTradeType)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CorrectPriceByBookNo(strLogInID, bAsyncOrder, strAccount, strSymbol, strBookNo, strPrice, nTradeType, out strMessage);

            WriteMessage("證期權依書號改價：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CorrectPriceByBookNo");
        }


        private void overseaFutureOrderControl1_OnDecreaseOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty, string strOrderLinkedID)
        {
            string strMessage = "";
            if (strOrderLinkedID == null)
            {
                m_nCode = m_pSKOrder.OverSeaDecreaseOrderBySeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, nDecreaseQty, out strMessage);
                WriteMessage("海期改量：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaDecreaseOrderBySeqNo");
            }
            else
            {
                m_nCode = m_pSKOrder.OverSeaDecreaseOrderBySeqNoOLID(strLogInID, bAsyncOrder, strAccount, strSeqNo, nDecreaseQty, strOrderLinkedID, out strMessage);
                WriteMessage("海期改量OLID：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaDecreaseOrderBySeqNoOLID");
            }
        }

        private void overseaFutureOrderControl1_OnCorrectPriceOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strCorrectPrice)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.OverSeaCorrectPriceBySGXAPISeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, strCorrectPrice, out strMessage);

            WriteMessage("海期改價(SGXAPI)：" + strMessage);
            SendReturnMessage("Order", m_nCode, "OverSeaCorrectPriceBySGXAPISeqNo");
        }

        private void stockOrderControl1_OnCancelOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelOrderBySeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, out strMessage);

            WriteMessage("刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOrderBySeqNo");
        }

        private void stockOrderControl1_OnCancelOrderByBookSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelOrderByBookNo(strLogInID, bAsyncOrder, strAccount, strBookNo, out strMessage);

            WriteMessage("刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOrderByBookNo");
        }

        private void stockOrderControl1_OnCancelOrderByStockSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelOrderByStockNo(strLogInID, bAsyncOrder, strAccount, strStockNo, out strMessage);

            WriteMessage("刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOrderByStockNo");
        }

        private void stockOrderControl1_OnCancelOrderByStockNoAdvanceSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo, int nBuySell, string bstrPrice)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelOrderByStockNoAdvance(strLogInID, bAsyncOrder, strAccount, strStockNo, nBuySell, bstrPrice, out strMessage);

            WriteMessage("刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOrderByStockNoAdvance");
        }

        private void futureStopLossControl1_OnCancelFutureStopLossOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelFutureStopLoss(strLogInID, bAsyncOrder, strAccount, strBookNo, strSymbol, out strMessage);

            WriteMessage("停損刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelFutureStopLoss");
        }

        private void futureStopLossControl1_OnCancelMovingStopLossOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelMovingStopLoss(strLogInID, bAsyncOrder, strAccount, strBookNo, strSymbol, out strMessage);

            WriteMessage("移動停損刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelMovingStopLoss");
        }

        private void futureStopLossControl1_OnCancelOptionStopLossOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelOptionStopLoss(strLogInID, bAsyncOrder, strAccount, strBookNo, strSymbol, out strMessage);

            WriteMessage("選擇權停損刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOptionStopLoss");
        }

        private void futureStopLossControl1_OnCancelFutureOCOOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelFutureOCO(strLogInID, bAsyncOrder, strAccount, strBookNo, strSymbol, out strMessage);

            WriteMessage("OCO刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelFutureOCO");
        }

        private void futureStopLossControl1_OnCancelFutureMITOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelFutureMIT(strLogInID, bAsyncOrder, strAccount, strBookNo, strSymbol, out strMessage);

            WriteMessage("FutureMIT刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelFutureMIT");
        }

        private void futureStopLossControl1_OnCancelOptionMITOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelOptionMIT(strLogInID, bAsyncOrder, strAccount, strBookNo, strSymbol, out strMessage);

            WriteMessage("OptionMIT刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOptionMIT");
        }

        private void futureOrderControl1_OnCorrectPriceBySeqNo(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strPrice, int nTradeType)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CorrectPriceBySeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, strPrice, nTradeType, out strMessage);

            WriteMessage("期權改價：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CorrectPriceBySeqNo");
        }
        private void futureOrderControl1_OnCorrectPriceByBookNo(string strLogInID, bool bAsyncOrder, string strAccount, string strSymbol, string strBookNo, string strPrice, int nTradeType)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CorrectPriceByBookNo(strLogInID, bAsyncOrder, strAccount, strSymbol, strBookNo, strPrice, nTradeType, out strMessage);

            WriteMessage("期權依書號改價：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CorrectPriceByBookNo");
        }

        private void overseaFutureOrderControl1_OnCancelOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strOrderLinkedID)
        {
            string strMessage = "";

            if (strOrderLinkedID == null)
            {
                m_nCode = m_pSKOrder.OverSeaCancelOrderBySeqNo(strLogInID, bAsyncOrder, strAccount, strSeqNo, out strMessage);
                WriteMessage("海期刪單：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaCancelOrderBySeqNo");
            }
            else
            {
                m_nCode = m_pSKOrder.OverSeaCancelOrderBySeqNoOLID(strLogInID, bAsyncOrder, strAccount, strSeqNo, strOrderLinkedID, out strMessage);
                WriteMessage("海期刪單OLID：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaCancelOrderBySeqNoOLID");
            }
        }

        private void overseaFutureOrderControl1_OnCancelOrderByBookSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.OverSeaCancelOrderByBookNo(strLogInID, bAsyncOrder, strAccount, strBookNo, out strMessage);

            WriteMessage("海期依書號刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "OverSeaCancelOrderByBookNo");
        }

        private void overseaFutureOrderControl1_OnCorrectSpreadOrderByBookSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDERFORGW pOrder, string strOrderLinkedID)
        {
            string strMessage = "";
            if (strOrderLinkedID == "")
            {
                m_nCode = m_pSKOrder.OverSeaCorrectPriceSpreadByBookNo(strLogInID, bAsyncOrder, pOrder, out strMessage);
                WriteMessage("海期價差依書號改價：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaCorrectPriceSpreadOrderByBookNo");
            }
            else
            {
                m_nCode = m_pSKOrder.OverSeaCorrectPriceSpreadByBookNoOLID(strLogInID, bAsyncOrder, pOrder, strOrderLinkedID, out strMessage);
                WriteMessage("海期價差依書號改價OLID：" + strMessage);
                SendReturnMessage("Order", m_nCode, "OverSeaCorrectPriceSpreadByBookNoOLID");
            }

        }

        private void overseaFutureOrderControl1_OnCorrectOptionOrderByBookSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDERFORGW pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.OverSeaOptionCorrectPriceByBookNo(strLogInID, bAsyncOrder, pOrder, out strMessage);
            WriteMessage("海選依書號改價：" + strMessage);
            SendReturnMessage("Order", m_nCode, "OverSeaOptionCorrectPriceOrderByBookNo");

        }

        private void stockOrderControl1_OnRealBalanceSignal(string strLogInID, string strAccount)
        {
            m_nCode = m_pSKOrder.GetRealBalanceReport(strLogInID, strAccount);
            SendReturnMessage("Order", m_nCode, "GetRealBalanceReport");
        }

        private void stockOrderControl1_OnRequestProfitReportSignal(string strLogInID, string strAccount)
        {
            m_nCode = m_pSKOrder.GetRequestProfitReport(strLogInID, strAccount);
            SendReturnMessage("Order", m_nCode, "GetRequestProfitReport");
        }

        private void stockOrderControl1_OnRequestAmountLimitSignal(string strLogInID, string strAccount, string strStockNo)
        {
            m_nCode = m_pSKOrder.GetMarginPurchaseAmountLimit(strLogInID, strAccount, strStockNo);
            SendReturnMessage("Order", m_nCode, "GetMarginPurchaseAmountLimit");
        }

        private void stockOrderControl1_OnRequestBalanceQuerySignal(string strLogInID, string strAccount, string strStockNo)
        {
            m_nCode = m_pSKOrder.GetBalanceQuery(strLogInID, strAccount, strStockNo);
            SendReturnMessage("Order", m_nCode, "GetBalanceQuery");
        }

        private void stockOrderControl1_OnProfitGWReportSignal(string strLogInID, TSPROFITLOSSGWQUERY pGWQuery)
        {
            m_nCode = m_pSKOrder.GetProfitLossGWReport(strLogInID, pGWQuery);
            SendReturnMessage("Order", m_nCode, "GetProfitLossGWReport");
        }

        private void futureOrderControl1_OnOpenInterestSignal(string strLogInID, string strAccount, int nFormat)
        {
            m_nCode = m_pSKOrder.GetOpenInterestGW(strLogInID, strAccount, nFormat);
            SendReturnMessage("Order", m_nCode, "GetOpenInterestGW");
        }

        private void futureOrderControl1_OnOpenInterestOldSignal(string strLogInID, string strAccount)
        {
            m_nCode = m_pSKOrder.GetOpenInterest(strLogInID, strAccount);
            SendReturnMessage("Order", m_nCode, "GetOpenInterest");
        }

        private void futureOrderControl1_OnOpenInterestWithFormatSignal(string strLogInID, string strAccount, int nFormat)
        {
            m_nCode = m_pSKOrder.GetOpenInterestWithFormat(strLogInID, strAccount, nFormat);
            SendReturnMessage("Order", m_nCode, "GetOpenInterestWithFormat");
        }

        private void overseaFutureOrderControl1_OnOpenInterestSignal(string strLogInID, string strAccount)
        {
            m_nCode = m_pSKOrder.GetOverseaFutureOpenInterest(strLogInID, strAccount);
            SendReturnMessage("Order", m_nCode, "GetOverseaFutureOpenInterest");
        }
        private void futureStopLossControl1_OnStopLossReportSignal(string strLogInID, string strAccount, int nReportStatus, string strKind, string strDate)
        {
            m_nCode = m_pSKOrder.GetStopLossReport(strLogInID, strAccount, nReportStatus, strKind, strDate);
            SendReturnMessage("Order", m_nCode, "GetStopLossReport");
        }

        private void overseaFutureOrderControl1_OnOverseaFuturesSignal()
        {
            m_nCode = m_pSKOrder.GetOverseaFutures();
            SendReturnMessage("Order", m_nCode, "GetOverseaFutures");
        }

        private void overseaFutureOrderControl1_OnOIGWSignal(string strLogInID, string strAccount, int nFormat)
        {
            m_nCode = m_pSKOrder.GetOverseaFutureOpenInterestGW(strLogInID, strAccount, nFormat);
            SendReturnMessage("Order", m_nCode, "GetOFOpenInterestGW");
        }

        private void overseaOptionOrderControl1_OnOnOverseaOptionSignal()
        {
            m_nCode = m_pSKOrder.GetOverseaOptions();
            SendReturnMessage("Order", m_nCode, "GetOverseaOptions");
        }

        private void overseaOptionOrderControl1_OnOverseaOptionOrderGWSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDERFORGW pStock)
        {
            string strMessage = "";

            string strOLID = "";
            //m_nCode = m_pSKOrder.OverSeaFutureOrderGW(strLogInID, bAsyncOrder, pStock, strOLID, out strMessage);

            WriteMessage("海選委託GW：" + strMessage);
            SendReturnMessage("Order", m_nCode, "OverseaOptionOrderGW");
        }

        private void overseaFutureOrderControl1_OnOverSeaFutureRightSignal(string strLogInID, string strAccount)
        {
            m_nCode = m_pSKOrder.GetRequestOverSeaFutureRight(strLogInID, strAccount);
            SendReturnMessage("Order", m_nCode, "GetRequestOverSeaFutureRight");
        }


        private void futureOrderControl1_OnFutureRightsSignal(string strLogInID, string strAccount, int nCoinType)
        {
            m_nCode = m_pSKOrder.GetFutureRights(strLogInID, strAccount, (short)nCoinType);
            SendReturnMessage("Order", m_nCode, "GetFutureRights");
            if (pVDG_FR != null)
            {
                pVDG_FR.Activate();
                pVDG_FR.Visible = true;
            }
            else
            {
                ViewDataGrid_FutureR pVDG_FR = new ViewDataGrid_FutureR();
                pVDG_FR.Activate();
                pVDG_FR.Visible = true;
            }
        }

        private void TFTOMIT_OnFutureMITOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureMITOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨MIT委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureMITOrder");
        }

        private void TFTOMIT_OnFutureMITOrderV1Signal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureMITOrderV1(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨MIT委託V1：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureMITOrderV1");
        }

        private void futureOrderControl1_OnSendTXOffsetSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strYearMonth, int nBuySell, int nQty)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendTXOffset(strLogInID, bAsyncOrder, strAccount, strYearMonth, nBuySell, nQty, out strMessage);

            WriteMessage("大小台互抵：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendTXOffset");
        }
        private void StockStrategyOrderControl1_OnStockStrategyFTLCBSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyCB(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-FTLCB策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyFTLCB");
        }

        private void futureOrderControl1_OnSendTFOffsetSignal(string strLogInID, bool bAsyncOrder, string strAccount, int nCommodity, string strYearMonth, int nBuySell, int nQty)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendTFOffset(strLogInID, bAsyncOrder, strAccount, nCommodity, strYearMonth, nBuySell, nQty, out strMessage);

            WriteMessage("期貨互抵-可選商品：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendTFOffset");
        }


        private void TFTOMIT_OnOptionMITOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOptionMITOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("選擇權MIT委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOptionMITOrder");
        }

        private void withDrawInOutControl1_OnWithDrawSignal(string strLogInID, string strAccountOut, int nTypeOut, string strAccountIn, int nTypeIn, int nCurrency, string strDollars, string strPWD)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.WithDraw(strLogInID, strAccountOut, nTypeOut, strAccountIn, nTypeIn, nCurrency, strDollars, strPWD, out strMessage);

            WriteMessage("國內外出入金互轉：" + strMessage);
            SendReturnMessage("Order", m_nCode, "WithDrawInOut");
        }

        private void withDrawInOutControl1_OnCapitalPayWithDrawSignal(string strLogInID, string strBankCode, string strSwiftCode, string strBankAcno, string strDollars)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CapitalPayWithDraw(strLogInID, strBankCode, strSwiftCode, strBankAcno, strDollars, out strMessage);

            WriteMessage("一戶通出金：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CapitalPayWithDrawInOut");
        }

        private void balanceInOutControl1_OnWithDrawSignal(string strLogInID)
        {
            string strMessage = "";
            strMessage = m_pSKOrder.GetBalance(strLogInID);

            WriteMessage("一戶通查詢結果：" + strMessage);
            // SendReturnMessage("Order", m_nCode, "GetBalance");
        }
        private void GetT3DueAmtInOutControl1_OnWithDrawSignal(string strLogInID, string strAccount)
        {
            string strMessage = "";
            strMessage = m_pSKOrder.GetT3DueAmt(strLogInID, strAccount);

            WriteMessage("近三日交割款查詢結果：" + strMessage);
            // SendReturnMessage("Order", m_nCode, "GetBalance");
        }

        private void GetIPAAmtInOutControl1_OnWithDrawSignal(string strLogInID, string strAccount, int nMarket)//20241224
        {
            string strMessage = "";
            strMessage = m_pSKOrder.GetIPAAmt(strLogInID, strAccount, nMarket);

            WriteMessage("已圈存金額查詢結果：" + "\n" + strMessage);
            // SendReturnMessage("Order", m_nCode, "GetBalance");
        }

        private void GetBankBlockControl1_OnWithDrawSignal(string strLogInID, string strAccount)//20241224
        {
            string strMessage = "";
            strMessage = m_pSKOrder.GetBankBlock(strLogInID, strAccount);

            string[] strValues;
            strValues = strMessage.Split('#');

            WriteMessage("複委託外幣圈存查詢結果：\n");

            foreach (string strValue in strValues)
            {
                WriteMessage(strValue + "\n");
            }
            SendReturnMessage("Order", m_nCode, "GetBankBlock");

        }
        private void GetNTDBlockControl1_OnWithDrawSignal(string strLogInID, string strAccount)//20241224
        {
            string strMessage = "";

            strMessage = m_pSKOrder.GetNTDBlock(strLogInID, strAccount);

            string[] strValues;
            strValues = strMessage.Split('#');

            WriteMessage("複委託台幣圈存查詢結果：\n");

            foreach (string strValue in strValues)
            {
                WriteMessage(strValue + "\n");
            }
            SendReturnMessage("Order", m_nCode, "GetNTDBankBlock");

        }

        private void futureOrderControl1_OnSendTFOffsetNewSignal(string strLogInID, bool bAsyncOrder, string strAccount, int nCommodity, string strYearMonth, int nBuySell, int nQty, int nQty_2, int nQty_3)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendTFOffsetNew(strLogInID, bAsyncOrder, strAccount, nCommodity, strYearMonth, nBuySell, nQty, nQty_2, nQty_3, out strMessage);

            WriteMessage("期貨三階互抵-可選商品：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendTFOffset");
        }

        private void StockStrategyOrderControl1_OnStockStrategyDayTradeSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyDayTrade(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-當沖組合：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyDayTrade");
        }

        private void StockStrategyOrderControl1_OnStockStrategyAllCleanOutSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDEROUT pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyClear(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-出清策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyAllCleanOut");
        }

        private void StockStrategyOrderControl1_OnTSStrategyReportSignal(string strLogInID, string strAccount, string strMarketType, int nTypeReport, string strKindReport, string strStartDate)
        {
            m_nCode = m_pSKOrder.GetTSSmartStrategyReport(strLogInID, strAccount, strMarketType, nTypeReport, strKindReport, strStartDate);
            SendReturnMessage("Order", m_nCode, "GetTSStrategyReport");

        }
        private void StockStrategyOrderControl1_OnCancelTSStrategyOrderSignal(string strLogInID, bool bAsyncOrder, string strAccount, string strSmartKey, int nTradeType)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.CancelTSStrategyOrder(strLogInID, bAsyncOrder, strAccount, strSmartKey, nTradeType, out strMessage);
            if (nTradeType == 1)
            {
                WriteMessage("證券智慧單刪單-當沖-母單：" + strMessage);
                SendReturnMessage("Order", m_nCode, "CancelTSStrategyOrderDayTrade");
            }
            else if (nTradeType == 4)
            {
                WriteMessage("證券智慧單刪單-出清：" + strMessage);
                SendReturnMessage("Order", m_nCode, "CancelTSStrategyOrderClearOut");
            }
            else if (nTradeType == 5)
            {
                WriteMessage("證券智慧單刪單-MIT_OCO：" + strMessage);
                SendReturnMessage("Order", m_nCode, "CancelTSStrategyOrderMIT_OCO");

            }
            else if (nTradeType == 6)
            {
                WriteMessage("證券智慧單刪單-MIOC：" + strMessage);
                SendReturnMessage("Order", m_nCode, "CancelTSStrategyOrderMIOC");

            }
            else if (nTradeType == 7)
            {
                WriteMessage("證券智慧單刪單-MST：" + strMessage);
                SendReturnMessage("Order", m_nCode, "CancelTSStrategyOrderMST");

            }
        }

        private void StockStrategyOrderControl1_OnCancelTSStrategyOrderV1Signal(string strLogInID, CANCELSTRATEGYORDER pOrder)
        {

            string strMessage = "";
            m_nCode = m_pSKOrder.CancelTSStrategyOrderV1(strLogInID, pOrder, out strMessage);

            WriteMessage("證券智慧單刪單V1-" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelTSStrategyOrderV1");
        }

        private void StockStrategyOrderControl1_OnStockStrategyMITSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyMIT(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-MIT策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyMIT");
        }

        private void StockStrategyOrderControl1_OnStockStrategyOCOSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDEROCO pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyOCO(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-OCO策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyOCO");
        }
        private void StockStrategyOrderControl1_OnStockStrategyMIOCSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIOC pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyMIOC(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-MIOC策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyMIOC");
        }
        private void StockStrategyOrderControl1_OnStockStrategyMSTOrderSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyMST(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-MST策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyMST");
        }


        #endregion


        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        void SendReturnMessage(string strType, int nCode, string strMessage)
        {
            if (GetMessage != null)
            {
                GetMessage(strType, nCode, strMessage);
            }
        }

        public void WriteMessage(string strMsg)
        {
            listInformation.Items.Add(strMsg);

            listInformation.SelectedIndex = listInformation.Items.Count - 1;

            //listInformation.HorizontalScrollbar = true;

            // Create a Graphics object to use when determining the size of the largest item in the ListBox.
            Graphics g = listInformation.CreateGraphics();

            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            // Set the HorizontalExtent property.
            //[20170607-d-]listInformation.HorizontalExtent = hzSize;
        }

        #endregion

        private void Btn_SGXAPI_Click(object sender, EventArgs e)
        {
            if (boxOSFutureAccount.SelectedIndex == -1)
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }


            string strInfo = boxOSFutureAccount.Text;

            string[] strValues;
            strValues = strInfo.Split(' ');

            m_pSKOrder.AddSGXAPIOrderSocket(strValues[0], strValues[1]);
            overseaFutureOrderControl1.SGXDMA = m_bSGX;
        }

        private void btn_PingandTracertTest_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.SKOrderLib_PingandTracertTest();

            SendReturnMessage("Order", m_nCode, "SKOrderLib_PingandTracertTest");
        }

        private void btn_TelnetTest_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.SKOrderLib_TelnetTest();

            SendReturnMessage("Order", m_nCode, "SKOrderLib_TelnetTest");
        }

        //private void button1_Click(object sender, EventArgs e)
        // {
        //    string[] strValue = boxOSFutureAccount.Text.Split(' ');
        //     string strLoginID = strValue[0];
        //    m_nCode = m_pSKOrder.SKOrderLib_UpdateToken(strLoginID);

        //     SendReturnMessage("Order", m_nCode, "SKOrderLib_UpdateToken");
        // }

        private void SwitchAccountTimer_Click(object sender, EventArgs e)
        {
            _TimerTimer = new TimersTimer();
            _TimerTimer.Interval = 10000;//Unit is ms
            _TimerTimer.Elapsed += new System.Timers.ElapsedEventHandler(_TimersTimer_Elapsed);
            _TimerTimer.Enabled = true;
            _TimerTimer.Start();

        }
        void _TimersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 1; i < boxOSFutureAccount.Items.Count; i++)
            {
                boxOSFutureAccount.SelectedIndex = i;
                boxOSFutureAccount_SelectedIndexChanged(sender, e);
                Thread.Sleep(1000);
            }


        }
        private void StopSwitchATimer_Click(object sender, EventArgs e)
        {

            _TimerTimer.Enabled = false;
            _TimerTimer.Stop();

        }

        private void MC_Initialize_Click(object sender, EventArgs e)
        {
            IDs_Box.Items.Clear();
            if (m_strLoginID != "")
            {
                IDs_Box.Items.Add(m_strLoginID);
            }
            if (m_strLoginID2 != "")
            {
                IDs_Box.Items.Add(m_strLoginID2);
            }

            if (m_bfirst == true)
            {




                m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(m_OrderObj_OnAccount);
                m_pSKOrder.OnAsyncOrder += new _ISKOrderLibEvents_OnAsyncOrderEventHandler(m_pSKOrder_OnAsyncOrder);
                m_pSKOrder.OnAsyncOrderOLID += new _ISKOrderLibEvents_OnAsyncOrderOLIDEventHandler(m_pSKOrder_OnAsyncOrderOLID);
                //m_pSKOrder.OnRealBalanceReport += new _ISKOrderLibEvents_OnRealBalanceReportEventHandler(m_pSKOrder_OnRealBalanceReport);
                m_pSKOrder.OnOpenInterest += new _ISKOrderLibEvents_OnOpenInterestEventHandler(m_pSKOrder_OnOpenInterest);
                m_pSKOrder.OnOverseaFutureOpenInterest += new _ISKOrderLibEvents_OnOverseaFutureOpenInterestEventHandler(m_pSKOrder_OnOverseaFutureOpenInterest);
                m_pSKOrder.OnStopLossReport += new _ISKOrderLibEvents_OnStopLossReportEventHandler(m_pSKOrder_OnStopLossReport);
                m_pSKOrder.OnOverseaFuture += new _ISKOrderLibEvents_OnOverseaFutureEventHandler(m_pSKOrder_OnOverseaFuture);
                m_pSKOrder.OnOverseaOption += new _ISKOrderLibEvents_OnOverseaOptionEventHandler(m_pSKOrder_OnOverseaOption);
                m_pSKOrder.OnFutureRights += new _ISKOrderLibEvents_OnFutureRightsEventHandler(m_pSKOrder_OnFutureRights);
                m_pSKOrder.OnRequestProfitReport += new _ISKOrderLibEvents_OnRequestProfitReportEventHandler(m_pSKOrder_OnRequestProfitReport);
                m_pSKOrder.OnOverSeaFutureRight += new _ISKOrderLibEvents_OnOverSeaFutureRightEventHandler(m_pSKOrder_OnOverSeaFutureRight);
                //m_pSKOrder.OnMarginPurchaseAmountLimit += new _ISKOrderLibEvents_OnMarginPurchaseAmountLimitEventHandler(m_pSKOrder_OnMarginPurchaseAmountLimit);
                //m_pSKOrder.OnBalanceQuery += new _ISKOrderLibEvents_OnBalanceQueryEventHandler(m_pSKOrder_OnBalanceQueryReport);
                m_pSKOrder.OnTSSmartStrategyReport += new _ISKOrderLibEvents_OnTSSmartStrategyReportEventHandler(m_pSKOrder_OnTSStrategyReport);
                m_pSKOrder.OnProfitLossGWReport += new _ISKOrderLibEvents_OnProfitLossGWReportEventHandler(m_pSKOrder_OnTSProfitLossGWReport);
                m_pSKOrder.OnOFOpenInterestGWReport += new _ISKOrderLibEvents_OnOFOpenInterestGWReportEventHandler(m_pSKOrder_OnOFOpenInterestGW);
                m_pSKOrder.OnTelnetTest += new _ISKOrderLibEvents_OnTelnetTestEventHandler(m_pSKOrder_OnTelnetTest);
                m_pSKOrder.OnPasswordUpdateToken += new _ISKOrderLibEvents_OnPasswordUpdateTokenEventHandler(m_pSKOrder_OnPasswordUpdateToken);
                // m_pSKOrder.OnAsyncOrderGW += new _ISKOrderLibEvents_OnAsyncOrderGWEventHandler(m_pSKOrder_OnAsyncOrderGW);
                m_pSKOrder.OnProxyStatus += new _ISKOrderLibEvents_OnProxyStatusEventHandler(m_pSKOrder_OnProxyStatus);
                m_pSKOrder.OnProxyOrder += new _ISKOrderLibEvents_OnProxyOrderEventHandler(m_pSKOrder_OnProxyOrder);
                m_pSKOrder.OnOpenInterestGWStatus += new _ISKOrderLibEvents_OnOpenInterestGWStatusEventHandler(m_pSKOrder_OnOpenInterestGWStatus);
                m_pSKOrder.OnOverseaFutureOpenInterestGWStatus += new _ISKOrderLibEvents_OnOverseaFutureOpenInterestGWStatusEventHandler(m_pSKOrder_OnOverseaFutureOpenInterestGWStatus);
                m_pSKOrder.OnFutureRightsStatus += new _ISKOrderLibEvents_OnFutureRightsStatusEventHandler(m_pSKOrder_OnFutureRightsStatus);
                m_pSKOrder.OnOverseaFutureRightsStatus += new _ISKOrderLibEvents_OnOverseaFutureRightsStatusEventHandler(m_pSKOrder_OnOverseaFutureRightsStatus);
                m_pSKOrder.OnOpenInterestJson += new _ISKOrderLibEvents_OnOpenInterestJsonEventHandler(m_pSKOrder_OnOpenInterestJson);

                m_bfirst = false;
            }



            m_nCode = m_pSKOrder.SKOrderLib_MCInitialize(m_strLoginID);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_MCInitialize");
        }

        private void btnDownloadOFGW_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.SKOrderLib_LoadOfCommodityGW(m_strLoginID);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_LoadOfCommodityGW:" + m_strLoginID);
            overseaFutureOrderControl1.SGXDMA = m_bSGX;
        }

        private void btn_LogUpload_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.SKOrderLib_LogUpload();

            SendReturnMessage("Order", m_nCode, "SKOrderLib_LogUpload");
        }

        private void btnProxyDisconnect_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.ProxyDisconnectByID(m_strLoginID);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_ProxyDisconnectByID:" + m_strLoginID);
        }

        private void btnProxyDisconnect2_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.ProxyDisconnectByID(m_strLoginID2);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_ProxyDisconnectByID:" + m_strLoginID2);
        }

        private void btnProxyReConnect_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.ProxyReconnectByID(m_strLoginID);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_ProxyReConnectByID:" + m_strLoginID);
        }

        private void btnProxyReConnect2_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKOrder.ProxyReconnectByID(m_strLoginID2);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_ProxyReConnectByID:" + m_strLoginID2);
        }

        private void btnProxyIni_Click(object sender, EventArgs e)
        {
            string strIDs = txtInitIDs.Text.Trim();

            m_nCode = m_pSKOrder.SKOrderLib_InitialProxyByID(strIDs);
            SendReturnMessage("Order", m_nCode, "SKOrderLib_InitialProxyByID");
        }

        private void proxyControl1_OnOverseaFutureProxyOrderSignal(string strLogInID, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";

            m_nCode = m_pSKOrder.SendOverseaFutureProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy海期委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureProxyOrder");
        }
        //[20231123]
        private void proxyControl1_OnStockProxyOrderSignal(string strLogInID, STOCKPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy證券委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendStockProxyOrder");
        }

        //[20231123]
        private void proxyControl1_OnStockProxyAlterSignal(string strLogInID, STOCKPROXYORDER pStock)
        {
            string strMessage = "";

            m_nCode = m_pSKOrder.SendStockProxyAlter(strLogInID, pStock, out strMessage);

            WriteMessage("proxy證券刪改單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendStockProxyAlter");
        }

        ////20250115
        private void proxyControl1_OnStockProxyPreAlterSignal(string strLogInID, STOCKPROXYORDER pStock)
        {
            string strMessage = "";

            m_nCode = m_pSKOrder.SendStockProxyPreAlter(strLogInID, pStock, out strMessage);

            WriteMessage("proxy證券特殊刪改單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendStockProxyPreAlter");
        }

        //[20231123]
        private void proxyControl1_OnFutureProxyOrderSignal(string strLogInID, FUTUREPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureProxyOrderCLR(strLogInID, pStock, out strMessage);

            WriteMessage("proxy期貨委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureProxyOrder");
        }

        //[20231128]
        private void proxyControl1_OnFutureProxyAlterSignal(string strLogInID, FUTUREPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureProxyAlter(strLogInID, pStock, out strMessage);

            WriteMessage("proxy期貨刪改單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureProxyAlter");
        }

        //[20231127]
        private void proxyControl1_OnForeignProxyCancelSignal(string strLogInID, OSSTOCKPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendForeignStockProxyCancel(strLogInID, pStock, out strMessage);

            WriteMessage("proxy複委託刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendForeignStockProxyCancel");
        }

        //[20231127]
        private void proxyControl1_OnForeignProxyOrderSignal(string strLogInID, OSSTOCKPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendForeignStockProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy複委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendForeignStockProxyOrder");
        }

        //[20231128]
        private void proxyControl1_OnOptionProxyOrderSignal(string strLogInID, FUTUREPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOptionProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy選擇權委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOptionProxyOrder");
        }

        //[20231128]
        private void proxyControl1_OnOptionProxyAlterSignal(string strLogInID, FUTUREPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOptionProxyAlter(strLogInID, pStock, out strMessage);

            WriteMessage("proxy選擇權刪改單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOptionProxyAlter");
        }

        //[20231128]
        private void proxyControl1_OnDuplexProxyOrderSignal(string strLogInID, FUTUREPROXYORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendDuplexProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy選擇權複式單委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendDuplexProxyOrder");
        }

        //[20231129]
        private void proxyControl1_OnOverseaFutureProxyAlterSignal(string strLogInID, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";

            m_nCode = m_pSKOrder.SendOverseaFutureProxyAlter(strLogInID, pStock, out strMessage);

            WriteMessage("proxy海期刪改單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureProxyAlter");
        }

        //[20231129]
        private void proxyControl1_OnOverseaFutureProxyOrderSpreadSignal(string strLogInID, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureSpreadProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy海期價差委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureSpreadProxyOrder");
        }

        private void proxyControl1_OnOverseaFutureProxyOrderSpread2Signal(string strLogInID, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaFutureSpreadProxyOrder2(strLogInID, pStock, out strMessage);

            WriteMessage("proxy海期價差委託2：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaFutureSpreadProxyOrder2");
        }

        //[20231129]
        private void proxyControl1_OnOverseaOptionProxyOrderSignal(string strLogInID, OVERSEAFUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverseaOptionProxyOrder(strLogInID, pStock, out strMessage);

            WriteMessage("proxy海選委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverseaOptionProxyOrder");
        }

        //[20231213]
        /*private void StockStrategyOrderControl1_OnStockStrategyMMITSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyMMIT(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-MIT多條件策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyMMIT");
        }*/

        //[20231214]
        private void StockStrategyOrderControl1_OnStockStrategyABSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDERMIT pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyAB(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-AB策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyAB");
        }

        //[20231214]
        private void StockStrategyOrderControl1_OnStockStrategyCBSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyCB(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-CB策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyCB");
        }

        //[20231215]
        /*private void StockStrategyOrderControl1_OnStockStrategyMBASignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyMBA(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-MBA策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyMBA");
        }*/

        //[20231218]
        /*private void StockStrategyOrderControl1_OnStockStrategyLLSSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyLLS(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-LLS策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyLLS");
        }*/

        //[20231219]
        private void TFTOMIT_OnFutureABOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureABOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("期貨AB委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendFutureABOrder");
        }

        //[20231220]
        private void OverseaStrategyControl_OnOverseaFutureOCOOrderSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pOrder)
        {
            string strMessage = "";

            m_nCode = m_pSKOrder.SendOverSeaFutureOCOOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("海期OCO委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverSeaFutureOCOOrder");
        }

        //[20231220]
        private void OverseaStrategyControl_OnCancelOFStrategyOrderSignal(string strLogInID, CANCELSTRATEGYORDER pOrder)
        {
            string strMessage = "";

            m_nCode = m_pSKOrder.CancelOFStrategyOrder(strLogInID, pOrder, out strMessage);

            WriteMessage("海期智慧單刪單：" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelOFStrategyOrder");
        }

        //[20231220]
        private void OverseaStrategyControl_OnOFSmartStrategyReportSignal(string strLogInID, string strAccount, string strMarketType, int nTypeReport, string strKindReport, string strStartDate)
        {
            m_nCode = m_pSKOrder.GetOFSmartStrategyReport(strLogInID, strAccount, strMarketType, nTypeReport, strKindReport, strStartDate);
            SendReturnMessage("Order", m_nCode, "GetOFSmartStrategyReport");
        }

        //[20231221]
        private void OverseaStrategyControl_OnOverseaFutureABOrderSignal(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendOverSeaFutureABOrder(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("海期AB委託：" + strMessage);
            SendReturnMessage("Order", m_nCode, "SendOverSeaFutureABOrder");
        }

        //[20240109]
        /*private void stockOrderControl1_OnSpecialRequestSignal(string strLogInID, string strTradeType, string strApplyDate, string strStockID, string strQty, string strAmt, string strBrokerID, string strAcno, string strPaymentDate)
        {
            string strResult = m_pSKOrder.SpecialRequest(strLogInID, strTradeType, strApplyDate, strStockID, strQty, strAmt, strBrokerID, strAcno, strPaymentDate);

            WriteMessage("特殊指示 :" + strResult);

            SendReturnMessage("Order", m_nCode, "SpecialRequest");
        }*/


        //[20240109]
        private void stockOrderControl1_OnOrderReportSignal(string strLogInID, string strAccount, long lFormat)
        {
            string strMessage = "";

            strMessage = m_pSKOrder.GetOrderReport(strLogInID, strAccount, (int)lFormat);
            string[] lines = strMessage.Split(new[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                WriteMessage("委託回報查詢 :" + line);
            }
            
        }

        //[20240109]
        private void stockOrderControl1_OnFulfillReportSignal(string strLogInID, string strAccount, long lFormat)
        {
            string strMessage = "";

            strMessage = m_pSKOrder.GetFulfillReport(strLogInID, strAccount, (int)lFormat);
            string[] lines = strMessage.Split(new[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                WriteMessage("成交回報查詢 :" + line);
            }
            
        }
        
        //[20240222]
        /*private void StockStrategyOrderControl1_OnStockStrategyFTLDayTradeOrderSignal(string strLogInID, bool bAsyncOrder, STOCKSTRATEGYORDER pOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendStockStrategyFTLDayTrade(strLogInID, bAsyncOrder, pOrder, out strMessage);

            WriteMessage("證券智慧單-快速當沖OCO策略：" + strMessage);
            SendReturnMessage("Order", m_nCode, "StockStrategyFTLDayTrade");
        }*/
        
        //[20240229]
        private void StockStrategyOrderControl1_OnCancelStrategyListSignal(string strLogInID, CANCELSTRATEGYORDER pOrder)
        {

            string strMessage = "";
            m_nCode = m_pSKOrder.CancelStrategyList(strLogInID, pOrder, out strMessage);

            WriteMessage("智慧單多筆刪單" + strMessage);
            SendReturnMessage("Order", m_nCode, "CancelStrategyList");
        }

        private void ForeignOrderControl1_OnForeignStockOrderOLIDSignal(string strLogInID, bool bAsyncOrder, FOREIGNORDER pStock, string strOrderLinkedID)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendForeignStockOrderOLID(strLogInID, bAsyncOrder, pStock, strOrderLinkedID, out strMessage);

            WriteMessage("複委託OLID：" + strMessage);
            SendReturnMessage("Order", m_nCode, "ForeignStockOrderOLID");
        }

       /* private void optionOrderControl1_OnOptionSimulationSignal(string strLogInID, FUTUREORDER pFutureOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.BasketOptionSimulation(strLogInID, pFutureOrder, out strMessage);
            string[] lines = strMessage.Split(new[] { "#", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                WriteMessage("全拆平組試算 :" + line);
            }
        }*/

        private void optionOrderControl1_OnAllCoverDisOptionsSignal(string strLogInID, FUTUREORDER pFutureOrder)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.AllCoverDisOptions(strLogInID, pFutureOrder, out strMessage);
            WriteMessage("全拆組/全拆平組：" + strMessage);
            SendReturnMessage("Order", m_nCode, "AllCoverDisOption");

        }

        private void stockOrderControl1_OnGetAvgCostSignal(string strLogInID, SKAVGCOST pGWQuery)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.GetAvgCost(strLogInID, pGWQuery, out strMessage);

            string[] strValues = strMessage.Split('#');

            WriteMessage("[GetAvgCostData] 昨日均價 查詢：\n");

            foreach (string strValue in strValues)
            {
                WriteMessage(strValue + "\n");
            }
            SendReturnMessage("Order", m_nCode, "GetAvgCostData");
        }
    }
}
