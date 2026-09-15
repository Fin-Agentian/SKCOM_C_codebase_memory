#include "SKOrderLib.h"
#include "SKCenterLib.h"

#include <vector>
#include <string>
using namespace std;

CSKOrderLib::CSKOrderLib()
{
    m_pSKOrderLib.CreateInstance(__uuidof(SKCOMLib::SKOrderLib));
    m_pSKOrderLibEventHandler = new ISKOrderLibEventHandler(*this, m_pSKOrderLib, &CSKOrderLib::OnEventFiringObjectInvoke);
}

CSKOrderLib::~CSKOrderLib()
{
    if (m_pSKOrderLibEventHandler)
    {
        m_pSKOrderLibEventHandler->ShutdownConnectionPoint();
        m_pSKOrderLibEventHandler->Release();
        m_pSKOrderLibEventHandler = NULL;
    }

    if (m_pSKOrderLib) {
        m_pSKOrderLib->Release();
    }
}

HRESULT CSKOrderLib::OnEventFiringObjectInvoke
(
    ISKOrderLibEventHandler* pEventHandler,
    DISPID dispidMember,
    REFIID riid,
    LCID lcid,
    WORD wFlags,
    DISPPARAMS* pdispparams,
    VARIANT* pvarResult,
    EXCEPINFO* pexcepinfo,
    UINT* puArgErr
)
{
    VARIANT varlValue;
    VariantInit(&varlValue);
    VariantClear(&varlValue);

    switch (dispidMember)
    {
        case 1 :
        {
            varlValue = (pdispparams->rgvarg)[1];
            _bstr_t bstrLoginID = V_BSTR(&varlValue);
            varlValue = (pdispparams->rgvarg)[0];
            _bstr_t bstrData = V_BSTR(&varlValue);
            OnAccount(string(bstrLoginID), string(bstrData));

            break;
        }
        case 2 : 
        {
            varlValue = (pdispparams->rgvarg)[2];
            LONG nThreadID = V_I4(&varlValue);
            varlValue = (pdispparams->rgvarg)[1];
            LONG nCode = V_I4(&varlValue);
            varlValue = (pdispparams->rgvarg)[0];
            _bstr_t bstrMessage = V_BSTR(&varlValue);
            OnAsyncOrder(nThreadID, nCode, string(bstrMessage));

            break;
        }
    }

    return S_OK;
}

//Methods
long CSKOrderLib::Initialize()
{
    return m_pSKOrderLib->SKOrderLib_Initialize();
}

long CSKOrderLib::GetUserAccount()
{
    return m_pSKOrderLib->GetUserAccount();
}

long CSKOrderLib::ReadCertByID(string strLogInID)
{
    return m_pSKOrderLib->ReadCertByID(_bstr_t(strLogInID.c_str()));
}

long CSKOrderLib::SendStockOrder(string strLogInID, bool bAsyncOrder, string strStockNo, short sPrime, short sPeriod, short sFlag, short sBuySell, string strPrice, long nQty, long nTradeType, long nSpecialTradeType)
{
    string strFullAccount_TS = "";

    if (vec_strFullAccount_TS.size() > 0)
        strFullAccount_TS = vec_strFullAccount_TS[0];
    else 
    {
        cout << "SendStockOrder Error : No Stock Account.";
        return -1;
    }

    SKCOMLib::STOCKORDER pOrder;
    pOrder.bstrFullAccount = _bstr_t(strFullAccount_TS.c_str()).Detach();
    pOrder.bstrStockNo = _bstr_t(strStockNo.c_str()).Detach();
    pOrder.sPrime = sPrime;
    pOrder.sPeriod = sPeriod;
    pOrder.sFlag = sFlag;
    pOrder.sBuySell = sBuySell;
    pOrder.bstrPrice = _bstr_t(strPrice.c_str()).Detach();
    pOrder.nQty = nQty;
    pOrder.nTradeType = nTradeType;
    pOrder.nSpecialTradeType = nSpecialTradeType;

    BSTR bstrMessage;
    long m_nCode = m_pSKOrderLib->SendStockOrder(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder), &pOrder, &bstrMessage);

    cout << "SendStockOrder : " << string(_bstr_t(bstrMessage)) << endl;

    ::SysFreeString(bstrMessage);

    return m_nCode;
}

