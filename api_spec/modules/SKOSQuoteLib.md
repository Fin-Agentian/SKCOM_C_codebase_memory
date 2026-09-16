# SKOSQuoteLib — 海外期貨（海期）報價元件：連線報價伺服器、訂閱海期商品即時報價／五檔十檔／成交明細與 K 線

> 版本基準 V2.13.59（以 V2.13.57 規格為底增補；差異見 [../changelog_2.13.57_to_2.13.59.md](../changelog_2.13.57_to_2.13.59.md)）。
> 主要來源：`api_spec/_raw/14.海期報價.md`；輔助來源：`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md`（4-5 節，行 2949–3308）、`api_spec/_raw/2.導覽.md`（新舊函式對照表）。
> **官方 V2.13.59 手冊已移除此章節；函式本身未移除（Interop 符號兩版一致），本節以 V2.13.57 留存原文為準。** 移除範圍：主手冊 4-5 SKOSQuoteLib（海期報價）整章消失，原位置改放 4-4-u OnNotifyLiveKLineData，章節編號由 4-4 直接跳到 4-7（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2905`、`:2920`）；結構 5-8 SKFOREIGNTICK／5-12 SKFOREIGNTICK_9／5-22 SKFOREIGNLONG／5-23 SKFOREIGN_9LONG 一併刪除，結構編號出現 5-6→5-9、5-11→5-13、5-21→5-24 三處斷層（同檔 `:4100`、`:4116`、`:4164`、`:4484`、`:4559`）；`2.導覽.md` 的「SKOSQuoteLib：海期報價物件」介紹區塊與新舊函式對照表亦不存在（`api_spec/_raw/v2.13.59/2.導覽.md` 全檔僅剩錯誤碼 2025 一處提及，見 `:102`）；分冊 `14.海期報價.docx` 不在 .59 說明文件夾（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/策略王COM元件使用說明_SKCOMTesterV2/` 編號由 13 跳到 16）。
> 判定依據（函式未移除）：Interop.SKCOMLib 的 SKOSQuoteLib 相關符號 sym57↔sym59 為 56:56 逐行相同；原生 SKCOM.dll x64 匯出表兩版皆含 `SKOSQuoteLib_RequestStocks`／`SKOSQuoteLib_RequestTicks`／`SKOSQuoteLib_GetStockByNoNineDigitLONG`（78→83、+5/−0，新增的 5 支皆為下單／權益數相關）；.59 官方範例的海期下單表單仍實際呼叫 `SKOSQuoteLib_EnterMonitorLONG`（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:321`）。
> 回傳錯誤碼對照見 [../error_codes.md](../error_codes.md)。

## 總覽：功能分區表

| 分區 | 函式／事件 | 用途一句話 |
|---|---|---|
| 連線 | SKOSQuoteLib_EnterMonitorLONG | （LONG index）與海期報價伺服器建立連線 |
| 連線 | SKOSQuoteLib_LeaveMonitor | 中斷報價伺服器連線 |
| 連線 | SKOSQuoteLib_IsConnected | 檢查目前連線狀態 |
| 連線 | SKOSQuoteLib_GetQuoteStatus | 查詢報價連線數與是否超過連線限制（僅限連線成功後） |
| 連線 | SKOSQuoteLib_SetOSQuoteServer | 切換海期／海選報價資訊源（正式或備援） |
| 連線 | SKOSQuoteLib_Initialize | 重新初始海期物件 |
| 商品清單 | SKOSQuoteLib_RequestOverseaProducts | 要求海外商品檔（由 OnOverseaProducts 回傳） |
| 商品清單 | SKOSQuoteLib_GetOverseaProductDetail | 取得海期商品檔（含下單代碼，由 OnOverseaProductsDetail 回傳） |
| 個期資訊 | SKOSQuoteLib_GetStockByNoLONG | 依商品代號取回海期報價資訊（SKFOREIGNLONG） |
| 個期資訊 | SKOSQuoteLib_GetStockByNoNineDigitLONG | 依商品代號取回海期報價資訊（CME 九位小數擴充） |
| 即時報價 | SKOSQuoteLib_RequestStocks | 訂閱指定商品即時報價（由 OnNotifyQuoteLONG 通知） |
| 即時報價 | SKOSQuoteLib_GetStockByIndexLONG | 依系統索引代碼取回海期報價資訊（SKFOREIGNLONG） |
| 即時報價 | SKOSQuoteLib_GetStockByIndexNineDigitLONG | 依系統索引代碼取回海期報價資訊（CME 九位小數擴充） |
| 十檔&五檔&成交明細 | SKOSQuoteLib_RequestTicks | 訂閱成交明細＋五檔＋十檔（含歷史 Tick 回補） |
| 十檔&五檔&成交明細 | SKOSQuoteLib_RequestMarketDepth | 僅訂閱最佳十檔、五檔（不含成交明細） |
| 十檔&五檔&成交明細 | SKOSQuoteLib_RequestLiveTick | 僅訂閱即時成交明細（不含五檔十檔、不回補歷史） |
| 十檔&五檔&成交明細 | SKOSQuoteLib_GetTickNineDigitLONG | 取得成交明細 Tick 物件（CME 九位小數擴充） |
| 十檔&五檔&成交明細 | SKOSQuoteLib_GetBest5NineDigitLONG | 取得最佳五檔物件（CME 九位小數擴充） |
| 技術分析 | SKOSQuoteLib_RequestKLine | K 線查詢（不可指定日期區間） |
| 技術分析 | SKOSQuoteLib_RequestKLineByDate | K 線查詢，可指定日期區間與幾分 K |
| 技術分析 | SKOSQuoteLib_RequestKLineAMByDate | 指定區間 K 線查詢（主說明僅列於功能表，無細節） |
| 僅見於範例碼 | SKOSQuoteLib_RequestServerTime | 要求報價主機回傳目前時間（由 OnNotifyServerTime 通知） |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_EnterMonitor | 舊版連線，改用 EnterMonitorLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetStockByIndex | 舊版（SHORT index），改用 GetStockByIndexLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetStockByIndexNineDigit | 舊版，改用 GetStockByIndexNineDigitLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetStockByNo | 舊版，改用 GetStockByNoLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetStockByNoNineDigit | 舊版，改用 GetStockByNoNineDigitLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetTick | 舊版取得 Tick，改用 GetTickNineDigitLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetTickNineDigit | 舊版，改用 GetTickNineDigitLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetTickLONG | 僅出現於主說明備註文字，改用 GetTickNineDigitLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetBest5 | 舊版取得五檔，改用 GetBest5NineDigitLONG |
| 舊版（V2.13.46 起移除） | SKOSQuoteLib_GetBest5NineDigit | 舊版，改用 GetBest5NineDigitLONG |
| 事件：連線 | OnConnect | 接收連線／斷線狀態 |
| 事件：商品清單 | OnOverseaProducts | 海期商品清單資料逐筆回傳 |
| 事件：商品清單 | OnOverseaProductsDetail | 海期商品清單資料（含下單代碼）逐筆回傳 |
| 事件：即時報價 | OnNotifyQuoteLONG | 訂閱商品報價異動通知（回傳 nIndex） |
| 事件：十檔&五檔&成交明細 | OnNotifyTicksNineDigitLONG | 即時 Tick 通知（九位小數擴充） |
| 事件：十檔&五檔&成交明細 | OnNotifyHistoryTicksNineDigitLONG | 當日歷史 Tick 回補通知（九位小數擴充） |
| 事件：十檔&五檔&成交明細 | OnNotifyBest5NineDigitLONG | 最佳五檔異動通知（九位小數擴充） |
| 事件：十檔&五檔&成交明細 | OnNotifyBest10NineDigitLONG | 最佳十檔異動通知（九位小數擴充） |
| 事件：技術分析 | OnKLineData | 歷史 K 線資料逐筆回傳 |
| 事件：僅見於範例碼 | OnNotifyServerTime | 報價主機時間通知（RequestServerTime 對應事件） |

> V2.13.58／V2.13.59 文件異動：官方在 .59 就地改寫了 2.13.46 版本歷程條目，移除清單由「國內報價 SKQuoteLib／海期報價 SKOSQuoteLib／海選報價 SKOOQuoteLib」縮為只剩「國內報價 SKQuoteLib」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:25`）。這是與 4-5 整章下架同一批的用語刷洗，**不可據此推論上表這些舊版函式已恢復提供**——官方所稱「移除」係指不再提供／不再支援，而非 typelib 拿掉。上表定位維持不變，詳見下方「舊版函式」節。

