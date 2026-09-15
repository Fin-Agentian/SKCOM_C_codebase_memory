# SKCenterLib — 登入、環境設定與系統中心物件（所有其他物件使用前的入口）

SKCenterLib 是策略王 COM 元件（SKCOM.dll，透過 `Interop.SKCOMLib` 使用）的登入與環境設定物件。所有下單（SKOrderLib）、回報（SKReplyLib）、報價（SKQuoteLib / SKOSQuoteLib / SKOOQuoteLib）功能都必須先經由本物件完成雙因子登入後方可使用。大部分函式執行成功回傳 0（SK_SUCCESS），錯誤代碼見 [../error_codes.md](../error_codes.md)。

- 文件出處：`api_spec/_raw/3.登入.md`、`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md`（4-1 節）、`api_spec/_raw/16.SGX_DMA專線.md`、`api_spec/_raw/1.環境設置.md`、`api_spec/_raw/2.導覽.md`
- 文件出處（V2.13.59 增補部分）：`api_spec/_raw/v2.13.59/3.登入.md`、`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md`（4-1 節）、`api_spec/_raw/v2.13.59/1.環境設置.md`、`api_spec/_raw/v2.13.59/2.導覽.md`
- 版本基準：V2.13.59（以 V2.13.57 規格為底增補；差異見 [../changelog_2.13.57_to_2.13.59.md](../changelog_2.13.57_to_2.13.59.md)）
- 本模組 V2.13.57→V2.13.59 無函式／事件增減：`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:155-281` 的 4-1 節清單（4-1-1～4-1-11、4-1-a～4-1-c）與 V2.13.57 完全相同，`Interop.SKCOMLib` 的 `SKCenterLib_*` 符號兩版亦一致。本次差異集中在新增錯誤碼 9996 與文件敘述層級。

## 總覽：功能分區表

| 分區 | 函式/事件 | 用途一句話 |
|---|---|---|
| 登入 | SKCenterLib_Login | 元件初始登入（雙因子-憑證綁定） |
| 登入 | SKCenterLib_LoginSetQuote | 登入並設定是否啟用報價功能（Y/N） |
| 登入（群組身份） | SKCenterLib_GenerateKeyCert | AP/APH 群組無憑證身份，以附屬帳號憑證產生雙因子登入 key |
| 環境設定 | SKCenterLib_SetLogPath | 設定 LOG 檔存放路徑（需最先呼叫） |
| 環境設定 | SKCenterLib_SetAuthority | 手動設定特殊功能屬性（SGX 專線 / 測試環境切換） |
| 環境設定 | SKCenterLib_Debug | 開啟函式呼叫 Log 記錄（Debug 模式） |
| 系統資訊 | SKCenterLib_GetReturnCodeMessage | 將回傳代碼轉為文字訊息 |
| 系統資訊 | SKCenterLib_GetLastLogInfo | 取得最後一筆 LOG 內容 |
| 系統資訊 | SKCenterLib_GetSKAPIVersionAndBit | 取得目前註冊 SKAPI 版本及位元（EX: 2.13.30_x64） |
| 同意書 | SKCenterLib_RequestAgreement | 查詢所有聲明書及同意書簽署狀態 |
| 已移除 | SKCenterLib_ResetServer | 功能說明已於 V2.13.45 自官方文件刪除 |
| 僅見於範例碼 | SKCenterLib_SetICEBrand | 設定 ICE 品牌來源（文件未載） |
| 僅見於範例碼 | SKCenterLib_SetMCBrand | 設定 MC 品牌來源（文件未載） |
| 僅見於範例碼 | SKCenterLib_SetMCWhiteBrand | 設定 MC White 品牌來源（文件未載） |
| 事件 | OnTimer | 定時 Timer 通知，每分鐘回傳一次時間 |
| 事件 | OnShowAgreement | 同意書狀態（未簽署）通知 |
| 事件 | OnNotifySGXAPIOrderStatus | SGX API DMA 專線下單連線狀態通知 |

