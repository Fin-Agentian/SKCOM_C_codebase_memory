# SKOOQuoteLib — 海選（海外選擇權）報價元件：連線報價伺服器、下載商品檔、訂閱即時報價／Ticks／五檔／十檔

> 來源：`api_spec/_raw/15.海選報價.md`、`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md`（4-6 節，行 3309–3567）、`api_spec/_raw/2.導覽.md`。
> 版本基準：V2.13.59（以 V2.13.57 規格為底增補；差異見 [../changelog_2.13.57_to_2.13.59.md](../changelog_2.13.57_to_2.13.59.md)）。自 V2.13.31 起新增 LONG index 系列；**V2.13.46（含）以上已移除舊版（非 LONG）行情函式**（此句為官方文字，與 Interop 符號現況有出入，見「陷阱與注意」第 1 條）。
>
> **V2.13.59 文件狀態（重要）**：官方 V2.13.59 手冊已移除 4-6 SKOOQuoteLib（海選報價）整章與 `15.海選報價` 分冊；函式本身未移除（Interop 符號兩版一致），本檔各節以 V2.13.57 留存原文為準。佐證：
> - 合訂本章節編號由 4-4 SKQuoteLib（國內報價）直接跳到 4-7 SKOrderLib（proxy server下單），中間 4-5／4-6 兩章不存在（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2187`、`:2920`）；全文僅在錯誤碼表 `:4845` 尚提及 SKOOQuoteLib（錯誤碼 2026）。
> - `api_spec/_raw/v2.13.59/2.導覽.md:103` 同樣只剩錯誤碼 2026 那一列，物件介紹與架構圖條目已刪。
> - `api_spec/_raw/v2.13.59/` 目錄無 `15.海選報價.md`（分冊編號 1–13、16 齊全，14／15 缺號）；結構 SKFOREIGNLONG／SKFOREIGNTICK 等章節亦一併刪除。
> - 但 Interop.SKCOMLib 的 SKOOQuoteLib 符號 .57↔.59 共 37 個逐行相同（含非 LONG 的 `SKOOQuoteLib_EnterMonitor`／`GetStockByIndex`／`GetStockByNo`／`GetTick`／`GetBest5`，以及本檔未收錄的 `SKOOQuoteLib_SetQuoteServer`），原生 SKCOM.dll 匯出表兩版同為 `SKOOQuoteLib_GetStockByNoLONG`／`RequestStocks`／`RequestTicks` 三支、無增減；`.59` 範例的海選下單表單仍在呼叫 `SKOOQuoteLib_EnterMonitorLONG`（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:326`）。**文件下架不等於功能下架。**

## 總覽：功能分區表

| 分區 | 函式 / 事件 | 用途一句話 |
|---|---|---|
| 連線 | SKOOQuoteLib_EnterMonitorLONG | (LONG index) 與海選報價伺服器建立連線 |
| 連線 | SKOOQuoteLib_LeaveMonitor | 中斷報價伺服器連線 |
| 連線 | SKOOQuoteLib_IsConnected | 查詢目前連線狀態 |
| 商品清單 | SKOOQuoteLib_RequestProducts | 下載海外商品檔（結果由 OnProducts 回傳） |
| 海選資訊 | SKOOQuoteLib_GetStockByNoLONG | 依商品代號取回商品報價物件（SKFOREIGNLONG） |
| 即時報價 | SKOOQuoteLib_RequestStocks | 訂閱指定商品即時報價（OnNotifyQuoteLONG 通知） |
| 即時報價 | SKOOQuoteLib_GetStockByIndexLONG | 依系統索引代碼取回商品報價物件 |
| 十檔&五檔&成交明細 | SKOOQuoteLib_RequestTicks | 訂閱 Ticks＋五檔＋十檔（含當日回補） |
| 十檔&五檔&成交明細 | SKOOQuoteLib_RequestLiveTick | 僅訂閱即時成交明細（無五檔/十檔/回補） |
| 十檔&五檔&成交明細 | SKOOQuoteLib_RequestMarketDepth | 僅訂閱最佳十檔（含五檔，無成交明細） |
| 十檔&五檔&成交明細 | SKOOQuoteLib_GetTickLONG | 取得指定筆成交明細物件（SKFOREIGNTICK） |
| 十檔&五檔&成交明細 | SKOOQuoteLib_GetBest5LONG | 取得最佳五檔物件（SKBEST5） |
| 事件-連線 | OnConnect | 連線／斷線狀態通知 |
| 事件-商品清單 | OnProducts | 海選商品清單逐筆回傳 |
| 事件-即時報價 | OnNotifyQuoteLONG | (LONG index) 報價異動通知 |
| 事件-成交明細 | OnNotifyTicksLONG | (LONG index) 即時 Tick 通知 |
| 事件-成交明細 | OnNotifyHistoryTicksLONG | (LONG index) 當日 Tick 回補通知 |
| 事件-五檔 | OnNotifyBest5LONG | (LONG index) 最佳五檔異動通知 |
| 事件-十檔 | OnNotifyBest10LONG | (LONG index) 最佳十檔異動通知 |
| 已移除（V2.13.46 起） | SKOOQuoteLib_EnterMonitor / GetStockByIndex / GetStockByNo / GetTick / GetBest5 | 舊版 SHORT index 函式，改用對應 LONG 版 |
| 已移除（V2.13.46 起） | OnNotifyQuote / OnNotifyTicks / OnNotifyHistoryTicks / OnNotifyBest5 / OnNotifyBest10 | 舊版 SHORT index 事件，改用對應 LONG 版 |