long CSKOrderLib::SendFutureOrder(string strLogInID, bool bAsyncOrder, string strStockNo, short sTradeType, short sBuySell, short sDayTrade, short sNewClose, string strPrice, long nQty, short sReserved)
{
    string strFullAccount_TF = "";

    if (vec_strFullAccount_TF.size() > 0)
        strFullAccount_TF = vec_strFullAccount_TF[0];
    else
    {
        cout << "SendFutureOrder Error : No Future Account.";
        return -1;
    }

    SKCOMLib::FUTUREORDER pFutures;
    pFutures.bstrFullAccount = _bstr_t(strFullAccount_TF.c_str()).Detach();
    pFutures.bstrStockNo = _bstr_t(strStockNo.c_str()).Detach();
    pFutures.sTradeType = sTradeType;
    pFutures.sBuySell = sBuySell;
    pFutures.sDayTrade = sDayTrade;
    pFutures.sNewClose = sNewClose;
    pFutures.bstrPrice = _bstr_t(strPrice.c_str()).Detach();
    pFutures.nQty = nQty;
    pFutures.sReserved = sReserved;

    BSTR bstrMessage;
    long m_nCode = m_pSKOrderLib->SendFutureOrderCLR(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder), &pFutures, &bstrMessage);
    cout << "SendFutureOrder : " << string(_bstr_t(bstrMessage)) << endl;

    ::SysFreeString(bstrMessage);

    return  m_nCode;
}

long CSKOrderLib::SendOptionOrder(string strLogInID, bool bAsyncOrder, string strStockNo, short sTradeType, short sBuySell, short sDayTrade, short sNewClose, string strPrice, long nQty, short sReserved)
{
    string strFullAccount_TF = "";

    if (vec_strFullAccount_TF.size() > 0)
        strFullAccount_TF = vec_strFullAccount_TF[0];
    else
    {
        cout << "SendOptionOrder Error : No Future Account.";
        return -1;
    }

    SKCOMLib::FUTUREORDER pFutures;
    pFutures.bstrFullAccount = _bstr_t(strFullAccount_TF.c_str()).Detach();
    pFutures.bstrStockNo = _bstr_t(strStockNo.c_str()).Detach();
    pFutures.sTradeType = sTradeType;
    pFutures.sBuySell = sBuySell;
    pFutures.sDayTrade = sDayTrade;
    pFutures.sNewClose = sNewClose;
    pFutures.bstrPrice = _bstr_t(strPrice.c_str()).Detach();
    pFutures.nQty = nQty;
    pFutures.sReserved = sReserved;

    BSTR bstrMessage;
    long m_nCode = m_pSKOrderLib->SendOptionOrder(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder), &pFutures, &bstrMessage);
    cout << "SendOptionOrder : " << string(_bstr_t(bstrMessage)) << endl;

    ::SysFreeString(bstrMessage);

    return  m_nCode;
}

long  CSKOrderLib::CancelOrder(string strLogInID, bool bAsyncOrder, int nMarket, int nType, string strNo)
{
    string strFullAccount = "";
    long m_nCode = 0;

    if (nMarket == 0)
    {
        if (vec_strFullAccount_TS.size() > 0)
            strFullAccount = vec_strFullAccount_TS[0];
        else
        {
            cout << "CancelOrder Error : No Stock Account.";
            return -1;
        }
    }
    else if (nMarket == 1 || nMarket == 2)
    {
        if (vec_strFullAccount_TF.size() > 0)
            strFullAccount = vec_strFullAccount_TF[0];
        else
        {
            cout << "CancelOrder Error : No Future Account.";
            return -1;
        }
    }

    BSTR bstrMessage = ::SysAllocString(L"");

    if (nType == 0)
        m_nCode = m_pSKOrderLib->CancelOrderBySeqNo(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder),
            _bstr_t(strFullAccount.c_str()), _bstr_t(strNo.c_str()), &bstrMessage);
    else if (nType == 1)
        m_nCode = m_pSKOrderLib->CancelOrderByBookNo(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder),
            _bstr_t(strFullAccount.c_str()), _bstr_t(strNo.c_str()), &bstrMessage);
    else if (nType == 2)
        m_nCode = m_pSKOrderLib->CancelOrderByStockNo(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder),
            _bstr_t(strFullAccount.c_str()), _bstr_t(strNo.c_str()), &bstrMessage);

    cout << "CancelOrder : " << string(_bstr_t(bstrMessage)) << endl;

    ::SysFreeString(bstrMessage);

    return m_nCode;
}

