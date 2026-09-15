#pragma once

#include "SKCOM_reference.h"
#include "TEventHandler.h"
#include <vector>
#include <string>
#include <iostream>

using namespace std;

//void OrderType();

class CSKOrderLib
{
public:
	typedef TEventHandlerNamespace::TEventHandler<CSKOrderLib, SKCOMLib::ISKOrderLib, SKCOMLib::_ISKOrderLibEvents>ISKOrderLibEventHandler;

	CSKOrderLib();
	~CSKOrderLib();

	// Methods
	long Initialize();
	long GetUserAccount();
	long ReadCertByID(string strLogInID);
	long SendStockOrder(string strLogInID, bool bAsyncOrder, string strStockNo, short sPrime, short sPeriod, short sFlag, short sBuySell, string strPrice, long nQty, long nTradeType, long nSpecialTradeType);
	long SendFutureOrder(string strLogInID, bool bAsyncOrder, string strStockNo, short sTradeType, short sBuySell, short sDayTrade, short sNewClose, string strPrice, long nQty, short sReserved);
	long SendOptionOrder(string strLogInID, bool bAsyncOrder, string strStockNo, short sTradeType, short sBuySell, short sDayTrade, short sNewClose, string strPrice, long nQty, short sReserved);

	long DecreaseOrder(string strLogInID, bool bAsyncOrder, int nMarket, string strNo, long nDecreaseQty);
	long CorrectPrice(string strLogInID, bool bAsyncOrder, int nMarket, int nType, string strNo, string strPrice, long nTradeType);
	long CancelOrder(string strLogInID, bool bAsyncOrder, int nMarket, int nType, string strNo);
	// Event
	void OnAccount(string strLoginID, string strAccountData);
	void OnAsyncOrder(long nThreadID, long nCode, string strMessage);

private:
	HRESULT OnEventFiringObjectInvoke
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
	);

	SKCOMLib::ISKOrderLibPtr m_pSKOrderLib;
	ISKOrderLibEventHandler* m_pSKOrderLibEventHandler;

	vector<string> vec_strFullAccount_TS;
	vector<string> vec_strFullAccount_TF;
	vector<string> vec_strFullAccount_OF;
};