## 初始化與事件註冊

C# 實際寫法（Interop.SKCOMLib，`using SKCOMLib;`）。物件建立（Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:21）：

```csharp
using SKCOMLib;

SKCenterLib m_pSKCenter = new SKCenterLib();     // 登入&環境設定物件
SKOSQuoteLib m_pSKOSQuote = new SKOSQuoteLib();  // 海期報價物件
```

事件掛載（抄自 Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:97-119，於首次連線前掛載一次）：

```csharp
m_SKOSQuoteLib.OnConnect += new _ISKOSQuoteLibEvents_OnConnectEventHandler(this.OnConnect);
m_SKOSQuoteLib.OnOverseaProducts += new _ISKOSQuoteLibEvents_OnOverseaProductsEventHandler(this.OnOverSeaProducts);
m_SKOSQuoteLib.OnKLineData += new _ISKOSQuoteLibEvents_OnKLineDataEventHandler(this.OnKLineData);
m_SKOSQuoteLib.OnNotifyServerTime += new _ISKOSQuoteLibEvents_OnNotifyServerTimeEventHandler(this.OnServerTime);
m_SKOSQuoteLib.OnOverseaProductsDetail += new _ISKOSQuoteLibEvents_OnOverseaProductsDetailEventHandler(this.OnOverSeaProductsDetail);

m_SKOSQuoteLib.OnNotifyQuoteLONG += new _ISKOSQuoteLibEvents_OnNotifyQuoteLONGEventHandler(this.OnQuoteUpdate);
m_SKOSQuoteLib.OnNotifyTicksNineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyTicksNineDigitLONGEventHandler(this.OnNotifyTicksNineLONG);
m_SKOSQuoteLib.OnNotifyHistoryTicksNineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyHistoryTicksNineDigitLONGEventHandler(this.OnNotifyHistoryTicksNineLONG);
m_SKOSQuoteLib.OnNotifyBest5NineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyBest5NineDigitLONGEventHandler(this.OnNotifyBest5);
m_SKOSQuoteLib.OnNotifyBest10NineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyBest10NineDigitLONGEventHandler(this.OnNotifyBest10);

// 掛載完成後才連線
m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_EnterMonitorLONG();
```

前置條件：須先以 SKCenterLib_Login 登入，且需先簽署期貨 API 下單聲明書。

## 方法

### SKOSQuoteLib_Initialize
- 用途：重新初始海期物件。
- 簽名：`int SKOSQuoteLib_Initialize();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：需先簽署期貨 API 下單聲明書，方可使用。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:406、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:453

### SKOSQuoteLib_EnterMonitorLONG
- 用途：（LONG index）與海期報價伺服器建立連線。
- 簽名：`int SKOSQuoteLib_EnterMonitorLONG();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：連線狀態由 OnConnect 事件回傳；需先簽署期貨 API 下單聲明書（以上為 `_raw/14.海期報價.md:90` 原文）。海期各 LONG 事件均載明「須以本函式登入，該事件才會被觸發」；「與舊版 SKOSQuoteLib_EnterMonitor 僅能擇一使用、登入後僅觸發 LONG index 類型事件（OnNotifyQuoteLONG、OnNotifyTicksNineDigitLONG、OnNotifyHistoryTicksNineDigitLONG、OnNotifyBest5NineDigitLONG、OnNotifyBest10NineDigitLONG）」係依 SKQuoteLib_EnterMonitorLONG 之同構說明類推（主說明:2577），海期文件未明載。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:119、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:399
- V2.13.59 現況：本函式在官方 V2.13.59 手冊雖已無專屬章節，但仍是可用且必要的前置呼叫——.59 範例的三個海期／海選下單表單都在載入時呼叫它（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:321`、`.../Order/StrategyOrderForm/OFStrategyOrderForm.cs:349`、`.../Order/SKProxyOrder/SKProxySendOrderForm/OFSKProxySendOrderForm.cs:289`），且錯誤碼 2025「請先執行海期報價SKOSQuoteLib_EnterMonitorLONG連線，再使用目前功能」仍列於 .59 手冊（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4844`、`api_spec/_raw/v2.13.59/2.導覽.md:102`）。
- V2.13.59 範例參考：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:19`（物件建立）、`:321`（呼叫）。.59 已無專門的海期報價範例畫面，只剩下單表單裡的這段連線程式碼可參考。

### SKOSQuoteLib_LeaveMonitor
- 用途：中斷報價伺服器連線。
- 簽名：`int SKOSQuoteLib_LeaveMonitor();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：連線狀態由 OnConnect 事件回傳。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:126、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:427