long CSKOrderLib::CorrectPrice(string strLogInID, bool bAsyncOrder, int nMarket, int nType, string strNo, string strPrice, long nTradeType)
{
    string strFullAccount = "";
    string strMarketSymbol = "";
    long m_nCode = 0;

    if (nMarket == 0)
    {
        if (vec_strFullAccount_TS.size() > 0)
            strFullAccount = vec_strFullAccount_TS[0];
        else
        {
            cout << "CorrectPrice Error : No Stock Account.";
            return -1;
        }

        strMarketSymbol = "TS";
    }
    else if (nMarket == 1 || nMarket == 2)
    {
        if (vec_strFullAccount_TF.size() > 0)
            strFullAccount = vec_strFullAccount_TF[0];
        else
        {
            cout << "CorrectPrice Error : No Future Account.";
            return -1;
        }

        if(nMarket == 1)
            strMarketSymbol = "TF";
        else
            strMarketSymbol = "TO";
    }
    
    BSTR bstrMessage = ::SysAllocString(L"");

    if (nType == 0)
        m_nCode = m_pSKOrderLib->CorrectPriceBySeqNo(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder),
            _bstr_t(strFullAccount.c_str()), _bstr_t(strNo.c_str()), _bstr_t(strPrice.c_str()), nTradeType, &bstrMessage);
    else if (nType == 1)
        m_nCode = m_pSKOrderLib->CorrectPriceByBookNo(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder),
            _bstr_t(strFullAccount.c_str()), _bstr_t(strMarketSymbol.c_str()), _bstr_t(strNo.c_str()), _bstr_t(strPrice.c_str()), nTradeType, &bstrMessage);

    cout << "CorrectPrice : " << string(_bstr_t(bstrMessage)) << endl;

    ::SysFreeString(bstrMessage);

    return m_nCode;
}

long CSKOrderLib::DecreaseOrder(string strLogInID, bool bAsyncOrder, int nMarket, string strNo, long nDecreaseQty)
{
    string strFullAccount = "";
    long m_nCode = 0;

    if (nMarket == 0)
    {
        if (vec_strFullAccount_TS.size() > 0)
            strFullAccount = vec_strFullAccount_TS[0];
        else
        {
            cout << "DecreaseOrder Error : No Stock Account.";
            return -1;
        }
    }
    else if (nMarket == 1 || nMarket == 2)
    {
        if (vec_strFullAccount_TF.size() > 0)
            strFullAccount = vec_strFullAccount_TF[0];
        else
        {
            cout << "DecreaseOrder Error : No Future Account.";
            return -1;
        }
    }

    BSTR bstrMessage;
    m_nCode = m_pSKOrderLib->DecreaseOrderBySeqNo(_bstr_t(strLogInID.c_str()), VARIANT_BOOL(bAsyncOrder),
        _bstr_t(strFullAccount.c_str()), _bstr_t(strNo.c_str()), nDecreaseQty, &bstrMessage);
    cout << "DecreaseOrder : " << string(_bstr_t(bstrMessage)) << endl;

    ::SysFreeString(bstrMessage);

    return m_nCode;
}

// Event
void CSKOrderLib::OnAccount(string strLoginID, string strAccountData)
{
    cout << "¡iOnAccount¡jID=" + strLoginID + "  Account=" + strAccountData << endl;
    vector<string> vec_strValues;
    string strTemp;

    while (true)
    {
        strTemp = strAccountData.find(",") == -1 ? strAccountData : strAccountData.substr(0, strAccountData.find(","));

        vec_strValues.push_back(strTemp);

        if (strAccountData.find(",") == -1)
            break;

        strAccountData = strAccountData.substr(strAccountData.find(",") + 1, strAccountData.length() - 1);
    }

    if (vec_strValues.size() >= 7 && vec_strValues[0] == "TS")
    {
        string strFullAccount_TS = vec_strValues[1] + vec_strValues[3];
        vec_strFullAccount_TS.push_back(strFullAccount_TS);
        printf("¡iOnAccount¡jTSFullAccount=%s\n", strFullAccount_TS.c_str());
    }
    else if (vec_strValues.size() >= 7 && vec_strValues[0] == "TF")
    {
        string strFullAccount_TF = vec_strValues[1] + vec_strValues[3];
        vec_strFullAccount_TF.push_back(strFullAccount_TF);
        printf("¡iOnAccount¡jTFFullAccount=%s\n", strFullAccount_TF.c_str());
    }
    else if (vec_strValues.size() >= 7 && vec_strValues[0] == "OF")
    {
        string strFullAccount_OF = vec_strValues[1] + vec_strValues[3];
        vec_strFullAccount_OF.push_back(strFullAccount_OF);
        printf("¡iOnAccount¡jOFFullAccount=%s\n", strFullAccount_OF.c_str());
    }
    else
    {
    }
}

void CSKOrderLib::OnAsyncOrder(long nThreadID, long nCode, string strMessage)
{
    cout << "¡iOnAsyncOrder¡jThreadID : " << nThreadID << ", nCode : " << nCode << ", Message : " << strMessage;
}