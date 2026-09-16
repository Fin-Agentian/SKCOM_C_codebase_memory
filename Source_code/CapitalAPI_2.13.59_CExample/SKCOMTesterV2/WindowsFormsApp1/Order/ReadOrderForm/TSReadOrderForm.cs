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
    public partial class TSReadOrderForm : Form
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
        public TSReadOrderForm()
        {
            // Init
            {
                InitializeComponent();
                //dataGridView
                {
                    //dataGridViewGetRealBalanceReport
                    {
                        dataGridViewGetRealBalanceReport.Columns.Add("Column1", "股票代號");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column2", "庫存種類(T:集保 C:融資 L:融券)");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column3", "資額度(原始)");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column4", "資額度(可用)");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column5", "券額度(原始)");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column6", "券額度(可用)");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column7", "股數:昨日庫存");

                        dataGridViewGetRealBalanceReport.Columns.Add("Column8", "今日委買");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column9", "今日委賣");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column10", "今日買進成交");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column11", "今日賣出成交");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column12", "今日資券可回補/集保庫存可賣出");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column13", "可資沖股數");

                        dataGridViewGetRealBalanceReport.Columns.Add("Column14", "可券沖股數");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column15", "即時庫存");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column16", "X(此欄請忽略)");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column17", "即時個股維持率");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column18", "LOGIN_ID");
                        dataGridViewGetRealBalanceReport.Columns.Add("Column19", "ACCOUNT_NO");
                    }

                    //dataGridViewGetBalanceQuery
                    {
                        dataGridViewGetBalanceQuery.Columns.Add("Column1", "證券帳號");
                        dataGridViewGetBalanceQuery.Columns.Add("Column2", "股票代號");
                        dataGridViewGetBalanceQuery.Columns.Add("Column3", "股票股數");
                        dataGridViewGetBalanceQuery.Columns.Add("Column4", "融資股數");
                        dataGridViewGetBalanceQuery.Columns.Add("Column5", "融資金額");

                        dataGridViewGetBalanceQuery.Columns.Add("Column6", "融券股數");
                        dataGridViewGetBalanceQuery.Columns.Add("Column7", "融券金額");
                        dataGridViewGetBalanceQuery.Columns.Add("Column8", "保證金");
                        dataGridViewGetBalanceQuery.Columns.Add("Column9", "擔保品");
                    }

                    //dataGridViewOnMarginPurchaseAmountLimit
                    {
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column1", "商品代號");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column2", "融資標記(0:正常1:停資9:取消信用交易)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column3", "融資限量(0:無配額 9999:無限制)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column4", "融資比率");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column5", "融券標記(0:正常1:停資9:取消信用交易)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column6", "融券限量(0:無配額 9999:無限制)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column7", "融券比率");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column8", "當沖標記(0:不可當沖 1:可當沖)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column9", "被降成標記(0:正常 1:被降成 2:被升成)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column10", "平盤放空標記(0:不可平盤放空 1:可平盤放空)");

                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column11", "全額交割標記(0:正常 1:全額交割)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column12", "警示標記(0:正常1:公司內部警示記號)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column13", "處置股票標記(0:正常1處置股票2:再處置股票3:彈性處置股票)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column14", "注意股票標記(0:正常1:注意股票)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column15", "受限股票標記(0正常1:證券商委託申報受限股票)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column16", "異常推介標記(0:正常1:異常推介個股)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column17", "特殊異常標記(0:正常1:特殊異常股票)");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column18", "單筆委託張數限制");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column19", "多筆委託張數限制");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column20", "款券預收成數");

                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column21", "LOGIN_ID");
                        dataGridViewOnMarginPurchaseAmountLimit.Columns.Add("Column22", "ACCOUNT_NO");
                    }

                    //未實現損益
                    {
                        //dataGridViewOnProfitLossGWReport1 (彙總)
                        {
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column1", "股票名稱");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column2", "股票代號");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column3", "幣別");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column4", "交易種類");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column5", "庫存股數");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column6", "市價");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column7", "今日市價漲跌");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column8", "市值");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column9", "淨值");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column10", "損益");

                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column11", "平均買進(券賣)成本");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column12", "付出成本");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column13", "成交價金");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column14", "手續費");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column15", "預估手續費");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column16", "交易稅");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column17", "預估交易稅");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column18", "融資自備款/融券保證金");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column19", "融資金/擔保品");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column20", "預估利息");

                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column21", "股息(目前此欄無值)");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column22", "試算報酬率");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column23", "未知成本股數");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column24", "備註");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column25", "詳細資料(Y:有資料 N:無資料)");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column26", "排序序號");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column27", "交易種類代號(0:現股3:融資(自)4:融券(自)8:券差9:無券賣出)");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column28", "損益兩平點(此欄只提供交易種類為現股之交易)");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column29", "LOGIN_ID");
                            dataGridViewOnProfitLossGWReport1.Columns.Add("Column30", "ACCOUNT_NO");
                        }
                        //dataGridViewOnProfitLossGWReport2 (明細)
                        {
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column1", "股票名稱");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column2", "股票代號");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column3", "成交日期");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column4", "交易種類");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column5", "買進股數");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column6", "可沖股數");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column7", "單價");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column8", "成交價金");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column9", "損益");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column10", "試算報酬率");

                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column11", "手續費");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column12", "預估手續費");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column13", "交易稅");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column14", "預估交易稅");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column15", "融資自備款/融券保證金");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column16", "融資金額/擔保品金額");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column17", "融資/融券利息");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column18", "融券手續費");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column19", "收付金額");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column20", "股息(目前此欄無值)");

                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column21", "備註");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column22", "委託書號");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column23", "交易種類代號(配合第4欄交易種類0:現股 3:融資(自) 4:融券(自))");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column24", "LOGIN_ID");
                            dataGridViewOnProfitLossGWReport2.Columns.Add("Column25", "ACCOUNT_NO");
                        }
                    }

                    //已實現損益
                    {
                        //dataGridViewOnProfitLossGWReport3 (彙總)
                        {
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column1", "交易種類");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column2", "交易種類代號(0:現股1:融資(代) 2:融券(代) 3:融資(自) 4:融券(自) 5:無券 6:現沖 8:券差)");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column3", "日期(民國年月日)");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column4", "營業單位代碼");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column5", "證券帳號");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column6", "股票代號");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column7", "股數");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column8", "成交價格");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column9", "損益");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column10", "報酬率");

                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column11", "備註");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column12", "詳細資料(Y:有資料 N:無資料)");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column13", "委託書號(賣)(交易種類是股息，此欄為編號)");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column14", "序號(賣)");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column15", "股票名稱");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column16", "幣別");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column17", "投資額");
                            dataGridViewOnProfitLossGWReport3.Columns.Add("Column18", "LOGIN_ID");
                        }
                        //dataGridViewOnProfitLossGWReport4 (明細)
                        {
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column1", "日期(民國)");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column2", "交易種類");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column3", "股票代號");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column4", "股票名稱");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column5", "委託書號");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column6", "買賣別");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column7", "股數");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column8", "單價");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column9", "價金");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column10", "手續費");

                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column11", "交易稅");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column12", "融資自備款/融券保證金");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column13", "融資金額/擔保品金額");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column14", "融資/融券利息");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column15", "融券手續費");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column16", "收付金額");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column17", "已沖銷股數");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column18", "備註");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column19", "交易種類代號(0:現股 1:融資(代) 2:融券(代) 3:融資(自) 4:融券(自))");
                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column20", "LOGIN_ID");

                            dataGridViewOnProfitLossGWReport4.Columns.Add("Column21", "ACCOUNT_NO");
                        }
                        //dataGridViewOnProfitLossGWReport5 (投資總額)
                        {
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column1", "營業單位代碼");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column2", "證券帳號");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column3", "投資總額(台幣)");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column4", "已實現投資損益(台幣)");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column5", "報酬率(台幣)");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column6", "投資總額(人民幣)");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column7", "已實現投資損益(人民幣)");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column8", "報酬率(人民幣)");
                            dataGridViewOnProfitLossGWReport5.Columns.Add("Column9", "LOGIN_ID");
                        }
                        //dataGridViewOnProfitLossGWReport6 (彙總-依商品代碼資料格式)
                        {
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column1", "交易種類");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column2", "交易種類代號");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column3", "營業單位代碼");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column4", "證券帳號");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column5", "股票代號");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column6", "股數");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column7", "已實現投資損益(台幣)");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column8", "報酬率(台幣)");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column9", "幣別");
                            dataGridViewOnProfitLossGWReport6.Columns.Add("Column10", "LOGIN_ID");
                        }
                    }

                    //現股當沖
                    {
                        //dataGridViewOnProfitLossGWReport7 (彙總)
                        {
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column1", "序號(2:彙總總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column2", "股票名稱");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column3", "股票代號");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column4", "幣別(中文)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column5", "已沖銷股數");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column6", "待沖銷股數");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column7", "委買中股數");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column8", "委賣中股數");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column9", "可沖銷股數");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column10", "市價");

                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column11", "今日市價漲跌");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column12", "市值(序號為2時，市值總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column13", "淨值(序號為2時，淨價總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column14", "未實現損益(序號為2時，未實現損益總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column15", "已實現損益(序號為2時，已實現損益總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column16", "損益總計(序號為2時，損益總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column17", "平均單價");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column18", "付出成本(序號為2時，付出成本總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column19", "成交價金(序號為2時，成交價金總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column20", "手續費(序號為2時，手續費總計)");

                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column21", "預估手續費(序號為2時，預估手續費總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column22", "交易稅(序號為2時，交易稅總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column23", "預估交易稅(序號為2時，預估交易稅總計)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column24", "試算報酬率");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column25", "備註");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column26", "詳細資料(Y:有資料 N:無資料)");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column27", "幣別");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column28", "LOGIN_ID");
                            dataGridViewOnProfitLossGWReport7.Columns.Add("Column29", "ACCOUNT_NO");
                        }
                        //dataGridViewOnProfitLossGWReport8 (明細)
                        {
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column1", "股票名稱");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column2", "委託書號");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column3", "買賣股數");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column4", "已沖股數");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column5", "單價");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column6", "成交價金");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column7", "手續費");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column8", "預估手續費");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column9", "交易稅");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column10", "預估交易稅");

                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column11", "收付金額");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column12", "股票代號");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column13", "買賣別 (B:買; S:賣)");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column14", "LOGIN_ID");
                            dataGridViewOnProfitLossGWReport8.Columns.Add("Column15", "ACCOUNT_NO");
                        }
                    }

                    //昨日未沖銷(昨日均價)
                    {
                        dataGridViewGetAvgCost.Columns.Add("Column1", "成交日期");
                        dataGridViewGetAvgCost.Columns.Add("Column2", "分公司四碼");
                        dataGridViewGetAvgCost.Columns.Add("Column3", "帳號");
                        dataGridViewGetAvgCost.Columns.Add("Column4", "股票代號");
                        dataGridViewGetAvgCost.Columns.Add("Column5", "成交序號");

                        dataGridViewGetAvgCost.Columns.Add("Column6", "庫存種類");
                        dataGridViewGetAvgCost.Columns.Add("Column7", "剩餘股數");
                        dataGridViewGetAvgCost.Columns.Add("Column8", "單價");
                        dataGridViewGetAvgCost.Columns.Add("Column9", "手續費");
                        dataGridViewGetAvgCost.Columns.Add("Column10", "融資自備款/融券保證金");

                        dataGridViewGetAvgCost.Columns.Add("Column11", "融資金額/擔保品金額");
                        dataGridViewGetAvgCost.Columns.Add("Column12", "融資/融券利息");
                        dataGridViewGetAvgCost.Columns.Add("Column13", "融券手續費");

                    }
                }
                //comboBox
                {
                    // comboBoxnTPQueryType
                    {
                        comboBoxnTPQueryType.Items.Add("未實現損益");
                        comboBoxnTPQueryType.Items.Add("已實現損益");
                        comboBoxnTPQueryType.Items.Add("現股當沖");
                    }
                    // comboBoxnFunc
                    {
                        comboBoxnFunc.Items.Add("彙總");
                        comboBoxnFunc.Items.Add("明細");
                        comboBoxnFunc.Items.Add("投資總額");
                        comboBoxnFunc.Items.Add("彙總(依股票代號)");
                    }
                    // comboBoxbstrTradeType
                    {
                        comboBoxbstrTradeType.Items.Add("現股");
                        comboBoxbstrTradeType.Items.Add("融資(自)");
                        comboBoxbstrTradeType.Items.Add("融券(自)");
                        comboBoxbstrTradeType.Items.Add("融資(代)");
                        comboBoxbstrTradeType.Items.Add("融券(代)");
                        comboBoxbstrTradeType.Items.Add("券差");
                        comboBoxbstrTradeType.Items.Add("無券賣出");
                    }
                }
            }
        }
        private void buttonGetBalanceQuery_Click(object sender, EventArgs e)
        {
            dataGridViewGetBalanceQuery.Rows.Clear(); // 每次查詢前，清除上一次查詢結果
            // 集保庫存查詢
            string bstrAccount = comboBoxAccount.Text;
            string bstrStockNo = textBoxbstrStockNo1.Text; // 代空為全部商品回傳
            int nCode = m_pSKOrder.GetBalanceQuery(comboBoxUserID.Text, bstrAccount, bstrStockNo);
            // 取得回傳訊息
            string msg = "【GetBalanceQuery】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonGetMarginPurchaseAmountLimit_Click(object sender, EventArgs e)
        {
            string bstrLogInID = comboBoxUserID.Text; // 登入ID
            string bstrAccount = comboBoxAccount.Text; // 證券帳號，分公司四碼＋帳號7碼
            string bstrStockNo = textBoxbstrStockNo2.Text; // 商品代碼，代空為全部商品回傳

            dataGridViewOnMarginPurchaseAmountLimit.Rows.Clear(); //清空

            // 資券配額查詢
            int nCode = m_pSKOrder.GetMarginPurchaseAmountLimit(bstrLogInID, bstrAccount, bstrStockNo);
            // 取得回傳訊息
            string msg = "【GetMarginPurchaseAmountLimit】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonGetProfitLossGWReport_Click(object sender, EventArgs e)
        {
            string bstrLogInID = comboBoxUserID.Text; // 登入ID 
            TSPROFITLOSSGWQUERY pPLGWQuery = new TSPROFITLOSSGWQUERY(); // SKCOM元件中的TSPROFITLOSSGWQUERY物件，將損益查詢條件填入該物件後，再帶入此欄位中

            pPLGWQuery.bstrFullAccount = comboBoxAccount.Text;
            
            if (comboBoxnTPQueryType.Text == "未實現損益")//損益類別 0: 未實現損益；1: 已實現損益 2:現股當沖
            {
                pPLGWQuery.nTPQueryType = 0;
                //損益回傳格式 //(未實現) 0:彙總 ；1:明細 
                if (comboBoxnFunc.Text == "彙總")
                {
                    pPLGWQuery.nFunc = 0;
                    dataGridViewOnProfitLossGWReport1.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage1; // 未實現(彙總)
                }
                else if (comboBoxnFunc.Text == "明細")
                {
                    pPLGWQuery.nFunc = 1;
                    dataGridViewOnProfitLossGWReport2.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage2; // 未實現(明細)
                }

                if (pPLGWQuery.nFunc == 1)
                {
                    pPLGWQuery.bstrStockNo = textBoxbstrStockNo3.Text; // 當nFunc 為 1:明細 時,指定商品代碼

                    // 當nFunc 為 1:明細 時,指定交易類別 0:現股 3:融資(自) 4:融券(自) 8:券差 9:無券賣出
                    if (comboBoxbstrTradeType.Text == "現股") pPLGWQuery.bstrTradeType = "0";
                    else if (comboBoxbstrTradeType.Text == "融資(自)") pPLGWQuery.bstrTradeType = "3";
                    else if (comboBoxbstrTradeType.Text == "融券(自)") pPLGWQuery.bstrTradeType = "4";
                    else if (comboBoxbstrTradeType.Text == "券差") pPLGWQuery.bstrTradeType = "8";
                    else if (comboBoxbstrTradeType.Text == "無券賣出") pPLGWQuery.bstrTradeType = "9";
                }

            }
            else if (comboBoxnTPQueryType.Text == "已實現損益")
            {
                pPLGWQuery.nTPQueryType = 1;
                //損益回傳格式 //(已實現) // 0:彙總；1:明細; 2:投資總額; 3: 彙總(依股票代號) 
                if (comboBoxnFunc.Text == "彙總")
                {
                    pPLGWQuery.nFunc = 0;
                    dataGridViewOnProfitLossGWReport3.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage3; // 已實現(彙總)
                }
                else if (comboBoxnFunc.Text == "明細")
                {
                    pPLGWQuery.nFunc = 1;
                    dataGridViewOnProfitLossGWReport4.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage4; // 已實現(明細)
                }
                else if (comboBoxnFunc.Text == "投資總額")
                {
                    pPLGWQuery.nFunc = 2;
                    dataGridViewOnProfitLossGWReport5.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage5; // 已實現(投資總額)
                }
                else if (comboBoxnFunc.Text == "彙總(依股票代號)")
                {
                    pPLGWQuery.nFunc = 3;
                    dataGridViewOnProfitLossGWReport6.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage6; // 已實現(彙總(依股票代號))
                }

                if (pPLGWQuery.nFunc == 1 || pPLGWQuery.nFunc == 3)
                {
                    pPLGWQuery.bstrStockNo = textBoxbstrStockNo3.Text;//當nFunc 為 1:明細 及3: 彙總依股票代號 時，指定商品代碼
                }

                if (pPLGWQuery.nFunc == 1)
                {
                    // 當nFunc 為 1:明細 時,指定交易類別 0:現股 1:融資(代) 2:融券(代) 3:融資(自) 4:融券(自) 
                    if (comboBoxbstrTradeType.Text == "現股") pPLGWQuery.bstrTradeType = "0";
                    else if (comboBoxbstrTradeType.Text == "融資(代)") pPLGWQuery.bstrTradeType = "1";
                    else if (comboBoxbstrTradeType.Text == "融券(代)") pPLGWQuery.bstrTradeType = "2";
                    else if (comboBoxbstrTradeType.Text == "融資(自)") pPLGWQuery.bstrTradeType = "3";
                    else if (comboBoxbstrTradeType.Text == "融券(自) ") pPLGWQuery.bstrTradeType = "4";
                }
                //當nFunc 為 0:彙總 時，起始日西元年月日//YYYYMMDD ； 當nFunc 為 1:明細 及 3:彙總依股票代號 時，起始日民國年月日
                if (pPLGWQuery.nFunc == 0 || pPLGWQuery.nFunc == 1 || pPLGWQuery.nFunc == 3) pPLGWQuery.bstrStartDate = textBoxbstrStartDate.Text;
                //當nFunc 為 0:彙總 時，結束日西元年月日//YYYYMMDD ； 當nFunc 為 1:明細格式時,不需填結束日 ； 當nFunc 3:彙總依股票代號 時，結束日 民國年月日
                if (pPLGWQuery.nFunc == 0 || pPLGWQuery.nFunc == 3) pPLGWQuery.bstrEndDate = textBoxbstrEndDate.Text;
                //當nFunc 為 1:明細 時,指定委託書號
                if (pPLGWQuery.nFunc == 1) pPLGWQuery.bstrBookNo = textBoxbstrBookNo.Text;
                //當nFunc 為 1:明細 時,指定序號
                if (pPLGWQuery.nFunc == 1) pPLGWQuery.bstrSeqNo = textBoxbstrSeqNo.Text;
            }
            else if (comboBoxnTPQueryType.Text == "現股當沖")
            {
                pPLGWQuery.nTPQueryType = 2;
                //損益回傳格式 //(未實現) 1:彙總 ；2:明細 
                if (comboBoxnFunc.Text == "彙總")
                {
                    pPLGWQuery.nFunc = 1;
                    dataGridViewOnProfitLossGWReport7.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage7; // 現股當沖(彙總)
                }
                else if (comboBoxnFunc.Text == "明細")
                {
                    pPLGWQuery.nFunc = 2;
                    dataGridViewOnProfitLossGWReport8.Rows.Clear();

                    tabControlTSPROFITLOSSGWQUERY.SelectedTab = tabPage8; // 現股當沖(明細)
                }

                if (pPLGWQuery.nFunc == 2)
                {
                    pPLGWQuery.bstrStockNo = textBoxbstrStockNo3.Text;//當nFunc 為 2:明細時，需指定商品代碼
                }
            }

            // 依照TSPROFITLOSSGWQUERY物件裡的nTPQueryType參數決定損益類別(0: 未實現損益；1: 已實現損益 2:現股當沖)
            int nCode = m_pSKOrder.GetProfitLossGWReport(bstrLogInID, pPLGWQuery);
            // 取得回傳訊息
            string msg = "【GetProfitLossGWReport】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void comboBoxnTPQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxnFunc.Items.Clear();

            buttonGetProfitLossGWReport.Enabled = false; // 防呆機制(至少選一個損益回傳格式才能"送出"，所以選損益類別時，送出Enabled = false) 

            if (comboBoxnTPQueryType.Text == "未實現損益")
            {
                // comboBoxnFunc
                {
                    comboBoxnFunc.Items.Add("彙總");
                    comboBoxnFunc.Items.Add("明細");
                }

                // Visible
                {
                    // 交易類別
                    comboBoxbstrTradeType.Visible = false;
                    // 商品代號
                    labelbstrStockNo3.Visible = false;
                    textBoxbstrStockNo3.Visible = false;
                    // 起始日
                    labelbstrStartDate.Visible = false;
                    textBoxbstrStartDate.Visible = false;
                    // 結束日
                    labelbstrEndDate.Visible = false;
                    textBoxbstrEndDate.Visible = false;
                    // 指定委託書號
                    labelbstrBookNo.Visible = false;
                    textBoxbstrBookNo.Visible = false;
                    // 指定序號
                    labelbstrSeqNo.Visible = false;
                    textBoxbstrSeqNo.Visible = false;
                }
            }
            else if (comboBoxnTPQueryType.Text == "已實現損益")
            {
                // comboBoxnFunc
                {
                    comboBoxnFunc.Items.Add("彙總");
                    comboBoxnFunc.Items.Add("明細");
                    comboBoxnFunc.Items.Add("投資總額");
                    comboBoxnFunc.Items.Add("彙總(依股票代號)");
                }

                // Visible
                {
                    // 交易類別
                    comboBoxbstrTradeType.Visible = false;
                    // 商品代號
                    labelbstrStockNo3.Visible = false;
                    textBoxbstrStockNo3.Visible = false;
                    // 起始日
                    labelbstrStartDate.Visible = false;
                    textBoxbstrStartDate.Visible = false;
                    // 結束日
                    labelbstrEndDate.Visible = false;
                    textBoxbstrEndDate.Visible = false;
                    // 指定委託書號
                    labelbstrBookNo.Visible = false;
                    textBoxbstrBookNo.Visible = false;
                    // 指定序號
                    labelbstrSeqNo.Visible = false;
                    textBoxbstrSeqNo.Visible = false;
                }
            }
            else if (comboBoxnTPQueryType.Text == "現股當沖")
            {
                // comboBoxnFunc
                {
                    comboBoxnFunc.Items.Add("彙總");
                    comboBoxnFunc.Items.Add("明細");
                }

                // Visible
                {
                    // 交易類別
                    comboBoxbstrTradeType.Visible = false;
                    // 商品代號
                    labelbstrStockNo3.Visible = false;
                    textBoxbstrStockNo3.Visible = false;
                    // 起始日
                    labelbstrStartDate.Visible = false;
                    textBoxbstrStartDate.Visible = false;
                    // 結束日
                    labelbstrEndDate.Visible = false;
                    textBoxbstrEndDate.Visible = false;
                    // 指定委託書號
                    labelbstrBookNo.Visible = false;
                    textBoxbstrBookNo.Visible = false;
                    // 指定序號
                    labelbstrSeqNo.Visible = false;
                    textBoxbstrSeqNo.Visible = false;
                }
            }
        }
        private void comboBoxnFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxbstrTradeType.Items.Clear();

            buttonGetProfitLossGWReport.Enabled = true; // 防呆機制(至少選一個損益回傳格式才能"送出")

            if (comboBoxnFunc.Text == "明細" || comboBoxnFunc.Text == "彙總(依股票代號)")
            {
                //Visible
                {
                    // 商品代號
                    labelbstrStockNo3.Visible = true;
                    textBoxbstrStockNo3.Visible = true;

                    // 交易類別
                    if (comboBoxnTPQueryType.Text == "現股當沖")
                    {
                        comboBoxbstrTradeType.Visible = false;
                    }
                    else
                    {
                        comboBoxbstrTradeType.Visible = true;
                    }
                }

                if (comboBoxnTPQueryType.Text == "未實現損益")
                {
                    // comboBoxbstrTradeType
                    {
                        comboBoxbstrTradeType.Items.Add("現股");
                        comboBoxbstrTradeType.Items.Add("融資(自)");
                        comboBoxbstrTradeType.Items.Add("融券(自)");
                        comboBoxbstrTradeType.Items.Add("券差");
                        comboBoxbstrTradeType.Items.Add("無券賣出");
                    }
                }
                else if (comboBoxnTPQueryType.Text == "已實現損益")
                {
                    // comboBoxbstrTradeType
                    {
                        comboBoxbstrTradeType.Items.Add("現股");
                        comboBoxbstrTradeType.Items.Add("融資(自)");
                        comboBoxbstrTradeType.Items.Add("融券(自)");
                        comboBoxbstrTradeType.Items.Add("融資(代)");
                        comboBoxbstrTradeType.Items.Add("融券(代)");
                    }
                }
            }
            else
            {
                //Visible
                {
                    // 商品代號
                    labelbstrStockNo3.Visible = false;
                    textBoxbstrStockNo3.Visible = false;
                    // 交易類別
                    comboBoxbstrTradeType.Visible = false;
                }
            }

            if (comboBoxnTPQueryType.Text == "已實現損益")
            {
                if (comboBoxnFunc.Text == "彙總")
                {
                    // 起始日年月日
                    labelbstrStartDate.Visible = true;
                    textBoxbstrStartDate.Visible = true;
                    textBoxbstrStartDate.Text = "20231201";//西元YYYYMMDD
                    // 結束日年月日
                    labelbstrEndDate.Visible = true;
                    textBoxbstrEndDate.Visible = true;
                    textBoxbstrEndDate.Text = "20231231";//西元YYYYMMDD
                }
                else
                {
                    // 起始日年月日
                    labelbstrStartDate.Visible = false;
                    textBoxbstrStartDate.Visible = false;
                    // 結束日年月日
                    labelbstrEndDate.Visible = false;
                    textBoxbstrEndDate.Visible = false;
                }
                if (comboBoxnFunc.Text == "明細")
                {
                    // 起始日年月日
                    labelbstrStartDate.Visible = true;
                    textBoxbstrStartDate.Visible = true;
                    textBoxbstrStartDate.Text = "1121201";//民國YYYMMDD

                    // 指定委託書號
                    labelbstrBookNo.Visible = true;
                    textBoxbstrBookNo.Visible = true;

                    // 指定序號
                    labelbstrSeqNo.Visible = true;
                    textBoxbstrSeqNo.Visible = true;
                }
                else
                {
                    // 起始日年月日
                    labelbstrStartDate.Visible = false;
                    textBoxbstrStartDate.Visible = false;

                    // 指定委託書號
                    labelbstrBookNo.Visible = false;
                    textBoxbstrBookNo.Visible = false;

                    // 指定序號
                    labelbstrSeqNo.Visible = false;
                    textBoxbstrSeqNo.Visible = false;
                }
                if (comboBoxnFunc.Text == "彙總(依股票代號)")
                {
                    // 起始日年月日
                    labelbstrStartDate.Visible = true;
                    textBoxbstrStartDate.Visible = true;
                    textBoxbstrStartDate.Text = "1121201";//民國YYYMMDD
                    // 結束日年月日
                    labelbstrEndDate.Visible = true;
                    textBoxbstrEndDate.Visible = true;
                    textBoxbstrEndDate.Text = "1121231";//民國YYYMMDD
                }
                else
                {
                    // 起始日年月日
                    labelbstrStartDate.Visible = false;
                    textBoxbstrStartDate.Visible = false;
                    // 結束日年月日
                    labelbstrEndDate.Visible = false;
                    textBoxbstrEndDate.Visible = false;
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
            //證券即時庫存。透過呼叫GetRealBalanceReport後，資訊由該事件回傳。
            m_pSKOrder.OnRealBalanceReport += new _ISKOrderLibEvents_OnRealBalanceReportEventHandler(OnRealBalanceReport);
            void OnRealBalanceReport(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "970") // 970,Mtalk發生錯誤 (後台的問題)
                {
                    dataGridViewGetRealBalanceReport.Rows.Add(bstrData);
                }
                else
                {
                    dataGridViewGetRealBalanceReport.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17]);
                }
            }
            //集保庫存查詢。透過呼叫 GetBalanceQuery後，資訊由該事件回傳。
            m_pSKOrder.OnBalanceQuery += new _ISKOrderLibEvents_OnBalanceQueryEventHandler(OnBalanceQuery);
            void OnBalanceQuery(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "M003") // 查無資料
                {
                    dataGridViewGetBalanceQuery.Rows.Add(bstrData + "查無資料");
                }
                else
                {
                    dataGridViewGetBalanceQuery.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8]);
                }
            }
            //資券配額查詢。透過呼叫 GetMarginPurchaseAmountLimit後，資訊由該事件回傳
            m_pSKOrder.OnMarginPurchaseAmountLimit += new _ISKOrderLibEvents_OnMarginPurchaseAmountLimitEventHandler(OnMarginPurchaseAmountLimit);
            void OnMarginPurchaseAmountLimit(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "M000") // 成功
                {
                    //dataGridViewOnOverSeaFutureRight.Rows.Add(bstrData);
                }
                else
                {
                    dataGridViewOnMarginPurchaseAmountLimit.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21]);
                }
            }
            //證券新損益查詢結果。透過呼叫 GetProfitLossGWReport 後，資訊由該事件回傳
            m_pSKOrder.OnProfitLossGWReport += new _ISKOrderLibEvents_OnProfitLossGWReportEventHandler(OnProfitLossGWReport);
            void OnProfitLossGWReport(string bstrData)
            {
                // 使用 Split 方法將字串拆分成陣列
                string[] values = bstrData.Split(',');
                if (values[0] == "000") // 回傳成功
                {
                    // DO NOTHING
                }
                else
                {
                    if (comboBoxnTPQueryType.Text == "未實現損益")
                    {
                        if (comboBoxnFunc.Text == "彙總") dataGridViewOnProfitLossGWReport1.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29]);
                        else if (comboBoxnFunc.Text == "明細") dataGridViewOnProfitLossGWReport2.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24]);
                    }
                    else if (comboBoxnTPQueryType.Text == "已實現損益")
                    {
                        if (comboBoxnFunc.Text == "彙總") dataGridViewOnProfitLossGWReport3.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17]);
                        else if (comboBoxnFunc.Text == "明細") dataGridViewOnProfitLossGWReport4.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20]);
                        else if (comboBoxnFunc.Text == "投資總額") dataGridViewOnProfitLossGWReport5.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8]);
                        else if (comboBoxnFunc.Text == "彙總(依股票代號)") dataGridViewOnProfitLossGWReport6.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9]);
                    }
                    else if (comboBoxnTPQueryType.Text == "現股當沖")
                    {
                        if (comboBoxnFunc.Text == "彙總") dataGridViewOnProfitLossGWReport7.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28]);
                        else if (comboBoxnFunc.Text == "明細") dataGridViewOnProfitLossGWReport8.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14]);
                    }
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

        private void buttonGetRealBalanceReport_Click(object sender, EventArgs e)
        {
            dataGridViewGetRealBalanceReport.Rows.Clear(); // 每次查詢前，清除上一次查詢結果

            // 查詢證券即時庫存內容
            string bstrAccount = comboBoxAccount.Text;
            int nCode = m_pSKOrder.GetRealBalanceReport(comboBoxUserID.Text, bstrAccount);
            // 取得回傳訊息
            string msg = "【GetRealBalanceReport】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void ButtonGetAvgCost_Click(object sender, EventArgs e)
        {
            dataGridViewGetAvgCost.Rows.Clear(); // 每次查詢前，清除上一次查詢結果

            SKAVGCOST pGWQuery = new SKAVGCOST();

            string bstrLogInID = comboBoxUserID.Text; // 登入ID 

            // 查詢昨日未沖銷明細
            pGWQuery.bstrAccount = comboBoxAccount.Text;
            pGWQuery.bstrStockNo = textBoxGetAvgCostStockNo.Text;//空白:所有股票代號

            pGWQuery.bstrFunc = "0"; //0:查詢昨日未沖銷明細

            string bstrMessage = "";

            int nCode = m_pSKOrder.GetAvgCost(bstrLogInID, pGWQuery, out bstrMessage);
            string msg = "【GetAvgCostData】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            string[] strValues = bstrMessage.Split('#');

            foreach (string strValue in strValues)
            {
                string[] values = strValue.Split(',');

                if(values[0] == "M003") //查無資料
                {
                    dataGridViewGetAvgCost.Rows.Add(strValue + "查無資料");
                }
                else if(values[0] == "M999")
                {
                    dataGridViewGetAvgCost.Rows.Add(strValue);
                }
                else if (values[0] != "") 
                {
                    dataGridViewGetAvgCost.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12]);
                }
            }

        }
    }
}
