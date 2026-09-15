#include "SKCenterLib.h"
#include <iostream>
#include <string>
#include <vector>
#include <typeinfo>
using namespace std;

vector<string>UserAccount;

CSKCenterLib::CSKCenterLib()
{
    m_pSKCenterLib.CreateInstance(__uuidof(SKCOMLib::SKCenterLib));
    m_pSKCenterLibEventHandler = new ISKCenterLibEventHandler(*this, m_pSKCenterLib, &CSKCenterLib::OnEventFiringObjectInvoke);
}

CSKCenterLib::~CSKCenterLib()
{
    if (m_pSKCenterLibEventHandler)
    {
        m_pSKCenterLibEventHandler->ShutdownConnectionPoint();
        m_pSKCenterLibEventHandler->Release();
        m_pSKCenterLibEventHandler = NULL;
    }

    if (m_pSKCenterLib)
    {
        m_pSKCenterLib->Release();
    }
}

HRESULT CSKCenterLib::OnEventFiringObjectInvoke
(
    ISKCenterLibEventHandler* pEventHandler,
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
        case 1: // Event1 event.
            varlValue = (pdispparams->rgvarg)[0];
            OnTimer(V_I4(&varlValue));
            break;
    }

    return S_OK;
}

// Methods
long CSKCenterLib::Login(const char* szUserID, const char* szPassword)
{
    return m_pSKCenterLib->SKCenterLib_Login(szUserID, szPassword);
}

_bstr_t CSKCenterLib::GetReturnCodeMessage(long nCode)
{
    return m_pSKCenterLib->SKCenterLib_GetReturnCodeMessage(nCode);
}

_bstr_t CSKCenterLib::GetLastLogInfo()
{
    return m_pSKCenterLib->SKCenterLib_GetLastLogInfo();
}

void CSKCenterLib::PrintfCodeMessage(string Features, string FunctionName, long nCode)
{
    if(nCode == 0)
        printf("【%s】【%s】【%s】\n", Features.c_str(), FunctionName.c_str(), (char*)m_pSKCenterLib->SKCenterLib_GetReturnCodeMessage(nCode));
    else
        printf("【%s】【%s】【%s】【%s】\n", Features.c_str(), FunctionName.c_str(),  (char*)m_pSKCenterLib->SKCenterLib_GetReturnCodeMessage(nCode) , (char*)m_pSKCenterLib->SKCenterLib_GetLastLogInfo());
}

// Events
void CSKCenterLib::OnTimer(LONG nTime)
{
    cout << endl << "Now Time : " << nTime << endl;
}