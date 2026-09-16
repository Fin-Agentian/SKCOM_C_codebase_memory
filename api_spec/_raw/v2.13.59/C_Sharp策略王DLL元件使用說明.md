# C_Sharp策略王DLL元件使用說明

> 來源：Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/C_Sharp策略王DLL元件使用說明.docx

VS2019引用SKDLLCSharp.dll
| 1、VS2019 引用：Add >> Reference >> Brose 選 SKDLLCSharp.dll |
|---|
| 2、程式碼加入：using SKDLLCSharp; |
| 3、使用SK範例：SK.SKCenterLib_Login()，於事件、函式前加入 SK. |

登入
OnReplyMessage
| 當有公告將主動呼叫函式，並通知公告類訊息。 |
|---|
| 宣告 | event Action<string> OnReplyMessage; |
| 參數 | strLoginID | 登入ID |
|  | strMessage | 每一筆資料以「,」分隔每一個欄位 / MsgNo　// 訊息編號 / StartTime　// 訊息開始日期時間 / EndTime　// 訊息結束時間 / Message　// 訊息內容 |
| 備註 | 需在登入前註冊事件 OnReplyMessage， / 否則登入時會收到錯誤代碼 2017。 |
| 範例程式碼 | // listOnReplyMessage is a ListBox used to display incoming reply messages / SK.OnReplyMessage += (strLoginID, strMessage) => // 註冊 OnReplyMessage 事件，當有新公告訊息時觸發 / {// 根據收到的訊息更新 UI 或處理資料 / if (listOnReplyMessage.InvokeRequired) / { / listOnReplyMessage.Invoke(new Action(() => / { / listOnReplyMessage.Items.Add("[OnReplyMessage]回傳值:" + strLoginID + "_" + strMessage); // 例如顯示訊息內容 / })); / } / else / { / listOnReplyMessage.Items.Add("[OnReplyMessage]回傳值:" + strLoginID + "_" + strMessage); / } / }; |

