#include "SKQuote.h"

DataTable^ CppCLITester::SKQuote::CreateStocksDataTable()
{
    DataTable^ myDataTable = gcnew DataTable();

    DataColumn^ myDataColumn;

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nStockidx";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int16");
    myDataColumn->ColumnName = "m_sDecimal";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int16");
    myDataColumn->ColumnName = "m_sTypeNo";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_cMarketNo";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_caStockNo";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_caName";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nOpen";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nHigh";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nLow";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nClose";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nTickQty";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nRef";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    //myDataColumn.DataType = Type::GetType("System.Double");
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_nBid";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nBc";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    //myDataColumn.DataType = Type::GetType("System.Double");
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_nAsk";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nAc";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nTBc";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nTAc";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nFutureOI";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nTQty";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nYQty";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nUp";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nDown";
    myDataTable->Columns->Add(myDataColumn);


    // 設定 DataTable的 PrimaryKey
    array<System::Data::DataColumn^>^ colarray = gcnew array<System::Data::DataColumn^>(1);
    colarray[0] = myDataTable->Columns["m_caStockNo"];
    myDataTable->PrimaryKey = colarray;

    return myDataTable;
}

DataTable^ CppCLITester::SKQuote::CreateBest5AskTable()
{
    DataTable^ myDataTable = gcnew DataTable();

    DataColumn^ myDataColumn;

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_nAskQty";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    //myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_nAsk";
    myDataTable->Columns->Add(myDataColumn);

    return myDataTable;
}

Void CppCLITester::SKQuote::OnConnection(int nKind, int nCode)
{
    if (nKind == 3001)
    {
        if (nCode == 0)
        {
            lblSignal->ForeColor = Color::Yellow;
        }
    }
    else if (nKind == 3002)
    {
        lblSignal->ForeColor = Color::Red;
    }
    else if (nKind == 3003)
    {
        lblSignal->ForeColor = Color::Green;
    }
    else if (nKind == 3021)
    {
        lblSignal->ForeColor = Color::DarkRed;
    }
    else if (nKind == 3032)
    {
        lblSignal->ForeColor = Color::DimGray;
    }
}

Void CppCLITester::SKQuote::OnNotifyQuoteLONG(short sMarketNo, int nStockIdx)
{
    SKCOMLib::SKSTOCKLONG pSKStock;

    m_pSKQuote->SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, pSKStock);

    OnUpDateDataRow(pSKStock);
}

