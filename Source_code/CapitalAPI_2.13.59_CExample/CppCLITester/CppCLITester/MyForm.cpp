#include "MyForm.h"

using namespace System;
using namespace System::Windows::Forms;

[STAThreadAttribute]
void  Main() {		//array<String^>^ args

	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	CppCLITester::MyForm form;
	Application::Run(% form);


}

//methods
System::Void CppCLITester::MyForm::LogIn_Click(System::Object^ sender, System::EventArgs^ e)
{
	String^ Account = this->txtAccount->Text->Trim()->ToUpper();
	String^ Password = this->txtPassword->Text->Trim();

	int nCode = m_pSKCenter->SKCenterLib_Login(Account, Password);

	//GetMessage("Center", nCode, "LogIn_Click");

	if (nCode == 0)
	{
		WriteMessage(Account + " LogIn success");
		skOrder1->get_UserID(Account);
		
	}

	skReply1->get_UserID(Account);
}

//event
void CppCLITester::MyForm::OnAnnouncement(System::String^ bstrUserID, System::String^ bstrMessage, short% sConfirmCode)
{
	sConfirmCode = -1;
}

//custom
void CppCLITester::MyForm::GetMessage(String^ strType, int nCode, String^ strMessage)
{
	System::String^ tempstr;

	tempstr = "¡i" + strType + "¡j¡i" + strMessage + "¡j¡i" + m_pSKCenter->SKCenterLib_GetReturnCodeMessage(nCode) + "¡j";

	if (nCode != 0)
	{
		tempstr += m_pSKCenter->SKCenterLib_GetLastLogInfo();
	}

	this->listBox1->Items->Add(tempstr);
	
}

void CppCLITester::MyForm::WriteMessage(String^ strMessage)
{
	this->listBox1->Items->Add(strMessage);
}
