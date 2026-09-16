#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace CppCLITester {

	/// <summary>
	/// FutureOrderControl 的摘要
	/// </summary>
	public ref class FutureOrderControl : public System::Windows::Forms::UserControl
	{
		
#pragma region 變數
	public :
		// 期貨下單
		delegate void OnFutureOrderSignalHandler(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pOrder);
		event OnFutureOrderSignalHandler^ OnFutureOrderSignal;

		//期貨下單 CLR
		delegate void OnFutureOrderCLRSignalHandler(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pAsyncOrder);
		event OnFutureOrderCLRSignalHandler^ OnFutureOrderCLRSignal;


		//期貨減量
		delegate void OnDecreaseOrderSignalHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, int nDecreaseQty);
		event OnDecreaseOrderSignalHandler^ OnDecreaseOrderSignal;

		// 期貨刪單 - 商品代碼
		delegate void OnCancelByStockNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo);
		event OnCancelByStockNoHandler^ OnCancelByStockNo;

		// 期貨刪單 - 委託序號
		delegate void OnCancelBySeqNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo);
		event OnCancelBySeqNoHandler^ OnCancelBySeqNo;

		// 期貨刪單 - 書號
		delegate void OnCancelByBookNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo);
		event OnCancelByBookNoHandler^ OnCancelByBookNo;

		// 期貨改價 - 委託序號
		delegate void OnCorrectPriceBySeqNoHandler(System::String^ bstrLogInID,bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, System::String^ bstrPrice, int nTradeType);
		event OnCorrectPriceBySeqNoHandler^ OnCorrectPriceBySeqNo;
		
		// 期貨改嫁 - 書號
		delegate void OnCorrectPriceByBookNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrMarketSymbol, System::String^ bstrBookNo, System::String^ bstrPrice, int nTradeType );
		event OnCorrectPriceByBookNoHandler^ OnCorrectPriceByBookNo;

		//查詢期貨平倉
		delegate void OnGetOpenInterestHandler(System::String^ bstrLogInID, System::String^ bstrAccount);
		event OnGetOpenInterestHandler^ OnGetOpenInterest;

		//查詢期貨平倉 - 指定格式
		delegate void OnGetOpenInterestWithFormatHandler(System::String^ bstrLogInID, System::String^ bstrAccount, int nFormat);
		event OnGetOpenInterestWithFormatHandler^ OnGetOpenInterestWithFormat;

		//查詢國內權益數
		delegate void GetFutureRightsHandler(System::String^ bstrLogInID, System::String^ bstrAccount, int CoinType);
		event GetFutureRightsHandler^ GetFutureRights;

		//大小台互抵
		delegate void SendTXOffsetHandler(System::String^ bstrLogInID,bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrYearMonth, int nBuySell, int nQty);
		event SendTXOffsetHandler^ SendTXOffset;

		void set_UserAccount(System::String^ value)
		{
			m_UserAccount = value;
		}
		void set_UserID(System::String^ value)
		{
			m_UserID = value;
		}
	private :
		System::String^ m_UserAccount;
		System::String^ m_UserID;
#pragma endregion	

#pragma region Mehtod
		Void btnSendFutureOrder_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnSendFutureOrderCLR_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnDecreaseQty_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCancelOrder_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCancelOrderBySeqNo_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCancelOrderByBookNo_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCorrectPriceBySeqNo_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCorrectPriceByBookNo_Click(System::Object^ sender, System::EventArgs^ e);
		Void GetOpenInterest_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetOpenInterestFormat_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetFutureRights_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnSendTXOffset_Click(System::Object^ sender, System::EventArgs^ e);