### SKOSQuoteLib_IsConnected
- 用途：檢查目前連線狀態。
- 簽名：`int SKOSQuoteLib_IsConnected();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：1 表示連線中，其餘非 1 數值都表示失敗（注意：與其他函式「0=成功」慣例不同）。
- 備註：請同時接收通知事件 OnConnect。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:421、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:462

### SKOSQuoteLib_GetQuoteStatus
- 用途：查詢報價連線狀態（是否超過報價連線限制、連線數資訊）。
- 簽名：`int SKOSQuoteLib_GetQuoteStatus(ref int pnConnectionCount, ref bool pbIsOutLimit);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| pnConnectionCount | ref int（[in,out] LONG*） | 連線數：pbIsOutLimit 為 true 時＝目前限制最大可使用連線數；為 false 時＝先前已使用連線數（不含當次新連線）。帶入任一數值，函式庫會回填連線數 |
| pbIsOutLimit | ref bool（[in,out] VARIANT_BOOL*） | 報價連線數是否超過限制；帶入 false，函式庫會回填布林值 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：限當次報價連線成功後查詢用。可搭配 LOG 檔（日期_OSQuote.log）確認是否下載海期商品檔 LoadOSCommdity；有海期帳號時，預設 Login 會占用一條報價連線（海期）。
- V2.13.58／V2.13.59 文件刪減提醒：解讀本函式回傳值所需的「3-3 行情物件連線限制說明」（總連線數共 2 條、國內證券與國內期貨共用一條、海外期選單獨一條、以 SKCenterLib_LogInSetQuote(ID, pw, 'N') 停用報價）在 .59 主手冊已整段刪除，3-2 之後直接跳到「4、ATL物件函式說明」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:154`；.59 全目錄 grep「行情物件連線限制」「總連線數量」皆 0 命中，未搬移至他章）。上述連線數規則與 OSQuote.log 說明僅存於 V2.13.57 文件，本節保留原文。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:1075、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:476

### SKOSQuoteLib_SetOSQuoteServer
- 用途：切換海期、海選報價資訊源（正式或備援）。
- 簽名：`int SKOSQuoteLib_SetOSQuoteServer(short sServer);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| sServer | short（SHORT） | 0：預設；1：備援 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：這不是切換 Server（Server 是自動分配的），是切換資訊源。切換後需斷線再重新連線才生效。目前僅以下交易所有資訊源切換功能：海期 CME、CBOT、NYM、SGX、HKEx；海選 HKEx。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:242、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:439

### SKOSQuoteLib_RequestOverseaProducts
- 用途：要求取得海外商品檔。
- 簽名：`int SKOSQuoteLib_RequestOverseaProducts();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：商品資訊由 OnOverseaProducts 事件取得。須先以 SKOSQuoteLib_EnterMonitorLONG 登入。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:181、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:408

### SKOSQuoteLib_GetOverseaProductDetail
- 用途：取得海期商品檔（含下單交易所代碼及下單商品代碼）。
- 簽名：`int SKOSQuoteLib_GetOverseaProductDetail(short sType);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| sType | short（SHORT） | 1：取得海期商品檔，含下單交易所代碼及下單商品代碼 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：商品資訊由 OnOverseaProductsDetail 事件取得。須先以 SKOSQuoteLib_EnterMonitorLONG 登入。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:415、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:418

### SKOSQuoteLib_RequestStocks
- 用途：訂閱指定商品即時報價，要求伺服器對 bstrStockNos 內的商品代號做報價通知。
- 簽名：`int SKOSQuoteLib_RequestStocks(ref short psPageNo, string bstrStockNos);`（SKCOMTester 以 `ref` 呼叫；SKCOMTesterV2 的 interop 版本為傳值 `SKOSQuoteLib_RequestStocks(psPageNo, bstrStockNos)`，依實際引用之 Interop.SKCOMLib 而定）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | ref short（[in,out] SHORT*） | 請固定帶 1 |
| bstrStockNos | string（BSTR） | 欲訂閱的海期商品代號，以「交易所代碼,商品報價代碼」為單位，多筆以 `#` 區隔。例：`CBOT,ZB1712#HKEx,HSI1712#CBOT,YM1712` |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：對應事件 OnNotifyQuoteLONG。若字串中含無效商品代碼，整批訂閱不會送出。避免在 OnConnect 事件內直接呼叫（各交易所商品未下載完成前無法訂閱）。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:277、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:582

### SKOSQuoteLib_GetStockByNoLONG
- 用途：（LONG index）根據商品代號，取回海期報價的相關資訊。
- 簽名：`int SKOSQuoteLib_GetStockByNoLONG(string bstrStockNo, ref SKFOREIGNLONG pSKStock);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | string（BSTR） | 海期商品代號，例如 `CME,ES2109`（小SP指數） |
| pSKStock | ref SKFOREIGNLONG（[in,out] struct SKFOREIGNLONG*） | SKCOM 元件中的 SKFOREIGNLONG 物件，帶入後由元件回填 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：須先以 SKOSQuoteLib_EnterMonitorLONG 登入。價格欄位需依 sDecimal 自行處理小數。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:651

### SKOSQuoteLib_GetStockByNoNineDigitLONG
- 用途：（LONG index）（CME 九位小數擴充）根據商品代號，取回海期報價的相關資訊。
- 簽名：`int SKOSQuoteLib_GetStockByNoNineDigitLONG(string bstrStockNo, ref SKFOREIGN_9LONG pSKStock);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | string（BSTR） | 海期商品代號，例如 `CME,ES2109` |
| pSKStock | ref SKFOREIGN_9LONG（[in,out] struct SKFOREIGN_9LONG*） | SKCOM 元件中的 SKFOREIGN_9LONG 物件，帶入後由元件回填 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：須先以 SKOSQuoteLib_EnterMonitorLONG 登入。價格欄位為 long（LONGLONG），需依 sDecimal 自行處理小數。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:597

### SKOSQuoteLib_GetStockByIndexLONG
- 用途：（LONG index）根據系統所編的索引代碼，取回海期報價的相關資訊。
- 簽名：`int SKOSQuoteLib_GetStockByIndexLONG(int nIndex, ref SKFOREIGNLONG pSKStock);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的索引代碼 |
| pSKStock | ref SKFOREIGNLONG（[in,out] struct SKFOREIGNLONG*） | SKCOM 元件中的 SKFOREIGNLONG 物件，帶入後由元件回填 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：商品物件價格未經小數處理。避免在 OnConnect 事件內直接呼叫。須先以 SKOSQuoteLib_EnterMonitorLONG 登入。通常於 OnNotifyQuoteLONG 事件觸發後以 nIndex 呼叫。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:315

### SKOSQuoteLib_GetStockByIndexNineDigitLONG
- 用途：（LONG index）（CME 九位小數擴充）根據系統所編的索引代碼，取回海期報價的相關資訊。
- 簽名：`int SKOSQuoteLib_GetStockByIndexNineDigitLONG(int nIndex, ref SKFOREIGN_9LONG pSKStock);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的索引代碼 |
| pSKStock | ref SKFOREIGN_9LONG（[in,out] struct SKFOREIGN_9LONG*） | SKCOM 元件中的 SKFOREIGN_9LONG 物件，帶入後由元件回填 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：商品物件價格未經小數處理。避免在 OnConnect 事件內直接呼叫。須先以 SKOSQuoteLib_EnterMonitorLONG 登入。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:805

