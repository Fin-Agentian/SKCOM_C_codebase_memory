#include "FutureOrderControl.h"

System::Void CppCLITester::FutureOrderControl::btnSendFutureOrder_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

    System::String^ strFutureNo;
    int nBidAsk;
    int nPeriod;
    int nFlag;
    System::String^ strPrice;
    int nQty;


    if (txtStockNo->Text->Trim() == "")
    {
        MessageBox::Show("請輸入商品代碼");
        return;
    }
    strFutureNo = txtStockNo->Text->Trim();

    if (boxBidAsk->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇買賣別");
        return;
    }
    nBidAsk = boxBidAsk->SelectedIndex;

    if (boxPeriod->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇委託條件");
        return;
    }
    nPeriod = boxPeriod->SelectedIndex;

    if (boxFlag->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇當沖與否");
        return;
    }
    nFlag = boxFlag->SelectedIndex;

    double dPrice = 0.0;
    //if (double->TryParse(txtPrice->Text->Trim(), out dPrice) == false)
    if (double::TryParse(txtPrice->Text->Trim(), dPrice) == false && txtPrice->Text->Trim() != "M" && txtPrice->Text->Trim() != "P")
    {
        MessageBox::Show("委託價請輸入數字");
        return;
    }
    strPrice = txtPrice->Text->Trim();

    if (int::TryParse(txtQty->Text->Trim(), nQty) == false)
    {
        MessageBox::Show("委託量請輸入數字");
        return;
    }

    SKCOMLib::FUTUREORDER pFutureOrder;

    pFutureOrder.bstrFullAccount = m_UserAccount;
    pFutureOrder.bstrPrice = strPrice;
    pFutureOrder.bstrStockNo = strFutureNo;
    pFutureOrder.nQty = nQty;
    pFutureOrder.sBuySell = (short)nBidAsk;
    pFutureOrder.sDayTrade = (short)nFlag;
    pFutureOrder.sTradeType = (short)nPeriod;

  //  if (OnFutureOrderSignal != null)
   // {
        OnFutureOrderSignal(m_UserID, ckboxAsyn->Checked, pFutureOrder);
   // }
}

System::Void CppCLITester::FutureOrderControl::btnSendFutureOrderCLR_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

    System::String^ strFutureNo;
    int nBidAsk;
    int nPeriod;
    int nFlag;
    System::String^ strPrice;
    int nQty;
    int nNewClose;
    int nReserved;


    if (txtStockNo->Text->Trim() == "")
    {
        MessageBox::Show("請輸入商品代碼");
        return;
    }
    strFutureNo = txtStockNo->Text->Trim();

    if (boxBidAsk->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇買賣別");
        return;
    }
    nBidAsk = boxBidAsk->SelectedIndex;

    if (boxPeriod->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇委託條件");
        return;
    }
    nPeriod = boxPeriod->SelectedIndex;

    if (boxNewClose->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇倉別");
        return;
    }
    nNewClose = boxNewClose->SelectedIndex;

    if (boxFlag->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇當沖與否");
        return;
    }
    nFlag = boxFlag->SelectedIndex;

    double dPrice = 0.0;
    if (double::TryParse(txtPrice->Text->Trim(), dPrice) == false && txtPrice->Text->Trim() != "M" && txtPrice->Text->Trim() != "P")
    {
        MessageBox::Show("委託價請輸入數字");
        return;
    }
    strPrice = txtPrice->Text->Trim();

    if (int::TryParse(txtQty->Text->Trim(), nQty) == false)
    {
        MessageBox::Show("委託量請輸入數字");
        return;
    }

    if (boxReserved->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇盤別");
        return;
    }
    nReserved = boxReserved->SelectedIndex;

    SKCOMLib::FUTUREORDER pFutureOrder;

    pFutureOrder.bstrFullAccount = m_UserAccount;
    pFutureOrder.bstrPrice = strPrice;
    pFutureOrder.bstrStockNo = strFutureNo;
    pFutureOrder.nQty = nQty;
    pFutureOrder.sBuySell = (short)nBidAsk;
    pFutureOrder.sDayTrade = (short)nFlag;
    pFutureOrder.sTradeType = (short)nPeriod;
    pFutureOrder.sNewClose = (short)nNewClose;
    pFutureOrder.sReserved = (short)nReserved;

   // if (OnFutureOrderCLRSignal != null)
    //{
        OnFutureOrderCLRSignal(m_UserID, ckboxAsyn->Checked, pFutureOrder);
    //}
}

System::Void CppCLITester::FutureOrderControl::btnDecreaseQty_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }
    int nQty = 0;
    System::String^ strSeqNo;

    if (txtDecreaseSeqNo->Text->Trim() == "")
    {
        MessageBox::Show("請輸入委託序號");
        return;
    }
    strSeqNo = txtDecreaseSeqNo->Text->Trim();

    if (int::TryParse(txtDecreaseQty->Text->Trim(), nQty) == false)
    {
        MessageBox::Show("改量請輸入數字");
        return;
    }
   // if (OnDecreaseOrderSignal != null)
    //{
        OnDecreaseOrderSignal(m_UserID, true, m_UserAccount, strSeqNo, nQty);
    //}
}

