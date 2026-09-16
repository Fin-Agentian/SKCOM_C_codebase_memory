# SKCOM API 錯誤碼／回傳碼／登入代碼對照表

> **關於「登入代碼」原始文件不全的警告**
> `api_spec/_raw/策略王COM元件使用說明_V2.13.45以上登入代碼定義.md` 原始 docx 抽取結果**只有 26 行**，內容僅含兩個小表：①V2.13.45(含)以上「AP/APH 群組專用」登入代碼（156~158，共 3 碼）②V2.13.45 版「密碼平台回覆」登入失敗代碼（共 9 碼：101/300/306/307/502/507/511/600/604）。此文件**不含**一般 SK_ERROR / SK_WARNING / SK_SUBJECT 完整代碼表，且密碼平台代碼還缺 321、602、603（`api_spec/modules/SKCenterLib.md:311` 已註記此缺漏）。推測原文件在該處以圖片/ 分頁表格呈現，docx 轉文字時遺漏，並非本表整理疏漏。
> 本表已改以下方「資料來源與方法」所列的完整表格重新核對、補齊。

## 資料來源與方法

以下表格出處欄使用簡稱，對照如下：

| 簡稱 | 檔案 | 位置 |
|---|---|---|
| **主表** | `api_spec/_raw/策略王COM元件使用說明_V2.13.57.md` | 第 6 節「代碼定義表」，行 5414–5678（V2.13.57，最新、最完整的單一總表，含 Proxy Server 及密碼平台代碼） |
| **主表(V2.13.59)** | `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md` | 第 6 節「代碼定義表」，行 4698–4934（V2.13.59 版總表，本次升版的比對基準；與 V2.13.57 同節逐行比對，**唯一差異是新增 9996 一列**） |
| **導覽(V2.13.59)** | `api_spec/_raw/v2.13.59/2.導覽.md` | 「行情錯誤代碼定義表」行 91–140；版本控管變更記錄行 171–172（2.13.58／2.13.59 官方 changelog 兩列） |
| **登入(V2.13.59)** | `api_spec/_raw/v2.13.59/3.登入.md` | 「錯誤代碼定義表」行 314–350（此分冊表**未收錄 9996**）；V2.13.45 密碼平台回覆代碼行 352–366 |
| **登入** | `api_spec/_raw/3.登入.md` | 「錯誤代碼定義表」，行 314–366 |
| **下單** | `api_spec/_raw/4.下單準備介紹.md` | 「錯誤代碼定義表」行 746–860；「Proxy Server 下單錯誤代碼」行 862–880 |
| **導覽** | `api_spec/_raw/2.導覽.md` | 「行情錯誤代碼定義表」行 219–265；版本控管變更記錄 |
| **回報** | `api_spec/_raw/12.回報.md` | 「錯誤代碼定義表」行 537–543 |
| **SGX** | `api_spec/_raw/16.SGX_DMA專線.md` | 「代碼定義表」行 554–564 |
| **登入節錄** | `api_spec/_raw/策略王COM元件使用說明_V2.13.45以上登入代碼定義.md` | 全文（僅 26 行，不全，見上方警告） |
| **SKCenterLib模組** | `api_spec/modules/SKCenterLib.md` | 既有可信規格檔的補充脈絡／陷阱說明 |

方法：以**主表**（V2.13.57 版章節 6）為主幹全文抄錄，逐碼與登入／下單／導覽／回報／SGX／登入節錄等分冊文件（`grep -n "代碼\|錯誤\|SK_ERROR\|回傳值"` 交叉核對）比對是否一致；不一致或僅單一來源獨有處於「說明」欄註記。凡代碼在主表中為空白列（僅代碼、常數與說明皆缺）者，標記為「已停用／移除」並附上版本異動記錄可考的舊名稱。

版本基準：**V2.13.59**（以 V2.13.57 規格為底增補；差異見 `api_spec/changelog_2.13.57_to_2.13.59.md`）。原 V2.13.57 基準說明保留備考：`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md` 版本控管記錄至 2025/04/14 V2.13.53 為止。

V2.13.59 比對來源與結論：以 `api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4698-4934`（第 6 節代碼定義表）逐行比對 V2.13.57 同節，**唯一差異是新增 9996 SK_ERROR_UPDATE_API_REQUIRED**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4893`），其餘 1000–1129／2001–2030／3001–3033／4001／5001–5019 全部逐碼相同。官方版本控管記錄已延伸至 2026/03/09 V2.13.58 與 2026/08/03 V2.13.59 兩列（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:37-38`、`api_spec/_raw/v2.13.59/2.導覽.md:171-172`），本檔本次的行為註記皆以該兩列為依據。

註：V2.13.59 官方文件下架了數個章節（主手冊 3-3 行情連線限制、4-5／4-6 海期海選報價章、`14.海期報價`／`15.海選報價` 兩份分冊），但相關**代碼未被移除**（2015／2025／2026／3030 皆仍列於 .59 主表）。本檔凡引用已下架章節作為說明來源者，均在該列就地標明「來源 V2.13.57，.59 已刪除該節」。

---

