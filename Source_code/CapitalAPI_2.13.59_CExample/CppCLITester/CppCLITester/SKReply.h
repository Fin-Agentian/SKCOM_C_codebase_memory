#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace CppCLITester {

	/// <summary>
	/// SKReply 的摘要
	/// </summary>
	public ref class SKReply : public System::Windows::Forms::UserControl
	{
#pragma region 變數



	public:
		// 
		delegate void GetMessageHandler(String^ strType, int nCode, String^ strMessage);
		event GetMessageHandler^ GetMessage;

		delegate void WriteMessageHandler(System::String^ strMessage);
		event WriteMessageHandler^ WriteMessage;

		void get_SKReplyObj(SKCOMLib::SKReplyLibClass^ value)
		{
			m_pSKReply = value;
		}

		void get_UserID(System::String^ value)
		{
			m_UserID = value;
		}

	private:
		SKCOMLib::SKReplyLibClass^ m_pSKReply;
		System::String^ m_UserID;
		bool m_first = true;
		DataTable^ myDataTable;
		int DataIndex = 0;
		System::Collections::Generic::List<String^>^ Stringarray = gcnew System::Collections::Generic::List<String^>();
#pragma endregion

#pragma region Methods
	private:
		Void btnConnect_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnDisconnect_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnIsConnected_Click(System::Object^ sender, System::EventArgs^ e);
		Void comboBox1_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e);

		//event
		Void OnConnect(System::String^ bstrUserID, int nErrorCode);
		Void OnDisconnect(System::String^ bstrUserID, int nErrorCode);
		Void OnComplete(System::String^ bstrUserID);
		Void OnNewData(System::String^ bstrUserID, System::String^ bstrMessage);

		// custom
		Void ShowDetail(System::String^ type);
		Void InitialDataGridView(System::String^ type);