Void CppCLITester::SKQuote::OnNotifyBest5LONG(short sMarketNo, int nStockidx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
{
    // 取得價位的小數位數
    SKCOMLib::SKSTOCKLONG pSKStock;
    double dDigitNum = 0.000;
    System::String^ strStockNoTick = txtTick->Text->Trim();
    int nCode = m_pSKQuote->SKQuoteLib_GetStockByNoLONG(strStockNoTick, pSKStock);
    if (nCode == 0)
        dDigitNum = (Math::Pow(10, pSKStock.sDecimal));
    else
        dDigitNum = 100.00;//default value 

    if (m_dBest5Bid->Rows->Count == 0 && m_dBest5Ask->Rows->Count == 0)
    {
        //買
        DataRow^ MyDataRow = m_dBest5Bid->NewRow();
        MyDataRow["m_nAskQty"] = nBestBidQty1;
        if (nBestBid1 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        { 
            MyDataRow["m_nAsk"] = (nBestBid1 / dDigitNum).ToString();
        }
        m_dBest5Bid->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Bid->NewRow();
        MyDataRow["m_nAskQty"] = nBestBidQty2;
        if (nBestBid2 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestBid2 / dDigitNum).ToString();
        }
        m_dBest5Bid->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Bid->NewRow();
        MyDataRow["m_nAskQty"] = nBestBidQty3;
        if (nBestBid3 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestBid3 / dDigitNum).ToString();
        }
        m_dBest5Bid->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Bid->NewRow();
        MyDataRow["m_nAskQty"] = nBestBidQty4;
        if (nBestBid4 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestBid4 / dDigitNum).ToString();
        }
        m_dBest5Bid->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Bid->NewRow();
        MyDataRow["m_nAskQty"] = nBestBidQty5;
        if (nBestBid5 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestBid5 / dDigitNum).ToString();
        }
        m_dBest5Bid->Rows->Add(MyDataRow);


        //賣
        MyDataRow = m_dBest5Ask->NewRow();
        MyDataRow["m_nAskQty"] = nBestAskQty1;
        if (nBestAsk1 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestAsk1 / dDigitNum).ToString();
        }
        m_dBest5Ask->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Ask->NewRow();
        MyDataRow["m_nAskQty"] = nBestAskQty2;
        if (nBestAsk2 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestAsk2 / dDigitNum).ToString();
        }
        m_dBest5Ask->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Ask->NewRow();
        MyDataRow["m_nAskQty"] = nBestAskQty3;
        if (nBestAsk3 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestAsk3 / dDigitNum).ToString();
        }
        m_dBest5Ask->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Ask->NewRow();
        MyDataRow["m_nAskQty"] = nBestAskQty4;
        if (nBestAsk4 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestAsk4 / dDigitNum).ToString();
        }
        m_dBest5Ask->Rows->Add(MyDataRow);

        MyDataRow = m_dBest5Ask->NewRow();
        MyDataRow["m_nAskQty"] = nBestAskQty5;
        if (nBestAsk5 == kMarketPrice)
        {
            MyDataRow["m_nAsk"] = "m";
        }
        else
        {
            MyDataRow["m_nAsk"] = (nBestAsk5 / dDigitNum).ToString();
        }
        m_dBest5Ask->Rows->Add(MyDataRow);
    }
    else
    {
        //買
        GridBest5Bid->Rows[0]->Cells["m_nAskQty"]->Value = nBestBidQty1;
        if (nBestBid1 == kMarketPrice)
        {
            GridBest5Bid->Rows[0]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Bid->Rows[0]->Cells["m_nAsk"]->Value = (nBestBid1 / dDigitNum).ToString();
        }

        GridBest5Bid->Rows[1]->Cells["m_nAskQty"]->Value = nBestBidQty2;
        if (nBestBid2 == kMarketPrice)
        {
            GridBest5Bid->Rows[1]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Bid->Rows[1]->Cells["m_nAsk"]->Value = (nBestBid2 / dDigitNum).ToString();
        }

        GridBest5Bid->Rows[2]->Cells["m_nAskQty"]->Value = nBestBidQty3;
        if (nBestBid3 == kMarketPrice)
        {
            GridBest5Bid->Rows[2]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Bid->Rows[2]->Cells["m_nAsk"]->Value = (nBestBid3 / dDigitNum).ToString();
        }

        GridBest5Bid->Rows[3]->Cells["m_nAskQty"]->Value = nBestBidQty4;
        if (nBestBid4 == kMarketPrice)
        {
            GridBest5Bid->Rows[3]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Bid->Rows[3]->Cells["m_nAsk"]->Value = (nBestBid4 / dDigitNum).ToString();
        }

        GridBest5Bid->Rows[4]->Cells["m_nAskQty"]->Value = nBestBidQty5;
        if (nBestBid5 == kMarketPrice)
        {
            GridBest5Bid->Rows[4]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Bid->Rows[4]->Cells["m_nAsk"]->Value = (nBestBid5 / dDigitNum).ToString();
        }

        //賣
        GridBest5Ask->Rows[0]->Cells["m_nAskQty"]->Value = nBestAskQty1;
        if (nBestAsk1 == kMarketPrice)
        {
            GridBest5Ask->Rows[0]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Ask->Rows[0]->Cells["m_nAsk"]->Value = (nBestAsk1 / dDigitNum).ToString();
        }

        GridBest5Ask->Rows[1]->Cells["m_nAskQty"]->Value = nBestAskQty2;
        if (nBestAsk2 == kMarketPrice)
        {
            GridBest5Ask->Rows[1]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Ask->Rows[1]->Cells["m_nAsk"]->Value = (nBestAsk2 / dDigitNum).ToString();
        }

        GridBest5Ask->Rows[2]->Cells["m_nAskQty"]->Value = nBestAskQty3;
        if (nBestAsk3 == kMarketPrice)
        {
            GridBest5Ask->Rows[2]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Ask->Rows[2]->Cells["m_nAsk"]->Value = (nBestAsk3 / dDigitNum).ToString();
        }

        GridBest5Ask->Rows[3]->Cells["m_nAskQty"]->Value = nBestAskQty4;
        if (nBestAsk4 == kMarketPrice)
        {
            GridBest5Ask->Rows[3]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Ask->Rows[3]->Cells["m_nAsk"]->Value = (nBestAsk4 / dDigitNum).ToString();
        }

        GridBest5Ask->Rows[4]->Cells["m_nAskQty"]->Value = nBestAskQty5;
        if (nBestAsk5 == kMarketPrice)
        {
            GridBest5Ask->Rows[4]->Cells["m_nAsk"]->Value = "m";
        }
        else
        {
            GridBest5Ask->Rows[4]->Cells["m_nAsk"]->Value = (nBestAsk5 / dDigitNum).ToString();
        }
    }
}

