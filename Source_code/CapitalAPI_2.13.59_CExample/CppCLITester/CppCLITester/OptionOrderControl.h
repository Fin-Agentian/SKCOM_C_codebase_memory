#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace CppCLITester {

	/// <summary>
	/// OptionOrderControl 的摘要
	/// </summary>
	public ref class OptionOrderControl : public System::Windows::Forms::UserControl
	{
#pragma region 變數
	public :
		//選擇權委託
		delegate void OnOptionOrderSignalHandler(System::String^ bstrLogInID, bool bAsyncOrder, SKCOMLib::FUTUREORDER pOrder);
		event OnOptionOrderSignalHandler^ OnOptionOrderSignal;

		void get_UserAccount(System::String^ value)
		{
			m_UserAccount = value;
		}

		void get_UserID(System::String^ value)
		{
			m_UserID = value;
		}

	private:
		System::String^ m_UserAccount;
		System::String^ m_UserID;

#pragma endregion

#pragma region Methods
		Void btnSendOptionOrder_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnSendOptionOrderAsync_Click(System::Object^ sender, System::EventArgs^ e);
#pragma endregion

	public:
		OptionOrderControl(void)
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
		~OptionOrderControl()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::GroupBox^ groupBox1;
	protected:
	private: System::Windows::Forms::ComboBox^ boxReserved;
	private: System::Windows::Forms::Label^ label18;
	private: System::Windows::Forms::Button^ btnSendOptionOrderAsync;
	private: System::Windows::Forms::Button^ btnSendOptionOrder;
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
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->boxReserved = (gcnew System::Windows::Forms::ComboBox());
			this->label18 = (gcnew System::Windows::Forms::Label());
			this->btnSendOptionOrderAsync = (gcnew System::Windows::Forms::Button());
			this->btnSendOptionOrder = (gcnew System::Windows::Forms::Button());
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
			this->groupBox1->SuspendLayout();
			this->SuspendLayout();
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->boxReserved);
			this->groupBox1->Controls->Add(this->label18);
			this->groupBox1->Controls->Add(this->btnSendOptionOrderAsync);
			this->groupBox1->Controls->Add(this->btnSendOptionOrder);
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
			this->groupBox1->Location = System::Drawing::Point(20, 20);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(800, 90);
			this->groupBox1->TabIndex = 5;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"選擇權委託";
			// 
			// boxReserved
			// 
			this->boxReserved->FormattingEnabled = true;
			this->boxReserved->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"盤中", L"T盤預約" });
			this->boxReserved->Location = System::Drawing::Point(557, 45);
			this->boxReserved->Name = L"boxReserved";
			this->boxReserved->Size = System::Drawing::Size(83, 20);
			this->boxReserved->TabIndex = 21;
			// 
			// label18
			// 
			this->label18->AutoSize = true;
			this->label18->Location = System::Drawing::Point(568, 21);
			this->label18->Name = L"label18";
			this->label18->Size = System::Drawing::Size(29, 12);
			this->label18->TabIndex = 20;
			this->label18->Text = L"盤別";
			// 
			// btnSendOptionOrderAsync
			// 
			this->btnSendOptionOrderAsync->Location = System::Drawing::Point(661, 43);
			this->btnSendOptionOrderAsync->Name = L"btnSendOptionOrderAsync";
			this->btnSendOptionOrderAsync->Size = System::Drawing::Size(124, 23);
			this->btnSendOptionOrderAsync->TabIndex = 13;
			this->btnSendOptionOrderAsync->Text = L"SendOptionOrderAsync";
			this->btnSendOptionOrderAsync->UseVisualStyleBackColor = true;
			this->btnSendOptionOrderAsync->Click += gcnew System::EventHandler(this, &OptionOrderControl::btnSendOptionOrderAsync_Click);
			// 
			// btnSendOptionOrder
			// 
			this->btnSendOptionOrder->Location = System::Drawing::Point(661, 16);
			this->btnSendOptionOrder->Name = L"btnSendOptionOrder";
			this->btnSendOptionOrder->Size = System::Drawing::Size(124, 23);
			this->btnSendOptionOrder->TabIndex = 12;
			this->btnSendOptionOrder->Text = L"SendOptionOrder";
			this->btnSendOptionOrder->UseVisualStyleBackColor = true;
			this->btnSendOptionOrder->Click += gcnew System::EventHandler(this, &OptionOrderControl::btnSendOptionOrder_Click);
			// 
			// txtQty
			// 
			this->txtQty->Location = System::Drawing::Point(502, 45);
			this->txtQty->Name = L"txtQty";
			this->txtQty->Size = System::Drawing::Size(49, 22);
			this->txtQty->TabIndex = 11;
			// 
			// txtPrice
			// 
			this->txtPrice->Location = System::Drawing::Point(399, 45);
			this->txtPrice->Name = L"txtPrice";
			this->txtPrice->Size = System::Drawing::Size(74, 22);
			this->txtPrice->TabIndex = 10;
			// 
			// boxFlag
			// 
			this->boxFlag->FormattingEnabled = true;
			this->boxFlag->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"新倉", L"平倉", L"自動" });
			this->boxFlag->Location = System::Drawing::Point(303, 45);
			this->boxFlag->Name = L"boxFlag";
			this->boxFlag->Size = System::Drawing::Size(68, 20);
			this->boxFlag->TabIndex = 9;
			// 
			// boxPeriod
			// 
			this->boxPeriod->FormattingEnabled = true;
			this->boxPeriod->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"ROD", L"IOC", L"FOK" });
			this->boxPeriod->Location = System::Drawing::Point(204, 43);
			this->boxPeriod->Name = L"boxPeriod";
			this->boxPeriod->Size = System::Drawing::Size(64, 20);
			this->boxPeriod->TabIndex = 8;
			// 
			// boxBidAsk
			// 
			this->boxBidAsk->FormattingEnabled = true;
			this->boxBidAsk->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"買", L"賣" });
			this->boxBidAsk->Location = System::Drawing::Point(129, 45);
			this->boxBidAsk->Name = L"boxBidAsk";
			this->boxBidAsk->Size = System::Drawing::Size(49, 20);
			this->boxBidAsk->TabIndex = 7;
			// 
			// txtStockNo
			// 
			this->txtStockNo->Location = System::Drawing::Point(19, 45);
			this->txtStockNo->MaxLength = 20;
			this->txtStockNo->Name = L"txtStockNo";
			this->txtStockNo->Size = System::Drawing::Size(93, 22);
			this->txtStockNo->TabIndex = 6;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(501, 21);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(41, 12);
			this->label6->TabIndex = 5;
			this->label6->Text = L"委託量";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(411, 21);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(41, 12);
			this->label5->TabIndex = 4;
			this->label5->Text = L"委託價";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(315, 21);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(29, 12);
			this->label4->TabIndex = 3;
			this->label4->Text = L"倉別";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(202, 21);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(53, 12);
			this->label3->TabIndex = 2;
			this->label3->Text = L"委託條件";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(127, 21);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(41, 12);
			this->label2->TabIndex = 1;
			this->label2->Text = L"買賣別";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(39, 21);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(53, 12);
			this->label1->TabIndex = 0;
			this->label1->Text = L"商品代碼";
			// 
			// OptionOrderControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->groupBox1);
			this->Name = L"OptionOrderControl";
			this->Size = System::Drawing::Size(849, 596);
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion



};
}