### SKOSQuoteLib_RequestTicks
- 用途：訂閱與要求傳送成交明細以及五檔、十檔（含當日歷史 Tick 回補）。
- 簽名：`int SKOSQuoteLib_RequestTicks(ref short psPageNo, string bstrStockNo);`（SKCOMTesterV2 interop 版本為傳值呼叫，同 RequestStocks 說明）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | ref short（[in,out] SHORT*） | 請從 1 開始 |
| bstrStockNo | string（BSTR） | 指定商品，一個 Page 僅能索取一檔。格式「交易所代碼,商品報價代碼」，例：`CME,ES2012` |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：對應事件—即時 Tick：OnNotifyTicksNineDigitLONG；Tick 回補：OnNotifyHistoryTicksNineDigitLONG；最佳五檔：OnNotifyBest5NineDigitLONG；最佳十檔：OnNotifyBest10NineDigitLONG。若商品代碼無效則不送出訂閱。避免在 OnConnect 事件內直接呼叫。與 RequestMarketDepth／RequestLiveTick 請擇一使用。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:170、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:492

### SKOSQuoteLib_RequestMarketDepth
- 用途：訂閱與要求傳送最佳十檔（僅包含最佳十、五檔；不包含歷史與即時成交明細）。
- 簽名：`int SKOSQuoteLib_RequestMarketDepth(ref short psPageNo, string bstrStockNo);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | ref short（[in,out] SHORT*） | 請從 1 開始 |
| bstrStockNo | string（BSTR） | 索取的商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：五檔由 OnNotifyBest5NineDigitLONG、十檔由 OnNotifyBest10NineDigitLONG 事件通知。不傳送成交明細，與 RequestTicks 不同，請擇一使用；因無回補歷史成交明細，GetTickNineDigitLONG 僅可查閱訂閱後之即時成交明細。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:1064、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:707

### SKOSQuoteLib_RequestLiveTick
- 用途：訂閱與要求傳送即時成交明細（不訂閱最佳五檔／十檔，亦不含歷史 Ticks）。
- 簽名：`int SKOSQuoteLib_RequestLiveTick(ref short psPageNo, string bstrStockNo);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | ref short（[in,out] SHORT*） | 請從 1 開始 |
| bstrStockNo | string（BSTR） | 索取的商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：即時 Tick 由 OnNotifyTicksNineDigitLONG 事件通知。不回補歷史成交明細，與 RequestTicks 不同，請擇一使用；GetTickNineDigitLONG 僅可查閱訂閱後之即時成交明細。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:445

### SKOSQuoteLib_GetTickNineDigitLONG
- 用途：（LONG index）（CME 九位小數擴充）取得成交明細資訊（需先以 RequestTicks 訂閱）。
- 簽名：`int SKOSQuoteLib_GetTickNineDigitLONG(int nIndex, int nPtr, ref SKFOREIGNTICK_9 pSKTick);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的海期商品索引代碼 |
| nPtr | int（LONG） | 第幾筆成交明細（成交明細順序），可據此取得指定第幾筆成交明細 |
| pSKTick | ref SKFOREIGNTICK_9（[in,out] struct SKFOREIGNTICK_9*） | SKCOM 元件中的 SKFOREIGNTICK_9 物件，帶入後由元件回填（docx 參數表誤植為 pSKStock） |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：避免在 OnNotifyHistoryTicksNineDigitLONG、OnNotifyTicksNineDigitLONG 通知事件裡呼叫。須先以 SKOSQuoteLib_EnterMonitorLONG 登入。V2.13.42 起 SKFOREIGNTICK_9 新增成交日期欄位 nDate。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:374、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:263

### SKOSQuoteLib_GetBest5NineDigitLONG
- 用途：（LONG index）（CME 九位小數擴充）取得最佳五檔價格資訊（需先以 RequestTicks 訂閱）。
- 簽名：`int SKOSQuoteLib_GetBest5NineDigitLONG(int nIndex, ref SKBEST5_9 pSKBest5);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的海期商品索引代碼 |
| pSKBest5 | ref SKBEST5_9（[in,out] struct SKBEST5_9*） | SKCOM 元件中的 SKBEST5_9 物件，帶入後由元件回填（docx 參數表誤植為 pSKStock） |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：五檔更新由 OnNotifyBest5NineDigitLONG 事件通知；避免在該通知事件裡呼叫。須先以 SKOSQuoteLib_EnterMonitorLONG 登入。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:392、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:144

### SKOSQuoteLib_RequestKLine
- 用途：取得 K 線資料（不可指定日期區間）。
- 簽名：`int SKOSQuoteLib_RequestKLine(string bstrStockNo, short sKLineType);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | string（BSTR） | 海期商品代號，例如 `CME,ES1609` |
| sKLineType | short（SHORT） | 0=分鐘線；1=日線；2=週線；3=月線 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：K 線資料由 OnKLineData 事件通知取得。若商品代碼無效則不送出查詢。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:197（SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:562 為註解示範）

### SKOSQuoteLib_RequestKLineByDate
- 用途：取得 K 線資料，可指定日期區間，分 K 時可指定幾分 K。
- 簽名：`int SKOSQuoteLib_RequestKLineByDate(string bstrStockNo, short sKLineType, string bstrStartDate, string bstrEndDate, short sMinuteNumber);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | string（BSTR） | 海期商品代號，例如 `CME,ES1609` |
| sKLineType | short（SHORT） | 0=分線；1=日線；2=週線；3=月線 |
| bstrStartDate | string（BSTR） | 起始日期，格式 YYYYMMDD（ex: 20201001） |
| bstrEndDate | string（BSTR） | 結束日期，格式 YYYYMMDD（ex: 20201010） |
| sMinuteNumber | short（SHORT） | 指定幾分 K（1=1分K、3=3分K）；僅 sKLineType=0 時有意義 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：K 線資料由 OnKLineData 事件通知取得。若商品代碼無效則不送出查詢。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:217、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:558

### SKOSQuoteLib_RequestKLineAMByDate
- 用途：指定區間 K 線查詢（主說明 4-5 功能表列出，行 2975，但無細節章節）。
- 簽名：`int SKOSQuoteLib_RequestKLineAMByDate(string bstrStockNo, short sKLineType, short sOutType, short sTradeSession, string bstrStartDate, string bstrEndDate, short sMinuteNumber);`（依 SKQuoteLib_RequestKLineAMByDate 同名函式推測，簽名待確認）
- 參數表（依 SKQuoteLib 版推測，文件未載）：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | string（BSTR） | 商品代號（文件未載） |
| sKLineType | short（SHORT） | K 線種類（文件未載；SKQuoteLib 版：0=分線、4=日線、5=週線、6=月線） |
| sOutType | short（SHORT） | 輸出格式（文件未載；SKQuoteLib 版：0=舊版、1=新版） |
| sTradeSession | short（SHORT） | 盤別（文件未載；SKQuoteLib 版：0=全盤、1=AM盤） |
| bstrStartDate | string（BSTR） | 起始日期 YYYYMMDD（文件未載） |
| bstrEndDate | string（BSTR） | 結束日期 YYYYMMDD（文件未載） |
| sMinuteNumber | short（SHORT） | 幾分 K（文件未載） |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：對應事件推測為 OnKLineData。海期範例碼未使用此函式（SKQuoteLib 版用法見 Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKQuote.cs:1429）。
- 範例：範例未見