## 一、下單／系統一般錯誤碼 SK_ERROR（1000–1129）

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 1000 | SK_ERROR_LOGIN_FIRST | 請先由 Center 進行登入動作。＊請注意登入帳號是否為大寫 | 主表; 登入; 下單 |
| 1001 | SK_ERROR_INITIALIZE_FAIL | 登入失敗，請由 LOG 查看失敗原因。 | 主表; 登入; 下單 |
| 1002 | SK_ERROR_ACCOUNT_NOT_EXIST | 交易帳號不存在。 | 主表; 下單 |
| 1003 | SK_ERROR_ACCOUNT_MARKET_NOT_MATCH | 下單帳號類型不正確，是否有證券帳號下期貨之情形。 | 主表; 下單 |
| 1004 | SK_ERROR_PERIOD_OUT_OF_RANGE | PERIOD 超出選擇範圍。 | 主表; 下單 |
| 1005 | SK_ERROR_FLAG_OUT_OF_RANGE | FLAG 超出選擇範圍。 | 主表; 下單 |
| 1006 | SK_ERROR_BUYSELL_OUT_OF_RANGE | BUYSELL 超出選擇範圍。 | 主表; 下單 |
| 1007 | SK_ERROR_ORDER_SERVER_INVALID | 下單主機不存在。 | 主表; 下單 |
| 1008 | （已停用／移除） | V2.13.35 曾定義為 `SK_ERROR_CERT_IS_EXPIRED_OR_INVALID`（憑證過期或無效），V2.13.38 起自文件刪除（見導覽.md 版本控管 2.13.35／2.13.38 記錄）。 | 主表(空白列); 導覽 |
| 1009 | SK_ERROR_TRADE_TYPR_OUT_OF_RANGE | TRADE TYPE 超出選擇範圍。（原文常數拼字如此，TYPR 非 TYPE） | 主表; 下單 |
| 1010 | SK_ERROR_DAY_TRADE_OUT_OF_RANGE | DAY TRADE 超出選擇範圍。 | 主表; 下單 |
| 1011 | SK_ERROR_ORDER_SIGN_INVALID | 文件未附文字說明；依 `4.下單準備介紹.md:111` 前後文，指送出委託前未完成憑證讀取／驗章（ReadCertByID）。 | 主表; 下單 |
| 1012 | SK_ERROR_NEW_CLOSE_OUT_OF_RANGE | NEW CLOSE 超出選擇範圍。 | 主表; 下單 |
| 1013 | SK_ERROR_PRODUCT_INVALID | 下單商品不存在。 | 主表; 下單 |
| 1014 | SK_ERROR_QTY_INVALID | 單量錯誤，請確認是否為數字。 | 主表; 下單 |
| 1015 | SK_ERROR_DAYTRADE_DENIED | 商品不支援當沖。 | 主表; 下單 |
| 1016 | SK_ERROR_SPCIAL_TRADE_TYPE_INVALID | 商品不支援該下單類型。（原文常數拼字如此，SPCIAL 非 SPECIAL） | 主表; 下單 |
| 1017 | SK_ERROR_PRICE_INVALID | 下單價格錯誤。 | 主表; 下單 |
| 1018 | SK_ERROR_INDEX_OUT_OF_RANGE | INDEX 超出選擇範圍。 | 主表; 下單 |
| 1019 | SK_ERROR_QUERY_IN_PROCESSING | 查詢尚未結束請稍後再試。 | 主表; 下單 |
| 1020 | SK_ERROR_LOGIN_INVALID | 文件未附文字說明。 | 主表; 登入 |
| 1021 | SK_ERROR_REGISTER_CALLBACK | 文件未附文字說明。 | 主表; 登入 |
| 1022 | SK_ERROR_FUNCTION_PERMISSION_DENIED | 文件未附文字說明。 | 主表; 下單 |
| 1023 | SK_ERROR_MARKET_OUT_OF_RANGE | MARKET 超出選擇範圍。**V2.13.58 修正**：官方修正「證券智慧單 CB 單 SendStockStrategyCB 發生 SK_ERROR_MARKET_OUT_OF_RANGE 問題」（`api_spec/_raw/v2.13.59/2.導覽.md:171` 功能修正第 1 項）。文件未載明原本哪些商品／市場代碼會誤觸發；若舊程式把「收到 1023」當成跳過或重試的 workaround，升級後該筆委託會真的送出，**升級前務必在模擬環境複測 SendStockStrategyCB 的錯誤分支**。 | 主表; 下單; 導覽(V2.13.59) |
| 1024 | （已停用／移除） | V2.13.35 曾定義為 `SK_ERROR_VERIFY_STAMP_BY_CERT_IS_FAIL`（憑證驗章失敗），V2.13.38 起自文件刪除。 | 主表(空白列); 導覽 |
| 1025 | SK_ERROR_FOREIGNSTOCK_PRICE_OUT_OF_RANGE | 文件未附文字說明；依常數名為複委託下單價格超出範圍。 | 主表; 下單 |
| 1026 | SK_ERROR_FOREIGNSTOCK_UNDEFINE_COINTYPE | 文件未附文字說明；依常數名為複委託幣別未定義。 | 主表; 下單 |
| 1027 | SK_ERROR_FOREIGNSTOCK_SAME_COINSTYPE | 文件未附文字說明。 | 主表; 下單 |
| 1028 | SK_ERROR_FOREIGNSTOCK_SALE_SHOULD_ORIGINAL_COIN | 文件未附文字說明；依常數名為複委託賣出應使用原幣別。 | 主表; 下單 |
| 1029 | SK_ERROR_FOREIGNSTOCK_TRADE_UNIT_INVALID | 文件未附文字說明。 | 主表; 下單 |
| 1030 | SK_ERROR_FOREIGNSTOCK_STOCKNO_INVALID | 文件未附文字說明。 | 主表; 下單 |
| 1031 | SK_ERROR_FOREIGNSTOCK_ACCOUNTTYPE_INVALID | 文件未附文字說明。 | 主表; 下單 |
| 1032 | SK_ERROR_FOREIGNSTOCK_INITIALIZE_FAIL | 文件未附文字說明。 | 主表; 下單 |
| 1033 | SK_ERROR_TS_INITIALIZE_FAIL | 文件未附文字說明。 | 主表; 下單 |
| 1034 | SK_ERROR_OVERSEA_TRADE_PRODUCT_FAIL | 文件未附文字說明。 | 主表; 下單 |
| 1035 | SK_ERROR_OVERSEA_TRADE_DATA_NOT_COMPLETE | 海期交易商品尚未初始完成，請先執行 SKOrderLib_LoadOSCommodity。 | 主表; 下單 |
| 1036 | SK_ERROR_CERT_VERIFY_CN_INVALID | 憑證 CN 錯誤。 | 主表; 下單 |
| 1037 | SK_ERROR_CERT_VERIFY_SERVER_REJECT | 憑證驗章失敗，請確認憑證有效性。 | 主表; 下單 |
| 1038 | SK_ERROR_CERT_NOT_VERIFIED | 憑證尚未驗章，請先執行 ReadCertByID。 | 主表; 下單 |
| 1039 | SK_ERROR_OVERSEA_ACCOUNT_NOT_EXIST | 海期帳號不存在。 | 主表; 下單 |
| 1040 | SK_ERROR_ORDER_LOCK | 下單上鎖，請先執行 UnlockOrder 進行解鎖。 | 主表; 下單 |
| 1041 | SK_ERROR_SERVER_NOT_CONNECTED | 與主機尚未連線。 | 主表; 下單 |
| 1042 | SK_ERROR_OVERSEA_KLINE_DATA_TYPE_NOT_FOUND | KLINE TYPE 超出選擇範圍。 | 主表; 下單 |
| 1043 | SK_ERROR_STRIKEPRICE_INVALID | 履約價不正確。 | 主表; 下單 |
| 1044 | SK_ERROR_CALLPUT_INVALID | CALL PUT 不正確。 | 主表; 下單 |
| 1045 | SK_ERROR_CERT_NOT_FOUND | 憑證不存在，請先匯入憑證到 IE 中。 | 主表; 下單 |
| 1046 | SK_ERROR_RESERVED_OUT_OF_RANGE | 委託盤別超出範圍。 | 主表; 下單 |
| 1047 | SK_ERROR_SMART_TRADE_ORDER_SERVER_INVALID | 智慧單中台主機不存在。 | 主表; 下單 |
| 1048 | SK_ERROR_CANCEL_ORDER_SMARTKEY_INVALID | 智慧單（停損單）序號為無效值。 | 主表; 下單 |
| 1049 | SK_ERROR_OVERSEA_FUTURE_SPREAD_ORDER | 海期價差委託時（價格為非 0 數值）出現分子填入負號之錯誤。 | 主表; 下單 |
| 1050 | SK_ERROR_MIT_ORDER_EXCLUDE_SPREAD_PRODUCT | 智慧單 MIT 不可委託價差商品。 | 主表; 下單 |
| 1051 | SK_ERROR_SGX_API_ORDER_SEQNO_CONFILICT | SGX API 委託序號重複。 | 主表; 下單; SGX |
| 1052 | SK_ERROR_OVERSEA_FUTURE_SPREAD_ORDER_NO_DAYTRADE | 海期價差委託不可帶當沖。 | 主表; 下單 |
| 1053 | SK_ERROR_SGX_API_LOGON_FAIL | SGX API 登入失敗。 | 主表; 下單; SGX; SKCenterLib模組（OnNotifySGXAPIOrderStatus 事件用值） |
| 1054 | SK_ERROR_MIT_ORDER_MUST_GIVE_TRIGGER_PRICE | MIT 委託內容需含觸發價。 | 主表; 下單 |
| 1055 | （文件未定義，跳號） | 主表與下單.md 皆由 1054 直接跳號至 1056，非移除記錄可考，推測原始編號預留未使用。 | 主表; 下單 |
| 1056 | SK_ERROR_STRATEGY_ORDER_MUST_GIVE_DEAL_PRICE | 智慧單委託內容需含成交價。 | 主表; 下單 |
| 1057 | SK_ERROR_WITHDRAW_WITHOUT_PASSWORD | 未輸入出入金密碼。 | 主表; 下單 |
| 1058 | SK_ERROR_WITHDRAWINOUT_TYPE_OUT_OF_RANGE | 出入金互轉類別錯誤。 | 主表; 下單 |
| 1059 | SK_ERROR_CURRENCY_OUT_OF_RANGE | 幣別錯誤。 | 主表; 下單 |
| 1060 | SK_ERROR_AP_GW_SERVER_INVALID | AP GW 主機位址錯誤。 | 主表; 下單 |
| 1061 | SK_ERROR_STRATEGY_ORDER_BUYSELL_INVALID | 證券智慧單買賣別錯誤。 | 主表; 下單 |
| 1062 | SK_ERROR_STRATEGY_ORDER_SMARTKEY_TYPE_INVALID | 證券智慧單號錯誤。 | 主表; 下單 |
| 1063 | SK_ERROR_STRATEGY_ORDER_MARKET_TYPE_INVALID | 證券智慧單市場代碼錯誤。 | 主表; 下單 |
| 1064 | SK_ERROR_STRATEGY_ORDER_TRADE_TYPE_INVALID | 證券智慧單單別錯誤。 | 主表; 下單 |
| 1065 | SK_ERROR_CORRECT_PRICE_ONLY_LMT_RANGE | 海期改價僅限限價單委託。 | 主表; 下單 |
| 1066 | SK_ERROR_TF_OFFSET_COMMODITY_OUT_OF_RANGE | 期貨互抵商品別無效（限定為 0 或 1）。 | 主表; 下單 |
| 1067 | SK_ERROR_SPECIAL_TRADE_TYPE_OUT_OF_RANGE | （逐筆交易）委託價格類型有誤。 | 主表; 下單 |
| 1068 | SK_ERROR_SPECIAL_TRADE_TYPE_IS_MARKETPRICE_AND_ORDERPRICE_SHOULD_BE_ZERO | （逐筆交易）凡市價單之委託價應為 0。 | 主表; 下單 |
| 1069 | SK_ERROR_MARKET_TYPE_INVALID | 市場別有誤。 | 主表; 下單 |
| 1070 | SK_ERROR_SUB_CERT_DIDNOT_BELONG_TO_LOGINID_AP | 雙因子登入相關／子帳不屬於該 AP 群組帳號。 | 主表; 登入; 下單; SKCenterLib模組 |
| 1071 | SK_ERROR_CORRECT_PRICE_ONLY_LMT_RANGE | 海期改價單委託類型限 LMT。（注意：常數名與 1065 相同，為官方文件原文如此，代碼不同、常數重複命名） | 主表; 下單 |
| 1072 | SK_ERROR_SGX_DMA_IS_NOT_ALLOW_CORRECTPRICE_BYBOOKNO | SGX 專線無書號改價。 | 主表; 下單; SGX |
| 1073 | SK_ERROR_SGX_API_ORDER_SEQNO_ERROR | SGX 專線改價-委託序號錯誤。 | 主表; 下單; SGX |
| 1074 | SK_ERROR_QUERY_AGREEMENT | 商品或功能需要同意書，但同意書狀態未簽署（原文誤植為「簽暑」）。 | 主表; 登入 |
| 1075 | SK_ERROR_ALL_AGREEMENT_SIGNED | 全部同意書已簽署，停止執行查詢。 | 主表; 登入; SKCenterLib模組 |
| 1076 | SK_ERROR_PROFITLOSSGWQUERY_DATE_INVALID | 損益查詢日期有誤。 | 主表; 下單 |
| 1077 | SK_ERROR_PROFITLOSSGWQUERY_BOOKNO_IS_INVALID | 損益查詢書號有誤。 | 主表; 下單 |
| 1078 | SK_ERROR_PROFITLOSSGWQUERY_SEQNO_IS_INVALID | 損益查詢序號有誤。 | 主表; 下單 |
| 1079 | SK_ERROR_LOGIN_WITHOUT_PASSWORD | 登入未輸入個人密碼。 | 主表; 登入 |
| 1080 | SK_ERROR_LOGIN_WITHOUT_LOGINID | 登入未輸入登入 ID 帳號。 | 主表; 登入 |
| 1081 | SK_ERROR_LOGIN_WITHOUT_SETQUOTE | 登入未設定開啟行情功能。 | 主表; 導覽; SKCenterLib模組 |
| 1082 | SK_ERROR_THERE_IS_NO_OVERSEA_FUTURE_ACCOUNT | 登入 ID 查無海期帳號。 | 主表; 下單 |
| 1083 | SK_ERROR_MOVE_POINT_IS_INVALID | 移動點數有誤。 | 主表; 下單 |
| 1084 | SK_ERROR_TRADE_KIND_IS_INVALID | 無效委託類別。 | 主表; 下單 |
| 1085 | SK_ERROR_PRICE_TYPE_IS_INVALID | 無效價格型別。 | 主表; 下單 |
| 1086 | SK_ERROR_STRATEGY_OUTORDER_PRICE_TYPE_INVALID | 無效的出場單委託價格類別。 | 主表; 下單 |
| 1087 | SK_ERROR_DATE_TYPE_IS_INVALID | 無效的日期格式。 | 主表; 下單 |
| 1088 | SK_ERROR_STRATEGY_ORDER_PRIME_TYPE_INVALID | 無效的股票市場類別。 | 主表; 下單 |
| 1089 | SK_ERROR_TRIGGER_PRICE_IS_EQUAL_TO_DEAL | 指定觸發價等於成交價格無法觸發，請改觸發條件。 | 主表; 下單 |
| 1090 | SK_ERROR_SEQNO_IS_INVALID | 委託序號有誤。 | 主表; 下單 |
| 1091 | SK_ERROR_BOOKNO_IS_INVALID | 委託書號有誤。 | 主表; 下單 |
| 1092 | SK_ERROR_ORDER_TYPE_IS_INVALID | 委託別有誤（例如現股等）。 | 主表; 下單 |
| 1093 | SK_ERROR_TRIGGER_DIRECTION_IS_INVALID | 指定觸價方向有誤。 | 主表; 下單 |
| 1094 | SK_ERROR_TRIGGER_BASE_IS_INVALID | 無效觸價基準；或未給觸價基準。 | 主表; 下單 |
| 1095 | SK_ERROR_QUOTE_CONNECT_FIRST | 報價尚未連線，請先連線。 | 主表; 導覽 |
| 1096 | SK_ERROR_TRIGGER_METHOD_IS_INVALID | 選擇之觸發方式有誤。 | 主表; 下單 |
| 1097 | SK_ERROR_TELNET_LOGINSERVER_FAIL | Telnet 登入主機失敗，請確認您的環境（Firewall 及 hosts 等）。 | 主表; 登入; SKCenterLib模組 |
| 1098 | SK_ERROR_TELNET_AGREEMENTSERVER_FAIL | Telnet 同意書查詢主機失敗，請確認您的環境（Firewall 及 hosts 等）。 | 主表; 登入; SKCenterLib模組 |
| 1099 | SK_ERROR_LOGUPLOAD_FAIL | 上傳 LOG 失敗。 | 主表; 登入 |
| 1100 | SK_ERROR_STRATEGYORDER_OUT_CONDICTION_INVALID | 智慧單：未選擇至少一個出場條件。 | 主表; 下單 |
| 1101 | SK_ERROR_CANCEL_ORDER_FIELD_INVALID | 智慧單刪單欄位值無效。 | 主表; 下單 |
| 1102 | SK_ERROR_ORDER_FIELD_INVALID | 智慧單出場條件或設定值無效。 | 主表; 下單 |
| 1103 | SK_ERROR_AP_APH_GENERATEKEY_INVALID_BEFORE_LOGIN | ［群組身份］雙因子登入相關／AP_APH GenerateKey 初始失敗或未在登入前執行 SKCenterLib_GenerateKeyCert。 | 主表; 下單; SKCenterLib模組 |
| 1104 | SK_ERROR_CANNOT_GET_DID_FROM_REGISTRY | 文件未附文字說明。 | 主表; 下單 |
| 1105 | SK_ERROR_PRODUCT_INVALID_IN_SGXTABLE | （SGX DMA 專線用）因專線商品清單查無對應商品，請確認該商品代碼是否有效或洽交易室確認是否可網路交易。V2.13.42 加入補充說明。 | 主表; 下單; SGX |
| 1106 | SK_ERROR_GET_TANDEMINFO_FAIL_SRVKLINE | 無法轉後台商品代碼等資料，可先確認委託商品代碼是否正確。 | 主表; 下單 |
| 1107 | SK_ERROR_LIMIT_NEAR_BY_YEARMONTH | 限制近月商品代碼（例：TX00、MTX00）。 | 主表; 下單 |
| 1108 | SK_ERROR_TELNET_SERVER_FAIL | 因網路異常，無法連線。V2.13.40 新增（當網路異常時，執行國內海外報價、回報連線功能會回傳此碼）。 | 主表; 下單; 導覽 |
| 1109 | SK_ERROR_THERE_IS_NO_TOKEN | 因登入未取得時效 token，請重新登入。 | 主表; 下單 |
| 1110 | SK_ERROR_FOREIGNSTOCK_ORDERTYPE_INVALID | 複委託下單或刪單時，請填入委託類別：買、賣或刪單。 | 主表; 下單 |
| 1111 | SK_ERROR_FOREIGNSTOCK_TRADETYPE_INVALID | 複委託下單，請填入下單庫存類別。 | 主表; 下單 |
| 1112 | SK_ERROR_QUERY_FORMAT_INVALID | 需指定查詢格式（例：新國內未平倉功能 GW 需指定格式）。V2.13.42 新增。 | 主表; 下單 |
| 1121 | SK_ERROR_STRATEGY_ORDER_PRERISK_INVALID | 無效預風控設定。V2.13.51 新增。 | 主表; 導覽 |
| 1124 | SK_ERROR_BEST_QTY_IS_INVALID | MIT 多條件單，內外盤委託量條件時未給內外盤委託量。V2.13.45 新增。 | 主表 |
| 1125 | SK_ERROR_TRIGGER_CONDITION_IS_INVALID | 無效觸價條件；或未給觸價條件。V2.13.45 新增。 | 主表 |
| 1126 | SK_ERROR_EXCHANGE_ID_IS_INVALID | 查無商品交易所代碼。V2.13.49 新增。 | 主表 |
| 1127 | SK_ERROR_QUERY_IS_OVER_TEN_TIMES | 一個 account 在 1 分鐘內不可超過 10 次。V2.13.51 新增（配合昨日均價查詢 GetAvgCost 限流）。 | 主表; 導覽 |
| 1128 | SK_ERROR_CERT_INITIALIZE_FAIL | WebCA 憑證＿初始失敗。 | 主表; 登入 |
| 1129 | SK_ERROR_LOGIN_FAIL_IN_PROCESSING | 登入失敗，請稍等五秒後再試。V2.13.57（含）以上：登入失敗需間隔五秒才能再次嘗試。 | 主表; 登入; SKCenterLib模組 |

