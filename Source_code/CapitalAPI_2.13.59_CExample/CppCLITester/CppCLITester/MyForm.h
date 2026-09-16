#pragma once
#include "SKOrder.h"
#include "SKReply.h"
#include "SKQuote.h"

namespace CppCLITester {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	//using namespace SKCOMLib;


	/// <summary>
	/// MyForm 的摘要
	/// </summary> 
	public ref class MyForm : public System::Windows::Forms::Form
	{

#pragma region 
	private:

		//SKCOM 物件宣告
		SKCOMLib::SKReplyLibClass^ m_pSKReply = gcnew SKCOMLib::SKReplyLibClass;
		SKCOMLib::SKOrderLibClass^ m_pSKOrder = gcnew SKCOMLib::SKOrderLibClass;
		SKCOMLib::SKCenterLibClass^ m_pSKCenter = gcnew SKCOMLib::SKCenterLibClass;
		SKCOMLib::SKQuoteLibClass^ m_pSKQuote = gcnew SKCOMLib::SKQuoteLibClass;

	private:
		CppCLITester::SKReply^ skReply1;
		CppCLITester::SKQuote^ skQuote1;
		CppCLITester::SKOrder^ skOrder1;
#pragma endregion

#pragma region Methods
		//methods
		Void LogIn_Click(System::Object^ sender, System::EventArgs^ e);

		//event
		void OnAnnouncement(System::String^ bstrUserID, System::String^ bstrMessage, short% sConfirmCode);

		//custom
		void GetMessage(String^ strType, int nCode, String^ strMessage);
		void WriteMessage(String^ strMessage);
		

#pragma endregion


	public:
		MyForm(void)
		{
			InitializeComponent();
			skOrder1->set_data(m_pSKOrder);
			skReply1->get_SKReplyObj(m_pSKReply);
			skQuote1->get_SKQuoteObj(m_pSKQuote);
			m_pSKReply->OnReplyMessage += gcnew SKCOMLib::_ISKReplyLibEvents_OnReplyMessageEventHandler(this, &MyForm::OnAnnouncement);
		}

	protected:
		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}

	private:
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>


		System::ComponentModel::Container^ components;