Void CppCLITester::SKQuote::OnNotifyHistoryTicksLONG(short sMarketNo, int nStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
{
    System::String^  strData = "";

    int nMarketPrice = m_pSKQuote->SKQuoteLib_GetMarketPriceTS();

    if (chkbox_msms->Checked == true)
        strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
    else
        strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + " " + lTimemillismicros.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
    if (Box_M->Checked == true)
    {
        if (nBid == kMarketPrice)
            strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + "M" + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
        else if (nAsk == kMarketPrice)
            strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + "M" + "," + nClose.ToString() + "," + nQty.ToString();
        else
            strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

    }

    if (strData != "" && ((chkBoxSimulate->Checked) || (!chkBoxSimulate->Checked && nSimulate == 0)))
        listTicks->Items->Add("[OnNotifyHistoryTicksLONG]" + strData);
}

Void CppCLITester::SKQuote::OnNotifyTicksLONG(short sMarketNo, int nStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
{
    System::String^ strData = "";

    if (chkbox_msms->Checked == true)
        strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
    else
        strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + " " + lTimemillismicros.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
    if (Box_M->Checked == true)
    {
        if (nBid == kMarketPrice)
            strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + "M" + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
        else if (nAsk == kMarketPrice)
            strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + "M" + "," + nClose.ToString() + "," + nQty.ToString();
        else
            strData = nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
    }

    if (strData != "" && ((chkBoxSimulate->Checked) || (!chkBoxSimulate->Checked && nSimulate == 0)))
        listTicks->Items->Add("[OnNotifyTicksLONG]" + strData);
}

Void CppCLITester::SKQuote::OnNotifyKLineData(System::String^ bstrStockNo, System::String^ bstrData)
{
    listKLine->Items->Add("[OnNotifyKLineData]" + bstrData);
}

System::Void CppCLITester::SKQuote::button1_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_first)
    {
        m_first = false;

        m_pSKQuote->OnConnection += gcnew SKCOMLib::_ISKQuoteLibEvents_OnConnectionEventHandler(this, &CppCLITester::SKQuote::OnConnection);
        // 報價
        m_pSKQuote->OnNotifyQuoteLONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyQuoteLONG);

        // 成交明細以及五檔
        m_pSKQuote->OnNotifyBest5LONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyBest5LONG);
        m_pSKQuote->OnNotifyTicksLONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyTicksLONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyTicksLONG);
        m_pSKQuote->OnNotifyHistoryTicksLONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyHistoryTicksLONG);
        
        //kLine
        m_pSKQuote->OnNotifyKLineData += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyKLineDataEventHandler(this, &CppCLITester::SKQuote::OnNotifyKLineData);
        
        //market Info
        m_pSKQuote->OnNotifyMarketTot += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyMarketTotEventHandler(this, &CppCLITester::SKQuote::OnNotifyMarketTot);
        m_pSKQuote->OnNotifyMarketBuySell += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyMarketBuySellEventHandler(this, &CppCLITester::SKQuote::OnNotifyMarketBuySell);
        m_pSKQuote->OnNotifyMarketHighLow += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyMarketHighLowEventHandler(this, &CppCLITester::SKQuote::OnNotifyMarketHighLow);

        //Bool & MACD
        m_pSKQuote->OnNotifyBoolTunelLONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyBoolTunelLONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyBoolTunelLONG);
        m_pSKQuote->OnNotifyMACDLONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyMACDLONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyMACDLONG);

        //FutureTrade Info
        m_pSKQuote->OnNotifyFutureTradeInfoLONG += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyFutureTradeInfoLONGEventHandler(this, &CppCLITester::SKQuote::OnNotifyFutureTradeInfoLONG);

        //Strike Prices
        m_pSKQuote->OnNotifyStrikePrices += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyStrikePricesEventHandler(this, &CppCLITester::SKQuote::OnNotifyStrikePrices);

        //Stock List
        m_pSKQuote->OnNotifyStockList += gcnew SKCOMLib::_ISKQuoteLibEvents_OnNotifyStockListEventHandler(this, &CppCLITester::SKQuote::OnNotifyStockList);
    }
    kMarketPrice = m_pSKQuote->SKQuoteLib_GetMarketPriceTS();

    int n_mCode = m_pSKQuote->SKQuoteLib_EnterMonitorLONG();

    GetMessage("Quote", n_mCode, "SKQuoteLib_EnterMonitorLONG");
}

Void CppCLITester::SKQuote::btnDisconnect_Click(System::Object^ sender, System::EventArgs^ e)
{
    int n_mCode = m_pSKQuote->SKQuoteLib_LeaveMonitor();

    GetMessage("SKQuote", n_mCode, "SKQuoteLib_LeaveMonitor");
}

Void CppCLITester::SKQuote::OnNotifyMarketTot(short sMarketNo, short sPtr, int nTime, int nTotv, int nTots, int nTotc)
{
    double dTotv = nTotv / 100.0;

    if (sMarketNo == 0)
    {
        lblTotv->Text = dTotv.ToString() + "(億)";
        lblTots->Text = nTots.ToString() + "(張)";
        lblTotc->Text = nTotc.ToString() + "(筆)";
    }
    else
    {
        lblTotv2->Text = dTotv.ToString() + "(億)";
        lblTots2->Text = nTots.ToString() + "(張)";
        lblTotc2->Text = nTotc.ToString() + "(筆)";
    }
}

Void CppCLITester::SKQuote::OnNotifyMarketBuySell(short sMarketNo, short sPtr, int nTime, int nBc, int nSc, int nBs, int nSs)
{
    if (sMarketNo == 0)
    {
        lbllBc->Text = nBc.ToString() + "(筆)";
        lbllBs->Text = nBs.ToString() + "(張)";
        lbllSc->Text = nSc.ToString() + "(筆)";
        lbllSs->Text = nSs.ToString() + "(張)";
    }
    else
    {
        lbllBc2->Text = nBc.ToString() + "(筆)";
        lbllBs2->Text = nBs.ToString() + "(張)";
        lbllSc2->Text = nSc.ToString() + "(筆)";
        lbllSs2->Text = nSs.ToString() + "(張)";
    }
}

