# SKOrderLib — 下單物件：國內證券/期選、海外期選、複委託、智慧單、SGX DMA 專線與 ProxyServer 下單的唯一入口

SKOrderLib 是策略王 COM 元件（SKCOM.dll，經 `Interop.SKCOMLib` 使用）的下單物件。所有委託（含刪改單、智慧單、帳務/庫存/未平倉查詢、出入金互轉、Proxy 下單、SGX DMA 專線）都由本物件送出。使用前必須先以 SKCenterLib 完成登入，再呼叫 `SKOrderLib_Initialize()` 初始化、`ReadCertByID()` 讀取憑證、`GetUserAccount()` 取回交易帳號。方法回傳值多為 LONG 錯誤碼（0＝成功），對照見 [../error_codes.md](../error_codes.md)。

- 文件出處：`api_spec/_raw/4.下單準備介紹.md`、`5.下單-國內證券.md`、`6.下單-證券智慧單.md`、`7.下單-國內期選.md`、`8.下單-國內期選智慧單.md`、`9.下單-海外期選.md`、`10.下單-海外期選智慧單.md`、`11.下單-複委託.md`、`16.SGX_DMA專線.md`、`策略王COM元件使用說明_ProxyServer下單元件.md`、`策略王COM元件使用說明_V2.13.57.md`（4-2 節）
- V2.13.59 增補出處：`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md`（4-2／4-7 節）、同目錄 `4.下單準備介紹.md`（錯誤碼表）、`5.`～`11.`、`16.SGX_DMA專線.md`、`策略王COM元件使用說明_ProxyServer下單元件.md`（該分冊本身仍標示「文件版本：V2.13.47」，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_ProxyServer下單元件.md:6`）、`C_Sharp策略王DLL元件使用說明.md`（簡化版 SKDLLCSharp 包裝層）
- 版本基準：V2.13.59（以 V2.13.57 規格為底增補；差異見 [../changelog_2.13.57_to_2.13.59.md](../changelog_2.13.57_to_2.13.59.md)）
- 版本標記慣例：未標版本者為 V2.13.57 既有內容並經 V2.13.59 手冊複核；新增功能標「V2.13.59 新增」／「V2.13.58 起」，行為與文件更正標「V2.13.58／V2.13.59 修正」；官方 changelog 未列、只能由兩版手冊比對得知的異動，標「V2.13.58／V2.13.59」並註明無從判定版本
- 委託/成交回報的「主動回報」事件（OnNewData 等）屬 SKReplyLib（見 `12.回報.md`），不在本檔範圍

## 總覽：功能分區表

| 分區 | 函式/事件 | 用途一句話 |
|---|---|---|
| 初始化/憑證/帳號 | SKOrderLib_Initialize | 下單物件初始化（一切下單功能的前置） |
| 初始化/憑證/帳號 | ReadCertByID | 讀取/驗證憑證，未讀憑證下單回 1038 |
| 初始化/憑證/帳號 | GetUserAccount | 取回可交易帳號（由 OnAccount 回傳） |
| 初始化/憑證/帳號 | SKOrderLib_GetLoginType | 查詢登入帳號類型（一般/VIP） |
| 初始化/憑證/帳號 | SKOrderLib_GetSpeedyType | 查詢下單線路（一般/Speedy） |
| 初始化/憑證/帳號 | SKOrderLib_MCInitialize | （僅見於範例碼）MC 品牌下單初始化 |
| 初始化/憑證/帳號 | SKOrderLib_UpdateToken | （僅見於範例碼）更新登入 Token，結果由 OnPasswordUpdateToken 回傳 |
| 下單限制 | SetMaxQty | 設定每秒委託「量」上限，超過即上鎖 |
| 下單限制 | SetMaxCount | 設定每秒委託「筆數」上限，超過即上鎖 |
| 下單限制 | UnlockOrder | 解除下單上鎖 |
| 主機測試/LOG | SKOrderLib_PingandTracertTest | Ping/Tracert 測試 API 主機連線 |
| 主機測試/LOG | SKOrderLib_TelnetTest | Telnet 測試主機（OnTelnetTest 回傳） |
| 主機測試/LOG | SKOrderLib_LogUpload | 上傳近 3 日 LOG |
| 回報查詢 | GetOrderReport | 委託回報查詢（阻塞式、字串回傳） |
| 回報查詢 | GetFulfillReport | 成交回報查詢（阻塞式、字串回傳） |
| 國內證券查詢 | GetRealBalanceReport | 證券即時庫存（OnRealBalanceReport） |
| 國內證券查詢 | GetBalanceQuery | 集保庫存查詢（V2.13.54 起停止提供） |
| 國內證券查詢 | GetMarginPurchaseAmountLimit | 資券配額查詢（OnMarginPurchaseAmountLimit） |
| 國內證券查詢 | GetProfitLossGWReport | 新損益試算（未實現/已實現/現股當沖；OnProfitLossGWReport） |
| 國內證券查詢 | GetRequestProfitReport | 舊版證券即時損益試算（即將下線） |
| 國內證券查詢 | GetAvgCost | 查詢昨日未沖銷證券明細 |
| 國內證券查詢 | GetBalance | 一戶通/餘額/可用金額查詢（直接回傳字串） |
| 國內證券查詢 | GetT3DueAmt | 近三日交割款查詢（直接回傳字串） |
| 國內證券查詢 | GetIPAAmt | 台股圈存查詢（直接回傳字串） |
| 國內證券下單 | SendStockOrder | 送出證券委託（適用逐筆交易） |
| 國內證券下單 | SendStockOddLotOrder | 送出盤中零股委託 |
| 國內刪改單（證期權共用） | CorrectPriceBySeqNo | 依 13 碼序號改價 |
| 國內刪改單（證期權共用） | CorrectPriceByBookNo | 依 5 碼書號改價 |
| 國內刪改單（證期權共用） | DecreaseOrderBySeqNo | 依序號委託減量 |
| 國內刪改單（證期權共用） | CancelOrderBySeqNo | 依 13 碼序號刪單 |
| 國內刪改單（證期權共用） | CancelOrderByBookNo | 依 5 碼書號刪單 |
| 國內刪改單（證期權共用） | CancelOrderByStockNo | 依商品代號刪單（帶空可刪全部） |
| 國內刪改單（證期權共用） | CancelOrderByStockNoAdvance | 依商品代號＋買賣別＋價格刪單 |
| 證券智慧單 | SendStockStrategyDayTrade | 當沖條件委託 |
| 證券智慧單 | SendStockStrategyClear | 出清條件委託 |
| 證券智慧單 | SendStockStrategyMIT | MIT 觸價委託（含長效單、預風控） |
| 證券智慧單 | SendStockStrategyOCO | 二擇一 OCO 委託 |
| 證券智慧單 | SendStockStrategyMIOC | 多次 IOC 委託 |
| 證券智慧單 | SendStockStrategyMST | 移動停損委託 |
| 證券智慧單 | SendStockStrategyAB | 看 A 下 B 單委託 |
| 證券智慧單 | SendStockStrategyCB | 自組單委託 |
| 證券智慧單 | SendStockStrategyFTLDayTrade | （僅見於範例碼）快速當沖 OCO 策略 |
| 證券智慧單（已移除） | SendStockStrategyLLS | 漲跌停盯盤單（V2.13.48 移除） |
| 證券智慧單（已移除） | SendStockStrategyMBA | MBA 策略（V2.13.48 移除） |
| 證券智慧單（已移除） | SendStockStrategyMMIT | MIT 多條件策略（V2.13.48 移除） |
| 證券智慧單 | CancelTSStrategyOrder | 取消證券智慧單（MIOC/MST/MIT） |
| 證券智慧單 | CancelTSStrategyOrderV1 | 新版取消證券智慧單（當沖/出清/OCO 用） |
| 智慧單（共用） | CancelStrategyList | 取消多筆智慧單（證/期/海外通用） |
| 證券智慧單 | GetTSSmartStrategyReport | 證券智慧單被動查詢（OnTSSmartStrategyReport） |
| 國內期選查詢 | GetOpenInterestGW | (新)期貨未平倉 GW 查詢（OnOpenInterest＋OnOpenInterestGWStatus） |
| 國內期選查詢 | GetOpenInterest | 舊版期貨未平倉查詢 |
| 國內期選查詢 | GetOpenInterestWithFormat | 期貨未平倉查詢（可指定格式 1~3） |
| 國內期選查詢 | GetFutureRights | 國內權益數查詢（OnFutureRights） |
| 期貨互抵 | SendTFOffset | 大小台/電/金互抵 |
| 期貨互抵 | SendTXOffset | 大小台互抵（舊） |
| 期貨互抵 | SendTFOffsetNew | 大、小、微台互抵 |
| 選擇權組合/拆解 | AssembleOptions | 選擇權組合部位（非交易、無回報） |
| 選擇權組合/拆解 | DisassembleOptions | 複式單拆解（非交易、無回報） |
| 選擇權組合/拆解 | CoverAllProduct | 雙邊部位了結（非交易、無回報） |
| 選擇權組合/拆解 | AllCoverDisOptions | 全拆組單/全拆組平單 |
| 選擇權組合/拆解 | BasketOptionSimulation | （僅見於範例碼）全拆組單試算 |
| 國內期選下單 | SendFutureOrderCLR | 期貨委託（可選倉別、盤別；新客戶用此） |
| 國內期選下單 | SendFutureOrder | 舊版期貨委託（不填倉位、固定盤中） |
| 國內期選下單 | SendOptionOrder | 選擇權委託 |
| 國內期選下單 | SendDuplexOrder | 國內選擇權複式單委託 |
| 期選智慧單 | SendFutureSTPOrderV1 | 新版期貨停損委託（可指定月份、長效單） |
| 期選智慧單 | SendFutureStopLossOrder | 期貨停損委託（限近月代碼） |
| 期選智慧單 | SendOptionStopLossOrder | 選擇權停損委託 |
| 期選智慧單 | SendFutureMSTOrderV1 | 新版移動停損委託（可指定月份） |
| 期選智慧單 | SendMovingStopLossOrder | 移動停損委託（限近月代碼） |
| 期選智慧單 | SendFutureMITOrderV1 | 新版期貨 MIT 委託（可指定月份） |
| 期選智慧單 | SendFutureMITOrder | 期貨 MIT 委託（限近月代碼） |
| 期選智慧單 | SendOptionMITOrder | 選擇權 MIT 委託 |
| 期選智慧單 | SendFutureOCOOrderV1 | 新版期貨二擇一委託（可指定月份、長效單） |
| 期選智慧單 | SendFutureOCOOrder | 期貨二擇一委託（限近月代碼） |
| 期選智慧單 | SendFutureABOrder | 期貨看 A 下 B 委託 |
| 期選智慧單 | CancelTFStrategyOrderV1 | 新版取消期貨智慧單（含書號） |
| 期選智慧單（舊刪單） | CancelFutureStopLoss | 取消期貨停損（舊；建議改用 V1） |
| 期選智慧單（舊刪單） | CancelMovingStopLoss | 取消移動停損（舊；建議改用 V1） |
| 期選智慧單（舊刪單） | CancelOptionStopLoss | 取消選擇權停損（舊；建議改用 V1） |
| 期選智慧單（舊刪單） | CancelFutureOCO | 取消期貨二擇一（舊；建議改用 V1） |
| 期選智慧單（舊刪單） | CancelFutureMIT | 取消期貨 MIT（舊；建議改用 V1） |
| 期選智慧單（舊刪單） | CancelOptionMIT | 取消選擇權 MIT（舊；建議改用 V1） |
| 期選智慧單 | GetStopLossReport | 期貨智慧單被動查詢（OnStopLossReport） |
| 海外商品檔 | SKOrderLib_LoadOSCommodity | 下載海期商品檔（海期下單前置） |
| 海外商品檔 | SKOrderLib_LoadOOCommodity | 下載海選商品檔（海選下單前置） |
| 海外商品檔 | SKOrderLib_LoadOfCommodityGW | （僅見於範例碼）下載海期 GW 商品檔 |
| 海外查詢 | GetOverseaFutures | 查詢海期下單商品（OnOverseaFuture） |
| 海外查詢 | GetOverseaOptions | 查詢海選下單商品（OnOverseaOption） |
| 海外查詢 | GetOverseaFutureOpenInterestGW | 海期未平倉 GW 查詢（彙總/明細；OnOFOpenInterestGWReport） |
| 海外查詢 | GetOverseaFutureOpenInterest | 海期未平倉查詢（OnOverseaFutureOpenInterest） |
| 海外查詢 | GetRequestOverSeaFutureRight | 海外期貨權益數（OnOverSeaFutureRight） |
| 出入金 | WithDraw | 國內外出入金互轉 |
| 出入金 | CapitalPayWithDraw | （僅見於範例碼）一戶通出金 |
| 海外下單 | SendOverseaFutureOrder | 海期委託 |
| 海外下單 | SendOverseaFutureOrderOLID | 海期委託（含自訂資料欄 OLID） |
| 海外下單 | SendOverseaFutureSpreadOrder | 海期價差委託（V2.13.54 起停用，改用 2） |
| 海外下單 | SendOverseaFutureSpreadOrderOLID | 海期價差委託 OLID（同上停用） |
| 海外下單 | SendOverseaFutureSpreadOrder2 | 海期價差委託（新買賣別判斷） |
| 海外下單 | SendOverseaFutureSpreadOrder2OLID | 海期價差委託 2（含 OLID） |
| 海外下單 | SendOverseaOptionOrder | 海選委託 |
| 海外下單 | SendOverseaOptionOrderOLID | 海選委託（含自訂資料欄 OLID；V2.13.58 新增） |
| 海外下單 | OverSeaFutureOrderGW | （僅見於範例碼）海期委託 GW／SGX DMA |
| 海外刪改單 | OverSeaCorrectPriceByBookNo | 海期改價（書號；僅限價改限價） |
| 海外刪改單 | OverSeaCorrectPriceByBookNoOLID | 海期改價 OLID（書號） |
| 海外刪改單 | OverSeaCorrectPriceSpreadByBookNo | 海期價差改價（書號） |
| 海外刪改單 | OverSeaCorrectPriceSpreadByBookNoOLID | 海期價差改價 OLID（書號） |
| 海外刪改單 | OverSeaOptionCorrectPriceByBookNo | 海選改價（書號） |
| 海外刪改單 | OverSeaDecreaseOrderBySeqNo | 海期選減量（序號；SGX DMA 亦用此，15 碼序號） |
| 海外刪改單 | OverSeaDecreaseOrderBySeqNoOLID | 海期減量 OLID（序號） |
| 海外刪改單 | OverSeaCancelOrderBySeqNo | 海期選刪單（序號；SGX DMA 亦用此，15 碼序號） |
| 海外刪改單 | OverSeaCancelOrderBySeqNoOLID | 海期刪單 OLID（序號） |
| 海外刪改單 | OverSeaCancelOrderByBookNo | 海期選刪單（書號） |
| 海外智慧單 | SendOverSeaFutureOCOOrder | 海期二擇一委託（含長效） |
| 海外智慧單 | SendOverSeaFutureABOrder | 海期 AB 單委託 |
| 海外智慧單 | CancelOFStrategyOrder | 取消海期智慧單 |
| 海外智慧單 | GetOFSmartStrategyReport | 海期智慧單被動查詢（OnOFSmartStrategyReport） |
| 複委託 | SendForeignStockOrder | 複委託下單 |
| 複委託 | SendForeignStockOrderOLID | 複委託下單（含自訂資料欄 OLID） |
| 複委託 | CancelForeignStockOrder | 新版複委託刪單（需序號＋書號） |
| 複委託 | GetBankBlock | 複委託外幣圈存查詢（一戶通） |
| 複委託 | GetNTDBlock | 複委託台幣圈存查詢（一戶通） |
| 複委託 | SpecialRequest | （僅見於範例碼）特殊指示申請 |
| SGX DMA 專線 | AddSGXAPIOrderSocket | 建立 SGX API 專線（結果 OnNotifySGXAPIOrderStatus，屬 SKCenterLib 事件） |
| SGX DMA 專線 | OverSeaCorrectPriceBySGXAPISeqNo | SGX 專線改價（僅 15 碼序號，無書號改價） |
| Proxy 連線 | SKOrderLib_InitialProxyByID | 初始化 Proxy 下單連線並登入 |
| Proxy 連線 | ProxyDisconnectByID | 主動斷線 Proxy server |
| Proxy 連線 | ProxyReconnectByID | 重新連線 Proxy server 並登入 |
| Proxy 下單 | SendStockProxyOrder | Proxy 證券下單 |
| Proxy 下單 | SendStockProxyAlter | Proxy 證券刪改單 |
| Proxy 下單 | SendStockProxyPreAlter | Proxy 證券特殊刪改單（V2.13.55 新增） |
| Proxy 下單 | SendFutureProxyOrderCLR | Proxy 期貨委託（含倉別盤別） |
| Proxy 下單 | SendFutureProxyAlter | Proxy 期貨刪改單 |
| Proxy 下單 | SendOptionProxyOrder | Proxy 選擇權委託 |
| Proxy 下單 | SendOptionProxyAlter | Proxy 選擇權刪改單 |
| Proxy 下單 | SendDuplexProxyOrder | Proxy 選擇權複式單 |
| Proxy 下單 | SendOverseaFutureProxyOrder | Proxy 海期委託 |
| Proxy 下單 | SendOverseaFutureSpreadProxyOrder | Proxy 海期價差委託（V2.13.54 起停用，改用 2） |
| Proxy 下單 | SendOverseaFutureSpreadProxyOrder2 | Proxy 海期價差委託（新買賣別判斷） |
| Proxy 下單 | SendOverseaOptionProxyOrder | Proxy 海選委託 |
| Proxy 下單 | SendOverseaFutureProxyAlter | Proxy 海期選刪改單 |
| Proxy 下單 | SendForeignStockProxyOrder | Proxy 複委託下單 |
| Proxy 下單 | SendForeignStockProxyCancel | Proxy 複委託刪單 |
| GW（僅見於範例碼） | SendStockOrderGW | 證券委託 GW（範例中已註解） |
| GW（僅見於範例碼） | AlterStockOrder | 異動證券委託 GW（範例中已註解） |
| GW（僅見於範例碼） | SendFutureOrderGW | 期貨委託 GW（範例中已註解） |
| GW（僅見於範例碼） | AlterTFTOOrder | 期貨委託異動 GW（範例中已註解） |
| GW（僅見於範例碼） | SendOptionOrderGW | 選擇權委託 GW（範例中已註解） |
| GW（僅見於範例碼） | AlterOverSeaFutureOrder | 異動海期委託 GW（範例中已註解） |
| 僅見於範例碼 | DecreaseOrderByBookNo | 依書號委託減量 |
| 事件 | OnAccount | 交易帳號資料回傳（GetUserAccount 觸發） |
| 事件 | OnAsyncOrder | 非同步委託結果 |
| 事件 | OnAsyncOrderOLID | 非同步委託結果（含自訂資料欄） |
| 事件 | OnAsyncOrderGW | （僅見於範例碼）GW 非同步委託結果 |
| 事件 | OnProxyStatus | Proxy 連線/登入狀態（5001 才可下 Proxy 單） |
| 事件 | OnProxyOrder | Proxy 委託結果（每筆委託回兩筆通知） |
| 事件 | OnTelnetTest | Telnet 測試結果 |
| 事件 | OnPasswordUpdateToken | （僅見於範例碼）Token 更新結果 |
| 事件 | OnRealBalanceReport | 證券即時庫存資料 |
| 事件 | OnBalanceQuery | 集保庫存資料（配套函式已停用） |
| 事件 | OnMarginPurchaseAmountLimit | 資券配額資料 |
| 事件 | OnProfitLossGWReport | 證券新損益試算資料 |
| 事件 | OnRequestProfitReport | 舊版證券即時損益資料（即將下線） |
| 事件 | OnTSSmartStrategyReport | 證券智慧單被動回報 |
| 事件 | OnOpenInterest | 國內期貨未平倉資料 |
| 事件 | OnOpenInterestJson | 國內期貨未平倉一次以 JSON 陣列回傳（V2.13.58 新增） |
| 事件 | OnOpenInterestGWStatus | 國內期貨未平倉 GW 查詢狀態 |
| 事件 | OnFutureRights | 國內期貨權益數資料 |
| 事件 | OnFutureRightsStatus | 國內權益數查詢狀態（V2.13.58／V2.13.59 新增，官方 changelog 未列版本） |
| 事件 | OnStopLossReport | 期貨智慧單（停損/MST/MIT/OCO/AB）被動回報 |
| 事件 | OnOverseaFuture | 海期下單商品資料 |
| 事件 | OnOverseaOption | 海選下單商品資料 |
| 事件 | OnOFOpenInterestGWReport | 海期未平倉 GW 資料（彙總/明細） |
| 事件 | OnOverseaFutureOpenInterest | 海期未平倉資料（舊版查詢） |
| 事件 | OnOverseaFutureOpenInterestGWStatus | 海期未平倉 GW 查詢狀態 |
| 事件 | OnOverSeaFutureRight | 海外期貨權益數資料 |
| 事件 | OnOverseaFutureRightsStatus | 海外權益數查詢狀態（V2.13.58／V2.13.59 新增，官方 changelog 未列版本；注意大小寫為 Oversea） |
| 事件 | OnOFSmartStrategyReport | 海期智慧單被動回報 |
| 未公開（勿用） | GetOFOpenInterestWithDetails / OnOpenInterestWithDetails / OnOverseaFutureOpenInterestWithDetails | V2.13.59 Interop 出現此組符號，官方手冊與 changelog 均未記載（見「未公開介面（勿用）」節） |

## 初始化與事件註冊

官方範例的實際寫法（節錄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:28-29,352-386` 與 `SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:21`）：