> **V2.13.59 訂正**：官方 V2.13.59 已回溯改寫 2.13.46 的版本歷程條目，「移除函式如下」的範圍縮為只剩「國內報價 SKQuoteLib」，不再包含海期／海選報價（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:25`）。上表兩列「已移除（V2.13.46 起）」的非 LONG 成員，在 .57／.59 的 Interop.SKCOMLib 中確實都仍有型別宣告。**但 typelib 有宣告不等於功能仍提供，這段刪字也不足以推論舊版函式已恢復提供**：SKQuoteLib 的非 LONG 版同樣仍列於 sym59，官方卻始終將其列為已移除，可見官方所稱「移除」指的是不再提供／不再支援，而非把宣告自 typelib 拿掉（同一判讀見 `SKOSQuoteLib.md` 的「舊版函式」節）。官方兩版都未提供這些非 LONG 成員的文件與簽名，**新開發一律使用 EnterMonitorLONG＋`*LONG` 系列**，本檔下方各「（已移除）」節維持原稱呼以對應 V2.13.57 官方敘述。

## 初始化與事件註冊

C# 實際寫法（抄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs`）：

> **V2.13.59 範例狀態**：官方 V2.13.59 範例包已整批下架海選報價示範，本節與下方各節引用的 `OOQuoteForm.cs`、`SKOOQuote.cs` 在 `.59` 樹中皆不存在（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/csapiTester.csproj:142` 的 Quote 目錄只剩 `QuoteForm.*`；`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKCOMTester.csproj:170` 只剩 `SKQuote.cs`）。程式碼本身仍可照抄——COM 介面未變；引用行號請沿用 `.57` 樹。`.59` 的 `Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/Form1.cs:28` 還留著 `SKOOQuoteLib m_pSKOOQuote;` 欄位宣告，但該專案內已無任何 `new SKOOQuoteLib()` 與事件掛載，是刪頁籤時遺留的孤兒宣告，勿誤以為那裡有可參考的初始化流程。`.59` 全樹僅剩三個海選下單表單各自建立物件（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:20`、`Order/StrategyOrderForm/OFStrategyOrderForm.cs:20`、`Order/SKProxyOrder/SKProxySendOrderForm/OFSKProxySendOrderForm.cs:20`），且只用來呼叫 `SKOOQuoteLib_EnterMonitorLONG` 連線備妥商品檔，未示範任何訂閱與事件處理。

```csharp
using SKCOMLib;   // Interop.SKCOMLib（COM 元件的 .NET Interop）

// 宣告物件（OOQuoteForm.cs:20-21）
SKCenterLib m_pSKCenter = new SKCenterLib();     // 登入&環境設定物件
SKOOQuoteLib m_pSKOOQuote = new SKOOQuoteLib();  // 海選報價物件

// 事件掛載（可放在 Form 的 Load 事件裡）
// OOQuoteForm.cs:115
m_pSKOOQuote.OnConnect += new _ISKOOQuoteLibEvents_OnConnectEventHandler(OnConnect);
// OOQuoteForm.cs:133
m_pSKOOQuote.OnProducts += new _ISKOOQuoteLibEvents_OnProductsEventHandler(OnProducts);
// OOQuoteForm.cs:144
m_pSKOOQuote.OnNotifyQuoteLONG += new _ISKOOQuoteLibEvents_OnNotifyQuoteLONGEventHandler(OnNotifyQuoteLONG);
// OOQuoteForm.cs:190
m_pSKOOQuote.OnNotifyTicksLONG += new _ISKOOQuoteLibEvents_OnNotifyTicksLONGEventHandler(OnNotifyTicksLONG);
// OOQuoteForm.cs:213（範例中註解掉，回補事件簽名同 OnNotifyTicksLONG）
// m_pSKOOQuote.OnNotifyHistoryTicksLONG += new _ISKOOQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(OnNotifyHistoryTicksLONG);
// OOQuoteForm.cs:229
m_pSKOOQuote.OnNotifyBest5LONG += new _ISKOOQuoteLibEvents_OnNotifyBest5LONGEventHandler(OnNotifyBest5LONG);
// OOQuoteForm.cs:276
m_pSKOOQuote.OnNotifyBest10LONG += new _ISKOOQuoteLibEvents_OnNotifyBest10LONGEventHandler(OnNotifyBest10LONG);

// 連線（可放在 Button 的 Click 事件裡；OOQuoteForm.cs:390-393）
int nCode = m_pSKOOQuote.SKOOQuoteLib_EnterMonitorLONG();
string msg = "【SKOOQuoteLib_EnterMonitorLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
```

典型流程：`SKCenterLib_Login` 成功 → `SKOOQuoteLib_EnterMonitorLONG` → 等 `OnConnect` 回傳 3001（連線）與商品下載完成 → `RequestProducts` / `RequestStocks` / `RequestTicks` → 由 `OnNotify*LONG` 事件接資料。

## 方法

### SKOOQuoteLib_EnterMonitorLONG