Void CppCLITester::SKQuote::OnNotifyMarketHighLow(short sMarketNo, short sPtr, int nTime, short sUp, short sDown, short sHigh, short sLow, short sNoChange)
{
    if (sMarketNo == 0)
    {
        lblsUp->Text = sUp.ToString();
        lblsDown->Text = sDown.ToString();
        lblsHigh->Text = sHigh.ToString();
        lblsLow->Text = sLow.ToString();
        lblsNoChange->Text = sNoChange.ToString();
    }
    else
    {
        lblsUp2->Text = sUp.ToString();
        lblsDown2->Text = sDown.ToString();
        lblsHigh2->Text = sHigh.ToString();
        lblsLow2->Text = sLow.ToString();
        lblsNoChange2->Text = sNoChange.ToString();
    }
}

Void CppCLITester::SKQuote::OnNotifyBoolTunelLONG(short sMarketNo, int nStockidx, System::String^ bstrAVG, System::String^ bstrUBT, System::String^ bstrLBT)
{
    lblAVG->Text = bstrAVG;
    lblUBT->Text = bstrUBT;
    lblLBT->Text = bstrLBT;
}

Void CppCLITester::SKQuote::OnNotifyMACDLONG(short sMarketNo, int nStockidx, System::String^ bstrMACD, System::String^ bstrDIF, System::String^ bstrOSC)
{
    lblMACD->Text = bstrMACD;
    lblDIF->Text = bstrDIF;
    lblOSC->Text = bstrOSC;
}

Void CppCLITester::SKQuote::OnNotifyFutureTradeInfoLONG(System::String^ bstrStockNo, short sMarketNo, int nStockIdx, int nBuyTotalCount, int nSellTotalCount, int nBuyTotalQty, int nSellTotalQty, int nBuyDealTotalCount, int nSellDealTotalCount)
{
    lblMarketNo->Text = "MarketNo";
    lblStockIdx->Text = "StockIndex";
    lblFTIBc->Text = "TotalBc";
    lblFTISc->Text = "TotalSc";
    lblFTIBq->Text = "TotalBq";
    lblFTISq->Text = "TotalSq";
    lblFTIBDC->Text = "TotalBDC";
    lblFTISDC->Text = "TotalSDC";

    lblMarketNo->Text = sMarketNo.ToString();
    lblStockIdx->Text = nStockIdx.ToString();
    lblFTIBc->Text = nBuyTotalCount.ToString();
    lblFTISc->Text = nSellTotalCount.ToString();
    lblFTIBq->Text = nBuyTotalQty.ToString();
    lblFTISq->Text = nSellTotalQty.ToString();
    lblFTIBDC->Text = nBuyDealTotalCount.ToString();
    lblFTISDC->Text = nSellDealTotalCount.ToString();
}

Void CppCLITester::SKQuote::OnNotifyStrikePrices(System::String^ bstrOptionData)
{
    System::String^  strData = "";
    strData = "[OnNotifyStrikePrices]" + bstrOptionData;

    listStrikePrices->Items->Add(strData);
    m_nCount++;
    listStrikePrices->SelectedIndex = listStrikePrices->Items->Count - 1;

    if (bstrOptionData->Substring(0, 2) != "##")   //開頭##表結束，不計商品數量
        txt_StrikePriceCount->Text = m_nCount.ToString();
}

Void CppCLITester::SKQuote::OnNotifyStockList(short sMarketNo, System::String^ bstrStockListData)
{
    System::String^  strData = "";
    strData = "[OnNotifyStockList]" + bstrStockListData;

    StockList->Items->Add(strData);
    m_nCount++;
    if (StockList->Items->Count < 200)
        StockList->SelectedIndex = listStrikePrices->Items->Count - 1;
    else
        StockList->Items->Clear();

    //Size^ size = TextRenderer->MeasureText(bstrStockListData, StockList->Font);
    //if (StockList->HorizontalExtent < size->Width + 20)
    //    StockList->HorizontalExtent = size->Width + 20;
}

