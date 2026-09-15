#pragma once

#include "SKCOM_reference.h"
#include "TEventHandler.h"

class CSKQuoteLib
{
public:
    typedef TEventHandlerNamespace::TEventHandler<CSKQuoteLib, SKCOMLib::ISKQuoteLib, SKCOMLib::_ISKQuoteLibEvents> ISKQuoteLibEventHandler;

    CSKQuoteLib();
    ~CSKQuoteLib();

    // Methods
    long EnterMonitorLONG();
    long IsConnected();
    long LeaveMonitor();
    long RequestStocks(short* psPageNo, string strStockNos);
    long RequestTicks(short* psPageNo, string strStockNos);
    long RequestStockList(short MarketNo);
    long GetStockByIndexLONG(short sMarketNo, long bStockIndex, SKCOMLib::SKSTOCKLONG* pSKStock);

    // Events
    void OnConnection(long nKind, long nCode);
    void OnNotifyQuoteLONG(short sMarketNo, long nStockIndex);
    void OnNotifyTicksLONG(long nStockIndex, long nPtr, long nDate, long lTimehms, long nBid, long nAsk, long nClose, long nQty);
    void OnNotifyHistoryTicksLONG(long nStockIndex, long nPtr, long nDate, long lTimehms, long nBid, long nAsk, long nClose, long nQty);
    void OnNotifyBest5LONG(long nBestBid1, long nBestBidQty1, long nBestBid2, long nBestBidQty2, long nBestBid3, long nBestBidQty3, long nBestBid4, long nBestBidQty4, long nBestBid5, long nBestBidQty5, long nBestAsk1, long nBestAskQty1, long nBestAsk2, long nBestAskQty2, long nBestAsk3, long nBestAskQty3, long nBestAsk4, long nBestAskQty4, long nBestAsk5, long nBestAskQty5);
    void OnNotifyStockList(long sMarketNo, string strStockData);

private:
    HRESULT OnEventFiringObjectInvoke
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
    );

    SKCOMLib::ISKQuoteLibPtr m_pSKQuoteLib;
    ISKQuoteLibEventHandler* m_pSKQuoteLibEventHandler;
};