#include "SKCenterLib.h"
#include "SKQuoteLib.h"
#include "SKReplyLib.h"
#include "SKOrderLib.h"
#include <conio.h>
#include <thread>

CSKCenterLib* pSKCenterLib;
CSKQuoteLib* pSKQuoteLib;
CSKReplyLib* pSKReplyLib;
CSKOrderLib* pSKOrderLib;

long g_nCode = 0;
string g_strUserId;

void Order()
{
	bool bOrder = true;
	string strStockNo = "", strPrice = "", strNo = "";
	short sPrime = 0, sPeriod = 0, sFlag = 0, sBuySell = 0, sTradeType = 0, sDayTrade = 0, sNewClose = 0, sReserved = 0;
	int nOrderType = 0, nMarket = 0, nType = 0;
	long nQty = 0, nTradeType = 0, nSpecialTradeType = 0;
	
	//初始化
	g_nCode = pSKOrderLib->Initialize();
	pSKCenterLib->PrintfCodeMessage("Order", "Initialize", g_nCode);

	//讀取憑證
	g_nCode = pSKOrderLib->ReadCertByID(g_strUserId);
	pSKCenterLib->PrintfCodeMessage("Order", "ReadCertByID", g_nCode);

	//取得帳號
	g_nCode = pSKOrderLib->GetUserAccount();
	pSKCenterLib->PrintfCodeMessage("Order", "GetUserAccount", g_nCode);

	while (bOrder)
	{
		cout <<  endl << "請選擇功能 (1：下單, 2：刪單, 3：改價, 4：改量, -1：退出)：";
		cin >> nOrderType;
		switch(nOrderType)
		{
			case 1:
				cout << "請選擇市場 (0：證券, 1：期貨, 2：選擇權, -1：退出)：";
				cin >> nOrderType;
				switch (nOrderType)
				{
					case 0:
						cout << "請輸入商品代碼：";
						cin >> strStockNo;
						cout << "請輸入公司種類 (0：上市櫃, 1：興櫃)：";
						cin >> sPrime;
						cout << "請輸入下單時段 (0：盤中, 1：盤後, 2：盤後零股)：";
						cin >> sPeriod;
						cout << "請輸入股票種類 (0：現股, 1：融資, 2：融券)：";
						cin >> sFlag;
						cout << "請輸入買賣別 (0：買, 1：賣)：";
						cin >> sBuySell;
						cout << "請輸入價格：";
						cin >> strPrice;
						cout << "請輸入數量：";
						cin >> nQty;
						cout << "請輸入ROD/IOC/FOK (0：ROD, 1：IOC, 2：FOK)：";
						cin >> nTradeType;
						cout << "請輸入限市價 (1：市價, 2：限價)：";
						cin >> nSpecialTradeType;
					
						g_nCode = pSKOrderLib->SendStockOrder(g_strUserId, false, strStockNo, sPrime, sPeriod, sFlag, sBuySell, strPrice, nQty, nTradeType, nSpecialTradeType);
						
						pSKCenterLib->PrintfCodeMessage("Order", "SendStockOrder", g_nCode);

						break;
					case 1:
					case 2:
						cout << "請輸入商品代碼：";
						cin >> strStockNo;
						cout << "請輸入ROD/IOC/FOK (0：ROD, 1：IOC, 2：FOK)：";
						cin >> sTradeType;
						cout << "請輸入買賣別 (0：買, 1：賣)：";
						cin >> sBuySell;
						cout << "請輸入是否當沖 (0：否, 1：是)：";
						cin >> sDayTrade;
						cout << "請輸入新平倉 (0：新倉, 1：平倉, 2：自動)：";
						cin >> sNewClose;
						cout << "請輸入價格：";
						cin >> strPrice;
						cout << "請輸入數量：";
						cin >> nQty;
						cout << "請輸入盤別 (0：盤中, 1：T盤預約)：";
						cin >> sReserved;
						
						if (nOrderType == 1)
						{
							g_nCode = pSKOrderLib->SendFutureOrder(g_strUserId, false, strStockNo, sTradeType, sBuySell, sDayTrade, sNewClose, strPrice, nQty, sReserved);
							pSKCenterLib->PrintfCodeMessage("Order", "SendFutureOrder", g_nCode);
						}
						else if (nOrderType == 2)
						{
							g_nCode = pSKOrderLib->SendOptionOrder(g_strUserId, false, strStockNo, sTradeType, sBuySell, sDayTrade, sNewClose, strPrice, nQty, sReserved);
							pSKCenterLib->PrintfCodeMessage("Order", "SendOptionOrder", g_nCode);
						}

						break;
					case -1:
						break;
					default:
						cout << "輸入代碼錯誤，請重新輸入" << endl;
						break;
				}
				break;
			case 2:
				cout << "請輸入市場別 (0：證券, 1：期貨, 2：選擇權)：";
				cin >> nMarket;
				cout << "請輸入刪單類別 (0：序號刪單, 1：書號刪單, 2：商品代號刪單)：";
				cin >> nType;
				cout << "請輸入刪單碼 (序號、書號、代號)：";
				cin >> strNo;

				g_nCode = pSKOrderLib->CancelOrder(g_strUserId, false, nMarket, nType, strNo);

				pSKCenterLib->PrintfCodeMessage("Order", "CancelOrder", g_nCode);

				break;
			case 3:
				cout << "請輸入市場別 (0：證券, 1：期貨, 2：選擇權)：";
				cin >> nMarket;
				cout << "請輸入改價類別 (0：序號改價, 1：書號改價)：";
				cin >> nType;
				cout << "請輸入改價碼 (序號、書號)：";
				cin >> strNo;
				cout << "請輸入改價價格：";
				cin >> strPrice;
				cout << "請輸入ROD/IOC/FOK (0：ROD, 1：IOC, 2：FOK)：";
				cin >> nTradeType;
				g_nCode = pSKOrderLib->CorrectPrice(g_strUserId, false, nMarket, nType, strNo, strPrice, nTradeType);

				pSKCenterLib->PrintfCodeMessage("Order", "CorrectPrice", g_nCode);

				break;
			case 4:
				cout << "請輸入市場別 (0：證券, 1：期貨, 2：選擇權)：";
				cin >> nMarket;
				cout << "請輸入改量碼 (序號)：";
				cin >> strNo;
				cout << "請輸入減量數量：";
				cin >> nQty;

				g_nCode = pSKOrderLib->DecreaseOrder(g_strUserId, false, nMarket, strNo, nQty);

				pSKCenterLib->PrintfCodeMessage("Order", "CorrectPrice", g_nCode);
				break;
			case -1:
				bOrder = false;
				break;
			default:
				cout << "輸入代碼錯誤，請重新輸入" << endl;
				break;
		}
	}
}

