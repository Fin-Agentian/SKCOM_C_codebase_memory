# SKReplyLib — 回報（委託/成交回報事件中樞，登入前必須註冊）

SKReplyLib 負責回報主機連線與所有主動回報：公告、委託/成交回報（OnNewData）、智慧單回報（OnStrategyData）。
**關鍵前置條件：呼叫 `SKCenterLib_Login` 之前，必須先建立 SKReplyLib 物件並註冊 `OnReplyMessage` 事件（handler 內回傳 sConfirmCode = -1），否則登入失敗**（見 `_raw/3.登入.md:139`、主說明 4-3-e）。

> 版本基準 V2.13.59（以 V2.13.57 規格為底增補；差異見 [../changelog_2.13.57_to_2.13.59.md](../changelog_2.13.57_to_2.13.59.md)）。SKReplyLib 於 .57→.59 之間 COM 介面零變更（Interop 符號兩版一致、無函式／事件增刪），變動全在 DLL 內部行為與回報字串內容——四項修正加一項欄位新增，逐項見各節「V2.13.58／V2.13.59」註記與「陷阱與注意」15~17。
>
> 來源：`api_spec/_raw/12.回報.md`、`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md`（4-3 節，行 2075–2245）、官方 C# 範例碼。
> V2.13.59 增補來源：`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md`（4-3 節，行 2011–2185；版本控管表 2.13.58 見行 37、2.13.59 見行 38）、`api_spec/_raw/v2.13.59/12.回報.md`、`Source_code/CapitalAPI_2.13.59_CExample/` 範例碼。

## 總覽：功能分區表

| 分區 | 函式 / 事件 | 用途一句話 |
|---|---|---|
| 連線 | SKReplyLib_ConnectByID | 指定登入帳號建立回報主機連線 |
| 連線（舊） | SKReplyLib_CloseByID | 舊主機斷線（保留給舊客戶，新用戶改用 SolaceCloseByID） |
| 連線 | SKReplyLib_SolaceCloseByID | 中斷指定帳號的 Solace 回報連線 |
| 連線 | SKReplyLib_IsConnectedByID | 查詢指定帳號回報連線狀態 |
| 公告事件 | OnReplyMessage | 公告資訊通知；**登入前必註冊**並回傳 -1 |
| 公告事件 | OnReplyClearMessage | 公告開始清除前日資料通知 |
| 公告事件 | OnReplyMessageSpecial | 元朔訊息中心回傳（僅見於範例碼） |
| 連線事件（舊） | OnConnect | 連線結果通知（v2.13.48 起改用 OnSolaceReplyConnection） |
| 連線事件（舊） | OnDisconnect | 斷線通知（v2.13.48 起改用 OnSolaceReplyDisconnect） |
| 連線事件 | OnSolaceReplyConnection | Solace 回報連線結果通知 |
| 連線事件 | OnSolaceReplyDisconnect | Solace 回報斷線結果通知 |
| 連線事件 | OnComplete | 回報回補完成通知（未收到＝連線/資料異常） |
| 清盤事件 | OnReplyClear | 回報開始清除前日資料通知（依市場別 R1~R23） |
| 回報資料事件 | OnNewData | 委託/取消/改量/改價/成交等主動回報（新格式，含預約單） |
| 回報資料事件（舊） | OnData | 舊格式回報，即將下線；欄位同 OnNewData |
| 智慧單事件（舊） | OnSmartData | 舊版智慧單回報，已停止更新格式，改接 OnStrategyData |
| 智慧單事件 | OnStrategyData | 新版智慧單主動回報（國內證券/期選、海期） |

## 初始化與事件註冊：C# 實際寫法

物件建立與「登入前註冊公告」——抄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:19-20, 184-200`：

```csharp
using SKCOMLib; // Interop.SKCOMLib

SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
SKReplyLib m_pSKReply = new SKReplyLib();    // 回報物件

private void MainForm_Load(object sender, EventArgs e)
{
    // 註冊公告(必要) —— 必須在 SKCenterLib_Login 之前
    m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(OnAnnouncement);
    void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
    {
        nConfirmCode = -1; // 回傳確認值，未回傳 -1 將無法正確登入
        string msg = "【註冊公告OnReplyMessage】" + strUserID + "_" + bstrMessage;
        richTextBoxMessage.AppendText(msg + "\n");
    }
}
```

回報相關事件掛載（連線前註冊一次即可）——抄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:277-290`：