## 僅見於範例碼

### SKOSQuoteLib_RequestServerTime
- 用途：要求海期報價主機傳送目前時間（21 份 docx 手冊皆未記載此海期版本；SKQuoteLib 有同名函式）。
- 簽名：`int SKOSQuoteLib_RequestServerTime();`（自範例碼實際呼叫萃取；docx 未載，簽名待確認）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | 文件未載 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（推測同慣例，見 [../error_codes.md](../error_codes.md)）。
- 備註：對應事件 OnNotifyServerTime（見事件節）。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:224

### SKOSQuoteLib_GetOverseaProductsDetail（非實際方法：範例碼字串誤植）
- 此名稱僅出現在範例碼的 log 訊息字串（Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:416），實際呼叫的方法是 `SKOSQuoteLib_GetOverseaProductDetail`（同檔 :415）。SKCOMLib 介面上沒有 `GetOverseaProductsDetail`（多了 s）這個方法，請勿使用此名稱。

## 舊版函式（V2.13.31 起以 LONG index 版取代，V2.13.46 起自元件移除）

以下名稱僅出現在文件的新舊對照表（`2.導覽.md:164-172`、`策略王COM元件使用說明_V2.13.57.md:221-229`）與更版紀錄；V2.13.46（含）以上版本不再提供，範例碼亦未使用。單一市場商品總數可能超過 SHORT 上限（32767），舊版（SHORT index）函式會無法正常取得報價，一律改用右列新版。

> **V2.13.58／V2.13.59 文件異動：2.13.46 的移除清單被就地改寫，但不改變本節結論。** 官方在 .59 把 2024/1/29（2.13.46）版本歷程條目的移除清單由「國內報價 SKQuoteLib／海期報價 SKOSQuoteLib／海選報價 SKOOQuoteLib」縮為只剩「國內報價 SKQuoteLib」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:25`；.57 原文見 `_raw/策略王COM元件使用說明_V2.13.57.md:25`），與 4-5 整章、3-4 對照表同批下架，且 2.13.58／2.13.59 兩列版本歷程都未說明此次回溯改寫。**這段刪字不足以證明舊版函式已恢復提供**：可查證的只有 SKOSQuoteLib_EnterMonitor、GetStockByIndex、GetStockByIndexNineDigit、GetStockByNo、GetStockByNoNineDigit、GetTick、GetTickNineDigit、GetBest5、GetBest5NineDigit 的 Interop 型別宣告在 .59 仍在（sym57↔sym59 對 SKOSQuoteLib 56:56 逐行相同）；但 typelib 有宣告不等於功能仍提供——SKQuoteLib 的非-LONG 版同樣仍列於 sym59，官方卻始終列為已移除，可見官方「移除」指的是不再提供／不再支援。
> 實務結論不變：這些舊版（SHORT index）函式一律不要使用——商品總數超過 32767 時無法正常取得報價（V2.13.57 主手冊 3-4 節原文涵蓋國內、海期、海選，見 `_raw/策略王COM元件使用說明_V2.13.57.md:187`），且依 SKQuoteLib_EnterMonitorLONG 之同構說明類推，以 EnterMonitorLONG 登入後其配套的非 LONG 事件不會觸發（海期文件未明載）。另 .59 主手冊已刪除「3-4 行情功能修改說明」整節（含 SKOSQuoteLib 的新舊函式對應一覽表），`2.導覽.md` 的同一份對照表亦一併刪除（`api_spec/_raw/v2.13.59/2.導覽.md` 全檔僅剩 `:102` 一處提及 SKOSQuoteLib），對照表原文僅存於 V2.13.57 文件。

### SKOSQuoteLib_EnterMonitor
- 用途：舊版連線。改用 SKOSQuoteLib_EnterMonitorLONG（兩者僅能擇一使用）。
- 簽名：`int SKOSQuoteLib_EnterMonitor();`（依 LONG 版推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除；使用舊版登入才會觸發非 LONG 事件家族。
- 範例：範例未見

### SKOSQuoteLib_GetStockByIndex
- 用途：舊版（SHORT index）依索引取商品物件。改用 SKOSQuoteLib_GetStockByIndexLONG。
- 簽名：`int SKOSQuoteLib_GetStockByIndex(short sIndex, ref SKFOREIGN pSKStock);`（依 LONG 版推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

### SKOSQuoteLib_GetStockByIndexNineDigit
- 用途：舊版（SHORT index）九位小數版。改用 SKOSQuoteLib_GetStockByIndexNineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetStockByIndexNineDigit(short sIndex, ref SKFOREIGN_9 pSKStock);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

### SKOSQuoteLib_GetStockByNo
- 用途：舊版依商品代號取商品物件。改用 SKOSQuoteLib_GetStockByNoLONG。
- 簽名：`int SKOSQuoteLib_GetStockByNo(string bstrStockNo, ref SKFOREIGN pSKStock);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

### SKOSQuoteLib_GetStockByNoNineDigit
- 用途：舊版九位小數版。改用 SKOSQuoteLib_GetStockByNoNineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetStockByNoNineDigit(string bstrStockNo, ref SKFOREIGN_9 pSKStock);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

### SKOSQuoteLib_GetTick
- 用途：舊版取得成交明細。改用 SKOSQuoteLib_GetTickNineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetTick(short sIndex, int nPtr, ref SKFOREIGNTICK_9 pSKTick);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。更版紀錄（V2.13.42）：回傳物件 SKFOREIGNTICK_9 欄位異動（新增成交日期）。
- 範例：範例未見

### SKOSQuoteLib_GetTickNineDigit
- 用途：舊版（SHORT index）九位小數取 Tick。改用 SKOSQuoteLib_GetTickNineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetTickNineDigit(short sIndex, int nPtr, ref SKFOREIGNTICK_9 pSKTick);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

