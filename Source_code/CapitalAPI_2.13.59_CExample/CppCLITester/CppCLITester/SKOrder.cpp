#include "SKOrder.h"

System::Void CppCLITester::SKOrder::btnInitialize_Click(System::Object^ sender, System::EventArgs^ e)
{
	int nCode = m_pSKOrder->SKOrderLib_Initialize();

    if (clicked)
    {
        clicked = false;
        m_pSKOrder->OnAccount += gcnew SKCOMLib::_ISKOrderLibEvents_OnAccountEventHandler(this, &SKOrder::OnAccount);
        m_pSKOrder->OnRealBalanceReport += gcnew SKCOMLib::_ISKOrderLibEvents_OnRealBalanceReportEventHandler(this, &SKOrder::OnRealBalanceReport);
        m_pSKOrder->OnBalanceQuery += gcnew SKCOMLib::_ISKOrderLibEvents_OnBalanceQueryEventHandler(this, &SKOrder::OnBalanceQuery);
        m_pSKOrder->OnMarginPurchaseAmountLimit += gcnew SKCOMLib::_ISKOrderLibEvents_OnMarginPurchaseAmountLimitEventHandler(this, &SKOrder::OnMarginPurchaseAmountLimit);
        m_pSKOrder->OnRequestProfitReport += gcnew SKCOMLib::_ISKOrderLibEvents_OnRequestProfitReportEventHandler(this, &SKOrder::OnRequestProfitReport);
        m_pSKOrder->OnProfitLossGWReport += gcnew SKCOMLib::_ISKOrderLibEvents_OnProfitLossGWReportEventHandler(this, &SKOrder::OnProfitLossGWReport);
        m_pSKOrder->OnFutureRights += gcnew SKCOMLib::_ISKOrderLibEvents_OnFutureRightsEventHandler(this, &SKOrder::OnFutureRights);
        m_pSKOrder->OnOpenInterest += gcnew SKCOMLib::_ISKOrderLibEvents_OnOpenInterestEventHandler(this, &SKOrder::OnOpenInterest);
        m_pSKOrder->OnAsyncOrder += gcnew SKCOMLib::_ISKOrderLibEvents_OnAsyncOrderEventHandler(this, &SKOrder::OnAsyncOrder);
    }

	GetMessage("Order", nCode, "SKOrderLib_Intialize");
}

