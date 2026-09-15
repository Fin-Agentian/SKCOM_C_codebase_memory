using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;

namespace SKCOMTester
{
    public partial class Form1 : Form
    {
        #region Environment Variable
        //----------------------------------------------------------------------
        // Environment Variable
        //----------------------------------------------------------------------
        int m_nCode;

        SKCenterLib m_pSKCenter;
        SKCenterLib m_pSKCenter2;
        SKOrderLib m_pSKOrder;

        SKReplyLib m_pSKReply;
        SKQuoteLib m_pSKQuote;
        SKOSQuoteLib m_pSKOSQuote;
        SKOOQuoteLib m_pSKOOQuote;
        SKReplyLib m_pSKReply2;
        SKQuoteLib m_pSKQuote2;
        SKOrderLib m_pSKOrder2;

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            m_pSKCenter = new SKCenterLib();
            m_pSKCenter2 = new SKCenterLib();   

            m_pSKOrder = new SKOrderLib();
            skOrder1.OrderObj = m_pSKOrder;

            m_pSKReply = new SKReplyLib();
            skReply1.SKReplyLib = m_pSKReply;

            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.OnAnnouncement);
            m_pSKReply.OnReplyMessageSpecial += new _ISKReplyLibEvents_OnReplyMessageSpecialEventHandler(this.OnAnnouncementSpecial);//[-20240321-Add] 元朔訊息中心回傳

            m_pSKQuote = new SKQuoteLib();            
            skQuote1.SKQuoteLib = m_pSKQuote;            

            m_pSKCenter2.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(this.OnShowAgreement);
            m_pSKCenter2.OnNotifySGXAPIOrderStatus += new _ISKCenterLibEvents_OnNotifySGXAPIOrderStatusEventHandler(this.m_pSKCenter_OnSGXAPIOrderStatus);