## 二、警告碼 SK_WARNING（2001–2030）

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 2001 | SK_WARNING_OF_COM_DATA_MISSING | 海期交易商品檔下載失敗。 | 主表; 下單 |
| 2002 | SK_WARNING_TS_READY | 文件未附文字說明。 | 主表; 導覽 |
| 2003 | SK_WARNING_LOGIN_ALREADY | ID 已登入，無需重複登入。 | 主表; 登入 |
| 2004 | SK_WARNING_LOGIN_SPECIAL_ALREADY | 文件未附文字說明。 | 主表; 登入 |
| 2005 | SK_WARNING_CERT_VERIFIED_ALREADY | 驗章已過，無需重複驗章。 | 主表; 登入 |
| 2006 | SK_WARNING_ORDER_DID_NOT_LOCKED | 下單市場未上鎖。 | 主表; 下單 |
| 2007 | SK_WARNING_OO_COM_QUOTEDATA_MISSING | 海選商品檔下載失敗。 | 主表; 導覽 |
| 2008 | SK_WARNING_OO_COM_ORDERDATA_MISSING | 海選交易商品檔下載失敗。 | 主表; 下單 |
| 2009 | SK_WARNING_SIGN_TS_SMARTTRADE_RISK_NOITICE_FIRST | 提醒簽署證券智慧單風險預告書。 | 主表; 下單 |
| 2010 | SK_WARNING_SIGN_TF_SMARTTRADE_RISK_NOITICE_FIRST | 提醒簽署期貨智慧單風險預告書。 | 主表; 下單 |
| 2011 | （已停用／移除） | V2.13.38 起自文件刪除；原始定義名稱未見於現存版本控管記錄可考。 | 主表(空白列); 導覽 |
| 2012 | SK_WARNING_OSQUOTECENTER_IS_NOT_EXIST | 下單：下載海期／海選元件不存在。 | 主表; 下單 |
| 2013 | SK_WARNING_INITIALIZE_OSQUOTECENTER_CONNECTION_FAIL | 下單：下載海期商品檔元件連線失敗。 | 主表; 下單 |
| 2014 | SK_WARNING_INITIALIZE_OSQUOTECENTER_OO_CONNECTION_FAIL | 下單：下載海選商品檔元件連線失敗。 | 主表; 下單 |
| 2015 | SK_WARNING_DOWNLOAD_OF_COM_DATA_IS_TIMEOUT | 下單：下載海期商品檔未完成。處置：重新連線海期行情主機或重新下載商品檔（`SKOSQuoteLib_EnterMonitorLONG` → 再 `SKOrderLib_LoadOSCommodity`）。＊此處置建議來源為 V2.13.57 分冊 `api_spec/_raw/9.下單-海外期選.md:185,204`，V2.13.59 分冊 9 已刪除該備註（`api_spec/_raw/v2.13.59/9.下單-海外期選.md` 全文查無 2015／「重新下載」字樣），但代碼本身仍在 .59 主表（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4834`），執行期前置條件未變。 | 主表; 下單; 主表(V2.13.59) |
| 2016 | SK_WARNING_DOWNLOAD_OO_COM_DATA_IS_TIMEOUT | 下單：下載海選商品檔未完成。 | 主表; 下單 |
| 2017 | SK_WARNING_REGISTER_REPLYLIB_ONREPLYMESSAGE_FIRST | 請（註冊）接收公告再登入，請參考單元【註冊公告】。登入前需先掛 SKReplyLib.OnReplyMessage 並回傳 -1。 | 主表; 登入; SKCenterLib模組 |
| 2018 | SK_WARNING_SIGN_STOCK_OR_FUTURE_API_AGREEMENT_FIRST | 1.確認為證券或期貨網路戶 2.確認未簽署證券 API 下單聲明書或期貨 API 下單聲明書 3.或無法取得聲明書狀態（例：確認 Internet 設定是否支援 TLS1.2）。 | 主表; 登入; SKCenterLib模組 |
| 2019 | SK_WARNING_SIGN_FUTURE_API_AGREEMENT_FIRST | 1.確認是否為期貨網路戶 2.確認未簽署期貨 API 下單聲明書 3.或無法取得聲明書狀態（例：確認 Internet 設定是否支援 TLS1.2）。 | 主表; 登入; SKCenterLib模組 |
| 2020 | SK_WARNING_PRECHECK_RESULT_FAIL | 取得行情主機或回報主機連線資訊失敗（例：RCode）。 | 主表; 導覽; 回報 |
| 2021 | SK_WARNING_PRECHECK_RESULT_EMPTY | 取得行情主機或回報主機連線資訊結果為空值。 | 主表; 導覽; 回報 |
| 2022 | SK_WARNING_MORDER_STOP_SERVICE | 停止模擬平台服務通知。 | 主表; 登入 |
| 2023 | SK_WARNING_QUOTE_MUST_SKQUOTELIB_ENTERMONITORLONG_FIRST | 請先執行國內報價 SKQuoteLib_EnterMonitorLONG 連線，再使用目前功能。 | 主表; 導覽 |
| 2024 | SK_WARNING_QUOTE_MUST_SKQUOTELIB_ENTERMONITOR_FIRST | 請先執行國內報價 SKQuoteLib_EnterMonitor 連線，再使用目前功能。 | 主表; 導覽 |
| 2025 | SK_WARNING_OSQUOTE_MUST_SKOSQUOTELIB_ENTERMONITORLONG_FIRST | 請先執行海期報價 SKOSQuoteLib_EnterMonitorLONG 連線，再使用目前功能。＊V2.13.59 **仍保留此碼**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4844`、`api_spec/_raw/v2.13.59/2.導覽.md:102`），但對應章節說明已下架（主手冊 4-5 SKOSQuoteLib 整章與 `14.海期報價` 分冊在 .59 已不存在）；規則仍在、說明不見，海期報價規格請查 V2.13.57 留存文件。 | 主表; 導覽; 主表(V2.13.59); 導覽(V2.13.59) |
| 2026 | SK_WARNING_OOQUOTE_MUST_SKOOQUOTELIB_ENTERMONITORLONG_FIRST | 請先執行海選報價 SKOOQuoteLib_EnterMonitorLONG 連線，再使用目前功能。＊同 2025：V2.13.59 仍保留此碼（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4845`、`api_spec/_raw/v2.13.59/2.導覽.md:103`），但主手冊 4-6 SKOOQuoteLib 整章與 `15.海選報價` 分冊已下架。 | 主表; 導覽; 主表(V2.13.59); 導覽(V2.13.59) |
| 2027 | （已停用／移除） | V2.13.35 曾定義為 `SK_WARNING_ACTIVE_CERTIFICATION_FIRST`，V2.13.38 起自文件刪除。 | 主表(空白列); 導覽 |
| 2028 | SK_WARNING_SHOULD_IGNORE_GENERATEKEYCERT_STEP | 雙因子登入相關／目前身份不需執行此功能－雙因子群組 GenerateKey。非群組身份執行 SKCenterLib_GenerateKeyCert 會收到此碼。 | 主表; 登入; SKCenterLib模組 |
| 2029 | SK_WARNING_SHOULD_CHECK_NETWORK | 文件未附文字說明。 | 主表; 登入 |
| 2030 | SK_WARNING_SHOULD_FILLED_YEARMONTH_WHEN_NOT_NEAR_MONTH | （非近月）指定月份商品需填入商品契約年月共 6 碼。 | 主表; 登入 |

## 三、報價／連線通知碼 SK_SUBJECT（3001–3033）與 SK_KLINE（4001）

這組代碼多半不是函式回傳值，而是由 `OnConnection(BSTR bstrUserID, LONG nKind)`（國內／海外報價、回報連線狀態通知事件）的整數參數帶出。

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 3001 | SK_SUBJECT_CONNECTION_CONNECTED | 連線。 | 主表; 導覽 |
| 3002 | SK_SUBJECT_CONNECTION_DISCONNECT | 斷線。 | 主表; 導覽; 回報; SGX |
| 3003 | SK_SUBJECT_CONNECTION_STOCKS_READY | 報價商品載入完成。需等此事件後才可進行商品檔查詢／訂閱。 | 主表; 導覽; 國內報價.md |
| 3004 | SK_SUBJECT_CONNECTION_CLEAR | 文件未附文字說明。 | 主表; 導覽 |
| 3005 | SK_SUBJECT_CONNECTION_RECONNECT | 文件未附文字說明。 | 主表; 導覽 |
| 3006 | SK_SUBJECT_QUOTE_PAGE_EXCEED | PageNo 大於上限。 | 主表; 導覽 |
| 3007 | SK_SUBJECT_QUOTE_PAGE_INCORRECT | 文件未附文字說明。 | 主表; 導覽 |
| 3008 | SK_SUBJECT_TICK_PAGE_EXCEED | Tick PageNo 大於上限。 | 主表; 導覽 |
| 3009 | SK_SUBJECT_TICK_PAGE_INCORRECT | 文件未附文字說明。 | 主表; 導覽 |
| 3010 | SK_SUBJECT_TICK_STOCK_NOT_FOUND | Tick 商品不存在。 | 主表; 導覽 |
| 3011 | SK_SUBJECT_BEST5_DATA_NOT_FOUND | 商品五檔不存在。 | 主表; 導覽 |
| 3012 | SK_SUBJECT_QUOTEREQUEST_NOT_FOUND | 文件未附文字說明。 | 主表; 導覽 |
| 3013 | SK_SUBJECT_SERVER_TIME_NOT_FOUND | 文件未附文字說明。 | 主表; 導覽 |
| 3015 | SK_SUBJECT_ALL_MARKET_OK | 文件未附文字說明。 | 主表; 導覽 |
| 3016 | SK_SUBJECT_MARKET_INFO_READY | 文件未附文字說明。 | 主表; 導覽 |
| 3017 | SK_SUBJECT_CATALOG_LIST_READY | 文件未附文字說明。 | 主表; 導覽 |
| 3018 | SK_SUBJECT_INITIALIZESTOCKS | 文件未附文字說明。 | 主表; 導覽 |
| 3019 | SK_SUBJECT_MACD_DATA_NOT_FOUND | MACD 不存在。 | 主表; 導覽 |
| 3020 | SK_SUBJECT_BOOLTUNEL_DATA_NOT_FOUND | BOOL 通道不存在。 | 主表; 導覽 |
| 3021 | SK_SUBJECT_CONNECTION_FAIL_WITHOUTNETWORK | 連線失敗（網路異常等）。 | 主表; 導覽 |
| 3022 | SK_SUBJECT_CONNECTION_SOLCLIENTAPI_FAIL | Solace 底層連線錯誤。 | 主表; 導覽; 回報 |
| 3023 | SK_SUBJECT_STOCKNO_IS_INVALID | 商品代碼無效。 | 主表; 導覽 |
| 3024 | SK_SUBJECT_MARKET_NO_IS_OUT_OF_RANGE | 國內市場代碼超出範圍（0~4）。 | 主表; 導覽 |
| 3025 | SK_SUBJECT_CANT_ACCEPT_SPREAD_COMMODITY | 不可委託價差商品。 | 主表; 導覽 |
| 3026 | SK_SUBJECT_CONNECTION_SGX_API_READY | SGX API 專線建立完成。 | 主表; 導覽; SGX; SKCenterLib模組（OnNotifySGXAPIOrderStatus 事件用值） |
| 3027 | SK_SUBJECT_TICK_LIMIT_EXCEED | 超過可訂閱 TICK／Best5／Best10 商品檔數。 | 主表; 導覽 |
| 3028 | SK_SUBJECT_QUOTE_LIMIT_EXCEED_IN_ONE_PAGE | 超過可訂閱單頁即時報價商品檔數。 | 主表; 導覽 |
| 3029 | SK_SUBJECT_QUOTE_STRING_EXIST_NULL | 查詢即時報價含空白、空值。 | 主表; 導覽 |
| 3030 | SK_SUBJECT_NO_QUOTE_SUBSCRIBE | 行情連線超過限制時，無法訂閱行情通知。一個 ID 預設最多 2 條行情連線（國內共用 1 條、海外期選 1 條）——＊「2 條」的細節來源為 **V2.13.57 主手冊 3-3 行情物件連線限制說明**（`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md:154-166`），**V2.13.59 已刪除該節**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:154` 由 3-2 直接跳到「4、ATL物件函式說明」）；.59 主表僅存一句「行情連線超過限制時，無法訂閱行情通知」（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4878`、`api_spec/_raw/v2.13.59/2.導覽.md:132`）。代碼與執行期限制未變，僅官方說明下架。 | 主表; 導覽; SKCenterLib模組; 主表(V2.13.59); 導覽(V2.13.59) |
| 3031 | SK_SUBJECT_NO_RELATED_MARKET_STOCKS | 未下載相關市場商品基本資料（原因：可確認證券或期貨下單聲明書簽署狀態）。 | 主表; 導覽; 國內報價.md |
| 3032 | SK_SUBJECT_INITIALIZESTOCKS_FAIL | 文件未附文字說明。 | 主表; 導覽 |
| 3033 | SK_SUBJECT_SOLACE_SESSION_EVENT_ERROR | Solace Session down 錯誤（因 AP 與主機連線異常，由主機端主動斷線）。 | 主表; 導覽; 回報 |
| 4001 | SK_KLINE_DATA_TYPE_NOT_FOUND | KLINE TYPE 超出選擇範圍。 | 主表; 導覽 |

## 四、系統層級碼（9996–9999）

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 9996 | SK_ERROR_UPDATE_API_REQUIRED | 此版本已無法登入，請更新版本。（**V2.13.59 新增**，官方 changelog 原文「新增錯誤代碼9996 SK_ERROR_UPDATE_API_REQUIRED此版本已無法登入，請更新版本」，`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:38`）＊**終止性錯誤**：代表所用 SKCOM 元件版本已被群益停用，須更新元件版本，程式端重試無效；登入錯誤處理務必把 9996 與 9997 分流，自動重試白名單不得包含 9996。＊備註：此碼只見於 .59 主手冊第 6 章代碼表（`…_V2.13.59.md:4893`）與 `api_spec/_raw/v2.13.59/2.導覽.md:137`，**分冊 `3.登入.md` 的「錯誤代碼定義表」（行 314–350）未收錄**，僅列到 9997（`api_spec/_raw/v2.13.59/3.登入.md:350`）——查分冊者會漏掉此碼。 | 主表(V2.13.59); 導覽(V2.13.59) |
| 9997 | SK_ERROR_LOGIN_FAIL_LIMIT | 登入失敗已達五次，請重啟 API。 | 主表; 登入; SKCenterLib模組 |
| 9998 | SK_THIS_FUNCTION_NOT_SUPPORTED | 此函式目前不提供。 | 主表; 下單 |
| 9999 | SK_FAIL | ＊報價部分：若您未開立證券或期貨帳戶，無法訂閱或取得相關市場商品資料；若您未簽署證券 API 下單聲明書或期貨 API 下單聲明書，將無法訂閱或取得相關市場商品資料。V2.13.53 版本控管記錄另提到「APH 執行 SKOrderLib_Initialize 出現 SK_FAIL」之修正案例，可見本碼亦見於下單初始化情境，非僅報價專用。 | 主表; 導覽 |

## 五、登入代碼｜舊版與特殊代碼（100–199、500–599）

僅適用特定版本區間，V2.13.45（含）以上改採第六節的密碼平台代碼（見下節）。

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 151 | SK_ERROR_LOGIN_WRONG_PASSWORD | 僅適用 V2.13.43（含）以下版本／密碼錯誤。 | 主表; 登入 |
| 152 | SK_ERROR_LOGIN_WRONG_PASSWORD_OVER_LIMIT | 僅適用 V2.13.43（含）以下版本／密碼輸入錯誤次數超過上限，請至群益金融網－密碼專區解鎖。 | 主表; 登入 |
| 153 | SK_ERROR_LOGIN_WRONG_ID | 僅適用 V2.13.43（含）以下版本／您輸入的資料錯誤！請輸入正確的身份證字號。 | 主表; 登入 |
| 155 | SK_ERROR_CHANGE_PASSWORD_IN_FIRST_TIME | 僅適用 V2.13.43（含）以下版本首次使用，請先更改密碼（可使用群益策略王更改）。 | 主表; 登入 |
| 156 | SK_ERROR_LOGIN_NO_SERVER_LIST | 僅適用 V2.13.45（含）以上版本／（GW）未能取得主機清單。 | 主表; 登入; 登入節錄 |
| 157 | SK_ERROR_LOGIN_NO_ACCONTS | 僅適用 V2.13.45（含）以上版本／未能取得交易帳號。（原文常數拼字如此，ACCONTS 非 ACCOUNTS） | 主表; 登入; 登入節錄 |
| 158 | SK_ERROR_LOGIN_NO_SPEC_AUTHORITY | 僅適用 V2.13.45（含）以上版本／未能取得特殊權限（SGX 密碼權限、DMA 等）。 | 主表; 登入; 登入節錄; SGX |
| 500~599（區間） | （未附共用常數，個別代碼未逐一列出） | 說明僅適用 V2.13.43（含）以前版本／請先確認是否安裝「有效憑證」，可至群益金融網→憑證專區確認及申請（`https://www.capital.com.tw/web/#/certificate/ap`）；新申請憑證可能需使用 RAWinApp.exe 開通。 | 主表; 登入 |

