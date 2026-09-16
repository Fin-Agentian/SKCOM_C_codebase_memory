#include "SKReplyLib.h"

CSKReplyLib::CSKReplyLib()
{
	m_pSKReplyLib.CreateInstance(__uuidof(SKCOMLib::SKReplyLib));
	m_pSKReplyLibEventHandler = new ISKReplyLibEventHandler(*this, m_pSKReplyLib, &CSKReplyLib::OnEventFiringObjectInvoke);
}

CSKReplyLib::~CSKReplyLib()
{
	if (m_pSKReplyLibEventHandler)
	{
		m_pSKReplyLibEventHandler->ShutdownConnectionPoint();
		m_pSKReplyLibEventHandler->Release();
		m_pSKReplyLibEventHandler = NULL;
	}

	if (m_pSKReplyLib)
	{
		m_pSKReplyLib->Release();
	}
}

HRESULT CSKReplyLib::OnEventFiringObjectInvoke
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
)
{
	VARIANT varlValue;
	VariantInit(&varlValue);
	VariantClear(&varlValue);

	switch (dispidMember)
	{
		case 1:
		{
			varlValue = (pdispparams->rgvarg)[1];
			_bstr_t bstrLoginID = V_BSTR(&varlValue);
			varlValue = (pdispparams->rgvarg)[0];
			LONG nCode = V_I4(&varlValue);
			OnConnect(string(bstrLoginID), nCode);
			break;
		}
		case 2:
		{
			varlValue = (pdispparams->rgvarg)[1];
			_bstr_t bstrLoginID = V_BSTR(&varlValue);
			varlValue = (pdispparams->rgvarg)[0];
			LONG nCode = V_I4(&varlValue);
			OnDisconnect(string(bstrLoginID), nCode);
			break;
		}
		case 3:
		{
			OnComplete();
			break;
		}
		case 4:
			break;
		case 5:
			break;
		case 6: // Event1 event.
		{
			varlValue = (pdispparams->rgvarg)[2];
			_bstr_t bstrUserID = V_BSTR(&varlValue);
			varlValue = (pdispparams->rgvarg)[1];
			_bstr_t bstrMessage = V_BSTR(&varlValue);
			varlValue = (pdispparams->rgvarg)[0];
			OnReplyMessage(string(bstrMessage), string(bstrUserID), varlValue.piVal);
			break;
		}
		case 7:
			break;
		case 8:
		{
			varlValue = (pdispparams->rgvarg)[0];
			_bstr_t Data = V_BSTR(&varlValue);
			OnNewData(string(Data));
			break;
		}
		case 9:
			break;
		case 10:
			break;
		case 11:
			break;
		case 12:
			break;
		case 13:
			break;
	}

	return S_OK;
}

//Methods
long CSKReplyLib::SKReplyLib_ConnectByID(string strUserID)
{
	return m_pSKReplyLib->SKReplyLib_ConnectByID(_bstr_t(strUserID.c_str()));
}
long CSKReplyLib::SKReplyLib_IsConnectedByID(string strUserID)
{
	return m_pSKReplyLib->SKReplyLib_IsConnectedByID(_bstr_t(strUserID.c_str()));
}
long CSKReplyLib::SKReplyLib_SolaceCloseByID(string strUserID)
{
	return m_pSKReplyLib->SKReplyLib_CloseByID(_bstr_t(strUserID.c_str()));
}

//Event
void CSKReplyLib::OnConnect(string strUserID, long nCode)
{
	cout << "【OnConnect】回報連線：" << strUserID << ", " << nCode << endl;
}
void CSKReplyLib::OnDisconnect(string strUserID, long nCode)
{
	cout << "【OnConnect】回報斷線：" << strUserID << ", " << nCode << endl;
}
void CSKReplyLib::OnComplete()
{
	cout << "【OnConnect】回補完成" << endl;
}

void CSKReplyLib::OnReplyMessage(string strMessage, string strLoginID, short* sConfirmCode)
{
	*sConfirmCode = -1;
	cout << "【OnReplyMessage】Login ID : " << strLoginID << ", Message : " << strMessage << endl;
}

void CSKReplyLib::OnNewData(string strData)
{
	cout << "【OnNewData】" << strData << endl;
}