            if (AP_CKBOX.Checked == false)
            {
                label2.Visible = false;
                Center_Box.Visible = false;
                Group_ID1.Visible = false;
                SUB_ID1.Visible = false;
                BTN_GENKEY.Visible = false;
            }
            Center_Box.SelectedIndex = 0;
            AuthorityBox.SelectedIndex = 0;

        }

        #endregion

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            if (SetSource.SelectedIndex ==1)
                m_pSKCenter.SKCenterLib_SetICEBrand(txtAccount.Text.Trim().ToUpper());
            if (SetSource.SelectedIndex == 2) //[MC]
                m_pSKCenter.SKCenterLib_SetMCBrand(txtAccount.Text.Trim().ToUpper());
            if(SetSource.SelectedIndex == 3) //[MC white]
                m_pSKCenter.SKCenterLib_SetMCWhiteBrand(txtAccount.Text.Trim().ToUpper(), true);

            // if (checkSGXDMA.Checked == true)
            //     m_pSKCenter.SKCenterLib_SetAuthority(0);
            // else
            //    m_pSKCenter.SKCenterLib_SetAuthority(1);

            //bit 1(dev) ; bit0 (SGXDMA//)
            if (AuthorityBox.SelectedIndex != -1)//formal env. and no SGX
                m_pSKCenter.SKCenterLib_SetAuthority(AuthorityBox.SelectedIndex);
            //2 :dev- no SGX SKCenterLib_SetAuthority(2);
            //3 :dev-SGX SKCenterLib_SetAuthority(3);
            //else
            //    m_pSKCenter.SKCenterLib_SetAuthority(2);

            //local-test//
            //[-for API exam switch-0513-add-]      
            

            m_nCode = m_pSKCenter.SKCenterLib_Login(txtAccount.Text.Trim().ToUpper(), txtPassWord.Text.Trim());

            if (m_nCode == 0)
            {


                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "雙因子登入成功");
                skOrder1.LoginID = txtAccount.Text.Trim().ToUpper();
                skOrder1.LoginID2 = txtAccount2.Text.Trim().ToUpper();

                skReply1.LoginID = txtAccount.Text.Trim().ToUpper();


                skQuote1.LoginID = txtAccount.Text.Trim().ToUpper();
               // if (checkSGXDMA.Checked == true)//[debug]
               //     this.StartMTimer_Click(sender,e);

            }
            else if (m_nCode == 101)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 請重新登入");

            }
            else if (m_nCode == 300)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 請確認密碼");

            }
            else if (m_nCode == 306)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功,請確認身份證ID  (DEV 請先申請再登入)");

            }
            else if (m_nCode == 307)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功,密碼目前鎖定狀態,請至金融網解鎖");

            }
            else if ((m_nCode == 600 || m_nCode == 604))
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 請在強制雙因子實施前確認憑證是否有效");
                skOrder1.LoginID = txtAccount.Text.Trim().ToUpper();
                skOrder1.LoginID2 = txtAccount2.Text.Trim().ToUpper();

                skReply1.LoginID = txtAccount.Text.Trim().ToUpper();

                skQuote1.LoginID = txtAccount.Text.Trim().ToUpper();        

            }
            else if (m_nCode == 502)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 目前為強制雙因子登入,請確認群組欲指定子帳ID");
                
            }
            else if (m_nCode == 507)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 目前為強制雙因子登入,請確認裝置碼綁定");

            }
            else if (m_nCode == 511)
            {//[V2.13.44]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 目前為強制雙因子登入,請確認群組是否申請權限");

            }
            else if (m_nCode == 321)
            {//[V2.13.53]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":測試未完成：請先至API下載專區 完成測試後再進行下一步。");
            }
            else if (m_nCode == 1129)
            {//[V2.13.56]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":登入失敗，請稍等五秒後再試，謝謝!");
            }
            else if (m_nCode == 9997)
            {//[V2.13.56]
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":登入失敗已達五次，請重啟API，謝謝!");
            }
            else
                WriteMessage(m_pSKCenter.SKCenterLib_GetReturnCodeMessage(m_nCode));

            string strSKAPIVersion = "";
            if (m_nCode == 0)
            {
                if (lbl_SKAPI.Text == "" || lbl_SKAPI.Text == "SKAPI：")
                {
                    strSKAPIVersion = lbl_SKAPI.Text;

                    lbl_SKAPI.Text += m_pSKCenter.SKCenterLib_GetSKAPIVersionAndBit(txtAccount.Text.Trim().ToUpper());
                }
            }
        }

        private void btn_Center_Log_Click(object sender, EventArgs e)
        {
            m_pSKCenter.SKCenterLib_SetLogPath(txt_Center_LogPath.Text.Trim());
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
            listInformation.HorizontalExtent = hzSize;
        }

        public void WriteMessage(int nCode)
        {
            listInformation.Items.Add( m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) );

            listInformation.SelectedIndex = listInformation.Items.Count - 1;

            //listInformation.HorizontalScrollbar = true;

            // Create a Graphics object to use when determining the size of the largest item in the ListBox.
            Graphics g = listInformation.CreateGraphics();

            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            // Set the HorizontalExtent property.
            listInformation.HorizontalExtent = hzSize;
        }

        private void GetMessage(string strType, int nCode, string strMessage)
        {
            string strInfo = "";

            if (nCode != 0)
                strInfo ="【"+ m_pSKCenter.SKCenterLib_GetLastLogInfo()+ "】";

            WriteMessage("【" + strType + "】【" + strMessage + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo);
        }
        
        void OnShowAgreement(string strData)
        {
            WriteMessage("[OnShowAgreement]" + strData);
        }
        
        void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            WriteMessage(strUserID + "_" + bstrMessage);
            nConfirmCode =-1;
            
        }

        void m_pSKCenter_OnSGXAPIOrderStatus(int nStatus, string strAccount)
        {
            if (nStatus == 3001)
            {
                //if (nStatus == 0)
                //{
                lblSignalSGXAPI.ForeColor = Color.Yellow;
                skOrder1.SGXDMA = false;
                //}
            }
            else if (nStatus == 3002)
            {
                lblSignalSGXAPI.ForeColor = Color.Red;
                skOrder1.SGXDMA = false;

            }
            else if (nStatus == 3026)
            {
                lblSignalSGXAPI.ForeColor = Color.Green;
                skOrder1.SGXDMA = true;
            }
            else if (nStatus  == 1053)
            {
                lblSignalSGXAPI.ForeColor = Color.Black;
                skOrder1.SGXDMA = false;
            }
           

             WriteMessage("【OF:" + strAccount + "】【SGX API Order Connection:" + nStatus + "】");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (AuthorityBox.SelectedIndex != -1)//formal env. and no SGX
                m_pSKCenter2.SKCenterLib_SetAuthority(AuthorityBox.SelectedIndex);
            //2 :dev- no SGX SKCenterLib_SetAuthority(2);
            //3 :dev-SGX SKCenterLib_SetAuthority(3);
            //else
           //    m_pSKCenter2.SKCenterLib_SetAuthority(2);

            m_nCode = m_pSKCenter2.SKCenterLib_Login(txtAccount2.Text.Trim().ToUpper(), txtPassWord2.Text.Trim());

            if (m_nCode == 0)
            {
                skOrder1.LoginID = txtAccount.Text.Trim().ToUpper();
                skOrder1.LoginID2 = txtAccount2.Text.Trim().ToUpper();
                skReply1.LoginID2 = txtAccount2.Text.Trim().ToUpper();
                skQuote1.LoginID2 = txtAccount2.Text.Trim().ToUpper();
                WriteMessage(DateTime.Now.TimeOfDay.ToString()+"雙因子登入成功" );
            }
            else if ((m_nCode >= 600 && m_nCode <= 699))
            {
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 請在強制雙因子實施前確認憑證是否有效");
                skOrder1.LoginID = txtAccount.Text.Trim().ToUpper();
                skOrder1.LoginID2 = txtAccount2.Text.Trim().ToUpper();

                skReply1.LoginID = txtAccount.Text.Trim().ToUpper();
                skReply1.LoginID2 = txtAccount.Text.Trim().ToUpper();

                skQuote1.LoginID = txtAccount.Text.Trim().ToUpper();

                
            }
            else if ((m_nCode >= 500 && m_nCode <= 599))
            {
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "_" + m_nCode.ToString() + ":未使用雙因子登入成功, 目前為強制雙因子登入,請確認憑證是否有效");
                
            }
            else
                WriteMessage(m_pSKCenter.SKCenterLib_GetReturnCodeMessage(m_nCode));

            string strSKAPIVersion = "";
            if (lbl_SKAPI.Text == "" || lbl_SKAPI.Text == "SKAPI：")
            {
                strSKAPIVersion = lbl_SKAPI.Text;

                lbl_SKAPI.Text += m_pSKCenter.SKCenterLib_GetSKAPIVersionAndBit(txtAccount.Text.Trim().ToUpper());
            }
        }

        private void btnRequestAgreement_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKCenter.SKCenterLib_RequestAgreement(txtAccount.Text.Trim().ToUpper());
            GetMessage("Center", m_nCode, "RequestAgreement");
        }

        private void btnInitializeQuote_Click(object sender, EventArgs e)
        {
            /*if (checkSGXDMA.Checked == true)
                m_pSKCenter.SKCenterLib_SetAuthority(0);
            else
                m_pSKCenter.SKCenterLib_SetAuthority(1);*/
            if (AuthorityBox.SelectedIndex != -1)//[-20240520-Add] 設定正測區
                m_pSKCenter.SKCenterLib_SetAuthority(AuthorityBox.SelectedIndex);
            //[-for API exam switch-0513-add-]


            m_nCode = m_pSKCenter.SKCenterLib_LoginSetQuote(txtAccount.Text.Trim().ToUpper(), txtPassWord.Text.Trim(), checkQuoteFlag.Checked ? "Y" : "N");
            if (m_nCode == 0 || (m_nCode >=600 && m_nCode <=699))
            {
                WriteMessage(DateTime.Now.TimeOfDay.ToString() + "登入成功");
                skOrder1.LoginID = txtAccount.Text.Trim().ToUpper();
                skOrder1.LoginID2 = txtAccount2.Text.Trim().ToUpper();

                skReply1.LoginID = txtAccount.Text.Trim().ToUpper();
                

                skQuote1.LoginID = txtAccount.Text.Trim().ToUpper();
            }
            else
                WriteMessage(m_pSKCenter.SKCenterLib_GetReturnCodeMessage(m_nCode));
        }

        private void BTN_GENKEY_Click(object sender, EventArgs e)
        {
            if (AuthorityBox.SelectedIndex != -1)//formal env. and no SGX
                m_pSKCenter.SKCenterLib_SetAuthority(AuthorityBox.SelectedIndex);

            if (Center_Box.SelectedIndex == 0)
                m_nCode = m_pSKCenter.SKCenterLib_GenerateKeyCert(Group_ID1.Text.Trim().ToUpper(), SUB_ID1.Text.Trim().ToUpper());
            else if (Center_Box.SelectedIndex == 1)
                m_nCode = m_pSKCenter2.SKCenterLib_GenerateKeyCert(Group_ID1.Text.Trim().ToUpper(), SUB_ID1.Text.Trim().ToUpper());
            
            if (m_nCode == 0 )
            {
                string strGroupID = Group_ID1.Text.Trim().ToUpper();
                string strSubID = SUB_ID1.Text.Trim().ToUpper();
                WriteMessage("雙因子群組AP_APH :"+ strGroupID +" with " + strSubID + " GenerateKey Success!");
                if (Center_Box.SelectedIndex == 0 && txtAccount.Text != "") txtAccount.Text = strGroupID;
                if (Center_Box.SelectedIndex == 1 && txtAccount2.Text != "" ) txtAccount2.Text = strGroupID;
            }
            else
                WriteMessage(m_pSKCenter.SKCenterLib_GetReturnCodeMessage(m_nCode));

        }

        private void AP_CKBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (AP_CKBOX.Checked)
            {
                label2.Visible = true;
                Center_Box.Visible = true;
                Group_ID1.Visible = true;
                SUB_ID1.Visible = true;
                BTN_GENKEY.Visible = true;
            }
            else if (AP_CKBOX.Checked == false)
            {
                label2.Visible = false;
                Center_Box.Visible = false;
                Group_ID1.Visible = false;
                SUB_ID1.Visible = false;
                BTN_GENKEY.Visible = false;
            }
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtAccount.Text.Contains("APH") || txtAccount.Text.Contains("APG"))
            {
                label2.Visible = true;
                Center_Box.Visible = true;
                Group_ID1.Visible = true;
                SUB_ID1.Visible = true;
                BTN_GENKEY.Visible = true;
                AP_CKBOX.Checked = true;
            }
            else
            {
                label2.Visible = false;
                Center_Box.Visible = false;
                Group_ID1.Visible = false;
                SUB_ID1.Visible = false;
                BTN_GENKEY.Visible = false;
                AP_CKBOX.Checked = false;

            }
        }

        private void AuthorityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AuthorityBox.SelectedIndex == 0)
            {
                EnvLable.Text = "正式環境";
                EnvLable.BackColor = Color.Yellow;
            }
            else if (AuthorityBox.SelectedIndex ==1)
            {
                EnvLable.Text = "正式環境_SGX";
                EnvLable.BackColor = Color.Yellow;
            }
            else if (AuthorityBox.SelectedIndex == 2)
            {
                EnvLable.Text = "測VPN環境";
                EnvLable.BackColor = Color.Red;
            }
            else if (AuthorityBox.SelectedIndex ==3)
            {
                EnvLable.Text = "測VPN_SGX環境";
                EnvLable.BackColor = Color.Red;
            }
        }

        /*
        private void StopMTimer_Click(object sender, EventArgs e)
        {            
        }

        private void StartMTimer_Click(object sender, EventArgs e)
        {                           
        }*/

        //[-20240321-Add]
        void OnAnnouncementSpecial(string bstrMessage)
        {
            WriteMessage("Message：" + bstrMessage);
        }
    }  
}