## 初始化與事件註冊

環境前置：SKCOM.dll 為 ActiveX COM 元件，需先以 `regsvr32`（或元件資料夾內 `install.bat`，系統管理員身分）註冊，位元（x64/x86）需與專案建置目標一致；C# 專案以 Add Reference 引入 SKCOM.dll 後 `using SKCOMLib;`（詳見 `api_spec/_raw/1.環境設置.md`）。

V2.13.59 文件改寫：官方分冊已改寫 regsvr32 的位元對應敘述——「x32位元: 透過SysWow64的regsvr32.exe註冊 / x64位元: 透過System32的regsvr32.exe註冊 或 直接註冊即可」（`api_spec/_raw/v2.13.59/1.環境設置.md:99-100`），與 V2.13.57 寫法（x86 直接註冊、x64 透過 SysWow64）恰好互換。官方 2.13.58／2.13.59 版本歷程均未收錄此項，無從判定改寫落在哪一版，官方亦未自承前版有誤（V2.13.57 舊句以「作業系統位元」為主詞時亦可自洽）。新敘述以「元件位元」為主詞讀，與兩版隨附 `install.bat` 的實際內容一致：x86 版元件呼叫 `%systemroot%\SysWoW64\regsvr32.exe`（`Source_code/CapitalAPI_2.13.59_CExample/元件/x86/install.bat:14`；同檔 `:7-12` 另有 Windows XP x86 分支改直接呼叫 `regsvr32.exe`），x64 版元件直接呼叫 `regsvr32.exe`（`Source_code/CapitalAPI_2.13.59_CExample/元件/x64/install.bat:5`）。註冊時請以 `install.bat` 為準。註：V2.13.59 合輯主手冊附錄 A 仍為舊敘述（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4956-4957`），與同版分冊不一致。

官方範例 SKCOMTesterV2 的實際寫法（節錄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:3,19,184-235`）：

```csharp
using SKCOMLib;

// 全域變數
SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件

private void MainForm_Load(object sender, EventArgs e)
{
    // 註冊公告(必要)：登入前必須先掛 SKReplyLib.OnReplyMessage 並回傳 -1
    m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(OnAnnouncement);
    void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
    {
        nConfirmCode = -1;
        string msg = "【註冊公告OnReplyMessage】" + strUserID + "_" + bstrMessage;
        richTextBoxMessage.AppendText(msg + "\n");
    }

    // 同意書狀態通知
    m_pSKCenter.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(OnShowAgreement);

    // 定時Timer通知。每分鐘會由該函式得到一個時間
    m_pSKCenter.OnTimer += new _ISKCenterLibEvents_OnTimerEventHandler(OnTimer);

    // SGX API DMA專線下單連線狀態
    m_pSKCenter.OnNotifySGXAPIOrderStatus += new _ISKCenterLibEvents_OnNotifySGXAPIOrderStatusEventHandler(OnNotifySGXAPIOrderStatus);

    // 取得目前註冊SKAPI 版本及位元
    labelSKCenterLib_GetSKAPIVersionAndBit.Text = m_pSKCenter.SKCenterLib_GetSKAPIVersionAndBit("xxxxx");
}
```

另見 SKCOMTester（舊版範例）以第二個 SKCenterLib 物件掛事件的寫法：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:43-44,64-65`。多個 AP/APH 群組 ID 登入時，官方建議使用不同 SKCenterLib 物件分別處理（V2.13.35 版本說明）。

## 方法

### SKCenterLib_SetLogPath
- 用途：設定 LOG 檔存放路徑。預設 LOG 存放於執行之應用程式下（與 SKCOM.dll 同層之 CapitalLog 資料夾）。
- 簽名：`int SKCenterLib_SetLogPath(string bstrPath)`（文件宣告 `Long SKCenterLib_SetLogPath([in] BSTR bstrPath);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrPath | string (BSTR) | LOG 檔存放路徑 |