- 用途：(LONG index) 與海選報價伺服器建立連線。
- 簽名：`int SKOOQuoteLib_EnterMonitorLONG();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：連線狀態由 OnConnect 事件回傳。需先簽署期貨API下單聲明書方可使用。與舊版 SKOOQuoteLib_EnterMonitor 僅能擇一使用；用本函式登入後，系統只觸發 LONG 系列事件（OnNotifyQuoteLONG 等），非 LONG 舊事件不會觸發。海選委託下單前須先備妥商品檔，可先用本函式連線下載（見 **V2.13.57 版**分冊 `9.下單-海外期選.md`；出現錯誤代碼 2015 請重連海期行情主機或重新下載）。
  - V2.13.59 文件變更：官方已把分冊 9 與合訂本 4-2-40 `SKOrderLib_LoadOOCommodity` 的備註精簡為只剩「具海期帳號，海選委託下單前須先下載」，刪去「與 SKOOQuoteLib_EnterMonitorLONG 相關，可以先進行海選連線備妥商品檔」與錯誤代碼 2015 的排除方法（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:204`、`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:840`）。函式簽名與回傳語意未變，錯誤碼 2015（SK_WARNING_DOWNLOAD_OF_COM_DATA_IS_TIMEOUT，「下單:下載海期商品檔未完成」）本身仍列於 .59 錯誤碼表（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4834`），只是排查步驟在 .59 已無處可查，需回頭參考 V2.13.57 版文件。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:390`、`SKCOMTester/SKOOQuote.cs:92`、`SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:326`、`Order/StrategyOrderForm/OFStrategyOrderForm.cs:354`、`Order/SKProxyOrder/SKProxySendOrderForm/OFSKProxySendOrderForm.cs:294`

### SKOOQuoteLib_LeaveMonitor

- 用途：中斷報價伺服器連線。
- 簽名：`int SKOOQuoteLib_LeaveMonitor();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：連線狀態由 OnConnect 事件回傳。避免在 OnConnect 事件內直接呼叫。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:398`（FormClosing 亦於 :350 呼叫）、`SKCOMTester/SKOOQuote.cs:99`

### SKOOQuoteLib_IsConnected

- 用途：檢查目前連線狀態。
- 簽名：`int SKOOQuoteLib_IsConnected();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG；**1 表示連線中**，其餘非 1 數值都表示失敗（注意：非一般 0=成功 慣例）。
- 備註：請同時接收 OnConnect 通知事件。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:406`、`SKCOMTester/SKOOQuote.cs:247`

### SKOOQuoteLib_RequestProducts

- 用途：取得海外商品檔（商品清單）。
- 簽名：`int SKOOQuoteLib_RequestProducts();`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：商品資訊由 OnProducts 事件逐筆取得；全部回傳完畢會收到一筆以「##」開頭的內容。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:418`、`SKCOMTester/SKOOQuote.cs:107`

### SKOOQuoteLib_RequestStocks

- 用途：訂閱指定商品即時報價；報價更新由 OnNotifyQuoteLONG 事件通知。
- 簽名（V2 Interop 實際呼叫）：`int SKOOQuoteLib_RequestStocks(short psPageNo, string bstrStockNos);`（舊 SKCOMTester Interop 為 `ref short psPageNo`；IDL 宣告 `Long SKOOQuoteLib_RequestStocks([in,out] SHORT* psPageNo, [in] BSTR bstrStockNos);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（C# short 或 ref short） | 請固定帶 1。 |
| bstrStockNos | BSTR | 欲訂閱的海選商品代號，以{「交易所代碼」,「商品報價代碼」}為單位，多筆以 `#` 區隔。例：`CBOT,ZN12400X7#HKEx,HSI28000L7#CBOT,YM23500X7` |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：避免在 OnConnect 事件內直接訂閱（各交易所商品未下載完成會訂閱失敗）。對應事件：OnNotifyQuoteLONG。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:486`、`SKCOMTester/SKOOQuote.cs:148`（ref 寫法）

### SKOOQuoteLib_RequestTicks

- 用途：訂閱與要求傳送成交明細、五檔、十檔（首次訂閱含當日 Tick 回補）。
- 簽名（V2 Interop 實際呼叫）：`int SKOOQuoteLib_RequestTicks(short psPageNo, string bstrStockNo);`（舊 Interop 為 `ref short`；IDL：`Long SKOOQuoteLib_RequestTicks([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（C# short 或 ref short） | 請從 1 開始。 |
| bstrStockNo | BSTR | 指定商品，一個 Page 僅能索取一檔。格式「交易所代碼」,「商品報價代碼」，例：`CBOT,YM35000U1` |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：即時 Tick → OnNotifyTicksLONG；回補 Tick → OnNotifyHistoryTicksLONG；五檔 → OnNotifyBest5LONG；十檔 → OnNotifyBest10LONG。避免在 OnConnect 內直接訂閱。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:503`、`SKCOMTester/SKOOQuote.cs:194`（ref 寫法）

### SKOOQuoteLib_RequestLiveTick

- 用途：訂閱與要求傳送「即時」成交明細；不訂閱五檔/十檔、不含歷史 Ticks 回補。
- 簽名（依 IDL 重建）：`int SKOOQuoteLib_RequestLiveTick(short psPageNo, string bstrStockNo);`（舊 Interop 為 `ref short`；IDL：`Long SKOOQuoteLib_RequestLiveTick([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（C# short 或 ref short） | 請從 1 開始。 |
| bstrStockNo | BSTR | 索取的商品代號，一個 Page 僅能索取一檔。 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：即時 Tick 由 OnNotifyTicksLONG 通知。與 RequestTicks **請擇一使用**；因不回補歷史成交明細，用本函式時 SKOOQuoteLib_GetTickLONG 僅能查閱「訂閱後」的即時成交明細。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:271`（V2 的 OOQuoteForm.cs:507 為註解示範）

### SKOOQuoteLib_RequestMarketDepth

