# CapitalAPI 2.13.57 → 2.13.59 差異分析

> 產生日期：2026-09-07　｜　比對對象：`Source_code/CapitalAPI_2.13.57_CExample` vs `Source_code/CapitalAPI_2.13.59_CExample`（官方範例包：C#/C++ 範例、`元件/` 內 DLL、docx 手冊）
> 方法：多代理工作流（13 個讀取代理 → 合併 54 筆 → 每筆 3 視角對抗驗證 → 分區裁決 → 總裁決 → 完整性稽核），共 189 個代理；證據層次：DLL 符號／PE 匯出表 ＞ 官方手冊版本歷程 ＞ 範例原始碼 ＞ 分冊文件。
> 本檔非官方文件；官方 changelog 原文見 `_raw/策略王COM元件使用說明_V2.13.57.md`（.57）與 .59 手冊版本歷程表。

## 0. 結論

**整體判定：混合：整體優化。**

2.13.57 → 2.13.59 在 COM／Interop 表面是純加法（Interop 39 加 0 減、原生匯出 77→82 零移除），新增即時分K、未平倉 JSON、權益數查詢狀態與海選 OLID 等能力並修掉十餘項缺陷，對直接使用 COM 的開發者是明確優化；退步集中在 COM 之外的三處——簡化版 SKDLLCSharp 破壞性移除海期／海選報價、官方文件與範例大規模下架、以及數處回報／查詢字串格式改變需跟進。

2.13.57 → 2.13.59 在 COM API 表面是純加法：Interop.SKCOMLib 新增 39 個符號、零移除；原生 SKCOM.dll 具名匯出以 objdump 匯出表複驗由 77 增為 82（新增 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights／GetOFOpenInterestGW／GetOFFutureRights，零移除）。最重要的五項變更：(1) C-01 新增即時分K 整組 API（SKQuoteLib_GetLiveKLineLONG＋OnNotifyLiveKLineData＋SKKLINE），由既有 RequestTicks 訂閱觸發；(2) C-13 內外期未平倉「查無資料」由 M003 NO DATA# 改為「001,查無資料,帳號」，舊字串比對會失效並可能解析出假部位，是唯一的 must_change；(3) C-31 新增 9996 SK_ERROR_UPDATE_API_REQUIRED，舊版元件將被停用、重試無效，使升級從選項變成必然；(4) C-34 簡化版 SKDLLCSharp 砍掉 6 個海期／海選報價方法，該路線編譯即失敗（COM／Interop 完好）；(5) C-21 官方把下單回傳 0 從「委託成功」改述為「已送至交易所，結果請由回報確認」。另有 C-12 OnOpenInterestJson、C-15 權益數查詢狀態事件、C-29 SendOverseaOptionOrderOLID、C-03 EnterMonitorLONGByMarket 四項可選新能力與約 12 項行為修正。退步集中在三處且都不在 COM 介面：簡化包裝層破壞性移除、官方文件大規模下架（海期海選章節、3-3 連線限制、3-4 SHORT 32767 對照表）、範例包減法與新示範品質瑕疵（C-44 分K 寫死 /100、C-45 null 未防護）。結論：對直接使用 COM／Interop 者是明確優化，但須跟進數處回報／查詢字串格式變動。

一句話版：**直接呼叫 COM／Interop 的程式，換上 .59 一行不改也能跑，並多出即時分K、未平倉 JSON、權益數查詢狀態、海選 OLID 等能力；但有 4 處回報／查詢字串格式變動要跟進，而簡化版 `SKDLLCSharp.dll` 使用者若用到海期／海選報價，換版即編譯失敗。**

## 1. 硬證據：二進位層

| 項目 | 2.13.57 | 2.13.59 | 差異 |
|---|---|---|---|
| `元件/x64/Interop.SKCOMLib.dll` 符號集合 | 1,252 | 1,291 | **+39 / −0**（IID、組件版本未變，drop-in 可替換） |
| `元件/x64/SKCOM.dll` PE 匯出表（objdump） | 78 | 83 | **+5 / −0**：`GetRealBalanceReport`、`GetOpenInterestGW`、`GetFutureRights`、`GetOFOpenInterestGW`、`GetOFFutureRights` |
| `SKDLLTester/.../SKDLLCSharp.dll`（簡化版包裝） | 45,568 B | 70,144 B | **−6 方法**（海期/海選報價）**+5 方法**（帳務/未平倉查詢）＋ 5 個 `*ParserResult` 型別 |
| 官方 docx 手冊 | 21 份 | 19 份 | `14.海期報價`、`15.海選報價` 消失；主手冊刪 4-5／4-6 兩章與 3-3／3-4 節 |
| 範例 `.cs` 檔數 | 114 | 96 | .59 移除 `SKOSQuote`/`SKOOQuote`（SKCOMTester）、`OSQuoteForm`/`OOQuoteForm`（V2）、`SKOSQuote.cpp/.h`（CppCLI） |

Interop 新增的 39 個符號 100% 歸入 7 個功能群：

| 功能群 | 符號 | 官方 changelog | 手冊 | 範例 |
|---|---|---|---|---|
| 即時分K | `SKQuoteLib_GetLiveKLineLONG`、`OnNotifyLiveKLineData`、`struct SKKLINE`（10 符號） | 2.13.59 | 4-4-35 / 4-4-u / 5-26 | SKCOMTester/SKQuote.cs |
| 指定市場連線 | `SKQuoteLib_EnterMonitorLONGByMarket(nMarketType)` | 2.13.59 | 4-4-36 | 有 |
| 匯出商品清單 | `SKQuoteLib_ExportStockList` | **未公開** | 無 | 無 |
| 未平倉 JSON | `OnOpenInterestJson`（5 符號） | 2.13.58 | 有 | 有（手刻字串切割） |
| 權益數查詢狀態 | `OnFutureRightsStatus`、`OnOverseaFutureRightsStatus`（10 符號） | **未列** | 4-2-v / 4-2-w | 有 |
| 未平倉明細 | `GetOFOpenInterestWithDetails`、`OnOpenInterestWithDetails`、`OnOverseaFutureOpenInterestWithDetails`（11 符號） | **未公開** | 無 | 無 |
| 海選 OLID 下單 | `SendOverseaOptionOrderOLID` | 2.13.58 | 4-2-116 | 有 |

新錯誤碼：**9996 `SK_ERROR_UPDATE_API_REQUIRED`「此版本已無法登入，請更新版本」**（2.13.59；終止性，重試無效）。

### 1.1 稽核修正（請先讀）

- 本分析最初以 `strings` 掃 `SKCOM.dll` 做「原生匯出」比對，產生了兩個假象：`SKQuoteLib_DeltaT→Delta` 改名（C-38，驗證階段以 2/3 票淘汰）與 8 個 `SKOOQuoteLib_*WWW` 新匯出（C-39）。以 `objdump -p` 直接讀 PE Export Directory 複驗：兩版匯出表均無任何 `WWW`/`Delta` 符號，`SKOOQuoteLib_*` 兩版都恰為同樣 3 支。**C-39 應視為撤銷**，原生匯出的真實變化只有上表的 +5／−0。
- C-34 的範圍被合併階段放大：`SKDLLCSharp.dll` 實際移除的是 6 支方法（`SKOSQuoteLib_RequestStocks/RequestTicks/GetStockByNoNineDigitLONG`、`SKOOQuoteLib_RequestStocks/RequestTicks/GetStockByNoLONG`）加 `pSKForeign_9LONG`/`Format` 兩個識別字；`OnNotifyOS*/OO*` 24 個事件識別字兩版都在，形成「事件還在、無法訂閱」的死碼。
- 裁決文中「原生匯出 77→82」與本表 78→83 是計數方式差異（是否含序數 0 項），+5／−0 的結論一致。
- 符號比對只看名稱集合，未驗證既有方法的參數型別／vtable 位置；C-36「簡化版下單回傳值修正」未在二進位層反編譯確認。
- `OnNotifyLiveKLineData` 的 `nType=0` 為「清盤通知，本筆價格全 0，須捨棄已收資料」，自建分K 累積器必須處理，官方範例未示範。
- 本 repo 的 .57 資料夾是「補入海期／海選報價」的完整包（commit `01eab54`），.59 則為官方原始下載包；14/15 分冊與相關範例究竟在 .58 或 .59 下架、或本就不在標準包內，官方 changelog 未載。

## 2. 官方版本歷程（.59 手冊新增的兩列）

**2026/03/09 2.13.58** — 功能異動：新增 `OnOpenInterestJson`（內期未平倉 JSON 一次回傳）；`GetOpenInterest`/`GetOpenInterestGW`/`GetOpenInterestWithFormat`/`GetOverSeaFutureOpenInterest`「查無庫存」新增 Account 欄位。功能修正：`SendStockStrategyCB` 1023 `SK_ERROR_MARKET_OUT_OF_RANGE`；國內期貨未平倉 Parse 失敗例外處理；訂閱國內行情價差商品；訂閱國內報價慢；SGX 專線主動回報缺漏；只有全盤商品的 T 盤需加 AM；`GetStockByNoLONG` 取物件。功能新增：`SendOverseaOptionOrderOLID`。文件調整：下單函式回傳 0 ＝「成功送至交易所，結果請由回報確認」。

**2026/08/03 2.13.59** — 功能新增：國內期選主動回報 `OnNewData` 新欄位「下單時間 HH:mm:ss.fff」；自營帳號 `GetRealBalanceReport` 新增「昨日庫存」；即時分K `SKQuoteLib_GetLiveKLineLONG`；`GetOpenInterestGW` 新欄位「商品－下單代碼」；`SKQuoteLib_EnterMonitorLONGByMarket`；錯誤碼 9996。功能修正：國內報價第一筆 Ticks 缺失；內期夜盤收盤價未更新；複委託回報無時效欄位時給空值；主動回報多帳號無法斷線；智慧單被動回報缺逗號；index 不一致；簡化版 API 下單成功回傳值。


## 3. 分區裁決

| 領域 | 判定 | 變更 |
|---|---|---|
| quote（國內報價 SKQuoteLib） | **優化** | C-01, C-02, C-03, C-04, C-05, C-06, C-07, C-08, C-09, C-10, C-11 |
| order（下單／帳務查詢 SKOrderLib） | **混合：整體優化** | C-12, C-13, C-14, C-15, C-16, C-17, C-18, C-19, C-20, C-21, C-22 |
| reply（回報 SKReplyLib） | **混合：整體優化** | C-23, C-24, C-25, C-26, C-27, C-28 |
| oversea（海外期選下單） | **優化** | C-29, C-30 |
| center_login（登入／同意書 SKCenterLib） | **優化** | C-31, C-32 |
| dll_wrapper（Interop 與 SKDLLCSharp 簡化包裝層） | **混合：整體退步** | C-33, C-34, C-35, C-36, C-37, C-39, C-40, C-41 |
| sample_package（官方範例包） | **混合：整體退步** | C-42, C-43, C-44, C-45, C-46, C-47 |
| doc（官方文件） | **混合：整體退步** | C-48, C-49, C-50, C-51, C-52, C-53 |
| other（環境設置／註冊） | **優化** | C-54 |

### quote（國內報價 SKQuoteLib） — 優化

淨新增、零移除。新增「即時分K」整組 API（SKQuoteLib_GetLiveKLineLONG＋OnNotifyLiveKLineData＋SKKLINE 結構）與可指定市場的 SKQuoteLib_EnterMonitorLONGByMarket，皆具 DLL 符號＋官方手冊＋範例三重佐證；既有符號與簽章全數保留，Interop vtable 為 append-only（ISKQuoteLib 40→43，新方法排在 slot 41-43），舊程式零改動可續用。另累計 7 項行為修正（價差商品訂閱、報價慢、T盤加 AM 範圍、GetStockByNoLONG 取物件、第一筆 Ticks 缺失、內期夜盤收盤價、index 不一致），方向皆為修補缺陷。退步屬次要：RequestTicks 訂閱被單方面擴充為含即時分K 且查無取消函式；手冊 4-4-35 誤植不存在的 RequestLiveKLine；ExportStockList 有符號無文件。

### order（下單／帳務查詢 SKOrderLib） — 混合：整體優化

主軸是「補齊查詢的成敗／批次管道」與「把文件語意講精確」。新增 OnOpenInterestJson（未平倉一次 JSON 回傳）、OnFutureRightsStatus／OnOverseaFutureRightsStatus（權益數查詢成敗），皆為並存式新增、DLL 零符號移除、事件掛在 typelib 尾端，不升即無感。文件面把「下單回傳 0＝委託成功」降級為「已送達交易所，結果請由回報確認」，橫跨主手冊 81 處與 8 份分冊，是把長年誤解講白。退步在相容性細節：未平倉查無資料格式改變會讓舊字串比對直接失效；GetOpenInterestGW 新增欄位但插入位置在文件中是圖片無法確認；*GWStatus 觸發來源擴大會讓狀態旗標誤判；主手冊與分冊對海期側、查無資料欄數彼此矛盾；另有 11 個 *WithDetails 符號完全無文件。功能進步，跟進成本不低。

### reply（回報 SKReplyLib） — 混合：整體優化

全部是 DLL 內部行為與資料內容變動，COM 介面零變更：SKReplyLib_*、OnNewData、OnStrategyData 與三支智慧單被動回報事件的符號兩版完全相同，.57 程式不改一行即可掛 .59。實質是四項修正（複委託時效欄位空值化、多帳號回報連線無法斷線、智慧單被動回報缺逗號、SGX 專線主動回報缺漏）加一項欄位新增（國內期選 OnNewData 多出「下單時間 HH:mm:ss.fff」），方向都是把錯的或缺的補回來。退步只有兩處且不在 API 面：官方只在版本控管表寫一行，欄位表與 ReplyForm.cs 範例（仍 values[47] SeqNo、無長度防護）完全沒更新，新欄位與補上的逗號都沒有權威欄位序可查；以及 SKCOMTester/SKReply.cs 的 OnComplete／OnNewData 被拿掉雙帳號分流，雙帳號回報範例失效。

### oversea（海外期選下單） — 優化

只有兩件事。一是新增 SendOverseaOptionOrderOLID（sym59 獨有、主手冊 4-2-116、範例加 SendOOOrderAsyncOLID 按鈕與自訂欄位輸入框），補齊 OLID 家族最後缺口，海選非同步委託終於能帶客戶自訂資料並由 OnAsyncOrderOLID 對回；OVERSEAFUTUREORDER 結構未動、SendOverseaOptionOrder 原簽名保留，舊碼零改動。二是分冊 9 的 LoadOSCommodity／LoadOOCommodity 備註精簡，失去「先 EnterMonitorLONG 備妥商品檔」與「2015 請重連海期行情主機或重新下載」兩條排查提示，但 2015 定義與 OSQuote.log 檢查說明仍留在 .59 其他文件，屬文件精簡而非行為變更。全域無函式被移除或改簽名。

### center_login（登入／同意書 SKCenterLib） — 優化

兩項變更均不動 COM 介面：四份符號清單中 SKCenterLib_Login、SKCenterLib_RequestAgreement、OnShowAgreement 完全零差異。實質新增只有錯誤碼 9996 SK_ERROR_UPDATE_API_REQUIRED「此版本已無法登入，請更新版本」，見於 .59 changelog、主手冊代碼表與 2.導覽.md，.57 全部原文皆無；同批把 9996/9997/9998 補進 2.導覽原本由 4001 直跳 9999 的表格。另一項是 3.登入.md 刪去「海外行情同意書一定查詢」特例，但同包主手冊仍保留原句，屬文件不一致的文字修剪。判為優化：版本淘汰情境從模糊失敗變成可辨識代碼；代價是需自行補終止性分支，官方範例與 3.登入.md 都仍只處理到 9997。

### dll_wrapper（Interop 與 SKDLLCSharp 簡化包裝層） — 混合：整體退步

一加一減。加：Interop.SKCOMLib 純新增 39 符號、0 移除（本次以 comm 複驗確認），IID 與組件版本未變，.57 程式免改免重編；原生 SKCOM.dll 匯出表以 objdump 實測由 77 增為 82（純增 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights／GetOFOpenInterestGW／GetOFFutureRights，0 移除），SKDLLCSharp 同步補上這五個帳務／未平倉查詢包裝，並修正簡化版下單成功回傳值。減：同一顆 SKDLLCSharp.dll 把海期／海選報價整組拿掉，六個方法在 .59 完全消失（實測 .57 各命中 1、.59 全為 0），OnNotifyOS/OO* 事件符號卻殘留成孤兒；ManageServerConnection 的 nTargetType 2／3 也自手冊刪除，官方 changelog 隻字未提。COM／Interop 層完好無損，退步僅限簡化包裝層，但那是無同層替代品的破壞性移除，加上新方法文件宣告型別寫錯，故整體判退步。

### sample_package（官方範例包） — 混合：整體退步

.59 範例包主軸是減法：四個範例專案（SKCOMTester、SKCOMTesterV2、CppCLITester、SKDLLTester）連同 ExcelSample.xls 與隨包手冊 14／15 章，把海期／海選報價示範整批下架（本次已複驗 .59 SKCOMTester 目錄中 SKOSQuote.cs／SKOOQuote.cs 三件套確實消失），官方版本歷程未公告。COM 介面完好——Interop 符號兩版一致、.59 下單範例仍呼叫 SKOSQuoteLib_EnterMonitorLONG，既有程式照跑；但 SKDLLCSharp.dll 的 C# DLL 路線經實測已少掉 6 個海期／海選方法，屬實質破壞。加分只有即時分K 與 OnOpenInterestJson 兩支新示範，品質卻不佳：前者價格寫死 /100 與同檔 sDecimal 慣例矛盾，後者手刻字串切割且 null 檢查排在 Contains 之後。再加上 MainForm 殘留兩顆空白按鈕、vcxproj 的 Interop HintPath 懸空。

### doc（官方文件） — 混合：整體退步

文件面以「大規模刪除」為主而非補強：主手冊移除 4-5 SKOSQuoteLib、4-6 SKOOQuoteLib 兩整章（4-4 直接跳 4-7）與 5-8/5-12/5-22/5-23 四個海外結構（造成 5-x 編號整批位移），14.海期報價／15.海選報價 兩份分冊在 .59 目錄中不存在，3-3 行情連線限制與 3-4「SHORT 32767 新舊對照表」整段刪除，連舊版 changelog 的海期／海選字樣也被回溯抹去且清洗不徹底。但這是文件消失而非 API 消失：sym57/sym59 對 SKOSQuoteLib 56:56、SKOOQuoteLib 37:37 完全相同，錯誤碼 2015/2025/2026 與 3030 仍在，執行期規則未變。新增章節又引入不存在的 RequestLiveKLine，並殘留指向已刪 3-4 節與 5-9/5-24 的死連結。程式碼不必改，但官方可查性顯著下降。正向僅有版本歷程新增 2.13.58／2.13.59 兩列 changelog（本次最權威的差異來源）。

### other（環境設置／註冊） — 優化

只有一項純文件變更：《1.環境設置》把 regsvr32 註冊說明由「x86位元:直接註冊即可／x64位元:透過SysWow64的regsvr32.exe註冊」改寫為「x32位元: 透過SysWow64／x64位元: 透過System32 或直接註冊」。COM 符號、原生匯出與範例程式皆無對應變更；兩版 元件/x86、x64 的 install.bat 內容完全相同，且其行為早在 .57 就等同 .59 新敘述，可判定新文字是向既有安裝腳本對齊、修掉舊措辭的誤導。扣分處是同一套 .59 合輯手冊附錄 A 仍保留舊字串，只讀合輯手冊者仍會拿到錯誤指引；整體仍屬小幅優化。

## 4. 需跟進的相容性風險（依嚴重度）

影響等級：破壞性 ＝ 既有呼叫失效；必改 ＝ 不改會壞；建議跟進 ＝ 解析器/流程需容錯或複驗。

### C-13（必改／優化）

**問題：** 內／外期未平倉「查無資料」回傳格式改變：由 M003 NO DATA#（GW 版「001 查無資料」）改為逗號分隔的「001,查無資料,帳號」，影響 GetOpenInterest／GetOpenInterestGW／GetOpenInterestWithFormat／GetOverseaFutureOpenInterest 四支。舊字串比對完全偵測不到，3 欄字串會被丟進 10~40 欄的正常解析路徑，造成 IndexOutOfRange 或憑空產生一列假部位。本 repo api_spec/modules/SKOrderLib.md:1235、:2554 目前正是舊寫法。這是本次唯一明確的 must_change。

**因應：** 改為「split(',') 首欄 == "001" 或整串含『查無資料』」雙條件判斷，索引存取前先檢查欄位數；同時容忍主手冊的 3 欄與分冊 7 的 2 欄兩種官方不一致格式；海期 OnOverseaFutureOpenInterest 套用同一判斷。

### C-34（破壞性／退步）

**問題：** 簡化包裝層 SKDLLCSharp.dll 破壞性移除海期／海選報價六個方法（SKOSQuoteLib_RequestStocks／RequestTicks／GetStockByNoNineDigitLONG、SKOOQuoteLib_RequestStocks／RequestTicks／GetStockByNoLONG）。本次已實測複驗：.57 版 x64 Release SKDLLCSharp.dll（45568 bytes）六者字串各命中 1，.59 版（70144 bytes）全部為 0，同檔卻新增 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights。走此路線且用到海外報價者換 DLL 即編譯／繫結失敗，OnNotifyOS/OO* 事件符號殘留成無法訂閱的孤兒，官方 changelog 隻字未提。COM／Interop 與原生匯出完全不受影響。

**因應：** (a) 短期把海期／海選報價路徑凍結在 .57 版 SKDLLCSharp.dll（其他部分照常升級，因 Interop 與原生匯出零移除）；(b) 長期改直呼 COM 的 SKOSQuoteLib／SKOOQuoteLib（Interop 符號兩版逐行相同、能力完整），流程照 api_spec/flows/C-quotes.md；(c) 向群益確認是刻意棄用還是建置遺漏。

### C-31（建議跟進／優化）

**問題：** 新增終止性登入錯誤碼 9996 SK_ERROR_UPDATE_API_REQUIRED「此版本已無法登入，請更新版本」。後端一旦對舊版元件回 9996，重試永遠無效；只認得 9997／1129 的既有程式會落入泛用 else，含自動重試迴圈者將無限重試並撞上 2.13.57 起的「登入失敗五次須重啟 API」鎖定，使故障放大。官方 .59 範例 SKCOMTester/Form1.cs:166-179 只處理 511／321／1129／9997，未示範 9996。此碼也使升級從「可選」變成「遲早必須」。

**因應：** 在登入回傳處理新增獨立 9996 分支（排在泛用 else 之前、與 9997 分流），顯示「元件版本過舊請更新」、停止所有自動重試並關閉重連 Timer，標記為需人工介入；把 9996 加入不可重試白名單。根治手段是升級到 2.13.59 元件。

### C-14（建議跟進／優化）

**問題：** GetOpenInterestGW 未平倉回傳新增「商品－下單代碼」欄位，且該欄查詢異常時回空值需重呼叫。官方 md 中欄位序列是圖片、抽取後看不到，無法確認是附加末端或中間插入；若為中間插入，所有固定索引解析器整排錯位且不會拋例外。

**因應：** 改為長度容錯解析：勿用 values.Length 等值檢查、勿用「最後一個元素」定位 LOGIN_ID，改以具名對照表或穩定前綴欄位定位；升級後先在模擬環境呼叫並原樣印出 bstrData 確認欄位序列再上線；該欄為空時降級顯示並提供重呼叫路徑。

### C-26（建議跟進／優化）