#pragma endregion


	public:
		FutureOrderControl(void)
		{
			InitializeComponent();

			//
			//TODO:  在此加入建構函式程式碼
			//
		}

	protected:
		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		~FutureOrderControl()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::GroupBox^ groupBox7;
	protected:
	private: System::Windows::Forms::TextBox^ txtOffsetQty;
	private: System::Windows::Forms::Label^ label22;
	private: System::Windows::Forms::ComboBox^ boxOffsetBuySell;
	private: System::Windows::Forms::Label^ label21;
	private: System::Windows::Forms::TextBox^ txtOffsetYearMonth;
	private: System::Windows::Forms::Label^ label20;
	private: System::Windows::Forms::Button^ btnSendTXOffset;
	private: System::Windows::Forms::GroupBox^ groupBox6;
	private: System::Windows::Forms::Label^ label14;
	private: System::Windows::Forms::ComboBox^ comBox_CoinType;
	private: System::Windows::Forms::Button^ btnGetFutureRights;
	private: System::Windows::Forms::GroupBox^ groupBox5;
	private: System::Windows::Forms::Button^ GetOpenInterest;
	private: System::Windows::Forms::Label^ label23;
	private: System::Windows::Forms::Button^ btnGetOpenInterestFormat;
	private: System::Windows::Forms::ComboBox^ FormatBox;
	private: System::Windows::Forms::GroupBox^ groupBox3;
	private: System::Windows::Forms::ComboBox^ boxCorrectSymbol;
	private: System::Windows::Forms::Label^ label16;
	private: System::Windows::Forms::Button^ btnCorrectPriceByBookNo;
	private: System::Windows::Forms::TextBox^ txtCorrectBookNo;
	private: System::Windows::Forms::Label^ label15;
	private: System::Windows::Forms::Button^ btnCorrectPriceBySeqNo;
	private: System::Windows::Forms::ComboBox^ boxCorrectTradeType;
	private: System::Windows::Forms::Label^ label12;
	private: System::Windows::Forms::TextBox^ txtCorrectPrice;
	private: System::Windows::Forms::Label^ label10;
	private: System::Windows::Forms::TextBox^ txtCorrectSeqNo;
	private: System::Windows::Forms::Label^ label9;
	private: System::Windows::Forms::GroupBox^ groupBox2;
	private: System::Windows::Forms::Button^ btnCancelOrderByBookNo;
	private: System::Windows::Forms::TextBox^ txtCancelBookNo;
	private: System::Windows::Forms::Label^ label19;
	private: System::Windows::Forms::Button^ btnCancelOrderBySeqNo;
	private: System::Windows::Forms::TextBox^ txtCancelSeqNo;
	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::Button^ btnCancelOrder;
	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::TextBox^ txtCancelStockNo;
	private: System::Windows::Forms::GroupBox^ groupBox4;
	private: System::Windows::Forms::TextBox^ txtDecreaseQty;
	private: System::Windows::Forms::Label^ label13;
	private: System::Windows::Forms::Button^ btnDecreaseQty;
	private: System::Windows::Forms::TextBox^ txtDecreaseSeqNo;
	private: System::Windows::Forms::Label^ label11;
	private: System::Windows::Forms::GroupBox^ groupBox1;
	private: System::Windows::Forms::ComboBox^ boxReserved;
	private: System::Windows::Forms::Label^ label18;

	private: System::Windows::Forms::Button^ btnSendFutureOrderCLR;
	private: System::Windows::Forms::ComboBox^ boxNewClose;
	private: System::Windows::Forms::Label^ label17;

	private: System::Windows::Forms::Button^ btnSendFutureOrder;
	private: System::Windows::Forms::TextBox^ txtQty;
	private: System::Windows::Forms::TextBox^ txtPrice;
	private: System::Windows::Forms::ComboBox^ boxFlag;
	private: System::Windows::Forms::ComboBox^ boxPeriod;
	private: System::Windows::Forms::ComboBox^ boxBidAsk;
	private: System::Windows::Forms::TextBox^ txtStockNo;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::CheckBox^ ckboxAsyn;

	private:
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		void InitializeComponent(void)
		{
			this->groupBox7 = (gcnew System::Windows::Forms::GroupBox());
			this->txtOffsetQty = (gcnew System::Windows::Forms::TextBox());
			this->label22 = (gcnew System::Windows::Forms::Label());
			this->boxOffsetBuySell = (gcnew System::Windows::Forms::ComboBox());
			this->label21 = (gcnew System::Windows::Forms::Label());
			this->txtOffsetYearMonth = (gcnew System::Windows::Forms::TextBox());
			this->label20 = (gcnew System::Windows::Forms::Label());
			this->btnSendTXOffset = (gcnew System::Windows::Forms::Button());
			this->groupBox6 = (gcnew System::Windows::Forms::GroupBox());
			this->label14 = (gcnew System::Windows::Forms::Label());
			this->comBox_CoinType = (gcnew System::Windows::Forms::ComboBox());
			this->btnGetFutureRights = (gcnew System::Windows::Forms::Button());
			this->groupBox5 = (gcnew System::Windows::Forms::GroupBox());
			this->GetOpenInterest = (gcnew System::Windows::Forms::Button());
			this->label23 = (gcnew System::Windows::Forms::Label());
			this->btnGetOpenInterestFormat = (gcnew System::Windows::Forms::Button());
			this->FormatBox = (gcnew System::Windows::Forms::ComboBox());
			this->groupBox3 = (gcnew System::Windows::Forms::GroupBox());
			this->boxCorrectSymbol = (gcnew System::Windows::Forms::ComboBox());
			this->label16 = (gcnew System::Windows::Forms::Label());
			this->btnCorrectPriceByBookNo = (gcnew System::Windows::Forms::Button());
			this->txtCorrectBookNo = (gcnew System::Windows::Forms::TextBox());
			this->label15 = (gcnew System::Windows::Forms::Label());
			this->btnCorrectPriceBySeqNo = (gcnew System::Windows::Forms::Button());
			this->boxCorrectTradeType = (gcnew System::Windows::Forms::ComboBox());
			this->label12 = (gcnew System::Windows::Forms::Label());
			this->txtCorrectPrice = (gcnew System::Windows::Forms::TextBox());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->txtCorrectSeqNo = (gcnew System::Windows::Forms::TextBox());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->btnCancelOrderByBookNo = (gcnew System::Windows::Forms::Button());
			this->txtCancelBookNo = (gcnew System::Windows::Forms::TextBox());
			this->label19 = (gcnew System::Windows::Forms::Label());
			this->btnCancelOrderBySeqNo = (gcnew System::Windows::Forms::Button());
			this->txtCancelSeqNo = (gcnew System::Windows::Forms::TextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->btnCancelOrder = (gcnew System::Windows::Forms::Button());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->txtCancelStockNo = (gcnew System::Windows::Forms::TextBox());
			this->groupBox4 = (gcnew System::Windows::Forms::GroupBox());
			this->txtDecreaseQty = (gcnew System::Windows::Forms::TextBox());
			this->label13 = (gcnew System::Windows::Forms::Label());
			this->btnDecreaseQty = (gcnew System::Windows::Forms::Button());
			this->txtDecreaseSeqNo = (gcnew System::Windows::Forms::TextBox());
			this->label11 = (gcnew System::Windows::Forms::Label());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->ckboxAsyn = (gcnew System::Windows::Forms::CheckBox());
			this->boxReserved = (gcnew System::Windows::Forms::ComboBox());
			this->label18 = (gcnew System::Windows::Forms::Label());
			this->btnSendFutureOrderCLR = (gcnew System::Windows::Forms::Button());
			this->boxNewClose = (gcnew System::Windows::Forms::ComboBox());
			this->label17 = (gcnew System::Windows::Forms::Label());
			this->btnSendFutureOrder = (gcnew System::Windows::Forms::Button());
			this->txtQty = (gcnew System::Windows::Forms::TextBox());
			this->txtPrice = (gcnew System::Windows::Forms::TextBox());
			this->boxFlag = (gcnew System::Windows::Forms::ComboBox());
			this->boxPeriod = (gcnew System::Windows::Forms::ComboBox());
			this->boxBidAsk = (gcnew System::Windows::Forms::ComboBox());
			this->txtStockNo = (gcnew System::Windows::Forms::TextBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->groupBox7->SuspendLayout();
			this->groupBox6->SuspendLayout();
			this->groupBox5->SuspendLayout();
			this->groupBox3->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->groupBox4->SuspendLayout();
			this->groupBox1->SuspendLayout();
			this->SuspendLayout();
			// 
			// groupBox7
			// 
			this->groupBox7->Controls->Add(this->txtOffsetQty);
			this->groupBox7->Controls->Add(this->label22);
			this->groupBox7->Controls->Add(this->boxOffsetBuySell);
			this->groupBox7->Controls->Add(this->label21);
			this->groupBox7->Controls->Add(this->txtOffsetYearMonth);
			this->groupBox7->Controls->Add(this->label20);
			this->groupBox7->Controls->Add(this->btnSendTXOffset);
			this->groupBox7->Location = System::Drawing::Point(27, 531);
			this->groupBox7->Name = L"groupBox7";
			this->groupBox7->Size = System::Drawing::Size(798, 49);
			this->groupBox7->TabIndex = 18;
			this->groupBox7->TabStop = false;
			this->groupBox7->Text = L"大小台互抵";
			// 
			// txtOffsetQty
			// 
			this->txtOffsetQty->Location = System::Drawing::Point(323, 19);
			this->txtOffsetQty->Name = L"txtOffsetQty";
			this->txtOffsetQty->Size = System::Drawing::Size(49, 22);
			this->txtOffsetQty->TabIndex = 27;
			// 
			// label22
			// 
			this->label22->AutoSize = true;
			this->label22->Location = System::Drawing::Point(272, 22);
			this->label22->Name = L"label22";
			this->label22->Size = System::Drawing::Size(41, 12);
			this->label22->TabIndex = 26;
			this->label22->Text = L"委託量";
			// 
			// boxOffsetBuySell
			// 
			this->boxOffsetBuySell->FormattingEnabled = true;
			this->boxOffsetBuySell->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"買", L"賣" });
			this->boxOffsetBuySell->Location = System::Drawing::Point(198, 19);
			this->boxOffsetBuySell->Name = L"boxOffsetBuySell";
			this->boxOffsetBuySell->Size = System::Drawing::Size(49, 20);
			this->boxOffsetBuySell->TabIndex = 25;
			// 
			// label21
			// 
			this->label21->AutoSize = true;
			this->label21->Location = System::Drawing::Point(149, 22);
			this->label21->Name = L"label21";
			this->label21->Size = System::Drawing::Size(41, 12);
			this->label21->TabIndex = 24;
			this->label21->Text = L"買賣別";
			// 
			// txtOffsetYearMonth
			// 
			this->txtOffsetYearMonth->Location = System::Drawing::Point(63, 19);
			this->txtOffsetYearMonth->MaxLength = 15;
			this->txtOffsetYearMonth->Name = L"txtOffsetYearMonth";
			this->txtOffsetYearMonth->Size = System::Drawing::Size(79, 22);
			this->txtOffsetYearMonth->TabIndex = 23;
			// 
			// label20
			// 
			this->label20->AutoSize = true;
			this->label20->Location = System::Drawing::Point(28, 22);
			this->label20->Name = L"label20";
			this->label20->Size = System::Drawing::Size(29, 12);
			this->label20->TabIndex = 22;
			this->label20->Text = L"年月";
			// 
			// btnSendTXOffset
			// 
			this->btnSendTXOffset->Location = System::Drawing::Point(550, 12);
			this->btnSendTXOffset->Name = L"btnSendTXOffset";
			this->btnSendTXOffset->Size = System::Drawing::Size(175, 29);
			this->btnSendTXOffset->TabIndex = 10;
			this->btnSendTXOffset->Text = L"SendTXOffset";
			this->btnSendTXOffset->UseVisualStyleBackColor = true;
			this->btnSendTXOffset->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnSendTXOffset_Click);
			// 
			// groupBox6
			// 
			this->groupBox6->Controls->Add(this->label14);
			this->groupBox6->Controls->Add(this->comBox_CoinType);
			this->groupBox6->Controls->Add(this->btnGetFutureRights);
			this->groupBox6->Location = System::Drawing::Point(428, 465);
			this->groupBox6->Name = L"groupBox6";
			this->groupBox6->Size = System::Drawing::Size(399, 49);
			this->groupBox6->TabIndex = 17;
			this->groupBox6->TabStop = false;
			this->groupBox6->Text = L"權益數";
			// 
			// label14
			// 
			this->label14->AutoSize = true;
			this->label14->Location = System::Drawing::Point(49, 22);
			this->label14->Name = L"label14";
			this->label14->Size = System::Drawing::Size(29, 12);
			this->label14->TabIndex = 22;
			this->label14->Text = L"幣別";
			// 
			// comBox_CoinType
			// 
			this->comBox_CoinType->FormattingEnabled = true;
			this->comBox_CoinType->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"ALL", L"TWD", L"RMB" });
			this->comBox_CoinType->Location = System::Drawing::Point(111, 19);
			this->comBox_CoinType->Name = L"comBox_CoinType";
			this->comBox_CoinType->Size = System::Drawing::Size(64, 20);
			this->comBox_CoinType->TabIndex = 21;
			// 
			// btnGetFutureRights
			// 
			this->btnGetFutureRights->Location = System::Drawing::Point(218, 14);
			this->btnGetFutureRights->Name = L"btnGetFutureRights";
			this->btnGetFutureRights->Size = System::Drawing::Size(175, 29);
			this->btnGetFutureRights->TabIndex = 10;
			this->btnGetFutureRights->Text = L"GetFutureRights";
			this->btnGetFutureRights->UseVisualStyleBackColor = true;
			this->btnGetFutureRights->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnGetFutureRights_Click);
			// 
			// groupBox5
			// 
			this->groupBox5->Controls->Add(this->GetOpenInterest);
			this->groupBox5->Controls->Add(this->label23);
			this->groupBox5->Controls->Add(this->btnGetOpenInterestFormat);
			this->groupBox5->Controls->Add(this->FormatBox);
			this->groupBox5->Location = System::Drawing::Point(27, 465);
			this->groupBox5->Name = L"groupBox5";
			this->groupBox5->Size = System::Drawing::Size(383, 49);
			this->groupBox5->TabIndex = 16;
			this->groupBox5->TabStop = false;
			this->groupBox5->Text = L"未平倉";
			// 
			// GetOpenInterest
			// 
			this->GetOpenInterest->Location = System::Drawing::Point(39, 16);
			this->GetOpenInterest->Name = L"GetOpenInterest";
			this->GetOpenInterest->Size = System::Drawing::Size(103, 29);
			this->GetOpenInterest->TabIndex = 27;
			this->GetOpenInterest->Text = L"GetOpenInterest";
			this->GetOpenInterest->UseVisualStyleBackColor = true;
			this->GetOpenInterest->Click += gcnew System::EventHandler(this, &FutureOrderControl::GetOpenInterest_Click);
			// 
			// label23
			// 
			this->label23->AutoSize = true;
			this->label23->Location = System::Drawing::Point(169, 8);
			this->label23->Name = L"label23";
			this->label23->Size = System::Drawing::Size(29, 12);
			this->label23->TabIndex = 23;
			this->label23->Text = L"格式";
			// 
			// btnGetOpenInterestFormat
			// 
			this->btnGetOpenInterestFormat->Location = System::Drawing::Point(257, 12);
			this->btnGetOpenInterestFormat->Name = L"btnGetOpenInterestFormat";
			this->btnGetOpenInterestFormat->Size = System::Drawing::Size(127, 29);
			this->btnGetOpenInterestFormat->TabIndex = 10;
			this->btnGetOpenInterestFormat->Text = L"GetOpenInterestFormat";
			this->btnGetOpenInterestFormat->UseVisualStyleBackColor = true;
			this->btnGetOpenInterestFormat->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnGetOpenInterestFormat_Click);
			// 
			// FormatBox
			// 
			this->FormatBox->FormattingEnabled = true;
			this->FormatBox->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"1:完整", L"2:格式1", L"3:格式2" });
			this->FormatBox->Location = System::Drawing::Point(168, 21);
			this->FormatBox->Name = L"FormatBox";
			this->FormatBox->Size = System::Drawing::Size(81, 20);
			this->FormatBox->TabIndex = 22;
			// 
			// groupBox3
			// 
			this->groupBox3->Controls->Add(this->boxCorrectSymbol);
			this->groupBox3->Controls->Add(this->label16);
			this->groupBox3->Controls->Add(this->btnCorrectPriceByBookNo);
			this->groupBox3->Controls->Add(this->txtCorrectBookNo);
			this->groupBox3->Controls->Add(this->label15);
			this->groupBox3->Controls->Add(this->btnCorrectPriceBySeqNo);
			this->groupBox3->Controls->Add(this->boxCorrectTradeType);
			this->groupBox3->Controls->Add(this->label12);
			this->groupBox3->Controls->Add(this->txtCorrectPrice);
			this->groupBox3->Controls->Add(this->label10);
			this->groupBox3->Controls->Add(this->txtCorrectSeqNo);
			this->groupBox3->Controls->Add(this->label9);
			this->groupBox3->Location = System::Drawing::Point(29, 356);
			this->groupBox3->Name = L"groupBox3";
			this->groupBox3->Size = System::Drawing::Size(798, 94);
			this->groupBox3->TabIndex = 15;
			this->groupBox3->TabStop = false;
			this->groupBox3->Text = L"改價";
			// 
			// boxCorrectSymbol
			// 
			this->boxCorrectSymbol->FormattingEnabled = true;
			this->boxCorrectSymbol->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"TF", L"TO" });
			this->boxCorrectSymbol->Location = System::Drawing::Point(255, 66);
			this->boxCorrectSymbol->Name = L"boxCorrectSymbol";
			this->boxCorrectSymbol->Size = System::Drawing::Size(69, 20);
			this->boxCorrectSymbol->TabIndex = 26;
			// 
			// label16
			// 
			this->label16->AutoSize = true;
			this->label16->Location = System::Drawing::Point(196, 72);
			this->label16->Name = L"label16";
			this->label16->Size = System::Drawing::Size(53, 12);
			this->label16->TabIndex = 25;
			this->label16->Text = L"市場簡稱";
			// 
			// btnCorrectPriceByBookNo
			// 
			this->btnCorrectPriceByBookNo->Location = System::Drawing::Point(602, 61);
			this->btnCorrectPriceByBookNo->Name = L"btnCorrectPriceByBookNo";
			this->btnCorrectPriceByBookNo->Size = System::Drawing::Size(190, 27);
			this->btnCorrectPriceByBookNo->TabIndex = 24;
			this->btnCorrectPriceByBookNo->Text = L"CorrectPriceByBookNo";
			this->btnCorrectPriceByBookNo->UseVisualStyleBackColor = true;
			this->btnCorrectPriceByBookNo->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnCorrectPriceByBookNo_Click);
			// 
			// txtCorrectBookNo
			// 
			this->txtCorrectBookNo->Location = System::Drawing::Point(101, 66);
			this->txtCorrectBookNo->Name = L"txtCorrectBookNo";
			this->txtCorrectBookNo->Size = System::Drawing::Size(89, 22);
			this->txtCorrectBookNo->TabIndex = 22;
			// 
			// label15
			// 
			this->label15->AutoSize = true;
			this->label15->Location = System::Drawing::Point(14, 72);
			this->label15->Name = L"label15";
			this->label15->Size = System::Drawing::Size(53, 12);
			this->label15->TabIndex = 23;
			this->label15->Text = L"委託書號";
			// 
			// btnCorrectPriceBySeqNo
			// 
			this->btnCorrectPriceBySeqNo->Location = System::Drawing::Point(602, 16);
			this->btnCorrectPriceBySeqNo->Name = L"btnCorrectPriceBySeqNo";
			this->btnCorrectPriceBySeqNo->Size = System::Drawing::Size(190, 27);
			this->btnCorrectPriceBySeqNo->TabIndex = 21;
			this->btnCorrectPriceBySeqNo->Text = L"CorrectPriceBySeqNo";
			this->btnCorrectPriceBySeqNo->UseVisualStyleBackColor = true;
			this->btnCorrectPriceBySeqNo->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnCorrectPriceBySeqNo_Click);
			// 
			// boxCorrectTradeType
			// 
			this->boxCorrectTradeType->FormattingEnabled = true;
			this->boxCorrectTradeType->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"ROD", L"IOC", L"FOK" });
			this->boxCorrectTradeType->Location = System::Drawing::Point(529, 43);
			this->boxCorrectTradeType->Name = L"boxCorrectTradeType";
			this->boxCorrectTradeType->Size = System::Drawing::Size(64, 20);
			this->boxCorrectTradeType->TabIndex = 20;
			// 
			// label12
			// 
			this->label12->AutoSize = true;
			this->label12->Location = System::Drawing::Point(470, 46);
			this->label12->Name = L"label12";
			this->label12->Size = System::Drawing::Size(53, 12);
			this->label12->TabIndex = 19;
			this->label12->Text = L"委託條件";
			// 
			// txtCorrectPrice
			// 
			this->txtCorrectPrice->Location = System::Drawing::Point(390, 43);
			this->txtCorrectPrice->Name = L"txtCorrectPrice";
			this->txtCorrectPrice->Size = System::Drawing::Size(74, 22);
			this->txtCorrectPrice->TabIndex = 18;
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->Location = System::Drawing::Point(331, 46);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(53, 12);
			this->label10->TabIndex = 17;
			this->label10->Text = L"修改價格";
			// 
			// txtCorrectSeqNo
			// 
			this->txtCorrectSeqNo->Location = System::Drawing::Point(101, 21);
			this->txtCorrectSeqNo->Name = L"txtCorrectSeqNo";
			this->txtCorrectSeqNo->Size = System::Drawing::Size(136, 22);
			this->txtCorrectSeqNo->TabIndex = 15;
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(14, 27);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(53, 12);
			this->label9->TabIndex = 16;
			this->label9->Text = L"委託序號";
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->btnCancelOrderByBookNo);
			this->groupBox2->Controls->Add(this->txtCancelBookNo);
			this->groupBox2->Controls->Add(this->label19);
			this->groupBox2->Controls->Add(this->btnCancelOrderBySeqNo);
			this->groupBox2->Controls->Add(this->txtCancelSeqNo);
			this->groupBox2->Controls->Add(this->label8);
			this->groupBox2->Controls->Add(this->btnCancelOrder);
			this->groupBox2->Controls->Add(this->label7);
			this->groupBox2->Controls->Add(this->txtCancelStockNo);
			this->groupBox2->Location = System::Drawing::Point(27, 256);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(800, 81);
			this->groupBox2->TabIndex = 14;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"取消委託";
			// 
			// btnCancelOrderByBookNo
			// 
			this->btnCancelOrderByBookNo->Location = System::Drawing::Point(630, 48);
			this->btnCancelOrderByBookNo->Name = L"btnCancelOrderByBookNo";
			this->btnCancelOrderByBookNo->Size = System::Drawing::Size(162, 23);
			this->btnCancelOrderByBookNo->TabIndex = 8;
			this->btnCancelOrderByBookNo->Text = L"Cancel Order By BookNo";
			this->btnCancelOrderByBookNo->UseVisualStyleBackColor = true;
			this->btnCancelOrderByBookNo->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnCancelOrderByBookNo_Click);
			// 
			// txtCancelBookNo
			// 
			this->txtCancelBookNo->Location = System::Drawing::Point(488, 48);
			this->txtCancelBookNo->Name = L"txtCancelBookNo";
			this->txtCancelBookNo->Size = System::Drawing::Size(136, 22);
			this->txtCancelBookNo->TabIndex = 7;
			// 
			// label19
			// 
			this->label19->AutoSize = true;
			this->label19->Location = System::Drawing::Point(429, 54);
			this->label19->Name = L"label19";
			this->label19->Size = System::Drawing::Size(53, 12);
			this->label19->TabIndex = 6;
			this->label19->Text = L"委託書號";
			// 
			// btnCancelOrderBySeqNo
			// 
			this->btnCancelOrderBySeqNo->Location = System::Drawing::Point(245, 48);
			this->btnCancelOrderBySeqNo->Name = L"btnCancelOrderBySeqNo";
			this->btnCancelOrderBySeqNo->Size = System::Drawing::Size(178, 23);
			this->btnCancelOrderBySeqNo->TabIndex = 5;
			this->btnCancelOrderBySeqNo->Text = L"Cancel Order By SeqNo";
			this->btnCancelOrderBySeqNo->UseVisualStyleBackColor = true;
			this->btnCancelOrderBySeqNo->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnCancelOrderBySeqNo_Click);
			// 
			// txtCancelSeqNo
			// 
			this->txtCancelSeqNo->Location = System::Drawing::Point(103, 51);
			this->txtCancelSeqNo->Name = L"txtCancelSeqNo";
			this->txtCancelSeqNo->Size = System::Drawing::Size(136, 22);
			this->txtCancelSeqNo->TabIndex = 4;
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(16, 61);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(53, 12);
			this->label8->TabIndex = 3;
			this->label8->Text = L"委託序號";
			// 
			// btnCancelOrder
			// 
			this->btnCancelOrder->Location = System::Drawing::Point(245, 19);
			this->btnCancelOrder->Name = L"btnCancelOrder";
			this->btnCancelOrder->Size = System::Drawing::Size(178, 23);
			this->btnCancelOrder->TabIndex = 2;
			this->btnCancelOrder->Text = L"Cancel Order By StockNo";
			this->btnCancelOrder->UseVisualStyleBackColor = true;
			this->btnCancelOrder->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnCancelOrder_Click);
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(16, 30);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(53, 12);
			this->label7->TabIndex = 1;
			this->label7->Text = L"商品代碼";
			// 
			// txtCancelStockNo
			// 
			this->txtCancelStockNo->Location = System::Drawing::Point(103, 20);
			this->txtCancelStockNo->Name = L"txtCancelStockNo";
			this->txtCancelStockNo->Size = System::Drawing::Size(136, 22);
			this->txtCancelStockNo->TabIndex = 0;
			// 
			// groupBox4
			// 
			this->groupBox4->Controls->Add(this->txtDecreaseQty);
			this->groupBox4->Controls->Add(this->label13);
			this->groupBox4->Controls->Add(this->btnDecreaseQty);
			this->groupBox4->Controls->Add(this->txtDecreaseSeqNo);
			this->groupBox4->Controls->Add(this->label11);
			this->groupBox4->Location = System::Drawing::Point(27, 172);
			this->groupBox4->Name = L"groupBox4";
			this->groupBox4->Size = System::Drawing::Size(800, 54);
			this->groupBox4->TabIndex = 13;
			this->groupBox4->TabStop = false;
			this->groupBox4->Text = L"委託減量";
			// 
			// txtDecreaseQty
			// 
			this->txtDecreaseQty->Location = System::Drawing::Point(399, 18);
			this->txtDecreaseQty->Name = L"txtDecreaseQty";
			this->txtDecreaseQty->Size = System::Drawing::Size(72, 22);
			this->txtDecreaseQty->TabIndex = 10;
			// 
			// label13
			// 
			this->label13->AutoSize = true;
			this->label13->Location = System::Drawing::Point(262, 22);
			this->label13->Name = L"label13";
			this->label13->Size = System::Drawing::Size(104, 12);
			this->label13->TabIndex = 17;
			this->label13->Text = L" 輸入欲減少的數量";
			// 
			// btnDecreaseQty
			// 
			this->btnDecreaseQty->Location = System::Drawing::Point(582, 18);
			this->btnDecreaseQty->Name = L"btnDecreaseQty";
			this->btnDecreaseQty->Size = System::Drawing::Size(190, 23);
			this->btnDecreaseQty->TabIndex = 11;
			this->btnDecreaseQty->Text = L"Decrease Order By SeqNo";
			this->btnDecreaseQty->UseVisualStyleBackColor = true;
			this->btnDecreaseQty->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnDecreaseQty_Click);
			// 
			// txtDecreaseSeqNo
			// 
			this->txtDecreaseSeqNo->Location = System::Drawing::Point(103, 18);
			this->txtDecreaseSeqNo->Name = L"txtDecreaseSeqNo";
			this->txtDecreaseSeqNo->Size = System::Drawing::Size(136, 22);
			this->txtDecreaseSeqNo->TabIndex = 9;
			// 
			// label11
			// 
			this->label11->AutoSize = true;
			this->label11->Location = System::Drawing::Point(16, 24);
			this->label11->Name = L"label11";
			this->label11->Size = System::Drawing::Size(53, 12);
			this->label11->TabIndex = 14;
			this->label11->Text = L"委託序號";
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->ckboxAsyn);
			this->groupBox1->Controls->Add(this->boxReserved);
			this->groupBox1->Controls->Add(this->label18);
			this->groupBox1->Controls->Add(this->btnSendFutureOrderCLR);
			this->groupBox1->Controls->Add(this->boxNewClose);
			this->groupBox1->Controls->Add(this->label17);
			this->groupBox1->Controls->Add(this->btnSendFutureOrder);
			this->groupBox1->Controls->Add(this->txtQty);
			this->groupBox1->Controls->Add(this->txtPrice);
			this->groupBox1->Controls->Add(this->boxFlag);
			this->groupBox1->Controls->Add(this->boxPeriod);
			this->groupBox1->Controls->Add(this->boxBidAsk);
			this->groupBox1->Controls->Add(this->txtStockNo);
			this->groupBox1->Controls->Add(this->label6);
			this->groupBox1->Controls->Add(this->label5);
			this->groupBox1->Controls->Add(this->label4);
			this->groupBox1->Controls->Add(this->label3);
			this->groupBox1->Controls->Add(this->label2);
			this->groupBox1->Controls->Add(this->label1);
			this->groupBox1->Location = System::Drawing::Point(27, 14);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(800, 130);
			this->groupBox1->TabIndex = 12;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"期貨委託";
			// 
			// ckboxAsyn
			// 
			this->ckboxAsyn->AutoSize = true;
			this->ckboxAsyn->Location = System::Drawing::Point(507, 45);
			this->ckboxAsyn->Name = L"ckboxAsyn";
			this->ckboxAsyn->Size = System::Drawing::Size(60, 16);
			this->ckboxAsyn->TabIndex = 20;
			this->ckboxAsyn->Text = L"非同步";
			this->ckboxAsyn->UseVisualStyleBackColor = true;
			// 
			// boxReserved
			// 
			this->boxReserved->FormattingEnabled = true;
			this->boxReserved->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"盤中", L"T盤預約" });
			this->boxReserved->Location = System::Drawing::Point(427, 98);
			this->boxReserved->Name = L"boxReserved";
			this->boxReserved->Size = System::Drawing::Size(88, 20);
			this->boxReserved->TabIndex = 19;
			// 
			// label18
			// 
			this->label18->AutoSize = true;
			this->label18->Location = System::Drawing::Point(425, 83);
			this->label18->Name = L"label18";
			this->label18->Size = System::Drawing::Size(29, 12);
			this->label18->TabIndex = 18;
			this->label18->Text = L"盤別";
			// 
			// btnSendFutureOrderCLR
			// 
			this->btnSendFutureOrderCLR->Location = System::Drawing::Point(614, 72);
			this->btnSendFutureOrderCLR->Name = L"btnSendFutureOrderCLR";
			this->btnSendFutureOrderCLR->Size = System::Drawing::Size(178, 23);
			this->btnSendFutureOrderCLR->TabIndex = 16;
			this->btnSendFutureOrderCLR->Text = L"SendFutureOrderCLR";
			this->btnSendFutureOrderCLR->UseVisualStyleBackColor = true;
			this->btnSendFutureOrderCLR->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnSendFutureOrderCLR_Click);
			// 
			// boxNewClose
			// 
			this->boxNewClose->FormattingEnabled = true;
			this->boxNewClose->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"新倉", L"平倉", L"自動" });
			this->boxNewClose->Location = System::Drawing::Point(333, 99);
			this->boxNewClose->Name = L"boxNewClose";
			this->boxNewClose->Size = System::Drawing::Size(50, 20);
			this->boxNewClose->TabIndex = 15;
			// 
			// label17
			// 
			this->label17->AutoSize = true;
			this->label17->Location = System::Drawing::Point(331, 84);
			this->label17->Name = L"label17";
			this->label17->Size = System::Drawing::Size(29, 12);
			this->label17->TabIndex = 14;
			this->label17->Text = L"倉別";
			// 
			// btnSendFutureOrder
			// 
			this->btnSendFutureOrder->Location = System::Drawing::Point(614, 39);
			this->btnSendFutureOrder->Name = L"btnSendFutureOrder";
			this->btnSendFutureOrder->Size = System::Drawing::Size(178, 23);
			this->btnSendFutureOrder->TabIndex = 12;
			this->btnSendFutureOrder->Text = L"SendFutureOrder";
			this->btnSendFutureOrder->UseVisualStyleBackColor = true;
			this->btnSendFutureOrder->Click += gcnew System::EventHandler(this, &FutureOrderControl::btnSendFutureOrder_Click);
			// 
			// txtQty
			// 
			this->txtQty->Location = System::Drawing::Point(427, 42);
			this->txtQty->Name = L"txtQty";
			this->txtQty->Size = System::Drawing::Size(49, 22);
			this->txtQty->TabIndex = 11;
			// 
			// txtPrice
			// 
			this->txtPrice->Location = System::Drawing::Point(335, 42);
			this->txtPrice->Name = L"txtPrice";
			this->txtPrice->Size = System::Drawing::Size(74, 22);
			this->txtPrice->TabIndex = 10;
			// 
			// boxFlag
			// 
			this->boxFlag->FormattingEnabled = true;
			this->boxFlag->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"非當沖", L"當沖" });
			this->boxFlag->Location = System::Drawing::Point(247, 42);
			this->boxFlag->Name = L"boxFlag";
			this->boxFlag->Size = System::Drawing::Size(68, 20);
			this->boxFlag->TabIndex = 9;
			// 
			// boxPeriod
			// 
			this->boxPeriod->FormattingEnabled = true;
			this->boxPeriod->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"ROD", L"IOC", L"FOK" });
			this->boxPeriod->Location = System::Drawing::Point(173, 42);
			this->boxPeriod->Name = L"boxPeriod";
			this->boxPeriod->Size = System::Drawing::Size(64, 20);
			this->boxPeriod->TabIndex = 8;
			// 
			// boxBidAsk
			// 
			this->boxBidAsk->FormattingEnabled = true;
			this->boxBidAsk->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"買", L"賣" });
			this->boxBidAsk->Location = System::Drawing::Point(105, 42);
			this->boxBidAsk->Name = L"boxBidAsk";
			this->boxBidAsk->Size = System::Drawing::Size(49, 20);
			this->boxBidAsk->TabIndex = 7;
			// 
			// txtStockNo
			// 
			this->txtStockNo->Location = System::Drawing::Point(19, 42);
			this->txtStockNo->MaxLength = 15;
			this->txtStockNo->Name = L"txtStockNo";
			this->txtStockNo->Size = System::Drawing::Size(64, 22);
			this->txtStockNo->TabIndex = 6;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(425, 18);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(41, 12);
			this->label6->TabIndex = 5;
			this->label6->Text = L"委託量";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(333, 18);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(41, 12);
			this->label5->TabIndex = 4;
			this->label5->Text = L"委託價";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(245, 18);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(53, 12);
			this->label4->TabIndex = 3;
			this->label4->Text = L"當沖與否";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(171, 18);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(53, 12);
			this->label3->TabIndex = 2;
			this->label3->Text = L"委託條件";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(103, 18);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(41, 12);
			this->label2->TabIndex = 1;
			this->label2->Text = L"買賣別";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(18, 18);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(53, 12);
			this->label1->TabIndex = 0;
			this->label1->Text = L"商品代碼";
			// 
			// FutureOrderControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->groupBox7);
			this->Controls->Add(this->groupBox6);
			this->Controls->Add(this->groupBox5);
			this->Controls->Add(this->groupBox3);
			this->Controls->Add(this->groupBox2);
			this->Controls->Add(this->groupBox4);
			this->Controls->Add(this->groupBox1);
			this->Name = L"FutureOrderControl";
			this->Size = System::Drawing::Size(849, 596);
			this->groupBox7->ResumeLayout(false);
			this->groupBox7->PerformLayout();
			this->groupBox6->ResumeLayout(false);
			this->groupBox6->PerformLayout();
			this->groupBox5->ResumeLayout(false);
			this->groupBox5->PerformLayout();
			this->groupBox3->ResumeLayout(false);
			this->groupBox3->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->groupBox4->ResumeLayout(false);
			this->groupBox4->PerformLayout();
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion


 
};
}
