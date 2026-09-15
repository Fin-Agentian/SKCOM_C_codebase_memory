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
    public partial class TSStrategyOrderForm : Form
    {
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境設定物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        // 是否為非同步委託
        bool bAsyncOrder = false;
        // 存[UserID]對應-交易帳號
        Dictionary<string, List<string>> m_dictUserID = new Dictionary<string, List<string>>();
        List<string> allkeys;       
        static void AddUserID(Dictionary<string, List<string>> dictUserID, string UserID, string AccountData)
        {
            string[] values = AccountData.Split(',');

            // broker ID (IB)4碼 + 帳號7碼
            string Account = values[1] + values[3];

            if (dictUserID.ContainsKey(UserID))
            {
                // 如果已經存在，添加到現有的 List<string>
                dictUserID[UserID].Add(Account);
            }
            else
            {
                // 如果不存在，創建一個新的 List<string>，並添加到字典中
                dictUserID[UserID] = new List<string> { Account };
            }
        }
        public TSStrategyOrderForm()
        {
            // Init
            {
                InitializeComponent();

                // comboBox
                {
                    // DayTrade
                    {
                        // comboBoxnBuySellDT
                        {
                            comboBoxnBuySellDT.Items.Add("0:現股買");
                            comboBoxnBuySellDT.Items.Add("1:無券賣出");
                        }
                        // comboBoxnOrderPriceCondDT
                        {
                            comboBoxnOrderPriceCondDT.Items.Add("0:ROD");
                            comboBoxnOrderPriceCondDT.Items.Add("3:IOC");
                            comboBoxnOrderPriceCondDT.Items.Add("4:FOK");
                        }
                        // comboBoxnOrderPriceTypeDT
                        {
                            comboBoxnOrderPriceTypeDT.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeDT.Items.Add("2:限價");
                        }
                        // comboBoxnInnerOrderIsMITDT
                        {
                            comboBoxnInnerOrderIsMITDT.Items.Add("0:N");
                            comboBoxnInnerOrderIsMITDT.Items.Add("1:Y");
                        }
                        // comboBoxnMITDirDT
                        {
                            comboBoxnMITDirDT.Items.Add("0:未啟用MIT");
                            comboBoxnMITDirDT.Items.Add("1:向上觸發(大於等於)");
                            comboBoxnMITDirDT.Items.Add("2:向下觸發(小於等於)");
                        }
                        // comboBoxnClearAllFlagDT
                        {
                            comboBoxnClearAllFlagDT.Items.Add("0:否");
                            comboBoxnClearAllFlagDT.Items.Add("1:是");
                        }
                        // comboBoxnClearAllPriceTypeDT
                        {
                            comboBoxnClearAllPriceTypeDT.Items.Add("1:市價");
                            comboBoxnClearAllPriceTypeDT.Items.Add("2:限價");
                        }
                        // comboBoxnFinalClearFlagDT
                        {
                            comboBoxnFinalClearFlagDT.Items.Add("0:否");
                            comboBoxnFinalClearFlagDT.Items.Add("1:是");
                        }
                        // comboBoxnTakeProfitFlagDT
                        {
                            comboBoxnTakeProfitFlagDT.Items.Add("0:否");
                            comboBoxnTakeProfitFlagDT.Items.Add("1:是");
                        }
                        // comboBoxnStopLossFlagDT
                        {
                            comboBoxnStopLossFlagDT.Items.Add("0:否");
                            comboBoxnStopLossFlagDT.Items.Add("1:是");
                        }
                        // comboBoxnClearOrderCondDT
                        {
                            comboBoxnClearOrderCondDT.Items.Add("0:ROD");
                            comboBoxnClearOrderCondDT.Items.Add("3:IOC");
                            comboBoxnClearOrderCondDT.Items.Add("4:FOK");
                        }

                        // comboBoxnRDOTPPercent
                        {
                            comboBoxnRDOTPPercent.Items.Add("0:觸發價");
                            comboBoxnRDOTPPercent.Items.Add("1:漲幅");
                        }
                        // comboBoxnRDTPMarketPriceType
                        {
                            comboBoxnRDTPMarketPriceType.Items.Add("1:市價");
                            comboBoxnRDTPMarketPriceType.Items.Add("2:限價");
                        }
                        // comboBoxnRDOSLPercent
                        {
                            comboBoxnRDOSLPercent.Items.Add("0:觸發價");
                            comboBoxnRDOSLPercent.Items.Add("1:漲跌幅");
                        }
                        // comboBoxnRDSLMarketPriceType
                        {
                            comboBoxnRDSLMarketPriceType.Items.Add("1:市價");
                            comboBoxnRDSLMarketPriceType.Items.Add("2:限價");
                        }
                        // comboBoxnTakeProfitOrderCond
                        {
                            comboBoxnTakeProfitOrderCond.Items.Add("0:ROD");
                            comboBoxnTakeProfitOrderCond.Items.Add("3:IOC");
                            comboBoxnTakeProfitOrderCond.Items.Add("4:FOK");
                        }
                        // comboBoxnStopLossOrderCond
                        {
                            comboBoxnStopLossOrderCond.Items.Add("0:ROD");
                            comboBoxnStopLossOrderCond.Items.Add("3:IOC");
                            comboBoxnStopLossOrderCond.Items.Add("4:FOK");
                        }
                    }
                    // ClearOut
                    {
                        // comboBoxnBuySellC
                        {
                            comboBoxnBuySellC.Items.Add("0:買");
                            comboBoxnBuySellC.Items.Add("1:賣");
                        }
                        // comboBoxnClearAllFlagC
                        {
                            comboBoxnClearAllFlagC.Items.Add("0:否");
                            comboBoxnClearAllFlagC.Items.Add("1:是");
                        }
                        // comboBoxnClearAllPriceTypeC
                        {
                            comboBoxnClearAllPriceTypeC.Items.Add("1:市價");
                            comboBoxnClearAllPriceTypeC.Items.Add("2:限價");
                        }
                        // comboBoxnFinalClearFlagC
                        {
                            comboBoxnFinalClearFlagC.Items.Add("0:否");
                            comboBoxnFinalClearFlagC.Items.Add("1:是");
                        }
                        // comboBoxnOrderTypeC
                        {
                            comboBoxnOrderTypeC.Items.Add("0:現股");
                            comboBoxnOrderTypeC.Items.Add("3:融資");
                            comboBoxnOrderTypeC.Items.Add("4:融券");
                        }
                        // comboBoxnClearOrderCondC
                        {
                            comboBoxnClearOrderCondC.Items.Add("0:ROD");
                            comboBoxnClearOrderCondC.Items.Add("3:IOC");
                            comboBoxnClearOrderCondC.Items.Add("4:FOK");
                        }

                        // comboBoxnLTEFlag
                        {
                            comboBoxnLTEFlag.Items.Add("0:否");
                            comboBoxnLTEFlag.Items.Add("1:是");
                        }
                        // comboBoxnLTEMarketPrice
                        {
                            comboBoxnLTEMarketPrice.Items.Add("1:市價");
                            comboBoxnLTEMarketPrice.Items.Add("2:限價");
                        }
                        // comboBoxnGTEFlag
                        {
                            comboBoxnGTEFlag.Items.Add("0:否");
                            comboBoxnGTEFlag.Items.Add("1:是");
                        }
                        // comboBoxnGTEMarketPrice
                        {
                            comboBoxnGTEMarketPrice.Items.Add("1:市價");
                            comboBoxnGTEMarketPrice.Items.Add("2:限價");
                        }
                        // comboBoxnGTEOrderCond
                        {
                            comboBoxnGTEOrderCond.Items.Add("0:ROD");
                            comboBoxnGTEOrderCond.Items.Add("3:IOC");
                            comboBoxnGTEOrderCond.Items.Add("4:FOK");
                        }
                        // comboBoxnLTEOrderCond
                        {
                            comboBoxnLTEOrderCond.Items.Add("0:ROD");
                            comboBoxnLTEOrderCond.Items.Add("3:IOC");
                            comboBoxnLTEOrderCond.Items.Add("4:FOK");
                        }
                    }
                    // MIT
                    {
                        // comboBoxnOrderTypeMIT
                        {
                            comboBoxnOrderTypeMIT.Items.Add("0:現股");
                            comboBoxnOrderTypeMIT.Items.Add("3:融資");
                            comboBoxnOrderTypeMIT.Items.Add("4:融券");
                            comboBoxnOrderTypeMIT.Items.Add("8:無券普賣");
                        }
                        // comboBoxnBuySellMIT
                        {
                            comboBoxnBuySellMIT.Items.Add("0:買");
                            comboBoxnBuySellMIT.Items.Add("1:賣");
                        }
                        // comboBoxnOrderPriceTypeMIT
                        {
                            comboBoxnOrderPriceTypeMIT.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeMIT.Items.Add("2:限價");
                        }
                        // comboBoxnOrderCondMIT
                        {
                            comboBoxnOrderCondMIT.Items.Add("0:ROD");
                            comboBoxnOrderCondMIT.Items.Add("3:IOC");
                            comboBoxnOrderCondMIT.Items.Add("4:FOK");
                        }
                        // comboBoxnTriggerDirMIT
                        {
                            comboBoxnTriggerDirMIT.Items.Add("1:GTE大於等於");
                            comboBoxnTriggerDirMIT.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnLongActionFlagMIT
                        {
                            comboBoxnLongActionFlagMIT.Items.Add("0:否");
                            comboBoxnLongActionFlagMIT.Items.Add("1:是");
                        }
                        // comboBoxnLATypeMIT
                        {
                            comboBoxnLATypeMIT.Items.Add("0:非長效單");
                            comboBoxnLATypeMIT.Items.Add("1:效期內觸發即失效");
                            comboBoxnLATypeMIT.Items.Add("3:效期內完全成交即失效");
                        }

                        // comboBoxnPreRiskFlag
                        {
                            comboBoxnPreRiskFlag.Items.Add("0:關閉預風控");
                            comboBoxnPreRiskFlag.Items.Add("1:開啟預風控");
                        }
                    }
                    // OCO
                    {
                        // comboBoxnBuySellUp
                        {
                            comboBoxnBuySellUp.Items.Add("0:買");
                            comboBoxnBuySellUp.Items.Add("1:賣");
                        }
                        // comboBoxnBuySellDown
                        {
                            comboBoxnBuySellDown.Items.Add("0:買");
                            comboBoxnBuySellDown.Items.Add("1:賣");
                        }
                        // comboBoxnOrderCondUp
                        {
                            comboBoxnOrderCondUp.Items.Add("0:ROD");
                            comboBoxnOrderCondUp.Items.Add("3:IOC");
                            comboBoxnOrderCondUp.Items.Add("4:FOK");
                        }
                        // comboBoxnOrderCondDown
                        {
                            comboBoxnOrderCondDown.Items.Add("0:ROD");
                            comboBoxnOrderCondDown.Items.Add("3:IOC");
                            comboBoxnOrderCondDown.Items.Add("4:FOK");
                        }
                        // comboBoxnOrderTypeUp
                        {
                            comboBoxnOrderTypeUp.Items.Add("0:現股");
                            comboBoxnOrderTypeUp.Items.Add("3:融資");
                            comboBoxnOrderTypeUp.Items.Add("4:融券");
                            comboBoxnOrderTypeUp.Items.Add("8:無券普賣");
                        }
                        // comboBoxnOrderTypeDown
                        {
                            comboBoxnOrderTypeDown.Items.Add("0:現股");
                            comboBoxnOrderTypeDown.Items.Add("3:融資");
                            comboBoxnOrderTypeDown.Items.Add("4:融券");
                            comboBoxnOrderTypeDown.Items.Add("8:無券普賣");
                        }
                        // comboBoxnOrderPriceTypeUp
                        {
                            comboBoxnOrderPriceTypeUp.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeUp.Items.Add("2:限價");
                        }
                        // comboBoxnOrderPriceTypeDown
                        {
                            comboBoxnOrderPriceTypeDown.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeDown.Items.Add("2:限價");
                        }

                    }
                    // MIOC
                    {
                        // comboBoxnPrimeMIOC
                        {
                            comboBoxnPrimeMIOC.Items.Add("0:上市");
                            comboBoxnPrimeMIOC.Items.Add("1:上櫃");
                        }
                        // comboBoxnOrderTypeMIOC
                        {
                            comboBoxnOrderTypeMIOC.Items.Add("0:現股");
                            comboBoxnOrderTypeMIOC.Items.Add("3:融資");
                            comboBoxnOrderTypeMIOC.Items.Add("4:融券");
                            comboBoxnOrderTypeMIOC.Items.Add("8:無券普賣");
                        }

                        // comboBoxnBuySellMIOC1
                        {
                            comboBoxnBuySellMIOC1.Items.Add("1:買");
                            comboBoxnBuySellMIOC1.Items.Add("2:賣");
                        }
                     
                        // comboBoxnOrderPriceTypeMIOC
                        {
                            comboBoxnOrderPriceTypeMIOC.Items.Add("0:市價");
                            comboBoxnOrderPriceTypeMIOC.Items.Add("1:(買單)委賣價或(賣單)委買價");
                        }
                    }
                    // MST
                    {
                        // comboBoxnOrderTypeMST
                        {
                            comboBoxnOrderTypeMST.Items.Add("0:現股");
                            comboBoxnOrderTypeMST.Items.Add("3:融資");
                            comboBoxnOrderTypeMST.Items.Add("4:融券");
                            comboBoxnOrderTypeMST.Items.Add("8:無券普賣");
                        }
                        // comboBoxnBuySellMST
                        { 
                            comboBoxnBuySellMST.Items.Add("0:買");
                            comboBoxnBuySellMST.Items.Add("1:賣");
                        }
                        // comboBoxnOrderPriceTypeMST
                        {
                            comboBoxnOrderPriceTypeMST.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeMST.Items.Add("2:限價");
                        }
                        // comboBoxnOrderCondMST
                        {
                            comboBoxnOrderCondMST.Items.Add("0:ROD");
                            comboBoxnOrderCondMST.Items.Add("3:IOC");
                            comboBoxnOrderCondMST.Items.Add("4:FOK");
                        }
                        // comboBoxnTriggerDirMST
                        {
                            comboBoxnTriggerDirMST.Items.Add("1:GTE大於等於");
                            comboBoxnTriggerDirMST.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnPrimeMST
                        {
                            comboBoxnPrimeMST.Items.Add("0:上市");
                            comboBoxnPrimeMST.Items.Add("1:上櫃");
                        }
                        // comboBoxnLongActionFlagMST
                        {
                            comboBoxnLongActionFlagMST.Items.Add("0:否");
                            comboBoxnLongActionFlagMST.Items.Add("1:是");
                        }
                        // comboBoxnLATypeMST
                        {
                            comboBoxnLATypeMST.Items.Add("0:非長效單");
                            comboBoxnLATypeMST.Items.Add("1:效期內觸發即失效");
                        }

                        // comboBoxnTriggerMethod
                        {
                            comboBoxnTriggerMethod.Items.Add("0:否,由市價啟動");
                            comboBoxnTriggerMethod.Items.Add("1:是,由自訂價格啟動");
                        }

                    }
                    // AB
                    {
                        // comboBoxnOrderTypeAB
                        {
                            comboBoxnOrderTypeAB.Items.Add("0:現股");
                            comboBoxnOrderTypeAB.Items.Add("3:融資");
                            comboBoxnOrderTypeAB.Items.Add("4:融券");
                            comboBoxnOrderTypeAB.Items.Add("8:無券普賣");
                        }
                        // comboBoxnBuySellAB
                        {
                            comboBoxnBuySellAB.Items.Add("0:買");
                            comboBoxnBuySellAB.Items.Add("1:賣");
                        }
                        // comboBoxnOrderPriceTypeAB
                        {
                            comboBoxnOrderPriceTypeAB.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeAB.Items.Add("2:限價");
                        }
                        // comboBoxnOrderCondAB
                        {
                            comboBoxnOrderCondAB.Items.Add("0:ROD");
                            comboBoxnOrderCondAB.Items.Add("3:IOC");
                            comboBoxnOrderCondAB.Items.Add("4:FOK");
                        }
                        // comboBoxnTriggerDirAB
                        {
                            comboBoxnTriggerDirAB.Items.Add("1:GTE大於等於");
                            comboBoxnTriggerDirAB.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnPrimeAB
                        {
                            comboBoxnPrimeAB.Items.Add("0:上市");
                            comboBoxnPrimeAB.Items.Add("1:上櫃");
                        }

                        // comboBoxnMarketNo
                        {
                            comboBoxnMarketNo.Items.Add("1:國內證");
                            comboBoxnMarketNo.Items.Add("2:國內期");
                            comboBoxnMarketNo.Items.Add("3:國外證");
                            comboBoxnMarketNo.Items.Add("4:國外期");
                        }
                        // comboBoxnReserved
                        {
                            comboBoxnReserved.Items.Add("0:否");
                            comboBoxnReserved.Items.Add("1:是");
                        }
                    }
                    // CB
                    {
                        // comboBoxnOrderTypeCB
                        {
                            comboBoxnOrderTypeCB.Items.Add("0:現");
                            comboBoxnOrderTypeCB.Items.Add("3:(自)融資");
                            comboBoxnOrderTypeCB.Items.Add("4:(自)融券");
                            comboBoxnOrderTypeCB.Items.Add("8:無券賣出");
                        }
                        // comboBoxnOrderPriceCondCB
                        {
                            comboBoxnOrderPriceCondCB.Items.Add("0:ROD");
                            comboBoxnOrderPriceCondCB.Items.Add("3:IOC");
                            comboBoxnOrderPriceCondCB.Items.Add("4:FOK");
                        }
                        // comboBoxnBuySellCB
                        {
                            comboBoxnBuySellCB.Items.Add("0:買");
                            comboBoxnBuySellCB.Items.Add("1:賣");
                        }
                        // comboBoxnOrderPriceTypeCB
                        {
                            comboBoxnOrderPriceTypeCB.Items.Add("1:市價");
                            comboBoxnOrderPriceTypeCB.Items.Add("2:限價");
                        }
                        // comboBoxnInnerOrderIsMITCB
                        {
                            comboBoxnInnerOrderIsMITCB.Items.Add("0:N");
                            comboBoxnInnerOrderIsMITCB.Items.Add("1:Y");
                        }
                        // comboBoxnStopLossFlagCB
                        {
                            comboBoxnStopLossFlagCB.Items.Add("0:否");
                            comboBoxnStopLossFlagCB.Items.Add("1:是");
                        }
                        // comboBoxnSLDirCB
                        {
                            comboBoxnSLDirCB.Items.Add("0:None");
                            comboBoxnSLDirCB.Items.Add("1:GTE大於等於");
                            comboBoxnSLDirCB.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnTickFlagCB
                        {
                            comboBoxnTickFlagCB.Items.Add("0:否");
                            comboBoxnTickFlagCB.Items.Add("1:是");
                        }
                        // comboBoxnPreQtyFlagCB
                        {
                            comboBoxnPreQtyFlagCB.Items.Add("0:否");
                            comboBoxnPreQtyFlagCB.Items.Add("1:是");
                        }
                        // comboBoxnPreQtyDirCB
                        {
                            comboBoxnPreQtyDirCB.Items.Add("0:None");
                            comboBoxnPreQtyDirCB.Items.Add("1:GTE大於等於");
                            comboBoxnPreQtyDirCB.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnSumQtyFlagCB
                        {
                            comboBoxnSumQtyFlagCB.Items.Add("0:否");
                            comboBoxnSumQtyFlagCB.Items.Add("1:是");
                        }
                        // comboBoxnClearAllFlagCB
                        {
                            comboBoxnClearAllFlagCB.Items.Add("0:否");
                            comboBoxnClearAllFlagCB.Items.Add("1:是");
                        }
                        // comboBoxnFinalClearFlagCB
                        {
                            comboBoxnFinalClearFlagCB.Items.Add("0:否");
                            comboBoxnFinalClearFlagCB.Items.Add("1:是");
                        }
                        // comboBoxnMITDirCB
                        {
                            comboBoxnMITDirCB.Items.Add("0:None");
                            comboBoxnMITDirCB.Items.Add("1:GTE大於等於");
                            comboBoxnMITDirCB.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnTakeProfitFlagCB
                        {
                            comboBoxnTakeProfitFlagCB.Items.Add("0:否");
                            comboBoxnTakeProfitFlagCB.Items.Add("1:是");
                        }

                        // comboBoxnTPDir
                        {
                            comboBoxnTPDir.Items.Add("0:None");
                            comboBoxnTPDir.Items.Add("1:GTE大於等於");
                            comboBoxnTPDir.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnTickDir
                        {
                            comboBoxnTickDir.Items.Add("0:None");
                            comboBoxnTickDir.Items.Add("1:GTE大於等於");
                            comboBoxnTickDir.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnUpDownFlag
                        {
                            comboBoxnUpDownFlag.Items.Add("0:否");
                            comboBoxnUpDownFlag.Items.Add("1:是");
                        }
                        // comboBoxnUpDownDir
                        {
                            comboBoxnUpDownDir.Items.Add("0:None");
                            comboBoxnUpDownDir.Items.Add("1:GTE大於等於");
                            comboBoxnUpDownDir.Items.Add("2:LTE小於等於");
                        }
                        // comboBoxnSumQtyDir
                        {
                            comboBoxnSumQtyDir.Items.Add("0:None");
                            comboBoxnSumQtyDir.Items.Add("1:GTE大於等於");
                            comboBoxnSumQtyDir.Items.Add("2:LTE小於等於");
                        }
                    }

                    // 刪單
                    {
                        // comboBoxnTradeKindVersion
                        {
                            comboBoxnTradeKindVersion.Items.Add("舊版");
                            comboBoxnTradeKindVersion.Items.Add("V1版");
                        }
                        // comboBoxnTradeKind
                        {
                            comboBoxnTradeKind.Items.Add("6:MIOC");
                            comboBoxnTradeKind.Items.Add("7:MST");
                            comboBoxnTradeKind.Items.Add("8:MIT");
                        }
                        // comboBoxnTradeKindV1
                        {
                            comboBoxnTradeKindV1.Items.Add("3:OCO");
                            comboBoxnTradeKindV1.Items.Add("8:MIT");
                            comboBoxnTradeKindV1.Items.Add("9:MST");
                            comboBoxnTradeKindV1.Items.Add("10:AB");
                            comboBoxnTradeKindV1.Items.Add("11:當沖");
                            comboBoxnTradeKindV1.Items.Add("17:出清");
                            comboBoxnTradeKindV1.Items.Add("27：CB");
                        }
                        // comboBoxnDeletType
                        {
                            comboBoxnDeletType.Items.Add("1:全部");
                            comboBoxnDeletType.Items.Add("2:進場單");
                            comboBoxnDeletType.Items.Add("3:出場單");
                        }
                        // comboBoxnMarket
                        {
                            comboBoxnMarket.Items.Add("1：國內證");
                            comboBoxnMarket.Items.Add("2：國內期");
                            comboBoxnMarket.Items.Add("3：國外證");
                            comboBoxnMarket.Items.Add("4：國外期");
                        }
                    }

                    // 查詢
                    {
                        // comboBoxbstrKind
                        {
                            comboBoxbstrKind.Items.Add("DayTrade:當沖");
                            comboBoxbstrKind.Items.Add("ClearOut:出清");
                            comboBoxbstrKind.Items.Add("MIT：觸價單、MIT長效單");
                            comboBoxbstrKind.Items.Add("OCO：二擇一");
                            comboBoxbstrKind.Items.Add("MIOC：多次IOC");
                            comboBoxbstrKind.Items.Add("MST：移動停損、MST長效單");
                            comboBoxbstrKind.Items.Add("AB：看A下B單");
                            comboBoxbstrKind.Items.Add("CB：自組單");
                        }
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
        private void comboBoxUserID_DropDown(object sender, EventArgs e)
        {
            m_dictUserID.Clear(); //清空之前的帳號

            // 取回可交易的所有帳號
            int nCode = m_pSKOrder.GetUserAccount();
            // 取得回傳訊息
            string msg = "【GetUserAccount】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void SendOrderForm_Load(object sender, EventArgs e)
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
            //<新版證券智慧單被動回報>依各種策略單別提供格式內容
            m_pSKOrder.OnTSSmartStrategyReport += new _ISKOrderLibEvents_OnTSSmartStrategyReportEventHandler(OnTSSmartStrategyReport);
            void OnTSSmartStrategyReport(string bstrData)
            {
                richTextBoxOnTSSmartStrategyReport.AppendText(bstrData + '\n');
            }
        }
        private void buttonGetTSSmartStrategyReport_Click(object sender, EventArgs e)
        {
            string bstrKind = "DayTrade";
            string bstrDate = textBoxbstrDate.Text; // 查詢日期(ex:20220601)

            if (comboBoxbstrKind.Text == "DayTrade:當沖") bstrKind = "DayTrade";
            else if (comboBoxbstrKind.Text == "ClearOut:出清") bstrKind = "ClearOut";
            else if (comboBoxbstrKind.Text == "MIT：觸價單、MIT長效單") bstrKind = "MIT";
            else if (comboBoxbstrKind.Text == "OCO：二擇一") bstrKind = "OCO";
            else if (comboBoxbstrKind.Text == "MIOC：多次IOC") bstrKind = "MIOC";
            else if (comboBoxbstrKind.Text == "MST：移動停損、MST長效單") bstrKind = "MST";
            else if (comboBoxbstrKind.Text == "AB：看A下B單") bstrKind = "AB";
            else if (comboBoxbstrKind.Text == "CB：自組單") bstrKind = "CB";

            // 查詢證券智慧單
            int nCode = m_pSKOrder.GetTSSmartStrategyReport(comboBoxUserID.Text, comboBoxAccount.Text, "TS", 0, bstrKind, bstrDate);
            // 取得回傳訊息
            string msg = "【GetTSSmartStrategyReport】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonCancelTSStrategyOrder_Click(object sender, EventArgs e)
        {
            int nTradeKind = 0; // 智慧單類型6:MIOC;7:MST; 8:MIT。
            string bstrMessage;
            if (comboBoxnTradeKind.Text == "6:MIOC") nTradeKind = 6;
            else if (comboBoxnTradeKind.Text == "7:MST") nTradeKind = 7;
            else if (comboBoxnTradeKind.Text == "8:MIT") nTradeKind = 8;
            // 取消證券智慧單委託。欄位請參考GetTSStrategyOrder 回傳的內容。注意，當已經觸發的智慧單，將無法取消委託。
            int nCode = m_pSKOrder.CancelTSStrategyOrder(comboBoxUserID.Text, bAsyncOrder, comboBoxAccount.Text, textBoxbstrSmartKey.Text, nTradeKind, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelTSStrategyOrder】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonCancelTSStrategyOrderV1_Click(object sender, EventArgs e)
        {
            CANCELSTRATEGYORDER pCancelOrder = new CANCELSTRATEGYORDER(); // 智慧單刪單物件
            pCancelOrder.bstrFullAccount = comboBoxAccount.Text;
            //市場別 1：國內證、2：國內期、3：國外證、4：國外期
            if (comboBoxnMarket.Text == "1：國內證") pCancelOrder.nMarket = 1;
            else if (comboBoxnMarket.Text == "2：國內期") pCancelOrder.nMarket = 2;
            else if (comboBoxnMarket.Text == "3：國外證") pCancelOrder.nMarket = 3;
            else if (comboBoxnMarket.Text == "4：國外期") pCancelOrder.nMarket = 4;

            pCancelOrder.bstrParentSmartKey = textBoxbstrParentSmartKey.Text; // 智慧母單號; 
            pCancelOrder.bstrSmartKey = textBoxbstrSmartKey.Text; // 智慧單號; 出場單則給出場單號

            if (comboBoxnTradeKindV1.Text == "3:OCO") pCancelOrder.nTradeKind = 3;
            else if (comboBoxnTradeKindV1.Text == "8:MIT") pCancelOrder.nTradeKind = 8;
            else if (comboBoxnTradeKindV1.Text == "9:MST") pCancelOrder.nTradeKind = 9;
            else if (comboBoxnTradeKindV1.Text == "10:AB") pCancelOrder.nTradeKind = 10;
            else if (comboBoxnTradeKindV1.Text == "11:當沖") pCancelOrder.nTradeKind = 11;
            else if (comboBoxnTradeKindV1.Text == "17:出清") pCancelOrder.nTradeKind = 17;
            else if (comboBoxnTradeKindV1.Text == "27：CB") pCancelOrder.nTradeKind = 27;
            else pCancelOrder.nTradeKind = 0;
            if (comboBoxnDeletType.Text == "1:全部") pCancelOrder.nDeleteType = 1;
            else if (comboBoxnDeletType.Text == "2:進場單") pCancelOrder.nDeleteType = 2;
            else if (comboBoxnDeletType.Text == "3:出場單") pCancelOrder.nDeleteType = 3;

            pCancelOrder.bstrSeqNo = textBoxbstrSeqNo.Text;//委託序號(1~3三種類型均需提供, 預約單可忽略)
            pCancelOrder.bstrOrderNo = textBoxbstrOrderNo.Text; //((刪進場/出場單提供))委託書號，（若觸發，需給書號）
            pCancelOrder.bstrSmartKeyOut = textBoxbstrSmartKeyOut.Text; //出場單號(刪全部時提供，刪進場單及出場單

            string bstrMessage;
            // 取消證券智慧單委託新版本。刪單欄位請參考GetTSStrategyOrder 回傳的內容。
            int nCode = m_pSKOrder.CancelTSStrategyOrderV1(comboBoxUserID.Text, pCancelOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelTSStrategyOrderV1】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonSendStockStrategyDayTrade_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyDT.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDER pOrder = new STOCKSTRATEGYORDER();
                pOrder.bstrFullAccount = comboBoxAccount.Text;

                pOrder.bstrStockNo = textBoxbstrStockNoDT.Text;
                pOrder.nQty = int.Parse(textBoxnQtyDT.Text);
                pOrder.bstrOrderPrice = textBoxbstrOrderPriceDT.Text;

                if (comboBoxnBuySellDT.Text == "0:現股買") pOrder.nBuySell = 0;
                else if (comboBoxnBuySellDT.Text == "1:無券賣出") pOrder.nBuySell = 1;

                if (comboBoxnOrderPriceCondDT.Text == "0:ROD") pOrder.nOrderPriceCond = 0;
                else if (comboBoxnOrderPriceCondDT.Text == "3:IOC") pOrder.nOrderPriceCond = 3;
                else if (comboBoxnOrderPriceCondDT.Text == "4:FOK") pOrder.nOrderPriceCond = 4;

                if (comboBoxnOrderPriceTypeDT.Text == "1:市價") pOrder.nOrderPriceType = 1;
                else if (comboBoxnOrderPriceTypeDT.Text == "2:限價") pOrder.nOrderPriceType = 2;

                if (comboBoxnInnerOrderIsMITDT.Text == "0:N") pOrder.nInnerOrderIsMIT = 0;
                else if (comboBoxnInnerOrderIsMITDT.Text == "1:Y") pOrder.nInnerOrderIsMIT = 1;

                if (comboBoxnMITDirDT.Text == "0:未啟用MIT") pOrder.nMITDir = 0;
                else if (comboBoxnMITDirDT.Text == "1:向上觸發(大於等於)") pOrder.nMITDir = 1;
                else if (comboBoxnMITDirDT.Text == "2:向下觸發(小於等於)") pOrder.nMITDir = 2;

                pOrder.bstrMITTriggerPrice = textBoxbstrMITTriggerPriceDT.Text;
                pOrder.bstrMITDealPrice = textBoxbstrMITDealPriceDT.Text;

                if (comboBoxnClearAllFlagDT.Text == "0:否") pOrder.nClearAllFlag = 0;
                else if (comboBoxnClearAllFlagDT.Text == "1:是") pOrder.nClearAllFlag = 1;

                pOrder.bstrClearCancelTime = textBoxbstrClearCancelTimeDT.Text;

                if (comboBoxnClearAllPriceTypeDT.Text == "1:市價") pOrder.nClearAllPriceType = 1;
                else if (comboBoxnClearAllPriceTypeDT.Text == "2:限價") pOrder.nClearAllPriceType = 2;

                pOrder.bstrClearAllOrderPrice = textBoxbstrClearAllOrderPriceDT.Text;

                if (comboBoxnFinalClearFlagDT.Text == "0:否") pOrder.nFinalClearFlag = 0;
                else if (comboBoxnFinalClearFlagDT.Text == "1:是") pOrder.nFinalClearFlag = 1;

                if (comboBoxnTakeProfitFlagDT.Text == "0:否") pOrder.nTakeProfitFlag = 0;
                else if (comboBoxnTakeProfitFlagDT.Text == "1:是") pOrder.nTakeProfitFlag = 1;

                if (comboBoxnRDOTPPercent.Text == "0:觸發價") pOrder.nRDOTPPercent = 0;
                else if (comboBoxnRDOTPPercent.Text == "1:漲幅") pOrder.nRDOTPPercent = 1;

                pOrder.bstrTPPercent = textBoxbstrTPPercent.Text;
                pOrder.bstrTPTrigger = textBoxbstrTPTrigger.Text;

                if (comboBoxnRDTPMarketPriceType.Text == "1:市價") pOrder.nRDTPMarketPriceType = 1;
                else if (comboBoxnRDTPMarketPriceType.Text == "2:限價") pOrder.nRDTPMarketPriceType = 2;

                pOrder.bstrTPOrderPrice = textBoxbstrTPOrderPriceDT.Text;

                if (comboBoxnStopLossFlagDT.Text == "0:否") pOrder.nStopLossFlag = 0;
                else if (comboBoxnStopLossFlagDT.Text == "1:是") pOrder.nStopLossFlag = 1;

                if (comboBoxnRDOSLPercent.Text == "0:觸發價") pOrder.nRDOSLPercent = 0;
                else if (comboBoxnRDOSLPercent.Text == "1:漲跌幅") pOrder.nRDOSLPercent = 1;

                pOrder.bstrSLPercent = textBoxbstrSLPercentDT.Text;
                pOrder.bstrSLTrigger = textBoxbstrSLTrigger.Text;

                if (comboBoxnRDSLMarketPriceType.Text == "1:市價") pOrder.nRDSLMarketPriceType = 1;
                else if (comboBoxnRDSLMarketPriceType.Text == "2:限價") pOrder.nRDSLMarketPriceType = 2;

                pOrder.bstrSLOrderPrice = textBoxbstrSLOrderPriceDT.Text;

                if (comboBoxnTakeProfitOrderCond.Text == "0:ROD") pOrder.nTakeProfitOrderCond = 0;
                else if (comboBoxnTakeProfitOrderCond.Text == "3:IOC") pOrder.nTakeProfitOrderCond = 3;
                else if (comboBoxnTakeProfitOrderCond.Text == "4:FOK") pOrder.nTakeProfitOrderCond = 4;

                if (comboBoxnStopLossOrderCond.Text == "0:ROD") pOrder.nStopLossOrderCond = 0;
                else if (comboBoxnStopLossOrderCond.Text == "3:IOC") pOrder.nStopLossOrderCond = 3;
                else if (comboBoxnStopLossOrderCond.Text == "4:FOK") pOrder.nStopLossOrderCond = 4;

                if (comboBoxnClearOrderCondDT.Text == "0:ROD") pOrder.nClearOrderCond = 0;
                else if (comboBoxnClearOrderCondDT.Text == "3:IOC") pOrder.nClearOrderCond = 3;
                else if (comboBoxnClearOrderCondDT.Text == "4:FOK") pOrder.nClearOrderCond = 4;

                string bstrMessage;
                // 送出證券智慧單當沖條件委託。
                int nCode = m_pSKOrder.SendStockStrategyDayTrade(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyDayTrade】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyClear_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyC.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDEROUT pOrder = new STOCKSTRATEGYORDEROUT();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoC.Text;
                pOrder.bstrClearCancelTime = textBoxbstrClearCancelTimeC.Text;
                pOrder.bstrClearAllOrderPrice = textBoxbstrClearAllOrderPriceC.Text;

                pOrder.bstrLTETriggerPrice = textBoxbstrLTETriggerPrice.Text;
                pOrder.bstrLTEOrderPrice = textBoxbstrLTEOrderPrice.Text;
                pOrder.bstrGTETriggerPrice = textBoxbstrGTETriggerPrice.Text;
                pOrder.bstrGTEOrderPrice = textBoxbstrGTEOrderPrice.Text;

                pOrder.nQty = int.Parse(textBoxnQtyC.Text);

                if (comboBoxnBuySellC.Text == "0:買") pOrder.nBuySell = 0;
                else if (comboBoxnBuySellC.Text == "1:賣") pOrder.nBuySell = 1;

                if (comboBoxnClearAllFlagC.Text == "0:否") pOrder.nClearAllFlag = 0;
                else if (comboBoxnClearAllFlagC.Text == "1:是") pOrder.nClearAllFlag = 1;

                if (comboBoxnClearAllPriceTypeC.Text == "1:市價") pOrder.nClearAllPriceType = 1;
                else if (comboBoxnClearAllPriceTypeC.Text == "2:限價") pOrder.nClearAllPriceType = 2;

                if (comboBoxnFinalClearFlagC.Text == "0:否") pOrder.nFinalClearFlag = 0;
                else if (comboBoxnFinalClearFlagC.Text == "1:是") pOrder.nFinalClearFlag = 1;

                if (comboBoxnOrderTypeC.Text == "0:現股") pOrder.nOrderType = 0;
                else if (comboBoxnOrderTypeC.Text == "3:融資") pOrder.nOrderType = 3;
                else if (comboBoxnOrderTypeC.Text == "4:融券") pOrder.nOrderType = 4;

                if (comboBoxnLTEFlag.Text == "0:否") pOrder.nLTEFlag = 0;
                else if (comboBoxnLTEFlag.Text == "1:是") pOrder.nLTEFlag = 1;

                if (comboBoxnLTEMarketPrice.Text == "1:市價") pOrder.nLTEMarketPrice = 1;
                else if (comboBoxnLTEMarketPrice.Text == "2:限價") pOrder.nLTEMarketPrice = 2;

                if (comboBoxnGTEFlag.Text == "0:否") pOrder.nGTEFlag = 0;
                else if (comboBoxnGTEFlag.Text == "1:是") pOrder.nGTEFlag = 1;

                if (comboBoxnGTEMarketPrice.Text == "1:市價") pOrder.nGTEMarketPrice = 1;
                else if (comboBoxnGTEMarketPrice.Text == "2:限價") pOrder.nGTEMarketPrice = 2;

                if (comboBoxnGTEOrderCond.Text == "0:ROD") pOrder.nGTEOrderCond = 0;
                else if (comboBoxnGTEOrderCond.Text == "3:IOC") pOrder.nGTEOrderCond = 3;
                else if (comboBoxnGTEOrderCond.Text == "4:FOK") pOrder.nGTEOrderCond = 4;

                if (comboBoxnLTEOrderCond.Text == "0:ROD") pOrder.nLTEOrderCond = 0;
                else if (comboBoxnLTEOrderCond.Text == "3:IOC") pOrder.nLTEOrderCond = 3;
                else if (comboBoxnLTEOrderCond.Text == "4:FOK") pOrder.nLTEOrderCond = 4;

                if (comboBoxnClearOrderCondC.Text == "0:ROD") pOrder.nClearOrderCond = 0;
                else if (comboBoxnClearOrderCondC.Text == "3:IOC") pOrder.nClearOrderCond = 3;
                else if (comboBoxnClearOrderCondC.Text == "4:FOK") pOrder.nClearOrderCond = 4;

                string bstrMessage;
                // 送出證券智慧單出清條件委託。
                int nCode = m_pSKOrder.SendStockStrategyClear(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyClear】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyMIT_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyMIT.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDERMIT pOrder = new STOCKSTRATEGYORDERMIT();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoMIT.Text;
                pOrder.bstrOrderPrice = textBoxbstrOrderPriceMIT.Text;
                pOrder.bstrTriggerPrice = textBoxbstrTriggerPriceMIT.Text;
                pOrder.bstrDealPrice = textBoxbstrDealPriceMIT.Text;
                pOrder.bstrLongEndDate = textBoxbstrLongEndDateMIT.Text;
                pOrder.nQty = int.Parse(textBoxnQtyMIT.Text);

                if (comboBoxnOrderTypeMIT.Text == "0:現股") pOrder.nOrderType = 0;
                else if (comboBoxnOrderTypeMIT.Text == "3:融資") pOrder.nOrderType = 3;
                else if (comboBoxnOrderTypeMIT.Text == "4:融券") pOrder.nOrderType = 4;
                else if (comboBoxnOrderTypeMIT.Text == "8:無券普賣") pOrder.nOrderType = 8;

                if (comboBoxnBuySellMIT.Text == "0:買") pOrder.nBuySell = 0;
                else if (comboBoxnBuySellMIT.Text == "1:賣") pOrder.nBuySell = 1;

                if (comboBoxnOrderPriceTypeMIT.Text == "1:市價") pOrder.nOrderPriceType = 1;
                else if (comboBoxnOrderPriceTypeMIT.Text == "2:限價") pOrder.nOrderPriceType = 2;

                if (comboBoxnOrderCondMIT.Text == "0:ROD") pOrder.nOrderCond = 0;
                else if (comboBoxnOrderCondMIT.Text == "3:IOC") pOrder.nOrderCond = 3;
                else if (comboBoxnOrderCondMIT.Text == "4:FOK") pOrder.nOrderCond = 4;

                if (comboBoxnTriggerDirMIT.Text == "1:GTE大於等於") pOrder.nTriggerDir = 1;
                else if (comboBoxnTriggerDirMIT.Text == "2:LTE小於等於") pOrder.nTriggerDir = 2;

                if (comboBoxnPreRiskFlag.Text == "0:關閉預風控") pOrder.nPreRiskFlag = 0;
                else if (comboBoxnPreRiskFlag.Text == "1:開啟預風控") pOrder.nPreRiskFlag = 1;

                if (comboBoxnLongActionFlagMIT.Text == "0:否") pOrder.nLongActionFlag = 0;
                else if (comboBoxnLongActionFlagMIT.Text == "1:是") pOrder.nLongActionFlag = 1;

                if (comboBoxnLATypeMIT.Text == "1:效期內觸發即失效") pOrder.nLAType = 1;
                else if (comboBoxnLATypeMIT.Text == "3:效期內完全成交即失效") pOrder.nLAType = 3;

                string bstrMessage;
                // 送出證券智慧單MIT條件委託。
                int nCode = m_pSKOrder.SendStockStrategyMIT(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyMIT】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyOCO_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyOCO.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDEROCO pOrder = new STOCKSTRATEGYORDEROCO();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoOCO.Text;
                pOrder.nQty = int.Parse(textBoxnQtyOCO.Text);
                pOrder.bstrOrderPrice = textBoxbstrOrderPriceOCO.Text;

                pOrder.bstrTriggerUp = textBoxbstrTriggerUp.Text;
                pOrder.bstrOrderPrice2 = textBoxbstrOrderPrice2.Text;
                pOrder.bstrTriggerDown = textBoxbstrTriggerDown.Text;

                if (comboBoxnBuySellUp.Text == "0:買") pOrder.nBuySellUp = 0;
                else if (comboBoxnBuySellUp.Text == "1:賣") pOrder.nBuySellUp = 1;

                if (comboBoxnBuySellDown.Text == "0:買") pOrder.nBuySellDown = 0;
                else if (comboBoxnBuySellDown.Text == "1:賣") pOrder.nBuySellDown = 1;

                if (comboBoxnOrderCondUp.Text == "0:ROD") pOrder.nOrderCondUp = 0;
                else if (comboBoxnOrderCondUp.Text == "3:IOC") pOrder.nOrderCondUp = 3;
                else if (comboBoxnOrderCondUp.Text == "4:FOK") pOrder.nOrderCondUp = 4;

                if (comboBoxnOrderCondDown.Text == "0:ROD") pOrder.nOrderCondDown = 0;
                else if (comboBoxnOrderCondDown.Text == "3:IOC") pOrder.nOrderCondDown = 3;
                else if (comboBoxnOrderCondDown.Text == "4:FOK") pOrder.nOrderCondDown = 4;

                if (comboBoxnOrderTypeUp.Text == "0:現股") pOrder.nOrderTypeUp = 0;
                else if (comboBoxnOrderTypeUp.Text == "3:融資") pOrder.nOrderTypeUp = 3;
                else if (comboBoxnOrderTypeUp.Text == "4:融券") pOrder.nOrderTypeUp = 4;
                else if (comboBoxnOrderTypeUp.Text == "8:無券普賣") pOrder.nOrderTypeUp = 8;

                if (comboBoxnOrderTypeDown.Text == "0:現股") pOrder.nOrderTypeDown = 0;
                else if (comboBoxnOrderTypeDown.Text == "3:融資") pOrder.nOrderTypeDown = 3;
                else if (comboBoxnOrderTypeDown.Text == "4:融券") pOrder.nOrderTypeDown = 4;
                else if (comboBoxnOrderTypeDown.Text == "8:無券普賣") pOrder.nOrderTypeDown = 8;

                if (comboBoxnOrderPriceTypeUp.Text == "1:市價") pOrder.nOrderPriceTypeUp = 1;
                else if (comboBoxnOrderPriceTypeUp.Text == "2:限價") pOrder.nOrderPriceTypeUp = 2;

                if (comboBoxnOrderPriceTypeDown.Text == "1:市價") pOrder.nOrderPriceTypeDown = 1;
                else if (comboBoxnOrderPriceTypeDown.Text == "2:限價") pOrder.nOrderPriceTypeDown = 2;

                string bstrMessage;
                // 送出證券智慧單二擇一OCO條件委託。
                int nCode = m_pSKOrder.SendStockStrategyOCO(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyOCO】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyMIOC_Click(object sender, EventArgs e)
        {
            if (textBoxnOneceQtyLimit.Text == "" || textBoxnTotalQty.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數/單次交易張數上限)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDERMIOC pOrder = new STOCKSTRATEGYORDERMIOC();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoMIOC.Text;
                pOrder.bstrOrderPriceUp = textBoxbstrOrderPriceUp.Text;
                pOrder.bstrOrderPriceDown = textBoxbstrOrderPriceDown.Text;
                pOrder.nOneceQtyLimit = int.Parse(textBoxnOneceQtyLimit.Text);
                pOrder.nTotalQty = int.Parse(textBoxnTotalQty.Text);

                if (comboBoxnPrimeMIOC.Text == "0:上市") pOrder.nPrime = 0;
                else if (comboBoxnPrimeMIOC.Text == "1:上櫃") pOrder.nPrime = 1;

                if (comboBoxnBuySellMIOC1.Text == "1:買") pOrder.nBuySell = 1;
                else if (comboBoxnBuySellMIOC1.Text == "2:賣") pOrder.nBuySell = 2;

                if (comboBoxnOrderTypeMIOC.Text == "0:現股") pOrder.nOrderType = 0;
                else if (comboBoxnOrderTypeMIOC.Text == "3:融資") pOrder.nOrderType = 3;
                else if (comboBoxnOrderTypeMIOC.Text == "4:融券") pOrder.nOrderType = 4;
                else if (comboBoxnOrderTypeMIOC.Text == "8:無券普賣") pOrder.nOrderType = 8;

                if (comboBoxnOrderPriceTypeMIOC.Text == "0:市價") pOrder.nOrderPriceType = 1;
                else if (comboBoxnOrderPriceTypeMIOC.Text == "1:(買單)委賣價或(賣單)委買價") pOrder.nOrderPriceType = 2;

                string bstrMessage;
                // 送出證券智慧單多次IOC條件委託。
                int nCode = m_pSKOrder.SendStockStrategyMIOC(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyMIOC】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyMST_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyMST.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDERMIT pOrder = new STOCKSTRATEGYORDERMIT();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoMST.Text;
                pOrder.bstrOrderPrice = textBoxbstrOrderPriceMST.Text;
                pOrder.bstrMovePoint = textBoxbstrMovePoint.Text;
                pOrder.bstrDealPrice = textBoxbstrDealPriceMST.Text;
                pOrder.bstrStartPrice = textBoxbstrStartPriceMST.Text;
                pOrder.nQty = int.Parse(textBoxnQtyMST.Text);
                pOrder.bstrLongEndDate = textBoxbstrLongEndDateMST.Text;

                if (comboBoxnOrderTypeMST.Text == "0:現股") pOrder.nOrderType = 0;
                else if (comboBoxnOrderTypeMST.Text == "3融資") pOrder.nOrderType = 3;
                else if (comboBoxnOrderTypeMST.Text == "4:融券") pOrder.nOrderType = 4;
                else if (comboBoxnOrderTypeMST.Text == "8:無券普賣") pOrder.nOrderType = 8;

                if (comboBoxnBuySellMST.Text == "0:買") pOrder.nBuySell = 0;
                else if (comboBoxnBuySellMST.Text == "1:賣") pOrder.nBuySell = 1;

                if (comboBoxnOrderPriceTypeMST.Text == "1:市價") pOrder.nOrderPriceType = 1;
                else if (comboBoxnOrderPriceTypeMST.Text == "2:限價") pOrder.nOrderPriceType = 2;

                if (comboBoxnOrderCondMST.Text == "0:ROD") pOrder.nOrderCond = 0;
                else if (comboBoxnOrderCondMST.Text == "3:IOC") pOrder.nOrderCond = 3;
                else if (comboBoxnOrderCondMST.Text == "4:FOK") pOrder.nOrderCond = 4;

                if (comboBoxnTriggerDirMST.Text == "1:GTE大於等於") pOrder.nTriggerDir = 1;
                else if (comboBoxnTriggerDirMST.Text == "2:LTE小於等於") pOrder.nTriggerDir = 2;

                if (comboBoxnTriggerMethod.Text == "0:否,由市價啟動") pOrder.nTriggerMethod = 0;
                else if (comboBoxnTriggerMethod.Text == "1:是,由自訂價格啟動") pOrder.nTriggerMethod = 1;

                if (comboBoxnPrimeMST.Text == "0:上市") pOrder.nPrime = 0;
                else if (comboBoxnPrimeMST.Text == "1:上櫃") pOrder.nPrime = 1;

                if (comboBoxnLongActionFlagMST.Text == "0:否") pOrder.nLongActionFlag = 0;
                else if (comboBoxnLongActionFlagMST.Text == "1:是") pOrder.nLongActionFlag = 1;

                if (comboBoxnLATypeMST.Text == "0:非長效單") pOrder.nLAType = 0;
                else if (comboBoxnLATypeMST.Text == "1:效期內觸發即失效") pOrder.nLAType = 1;

                string bstrMessage;
                // 送出證券智慧單移動停損條件委託。
                int nCode = m_pSKOrder.SendStockStrategyMST(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyMST】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyAB_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyAB.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDERMIT pOrder = new STOCKSTRATEGYORDERMIT();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoAB.Text;
                pOrder.bstrOrderPrice = textBoxbstrOrderPriceAB.Text;
                pOrder.bstrTriggerPrice = textBoxbstrTriggerPriceAB.Text;
                pOrder.bstrExchangeNo = textBoxbstrExchangeNo.Text;
                pOrder.bstrStockNo2 = textBoxbstrStockNo2.Text;
                pOrder.bstrStartPrice = textBoxbstrStartPriceAB.Text;
                pOrder.nQty = int.Parse(textBoxnQtyAB.Text);

                if (comboBoxnOrderTypeAB.Text == "0:現股") pOrder.nOrderType = 0;
                else if (comboBoxnOrderTypeAB.Text == "3融資") pOrder.nOrderType = 3;
                else if (comboBoxnOrderTypeAB.Text == "4:融券") pOrder.nOrderType = 4;
                else if (comboBoxnOrderTypeAB.Text == "8:無券普賣") pOrder.nOrderType = 8;

                if (comboBoxnBuySellAB.Text == "0:買") pOrder.nBuySell = 0;
                else if (comboBoxnBuySellAB.Text == "1:賣") pOrder.nBuySell = 1;

                if (comboBoxnOrderPriceTypeAB.Text == "1:市價") pOrder.nOrderPriceType = 1;
                else if (comboBoxnOrderPriceTypeAB.Text == "2:限價") pOrder.nOrderPriceType = 2;

                if (comboBoxnOrderCondAB.Text == "0:ROD") pOrder.nOrderCond = 0;
                else if (comboBoxnOrderCondAB.Text == "3:IOC") pOrder.nOrderCond = 3;
                else if (comboBoxnOrderCondAB.Text == "4:FOK") pOrder.nOrderCond = 4;

                if (comboBoxnTriggerDirAB.Text == "1:GTE大於等於") pOrder.nTriggerDir = 1;
                else if (comboBoxnTriggerDirAB.Text == "2:LTE小於等於") pOrder.nTriggerDir = 2;

                if (comboBoxnMarketNo.Text == "1:國內證") pOrder.nMarketNo = 1;
                else if (comboBoxnMarketNo.Text == "2:國內期") pOrder.nMarketNo = 2;
                else if (comboBoxnMarketNo.Text == "3:國外證") pOrder.nMarketNo = 3;
                else if (comboBoxnMarketNo.Text == "4:國外期") pOrder.nMarketNo = 4;

                if (comboBoxnReserved.Text == "0:否") pOrder.nReserved = 0;
                else if (comboBoxnReserved.Text == "1:是") pOrder.nReserved = 1;

                if (comboBoxnPrimeAB.Text == "0:上市") pOrder.nPrime = 1;
                else if (comboBoxnPrimeAB.Text == "1:上櫃") pOrder.nPrime = 2;

                string bstrMessage;
                // 送出證券智慧單看A下B單委託。
                int nCode = m_pSKOrder.SendStockStrategyAB(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyAB】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSendStockStrategyCB_Click(object sender, EventArgs e)
        {
            if (textBoxnQtyCB.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊(委託張數)！", "", MessageBoxButtons.OK);
            }
            else
            {
                STOCKSTRATEGYORDER pOrder = new STOCKSTRATEGYORDER();
                pOrder.bstrFullAccount = comboBoxAccount.Text;
                pOrder.bstrStockNo = textBoxbstrStockNoCB.Text;
                pOrder.nQty = int.Parse(textBoxnQtyCB.Text);
                pOrder.bstrOrderPrice = textBoxbstrOrderPriceCB.Text;
                pOrder.bstrMITTriggerPrice = textBoxbstrMITTriggerPriceCB.Text;
                pOrder.bstrTPOrderPrice = textBoxbstrTPOrderPriceCB.Text;
                pOrder.bstrSLOrderPrice = textBoxbstrSLOrderPriceCB.Text;
                pOrder.bstrTick = textBoxbstrTickCB.Text;
                pOrder.bstrSLPercent = textBoxbstrSLPercentCB.Text;
                pOrder.bstrPreQty = textBoxbstrPreQtyCB.Text;
                pOrder.bstrSumQty = textBoxbstrSumQtyCB.Text;
                pOrder.bstrClearCancelTime = textBoxbstrClearCancelTimeCB.Text;

                if (comboBoxnOrderTypeCB.Text == "0:現股") pOrder.nOrderType = 0;
                else if (comboBoxnOrderTypeCB.Text == "3:融資") pOrder.nOrderType = 3;
                else if (comboBoxnOrderTypeCB.Text == "4:融券") pOrder.nOrderType = 4;
                else if (comboBoxnOrderTypeCB.Text == "8:無券普賣") pOrder.nOrderType = 8;

                if (comboBoxnOrderPriceCondCB.Text == "0:ROD") pOrder.nOrderPriceCond = 0;
                else if (comboBoxnOrderPriceCondCB.Text == "3:IOC") pOrder.nOrderPriceCond = 3;
                else if (comboBoxnOrderPriceCondCB.Text == "4:FOK") pOrder.nOrderPriceCond = 4;

                if (comboBoxnBuySellCB.Text == "0:買") pOrder.nBuySell = 0;
                else if (comboBoxnBuySellCB.Text == "1:賣") pOrder.nBuySell = 1;

                if (comboBoxnOrderPriceTypeCB.Text == "1:市價") pOrder.nOrderPriceType = 1;
                else if (comboBoxnOrderPriceTypeCB.Text == "2:限價") pOrder.nOrderPriceType = 2;

                if (comboBoxnInnerOrderIsMITCB.Text == "0:否") pOrder.nInnerOrderIsMIT = 0;
                else if (comboBoxnInnerOrderIsMITCB.Text == "1:是") pOrder.nInnerOrderIsMIT = 1;

                if (comboBoxnTPDir.Text == "0:None") pOrder.nTPDir = 0;
                else if (comboBoxnTPDir.Text == "1:GTE大於等於") pOrder.nTPDir = 1;
                else if (comboBoxnTPDir.Text == "2:LTE小於等於") pOrder.nTPDir = 2;

                if (comboBoxnStopLossFlagCB.Text == "0:否") pOrder.nStopLossFlag = 0;
                else if (comboBoxnStopLossFlagCB.Text == "1:是") pOrder.nStopLossFlag = 1;

                if (comboBoxnSLDirCB.Text == "0:None") pOrder.nSLDir = 0;
                else if (comboBoxnSLDirCB.Text == "1:GTE大於等於") pOrder.nSLDir = 1;
                else if (comboBoxnSLDirCB.Text == "2:LTE小於等於") pOrder.nSLDir = 2;

                if (comboBoxnTickFlagCB.Text == "0:否") pOrder.nTickFlag = 0;
                else if (comboBoxnTickFlagCB.Text == "1:是") pOrder.nTickFlag = 1;

                if (comboBoxnTickDir.Text == "0:None") pOrder.nTickDir = 0;
                else if (comboBoxnTickDir.Text == "1:GTE大於等於") pOrder.nTickDir = 1;
                else if (comboBoxnTickDir.Text == "2:LTE小於等於") pOrder.nTickDir = 2;

                if (comboBoxnUpDownFlag.Text == "0:否") pOrder.nUpDownFlag = 0;
                else if (comboBoxnUpDownFlag.Text == "1:是") pOrder.nUpDownFlag = 1;

                if (comboBoxnUpDownDir.Text == "0:None") pOrder.nUpDownDir = 0;
                else if (comboBoxnUpDownDir.Text == "1:GTE大於等於") pOrder.nUpDownDir = 1;
                else if (comboBoxnUpDownDir.Text == "2:LTE小於等於") pOrder.nUpDownDir = 2;

                if (comboBoxnPreQtyFlagCB.Text == "0:否") pOrder.nPreQtyFlag = 0;
                else if (comboBoxnPreQtyFlagCB.Text == "1:是") pOrder.nPreQtyFlag = 1;

                if (comboBoxnPreQtyDirCB.Text == "0:None") pOrder.nPreQtyDir = 0;
                else if (comboBoxnPreQtyDirCB.Text == "1:GTE大於等於") pOrder.nPreQtyDir = 1;
                else if (comboBoxnPreQtyDirCB.Text == "2:LTE小於等於") pOrder.nPreQtyDir = 2;

                if (comboBoxnSumQtyFlagCB.Text == "0:否") pOrder.nSumQtyFlag = 0;
                else if (comboBoxnSumQtyFlagCB.Text == "1:是") pOrder.nSumQtyFlag = 1;

                if (comboBoxnSumQtyDir.Text == "0:None") pOrder.nSumQtyDir = 0;
                else if (comboBoxnSumQtyDir.Text == "1:GTE大於等於") pOrder.nSumQtyDir = 1;
                else if (comboBoxnSumQtyDir.Text == "2:LTE小於等於") pOrder.nSumQtyDir = 2;

                if (comboBoxnClearAllFlagCB.Text == "0:否") pOrder.nClearAllFlag = 0;
                else if (comboBoxnClearAllFlagCB.Text == "1:是") pOrder.nClearAllFlag = 1;

                if (comboBoxnFinalClearFlagCB.Text == "0:否") pOrder.nFinalClearFlag = 0;
                else if (comboBoxnFinalClearFlagCB.Text == "1:是") pOrder.nFinalClearFlag = 1;

                if (comboBoxnMITDirCB.Text == "0:None") pOrder.nMITDir = 0;
                else if (comboBoxnMITDirCB.Text == "1:GTE大於等於") pOrder.nMITDir = 1;
                else if (comboBoxnMITDirCB.Text == "2:LTE小於等於") pOrder.nMITDir = 2;

                if (comboBoxnTakeProfitFlagCB.Text == "0:否") pOrder.nTakeProfitFlag = 0;
                else if (comboBoxnTakeProfitFlagCB.Text == "1:是") pOrder.nTakeProfitFlag = 1;

                string bstrMessage;
                // 送出證券智慧單自組單委託。
                int nCode = m_pSKOrder.SendStockStrategyCB(comboBoxUserID.Text, bAsyncOrder, ref pOrder, out bstrMessage);
                // 取得回傳訊息
                string msg = "【SendStockStrategyCB】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void comboBoxnTradeKindV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxnTradeKindV1.Text == "11:當沖")
            {
                labelbstrParentSmartKey.Visible = true;
                textBoxbstrParentSmartKey.Visible = true;
                labelnDeletType.Visible = true;
                comboBoxnDeletType.Visible = true;
                labelbstrSmartKeyOut.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBoxbstrSmartKeyOut.Visible = true;
            }
            else
            {
                labelbstrParentSmartKey.Visible = false;
                textBoxbstrParentSmartKey.Visible = false;
                labelnDeletType.Visible = false;
                comboBoxnDeletType.Visible = false;
                labelbstrSmartKeyOut.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                textBoxbstrSmartKeyOut.Visible = false;
            }
        }

        private void comboBoxnTradeKindVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxnTradeKindVersion.Text == "舊版")
            {
                labelnTradeKind2.Visible = true;
                comboBoxnTradeKind.Visible = true;
                buttonCancelTSStrategyOrder.Visible = true;

                labelnTradeKind.Visible = false;
                comboBoxnTradeKindV1.Visible = false;
                buttonCancelTSStrategyOrderV1.Visible = false;
            }
            else
            {
                labelnTradeKind2.Visible = false;
                comboBoxnTradeKind.Visible = false;
                buttonCancelTSStrategyOrder.Visible = false;

                labelnTradeKind.Visible = true;
                comboBoxnTradeKindV1.Visible = true;
                buttonCancelTSStrategyOrderV1.Visible = true;
            }
        }

        private void buttonCancelStrategyList_Click(object sender, EventArgs e)
        {
            CANCELSTRATEGYORDER pCancelOrder = new CANCELSTRATEGYORDER(); // 智慧單刪單物件
            pCancelOrder.bstrLogInID = comboBoxUserID.Text;
            pCancelOrder.bstrFullAccount = comboBoxAccount.Text;
            //市場別 1：國內證、2：國內期、3：國外證、4：國外期
            if (comboBoxnMarket.Text == "1：國內證") pCancelOrder.nMarket = 1;
            else if (comboBoxnMarket.Text == "2：國內期") pCancelOrder.nMarket = 2;
            else if (comboBoxnMarket.Text == "3：國外證") pCancelOrder.nMarket = 3;
            else if (comboBoxnMarket.Text == "4：國外期") pCancelOrder.nMarket = 4;

            pCancelOrder.bstrSmartKey = textBoxbstrSmartKey.Text; // 智慧單號

            string bstrMessage;
            // 取消多筆智慧單委託。刪單欄位請參考智慧單被動回報回傳的內容。
            int nCode = m_pSKOrder.CancelStrategyList(comboBoxUserID.Text, pCancelOrder, out bstrMessage);
            // 取得回傳訊息
            string msg = "【CancelStrategyList】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
    }
}