void Reply()
{
	bool bReply = true;
	int nReplyType = 0;

	while (bReply)
	{
		cout << endl << "請選擇功能 (1：回報連線, 2：連線狀態, 3：回報斷線, -1：退出)：";
		cin >> nReplyType;
		switch (nReplyType)
		{
			case 1 :
				g_nCode = pSKReplyLib->SKReplyLib_ConnectByID(g_strUserId);
				
				pSKCenterLib->PrintfCodeMessage("Reply", "SKReplyLib_ConnectByID", g_nCode);
				break;
			case 2:
				g_nCode = pSKReplyLib->SKReplyLib_IsConnectedByID(g_strUserId);

				cout << "SKReplyLib_IsConnectedByID (0：未連線, 1：連線, 2：下載中)：" << g_nCode << endl;
				break;
			case 3:
				g_nCode = pSKReplyLib->SKReplyLib_SolaceCloseByID(g_strUserId);

				pSKCenterLib->PrintfCodeMessage("Reply", "SKReplyLib_SolaceCloseByID", g_nCode);
				break;
			case -1:
				bReply = false;
				break;
			default:
				cout << "輸入代碼錯誤，請重新輸入" << endl;
				break;
		}
	}
}

void Quote()
{
	bool bQuote = true;
	int QuoteType = 0;
	short sPageNo = -1, sMarket = 0;
	string strStockNo = "";

	// 報價功能
	while (bQuote)
	{
		// 選擇功能
		cout << endl << "輸入代號(1：報價連線, 2：連線狀態, 3：報價斷線, 4：Quote, 5：Ticks & Best5, 6：StockList, -1：離開報價功能) ：";
		
		cin >> QuoteType;
		switch (QuoteType)
		{
			case 1:
				g_nCode = pSKQuoteLib->EnterMonitorLONG();

				pSKCenterLib->PrintfCodeMessage("Quote", "EnterMonitor", g_nCode);
				break;
			case 2:
				g_nCode = pSKQuoteLib->IsConnected();

				cout << "SKQuoteLib_IsConnected (0：未連線, 1：連線, 2：下載中)：" << g_nCode << endl;
				break;
			case 3:
				g_nCode = pSKQuoteLib->LeaveMonitor();

				pSKCenterLib->PrintfCodeMessage("Quote", "LeaveMonitor", g_nCode);
				break;
			case 4:
				if (pSKQuoteLib->IsConnected() != 1)
				{
					cout << "尚未連線" << endl;
					break;
				}
				cout << "請輸入商品代碼 (多筆以,分隔)：";
				cin >> strStockNo;

				sPageNo = 1;
				g_nCode = pSKQuoteLib->RequestStocks(&sPageNo, strStockNo);

				pSKCenterLib->PrintfCodeMessage("Quote", "RequestStocks", g_nCode);
				break;
			case 5:
				if (pSKQuoteLib->IsConnected() != 1)
				{
					cout << "尚未連線" << endl;
					break;
				}
				cout << "請輸入商品代碼 (限單筆)：";
				cin >> strStockNo;

				sPageNo = -1;
				g_nCode = pSKQuoteLib->RequestTicks(&sPageNo, strStockNo);

				pSKCenterLib->PrintfCodeMessage("Quote", "RequestTicks", g_nCode);
				break;
			case 6:
				if (pSKQuoteLib->IsConnected() != 1)
				{
					cout << "尚未連線" << endl;
					break;
				}
				cout << "請輸入市場代碼 (0：上市, 1：上櫃, 2：期貨, 3：選擇權, 4：興櫃, 5：上市盤中零股, 6：上櫃盤中零股)：";
				cin >> sMarket;

				g_nCode = pSKQuoteLib->RequestStockList(sMarket);

				pSKCenterLib->PrintfCodeMessage("Quote", "RequestStockList", g_nCode);
				break;
			case -1:
				bQuote = false;
				break;
			default:
				cout << "輸入代碼錯誤，請重新輸入" << endl;
				break;
		}
	}
}

