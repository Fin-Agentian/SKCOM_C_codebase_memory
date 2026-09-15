#!/usr/bin/env python3
"""群益 CapitalAPI docx 手冊 → Markdown 純文字抽取器。

用法：python3 tools/extract_docx.py [--tree CapitalAPI_2.13.59_CExample] [--out api_spec/_raw/v2.13.59]
輸入：Source_code/<tree>/**/*.docx（預設取版本最高的 CapitalAPI_*_CExample）
輸出：<out>/<檔名>.md（預設 api_spec/_raw/v<版本>/；段落 + Markdown 表格，供 AI/人閱讀與後續規格結構化）

註：api_spec/_raw/*.md 平面檔是 V2.13.57 基準（modules/flows 以 `_raw/<檔>.md:行號` 引用，勿覆寫）；
    之後每個版本各放一個 v<版本>/ 子目錄。
"""
import re
import zipfile
from pathlib import Path
from xml.etree import ElementTree as ET

W = "{http://schemas.openxmlformats.org/wordprocessingml/2006/main}"
MC = "{http://schemas.openxmlformats.org/markup-compatibility/2006}"

ROOT = Path(__file__).resolve().parent.parent
SRC = ROOT / "Source_code"
OUT = ROOT / "api_spec" / "_raw"


def strip_fallbacks(el):
    """移除 mc:Fallback，避免 AlternateContent（如文字方塊）內容重複抽出。"""
    for parent in el.iter():
        for child in list(parent):
            if child.tag == f"{MC}Fallback":
                parent.remove(child)


def para_text(p):
    return "".join(t.text or "" for t in p.iter(f"{W}t"))


def para_style(p):
    ppr = p.find(f"{W}pPr")
    if ppr is not None:
        st = ppr.find(f"{W}pStyle")
        if st is not None:
            return st.get(f"{W}val", "")
    return ""


def heading_prefix(style):
    m = re.match(r"(?:Heading|標題)\s*([1-6])", style or "")
    return "#" * (int(m.group(1)) + 1) + " " if m else ""


def cell_text(tc):
    parts = [para_text(p).strip() for p in tc.findall(f"{W}p")]
    return " / ".join(x for x in parts if x).replace("|", "\\|")


def table_md(tbl):
    lines = []
    rows = tbl.findall(f"{W}tr")
    for i, tr in enumerate(rows):
        cells = [cell_text(tc) for tc in tr.findall(f"{W}tc")]
        lines.append("| " + " | ".join(cells) + " |")
        if i == 0:
            lines.append("|" + "---|" * len(cells))
    return lines


def extract(docx_path):
    z = zipfile.ZipFile(docx_path)
    root = ET.fromstring(z.read("word/document.xml"))
    strip_fallbacks(root)
    body = root.find(f"{W}body")
    out = []
    for child in body:
        tag = child.tag
        if tag == f"{W}p":
            text = para_text(child).strip()
            if text:
                out.append(heading_prefix(para_style(child)) + text)
        elif tag == f"{W}tbl":
            out.extend(table_md(child))
            out.append("")
    return "\n".join(out) + "\n"


def tree_version(tree):
    m = re.search(r"CapitalAPI_([\d.]+)_CExample", tree.name)
    return m.group(1) if m else tree.name


def find_default_tree():
    trees = [p for p in SRC.glob("CapitalAPI_*_CExample") if p.is_dir()]
    if not trees:
        raise SystemExit("Source_code/ 下找不到 CapitalAPI_*_CExample")
    return max(trees, key=lambda p: tuple(int(x) for x in tree_version(p).split(".")))


def main():
    import argparse
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--tree", help="Source_code 下的範例包目錄名或路徑（預設：版本最高者）")
    ap.add_argument("--out", help="輸出目錄（預設：api_spec/_raw/v<版本>/）")
    a = ap.parse_args()
    if a.tree:
        tree = Path(a.tree) if Path(a.tree).is_dir() else SRC / a.tree
    else:
        tree = find_default_tree()
    out = Path(a.out) if a.out else OUT / f"v{tree_version(tree)}"
    out.mkdir(parents=True, exist_ok=True)
    print(f"來源 {tree.relative_to(ROOT)} → {out.relative_to(ROOT)}/")
    for docx in sorted(tree.rglob("*.docx")):
        if "~$" in docx.name:  # Word 暫存檔
            continue
        md = out / (docx.stem + ".md")
        content = f"# {docx.stem}\n\n> 來源：{docx.relative_to(ROOT)}\n\n" + extract(docx)
        md.write_text(content, encoding="utf-8")
        print(f"{len(content.splitlines()):6d} 行  {md.name}")


if __name__ == "__main__":
    main()
