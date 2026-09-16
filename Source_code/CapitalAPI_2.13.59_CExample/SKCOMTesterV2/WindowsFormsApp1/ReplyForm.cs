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
    public partial class ReplyForm : Form
    {
        // 關閉標誌，關閉From後，不讓事件將訊息傳遞至控件上
        bool isClosing = false;
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
        SKReplyLib m_pSKReply = new SKReplyLib(); // 回報物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        // 存[UserID]對應 交易帳號
        Dictionary<string, List<string>> m_dictUserID = new Dictionary<string, List<string>>();
        List<string> allkeys; // UserID
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
        public ReplyForm()
        {
            //Init
            {
                InitializeComponent();
                //dataGridView
                {
                    // 一般回報
                    {
                        //dataGridViewTS
                        {
                            dataGridViewTS.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTS.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTS.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTS.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTS.Columns.Add("Column5", "交易帳號");
                            dataGridViewTS.Columns.Add("Column6", "證逐筆");
                            dataGridViewTS.Columns.Add("Column7", "商品代碼");
                            dataGridViewTS.Columns.Add("Column8", "委託書號");
                            dataGridViewTS.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewTS.Columns.Add("Column10", "股數/口數");

                            dataGridViewTS.Columns.Add("Column11", "異動前量");
                            dataGridViewTS.Columns.Add("Column12", "異動後量");
                            dataGridViewTS.Columns.Add("Column13", "交易日");
                            dataGridViewTS.Columns.Add("Column14", "交易時間");
                            dataGridViewTS.Columns.Add("Column15", "子帳帳號");
                            dataGridViewTS.Columns.Add("Column16", "營業員編號");
                            dataGridViewTS.Columns.Add("Column17", "委託介面");
                            dataGridViewTS.Columns.Add("Column18", "回報流水號");
                            dataGridViewTS.Columns.Add("Column19", "成交序號");
                            dataGridViewTS.Columns.Add("Column20", "有效委託日");

                            dataGridViewTS.Columns.Add("Column21", "委託單錯誤訊息");
                            dataGridViewTS.Columns.Add("Column22", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewTS.Columns.Add("Column23", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTS.Columns.Add("Column24", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewTA
                        {
                            dataGridViewTA.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTA.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTA.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTA.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTA.Columns.Add("Column5", "交易帳號");
                            dataGridViewTA.Columns.Add("Column6", "證逐筆");
                            dataGridViewTA.Columns.Add("Column7", "商品代碼");
                            dataGridViewTA.Columns.Add("Column8", "委託書號");
                            dataGridViewTA.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewTA.Columns.Add("Column10", "股數/口數");

                            dataGridViewTA.Columns.Add("Column11", "交易日");
                            dataGridViewTA.Columns.Add("Column12", "交易時間");
                            dataGridViewTA.Columns.Add("Column13", "子帳帳號");
                            dataGridViewTA.Columns.Add("Column14", "營業員編號");
                            dataGridViewTA.Columns.Add("Column15", "委託介面");
                            dataGridViewTA.Columns.Add("Column16", "回報流水號");
                            dataGridViewTA.Columns.Add("Column17", "成交序號");
                            dataGridViewTA.Columns.Add("Column18", "有效委託日");
                            dataGridViewTA.Columns.Add("Column19", "委託單錯誤訊息");
                            dataGridViewTA.Columns.Add("Column20", "交易所動態退單代碼(E:交易所動態退單)");

                            dataGridViewTA.Columns.Add("Column21", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTA.Columns.Add("Column22", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewTL
                        {
                            dataGridViewTL.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTL.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTL.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTL.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTL.Columns.Add("Column5", "交易帳號");
                            dataGridViewTL.Columns.Add("Column6", "證逐筆");
                            dataGridViewTL.Columns.Add("Column7", "商品代碼");
                            dataGridViewTL.Columns.Add("Column8", "委託書號");
                            dataGridViewTL.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewTL.Columns.Add("Column10", "股數/口數");

                            dataGridViewTL.Columns.Add("Column11", "交易日");
                            dataGridViewTL.Columns.Add("Column12", "交易時間");
                            dataGridViewTL.Columns.Add("Column13", "子帳帳號");
                            dataGridViewTL.Columns.Add("Column14", "營業員編號");
                            dataGridViewTL.Columns.Add("Column15", "委託介面");
                            dataGridViewTL.Columns.Add("Column16", "回報流水號");
                            dataGridViewTL.Columns.Add("Column17", "成交序號");
                            dataGridViewTL.Columns.Add("Column18", "有效委託日");
                            dataGridViewTL.Columns.Add("Column19", "委託單錯誤訊息");
                            dataGridViewTL.Columns.Add("Column20", "交易所動態退單代碼(E:交易所動態退單)");

                            dataGridViewTL.Columns.Add("Column21", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTL.Columns.Add("Column22", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewTP
                        {
                            dataGridViewTP.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTP.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTP.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTP.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTP.Columns.Add("Column5", "交易帳號");
                            dataGridViewTP.Columns.Add("Column6", "證逐筆");
                            dataGridViewTP.Columns.Add("Column7", "商品代碼");
                            dataGridViewTP.Columns.Add("Column8", "委託書號");
                            dataGridViewTP.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewTP.Columns.Add("Column10", "股數/口數");

                            dataGridViewTP.Columns.Add("Column11", "交易日");
                            dataGridViewTP.Columns.Add("Column12", "交易時間");
                            dataGridViewTP.Columns.Add("Column13", "子帳帳號");
                            dataGridViewTP.Columns.Add("Column14", "營業員編號");
                            dataGridViewTP.Columns.Add("Column15", "委託介面");
                            dataGridViewTP.Columns.Add("Column16", "回報流水號");
                            dataGridViewTP.Columns.Add("Column17", "成交序號");
                            dataGridViewTP.Columns.Add("Column18", "有效委託日");
                            dataGridViewTP.Columns.Add("Column19", "委託單錯誤訊息");
                            dataGridViewTP.Columns.Add("Column20", "交易所動態退單代碼(E:交易所動態退單)");

                            dataGridViewTP.Columns.Add("Column21", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTP.Columns.Add("Column22", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewTC
                        {
                            dataGridViewTC.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTC.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTC.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTC.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTC.Columns.Add("Column5", "交易帳號");
                            dataGridViewTC.Columns.Add("Column6", "證逐筆");
                            dataGridViewTC.Columns.Add("Column7", "商品代碼");
                            dataGridViewTC.Columns.Add("Column8", "委託書號");
                            dataGridViewTC.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewTC.Columns.Add("Column10", "股數/口數");

                            dataGridViewTC.Columns.Add("Column11", "交易日");
                            dataGridViewTC.Columns.Add("Column12", "交易時間");
                            dataGridViewTC.Columns.Add("Column13", "子帳帳號");
                            dataGridViewTC.Columns.Add("Column14", "營業員編號");
                            dataGridViewTC.Columns.Add("Column15", "委託介面");
                            dataGridViewTC.Columns.Add("Column16", "回報流水號");
                            dataGridViewTC.Columns.Add("Column17", "成交序號");
                            dataGridViewTC.Columns.Add("Column18", "有效委託日");
                            dataGridViewTC.Columns.Add("Column19", "委託單錯誤訊息");
                            dataGridViewTC.Columns.Add("Column20", "交易所動態退單代碼(E:交易所動態退單)");

                            dataGridViewTC.Columns.Add("Column21", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTC.Columns.Add("Column22", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewTF
                        {
                            dataGridViewTF.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTF.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTF.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTF.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTF.Columns.Add("Column5", "交易帳號");
                            dataGridViewTF.Columns.Add("Column6", "證逐筆");
                            dataGridViewTF.Columns.Add("Column7", "商品代碼");
                            dataGridViewTF.Columns.Add("Column8", "委託書號");
                            dataGridViewTF.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewTF.Columns.Add("Column10", "第一腳成交價");

                            dataGridViewTF.Columns.Add("Column11", "第二腳成交價");
                            dataGridViewTF.Columns.Add("Column12", "第二腳觸發價分子");
                            dataGridViewTF.Columns.Add("Column13", "第二腳觸發價分母");
                            dataGridViewTF.Columns.Add("Column14", "股數/口數");
                            dataGridViewTF.Columns.Add("Column15", "交易日");
                            dataGridViewTF.Columns.Add("Column16", "交易時間");
                            dataGridViewTF.Columns.Add("Column17", "子帳帳號");
                            dataGridViewTF.Columns.Add("Column18", "營業員編號");
                            dataGridViewTF.Columns.Add("Column19", "委託介面");
                            dataGridViewTF.Columns.Add("Column20", "回報流水號");

                            dataGridViewTF.Columns.Add("Column21", "A:盤中單 B:預約單");
                            dataGridViewTF.Columns.Add("Column22", "第一腳商品代碼");
                            dataGridViewTF.Columns.Add("Column23", "第一腳商品結算年月");
                            dataGridViewTF.Columns.Add("Column24", "第二腳商品代碼");
                            dataGridViewTF.Columns.Add("Column25", "第二腳商品結算年月");
                            dataGridViewTF.Columns.Add("Column26", "成交序號");
                            dataGridViewTF.Columns.Add("Column27", "下單期標");
                            dataGridViewTF.Columns.Add("Column28", "盤別A：T盤  B：T+1盤");
                            dataGridViewTF.Columns.Add("Column29", "有效委託日");
                            dataGridViewTF.Columns.Add("Column30", "委託單錯誤訊息");

                            dataGridViewTF.Columns.Add("Column31", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewTF.Columns.Add("Column32", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTF.Columns.Add("Column33", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewTO
                        {
                            dataGridViewTO.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewTO.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewTO.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewTO.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewTO.Columns.Add("Column5", "交易帳號");
                            dataGridViewTO.Columns.Add("Column6", "證逐筆");
                            dataGridViewTO.Columns.Add("Column7", "商品代碼");
                            dataGridViewTO.Columns.Add("Column8", "履約價");
                            dataGridViewTO.Columns.Add("Column9", "委託書號");
                            dataGridViewTO.Columns.Add("Column10", "價格(N:「委託」為委託價；D:「成交」為成交價)");

                            dataGridViewTO.Columns.Add("Column11", "第一腳成交價");
                            dataGridViewTO.Columns.Add("Column12", "第二腳成交價");
                            dataGridViewTO.Columns.Add("Column13", "第二腳觸發價分子");
                            dataGridViewTO.Columns.Add("Column14", "第二腳觸發價分母");
                            dataGridViewTO.Columns.Add("Column15", "股數/口數");
                            dataGridViewTO.Columns.Add("Column16", "交易日");
                            dataGridViewTO.Columns.Add("Column17", "交易時間");
                            dataGridViewTO.Columns.Add("Column18", "子帳帳號");
                            dataGridViewTO.Columns.Add("Column19", "營業員編號");
                            dataGridViewTO.Columns.Add("Column20", "委託介面");

                            dataGridViewTO.Columns.Add("Column21", "回報流水號");
                            dataGridViewTO.Columns.Add("Column22", "A:盤中單 B:預約單");
                            dataGridViewTO.Columns.Add("Column23", "第一腳商品代碼");
                            dataGridViewTO.Columns.Add("Column24", "第一腳商品結算年月");
                            dataGridViewTO.Columns.Add("Column25", "第一腳商品履約價");
                            dataGridViewTO.Columns.Add("Column26", "第二腳商品代碼");
                            dataGridViewTO.Columns.Add("Column27", "第二腳商品結算年月");
                            dataGridViewTO.Columns.Add("Column28", "第二腳商品履約價");
                            dataGridViewTO.Columns.Add("Column29", "成交序號");
                            dataGridViewTO.Columns.Add("Column30", "下單期標");

                            dataGridViewTO.Columns.Add("Column31", "盤別A：T盤  B：T+1盤");
                            dataGridViewTO.Columns.Add("Column32", "有效委託日");
                            dataGridViewTO.Columns.Add("Column33", "選擇權類型C：Call　P：Put");
                            dataGridViewTO.Columns.Add("Column34", "委託單錯誤訊息");
                            dataGridViewTO.Columns.Add("Column35", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewTO.Columns.Add("Column36", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewTO.Columns.Add("Column37", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewOF
                        {
                            dataGridViewOF.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewOF.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewOF.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewOF.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewOF.Columns.Add("Column5", "交易帳號");
                            dataGridViewOF.Columns.Add("Column6", "證逐筆");
                            dataGridViewOF.Columns.Add("Column7", "商品代碼");
                            dataGridViewOF.Columns.Add("Column8", "委託書號");
                            dataGridViewOF.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewOF.Columns.Add("Column10", "分子");

                            dataGridViewOF.Columns.Add("Column11", "分母");
                            dataGridViewOF.Columns.Add("Column12", "觸發價格");
                            dataGridViewOF.Columns.Add("Column13", "第一腳觸發價格分子");
                            dataGridViewOF.Columns.Add("Column14", "觸發價格分母");
                            dataGridViewOF.Columns.Add("Column15", "第二腳成交價");
                            dataGridViewOF.Columns.Add("Column16", "第二腳觸發價分子");
                            dataGridViewOF.Columns.Add("Column17", "第二腳觸發價分母");
                            dataGridViewOF.Columns.Add("Column18", "股數/口數");
                            dataGridViewOF.Columns.Add("Column19", "交易日");
                            dataGridViewOF.Columns.Add("Column20", "交易時間");

                            dataGridViewOF.Columns.Add("Column21", "子帳帳號");
                            dataGridViewOF.Columns.Add("Column22", "營業員編號");
                            dataGridViewOF.Columns.Add("Column23", "委託介面");
                            dataGridViewOF.Columns.Add("Column24", "回報流水號");
                            dataGridViewOF.Columns.Add("Column25", "第一腳商品代碼");
                            dataGridViewOF.Columns.Add("Column26", "第一腳商品結算年月");
                            dataGridViewOF.Columns.Add("Column27", "第二腳商品代碼");
                            dataGridViewOF.Columns.Add("Column28", "第二腳商品結算年月");
                            dataGridViewOF.Columns.Add("Column29", "成交序號");
                            dataGridViewOF.Columns.Add("Column30", "下單期標");

                            dataGridViewOF.Columns.Add("Column31", "有效委託日");
                            dataGridViewOF.Columns.Add("Column32", "上手單號");
                            dataGridViewOF.Columns.Add("Column33", "委託單錯誤訊息");
                            dataGridViewOF.Columns.Add("Column34", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewOF.Columns.Add("Column35", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewOF.Columns.Add("Column36", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewOO
                        {
                            dataGridViewOO.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewOO.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewOO.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewOO.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewOO.Columns.Add("Column5", "交易帳號");
                            dataGridViewOO.Columns.Add("Column6", "證逐筆");
                            dataGridViewOO.Columns.Add("Column7", "商品代碼");
                            dataGridViewOO.Columns.Add("Column8", "履約價");
                            dataGridViewOO.Columns.Add("Column9", "委託書號");
                            dataGridViewOO.Columns.Add("Column10", "價格(N:「委託」為委託價；D:「成交」為成交價)");

                            dataGridViewOO.Columns.Add("Column11", "分子");
                            dataGridViewOO.Columns.Add("Column12", "分母");
                            dataGridViewOO.Columns.Add("Column13", "觸發價格");
                            dataGridViewOO.Columns.Add("Column14", "第一腳觸發價格分子");
                            dataGridViewOO.Columns.Add("Column15", "觸發價格分母");
                            dataGridViewOO.Columns.Add("Column16", "第二腳成交價");
                            dataGridViewOO.Columns.Add("Column17", "第二腳觸發價分子");
                            dataGridViewOO.Columns.Add("Column18", "第二腳觸發價分母");
                            dataGridViewOO.Columns.Add("Column19", "股數/口數");
                            dataGridViewOO.Columns.Add("Column20", "交易日");

                            dataGridViewOO.Columns.Add("Column21", "交易時間");
                            dataGridViewOO.Columns.Add("Column22", "子帳帳號");
                            dataGridViewOO.Columns.Add("Column23", "營業員編號");
                            dataGridViewOO.Columns.Add("Column24", "委託介面");
                            dataGridViewOO.Columns.Add("Column25", "回報流水號");
                            dataGridViewOO.Columns.Add("Column26", "第一腳商品代碼");
                            dataGridViewOO.Columns.Add("Column27", "第一腳商品結算年月");
                            dataGridViewOO.Columns.Add("Column28", "第一腳商品履約價");
                            dataGridViewOO.Columns.Add("Column29", "第二腳商品代碼");
                            dataGridViewOO.Columns.Add("Column30", "第二腳商品結算年月");

                            dataGridViewOO.Columns.Add("Column31", "第二腳商品履約價");
                            dataGridViewOO.Columns.Add("Column32", "成交序號");
                            dataGridViewOO.Columns.Add("Column33", "下單期標");
                            dataGridViewOO.Columns.Add("Column34", "有效委託日");
                            dataGridViewOO.Columns.Add("Column35", "選擇權類型C：Call　P：Put");
                            dataGridViewOO.Columns.Add("Column36", "上手單號");
                            dataGridViewOO.Columns.Add("Column37", "委託單錯誤訊息");
                            dataGridViewOO.Columns.Add("Column38", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewOO.Columns.Add("Column39", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewOO.Columns.Add("Column40", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewOS
                        {
                            dataGridViewOS.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewOS.Columns.Add("Column2", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewOS.Columns.Add("Column3", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewOS.Columns.Add("Column4", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewOS.Columns.Add("Column5", "交易帳號");
                            dataGridViewOS.Columns.Add("Column6", "證逐筆");
                            dataGridViewOS.Columns.Add("Column7", "商品代碼");
                            dataGridViewOS.Columns.Add("Column8", "委託書號");
                            dataGridViewOS.Columns.Add("Column9", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewOS.Columns.Add("Column10", "股數/口數");

                            dataGridViewOS.Columns.Add("Column11", "異動前量");
                            dataGridViewOS.Columns.Add("Column12", "異動後量");
                            dataGridViewOS.Columns.Add("Column13", "交易日");
                            dataGridViewOS.Columns.Add("Column14", "交易時間");
                            dataGridViewOS.Columns.Add("Column15", "子帳帳號");
                            dataGridViewOS.Columns.Add("Column16", "營業員編號");
                            dataGridViewOS.Columns.Add("Column17", "委託介面");
                            dataGridViewOS.Columns.Add("Column18", "回報流水號");
                            dataGridViewOS.Columns.Add("Column19", "成交序號");
                            dataGridViewOS.Columns.Add("Column20", "有效委託日");

                            dataGridViewOS.Columns.Add("Column21", "委託單錯誤訊息");
                            dataGridViewOS.Columns.Add("Column22", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewOS.Columns.Add("Column23", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewOS.Columns.Add("Column24", "13碼序號(成交單含IOC/FOK產生取消單)");
                        }
                        //dataGridViewNoClass
                        {
                            dataGridViewNoClass.Columns.Add("Column1", "原始13碼委託序號");
                            dataGridViewNoClass.Columns.Add("Column2", "市場種類(TS:證券 TA:盤後 TL:零股 TP:興櫃TC: 盤中零股TF:期貨 TO:選擇權OF:海期OO:海選 OS:複委託)");
                            dataGridViewNoClass.Columns.Add("Column3", "種類(N:委託 C:取消 U:改量 P:改價D:成交 B:改價改量S:動態退單)");
                            dataGridViewNoClass.Columns.Add("Column4", "OrderErr(Y:失敗 T:逾時 N:正常)");
                            dataGridViewNoClass.Columns.Add("Column5", "Broker(TS,TA,TL,TP: 分公司代號 unit noTF,TO: IB 代號 broker id)");
                            dataGridViewNoClass.Columns.Add("Column6", "交易帳號");
                            dataGridViewNoClass.Columns.Add("Column7", "證逐筆");
                            dataGridViewNoClass.Columns.Add("Column8", "交易所");
                            dataGridViewNoClass.Columns.Add("Column9", "商品代碼");
                            dataGridViewNoClass.Columns.Add("Column10", "履約價");

                            dataGridViewNoClass.Columns.Add("Column11", "委託書號");
                            dataGridViewNoClass.Columns.Add("Column12", "價格(N:「委託」為委託價；D:「成交」為成交價)");
                            dataGridViewNoClass.Columns.Add("Column13", "海期(分子)");
                            dataGridViewNoClass.Columns.Add("Column14", "海期(分母)");
                            dataGridViewNoClass.Columns.Add("Column15", "海期(觸發價)/第一腳成交價");
                            dataGridViewNoClass.Columns.Add("Column16", "海期(第一腳觸發價分子)");
                            dataGridViewNoClass.Columns.Add("Column17", "海期(第一腳觸發價分母)");
                            dataGridViewNoClass.Columns.Add("Column18", "期選(第二腳成交價)");
                            dataGridViewNoClass.Columns.Add("Column19", "第二腳觸發價分子");
                            dataGridViewNoClass.Columns.Add("Column20", "第二腳觸發價分母");

                            dataGridViewNoClass.Columns.Add("Column21", "股數/口數");
                            dataGridViewNoClass.Columns.Add("Column22", "異動前量");
                            dataGridViewNoClass.Columns.Add("Column23", "異動後量");
                            dataGridViewNoClass.Columns.Add("Column24", "交易日");
                            dataGridViewNoClass.Columns.Add("Column25", "交易時間");
                            dataGridViewNoClass.Columns.Add("Column26", "成交序號(請以ExecutionNo為主)");
                            dataGridViewNoClass.Columns.Add("Column27", "子帳帳號");
                            dataGridViewNoClass.Columns.Add("Column28", "營業員編號");
                            dataGridViewNoClass.Columns.Add("Column29", "委託介面");
                            dataGridViewNoClass.Columns.Add("Column30", "委託日期");

                            dataGridViewNoClass.Columns.Add("Column31", "回報流水號");
                            dataGridViewNoClass.Columns.Add("Column32", "A:盤中單 B:預約單");
                            dataGridViewNoClass.Columns.Add("Column33", "第一腳商品代碼");
                            dataGridViewNoClass.Columns.Add("Column34", "第一腳商品結算年月");
                            dataGridViewNoClass.Columns.Add("Column35", "第一腳商品履約價");
                            dataGridViewNoClass.Columns.Add("Column36", "第二腳商品代碼");
                            dataGridViewNoClass.Columns.Add("Column37", "第二腳商品結算年月");
                            dataGridViewNoClass.Columns.Add("Column38", "第二腳商品履約價");
                            dataGridViewNoClass.Columns.Add("Column39", "成交序號(ExecutionNo)");
                            dataGridViewNoClass.Columns.Add("Column40", "下單期標");

                            dataGridViewNoClass.Columns.Add("Column41", "盤別A：T盤  B：T+1盤");
                            dataGridViewNoClass.Columns.Add("Column42", "有效委託日");
                            dataGridViewNoClass.Columns.Add("Column43", "選擇權類型C：Call　P：Put");
                            dataGridViewNoClass.Columns.Add("Column44", "上手單號");
                            dataGridViewNoClass.Columns.Add("Column45", "委託單錯誤訊息");
                            dataGridViewNoClass.Columns.Add("Column46", "交易所動態退單代碼(E:交易所動態退單)");
                            dataGridViewNoClass.Columns.Add("Column47", "交易所或後台退單訊息([00]:2碼數字,交易所回應代碼及訊息;[000]:3碼數字,交易後台代碼及訊息;[D]委託成功後,由交易所主動退單及退單原因)");
                            dataGridViewNoClass.Columns.Add("Column48", "13碼序號(成交單含IOC/FOK產生取消單)");
                            dataGridViewNoClass.Columns.Add("Column49", "[海期][停損限價/停損市價][已觸發][委託回報]海期停損單觸發註記 :Y");
                        }
                    }
                    // 智慧單回報
                    {
                        // 證券
                        {
                            //dataGridViewTSMST
                            {
                                //dataGridViewTSMST 證券(共用欄位)
                                {
                                    dataGridViewTSMST.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSMST.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSMST.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSMST.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSMST.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSMST.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSMST.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSMST.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSMST.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSMST.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSMST.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSMST.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSMST.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSMST.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSMST.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSMST.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSMST.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSMST.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSMST.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSMST.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSMST.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSMST.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSMST.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSMST.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSMST.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSMST.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSMST.Columns.Add("Column27", "來源別");
                                    dataGridViewTSMST.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSMST.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSMST.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSMST.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSMST.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSMST ONLY
                                {
                                    dataGridViewTSMST2.Columns.Add("Column33", "MST:移動點數"); // 接續共用欄位 33
                                    dataGridViewTSMST2.Columns.Add("Column34", "MST:觸價基準");
                                    dataGridViewTSMST2.Columns.Add("Column35", "MST:當前市價");
                                    dataGridViewTSMST2.Columns.Add("Column36", "前一個觸發價格");
                                    dataGridViewTSMST2.Columns.Add("Column37", "啟動方式[0:由市價啟動 1: 自訂價格啟動]");
                                    dataGridViewTSMST2.Columns.Add("Column38", "[自訂適用]啟動價格");
                                    dataGridViewTSMST2.Columns.Add("Column39", "[自訂適用]觸價方向 0:none 1: 大於等於 2:小於等於");
                                    dataGridViewTSMST2.Columns.Add("Column40", "長效單註記");
                                    dataGridViewTSMST2.Columns.Add("Column41", "長效單序號");

                                    dataGridViewTSMST2.Columns.Add("Column42", "長效單結束日期");
                                    dataGridViewTSMST2.Columns.Add("Column43", "是否觸發即停止(預設為true)0:條件觸發後，該筆長效單功能即失效 1:不管有無觸發，每日都送一次條件單，直到長效單結束日後才失效");
                                    dataGridViewTSMST2.Columns.Add("Column44", "長效單類別 0：None 1：效期內觸發即失效 2：[停用]長效單結束日失效 3：效期內完全成交即失效");
                                    dataGridViewTSMST2.Columns.Add("Column45", "長效單委託總量");
                                    dataGridViewTSMST2.Columns.Add("Column46", "長效單成交總量");
                                }
                            }
                            //dataGridViewTSMIOC
                            {
                                //dataGridViewTSMIOC 證券(共用欄位)
                                {
                                    dataGridViewTSMIOC.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSMIOC.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSMIOC.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSMIOC.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSMIOC.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSMIOC.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSMIOC.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSMIOC.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSMIOC.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSMIOC.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSMIOC.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSMIOC.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSMIOC.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSMIOC.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSMIOC.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSMIOC.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSMIOC.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSMIOC.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSMIOC.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSMIOC.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSMIOC.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSMIOC.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSMIOC.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSMIOC.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSMIOC.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSMIOC.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSMIOC.Columns.Add("Column27", "來源別");
                                    dataGridViewTSMIOC.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSMIOC.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSMIOC.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSMIOC.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSMIOC.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSMIOC ONLY
                                {
                                    dataGridViewTSMIOC2.Columns.Add("Column33", "委託價上限"); // 接續共用欄位 33
                                    dataGridViewTSMIOC2.Columns.Add("Column34", "委託價下限");
                                    dataGridViewTSMIOC2.Columns.Add("Column35", "成交數量");
                                    dataGridViewTSMIOC2.Columns.Add("Column36", "單次委託量上限");
                                    dataGridViewTSMIOC2.Columns.Add("Column37", "總委託量");
                                    dataGridViewTSMIOC2.Columns.Add("Column38", "開始時間");
                                }
                            }
                            //dataGridViewTSMIT
                            {
                                //dataGridViewTSMIT 證券(共用欄位)
                                {
                                    dataGridViewTSMIT.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSMIT.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSMIT.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSMIT.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSMIT.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSMIT.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSMIT.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSMIT.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSMIT.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSMIT.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSMIT.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSMIT.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSMIT.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSMIT.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSMIT.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSMIT.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSMIT.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSMIT.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSMIT.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSMIT.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSMIT.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSMIT.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSMIT.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSMIT.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSMIT.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSMIT.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSMIT.Columns.Add("Column27", "來源別");
                                    dataGridViewTSMIT.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSMIT.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSMIT.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSMIT.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSMIT.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSMIT ONLY
                                {
                                    dataGridViewTSMIT2.Columns.Add("Column33", "當前市價，若下單未填則為0"); // 接續共用欄位 33
                                    dataGridViewTSMIT2.Columns.Add("Column34", "成交價觸發價格");
                                    dataGridViewTSMIT2.Columns.Add("Column35", "預風控註記");
                                    dataGridViewTSMIT2.Columns.Add("Column36", "拆單註記");
                                    dataGridViewTSMIT2.Columns.Add("Column37", "長效單註記");
                                    dataGridViewTSMIT2.Columns.Add("Column38", "長效單序號");
                                    dataGridViewTSMIT2.Columns.Add("Column39", "長效單結束日期");

                                    dataGridViewTSMIT2.Columns.Add("Column40", "是否觸發即停止(預設為true)0:條件觸發後，該筆長效單功能即失效 1:不管有無觸發，每日都送一次條件單，直到長效單結束日後才失效");
                                    dataGridViewTSMIT2.Columns.Add("Column41", "長效單類別 0：None 1：效期內觸發即失效 2：[停用]長效單結束日失效 3：效期內完全成交即失效");
                                    dataGridViewTSMIT2.Columns.Add("Column42", "長效單委託總量");
                                    dataGridViewTSMIT2.Columns.Add("Column43", "長效單成交總量");
                                }
                            }
                            //dataGridViewTSDayTrading
                            {
                                //dataGridViewTSDayTrading 證券(共用欄位)
                                {
                                    dataGridViewTSDayTrading.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSDayTrading.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSDayTrading.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSDayTrading.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSDayTrading.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSDayTrading.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSDayTrading.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSDayTrading.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSDayTrading.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSDayTrading.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSDayTrading.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSDayTrading.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSDayTrading.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSDayTrading.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSDayTrading.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSDayTrading.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSDayTrading.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSDayTrading.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSDayTrading.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSDayTrading.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSDayTrading.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSDayTrading.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSDayTrading.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSDayTrading.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSDayTrading.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSDayTrading.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSDayTrading.Columns.Add("Column27", "來源別");
                                    dataGridViewTSDayTrading.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSDayTrading.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSDayTrading.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSDayTrading.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSDayTrading.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSDayTrading ONLY
                                {
                                    dataGridViewTSDayTrading2.Columns.Add("Column33", "是否啟用MIT進場 0:否;1:是"); // 接續共用欄位 33
                                    dataGridViewTSDayTrading2.Columns.Add("Column34", "當下市價");
                                    dataGridViewTSDayTrading2.Columns.Add("Column35", "出清方式17:只勾選時間出清條件18:GTE(條件一)19:LTE(條件二)20:GTE_LTE(條件一+條件二)");
                                    dataGridViewTSDayTrading2.Columns.Add("Column36", "停利洗價模式1:指定價格;2:指定漲跌幅(%)");
                                    dataGridViewTSDayTrading2.Columns.Add("Column37", "停利指定漲跌幅");
                                    dataGridViewTSDayTrading2.Columns.Add("Column38", "停利觸發價");
                                    dataGridViewTSDayTrading2.Columns.Add("Column39", "停利委託方式1:市價 ;2:限價");
                                    dataGridViewTSDayTrading2.Columns.Add("Column40", "停利委託價");

                                    dataGridViewTSDayTrading2.Columns.Add("Column41", "停利委託時效 0:ROD當日有效 3:IOC 4:FOK");
                                    dataGridViewTSDayTrading2.Columns.Add("Column42", "停損洗價模式1:指定價格 ;2:指定漲跌幅(%)");
                                    dataGridViewTSDayTrading2.Columns.Add("Column43", "停損指定漲跌幅");
                                    dataGridViewTSDayTrading2.Columns.Add("Column44", "停損觸發價");
                                    dataGridViewTSDayTrading2.Columns.Add("Column45", "停損委託時效 0:ROD當日有效 3:IOC 4:FOK");
                                    dataGridViewTSDayTrading2.Columns.Add("Column46", "是否執行時間出清 0:否;1:是");
                                    dataGridViewTSDayTrading2.Columns.Add("Column47", "指定出清時間");
                                    dataGridViewTSDayTrading2.Columns.Add("Column48", "時間出清委託方式 1:市價;2:限價");
                                    dataGridViewTSDayTrading2.Columns.Add("Column49", "時間出清委託價");
                                    dataGridViewTSDayTrading2.Columns.Add("Column50", "時間出清委託時效 0:當日有效 3:IOC(立即成交否則取消) 4:FOK(全部成交否則取消)");

                                    dataGridViewTSDayTrading2.Columns.Add("Column51", "是否執行盤後定價0:否;1:是");
                                    dataGridViewTSDayTrading2.Columns.Add("Column52", "盤後定價執行時間");
                                }
                            }
                            //dataGridViewTSClearOut
                            {
                                //dataGridViewTSClearOut 證券(共用欄位)
                                {
                                    dataGridViewTSClearOut.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSClearOut.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSClearOut.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSClearOut.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSClearOut.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSClearOut.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSClearOut.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSClearOut.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSClearOut.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSClearOut.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSClearOut.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSClearOut.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSClearOut.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSClearOut.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSClearOut.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSClearOut.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSClearOut.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSClearOut.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSClearOut.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSClearOut.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSClearOut.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSClearOut.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSClearOut.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSClearOut.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSClearOut.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSClearOut.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSClearOut.Columns.Add("Column27", "來源別");
                                    dataGridViewTSClearOut.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSClearOut.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSClearOut.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSClearOut.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSClearOut.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSClearOut ONLY
                                {
                                    dataGridViewTSClearOut2.Columns.Add("Column33", "17:只勾選時間出清條件 18:GTE(條件一) 19:LTE(條件二) 20:GTE_LTE(條件一+條件二)"); // 接續共用欄位 33
                                    dataGridViewTSClearOut2.Columns.Add("Column34", "條件一觸發價");
                                    dataGridViewTSClearOut2.Columns.Add("Column35", "條件一委託方式 1:市價; 2:限價");
                                    dataGridViewTSClearOut2.Columns.Add("Column36", "條件一委託價");
                                    dataGridViewTSClearOut2.Columns.Add("Column37", "條件一委託時效 0:ROD;3:IOC;4:FOK");
                                    dataGridViewTSClearOut2.Columns.Add("Column38", "條件二觸發價");
                                    dataGridViewTSClearOut2.Columns.Add("Column39", "條件二委託方式 1:市價; 2:限價");
                                    dataGridViewTSClearOut2.Columns.Add("Column40", "條件二委託價");

                                    dataGridViewTSClearOut2.Columns.Add("Column41", "條件二委託時效 0:ROD;3:IOC;4:FOK");
                                    dataGridViewTSClearOut2.Columns.Add("Column42", "是否執行時間出清 0:否;1:是");
                                    dataGridViewTSClearOut2.Columns.Add("Column43", "指定出清時間");
                                    dataGridViewTSClearOut2.Columns.Add("Column44", "時間出清委託方式 1:市價; 2:限價");
                                    dataGridViewTSClearOut2.Columns.Add("Column45", "時間出清委託價");
                                    dataGridViewTSClearOut2.Columns.Add("Column46", "時間出清委託時效 0:ROD;3:IOC;4:FOK");
                                    dataGridViewTSClearOut2.Columns.Add("Column47", "是否執行盤後定價 0:否;1:是");
                                    dataGridViewTSClearOut2.Columns.Add("Column48", "盤後定價執行時間");
                                    dataGridViewTSClearOut2.Columns.Add("Column49", "條件一是否有觸發");
                                    dataGridViewTSClearOut2.Columns.Add("Column50", "條件一觸發價格");

                                    dataGridViewTSClearOut2.Columns.Add("Column51", "條件二是否有觸發");
                                    dataGridViewTSClearOut2.Columns.Add("Column52", "條件二觸發價格");
                                    dataGridViewTSClearOut2.Columns.Add("Column53", "時間出清是否有觸發");
                                    dataGridViewTSClearOut2.Columns.Add("Column54", "盤後定價是否有觸發");
                                    dataGridViewTSClearOut2.Columns.Add("Column55", "總委託量");
                                    dataGridViewTSClearOut2.Columns.Add("Column56", "已成交量");
                                    dataGridViewTSClearOut2.Columns.Add("Column57", "進場成交價");
                                }
                            }
                            //dataGridViewTSOCO
                            {
                                //dataGridViewTSOCO 證券(共用欄位)
                                {
                                    dataGridViewTSOCO.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSOCO.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSOCO.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSOCO.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSOCO.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSOCO.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSOCO.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSOCO.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSOCO.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSOCO.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSOCO.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSOCO.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSOCO.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSOCO.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSOCO.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSOCO.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSOCO.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSOCO.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSOCO.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSOCO.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSOCO.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSOCO.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSOCO.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSOCO.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSOCO.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSOCO.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSOCO.Columns.Add("Column27", "來源別");
                                    dataGridViewTSOCO.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSOCO.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSOCO.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSOCO.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSOCO.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSOCO ONLY
                                {
                                    dataGridViewTSOCO2.Columns.Add("Column33", "第一腳觸發價"); // 接續共用欄位 33
                                    dataGridViewTSOCO2.Columns.Add("Column34", "第二腳觸發價");
                                    dataGridViewTSOCO2.Columns.Add("Column35", "第二腳委託價");
                                    dataGridViewTSOCO2.Columns.Add("Column36", "第二腳委託價格類別 1：市價 2：限價");
                                    dataGridViewTSOCO2.Columns.Add("Column37", "第二腳委託時效 0:ROD;3:IOC;4:FOK");
                                    dataGridViewTSOCO2.Columns.Add("Column38", "[限證券]第二腳委託種類別 0：現股 3：(融)自資 4：(融)自券 8：無券普賣");
                                    dataGridViewTSOCO2.Columns.Add("Column39", "第二腳委託價格別");
                                    dataGridViewTSOCO2.Columns.Add("Column40", "0:前日收盤價 1:漲停價 2:跌停價 7使用者輸入價");
                                }
                            }
                            //dataGridViewTSAB
                            {
                                //dataGridViewTSAB 證券(共用欄位)
                                {
                                    dataGridViewTSAB.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSAB.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSAB.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSAB.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSAB.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSAB.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSAB.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSAB.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSAB.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSAB.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSAB.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSAB.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSAB.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSAB.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSAB.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSAB.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSAB.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSAB.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSAB.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSAB.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSAB.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSAB.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSAB.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSAB.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSAB.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSAB.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSAB.Columns.Add("Column27", "來源別");
                                    dataGridViewTSAB.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSAB.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSAB.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSAB.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSAB.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSAB ONLY
                                {
                                    dataGridViewTSAB2.Columns.Add("Column33", "成交價觸發價格"); // 接續共用欄位 33
                                }
                            }
                            //dataGridViewTSCB
                            {
                                //dataGridViewTSCB 證券(共用欄位)
                                {
                                    dataGridViewTSCB.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTSCB.Columns.Add("Column2", "0: 一般 1:零股 2:盤後");
                                    dataGridViewTSCB.Columns.Add("Column3", "智慧單(母單)序號");
                                    dataGridViewTSCB.Columns.Add("Column4", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTSCB.Columns.Add("Column5", "分公司代碼");
                                    dataGridViewTSCB.Columns.Add("Column6", "交易帳號");
                                    dataGridViewTSCB.Columns.Add("Column7", "子帳帳號");
                                    dataGridViewTSCB.Columns.Add("Column8", "交易所名稱");
                                    dataGridViewTSCB.Columns.Add("Column9", "13碼序號");
                                    dataGridViewTSCB.Columns.Add("Column10", "原始13碼序號");

                                    dataGridViewTSCB.Columns.Add("Column11", "委託書號");
                                    dataGridViewTSCB.Columns.Add("Column12", "商品代碼");
                                    dataGridViewTSCB.Columns.Add("Column13", "B: 買 S:賣");
                                    dataGridViewTSCB.Columns.Add("Column14", "委託種類別0：現股 3：自)融資4：自)融券 8：無券普賣");
                                    dataGridViewTSCB.Columns.Add("Column15", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTSCB.Columns.Add("Column16", "委託價格");
                                    dataGridViewTSCB.Columns.Add("Column17", "1市價;2限價;3:範圍市價");
                                    dataGridViewTSCB.Columns.Add("Column18", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTSCB.Columns.Add("Column19", "TS:張數; TF:口數");
                                    dataGridViewTSCB.Columns.Add("Column20", "觸發價");

                                    dataGridViewTSCB.Columns.Add("Column21", "觸發時間");
                                    dataGridViewTSCB.Columns.Add("Column22", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");
                                    dataGridViewTSCB.Columns.Add("Column23", "是否當沖 證券：(目前此欄無資料) 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTSCB.Columns.Add("Column24", "下單時間");
                                    dataGridViewTSCB.Columns.Add("Column25", "營業員代碼");
                                    dataGridViewTSCB.Columns.Add("Column26", "USER PC IP");
                                    dataGridViewTSCB.Columns.Add("Column27", "來源別");
                                    dataGridViewTSCB.Columns.Add("Column28", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTSCB.Columns.Add("Column29", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTSCB.Columns.Add("Column30", "訊息EX:[智慧單號][商品代碼]訊息內容");

                                    dataGridViewTSCB.Columns.Add("Column31", "更新時間");
                                    dataGridViewTSCB.Columns.Add("Column32", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTSCB ONLY
                                {
                                    dataGridViewTSCB2.Columns.Add("Column33", "是否為條件全部成立 AND: true OR: flase"); // 接續共用欄位 33
                                    dataGridViewTSCB2.Columns.Add("Column34", "是否為自訂啟動時間");
                                    dataGridViewTSCB2.Columns.Add("Column35", "自訂啟動時間");
                                    dataGridViewTSCB2.Columns.Add("Column36", "是否執行成交價");
                                    dataGridViewTSCB2.Columns.Add("Column37", "成交價");
                                    dataGridViewTSCB2.Columns.Add("Column38", "成交價的觸價方向 0：None 1：GTE 2：LTE");
                                    dataGridViewTSCB2.Columns.Add("Column39", "是否執行委買價");
                                    dataGridViewTSCB2.Columns.Add("Column40", "委買價");

                                    dataGridViewTSCB2.Columns.Add("Column41", "委買價的觸價方向 0：None 1：GTE 2：LTE");
                                    dataGridViewTSCB2.Columns.Add("Column42", "是否執行委賣價");
                                    dataGridViewTSCB2.Columns.Add("Column43", "委賣價");
                                    dataGridViewTSCB2.Columns.Add("Column44", "委賣價的觸價方向 0：None 1：GTE 2：LTE");
                                    dataGridViewTSCB2.Columns.Add("Column45", "是否執行漲跌tick");
                                    dataGridViewTSCB2.Columns.Add("Column46", "Tick數");
                                    dataGridViewTSCB2.Columns.Add("Column47", "漲跌tick的觸價方向 0：None 1：GTE 2：LTE");
                                    dataGridViewTSCB2.Columns.Add("Column48", "是否執行漲跌幅(%)");
                                    dataGridViewTSCB2.Columns.Add("Column49", "漲跌幅(%)");
                                    dataGridViewTSCB2.Columns.Add("Column50", "漲跌幅的觸價方向 0：None 1：GTE 2：LTE");
                                    
                                    dataGridViewTSCB2.Columns.Add("Column51", "是否執行總量");
                                    dataGridViewTSCB2.Columns.Add("Column52", "總量");
                                    dataGridViewTSCB2.Columns.Add("Column53", "總量的觸價方向 0：None 1：GTE 2：LTE");
                                    dataGridViewTSCB2.Columns.Add("Column54", "成交價觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column55", "委買價觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column56", "委賣價觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column57", "漲跌tick觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column58", "漲跌幅觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column59", "總量觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column60", "成交價觸發價格");

                                    dataGridViewTSCB2.Columns.Add("Column61", "委買價觸發價格");
                                    dataGridViewTSCB2.Columns.Add("Column62", "委賣價觸發價格");
                                    dataGridViewTSCB2.Columns.Add("Column63", "漲跌tick觸發tick");
                                    dataGridViewTSCB2.Columns.Add("Column64", "漲跌幅觸發幅度");
                                    dataGridViewTSCB2.Columns.Add("Column65", "總量觸發數量");
                                    dataGridViewTSCB2.Columns.Add("Column66", "是否執行單量");
                                    dataGridViewTSCB2.Columns.Add("Column67", "單量");
                                    dataGridViewTSCB2.Columns.Add("Column68", "單量的觸價方向 0：None 1：GTE 2：LTE");
                                    dataGridViewTSCB2.Columns.Add("Column69", "單量觸發記號 0：未觸發 1：觸發");
                                    dataGridViewTSCB2.Columns.Add("Column70", "單量觸發數量");
                                }
                            }
                            
                        }
                        // 期貨
                        {
                            //dataGridViewTFSTP
                            {
                                //dataGridViewTFSTP 期貨(共用欄位)
                                {
                                    dataGridViewTFSTP.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTFSTP.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewTFSTP.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTFSTP.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewTFSTP.Columns.Add("Column5", "交易帳號");
                                    dataGridViewTFSTP.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewTFSTP.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewTFSTP.Columns.Add("Column8", "13碼序號");
                                    dataGridViewTFSTP.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewTFSTP.Columns.Add("Column10", "委託書號");

                                    dataGridViewTFSTP.Columns.Add("Column11", "商品代碼");
                                    dataGridViewTFSTP.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewTFSTP.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTFSTP.Columns.Add("Column14", "委託價格");
                                    dataGridViewTFSTP.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewTFSTP.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTFSTP.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewTFSTP.Columns.Add("Column18", "觸發價");
                                    dataGridViewTFSTP.Columns.Add("Column19", "觸發時間");
                                    dataGridViewTFSTP.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewTFSTP.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTFSTP.Columns.Add("Column22", "下單時間");
                                    dataGridViewTFSTP.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewTFSTP.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewTFSTP.Columns.Add("Column25", "來源別");
                                    dataGridViewTFSTP.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTFSTP.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTFSTP.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewTFSTP.Columns.Add("Column29", "更新時間");
                                    dataGridViewTFSTP.Columns.Add("Column30", "[國內期選]倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewTFSTP.Columns.Add("Column31", "[國內期選]商品契約年月EX: 202212");
                                    dataGridViewTFSTP.Columns.Add("Column32", "[國內期選]履約價");
                                    dataGridViewTFSTP.Columns.Add("Column33", "[國內期選]是否為價差商品0:否 ;1:是");
                                    dataGridViewTFSTP.Columns.Add("Column34", "[國內期選]N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewTFSTP.Columns.Add("Column35", "[國內期選]是否為選擇權0:否;1:是 ");
                                    dataGridViewTFSTP.Columns.Add("Column36", "[國內期選]是否為預約單0:否;1:是");
                                    dataGridViewTFSTP.Columns.Add("Column37", "[國內期選]預約單序號 非預約單為0");
                                    dataGridViewTFSTP.Columns.Add("Column38", "[國內期選]交易日");
                                    dataGridViewTFSTP.Columns.Add("Column39", "[國內期選]原始下單商品市場EX:TF");

                                    dataGridViewTFSTP.Columns.Add("Column40", "[國內期選]下單交易所EX: TAIFEX");
                                    dataGridViewTFSTP.Columns.Add("Column41", "[國內期選]第一腳後台商品代碼EX:FITX");
                                    dataGridViewTFSTP.Columns.Add("Column42", "[國內期選]第二腳後台商品代碼");
                                    dataGridViewTFSTP.Columns.Add("Column43", "[國內期選]第二腳商品契約年月");
                                    dataGridViewTFSTP.Columns.Add("Column44", "[國內期選]第二腳履約價");
                                    dataGridViewTFSTP.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTFSTP ONLY
                                {
                                    dataGridViewTFSTP2.Columns.Add("Column46", "長效單註記"); // 接續共用欄位 46
                                    dataGridViewTFSTP2.Columns.Add("Column47", "長效單序號");
                                    dataGridViewTFSTP2.Columns.Add("Column48", "長效單結束日期");
                                    dataGridViewTFSTP2.Columns.Add("Column49", "是否觸發即停止(預設為true) 0:條件觸發後，該筆長效單功能即失效 1:不管有無觸發，每日都送一次條件單，直到長效單結束日後才失效");

                                    dataGridViewTFSTP2.Columns.Add("Column50", "長效單類別 0：None 1：效期內觸發即失效 2：[停用]長效單結束日失效 3：效期內完全成交即失效");
                                    dataGridViewTFSTP2.Columns.Add("Column51", "長效單委託總量");
                                    dataGridViewTFSTP2.Columns.Add("Column52", "長效單成交總量");
                                }
                            }
                            //dataGridViewTFMST
                            {
                                //dataGridViewTFMST 期貨(共用欄位)
                                {
                                    dataGridViewTFMST.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTFMST.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewTFMST.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTFMST.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewTFMST.Columns.Add("Column5", "交易帳號");
                                    dataGridViewTFMST.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewTFMST.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewTFMST.Columns.Add("Column8", "13碼序號");
                                    dataGridViewTFMST.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewTFMST.Columns.Add("Column10", "委託書號");

                                    dataGridViewTFMST.Columns.Add("Column11", "商品代碼");
                                    dataGridViewTFMST.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewTFMST.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTFMST.Columns.Add("Column14", "委託價格");
                                    dataGridViewTFMST.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewTFMST.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTFMST.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewTFMST.Columns.Add("Column18", "觸發價");
                                    dataGridViewTFMST.Columns.Add("Column19", "觸發時間");
                                    dataGridViewTFMST.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewTFMST.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTFMST.Columns.Add("Column22", "下單時間");
                                    dataGridViewTFMST.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewTFMST.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewTFMST.Columns.Add("Column25", "來源別");
                                    dataGridViewTFMST.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTFMST.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTFMST.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewTFMST.Columns.Add("Column29", "更新時間");
                                    dataGridViewTFMST.Columns.Add("Column30", "[國內期選]倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewTFMST.Columns.Add("Column31", "[國內期選]商品契約年月EX: 202212");
                                    dataGridViewTFMST.Columns.Add("Column32", "[國內期選]履約價");
                                    dataGridViewTFMST.Columns.Add("Column33", "[國內期選]是否為價差商品0:否 ;1:是");
                                    dataGridViewTFMST.Columns.Add("Column34", "[國內期選]N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewTFMST.Columns.Add("Column35", "[國內期選]是否為選擇權0:否;1:是 ");
                                    dataGridViewTFMST.Columns.Add("Column36", "[國內期選]是否為預約單0:否;1:是");
                                    dataGridViewTFMST.Columns.Add("Column37", "[國內期選]預約單序號 非預約單為0");
                                    dataGridViewTFMST.Columns.Add("Column38", "[國內期選]交易日");
                                    dataGridViewTFMST.Columns.Add("Column39", "[國內期選]原始下單商品市場EX:TF");

                                    dataGridViewTFMST.Columns.Add("Column40", "[國內期選]下單交易所EX: TAIFEX");
                                    dataGridViewTFMST.Columns.Add("Column41", "[國內期選]第一腳後台商品代碼EX:FITX");
                                    dataGridViewTFMST.Columns.Add("Column42", "[國內期選]第二腳後台商品代碼");
                                    dataGridViewTFMST.Columns.Add("Column43", "[國內期選]第二腳商品契約年月");
                                    dataGridViewTFMST.Columns.Add("Column44", "[國內期選]第二腳履約價");
                                    dataGridViewTFMST.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTFMST ONLY
                                {
                                    dataGridViewTFMST2.Columns.Add("Column46", "MST:移動點數"); // 接續共用欄位 46
                                    dataGridViewTFMST2.Columns.Add("Column47", "MST:觸價基準");
                                    dataGridViewTFMST2.Columns.Add("Column48", "MST:當前市價");
                                    dataGridViewTFMST2.Columns.Add("Column49", "前一個觸發價格");
                                }
                            }
                            //dataGridViewTFMIT
                            {
                                //dataGridViewTFMIT 期貨(共用欄位)
                                {
                                    dataGridViewTFMIT.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTFMIT.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewTFMIT.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTFMIT.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewTFMIT.Columns.Add("Column5", "交易帳號");
                                    dataGridViewTFMIT.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewTFMIT.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewTFMIT.Columns.Add("Column8", "13碼序號");
                                    dataGridViewTFMIT.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewTFMIT.Columns.Add("Column10", "委託書號");

                                    dataGridViewTFMIT.Columns.Add("Column11", "商品代碼");
                                    dataGridViewTFMIT.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewTFMIT.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTFMIT.Columns.Add("Column14", "委託價格");
                                    dataGridViewTFMIT.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewTFMIT.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTFMIT.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewTFMIT.Columns.Add("Column18", "觸發價");
                                    dataGridViewTFMIT.Columns.Add("Column19", "觸發時間");
                                    dataGridViewTFMIT.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewTFMIT.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTFMIT.Columns.Add("Column22", "下單時間");
                                    dataGridViewTFMIT.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewTFMIT.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewTFMIT.Columns.Add("Column25", "來源別");
                                    dataGridViewTFMIT.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTFMIT.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTFMIT.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewTFMIT.Columns.Add("Column29", "更新時間");
                                    dataGridViewTFMIT.Columns.Add("Column30", "[國內期選]倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewTFMIT.Columns.Add("Column31", "[國內期選]商品契約年月EX: 202212");
                                    dataGridViewTFMIT.Columns.Add("Column32", "[國內期選]履約價");
                                    dataGridViewTFMIT.Columns.Add("Column33", "[國內期選]是否為價差商品0:否 ;1:是");
                                    dataGridViewTFMIT.Columns.Add("Column34", "[國內期選]N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewTFMIT.Columns.Add("Column35", "[國內期選]是否為選擇權0:否;1:是 ");
                                    dataGridViewTFMIT.Columns.Add("Column36", "[國內期選]是否為預約單0:否;1:是");
                                    dataGridViewTFMIT.Columns.Add("Column37", "[國內期選]預約單序號 非預約單為0");
                                    dataGridViewTFMIT.Columns.Add("Column38", "[國內期選]交易日");
                                    
                                    dataGridViewTFMIT.Columns.Add("Column39", "[國內期選]原始下單商品市場EX:TF");

                                    dataGridViewTFMIT.Columns.Add("Column40", "[國內期選]下單交易所EX: TAIFEX");
                                    dataGridViewTFMIT.Columns.Add("Column41", "[國內期選]第一腳後台商品代碼EX:FITX");
                                    dataGridViewTFMIT.Columns.Add("Column42", "[國內期選]第二腳後台商品代碼");
                                    dataGridViewTFMIT.Columns.Add("Column43", "[國內期選]第二腳商品契約年月");
                                    dataGridViewTFMIT.Columns.Add("Column44", "[國內期選]第二腳履約價");
                                    dataGridViewTFMIT.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTFMIT ONLY
                                {
                                    dataGridViewTFMIT2.Columns.Add("Column46", "當前市價，若下單未填則為0"); // 接續共用欄位 46
                                    dataGridViewTFMIT2.Columns.Add("Column47", "成交價觸發價格");
                                }
                            }
                            //dataGridViewTFOCO
                            {
                                //dataGridViewTFOCO 期貨(共用欄位)
                                {
                                    dataGridViewTFOCO.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTFOCO.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewTFOCO.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTFOCO.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewTFOCO.Columns.Add("Column5", "交易帳號");
                                    dataGridViewTFOCO.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewTFOCO.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewTFOCO.Columns.Add("Column8", "13碼序號");
                                    dataGridViewTFOCO.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewTFOCO.Columns.Add("Column10", "委託書號");

                                    dataGridViewTFOCO.Columns.Add("Column11", "商品代碼");
                                    dataGridViewTFOCO.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewTFOCO.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTFOCO.Columns.Add("Column14", "委託價格");
                                    dataGridViewTFOCO.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewTFOCO.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTFOCO.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewTFOCO.Columns.Add("Column18", "觸發價");
                                    dataGridViewTFOCO.Columns.Add("Column19", "觸發時間");
                                    dataGridViewTFOCO.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewTFOCO.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTFOCO.Columns.Add("Column22", "下單時間");
                                    dataGridViewTFOCO.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewTFOCO.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewTFOCO.Columns.Add("Column25", "來源別");
                                    dataGridViewTFOCO.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTFOCO.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTFOCO.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewTFOCO.Columns.Add("Column29", "更新時間");
                                    dataGridViewTFOCO.Columns.Add("Column30", "[國內期選]倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewTFOCO.Columns.Add("Column31", "[國內期選]商品契約年月EX: 202212");
                                    dataGridViewTFOCO.Columns.Add("Column32", "[國內期選]履約價");
                                    dataGridViewTFOCO.Columns.Add("Column33", "[國內期選]是否為價差商品0:否 ;1:是");
                                    dataGridViewTFOCO.Columns.Add("Column34", "[國內期選]N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewTFOCO.Columns.Add("Column35", "[國內期選]是否為選擇權0:否;1:是 ");
                                    dataGridViewTFOCO.Columns.Add("Column36", "[國內期選]是否為預約單0:否;1:是");
                                    dataGridViewTFOCO.Columns.Add("Column37", "[國內期選]預約單序號 非預約單為0");
                                    dataGridViewTFOCO.Columns.Add("Column38", "[國內期選]交易日");
                                    
                                    dataGridViewTFOCO.Columns.Add("Column39", "[國內期選]原始下單商品市場EX:TF");

                                    dataGridViewTFOCO.Columns.Add("Column40", "[國內期選]下單交易所EX: TAIFEX");
                                    dataGridViewTFOCO.Columns.Add("Column41", "[國內期選]第一腳後台商品代碼EX:FITX");
                                    dataGridViewTFOCO.Columns.Add("Column42", "[國內期選]第二腳後台商品代碼");
                                    dataGridViewTFOCO.Columns.Add("Column43", "[國內期選]第二腳商品契約年月");
                                    dataGridViewTFOCO.Columns.Add("Column44", "[國內期選]第二腳履約價");
                                    dataGridViewTFOCO.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTFOCO ONLY
                                {
                                    dataGridViewTFOCO2.Columns.Add("Column46", "第一腳觸發價"); // 接續共用欄位 46
                                    dataGridViewTFOCO2.Columns.Add("Column47", "第二腳觸發價");
                                    dataGridViewTFOCO2.Columns.Add("Column48", "第二腳委託價");
                                    dataGridViewTFOCO2.Columns.Add("Column49", "第二腳委託價格類別 1：市價 2：限價");

                                    dataGridViewTFOCO2.Columns.Add("Column50", "第二腳委託時效 0:ROD;3:IOC;4:FOK");
                                    dataGridViewTFOCO2.Columns.Add("Column51", "第二腳買賣別");
                                    dataGridViewTFOCO2.Columns.Add("Column52", "第二腳委託價格別 0:前日收盤價 1:漲停價 2:跌停價 7使用者輸入價");
                                    dataGridViewTFOCO2.Columns.Add("Column53", "[限期貨選擇權]第二腳倉位");
                                    dataGridViewTFOCO2.Columns.Add("Column54", "長效單註記");
                                    dataGridViewTFOCO2.Columns.Add("Column55", "長效單序號");
                                    dataGridViewTFOCO2.Columns.Add("Column56", "長效單結束日期");
                                    dataGridViewTFOCO2.Columns.Add("Column57", "是否觸發即停止(預設為true) 0:條件觸發後，該筆長效單功能即失效 1:不管有無觸發，每日都送一次條件單，直到長效單結束日後才失效");
                                    dataGridViewTFOCO2.Columns.Add("Column58", "長效單類別 0：None 1：效期內觸發即失效 2：[停用]長效單結束日失效 3：效期內完全成交即失效");
                                    dataGridViewTFOCO2.Columns.Add("Column59", "長效單委託總量");

                                    dataGridViewTFOCO2.Columns.Add("Column60", "長效單成交總量");
                                }
                            }
                            //dataGridViewTFAB
                            {
                                //dataGridViewTFAB 期貨(共用欄位)
                                {
                                    dataGridViewTFAB.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewTFAB.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewTFAB.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewTFAB.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewTFAB.Columns.Add("Column5", "交易帳號");
                                    dataGridViewTFAB.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewTFAB.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewTFAB.Columns.Add("Column8", "13碼序號");
                                    dataGridViewTFAB.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewTFAB.Columns.Add("Column10", "委託書號");

                                    dataGridViewTFAB.Columns.Add("Column11", "商品代碼");
                                    dataGridViewTFAB.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewTFAB.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewTFAB.Columns.Add("Column14", "委託價格");
                                    dataGridViewTFAB.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewTFAB.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewTFAB.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewTFAB.Columns.Add("Column18", "觸發價");
                                    dataGridViewTFAB.Columns.Add("Column19", "觸發時間");
                                    dataGridViewTFAB.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewTFAB.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewTFAB.Columns.Add("Column22", "下單時間");
                                    dataGridViewTFAB.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewTFAB.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewTFAB.Columns.Add("Column25", "來源別");
                                    dataGridViewTFAB.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewTFAB.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewTFAB.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewTFAB.Columns.Add("Column29", "更新時間");
                                    dataGridViewTFAB.Columns.Add("Column30", "[國內期選]倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewTFAB.Columns.Add("Column31", "[國內期選]商品契約年月EX: 202212");
                                    dataGridViewTFAB.Columns.Add("Column32", "[國內期選]履約價");
                                    dataGridViewTFAB.Columns.Add("Column33", "[國內期選]是否為價差商品0:否 ;1:是");
                                    dataGridViewTFAB.Columns.Add("Column34", "[國內期選]N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewTFAB.Columns.Add("Column35", "[國內期選]是否為選擇權0:否;1:是 ");
                                    dataGridViewTFAB.Columns.Add("Column36", "[國內期選]是否為預約單0:否;1:是");
                                    dataGridViewTFAB.Columns.Add("Column37", "[國內期選]預約單序號 非預約單為0");
                                    dataGridViewTFAB.Columns.Add("Column38", "[國內期選]交易日");
                                    
                                    dataGridViewTFAB.Columns.Add("Column39", "[國內期選]原始下單商品市場EX:TF");

                                    dataGridViewTFAB.Columns.Add("Column40", "[國內期選]下單交易所EX: TAIFEX");
                                    dataGridViewTFAB.Columns.Add("Column41", "[國內期選]第一腳後台商品代碼EX:FITX");
                                    dataGridViewTFAB.Columns.Add("Column42", "[國內期選]第二腳後台商品代碼");
                                    dataGridViewTFAB.Columns.Add("Column43", "[國內期選]第二腳商品契約年月");
                                    dataGridViewTFAB.Columns.Add("Column44", "[國內期選]第二腳履約價");
                                    dataGridViewTFAB.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewTFAB ONLY
                                {
                                    dataGridViewTFAB2.Columns.Add("Column46", "成交價觸發價格"); // 接續共用欄位 46
                                }
                            }
                        }
                        // 海期
                        {
                            //dataGridViewOFOCO
                            {
                                //dataGridViewOFOCO 海期(共用欄位)
                                {
                                    dataGridViewOFOCO.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewOFOCO.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewOFOCO.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewOFOCO.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewOFOCO.Columns.Add("Column5", "交易帳號");
                                    dataGridViewOFOCO.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewOFOCO.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewOFOCO.Columns.Add("Column8", "13碼序號");
                                    dataGridViewOFOCO.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewOFOCO.Columns.Add("Column10", "委託書號");

                                    dataGridViewOFOCO.Columns.Add("Column11", "商品代碼");
                                    dataGridViewOFOCO.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewOFOCO.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewOFOCO.Columns.Add("Column14", "委託價格");
                                    dataGridViewOFOCO.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewOFOCO.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewOFOCO.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewOFOCO.Columns.Add("Column18", "觸發價");
                                    dataGridViewOFOCO.Columns.Add("Column19", "觸發時間");
                                    dataGridViewOFOCO.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewOFOCO.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewOFOCO.Columns.Add("Column22", "下單時間");
                                    dataGridViewOFOCO.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewOFOCO.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewOFOCO.Columns.Add("Column25", "來源別");
                                    dataGridViewOFOCO.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewOFOCO.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewOFOCO.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewOFOCO.Columns.Add("Column29", "更新時間");
                                    dataGridViewOFOCO.Columns.Add("Column30", "倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewOFOCO.Columns.Add("Column31", "商品契約年月EX: 202212");
                                    dataGridViewOFOCO.Columns.Add("Column32", "履約價");
                                    dataGridViewOFOCO.Columns.Add("Column33", "是否為價差商品0:否 ;1:是");
                                    dataGridViewOFOCO.Columns.Add("Column34", "N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewOFOCO.Columns.Add("Column35", "是否為選擇權0:否;1:是 ");
                                    dataGridViewOFOCO.Columns.Add("Column36", "是否為預約單0:否;1:是");
                                    dataGridViewOFOCO.Columns.Add("Column37", "預約單序號 非預約單為0");
                                    dataGridViewOFOCO.Columns.Add("Column38", "交易日");
                                    
                                    dataGridViewOFOCO.Columns.Add("Column39", "原始下單商品市場EX:TF");

                                    dataGridViewOFOCO.Columns.Add("Column40", "下單交易所EX: TAIFEX");
                                    dataGridViewOFOCO.Columns.Add("Column41", "第一腳後台商品代碼EX:FITX");
                                    dataGridViewOFOCO.Columns.Add("Column42", "第二腳後台商品代碼");
                                    dataGridViewOFOCO.Columns.Add("Column43", "第二腳商品契約年月");
                                    dataGridViewOFOCO.Columns.Add("Column44", "第二腳履約價");
                                    dataGridViewOFOCO.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewOFOCO ONLY
                                {
                                    dataGridViewOFOCO2.Columns.Add("Column46", "第一隻腳觸發價"); // 接續共用欄位 46
                                    dataGridViewOFOCO2.Columns.Add("Column47", "第一隻腳觸發價分子");
                                    dataGridViewOFOCO2.Columns.Add("Column48", "第一隻腳觸發價分母");
                                    dataGridViewOFOCO2.Columns.Add("Column49", "第二隻腳觸發價");
                                    dataGridViewOFOCO2.Columns.Add("Column50", "第二隻腳觸發價分子");

                                    dataGridViewOFOCO2.Columns.Add("Column51", "第二隻腳觸發價分母");
                                    dataGridViewOFOCO2.Columns.Add("Column52", "第二隻腳委託價");
                                    dataGridViewOFOCO2.Columns.Add("Column53", "第二隻腳委託價分子");
                                    dataGridViewOFOCO2.Columns.Add("Column54", "第二隻腳委託價分母");
                                    dataGridViewOFOCO2.Columns.Add("Column55", "第二隻腳委託價格類別 1：市價 2：限價");
                                    dataGridViewOFOCO2.Columns.Add("Column56", "第二隻腳委託時效 0：當日有效ROD 3：立即成交IOC 4：立即全部成交FOK");
                                    dataGridViewOFOCO2.Columns.Add("Column57", "第二隻腳買賣別");
                                    dataGridViewOFOCO2.Columns.Add("Column58", "第二隻腳新平倉 0：新單 1：平倉 2：自動");
                                    dataGridViewOFOCO2.Columns.Add("Column59", "長效單註記");
                                    dataGridViewOFOCO2.Columns.Add("Column60", "長效單序號");

                                    dataGridViewOFOCO2.Columns.Add("Column61", "長效單結束日期");
                                    dataGridViewOFOCO2.Columns.Add("Column62", "是否觸發即停止(預設為true) 0:條件觸發後，該筆長效單功能即失效 1:不管有無觸發，每日都送一次條件單，直到長效單結束日後才失效");
                                    dataGridViewOFOCO2.Columns.Add("Column63", "長效單類別 0：None 1：效期內觸發即失效 2：[停用]長效單結束日失效 3：效期內完全成交即失效");
                                    dataGridViewOFOCO2.Columns.Add("Column64", "長效單委託總量");
                                    dataGridViewOFOCO2.Columns.Add("Column65", "長效單成交總量");
                                }
                            }
                            //dataGridViewOFAB
                            {
                                //dataGridViewOFAB 海期(共用欄位)
                                {
                                    dataGridViewOFAB.Columns.Add("Column1", "1:新增  2:刪除");
                                    dataGridViewOFAB.Columns.Add("Column2", "智慧單(母單)序號");
                                    dataGridViewOFAB.Columns.Add("Column3", "委託單順序(判斷每筆回報順序使用)");
                                    dataGridViewOFAB.Columns.Add("Column4", "分公司代碼");
                                    dataGridViewOFAB.Columns.Add("Column5", "交易帳號");
                                    dataGridViewOFAB.Columns.Add("Column6", "子帳帳號");
                                    dataGridViewOFAB.Columns.Add("Column7", "交易所名稱");
                                    dataGridViewOFAB.Columns.Add("Column8", "13碼序號");
                                    dataGridViewOFAB.Columns.Add("Column9", "原始13碼序號");
                                    dataGridViewOFAB.Columns.Add("Column10", "委託書號");

                                    dataGridViewOFAB.Columns.Add("Column11", "商品代碼");
                                    dataGridViewOFAB.Columns.Add("Column12", "B: 買 S:賣");
                                    dataGridViewOFAB.Columns.Add("Column13", "0=前日收盤價 (平盤價);1:漲停價 ;2:跌停價;7:使用者輸入價");
                                    dataGridViewOFAB.Columns.Add("Column14", "委託價格");
                                    dataGridViewOFAB.Columns.Add("Column15", "1市價;2限價;3:範圍市價 期貨:不支援市價單");
                                    dataGridViewOFAB.Columns.Add("Column16", "0：ROD 3：IOC  4：FOK");
                                    dataGridViewOFAB.Columns.Add("Column17", "TS:張數; TF:口數");
                                    dataGridViewOFAB.Columns.Add("Column18", "觸發價");
                                    dataGridViewOFAB.Columns.Add("Column19", "觸發時間");
                                    dataGridViewOFAB.Columns.Add("Column20", "觸發價方向0: None;1:GTE(大於等於); 2:LTE(小於等於)");

                                    dataGridViewOFAB.Columns.Add("Column21", "是否當沖 期貨：非當沖:空值；當沖:Y");
                                    dataGridViewOFAB.Columns.Add("Column22", "下單時間");
                                    dataGridViewOFAB.Columns.Add("Column23", "營業員代碼");
                                    dataGridViewOFAB.Columns.Add("Column24", "USER PC IP");
                                    dataGridViewOFAB.Columns.Add("Column25", "來源別");
                                    dataGridViewOFAB.Columns.Add("Column26", "32：中台收單成功 33：中台收單失敗 34：洗價中 35：洗價中-觸發價更新(移動停損單) 36：洗價失敗  37：洗價觸發 38：觸發下單 39：下單失敗 40：使用者刪單 999:萬用狀態,請同時確認UniversalMsg萬用訊息");
                                    dataGridViewOFAB.Columns.Add("Column27", "錯誤訊息註記(Y失敗;N成功)");
                                    dataGridViewOFAB.Columns.Add("Column28", "訊息EX:[智慧單號][商品代碼]訊息內容");
                                    dataGridViewOFAB.Columns.Add("Column29", "更新時間");
                                    dataGridViewOFAB.Columns.Add("Column30", "倉位 0:新倉;1:平倉; 2:自動");

                                    dataGridViewOFAB.Columns.Add("Column31", "商品契約年月EX: 202212");
                                    dataGridViewOFAB.Columns.Add("Column32", "履約價");
                                    dataGridViewOFAB.Columns.Add("Column33", "是否為價差商品0:否 ;1:是");
                                    dataGridViewOFAB.Columns.Add("Column34", "N:非買權及賣權 C:買權Call或P:賣權Put ");
                                    dataGridViewOFAB.Columns.Add("Column35", "是否為選擇權0:否;1:是 ");
                                    dataGridViewOFAB.Columns.Add("Column36", "是否為預約單0:否;1:是");
                                    dataGridViewOFAB.Columns.Add("Column37", "預約單序號 非預約單為0");
                                    dataGridViewOFAB.Columns.Add("Column38", "交易日");

                                    dataGridViewOFAB.Columns.Add("Column39", "原始下單商品市場EX:TF");

                                    dataGridViewOFAB.Columns.Add("Column40", "下單交易所EX: TAIFEX");
                                    dataGridViewOFAB.Columns.Add("Column41", "第一腳後台商品代碼EX:FITX");
                                    dataGridViewOFAB.Columns.Add("Column42", "第二腳後台商品代碼");
                                    dataGridViewOFAB.Columns.Add("Column43", "第二腳商品契約年月");
                                    dataGridViewOFAB.Columns.Add("Column44", "第二腳履約價");
                                    dataGridViewOFAB.Columns.Add("Column45", "萬用訊息 適用於智慧單狀態Sataus為999情況");
                                }
                                //dataGridViewOFAB ONLY
                                {
                                    dataGridViewOFAB2.Columns.Add("Column46", "成交價觸發價格"); // 接續共用欄位 46
                                }
                            }
                        }
                    }
                }
            }         
        }
        private void ReplyForm_Load(object sender, EventArgs e)
        {
            //下單帳號資訊
            {
                m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(OnAccount);
                void OnAccount(string bstrLogInID, string bstrAccountData)
                {
                    AddUserID(m_dictUserID, bstrLogInID, bstrAccountData);

                    //獲得所有key
                    if (allkeys != null) allkeys.Clear();
                    allkeys = new List<string>(m_dictUserID.Keys);

                    if (comboBoxUserID.DataSource != null) comboBoxUserID.DataSource = null;
                    comboBoxUserID.DataSource = allkeys;
                }
            }
            // 當有回報將主動呼叫函式，並通知委託的狀態。(新格式 包含預約單回報)
            {
                m_pSKReply.OnNewData += new _ISKReplyLibEvents_OnNewDataEventHandler(OnNewData);
                void OnNewData(string bstrLogInID, string bstrData)
                {
                    if (isClosing != true)
                    {
                        // 宣告bstrData切出來的參數
                        string[] values = new string[48];

                        string KeyNo;
                        string MarketType;
                        string Type;
                        string OrderErr;
                        string Broker;
                        string CustNo;
                        string BuySell;
                        string ExchangeID;
                        string ComId;
                        string StrikePrice;

                        string OrderNo;
                        string Price;
                        string Numerator;
                        string Denominator;
                        string Price1;
                        string Numerator1;
                        string Denominator1;
                        string Price2;
                        string Numerator2;
                        string Denominator2;

                        string Qty;
                        string BeforeQty;
                        string AfterQty;
                        string Date;
                        string Time;
                        string OkSeq;
                        string SubID;
                        string SaleNo;
                        string Agent;
                        string TradeDate;

                        string MsgNo;
                        string PreOrder;
                        string ComId1;
                        string YearMonth1;
                        string StrikePrice1;
                        string ComId2;
                        string YearMonth2;
                        string StrikePrice2;
                        string ExecutionNo;
                        string PriceSymbol;

                        string Reserved;
                        string OrderEffective;
                        string CallPut;
                        string OrderSeq;
                        string ErrorMsg;
                        string CancelOrderMarkByExchange;
                        string ExchangeTandemMsg;
                        string SeqNo;
                        string OFSTPFlag;

                        // 使用 Split 方法將字串拆分成陣列
                        values = bstrData.Split(',');
                        if (values[0] == "980") // 980(後台問題)
                        {
                            dataGridViewNoClass.Rows.Add(bstrData);
                        }
                        else
                        {
                            // 宣告bstrData參數
                            KeyNo = values[0];
                            MarketType = values[1];
                            Type = values[2];
                            OrderErr = values[3];
                            Broker = values[4];
                            CustNo = values[5];
                            BuySell = values[6];
                            ExchangeID = values[7];
                            ComId = values[8];
                            StrikePrice = values[9];

                            OrderNo = values[10];
                            Price = values[11];
                            Numerator = values[12];
                            Denominator = values[13];
                            Price1 = values[14];
                            Numerator1 = values[15];
                            Denominator1 = values[16];
                            Price2 = values[17];
                            Numerator2 = values[18];
                            Denominator2 = values[19];

                            Qty = values[20];
                            BeforeQty = values[21];
                            AfterQty = values[22];
                            Date = values[23];
                            Time = values[24];
                            OkSeq = values[25];
                            SubID = values[26];
                            SaleNo = values[27];
                            Agent = values[28];
                            TradeDate = values[29];

                            MsgNo = values[30];
                            PreOrder = values[31];
                            ComId1 = values[32];
                            YearMonth1 = values[33];
                            StrikePrice1 = values[34];
                            ComId2 = values[35];
                            YearMonth2 = values[36];
                            StrikePrice2 = values[37];
                            ExecutionNo = values[38];
                            PriceSymbol = values[39];

                            Reserved = values[40];
                            OrderEffective = values[41];
                            CallPut = values[42];
                            OrderSeq = values[43];
                            ErrorMsg = values[44];
                            CancelOrderMarkByExchange = values[45];
                            ExchangeTandemMsg = values[46];
                            SeqNo = values[47];
                            //OFSTPFlag = values[48];
                            if (MarketType == "TS") dataGridViewTS.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Qty, BeforeQty, AfterQty, Date, Time, SubID, SaleNo, Agent, MsgNo, ExecutionNo, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "TA") dataGridViewTA.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, ExecutionNo, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "TL") dataGridViewTL.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, ExecutionNo, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "TP") dataGridViewTP.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, ExecutionNo, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "TC") dataGridViewTC.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, ExecutionNo, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "TF") dataGridViewTF.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Price1, Price2, Numerator2, Denominator2, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, PreOrder, ComId1, YearMonth1, ComId2, YearMonth2, ExecutionNo, PriceSymbol, Reserved, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "TO") dataGridViewTO.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, StrikePrice, OrderNo, Price, Price1, Price2, Numerator2, Denominator2, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, PreOrder, ComId1, YearMonth1, StrikePrice1, ComId2, YearMonth2, StrikePrice2, ExecutionNo, PriceSymbol, Reserved, OrderEffective, CallPut, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "OF") dataGridViewOF.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Numerator, Denominator, Price1, Numerator1, Denominator1, Price2, Numerator2, Denominator2, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, ComId1, YearMonth1, ComId2, YearMonth2, ExecutionNo, PriceSymbol, OrderEffective, OrderSeq, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "OO") dataGridViewOO.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, StrikePrice, OrderNo, Price, Numerator, Denominator, Price1, Numerator1, Denominator1, Price2, Numerator2, Denominator2, Qty, Date, Time, SubID, SaleNo, Agent, MsgNo, ComId1, YearMonth1, StrikePrice1, ComId2, YearMonth2, StrikePrice2, ExecutionNo, PriceSymbol, OrderEffective, CallPut, OrderSeq, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            if (MarketType == "OS") dataGridViewOS.Rows.Add(KeyNo, Type, OrderErr, Broker, CustNo, BuySell, ComId, OrderNo, Price, Qty, BeforeQty, AfterQty, Date, Time, SubID, SaleNo, Agent, MsgNo, ExecutionNo, OrderEffective, ErrorMsg, CancelOrderMarkByExchange, ExchangeTandemMsg, SeqNo);
                            dataGridViewNoClass.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47]);
                        }
                    }
                }
            }
            // 回報連線後會進行回報回補，等收到此事件通知後表示回補完成
            {
                m_pSKReply.OnComplete += new _ISKReplyLibEvents_OnCompleteEventHandler(OnComplete);
                void OnComplete(string bstrUserID)
                {
                    if (isClosing != true)
                    {
                        // 取得回傳訊息
                        string msg = "【OnComplete】" + bstrUserID + "回報連線&資料正常";
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            // 當有回報開始清除前日資料時，會發出的通知，表示清除前日回報
            {
                m_pSKReply.OnReplyClear += new _ISKReplyLibEvents_OnReplyClearEventHandler(OnReplyClear);
                void OnReplyClear(string bstrMarket)
                {
                    if (isClosing != true)
                    {
                        string msg = "";
                        // 取得回傳訊息
                        if (bstrMarket == "R1") msg = "證券";
                        else if (bstrMarket == "R2") msg = "國內期選";
                        else if (bstrMarket == "R3") msg = "海外股市";
                        else if (bstrMarket == "R4") msg = "海外期選";
                        else if (bstrMarket == "R11") msg = "盤中零股";
                        else if (bstrMarket == "R20" || bstrMarket == "R21" || bstrMarket == "R22" || bstrMarket == "R23") msg = "智慧單";
                        msg = "【OnReplyClear】" + msg + "正在清除前日回報!!!";
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            // 當公告開始清除前日資料時，會發出的通知
            {
                m_pSKReply.OnReplyClearMessage += new _ISKReplyLibEvents_OnReplyClearMessageEventHandler(OnReplyClearMessage);
                void OnReplyClearMessage(string bstrUserID)
                {
                    if (isClosing != true)
                    {
                        // 取得回傳訊息
                        string msg = "【OnReplyClearMessage】" + bstrUserID + "正在清除前日回報!";
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            // 當中斷solace連線，會透過此事件函式告知斷線結果
            {
                m_pSKReply.OnSolaceReplyDisconnect += new _ISKReplyLibEvents_OnSolaceReplyDisconnectEventHandler(OnSolaceReplyDisconnect);
                void OnSolaceReplyDisconnect(string bstrUserID, int nErrorCode)
                {
                    if (isClosing != true)
                    {
                        string msg;
                        if (nErrorCode == 3002)
                        {
                            msg = "斷線成功";
                        }
                        else if (nErrorCode == 3033) //SK_SUBJECT_SOLACE_SESSION_EVENT_ERROR (Solace Session down錯誤 (因AP與主機連線異常，由主機端主動斷線))
                        {
                            msg = "連線異常";
                            timerSolaceReconnect.Enabled = true; // 斷線重連timer啟用，5秒重連一次
                        }
                        else
                        {
                            msg = "未預期的斷線" + nErrorCode;
                        }
                        // 取得回傳訊息
                        msg = "【OnSolaceReplyDisconnect】" + bstrUserID + "_" + msg;
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            // 當solace連線，會透過此事件函式告知
            {
                m_pSKReply.OnSolaceReplyConnection += new _ISKReplyLibEvents_OnSolaceReplyConnectionEventHandler(OnSolaceReplyConnection);
                void OnSolaceReplyConnection(string bstrUserID, int nErrorCode)
                {
                    if (isClosing != true)
                    {
                        string msg;
                        if (nErrorCode == 0) msg = "連線成功";
                        else msg = "連線失敗";
                        // 取得回傳訊息
                        msg = "【OnSolaceReplyConnection】" + bstrUserID + "_" + msg;
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            // 當有智慧單回報將主動呼叫函式，並通知智慧單的狀態
            {
                m_pSKReply.OnStrategyData += new _ISKReplyLibEvents_OnStrategyDataEventHandler(OnStrategyData);
                void OnStrategyData(string bstrLogInID, string bstrData)
                {
                    if (isClosing != true)
                    {
                        // 使用 Split 方法將字串拆分成陣列
                        string[] values = bstrData.Split(',');                       
                        if (values[0] == "980") // 980(後台問題)
                        {
                            dataGridViewTSMST.Rows.Add(bstrData); // 錯誤顯示在證券-MST 第 0 Row
                        }
                        else
                        {
                            string TradeKind = values[5]; // 單別
                            if (values[0] == "TS") // 證券市場
                            {
                                if (TradeKind == "0") // None
                                {
                                    // 先不處理
                                }
                                else if (TradeKind == "9") // MST
                                {
                                    dataGridViewTSMST.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSMST2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47]);
                                }
                                else if (TradeKind == "29") // MIOC
                                {
                                    dataGridViewTSMIOC.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSMIOC2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39]);
                                }
                                else if (TradeKind == "8") // MIT
                                {
                                    dataGridViewTSMIT.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSMIT2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44]);
                                }
                                else if (TradeKind == "11") // DayTrading
                                {
                                    dataGridViewTSDayTrading.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSDayTrading2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48], values[49], values[50], values[51], values[52], values[53]);
                                }
                                else if (TradeKind == "17") // ClearOut
                                {
                                    dataGridViewTSClearOut.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSClearOut2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48], values[49], values[50], values[51], values[52], values[53]);
                                }
                                else if (TradeKind == "3") // OCO
                                {
                                    dataGridViewTSOCO.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSOCO2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41]);
                                }
                                else if (TradeKind == "10") // AB
                                {
                                    dataGridViewTSAB.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSAB2.Rows.Add(values[34]);
                                }
                                else if (TradeKind == "27") // CB
                                {
                                    dataGridViewTSCB.Rows.Add(values[1], values[2], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33]);
                                    dataGridViewTSCB2.Rows.Add(values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48], values[49], values[50], values[51], values[52], values[53], values[54], values[55], values[56], values[57], values[58], values[59], values[60], values[61], values[62], values[63], values[64], values[65], values[66], values[67], values[68], values[69], values[70], values[71]);
                                }
                            }
                            else if (values[0] == "TF") // 國內期選
                            {
                                TradeKind = values[5]; // 單別
                                if (TradeKind == "0") // None
                                {
                                    // 先不處理
                                }
                                else if (TradeKind == "5") // STP
                                {
                                    dataGridViewTFSTP.Rows.Add(values[1], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48]);
                                    dataGridViewTFSTP2.Rows.Add(values[49], values[50], values[51], values[52], values[53], values[54], values[55]);
                                }
                                else if (TradeKind == "8") // MIT
                                {
                                    dataGridViewTFMIT.Rows.Add(values[1], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48]);
                                    dataGridViewTFMIT2.Rows.Add(values[49], values[50]);
                                }
                                else if (TradeKind == "9") // MST
                                {
                                    dataGridViewTFMST.Rows.Add(values[1], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48]);
                                    dataGridViewTFMST2.Rows.Add(values[49], values[50], values[51], values[52]);
                                }
                                else if (TradeKind == "3") // OCO
                                {
                                    dataGridViewTFOCO.Rows.Add(values[1], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48]);
                                    dataGridViewTFOCO2.Rows.Add(values[49], values[50], values[51], values[52], values[53], values[54], values[55], values[56], values[57], values[58], values[59], values[60], values[61], values[62], values[63]);
                                }
                                else if (TradeKind == "10") // AB
                                {
                                    dataGridViewTFAB.Rows.Add(values[1], values[3], values[4], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46], values[47], values[48]);
                                    dataGridViewTFAB2.Rows.Add(values[49]);
                                }
                            }
                            else if (values[0] == "OF")// OF: 海外期貨
                            {
                                TradeKind = values[4]; // 單別
                                if (TradeKind == "0") // None
                                {
                                    // 先不處理
                                }
                                else if (TradeKind == "3") // OCO
                                {
                                    dataGridViewOFOCO.Rows.Add(values[1], values[2], values[3], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46]);
                                    dataGridViewOFOCO2.Rows.Add(values[47], values[48], values[49], values[50], values[51], values[52], values[53], values[54], values[55], values[56], values[57], values[58], values[59], values[60], values[61], values[62], values[63], values[64], values[65], values[66]);
                                }
                                else if (TradeKind == "10") // AB
                                {
                                    dataGridViewOFAB.Rows.Add(values[1], values[2], values[3], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19], values[20], values[21], values[22], values[23], values[24], values[25], values[26], values[27], values[28], values[29], values[30], values[31], values[32], values[33], values[34], values[35], values[36], values[37], values[38], values[39], values[40], values[41], values[42], values[43], values[44], values[45], values[46]);
                                    dataGridViewOFAB2.Rows.Add(values[47]);
                                }
                            }
                        }
                    }
                }
            }
            // 下單物件初始化
            {
                int nCode = m_pSKOrder.SKOrderLib_Initialize();
                // 取得回傳訊息
                string msg = "【SKOrderLib_Initialize】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
            // 取回可交易的所有帳號
            {
                int nCode = m_pSKOrder.GetUserAccount();
                // 取得回傳訊息
                string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void ReplyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            // 中斷指定帳號的連線
            m_pSKReply.SKReplyLib_SolaceCloseByID(comboBoxUserID.Text);
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
        private void timerSolaceReconnect_Tick(object sender, EventArgs e)
        {
            // 指定回報連線的使用者登入帳號
            int nCode = m_pSKReply.SKReplyLib_ConnectByID(comboBoxUserID.Text);
            string msg = "【自動重連中...】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
            if (nCode == 0) // 連線成功
            {
                // 中斷指定帳號的連線
                m_pSKReply.SKReplyLib_SolaceCloseByID(comboBoxUserID.Text);
                // 指定回報連線的使用者登入帳號
                nCode = m_pSKReply.SKReplyLib_ConnectByID(comboBoxUserID.Text);
                if (nCode == 0) // 連線成功
                {
                    msg = "【SKReplyLib_ConnectByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    timerSolaceReconnect.Enabled = false; // 斷線重連timer禁用
                }
            }
        }
        private void buttonSKReplyLib_IsConnectedByID_Click(object sender, EventArgs e)
        {
            string msg;
            // 檢查輸入的帳號目前連線狀態
            int nCode = m_pSKReply.SKReplyLib_IsConnectedByID(comboBoxUserID.Text);

            // 取得回傳訊息
            switch (nCode)
            {
                case 0:
                    msg = "【SKReplyLib_IsConnectedByID】斷線";
                    break;
                case 1:
                    msg = "【SKReplyLib_IsConnectedByID】連線中";
                    break;
                case 2:
                    msg = "【SKReplyLib_IsConnectedByID】下載中";
                    break;
                default:
                    msg = "【SKReplyLib_IsConnectedByID】出錯啦";
                    break;
            }
            richTextBoxMessage.AppendText(msg + "\n");          
        }
        private void buttonSKReplyLib_SolaceCloseByID_Click(object sender, EventArgs e)
        {
            // 中斷指定帳號的連線
            int nCode = m_pSKReply.SKReplyLib_SolaceCloseByID(comboBoxUserID.Text);

            // 取得回傳訊息->由事件來回傳
            string msg = "【SKReplyLib_SolaceCloseByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKReplyLib_ConnectByID_Click(object sender, EventArgs e)
        {
            // Clear dataGridView
            {
                // 一般回報
                {
                    dataGridViewTS.Rows.Clear();
                    dataGridViewTA.Rows.Clear();
                    dataGridViewTL.Rows.Clear();
                    dataGridViewTP.Rows.Clear();
                    dataGridViewTC.Rows.Clear();
                    dataGridViewTF.Rows.Clear();
                    dataGridViewTO.Rows.Clear();
                    dataGridViewOF.Rows.Clear();
                    dataGridViewOO.Rows.Clear();
                    dataGridViewOS.Rows.Clear();
                    dataGridViewNoClass.Rows.Clear();
                }
                // 智慧單回報
                {
                    // 證券
                    {
                        dataGridViewTSMST.Rows.Clear();
                        dataGridViewTSMIOC.Rows.Clear();
                        dataGridViewTSMIT.Rows.Clear();
                        dataGridViewTSDayTrading.Rows.Clear();
                        dataGridViewTSClearOut.Rows.Clear();
                        dataGridViewTSOCO.Rows.Clear();
                        dataGridViewTSAB.Rows.Clear();
                        dataGridViewTSCB.Rows.Clear();

                        dataGridViewTSMST2.Rows.Clear();
                        dataGridViewTSMIOC2.Rows.Clear();
                        dataGridViewTSMIT2.Rows.Clear();
                        dataGridViewTSDayTrading2.Rows.Clear();
                        dataGridViewTSClearOut2.Rows.Clear();
                        dataGridViewTSOCO2.Rows.Clear();
                        dataGridViewTSAB2.Rows.Clear();
                        dataGridViewTSCB2.Rows.Clear();
                    }
                    // 期選
                    {
                        dataGridViewTFSTP.Rows.Clear();
                        dataGridViewTFMIT.Rows.Clear();
                        dataGridViewTFMST.Rows.Clear();
                        dataGridViewTFOCO.Rows.Clear();
                        dataGridViewTFAB.Rows.Clear();

                        dataGridViewTFSTP2.Rows.Clear();
                        dataGridViewTFMIT2.Rows.Clear();
                        dataGridViewTFMST2.Rows.Clear();
                        dataGridViewTFOCO2.Rows.Clear();
                        dataGridViewTFAB2.Rows.Clear();
                    }
                    // 海期
                    {
                        dataGridViewOFOCO.Rows.Clear();
                        dataGridViewOFAB.Rows.Clear();

                        dataGridViewOFOCO2.Rows.Clear();
                        dataGridViewOFAB2.Rows.Clear();
                    }
                }
            }
            // 指定回報連線的使用者登入帳號
            int nCode = m_pSKReply.SKReplyLib_ConnectByID(comboBoxUserID.Text);

            // 取得回傳訊息
            string msg = "【SKReplyLib_ConnectByID】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