void init()
{
	pSKCenterLib = new CSKCenterLib;
	pSKQuoteLib = new CSKQuoteLib;
	pSKReplyLib = new CSKReplyLib;
	pSKOrderLib = new CSKOrderLib;
}

void release()
{
	delete pSKCenterLib;
	delete pSKQuoteLib;
	delete pSKReplyLib;
	delete pSKOrderLib;

	CoUninitialize();
}

void thread_main()
{
	int nServiceType;
	bool bWhile = true;
	while (bWhile)
	{
		printf("\n請選擇要使用的項目 (1：下單 , 2：回報 , 3：報價 , -1：離開)：");

		cin >> nServiceType;
		switch (nServiceType)
		{
			case -1:
				bWhile = false;
				printf("離開程式\n");
				break;
			case 1:
				Order();
				break;
			case 2:
				Reply();
				break;
			case 3:
				Quote();
				break;
			default:
				printf("輸入代碼錯誤，請重新輸入\n");
				break;
		}
	}

	release();

	exit(0);
}

int main()
{
	CoInitialize(NULL);

	init();

	printf("請輸入身分證字號：");
	cin >> g_strUserId;

	printf("請輸入密碼：");
	string pwd;
	HANDLE hStdin = GetStdHandle(STD_INPUT_HANDLE);
	DWORD mode = 0;
	GetConsoleMode(hStdin, &mode);
	SetConsoleMode(hStdin, mode & (~ENABLE_ECHO_INPUT));
	cin >> pwd;
	cout << endl;

	g_nCode = pSKCenterLib->Login(g_strUserId.c_str(), pwd.c_str());

	pSKCenterLib->PrintfCodeMessage("Center", "Login", g_nCode);

	if (g_nCode != 0)
	{
		return 0;
	}

	SetConsoleMode(hStdin, mode);

	thread tMain(thread_main); 
	if (tMain.joinable())
		tMain.detach();

	MSG msg;
	while (GetMessageW(&msg, NULL, 0, 0)) // Get SendMessage loop 
	{
		DispatchMessageW(&msg);
	}

	return 0;
}
