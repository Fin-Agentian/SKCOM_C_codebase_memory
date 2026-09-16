#include "SKQuoteLib.h"

CSKQuoteLib::CSKQuoteLib()
{
    m_pSKQuoteLib.CreateInstance(__uuidof(SKCOMLib::SKQuoteLib));
    m_pSKQuoteLibEventHandler = new ISKQuoteLibEventHandler(*this, m_pSKQuoteLib, &CSKQuoteLib::OnEventFiringObjectInvoke);
}

CSKQuoteLib::~CSKQuoteLib()
{
    if (m_pSKQuoteLibEventHandler)
	{
        m_pSKQuoteLibEventHandler->ShutdownConnectionPoint();
        m_pSKQuoteLibEventHandler->Release();
        m_pSKQuoteLibEventHandler = NULL;
    }

    if (m_pSKQuoteLib)
	{
        m_pSKQuoteLib->Release();
    }
}

HRESULT CSKQuoteLib::OnEventFiringObjectInvoke
(
	ISKQuoteLibEventHandler* pEventHandler,
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
		case 1: // OnConnection
		{
			long nKind = V_I4(&(pdispparams->rgvarg)[1]);
			long nCode = V_I4(&(pdispparams->rgvarg)[0]);
			OnConnection(nKind, nCode);
			break;
		}
		case 16: // RequestStockList
		{
			short sMarketNo = V_I2(&(pdispparams->rgvarg)[1]);
			_bstr_t bstrStockData = V_BSTR(&(pdispparams->rgvarg)[0]);
			OnNotifyStockList(sMarketNo, string(bstrStockData));
			break;
		}
		case 19: // OnNotifyQuoteLONG
		{
			short sMarketNo = V_I2(&(pdispparams->rgvarg)[1]);
			long nStockIndex = V_I4(&(pdispparams->rgvarg)[0]);
			OnNotifyQuoteLONG(sMarketNo, nStockIndex);
			break;
		}
		case 20: // OnNotifyHistoryTicksLONG
		{
			long nStockIndex = V_I4(&(pdispparams->rgvarg)[9]);
			long nPtr = V_I4(&(pdispparams->rgvarg)[8]);
			long nDate = V_I4(&(pdispparams->rgvarg)[7]);
			long lTimehms = V_I4(&(pdispparams->rgvarg)[6]);
			long nBid = V_I4(&(pdispparams->rgvarg)[4]);
			long nAsk = V_I4(&(pdispparams->rgvarg)[3]);
			long nClose = V_I4(&(pdispparams->rgvarg)[2]);
			long nQty = V_I4(&(pdispparams->rgvarg)[1]);
			OnNotifyHistoryTicksLONG(nStockIndex, nPtr, nDate, lTimehms, nBid, nAsk, nClose, nQty);
			break;
		}
		case 21: //OnNotifyTicksLONG
		{
			long nStockIndex = V_I4(&(pdispparams->rgvarg)[9]);
			long nPtr = V_I4(&(pdispparams->rgvarg)[8]);
			long nDate = V_I4(&(pdispparams->rgvarg)[7]);
			long lTimehms = V_I4(&(pdispparams->rgvarg)[6]);
			long nBid = V_I4(&(pdispparams->rgvarg)[4]);
			long nAsk = V_I4(&(pdispparams->rgvarg)[3]);
			long nClose = V_I4(&(pdispparams->rgvarg)[2]);
			long nQty = V_I4(&(pdispparams->rgvarg)[1]);
			OnNotifyTicksLONG(nStockIndex, nPtr, nDate, lTimehms, nBid, nAsk, nClose, nQty);
			break;
		}
		case 22: // OnNotifyBest5LONG
		{
			//禦基
			long nBestBidQty1 = V_I4(&(pdispparams->rgvarg)[24]);
			long nBestBid1 = V_I4(&(pdispparams->rgvarg)[23]);

			long nBestBidQty2 = V_I4(&(pdispparams->rgvarg)[22]);
			long nBestBid2 = V_I4(&(pdispparams->rgvarg)[21]);

			long nBestBidQty3 = V_I4(&(pdispparams->rgvarg)[20]);
			long nBestBid3 = V_I4(&(pdispparams->rgvarg)[19]);

			long nBestBidQty4 = V_I4(&(pdispparams->rgvarg)[18]);
			long nBestBid4 = V_I4(&(pdispparams->rgvarg)[17]);

			long nBestBidQty5 = V_I4(&(pdispparams->rgvarg)[16]);
			long nBestBid5 = V_I4(&(pdispparams->rgvarg)[15]);

			//芥基
			long nBestAsk1 = V_I4(&(pdispparams->rgvarg)[12]);
			long nBestAskQty1 = V_I4(&(pdispparams->rgvarg)[13]);

			long nBestAsk2 = V_I4(&(pdispparams->rgvarg)[10]);
			long nBestAskQty2 = V_I4(&(pdispparams->rgvarg)[11]);

			long nBestAsk3 = V_I4(&(pdispparams->rgvarg)[8]);
			long nBestAskQty3 = V_I4(&(pdispparams->rgvarg)[9]);

			long nBestAsk4 = V_I4(&(pdispparams->rgvarg)[6]);
			long nBestAskQty4 = V_I4(&(pdispparams->rgvarg)[7]);

			long nBestAsk5 = V_I4(&(pdispparams->rgvarg)[4]);
			long nBestAskQty5 = V_I4(&(pdispparams->rgvarg)[5]);

			OnNotifyBest5LONG(nBestBid1, nBestBidQty1, nBestBid2, nBestBidQty2, nBestBid3, nBestBidQty3, nBestBid4, nBestBidQty4, nBestBid5, nBestBidQty5, nBestAsk1, nBestAskQty1, nBestAsk2, nBestAskQty2, nBestAsk3, nBestAskQty3, nBestAsk4, nBestAskQty4, nBestAsk5, nBestAskQty5);

			break;
		}
	}

    return S_OK;
}