```csharp
using SKCOMLib;

// 宣告物件（SKCOMTesterV2 寫法）
SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
SKOrderLib  m_pSKOrder  = new SKOrderLib();  // 下單物件

// 事件掛載（SKCOMTester/SKOrder.cs 實際片段；掛一次即可，重複掛載會重複觸發）
m_pSKOrder.OnAccount            += new _ISKOrderLibEvents_OnAccountEventHandler(m_OrderObj_OnAccount);
m_pSKOrder.OnAsyncOrder         += new _ISKOrderLibEvents_OnAsyncOrderEventHandler(m_pSKOrder_OnAsyncOrder);
m_pSKOrder.OnAsyncOrderOLID     += new _ISKOrderLibEvents_OnAsyncOrderOLIDEventHandler(m_pSKOrder_OnAsyncOrderOLID);
m_pSKOrder.OnRealBalanceReport  += new _ISKOrderLibEvents_OnRealBalanceReportEventHandler(m_pSKOrder_OnRealBalanceReport);
m_pSKOrder.OnOpenInterest       += new _ISKOrderLibEvents_OnOpenInterestEventHandler(m_pSKOrder_OnOpenInterest);
m_pSKOrder.OnOverseaFutureOpenInterest += new _ISKOrderLibEvents_OnOverseaFutureOpenInterestEventHandler(m_pSKOrder_OnOverseaFutureOpenInterest);
m_pSKOrder.OnStopLossReport     += new _ISKOrderLibEvents_OnStopLossReportEventHandler(m_pSKOrder_OnStopLossReport);
m_pSKOrder.OnOverseaFuture      += new _ISKOrderLibEvents_OnOverseaFutureEventHandler(m_pSKOrder_OnOverseaFuture);
m_pSKOrder.OnOverseaOption      += new _ISKOrderLibEvents_OnOverseaOptionEventHandler(m_pSKOrder_OnOverseaOption);
m_pSKOrder.OnFutureRights       += new _ISKOrderLibEvents_OnFutureRightsEventHandler(m_pSKOrder_OnFutureRights);
m_pSKOrder.OnRequestProfitReport += new _ISKOrderLibEvents_OnRequestProfitReportEventHandler(m_pSKOrder_OnRequestProfitReport);
m_pSKOrder.OnOverSeaFutureRight += new _ISKOrderLibEvents_OnOverSeaFutureRightEventHandler(m_pSKOrder_OnOverSeaFutureRight);
m_pSKOrder.OnMarginPurchaseAmountLimit += new _ISKOrderLibEvents_OnMarginPurchaseAmountLimitEventHandler(m_pSKOrder_OnMarginPurchaseAmountLimit);
m_pSKOrder.OnBalanceQuery       += new _ISKOrderLibEvents_OnBalanceQueryEventHandler(m_pSKOrder_OnBalanceQueryReport);
m_pSKOrder.OnTSSmartStrategyReport += new _ISKOrderLibEvents_OnTSSmartStrategyReportEventHandler(m_pSKOrder_OnTSStrategyReport);
m_pSKOrder.OnProfitLossGWReport += new _ISKOrderLibEvents_OnProfitLossGWReportEventHandler(m_pSKOrder_OnTSProfitLossGWReport);
m_pSKOrder.OnOFOpenInterestGWReport += new _ISKOrderLibEvents_OnOFOpenInterestGWReportEventHandler(m_pSKOrder_OnOFOpenInterestGW);
m_pSKOrder.OnTelnetTest         += new _ISKOrderLibEvents_OnTelnetTestEventHandler(m_pSKOrder_OnTelnetTest);
m_pSKOrder.OnPasswordUpdateToken += new _ISKOrderLibEvents_OnPasswordUpdateTokenEventHandler(m_pSKOrder_OnPasswordUpdateToken);
m_pSKOrder.OnAsyncOrderGW       += new _ISKOrderLibEvents_OnAsyncOrderGWEventHandler(m_pSKOrder_OnAsyncOrderGW);
m_pSKOrder.OnProxyStatus        += new _ISKOrderLibEvents_OnProxyStatusEventHandler(m_pSKOrder_OnProxyStatus);
m_pSKOrder.OnProxyOrder         += new _ISKOrderLibEvents_OnProxyOrderEventHandler(m_pSKOrder_OnProxyOrder);
m_pSKOrder.OnOFSmartStrategyReport += new _ISKOrderLibEvents_OnOFSmartStrategyReportEventHandler(m_pSKOrder_OnOFStrategyReport);
m_pSKOrder.OnOpenInterestGWStatus += new _ISKOrderLibEvents_OnOpenInterestGWStatusEventHandler(m_pSKOrder_OnOpenInterestGWStatus);
m_pSKOrder.OnOverseaFutureOpenInterestGWStatus += new _ISKOrderLibEvents_OnOverseaFutureOpenInterestGWStatusEventHandler(m_pSKOrder_OnOverseaFutureOpenInterestGWStatus);

// V2.13.58／V2.13.59 新增的三個事件（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:449-451`；
// 同一份範例的另一個初始化按鈕 MCInitialize（MC_Initialize_Click）另有一份幾乎相同的訂閱區塊，見同檔 :1690-1692；
// 兩段皆以 m_bfirst 旗標保護，只會執行其一）
m_pSKOrder.OnFutureRightsStatus += new _ISKOrderLibEvents_OnFutureRightsStatusEventHandler(m_pSKOrder_OnFutureRightsStatus);
m_pSKOrder.OnOverseaFutureRightsStatus += new _ISKOrderLibEvents_OnOverseaFutureRightsStatusEventHandler(m_pSKOrder_OnOverseaFutureRightsStatus);
m_pSKOrder.OnOpenInterestJson += new _ISKOrderLibEvents_OnOpenInterestJsonEventHandler(m_pSKOrder_OnOpenInterestJson);

