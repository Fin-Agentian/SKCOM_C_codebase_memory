# 更新紀錄（CHANGELOG）

本檔為 **SKCOM 規格庫** 的發版紀錄；官方 CapitalAPI 版本歷程請見 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md`（版本控管表）。詳細變更矩陣、對抗驗證證據與 33 個開放問題見 [`api_spec/changelog_2.13.57_to_2.13.59.md`](api_spec/changelog_2.13.57_to_2.13.59.md)。

---

## [v2.13.59] — 2026-09-08

規格庫基準由 **V2.13.57** 升到 **V2.13.59**。核心結論：**混合：整體優化**——COM／Interop 表面是純加法（Interop `+39/−0`、原生 `SKCOM.dll` PE 匯出表 `78→83, +5/−0`），直接呼叫 COM 者換版一行不改、且多出即時分K、未平倉 JSON、權益數查詢狀態、海選 OLID 等新能力；退步集中在 COM 之外的三處。

### 官方 CapitalAPI 兩次版本歷程（.59 手冊新增）

- **2026-03-09 V2.13.58** — 新增 `OnOpenInterestJson`、`SendOverseaOptionOrderOLID`；未平倉「查無庫存」回傳新增 `Account` 欄位；`SendStockStrategyCB` 1023 修復、SGX 專線主動回報缺漏、國內報價價差商品／報價慢／T 盤加 AM 範圍縮小、`GetStockByNoLONG` 取物件修正；文件調整下單函式回傳 0 語意為「已送至交易所」（橫跨主手冊 81 處與 8 份分冊）。
- **2026-08-03 V2.13.59** — 即時分K `SKQuoteLib_GetLiveKLineLONG`＋`OnNotifyLiveKLineData`＋`struct SKKLINE`；`SKQuoteLib_EnterMonitorLONGByMarket`；`GetOpenInterestGW` 新增「商品－下單代碼」欄位；`OnNewData` 國內期選新增「下單時間 HH:mm:ss.fff」；`GetRealBalanceReport` 新增「昨日庫存」；新錯誤碼 **9996 `SK_ERROR_UPDATE_API_REQUIRED`**（此版本已無法登入，須更新版本；終止性）。修正：國內報價第一筆 Ticks 缺失、內期夜盤收盤價未更新、複委託回報無時效欄位時給空值、主動回報多帳號無法斷線、智慧單被動回報缺逗號、`index` 不一致、簡化版 API 下單成功回傳值。

### API 表面變化（硬證據）

| 項目 | .57 | .59 | 差異 |
|---|---|---|---|
| `元件/x64/Interop.SKCOMLib.dll` 符號 | 1,252 | 1,291 | **+39 / −0**；IID、組件版本未變，drop-in 可替換 |
| `元件/x64/SKCOM.dll` PE 匯出表（objdump） | 78 | 83 | **+5 / −0**：`GetRealBalanceReport`、`GetOpenInterestGW`、`GetFutureRights`、`GetOFOpenInterestGW`、`GetOFFutureRights` |
| 簡化版 `SKDLLTester/.../SKDLLCSharp.dll` | 45,568 B | 70,144 B | **−6 方法** 加 **+5 方法**（詳下） |
| 官方 docx 手冊 | 21 份 | 19 份 | 下架 `14.海期報價.docx`、`15.海選報價.docx`；主手冊刪 4-5／4-6 兩章與 3-3／3-4 節 |
| 官方範例 `.cs` 檔 | 114 | 96 | 移除 `SKOSQuote`／`SKOOQuote`（SKCOMTester）、`OSQuoteForm`／`OOQuoteForm`（V2）、`SKOSQuote.cpp/.h`（CppCLI） |

### ⚠️ 相容性：不改會壞或建議跟進

- **[必改]** 內／外期未平倉「查無資料」由 `M003 NO DATA#` 改為 `001,查無資料,帳號`（V2.13.58）——舊字串比對失效，可能將 3 欄字串塞進正常解析路徑造成 `IndexOutOfRangeException` 或誤解析出假部位。改法：`split(',') 首欄=="001" 或整串含「查無資料」`＋索引前檢查欄位數；同時容忍主手冊 3 欄與分冊 7 的 2 欄兩種官方不一致格式。
- **[強制升級訊號]** 新錯誤碼 **9996** 為終止性錯誤，重試永遠無效——只認 9997 者將落入泛用 else、含自動重試迴圈者會無限重試並撞上「登入失敗五次須重啟 API」鎖定。改法：在登入回傳處理加獨立 9996 分支（排在泛用 else 之前）、加入不可重試白名單；根治手段是升級到 2.13.59 元件。
- **[破壞性—僅限簡化版 `SKDLLCSharp.dll`]** 移除 6 支海期／海選報價方法（`SKOSQuoteLib_RequestStocks/RequestTicks/GetStockByNoNineDigitLONG`、`SKOOQuoteLib_RequestStocks/RequestTicks/GetStockByNoLONG`）；`OnNotifyOS*/OO*` 24 個事件識別字保留，形成「事件在、無法訂閱」的孤兒。**COM／Interop 完好無損**（sym57/sym59 對 SKOSQuoteLib 56:56、SKOOQuoteLib 37:37 逐行相同）。改法：短期把海期／海選報價路徑凍結在 .57 版 SKDLLCSharp.dll、長期改直呼 COM 元件。
- **[建議跟進]** `GetOpenInterestGW` 新增「商品－下單代碼」欄位、`OnNewData`（國內期選 TF/TO）新增「下單時間 HH:mm:ss.fff」、複委託回報無時效欄位時給空值、智慧單被動回報補逗號——**官方均未指名欄位位置**，可能附加末端也可能中間插入。改法：欄位數檢查由「恰好 N」改「至少 N」、索引前加邊界防護、實機 dump 一筆確認 index 後才啟用固定索引解析；解析器改用具名對照表或穩定前綴欄位定位。
- **[語意調整]** 下單／改單／刪單／Proxy 全家族回傳 0 由「委託成功」改述為「已送至交易所，結果請由回報確認」（V2.13.58 文件調整、無介面變化）；把 `nCode==0` 當終態的邏輯語意上一直是錯的、此版只是講白。改法：一律改為「已受理」，最終狀態由 `SKReplyLib.OnNewData`／`OnAsyncOrder`／`OnProxyOrder` 決定。

