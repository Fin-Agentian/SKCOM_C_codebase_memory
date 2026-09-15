# SKCOM — 群益 CapitalAPI 的 AI 可讀規格庫

群益期貨 CapitalAPI（策略王 COM 元件，**v2.13.59**；以 v2.13.57 為底增補）規格庫，供 AI 輔助開發使用。

- 規格庫入口：`api_spec/README.md`；原文：`api_spec/_raw/`（.57 平面檔為既有引用基準）與 `api_spec/_raw/v2.13.59/`（.59 版）；`tools/extract_docx.py` 可重抽
- 版本差異：詳表見 `api_spec/changelog_2.13.57_to_2.13.59.md`、release notes 見 `CHANGELOG.md`；.59 官方文件下架海期／海選報價章節與範例，但 COM API 本身完好——直接呼叫 COM 者不受影響，用簡化版 `SKDLLCSharp.dll` 者請看 CHANGELOG 的破壞性變更說明
- API 查詢優先序：codebase-memory 圖譜（`search_graph`，規格 Section＋官方範例 Method 雙軌可查）→ `api_spec/modules/*.md` → `api_spec/flows/*.md` → `api_spec/_raw/`
- 下單相關操作涉及真實金流，任何生成的程式碼先在模擬環境測試
- 本庫非官方文件，內容以群益期貨官方公告為準；已知未盡完善之處記錄於各 `modules/*.md` 的「陷阱與注意」節