- 用途：要求傳送最佳十檔（包含最佳五檔；不訂閱歷史與即時成交明細）。
- 簽名（V2 Interop 實際呼叫）：`int SKOOQuoteLib_RequestMarketDepth(short psPageNo, string bstrStockNo);`（舊 Interop 為 `ref short`；IDL：`Long SKOOQuoteLib_RequestMarketDepth([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（C# short 或 ref short） | 請從 1 開始。 |
| bstrStockNo | BSTR | 指定商品，一個 Page 僅能索取一檔。格式「交易所代碼」,「商品報價代碼」，例：`CBOT,YM35000I1` |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：五檔 → OnNotifyBest5LONG；十檔 → OnNotifyBest10LONG。避免在 OnConnect 內直接訂閱。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:524`、`SKCOMTester/SKOOQuote.cs:960`（ref 寫法）

### SKOOQuoteLib_GetStockByNoLONG

- 用途：(LONG index) 依商品代號取回海選報價相關資訊（填入 SKFOREIGNLONG 物件）。
- 簽名：`int SKOOQuoteLib_GetStockByNoLONG(string bstrStockNo, ref SKFOREIGNLONG pSKStock);`（IDL：`Long SKOOQuoteLib_GetStockByNoLONG([in] BSTR bstrStockNo, [in,out] struct SKFOREIGNLONG* pSKStock);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 海選商品代號，例：`CBOT,YM35000I1`（含交易所代碼） |
| pSKStock | SKFOREIGNLONG* | SKCOM 元件的 SKFOREIGNLONG 物件，帶入此欄位 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：須先以 SKOOQuoteLib_EnterMonitorLONG 登入。SKFOREIGNLONG 主要欄位：nStockidx（LONG 索引）、sDecimal（報價小數位數）、nDenominator（分母）、bstrMarketNo/bstrExchangeNo/bstrExchangeName、bstrStockNo/bstrStockName、bstrCallPut、nOpen/nHigh/nLow/nClose/nSettlePrice/nTickQty/nRef/nBid/nBc/nAsk/nAc/nTQty/nStrikePrice/nTradingDay。價格欄位未經小數處理，需依 sDecimal（及 nDenominator）自行換算；當日非交易日時資料為前一交易日。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:428`、`SKCOMTester/SKOOQuote.cs:309`

### SKOOQuoteLib_GetStockByIndexLONG

- 用途：(LONG index) 依系統所編的索引代碼取回海選報價相關資訊；通常在 OnNotifyQuoteLONG 事件內以 nIndex 呼叫。
- 簽名：`int SKOOQuoteLib_GetStockByIndexLONG(int nIndex, ref SKFOREIGNLONG pSKStock);`（IDL：`Long SKOOQuoteLib_GetStockByIndexLONG([in] LONG nIndex, [in,out] struct SKFOREIGNLONG* pSKStock);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | LONG | 系統所編的海選商品索引代碼 |
| pSKStock | SKFOREIGNLONG* | SKCOM 元件的 SKFOREIGNLONG 物件，帶入此欄位 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：商品物件價格未經小數處理。避免在 OnConnect 事件內直接呼叫（商品未下載完成會取不到基本資料）。須先以 SKOOQuoteLib_EnterMonitorLONG 登入。對應事件：OnNotifyQuoteLONG。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:152`、`SKCOMTester/SKOOQuote.cs:306`

### SKOOQuoteLib_GetTickLONG

- 用途：(LONG index) 取得指定第幾筆成交明細資訊（需先以 RequestTicks 訂閱）。
- 簽名：`int SKOOQuoteLib_GetTickLONG(int nIndex, int nPtr, ref SKFOREIGNTICK pSKTick);`（IDL：`Long SKOOQuoteLib_GetTickLONG([in] LONG nIndex, [in] LONG nPtr, [in,out] struct SKFOREIGNTICK* pSKTick);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | LONG | 系統所編的海選商品索引代碼 |
| nPtr | LONG | 第幾筆成交明細（成交明細順序） |
| pSKTick | SKFOREIGNTICK* | SKCOM 元件的 SKFOREIGNTICK 物件，帶入此欄位 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：**避免在 OnNotifyHistoryTicksLONG / OnNotifyTicksLONG 事件內呼叫本函式**（官方文件明載，範例雖示範但實務請於事件外處理）。須先以 SKOOQuoteLib_EnterMonitorLONG 登入。SKFOREIGNTICK 欄位：nPtr、nTime、nClose（需依 sDecimal 自行處理小數）、nQty、nDate（YYYYMMDD，依交易所時區；V2.13.42 新增）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:196`、`SKCOMTester/SKOOQuote.cs:214`

### SKOOQuoteLib_GetBest5LONG

- 用途：(LONG index) 取得最佳五檔價格資訊（需先以 RequestTicks 或 RequestMarketDepth 訂閱）。
- 簽名：`int SKOOQuoteLib_GetBest5LONG(int nIndex, ref SKBEST5 pSKBest5);`（IDL：`Long SKOOQuoteLib_GetBest5LONG([in] LONG nIndex, [in,out] struct SKBEST5* pSKBest5);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | LONG | 系統所編的海選商品索引代碼 |
| pSKBest5 | SKBEST5* | SKCOM 元件的 SKBEST5 物件，帶入此欄位 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：五檔更新由 OnNotifyBest5LONG 事件通知。須先以 SKOOQuoteLib_EnterMonitorLONG 登入。SKBEST5 欄位：nBid1~5/nBidQty1~5、nAsk1~5/nAskQty1~5、nExtendBid/nExtendBidQty、nExtendAsk/nExtendAskQty（衍生一檔，**海外無衍生一檔**）、nSimulate（0:一般 1:試算/試撮）。價格欄需依 sDecimal 自行處理小數。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:235`、`SKCOMTester/SKOOQuote.cs:233`