// 初始化（登入成功後執行）
int m_nCode = m_pSKOrder.SKOrderLib_Initialize();
// 讀取憑證（初始化成功後、下單前必做；群組帳號需對每個 ID 依序執行）
m_nCode = m_pSKOrder.ReadCertByID(m_strLoginID);
// 取回交易帳號（結果由 OnAccount 事件回傳）
m_nCode = m_pSKOrder.GetUserAccount();
```

標準前置流程：SKCenterLib 登入 → `SKOrderLib_Initialize()` → `ReadCertByID()` → `GetUserAccount()`（帳號由 OnAccount 取得）→（海外）`SKOrderLib_LoadOSCommodity()`/`SKOrderLib_LoadOOCommodity()` →（Proxy）`SKOrderLib_InitialProxyByID()` 並等 OnProxyStatus=5001。

## 共用結構物件（Struct）

下單方法的 `pOrder` 參數皆為 SKCOMLib 中的 struct，欄位如下（各方法參數表以此為準；欄位語意依委託類型不同，詳見各 struct 變體註解）。

### STOCKORDER（證券下單物件；出處 `5.下單-國內證券.md`）

```csharp
struct STOCKORDER {
  string bstrFullAccount; // 證券帳號：分公司代碼＋帳號7碼
  string bstrStockNo;     // 委託股票代號
  short  sPrime;          // 0:上市上櫃 1:興櫃
  short  sPeriod;         // 0:盤中 1:盤後 2:零股 4:盤中零股
  short  sFlag;           // 0:現股 1:融資 2:融券 3:無券（盤中零股僅 0）
  short  sBuySell;        // 0:買進 1:賣出
  string bstrPrice;       // 委託價；「M」參考價(昨收)、「H」漲停、「L」跌停
  int    nQty;            // 整股=張數；零股=股數(盤中零股 1~999 股)
  int    nTradeType;        // [逐筆] 0:ROD 1:IOC 2:FOK
  int    nSpecialTradeType; // [逐筆] 1:市價 2:限價（市價 Price 給 0；限價 Price 不可 0）
}
```

### TSPROFITLOSSGWQUERY（新損益試算查詢物件；出處 `5.下單-國內證券.md`）

```csharp
struct TSPROFITLOSSGWQUERY {
  string bstrFullAccount; // 證券帳號：分公司四碼＋帳號7碼
  int    nTPQueryType;    // 損益類別 0:未實現 1:已實現 2:現股當沖
  int    nFunc;           // 回傳格式：未實現 0彙總/1明細；已實現 0彙總/1明細/2投資總額/3彙總(依股票代號)；現股當沖 1彙總/2明細
  string bstrStockNo;     // 明細時指定商品代碼
  string bstrTradeType;   // 明細時指定交易類別（0:現股 1:融資(代) 2:融券(代) 3:融資(自) 4:融券(自) 6:現沖 7:信用當沖 8:券差 9:無券賣出）
  string bstrStartDate;   // 已實現彙總:西元YYYYMMDD；明細/彙總依代號:民國年月日
  string bstrEndDate;     // 已實現彙總:西元YYYYMMDD；明細不需填
  string bstrBookNo;      // 明細時指定委託書號
  string bstrSeqNo;       // 明細時指定序號
}
```

### SKAVGCOST（昨日未沖銷明細查詢物件；出處 `5.下單-國內證券.md`）

```csharp
struct SKAVGCOST {
  string bstrAccount; // 證券帳號：分公司四碼＋帳號7碼
  string bstrFunc;    // 功能（0:查詢昨日未沖銷明細）
  string bstrStockNo; // 股票代號（空白=全部）
}
```

### STOCKPROXYORDER（Proxy 證券下單/刪改單物件；出處 `5.下單-國內證券.md`、ProxyServer 文件）

```csharp
struct STOCKPROXYORDER {
  string bstrFullAccount;   // 證券帳號：分公司代碼＋帳號7碼
  string bstrStockNo;       // 委託股票代號
  string bstrOrderType;     // 下單:1現買 2現賣 3資買 4資賣 5券買 6券賣 7無券賣；刪改:0刪單 1改量 2改價
  int    nSpecialTradeType; // 1:市價 2:限價
  int    nPeriod;           // 0:盤中 1:零股 2:盤後 3:盤中零股
  string bstrPrice;         // 委託價（改價:必填,""=市價；減量:0；刪單:""或原始價）
  int    nQty;              // 股數（改價:0；減量:要減的量；刪單:原始量）
  int    nTradeType;        // 0:ROD 1:IOC 2:FOK
  int    nPriceMark;        // 價格旗標 0:一般定價 1:前日收盤價 2:漲停 3:跌停
  string bstrBookNo;        // 委託書號（刪改用）
  string bstrSeqNo;         // 委託序號（刪改用；特殊刪改單帶 "" 會刪證券市場全部委託）
  string bstrPrice_forD;    // [特殊刪改]原委託價（商品代號刪單情境）
  string bstrBuySell_forD;  // [特殊刪改]原委託買賣別 0:BUY 1:SELL
}
```

### FUTUREORDER（期權下單物件；出處 `7.下單-國內期選.md`、`8.下單-國內期選智慧單.md`）

```csharp
struct FUTUREORDER {
  string bstrFullAccount;  // 期貨帳號：Broker id（例:F020000）＋帳號7碼
  string bstrStockNo;      // 委託期權代號；價差單填 "近月/遠月"（sBuySell 為近月方向）；複式/組合為代號1
  string bstrStockNo2;     // 委託選擇權代號2（複式單/組合部位）
  short  sTradeType;       // 一般:0:ROD 1:IOC 2:FOK；智慧單:0:ROD 3:IOC 4:FOK（複式單僅 IOC/FOK）
  short  sBuySell;         // 0:買進 1:賣出
  short  sBuySell2;        // 0:買進 1:賣出（複式單/組合第二腳）
  short  sDayTrade;        // 當沖 0:否 1:是（可當沖商品依交易所規定）
  short  sNewClose;        // 0:新倉 1:平倉 2:自動
  string bstrPrice;        // 委託價（IOC/FOK 可用 M=市價、P=範圍市價；智慧單請改用 nOrderPriceType，勿用 P）；[OCO]停利委託價
  string bstrPrice2;       // [OCO]停損委託價（第二腳委託價）
  int    nQty;             // 交易口數
  string bstrTrigger;      // 觸發價（停損/MST/MIT/OCO 用，不可 0、不可特殊價代碼）；[OCO]停利觸發價
  string bstrTrigger2;     // [OCO]停損觸發價（第二腳，市價小於觸發價2時觸發）
  string bstrMovingPoint;  // 移動點數（僅移動停損 MST 用）
  short  sReserved;        // 盤別 0:盤中(T盤及T+1盤) 1:T盤預約（MIT 不須填）
  string bstrDealPrice;    // 成交價（MIT 必填：委託當下市價，不可 0）；[AB]A 商品成交價
  string bstrSettlementMonth;  // 商品契約年月 YYYYMM（TX00/MTX00 等近月代碼可免填）；[AB]B 商品月份
  string bstrSettlementMonth2; // [AB]B 商品契約月份2（價差用，非價差填 0）
  int    nOrderPriceType;  // 委託價類別 2:限價 3:範圍市價（智慧單不支援市價；ROD 僅可限價）
  int    nTriggerDirection;// 觸發方向 1:GTE(大於等於) 2:LTE(小於等於)（MIT/AB 用）
  int    nLongActionFlag;  // 是否長效單 0:否 1:是（STP/OCO，V2.13.45+）
  string bstrLongEndDate;  // 長效單結束日 YYYYMMDD
  int    nLAType;          // 觸發結束條件 1:效期內觸發即失效 3:效期內完全成交即失效
  short  nTimeFlag;        // [OCO]預約/長效盤別 1:T盤 2:T+1盤 3:全盤；[AB]是否預約單/上市上櫃註記（依單型語意不同）
  int    nMarketNo;        // [AB]A 商品市場編號 1:國內證 2:國內期 3:國外證 4:國外期
  string bstrCIDTandem;    // [AB]A 商品交易所代碼（TSE/TAIFEX/CME）；SendFutureOrderCLR V2.13.54+ 亦可用（帶 FITX＋bstrSettlementMonth）
  int    nCallPut;         // [AB]B 商品是否選擇權 0:否 1:Call 2:Put
  string bstrStrikePrice;  // [AB]履約價（非選擇權填 0）
  int    nFlag;            // [AB]是否委託價差商品 0:否 1:是
  int    nFunType;         // [AllCoverDisOptions]功能 1:全拆組 2:全拆組平
}
```

### FUTUREOCOORDER（舊版二擇一 OCO 下單物件；出處 `8.下單-國內期選智慧單.md`）

```csharp
struct FUTUREOCOORDER {
  string bstrFullAccount; // 期貨帳號：Broker id＋帳號7碼
  string bstrStockNo;     // 委託期權代號（限近月代碼）
  string bstrPrice;       // 第一腳委託價
  string bstrTrigger;     // 第一腳觸發價（市價大於觸發價1時觸發）
  string bstrPrice2;      // 第二腳委託價
  string bstrTrigger2;    // 第二腳觸發價（市價小於觸發價2時觸發）
  short  sTradeType;      // 3:IOC 4:FOK
  short  sBuySell;        // 第一腳 0:買進 1:賣出
  short  sBuySell2;       // 第二腳 0:買進 1:賣出
  short  sDayTrade;       // 當沖 0:否 1:是
  short  sNewClose;       // 0:新倉 1:平倉 2:自動
  int    nQty;            // 交易口數
  int    sOrderPriceType1;// 兩腳委託價類型 2:限價 3:範圍市價（不支援市價）
  short  sReserved;       // 盤別 0:盤中 1:預約
}
```

### FUTUREPROXYORDER（Proxy 期選下單/刪改單物件；出處 `7.下單-國內期選.md`、ProxyServer 文件）

```csharp
struct FUTUREPROXYORDER {
  string bstrFullAccount; // 期貨帳號：Broker id＋帳號7碼
  string bstrStockNo;     // 委託商品代號（期貨 ex:FITX；選擇權 ex:TXO；週選 ex:TX4＋bstrSettleYM）
  string bstrSettleYM;    // 指定月份商品契約年月 6 碼 EX:202212（近月商品 TX00 → FITX＋近月月份）
  string bstrSettleYM2;   // [複式單]契約年月2
  string bstrStrike;      // [選擇權]履約價1
  string bstrStrike2;     // [複式單]履約價2
  int    nCP;             // [選擇權]0:CALL 1:PUT
  int    nCP2;            // [複式單]買賣權2
  int    nBuySell;        // 0:買進 1:賣出
  int    nBuySell2;       // [複式單]買賣別2
  int    nPriceFlag;      // 0:市價 1:限價 2:範圍市價
  int    nDayTrade;       // 當沖 0:否 1:是
  string bstrOrderType;   // 下單:0新倉 1平倉 2自動；刪改:0刪單 1減量 2改價
  int    nReserved;       // 0:盤中單 1:預約單
  int    nQty;            // 交易口數（改量:要減的量；改價:0 或 ""；刪單:原始量）
  string bstrPrice;       // 委託價（改量:0 或 ""；改價:新價；刪單:原始價或 ""）
  int    nTradeType;      // 0:ROD 1:IOC 2:FOK（複式單僅 1:IOC 2:FOK）
  string bstrBookNo;      // [刪改]委託書號
  string bstrSeqNo;       // [刪改]委託序號
}
```

### STOCKSTRATEGYORDER（證券智慧單物件-當沖/CB；出處 `6.下單-證券智慧單.md`）

```csharp
struct STOCKSTRATEGYORDER {
  string bstrFullAccount;    // 證券帳號：分公司四碼＋帳號7碼
  // 商品
  string bstrStockNo;        // 委託股票代號
  int    nBuySell;           // 當沖進場 0:現股買 1:無券賣出；CB 0:買 1:賣
  int    nOrderType;         // [CB]委託交易別 0:現股 3:融資 4:融券 8:無券普賣
  int    nQty;               // 委託張數
  int    nOrderPriceCond;    // 委託時效 0:ROD 3:IOC 4:FOK
  string bstrOrderPrice;     // 進場委託價／[CB]委託價
  int    nOrderPriceType;    // 委託價類別 1:市價 2:限價
  // 當沖進場 MIT 條件
  int    nInnerOrderIsMIT;   // 進場是否 MIT 0:N 1:Y
  int    nMITDir;            // 進場 MIT 觸價方向 0:未啟用 1:向上(≧) 2:向下(≦)
  string bstrMITTriggerPrice;// 進場 MIT 觸發價（未啟用填 0）
  string bstrMITDealPrice;   // 進場 MIT 當下市價（未啟用填 0）
  // 停損出場
  int    nStopLossFlag;      // 停損 0:否 1:是
  int    nRDOSLPercent;      // 停損類型 0:觸發價 1:漲跌幅
  string bstrSLTrigger;      // 停損觸發價
  string bstrSLPercent;      // 停損百分比
  string bstrSLOrderPrice;   // 停損委託價
  int    nRDSLMarketPriceType; // 停損委託價方式 1:市價 2:限價
  int    nStopLossOrderCond; // 停損出場時效 0:ROD 3:IOC 4:FOK
  // 停利出場
  int    nTakeProfitFlag;    // 停利 0:否 1:是
  int    nRDOTPPercent;      // 停利類型 0:觸發價 1:漲幅
  string bstrTPTrigger;      // 停利觸發價
  string bstrTPPercent;      // 停利百分比
  string bstrTPOrderPrice;   // 停利委託價
  int    nRDTPMarketPriceType; // 停利委託價方式 1:市價 2:限價
  int    nTakeProfitOrderCond; // 停利出場時效 0:ROD 3:IOC 4:FOK
  // 時間出清
  int    nClearAllFlag;      // 時間出清 0:否 1:是；[CB]是否自訂啟動時間 0:立即 1:自訂
  string bstrClearCancelTime;// 出清時間 hhmm（每日 13:20 截止）；[CB]自訂啟動時間 hhmmss（14 天內）
  string bstrClearAllOrderPrice; // 出清委託價
  int    nClearAllPriceType; // 出清委託價方式 1:市價 2:限價
  int    nClearOrderCond;    // 出清出場時效 0:ROD 3:IOC 4:FOK
  // 盤後定價出場
  int    nFinalClearFlag;    // 盤後定盤交易 0:否 1:是；[CB]條件關係 0:任一成立 1:全部成立
}
```

### STOCKSTRATEGYORDEROUT（證券智慧單物件-出清；出處 `6.下單-證券智慧單.md`）

```csharp
struct STOCKSTRATEGYORDEROUT {
  string bstrFullAccount;   // 證券帳號：分公司四碼＋帳號7碼
  string bstrStockNo;       // 委託股票代號
  int    nBuySell;          // 0:買 1:賣
  int    nOrderType;        // 出清進場委託交易別 0:現股 3:融資 4:融券
  int    nQty;              // 委託張數
  int    nGTEFlag;          // 成交價大於指定價觸發 0:否 1:是
  string bstrGTETriggerPrice; string bstrGTEOrderPrice; int nGTEMarketPrice; int nGTEOrderCond;
  int    nLTEFlag;          // 成交價小於指定價觸發 0:否 1:是
  string bstrLTETriggerPrice; string bstrLTEOrderPrice; int nLTEMarketPrice; int nLTEOrderCond;
  int    nClearAllFlag;     // 時間出清 0:否 1:是
  string bstrClearCancelTime; string bstrClearAllOrderPrice; int nClearAllPriceType; int nClearOrderCond;
  int    nFinalClearFlag;   // 盤後定盤交易 0:否 1:是
} // 大於/小於條件的 MarketPrice: 1市價 2限價；OrderCond: 0ROD 3IOC 4FOK
```

### STOCKSTRATEGYORDERMIT（證券智慧單物件-MIT/MST/AB；出處 `6.下單-證券智慧單.md`）

```csharp
struct STOCKSTRATEGYORDERMIT {
  string bstrFullAccount;  // 證券帳號：分公司四碼＋帳號7碼
  string bstrDealPrice;    // 成交價（當下市價，洗價機留存用）
  string bstrStockNo;      // 委託股票代號；[AB]為 B 商品
  int    nPrime;           // [MST/MIOC]0:上市 1:上櫃
  int    nOrderType;       // 委託交易別 0:現股 3:融資 4:融券 8:無券普賣
  int    nOrderCond;       // 委託時效 0:ROD 3:IOC 4:FOK
  int    nBuySell;         // 0:買 1:賣
  int    nTriggerDir;      // 觸價方向 1:GTE(≧) 2:LTE(≦)
  string bstrTriggerPrice; // 觸發價
  int    nQty;             // 委託張數
  string bstrOrderPrice;   // 委託價（市價單填 0）
  int    nOrderPriceType;  // 委託價類別 1:市價 2:限價
  int    nLongActionFlag;  // 長效單 0:否 1:是（V2.13.45+）
  string bstrLongEndDate;  // 長效單結束日 YYYYMMDD
  int    nLAType;          // 觸發結束條件 1:觸發即失效 3:完全成交即失效
  int    nPreRiskFlag;     // 預風控 0:關閉 1:開啟（不支援信用交易）
  // AB 單專用（看 A）
  string bstrStockNo2;     // A 商品代號
  int    nMarketNo;        // A 商品市場 1:國內證 2:國內期 3:國外證 4:國外期
  string bstrExchangeNo;   // A 商品交易所代碼（TSE/TAIFEX/CME）
  string bstrStartPrice;   // A 商品成交價
  int    nReserved;        // 是否預約單 0:否 1:是（A 為國內期選商品時可選）
}
```

### STOCKSTRATEGYORDEROCO / STOCKSTRATEGYORDERMIOC（證券 OCO／多次 IOC；出處 `6.下單-證券智慧單.md`）

```csharp
struct STOCKSTRATEGYORDEROCO {  // v2.13.48 新增無券普賣委託別
  string bstrFullAccount;  // 證券帳號：分公司四碼＋帳號7碼
  string bstrStockNo;      // 委託股票代號（其餘組合欄位文件表格未完整抽出，見原 docx）
}
struct STOCKSTRATEGYORDERMIOC {
  string bstrFullAccount;    // 證券帳號：分公司四碼＋帳號7碼
  string bstrStockNo;        // 委託商品代號
  int    nPrime;             // 0:上市 1:上櫃
  int    nOrderType;         // 0:現 3:(自)融資 4:(自)融券 8:無券賣
  int    nBuySell;           // 0:買進 1:賣出
  int    nOrderPriceType;    // 1:市價 2:(買單)委賣價或(賣單)委買價（實際價格由中台決定）
  int    nOneceQtyLimit;     // 單次交易張數上限
  string bstrOrderPriceUp;   // 委託價上限
  string bstrOrderPriceDown; // 委託價下限
  int    nTotalQty;          // 總委張數
}
```

### CANCELSTRATEGYORDER（智慧單刪單物件；出處 `6.` / `8.` / `10.` 各章）

```csharp
struct CANCELSTRATEGYORDER {
  string bstrLogInID;        // 登入ID（期貨刪單 V1 用）
  string bstrFullAccount;    // 帳號（證券:分公司四碼＋帳號7碼；期貨:期貨帳號）
  int    nMarket;            // 市場別 1:國內證 2:國內期 3:國外證 4:國外期（AB 單選 A 商品市場）
  string bstrParentSmartKey; // [證券當沖]智慧母單號
  string bstrSmartKey;       // 智慧單號（出場單給出場單號；多筆刪單以逗號分隔 EX:682020,682021）
  int    nTradeKind;         // 3:OCO 5:STP 8:MIT 9:MST 10:AB 11:當沖 17:出清 27:CB（證券 CancelTSStrategyOrder 用 6:MIOC 7:MST 8:MIT）
  int    nDeleteType;        // [證券當沖]刪單類型 1:全部 2:進場單 3:出場單
  string bstrSeqNo;          // 委託序號（預約單可忽略）
  string bstrOrderNo;        // 委託書號（已觸發需給書號；未填影響解除保證金風控）
  string bstrSmartKeyOut;    // [證券當沖]出場單號（刪全部時提供；多筆以逗號分隔）
  string bstrLongActionKey;  // 長效單號（非長效單輸入 0；V2.13.45+）
}
```

### OVERSEAFUTUREORDER（海外期權下單物件；出處 `9.` / `10.` / `16.` / ProxyServer 文件）

```csharp
struct OVERSEAFUTUREORDER {
  string bstrFullAccount;     // 海期帳號：分公司代碼＋帳號7碼
  string bstrExchangeNo;      // 交易所代碼（EX: CME）；[AB]A 商品交易所
  string bstrExchangeNo2;     // [AB]B 商品交易所代碼
  string bstrStockNo;         // 海外期權代號（EX: ES）；[AB]為 B 商品
  string bstrStockNo2;        // [AB]A 商品代號
  string bstrYearMonth;       // 近月商品年月 YYYYMM 6 碼
  string bstrYearMonth2;      // 遠月商品年月 YYYYMM（價差單用；非價差填 0）
  string bstrOrder;           // 委託價；[OCO]第一腳委託價；[AB]B 委託價
  string bstrOrderNumerator;  // 委託價分子（若無填 0）
  string bstrOrderDenominator;// 委託價分母（海選/刪改/OCO 用）
  string bstrOrder2;          // [OCO]第二腳委託價；[AB]A 商品市價
  string bstrOrderNumerator2; string bstrOrderDenominator2; // [OCO]第二腳分子/分母
  string bstrTrigger;         // 觸發價（STP/STL 用）；[OCO]第一腳觸發價(大於)；[AB]觸發價
  string bstrTriggerNumerator; string bstrTriggerDenominator; // 觸發價分子/分母（若無填 0）
  string bstrTrigger2;        // [OCO]第二腳觸發價(小於)
  string bstrTriggerNumerator2; string bstrTriggerDenominator2; // [OCO]第二腳觸發價分子/分母
  short  sBuySell;            // 0:買進 1:賣出（價差商品注意近遠月前 +/- 符號；種類 6/8 以遠月為基準、7 以近月為基準）
  int    nBuySell2;           // [OCO/AB]第二腳/價差買賣別2（非價差填 0）
  short  sNewClose;           // 0:新倉（海期僅新倉）1:平倉（海選可用）
  short  sDayTrade;           // 當沖 0:否 1:是（海期價差不提供當沖）
  short  sTradeType;          // 委託時效：海期 0:ROD 1:IOC 2:FOK（文件另一處作 0:ROD 1:FOK 2:IOC，以實測為準；限價可選，其餘固定 ROD）；海選固定 0:ROD；智慧單 0:ROD 3:IOC 4:FOK
  short  sSpecialTradeType;   // 0:LMT 限價 1:MKT 市價 2:STL 停損限價 3:STP 停損市價；[AB]0:非證券 1:上市 2:上櫃
  string bstrStrikePrice;     // 履約價（選擇權用）
  short  sCallPut;            // 0:CALL 1:PUT（選擇權用）；[AB]0:否 1:Call 2:Put
  int    nQty;                // 交易口數
  int    nQty2;               // [GW]複式單口數
  int    nOrderPriceType;     // [OCO/AB]委託價類別 1:市價 2:限價
  int    nTriggerDirection;   // [AB]觸價方向 1:GTE 2:LTE
  int    nMarketNo;           // [AB]A 商品市場編號 1:國內證 2:國內期 3:國外證 4:國外期
  int    nSpreadFlag;         // [Proxy 刪改]0:OF 海期 1:OF-spread 價差 2:OO 海選；[AB]是否價差商品
  int    nAlterType;          // [Proxy 刪改]0:刪單 1:減量 2:改價
  string bstrBookNo;          // [Proxy 刪改]書號
  string bstrSeqNo;           // [Proxy 刪改]原始 13 碼序號
  int    nReserved;           // [OCO]預約單 0:否 1:是
  int    nTimeFlag;           // [OCO]預約盤別 1:T盤 2:T+1盤 3:全盤；[AB]是否預約單（A 為國內期選時可選）
  int    nLongActionFlag;     // [OCO]長效單 0:否 1:是
  string bstrLongEndDate;     // [OCO]長效單結束日 YYYYMMDD
  int    nLAType;             // [OCO]觸發結束條件 1:觸發即失效 3:完全成交即失效
}
```

### OVERSEAFUTUREORDERFORGW（海期委託 GW 格式物件—改價用；出處 `9.下單-海外期選.md`）

```csharp
struct OVERSEAFUTUREORDERFORGW {
  string bstrFullAccount;     // 海期帳號：分公司四碼＋帳號7碼
  string bstrExchangeNo;      // 交易所代碼
  string bstrStockNo;         // 期權代號
  string bstrStockNo2;        // 期價差商品代號（價差改價用）
  string bstrYearMonth;       // 近月商品年月 YYYYMM
  string bstrYearMonth2;      // 遠月商品年月 YYYYMM（價差改價用）
  string bstrOrderPrice;      // 新委託價
  string bstrOrderNumerator;  // 新委託價分子
  string bstrOrderDenominator;// 新委託價分母
  string bstrTriggerPrice; string bstrTriggerNumerator; string bstrTriggerDenominator; // 目前未使用可忽略
  string bstrStrikePrice;     // 履約價（選擇權改價用）
  string bstrBookNo;          // 委託書號
  string bstrSeqNo;           // 委託序號
  int    nBuySell;  int nBuySell2; // 0:買進 1:賣出（改價可忽略）
  int    nNewClose;           // 0:新倉 1:平倉 2:自動（海期僅新倉，海選新/平倉）
  int    nDayTrade;           // 當沖 0:否 1:是（改價可忽略）
  int    nTradeType;          // 0:ROD 3:IOC 4:FOK（改價目前固定 ROD）
  int    nSpecialTradeType;   // 0:LMT（目前僅限價改限價）
  int    nCallPut;            // 0:CALL 1:PUT（選擇權改價用）
  int    nQty; int nQty2;     // 交易口數（改價可忽略）
}
```

### FOREIGNORDER / OSSTOCKPROXYORDER（複委託下單/刪單物件；出處 `11.下單-複委託.md`）

```csharp
struct FOREIGNORDER {
  string bstrFullAccount; // 複委託帳號：分公司代碼＋帳號7碼
  string bstrStockNo;     // 委託股票代號
  string bstrExchangeNo;  // 交易所：US美 HK港 JP日 SP新加坡 SG新(幣)坡股 SA滬 HA深（V2.13.46+）
  string bstrPrice;       // 委託價格
  string bstrCurrency1;   // 扣款幣別順序1（HKD/NTD/USD/JPY/SGD/EUR/AUD/CNY/GBP）
  string bstrCurrency2;   // 扣款幣別順序2
  string bstrCurrency3;   // 扣款幣別順序3
  string bstrSeqNo;       // [刪單必填]委託序號
  string bstrBookNo;      // [刪單必填]委託書號
  int    nAccountType;    // 專戶別 1:外幣專戶 2:台幣專戶
  int    nQty;            // 委託量
  int    nOrderType;      // 1:買 2:賣 4:刪單
  int    nTradeType;      // 庫存別（賣美股必填）1:[美股]一般/定股(CITI) 2:定額(VIEWTRADE)
}
struct OSSTOCKPROXYORDER { // Proxy 複委託：同上，但委託量為 bstrProxyQty（string；
  // 僅賣出美股且庫存別為定額(VIEWTRADE)時可帶小數股數，其餘須整數），nTradeType 另有 3:其他股市(一般)，非賣美股可填 0
}
```

## 方法

> 簽名為 C# 實際呼叫樣式（Interop.SKCOMLib）。COM 原型的 `[in] struct X* pOrder` 在部分範例以 `ref` 傳遞（SKCOMTesterV2）、部分直接傳值（SKCOMTester），依所引用的 Interop 版本而定。回傳型別 LONG 對應 C# `int`。範例路徑相對於 repo 根目錄。

### 初始化 / 憑證 / 帳號

### SKOrderLib_Initialize
- 用途：下單物件初始化。產生 SKOrderLib 物件後需先執行，才可使用其他下單相關函式。
- 簽名：`int SKOrderLib_Initialize()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0＝初始化成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)）。
- 備註：前置條件為 SKCenterLib 登入成功（否則回 1000 SK_ERROR_LOGIN_FIRST）。別名/呼叫名核對：docx（`4.下單準備介紹.md`）與程式碼註解偶以「Initialize」泛稱本函式，惟 SKOrderLib 對外方法名稱固定為 `SKOrderLib_Initialize`（grep `Source_code/**/*.cs` 未見裸 `.Initialize(` 呼叫點）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:382`

### ReadCertByID
- 用途：讀取/驗證憑證。初始化成功後、送出任何委託前必做；群組帳號需對所有 ID 依序執行。
- 簽名：`int ReadCertByID(string bstrLogInID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入帳號 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：未讀憑證即下單會得到 SK_ERROR_ORDER_SIGN_INVALID（1011）；憑證不存在回 1045（請先匯入憑證）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:394`

### GetUserAccount
- 用途：取回目前可交易的所有帳號，資料由 OnAccount 事件回傳。
- 簽名：`int GetUserAccount()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：登入前需先簽署證券或期貨 API 下單聲明書，否則取不到該市場帳號（重簽後需重新登入）。對應事件：OnAccount。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SKProxyOrder/SKProxySendOrderForm/TSSKProxySendOrderForm.cs:157`

### SKOrderLib_GetLoginType
- 用途：查詢登入帳號類型。
- 簽名：`int SKOrderLib_GetLoginType(string bstrLogInID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入帳號 |

- 回傳：0：一般帳號，1：VIP 帳號（非錯誤碼語意）。
- 備註：無。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:513`

### SKOrderLib_GetSpeedyType
- 用途：查詢登入帳號下單線路。
- 簽名：`int SKOrderLib_GetSpeedyType(string bstrLogInID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入帳號 |

- 回傳：0：一般線路，1：Speedy 線路（非錯誤碼語意）。
- 備註：無。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:523`

### 下單限制

### SetMaxQty
- 用途：設定每秒委託「量」上限；一秒內超過設定值，該市場下單將被鎖定，需 UnlockOrder 解鎖。
- 簽名：`int SetMaxQty(int nMarketType, int nMaxQty)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nMarketType | int | 0:TS(證券) 1:TF(期貨) 2:TO(選擇權) 3:OS(複委託) 4:OF(海外期貨) 5:OO(海外選擇權) |
| nMaxQty | int | 委託量上限；小於等於 0 為無限制 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：被上鎖後下單回 1040 SK_ERROR_ORDER_LOCK。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:421`

### SetMaxCount
- 用途：設定每秒委託「筆數」上限；超過即鎖定該市場下單。
- 簽名：`int SetMaxCount(int nMarketType, int nMaxCount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nMarketType | int | 同 SetMaxQty 的市場代碼 0~5 |
| nMaxCount | int | 委託筆數上限；小於等於 0 為無限制 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：被上鎖後需 UnlockOrder 解鎖。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:431`

### UnlockOrder
- 用途：下單解鎖。下單函式因超過限制被上鎖後，經此函式解鎖才可繼續下單。
- 簽名：`int UnlockOrder(int nMarketType)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nMarketType | int | 同 SetMaxQty 的市場代碼 0~5 |

- 回傳：LONG 錯誤碼；0 成功（市場未上鎖時回 2006 SK_WARNING_ORDER_DID_NOT_LOCKED）。
- 備註：無。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:437`

### 主機測試 / LOG

### SKOrderLib_PingandTracertTest
- 用途：以 Ping 和 Tracert 測試 API 使用到的主機是否正常。
- 簽名：`int SKOrderLib_PingandTracertTest()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：適用出現微軟 socket error 時確認主機連線；結果寫入 LOG 檔「PingTest」，看到 `TracertTest -> XXX` 才算測試完成。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1516`

### SKOrderLib_TelnetTest
- 用途：以 Telnet 指令測試與 API 主機連線是否正常。
- 簽名：`int SKOrderLib_TelnetTest()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnTelnetTest 事件回傳（「測試中」→「共測試x台主機 : 成功y台, 失敗z台」）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1523`

### SKOrderLib_LogUpload
- 用途：上傳近 3 日 LOG。
- 簽名：`int SKOrderLib_LogUpload()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：上傳後 LOG 路徑會生成一個資料夾與一個 zip，資料夾內為上傳的檔案。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1625`

### 回報查詢

### GetOrderReport
- 用途：委託回報查詢（國內證期選；同步阻塞式，直接回傳字串）。
- 簽名：`string GetOrderReport(string bstrLogInID, string bstrFullAccount, int nFormat)`（COM 宣告：`void GetOrderReport([in] BSTR, [in] BSTR, [in] LONG, [out,retval] BSTR*)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入帳號 |
| bstrFullAccount | string | 完整帳號：分公司代號＋交易帳號 |
| nFormat | int | 1:全部 2:有效 3:可消 4:已消 5:已成 6:失敗 7:合併同價格 8:合併同商品 9:預約 |

- 回傳：委託回報內容字串；欄位以「,」分隔、每筆以 `\r\n` 分段；異常時 M999:查詢錯誤、M003:查無資料。欄位定義（格式 1~6、9 共 70 欄，含市場別/委託狀態/委託價量/海期分子分母/SGX 上手書號等）見 `api_spec/_raw/4.下單準備介紹.md:160-233`。
- 備註：查詢為阻塞式，建議以執行緒查詢；每次查詢需間隔 5 秒；回報不含盤中零股；中文編碼 UTF-8。SGX DMA 委託可由欄位 61「上手書號」比對。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1928`

### GetFulfillReport
- 用途：成交回報查詢（國內證期選；同步阻塞式，直接回傳字串）。
- 簽名：`string GetFulfillReport(string bstrLogInID, string bstrFullAccount, int nFormat)`（COM 宣告同上為 retval 形式）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入帳號 |
| bstrFullAccount | string | 完整帳號：分公司代號＋交易帳號 |
| nFormat | int | 1:完整 2:合併同書號 3:合併同價格 4:合併同商品 5:T+1成交回報 |

- 回傳：成交回報內容字串；格式同 GetOrderReport（欄位定義見 `api_spec/_raw/4.下單準備介紹.md:315-449`）。
- 備註：阻塞式、5 秒間隔、不含盤中零股、UTF-8，同 GetOrderReport。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1943`

### 國內證券查詢

### GetRealBalanceReport
- 用途：查詢證券即時庫存。
- 簽名：`int GetRealBalanceReport(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 證券帳號：分公司四碼＋帳號7碼 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnRealBalanceReport 事件回傳。v2.13.42~v2.13.54「可資沖/可券沖」兩欄位內容誤植互換，v2.13.55 起已修正。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 記載「自營帳號即時庫存查詢 GetRealBalanceReport 欄位新增"昨日庫存"」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但主手冊 OnRealBalanceReport 的欄位序列兩版逐項相同、原本就含「股數:昨日庫存」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1708`），**changelog 說「新增欄位」、手冊欄位表卻兩版逐字相同，兩者無法從文件調和**（可能是自營帳號情境下該欄先前無值而本版補值，也可能是欄位表未同步更新，官方未說明）。若您的解析器以固定欄位數或固定索引取值，升版前請在模擬環境以自營帳號實測欄位數與欄位序列。
- 備註（V2.13.58／V2.13.59 新增／簡化版包裝層）：亦可經簡化版 SKDLLCSharp 包裝層呼叫 `GetRealBalanceReport(strLogInID, strAccount)`（V2.13.57 的 SKDLLCSharp.dll 無此方法，官方兩版 changelog 亦未列，無從判定落在哪一版），回傳 `RealBalanceReportParserResult`（StatusCode／Message／RawData／Blocks），為同步取值、非 COM 的「錯誤碼＋事件」模式（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:362-370`）。官方手冊「宣告」欄誤寫為 `(int Code, string Message)` 之 ValueTuple，實際回傳型別以 `SKDLLCSharp.dll` 中繼資料為準，範例碼以 `var` 承接後取用 StatusCode／Message／Blocks（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.cs:2986-3000`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1105`

### GetBalanceQuery
- 用途：集保庫存查詢（已停用）。
- 簽名：`int GetBalanceQuery(string bstrLogInID, string bstrAccount, string bstrStockNo)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 證券帳號（分公司四碼＋帳號7碼；帶空＝所有證券帳號） |
| bstrStockNo | string | 商品代碼（帶空＝全部商品） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnBalanceQuery 事件回傳。**V2.13.54 後不再提供，請改用 GetRealBalanceReport。**
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1123`

### GetMarginPurchaseAmountLimit
- 用途：證券資券配額查詢。
- 簽名：`int GetMarginPurchaseAmountLimit(string bstrLogInID, string bstrAccount, string bstrStockNo)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 證券帳號：分公司四碼＋帳號7碼 |
| bstrStockNo | string | 商品代碼（帶空＝全部商品） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnMarginPurchaseAmountLimit 事件回傳；回傳【M003】表示該股票無信用交易資格。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1117`

### GetProfitLossGWReport
- 用途：證券新損益試算查詢（未實現／已實現／現股當沖，由查詢物件的 nTPQueryType 決定）。
- 簽名：`int GetProfitLossGWReport(string bstrLogInID, ref TSPROFITLOSSGWQUERY pPLGWQuery)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pPLGWQuery | TSPROFITLOSSGWQUERY | 查詢條件物件（見共用結構） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnProfitLossGWReport 事件回傳。已實現彙總用西元起訖日、明細用民國起始日（不需結束日）；現股當沖限查當日。V2.13.21 起提供，為 GetRequestProfitReport 的替代品。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1129`

### GetRequestProfitReport
- 用途：舊版證券即時損益試算（即將下線）。
- 簽名：`int GetRequestProfitReport(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 證券帳號 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：官方標記「即將下線」，請改用 GetProfitLossGWReport＋OnProfitLossGWReport。結果由 OnRequestProfitReport 回傳。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1111`

### GetAvgCost
- 用途：查詢昨日未沖銷的證券明細。
- 簽名：`int GetAvgCost(string bstrLogInID, ref SKAVGCOST pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pOrder | SKAVGCOST | 查詢條件物件（見共用結構） |
| bstrMessage | out string | 委託查詢內容 |

- 回傳：LONG 錯誤碼；0 成功。回傳內容每筆以「#」分隔、欄位以「,」分隔；M999 查詢錯誤、M003 查無資料。
- 備註：一個 account 一分鐘內不可查詢超過 10 次（1127 SK_ERROR_QUERY_IS_OVER_TEN_TIMES）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:2007`

### GetBalance
- 用途：一戶通、餘額、可用金額查詢。
- 簽名：`string GetBalance(string bstrLogInID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |

- 回傳：查詢文字訊息（欄位以「,」分隔、結尾「.」；EX: `true,0,0,0.`）；-1 表示失敗。
- 備註：直接回傳字串，非事件回傳。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1290`

### GetT3DueAmt
- 用途：近三日交割款查詢。
- 簽名：`string GetT3DueAmt(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 證券帳號：分公司四碼＋帳號7碼 |

- 回傳：查詢文字訊息；不同幣別以「;」分隔、欄位以「,」分隔（日期為民國年格式，文件曾誤標西元、V2.13.55 已更正說明）。
- 備註：直接回傳字串。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1298`

### GetIPAAmt
- 用途：台股圈存查詢。
- 簽名：`string GetIPAAmt(string bstrLogInID, string bstrAccount, int nMarket)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 證券帳號：分公司四碼＋帳號7碼 |
| nMarket | int | 0:上市櫃 1:興櫃 |

- 回傳：欄位以「,」分隔、結尾「#」（有開通一戶通格式 EX: `646551,7831,7811#`）。
- 備註：未開通一戶通時回傳提示文字（請匯預收款至指定專戶）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1307`

### 國內證券下單

### SendStockOrder
- 用途：送出證券委託（適用逐筆交易）。
- 簽名：`int SendStockOrder(string bstrLogInID, bool bAsyncOrder, ref STOCKORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | STOCKORDER | 下單物件（見共用結構） |
| bstrMessage | out string | 同步：回傳 0 時為 13 碼委託序號；非 0 為失敗原因。非同步：參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。4 碼錯誤代碼見 [../error_codes.md](../error_codes.md)，其他錯誤由交易主機回傳。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:465`）。
- 備註：非同步委託結果由 OnAsyncOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:547`

### SendStockOddLotOrder
- 用途：送出證券盤中零股委託。
- 簽名：`int SendStockOddLotOrder(string bstrLogInID, bool bAsyncOrder, ref STOCKORDER pOrder, out string bstrMessage)`
- 參數表：同 SendStockOrder（pOrder 用盤中零股欄位：sPeriod=4、sFlag=0、nQty=1~999 股）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1254`）。
- 備註：非同步結果由 OnAsyncOrder 取得。盤中零股不適用改價（CorrectPrice 系列），刪單用 CancelOrderByStockNo / CancelOrderByStockNoAdvance。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:556`

### 國內刪改單（證券／期貨／選擇權共用）

> 別名/呼叫名核對：docx 與部分程式碼註解慣以「CorrectPrice」「DecreaseOrder」「CancelOrder」泛稱本節函式家族；經比對 `Source_code/**/*.cs`（grep 呼叫點）與 `5.下單-國內證券.md`/`7.下單-國內期選.md` 原文，SKOrderLib 並無無後綴的 `CorrectPrice`/`DecreaseOrder`/`CancelOrder` 方法（無 `.CorrectPrice(`／`.DecreaseOrder(`／`.CancelOrder(` 呼叫點），皆以下列帶 By* 後綴的方法家族提供。

### CorrectPriceBySeqNo
- 用途：證期權依 13 碼委託序號改價。
- 簽名：`int CorrectPriceBySeqNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, string bstrPrice, int nTradeType, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 證券：BrokerID＋帳號；期貨：IB＋帳號 |
| bstrSeqNo | string | 欲改價的委託序號 |
| bstrPrice | string | 修改價格 |
| nTradeType | int | 證券 0:ROD；期選 0:ROD 1:IOC 2:FOK |
| bstrMessage | out string | 同步：0 時為修改訊息；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝委託伺服器接收成功（實際結果以改價回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:661`）。
- 備註：證券逐筆 nTradeType 請設 0(ROD)；**不適用盤中零股**。非同步結果由 OnAsyncOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:901`

### CorrectPriceByBookNo
- 用途：證期權依 5 碼委託書號改價。
- 簽名：`int CorrectPriceByBookNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrMarketSymbol, string bstrBookNo, string bstrPrice, int nTradeType, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 證券：BrokerID＋帳號；期貨：IB＋帳號 |
| bstrMarketSymbol | string | 市場類別 TS:證券 TF:期貨 TO:選擇權 |
| bstrBookNo | string | 欲改價的委託書號 |
| bstrPrice | string | 修改價格 |
| nTradeType | int | 證券 0:ROD；期選 0:ROD 1:IOC 2:FOK |
| bstrMessage | out string | 同步：修改訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:676`）。
- 備註：證券逐筆 nTradeType 設 0；不適用盤中零股。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:909`

### DecreaseOrderBySeqNo
- 用途：依 13 碼序號委託減量。
- 簽名：`int DecreaseOrderBySeqNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, int nDecreaseQty, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 證券：BrokerID＋帳號；期貨：IB＋帳號 |
| bstrSeqNo | string | 欲減量的委託序號 |
| nDecreaseQty | int | 欲減少的數量 |
| bstrMessage | out string | 同步：修改訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以減量回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:581`）。
- 備註：亦適用證券逐筆及擬真平台。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:882`

### CancelOrderBySeqNo
- 用途：國內委託刪單（依 13 碼委託序號）。
- 簽名：`int CancelOrderBySeqNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 證券：BrokerID＋帳號；期貨：IB＋帳號 |
| bstrSeqNo | string | 欲刪除的委託序號 |
| bstrMessage | out string | 同步：刪單訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以刪單回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:619`）。
- 備註：亦適用證券逐筆及擬真平台。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:945`

### CancelOrderByBookNo
- 用途：國內委託刪單（依 5 碼書號）。
- 簽名：`int CancelOrderByBookNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrBookNo, out string bstrMessage)`
- 參數表：同 CancelOrderBySeqNo，但以 `bstrBookNo`（string，欲刪除的委託書號）取代序號。
- 回傳：LONG 錯誤碼；0＝接收成功（以回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:960`）。
- 備註：非同步結果由 OnAsyncOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:954`

### CancelOrderByStockNo
- 用途：國內委託刪單（依帳號所屬登入 ID＋商品代號）。
- 簽名：`int CancelOrderByStockNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrStockNo, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 證券：BrokerID＋帳號；期貨：IB＋帳號 |
| bstrStockNo | string | 欲刪除的商品代號；**帶空字串＝刪除帳號下所有委託**（V2.13.52 起） |
| bstrMessage | out string | 同步：刪單訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:631`）。
- 備註：包含刪除盤中零股委託，請留意證券代碼。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:963`

### CancelOrderByStockNoAdvance
- 用途：國內委託刪單（依商品代號＋買賣別＋委託價格精準刪單）。
- 簽名：`int CancelOrderByStockNoAdvance(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrStockNo, int nBuySell, string bstrPrice, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 證券：BrokerID＋帳號；期貨：IB＋帳號 |
| bstrStockNo | string | 欲刪除的商品代號（須為有效代碼） |
| nBuySell | int | 0:買進 1:賣出 |
| bstrPrice | string | 委託價格（不使用請帶空字串） |
| bstrMessage | out string | 同步：刪單訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1594`）。
- 備註：包含刪除盤中零股委託。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:972`

### 證券智慧單

> 共通前置：需簽署「證券智慧單風險預告書」（未簽署回 2009），可至金融網－同意書簽署專區申請。智慧單為獨立運作機制：若盤中已自行出清/回補，務必自行取消已設定的智慧單，避免條件觸發後重複成交。非同步結果一律由 OnAsyncOrder 取得；被動查詢用 GetTSSmartStrategyReport。

### SendStockStrategyDayTrade
- 用途：送出證券智慧單「當沖」條件委託（進場＋停損/停利/時間出清等出場條件一次設定）。
- 簽名：`int SendStockStrategyDayTrade(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | STOCKSTRATEGYORDER | 當沖條件物件（見共用結構） |
| bstrMessage | out string | 同步：0 時為委託日期、條件單號（智慧單序號）等；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功；其他錯誤由智慧單主機回傳。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1044`）。
- 備註：見本區共通前置。刪單用 CancelTSStrategyOrderV1（當沖/出清/OCO 不可用 CancelTSStrategyOrder）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:830`

### SendStockStrategyClear
- 用途：送出證券智慧單「出清」條件委託（大於/小於觸發、時間出清、盤後定盤）。
- 簽名：`int SendStockStrategyClear(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDEROUT pOrder, out string bstrMessage)`
- 參數表：同 SendStockStrategyDayTrade，pOrder 為 STOCKSTRATEGYORDEROUT（見共用結構）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1055`）。
- 備註：見本區共通前置。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:899`

### SendStockStrategyMIT
- 用途：送出證券智慧單 MIT 觸價條件委託（V2.13.30 起可自設觸價方向；V2.13.45 起支援長效單與預風控）。
- 簽名：`int SendStockStrategyMIT(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDERMIT pOrder, out string bstrMessage)`
- 參數表：同上，pOrder 為 STOCKSTRATEGYORDERMIT（見共用結構）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1167`）。
- 備註：預風控會於觸發前預先扣除委託額度（含庫存），取消智慧單可釋放占用額度。V2.13.51 起非交易時間（08:30~13:25 以外）下單即為預約單。被動回報 GetTSSmartStrategyReport、主動回報見回報文件（SKReplyLib）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:951`

### SendStockStrategyOCO
- 用途：送出證券智慧單二擇一（OCO）條件委託。
- 簽名：`int SendStockStrategyOCO(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDEROCO pOrder, out string bstrMessage)`
- 參數表：同上，pOrder 為 STOCKSTRATEGYORDEROCO（見共用結構；v2.13.48 新增無券普賣委託別）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1178`）。
- 備註：見本區共通前置；刪單用 CancelTSStrategyOrderV1。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:1007`

### SendStockStrategyMIOC
- 用途：送出證券智慧單「多次 IOC」條件委託（在價格上下限內分批以 IOC 委託至總量成交）。
- 簽名：`int SendStockStrategyMIOC(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDERMIOC pOrder, out string bstrMessage)`
- 參數表：同上，pOrder 為 STOCKSTRATEGYORDERMIOC（見共用結構）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1214`）。
- 備註：見本區共通前置。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:1045`

### SendStockStrategyMST
- 用途：送出證券智慧單移動停損（MST）條件委託（V2.13.30 起可自訂啟動價格、啟動觸價方向）。
- 簽名：`int SendStockStrategyMST(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDERMIT pOrder, out string bstrMessage)`
- 參數表：同上，pOrder 為 STOCKSTRATEGYORDERMIT（MST 變體欄位）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1225`）。
- 備註：見本區共通前置。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:1101`

### SendStockStrategyAB
- 用途：送出證券智慧單「看 A 下 B」委託（監看 A 商品觸價後委託 B 商品；A 可為國內外證/期商品）。
- 簽名：`int SendStockStrategyAB(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDERMIT pOrder, out string bstrMessage)`
- 參數表：同上，pOrder 為 STOCKSTRATEGYORDERMIT（AB 變體：bstrStockNo2/nMarketNo/bstrExchangeNo 等看 A 欄位）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1381`）。
- 備註：A 商品為國內期選商品時可選預約單，其餘市場為非預約單。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:1156`

### SendStockStrategyCB
- 用途：送出證券智慧單自組單（CB）委託（自訂觸發時間與多條件組合關係）。
- 簽名：`int SendStockStrategyCB(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDER pOrder, out string bstrMessage)`
- 參數表：同上，pOrder 為 STOCKSTRATEGYORDER（CB 變體欄位）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1392`）。
- 備註：見本區共通前置。
- 備註（V2.13.58 修正）：官方修正「證券智慧單 CB 單 SendStockStrategyCB 發生 SK_ERROR_MARKET_OUT_OF_RANGE（1023）」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37` 二、功能修正 1）。手冊本文未載明原本哪些商品／市場代碼會誤觸發，元件內部行為無從文件確認；若舊程式把「收到 1023」當成跳過或重試的 workaround，升級後該筆委託會真的送出，**升級前請在模擬環境複測本函式的錯誤分支**。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:1256`

### SendStockStrategyLLS
- 用途：證券漲跌停盯盤單（已移除）。
- 簽名：`int SendStockStrategyLLS(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDER pOrder, out string bstrMessage)`（依範例碼萃取；官方參數表已刪除）
- 參數表：文件未載（功能已移除）。
- 回傳：LONG 錯誤碼。
- 備註：**V2.13.48 起官方移除此功能**（changelog 記載），範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1857`（已註解）

### SendStockStrategyMBA
- 用途：證券 MBA 策略單（已移除）。
- 簽名：`int SendStockStrategyMBA(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDER pOrder, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（功能已移除）。
- 回傳：LONG 錯誤碼。
- 備註：**V2.13.48 起官方移除此功能**，範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1847`（已註解）

### SendStockStrategyMMIT
- 用途：證券 MIT 多條件策略單（已移除）。
- 簽名：`int SendStockStrategyMMIT(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDERMIT pOrder, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（功能已移除）。
- 回傳：LONG 錯誤碼。
- 備註：**V2.13.48 起官方移除此功能**，範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1817`（已註解）

### CancelTSStrategyOrder
- 用途：取消證券智慧單委託（限 MIOC/MST/MIT；已觸發的智慧單無法取消）。
- 簽名：`int CancelTSStrategyOrder(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, int nTradeKind, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 委託帳號（BrokerID＋帳號） |
| bstrSmartKey | string | 智慧單序號或進場單號（參考 GetTSSmartStrategyReport 回傳） |
| nTradeKind | int | 智慧單類型 6:MIOC 7:MST 8:MIT |
| bstrMessage | out string | 同步：刪單訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1082`）。
- 備註：當沖單/出清單/OCO 請改用 CancelTSStrategyOrderV1。刪單後請透過智慧單被動回報確認狀態。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:698`

### CancelTSStrategyOrderV1
- 用途：新版取消證券智慧單委託（刪單欄位參考 GetTSSmartStrategyReport 回傳內容）。
- 簽名：`int CancelTSStrategyOrderV1(string bstrLogInID, ref CANCELSTRATEGYORDER pCancelOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pCancelOrder | CANCELSTRATEGYORDER | 智慧單刪單物件（見共用結構；證券當沖/出清/OCO 等變體） |
| bstrMessage | out string | 非同步刪單：0 時為刪單之 Thread ID；非 0 為失敗原因（參照 OnAsyncOrder） |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1295`）。
- 備註：刪全部及刪進場單時，智慧母單號與智慧單號相同；若已觸發需給委託書號。可刪單狀態代碼（32/34/35/37/38/42/43）見 `api_spec/_raw/6.下單-證券智慧單.md:463-481`。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:734`

### CancelStrategyList
- 用途：取消多筆智慧單委託（V2.13.48 新增；證券/期貨/海外市場通用，智慧單號以逗號分隔）。
- 簽名：`int CancelStrategyList(string bstrLogInID, ref CANCELSTRATEGYORDER pCancelOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pCancelOrder | CANCELSTRATEGYORDER | 多筆刪單變體：bstrFullAccount＋nMarket（1~4；AB 單選 A 商品市場別）＋bstrSmartKey（逗號分隔多筆） |
| bstrMessage | out string | 非同步刪單：0 時為刪單之 Thread ID；非 0 為失敗原因 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1480`）。
- 備註：刪單後請透過智慧單被動回報確認狀態。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:1328`

### GetTSSmartStrategyReport
- 用途：證券智慧單被動查詢。
- 簽名：`int GetTSSmartStrategyReport(string bstrLogInID, string bstrAccount, string bstrMarketType, int nReportStatus, string bstrKind, string bstrDate)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（帳號） |
| bstrMarketType | string | TS:證券市場 |
| nReportStatus | int | 0:全部的委託單 |
| bstrKind | string | DayTrade:當沖 ClearOut:出清 MIT OCO MIOC MST AB CB |
| bstrDate | string | 查詢日期（EX:20201001） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnTSSmartStrategyReport 事件回傳。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 列「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名是哪一種智慧單、也未指出缺漏逗號的欄位位置，手冊本文並無新舊欄位比較表可對照。既有解析器若以固定索引取欄位，升版後可能整體位移；建議改為「先檢查欄位數（長度）再依欄位名對映」，並在模擬環境比對實際欄位數後才上線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/StrategyOrderForm/TSStrategyOrderForm.cs:685`

### 國內期選查詢

### GetOpenInterestGW
- 用途：(新)查詢期貨未平倉 GW，可指定回傳格式（V2.13.42 新增）。
- 簽名：`int GetOpenInterestGW(string bstrLogInID, string bstrAccount, int nFormat)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| nFormat | int | 回傳格式：1 |

- 回傳：LONG 錯誤碼；0 成功（未指定格式回 1112 SK_ERROR_QUERY_FORMAT_INVALID）。
- 備註：結果由 OnOpenInterest 回傳、查詢狀態由 OnOpenInterestGWStatus 回傳；含複式單（市場別 TM）；查無資料回傳 `001,查無資料,帳號`（V2.13.57 手冊 GW 版原文寫 `001 查無資料`，無逗號、無帳號欄；V2.13.58 起已改，見下。本節原稿誤植為 `M003 NO DATA#`，該格式是 V2.13.57 非格式版 GetOpenInterest 的寫法）。V2.13.58 起另可改由 OnOpenInterestJson 一次取得整批未平倉（該事件與逐筆的 OnOpenInterest 並存；官方未說明兩者是同時觸發還是需擇一訂閱，若兩者都訂閱是否會拿到重複資料需實測）。
- 備註（V2.13.58 修正／查無資料格式）：官方 changelog 記為「新增內、外期未平倉查詢 GetOpenInterest、GetOpenInterestGW、GetOpenInterestWithFormat、GetOverSeaFutureOpenInterest『查無庫存』時新增 Account 欄位」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37`），本文改為「若查無資料，則回傳 001,查無資料,帳號」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1725`、`api_spec/_raw/v2.13.59/7.下單-國內期選.md:622`）。官方兩處格式不一致：主手冊為 3 欄「001,查無資料,帳號」，分冊 7 的 OnOpenInterestJson 節僅寫 2 欄「001,查無資料」（`api_spec/_raw/v2.13.59/7.下單-國內期選.md:724`）。解析器應以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄與 3 欄兩種官方寫法；**勿**再比對 `M003 NO DATA`，也勿以固定欄位數判定查無資料（否則會把查無資料誤判成一般資料列）。
- 備註（V2.13.59 新增欄位）：官方 changelog 列「國內未平倉查詢 GetOpenInterestGW 新增欄位『商品－下單代碼』」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），本文同步新增「當『商品－下單代碼』查詢發生異常時，該欄位將回傳空值。請重新呼叫 GetOpenInterestGW 以重新取得資料。」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1725`、`api_spec/_raw/v2.13.59/7.下單-國內期選.md:622`）。**欄位序列本體在官方 docx 內是圖片／未展開清單，md 抽取後看不到內容，新欄位插在第幾欄無從文件確認**；以固定索引解析 GW 未平倉字串者，務必先在模擬環境實測欄位序列再上線。
- 備註（V2.13.58／V2.13.59 新增／簡化版包裝層）：亦可經簡化版 SKDLLCSharp 包裝層呼叫 `GetOpenInterestGW(strLogInID, strAccount, nFormat)`（V2.13.57 的 SKDLLCSharp.dll 無此方法，官方兩版 changelog 亦未列，無從判定落在哪一版），回傳 `OpenInterestParserResult`（StatusCode／Message／RawData／Blocks），為同步取值、非 COM 的「錯誤碼＋事件」模式（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:389-398`）。官方手冊「宣告」欄誤寫為 `(int Code, string Message)` 之 ValueTuple，實際回傳型別以 `SKDLLCSharp.dll` 中繼資料為準，範例碼以 `var` 承接後取用 StatusCode／Message／Blocks（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.cs:2878-2901`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1135`

### GetOpenInterest
- 用途：舊版查詢期貨未平倉（不含格式參數）。
- 簽名：`int GetOpenInterest(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnOpenInterest 事件回傳。新開發請用 GetOpenInterestGW。
- 備註（V2.13.58 修正／查無資料格式）：查無資料改回傳 `001,查無資料,帳號`（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1716`，changelog 見同檔 `:37`）。解析器應以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄（`api_spec/_raw/v2.13.59/7.下單-國內期選.md:724`）與 3 欄（主手冊）兩種官方不一致格式，勿以固定欄位數判定查無資料。
- 備註（V2.13.58／V2.13.59 修正）：V2.13.58 對「國內期貨未平倉 Parse 失敗」新增例外處理（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37`），惟手冊本文未說明新行為是「靜默略過」還是「回傳錯誤」，串接端仍應自行對 OnOpenInterest 字串做容錯。另外，V2.13.58 起本函式亦會觸發 OnOpenInterestJson（changelog 一、功能異動 1，同檔 `:37`），並在 V2.13.58／V2.13.59 起一併觸發 OnOpenInterestGWStatus（changelog 未列版本；見兩事件節）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1141`

### GetOpenInterestWithFormat
- 用途：查詢期貨未平倉，可指定回傳格式（1~3）。
- 簽名：`int GetOpenInterestWithFormat(string bstrLogInID, string bstrAccount, int nFormat)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| nFormat | int | 1:完整 2:格式1 3:格式2-含損益 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnOpenInterest 事件回傳。v2.13.53 起格式二暫不提供「單口手續費、交易稅(萬分之X)」欄位。
- 備註（V2.13.58 修正／查無資料格式）：查無資料改回傳 `001,查無資料,帳號`（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1721`，changelog 見同檔 `:37`）。解析器應以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄（`api_spec/_raw/v2.13.59/7.下單-國內期選.md:724`）與 3 欄（主手冊）兩種官方不一致格式，勿以固定欄位數判定查無資料。
- 備註（V2.13.58／V2.13.59 修正）：本函式亦會觸發 OnOpenInterestJson（V2.13.58 changelog 一、功能異動 1，同檔 `:37`；事件節見同檔 `:2005`）與 OnOpenInterestGWStatus（同檔 `:1973`；官方 changelog 未列此擴大，版本無從判定）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1147`

### GetFutureRights
- 用途：查詢國內權益數。
- 簽名：`int GetFutureRights(string bstrLogInID, string bstrAccount, short sCoinType)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| sCoinType | short | 0:全幣別 1:基幣(台幣TWD) 2:人民幣RMB |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnFutureRights 事件回傳（41 欄格式）；全幣別選項含基幣，第一筆為基幣。
- 備註（V2.13.58／V2.13.59 新增）：查詢成敗另由新事件 OnFutureRightsStatus 回傳（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1988-1994`、`api_spec/_raw/v2.13.59/7.下單-國內期選.md:711-717`）；在此之前只有資料事件 OnFutureRights，沒有成敗判斷管道。注意成功時 bstrErrorMsg 為字串 `"Success!"` 而非空字串。
- 備註（V2.13.58／V2.13.59 新增／簡化版包裝層）：亦可經簡化版 SKDLLCSharp 包裝層呼叫 `GetFutureRights(strLogInID, strAccount, nCoinType)`（V2.13.57 的 SKDLLCSharp.dll 無此方法，官方兩版 changelog 亦未列，無從判定落在哪一版），回傳 `FutureRightsParserResult`（StatusCode／Message／RawData／Blocks），為同步取值、非 COM 的「錯誤碼＋事件」模式（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:400-409`）。官方手冊「宣告」欄誤寫為 `(int Code, string Message)` 之 ValueTuple，實際回傳型別以 `SKDLLCSharp.dll` 中繼資料為準，範例碼以 `var` 承接後取用 StatusCode／Message／Blocks（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.cs:3027-3051`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1200`

### 期貨互抵

### SendTFOffset
- 用途：指定大小台／電／金互抵。
- 簽名：`int SendTFOffset(string bstrLogInID, bool bAsyncOrder, string bstrAccount, int nCommodity, string bstrYearMonth, int nBuySell, int nQty, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| nCommodity | int | 0:大小台 1:大小電 2:大小金 |
| bstrYearMonth | string | 年月 YYYYMM |
| nBuySell | int | 大台(電/金)買賣別 0:多方(買) 1:空方(賣) |
| nQty | int | 互抵口數（以大台/電/金口數為基本單位） |
| bstrMessage | out string | 同步：互抵訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝委託伺服器接收成功（商品別無效回 1066）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1269`）。
- 備註：1 口大台抵 4 口小台（金），1 口大電抵 8 口小電；需互為反向部位、相同月份。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1253`

### SendTXOffset
- 用途：大小台互抵（舊版，僅大小台）。
- 簽名：`int SendTXOffset(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrYearMonth, int nBuySell, int nQty, out string bstrMessage)`
- 參數表：同 SendTFOffset 但無 nCommodity（固定大小台）。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:974`）。
- 備註：1 口大台抵 4 口小台，需反向部位、相同月份。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1236`

### SendTFOffsetNew
- 用途：指定大、小、微台互抵。
- 簽名：`int SendTFOffsetNew(string bstrLogInID, bool bAsyncOrder, string bstrAccount, int nCommodity, string bstrYearMonth, int nBuySell, int nQty, int nQty_2, int nQty_3, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| nCommodity | int | 0:大抵微 1:小抵微 2:大小抵微 3:大抵小微 4:小抵大微 5:大抵小 |
| bstrYearMonth | string | 大小微台年月 YYYYMM |
| nBuySell | int | 0:多方(買) 1:空方(賣) |
| nQty | int | 互抵口數（大台為單位） |
| nQty_2 | int | 互抵口數（小台為單位） |
| nQty_3 | int | 互抵口數（微台為單位） |
| bstrMessage | out string | 同步：互抵訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1509`）。
- 備註：1 大口＝20 微台、1 小口＝5 微台；需相同月份；大抵小微以大台為買賣別基準、大小抵微以大台+小台為基準；不需互抵的商品口數帶 0。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1352`

### 選擇權組合／拆解／了結

> 共通：非交易行為、無回報，可由未平倉查詢確認部位。組合/拆解/了結直送結算所，需等處理結果回傳後方可進行下一筆。V2.13.55 起 AssembleOptions／DisassembleOptions／CoverAllProduct 新增「非同步委託」。

### AssembleOptions
- 用途：國內選擇權組合部位。
- 簽名：`int AssembleOptions(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 可忽略，限同步委託（V2.13.55 起支援非同步） |
| pOrder | FUTUREORDER | 組合條件（bstrStockNo/bstrStockNo2/sBuySell/sBuySell2/nQty） |
| bstrMessage | out string | 同步：0＝委託已送出；非 0 為失敗原因 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:985`）；本函式為非交易行為、無回報，處理結果請由未平倉查詢確認（同檔 `:986`）。
- 備註：需有可組合的選擇權部位。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:647`

### DisassembleOptions
- 用途：國內選擇權複式單拆解（單邊）。
- 簽名：`int DisassembleOptions(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 AssembleOptions（pOrder 用複式單拆解欄位：bstrStockNo＝複式單商品代碼、sBuySell、nQty；雙邊拆解欄位已於改版刪除 bstrStockNo2）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:996`）；本函式為非交易行為、無回報，處理結果請由未平倉查詢確認（同檔 `:997`）。
- 備註：需有複式單部位。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:656`

### CoverAllProduct
- 用途：國內選擇權雙邊部位了結。
- 簽名：`int CoverAllProduct(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 AssembleOptions（pOrder 用雙邊了結欄位：bstrStockNo、nQty）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1007`）；本函式為非交易行為、無回報，處理結果請由未平倉查詢確認（同檔 `:1008`）。
- 備註：需具有雙邊部位。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:665`

### AllCoverDisOptions
- 用途：全拆組單、全拆組平單下單（依試算結果進行委託）。
- 簽名：`int AllCoverDisOptions(string bstrLogInID, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pOrder | FUTUREORDER | bstrFullAccount＋nFunType（1:全拆組 2:全拆組平） |
| bstrMessage | out string | 同步：0＝委託已送出；非 0 為失敗原因 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：搭配 BasketOptionSimulation 試算結果使用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1998`

### 國內期選下單

### SendFutureOrderCLR
- 用途：送出期貨委託，需設倉別（sNewClose）與盤別（sReserved）。新客戶請用本函式。
- 簽名：`int SendFutureOrderCLR(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | FUTUREORDER | 下單物件（見共用結構；一般期選委託欄位） |
| bstrMessage | out string | 同步：0 時為 13 碼委託序號；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:480`）。
- 備註：V2.13.54 起新增 bstrCIDTandem、bstrSettlementMonth 欄位；商品可用兩種寫法：(1) bstrStockNo=`TX03`（若當年 TX03 已過期會自動委託隔年）(2) bstrCIDTandem=`FITX`＋bstrSettlementMonth=`202503`。價差單 bstrStockNo 帶「近月/遠月」、sBuySell 為近月方向。v2.13.47 修正委託價 M/P 錯誤。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:593`

### SendFutureOrder
- 用途：舊版送出期貨委託：不需填倉位、盤別固定盤中（不可更改）。新客戶請改用 SendFutureOrderCLR。
- 簽名：`int SendFutureOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureOrderCLR（sNewClose/sReserved 無效）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：官方文件（主說明 4-2-7）僅保留標題並導向 4-2-8 SendFutureOrderCLR。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:584`

### SendOptionOrder
- 用途：送出選擇權委託，需設盤別。
- 簽名：`int SendOptionOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureOrderCLR。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:491`）。
- 備註：非同步結果由 OnAsyncOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:620`

### SendDuplexOrder
- 用途：送出國內選擇權複式單委託。
- 簽名：`int SendDuplexOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureOrderCLR（pOrder 用複式單欄位：bstrStockNo/bstrStockNo2/sBuySell/sBuySell2，sTradeType 僅 1:IOC 2:FOK）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:938`）。
- 備註：複式單價格填法（Call/Put 多空頭價差、突破、盤整之 bstrPrice 計算方式）詳見 `api_spec/_raw/7.下單-國內期選.md:311`。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:638`

### 期選智慧單

> 共通前置：需簽署「期貨智慧單風險預告書」（未簽署回 2010）。非同步結果由 OnAsyncOrder 取得；成功訊息格式 EX：`000,20220419,智慧條件單已送出 條件單號：568157,無委託書號,568157,1689200000003`。被動查詢用 GetStopLossReport；V1 系列可下指定月份商品（需填 bstrSettlementMonth），非 V1 限近月商品代碼（TX00 等）。

### SendFutureSTPOrderV1
- 用途：新版送出期貨停損（STP）委託；指定月份需填商品契約年月；支援長效單（V2.13.45+）。
- 簽名：`int SendFutureSTPOrderV1(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | FUTUREORDER | 期貨智慧單 STP 變體（bstrTrigger 必填、nOrderPriceType 2:限價 3:範圍市價、長效單欄位） |
| bstrMessage | out string | 同步：委託日期、智慧單序號、委託書號等；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1306`）。
- 備註：見本區共通前置。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:801`

### SendFutureStopLossOrder
- 用途：送出期貨停損委託（限近月商品代碼）。
- 簽名：`int SendFutureStopLossOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureSTPOrderV1。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:535`）。
- 備註：非近月商品請用 SendFutureSTPOrderV1（否則回 1107 限制近月商品代碼）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:792`

### SendOptionStopLossOrder
- 用途：送出選擇權停損委託。
- 簽名：`int SendOptionStopLossOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureSTPOrderV1。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:557`）。
- 備註：範例程式碼可參考 SendFutureSTPOrderV1 做法。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:837`

### SendFutureMSTOrderV1
- 用途：新版送出移動停損（MST）委託；指定月份需填商品契約年月。
- 簽名：`int SendFutureMSTOrderV1(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureSTPOrderV1（pOrder 用 MST 變體：bstrMovingPoint 移動點數必填、sTradeType 僅 3:IOC 4:FOK、不須填委託價）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1317`）。
- 備註：移動點數有誤回 1083。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:819`

### SendMovingStopLossOrder
- 用途：送出移動停損委託（限近月商品代碼）。
- 簽名：`int SendMovingStopLossOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureMSTOrderV1。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:546`）。
- 備註：非近月請用 SendFutureMSTOrderV1。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:810`

### SendFutureMITOrderV1
- 用途：新版送出期貨 MIT（觸價）委託；指定月份需填商品契約年月。
- 簽名：`int SendFutureMITOrderV1(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureSTPOrderV1（pOrder 用 MIT 變體：**bstrTrigger 觸發價、bstrDealPrice 成交價、nTriggerDirection 觸價方向為必要欄位**）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1328`）。
- 備註：MIT 需含觸發價（1054）與成交價（1056）；觸發價等於成交價無法觸發（1089）；MIT 不可委託價差商品（1050）；MIT 單不須填盤別。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1227`

### SendFutureMITOrder
- 用途：送出期貨 MIT 委託（限近月商品代碼）。
- 簽名：`int SendFutureMITOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureMITOrderV1。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:908`）。
- 備註：非近月請用 SendFutureMITOrderV1。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1218`

### SendOptionMITOrder
- 用途：送出選擇權 MIT 委託。
- 簽名：`int SendOptionMITOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureMITOrderV1（觸發價/成交價/觸價方向必填）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:919`）。
- 備註：範例程式碼可參考 SendFutureMITOrderV1 做法。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1263`

### SendFutureOCOOrderV1
- 用途：新版送出期貨二擇一（OCO）委託；指定月份需填商品契約年月；支援長效單。
- 簽名：`int SendFutureOCOOrderV1(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureSTPOrderV1（pOrder 用 OCO 變體：兩腳觸發價/委託價/買賣別、nTimeFlag 盤別、長效單欄位）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1339`）。
- 備註：第一腳市價大於觸發價 1 觸發、第二腳市價小於觸發價 2 觸發。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:855`

### SendFutureOCOOrder
- 用途：送出期貨二擇一委託（限近月商品代碼；使用獨立的 FUTUREOCOORDER 物件）。
- 簽名：`int SendFutureOCOOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREOCOORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | FUTUREOCOORDER | 二擇一下單物件（見共用結構） |
| bstrMessage | out string | 同步：委託日期、智慧單序號等；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:864`）。
- 備註：非近月請用 SendFutureOCOOrderV1。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:846`

### SendFutureABOrder
- 用途：新版送出期貨「看 A 下 B」委託（指定月份需填商品契約年月）。
- 簽名：`int SendFutureABOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendFutureSTPOrderV1（pOrder 用 AB 變體：看 A 欄位 bstrStockNo2/nMarketNo/bstrCIDTandem/bstrDealPrice/nTriggerDirection/bstrTrigger；下 B 欄位含價差、選擇權 nCallPut/bstrStrikePrice）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1403`）。
- 備註：見本區共通前置。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1867`

### CancelTFStrategyOrderV1
- 用途：新版取消期貨智慧單委託（STP/MST/MIT/OCO/AB）。已產生書號之委託請填入書號，否則可能影響解除保證金風控。
- 簽名：`int CancelTFStrategyOrderV1(ref CANCELSTRATEGYORDER pCancelOrder, bool bAsyncOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| pCancelOrder | CANCELSTRATEGYORDER | 期貨刪單變體（bstrLogInID/bstrFullAccount/nMarket/bstrSmartKey/nTradeKind 3:OCO 5:STP 8:MIT 9:MST 10:AB/bstrSeqNo/bstrOrderNo/bstrLongActionKey） |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrMessage | out string | 非同步刪單：0 時為刪單之 Thread ID；非 0 為失敗原因 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1349`）。
- 備註：**注意參數順序與其他刪單函式不同（pCancelOrder 在前）**；刪單成功可取得智慧單號、13 碼序號、書號。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:828`

### CancelFutureStopLoss
- 用途：取消期貨停損委託（舊版）。
- 簽名：`int CancelFutureStopLoss(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, string bstrSymbol, out string bstrMessage)`（依範例碼萃取；官方文件僅存導引文字）
- 參數表：文件未載（參數表已自官方文件移除，僅存「舊用戶建議改用 4-2-87 CancelTFStrategyOrderV1」導引）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：委託已產生書號時，用本函式可能影響解除保證金風控，官方建議改用 CancelTFStrategyOrderV1 並填書號。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:981`

### CancelMovingStopLoss
- 用途：取消移動停損委託（舊版）。
- 簽名：`int CancelMovingStopLoss(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, string bstrSymbol, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（同上導引至 V1）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：同 CancelFutureStopLoss。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:990`

### CancelOptionStopLoss
- 用途：取消選擇權停損委託（舊版）。
- 簽名：`int CancelOptionStopLoss(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, string bstrSymbol, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（同上導引至 V1）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：官方文件對本函式的建議理由與其餘五個舊函式不同——原文為「為避免取消失敗，建議改用 4-2-87 並填入書號」，並非 CancelFutureStopLoss 等所述的「影響解除保證金風控」。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:999`

### CancelFutureOCO
- 用途：刪除期貨二擇一停損委託（舊版）。
- 簽名：`int CancelFutureOCO(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, string bstrSymbol, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（同上導引至 V1）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：同 CancelFutureStopLoss。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1008`

### CancelFutureMIT
- 用途：取消期貨 MIT 委託（舊版）。
- 簽名：`int CancelFutureMIT(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, string bstrSymbol, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（同上導引至 V1）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：同 CancelFutureStopLoss。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1017`

### CancelOptionMIT
- 用途：取消選擇權 MIT 委託（舊版）。
- 簽名：`int CancelOptionMIT(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSmartKey, string bstrSymbol, out string bstrMessage)`（依範例碼萃取）
- 參數表：文件未載（同上導引至 V1）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：同 CancelFutureStopLoss。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1026`

### GetStopLossReport
- 用途：新版期貨智慧單被動查詢（含停損、移動停損、二擇一、觸價單、AB 單）。
- 簽名：`int GetStopLossReport(string bstrLogInID, string bstrAccount, int nReportStatus, string bstrKind, string bstrDate)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（帳號） |
| nReportStatus | int | 0:全部的委託單 |
| bstrKind | string | STP:一般停損(含選擇權停損)(長效單) MST:移動停損 OCO:二擇一(長效單) MIT(含選擇權MIT) AB:AB單 |
| bstrDate | string | 查詢日期（EX:20220601） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnStopLossReport 事件回傳。V2.13.38 起配合智慧單平台改為新版格式（與舊版欄位相異）。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 列「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名是哪一種智慧單、也未指出缺漏逗號的欄位位置，手冊本文並無新舊欄位比較表可對照。既有解析器若以固定索引取欄位，升版後可能整體位移；建議改為「先檢查欄位數（長度）再依欄位名對映」，並在模擬環境比對實際欄位數後才上線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1158`

### 海外商品檔下載

### SKOrderLib_LoadOSCommodity
- 用途：下載海期商品檔。具海期帳號才可下載；海期委託下單前必做。
- 簽名：`int SKOrderLib_LoadOSCommodity()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註（來源版本 V2.13.57 分冊 9）：未下載即下海期單回 1035 SK_ERROR_OVERSEA_TRADE_DATA_NOT_COMPLETE。與 SKOSQuoteLib_EnterMonitorLONG 相關，可先連海期行情備妥商品檔；出現 2015（下載未完成）請重連海期行情主機或重新下載。可由 LOG（日期_OSQuote.log）確認 LoadOSCommdity；有海期帳號時預設 Login 會占用一條報價連線（海期）。
- 備註（V2.13.59 文件刪減）：V2.13.59 分冊 9 已把備註刪減為一句「具海期帳號，海期委託下單前須先下載」（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:185`），上列細節在 V2.13.59 手冊中查不到；惟相關錯誤碼（2001 海期商品檔下載失敗、2012~2015 元件不存在／連線失敗／下載未完成）仍列於 V2.13.59 錯誤碼表（`api_spec/_raw/v2.13.59/4.下單準備介紹.md:848-856`），前置條件依舊有效，故本節以 V2.13.57 留存原文為準。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:444`

### SKOrderLib_LoadOOCommodity
- 用途：下載海選商品檔與可交易商品檔；海選委託下單前必做。
- 簽名：`int SKOrderLib_LoadOOCommodity()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註（來源版本 V2.13.57 分冊 9）：與 SKOOQuoteLib_EnterMonitorLONG 相關；下載失敗回 2008/2016，出現 2015 請重連或重載。
- 備註（V2.13.59 文件刪減）：V2.13.59 分冊 9 已把備註刪減為一句「具海期帳號，海選委託下單前須先下載」（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:204`）；惟 2008（海選商品檔下載失敗）、2014（海選商品檔元件連線失敗）、2016（下載海選商品檔未完成）仍列於 V2.13.59 錯誤碼表（`api_spec/_raw/v2.13.59/4.下單準備介紹.md:850-857`），前置條件依舊有效，故本節以 V2.13.57 留存原文為準。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:452`

### 海外查詢

### GetOverseaFutures
- 用途：查詢海外期貨下單商品。
- 簽名：`int GetOverseaFutures()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：**請先執行 SKOrderLib_LoadOSCommodity**。結果由 OnOverseaFuture 事件回傳（2.13.54 起提供「商品種類」欄位，用於判斷價差單買賣別基準）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1164`

### GetOverseaOptions
- 用途：查詢海外選擇權下單商品。
- 簽名：`int GetOverseaOptions()`
- 參數表：無。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：**請先執行 SKOrderLib_LoadOOCommodity**。結果由 OnOverseaOption 事件回傳。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1176`

### GetOverseaFutureOpenInterestGW
- 用途：查詢海外期貨未平倉 GW（含彙總及明細格式）。
- 簽名：`int GetOverseaFutureOpenInterestGW(string bstrLogInID, string bstrAccount, int nFormat)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| nFormat | int | 查詢格式 1:彙總 2:明細 |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnOFOpenInterestGWReport 回傳、查詢狀態由 OnOverseaFutureOpenInterestGWStatus 回傳。
- 備註（V2.13.58／V2.13.59 新增／簡化版包裝層）：簡化版 SKDLLCSharp 包裝層另提供同功能的 `GetOFOpenInterestGW(strLogInID, strAccount, nFormat)`（V2.13.57 的 SKDLLCSharp.dll 無此方法，官方兩版 changelog 亦未列，無從判定落在哪一版）（**包裝層／原生 SKCOM.dll 匯出專用名稱，Interop.SKCOMLib 的 COM 介面中無此符號**），回傳 `OFOpenInterestParserResult`（StatusCode／Message／RawData／Blocks），為同步取值、非 COM 的「錯誤碼＋事件」模式（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:428-437`）。官方手冊「宣告」欄誤寫為 `(int Code, string Message)` 之 ValueTuple，實際回傳型別以 `SKDLLCSharp.dll` 中繼資料為準，範例碼以 `var` 承接後取用 StatusCode／Message／Blocks（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.cs:2926-2949`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1170`

### GetOverseaFutureOpenInterest
- 用途：查詢海外期貨未平倉（舊版）。
- 簽名：`int GetOverseaFutureOpenInterest(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnOverseaFutureOpenInterest 事件回傳。
- 備註（V2.13.58 修正／查無資料格式）：查無資料改回傳 `001,查無資料,帳號`（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1732`，changelog 見同檔 `:37`；V2.13.57 手冊此處原本沒有這句備註）。解析器應以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄與 3 欄兩種官方不一致格式，勿以固定欄位數判定查無資料。
- 備註（V2.13.59 修正）：分冊 9 記載本函式（非 GW 版）亦會觸發 OnOverseaFutureOpenInterestGWStatus（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:810`），但主手冊 4-2-u 仍只寫 GetOverseaFutureOpenInterestGW（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1981`）；兩者矛盾，實際行為需實測。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1153`

### GetRequestOverSeaFutureRight
- 用途：查詢海外期貨權益數。
- 簽名：`int GetRequestOverSeaFutureRight(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |

- 回傳：LONG 錯誤碼；0 成功（登入 ID 查無海期帳號回 1082）。
- 備註：結果由 OnOverSeaFutureRight 事件回傳。
- 備註（V2.13.58／V2.13.59 新增）：查詢成敗另由新事件 OnOverseaFutureRightsStatus 回傳（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1996-2002`、`api_spec/_raw/v2.13.59/9.下單-海外期選.md:817-823`）。成功時 bstrErrorMsg 為字串 `"Success!"` 而非空字串。**注意大小寫**：新的狀態事件是 `OnOverseaFutureRightsStatus`（Oversea），既有的資料事件卻是 `OnOverSeaFutureRight`（OverSea）。
- 備註（V2.13.58／V2.13.59 新增／簡化版包裝層）：簡化版 SKDLLCSharp 包裝層另提供同功能的 `GetOFFutureRights(strLogInID, strAccount, nCoinType)`（V2.13.57 的 SKDLLCSharp.dll 無此方法，官方兩版 changelog 亦未列，無從判定落在哪一版）（**包裝層／原生 SKCOM.dll 匯出專用名稱，Interop.SKCOMLib 的 COM 介面中無此符號**），回傳 `OFFutureRightsParserResult`（StatusCode／Message／RawData／Blocks），為同步取值、非 COM 的「錯誤碼＋事件」模式（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:439-448`）。官方手冊「宣告」欄誤寫為 `(int Code, string Message)` 之 ValueTuple，實際回傳型別以 `SKDLLCSharp.dll` 中繼資料為準，範例碼以 `var` 承接後取用 StatusCode／Message／Blocks（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.cs:3082-3106`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1193`

### 出入金

### WithDraw
- 用途：國內外出入金互轉。
- 簽名：`int WithDraw(string bstrLogInID, string bstrFullAccountOut, int nTypeOut, string bstrFullAccountIn, int nTypeIn, int nCurrency, string bstrDollars, string bstrPassword, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrFullAccountOut | string | 轉出期貨帳號（分公司代碼＋帳號7碼） |
| nTypeOut | int | 轉出類別 0:國內 1:國外 |
| bstrFullAccountIn | string | 轉入期貨帳號（分公司代碼＋帳號7碼） |
| nTypeIn | int | 轉入類別 0:國內 1:國外 |
| nCurrency | int | 幣別 0~9（幣別錯誤回 1059） |
| bstrDollars | string | 金額 |
| bstrPassword | string | 出入金密碼（未輸入回 1057） |
| bstrMessage | out string | 非同步：0 時為送單之 Thread ID；非 0 為失敗原因（參照 OnAsyncOrder） |

- 回傳：LONG 錯誤碼；0 成功（互轉類別錯誤回 1058）。 V2.13.58 文件調整：非同步委託 0 表示 Request 已送出（訊息內容為 Thread ID），結果由 OnAsyncOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1033`）。
- 備註：結果由 OnAsyncOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1272`

### 海外下單

### SendOverseaFutureOrder
- 用途：送出海外期貨委託。
- 簽名：`int SendOverseaFutureOrder(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | OVERSEAFUTUREORDER | 海期下單物件（見共用結構） |
| bstrMessage | out string | 同步：0 時為 13 碼委託序號；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:502`）。
- 備註：**前置：SKOrderLib_LoadOSCommodity()**。SGX DMA 專線模式下 SGX 交易所商品均經專線委託（同名函式行為切換，序號為 15 碼英數字，見 SGX DMA 分區備註）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:676`

### SendOverseaFutureOrderOLID
- 用途：送出海外期貨委託（含單獨自訂資料欄 OLID）。
- 簽名：`int SendOverseaFutureOrderOLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 SendOverseaFutureOrder，另加：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrOrderLinkedID | string | 僅非同步有效；客戶自訂資料，會在 OnAsyncOrderOLID 返回 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1131`）。
- 備註：非同步結果由 OnAsyncOrderOLID 取得（v2.13.48 調整回傳值）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:687`

### SendOverseaFutureSpreadOrder
- 用途：送出海外期貨價差委託（**V2.13.54 後不再提供**，請改用 SendOverseaFutureSpreadOrder2）。
- 簽名：`int SendOverseaFutureSpreadOrder(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureOrder（pOrder 需填 bstrYearMonth2 遠月年月；價差不提供當沖）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：前置 LoadOSCommodity。價差委託分子填負號會回 1049；價差帶當沖回 1052。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:699`

### SendOverseaFutureSpreadOrderOLID
- 用途：送出海外期貨價差委託（含 OLID；隨 SendOverseaFutureSpreadOrder 同步停用）。
- 簽名：`int SendOverseaFutureSpreadOrderOLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 SendOverseaFutureSpreadOrder＋bstrOrderLinkedID。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：非同步結果由 OnAsyncOrderOLID 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:721`

### SendOverseaFutureSpreadOrder2
- 用途：送出海外期貨價差委託（新版買賣別判斷）。帶 0:買時：以近月為主之商品＝買近賣遠；以遠月為主之商品＝買遠賣近。
- 簽名：`int SendOverseaFutureSpreadOrder2(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureSpreadOrder。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1650`）。
- 備註：商品種類（6:EQ 指數價差、8:FX 外匯價差＝以遠月為基準；7:SP 一般商品價差＝以近月為基準）由 OnOverseaFuture／OnOverseaProductsDetail 之「商品種類」欄位取得（2.13.54 起），詳見 `api_spec/_raw/9.下單-海外期選.md:5-30`。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:710`

### SendOverseaFutureSpreadOrder2OLID
- 用途：送出海外期貨價差委託 2（含 OLID）。
- 簽名：`int SendOverseaFutureSpreadOrder2OLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 SendOverseaFutureSpreadOrder2＋bstrOrderLinkedID。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrderOLID 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1662`）。
- 備註：非同步結果由 OnAsyncOrderOLID 取得；SGX 專線模式下 SGX 交易所商品均經由專線委託。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:732`

### SendOverseaOptionOrder
- 用途：送出海外選擇權委託。
- 簽名：`int SendOverseaOptionOrder(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureOrder（pOrder 用海選欄位：bstrStrikePrice/sCallPut 必填；sTradeType 固定 0:ROD；sNewClose 可新/平倉）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:524`）。
- 備註：**前置：SKOrderLib_LoadOOCommodity()**。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:760`

### SendOverseaOptionOrderOLID
- 用途：送出海外選擇權委託（含單獨自訂資料欄 OLID）。V2.13.58 新增。
- 簽名：`int SendOverseaOptionOrderOLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, string bstrOrderLinkedID, out string bstrMessage)`（COM 宣告：`Long SendOverseaOptionOrderOLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder, [in] BSTR bstrOrderLinkedID, [out] BSTR* bstrMessage)`，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1668`）
- 參數表：同 SendOverseaOptionOrder＋bstrOrderLinkedID（客戶自訂資料，**僅非同步委託有效**，由 OnAsyncOrderOLID 原值返回）。OVERSEAFUTUREORDER 結構本身未變動，OLID 以獨立參數傳入。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1674`）。
- 備註：**前置：SKOrderLib_LoadOOCommodity()**。V2.13.58 新增（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37` 三、功能新增 1）。bstrOrderLinkedID 僅在 bAsyncOrder=true 時有效，結果掛 OnAsyncOrderOLID（未新增專屬事件）。官方兩處文字不一致：備註寫「委託結果請由 OnAsyncOrderOLID 取得」，同節「回傳值」欄卻寫「結果請由 OnAsyncOrder 進行確認」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1674-1675`），實作時以 OnAsyncOrderOLID 為主、兩者皆宜監聽。
- 範例：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:770-777`；範例 UI 為 OverseaOptionOrderControl 的「SendOOOrderAsyncOLID」按鈕（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/OverseaOptionOrderControl.Designer.cs:493-500`）。官方文件本節見 `api_spec/_raw/v2.13.59/9.下單-海外期選.md:363-373`

### 海外刪改單

### OverSeaCorrectPriceByBookNo
- 用途：海期改價（依委託書號）；僅原限價單可改限價（ROD）。
- 簽名：`int OverSeaCorrectPriceByBookNo(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | OVERSEAFUTUREORDERFORGW | 改價物件（見共用結構；需填書號、序號、新價格） |
| bstrMessage | out string | 同步：修改訊息／失敗原因；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以回報為準）；非限價改價回 1065/1071。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1093`）。
- 備註：非同步結果由 OnAsyncOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:745`

### OverSeaCorrectPriceByBookNoOLID
- 用途：海期改價 OLID（依委託書號，含自訂資料欄）。
- 簽名：`int OverSeaCorrectPriceByBookNoOLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 OverSeaCorrectPriceByBookNo＋bstrOrderLinkedID（僅非同步有效，OnAsyncOrderOLID 返回）。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrderOLID 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1568`）。
- 備註：原委託為限價單方可改價（ROD）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:751`

### OverSeaCorrectPriceSpreadByBookNo
- 用途：海期價差改價（依委託書號）。
- 簽名：`int OverSeaCorrectPriceSpreadByBookNo(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, out string bstrMessage)`
- 參數表：同 OverSeaCorrectPriceByBookNo（pOrder 另填 bstrStockNo2/bstrYearMonth2 價差欄位）。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1104`）。
- 備註：原委託為限價單方可改價（ROD）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1081`

### OverSeaCorrectPriceSpreadByBookNoOLID
- 用途：海期價差改價 OLID（依委託書號）。
- 簽名：`int OverSeaCorrectPriceSpreadByBookNoOLID(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同上＋bstrOrderLinkedID。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrderOLID 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1580`）。
- 備註：非同步結果由 OnAsyncOrderOLID 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1087`

### OverSeaOptionCorrectPriceByBookNo
- 用途：海選改價（依委託書號）。
- 簽名：`int OverSeaOptionCorrectPriceByBookNo(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, out string bstrMessage)`
- 參數表：同 OverSeaCorrectPriceByBookNo（pOrder 需另填海選 Call/Put、履約價欄位）。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1115`）。
- 備註：原委託為限價單方可改價（ROD）。V2.13.45 起已刪除「僅支援自然人身份」限制。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1097`

### OverSeaDecreaseOrderBySeqNo
- 用途：海期委託減量（依委託序號）。SGX DMA 專線模式亦用本函式（15 碼英數字序號）。
- 簽名：`int OverSeaDecreaseOrderBySeqNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, int nDecreaseQty, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| bstrSeqNo | string | 欲減量的委託序號（一般 13 碼；SGX DMA 為 15 碼） |
| nDecreaseQty | int | 欲減少的數量 |
| bstrMessage | out string | 同步：修改訊息／失敗原因（SGX DMA：M000＋修改訊息）；非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0＝接收成功（以減量回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:594`）。
- 備註：SGX DMA 實際結果請以專線回報資料為主。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:921`

### OverSeaDecreaseOrderBySeqNoOLID
- 用途：海期委託減量 OLID（依委託序號）。
- 簽名：`int OverSeaDecreaseOrderBySeqNoOLID(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, int nDecreaseQty, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 OverSeaDecreaseOrderBySeqNo＋bstrOrderLinkedID（OnAsyncOrderOLID 返回）。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrderOLID 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1556`）。
- 備註：非同步結果由 OnAsyncOrderOLID 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:927`

### OverSeaCancelOrderBySeqNo
- 用途：海外期貨委託刪單（依委託序號）。SGX DMA 專線亦用本函式（15 碼序號由專線刪單）。
- 簽名：`int OverSeaCancelOrderBySeqNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| bstrSeqNo | string | 欲刪除的委託序號（SGX DMA 為 15 碼） |
| bstrMessage | out string | 同步：0 時為原始委託 13 碼（V2.13.45 起；SGX DMA 為 M000＋刪單訊息）；非 0 為失敗原因 |

- 回傳：LONG 錯誤碼；0＝接收成功（以刪單回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:773`）。
- 備註：SGX DMA 實際刪單成功與否以專線回報為主。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1055`

### OverSeaCancelOrderBySeqNoOLID
- 用途：海外期貨委託刪單 OLID（依委託序號）。
- 簽名：`int OverSeaCancelOrderBySeqNoOLID(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 OverSeaCancelOrderBySeqNo＋bstrOrderLinkedID。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrderOLID 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1542`）。
- 備註：非同步結果由 OnAsyncOrderOLID 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1061`

### OverSeaCancelOrderByBookNo
- 用途：海外期貨委託刪單（依委託書號）。
- 簽名：`int OverSeaCancelOrderByBookNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrBookNo, out string bstrMessage)`
- 參數表：同 OverSeaCancelOrderBySeqNo，但以 `bstrBookNo`（欲刪除的書號）取代序號。
- 回傳：LONG 錯誤碼；0＝接收成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:802`）。
- 備註：SGX DMA 專線不支援書號刪改（改價限序號，1072）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1070`

### 海外智慧單

### SendOverSeaFutureOCOOrder
- 用途：送出海外期貨 OCO（含長效）委託。
- 簽名：`int SendOverSeaFutureOCOOrder(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | OVERSEAFUTUREORDER | 海期智慧單 OCO 變體（兩腳觸發價/委託價、nReserved/nTimeFlag/長效單欄位） |
| bstrMessage | out string | 同步：0 時為 13 碼委託序號；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1414`）。
- 備註：**前置：SKOrderLib_LoadOSCommodity()**。v2.13.47 修正 nTimeFlag 參數（1:T盤 2:T+1盤）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1878`

### SendOverSeaFutureABOrder
- 用途：送出海外期貨 AB 單委託（看 A 下 B）。
- 簽名：`int SendOverSeaFutureABOrder(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDER pOrder, out string bstrMessage)`
- 參數表：同 SendOverSeaFutureOCOOrder（pOrder 用海期智慧單 AB 變體：看 A 欄位 bstrExchangeNo/bstrStockNo2/nMarketNo/bstrOrder2/nTriggerDirection/bstrTrigger；下 B 欄位含價差與選擇權欄位）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1425`）。
- 備註：前置 LoadOSCommodity；v2.13.48 修正 AB 單物件委託價別。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1906`

### CancelOFStrategyOrder
- 用途：取消海期智慧單委託（OCO/AB；刪單欄位參考 GetOFSmartStrategyReport 回傳內容）。
- 簽名：`int CancelOFStrategyOrder(string bstrLogInID, ref CANCELSTRATEGYORDER pCancelOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pCancelOrder | CANCELSTRATEGYORDER | 海期刪單變體（nMarket 1~4、nTradeKind 3:OCO 10:AB、bstrSeqNo/bstrOrderNo/bstrLongActionKey） |
| bstrMessage | out string | 非同步刪單：0 時為刪單之 Thread ID；非 0 為失敗原因 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1435`）。
- 備註：已觸發需給委託書號；刪單後透過智慧單被動回報確認狀態。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1889`

### GetOFSmartStrategyReport
- 用途：海期智慧單被動查詢。
- 簽名：`int GetOFSmartStrategyReport(string bstrLogInID, string bstrAccount, string bstrMarketType, int nReportStatus, string bstrKind, string bstrDate)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（帳號） |
| bstrMarketType | string | OF:海期市場 |
| nReportStatus | int | 0:全部的委託單 |
| bstrKind | string | OCO:二擇一 AB:AB單 |
| bstrDate | string | 查詢日期（EX:20201001） |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：結果由 OnOFSmartStrategyReport 事件回傳。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 列「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名是哪一種智慧單、也未指出缺漏逗號的欄位位置，手冊本文並無新舊欄位比較表可對照。既有解析器若以固定索引取欄位，升版後可能整體位移；建議改為「先檢查欄位數（長度）再依欄位名對映」，並在模擬環境比對實際欄位數後才上線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1898`

### 複委託

### SendForeignStockOrder
- 用途：送出複委託（美/港/日/新加坡/滬深股）。
- 簽名：`int SendForeignStockOrder(string bstrLogInID, bool bAsyncOrder, ref FOREIGNORDER pOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| pOrder | FOREIGNORDER | 複委託下單物件（見共用結構） |
| bstrMessage | out string | 同步：0 時為 13 碼委託序號；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功（委託類別未填回 1110、庫存類別未填回 1111）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:568`）。
- 備註：Ver 2.13.46+ 新增幣別 CNY、GBP 與滬深股市場。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:873`

### SendForeignStockOrderOLID
- 用途：送出複委託（含單獨自訂資料欄 OLID；V2.13.48 新增）。
- 簽名：`int SendForeignStockOrderOLID(string bstrLogInID, bool bAsyncOrder, ref FOREIGNORDER pOrder, string bstrOrderLinkedID, out string bstrMessage)`
- 參數表：同 SendForeignStockOrder＋bstrOrderLinkedID（僅非同步有效，OnAsyncOrderOLID 返回）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrderOLID 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1492`）。
- 備註：非同步結果由 OnAsyncOrderOLID 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1977`

### CancelForeignStockOrder
- 用途：新版複委託刪單（需同時填序號及委託書號）。
- 簽名：`int CancelForeignStockOrder(string bstrLogInID, bool bAsyncOrder, ref FOREIGNORDER pOrder, out string bstrMessage)`
- 參數表：同 SendForeignStockOrder（pOrder 用刪單欄位：bstrSeqNo/bstrBookNo 必填、nOrderType=4）。
- 回傳：LONG 錯誤碼；0＝接收成功（以刪單回報為準）。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnAsyncOrder（Proxy 為 OnProxyOrder）確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1360`）。
- 備註：V2.13.42 新增。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/UpdateOrderForm/OSUpdateOrderForm.cs:75`

### GetBankBlock
- 用途：複委託外幣圈存查詢（一戶通）。
- 簽名：`string GetBankBlock(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 複委託帳號：分公司四碼＋帳號7碼 |

- 回傳：查詢字串；欄位以「,」分隔、結尾「#」（EX: `CNY,5000000.00,1070.08,0.00,452.45,4998477.47,0.00,011,002,11111111111111,上銀營`）。
- 備註：直接回傳字串。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1316`

### GetNTDBlock
- 用途：複委託台幣圈存查詢（一戶通）。
- 簽名：`string GetNTDBlock(string bstrLogInID, string bstrAccount)`
- 參數表：同 GetBankBlock。
- 回傳：查詢字串；欄位以「,」分隔、結尾「#」。
- 備註：直接回傳字串。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1334`

### SGX DMA 專線

> 前置：SGX API DMA 專線需向營業員／交易後台申請。開啟方式：登入前呼叫 SKCenterLib_SetAuthority（SGX 專線屬性 0/1，見 SKCenterLib.md），登入後 AddSGXAPIOrderSocket 建線，連線狀態由 SKCenterLib 事件 OnNotifySGXAPIOrderStatus 回傳（3026＝專線建立完成、3002＝斷線、1053＝登入失敗）。專線模式下海期下單/價差（SendOverseaFutureOrder／SendOverseaFutureSpreadOrder 系列）、減量/刪單（OverSeaDecreaseOrderBySeqNo／OverSeaCancelOrderBySeqNo）均自動走專線，委託序號為 M000＋15 碼英數字（EX: `M000 EB1234567890123`）。

### AddSGXAPIOrderSocket
- 用途：建立 SGX API 專線。
- 簽名：`int AddSGXAPIOrderSocket(string bstrLogInID, string bstrAccount)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccount | string | 委託帳號（IB＋帳號） |

- 回傳：LONG 錯誤碼；0 成功（未取得特殊權限回 158；登入失敗 1053）。
- 備註：連線結果由 OnNotifySGXAPIOrderStatus（SKCenterLib 事件）取得，確認 3026 後方可委託。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1510`

### OverSeaCorrectPriceBySGXAPISeqNo
- 用途：(SGX DMA 專線)送出海外期貨改價委託（依 15 碼委託序號；SGX 專線無書號改價）。
- 簽名：`int OverSeaCorrectPriceBySGXAPISeqNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, string bstrSeqNo, string bstrCorrectPrice, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bAsyncOrder | bool | 是否為非同步委託 |
| bstrAccount | string | 委託帳號（IB＋帳號） |
| bstrSeqNo | string | 欲改價的 15 碼委託序號（錯誤回 1073） |
| bstrCorrectPrice | string | 修改價格 |
| bstrMessage | out string | 同步：0 時為 M000＋改價訊息；非 0 為失敗原因。非同步參照 OnAsyncOrder |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：僅 SGX API DMA 專線可用；實際改價成功與否以專線回報資料為主。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:936`

### ProxyServer 下單

> 前置：先 SKOrderLib_InitialProxyByID 建線並登入，**等 OnProxyStatus 回傳 5001（登入成功）後才可送 Proxy 單**；所有 Proxy 下單/刪改結果由 OnProxyOrder 事件回傳（送出成功 bstrMessage 為 ORKEY，每筆委託回兩筆通知）。Proxy 錯誤代碼 5001~5019 見 [../error_codes.md](../error_codes.md)。

### SKOrderLib_InitialProxyByID
- 用途：以使用者 ID 初始化 Proxy 下單連線（會先連線 proxy server 並登入）。
- 簽名：`int SKOrderLib_InitialProxyByID(string bstrLogInID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |

- 回傳：LONG 錯誤碼；0 成功。
- 備註：回傳 0 後需同時確認 OnProxyStatus 收到 5001 才算連線且登入成功；回傳非 0 請重新執行本函式。HANDLER 已存在回 5006。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1658`

### ProxyDisconnectByID
- 用途：以使用者 ID 主動斷線 proxy server。
- 簽名：`int ProxyDisconnectByID(string bstrLogInID)`
- 參數表：同上（bstrLogInID）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：範例碼的訊息字串使用別名「SKOrderLib_ProxyDisconnectByID」，實際方法名無前綴。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1632`

### ProxyReconnectByID
- 用途：以使用者 ID 重新連線先前主動斷線的 proxy server，並執行 login。
- 簽名：`int ProxyReconnectByID(string bstrLogInID)`
- 參數表：同上（bstrLogInID）。
- 回傳：LONG 錯誤碼；0＝連線成功。
- 備註：5002/5003/5004/5011 等狀態下依代碼表指示以本函式重連。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1644`

### SendStockProxyOrder
- 用途：經由 proxy server 送出證券委託。
- 簽名：`int SendStockProxyOrder(string bstrLogInID, ref STOCKPROXYORDER pSTOCKPROXYORDER, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pSTOCKPROXYORDER | STOCKPROXYORDER | Proxy 證券下單物件（見共用結構） |
| bstrMessage | out string | 委託送出成功：ORKEY；失敗：錯誤訊息 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2980`）。
- 備註：委託結果由 OnProxyOrder 取得；需 OnProxyStatus=5001。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Order/SKProxyOrder/SKProxySendOrderForm/TSSKProxySendOrderForm.cs:129`

### SendStockProxyAlter
- 用途：經由 proxy server 送出證券刪改單。
- 簽名：`int SendStockProxyAlter(string bstrLogInID, ref STOCKPROXYORDER pSTOCKPROXYORDER, out string bstrMessage)`
- 參數表：同 SendStockProxyOrder（pOrder 用刪改單欄位：bstrOrderType 0刪/1改量/2改價＋bstrBookNo/bstrSeqNo）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2990`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1686`

### SendStockProxyPreAlter
- 用途：經由 proxy server 送出證券特殊刪改單（帶入序號；V2.13.55 新增）。
- 簽名：`int SendStockProxyPreAlter(string bstrLogInID, ref STOCKPROXYORDER pSTOCKPROXYORDER, out string bstrMessage)`
- 參數表：同 SendStockProxyOrder（pOrder 用特殊刪改欄位；**序號帶 "" 或不帶會刪除證券市場所有委託**；支援股號刪單、股號＋價格＋買賣別、股號＋買賣別三種情境，用 bstrStockNo/bstrPrice_forD/bstrBuySell_forD）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3120`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1697`

### SendFutureProxyOrderCLR
- 用途：經由 proxy server 送出期貨委託，需設倉別與盤別。
- 簽名：`int SendFutureProxyOrderCLR(string bstrLogInID, ref FUTUREPROXYORDER pFUTUREPROXYORDER, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pFUTUREPROXYORDER | FUTUREPROXYORDER | Proxy 期貨下單物件（見共用結構） |
| bstrMessage | out string | 委託送出成功：ORKEY；失敗：錯誤訊息 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3000`）。
- 備註：近月商品（TX00）請以 bstrStockNo=`FITX`＋bstrSettleYM=近月月份下單。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1707`

### SendFutureProxyAlter
- 用途：經由 proxy server 送出期貨刪改單。
- 簽名：`int SendFutureProxyAlter(string bstrLogInID, ref FUTUREPROXYORDER pFUTUREPROXYORDER, out string bstrMessage)`
- 參數表：同 SendFutureProxyOrderCLR（pOrder 用期選刪改欄位：bstrOrderType 0刪/1減量/2改價＋書號/序號）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3010`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1717`

### SendOptionProxyOrder
- 用途：經由 proxy server 送出選擇權委託。
- 簽名：`int SendOptionProxyOrder(string bstrLogInID, ref FUTUREPROXYORDER pFUTUREPROXYORDER, out string bstrMessage)`
- 參數表：同 SendFutureProxyOrderCLR（pOrder 用選擇權欄位：bstrStrike/nCP）。週選下單 EX：2024/3 第 4 週 → bstrStockNo=`TX4`、bstrSettleYM=`202403`。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3020`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1747`

### SendOptionProxyAlter
- 用途：經由 proxy server 送出選擇權刪改單。
- 簽名：`int SendOptionProxyAlter(string bstrLogInID, ref FUTUREPROXYORDER pFUTUREPROXYORDER, out string bstrMessage)`
- 參數表：同 SendFutureProxyAlter。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3030`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1757`

### SendDuplexProxyOrder
- 用途：經由 proxy server 送出選擇權複式下單。
- 簽名：`int SendDuplexProxyOrder(string bstrLogInID, ref FUTUREPROXYORDER pFUTUREPROXYORDER, out string bstrMessage)`
- 參數表：同 SendOptionProxyOrder（pOrder 用複式單欄位：bstrSettleYM2/bstrStrike2/nCP2/nBuySell2；nTradeType 僅 1:IOC 2:FOK）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3040`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1767`

### SendOverseaFutureProxyOrder
- 用途：經由 proxy server 送出海期下單。
- 簽名：`int SendOverseaFutureProxyOrder(string bstrLogInID, ref OVERSEAFUTUREORDER pSKProxyOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pSKProxyOrder | OVERSEAFUTUREORDER | Proxy 海期下單物件（見共用結構） |
| bstrMessage | out string | 委託送出成功：ORKEY；失敗：錯誤訊息 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3050`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1666`

### SendOverseaFutureSpreadProxyOrder
- 用途：經由 proxy server 送出海期價差單（**V2.13.54 後不再提供**，請改用 SendOverseaFutureSpreadProxyOrder2，注意買賣別判斷已更動）。
- 簽名：`int SendOverseaFutureSpreadProxyOrder(string bstrLogInID, ref OVERSEAFUTUREORDER pSKProxyOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureProxyOrder（pOrder 需填 bstrYearMonth2）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1788`

### SendOverseaFutureSpreadProxyOrder2
- 用途：經由 proxy server 送出海期價差單（新版買賣別判斷，同 SendOverseaFutureSpreadOrder2）。
- 簽名：`int SendOverseaFutureSpreadProxyOrder2(string bstrLogInID, ref OVERSEAFUTUREORDER pSKProxyOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureSpreadProxyOrder。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3110`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1797`

### SendOverseaOptionProxyOrder
- 用途：經由 proxy server 送出海選下單。
- 簽名：`int SendOverseaOptionProxyOrder(string bstrLogInID, ref OVERSEAFUTUREORDER pSKProxyOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureProxyOrder（pOrder 用海選欄位：bstrStrikePrice/sCallPut）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3080`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1807`

### SendOverseaFutureProxyAlter
- 用途：經由 proxy server 送出海期選刪改單。
- 簽名：`int SendOverseaFutureProxyAlter(string bstrLogInID, ref OVERSEAFUTUREORDER pAsyncOrder, out string bstrMessage)`
- 參數表：同 SendOverseaFutureProxyOrder（pOrder 用刪改欄位：nSpreadFlag 0海期/1價差/2海選、nAlterType 0刪/1減量/2改價、bstrBookNo/bstrSeqNo）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3070`）。
- 備註：委託結果由 OnProxyOrder 取得。v2.13.48 修正 Proxy 海選刪改單物件。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1778`

### SendForeignStockProxyOrder
- 用途：經由 proxy server 送出複委託下單。
- 簽名：`int SendForeignStockProxyOrder(string bstrLogInID, ref OSSTOCKPROXYORDER pAsyncOrder, out string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| pAsyncOrder | OSSTOCKPROXYORDER | Proxy 複委託下單物件（見共用結構；委託量為字串 bstrProxyQty） |
| bstrMessage | out string | 委託送出成功：ORKEY；失敗：錯誤訊息 |

- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3090`）。
- 備註：委託結果由 OnProxyOrder 取得。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1737`

### SendForeignStockProxyCancel
- 用途：經由 proxy server 送出複委託刪單。
- 簽名：`int SendForeignStockProxyCancel(string bstrLogInID, ref OSSTOCKPROXYORDER pAsyncOrder, out string bstrMessage)`
- 參數表：同 SendForeignStockProxyOrder（pOrder 用刪單欄位：bstrSeqNo/bstrBookNo 必填、nOrderType=4）。
- 回傳：LONG 錯誤碼；0 成功。 V2.13.58 文件調整：0 表示已成功送至交易所（非交易成立），交易結果請由回報確認；非同步委託 0 表示 Request 已送出，結果由 OnProxyOrder 確認（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3100`）。
- 備註（V2.13.59 修正）：**委託結果由 OnProxyOrder 取得**；且須在連線且成功登入、OnProxyStatus 通知 5001 後才會送至 proxy server 下單（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3101`、`api_spec/_raw/v2.13.59/11.下單-複委託.md:137`）。V2.13.57 手冊此處誤植為「使用非同步委託，委託結果請由 OnAsyncOrder 取得」，V2.13.59 已更正為與其他 Proxy 函式一致的寫法（其他 Proxy 函式如 SendStockProxyPreAlter 兩版本來就是這個寫法，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:3121`；本函式是唯一被更正的一支）。依 V2.13.57 文件字面只掛 OnAsyncOrder 的程式會收不到通知，請改掛 OnProxyOrder。
- 備註（文件落後）：ProxyServer 專用分冊至今仍是舊寫法「委託結果請由 OnAsyncOrder 取得」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_ProxyServer下單元件.md:187`），該分冊自身標示文件版本 V2.13.47（同檔 `:6`）未隨主手冊更新，以主手冊為準。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1727`

## 僅見於範例碼

以下成員存在於官方 C# 範例碼（多數在 SKCOMTester/SKOrder.cs），但 21 份官方手冊中無對應參數表；簽名由範例碼實際呼叫萃取（變數型別依範例上下文推斷），**簽名待確認**、參數表文件未載。使用前建議先以小額/模擬環境驗證。

### SKOrderLib_MCInitialize
- 用途：MC（MultiCharts）品牌情境的下單初始化（範例在 MC 分頁以此取代 SKOrderLib_Initialize）。
- 簽名：`int SKOrderLib_MCInitialize(string bstrLogInID)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：搭配 SKCenterLib_SetMCBrand 等品牌設定使用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1612`

### SKOrderLib_LoadOfCommodityGW
- 用途：下載海期 GW 商品檔（範例按鈕「DownloadOFGW」）。
- 簽名：`int SKOrderLib_LoadOfCommodityGW(string bstrLogInID)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：與 SGX DMA 旗標搭配（範例載入後設定 SGXDMA 屬性）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1618`

### SKOrderLib_UpdateToken
- 用途：更新登入 Token／密碼權杖；結果由 OnPasswordUpdateToken 事件回傳。
- 簽名：`int SKOrderLib_UpdateToken(string bstrLogInID)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：範例碼中已註解停用；對應事件 OnPasswordUpdateToken。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1532`（已註解）

### CapitalPayWithDraw
- 用途：一戶通出金（指定銀行代碼／SWIFT／銀行帳號／金額）。
- 簽名：`int CapitalPayWithDraw(string bstrLogInID, string bstrBankCode, string bstrSwiftCode, string bstrBankAcno, string bstrDollars, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：範例訊息為「一戶通出金」。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1281`

### DecreaseOrderByBookNo
- 用途：依 5 碼書號委託減量（含市場類別參數）。
- 簽名：`int DecreaseOrderByBookNo(string bstrLogInID, bool bAsyncOrder, string bstrAccount, int nMarketType, string bstrBookNo, int nDecreaseQty, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載（nMarketType 推測同 SetMaxQty 的市場代碼或 TS/TF/TO 類別）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：對應事件推測為 OnAsyncOrder。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:891`

### SpecialRequest
- 用途：特殊指示申請（交易別/申請日/股票/數量/金額/交割日等）。
- 簽名：`string SpecialRequest(string bstrLogInID, string bstrTradeType, string bstrApplyDate, string bstrStockID, string bstrQty, string bstrAmt, string bstrBrokerID, string bstrAcno, string bstrPaymentDate)`（簽名待確認）
- 參數表：文件未載。
- 回傳：查詢/申請結果字串。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1915`（已註解）

### SendStockStrategyFTLDayTrade
- 用途：證券智慧單「快速當沖 OCO」策略委託。
- 簽名：`int SendStockStrategyFTLDayTrade(string bstrLogInID, bool bAsyncOrder, ref STOCKSTRATEGYORDER pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載（pOrder 為 STOCKSTRATEGYORDER）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1957`（已註解）

### BasketOptionSimulation
- 用途：選擇權全拆組單試算（試算結果供 AllCoverDisOptions 委託）。
- 簽名：`int BasketOptionSimulation(string bstrLogInID, ref FUTUREORDER pFutureOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載（bstrMessage 內容以「#」分隔多筆）。
- 回傳：LONG 錯誤碼；0 成功。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:1986`（已註解）

### SendStockOrderGW
- 用途：證券委託 GW 通道。
- 簽名：`int SendStockOrderGW(string bstrLogInID, bool bAsyncOrder, ref STOCKORDER pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用；對應事件推測為 OnAsyncOrderGW。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:565`（已註解）

### AlterStockOrder
- 用途：異動證券委託 GW（刪改）。
- 簽名：`int AlterStockOrder(string bstrLogInID, bool bAsyncOrder, ref STOCKORDER pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:575`（已註解）

### SendFutureOrderGW
- 用途：期貨委託 GW 通道。
- 簽名：`int SendFutureOrderGW(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:602`（已註解）

### AlterTFTOOrder
- 用途：期貨/選擇權委託異動 GW（刪改）。
- 簽名：`int AlterTFTOOrder(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:611`（已註解）

### SendOptionOrderGW
- 用途：選擇權委託 GW 通道。
- 簽名：`int SendOptionOrderGW(string bstrLogInID, bool bAsyncOrder, ref FUTUREORDER pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:629`（已註解）

### OverSeaFutureOrderGW
- 用途：海期委託 GW／SGX DMA 通道（含 OLID 參數）。
- 簽名：`int OverSeaFutureOrderGW(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, string bstrOrderLinkedID, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載（pOrder 為 OVERSEAFUTUREORDERFORGW）。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用；範例依 SGX 旗標顯示「SGX DMA海期委託」或「海期委託GW」。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:770`（已註解）

### AlterOverSeaFutureOrder
- 用途：異動海期委託 GW（刪改）。
- 簽名：`int AlterOverSeaFutureOrder(string bstrLogInID, bool bAsyncOrder, ref OVERSEAFUTUREORDERFORGW pOrder, out string bstrMessage)`（簽名待確認）
- 參數表：文件未載。
- 回傳：LONG 錯誤碼。
- 備註：範例碼中已註解停用。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:783`（已註解）

## 事件

事件由 `_ISKOrderLibEvents_*EventHandler` 委派掛載（見「初始化與事件註冊」）。handler 參數即事件簽名；多筆資料的查詢型事件以「##」開頭的一筆表示查詢結束。

### OnAccount
- 用途：帳號資訊回傳。呼叫 GetUserAccount 後觸發，每個帳號一筆。
- 簽名：`void OnAccount(string bstrLogInID, string bstrAccountData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string | 登入ID |
| bstrAccountData | string | 以逗點分隔：市場,分公司代碼,分公司,帳號,身份證字號,姓名（證券:分公司代碼；期貨:Broker id） |

- 回傳：無（void）。
- 備註：登入前需簽署 API 下單聲明書方能取得相關市場帳號。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:353`

### OnAsyncOrder
- 用途：非同步委託結果（幾乎所有 bAsyncOrder=true 的下單/刪改函式共用）。
- 簽名：`void OnAsyncOrder(int nThreadID, int nCode, string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nThreadID | int | 送單時取得的 Thread ID，用於對應下單來源 |
| nCode | int | 收單結果代碼（0 成功） |
| bstrMessage | string | 成功：委託序號；失敗：失敗原因。期貨智慧單：`000,日期,訊息 條件單號:xxx,書號,智慧單號,13碼序號`。SGX DMA：`M000 15碼英數序號`／`M999+原因` |

- 回傳：無。
- 備註：SGX DMA 下即使價格有誤仍可能收到 15 碼序號，實際成敗以專線回報為準。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:137`

### OnAsyncOrderOLID
- 用途：非同步委託結果（含單獨自訂資料欄），對應 *OLID 系列下單函式。
- 簽名：`void OnAsyncOrderOLID(int nThreadID, int nCode, string bstrMessage, string bstrOrderLinkedID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nThreadID | int | Thread ID |
| nCode | int | 收單結果代碼 |
| bstrMessage | string | 收單回傳訊息（13 碼序號） |
| bstrOrderLinkedID | string | 客戶自訂資料（下單時帶入的原值） |

- 回傳：無。
- 備註：v2.13.48 調整海期 OLID 回傳值。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:142`

### OnAsyncOrderGW
- 用途：（僅見於範例碼）GW 通道非同步委託結果。
- 簽名：`void OnAsyncOrderGW(int nThreadID, int nCode, string bstrMessage)`
- 參數表：同 OnAsyncOrder（文件未載）。
- 回傳：無。
- 備註：對應 SendStockOrderGW 等 GW 函式；文件未載。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:164`

### OnProxyStatus
- 用途：Proxy 連線/登入狀態。一個使用者 ID 與 proxy server 建一條連線，SKOrderLib_InitialProxyByID 後回傳該連線狀態。
- 簽名：`void OnProxyStatus(string bstrUserId, int nCode)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserId | string | 使用者 ID |
| nCode | int | 狀態代碼 5001~5006、5009~5019（5001＝連線且登入成功，才可送 Proxy 單；5009＝已送登入等待 5001） |

- 回傳：無。
- 備註：代碼對照見 [../error_codes.md](../error_codes.md) Proxy 段。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:244`

### OnProxyOrder
- 用途：Proxy 委託結果（每筆委託回傳二筆通知）。
- 簽名：`void OnProxyOrder(int nStampID, int nCode, string bstrMessage)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStampID | int | StampID（v2.13.47 補文字說明） |
| nCode | int | Proxy 收單結果代碼 |
| bstrMessage | string | Proxy 收單回傳訊息（v2.13.48 新增證券下單回傳欄位） |

- 回傳：無。
- 備註：ProxyServer 專文之宣告漏列 nStampID，以範例碼 3 參數為準。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:309`

### OnTelnetTest
- 用途：SKOrderLib_TelnetTest 的測試結果。
- 簽名：`void OnTelnetTest(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 「測試中」→「共測試x台主機 : 成功y台, 失敗z台」 |

- 回傳：無。
- 備註：無。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:240`

### OnPasswordUpdateToken
- 用途：（僅見於範例碼）Token/密碼權杖更新結果，對應 SKOrderLib_UpdateToken。
- 簽名：`void OnPasswordUpdateToken(int nStatus, string bstrLoginID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStatus | int | 更新狀態（文件未載） |
| bstrLoginID | string | 登入ID |

- 回傳：無。
- 備註：文件未載。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:147`

### OnRealBalanceReport
- 用途：證券即時庫存資料，GetRealBalanceReport 後逐筆回傳。
- 簽名：`void OnRealBalanceReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔：股票代號,庫存種類(T集保/C融資/L融券),資額度(原始),資額度(可用),券額度(原始),券額度(可用),昨日庫存股數,今日委買,今日委賣,今日買進成交,今日賣出成交,今日資券可回補/集保庫存可賣出,可資沖股數,可券沖股數,即時庫存,X(忽略),即時個股維持率,LOGIN_ID,ACCOUNT_NO |

- 回傳：無。
- 備註：全部回傳完畢時回一筆「##」開頭內容。v2.13.42~54「可資沖/可券沖」欄位互換，v2.13.55 修正。
- 備註（V2.13.59 修正）：官方 changelog 記「自營帳號即時庫存查詢 GetRealBalanceReport 欄位新增"昨日庫存"」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但本事件的欄位序列兩版逐項相同、原本就含「股數:昨日庫存」（同檔 `:1708`），**changelog 說「新增欄位」、手冊欄位表卻兩版逐字相同，兩者無法從文件調和**，官方未說明差異來源。實際欄位數與欄位序列請在模擬環境以自營帳號實測確認。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:169`

### OnBalanceQuery
- 用途：集保庫存查詢結果（GetBalanceQuery 觸發；該函式 V2.13.54 起停用）。
- 簽名：`void OnBalanceQuery(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔的庫存欄位；【M003】表示沒有庫存 |

- 回傳：無。
- 備註：新開發請改用 GetRealBalanceReport＋OnRealBalanceReport。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:223`

### OnMarginPurchaseAmountLimit
- 用途：資券配額查詢結果（GetMarginPurchaseAmountLimit 觸發）。
- 簽名：`void OnMarginPurchaseAmountLimit(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 【M000】成功後以「,」分隔各欄位；【M003】表示無信用交易資格 |

- 回傳：無。
- 備註：無。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:218`

### OnProfitLossGWReport
- 用途：證券新損益查詢結果（GetProfitLossGWReport 觸發；未實現/已實現/現股當沖三種格式）。
- 簽名：`void OnProfitLossGWReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 第一筆為查詢結果（成功「000,訊息」；失敗「錯誤代碼,錯誤訊息」）；第二筆起為資料，以「,」分隔（彙總/明細/投資總額等格式依查詢物件 nFunc） |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」開頭內容。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:232`

### OnRequestProfitReport
- 用途：舊版證券即時損益試算結果（即將下線）。
- 簽名：`void OnRequestProfitReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔的損益欄位（自:自辦券商/代:代辦信用） |

- 回傳：無。
- 備註：請改用 OnProfitLossGWReport。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:208`

### OnTSSmartStrategyReport
- 用途：證券智慧單被動查詢結果（GetTSSmartStrategyReport 觸發），依智慧單類型（MIOC/MST/MIT/當沖母單/進場單/出場單/出清/OCO/AB/CB）提供不同欄位格式。
- 簽名：`void OnTSSmartStrategyReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔；【M003,無資料】表示查無被動回報；資料排序為最後一筆委託先回傳 |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」開頭內容。V2.13.30 起 MIT 提供新版被動回報。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 列「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名是哪一種智慧單、也未指出缺漏逗號的欄位位置，手冊本文並無新舊欄位比較表可對照。既有解析器若以固定索引取欄位，升版後可能整體位移；建議改為「先檢查欄位數（長度）再依欄位名對映」，並在模擬環境比對實際欄位數後才上線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:227`

### OnOpenInterest
- 用途：國內期貨未平倉資料；GetOpenInterestGW（GW 格式）、GetOpenInterest（舊版）、GetOpenInterestWithFormat（格式 1~3）皆由本事件回傳。
- 簽名：`void OnOpenInterest(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔，欄位依查詢格式而異；GW 版含複式單（市場別 TM） |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」開頭內容；查無資料回 `001,查無資料,帳號`（V2.13.57 手冊此處分兩種寫法：GetOpenInterest 版為 `M003 NO DATA#`、GetOpenInterestGW 版為 `001 查無資料`，V2.13.58 起統一）。
- 備註（V2.13.58 修正／查無資料格式）：三種查詢的「查無資料」皆統一為「新增 Account 欄位」後的格式（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37` changelog；本文見同檔 `:1716`／`:1721`／`:1725`）。解析器應以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄（`api_spec/_raw/v2.13.59/7.下單-國內期選.md:724`）與 3 欄（主手冊）兩種官方不一致格式；**勿**再比對 `M003 NO DATA`，也勿以固定欄位數判定查無資料。官方分冊 7 的示範解析即以 `values[0] == "001" || values[0] == "970"` 判斷（`api_spec/_raw/v2.13.59/7.下單-國內期選.md:635`）。
- 備註（V2.13.59 新增欄位）：GW 版（GetOpenInterestGW）新增「商品－下單代碼」欄位，查詢異常時該欄回空值、須重新呼叫 GetOpenInterestGW（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1725`）；欄位序列在官方 docx 內為圖片，插入位置未經文件確認，需實測。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:174`

### OnOpenInterestJson
- 用途：國內期貨未平倉「一次回傳所有庫存」（JSON 陣列格式）。V2.13.58 新增，由 GetOpenInterestGW、GetOpenInterest、GetOpenInterestWithFormat 三支查詢皆會觸發（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2005`、`api_spec/_raw/v2.13.59/7.下單-國內期選.md:720`）。與逐筆回傳的 OnOpenInterest 並存；**官方未說明兩者是同時觸發還是需擇一訂閱**，同時訂閱是否會拿到重複資料、如何去重，手冊與範例均無交代，需實測。
- 簽名：`void OnOpenInterestJson(string bstrData)`（COM 宣告：`void OnOpenInterestJson([in] BSTR bstrData)`，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2007`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | JSON 陣列字串，一次帶回全部未平倉；**陣列元素內部仍是逗號分隔的欄位字串**，非巢狀物件。欄位序列依觸發來源（GetOpenInterest／GetOpenInterestWithFormat 格式 1~3／GetOpenInterestGW）而異。官方回傳範例：`["TM,F0200009999999,TXU28500/28250K5,0,0,17,0,F123456789","TO,F0200009999999,TXU28500K5,43,0,0,0,F123456789"]` |

- 回傳：無（void）。
- 備註：查無資料回傳 `001,查無資料,帳號`（主手冊 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2009`）／`001,查無資料`（分冊 7 `api_spec/_raw/v2.13.59/7.下單-國內期選.md:724`），兩者官方寫法不一致，解析請同時容忍 2 欄與 3 欄。本事件為 V2.13.58 changelog「一、功能異動 1」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37`）。
- 備註（範例碼陷阱，勿照抄）：官方示範以 `IndexOf('"')` 逐段配對雙引號手刻切割，並未使用 JSON 函式庫；元素內容若含跳脫雙引號或巢狀結構會切錯或漏項。另外其空值檢查寫成 `bstrData.Contains("001,查無資料") || string.IsNullOrWhiteSpace(bstrData)`——`Contains` 排在 null 檢查之前，bstrData 為 null 時會先擲 NullReferenceException。串接端請改用正式 JSON 解析器並先做 null 檢查（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:179-238`）。
- 範例：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:179-238`；事件註冊見同檔 `:451`（另一個初始化按鈕 MC_Initialize_Click 的重複訂閱區塊於 `:1692`）

### OnOpenInterestGWStatus
- 用途：國內期貨未平倉的查詢狀態。**V2.13.58／V2.13.59 起 GetOpenInterestGW、GetOpenInterest、GetOpenInterestWithFormat 三支查詢皆會觸發**（V2.13.57 手冊只寫 GetOpenInterestGW；官方兩版 changelog 均未列此項，無從判定落在哪一版），見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1973`、`api_spec/_raw/v2.13.59/7.下單-國內期選.md:704`。
- 簽名：`void OnOpenInterestGWStatus(int nQueryStatus, string bstrErrorMsg)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nQueryStatus | int | 0:查詢成功 1:查詢失敗 |
| bstrErrorMsg | string | 成功為空；失敗為錯誤訊息 |

- 回傳：無。
- 備註（V2.13.59 修正）：觸發來源已由「僅 GetOpenInterestGW」擴大為三支查詢，因此**不可再用「收到本事件即代表是 GW 查詢」來推斷查詢來源**；同時發出多種未平倉查詢時，請自行以 request 序列或旗標對應。與同族的 OnFutureRightsStatus／OnOverseaFutureRightsStatus 不同，本事件成功時 bstrErrorMsg 為空字串（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1977`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:319`

### OnFutureRights
- 用途：國內期貨權益數資料（GetFutureRights 觸發）。
- 簽名：`void OnFutureRights(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔 41 欄：0帳戶餘額 1浮動損益 2已實現費用 3交易稅 4預扣權利金 5權利金收付 6權益數 7超額保證金 8存提款 9買方市值 10賣方市值 11期貨平倉損益 12盤中未實現 13原始保證金 14維持保證金 15部位原始保證金 16部位維持保證金 17委託保證金 18超額最佳保證金 19權利總值 20預扣費用 21原始保證金 22昨日餘額 23選擇權組合單加不加收保證金 24維持率 25幣別 26足額原始保證金 27足額維持保證金 28足額可用 29抵繳金額 30有價可用 31可用餘額 32足額現金可用 33有價價值 34風險指標 35選擇權到期差異 36選擇權到期差損 37期貨到期損益 38加收保證金 39 LOGIN_ID 40 ACCOUNT_NO |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」；全幣別（含基幣）時第一筆為基幣。主說明文件中又名「SKOrderLib_OnFutureRights」。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:199`

### OnFutureRightsStatus
- 用途：國內權益數查詢的狀態（GetFutureRights 觸發）。V2.13.58／V2.13.59 新增——V2.13.57 手冊與 Interop 皆無此事件，但官方兩版 changelog 皆未列此項（屬「手冊本文有、版本歷程無」的新增），**無從判定落在 V2.13.58 或 V2.13.59**。
- 簽名：`void OnFutureRightsStatus(int nQueryStatus, string bstrErrorMsg)`（COM 宣告：`void OnFutureRightsStatus([in] LONG nQueryStatus, [in] BSTR bstrErrorMsg)`，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1991`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nQueryStatus | int | 0:查詢成功 1:查詢失敗 |
| bstrErrorMsg | string | **成功為字串 `"Success!"`**、失敗為錯誤訊息 |

- 回傳：無（void）。
- 備註：本事件替原本只回資料的 OnFutureRights 補上成敗判斷管道。**陷阱**：成功時 bstrErrorMsg 是 `"Success!"` 而非空字串，與同族的 OnOpenInterestGWStatus／OnOverseaFutureOpenInterestGWStatus（成功為空）不同，共用解析器會誤判成失敗（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1993`、`api_spec/_raw/v2.13.59/7.下單-國內期選.md:716`）。
- 範例：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:390-393`；事件註冊見同檔 `:449`（另一個初始化按鈕 MC_Initialize_Click 的重複訂閱區塊於 `:1690`）

### OnStopLossReport
- 用途：期貨智慧單（STP/MST/MIT/OCO/AB）被動回報（GetStopLossReport 觸發）。
- 簽名：`void OnStopLossReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 第一筆「M000,總數」表示成功與筆數；第二筆起為內容，以「,」分隔，欄位依單型（STP/MST/MIT/OCO/AB）而異 |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」。V2.13.38 起為新版格式，與舊版欄位相異。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 列「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名是哪一種智慧單、也未指出缺漏逗號的欄位位置，手冊本文並無新舊欄位比較表可對照。既有解析器若以固定索引取欄位，升版後可能整體位移；建議改為「先檢查欄位數（長度）再依欄位名對映」，並在模擬環境比對實際欄位數後才上線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:184`

### OnOverseaFuture
- 用途：海外期貨下單商品資料（GetOverseaFutures 觸發）。
- 簽名：`void OnOverseaFuture(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「;」分隔：交易所代碼;交易所名稱;商品代碼;商品名稱;年月;跳動點;分母;可接受交易種類;可當沖;委託時效(ROD;FOK;IOC)。2.13.54 起另提供「商品種類」欄位（6:EQ 7:SP 8:FX，判斷價差買賣別基準） |

- 回傳：無。
- 備註：2020/11 新增委託時效欄位。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:189`

### OnOverseaOption
- 用途：海外選擇權下單商品資料（GetOverseaOptions 觸發）。
- 簽名：`void OnOverseaOption(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 以「,」分隔：下單交易所代碼,交易所名稱,下單商品代碼,商品名稱,商品年月,價格跳動點,履約價最小跳動點,基準履約價,最低履約價,最高履約價,履約價除數,分母,可委託類型,當沖減收保證金,標的年月 |

- 回傳：無。
- 備註：價格跳動點為區間表示法（`區間上限|跳動點|是否分數顯示`，以「/」分隔多筆）；最終履約價需除以履約價除數，詳見 `api_spec/_raw/9.下單-海外期選.md:468`。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:194`

### OnOFOpenInterestGWReport
- 用途：海外期貨未平倉 GW 資料（GetOverseaFutureOpenInterestGW 觸發；彙總或明細格式）。
- 簽名：`void OnOFOpenInterestGWReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 第一筆「@@,訊息代碼,訊息內容」為查詢狀態（0=成功）；第二筆起為未平倉資料，以「,」分隔；海期/海選欄位（帳號、交易所、商品代號含年月、買賣別、未平口數、平均價、現價、未平損益、委託買賣口數、昨日結算價、當沖未平口數、原始保證金、是否選擇權、[海選]履約價/CP）詳見 `api_spec/_raw/9.下單-海外期選.md:474-490` |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」；分子分母價格以空白間隔（EX: `379 02/4`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:236`

### OnOverseaFutureOpenInterest
- 用途：海外期貨未平倉資料（GetOverseaFutureOpenInterest 觸發，舊版）。
- 簽名：`void OnOverseaFutureOpenInterest(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 第一筆「@@,訊息代碼,訊息內容」；後續以「,」分隔：交易所代碼,交易所中文名稱,帳號,商品代碼＋年月,商品中文名稱,買賣別(B/S),數量,市價,平均成交價,昨日結算價,損益 |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」。
- 備註（V2.13.58 修正／查無資料格式）：V2.13.59 手冊新增「若查無資料，則回傳 001,查無資料,帳號」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1732`，changelog 見同檔 `:37`；V2.13.57 手冊此處原本沒有這句）。解析器應以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄與 3 欄兩種官方不一致格式。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:179`

### OnOverseaFutureOpenInterestGWStatus
- 用途：海外期貨未平倉 GW 查詢狀態（GetOverseaFutureOpenInterestGW 觸發）。
- 簽名：`void OnOverseaFutureOpenInterestGWStatus(int nQueryStatus, string bstrErrorMsg)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nQueryStatus | int | 0:查詢成功 1:查詢失敗 |
| bstrErrorMsg | string | 成功為空；失敗為錯誤訊息 |

- 回傳：無。
- 備註（V2.13.59 修正／官方矛盾）：分冊 9 已把觸發來源寫成「GetOverseaFutureOpenInterestGW 或 GetOverseaFutureOpenInterest」（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:810`），主手冊 4-2-u 卻仍只寫 GetOverseaFutureOpenInterestGW（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1981`）；兩者矛盾，非-GW 版是否觸發需實測。無論結果為何，**勿以本事件推斷查詢來源**。與 OnOverseaFutureRightsStatus 不同，本事件成功時 bstrErrorMsg 為空字串（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1985`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:324`

### OnOverSeaFutureRight
- 用途：海外期貨權益數資料（GetRequestOverSeaFutureRight 觸發）。
- 簽名：`void OnOverSeaFutureRight(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 第一筆為查詢結果（成功以「,」分隔「000」與資料筆數；失敗以「;」分隔錯誤代碼與訊息）；第二筆起為權益數內容，以「,」分隔 |

- 回傳：無。
- 備註：資料傳送完畢回一筆「##」。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:213`

### OnOverseaFutureRightsStatus
- 用途：海外權益數的查詢狀態（GetRequestOverSeaFutureRight 觸發）。V2.13.58／V2.13.59 新增——V2.13.57 手冊與 Interop 皆無此事件，官方兩版 changelog 皆未列此項，**無從判定落在 V2.13.58 或 V2.13.59**。
- 簽名：`void OnOverseaFutureRightsStatus(int nQueryStatus, string bstrErrorMsg)`（COM 宣告：`void OnOverseaFutureRightsStatus([in] LONG nQueryStatus, [in] BSTR bstrErrorMsg)`，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1999`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nQueryStatus | int | 0:查詢成功 1:查詢失敗 |
| bstrErrorMsg | string | **成功為字串 `"Success!"`**、失敗為錯誤訊息 |

- 回傳：無（void）。
- 備註：本事件替原本只回資料的 OnOverSeaFutureRight 補上成敗判斷管道。**陷阱一**：成功時 bstrErrorMsg 是 `"Success!"` 而非空字串，與 OnOverseaFutureOpenInterestGWStatus（成功為空）不同（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2001`、`api_spec/_raw/v2.13.59/9.下單-海外期選.md:822`）。**陷阱二（大小寫）**：新的狀態事件是 `OnOverseaFutureRightsStatus`（Oversea），既有的資料事件卻是 `OnOverSeaFutureRight`（OverSea），兩者拼法不同，訂閱時勿混用。
- 範例：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:395-398`；事件註冊見同檔 `:450`（另一個初始化按鈕 MC_Initialize_Click 的重複訂閱區塊於 `:1691`）

### OnOFSmartStrategyReport
- 用途：海期智慧單被動查詢結果（GetOFSmartStrategyReport 觸發；OCO/AB 兩種格式）。
- 簽名：`void OnOFSmartStrategyReport(string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string | 第一筆「M000,總數」；第二筆起以「,」分隔，欄位依 OCO/AB 而異 |

- 回傳：無。
- 備註：全部回傳完畢回一筆「##」。
- 備註（V2.13.59 修正）：官方 V2.13.59 changelog 列「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名是哪一種智慧單、也未指出缺漏逗號的欄位位置，手冊本文並無新舊欄位比較表可對照。既有解析器若以固定索引取欄位，升版後可能整體位移；建議改為「先檢查欄位數（長度）再依欄位名對映」，並在模擬環境比對實際欄位數後才上線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKOrder.cs:314`

## 未公開介面（勿用）

V2.13.59 的 `Interop.SKCOMLib` 出現下列 11 個 SKOrderLib 相關符號，V2.13.57 完全沒有；但官方手冊（`api_spec/_raw/v2.13.59/` 的 19 份 V2.13.59 原文與 `api_spec/_raw/` 的 21 份 V2.13.57 原文）與 V2.13.58／V2.13.59 兩版 changelog 均未記載，兩棵官方 C# 範例樹（`Source_code/CapitalAPI_2.13.57_CExample/`、`Source_code/CapitalAPI_2.13.59_CExample/`）也 0 命中。**用途與簽名未知，勿在正式程式使用。**

| 類別 | 符號 |
|---|---|
| ISKOrderLib 方法 | GetOFOpenInterestWithDetails |
| 事件（國內期選） | OnOpenInterestWithDetails、_ISKOrderLibEvents_OnOpenInterestWithDetailsEventHandler、add_OnOpenInterestWithDetails、remove_OnOpenInterestWithDetails、m_OnOpenInterestWithDetailsDelegate |
| 事件（海外期選） | OnOverseaFutureOpenInterestWithDetails、_ISKOrderLibEvents_OnOverseaFutureOpenInterestWithDetailsEventHandler、add_OnOverseaFutureOpenInterestWithDetails、remove_OnOverseaFutureOpenInterestWithDetails、m_OnOverseaFutureOpenInterestWithDetailsDelegate |

- 既有的 GetOpenInterest／GetOpenInterestGW／GetOpenInterestWithFormat／GetOverseaFutureOpenInterest(GW) 與 OnOpenInterest／OnOverseaFutureOpenInterest／*GWStatus 全數保留，故此組屬純新增，未取代任何現有介面。
- 無法從現有資料判定參數簽名、回傳欄位格式，也無法判定與 V2.13.58 新增的 OnOpenInterestJson 之間的分工；要取得簽章只能對 `Interop.SKCOMLib.dll` 做 ildasm／OleView 反查。
- 未來官方補文件前，請一律使用已記載的未平倉查詢函式與事件。

## 陷阱與注意

1. **前置順序不可省**：SKCenterLib 登入 → SKOrderLib_Initialize（否則 1000）→ ReadCertByID（否則 1011 SK_ERROR_ORDER_SIGN_INVALID）→ GetUserAccount；海外另需 LoadOSCommodity/LoadOOCommodity（否則 1035/2008），Proxy 需 InitialProxyByID＋等 OnProxyStatus=5001。
2. **聲明書/風險預告書**：未簽 API 下單聲明書取不到帳號（157）；證券智慧單需簽證券智慧單風險預告書（2009）、期貨智慧單需簽期貨智慧單風險預告書（2010）。
3. **回傳 0 ≠ 成交**：多數下單/刪改函式回 0 只代表「委託伺服器接收成功」，實際狀態以回報（SKReplyLib OnNewData）或被動查詢為準；SGX DMA 即使價格有誤仍可能拿到 15 碼序號。 **V2.13.58 起官方把這件事寫死進文件**：changelog「四、文件調整 1」載明「同步、非同步委託收到回傳值為 0 時，表示成功送至交易所，交易結果請由回報確認」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37`），主手冊並在 81 支下單／刪改單／Proxy 函式的「回傳值」欄改寫語意（4-2 節 67 支、4-7 Proxy 節 14 支）、另在 27 處備註加註「*此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認」（例：同檔 `:466`、`:3101`）。介面與符號完全沒變，但「回傳 0 = 委託成功」已被正式降級為「已受理／已送達交易所」，任何以回傳 0 當成交易成立的邏輯都必須改以回報為準。
4. **同步/非同步**：bAsyncOrder=true 時 bstrMessage 只回 Thread ID，結果經 OnAsyncOrder（OLID 系列經 OnAsyncOrderOLID）以 nThreadID 對應下單來源。
5. **GetOrderReport/GetFulfillReport 為阻塞式**：請用執行緒呼叫、每次查詢間隔 5 秒、回報不含盤中零股、中文 UTF-8。GetAvgCost 一分鐘限 10 次（1127）。
6. **盤中零股限制**：CorrectPriceBySeqNo/ByBookNo 不適用盤中零股；刪盤中零股用 CancelOrderByStockNo(Advance)。CancelOrderByStockNo 的 bstrStockNo 帶空字串會刪除帳號下**所有**委託（V2.13.52 行為變更）。
7. **智慧單為獨立機制**：已自行出清/回補時務必自行取消智慧單，避免觸發後重複成交；已觸發的智慧單不可用 CancelTSStrategyOrder 取消；刪期貨智慧單未填書號會影響解除保證金風控（改用 V1 並填書號）。MIT 必填觸發價/成交價/觸發方向（1054/1056/1089），且不可委託價差商品（1050）。
8. **CancelTFStrategyOrderV1 參數順序特殊**：pCancelOrder 在第一個參數（其他刪單函式皆 bstrLogInID 在前）。
9. **海期價差買賣別**：SendOverseaFutureSpreadOrder(Proxy) V2.13.54 停用；改用 *2 版本後買賣別以「商品種類」為基準（6:EQ、8:FX 以遠月為主；7:SP 以近月為主），帶 0:買時近月為主商品＝買近賣遠、遠月為主商品＝買遠賣近。價差單不可帶當沖（1052）、委託價非 0 時分子不可帶負號（1049）。
10. **海外改價僅限價改限價**（1065/1071）；SGX DMA 無書號改價（1072），僅能以 15 碼序號用 OverSeaCorrectPriceBySGXAPISeqNo。海期委託 sNewClose 目前僅新倉，海選可新/平倉；海選 sTradeType 固定 ROD。
11. **價格特殊代碼**：一般期選 IOC/FOK 可用 M(市價)/P(範圍市價)；智慧單一律改用 nOrderPriceType（2 限價/3 範圍市價），**不可**用 P；證券 bstrPrice 可用 M(參考價)/H(漲停)/L(跌停)；逐筆市價單 Price 必須 0（1068）。
12. **海期/海選價格分子分母**：委託價、觸發價有 Numerator/Denominator 欄位（債券報價如 130'05）；OnOverseaOption 履約價需除以「履約價除數」；海選價格跳動點為區間制。
13. **限近月 vs 指定月份**：SendFutureStopLossOrder／SendMovingStopLossOrder／SendFutureMITOrder／SendFutureOCOOrder 限近月商品代碼（TX00/MTX00，違者 1107）；指定月份請用 V1 系列並填 bstrSettlementMonth（非近月未填 6 碼年月回 2030）。
14. **每秒下單限制**：SetMaxQty/SetMaxCount 超限即上鎖（1040），需 UnlockOrder 解鎖後才能繼續下單。
15. **Proxy 事件簽名不一致**：ProxyServer 專文 OnProxyOrder 宣告漏列 nStampID，實際為 3 參數（v2.13.47 補述）；OnProxyStatus 收 5004（每日斷線）需等 1 分鐘再 ProxyReconnectByID；5018 送單異常時需確認 5001 並重連。
16. **文件勘誤**：`11.下單-複委託.md` 開頭功能表「一般下單/刪單」與「Proxy下單/刪單」兩表標題互換（SendForeignStockOrder 為一般下單、SendForeignStockProxyOrder 為 Proxy），以各函式內文為準。主說明以「SKOrderLib_OnFutureRights」稱呼 OnFutureRights 事件，實際事件名無前綴；範例碼訊息字串中的「SKOrderLib_ProxyDisconnectByID/SKOrderLib_ProxyReConnectByID」亦僅為顯示文字，方法名為 ProxyDisconnectByID/ProxyReconnectByID。
17. **版本異動高風險點**：GetBalanceQuery（V2.13.54 停用）、SendStockStrategyLLS/MBA/MMIT（V2.13.48 移除）、GetRequestProfitReport/OnRequestProfitReport（即將下線）、GetRealBalanceReport 可資沖/可券沖欄位（v2.13.42~54 錯置）、GetOpenInterestWithFormat 格式二部分欄位 v2.13.53 起暫停提供。
     - **V2.13.57 → V2.13.59 新增的高風險點**：(a) 內／外期未平倉「查無資料」格式統一為 `001,查無資料,帳號`（V2.13.58；`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37`；V2.13.57 內期非格式版原為 `M003 NO DATA#`、GW 版為 `001 查無資料`，外期版原本沒有這句備註），以字串比對 `M003 NO DATA` 或以固定欄位數判斷者會失效或把查無資料誤判為一般資料列；(b) GetOpenInterestGW 新增欄位「商品－下單代碼」，且該欄查詢異常時回空值需重呼叫（V2.13.59；同檔 `:38`、`:1725`），欄位序列在官方 docx 為圖片、插入位置無從文件確認，固定索引解析者必須先實測；(c) 下單／刪改單／Proxy 回傳值語意降級為「已送至交易所」（V2.13.58；同檔 `:37`）；(d) 簡化版 SKDLLCSharp 包裝層「修正簡化版 API 下單成功回傳值」（V2.13.59；同檔 `:38`），使用包裝層下單者需重新確認成功判斷條件。
18. **interop ref/out 差異**：`[in] struct X*` 參數在 SKCOMTesterV2 的 Interop 以 `ref` 傳遞、在 SKCOMTester 直接傳值；依專案實際引用的 Interop.SKCOMLib 簽名為準。
19. **未平倉「查無資料」判斷（V2.13.58 起）**：一律以「首欄 == `001`」或「整串含『查無資料』」判斷，並同時容忍 2 欄（分冊 7，`api_spec/_raw/v2.13.59/7.下單-國內期選.md:724`）與 3 欄（主手冊，`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1725`）兩種官方不一致格式。官方分冊 7 的示範另把後台錯誤碼 `970` 一併視為非資料列（`api_spec/_raw/v2.13.59/7.下單-國內期選.md:635`）。
20. **狀態事件成功值不一致**：OnFutureRightsStatus／OnOverseaFutureRightsStatus 成功時 bstrErrorMsg 為字串 `"Success!"`（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1993`、`:2001`），OnOpenInterestGWStatus／OnOverseaFutureOpenInterestGWStatus 成功時卻為空字串（同檔 `:1977`、`:1985`）。四個事件共用同一支解析器會誤判成敗。
21. **狀態事件不可用來推斷查詢來源**：OnOpenInterestGWStatus 自 V2.13.58／V2.13.59 起由 GetOpenInterestGW、GetOpenInterest、GetOpenInterestWithFormat 三支皆觸發（changelog 未列版本）（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1973`）；海期側分冊 9 與主手冊 4-2-u 對 OnOverseaFutureOpenInterestGWStatus 的觸發來源說法矛盾（`api_spec/_raw/v2.13.59/9.下單-海外期選.md:810` vs `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1981`），需實測。請自行以 request 序列或旗標對應查詢來源。
22. **事件名大小寫易錯**：新增的 `OnOverseaFutureRightsStatus` 用 Oversea，既有資料事件 `OnOverSeaFutureRight` 用 OverSea；`GetRequestOverSeaFutureRight`（OverSea）與 `GetOverseaFutureOpenInterest`（Oversea）亦不同。訂閱／反射取用事件名時務必逐字比對。
23. **OnOpenInterestJson 官方示範不可照抄**：官方 handler 以 `IndexOf('"')` 手刻切割字串而非 JSON 解析，且把 `bstrData.Contains(...)` 排在 `string.IsNullOrWhiteSpace(bstrData)` 之前，bstrData 為 null 時會擲 NullReferenceException（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKOrder.cs:179-238`）。請改用正式 JSON 解析器並先做 null 檢查。
24. **簡化版 SKDLLCSharp 包裝層與 COM 模式不同**：V2.13.58／V2.13.59 新增（V2.13.57 包裝層無、官方 changelog 未列版本）的 GetRealBalanceReport／GetOpenInterestGW／GetFutureRights／GetOFOpenInterestGW／GetOFFutureRights 為同步回傳 `*ParserResult`（StatusCode／Message／RawData／Blocks），不是 COM 的「錯誤碼＋事件」模式；其中 `GetOFOpenInterestGW`／`GetOFFutureRights` 只存在於包裝層與原生 SKCOM.dll 的 C 匯出表（V2.13.59 新增的 5 個原生匯出：GetRealBalanceReport／GetOpenInterestGW／GetFutureRights／GetOFOpenInterestGW／GetOFFutureRights），**Interop.SKCOMLib 的 COM 介面中沒有這兩個符號**（COM 端請用 GetOverseaFutureOpenInterestGW／GetRequestOverSeaFutureRight）。官方手冊「宣告」欄一律誤寫為 `(int Code, string Message)` 之 ValueTuple（`api_spec/_raw/v2.13.59/C_Sharp策略王DLL元件使用說明.md:362-448`），實際回傳型別以隨包的 `SKDLLCSharp.dll` 中繼資料為準（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/bin/x64/Release/SKDLLCSharp.dll`）；範例碼以 `var` 承接並取用 `StatusCode`／`Message`／`Blocks`（`Source_code/CapitalAPI_2.13.59_CExample/SKDLLTester/SKDLLTester/Form1.cs:2878-3133`）。另每個方法還有一個 `int GetXxx(..., StringBuilder, int)` 多載，手冊完全未提，用途與緩衝區需求不明。
25. **未公開符號勿用**：V2.13.59 Interop 另出現 GetOFOpenInterestWithDetails 與兩個 *WithDetails 事件共 11 個符號，官方手冊與 changelog 均未記載、用途與簽名未知，見「未公開介面（勿用）」節。