**問題：** 修正智慧單被動回報缺少逗號問題（涉 OnTSSmartStrategyReport／OnStopLossReport／OnOFSmartStrategyReport）。補上逗號後分隔數改變、其後欄位整批位移且不拋例外（靜默錯位）；官方未指名單別、欄位位置，也未附慣用的修改前後比較表，而官方範例本身即大量硬編 values[48]~values[63]。錯位資料若用於下單或風控判讀風險高。

**因應：** 以 .59 逐單別實跑三支查詢，把欄位數與內容和 .57 基準逐欄 diff；解析改為欄位數檢查＋欄位名對映而非硬編索引；移除為舊版缺逗號寫的任何 workaround 後再重測；注意 2.13.48 曾把智慧單回報逗號由半形改全形，比對時一併確認分隔字元。

### C-23（建議跟進／不明）

**問題：** 國內期選主動回報 OnNewData 新增欄位「下單時間 HH:mm:ss.fff」，但主手冊 4-3-g、12.回報.md 欄位表與範例 ReplyForm.cs（仍 new string[48]、逐一取 values[0]~values[47]、無長度防護）兩版逐字相同，官方沒說新欄位插在第幾欄。以 Split(',').Length == 48/49 嚴格驗證、tuple-unpacking 解析或以固定欄位數重組回報字串者一律失效。

**因應：** 欄位數檢查由「恰好 N 欄」改為「至少 N 欄」，每次索引取值加 values.Length 邊界保護；在模擬環境下一筆國內期選（TF/TO）委託 dump 原始 bstrData 逐欄比對確認新欄位 index 後才啟用；確認前 values[0..47] 邏輯原樣保留。

### C-21（建議跟進／優化）

**問題：** 2.13.58 文件調整把下單／改單／刪單／Proxy 全家族的回傳語意明確降級（主手冊 81 處、分冊近百處）：回傳 0 只代表「已成功送至交易所」，非同步 0 只代表 Request 已送出。任何把同步回傳 0 當成交易成立、據以更新部位或停止追蹤的邏輯，語意上一直是錯的，此版只是講白。

**因應：** 審視所有以 nCode==0 作為終態的分支，一律改為「已受理」；最終狀態由 SKReplyLib 的 OnNewData 回報（或 OnAsyncOrder／OnProxyOrder 非同步結果）決定；為每筆委託建立 ThreadID／委託序號到回報的配對追蹤與逾時告警。

### C-09（建議跟進／優化）

**問題：** 2.13.59 修正「國內報價第一筆 Ticks 缺失」。凡在應用層做過補償者（刻意忽略首筆、自行補值、以 nPtr 連續性偵測缺漏回補、或以 GetTickLONG 從 nPtr=0 掃描）會多算或索引位移；且同版新增 9996 使 .57 最終無法登入，無法以不升級迴避。

**因應：** 盤前逐一檢視 tick 消費路徑，移除首筆特判／去重／補值 workaround，改以 nPtr 為唯一去重鍵；同商品同交易日在 .57 與 .59 各錄一份原始 tick 流逐筆比對，確認總筆數、首筆 nPtr 與成交量統計一致後再上線。

### C-35（破壞性／退步）

**問題：** 簡化版 ManageServerConnection 的 nTargetType 由 0/1/2/3/4 縮為 0:回報／1:國內行情／4:Proxy下單，2:海期行情、3:海選行情自手冊下架，且即使連上也已無配套報價方法（見 C-34）；nStatus=4「備援(僅海期選)」手冊刪除但範例 comboBoxStatus 仍保留該選項，互相矛盾。

**因應：** 檢查所有 ManageServerConnection 呼叫點，nTargetType=2/3 改走 COM 的 SKOSQuoteLib_EnterMonitorLONG／SKOOQuoteLib_EnterMonitorLONG；0／1／4 語意未重編號可維持不變；nStatus=4 在真機驗證前不要使用。

### C-36（建議跟進／優化）

**問題：** 2.13.59 changelog 記載「修正簡化版API下單成功回傳值」，但 SKDLLCSharp 下單函式符號集合兩版一致、手冊回傳值定義未改，屬實作內部行為修正。若既有程式針對舊版的錯誤回傳值寫死判斷，升級後成功／失敗判定可能反向或漏判，實務上接近 must_change。

**因應：** 在模擬環境對每個 Send*ProxyOrder／Send*ProxyAlter／SendTFOffset 各下一筆測試單，記錄 .57 與 .59 的 (Code, Message) 實際值做差異比對；成功判定改為「Code==0 且以回報事件為最終確認」，不依賴 Message 字串格式。

### C-22（必改／優化）

**問題：** SendForeignStockProxyCancel 的結果回呼在手冊中由 OnAsyncOrder 更正為 OnProxyOrder（並補上需 OnProxyStatus 通知 5001 的條件）。DLL 行為未變，此破損早於 .59 即存在、.59 只是揭露；且 ProxyServer 下單元件分冊（版本仍 V2.13.47）第 187 行仍是舊寫法，照該分冊開發仍會被誤導。

**因應：** 複委託 Proxy 刪單改以 OnProxyOrder 接收結果，並確認 OnProxyStatus 已通知 5001 後才送單；過渡期兩個事件都掛、以實測確認哪一個真正觸發後再移除無效者。

### C-24（建議跟進／優化）

**問題：** 修正「複委託回報沒給時效欄位時需給前端空值」。官方未說明修正前行為，可能是整欄缺漏（升級後逗號數增加而位移）或給了非空錯值（升級後僅該欄變空），兩種讀法皆無法由文件或範例判定；且 12.回報.md 的 OnNewData 欄位表無「委託時效」欄，複委託結構亦無時效成員，不可假定值域為 ROD/IOC/FOK。

**因應：** MarketType = OS 的回報所有欄位先判 string.IsNullOrEmpty 再轉型，禁止直接 int.Parse／Enum.Parse／DateTime.Parse；升級後實跑一筆複委託委託與成交回報，與 .57 實測輸出逐欄 diff，確認是「欄位變空」還是「欄位數增加」；若為後者一併套用 C-23 的長度容錯。

### C-16（建議跟進／優化）

**問題：** OnOpenInterestGWStatus 的觸發來源擴大到 GetOpenInterest 與 GetOpenInterestWithFormat（官方範例是初始化時全域掛載，同一支程式又同時提供三個查詢入口），凡以「收到 *GWStatus 即代表 GW 查詢完成」當狀態旗標者會誤判查詢來源。海期側僅分冊 9 擴寫、主手冊 4-2-u 未同步，官方文件自相矛盾。

**因應：** 不要以事件本身推斷查詢來源；改為呼叫查詢函式時自行記錄本次查詢（查詢序號或旗標配對），*GWStatus 只當作「上一次未平倉查詢的成敗通知」。海期側以模擬環境實測為準。

### C-02（建議跟進／優化）

**問題：** SKQuoteLib_RequestTicks 的訂閱內容被單方面擴充：同一次訂閱除成交明細／五檔外會額外推送即時分K（含當日回補、之後每筆 Tick 一則 nType=2 通知），且 sym59/nat59 查無 RequestLiveKLine／Cancel*LiveKLine 之類可單獨關閉的符號，既有只用 Tick／Best5 的程式被動承受額外推送量。

**因應：** COM sink 訂閱制下未掛 OnNotifyLiveKLineData handler 者不會被派送到應用層，OnNotifyTicksLONG／OnNotifyBest5LONG 簽章與格式未變、解析器無須改動；升級後在模擬環境對高頻商品（如 TX00）觀察 CPU／延遲一整個交易時段，量能吃緊則減少同時訂閱檔數（RequestTicks 上限 10 檔）。

### C-20（建議跟進／優化）

**問題：** changelog 寫「自營帳號即時庫存查詢 GetRealBalanceReport 欄位新增昨日庫存」，但主手冊 4-2-c 的 OnRealBalanceReport 欄位表兩版逐字相同（本就含昨日庫存），無法判定是補值還是自營帳號回傳欄數改變；若為後者，依 .57 實測寫死欄位索引的自營帳號解析器會整排錯位。

**因應：** 自營帳號使用者在模擬環境重跑 GetRealBalanceReport，印出 bstrData 實際欄位數並與手冊 19 欄對照，確認「昨日庫存股數」位置與值；解析改為長度容錯（官方範例只取 values[0..17]、欄位表列 19 欄，本就有落差）。一般帳號不受影響。

### C-07（建議跟進／優化）

**問題：** 2.13.58 修正「報價只有全盤商品的 T 盤需要加 AM」，等同縮小必須加 AM 的商品範圍。無條件對所有商品代號接 AM 的程式可能訂到不存在的代號，而 RequestStocks 備註明訂「帶入的股票代號不存在則直接略過、不回傳錯誤」——失敗形式是靜默無報價、無錯誤碼。註：此範圍解讀來自版本歷程單句，手冊正文未同步更新。

**因應：** 盤點所有組出 XXXXAM 代號的位置，改為僅對全盤商品加 AM 並以商品檔（OnNotifyStockList／OnNotifyCommodityListWithTypeNo）驗證代號存在；訂閱端加「N 秒未收到報價即告警」的靜默失敗偵測；換版後於 AM 盤時段實測。

### C-51（建議跟進／退步）

**問題：** 新增的 4-4-35 GetLiveKLineLONG 說明寫「需先訂閱即時分K RequestLiveKLine」，但該名稱在四份符號表與 .57 原文皆 0 命中。照字面去找會找不到函式，且極可能誤用名稱相近、確實存在但只推 OnNotifyTicksLONG 的 SKQuoteLib_RequestLiveTick（sym59:340、手冊 4-4-18），導致 OnNotifyLiveKLineData 永遠不觸發。

**因應：** 即時分K 的訂閱入口一律用 SKQuoteLib_RequestTicks；在 api_spec 明寫 RequestLiveKLine 為官方筆誤、不存在於任何符號表，並標註 RequestLiveTick 不會推播分K。

### C-48（建議跟進／退步）

**問題：** 官方文件全面下架海期／海選報價：主手冊 4-5／4-6 兩章、SKFOREIGNTICK 等 4 個結構、14.海期報價／15.海選報價 兩份分冊皆消失，9.下單-海外期選.md 的 LoadOSCommodity／LoadOOCommodity 備註被砍成一句（原本的「先連海期備妥商品檔」「2015 請重連或重新下載」不見了）；但錯誤碼 2015／2025／2026 仍列在 .59 錯誤表，執行期仍強制此前置條件，形成「規則還在、說明不見」。sym57/sym59 對 SKOSQuoteLib 56:56、SKOOQuoteLib 37:37 完全相同，是文件移除而非 API 移除。