### SKOOQuoteLib_EnterMonitor（已移除）

- 用途：舊版（SHORT index）與報價伺服器建立連線。
- 簽名：`int SKOOQuoteLib_EnterMonitor();`（簽名待確認；V2.13.46 起移除，僅見於導覽之新舊對照表）
- 參數表：| 參數 | 型別 | 說明 |（文件未載——現行手冊已刪除本節）
- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：V2.13.31 起由 SKOOQuoteLib_EnterMonitorLONG 取代（商品總數可能超過 SHORT 上限 32767）；與 LONG 版僅能擇一使用；V2.13.46（含）以上版本不提供。
- 範例：範例未見

### SKOOQuoteLib_GetStockByIndex（已移除）

- 用途：舊版（SHORT index）依索引代碼取回商品報價物件。
- 簽名：`int SKOOQuoteLib_GetStockByIndex(short sIndex, ref SKFOREIGN pSKStock);`（簽名待確認，依 LONG 版與 SKFOREIGN/SKFOREIGNLONG 差異推定）
- 參數表：| 參數 | 型別 | 說明 |（文件未載——現行手冊已刪除本節）
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：V2.13.31 起由 SKOOQuoteLib_GetStockByIndexLONG 取代；V2.13.46 起移除。
- 範例：範例未見

### SKOOQuoteLib_GetStockByNo（已移除）

- 用途：舊版（SHORT index）依商品代號取回商品報價物件。
- 簽名：`int SKOOQuoteLib_GetStockByNo(string bstrStockNo, ref SKFOREIGN pSKStock);`（簽名待確認）
- 參數表：| 參數 | 型別 | 說明 |（文件未載——現行手冊已刪除本節）
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：V2.13.31 起由 SKOOQuoteLib_GetStockByNoLONG 取代；V2.13.46 起移除。
- 範例：範例未見

### SKOOQuoteLib_GetTick（已移除）

- 用途：舊版（SHORT index）取得成交明細物件。
- 簽名：`int SKOOQuoteLib_GetTick(short sIndex, int nPtr, ref SKFOREIGNTICK pSKTick);`（簽名待確認）
- 參數表：| 參數 | 型別 | 說明 |（文件未載——現行手冊已刪除本節）
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：V2.13.31 起由 SKOOQuoteLib_GetTickLONG 取代；V2.13.46 起移除。
- 範例：範例未見

### SKOOQuoteLib_GetBest5（已移除）

- 用途：舊版（SHORT index）取得最佳五檔物件。
- 簽名：`int SKOOQuoteLib_GetBest5(short sIndex, ref SKBEST5 pSKBest5);`（簽名待確認）
- 參數表：| 參數 | 型別 | 說明 |（文件未載——現行手冊已刪除本節）
- 回傳：LONG 錯誤碼（見 [../error_codes.md](../error_codes.md)）。
- 備註：V2.13.31 起由 SKOOQuoteLib_GetBest5LONG 取代；V2.13.46 起移除。
- 範例：範例未見

## 事件

### OnConnect

- 用途：接收連線／斷線狀態。
- 簽名（handler）：`void OnConnect(int nCode, int nSocketCode);`（IDL：`void OnConnect([in] LONG nCode, [in] LONG nSocketCode);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nCode | LONG | SK_SUBJECT_CONNECTION_CONNECTED＝連線事件；SK_SUBJECT_CONNECTION_DISCONNECT＝斷線事件；其他見代碼定義表 |
| nSocketCode | LONG | 事件所獲得的代碼；0＝執行正確，非 0＝例外事件 |

- 備註：**避免在此事件內直接進行** EnterMonitor(LONG)、LeaveMonitor、RequestStocks、RequestTicks 等操作——各交易所商品未下載完成時將無法訂閱。參數名稱與 SKCOM.dll `_ISKOOQuoteLibEvents` 一致。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:115`、`SKCOMTester/SKOOQuote.cs:75`（handler 定義 :284）

### OnProducts

- 用途：海選報價商品清單資訊（RequestProducts 的回傳）。
- 簽名（handler）：`void OnProducts(string bstrValue);`（IDL：`void OnProducts([in] BSTR bstrValue);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrValue | BSTR | 字串格式：`[交易所代碼],[交易所名稱],[商品報價代碼],[商品名稱],[最後交易日]` |

- 備註：全部資料回傳完畢時，會回傳一筆以「##」開頭的內容表示查詢結束。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:133`、`SKCOMTester/SKOOQuote.cs:76`（handler 定義 :296）

### OnNotifyQuoteLONG

- 用途：(LONG index) 已訂閱的海選商品報價異動通知。
- 簽名（handler）：`void OnNotifyQuoteLONG(int nIndex);`（IDL：`void OnNotifyQuoteLONG([in] LONG nIndex);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | LONG | 系統所編的海選商品索引代碼 |

- 備註：事件觸發後，以 nIndex 帶入 SKOOQuoteLib_GetStockByIndexLONG 取得商品報價物件。須以 SKOOQuoteLib_EnterMonitorLONG 登入方會觸發。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:144`、`SKCOMTester/SKOOQuote.cs:77`（handler 定義 :302）

### OnNotifyTicksLONG