Void CppCLITester::SKQuote::OnUpDateDataRow(SKCOMLib::SKSTOCKLONG pStock)
{

    System::String^  strStockNo = pStock.bstrStockNo;

    DataRow^ drFind = m_dtStocks->Rows->Find(strStockNo);

    if (drFind == nullptr)
    {

        DataRow^  myDataRow = m_dtStocks->NewRow();

        myDataRow["m_nStockidx"] = pStock.nStockIdx;
        myDataRow["m_sDecimal"] = pStock.sDecimal;
        myDataRow["m_sTypeNo"] = pStock.sTypeNo;
        myDataRow["m_cMarketNo"] = pStock.bstrMarketNo;
        myDataRow["m_caStockNo"] = pStock.bstrStockNo;
        myDataRow["m_caName"] = pStock.bstrStockName;
        myDataRow["m_nOpen"] = pStock.nOpen / (Math::Pow(10, pStock.sDecimal));
        myDataRow["m_nHigh"] = pStock.nHigh / (Math::Pow(10, pStock.sDecimal));
        myDataRow["m_nLow"] = pStock.nLow / (Math::Pow(10, pStock.sDecimal));
        myDataRow["m_nClose"] = pStock.nClose / (Math::Pow(10, pStock.sDecimal));
        myDataRow["m_nTickQty"] = pStock.nTickQty;
        myDataRow["m_nRef"] = pStock.nRef / (Math::Pow(10, pStock.sDecimal));

        if (pStock.nBid == kMarketPrice)
            myDataRow["m_nBid"] = "市價";
        else
            myDataRow["m_nBid"] = (pStock.nBid / (Math::Pow(10, pStock.sDecimal))).ToString();


        myDataRow["m_nBc"] = pStock.nBc;

        if (pStock.nAsk == kMarketPrice)
            myDataRow["m_nAsk"] = "市價";
        else
            myDataRow["m_nAsk"] = (pStock.nAsk / (Math::Pow(10, pStock.sDecimal))).ToString();


        m_nSimulateStock = pStock.nSimulate;                 //成交價/買價/賣價;揭示
        myDataRow["m_nAc"] = pStock.nAc;
        myDataRow["m_nTBc"] = pStock.nTBc;
        myDataRow["m_nTAc"] = pStock.nTAc;
        myDataRow["m_nFutureOI"] = pStock.nFutureOI;
        myDataRow["m_nTQty"] = pStock.nTQty;
        myDataRow["m_nYQty"] = pStock.nYQty;
        myDataRow["m_nUp"] = pStock.nUp / (Math::Pow(10, pStock.sDecimal));
        myDataRow["m_nDown"] = pStock.nDown / (Math::Pow(10, pStock.sDecimal));

        m_dtStocks->Rows->Add(myDataRow);
    }
    else
    {
        drFind["m_nStockidx"] = pStock.nStockIdx;
        drFind["m_sDecimal"] = pStock.sDecimal;
        drFind["m_sTypeNo"] = pStock.sTypeNo;
        drFind["m_cMarketNo"] = pStock.bstrMarketNo;
        drFind["m_caStockNo"] = pStock.bstrStockNo;
        drFind["m_caName"] = pStock.bstrStockName;
        drFind["m_nOpen"] = pStock.nOpen / (Math::Pow(10, pStock.sDecimal));
        drFind["m_nHigh"] = pStock.nHigh / (Math::Pow(10, pStock.sDecimal));
        drFind["m_nLow"] = pStock.nLow / (Math::Pow(10, pStock.sDecimal));
        drFind["m_nClose"] = pStock.nClose / (Math::Pow(10, pStock.sDecimal));
        drFind["m_nTickQty"] = pStock.nTickQty;
        drFind["m_nRef"] = pStock.nRef / (Math::Pow(10, pStock.sDecimal));

        if (pStock.nBid == kMarketPrice)
            drFind["m_nBid"] = "市價";
        else
            drFind["m_nBid"] = (pStock.nBid / (Math::Pow(10, pStock.sDecimal))).ToString();


        drFind["m_nBc"] = pStock.nBc;

        if (pStock.nAsk == kMarketPrice)
            drFind["m_nAsk"] = "市價";
        else
            drFind["m_nAsk"] = (pStock.nAsk / (Math::Pow(10, pStock.sDecimal))).ToString();


        drFind["m_nAc"] = pStock.nAc;
        drFind["m_nTBc"] = pStock.nTBc;
        drFind["m_nTAc"] = pStock.nTAc;
        drFind["m_nFutureOI"] = pStock.nFutureOI;
        drFind["m_nTQty"] = pStock.nTQty;
        drFind["m_nYQty"] = pStock.nYQty;
        drFind["m_nUp"] = pStock.nUp / (Math::Pow(10, pStock.sDecimal));
        drFind["m_nDown"] = pStock.nDown / (Math::Pow(10, pStock.sDecimal));
        m_nSimulateStock = pStock.nSimulate;                 //成交價/買價/賣價;揭示
    }
}

