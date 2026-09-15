#pragma once




namespace CppCLITester {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// StockOrderControl 的摘要
	/// </summary>
	public ref class StockOrderControl : public System::Windows::Forms::UserControl
	{
#pragma region 變數
	public :
		
		// 證券下單
		delegate void  OnOrderSignalHandler(System::String ^ m_UserID, bool bAsyncOrder, SKCOMLib::STOCKORDER pOrder);
		event OnOrderSignalHandler^ OnOrderSignal;

		//證券改量
		delegate void OnDecreaseSignalHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, int nDecreaseQty);
		event OnDecreaseSignalHandler^ OnDecreaseSignal;

		//證券刪單 - 商品代碼
		delegate void OnCancelByStockNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrStockNo);
		event OnCancelByStockNoHandler^ OnCancelByStockNo;

		//證券刪單 - 委託序號
		delegate void OnCancelBySeqNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo);
		event OnCancelBySeqNoHandler^ OnCancelBySeqNo;

		//證券刪單 - 委託書號
		delegate void OnCancelByBookNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrBookNo);
		event OnCancelByBookNoHandler^ OnCancelByBookNo;

		//證券改價 - 委託序號
		delegate void OnCorrectPriceBySeqNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrSeqNo, System::String^ bstrPrice, int  nTradeType);
		event OnCorrectPriceBySeqNoHandler^ OnCorrectPriceBySeqNo;

		//委託改價 - 委託序號
		delegate void OnCorrectPriceByBookNoHandler(System::String^ bstrLogInID, bool bAsyncOrder, System::String^ bstrAccount, System::String^ bstrMarketSymbol, System::String^ bstrBookNo, System::String^ bstrPrice, int  nTradeType);
		event OnCorrectPriceByBookNoHandler^ OnCorrectPriceByBookNo;

		//證券及時庫存查詢
		delegate void OnGetRealBalanceReportHandler(System::String^ bstrLogInID, System::String^ bstrAccount);
		event OnGetRealBalanceReportHandler^ OnGetRealBalanceReport;

		//證券庫存查詢
		delegate void OnGetBalanceQueryHandler(System::String^ bstrLogInID, System::String^ bstrAccount,System::String^ StockNo);
		event OnGetBalanceQueryHandler^ OnGetBalanceQuery;

		//資券配額查詢
		delegate void OnGetMarginPurchaseAmountLimitHandler(System::String^ bstrLogInID, System::String^ bstrAccount, System::String^ StockNo);
		event OnGetMarginPurchaseAmountLimitHandler^ OnGetMarginPurchaseAmountLimit;
		
		// 證券及時損益試算
		delegate void OnGetRequestProfitReportHandler(System::String^ bstrLogInID, System::String^ bstrAccount);
		event OnGetRequestProfitReportHandler^ OnGetRequestProfitReport;

		//未實現損益試算
		delegate void OnProfitGWReportSignalHandler(System::String^ bstrLogInID, SKCOMLib::TSPROFITLOSSGWQUERY pPLGWQuery);
		event OnProfitGWReportSignalHandler^ OnProfitGWReportSignal;

		//證券鈴鼓下單
		delegate void OnOddOrderSignalHandler(System::String^ bstrLogInID,bool  bAsyncOrder, SKCOMLib::STOCKORDER pOrder);
		event OnOddOrderSignalHandler^ OnOddOrderSignal;


		void set_UserAccount(System::String^ value)
		{
			m_UserAccount = value;
		}
		void set_UserID(System::String^ value)
		{
			m_UserID = value;
		}


	private :
		System::String^  m_UserAccount;
		System::String^ m_UserID;
#pragma endregion

#pragma region Methods
		   Void btnSendStockOrder_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnDecreaseQty_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnCancelOrder_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnCancelOrderBySeqNo_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnCancelOrderByBookNo_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnCorrectPriceBySeqNo_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnCorrectPriceByBookNo_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnGetRealBalanceReport_Click(System::Object^ sender, System::EventArgs^ e);
		   Void GetBalanceQueryReport_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnGetAmountLimit_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btnGetRequestProfitReport_Click(System::Object^ sender, System::EventArgs^ e);
		   Void btn_GetProfitLossGW_Click(System::Object^ sender, System::EventArgs^ e);
		   Void StockOddOrder_Click(System::Object^ sender, System::EventArgs^ e);
		   Void StockOddOrderAsync_Click(System::Object^ sender, System::EventArgs^ e);