## 六、登入代碼｜V2.13.45（含）以上密碼平台回覆代碼

這組代碼由**密碼驗證平台**（非 SKCOM.dll 本身）回覆，記錄在 `Center.log`，**不會**經由 `SKCenterLib_GetReturnCodeMessage` 轉出文字，需自行查閱 LOG 或用 `SKCenterLib_GetLastLogInfo()` 取得。

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 101 | （無正式常數，僅有說明文字：登入 token 異常） | 處理方式：請重新登入。 | 主表; 登入; 登入節錄 |
| 300 | （無正式常數，僅有說明文字：密碼驗證失敗） | 處理方式：您輸入的密碼錯誤，請重新輸入！ | 主表; 登入; 登入節錄; SKCenterLib模組（登入常見問題） |
| 306 | （無正式常數，僅有說明文字：身分證字號錯誤） | 處理方式：您輸入的身份證字號錯誤，請重新輸入！ | 主表; 登入; 登入節錄; SKCenterLib模組（登入常見問題） |
| 307 | （無正式常數，僅有說明文字：密碼被鎖定） | 處理方式：請至群益金融網－密碼專區解鎖。 | 主表; 登入; 登入節錄 |
| 321 | （無正式常數，僅有說明文字：測試未完成） | 說明：請先至 API 下載專區完成測試後再進行下一步。處理方式：至 API 下載專區下載驗證小工具（SKCOMVerifyDJ），完成驗證測試。V2.13.53 版本控管記錄：GW 登入 VerifyDJ 證券期貨都沒做驗證，要收此錯誤碼。**登入節錄.md 缺此碼**（見檔頭警告）。 | 主表; 登入; 導覽 |
| 502 | （無正式常數，僅有說明文字：[特殊身份]帳戶錯誤） | 處理方式：群組身份欲指定子帳錯誤。 | 主表; 登入; 登入節錄 |
| 507 | （無正式常數，僅有說明文字：驗證裝置碼失敗） | 處理方式：無效裝置碼或未綁定裝置碼，可透過策略王綁定。 | 主表; 登入; 登入節錄 |
| 511 | （無正式常數，僅有說明文字：[特殊身份]群組未申請權限） | 處理方式：AP 群組帳號登入異常，請確認是否有申請登入權限。 | 主表; 登入; 登入節錄 |
| 600 | （無正式常數，僅有說明文字：驗證憑證錯誤） | 處理方式：請確認是否安裝「有效憑證」；若電腦同時存在二張憑證，請先刪除無效或過期憑證。 | 主表; 登入; 登入節錄; SKCenterLib模組（登入常見問題） |
| 602 | （無正式常數，僅有說明文字：雙因子登入失敗） | 處理方式：請確認是否安裝登入 ID 有效憑證。**登入節錄.md 缺此碼**（見檔頭警告）。 | 主表; 登入 |
| 603 | （無正式常數，僅有說明文字：雙因子登入失敗，無第二因子） | 處理方式：請確認是否安裝登入 ID 有效憑證。**登入節錄.md 缺此碼**（見檔頭警告）。 | 主表; 登入 |
| 604 | （無正式常數，僅有說明文字：憑證過期或已註銷） | 處理方式：請至群益金融網－憑證專區申請。 | 主表; 登入; 登入節錄 |

