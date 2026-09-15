#pragma once
#include "StockOrderControl.h"
#include "FutureOrderControl.h"
#include "OptionOrderControl.h"

namespace CppCLITester {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	/// <summary>
	/// Order 的摘要
	/// </summary>
	public ref class SKOrder : public System::Windows::Forms::UserControl
	{

#pragma region  變數
	public:
		delegate void GetMessageHandler(System::String^ strType, int nCode, System::String^ strMessage);
		event GetMessageHandler^ GetMessage;

		delegate void WriteMessageHandler(System::String^ strMessage);
		event WriteMessageHandler^ WriteMessage;

		SKCOMLib::SKOrderLibClass^ get_data()
		{
			return m_pSKOrder;
		}

		void set_data(SKCOMLib::SKOrderLibClass^ value)
		{
			m_pSKOrder = value;
		}

		void get_UserID(System::String^ value)		{
			m_UserID = value;
		}

	private:
		SKCOMLib::SKOrderLibClass^ m_pSKOrder;
		System::String^ m_UserID;
		List < System::String^ >^ boxmessage = gcnew List<System::String^>();
		bool clicked = true;

#pragma endregion		

#pragma region Methods
		////methods
		   //StockOrderControl
		Void btnInitialize_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetAccount_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnReadCert_Click(System::Object^ sender, System::EventArgs^ e);

		Void OnOrderSignal(System::String^ m_UserID, bool bAsyncOrder, SKCOMLib::STOCKORDER pOrder);
		Void OnDecreaseSignal(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, int nDecreaseQty);
		Void OnCancelByStockNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrStockNo);
		Void OnCancelBySeqNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo);
		Void OnCancelByBookNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrBookNo);
		Void OnCorrectPriceBySeqNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, System::String^ bstrPrice, int  nTradeType);
		Void OnCorrectPriceByBookNo(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrMarketSymbol, System::String^ bstrBookNo, System::String^ bstrPrice, int  nTradeType);
		Void OnGetRealBalanceReport(System::String^ bstrLogInID, System::String^ bstrAccount);
		Void OnGetBalanceQuery(System::String^ bstrLogInID, System::String^ bstrAccount, System::String^ StockNo);
		Void OnGetMarginPurchaseAmountLimit(System::String^ bstrLogInID, System::String^ bstrAccount, System::String^ StockNo);
		Void OnGetRequestProfitReport(System::String^ bstrLogInID, System::String^ bstrAccount);
		Void OnProfitGWReportSignal(System::String^ bstrLogInID, SKCOMLib::TSPROFITLOSSGWQUERY pPLGWQuery);
		Void OnOddOrderSignal(System::String^ bstrLogInID, bool  bAsyncOrder, SKCOMLib::STOCKORDER pOrder);

		//FutureOrderControl
		Void OnFutureOrderSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pOrder);
		Void OnFutureOrderCLRSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pAsyncOrder);
		Void OnGetOpenInterest(System::String^ bstrLogInID, System::String^ bstrAccount);
		Void OnGetOpenInterestWithFormat(System::String^ bstrLogInID, System::String^ bstrAccount, int nFormat);
		Void GetFutureRights(System::String^ bstrLogInID, System::String^ bstrAccount, int CoinType);
		Void SendTXOffset(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrYearMonth, int nBuySell, int nQty);

		// 選擇權
		Void OnOptionOrderSignal(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pOrder);

		//event
		Void OnAccount(System::String^ bstrLoginID, System::String^ bstrAccountData);
		Void OnRealBalanceReport(System::String^ bstrMessage);
		Void OnBalanceQuery(System::String^ strMessage);
		Void OnMarginPurchaseAmountLimit(System::String^ strMessage);
		Void OnRequestProfitReport(System::String^ strMessage);
		Void OnProfitLossGWReport(System::String^ strMessage);
		Void OnFutureRights(System::String^ strMessage);
		Void OnOpenInterest(System::String^ strMessage);
		Void OnAsyncOrder(int nThreadID, int nCode, System::String^ bstrAccountData);
		//custom
		Void boxStockAccount_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e);