#pragma region Click
System::Void CppCLITester::SKOrder::btnGetAccount_Click(System::Object^ sender, System::EventArgs^ e)
{
    this->boxStockAccount->Items->Clear();
    this->boxFutureAccount->Items->Clear();
    this->boxOSFutureAccount->Items->Clear();
    this->boxOSStockAccount->Items->Clear();

    int nCode = m_pSKOrder->GetUserAccount();
    GetMessage("Order", nCode, "GetUserAccount");
}
System::Void CppCLITester::SKOrder::btnReadCert_Click(System::Object^ sender, System::EventArgs^ e)
{
    int m_nCode = m_pSKOrder->ReadCertByID(m_UserID);

    GetMessage("Order", m_nCode, "ReadCertByID");
}
System::Void CppCLITester::SKOrder::OnOrderSignal(System::String^ m_UserID, bool bAsyncOrder, SKCOMLib::STOCKORDER pOrder)
{
    System::String^ strMessage ;

    // 傳入API
    int m_nCode = m_pSKOrder->SendStockOrder(m_UserID, bAsyncOrder, pOrder, strMessage);

    listBox1->Items->Add("證券委託 :" + strMessage);
    GetMessage("Order", m_nCode, "SendStockOrder");
}
System::Void CppCLITester::SKOrder::OnDecreaseSignal(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, int nDecreaseQty)
{
    System::String^ strMessage;

    int m_nCode = m_pSKOrder->DecreaseOrderBySeqNo(bstrLogInID, bAsyncOrder, bstrAccount, bstrSeqNo, nDecreaseQty, strMessage);

    listBox1->Items->Add("減量 :" + strMessage);
    GetMessage("Order", m_nCode, "DecreaseOrderBySeqNo");
}
System::Void CppCLITester::SKOrder::OnCancelByStockNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrStockNo)
{
    System::String^ strMessage;
    int m_nCode = m_pSKOrder->CancelOrderByStockNo(bstrLogInID, bAsyncOrder, bstrAccount, bstrStockNo, strMessage);

    listBox1->Items->Add("商品代號刪單 :" + strMessage);
    GetMessage("Order", m_nCode, "CancelOrderByStockNo");
}
System::Void CppCLITester::SKOrder::OnCancelBySeqNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqkNo)
{
    System::String^ strMessage;

    int m_nCode = m_pSKOrder->CancelOrderBySeqNo(bstrLogInID, bAsyncOrder, bstrAccount, bstrSeqkNo, strMessage);

    listBox1->Items->Add("序號刪單 :" + strMessage);
    GetMessage("Order", m_nCode, "CancelOrderBySeqNo");
}
System::Void CppCLITester::SKOrder::OnCancelByBookNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrBookNo)
{
    System::String^ strMessage;

    int m_nCode = m_pSKOrder->CancelOrderByBookNo(bstrLogInID, bAsyncOrder, bstrAccount, bstrBookNo, strMessage);
    
    listBox1->Items->Add("書號刪單 :" + strMessage);
    GetMessage("Order", m_nCode, "CancelOrderByBookNo");
}
System::Void CppCLITester::SKOrder::OnCorrectPriceBySeqNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, System::String^ bstrPrice, int nTradeType)
{
    System::String^ strMessage;
    
    int m_nCode = m_pSKOrder->CorrectPriceBySeqNo(bstrLogInID, bAsyncOrder, bstrAccount, bstrSeqNo, bstrPrice, nTradeType,strMessage);

    listBox1->Items->Add("序號改價 :" + strMessage);
    GetMessage("Order", m_nCode, "OnCorrectPriceBySeqNo");
}
System::Void CppCLITester::SKOrder::OnCorrectPriceByBookNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount,System::String ^bstrMarketSymbol, System::String^ bstrBookNo, System::String^ bstrPrice, int nTradeType)
{
    System::String^ strMessage;
    
    int m_nCode = m_pSKOrder->CorrectPriceByBookNo(bstrLogInID, bAsyncOrder, bstrAccount, bstrMarketSymbol, bstrBookNo, bstrPrice, nTradeType, strMessage);
    
    listBox1->Items->Add("書號改價 :" + strMessage);
    GetMessage("Order",m_nCode,"OnCorrectPriceByBookNo");
}
System::Void CppCLITester::SKOrder::OnGetRealBalanceReport(System::String^ bstrLogInID, System::String^ bstrAccount)
{
    int m_nCode = m_pSKOrder->GetRealBalanceReport(bstrLogInID, bstrAccount);

    GetMessage("Order", m_nCode, "OnGetRealBalanceReport");
}
System::Void CppCLITester::SKOrder::OnGetBalanceQuery(System::String^ bstrLogInID, System::String^ bstrAccount, System::String^ StockNo)
{
    int m_nCode = m_pSKOrder->GetBalanceQuery(bstrLogInID, bstrAccount, StockNo);

    GetMessage("Order", m_nCode, "GetBalanceQuery");
}
System::Void CppCLITester::SKOrder::OnGetMarginPurchaseAmountLimit(System::String^ bstrLogInID, System::String^ bstrAccount, System::String^ StockNo)
{
    int m_nCode = m_pSKOrder->GetMarginPurchaseAmountLimit(bstrLogInID, bstrAccount, StockNo);

    GetMessage("Order", m_nCode, "GetMarginPurchaseAmountLimit");
}
System::Void CppCLITester::SKOrder::OnGetRequestProfitReport(System::String^ bstrLogInID, System::String^ bstrAccount)
{
    int m_nCode = m_pSKOrder->GetRequestProfitReport(bstrLogInID, bstrAccount);

    GetMessage("Order", m_nCode, "GetRequestProfitReport");
}
System::Void CppCLITester::SKOrder::OnProfitGWReportSignal(System::String^ bstrLogInID, SKCOMLib::TSPROFITLOSSGWQUERY pPLGWQuery)
{
    int m_nCode = m_pSKOrder->GetProfitLossGWReport(bstrLogInID, pPLGWQuery);

    GetMessage("Order", m_nCode, "GetProfitLossGWReport");
}
System::Void CppCLITester::SKOrder::OnOddOrderSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::STOCKORDER pOrder)
{
    System::String^ strMessage;
    int m_nCode = m_pSKOrder->SendStockOddLotOrder(bstrLogInID, bAsyncOrder, pOrder,strMessage);

    listBox1->Items->Add("零股委託 :" + strMessage);
    GetMessage("Order", m_nCode, "SendStockOddLotOrder");
}
System::Void CppCLITester::SKOrder::OnGetOpenInterest(System::String^ bstrLogInID, System::String^ bstrAccount)
{
    int m_nCode = m_pSKOrder->GetOpenInterest(bstrLogInID, bstrAccount);

    GetMessage("Order", m_nCode, "GetOpenInterest");
}
System::Void CppCLITester::SKOrder::OnGetOpenInterestWithFormat(System::String^ bstrLogInID, System::String^ bstrAccount, int nFormat)
{
    int m_nCode = m_pSKOrder->GetOpenInterestWithFormat(bstrLogInID, bstrAccount, nFormat);

    GetMessage("Order", m_nCode, "GetOpenInterestWithFormat");
}
System::Void CppCLITester::SKOrder::GetFutureRights(System::String^ bstrLogInID, System::String^ bstrAccount, int CoinType)
{
    int m_nCode = m_pSKOrder->GetFutureRights(bstrLogInID, bstrAccount, CoinType);

    GetMessage("Order", m_nCode, "GetFutureRights");
}
System::Void CppCLITester::SKOrder::SendTXOffset(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrYearMonth, int nBuySell, int nQty)
{
     System::String^ strMessage;

    int m_nCode = m_pSKOrder->SendTXOffset(bstrLogInID, bAsyncOrder, bstrAccount, bstrYearMonth, nBuySell, nQty, strMessage);

    listBox1->Items->Add("SendTXOffset：" + strMessage);
    GetMessage("Order", m_nCode, "SendTXOffset");
}
System::Void CppCLITester::SKOrder::OnOptionOrderSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pOrder)
{
    System::String^  strMessage = "";
    int m_nCode = m_pSKOrder->SendOptionOrder(bstrLogInID, bAsyncOrder, pOrder,strMessage);

    listBox1->Items->Add("選擇權委託：" + strMessage);
    GetMessage("Order", m_nCode, "SendOptionOrder");
}
System::Void CppCLITester::SKOrder::OnFutureOrderSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pOrder)
{
    System::String^ strMessage; 

    int m_nCode = m_pSKOrder->SendFutureOrder(bstrLogInID, bAsyncOrder, pOrder,strMessage);

    listBox1->Items->Add("期貨委託：" + strMessage);
    GetMessage("Order", m_nCode, "SendFutureOrder");
}
System::Void CppCLITester::SKOrder::OnFutureOrderCLRSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pAsyncOrder)
{
    System::String^ strMessage;

    int m_nCode = m_pSKOrder->SendFutureOrderCLR(bstrLogInID, bAsyncOrder, pAsyncOrder, strMessage);

    listBox1->Items->Add("期貨委託(含倉盤)：" + strMessage);
    GetMessage("Order", m_nCode, "SendFutureOrderCLR");
}
#pragma endregion