## 七、Proxy Server 下單錯誤碼（5001–5019）

由 `OnProxyStatus(BSTR bstrUserId, LONG nCode)` 事件回傳，用於透過 Proxy Server 下單時的連線／登入狀態通知（`SKOrderLib_InitialProxyByID` 後開始收）。

| 代碼 | 常數/名稱 | 說明 | 出處 |
|---|---|---|---|
| 5001 | SK_PROXY_SERVER_LOGIN_SUCCESS | Proxy handler 連線中且狀態為登入成功。 | 主表; 下單 |
| 5002 | SK_PROXY_SERVER_LOGIN_FAIL | Proxy handler 連線中且狀態為登入失敗，請重新連線 ProxyReconnectByID。 | 主表; 下單 |
| 5003 | SK_PROXY_SERVER_DISCONNECT | Proxy handler 斷線中，請重新連線 ProxyReconnectByID。C# 範例（`4.下單準備介紹.md` OnProxyStatus 事件段落）內文將此碼誤植為 `SK_PROXY_SERVER_LOGIN_DISCONNECT`，以主表官方常數 `SK_PROXY_SERVER_DISCONNECT` 為準。 | 主表; 下單 |
| 5004 | SK_PROXY_SERVER_SCHEDULE_DIALY_DISCONNECT | Proxy SERVER 送出每日斷線通知，請等待 1 分鐘後重新連線 ProxyReconnectByID。 | 主表; 下單 |
| 5005 | SK_PROXY_SERVER_SWITCH_MODE | Proxy SERVER 送出切轉主機通知。 | 主表; 下單 |
| 5006 | SK_PROXY_SERVER_HANDLER_IS_EXIST | SKOrderLib_InitialProxyByID 時發生該 ID 連線 HANDLER 已存在，請重新連線 ProxyReconnectByID。C# 範例內文誤植為 `SK_PROXY_SERVER_CONNECTION_IS_EXIST`，以主表官方常數為準。 | 主表; 下單 |
| 5009 | SK_PROXY_SERVER_LOGIN_SEND_DATA_SUCCESS | Proxy handler 已連線且登入，等待 Server 回傳通知 5001。＊請確認收到 5001 再送 proxy 下單。 | 主表; 下單 |
| 5010 | SK_PROXY_SERVER_CONNECT_SUCCESS | Proxy handler 連線中尚未登入，需斷線 ProxyDisconnectByID 成功，再重新連線 ProxyReconnectByID。 | 主表; 下單 |
| 5011 | SK_PROXY_SERVER_CONNECT_FAIL | Proxy handler 連線失敗，請重新連線 ProxyReconnectByID 或初始化 SKOrderLib_InitialProxyByID。 | 主表; 下單 |
| 5012 | SK_PROXY_SERVER_DISCONNECT_FAIL | Proxy handler 斷線失敗。 | 主表; 下單 |
| 5013 | SK_PROXY_LOAD_PROXYLIB_FAIL | 載入 ProxyLIB DLL 失敗，請先確認 Proxy 初始化 SKOrderLib_InitialProxyByID。 | 主表; 下單 |
| 5014 | SK_PROXY_SERVER_CONNECT_FAIL_WITHOUT_HANDLER | Proxy handler 連線失敗，請重新連線 ProxyReconnectByID。 | 主表; 下單 |
| 5015 | SK_PROXY_INITIALIZE_FAIL | Proxy 初始化失敗，請重新連線 ProxyReconnectByID 或初始化 SKOrderLib_InitialProxyByID。 | 主表; 下單 |
| 5016 | SK_PROXY_ERROR_THE_ID_IS_CONNECTED_ALREADY | 重新連線時發生重複連線通知；目前 Proxy handler 連線中且狀態為登入成功。 | 主表; 下單 |
| 5017 | SK_PROXY_ERROR_THE_ID_IS_DISCONNECTED_ALREADY | 斷線時，發生重複斷線通知。 | 主表; 下單 |
| 5018 | SK_PROXY_SEND_ORDER_FAIL | 送出 ProxyOrder 異常，請確認 SKOrderLib event：OnProxyStatus 為 SK_PROXY_SERVER_LOGIN_SUCCESS 並重新連線 ProxyReconnectByID。 | 主表; 下單 |
| 5019 | SK_PROXY_SERVER_ACCOUNT_RESTRICTED | 此帳號已被限制連線，請洽詢營業員。 | 主表; 下單 |