- 用途：(LONG index) 已訂閱商品的即時成交明細通知。
- 簽名（handler）：`void OnNotifyTicksLONG(int nIndex, int nPtr, int nDate, int nTime, int nClose, int nQty);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | LONG | 系統所編的海選商品索引代碼 |
| nPtr | LONG | 資料位址（Key） |
| nDate | LONG | 交易日期（YYYYMMDD） |
| nTime | LONG | 時間 |
| nClose | LONG | 成交價（未處理小數） |
| nQty | LONG | 成交量 |

- 備註：價格未進行小數處理，需依 SKFOREIGNLONG 的 sDecimal 自行換算。避免在本事件（及 OnNotifyHistoryTicksLONG）內呼叫 SKOOQuoteLib_GetTickLONG。須以 EnterMonitorLONG 登入方會觸發。由 RequestTicks 或 RequestLiveTick 觸發。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:190`、`SKCOMTester/SKOOQuote.cs:78`（handler 定義 :389）

### OnNotifyHistoryTicksLONG

- 用途：(LONG index) 首次以 RequestTicks 訂閱時回補當天 Tick。
- 簽名（handler）：`void OnNotifyHistoryTicksLONG(int nIndex, int nPtr, int nDate, int nTime, int nClose, int nQty);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nIndex | LONG | 系統所編的海選商品索引代碼 |
| nPtr | LONG | 資料位址（Key） |
| nDate | LONG | 交易日期（YYYYMMDD） |
| nTime | LONG | 時間 |
| nClose | LONG | 成交價（未處理小數） |
| nQty | LONG | 成交量 |

- 備註：同 OnNotifyTicksLONG：價格未處理小數；避免在事件內呼叫 GetTickLONG；須以 EnterMonitorLONG 登入。RequestLiveTick 不會觸發本事件（無回補）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:79`（handler 定義 :398）；V2 OOQuoteForm.cs:213 為註解示範

### OnNotifyBest5LONG

- 用途：(LONG index) 已訂閱商品最佳五檔異動通知，直接回傳五檔價量。
- 簽名（handler）：`void OnNotifyBest5LONG(int nStockidx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5);`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStockidx | LONG | 系統所編的商品索引代碼 |
| nBestBid1~nBestBid5 | LONG | 第一～五檔買價（未處理小數） |
| nBestBidQty1~nBestBidQty5 | LONG | 第一～五檔買量 |
| nBestAsk1~nBestAsk5 | LONG | 第一～五檔賣價（未處理小數） |
| nBestAskQty1~nBestAskQty5 | LONG | 第一～五檔賣量 |

- 備註：價格未處理小數（依 SKFOREIGNLONG 的 sDecimal 與 nDenominator 換算）。屬 RequestTicks / RequestMarketDepth 的通知事件；須以 EnterMonitorLONG 登入。亦可於事件內以 nStockidx 呼叫 GetBest5LONG 取整包 SKBEST5。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:229`、`SKCOMTester/SKOOQuote.cs:80`（handler 定義 :407）

### OnNotifyBest10LONG

- 用途：(LONG index) 已訂閱商品最佳十檔異動通知，直接回傳十檔價量。
- 簽名（handler）：`void OnNotifyBest10LONG(int nStockidx, int nBestBid1, int nBestBidQty1, ..., int nBestBid10, int nBestBidQty10, int nBestAsk1, int nBestAskQty1, ..., int nBestAsk10, int nBestAskQty10);`（共 41 個參數：索引 + 買價/買量×10 + 賣價/賣量×10）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStockidx | LONG | 系統所編的商品索引代碼 |
| nBestBid1~nBestBid10 | LONG | 第一～十檔買價（未處理小數） |
| nBestBidQty1~nBestBidQty10 | LONG | 第一～十檔買量 |
| nBestAsk1~nBestAsk10 | LONG | 第一～十檔賣價（未處理小數） |
| nBestAskQty1~nBestAskQty10 | LONG | 第一～十檔賣量 |

- 備註：價格未處理小數（依 sDecimal 與 nDenominator 換算）。屬 RequestTicks / RequestMarketDepth 的通知事件；須以 EnterMonitorLONG 登入。無對應的 GetBest10 函式，十檔資料只能由本事件接收。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/OOQuoteForm.cs:276`、`SKCOMTester/SKOOQuote.cs:81`（handler 定義 :501）

### OnNotifyQuote（已移除）

- 用途：舊版（SHORT index）報價異動通知。
- 簽名：`void OnNotifyQuote(short sIndex);`（簽名待確認；V2.13.46 起移除）
- 備註：以 EnterMonitorLONG 登入時**不會被觸發**；由 OnNotifyQuoteLONG 取代。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:83`（註解掉的舊版掛載示範）

### OnNotifyTicks（已移除）

- 用途：舊版（SHORT index）即時成交明細通知。
- 簽名：`void OnNotifyTicks(short sIndex, int nPtr, int nDate, int nTime, int nClose, int nQty);`（簽名待確認）
- 備註：以 EnterMonitorLONG 登入時不會被觸發；由 OnNotifyTicksLONG 取代。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:84`（註解掉的舊版掛載示範）

### OnNotifyHistoryTicks（已移除）

- 用途：舊版（SHORT index）當日 Tick 回補通知。
- 簽名：`void OnNotifyHistoryTicks(short sIndex, int nPtr, int nDate, int nTime, int nClose, int nQty);`（簽名待確認）
- 備註：以 EnterMonitorLONG 登入時不會被觸發；由 OnNotifyHistoryTicksLONG 取代。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:85`（註解掉的舊版掛載示範）

### OnNotifyBest5（已移除）

- 用途：舊版（SHORT index）最佳五檔異動通知。
- 簽名：`void OnNotifyBest5(short sStockidx, ...20 個五檔價量參數);`（簽名待確認）
- 備註：以 EnterMonitorLONG 登入時不會被觸發；由 OnNotifyBest5LONG 取代。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:86`（註解掉的舊版掛載示範）

