#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace CppCLITester {

	/// <summary>
	/// SKQuote 的摘要
	/// </summary>
	public ref class SKQuote : public System::Windows::Forms::UserControl
	{
#pragma region 變數


	public:
		delegate void GetMessageHandler(System::String^ strType, int nCode, System::String^ strMessage);
		event GetMessageHandler^ GetMessage;

		delegate void WriteMessageHandler(System::String^ strMessage);
		event WriteMessageHandler^ WriteMessage;

		void get_SKQuoteObj(SKCOMLib::SKQuoteLibClass^ value)
		{
			m_pSKQuote = value;
		}

	private:
		SKCOMLib::SKQuoteLibClass^ m_pSKQuote;
		int kMarketPrice;
		DataTable^ m_dtStocks;
		DataTable^ m_dBest5Ask;
		DataTable^ m_dBest5Bid;
		int m_nSimulateStock;
		bool m_first = true;
		int m_nCount = 0;

#pragma endregion
	
#pragma region Methods
		// Methods
		Void button1_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnDisconnect_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnQueryStocks_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnIsConnected_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnTicks_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnLiveTick_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnTickStop_Click(System::Object^ sender, System::EventArgs^ e);
		Void RequestStockListBtn_Click(System::Object^ sender, System::EventArgs^ e);
		Void GetStrikePrices_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnKLine_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnKLineAM_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnKLineAMByDate_Click(System::Object^ sender, System::EventArgs^ e);
		Void Btn_RequestFutureTradeInfo_Click(System::Object^ sender, System::EventArgs^ e);
		Void btn_cancelFTI_Click(System::Object^ sender, System::EventArgs^ e);
		Void button4_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetBool_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCancelBool_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetMACD_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnCancelMACD_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetTick_Click(System::Object^ sender, System::EventArgs^ e);
		Void btnGetBest5_Click(System::Object^ sender, System::EventArgs^ e);

		// event
		Void OnConnection(int nKind, int nCode);
		Void OnNotifyQuoteLONG(short sMarketNo, int nStockIdx);
		Void OnNotifyBest5LONG(short sMarketNo, int nStockidx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate);
		Void OnNotifyHistoryTicksLONG(short sMarketNo, int nIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate);
		Void OnNotifyTicksLONG(short sMarketNo, int nIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate);
		Void OnNotifyKLineData(System::String^ bstrStockNo, System::String^ bstrData);
		Void OnNotifyMarketTot(short sMarketNo, short sPtr, int nTime, int nTotv, int nTots, int nTotc);
		Void OnNotifyMarketBuySell(short sMarketNo, short sPtr, int nTime, int nBc, int nSc, int nBs, int nSs);
		Void OnNotifyMarketHighLow(short sMarketNo, short sPtr, int nTime, short sUp, short sDown, short sHigh, short sLow, short sNoChange);
		Void OnNotifyBoolTunelLONG(short sMarketNo, int nStockidx, System::String^ bstrAVG, System::String^ bstrUBT, System::String^ bstrLBT);
		Void OnNotifyMACDLONG(short sMarketNo, int nStockidx, System::String^ bstrMACD, System::String^ bstrDIF, System::String^ bstrOSC);
		Void OnNotifyFutureTradeInfoLONG(System::String^ bstrStockNo, short sMarketNo, int nStockidx, int nBuyTotalCount, int nSellTotalCount, int nBuyTotalQty, int nSellTotalQty, int nBuyDealTotalCount, int nSellDealTotalCount);
		Void OnNotifyStrikePrices(System::String^ bstrOptionData);
		Void OnNotifyStockList(short sMarketNo, System::String^ bstrStockData);

		//custom
		Void OnUpDateDataRow(SKCOMLib::SKSTOCKLONG pStock);
		DataTable^ CreateStocksDataTable();
		DataTable^ CreateBest5AskTable();

#pragma endregion

		
	public:
		SKQuote(void)
		{
			m_dtStocks = CreateStocksDataTable();
			m_dBest5Ask = CreateBest5AskTable();
			m_dBest5Bid = CreateBest5AskTable();
			
			InitializeComponent();
			//
			//TODO:  在此加入建構函式程式碼
			//
		}

	protected:
		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		~SKQuote()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ btnDisconnect;
	protected:
	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::Label^ lblSignal;
	private: System::Windows::Forms::Label^ ConnectedLabel;
	private: System::Windows::Forms::Button^ btnIsConnected;
	private: System::Windows::Forms::GroupBox^ groupBox1;
	private: System::Windows::Forms::TabControl^ tabControl1;
	private: System::Windows::Forms::TabPage^ tabPage1;
	private: System::Windows::Forms::Button^ btnCancelStocks;

	private: System::Windows::Forms::TextBox^ txtPageNo;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Button^ btnQueryStocks;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ txtStocks;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::TabPage^ tabPage2;
	private: System::Windows::Forms::DataGridView^ dataGridView1;
	private: System::Windows::Forms::TextBox^ txtTickPageNo;
	private: System::Windows::Forms::Label^ label45;
	private: System::Windows::Forms::Button^ btnLiveTick;
	private: System::Windows::Forms::DataGridView^ GridBest5Bid;
	private: System::Windows::Forms::DataGridView^ GridBest5Ask;
	private: System::Windows::Forms::Button^ btnTickStop;
	private: System::Windows::Forms::ListBox^ listTicks;
	private: System::Windows::Forms::Button^ btnTicks;
	private: System::Windows::Forms::CheckBox^ chkBoxSimulate;
	private: System::Windows::Forms::CheckBox^ Box_M;
	private: System::Windows::Forms::CheckBox^ chkbox_msms;
	private: System::Windows::Forms::TabPage^ tabPage3;
	private: System::Windows::Forms::TabPage^ tabPage4;
	private: System::Windows::Forms::TabPage^ tabPage5;
	private: System::Windows::Forms::ListBox^ listKLine;
	private: System::Windows::Forms::Label^ label47;
	private: System::Windows::Forms::Button^ btnKLineAM;
	private: System::Windows::Forms::ComboBox^ boxOutType;
	private: System::Windows::Forms::ComboBox^ boxTradeSession;
	private: System::Windows::Forms::Label^ label46;
	private: System::Windows::Forms::Button^ btnKLine;
	private: System::Windows::Forms::TextBox^ txtKLine;
	private: System::Windows::Forms::TextBox^ txtStartDate;
	private: System::Windows::Forms::Button^ btnKLineAMByDate;

	private: System::Windows::Forms::TextBox^ txtEndDate;
	private: System::Windows::Forms::TabPage^ tabPage6;
	private: System::Windows::Forms::TabPage^ tabPage7;
	private: System::Windows::Forms::TabPage^ tabPage8;
	private: System::Windows::Forms::Button^ button4;
	private: System::Windows::Forms::Label^ label13;
	private: System::Windows::Forms::Label^ label9;
	private: System::Windows::Forms::TableLayoutPanel^ tableLayoutPanel2;
	private: System::Windows::Forms::Label^ lblsNoChange2;
	private: System::Windows::Forms::Label^ lblsDown2;
	private: System::Windows::Forms::Label^ lblsLow2;
	private: System::Windows::Forms::Label^ lblsUp2;
	private: System::Windows::Forms::Label^ lblsHigh2;
	private: System::Windows::Forms::Label^ label21;
	private: System::Windows::Forms::Label^ label22;
	private: System::Windows::Forms::Label^ label23;
	private: System::Windows::Forms::Label^ label24;
	private: System::Windows::Forms::Label^ label25;
	private: System::Windows::Forms::Label^ lbllSs2;
	private: System::Windows::Forms::Label^ lbllSc2;
	private: System::Windows::Forms::Label^ lbllBs2;
	private: System::Windows::Forms::Label^ lbllBc2;
	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::Label^ lblTotc2;
	private: System::Windows::Forms::Label^ lblTots2;
	private: System::Windows::Forms::Label^ label10;
	private: System::Windows::Forms::Label^ label12;
	private: System::Windows::Forms::Label^ lblTotv2;
	private: System::Windows::Forms::TableLayoutPanel^ tableLayoutPanel1;
	private: System::Windows::Forms::Label^ lblsNoChange;
	private: System::Windows::Forms::Label^ lblsDown;
	private: System::Windows::Forms::Label^ lblsLow;
	private: System::Windows::Forms::Label^ lblsUp;
	private: System::Windows::Forms::Label^ label20;
	private: System::Windows::Forms::Label^ lblsHigh;
	private: System::Windows::Forms::Label^ label17;
	private: System::Windows::Forms::Label^ label18;
	private: System::Windows::Forms::Label^ label15;
	private: System::Windows::Forms::Label^ lbllSs;
	private: System::Windows::Forms::Label^ lbllSc;
	private: System::Windows::Forms::Label^ lbllBs;
	private: System::Windows::Forms::Label^ lbllBc;
	private: System::Windows::Forms::Label^ label14;
	private: System::Windows::Forms::Label^ lblTotc;
	private: System::Windows::Forms::Label^ lblTots;
	private: System::Windows::Forms::Label^ label11;
	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::Label^ lblTotv;
	private: System::Windows::Forms::Label^ label19;
	private: System::Windows::Forms::Label^ label34;
	private: System::Windows::Forms::GroupBox^ groupBox6;
	private: System::Windows::Forms::Button^ btnCancelMACD;
	private: System::Windows::Forms::Label^ lblOSC;
	private: System::Windows::Forms::Label^ lblDIF;
	private: System::Windows::Forms::Label^ lblMACD;
	private: System::Windows::Forms::Label^ label29;
	private: System::Windows::Forms::Label^ label30;
	private: System::Windows::Forms::TextBox^ textMACD;
	private: System::Windows::Forms::Label^ label31;
	private: System::Windows::Forms::Label^ label32;
	private: System::Windows::Forms::Button^ btnGetMACD;
	private: System::Windows::Forms::Label^ label33;
	private: System::Windows::Forms::GroupBox^ groupBox2;
	private: System::Windows::Forms::Button^ btnCancelBool;
	private: System::Windows::Forms::Label^ lblLBT;
	private: System::Windows::Forms::Label^ lblUBT;
	private: System::Windows::Forms::Label^ lblAVG;
	private: System::Windows::Forms::Label^ label28;
	private: System::Windows::Forms::Label^ label27;
	private: System::Windows::Forms::TextBox^ textBool;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label16;
	private: System::Windows::Forms::Button^ btnGetBool;
	private: System::Windows::Forms::Label^ label26;
	private: System::Windows::Forms::Button^ btn_cancelFTI;
	private: System::Windows::Forms::Button^ Btn_RequestFutureTradeInfo;
	private: System::Windows::Forms::TextBox^ text_StockNo;
	private: System::Windows::Forms::Label^ label35;
	private: System::Windows::Forms::TableLayoutPanel^ tableLayoutPanel3;
	private: System::Windows::Forms::Label^ lblStockIdx;
	private: System::Windows::Forms::Label^ lblMarketNo;
	private: System::Windows::Forms::Label^ label44;
	private: System::Windows::Forms::Label^ label43;
	private: System::Windows::Forms::Label^ label40;
	private: System::Windows::Forms::Label^ lblFTIBDC;
	private: System::Windows::Forms::Label^ label41;
	private: System::Windows::Forms::Label^ lblFTISDC;
	private: System::Windows::Forms::Label^ lblFTISq;
	private: System::Windows::Forms::Label^ label39;
	private: System::Windows::Forms::Label^ lblFTISc;
	private: System::Windows::Forms::Label^ label37;
	private: System::Windows::Forms::Label^ lblFTIBq;
	private: System::Windows::Forms::Label^ label38;
	private: System::Windows::Forms::Label^ label36;
	private: System::Windows::Forms::Label^ lblFTIBc;
	private: System::Windows::Forms::Label^ txt_StrikePriceCount;
	private: System::Windows::Forms::ListBox^ listStrikePrices;
	private: System::Windows::Forms::GroupBox^ groupBox7;
	private: System::Windows::Forms::Button^ GetStrikePrices;
	private: System::Windows::Forms::Label^ label48;
	private: System::Windows::Forms::Label^ label42;
	private: System::Windows::Forms::Button^ RequestStockListBtn;
	private: System::Windows::Forms::TextBox^ MarketNo_txt;
	private: System::Windows::Forms::ListBox^ StockList;
	private: System::Windows::Forms::ComboBox^ boxKLine;
	private: System::Windows::Forms::GroupBox^ groupBox5;
	private: System::Windows::Forms::TextBox^ txtBestMarket;
	private: System::Windows::Forms::Label^ lblBest5Ask;
	private: System::Windows::Forms::Label^ lblBest5Bid;
	private: System::Windows::Forms::Button^ btnGetBest5;
	private: System::Windows::Forms::TextBox^ txtBestStockidx;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::GroupBox^ groupBox4;
	private: System::Windows::Forms::TextBox^ txtTickMarket;
	private: System::Windows::Forms::Label^ lblGetTick;
	private: System::Windows::Forms::Button^ btnGetTick;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::TextBox^ txtTickPtr;
	private: System::Windows::Forms::TextBox^ txtTickStockidx;
	private: System::Windows::Forms::TextBox^ txtMinuteNumber;
	private: System::Windows::Forms::Label^ label59;
	private: System::Windows::Forms::TextBox^ txtTick;

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
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->lblSignal = (gcnew System::Windows::Forms::Label());
			this->ConnectedLabel = (gcnew System::Windows::Forms::Label());
			this->btnIsConnected = (gcnew System::Windows::Forms::Button());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->btnCancelStocks = (gcnew System::Windows::Forms::Button());
			this->txtPageNo = (gcnew System::Windows::Forms::TextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->btnQueryStocks = (gcnew System::Windows::Forms::Button());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->txtStocks = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->tabPage2 = (gcnew System::Windows::Forms::TabPage());
			this->groupBox5 = (gcnew System::Windows::Forms::GroupBox());
			this->txtBestMarket = (gcnew System::Windows::Forms::TextBox());
			this->lblBest5Ask = (gcnew System::Windows::Forms::Label());
			this->lblBest5Bid = (gcnew System::Windows::Forms::Label());
			this->btnGetBest5 = (gcnew System::Windows::Forms::Button());
			this->txtBestStockidx = (gcnew System::Windows::Forms::TextBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->groupBox4 = (gcnew System::Windows::Forms::GroupBox());
			this->txtTickMarket = (gcnew System::Windows::Forms::TextBox());
			this->lblGetTick = (gcnew System::Windows::Forms::Label());
			this->btnGetTick = (gcnew System::Windows::Forms::Button());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->txtTickPtr = (gcnew System::Windows::Forms::TextBox());
			this->txtTickStockidx = (gcnew System::Windows::Forms::TextBox());
			this->chkBoxSimulate = (gcnew System::Windows::Forms::CheckBox());
			this->Box_M = (gcnew System::Windows::Forms::CheckBox());
			this->chkbox_msms = (gcnew System::Windows::Forms::CheckBox());
			this->txtTickPageNo = (gcnew System::Windows::Forms::TextBox());
			this->label45 = (gcnew System::Windows::Forms::Label());
			this->btnLiveTick = (gcnew System::Windows::Forms::Button());
			this->GridBest5Bid = (gcnew System::Windows::Forms::DataGridView());
			this->GridBest5Ask = (gcnew System::Windows::Forms::DataGridView());
			this->btnTickStop = (gcnew System::Windows::Forms::Button());
			this->listTicks = (gcnew System::Windows::Forms::ListBox());
			this->btnTicks = (gcnew System::Windows::Forms::Button());
			this->txtTick = (gcnew System::Windows::Forms::TextBox());
			this->tabPage3 = (gcnew System::Windows::Forms::TabPage());
			this->listKLine = (gcnew System::Windows::Forms::ListBox());
			this->label47 = (gcnew System::Windows::Forms::Label());
			this->btnKLineAM = (gcnew System::Windows::Forms::Button());
			this->boxOutType = (gcnew System::Windows::Forms::ComboBox());
			this->boxTradeSession = (gcnew System::Windows::Forms::ComboBox());
			this->label46 = (gcnew System::Windows::Forms::Label());
			this->btnKLine = (gcnew System::Windows::Forms::Button());
			this->txtKLine = (gcnew System::Windows::Forms::TextBox());
			this->txtStartDate = (gcnew System::Windows::Forms::TextBox());
			this->btnKLineAMByDate = (gcnew System::Windows::Forms::Button());
			this->boxKLine = (gcnew System::Windows::Forms::ComboBox());
			this->txtEndDate = (gcnew System::Windows::Forms::TextBox());
			this->tabPage4 = (gcnew System::Windows::Forms::TabPage());
			this->button4 = (gcnew System::Windows::Forms::Button());
			this->label13 = (gcnew System::Windows::Forms::Label());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->tableLayoutPanel2 = (gcnew System::Windows::Forms::TableLayoutPanel());
			this->lblsNoChange2 = (gcnew System::Windows::Forms::Label());
			this->lblsDown2 = (gcnew System::Windows::Forms::Label());
			this->lblsLow2 = (gcnew System::Windows::Forms::Label());
			this->lblsUp2 = (gcnew System::Windows::Forms::Label());
			this->lblsHigh2 = (gcnew System::Windows::Forms::Label());
			this->label21 = (gcnew System::Windows::Forms::Label());
			this->label22 = (gcnew System::Windows::Forms::Label());
			this->label23 = (gcnew System::Windows::Forms::Label());
			this->label24 = (gcnew System::Windows::Forms::Label());
			this->label25 = (gcnew System::Windows::Forms::Label());
			this->lbllSs2 = (gcnew System::Windows::Forms::Label());
			this->lbllSc2 = (gcnew System::Windows::Forms::Label());
			this->lbllBs2 = (gcnew System::Windows::Forms::Label());
			this->lbllBc2 = (gcnew System::Windows::Forms::Label());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->lblTotc2 = (gcnew System::Windows::Forms::Label());
			this->lblTots2 = (gcnew System::Windows::Forms::Label());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->label12 = (gcnew System::Windows::Forms::Label());
			this->lblTotv2 = (gcnew System::Windows::Forms::Label());
			this->tableLayoutPanel1 = (gcnew System::Windows::Forms::TableLayoutPanel());
			this->lblsNoChange = (gcnew System::Windows::Forms::Label());
			this->lblsDown = (gcnew System::Windows::Forms::Label());
			this->lblsLow = (gcnew System::Windows::Forms::Label());
			this->lblsUp = (gcnew System::Windows::Forms::Label());
			this->label20 = (gcnew System::Windows::Forms::Label());
			this->lblsHigh = (gcnew System::Windows::Forms::Label());
			this->label17 = (gcnew System::Windows::Forms::Label());
			this->label18 = (gcnew System::Windows::Forms::Label());
			this->label15 = (gcnew System::Windows::Forms::Label());
			this->lbllSs = (gcnew System::Windows::Forms::Label());
			this->lbllSc = (gcnew System::Windows::Forms::Label());
			this->lbllBs = (gcnew System::Windows::Forms::Label());
			this->lbllBc = (gcnew System::Windows::Forms::Label());
			this->label14 = (gcnew System::Windows::Forms::Label());
			this->lblTotc = (gcnew System::Windows::Forms::Label());
			this->lblTots = (gcnew System::Windows::Forms::Label());
			this->label11 = (gcnew System::Windows::Forms::Label());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->lblTotv = (gcnew System::Windows::Forms::Label());
			this->label19 = (gcnew System::Windows::Forms::Label());
			this->tabPage5 = (gcnew System::Windows::Forms::TabPage());
			this->label34 = (gcnew System::Windows::Forms::Label());
			this->groupBox6 = (gcnew System::Windows::Forms::GroupBox());
			this->btnCancelMACD = (gcnew System::Windows::Forms::Button());
			this->lblOSC = (gcnew System::Windows::Forms::Label());
			this->lblDIF = (gcnew System::Windows::Forms::Label());
			this->lblMACD = (gcnew System::Windows::Forms::Label());
			this->label29 = (gcnew System::Windows::Forms::Label());
			this->label30 = (gcnew System::Windows::Forms::Label());
			this->textMACD = (gcnew System::Windows::Forms::TextBox());
			this->label31 = (gcnew System::Windows::Forms::Label());
			this->label32 = (gcnew System::Windows::Forms::Label());
			this->btnGetMACD = (gcnew System::Windows::Forms::Button());
			this->label33 = (gcnew System::Windows::Forms::Label());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->btnCancelBool = (gcnew System::Windows::Forms::Button());
			this->lblLBT = (gcnew System::Windows::Forms::Label());
			this->lblUBT = (gcnew System::Windows::Forms::Label());
			this->lblAVG = (gcnew System::Windows::Forms::Label());
			this->label28 = (gcnew System::Windows::Forms::Label());
			this->label27 = (gcnew System::Windows::Forms::Label());
			this->textBool = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label16 = (gcnew System::Windows::Forms::Label());
			this->btnGetBool = (gcnew System::Windows::Forms::Button());
			this->label26 = (gcnew System::Windows::Forms::Label());
			this->tabPage6 = (gcnew System::Windows::Forms::TabPage());
			this->btn_cancelFTI = (gcnew System::Windows::Forms::Button());
			this->Btn_RequestFutureTradeInfo = (gcnew System::Windows::Forms::Button());
			this->text_StockNo = (gcnew System::Windows::Forms::TextBox());
			this->label35 = (gcnew System::Windows::Forms::Label());
			this->tableLayoutPanel3 = (gcnew System::Windows::Forms::TableLayoutPanel());
			this->lblStockIdx = (gcnew System::Windows::Forms::Label());
			this->lblMarketNo = (gcnew System::Windows::Forms::Label());
			this->label44 = (gcnew System::Windows::Forms::Label());
			this->label43 = (gcnew System::Windows::Forms::Label());
			this->label40 = (gcnew System::Windows::Forms::Label());
			this->lblFTIBDC = (gcnew System::Windows::Forms::Label());
			this->label41 = (gcnew System::Windows::Forms::Label());
			this->lblFTISDC = (gcnew System::Windows::Forms::Label());
			this->lblFTISq = (gcnew System::Windows::Forms::Label());
			this->label39 = (gcnew System::Windows::Forms::Label());
			this->lblFTISc = (gcnew System::Windows::Forms::Label());
			this->label37 = (gcnew System::Windows::Forms::Label());
			this->lblFTIBq = (gcnew System::Windows::Forms::Label());
			this->label38 = (gcnew System::Windows::Forms::Label());
			this->label36 = (gcnew System::Windows::Forms::Label());
			this->lblFTIBc = (gcnew System::Windows::Forms::Label());
			this->tabPage7 = (gcnew System::Windows::Forms::TabPage());
			this->txt_StrikePriceCount = (gcnew System::Windows::Forms::Label());
			this->listStrikePrices = (gcnew System::Windows::Forms::ListBox());
			this->groupBox7 = (gcnew System::Windows::Forms::GroupBox());
			this->GetStrikePrices = (gcnew System::Windows::Forms::Button());
			this->label48 = (gcnew System::Windows::Forms::Label());
			this->tabPage8 = (gcnew System::Windows::Forms::TabPage());
			this->label42 = (gcnew System::Windows::Forms::Label());
			this->RequestStockListBtn = (gcnew System::Windows::Forms::Button());
			this->MarketNo_txt = (gcnew System::Windows::Forms::TextBox());
			this->StockList = (gcnew System::Windows::Forms::ListBox());
			this->txtMinuteNumber = (gcnew System::Windows::Forms::TextBox());
			this->label59 = (gcnew System::Windows::Forms::Label());
			this->groupBox1->SuspendLayout();
			this->tabControl1->SuspendLayout();
			this->tabPage1->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->tabPage2->SuspendLayout();
			this->groupBox5->SuspendLayout();
			this->groupBox4->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->GridBest5Bid))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->GridBest5Ask))->BeginInit();
			this->tabPage3->SuspendLayout();
			this->tabPage4->SuspendLayout();
			this->tableLayoutPanel2->SuspendLayout();
			this->tableLayoutPanel1->SuspendLayout();
			this->tabPage5->SuspendLayout();
			this->groupBox6->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->tabPage6->SuspendLayout();
			this->tableLayoutPanel3->SuspendLayout();
			this->tabPage7->SuspendLayout();
			this->groupBox7->SuspendLayout();
			this->tabPage8->SuspendLayout();
			this->SuspendLayout();
			// 
			// btnDisconnect
			// 
			this->btnDisconnect->Location = System::Drawing::Point(31, 48);
			this->btnDisconnect->Name = L"btnDisconnect";
			this->btnDisconnect->Size = System::Drawing::Size(96, 23);
			this->btnDisconnect->TabIndex = 52;
			this->btnDisconnect->Text = L"Disconnect";
			this->btnDisconnect->UseVisualStyleBackColor = true;
			this->btnDisconnect->Click += gcnew System::EventHandler(this, &SKQuote::btnDisconnect_Click);
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(31, 22);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(96, 23);
			this->button1->TabIndex = 50;
			this->button1->Text = L"Connect";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &SKQuote::button1_Click);
			// 
			// lblSignal
			// 
			this->lblSignal->AutoSize = true;
			this->lblSignal->Font = (gcnew System::Drawing::Font(L"新細明體", 16, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblSignal->ForeColor = System::Drawing::Color::Red;
			this->lblSignal->Location = System::Drawing::Point(15, 18);
			this->lblSignal->Name = L"lblSignal";
			this->lblSignal->Size = System::Drawing::Size(32, 22);
			this->lblSignal->TabIndex = 0;
			this->lblSignal->Text = L"●";
			// 
			// ConnectedLabel
			// 
			this->ConnectedLabel->AutoSize = true;
			this->ConnectedLabel->Location = System::Drawing::Point(368, 45);
			this->ConnectedLabel->Name = L"ConnectedLabel";
			this->ConnectedLabel->Size = System::Drawing::Size(11, 12);
			this->ConnectedLabel->TabIndex = 54;
			this->ConnectedLabel->Text = L"0";
			// 
			// btnIsConnected
			// 
			this->btnIsConnected->Location = System::Drawing::Point(287, 36);
			this->btnIsConnected->Name = L"btnIsConnected";
			this->btnIsConnected->Size = System::Drawing::Size(75, 29);
			this->btnIsConnected->TabIndex = 53;
			this->btnIsConnected->Text = L"IsConnected";
			this->btnIsConnected->UseVisualStyleBackColor = true;
			this->btnIsConnected->Click += gcnew System::EventHandler(this, &SKQuote::btnIsConnected_Click);
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->lblSignal);
			this->groupBox1->Location = System::Drawing::Point(155, 22);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(126, 54);
			this->groupBox1->TabIndex = 51;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"solace_QuoteServer";
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->tabPage1);
			this->tabControl1->Controls->Add(this->tabPage2);
			this->tabControl1->Controls->Add(this->tabPage3);
			this->tabControl1->Controls->Add(this->tabPage4);
			this->tabControl1->Controls->Add(this->tabPage5);
			this->tabControl1->Controls->Add(this->tabPage6);
			this->tabControl1->Controls->Add(this->tabPage7);
			this->tabControl1->Controls->Add(this->tabPage8);
			this->tabControl1->Location = System::Drawing::Point(18, 90);
			this->tabControl1->Name = L"tabControl1";
			this->tabControl1->SelectedIndex = 0;
			this->tabControl1->Size = System::Drawing::Size(760, 558);
			this->tabControl1->TabIndex = 55;
			// 
			// tabPage1
			// 
			this->tabPage1->Controls->Add(this->dataGridView1);
			this->tabPage1->Controls->Add(this->btnCancelStocks);
			this->tabPage1->Controls->Add(this->txtPageNo);
			this->tabPage1->Controls->Add(this->label4);
			this->tabPage1->Controls->Add(this->btnQueryStocks);
			this->tabPage1->Controls->Add(this->label2);
			this->tabPage1->Controls->Add(this->txtStocks);
			this->tabPage1->Controls->Add(this->label3);
			this->tabPage1->Location = System::Drawing::Point(4, 22);
			this->tabPage1->Name = L"tabPage1";
			this->tabPage1->Padding = System::Windows::Forms::Padding(3);
			this->tabPage1->Size = System::Drawing::Size(752, 532);
			this->tabPage1->TabIndex = 0;
			this->tabPage1->Text = L"Quote";
			this->tabPage1->UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Location = System::Drawing::Point(10, 51);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->RowTemplate->Height = 24;
			this->dataGridView1->Size = System::Drawing::Size(733, 471);
			this->dataGridView1->TabIndex = 20;
			// 
			// btnCancelStocks
			// 
			this->btnCancelStocks->Location = System::Drawing::Point(669, 18);
			this->btnCancelStocks->Name = L"btnCancelStocks";
			this->btnCancelStocks->Size = System::Drawing::Size(75, 23);
			this->btnCancelStocks->TabIndex = 19;
			this->btnCancelStocks->Text = L"取消";
			this->btnCancelStocks->UseVisualStyleBackColor = true;
			// 
			// txtPageNo
			// 
			this->txtPageNo->Location = System::Drawing::Point(64, 16);
			this->txtPageNo->Name = L"txtPageNo";
			this->txtPageNo->Size = System::Drawing::Size(46, 22);
			this->txtPageNo->TabIndex = 17;
			this->txtPageNo->Text = L"-1";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(7, 21);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(41, 12);
			this->label4->TabIndex = 16;
			this->label4->Text = L"PageNo";
			// 
			// btnQueryStocks
			// 
			this->btnQueryStocks->Location = System::Drawing::Point(586, 18);
			this->btnQueryStocks->Name = L"btnQueryStocks";
			this->btnQueryStocks->Size = System::Drawing::Size(75, 23);
			this->btnQueryStocks->TabIndex = 15;
			this->btnQueryStocks->Text = L"查詢";
			this->btnQueryStocks->UseVisualStyleBackColor = true;
			this->btnQueryStocks->Click += gcnew System::EventHandler(this, &SKQuote::btnQueryStocks_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(450, 21);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(116, 12);
			this->label2->TabIndex = 14;
			this->label2->Text = L"( 多筆以逗號{,}區隔 )";
			// 
			// txtStocks
			// 
			this->txtStocks->Location = System::Drawing::Point(196, 16);
			this->txtStocks->Name = L"txtStocks";
			this->txtStocks->Size = System::Drawing::Size(243, 22);
			this->txtStocks->TabIndex = 13;
			this->txtStocks->Text = L"TX00,MTX00,6005";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(138, 21);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(53, 12);
			this->label3->TabIndex = 12;
			this->label3->Text = L"商品代碼";
			// 
			// tabPage2
			// 
			this->tabPage2->Controls->Add(this->groupBox5);
			this->tabPage2->Controls->Add(this->groupBox4);
			this->tabPage2->Controls->Add(this->chkBoxSimulate);
			this->tabPage2->Controls->Add(this->Box_M);
			this->tabPage2->Controls->Add(this->chkbox_msms);
			this->tabPage2->Controls->Add(this->txtTickPageNo);
			this->tabPage2->Controls->Add(this->label45);
			this->tabPage2->Controls->Add(this->btnLiveTick);
			this->tabPage2->Controls->Add(this->GridBest5Bid);
			this->tabPage2->Controls->Add(this->GridBest5Ask);
			this->tabPage2->Controls->Add(this->btnTickStop);
			this->tabPage2->Controls->Add(this->listTicks);
			this->tabPage2->Controls->Add(this->btnTicks);
			this->tabPage2->Controls->Add(this->txtTick);
			this->tabPage2->Location = System::Drawing::Point(4, 22);
			this->tabPage2->Name = L"tabPage2";
			this->tabPage2->Padding = System::Windows::Forms::Padding(3);
			this->tabPage2->Size = System::Drawing::Size(752, 532);
			this->tabPage2->TabIndex = 1;
			this->tabPage2->Text = L"Tick & Best5";
			this->tabPage2->UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this->groupBox5->Controls->Add(this->txtBestMarket);
			this->groupBox5->Controls->Add(this->lblBest5Ask);
			this->groupBox5->Controls->Add(this->lblBest5Bid);
			this->groupBox5->Controls->Add(this->btnGetBest5);
			this->groupBox5->Controls->Add(this->txtBestStockidx);
			this->groupBox5->Controls->Add(this->label5);
			this->groupBox5->Location = System::Drawing::Point(376, 391);
			this->groupBox5->Name = L"groupBox5";
			this->groupBox5->Size = System::Drawing::Size(321, 119);
			this->groupBox5->TabIndex = 37;
			this->groupBox5->TabStop = false;
			this->groupBox5->Text = L"GetBest5";
			// 
			// txtBestMarket
			// 
			this->txtBestMarket->Location = System::Drawing::Point(123, 18);
			this->txtBestMarket->Name = L"txtBestMarket";
			this->txtBestMarket->Size = System::Drawing::Size(38, 22);
			this->txtBestMarket->TabIndex = 8;
			// 
			// lblBest5Ask
			// 
			this->lblBest5Ask->AutoSize = true;
			this->lblBest5Ask->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblBest5Ask->Location = System::Drawing::Point(15, 91);
			this->lblBest5Ask->Name = L"lblBest5Ask";
			this->lblBest5Ask->Size = System::Drawing::Size(228, 12);
			this->lblBest5Ask->TabIndex = 7;
			this->lblBest5Ask->Text = L"Best5_Sell..........................................................";
			// 
			// lblBest5Bid
			// 
			this->lblBest5Bid->AutoSize = true;
			this->lblBest5Bid->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblBest5Bid->Location = System::Drawing::Point(15, 66);
			this->lblBest5Bid->Name = L"lblBest5Bid";
			this->lblBest5Bid->Size = System::Drawing::Size(231, 12);
			this->lblBest5Bid->TabIndex = 6;
			this->lblBest5Bid->Text = L"Best5_Buy..........................................................";
			// 
			// btnGetBest5
			// 
			this->btnGetBest5->Location = System::Drawing::Point(240, 18);
			this->btnGetBest5->Name = L"btnGetBest5";
			this->btnGetBest5->Size = System::Drawing::Size(75, 23);
			this->btnGetBest5->TabIndex = 5;
			this->btnGetBest5->Text = L"GetBest5";
			this->btnGetBest5->UseVisualStyleBackColor = true;
			this->btnGetBest5->Click += gcnew System::EventHandler(this, &SKQuote::btnGetBest5_Click);
			// 
			// txtBestStockidx
			// 
			this->txtBestStockidx->Location = System::Drawing::Point(167, 16);
			this->txtBestStockidx->Name = L"txtBestStockidx";
			this->txtBestStockidx->Size = System::Drawing::Size(67, 22);
			this->txtBestStockidx->TabIndex = 4;
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(15, 28);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(82, 12);
			this->label5->TabIndex = 3;
			this->label5->Text = L"Market/Stockidx";
			// 
			// groupBox4
			// 
			this->groupBox4->Controls->Add(this->txtTickMarket);
			this->groupBox4->Controls->Add(this->lblGetTick);
			this->groupBox4->Controls->Add(this->btnGetTick);
			this->groupBox4->Controls->Add(this->label6);
			this->groupBox4->Controls->Add(this->txtTickPtr);
			this->groupBox4->Controls->Add(this->txtTickStockidx);
			this->groupBox4->Location = System::Drawing::Point(19, 393);
			this->groupBox4->Name = L"groupBox4";
			this->groupBox4->Size = System::Drawing::Size(321, 117);
			this->groupBox4->TabIndex = 36;
			this->groupBox4->TabStop = false;
			this->groupBox4->Text = L"GetTick";
			// 
			// txtTickMarket
			// 
			this->txtTickMarket->Location = System::Drawing::Point(152, 21);
			this->txtTickMarket->Name = L"txtTickMarket";
			this->txtTickMarket->Size = System::Drawing::Size(38, 22);
			this->txtTickMarket->TabIndex = 5;
			// 
			// lblGetTick
			// 
			this->lblGetTick->AutoSize = true;
			this->lblGetTick->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblGetTick->Location = System::Drawing::Point(8, 86);
			this->lblGetTick->Name = L"lblGetTick";
			this->lblGetTick->Size = System::Drawing::Size(234, 12);
			this->lblGetTick->TabIndex = 4;
			this->lblGetTick->Text = L"HistoryTick..........................................................";
			// 
			// btnGetTick
			// 
			this->btnGetTick->Location = System::Drawing::Point(6, 50);
			this->btnGetTick->Name = L"btnGetTick";
			this->btnGetTick->Size = System::Drawing::Size(75, 23);
			this->btnGetTick->TabIndex = 3;
			this->btnGetTick->Text = L"GetTick";
			this->btnGetTick->UseVisualStyleBackColor = true;
			this->btnGetTick->Click += gcnew System::EventHandler(this, &SKQuote::btnGetTick_Click);
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(6, 24);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(104, 12);
			this->label6->TabIndex = 2;
			this->label6->Text = L"Market/Stockidx/nPtr";
			// 
			// txtTickPtr
			// 
			this->txtTickPtr->Location = System::Drawing::Point(251, 21);
			this->txtTickPtr->Name = L"txtTickPtr";
			this->txtTickPtr->Size = System::Drawing::Size(52, 22);
			this->txtTickPtr->TabIndex = 1;
			// 
			// txtTickStockidx
			// 
			this->txtTickStockidx->Location = System::Drawing::Point(197, 21);
			this->txtTickStockidx->Name = L"txtTickStockidx";
			this->txtTickStockidx->Size = System::Drawing::Size(48, 22);
			this->txtTickStockidx->TabIndex = 0;
			// 
			// chkBoxSimulate
			// 
			this->chkBoxSimulate->AutoCheck = false;
			this->chkBoxSimulate->AutoSize = true;
			this->chkBoxSimulate->Location = System::Drawing::Point(169, 322);
			this->chkBoxSimulate->Name = L"chkBoxSimulate";
			this->chkBoxSimulate->Size = System::Drawing::Size(84, 16);
			this->chkBoxSimulate->TabIndex = 33;
			this->chkBoxSimulate->Text = L"含試算揭示";
			this->chkBoxSimulate->UseVisualStyleBackColor = true;
			// 
			// Box_M
			// 
			this->Box_M->AutoSize = true;
			this->Box_M->Checked = true;
			this->Box_M->CheckState = System::Windows::Forms::CheckState::Checked;
			this->Box_M->Location = System::Drawing::Point(259, 319);
			this->Box_M->Name = L"Box_M";
			this->Box_M->Size = System::Drawing::Size(108, 16);
			this->Box_M->TabIndex = 32;
			this->Box_M->Text = L"含市價揭示轉換";
			this->Box_M->UseVisualStyleBackColor = true;
			// 
			// chkbox_msms
			// 
			this->chkbox_msms->AutoSize = true;
			this->chkbox_msms->Location = System::Drawing::Point(14, 322);
			this->chkbox_msms->Name = L"chkbox_msms";
			this->chkbox_msms->Size = System::Drawing::Size(96, 16);
			this->chkbox_msms->TabIndex = 31;
			this->chkbox_msms->Text = L"不含毫秒微秒";
			this->chkbox_msms->UseVisualStyleBackColor = true;
			// 
			// txtTickPageNo
			// 
			this->txtTickPageNo->Location = System::Drawing::Point(88, 15);
			this->txtTickPageNo->Name = L"txtTickPageNo";
			this->txtTickPageNo->Size = System::Drawing::Size(46, 22);
			this->txtTickPageNo->TabIndex = 30;
			this->txtTickPageNo->Text = L"-1";
			// 
			// label45
			// 
			this->label45->AutoSize = true;
			this->label45->Location = System::Drawing::Point(31, 20);
			this->label45->Name = L"label45";
			this->label45->Size = System::Drawing::Size(41, 12);
			this->label45->TabIndex = 29;
			this->label45->Text = L"PageNo";
			// 
			// btnLiveTick
			// 
			this->btnLiveTick->Location = System::Drawing::Point(169, 47);
			this->btnLiveTick->Name = L"btnLiveTick";
			this->btnLiveTick->Size = System::Drawing::Size(112, 27);
			this->btnLiveTick->TabIndex = 28;
			this->btnLiveTick->Text = L"RequestLiveTick";
			this->btnLiveTick->UseVisualStyleBackColor = true;
			this->btnLiveTick->Click += gcnew System::EventHandler(this, &SKQuote::btnLiveTick_Click);
			// 
			// GridBest5Bid
			// 
			this->GridBest5Bid->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->GridBest5Bid->Location = System::Drawing::Point(418, 193);
			this->GridBest5Bid->MultiSelect = false;
			this->GridBest5Bid->Name = L"GridBest5Bid";
			this->GridBest5Bid->RowHeadersVisible = false;
			this->GridBest5Bid->RowTemplate->Height = 24;
			this->GridBest5Bid->ScrollBars = System::Windows::Forms::ScrollBars::None;
			this->GridBest5Bid->Size = System::Drawing::Size(159, 178);
			this->GridBest5Bid->TabIndex = 27;
			// 
			// GridBest5Ask
			// 
			this->GridBest5Ask->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->GridBest5Ask->Location = System::Drawing::Point(418, 6);
			this->GridBest5Ask->MultiSelect = false;
			this->GridBest5Ask->Name = L"GridBest5Ask";
			this->GridBest5Ask->RowHeadersVisible = false;
			this->GridBest5Ask->RowTemplate->Height = 24;
			this->GridBest5Ask->ScrollBars = System::Windows::Forms::ScrollBars::None;
			this->GridBest5Ask->Size = System::Drawing::Size(159, 181);
			this->GridBest5Ask->TabIndex = 26;
			// 
			// btnTickStop
			// 
			this->btnTickStop->Location = System::Drawing::Point(300, 16);
			this->btnTickStop->Name = L"btnTickStop";
			this->btnTickStop->Size = System::Drawing::Size(102, 25);
			this->btnTickStop->TabIndex = 25;
			this->btnTickStop->Text = L"Stop";
			this->btnTickStop->UseVisualStyleBackColor = true;
			this->btnTickStop->Click += gcnew System::EventHandler(this, &SKQuote::btnTickStop_Click);
			// 
			// listTicks
			// 
			this->listTicks->BackColor = System::Drawing::SystemColors::Window;
			this->listTicks->Font = (gcnew System::Drawing::Font(L"新細明體", 13, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->listTicks->FormattingEnabled = true;
			this->listTicks->HorizontalExtent = 1000;
			this->listTicks->HorizontalScrollbar = true;
			this->listTicks->ItemHeight = 17;
			this->listTicks->Location = System::Drawing::Point(10, 77);
			this->listTicks->Name = L"listTicks";
			this->listTicks->ScrollAlwaysVisible = true;
			this->listTicks->Size = System::Drawing::Size(371, 242);
			this->listTicks->TabIndex = 24;
			// 
			// btnTicks
			// 
			this->btnTicks->Location = System::Drawing::Point(169, 14);
			this->btnTicks->Name = L"btnTicks";
			this->btnTicks->Size = System::Drawing::Size(112, 27);
			this->btnTicks->TabIndex = 23;
			this->btnTicks->Text = L"Request Tick";
			this->btnTicks->UseVisualStyleBackColor = true;
			this->btnTicks->Click += gcnew System::EventHandler(this, &SKQuote::btnTicks_Click);
			// 
			// txtTick
			// 
			this->txtTick->Location = System::Drawing::Point(59, 47);
			this->txtTick->Name = L"txtTick";
			this->txtTick->Size = System::Drawing::Size(63, 22);
			this->txtTick->TabIndex = 22;
			// 
			// tabPage3
			// 
			this->tabPage3->Controls->Add(this->txtMinuteNumber);
			this->tabPage3->Controls->Add(this->label59);
			this->tabPage3->Controls->Add(this->listKLine);
			this->tabPage3->Controls->Add(this->label47);
			this->tabPage3->Controls->Add(this->btnKLineAM);
			this->tabPage3->Controls->Add(this->boxOutType);
			this->tabPage3->Controls->Add(this->boxTradeSession);
			this->tabPage3->Controls->Add(this->label46);
			this->tabPage3->Controls->Add(this->btnKLine);
			this->tabPage3->Controls->Add(this->txtKLine);
			this->tabPage3->Controls->Add(this->txtStartDate);
			this->tabPage3->Controls->Add(this->btnKLineAMByDate);
			this->tabPage3->Controls->Add(this->boxKLine);
			this->tabPage3->Controls->Add(this->txtEndDate);
			this->tabPage3->Location = System::Drawing::Point(4, 22);
			this->tabPage3->Name = L"tabPage3";
			this->tabPage3->Padding = System::Windows::Forms::Padding(3);
			this->tabPage3->Size = System::Drawing::Size(752, 532);
			this->tabPage3->TabIndex = 2;
			this->tabPage3->Text = L"KLine";
			this->tabPage3->UseVisualStyleBackColor = true;
			// 
			// listKLine
			// 
			this->listKLine->Font = (gcnew System::Drawing::Font(L"新細明體", 13, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->listKLine->FormattingEnabled = true;
			this->listKLine->Location = System::Drawing::Point(20, 138);
			this->listKLine->Name = L"listKLine";
			this->listKLine->Size = System::Drawing::Size(714, 378);
			this->listKLine->TabIndex = 71;
			// 
			// label47
			// 
			this->label47->AutoSize = true;
			this->label47->Location = System::Drawing::Point(204, 95);
			this->label47->Name = L"label47";
			this->label47->Size = System::Drawing::Size(45, 12);
			this->label47->TabIndex = 79;
			this->label47->Text = L"EndDate";
			// 
			// btnKLineAM
			// 
			this->btnKLineAM->Location = System::Drawing::Point(392, 48);
			this->btnKLineAM->Name = L"btnKLineAM";
			this->btnKLineAM->Size = System::Drawing::Size(115, 23);
			this->btnKLineAM->TabIndex = 73;
			this->btnKLineAM->Text = L"NewQuery";
			this->btnKLineAM->UseVisualStyleBackColor = true;
			this->btnKLineAM->Click += gcnew System::EventHandler(this, &SKQuote::btnKLineAM_Click);
			// 
			// boxOutType
			// 
			this->boxOutType->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxOutType->FormattingEnabled = true;
			this->boxOutType->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"0 = 舊版輸出格式", L"1 = 新版輸出格式" });
			this->boxOutType->Location = System::Drawing::Point(247, 12);
			this->boxOutType->Name = L"boxOutType";
			this->boxOutType->Size = System::Drawing::Size(139, 20);
			this->boxOutType->TabIndex = 72;
			// 
			// boxTradeSession
			// 
			this->boxTradeSession->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxTradeSession->FormattingEnabled = true;
			this->boxTradeSession->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"0 = 全盤K線(國內期選用)", L"1 = AM盤K線(國內期選用)" });
			this->boxTradeSession->Location = System::Drawing::Point(247, 48);
			this->boxTradeSession->Name = L"boxTradeSession";
			this->boxTradeSession->Size = System::Drawing::Size(139, 20);
			this->boxTradeSession->TabIndex = 74;
			// 
			// label46
			// 
			this->label46->AutoSize = true;
			this->label46->Location = System::Drawing::Point(18, 95);
			this->label46->Name = L"label46";
			this->label46->Size = System::Drawing::Size(47, 12);
			this->label46->TabIndex = 78;
			this->label46->Text = L"StartDate";
			// 
			// btnKLine
			// 
			this->btnKLine->Location = System::Drawing::Point(392, 8);
			this->btnKLine->Name = L"btnKLine";
			this->btnKLine->Size = System::Drawing::Size(106, 23);
			this->btnKLine->TabIndex = 70;
			this->btnKLine->Text = L"Query";
			this->btnKLine->UseVisualStyleBackColor = true;
			this->btnKLine->Click += gcnew System::EventHandler(this, &SKQuote::btnKLine_Click);
			// 
			// txtKLine
			// 
			this->txtKLine->Location = System::Drawing::Point(20, 12);
			this->txtKLine->Name = L"txtKLine";
			this->txtKLine->Size = System::Drawing::Size(63, 22);
			this->txtKLine->TabIndex = 68;
			this->txtKLine->Text = L"TX00";
			// 
			// txtStartDate
			// 
			this->txtStartDate->Location = System::Drawing::Point(86, 89);
			this->txtStartDate->Name = L"txtStartDate";
			this->txtStartDate->Size = System::Drawing::Size(100, 22);
			this->txtStartDate->TabIndex = 75;
			this->txtStartDate->Text = L"20200801";
			// 
			// btnKLineAMByDate
			// 
			this->btnKLineAMByDate->Location = System::Drawing::Point(619, 89);
			this->btnKLineAMByDate->Name = L"btnKLineAMByDate";
			this->btnKLineAMByDate->Size = System::Drawing::Size(115, 23);
			this->btnKLineAMByDate->TabIndex = 77;
			this->btnKLineAMByDate->Text = L"NewQuery(Date)";
			this->btnKLineAMByDate->UseVisualStyleBackColor = true;
			this->btnKLineAMByDate->Click += gcnew System::EventHandler(this, &SKQuote::btnKLineAMByDate_Click);
			// 
			// boxKLine
			// 
			this->boxKLine->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->boxKLine->FormattingEnabled = true;
			this->boxKLine->Items->AddRange(gcnew cli::array< System::Object^  >(7) {
				L"0 = 1分鐘線。", L"需自行組 5分鐘線。)", L"需自行組 30分鐘線。)", L"solace未提供日線288天。",
					L"4 =完整日線。", L"5 =週線。", L"6 =月線。"
			});
			this->boxKLine->Location = System::Drawing::Point(101, 12);
			this->boxKLine->Name = L"boxKLine";
			this->boxKLine->Size = System::Drawing::Size(121, 20);
			this->boxKLine->TabIndex = 69;
			// 
			// txtEndDate
			// 
			this->txtEndDate->Location = System::Drawing::Point(259, 89);
			this->txtEndDate->Name = L"txtEndDate";
			this->txtEndDate->Size = System::Drawing::Size(100, 22);
			this->txtEndDate->TabIndex = 76;
			this->txtEndDate->Text = L"20200901";
			// 
			// tabPage4
			// 
			this->tabPage4->Controls->Add(this->button4);
			this->tabPage4->Controls->Add(this->label13);
			this->tabPage4->Controls->Add(this->label9);
			this->tabPage4->Controls->Add(this->tableLayoutPanel2);
			this->tabPage4->Controls->Add(this->tableLayoutPanel1);
			this->tabPage4->Location = System::Drawing::Point(4, 22);
			this->tabPage4->Name = L"tabPage4";
			this->tabPage4->Padding = System::Windows::Forms::Padding(3);
			this->tabPage4->Size = System::Drawing::Size(752, 532);
			this->tabPage4->TabIndex = 3;
			this->tabPage4->Text = L"Market Info";
			this->tabPage4->UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this->button4->Location = System::Drawing::Point(616, 39);
			this->button4->Name = L"button4";
			this->button4->Size = System::Drawing::Size(75, 23);
			this->button4->TabIndex = 13;
			this->button4->Text = L"查詢";
			this->button4->UseVisualStyleBackColor = true;
			this->button4->Click += gcnew System::EventHandler(this, &SKQuote::button4_Click);
			// 
			// label13
			// 
			this->label13->AutoSize = true;
			this->label13->Location = System::Drawing::Point(162, 321);
			this->label13->Name = L"label13";
			this->label13->Size = System::Drawing::Size(29, 12);
			this->label13->TabIndex = 12;
			this->label13->Text = L"上櫃";
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(162, 81);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(29, 12);
			this->label9->TabIndex = 11;
			this->label9->Text = L"上市";
			// 
			// tableLayoutPanel2
			// 
			this->tableLayoutPanel2->BackColor = System::Drawing::Color::Black;
			this->tableLayoutPanel2->ColumnCount = 4;
			this->tableLayoutPanel2->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				20.23291F)));
			this->tableLayoutPanel2->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				27.77252F)));
			this->tableLayoutPanel2->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				25.24774F)));
			this->tableLayoutPanel2->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				26.74683F)));
			this->tableLayoutPanel2->Controls->Add(this->lblsNoChange2, 1, 5);
			this->tableLayoutPanel2->Controls->Add(this->lblsDown2, 3, 4);
			this->tableLayoutPanel2->Controls->Add(this->lblsLow2, 1, 4);
			this->tableLayoutPanel2->Controls->Add(this->lblsUp2, 3, 3);
			this->tableLayoutPanel2->Controls->Add(this->lblsHigh2, 1, 3);
			this->tableLayoutPanel2->Controls->Add(this->label21, 0, 5);
			this->tableLayoutPanel2->Controls->Add(this->label22, 2, 4);
			this->tableLayoutPanel2->Controls->Add(this->label23, 0, 4);
			this->tableLayoutPanel2->Controls->Add(this->label24, 0, 3);
			this->tableLayoutPanel2->Controls->Add(this->label25, 2, 3);
			this->tableLayoutPanel2->Controls->Add(this->lbllSs2, 2, 2);
			this->tableLayoutPanel2->Controls->Add(this->lbllSc2, 3, 2);
			this->tableLayoutPanel2->Controls->Add(this->lbllBs2, 2, 1);
			this->tableLayoutPanel2->Controls->Add(this->lbllBc2, 3, 1);
			this->tableLayoutPanel2->Controls->Add(this->label8, 0, 2);
			this->tableLayoutPanel2->Controls->Add(this->lblTotc2, 3, 0);
			this->tableLayoutPanel2->Controls->Add(this->lblTots2, 2, 0);
			this->tableLayoutPanel2->Controls->Add(this->label10, 0, 1);
			this->tableLayoutPanel2->Controls->Add(this->label12, 0, 0);
			this->tableLayoutPanel2->Controls->Add(this->lblTotv2, 1, 0);
			this->tableLayoutPanel2->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->tableLayoutPanel2->Location = System::Drawing::Point(161, 336);
			this->tableLayoutPanel2->Name = L"tableLayoutPanel2";
			this->tableLayoutPanel2->RowCount = 6;
			this->tableLayoutPanel2->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel2->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel2->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel2->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel2->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel2->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel2->Size = System::Drawing::Size(391, 161);
			this->tableLayoutPanel2->TabIndex = 10;
			// 
			// lblsNoChange2
			// 
			this->lblsNoChange2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsNoChange2->AutoSize = true;
			this->lblsNoChange2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsNoChange2->ForeColor = System::Drawing::Color::Cyan;
			this->lblsNoChange2->Location = System::Drawing::Point(184, 149);
			this->lblsNoChange2->Name = L"lblsNoChange2";
			this->lblsNoChange2->Size = System::Drawing::Size(0, 12);
			this->lblsNoChange2->TabIndex = 6;
			// 
			// lblsDown2
			// 
			this->lblsDown2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsDown2->AutoSize = true;
			this->lblsDown2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsDown2->ForeColor = System::Drawing::Color::Lime;
			this->lblsDown2->Location = System::Drawing::Point(388, 118);
			this->lblsDown2->Name = L"lblsDown2";
			this->lblsDown2->Size = System::Drawing::Size(0, 12);
			this->lblsDown2->TabIndex = 17;
			// 
			// lblsLow2
			// 
			this->lblsLow2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsLow2->AutoSize = true;
			this->lblsLow2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsLow2->ForeColor = System::Drawing::Color::Lime;
			this->lblsLow2->Location = System::Drawing::Point(184, 118);
			this->lblsLow2->Name = L"lblsLow2";
			this->lblsLow2->Size = System::Drawing::Size(0, 12);
			this->lblsLow2->TabIndex = 18;
			// 
			// lblsUp2
			// 
			this->lblsUp2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsUp2->AutoSize = true;
			this->lblsUp2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsUp2->ForeColor = System::Drawing::Color::Red;
			this->lblsUp2->Location = System::Drawing::Point(388, 92);
			this->lblsUp2->Name = L"lblsUp2";
			this->lblsUp2->Size = System::Drawing::Size(0, 12);
			this->lblsUp2->TabIndex = 15;
			// 
			// lblsHigh2
			// 
			this->lblsHigh2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsHigh2->AutoSize = true;
			this->lblsHigh2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsHigh2->ForeColor = System::Drawing::Color::Red;
			this->lblsHigh2->Location = System::Drawing::Point(184, 92);
			this->lblsHigh2->Name = L"lblsHigh2";
			this->lblsHigh2->Size = System::Drawing::Size(0, 12);
			this->lblsHigh2->TabIndex = 16;
			// 
			// label21
			// 
			this->label21->AutoSize = true;
			this->label21->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label21->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label21->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label21->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label21->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label21->Location = System::Drawing::Point(3, 130);
			this->label21->Name = L"label21";
			this->label21->Size = System::Drawing::Size(73, 31);
			this->label21->TabIndex = 17;
			this->label21->Text = L"平  盤";
			// 
			// label22
			// 
			this->label22->AutoSize = true;
			this->label22->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label22->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label22->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label22->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label22->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label22->Location = System::Drawing::Point(190, 104);
			this->label22->Name = L"label22";
			this->label22->Size = System::Drawing::Size(92, 26);
			this->label22->TabIndex = 18;
			this->label22->Text = L"下跌數";
			// 
			// label23
			// 
			this->label23->AutoSize = true;
			this->label23->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label23->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label23->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label23->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label23->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label23->Location = System::Drawing::Point(3, 104);
			this->label23->Name = L"label23";
			this->label23->Size = System::Drawing::Size(73, 26);
			this->label23->TabIndex = 19;
			this->label23->Text = L"跌  停";
			// 
			// label24
			// 
			this->label24->AutoSize = true;
			this->label24->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label24->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label24->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label24->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label24->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label24->Location = System::Drawing::Point(3, 78);
			this->label24->Name = L"label24";
			this->label24->Size = System::Drawing::Size(73, 26);
			this->label24->TabIndex = 20;
			this->label24->Text = L"漲  停";
			// 
			// label25
			// 
			this->label25->AutoSize = true;
			this->label25->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label25->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label25->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label25->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label25->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label25->Location = System::Drawing::Point(190, 78);
			this->label25->Name = L"label25";
			this->label25->Size = System::Drawing::Size(92, 26);
			this->label25->TabIndex = 21;
			this->label25->Text = L"上漲數";
			// 
			// lbllSs2
			// 
			this->lbllSs2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllSs2->AutoSize = true;
			this->lbllSs2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllSs2->ForeColor = System::Drawing::Color::Lime;
			this->lbllSs2->Location = System::Drawing::Point(282, 66);
			this->lbllSs2->Name = L"lbllSs2";
			this->lbllSs2->Size = System::Drawing::Size(0, 12);
			this->lbllSs2->TabIndex = 15;
			// 
			// lbllSc2
			// 
			this->lbllSc2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllSc2->AutoSize = true;
			this->lbllSc2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllSc2->ForeColor = System::Drawing::Color::Lime;
			this->lbllSc2->Location = System::Drawing::Point(388, 66);
			this->lbllSc2->Name = L"lbllSc2";
			this->lbllSc2->Size = System::Drawing::Size(0, 12);
			this->lbllSc2->TabIndex = 15;
			// 
			// lbllBs2
			// 
			this->lbllBs2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllBs2->AutoSize = true;
			this->lbllBs2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllBs2->ForeColor = System::Drawing::Color::Red;
			this->lbllBs2->Location = System::Drawing::Point(282, 40);
			this->lbllBs2->Name = L"lbllBs2";
			this->lbllBs2->Size = System::Drawing::Size(0, 12);
			this->lbllBs2->TabIndex = 14;
			// 
			// lbllBc2
			// 
			this->lbllBc2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllBc2->AutoSize = true;
			this->lbllBc2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllBc2->ForeColor = System::Drawing::Color::Red;
			this->lbllBc2->Location = System::Drawing::Point(388, 40);
			this->lbllBc2->Name = L"lbllBc2";
			this->lbllBc2->Size = System::Drawing::Size(0, 12);
			this->lbllBc2->TabIndex = 13;
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label8->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label8->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label8->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label8->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label8->Location = System::Drawing::Point(3, 52);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(73, 26);
			this->label8->TabIndex = 8;
			this->label8->Text = L"委賣";
			// 
			// lblTotc2
			// 
			this->lblTotc2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblTotc2->AutoSize = true;
			this->lblTotc2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblTotc2->ForeColor = System::Drawing::Color::Cyan;
			this->lblTotc2->Location = System::Drawing::Point(388, 14);
			this->lblTotc2->Name = L"lblTotc2";
			this->lblTotc2->Size = System::Drawing::Size(0, 12);
			this->lblTotc2->TabIndex = 7;
			// 
			// lblTots2
			// 
			this->lblTots2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblTots2->AutoSize = true;
			this->lblTots2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblTots2->ForeColor = System::Drawing::Color::Cyan;
			this->lblTots2->Location = System::Drawing::Point(282, 14);
			this->lblTots2->Name = L"lblTots2";
			this->lblTots2->Size = System::Drawing::Size(0, 12);
			this->lblTots2->TabIndex = 6;
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label10->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label10->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label10->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label10->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label10->Location = System::Drawing::Point(3, 26);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(73, 26);
			this->label10->TabIndex = 4;
			this->label10->Text = L"委買";
			// 
			// label12
			// 
			this->label12->AutoSize = true;
			this->label12->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label12->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label12->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label12->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label12->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label12->Location = System::Drawing::Point(3, 0);
			this->label12->Name = L"label12";
			this->label12->Size = System::Drawing::Size(73, 26);
			this->label12->TabIndex = 0;
			this->label12->Text = L"大盤成交";
			// 
			// lblTotv2
			// 
			this->lblTotv2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblTotv2->AutoSize = true;
			this->lblTotv2->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblTotv2->ForeColor = System::Drawing::Color::Cyan;
			this->lblTotv2->Location = System::Drawing::Point(184, 14);
			this->lblTotv2->Name = L"lblTotv2";
			this->lblTotv2->Size = System::Drawing::Size(0, 12);
			this->lblTotv2->TabIndex = 5;
			// 
			// tableLayoutPanel1
			// 
			this->tableLayoutPanel1->BackColor = System::Drawing::Color::Black;
			this->tableLayoutPanel1->ColumnCount = 4;
			this->tableLayoutPanel1->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				21.15115F)));
			this->tableLayoutPanel1->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				27.45281F)));
			this->tableLayoutPanel1->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				24.9571F)));
			this->tableLayoutPanel1->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				26.43893F)));
			this->tableLayoutPanel1->Controls->Add(this->lblsNoChange, 1, 5);
			this->tableLayoutPanel1->Controls->Add(this->lblsDown, 3, 4);
			this->tableLayoutPanel1->Controls->Add(this->lblsLow, 1, 4);
			this->tableLayoutPanel1->Controls->Add(this->lblsUp, 3, 3);
			this->tableLayoutPanel1->Controls->Add(this->label20, 0, 5);
			this->tableLayoutPanel1->Controls->Add(this->lblsHigh, 1, 3);
			this->tableLayoutPanel1->Controls->Add(this->label17, 2, 4);
			this->tableLayoutPanel1->Controls->Add(this->label18, 0, 4);
			this->tableLayoutPanel1->Controls->Add(this->label15, 0, 3);
			this->tableLayoutPanel1->Controls->Add(this->lbllSs, 2, 2);
			this->tableLayoutPanel1->Controls->Add(this->lbllSc, 3, 2);
			this->tableLayoutPanel1->Controls->Add(this->lbllBs, 2, 1);
			this->tableLayoutPanel1->Controls->Add(this->lbllBc, 3, 1);
			this->tableLayoutPanel1->Controls->Add(this->label14, 0, 2);
			this->tableLayoutPanel1->Controls->Add(this->lblTotc, 3, 0);
			this->tableLayoutPanel1->Controls->Add(this->lblTots, 2, 0);
			this->tableLayoutPanel1->Controls->Add(this->label11, 0, 1);
			this->tableLayoutPanel1->Controls->Add(this->label7, 0, 0);
			this->tableLayoutPanel1->Controls->Add(this->lblTotv, 1, 0);
			this->tableLayoutPanel1->Controls->Add(this->label19, 2, 3);
			this->tableLayoutPanel1->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->tableLayoutPanel1->Location = System::Drawing::Point(158, 107);
			this->tableLayoutPanel1->Name = L"tableLayoutPanel1";
			this->tableLayoutPanel1->RowCount = 6;
			this->tableLayoutPanel1->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel1->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel1->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel1->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel1->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel1->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 16.66667F)));
			this->tableLayoutPanel1->Size = System::Drawing::Size(391, 161);
			this->tableLayoutPanel1->TabIndex = 9;
			// 
			// lblsNoChange
			// 
			this->lblsNoChange->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsNoChange->AutoSize = true;
			this->lblsNoChange->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsNoChange->ForeColor = System::Drawing::Color::Cyan;
			this->lblsNoChange->Location = System::Drawing::Point(186, 149);
			this->lblsNoChange->Name = L"lblsNoChange";
			this->lblsNoChange->Size = System::Drawing::Size(0, 12);
			this->lblsNoChange->TabIndex = 7;
			// 
			// lblsDown
			// 
			this->lblsDown->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsDown->AutoSize = true;
			this->lblsDown->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsDown->ForeColor = System::Drawing::Color::Lime;
			this->lblsDown->Location = System::Drawing::Point(388, 118);
			this->lblsDown->Name = L"lblsDown";
			this->lblsDown->Size = System::Drawing::Size(0, 12);
			this->lblsDown->TabIndex = 15;
			// 
			// lblsLow
			// 
			this->lblsLow->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsLow->AutoSize = true;
			this->lblsLow->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsLow->ForeColor = System::Drawing::Color::Lime;
			this->lblsLow->Location = System::Drawing::Point(186, 118);
			this->lblsLow->Name = L"lblsLow";
			this->lblsLow->Size = System::Drawing::Size(0, 12);
			this->lblsLow->TabIndex = 16;
			// 
			// lblsUp
			// 
			this->lblsUp->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsUp->AutoSize = true;
			this->lblsUp->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsUp->ForeColor = System::Drawing::Color::Red;
			this->lblsUp->Location = System::Drawing::Point(388, 92);
			this->lblsUp->Name = L"lblsUp";
			this->lblsUp->Size = System::Drawing::Size(0, 12);
			this->lblsUp->TabIndex = 14;
			// 
			// label20
			// 
			this->label20->AutoSize = true;
			this->label20->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label20->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label20->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label20->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label20->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label20->Location = System::Drawing::Point(3, 130);
			this->label20->Name = L"label20";
			this->label20->Size = System::Drawing::Size(76, 31);
			this->label20->TabIndex = 17;
			this->label20->Text = L"平  盤";
			// 
			// lblsHigh
			// 
			this->lblsHigh->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblsHigh->AutoSize = true;
			this->lblsHigh->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblsHigh->ForeColor = System::Drawing::Color::Red;
			this->lblsHigh->Location = System::Drawing::Point(186, 92);
			this->lblsHigh->Name = L"lblsHigh";
			this->lblsHigh->Size = System::Drawing::Size(0, 12);
			this->lblsHigh->TabIndex = 13;
			// 
			// label17
			// 
			this->label17->AutoSize = true;
			this->label17->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label17->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label17->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label17->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label17->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label17->Location = System::Drawing::Point(192, 104);
			this->label17->Name = L"label17";
			this->label17->Size = System::Drawing::Size(91, 26);
			this->label17->TabIndex = 17;
			this->label17->Text = L"下跌數";
			// 
			// label18
			// 
			this->label18->AutoSize = true;
			this->label18->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label18->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label18->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label18->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label18->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label18->Location = System::Drawing::Point(3, 104);
			this->label18->Name = L"label18";
			this->label18->Size = System::Drawing::Size(76, 26);
			this->label18->TabIndex = 18;
			this->label18->Text = L"跌  停";
			// 
			// label15
			// 
			this->label15->AutoSize = true;
			this->label15->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label15->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label15->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label15->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label15->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label15->Location = System::Drawing::Point(3, 78);
			this->label15->Name = L"label15";
			this->label15->Size = System::Drawing::Size(76, 26);
			this->label15->TabIndex = 15;
			this->label15->Text = L"漲  停";
			// 
			// lbllSs
			// 
			this->lbllSs->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllSs->AutoSize = true;
			this->lbllSs->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllSs->ForeColor = System::Drawing::Color::Lime;
			this->lbllSs->Location = System::Drawing::Point(283, 66);
			this->lbllSs->Name = L"lbllSs";
			this->lbllSs->Size = System::Drawing::Size(0, 12);
			this->lbllSs->TabIndex = 14;
			// 
			// lbllSc
			// 
			this->lbllSc->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllSc->AutoSize = true;
			this->lbllSc->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllSc->ForeColor = System::Drawing::Color::Lime;
			this->lbllSc->Location = System::Drawing::Point(388, 66);
			this->lbllSc->Name = L"lbllSc";
			this->lbllSc->Size = System::Drawing::Size(0, 12);
			this->lbllSc->TabIndex = 14;
			// 
			// lbllBs
			// 
			this->lbllBs->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllBs->AutoSize = true;
			this->lbllBs->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllBs->ForeColor = System::Drawing::Color::Red;
			this->lbllBs->Location = System::Drawing::Point(283, 40);
			this->lbllBs->Name = L"lbllBs";
			this->lbllBs->Size = System::Drawing::Size(0, 12);
			this->lbllBs->TabIndex = 13;
			// 
			// lbllBc
			// 
			this->lbllBc->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lbllBc->AutoSize = true;
			this->lbllBc->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lbllBc->ForeColor = System::Drawing::Color::Red;
			this->lbllBc->Location = System::Drawing::Point(388, 40);
			this->lbllBc->Name = L"lbllBc";
			this->lbllBc->Size = System::Drawing::Size(0, 12);
			this->lbllBc->TabIndex = 12;
			// 
			// label14
			// 
			this->label14->AutoSize = true;
			this->label14->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label14->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label14->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label14->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label14->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label14->Location = System::Drawing::Point(3, 52);
			this->label14->Name = L"label14";
			this->label14->Size = System::Drawing::Size(76, 26);
			this->label14->TabIndex = 11;
			this->label14->Text = L"委  賣";
			// 
			// lblTotc
			// 
			this->lblTotc->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblTotc->AutoSize = true;
			this->lblTotc->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblTotc->ForeColor = System::Drawing::Color::Cyan;
			this->lblTotc->Location = System::Drawing::Point(388, 14);
			this->lblTotc->Name = L"lblTotc";
			this->lblTotc->Size = System::Drawing::Size(0, 12);
			this->lblTotc->TabIndex = 7;
			// 
			// lblTots
			// 
			this->lblTots->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblTots->AutoSize = true;
			this->lblTots->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblTots->ForeColor = System::Drawing::Color::Cyan;
			this->lblTots->Location = System::Drawing::Point(283, 14);
			this->lblTots->Name = L"lblTots";
			this->lblTots->Size = System::Drawing::Size(0, 12);
			this->lblTots->TabIndex = 6;
			// 
			// label11
			// 
			this->label11->AutoSize = true;
			this->label11->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label11->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label11->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label11->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label11->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label11->Location = System::Drawing::Point(3, 26);
			this->label11->Name = L"label11";
			this->label11->Size = System::Drawing::Size(76, 26);
			this->label11->TabIndex = 4;
			this->label11->Text = L"委  買";
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label7->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label7->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label7->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label7->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label7->Location = System::Drawing::Point(3, 0);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(76, 26);
			this->label7->TabIndex = 0;
			this->label7->Text = L"大盤成交";
			// 
			// lblTotv
			// 
			this->lblTotv->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblTotv->AutoSize = true;
			this->lblTotv->Font = (gcnew System::Drawing::Font(L"新細明體", 9, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblTotv->ForeColor = System::Drawing::Color::Cyan;
			this->lblTotv->Location = System::Drawing::Point(186, 14);
			this->lblTotv->Name = L"lblTotv";
			this->lblTotv->Size = System::Drawing::Size(0, 12);
			this->lblTotv->TabIndex = 5;
			// 
			// label19
			// 
			this->label19->AutoSize = true;
			this->label19->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label19->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label19->Dock = System::Windows::Forms::DockStyle::Fill;
			this->label19->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label19->ForeColor = System::Drawing::SystemColors::ControlText;
			this->label19->Location = System::Drawing::Point(192, 78);
			this->label19->Name = L"label19";
			this->label19->Size = System::Drawing::Size(91, 26);
			this->label19->TabIndex = 19;
			this->label19->Text = L"上漲數";
			// 
			// tabPage5
			// 
			this->tabPage5->Controls->Add(this->label34);
			this->tabPage5->Controls->Add(this->groupBox6);
			this->tabPage5->Controls->Add(this->groupBox2);
			this->tabPage5->Location = System::Drawing::Point(4, 22);
			this->tabPage5->Name = L"tabPage5";
			this->tabPage5->Padding = System::Windows::Forms::Padding(3);
			this->tabPage5->Size = System::Drawing::Size(752, 532);
			this->tabPage5->TabIndex = 4;
			this->tabPage5->Text = L"BOOL & MACD";
			this->tabPage5->UseVisualStyleBackColor = true;
			// 
			// label34
			// 
			this->label34->AutoSize = true;
			this->label34->Location = System::Drawing::Point(27, 30);
			this->label34->Name = L"label34";
			this->label34->Size = System::Drawing::Size(164, 12);
			this->label34->TabIndex = 20;
			this->label34->Text = L"技術分析-時間單位:日線(完整)";
			// 
			// groupBox6
			// 
			this->groupBox6->Controls->Add(this->btnCancelMACD);
			this->groupBox6->Controls->Add(this->lblOSC);
			this->groupBox6->Controls->Add(this->lblDIF);
			this->groupBox6->Controls->Add(this->lblMACD);
			this->groupBox6->Controls->Add(this->label29);
			this->groupBox6->Controls->Add(this->label30);
			this->groupBox6->Controls->Add(this->textMACD);
			this->groupBox6->Controls->Add(this->label31);
			this->groupBox6->Controls->Add(this->label32);
			this->groupBox6->Controls->Add(this->btnGetMACD);
			this->groupBox6->Controls->Add(this->label33);
			this->groupBox6->Location = System::Drawing::Point(29, 269);
			this->groupBox6->Name = L"groupBox6";
			this->groupBox6->Size = System::Drawing::Size(446, 124);
			this->groupBox6->TabIndex = 19;
			this->groupBox6->TabStop = false;
			this->groupBox6->Text = L"MACD(最新一筆)";
			// 
			// btnCancelMACD
			// 
			this->btnCancelMACD->Location = System::Drawing::Point(267, 18);
			this->btnCancelMACD->Name = L"btnCancelMACD";
			this->btnCancelMACD->Size = System::Drawing::Size(161, 23);
			this->btnCancelMACD->TabIndex = 11;
			this->btnCancelMACD->Text = L"Cancel GetMACD";
			this->btnCancelMACD->UseVisualStyleBackColor = true;
			this->btnCancelMACD->Click += gcnew System::EventHandler(this, &SKQuote::btnCancelMACD_Click);
			// 
			// lblOSC
			// 
			this->lblOSC->AutoSize = true;
			this->lblOSC->Location = System::Drawing::Point(159, 99);
			this->lblOSC->Name = L"lblOSC";
			this->lblOSC->Size = System::Drawing::Size(27, 12);
			this->lblOSC->TabIndex = 10;
			this->lblOSC->Text = L"OSC";
			// 
			// lblDIF
			// 
			this->lblDIF->AutoSize = true;
			this->lblDIF->Location = System::Drawing::Point(159, 77);
			this->lblDIF->Name = L"lblDIF";
			this->lblDIF->Size = System::Drawing::Size(23, 12);
			this->lblDIF->TabIndex = 9;
			this->lblDIF->Text = L"DIF";
			// 
			// lblMACD
			// 
			this->lblMACD->AutoSize = true;
			this->lblMACD->Location = System::Drawing::Point(159, 55);
			this->lblMACD->Name = L"lblMACD";
			this->lblMACD->Size = System::Drawing::Size(39, 12);
			this->lblMACD->TabIndex = 8;
			this->lblMACD->Text = L"MACD";
			// 
			// label29
			// 
			this->label29->AutoSize = true;
			this->label29->Location = System::Drawing::Point(16, 99);
			this->label29->Name = L"label29";
			this->label29->Size = System::Drawing::Size(30, 12);
			this->label29->TabIndex = 7;
			this->label29->Text = L"OSC:";
			// 
			// label30
			// 
			this->label30->AutoSize = true;
			this->label30->Location = System::Drawing::Point(16, 77);
			this->label30->Name = L"label30";
			this->label30->Size = System::Drawing::Size(26, 12);
			this->label30->TabIndex = 6;
			this->label30->Text = L"DIF:";
			// 
			// textMACD
			// 
			this->textMACD->Location = System::Drawing::Point(79, 18);
			this->textMACD->Name = L"textMACD";
			this->textMACD->Size = System::Drawing::Size(63, 22);
			this->textMACD->TabIndex = 6;
			this->textMACD->Text = L"6005";
			// 
			// label31
			// 
			this->label31->AutoSize = true;
			this->label31->Location = System::Drawing::Point(6, 21);
			this->label31->Name = L"label31";
			this->label31->Size = System::Drawing::Size(53, 12);
			this->label31->TabIndex = 5;
			this->label31->Text = L"商品代碼";
			// 
			// label32
			// 
			this->label32->AutoSize = true;
			this->label32->Location = System::Drawing::Point(94, 55);
			this->label32->Name = L"label32";
			this->label32->Size = System::Drawing::Size(0, 12);
			this->label32->TabIndex = 4;
			// 
			// btnGetMACD
			// 
			this->btnGetMACD->Location = System::Drawing::Point(162, 18);
			this->btnGetMACD->Name = L"btnGetMACD";
			this->btnGetMACD->Size = System::Drawing::Size(86, 23);
			this->btnGetMACD->TabIndex = 3;
			this->btnGetMACD->Text = L"Get MACD";
			this->btnGetMACD->UseVisualStyleBackColor = true;
			this->btnGetMACD->Click += gcnew System::EventHandler(this, &SKQuote::btnGetMACD_Click);
			// 
			// label33
			// 
			this->label33->AutoSize = true;
			this->label33->Location = System::Drawing::Point(16, 55);
			this->label33->Name = L"label33";
			this->label33->Size = System::Drawing::Size(42, 12);
			this->label33->TabIndex = 2;
			this->label33->Text = L"MACD:";
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->btnCancelBool);
			this->groupBox2->Controls->Add(this->lblLBT);
			this->groupBox2->Controls->Add(this->lblUBT);
			this->groupBox2->Controls->Add(this->lblAVG);
			this->groupBox2->Controls->Add(this->label28);
			this->groupBox2->Controls->Add(this->label27);
			this->groupBox2->Controls->Add(this->textBool);
			this->groupBox2->Controls->Add(this->label1);
			this->groupBox2->Controls->Add(this->label16);
			this->groupBox2->Controls->Add(this->btnGetBool);
			this->groupBox2->Controls->Add(this->label26);
			this->groupBox2->Location = System::Drawing::Point(29, 63);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(446, 124);
			this->groupBox2->TabIndex = 18;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"布林通道(最新一筆)";
			// 
			// btnCancelBool
			// 
			this->btnCancelBool->Location = System::Drawing::Point(304, 18);
			this->btnCancelBool->Name = L"btnCancelBool";
			this->btnCancelBool->Size = System::Drawing::Size(124, 25);
			this->btnCancelBool->TabIndex = 21;
			this->btnCancelBool->Text = L"Cancel GetBOOL";
			this->btnCancelBool->UseVisualStyleBackColor = true;
			this->btnCancelBool->Click += gcnew System::EventHandler(this, &SKQuote::btnCancelBool_Click);
			// 
			// lblLBT
			// 
			this->lblLBT->AutoSize = true;
			this->lblLBT->Location = System::Drawing::Point(129, 99);
			this->lblLBT->Name = L"lblLBT";
			this->lblLBT->Size = System::Drawing::Size(27, 12);
			this->lblLBT->TabIndex = 20;
			this->lblLBT->Text = L"LBT";
			// 
			// lblUBT
			// 
			this->lblUBT->AutoSize = true;
			this->lblUBT->Location = System::Drawing::Point(128, 77);
			this->lblUBT->Name = L"lblUBT";
			this->lblUBT->Size = System::Drawing::Size(28, 12);
			this->lblUBT->TabIndex = 19;
			this->lblUBT->Text = L"UBT";
			// 
			// lblAVG
			// 
			this->lblAVG->AutoSize = true;
			this->lblAVG->Location = System::Drawing::Point(128, 55);
			this->lblAVG->Name = L"lblAVG";
			this->lblAVG->Size = System::Drawing::Size(29, 12);
			this->lblAVG->TabIndex = 18;
			this->lblAVG->Text = L"AVG";
			// 
			// label28
			// 
			this->label28->AutoSize = true;
			this->label28->Location = System::Drawing::Point(9, 99);
			this->label28->Name = L"label28";
			this->label28->Size = System::Drawing::Size(78, 12);
			this->label28->TabIndex = 7;
			this->label28->Text = L"通道下端LBT:";
			// 
			// label27
			// 
			this->label27->AutoSize = true;
			this->label27->Location = System::Drawing::Point(9, 77);
			this->label27->Name = L"label27";
			this->label27->Size = System::Drawing::Size(79, 12);
			this->label27->TabIndex = 6;
			this->label27->Text = L"通道上端UBT:";
			// 
			// textBool
			// 
			this->textBool->Location = System::Drawing::Point(79, 18);
			this->textBool->Name = L"textBool";
			this->textBool->Size = System::Drawing::Size(63, 22);
			this->textBool->TabIndex = 6;
			this->textBool->Text = L"6005";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(6, 21);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(53, 12);
			this->label1->TabIndex = 5;
			this->label1->Text = L"商品代碼";
			// 
			// label16
			// 
			this->label16->AutoSize = true;
			this->label16->Location = System::Drawing::Point(94, 55);
			this->label16->Name = L"label16";
			this->label16->Size = System::Drawing::Size(0, 12);
			this->label16->TabIndex = 4;
			// 
			// btnGetBool
			// 
			this->btnGetBool->Location = System::Drawing::Point(213, 18);
			this->btnGetBool->Name = L"btnGetBool";
			this->btnGetBool->Size = System::Drawing::Size(75, 23);
			this->btnGetBool->TabIndex = 3;
			this->btnGetBool->Text = L"Get Bool";
			this->btnGetBool->UseVisualStyleBackColor = true;
			this->btnGetBool->Click += gcnew System::EventHandler(this, &SKQuote::btnGetBool_Click);
			// 
			// label26
			// 
			this->label26->AutoSize = true;
			this->label26->Location = System::Drawing::Point(9, 55);
			this->label26->Name = L"label26";
			this->label26->Size = System::Drawing::Size(56, 12);
			this->label26->TabIndex = 2;
			this->label26->Text = L"均線AVG:";
			// 
			// tabPage6
			// 
			this->tabPage6->Controls->Add(this->btn_cancelFTI);
			this->tabPage6->Controls->Add(this->Btn_RequestFutureTradeInfo);
			this->tabPage6->Controls->Add(this->text_StockNo);
			this->tabPage6->Controls->Add(this->label35);
			this->tabPage6->Controls->Add(this->tableLayoutPanel3);
			this->tabPage6->Location = System::Drawing::Point(4, 22);
			this->tabPage6->Name = L"tabPage6";
			this->tabPage6->Padding = System::Windows::Forms::Padding(3);
			this->tabPage6->Size = System::Drawing::Size(752, 532);
			this->tabPage6->TabIndex = 5;
			this->tabPage6->Text = L"Futures TradeInfo";
			this->tabPage6->UseVisualStyleBackColor = true;
			// 
			// btn_cancelFTI
			// 
			this->btn_cancelFTI->Location = System::Drawing::Point(413, 40);
			this->btn_cancelFTI->Name = L"btn_cancelFTI";
			this->btn_cancelFTI->Size = System::Drawing::Size(75, 23);
			this->btn_cancelFTI->TabIndex = 16;
			this->btn_cancelFTI->Text = L"Cancel";
			this->btn_cancelFTI->UseVisualStyleBackColor = true;
			this->btn_cancelFTI->Click += gcnew System::EventHandler(this, &SKQuote::btn_cancelFTI_Click);
			// 
			// Btn_RequestFutureTradeInfo
			// 
			this->Btn_RequestFutureTradeInfo->Location = System::Drawing::Point(324, 41);
			this->Btn_RequestFutureTradeInfo->Name = L"Btn_RequestFutureTradeInfo";
			this->Btn_RequestFutureTradeInfo->Size = System::Drawing::Size(75, 23);
			this->Btn_RequestFutureTradeInfo->TabIndex = 15;
			this->Btn_RequestFutureTradeInfo->Text = L"查詢";
			this->Btn_RequestFutureTradeInfo->UseVisualStyleBackColor = true;
			this->Btn_RequestFutureTradeInfo->Click += gcnew System::EventHandler(this, &SKQuote::Btn_RequestFutureTradeInfo_Click);
			// 
			// text_StockNo
			// 
			this->text_StockNo->Location = System::Drawing::Point(169, 41);
			this->text_StockNo->Name = L"text_StockNo";
			this->text_StockNo->Size = System::Drawing::Size(126, 22);
			this->text_StockNo->TabIndex = 14;
			this->text_StockNo->Text = L"TX00";
			// 
			// label35
			// 
			this->label35->AutoSize = true;
			this->label35->Location = System::Drawing::Point(96, 44);
			this->label35->Name = L"label35";
			this->label35->Size = System::Drawing::Size(53, 12);
			this->label35->TabIndex = 13;
			this->label35->Text = L"商品代碼";
			// 
			// tableLayoutPanel3
			// 
			this->tableLayoutPanel3->BackColor = System::Drawing::Color::Black;
			this->tableLayoutPanel3->ColumnCount = 4;
			this->tableLayoutPanel3->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				25)));
			this->tableLayoutPanel3->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				25)));
			this->tableLayoutPanel3->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				25)));
			this->tableLayoutPanel3->ColumnStyles->Add((gcnew System::Windows::Forms::ColumnStyle(System::Windows::Forms::SizeType::Percent,
				25)));
			this->tableLayoutPanel3->Controls->Add(this->lblStockIdx, 3, 0);
			this->tableLayoutPanel3->Controls->Add(this->lblMarketNo, 1, 0);
			this->tableLayoutPanel3->Controls->Add(this->label44, 2, 0);
			this->tableLayoutPanel3->Controls->Add(this->label43, 0, 0);
			this->tableLayoutPanel3->Controls->Add(this->label40, 0, 3);
			this->tableLayoutPanel3->Controls->Add(this->lblFTIBDC, 1, 3);
			this->tableLayoutPanel3->Controls->Add(this->label41, 2, 3);
			this->tableLayoutPanel3->Controls->Add(this->lblFTISDC, 3, 3);
			this->tableLayoutPanel3->Controls->Add(this->lblFTISq, 3, 2);
			this->tableLayoutPanel3->Controls->Add(this->label39, 2, 2);
			this->tableLayoutPanel3->Controls->Add(this->lblFTISc, 3, 1);
			this->tableLayoutPanel3->Controls->Add(this->label37, 2, 1);
			this->tableLayoutPanel3->Controls->Add(this->lblFTIBq, 1, 2);
			this->tableLayoutPanel3->Controls->Add(this->label38, 0, 2);
			this->tableLayoutPanel3->Controls->Add(this->label36, 0, 1);
			this->tableLayoutPanel3->Controls->Add(this->lblFTIBc, 1, 1);
			this->tableLayoutPanel3->Location = System::Drawing::Point(99, 85);
			this->tableLayoutPanel3->Name = L"tableLayoutPanel3";
			this->tableLayoutPanel3->RowCount = 4;
			this->tableLayoutPanel3->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 25)));
			this->tableLayoutPanel3->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 25)));
			this->tableLayoutPanel3->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Percent, 25)));
			this->tableLayoutPanel3->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Absolute, 20)));
			this->tableLayoutPanel3->RowStyles->Add((gcnew System::Windows::Forms::RowStyle(System::Windows::Forms::SizeType::Absolute, 20)));
			this->tableLayoutPanel3->Size = System::Drawing::Size(418, 82);
			this->tableLayoutPanel3->TabIndex = 12;
			// 
			// lblStockIdx
			// 
			this->lblStockIdx->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblStockIdx->AutoSize = true;
			this->lblStockIdx->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblStockIdx->ForeColor = System::Drawing::Color::Lime;
			this->lblStockIdx->Location = System::Drawing::Point(331, 5);
			this->lblStockIdx->Name = L"lblStockIdx";
			this->lblStockIdx->Size = System::Drawing::Size(84, 15);
			this->lblStockIdx->TabIndex = 12;
			this->lblStockIdx->Text = L"lblStockIdx";
			// 
			// lblMarketNo
			// 
			this->lblMarketNo->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblMarketNo->AutoSize = true;
			this->lblMarketNo->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblMarketNo->ForeColor = System::Drawing::Color::Lime;
			this->lblMarketNo->Location = System::Drawing::Point(114, 5);
			this->lblMarketNo->Name = L"lblMarketNo";
			this->lblMarketNo->Size = System::Drawing::Size(91, 15);
			this->lblMarketNo->TabIndex = 12;
			this->lblMarketNo->Text = L"lblMarketNo";
			// 
			// label44
			// 
			this->label44->AutoSize = true;
			this->label44->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label44->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label44->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label44->Location = System::Drawing::Point(211, 0);
			this->label44->Name = L"label44";
			this->label44->Size = System::Drawing::Size(68, 17);
			this->label44->TabIndex = 12;
			this->label44->Text = L"StockIdx";
			// 
			// label43
			// 
			this->label43->AutoSize = true;
			this->label43->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label43->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label43->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label43->Location = System::Drawing::Point(3, 0);
			this->label43->Name = L"label43";
			this->label43->Size = System::Drawing::Size(75, 17);
			this->label43->TabIndex = 12;
			this->label43->Text = L"MarketNo";
			// 
			// label40
			// 
			this->label40->AutoSize = true;
			this->label40->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label40->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label40->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label40->Location = System::Drawing::Point(3, 60);
			this->label40->Name = L"label40";
			this->label40->Size = System::Drawing::Size(89, 17);
			this->label40->TabIndex = 10;
			this->label40->Text = L"總成交買筆";
			// 
			// lblFTIBDC
			// 
			this->lblFTIBDC->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblFTIBDC->AutoSize = true;
			this->lblFTIBDC->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblFTIBDC->ForeColor = System::Drawing::Color::Lime;
			this->lblFTIBDC->Location = System::Drawing::Point(124, 67);
			this->lblFTIBDC->Name = L"lblFTIBDC";
			this->lblFTIBDC->Size = System::Drawing::Size(81, 15);
			this->lblFTIBDC->TabIndex = 15;
			this->lblFTIBDC->Text = L"lblFTIBDC";
			// 
			// label41
			// 
			this->label41->AutoSize = true;
			this->label41->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label41->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label41->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label41->Location = System::Drawing::Point(211, 60);
			this->label41->Name = L"label41";
			this->label41->Size = System::Drawing::Size(89, 17);
			this->label41->TabIndex = 10;
			this->label41->Text = L"總成交賣筆";
			// 
			// lblFTISDC
			// 
			this->lblFTISDC->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblFTISDC->AutoSize = true;
			this->lblFTISDC->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblFTISDC->ForeColor = System::Drawing::Color::Lime;
			this->lblFTISDC->Location = System::Drawing::Point(335, 67);
			this->lblFTISDC->Name = L"lblFTISDC";
			this->lblFTISDC->Size = System::Drawing::Size(80, 15);
			this->lblFTISDC->TabIndex = 16;
			this->lblFTISDC->Text = L"lblFTISDC";
			// 
			// lblFTISq
			// 
			this->lblFTISq->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblFTISq->AutoSize = true;
			this->lblFTISq->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblFTISq->ForeColor = System::Drawing::Color::Lime;
			this->lblFTISq->Location = System::Drawing::Point(348, 45);
			this->lblFTISq->Name = L"lblFTISq";
			this->lblFTISq->Size = System::Drawing::Size(67, 15);
			this->lblFTISq->TabIndex = 14;
			this->lblFTISq->Text = L"lblFTISq";
			// 
			// label39
			// 
			this->label39->AutoSize = true;
			this->label39->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label39->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label39->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label39->Location = System::Drawing::Point(211, 40);
			this->label39->Name = L"label39";
			this->label39->Size = System::Drawing::Size(73, 17);
			this->label39->TabIndex = 10;
			this->label39->Text = L"總委賣口";
			// 
			// lblFTISc
			// 
			this->lblFTISc->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblFTISc->AutoSize = true;
			this->lblFTISc->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblFTISc->ForeColor = System::Drawing::Color::Lime;
			this->lblFTISc->Location = System::Drawing::Point(349, 25);
			this->lblFTISc->Name = L"lblFTISc";
			this->lblFTISc->Size = System::Drawing::Size(66, 15);
			this->lblFTISc->TabIndex = 12;
			this->lblFTISc->Text = L"lblFTISc";
			// 
			// label37
			// 
			this->label37->AutoSize = true;
			this->label37->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label37->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label37->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label37->Location = System::Drawing::Point(211, 20);
			this->label37->Name = L"label37";
			this->label37->Size = System::Drawing::Size(73, 17);
			this->label37->TabIndex = 10;
			this->label37->Text = L"總委賣筆";
			// 
			// lblFTIBq
			// 
			this->lblFTIBq->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblFTIBq->AutoSize = true;
			this->lblFTIBq->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblFTIBq->ForeColor = System::Drawing::Color::Lime;
			this->lblFTIBq->Location = System::Drawing::Point(137, 45);
			this->lblFTIBq->Name = L"lblFTIBq";
			this->lblFTIBq->Size = System::Drawing::Size(68, 15);
			this->lblFTIBq->TabIndex = 13;
			this->lblFTIBq->Text = L"lblFTIBq";
			// 
			// label38
			// 
			this->label38->AutoSize = true;
			this->label38->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label38->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label38->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label38->Location = System::Drawing::Point(3, 40);
			this->label38->Name = L"label38";
			this->label38->Size = System::Drawing::Size(73, 17);
			this->label38->TabIndex = 10;
			this->label38->Text = L"總委買口";
			// 
			// label36
			// 
			this->label36->AutoSize = true;
			this->label36->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->label36->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->label36->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->label36->Location = System::Drawing::Point(3, 20);
			this->label36->Name = L"label36";
			this->label36->Size = System::Drawing::Size(73, 17);
			this->label36->TabIndex = 9;
			this->label36->Text = L"總委買筆";
			// 
			// lblFTIBc
			// 
			this->lblFTIBc->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblFTIBc->AutoSize = true;
			this->lblFTIBc->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(136)));
			this->lblFTIBc->ForeColor = System::Drawing::Color::Lime;
			this->lblFTIBc->Location = System::Drawing::Point(138, 25);
			this->lblFTIBc->Name = L"lblFTIBc";
			this->lblFTIBc->Size = System::Drawing::Size(67, 15);
			this->lblFTIBc->TabIndex = 11;
			this->lblFTIBc->Text = L"lblFTIBc";
			// 
			// tabPage7
			// 
			this->tabPage7->Controls->Add(this->txt_StrikePriceCount);
			this->tabPage7->Controls->Add(this->listStrikePrices);
			this->tabPage7->Controls->Add(this->groupBox7);
			this->tabPage7->Location = System::Drawing::Point(4, 22);
			this->tabPage7->Name = L"tabPage7";
			this->tabPage7->Padding = System::Windows::Forms::Padding(3);
			this->tabPage7->Size = System::Drawing::Size(752, 532);
			this->tabPage7->TabIndex = 6;
			this->tabPage7->Text = L"OptionStrikePrice";
			this->tabPage7->UseVisualStyleBackColor = true;
			// 
			// txt_StrikePriceCount
			// 
			this->txt_StrikePriceCount->AutoSize = true;
			this->txt_StrikePriceCount->Location = System::Drawing::Point(187, 91);
			this->txt_StrikePriceCount->Name = L"txt_StrikePriceCount";
			this->txt_StrikePriceCount->Size = System::Drawing::Size(0, 12);
			this->txt_StrikePriceCount->TabIndex = 21;
			// 
			// listStrikePrices
			// 
			this->listStrikePrices->FormattingEnabled = true;
			this->listStrikePrices->ItemHeight = 12;
			this->listStrikePrices->Location = System::Drawing::Point(9, 93);
			this->listStrikePrices->Name = L"listStrikePrices";
			this->listStrikePrices->Size = System::Drawing::Size(725, 424);
			this->listStrikePrices->TabIndex = 20;
			// 
			// groupBox7
			// 
			this->groupBox7->Controls->Add(this->GetStrikePrices);
			this->groupBox7->Controls->Add(this->label48);
			this->groupBox7->Location = System::Drawing::Point(9, 24);
			this->groupBox7->Name = L"groupBox7";
			this->groupBox7->Size = System::Drawing::Size(149, 59);
			this->groupBox7->TabIndex = 19;
			this->groupBox7->TabStop = false;
			this->groupBox7->Text = L"選擇權商品";
			// 
			// GetStrikePrices
			// 
			this->GetStrikePrices->Location = System::Drawing::Point(6, 24);
			this->GetStrikePrices->Name = L"GetStrikePrices";
			this->GetStrikePrices->Size = System::Drawing::Size(124, 25);
			this->GetStrikePrices->TabIndex = 21;
			this->GetStrikePrices->Text = L"GetStrikePrice";
			this->GetStrikePrices->UseVisualStyleBackColor = true;
			this->GetStrikePrices->Click += gcnew System::EventHandler(this, &SKQuote::GetStrikePrices_Click);
			// 
			// label48
			// 
			this->label48->AutoSize = true;
			this->label48->Location = System::Drawing::Point(94, 55);
			this->label48->Name = L"label48";
			this->label48->Size = System::Drawing::Size(0, 12);
			this->label48->TabIndex = 4;
			// 
			// tabPage8
			// 
			this->tabPage8->Controls->Add(this->label42);
			this->tabPage8->Controls->Add(this->RequestStockListBtn);
			this->tabPage8->Controls->Add(this->MarketNo_txt);
			this->tabPage8->Controls->Add(this->StockList);
			this->tabPage8->Location = System::Drawing::Point(4, 22);
			this->tabPage8->Name = L"tabPage8";
			this->tabPage8->Padding = System::Windows::Forms::Padding(3);
			this->tabPage8->Size = System::Drawing::Size(752, 532);
			this->tabPage8->TabIndex = 7;
			this->tabPage8->Text = L"StockList";
			this->tabPage8->UseVisualStyleBackColor = true;
			// 
			// label42
			// 
			this->label42->AutoSize = true;
			this->label42->Location = System::Drawing::Point(25, 57);
			this->label42->Name = L"label42";
			this->label42->Size = System::Drawing::Size(52, 12);
			this->label42->TabIndex = 25;
			this->label42->Text = L"MarketNo";
			// 
			// RequestStockListBtn
			// 
			this->RequestStockListBtn->Location = System::Drawing::Point(167, 50);
			this->RequestStockListBtn->Name = L"RequestStockListBtn";
			this->RequestStockListBtn->Size = System::Drawing::Size(135, 29);
			this->RequestStockListBtn->TabIndex = 24;
			this->RequestStockListBtn->Text = L"RequestStockList";
			this->RequestStockListBtn->UseVisualStyleBackColor = true;
			this->RequestStockListBtn->Click += gcnew System::EventHandler(this, &SKQuote::RequestStockListBtn_Click);
			// 
			// MarketNo_txt
			// 
			this->MarketNo_txt->Location = System::Drawing::Point(96, 54);
			this->MarketNo_txt->Name = L"MarketNo_txt";
			this->MarketNo_txt->Size = System::Drawing::Size(22, 22);
			this->MarketNo_txt->TabIndex = 23;
			// 
			// StockList
			// 
			this->StockList->FormattingEnabled = true;
			this->StockList->HorizontalScrollbar = true;
			this->StockList->ItemHeight = 12;
			this->StockList->Location = System::Drawing::Point(19, 103);
			this->StockList->Name = L"StockList";
			this->StockList->ScrollAlwaysVisible = true;
			this->StockList->Size = System::Drawing::Size(712, 400);
			this->StockList->TabIndex = 22;
			// 
			// txtMinuteNumber
			// 
			this->txtMinuteNumber->Location = System::Drawing::Point(527, 91);
			this->txtMinuteNumber->Name = L"txtMinuteNumber";
			this->txtMinuteNumber->Size = System::Drawing::Size(70, 22);
			this->txtMinuteNumber->TabIndex = 81;
			this->txtMinuteNumber->Text = L"1";
			// 
			// label59
			// 
			this->label59->AutoSize = true;
			this->label59->Location = System::Drawing::Point(377, 96);
			this->label59->Name = L"label59";
			this->label59->Size = System::Drawing::Size(140, 12);
			this->label59->TabIndex = 80;
			this->label59->Text = L"MinuteNumber(指定幾分K)";
			// 
			// SKQuote
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->tabControl1);
			this->Controls->Add(this->btnDisconnect);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->ConnectedLabel);
			this->Controls->Add(this->btnIsConnected);
			this->Controls->Add(this->groupBox1);
			this->Name = L"SKQuote";
			this->Size = System::Drawing::Size(792, 659);
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->tabControl1->ResumeLayout(false);
			this->tabPage1->ResumeLayout(false);
			this->tabPage1->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->tabPage2->ResumeLayout(false);
			this->tabPage2->PerformLayout();
			this->groupBox5->ResumeLayout(false);
			this->groupBox5->PerformLayout();
			this->groupBox4->ResumeLayout(false);
			this->groupBox4->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->GridBest5Bid))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->GridBest5Ask))->EndInit();
			this->tabPage3->ResumeLayout(false);
			this->tabPage3->PerformLayout();
			this->tabPage4->ResumeLayout(false);
			this->tabPage4->PerformLayout();
			this->tableLayoutPanel2->ResumeLayout(false);
			this->tableLayoutPanel2->PerformLayout();
			this->tableLayoutPanel1->ResumeLayout(false);
			this->tableLayoutPanel1->PerformLayout();
			this->tabPage5->ResumeLayout(false);
			this->tabPage5->PerformLayout();
			this->groupBox6->ResumeLayout(false);
			this->groupBox6->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->tabPage6->ResumeLayout(false);
			this->tabPage6->PerformLayout();
			this->tableLayoutPanel3->ResumeLayout(false);
			this->tableLayoutPanel3->PerformLayout();
			this->tabPage7->ResumeLayout(false);
			this->tabPage7->PerformLayout();
			this->groupBox7->ResumeLayout(false);
			this->groupBox7->PerformLayout();
			this->tabPage8->ResumeLayout(false);
			this->tabPage8->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion



};
}
 