#pragma endregion

	public:
		StockOrderControl(void)
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
		~StockOrderControl()
		{
			if (components)
			{
				delete components;
			}
		}



	private: System::Windows::Forms::GroupBox^ groupBox4;
	private: System::Windows::Forms::ComboBox^ MarketBox;
	private: System::Windows::Forms::Label^ label21;
	private: System::Windows::Forms::TextBox^ txtDecreaseQty;
	private: System::Windows::Forms::Label^ label13;
	private: System::Windows::Forms::Button^ btnDecreaseQty;
	private: System::Windows::Forms::TextBox^ txtDecreaseBookNo;
	private: System::Windows::Forms::Label^ label11;
	private: System::Windows::Forms::GroupBox^ groupBox8;
	private: System::Windows::Forms::ComboBox^ boxCorrectSymbol;
	private: System::Windows::Forms::Label^ label16;
	private: System::Windows::Forms::Button^ btnCorrectPriceByBookNo;
	private: System::Windows::Forms::TextBox^ txtCorrectBookNo;
	private: System::Windows::Forms::Label^ label15;
	private: System::Windows::Forms::Button^ btnCorrectPriceBySeqNo;
	private: System::Windows::Forms::ComboBox^ boxCorrectTradeType;
	private: System::Windows::Forms::Label^ label17;
	private: System::Windows::Forms::TextBox^ txtCorrectPrice;
	private: System::Windows::Forms::Label^ label18;
	private: System::Windows::Forms::TextBox^ txtCorrectSeqNo;
	private: System::Windows::Forms::Label^ label20;
	private: System::Windows::Forms::GroupBox^ groupBox7;
	private: System::Windows::Forms::TextBox^ txtBalanceQueryStockNo;
	private: System::Windows::Forms::Label^ label10;
	private: System::Windows::Forms::Button^ GetBalanceQueryReport;
	private: System::Windows::Forms::GroupBox^ groupBox6;
	private: System::Windows::Forms::TextBox^ txtAmountLimitStockNo;
	private: System::Windows::Forms::Label^ label12;
	private: System::Windows::Forms::Button^ btnGetAmountLimit;
	private: System::Windows::Forms::GroupBox^ groupBox5;
	private: System::Windows::Forms::TextBox^ txt_ProfitLossSeqNo;
	private: System::Windows::Forms::Label^ label29;
	private: System::Windows::Forms::TextBox^ txt_ProfitLossBookNo;
	private: System::Windows::Forms::Label^ label28;
	private: System::Windows::Forms::TextBox^ txt_ProfitLossYMEnd;
	private: System::Windows::Forms::Label^ label27;
	private: System::Windows::Forms::TextBox^ txt_ProfitLossYMStart;
	private: System::Windows::Forms::Label^ label26;
	private: System::Windows::Forms::Label^ label25;
	private: System::Windows::Forms::ComboBox^ box_TradeType;
	private: System::Windows::Forms::TextBox^ txt_ProfitLossStock;
	private: System::Windows::Forms::Label^ label24;
	private: System::Windows::Forms::Label^ label23;
	private: System::Windows::Forms::Label^ label22;
	private: System::Windows::Forms::Button^ btn_GetProfitLossGW;
	private: System::Windows::Forms::ComboBox^ box_QueryType;
	private: System::Windows::Forms::ComboBox^ box_format;
	private: System::Windows::Forms::Button^ btnGetRequestProfitReport;
	private: System::Windows::Forms::GroupBox^ groupBox3;
	private: System::Windows::Forms::Button^ btnGetRealBalanceReport;
	private: System::Windows::Forms::GroupBox^ groupBox2;
	private: System::Windows::Forms::Button^ btnCancelOrderByBookNo;
	private: System::Windows::Forms::Button^ btnCancelOrderBySeqNo;
	private: System::Windows::Forms::TextBox^ txtCancelBookNo;
	private: System::Windows::Forms::TextBox^ txtCancelSeqNo;
	private: System::Windows::Forms::Label^ label19;
	private: System::Windows::Forms::Label^ label14;
	private: System::Windows::Forms::Button^ btnCancelOrder;
	private: System::Windows::Forms::Label^ label30;
	private: System::Windows::Forms::TextBox^ txtCancelStockNo;
	private: System::Windows::Forms::CheckBox^ ckboxAsyn;
	private: System::Windows::Forms::CheckBox^ ckboxDecreaseAsyn;
	private: System::Windows::Forms::GroupBox^ groupBox9;
	private: System::Windows::Forms::ComboBox^ comboBox3;
	private: System::Windows::Forms::Label^ label31;
	private: System::Windows::Forms::Button^ StockOddOrderAsync;
	private: System::Windows::Forms::Button^ StockOddOrder;
	private: System::Windows::Forms::TextBox^ QtyOdd;
	private: System::Windows::Forms::TextBox^ PriceOdd;
	private: System::Windows::Forms::ComboBox^ comboBox4;
	private: System::Windows::Forms::ComboBox^ BoxOddPeriod;
	private: System::Windows::Forms::ComboBox^ BuySellBoxOdd;
	private: System::Windows::Forms::TextBox^ StockNoOdd;
	private: System::Windows::Forms::Label^ label33;
	private: System::Windows::Forms::Label^ label34;
	private: System::Windows::Forms::Label^ label35;
	private: System::Windows::Forms::Label^ label36;
	private: System::Windows::Forms::Label^ label37;
	private: System::Windows::Forms::Label^ label38;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Button^ btnSendStockOrder;
	private: System::Windows::Forms::GroupBox^ 下單;
	private: System::Windows::Forms::TextBox^ txtQty;
	private: System::Windows::Forms::Label^ label9;
	private: System::Windows::Forms::TextBox^ txtPrice;
	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::ComboBox^ boxSpecialTradeType;
	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::ComboBox^ boxCond;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::ComboBox^ boxFlag;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::ComboBox^ boxPeriod;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::ComboBox^ boxBidAsk;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::ComboBox^ boxPrime;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ txtStockNo;

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
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->btnSendStockOrder = (gcnew System::Windows::Forms::Button());
			this->下單 = (gcnew System::Windows::Forms::GroupBox());
			this->ckboxAsyn = (gcnew System::Windows::Forms::CheckBox());
			this->txtQty = (gcnew System::Windows::Forms::TextBox());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->txtPrice = (gcnew System::Windows::Forms::TextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->boxSpecialTradeType = (gcnew System::Windows::Forms::ComboBox());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->boxCond = (gcnew System::Windows::Forms::ComboBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->boxFlag = (gcnew System::Windows::Forms::ComboBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->boxPeriod = (gcnew System::Windows::Forms::ComboBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->boxBidAsk = (gcnew System::Windows::Forms::ComboBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->boxPrime = (gcnew System::Windows::Forms::ComboBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->txtStockNo = (gcnew System::Windows::Forms::TextBox());
			this->groupBox4 = (gcnew System::Windows::Forms::GroupBox());
			this->ckboxDecreaseAsyn = (gcnew System::Windows::Forms::CheckBox());
			this->MarketBox = (gcnew System::Windows::Forms::ComboBox());
			this->label21 = (gcnew System::Windows::Forms::Label());
			this->txtDecreaseQty = (gcnew System::Windows::Forms::TextBox());
			this->label13 = (gcnew System::Windows::Forms::Label());
			this->btnDecreaseQty = (gcnew System::Windows::Forms::Button());
			this->txtDecreaseBookNo = (gcnew System::Windows::Forms::TextBox());
			this->label11 = (gcnew System::Windows::Forms::Label());
			this->groupBox8 = (gcnew System::Windows::Forms::GroupBox());
			this->boxCorrectSymbol = (gcnew System::Windows::Forms::ComboBox());
			this->label16 = (gcnew System::Windows::Forms::Label());
			this->btnCorrectPriceByBookNo = (gcnew System::Windows::Forms::Button());
			this->txtCorrectBookNo = (gcnew System::Windows::Forms::TextBox());
			this->label15 = (gcnew System::Windows::Forms::Label());
			this->btnCorrectPriceBySeqNo = (gcnew System::Windows::Forms::Button());
			this->boxCorrectTradeType = (gcnew System::Windows::Forms::ComboBox());
			this->label17 = (gcnew System::Windows::Forms::Label());
			this->txtCorrectPrice = (gcnew System::Windows::Forms::TextBox());
			this->label18 = (gcnew System::Windows::Forms::Label());
			this->txtCorrectSeqNo = (gcnew System::Windows::Forms::TextBox());
			this->label20 = (gcnew System::Windows::Forms::Label());
			this->groupBox7 = (gcnew System::Windows::Forms::GroupBox());
			this->txtBalanceQueryStockNo = (gcnew System::Windows::Forms::TextBox());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->GetBalanceQueryReport = (gcnew System::Windows::Forms::Button());
			this->groupBox6 = (gcnew System::Windows::Forms::GroupBox());
			this->txtAmountLimitStockNo = (gcnew System::Windows::Forms::TextBox());
			this->label12 = (gcnew System::Windows::Forms::Label());
			this->btnGetAmountLimit = (gcnew System::Windows::Forms::Button());
			this->groupBox5 = (gcnew System::Windows::Forms::GroupBox());
			this->txt_ProfitLossSeqNo = (gcnew System::Windows::Forms::TextBox());
			this->label29 = (gcnew System::Windows::Forms::Label());
			this->txt_ProfitLossBookNo = (gcnew System::Windows::Forms::TextBox());
			this->label28 = (gcnew System::Windows::Forms::Label());
			this->txt_ProfitLossYMEnd = (gcnew System::Windows::Forms::TextBox());
			this->label27 = (gcnew System::Windows::Forms::Label());
			this->txt_ProfitLossYMStart = (gcnew System::Windows::Forms::TextBox());
			this->label26 = (gcnew System::Windows::Forms::Label());
			this->label25 = (gcnew System::Windows::Forms::Label());
			this->box_TradeType = (gcnew System::Windows::Forms::ComboBox());
			this->txt_ProfitLossStock = (gcnew System::Windows::Forms::TextBox());
			this->label24 = (gcnew System::Windows::Forms::Label());
			this->label23 = (gcnew System::Windows::Forms::Label());
			this->label22 = (gcnew System::Windows::Forms::Label());
			this->btn_GetProfitLossGW = (gcnew System::Windows::Forms::Button());
			this->box_QueryType = (gcnew System::Windows::Forms::ComboBox());
			this->box_format = (gcnew System::Windows::Forms::ComboBox());
			this->btnGetRequestProfitReport = (gcnew System::Windows::Forms::Button());
			this->groupBox3 = (gcnew System::Windows::Forms::GroupBox());
			this->btnGetRealBalanceReport = (gcnew System::Windows::Forms::Button());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->btnCancelOrderByBookNo = (gcnew System::Windows::Forms::Button());
			this->btnCancelOrderBySeqNo = (gcnew System::Windows::Forms::Button());
			this->txtCancelBookNo = (gcnew System::Windows::Forms::TextBox());
			this->txtCancelSeqNo = (gcnew System::Windows::Forms::TextBox());
			this->label19 = (gcnew System::Windows::Forms::Label());
			this->label14 = (gcnew System::Windows::Forms::Label());
			this->btnCancelOrder = (gcnew System::Windows::Forms::Button());
			this->label30 = (gcnew System::Windows::Forms::Label());
			this->txtCancelStockNo = (gcnew System::Windows::Forms::TextBox());
			this->groupBox9 = (gcnew System::Windows::Forms::GroupBox());
			this->comboBox3 = (gcnew System::Windows::Forms::ComboBox());
			this->label31 = (gcnew System::Windows::Forms::Label());
			this->StockOddOrderAsync = (gcnew System::Windows::Forms::Button());
			this->StockOddOrder = (gcnew System::Windows::Forms::Button());
			this->QtyOdd = (gcnew System::Windows::Forms::TextBox());
			this->PriceOdd = (gcnew System::Windows::Forms::TextBox());
			this->comboBox4 = (gcnew System::Windows::Forms::ComboBox());
			this->BoxOddPeriod = (gcnew System::Windows::Forms::ComboBox());
			this->BuySellBoxOdd = (gcnew System::Windows::Forms::ComboBox());
			this->StockNoOdd = (gcnew System::Windows::Forms::TextBox());
			this->label33 = (gcnew System::Windows::Forms::Label());
			this->label34 = (gcnew System::Windows::Forms::Label());
			this->label35 = (gcnew System::Windows::Forms::Label());
			this->label36 = (gcnew System::Windows::Forms::Label());
			this->label37 = (gcnew System::Windows::Forms::Label());
			this->label38 = (gcnew System::Windows::Forms::Label());
			this->下單->SuspendLayout();
			this->groupBox4->SuspendLayout();
			this->groupBox8->SuspendLayout();
			this->groupBox7->SuspendLayout();
			this->groupBox6->SuspendLayout();
			this->groupBox5->SuspendLayout();
			this->groupBox3->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->groupBox9->SuspendLayout();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(26, 34);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(53, 12);
			this->label1->TabIndex = 0;
			this->label1->Text = L"商品代碼";
			// 
			// btnSendStockOrder
			// 
			this->btnSendStockOrder->Location = System::Drawing::Point(739, 94);
			this->btnSendStockOrder->Name = L"btnSendStockOrder";
			this->btnSendStockOrder->Size = System::Drawing::Size(111, 23);
			this->btnSendStockOrder->TabIndex = 1;
			this->btnSendStockOrder->Text = L"SendStockOrder";
			this->btnSendStockOrder->UseVisualStyleBackColor = true;
			this->btnSendStockOrder->Click += gcnew System::EventHandler(this, &StockOrderControl::btnSendStockOrder_Click);
			// 
			// 下單
			// 
			this->下單->Controls->Add(this->ckboxAsyn);
			this->下單->Controls->Add(this->txtQty);
			this->下單->Controls->Add(this->btnSendStockOrder);
			this->下單->Controls->Add(this->label9);
			this->下單->Controls->Add(this->txtPrice);
			this->下單->Controls->Add(this->label8);
			this->下單->Controls->Add(this->boxSpecialTradeType);
			this->下單->Controls->Add(this->label7);
			this->下單->Controls->Add(this->boxCond);
			this->下單->Controls->Add(this->label6);
			this->下單->Controls->Add(this->boxFlag);
			this->下單->Controls->Add(this->label5);
			this->下單->Controls->Add(this->boxPeriod);
			this->下單->Controls->Add(this->label4);
			this->下單->Controls->Add(this->boxBidAsk);
			this->下單->Controls->Add(this->label3);
			this->下單->Controls->Add(this->boxPrime);
			this->下單->Controls->Add(this->label2);
			this->下單->Controls->Add(this->txtStockNo);
			this->下單->Controls->Add(this->label1);
			this->下單->Location = System::Drawing::Point(12, 21);
			this->下單->Name = L"下單";
			this->下單->Size = System::Drawing::Size(888, 123);
			this->下單->TabIndex = 2;
			this->下單->TabStop = false;
			this->下單->Text = L"證券下單";
			// 
			// ckboxAsyn
			// 
			this->ckboxAsyn->AutoSize = true;
			this->ckboxAsyn->Location = System::Drawing::Point(769, 54);
			this->ckboxAsyn->Name = L"ckboxAsyn";
			this->ckboxAsyn->Size = System::Drawing::Size(60, 16);
			this->ckboxAsyn->TabIndex = 18;
			this->ckboxAsyn->Text = L"非同步";
			this->ckboxAsyn->UseVisualStyleBackColor = true;
			// 
			// txtQty
			// 
			this->txtQty->Location = System::Drawing::Point(496, 54);
			this->txtQty->Name = L"txtQty";
			this->txtQty->Size = System::Drawing::Size(85, 22);
			this->txtQty->TabIndex = 17;
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(516, 30);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(41, 12);
			this->label9->TabIndex = 16;
			this->label9->Text = L"委託量";
			// 
			// txtPrice
			// 
			this->txtPrice->Location = System::Drawing::Point(405, 54);
			this->txtPrice->Name = L"txtPrice";
			this->txtPrice->Size = System::Drawing::Size(85, 22);
			this->txtPrice->TabIndex = 15;
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(425, 30);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(41, 12);
			this->label8->TabIndex = 14;
			this->label8->Text = L"委託價";
			// 
			// boxSpecialTradeType
			// 
			this->boxSpecialTradeType->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxSpecialTradeType->FormattingEnabled = true;
			this->boxSpecialTradeType->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L" ( 1市價 )", L" ( 2限價 )" });
			this->boxSpecialTradeType->Location = System::Drawing::Point(661, 54);
			this->boxSpecialTradeType->Name = L"boxSpecialTradeType";
			this->boxSpecialTradeType->Size = System::Drawing::Size(68, 20);
			this->boxSpecialTradeType->TabIndex = 13;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(673, 30);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(53, 12);
			this->label7->TabIndex = 12;
			this->label7->Text = L"委託類型";
			// 
			// boxCond
			// 
			this->boxCond->FormattingEnabled = true;
			this->boxCond->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"ROD", L"IOC", L"FOK" });
			this->boxCond->Location = System::Drawing::Point(587, 54);
			this->boxCond->Name = L"boxCond";
			this->boxCond->Size = System::Drawing::Size(68, 20);
			this->boxCond->TabIndex = 11;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(599, 30);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(53, 12);
			this->label6->TabIndex = 10;
			this->label6->Text = L"委託條件";
			// 
			// boxFlag
			// 
			this->boxFlag->FormattingEnabled = true;
			this->boxFlag->Items->AddRange(gcnew cli::array< System::Object^  >(4) { L"現股", L"融資", L"融券", L"無券" });
			this->boxFlag->Location = System::Drawing::Point(331, 56);
			this->boxFlag->Name = L"boxFlag";
			this->boxFlag->Size = System::Drawing::Size(68, 20);
			this->boxFlag->TabIndex = 9;
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(343, 32);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(53, 12);
			this->label5->TabIndex = 8;
			this->label5->Text = L"當沖與否";
			// 
			// boxPeriod
			// 
			this->boxPeriod->FormattingEnabled = true;
			this->boxPeriod->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"整股", L"盤後", L"零股" });
			this->boxPeriod->Location = System::Drawing::Point(254, 57);
			this->boxPeriod->Name = L"boxPeriod";
			this->boxPeriod->Size = System::Drawing::Size(68, 20);
			this->boxPeriod->TabIndex = 7;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(266, 33);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(53, 12);
			this->label4->TabIndex = 6;
			this->label4->Text = L"委託方式";
			// 
			// boxBidAsk
			// 
			this->boxBidAsk->FormattingEnabled = true;
			this->boxBidAsk->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"買", L"賣" });
			this->boxBidAsk->Location = System::Drawing::Point(181, 58);
			this->boxBidAsk->Name = L"boxBidAsk";
			this->boxBidAsk->Size = System::Drawing::Size(68, 20);
			this->boxBidAsk->TabIndex = 5;
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(193, 34);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(41, 12);
			this->label3->TabIndex = 4;
			this->label3->Text = L"買賣別";
			// 
			// boxPrime
			// 
			this->boxPrime->FormattingEnabled = true;
			this->boxPrime->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"上市櫃", L"興櫃" });
			this->boxPrime->Location = System::Drawing::Point(107, 58);
			this->boxPrime->Name = L"boxPrime";
			this->boxPrime->Size = System::Drawing::Size(68, 20);
			this->boxPrime->TabIndex = 3;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(119, 34);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(69, 12);
			this->label2->TabIndex = 2;
			this->label2->Text = L"上市櫃-興櫃";
			// 
			// txtStockNo
			// 
			this->txtStockNo->Location = System::Drawing::Point(6, 58);
			this->txtStockNo->Name = L"txtStockNo";
			this->txtStockNo->Size = System::Drawing::Size(85, 22);
			this->txtStockNo->TabIndex = 1;
			// 
			// groupBox4
			// 
			this->groupBox4->Controls->Add(this->ckboxDecreaseAsyn);
			this->groupBox4->Controls->Add(this->MarketBox);
			this->groupBox4->Controls->Add(this->label21);
			this->groupBox4->Controls->Add(this->txtDecreaseQty);
			this->groupBox4->Controls->Add(this->label13);
			this->groupBox4->Controls->Add(this->btnDecreaseQty);
			this->groupBox4->Controls->Add(this->txtDecreaseBookNo);
			this->groupBox4->Controls->Add(this->label11);
			this->groupBox4->Location = System::Drawing::Point(12, 150);
			this->groupBox4->Name = L"groupBox4";
			this->groupBox4->Size = System::Drawing::Size(888, 54);
			this->groupBox4->TabIndex = 6;
			this->groupBox4->TabStop = false;
			this->groupBox4->Text = L"委託減量";
			// 
			// ckboxDecreaseAsyn
			// 
			this->ckboxDecreaseAsyn->AutoSize = true;
			this->ckboxDecreaseAsyn->Location = System::Drawing::Point(602, 20);
			this->ckboxDecreaseAsyn->Name = L"ckboxDecreaseAsyn";
			this->ckboxDecreaseAsyn->Size = System::Drawing::Size(60, 16);
			this->ckboxDecreaseAsyn->TabIndex = 29;
			this->ckboxDecreaseAsyn->Text = L"非同步";
			this->ckboxDecreaseAsyn->UseVisualStyleBackColor = true;
			// 
			// MarketBox
			// 
			this->MarketBox->FormattingEnabled = true;
			this->MarketBox->Items->AddRange(gcnew cli::array< System::Object^  >(1) { L"TS" });
			this->MarketBox->Location = System::Drawing::Point(307, 18);
			this->MarketBox->Name = L"MarketBox";
			this->MarketBox->Size = System::Drawing::Size(69, 20);
			this->MarketBox->TabIndex = 28;
			this->MarketBox->Visible = false;
			// 
			// label21
			// 
			this->label21->AutoSize = true;
			this->label21->Location = System::Drawing::Point(248, 24);
			this->label21->Name = L"label21";
			this->label21->Size = System::Drawing::Size(53, 12);
			this->label21->TabIndex = 27;
			this->label21->Text = L"市場代碼";
			this->label21->Visible = false;
			// 
			// txtDecreaseQty
			// 
			this->txtDecreaseQty->Location = System::Drawing::Point(512, 18);
			this->txtDecreaseQty->Name = L"txtDecreaseQty";
			this->txtDecreaseQty->Size = System::Drawing::Size(72, 22);
			this->txtDecreaseQty->TabIndex = 10;
			// 
			// label13
			// 
			this->label13->AutoSize = true;
			this->label13->Location = System::Drawing::Point(397, 22);
			this->label13->Name = L"label13";
			this->label13->Size = System::Drawing::Size(104, 12);
			this->label13->TabIndex = 17;
			this->label13->Text = L" 輸入欲減少的數量";
			// 
			// btnDecreaseQty
			// 
			this->btnDecreaseQty->Location = System::Drawing::Point(678, 19);
			this->btnDecreaseQty->Name = L"btnDecreaseQty";
			this->btnDecreaseQty->Size = System::Drawing::Size(172, 23);
			this->btnDecreaseQty->TabIndex = 11;
			this->btnDecreaseQty->Text = L"Decrease Order By SeqNo";
			this->btnDecreaseQty->UseVisualStyleBackColor = true;
			this->btnDecreaseQty->Click += gcnew System::EventHandler(this, &StockOrderControl::btnDecreaseQty_Click);
			// 
			// txtDecreaseBookNo
			// 
			this->txtDecreaseBookNo->Location = System::Drawing::Point(103, 18);
			this->txtDecreaseBookNo->Name = L"txtDecreaseBookNo";
			this->txtDecreaseBookNo->Size = System::Drawing::Size(127, 22);
			this->txtDecreaseBookNo->TabIndex = 9;
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
			// groupBox8
			// 
			this->groupBox8->Controls->Add(this->boxCorrectSymbol);
			this->groupBox8->Controls->Add(this->label16);
			this->groupBox8->Controls->Add(this->btnCorrectPriceByBookNo);
			this->groupBox8->Controls->Add(this->txtCorrectBookNo);
			this->groupBox8->Controls->Add(this->label15);
			this->groupBox8->Controls->Add(this->btnCorrectPriceBySeqNo);
			this->groupBox8->Controls->Add(this->boxCorrectTradeType);
			this->groupBox8->Controls->Add(this->label17);
			this->groupBox8->Controls->Add(this->txtCorrectPrice);
			this->groupBox8->Controls->Add(this->label18);
			this->groupBox8->Controls->Add(this->txtCorrectSeqNo);
			this->groupBox8->Controls->Add(this->label20);
			this->groupBox8->Location = System::Drawing::Point(12, 334);
			this->groupBox8->Name = L"groupBox8";
			this->groupBox8->Size = System::Drawing::Size(888, 94);
			this->groupBox8->TabIndex = 17;
			this->groupBox8->TabStop = false;
			this->groupBox8->Text = L"改價";
			// 
			// boxCorrectSymbol
			// 
			this->boxCorrectSymbol->FormattingEnabled = true;
			this->boxCorrectSymbol->Items->AddRange(gcnew cli::array< System::Object^  >(1) { L"TS" });
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
			this->label16->Text = L"市場代碼";
			// 
			// btnCorrectPriceByBookNo
			// 
			this->btnCorrectPriceByBookNo->Location = System::Drawing::Point(602, 61);
			this->btnCorrectPriceByBookNo->Name = L"btnCorrectPriceByBookNo";
			this->btnCorrectPriceByBookNo->Size = System::Drawing::Size(190, 27);
			this->btnCorrectPriceByBookNo->TabIndex = 24;
			this->btnCorrectPriceByBookNo->Text = L"CorrectPriceByBookNo";
			this->btnCorrectPriceByBookNo->UseVisualStyleBackColor = true;
			this->btnCorrectPriceByBookNo->Click += gcnew System::EventHandler(this, &StockOrderControl::btnCorrectPriceByBookNo_Click);
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
			this->btnCorrectPriceBySeqNo->Click += gcnew System::EventHandler(this, &StockOrderControl::btnCorrectPriceBySeqNo_Click);
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
			// label17
			// 
			this->label17->AutoSize = true;
			this->label17->Location = System::Drawing::Point(470, 46);
			this->label17->Name = L"label17";
			this->label17->Size = System::Drawing::Size(53, 12);
			this->label17->TabIndex = 19;
			this->label17->Text = L"委託條件";
			// 
			// txtCorrectPrice
			// 
			this->txtCorrectPrice->Location = System::Drawing::Point(390, 43);
			this->txtCorrectPrice->Name = L"txtCorrectPrice";
			this->txtCorrectPrice->Size = System::Drawing::Size(74, 22);
			this->txtCorrectPrice->TabIndex = 18;
			// 
			// label18
			// 
			this->label18->AutoSize = true;
			this->label18->Location = System::Drawing::Point(331, 46);
			this->label18->Name = L"label18";
			this->label18->Size = System::Drawing::Size(53, 12);
			this->label18->TabIndex = 17;
			this->label18->Text = L"修改價格";
			// 
			// txtCorrectSeqNo
			// 
			this->txtCorrectSeqNo->Location = System::Drawing::Point(101, 21);
			this->txtCorrectSeqNo->Name = L"txtCorrectSeqNo";
			this->txtCorrectSeqNo->Size = System::Drawing::Size(136, 22);
			this->txtCorrectSeqNo->TabIndex = 15;
			// 
			// label20
			// 
			this->label20->AutoSize = true;
			this->label20->Location = System::Drawing::Point(14, 27);
			this->label20->Name = L"label20";
			this->label20->Size = System::Drawing::Size(53, 12);
			this->label20->TabIndex = 16;
			this->label20->Text = L"委託序號";
			// 
			// groupBox7
			// 
			this->groupBox7->Controls->Add(this->txtBalanceQueryStockNo);
			this->groupBox7->Controls->Add(this->label10);
			this->groupBox7->Controls->Add(this->GetBalanceQueryReport);
			this->groupBox7->Location = System::Drawing::Point(18, 631);
			this->groupBox7->Name = L"groupBox7";
			this->groupBox7->Size = System::Drawing::Size(407, 66);
			this->groupBox7->TabIndex = 16;
			this->groupBox7->TabStop = false;
			this->groupBox7->Text = L"證券集保庫存";
			// 
			// txtBalanceQueryStockNo
			// 
			this->txtBalanceQueryStockNo->Location = System::Drawing::Point(65, 22);
			this->txtBalanceQueryStockNo->Name = L"txtBalanceQueryStockNo";
			this->txtBalanceQueryStockNo->Size = System::Drawing::Size(100, 22);
			this->txtBalanceQueryStockNo->TabIndex = 4;
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->Location = System::Drawing::Point(6, 29);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(53, 12);
			this->label10->TabIndex = 3;
			this->label10->Text = L"商品代碼";
			// 
			// GetBalanceQueryReport
			// 
			this->GetBalanceQueryReport->Location = System::Drawing::Point(174, 21);
			this->GetBalanceQueryReport->Name = L"GetBalanceQueryReport";
			this->GetBalanceQueryReport->Size = System::Drawing::Size(179, 29);
			this->GetBalanceQueryReport->TabIndex = 0;
			this->GetBalanceQueryReport->Text = L"GetBalanceQueryReport";
			this->GetBalanceQueryReport->UseVisualStyleBackColor = true;
			this->GetBalanceQueryReport->Click += gcnew System::EventHandler(this, &StockOrderControl::GetBalanceQueryReport_Click);
			// 
			// groupBox6
			// 
			this->groupBox6->Controls->Add(this->txtAmountLimitStockNo);
			this->groupBox6->Controls->Add(this->label12);
			this->groupBox6->Controls->Add(this->btnGetAmountLimit);
			this->groupBox6->Location = System::Drawing::Point(456, 631);
			this->groupBox6->Name = L"groupBox6";
			this->groupBox6->Size = System::Drawing::Size(444, 66);
			this->groupBox6->TabIndex = 15;
			this->groupBox6->TabStop = false;
			this->groupBox6->Text = L"資券配額";
			// 
			// txtAmountLimitStockNo
			// 
			this->txtAmountLimitStockNo->Location = System::Drawing::Point(72, 21);
			this->txtAmountLimitStockNo->Name = L"txtAmountLimitStockNo";
			this->txtAmountLimitStockNo->Size = System::Drawing::Size(100, 22);
			this->txtAmountLimitStockNo->TabIndex = 2;
			// 
			// label12
			// 
			this->label12->AutoSize = true;
			this->label12->Location = System::Drawing::Point(13, 28);
			this->label12->Name = L"label12";
			this->label12->Size = System::Drawing::Size(53, 12);
			this->label12->TabIndex = 1;
			this->label12->Text = L"商品代碼";
			// 
			// btnGetAmountLimit
			// 
			this->btnGetAmountLimit->Location = System::Drawing::Point(184, 20);
			this->btnGetAmountLimit->Name = L"btnGetAmountLimit";
			this->btnGetAmountLimit->Size = System::Drawing::Size(179, 29);
			this->btnGetAmountLimit->TabIndex = 0;
			this->btnGetAmountLimit->Text = L"GetAmountLimit";
			this->btnGetAmountLimit->UseVisualStyleBackColor = true;
			this->btnGetAmountLimit->Click += gcnew System::EventHandler(this, &StockOrderControl::btnGetAmountLimit_Click);
			// 
			// groupBox5
			// 
			this->groupBox5->Controls->Add(this->txt_ProfitLossSeqNo);
			this->groupBox5->Controls->Add(this->label29);
			this->groupBox5->Controls->Add(this->txt_ProfitLossBookNo);
			this->groupBox5->Controls->Add(this->label28);
			this->groupBox5->Controls->Add(this->txt_ProfitLossYMEnd);
			this->groupBox5->Controls->Add(this->label27);
			this->groupBox5->Controls->Add(this->txt_ProfitLossYMStart);
			this->groupBox5->Controls->Add(this->label26);
			this->groupBox5->Controls->Add(this->label25);
			this->groupBox5->Controls->Add(this->box_TradeType);
			this->groupBox5->Controls->Add(this->txt_ProfitLossStock);
			this->groupBox5->Controls->Add(this->label24);
			this->groupBox5->Controls->Add(this->label23);
			this->groupBox5->Controls->Add(this->label22);
			this->groupBox5->Controls->Add(this->btn_GetProfitLossGW);
			this->groupBox5->Controls->Add(this->box_QueryType);
			this->groupBox5->Controls->Add(this->box_format);
			this->groupBox5->Controls->Add(this->btnGetRequestProfitReport);
			this->groupBox5->Location = System::Drawing::Point(224, 536);
			this->groupBox5->Name = L"groupBox5";
			this->groupBox5->Size = System::Drawing::Size(676, 89);
			this->groupBox5->TabIndex = 14;
			this->groupBox5->TabStop = false;
			this->groupBox5->Text = L"證券即時損益試算";
			// 
			// txt_ProfitLossSeqNo
			// 
			this->txt_ProfitLossSeqNo->Location = System::Drawing::Point(571, 41);
			this->txt_ProfitLossSeqNo->MaxLength = 13;
			this->txt_ProfitLossSeqNo->Name = L"txt_ProfitLossSeqNo";
			this->txt_ProfitLossSeqNo->Size = System::Drawing::Size(99, 22);
			this->txt_ProfitLossSeqNo->TabIndex = 37;
			// 
			// label29
			// 
			this->label29->AutoSize = true;
			this->label29->Location = System::Drawing::Point(517, 46);
			this->label29->Name = L"label29";
			this->label29->Size = System::Drawing::Size(53, 12);
			this->label29->TabIndex = 36;
			this->label29->Text = L"委託序號";
			// 
			// txt_ProfitLossBookNo
			// 
			this->txt_ProfitLossBookNo->Location = System::Drawing::Point(452, 41);
			this->txt_ProfitLossBookNo->MaxLength = 8;
			this->txt_ProfitLossBookNo->Name = L"txt_ProfitLossBookNo";
			this->txt_ProfitLossBookNo->Size = System::Drawing::Size(53, 22);
			this->txt_ProfitLossBookNo->TabIndex = 35;
			// 
			// label28
			// 
			this->label28->AutoSize = true;
			this->label28->Location = System::Drawing::Point(393, 46);
			this->label28->Name = L"label28";
			this->label28->Size = System::Drawing::Size(53, 12);
			this->label28->TabIndex = 34;
			this->label28->Text = L"委託書號";
			// 
			// txt_ProfitLossYMEnd
			// 
			this->txt_ProfitLossYMEnd->Location = System::Drawing::Point(313, 39);
			this->txt_ProfitLossYMEnd->Name = L"txt_ProfitLossYMEnd";
			this->txt_ProfitLossYMEnd->Size = System::Drawing::Size(74, 22);
			this->txt_ProfitLossYMEnd->TabIndex = 33;
			this->txt_ProfitLossYMEnd->Text = L"20210910";
			// 
			// label27
			// 
			this->label27->AutoSize = true;
			this->label27->Location = System::Drawing::Point(266, 44);
			this->label27->Name = L"label27";
			this->label27->Size = System::Drawing::Size(41, 12);
			this->label27->TabIndex = 32;
			this->label27->Text = L"結束日";
			// 
			// txt_ProfitLossYMStart
			// 
			this->txt_ProfitLossYMStart->Location = System::Drawing::Point(186, 39);
			this->txt_ProfitLossYMStart->Name = L"txt_ProfitLossYMStart";
			this->txt_ProfitLossYMStart->Size = System::Drawing::Size(74, 22);
			this->txt_ProfitLossYMStart->TabIndex = 31;
			this->txt_ProfitLossYMStart->Text = L"20210901";
			// 
			// label26
			// 
			this->label26->AutoSize = true;
			this->label26->Location = System::Drawing::Point(145, 44);
			this->label26->Name = L"label26";
			this->label26->Size = System::Drawing::Size(41, 12);
			this->label26->TabIndex = 30;
			this->label26->Text = L"起始日";
			// 
			// label25
			// 
			this->label25->AutoSize = true;
			this->label25->Location = System::Drawing::Point(541, 16);
			this->label25->Name = L"label25";
			this->label25->Size = System::Drawing::Size(41, 12);
			this->label25->TabIndex = 29;
			this->label25->Text = L"交易別";
			// 
			// box_TradeType
			// 
			this->box_TradeType->FormattingEnabled = true;
			this->box_TradeType->Items->AddRange(gcnew cli::array< System::Object^  >(10) {
				L"0:現股", L"1:融資(代)", L"2:融券(代)", L"3:融資(自)",
					L"4:融券(自)", L"5:無券", L"6:現沖", L"8:券差", L"9:無券賣出", L"空值:全部"
			});
			this->box_TradeType->Location = System::Drawing::Point(588, 13);
			this->box_TradeType->Name = L"box_TradeType";
			this->box_TradeType->Size = System::Drawing::Size(64, 20);
			this->box_TradeType->TabIndex = 28;
			// 
			// txt_ProfitLossStock
			// 
			this->txt_ProfitLossStock->Location = System::Drawing::Point(471, 13);
			this->txt_ProfitLossStock->MaxLength = 8;
			this->txt_ProfitLossStock->Name = L"txt_ProfitLossStock";
			this->txt_ProfitLossStock->Size = System::Drawing::Size(64, 22);
			this->txt_ProfitLossStock->TabIndex = 27;
			// 
			// label24
			// 
			this->label24->AutoSize = true;
			this->label24->Location = System::Drawing::Point(412, 18);
			this->label24->Name = L"label24";
			this->label24->Size = System::Drawing::Size(53, 12);
			this->label24->TabIndex = 26;
			this->label24->Text = L"商品代碼";
			// 
			// label23
			// 
			this->label23->AutoSize = true;
			this->label23->Location = System::Drawing::Point(272, 16);
			this->label23->Name = L"label23";
			this->label23->Size = System::Drawing::Size(65, 12);
			this->label23->TabIndex = 25;
			this->label23->Text = L"彙總或明細";
			// 
			// label22
			// 
			this->label22->AutoSize = true;
			this->label22->Location = System::Drawing::Point(144, 16);
			this->label22->Name = L"label22";
			this->label22->Size = System::Drawing::Size(53, 12);
			this->label22->TabIndex = 24;
			this->label22->Text = L"損益類型";
			// 
			// btn_GetProfitLossGW
			// 
			this->btn_GetProfitLossGW->Location = System::Drawing::Point(7, 53);
			this->btn_GetProfitLossGW->Name = L"btn_GetProfitLossGW";
			this->btn_GetProfitLossGW->Size = System::Drawing::Size(127, 29);
			this->btn_GetProfitLossGW->TabIndex = 23;
			this->btn_GetProfitLossGW->Text = L"GetProfitLossGW(新)";
			this->btn_GetProfitLossGW->UseVisualStyleBackColor = true;
			this->btn_GetProfitLossGW->Click += gcnew System::EventHandler(this, &StockOrderControl::btn_GetProfitLossGW_Click);
			// 
			// box_QueryType
			// 
			this->box_QueryType->FormattingEnabled = true;
			this->box_QueryType->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"未實現", L"已實現", L"現股當沖損益" });
			this->box_QueryType->Location = System::Drawing::Point(202, 13);
			this->box_QueryType->Name = L"box_QueryType";
			this->box_QueryType->Size = System::Drawing::Size(64, 20);
			this->box_QueryType->TabIndex = 22;
			// 
			// box_format
			// 
			this->box_format->FormattingEnabled = true;
			this->box_format->Items->AddRange(gcnew cli::array< System::Object^  >(4) { L"0:Summary", L"1:Detail", L"2:(實現)投資總額", L"3:(實現)彙總BY股票代號" });
			this->box_format->Location = System::Drawing::Point(342, 13);
			this->box_format->Name = L"box_format";
			this->box_format->Size = System::Drawing::Size(64, 20);
			this->box_format->TabIndex = 21;
			// 
			// btnGetRequestProfitReport
			// 
			this->btnGetRequestProfitReport->Location = System::Drawing::Point(7, 18);
			this->btnGetRequestProfitReport->Name = L"btnGetRequestProfitReport";
			this->btnGetRequestProfitReport->Size = System::Drawing::Size(127, 29);
			this->btnGetRequestProfitReport->TabIndex = 0;
			this->btnGetRequestProfitReport->Text = L"GetRequestProfitReport";
			this->btnGetRequestProfitReport->UseVisualStyleBackColor = true;
			this->btnGetRequestProfitReport->Click += gcnew System::EventHandler(this, &StockOrderControl::btnGetRequestProfitReport_Click);
			// 
			// groupBox3
			// 
			this->groupBox3->Controls->Add(this->btnGetRealBalanceReport);
			this->groupBox3->Location = System::Drawing::Point(16, 548);
			this->groupBox3->Name = L"groupBox3";
			this->groupBox3->Size = System::Drawing::Size(202, 66);
			this->groupBox3->TabIndex = 13;
			this->groupBox3->TabStop = false;
			this->groupBox3->Text = L"證券即時庫存";
			// 
			// btnGetRealBalanceReport
			// 
			this->btnGetRealBalanceReport->Location = System::Drawing::Point(6, 21);
			this->btnGetRealBalanceReport->Name = L"btnGetRealBalanceReport";
			this->btnGetRealBalanceReport->Size = System::Drawing::Size(179, 29);
			this->btnGetRealBalanceReport->TabIndex = 0;
			this->btnGetRealBalanceReport->Text = L"GetRealBalanceReport";
			this->btnGetRealBalanceReport->UseVisualStyleBackColor = true;
			this->btnGetRealBalanceReport->Click += gcnew System::EventHandler(this, &StockOrderControl::btnGetRealBalanceReport_Click);
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->btnCancelOrderByBookNo);
			this->groupBox2->Controls->Add(this->btnCancelOrderBySeqNo);
			this->groupBox2->Controls->Add(this->txtCancelBookNo);
			this->groupBox2->Controls->Add(this->txtCancelSeqNo);
			this->groupBox2->Controls->Add(this->label19);
			this->groupBox2->Controls->Add(this->label14);
			this->groupBox2->Controls->Add(this->btnCancelOrder);
			this->groupBox2->Controls->Add(this->label30);
			this->groupBox2->Controls->Add(this->txtCancelStockNo);
			this->groupBox2->Location = System::Drawing::Point(12, 228);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(888, 88);
			this->groupBox2->TabIndex = 12;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"取消委託";
			// 
			// btnCancelOrderByBookNo
			// 
			this->btnCancelOrderByBookNo->Location = System::Drawing::Point(630, 47);
			this->btnCancelOrderByBookNo->Name = L"btnCancelOrderByBookNo";
			this->btnCancelOrderByBookNo->Size = System::Drawing::Size(162, 23);
			this->btnCancelOrderByBookNo->TabIndex = 13;
			this->btnCancelOrderByBookNo->Text = L"Cancel Order By BookNo";
			this->btnCancelOrderByBookNo->UseVisualStyleBackColor = true;
			this->btnCancelOrderByBookNo->Click += gcnew System::EventHandler(this, &StockOrderControl::btnCancelOrderByBookNo_Click);
			// 
			// btnCancelOrderBySeqNo
			// 
			this->btnCancelOrderBySeqNo->Location = System::Drawing::Point(245, 48);
			this->btnCancelOrderBySeqNo->Name = L"btnCancelOrderBySeqNo";
			this->btnCancelOrderBySeqNo->Size = System::Drawing::Size(178, 23);
			this->btnCancelOrderBySeqNo->TabIndex = 5;
			this->btnCancelOrderBySeqNo->Text = L"Cancel Order By SeqNo";
			this->btnCancelOrderBySeqNo->UseVisualStyleBackColor = true;
			this->btnCancelOrderBySeqNo->Click += gcnew System::EventHandler(this, &StockOrderControl::btnCancelOrderBySeqNo_Click);
			// 
			// txtCancelBookNo
			// 
			this->txtCancelBookNo->Location = System::Drawing::Point(488, 47);
			this->txtCancelBookNo->Name = L"txtCancelBookNo";
			this->txtCancelBookNo->Size = System::Drawing::Size(136, 22);
			this->txtCancelBookNo->TabIndex = 12;
			// 
			// txtCancelSeqNo
			// 
			this->txtCancelSeqNo->Location = System::Drawing::Point(103, 51);
			this->txtCancelSeqNo->Name = L"txtCancelSeqNo";
			this->txtCancelSeqNo->Size = System::Drawing::Size(136, 22);
			this->txtCancelSeqNo->TabIndex = 4;
			// 
			// label19
			// 
			this->label19->AutoSize = true;
			this->label19->Location = System::Drawing::Point(429, 53);
			this->label19->Name = L"label19";
			this->label19->Size = System::Drawing::Size(53, 12);
			this->label19->TabIndex = 11;
			this->label19->Text = L"委託書號";
			// 
			// label14
			// 
			this->label14->AutoSize = true;
			this->label14->Location = System::Drawing::Point(16, 61);
			this->label14->Name = L"label14";
			this->label14->Size = System::Drawing::Size(53, 12);
			this->label14->TabIndex = 3;
			this->label14->Text = L"委託序號";
			// 
			// btnCancelOrder
			// 
			this->btnCancelOrder->Location = System::Drawing::Point(245, 19);
			this->btnCancelOrder->Name = L"btnCancelOrder";
			this->btnCancelOrder->Size = System::Drawing::Size(178, 23);
			this->btnCancelOrder->TabIndex = 2;
			this->btnCancelOrder->Text = L"Cancel Order By StockNo";
			this->btnCancelOrder->UseVisualStyleBackColor = true;
			this->btnCancelOrder->Click += gcnew System::EventHandler(this, &StockOrderControl::btnCancelOrder_Click);
			// 
			// label30
			// 
			this->label30->AutoSize = true;
			this->label30->Location = System::Drawing::Point(16, 30);
			this->label30->Name = L"label30";
			this->label30->Size = System::Drawing::Size(53, 12);
			this->label30->TabIndex = 1;
			this->label30->Text = L"商品代碼";
			// 
			// txtCancelStockNo
			// 
			this->txtCancelStockNo->Location = System::Drawing::Point(103, 20);
			this->txtCancelStockNo->Name = L"txtCancelStockNo";
			this->txtCancelStockNo->Size = System::Drawing::Size(136, 22);
			this->txtCancelStockNo->TabIndex = 0;
			// 
			// groupBox9
			// 
			this->groupBox9->Controls->Add(this->comboBox3);
			this->groupBox9->Controls->Add(this->label31);
			this->groupBox9->Controls->Add(this->StockOddOrderAsync);
			this->groupBox9->Controls->Add(this->StockOddOrder);
			this->groupBox9->Controls->Add(this->QtyOdd);
			this->groupBox9->Controls->Add(this->PriceOdd);
			this->groupBox9->Controls->Add(this->comboBox4);
			this->groupBox9->Controls->Add(this->BoxOddPeriod);
			this->groupBox9->Controls->Add(this->BuySellBoxOdd);
			this->groupBox9->Controls->Add(this->StockNoOdd);
			this->groupBox9->Controls->Add(this->label33);
			this->groupBox9->Controls->Add(this->label34);
			this->groupBox9->Controls->Add(this->label35);
			this->groupBox9->Controls->Add(this->label36);
			this->groupBox9->Controls->Add(this->label37);
			this->groupBox9->Controls->Add(this->label38);
			this->groupBox9->Location = System::Drawing::Point(12, 434);
			this->groupBox9->Name = L"groupBox9";
			this->groupBox9->Size = System::Drawing::Size(888, 80);
			this->groupBox9->TabIndex = 18;
			this->groupBox9->TabStop = false;
			this->groupBox9->Text = L"證券盤中零股";
			// 
			// comboBox3
			// 
			this->comboBox3->FormattingEnabled = true;
			this->comboBox3->Items->AddRange(gcnew cli::array< System::Object^  >(1) { L"上市櫃" });
			this->comboBox3->Location = System::Drawing::Point(423, 43);
			this->comboBox3->Name = L"comboBox3";
			this->comboBox3->Size = System::Drawing::Size(69, 20);
			this->comboBox3->TabIndex = 10;
			this->comboBox3->Visible = false;
			// 
			// label31
			// 
			this->label31->AutoSize = true;
			this->label31->Location = System::Drawing::Point(423, 21);
			this->label31->Name = L"label31";
			this->label31->Size = System::Drawing::Size(69, 12);
			this->label31->TabIndex = 9;
			this->label31->Text = L"上市櫃-興櫃";
			this->label31->Visible = false;
			// 
			// StockOddOrderAsync
			// 
			this->StockOddOrderAsync->Location = System::Drawing::Point(635, 47);
			this->StockOddOrderAsync->Name = L"StockOddOrderAsync";
			this->StockOddOrderAsync->Size = System::Drawing::Size(190, 23);
			this->StockOddOrderAsync->TabIndex = 8;
			this->StockOddOrderAsync->Text = L"SendStockOddOrderAsync";
			this->StockOddOrderAsync->UseVisualStyleBackColor = true;
			this->StockOddOrderAsync->Click += gcnew System::EventHandler(this, &StockOrderControl::StockOddOrderAsync_Click);
			// 
			// StockOddOrder
			// 
			this->StockOddOrder->Location = System::Drawing::Point(635, 18);
			this->StockOddOrder->Name = L"StockOddOrder";
			this->StockOddOrder->Size = System::Drawing::Size(190, 23);
			this->StockOddOrder->TabIndex = 7;
			this->StockOddOrder->Text = L"SendStockOddOrder";
			this->StockOddOrder->UseVisualStyleBackColor = true;
			this->StockOddOrder->Click += gcnew System::EventHandler(this, &StockOrderControl::StockOddOrder_Click);
			// 
			// QtyOdd
			// 
			this->QtyOdd->Location = System::Drawing::Point(364, 43);
			this->QtyOdd->Name = L"QtyOdd";
			this->QtyOdd->Size = System::Drawing::Size(49, 22);
			this->QtyOdd->TabIndex = 6;
			// 
			// PriceOdd
			// 
			this->PriceOdd->Location = System::Drawing::Point(284, 43);
			this->PriceOdd->Name = L"PriceOdd";
			this->PriceOdd->Size = System::Drawing::Size(74, 22);
			this->PriceOdd->TabIndex = 5;
			// 
			// comboBox4
			// 
			this->comboBox4->FormattingEnabled = true;
			this->comboBox4->Items->AddRange(gcnew cli::array< System::Object^  >(1) { L"現股" });
			this->comboBox4->Location = System::Drawing::Point(509, 43);
			this->comboBox4->Name = L"comboBox4";
			this->comboBox4->Size = System::Drawing::Size(64, 20);
			this->comboBox4->TabIndex = 4;
			this->comboBox4->Visible = false;
			// 
			// BoxOddPeriod
			// 
			this->BoxOddPeriod->FormattingEnabled = true;
			this->BoxOddPeriod->Items->AddRange(gcnew cli::array< System::Object^  >(1) { L"盤中零股" });
			this->BoxOddPeriod->Location = System::Drawing::Point(144, 45);
			this->BoxOddPeriod->Name = L"BoxOddPeriod";
			this->BoxOddPeriod->Size = System::Drawing::Size(64, 20);
			this->BoxOddPeriod->TabIndex = 3;
			// 
			// BuySellBoxOdd
			// 
			this->BuySellBoxOdd->FormattingEnabled = true;
			this->BuySellBoxOdd->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"買", L"賣" });
			this->BuySellBoxOdd->Location = System::Drawing::Point(86, 45);
			this->BuySellBoxOdd->Name = L"BuySellBoxOdd";
			this->BuySellBoxOdd->Size = System::Drawing::Size(49, 20);
			this->BuySellBoxOdd->TabIndex = 2;
			// 
			// StockNoOdd
			// 
			this->StockNoOdd->Location = System::Drawing::Point(19, 45);
			this->StockNoOdd->MaxLength = 8;
			this->StockNoOdd->Name = L"StockNoOdd";
			this->StockNoOdd->Size = System::Drawing::Size(64, 22);
			this->StockNoOdd->TabIndex = 1;
			// 
			// label33
			// 
			this->label33->AutoSize = true;
			this->label33->Location = System::Drawing::Point(362, 23);
			this->label33->Name = L"label33";
			this->label33->Size = System::Drawing::Size(41, 12);
			this->label33->TabIndex = 5;
			this->label33->Text = L"委託量";
			// 
			// label34
			// 
			this->label34->AutoSize = true;
			this->label34->Location = System::Drawing::Point(282, 23);
			this->label34->Name = L"label34";
			this->label34->Size = System::Drawing::Size(41, 12);
			this->label34->TabIndex = 4;
			this->label34->Text = L"委託價";
			// 
			// label35
			// 
			this->label35->AutoSize = true;
			this->label35->Location = System::Drawing::Point(507, 23);
			this->label35->Name = L"label35";
			this->label35->Size = System::Drawing::Size(29, 12);
			this->label35->TabIndex = 3;
			this->label35->Text = L"現股";
			this->label35->Visible = false;
			// 
			// label36
			// 
			this->label36->AutoSize = true;
			this->label36->Location = System::Drawing::Point(142, 23);
			this->label36->Name = L"label36";
			this->label36->Size = System::Drawing::Size(53, 12);
			this->label36->TabIndex = 2;
			this->label36->Text = L"委託方式";
			// 
			// label37
			// 
			this->label37->AutoSize = true;
			this->label37->Location = System::Drawing::Point(86, 23);
			this->label37->Name = L"label37";
			this->label37->Size = System::Drawing::Size(41, 12);
			this->label37->TabIndex = 1;
			this->label37->Text = L"買賣別";
			// 
			// label38
			// 
			this->label38->AutoSize = true;
			this->label38->Location = System::Drawing::Point(19, 23);
			this->label38->Name = L"label38";
			this->label38->Size = System::Drawing::Size(53, 12);
			this->label38->TabIndex = 0;
			this->label38->Text = L"商品代碼";
			// 
			// StockOrderControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->groupBox9);
			this->Controls->Add(this->groupBox8);
			this->Controls->Add(this->groupBox7);
			this->Controls->Add(this->groupBox6);
			this->Controls->Add(this->groupBox5);
			this->Controls->Add(this->groupBox3);
			this->Controls->Add(this->groupBox2);
			this->Controls->Add(this->groupBox4);
			this->Controls->Add(this->下單);
			this->Name = L"StockOrderControl";
			this->Size = System::Drawing::Size(913, 711);
			this->下單->ResumeLayout(false);
			this->下單->PerformLayout();
			this->groupBox4->ResumeLayout(false);
			this->groupBox4->PerformLayout();
			this->groupBox8->ResumeLayout(false);
			this->groupBox8->PerformLayout();
			this->groupBox7->ResumeLayout(false);
			this->groupBox7->PerformLayout();
			this->groupBox6->ResumeLayout(false);
			this->groupBox6->PerformLayout();
			this->groupBox5->ResumeLayout(false);
			this->groupBox5->PerformLayout();
			this->groupBox3->ResumeLayout(false);
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->groupBox9->ResumeLayout(false);
			this->groupBox9->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion


		 


};
 
}