```csharp
m_SKReplyLib.OnSolaceReplyConnection += new _ISKReplyLibEvents_OnSolaceReplyConnectionEventHandler(this.OnSolaceReplyConnection);
m_SKReplyLib.OnSolaceReplyDisconnect += new _ISKReplyLibEvents_OnSolaceReplyDisconnectEventHandler(this.OnSolaceReplyDisconnect);
m_SKReplyLib.OnComplete           += new _ISKReplyLibEvents_OnCompleteEventHandler(this.OnComplete);
m_SKReplyLib.OnReplyClear         += new _ISKReplyLibEvents_OnReplyClearEventHandler(this.OnClear);
m_SKReplyLib.OnNewData            += new _ISKReplyLibEvents_OnNewDataEventHandler(this.OnNewData);
m_SKReplyLib.OnReplyClearMessage  += new _ISKReplyLibEvents_OnReplyClearMessageEventHandler(this.OnClearMessage);
m_SKReplyLib.OnStrategyData       += new _ISKReplyLibEvents_OnStrategyDataEventHandler(this.OnStrategyData);
// 舊事件（範例中已註解停用）：
// m_SKReplyLib.OnConnect    += new _ISKReplyLibEvents_OnConnectEventHandler(this.OnConnect);
// m_SKReplyLib.OnDisconnect += new _ISKReplyLibEvents_OnDisconnectEventHandler(this.OnDisconnect);
```

標準流程：`new SKReplyLib()` → 註冊 OnReplyMessage → `SKCenterLib_Login` → 註冊回報事件 → `SKReplyLib_ConnectByID` → 等 `OnSolaceReplyConnection`（nErrorCode==0）與 `OnComplete`（回補完成）→ 開始接收 OnNewData / OnStrategyData。

## 方法

### SKReplyLib_ConnectByID

- 用途：指定回報連線的使用者登入帳號，建立回報主機（Solace）連線並觸發回報回補。
- 簽名：`int SKReplyLib_ConnectByID(string bstrUserID)`（原始宣告 `Long SKReplyLib_ConnectByID([in] BSTR bstrUserID)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 登入帳號 |

- 回傳：LONG 錯誤碼；0 成功，非 0 失敗（見 [../error_codes.md](../error_codes.md)；訊息可用 `SKCenterLib_GetReturnCodeMessage(nCode)` 轉換）。
- 備註：需先簽署證券或期貨 API 下單聲明書方可使用。智慧單回報支援國內證券、期貨、選擇權。連線結果與回補完成分別由 `OnSolaceReplyConnection`、`OnComplete` 事件通知。
- **V2.13.59 修正：「主動回報連線多帳號，無法斷線問題」**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`）。官方版本控管表未指名是哪一支函式，本節說明與宣告在 .59 手冊 4-3-1（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2034-2040`）完全未改。同時對多個帳號呼叫 ConnectByID 的程式，升版後應實測斷線／重連行為（見 SKReplyLib_SolaceCloseByID 節與「陷阱與注意」15）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1807,1923`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:295,330`

### SKReplyLib_CloseByID

- 用途：中斷指定帳號的回報連線（2018 舊主機時代功能）。
- 簽名：`int SKReplyLib_CloseByID(string bstrUserID)`（文件僅寫「請參考 SKReplyLib_SolaceCloseByID」，未附宣告；簽名自範例碼萃取）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 登入帳號 |

- 回傳：LONG 錯誤碼；0 成功（見 [../error_codes.md](../error_codes.md)）。
- 備註：主說明 4-3-2——配合 2018 舊主機下線，舊客戶串接保留使用；**新用戶請直接使用 SKReplyLib_SolaceCloseByID**。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:303,338`

### SKReplyLib_IsConnectedByID

- 用途：檢查輸入帳號目前的回報連線狀態。
- 簽名：`int SKReplyLib_IsConnectedByID(string bstrUserID)`（原始宣告 `Long SKReplyLib_IsConnectedByID([in] BSTR bstrUserID)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 登入帳號 |

- 回傳：正式環境：**0＝斷線、1＝連線中、2＝下載中（回補中）**；其他值為錯誤碼（見 [../error_codes.md](../error_codes.md)）。注意：回傳值不是單純成功/失敗碼。
- 備註：主說明 4-3-3 要求同時接收通知事件 `OnSolaceReplyConnection`（12.回報.md 舊文寫 OnConnect，以主說明為準）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1829`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:362,391`

### SKReplyLib_SolaceCloseByID

- 用途：中斷指定帳號的 Solace 伺服器連線。
- 簽名：`int SKReplyLib_SolaceCloseByID(string bstrUserID)`（原始宣告 `Long SKReplyLib_SolaceCloseByID([in] BSTR bstrUserID)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 登入帳號 |