### SKOSQuoteLib_GetTickLONG
- 用途：僅出現於主說明備註文字（`策略王COM元件使用說明_V2.13.57.md:3221、3233`「避免在…通知事件裡進行 SKOSQuoteLib_GetTickLONG()」），疑為文件沿用 SKQuoteLib／SKOOQuoteLib 敘述之誤植；SKOSQuoteLib 現行對應功能為 SKOSQuoteLib_GetTickNineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetTickLONG(int nIndex, int nPtr, ref SKFOREIGNTICK pSKTick);`（依 SKOOQuoteLib_GetTickLONG 推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：範例碼與功能表皆未見此方法，請以 GetTickNineDigitLONG 為準。
- 範例：範例未見

### SKOSQuoteLib_GetBest5
- 用途：舊版取得最佳五檔。改用 SKOSQuoteLib_GetBest5NineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetBest5(short sIndex, ref SKBEST5 pSKBest5);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

### SKOSQuoteLib_GetBest5NineDigit
- 用途：舊版（SHORT index）九位小數取五檔。改用 SKOSQuoteLib_GetBest5NineDigitLONG。
- 簽名：`int SKOSQuoteLib_GetBest5NineDigit(short sIndex, ref SKBEST5_9 pSKBest5);`（推測，簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：已移除。
- 範例：範例未見

## 事件

### OnConnect
- 用途：接收連線／斷線狀態。
- 簽名：`void OnConnect(int nCode, int nSocketCode)`（handler 掛載：`m_pSKOSQuote.OnConnect += new _ISKOSQuoteLibEvents_OnConnectEventHandler(OnConnect);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nCode | int（LONG） | SK_SUBJECT_CONNECTION_CONNECTED（3001）＝連線事件；SK_SUBJECT_CONNECTION_DISCONNECT＝斷線事件；其他見代碼定義表 |
| nSocketCode | int（LONG） | 事件所獲代碼；0＝執行正確，非 0＝例外事件 |

- 回傳：無（事件 handler）。
- 備註：避免在此事件內直接進行 EnterMonitorLONG、LeaveMonitor、RequestStocks、RequestTicks 等呼叫——各交易所商品未下載完成前無法訂閱。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:458、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:360

### OnOverseaProducts
- 用途：海期報價商品清單資訊逐筆回傳（RequestOverseaProducts 的對應事件）。
- 簽名：`void OnOverseaProducts(string bstrValue)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrValue | string（BSTR） | 字串格式：`[交易所代碼],[交易所名稱],[商品報價代碼],[商品名稱],[最後交易日],[第一通知日]` |

- 回傳：無（事件 handler）。
- 備註：全部資料回傳完畢後，會回傳一筆以「##」開頭的內容表示查詢結束。最後交易日為 0 或 99991231 時，表示該商品沒有最後交易日或為指數匯率等不可交易商品。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:780、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:378

### OnOverseaProductsDetail
- 用途：海期報價商品清單資訊（含下單代碼）逐筆回傳（GetOverseaProductDetail 的對應事件）。
- 簽名：`void OnOverseaProductsDetail(string bstrValue)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrValue | string（BSTR） | 字串格式：`[交易所代碼],[交易所名稱],[商品報價代碼],[商品名稱],[交易所下單代碼],[商品下單代碼],[最後交易日],[第一通知日]` |

- 回傳：無（事件 handler）。
- 備註：「##」開頭表示查詢結束；最後交易日 0／99991231 意義同 OnOverseaProducts。價差商品之[商品下單代碼]由商品代號_年月1/年月2組成，例：`AP_201906/201909`、`JY_201912/201909`。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:785、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:387

### OnNotifyQuoteLONG
- 用途：所訂閱（RequestStocks）的個別海期商品報價異動通知。
- 簽名：`void OnNotifyQuoteLONG(int nIndex)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的海期商品索引代碼 |

- 回傳：無（事件 handler）。
- 備註：事件觸發後，以 nIndex 帶入 SKOSQuoteLib_GetStockByIndexLONG（或 GetStockByIndexNineDigitLONG）取得商品報價物件。須以 SKOSQuoteLib_EnterMonitorLONG 登入才會被觸發。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:800、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:305

### OnNotifyTicksNineDigitLONG
- 用途：所訂閱的個別海期商品成交明細異動時回傳（即時 Tick）。
- 簽名：`void OnNotifyTicksNineDigitLONG(int nIndex, int nPtr, int nDate, int nTime, long nClose, int nQty)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的海期商品索引代碼 |
| nPtr | int（LONG） | 資料的位址（Key），即成交明細順序 |
| nDate | int（LONG） | 交易日期（YYYYMMDD） |
| nTime | int（LONG） | 時間（時分秒） |
| nClose | long（LONGLONG） | 成交價（九位小數擴充，未除小數） |
| nQty | int（LONG） | 成交量 |

- 回傳：無（事件 handler）。
- 備註：價格未進行小數點處理，需依 SKFOREIGN_9LONG 物件內 sDecimal 自行處理。避免在此事件與 OnNotifyHistoryTicksNineDigitLONG 內呼叫 SKOSQuoteLib_GetTickNineDigitLONG。須以 EnterMonitorLONG 登入才會被觸發。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:488、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:257

### OnNotifyHistoryTicksNineDigitLONG
- 用途：首次索取個別海期商品成交明細時，回補當天 Tick。
- 簽名：`void OnNotifyHistoryTicksNineDigitLONG(int nIndex, int nPtr, int nDate, int nTime, long nClose, int nQty)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | int（LONG） | 系統所編的海期商品索引代碼 |
| nPtr | int（LONG） | 資料的位址（Key） |
| nDate | int（LONG） | 交易日期（YYYYMMDD） |
| nTime | int（LONG） | 時間 |
| nClose | long（LONGLONG） | 成交價（未除小數） |
| nQty | int（LONG） | 成交量 |

- 回傳：無（事件 handler）。
- 備註：僅 RequestTicks 會觸發回補（RequestMarketDepth／RequestLiveTick 不回補）。價格未處理小數；避免在此事件內呼叫 GetTickNineDigitLONG。須以 EnterMonitorLONG 登入才會被觸發。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:497（V2 範例 OSQuoteForm.cs:280 掛載處為註解狀態）

### OnNotifyBest5NineDigitLONG
- 用途：所訂閱的個別海期商品最佳五檔異動時回傳。
- 簽名：`void OnNotifyBest5NineDigitLONG(int nStockidx, long nBestBid1, int nBestBidQty1, long nBestBid2, int nBestBidQty2, long nBestBid3, int nBestBidQty3, long nBestBid4, int nBestBidQty4, long nBestBid5, int nBestBidQty5, long nBestAsk1, int nBestAskQty1, long nBestAsk2, int nBestAskQty2, long nBestAsk3, int nBestAskQty3, long nBestAsk4, int nBestAskQty4, long nBestAsk5, int nBestAskQty5)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStockidx | int（LONG） | 系統所編的商品索引代碼 |
| nBestBid1~5 | long（LONGLONG） | 第一～五檔買價（未除小數） |
| nBestBidQty1~5 | int（LONG） | 第一～五檔買量 |
| nBestAsk1~5 | long（LONGLONG） | 第一～五檔賣價（未除小數） |
| nBestAskQty1~5 | int（LONG） | 第一～五檔賣量 |

- 回傳：無（事件 handler）。
- 備註：RequestTicks／RequestMarketDepth 相關通知事件。價格未進行小數點處理（FOREIGN 物件內有小數位數與分母）。避免在此事件內呼叫 GetBest5NineDigitLONG。須以 EnterMonitorLONG 登入才會被觸發。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:506、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:138