**因應：** 把 .57 的 docx 與 _raw/*.md（尤其 14／15 分冊、9.下單-海外期選.md 舊版備註）永久保存並納入版本控制；api_spec 對應模組檔開頭加註「V2.13.59 官方已無此章節，內容以 V2.13.57 留存文件為準，函式本身未移除」；引用結構編號時改寫結構名（.59 的 5-x 編號已因刪除而位移）。

### C-49（建議跟進／退步）

**問題：** 主手冊刪除 3-3 行情物件連線限制（每 ID 2 條、國內共用 1 條／海外期選 1 條、案例圖）、3-3-2 RequestStocks 與 RequestStocksWithMarketNo 混用禁忌，以及 3-4 SHORT 32767 新舊函式對照表，第 3 章只剩 3-1／3-2。排查「訂閱不到行情」「取不到 index > 32767 商品」最關鍵的官方依據已無處可查；本 repo api_spec/modules/SKQuoteLib.md:649 仍以「見主說明 3-4」為出處。

**因應：** 把所有引用處出處改標「V2.13.57 主手冊 3-3／3-3-2／3-4（.59 已刪除）」；補記 .59 僅存的替代佐證（4-4-23 GetQuoteStatus 備註「最大連線數為2…回傳:2,True」、錯誤碼 3030、4-1-9 LoginSetQuote 的 Y/N）；把 .57 主手冊該段落節錄留存於 repo。

### C-11（建議跟進／不明）

**問題：** 2.13.59 changelog 一句「修正 index 不一致問題」，未指名模組、無任何符號或本文佐證。若實際涉及報價商品索引，會快取 nStockidx（或商品清單順序）再回頭以 GetStockByIndexLONG／GetStockByNoLONG 取物件的程式可能取到錯的商品，且無錯誤碼可偵測。

**因應：** 不預先改碼，但升級後回歸驗證索引與物件對應：於 OnNotifyQuoteLONG 內即時以 (sMarketNo, nIndex) 取物件並比對 bstrStockNo 是否等於預期商品；避免跨 EnterMonitorLONG 週期沿用舊索引，斷線重連後一律重建索引對照表。

### C-44（建議跟進／中性）

**問題：** 官方即時分K 示範 buttonGetLiveKLine_Click 把 SKKLINE 的 nOpen/nHigh/nLow/nClose 寫死 /100m，與同一檔 SKQuote.cs 全篇使用 Math.Pow(10, sDecimal) 的慣例矛盾，事件端 OnNotifyLiveKLineData 又完全不還原；SKKLINE 結構不含小數位欄位，官方手冊明寫「價格為原始價格，需自行除以小數位數還原」，非兩位小數商品照抄會顯示錯價。

**因應：** 以 SKQuoteLib_GetStockByNoLONG／GetStockByIndexLONG 取 SKSTOCKLONG.sDecimal 快取成商品→小數位對照表，用 nOpen / Math.Pow(10, sDecimal) 還原；K 棒聚合階段保留原始整數、只在顯示層還原；取不到 sDecimal 時明確報錯而非 fallback 100。

### C-45（無／中性）

**問題：** OnOpenInterestJson 官方示範以手刻 IndexOf('"') 配對切割冒充 JSON 解析（全樹無任何 JSON 函式庫），且 bstrData.Contains("001,查無資料") 排在 IsNullOrWhiteSpace 與 try 之前，null 輸入會擲出未捕捉的 NullReferenceException（在 COM 事件回呼中拋例外可能終止行程）。

**因應：** 把 null／空字串判斷提到最前面、整段解析包進 try/catch；改用 System.Text.Json 或 Newtonsoft.Json 反序列化為 string[]，再依官方格式1／格式2 以逗號切欄位並驗證欄位數；解析失敗時記錄原始 bstrData 以便回溯。

### C-40（建議跟進／不明）

**問題：** 《C_Sharp策略王DLL元件使用說明》三處品質問題：五個新方法的「宣告」欄誤寫成 (int Code, string Message) ValueTuple（實際回傳 *ParserResult，照抄 var (code,msg)= 會編譯失敗且取不到 Blocks）、GetOFOpenInterestGW 範例多寫 nFormat + 1（送出參數表未定義的值）、文件頁尾版號標 2.13.58 落後於 .59 樹。

**因應：** 以 SKDLLTester/Form1.cs 實際用法為準：var result = SK.GetXxx(...); 讀 result.StatusCode／Message／Blocks；nFormat 直接傳 0 不加 1；在規格庫加註「宣告型別以 SKDLLTester 原始碼為準」。

### C-41（建議跟進／中性）

**問題：** SKDLLTester 的 buttonManageServerConnection_Click 新增 if (comboBoxTargetType.SelectedIndex == 2) nTargetType = 4; 補償映射，校正下拉選單刪掉海期／海選兩項後 Proxy下單的索引位移；nTargetType 數值本身未重編號，但手冊範例欄未同步此映射。曾照抄 int nTargetType = comboBoxTargetType.SelectedIndex; 者會把 Proxy下單誤傳成 2。

**因應：** 不要用 SelectedIndex 當 nTargetType，改用顯式常數或 SelectedItem 字串前綴解析；若沿用 tester 寫法務必一併複製補償映射（.59 Form1.cs:1138-1146）。

## 5. 範例包與文件下架的正確解讀

.59 範例包最醒目的事實是「海期／海選報價示範整批消失」，但這件事必須分三層讀，結論才不會誤判。

【事實層】已複驗：.57 的 SKCOMTester 目錄有 SKOSQuote.cs／SKOSQuote.Designer.cs／SKOSQuote.resx 與 SKOOQuote 三件套，.59 目錄中這六個檔案完全不存在（.59 只剩 SKOrder／SKQuote／SKReply 三組）。同樣的刪除也發生在 SKCOMTesterV2（Quote/OSQuoteForm.*、OOQuoteForm.*，且 MainForm.Designer.cs 拔除不徹底、殘留兩顆無文字空白按鈕）、CppCLITester（SKOSQuote.cpp/.h/.resx 三個專案項目移除）、ExcelSample.xls（491008→582656 bytes，刪掉「海期報價」「海選報價」兩張 RTD 分頁與 OSQuoteObj/OOQuoteObj VBA 模組及 OnRequestOSQuote/OnRequestOOQuote），以及隨包手冊——14.海期報價.docx／15.海選報價.docx 在 .59 目錄中不存在，主手冊也刪掉 4-5 SKOSQuoteLib／4-6 SKOOQuoteLib 兩整章與 4 個 SKFOREIGN* 結構。官方 2.13.58／2.13.59 兩列 changelog 對此隻字未提。

【COM API 還在嗎？在，而且完整無損】sym57 與 sym59 對 SKOSQuoteLib 是 56:56、SKOOQuoteLib 是 37:37，逐行相同；本次以 comm 複驗 Interop.SKCOMLib 全域為「39 加、0 減」，沒有任何符號被移除。原生 SKCOM.dll 以 objdump 解析 PE 匯出表實測為 77（.57）→ 82（.59），移除數為 0，新增的 5 支是 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights／GetOFOpenInterestGW／GetOFFutureRights，與海期海選無關。錯誤碼 2015／2025／2026 仍列在 .59 錯誤表，.59 自己的下單範例（OFSendOrderForm／OFStrategyOrderForm）也還在呼叫 SKOSQuoteLib_EnterMonitorLONG。所以：走 COM／Interop 的海期海選報價程式一行都不用改，能力完整保留，這純粹是「文件與範例下架」，不是「API 下架」。

【簡化版 SKDLLCSharp 包裝層呢？這一層是真的被砍了】這是本次唯一名副其實的破壞性移除，也是最容易被上面那句「COM 還在」掩蓋的風險。實測 .57 的 x64 Release SKDLLCSharp.dll（45568 bytes）中 SKOSQuoteLib_RequestStocks／SKOSQuoteLib_RequestTicks／SKOSQuoteLib_GetStockByNoNineDigitLONG／SKOOQuoteLib_RequestStocks／SKOOQuoteLib_RequestTicks／SKOOQuoteLib_GetStockByNoLONG 六者字串各命中 1；.59 版（70144 bytes，2026-03-17）全部為 0，而同一顆 DLL 反而新增了 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights。x86 版結果一致。更不對稱的是 OnNotifyOSQuoteLONG／OnNotifyOOQuoteLONG 的事件符號仍殘留在 .59 DLL 中，卻已無任何訂閱或取值方法可搭配，等同孤兒事件。同批 ManageServerConnection 的 nTargetType 也從 0/1/2/3/4 縮為 0:回報／1:國內行情／4:Proxy下單，2:海期行情、3:海選行情自手冊下架。走這條路線且用到海外報價者，換上 .59 的 SKDLLCSharp.dll 會直接編譯／繫結失敗，且此層沒有同層替代品。

【正確解讀與處置】(1) 純 COM／Interop 使用者：不受影響，照常升級，只是未來查海期海選規格只能靠 .57 留存文件。(2) SKDLLCSharp 使用者：把海期海選報價路徑凍結在 .57 版組件（其餘部分仍可升級，因 Interop 與原生匯出零移除），或改寫為直呼 COM。(3) 無論哪條路線，都應把 .57 整棵範例樹與 14／15 分冊永久歸檔納入版控——.59 之後已無官方參考碼可查。(4) 這個「符號都在、文件與範例卻全面下架」的不對稱訊號本身值得向群益求證：是僅停止維護文件，還是海期海選報價元件已進入靜默棄用倒數。在得到答覆前，不建議在此二元件上投入新開發。

【範例包的加分與扣分】.59 只新增兩處示範且品質不佳：即時分K（buttonGetLiveKLine_Click 把 SKKLINE 價格寫死 /100m，與同檔 Math.Pow(10, sDecimal) 慣例矛盾，事件端又完全不還原）與 OnOpenInterestJson（手刻 IndexOf('\"') 切割冒充 JSON 解析，且 Contains 檢查排在 null 判斷之前，null 輸入會擲出未捕捉的 NullReferenceException）。兩者都不宜直接照抄。

## 6. 升級步驟（維護 2.13.57 程式者）

1. 第 0 步｜評估與凍結基準：先盤點自家程式走的是哪條路線——(a) 純 COM／Interop、(b) SKDLLCSharp 簡化包裝層、(c) 兩者混用。grep 這些關鍵字判定：SKOSQuoteLib_RequestStocks、SKOOQuoteLib_RequestStocks、GetStockByNoNineDigitLONG、ManageServerConnection、`using SKDLLCSharp;`。同時把整棵 /home/hg/PERSONAL/SKCOM/Source_code/CapitalAPI_2.13.57_CExample（尤其 SKCOMTester/SKOSQuote.cs、SKOOQuote.cs、SKCOMTesterV2/Quote/OSQuoteForm.*、OOQuoteForm.*、CppCLITester/SKOSQuote.*、ExcelSample.xls）與 14.海期報價.docx、15.海選報價.docx、策略王COM元件使用說明_V2.13.57.docx 永久歸檔納入版控——.59 包已不再提供，這是海期／海選唯一的官方參考。
2. 第 1 步｜錄製 .57 基準資料：在升級前，於模擬環境用 .57 各錄一份原始字串樣本並存檔：同一商品同一交易日的完整 tick 流（OnNotifyTicksLONG 的 nPtr 序列）、一筆國內期選（TF/TO）與一筆複委託（OS）的 OnNewData 原文、三支智慧單被動回報（GetTSSmartStrategyReport／GetStopLossReport／GetOFSmartStrategyReport）的回傳、未平倉四支查詢的正常與查無資料回傳、各 Proxy 下單函式的 (Code, Message)。沒有這份基準，後面所有欄位級 diff 都做不了。
3. 第 2 步｜先加固解析器再換元件（可在 .57 上先行部署）：把所有「欄位數必須等於 N」的驗證改為「至少 N 欄」，每次 values[n] 前加 values.Length 邊界檢查；未平倉查無資料判斷改為『split(',') 首欄 == "001" 或整串含「查無資料」』並同時容忍 2 欄與 3 欄；複委託（MarketType = OS）所有欄位先判空字串再轉型；保留原始 bstrData 原文供事後回溯。（C-13、C-14、C-23、C-24）
4. 第 3 步｜補上 9996 終止性登入分支（不換元件也該先做）：在登入回傳處理新增 else if (nCode == 9996)，排在泛用 else 之前、與 9997 分流，顯示「元件版本過舊，請更新 SKCOM 元件」、停止所有自動重試並關閉重連 Timer、標記需人工介入；把 9996 加入不可重試白名單，避免無限重試撞上登入失敗五次鎖定。（C-31）
5. 第 4 步｜換裝元件：以系統管理員身分先執行舊版 元件/<平台>/Uninstall.bat，再執行 2.13.59 對應平台的 install.bat（x86／x64 擇一勿混用；32 位元 DLL 用 %systemroot%\\SysWoW64\\regsvr32.exe、64 位元用 System32\\regsvr32.exe 或直接註冊）。原生 SKCOM.dll 與 Interop.SKCOMLib.dll 一併替換——原生匯出由 77 增為 82、0 移除，Interop 為 39 加 0 減且 IID 與組件版本未變，屬 drop-in；.57 編譯的早期繫結程式因 vtable append-only 不會錯位，可先不重編、僅換元件驗證。註冊後以 SKCenterLib_GetSKAPIVersionAndBit() 確認版本與位元（EX: 2.13.59_x64）與建置平台一致。（C-37、C-39、C-54）
6. 第 5 步｜若走 SKDLLCSharp 簡化包裝層且用到海期／海選報價：不要換 .59 的 SKDLLCSharp.dll。短期把該路徑凍結在 .57 版組件（其餘部分照常升級），長期改直呼 COM 的 SKOSQuoteLib／SKOOQuoteLib（EnterMonitorLONG → 等 OnConnect 3001 → RequestStocks／RequestTicks，流程見 api_spec/flows/C-quotes.md）。同時修正 ManageServerConnection：nTargetType 只保留 0／1／4，移除 2／3 用法，不要以 UI SelectedIndex 直接當參數，nStatus=4 在確認前不使用。（C-34、C-35、C-41）
7. 第 6 步｜移除為舊版缺陷寫的所有 workaround：刪掉 tick 首筆特判／去重／補值（改以 nPtr 為唯一去重鍵）；刪掉為智慧單被動回報缺逗號寫的欄位偏移補償；刪掉以「SendStockStrategyCB 收到 1023 即跳過或重試」的繞道（該委託在 .58 起會真的送出）。這些補償在 .59 會反過來變成錯誤來源。（C-09、C-26、C-18）
8. 第 7 步｜修正語意層假設：把所有以 nCode==0 作為終態的下單／改單／刪單分支改為「已受理」語意，最終狀態一律由 SKReplyLib 的 OnNewData（或 OnAsyncOrder／OnProxyOrder）決定，並建立委託序號／ThreadID 對回報的配對追蹤與逾時告警；複委託 Proxy 刪單 SendForeignStockProxyCancel 改掛 OnProxyOrder 並確認 OnProxyStatus 已通知 5001 後才送單；不要以 OnOpenInterestGWStatus 推斷查詢來源，改由呼叫端自行記錄。（C-21、C-22、C-16）
9. 第 8 步｜模擬環境逐項實測欄位序（升級後最關鍵的一步）：呼叫 GetOpenInterestGW 印出完整 bstrData 確認「商品－下單代碼」的實際欄位位置（若證實為中間插入，該項立即升為必改）；下一筆國內期選委託確認 OnNewData「下單時間 HH:mm:ss.fff」的實際 index；下一筆複委託確認時效欄位是變空還是欄位數增加；三支智慧單被動回報逐單別與 .57 基準逐欄 diff；自營帳號重跑 GetRealBalanceReport 核對欄位數與「昨日庫存股數」位置；各 Proxy 下單函式重錄 (Code, Message) 與 .57 比對。全部確認後才更新欄位對照表並啟用新欄位。（C-13、C-14、C-20、C-23、C-24、C-26、C-36）
10. 第 9 步｜回歸驗證受行為修正影響的路徑：AM 代號組法改為僅對全盤商品加 AM 並以商品檔驗證代號存在，訂閱端加「N 秒未收到報價即告警」的靜默失敗偵測；重驗商品索引與物件對應（於 OnNotifyQuoteLONG 內以 (sMarketNo, nIndex) 取物件比對 bstrStockNo），斷線重連後重建索引對照表；多帳號回報實測 SolaceCloseByID 斷線釋放與重連是否疊加；SGX 專線使用者複查原本的輪詢補洞邏輯是否會重複處理同一筆委託。（C-07、C-11、C-25、C-27）
11. 第 10 步｜（可選）採用新能力：即時分K——在 EnterMonitorLONG 前掛 OnNotifyLiveKLineData，照舊呼叫 RequestTicks 訂閱（沒有獨立分K 訂閱函式，手冊的 RequestLiveKLine 不存在，也勿誤用 RequestLiveTick），處理 nType=0／1／2 三種語意，nTimehm 為不補零 HHmm 需自行補零，價格以 SKSTOCKLONG.sDecimal 搭配 Math.Pow(10, sDecimal) 還原（不可照抄官方 /100m），且不得在事件內回呼 GetLiveKLineLONG。未平倉 JSON——訂閱 OnOpenInterestJson，用正式 JSON 解析器並把 null 檢查提到最前面。權益數狀態——訂閱 OnFutureRightsStatus／OnOverseaFutureRightsStatus，注意成功時 bstrErrorMsg 是 "Success!" 而非空字串，勿與 *GWStatus 家族共用成敗解析器。海選 OLID——SendOverseaOptionOrderOLID 的 bAsyncOrder 必須為 true 且結果掛 OnAsyncOrderOLID。市場分流——可改用 EnterMonitorLONGByMarket(0=證券／1=期貨)，但需實測那批註明「須使用 EnterMonitorLONG 登入」的 LONG 函式是否仍正常。（C-01、C-03、C-12、C-15、C-29、C-44、C-45、C-51）
12. 第 11 步｜避開未公開與已知瑕疵的介面：不要使用 SKQuoteLib_ExportStockList（有符號無文件）、GetOFOpenInterestWithDetails／OnOpenInterestWithDetails／OnOverseaFutureOpenInterestWithDetails（11 個符號，手冊、changelog、兩棵範例樹全部零命中）。若以官方範例當模板，刪掉 SKCOMTesterV2/MainForm.Designer.cs 殘留的 buttonOSQuoteForm／buttonOOQuoteForm、SKCOMTester/Form1.cs 未初始化的 m_pSKOSQuote／m_pSKOOQuote，並自行補回 SKReply.cs 被拿掉的雙帳號分流（.57 版才是正確參考）。重編 CppCLITester 需 VS2022（v143）＋Windows SDK 10.0.20348.0，並自行產生或修正懸空的 Interop HintPath。（C-04、C-17、C-28、C-43、C-47）
13. 第 12 步｜更新本 repo 規格庫：依 spec_updates 逐檔改寫，並把 README.md、CLAUDE.md、error_codes.md 的版本基準由 2.13.57 改為 2.13.59，同時明確標註「海期／海選章節與 3-3／3-4 內容僅存於 .57 留存文件，函式未移除」。

## 7. 本規格庫待更新清單

- **`api_spec/README.md`** — 版本基準由 V2.13.57 全面改標 V2.13.59（第 3、45、57 行的版本字樣與範例路徑）。同時加註三點：(1) 「範例」欄位的路徑基準刻意保留 .57 樹，因 .59 已刪除 SKOSQuote.cs／SKOOQuote.cs／OSQuoteForm.*／OOQuoteForm.* 等檔，海期海選的行號證據在 .59 樹無法對應；(2) 官方 docx 由 21 份減為 19 份（14.海期報價、15.海選報價 下架），本庫同時涵蓋 .57 留存文件與 .59 新增內容；(3) 《C_Sharp策略王DLL元件使用說明》頁尾版號為 2.13.58，落後於 .59 樹。
- **`CLAUDE.md`** — 第 3 行「群益期貨 CapitalAPI（策略王 COM 元件，v2.13.57）規格庫」的版本號改為 v2.13.59；並在「API 查詢優先序」那行後補一句版本註記：海期／海選報價（SKOSQuoteLib／SKOOQuoteLib）與行情連線限制、SHORT 32767 對照表的官方章節僅存於 V2.13.57 留存文件，函式本身未移除。維持 CLAUDE.md 30 行內限制，勿擴充。
- **`api_spec/modules/SKQuoteLib.md`** — (1) 新增 SKQuoteLib_GetLiveKLineLONG（Long ...([in] BSTR bstrStockNo, [in,out] struct SKKLINE* pSKKLine)，備註須先 EnterMonitorLONG、避免在 OnNotifyLiveKLineData 內回呼）與 SKQuoteLib_EnterMonitorLONGByMarket（Long ...([in] LONG nMarketType)，0=只訂閱國內證券／1=只訂閱國內期貨，與 EnterMonitorLONG 擇一）兩節，方法計數由 34 改為 36、版本標註改 V2.13.59。(2) 事件節新增 OnNotifyLiveKLineData（void ...(LONG nType, BSTR bstrStockNo, LONG nDate, LONG nTimehm, LONG nOpen, LONG nHigh, LONG nLow, LONG nClose, LONG nQty)；nType 0=清盤重置本筆價格全 0 需捨棄已收資料／1=即時分K 含當日回補／2=該分鐘每筆Tick），計數 20→21。(3) 新增 SKKLINE 結構定義，註明 nTimehm 為不補零 HHmm（904=09:04）、價格為未還原原始整數。(4) RequestTicks 節（第 165-178 行）備註由「成交明細＋五檔」改為含即時分K，註明這是分K 唯一有文件依據的訂閱入口、無獨立取消函式。(5) 陷阱節新增：手冊 4-4-35 誤植不存在的 RequestLiveKLine（四份符號表 0 命中，實際入口為 RequestTicks，且勿誤用只推 OnNotifyTicksLONG 的 RequestLiveTick）；官方範例寫死 /100m 與同檔 Math.Pow(10, sDecimal) 慣例矛盾、SKKLINE 不帶小數位須另取 sDecimal 還原；.58 修正價差商品訂閱／報價慢／T盤加 AM 範圍／GetStockByNoLONG 取物件；.59 修正第一筆 Ticks 缺失（既有補償需移除）／內期夜盤收盤價／index 不一致（範圍未指名）。(6) 第 649 行「見主說明 3-4 行情功能修改說明」改標「V2.13.57 主手冊 3-4（V2.13.59 已整節刪除）」；GetQuoteStatus 節與陷阱補註「3-3 連線限制說明已刪，兩條連線的分配政策僅存於 V2.13.57 文件」。(7) 新增「未公開介面（勿用）」記錄 SKQuoteLib_ExportStockList(short sMarketNo)→int，官方手冊／changelog／範例三處皆無。(8) DLL 載體節（約 L115-131、L602-645、L1020 陷阱第 15 點）更新 ManageServerConnection 的 nTargetType 自 .59 起僅 0／1／4，nStatus=4 備援僅存於範例、手冊已刪。
- **`api_spec/modules/SKOrderLib.md`** — (1) 第 1235、2554 行的「查無資料回傳 M003 NO DATA#」改寫為 .58 起的「001,查無資料,帳號」，並加註『解析器應以首欄==001 或整串含「查無資料」判斷，並同時容忍 2 欄（分冊 7）與 3 欄（主手冊）兩種官方不一致格式』；第 1249、1264、1677 行（GetOpenInterest／GetOpenInterestWithFormat／GetOverseaFutureOpenInterest）補上同一段說明。(2) GetOpenInterestGW 節（第 1223 行起）新增 .59 的「商品－下單代碼」欄位，照官方原文記載「查詢異常時回傳空值，請重新呼叫 GetOpenInterestGW」，並註明欄位序列在官方 docx 內為圖片、插入位置未經文件確認需實測。(3) 新增事件節 OnOpenInterestJson（void ...(string bstrData)；由 GetOpenInterestGW／GetOpenInterest／GetOpenInterestWithFormat 觸發；bstrData 為 JSON 陣列字串、元素內部仍逗號分隔；與逐筆 OnOpenInterest 並存），並在第 176 行起事件總表與第 204 行起註冊區塊補行，範例指向 .59 樹 SKCOMTester/SKOrder.cs:179-238；陷阱記錄官方示範以 IndexOf('"') 手刻切割、null 檢查排在 Contains 之後會擲 NullReferenceException，不宜照抄。(4) 新增 OnFutureRightsStatus 與 OnOverseaFutureRightsStatus 兩節（void On*RightsStatus(int nQueryStatus, string bstrErrorMsg)；nQueryStatus 0=成功 1=失敗；分別由 GetFutureRights 與 GetRequestOverSeaFutureRight 觸發），務必記下『成功時 bstrErrorMsg 為 "Success!" 而非空字串，與 *GWStatus 家族不同』與大小寫陷阱『新事件是 OnOverseaFutureRightsStatus(Oversea)、既有資料事件卻是 OnOverSeaFutureRight(OverSea)』。(5) OnOpenInterestGWStatus（第 2557 行）用途改為三支查詢皆會觸發；OnOverseaFutureOpenInterestGWStatus（第 2649 行）加註分冊 9 與主手冊 4-2-u 矛盾需實測，並提醒勿以事件推斷查詢來源。(6) 新增「未公開介面（勿用）」節記錄 GetOFOpenInterestWithDetails／OnOpenInterestWithDetails／OnOverseaFutureOpenInterestWithDetails 共 11 個符號零文件零範例。(7) 所有下單／刪改單／Proxy 函式的「回傳」欄統一補上『0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認』。(8) SendForeignStockProxyCancel 節（第 2221 行起）備註改為由 OnProxyOrder 取得結果、需 OnProxyStatus 通知 5001 後才送單，並註明 .57 手冊誤植為 OnAsyncOrder、ProxyServer 分冊（V2.13.47）至今仍是舊寫法。(9) 新增 SendOverseaOptionOrderOLID：函式總表第 113 行後補一列，正文第 1780 行後比照 SendOverseaFutureSpreadOrder2OLID 格式補一節，簽名 int SendOverseaOptionOrderOLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, string bstrOrderLinkedID, out string bstrMessage)，備註標「前置 SKOrderLib_LoadOOCommodity()；2.13.58 新增；bAsyncOrder 須為 true，結果掛 OnAsyncOrderOLID」，範例指 .59 樹 SKCOMTester/SKOrder.cs:770。(10) GetRealBalanceReport／OnRealBalanceReport（第 778、2467 行附近）加註 .59 changelog「自營帳號欄位新增昨日庫存」但手冊欄位表兩版未變、實際欄位數需實測。(11) 三支智慧單被動回報事件（OnTSSmartStrategyReport L175／OnStopLossReport L179／OnOFSmartStrategyReport L186 及對應查詢 L61／L96／L128）加註 .59 修正缺逗號、官方未指名單別與欄位位置亦無比較表，解析改長度檢查＋欄位名對映。(12) SKOrderLib_LoadOSCommodity（第 1622 行）／LoadOOCommodity（第 1630 行）保留 .57 完整備註並標來源版本，加註「.59 分冊 9 已刪減為一句，但 2015／2025／2026 錯誤碼仍在，前置條件依舊有效」。(13) 五個新查詢（GetRealBalanceReport L778、GetOpenInterestGW L1223、GetFutureRights L1267 與新增的 GetOFOpenInterestGW／GetOFFutureRights）補一行「.59 起亦可經 SKDLLCSharp 簡化包裝層呼叫，回傳 *ParserResult（StatusCode／Message／RawData／Blocks），非 COM 的錯誤碼＋事件模式；官方手冊宣告欄誤寫為 ValueTuple，型別以 SKDLLTester 原始碼為準」。(14) 第 2707 行「版本異動高風險點」補入本次三項：未平倉查無資料格式變更、GetOpenInterestGW 新欄位、下單回傳值語意降級，以及簡化版下單回傳值修正。
- **`api_spec/modules/SKReplyLib.md`** — (1) OnNewData 節（L248-316 欄位表）在 index 47 SeqNo／index 48 OFSTPFlag 之後加註「V2.13.59 國內期選新增欄位『下單時間 HH:mm:ss.fff』，官方僅在版本控管表載明一行，主手冊 4-3-g、12.回報.md 欄位表與範例 ReplyForm.cs 皆未更新，欄位 index 未定、待實機驗證」，標示適用市場 TF/TO；bstrData 說明補「複委託（MarketType OS）之時效欄位在來源未提供時，V2.13.59 起回傳空字串，解析須容忍空值」。(2) 「陷阱與注意」第 9 條（L392）目前只列到 V2.13.40 共 49 欄，追加 V2.13.59 新增「下單時間」且 index 未公開；第 8 條（L391）追加複委託時效欄位空值容錯；新增一條「多帳號回報連線：V2.13.59 修正多帳號無法斷線問題（手冊未指名函式），升版後應實測 SolaceCloseByID 的釋放與重連行為」，並在 SKReplyLib_ConnectByID／SolaceCloseByID 兩節備註加同一版本註記。(3) 範例引用維持指向 .57 樹並加註「.59 包的 SKCOMTester/SKReply.cs 已移除 OnComplete／OnNewData 的雙帳號分流（第二組帳號訊息會併入第一組清單、且會誤點亮第一組 Solace 燈號），雙帳號回報請以 .57 版本為參考」。
- **`api_spec/modules/SKOSQuoteLib.md`** — 檔頭加版本基準說明：「V2.13.59 官方主手冊已移除 4-5 SKOSQuoteLib 整章、5-8 SKFOREIGNTICK／5-12 SKFOREIGNTICK_9／5-22 SKFOREIGNLONG／5-23 SKFOREIGN_9LONG 結構與 14.海期報價 分冊；本檔內容以 V2.13.57 留存文件為準。函式未被移除——sym59 與 sym57 對 SKOSQuoteLib 56:56 逐行相同，原生匯出未減。」「陷阱與注意」新增三條：(1) .59 發佈包已移除全部海期報價範例（SKCOMTester/SKOSQuote.cs、SKCOMTesterV2 Quote/OSQuoteForm.*、CppCLITester SKOSQuote.*、ExcelSample「海期報價」分頁）與隨包 14.海期報價.docx，官方版本歷程未公告；(2) COM 介面本身未變，SKOSQuoteLib_EnterMonitorLONG 仍被 .59 的 OFSendOrderForm／OFStrategyOrderForm 呼叫，錯誤碼 2025 仍在手冊；(3) SKDLLCSharp.dll 於 .59 已移除 SKOSQuoteLib_RequestStocks／RequestTicks／GetStockByNoNineDigitLONG 三個公開方法（x64/x86 一致，實測 .57 各命中 1、.59 全為 0），OnNotifyOS* 事件符號殘留成孤兒，DLL 路線使用者屬 breaking。
- **`api_spec/modules/SKOOQuoteLib.md`** — 同 SKOSQuoteLib.md 的處理：檔頭註明 .59 已移除 4-6 SKOOQuoteLib 整章與 15.海選報價 分冊、本檔以 V2.13.57 為準，sym59 中 37 個成員（含非 LONG 的 GetTick）仍存在、原生匯出未減，文件下架不等於功能下架。改寫 L455「別與 DLL 版混淆」條：SKDLLCSharp 版的 SKOOQuoteLib_RequestStocks／RequestTicks／GetStockByNoLONG 自 .59 起已自包裝層移除（實測 .57=1、.59=0），OnNotifyOOQuoteLONG／Best10／Ticks 的 add_/remove_ 符號殘留但無配套方法；COM 版與原生 flat export 不受影響，海選報價請直呼 COM。第 443 行「V2.13.46 已移除舊版非 LONG 函式」標註為官方文字，與符號現況的差異需保留說明。第 79 行與第 452 行引用的「見 9.下單-海外期選.md；錯誤代碼 2015…」來源在 .59 分冊 9 已被刪除，需改標「.57 版分冊 9」或改引 .59 合訂本 4-2-39。
- **`api_spec/modules/SKCenterLib.md`** — (1) SKCenterLib_Login 備註（約第 98 行）補「V2.13.59 起另有 9996 SK_ERROR_UPDATE_API_REQUIRED，表示元件版本已被停用、須更新版本、重試無效，須與 9997 分流處理」；第 309 行「V2.13.57 登入節流」段同步補 9996 並註明自動重試白名單不得包含 9996。(2) 第 164 行 SKCenterLib_RequestAgreement 備註「查詢時海外行情同意書一定查詢」加註：V2.13.59 分冊 3.登入.md 已刪去此特例但同版主手冊仍保留原句，兩者不一致、行為以群益確認為準。(3) 第 312 行「一個 ID 預設最多 2 條行情連線（國內共用 1 條、海外期選 1 條）」補來源標註：出自 V2.13.57 主手冊 3-3，V2.13.59 已刪除該節，.59 僅剩錯誤碼 3030 與 4-4-23 GetQuoteStatus 備註可佐證。(4) 第 32 行環境前置與第 317 行陷阱補上 .59 更正後的具體註冊路徑：32 位元 SKCOM.dll 用 %systemroot%\SysWoW64\regsvr32.exe、64 位元用 System32\regsvr32.exe 或直接註冊（依據兩版相同的 install.bat），並註明 .59 合輯手冊附錄 A 仍為舊敘述。(5) 第 6 行版本基準改標 V2.13.59。
- **`api_spec/error_codes.md`** — (1) 第 24 行「版本基準：V2.13.57」改為 V2.13.59 並註明比對來源。(2) 第 228 行節標題「## 四、系統層級碼（9997–9999）」改為（9996–9999），於 9997 之前新增一列：| 9996 | SK_ERROR_UPDATE_API_REQUIRED | 此版本已無法登入，請更新版本。（V2.13.59 新增；終止性錯誤，須更新 SKCOM 元件，重試無效） | 主表(V2.13.59 第6章); 導覽 |，備註標明 .59 的 3.登入.md 代碼表未收錄此碼。(3) 第 55 行 1023 SK_ERROR_MARKET_OUT_OF_RANGE 補註「2.13.58 修正證券智慧單 CB 單 SendStockStrategyCB 誤觸發此碼；若舊程式以收到 1023 作為跳過或重試的 workaround，升級後該委託會真的送出，需複測」。(4) 第 222 行 3030 的出處改註「來源 V2.13.57 主手冊 3-3，V2.13.59 已刪除該節，.59 主表僅存一句『行情連線超過限制時無法訂閱行情通知』」。(5) 第 171 行 2015 補處置註記「重連海期行情主機或重新下載商品檔（來源：.57 分冊 9，.59 已移除該建議）」；2025／2026 補註「.59 仍保留但對應章節說明已下架」。(6) 第 306-311 行 Appendix A（regsvr32 常見錯誤）補一列「使用位元不對應的 regsvr32.exe 亦會出現 DllRegisterServer 呼叫失敗」並附正確路徑對應。
- **`api_spec/flows/C-quotes.md`** — (1) 檔頭加版本註記：截至 2.13.59，COM 介面（Interop 符號、SKCOM.dll 匯出）完全未變、流程照走無誤；但官方 .59 已下架全部海期／海選範例與隨包手冊 14／15 章及主手冊 4-5／4-6，本檔段二（步驟 10-16 海期）／段三（步驟 17-23 海選）引用的範例路徑僅存在於 .57 樹。(2) 加警告段：走 SKDLLCSharp.dll 者，.59 該組件已移除 SKOSQuoteLib_RequestStocks／RequestTicks／GetStockByNoNineDigitLONG 與 SKOOQuoteLib_RequestStocks／RequestTicks／GetStockByNoLONG，段二／段三在 DLL 路線上已不可行，須改走 COM。(3) 段一「步驟總表」第 3-8 步更新：步驟 3 事件掛載加入 OnNotifyLiveKLineData；步驟 4 補 EnterMonitorLONGByMarket 擇一選項；步驟 7 訂閱說明改為含即時分K；步驟 8 接收事件加入 OnNotifyLiveKLineData，並補最小分K 消費骨架（nType 三態處理＋以 sDecimal 還原＋不得於事件內回呼 GetLiveKLineLONG＋提醒 RequestLiveKLine 不存在、勿誤用 RequestLiveTick）。(4) 「常見錯誤與檢查點」新增：.59 後 tick 首筆不再缺漏（移除應用層補償）、AM 代號組法改為僅全盤商品且失敗為靜默無報價、快取 nStockidx 需重驗、換版後欄位數變動導致靜默錯位。(5) 步驟 12 引用的 OSQuote.log 判讀來源在 .59 僅存於 LoadOSCommodity 備註，需標註。
- **`api_spec/flows/D-reply.md`** — (1) 「OnNewData 逐欄位解析表」（L49 起）導言補一列：V2.13.59 國內期選再增「下單時間 HH:mm:ss.fff」且官方未給 index。(2) 「最小可運作 C# 骨架」（L110 起）加入 values.Length 邊界防護與「保留原始 bstrData 供比對」的示範，並示範複委託（MarketType OS）欄位先判空再轉型。(3) 「常見錯誤與檢查點」（L382 起）加入「換版後欄位數變動導致靜默錯位」的檢查項與 .57／.59 逐欄 diff 的驗證方法。
- **`api_spec/flows/A-login.md`** — (1) 第 90 行失敗碼清單補入 9996。(2) 第 161 行 sequence 註解補一列「Center-->>App: 9996（版本已停用，須更新元件，不可重試）」。(3) 第 188 行「常見錯誤與檢查點」第 4 點擴充為 1129／9997／9996 三者分流，明確標示 9996 為終止性錯誤、程式端無法自救。(4) 第 18 行步驟 1（regsvr32／install.bat 註冊）與第 194 行陷阱第 10 點加註 .59 更正後的 SysWoW64／System32 對應關係，並提醒 .59 合輯手冊附錄 A 仍為舊敘述、應以《1.環境設置》與 install.bat 為準。(5) 第 193 行「避免佔用行情連線（預設每 ID 最多 2 條）」加註來源為 V2.13.57 主手冊 3-3（.59 已刪），保留 1081／3030 說明。
- **`api_spec/flows/B-future-order.md`** — 第 76 行「回傳 nCode==0 只代表『委託伺服器接收成功』」依 2.13.58 官方措辭改為「已成功送至交易所」，第 74 行同步／非同步回傳對照表補上「非同步 0 = Request 送出中，結果由 OnAsyncOrder 確認」；流程尾端新增「未平倉查詢與對帳」小節，指向新的 OnOpenInterestJson、查無資料新格式（001,查無資料[,帳號]）與 GetOpenInterestGW 新增的「商品－下單代碼」欄位。
- **`api_spec/_raw/`** — 重抽 .59 版原文並保留 .57 版：以 tools/extract_docx.py 抽取 .59 的 19 份 docx 另存（例如 _raw59/ 或加版本後綴），同時把 .57 版的 14.海期報價.md、15.海選報價.md、9.下單-海外期選.md（含被 .59 刪除的 2015 處置建議與 EnterMonitorLONG 順序提示）、策略王COM元件使用說明_V2.13.57.md 的 3-3／3-3-2／3-4 段落節錄標記為「V2.13.59 已下架，永久保留」，勿以 .59 版覆蓋。1.環境設置.md 需以 .59 版重抽（.57 版含被更正的舊 regsvr32 敘述）。

## 8. 待向群益確認／需實機測試的問題

- 即時分K 的額外推送能否單獨關閉？sym59/nat59 查無 RequestLiveKLine 或 Cancel*LiveKLine 類符號，SKQuoteLib_CancelRequestTicks 是否會連帶停止分K 推送、或有未公開的關閉方式？分K 訂閱是否佔用 RequestTicks 的 10 檔額度、或另有獨立上限？加掛 OnNotifyLiveKLineData 後對高頻商品（如 TX00）的實際頻寬與事件量增幅為何？
- 手冊 4-4-35 的前置條件「需先訂閱即時分K RequestLiveKLine」是否為誤植？該字串在 .59 全部文件與四份符號表中僅出現於這句話本身。需請群益更正，或實機驗證：只呼叫 EnterMonitorLONG 而不呼叫 RequestTicks 時 OnNotifyLiveKLineData 是否會觸發。
- SKQuoteLib_GetLiveKLineLONG 回傳的價格是否一律可用同商品 SKSTOCKLONG.sDecimal 還原？SKKLINE 結構不帶小數位欄位，需以非兩位小數的商品（部分權證、外幣計價商品）實機驗證，確認官方示範的 /100 是巧合還是有隱含規則。
- 改用 SKQuoteLib_EnterMonitorLONGByMarket 連線後，那批備註寫著「須使用 SKQuoteLib_EnterMonitorLONG 登入」的 LONG 函式與事件（含 GetLiveKLineLONG、OnNotifyLiveKLineData）是否仍正常運作？nMarketType 只定義 0/1，傳入其他值（含 2 或負值）的行為為何？
- SKQuoteLib_ExportStockList(short sMarketNo) 的語意為何？匯出到哪個路徑／檔名／格式，是否有對應通知事件，為何未列入 changelog——是否可用？
- GetOpenInterestGW 新增的「商品－下單代碼」實際插在第幾欄？是附加於末端（官方範例未同步調整，暗示為末端）還是中間插入（則所有固定索引解析器整排錯位）？只能實機呼叫後印出原始 bstrData 確認。
- 未平倉「查無資料」到底是 2 欄「001,查無資料」還是 3 欄「001,查無資料,帳號」？主手冊 4-2-x 與分冊 7.下單-國內期選.md 寫法不一致，且四支查詢函式是否都套用同一格式亦未言明。
- 呼叫同一支未平倉查詢時，OnOpenInterestJson 與逐筆的 OnOpenInterest 是同時觸發還是需擇一訂閱？若同時觸發，兩者訂閱者會拿到重複資料，去重方式為何？
- 海期側 OnOverseaFutureOpenInterestGWStatus 是否真的已擴大到由 GetOverseaFutureOpenInterest 觸發？分冊 9 說有、主手冊 4-2-u 說沒有，官方文件互相矛盾。此擴大（C-16）究竟發生在 2.13.58 還是 2.13.59？兩版 changelog 均未列。
- GetOFOpenInterestWithDetails 與 OnOpenInterestWithDetails／OnOverseaFutureOpenInterestWithDetails（11 個符號）的參數簽名、回傳欄位格式為何？是否可正式使用、與 OnOpenInterestJson 的分工是什麼？官方完全未公開。
- OnNewData 新增的「下單時間 HH:mm:ss.fff」實際落在第幾欄？正式欄位名稱為何（兩版手冊全文 grep OrderTime 皆 0 次）？是否僅出現在國內期選（TF/TO），證券／海期／複委託的欄位數是否維持原狀？是否對所有 Type（N/C/U/P/D/B/S）都填值？
- 複委託時效欄位落在 OnNewData 的第幾欄？值域為何（FOREIGNORDER／OSSTOCKPROXYORDER 結構皆無時效成員）？修正前究竟是整欄缺漏（欄位數不同）還是給了非空錯值？複委託若走 ProxyServer 路徑的 OnProxyOrder，是否同受此空值化修正影響？
- C-26 的缺逗號修正實際發生在三支被動回報中的哪一支、哪一種單別、哪兩個欄位之間？修正後欄位總數各增加多少？目前的分隔字元是半形還是全形逗號（2.13.48 曾做過半形改全形），各單別是否一致？
- 9996 由哪一個函式或事件回傳（SKCenterLib_Login 同步回傳值、OnLoginResult、或 LoginSetQuote）？群益是否已經／預計何時對 2.13.57 及更舊版本啟用？是否為永久性拒絕、有無寬限期、是否會先以 1129／9997 出現再轉為 9996？.59 的 3.登入.md 代碼表未收錄 9996 是疏漏還是意謂不從登入路徑回傳？
- SKDLLCSharp 移除海期／海選報價的 6 個方法是官方正式廢止還是建置疏漏？該組件中 OnNotifyOSQuoteLONG／OnNotifyOOQuoteLONG 事件符號仍殘留卻已無配套取價／訂閱函式，此不對稱狀態需群益回覆；若為遺漏，下一版是否會補回。
- 群益是否計畫淘汰 SKOSQuoteLib／SKOOQuoteLib 報價元件？COM 介面目前完好（sym 兩版逐行相同、原生匯出未減）、範例仍呼叫 EnterMonitorLONG、錯誤碼 2015/2025/2026 仍在，但範例、手冊 4-5／4-6 章與 14／15 分冊全面下架，訊號相反。投入新開發前需確認官方支援年限與是否有替代海外報價方案。
- 「修正簡化版API下單成功回傳值」具體修正方向為何（錯在 StatusCode 還是 Message／ORKEY 格式）？舊值與新值分別是什麼？
- SKDLLCSharp 五個新查詢方法各有兩個多載（int GetXxx(..., StringBuilder, int) 與 XxxParserResult GetXxx(...)），手冊只寫其一且宣告型別寫錯——StringBuilder 版是官方支援的公開用法還是內部殘留？緩衝區大小要求為何？GetOFOpenInterestGW 的 nFormat 除 0（彙總）外是否還有有效值（範例的 nFormat + 1 是否暗示存在格式 1）？GetOFFutureRights 底層是否即 COM 的 GetRequestOverSeaFutureRight、是否會走新增的 OnOverseaFutureRightsStatus？
- 行情連線上限（每 ID 2 條、國內共用 1 條／海外期選 1 條）在 .59 是否仍為相同規則？3-3 整節被刪後只剩 3030 與 GetQuoteStatus 備註可反推，需實機同時開國內＋海期連線驗證。3-4 的「商品總數超過 SHORT 32767」對照表被刪，是否代表非 LONG 舊函式已完全不可用？sym59 中相關非 LONG 成員仍在。
- 2.13.59「修正 index 不一致問題」實際涉及哪個模組？changelog 中該項排在回報／下單相關修正之間，是否根本不屬 quote 領域？2.13.59「修正內期夜盤收盤價未更新」修的是 SKSTOCKLONG 的哪個欄位（nClose 官方註解為「成交價」而非收盤價）還是 K 線類 API？2.13.58「修正報價只有全盤商品的 T 盤需要加 AM」後，究竟哪些商品仍需加 AM（手冊正文未同步更新）？
- 自營帳號的 GetRealBalanceReport 在 .59 回傳的實際欄位數是否與 .57 相同？「昨日庫存」是原本就在只是無值，還是此版才補上一欄導致欄位數改變？
- 2.13.58「國內期貨未平倉 Parse 失敗新增例外處理」的實際行為是靜默丟棄該筆，還是透過 OnOpenInterestGWStatus 回報 nQueryStatus=1？影響串接端能否偵測到資料缺漏。
- SGX 專線主動回報「缺漏」的具體型態是什麼（漏整筆、漏特定 Type、還是特定時段）？是否需要對 .57 期間的歷史回報做補對帳？該路徑走 OnNewData 還是 OnNotifySGXAPIOrderStatus？C-25 多帳號斷線修正實際涉及 SolaceCloseByID 還是舊版 CloseByID，修正後 OnSolaceReplyDisconnect 的觸發時序與 3002／3033 是否有變？
- 主手冊 4-2-116 自相矛盾：SendOverseaOptionOrderOLID 的回傳值欄寫「結果請由 OnAsyncOrder 確認」，但同節參數欄與備註都寫 OnAsyncOrderOLID。實際觸發哪一個（或兩者皆觸發）？bstrOrderLinkedID 的長度上限、允許字元集、是否原樣回傳、重複值是否被拒？同步委託（bAsyncOrder=false）時帶入非空 OLID 會被靜默忽略、報錯還是導致委託失敗？是否支援海選價差／複式單與 SGX 專線模式？
- 海期／海選的範例、隨包手冊 14／15 章與 ExcelSample 分頁究竟是在 2.13.58 還是 2.13.59 移除？官方版本歷程表兩列均未公告，本地只有 .57 與 .59 兩個發佈包，只能斷定「.59 包中已不存在」。需向群益索取 .58 包或版本說明確認。14／15 分冊是否仍可自群益 API 下載專區單獨取得？
- OnOpenInterestJson 上游實際是否可能輸出含跳脫雙引號或巢狀物件的 JSON？官方只承諾「以逗號分隔的字串陣列」，未定義跳脫規則，需實機收單樣本才能判定官方示範解析器的風險是理論性還是實質性。
- ExcelSample.xls 除刪除「海期報價」「海選報價」兩張分頁與對應 VBA 外，RTD 行為、巨集或儲存格公式是否另有變更？需有 Excel ＋ 已註冊 SKCOM.dll 環境者實際開啟兩版比對。
- CppCLITester.vcxproj 新增的 Interop.SKCOMLib HintPath 指向出貨包中不存在的 ..\x64\Debug\（Release 組態亦指向 Debug 路徑）——官方是預期使用者自行 tlbimp 產生互通組件，還是打包遺漏？《1.環境設置》仍寫 VS2019 而專案已升 v143（VS2022），官方建議的最低 IDE 版本為何？
- SKCOMTester/SKReply.cs 移除 OnComplete／OnNewData 雙帳號分流是官方刻意簡化還是編輯疏漏？同檔其他事件分流仍在、Designer 也仍保留 listNewMessage2 與 lblSignalReplySolace2 控制項。需實機雙帳號登入觀察 listNewMessage2 是否永遠為空以佐證。
- .59 包內 3.登入.md 與主手冊對「海外行情同意書一定查詢」說法互相矛盾（分冊已刪、主手冊 L232 仍保留），實際元件行為是否已改為「已簽署即不再查詢」？若已改，海外行情同意書在已簽署狀態下是否會落入 1075 SK_ERROR_ALL_AGREEMENT_SIGNED 早停路徑？同樣地，《1.環境設置》已更正 regsvr32 敘述但 .59 合輯手冊附錄 A（第 4956-4957 行）仍是舊字串，哪一份為官方定稿？
- 元件/x86/install.bat 只對 Windows 5.1.x（XP）走直接註冊，其餘一律呼叫 %systemroot%\SysWoW64\regsvr32.exe；在「32 位元的 Windows 7／10」上並無 SysWoW64 目錄，此腳本與 .59 新敘述是否會註冊失敗？官方是否仍支援 32 位元 OS？
- 本次僅比對符號名稱集合，未驗證既有 COM 方法的參數型別／順序／vtable 位置是否變動；若要保證 binary 相容，需以 ildasm／tlbexp 對兩版 Interop 做簽名級比對。
- 2.導覽.md 的 SKQuoteLib 功能摘要表未列「即時分K」是編輯疏漏，還是官方認定其歸屬既有的「技術分析」列？影響本庫在導覽層要不要自行補列。

## 附錄 A：54 筆變更總表

狀態：✔ 通過三視角驗證；✗ 淘汰；⚠ 稽核撤銷。欄位值已套用驗證階段的多數修正。

| ID | 領域 | 類型 | 影響 | 評價 | 狀態 | 變更 |
|---|---|---|---|---|---|---|
| C-01 | 國內報價 | API 新增 | 可選用 | 優化 | ✔ | [2.13.59 功能新增] 即時分K 整組 API：SKQuoteLib_GetLiveKLineLONG + OnNotifyLiveKLineData + SKKLINE 結構 |
| C-02 | 國內報價 | API 變更 | 建議跟進 | 優化 | ✔ | [2.13.59 連帶異動] SKQuoteLib_RequestTicks 訂閱內容擴充：同一次訂閱除 Tick／五檔外還會推送即時分K |
| C-03 | 國內報價 | API 新增 | 可選用 | 優化 | ✔ | [2.13.59 功能新增] SKQuoteLib_EnterMonitorLONGByMarket(nMarketType)：可指定只訂閱國內證券或國內期貨市場 |
| C-04 | 國內報價 | API 新增 | 無 | 不明 | ✔ | [未公開] 新增 SKQuoteLib_ExportStockList（Interop 與原生 DLL 都有，官方手冊與範例皆無） |
| C-05 | 國內報價 | 行為修正 | 無 | 優化 | ✔ | [2.13.58 功能修正 3] 修正訂閱國內行情「價差商品」問題 |
| C-06 | 國內報價 | 行為修正 | 無 | 優化 | ✔ | [2.13.58 功能修正 4] 修正訂閱國內報價慢的問題 |
| C-07 | 國內報價 | 行為修正 | 建議跟進 | 優化 | ✔ | [2.13.58 功能修正 6] 修正「只有全盤商品的 T 盤需要加 AM」的報價行為 |
| C-08 | 國內報價 | 行為修正 | 無 | 優化 | ✔ | [2.13.58 功能修正 7] 修正 SKQuoteLib_GetStockByNoLONG 以商品代號取商品物件的問題 |
| C-09 | 國內報價 | 行為修正 | 建議跟進 | 優化 | ✔ | [2.13.59 功能修正] 修正國內報價第一筆 Ticks 缺失 |
| C-10 | 國內報價 | 行為修正 | 無 | 優化 | ✔ | [2.13.59 功能修正] 修正內期夜盤收盤價未更新問題 |
| C-11 | 國內報價 | 行為修正 | 建議跟進 | 不明 | ✔ | [2.13.59 功能修正] 修正 index 不一致問題（模組未指名） |
| C-12 | 下單/帳務 | API 新增 | 可選用 | 優化 | ✔ | [2.13.58 功能異動 1] 新增事件 OnOpenInterestJson：內期未平倉一次以 JSON 陣列回傳所有庫存 |
| C-13 | 下單/帳務 | API 變更 | 必改 | 優化 | ✔ | [2.13.58 功能異動 2] 內／外期未平倉「查無庫存」回傳格式改變：新增 Account 欄位，統一為「001,查無資料,帳號」 |
| C-14 | 下單/帳務 | API 變更 | 建議跟進 | 優化 | ✔ | [2.13.59 功能新增] 國內未平倉 GetOpenInterestGW 新增欄位「商品－下單代碼」，查詢異常時該欄回空值需重呼叫 |
| C-15 | 下單/帳務 | API 新增 | 可選用 | 優化 | ✔ | [未列 changelog] 新增內／外期權益數查詢狀態事件 OnFutureRightsStatus 與 OnOverseaFutureRightsStatus |
| C-16 | 下單/帳務 | API 變更 | 建議跟進 | 優化 | ✔ | [未列 changelog] 未平倉 GW 查詢狀態事件的觸發來源擴大到非-GW 版查詢函式 |
| C-17 | 下單/帳務 | API 新增 | 可選用 | 不明 | ✔ | [未公開] 新增未平倉明細查詢 GetOFOpenInterestWithDetails 與內／外期兩個 *WithDetails 事件（11 個符號） |
| C-18 | 下單/帳務 | 行為修正 | 無 | 優化 | ✔ | [2.13.58 功能修正 1] 修正證券智慧單 CB 單 SendStockStrategyCB 發生 SK_ERROR_MARKET_OUT_OF_RANGE |
| C-19 | 下單/帳務 | 行為修正 | 無 | 優化 | ✔ | [2.13.58 功能修正 2] 國內期貨未平倉 Parse 失敗時新增例外處理 |
| C-20 | 下單/帳務 | 行為修正 | 建議跟進 | 優化 | ✔ | [2.13.59 功能新增] 自營帳號即時庫存查詢 GetRealBalanceReport 補上「昨日庫存」值（欄位表本來就有） |
| C-21 | 下單/帳務 | 純文件 | 建議跟進 | 優化 | ✔ | [2.13.58 文件調整 1] 全面改寫下單／改單／刪單／Proxy 函式的「回傳值」語意，並新增「委託成功＝送達交易所」強調備註 |
| C-22 | 下單/帳務 | 純文件 | 必改 | 優化 | ✔ | [未列 changelog] Proxy 函式的結果回呼由 OnAsyncOrder 更正為 OnProxyOrder（SendForeignStockProxyCancel、SendStockProxyPreAlter） |
| C-23 | 回報 | API 變更 | 建議跟進 | 不明 | ✔ | [2.13.59 功能新增] 國內期選主動回報 OnNewData 新增欄位「下單時間 HH:mm:ss.fff」，但欄位表未同步更新 |
| C-24 | 回報 | 行為修正 | 建議跟進 | 優化 | ✔ | [2.13.59 功能修正] 修正複委託回報未給時效欄位時，改回傳空值給前端 |
| C-25 | 回報 | 行為修正 | 無 | 優化 | ✔ | [2.13.59 功能修正] 修正主動回報連線多帳號時無法斷線的問題 |
| C-26 | 回報 | 行為修正 | 建議跟進 | 優化 | ✔ | [2.13.59 功能修正] 修正智慧單被動回報缺少逗號問題 |
| C-27 | 回報 | 行為修正 | 無 | 優化 | ✔ | [2.13.58 功能修正 5] 修正 SGX 專線主動回報缺漏問題 |
| C-28 | 回報 | 範例變更 | 無 | 退步 | ✔ | SKCOMTester/SKReply.cs 範例的 OnComplete／OnNewData 失去雙帳號分流，第二組帳號訊息會誤寫進第一組清單 |
| C-29 | 海外期選 | API 新增 | 可選用 | 優化 | ✔ | [2.13.58 功能新增 1] 新增海選下單函式 SendOverseaOptionOrderOLID（可帶自訂資料欄 bstrOrderLinkedID） |
| C-30 | 海外期選 | 純文件 | 無 | 中性 | ✔ | 9.下單-海外期選：LoadOSCommodity／LoadOOCommodity 備註刪減，失去錯誤碼 2015 排除方法與 LOG 檢查說明 |
| C-31 | 登入/中心 | 錯誤碼 | 建議跟進 | 優化 | ✔ | [2.13.59 功能新增] 新增錯誤代碼 9996 SK_ERROR_UPDATE_API_REQUIRED（同批把 9997／9998 補進代碼表） |
| C-32 | 登入/中心 | 純文件 | 無 | 不明 | ✔ | [未列 changelog] 同意書查詢移除「海外行情同意書一定查詢」特例，並自特殊商品說明刪去海外行情字樣 |
| C-33 | DLL/包裝層 | API 新增 | 可選用 | 優化 | ✔ | 簡化版 SKDLLCSharp 包裝層新增 5 個帳務／未平倉查詢方法，SKDLLTester 同步新增示範入口 |
| C-34 | DLL/包裝層 | API 移除 | 破壞性 | 退步 | ✔ | 簡化版 SKDLLCSharp 包裝層移除「海期行情」「海選行情」整組報價方法（底層 COM 完好無損） |
| C-35 | DLL/包裝層 | API 變更 | 破壞性 | 退步 | ✔ | 簡化版 ManageServerConnection 移除海期／海選連線目標（nTargetType 2、3）與 nStatus 4 備援選項 |
| C-36 | DLL/包裝層 | 行為修正 | 建議跟進 | 優化 | ✔ | [2.13.59 功能修正] 修正簡化版 API 下單成功回傳值 |
| C-37 | DLL/包裝層 | API 新增 | 可選用 | 優化 | ✔ | Interop.SKCOMLib.dll 是純新增：39 加 0 減，.57 的呼叫程式碼可直接重編 |
| C-38 | DLL/包裝層 | API 變更 | 無 | 中性 | ✗ 淘汰（strings 假象） | 原生 SKCOM.dll 唯一被移除的匯出 SKQuoteLib_DeltaT 改名為 SKQuoteLib_Delta；走 COM 者不受影響 |
| C-39 | DLL/包裝層 | API 新增 | 無 | 中性 | ⚠ 撤銷（strings 假象，見 1.1） | 原生 SKCOM.dll 多出 8 個 SKOOQuoteLib_* flat export，對應 COM 方法 .57 早就有——非 API 新增 |
| C-40 | DLL/包裝層 | 純文件 | 建議跟進 | 不明 | ✔ | C_Sharp策略王DLL元件使用說明 三則文件品質問題：宣告簽章與範例不符、範例多寫 +1、文件版號標 2.13.58 |
| C-41 | DLL/包裝層 | 範例變更 | 建議跟進 | 中性 | ✔ | SKDLLTester 登入流程新增 UI 索引→nTargetType 的補償映射，以配合下拉選單移除海期／海選兩項 |
| C-42 | 範例包 | 範例移除 | 無 | 中性 | ✔ | 四個範例專案全面移除「海期報價」「海選報價」示範（COM 介面完好保留） |
| C-43 | 範例包 | 範例變更 | 無 | 退步 | ✔ | SKCOMTesterV2 MainForm 殘留兩顆無文字、無事件的空白按鈕（海期／海選功能拔除不徹底） |
| C-44 | 範例包 | 範例變更 | 建議跟進 | 中性 | ✔ | 即時分K 官方範例把價格固定除以 100，未依商品小數位還原 |
| C-45 | 範例包 | 範例變更 | 無 | 中性 | ✔ | OnOpenInterestJson 官方範例以手刻字串切割解析，非真正 JSON 解析 |
| C-46 | 範例包 | 範例變更 | 無 | 退步 | ✔ | ExcelSample：官方 .xls 活頁簿已更新（內容無法文字比對）；.57 樹中的 Program.cs 屬使用者自建檔案而非官方範例 |
| C-47 | 範例包 | 範例變更 | 無 | 中性 | ✔ | CppCLITester.vcxproj 建置工具鏈升級（v142→v143、WindowsTargetPlatformVersion、新增顯式 Interop.SKCOMLib 參照） |
| C-48 | 官方文件 | 純文件 | 建議跟進 | 退步 | ✔ | [未列 changelog，本次最大文件變更] 官方文件全面移除海期／海選報價：主手冊 4-5／4-6 章與 4 個結構、導覽物件說明與架構圖、14/15 兩份分冊 |
| C-49 | 官方文件 | 純文件 | 建議跟進 | 退步 | ✔ | [未列 changelog] 刪除 3-3 行情連線數限制、3-3-1／3-3-2 使用說明、3-4 行情功能修改說明（SHORT 32767 新舊對照表） |
| C-50 | 官方文件 | 純文件 | 無 | 不明 | ✔ | [未列 changelog] 舊版本歷程條目被回溯改寫，刪去所有海期／海選報價字樣（含 2.13.46 的移除範圍訂正） |
| C-51 | 官方文件 | 純文件 | 建議跟進 | 退步 | ✔ | [文件缺陷] .59 手冊出現不存在的 RequestLiveKLine，且 2.13.31 交叉引用指向已刪的 3-4 章節 |
| C-52 | 官方文件 | 純文件 | 無 | 中性 | ✔ | 主手冊文件版本由 V2.13.57 升至 V2.13.59，版本歷程表新增 2.13.58／2.13.59 兩列 |
| C-53 | 官方文件 | 純文件 | 無 | 中性 | ✔ | 2.導覽 的 SKQuoteLib 功能摘要表未同步反映新增的「即時分K」與「指定市場連線」 |
| C-54 | 其他 | 純文件 | 無 | 優化 | ✔ | 1.環境設置：DLL 註冊的 x86/x64 與 regsvr32 對應關係修正 |

## 附錄 B：驗證階段套用的欄位修正

- C-01：[spec] detail 補充：三個維度全部成立（版本歸屬 2.13.59 無誤，2.13.58 changelog 未提及，.57 全套文件零命中）。手冊有：主手冊新增 4-4-35（函式）、4-4-u（事件）、5-26（結構），13.國內報價.md 同步新增，2.13.59 changelog 明列「即時分K SKQuoteLib_GetLiveKLineLONG」。DLL 有：sym57→sym59 新增 SKQuoteLib_GetLiveKLineLONG / OnNotifyLiveKLineData / SKKLINE / _ISKQuoteLibEvents_OnNotifyLiveKLineDataEventHandler / add_/remove_/m_*Delegate / pSKKLine / nTimehm / nType。範例有：SKQuote.cs 註冊事件、實作 handler（含自組 5 分K 示範）、呼叫 GetLiveKLineLONG，Designer 新增 groupBox8/txtLiveKLineStockNo/buttonGetLiveKLine/listBoxLiveKLine/listBoxLiveKLine_5（掛在既有 tabPage2，未新增頁籤）。【訂閱前置（原 detail 遺漏，實作必讀）】即時分K 沒有獨立訂閱函式：訂閱一律走既有的 SKQuoteLib_RequestTicks，其官方說明在 .59 由「訂閱要求傳送成交明細以及五檔」擴充為「訂閱要求傳送成交明細、五檔、即時分K(包含當日分K回補)、該分鐘每筆Tick更新一次分K」，「相關通知事件」亦加上「即時分K…由OnNotifyLiveKLineData事件通知」；此外須先 SKQuoteLib_EnterMonitorLONG 並等 OnConnection 收到 SK_SUBJECT_CONNECTION_STOCKS_READY。注意手冊 4-4-35 標題行寫「(需先訂閱即時分K RequestLiveKLine)取得分K資訊」，但 RequestLiveKLine 這個函式名在 sym59、原生 SKCOM.dll 匯出表、全部 .59 手冊章節與 .59 範例碼中皆不存在（應是沿用 GetBest5LONG 敘述句型的官方筆誤），請以 RequestTicks 為準。語意重點：nType 0=資料重置(清盤，本筆價格全為 0，需捨棄已收資料)、1=即時分K(先收當日回補分K，之後 1 分鐘一次)、2=該分鐘每筆 Tick 各給一次；價格為原始整數需自行依商品小數位還原；nTimehm 是不補零的 HHmm 整數(904=09:04)；不支援盤中零股與價差商品，證券以整股計算、不含「鉅額交易、盤中零股、盤後零股」；未開證券/期貨帳戶者無法取得對應市場的即時分K；官方明示避免在 OnNotifyLiveKLineData 事件內呼叫 GetLiveKLineLONG。kind 取 api_added（DLL 符號 + 官方手冊雙重證據，強度最高）。；[code] detail 補充：三個維度全部成立。DLL 有：comm -13 sym57→sym59 的 39 個新增符號中，10 個屬本功能（SKQuoteLib_GetLiveKLineLONG、OnNotifyLiveKLineData、事件委派＋add_/remove_/m_*Delegate、結構 SKKLINE，以及只隨此功能進來的參數名 pSKKLine/nType/nTimehm；sym57 僅有拼字不同的 nTimehms，非同一符號）。手冊有：主手冊新增 4-4-35（函式，diff:944）、4-4-u（事件，diff:1536，宣告與參數表在 1589-1590）、5-26（結構，diff:1860），13.國內報價.md 同步新增，2.13.59 changelog 明列「即時分K SKQuoteLib_GetLiveKLineLONG」。範例有：SKCOMTester/SKQuote.cs 註冊事件（diff:7）、實作 handler（diff:64，附自組 5 分 K 示範）、呼叫 GetLiveKLineLONG（diff:136），Designer 新增 groupBox8/txtLiveKLineStockNo/buttonGetLiveKLine/lblLiveKLineDate/lblLiveKLineOHLC/lblLiveKLineQty/listBoxLiveKLine/listBoxLiveKLine_5，掛在既有 tabPage2（Designer.cs:506-508），未新增頁籤；.57 樹 SKCOMTester/ 全無此類符號。語意重點：nType 0=資料重置(清盤，本筆價格全為 0，需捨棄已收資料)、1=即時分K(先收當日回補分K，之後 1 分鐘一次)、2=該分鐘每筆 Tick；價格為原始整數需自行依商品小數位還原（官方例：群益證 3815→38.15、台指期 4557900→45579.00，範例碼固定除以 100m 僅為示意，不可照抄）；nTimehm 是不補零的 HHmm 整數(904=09:04)；不支援盤中零股與價差商品，且未開立對應證券／期貨帳戶即無法訂閱該市場即時分K；官方明示避免在 OnNotifyLiveKLineData 事件內呼叫 GetLiveKLineLONG。額外注意：4-4-35 說明寫「需先訂閱即時分K RequestLiveKLine」，但 sym59 並無 SKQuoteLib_RequestLiveKLine 這個符號（只有既有的 SKQuoteLib_RequestLiveTick），.59 範例樹亦無任何 RequestLiveKLine 呼叫——訂閱入口在官方文件與型別庫間對不上，實作時須另行確認（範例僅做 EnterMonitor + 註冊事件）。kind 取 api_added（DLL 符號 + 官方手冊雙重證據，強度最高）。；[impact] detail 補充：三個維度全部成立。DLL 有：comm -13 sym57→sym59 的 39 個新增符號中，10 個屬本功能（函式、事件、事件委派＋add_/remove_/m_*Delegate、結構 SKKLINE，以及只隨此功能進來的參數名 pSKKLine/nType/nTimehm；sym57 僅有相異的 nTimehms）。手冊有：主手冊新增 4-4-35（函式）、4-4-u（事件）、5-26（結構），13.國內報價.md 同步新增，2.13.59 changelog 明列「即時分K SKQuoteLib_GetLiveKLineLONG」。範例有：SKCOMTester/SKQuote.cs 註冊事件、實作 handler、呼叫 GetLiveKLineLONG，Designer 新增 groupBox8/txtLiveKLineStockNo/buttonGetLiveKLine/listBoxLiveKLine/listBoxLiveKLine_5（掛在既有 tabPage2 內，未新增頁籤）。語意重點：nType 0=資料重置(清盤，本筆價格全為 0，需捨棄已收資料)、1=即時分K(含當日回補，1 分鐘一次)、2=該分鐘每筆 Tick；價格為原始整數需自行依商品小數位還原；nTimehm 是不補零的 HHmm 整數(904=09:04)；不支援盤中零股與價差商品；官方明示避免在 OnNotifyLiveKLineData 事件內呼叫 GetLiveKLineLONG。訂閱入口：手冊只在 4-4-3 SKQuoteLib_RequestTicks 的「相關通知事件」加註本事件，RequestTicksWithMarketNo 與 RequestLiveTick 未列（與「不支援盤中零股」一致）；但 4-4-35 說明欄寫的前置條件「需先訂閱即時分K RequestLiveKLine」是不存在的函式（sym59 與全部 .59 文件皆無此符號，該字串僅出現在這句話本身），照字面找會找不到 API——實際請用 SKQuoteLib_RequestTicks 訂閱，勿與「僅即時成交明細」的 SKQuoteLib_RequestLiveTick 混淆。對 .57 既有程式：不註冊事件則行為不變（純 opt_in），無回報字串欄位變動、無既有簽章異動。另注意官方範例 buttonGetLiveKLine_Click 把小數位寫死成 /100m，與手冊「需自行除以小數位數還原」相牴觸，勿直接沿用。kind 取 api_added（DLL 符號 + 官方手冊雙重證據，強度最高）。
- C-02：[code] detail 補充：detail 主體成立，但「事件處理端需能忽略未預期事件而不當成錯誤」這句無證據支撐且技術上不成立：.NET Interop（本範例的用法）未掛 OnNotifyLiveKLineData handler 時該事件根本不會被派送，不會產生錯誤。建議把 should_update 的理由改為可佐證的版本：.59 起 SKQuoteLib_RequestTicks 的同一次訂閱會額外推送即時分K（含當日分K回補與每筆Tick更新分K），且 sym59/nat59 中查無任何 RequestLiveKLine 或 Cancel*LiveKLine 之類的獨立訂閱/取消符號 → 使用者無法單獨關閉這股額外流量，既有只用 Tick/Best5 的程式會被動承受額外頻寬與事件量；若要利用則需新增 OnNotifyLiveKLineData handler（並可搭配 4-4-35 SKQuoteLib_GetLiveKLineLONG 主動取值）。另可補一句原文限制：OnNotifyLiveKLineData 備註載明「不支援盤中零股」「不支援價差商品」「價格為原始價格需自行除以小數位數還原」。
- C-04：dev_impact: opt_in → none（2 個視角同意）；[impact] title 建議：[未公開] 新增 SKQuoteLib_ExportStockList(short sMarketNo) → int（Interop 有簽章、原生 DLL 有符號，官方手冊/changelog/範例皆無）
- C-07：[code] detail 補充：DLL 無可見符號差異（SKQuoteLib_RequestTicks / SKQuoteLib_RequestStocks 在 sym57/sym59 與 nat57/nat59 皆存在且無增減）／範例無（全部原始碼 diff 無任何含 AM 的變更行）／唯一依據為手冊 .59 新增的 2.13.58 版本歷程列「6、修正報價只有全盤商品的T盤需要加AM」。既有規則：部分期選商品有 T+1 盤，商品代號加 AM 才能取得純 AM 盤行情（例 TX00AM），且加 AM 無法下單；此規則段落同時出現在 4-4-2 SKQuoteLib_RequestStocks 與 4-4-3 SKQuoteLib_RequestTicks 的備註。需注意：這兩處備註文字在 .57→.59 完全未修改，官方並未在功能說明處補述新範圍，因此「必須加 AM 的商品範圍縮小為全盤商品」是對版本歷程單句的解讀，非手冊明載；實際差異落在 DLL/報價伺服器內部行為，無法由符號或範例佐證。實務建議：若既有程式無條件對所有商品代號加 AM，換版後請重新確認訂閱代號組法與收到的盤別資料。
- C-11：[spec] detail 補充：DLL 無可見符號差異／手冊有（版本歷程表，措辭極含糊）／範例無。主手冊《策略王COM元件使用說明_V2.13.59.md》第 38 行 2026/08/03 / 2.13.59 列「二、功能修正」中僅一句「修正index不一致問題」，未指名模組；《2.導覽.md》第 172 行有同文字重複列。已逐檔驗證：raw59 全部文件本文（含 13.國內報價.md 的 SKQuoteLib_GetStockByIndexLONG 宣告、SKSTOCKLONG 物件、OnNotifyQuoteLONG 的 nStockidx 備註）在 .57→.59 之間完全未修改，故手冊本文確無對應敘述。注意：本項的 area="quote" 與 symbols（SKQuoteLib_GetStockByIndexLONG / SKSTOCKLONG）純屬字面聯想，官方文件無任何指向，建議清空 symbols 或明確標記為「推測，未經證實」，以免下游誤讀為已確認受影響的符號。可辨識的參考點是相鄰版本 2.13.58 有一項不同但相近的修正「修正GetStockByNoLONG商品代號取物件問題」（主手冊第 37 行），顯示廠商近期確在處理報價取物件相關議題，但該項屬 .58 而非 .59，不可用來替本項背書。實務建議維持保守：若程式會快取索引值再回頭查物件，換版後重新驗證即可，不需預先改碼。；[impact] detail 補充：僅有 changelog 一行「修正index不一致問題」，未指名模組；API 表面無任何佐證：sym57 與 sym59 對 SKQuoteLib_GetStockByIndexLONG(315→326)、SKQuoteLib_GetStockByNoLONG(318→329)、SKSTOCKLONG(347→358)、pSKStockLONG(1130→1160) 皆存在且無刪除項，手冊本文（13.國內報價.md、主手冊）在這些條目上零變動，範例碼亦無對應修改（diffs 內唯一 index 命中是 SKQuoteLib_EnterMonitorLONGByMarket 用的市場別下拉 SelectedIndex，與商品索引無關）。symbols 欄列的 SKQuoteLib_GetStockByIndexLONG / SKSTOCKLONG 屬推測性歸屬，非證據所指。可回溯的相鄰事實：57→59 之間還會一併吃到 2.13.58 的「修正GetStockByNoLONG商品代號取物件問題」（raw59/2.導覽.md 版本歷程），同屬報價取物件路徑。因應方式：不需改呼叫方式，但若程式會快取 nStockidx（或商品清單順序）再回頭以 GetStockByIndexLONG/GetStockByNoLONG 取物件，換版後應重新驗證索引與物件的對應，並避免跨 EnterMonitorLONG 週期沿用舊索引。
- C-13：[impact] detail 補充：DLL 無可見符號差異（純回傳字串格式變更）／手冊有（changelog＋主手冊四處備註：4-2-d OnOpenInterest 不含格式、OnOpenInterestWithFormat、GW 版，以及 4-2-e OnOverseaFutureOpenInterest＋分冊 7.下單-國內期選.md）／範例有間接佐證（原判「範例無」需修正）：.59 新增的 SKCOMTester/SKOrder.cs 在 OnOpenInterestJson 處理器以 bstrData.Contains("001,查無資料") 判斷查無資料，該檔／該處理器在 .57 樹不存在，屬程式碼級佐證新字串確實上線。主手冊本文：OnOpenInterest（不含格式）由「回傳M003 NO DATA#」改為「001,查無資料,帳號」；GW 版由「001 查無資料」（空白分隔）改為「001,查無資料,帳號」（逗號分隔、3 欄）；OnOpenInterestWithFormat 與 OnOverseaFutureOpenInterest 則是新增這句備註（.57 原本沒寫）。dev_impact 取 must_change（證據強度：官方 changelog > 文件 diff），但破壞面有條件性，跟進時應據此定位風險：(a) 以字串比對「M003 NO DATA」判斷查無資料者 → .59 偵測不到，落入正常解析路徑，3 欄字串對 10~40 欄的索引存取會 IndexOutOfRange 或產生假部位列（本 repo api_spec/modules/SKOrderLib.md:1235、:2554 目前正是這樣寫，需同步更新）；(b) GW 版原以空白格式「001 查無資料」整串比對者 → 新格式為逗號分隔，比對失效；(c) 照抄官方 .57 範例 TFReadOrderForm.cs:167「values[0]=="001"」者不受影響，因 split 後 values[0] 仍為 "001"。建議一律改為「首欄 == 001 或整串含『查無資料』」的容錯判斷，勿依固定欄位數。附註：.59 文件對新事件 OnOpenInterestJson 的查無資料格式自相矛盾（主手冊 4-2-x 寫「001,查無資料,帳號」，分冊 7.下單-國內期選.md 與官方範例碼寫「001,查無資料」無帳號），故「統一」僅適用於 changelog 明列的四個 Get* 函式，OnOpenInterestJson 不可假設帶 Account 欄位。
- C-14：dev_impact: must_change → should_update（2 個視角同意）
- C-16：[code] detail 補充：DLL 無可見符號差異（OnOpenInterestGWStatus / OnOverseaFutureOpenInterestGWStatus 及七個相關符號在 sym57.txt、sym59.txt 皆各出現一次，nat57/nat59 無 OpenInterest 匯出）／手冊有（主手冊 4-2-t 與 7、9 兩份分冊改寫）／範例無驗證（兩版 SKOrder.cs 的 GWStatus 註冊行皆為 diff context 行，.59 僅新增 OnFutureRightsStatus、OnOverseaFutureRightsStatus、OnOpenInterestJson 註冊）。主手冊 4-2-t 由「透過呼叫 GetOpenInterestGW 後」改為「GetOpenInterestGW、GetOpenInterest、GetOpenInterestWithFormat 後」；分冊 7.下單-國內期選.md 為同義改寫（「GetOpenInterestGW 或GetOpenInterest 或 GetOpenInterestWithFormat」）；9.下單-海外期選.md 對 OnOverseaFutureOpenInterestGWStatus 擴為「GetOverseaFutureOpenInterestGW或GetOverseaFutureOpenInterest」。需補註兩點：(1) 主手冊 4-2-u（OnOverseaFutureOpenInterestGWStatus，raw59 第 1981 行）仍維持舊寫法「透過呼叫 GetOverseaFutureOpenInterestGW後」，只有分冊 9 被改，官方文件本身前後不一致，海期側的擴大觸發僅有分冊單一來源。(2) 此變更很可能實際發生於 2.13.58 而非 .59：raw59 版本歷程列 2.13.58 才新增 OnOpenInterestJson，而 OnOpenInterestJson（raw59 第 2005 行）的觸發來源即為三個函式；.57→.59 的 doc diff 只是把跨兩版的累積差異一次呈現。.58 與 .59 的 changelog 均未提及 GWStatus 觸發來源擴大，「未列 changelog」成立。影響：原本「收到 *GWStatus 就代表是 GW 查詢」的狀態旗標判斷會誤判。同一 hunk 另把 4-2-t／4-2-u 的空白備註欄填成「成功與失敗」（無資訊量）。無法從文件或符號斷定 .57 呼叫非-GW 版時是否完全不觸發該事件，實際行為建議實測。
- C-17：[impact] detail 補充：DLL 有／手冊無／範例無，且為 .59 真正新增（非 .57 已有僅未暴露）：`strings -a 2.13.57/元件/x64/SKCOM.dll | grep WithDetails` 0 命中，.59 同檔 11 處命中。39 個 Interop 新增符號中 11 個屬此組：GetOFOpenInterestWithDetails，配上 _ISKOrderLibEvents 的 OnOpenInterestWithDetails（國內期選）與 OnOverseaFutureOpenInterestWithDetails（海期），各帶 EventHandler＋add_/remove_/m_*Delegate。.59 相對 .57 零符號移除，既有 GetOpenInterest／GetOpenInterestGW／GetOpenInterestWithFormat／GetOverseaFutureOpenInterest(GW)／OnOpenInterest／OnOverseaFutureOpenInterest／*GWStatus／OnOFOpenInterestGWReport 全數保留，故純屬追加。相容性：typelib 名稱堆疊顯示六個新事件全聚在 _ISKOrderLibEvents 尾端、GetOFOpenInterestWithDetails 亦為 ISKOrderLib 區塊最後一個名字，屬尾端追加，舊 DISPID/vtable 不位移，既有 .NET event handler 與 C++ 硬編 DISPID 的 sink（CppTester/SKOrderLib.cpp 用 `switch (dispidMember) case 1/2`）不受影響 → 對 .57 既有程式零強制動作。回傳格式已有線索但仍不足以照著寫：SKCOM.dll 內有 `RequestOFOpenInterestWithDetails result JSON data error`／`... Query result is`／`... exception`，與既有 `RequestOFOpenInterestGW` 系列並列，可判定走 GW 查詢通道、結果為 JSON；typelib 字串表中新事件後未出現新參數名，推測沿用既有 BSTR bstrData 單參數（此為推測，非確證）。與 OnOpenInterestJson 的分工亦有線索：後者手冊明載為內期未平倉一次回傳所有庫存(JSON，raw59/7.下單-國內期選.md:719-722；2.13.58 changelog)，WithDetails 組看似海期(OF)對應版本。惟有一處不對稱需留意：sym59 中只有 GetOFOpenInterestWithDetails（海期）一個查詢函式，並無 GetOpenInterestWithDetails，但卻存在內期 OnOpenInterestWithDetails 事件，故這 11 個符號未必構成完整一套，內期事件觸發路徑不明。對照組可證此組確為刻意未公開而非文件抽取遺漏：同批新增的 OnOpenInterestJson 有 changelog、有手冊條目、.59 範例也確實掛上（SKCOMTester/SKOrder.cs 新增 `m_pSKOrder.OnOpenInterestJson += ...`），而 WithDetails 三邊皆 0。建議規格庫標「未公開、勿用」。
- C-20：dev_impact: none → should_update（2 個視角同意）
- C-22：[spec] detail 補充：DLL 無符號差異／手冊有（主手冊 4-7-16 SendForeignStockProxyCancel 與分冊 11.下單-複委託.md 同一支函式，共兩處）／範例無。.57 寫「使用非同步委託，委託結果請由OnAsyncOrder取得。／在有連上且成功登入proxy server的狀態下，會由proxy server進行非同步下單。」，.59 改為與其他 Proxy 函式一致的「透過Proxy Server委託，委託結果請由OnProxyOrder取得。／連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。」。SendStockProxyPreAlter 不屬於本項：其備註在 .57 主手冊 4-7-17 與分冊 5.下單-國內證券.md 中原本就已寫 OnProxyOrder，.59 未更動該句（僅同步套用全書「回傳值」欄改寫與「*此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認」註記）。symbols 應修正為 SendForeignStockProxyCancel、OnProxyOrder、OnAsyncOrder、OnProxyStatus（移除 SendStockProxyPreAlter）。此函式本走 Proxy 通道，屬文件對齊實際行為的更正；依 .57 文件字面只掛 OnAsyncOrder 的程式收不到通知，故 dev_impact=must_change（但注意 DLL 無變更，此破損早於 .59 即存在，.59 只是揭露）。歸屬提醒：整批備註/回傳值改寫由主手冊版本歷程 2.13.58「四、文件調整：1、調整下單函式文件說明」涵蓋，較可能是 2.13.58 而非 2.13.59 的異動；該條未點名本次 OnAsyncOrder→OnProxyOrder 更正。殘留不一致：ProxyServer 下單元件分冊（文件版本仍為 V2.13.47，.57→.59 只有標頭差異）第 187 行仍保留舊的 OnAsyncOrder 寫法，依該分冊開發者仍會被誤導。
- C-23：dev_impact: must_change → should_update（3 個視角同意）；[code] detail 補充：DLL 符號零差異（sym57/sym59 的 OnNewData、_ISKReplyLibEvents_OnNewDataEventHandler、add_/remove_OnNewData、m_OnNewDataDelegate 逐行相同；.59 新增符號中無任何回報相關項目），事件簽章不變，只是回報字串多一欄。手冊只有 changelog 一行：主手冊 4-3-g OnNewData 區段（.57 L2172-2200 vs .59 L2108-2136）逐行相同，12.回報.md 兩版全檔（544 行 / 112339 bytes）僅差抽取器寫入的來源路徑行，回報欄位表零異動。全部 raw 文件 grep「下單時間」只命中 2 個 changelog 表格與 12.回報.md 第 531 行的 C# DataGridView 欄位標題（屬智慧單回報 TSMST/TFSTP 等格線，非 OnNewData 欄位定義，且 .57/.59 該檔案位元組相同）。範例方面需修正原宣稱的「範例無」：SKCOMTester/SKReply.cs 的 OnNewData handler 在 .57→.59 確有變更，但只是移除第二登入帳號分支（對應 changelog「修正主動回報連線多帳號，無法斷線問題」），與新欄位無關；真正的固定索引解析範例 SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs 兩版完全相同，仍宣告 string[48]、逐一取 values[0]~values[47]（values[48] OFSTPFlag 保持註解狀態）、且無 values.Length 長度防護，官方並未為新欄位更新任何範例。結論：官方沒說新欄位插在第幾欄，也沒有任何範例或欄位表可反推位置。若欄位附加在尾端（index 48），既有 values[0..47] 解析仍可運作；若插在中段則整批錯位。因無證據證實必然損壞，dev_impact 定為 should_update（回報多欄位、解析器需容錯：改用長度防護、勿假設固定欄數），並建議在模擬環境實測欄位序列後再固定索引；assessment 維持 unclear（新增欄位本身有利，但缺欄位位置使影響無法評估）。；[impact] detail 補充：DLL 無符號差異（事件簽章不變，只是字串多一欄）／手冊只有 2.導覽 與主手冊 changelog 各一行／範例未反映新欄位。官方對 OnNewData 從來沒有獨立欄位定義表：主手冊 4-3-g 與 12.回報.md 4-3-g 都只有「宣告／參數（每一筆資料以「,」分隔每一個欄位）／備註」加一張 V2.13.45 修改比較表；欄位序的唯一權威來源是 12.回報.md 內嵌 C# 範例（values[0] KeyNo … values[44] ErrorMsg、values[45] CancelOrderMarkByExchange、values[46] ExchangeTandemMsg、values[47] SeqNo、//values[48] OFSTPFlag 註解掉），而該範例對應的 SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs 兩版 byte-identical，12.回報.md 的 .57→.59 diff 也只有檔名標題行一個 hunk。兩處 grep「下單時間」的命中皆為智慧單 DataGridView 欄位標題（TSMST Column24／TFSTP Column22），.57 已存在，與本次無關。

影響評估：changelog 明寫「國內期選」，僅涉 MarketType TF/TO（R2），純證券解析器不受影響。群益對 OnNewData 的既有慣例是 append 到末欄——2.13.38「主動回報4-3-g OnNewData最後一欄加入欄位SeqNo」、2.13.40 的海期停損觸發註記落在 index 48——若沿慣例，讀固定索引 0~47 的解析器（含官方範例本身）不會錯位。真正會壞的是「欄位數必須恰好等於 N」的嚴格驗證或 tuple-unpacking 式解析。故 dev_impact=should_update：解析器改為「至少 N 欄」的容錯寫法即可（本庫 api_spec/modules/SKReplyLib.md:392 早已如此建議），並建議在模擬環境實測一次確認新欄位確實在末端。
- C-24：[spec] detail 補充：DLL 無符號差異／手冊有（僅主手冊版本控管表 2.13.59 格「二、功能修正」條列，對應章節無異動）／範例無。官方僅載明「修正當複委託回報沒給時效欄位時，需給前端空值」，未說明修正前的實際行為（缺欄、null 或保留舊值皆有可能），亦未指明落在哪一支回報的第幾欄——12.回報.md 的 OnNewData 48 欄位表無「委託時效」欄（第 42 欄為 OrderEffective 有效委託日），且該檔 .57→.59 內容零變更。實務上解析複委託回報時效欄位應容忍空字串，不可假設必有 ROD/IOC/FOK 值；欄位錯位風險無文件依據，不宜斷言。symbols 建議收斂：SendForeignStockOrder 為下單函式（主手冊 4-2-15），與回報欄位修正無直接關聯；OnNewData 亦未經文件證實承載該欄。；[impact] detail 補充：僅有文件證據（2.13.59 版本控管表一行），DLL 符號無差異、範例程式 ReplyForm.cs 逐字未改、12.回報.md 欄位規格亦未改版。複委託回報走 OnNewData（bstrData 為逗號分隔字串，市場種類 OS），因此空值化的影響落在既有的逗號分割解析器上：官方原文只說『需給前端空值』，未載明修正前是整欄缺漏（升級後逗號數增加→位移）還是給了非空的錯值（升級後僅該欄變空），兩種讀法皆無法由文件或程式碼判定，建議 .57 既有程式在升級 DLL 後實測一筆複委託回報再定案。解析器至少須容忍該位置為空字串（勿直接 int.Parse/enum 轉換）。注意：複委託本身沒有時效輸入欄位（FOREIGNORDER 與 OSSTOCKPROXYORDER 皆無此成員，範例 OS 回報 24 欄亦無時效欄），文件中 ROD/IOC/FOK 的時效列舉屬證券/期選智慧單與海期兩腳單，不應套用到複委託；受影響符號為回報事件 OnNewData（ProxyServer 路徑為 OnProxyOrder），SendForeignStockOrder 只是所屬領域、非受影響符號。
- C-25：[spec] detail 補充：DLL 無符號差異（sym57/sym59 中 ReplyLib 相關 23 筆完全相同）／手冊有（僅 2.13.59 版本控管表「二、功能修正」條列，4-3 SKReplyLib 章節內文與函式表在 .57→.59 完全未動，未指名函式）／範例無。影響同時掛多個帳號回報連線的程式在關閉／重連時的資源釋放；此前可能斷不掉、重連疊加。symbols 為依語意推定、手冊未指名；且依 .59 手冊 4-3-2，SKReplyLib_CloseByID 已標註為「配合2018 舊主機下線，舊客戶串接保留使用，新用戶請直接參考4-3-4 SKReplyLib_SolaceCloseByID」，故現行斷線路徑應以 SKReplyLib_SolaceCloseByID 為主要推定符號（搭配 OnSolaceReplyDisconnect 通知事件）。
- C-27：[impact] detail 補充：DLL 無符號差異（sym57/sym59、nat57/nat59 中無 SGX 相關符號增減）／手冊僅版本歷程表有此列／範例包 SGX 相關行皆為 context 行、無變更。屬 DLL 內部行為修正，不需改呼叫方式，但對 SGX DMA 專線使用者非「不影響」：手冊各 SGX DMA 下單/改價/減量/刪單函式備註均寫明「*實際委託成功與否，請以專線回報資料為主」「詳細委託狀態仍須以委託回報內容為主」，專線主動回報即委託狀態的權威來源，.57 的缺漏代表既有程式可能漏單，需換版才修正，且換版後回報由缺漏轉為完整，若原本以輪詢/對帳補洞應複查是否重複處理 → 建議跟進。解析格式無風險：4-3-g-2（SGX DMA）OnNewData 章節 .57 與 .59 逐行相同，未新增欄位（.59 新增「下單時間HH:mm:ss.fff」者為國內期選 OnNewData，屬另一項）。symbols 標 OnNewData 為推斷：手冊未指明事件，SGX 專線回報面另有 OnNotifySGXAPIOrderStatus，兩者符號於 .57/.59 皆存在且無差異。
- C-28：[spec] detail 補充：DLL 無（事件簽章未變）／手冊無此條目／範例有（且屬回歸）。.57 版 OnComplete 依 strUserID 等於 m_strLoginID 或 m_strLoginID2 分別寫入 listNewMessage/lblSignalReplySolace 或 listNewMessage2/lblSignalReplySolace2；OnNewData 同樣分流。.59 把兩個 if/else if 整段拿掉，一律寫入 listNewMessage，且 OnComplete 改為無條件把 lblSignalReplySolace（第一組號誌）點綠——第二組帳號回補完成時會誤點亮第一組燈號，第二組燈號永遠停在 OnSolaceReplyConnection 設的黃色。範圍限縮：lblSignalReplySolace2 在 .59 仍被 OnSolaceReplyConnection(222)、OnSolaceReplyDisconnect(233) 使用，「不再被使用」僅適用於 OnComplete/OnNewData 這兩個 handler。同檔其他事件（OnSmartData 171-173、OnStrategyData 183-185、OnMessage 199、OnClearMessage 244-247）的雙帳號分流仍在，屬範例內部不一致。動機判定保留兩種可能：可能是編輯疏漏，也可能是向官方手冊靠攏——12.回報.md 官方自附的 OnComplete/OnNewData C# 範例本來就只寫進單一 richTextBoxMessage／單一 dataGridView，不做 ID 分流。版本歸屬：官方主手冊 2.13.58 與 2.13.59 版本歷程皆無此條目（.59 唯一沾邊的是「修正主動回報連線多帳號，無法斷線問題」，講連線非分流），故只能認定為 .57→.59 兩個發布包之間的異動，無法區分是 .58 還是 .59 所改。不涉及 COM 事件本身，dev_impact=none；assessment=regression 是就「複製此範例作為雙帳號回報起始模板」的開發者而言。；[spec] title 建議：SKCOMTester/SKReply.cs 範例的 OnComplete／OnNewData 失去雙帳號分流，第二組帳號訊息會誤寫進第一組清單（.57→.59 之間，官方文件未載）
- C-30：[code] detail 補充：DLL 無符號差異（nat57.txt:41,43 vs nat59.txt:49,51；sym57.txt:291-292 vs sym59.txt:299-300 皆存在）／範例無變更（SKCOMTester/SKOrder.cs 呼叫碼逐字相同，僅行號 444/452→518/526 位移；MainForm.cs 完全相同；diffs/*.diff 無任何 LoadOS/LoadOOCommodity 命中）／僅手冊分冊 9 備註欄刪減。.57 兩個函式的備註各含「與SKOSQuoteLib_EnterMonitorLONG（海選為 SKOOQuoteLib_EnterMonitorLONG）相關，可以先進行海期(選)連線備妥商品檔」與「*出現錯誤代碼2015,請重新連海期行情主機或重新下載.」，LoadOS 另含一段用 日期_OSQuote.log 確認商品檔是否下載成功的說明；.59 分冊 9 只留「具海期帳號，海期／海選委託下單前須先下載」。函式簽名與回傳值語意未變。需修正原宣稱的誇大：LOG（日期_OSQuote.log）檢查說明在 .59 仍保留於合冊手冊 策略王COM元件使用說明_V2.13.59.md:832（4-2-39 LoadOSCommodity），錯誤碼 2015 定義也仍列於 .59 的 4.下單準備介紹.md:856 錯誤代碼表；.59 全套文件中真正查不到的只有 EnterMonitorLONG 關聯提示與「重新連海期行情主機或重新下載」這條排除方法（可回頭參考 .57 版文件）。
- C-31：dev_impact: must_change → should_update（2 個視角同意）
- C-32：kind: behavior_fix → doc_only（2 個視角同意）；dev_impact: should_update → none（2 個視角同意）
- C-33：[impact] detail 補充：包裝層 DLL 有／包裝層手冊有／範例有。C_Sharp策略王DLL元件使用說明 新增五段：GetRealBalanceReport(loginID, account)、GetOpenInterestGW(loginID, account, nFormat)、GetFutureRights(loginID, account, nCoinType)、GetOFOpenInterestGW(loginID, account, nFormat)、GetOFFutureRights(loginID, account, nCoinType)，各自回傳含 StatusCode/Message/RawData/Blocks 的 ParserResult。strings 比對 .57/.59 兩版 SKDLLCSharp.dll(x64 Release)：五個方法名與五個 ParserResult 型別名皆 57=0、59=1（DLL 45,568→70,144 bytes）。SKDLLTester/Form1.cs 新增五個 Click handler；Form1.Designer.cs 僅在既有頁籤內新增 groupBox33「證券即時庫存查詢」、groupBox34「國內期貨權益數查詢」、groupBox31「國內期貨未平倉查詢」、groupBox35「國外期貨權益數查詢」、groupBox32「國外期貨未平倉查詢」（.59 TabPage 總數由 25 降為 17，確無新頁籤）。COM 層對應關係修正：GetRealBalanceReport／GetOpenInterestGW／GetFutureRights 在 Interop.SKCOMLib 的 .57 與 .59 符號表皆存在；GetOFOpenInterestGW 對應 GetOverseaFutureOpenInterestGW（兩版皆有）；GetOFFutureRights 的明顯對應者是 COM 的 GetRequestOverSeaFutureRight（兩版皆有，官方手冊列為「查詢海外期貨權益數」），並非無跡可循。但「底層 COM 未變」對兩個權益數查詢不成立：OnFutureRightsStatus 與 OnOverseaFutureRightsStatus 兩個查詢狀態事件在 .57 Interop 符號表 0 命中、.59 才新增，且 .59 手冊寫明資訊由該事件回傳；同步式包裝很可能靠它判定查詢結束，因此在 .57 的 COM 元件上自行複刻同樣的同步語意未必可行。開發者影響：純新增，既有 .57 呼叫不受影響，回報字串亦無新增欄位（原始字串保留在 RawData），故為 opt_in；但要取得這五個方法必須換用 .59 的 SKDLLCSharp.dll，而同一顆 DLL 在 .59 已移除 SKOSQuoteLib_RequestStocks/RequestTicks/GetStockByNoNineDigitLONG、SKOOQuoteLib_RequestStocks/RequestTicks/GetStockByNoLONG、pSKForeign_9LONG 等海期/海選報價包裝，仍在用這些包裝的專案要一併評估。解析注意：GetOFOpenInterestGW 的 Block 依 IsOption（"OF"/"OO"）分流，OO 多出 StrikePrice 與 CallPut 兩欄。文件瑕疵兩處：(1) 五段的「宣告」列一律寫成回傳 (int Code, string Message)，與「回傳值」及範例實際使用的 XxxParserResult（result.StatusCode/result.Blocks）矛盾，照宣告寫會編不過；(2) 手冊 GetOFOpenInterestGW 範例傳 nFormat + 1，Form1.cs 實際傳 nFormat，格式參數該傳何值文件與範例不一致。
- C-35：[spec] detail 補充：包裝層手冊有／範例有（tester 下拉選單同步移除）／版本歷程未公告。C_Sharp策略王DLL元件使用說明：方法說明由「與(回報/國內行情/海期行情/海選行情/下單)主機建立連線」改為「與(回報/國內行情/下單)主機建立連線」；nTargetType 由「0:回報；1:國內行情；2:海期行情；3:海選行情；4:Proxy下單」改為「0:回報；1:國內行情；4:Proxy下單」；nStatus 少了「4:連線(備援(僅海期選))」；OnConnection 的 loginID 說明同步移除「海期行情：SKOSQuote / 海期行情：SKOOQuote」。SKDLLTester 的 comboBoxTargetType 也移除「2:海期行情」「3:海選行情」兩項。既有程式若呼叫 ManageServerConnection(nTargetType=2/3) 或 nStatus=4，在 .59 的包裝層已無對應連線目標，故 dev_impact=breaking。版本歸屬：該包裝層手冊頁尾「文件版本」由 2.13.57 改為 2.13.58（隨 .59 包發佈），且 COM 主手冊 2.13.58／2.13.59 兩列版本歷程均未提及此異動，屬未公告變更。範圍修正：不可宣稱「僅限包裝層、直接用 COM 的 SKOSQuoteLib_EnterMonitorLONG 不受影響」——.59 全套官方文件已把海期／海選報價下架（14.海期報價.md、15.海選報價.md 兩份文件消失；主手冊 SKOSQuoteLib 出現次數 97→1，僅剩錯誤碼列；導覽移除 SKOSQuoteLib/SKOOQuoteLib 報價物件段），DLL 是否仍匯出需另以符號證據判定。下架對象是海期／海選「行情」，海期／海選 Proxy 下單與海外未平倉、權益數查詢在 .59 包裝層手冊中仍保留。；[impact] detail 補充：包裝層手冊有／範例有（tester 下拉選單同步移除並加補償映射）／底層 COM 未變且反而擴充。手冊 C_Sharp策略王DLL元件使用說明：方法說明由「與(回報/國內行情/海期行情/海選行情/下單)主機建立連線」改為「與(回報/國內行情/下單)主機建立連線」；nTargetType 由「0:回報；1:國內行情；2:海期行情；3:海選行情；4:Proxy下單」改為「0:回報；1:國內行情；4:Proxy下單」；OnConnection 的 loginID 說明同步移除「海期行情：SKOSQuote / 海期行情：SKOOQuote」。nStatus 一項需修正原宣稱：手冊確實刪掉「4:連線(備援(僅海期選))」，但 SKDLLTester .59 的 comboBoxStatus 仍完整保留該選項（Form1.Designer.cs:4482-4487，與 .57:4209-4214 逐字相同），故 nStatus 4 屬純文件移除、手冊與範例現在互相矛盾，範例並未同步。範例僅移除 comboBoxTargetType 的「2:海期行情」「3:海選行情」兩項，並在 Form1.cs 加上 SelectedIndex==2 → nTargetType=4 的補償映射。二進位佐證：.59 的 SKDLLCSharp.dll 已不含 SKOSQuoteLib_RequestTicks/RequestStocks/GetStockByNoNineDigitLONG 與 SKOOQuoteLib_RequestTicks/RequestStocks/GetStockByNoLONG 這 6 支方法字串（.57 皆有），可見包裝層是實際下架海期／海選，非僅改手冊字面，故 dev_impact=breaking：既有以包裝層寫成、呼叫 ManageServerConnection(nTargetType=2/3) 的程式在換用 .59 SKDLLCSharp.dll 後連線目標已不存在，且即使連上也無對應報價請求方法可用。對開發者另有一個具體地雷：.57 範例慣用寫法 int nTargetType = comboBoxTargetType.SelectedIndex; 在刪掉兩個選項後，索引 2 會變成「4:Proxy下單」——若只照抄 .57 寫法而未加 .59 的補償映射，會把下單連線靜默送成已作廢的 nTargetType=2。範圍限於簡化版包裝層：原生 SKCOM.dll 在 .59 反而新增 SKOOQuoteLib_EnterMonitorLONGWWW / EnterMonitorWWW / GetStockByNoLONGWWW / GetStockByNoWWW / GetTickLONG / RequestLiveTick / RequestStocksWW / RequestTicksWWW 共 8 支海選符號，Interop.SKCOMLib 也仍保有 SKOSQuoteLib_EnterMonitorLONG 等，直接使用 COM 者完全不受影響，因此 assessment=regression 只對包裝層使用者成立，對 COM 直用者為 neutral／甚至 improvement。與 C-34 為同一次海期／海選包裝層下架動作的一部分。
- C-36：[code] detail 補充：changelog 原文「修正簡化版API下單成功回傳值」。「簡化版API」即 SKDLLCSharp.dll 這套 C# 包裝（raw59/C_Sharp策略王DLL元件使用說明.md 開頭：「VS2019 引用…選 SKDLLCSharp.dll / using SKDLLCSharp;」）。下單回傳值的修正屬實作內部變更，無符號級證據：SKDLLCSharp.dll 兩版的下單/刪改單識別字集合完全一致（Send*ProxyOrder、Send*ProxyAlter、SendTFOffset、cancelOrderMarkByExchange 完全相同），SKDLLTester 範例也未改動任何下單回傳值判定；Interop.SKCOMLib（sym57/sym59）本就不含此包裝層。注意：不可說「包裝層 DLL 無符號差異」——SKDLLCSharp.dll 本身變動很大（x64 45568→70144 bytes、x86 46080→70656），新增 GetRealBalanceReport / GetOpenInterestGW / GetFutureRights / GetOFOpenInterestGW / GetOFFutureRights 及其 *ParserResult 型別，並移除 SKOSQuoteLib_* / SKOOQuoteLib_* 海期海選報價包裝（另屬他項 finding）。另外，本項並非 changelog 孤證：C_Sharp策略王DLL元件使用說明.md.diff 中，每個 Proxy 下單／刪改單函式的「備註」欄在 .59 都新增「*此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認」（「回傳值」欄未改，仍為 Code=0 成功、Message 成功回 ORKEY）。使用簡化版包裝者請重新確認成功判定條件，並與 C-21 的「回傳 0 只代表送達交易所」語意一併理解。；[impact] detail 補充：changelog 有（主手冊 2.13.59 列）／包裝層手冊未展開回傳值定義細節。changelog 原文「修正簡化版API下單成功回傳值」，就套件內容推斷指的是 SKDLLCSharp（C_Sharp策略王DLL元件）這套獨立 C# 包裝層——注意「簡化版」三字在 .57/.59 全部文件與原始碼中僅出現於此 changelog 句，此對應為推論而非文件明載，只是套件內唯一符合的包裝層。證據面：SKDLLCSharp.dll 確有改版（x64 45,568→70,144 bytes，md5 a72a341c…→5c8c782c…），符號比對可見新增 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights／GetOFOpenInterestGW／GetOFFutureRights，移除 SKOSQuoteLib_*／SKOOQuoteLib_*；但「下單函式符號集合完全一致」（兩版 Send* 皆同樣 15 個），故本項確為行為修正而非介面異動。C_Sharp策略王DLL元件使用說明.md.diff 的「回傳值」欄定義未改，但全部 13 個下單／刪改單函式的「備註」新增了「*此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認」——該句同時加入 5/7/9/11 下單文件與 COM 主手冊，對應 2.13.58「文件調整：調整下單函式文件說明」，屬全文件調整，非本項專屬佐證。開發者影響：使用簡化版包裝者需重新確認成功判定條件（Code==0 與 Message 是否為 ORKEY），並一併留意 C-21 的「回傳 0 只代表送達交易所」語意；直接使用 COM 介面者不受影響。修正方向（錯在 Code 或 Message）無證據可判定，SKDLLTester 範例兩版對回傳值的處理寫法完全相同，故維持 should_update；若既有程式曾針對舊版錯誤回傳值寫死判斷，實務上接近 must_change。
- C-37：kind: api_changed → api_added（2 個視角同意）；dev_impact: should_update → opt_in（2 個視角同意）；[impact] detail 補充：DLL 有（本項本身即符號比對結論）／手冊無／範例無。獨立複驗：comm -23 sym57 sym59 = 0 行，comm -13 = 39 行。.59 Interop 未移除任何型別名、介面名、方法名、事件名、委派型別、add_/remove_ 存取子、m_*Delegate 欄位或參數名；ISKCenterLib／ISKOrderLib／ISKQuoteLib／ISKReplyLib／ISKOSQuoteLib／ISKOOQuoteLib 全數保留（SKOOQuote 相關符號兩版 grep 輸出逐行相同）。39 個新增符號 100% 歸入 7 個功能群（RightsStatus 10、OpenInterestWithDetails 11、LiveKLine 10、OnOpenInterestJson 5、EnterMonitorLONGByMarket／ExportStockList／SendOverseaOptionOrderOLID 各 1），無未歸類殘留。\n\n對維護 .57 程式的工程師而言，實務結論比原宣稱更寬鬆：(a) 兩版 Interop.SKCOMLib.dll 內嵌的 19 個 GUID（LIBID/IID/CLSID）集合完全相同，群益未改 IID，舊 Interop 對新原生 SKCOM.dll 仍可正常 QueryInterface；(b) 組件識別未變（名稱 Interop.SKCOMLib、FileVersion/ProductVersion 皆 1.0.0.0、未強式簽章），新 Interop 屬 drop-in 檔案替換，不會觸發版本繫結失敗；(c) 0 移除代表舊程式呼叫的成員一個不少。故升級 .59 既是 source-compatible 也未見 binary-incompatible 跡象，不強制重新編譯，也不強制換 Interop——只有要使用新事件／新方法時才需要換上 .59 的 Interop.SKCOMLib.dll，屬 opt_in。\n\n限制與殘留風險：本比對為「符號名稱集合」比對（sym 檔取自組件字串／#Strings 層級，可見 BSJB、ArrayList 等 metadata 殘跡），偵測不到既有方法的參數型別、參數順序或 vtable 位置變動，需 ildasm/tlbexp 反編譯才能確認；且因兩版 IID 相同，萬一新成員被插在 vtable 中間而非附加於末端，舊 Interop 會靜默錯位呼叫而非明確報錯——目前無任何證據顯示存在此情況，僅列為驗證缺口。另附註：組件確實重建過（MVID 由 d7d80a4e-bff8-4d88-88ec-2327e7d31ad0 變為 cbc1719c-190c-44f7-8c0f-577f58eca0c2，檔案 155136→161280 bytes），但重建本身不構成開發者必須跟進的動作。
- C-39：kind: api_changed → api_added（2 個視角同意）；[spec] detail 補充：維持原判定，僅補兩點文件面的精確化：(1) 官方手冊（.57 與 .59 兩套 md 全文）從不出現 W/WW/WWW 尾綴的函式名，這層 flat export 屬未文件化的原生匯出層，2.13.58 與 2.13.59 的版本歷程表對 SKOOQuoteLib 也隻字未提，故文件無記載不構成反例。(2) 8 個新匯出中，EnterMonitorWWW／GetStockByNoWWW 去尾綴後對應的是「無 LONG 版」方法，官方手冊 2.13.46 那列已明寫「V2.13.46(含)以上版本，不提供舊版行情相關函式…海選報價 SKOOQuoteLib」，因此它們雖在 .57 Interop metadata 中存在，官方視角仍屬不支援的舊版函式；其餘 6 個（EnterMonitorLONGWWW、GetStockByNoLONGWWW、GetTickLONG、RequestLiveTick、RequestStocksWW、RequestTicksWWW）對應的方法在 .57 手冊 4-6 SKOOQuoteLib（海選報價）函式表中皆有明列。另附：nat57→nat59 除 10 個新增外尚有 1 個移除 SKQuoteLib_DeltaT，本項未涵蓋（屬 C-38）。；[code] title 建議：原生 SKCOM.dll 匯出表 .57→.59 僅新增 5 個下單/帳務 flat export，SKOOQuoteLib_ 匯出完全未變（原「多出 8 個 SKOOQuoteLib_ 匯出」係字串掃描雜訊）；[impact] detail 補充：DLL 有（僅原生匯出層）／手冊無／範例無。nat59 比 nat57 多的 10 個匯出中，8 個是 SKOOQuoteLib_（EnterMonitorWWW、EnterMonitorLONGWWW、GetStockByNoWWW、GetStockByNoLONGWWW、GetTickLONG、RequestLiveTick、RequestStocksWW、RequestTicksWWW）。其中 6 個去掉 W/WW/WWW 尾綴後、另 2 個（GetTickLONG、RequestLiveTick）直接同名，對應的 COM 方法在 .57 的 Interop metadata 裡全部已存在，且 ISKOOQuoteLib 的方法與事件集合兩版逐字相同（見 C-37）。結論：不是新功能，只是原生 DLL 對外暴露的 flat export 子集變多（.57 只暴露 4 個 SKOOQuoteLib_ 匯出，.59 有 12 個）；這些帶後綴的名稱在任何版本手冊中都查不到（raw59 全文 grep 'WWW' 零命中），無文件、無型別資訊、無範例，開發者無從採用。COM 使用者看不到差異，直接 P/Invoke 的使用者也只是多了符號、既有呼叫不受影響，不需跟進。另外 2 個新增分別是 SKQuoteLib_ExportStockListWW（真新增，見 C-04）與 SKQuoteLib_Delta（見 C-38；同時 SKQuoteLib_DeltaT 是本次唯一被移除的匯出）。附帶意義（須限縮）：這 8 個新匯出證明原生 SKCOM.dll 與 COM typelib 仍保有海選實作，可反駁『COM 層移除海期／海選』的說法；但不可外推為『海選整體未下架』——同一交付包中 SKDLLCSharp.dll 包裝層已把 SKOOQuoteLib_/SKOSQuoteLib_ 方法全數刪光（x64 3→0、x86 6→0）、C_Sharp DLL 手冊 SKOOQuote 提及 13→0、14.海期報價.md 與 15.海選報價.md 兩份文件整份消失、SKDLLTester/Form1.cs 用法 10→0。正確表述為：原生／COM 層仍在，被移除的是 SKDLLCSharp 包裝層＋文件＋範例（走 flat-DLL 路徑的海選開發者屬 breaking，應另立 finding）。
- C-41：dev_impact: none → should_update（2 個視角同意）；[code] title 建議：SKDLLTester 的 ManageServerConnection 按鈕新增 UI 索引→nTargetType 補償映射，以配合下拉選單移除海期／海選兩項；[impact] detail 補充：不是純範例程式變更。官方 .59 手冊把 ManageServerConnection 的 nTargetType 定義從『0:回報；1:國內行情；2:海期行情；3:海選行情；4:Proxy下單』縮為『0:回報；1:國內行情；4:Proxy下單』，nStatus 也刪掉『4:連線(備援(僅海期選))』，函式說明改為『與(回報/國內行情/下單)主機建立連線』，並整段移除 SKDLLCSharp 的海期行情章節（OnNotifyOSQuoteLONG、SKOSQuoteLib_GetStockByNoNineDigitLONG、SKOSQuoteLib_RequestStocks 等）。數值本身確實沒有重編號（4 仍是 4），但『呼叫端仍可自行傳 2/3』已無文件依據，既有 .57 程式若以 nTargetType=2/3 連海期／海選行情主機需重新驗證或改道 → should_update。範圍限於 SKDLLCSharp wrapper；COM 層 SKOSQuoteLib 未受影響（sym57/sym59 的 OSQuote 符號同為 56 筆）。tester 端 Form1.cs:1144-1146 新增 `if (comboBoxTargetType.SelectedIndex == 2) nTargetType = 4;`，把清單刪項後位移的 Proxy下單校正回 4，行為不變。另需注意：.59 手冊中 ManageServerConnection 的『範例程式碼』欄位並未同步加上此映射，仍為 `int nTargetType = comboBoxTargetType.SelectedIndex;`，照抄該片段搭配新三項清單會讓 Proxy下單誤傳成 2。；[impact] title 建議：SKDLLCSharp 的 ManageServerConnection 官方定義移除 nTargetType 2/3（海期／海選行情）與 nStatus 4（備援），tester 同步拿掉下拉選項並補上 SelectedIndex==2→4 映射
- C-42：[code] detail 補充：（原宣稱經逐條驗證屬實，以下為補完後版本）DLL 無（COM 未變）／手冊有（見 C-48）／範例有（整批刪檔）。四個專案同步下架：(1) SKCOMTester——Form1.Designer.cs 刪除 tabpage4「海期報價」與 tabPage5「海選報價」兩頁籤（各含 skosQuote1/skooQuote1 控件），Form1.cs 刪除 new SKOSQuoteLib()/new SKOOQuoteLib() 與 wiring（注意：欄位宣告 `SKOSQuoteLib m_pSKOSQuote; SKOOQuoteLib m_pSKOOQuote;` 於 .59 Form1.cs 第 27-28 行仍保留，成為未使用的殘留欄位），csproj 移除 6 個項目且 .59 樹無此六檔；(2) SKCOMTesterV2——csapiTester.csproj 移除 Quote\OOQuoteForm.*／OSQuoteForm.* 六項，MainForm.cs 移除兩個 Click handler，Designer 移除兩顆按鈕，Quote 目錄只剩 QuoteForm.*(+vssver2.scc)；(3) CppCLITester——vcxproj 移除 SKOSQuote.cpp/.h/.resx（補充：.57 的 MyForm.h 本來就未引用 SKOSQuote，該控件在 .57 即為未接線的孤兒檔，故 UI 上並未少掉入口；同一 diff 另含 PlatformToolset v142→v143、WindowsTargetPlatformVersion 10.0→10.0.20348.0、新增 Interop.SKCOMLib Reference，屬另一項變更）；(4) SKDLLTester——Form1.Designer.cs 刪除 tabPage10「海期行情」、tabPage11「海選行情」及其下 tabControl5(tabPage15/16/17)、tabControl6(tabPage19/20/21) 與 6 個 DataGridView，且 comboBoxTargetType 的選項清單一併移除 "2:海期行情"、"3:海選行情"（僅剩 0:回報/1:國內行情/4:Proxy下單）——此為真正的功能入口減少；Form1.cs 亦刪除 OnNotifyOSQuoteLONG/OnNotifyOOQuoteLONG 事件掛載與 SKOSQuoteLib_GetStockByNoNineDigitLONG/SKOOQuoteLib_GetStockByNoLONG 呼叫示範。COM 未變之三重反證全部覆核通過。另注意：.57 樹的 ExcelSample/Program.cs 亦大量使用 SKOSQuoteLib 而 .59 無，但該檔（連同 CapitalQuoteService.csproj）是本機自建專案塞進官方資料夾，官方 ExcelSample 只有 ExcelSample.xls，不可計為第五個被移除的官方範例。；[impact] detail 補充：原宣稱的範例移除事實與「COM 未變」三重反證全部複驗通過，維持不變。需修正的是層級與影響，本項實際橫跨兩層，建議拆分：
(A) sample_removed / dev_impact=none / neutral — SKCOMTester、SKCOMTesterV2、CppCLITester 三個 COM 路線專案：檔案與頁籤/按鈕整批刪除，但 Interop 符號 sym57↔sym59 對 SKOSQuoteLib/SKOOQuoteLib 的 93 行完全相同、原生 SKCOM.dll 的 SKOOQuoteLib_ 匯出 4→12、.59 下單範例仍呼叫 SKOSQuoteLib_EnterMonitorLONG()、.59 手冊仍保留錯誤碼 2025/2026 指向該函式。既有 COM 程式照跑，只是失去官方接法示範（需自留 .57 範例與 .57 手冊 14/15 章）。
(B) api_removed / dev_impact=breaking / regression — SKDLLTester 走的 SKDLLCSharp.dll（C# DLL 元件）路線：.59 的 SKDLLCSharp.dll 為新建置（2026-03-17, 70144 bytes vs .57 2025-09-23, 45568 bytes），metadata 中 SKOSQuoteLib_RequestStocks、SKOSQuoteLib_RequestTicks、SKOSQuoteLib_GetStockByNoNineDigitLONG、SKOOQuoteLib_RequestStocks、SKOOQuoteLib_RequestTicks、SKOOQuoteLib_GetStockByNoLONG 六個公開方法全數消失（同檔 SKQuoteLib_ 維持 8 個；x86 版一致），事件名 OnNotifyOSQuoteLONG/OnNotifyOOQuoteLONG 雖殘留卻已無取價/訂閱函式可搭配。手冊同步刪除整節「海期行情/海選行情」，並把 ManageServerConnection 的 nTargetType 由『0:回報；1:國內行情；2:海期行情；3:海選行情；4:Proxy下單』縮為『0:回報；1:國內行情；4:Proxy下單』、OnConnection 的 loginID 刪掉 SKOSQuote/SKOOQuote；範例 Form1.Designer.cs 亦移除 comboBoxTargetType 的『2:海期行情』『3:海選行情』。以 .57 SKDLLCSharp.dll 寫的海期/海選報價程式換上 .59 DLL 會直接編譯失敗，屬 breaking，不可歸為單純「失去參考範例」。
殘留一致性瑕疵可記：.59 SKDLLTester Form1.Designer.cs 的 nStatus 下拉仍保留『4:連線(備援(僅海期選))』，但 nTargetType 已無海期/海選可選，該選項成為死選項。
- C-44：[spec] detail 補充：純範例品質風險（屬 C-01 新功能的示範碼），非 API 缺陷。buttonGetLiveKLine_Click 將 pSKKLine.nOpen/nHigh/nLow/nClose 一律 /100m 顯示；但官方手冊在 OnNotifyLiveKLineData 備註明寫「請注意價格為原始價格，需自行除以小數位數還原價格」（例：群益證 3815→38.15、台指期 4557900→45579.00）。原判斷中「/100 是否放諸所有商品皆準未查證」一點已可解：官方提供的小數位數來源是 SKSTOCKLONG.sDecimal（raw59 13.國內報價.md struct 定義有 `SHORT sDecimal;// 小數位數`，經 SKQuoteLib_GetStockByNoLONG/GetStockByIndexLONG 取得），且同一個範例檔 SKCOMTester/SKQuote.cs 對其他所有報價價格（nOpen/nHigh/nLow/nClose/nRef/nBid/nAsk/nUp/nDown）一律用 `/ Math.Pow(10, pStockLONG.sDecimal)` 換算——只有新加的即時分K 這段寫死 /100，屬同檔內部不一致。另一處不一致：同批新增的 OnNotifyLiveKLineData 事件處理與自組 5 分K 邏輯完全使用原始整數（未除），只有 GetLiveKLineLONG 按鈕除 100，兩處顯示尺度不同。開發者照抄時應改為依 sDecimal 還原。注意手冊的小數位警語掛在事件備註，SKQuoteLib_GetLiveKLineLONG 與 SKKLINE struct 說明未重述，SKKLINE 本身也不帶 sDecimal 欄位，需另行查商品小數位數。；[code] detail 補充：純範例品質風險（屬 .59 新增即時分K 功能的示範碼）。buttonGetLiveKLine_Click 把 pSKKLine.nOpen/nHigh/nLow/nClose 一律 /100m 顯示；官方手冊在 OnNotifyLiveKLineData 備註明寫「請注意價格為原始價格，需自行除以小數位數還原價格」（舉例證券 3815→38.15、台指期 4557900→45579.00，兩例恰好都是 2 位小數）。已查證：SKKLINE 結構本身不含小數位欄位（僅 nDate/nTimehm/nOpen/nHigh/nLow/nClose/nQty），小數位須另從 SKSTOCKLONG.sDecimal 取得；而同一份範例檔 SKCOMTester/SKQuote.cs 在報價/最佳五檔路徑早已正確使用 Math.Pow(10, pSKStockLONG.sDecimal)（並以 100.00 為取不到時的預設值），可見 /100m 是新示範碼的偷懶寫法而非官方換算規則。非 2 位小數的商品照抄會顯示錯價。另注意同一新功能內表現不一致：OnNotifyLiveKLineData 事件處理與「自組5分K」聚合完全不做換算、直接輸出原始整數，只有 GetLiveKLine 按鈕做了 /100m。抄用者應改為依 sDecimal 還原。；[impact] detail 補充：純範例品質風險（屬 C-01 新功能的示範碼），對維護 .57 程式者屬可選採用。兩條路徑各有問題：(a) buttonGetLiveKLine_Click（SKQuote.cs:1764-1767）把 SKQuoteLib_GetLiveKLineLONG 回傳的 pSKKLine.nOpen/nHigh/nLow/nClose 一律 /100m 顯示；(b) m_SKQuoteLib_OnNotifyLiveKLineData（SKQuote.cs:950-1010）則完全不做還原，直接以原始整數顯示並以整數累加自組 5 分 K。官方手冊在 OnNotifyLiveKLineData 備註明寫「請注意價格為原始價格，需自行除以小數位數還原價格」（證券 3815→38.15、台指期 4557900→45579.00）。關鍵反證：同一份範例檔在 Tick/五檔與商品清單路徑（SKQuote.cs:567-574、1203-1213）是先以 SKQuoteLib_GetStockByNoLONG / SKQuoteLib_GetStockByMarketAndNo 取得 pSKStockLONG.sDecimal，再用 Math.Pow(10, sDecimal) 還原，僅在查詢失敗時才 fallback 100.00 —— 可見 100 在群益自家範例慣例中只是預設值而非通則；且 SKKLINE 結構本身不帶小數位欄位（僅 nDate/nTimehm/nOpen/nHigh/nLow/nClose/nQty），採用此新功能者必須自行另外查商品的 sDecimal 才能正確還原。建議：不要照抄 /100，改沿用同檔 sDecimal 的作法，並且在自組 K 棒時保留原始整數、只在顯示層還原。
- C-46：assessment: unclear → regression（2 個視角同意）；[impact] detail 補充：純範例包層面，無 COM API 介面變更。官方 ExcelSample 資料夾在 .57／.59 皆只應含 ExcelSample.xls；.59 版（582656 bytes, Last Saved 2026-03-20）與 .57 版（491008 bytes, Last Saved 2025-07-01）MD5 不同，確為官方更新。與原宣稱不同，此檔可做文字級比對（soffice --headless --convert-to fods 轉 flat ODS XML）：.59 工作表由 11 張減為 9 張，被移除的是「海期報價」與「海選報價」（.57 中各含 SKOSQuoteLib 海期／海選 RTD 示範列，如 CBT/FV2003、EUR/ESX03475G7、HKF/HSI26000S7，欄位為 開盤/最高/最低/成交/結算/單量/昨收/買價/買量/賣價/賣量/成交量），以 UTF-16LE 與 CP950 掃 .59 原始 xls 位元組確認兩表名出現 0 次（非隱藏）；同時「海期&海選可交易商品」由僅表頭（Return: [GetOverseaOptions] 0）擴充為 957 列實際商品快照（Return: [GetOverseaFutures] 0，含 ASX/TFX 等交易所、2603~2612 商品年月、價格跳動點、分母、可委託類型、可否當沖），並附美式週選（M3D/M4A/S3D 等）欄位。其餘六張表（回報、新回報、證券委託、複委託、海期&海選委託、國內期貨&選擇權委託）文字內容逐列相同；「報價」分頁僅行情數值換成新快照（台積電 213.5→1875、TX00 10375→33973），欄位結構未變；「登入」多一列狀態值與 [LoadOOCommodity]Code:0 回傳字串，非新功能入口。對開發者影響：C#／其他語言呼叫端零改動；只有以此活頁簿當海期／海選 Excel RTD 參考的人失去示範分頁（該功能仍受支援，.59 手冊仍載 MS EXCEL x86 用 5-9 SKFOREIGNTICK、5-10 SKBEST5，x64 用擴充 SKFOREIGNTICK_9、SKBEST5_9），建議保留 .57 的活頁簿當範本。另澄清易誤判點（原宣稱正確）：.57 樹 ExcelSample/ 下的 Program.cs、CapitalQuoteService.csproj（SDK-style net472、namespace CapitalQuoteService、targetSymbol="MGC2602"、以 subscribe.json/order.json/quote.json/status.json 做自訂 IPC）連同 SKCOM.dll、bin/obj/.vs 與 bin/x64/Debug/CapitalLog/20260130_*.log，皆為 repo 擁有者自建的私有工具意外落在解壓樹內，非群益官方範例，故「.59 拿掉 ExcelSample/Program.cs」不是官方變更。
- C-47：[spec] detail 補充：建置設定變更：WindowsTargetPlatformVersion 由 10.0 改為 10.0.20348.0；四組 PlatformToolset（Debug/Release × Win32/x64）由 v142 改為 v143（VS2019→VS2022 工具鏈）；新增一筆顯式 <Reference Include="Interop.SKCOMLib"><HintPath>..\x64\Debug\Interop.SKCOMLib.dll</HintPath></Reference>（.57 無此項；注意該 HintPath 硬指向 x64\Debug，Release/Win32 組態同樣沿用）。惟本檔非「純建置設定變更」：同一 vcxproj diff 另移除 ClCompile SKOSQuote.cpp、ClInclude SKOSQuote.h、EmbeddedResource SKOSQuote.resx，且三個檔案在 .59 樹已實體刪除，代表 C++/CLI 範例拿掉了「海外報價（SKOSQuote）」功能入口——此為範例層移除，API 表面未變（raw59 主手冊仍保留 SKOSQuoteLib_EnterMonitorLONG 與錯誤碼 2025）。另官方《1.環境設置》在 .59 仍寫「使用 Microsoft Visual Studio 2019 建立新專案」，未跟著改為 VS2022，文件與範例工具鏈存在落差。；[impact] title 建議：CppCLITester.vcxproj 建置工具鏈升級（v142→v143、SDK 版本釘死）並移除 SKOSQuote 範例項目
- C-48：[spec] detail 補充：DLL 無（COM 符號 0 刪除）／手冊有（大規模刪除）／範例有（同批下架，見 C-42）。範圍：(a) 主手冊單一 hunk（diff L964 `@@ -2946,625 +2902,20 @@`，實測刪 618 行、加 13 行；625/20 為 hunk 跨度含 6 行 context）移除 4-5 SKOSQuoteLib（海期報價）與 4-6 SKOOQuoteLib（海選報價）兩整章，原位置改放新的 4-4-u OnNotifyLiveKLineData，章節序出現 4-4 → 4-4-u → 4-7 的斷層；(b) 連帶刪除結構 5-8 SKFOREIGNTICK、5-12 SKFOREIGNTICK_9、5-22 SKFOREIGNLONG、5-23 SKFOREIGN_9LONG（.59 序號跳為 5-6→5-9、5-11→5-13、5-21-2→5-24）與 3-1 物件架構圖中的相關條目；(c) 同批刪除主手冊 3-3 行情物件連線限制說明、3-3-1 海期相關說明、3-3-2 國內行情相關說明、3-4 行情功能修改說明整段（diff L69 `@@ -151,106 +151,6 @@`，刪 100 行）——注意此段連「行情總連線數上限 2 條」規則、3-3-2 SKQuoteLib1/SKQuoteLib2 混用禁忌、3-4 SKQuoteLib 新舊函式對應表都一併移除，對純國內報價開發者亦有影響，2.導覽.md 同段落亦刪；(d) 2.導覽.md 刪除「SKOSQuoteLib：海期報價物件」「SKOOQuoteLib：海選報價物件」兩個介紹區塊與架構圖文字中的 SKFOREIGNTICK/SKFOREIGNLONG/SKFOREIGN_9LONG/SKFOREIGNTICK_9/SKBEST5_9；(e) 14.海期報價.docx、15.海選報價.docx 兩份分冊在 .59 說明文件夾中不存在（其餘 1–13、16 都在，編號缺口），raw59 抽出結果亦無對應 md；(f) 除了新版本列未提及此次移除外，官方還「回溯改寫」歷史 changelog：2.13.31、2.13.33、2.13.45、2.13.52 各列的海期／海選字句被刪，最明顯的是 2.13.46 由「移除函式如下：國內報價 SKQuoteLib / 海期報價 SKOSQuoteLib / 海選報價 SKOOQuoteLib」縮成「移除函式如下：國內報價 SKQuoteLib」——即歷史紀錄也查不到蹤跡。整份 .59 主手冊只剩 2 處提及這兩個物件（錯誤碼 2025／2026 的說明）。附帶瑕疵：2.導覽.md 開頭「元件中包含六個ATL物件，與十八個結構物件」未同步更新，與只剩四個 ATL 物件的內文自相矛盾。關鍵判定：這是文件移除而非 API 移除——sym57↔sym59 對 SKOSQuoteLib/SKOOQuoteLib 逐行相同（各 56／37 筆，comm -23 無刪除）、原生匯出不減反增（C-39）、.59 範例的下單表單仍在呼叫這兩個元件（C-42）；海外期選「下單」文件（9.下單-海外期選.docx、10.下單-海外期選智慧單.docx）也仍在，被下架的僅是「報價」。後果：海期／海選報價功能仍可呼叫，但 2.13.59 版文件起沒有官方文件可查，只能沿用 2.13.57 版留存文件；api_spec/modules/SKOSQuoteLib.md、SKOOQuoteLib.md 內容仍有效，但應加註「.59 版官方主手冊已無此章節，內容以 2.13.57 版留存文件為準」；另建議把被刪的 3-3 行情連線數限制與 3-4 對應表也留存進 api_spec。；[code] detail 補充：DLL 無（COM 符號 0 刪除）／手冊有（大規模刪除）／範例有（同批下架，見 C-42）。範圍：(a) 主手冊單一 hunk `@@ -2946,625 +2902,20 @@`（純刪 618 行、純增 13 行、context 7 行；625/20 為 hunk 行距），移除 4-5 SKOSQuoteLib（海期報價）與 4-6 SKOOQuoteLib（海選報價）兩整章，原位置改放新的 4-4-u OnNotifyLiveKLineData，章節編號出現 4-4 直接跳 4-7 的斷層；(b) 連帶刪除結構 5-8 SKFOREIGNTICK、5-12 SKFOREIGNTICK_9、5-22 SKFOREIGNLONG、5-23 SKFOREIGN_9LONG（5-21 直接跳 5-24）與 3-1 物件架構圖中的相關條目；(c) 2.導覽.md 刪除「SKOSQuoteLib：海期報價物件」「SKOOQuoteLib：海選報價物件」兩個介紹區塊、「3-3 行情物件連線限制說明」整節，以及架構圖文字中的 SKFOREIGNTICK/SKFOREIGNLONG/SKFOREIGN_9LONG/SKFOREIGNTICK_9/SKBEST5_9；(d) 14.海期報價.docx、15.海選報價.docx 兩份分冊在 .59 的說明文件夾中不存在（其餘 1–13、16 號都在，編號出現缺口），raw59 抽出結果亦無對應 md。整份 .59 主手冊只剩 2 處提及這兩個物件（錯誤碼 2025／2026 的說明）。此外，.59 連歷史版本控管表都被回溯竄改：2.13.31、2.13.33、2.13.40、2.13.46 等舊條目中的海期／海選字樣被抹去（例：2.13.46「移除函式如下：國內報價 SKQuoteLib／海期報價 SKOSQuoteLib／海選報價 SKOOQuoteLib」在 .59 只剩「國內報價 SKQuoteLib」），而 2.13.59 當期條目對此次下架隻字未提。刪除留下多處內部矛盾：3-1 與 2.導覽 仍寫「元件中包含六個ATL物件，與十八個結構物件」但只列出 4 個；主手冊 4115、4163 行仍指向已刪的「5-9 SKFOREIGNTICK」「SKFOREIGNTICK_9」形成懸空交叉引用。關鍵判定：這是文件移除而非 API 移除——sym57↔sym59 對 SKOSQuoteLib/SKOOQuoteLib 逐行相同（56／37 行）、全檔刪除符號數為 0、原生匯出總數 84→93（唯一消失的原生匯出是 SKQuoteLib_DeltaT，與海期／海選無關，見 C-39）、.59 範例的下單表單仍在呼叫這兩個元件（C-42）。後果：海期／海選報價功能仍可呼叫，但 2.13.59 起沒有官方文件可查，只能沿用 2.13.57 版留存文件；api_spec/modules/SKOSQuoteLib.md、SKOOQuoteLib.md 內容仍有效，但應加註「.59 起官方主手冊已無此章節，內容以 2.13.57 版留存文件為準」。
- C-49：[impact] detail 補充：DLL 無（sym57/sym59 對 SKOSQuoteLib 56/56、SKOOQuoteLib 37/37、GetQuoteStatus 2/2、RequestStocksWithMarketNo 1/1、GetStockByNoLONG 3/3 完全相同）／手冊有（主手冊 hunk @@ -151,106 +151,6 @@ 一次刪 100 行；.59 第 3 章只剩 3-1 物件架構、3-2 註冊公告）／範例無。確定消失且無替代來源：(a) 兩條行情連線的分配政策（國內證券與國內期貨共用一條、海外期選單獨一條）與案例一／二情境圖；(b) 3-3-2「不建議在同一 SKQuoteLib 物件混用 RequestStocks 與 RequestStocksWithMarketNo、RequestTicks 與 RequestTicksWithMarketNo」警語（raw59 0 命中）；(c) 3-4 SKQuoteLib／SKOSQuoteLib／SKOOQuoteLib 三張舊→新函式一對一對照表與 32767 的三種情境；(d) 海期 OSQuote.log 判讀說明。但下列資訊仍留在 .59，故非全面失傳：4-4-23 SKQuoteLib_GetQuoteStatus 備註仍載「若最大連線數為2, 且目前連線超過限制,則回傳:2,True.」、錯誤碼 3030 SK_SUBJECT_NO_QUOTE_SUBSCRIBE 仍在錯誤表、4-1-9 SKCenterLib_LoginSetQuote 仍載 bstrSetFlag「Y:啟用報價 N:停用報價」、2.13.31 版本歷程列仍完整列出國內報價的 LONG 函式與事件清單。開發者影響：連線上限機制與 API 皆未變動，以 2.13.57 寫成的下單／報價程式不需修改任何程式碼，亦無新功能可選用，程式面為 none；實際要做的是文件保存——保留 V2.13.57 手冊，並在 api_spec 引用「一 ID 兩條行情連線」「SHORT 32767 新舊對照表」「WithMarketNo 混用禁忌」時註明來源為 V2.13.57 文件。另主手冊 2.13.31 列仍寫「詳情請參考 3-4 行情功能修改說明」而該章已刪，且 2.導覽.md 同列已把該句移除，形成 .59 內部兩份文件不一致的失效交叉引用（見 C-51）。
- C-50：[spec] detail 補充：DLL 無／手冊有／範例無。經逐列驗證，2021/07/08(2.13.31)、2021/9/13(2.13.33)、2021/10/19(2.13.35)、2022/7/15(2.13.39)、2022/11/2(2.13.40)、2023/3/17(2.13.42)、2024/1/15(2.13.45)、2024/12/19(2.13.52)、2025/04/14(2.13.53)、2024/1/29(2.13.46) 共十列既有 changelog 被就地改寫（宣稱原列 8 列，另補 2.13.39 刪「海期報價通知事件，新增備註說明：4-5-a、4-6-a」、2.13.45 刪「海外報價：海期價差商品年月揭示修正」）。內容包括：2.13.31 由「修正海選報價取得商品檔，交易所代號不正確(CBT->CBOT、EUR->Eurex)」＋「單一市場(國內、海期、海選)商品總數…」改為「單一市場(國內)…」並刪除整份 SKOSQuoteLib／SKOOQuoteLib 新增函式清單；2.13.33 刪掉「修正海期選報價，能夠使用下單交易所代碼取得報價（CBT→CBOT）」；2.13.35 刪「海期熱門月(近月)商品代號擴充…提供熱門月之交易所 TCE/CME/Eurex/OSE/CFE/ICESG/CBOT/NYM/ICEUS」；2.13.42 刪「SKOSQuoteLib_GetTick 回傳 SKFOREIGNTICK_9 新增成交日期／SKOOQuoteLib_GetTickLONG 回傳 SKFOREIGNTICK 新增成交日期」；2.13.53 刪「海期選連線行情收到3001代表成功」並整段重新編號；2.13.46 的「移除函式如下：國內報價 SKQuoteLib / 海期報價 SKOSQuoteLib / 海選報價 SKOOQuoteLib」改為只剩「國內報價 SKQuoteLib」（主手冊與 2.導覽.md 同步改）。混合評價：就「訂正」而言正確（sym59 中 SKOOQuoteLib_GetTick 與 GetTickLONG 並存、SKOSQuoteLib_GetTick 仍在，SKOOQuoteLib 符號數 .57/.59 同為 37，海期／海選非-LONG 舊函式從未真正移除，.59 文字較符 DLL 現況）；但就「歷史沿革證據」而言是損失。需注意清洗並不徹底：2.13.50 列仍完整保留「海期報價商品清單資訊OnOverseaProducts、OnOverseaProductsDetail新增商品第一通知日」，錯誤碼 2025 SK_WARNING_OSQUOTE_MUST_SKOSQUOTELIB_ENTERMONITORLONG_FIRST 說明仍寫「請先執行海期報價SKOSQuoteLib_EnterMonitorLONG連線」，2.13.31 改寫後也仍留 SKFOREIGNLONG／SKFOREIGN_9LONG 海外報價物件；因此手冊內部對海期／海選報價的敘述目前處於不一致狀態。2.13.58 與 2.13.59 兩列新 changelog 均未提及此次回溯改寫，無法歸屬到特定版本。與 C-48 是同一次「主手冊移除海期／海選」動作的一部分。；[code] detail 補充：DLL 無／手冊有／範例無。2021/07/08(2.13.31)、2021/9/13(2.13.33)、2021/10/19(2.13.35)、2022/11/2(2.13.40)、2023/3/17(2.13.42)、2024/1/29(2.13.46)、2024/12/19(2.13.52)、2025/04/14(2.13.53) 等既有 changelog 列被就地修改，刪去海期／海選相關敘述：2.13.31 由「單一市場(國內、海期、海選)商品總數…」改為「單一市場(國內)…」並刪除整份 SKOSQuoteLib／SKOOQuoteLib 新增函式與事件清單；2.13.33 刪掉「修正海期選報價，能夠使用下單交易所代碼取得報價（CBT→CBOT）」；2.13.35 刪掉「海期熱門月(近月)商品代號擴充」及提供熱門月之交易所清單（TCE、CME、Eurex、OSE、CFE、ICESG、CBOT、NYM、ICEUS）；2.13.40 由「國內報價SKQuoteLib、海外報價(SKOSQuoteLib 及SKOOQuoteLib)、結構物件」改為「國內報價SKQuoteLib、結構物件」；2.13.42 刪掉「海期功能相關: SKOSQuoteLib_GetTick 回傳物件5-12 SKFOREIGNTICK_9 欄位異動（新增成交日期）／SKOOQuoteLib_GetTickLONG 回傳物件5-8 SKFOREIGNTICK 欄位異動（新增成交日期）」——這是海外 TICK 結構欄位沿革的唯一紀錄；2.13.46 的「移除函式如下：國內報價 SKQuoteLib / 海期報價 SKOSQuoteLib / 海選報價 SKOOQuoteLib」改為只剩「國內報價 SKQuoteLib」（主手冊與 2.導覽.md 同步）；2.13.52 刪掉「修正群組帳號海期行情商品檔缺少問題」並把「修正只有海期帳號也能下單、查行情」削為「也能下單」；2.13.53 刪掉「文件更新：海期選連線行情收到3001代表成功，備註取得商品檔需要先連線」並整段重新編號。混合評價：就「訂正」而言方向正確（sym57/sym59 皆同時列有 SKOOQuoteLib_GetTick 與 SKOOQuoteLib_GetTickLONG、SKOSQuoteLib_GetTick 與 SKOSQuoteLib_GetTickNineDigit(LONG)，海期／海選的非-LONG 舊函式在兩版 Interop 中都未消失，故 2.13.46「已移除海期海選函式」的舊敘述本就與 DLL 現況不符）；但就「歷史沿革證據」而言是損失（海期交易所代碼 CBT→CBOT 行為切換、熱門月商品代號擴充與交易所清單、SKFOREIGNTICK/_9 新增成交日期欄位、3001 連線語意等，日後只能回頭查 2.13.57 版文件）。與 C-48 是同一次「主手冊移除海期／海選」動作的一部分。；[impact] detail 補充：DLL 無／手冊有／範例無。既有 changelog 列被就地改寫，刪除海期／海選相關歷史：2.13.31（刪 CBT→CBOT 交易所代號修正、刪整份 SKOSQuoteLib／SKOOQuoteLib 新增函式與事件清單、「單一市場(國內、海期、海選)」→「單一市場(國內)」）、2.13.33（整段「修正海期選報價，能夠使用下單交易所代碼取得報價（CBT→CBOT）」消失）、2.13.35（刪海期熱門月(近月)商品代號擴充與交易所清單 TCE/CME/Eurex/OSE/CFE/ICESG/CBOT/NYM/ICEUS）、2.13.40（「國內報價SKQuoteLib、海外報價(SKOSQuoteLib 及SKOOQuoteLib)、結構物件」→「國內報價SKQuoteLib、結構物件」）、2.13.42（刪 SKOSQuoteLib_GetTick 回傳 SKFOREIGNTICK_9、SKOOQuoteLib_GetTickLONG 回傳 SKFOREIGNTICK 新增成交日期欄位之異動紀錄）、2.13.52（刪「修正群組帳號海期行情商品檔缺少問題」）、2.13.53（「海期選連線行情收到3001代表成功」被換成「錯誤代碼1000，請注意登入帳號是否為大寫」）、2.13.46（「移除函式如下：國內報價 SKQuoteLib／海期報價 SKOSQuoteLib／海選報價 SKOOQuoteLib」→ 只剩「國內報價 SKQuoteLib」）。

更正：不可稱此為「事實訂正」。符號證據無法支撐——SKOOQuoteLib_GetTick 與 GetTickLONG 在 sym57、sym59 中同時存在（兩檔 SKOSQuoteLib 出現次數皆 56），該證據不區分版本；且 SKQuoteLib_GetTick／EnterMonitor／GetBest5／GetStockByNo 非-LONG 版在 sym59 同樣存在，而 .59 仍保留「移除：國內報價 SKQuoteLib」，可見原文「移除」指不再提供／不再支援而非 typelib 拿掉。故這是與 C-48 同一次「主手冊縮回國內範疇」的用語刷洗，不是精確度改善。

淨效果為文件退化：除歷史沿革（海期交易所代碼行為、熱門月商品代號擴充、SKFOREIGNTICK 成交日期欄位新增時點）只能回查 .57 版手冊外，2.導覽 的整節「行情功能修改說明」（含 SHORT 32767 溢位三種情境與 V2.13.30→V2.13.31 新舊功能對應一覽表）在 .59 一併消失，.59 主手冊亦無「3-4 行情功能修改說明」章節，但 2.13.31 changelog 仍寫「詳情請參考 3-4 行情功能修改說明」，形成斷鏈參照。

對維護 .57 程式者無強制動作：不涉呼叫方式、回報欄位或錯誤碼，只需保留 .57 手冊作為海期／海選與 LONG 遷移的歷史參照。
- C-54：dev_impact: should_update → none（2 個視角同意）；[spec] detail 補充：DLL 無／範例無／僅文件變更，且變更範圍侷限於 SKCOMTesterV2 附屬手冊 1.環境設置.docx。.57 寫「x86位元:直接註冊即可 / x64位元:透過SysWow64的regsvr32.exe註冊」；.59 改為「x32位元: 透過SysWow64的regsvr32.exe註冊 / x64位元: 透過System32的regsvr32.exe註冊 或 直接註冊即可」，這是本檔 .57→.59 唯一的實質內容差異（其餘僅來源路徑行）。關鍵反證：.59 主手冊《策略王COM元件使用說明_V2.13.59.md》附錄 A 第 4956–4957 行仍為舊字串，與 .57 主手冊第 5671–5672 行逐字相同，主手冊 doc diff 對 SysWow64/System32/位元 字串零命中；.59 版本歷程表 2.13.58、2.13.59 兩列亦無註冊／環境設置相關條目。因此不宜判定為官方「更正錯誤」，而應視為敘述主詞由『作業系統位元』改為『DLL 位元（預設 64 位元 OS）』的改寫，兩種寫法在各自主詞下都自洽，且 .59 文件組內部並不一致。開發者實務上仍以「安裝的 SKCOM.dll 位元須與執行檔平台一致（同文件已載明『安裝x64版的SKCOM.dll，卻使用x86平台執行』為錯誤範例）」為準，無須變更任何呼叫程式碼。

## 附錄 C：稽核代理指出的缺口

- **[high]** C-39（原生 SKCOM.dll 多出 8 個 SKOOQuoteLib_* flat export）建立在與已被淘汰的 C-38 完全相同的 strings 假象上，前提已被實證推翻，是目前唯一仍存活的偽陽性。用 objdump 直接解析 PE Export Directory：CapitalAPI_2.13.57_CExample/元件/x64/SKCOM.dll 與 2.13.59 同路徑檔，兩版匯出表中 SKOOQuoteLib_* 都只有且僅有 3 支且完全相同（SKOOQuoteLib_GetStockByNoLONG / SKOOQuoteLib_RequestStocks / SKOOQuoteLib_RequestTicks，.57 位於 real 表第 42-44 筆、.59 第 47-49 筆）；C-39 所列的 EnterMonitorWWW / EnterMonitorLONGWWW / GetStockByNoWWW / GetStockByNoLONGWWW / GetTickLONG / RequestLiveTick / RequestStocksWW / RequestTicksWWW 這 8 個名字在兩版的任一架構匯出表中都不存在，僅存在於 nat59.txt（第 16-27 行）這份 strings 產物。W/WW/WWW 尾綴與 C-38 的 DeltaT 是同一種 typelib 名稱池相鄰位元組污染。此 finding 應整條刪除或改寫成「nat*.txt 不可用」的方法學註記，不能留在 API 表面變更清單裡。
  - 檢查方法：objdump -p 元件/x64/SKCOM.dll | sed -n '/\[Ordinal\/Name Pointer\] Table/,/^$/p' 取兩版名稱表後 comm 比對；再 grep SKOOQuote 兩份真實匯出表確認各 3 筆且相同；x86 同法複驗。
- **[high]** 沒有任何存活 finding 以可靠證據記錄「原生 SKCOM.dll 的 flat export 表真正變化」。實測（objdump 名稱表）：x64 .57 = 78 筆、.59 = 83 筆；x86 .59 = 83 筆；移除 0 筆，新增恰為 5 筆 GetFutureRights / GetOFFutureRights / GetOFOpenInterestGW / GetOpenInterestGW / GetRealBalanceReport。裁決摘要宣稱「77 增為 82」卻沒有任何 finding 承載這條；C-33 雖然涵蓋同樣 5 個名字，但框在「簡化版 SKDLLCSharp 包裝層」層次，且其 evidence 引用 nat57.txt/nat59.txt 的 84/93 行數（該檔非匯出表，數字錯誤）。對不走 COM 而用 GetProcAddress／P-Invoke／非 .NET 語言的使用者，這是一項獨立的 api_added / opt_in 表面變更，目前無人歸類。
  - 檢查方法：objdump -p 兩版 元件/{x64,x86}/SKCOM.dll 的 [Ordinal/Name Pointer] Table，comm -23 / comm -13 取移除與新增集合；x86 應看到 stdcall 裝飾名（_GetFutureRights@20 等）。再與 C_Sharp策略王DLL元件使用說明 新增的 5 個查詢方法對齊，確認 kind=api_added 而非僅 wrapper 變更。
- **[medium]** C-34（簡化版 SKDLLCSharp 移除整組海期／海選報價）的 symbols 過度宣稱，破壞範圍被放大了一倍。實測兩版 SKDLLCSharp.dll(x64 Release) 識別字集合差集：被移除的只有 6 個方法（SKOOQuoteLib_RequestStocks / SKOOQuoteLib_RequestTicks / SKOOQuoteLib_GetStockByNoLONG / SKOSQuoteLib_RequestStocks / SKOSQuoteLib_RequestTicks / SKOSQuoteLib_GetStockByNoNineDigitLONG）外加 Format 與 pSKForeign_9LONG 兩個識別字，共 8 個。C-34 另外列的 6 個事件 OnNotifyOSQuoteLONG / OnNotifyOSBest10 / OnNotifyOSTicks / OnNotifyOOQuoteLONG / OnNotifyOOBest10 / OnNotifyOOTicks 在 .59 包裝層仍然完整存在（add_、remove_、RegisterEventOnNotifyOS*、OnNotifyOS*Callback 全部在 .59 的識別字表中）。正確結論應是：事件保留、訂閱與取值方法被拿掉，形成「事件還在但已無法訂閱」的死碼，編譯失敗點只落在那 6 個方法呼叫。
  - 檢查方法：strings -a -n 4 兩版 SKDLLTester/SKDLLTester/bin/x64/Release/SKDLLCSharp.dll，過濾 ^[A-Za-z_][A-Za-z0-9_]{3,}$ 後 sort -u，comm -23 取 .57 獨有（= 真正被移除者，共 8 個）；再 grep -E 'OnNotifyO[SO]' 兩份清單，確認 24 個事件相關識別字兩版都在。
- **[medium]** C-36「修正簡化版 API 下單成功回傳值」至今只有 changelog 一行與文件行為證據，symbols 為空，從未在二進位層確認回傳語意到底怎麼變。這是唯一一項「官方明說回傳值被改」卻沒查證的項目——若簡化版 Send* 的成功判定（0 / Code 物件 / Message）語意變了，簡化版使用者既有的 if(ret==0) 判斷會靜默失效，那會是除 C-13 之外的第二個 must_change/breaking，直接影響裁決摘要「唯一的 must_change」這句結論。兩版 SKDLLCSharp.dll 都在 repo 內，可反編譯驗證，不做等於留一個未定的 breaking 風險。
  - 檢查方法：用 dotnet（本機已有 SDK 8.0.414）跑 System.Reflection.Metadata 或安裝 ilspycmd，dump 兩版 SKDLLCSharp.dll 中 Send* 方法的回傳型別與 IL；重點比對回傳型別是否由 int 改為包裝型別、以及成功路徑回傳的常數。另交叉比對 docdiffs/C_Sharp策略王DLL元件使用說明.md.diff 的下單章節「回傳值」欄與 SKDLLTester/Form1.cs 對回傳值的判斷寫法（.57:1788-1791 vs .59:1028-1031）。
- **[low]** 新事件 OnNotifyLiveKLineData 的 nType 狀態機語意沒有被任何 finding 明文列為開發者必須處理的行為：官方原文為「nType 0:資料重置，需要捨棄目前已收到資料，本筆資料價格都給 0（清盤通知）／1:即時分K（包含回補當日分K）／2:該分鐘每筆Tick」。C-01 僅把 nType 列進 symbols，C-44 只談價格除以 100。任何自建分K 累積器若不處理 nType=0，會把清盤前的錯誤 K 棒留在記憶體，且會把價格全 0 的那一筆當成正常 K 棒寫入。這是一項具體的 should_update 行為要求，屬於 API 表面（事件參數語意）而非範例層。
  - 檢查方法：讀 raw59/13.國內報價.md 的 OnNotifyLiveKLineData 參數表與主手冊 4-4-u 全文，確認 nType 三種值語意；再對照官方範例 SKQuote.cs 的 OnNotifyLiveKLineData 處理函式，檢查它是否對 nType=0 做捨棄處理（若未做，應補進 C-44 那類範例品質問題）。

## 附錄 D：原始材料

- 原始碼 unified diff（忽略 CR/空白）：19 檔，最大者 `SKDLLTester/Form1.Designer.cs`（1,165 行）、`Form1.cs`（913 行）、`SKCOMTester/SKQuote.Designer.cs`（395 行）
- 規格文件 diff：主手冊 1,080 行、`C_Sharp策略王DLL元件使用說明` 203 行、`2.導覽` 152 行、`9.下單-海外期選` 82 行、`7.下單-國內期選` 71 行、`13.國內報價` 51 行
- 工作流腳本與逐代理記錄：`~/.claude/projects/-home-hg-PERSONAL-SKCOM/99d748f2-*/workflows/`、`.../subagents/workflows/wf_edc1764d-e40/journal.jsonl`