### OnNotifyBest10（已移除）

- 用途：舊版（SHORT index）最佳十檔異動通知。
- 簽名：`void OnNotifyBest10(short sStockidx, ...40 個十檔價量參數);`（簽名待確認）
- 備註：以 EnterMonitorLONG 登入時不會被觸發；由 OnNotifyBest10LONG 取代。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:87`（註解掉的舊版掛載示範）

## 僅見於範例碼

- **SKOOQuoteLib_RequestTiks**：出現在 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOOQuote.cs:198` 的 `SendReturnMessage("SKOOQuoteLib_RequestTiks")`。這只是 log 訊息字串的**拼字錯誤**（Ticks → Tiks），實際呼叫的是同函式上方 :194 的 `SKOOQuoteLib_RequestTicks`；SKCOM 元件內**沒有** RequestTiks 這個方法，勿依此命名呼叫。V2.13.59 補註：該檔已隨海選報價示範一併下架（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKCOMTester.csproj:170` 的 Compile 項只剩 `SKQuote.cs`），此拼字錯誤僅存在於 `.57` 範例樹。

## 陷阱與注意

1. **LONG index 是唯一現行版本**：V2.13.31 因單一市場商品總數可能超過 SHORT 上限（32767）新增 LONG 系列；V2.13.46（含）以上已移除舊版非 LONG 函式與事件。新開發一律使用 EnterMonitorLONG＋`*LONG` 系列。**V2.13.59 訂正**：官方 .59 已把 2.13.46 的「移除函式如下」縮為只剩「國內報價 SKQuoteLib」，不再包含海選報價；海選的非 LONG 成員在 .57／.59 的 Interop.SKCOMLib 中都仍有型別宣告（僅無官方文件），但宣告仍在不等於功能仍提供，實務結論不變——一律不要使用，詳見「總覽：功能分區表」下方的「V2.13.59 訂正」。
2. **EnterMonitor 與 EnterMonitorLONG 擇一**：用 EnterMonitorLONG 登入後只會觸發 LONG 系列事件，OnNotifyQuote/OnNotifyTicks/OnNotifyHistoryTicks/OnNotifyBest5/OnNotifyBest10（非 LONG）不會被觸發。
3. **OnConnect 內不要做事**：避免在 OnConnect 事件內直接呼叫 EnterMonitor(LONG)/LeaveMonitor/RequestStocks/RequestTicks——各交易所商品未下載完成時會訂閱失敗、取不到商品基本資料。
4. **價格皆為未處理小數的 LONG**：商品物件、Tick、五檔、十檔的價格欄一律未除小數，需依 SKFOREIGNLONG 的 `sDecimal`（報價小數位數）與 `nDenominator`（分母）自行換算（官方 C# 範例以 `/100.0` 僅為示意）。海選（SKOOQuoteLib）**沒有** SKOSQuoteLib 的 NineDigit（CME 九位小數）系列。
5. **事件內禁呼叫 GetTickLONG**：官方明載避免在 OnNotifyHistoryTicksLONG / OnNotifyTicksLONG 事件裡呼叫 SKOOQuoteLib_GetTickLONG（範例程式雖如此示範，實務請改於事件外處理，否則可能阻塞 COM 事件執行緒）。
6. **三種 Tick/深度訂閱擇一**：RequestTicks（成交明細＋五檔＋十檔＋當日回補）、RequestLiveTick（僅即時成交明細，無回補；GetTickLONG 只能查訂閱後資料）、RequestMarketDepth（僅五檔＋十檔）。RequestTicks 與 RequestLiveTick 請擇一使用。
7. **Page 規則**：RequestStocks 的 psPageNo 固定帶 1、可一次帶多檔（`#` 分隔，格式 `交易所代碼,商品報價代碼`）；RequestTicks / RequestLiveTick / RequestMarketDepth 的 psPageNo 從 1 開始、一個 Page 僅能索取一檔。
8. **psPageNo 的 Interop 差異**：IDL 為 `[in,out] SHORT*`；舊版 SKCOMTester Interop 產生 `ref short`（`SKOOQuote.cs:148,194,271,960`），SKCOMTesterV2 的 Interop 則以傳值 `short` 呼叫（`OOQuoteForm.cs:486,503,524`）。依你專案引用的 Interop.SKCOMLib.dll 實際簽名為準。
9. **前置條件鏈**：需先簽署期貨API下單聲明書 → SKCenterLib_Login → EnterMonitorLONG，Get*/Request* 函式與 *LONG 事件皆以 EnterMonitorLONG 登入為前提；未連線先呼叫會得錯誤碼 2026（SK_WARNING_OOQUOTE_MUST_SKOOQUOTELIB_ENTERMONITORLONG_FIRST）。
10. **與下單的關聯**：具海期帳號者，海選委託（SendOverseaOptionOrder 系列）下單前須先備妥海選商品檔，可先做海選連線下載；出現錯誤代碼 2015 請重連海期行情主機或重新下載（見 **V2.13.57 版**分冊 `9.下單-海外期選.md`）。V2.13.59 文件變更：該段備註已被精簡成只剩「具海期帳號，海選委託下單前須先下載」，2015 的排除方法與「可先做海選連線備妥商品檔」一句在 .59 已刪（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:204`、`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:840`），排查時請沿用本條所留的 .57 原文。
11. **IsConnected 回傳值特殊**：1 才是連線中（其他函式是 0＝成功）。
12. **SKBEST5 衍生檔欄位**：nExtendBid/nExtendAsk 等衍生一檔欄位海外商品無效；nSimulate＝1 表示試算（試撮）揭示。
13. **別與 DLL 版混淆（V2.13.59 已下架簡化版海選報價）**：SKDLLTester（`C_Sharp策略王DLL元件使用說明.md`）的同名函式簽名不同（如 `int SKOOQuoteLib_RequestTicks(int nItemNo, string strStockNo)`、`SKFOREIGN_9LONG2 SKOOQuoteLib_GetStockByNoLONG(string)`、事件 `OnNotifyOOQuoteLONG`），那是另一套 DLL 包裝介面，本檔僅涵蓋 SKCOM COM 元件。**自 V2.13.59 起，簡化版 SKDLLCSharp 包裝層的 `SKOOQuoteLib_RequestStocks`／`SKOOQuoteLib_RequestTicks`／`SKOOQuoteLib_GetStockByNoLONG` 三個方法已被移除**：對兩版 SKDLLCSharp.dll(x64 Release) 取識別字比對，三者在 .57 各出現 1 次、在 .59 為 0 次；`add_OnNotifyOOQuoteLONG`／`add_OnNotifyOOBest10`／`add_OnNotifyOOTicks`（與對應的 `remove_*`、`*Callback`）符號兩版都在，但失去配套的訂閱與取值方法，等於掛得上事件卻沒東西可訂閱。官方 V2.13.59 的 `C_Sharp策略王DLL元件使用說明` 亦已整段刪除「海期行情」「海選行情」兩節（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md` 全檔 0 次提及 SKOOQuoteLib），`.59` 的 SKDLLTester 範例對 SKOOQuoteLib_／OnNotifyOO* 的引用數為 0（`.57` 的 `SKDLLTester/SKDLLTester/Form1.cs` 為 9 處：3 個事件訂閱、3 次方法呼叫、3 處 log 字串）。**COM 版與原生 SKCOM.dll flat export 不受影響——海選報價請直接呼叫本檔所述的 COM 介面。**
14. **簡化版 ManageServerConnection 已無海選連線目標（V2.13.59 起）**：簡化版包裝層的 `ManageServerConnection` 說明已由「與(回報/國內行情/海期行情/海選行情/下單)主機建立連線」改為「與(回報/國內行情/下單)主機建立連線」，`nTargetType` 由「0:回報；1:國內行情；2:海期行情；3:海選行情；4:Proxy下單」縮為「0:回報；1:國內行情；4:Proxy下單」，`nStatus` 也少了「4:連線(備援(僅海期選))」（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:49-50`；範例下拉選單同步只剩三項，`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.Designer.cs:4470-4473`）。舊程式若在包裝層以 `nTargetType=3` 建立海選行情連線，在 .59 已無對應目標；直接使用 COM 的 `SKOOQuoteLib_EnterMonitorLONG` 不受影響。
15. **官方 V2.13.59 已全面下架海選報價的文件與範例（但 API 還在）**：合訂本 4-6 整章、`15.海選報價` 分冊、SKFOREIGNLONG／SKFOREIGNTICK 等結構章節、導覽的物件介紹與架構圖條目，以及三個範例專案（SKCOMTester、SKCOMTesterV2、SKDLLTester）的海選報價／海選行情頁籤與表單，在 .59 全部消失（`.59` 樹中 `海選` 字樣只剩下單與回報相關，如 `Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.Designer.cs:896` 的「下載海選商品檔」按鈕、`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.Designer.cs:1762` 的「海選」回報頁籤）（詳見檔頭「V2.13.59 文件狀態」）。Interop 符號 37 個兩版逐行相同、原生匯出三支不增不減，`.59` 的海選下單表單也還在呼叫 `SKOOQuoteLib_EnterMonitorLONG`（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SendOrderForm/OFSendOrderForm.cs:326`、`Order/StrategyOrderForm/OFStrategyOrderForm.cs:354`、`Order/SKProxyOrder/SKProxySendOrderForm/OFSKProxySendOrderForm.cs:294`）。實務含意：**本檔（＝V2.13.57 留存原文）是目前唯一可查的海選報價規格來源**；官方既已停止維護此章，遇到與文件不符的行為請以實測為準，並先向群益確認該功能是否仍受支援，再投入正式環境。
16. **V2.13.59 已刪除行情連線數限制與 SHORT 32767 對照表**：官方 .59 合訂本移除了 3-3「行情物件連線限制說明」（含「國內證券與國內期貨共用一條、海外期選單獨一條」的總連線數規則）、3-3-2 國內行情相關說明與 3-4「行情功能修改說明」（含 SKQuoteLib／SKOSQuoteLib／SKOOQuoteLib 新舊函式對應一覽表）——`.59` 主手冊第 3 章只剩 3-1 物件架構（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:127`）與 3-2 註冊公告（`:135`），全檔已查無「行情物件連線限制」「新舊功能對應一覽表」等字樣；而 2.13.31 的版本歷程卻仍寫著「詳情請參考 3-4 行情功能修改說明」（`:11`），成為指向已刪章節的失效交叉引用。本檔第 1 條所述的 LONG／SHORT 上限背景，以及「海外期選單獨佔用一條報價連線」這類排查「訂閱不到行情」的關鍵說明，在 .59 官方文件已無出處，僅能回頭查 V2.13.57 版文件。
