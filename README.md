# SKCOM — 群益 CapitalAPI 的 AI 可讀規格庫

[![CI](https://github.com/chengyangtu/SKCOM_C_codebase_memory/actions/workflows/ci.yml/badge.svg)](https://github.com/chengyangtu/SKCOM_C_codebase_memory/actions/workflows/ci.yml)

把群益證券/期貨 CapitalAPI（策略王 COM 元件，**v2.13.59**；以 v2.13.57 為底增補）的官方 docx 手冊與官方範例碼，整理成 AI 可直接消化的結構化規格＋預建知識圖譜——不用再把 400 頁 Word 檔一頁頁餵給 AI。

> 版本差異一覽：[`api_spec/changelog_2.13.57_to_2.13.59.md`](api_spec/changelog_2.13.57_to_2.13.59.md)（逐項對照）與 [`CHANGELOG.md`](CHANGELOG.md)（release notes，含破壞性變更）。

> ⚠️ 非官方文件，內容以群益官方公告為準；下單 API 涉及真實金流，AI 生成的程式碼請一律先在模擬環境測試。

## 快速上手

### 方式一：直接餵檔案（零安裝，30 秒）

把對應檔案貼給任何 LLM（ChatGPT / Claude / 本地模型）當 context，直接開問：

| 你想做的事 | 餵這個檔案 |
|---|---|
| 查某個函式怎麼呼叫 | [`api_spec/modules/`](api_spec/modules/) 對應模組（如 `SKOrderLib.md`） |
| 寫完整流程：登入→下單→回報 | [`api_spec/flows/`](api_spec/flows/) 四條端到端流程 |
| 看懂錯誤碼 | [`api_spec/error_codes.md`](api_spec/error_codes.md)（226 筆對照） |
| 從零開始不知道讀什麼 | [`api_spec/README.md`](api_spec/README.md) 規格庫入口 |

### 方式二：查詢預建知識圖譜（給 AI agent 用，免重新索引）

repo 內建 [codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp) 的圖譜快照（11,700+ 節點、21,000+ 邊），規格 Section 與官方範例 Method 雙軌可查——一次查詢同時拿到函式簽名、規格說明與真實呼叫範例：

```bash
# 安裝 codebase-memory-mcp 後，指到本 repo：
codebase-memory-mcp cli index_repository '{"repo_path":"/path/to/SKCOM_C_codebase_memory"}'
# 偵測到 .codebase-memory/graph.db.zst 會直接還原快照，不會整個重新掃描
```

之後即可用 `search_graph` / `trace_path` / `get_code_snippet` 查詢。搭配各家 AI 工具的細節見 [docs/USAGE_AI.md](docs/USAGE_AI.md)。

## Repo 結構

```
api_spec/            ★ 核心交付：AI 可讀規格庫
├── modules/         6 個 COM lib 完整規格（382 節；每方法含簽名/參數表/回傳/版本陷阱/範例碼行號）
├── flows/           登入、期選下單、報價訂閱、回報解析（Mermaid 時序圖 + 最小 C# 骨架）
├── error_codes.md   226 筆錯誤/回傳/登入代碼對照
├── _raw/            官方手冊逐字全文（V2.13.57 平面檔 21 份 + v2.13.59/ 19 份）
└── changelog_2.13.57_to_2.13.59.md   兩版逐項差異對照
Source_code/         群益官方 C#/C++ 範例專案（圖譜交叉引用用）
.codebase-memory/    預建知識圖譜快照（clone 即用）
tools/               docx → Markdown 抽取工具（純標準庫）
docs/                AI 工具使用指南
```

品質把關：每個模組規格經「生成 agent＋獨立對抗驗證 agent」雙重處理；已知未盡完善處記錄於各 `modules/*.md` 的「陷阱與注意」節。

## 授權

- 本專案原創內容（`api_spec/` 規格文字、`tools/`）：[MIT](LICENSE)
- `Source_code/` 與 `api_spec/_raw/`：著作權歸群益期貨所有，收錄僅供技術參考（詳見 [NOTICE.md](NOTICE.md)）；如群益官方要求移除，將立即執行

## 貢獻與安全

- 歡迎規格勘誤、補充與工具改善 → [CONTRIBUTING.md](CONTRIBUTING.md)（如何附出處、重跑抽取、重建圖譜快照）
- 行為準則 → [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md)；安全或敏感問題私下回報 → [SECURITY.md](SECURITY.md)
- `main` 分支受 branch protection：變更需經 PR 且 CI 通過；CI 採最小權限（`contents: read`、全程零 secrets、第三方 action 釘選 commit SHA）
