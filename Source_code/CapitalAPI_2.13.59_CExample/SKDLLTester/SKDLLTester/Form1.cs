using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SKDLLCSharp; // Import SKDLLCSharp.dll

namespace SKDLLTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
            comboBoxAuthorityFlag.SelectedIndex = 0;
            comboBoxStatus.SelectedIndex = 0;
            comboBoxTargetType.SelectedIndex = 0;

            m_dtForeigns = CreateStocksDataTableOS();
            m_dtForeigns2 = CreateStocksDataTableOS();
            m_dtStocks = CreateStocksDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();

            m_dtBest10Ask = CreateBest5AskTable();
            m_dtBest10Bid = CreateBest5AskTable();

            m_dtBest10Ask2 = CreateBest5AskTable();
            m_dtBest10Bid2 = CreateBest5AskTable();
        }

        private DataTable m_dtStocks;
        private DataTable m_dtBest5Ask;
        private DataTable m_dtBest5Bid;

        private DataTable m_dtForeigns;
        private DataTable m_dtBest10Ask;
        private DataTable m_dtBest10Bid;

        private DataTable m_dtForeigns2;
        private DataTable m_dtBest10Ask2;
        private DataTable m_dtBest10Bid2;

        private int kMarketPrice = 0;
        private int m_nSimulateStock;
        private DataTable CreateStocksDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sTypeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nFutureOI";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nYQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nUp";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nDown";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nCloseS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQtyS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nBidS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nAskS";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOddLotPer";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nDealTime";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_caStockNo"] };

            return myDataTable;
        }
        private DataTable CreateStocksDataTableOS()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nDenominator";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caExchangeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caExchangeName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_dSettlePrice";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int64");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_nStockidx"] };

            return myDataTable;
        }
        private void OnUpDateDataRow(SK.SKSTOCKLONG2 pStockLONG)
        {

            string strStockNo = pStockLONG.strStockNo;

            DataRow drFind = m_dtStocks.Rows.Find(strStockNo);
            if (drFind == null)
            {
                try
                {
                    DataRow myDataRow = m_dtStocks.NewRow();

                    myDataRow["m_nStockidx"] = pStockLONG.nStockidx;
                    myDataRow["m_sDecimal"] = pStockLONG.nDecimal;
                    myDataRow["m_sTypeNo"] = pStockLONG.nTypeNo;
                    myDataRow["m_cMarketNo"] = pStockLONG.nMarketNo;
                    myDataRow["m_caStockNo"] = pStockLONG.strStockNo;
                    myDataRow["m_caName"] = pStockLONG.strStockName;
                    myDataRow["m_nOpen"] = pStockLONG.nOpen / (Math.Pow(10, pStockLONG.nDecimal));
                    myDataRow["m_nHigh"] = pStockLONG.nHigh / (Math.Pow(10, pStockLONG.nDecimal));
                    myDataRow["m_nLow"] = pStockLONG.nLow / (Math.Pow(10, pStockLONG.nDecimal));
                    myDataRow["m_nClose"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.nDecimal));
                    myDataRow["m_nTickQty"] = pStockLONG.nTickQty;
                    myDataRow["m_nRef"] = pStockLONG.nRef / (Math.Pow(10, pStockLONG.nDecimal));

                    if (pStockLONG.nBid == kMarketPrice)
                        myDataRow["m_nBid"] = "市價";
                    else
                        myDataRow["m_nBid"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.nDecimal))).ToString();


                    myDataRow["m_nBc"] = pStockLONG.nBc;

                    if (pStockLONG.nAsk == kMarketPrice)
                        myDataRow["m_nAsk"] = "市價";
                    else
                        myDataRow["m_nAsk"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.nDecimal))).ToString();


                    m_nSimulateStock = pStockLONG.nSimulate;                 //成交價/買價/賣價;揭示
                    myDataRow["m_nAc"] = pStockLONG.nAc;
                    myDataRow["m_nTBc"] = pStockLONG.nTBc;
                    myDataRow["m_nTAc"] = pStockLONG.nTAc;
                    myDataRow["m_nFutureOI"] = pStockLONG.nFutureOI;
                    myDataRow["m_nTQty"] = pStockLONG.nTQty;
                    myDataRow["m_nYQty"] = pStockLONG.nYQty;
                    myDataRow["m_nUp"] = pStockLONG.nUp / (Math.Pow(10, pStockLONG.nDecimal));
                    myDataRow["m_nDown"] = pStockLONG.nDown / (Math.Pow(10, pStockLONG.nDecimal));
                    myDataRow["m_nDealTime"] = pStockLONG.nDealTime;//[-20240508-Add]
                    if (pStockLONG.nMarketNo == 5 || pStockLONG.nMarketNo == 6)
                    {
                        if (m_nSimulateStock == 1) //試算揭示//
                        {
                            myDataRow["m_nCloseS"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.nDecimal));//"試撮成交價";
                            myDataRow["m_nTickQtyS"] = pStockLONG.nTickQty;//"試撮單量";
                            myDataRow["m_nBidS"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.nDecimal))).ToString();//"試撮買價";
                            myDataRow["m_nAskS"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.nDecimal))).ToString();//"試撮賣價";
                        }
                    }
                    m_dtStocks.Rows.Add(myDataRow);

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            else
            {
                drFind["m_nStockidx"] = pStockLONG.nStockidx;
                drFind["m_sDecimal"] = pStockLONG.nDecimal;
                drFind["m_sTypeNo"] = pStockLONG.nTypeNo;
                drFind["m_cMarketNo"] = pStockLONG.nMarketNo;
                drFind["m_caStockNo"] = pStockLONG.strStockNo;
                drFind["m_caName"] = pStockLONG.strStockName;
                drFind["m_nOpen"] = pStockLONG.nOpen / (Math.Pow(10, pStockLONG.nDecimal));
                drFind["m_nHigh"] = pStockLONG.nHigh / (Math.Pow(10, pStockLONG.nDecimal));
                drFind["m_nLow"] = pStockLONG.nLow / (Math.Pow(10, pStockLONG.nDecimal));
                drFind["m_nClose"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.nDecimal));
                drFind["m_nTickQty"] = pStockLONG.nTickQty;
                drFind["m_nRef"] = pStockLONG.nRef / (Math.Pow(10, pStockLONG.nDecimal));

                if (pStockLONG.nBid == kMarketPrice)
                    drFind["m_nBid"] = "市價";
                else
                    drFind["m_nBid"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.nDecimal))).ToString();


                drFind["m_nBc"] = pStockLONG.nBc;

                if (pStockLONG.nAsk == kMarketPrice)
                    drFind["m_nAsk"] = "市價";
                else
                    drFind["m_nAsk"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.nDecimal))).ToString();


                drFind["m_nAc"] = pStockLONG.nAc;
                drFind["m_nTBc"] = pStockLONG.nTBc;
                drFind["m_nTAc"] = pStockLONG.nTAc;
                drFind["m_nFutureOI"] = pStockLONG.nFutureOI;
                drFind["m_nTQty"] = pStockLONG.nTQty;
                drFind["m_nYQty"] = pStockLONG.nYQty;
                drFind["m_nUp"] = pStockLONG.nUp / (Math.Pow(10, pStockLONG.nDecimal));
                drFind["m_nDown"] = pStockLONG.nDown / (Math.Pow(10, pStockLONG.nDecimal));
                m_nSimulateStock = pStockLONG.nSimulate;                 //成交價/買價/賣價;揭示
                drFind["m_nDealTime"] = pStockLONG.nDealTime;//[-20240508-Add]

                if (pStockLONG.nMarketNo == 5 || pStockLONG.nMarketNo == 6)
                {
                    if (m_nSimulateStock == 1) //試算揭示//
                    {
                        drFind["m_nCloseS"] = pStockLONG.nClose / (Math.Pow(10, pStockLONG.nDecimal));//"試撮成交價";
                        drFind["m_nTickQtyS"] = pStockLONG.nTickQty;//"試撮單量";
                        drFind["m_nBidS"] = (pStockLONG.nBid / (Math.Pow(10, pStockLONG.nDecimal))).ToString();//"試撮買價";
                        drFind["m_nAskS"] = (pStockLONG.nAsk / (Math.Pow(10, pStockLONG.nDecimal))).ToString();//"試撮賣價";
                    }
                }


            }
        }
        private void OnUpDateDataQuote(SK.SKFOREIGN_9LONG2 pForeign_9LONG)
        {
            int nStockIdx = pForeign_9LONG.nStockidx;
            DataRow drFind = m_dtForeigns.Rows.Find(nStockIdx);
            if (drFind == null)
            {
                DataRow myDataRow = m_dtForeigns.NewRow();

                myDataRow["m_nStockidx"] = pForeign_9LONG.nStockidx;
                myDataRow["m_sDecimal"] = pForeign_9LONG.nDecimal;
                myDataRow["m_nDenominator"] = pForeign_9LONG.nDenominator;
                myDataRow["m_cMarketNo"] = pForeign_9LONG.nMarketNo;
                myDataRow["m_caExchangeNo"] = pForeign_9LONG.strExchangeNo.Trim();
                myDataRow["m_caExchangeName"] = pForeign_9LONG.strExchangeName.Trim();
                myDataRow["m_caStockNo"] = pForeign_9LONG.strStockNo.Trim();
                myDataRow["m_caStockName"] = pForeign_9LONG.strStockName.Trim();

                myDataRow["m_nOpen"] = pForeign_9LONG.nOpen / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nHigh"] = pForeign_9LONG.nHigh / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nLow"] = pForeign_9LONG.nLow / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nClose"] = pForeign_9LONG.nClose / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_dSettlePrice"] = pForeign_9LONG.nSettlePrice / (Math.Pow(10, pForeign_9LONG.nDecimal));

                myDataRow["m_nTickQty"] = pForeign_9LONG.nTickQty;
                myDataRow["m_nRef"] = pForeign_9LONG.nRef / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nBid"] = pForeign_9LONG.nBid / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nBc"] = pForeign_9LONG.nBc;
                myDataRow["m_nAsk"] = pForeign_9LONG.nAsk / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nAc"] = pForeign_9LONG.nAc;
                myDataRow["m_nTQty"] = pForeign_9LONG.nTQty;

                m_dtForeigns.Rows.Add(myDataRow);
            }
            else
            {
                drFind["m_nStockidx"] = pForeign_9LONG.nStockidx;
                drFind["m_sDecimal"] = pForeign_9LONG.nDecimal;
                drFind["m_nDenominator"] = pForeign_9LONG.nDenominator;
                drFind["m_cMarketNo"] = pForeign_9LONG.nMarketNo;
                drFind["m_caExchangeNo"] = pForeign_9LONG.strExchangeNo.Trim();
                drFind["m_caExchangeName"] = pForeign_9LONG.strExchangeName.Trim();
                drFind["m_caStockNo"] = pForeign_9LONG.strStockNo.Trim();
                drFind["m_caStockName"] = pForeign_9LONG.strStockName.Trim();

                drFind["m_nOpen"] = pForeign_9LONG.nOpen / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nHigh"] = pForeign_9LONG.nHigh / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nLow"] = pForeign_9LONG.nLow / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nClose"] = pForeign_9LONG.nClose / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_dSettlePrice"] = pForeign_9LONG.nSettlePrice / (Math.Pow(10, pForeign_9LONG.nDecimal));

                drFind["m_nTickQty"] = pForeign_9LONG.nTickQty;
                drFind["m_nRef"] = pForeign_9LONG.nRef / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nBid"] = pForeign_9LONG.nBid / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nBc"] = pForeign_9LONG.nBc;
                drFind["m_nAsk"] = pForeign_9LONG.nAsk / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nAc"] = pForeign_9LONG.nAc;
                drFind["m_nTQty"] = pForeign_9LONG.nTQty;
            }
        }
        private void OnUpDateDataQuote2(SK.SKFOREIGN_9LONG2 pForeign_9LONG)
        {
            int nStockIdx = pForeign_9LONG.nStockidx;
            DataRow drFind = m_dtForeigns2.Rows.Find(nStockIdx);
            if (drFind == null)
            {
                DataRow myDataRow = m_dtForeigns2.NewRow();

                myDataRow["m_nStockidx"] = pForeign_9LONG.nStockidx;
                myDataRow["m_sDecimal"] = pForeign_9LONG.nDecimal;
                myDataRow["m_nDenominator"] = pForeign_9LONG.nDenominator;
                myDataRow["m_cMarketNo"] = pForeign_9LONG.nMarketNo;
                myDataRow["m_caExchangeNo"] = pForeign_9LONG.strExchangeNo.Trim();
                myDataRow["m_caExchangeName"] = pForeign_9LONG.strExchangeName.Trim();
                myDataRow["m_caStockNo"] = pForeign_9LONG.strStockNo.Trim();
                myDataRow["m_caStockName"] = pForeign_9LONG.strStockName.Trim();

                myDataRow["m_nOpen"] = pForeign_9LONG.nOpen / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nHigh"] = pForeign_9LONG.nHigh / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nLow"] = pForeign_9LONG.nLow / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nClose"] = pForeign_9LONG.nClose / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_dSettlePrice"] = pForeign_9LONG.nSettlePrice / (Math.Pow(10, pForeign_9LONG.nDecimal));

                myDataRow["m_nTickQty"] = pForeign_9LONG.nTickQty;
                myDataRow["m_nRef"] = pForeign_9LONG.nRef / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nBid"] = pForeign_9LONG.nBid / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nBc"] = pForeign_9LONG.nBc;
                myDataRow["m_nAsk"] = pForeign_9LONG.nAsk / (Math.Pow(10, pForeign_9LONG.nDecimal));
                myDataRow["m_nAc"] = pForeign_9LONG.nAc;
                myDataRow["m_nTQty"] = pForeign_9LONG.nTQty;

                m_dtForeigns2.Rows.Add(myDataRow);
            }
            else
            {
                drFind["m_nStockidx"] = pForeign_9LONG.nStockidx;
                drFind["m_sDecimal"] = pForeign_9LONG.nDecimal;
                drFind["m_nDenominator"] = pForeign_9LONG.nDenominator;
                drFind["m_cMarketNo"] = pForeign_9LONG.nMarketNo;
                drFind["m_caExchangeNo"] = pForeign_9LONG.strExchangeNo.Trim();
                drFind["m_caExchangeName"] = pForeign_9LONG.strExchangeName.Trim();
                drFind["m_caStockNo"] = pForeign_9LONG.strStockNo.Trim();
                drFind["m_caStockName"] = pForeign_9LONG.strStockName.Trim();

                drFind["m_nOpen"] = pForeign_9LONG.nOpen / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nHigh"] = pForeign_9LONG.nHigh / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nLow"] = pForeign_9LONG.nLow / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nClose"] = pForeign_9LONG.nClose / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_dSettlePrice"] = pForeign_9LONG.nSettlePrice / (Math.Pow(10, pForeign_9LONG.nDecimal));

                drFind["m_nTickQty"] = pForeign_9LONG.nTickQty;
                drFind["m_nRef"] = pForeign_9LONG.nRef / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nBid"] = pForeign_9LONG.nBid / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nBc"] = pForeign_9LONG.nBc;
                drFind["m_nAsk"] = pForeign_9LONG.nAsk / (Math.Pow(10, pForeign_9LONG.nDecimal));
                drFind["m_nAc"] = pForeign_9LONG.nAc;
                drFind["m_nTQty"] = pForeign_9LONG.nTQty;
            }
        }
        private DataTable CreateBest5AskTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAskQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;

        }
        private void UpdateBest5Grid(int marketNo, int[] bids, int[] bidQtys, int[] asks, int[] askQtys, int extendBid, int extendBidQty, int nExtendAsk, int nExtendAskQty, int simulate)
        {
            //0:一般;1:試算揭示
            if (simulate == 0)
            {
                GridBest5Ask.ForeColor = Color.Black;
                GridBest5Bid.ForeColor = Color.Black;
            }
            else
            {
                GridBest5Ask.ForeColor = Color.Gray;
                GridBest5Bid.ForeColor = Color.Gray;
            }

            double dDigitNum = 0.000;

            dDigitNum = 100.00;//default value

            if (m_dtBest5Ask.Rows.Count == 0 && m_dtBest5Bid.Rows.Count == 0)
            {
                DataRow myDataRow;

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = askQtys[0];
                if (asks[0] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (asks[0] / dDigitNum).ToString();///100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = askQtys[1];

                if (asks[1] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (asks[1] / dDigitNum).ToString();//100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = askQtys[2];

                if (asks[2] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (asks[2] / dDigitNum).ToString();//100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = askQtys[3];

                if (asks[3] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (asks[3] / dDigitNum).ToString();// 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = askQtys[4];

                if (asks[4] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (asks[4] / dDigitNum).ToString();// 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);



                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = bidQtys[0];

                if (bids[0] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else myDataRow["m_nAsk"] = (bids[0] / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = bidQtys[1];
                if (bids[1] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else myDataRow["m_nAsk"] = (bids[1] / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = bidQtys[2];
                if (bids[2] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (bids[2] / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = bidQtys[3];
                if (bids[3] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (bids[3] / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = bidQtys[4];
                if (bids[4] == kMarketPrice)
                    myDataRow["m_nAsk"] = "M";
                else
                    myDataRow["m_nAsk"] = (bids[4] / dDigitNum).ToString();
                m_dtBest5Bid.Rows.Add(myDataRow);

            }
            else
            {
                m_dtBest5Ask.Rows[0]["m_nAskQty"] = askQtys[0];
                if (asks[0] == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[0]["m_nAsk"] = (asks[0] / dDigitNum).ToString();

                m_dtBest5Ask.Rows[1]["m_nAskQty"] = askQtys[1];
                if (asks[1] == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[1]["m_nAsk"] = (asks[1] / dDigitNum).ToString();

                m_dtBest5Ask.Rows[2]["m_nAskQty"] = askQtys[2];
                if (asks[2] == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[2]["m_nAsk"] = (asks[2] / dDigitNum).ToString();

                m_dtBest5Ask.Rows[3]["m_nAskQty"] = askQtys[3];
                if (asks[3] == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[3]["m_nAsk"] = (asks[3] / dDigitNum).ToString();

                m_dtBest5Ask.Rows[4]["m_nAskQty"] = askQtys[4];
                if (asks[4] == kMarketPrice) m_dtBest5Ask.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Ask.Rows[4]["m_nAsk"] = (asks[4] / dDigitNum).ToString();


                m_dtBest5Bid.Rows[0]["m_nAskQty"] = bidQtys[0];
                if (bids[0] == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[0]["m_nAsk"] = (bids[0] / dDigitNum).ToString();

                m_dtBest5Bid.Rows[1]["m_nAskQty"] = bidQtys[1];
                if (bids[1] == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[1]["m_nAsk"] = (bids[1] / dDigitNum).ToString();

                m_dtBest5Bid.Rows[2]["m_nAskQty"] = bidQtys[2];
                if (bids[2] == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[2]["m_nAsk"] = (bids[2] / dDigitNum).ToString();

                m_dtBest5Bid.Rows[3]["m_nAskQty"] = bidQtys[3];
                if (bids[3] == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[3]["m_nAsk"] = (bids[3] / dDigitNum).ToString();

                m_dtBest5Bid.Rows[4]["m_nAskQty"] = bidQtys[4];
                if (bids[4] == kMarketPrice) m_dtBest5Bid.Rows[0]["m_nAsk"] = "M";
                else m_dtBest5Bid.Rows[4]["m_nAsk"] = (bids[4] / dDigitNum).ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // listOnReplyMessage is a ListBox used to display incoming reply messages
            SK.OnReplyMessage += (strLoginID, strMessage) => // 註冊 OnReplyMessage 事件，當有新公告訊息時觸發
            {// 根據收到的訊息更新 UI 或處理資料
                if (listOnReplyMessage.InvokeRequired)
                {
                    listOnReplyMessage.Invoke(new Action(() =>
                    {
                        listOnReplyMessage.Items.Add("[OnReplyMessage]回傳值:" + strLoginID + "_" + strMessage); // 例如顯示訊息內容
                    }));
                }
                else
                {
                    listOnReplyMessage.Items.Add("[OnReplyMessage]回傳值:" + strLoginID + "_" + strMessage);
                }
            };
            // 當連線狀態改變時觸發，顯示使用者登入狀態或錯誤訊息到 ListBox
            SK.OnConnection += (loginID, code) =>
            {
                // 檢查是否在 UI 執行緒中，避免跨執行緒操作 UI
                if (listOnReplyMessage.InvokeRequired)
                {
                    listOnReplyMessage.Invoke(new Action(() =>
                    {
                        // 在 UI 執行緒中顯示連線狀態訊息
                        listOnReplyMessage.Items.Add($"[OnConnection]使用者 {loginID} 狀態碼: {SK.GetMessage(code)}");
                    }));
                }
                else
                {
                    // 已在 UI 執行緒，直接顯示訊息
                    listOnReplyMessage.Items.Add($"[OnConnection]使用者 {loginID} 狀態碼: {SK.GetMessage(code)}");
                }
            };
            // 當委託單（Proxy Order）有狀態變更時觸發，顯示 StampID、狀態碼與訊息
            SK.OnProxyOrder += (StampID, Code, Message) =>
            {// 根據收到的訊息更新 UI 或處理資料
                if (listOnReplyMessage.InvokeRequired)
                {
                    listOnReplyMessage.Invoke(new Action(() =>
                    {
                        listOnReplyMessage.Items.Add($"[OnProxyOrder]回傳值: StampID[{StampID}] 狀態碼: {Code} 訊息: {Message}");  // 例如顯示訊息內容
                    }));
                }
                else
                {
                    listOnReplyMessage.Items.Add($"[OnProxyOrder]回傳值: StampID[{StampID}] 狀態碼: {Code} 訊息: {Message}");
                }
            };
            // listOnNewOrderData 與 listOnNewFulfillData 是用來顯示補回通知的 ListBox 控件
            SK.OnComplete += loginID =>
            {
                if (listOnNewOrderData.InvokeRequired)
                {
                    listOnNewOrderData.Invoke(new Action(() =>
                    {
                        listOnNewOrderData.Items.Add($"[OnComplete]委託回補完成通知：{loginID}");  // 例如顯示訊息內容
                    }));
                }
                else
                {
                    listOnNewOrderData.Items.Add($"[OnComplete]委託回補完成通知：{loginID}");
                }

                if (listOnNewFulfillData.InvokeRequired)
                {
                    listOnNewFulfillData.Invoke(new Action(() =>
                    {
                        listOnNewFulfillData.Items.Add($"[OnComplete]成交回補完成通知：{loginID}");  // 例如顯示訊息內容
                    }));
                }
                else
                {
                    listOnNewFulfillData.Items.Add($"[OnComplete]成交回補完成通知：{loginID}");
                }
            };
            // listOnNewOrderData 是用來顯示新委託資料的 ListBox 控件
            SK.OnNewOrderData += (loginID, data) =>
            {
                if (listOnNewOrderData.InvokeRequired)
                {
                    listOnNewOrderData.BeginInvoke(new Action(() =>
                    {
                        listOnNewOrderData.Items.Add($"[{loginID}]{data.Raw}");  // 顯示原始字串資料
                        listOnNewOrderData.Items.Add($"{data.OrderNo}");         // 顯示解析後的委託書號
                    }));
                }
                else
                {
                    listOnNewOrderData.Items.Add($"[{loginID}]{data.Raw}");      // 顯示原始字串資料
                    listOnNewOrderData.Items.Add($"{data.OrderNo}");             // 顯示解析後的委託書號
                }
            };
            // listOnNewFulfillData 是用來顯示新成交資料的 ListBox 控件
            SK.OnNewFulfillData += (loginID, data) =>
            {
                if (listOnNewFulfillData.InvokeRequired)
                {
                    listOnNewFulfillData.BeginInvoke(new Action(() =>
                    {
                        listOnNewFulfillData.Items.Add($"[{loginID}]{data.Raw}");  // 顯示原始字串資料
                        listOnNewFulfillData.Items.Add($"{data.OrderNo}");         // 顯示解析後的委託書號
                    }));
                }
                else
                {
                    listOnNewFulfillData.Items.Add($"[{loginID}]{data.Raw}");      // 顯示原始字串資料
                    listOnNewFulfillData.Items.Add($"{data.OrderNo}");             // 顯示解析後的委託書號
                }
            };
            SK.OnNotifyQuoteLONG += (nMarketNo, strStockNo) =>
            {
                // 建立報價物件
                SK.SKSTOCKLONG2 pSKStockLONG = new SK.SKSTOCKLONG2();
                // 收回報價物件
                pSKStockLONG = SK.SKQuoteLib_GetStockByStockNo(nMarketNo, strStockNo);
                // 如果報價物件回傳正常，則將數值呈現至 UI
                if (pSKStockLONG.nCode == 0)
                    OnUpDateDataRow(pSKStockLONG);
            };
            // UpdateBest5Grid 用於將最新五檔報價顯示至 UI，例如 DataGridView
            SK.OnNotifyBest5LONG += (marketNo, strStockNo, bids, bidQtys, asks, askQtys, extendBid, extendBidQty, nExtendAsk, nExtendAskQty, simulate) =>
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateBest5Grid(marketNo, bids, bidQtys, asks, askQtys, extendBid, extendBidQty, nExtendAsk, nExtendAskQty, simulate);
                    }));
                }
                else
                {
                    UpdateBest5Grid(marketNo, bids, bidQtys, asks, askQtys, extendBid, extendBidQty, nExtendAsk, nExtendAskQty, simulate);
                }
            };
            // listTicks 是用來顯示即時逐筆成交資料的 ListBox 控件
            SK.OnNotifyTicksLONG += (marketNo, strStockNo, ptr, date, timeHMS, timeMicro, bid, ask, close, qty, simulate) =>
            {
                string strData = "";
                int nMarketPrice = kMarketPrice;

                if (chkbox_msms.Checked == true)
                    strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + bid.ToString() + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString();
                else
                    strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + " " + timeMicro.ToString() + "," + bid.ToString() + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString();
                if (Box_M.Checked == true)
                {
                    if (bid == kMarketPrice)
                        strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + "M" + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString();
                    else if (ask == kMarketPrice)
                        strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + bid.ToString() + "," + "M" + "," + close.ToString() + "," + qty.ToString();
                    else
                        strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + bid.ToString() + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString();
                }
                //[揭示]//0:一般;1:試算揭示

                if (strData != "" && ((chkBoxSimulate.Checked) || (!chkBoxSimulate.Checked && simulate == 0)))
                {
                    if (listTicks.InvokeRequired)
                    {
                        listTicks.Invoke(new Action(() =>
                        {
                            listTicks.Items.Add("[OnNotifyTicksLONG]" + strData);
                        }));
                    }
                    else
                    {
                        listTicks.Items.Add("[OnNotifyTicksLONG]" + strData);
                    }
                }
            };
        }
        private void buttonSKCenterLib_Login_Click(object sender, EventArgs e)
        {
            // 取得使用者輸入的登入帳號與密碼
            string strLoginID = textBoxUserID.Text;
            string strPassword = textBoxPassword.Text;

            // 從權限選單中取得選擇的登入旗標（0=正式、1=測試...）
            int nFlag = comboBoxAuthorityFlag.SelectedIndex;

            // 呼叫群益 API 執行登入，回傳結果包含帳號資訊與狀態碼
            var result = SK.Login(strLoginID, strPassword, nFlag);

            // 檢查登入是否成功（Code 為 0 表示成功）
            if (result.Code == 0)
            {
                // ===== 登入成功後，將各帳號類型加入對應的 ComboBox 供使用者選擇 =====

                // TS 帳號：證券帳號，顯示登入ID與完整帳號
                foreach (var account in result.TSAccounts)
                    comboBoxTS.Items.Add($"{account.LoginID} {account.FullAccount}");

                // OS 帳號：複委託帳號，顯示登入ID與完整帳號
                foreach (var account in result.OSAccounts)
                    comboBoxOS.Items.Add($"{account.LoginID} {account.FullAccount}");

                // TF 帳號：內期帳號，顯示登入ID與完整帳號
                foreach (var account in result.TFAccounts)
                    comboBoxTF.Items.Add($"{account.LoginID} {account.FullAccount}");

                // OF 帳號：海期帳號，顯示登入ID與完整帳號
                foreach (var account in result.OFAccounts)
                    comboBoxOF.Items.Add($"{account.LoginID} {account.FullAccount}");

                // 自動選取每個 ComboBox 的第一個帳號作為預設值
                if (comboBoxTS.Items.Count > 0) comboBoxTS.SelectedIndex = 0;
                if (comboBoxOS.Items.Count > 0) comboBoxOS.SelectedIndex = 0;
                if (comboBoxTF.Items.Count > 0) comboBoxTF.SelectedIndex = 0;
                if (comboBoxOF.Items.Count > 0) comboBoxOF.SelectedIndex = 0;
            }

            // 登入後在 ListBox 顯示登入結果訊息（由 API 回傳的文字）
            listOnReplyMessage.Items.Add("[Login]: " + SK.GetMessage(result.Code));
        }
        private void btnSendStockProxyOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTS.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strStockNo;
            string strPrice;
            string OrderType = "";
            int Qty;
            int nORDERType, nPriceType, nTimeInForce, nMarketType, nPriceMark;

            if (txtProxyStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyStockNo.Text.Trim();


            if (ProxyOrderTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox.SelectedIndex + 1;

            if (ProxyPriceTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            nPriceType = ProxyPriceTypeBox.SelectedIndex + 1;

            if (ProxyTimeBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託時效");
                return;
            }
            nTimeInForce = ProxyTimeBox.SelectedIndex;

            if (ProxyMarketBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nMarketType = ProxyMarketBox.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyStockPrice.Text.Trim();

            if (int.TryParse(txtStockQty.Text.Trim(), out Qty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (ProxyPriceMarkBox.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格旗標");
                return;
            }
            nPriceMark = ProxyPriceMarkBox.SelectedIndex;

            switch (nORDERType)
            {
                case 1: OrderType = "1"; break;
                case 2: OrderType = "2"; break;
                case 3: OrderType = "3"; break;
                case 4: OrderType = "4"; break;
                case 5: OrderType = "5"; break;
                case 6: OrderType = "6"; break;
                case 7: OrderType = "7"; break;
                default: OrderType = ""; break;
            }

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendStockProxyOrder(loginID, strStockNo, account, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty);
            //var (nCode2, message2) = SK.SendStockProxyOrder(loginID, strStockNo, account, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty, nPriceMark);

            listOnReplyMessage.Items.Add("[SendStockProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnSendStockProxyAlter_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTS.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strStockNo;
            string strPrice, strOrderNo, strSeqNo, OrderType = "";
            int Qty;
            int nPriceType, nTimeInForce, nMarketType, nPriceMark, nORDERType;

            if (txtProxyStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyStockNo2.Text.Trim();

            if (ProxyOrderTypeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇下單類別");
                return;
            }
            nORDERType = ProxyOrderTypeBox2.SelectedIndex;


            if (ProxyPriceTypeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格類別");
                return;
            }
            nPriceType = ProxyPriceTypeBox2.SelectedIndex + 1;

            if (ProxyTimeBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託時效");
                return;
            }
            nTimeInForce = ProxyTimeBox2.SelectedIndex;

            if (ProxyMarketBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nMarketType = ProxyMarketBox2.SelectedIndex;


            double dPrice = 0.0;
            if (double.TryParse(txtProxyStockPrice2.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyStockPrice2.Text.Trim();

            if (int.TryParse(txtStockQty2.Text.Trim(), out Qty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (ProxyPriceMarkBox2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇價格旗標");
                return;
            }
            nPriceMark = ProxyPriceMarkBox2.SelectedIndex;

            if (txtStockBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtStockBookNo.Text.Trim();

            if (txtStockSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtStockSeqNo.Text.Trim();

            switch (nORDERType)
            {
                case 0: OrderType = "0"; break;
                case 1: OrderType = "1"; break;
                case 2: OrderType = "2"; break;
                default: OrderType = ""; break;
            }

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendStockProxyAlter(loginID, strStockNo, account, strSeqNo, strOrderNo, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty);
            //var (nCode2, message2) = SK.SendStockProxyAlter(loginID, strStockNo, account, strSeqNo, strOrderNo, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty, nPriceMark);

            listOnReplyMessage.Items.Add("[SendStockProxyAlter]回傳值:" + nCode + " " + message);
        }
        private void buttonManageServerConnection_Click(object sender, EventArgs e)
        {
            // 取得使用者選擇的連線狀態（0=連線，1=斷線...）
            int nStatus = comboBoxStatus.SelectedIndex;

            // 取得目標伺服器類型（例如回報、行情...）
            int nTargetType = comboBoxTargetType.SelectedIndex;
            if (comboBoxTargetType.SelectedIndex == 2)
                nTargetType = 4;

            // 呼叫群益 API 控制伺服器連線狀態
            int result = SK.ManageServerConnection(textBoxUserID.Text, nStatus, nTargetType);

            // 顯示執行結果（由 API 回傳的狀態訊息）
            listOnReplyMessage.Items.Add("[ManageServerConnection]回傳值:" + SK.GetMessage(result));
        }
        private void btnSendProxyFutureOrderCLR_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strFutureNo;
            string strYM;
            int nBS;
            int nCOND;
            int nDayTrade;
            string strPrice;
            int nQty;
            int nPriceFlag;
            int nORDERType;
            int nPreOrder;
            string strOrderType;

            if (txtFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtFutureNo.Text.Trim();

            if (txtProxyYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM.Text.Trim();

            if (boxProxyBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS.SelectedIndex;

            if (boxProxyCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND.SelectedIndex;

            if (boxDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxDayTrade.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtFuturePrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtFuturePrice.Text.Trim();

            if (int.TryParse(txtFutureQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxProxyPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxProxyPriceFlag.SelectedIndex;

            if (boxProxyORDERType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxProxyORDERType.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            if (boxProxyPreOrder.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder.SelectedIndex;

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendFutureProxyOrder(loginID, account, strFutureNo, strYM, nBS, nPriceFlag, nDayTrade, strOrderType, nPreOrder, nQty, strPrice, nCOND);
            listOnReplyMessage.Items.Add("[SendFutureProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnProxyFutureAlter_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nCOND;
            string strPrice = "";
            int nQty = 0;
            int nPreOrder;
            string strOrderNo;
            string strSeqNo, strOrderType = "";
            int nORDERType;

            if (boxProxyCOND2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND2.SelectedIndex;


            if (boxProxyPreOrder2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder2.SelectedIndex;

            if (boxProxyORDERType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = boxProxyORDERType2.SelectedIndex;
            if (nORDERType == 2)
            {
                double dPrice = 0.0;
                if (double.TryParse(txtProxyPrice2.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
                strPrice = txtProxyPrice2.Text.Trim();
            }

            if (nORDERType == 0 || nORDERType == 1)
            {
                if (int.TryParse(txtProxyQty2.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }

            if (txtProxyOrderNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyOrderNo.Text.Trim();

            if (txtProxySeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxySeqNo.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendFutureProxyAlter(loginID, account, strOrderType, strPrice, nPreOrder, nQty, nCOND, strOrderNo, strSeqNo);
            listOnReplyMessage.Items.Add("[SendFutureProxyAlter]回傳值:" + nCode + " " + message);
        }
        private void btnSendProxyOptionOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strFutureNo, strYM, strStrike, strOrderType = "";
            int nBS;
            string strPrice;
            int nQty, nPriceFlag;
            int nCP, nORDERType, nCOND, nDayTrade, nPreOrder;

            if (txtOptionNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOptionNo.Text.Trim();

            if (txtOptionYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtOptionYM.Text.Trim();

            if (txtProxyStrike.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrike = txtProxyStrike.Text.Trim();

            if (boxProxyCP.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCP = boxProxyCP.SelectedIndex;

            if (boxOptionBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxOptionBS.SelectedIndex;

            if (boxOptionCOND.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxOptionCOND.SelectedIndex;

            if (boxORDERType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxORDERType.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtOptionPrice.Text.Trim(), out dPrice) == false && txtOptionPrice.Text.Trim() != "M" && txtOptionPrice.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtOptionPrice.Text.Trim();

            if (int.TryParse(txtOptionQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOptionDayTrade.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否當沖");
                return;
            }
            nDayTrade = boxOptionDayTrade.SelectedIndex;

            if (boxPreOrder.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxPreOrder.SelectedIndex;

            if (boxPriceFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxPriceFlag.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendOptionProxyOrder(loginID, account, strFutureNo, strPrice, strYM, strStrike, strOrderType, nPreOrder, nQty, nCP, nBS, nPriceFlag, nCOND, nDayTrade);
            listOnReplyMessage.Items.Add("[SendOptionProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnSendProxyDuplexOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strFutureNo, strFutureNo2, strYM, strYM2, strStrike, strStrike2, strOrderType = "";
            int nBS, nBS2;
            string strPrice;
            int nQty, nPriceFlag;
            int nCP, nCP2, nORDERType, nCOND, nDayTrade, nPreOrder;

            if (txtOptionNo1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtOptionNo1.Text.Trim();

            if (txtOptionNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼2");
                return;
            }
            strFutureNo2 = txtOptionNo2.Text.Trim();

            if (txtProxyYM1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月");
                return;
            }
            strYM = txtProxyYM1.Text.Trim();

            if (txtProxyYM2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品年月2");
                return;
            }
            strYM2 = txtProxyYM2.Text.Trim();

            if (txtProxyStrike1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價");
                return;
            }
            strStrike = txtProxyStrike1.Text.Trim();

            if (txtProxyStrike2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入履約價2");
                return;
            }
            strStrike2 = txtProxyStrike2.Text.Trim();

            if (boxProxyCP1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權");
                return;
            }
            nCP = boxProxyCP1.SelectedIndex;

            if (boxProxyCP2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣權2");
                return;
            }
            nCP2 = boxProxyCP2.SelectedIndex;

            if (boxProxyBS1.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBS = boxProxyBS1.SelectedIndex;

            if (boxProxyBS2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別2");
                return;
            }
            nBS2 = boxProxyBS2.SelectedIndex;

            if (boxOptionCOND2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxOptionCOND2.SelectedIndex;

            if (boxORDERType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nORDERType = boxORDERType2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtOptionPrice2.Text.Trim(), out dPrice) == false && txtOptionPrice2.Text.Trim() != "M" && txtOptionPrice2.Text.Trim() != "P")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtOptionPrice2.Text.Trim();

            if (int.TryParse(txtOptionQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxDayTrade2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇是否當沖");
                return;
            }
            nDayTrade = boxDayTrade2.SelectedIndex;

            if (boxPreOrder2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxPreOrder2.SelectedIndex;

            if (boxPriceFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委價類別");
                return;
            }
            nPriceFlag = boxPriceFlag2.SelectedIndex;

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendDuplexProxyOrder(loginID, account, strFutureNo, strPrice, strYM, strStrike, strYM2, strStrike2, strOrderType, nPreOrder, nQty, nCP, nBS, strFutureNo2, nCP2, nBS2, nPriceFlag, nCOND, nDayTrade);
            listOnReplyMessage.Items.Add("[SendDuplexProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnProxyOptionAlter_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nCOND;
            string strPrice = "";
            int nQty = 0;
            int nPreOrder;
            string strOrderNo, strOrderType = "";
            string strSeqNo;
            int nORDERType;

            if (boxProxyCOND3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nCOND = boxProxyCOND3.SelectedIndex;

            if (boxProxyPreOrder3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }
            nPreOrder = boxProxyPreOrder3.SelectedIndex;

            if (boxProxyORDERType3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類別");
                return;
            }
            nORDERType = boxProxyORDERType3.SelectedIndex;

            if (nORDERType == 2)
            {
                double dPrice = 0.0;
                if (double.TryParse(txtProxyPrice3.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
                strPrice = txtProxyPrice3.Text.Trim();
            }

            if (nORDERType == 0 || nORDERType == 1)
            {
                if (int.TryParse(txtProxyQty3.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }
            if (txtProxyOrderNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyOrderNo3.Text.Trim();

            if (txtProxySeqNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxySeqNo3.Text.Trim();

            switch (nORDERType)
            {
                case 0: strOrderType = "0"; break;
                case 1: strOrderType = "1"; break;
                case 2: strOrderType = "2"; break;
                default: strOrderType = ""; break;
            }
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendOptionProxyAlter(loginID, account, strOrderType, strPrice, nPreOrder, nQty, nCOND, strOrderNo, strSeqNo);
            listOnReplyMessage.Items.Add("[SendOptionProxyAlter]回傳值:" + nCode + " " + message);
        }
        private void btnSendOverseaFutureProxyOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtProxyTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo.Text.Trim();

            if (txtOFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo.Text.Trim();

            if (txtProxyYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth.Text.Trim();

            double dPrice = 0.0;

            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrder.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
            }
            strOrder = txtProxyOrder.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrderNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價分子請輸入數字");
                    return;
                }
            }
            strOrderNumerator = txtProxyOrderNumerator.Text.Trim();

            //委託價分母
            string strOrderDeno = "";
            strOrderDeno = txtProxyOrderDeno.Text.Trim();

            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTrigger.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字");
                    return;
                }
            }
            strTrigger = txtProxyTrigger.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTriggerNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價分子請輸入數字");
                    return;
                }
            }
            strTriggerNumerator = txtProxyTriggerNumerator.Text.Trim();


            if (int.TryParse(txtOFQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOFBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxOFBS.SelectedIndex;

            if (boxProxyNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose.SelectedIndex;

            if (boxProxyFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxProxyFlag.SelectedIndex;

            if (boxProxyPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod.SelectedIndex;

            if (boxProxySpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType.SelectedIndex;

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendOverseaFutureProxyOrder(loginID, account, strTradeName, strStockNo, strYearMonth, strOrder, strOrderNumerator, strTrigger, strTriggerNumerator, nBuySell, nNewClose, nDayTrade, nTradeType, nSpecialTradeType, nQty);
            listOnReplyMessage.Items.Add("[SendOverseaFutureProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnSendOverseaFutureSpreadProxyOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth, strYearMonth2;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtProxyTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo.Text.Trim();

            if (txtOFutureNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo.Text.Trim();

            if (txtProxyYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth.Text.Trim();

            double dPrice = 0.0;

            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrder.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
            }
            strOrder = txtProxyOrder.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 0 || boxProxySpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtProxyOrderNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價分子請輸入數字");
                    return;
                }
            }
            strOrderNumerator = txtProxyOrderNumerator.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTrigger.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字");
                    return;
                }
            }
            strTrigger = txtProxyTrigger.Text.Trim();


            if (boxProxySpecialTradeType.SelectedIndex == 2 || boxProxySpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtProxyTriggerNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價分子請輸入數字");
                    return;
                }
            }
            strTriggerNumerator = txtProxyTriggerNumerator.Text.Trim();


            if (int.TryParse(txtOFQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOFBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxOFBS.SelectedIndex;

            if (boxProxyNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose.SelectedIndex;

            if (boxProxyFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxProxyFlag.SelectedIndex;

            if (boxProxyPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod.SelectedIndex;

            if (boxProxySpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType.SelectedIndex;

            if (txtProxyYearMonth2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月(遠月)");
                return;
            }
            strYearMonth2 = txtProxyYearMonth2.Text.Trim();

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendOverseaFutureSpreadProxyOrder(loginID, account, strTradeName, strStockNo, strYearMonth, strYearMonth2, strOrder, strOrderNumerator, strTrigger, strTriggerNumerator, nBuySell, nNewClose, nDayTrade, nTradeType, nSpecialTradeType, nQty);
            listOnReplyMessage.Items.Add("[SendOverseaFutureSpreadProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnSendOverseaFutureProxyAlter_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strTradeName;
            string strStockNo, strOrderNo;
            string strYearMonth, strYearMonth2;
            int nNewClose;
            int nTradeType;
            int nSpecialTradeType, nFunCode;
            string strOrder, strSeqNo;
            string strOrderNumerator, strOrderD;

            int nQty;

            if (txtProxyTradeNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtProxyTradeNo2.Text.Trim();

            if (txtOFutureNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOFutureNo2.Text.Trim();

            if (txtProxyYearMonth3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtProxyYearMonth3.Text.Trim();
            strYearMonth2 = txtProxyYearMonth4.Text.Trim();

            double dPrice = 0.0;

            strOrder = txtProxyOrder2.Text.Trim();

            strOrderNumerator = txtProxyOrderNumerator2.Text.Trim();

            strOrderD = txtProxyPriceD.Text.Trim();

            int.TryParse(txtOFQty2.Text.Trim(), out nQty);

            if (boxProxyNewClose2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxProxyNewClose2.SelectedIndex;


            if (boxProxyPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxProxyPeriod2.SelectedIndex;

            if (boxProxySpecialTradeType2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxProxySpecialTradeType2.SelectedIndex;

            if (boxFunCode.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇異動功能(刪單/減量/改價)");
                return;
            }
            nFunCode = boxFunCode.SelectedIndex;



            if (txtOFBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtOFBookNo.Text.Trim();

            if (txtSeqNo4.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtSeqNo4.Text.Trim();

            int nSpread;
            nSpread = BoxAlterSpread.SelectedIndex;

            if (nSpread == 2)
            {
                if (txt_Strike_proxy.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入履約價");
                    return;
                }
            }
            string strStrikePrice = txt_Strike_proxy.Text.Trim();

            if (nSpread == 2)
            {
                if (boxCallPutAlter.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇買賣權");
                    return;
                }
            }
            int nCallPut = boxCallPutAlter.SelectedIndex;
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendOverseaFutureProxyAlter(loginID, account, strTradeName, strStockNo, strYearMonth, strYearMonth2, strOrder, strOrderNumerator, strOrderD, nNewClose, nTradeType, nSpecialTradeType, nQty, strOrderNo, strSeqNo, nSpread, nFunCode, strStrikePrice, nCallPut);
            listOnReplyMessage.Items.Add("[SendOverseaFutureProxyAlter]回傳值:" + nCode + " " + message);
        }
        private void btnSendOverseaOptionProxyOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            int nCallPut;
            string strStrikePrice;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtOOTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtOOTradeNo.Text.Trim();

            if (txtOONo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtOONo.Text.Trim();

            if (txtOOYM.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOOYM.Text.Trim();

            double dPrice = 0.0;
            if (double.TryParse(txtOOPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrder = txtOOPrice.Text.Trim();

            if (double.TryParse(txtOONumerator.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價分子請輸入數字");
                return;
            }
            strOrderNumerator = txtOONumerator.Text.Trim();
            string strOrderDeno = txtOODeno.Text.Trim();
            strTrigger = txtOOTrigger.Text.Trim();

            strTriggerNumerator = txtOOTriggerNumerator.Text.Trim();

            strStrikePrice = txtProxyStrikePrice.Text.Trim();

            if (int.TryParse(txtOOQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxOOBS.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxOOBS.SelectedIndex;

            if (boxOONewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxOONewClose.SelectedIndex;

            if (boxOOFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxOOFlag.SelectedIndex;

            if (boxOOPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxOOPeriod.SelectedIndex;

            if (boxOOSpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxOOSpecialTradeType.SelectedIndex;

            if (boxProxyCallPut.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇CALL PUT");
                return;
            }
            nCallPut = boxProxyCallPut.SelectedIndex;

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendOverseaOptionProxyOrder(loginID, account, strTradeName, strStockNo, strYearMonth, strOrder, strOrderNumerator, strOrderDeno, strTrigger, strTriggerNumerator, nBuySell, nNewClose, nDayTrade, nTradeType, nSpecialTradeType, strStrikePrice, nCallPut, nQty);
            listOnReplyMessage.Items.Add("[SendOverseaOptionProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnSendForeignStockProxyOrder_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOS.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strStockNo;
            string strPrice;
            string strCurrency1;
            string strCurrency2;
            string strCurrency3;
            string strExchangeNo = "";
            int nBidAsk;
            int nAccountType;
            int nQty = 0;
            int nTradeType = 0;
            string strProxyQty = "";
            if (boxProxyAccountType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇專戶別");
                return;
            }
            nAccountType = boxProxyAccountType.SelectedIndex + 1;

            if (boxProxyExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxProxyExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxProxyExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxProxyExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxProxyExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxProxyExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxProxyExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxProxyExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }

            if (boxProxyBidAsk.SelectedIndex == 0 && boxProxyCurrency1.SelectedIndex < 0)
            {
                MessageBox.Show("買單請至少選擇扣款幣別 1");
                return;
            }
            strCurrency1 = boxProxyCurrency1.Text;
            strCurrency2 = boxProxyCurrency2.Text;
            strCurrency3 = boxProxyCurrency3.Text;

            if (txtProxyOStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyOStockNo.Text.Trim();

            if (boxProxyBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxProxyBidAsk.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtProxyPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtProxyPrice.Text.Trim();

            if (nBidAsk == 1 && strExchangeNo == "US")
            {
                if (boxProxyTradeType.SelectedIndex < 0)
                {
                    MessageBox.Show("請選擇庫存別");
                    return;
                }
                nTradeType = boxProxyTradeType.SelectedIndex + 1;
            }
            else
            {
                if (boxProxyTradeType.SelectedIndex < 0)
                    nTradeType = 0;
                else
                {
                    nTradeType = boxProxyTradeType.SelectedIndex + 1;
                }
            }

            double dQty = 0.0;
            if (nBidAsk == 1 && strExchangeNo == "US" && nTradeType == 2)
            {
                if (double.TryParse(txtProxyQty.Text.Trim(), out dQty) == false)
                {
                    MessageBox.Show("委託量請輸入數字");
                    return;
                }
            }
            else
            {
                if (int.TryParse(txtProxyQty.Text.Trim(), out nQty) == false)
                {
                    MessageBox.Show("委託量請輸入整數數字");
                    return;
                }
            }
            strProxyQty = txtProxyQty.Text.Trim();

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendForeignStockProxyOrder(loginID, account, strStockNo, strExchangeNo, strPrice, strCurrency1, strCurrency2, strCurrency3, strProxyQty, nAccountType, nBidAsk + 1, nTradeType);
            listOnReplyMessage.Items.Add("[SendForeignStockProxyOrder]回傳值:" + nCode + " " + message);
        }
        private void btnProxyCancelForeignOrderBySeqNo_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOS.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            string strExchangeNo = "";
            string strSeqNo = "";
            string strOrderNo = "";
            string strStockNo = "";
            if (boxProxyCancelExchange.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇交易所");
                return;
            }
            if (boxProxyCancelExchange.SelectedIndex == 0)
            {
                strExchangeNo = "US";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 1)
            {
                strExchangeNo = "HK";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 2)
            {
                strExchangeNo = "JP";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 3)
            {
                strExchangeNo = "SP";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 4)
            {
                strExchangeNo = "SG";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 5)
            {
                strExchangeNo = "SA";
            }
            else if (boxProxyCancelExchange.SelectedIndex == 6)
            {
                strExchangeNo = "HA";
            }

            if (txtProxyOStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtProxyOStockNo2.Text.Trim();

            if (txtProxyCancelSeqNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託序號");
                return;
            }
            strSeqNo = txtProxyCancelSeqNo.Text.Trim();

            if (txtProxyCancelBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strOrderNo = txtProxyCancelBookNo.Text.Trim();

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendForeignStockProxyCancel(loginID, account, strStockNo, strExchangeNo, strSeqNo, strOrderNo);
            listOnReplyMessage.Items.Add("[SendForeignStockProxyCancel]回傳值:" + nCode + " " + message);
        }
        private void btnQueryOddLot_Click(object sender, EventArgs e)
        {
            // UI
            {
            gridStocks.ClearSelection();

            m_dtStocks.Clear();
            gridStocks.DataSource = m_dtStocks;

            gridStocks.Columns["m_nStockidx"].Visible = false;
            gridStocks.Columns["m_sDecimal"].Visible = false;
            gridStocks.Columns["m_sTypeNo"].Visible = false;
            gridStocks.Columns["m_cMarketNo"].Visible = false;
            gridStocks.Columns["m_caStockNo"].HeaderText = "代碼";
            gridStocks.Columns["m_caName"].HeaderText = "名稱";
            gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
            gridStocks.Columns["m_nHigh"].HeaderText = "最高";
            gridStocks.Columns["m_nLow"].HeaderText = "最低";
            gridStocks.Columns["m_nClose"].HeaderText = "成交價";
            gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
            gridStocks.Columns["m_nRef"].HeaderText = "昨收價";
            gridStocks.Columns["m_nBid"].HeaderText = "買價";
            gridStocks.Columns["m_nBc"].HeaderText = "買量";
            gridStocks.Columns["m_nAsk"].HeaderText = "賣價";
            gridStocks.Columns["m_nAc"].HeaderText = "賣量";
            gridStocks.Columns["m_nTBc"].HeaderText = "買盤量";
            gridStocks.Columns["m_nTAc"].HeaderText = "賣盤量";
            gridStocks.Columns["m_nFutureOI"].Visible = false;
            gridStocks.Columns["m_nTQty"].HeaderText = "總量";
            gridStocks.Columns["m_nYQty"].HeaderText = "昨量";
            gridStocks.Columns["m_nUp"].HeaderText = "漲停";
            gridStocks.Columns["m_nDown"].HeaderText = "跌停";

            gridStocks.Columns["m_nCloseS"].HeaderText = "試撮成交價";
            gridStocks.Columns["m_nTickQtyS"].HeaderText = "試撮單量";
            gridStocks.Columns["m_nBidS"].HeaderText = "試撮買價";
            gridStocks.Columns["m_nAskS"].HeaderText = "試撮賣價";

            gridStocks.Columns["m_nOddLotPer"].HeaderText = "整零價差";
            gridStocks.Columns["m_nDealTime"].HeaderText = "成交時間";
            }

            string[] Stocks = txtStocks2.Text.Trim().Split(new Char[] { ',' });
            foreach (string s in Stocks)
            {
                SK.SKSTOCKLONG2 pSKStockLONG = new SK.SKSTOCKLONG2();
                if (pSKStockLONG.nCode == 0)
                    OnUpDateDataRow(pSKStockLONG);
            }

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            int nCode = SK.SKQuoteLib_RequestStocksOddLot(txtStocks2.Text.Trim());
            listOnReplyMessage.Items.Add("[SKQuoteLib_RequestStocksOddLot]回傳值:" + SK.GetMessage(nCode));

        }
        private void btnQueryStocks_Click(object sender, EventArgs e)
        {
            // UI
            {
                m_dtStocks.Clear();
                gridStocks.ClearSelection();

                gridStocks.DataSource = m_dtStocks;

                gridStocks.Columns["m_nStockidx"].Visible = false;
                gridStocks.Columns["m_sDecimal"].Visible = false;
                gridStocks.Columns["m_sTypeNo"].Visible = false;
                gridStocks.Columns["m_cMarketNo"].Visible = false;
                gridStocks.Columns["m_caStockNo"].HeaderText = "代碼";
                gridStocks.Columns["m_caName"].HeaderText = "名稱";
                gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
                gridStocks.Columns["m_nHigh"].HeaderText = "最高";
                gridStocks.Columns["m_nLow"].HeaderText = "最低";
                gridStocks.Columns["m_nClose"].HeaderText = "成交價";
                gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
                gridStocks.Columns["m_nRef"].HeaderText = "昨收價";
                gridStocks.Columns["m_nBid"].HeaderText = "買價";
                gridStocks.Columns["m_nBc"].HeaderText = "買量";
                gridStocks.Columns["m_nAsk"].HeaderText = "賣價";
                gridStocks.Columns["m_nAc"].HeaderText = "賣量";
                gridStocks.Columns["m_nTBc"].HeaderText = "買盤量";
                gridStocks.Columns["m_nTAc"].HeaderText = "賣盤量";
                gridStocks.Columns["m_nFutureOI"].Visible = false;
                gridStocks.Columns["m_nTQty"].HeaderText = "總量";
                gridStocks.Columns["m_nYQty"].HeaderText = "昨量";
                gridStocks.Columns["m_nUp"].HeaderText = "漲停";
                gridStocks.Columns["m_nDown"].HeaderText = "跌停";
                gridStocks.Columns["m_nDealTime"].HeaderText = "成交時間";

                gridStocks.Columns[0].Frozen = true;
            }
            string[] Stocks = txtStocks.Text.Trim().Split(new Char[] { ',' });

            foreach (string s in Stocks)
            {
                SK.SKSTOCKLONG2 pSKStockLONG = new SK.SKSTOCKLONG2();

                if (pSKStockLONG.nCode == 0)
                    OnUpDateDataRow(pSKStockLONG);
            }
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            int nCode = SK.SKQuoteLib_RequestStocks(txtStocks.Text.Trim());
            listOnReplyMessage.Items.Add("[SKQuoteLib_RequestStocks]回傳值:" + SK.GetMessage(nCode));
        }
        private void btnCancelStocks_Click(object sender, EventArgs e)
        {
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 這裡範例為取消整股報價

            int nCode = SK.SKQuoteLib_CancelRequestStocks(txtStocks.Text.Trim());
            listOnReplyMessage.Items.Add("[SKQuoteLib_CancelRequestStocks]回傳值:" + SK.GetMessage(nCode));
        }
        private void btnTicks_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            GridBest5Ask.DataSource = m_dtBest5Ask;
            GridBest5Bid.DataSource = m_dtBest5Bid;

            GridBest5Ask.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Ask.Columns["m_nAskQty"].Width = 60;
            GridBest5Ask.Columns["m_nAsk"].HeaderText = "賣價";
            GridBest5Ask.Columns["m_nAsk"].Width = 60;

            GridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Bid.Columns["m_nAskQty"].Width = 60;
            GridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            GridBest5Bid.Columns["m_nAsk"].Width = 60;

            // 參數設定程式碼省略(可參考完整C#範例程式)
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            int nCode = SK.SKQuoteLib_RequestTicks(int.Parse(txtItemQuote.Text.Trim()), txtTick.Text.Trim());
            listOnReplyMessage.Items.Add("[SKQuoteLib_RequestTicks]回傳值:" + SK.GetMessage(nCode));
        }
        private void btnTickStop_Click(object sender, EventArgs e)
        {
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            int nCode = SK.SKQuoteLib_CancelRequestTicks(txtTick.Text.Trim());
            listOnReplyMessage.Items.Add("[SKQuoteLib_CancelRequestTicks]回傳值:" + SK.GetMessage(nCode));
        }
        private void chkbox_msms_CheckedChanged(object sender, EventArgs e)
        {//市價轉換與含msns一致
            if (chkbox_msms.Checked == true)
                Box_M.Checked = true;
            else
                Box_M.Checked = false;
        }
        private void Box_M_CheckedChanged(object sender, EventArgs e)
        {//市價轉換與含msns一致
            if (Box_M.Checked == true)
                chkbox_msms.Checked = true;
            else
                chkbox_msms.Checked = false;
        }
        private void buttonLoadCommodity_Click(object sender, EventArgs e)
        {
            // 取得選擇的市場代碼（0=上市, 1=上櫃, 2=期貨...）
            int nMarketNo = comboBoxnMarketNo.SelectedIndex;

            // 呼叫群益 API 下載該市場的商品資訊（股票、期貨等）
            int result = SK.LoadCommodity(nMarketNo);

            // 顯示下載結果（由 API 回傳的訊息）到 ListBox
            listOnReplyMessage.Items.Add("[LoadCommodity]回傳值:" + SK.GetMessage(result));
        }
        private void btnTicks_OddLot_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            GridBest5Ask.DataSource = m_dtBest5Ask;
            GridBest5Bid.DataSource = m_dtBest5Bid;

            GridBest5Ask.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Ask.Columns["m_nAskQty"].Width = 60;
            GridBest5Ask.Columns["m_nAsk"].HeaderText = "賣價";
            GridBest5Ask.Columns["m_nAsk"].Width = 60;

            GridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Bid.Columns["m_nAskQty"].Width = 60;
            GridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            GridBest5Bid.Columns["m_nAsk"].Width = 60;

            // 參數設定程式碼省略(可參考完整C#範例程式)
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            int nCode = SK.SKQuoteLib_RequestTicksOddLot(int.Parse(txtItemQuote.Text.Trim()), txtTick.Text.Trim());
            listOnReplyMessage.Items.Add("[SKQuoteLib_RequestTicksOddLot]回傳值:" + SK.GetMessage(nCode));
        }
        private void RequestStockListBtn_Click(object sender, EventArgs e)
        {
            StockList.Items.Clear();

            if (MarketNo_txt.Text.Trim() == "")
            {
                MessageBox.Show("請輸入市場代碼");
                return;
            }
            int nMarketNo = int.Parse(MarketNo_txt.Text.Trim());

            // ✅ 呼叫封裝方法，取得物件
            var parser = SK.RequestStockList(nMarketNo);
            string a = parser.RawData;            
            // ✅ 顯示該市場下所有商品資料（每個 TypeNo 下的商品）
            foreach (var result in parser.AllTypeLists)
            {
                StockList.Items.Add($"【類別:{result.TypeNo},{result.TypeName}】");

                foreach (var item in result.Items)
                {
                    StockList.Items.Add($"  {item.strQuoteCode},{item.strStockName},{item.strOrderCode},{item.strExpiryDate}"); // 取出 行情代號,商品名稱,下單代號,最後交易日(僅期選)
                }
            }

            // ✅ 從物件中再特別取出 TypeNo 下的商品
            int typeNo = int.Parse(TypeNo_txt.Text);
            var result2 = parser.GetTypeNo(typeNo);

            if (result2 != null)
            {
                StockList.Items.Add($"【%{result2.TypeNo}% {result2.TypeName}】");

                foreach (var item in result2.Items)
                {
                    StockList.Items.Add($"  {item.strQuoteCode},{item.strStockName},{item.strOrderCode},{item.strExpiryDate}"); // 取出 行情代號,商品名稱,下單代號,最後交易日(僅期選)
                }
            }
            else
            {
                StockList.Items.Add($"查無 TypeNo {typeNo}");
            }

            // ✅ 從該市場中再取出 TypeNo 資料(例如 "1水泥", "2食品")
            string AllType = "";
            var typeList = parser.GetAllType();
            foreach (var item in typeList)
            {
                AllType += item; // 例如 "1水泥", "2食品"
            }
        }

        private void buttonGetForeignBlock_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOS.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nCurrencyType;

            if (comboBoxGetForeignBlocknFunc.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇幣別");
                return;
            }
            nCurrencyType = comboBoxGetForeignBlocknFunc.SelectedIndex;

            // 參數設定程式碼省略(可參考完整C#範例程式)
            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            var result = SK.GetForeignBlock(loginID, account, nCurrencyType);
            listOnReplyMessage.Items.Add("[GetForeignBlock]回傳值:" + result.StatusCode + " " + result.Message);

            if (result.StatusCode == 0 && result.Blocks.Count > 0)
            {
                foreach (var block in result.Blocks)
                {
                    listOnReplyMessage.Items.Add(
                        $"銀行: {block.BankName} ({block.BankCode}-{block.BankBranchCode}-{block.BankAccount})"
                    );

                    listOnReplyMessage.Items.Add(
                        $"幣別: {block.Currency}, 圈存金額: {block.UnpayableAmt}, 買進未扣款: {block.UnpayableBuy}, " +
                        $"股市委買: {block.TodayOrder}, 匯出換匯金額: {block.OutAmt}, 可解圈金額: {block.UnblockAmt}, 基金委買: {block.FundOrderAmt}"
                    );

                    listOnReplyMessage.Items.Add("--------------------------------------------------");
                }
            }
            else
            {
                listOnReplyMessage.Items.Add("[GetForeignBlock]沒有資料或回傳錯誤");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strLoginID = textBoxUserID.Text;
            string strAccountOut;
            string strAccountIn;
            int nTypeOut;
            int nTypeIn;
            int nCurrency;
            string strDollars;
            string strPWD;

            if (boxTypeOut.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇轉出類別");
                return;
            }
            nTypeOut = boxTypeOut.SelectedIndex;

            if (textBoxAccountOut.Text == "")
            {
                MessageBox.Show("請輸入轉出帳號");
                return;
            }
            strAccountOut = textBoxAccountOut.Text;


            if (boxTypeIn.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇轉入類別");
                return;
            }
            nTypeIn = boxTypeIn.SelectedIndex;

            if (textBoxAccountIn.Text == "")
            {
                MessageBox.Show("請輸入轉入帳號");
                return;
            }
            strAccountIn = textBoxAccountIn.Text;

            if (boxCurrency.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇幣別");
                return;
            }
            nCurrency = boxCurrency.SelectedIndex;

            if (txtDollars.Text == "")
            {
                MessageBox.Show("請輸入互轉金額");
                return;
            }
            strDollars = txtDollars.Text;

            if (txtPWD.Text == "")
            {
                MessageBox.Show("請輸入出金密碼");
                return;
            }
            strPWD = txtPWD.Text;

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件

            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.WithDraw(strLoginID, strAccountOut, nTypeOut, strAccountIn, nTypeIn, nCurrency, strDollars, strPWD);
            listOnReplyMessage.Items.Add("[WithDraw]回傳值:" + nCode + " " + message);
        }

        private void btnSendTFOffset_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nBidAsk, nQty, nQty_2, nQty_3, nCommodity;
            string strYearMonth;

            if (txtOffsetNewYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtOffsetNewYearMonth.Text.Trim();

            if (CommodityBoxNew.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇互抵商品");
                return;
            }
            nCommodity = CommodityBoxNew.SelectedIndex;

            if (boxOffsetNewBuySell.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxOffsetNewBuySell.SelectedIndex;

            if (int.TryParse(txtOffsetNewQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量1請輸入數字");
                return;
            }
            if (int.TryParse(txtOffsetNewQty_2.Text.Trim(), out nQty_2) == false)
            {
                MessageBox.Show("委託量2請輸入數字");
                return;
            }
            if (int.TryParse(txtOffsetNewQty_3.Text.Trim(), out nQty_3) == false)
            {
                MessageBox.Show("委託量3請輸入數字");
                return;
            }

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 參數設定程式碼省略(可參考完整C#範例程式)
            var (nCode, message) = SK.SendTFOffset(loginID, account, nCommodity, strYearMonth, nBidAsk, nQty, nQty_2, nQty_3);
            listOnReplyMessage.Items.Add("[SendTFOffset]回傳值:" + nCode + " " + message);
        }

        private void btnGetOpenInterestGW_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nFormat;

            if (boxGetOpenInterestGWFormat.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇格式");
                return;
            }
            nFormat = boxGetOpenInterestGWFormat.SelectedIndex;

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 參數設定程式碼省略(可參考完整C#範例程式)
            var result = SK.GetOpenInterestGW(loginID, account, nFormat);
            listOnReplyMessage.Items.Add("[GetOpenInterestGW]回傳值:" + result.StatusCode + " " + result.Message);

            if (result.StatusCode == 0 && result.Blocks.Count > 0)
            {
                foreach (var block in result.Blocks)
                {
                    listOnReplyMessage.Items.Add(
                        $"帳號資料: {block.MarketType}-({block.Account}-{block.LoginID})"
                    );

                    listOnReplyMessage.Items.Add(
                        $"商品: {block.FutureNo}, 買賣別: {block.BuySell}, 未平倉口數: {block.Qty}, " +
                        $"當沖未平口數: {block.Qty_Trade}, 平均成本: {block.AvgPrice}, 手續費: {block.Fee}, 交易稅: {block.Tax}"
                    );

                    listOnReplyMessage.Items.Add("--------------------------------------------------");
                }
            }
            else
            {
                listOnReplyMessage.Items.Add("[GetOpenInterestGW]沒有資料或回傳錯誤");
            }
        }

        private void btnGetOFOpenInterestGW_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxOF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nFormat;

            if (boxGetOFOpenInterestGWFormat.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇格式");
                return;
            }
            nFormat = boxGetOFOpenInterestGWFormat.SelectedIndex;

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 參數設定程式碼省略(可參考完整C#範例程式)
            var result = SK.GetOFOpenInterestGW(loginID, account, nFormat);
            listOnReplyMessage.Items.Add("[GetOFOpenInterestGW]回傳值:" + result.StatusCode + " " + result.Message);

            if (result.StatusCode == 0 && result.Blocks.Count > 0)
            {
                foreach (var block in result.Blocks)
                {
                    listOnReplyMessage.Items.Add(
                        $"帳號資料: {block.IsOption}-({block.Account}-{block.LoginID})"
                    );
                    
                    if(block.IsOption == "OF")
                    {
                        listOnReplyMessage.Items.Add(
                            $"交易所: {block.ExchangeName} {block.ExchangeCode},商品: {block.ProductNameWithYM} {block.ProductCodeWithYM},買賣別: {block.BuySell},未平倉口數: {block.OpenInterest},平均價: {block.AvgPrice},現價: {block.MarketPrice}," +
                            $"未平損益: {block.UnrealizedPL},委託買價/量: {block.BidPrice}/{block.BidVolume},委託賣價/量: {block.AskPrice}/{block.AskVolume},賣方新倉口數: {block.AskNewOpenVolume},昨日結算價: {block.PrevSettlementPrice},當沖未平口數: {block.DayTradeOpenInterest}," +
                            $"原始保證金: {block.InitialMargin}"
                        );
                    }
                    else if(block.IsOption == "OO")
                    {
                        listOnReplyMessage.Items.Add(
                            $"交易所: {block.ExchangeName} {block.ExchangeCode},商品: {block.ProductNameWithYM} {block.ProductCodeWithYM},買賣別: {block.BuySell},未平倉口數: {block.OpenInterest},平均價: {block.AvgPrice},現價: {block.MarketPrice}," +
                            $"未平損益: {block.UnrealizedPL},委託買價/量: {block.BidPrice}/{block.BidVolume},委託賣價/量: {block.AskPrice}/{block.AskVolume},賣方新倉口數: {block.AskNewOpenVolume},昨日結算價: {block.PrevSettlementPrice},當沖未平口數: {block.DayTradeOpenInterest}," +
                            $"原始保證金: {block.InitialMargin},履約價: {block.StrikePrice},C/P: {block.CallPut}"
                        );
                    }


                    listOnReplyMessage.Items.Add("--------------------------------------------------");
                }
            }
            else
            {
                listOnReplyMessage.Items.Add("[GetOFOpenInterestGW]沒有資料或回傳錯誤");
            }
        }
        private void btnGetRealBalanceReport_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTS.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 參數設定程式碼省略(可參考完整C#範例程式)
            var result = SK.GetRealBalanceReport(loginID, account);
            listOnReplyMessage.Items.Add("[GetRealBalanceReport]回傳值:" + result.StatusCode + " " + result.Message);

            if (result.StatusCode == 0 && result.Blocks.Count > 0)
            {
                foreach (var block in result.Blocks)
                {
                    listOnReplyMessage.Items.Add(
                        $"帳號資料: {block.LoginID}-{block.AccountNo}"
                    );

                    listOnReplyMessage.Items.Add(
                        $"股票代號: {block.StockCode},庫存種類: {block.InventoryType},資額度(原始): {block.CreditLimitOriginal},資額度(可用): {block.CreditLimitAvailable}," +
                        $"券額度(原始): {block.ShortLimitOriginal},券額度(可用): {block.ShortLimitAvailable},昨日庫存: {block.YesterdayInventory},今日委買: {block.TodayBidQty},今日委賣: {block.TodayAskQty}," +
                        $"今日買進成交: {block.TodayBuyMatched},今日賣出成交: {block.TodaySellMatched},今日資券可賣出/回補: {block.AvailableToSellOrCover},可資沖股數: {block.DayTradeBuyableQty},可券沖股數: {block.DayTradeSellableQty}," +
                        $"即時庫存: {block.RealTimeInventory},即時個股維持率: {block.RealTimeMarginRate}"
                    );

                    listOnReplyMessage.Items.Add("--------------------------------------------------");
                }
            }
            else
            {
                listOnReplyMessage.Items.Add("[GetRealBalanceReport]沒有資料或回傳錯誤");
            }
        }

        private void btnGetFutureRights_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nCoinType;

            if (boxGetFutureRights_CoinType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇格式");
                return;
            }
            nCoinType = boxGetFutureRights_CoinType.SelectedIndex;


            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 參數設定程式碼省略(可參考完整C#範例程式)
            var result = SK.GetFutureRights(loginID, account, nCoinType);
            listOnReplyMessage.Items.Add("[GetFutureRights]回傳值:" + result.StatusCode + " " + result.Message);

            if (result.StatusCode == 0 && result.Blocks.Count > 0)
            {
                foreach (var block in result.Blocks)
                {
                    listOnReplyMessage.Items.Add(
                        $"帳號資料: {block.LoginID}-{block.AccountNo}"
                    );

                    listOnReplyMessage.Items.Add(
                        $"帳戶餘額: {block.AccountBalance},浮動損益: {block.FloatingPL},已實現費用: {block.RealizedFee},交易稅: {block.TransactionTax},預扣權利金: {block.WithheldPremium}," +
                        $"權利金收付: {block.PremiumPayment},權益數: {block.Equity},超額保證金: {block.ExcessMargin},存提款: {block.DepositWithdrawal},買方市值: {block.BuyerMarketValue}," +
                        $"賣方市值: {block.SellerMarketValue},期貨平倉損益: {block.FuturesClosePL},盤中未實現: {block.IntradayUnrealized},原始保證金: {block.InitialMargin},維持保證金: {block.MaintenanceMargin}," +
                        $"部位原始保證金: {block.PositionInitialMargin},部位維持保證金: {block.PositionMaintenanceMargin},委託保證金: {block.OrderMargin},超額最佳保證金: {block.ExcessOptimalMargin},權利總值: {block.TotalPremiumValue}," +
                        $"預扣費用: {block.WithheldFee},原始保證金: {block.OriginalInitialMargin},昨日餘額: {block.YesterdayBalance},選擇權組合單加不加收保證金: {block.OptionComboMarginFlag},維持率: {block.MaintenanceRate}," +
                        $"幣別: {block.Currency},足額原始保證金: {block.FullInitialMargin},足額維持保證金: {block.FullMaintenanceMargin},足額可用: {block.FullAvailable},抵繳金額: {block.OffsetAmount}," +
                        $"有價可用: {block.SecuritiesAvailable},可用餘額: {block.AvailableBalance},足額現金可用: {block.FullCashAvailable},有價價值: {block.SecuritiesValue},風險指標: {block.RiskIndicator}," +
                        $"選擇權到期差異: {block.OptionExpiryDifference},選擇權到期差損: {block.OptionExpiryPL},期貨到期損益: {block.FuturesExpiryPL},加收保證金: {block.AdditionalMargin}"
                    );

                    listOnReplyMessage.Items.Add("--------------------------------------------------");
                }
            }
            else
            {
                listOnReplyMessage.Items.Add("[GetFutureRights]沒有資料或回傳錯誤");
            }
        }

        private void btnGetOFFutureRights_Click(object sender, EventArgs e)
        {
            string loginID = "";
            string account = "";
            string selectedText = comboBoxTF.Text;
            string[] parts = selectedText.Split(' '); // 以空白分隔
            if (parts.Length >= 2)
            {
                loginID = parts[0]; // 第一部分就是登入ID
                account = parts[1]; // 第二部分就是帳號
            }

            int nCoinType;

            if (boxGetOFFutureRights_CoinType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇格式");
                return;
            }
            nCoinType = boxGetOFFutureRights_CoinType.SelectedIndex;


            // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件
            // 參數設定程式碼省略(可參考完整C#範例程式)
            var result = SK.GetOFFutureRights(loginID, account, nCoinType);
            listOnReplyMessage.Items.Add("[GetOFFutureRights]回傳值:" + result.StatusCode + " " + result.Message);

            if (result.StatusCode == 0 && result.Blocks.Count > 0)
            {
                foreach (var block in result.Blocks)
                {
                    listOnReplyMessage.Items.Add(
                        $"帳號資料: {block.LoginID}-{block.AccountNo}"
                    );

                    listOnReplyMessage.Items.Add(
                        $"幣別: {block.Currency}, 風險指標: {block.RiskIndicator}, 足額維持率: {block.FullMaintenanceMargin}, " +
                        $"可動用保證金: {block.WithdrawableMargin}, 委託保證金: {block.OrderMargin}, 維持保證金: {block.MaintenanceMargin}, 原始保證金: {block.InitialMargin}, " +
                        $"淨值權益數: {block.NetEquity}, 預扣權利金: {block.WithheldPremium}, 未沖銷買方選擇權市值: {block.OptionLongMarketValue}, 未沖銷賣方選擇權市值: {block.OptionShortMarketValue}, " +
                        $"帳戶權益: {block.AccountEquity}, 盤中未實現: {block.IntradayUnrealized}, 未沖銷期貨浮動損益: {block.FuturesFloatingPL}, 今日權利金支付: {block.PremiumPaidToday}, " +
                        $"本日期貨平倉損益淨額: {block.FuturesClosePLToday}, 已實現費用、手續費: {block.RealizedFee}, 今日存提款: {block.DepositWithdrawalToday}, 前日帳戶餘額: {block.PreviousDayBalance}, " +
                        $"參考匯率: {block.ReferenceRate}, 選擇權到期差益: {block.OptionExpiryGain}, 選擇權到期差損: {block.OptionExpiryLoss}, 期貨交割損益: {block.FuturesDeliveryPL}, " +
                        $"超額/追繳保證金: {block.ExcessOrMarginCall}, 到期履約損益: {block.ExerciseSettlementPL}, 帳戶餘額: {block.AccountBalance}, 固定保證金: {block.FixedMargin}, 變動保證金: {block.VariableMargin}"
                    );


                    listOnReplyMessage.Items.Add("--------------------------------------------------");
                }
            }
            else
            {
                listOnReplyMessage.Items.Add("[GetOFFutureRights]沒有資料或回傳錯誤");
            }
        }
    }
}