完整 24 項風險與逐項因應：[`api_spec/changelog_2.13.57_to_2.13.59.md#4-需跟進的相容性風險依嚴重度`](api_spec/changelog_2.13.57_to_2.13.59.md)。

### 常見問題

**Q. .59 還有海期／海選下單和報價功能嗎？**

- **海期／海選下單**：完全還在，2.13.58 還新增 `SendOverseaOptionOrderOLID`（可帶自訂資料欄）。
- **海期／海選報價（原生 COM `SKOSQuoteLib`／`SKOOQuoteLib`）**：**完好無損**。Interop 符號兩版逐行相同，錯誤碼 2015／2025／2026 仍在，.59 的下單範例還在呼叫 `SKOSQuoteLib_EnterMonitorLONG`。
- **官方 docx 手冊與 C# 範例**：整批下架（4-5／4-6 章、14／15 分冊消失、`SKOSQuote.cs`／`SKOOQuote.cs` 從 SKCOMTester 移除）——本 repo 保留 V2.13.57 的原文與範例於 `api_spec/_raw/*.md` 平面檔與 `Source_code/CapitalAPI_2.13.57_CExample/`，做為留存文件；規格 `api_spec/modules/SKOSQuoteLib.md`／`SKOOQuoteLib.md` 檔頭已加註「官方 V2.13.59 手冊已移除本章，函式未移除；本節以 V2.13.57 留存原文為準」。
- **簡化版 `SKDLLCSharp.dll`**：見上方「破壞性變更」，該路線的海期／海選報價已無法使用，須改走 COM。

**Q. Clone 下來後怎麼直接開始開發？**

規格庫本身是純 Markdown＋官方原始文件＋範例碼，clone 完就能用：