- 回傳：int（LONG）錯誤碼；0 表示成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：欲變更 Log 路徑，此函式需「最先」呼叫（先於 Login 等一切函式）。V2.13.38 曾失效、V2.13.39 修正。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:390`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:204`

### SKCenterLib_Login
- 用途：元件初始登入。使用本 Library 前必須先通過雙因子（憑證綁定）身份認證。
- 簽名：`int SKCenterLib_Login(string bstrUserID, string bstrPassword)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 使用者登入帳號 |
| bstrPassword | string (BSTR) | 密碼 |

- 回傳：int（LONG）錯誤碼；0 表示初始化成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：
  - 登入前需先註冊公告 SKReplyLib_OnReplyMessage（否則 2017 SK_WARNING_REGISTER_REPLYLIB_ONREPLYMESSAGE_FIRST）。
  - 一般身份：登入前需安裝登入 ID 有效憑證；AP/APH 群組身份：需先執行 SKCenterLib_GenerateKeyCert 成功，否則得到 1103 SK_ERROR_AP_APH_GENERATEKEY_INVALID_BEFORE_LOGIN。
  - V2.13.57 含以上：登入失敗需間隔五秒後才能再次嘗試（1129）；登入失敗達五次須重新啟動 API（9997）。
    - 此段敘述 V2.13.59 未變動（`api_spec/_raw/v2.13.59/3.登入.md:139`、`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:191`）。
  - V2.13.59 新增：9996 SK_ERROR_UPDATE_API_REQUIRED「此版本已無法登入，請更新版本。」——官方說明僅此一句（未載重試規則）；依其字面，此碼代表目前使用的元件版本已無法再登入，須更新 API 版本，與 9997（失敗五次、重啟 API 即可再試）應分流處理（`api_spec/_raw/v2.13.59/2.導覽.md:137,172`、`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4893`）。注意 V2.13.59 分冊 `3.登入.md` 的登入錯誤碼表只列到 9997（`api_spec/_raw/v2.13.59/3.登入.md:350`），9996 僅見於導覽與主手冊第 6 章代碼表。
  - 登入失敗原因可查 Center.log；常見（V2.13.45 起密碼平台代碼）：300 密碼錯誤、306 身分證字號（帳號）錯誤、602 未安裝登入 ID 有效憑證、604 憑證過期或已註銷。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:880`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:109`、`Source_code/CapitalAPI_2.13.57_CExample/ExcelSample/Program.cs:507`

### SKCenterLib_GetReturnCodeMessage
- 用途：取得定義代碼之文字訊息，慣例上包住所有函式回傳碼做訊息顯示。
- 簽名：`string SKCenterLib_GetReturnCodeMessage(int nCode)`（文件宣告 `BSTR SKCenterLib_GetReturnCodeMessage([in] LONG nCode);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nCode | int (LONG) | 函式回傳值（錯誤碼） |

- 回傳：string，代碼文字訊息。
- 備註：全範例碼共用之錯誤碼轉譯函式；代碼一覽見 [../error_codes.md](../error_codes.md)。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:882`（另有全專案數百處使用）

### SKCenterLib_Debug
- 用途：函式呼叫 Log 記錄。開啟時會記錄呼叫過的函式與所帶入的參數。
- 簽名：`int SKCenterLib_Debug(bool bDebug)`（依 docx 宣告 `Long SKCenterLib_Debug([in] VARIANT_BOOL bDebug);` 重建，簽名待確認）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bDebug | bool (VARIANT_BOOL) | 是否為 Debug 模式 |

- 回傳：int（LONG）錯誤碼；0 表示成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：LOG 會產生於 LOG 資料夾，檔名 `YYYYMMDD_XXXX.log`。
- 範例：範例未見

### SKCenterLib_GetLastLogInfo
- 用途：取得最後一筆 LOG 內容。
- 簽名：`string SKCenterLib_GetLastLogInfo()`（文件宣告 `BSTR SKCenterLib_GetLastLogInfo();`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| （無） | — | — |

- 回傳：string，最後一筆 LOG 資料內容。
- 備註：可搭配 GetReturnCodeMessage 補充錯誤診斷（CppTester 即兩者並印）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:400`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:246`

### SKCenterLib_SetAuthority
- 用途：（SGX 專線 only）手動設定特殊功能屬性開啟或關閉；同時可切換正式/測試環境。
- 簽名：`int SKCenterLib_SetAuthority(int nAuthorityFlag)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nAuthorityFlag | int (LONG) | 特殊功能屬性旗標：bit 0＝SGX 專線關閉/開啟（0/1）；bit 1＝環境設定（預設 0＝正式環境）。即 0=正式、1=正式+SGX、2=測試、3=測試+SGX |