#pragma endregion

	public:
		SKOrder(void)
		{
			InitializeComponent();

			this->stockOrderControl1->OnOrderSignal += gcnew CppCLITester::StockOrderControl::OnOrderSignalHandler(this, &CppCLITester::SKOrder::OnOrderSignal);
			this->stockOrderControl1->OnDecreaseSignal += gcnew CppCLITester::StockOrderControl::OnDecreaseSignalHandler(this, &CppCLITester::SKOrder::OnDecreaseSignal);
			this->stockOrderControl1->OnCancelByStockNo += gcnew CppCLITester::StockOrderControl::OnCancelByStockNoHandler(this, &CppCLITester::SKOrder::OnCancelByStockNo);
			this->stockOrderControl1->OnCancelBySeqNo += gcnew CppCLITester::StockOrderControl::OnCancelBySeqNoHandler(this, &CppCLITester::SKOrder::OnCancelBySeqNo);
			this->stockOrderControl1->OnCancelByBookNo += gcnew CppCLITester::StockOrderControl::OnCancelByBookNoHandler(this, &CppCLITester::SKOrder::OnCancelByBookNo);
			this->stockOrderControl1->OnCorrectPriceBySeqNo += gcnew CppCLITester::StockOrderControl::OnCorrectPriceBySeqNoHandler(this, &CppCLITester::SKOrder::OnCorrectPriceBySeqNo);
			this->stockOrderControl1->OnCorrectPriceByBookNo += gcnew CppCLITester::StockOrderControl::OnCorrectPriceByBookNoHandler(this, &CppCLITester::SKOrder::OnCorrectPriceByBookNo);
			this->stockOrderControl1->OnGetRealBalanceReport += gcnew CppCLITester::StockOrderControl::OnGetRealBalanceReportHandler(this, &CppCLITester::SKOrder::OnGetRealBalanceReport);
			this->stockOrderControl1->OnGetBalanceQuery += gcnew CppCLITester::StockOrderControl::OnGetBalanceQueryHandler(this, &CppCLITester::SKOrder::OnGetBalanceQuery);
			this->stockOrderControl1->OnGetMarginPurchaseAmountLimit += gcnew CppCLITester::StockOrderControl::OnGetMarginPurchaseAmountLimitHandler(this, &CppCLITester::SKOrder::OnGetMarginPurchaseAmountLimit);
			this->stockOrderControl1->OnGetRequestProfitReport += gcnew CppCLITester::StockOrderControl::OnGetRequestProfitReportHandler(this, &CppCLITester::SKOrder::OnGetRequestProfitReport);
			this->stockOrderControl1->OnProfitGWReportSignal += gcnew CppCLITester::StockOrderControl::OnProfitGWReportSignalHandler(this, &CppCLITester::SKOrder::OnProfitGWReportSignal);
			this->stockOrderControl1->OnOddOrderSignal += gcnew CppCLITester::StockOrderControl::OnOddOrderSignalHandler(this, &CppCLITester::SKOrder::OnOddOrderSignal);
		
			this->futureOrderControl1->OnFutureOrderSignal += gcnew CppCLITester::FutureOrderControl::OnFutureOrderSignalHandler(this, &CppCLITester::SKOrder::OnFutureOrderSignal);
			this->futureOrderControl1->OnFutureOrderCLRSignal += gcnew CppCLITester::FutureOrderControl::OnFutureOrderCLRSignalHandler(this, &CppCLITester::SKOrder::OnFutureOrderCLRSignal);
			this->futureOrderControl1->OnDecreaseOrderSignal += gcnew CppCLITester::FutureOrderControl::OnDecreaseOrderSignalHandler(this, &CppCLITester::SKOrder::OnDecreaseSignal);
			this->futureOrderControl1->OnCancelByStockNo += gcnew CppCLITester::FutureOrderControl::OnCancelByStockNoHandler(this, &CppCLITester::SKOrder::OnCancelByStockNo);
			this->futureOrderControl1->OnCancelBySeqNo += gcnew CppCLITester::FutureOrderControl::OnCancelBySeqNoHandler(this, &CppCLITester::SKOrder::OnCancelBySeqNo);
			this->futureOrderControl1->OnCancelByBookNo += gcnew CppCLITester::FutureOrderControl::OnCancelByBookNoHandler(this, &CppCLITester::SKOrder::OnCancelByBookNo);
			this->futureOrderControl1->OnCorrectPriceBySeqNo += gcnew CppCLITester::FutureOrderControl::OnCorrectPriceBySeqNoHandler(this, &CppCLITester::SKOrder::OnCorrectPriceBySeqNo);
			this->futureOrderControl1->OnCorrectPriceByBookNo += gcnew CppCLITester::FutureOrderControl::OnCorrectPriceByBookNoHandler(this, &CppCLITester::SKOrder::OnCorrectPriceByBookNo);
			this->futureOrderControl1->OnGetOpenInterest += gcnew CppCLITester::FutureOrderControl::OnGetOpenInterestHandler(this, &CppCLITester::SKOrder::OnGetOpenInterest);
			this->futureOrderControl1->OnGetOpenInterestWithFormat += gcnew CppCLITester::FutureOrderControl::OnGetOpenInterestWithFormatHandler(this, &CppCLITester::SKOrder::OnGetOpenInterestWithFormat);
			this->futureOrderControl1->GetFutureRights += gcnew CppCLITester::FutureOrderControl::GetFutureRightsHandler(this, &CppCLITester::SKOrder::GetFutureRights);
			this->futureOrderControl1->SendTXOffset += gcnew CppCLITester::FutureOrderControl::SendTXOffsetHandler(this, &CppCLITester::SKOrder::SendTXOffset);
		
			this->optionOrderControl1->OnOptionOrderSignal += gcnew CppCLITester::OptionOrderControl::OnOptionOrderSignalHandler(this, &CppCLITester::SKOrder::OnOptionOrderSignal);
		}

	protected:
		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		~SKOrder()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TabControl^ tabControl1;
	protected:
	private: System::Windows::Forms::TabPage^ tabPage1;
	private: System::Windows::Forms::TabPage^ tabPage2;
	private: System::Windows::Forms::ComboBox^ boxOSStockAccount;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::ComboBox^ boxOSFutureAccount;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::ComboBox^ boxFutureAccount;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::ComboBox^ boxStockAccount;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Button^ btnGetAccount;
	private: System::Windows::Forms::Button^ btnReadCertByID;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::Button^ btnInitialize;
	private: System::Windows::Forms::Label^ label1;
	private: CppCLITester::StockOrderControl^ stockOrderControl1;
	private: CppCLITester::FutureOrderControl^ futureOrderControl1;
	private: System::Windows::Forms::TabPage^ tabPage3;
	private: System::Windows::Forms::ListBox^ listBox1;
	private: CppCLITester::OptionOrderControl^ optionOrderControl1;

	protected:


	private:
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		void InitializeComponent(void)
		{
			this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
			this->stockOrderControl1 = (gcnew CppCLITester::StockOrderControl());
			this->tabPage2 = (gcnew System::Windows::Forms::TabPage());
			this->futureOrderControl1 = (gcnew CppCLITester::FutureOrderControl());
			this->tabPage3 = (gcnew System::Windows::Forms::TabPage());
			this->optionOrderControl1 = (gcnew CppCLITester::OptionOrderControl());
			this->boxOSStockAccount = (gcnew System::Windows::Forms::ComboBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->boxOSFutureAccount = (gcnew System::Windows::Forms::ComboBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->boxFutureAccount = (gcnew System::Windows::Forms::ComboBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->boxStockAccount = (gcnew System::Windows::Forms::ComboBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->btnGetAccount = (gcnew System::Windows::Forms::Button());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->btnReadCertByID = (gcnew System::Windows::Forms::Button());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->btnInitialize = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->listBox1 = (gcnew System::Windows::Forms::ListBox());
			this->tabControl1->SuspendLayout();
			this->tabPage1->SuspendLayout();
			this->tabPage2->SuspendLayout();
			this->tabPage3->SuspendLayout();
			this->SuspendLayout();
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->tabPage1);
			this->tabControl1->Controls->Add(this->tabPage2);
			this->tabControl1->Controls->Add(this->tabPage3);
			this->tabControl1->Location = System::Drawing::Point(15, 120);
			this->tabControl1->Name = L"tabControl1";
			this->tabControl1->SelectedIndex = 0;
			this->tabControl1->Size = System::Drawing::Size(922, 554);
			this->tabControl1->TabIndex = 25;
			// 
			// tabPage1
			// 
			this->tabPage1->AutoScroll = true;
			this->tabPage1->Controls->Add(this->stockOrderControl1);
			this->tabPage1->Location = System::Drawing::Point(4, 22);
			this->tabPage1->Name = L"tabPage1";
			this->tabPage1->Padding = System::Windows::Forms::Padding(3);
			this->tabPage1->Size = System::Drawing::Size(914, 528);
			this->tabPage1->TabIndex = 0;
			this->tabPage1->Text = L"證券";
			this->tabPage1->UseVisualStyleBackColor = true;
			// 
			// stockOrderControl1
			// 
			this->stockOrderControl1->Location = System::Drawing::Point(14, 19);
			this->stockOrderControl1->Name = L"stockOrderControl1";
			this->stockOrderControl1->Size = System::Drawing::Size(877, 688);
			this->stockOrderControl1->TabIndex = 0;
			// 
			// tabPage2
			// 
			this->tabPage2->AutoScroll = true;
			this->tabPage2->Controls->Add(this->futureOrderControl1);
			this->tabPage2->Location = System::Drawing::Point(4, 22);
			this->tabPage2->Name = L"tabPage2";
			this->tabPage2->Padding = System::Windows::Forms::Padding(3);
			this->tabPage2->Size = System::Drawing::Size(914, 528);
			this->tabPage2->TabIndex = 1;
			this->tabPage2->Text = L"期貨";
			this->tabPage2->UseVisualStyleBackColor = true;
			// 
			// futureOrderControl1
			// 
			this->futureOrderControl1->Location = System::Drawing::Point(6, 6);
			this->futureOrderControl1->Name = L"futureOrderControl1";
			this->futureOrderControl1->Size = System::Drawing::Size(849, 596);
			this->futureOrderControl1->TabIndex = 0;
			// 
			// tabPage3
			// 
			this->tabPage3->Controls->Add(this->optionOrderControl1);
			this->tabPage3->Location = System::Drawing::Point(4, 22);
			this->tabPage3->Name = L"tabPage3";
			this->tabPage3->Padding = System::Windows::Forms::Padding(3);
			this->tabPage3->Size = System::Drawing::Size(914, 528);
			this->tabPage3->TabIndex = 2;
			this->tabPage3->Text = L"選擇權";
			this->tabPage3->UseVisualStyleBackColor = true;
			// 
			// optionOrderControl1
			// 
			this->optionOrderControl1->Location = System::Drawing::Point(23, 17);
			this->optionOrderControl1->Name = L"optionOrderControl1";
			this->optionOrderControl1->Size = System::Drawing::Size(849, 488);
			this->optionOrderControl1->TabIndex = 0;
			// 
			// boxOSStockAccount
			// 
			this->boxOSStockAccount->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxOSStockAccount->FormattingEnabled = true;
			this->boxOSStockAccount->Location = System::Drawing::Point(567, 94);
			this->boxOSStockAccount->Name = L"boxOSStockAccount";
			this->boxOSStockAccount->Size = System::Drawing::Size(151, 20);
			this->boxOSStockAccount->TabIndex = 24;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(603, 70);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(65, 12);
			this->label6->TabIndex = 23;
			this->label6->Text = L"複委託帳號";
			// 
			// boxOSFutureAccount
			// 
			this->boxOSFutureAccount->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxOSFutureAccount->FormattingEnabled = true;
			this->boxOSFutureAccount->Location = System::Drawing::Point(372, 94);
			this->boxOSFutureAccount->Name = L"boxOSFutureAccount";
			this->boxOSFutureAccount->Size = System::Drawing::Size(151, 20);
			this->boxOSFutureAccount->TabIndex = 22;
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(426, 70);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(53, 12);
			this->label5->TabIndex = 21;
			this->label5->Text = L"海期帳號";
			// 
			// boxFutureAccount
			// 
			this->boxFutureAccount->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxFutureAccount->FormattingEnabled = true;
			this->boxFutureAccount->Location = System::Drawing::Point(195, 94);
			this->boxFutureAccount->Name = L"boxFutureAccount";
			this->boxFutureAccount->Size = System::Drawing::Size(151, 20);
			this->boxFutureAccount->TabIndex = 20;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(240, 70);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(53, 12);
			this->label4->TabIndex = 19;
			this->label4->Text = L"期貨帳號";
			// 
			// boxStockAccount
			// 
			this->boxStockAccount->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxStockAccount->FormattingEnabled = true;
			this->boxStockAccount->Location = System::Drawing::Point(15, 94);
			this->boxStockAccount->Name = L"boxStockAccount";
			this->boxStockAccount->Size = System::Drawing::Size(151, 20);
			this->boxStockAccount->TabIndex = 18;
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(67, 70);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(53, 12);
			this->label3->TabIndex = 17;
			this->label3->Text = L"證券帳號";
			// 
			// btnGetAccount
			// 
			this->btnGetAccount->Location = System::Drawing::Point(715, 30);
			this->btnGetAccount->Name = L"btnGetAccount";
			this->btnGetAccount->Size = System::Drawing::Size(118, 23);
			this->btnGetAccount->TabIndex = 16;
			this->btnGetAccount->Text = L"Get Account";
			this->btnGetAccount->UseVisualStyleBackColor = true;
			this->btnGetAccount->Click += gcnew System::EventHandler(this, &SKOrder::btnGetAccount_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(613, 35);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(77, 12);
			this->label2->TabIndex = 15;
			this->label2->Text = L"取得下單帳號";
			// 
			// btnReadCertByID
			// 
			this->btnReadCertByID->Location = System::Drawing::Point(405, 30);
			this->btnReadCertByID->Name = L"btnReadCertByID";
			this->btnReadCertByID->Size = System::Drawing::Size(118, 23);
			this->btnReadCertByID->TabIndex = 16;
			this->btnReadCertByID->Text = L"ReadCertByID";
			this->btnReadCertByID->UseVisualStyleBackColor = true;
			this->btnReadCertByID->Click += gcnew System::EventHandler(this, &SKOrder::btnReadCert_Click);
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(313, 35);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(53, 12);
			this->label7->TabIndex = 15;
			this->label7->Text = L"取得憑證";
			// 
			// btnInitialize
			// 
			this->btnInitialize->Location = System::Drawing::Point(114, 30);
			this->btnInitialize->Name = L"btnInitialize";
			this->btnInitialize->Size = System::Drawing::Size(118, 23);
			this->btnInitialize->TabIndex = 14;
			this->btnInitialize->Text = L"Order Initialize";
			this->btnInitialize->UseVisualStyleBackColor = true;
			this->btnInitialize->Click += gcnew System::EventHandler(this, &SKOrder::btnInitialize_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(31, 35);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(77, 12);
			this->label1->TabIndex = 13;
			this->label1->Text = L"下單物件初始";
			// 
			// listBox1
			// 
			this->listBox1->FormattingEnabled = true;
			this->listBox1->HorizontalScrollbar = true;
			this->listBox1->ItemHeight = 12;
			this->listBox1->Location = System::Drawing::Point(15, 680);
			this->listBox1->Name = L"listBox1";
			this->listBox1->Size = System::Drawing::Size(918, 124);
			this->listBox1->TabIndex = 26;
			// 
			// SKOrder
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->listBox1);
			this->Controls->Add(this->tabControl1);
			this->Controls->Add(this->boxOSStockAccount);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->boxOSFutureAccount);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->boxFutureAccount);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->boxStockAccount);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->btnGetAccount);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->btnReadCertByID);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->btnInitialize);
			this->Controls->Add(this->label1);
			this->Name = L"SKOrder";
			this->Size = System::Drawing::Size(953, 828);
			this->tabControl1->ResumeLayout(false);
			this->tabPage1->ResumeLayout(false);
			this->tabPage2->ResumeLayout(false);
			this->tabPage3->ResumeLayout(false);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion


	};
}