## 八、Microsoft／Windows 系統層級錯誤碼（非 SK 自訂）

當登入、下單、讀取同意書發生的錯誤代碼無法從上列各節代碼定義表確認時，官方文件指向以下 Microsoft 錯誤碼範圍（`api_spec/_raw/3.登入.md:368-375`、`策略王COM元件使用說明_V2.13.57.md` 第 7 節）：

| 範圍 | 說明 | 參考連結 |
|---|---|---|
| ＜997；10004~10112；11001~11031 | Windows Sockets Error Codes（WinSock） | `https://docs.microsoft.com/zh-tw/windows/win32/winsock/windows-sockets-error-codes-2` |
| 12002~12054；12111~12174 | WinINet functions return error codes | `https://docs.microsoft.com/zh-tw/windows/win32/wininet/wininet-errors` |
| （無固定範圍） | Microsoft Windows HTTP error code（WinHTTP） | `https://docs.microsoft.com/zh-tw/windows/win32/winhttp/error-messages` |

備註：`2.導覽.md` 版本控管記錄 V2.13.52（2024/12/19）提到「測試區下單前驗章 12175（SSL 錯誤）」，該代碼略高於文件所列 WinINet 上限 12174，性質相近但未見官方逐碼定義，僅作為已知案例記錄於此。