### OnNotifyBest10NineDigitLONG
- 用途：所訂閱的個別海期商品最佳十檔異動時回傳。
- 簽名：`void OnNotifyBest10NineDigitLONG(int nStockIdx, long nBestBid1, int nBestBidQty1, ..., long nBestBid10, int nBestBidQty10, long nBestAsk1, int nBestAskQty1, ..., long nBestAsk10, int nBestAskQty10)`（共 41 參數：nStockIdx + 十檔買價/買量 + 十檔賣價/賣量，價格為 long、量為 int，排列同五檔版）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStockIdx | int（LONG） | 系統所編的商品索引代碼 |
| nBestBid1~10 | long（LONGLONG） | 第一～十檔買價（未除小數） |
| nBestBidQty1~10 | int（LONG） | 第一～十檔買量 |
| nBestAsk1~10 | long（LONGLONG） | 第一～十檔賣價（未除小數） |
| nBestAskQty1~10 | int（LONG） | 第一～十檔賣量 |

- 回傳：無（事件 handler）。
- 備註：RequestTicks／RequestMarketDepth 相關通知事件。十檔沒有對應的 Get 函式，資料直接取自事件參數。價格未處理小數。須以 EnterMonitorLONG 登入才會被觸發。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:601、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:186

### OnKLineData
- 用途：歷史 K 線資料逐筆回傳（RequestKLine／RequestKLineByDate 的對應事件）。
- 簽名：`void OnKLineData(string bstrStockNo, string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | string（BSTR） | 商品代碼 |
| bstrData | string（BSTR） | `[日期(時間)],[開盤價],[最高價],[最低價],[收盤價],[成交量(張) 或 成交金額]`，以逗號分隔 |

- 回傳：無（事件 handler）。
- 備註：不提供開盤參考價。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:790、Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OSQuoteForm.cs:296

### OnNotifyServerTime（僅見於範例碼）
- 用途：報價主機時間通知（SKOSQuoteLib_RequestServerTime 的對應事件；21 份 docx 手冊未記載海期版本）。
- 簽名：`void OnNotifyServerTime(short sHour, short sMinute, short sSecond)`（自範例碼 handler 萃取；注意與 SKQuoteLib 版不同——海期版無第 4 個參數 nTotal）
- 參數表（文件未載，依範例碼推測）：

| 參數 | 型別 | 說明 |
|---|---|---|
| sHour | short（SHORT） | 時 |
| sMinute | short（SHORT） | 分 |
| sSecond | short（SHORT） | 秒 |

- 回傳：無（事件 handler）。
- 備註：掛載名 `_ISKOSQuoteLibEvents_OnNotifyServerTimeEventHandler`。
- 範例：Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOSQuote.cs:100（掛載）、:795（handler）

## 陷阱與注意

1. **一律使用 LONG index 家族**：單一市場商品總數可能超過 SHORT 上限（32767），V2.13.31 起新增 LONG 版函式／事件，V2.13.46（含）以上版本已移除舊版行情函式（`2.導覽.md:284`）。以 EnterMonitorLONG 登入後，非 LONG 舊事件（OnNotifyQuote、OnNotifyTicks、OnNotifyHistoryTicks、OnNotifyTicksNineDigit、OnNotifyHistoryTicksNineDigit、OnNotifyBest5、OnNotifyBest5NineDigit、OnNotifyBest10、OnNotifyBest10NineDigit）**一律不會觸發**；只會觸發 OnNotifyQuoteLONG、OnNotifyTicksNineDigitLONG、OnNotifyHistoryTicksNineDigitLONG、OnNotifyBest5NineDigitLONG、OnNotifyBest10NineDigitLONG（此觸發規則依 SKQuoteLib_EnterMonitorLONG 說明類推，主說明:2577；海期文件僅於各 LONG 事件載明「須以 EnterMonitorLONG 登入才會觸發」）。
2. **CME 九位小數擴充**：海期 Tick／五檔／十檔事件與 Get 函式的價格為 `long`（LONGLONG）且**未經小數處理**，需自行依 SKFOREIGN_9LONG（或 SKFOREIGNLONG）的 `sDecimal`（報價小數位數）／`nDenominator`（分母）換算。範例碼中 `/ 100.0` 僅為示意，實務應以 sDecimal 為準。
3. **僅限連線後使用**：GetQuoteStatus 限當次報價連線成功後查詢；RequestOverseaProducts、GetOverseaProductDetail、GetStockByNo/ByIndex 系列、GetTick/GetBest5 系列皆須先以 EnterMonitorLONG 登入。錯誤碼 2025 即「請先執行海期報價 SKOSQuoteLib_EnterMonitorLONG 連線」。
4. **勿在 OnConnect 內訂閱**：各交易所商品未下載完成前，RequestStocks／RequestTicks 等訂閱會失敗；請等商品檔下載完成（可查 日期_OSQuote.log 的 LoadOSCommdity）再訂閱。
5. **勿在通知事件內呼叫 Get 函式**：避免在 OnNotifyTicksNineDigitLONG／OnNotifyHistoryTicksNineDigitLONG 內呼叫 GetTickNineDigitLONG；避免在 OnNotifyBest5NineDigitLONG 內呼叫 GetBest5NineDigitLONG。文件備註以全名稱 SKOSQuoteLib_OnNotifyTicksNineDigitLONG、SKOSQuoteLib_OnNotifyHistoryTicksNineDigitLONG、SKOSQuoteLib_OnNotifyBest5NineDigitLONG 指涉這些事件；主說明 3221/3233 行另誤植為 SKOSQuoteLib_OnNotifyTicksLONG／SKOSQuoteLib_OnNotifyHistoryTicksLONG／GetTickLONG，SKOSQuoteLib 實際上無該名稱成員。
6. **三種 Tick 訂閱擇一**：RequestTicks（明細＋五檔＋十檔＋歷史回補）、RequestMarketDepth（僅五檔十檔）、RequestLiveTick（僅即時明細）請擇一使用；後兩者無歷史回補，GetTickNineDigitLONG 只能查到訂閱後的即時明細。
7. **訂閱字串驗證**：RequestStocks 多筆以 `#` 分隔，其中含任一無效商品代碼時**整批不送出**；RequestTicks／RequestMarketDepth／RequestLiveTick 一個 Page 僅能訂一檔。
8. **前置條件**：使用海期報價需先簽署期貨 API 下單聲明書；有海期帳號時預設 Login 即占用一條海期報價連線。
9. **資訊源切換**：SetOSQuoteServer 切換的是資訊源（非 Server），切換後必須斷線重連才生效；僅 CME、CBOT、NYM、SGX、HKEx（海期）支援。
10. **IsConnected 回傳慣例不同**：1 表示連線中（非 0=成功的慣例）。
11. **非交易日資料**：SKFOREIGNLONG／SKFOREIGN_9LONG 於當日非交易日時，資料為前一交易日。
12. **SKBEST5_9 的衍生檔欄位**：nExtendBid／nExtendAsk 等衍生一檔欄位海外市場無值。nSimulate：0=一般、1=試算。
13. **V2.13.59 發佈包已移除全部海期報價範例**：四個範例專案同批下架海期報價示範——SKCOMTester 的 `SKOSQuote.cs/.Designer.cs/.resx` 三檔在 .59 樹中不存在，csproj 只剩 `SKQuote.cs`（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKCOMTester.csproj:170`），`Form1.cs:27` 僅殘留 `SKOSQuoteLib m_pSKOSQuote;` 的孤兒宣告（該專案已無 `new SKOSQuoteLib()`）；SKCOMTesterV2 的 `Quote\OSQuoteForm.*` 已移除，Quote 目錄只剩 QuoteForm（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/csapiTester.csproj:142`）；CppCLITester 的 `SKOSQuote.cpp/.h/.resx` 已自 vcxproj 移除，只剩 SKQuote（`Source_code/CapitalAPI_2.13.59_CExample/CppCLITester/CppCLITester/CppCLITester.vcxproj:140`）；ExcelSample 的海期報價分頁亦已拿掉（`Source_code/CapitalAPI_2.13.59_CExample/ExcelSample/ExcelSample.xls`，二進位檔無行號；`strings` 比對 .57 含 14 處 SKOSQuote 字串、.59 為 0）。隨包分冊 `14.海期報價.docx` 同樣不在 .59 說明文件夾。官方版本歷程未公告此次下架，本檔的範例引用因此一律停留在 `CapitalAPI_2.13.57_CExample/`。
14. **COM 介面本身未變，海期報價仍可用**：文件與範例下架不等於 API 移除。Interop.SKCOMLib 的 SKOSQuoteLib 符號 sym57↔sym59 為 56:56 逐行相同，原生 SKCOM.dll x64 匯出表未減（78→83、+5/−0，且 `SKOSQuoteLib_RequestStocks`／`RequestTicks`／`GetStockByNoNineDigitLONG` 兩版皆在），`SKOSQuoteLib_EnterMonitorLONG` 仍被 .59 範例的海期下單表單呼叫（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:321`），錯誤碼 2025 也仍列於 .59 手冊（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4844`）。既有海期報價程式升到 2.13.59 在介面層不需改寫（惟 .59 版本歷程有一句未指名模組的「修正index不一致問題」，見同檔 `:38`，升級後建議回歸驗證 nIndex／nStockidx 與商品代號的對應是否仍正確）；但排查問題時官方已無對應章節可查，請以本檔與 `_raw/14.海期報價.md`（.57 留存）為準。
15. **V2.13.59 簡化版 SKDLLCSharp.dll 移除海期報價包裝方法（DLL 路線 breaking）**：走簡化版 C# DLL（`SKDLLCSharp.dll`）而非直接使用 COM 的程式，在 .59 會編譯失敗——`SKOSQuoteLib_RequestStocks`、`SKOSQuoteLib_RequestTicks`、`SKOSQuoteLib_GetStockByNoNineDigitLONG` 三個公開方法在 .59 的包裝層 DLL 中已不存在（x64／x86 一致，`strings` 實測 .57 各命中 1、.59 全為 0），`OnNotifyOSQuoteLONG`／`OnNotifyOSBest10`／`OnNotifyOSTicks` 的 add_/remove_/RegisterEvent 符號雖殘留卻失去配套的訂閱與取值方法，形同孤兒。包裝層手冊的「海期行情」整段亦已刪除（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md` 全檔 0 處提及「海期行情」），`ManageServerConnection` 的 nTargetType 由「0:回報；1:國內行情；2:海期行情；3:海選行情；4:Proxy下單」縮為「0:回報；1:國內行情；4:Proxy下單」（同檔 `:50`），OnConnection 的 loginID 說明也移除了「海期行情：SKOSQuote」（同檔 `:39`），範例下拉選單同步只剩三項（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.Designer.cs:4470-4473`）。底層 COM 完好，因此解法是改走 Interop.SKCOMLib 直接呼叫 SKOSQuoteLib。
16. **V2.13.59 已無錯誤碼 2015 與 OSQuote.log 的排查說明**：`SKOrderLib_LoadOSCommodity` 的備註在 .59 精簡為只剩「具海期帳號，海期委託下單前須先下載」（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:185`），.57 原有的「與 SKOSQuoteLib_EnterMonitorLONG 相關，可以先進行海期連線備妥商品檔」「出現錯誤代碼 2015，請重新連海期行情主機或重新下載」以及用 `日期_OSQuote.log` 確認商品檔是否下載成功的說明全數消失。函式簽名與回傳語意未變，遇到 2015／商品檔未備妥時請回頭參考 V2.13.57 文件與本檔第 4、8 條。

## 附錄：相關 Struct 結構物件（見 `_raw/14.海期報價.md:693-791`）

> V2.13.59 現況：主手冊的 5-8 SKFOREIGNTICK、5-12 SKFOREIGNTICK_9、5-22 SKFOREIGNLONG、5-23 SKFOREIGN_9LONG 四個結構章節已刪除（結構編號斷層：`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4100` 的 5-6 直接跳到 `:4116` 的 5-9、`:4164` 的 5-13 前無 5-12、`:4484` 的 5-21 直接跳到 `:4559` 的 5-24）；SKBEST5_9 則仍留在 .59 手冊（同檔 `:4164`）。結構定義本身未變更，下表以 V2.13.57 留存原文為準。

| Struct | 用途 | 關鍵欄位 |
|---|---|---|
| SKFOREIGNLONG | 海外報價商品物件（LONG index） | nStockidx、sDecimal（報價小數位數）、nDenominator（分母）、bstrMarketNo、bstrExchangeNo/Name、bstrStockNo/Name、bstrCallPut、nOpen/nHigh/nLow/nClose（int）、nSettlePrice、nTickQty、nRef、nBid/nBc/nAsk/nAc、nTQty、nStrikePrice、nTradingDay |
| SKFOREIGN_9LONG | 海外報價商品物件—九位擴充（LONG index） | 同上，但價格欄（nOpen/nHigh/nLow/nClose/nSettlePrice/nRef/nBid/nAsk/nTQty）為 long（LONGLONG） |
| SKFOREIGNTICK_9 | 海外報價 TICK 物件—九位擴充 | nPtr、nTime、nClose（long）、nQty、nDate（YYYYMMDD，依交易所時區；V2.13.42 新增） |
| SKBEST5_9 | 五檔價格物件—九位擴充 | nBid1~5/nAsk1~5（long）、nBidQty1~5/nAskQty1~5、nExtendBid/Ask（海外無值）、nSimulate（0一般/1試算） |