#pragma region event
System::Void CppCLITester::SKOrder::OnAccount(System::String^ bstrLoginID, System::String^ bstrAccountData)
{
    array<String^>^ strValues = bstrAccountData->Split(',');

    String^ strAccount = bstrLoginID + " " + strValues[1] + strValues[3];

    if (strValues[0] == "TS")
    {
        this->boxStockAccount->Items->Add(strAccount);
        this->boxStockAccount->SelectedIndex = 0;

        stockOrderControl1->set_UserID(bstrLoginID);
        stockOrderControl1->set_UserAccount(strValues[1] + strValues[3]);
    }
    else if (strValues[0] == "TF")
    {
        this->boxFutureAccount->Items->Add(strAccount);
        this->boxFutureAccount->SelectedIndex = 0;

        futureOrderControl1->set_UserID(bstrLoginID);
        futureOrderControl1->set_UserAccount(strValues[1] + strValues[3]);

        optionOrderControl1->get_UserID(bstrLoginID);
        optionOrderControl1->get_UserAccount(strValues[1] + strValues[3]);
    }
    else if (strValues[0] == "OF")
    {
        this->boxOSFutureAccount->Items->Add(strAccount);
        this->boxOSFutureAccount->SelectedIndex = 0;
    }
    else if (strValues[0] == "OS")
    {
        this->boxOSStockAccount->Items->Add(strAccount);
        this->boxOSStockAccount->SelectedIndex = 0;
    }
}

System::Void CppCLITester::SKOrder::OnRealBalanceReport(System::String^ bstrMessage)
{
    listBox1->Items->Add("[OnRealBalanceReport]" + bstrMessage);
}
System::Void CppCLITester::SKOrder::OnBalanceQuery(System::String^ strMessage)
{
    listBox1->Items->Add("[OnBalanceQuery]" + strMessage);
}
System::Void CppCLITester::SKOrder::OnMarginPurchaseAmountLimit(System::String^ strMessage)
{
    listBox1->Items->Add("[OnMarginPurchaseAmountLimit]" + strMessage);
}
System::Void CppCLITester::SKOrder::OnRequestProfitReport(System::String^ strMessage)
{
    listBox1->Items->Add("[OnRequestProfitReport]" + strMessage);
}
System::Void CppCLITester::SKOrder::OnProfitLossGWReport(System::String^ strMessage)
{
    listBox1->Items->Add("[OnProfitLossGWReport]" + strMessage);
}
System::Void CppCLITester::SKOrder::OnFutureRights(System::String^ strMessage)
{
    listBox1->Items->Add("[OnFutureRights]" + strMessage);
}
System::Void CppCLITester::SKOrder::OnOpenInterest(System::String^ strMessage)
{
    listBox1->Items->Add("[OnOpenInterest]" + strMessage);
}
System::Void CppCLITester::SKOrder::OnAsyncOrder(int nThreadID, int nCode, System::String^ strMessage)
{
    listBox1->Items->Add("[OnAsyncOrder]Thread ID:" + nThreadID.ToString() + " Code:" + nCode.ToString() + " Message:" + strMessage);
}
#pragma endregion

#pragma region else
System::Void CppCLITester::SKOrder::boxStockAccount_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e)
{
    System::String^ strInfo = boxFutureAccount->Text;

    array<System::String^>^ strValues;
    strValues = strInfo->Split(' ');

    //futureOrderControl1->UserID = strValues[0];
    //futureOrderControl1->UserAccount = strValues[1];

    stockOrderControl1->set_UserID(strValues[0]);
    stockOrderControl1->set_UserAccount(strValues[1]);
}
#pragma endregion