Login
| 元件初始登入。在使用此 Library 前必須先通過使用者的雙因子(憑證綁定)身份認證，方可使用。 |
|---|
| 宣告 | LoginResult Login(string id, string pwd); / LoginResult Login(string id, string pwd, int flag); / LoginResult Login(string id, string pwd, int flag, string cert); / LoginResult Login(string id, string pwd, int flag, string cert, string path); |
| 參數 | id | 登入ID |
|  | pwd | 密碼 |
|  | flag | 環境：0:正式、1:正式SGX、2:測試、3:測試SGX |
|  | cert | AP及APH帳號需帶入子帳ID (其它帳號不需帶入ID，帶空字串) |
|  | path | 設定CapitalLOG存放路徑 / (預設同執行檔路徑，帶空字串為預設) |
| 回傳值 | struct LoginResult / { / int Code; // 0表示初始化成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。(失敗時，其餘欄位為null) / string RawAccountData; // 原始帳號資訊(以#號分隔每一組帳號資訊,逗點分隔每一個欄位,欄位依序為：『市場,分公司,分公司代號,帳號,身份證字號,姓名,是否為VIP(1:是、0:否)』) / List<AccountData> TSAccounts; //證券帳號資訊 / List<AccountData> OSAccounts; //複委託帳號資訊 / List<AccountData> TFAccounts; //內期帳號資訊 / List<AccountData> OFAccounts; //海期帳號資訊 / } / struct AccountData / { / string LoginID; // 登入ID / string Type; // 市場 / string Branch; // 分公司 / string Account; // 帳號 / string FullAccount { get; } // 分公司+帳號 / } |
| 備註 | 需在登入前註冊事件 OnReplyMessage， / 否則登入時會收到錯誤代碼 2017。 / 登入失敗時可由log查詢失敗原因。 |
| 範例程式碼 | // 取得使用者輸入的登入帳號與密碼 / string strLoginID = textBoxUserID.Text; / string strPassword = textBoxPassword.Text; / // 從權限選單中取得選擇的登入旗標（0=正式、1=測試...） / int nFlag = comboBoxAuthorityFlag.SelectedIndex; / // 呼叫群益 API 執行登入，回傳結果包含帳號資訊與狀態碼 / var result = SK.Login(strLoginID, strPassword, nFlag); / // 檢查登入是否成功（Code 為 0 表示成功） / if (result.Code == 0) / { / // ===== 登入成功後，將各帳號類型加入對應的 ComboBox 供使用者選擇 ===== / // TS 帳號：證券帳號，顯示登入ID與完整帳號 / foreach (var account in result.TSAccounts) / comboBoxTS.Items.Add($"{account.LoginID} {account.FullAccount}"); / // OS 帳號：複委託帳號，顯示登入ID與完整帳號 / foreach (var account in result.OSAccounts) / comboBoxOS.Items.Add($"{account.LoginID} {account.FullAccount}"); / // TF 帳號：內期帳號，顯示登入ID與完整帳號 / foreach (var account in result.TFAccounts) / comboBoxTF.Items.Add($"{account.LoginID} {account.FullAccount}"); / // OF 帳號：海期帳號，顯示登入ID與完整帳號 / foreach (var account in result.OFAccounts) / comboBoxOF.Items.Add($"{account.LoginID} {account.FullAccount}"); / // 自動選取每個 ComboBox 的第一個帳號作為預設值 / if (comboBoxTS.Items.Count > 0) comboBoxTS.SelectedIndex = 0; / if (comboBoxOS.Items.Count > 0) comboBoxOS.SelectedIndex = 0; / if (comboBoxTF.Items.Count > 0) comboBoxTF.SelectedIndex = 0; / if (comboBoxOF.Items.Count > 0) comboBoxOF.SelectedIndex = 0; / } / // 登入後在 ListBox 顯示登入結果訊息（由 API 回傳的文字） / listOnReplyMessage.Items.Add("[Login]: " + SK.GetMessage(result.Code)); |

連線主機(回報/行情/Proxy下單)
OnConnection
| 接收連線狀態。 |
|---|
| 宣告 | event Action<string, int> OnConnection; |
| 參數 | loginID | 回報：登入ID / 國內行情：SKQuote |
|  | Code | 其他錯誤代碼，可參考代碼定義表 |
| 備註 | 避免在此處直接進行訂閱報價行為，若各交易所商品未下載完成， / 即無法進行商品訂閱。 |
| 範例程式碼 | // 當連線狀態改變時觸發，顯示使用者登入狀態或錯誤訊息到 ListBox / SK.OnConnection += (loginID, code) => / { / // 檢查是否在 UI 執行緒中，避免跨執行緒操作 UI / if (listOnReplyMessage.InvokeRequired) / { / listOnReplyMessage.Invoke(new Action(() => / { / // 在 UI 執行緒中顯示連線狀態訊息 / listOnReplyMessage.Items.Add($"[OnConnection]使用者 {loginID} 狀態碼: {SK.GetMessage(code)}"); / })); / } / else / { / // 已在 UI 執行緒，直接顯示訊息 / listOnReplyMessage.Items.Add($"[OnConnection]使用者 {loginID} 狀態碼: {SK.GetMessage(code)}"); / } / }; |

ManageServerConnection
| 與(回報/國內行情/下單)主機建立連線。 |
|---|
| 宣告 | int ManageServerConnection(string strLogInID, int nStatus, int nTargetType); |
| 參數 | strLogInID | 登入ID(用於「回報、下單連線」時填入；若為「行情連線」，請填空字串"") |
|  | nStatus | 0:連線 / 1:斷線 / 2:重連(僅下單) / 3:連線(不下載商品檔(僅國內)) |
|  | nTargetType | 連線用途(0:回報；1:國內行情；4:Proxy下單) |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 連線狀態由 OnConnection 事件回傳。 / 連線回報、行情需先簽署證券或期貨API下單聲明書，方可使用 / 斷線國內行情時，會中斷國內行情以及所有回報連線 |
| 範例程式碼 | // 取得使用者選擇的連線狀態（0=連線，1=斷線...） / int nStatus = comboBoxStatus.SelectedIndex; / // 取得目標伺服器類型（例如回報、行情...） / int nTargetType = comboBoxTargetType.SelectedIndex; / // 呼叫群益 API 控制伺服器連線狀態 / int result = SK.ManageServerConnection(textBoxUserID.Text, nStatus, nTargetType); / // 顯示執行結果（由 API 回傳的狀態訊息） / listOnReplyMessage.Items.Add("[ManageServerConnection]回傳值:" + SK.GetMessage(result)); |

LoadCommodity
| 下載指定市場商品檔。 |
|---|
| 宣告 | int LoadCommodity(int nMarketNo); |
| 參數 | nMarketNo | 0: 上市 / 1: 上櫃 / 2: 期貨T盤行情 / 3: 選擇權T盤行情 / 4: 興櫃 / 5: 零股-上市 / 6: 零股-上櫃 / 7: 期貨全盤行情 / 8: 選擇權全盤行情 / 9: 客製化期貨 / 10: 客製化選擇權 / 11: 全市場 (包含前述0~10市場) / 12: 股票市場 (包含 上市上櫃、零股-上市上櫃、興櫃) / 13: 期貨市場 (包含 期選T盤、全盤、客期選) / 14: 海期市場 / 15: 海選市場 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 連線狀態由 OnConnection 事件回傳。 / 需先簽署證券或期貨API下單聲明書，方可使用 |
| 範例程式碼 | // 取得選擇的市場代碼（0=上市, 1=上櫃, 2=期貨...） / int nMarketNo = comboBoxnMarketNo.SelectedIndex; / // 呼叫群益 API 下載該市場的商品資訊（股票、期貨等） / int result = SK.LoadCommodity(nMarketNo); / // 顯示下載結果（由 API 回傳的訊息）到 ListBox / listOnReplyMessage.Items.Add("[LoadTSCommodity]回傳值:" + SK.GetMessage(result)); |

RequestStockList
| *請先執行 LoadCommodity 下載商品檔。 / 列出指定市場商品檔。 / 可再透過GetTypeNo 取出指定產業類別的商品檔(例如: GetTypeNo(1) 取出類別為 水泥 的商品檔) |
|---|
| 宣告 | StockListParser RequestStockList(int nMarketNo); |
| 參數 | nMarketNo | 0: 上市 / 1: 上櫃 / 2: 期貨行情 / 3: 選擇權行情 / 4: 興櫃 / 9: 客製化期貨 / 10: 客製化選擇權 |
| 回傳值 | class StockListParser / { / StockListParser(string rawData); / List<StockListResult> AllTypeLists { get; } // 該市場下所有商品資料（每個 TypeNo 下的商品） / List<string> GetAllType(); // 從物件中再特別取出 TypeNo 資料 (例如 "1水泥", "2食品") / StockListResult GetTypeNo(int typeNo); // 從物件中再特別取出 TypeNo 下的商品 / } / class StockListResult / { / StockListResult(); / int TypeNo { get; set; }// 類別代號 / string TypeName { get; set; } // 類別名稱 / List<StockInfo> Items { get; set; } / string All { get; } / } / class StockInfo / { / StockInfo(); / string strQuoteCode { get; set; } // 行情代號 / string strStockName { get; set; } // 商品名稱 / string strOrderCode { get; set; } // 下單代號 / string strExpiryDate { get; set; } // 最後交易日(僅期選) / override string ToString(); / } |
| 備註 | 連線狀態由 OnConnection 事件回傳。 / 需先簽署證券或期貨API下單聲明書，方可使用 / 產業類別代碼對照表 (另外也可透過 GetAllType(); // 從物件中再特別取出 TypeNo 資料 (例如 "1水泥", "2食品")) / 上市： / 上櫃： / 期貨： / 選擇權： / 興櫃： / 客期： |
| 範例程式碼 | // ✅ 呼叫封裝方法，取得物件 / var parser = SK.RequestStockList(nMarketNo); / // ✅ 顯示該市場下所有商品資料（每個 TypeNo 下的商品） / foreach (var result in parser.AllTypeLists) / { / StockList.Items.Add($"【類別:{result.TypeNo},{result.TypeName}】"); / foreach (var item in result.Items) / { / StockList.Items.Add($"  {item.strQuoteCode},{item.strStockName},{item.strOrderCode},{item.strExpiryDate}"); // 取出 行情代號,商品名稱,下單代號,最後交易日(僅期選) / } / } / // ✅ 從物件中再特別取出 TypeNo 下的商品 / int typeNo = int.Parse(TypeNo_txt.Text); / var result2 = parser.GetTypeNo(typeNo); / if (result2 != null) / { / StockList.Items.Add($"【%{result2.TypeNo}% {result2.TypeName}】"); / foreach (var item in result2.Items) / { / StockList.Items.Add($"  {item.strQuoteCode},{item.strStockName},{item.strOrderCode},{item.strExpiryDate}"); // 取出 行情代號,商品名稱,下單代號,最後交易日(僅期選) / } / } / else / { / StockList.Items.Add($"查無 TypeNo {typeNo}"); / } / // ✅ 從該市場中再取出 TypeNo 資料(例如 "1水泥", "2食品") / string AllType = ""; / var typeList = parser.GetAllType(); / foreach (var item in typeList) / { / AllType += item; // 例如 "1水泥", "2食品" / } |

新單
OnProxyOrder
| Proxy委託結果。 |
|---|
| 宣告 | event Action<int, int, string> OnProxyOrder; |
| 參數 | StampID | StampID |
|  | Code | Proxy收單結果代碼。 |
|  | Message | Proxy收單回傳訊息。 |
| 備註 | 送出委託後會回傳二筆通知 |
| 範例程式碼 | // 當委託單（Proxy Order）有狀態變更時觸發，顯示 StampID、狀態碼與訊息 / SK.OnProxyOrder += (StampID, Code, Message) => / {// 根據收到的訊息更新 UI 或處理資料 / if (listOnReplyMessage.InvokeRequired) / { / listOnReplyMessage.Invoke(new Action(() => / { / listOnReplyMessage.Items.Add($"[OnProxyOrder]回傳值: StampID[{StampID}] 狀態碼: {Code} 訊息: {Message}");  // 例如顯示訊息內容 / })); / } / else / { / listOnReplyMessage.Items.Add($"[OnProxyOrder]回傳值: StampID[{StampID}] 狀態碼: {Code} 訊息: {Message}"); / } / }; |

證券 SendStockProxyOrder
| 經由Socket送出證券委託。 |
|---|
| 宣告 | (int Code, string Message) SendStockProxyOrder(string loginID, string stockNo, string fullAccount, string price, string orderType, int specialTradeType, int tradeType, int period, int qty); / (int Code, string Message) SendStockProxyOrder(string loginID, string stockNo, string fullAccount, string price, string orderType, int specialTradeType, int tradeType, int period, int qty, int PriceMark); |
| 參數 | loginID | 登入ID。 |
|  | stockNo | 委託股票代號 |
|  | fullAccount | 證券帳號，分公司代碼＋帳號7碼 |
|  | price | 委託價格 |
|  | orderType | 下單類別(1: 現股買進。2: 現股賣出。3: 融資買進。4: 融資賣出。5: 融券買進。6: 融券賣出。7: 無券賣出) |
|  | specialTradeType | 1:市價  2:限價 |
|  | tradeType | 0:ROD ; 1:IOC ; 2:FOK |
|  | period | 0:盤中 1:零股 2:盤後交易 3:盤中零股 |
|  | qty | 股數 |
|  | PriceMark | 價格旗標 0:一般定價 1:前日收盤價 2:漲停 3:跌停 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendStockProxyOrder(loginID, strStockNo, account, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty); / //var (nCode2, message2) = SK.SendStockProxyOrder(loginID, strStockNo, account, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty, nPriceMark); / listOnReplyMessage.Items.Add("[SendStockProxyOrder]回傳值:" + nCode + " " + message); |

內期 SendFutureProxyOrder
| 經由Socket送出期貨委託，需設倉別與盤別。 |
|---|
| 宣告 | (int Code, string Message) SendFutureProxyOrder(string loginID, string fullAccount, string stockNo, string settleYM, int buySell, int priceFlag, int dayTrade, string orderType, int reserved, int qty, string price, int tradeType); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號，Broker id (例如: F020000)＋帳號7碼 |
|  | stockNo | 委託商品代號 ex:FITX |
|  | settleYM | 指定月份商品契約年月，共6碼EX:202212 |
|  | buySell | 0:買進 1:賣出 |
|  | priceFlag | 0:市價  1:限價 2:範圍市價 |
|  | dayTrade | 當沖0:否 1:是，可當沖商品請參考交易所規定。 |
|  | orderType | 0:新倉 1:平倉 2:自動 |
|  | reserved | 0:盤中單 1:預約單 |
|  | qty | 交易口數{組合部位} |
|  | price | 委託價格 |
|  | tradeType | 0:ROD 1:IOC 2:FOK |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendFutureProxyOrder(loginID, account, strFutureNo, strYM, nBS, nPriceFlag, nDayTrade, strOrderType, nPreOrder, nQty, strPrice, nCOND); / listOnReplyMessage.Items.Add("[SendFutureProxyOrder]回傳值:" + nCode + " " + message); |

內選 SendOptionProxyOrder
| 經由Socket送出選擇權委託 |
|---|
| 宣告 | (int Code, string Message) SendOptionProxyOrder(string loginID, string fullAccount, string stockNo, string price, string settleYM, string strike, string orderType, int reserved, int qty, int cp, int buySell, int priceFlag, int tradeType, int dayTrade); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號，Broker id (例如: F020000)＋帳號7碼 |
|  | stockNo | 委託商品代號 ex:TXO |
|  | price | 委託價格 |
|  | settleYM | 指定月份商品契約年月，共6碼EX:202212 |
|  | strike | 履約價1 |
|  | orderType | 0:新倉 1:平倉 2:自動 |
|  | reserved | 0:盤中單 1:預約單 |
|  | qty | 交易口數{組合部位} |
|  | cp | 0:CALL  1:PUT |
|  | buySell | 0:買進 1:賣出 |
|  | priceFlag | 0:市價  1:限價 2:範圍市價 |
|  | tradeType | 0:ROD 1:IOC 2:FOK |
|  | dayTrade | 當沖0:否 1:是，可當沖商品請參考交易所規定。 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendOptionProxyOrder(loginID, account, strFutureNo, strPrice, strYM, strStrike, strOrderType, nPreOrder, nQty, nCP, nBS, nPriceFlag, nCOND, nDayTrade); / listOnReplyMessage.Items.Add("[SendOptionProxyOrder]回傳值:" + nCode + " " + message); |

複式單 SendDuplexProxyOrder
| 經由Socket送出選擇權複式下單。 |
|---|
| 宣告 | (int Code, string Message) SendDuplexProxyOrder(string loginID, string fullAccount, string stockNo, string price, string settleYM, string strike, string settleYM2, string strike2, string orderType, int reserved, int qty, int cp, int buySell, string stockNo2, int cp2, int buySell2, int priceFlag, int tradeType, int dayTrade); |
| 參數 | strLogInID | 登入ID。 |
|  | fullAccount | 期貨帳號，Broker id (例如: F020000)＋帳號7碼 |
|  | stockNo | 委託商品代號 ex:TXO |
|  | price | 委託價格 |
|  | settleYM | 指定月份商品契約年月，共6碼EX:202212 |
|  | strike | 履約價1 |
|  | settleYM2 | 指定月份商品契約年月2，共6碼EX:202301 |
|  | strike2 | 履約價2 |
|  | orderType | 0:新倉 1:平倉 2:自動 |
|  | reserved | 0:盤中單 1:預約單 |
|  | qty | 交易口數{組合部位} |
|  | cp | 買賣權1 ( 0:CALL  1:PUT ) |
|  | buySell | 買賣別1 ( 0:買進 1:賣出) |
|  | stockNo2 | 委託商品代號2 |
|  | cp2 | 買賣權2 ( 0:CALL  1:PUT ) |
|  | buySell2 | 買賣別2 ( 0:買進 1:賣出) |
|  | priceFlag | 0:市價  1:限價 2:範圍市價 |
|  | tradeType | 1:IOC 2:FOK |
|  | dayTrade | 當沖0:否 1:是，可當沖商品請參考交易所規定。 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendDuplexProxyOrder(loginID, account, strFutureNo, strPrice, strYM, strStrike, strYM2, strStrike2, strOrderType, nPreOrder, nQty, nCP, nBS, strFutureNo2, nCP2, nBS2, nPriceFlag, nCOND, nDayTrade); / listOnReplyMessage.Items.Add("[SendDuplexProxyOrder]回傳值:" + nCode + " " + message); |

海期 SendOverseaFutureProxyOrder
| 經由Socket送出海期下單。 |
|---|
| 宣告 | (int Code, string Message) SendOverseaFutureProxyOrder(string loginID, string fullAccount, string exchangeNo, string stockNo, string yearMonth, string order, string orderNumerator, string trigger, string triggerNumerator, int buySell, int newClose, int dayTrade, int tradeType, int specialTradeType, int qty); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 海期帳號，分公司代碼＋帳號7碼 |
|  | exchangeNo | 交易所代碼 |
|  | stockNo | 海外期權代號 |
|  | yearMonth | 近月商品年月( YYYYMM) 6碼 |
|  | order | 委託價 |
|  | orderNumerator | 委託價分子 |
|  | trigger | 觸發價 |
|  | triggerNumerator | 觸發價分子 |
|  | buySell | 0:買進 1:賣出 {價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號} |
|  | newClose | 新平倉，0:新倉  {目前海期僅新倉可選} |
|  | dayTrade | 當沖0:否 1:是；{海期價差單不提供當沖}  //可當沖商品請參考交易所規定。 |
|  | tradeType | 0:ROD     1:IOC   2:FOK //{限價單LMT可選ROD/IOC/FOK，其餘單別固定ROD} |
|  | specialTradeType | 0:LMT 限價單 1:MKT  2:STL  3.STP |
|  | qty | 交易口數 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendOverseaFutureProxyOrder(loginID, account, strTradeName, strStockNo, strYearMonth, strOrder, strOrderNumerator, strTrigger, strTriggerNumerator, nBuySell, nNewClose, nDayTrade, nTradeType, nSpecialTradeType, nQty); / listOnReplyMessage.Items.Add("[SendOverseaFutureProxyOrder]回傳值:" + nCode + " " + message); |

海期價差 SendOverseaFutureSpreadProxyOrder
| *需要先執行 LoadCommodity，下載 14 海期市場 / 經由Socket送出海期價差單下單。 |
|---|
| 宣告 | (int Code, string Message) SendOverseaFutureSpreadProxyOrder(string loginID, string fullAccount, string exchangeNo, string stockNo, string nearMonth, string farMonth, string order, string orderNumerator, string trigger, string triggerNumerator, int buySell, int newClose, int dayTrade, int tradeType, int specialTradeType, int qty); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 海期帳號，分公司代碼＋帳號7碼 |
|  | exchangeNo | 交易所代碼 |
|  | stockNo | 海外期權代號 |
|  | nearMonth | 近月商品年月( YYYYMM) 6碼 |
|  | farMonth | 遠月商品年月( YYYYMM) 6碼 {價差下單使用} |
|  | order | 委託價 |
|  | orderNumerator | 委託價分子 |
|  | trigger | 觸發價 |
|  | triggerNumerator | 觸發價分子 |
|  | buySell | 0:買進 1:賣出 {價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號} |
|  | newClose | 新平倉，0:新倉  {目前海期僅新倉可選} |
|  | dayTrade | 當沖0:否 1:是；{海期價差單不提供當沖}  //可當沖商品請參考交易所規定。 |
|  | tradeType | 0:ROD     1:IOC   2:FOK //{限價單LMT可選ROD/IOC/FOK，其餘單別固定ROD} |
|  | specialTradeType | 0:LMT 限價單 1:MKT  2:STL  3.STP |
|  | qty | 交易口數 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendOverseaFutureSpreadProxyOrder(loginID, account, strTradeName, strStockNo, strYearMonth, strYearMonth2, strOrder, strOrderNumerator, strTrigger, strTriggerNumerator, nBuySell, nNewClose, nDayTrade, nTradeType, nSpecialTradeType, nQty); / listOnReplyMessage.Items.Add("[SendOverseaFutureSpreadProxyOrder]回傳值:" + nCode + " " + message); |

海選 SendOverseaOptionProxyOrder
| 經由Socket送出海選下單。 |
|---|
| 宣告 | (int Code, string Message) SendOverseaOptionProxyOrder(string loginID, string fullAccount, string exchangeNo, string stockNo, string yearMonth, string order, string orderNumerator, string orderDenominator, string trigger, string triggerNumerator, int buySell, int newClose, int dayTrade, int tradeType, int specialTradeType, string strikePrice, int callPut, int qty); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 海期帳號，分公司代碼＋帳號7碼 |
|  | exchangeNo | 交易所代碼 |
|  | stockNo | 海外期權代號 |
|  | yearMonth | 近月商品年月( YYYYMM) 6碼 |
|  | order | 委託價 |
|  | orderNumerator | 委託價分子 |
|  | orderDenominator | 委託價分母 |
|  | trigger | 觸發價 |
|  | triggerNumerator | 觸發價分子 |
|  | buySell | 0:買進 1:賣出 {價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號} |
|  | newClose | 新平倉，0:新倉 1:平倉 {目前海選可使用新、平倉} |
|  | dayTrade | 當沖0:否 1:是 //可當沖商品請參考交易所規定。 |
|  | tradeType | 0:ROD  {目前海選均固定ROD} |
|  | specialTradeType | 0:LMT 限價單 1:MKT  2:STL  3.STP |
|  | strikePrice | 履約價。{選擇權使用} |
|  | callPut | 0:CALL  1:PUT {選擇權使用} |
|  | qty | 交易口數。 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendOverseaOptionProxyOrder(loginID, account, strTradeName, strStockNo, strYearMonth, strOrder, strOrderNumerator, strOrderDeno, strTrigger, strTriggerNumerator, nBuySell, nNewClose, nDayTrade, nTradeType, nSpecialTradeType, strStrikePrice, nCallPut, nQty); / listOnReplyMessage.Items.Add("[SendOverseaOptionProxyOrder]回傳值:" + nCode + " " + message); |

複委託 SendForeignStockProxyOrder
| 經由Socket送出複委託下單。 |
|---|
| 宣告 | (int Code, string Message) SendForeignStockProxyOrder(string loginID, string fullAccount, string stockNo, string exchangeNo, string price, string currency1, string currency2, string currency3, string proxyQty, int accountType, int orderType, int tradeType); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 複委託帳號，分公司代碼＋帳號7碼 |
|  | stockNo | 委託股票代號 |
|  | exchangeNo | 交易所代碼，US：美股， HK：港股，JP：日股， SP：新加坡，SG：新(幣)坡股，SA: 滬股，HA: 深股 |
|  | price | 委託價格 |
|  | currency1 | 扣款幣別，幣別順序1  (幣別可輸入 : HKD、NTD、USD、JPY、SGD、EUR、AUD、CNY、GBP) |
|  | currency2 | 扣款幣別，幣別順序2 |
|  | currency3 | 扣款幣別，幣別順序3 |
|  | proxyQty | 委託量 (股數。只有賣出美股且庫存別為定額(VIEWTRADE)時，股數才能有小數位數，其餘都必須為整數) |
|  | accountType | 專戶別種類，1:外幣專戶 2:台幣專戶 |
|  | orderType | 1:買 2:賣 |
|  | tradeType | 庫存別 ，賣出美股時必填 //1:[美股]一般/定股(CITI) 2:定額(VIEWTRADE) 3:其他股市(一般) 非賣出美股可輸入0 :表示填空值 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnConnection為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendForeignStockProxyOrder(loginID, account, strStockNo, strExchangeNo, strPrice, strCurrency1, strCurrency2, strCurrency3, strProxyQty, nAccountType, nBidAsk + 1, nTradeType); / listOnReplyMessage.Items.Add("[SendForeignStockProxyOrder]回傳值:" + nCode + " " + message); |

刪改單
證券 SendStockProxyAlter
| 經由Socket送出證券刪改單。 |
|---|
| 宣告 | (int Code, string Message) SendStockProxyAlter(string loginID, string stockNo, string fullAccount, string seqNo, string bookNo, string price, string orderType, int specialTradeType, int tradeType, int period, int qty); / (int Code, string Message) SendStockProxyAlter(string loginID, string stockNo, string fullAccount, string seqNo, string bookNo, string price, string orderType, int specialTradeType, int tradeType, int period, int qty, int priceMark); |
| 參數 | loginID | 登入ID。 |
|  | stockNo | 委託股票代號 |
|  | fullAccount | 證券帳號，分公司代碼＋帳號7碼 |
|  | seqNo | 委託序號 |
|  | bookNo | 委託書號 |
|  | price | 委託價格(改價:必填，帶””表示市價。減量:帶0，刪單:帶””或是原始價) |
|  | orderType | 下單類別 (0:刪單。1:改量。2:改價。) |
|  | specialTradeType | 1:市價 2:限價 |
|  | tradeType | 0:ROD ; 1:IOC ; 2:FOK |
|  | period | 0:盤中 1:零股 2:盤後交易 3:盤中零股 |
|  | qty | 股數(改價:帶0。減量:帶要減的量。刪單:帶原始量) |
|  | priceMark | 價格旗標 0:一般定價 1:前日收盤價 2:漲停 3:跌停 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendStockProxyAlter(loginID, strStockNo, account, strSeqNo, strOrderNo, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty); / //var (nCode2, message2) = SK.SendStockProxyAlter(loginID, strStockNo, account, strSeqNo, strOrderNo, strPrice, OrderType, nPriceType, nTimeInForce, nMarketType, Qty, nPriceMark); / listOnReplyMessage.Items.Add("[SendStockProxyAlter]回傳值:" + nCode + " " + message); |

內期 SendFutureProxyAlter
| 經由Socket送出期貨刪改單。 |
|---|
| 宣告 | (int Code, string Message) SendFutureProxyAlter(string loginID, string fullAccount, string orderType, string price, int reserved, int qty, int tradeType, string bookNo, string seqNo); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號，Broker id (例如: F020000)＋帳號7碼 |
|  | orderType | 0:刪單 1:減量 2:改價 |
|  | price | 委託價格(改量:帶0，也可以帶""。改價:帶需要改的價。刪單:帶原始價，也可帶"") |
|  | reserved | 0:盤中單 1:預約單 |
|  | qty | 交易口數(改量 : 帶要減的量。改價 : 帶0也可帶""。刪單 : 帶原始量) |
|  | tradeType | 0:ROD 1:IOC 2:FOK |
|  | bookNo | 委託書號 |
|  | seqNo | 委託序號 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendFutureProxyAlter(loginID, account, strOrderType, strPrice, nPreOrder, nQty, nCOND, strOrderNo, strSeqNo); / listOnReplyMessage.Items.Add("[SendFutureProxyAlter]回傳值:" + nCode + " " + message); |

內選 SendOptionProxyAlter
| 經由Socket送出選擇權刪改單。 |
|---|
| 宣告 | (int Code, string Message) SendOptionProxyAlter(string loginID, string fullAccount, string orderType, string price, int reserved, int qty, int tradeType, string bookNo, string seqNo); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號，Broker id (例如: F020000)＋帳號7碼 |
|  | orderType | 0:刪單 1:減量 2:改價 |
|  | price | 委託價格(改量:帶0，也可以帶""。改價:帶需要改的價。刪單:帶原始價，也可帶"") |
|  | reserved | 0:盤中單 1:預約單 |
|  | qty | 交易口數(改量 : 帶要減的量。改價 : 帶0也可帶""。刪單 : 帶原始量) |
|  | tradeType | 0:ROD 1:IOC 2:FOK |
|  | bookNo | 委託書號 |
|  | seqNo | 委託序號 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendOptionProxyAlter(loginID, account, strOrderType, strPrice, nPreOrder, nQty, nCOND, strOrderNo, strSeqNo); / listOnReplyMessage.Items.Add("[SendOptionProxyAlter]回傳值:" + nCode + " " + message); |

海期選 SendOverseaFutureProxyAlter
| 經由Socket送出海期選刪改單。 |
|---|
| 宣告 | (int Code, string Message) SendOverseaFutureProxyAlter(string loginID, string fullAccount, string exchangeNo, string stockNo, string yearMonth, string yearMonth2, string order, string orderNumerator, string orderDenominator, int newClose, int tradeType, int specialTradeType, int qty, string bookNo, string seqNo, int spreadFlag, int alterType, string strikePrice, int callPut); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 海期帳號，分公司代碼＋帳號7碼 |
|  | exchangeNo | 交易所代碼 |
|  | stockNo | 海外期權代號 |
|  | yearMonth | 近月商品年月( YYYYMM) 6碼 |
|  | yearMonth2 | 遠月商品年月( YYYYMM) 6碼 {價差刪改單使用} |
|  | order | 委託價 |
|  | orderNumerator | 委託價分子 |
|  | orderDenominator | 委託價分母 |
|  | newClose | 新平倉，0:新倉 1:平倉 {目前海選可使用新、平倉} |
|  | tradeType | 0:ROD  {目前海選均固定ROD} |
|  | specialTradeType | 0:LMT 限價單 1:MKT  2:STL  3.STP |
|  | qty | 交易口數 |
|  | bookNo | 書號 |
|  | seqNo | 原始13碼序號 |
|  | spreadFlag | 0 :OF海期; 1: OF-spread 海期價差;  2: OO 海選 |
|  | alterType | 0: Cancel 刪單;1: Decrease 減量; 2: Correct 改價 |
|  | strikePrice | 履約價。{選擇權使用} |
|  | callPut | 0:CALL  1:PUT {選擇權使用} |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendOverseaFutureProxyAlter(loginID, account, strTradeName, strStockNo, strYearMonth, strYearMonth2, strOrder, strOrderNumerator, strOrderD, nNewClose, nTradeType, nSpecialTradeType, nQty, strOrderNo, strSeqNo, nSpread, nFunCode, strStrikePrice, nCallPut); / listOnReplyMessage.Items.Add("[SendOverseaFutureProxyAlter]回傳值:" + nCode + " " + message); |

複委託 SendForeignStockProxyCancel
| 經由Socket送出複委託刪單。 |
|---|
| 宣告 | (int Code, string Message) SendForeignStockProxyCancel(string loginID, string fullAccount, string stockNo, string exchangeNo, string seqNo, string bookNo); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 委託帳號，分公司代碼＋帳號7碼 |
|  | stockNo | 委託股票代號 |
|  | exchangeNo | 交易所代碼，US：美股， HK：港股，JP：日股， SP：新加坡，SG：新(幣)坡股，SA: 滬股，HA: 深股 |
|  | seqNo | 13碼序號[刪單時需填入] |
|  | bookNo | 委託書號[刪單時需填入] |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：ORKEY；委託送出失敗：錯誤訊息 |
| 備註 | 透過 Socket 委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendForeignStockProxyCancel(loginID, account, strStockNo, strExchangeNo, strSeqNo, strOrderNo); / listOnReplyMessage.Items.Add("[SendForeignStockProxyCancel]回傳值:" + nCode + " " + message); |

帳務
證券
證券即時庫存查詢 GetRealBalanceReport
| 證券即時庫存查詢。 |
|---|
| 宣告 | (int Code, string Message) GetRealBalanceReport (string strLogInID, string strAccount); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 證券帳號， / BrokerID(例如:9182)＋帳號7碼 |
| 回傳值 | public class RealBalanceReport / { / string StockCode { get; set; }            // 股票代號 / string InventoryType { get; set; }        // 庫存種類 / (T:集保 C:融資 L:融券) / string CreditLimitOriginal { get; set; }  // 資額度(原始) / string CreditLimitAvailable { get; set; } // 資額度(可用) / string ShortLimitOriginal { get; set; }   // 券額度(原始) / string ShortLimitAvailable { get; set; }  // 券額度(可用) / string YesterdayInventory { get; set; }   // 股數:昨日庫存 / string TodayBidQty { get; set; }          // 今日委買 / string TodayAskQty { get; set; }          // 今日委賣 / string TodayBuyMatched { get; set; }      // 今日買進成交 / string TodaySellMatched { get; set; }     // 今日賣出成交 / string AvailableToSellOrCover { get; set; // 今日資券可回   補/集保庫存可賣出 / string DayTradeBuyableQty { get; set; }   // 可資沖股數 / string DayTradeSellableQty { get; set; }  // 可券沖股數 / string RealTimeInventory { get; set; }    // 即時庫存 / string RealTimeMarginRate { get; set; }  // 即時個股維持率 / string LoginID { get; set; }              // LOGIN_ID / string AccountNo { get; set; }            // ACCOUNT_NO / int StatusCode { get; set; }         // 回傳狀態碼 / string Message { get; set; }         // 錯誤訊息 / string RawData { get; set; }         // 原始字串 / } / public class RealBalanceReportParserResult / { / int StatusCode { get; set; }          // 回傳狀態碼 / string Message { get; set; }          // 錯誤訊息 / string RawData { get; set; }          // 原始字串 / List<RealBalanceReport> Blocks { get; set; } = new List<RealBalanceReport>(); / } |
| 備註 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message成功：空字串；失敗：錯誤訊息 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / var result = SK.GetRealBalanceReport(loginID, account); / listOnReplyMessage.Items.Add("[GetRealBalanceReport]回傳值:" + result.StatusCode + " " + result.Message); / if (result.StatusCode == 0 && result.Blocks.Count > 0) / { / foreach (var block in result.Blocks) / { / listOnReplyMessage.Items.Add( / $"帳號資料: {block.LoginID}-{block.AccountNo}" / ); / listOnReplyMessage.Items.Add( / $"股票代號: {block.StockCode},庫存種類: {block.InventoryType},資額度(原始): {block.CreditLimitOriginal},資額度(可用): {block.CreditLimitAvailable}," + / $"券額度(原始): {block.ShortLimitOriginal},券額度(可用): {block.ShortLimitAvailable},昨日庫存: {block.YesterdayInventory},今日委買: {block.TodayBidQty},今日委賣: {block.TodayAskQty}," + / $"今日買進成交: {block.TodayBuyMatched},今日賣出成交: {block.TodaySellMatched},可賣出/回補: {block.AvailableToSellOrCover},可資沖股數: {block.DayTradeBuyableQty},可券沖股數: {block.DayTradeSellableQty}," + / $"即時庫存: {block.RealTimeInventory},即時個股維持率: {block.RealTimeMarginRate}" / ); / listOnReplyMessage.Items.Add("--------------------------------------------------"); / } / } / else / { / listOnReplyMessage.Items.Add("[GetRealBalanceReport]沒有資料或回傳錯誤"); / } |

內期
部位互抵(大小微台、大小電金) SendTFOffset
| 部位互抵(大小微台、大小電金)。 |
|---|
| 宣告 | (int Code, string Message)SendTFOffset(string strLogInID, string strAccount, int nCommodity, string strYearMonth, int nBuySell, int nQty, int nQty2, int nQty3); |
| 參數 | strLogInID | 登入ID。 |
|  | strAccount | 委託帳號(IB＋帳號) |
|  | nCommodity | 指定商品0:大抵微; 1:小抵微; 2:大小抵微; 3:大抵小微; 4:小抵大微; 5:大抵小; 6:大小電; 7:大小金 |
|  | strYearMonth | 年月(YYYYMM)ex:202509 |
|  | nBuySell | 大買賣別。0:多方(買) 1:空方(賣) |
|  | nQty | 互抵口數，以大台(電、金)口數為基本單位。 |
|  | nQty2 | 互抵口數，以小台口數為基本單位。 / (如使用6:大小電; 7:大小金，此欄位帶0) |
|  | nQty3 | 互抵口數，以微台口數為基本單位。 / (如使用6:大小電; 7:大小金，此欄位帶0) |
| 回傳值 | 如果Code = 0表示成功，訊息內容則為互抵訊息。回傳值非0表示互抵失敗，訊息內容為失敗原因。 / 錯誤代碼可參考對照表。 |
| 備註 |  |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.SendTFOffset(loginID, account, nCommodity, strYearMonth, nBidAsk, nQty, nQty_2, nQty_3); / listOnReplyMessage.Items.Add("[SendTFOffset]回傳值:" + nCode + " " + message); |

國內期貨未平倉查詢 GetOpenInterestGW
| 國內期貨未平倉查詢。 |
|---|
| 宣告 | (int Code, string Message)GetOpenInterestGW(string strLogInID, string strAccount, int nFormat); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號， / BrokerID(例如:F020000)＋帳號7碼 |
|  | nFormat | 指定格式0:預設 |
| 回傳值 | class OpenInterest / { / string MarketType { get; set; }      // 市場別 / string Account { get; set; }         // 帳號 / string FutureNo { get; set; }        // 商品 / string BuySell { get; set; }         // 買賣別 / string Qty { get; set; }             // 未平倉部位 / string Qty_Trade { get; set; }       // 當沖未平倉部位 / string AvgPrice { get; set; }  // 平均成本(小數部分已處理) / string Fee { get; set; }             // 單口手續費 / string Tax { get; set; }             // 交易稅(萬分之X) / string LoginID { get; set; }         // LOGIN_ID / int StatusCode { get; set; }         // 回傳狀態碼 / string Message { get; set; }         // 錯誤訊息 / string RawData { get; set; }         // 原始字串 / } / class OpenInterestParserResult / { / int StatusCode { get; set; }          // 回傳狀態碼 / string Message { get; set; }          // 錯誤訊息 / string RawData { get; set; }          // 原始字串 / List<OpenInterest> Blocks { get; set; } = new List<OpenInterest>(); / } |
| 備註 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message成功：空字串；失敗：錯誤訊息 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / var result = SK.GetOpenInterestGW(loginID, account, nFormat); / listOnReplyMessage.Items.Add("[GetOpenInterestGW]回傳值:" + result.StatusCode + " " + result.Message); / if (result.StatusCode == 0 && result.Blocks.Count > 0) / { / foreach (var block in result.Blocks) / { / listOnReplyMessage.Items.Add( / $"帳號資料: {block.MarketType}-({block.Account}-{block.LoginID})" / ); / listOnReplyMessage.Items.Add( / $"商品: {block.FutureNo}, 買賣別: {block.BuySell}, 未平倉口數: {block.Qty}, " + / $"當沖未平口數: {block.Qty_Trade}, 平均成本: {block.AvgPrice}, 手續費: {block.Fee}, 交易稅: {block.Tax}" / ); / listOnReplyMessage.Items.Add("--------------------------------------------------"); / } / } / else / { / listOnReplyMessage.Items.Add("[GetOpenInterestGW]沒有資料或回傳錯誤"); / } |

國內權益數查詢 GetFutureRights
| 國內權益數查詢。 |
|---|
| 宣告 | (int Code, string Message) GetFutureRights (string strLogInID, string strAccount, int nCoinType); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號， / BrokerID(例如:F020000)＋帳號7碼 |
|  | nCoinType | 幣別： / 0:全幣別，1:基幣(台幣TWD)，2:人民幣RMB |
| 回傳值 | public class FutureRights / { / string AccountBalance { get; set; }         // 帳戶餘額 / string FloatingPL { get; set; }             // 浮動損益 / string RealizedFee { get; set; }            // 已實現費用 / string TransactionTax { get; set; }         // 交易稅 / string WithheldPremium { get; set; }        // 預扣權利金 / string PremiumPayment { get; set; }         // 權利金收付 / string Equity { get; set; }                 // 權益數 / string ExcessMargin { get; set; }           // 超額保證金 / string DepositWithdrawal { get; set; }      // 存提款 / string BuyerMarketValue { get; set; }       // 買方市值 / string SellerMarketValue { get; set; }      // 賣方市值 / string FuturesClosePL { get; set; }         // 期貨平倉損益 / string IntradayUnrealized { get; set; }     // 盤中未實現 / string InitialMargin { get; set; }          // 原始保證金 / string MaintenanceMargin { get; set; }      // 維持保證金 / string PositionInitialMargin { get; set; }  // 部位原始保證金 / string PositionMaintenanceMargin { get; set; } // 部位維持保證金 / string OrderMargin { get; set; }             // 委託保證金 / string ExcessOptimalMargin { get; set; } //超額最佳保證金 / string TotalPremiumValue { get; set; }       // 權利總值 / string WithheldFee { get; set; }             // 預扣費用 / string OriginalInitialMargin { get; set; }  // 原始保證金 / string YesterdayBalance { get; set; }        // 昨日餘額 / string OptionComboMarginFlag { get; set; }   // 選擇權組合單加不加收保證金 / string MaintenanceRate { get; set; }         // 維持率 / string Currency { get; set; }                // 幣別 / string FullInitialMargin { get; set; }  // 足額原始保證金 / string FullMaintenanceMargin { get; set; //足額維持保證金 / string FullAvailable { get; set; }           // 足額可用 / string OffsetAmount { get; set; }            // 抵繳金額 / string SecuritiesAvailable { get; set; }     // 有價可用 / string AvailableBalance { get; set; }        // 可用餘額 / string FullCashAvailable { get; set; }    // 足額現金可用 / string SecuritiesValue { get; set; }         // 有價價值 / string RiskIndicator { get; set; }           // 風險指標 / string OptionExpiryDifference { get; set; }  // 選擇權到期差異 / string OptionExpiryPL { get; set; }    // 選擇權到期差損 / string FuturesExpiryPL { get; set; }      // 期貨到期損益 / string AdditionalMargin { get; set; }        // 加收保證金 / string LoginID { get; set; }                 // LOGIN_ID / string AccountNo { get; set; }         // ACCOUNT_NO / int StatusCode { get; set; }       // 回傳狀態碼 / string Message { get; set; }       // 錯誤訊息 / string RawData { get; set; }       // 原始字串 / } / public class FutureRightsParserResult / { / int StatusCode { get; set; }      // 回傳狀態碼 / string Message { get; set; }      // 錯誤訊息 / string RawData { get; set; }      // 原始字串 / List<FutureRights> Blocks { get; set; } = new List<FutureRights>(); / } |
| 備註 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message成功：空字串；失敗：錯誤訊息 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / var result = SK.GetFutureRights(loginID, account, nCoinType); / listOnReplyMessage.Items.Add("[GetFutureRights]回傳值:" + result.StatusCode + " " + result.Message); / if (result.StatusCode == 0 && result.Blocks.Count > 0) / { / foreach (var block in result.Blocks) / { / listOnReplyMessage.Items.Add( / $"帳號資料: {block.LoginID}-{block.AccountNo}" / ); / listOnReplyMessage.Items.Add( / $"帳戶餘額: {block.AccountBalance},浮動損益: {block.FloatingPL},已實現費用: {block.RealizedFee},交易稅: {block.TransactionTax},預扣權利金: {block.WithheldPremium}," + / $"權利金收付: {block.PremiumPayment},權益數: {block.Equity},超額保證金: {block.ExcessMargin},存提款: {block.DepositWithdrawal},買方市值: {block.BuyerMarketValue}," + / $"賣方市值: {block.SellerMarketValue},期貨平倉損益: {block.FuturesClosePL},盤中未實現: {block.IntradayUnrealized},原始保證金: {block.InitialMargin},維持保證金: {block.MaintenanceMargin}," + / $"部位原始保證金: {block.PositionInitialMargin},部位維持保證金: {block.PositionMaintenanceMargin},委託保證金: {block.OrderMargin},超額最佳保證金: {block.ExcessOptimalMargin},權利總值: {block.TotalPremiumValue}," + / $"預扣費用: {block.WithheldFee},原始保證金(原始): {block.OriginalInitialMargin},昨日餘額: {block.YesterdayBalance},選擇權組合單加收保證金: {block.OptionComboMarginFlag},維持率: {block.MaintenanceRate}," + / $"幣別: {block.Currency},足額原始保證金: {block.FullInitialMargin},足額維持保證金: {block.FullMaintenanceMargin},足額可用: {block.FullAvailable},抵繳金額: {block.OffsetAmount}," + / $"有價可用: {block.SecuritiesAvailable},可用餘額: {block.AvailableBalance},足額現金可用: {block.FullCashAvailable},有價價值: {block.SecuritiesValue},風險指標: {block.RiskIndicator}," + / $"選擇權到期差異: {block.OptionExpiryDifference},選擇權到期差損: {block.OptionExpiryPL},期貨到期損益: {block.FuturesExpiryPL},加收保證金: {block.AdditionalMargin}" / ); / listOnReplyMessage.Items.Add("--------------------------------------------------"); / } / } / else / { / listOnReplyMessage.Items.Add("[GetFutureRights]沒有資料或回傳錯誤"); / } |

海期
國內外期貨保證金互轉 WithDraw
| 國內外出入金互轉。 |
|---|
| 宣告 | (int Code, string Message) WithDraw(string strLogInID, string strFullAccountOut, int nTypeOut, string strFullAccountIn, int nTypeIn, int nCurrency, string strDollars, string strPassword); |
| 參數 | strLogInID | 登入ID。 |
|  | strFullAccountOut | 轉出期貨帳號(分公司代碼＋帳號7碼) |
|  | nTypeOut | 轉出類別(0:國內；1:國外) |
|  | strFullAccountIn | 轉入期貨帳號(分公司代碼＋帳號7碼) |
|  | nTypeIn | 轉入類別(0:國內；1:國外) |
|  | nCurrency | 幣別 |
|  | strDollars | 金額 |
|  | strPassword | 出入金密碼 |
| 回傳值 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message委託送出成功：成功訊息；委託送出失敗：錯誤訊息 |
| 備註 |  |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 參數設定程式碼省略(可參考完整C#範例程式) / var (nCode, message) = SK.WithDraw(strLoginID, strAccountOut, nTypeOut, strAccountIn, nTypeIn, nCurrency, strDollars, strPWD); / listOnReplyMessage.Items.Add("[WithDraw]回傳值:" + nCode + " " + message); |

海外期貨未平倉查詢 GetOFOpenInterestGW
| 海外期貨未平倉查詢。 |
|---|
| 宣告 | (int Code, string Message) GetOFOpenInterestGW (string strLogInID, string strAccount, int nFormat); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 海期帳號， / BrokerID(例如:F020000)＋帳號7碼 |
|  | nFormat | 指定格式0:彙總 |
| 回傳值 | class OFOpenInterest / { / string Account { get; set; }             // 帳號 / string ExchangeCode { get; set; }        // 交易所代號 / string ExchangeName { get; set; }        // 交易所中文名稱 / string ProductCodeWithYM { get; set; }  // 含年月-商品代號    (EX: NG 202012) / string ProductCode { get; set; }         // 不含年月-商品代碼 (EX: NG) / string ProductYM { get; set; }           // 商品年月 / string ProductNameWithYM { get; set; }   // 商品中文名稱(含年月) / string BuySell { get; set; }             // 買賣別 / string OpenInterest { get; set; }        // 未平口數 / string AvgPrice { get; set; }            // 平均價(成交均價) / string MarketPrice { get; set; }         // (非即時)現價(市價) / string UnrealizedPL { get; set; }        // 未平損益 / string BidPrice { get; set; }            // 委託買 / string BidVolume { get; set; }           // 買方委託口數 / string AskPrice { get; set; }            // 委託賣 / string AskVolume { get; set; }           // 賣方委託口數 / string AskNewOpenVolume { get; set; }    // 賣方委託新倉口數 / string PrevSettlementPrice { get; set; } // 昨日結算價（EX:379 02/4） / string DayTradeOpenInterest { get; set; }// 當沖未平口數 / string InitialMargin { get; set; }       // 原始保證金 / string IsOption { get; set; }            // 市場別 / string LoginID { get; set; }             //  LOGIN_ID / string StrikePrice { get; set; }         // 履約價 / string CallPut { get; set; }             // C/P (C:Call / P:Put) / } / class OFOpenInterestParserResult / { / int StatusCode { get; set; }          // 回傳狀態碼 / string Message { get; set; }          // 錯誤訊息 / string RawData { get; set; }          // 原始字串 / List< OFOpenInterest > Blocks { get; set; } = new List< OFOpenInterest >(); / } |
| 備註 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message成功：空字串；失敗：錯誤訊息 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / var result = SK.GetOFOpenInterestGW(loginID, account, nFormat + 1); / listOnReplyMessage.Items.Add("[GetOFOpenInterestGW]回傳值:" + result.StatusCode + " " + result.Message); / if (result.StatusCode == 0 && result.Blocks.Count > 0) / { / foreach (var block in result.Blocks) / { / listOnReplyMessage.Items.Add( / $"帳號資料: {block.IsOption}-({block.Account}-{block.LoginID})" / ); / if(block.IsOption == "OF") / { / listOnReplyMessage.Items.Add( / $"交易所: {block.ExchangeName} {block.ExchangeCode},商品: {block.ProductNameWithYM} {block.ProductCodeWithYM},買賣別: {block.BuySell},未平倉口數: {block.OpenInterest},平均價: {block.AvgPrice},現價: {block.MarketPrice}," + / $"未平損益: {block.UnrealizedPL},委託買價/量: {block.BidPrice}/{block.BidVolume},委託賣價/量: {block.AskPrice}/{block.AskVolume},賣方新倉口數: {block.AskNewOpenVolume},昨日結算價: {block.PrevSettlementPrice},當沖未平口數: {block.DayTradeOpenInterest}," + / $"原始保證金: {block.InitialMargin}" / ); / } / else if(block.IsOption == "OO") / { / listOnReplyMessage.Items.Add( / $"交易所: {block.ExchangeName} {block.ExchangeCode},商品: {block.ProductNameWithYM} {block.ProductCodeWithYM},買賣別: {block.BuySell},未平倉口數: {block.OpenInterest},平均價: {block.AvgPrice},現價: {block.MarketPrice}," + / $"未平損益: {block.UnrealizedPL},委託買價/量: {block.BidPrice}/{block.BidVolume},委託賣價/量: {block.AskPrice}/{block.AskVolume},賣方新倉口數: {block.AskNewOpenVolume},昨日結算價: {block.PrevSettlementPrice},當沖未平口數: {block.DayTradeOpenInterest}," + / $"原始保證金: {block.InitialMargin},履約價: {block.StrikePrice},C/P: {block.CallPut}" / ); / } / listOnReplyMessage.Items.Add("--------------------------------------------------"); / } / } / else / { / listOnReplyMessage.Items.Add("[GetOFOpenInterestGW]沒有資料或回傳錯誤"); / } |

海外權益數查詢 GetOFFutureRights
| 海外權益數查詢。 |
|---|
| 宣告 | (int Code, string Message) GetOFFutureRights (string strLogInID, string strAccount, int nCoinType); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 期貨帳號， / BrokerID(例如:F020000)＋帳號7碼 |
|  | nCoinType | 幣別： / 0:全幣別 |
| 回傳值 | public class OFFutureRights / { / string LoginID { get; set; }               // LOGIN_ID / string AccountNo { get; set; }             // 帳號 / string Currency { get; set; }              // 幣別 / string RiskIndicator { get; set; }      // 風險指標/維持率 / string FullMaintenanceMargin { get; set; } // 足額維持率 / string WithdrawableMargin { get; set; } // 可動用(出金)保證金 / string OrderMargin { get; set; }           // 委託保證金 / string MaintenanceMargin { get; set; }     // 維持保證金 / string InitialMargin { get; set; }         // 原始保證金 / string NetEquity { get; set; }      // 淨值權益數、權益總值 / string WithheldPremium { get; set; }       // 預扣權利金 / string OptionLongMarketValue { get; set; } // 未沖銷買方選擇權市值 / string OptionShortMarketValue { get; set; }// 未沖銷賣方選擇權市值 / string AccountEquity { get; set; }     // 帳戶權益、權益數 / string IntradayUnrealized { get; set; }    // 盤中未實現 / string FuturesFloatingPL { get; set; }// 未沖銷期貨浮動損益 / string PremiumPaidToday { get; set; }    // 今日權利金支付 / string FuturesClosePLToday { get; set; }   // 本日期貨平倉損益淨額 / string RealizedFee { get; set; }     // 已實現費用、手續費 / string DepositWithdrawalToday { get; set; }// 今日存提款 / string PreviousDayBalance { get; set; }    // 前日帳戶餘額 / string ReferenceRate { get; set; }         // 參考匯率 / string OptionExpiryGain { get; set; }   // 選擇權到期差益 / string OptionExpiryLoss { get; set; }   // 選擇權到期差損 / string FuturesDeliveryPL { get; set; }     // 期貨交割損益 / string ExcessOrMarginCall { get; set; }// 超額/追繳保證金 / string ExerciseSettlementPL { get; set; } // 到期履約損益 / string AccountBalance { get; set; }        // 帳戶餘額 / string FixedMargin { get; set; }           // 固定保證金 / string VariableMargin { get; set; }        // 變動保證金 / int StatusCode { get; set; }         // 回傳狀態碼 / string Message { get; set; }         // 錯誤訊息 / string RawData { get; set; }         // 原始字串 / } / public class OFFutureRightsParserResult / { / int StatusCode { get; set; }          // 回傳狀態碼 / string Message { get; set; }          // 錯誤訊息 / string RawData { get; set; }          // 原始字串 / List<OFFutureRights> Blocks { get; set; } = new List<OFFutureRights>(); / } |
| 備註 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message成功：空字串；失敗：錯誤訊息 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / var result = SK.GetOFFutureRights(loginID, account, nCoinType); / listOnReplyMessage.Items.Add("[GetOFFutureRights]回傳值:" + result.StatusCode + " " + result.Message); / if (result.StatusCode == 0 && result.Blocks.Count > 0) / { / foreach (var block in result.Blocks) / { / listOnReplyMessage.Items.Add( / $"帳號資料: {block.LoginID}-{block.AccountNo}" / ); / listOnReplyMessage.Items.Add( / $"幣別: {block.Currency}, 風險指標: {block.RiskIndicator}, 足額維持保證金: {block.FullMaintenanceMargin}, " + / $"可動用保證金: {block.WithdrawableMargin}, 委託保證金: {block.OrderMargin}, 維持保證金: {block.MaintenanceMargin}, 原始保證金: {block.InitialMargin}, " + / $"淨值權益數: {block.NetEquity}, 預扣權利金: {block.WithheldPremium}, 未沖銷買方選擇權市值: {block.OptionLongMarketValue}, 未沖銷賣方選擇權市值: {block.OptionShortMarketValue}, " + / $"帳戶權益: {block.AccountEquity}, 盤中未實現: {block.IntradayUnrealized}, 未沖銷期貨浮動損益: {block.FuturesFloatingPL}, 今日權利金支付: {block.PremiumPaidToday}, " + / $"本日期貨平倉損益淨額: {block.FuturesClosePLToday}, 已實現費用: {block.RealizedFee}, 今日存提款: {block.DepositWithdrawalToday}, 前日帳戶餘額: {block.PreviousDayBalance}, " + / $"參考匯率: {block.ReferenceRate}, 選擇權到期差益: {block.OptionExpiryGain}, 選擇權到期差損: {block.OptionExpiryLoss}, 期貨交割損益: {block.FuturesDeliveryPL}, " + / $"超額/追繳保證金: {block.ExcessOrMarginCall}, 到期履約損益: {block.ExerciseSettlementPL}, 帳戶餘額: {block.AccountBalance}, 固定保證金: {block.FixedMargin}, 變動保證金: {block.VariableMargin}" / ); / listOnReplyMessage.Items.Add("--------------------------------------------------"); / } / } / else / { / listOnReplyMessage.Items.Add("[GetOFFutureRights]沒有資料或回傳錯誤"); / } |

複委託
台幣/外幣圈存查詢 GetForeignBlock
| 複委託台幣/外幣圈存查詢_一戶通 |
|---|
| 宣告 | ForeignBlockParserResult GetForeignBlock(string loginID, string fullAccount, int nFunc); |
| 參數 | loginID | 登入ID。 |
|  | fullAccount | 複委託帳號，分公司代碼＋帳號7碼 |
|  | nFunc | 0:台幣  1:外幣 |
| 回傳值 | class ForeignBlock / { / string BankCode { get; set; }          // 銀行代碼 / string BankBranchCode { get; set; }    // 銀行分行代號 / string BankAccount { get; set; }       // 銀行帳號 / string BankName { get; set; }          // 銀行名稱 / string Currency { get; set; }           // 幣別 / string UnpayableAmt { get; set; }       // 圈存金額 / string UnpayableBuy { get; set; }        // 買進未扣款 / string TodayOrder { get; set; }  // 股市委買 / string OutAmt { get; set; }          // 匯出換匯金額 / string UnblockAmt { get; set; }     // 可解圈金額 / string FundOrderAmt { get; set; }   // 基金委買 / int StatusCode { get; set; }               // 回傳狀態碼 / string Message { get; set; }           // 錯誤訊息 / string RawData { get; set; }           // 原始字串 / } / class ForeignBlockParserResult / { / int StatusCode { get; set; }          // 回傳狀態碼 / string Message { get; set; }          // 錯誤訊息 / string RawData { get; set; }          // 原始字串 / List<ForeignBlock> Blocks { get; set; } = new List<ForeignBlock>(); / } |
| 備註 | Code = 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / Message成功：空字串；失敗：錯誤訊息 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / var result = SK.GetForeignBlock(loginID, account, nCurrencyType); / listOnReplyMessage.Items.Add("[GetForeignBlock]回傳值:" + result.StatusCode + " " + result.Message); / if (result.StatusCode == 0 && result.Blocks.Count > 0) / { / foreach (var block in result.Blocks) / { / listOnReplyMessage.Items.Add( / $"銀行: {block.BankName} ({block.BankCode}-{block.BankBranchCode}-{block.BankAccount})" / ); / listOnReplyMessage.Items.Add( / $"幣別: {block.Currency}, 圈存金額: {block.UnpayableAmt}, 買進未扣款: {block.UnpayableBuy}, " + / $"股市委買: {block.TodayOrder}, 匯出換匯金額: {block.OutAmt}, 可解圈金額: {block.UnblockAmt}, 基金委買: {block.FundOrderAmt}" / ); / listOnReplyMessage.Items.Add("--------------------------------------------------"); / } / } / else / { / listOnReplyMessage.Items.Add("[GetForeignBlock]沒有資料或回傳錯誤"); / } |

回報
回補 OnComplete
| 回報連線後會進行回報回補，等收到此事件通知後表示回補完成。 |
|---|
| 宣告 | event Action<string> OnComplete; |
| 參數 | loginID | 登入ID |
| 備註 | 若未收到此通知,代表新建立的回報連線及回傳回報資料異常。 |
| 範例程式碼 | // listOnNewOrderData 與 listOnNewFulfillData 是用來顯示補回通知的 ListBox 控件 / SK.OnComplete += loginID => / { / if (listOnNewOrderData.InvokeRequired) / { / listOnNewOrderData.Invoke(new Action(() => / { / listOnNewOrderData.Items.Add($"[OnComplete]委託回補完成通知：{loginID}");  // 例如顯示訊息內容 / })); / } / else / { / listOnNewOrderData.Items.Add($"[OnComplete]委託回補完成通知：{loginID}"); / } / if (listOnNewFulfillData.InvokeRequired) / { / listOnNewFulfillData.Invoke(new Action(() => / { / listOnNewFulfillData.Items.Add($"[OnComplete]成交回補完成通知：{loginID}");  // 例如顯示訊息內容 / })); / } / else / { / listOnNewFulfillData.Items.Add($"[OnComplete]成交回補完成通知：{loginID}"); / } / }; |

委託+成交回報 OnNewData
| 當有回報將主動呼叫函式，並通知委託以及成交的狀態。(新格式 包含預約單回報) |
|---|
| 宣告 | event Action<string, OrderFulfillData> OnNewData; |
| 參數 | loginID | 登入ID |
|  | data | 回報資料 (範例：data.KeyNo 可取出委託序號欄位) |
| 備註 | OnNewData 為委託+成交回報， / 建議與 OnNewOrderData(委託回報) 跟 OnNewFulfillData(成交回報) 擇一使用 |
|  |  |
| 備註 | 「動態退單」 / 被「動態退單」的委託，會收到委託回報、取消回報與動態退單回報，若有成交部位還會有成交回報。 / 買進委託：可能成交價格 > 即時價格區間上限 → 退單 / 賣出委託：可能成交價格 < 即時價格區間下限 → 退單 / ˙即時價格區間上、下限計算公式 / 即時價格區間上限 = 退單價＋退單點數 / 即時價格區間下限 = 退單價－退單點數 / 退單點數:每日盤前計算完成(盤中固定) / 異動前量及異動後量只支援部分市場, (僅提供證券、複委託市場) / 2020/3/23證券逐筆交易開始提供相關二欄資料CancelOrderMarkByExchange 、ExchangeTandemMsg |
| 範例程式碼 | SK.OnNewData += (loginID, data) => / { / if (listOnNewOrderData.InvokeRequired) / { / listOnNewOrderData.BeginInvoke(new Action(() => / { / listOnNewOrderData.Items.Add($"[{loginID}]{data.Raw}"); // data 為原始字串 / listOnNewOrderData.Items.Add($"{data.OrderNo}"); // 這裡示範取出OrderNo / })); / } / else / { / listOnNewOrderData.Items.Add($"[{loginID}]{data.Raw}"); // data 為原始字串 / listOnNewOrderData.Items.Add($"{data.OrderNo}"); // 這裡示範取出OrderNo / } / }; |

委託回報 OnNewOrderData
| 當有回報將主動呼叫函式，並通知委託的狀態。(新格式 包含預約單回報) |
|---|
| 宣告 | event Action<string, OrderFulfillData> OnNewOrderData; |
| 參數 | loginID | 登入ID |
|  | data | 委託回報資料 (範例：data.KeyNo 可取出委託序號欄位) |
| 備註 | OnNewData 為委託+成交回報， / 建議與 OnNewOrderData(委託回報) 跟 OnNewFulfillData(成交回報) 擇一使用 |
|  |  |
| 備註 | 「動態退單」 / 被「動態退單」的委託，會收到委託回報、取消回報與動態退單回報，若有成交部位還會有成交回報。 / 買進委託：可能成交價格 > 即時價格區間上限 → 退單 / 賣出委託：可能成交價格 < 即時價格區間下限 → 退單 / ˙即時價格區間上、下限計算公式 / 即時價格區間上限 = 退單價＋退單點數 / 即時價格區間下限 = 退單價－退單點數 / 退單點數:每日盤前計算完成(盤中固定) / 異動前量及異動後量只支援部分市場, (僅提供證券、複委託市場) / 2020/3/23證券逐筆交易開始提供相關二欄資料CancelOrderMarkByExchange 、ExchangeTandemMsg |
| 範例程式碼 | // listOnNewOrderData 是用來顯示新委託資料的 ListBox 控件 / SK.OnNewOrderData += (loginID, data) => / { / if (listOnNewOrderData.InvokeRequired) / { / listOnNewOrderData.BeginInvoke(new Action(() => / { / listOnNewOrderData.Items.Add($"[{loginID}]{data.Raw}");  // 顯示原始字串資料 / listOnNewOrderData.Items.Add($"{data.OrderNo}");         // 顯示解析後的委託書號 / })); / } / else / { / listOnNewOrderData.Items.Add($"[{loginID}]{data.Raw}");      // 顯示原始字串資料 / listOnNewOrderData.Items.Add($"{data.OrderNo}");             // 顯示解析後的委託書號 / } / }; |

成交回報 OnNewFulfillData
| 當有回報將主動呼叫函式，並通知成交的狀態。(新格式 包含預約單回報) |
|---|
| 宣告 | event Action<string, OrderFulfillData> OnNewFulfillData; |
| 參數 | loginID | 登入ID |
|  | data | 成交回報資料 (範例：data.KeyNo 可取出委託序號欄位) |
| 備註 | OnNewData 為委託+成交回報， / 建議與 OnNewOrderData(委託回報) 跟 OnNewFulfillData(成交回報) 擇一使用 |
|  |  |
| 備註 | 「動態退單」 / 被「動態退單」的委託，會收到委託回報、取消回報與動態退單回報，若有成交部位還會有成交回報。 / 買進委託：可能成交價格 > 即時價格區間上限 → 退單 / 賣出委託：可能成交價格 < 即時價格區間下限 → 退單 / ˙即時價格區間上、下限計算公式 / 即時價格區間上限 = 退單價＋退單點數 / 即時價格區間下限 = 退單價－退單點數 / 退單點數:每日盤前計算完成(盤中固定) / 異動前量及異動後量只支援部分市場, (僅提供證券、複委託市場) / 2020/3/23證券逐筆交易開始提供相關二欄資料CancelOrderMarkByExchange 、ExchangeTandemMsg |
| 範例程式碼 | // listOnNewFulfillData 是用來顯示新成交資料的 ListBox 控件 / SK.OnNewFulfillData += (loginID, data) => / { / if (listOnNewFulfillData.InvokeRequired) / { / listOnNewFulfillData.BeginInvoke(new Action(() => / { / listOnNewFulfillData.Items.Add($"[{loginID}]{data.Raw}");  // 顯示原始字串資料 / listOnNewFulfillData.Items.Add($"{data.OrderNo}");         // 顯示解析後的委託書號 / })); / } / else / { / listOnNewFulfillData.Items.Add($"[{loginID}]{data.Raw}");      // 顯示原始字串資料 / listOnNewFulfillData.Items.Add($"{data.OrderNo}");             // 顯示解析後的委託書號 / } / }; |

國內行情
即時報價
OnNotifyQuoteLONG
| 當有索取的個股報價異動時，將透過此事件通知應用程式處理。 |
|---|
| 宣告 | event Action<int, string> OnNotifyQuoteLONG; |
| 參數 | nMarketNo | 報價有異動的商品市場別。 |
|  | strStockNo | 商品代號。 |
| 備註 | 透過以上兩個參數，可以再使用SKQuoteLib_GetStockByIndexLONG / 取出報價的內容。 / *開發人員可於OnUpDateDataRow(SKSTOCK pStock) 取得Stock揭示值，另外自行接手處理 / *依國內市場，可能需搭配MarketNo / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |
| 範例程式碼 | SK.OnNotifyQuoteLONG += (nMarketNo, strStockNo) => / { / // 建立報價物件 / SK.SKSTOCKLONG2 pSKStockLONG = new SK.SKSTOCKLONG2(); / // 收回報價物件 / pSKStockLONG = SK.SKQuoteLib_GetStockByStockNo(nMarketNo, strStockNo); / // 如果報價物件回傳正常，則將數值呈現至 UI / if (pSKStockLONG.nCode == 0) / OnUpDateDataRow(pSKStockLONG); / }; |

取報價物件(stockNo) SKQuoteLib_GetStockByStockNo
| 根據市場別編號與商品代號，取回商品報價的相關資訊。 |
|---|
| 宣告 | SKSTOCKLONG2 SKQuoteLib_GetStockByStockNo(int nMarketNo, string strStockNo); |
| 參數 | nMarketNo | 市場別編號 |
|  | strStockNo | 商品代號，例如 6005。 |
| 回傳值 | struct SKSTOCKLONG2 / { / int nCode; // 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / int nTradingDay; // 交易日(YYYYMMDD) 備註 :當日非交易日時, 資料為前一交易日 / int nDayTrade; // [限證券整股商品]可否當沖 0:一般 1:可先買後賣現股當沖 2:可先買後賣和先賣後買現股當沖 / int nSimulate; // 揭示 0:一般 1:試算(試撮)　＊[證券逐筆]盤中出現『1:試算』代表行情劇動，觸發價格穩定措施狀態; // [證券盤中零股] 盤中會進入試撮，請留意是否收取『試撮資料』 / int nDown; // 跌停價 / int nUp;// 漲停價 / int nYQty;// 昨量 / int nTQty;// 總量 / int nFutureOI;// 期貨未平倉 OI / int nTAc;// 賣盤量(即內盤量) / int nTBc;// 買盤量(即外盤量) / int nAc;// 賣量 / int nAsk;// 賣價 / int nBc;// 買量 / int nBid;// 買價 / int nRef;// 昨收、參考價 / int nTickQty;// 單量 / int nClose;// 成交價 / int nLow;// 最低價 / int nHigh;// 最高價 / int nOpen;// 開盤價 / string strStockNoSpread;//第二腳 交易所商品代碼_EX週/月價差// / string strStockName;// 商品名稱 / string strStockNo;// 商品代碼EX: 1101 台泥, TX12 台指期12月…etc. / int nMarketNo;// 市埸代碼 / int nTypeNo;//  EX: (證券)類股別 1 水泥 , 2 食品…etc. / int nDecimal;// 小數位數 / int nStockidx;// 商品自定索引代號 / int nTradingLotFlag;//[證券] 整股、盤中零股揭示註記   0:現股 ; 1:盤中零股 / int nDealTime;//成交時間 (hhmmss) / int nOddLotBid;// 盤後零股(14:25~14:30)買價 / int nOddLotAsk; // 盤後零股(14:25~14:30)賣價 / int nOddLotClose;// 盤後零股(14:25~14:30)成交價 / int nOddLotQty;// 盤後零股(14:25~14:30)成交量 / } |
| 備註 | 適用盤中零股 / ＊須等收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行。 / 未訂閱即時報價SKQuoteLib_RequestStocksWithMarketNo / ，執行此功能，僅可取得商品基本資料(商品名稱、昨收價，非即時性質欄位) |
| 範例程式碼 | SK.OnNotifyQuoteLONG += (nMarketNo, strStockNo) => / { / // 建立報價物件 / SK.SKSTOCKLONG2 pSKStockLONG = new SK.SKSTOCKLONG2(); / // 收回報價物件 / pSKStockLONG = SK.SKQuoteLib_GetStockByStockNo(nMarketNo, strStockNo); / // 如果報價物件回傳正常，則將數值呈現至 UI / if (pSKStockLONG.nCode == 0) / OnUpDateDataRow(pSKStockLONG); / }; |

訂閱報價 SKQuoteLib_RequestStocks
| 此功能不支援盤中零股。 / 訂閱指定商品即時報價 / 要求伺服器針對 strStockNos內的商品代號訂閱商品報價通知動作。 |
|---|
| 宣告 | int SKQuoteLib_RequestStocks(string strStockNos); |
| 參數 | strStockNos | 欲訂閱的商品代號，一筆以上的資料時，每檔商品代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyQuoteLONG |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品報價。 / *因應檔數限制，一個SKQuoteLib物件，僅可擇一使用一個即時報價訂閱 / (SKQuoteLib_RequestStocks功能或 SKQuoteLib_RequestStocksWithMarketNo功 / 能)。 / 可重新連線即還原限制與設定。 / 特殊行情： / 部分期選商品有T+1盤別交易，若不需要T+1盤的資訊，可在商品代號最後加上AM以取得純AM盤行情，需注意委託商品時加上AM是無法下單的，委託請使用原始商品代號 / 例：TX00的純AM盤行情為TX00AM |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / int nCode = SK.SKQuoteLib_RequestStocks(txtStocks.Text.Trim()); / listOnReplyMessage.Items.Add("[SKQuoteLib_RequestStocks]回傳值:" + SK.GetMessage(nCode)); |

訂閱零股報價 SKQuoteLib_RequestStocksOddLot
| 訂閱零股即時報價 / 要求伺服器針對 strStockNos內的商品代號訂閱商品報價通知動作。 |
|---|
| 宣告 | int SKQuoteLib_RequestStocksOddLot(string strStockNos); |
| 參數 | strStockNos | 欲訂閱的商品代號，一筆以上的資料時，每檔商品代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyQuoteLONG / OnNotifyOddLotSpreadDeal(整零價差，尚未更新上來) |
| 備註 | 適用盤中零股 / ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品報價。 / *因應檔數限制，一個SKQuoteLib物件，僅可擇一使用一個即時報價訂閱(SKQuoteLib_RequestStocks功能或 SKQuoteLib_RequestStocksWithMarketNo功能)。 / 可重新連線即還原限制與設定。 |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / int nCode = SK.SKQuoteLib_RequestStocksOddLot(txtStocks2.Text.Trim()); / listOnReplyMessage.Items.Add("[SKQuoteLib_RequestStocksOddLot]回傳值:" + SK.GetMessage(nCode)); |

解除訂閱報價 SKQuoteLib_CancelRequestStocks
| 取消訂閱SKQuoteLib_RequestStocks的報價通知，並停止更新商品報價。 |
|---|
| 宣告 | int SKQuoteLib_CancelRequestStocks(string strStockNos); |
| 參數 | strStockNos | 欲解除訂閱的商品代號，一筆以上的資料時，每檔股票代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 |  |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / // 這裡範例為取消整股報價 / int nCode = SK.SKQuoteLib_CancelRequestStocks(txtStocks.Text.Trim()); / listOnReplyMessage.Items.Add("[SKQuoteLib_CancelRequestStocks]回傳值:" + SK.GetMessage(nCode)); |

五檔&Ticks
即時五檔 OnNotifyBest5LONG
| (LONG index)當有索取的個股五檔價格有所異動，即透過向此註冊的事件進行處理。此函式會回傳所異動的個股五檔價格。 |
|---|
| 事件宣告 | event Action<int, string, int[], int[], int[], int[], int, int, int, int, int> OnNotifyBest5LONG; |
| 參數 | marketNo | 市埸代碼。 |
|  | strStockNo | 商品代號。 |
|  | bids | 買價。1到5檔：bestBids[0] ~ bestBids[4] |
|  | bidQtys | 買量。1到5檔：bidQtys [0] ~ bidQtys [4] |
|  | asks | 賣價。1到5檔：bestAsks [0] ~ bestAsks [4] |
|  | askQtys | 賣量。1到5檔：askQtys [0] ~ askQtys [4] |
|  | extendBid | 衍生一檔買價。 |
|  | extendBidQty | 衍生一檔買量。 |
|  | nExtendAsk | 衍生一檔賣價。 |
|  | nExtendAskQty | 衍生一檔賣量。 |
|  | simulate | 0:一般揭示 1:試算揭示 |
| 相關函式 | SKQuote_RequestTicks 相關通知事件。 |
| 備註 | 未進行揭示處理，開發人員需自行接手處理(判斷接收的五檔報價為一般或試算揭示)。 / ＊盤中零股: 因盤中仍會進入試撮，請留意是否收取『試撮資料』，即試算揭示。 / ＊須使用SKQuoteLib_RequestTicks 或SKQuoteLib_RequestTicksOddLot (擇一)訂閱，該事件才會被觸發。 |
| 範例程式碼 | // UpdateBest5Grid 用於將最新五檔報價顯示至 UI，例如 DataGridView / SK.OnNotifyBest5LONG += (marketNo, strStockNo, bids, bidQtys, asks, askQtys, extendBid, extendBidQty, nExtendAsk, nExtendAskQty, simulate) => / { / if (InvokeRequired) / { / this.Invoke(new Action(() => / { / UpdateBest5Grid(marketNo, bids, bidQtys, asks, askQtys, extendBid, extendBidQty, nExtendAsk, nExtendAskQty, simulate); / })); / } / else / { / UpdateBest5Grid(marketNo, bids, bidQtys, asks, askQtys, extendBid, extendBidQty, nExtendAsk, nExtendAskQty, simulate); / } / }; |

即時Ticks OnNotifyTicksLONG
| 當有索取的個股成交明細有所異動，即透過向此註冊事件回傳所異動的個股成交明細。 |
|---|
| 宣告 | event Action<int, string, int, int, int, int, int, int, int, int, int> OnNotifyTicksLONG; |
| 參數 | nMarketNo | 報價有異動的商品市場別。 |
|  | strStockNo | 商品代號。 |
|  | nPtr | 表示資料的位址(Key) |
|  | nDate | 交易日期。(YYYYMMDD) |
|  | nTimehms | 時間1。(時：分：秒) |
|  | nTimemillismicros | 時間2。(‘毫秒"微秒) |
|  | nBid | 買價。 |
|  | nAsk | 賣價。 |
|  | nClose | 成交價。 |
|  | nQty | 成交量。 |
|  | nSimulate | 0:一般揭示 1:試算揭示 |
| 備註 | nTimehms提供單位：時分秒hh:mm:ss。 / nTimemillismicros提供單位：毫秒微秒’millissecond’’micorsecond，預設提供毫秒微秒，開發人員自行決定是否顯示。(目前solace只提供證券商品) / 未進行揭示處理，開發人員需自行接手處理(判斷收的tick 為一般或試算揭示)。 / T盤切換T+1盤，不保留前一盤資料。開發者需自行清除前一盤資料。 / ＊須使用SKQuoteLib_RequestTicks 或 SKQuoteLib_RequestTicksOddLot (擇一)訂閱，該事件才會被觸發。 |
| 範例程式碼 | // listTicks 是用來顯示即時逐筆成交資料的 ListBox 控件 / SK.OnNotifyTicksLONG += (marketNo, strStockNo, ptr, date, timeHMS, timeMicro, bid, ask, close, qty, simulate) => / { / string strData = ""; / int nMarketPrice = kMarketPrice; / if (chkbox_msms.Checked == true) / strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + bid.ToString() + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString(); / else / strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + " " + timeMicro.ToString() + "," + bid.ToString() + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString(); / if (Box_M.Checked == true) / { / if (bid == kMarketPrice) / strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + "M" + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString(); / else if (ask == kMarketPrice) / strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + bid.ToString() + "," + "M" + "," + close.ToString() + "," + qty.ToString(); / else / strData = strStockNo.ToString() + "," + ptr.ToString() + "," + date.ToString() + " " + timeHMS.ToString() + "," + bid.ToString() + "," + ask.ToString() + "," + close.ToString() + "," + qty.ToString(); / } / //[揭示]//0:一般;1:試算揭示 / if (strData != "" && ((chkBoxSimulate.Checked) \|\| (!chkBoxSimulate.Checked && simulate == 0))) / { / if (listTicks.InvokeRequired) / { / listTicks.Invoke(new Action(() => / { / listTicks.Items.Add("[OnNotifyTicksLONG]" + strData); / })); / } / else / { / listTicks.Items.Add("[OnNotifyTicksLONG]" + strData); / } / } / }; |

訂閱五檔&Ticks SKQuoteLib_RequestTicks
| 此功能不支援盤中零股。 / 訂閱要求傳送成交明細以及五檔。 |
|---|
| 宣告 | int SKQuoteLib_RequestTicks(int nItemNo, string strStockNo); |
| 參數 | nItemNo | 請輸入第幾檔 (從 1 開始) |
|  | strStockNo | 索取的商品代號，一次請帶入一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 即時Tick由OnNotifyTicksLONG事件通知。 / Tick回補由 OnNotifyHistoryTicksLONG事件通知。 / 最佳五檔由OnNotifyBest5LONG事件通知。 |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品成交明細及最佳五檔。 / 特殊行情： / 部分期選商品有T+1盤別交易，若不需要T+1盤的資訊，可在商品代號最後加上AM以取得純AM盤行情，需注意委託商品時加上AM是無法下單的，委託請使用原始商品代號 / 例：TX00的純AM盤行情為TX00AM / *盤中零股：請使用SKQuoteLib_RequestTicksWithMarketNo |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / int nCode = SK.SKQuoteLib_RequestTicks(int.Parse(txtItemQuote.Text.Trim()), txtTick.Text.Trim()); / listOnReplyMessage.Items.Add("[SKQuoteLib_RequestTicks]回傳值:" + SK.GetMessage(nCode)); |

訂閱零股五檔&Ticks SKQuoteLib_RequestTicksOddLot
| 訂閱要求傳送成交明細以及五檔。 |
|---|
| 宣告 | int SKQuoteLib_RequestTicksOddLot(int nItemNo,string strStockNo); |
| 參數 | nItemNo | 請輸入第幾檔 (從 1 開始) |
|  | strStockNo | 索取的商品代號，一次請帶入一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 即時Tick由OnNotifyTicksLONG事件通知。 / Tick回補由 OnNotifyHistoryTicksLONG事件通知。 / 最佳五檔由OnNotifyBest5LONG事件通知。 |
| 備註 | 適用盤中零股 / ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品成交明細及最佳五檔。 |
| 範例程式碼 | // 參數設定程式碼省略(可參考完整C#範例程式) / // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / int nCode = SK.SKQuoteLib_RequestTicksOddLot(int.Parse(txtItemQuote.Text.Trim()), txtTick.Text.Trim()); / listOnReplyMessage.Items.Add("[SKQuoteLib_RequestTicksOddLot]回傳值:" + SK.GetMessage(nCode)); |

解除訂閱五檔&Ticks SKQuoteLib_CancelRequestTicks
| 取消訂閱RequestTicks的成交明細及五檔。 |
|---|
| 宣告 | int SKQuoteLib_CancelRequestTicks(string strStockNo); |
| 參數 | strStockNo | 欲解除訂閱的商品代號，一次僅能解訂閱一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 |  |
| 範例程式碼 | // listOnReplyMessage 是用來顯示回應訊息的 ListBox 控件 / int nCode = SK.SKQuoteLib_CancelRequestTicks(txtTick.Text.Trim()); / listOnReplyMessage.Items.Add("[SKQuoteLib_CancelRequestTicks]回傳值:" + SK.GetMessage(nCode)); |

服務型
GetMessage
| 取得定義代碼訊息文字。 |
|---|
| 宣告 | string GetMessage(int code); |
| 參數 | nCode | 函式回傳值。 |
| 回傳值 | 代碼文字訊息。 |
| 備註 |  |
| 範例程式碼 | // 登入後在 ListBox 顯示登入結果訊息（由 API 回傳的文字） / listOnReplyMessage.Items.Add("[Login]: " + SK.GetMessage(result.Code)); // 當 result.Code 為 0(成功)，將會回傳字串 SK_SUCCESS |

文件版本：2.13.58