- 回傳：LONG 錯誤碼；0 成功（見 [../error_codes.md](../error_codes.md)）。
- 備註：中斷單一指定 Solace 連線，**若該連線同時負責報價，報價也會一併中斷**。若需中斷所有 Solace 連線，改用 `SKQuoteLib_LeaveMonitor`（見 13.國內報價）。斷線結果由 `OnSolaceReplyDisconnect` 通知。
- **V2.13.59 修正：「主動回報連線多帳號，無法斷線問題」**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`）。手冊未指名函式，4-3-4 本節文字未改（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2055-2061`）；.59 前多帳號情境可能斷不掉、重連疊加。**升版後應實測 SolaceCloseByID 的資源釋放與重連行為**（多帳號逐一斷線是否都收到 `OnSolaceReplyDisconnect` 3002、重連後是否重複掛號）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1790,1852`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:346,354`

## 事件

### OnReplyMessage

- 用途：有公告時主動呼叫，通知公告類訊息。**登入前必須註冊此事件，方可正確登入**（不需先做回報連線）。
- 簽名：`void OnReplyMessage(string bstrUserID, string bstrMessage, out short sConfirmCode)`（原始宣告 `void OnReplyMessage([in] BSTR bstrUserID, [in] BSTR bstrMessage, [out] SHORT* sConfirmCode)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| bstrMessage | string | 逗號分隔：MsgNo（訊息編號）、StartTime（訊息開始日期時間）、EndTime（訊息結束時間）、Message（訊息內容） |
| sConfirmCode | out short | 回傳確認值，**handler 內必須設為 -1**（VARIANT_TRUE） |

- 回傳：無（handler 需設定 sConfirmCode）。
- 備註：對應主說明 4-3-e。雙因子登入（SKCenterLib_Login / LoginSetQuote）前的必要註冊。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:191`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:52,256`、`Source_code/CapitalAPI_2.13.57_CExample/ExcelSample/Program.cs:444`

### OnReplyClearMessage

- 用途：公告開始清除前日資料時發出的通知。
- 簽名：`void OnReplyClearMessage(string bstrUserID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |

- 回傳：無。
- 備註：對應主說明 4-3-j。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1599`

### OnConnect（舊）

- 用途：回報連線成功或失敗時通知連線結果。
- 簽名：`void OnConnect(string bstrUserID, int nErrorCode)`（原始宣告 `void OnConnect([in] BSTR bstrUserID, [in] LONG nErrorCode)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| nErrorCode | int (LONG) | （Socket）Error Code |

- 回傳：無。
- 備註：**v2.13.48 起請改用 OnSolaceReplyConnection**（主說明 4-3-a）。收到此事件時底層尚未完成處理，勿在事件內立即重連/斷線，請用 Timer 延遲數秒再操作。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:127`（handler 定義；註冊處已註解停用，見 SKReply.cs:277）

### OnDisconnect（舊）

- 用途：連線中斷時通知結果。
- 簽名：`void OnDisconnect(string bstrUserID, int nErrorCode)`（原始宣告 `void OnDisconnect([in] BSTR bstrUserID, [in] LONG nErrorCode)`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| nErrorCode | int (LONG) | （Socket）Error Code |

- 回傳：無。
- 備註：**v2.13.48 起請改用 OnSolaceReplyDisconnect**（主說明 4-3-b）。同樣勿在事件內立即重連/斷線。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:140`（handler 定義；註冊處已註解停用）

### OnSolaceReplyConnection

- 用途：Solace 回報連線時透過此事件告知結果。
- 簽名：`void OnSolaceReplyConnection(string bstrUserID, int nErrorCode)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| nErrorCode | int (LONG) | Error Code；0＝連線成功，非 0＝連線失敗 |

- 回傳：無。
- 備註：對應主說明 4-3-i。收到此事件時底層尚未完成處理，勿在事件內立即重連/斷線，請啟用 Timer 等待數秒後再操作。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1639`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:279`

### OnSolaceReplyDisconnect

- 用途：中斷 Solace 連線時透過此事件告知斷線結果。
- 簽名：`void OnSolaceReplyDisconnect(string bstrUserID, int nErrorCode)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| nErrorCode | int (LONG) | 3002＝斷線成功；3033＝連線異常（SK_SUBJECT_SOLACE_SESSION_EVENT_ERROR，主機端主動斷線）；其他＝未預期的斷線 |

- 回傳：無。
- 備註：對應主說明 4-3-h。勿在事件內立即重連/斷線；官方範例收到 3033 時啟用 Timer 每 5 秒重連一次（ReplyForm.cs:1625 `timerSolaceReconnect`）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1612`

### OnComplete

- 用途：回報連線後會進行回報回補，收到此事件表示回補完成。
- 簽名：`void OnComplete(string bstrUserID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |

- 回傳：無。
- 備註：對應主說明 4-3-c。**若未收到此通知，代表新建立的回報連線及回傳回報資料異常**——應視為連線失敗處理。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1566`
- **.59 範例注意（雙帳號回報請以 .57 版為參考）**：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:153-158` 的 OnComplete 已移除 .57 版依 `m_strLoginID`／`m_strLoginID2` 分流的兩段 if/else（.57 原樣見 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:153-172`），改為一律寫入 `listNewMessage` 並點亮 `lblSignalReplySolace`。第二組物件的事件仍有掛載（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:296`），因此**第二組帳號的回補完成訊息會併入第一組清單，且會誤點亮第一組的 Solace 燈號**；同檔 OnSmartData／OnStrategyData 的分流仍在（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:165-187`），屬範例內部不一致。COM 事件本身未變（簽名相同），只影響照抄此範例的雙帳號程式。

### OnReplyClear

- 用途：回報開始清除前日資料時發出的通知（清盤）。
- 簽名：`void OnReplyClear(string bstrMarket)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrMarket | string | 市場別：R1 證券／R2 國內期選／R3 海外股市／R4 海外期選／R11 盤中零股／R20~R23 智慧單 |

- 回傳：無。
- 備註：對應主說明 4-3-f。注意參數是市場別代碼而非登入 ID。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1579`

### OnNewData

- 用途：有回報（委託/取消/改量/改價/成交/動態退單）時主動呼叫，通知委託狀態。新格式，包含預約單回報。
- 簽名：`void OnNewData(string bstrUserID, string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| bstrData | string | 每一筆資料以「,」分隔（欄位序見下表）；`values[0]=="980"` 表後台問題訊息，非標準格式。**V2.13.59 修正：複委託（MarketType＝OS）之委託時效欄位在來源未提供時改回傳空字串給前端**（官方原文「修正當複委託回報沒給時效欄位時，需給前端空值」，`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`；手冊未指名對應欄位 index）——欄位仍在、只是內容為空，解析須容忍空值，不可假設一定有 ROD／IOC／FOK |

- bstrData 欄位序（依官方範例 handler 變數名與 dataGridViewNoClass 欄位標籤，`ReplyForm.cs:1425` 起；index 為 `Split(',')` 後的 0-based 位置）：

| # | 欄位名 | 說明 |
|---|---|---|
| 0 | KeyNo | 原始 13 碼委託序號 |
| 1 | MarketType | 市場：TS 證券／TA 盤後／TL 零股／TP 興櫃／TC 盤中零股／TF 期貨／TO 選擇權／OF 海期／OO 海選／OS 複委託 |
| 2 | Type | 種類：N 委託／C 取消／U 改量／P 改價／D 成交／B 改價改量／S 動態退單 |
| 3 | OrderErr | Y 失敗／T 逾時／N 正常 |
| 4 | Broker | TS,TA,TL,TP：分公司代號 unit no；TF,TO：IB 代號 broker id |
| 5 | CustNo | 交易帳號 |
| 6 | BuySell | 買賣別 |
| 7 | ExchangeID | 交易所 |
| 8 | ComId | 商品代碼 |
| 9 | StrikePrice | 履約價（V2.13.45 起為保留欄位，請改看 StrikePrice1/StrikePrice2） |
| 10 | OrderNo | 委託書號 |
| 11 | Price | 價格（N 委託＝委託價；D 成交＝成交價） |
| 12 | Numerator | 海期（分子） |
| 13 | Denominator | 海期（分母） |
| 14 | Price1 | 海期（觸發價）/ 第一腳成交價 |
| 15 | Numerator1 | 海期（第一腳觸發價分子） |
| 16 | Denominator1 | 海期（第一腳觸發價分母） |
| 17 | Price2 | 期選（第二腳成交價） |
| 18 | Numerator2 | 第二腳觸發價分子 |
| 19 | Denominator2 | 第二腳觸發價分母 |
| 20 | Qty | 股數/口數 |
| 21 | BeforeQty | 異動前量（僅證券、複委託市場提供） |
| 22 | AfterQty | 異動後量（僅證券、複委託市場提供） |
| 23 | Date | 交易日 |
| 24 | Time | 交易時間 |
| 25 | OkSeq | 成交序號（請以 ExecutionNo 為主） |
| 26 | SubID | 子帳帳號 |
| 27 | SaleNo | 營業員編號 |
| 28 | Agent | 委託介面 |
| 29 | TradeDate | 委託日期 |
| 30 | MsgNo | 回報流水號 |
| 31 | PreOrder | A：盤中單／B：預約單 |
| 32 | ComId1 | 第一腳商品代碼 |
| 33 | YearMonth1 | 第一腳商品結算年月 |
| 34 | StrikePrice1 | 第一腳商品履約價 |
| 35 | ComId2 | 第二腳商品代碼 |
| 36 | YearMonth2 | 第二腳商品結算年月 |
| 37 | StrikePrice2 | 第二腳商品履約價 |
| 38 | ExecutionNo | 成交序號（ExecutionNo） |
| 39 | PriceSymbol | 下單期標 |
| 40 | Reserved | 盤別 A：T 盤／B：T+1 盤 |
| 41 | OrderEffective | 有效委託日 |
| 42 | CallPut | 選擇權類型 C：Call／P：Put |
| 43 | OrderSeq | 上手單號 |
| 44 | ErrorMsg | 委託單錯誤訊息（海期錯誤單內容若含「,」會以空白替換） |
| 45 | CancelOrderMarkByExchange | 交易所動態退單代碼（E：交易所動態退單）；2020/3/23 證券逐筆交易起提供 |
| 46 | ExchangeTandemMsg | 交易所或後台退單訊息（[00] 2 碼＝交易所回應；[000] 3 碼＝交易後台；[D]＝委託成功後交易所主動退單及原因） |
| 47 | SeqNo | 13 碼序號（成交單含 IOC/FOK 產生取消單之比對用，V2.13.38 新增） |
| 48 | OFSTPFlag | [海期][停損限價/停損市價][已觸發][委託回報] 海期停損單觸發註記：Y（V2.13.40 新增；官方範例僅解析前 48 欄） |
| ? | （下單時間） | **V2.13.59 新增：國內期選（適用市場 TF 期貨／TO 選擇權）新增欄位「下單時間 HH:mm:ss.fff」，index 未定**——詳下方註記 |

- **V2.13.59 新增欄位「下單時間 HH:mm:ss.fff」，但欄位位置未公開**：官方僅在版本控管表載明一行「國內期選主動回報 OnNewData 新增欄位『下單時間HH:mm:ss.fff』」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），適用市場為 **TF 期貨／TO 選擇權**。主手冊 4-3-g 的參數與備註（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2108-2120`）、分冊 `api_spec/_raw/v2.13.59/12.回報.md:210-222` 的欄位說明與「修改比較表」皆未同步更新（比較表仍停在 V2.13.45 的履約價那一列；該分冊 .57→.59 全檔差異只有標題來源路徑一行），官方範例 `Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1431,1547-1548` 也仍是 `string[] values = new string[48]`、只解析到 `values[47]`（`OFSTPFlag = values[48]` 仍被註解）。**新欄位插在第幾欄官方未載，欄位 index 未定、待實機驗證**；以固定索引切逗號欄位的解析器可能錯位（防禦寫法見「陷阱與注意」9）。
- 回傳：無。
- 備註：「動態退單」——被動態退單的委託會收到委託回報、取消回報與動態退單回報，若有成交部位還會有成交回報。買進：可能成交價 > 即時價格區間上限 → 退單；賣出：可能成交價 < 區間下限 → 退單。區間上限＝退單價＋退單點數、下限＝退單價－退單點數（退單點數每日盤前計算、盤中固定）。SGX DMA 變體見主說明 4-3-g-2：宣告相同，可用「交易所單號」於一般線路比對回報；改走一般線路時 SGX DMA 專線委託回報僅含委託成功、不含因價格等因素之委託失敗。
- **V2.13.58 修正：「SGX 專線主動回報缺漏問題」**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37`）。影響 4-3-g-2（SGX DMA）OnNewData 的回報完整性；該節文字本身未改（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2122-2129`）。走 SGX DMA 專線者若曾自行補漏（例如以一般線路回報回填），升版後應重新核對是否重複。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1425`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:285,321`
- **.59 範例注意（雙帳號回報請以 .57 版為參考）**：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:160-164` 的 OnNewData 已移除 .57 版依 `m_strLoginID`／`m_strLoginID2` 分流的兩段 if/else（.57 原樣見 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:174-184`），改為一律寫入 `listNewMessage`；第二組物件仍有掛載此事件（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:301`），故第二組帳號的回報會併入第一組清單。與 OnComplete 同一處回歸。

### OnData（舊，即將下線）

- 用途：舊格式回報資料事件。
- 簽名：`void OnData(string bstrUserID, string bstrData)`（文件未附宣告，依「格式與 OnNewData 相同」推定；簽名待確認）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID（文件未載，依 OnNewData 推定） |
| bstrData | string | 欄位說明以 OnNewData 為主，格式相同 |

- 回傳：無。
- 備註：主說明 4-3-d 僅注記——舊用戶請以 4-3-g OnNewData 各欄位說明為主（格式相同）；新用戶請直接使用 OnNewData。標示「即將下線」。
- 範例：範例未見。

### OnSmartData（舊，已淘汰）

- 用途：舊版智慧單回報（國內期貨/選擇權 STP 停損單、MST 移動停損單、OCO 二擇一單、MIT）。
- 簽名：`void OnSmartData(string bstrUserID, string bstrData)`（自範例 handler 萃取）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| bstrData | string | 逗號分隔智慧單回報資料（舊格式，文件未再載明欄位） |

- 回傳：無。
- 備註：主說明 4-3-k——V2.13.32 起不再提供證券智慧單格式更新；**V2.13.38 起移除期貨及選擇權智慧單（STP、MIT、MST、OCO）回報格式**。請改接 OnStrategyData。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:185`（handler 定義存在但未註冊掛載）

### OnStrategyData

- 用途：新版智慧單主動回報。證券：MST、MIOC、MIT、當沖（DayTrade）、出清（ClearOut）、OCO、AB、CB；期貨：STP、MIT、MST、OCO、AB；海期：OCO、AB。
- 簽名：`void OnStrategyData(string bstrUserID, string bstrData)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string | 登入 ID |
| bstrData | string | 逗號分隔；`values[0]`＝市場別（TS 證券／TF 期選／OF 海期；"980" 表後台問題訊息）；先接該市場共用欄位，再接各單別專屬欄位 |

- bstrData 結構（依官方範例 handler `ReplyForm.cs:1655` 與 grid 標籤）：
  - `values[0]`＝市場別、`values[5]`＝單別 TradeKind。範例中的 TradeKind 代碼：證券——0 None／9 MST／29 MIOC／8 MIT／11 DayTrading／17 ClearOut／3 OCO／10 AB／27 CB；期貨——0 None／5 STP／8 MIT／9 MST／3 OCO／10 AB；海期——0 None／3 OCO／10 AB（`ReplyForm.cs:1671-1761`）。
  - 證券共用欄位（依序）：1:新增 2:刪除｜0:一般 1:零股 2:盤後｜智慧單(母單)序號｜委託單順序｜分公司代碼｜交易帳號｜子帳帳號｜交易所名稱｜13碼序號｜原始13碼序號｜委託書號｜商品代碼｜B:買 S:賣｜委託種類別(0現股/3自)融資/4自)融券/8無券普賣)｜委託價格別(0前日收盤/1漲停/2跌停/7使用者輸入)｜委託價格｜委託價類別(1市價/2限價/3範圍市價)｜委託時效(0 ROD/3 IOC/4 FOK)｜張數/口數｜觸發價｜觸發時間｜觸發價方向(0 None/1 GTE/2 LTE)｜是否當沖｜下單時間｜營業員代碼｜USER PC IP｜來源別｜智慧單狀態 Status（32 中台收單成功/33 收單失敗/34 洗價中/35 洗價中-觸發價更新/36 洗價失敗/37 洗價觸發/38 觸發下單/39 下單失敗/40 使用者刪單/999 萬用狀態→看 UniversalMsg）｜錯誤訊息註記(Y 失敗/N 成功)｜訊息｜更新時間｜萬用訊息 UniversalMsg。
  - 期貨共用欄位大致同上但無「一般/零股/盤後」欄，且尾端多國內期選專屬欄位：倉位(0 新倉/1 平倉/2 自動)、契約年月、履約價、是否價差商品、C/P、是否選擇權、是否預約單、預約單序號、交易日、原始下單商品市場、下單交易所、第一/第二腳後台商品代碼、第二腳契約年月、第二腳履約價、萬用訊息。
  - 各單別專屬欄位（接續共用欄位；完整欄位表見 `_raw/12.回報.md:249-530`）：證券 MST（MovePoint、BasePrice、MarketPrice、OrgTriggerPrice、IsStartPrice、StartPrice、StartPriceDirection ＋長效單欄位 LongActionFlag/LongActionKey/LongEndDate/TriggerStop/LAType/LAQty/LADeal）；MIOC（TouchUp、TouchDown、DealQty、LimitQty、SumQty、StartTime）；MIT（BasePrice、MarketDealTrigger、PreRiskFlag、SplitFlag ＋長效單欄位）；DayTrade（IsMIT、BasePrice、TradeKind_ClearOut、停利/停損 GTE/LTE 各組欄位、時間出清與盤後定價欄位）；ClearOut（TradeKind_ClearOut、條件一/二欄位、時間出清、觸發記號、SumQty、DealQty、DealPrice_In）；OCO（TouchPriceUp、TouchPriceDown、OrderPrice2、OrderPriceType2、OrderCond2、BuySell2、Order_Type2、OrderPrice_Mark2）；AB（MarketDealTrigger）；CB（IsAND、各條件 Is*/值/方向/觸發記號/Market* 系列欄位）。期貨 STP（長效單欄位）；MST/MIT/OCO/AB 類同證券版（OCO 多 OrderOffset2 第二腳倉位）。海期 OCO（觸發價與委託價各含分子/分母欄位 TouchPriceUp_M/_D 等、OrderOffset2 0 新單/1 平倉/2 自動 ＋長效單欄位）；AB（MarketDealTrigger）。
- 回傳：無。
- 備註：對應主說明 4-3-m。V2.13.45 起提供證券 MIT/當沖/出清/OCO/AB/CB（V2.13.48 移除 MMIT、MBA、LLS）與期貨 MIT/STP/MST/OCO/AB 新格式；V2.13.40 起共用欄位新增萬用訊息、市場別欄位刪除 OF 海期字樣。長效單相關欄位為 V2.13.45 新增。
- **V2.13.59 修正：「智慧單被動回報缺少逗號問題」**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`）。官方未指名是哪一支事件；字面上的「被動回報」指 SKOrderLib 的 `OnStopLossReport`／`OnTSSmartStrategyReport`／`OnOFSmartStrategyReport`，**OnStrategyData（主動回報）不在其列**。但同類問題曾發生在本事件——V2.13.47「修正證券追漲停市價單主動回報(OnStrategyData)欄位揭示問題（PreQtyDirection 與 IsAskQty 間缺少逗號間隔）」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:26`）。4-3-m 本節欄位表在 .59 未有異動。**若曾為舊版缺逗號寫過 workaround（例如硬把兩欄併一欄），升版後務必重新核對分隔數**。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1655`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:290,324`

## 僅見於範例碼

### OnReplyMessageSpecial

- 用途：元朔訊息中心回傳（範例碼註解標記 [-20240321-Add]）；接收特殊公告訊息。文件（12.回報.md 與主說明 4-3 節）均未記載。
- 推測簽名：`void OnReplyMessageSpecial(string bstrMessage)`（自 handler `OnAnnouncementSpecial(string bstrMessage)` 萃取；event handler 型別 `_ISKReplyLibEvents_OnReplyMessageSpecialEventHandler`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrMessage | string | 訊息內容（文件未載） |

- 回傳：無。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:53`（註冊）、`Form1.cs:481`（handler）

## 陷阱與注意

1. **登入順序硬性要求**：`new SKReplyLib()` ＋註冊 `OnReplyMessage`（handler 內 `sConfirmCode = -1`）必須在 `SKCenterLib_Login` 之前完成，否則登入失敗（錯誤 2017 相關）；此步驟不需先做回報連線。
2. **連線成功的判定要靠兩個事件**：`OnSolaceReplyConnection`（nErrorCode==0）只代表連上，**必須再等 `OnComplete`** 才代表回報回補完成；未收到 OnComplete 即為連線/資料異常。
3. **勿在連線/斷線事件內立即重連或斷線**：OnConnect/OnDisconnect/OnSolaceReplyConnection/OnSolaceReplyDisconnect 觸發時底層尚未完成處理，官方要求啟用 Timer 等待數秒後再重連（官方範例對 3033 用 5 秒 Timer）。
4. **OnSolaceReplyDisconnect 的 nErrorCode 語意特殊**：3002＝「斷線成功」（正常）、3033＝連線異常（主機端主動斷線，應重連），不要把 3002 當錯誤。
5. **SKReplyLib_IsConnectedByID 回傳值不是錯誤碼**：0 斷線／1 連線中／2 下載中；勿以「0＝成功」慣例解讀。
6. **SolaceCloseByID 會連報價一起斷**：若該 Solace 連線同時負責報價，中斷回報也會中斷報價；要斷全部 Solace 連線用 `SKQuoteLib_LeaveMonitor`。
7. **舊 API 汰換對照**：OnConnect→OnSolaceReplyConnection、OnDisconnect→OnSolaceReplyDisconnect（v2.13.48）；OnData→OnNewData（即將下線）；OnSmartData→OnStrategyData（V2.13.38 移除期選格式）；SKReplyLib_CloseByID→SKReplyLib_SolaceCloseByID。新開發一律用新版。
8. **OnNewData 解析注意**：先判斷 `values[0]=="980"`（後台問題訊息，非標準欄位格式）再解析；履約價欄位（index 9）為舊保留欄位，履約價請看 StrikePrice1/StrikePrice2（V2.13.45 修改比較表）；成交序號請以 ExecutionNo（index 38）為主而非 OkSeq；異動前量/異動後量僅證券與複委託市場提供；海期價格帶分子/分母欄位（index 12-19）需另行組合；ErrorMsg 內的「,」已被替換為空白（V2.13.39）。**V2.13.59 起複委託（MarketType＝OS）的委託時效欄位在來源未給值時會是空字串**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），欄位不會消失、也不會位移，但解析要容忍空值，別直接把該欄拿去做 enum 轉換或非空斷言。
9. **OnNewData 欄位數會隨版本增加**（V2.13.38 加 SeqNo、V2.13.40 加海期停損觸發註記共 49 欄；**V2.13.59 國內期選再加「下單時間 HH:mm:ss.fff」，但官方未公開其 index，欄位表、修改比較表與範例都沒更新**——`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`、`api_spec/_raw/v2.13.59/12.回報.md:210-222`、`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTesterV2/WindowsFormsApp1/ReplyForm.cs:1431`），解析請用「至少 N 欄」而非「恰好 N 欄」的防禦式寫法，並在模擬環境實測 TF／TO 的實際欄位序列後再定位新欄。
10. **OnReplyClear 參數是市場別**（R1/R2/R3/R4/R11/R20~R23）而非 UserID，與 OnReplyClearMessage（參數為 UserID）不同，勿混用。
11. **智慧單回報範圍**：ConnectByID 建立的智慧單回報僅支援國內證券、期貨、選擇權（海期智慧單 OCO/AB 另由 OnStrategyData 海期格式提供）；需先簽署 API 下單聲明書。
12. **OnStrategyData 欄位佈局依市場與單別而異**：共用欄位之後接各單別專屬欄位，證券與期貨共用欄位數不同（證券多「一般/零股/盤後」欄）；智慧單狀態 999 時要改讀萬用訊息 UniversalMsg 欄位。
13. 文件間小出入：12.回報.md 在 IsConnectedByID 備註寫「請同時接收 OnConnect」，主說明 V2.13.57 已改為「OnSolaceReplyConnection」——以主說明（新版）為準。
14. 抽取文本中的 `SKReplyLib_OnReplyMessage4`、`SKReplyLib_OnReplyMessageV2` 為 docx 轉檔黏字造成的假名（實為「OnReplyMessage + 4-3-e」「OnReplyMessage + V2.13.57」），並非真實成員。
15. **多帳號回報連線的斷線（V2.13.59 修正）**：官方版本控管表載明「修正主動回報連線多帳號，無法斷線問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），但未指名函式，4-3-1／4-3-4 兩節文字皆未改（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:2034-2040,2055-2061`）。同時掛多個帳號回報連線者，升版後應**實測 `SKReplyLib_SolaceCloseByID` 的釋放與重連行為**：逐一斷線是否都收到 `OnSolaceReplyDisconnect`（3002）、重連後是否有殘留連線疊加、`SKReplyLib_IsConnectedByID` 是否如實回 0。舊程式若寫了「斷不掉就重啟 API」的繞道，可在驗證後移除。
16. **智慧單「被動回報」缺逗號修正（V2.13.59）**：官方原文「修正智慧單被動回報缺少逗號問題」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`），未指名事件。被動回報事件屬 SKOrderLib（`OnStopLossReport` 4-2-f／`OnTSSmartStrategyReport` 4-2-n／`OnOFSmartStrategyReport` 4-2-s，見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:1734,1831,1953`），非本模組的 OnStrategyData；但 V2.13.47 曾對 OnStrategyData 做過同型修正（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:26`）。**若與 V2.13.47 同型（兩欄之間漏一個逗號），修好後分隔數會多一個**，凡是曾針對舊版錯誤格式做過欄位合併／位移補償的解析器，在 .59 就可能解錯，升版建議重跑一次欄位對照。實際被修的是哪一支事件、缺的逗號在哪兩欄之間，官方未載，需實測或洽群益確認。
17. **.59 官方範例的雙帳號回報回歸**：`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:153-164` 的 OnComplete／OnNewData 移除了 .57 版依 `m_strLoginID`／`m_strLoginID2` 的分流（.57 見 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKReply.cs:153-184`），第二組帳號訊息會併入第一組清單並誤點亮第一組 Solace 燈號；同檔 OnSmartData／OnStrategyData 分流仍在（`Source_code/CapitalAPI_2.13.59_CExample/SKCOMTester/SKReply.cs:165-187`）。**COM 事件與簽名未變**，只是範例不再適合當雙帳號回報的起始模板——雙帳號請以 .57 版範例為參考。