#pragma endregion

	
	public:
		SKReply(void)
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
		~SKReply()
		{
			if (components)
			{
				delete components;
			}
		}

	protected:
	private: System::Windows::Forms::Button^ btnDisconnect;
	private: System::Windows::Forms::Button^ btnConnect;
	private: System::Windows::Forms::Label^ lblSignal;
	private: System::Windows::Forms::Label^ ConnectedLabel;
	private: System::Windows::Forms::Button^ btnIsConnected;
	private: System::Windows::Forms::GroupBox^ groupBox8;
	private: System::Windows::Forms::Label^ lblSignalReplySolace;

	private: System::Windows::Forms::DataGridView^ dataGridView1;
	private: System::Windows::Forms::ComboBox^ comboBox1;


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
			this->btnDisconnect = (gcnew System::Windows::Forms::Button());
			this->btnConnect = (gcnew System::Windows::Forms::Button());
			this->lblSignal = (gcnew System::Windows::Forms::Label());
			this->ConnectedLabel = (gcnew System::Windows::Forms::Label());
			this->btnIsConnected = (gcnew System::Windows::Forms::Button());
			this->groupBox8 = (gcnew System::Windows::Forms::GroupBox());
			this->lblSignalReplySolace = (gcnew System::Windows::Forms::Label());
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->comboBox1 = (gcnew System::Windows::Forms::ComboBox());
			this->groupBox8->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->SuspendLayout();
			// 
			// btnDisconnect
			// 
			this->btnDisconnect->Location = System::Drawing::Point(122, 22);
			this->btnDisconnect->Name = L"btnDisconnect";
			this->btnDisconnect->Size = System::Drawing::Size(81, 37);
			this->btnDisconnect->TabIndex = 66;
			this->btnDisconnect->Text = L"Disconnect";
			this->btnDisconnect->UseVisualStyleBackColor = true;
			this->btnDisconnect->Click += gcnew System::EventHandler(this, &SKReply::btnDisconnect_Click);
			// 
			// btnConnect
			// 
			this->btnConnect->Location = System::Drawing::Point(35, 21);
			this->btnConnect->Name = L"btnConnect";
			this->btnConnect->Size = System::Drawing::Size(81, 37);
			this->btnConnect->TabIndex = 65;
			this->btnConnect->Text = L"Connect";
			this->btnConnect->UseVisualStyleBackColor = true;
			this->btnConnect->Click += gcnew System::EventHandler(this, &SKReply::btnConnect_Click);
			// 
			// lblSignal
			// 
			this->lblSignal->AutoSize = true;
			this->lblSignal->Font = (gcnew System::Drawing::Font(L"新細明體", 16, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblSignal->ForeColor = System::Drawing::Color::Red;
			this->lblSignal->Location = System::Drawing::Point(30, 21);
			this->lblSignal->Name = L"lblSignal";
			this->lblSignal->Size = System::Drawing::Size(32, 22);
			this->lblSignal->TabIndex = 64;
			this->lblSignal->Text = L"●";
			// 
			// ConnectedLabel
			// 
			this->ConnectedLabel->AutoSize = true;
			this->ConnectedLabel->Location = System::Drawing::Point(660, 32);
			this->ConnectedLabel->Name = L"ConnectedLabel";
			this->ConnectedLabel->Size = System::Drawing::Size(11, 12);
			this->ConnectedLabel->TabIndex = 70;
			this->ConnectedLabel->Text = L"0";
			// 
			// btnIsConnected
			// 
			this->btnIsConnected->Location = System::Drawing::Point(565, 20);
			this->btnIsConnected->Name = L"btnIsConnected";
			this->btnIsConnected->Size = System::Drawing::Size(75, 29);
			this->btnIsConnected->TabIndex = 69;
			this->btnIsConnected->Text = L"IsConnected";
			this->btnIsConnected->UseVisualStyleBackColor = true;
			this->btnIsConnected->Click += gcnew System::EventHandler(this, &SKReply::btnIsConnected_Click);
			// 
			// groupBox8
			// 
			this->groupBox8->Controls->Add(this->lblSignalReplySolace);
			this->groupBox8->Location = System::Drawing::Point(356, 13);
			this->groupBox8->Name = L"groupBox8";
			this->groupBox8->Size = System::Drawing::Size(126, 46);
			this->groupBox8->TabIndex = 67;
			this->groupBox8->TabStop = false;
			this->groupBox8->Text = L"Connect Status";
			// 
			// lblSignalReplySolace
			// 
			this->lblSignalReplySolace->AutoSize = true;
			this->lblSignalReplySolace->Font = (gcnew System::Drawing::Font(L"新細明體", 16, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblSignalReplySolace->ForeColor = System::Drawing::Color::Red;
			this->lblSignalReplySolace->Location = System::Drawing::Point(15, 18);
			this->lblSignalReplySolace->Name = L"lblSignalReplySolace";
			this->lblSignalReplySolace->Size = System::Drawing::Size(32, 22);
			this->lblSignalReplySolace->TabIndex = 0;
			this->lblSignalReplySolace->Text = L"●";
			// 
			// dataGridView1
			// 
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Location = System::Drawing::Point(34, 97);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->RowTemplate->Height = 24;
			this->dataGridView1->Size = System::Drawing::Size(742, 532);
			this->dataGridView1->TabIndex = 71;
			// 
			// comboBox1
			// 
			this->comboBox1->FormattingEnabled = true;
			this->comboBox1->Items->AddRange(gcnew cli::array< System::Object^  >(8) {
				L"全部", L"成交", L"委託", L"取消", L"改量", L"改價", L"改量改價",
					L"動態退單"
			});
			this->comboBox1->Location = System::Drawing::Point(35, 71);
			this->comboBox1->Name = L"comboBox1";
			this->comboBox1->Size = System::Drawing::Size(167, 20);
			this->comboBox1->TabIndex = 72;
			this->comboBox1->SelectedIndexChanged += gcnew System::EventHandler(this, &SKReply::comboBox1_SelectedIndexChanged);
			// 
			// SKReply
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->comboBox1);
			this->Controls->Add(this->dataGridView1);
			this->Controls->Add(this->btnDisconnect);
			this->Controls->Add(this->btnConnect);
			this->Controls->Add(this->lblSignal);
			this->Controls->Add(this->ConnectedLabel);
			this->Controls->Add(this->btnIsConnected);
			this->Controls->Add(this->groupBox8);
			this->Name = L"SKReply";
			this->Size = System::Drawing::Size(792, 659);
			this->groupBox8->ResumeLayout(false);
			this->groupBox8->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion


};
}