System::Void CppCLITester::FutureOrderControl::btnCancelOrder_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }
    System::String^ strStockNo;

    if (txtCancelStockNo->Text->Trim() == "")
    {
        MessageBox::Show("請輸入委託序號");
        return;
    }
    strStockNo = txtCancelStockNo->Text->Trim();
   // if (OnCancelOrderSignal != null)
    //{
    OnCancelByStockNo(m_UserID, true, m_UserAccount, strStockNo);
    //}
}

System::Void CppCLITester::FutureOrderControl::btnCancelOrderBySeqNo_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

    System::String^ strSeqNo;
        
    if (txtCancelSeqNo->Text->Trim() == "")
    {
        MessageBox::Show("請輸入委託序號");
        return;
    }
    strSeqNo = txtCancelSeqNo->Text->Trim();
   // if (OnCancelOrderSignal != null)
   // {
        OnCancelBySeqNo(m_UserID, true, m_UserAccount, strSeqNo);
    //}
}

System::Void CppCLITester::FutureOrderControl::btnCancelOrderByBookNo_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }
    System::String^ strBookNo;

    if (txtCancelBookNo->Text->Trim() == "")
    {
        MessageBox::Show("請輸入委託序號");
        return;
    }
    strBookNo = txtCancelBookNo->Text->Trim();
    ///if (OnCancelOrderSignal != null)
    //{
        OnCancelByBookNo(m_UserID, true, m_UserAccount, strBookNo);
   // }
}

System::Void CppCLITester::FutureOrderControl::btnCorrectPriceBySeqNo_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }
   
        int nTradeType;
        System::String^  strSeqNo;
        System::String^  strPrice;

        if (txtCorrectSeqNo->Text->Trim() == "")
        {
            MessageBox::Show("請輸入委託序號");
            return;
        }
        strSeqNo = txtCorrectSeqNo->Text->Trim();

        double dPrice = 0.0;
        if (double::TryParse(txtCorrectPrice->Text->Trim(), dPrice) == false)
        {
            MessageBox::Show("修改價格請輸入數字");
            return;
        }
        strPrice = txtCorrectPrice->Text->Trim();

        if (boxCorrectTradeType->SelectedIndex < 0)
        {
            MessageBox::Show("請選擇委託條件");
            return;
        }
        nTradeType = boxCorrectTradeType->SelectedIndex;

        OnCorrectPriceBySeqNo(m_UserID, true, m_UserAccount, strSeqNo, strPrice, nTradeType);

}

System::Void CppCLITester::FutureOrderControl::btnCorrectPriceByBookNo_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

        int nTradeType;
        System::String^ strBookNo;
        System::String^ strPrice;

        if (txtCorrectBookNo->Text->Trim() == "")
        {
            MessageBox::Show("請輸入委託書號");
            return;
        }
        strBookNo = txtCorrectBookNo->Text->Trim();

        double dPrice = 0.0;
        if (double::TryParse(txtCorrectPrice->Text->Trim(), dPrice) == false)
        {
            MessageBox::Show("修改價格請輸入數字");
            return;
        }
        strPrice = txtCorrectPrice->Text->Trim();

        if (boxCorrectSymbol->SelectedIndex < 0)
        {
            MessageBox::Show("請選擇市場簡稱");
            return;
        }
        nTradeType = boxCorrectTradeType->SelectedIndex;

        if (boxCorrectTradeType->SelectedIndex < 0)
        {
            MessageBox::Show("請選擇委託條件");
            return;
        }

        OnCorrectPriceByBookNo(m_UserID, true, m_UserAccount, boxCorrectSymbol->Text->Trim(), strBookNo, strPrice, nTradeType);

}

System::Void CppCLITester::FutureOrderControl::GetOpenInterest_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

    OnGetOpenInterest(m_UserID,m_UserAccount);
}

System::Void CppCLITester::FutureOrderControl::btnGetOpenInterestFormat_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

    int Format;

    if (FormatBox->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇回傳格式");
        return;
    }
    Format = FormatBox->SelectedIndex;

    OnGetOpenInterestWithFormat(m_UserID, m_UserAccount, Format);

}

System::Void CppCLITester::FutureOrderControl::btnGetFutureRights_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");
        return;
    }

    int CoinType;
    if (comBox_CoinType->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇幣別");
        return;
    }
    CoinType = comBox_CoinType->SelectedIndex;

    GetFutureRights(m_UserID,m_UserAccount,CoinType);

}

System::Void CppCLITester::FutureOrderControl::btnSendTXOffset_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_UserAccount == nullptr)
    {
        MessageBox::Show("請選擇期貨帳號");  
        return;
    }

    if (txtOffsetYearMonth->Text->Trim() == "")
    {
        MessageBox::Show("請輸入年月(YYYYMM)");
        return;
    }
    System::String^ YearMonth = txtOffsetYearMonth->Text->Trim();

    if (boxOffsetBuySell->SelectedIndex < 0)
    {
        MessageBox::Show("請選擇買賣別");
        return;
    }
    int BuySell = boxOffsetBuySell->SelectedIndex;
    
    int Qty;
    if (int::TryParse(txtOffsetQty->Text->Trim(), Qty) == false)
    {
        MessageBox::Show("委託量請輸入數字");
        return; 
    }
    
    SendTXOffset(m_UserID,false,m_UserAccount,YearMonth,BuySell,Qty);
}