### Appendix A：註冊 SKCOM.dll（regsvr32）時常見錯誤碼

| 代碼 | 說明 | 處理方式 |
|---|---|---|
| 0x8002801c | DllRegisterServer 呼叫失敗 | 需以 Administrator（系統管理員）身分登入電腦後再註冊 API |
| 0x80070005 | DllRegisterServer 呼叫失敗 | 執行 install.bat 需按右鍵以系統管理員身分執行註冊 API |
| （官方未列代碼） | 註冊失敗：**使用位元不對應的 regsvr32.exe**（官方未把此情形歸入上列兩碼，是《1.環境設置》另行規定的註冊前提） | 32 位元 SKCOM.dll 透過 SysWoW64 的 `regsvr32.exe` 註冊；64 位元 SKCOM.dll 透過 System32 的 `regsvr32.exe` 註冊或直接註冊即可（`api_spec/_raw/v2.13.59/1.環境設置.md:99-100`）。x86／x64 兩包的 install.bat 擇一勿混用，且兩包內容**並不相同**：x64 包直接呼叫 `regsvr32.exe "%~dp0SKCOM.dll"`（`Source_code/CapitalAPI_2.13.59_CExample/元件/x64/install.bat:5`），x86 包則呼叫 `%systemroot%\SysWoW64\regsvr32.exe`（`Source_code/CapitalAPI_2.13.59_CExample/元件/x86/install.bat:14`，僅 Windows XP x86 走直接註冊，同檔 `:7-12`）；註冊後以 `SKCenterLib_GetSKAPIVersionAndBit`（`api_spec/_raw/v2.13.59/3.登入.md:257-261`）確認回傳的「版本＿位元」（文件範例 `2.13.30_x64`）與建置平台一致 |

