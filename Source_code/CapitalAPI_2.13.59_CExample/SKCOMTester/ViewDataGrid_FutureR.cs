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
    public partial class ViewDataGrid_FutureR : Form
    {

        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        SKCOMLib.SKOrderLib m_pSKOrder = null;
        

        //pVDG_FR = new ViewDataGrid_FutureR();
        //skVDG_FR.OrderObj = m_pVDG_FR;


        public SKOrderLib OrderObj
        {
            get { return m_pSKOrder; }
            set { m_pSKOrder = value; }
        }

        public string m_strFutureRights = "";
        public string OnFutureRights
        {
            get { return m_strFutureRights; }
            set { m_strFutureRights = value; }
        }
        #endregion
        public ViewDataGrid_FutureR()
        {
            InitializeComponent();
        }

        private void ViewDataGrid_FutureR_Load(object sender, EventArgs e)
        {
            
            SetFutureRightsForm();
            dataGridView_FutureR.Update();
            //GetOnFutureRights(m_strFutureRights);
        }

        public void GetOnFutureRights(string bstrData)
        {
            AddFutureRightsForm(bstrData, dataGridView_FutureR);//添加資料至表格內
        }

        public void AddFutureRightsForm(string strData, DataGridView dgv)
        {
            string[] m_strData = strData.Split(',');

            if (m_strData[0] == "##")
                return;

            int rowId = dgv.Rows.Add();
            DataGridViewRow row = dgv.Rows[rowId];

            for (int i = 0; i < m_strData.Length; i++)
            {
                row.Cells[i].Value = m_strData[i];
            }
        }

        private void SetFutureRightsForm()
        {
            dataGridView_FutureR.Columns.Add("m_AccountBalance", "帳戶餘額");
            dataGridView_FutureR.Columns.Add("m_ChangeProfitLoss", "浮動損益");
            dataGridView_FutureR.Columns.Add("m_ComRealizedCost", "商品已實現費用");
            dataGridView_FutureR.Columns.Add("m_DealTax", "交易稅");
            dataGridView_FutureR.Columns.Add("m_WithholdingRights", "預扣權利金");
            dataGridView_FutureR.Columns.Add("m_RightsPayment", "權利金收付");
            dataGridView_FutureR.Columns.Add("m_FutureRights", "權益數");
            dataGridView_FutureR.Columns.Add("m_ExcessMargin", "超額保證金");
            dataGridView_FutureR.Columns.Add("m_DepositWithdrawal", "存提款");
            dataGridView_FutureR.Columns.Add("m_BuyerMarketCapitalization", "買方市值");
            dataGridView_FutureR.Columns.Add("m_SellerMarketCapitalization", "賣方市值");
            dataGridView_FutureR.Columns.Add("m_FutureOffsetProfitLoss", "期貨平倉損益");
            dataGridView_FutureR.Columns.Add("m_Unrealized", "盤中未實現");
            dataGridView_FutureR.Columns.Add("m_OriginalMargin", "原始保證金");
            dataGridView_FutureR.Columns.Add("m_MaintainMargin", "維持保證金");
            dataGridView_FutureR.Columns.Add("m_PositionOriginalMargin", "部位原始保證金");
            dataGridView_FutureR.Columns.Add("m_PositionMaintainMargin", "部位維持保證金");
            dataGridView_FutureR.Columns.Add("m_EntrustedMargin", "委託保證金");
            dataGridView_FutureR.Columns.Add("m_ExcessBestMargin", "超額/追繳保證金");
            dataGridView_FutureR.Columns.Add("m_RightsTotal", "權益總值");
            dataGridView_FutureR.Columns.Add("m_WithholdingPrices", "預扣費用");
            dataGridView_FutureR.Columns.Add("m_OriginalMargin1", "原始保證金");
            dataGridView_FutureR.Columns.Add("m_YesterdayAccountBalance", "昨日餘額");
            dataGridView_FutureR.Columns.Add("m_OptionDuplexMargin", "選擇權組合單加不加收保證金");
            dataGridView_FutureR.Columns.Add("m_MaintainRate", "維持率(權益比率)");
            dataGridView_FutureR.Columns.Add("m_Currency", "幣別");
            dataGridView_FutureR.Columns.Add("m_FullOriginalMargin", "足額原始保證金");
            dataGridView_FutureR.Columns.Add("m_FullMaintainMargin", "足額維持保證金");
            dataGridView_FutureR.Columns.Add("m_FullAvailable", "足額可用");
            dataGridView_FutureR.Columns.Add("m_RecoverPrices", "抵繳金額");
            dataGridView_FutureR.Columns.Add("m_MarketableAvailable", "有價可用");
            dataGridView_FutureR.Columns.Add("m_AvailableBalance", "可用餘額");
            dataGridView_FutureR.Columns.Add("m_FullMoneyAvailable", "足額現金可用");
            dataGridView_FutureR.Columns.Add("m_MarketableValue", "有價價值");
            dataGridView_FutureR.Columns.Add("m_Risk", "風險指標(清算維持率)");
            dataGridView_FutureR.Columns.Add("m_OptionProfit", "選擇權到期差益");
            dataGridView_FutureR.Columns.Add("m_OptionLoss", "選擇權到期差損");
            dataGridView_FutureR.Columns.Add("m_FutureProfitLoss", "期貨到期損益");
            dataGridView_FutureR.Columns.Add("m_IncreaseMargin", "加收保證金");
            dataGridView_FutureR.Columns.Add("m_IncreaseMargin", "ID");
            dataGridView_FutureR.Columns.Add("m_IncreaseMargin", "FullAccount");
        }

        private void BTN_ClearRows_Click(object sender, EventArgs e)
        {
            dataGridView_FutureR.Rows.Clear();
        }

        private void ViewDataGrid_FutureR_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            BTN_ClearRows_Click(sender, e);



        }
    }


    }