- 回傳：int（LONG）錯誤碼；0 表示成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：一般客戶可忽略。SGX API DMA 專線需向交易後台申請；連線狀態由 OnNotifySGXAPIOrderStatus 事件回報。範例碼於登入（Login/LoginSetQuote）前呼叫。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:99`（另見 299、360、384）

### SKCenterLib_RequestAgreement
- 用途：取得所有聲明書及同意書簽署狀態。預設登入時就會主動查詢，一般不需特別執行。
- 簽名：`int SKCenterLib_RequestAgreement(string bstrUserID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 使用者登入帳號 |

- 回傳：int（LONG）錯誤碼；0 表示成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：簽署狀態由 OnShowAgreement 事件回傳。查詢時海外行情同意書一定查詢，其他狀態已為【已簽署】者不再查詢；全部已簽署時回傳 1075 SK_ERROR_ALL_AGREEMENT_SIGNED。
  - V2.13.59 文件不一致（未列官方 changelog）：分冊已刪去「海外行情同意書一定查詢」特例，改為「查詢時，聲明書或同意書若狀態已為【已簽署】的時候，不會再進行查詢。」（`api_spec/_raw/v2.13.59/3.登入.md:244`）；但同版主手冊 4-1-8 仍保留含此特例的原句（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:232`）。兩者互相矛盾，實際行為以群益期貨確認為準；程式不應假設「海外行情同意書每次都會被重新查詢」。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:362`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:349`

### SKCenterLib_LoginSetQuote
- 用途：元件初始登入，並設定是否啟用報價元件功能（程式僅用下單/回報、不需行情時，以此登入可不占用行情連線）。
- 簽名：`int SKCenterLib_LoginSetQuote(string bstrUserID, string bstrPassword, string bstrSetFlag)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 使用者登入帳號 |
| bstrPassword | string (BSTR) | 密碼 |
| bstrSetFlag | string (BSTR) | 是否啟用報價元件功能：Y＝啟用報價、N＝停用報價 |

- 回傳：int（LONG）錯誤碼；0 表示初始化成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：前置條件與 SKCenterLib_Login 相同（先註冊公告、憑證/GenerateKeyCert）。設 N 停用報價後執行行情功能會得到 1081 SK_ERROR_LOGIN_WITHOUT_SETQUOTE。V2.13.48 曾修正此函式登入失敗問題。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:364`

### SKCenterLib_GetSKAPIVersionAndBit
- 用途：取得目前註冊 SKAPI 版本及位元。
- 簽名：`string SKCenterLib_GetSKAPIVersionAndBit(string bstrUserID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 使用者登入帳號 |

- 回傳：string，目前註冊 SKAPI 版本及 COM 位元（EX: `2.13.30_x64`）。
- 備註：可於登入前呼叫；官方範例實際帶入任意字串（"123"、"xxxxx"）也可取得版本。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:234`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:197`

### SKCenterLib_GenerateKeyCert
- 用途：僅適用 AP 及 APH 無憑證（群組）身份：以群組底下已安裝憑證之附屬帳號，產生雙因子登入憑證資訊（key）。
- 簽名：`int SKCenterLib_GenerateKeyCert(string bstrLogInID, string bstrCustCertID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrLogInID | string (BSTR) | （群組）登入帳號 |
| bstrCustCertID | string (BSTR) | 綁定在該群組之下且已安裝憑證之附屬帳號 ID |

- 回傳：int（LONG）錯誤碼；0 表示成功，其餘見 [../error_codes.md](../error_codes.md)。
- 備註：需在登入前執行且成功，再執行 SKCenterLib_Login，否則登入失敗（1103）。附屬帳號不屬於該 AP 群組時回傳 1070。非群組身份執行會收到 2028 SK_WARNING_SHOULD_IGNORE_GENERATEKEYCERT_STEP。多個 AP/APH 群組 ID 需逐一先 GenerateKeyCert 成功再登入。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:413`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:387`

### SKCenterLib_ResetServer（已移除）
- 用途：（推測）重設/切換連線主機。官方於 V2.13.45 版本說明中載明「刪除 SKCenterLib_ResetServer 功能說明」，現行文件無此函式規格。
- 簽名：不可考（文件已刪除、範例碼無使用），請勿在新程式中使用。
- 參數表：文件未載。
- 回傳：文件未載。
- 備註：僅存於版本沿革記錄（`api_spec/_raw/2.導覽.md:283`）。
- 範例：範例未見

## 僅見於範例碼

以下函式官方文件未載，僅出現在官方 C# 範例 SKCOMTester 的初始化流程（登入前依「行情來源品牌」下拉選單擇一呼叫），推測與白牌/品牌行情來源設定有關，一般客戶可忽略：

### SKCenterLib_SetICEBrand
- 用途：（推測）設定 ICE 品牌之行情/服務來源，帶入登入帳號。文件未載。
- 簽名（推測）：`int SKCenterLib_SetICEBrand(string bstrUserID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 文件未載（範例帶入登入帳號大寫） |

- 回傳：文件未載（範例未取用回傳值，推測為 LONG 錯誤碼）。
- 備註：於登入前呼叫。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:86`

### SKCenterLib_SetMCBrand
- 用途：（推測）設定 MC 品牌之行情/服務來源，帶入登入帳號。文件未載。
- 簽名（推測）：`int SKCenterLib_SetMCBrand(string bstrUserID)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 文件未載（範例帶入登入帳號大寫） |

- 回傳：文件未載（推測為 LONG 錯誤碼）。
- 備註：於登入前呼叫。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:88`

### SKCenterLib_SetMCWhiteBrand
- 用途：（推測）設定 MC White（白牌）品牌之行情/服務來源。文件未載。
- 簽名（推測）：`int SKCenterLib_SetMCWhiteBrand(string bstrUserID, bool bFlag)`
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrUserID | string (BSTR) | 文件未載（範例帶入登入帳號大寫） |
| bFlag | bool (VARIANT_BOOL) | 文件未載（範例固定帶 true） |

- 回傳：文件未載（推測為 LONG 錯誤碼）。
- 備註：於登入前呼叫。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:90`

## 事件

C# 事件掛載使用 `Interop.SKCOMLib` 之委派型別 `_ISKCenterLibEvents_On<事件名>EventHandler`。

### OnTimer
- 用途：定時 Timer 通知。每分鐘會由該事件得到一個時間。
- 簽名（handler）：`void OnTimer(int nTime)`（文件宣告 `void OnTimer([in] LONG nTime);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nTime | int (LONG) | 時間。EX：133525，表示 13:35:25 |

- 回傳：無。
- 備註：可作為 API 存活/心跳確認。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:213-220`

### OnShowAgreement
- 用途：同意書狀態通知（登入時主動查詢，或呼叫 SKCenterLib_RequestAgreement 後回傳）。
- 簽名（handler）：`void OnShowAgreement(string bstrData)`（文件宣告 `void OnShowAgreement([in] BSTR bstrData);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrData | string (BSTR) | 未簽署的同意書訊息；或 RequestAgreement 查詢之同意書狀態 |

- 回傳：無。
- 備註：API 不支援簽署同意書，請至群益金融網等其他管道簽署。＊期貨 API 下單聲明書簽署，新增補簽狀態為 P。對應函式：SKCenterLib_RequestAgreement。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:203-209`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:64,251-253`

### OnNotifySGXAPIOrderStatus
- 用途：SGX API DMA 專線下單連線狀態通知；確認連線成功方可進行委託。
- 簽名（handler）：`void OnNotifySGXAPIOrderStatus(int nStatus, string bstrOFAccount)`（文件宣告 `void OnNotifySGXAPIOrderStatus([in] LONG nStatus, [in] BSTR bstrOFAccount);`）
- 參數表：

| 參數 | 型別 | 說明 |
|---|---|---|
| nStatus | int (LONG) | 回傳連線狀態（3002＝斷線、3026＝SGX API 專線建立完成、1053＝SGX API 登入失敗） |
| bstrOFAccount | string (BSTR) | 回傳已登入成功之海期帳號 |

- 回傳：無。
- 備註：SGX API DMA 專線需向交易後台申請方可使用。對應函式：SKCenterLib_SetAuthority（開啟 SGX 專線屬性）。
- 範例：`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:224-230`、`Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:65`

## 陷阱與注意

- 呼叫順序固定：SKCenterLib_SetLogPath（如需改路徑，最先）→ 註冊 SKReplyLib.OnReplyMessage（必要，handler 需 `out short nConfirmCode` 回傳 -1）→（群組 AP/APH）SKCenterLib_GenerateKeyCert →（SGX 用戶）SKCenterLib_SetAuthority → SKCenterLib_Login 或 SKCenterLib_LoginSetQuote。未先註冊公告會得到 2017。
- 登入帳號需為大寫：官方範例一律 `txtAccount.Text.Trim().ToUpper()`；錯誤碼 1000 備註「請注意登入帳號是否為大寫」。
- V2.13.57 登入節流：登入失敗需間隔五秒才能再試（1129 SK_ERROR_LOGIN_FAIL_IN_PROCESSING）；失敗達五次須重新啟動 API（9997 SK_ERROR_LOGIN_FAIL_LIMIT）。自動重試邏輯務必加延遲與次數上限。
- V2.13.59 起另有 9996 SK_ERROR_UPDATE_API_REQUIRED「此版本已無法登入，請更新版本。」（`api_spec/_raw/v2.13.59/2.導覽.md:137`、`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4893`）：官方原文僅「此版本已無法登入，請更新版本。」一句；依其字面，此碼非暫時性失敗，重試或重啟 API 不會改變結果，須更新 SKCOM.dll 版本。**自動重試白名單不得包含 9996**——收到即停止重試、停止排程，並向使用者提示更新版本；9997 走「重啟 API」路徑、1129 走「延遲後重試」路徑，三者必須分流。
- 雙因子憑證綁定（V2.13.35 起）：一般身份需安裝登入 ID 有效憑證；憑證過期/未安裝常見錯誤 602；電腦同時存在兩張憑證時，請先刪除無效或過期憑證（600）。AP/APH 群組身份未先 GenerateKeyCert 會得 1103。
- V2.13.45 起登入錯誤碼改由密碼平台回覆（101/300/306/307/321/502/507/511/600/602/603/604，完整表見 `api_spec/_raw/3.登入.md:352-366`；另 `api_spec/_raw/策略王COM元件使用說明_V2.13.45以上登入代碼定義.md` 為節錄版，缺 321/602/603），與舊版 151~155、500~599 代碼並存於文件，判讀時注意 API 版本。
- 行情連線限制：一個 ID 預設最多 2 條行情連線（國內共用 1 條、海外期選 1 條）；超過限制訂閱行情會收到 3030。只做下單/回報的程式請用 `SKCenterLib_LoginSetQuote(ID, password, "N")` 停用報價以免占用連線；停用後使用行情功能會得 1081。
  - 來源標註：上述數字出自 V2.13.57 主手冊 3-3「行情物件連線限制說明」（`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md:154-164`）。V2.13.59 主手冊已整章刪除 3-3／3-3-1／3-3-2／3-4，3-2 註冊公告（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:135`）之後直接接第 4 章「ATL物件函式說明」（同檔 `:154`）；V2.13.59 僅剩錯誤碼 3030 SK_SUBJECT_NO_QUOTE_SUBSCRIBE「行情連線超過限制時，無法訂閱行情通知」（同檔 `:4878`）與 4-4-23 SKQuoteLib_GetQuoteStatus 備註「＊EX：若最大連線數為2, 且目前連線超過限制,則回傳:2,True.」（同檔 `:2480,2487`）可間接佐證限制仍在。官方 V2.13.59 手冊已移除此章節，但未宣告取消限制，本節連線限制說明以 V2.13.57 留存原文為準；設計上仍應假設 2 條上限，並以 SKQuoteLib_GetQuoteStatus 實測為準。
- 文件筆誤：`2.導覽.md:101` 與主說明 160 行將函式寫成 `SKCenterLib_LogInSetQuote`（In 大寫），實際 Interop 方法名為 `SKCenterLib_LoginSetQuote`。
- 主說明 4-1 節編號跳號（無 4-1-5），非缺漏函式。
- SKCenterLib_SetAuthority 的 nAuthorityFlag 是位元旗標（bit 0＝SGX、bit 1＝測試環境），不是布林；一般客戶不需呼叫，誤設 bit 1 會切到測試環境。
- 同意書：API 不支援簽署，只回報狀態（OnShowAgreement）；未簽署證券/期貨 API 下單聲明書會出現 2018/2019，且無法訂閱或取得相關市場商品資料（9999）。憑證/同意書查詢主機 Telnet 失敗見 1097/1098。
- COM 註冊位元須與程式建置位元一致（x64 dll 配 x64 build），SKCOM.dll 與憑證、報價元件需同資料夾註冊；更版時先 uninstall 舊版再 install 新版（詳見 `api_spec/_raw/1.環境設置.md`）。
  - V2.13.59 文件改寫：regsvr32 的位元對應敘述與 V2.13.57 恰好互換（官方版本歷程未收錄此項，無從判定落在哪一版，亦無官方文字自承前版有誤）。依 V2.13.59《1.環境設置》的寫法——註冊 32 位元 SKCOM.dll 用 `%systemroot%\SysWoW64\regsvr32.exe`、註冊 64 位元 SKCOM.dll 用 `System32\regsvr32.exe` 或直接 `regsvr32.exe`（`api_spec/_raw/v2.13.59/1.環境設置.md:99-100`；與 `Source_code/CapitalAPI_2.13.59_CExample/元件/x86/install.bat:14`、`Source_code/CapitalAPI_2.13.59_CExample/元件/x64/install.bat:5` 實際內容相符，V2.13.57 與 V2.13.59 兩版隨附的 install.bat 完全相同）。註：V2.13.59 合輯主手冊附錄 A 未同步更正，仍寫「x86位元:直接註冊即可 / x64位元:透過SysWow64的regsvr32.exe註冊」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4956-4957`），請勿依循。最保險是直接執行元件資料夾內對應位元的 `install.bat`／`Uninstall.bat`（系統管理員身分，`api_spec/_raw/v2.13.59/1.環境設置.md:12-13,24`）。
- SKCenterLib_ResetServer 已於 V2.13.45 自文件移除，勿再使用。
- SetICEBrand / SetMCBrand / SetMCWhiteBrand 為文件未載之品牌來源設定函式，僅特定（白牌）客群適用，一般客戶請勿呼叫。