＊**V2.13.59 文件調整（註冊路徑敘述）**：《1.環境設置》由 V2.13.57 的「x86位元:直接註冊即可／x64位元:透過SysWow64的regsvr32.exe註冊」改寫為上表所列的 SysWoW64／System32 對應（`api_spec/_raw/v2.13.59/1.環境設置.md:99-100`）。但**同一套 V2.13.59 合輯主手冊的附錄 A 仍保留舊字串**（`api_spec/_raw/v2.13.59/策略王COM元件使用說明_V2.13.59.md:4956-4957`），.59 文件組內部不一致，且官方 2.13.58／2.13.59 版本歷程未列此項（無從判定改寫落在哪一版）、V2.13.57 與 V2.13.59 兩版隨附的 install.bat 內容則完全相同（`diff` 驗證，x86／x64 各自沿用同一腳本）。只讀合輯手冊者會拿到舊指引，請以《1.環境設置》與 install.bat 為準，實務判準是「安裝的 SKCOM.dll 位元必須與執行檔平台一致」。

---

## 九、如何在程式中取得錯誤訊息

SKCOM 幾乎所有函式的回傳值都是 `int`（LONG）錯誤碼：`0` 表示成功，其餘對照本檔第一～七節的代碼表。取得代碼對應的文字說明，官方範例一律呼叫 `SKCenterLib_GetReturnCodeMessage`；若代碼表沒有文字說明或需要更多診斷資訊（尤其登入失敗），再輔以 `SKCenterLib_GetLastLogInfo` 取得最後一筆 LOG 內容。

### 9.1 SKCenterLib_GetReturnCodeMessage — 代碼轉文字訊息

- 簽名：`string SKCenterLib_GetReturnCodeMessage(int nCode)`（文件宣告 `BSTR SKCenterLib_GetReturnCodeMessage([in] LONG nCode);`）
- 用法慣例：無論呼叫哪個物件（SKCenterLib／SKOrderLib／SKQuoteLib／SKReplyLib…）的函式，取得其回傳的 `nCode` 後，一律用**同一個** `SKCenterLib` 物件的 `SKCenterLib_GetReturnCodeMessage(nCode)` 轉譯文字（因為代碼表為全域共用，不分物件）。官方範例 SKCOMTesterV2 全專案數百處呼叫皆遵循此固定寫法。

C# 範例（節錄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/MainForm.cs:879-883`）：

```csharp
int nCode = m_pSKCenter.SKCenterLib_Login(UserID, Password);
// 取得回傳訊息
string msg = "【SKCenterLib_Login】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
richTextBoxMethodMessage.AppendText(msg + "\n");
```

C++ 範例（節錄自 `Source_code/CapitalAPI_2.13.57_CExample/CppTester/CppTester/SKCenterLib.cpp:65-67,74-81`）：

```cpp
_bstr_t CSKCenterLib::GetReturnCodeMessage(long nCode)
{
    return m_pSKCenterLib->SKCenterLib_GetReturnCodeMessage(nCode);
}

// 成功／失敗分流，失敗時多印一次 GetLastLogInfo 供除錯
void CSKCenterLib::PrintfCodeMessage(string Features, string FunctionName, long nCode)
{
    if (nCode == 0)
        printf("【%s】【%s】【%s】\n", Features.c_str(), FunctionName.c_str(),
               (char*)m_pSKCenterLib->SKCenterLib_GetReturnCodeMessage(nCode));
    else
        printf("【%s】【%s】【%s】【%s】\n", Features.c_str(), FunctionName.c_str(),
               (char*)m_pSKCenterLib->SKCenterLib_GetReturnCodeMessage(nCode),
               (char*)m_pSKCenterLib->SKCenterLib_GetLastLogInfo());
}
```

### 9.2 SKCenterLib_GetLastLogInfo — 取得最後一筆 LOG 內容

- 簽名：`string SKCenterLib_GetLastLogInfo()`（文件宣告 `BSTR SKCenterLib_GetLastLogInfo();`）
- 用途：`GetReturnCodeMessage` 只能把「代碼」翻成固定的一句話；遇到本檔第五、六節那類「代碼相同、但實際原因隨主機回應而異」的情況（例如登入失敗、密碼平台回覆碼 101/300/306/307/321/502/507/511/600/602/603/604），需改查 LOG（`Center.log`，路徑由 `SKCenterLib_SetLogPath` 設定，預設在 SKCOM.dll 同層 `CapitalLog` 資料夾），或直接呼叫本函式取得最後一筆內容。

C# 範例（節錄自 `MainForm.cs:397-403`）：

```csharp
private void buttonSKCenterLib_GetLastLogInfo_Click(object sender, EventArgs e)
{
    string msg = m_pSKCenter.SKCenterLib_GetLastLogInfo();
    msg = "【SKCenterLib_GetLastLogInfo】" + msg;
    richTextBoxMethodMessage.AppendText(msg + "\n");
}
```

舊版範例 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/Form1.cs:246` 亦是相同用法：`strInfo = "【" + m_pSKCenter.SKCenterLib_GetLastLogInfo() + "】";`。

### 9.3 事件（Event）帶出的代碼——不透過函式回傳值

第三節 SK_SUBJECT 系列（報價／回報連線狀態）與部分 SGX 代碼，並非函式回傳值，而是由事件的整數參數直接帶出，需在事件處理常式內自行比對數值，例如：

```csharp
// 節錄自 Source_code/.../ReplyForm.cs：OnSolaceReplyDisconnect 事件
void OnSolaceReplyDisconnect(string bstrUserID, int nErrorCode)
{
    string msg;
    if (nErrorCode == 3002) msg = "斷線成功";              // SK_SUBJECT_CONNECTION_DISCONNECT
    else if (nErrorCode == 3033) msg = "連線異常";          // SK_SUBJECT_SOLACE_SESSION_EVENT_ERROR
    else msg = "未預期的斷線" + nErrorCode;
}
```

同樣模式也用於 `OnConnection`（報價/回報連線狀態，3001/3002/3003…）與 `OnNotifySGXAPIOrderStatus`（3002/3026/1053）。實作時建議寫一個共用的「代碼→中文說明」對照函式（可直接以本檔第一～七節內容為資料源），而不要只依賴 `GetReturnCodeMessage`——因為事件參數通常不會經過該函式转譯。

### 9.4 小結：診斷優先順序

1. 函式回傳非 0 → 先呼叫 `SKCenterLib_GetReturnCodeMessage(nCode)` 印出官方文字。
2. 若訊息不夠具體（尤其登入、憑證、密碼平台類代碼）→ 呼叫 `SKCenterLib_GetLastLogInfo()` 或直接查 `Center.log`。
3. 若代碼超出本檔範圍（尤其 5 位數以上或 0x 開頭）→ 查第八節 Microsoft／Windows 系統層級錯誤碼範圍與連結。
4. 事件參數（OnConnection、OnNotifySGXAPIOrderStatus、OnSolaceReplyDisconnect 等）不會自動轉文字，需自行對照第三節 SK_SUBJECT 表。