	private: System::Windows::Forms::Button^ LogIn;
	private: System::Windows::Forms::TextBox^ txtAccount;
	private: System::Windows::Forms::ListBox^ listBox1;
	private: System::Windows::Forms::TextBox^ txtPassword;
	private: System::Windows::Forms::Label^ lblPassword;
	private: System::Windows::Forms::Label^ lblAccount;
	private: System::Windows::Forms::TabControl^ tabControl1;
	private: System::Windows::Forms::TabPage^ tabPage1;
	private: System::Windows::Forms::TabPage^ tabPage2;
	private: System::Windows::Forms::TabPage^ tabPage3;
	private: System::Windows::Forms::TabPage^ tabPage4;


#pragma region Windows Form Designer generated code
		   /// <summary>
		   /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		   /// 這個方法的內容。
		   /// </summary>
		   void InitializeComponent(void)
		   {
			   this->LogIn = (gcnew System::Windows::Forms::Button());
			   this->lblAccount = (gcnew System::Windows::Forms::Label());
			   this->txtAccount = (gcnew System::Windows::Forms::TextBox());
			   this->listBox1 = (gcnew System::Windows::Forms::ListBox());
			   this->txtPassword = (gcnew System::Windows::Forms::TextBox());
			   this->lblPassword = (gcnew System::Windows::Forms::Label());
			   this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			   this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
			   this->skOrder1 = (gcnew CppCLITester::SKOrder());
			   this->tabPage2 = (gcnew System::Windows::Forms::TabPage());
			   this->skReply1 = (gcnew CppCLITester::SKReply());
			   this->tabPage3 = (gcnew System::Windows::Forms::TabPage());
			   this->skQuote1 = (gcnew CppCLITester::SKQuote());
			   this->tabPage4 = (gcnew System::Windows::Forms::TabPage());
			   this->tabControl1->SuspendLayout();
			   this->tabPage1->SuspendLayout();
			   this->tabPage2->SuspendLayout();
			   this->tabPage3->SuspendLayout();
			   this->tabPage4->SuspendLayout();
			   this->SuspendLayout();
			   // 
			   // LogIn
			   // 
			   this->LogIn->Location = System::Drawing::Point(234, 84);
			   this->LogIn->Name = L"LogIn";
			   this->LogIn->Size = System::Drawing::Size(75, 23);
			   this->LogIn->TabIndex = 2;
			   this->LogIn->Text = L"Log In";
			   this->LogIn->UseVisualStyleBackColor = true;
			   this->LogIn->Click += gcnew System::EventHandler(this, &MyForm::LogIn_Click);
			   // 
			   // lblAccount
			   // 
			   this->lblAccount->AutoSize = true;
			   this->lblAccount->Location = System::Drawing::Point(12, 26);
			   this->lblAccount->Name = L"lblAccount";
			   this->lblAccount->Size = System::Drawing::Size(29, 12);
			   this->lblAccount->TabIndex = 3;
			   this->lblAccount->Text = L"帳號";
			   // 
			   // txtAccount
			   // 
			   this->txtAccount->Location = System::Drawing::Point(83, 23);
			   this->txtAccount->Name = L"txtAccount";
			   this->txtAccount->Size = System::Drawing::Size(226, 22);
			   this->txtAccount->TabIndex = 0;
			   // 
			   // listBox1
			   // 
			   this->listBox1->FormattingEnabled = true;
			   this->listBox1->HorizontalScrollbar = true;
			   this->listBox1->ItemHeight = 12;
			   this->listBox1->Location = System::Drawing::Point(330, 19);
			   this->listBox1->Name = L"listBox1";
			   this->listBox1->Size = System::Drawing::Size(645, 88);
			   this->listBox1->TabIndex = 5;
			   // 
			   // txtPassword
			   // 
			   this->txtPassword->Location = System::Drawing::Point(83, 56);
			   this->txtPassword->Name = L"txtPassword";
			   this->txtPassword->PasswordChar = '*';
			   this->txtPassword->Size = System::Drawing::Size(226, 22);
			   this->txtPassword->TabIndex = 1;
			   // 
			   // lblPassword
			   // 
			   this->lblPassword->AutoSize = true;
			   this->lblPassword->Location = System::Drawing::Point(12, 59);
			   this->lblPassword->Name = L"lblPassword";
			   this->lblPassword->Size = System::Drawing::Size(29, 12);
			   this->lblPassword->TabIndex = 4;
			   this->lblPassword->Text = L"密碼";
			   // 
			   // tabControl1
			   // 
			   this->tabControl1->Controls->Add(this->tabPage1);
			   this->tabControl1->Controls->Add(this->tabPage2);
			   this->tabControl1->Controls->Add(this->tabPage3);
			   this->tabControl1->Controls->Add(this->tabPage4);
			   this->tabControl1->Location = System::Drawing::Point(14, 113);
			   this->tabControl1->Name = L"tabControl1";
			   this->tabControl1->SelectedIndex = 0;
			   this->tabControl1->Size = System::Drawing::Size(956, 847);
			   this->tabControl1->TabIndex = 6;
			   // 
			   // tabPage1
			   // 
			   this->tabPage1->Controls->Add(this->skOrder1);
			   this->tabPage1->Location = System::Drawing::Point(4, 22);
			   this->tabPage1->Name = L"tabPage1";
			   this->tabPage1->Padding = System::Windows::Forms::Padding(3);
			   this->tabPage1->Size = System::Drawing::Size(948, 821);
			   this->tabPage1->TabIndex = 0;
			   this->tabPage1->Text = L"下單";
			   this->tabPage1->UseVisualStyleBackColor = true;
			   // 
			   // skOrder1
			   // 
			   this->skOrder1->Location = System::Drawing::Point(0, 0);
			   this->skOrder1->Name = L"skOrder1";
			   this->skOrder1->Size = System::Drawing::Size(953, 813);
			   this->skOrder1->TabIndex = 0;
			   this->skOrder1->GetMessage += gcnew CppCLITester::SKOrder::GetMessageHandler(this, &CppCLITester::MyForm::GetMessage);
			   this->skOrder1->WriteMessage += gcnew CppCLITester::SKOrder::WriteMessageHandler(this, &CppCLITester::MyForm::WriteMessage);
			   // 
			   // tabPage2
			   // 
			   this->tabPage2->Controls->Add(this->skReply1);
			   this->tabPage2->Location = System::Drawing::Point(4, 22);
			   this->tabPage2->Name = L"tabPage2";
			   this->tabPage2->Padding = System::Windows::Forms::Padding(3);
			   this->tabPage2->Size = System::Drawing::Size(192, 74);
			   this->tabPage2->TabIndex = 1;
			   this->tabPage2->Text = L"回報";
			   this->tabPage2->UseVisualStyleBackColor = true;
			   // 
			   // skReply1
			   // 
			   this->skReply1->Location = System::Drawing::Point(0, 0);
			   this->skReply1->Name = L"skReply1";
			   this->skReply1->Size = System::Drawing::Size(953, 705);
			   this->skReply1->TabIndex = 0;
			   this->skReply1->GetMessage += gcnew CppCLITester::SKReply::GetMessageHandler(this, &CppCLITester::MyForm::GetMessage);
			   this->skReply1->WriteMessage += gcnew CppCLITester::SKReply::WriteMessageHandler(this, &CppCLITester::MyForm::WriteMessage);
			   // 
			   // tabPage3
			   // 
			   this->tabPage3->Controls->Add(this->skQuote1);
			   this->tabPage3->Location = System::Drawing::Point(4, 22);
			   this->tabPage3->Name = L"tabPage3";
			   this->tabPage3->Padding = System::Windows::Forms::Padding(3);
			   this->tabPage3->Size = System::Drawing::Size(192, 74);
			   this->tabPage3->TabIndex = 2;
			   this->tabPage3->Text = L"國內報價";
			   this->tabPage3->UseVisualStyleBackColor = true;
			   // 
			   // skQuote1
			   // 
			   this->skQuote1->Location = System::Drawing::Point(0, 0);
			   this->skQuote1->Name = L"skQuote1";
			   this->skQuote1->Size = System::Drawing::Size(953, 705);
			   this->skQuote1->TabIndex = 0;
			   this->skQuote1->GetMessage += gcnew CppCLITester::SKQuote::GetMessageHandler(this, &CppCLITester::MyForm::GetMessage);
			   this->skQuote1->WriteMessage += gcnew CppCLITester::SKQuote::WriteMessageHandler(this, &CppCLITester::MyForm::WriteMessage);
			   // 
			   // MyForm
			   // 
			   this->ClientSize = System::Drawing::Size(978, 960);
			   this->Controls->Add(this->tabControl1);
			   this->Controls->Add(this->txtPassword);
			   this->Controls->Add(this->lblPassword);
			   this->Controls->Add(this->listBox1);
			   this->Controls->Add(this->txtAccount);
			   this->Controls->Add(this->lblAccount);
			   this->Controls->Add(this->LogIn);
			   this->Name = L"MyForm";
			   this->tabControl1->ResumeLayout(false);
			   this->tabPage1->ResumeLayout(false);
			   this->tabPage2->ResumeLayout(false);
			   this->tabPage3->ResumeLayout(false);
			   this->tabPage4->ResumeLayout(false);
			   this->ResumeLayout(false);
			   this->PerformLayout();

		   }
#pragma endregion


};
}