System::Void CppCLITester::SKQuote::btnQueryStocks_Click(System::Object^ sender, System::EventArgs^ e)
{
    short page;
    if (short::TryParse(txtPageNo->Text->Trim(), page) == false)
    {
        MessageBox::Show("請輸入數字");
        return;
    }

    
    m_dtStocks->Clear();
    dataGridView1->ClearSelection();

    dataGridView1->DataSource = m_dtStocks;

    dataGridView1->Columns["m_nStockidx"]->Visible = false;
    dataGridView1->Columns["m_sDecimal"]->Visible = false;
    dataGridView1->Columns["m_sTypeNo"]->Visible = false;
    dataGridView1->Columns["m_cMarketNo"]->Visible = false;
    dataGridView1->Columns["m_caStockNo"]->HeaderText = "代碼";
    dataGridView1->Columns["m_caName"]->HeaderText = "名稱";
    dataGridView1->Columns["m_nOpen"]->HeaderText = "開盤價";
    //dataGridView1->Columns["m_nHigh"]->Visible = false;
    dataGridView1->Columns["m_nHigh"]->HeaderText = "最高";
    //dataGridView1->Columns["m_nLow"]->Visible = false;
    dataGridView1->Columns["m_nLow"]->HeaderText = "最低";
    dataGridView1->Columns["m_nClose"]->HeaderText = "成交價";
    dataGridView1->Columns["m_nTickQty"]->HeaderText = "單量";
    dataGridView1->Columns["m_nRef"]->HeaderText = "昨收價";
    dataGridView1->Columns["m_nBid"]->HeaderText = "買價";
    //dataGridView1->Columns["m_nBc"]->Visible = false;
    dataGridView1->Columns["m_nBc"]->HeaderText = "買量";
    dataGridView1->Columns["m_nAsk"]->HeaderText = "賣價";
    //dataGridView1->Columns["m_nAc"]->Visible = false;
    dataGridView1->Columns["m_nAc"]->HeaderText = "賣量";
    //dataGridView1->Columns["m_nTBc"]->Visible = false;
    dataGridView1->Columns["m_nTBc"]->HeaderText = "買盤量";
    //dataGridView1->Columns["m_nTAc"]->Visible = false;
    dataGridView1->Columns["m_nTAc"]->HeaderText = "賣盤量";
    dataGridView1->Columns["m_nFutureOI"]->Visible = false;
    //dataGridView1->Columns["m_nTQty"]->Visible = false;
    dataGridView1->Columns["m_nTQty"]->HeaderText = "總量";
    //dataGridView1->Columns["m_nYQty"]->Visible = false;
    dataGridView1->Columns["m_nYQty"]->HeaderText = "昨量";
    //dataGridView1->Columns["m_nUp"]->Visible = false;
    dataGridView1->Columns["m_nUp"]->HeaderText = "漲停";
    //dataGridView1->Columns["m_nDown"]->Visible = false;
    dataGridView1->Columns["m_nDown"]->HeaderText = "跌停";

    

    array<String^>^ Stocks;
    Stocks = txtStocks->Text->Trim()->Split(',');

    for each (System::String ^ s in Stocks)
    {
        SKCOMLib::SKSTOCKLONG pSKStock;

        int nCode = m_pSKQuote->SKQuoteLib_GetStockByNoLONG(s->Trim(), pSKStock);

        OnUpDateDataRow(pSKStock);

        if (nCode == 0)
        {
            OnUpDateDataRow(pSKStock);
        }
    }

    int n_mCode = m_pSKQuote->SKQuoteLib_RequestStocks(page, txtStocks->Text->Trim());

    txtPageNo->Text = page.ToString();

    GetMessage("Quote", n_mCode, "SKQuoteLib_RequestStocks");


}

Void CppCLITester::SKQuote::btnIsConnected_Click(System::Object^ sender, System::EventArgs^ e)
{
    int n_mCode = m_pSKQuote->SKQuoteLib_IsConnected();


    if (n_mCode == 1) // 連線中
    {
        ConnectedLabel->Text = "true";
        ConnectedLabel->BackColor = Color::Green;
    }
    else if (n_mCode == 2)//下載中
    {
        ConnectedLabel->Text = "False";
        ConnectedLabel->BackColor = Color::Yellow;
    }
    else // 斷線
    {
        ConnectedLabel->Text = "False";
        ConnectedLabel->BackColor = Color::Red;
    }

    GetMessage("Quote",n_mCode,"SKQuoteLib_IsConnected");
}

Void CppCLITester::SKQuote::btnTicks_Click(System::Object^ sender, System::EventArgs^ e)
{
    short sPage;

    if (short::TryParse(txtTickPageNo->Text, sPage) == false)
        return;
    listTicks->Items->Clear();
    m_dBest5Ask->Clear();
    m_dBest5Bid->Clear();
    GridBest5Ask->DataSource = m_dBest5Ask;
    GridBest5Bid->DataSource = m_dBest5Bid;

    GridBest5Ask->Columns["m_nAskQty"]->HeaderText = "張數";
    GridBest5Ask->Columns["m_nAskQty"]->Width = 60;
    GridBest5Ask->Columns["m_nAsk"]->HeaderText = "賣價";
    GridBest5Ask->Columns["m_nAsk"]->Width = 60;

    GridBest5Bid->Columns["m_nAskQty"]->HeaderText = "張數";
    GridBest5Bid->Columns["m_nAskQty"]->Width = 60;
    GridBest5Bid->Columns["m_nAsk"]->HeaderText = "買價";
    GridBest5Bid->Columns["m_nAsk"]->Width = 60;

    int m_nCode = m_pSKQuote->SKQuoteLib_RequestTicks( sPage, txtTick->Text->Trim());

    txtTickPageNo->Text = sPage.ToString();

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestTicks");
}

Void CppCLITester::SKQuote::btnLiveTick_Click(System::Object^ sender, System::EventArgs^ e)
{
    listTicks->Items->Clear();
    short sPage;

    if (short::TryParse(txtTickPageNo->Text,  sPage) == false)
        return;

    int m_nCode = m_pSKQuote->SKQuoteLib_RequestLiveTick( sPage, txtTick->Text->Trim());

    txtTickPageNo->Text = sPage.ToString();

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestLiveTick");
}