// Methods
long CSKQuoteLib::EnterMonitorLONG()
{
    return m_pSKQuoteLib->SKQuoteLib_EnterMonitorLONG();
}

long CSKQuoteLib::IsConnected()
{
	return m_pSKQuoteLib->SKQuoteLib_IsConnected();
}

long CSKQuoteLib::LeaveMonitor()
{
    return m_pSKQuoteLib->SKQuoteLib_LeaveMonitor();
}

long CSKQuoteLib::RequestStocks(short* psPageNo, string strStockNos)
{
    return m_pSKQuoteLib->SKQuoteLib_RequestStocks(psPageNo, _bstr_t(strStockNos.c_str()));
}

long CSKQuoteLib::GetStockByIndexLONG(short sMarketNo, long nStockIndex, SKCOMLib::SKSTOCKLONG* pSKStock)
{
    return m_pSKQuoteLib->SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIndex, pSKStock);
}

long CSKQuoteLib::RequestTicks(short* psPageNo, string strStockNos)
{
    return m_pSKQuoteLib->SKQuoteLib_RequestTicks(psPageNo, _bstr_t(strStockNos.c_str()));
}

long CSKQuoteLib::RequestStockList(short MarketNo)
{
    return m_pSKQuoteLib->SKQuoteLib_RequestStockList(MarketNo);
}

// Events
void CSKQuoteLib::OnConnection(long nKind, long nCode)
{
    switch(nKind)
	{
		case 3001: //Connected
		{
			cout << endl << "OnConnection硈絬Θ" << endl;
			break;
		}
		case 3002: //Disconnected
		{
			cout << endl << "OnConnection厨基耞絬" << endl;
			break;
		}
		case 3003: // 
		{
			cout << endl << "OnConnection坝珇更ЧΘ" << endl;
			break;
		}
	}
}

