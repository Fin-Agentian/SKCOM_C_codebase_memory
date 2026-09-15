#pragma once

#include "SKCOM_reference.h"
#include "TEventHandler.h"
#include <iostream>

using namespace std;

class CSKReplyLib
{
public :
	typedef TEventHandlerNamespace::TEventHandler<CSKReplyLib, SKCOMLib::ISKReplyLib, SKCOMLib::_ISKReplyLibEvents>ISKReplyLibEventHandler;
	
	CSKReplyLib();
	~CSKReplyLib();

	// Methods
    long SKReplyLib_ConnectByID(string strUserID);
    long SKReplyLib_IsConnectedByID(string strUserID);
    long SKReplyLib_SolaceCloseByID(string strUserID);


    // Event
    void OnReplyMessage(string strMessage, string strUserID, short* sConfirmCode);
    void OnNewData(string strData);
    void OnComplete();
    void OnConnect(string strUserID, long nCode);
    void OnDisconnect(string strUserID, long nCode);

private:
    HRESULT OnEventFiringObjectInvoke
    (
        ISKReplyLibEventHandler* pEventHandler,
        DISPID dispidMember,
        REFIID riid,
        LCID lcid,
        WORD wFlags,
        DISPPARAMS* pdispparams,
        VARIANT* pvarResult,
        EXCEPINFO* pexcepinfo,
        UINT* puArgErr
    );

    SKCOMLib::ISKReplyLibPtr m_pSKReplyLib;
    ISKReplyLibEventHandler* m_pSKReplyLibEventHandler;
};