1. **零安裝、餵給 AI**：把 `api_spec/README.md` 或任一 `modules/*.md`、`flows/*.md` 貼給 ChatGPT／Claude／本地模型當 context 即可。
2. **知識圖譜（免重新索引）**：裝 [`codebase-memory-mcp`](https://github.com/DeusData/codebase-memory-mcp) 後對本資料夾跑 `codebase-memory-mcp cli index_repository '{"repo_path":"..."}'`，本 repo 內建 `.codebase-memory/graph.db.zst` 壓縮快照（4.3 MB），會自動解壓＋增量同步，**不必等它重掃 22,000 個節點**。
3. **實際呼叫 API**：仍需在 **Windows** 環境用 `Source_code/CapitalAPI_2.13.59_CExample/元件/x64/install.bat`（或 x86 版）以管理員身分註冊 SKCOM 元件，並持有群益證券／期貨帳號＋下單憑證。這一點 .57 與 .59 完全相同，不是本規格庫可以繞過的門檻。

### 規格庫本身的改動

- **新增** `api_spec/changelog_2.13.57_to_2.13.59.md`（463 行）：完整差異分析、24 項風險、13 步升級指南、11 檔規格待更新清單、33 個開放問題、54 筆變更總表。
- **新增** `api_spec/_raw/v2.13.59/`：V2.13.59 版官方 19 份 docx 手冊全文抽取。
- **保留** `api_spec/_raw/*.md`（平面檔，V2.13.57 版）：規格檔中所有既有 `_raw/<檔>.md:行號` 引用的對應對象，做為版本歷程證據。
- **升版** 11 個規格檔：`api_spec/modules/*.md`（6 檔）、`api_spec/flows/*.md`（4 檔）、`api_spec/error_codes.md`；每檔經 opus 撰寫→opus 複核→sonnet 引用檢查→opus 跨檔一致性四階段（+642 行 / −139 行；34 個代理、6 項跨檔不一致均已修正）。
- **升版** `tools/extract_docx.py`：加 `--tree` / `--out`，預設抽取版本最高的 `CapitalAPI_*_CExample` 到 `api_spec/_raw/v<版本>/`；.57 平面檔不覆寫。
- **更新** `README.md`、`CLAUDE.md`、`api_spec/README.md`：版本基準改為 V2.13.59、目錄樹加 `_raw/v2.13.59/` 與 changelog 位置。
- **新增** `.codebase-memory/graph.db.zst`（4.3 MB，包含 22,309 節點／41,551 邊）：clone 後免重掃。
- **新增** `Source_code/CapitalAPI_2.13.59_CExample/`（102 MB）：官方 V2.13.59 完整範例包。
- **.gitignore** 排除 `fubon_api/`（24 MB 未解壓封包，與群益 CapitalAPI 無關）。

### 已知問題與限制

- 本次比對僅涵蓋符號名稱集合，未驗證既有 COM 方法的參數型別／vtable 位置——若有未公開的中段插入，`objdump`／`strings` 無法察覺；需 ildasm／tlbexp 級的簽名比對才能保證 binary 相容。目前無跡象顯示存在此情況。
- **未公開的介面**（有 DLL 符號、無官方文件、無範例）：`SKQuoteLib_ExportStockList`、`GetOFOpenInterestWithDetails`＋兩個 `*WithDetails` 事件（11 個符號）。規格庫已在各自模組加註「勿在正式程式使用」。
- 手冊 4-4-35 誤植不存在的 `RequestLiveKLine`——即時分K 的訂閱入口是 `SKQuoteLib_RequestTicks`，勿誤用只推 `OnNotifyTicksLONG` 的 `SKQuoteLib_RequestLiveTick`。
- `OnNotifyLiveKLineData` 的 `nType=0`（清盤通知，本筆價格全 0）自建分K 累積器必須處理，官方範例未示範。
- 官方即時分K 範例把價格寫死 `/100m`，與同檔 `Math.Pow(10, sDecimal)` 慣例矛盾；非兩位小數商品照抄會顯示錯。應以 `SKSTOCKLONG.sDecimal` 還原。
- 33 個仍需向群益或實機確認的開放問題見差異報告 §8。

### 方法學說明

本次 V2.13.57 → V2.13.59 差異分析為多代理工作流產出：

1. **差異偵測**：13 個 sonnet／opus 讀取代理平行讀 19 個原始碼 diff＋19 份手冊 diff＋DLL 符號差集＝119 筆原始 findings。
2. **合併**：opus xhigh 合併去重為 54 筆正典變更。
3. **對抗驗證**：每筆變更由「規格文件／原始碼與 DLL／開發者影響」三個對抗式代理各自試圖反駁；≥2 票反駁則淘汰、多數投票決定欄位修正。淘汰 1 筆（C-38 `SKQuoteLib_DeltaT→Delta` 為 `strings` 抽取假象）。
4. **分區＋總裁決**：opus xhigh 依 area 分區裁決，再由總裁決整合。
5. **完整性稽核**：opus 讀全部存活變更，抓出漏掉的面向；本次抓到 C-39（`SKOOQuoteLib_*WWW`）也是 `strings` 假象，以 `objdump -p` PE Export Directory 複驗後撤銷。

規格庫升版另跑一次 34 個代理的 pipeline（撰寫→複核→引用檢查×11 檔，最後跨檔一致性），共 +642／−139 行。

**證據等級**（適用於本 changelog 全部宣稱）：`objdump` PE 匯出表 ＞ Interop.SKCOMLib 符號 ＞ 官方主手冊版本歷程表 ＞ 分冊文件 ＞ 範例原始碼 ＞ `strings` 掃描（不採信）。

---

## 歷史里程碑（規格庫本身）

- **2026-07-05** `4faca9c` 公開前整理：移除個人 `.claude` 環境設定、加 LICENSE、README／CLAUDE 去除本機路徑
- **2026-07-04** `c5d5df6` AI 可讀規格庫完成：6 模組規格＋4 開發流程＋錯誤碼表（多代理生成＋對抗驗證）
- **2026-07-04** `01eab54` 換裝 V2.13.57 完整包（補海期／海選報價）＋ docx→md 抽取器＋ 21 份原文
- **2026-07-04** `038f473` 匯入群益 CapitalAPI V2.13.58 C# 範例包（實為 .57）