void CSKQuoteLib::OnNotifyQuoteLONG(short sMarketNo, long nStockIndex)
{
    SKCOMLib::SKSTOCKLONG skStock;
    long nResult = GetStockByIndexLONG(sMarketNo, nStockIndex, &skStock);
    if (nResult != 0)
        return;

    char* szStockNo = _com_util::ConvertBSTRToString(skStock.bstrStockNo);
    char* szStockName = _com_util::ConvertBSTRToString(skStock.bstrStockName);

    printf("OnNotifyQuoteLONG%s %s bid:%d ask:%d last:%d volume:%d\n",
        szStockNo,
        szStockName,
		skStock.nBid,
		skStock.nAsk,
		skStock.nClose,
		skStock.nTQty
    );

    delete[] szStockName;
    delete[] szStockNo;
}

void CSKQuoteLib::OnNotifyTicksLONG(long nStockIndex, long nPtr, long nDate, long lTimehms, long nBid, long nAsk, long nClose, long nQty)
{
    printf("OnNotifyTicksLONG%ld,%ld,%ld,%ld,%ld,%ld,%ld,%ld\n", nStockIndex, nPtr, nDate, lTimehms, nBid, nAsk, nClose, nQty);
}

void CSKQuoteLib::OnNotifyHistoryTicksLONG(long nStockIndex, long nPtr, long nDate, long lTimehms, long nBid, long nAsk, long nClose, long nQty)
{
    printf("OnNotifyHistoryTicksLONG%ld,%ld,%ld,%ld,%ld,%ld,%ld,%ld\n", nStockIndex, nPtr, nDate, lTimehms, nBid, nAsk, nClose, nQty);
}

void CSKQuoteLib::OnNotifyBest5LONG
(
	long nBestBid1, long nBestBidQty1,
    long nBestBid2, long nBestBidQty2,
    long nBestBid3, long nBestBidQty3,
    long nBestBid4, long nBestBidQty4,
    long nBestBid5, long nBestBidQty5,
    long nBestAsk1, long nBestAskQty1,
    long nBestAsk2, long nBestAskQty2,
    long nBestAsk3, long nBestAskQty3,
    long nBestAsk4, long nBestAskQty4,
    long nBestAsk5, long nBestAskQty5
)
{
	printf("OnNotifyBest5LONG\n");
    printf("禦基1%ld, 计秖1%ld\n", nBestBid1, nBestBidQty1);
    printf("禦基2%ld, 计秖2%ld\n", nBestBid2, nBestBidQty2);
    printf("禦基3%ld, 计秖3%ld\n", nBestBid3, nBestBidQty3);
    printf("禦基4%ld, 计秖4%ld\n", nBestBid4, nBestBidQty4);
    printf("禦基5%ld, 计秖5%ld\n\n", nBestBid5, nBestBidQty5);

    printf("芥基1%ld,计秖1%ld\n", nBestAsk1, nBestAskQty1);
    printf("芥基2%ld,计秖2%ld\n", nBestAsk2, nBestAskQty2);
    printf("芥基3%ld,计秖3%ld\n", nBestAsk3, nBestAskQty3);
    printf("芥基4%ld,计秖4%ld\n", nBestAsk4, nBestAskQty4);
    printf("芥基5%ld,计秖5%ld\n\n", nBestAsk5, nBestAskQty5);
    printf("=================================\n\n");
}

void CSKQuoteLib::OnNotifyStockList(long sMarketNo, string strStockData)
{
    string tempstr = "";
    for (int i = 0; i < strStockData.length(); i++)
    {
        if (strStockData[i] == ';')
        {
            cout << tempstr << endl;
            if (tempstr == "##,,")
            {
                break;
            }
            tempstr = "";
            continue;
        }
        tempstr += strStockData[i];
    }

    cout << endl;
}