Void CppCLITester::SKQuote::btnTickStop_Click(System::Object^ sender, System::EventArgs^ e)
{
    int m_nCode = m_pSKQuote->SKQuoteLib_CancelRequestTicks(txtTick->Text->Trim());

    GetMessage("Quote", m_nCode, "SKQuoteLib_CancelRequestTicks");
}

Void CppCLITester::SKQuote::RequestStockListBtn_Click(System::Object^ sender, System::EventArgs^ e)
{
    StockList->Items->Clear();

    if (MarketNo_txt->Text->Trim() == "")
    {
        MessageBox::Show("請輸入市場代碼");
        return;
    }
    short sMarketNo = Convert::ToInt16(MarketNo_txt->Text->Trim());

    int m_nCode = m_pSKQuote->SKQuoteLib_RequestStockList(sMarketNo);
    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestStockList");
}

Void CppCLITester::SKQuote::GetStrikePrices_Click(System::Object^ sender, System::EventArgs^ e)
{
    listStrikePrices->Items->Clear();

    int m_nCode = m_pSKQuote->SKQuoteLib_GetStrikePrices();

    GetMessage("Quote", m_nCode, "SKQuoteLib_GetStrikePrice");
}

Void CppCLITester::SKQuote::btnKLine_Click(System::Object^ sender, System::EventArgs^ e)
{
    listKLine->Items->Clear();

    short sKLineType = short::Parse(boxKLine->SelectedIndex.ToString());
    short sOutType = short::Parse(boxOutType->SelectedIndex.ToString());

    if (sKLineType < 0)
    {
        MessageBox::Show("請選擇KLine類型");
        return;
    }
    if (sOutType < 0)
    {
        MessageBox::Show("請選擇輸出格式類型");
        return;
    }

    int m_nCode = m_pSKQuote->SKQuoteLib_RequestKLine(txtKLine->Text->Trim(), sKLineType, sOutType);

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestKLine");
}

Void CppCLITester::SKQuote::btnKLineAM_Click(System::Object^ sender, System::EventArgs^ e)
{
    listKLine->Items->Clear();

    short sKLineType = short::Parse(boxKLine->SelectedIndex.ToString());
    short sOutType = short::Parse(boxOutType->SelectedIndex.ToString());
    short sTradeSession = short::Parse(boxTradeSession->SelectedIndex.ToString());

    if (sKLineType < 0)
    {
        MessageBox::Show("請選擇KLine類型");
        return;
    }
    if (sOutType < 0)
    {
        MessageBox::Show("請選擇輸出格式類型");
        return;
    }
    if (sTradeSession < 0)
    {
        MessageBox::Show("請選擇盤別");
        return;
    }

    int m_nCode = m_pSKQuote->SKQuoteLib_RequestKLineAM(txtKLine->Text->Trim(), sKLineType, sOutType, sTradeSession);

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestKLineAM");
}

Void CppCLITester::SKQuote::btnKLineAMByDate_Click(System::Object^ sender, System::EventArgs^ e)
{
    listKLine->Items->Clear();

    short sKLineType = short::Parse(boxKLine->SelectedIndex.ToString());
    short sOutType = short::Parse(boxOutType->SelectedIndex.ToString());
    short sTradeSession = short::Parse(boxTradeSession->SelectedIndex.ToString());
    short sMinuteNumber;

    if (sKLineType < 0)
    {
        MessageBox::Show("請選擇KLine類型");
        return;
    }
    if (sOutType < 0)
    {
        MessageBox::Show("請選擇輸出格式類型");
        return;
    }
    if (sTradeSession < 0)
    {
        MessageBox::Show("請選擇盤別");
        return;
    }
    if (txtStartDate->Text->Trim() == "")
    {
        MessageBox::Show("請輸入開始時間，格式YYYYMMDD");
        return;
    }
    if (txtEndDate->Text->Trim() == "")
    {
        MessageBox::Show("請輸入結束時間，格式YYYYMMDD");
        return;
    }

    if (short::TryParse(txtMinuteNumber->Text, sMinuteNumber) == false)
    {
        sMinuteNumber = 0;
    }
         
   int m_nCode = m_pSKQuote->SKQuoteLib_RequestKLineAMByDate(txtKLine->Text->Trim(), sKLineType, sOutType, sTradeSession, txtStartDate->Text->Trim(), txtEndDate->Text->Trim(), sMinuteNumber);
   // int m_nCode = m_pSKQuote->SKQuoteLib_RequestKLineAMByDate(txtKLine->Text->Trim(), sKLineType, sOutType, sTradeSession, txtStartDate->Text->Trim(), txtEndDate->Text->Trim());

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestKLineAMByDate");
}

Void CppCLITester::SKQuote::Btn_RequestFutureTradeInfo_Click(System::Object^ sender, System::EventArgs^ e)
{
    lblFTIBc->Text = "TotalBc";
    lblFTISc->Text = "TotalSc";
    lblFTIBq->Text = "TotalBq";
    lblFTISq->Text = "TotalSq";
    lblFTIBDC->Text = "TotalBDC";
    lblFTISDC->Text = "TotalSDC";

    short num = 0;

    int m_nCode = m_pSKQuote->SKQuoteLib_RequestFutureTradeInfo(num, text_StockNo->Text->Trim());

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestFutureTradeInfo");
}

