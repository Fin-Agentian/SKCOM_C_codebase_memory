#pragma once

#include "SKCOM_reference.h"
#include "TEventHandler.h"
#include <iostream>
#include <string>

using namespace std;
 
class CSKCenterLib
{
public:
    typedef TEventHandlerNamespace::TEventHandler<CSKCenterLib, SKCOMLib::ISKCenterLib, SKCOMLib::_ISKCenterLibEvents> ISKCenterLibEventHandler;

    CSKCenterLib();
    ~CSKCenterLib();

    // Methods
    long Login(const char* szUserID, const char* szPassword);
    
    _bstr_t GetReturnCodeMessage(long nCode);
    _bstr_t GetLastLogInfo();

    // Customize
    void PrintfCodeMessage(string Features, string FunctionName, long nCode);

    // Events
    void OnTimer(LONG nTime);

private:
    HRESULT OnEventFiringObjectInvoke(
        ISKCenterLibEventHandler* pEventHandler,
        DISPID dispidMember,
        REFIID riid,
        LCID lcid,
        WORD wFlags,
        DISPPARAMS* pdispparams,
        VARIANT* pvarResult,
        EXCEPINFO* pexcepinfo,
        UINT* puArgErr
    );

    SKCOMLib::ISKCenterLibPtr m_pSKCenterLib;
    ISKCenterLibEventHandler* m_pSKCenterLibEventHandler;
};