Void CppCLITester::SKQuote::btn_cancelFTI_Click(System::Object^ sender, System::EventArgs^ e)
{
    short num = 50;
    int m_nCode = m_pSKQuote->SKQuoteLib_RequestFutureTradeInfo(num, text_StockNo->Text->Trim());

    GetMessage("Quote", m_nCode, "SKQuoteLib_CancelRequestFutureTradeInfo");
}

Void CppCLITester::SKQuote::button4_Click(System::Object^ sender, System::EventArgs^ e)
{
    int m_nCode = m_pSKQuote->SKQuoteLib_GetMarketBuySellUpDown();
    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestMarketBuySellUpDown");
}

Void CppCLITester::SKQuote::btnGetBool_Click(System::Object^ sender, System::EventArgs^ e)
{
    short num = 0;
    int m_nCode = m_pSKQuote->SKQuoteLib_RequestBoolTunel(num, textBool->Text->Trim());
    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestBoolTunel");
}

Void CppCLITester::SKQuote::btnCancelBool_Click(System::Object^ sender, System::EventArgs^ e)
{
    short num = 50;
    int m_nCode = m_pSKQuote->SKQuoteLib_RequestBoolTunel(num, textBool->Text->Trim());
    GetMessage("Quote", m_nCode, "SKQuoteLib_CancelRequstBoolTunel");
}

Void CppCLITester::SKQuote::btnGetMACD_Click(System::Object^ sender, System::EventArgs^ e)
{
    short num = 0;
    int m_nCode = m_pSKQuote->SKQuoteLib_RequestMACD(num, textMACD->Text->Trim());
    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestMACD");
}

Void CppCLITester::SKQuote::btnCancelMACD_Click(System::Object^ sender, System::EventArgs^ e)
{
    short num = 50;
    int m_nCode = m_pSKQuote->SKQuoteLib_RequestMACD(num, textMACD->Text->Trim());
    GetMessage("Quote", m_nCode, "SKQuoteLib_CancelRequstMACD");
}

Void CppCLITester::SKQuote::btnGetTick_Click(System::Object^ sender, System::EventArgs^ e)
{
    short sMarket;
    int nStockidx;
    int nPtr;

    if (short::TryParse(txtTickMarket->Text, sMarket) == false)
        return;

    if (int::TryParse(txtTickStockidx->Text, nStockidx) == false)
        return;

    if (int::TryParse(txtTickPtr->Text,  nPtr) == false)
        return;

    SKCOMLib::SKTICK skHistoryTick;
    int m_nCode = m_pSKQuote->SKQuoteLib_GetTickLONG(sMarket, nStockidx, nPtr, skHistoryTick);
    lblGetTick->Text = "HistoryTick..........................................................";


    if (m_nCode == 0)
    {
        //lblGetTick.Text = skHistoryTick.nTime.ToString() + "/" + skHistoryTick.nBid.ToString() + " " + skHistoryTick.nAsk.ToString() + "/" + skHistoryTick.nClose.ToString() + " ...";
        lblGetTick->Text = skHistoryTick.nTimehms.ToString() + "/" + skHistoryTick.nBid.ToString() + " " + skHistoryTick.nAsk.ToString() + "/" + skHistoryTick.nClose.ToString() + "/" + skHistoryTick.nQty.ToString() + "...";

    }

    GetMessage("Quote", m_nCode, "SKQuoteLib_RequestOneHistoryTick");
}

Void CppCLITester::SKQuote::btnGetBest5_Click(System::Object^ sender, System::EventArgs^ e)
{
    short sMarket;
    int nStockidx;

    if (short::TryParse(txtBestMarket->Text, sMarket) == false)
        return;

    if (int::TryParse(txtBestStockidx->Text, nStockidx) == false)
        return;

    SKCOMLib::SKBEST5 skBest5;

    int  m_nCode = m_pSKQuote->SKQuoteLib_GetBest5LONG(sMarket, nStockidx, skBest5);

    lblBest5Bid->Text = "Best5_Buy..........................................................";
    lblBest5Ask->Text = "Best5_Sell..........................................................";

    if (m_nCode == 0)
    {
        lblBest5Bid->Text = skBest5.nBid1.ToString() + "/" + skBest5.nBidQty1.ToString() + " " + skBest5.nBid2.ToString() + "/" + skBest5.nBidQty2.ToString() + " " + skBest5.nBid3.ToString() + "/" + skBest5.nBidQty3.ToString() + " ...";

        lblBest5Ask->Text = skBest5.nAsk1.ToString() + "/" + skBest5.nAskQty1.ToString() + " " + skBest5.nAsk2.ToString() + "/" + skBest5.nAskQty2.ToString() + " " + skBest5.nAsk3.ToString() + "/" + skBest5.nAskQty3.ToString() + " ...";
    }

    GetMessage("Quote", m_nCode, "SKQuoteLib_GetBest5LONG");
}

