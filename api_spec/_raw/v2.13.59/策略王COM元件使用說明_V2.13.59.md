# 策略王COM元件使用說明_V2.13.59

> 來源：Source_code/CapitalAPI_2.13.59_CExample/策略王COM元件使用說明_V2.13.59.docx

策略王COM元件使用說明
文件版本：V2.13.59
1、說明…………………...…………………..GO
版本控管:
| 修改日期 | 元件版本編號 | 說明 |
|---|---|---|
| 2021/07/08 | 2.13.31 | 文件及功能新增: / 單一市場(國內)商品總數有可能超過SHORT型別上限(32767)，會導致舊版功能、函式無法正常取得報價&商品資訊，請改使用以下功能、事件、物件。 / 詳情請參考 3-4 行情功能修改說明 / 國內報價： / 新增功能 / SKQuoteLib_EnterMonitorLONG / SKQuoteLib_GetTickLONG / SKQuoteLib_GetBest5LONG / SKQuoteLib_GetMACDLONG / SKQuoteLib_GetBoolTunelLONG / SKQuoteLib_GetStockByIndexLONG / SKQuoteLib_GetStockByNoLONG / 新增事件 / OnNotifyQuoteLONG / OnNotifyTicksLONG / OnNotifyHistoryTicksLONG / OnNotifyBest5LONG / OnNotifyMACDLONG / OnNotifyBoolTunelLONG / OnNotifyFutureTradeInfoLONG / 文件及物件新增: / 國內報價商品物件5-23 SKSTOCKLONG / 和SKSTOCK不同之處為 -> LONG nStockidx / 海外報價商品物件5-24 SKFOREIGNLONG / 和SKFOREIGN不同之處為 -> LONG nStockidx / 海外報價商品物件5-25 SKFOREIGN_9LONG / 和SKFOREIGN_9不同之處為 -> LONG nStockidx / 文件修改 / 2.13.30 更版內容字誤調整 / 回報資料，請以SKReplyLib_OnNewData為主 / 回報功能表—刪除模擬平台通知 / 刪除V2.13.30前一版部分功能修改比較表 |
| 2021/9/3 | 2.13.32 | 文件修改 / 配合V2.13.31 更新3-1 物件架構圖 / 配合目前功能，調整4-2-c OnRealBalanceReport內容 / 文件及功能修正: / 國內報價K線-格式選新版回傳數值調整: / 4-4-f SKQuoteLib_OnNotifyKLineData / 文件新增 / 新增錯誤代碼1100~1102 / 證券智慧單: / 使用當沖、出清及二擇一客戶，請改接新版： / 文件及功能調整: / 下單物件-當沖5-16需設定是否以MIT進場 / 下單物件-當沖5-16、 出清5-17, 相關欄位調整 / (EX:委託價市價設定等)含比較表. / 被動回報資料，請改接新格式4-2-n / 主動回報料，請改接新格式4-3-m / 文件及功能、物件新增: / 新增:刪單功能4-2-82 CancelTSStategyOrderV1 / (當沖/出清/二擇一)及刪單物件5-26 CANCELSTRATEGYORDER |
| 2021/9/13 | 2.13.33 | 文件修改 / LONG相關函式、事件新增備註 / 附錄A可轉發套件內容配合Microsoft 更新 / 盤中零股行情功能: / 注意: 盤中仍有試撮行情,請自行判斷及揭示.(項目5 、6) / 新增)3-3-2 國內行情使用說明 / 新增)SKQuoteLib_RequestStocksWithMarketNo / 新增)SKQuoteLib_GetStockByMarketAndNo / 新增)SKQuoteLib_RequestTicksWithMarketNo / 新增)SKQuoteLib OnNotifyOddLotSpreadDeal事件通知 / 修改)SKSTOCKLONG 物件 / 修改)說明:5-7 SKTICK、5-10 SKBEST5 / 修改)RequestStockList商品檔查詢，可查盤中零股-上市上櫃（市場代碼為5、6） / 不提供)歷史K線查詢、大盤查詢 、技術分析MACD 、布林通道(Bollinger band) / 盤中零股相關資訊: https://www.capital.com.tw/event/stock/oddLot/default.asp |
| 2021/9/27 | 2.13.34 | 修正使用EXCEL (VBA) 另存新檔或關閉時，出現異常關閉 / 文件修改 / 證券智慧單-被動回報(4-2-n)及主動回報(4-3-m) / 第一及第二委託價格別:新增備註 |
| 2021/10/19 | 2.13.35 | 文件及功能新增: / 雙因子登入驗證: / 登入前，請先安裝有效下單憑證 / 策略王API僅支援下單憑證綁定登入 / 自然人、法人ID帳號、SUB子帳、群組APR_APS: 請安裝登入者有效憑證後，執行登入SKCenterLib_Login / 群組AP 或APH :無憑證身份客戶，請安裝登入ID底下附屬帳號的有效憑證，並在登入前執行SKCenterLib_GenerateKeyCert。 / 待SKCenterLib_GenerateKeyCert成功，再執行登入。 / 多個AP_APH群組ID登入，請分別執行SKCenterLib_GenerateKeyCert成功後，再以AP_ID帳號登入。 / （EX: 第一個AP_1先執行SKCenterLib_GenerateKeyCert成功，再使用AP_1登入; 而後再換第二個AP_2先執行SKCenterLib_GenerateKeyCert成功，再使用AP_2登入。 / 多個ID登入使用(開發)者，建議使用不同SKCenterLib物件分別處理。 / 新增錯誤訊息: / 雙因子登入相關: / 1008: SK_ERROR_CERT_IS_EXPIRED_OR_INVALID / 1024: SK_ERROR_VERIFY_STAMP_BY_CERT_IS_FAIL / 1070: SK_ERROR_SUB_CERT_DIDNOT_BELONG_TO_LOGINID_AP / 1103: SK_ERROR_AP_APH_GENERATEKEY_INVALID_BEFORE_LOGIN / 2027: SK_WARNING_ACTIVE_CERTIFICATION_FIRST / 2028: SK_WARNING_SHOULD_IGNORE_GENERATEKEYCERT_STEP / 文件修改: / 錯誤訊息: 2023~2026，需先執行EnterMonitorLONG 相關訊息 / 國內KLine 功能，加入備註說明 / 僅提供歷史資料 / 若您未開立證券帳戶，無法訂閱或取得證券上市櫃、興櫃市場商品及上市櫃指數資料(含歷史K線)。 / 若您未開立期貨帳戶，無法訂閱或取得期貨、選擇權市場商品資料(含歷史K線)。 |
| 2021/11/23 | 2.13.36 | 功能修正及異動: / 國內行情-證券市場:(使用V2.13.33~V2.13.35 客戶請換版) / 修正訂閱現股行情(含成交明細及最佳五檔) 偶發性異常情況　EX: 盤前無法收到試撮行情 / 雙因子登入功能調整(使用V2.13.35 客戶請換版) / 海期元件更新 (使用V2.13.35 客戶請換版) / 文件修改: / 3-3-2 國內行情相關說明異動 / 下單功能: / 損益試算—未實現（明細）：4-2-p股息欄註記為不提供，可至已實現（彙總）格式取得。 / 期選智慧單: 4-2-12~14、4-2-43、4-2-48、4-2-49  回傳委託訊息內容 異動 / 報價功能： / 4-4-3、 4-4-6、4-4-23 、4-4-29、  4-4-37、 4-4-39通知事件內容更新 / 4-4-31、4-4-32 、4-4-38、4-4-s 、4-4-t 備註內容更新 / 附錄A: 新增參考內容Microsoft 如何使用 Regsvr32 工具和疑難排解 / 錯誤代碼： / 雙因子登入錯誤代碼500 ~ 599，請先確認是否安裝有效憑證 |
| 2021/12/03 | 2.13.37 | 配合主機異動相關項目： / 因主機異動，使用下單-新損益試算4-2-73 GetProfitLossGWReport客戶請換版 / 主動回報：防止接收期貨智慧單主動回報資料時，出現crash / 12/2主機已調整完成主動回報—期貨智慧單OCO / 文件及功能修改: / 4-2-79 SendTFOffSet新增大小金互抵功能 / 參數 nCommodity 新增 2=大小金 / 4-2-p OnProfitLossGWReport 新損益試算通知事件 / 2-1：回傳第一筆為查詢結果（代碼及訊息）。 / 當查詢成功，第二筆再接續回傳N筆損益資料，含修改比較表。 / 2-2：已實現-彙總：欄位說明異動，含修改比較表 / 2-3：現股當沖-明細：新增欄位-買賣別，含修改比較表 / 文件修改: / 4-3 SKReplyLib說明修改 |
| 2022/5/20 | 2.13.38 | 配合主機異動相關項目： / 因主機異動，使用下單元件期貨智慧單客戶請安排更版 / 項目2:用戶端串接下單程式需異動 / 項目6~7:用戶端串接回報程式需異動 / 功能及文件修改: / 期貨智慧單相關 / 修改:期選智慧單下單物件，參見5-2-1，含修改比較表: / 委託時效設定值異動。 / 需指定特殊委託價類別，不支援市價、不可於委託價使用P代表範圍市價。 / MIT單別需指定觸價方向。 / 期貨智慧單下單功能異動: / 舊版:限委託近月商品。 / 新增功能如下: 可委託近月或指定月份。 / 新增: 新版期貨智慧單刪單4-2-87 CancelTFStrategyOrderV1，可刪除已觸價即市場上委託。 / 可直接在委託成功時，由回傳訊息取得，序號13碼及書號。 / 原期貨智慧單STP刪單、MST刪單、MIT刪單、OCO刪單4-2-20~4-2-22、4-2-44、4-2-50、4-2-51 限制刪除未觸價委託或預約單。 / 修改：智慧單刪單物件 5-26  CANCELSTRATEGYORDER / 修改：期貨智慧單查詢(被動回報)4-2-f　OnStopLossReport改版 / 修改：智慧單主動回報: 4-3-m OnStrategyData，加入期貨新版智慧單相關欄位 / 不提供舊版智慧單主動回報功能4-3-k OnSmartData / 其他修改項目： / 修改:證券智慧單被動回報4-2-n OnTSSmartStrategyReport: MIT 新增欄位「成交價觸發價格」，含修改比較表。 / 新增：主動回報4-3-g OnNewData最後一欄加入欄位SeqNo 「序號13碼」，提供成交單含IOC/FOK產生取消單，確認比對使用，含修改比較表。 / 雙因子登入：配合特殊身份自營self 調整。 / 文件修改: / 下單:海期未平倉事件，欄位補充說明4-2-e OnOverseaFutureOpenInterest / 回報資料說明修改4-3-g OnNewData / 新增說明5-7 TICK物件，毫秒微秒時間單位。 / 下單相關(全市場委託、改價、減量、刪單)回傳訊息說明調整： / 使用非同步委託，均參照參照4-2-b OnAsyncOrder。另於4-2-b OnAsyncOrder加入委託成功及委託失敗說明。 / (不公告金融網)新增獨立SGX DMA 相關函式及參數說明:4-2-10、 4-2-17、 4-2-27 、4-2-35、 4-2-70、 4-2-b 、4-3-g-2 / (不公告金融網)4-2-10、4-2-11、4-2-27 :需先下載商品檔，再進行海期選委託（含價差） / 海期下單物件說明調整5-1 OVERSEAFUTUREORDER / 調整4-3-2 說明 / 加入新增單元7 微軟錯誤代碼Microsoft Windows HTTP error code / 因微軟不再支援，刪除附錄A VS2010 SP1可轉發套件link / 新增錯誤代碼:1105~1108 、2029 、2030 / 刪除未使用錯訊代碼: 1008、 1024、 2011 、2027 / 新增說明:錯誤代碼2017加入hyperlink / 錯誤代碼2020、2021內容說明調整 |
| 2022/7/15 | 2.13.39 | 功能修正: （客戶端需更版,不需異動程式） / 修正V2.13.38 版本4-1-1 SKCenterLib_SetLogPath失效 / 修正V2.13.38 版本，期貨智慧單-停損單功能，委託時效(IOC、FOK)設定錯誤 / 4-2-12 SendFutureStopLossOrder / 4-2-83 SendFutureSTPOrderV1 / 回報4-3-g OnNewData 錯誤訊息ErrorMsg欄位: / EX海期錯誤單，該欄內容包含「,」分隔符號，處理「,」以空白替換。 / 文件修改: / 調整說明:下單物件期貨智慧單OCO欄位說明，參見5-2-1、5-5，含修改比較表（客戶端不需異動程式） / 4-3-g-2 調整為表格,做為欄位內容說明 / 回報通知新增備註說明:4-3-a、4-3-b、4-3-h 、4-3-i / 4-2-d OnOpenInterset 新增備註說明 / 移除前一版本修改比較表 / 下單-證券智慧單4-2-61~62 加入智慧單警語 / 智慧單用戶請注意：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |
| 2022/11/2 | 2.13.40 | 功能及文件異動： / 智慧單被動回報4-2-n證券當沖，新增欄位：萬用訊息，含修改前後比較表。 / 智慧單主動回報:4-3-m 共用欄位新增萬用訊息，含修改前後比較表。 / 主動回報:海期停損限價或停損市價單，觸發時需揭示觸發註記，客戶需異動程式；其他市場或海期未觸發情況，客戶端不需異動程式。 / 當網路異常時，執行國內海外報價、回報連線功能，回傳錯誤代碼1108 / 文件修改： / 回報相關 / 4-3-g OnNewData修改回報欄位說明(KeyNo 13碼序號相關)、新增回報欄位(海期停損單觸發註記)，含修改前後比較表。 / 4-3-1主動回報: 加註 提供國內市場智慧單、4-3-m智慧單主動回報，市場別欄位，說明刪除OF海期，含修改前後比較表。 / 智慧單相關 / 證券及期貨智慧單功能，備註說明：新增刪單後，請透過智慧單被動回報確認狀態。 / 修改期貨智慧單刪單說明(STP、MST、OCO、MIT)4-2-20 、 4-2-21 、4-2-44、 4-2-50 CancelFutureStopLoss / 修改4-2-87新版期貨智慧單刪單、 期貨智慧單刪單物件說明 / 其他 / 單元1.說明，補充傳統戶如何加開電子戶。 / 錯誤訊息：2018、 2019 說明加註TLS1.2相關 / 新增錯誤訊息1108，刪除舊版不使用錯誤代碼。 / 下單-國內權益數通知事件SKOrderLib_OnFutureRights 說明 GetFutureRight hyperlink更新 / 4-2-d OnOpenInterest回傳欄位表格化 / 國內報價功能列表:註明是否支援盤中零股 / 自2020/03/17V2.13.21版本起含新證券損益試算功能， / 新客戶請直接使用 / 4-2-73 GetProfitLossGWReport & 4-2-p OnProfitLossGWReport / 4-2-42 & 4-2-j舊證券即時損益試算列為即將下線功能。 / 自2021/7/8 V2.13.31版本起含index LONG功能，自 V2.13.40會刪除元件說明中，未使用index LONG功能及事件、物件，另重新編號，如下： / 國內報價SKQuoteLib、結構物件 |
| 2022/11/30 | 2.13.41 | 功能優化調整 / 文件修改: / 下單功能，加入說明 / 4-2-6~4-2-12 、 4-2-15~4-2-19 、4-2-43 、4-2-52 、 4-2-55~ 4-2-58 、 4-2-60 4-2-614-2-65~4-2-69 、 4-2-78~4-2-79 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 2023/3/17 | 2.13.42 | 智慧單相關: / 期貨智慧單(STP、MST、OCO、MIT)刪單說明異動 / 4-2-20、 4-2-21 、4-2-22、 4-2-44 、4-2-50、4-2-51。 / ＊使用相關上述刪單功能客戶，若您的委託已產生書號，為避免影響解除保證金風控，建議使用4-2-87 並填入書號。 / 4-2-82 CancelTSStrategyOrderV1修正參考刪單物件為5-24。 / 4-2-87 CancelTSStrategyOrderV1 備註說明修正 / 複委託: / 4-2-15 複委託下單功能相關，請留意5-3物件異動說明 / 新增4-2-88 新版-複委託刪單功能 / 國內帳務查詢: / 新增4-2-89 GetOpenInterestGW 新版國內期未平倉查詢 / Struct 物件: / 5-6 SKTICK：欄位說明異動 / 5-8海期TICK、 5-12 海期TICK九位小數物件：新增成交日期 / 5-2一般期選委託物件：價格說明異動 / 5-2-1 期貨MIT 觸價單欄位加入補充說明(觸價方向) / 5-3 複委託下單物件欄位異動、新增5-3-2 複委託刪單物件說明 / 代碼定義表 / 代碼1105 :加入補充說明 / 新增代碼1109~1112 / 其他: / 新增單元：2-8 註冊DLL，UI串接注意項目 / 附錄A:可轉發套件內容調整 |
| 2023/4/10 | 2.13.43 | 功能異動: / 修正海選部分商品下單失敗 |
| 2024/1/15 | 2.13.45 | 功能修正 / 修正範例C#下單同步及非同步設定相反情況 / 配合特殊身份—海期及海選書號改價功能調整 / (線上版本客戶V2.13.40 ~ V2.13.43 請留意) / 配合特殊身份—海期權益數：回傳帳號欄位異動 / 海外報價：海期價差商品年月揭示修正 / 功能新增 / 智慧單下單、刪單、物件、被動回報、主動回報 / -國內證券智慧單下單函式異動如下；刪單物件異動於CANCELSTRATEGYORDER；委託單查詢異動於GetTSSmartStrategyReport；主動回報異動於OnStrategyData / -國內期貨智慧單下單函式異動如下；刪單物件異動於CANCELSTRATEGYORDER；委託單查詢異動於GetStopLossReport；主動回報異動於OnStrategyData / -海期智慧單下單函式新增如下；刪單函式新增於CancelOFStrategyOrder；委託單查詢函式新增於GetOFSmartStrategyReport；主動回報新增於OnStrategyData / Proxy Server下單 / 新增透過Proxy Server下單之函式，新增函式見SKOrderLib(Proxy Server下單) / 新增客製化商品行情 / 可查客製化期貨、客製化選擇權商品（市場代碼為9、10），異動函式如下： / 客製化商品不提供歷史K線查詢、大盤查詢 、技術分析MACD 、布林通道(Bollinger band) / 海期交易所新增NSE印度交易所商品查詢、Tick&Best5、K線查詢、下單、報價 / 新增委託回報查詢GetOrderReport、成交回報查詢GetFulfillReport / 文件修改 / 刪除SKCenterLib_ResetServer功能說明 / 代碼定義表 / 新增代碼156~158、1124~1125、5001~5006、5009~5018 / 新增適用V2.13.45 (含)以上版本，雙因子登入失敗密碼平台錯誤代碼 / 修改前後比較表 |
| 2024/1/29 | 2.13.46 | 功能異動 / V2.13.46 (含)以上版本，不提供舊版行情相關函式， / 移除函式如下： / 國內報價 SKQuoteLib / 功能修正 / 修正4-2-6 SendStockOrder 證券下單委託價輸入昨收價(M)、漲停價(H)、跌停價(L)錯誤之情況 / 修正4-7-10 SendDuplexProxyOrder Proxy Server複式單下單錯誤情況 / 修正證券智慧單自組單、追漲停市價單、漲跌停盯盤單主動回報(OnStrategyData)揭示未顯示條件欄位問題 / 修正報價連線未收到SK_SUBJECT_CONNECTION_STOCKS_READY事件(3003)問題 / 功能新增 / 4-2-15 SendForeignStockOrder複委託下單幣別新增 CNY、GBP；下單市場新增滬股、深股。下單物件異動請參考FOREIGNORDER / 文件修改 / 海外期貨未平倉GW資料事件OnOverseaFutureOpenInterestGW，修正為OnOFOpenInterestGWReport / 更新3-1物件架構圖 / 補充證券AB單下單物件說明STOCKSTRATEGYORDERMIT / 證券MIT智慧單SendStockStrategyMIT預風控功能說明註記、主動回報欄位補充說明、下單物件STOCKSTRATEGYORDERMIT說明 / 商品代碼刪單功能CancelOrderByStockNo補充說明 / 4-4-4、4-4-16、4-4-24國內K線、4-4-b K線通知事件，補充說明。 / C#範例 / 調整複委託下刪單功能選項 (新增港、日、新加坡、滬深市場；新增幣別 CNY、GBP) / 調整證券智慧單刪單成功顯示訊息 / 移除未使用的功能按鈕 |
| 2024/2/22 | 2.13.47 | 功能修正 / 修正證券追漲停市價單主動回報(OnStrategyData)欄位揭示問題 (PreQtyDirection與IsAskQty間缺少逗號間隔) / 修正4-2-8 SendFutureOrderCLR 期貨下單委託價輸入市價(M)、範圍市價(P)錯誤之情況 / 修正4-2-94 SendStockStrategyLLS 證券漲跌停盯盤單下單錯誤情況、下單物件STOCKSTRATEGYORDER 調整 / 修正透過Proxy Server下單之委託結果事件回傳 / 文件修改 / 4-2-64 CancelTSStrategyOrder、4-2-82 CancelTSStrategyOrderV1說明內容調整 / 4-2-s OnOFSmartStrategyReport 排版調整 / 海期智慧單OCO下單物件OVERSEAFUTUREORDER預約盤別參數值修正 / 證券智慧單MST下單物件STOCKSTRATEGYORDERMIT說明修正 / 4-7-a OnProxyOrder回傳值修正 / C#範例 / 調整證券漲跌停盯盤單下單畫面 |
| 2024/05/23 | 2.13.48 | 功能修正 / 證券自組單主動回報(OnStrategyData)欄位揭示修正 / 海期OLID下單委託事件OnAsyncOrderOLID回傳值調整 / 功能異動 / 移除證券智慧單MBA、LLS、MMIT相關功能 (SendStockStrategyMBA、SendStockStrategyLLS、SendStockStrategyMMIT) / 證券智慧單OCO下單STOCKSTRATEGYORDEROCO新增無券普賣委託別 / Proxy Server 證券下單回傳事件OnProxyOrder欄位修改 / 移除回報連線相關事件OnConnect、OnDisconnect / 智慧單主動回報OnStrategyData、被動回報訊息欄位逗號格式異動(半形改為全形) / 功能新增 / 新增智慧單多筆刪單功能CancelStrategyList / 新增複委託OLID下單功能SendForeignStockOrderOLID / 新增國內報價回傳欄位SKSTOCKLONG成交時間 / 文件修改 / 複委託下單市場名稱修正(HA：深股、SA：滬股) / Proxy Server選擇權下單物件FUTUREPROXYORDER新增範例 / Proxy Server海選刪改單物件OVERSEAFUTUREORDER修正 / 證券MIOC下單物件STOCKSTRATEGYORDERMIOC買賣別說明修改 / 證券、期貨智慧單刪單格式CANCELSTRATEGYORDER說明調整 / C#範例 / 期貨停損單長效單下單失敗修正 / 移除證券智慧單功能畫面 (MBA、LLS、MIT多條件單) / 複委託下單同步/非同步錯誤調整 / 修正GetOpenInterestGW回傳格式(查無資料情況) / 期貨停損單長效單SendFutureSTPOrderV1範例修正 / 海期智慧單AB單物件OVERSEAFUTUREORDER委託價別修正 / 執行SKCenterLib_LoginSetQuote函式登入失敗修正 |
| 2024/06/14 | 2.13.49 | 功能修正 / 證券智慧單SendStockStrategyClear、SendStockStrategyDayTrade、SendStockStrategyMIT、SendStockStrategyOCO、SendStockStrategyCB下單商品交易所代碼(上市、上櫃)錯誤修正 / 新增查無商品交易所錯誤代碼1126 SK_ERROR_EXCHANGE_ID_IS_INVALID |
| 2024/07/29 | 2.13.50 | 功能修正 / 修正4-2-9 SendOptionOrder選擇權下單委託價輸入市價(M)、範圍市價(P)錯誤之情況 / 修正智慧單被動回報查詢查無資料時資料格式 / 功能新增 / 海期報價商品清單資訊OnOverseaProducts、OnOverseaProductsDetail新增商品”第一通知日” / 新增大小微台互抵功能SendTFOffsetNew / 新增全拆組單、全拆組平單下單AllCoverDisOptions / 新增最佳五檔事件(興櫃商品成交量傳送股數) OnNotifyBest5Emerging / 3. C#範例 / 新增大小微台互抵畫面 |
| 2024/8/14 | 2.13.51 | 1.功能修正： / 修正4-2-10 SendOverseaFutureOrder海外期貨委託時效調整(新增IOC、FOK) / 修正證券智慧單MIT非盤中時段下單皆為預約單 / 新增無效預風控設定錯誤代碼1121 SK_ERROR_STRATEGY_ORDER_PRERISK_INVALID / 新增昨日均價查詢限制錯誤代碼1127 SK_ERROR_QUERY_IS_OVER_TEN_TIMES / 2.功能新增： / 新增昨日均價(昨日未沖銷的證券明細-查詢)功能GetAvgCost / 3.文件修改： / 修改海外期貨下單商品OnOverseaOption回傳內容 / 3.C#範例 / 新增查詢昨日均價畫面 / 2.海外期貨委託市價單委託時效調整(僅ROD) |
| 2024/12/19 | 2.13.52 | 1.功能修正： / 修改國內委託刪改單CancelOrderByStockNo (不輸入商品代碼即刪除全部委託) / 新增指定國內市場別之商品清單事件OnNotifyStockList 、OnNotifyCommodityListWithTypeNo回傳內容(新增跳動點、幣別) / 修正SUB帳號查詢同意書問題 / 修正只有海期帳號也能下單 / 修正客戶下單期貨找不到帳號的問題 / 2.功能新增： / 新增海期OLID刪改單功能 / SKOrderLib (proxy server下單)下單函式委託送出回傳值bstrMessage調整 / 新增國內委託刪改單 CancelOrderByStockNoAdvance (商品代號+買賣別+(委託價格)) / 查詢行情同意書-新增宣導期文字(ICE US、ICE EU)3.補充說明： / 【使用2.13.43版本之前用戶須留意】，OnFutureRights在2.13.43版本之前的版本格式為+0000000000000(例如100，會被表示為+0000000010000)，新版調整成一般格式(100) / (未公告金融網) 客戶不需異動串接程式 / 1.判斷是SELF帳號就不需要雙因子驗證 / 2.修正Mac Address配置malloc問題 / 3.測試區下單前驗章12175(SSL錯誤) / 4. OnAccount文件說明更正：原先分公司(第2欄位)與分公司代碼(第3欄位)對調；備註：證券:分公司代碼；期貨:Broker id / 5.修正部分期選帶入帳號為Broker id + 帳號(範例 F020000 + 帳號)(原先錯誤為：分公司四碼+帳號) / 6.加LOG紀錄當客戶發生SK_ERROR_ACCOUNT_MARKET_NOT_MATCH(多紀錄市場別+登入帳號) / 7.修改GetGWAccount 加上 市場別key (避免下單期貨抓錯帳號(要下內期抓成海期市場別)) / 8.修改文件(委託/回報)被動查詢:只能查到國內證期選 / 9.被動查詢(成交/委託)回報抓 brokerId / 10.文件備註：API PageNo上限一般用戶為1 (SKQuoteLib_RequestStocks) / 11.修正SKCOMTester2查stock物件資料(有訂閱時，可拿到當下最新資料) / 12.SKCOMTester新增查期貨(委託/回報)被動查詢 / 13.修正APH帳號找不到APGWQuery主機 / 14.國內即時行情被動查詢 / 15.新版本的Proxylib更新到API的分支 / 16.修正文件跟範例程式的程式碼 (SKCOMTesterV2跟 PythonV2) 成交量/100.0的寫錯(不需要除100) / 17.Proxy下單委託成功，會收到回傳值ORKEY(說明文件) / 18.資券配額加上LOG紀錄GW查回來的資料(例外狀況) / 19.更正文件說明錯誤(正確是CancelTFStrategyOrderV1，寫錯成CancelTSStrategyOrderV1) |
| 2025/04/14 | 2.13.53 | 功能修正：1.修正 Proxy 事件重複值(被新資料覆蓋)。2.修正ICS、CBOT 交易所代碼(Tandem下單用)重複，影響商品檔儲存。3.內期未平倉改走GW。4.修正群組帳號海期下單。5.SELF身分別下SRVGW內期。6.驗章明文紀錄來源別(Proxy證券期貨海期)。7.修正說明文件GetProfitLossGWReport已實現損益可帶入交易種類別：0:現股1:融資(代) 2:融券(代) 3:融資(自) 4:融券(自)  6:現沖 7.信用當沖 8:券差。8.新增判斷海期不須使用的參數代為0。9.修正 APH 執行 SKOrderLib_Initialize 出現 SK_FAIL。10.修正SGX專線無法委託海期價差單。 / 功能新增：1.一戶通、餘額、可用金額查詢。2.近三日交割款查詢。3.確保歷史資料Kline完整性，新增事件(OnKLineComplete)回傳結尾。4.API驗證：新增API驗證小工具(SKCOMVerifyDJ)。5.複委託台外幣圈存查詢。6.台股圈存餘額查詢。7.GW登入VerifyDJ證券期貨都沒做驗證，要收錯誤碼321。8.修正母帳登入取不到子帳SGX權限。 / 文件修改：1.文件更新：錯誤代碼1000，備註"*請注意登入帳號是否為大寫"。2.文件更新:下期貨價差單說明。3.文件更新：下證券單委託價M、H、L。4.文件更新：(自訂)海期刪改單ByOLID。5.文件更新：海期下單物件，商品代號欄位範例。6.文件內容更正：函式名稱，證券出清寫錯成證券當沖。7.文件內容更正：變數名稱，證券MIOC單帶入參數nOrderPriceType (原本寫錯數字，寫成0、1)，1:市價;2(買單)委賣價…..。8.文件內容更正：函式名稱，期貨MST單SendFutureMSTOrder >> SendFutureMSTOrderV1。9.文件更新：錯誤代碼321、測試未完成：請先至API下載專區 完成測試後再進行下一步。 |
| 2025/05/09 | 2.13.54 | 一、功能新增 / 1、新增讀取WebCA憑證。 / 2、新增內期下單欄位，可帶入Tandem商品名稱 + 年月下單。 / 3、OnOverseaFuture、OnOverseaProductsDetail新增商品類別 / 二、功能刪除 / 1、海期價差單SendOverseaFutureSpreadOrder / ，改使用SendOverseaFutureSpreadOrder2。 / 2、海期價差單OLID SendOverseaFutureSpreadOrderOLID / ，改使用SendOverseaFutureSpreadOrder2OLID。 / 3、Proxy海期價差單 SendOverseaFutureSpreadProxyOrder，改使用SendOverseaFutureSpreadProxyOrder2。 / 4、集保庫存查詢GetBalanceQuery，改使用GetRealBalanceReport。 / 三、功能修正 / 1、修正事件 群組帳查GW證券即時庫存Token失敗逾時。 / 2、修正群組帳號查詢期貨未平倉、海期未平倉，出現「驗證逾時，請重新登入」問題。 / 3、新增 此函示暫不提供 錯誤代碼9998 SK_THIS_FUNCTION_NOT_SUPPORTED / 4、新增 WebCA憑證初始化失敗 錯誤代碼1128 SK_ERROR_CERT_INITIALIZE_FAIL / 5、修正APG、APH使用裝置碼登入 / 四、文件異動 / 1、新增2-10 海期價差單買賣別判斷更動說明 |
| 2025/07/14 | 2.13.55 | 一、功能修正 / 1、修正Proxy下單函式 AP、APH讀取不到WebCA憑證問題 / 2、修正 SGX DMA 海期價差單買賣別相反問題 / 3、修正內期未平倉GetOpenInterestWithFormat格式二，v2.13.53起暫不提供單口手續費、交易稅(萬分之X)欄位 / 4、修正GetRealBalanceReport欄位「可資沖股數」及「可券沖股數」 / 二、功能新增 / 1、新增Proxy證券特殊刪改單SendStockProxyPreAlter / 2、組合部位AssembleOptions、複式單拆解DisassembleOptions、雙邊部位了結CoverAllProduct  新增「非同步委託」 / 三、文件調整 / 1、更新證券Proxy特殊刪改單文件 / 2、可資沖/可券沖欄位錯置文件說明 / 3、查詢近三日交割款回傳格式文件更正(是民國年，但文件格式寫成西元年) |
| 2025/08/04 | 2.13.56 | 1、修正全拆平組單AllCoverDisOptions2、國內期貨OCO智慧單SendFutureOCOOrderV1新增 nTimeFlag 「全盤」選項 / 3、海期OCO智慧單SendOverSeaFutureOCOOrder新增 nTimeFlag 「全盤」選項4、新增OnOFSmartStrategyReport、OnStopLossReport、OnStrategyData OCO智慧單主被動回報欄位 "盤別" |
| 2025/09/15 | 2.13.57 | 一、功能異動 / 1、SKCenterLib_Login登入失敗需間隔五秒鐘後才能再次嘗試，登入失敗次數已達五次，則須重新啟動API。 / 二、功能修正 / 1、新增錯誤代碼9997 SK_ERROR_LOGIN_FAIL_LIMIT表示「登入失敗已達五次，請重啟API」。 / 2、新增錯誤代碼1129 SK_ERROR_LOGIN_FAIL_IN_PROCESSING / 表示「登入失敗，請稍等五秒後再試」。 / 3、修正群組帳號、SUB子帳讀取智慧單風險問題。 / 4、修正SKQuoteLib_RequestLiveTick 訂閱數問題。 / 5、修正SKQuoteLib_RequestTicks、SKQuoteLib_RequestTicksWithMarketNo可以無限訂閱問題 / 6、修正 Proxy特殊刪改單崩潰問題 / 7、修正GetNTDBlock回傳結尾 / 8、修正 Proxy海期選刪改單多帳號使用時，憑證抓到前一個問題 / 9、修正Proxy crash(Proxy下單前定時檢查連線狀態) / 二、功能新增 / 1、新增內外期未平倉CALLBACK回傳GW查詢狀態結果OnOpenInterestGWStatus、OnOverseaFutureOpenInterestGWStatus / 2、新增盤後零股報價(14:30) |
| 2026/03/09 | 2.13.58 | 一、功能異動： / 1、新增事件OnOpenInterestJson內期未平倉查詢一次回傳所有庫存(Json格式) / 2、新增內、外期未平倉查詢GetOpenInterest、GetOpenInterestGW、GetOpenInterestWithFormat、GetOverSeaFutureOpenInterest「查無庫存」時新增Account欄位 / 二、功能修正： / 1、修正證券智慧單CB單SendStockStrategyCB發生SK_ERROR_MARKET_OUT_OF_RANGE問題 / 2、新增例外處理 國內期貨未平倉Parse失敗情況 / 3、修正訂閱國內行情價差商品問題 / 4、修正訂閱國內報價慢的問題 / 5、修正SGX專線主動回報缺漏問題 / 6、修正報價只有全盤商品的T盤需要加AM / 7、修正GetStockByNoLONG商品代號取物件問題 / 三、功能新增： / 1、新增海選下單函式SendOverseaOptionOrderOLID提供自訂資料欄 / 四、文件調整： / 1、調整下單函式文件說明(同步、非同步委託收到回傳值為0時，表示成功送至交易所，交易結果請由回報確認) |
| 2026/08/03 | 2.13.59 | 一、功能新增： / 國內期選主動回報OnNewData新增欄位「下單時間HH:mm:ss.fff」 / 自營帳號即時庫存查詢GetRealBalanceReport欄位新增"昨日庫存" / 即時分K SKQuoteLib_GetLiveKLineLONG / 國內未平倉查詢GetOpenInterestGW新增欄位「商品－下單代碼 / 新增連線函式-可帶入參數指定要訂閱的市場SKQuoteLib_EnterMonitorLONGByMarket / 新增錯誤代碼9996 SK_ERROR_UPDATE_API_REQUIRED此版本已無法登入，請更新版本 / 二、功能修正： / 修正國內報價第一筆Ticks缺失 / 修正內期夜盤收盤價未更新問題 / 修正當複委託回報沒給時效欄位時，需給前端空值 / 修正主動回報連線多帳號，無法斷線問題 / 修正智慧單被動回報缺少逗號問題 / 修正index不一致問題 / 修正簡化版API下單成功回傳值 |

1、 說明
策略王API整合元件函式庫主要提供功能。欲使用此元件功能，必須先申請API權限才可登入使用，否則將無法初始元件，進而無法使用相關函式功能。
*同意書及風險預告書，請先確認是否為網路戶。
一般自然人：行情相關特殊商品同意書簽署，可至金融網－線上簽署專區點選。
（進入群益網上發－服務－同意區簽署點選）
行情相關特殊商品－國內期貨(ex:東證TSE TOPIX)期貨及海外行情(ex: CFE VIX、 ICE etc)
一般自然人：使用國內、外下單（含證期選智慧單），可至金融網－線上簽署專區點選。
（進入群益網上發－服務－同意區簽署點選）
法人使用國內、外下單與回報功能，請由業務單位代客戶申請需求單，註明申請開放國內、外特殊商品之行情資訊、下單、回報權限。
註：若傳統戶申請網路戶可透過下方link，加開電子戶。
https://iopen.capital.com.tw/eopen/#/main/home
(點選我已是群益證券客戶，再點最下方加開電子戶)
2、環境設定
2-1元件註冊(註冊API時出現的錯誤碼及處理)
SKCOM.dll為ActiveX COM元件，元件需先在Windows平台進行元件註冊動作。
依實際電腦環境與開發需求，擇一使用x86 或x64　COM元件。
使用regsvr32.exe 進行註冊。
直接執行 install.bat 進行註冊。(元件資料夾，再選x86或x64 擇一註冊，請使用系統管理員身分-Administrator權限)
2-2 注意事項
SKCOM.dll  COM元件與憑證元件、報價元件需放在同一資料夾內再進行2-1元件註冊。
範例程式(SKCOMTester and EXCEL file )不受此限。
2-3 完成COM元件註冊，開範例程式並成功載入SKCOMLib
使用Microsoft Visual Studio 2019載入SKCOMTester 專案，確認SKCOMLib是否已連結稍早註冊的COM元件。
2-4 未註冊COM元件，開範例程式錯誤畫面與如何處理
以下為未註冊COM元件，先開啟範例程式，未能取得SKCOMTester範例與SKCOMLib之連結錯誤畫面。
處理方式：
請先進行2-1元件註冊，再開啟SKCOMTester 專案，使SKCOMTester能正確連結到已註冊之SKCOMLib。
2-5　如何解除註冊COM元件
直接執行 Uninstall.bat 進行解除註冊。(元件資料夾，再選x86或x64 擇一解註冊)
2-6 SKCOM更版步驟
1. uninstall舊版本2. install新版本* 請留意擇一註冊x86或x64元件，避免混用x86及x64
* 安裝SKCOM.dll 和 UI EXE請配合執行相關位元(x86 or x64)
2-7 註冊DLL x86與x64 位元注意項目
未配合註冊相對應位元SKCOM來串接執行檔，可能發生的情況
錯誤範例：
安裝x86版的SKCOM.dll，卻使用Any CPU或x64平台去執行程式
安裝x64版的SKCOM.dll，卻使用x86平台執行
C# SKCOMTester 建置注意事項
專案(點右鍵) -> 屬性 -> 建置，位元請正確對應
X64
X86
2-8 註冊新版本DLL元後，注意UI串接路徑
下方以C#串接為例，(下圖為群益官方範例SKCOMTester)設定，點選「參考」中的SKCOMLib內容，確認路徑是否為最新版本。
同時確實「uninstall.bat」取消註冊舊版本。
2-9 API連線測試(需要雙因子登入)
依主管機關規定，使用正式API前，須先進行連線測試。
連線測試小程式(SKCOMVerifyDJ.exe)請至API下載專區下載。
1、雙因子登入
2-1、下單測試(證券) 【客戶如有證券或複委託帳號，請做此下單測試】
|  |
|---|

2-2、下單測試(國內期貨) 【客戶如有國內期貨或海外期貨帳號，請做此下單測試】
|  |
|---|

3、查詢測試紀錄有無驗證-成功
|  |
|---|

2-10 海期價差單買賣別判斷更動說明
海期商品會根據不同種類的商品，其買賣方向判斷方式將有所不同，主要以近月或遠月為基準進行判定。
海期商品種類區分如下：
1. 指數商品價差EQ (商品種類6)
2. 一般商品價差SP (商品種類7)
3. 外匯商品價差FX (商品種類8)
商品種類取得方式:
2.13.54起，事件OnOverseaFuture、OnOverseaProductsDetail將提供「商品種類」欄位。
OnOverseaFuture查詢畫面：
OnOverseaProductsDetail查詢畫面:
舉例說明：
當「商品種類」為 6，代表此商品為「指數商品價差（EQ）」。
以商品CME,ES-2506/+2509為例，該商品屬於以遠月為主的價差商品，因此pOrder.sBuySell為遠月買賣別，舉例來說:
若委託為「賣」單，價差委託為買進近月，賣出遠月。
若委託為「買」單，價差委託為賣出近月，買進遠月。
買賣別以遠月份商品為主。
當「商品種類」為 7，代表此商品為「一般商品價差（SP）」。
以商品CBOT,TY+2506/-2509為例，該商品屬於以近月為主的價差商品，因此pOrder.sBuySell為近月買賣別，舉例來說:
若委託為「賣」單，價差委託為賣出近月，買進遠月。
若委託為「買」單，價差委託為買進近月，賣出遠月。
買賣別以近月份商品為主
當「商品種類」為 8，代表此商品為「外匯商品價差（FX）」。
以商品CME,JY+2509/-2506為例，該商品屬於以遠月為主的價差商品，因此pOrder.sBuySell為遠月買賣別，舉例來說:
若委託為「賣」單，價差委託為買進近月，賣出遠月。
若委託為「買」單，價差委託為賣出近月，買進遠月。
買賣別以遠月份商品為主。
3、API 元件說明
3-1物件架構
元件中包含六個ATL物件，與十八個結構物件。ATL物件中部分函式帶入參數有結構物件，需依照指定物件帶入該參數欄位。
SKCenterLib：登入&環境設定。
SKOrderLib：下單物件。
SKQuoteLib：國內報價物件。
SKReplyLib：回報物件。
SKCenterLibSKOrderLibSKQuoteLibSKReplyLib
SKBEST5SKTICKSKSTOCKLONGGBoolTunelMACDSTOCKORDERFUTUREORDEROVERSEAFUTUREORDEROVERSEAFUTUREORDERFORGWSTOCKSTRATEGYORDEROUTSTOCKSTRATEGYORDERFOREIGNORDERFUTUREOCOORDERSTOCKPROXYORDERFUTUREPROXYORDEROSSTOCKPROXYORDER
3-2註冊公告（必要項目）
請於登入(SKCenterLib_Login或SKCenterLib_LoginSetQuote)前先註冊公告，建立SKReplyLib 物件及事件SKReplyLib_OnReplyMessage4-3-e
並在事件處理函式中，回傳指定參數，參數值為-1。
Python 範例：
建立SKReplyLib 物件及回報事件
註冊事件SKReplyLib_OnReplyMessage並在事件處理函式(OnReplyMessage)中，回傳指定參數，參數值為-1。
C#　範例：
建立SKReplyLib 物件及註冊回報事件SKReplyLib_OnReplyMessage
在事件處理函式(OnAnnouncement)中，回傳指定參數，參數值為-1。
EXCEL 範例：
建立SKReplyLib 物件及回報事件
|  |
|---|
|  |

在事件處理函式(SKReplyLib_OnReplyMessage)中，回傳指定參數，參數值為-1。
|  |
|---|

4、ATL物件函式說明
4-1 SKCenterLib
函式
| 功能 | 函式名稱 | 備註 |
|---|---|---|
| 設定LOG | SKCenterLib_SetLogPath | 欲變更Log路徑此函式需最先呼叫。 |
| 登入 | SKCenterLib_Login | <雙因子-憑證綁定> / 需先註冊4-3-e公告資訊SKReplyLib_OnReplyMessage |
| 取得代碼訊息 | SKCenterLib_GetReturnCodeMessage |  |
| 記錄呼叫函式 | SKCenterLib_Debug | 記錄函式呼叫log |
| 取得最後LOG訊息 | SKCenterLib_GetLastLogInfo | 取得最後一筆LOG內容 |
| 手動設定特殊功能屬性開啟或關閉 | SKCenterLib_SetAuthority | 設定特殊功能開啟與否 / (SGX 專線屬性:關閉/開啟:0/1 ) |
| 登入-設定是否啟用報價 | SKCenterLib_LoginSetQuote | <雙因子-憑證綁定> / 需先註冊4-3-e公告資訊SKReplyLib_OnReplyMessage |
| 取得註冊SKAPI版本 | SKCenterLib_GetSKAPIVersionAndBit | (含SKCOM 位元) |
| 雙因子初使 / 產生附屬帳號key | SKCenterLib_GenerateKeyCert | <雙因子-憑證綁定> / <群組特殊身份使用> |

事件
| 功能 | 事件名稱 | 備註 |
|---|---|---|
| TIMER | OnTimer |  |
| 同意書未簽署通知 | OnShowAgreement |  |
| SGX API專線下單連線狀態 | OnNotifySGXAPIOrderStatus | 注意，SGX API DMA專線需向交易後台申請，方可使用。 |

4-1-1 SKCenterLib_SetLogPath
| 設定LOG檔存放路徑。預設LOG存放於執行之應用程式下，如要變更LOG路徑，此函式需最先呼叫。 |
|---|
| 宣告 | Long SKCenterLib_SetLogPath([in] BSTR bstrPath); |
| 參數 | bstrPath | LOG檔存放路徑。 |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示初始失敗。錯誤代碼可參考對照表。 |
| 備註 | 預設LOG位置為以下（與預設SKCOM.dll 同一層）,資料夾名稱為CapitalLog |

4-1-2 SKCenterLib_Login
| 元件初始登入。在使用此 Library 前必須先通過使用者的雙因子(憑證綁定)身份認證，方可使用。 / （一般身份）請在登入前，安裝使用者帳號ID有效憑證 / （AP及APH身份）請在登入前，安裝登入ID底下附屬帳號之有效憑證，並先執行SKCenterLib_GenerateKeyCert成功後，再執行SKCenterLib_Login |
|---|
| 宣告 | Long SKCenterLib_Login([in] BSTR bstrUserID, [in] BSTR bstrPassword); |
| 參數 | bstrUserID | 使用者登入帳號。 |
|  | bstrPassword | 密碼。 |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | AP及APH身份請注意: / 如果登入前,未經SKCenterLib_GenerateKeyCert函式取得雙因子驗證key，將會登入失敗，得到 SK_ERROR_AP_APH_GENERATEKEY_INVALID 的錯誤。 / 登入失敗時可由log查詢失敗原因。 / 登入前，需先註冊4-3-e公告資訊SKReplyLib_OnReplyMessage / V2.13.57含以上版本，登入失敗需間隔五秒後才能再次嘗試，登入失敗次數已達五次，則須重新啟動API。 |

4-1-3 SKCenterLib_GetReturnCodeMessage
| 取得定義代碼訊息文字。 |
|---|
| 宣告 | BSTR SKCenterLib_GetReturnCodeMessage([in] LONG nCode); |
| 參數 | nCode | 函式回傳值。 |
| 回傳值 | 代碼文字訊息。 |
| 備註 |  |

4-1-4 SKCenterLib_Debug
| 函式呼叫Log記錄。開啟時會記錄呼叫過的函式與所帶入的參數。 |
|---|
| 宣告 | Long SKCenterLib_Debug([in] VARIANT_BOOL bDebug); |
| 參數 | bDebug | 是否為Debug模式。 |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | LOG會產生於LOG資料夾  YYYYMMDD_XXXX.log |
|  |  |

4-1-6 SKCenterLib_GetLastLogInfo
| 取得最後一筆LOG內容。 |
|---|
| 宣告 | BSTR SKCenterLib_GetLastLogInfo(); |
| 參數 | 無 |  |
| 回傳值 | 資料內容。 |
| 備註 |  |

4-1-7 SKCenterLib_SetAuthority
| (SGX 專線only)手動設定特殊功能屬性開啟或關閉。 |
|---|
| 宣告 | Long SKCenterLib_SetAuthority([in] LONG nAuthorityFlag); |
| 參數 | nAuthorityFlag | 指定特殊功能屬性是否開啟或關閉 |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | SGX 專線屬性：關閉／開啟：0／1 / Bit 1為環境設定：預設0，代表正式環境 / 一般客戶可忽略此部分 |

4-1-8 SKCenterLib_RequestAgreement
| 取得所有聲明書及同意書簽署狀態。 / 預設登入時就會主動查詢聲明書及同意書，不需特別再執行本功能。 |
|---|
| 宣告 | Long SKCenterLib_RequestAgreement([in] BSTR bstrUserID); |
| 參數 | bstrUserID | 使用者登入帳號。 |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 簽屬狀態會由OnShowAgreement回傳 / 查詢時，除了海外行情同意書一定查詢外，其他聲明書或同意書若狀態已為【已簽署】的時候，不會再進行查詢。 |

4-1-9 SKCenterLib_LoginSetQuote
| 元件初始登入。在使用此 Library 前必須先通過使用者的雙因子(憑證綁定)身份認證，方可使用。 / （一般身份）請在登入前，安裝使用者帳號ID有效憑證 / （AP及APH身份）請在登入前，安裝登入ID底下附屬帳號之有效憑證，並先執行SKCenterLib_GenerateKeyCert成功後，再執行SKCenterLib_Login |
|---|
| 宣告 | Long SKCenterLib_LoginSetQuote([in] BSTR bstrUserID, [in] BSTR bstrPassword, [in] BSTR bstrSetFlag); |
| 參數 | bstrUserID | 使用者登入帳號。 |
|  | bstrPassword | 密碼。 |
|  | bstrSetFlag | 設定是否啟用報價元件功能。 / Y:啟用報價 N:停用報價 |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | AP及APH身份請注意: / 如果登入前,未經SKCenterLib_GenerateKeyCert函式取得雙因子驗證key，將會登入失敗，得到 SK_ERROR_AP_APH_GENERATEKEY_INVALID 的錯誤。 / 登入失敗時可由log查詢失敗原因。 / 登入前，需先註冊4-3-e公告資訊SKReplyLib_OnReplyMessage |

4-1-10 SKCenterLib_GetSKAPIVersionAndBit
| 取得目前註冊SKAPI 版本及位元。 |
|---|
| 宣告 | BSTR SKCenterLib_GetSKAPIVersionAndBit([in] BSTR bstrUserID); |
| 參數 | bstrUserID | 使用者登入帳號。 |
| 回傳值 | 目前註冊SKAPI版本及COM位元。(EX: 2.13.30_x64) |
| 備註 |  |

4-1-11 SKCenterLib_GenerateKeyCert
| 僅適用AP及APH無憑證身份 / 請在登入前，安裝附屬帳號ID有效憑證，再透過此函式產生雙因子登入憑證資訊。 / 雙因子登入必須透過憑證，使用群組的帳號登入，必須自行選擇群組內其一附屬帳號，以進行驗證憑證相關程序。 |
|---|
| 宣告 | Long SKCenterLib_GenerateKeyCert ( [in] BSTR bstrLogInID, [in] BSTR bstrCustCertID); |
| 參數 | bstrLogInID | (群組)登入帳號。 |
|  | bstrCustCertID | 綁定在該群組之下且已安裝憑證之附屬帳號ID |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 請先安裝附屬帳號ID有效憑證，再執行此功能 / 如果登入前,未經此函式取得雙因子驗證key，執行SKCenterLib_Login / 將會登入失敗，得到 SK_ERROR_AP_APH_GENERATEKEY_INVALID 的錯誤。 |

4-1-a OnTimer
| 定時Timer通知。每分鐘會由該函式得到一個時間。 |
|---|
| 宣告 | void OnTimer([in] LONG nTime ); |
| 參數 | nTime | 時間。EX：133525，表示 13：35：25 |
| 備註 |  |

4-1-b OnShowAgreement
| 同意書狀態通知。 |
|---|
| 宣告 | void OnShowAgreement([in] BSTR bstrData); |
| 參數 | bstrData | 回傳未簽署的同意書訊息，或使用SKCenterLib_RequestAgreement時回傳同意書狀態。 |
| 備註 | API不支援簽署同意書，請至其他管道簽署同意書。 / ＊期貨API下單聲明書簽署，新增補簽狀態為P |

4-1-c OnNotifySGXAPIOrderStatus
| SGX API DMA專線下單連線狀態。 |
|---|
| 宣告 | void OnNotifySGXAPIOrderStatus ([in] LONG　nStatus  ,[in] BSTR bstrOFAccount); |
| 參數 | nStatus | 回傳連線狀態(3002 、3026 、1053) |
|  | bstrOFAccount | 回傳已登入成功之海期帳號。 |
| 備註 | SGX API專線下單連線狀態，確認連線成功方可進行委託。 |

4-2 SKOrderLib ( 下單 )
函式
| 功能 | 函式名稱 | 備註 |
|---|---|---|
| 初始 | SKOrderLib_Initialize | 需先執行才可執行相關下單函式。 |
| 取得交易帳號 | GetUserAccount | 下單帳號由該函式取得為主。 / 登入前，需先簽署證券或期貨API下單聲明書，方能取得相關市場帳號。 / 若未簽署，請簽署完成，再重新登入。 |
| 讀取憑證 | ReadCertByID | 驗證憑證有效後，方可進行各相關委託功能。 |
| 限制量設定 | SetMaxQty |  |
| 限制筆數設定 | SetMaxCount |  |
| 證券委託 | SendStockOrder | ＊適用逐筆交易 |
| 期貨委託含倉位盤別 | SendFutureOrderCLR | (新增)可選盤別、倉別 |
| 選擇權委託 | SendOptionOrder |  |
| 海期委託 | SendOverseaFutureOrder |  |
| 海選委託 | SendOverseaOptionOrder |  |
| 期貨停損委託 | SendFutureStopLossOrder | 簽署期貨智慧單風險預告書 / 僅可委託近月 |
| 移動停損委託 | SendMovingStopLossOrder | 簽署期貨智慧單風險預告書 / 僅可委託近月 |
| 選擇權停損委託 | SendOptionStopLossOrder | 簽署期貨智慧單風險預告書 |
| 期貨二擇一委託 | SendFutureOCOOrder | 簽署期貨智慧單風險預告書 / 僅可委託近月 |
| 複委託 | SendForeignStockOrder |  |
| 委託減量 | DecreaseOrderBySeqNo |  |
| 海期委託減量 | OverSeaDecreaseOrderBySeqNo |  |
| (自訂)海期委託減量ByOLID | OverSeaDecreaseOrderBySeqNoOLID |  |
| 國內刪單-序號 | CancelOrderBySeqNo | 13 碼序號 |
| 國內刪單-商品代號 | CancelOrderByStockNo | 依帳號所屬登入ID+商品代號 |
| 證期權改價 | CorrectPriceBySeqNo | 依序號改價 |
| 證期權改價 | CorrectPriceByBookNo | 依書號改價 |
| 海期價差委託 | SendOverseaFutureSpreadOrder | V2.13.54後，不再提供此函式 |
| 證券即時庫存 | GetRealBalanceReport |  |
| 海期未平倉查詢 | GetOverseaFutureOpenInterest |  |
| 停損單查詢 | GetStopLossReport |  |
| 海期下單商品 | GetOverseaFutures |  |
| 海選下單商品 | GetOverseaOptions |  |
| 下單解鎖 | UnlockOrder |  |
| 海期刪單 | OverSeaCancelOrderBySeqNo | 依序號刪單 |
| (自訂)海期刪單ByOLID | OverSeaCancelOrderBySeqNoOLID |  |
| 海期刪單 | OverSeaCancelOrderByBookNo | 依書號刪單 |
| 查詢登入類型 | SKOrderLib_GetLoginType |  |
| 查詢下單類型 | SKOrderLib_GetSpeedyType |  |
| 下載海期商品檔 | SKOrderLib_LoadOSCommodity | 取得海期帳才可下載商品檔，進行海期委託下單。 |
| 下載海選商品檔 | SKOrderLib_LoadOOCommodity | 取得海期帳才可下載商品檔，進行海選委託下單。 |
| 查詢國內權益數 | GetFutureRights |  |
| 查詢海外期貨權益數 | GetRequestOverSeaFutureRight |  |
| 查詢證券資券配額 | GetMarginPurchaseAmountLimit |  |
| 查詢集保庫存 | GetBalanceQuery | V2.13.54後，不再提供此函式 |
| 國內期貨MIT委託 | SendFutureMITOrder | 簽署期貨智慧單風險預告書 / 僅可委託近月 |
| 國內選擇權MIT委託 | SendOptionMITOrder | 簽署期貨智慧單風險預告書 |
| 國內選擇權複式單 | SendDuplexOrder |  |
| 建立SGX API專線 | AddSGXAPIOrderSocket | SGX API DMA專線需向營業員申請，方可使用。 |
| 國內刪單-書號 | CancelOrderByBookNo | 依5碼書號刪單 |
| 大小台互抵 | SendTXOffset |  |
| 組合部位 | AssembleOptions | 非交易行為，無回報 |
| 複式單拆解 | DisassembleOptions | 非交易行為，無回報 |
| 雙邊部位了結 | CoverAllProduct | 非交易行為，無回報 |
| 國內外出入金互轉 | WithDraw |  |
| 證券智慧單當沖條件委託 | SendStockStrategyDayTrade | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 證券智慧單出清條件委託 | SendStockStrategyClear | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 證券智慧單被動查詢 | GetTSSmartStrategyReport | 簽署證券智慧單風險預告書 |
| 證券智慧單刪單 | CancelTSStrategyOrder | 簽署證券智慧單風險預告書 |
| 海期改價 | OverSeaCorrectPriceByBookNo | 僅原限價單改限價 |
| (自訂)海期改價-書號ByOLID | OverSeaCorrectPriceByBookNoOLID |  |
| 海期價差改價 | OverSeaCorrectPriceSpreadByBookNo | 僅原限價單改限價 |
| (自訂)海期價差改價-書號ByOLID | OverSeaCorrectPriceSpreadByBookNoOLID |  |
| 海選改價 | OverSeaOptionCorrectPriceByBookNo | 僅原限價單改限價 |
| (自訂)海期委託ByOLID | SendOverseaFutureOrderOLID |  |
| (自訂)海期價差委託ByOLID | SendOverseaFutureSpreadOrderOLID | V2.13.54後，不再提供此函式 |
| SGX DMA專線 / 海期改價 | OverSeaCorrectPriceBySGXAPISeqNo | 僅SGX API DMA專線可使用。 |
| 證券智慧單MIT條件委託 | SendStockStrategyMIT | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 證券智慧單OCO條件委託 | SendStockStrategyOCO | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 新損益試算查詢 | GetProfitLossGWReport |  |
| 證券智慧單MIOC條件委託 | SendStockStrategyMIOC | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 證券智慧單MST條件委託 | SendStockStrategyMST | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 海外期貨未平倉GW查詢 | GetOverseaFutureOpenInterestGW | (查詢格式:彙總或明細) |
| Ping 和 Tracert主機測試 | SKOrderLib_PingandTracertTest | 適用於出現微軟https/socket error / 確認與主機連線狀態 |
| 證券盤中零股委託 | SendStockOddLotOrder |  |
| 指定期貨商品互抵 | SendTFOffset |  |
| 上傳LOG | SKOrderLib_LogUpload | 上傳近3日LOG |
| Telnet 主機測試 | SKOrderLib_TelnetTest | 測試主機連線是否正常 |
| 證券智慧單刪單新版 | CancelTSStrategyOrderV1 | 簽署證券智慧單風險預告書 |
| 新版)期貨停損委託V1 | SendFutureSTPOrderV1 | 簽署期貨智慧單風險預告書 / 可委託近月或指定月份 |
| 新版)移動停損委託V1 | SendFutureMSTOrderV1 | 簽署期貨智慧單風險預告書 / 可委託近月或指定月份 |
| 新版)國內期貨MIT委託V1 | SendFutureMITOrderV1 | 簽署期貨智慧單風險預告書 / 可委託近月或指定月份 |
| 新版)期貨二擇一委託V1 | SendFutureOCOOrderV1 | 簽署期貨智慧單風險預告書 / 可委託近月或指定月份 |
| 新版)期貨刪單V1 | CancelTFStrategyOrderV1 | 簽署期貨智慧單風險預告書 / 智慧單號及13碼序號，若委託已產生書號亦需一併填入 |
| (新)複委託刪單 | CancelForeignStockOrder |  |
| (新)期貨未平倉查詢-GW | GetOpenInterestGW |  |
| 證券AB單委託 | SendStockStrategyAB | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 證券自組單委託 | SendStockStrategyCB | 簽署證券智慧單風險預告書，相關警語參考備註 |
| 期貨AB單委託 | SendFutureABOrder | 簽署期貨智慧單風險預告書 |
| 海期二擇一單委託 | SendOverSeaFutureOCOOrder |  |
| 海期AB單委託 | SendOverSeaFutureABOrder |  |
| 海期智慧單刪單 | CancelOFStrategyOrder |  |
| 海期智慧單被動查詢 | GetOFSmartStrategyReport |  |
| 委託回報查詢 | GetOrderReport |  |
| 成交回報查詢 | GetFulfillReport |  |
| (自訂)複委託ByOLID | SendForeignStockOrderOLID |  |
| 智慧單多筆刪單 | CancelStrategyList |  |
| 昨日未沖銷的證券明細查詢 | GetAvgCost |  |
| 國內委託删單(By帳號所屬登入ID+商品代號+買賣別+委託價格) | CancelOrderByStockNoAdvance |  |
| 一戶通、餘額、可用金額查詢 | GetBalance |  |
| 近三日交割款查詢 | GetT3DueAmt |  |
| 台股圈存查詢 | GetIPAAmt |  |
| 複委託外幣圈存查詢 | GetBankBlock |  |
| 複委託台幣圈存查詢 | GetNTDBlock |  |
| 海外期貨價差委託 | SendOverseaFutureSpreadOrder2 |  |
| 送出海外期貨價差委託。ByOLID | SendOverseaFutureSpreadOrder2OLID |  |
| 送出海外選擇權委託。ByOLID | SendOverseaOptionOrderOLID |  |

事件
| 功能 | 事件名稱 | 備註 |
|---|---|---|
| 帳號資料 | OnAccount |  |
| 非同步委託結果 | OnAsyncOrder |  |
| 證券即時庫存 | OnRealBalanceReport |  |
| 期貨未平倉 | OnOpenInterest |  |
| 海期未平倉 | OnOverseaFutureOpenInterest |  |
| 停損單資料 | OnStopLossReport |  |
| 海期商品資料 | OnOverseaFuture |  |
| 海選商品資料 | OnOverseaOption |  |
| 國內權益數 | OnFutureRights |  |
| 證券即時損益資料 | OnRequestProfitReport | 即將下線 / 請使用4-2-73 GetProfitLossGWReport & / 4-2-p OnProfitLossGWReport |
| 海外期貨權益數 | OnOverSeaFutureRight |  |
| 查詢證券資券配額 | OnMarginPurchaseAmountLimit |  |
| 查詢集保庫存 | OnBalanceQuery |  |
| 證券智慧單資料 | OnTSSmartStrategyReport |  |
| (自訂)非同步委託結果ForOLID | OnAsyncOrderOLID |  |
| 新損益試算資料 | OnProfitLossGWReport |  |
| 海外期貨未平倉GW資料 | OnOFOpenInterestGWReport | (依查詢格式回傳彙總或明細) |
| Telnet測試 | OnTelnetTest |  |
| 海期智慧單資料 | OnOFSmartStrategyReport |  |
| 國內期貨未平倉GW的查詢狀態 | OnOpenInterestGWStatus |  |
| 海外期貨未平倉GW的查詢狀態 | OnOverseaFutureOpenInterestGWStatus |  |

4-2-1 SKOrderLib_Initialize
| SKOrderLib 下單物件初始化。産生下單物件後需先執行初始動作。 |
|---|
| 宣告 | Long SKOrderLib_Initialize(); |
| 參數 | 無。 |  |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示初始失敗。錯誤代碼可參考對照表。 |
| 備註 |  |

4-2-2 GetUserAccount
| 取回目前可交易的所有帳號。資料由OnAccount事件回傳。 |
|---|
| 宣告 | Long GetUserAccount(); |
| 參數 | 無。 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 登入前，需先簽署證券或期貨API下單聲明書，方能取得相關市場帳號。 / 若未簽署，請先簽署完成，再重新登入。 |

4-2-3 ReadCertByID
| 讀取憑證資訊。委託下單必須透過憑證，因此當元件初始化成功後即需要做讀取憑證的動作，如果使用群組的帳號做初始，則必須自行將所有的帳號依序做讀取憑證的動作。 / 如果送出委託前未經讀取憑證，送委託會得到 SK_ERROR_ORDER_SIGN_INVALID 的錯誤。 |
|---|
| 宣告 | Long ReadCertByID( [in] BSTR bstrLogInID ); |
| 參數 | bstrLogInID | 登入帳號。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 |  |

4-2-4 SetMaxQty
| 設定每秒委託「量」限制。一秒內下單超過設定值時下該類型下單將被鎖定，需進行解鎖才可繼續下單。 |
|---|
| 宣告 | Long SetMaxQty( [in] LONG nMarketType, [in] LONG nMaxQty ); |
| 參數 | nMarketType | 0：TS (證券) / 1：TF(期貨) / 2：TO(選擇權) / 3：OS(複委託) / 4：OF(海外期貨) / 5：OO(海外選擇權) |
|  | nMaxQty | 委託量，小於等於零無限制。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 若下單超過設定被上鎖時，需呼叫 UnlockOrder 解鎖，才可繼續下單。 |

4-2-5 SetMaxCount
| 設定每秒委託「筆數」限制。一秒內下單超過設定值時下該類型下單將被鎖定，需進行解鎖才可繼續下單。 |
|---|
| 宣告 | Long SetMaxCount( [in] LONG nMarketType, [in] LONG nMaxCount ); |
| 參數 | nMarketType | 0：TS (證券) / 1：TF(期貨) / 2：TO(選擇權) / 3：OS(複委託) / 4：OF(海外期貨) / 5：OO(海外選擇權) |
|  | nMaxCount | 委託筆數，小於等於零無限制。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 若下單超過設定被上鎖時，需呼叫 UnlockOrder 解鎖，才可繼續下單。 |

4-2-6 SendStockOrder
| 送出證券委託。 |
|---|
| 宣告 | Long SendStockOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 STOCKORDER 物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託： / 如果回傳值為0表示委託成功，訊息內容則為13碼的委託序號。 / 其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊亦適用證券逐筆 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-7 SendFutureOrder
| (新客戶請使用4-2-8) / 送出期貨委託，不需填倉位，盤別預設為盤中，不可更改。 |
|---|

4-2-8 SendFutureOrderCLR
| 送出期貨委託，需設倉別與盤別。 |
|---|
| 宣告 | Long SendFutureOrderCLR([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pAsyncOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / V2.13.54(含) 新增下單欄位bstrCIDTandem、bstrSettlementMonth。 / 目前下單商品規格有兩種方法: / 1. bstrStockNo帶入TX03。 / 注意：委託TX03如果今年TX03已過期，則會自動委託隔年TX03。 / 2. bstrCIDTandem帶入FITX，bstrSettlementMonth帶入202503。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-9 SendOptionOrder
| 送出選擇權委託，需設盤別。 |
|---|
| 宣告 | Long SendOptionOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-10 SendOverseaFutureOrder
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / 送出海外期貨委託。 |
|---|
| 宣告 | Long SendOverseaFutureOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / SGX專線模式：SGX交易所商品均經由專線委託。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

(SGX DMA)SendOverSeaFutureOrder
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / (SGX DMA專線)送出海外期貨委託。 |
|---|
| 宣告 | Long SendOverSeaFutureOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVRESEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVRESEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示送出委託： / 訊息內容則M000及15碼的英數字委託序號，以空白間隔。 / 回傳值非0表示委託失敗，訊息內容為M999+委託失敗原因 或交易所回傳錯誤。 / 非同步委託：參照4-2-b (SGX DMA)OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由SGX DMA OnAsyncOrder取得。 / SGX專線模式：SGX交易所商品均經由專線委託。 / *實際委託成功與否，請以專線回報資料為主。（EX:委託價格有誤，仍可能收到15碼序號） |

4-2-11 SendOverseaOptionOrder
| <請先執行 4-2-40商品檔下載 SKOrderLib_LoadOOCommodity()> / 送出海外選擇權委託。 |
|---|
| 宣告 | Long SendOverseaOptionOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-12 SendFutureStopLossOrder
| ［限近月商品代碼］送出期貨停損委託。 |
|---|
| 宣告 | Long SendFutureStopLossOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「智慧單主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 回傳成功訊息範例: / EX1: 000,20220419,智慧條件單已送出 條件單號：568157,無委託書號,568157,1689200000003 / EX2: 000,20220420,智慧條件單已送出 條件單號：568353,y0004,568353,1688000000001 |

4-2-13 SendMovingStopLossOrder
| ［限近月商品代碼］送出移動停損委託。 |
|---|
| 宣告 | Long SendMovingStopLossOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。 / 其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-14 SendOptionStopLossOrder
| 送出選擇權停損委託。 |
|---|
| 宣告 | Long SendOptionStopLossOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-15 SendForeignStockOrder
| 送出複委託。 |
|---|
| 宣告 | Long SendForeignStockOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FOREIGNORDER* pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pAsyncOrder | SKCOM元件中的FOREIGNORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / Ver 2.13.46版本以上(含) 新增幣別CNY、GBP；下單市場新增滬深股 (C#範例程式另加港股、日股、新加坡下單選項) / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-16 DecreaseOrderBySeqNo
| 委託減量。 |
|---|
| 宣告 | Long DecreaseOrderBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] LONG nDecreaseQty,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲改量的委託序號。 |
|  | nDecreaseQty | 欲減少的數量。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所有欄位值說明如上方供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託減量回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊亦適用證券逐筆及擬真平台 |

4-2-17 OverSeaDecreaseOrderBySeqNo
| 海期委託減量。 |
|---|
| 宣告 | Long OverSeaDecreaseOrderBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] LONG nDecreaseQty,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲改量的委託序號。 |
|  | nDecreaseQty | 欲減少的數量。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所有欄位值說明如上供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託減量回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 |

(SGX DMA)OverSeaDecreaseOrderBySeqNo
| (SGX DMA 專線) 海期委託減量。需使用15碼英數字委託序號專線減量。 |
|---|
| 宣告 | Long OverSeaDecreaseOrderBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] LONG nDecreaseQty,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲改量的15碼委託序號。 |
|  | nDecreaseQty | 欲減少的數量。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示送出委託，訊息內容則為M000+修改訊息，以空白間隔。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b (SGX DMA)OnAsyncOrder。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託減量回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由(SGX DMA)OnAsyncOrder取得。 / *實際委託成功與否，請以專線回報資料為主。 |

4-2-18 CancelOrderBySeqNo
| 國內委託删單(By委託序號)。 |
|---|
| 宣告 | Long CancelOrderBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲刪除的委託序號。 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為刪單訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所填欄位值說明請確認上方參數欄。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊亦適用證券逐筆及擬真平台 |

4-2-19 CancelOrderByStockNo
| 國內委託删單(By帳號所屬登入ID+商品代號)。 / 函式4-2-108CancelOrderByStockNoAdvance可帶入(商品代號+買賣別+委託價格) |
|---|
| 宣告 | Long CancelOrderByStockNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrStockNo,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrStockNo | 欲刪除的委託商品代號。(帶空字串，可刪除帳號下所有委託) |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為刪單訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所填欄位值說明請確認上方參數欄。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊請留意證券代碼，包含刪除盤中零股委託 |

| 異動前v2.13.45 | 欄位 | 異動後V2.13.52 |
|---|---|---|
| 欲刪除的委託商品代號，請輸入有效商品代碼。 | bstrStockNo / 欄位說明： / 帶空字串，可刪除帳號下所有委託。 | 商品代碼為空，則為刪除全部委託。 |

4-2-20 CancelFutureStopLoss
| 取消期貨停損委託。 / 舊用戶，若您的委託已產生書號，為避免影響解除保證金風控，建議改用4-2-87 並填入書號。 / 新用戶請參考4-2-87。 |
|---|

4-2-21 CancelMovingStopLoss
| 取消移動停損委託。 / 舊用戶，若您的委託已產生書號，為避免影響解除保證金風控，建議改用4-2-87 並填入書號。 / 新用戶請參考4-2-87。 |
|---|

4-2-22 CancelOptionStopLoss
| 取消選擇權停損委託。 / 舊用戶，若您的委託已產生書號，為避免取消失敗，建議改用4-2-87 並填入書號。 / 新用戶請參考4-2-87。 |
|---|

4-2-25 CorrectPriceBySeqNo
| 證期權依序號改價。 |
|---|
| 宣告 | Long CorrectPriceBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] BSTR bstrPrice, [in] LONG nTradeType,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲改量的委託序號。 |
|  | bstrPrice | 修改價格。 |
|  | nTradeType | 證券0:ROD / 期選0:ROD  1:IOC  2:FOK |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託改價回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊證券逐筆nTradeType:請設0(ROD). / ＊不適用盤中零股 |

4-2-26 CorrectPriceByBookNo
| 證期權依書號改價。 |
|---|
| 宣告 | Long CorrectPriceByBookNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrMarketSymbol, [in] BSTR bstrBookNo, [in] BSTR bstrPrice, [in] LONG nTradeType,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrMarketSymbol | 市場類別 TS: 證券TF:期貨 TO:選擇權 |
|  | bstrBookNo | 欲改量的委託書號。 |
|  | bstrPrice | 修改價格。 |
|  | nTradeType | 證券0:ROD / 期選0:ROD  1:IOC  2:FOK |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託改價回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊證券逐筆nTradeType:請設0(ROD). / ＊不適用盤中零股 |

4-2-27 SendOverseaFutureSpreadOrder
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / 送出海外期貨價差委託。 |
|---|
| 宣告 | Long SendOverseaFutureSpreadOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中(價差不提供當沖)。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / V2.13.54後，不再提供此函示，請改使用OverseaFutureSpreadOrder2，謝謝。 / 注意OverseaFutureSpreadOrder2 買賣別判斷有更動。請參考 2-10 海期價差單買賣別判斷更動說明。 |

(SGX DMA)SendOverSeaFutureSpreadOrder
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / SGX DMA專線模式 送出海外期貨價差委託。 |
|---|
| 宣告 | Long SendOverSeaFutureSpreadOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVRESEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVRESEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中(價差不提供當沖)。 |
|  | bstrMessage | 同步委託：送出委託，訊息內容則M000及15碼的英數字委託序號，以空白間隔。 / 回傳值非0表示委託失敗，訊息內容為M999+委託失敗原因 或交易所回傳錯誤。 / 非同步委託：參照4-2-b (SGX DMA)OnAsyncOrder。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 使用非同步委託，委託結果請由(SGX DMA) OnAsyncOrder取得。 / SGX專線模式：SGX交易所商品均經由專線委託。 / *實際委託成功與否，請以專線回報資料為主。 |

4-2-28 GetRealBalanceReport
| 查詢證券即時庫存內容。 |
|---|
| 宣告 | Long GetRealBalanceReport([in] BSTR bstrLogInID, [in] BSTR bstrAccount); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 證券帳號，分公司四碼＋帳號7碼。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由OnRealBalanceReport事件回傳。 |

4-2-29 GetOpenInterest
| 查詢期貨未平倉。 |
|---|
| 宣告 | Long GetOpenInterest([in] BSTR bstrLogInID, [in] BSTR bstrAccount); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOpenInterest 事件回傳。 |

4-2-30 GetOverseaFutureOpenInterest
| 查詢海外期貨未平倉。 |
|---|
| 宣告 | Long GetOverseaFutureOpenInterest([in] BSTR bstrLogInID, [in] BSTR bstrAccount); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOverseaFutureOpenInterest 事件回傳。 |

4-2-31 GetStopLossReport
| 新版期貨停損委託單查詢。 |
|---|
| 宣告 | Long GetStopLossReport([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] LONG nReportStatus, [in] BSTR bstrKind, [in] BSTR bstrDate); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 (帳號) 。 |
|  | nReportStatus | 0:全部的委託單。 |
|  | bstrKind | 智慧單類型 / STP:一般停損（含選擇權停損）(長效單) / MST:移動停損 / OCO:二擇一(長效單) / MIT(含選擇權MIT) / AB：AB單 |
|  | bstrDate | 查詢日期(ex:20220601) |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnStopLossReport 事件回傳。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / *V2.13.38版本起配合智慧單平台異動至新版本 |

4-2-32 GetOverseaFutures
| 查詢海外期貨下單商品。 |
|---|
| 宣告 | Long GetOverseaFutures(); |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOverseaFuture 事件回傳。 |

4-2-33 GetOverseaOptions
| 查詢海外選擇權下單商品。 |
|---|
| 宣告 | Long GetOverseaOptions(); |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOverseaOption 事件回傳。 |

4-2-34 UnlockOrder
| 下單解鎖。下單函式上鎖後需經由此函式解鎖才可繼續下單。 |
|---|
| 宣告 | Long UnlockOrder([in] LONG nMarketType); |
| 參數 | nMarketType | 0：TS (證券) / 1：TF(期貨) / 2：TO(選擇權) / 3：OS(複委託) / 4：OF(海外期貨) / 5：OO(海外選擇權) |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 |  |

4-2-35 OverSeaCancelOrderBySeqNo
| 海外期貨委託删單(By委託序號)。支援SGX DMA專線 |
|---|
| 宣告 | Long OverSeaCancelOrderBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲刪除的委託序號。 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為原始委託13碼。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 |

比較表
| 異動前 | 欄位 | 異動後V2.13.45 |
|---|---|---|
| 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。 | 參數 / bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為原始委託 / 13碼。 |

(SGX DMA)OverSeaCancelOrderBySeqNo
| 海外期貨委託删單(By委託序號)。支援SGX DMA專線 |
|---|
| 宣告 | Long OverSeaCancelOrderBySeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲刪除的15碼委託序號。 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示送出刪單成功，訊息內容則為M000與 刪單訊息(EX:刪單已送出，結果請由回報查詢確認)，以空白間隔。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b SGX DMA OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / SGX專線模式：15碼委託序號由專線刪單 / *實際刪單成功與否，請以專線回報資料為主。 |

4-2-36 OverSeaCancelOrderByBookNo
| 海外期貨委託删單(By委託書號)。不支援SGX DMA專線 |
|---|
| 宣告 | Long OverSeaCancelOrderByBookNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrBookNo,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrBookNo | 欲刪除的書號。 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為原始委託13碼。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 不支援SGX DMA專線模式。 |

比較表
| 異動前 | 欄位 | 異動後V2.13.45 |
|---|---|---|
| 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。 | 參數 / bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為原始 / 13碼。 |

4-2-37 SKOrderLib_GetLoginType
| 查詢登入帳號類型。 |
|---|
| 宣告 | Long SKOrderLib_GetLoginType([in] BSTR bstrLogInID); |
| 參數 | bstrLogInID | 登入帳號。 |
| 回傳值 | 0：一般帳號，1：VIP帳號。 |
| 備註 |  |

4-2-38 SKOrderLib_GetSpeedyType
| 查詢登入帳號下單線路。 |
|---|
| 宣告 | Long SKOrderLib_GetSpeedyType([in] BSTR bstrLogInID); |
| 參數 | bstrLogInID | 登入帳號。 |
| 回傳值 | 0：一般線路，1：Speedy線路。 |
| 備註 |  |

4-2-39 SKOrderLib_LoadOSCommodity
| SKOrderLib 下載海期商品檔。取得海期帳號才可讀取商品檔，進行海期委託下單。 |
|---|
| 宣告 | Long SKOrderLib_LoadOSCommodity(); |
| 參數 | 無。 |  |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示初始失敗。錯誤代碼可參考對照表。 |
| 備註 | 具海期帳號，海期委託下單前須先下載 / ▼以下LOG（日期_OSQuote.log）為確認是否下載海期商品檔LoadOSCommdity，下圖代表有海期帳號，預設Login會占用一條報價連線（海期） |

4-2-40 SKOrderLib_LoadOOCommodity
| SKOrderLib 下載海選商品檔、可交易商品檔。取得海期帳號才可讀取海選商品檔、海選可交易商品資訊，進行海選委託下單。 |
|---|
| 宣告 | Long SKOrderLib_LoadOOCommodity(); |
| 參數 | 無。 |  |
| 回傳值 | 0表示初始化成功，其餘非0數值都表示初始失敗。錯誤代碼可參考對照表。 |
| 備註 | 具海期帳號，海選委託下單前須先下載 |

4-2-41 GetFutureRights
| 查詢國內權益數。 |
|---|
| 宣告 | Long GetFutureRights([in] BSTR bstrLogInID, [in] BSTR bstrAccount,[in]SHORT sCoinType) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | sCoinType | 0:全幣別，1:基幣(台幣TWD)，2:人民幣RMB。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnFutureRights 事件回傳。 / 全幣別選項含基幣。 |

4-2-42 GetRequestProfitReport
| 證券即時損益試算。 / 請使用新損益試算查詢4-2-73 GetProfitLossGWReport |
|---|

4-2-43 SendFutureOCOOrder
| ［限近月商品代碼］送出期貨二擇一委託。 |
|---|
| 宣告 | Long SendFutureOCOOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREOCOORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREOCOORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「智慧單主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-44 CancelFutureOCO
| 刪除期貨二擇一停損委託。 / 舊用戶，若您的委託已產生書號，為避免影響解除保證金風控，建議改用4-2-87 並填入書號。 / 新用戶請參考4-2-87。 |
|---|

4-2-45 GetRequestOverSeaFutureRight
| 查詢海外期貨權益數。 |
|---|
| 宣告 | Long GetRequestOverSeaFutureRight ([in] BSTR bstrLogInID, [in] BSTR bstrAccount) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號)。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOverSeaFutureRight 事件回傳。 |

4-2-46 GetMarginPurchaseAmountLimit
| 資券配額查詢。 |
|---|
| 宣告 | Long GetMarginPurchaseAmountLimit ([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] BSTR bstrStockNo) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 證券帳號，分公司四碼＋帳號7碼。 |
|  | bstrStockNo | 商品代碼，代空為全部商品回傳 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnMarginPurchaseAmountLimit 事件回傳。 |

4-2-47 GetBalanceQuery
| 集保庫存查詢。 |
|---|
| 宣告 | Long GetBalanceQuery ([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] BSTR bstrStockNo) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 證券帳號，分公司四碼＋帳號7碼，帶空為所有證券帳號查詢。 |
|  | bstrStockNo | 商品代碼，代空為全部商品回傳 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnBalanceQuery 事件回傳。 / V2.13.54後，不再提供此函示，請改使用GetRealBalanceReport，謝謝。 |

4-2-48 SendFutureMITOrder
| ［限近月商品代碼］送出期貨MIT委託。 |
|---|
| 宣告 | Long SendFutureMITOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 / （此類型委託中的觸發價與成交價、觸價方向為必要欄位，請務必填入） |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-49 SendOptionMITOrder
| 送出選擇權MIT委託。 |
|---|
| 宣告 | Long SendOptionMITOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 / （此類型委託中的觸發價與成交價、觸價方向為必要欄位，請務必填入） |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-50 CancelFutureMIT
| 取消期貨MIT委託。 / 舊用戶，若您的委託已產生書號，為避免影響解除保證金風控，建議改用4-2-87 並填入書號。 / 新用戶請參考4-2-87。 |
|---|

4-2-51 CancelOptionMIT
| 取消選擇權MIT委託。 / 舊用戶，若您的委託已產生書號，為避免影響解除保證金風控，建議改用4-2-87 並填入書號。 / 新用戶請參考4-2-87。 |
|---|

4-2-52 SendDuplexOrder
| 送出國內選擇權複式單委託。 |
|---|
| 宣告 | Long SendDuplexOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非數值 表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 複式單下法 (FUTUREORDER 物件填法，以上圖為例): / Call多頭價差(策略王上溫漲)，買低履約價+賣高履約價 / 第一隻腳: bstrStockNo 填:高履約價(ex:11000)，sBuySell填:賣 / 第二隻腳: bstrStockNo2 填:低履約價(ex:10900)，sBuySell2填:買 / bstrPrice填法基本上為:bstrStockNo2賣價(99)–bstrStockNo買價(46.5)= 52.5 / 如果bstrPrice價格填的越大，越能立即成交，假設目前價差52,5，超過52.5那就能幾乎能立即成交(快速直覺:Call作多價格越高買就相當於市價買進，越容易成交)。 / Call空頭價差(策略王上溫跌作莊) / 第一隻腳: bstrStockNo 填:高履約價(ex:11000)，sBuySell填:買 / 第二隻腳: bstrStockNo2 填:低履約價(ex:10900)，sBuySell2填:賣 / bstrPrice填法基本上為:bstrStockNo2買價(97)–bstrStockNo賣價(47)= 50 / 如果bstrPrice價格填的越小，越能立即成交，假設目前價差50，小於50那就能幾乎能立即成交(快速直覺:Call作空價格越低賣就相當於賤價賣，越容易成交)。 / Put多頭價差(策略王上溫漲作莊) / 第一隻腳: bstrStockNo 填:低履約價(ex:10900)，sBuySell填:買 / 第二隻腳: bstrStockNo2 填:高履約價(ex:11000)，sBuySell2填:賣 / bstrPrice填法基本上為:bstrStockNo2買價(109)–bstrStockNo賣價(64)= 45 / 如果bstrPrice價格填的越小，越能立即成交，假設目前價差45，小於45那就能幾乎能立即成交(快速直覺:Put作多價格越低作莊就相當於賤價賣，越容易成交)。 / Put空頭價差(策略王上溫跌) / 第一隻腳: bstrStockNo 填:低履約價(ex:10900)，sBuySell填:賣 / 第二隻腳: bstrStockNo2 填:高履約價(ex:11000)，sBuySell2填:買 / bstrPrice填法基本上為:bstrStockNo2賣價(113)–bstrStockNo買價(62)= 51 / 如果bstrPrice價格填的越大，越能立即成交，假設目前價差51，大於51那就能幾乎能立即成交(快速直覺:Put作多價格越高買就相當於市價買，越容易成交)。 / 突破 / 第一隻腳: bstrStockNo填:call/put階可(不同履約價也可)，sBuySell填:買 / 第二隻腳: bstrStockNo2填: call/put階可(不同履約價也可)，sBuySell2填:買 / bstrPrice填法基本上為:bstrStockNo2賣價(99)+bstrStockNo賣價(113)= 212 / 如果bstrPrice價格填的越大，越能立即成交，假設目前價差212，大於212那就能幾乎能立即成交。 / 盤整 / 第一隻腳: bstrStockNo填: call/put階可(不同履約價也可)，sBuySell填:賣 / 第二隻腳: bstrStockNo2填: call/put階可(不同履約價也可)，sBuySell2填:賣 / bstrPrice填法基本上為:bstrStockNo2買價(97)+bstrStockNo買價(109)= 206 / 如果bstrPrice價格填的越小，越能立即成交，假設目前價差206，小於206那就能幾乎能立即成交。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-53 AddSGXAPIOrderSocket
| 建立SGX API專線。 / 注意，SGX API DMA專線需先向交易後台申請，方可使用。 |
|---|
| 宣告 | Long AddSGXAPIOrderSocket ([in] BSTR bstrLogInID, [in] BSTR bstrAccount); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  |  |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 連線結果請由OnNotifySGXAPIOrderStatus取得。 |

4-2-54 CancelOrderByBookNo
| 國內委託删單(By書號)。 |
|---|
| 宣告 | Long CancelOrderByBookNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrBookNo,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrBookNo | 欲刪除的委託書號。 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為刪單訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 |

4-2-55 SendTXOffset
| 大小台互抵。 |
|---|
| 宣告 | Long SendTXOffset([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrYearMonth, [in] LONG nBuySell, [in] LONG nQty, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrYearMonth | 大小台年月(YYYYMM)。 |
|  | nBuySell | 大台買賣別。0:多方(買) 1:空方(賣) |
|  | nQty | 互抵口數，以大台口數為基本單位。 |
|  | bstrMessage | 同步：如果回傳值為 0表示委託成功，訊息內容則為互抵訊息。回傳值非0表示互抵失敗，訊息內容為失敗原因。 / 非同步：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所有欄位值說明如上方供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託內容請致電交易室查詢， / 回傳值非0時表示失敗，請參考訊息確認錯誤原因。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 大小台互抵需要1口大台抵4口小台，且大小台需互為反向部位、相同月份，才可以互抵成功。 |

4-2-56 AssembleOptions
| 國內選擇權組合部位。 |
|---|
| 宣告 | Long AssembleOptions([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 / v2.13.55起新增非同步。v2.13.55前限同步委託。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將組合條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，表示委託已送出。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | ◆委託欄位，請參考期權下單物件說明，請留意需有可組合選擇權部位。 / ★非交易的行為，無回報，可由未平倉查詢確認部位。 / 因組合/拆解/了結功能是直送結算所，需等處理結果回傳後，方可再進行下一筆。 |

4-2-57 DisassembleOptions
| 國內選擇權複式單拆解。 |
|---|
| 宣告 | Long DisassembleOptions([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 / v2.13.55起新增非同步。v2.13.55前限同步委託。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將拆解條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，表示委託已送出。其餘非數值 表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「交易主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | ◆委託欄位，請參考期權下單物件說明，請留意需有複式單部位。 / ★非交易的行為，無回報，可由未平倉查詢確認部位。 / 因組合/拆解/了結功能是直送結算所，需等處理結果回傳後，方可再進行下一筆。 |

4-2-58 CoverAllProduct
| 國內選擇權雙邊部位了結。 |
|---|
| 宣告 | Long CoverAllProduct([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 / v2.13.55起新增非同步。v2.13.55前限同步委託。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將了結條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，表示委託已送出。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「交易主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | ◆委託欄位，請參考期權下單物件說明，請留意需具有雙邊部位。 / ★非交易的行為，無回報，可由未平倉查詢確認部位。 / 因組合/拆解/了結功能是直送結算所，需等處理結果回傳後，方可再進行下一筆。 |

4-2-59 GetOpenInterestWithFormat
| 查詢期貨未平倉－可指定回傳格式。 |
|---|
| 宣告 | Long GetOpenInterestWithFormat([in] BSTR bstrLogInID, [in] BSTR bstrAccount,[in] LONG nFormat); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | nFormat | 回傳格式：(1~3)。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOpenInterest 事件回傳。 / 新增參數：指定回傳格式（內容參考OnOpenInterest） / 1［完整］；2［格式1］；3［格式2-含損益］。 |

4-2-60　WithDraw
| 國內外出入金互轉。 |
|---|
| 宣告 | Long WithDraw ([in] BSTR bstrLogInID, [in] BSTR bstrFullAccountOut, [in] LONG nTypeOut, [in] BSTR bstrFullAccountIn, [in] LONG nTypeIn, [in] LONG nCurrency, [in] BSTR bstrDollars, [in] BSTR bstrPassword, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrFullAccountOut | 轉出期貨帳號(分公司代碼＋帳號7碼) |
|  | nTypeOut | 轉出類別(0:國內；1:國外) |
|  | bstrFullAccountIn | 轉入期貨帳號(分公司代碼＋帳號7碼) |
|  | nTypeIn | 轉入類別(0:國內；1:國外) |
|  | nCurrency | 幣別(0~9；詳閱－備註) |
|  | bstrDollars | 金額 |
|  | bstrPassword | 出入金密碼 |
|  | bstrMessage | 非同步：參照4-2-b OnAsyncOrder。 / 如果回傳值為 0表示互轉成功，訊息內容則為送單之Thread ID，表示互轉已送出。 / 回傳值非0表示互轉失敗，訊息內容為失敗原因。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 幣別： |

4-2-61　SendStockStrategyDayTrade
| 送出證券智慧單當沖條件委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyDayTrade ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「智慧單主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-62　SendStockStrategyClear
| 送出證券智慧單出清條件委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyClear ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDEROUT* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDEROUT物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-63　GetTSSmartStrategyReport
| 查詢證券智慧單。 |
|---|
| 宣告 | Long GetTSSmartStrategyReport ([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] BSTR bstrMarketType, [in] LONG nReportStatus, [in] BSTR bstrKind, [in] BSTR bstrDate); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 (帳號) 。 |
|  | bstrMarketType | TS :證券市場 |
|  | nReportStatus | 0:全部的委託單。 |
|  | bstrKind | 智慧單類型 / DayTrade:當沖 / ClearOut:出清 / MIT：觸價單、MIT長效單 / OCO：二擇一 / MIOC：多次IOC / MST：移動停損、MST長效單 / AB：看A下B單 / CB：自組單 |
|  | bstrDate | 查詢日期(ex:20201001) |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnTSSmartStrategyReport 事件回傳。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

*V2.13.48版本起移除MMIT、MBA、LLS智慧單查詢
4-2-64　CancelTSStrategyOrder
| 取消證券智慧單委託。欄位請參考GetTSSmartStrategyReport 回傳的內容。 / 注意，當已經觸發的智慧單，將無法取消委託。 |
|---|
| 宣告 | Long CancelTSStrategyOrder ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSmartKey, [in] Long nTradeKind, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( BrokerID＋帳號) 。 |
|  | bstrSmartKey | 智慧單序號或進場單號。 |
|  | nTradeKind | 智慧單類型 / 6:MIOC;7:MST(非長效單); 8:MIT(非長效單)。 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為智慧單號刪單訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 刪單後，請透過智慧單被動回報確認狀態。 / *當沖單/出清單/OCO二擇一: 請用CancelTSStrategyOrderV1 |

4-2-65　OverSeaCorrectPriceByBookNo
| 海期改價 (By 委託書號)。 |
|---|
| 宣告 | Long OverSeaCorrectPriceByBookNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDERFORGW* pOrder , [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDERFORGW物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *原委託為限價單方可改價(ROD) |

4-2-66　OverSeaCorrectPriceSpreadByBookNo
| 海期價差改價 (By 委託書號)。 |
|---|
| 宣告 | Long OverSeaCorrectPriceSpreadByBookNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDERFORGW* pOrder , [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDERFORGW物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *原委託為限價單方可改價(ROD) |

4-2-67　OverSeaOptionCorrectPriceByBookNo
| 海選改價 (By 委託書號)。 |
|---|
| 宣告 | Long OverSeaOptionCorrectPriceByBookNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDERFORGW* pOrder , [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDERFORGW物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *原委託為限價單方可改價(ROD) / *請填海選商品Call/Put 、履約價欄位。 |

| 異動前 | 欄位 | 異動後V2.13.45 |
|---|---|---|
| *僅支援自然人身份，尚未支援特殊身份(群組及子帳) | 備註 / 刪除身份限制描述 | 如上方表格 |

4-2-68 SendOverseaFutureOrderOLID
| 送出海外期貨委託。(含單獨自訂資料欄) |
|---|
| 宣告 | Long SendOverseaFutureOrderOLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder, [in] BSTR bstrOrderLinkedID,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / SGX專線模式：SGX交易所商品均經由專線委託。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-69 SendOverseaFutureSpreadOrderOLID
| 送出海外期貨價差委託。(含單獨自訂資料欄) |
|---|
| 宣告 | Long SendOverseaFutureSpreadOrderOLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder, [in] BSTR bstrOrderLinkedID,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中(價差不提供當沖)。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / SGX專線模式：SGX交易所商品均經由專線委託 / V2.13.54後，不再提供此函示，請改使用SendOverseaFutureSpreadOrder2OLID，謝謝。 / 注意SendOverseaFutureSpreadOrder2OLID買賣別判斷有更動。請參考 2-10 海期價差單買賣別判斷更動說明。 |

4-2-70 (SGX DMA)OverSeaCorrectPriceBySGXAPISeqNo
| (SGX DMA專線)送出海外期貨改價委託。 |
|---|
| 宣告 | Long OverSeaCorrectPriceBySGXAPISeqNo([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] BSTR bstrCorrectPrice, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲改價的15碼委託序號。 |
|  | bstrCorrectPrice | 修改價格。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示送出改價，訊息內容則為M000與改價訊息(EX:「改價已送出，結果請由回報查詢確認」，以空白間隔。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b (SGX DMA)OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由SGX DMA OnAsyncOrder取得。 / *實際改價成功與否，請以專線回報資料為主。 |

4-2-71 SendStockStrategyMIT
| 送出證券智慧單MIT條件委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyMIT ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDERMIT* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDERMIT物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 自V2.13.30(含)版本起，客戶可自設觸價方向(物件5-19)，相關回報部分：被動回報4-2-n，主動回報請參考4-3-m。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 / 預風控說明：此功能將於主機收到智慧單委託後立刻進行風控計算，可以大幅縮短觸價至送出委託的所耗時間。 / 預風控注意事項：此功能會在觸發前預先扣除條件中委託所使用之額度(包括能夠進行操作的庫存)，使用時請務必留意是否有進行中的MIT智慧單與委託，在未達成條件前取消智慧單委託可釋放占用額度。 / V2.13.51版本，如在非交易時間(08；30~13:25)交易，則為預約單。 / 盤中單 ：08:30~13:25 / 其餘時間：預約單 |

4-2-72 SendStockStrategyOCO
| 送出證券智慧單二擇一OCO條件委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyOCO ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDEROCO* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDEROCO物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-73 GetProfitLossGWReport
| 未實現損益試算。 |
|---|
| 宣告 | Long GetProfitLossGWReport ([in] BSTR bstrLogInID, [in] struct TSPROFITLOSSGWQUERY* pPLGWQuery); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pPLGWQuery | SKCOM元件中的TSPROFITLOSSGWQUERY物件，將未實現損益查詢條件填入該物件後，再帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 結果由OnProfitLossGWReport事件回傳。 |

| 已實現損益查詢。 |
|---|
| 宣告 | Long GetProfitLossGWReport ([in] BSTR bstrLogInID, [in] struct TSPROFITLOSSGWQUERY* pPLGWQuery); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pPLGWQuery | SKCOM元件中的TSPROFITLOSSGWQUERY物件，將未實現損益查詢條件填入該物件後，再帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 結果由OnProfitLossGWReport事件回傳。 / 彙總：起始日西元年月日及結束日西元年月日 / 明細：起始日民國年月日，不需填結束日 |

| 現股當沖損益試算。 |
|---|
| 宣告 | Long GetProfitLossGWReport ([in] BSTR bstrLogInID, [in] struct TSPROFITLOSSGWQUERY* pPLGWQuery); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pPLGWQuery | SKCOM元件中的TSPROFITLOSSGWQUERY物件，將未實現損益查詢條件填入該物件後，再帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 結果由OnProfitLossGWReport事件回傳。 / 限查當日交易 / 損益回傳格式 1: 彙總 2: 明細 |

4-2-74 SendStockStrategyMIOC
| 送出證券智慧單多次IOC條件委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyMIOC ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDERMIOC* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDERMIOC物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-75 SendStockStrategyMST
| 送出證券智慧單移動停損條件委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyMST ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDERMIT* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDERMIT物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 自V2.13.30(含)版本起，客戶可選擇是否自訂啟動價格、自訂啟動觸價方向等條件(詳參考，物件5-19)，相關回報部分：被動回報4-2-n，主動回報請參考4-3-m。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-76 GetOverseaFutureOpenInterestGW
| 查詢海外期貨未平倉GW。(含格式彙總及明細) |
|---|
| 宣告 | Long GetOverseaFutureOpenInterestGW([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] LONG nFormat); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | nFormat | 查詢格式(1: 彙總;2: 明細) |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOFOpenInterestGWReport 事件回傳。 |

4-2-77 SKOrderLib_PingandTracertTest
| 測試API使用到的主機是否正常 |
|---|
| 宣告 | Long SKOrderLib_PingandTracertTest(); |
| 參數 |  |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 適用於出現微軟https/socket error 可以確認與主機連線狀態 / 可在LOG檔「PingTest」看到測試結果 / 請注意：檔案內看到TracertTest -> XXX 才算測試完成。 |

4-2-78 SendStockOddLotOrder
| 送出證券盤中零股委託。 |
|---|
| 宣告 | Long SendStockOddLotOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 STOCKORDER 物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-79 SendTFOffSet
| 指定大小台/電/金互抵。 |
|---|
| 宣告 | Long SendTFOffset([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] LONG nCommodity, [in] BSTR bstrYearMonth, [in] LONG nBuySell, [in] LONG nQty, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | nCommodity | 指定商品0:大小台; 1:大小電; 2:大小金 |
|  | bstrYearMonth | 大小台(電、金)年月(YYYYMM)。 |
|  | nBuySell | 大台(電、金)買賣別。0:多方(買) 1:空方(賣) |
|  | nQty | 互抵口數，以大台(電、金)口數為基本單位。 |
|  | bstrMessage | 同步：如果回傳值為 0表示委託成功，訊息內容則為互抵訊息。回傳值非0表示互抵失敗，訊息內容為失敗原因。 / 非同步：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託內容請致電交易室查詢， / 回傳值非0時表示失敗，請參考訊息確認錯誤原因。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / EX:互抵需要1口大台抵4口小台(金)或一口大電抵8口小電，且大小台(電、金)需互為反向部位、相同月份，才可以互抵成功。 |

4-2-80 SKOrderLib_LogUpload
| LOG上傳。 |
|---|
| 宣告 | Long SKOrderLib_LogUpload (); |
| 參數 |  |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 可用此功能上傳近3日LOG。 / 上傳後，LOG路徑會生成一個資料夾和一個zip，資料夾內為上傳的檔案。 |

4-2-81 SKOrderLib_TelnetTest
| 用Telnet指令測試與API使用到的主機連線是否正常。 |
|---|
| 宣告 | Long SKOrderLib_TelnetTest (); |
| 參數 |  |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 結果由 OnTelnetTest 事件回傳。 |

4-2-82 CancelTSStrategyOrderV1
| 取消證券智慧單委託新版本。刪單欄位請參考GetTSSmartStrategyReport 回傳的內容。 |
|---|
| 宣告 | Long CancelTSStrategyOrderV1 ([in] BSTR bstrLogInID, [in] struct CANCELSTRATEGYORDER*  pCancelOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pCancelOrder | 智慧單刪單物件(參考5-24) |
|  | bstrMessage | 非同步刪單： / 參照4-2-b OnAsyncOrder。 / 如果回傳值為 0表示刪單成功，訊息內容則為刪單之Thread ID。 / 回傳值非0表示刪單失敗，訊息內容為失敗原因。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 刪單後，請透過智慧單被動回報確認狀態。 |

4-2-83 SendFutureSTPOrderV1
| (指定月份需填商品契約年月)新版—送出期貨停損委託。 |
|---|
| 宣告 | Long SendFutureSTPOrderV1([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 回傳成功訊息範例: / EX1: 000,20220419,智慧條件單已送出 條件單號：568157,無委託書號,568157,1689200000003 / EX2: 000,20220420,智慧條件單已送出 條件單號：568353,y0004,568353,1688000000001 |

4-2-84 SendFutureMSTOrderV1
| (指定月份需填商品契約年月)新版—送出移動停損委託。 |
|---|
| 宣告 | Long SendFutureMSTOrderV1([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員 / ，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-85 SendFutureMITOrderV1
| (指定月份需填商品契約年月)新版—送出期貨MIT委託。 |
|---|
| 宣告 | Long SendFutureMITOrderV1([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 / （此類型委託中的觸發價與成交價、觸價方向為必要欄位，請務必填入） |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-86 SendFutureOCOOrderV1
| (指定月份需填商品契約年月)新版—送出期貨二擇一委託。 |
|---|
| 宣告 | Long SendFutureOCOOrderV1([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-87　CancelTFStrategyOrderV1
| 新版—取消期貨智慧單委託。 / 已產生書號之委託，請填入書號，否則可能影響解除保證金風控。 |
|---|
| 宣告 | Long CancelTFStrategyOrderV1 ( [in] struct CANCELSTRATEGYORDER*  pCancelOrder, [in] VARIANT_BOOL bAsyncOrder, [out] BSTR* bstrMessage); |
|  | pCancelOrder | 智慧單刪單物件(參考5-26) / (委託成功可取得完整刪單訊息：智慧單號 、13碼序號、 書號) |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrMessage | 非同步刪單： / 參照4-2-b OnAsyncOrder。 / 如果回傳值為 0表示刪單成功，訊息內容則為刪單之Thread ID。 / 回傳值非0表示刪單失敗，訊息內容為失敗原因。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 |

4-2-88 CancelForeignStockOrder
| 新版-複委託刪單(需同時填序號及委託書號)。 |
|---|
| 宣告 | Long CancelForeignStockOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FOREIGNORDER* pOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FOREIGNORDER物件，將刪單條件填入該物件後，再帶入此欄位中。 / *參考5-3-2說明 |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為修改訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 |

4-2-89 GetOpenInterestGW
| 查詢期貨未平倉－可指定回傳格式。 |
|---|
| 宣告 | Long GetOpenInterestGW([in] BSTR bstrLogInID, [in] BSTR bstrAccount,[in] LONG nFormat); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | nFormat | 回傳格式：1。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOpenInterest 事件回傳。 / 新增參數：指定回傳格式（內容參考OnOpenInterest） |

4-2-90 SendStockStrategyAB
| 送出證券智慧單看A下B單委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyAB ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDERMIT* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDERMIT物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 相關回報部分：被動回報4-2-n，主動回報請參考4-3-m。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-91 SendStockStrategyCB
| 送出證券智慧單自組單委託。 / 智慧單用戶請注意備註。 |
|---|
| 宣告 | Long SendStockStrategyCB ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct STOCKSTRATEGYORDER * pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的STOCKSTRATEGYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、條件單號 (智慧單序號)等委託資料。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 相關回報部分：被動回報4-2-n，主動回報請參考4-3-m。 / ［智慧單用戶請注意］：智慧單為獨立運作機制，若盤中已自行出清或回補，請務必於盤中自行取消已設定的智慧單，避免條件觸發後重覆成交。 |

4-2-92 SendFutureABOrder
| (指定月份需填商品契約年月)新版—送出期貨看A下B委託。 |
|---|
| 宣告 | Long SendFutureABOrder ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的FUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署期貨智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 回傳成功訊息範例: / EX1: 000,20220419,智慧條件單已送出 條件單號：568157,無委託書號,568157,1689200000003 / EX2: 000,20220420,智慧條件單已送出 條件單號：568353,y0004,568353,1688000000001 |

4-2-93 SendOverSeaFutureOCOOrder
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / 送出海外期貨OCO(含長效)委託。 |
|---|
| 宣告 | Long SendOverSeaFutureOCOOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 |

4-2-94 SendOverSeaFutureABOrder
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / 送出海外期貨AB單委託。 |
|---|
| 宣告 | Long SendOverSeaFutureABOrder([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 |

4-2-95 CancelOFStrategyOrder
| 取消海期智慧單委託。刪單欄位請參考GetOFSmartStrategyReport回傳的內容。 |
|---|
| 宣告 | Long CancelOFStrategyOrder ([in] BSTR bstrLogInID, [in] struct CANCELSTRATEGYORDER*  pCancelOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pCancelOrder | 智慧單刪單物件(參考5-24) |
|  | bstrMessage | 非同步刪單： / 參照4-2-b OnAsyncOrder。 / 如果回傳值為 0表示刪單成功，訊息內容則為刪單之Thread ID。 / 回傳值非0表示刪單失敗，訊息內容為失敗原因。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 刪單後，請透過智慧單被動回報確認狀態。 |

4-2-96 GetOFSmartStrategyReport
| 查詢海期智慧單。 |
|---|
| 宣告 | Long GetOFSmartStrategyReport ([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] BSTR bstrMarketType, [in] LONG nReportStatus, [in] BSTR bstrKind, [in] BSTR bstrDate); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 委託帳號 (帳號) 。 |
|  | bstrMarketType | OF :海期市場 |
|  | nReportStatus | 0:全部的委託單。 |
|  | bstrKind | 智慧單類型 / OCO：二擇一 / AB：AB單 |
|  | bstrDate | 查詢日期(ex:20201001) |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 結果由 OnOFSmartStrategyReport 事件回傳。 |

4-2-97 GetOrderReport
| 委託回報查詢 |
|---|
| 宣告 | void GetOrderReport([in] BSTR bstrLogInID, [in] BSTR bstrFullAccount, [in] LONG nFormat, [out,retval] BSTR* bstrResult); |
| 參數 | bstrLogInID | 登入帳號。 |
|  | bstrFullAccount | 完整帳號。 / 分公司代號 + 交易帳號 |
|  | nFormat | 查詢種類 / 1:全部 2:有效 3:可消 4:已消 5:已成 6:失敗7:合併同價格 8:合併同商品 9:預約 |
|  | bstrResult | 委託回報內容 |
| 回傳值 | 委託回報內容 / 回傳格式：以 ”,” 當分隔符號，以 ”0x0D 0x0A”(\r\n)當每筆的段落。 / 回傳格式參考 / 非正常委託內容時 / M999: 查詢錯誤 / M003: 查無資料 |
| 備註 | 查詢為阻塞式，如為避免查詢造成應用程式停頓，請以執行緒方式進行查詢 / 限制每次查詢間需間隔五秒 / 回報不含盤中零股 / 回傳內容如有中文，編碼為 UTF-8 / 市場別為國內證期選 |

4-2-98 GetFulfillReport
| 成交回報查詢 |
|---|
| 宣告 | void GetFulfillReport([in] BSTR bstrLogInID, [in] BSTR bstrFullAccount, [in] LONG nFormat, [out,retval] BSTR* bstrResult); |
| 參數 | bstrLogInID | 登入帳號。 |
|  | bstrFullAccount | 完整帳號。 / 分公司代號 + 交易帳號 |
|  | nFormat | 查詢種類 / 1:完整 2:合併同書號 3:合併同價格 4:合併同商品 / 5:T+1成交回報 |
|  | bstrResult | 委託回報內容 |
| 回傳值 | 成交回報內容 / 回傳格式：以 ”,” 當分隔符號，以 ”0x0D 0x0A”(\r\n)當每筆的段落。 / 回傳格式參考 / 非正常委託內容時 / M999: 查詢錯誤 / M003: 查無資料 |
| 備註 | 查詢為阻塞式，如為避免查詢造成應用程式停頓，請以執行緒方式進行查詢 / 限制每次查詢間需間隔五秒 / 回報不含盤中零股 / 回傳內容如有中文，編碼為 UTF-8 / 市場別為國內證期選 |

4-2-99 CancelStrategyList
| 取消多筆智慧單委託。刪單欄位請參考智慧單被動回報回傳的內容。 |
|---|
| 宣告 | Long CancelStrategyList ([in] BSTR bstrLogInID, [in] struct CANCELSTRATEGYORDER*  pCancelOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pCancelOrder | 智慧單刪單物件(參考5-24) |
|  | bstrMessage | 非同步刪單： / 參照4-2-b OnAsyncOrder。 / 如果回傳值為 0表示刪單成功，訊息內容則為刪單之Thread ID。 / 回傳值非0表示刪單失敗，訊息內容為失敗原因。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 需簽署證券智慧單風險預告書及未簽署者可至金融網－同意書簽署專區申請。 / 刪單後，請透過智慧單被動回報確認狀態。 |

4-2-100 SendForeignStockOrderOLID
| 送出複委託。(含單獨自訂資料欄) |
|---|
| 宣告 | Long SendForeignStockOrderOLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct FOREIGNORDER* pAsyncOrder, [in] BSTR bstrOrderLinkedID, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pAsyncOrder | SKCOM元件中的FOREIGNORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0 數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrderOLID進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / Ver 2.13.46版本以上(含) 新增幣別CNY、GBP；下單市場新增滬深股 (C#範例程式另加港股、日股、新加坡下單選項) / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-101 SendTFOffsetNew
| 指定大、小、微台互抵。 |
|---|
| 宣告 | Long SendTFOffsetNew([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] LONG nCommodity, [in] BSTR bstrYearMonth, [in] LONG nBuySell, [in] LONG nQty, [in] LONG nQty_2, [in] LONG nQty_3 [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | nCommodity | 指定商品0:大抵微; 1:小抵微; 2:大小抵微; 3:大抵小微; 4:小抵大微; 5:大抵小 |
|  | bstrYearMonth | 大小微台年月(YYYYMM)。 |
|  | nBuySell | 大台微買賣別。0:多方(買) 1:空方(賣) |
|  | nQty | 互抵口數，以大台口數為基本單位。 |
|  | nQty_2 | 互抵口數，以小台口數為基本單位。 |
|  | nQty_3 | 互抵口數，以微台口數為基本單位。 |
|  | bstrMessage | 同步：如果回傳值為 0表示委託成功，訊息內容則為互抵訊息。回傳值非0表示互抵失敗，訊息內容為失敗原因。 / 非同步：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託內容請致電交易室查詢， / 回傳值非0時表示失敗，請參考訊息確認錯誤原因。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / EX:互抵需要1口大台抵4口小台，且大小、微台相同月份，才可以互抵成功。1大口等於20微台，1小口等於5微台。 / 大抵小微：以大台為買賣別基準。EX: 2大口(Sell)抵1口小台(Buy)及20口微台(Buy)。 / 大小抵微：以大台、小台為買賣別基準。EX: 1大口(Buy)+ 1口小台(Buy)抵25口微台(Sell)。 / *不須進行互抵的商品請於口數(Qty)帶入0 |

4-2-102 AllCoverDisOptions
| 全拆組單、全拆組平單下單。 / 依照試算結果進行委託。 |
|---|
| 宣告 | Long / AllCoverDisOptions ([in] BSTR bstrLogInID, [in] struct FUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pOrder | SKCOM元件中的 FUTUREORDER物件，將組合條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，表示委託已送出。 / 回傳值非0表示委託失敗，訊息內容為失敗原因。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | ★非交易的行為，無回報，可由未平倉查詢確認部位。 / 因組合/拆解/了結功能是直送結算所，需等處理結果回傳後，方可再進行下一筆。 |

4-2-103 GetAvgCost
| 查詢昨日未沖銷的證券明細。 |
|---|
| 宣告 | Long GetAvgCost ([in] BSTR bstrLogInID, [in] struct SKAVGCOST pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pOrder | SKCOM元件中的SKAVGCOST物件，將昨日未沖銷的證券明細條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託查詢內容。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 回傳格式：每一筆資料以「#」分隔，每一個欄位以「,」分隔。 / 回傳格式參考 / 非正常委託內容時 / M999: 查詢錯誤 / M003: 查無資料 |
| 備註 | 一個account在1分鐘內不可超過10次。 |

4-2-104 OverSeaCancelOrderBySeqNoOLID
| 海外期貨委託删單OLID (By委託序號)。 |
|---|
| 宣告 | Long OverSeaCancelOrderBySeqNoOLID ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] BSTR bstrOrderLinkedID, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲刪除的委託序號。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。回傳值非0表示委託失敗，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrderOLID進行確認。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 |

4-2-105 OverSeaDecreaseOrderBySeqNoOLID
| 海期委託減量OLID (By委託序號)。 |
|---|
| 宣告 | Long OverSeaDecreaseOrderBySeqNoOLID ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrSeqNo, [in] LONG nDecreaseQty, [in] BSTR bstrOrderLinkedID, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 委託帳號 ( IB＋帳號) 。 |
|  | bstrSeqNo | 欲改量的委託序號。 |
|  | nDecreaseQty | 欲減少的數量。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。回傳值非0表示委託失敗，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrderOLID進行確認。 / 當錯誤代碼為4碼時，可參考代碼定義表，所有欄位值說明如上供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託減量回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 |

4-2-106 OverSeaCorrectPriceByBookNoOLID
| 海期改價 OLID (By 委託書號)。 |
|---|
| 宣告 | Long OverSeaCorrectPriceByBookNoOLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDERFORGW* pOrder, [in] BSTR bstrOrderLinkedID, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVRESEAFUTUREORDERFORGW物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。回傳值非0表示委託失敗，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrderOLID進行確認。 / 當錯誤代碼為4碼時，可參考代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / *原委託為限價單方可改價(ROD) |

4-2-107 OverSeaCorrectPriceSpreadByBookNoOLID
| 海期價差改價 OLID (By 委託書號)。 |
|---|
| 宣告 | Long OverSeaCorrectPriceSpreadByBookNoOLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDERFORGW* pOrder, [in] BSTR bstrOrderLinkedID, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVRESEAFUTUREORDERFORGW物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為修改訊息。回傳值非0表示委託失敗，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrderOLID進行確認。 / 當錯誤代碼為4碼時，可參考代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / *原委託為限價單方可改價(ROD) |

4-2-108 CancelOrderByStockNoAdvance
| 國內委託删單(By帳號所屬登入ID+商品代號+買賣別+委託價格)。 |
|---|
| 宣告 | Long CancelOrderByStockNoAdvance([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] BSTR bstrAccount, [in] BSTR bstrStockNo, [in] LONG nBuySell, [in] BSTR bstrPrice, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | bstrAccount | 證券：委託帳號 (BrokerID＋帳號)。 / 期貨：委託帳號 ( IB＋帳號) 。 |
|  | bstrStockNo | 欲刪除的委託商品代號，請輸入有效商品代碼。 |
|  | nBuySell | 0:買進 1:賣出 |
|  | bstrPrice | 委託價格(不使用請帶空字串) |
|  | bstrMessage | 同步刪單：如果回傳值為 0表示刪單成功，訊息內容則為刪單訊息。回傳值非0表示刪單失敗，訊息內容為失敗原因。 / 非同步刪單：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 回傳值0 表示委託伺服器接收成功，詳細委託狀態仍須以委託刪單回報內容為主。 / 其他非 0 數值表示異常，詳細原因請參考 Log 內容說明。 / 使用非同步委託，委託結果請由OnAsyncOrder取得。 / ＊請留意證券代碼，包含刪除盤中零股委託 |

4-2-109 GetBalance
| 一戶通、餘額、可用金額查詢。 |
|---|
| 宣告 | BSTR GetBalance ([in] BSTR bstrLogInID) |
| 參數 | bstrLogInID | 登入ID。 |
| 回傳值 | 查詢文字訊息。 |
| 備註 | -1表示失敗。 / 每一筆資料以「,」分隔每一個欄位，結尾為「.」。欄位依序為： / EX: true,0,0,0. |

4-2-110 GetT3DueAmt
| 近三日交割款查詢。 |
|---|
| 宣告 | BSTR GetT3DueAmt ([in] BSTR bstrLogInID, [in] BSTR bstrAccount) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 證券帳號，分公司四碼＋帳號7碼。 |
| 回傳值 | 查詢文字訊息。 |
| 備註 | 不同幣別資料以「;」分隔，欄位以「,」分隔，欄位依序為： / EX: 1131111,1131113,-653812,1131108,1131112,-1204594,1131107,1131111,-3262935,臺幣;1131111,1131113,0,1131108,1131112,0,1131107,1131111,0,人民幣; |

4-2-111 GetIPAAmt
| 台股圈存查詢 |
|---|
| 宣告 | BSTR GetIPAAmt ([in] BSTR bstrLogInID, [in] BSTR bstrAccount, [in] LONG nMarket) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 證券帳號，分公司四碼＋帳號7碼。 |
|  | nMarket | 0:上市櫃 / 1:興櫃 |
| 回傳值 | 欄位以「,」分隔，結尾為「#」，欄位依序為： / 有開通一戶通 格式： / EX: / 646551,7831,7811# / 未開通一戶通 格式： |
| 備註 | 沒有一戶通： / EX: 請匯預收款至國泰世華商業銀行(013)群益金鼎證券營業處客戶興櫃結算專戶，再與分公司營業員聯絡 |

4-2-112 GetBankBlock
| 複委託外幣圈存查詢_一戶通 |
|---|
| 宣告 | BSTR GetBankBlock ([in] BSTR bstrLogInID, [in] BSTR bstrAccount,) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 複委託帳號，分公司四碼＋帳號7碼。 |
| 回傳值 | 欄位以「,」分隔，結尾為「#」，欄位依序為： / 格式： / EX: CNY,5000000.00,1070.08,0.00,452.45,4998477.47,0.00,011,002,11111111111111,上銀營 |
| 備註 |  |

4-2-113 GetNTDBlock
| 複委託台幣圈存查詢_一戶通 |
|---|
| 宣告 | BSTR GetNTDBlock ([in] BSTR bstrLogInID, [in] BSTR bstrAccount,) |
| 參數 | bstrLogInID | 登入ID。 |
|  | bstrAccount | 複委託帳號，分公司四碼＋帳號7碼。 |
| 回傳值 | 欄位以「,」分隔，結尾為「#」，欄位依序為： / 格式： |
| 備註 |  |

4-2-114 SendOverseaFutureSpreadOrder2
| <請先執行 4-2-39商品檔下載 SKOrderLib_LoadOSCommodity()> / 送出海外期貨價差委託。 / 使用此函式時，帶入0:買，如果是以近月買賣別為主之商品-買進賣遠；以遠月買賣別為主之商品-買遠賣近 |
|---|
| 宣告 | Long SendOverseaFutureSpreadOrder2([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中(價差不提供當沖)。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-b OnAsyncOrder。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-115 SendOverseaFutureSpreadOrder2OLID
| 送出海外期貨價差委託。(含單獨自訂資料欄) / 使用此函式時，帶入0:買，如果是以近月買賣別為主之商品-買進賣遠；以遠月買賣別為主之商品-買遠賣近 |
|---|
| 宣告 | Long SendOverseaFutureSpreadOrder2OLID([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder, [in] BSTR bstrOrderLinkedID,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中(價差不提供當沖)。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrderOLID進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / SGX專線模式：SGX交易所商品均經由專線委託 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-116 SendOverseaOptionOrderOLID
| 送出海外選擇權委託。(含單獨自訂資料欄) |
|---|
| 宣告 | Long SendOverseaOptionOrderOLID ([in] BSTR bstrLogInID, [in] VARIANT_BOOL bAsyncOrder, [in] struct OVERSEAFUTUREORDER* pOrder, [in] BSTR bstrOrderLinkedID,[out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | bAsyncOrder | 是否為非同步委託。 |
|  | pOrder | SKCOM元件中的 OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrOrderLinkedID | 僅非同步委託有效。 / 客戶自訂資料，會在OnAsyncOrderOLID返回。 |
|  | bstrMessage | 同步委託：如果回傳值為 0表示委託成功，訊息內容則為13碼的委託序號。其餘非0數值表示委託異常，請確認回報或洽詢營業員，訊息內容為失敗原因。 / 非同步委託：參照4-2-o OnAsyncOrderOLID。 |
| 回傳值 | 同步委託：0表示成功，其餘非0數值都表示失敗。 / 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnAsyncOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤由「主機」回傳錯誤代碼及錯誤原因，若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrderOLID取得。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-2-a OnAccount
| 帳號資訊。透過呼叫GetUserAccount後，帳號資訊由該事件回傳。 |
|---|
| 宣告 | void OnAccount([in] BSTR bstrLogInID, [in] BSTR bstrAccountData  ); |
| 參數 | bstrLogInID | 登入ID |
|  | bstrAccountData | 帳號資訊。以逗點分隔每一個欄位，欄位依序為： / 『市場,分公司代碼,分公司,帳號,身份證字號,姓名』 / *備註 / 證券：分公司代碼；期貨：Broker id |
| 備註 | 登入前，需先簽署證券或期貨API下單聲明書，方能取得相關市場帳號。 / 若未簽署，請先簽署完成，再重新登入。 |

4-2-b OnAsyncOrder
| 非同步委託結果。 |
|---|
| 宣告 | void OnAsyncOrder([in] LONG nThreadID,  [in] LONG nCode, [in] BSTR bstrMessage  ); |
| 參數 | nThreadID | Thread ID |
|  | nCode | 收單結果代碼。 |
|  | bstrMessage | 收單回傳訊息。 |
| 備註 | 送單函式中使用非同步下單時會取得一個Thread ID，當此事件觸發時可藉由nThreadID對應下單來源。 / 委託成功：收單回傳訊息為 委託序號 / 委託失敗：收單回傳訊息為 委託失敗原因 |
|  | 期貨智慧單: 如果回傳值為 0表示委託成功，訊息內容則為委託日期、智慧單序號、委託書號、委託內容(帳號等)，以實際回傳內容為主。 / 回傳成功訊息範例: / EX1: 000,20220419,智慧條件單已送出 條件單號：568157,無委託書號,568157,1689200000003 / EX2: 000,20220420,智慧條件單已送出 條件單號：568353,y0004,568353,1688000000001 |

(SGX DMA) OnAsyncOrder
| 非同步委託結果。 |
|---|
| 宣告 | void OnAsyncOrder([in] LONG nThreadID,  [in] LONG nCode, [in] BSTR bstrMessage  ); |
| 參數 | nThreadID | Thread ID |
|  | nCode | 收單結果代碼。 |
|  | bstrMessage | 收單回傳訊息。 |
| 備註 | 送單函式中使用非同步下單時會取得一個Thread ID，當此事件觸發時可藉由nThreadID對應下單來源。 / 送出委託：收單回傳訊息M000及15碼的英數字委託序號，以空格間隔。 / (EX: M000 EB1234567890123)。 / 委託失敗：收單回傳訊息為M999+委託失敗原因 或交易所回傳錯誤。 / *實際委託成功與否，請以專線回報資料為主。（EX:委託價格有誤，仍可能收到15碼序號） |

4-2-c OnRealBalanceReport
| 證券即時庫存。透過呼叫GetRealBalanceReport後，資訊由該事件回傳。 / 版本 v2.13.42 ~ v2.13.54 間，證券即時庫存查詢「可資沖 / 可券沖」欄位回傳順序有誤，兩者內容誤植互換。自 v2.13.55 起已修正此問題，請升級 API 後留意欄位順序。 |
|---|
| 宣告 | void OnRealBalanceReport( [in] BSTR bstrData  ); |
| 參數 | bstrData | 每一筆資料以「,」分隔每一個欄位，欄位依序為： / 股票代號, / 庫存種類,(T:集保 C:融資 L:融券) / 資額度(原始), / 資額度(可用), / 券額度(原始), / 券額度(可用), / 股數:昨日庫存, / 今日委買, / 今日委賣, / 今日買進成交, / 今日賣出成交, / 今日資券可回補/集保庫存可賣出, / 可資沖股數, / 可券沖股數, / 即時庫存, / X(此欄請忽略) / 即時個股維持率, / LOGIN_ID / ACCOUNT_NO |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

4-2-d OnOpenInterest
| 期貨未平倉。透過呼叫 GetOpenInterest (不含格式參數)後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOpenInterest( [in] BSTR bstrData  ); |
| 參數 | bstrData | 每一筆資料以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 若查無資料，則回傳001,查無資料,帳號 / 不含複式單，市場別：TM |
|  |
| 期貨未平倉含格式。透過呼叫 GetOpenInterestWithFormat (含格式參數)後，資訊由該事件回傳。 |
| 宣告 | void OnOpenInterest ( [in] BSTR bstrData  ); |
| 參數 | bstrData | 每一筆資料以「,」分隔每一個欄位，欄位依序為： / 完整： (含複式單，市場別：TM) / 格式1：(含複式單，市場別：TM) / 格式2：(不含複式單，市場別：TM，可自行計算損益) |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 含複式單，市場別：TM / 格式2可自行計算損益 / 若查無資料，則回傳001,查無資料,帳號 |
| (新)期貨未平倉GW。透過呼叫 GetOpenInterestGW 後，資訊由該事件回傳。 |
| 宣告 | void OnOpenInterest( [in] BSTR bstrData  ); |
| 參數 | bstrData | 每一筆資料以「,」分隔每一個欄位，欄位依序為： / 格式1: |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 若查無資料，則回傳001,查無資料,帳號 / 含複式單，市場別：TM / 當「商品－下單代碼」查詢發生異常時，「商品－下單代碼」欄位將回傳空值。請重新呼叫 GetOpenInterestGW 以重新取得資料。 |

4-2-e OnOverseaFutureOpenInterest
| 海外期貨未平倉。透過呼叫GetOverSeaFutureOpenInterest 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOverseaFutureOpenInterest( [in] BSTR bstrData  ); |
| 參數 | bstrData | 第一筆回傳內容為 「@@」開頭，表示查詢狀態，每一筆資料以「,」分隔每一個欄位，欄位依序為「@@,訊息代碼,訊息內容」 / 其中，當訊息代碼為 / 0：表示查詢成功，訊息內容為空字串 / 其他非「0」皆表示查詢錯誤，訊息內容將描述錯誤原因 / 如有未平倉資訊將從第二筆開始回傳，每一筆資料以「,」分隔每一個欄位，欄位依序為： / 海期交易所代碼 / 海期交易所中文名稱 / 帳號 / 海期商品代碼＋年月，代碼與年月間以空白區隔(年月是一起，未另外以空白區隔) / 海期選商品中文名稱 / 海選商品中文名稱EX: / 迷你S&PPUT(空格)202203(空格)3940(空格) P / 買賣別 B:買進 S：賣出 / 數量 / 市價 / 平均成交價 / 昨日結算價 / 損益 |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 若查無資料，則回傳001,查無資料,帳號 |

4-2-f OnStopLossReport
STP
| 新版期貨智慧單(包含停損單、移動停損、二擇一、觸價單)被動回報查詢。 / 透過呼叫 GetStopLossReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnStopLossReport( [in] BSTR bstrData  ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及停損單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之停損單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| STP: |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。*V2.13.38版本起配合智慧單平台異動至新版格式 |

MST
| 新版期貨智慧單(包含停損單、移動停損、二擇一、觸價單)查詢。透過呼叫 GetStopLossReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnStopLossReport( [in] BSTR bstrData  ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及停損單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之停損單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 回傳的內容以「,」分隔每一個欄位，欄位依序為： / MST: |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / *V2.13.38版本起配合智慧單平台異動至新版格式 |

OCO
| 新版期貨智慧單(包含停損單、移動停損、二擇一、觸價單)查詢。透過呼叫 GetStopLossReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnStopLossReport( [in] BSTR bstrData  ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及停損單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之停損單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 回傳的內容以「,」分隔每一個欄位，欄位依序為： / OCO： |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / *V2.13.38版本起配合智慧單平台異動至新版格式 |

MIT
| 新版期貨智慧單(包含停損單、移動停損、二擇一、觸價單)查詢。透過呼叫 GetStopLossReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnStopLossReport( [in] BSTR bstrData  ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及停損單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之停損單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 回傳的內容以「,」分隔每一個欄位，欄位依序為： / MIT: |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / *V2.13.38版本起配合智慧單平台異動至新版格式 |

AB單
| 新版期貨智慧單(包含停損單、移動停損、二擇一、觸價單)查詢。透過呼叫 GetStopLossReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnStopLossReport( [in] BSTR bstrData ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及停損單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之停損單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 回傳的內容以「,」分隔每一個欄位，欄位依序為： / AB： |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / *V2.13.38版本起配合智慧單平台異動至新版格式 |

4-2-g OnOverseaFuture
| 海外期貨下單商品。透過呼叫 GetOverseaFutures 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOverseaFuture([in] BSTR bstrData); |
| 參數 | bstrData | 回傳得字串內容，以分號(;)分隔每一個欄位，欄位依序為： / 交易所代碼; / 交易所名稱; / 商品代碼; / 商品名稱; / 年月; / 跳動點; / 分母; / 可接受交易種類; / 可當沖 / 委託時效(ROD;FOK;IOC) / 商品種類 |
| 備註 | 2020/11 新增委託時效欄位 / [商品類別] 代號依序如下 / 1:指數(現貨) / 2:外匯(現貨) / 3:股票 / 4權證 / 5:期貨 / 6:指數商品價差(EQ) / 7:一般商品價差(SP) / 8:外匯商品價差(FX) / 9:選擇權 / 10:債券跨商品價差 |

4-2-h OnOverseaOption
| 海外選擇權下單商品。透過呼叫 GetOverseaOptions  後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOverseaOption([in] BSTR bstrData); |
| 參數 | bstrData | 回傳得字串內容，以逗號(,)分隔每一個欄位，欄位依序為： / 下單交易所代碼, / 交易所名稱, / 下單商品代碼, / 商品名稱, / 商品年月, / 價格跳動點, / 履約價最小跳動點, / 基準履約價, / 最低履約價, / 最高履約價, / 履約價除數, / 分母, / 可委託類型, / 當沖減收保證金, / 標的年月 |
| 備註 | 價格跳動點顯示範例 / 例： 5\|0.05  5元(不含)以下,0.05以“\|“管線隔開  以“/“隔開每筆資料 / 0\|0.0025\|N/ 100000000\|0.005\|N / ＜＝代表0~100000000 之間最小價格跳動點為0.0025, 100000000以上為0.005；N代表不顯示成分數 / 99999999\|0.1\|N / <=代表０～99999999之間，最小價格跳動點為0.1；N代表不顯示成分數 / 99999999\|1\|Y / <=代表０～99999999之間，最小價格跳動點為1；Ｙ代表價格顯示成分數 / 10\|0.1\|N/ 50\|0.5\|N/ 500\|1\|N/ 1000\|5\|N/ 100000000\|10\|N / <=0~10~50~500~1000~100000000000（跳動點由左至右區間分別為0.1－0.5－1－5－10 ；N代表不顯示成分數 / 履約價範例 / 例：最高履約價 1300，最低履約價 1050  履約跳動點5  履約價除數10 / 原先5點一跳 1050、1055、1060…….1300，但需除以履約價除數10 / 最終履約價價格105、105.5、106、106.5……125.5、130 |

4-2-i OnFutureRights
| 國內期貨權益數。透過呼叫 GetFutureRights 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnFutureRights([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： / 0 帳戶餘額 / 1 浮動損益 / 2 已實現費用 / 3 交易稅 / 4 預扣權利金 / 5 權利金收付 / 6 權益數 / 7 超額保證金 / 8 存提款 / 9 買方市值 / 10 賣方市值 / 11 期貨平倉損益 / 12 盤中未實現 / 13 原始保證金 / 14 維持保證金 / 15 部位原始保證金 / 16 部位維持保證金 / 17 委託保證金 / 18 超額最佳保證金 / 19 權利總值 / 20 預扣費用 / 21 原始保證金 / 22 昨日餘額 / 23 選擇權組合單加不加收保證金 / 24 維持率 / 25 幣別 / 26 足額原始保證金 / 27 足額維持保證金 / 28 足額可用 / 29 抵繳金額 / 30 有價可用 / 31 可用餘額 / 32 足額現金可用 / 33 有價價值 / 34 風險指標 / 35 選擇權到期差異 / 36 選擇權到期差損 / 37 期貨到期損益 / 38 加收保證金 / 39  LOGIN_ID / 40  ACCOUNT_NO |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 全幣別（含基幣），回傳第一筆資料為基幣。 |

4-2-j OnRequestProfitReport
| 證券即時損益試算。透過呼叫 GetProfitLossGWReport後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnRequestProfitReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 自:自辦券商(目前適用) / 代: 代辦信用 |
| 證券即時損益試算。 / 即將下線 |
| 備註 |  |

4-2-k OnOverSeaFutureRight
| 海外期貨權益數。透過呼叫 GetRequestOverSeaFutureRight後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOverSeaFutureRight ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，第一筆為查詢結果，成功時以「,」分隔，為「000」開頭與資料筆數；失敗時以「;」分隔，為錯誤代碼與錯誤訊息。 / 成功時第二筆資料開始就是訊息內容，各筆資料以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 當資料已全部傳送完畢時，將回傳一筆「##」表示查詢結束 |

4-2-l OnMarginPurchaseAmountLimit
| 資券配額查詢。透過呼叫 GetMarginPurchaseAmountLimit後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnMarginPurchaseAmountLimit ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容【M000】代表成功，接著以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003】表示此股票無信用交易資格 |

4-2-m OnBalanceQuery
| 集保庫存查詢。透過呼叫 GetBalanceQuery後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnBalanceQuery ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003】表示沒有庫存 |

4-2-n OnTSSmartStrategyReport
<新版證券智慧單被動回報>依各種策略單別提供格式內容
MIOC
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 資料排序為：先回傳最後一筆委託, 後回傳最早之委託。 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

MST
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

新版MIT
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 自SKCOM V2.13.30(含)版本起，提供MIT新版被動回報 |

*V2.13.38版本起移除MMIT主動回報欄位
當沖單- 母單
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |

當沖單- 進場單
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： / 當沖-進場單格式, 若設進場MIT才有此欄 |

當沖單- 出場單
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： / 當沖-進場單格式, 若設進場MIT才有此欄 / 當沖-出場單格式 |

出清單
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： / 出清子單—觸發歷程： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

| [OCO 二擇一單]證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

AB單
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 自SKCOM V2.13.30(含)版本起，提供MIT新版被動回報 |

CB單(自組單)
| 證券智慧單被動查詢結果。透過呼叫 GetTSSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTSSmartStrategyReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的內容以「,」分隔每一個欄位，欄位依序為： |
| 備註 | 回傳【M003,無資料】表示目前查無被動回報 / 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

4-2-o OnAsyncOrderOLID
| 非同步委託結果。(含單獨自訂資料欄) |
|---|
| 宣告 | void OnAsyncOrderOLID([in] LONG nThreadID,  [in] LONG nCode, [in] BSTR bstrMessage, [in] BSTR bstrOrderLinkedID  ); |
| 參數 | nThreadID | Thread ID |
|  | nCode | 收單結果代碼。 |
|  | bstrMessage | 收單回傳訊息。(13碼序號) |
|  | bstrOrderLinkedID | 客戶自訂資料，下單時帶入。 |
| 備註 | 送單函式中使用非同步下單時會取得一個Thread ID，當此事件觸發時可藉由nThreadID對應下單來源。 |

4-2-p OnProfitLossGWReport
| 證券新損益查詢結果。透過呼叫 GetProfitLossGWReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnProfitLossGWReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，第一筆為查詢結果。 / 成功時，成功代碼為「000」與訊息，中間以「,」分隔 / 失敗時，為錯誤代碼與錯誤訊息，中間以「,」分隔。 / 成功時： / 第二筆資料開始就是資料內容，各筆資料以「,」分隔每一個欄位，欄位依序如下表，請參照下表。 |

| 證券新損益查詢結果。透過呼叫 GetProfitLossGWReport 後，資訊由該事件回傳。 / (未實現) |
|---|
| 宣告 | void OnProfitLossGWReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，第一筆為查詢結果。 / 成功代碼為「000」與訊息。 / 成功時： / 第二筆資料開始就是資料內容 / 回傳的內容以「,」分隔每一個欄位，欄位依序為： / (彙總資料格式) / (明細資料格式) |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

| 證券新損益查詢結果。透過呼叫 GetProfitLossGWReport 後，資訊由該事件回傳。 / (已實現) |
|---|
| 宣告 | void OnProfitLossGWReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，第一筆為查詢結果。 / 成功代碼為「000」與訊息。 / 成功時： / 第二筆資料開始就是資料內容 / 回傳的內容以「,」分隔每一個欄位，欄位依序為： / (彙總資料格式) / (明細資料格式) / (投資總額) / (彙總-依商品代碼資料格式) |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

| 證券新損益查詢結果。透過呼叫 GetProfitLossGWReport 後，資訊由該事件回傳。 / (現股當沖) |
|---|
| 宣告 | void OnProfitLossGWReport ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳的字串內容，第一筆為查詢結果。 / 成功代碼為「000」與訊息。 / 成功時： / 第二筆資料開始就是資料內容 / 回傳的內容以「,」分隔每一個欄位，欄位依序為： / (彙總資料格式) / (明細資料格式)： |

4-2-q OnOFOpenInterestGWReport
| 海外期貨未平倉彙總資料。透過呼叫GetOverseaFutureOpenInterestGW 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOFOpenInterestGWReport( [in] BSTR bstrData  ); |
| 參數 | bstrData | 第一筆回傳內容為 「@@」開頭，表示查詢狀態，每一筆資料以「,」分隔每一個欄位，欄位依序為「@@,訊息代碼,訊息內容」 / 其中，當訊息代碼為 / 0：表示查詢成功，訊息內容為空字串 / 其他非「0」皆表示查詢錯誤，訊息內容將描述錯誤原因 / 如有未平倉資訊將從第二筆開始回傳，每一筆資料以「,」分隔每一個欄位，欄位依序為： / 海期： / 帳號 / 交易所代號 / 交易所中文名稱 / 含年月-商品代號，以空白間隔 / （EX: NG     202012） / 不含年月-商品代碼（EX: NG） / 商品年月 / 商品中文名稱(含年月，以空白間隔) / EX: 日經225指 202012 / 買賣別 / 未平口數 / 平均價(又稱成交均價) / (非即時)現價又稱市價 / 未平損益 / 委託買 / 買方委託口數 / 委託賣 / 賣方委託口數 / 賣方委託新倉口數 / 昨日結算價（EX:379 02/4） / 若價格含分子分母，中間以空白間隔整數、分子分母。 / 當沖未平口數 / 原始保證金 / 是否為選擇權(0->期貨 1->選擇權) / 海選: / 帳號 / 交易所代號 / 交易所中文名稱 / 商品代號含年月、履約價、C/P(EX: C00380L0) / 不含年月-商品代碼（EX:C　為玉米 ） / 商品年月 / 商品中文名稱（含CALL/PUT、年月、履約價、C/P，以空白間隔） / EX: 玉米  CALL 202012 380 C / 買賣別 / 未平口數 / 平均價(又稱成交均價) / (非即時)現價又稱市價 / 未平損益 / 委託買 / 買方委託口數 / 委託賣 / 賣方委託口數 / 賣方委託新倉口數 / 昨日結算價 / 若價格含分子分母，中間以空白間隔整數與分子分母。 / EX:379 02/4 / 當沖未平口數 / 原始保證金 / 是否為選擇權(0->期貨 1->選擇權) / 履約價 / C/P (C:Call/P:Put) |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

| 海外期貨未平倉明細資料。透過呼叫GetOverseaFutureOpenInterestGW 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOFOpenInterestGWReport( [in] BSTR bstrData  ); |
| 參數 | bstrData | 每一筆資料以「,」分隔每一個欄位，欄位依序為： / 海期： / 帳號 / 成交日期 / 交易所代碼 / 交易所中文名稱 / 商品代碼不含年月(EX: SSI) / 商品年月 / 商品中文名稱(含年月，以空白間隔) / EX: 日經225指 202012 / 買賣別 / 成交口數 / 未平倉口數 / 成交價 / 現價 / 未平損益 / 委託書序號(前2碼櫃號) / 當沖註記 / 原始保證金 / 是否為選擇權(0->期貨 1->選擇權) / 海選： / 帳號 / 成交日期 / 交易所代碼 / 交易所中文名稱 / 商品代碼含C/P (以空白間隔C、P) / EX: C     C //玉米買權Call / EX: C     P //玉米賣權Put / 商品年月 / 商品中文名稱 / EX:玉米 202012 370 P / 買賣別 / 成交口數 / 未平倉口數 / 成交價 / 現價 / 未平損益 / 委託書序號(前2碼櫃號) / 當沖註記 / 原始保證金 / 是否為選擇權(0->期貨 1->選擇權) / 履約價 |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

4-2-r OnTelnetTest
| 透過呼叫 SKOrderLib_TelnetTest 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnTelnetTest ([in] BSTR bstrData); |
| 參數 | bstrData | 回傳內容： / 測試中 / 共測試x台主機 : 成功y台, 失敗z台 |
| 備註 |  |

4-2-s OnOFSmartStrategyReport
OCO單
| 海期智慧單查詢。透過呼叫 GetOFSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOFSmartStrategyReport( [in] BSTR bstrData ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及智慧單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之智慧單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 回傳的內容以「,」分隔每一個欄位，欄位依序為： / OCO： |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

AB單
| 海期智慧單查詢。透過呼叫 GetOFSmartStrategyReport 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOFSmartStrategyReport( [in] BSTR bstrData ); |
| 參數 | bstrData |
|  | ＊注意：新版本回傳內容及欄位與舊版本相異。 / 如果回傳的第一筆資料是一位英文M接連著三碼數字及智慧單總數，其中 "M000" 開頭，表示為成功回傳，其後提供欲查詢之智慧單總數。 / Ex: / (成功)M000,12 / 第二筆後面接的就是訊息內容。回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： |
| 回傳的內容以「,」分隔每一個欄位，欄位依序為： / AB： |
| 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

4-2-t OnOpenInterestGWStatus
| 國內期貨未平倉GW的查詢狀態。透過呼叫 GetOpenInterestGW、GetOpenInterest、GetOpenInterestWithFormat 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOpenInterestGWStatus( [in] LONG nQueryStatus, [in] BSTR bstrErrorMsg); |
| 參數 | nQueryStatus | 0:查詢成功；1查詢失敗 |
|  | bstrErrorMsg | 成功為空，失敗為錯誤訊息 |
| 備註 | 成功與失敗 |

4-2-u OnOverseaFutureOpenInterestGWStatus
| 海外期貨未平倉GW的查詢狀態。透過呼叫 GetOverseaFutureOpenInterestGW後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOverseaFutureOpenInterestGWStatus( [in] LONG nQueryStatus, [in] BSTR bstrErrorMsg); |
| 參數 | nQueryStatus | 0:查詢成功；1查詢失敗 |
|  | bstrErrorMsg | 成功為空，失敗為錯誤訊息 |
| 備註 | 成功與失敗 |

4-2-v OnFutureRightsStatus
| 國內權益數查詢的狀態。透過呼叫 GetFutureRights 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnFutureRightsStatus ( [in] LONG nQueryStatus, [in] BSTR bstrErrorMsg); |
| 參數 | nQueryStatus | 0:查詢成功；1查詢失敗 |
|  | bstrErrorMsg | 成功為"Success!"，失敗為錯誤訊息 |
| 備註 | 成功與失敗 |

4-2-w OnOverseaFutureRightsStatus
| 海外權益數的查詢狀態。透過呼叫GetRequestOverSeaFutureRight後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOverseaFutureRightsStatus ( [in] LONG nQueryStatus, [in] BSTR bstrErrorMsg); |
| 參數 | nQueryStatus | 0:查詢成功；1查詢失敗 |
|  | bstrErrorMsg | 成功為"Success!"，失敗為錯誤訊息 |
| 備註 | 成功與失敗 |

4-2-x OnOpenInterestJson
| (新)期貨未平倉GW。透過呼叫 GetOpenInterestGW、GetOpenInterest 、 GetOpenInterestWithFormat 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnOpenInterestJson( [in] BSTR bstrData); |
| 參數 | bstrData | 每一筆資料以「,」分隔每一個欄位。 / GetOpenInterest欄位依序為： / GetOpenInterestWithFormat欄位依序為： / 完整： (含複式單，市場別：TM) / 格式1：(含複式單，市場別：TM) / 格式2：(不含複式單，市場別：TM，可自行計算損益) / GetOpenInterestGW欄位依序為： / 回傳範例： / ["TM,F0200009999999,TXU28500/28250K5,0,0,17,0,F123456789","TO,F0200009999999,TXU28500K5,43,0,0,0,F123456789"] |
| 備註 | 若查無資料，則回傳 001,查無資料,帳號 |

4-3 SKReplyLib ( 回報 )
函式
| 功能 | 函式名稱 | 備註 |
|---|---|---|
| 連線 | SKReplyLib_ConnectByID | 回報主機連線 / OnSolaceReplyConnection / 、 OnComplete |
| 連線狀態 | SKReplyLib_IsConnectedByID | 查詢回報是否連線 |
| Solace斷線 | SKReplyLib_SolaceCloseByID | 回報斷線 |

事件
| 功能 | 事件名稱 | 備註 |
|---|---|---|
| 連線通知 | OnConnect |  |
| 斷線通知 | OnDisconnect |  |
| 連線完通知 | OnComplete |  |
| ☉回報資料 | OnData | 即將下線 |
| 公告資訊 | OnReplyMessage | 登入前進行此callback檢查 |
| 清盤通知 | OnReplyClear |  |
| 回報資料 | OnNewData |  |
| 斷線通知 | OnSolaceReplyDisconnect |  |
| 連線通知 | OnSolaceReplyConnection |  |
| 公告清盤通知 | OnReplyClearMessage |  |
| 智慧單回報新版 | OnStrategyData | 適用國內證期選 |

4-3-1 SKReplyLib_ConnectByID
| 指定回報連線的使用者登入帳號。 |
|---|
| 宣告 | Long SKReplyLib_ConnectByID( [in] BSTR bstrUserID ); |
| 參數 | bstrUserID | 登入帳號。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 需先簽署證券或期貨API下單聲明書，方可使用 / 智慧單回報:國內證券、期貨、選擇權 |

4-3-2 SKReplyLib_CloseByID
| 請參考4-3-4 SKReplyLib_SolaceCloseByID |
|---|
| 備註 | 配合2018 舊主機下線，舊客戶串接保留使用。 / 新用戶請直接參考4-3-4 SKReplyLib_SolaceCloseByID |

4-3-3 SKReplyLib_IsConnectedByID
| 檢查輸入的帳號目前連線狀態。 |
|---|
| 宣告 | Long SKReplyLib_IsConnectedByID( [in] BSTR bstrUserID ); |
| 參數 | bstrUserID | 登入帳號。 |
| 回傳值 | 正式環境： 0表示斷線。1表示連線中。2表示下載中。 / 錯誤代碼可參考對照表。 |
| 備註 | 請同時接收通知事件4-3-i OnSolaceReplyConnection |

4-3-4 SKReplyLib_SolaceCloseByID
| 中斷指定帳號的Solace伺服器連線。 |
|---|
| 宣告 | Long SKReplyLib_SolaceCloseByID([in] BSTR bstrUserID); |
| 參數 | bstrUserID | 登入帳號。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 中斷單一指定Solace連線，若該連線同時負責報價時也會中斷報價。 / 若需中斷所有Solace連線，請使用SKQuoteLib_LeaveMonitor。 |

4-3-a OnConnect
| 當連線成功或失敗，會透過此事件函式告知連線結果。 |
|---|
| 宣告 | void OnConnect([in] BSTR bstrUserID, [in] LONG nErrorCode  ); |
| 參數 | bstrUserID | 登入ID |
|  | nErrorCode | （Socket） Error Code |
| 備註 | Solace回報連線 / 請注意：收到此通知事件時，底層尚未完成斷線的處理，因此請勿在此事件中立刻執行重新連線或斷線的動作。請收到此事件後啟用一個 Timer，等待數秒後再嘗試重新連線或斷線。 |

*v2.13.48 版本起，回報連線事件請透過4-3-i OnSolaceReplyConnection
4-3-b OnDisconnect
| 當連線中斷將會透過此事件函式告知連線結果。 |
|---|
| 宣告 | void OnDisconnect([in] BSTR bstrUserID, [in] LONG nErrorCode  ); |
| 參數 | bstrUserID | 登入ID |
|  | nErrorCode | （Socket） Error Code |
| 備註 | Solace回報連線 / 請注意：收到此通知事件時，底層尚未完成斷線的處理，因此請勿在此事件中立刻執行重新連線或斷線的動作。請收到此事件後啟用一個 Timer，等待數秒後再嘗試重新連線或斷線。 |

*v2.13.48 版本起，回報斷線事件請透過4-3-h OnSolaceReplyDisconnect
4-3-c OnComplete
| 回報連線後會進行回報回補，等收到此事件通知後表示回補完成。 |
|---|
| 宣告 | void OnComplete([in] BSTR bstrUserID); |
| 參數 | bstrUserID | 登入ID |
| 備註 | 若未收到此通知,代表新建立的回報連線及回傳回報資料異常. |

4-3-d OnData
| ☉注意 / 舊用戶請以4-3-g OnNewData 各欄位說明為主，格式相同。 / 新用戶請直接參考使用4-3-g OnNewData。 |
|---|

4-3-e OnReplyMessage
| 當有公告將主動呼叫函式，並通知公告類訊息。 |
|---|
| 宣告 | void OnReplyMessage([in] BSTR bstrUserID , [in] BSTR bstrMessage,[out]SHORT* sConfirmCode); |
| 參數 | bstrUserID | 登入ID |
|  | bstrMessage | 每一筆資料以「,」分隔每一個欄位 / MsgNo　// 訊息編號 / StartTime　// 訊息開始日期時間 / EndTime　// 訊息結束時間 / Message　// 訊息內容 |
|  | sConfirmCode | ConfirmCode //回傳確認值VARIANT_TRUE |
| 備註 | 需在登入前先註冊此通知事件—OnReplyMessage，方可正確登入，不需先做回報連線。 |

4-3-f OnReplyClear
| 當有回報開始清除前日資料時，會發出的通知，表示清除前日回報 |
|---|
| 宣告 | void OnReplyClear([in] BSTR bstrMarket); |
| 參數 | bstrMarket | R1 證券 / R2 國內期選 / R3 海外股市 / R4 海外期選 / R11 盤中零股 / R20~R23 智慧單 |
| 備註 |  |

4-3-g OnNewData
| 當有回報將主動呼叫函式，並通知委託的狀態。(新格式 包含預約單回報) |
|---|
| 宣告 | void OnNewData([in] BSTR bstrUserID , [in] BSTR bstrData); |
| 參數 | bstrUserID | 登入ID |
|  | bstrData | 每一筆資料以「,」分隔每一個欄位 |
|  |  |
| 備註 | 「動態退單」 / 被「動態退單」的委託，會收到委託回報、取消回報與動態退單回報，若有成交部位還會有成交回報。 / 買進委託：可能成交價格 > 即時價格區間上限 → 退單 / 賣出委託：可能成交價格 < 即時價格區間下限 → 退單 / ˙即時價格區間上、下限計算公式 / 即時價格區間上限 = 退單價＋退單點數 / 即時價格區間下限 = 退單價－退單點數 / 退單點數:每日盤前計算完成(盤中固定) / 異動前量及異動後量只支援部分市場, (僅提供證券、複委託市場) / SGX DMA專線:可參考「交易所單號」於一般線路比對回報資料。 / 2020/3/23證券逐筆交易開始提供相關二欄資料CancelOrderMarkByExchange 、ExchangeTandemMsg |

修改比較表:
| 修改前 | 欄位 | 修改後V2.13.45 |
|---|---|---|
| 履約價 七位整數 | StikePrice | (配合舊回報資料保留欄位) / 履約價敬請參考StrikePrice1 and StrikePrice2 |

4-3-g-2 （SGX DMA）OnNewData
| 當有回報將主動呼叫函式，並通知委託的狀態。(新格式 包含預約單回報) |
|---|
| 宣告 | void OnNewData([in] BSTR bstrUserID , [in] BSTR bstrData); |
| 參數 | bstrUserID | 登入ID |
|  | bstrData | 每一筆資料以「,」分隔每一個欄位 |
|  |  |
| 備註 | SGX DMA專線:可參考「交易所單號」於一般線路比對回報資料。 / 若改一般線路取得SGX DMA專線委託回報僅含委託成功，不含委託失敗回報（因價格等因素之委託失敗）。 |

4-3-h OnSolaceReplyDisconnect
| 當中斷solace連線，會透過此事件函式告知斷線結果。 |
|---|
| 宣告 | void OnSolaceReplyDisconnect([in] BSTR bstrUserID, [in] LONG nErrorCode  ); |
| 參數 | bstrUserID | 登入ID |
|  | nErrorCode | Error Code |
| 備註 | 請注意：收到此通知事件時，底層尚未完成斷線的處理，因此請勿在此事件中立刻執行重新連線或斷線的動作。請收到此事件後啟用一個 Timer，等待數秒後再嘗試重新連線或斷線。 |

4-3-i OnSolaceReplyConnection
| 當solace連線，會透過此事件函式告知。 |
|---|
| 宣告 | void OnSolaceReplyConnection([in] BSTR bstrUserID, [in] LONG nErrorCode  ); |
| 參數 | bstrUserID | 登入ID |
|  | nErrorCode | Error Code |
| 備註 | 請注意：收到此通知事件時，底層尚未完成斷線的處理，因此請勿在此事件中立刻執行重新連線或斷線的動作。請收到此事件後啟用一個 Timer，等待數秒後再嘗試重新連線或斷線。 |

4-3-j OnReplyClearMessage
| 當公告開始清除前日資料時，會發出的通知 |
|---|
| 宣告 | void OnReplyClearMessage([in] BSTR bstrUserID); |
| 參數 | bstrUserID | 登入ID |
| 備註 |  |

4-3-k OnSmartData
| 當有智慧單回報將主動呼叫函式，並通知智慧單的狀態。 / 國內期貨/選擇權 / STP:停損單 MST:移動停損單 OCO:二擇一單  MIT:MIT單 / 請安排改接4-3-m OnStrategyData |
|---|
| 自SKCOM V2.13.32 (含)以上版本起,OnSmartData不提供證券智慧單回報相關格式更新. (MIT、當沖、出清、二擇一OCO) / *證券MIT、當沖、出清、二擇一OCO新單回報格式，請查閱4-3-m OnStrategyData。 / 自SKCOM V2.13.38 (含)以上版本起，移除OnSmartData期貨及選擇權智慧單(STP、 MIT、MST、OCO)回報相關格式. / *期貨MIT、STP、MST、二擇一OCO新單回報格式，請查閱4-3-m OnStrategyData。 |

4-3-m OnStrategyData
證券
| 當有智慧單回報將主動呼叫函式，並通知智慧單的狀態。 / 證券MST、MIOC、MIT、當沖、出清、OCO、AB、CB |
|---|
| 宣告 | void OnStrategyData([in] BSTR bstrUserID , [in] BSTR bstrData); |
| 參數 | bstrUserID | 登入ID |
|  | bstrData |  |
|  | 每一筆資料以「,」分隔每一個欄位 / ▼(證券)共用欄位 / ▼各智慧單別專屬欄位，接續共用欄位。 / MST only / (包含MST長效單回報欄位) / MIOC only / MIT only / (包含MIT長效單) / V2.13.45(含)以上新增預風控註記、拆單註記、長效單相關欄位 / DayTrade only / ClearOut only / OCO only / AB only / CB only / *CB only 紅字部分為v2.13.48修正之主動回報回傳值 |
|  |  |
| 備註 | 自SKCOM V2.13.45(含)以上版本, 4-3-m OnStrategyData提供證券MIT、當沖、出清、二擇一OCO、AB、CB、MBA、LLS / *自V2.13.48(含)以上版本，移除MMIT、MBA、LLS |

期貨
| 當有智慧單回報將主動呼叫函式，並通知智慧單的狀態。 / 期貨STP、MIT、MST、OCO、AB |
|---|
| 宣告 | void OnStrategyData([in] BSTR bstrUserID , [in] BSTR bstrData); |
| 參數 | bstrUserID | 登入ID |
|  | bstrData |  |
|  | 每一筆資料以「,」分隔每一個欄位 / ▼共用欄位 / ▼各智慧單別專屬欄位，接續共用欄位。 / STP only / MST only / MIT only / OCO only / AB only |
| 備註 | 自SKCOM V2.13.45(含)以上版本, 4-3-m OnStrategyData提供期貨MIT、STP、MST、二擇一OCO、AB新單回報格式 |

海期
| 當有智慧單回報將主動呼叫函式，並通知智慧單的狀態。 / 海期OCO、AB |
|---|
| 宣告 | void OnStrategyData([in] BSTR bstrUserID , [in] BSTR bstrData); |
| 參數 | bstrUserID | 登入ID |
|  | bstrData |  |
|  | 每一筆資料以「,」分隔每一個欄位 / ▼共用欄位 / ▼各智慧單別專屬欄位，接續共用欄位。 / OCO only / AB only |

4-4 SKQuoteLib ( 國內報價 )
函式
| 功能 | 函式名稱 | 備註 |
|---|---|---|
| 報價斷線 | SKQuoteLib_LeaveMonitor |  |
| 訂閱報價 | SKQuoteLib_RequestStocks | 不支援盤中零股。 |
| 訂閱Ticks及五檔 | SKQuoteLib_RequestTicks | 不支援盤中零股。 / OnNotifyTicksLONG、OnNotifyHistoryTicksLONG、OnNotifyBest5LONG |
| K線查詢 | SKQuoteLib_RequestKLine | 不支援盤中零股。 |
| 主機時間查詢 | SKQuoteLib_RequestServerTime |  |
| 取得大盤買賣 | SKQuoteLib_GetMarketBuySellUpDown | 不支援盤中零股。 |
| 訂閱MACD | SKQuoteLib_RequestMACD | 目前只提供證券市場，不含盤中零股 |
| 訂閱布林通道 | SKQuoteLib_RequestBoolTunel | 目前只提供證券市場，不含盤中零股。 |
| 訂閱期貨商品資訊 | SKQuoteLib_RequestFutureTradeInfo |  |
| 風險參數Delta | SKQuoteLib_Delta |  |
| 風險參數Gamma | SKQuoteLib_Gamma |  |
| 風險參數Theta | SKQuoteLib_Theta |  |
| 風險參數Vega | SKQuoteLib_Vega |  |
| 風險參數Rho | SKQuoteLib_Rho |  |
| 選擇權資訊 | SKQuoteLib_GetStrikePrices |  |
| 新K線查詢 | SKQuoteLib_RequestKLineAM | 國內期權可選盤別 |
| 國內商品清單查詢 | SKQuoteLib_RequestStockList | 盤中零股上市5、盤中零股上櫃 6 / 客製化期貨-9、客製化選擇權-10 |
| 訂閱即時成交明細 | SKQuoteLib_RequestLiveTick | 包含訂閱，僅即時成交明細 |
| 取得報價連線狀態 | SKQuoteLib_IsConnected |  |
| 取得證券市價特殊值 | SKQuoteLib_GetMarketPriceTS |  |
| 取消訂閱報價 | SKQuoteLib_CancelRequestStocks | 不支援盤中零股。 |
| 取消訂閱Tick及五檔 | SKQuoteLib_CancelRequestTicks | 不支援盤中零股。 |
| 取得連線狀態 | SKQuoteLib_GetQuoteStatus | (僅限連線成功後使用) |
| 指定區間 / K線查詢 | SKQuoteLib_RequestKLineAMByDate | 不支援盤中零股。 |
| 報價連線 | SKQuoteLib_EnterMonitorLONG | LONG index 對應 |
| 取得商品物件 | SKQuoteLib_GetStockByIndexLONG | LONG index 對應 |
| 取得商品物件 | SKQuoteLib_GetStockByNoLONG | LONG index 對應 / 不適用盤中零股 |
| 取得Tick | SKQuoteLib_GetTickLONG | LONG index 對應 |
| 取得五檔 | SKQuoteLib_GetBest5LONG | LONG index 對應 |
| 取得MACD | SKQuoteLib_GetMACDLONG | LONG index 對應 / 不支援盤中零股。 |
| 取得布林通道 | SKQuoteLib_GetBoolTunelLONG | LONG index 對應 / 不支援盤中零股。 |
| 取得行情報價 | SKQuoteLib_RequestStocksByMarketNo | 適用盤中零股 |
| 取得商品物件 | SKQuoteLib_GetStockByMarketAndNo | 適用盤中零股 |
| 取得即時成交明細Ticks&Best5 | SKQuote_RequestTicksWithMarketNo | 適用盤中零股 |
| 取得分K物件 | SKQuoteLib_GetLiveKLineLONG |  |
| 報價連線(指定要訂閱的市場) | SKQuoteLib_EnterMonitorLONGByMarket |  |

事件
| 功能 | 事件名稱 | 備註 |
|---|---|---|
| 連線狀態 | OnConnection | 避免在此處直接進行RequestStocks and RequestTicks 等報價訂閱 |
| K線資料 | OnNotifyKLineData |  |
| 主機時間 | OnNotifyServerTime |  |
| 大盤張筆 | OnNotifyMarketTot |  |
| 大盤買賣 | OnNotifyMarketBuySell |  |
| 大盤漲跌家數 | OnNotifyMarketHighLow | (包含權證) |
| 選擇權資訊 | OnNotifyStrikePrices | 選擇權 |
| 國內市場商品清單 | OnNotifyStockListData | (上市、上櫃、期貨、選擇權、 / 興櫃) |
| 大盤漲跌家數 | OnNotifyMarketHighLowNoWarrant | (同時回傳 / 含權證及不含權證資訊) |
| 國內市場商品清單含類別代碼 | OnNotifyCommodityListData WithTypeNo | (上市、上櫃、期貨、選擇權、 / 興櫃) |
| 報價更新通知 | OnNotifyQuoteLONG | LONG index 對應 |
| 回補Tick | OnNotifyHistoryTicksLONG | LONG index 對應 |
| 即時Tick | OnNotifyTicksLONG | LONG index 對應 |
| 即時五檔 | OnNotifyBest5LONG | LONG index 對應 |
| 布林通道 | OnNotifyBoolTunelLONG | LONG index 對應 |
| MACD | OnNotifyMACDLONG | LONG index 對應 |
| 期貨商品交易資訊 | OnNotifyFutureTradeInfoLONG | LONG index 對應 |
| 整零價差即時行情 | OnNotifyOddLotSpreadDeal |  |
| 歷史KLine回補完成 | OnKLineComplete |  |
| 即時分K | OnNotifyLiveKLineData |  |

4-4-1 SKQuoteLib_LeaveMonitor
| 中斷所有Solace伺服器連線。 |
|---|
| 宣告 | Long SKQuoteLib_LeaveMonitor(); |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 會中斷所有Solace連線，包含報價與回報，但不會中斷模擬平台回報連線與公告 / ＊斷線後，重新連線即還原設定： / 限制:『單一SKQuoteLib物件，僅一個訂閱Tick報價功能(10檔商品)及一個RequestStocks訂閱即時報價功能(100檔)』。 |

4-4-2 SKQuoteLib_RequestStocks
| 此功能不支援盤中零股。 / 訂閱指定商品即時報價 / 要求伺服器針對 bstrStockNos 內的商品代號訂閱商品報價通知動作。 |
|---|
| 宣告 | Long SKQuoteLib_RequestStocks([in,out] SHORT* psPageNo, [in] BSTR bstrStockNos); |
| 參數 | psPageNo | 請固定帶1。 |
|  | bstrStockNos | 欲訂閱的商品代號，一筆以上的資料時，每檔商品代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 4-4-b OnNotifyQuoteLONG |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品報價。 / *因應檔數限制，一個SKQuoteLib物件，僅可擇一使用一個即時報價訂閱(SKQuoteLib_RequestStocks功能或 SKQuoteLib_RequestStocksWithMarketNo功能)。 / 可重新連線即還原限制與設定。 / 舉例說明： / psPageNo = 1 時，bstrStockNos = “1101,1102,1103,1104,1108,1109,1110”，表示向伺服器索取這七檔股票的報價。 / 如果帶入的股票數超過100檔，則僅以100檔處理，並不會回傳錯誤。 / 如果帶入的股票代號不存在，則會直接略過不處理，也不會回傳錯誤。 / 特殊行情： / 部分期選商品有T+1盤別交易，若不需要T+1盤的資訊，可在商品代號最後加上AM以取得純AM盤行情，需注意委託商品時加上AM是無法下單的，委託請使用原始商品代號 / 例：TX00的純AM盤行情為TX00AM / 一般用戶目前PageNo上限為1 |

修改比較表:
| 修改前 |  | 修改後V2.13.54 |
|---|---|---|
| 當psPageNo=-1時帶入，函式庫會指定一個新的編號，並回傳給呼叫端。 | 參數psPageNo | 請固定帶1。 |
|  | 備註欄位 / 新增盤後交易介紹及參考資料 | 註:AM代表國內期選早盤行情，可參考臺灣期貨交易所股份有限公司 各商品規格。 / 查看交易時間 / https://www.taifex.com.tw/cht/2/tX / T+1盤： / 盤後交易介紹 / https://www.taifex.com.tw/cht/4/aHIntroduction |

4-4-3 SKQuoteLib_RequestTicks
| 此功能不支援盤中零股。 / 訂閱要求傳送成交明細、五檔、即時分K(包含當日分K回補)、該分鐘每筆Tick更新一次分K。 |
|---|
| 宣告 | Long SKQuoteLib_RequestTicks([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo); |
| 參數 | psPageNo | 請從0開始。 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 即時Tick由OnNotifyTicksLONG事件通知。 / Tick回補由 OnNotifyHistoryTicksLONG事件通知。 / 最佳五檔由OnNotifyBest5LONG事件通知。 / 即時分K(包含當日分K回補)及該分鐘每筆Tick更新一次分K由OnNotifyLiveKLineData事件通知。 |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品成交明細及最佳五檔。 / 特殊行情： / 部分期選商品有T+1盤別交易，若不需要T+1盤的資訊，可在商品代號最後加上AM以取得純AM盤行情，需注意委託商品時加上AM是無法下單的，委託請使用原始商品代號 / 例：TX00的純AM盤行情為TX00AM / *盤中零股：請使用SKQuoteLib_RequestTicksWithMarketNo. |

4-4-4 SKQuoteLib_RequestKLine
| （僅提供歷史資料）向報價伺服器提出，取得單一商品技術分析資訊需求。 |
|---|
| 宣告 | Long SKQuoteLib_RequestKLine([in] BSTR bstrStockNo, [in] SHORT sKLineType, [in] BSTR sOutType); |
| 參數 | bstrStockNo | 商品代號，例如 6005。 |
|  | sKLineType | 0 = 1分鐘線。 / 4 =完整日線。 / 5 =週線。 / 6 =月線。 |
|  | sOutType | 0=舊版輸出格式。 / 1=新版輸出格式。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyKLineData |
| 備註 | 不支援盤中零股。 / 證券:該查詢以整股為主計算，不包含「鉅額交易、盤中零股、盤後零股」 / 國內期選—全盤: / EX: 指定日期區間為2023/9/5全盤K LINE，將包含2023/9/4 17:25開盤後~2023/9/5 05:00，及2023/9/5 8:45~16:15。 / 國內期選—AM盤:當日T盤 / *若您未開立證券帳戶，無法訂閱或取得證券上市櫃、興櫃市場商品及上市櫃指數資料(含歷史K線)。 / *若您未開立期貨帳戶，無法訂閱或取得期貨選擇權相關市場商品資料(含歷史K線)。 / solace目前僅提供1分鐘K，其他5分鐘、30分鐘K Line可自行以1分鐘K Line為基礎組成，目前未提供288日K Line。 / 僅輸出AM盤，若需全盤請使用SKQuoteLib_RequestKLineAM |

比較表:
| 修改前 | 說明 | V2.13.45 |
|---|---|---|
| N/A | 備註 / 新增說明—證券市場 | 該查詢以整股為主計算，不包含「鉅額交易、盤中零股、盤後零股」 |
| N/A | 新增說明—國內期選盤別 | 全盤及AM盤 |

4-4-5 SKQuoteLib_RequestServerTime
| 要求報價主機傳送目前時間。 / 注意：為避免收盤後無報價資料傳送，導致連線被防火牆切斷，目前solace固定每五秒會自動更新時間，請固定每十五秒呼叫此函式，確保連線正常。 |
|---|
| 宣告 | Long SKQuoteLib_RequestServerTime(); |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 |  |

4-4-6 SKQuoteLib_GetMarketBuySellUpDown
| 要求傳送上市與上櫃大盤資訊(成交數,買賣數,漲跌家數)。 |
|---|
| 宣告 | Long SKQuoteLib_ GetMarketBuySellUpDown (); |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 大盤張筆OnNotifyMarketTot / 大盤買賣OnNotifyMarketBuySell / 大盤漲跌家數OnNotifyMarketHighLow / 大盤漲跌家數(『含權證家數』、 『不含權證家數』)OnNotifyMarketHighLowNoWarrant |
| 備註 | 不支援盤中零股。 |

4-4-7 SKQuoteLib_RequestMACD
| 要求傳送商品技術指標MACD。(平滑異同平均線) |
|---|
| 宣告 | Long SKQuoteLib_RequestMACD([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo); |
| 參數 | psPageNo | 每一次送出要求的動作都需要向報價伺服器指定一個 / 特定的 Page 編號，當代入 Page 為 50 會取消該檔的MACD。(請參考RequestStocks觀念說明)。 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 目前只提供證券市場，不含盤中零股。 |

4-4-8 SKQuoteLib_RequestBoolTunel
| 要求傳送商品布林通道BoolTunel。 |
|---|
| 宣告 | Long SKQuoteLib_RequestBoolTunel([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo); |
| 參數 | psPageNo | 每一次送出要求的動作都需要向報價伺服器指定一個 / 特定的 Page 編號，當代入 Page 為 50 會取消該檔的BoolTunel。(請參考RequestStocks觀念說明)。 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 目前只提供證券市場，不含盤中零股。 |

4-4-9 SKQuoteLib_RequestFutureTradeInfo
| 取得報價函式庫註冊接收期貨商品的交易資訊。 |
|---|
| 宣告 | Long SKQuoteLib_RequestFutureTradeInfo([in] SHORT* psPageNo, [in] BSTR bstrStockNo); |
| 參數 | psPageNo | 每一次送出要求的動作都需要向報價伺服器指定一個 / 特定的 Page 編號。(請參考RequestStocks觀念說明)。 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
|  |  |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 若未簽署期貨API下單同意書，將無法查詢。（錯誤代碼3031） |

4-4-10 SKQuoteLib_Delta
| 輸入 / 1.nCallPut商品買賣權別(Call: 0, Put: 1) / 2.S    指數 / 3. K    StrikePrice(履約價) / 4. R     RisklessRate(無風險利率) / 5. T     RemainMaturity(剩餘天數) / 6. sigma / 得到Delta值 |
|---|
| 宣告 | Long SKQuoteLib_Delta([in] SHORT nCallPut, [in] DOUBLE S, [in] DOUBLE K, [in] DOUBLE R, [in] DOUBLE T, [in] DOUBLE sigma, [out] DOUBLE* dDelta); |
| 參數 | nCallPut | 商品買賣權別( Call: 0 , Put: 1) |
|  | S | 指數 |
|  | K | StrikePrice(履約價) |
|  | R | RisklessRate(無風險利率) |
|  | T | RemainMaturity(剩餘天數) |
|  | sigma | sigma |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |

4-4-11  SKQuoteLib_Gamma
| 輸入 / 1. S 指數 / 2. K StrikePrice(履約價) / 3. R RisklessRate(無風險利率) / 4. T RemainMaturity(剩餘天數) / 5.sigma / 得到Gamma值 |
|---|
| 宣告 | Long SKQuoteLib_Gamma([[in] DOUBLE S, [in] DOUBLE K, [in] DOUBLE R, [in] DOUBLE T, [in] DOUBLE sigma, [out] DOUBLE* dGamma); |
| 參數 | S | 指數 |
|  | K | StrikePrice(履約價) |
|  | R | RisklessRate(無風險利率) |
|  | T | RemainMaturity(剩餘天數) |
|  | sigma | sigma |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |

4-4-12   SKQuoteLib_Theta
| 輸入 / 1.nCallPut商品買賣權別(Call: 0,Put: 1) / 2.S    台指期成交價 / 3. K    StrikePrice(履約價) / 4. R     RisklessRate(無風險利率) / 5. T     RemainMaturity(剩餘天數) / 6. v               波動率=sigma / 得到Theta值 |
|---|
| 宣告 | Long SKQuoteLib_Theta(([[in] SHORT nCallPut, [in] DOUBLE S, [in] DOUBLE K, [in] DOUBLE R, [in] DOUBLE T, [in] DOUBLE v, [out] DOUBLE* dTheta); |
| 參數 | nCallPut | 商品買賣權別( Call: 0 , Put: 1) |
|  | S | 台指期成交價 |
|  | K | StrikePrice(履約價) |
|  | R | RisklessRate(無風險利率) |
|  | T | RemainMaturity(剩餘天數) |
|  | sigma | V波動率=sigma |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |

4-4-13  SKQuoteLib_Vega
| 輸入 / 1. S 台指期成交價 / 2. K StrikePrice(履約價) / 3. R RisklessRate(無風險利率) / 4. T RemainMaturity(剩餘天數) / 5.v   波動率=sigma / 得到Vega值 |
|---|
| 宣告 | Long SKQuoteLib_Vega([[in] DOUBLE S, [in] DOUBLE K, [in] DOUBLE R, [in] DOUBLE T, [in] DOUBLE sigma, [out] DOUBLE* dVega); |
| 參數 | S | 台指期成交價 |
|  | K | StrikePrice(履約價) |
|  | R | RisklessRate(無風險利率) |
|  | T | RemainMaturity(剩餘天數) |
|  | sigma | V波動率=sigma |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |

4-4-14  SKQuoteLib_Rho
| 輸入 / 1.nCallPut商品買賣權別(Call: 0, Put: 1) / 2.S    台指期成交價 / 3. K    StrikePrice(履約價) / 4. R     RisklessRate(無風險利率) / 5. T     RemainMaturity(剩餘天數) / 6. v               波動率=sigma / 得到Rho值 |
|---|
| 宣告 | Long SKQuoteLib_Rho(([[in] SHORT nCallPut, [in] DOUBLE S, [in] DOUBLE K, [in] DOUBLE R, [in] DOUBLE T, [in] DOUBLE v, [out] DOUBLE* dRho); |
| 參數 | nCallPut | 商品買賣權別( Call: 0 , Put: 1) |
|  | S | 指數 |
|  | K | StrikePrice(履約價) |
|  | R | RisklessRate(無風險利率) |
|  | T | RemainMaturity(剩餘天數) |
|  | sigma | V波動率=sigma |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |

4-4-15 SKQuoteLib_GetStrikePrices
| 取得報價函式庫選擇權交易商品資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetStrikePrices() |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 / 若未簽署期貨API下單同意書，將無法查詢。（錯誤代碼3031） |

4-4-16 SKQuoteLib_RequestKLineAM
| （僅提供歷史資料）向報價伺服器提出，取得單一商品技術分析資訊需求，可選AM盤或全盤。 |
|---|
| 宣告 | Long SKQuoteLib_RequestKLineAM([in] BSTR bstrStockNo, [in] SHORT sKLineType, [in] BSTR sOutType, [in] SHORT sTradeSession); |
| 參數 | bstrStockNo | 商品代號，例如 6005。 |
|  | sKLineType | 0 = 1分鐘線。 / 4 =完整日線。 / 5 =週線。 / 6 =月線。 |
|  | sOutType | 0=舊版輸出格式。 / 1=新版輸出格式。 |
|  | sTradeSession | 僅國內期權技術分析有效。 / 0=全盤技術分析 / 1=AM盤技術分析 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyKLineData |
| 備註 | 國內期選—全盤: / EX: 2023/9/5全盤K LINE，將包含2023/9/4 17:25開盤後~2023/9/5 05:00，及2023/9/5 8:45~16:15。 / 國內期選—AM盤:當日T盤 / *若您未開立期貨帳戶，無法訂閱或取得期貨選擇權相關市場商品資料(含歷史K線)。 / 目前僅提供1分鐘K，其他5分鐘、30分鐘K Line可自行以1分鐘K Line為基礎組成，目前未提供288日K Line。 |

比較表:
| 修改前 | 說明 | V2.13.45 |
|---|---|---|
| N/A | 新增說明—國內期選盤別 | 全盤及AM盤 |

4-4-17 SKQuoteLib_RequestStockList
| 根據市場別編號，取得國內各市場代碼所包含的商品基本資料相關資訊。 |
|---|
| 宣告 | Long SKQuoteLib_RequestStockList([in] SHORT sMarketNo); |
| 參數 | sMarketNo | 市場別代碼。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyStockList |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，再進行商品檔查詢。 / 市場編號分別為： / 上市 0、上櫃 1、期貨 2、選擇權 3、興櫃 4、 / 盤中零股-上市5、盤中零股-上櫃6 / 客製化期貨-9、客製化選擇權-10 / 若未開立證券帳戶或未簽署證券API下單同意書，將無法查詢 / ”上市 0、上櫃 1、興櫃 4”、盤中零股-上市5、盤中零股-上櫃6。（錯誤代碼3031） / 若未開立期貨帳戶或未簽署期貨API下單同意書，將無法查詢”期貨 2、選擇權 3”。（錯誤代碼3031） |

4-4-18 SKQuoteLib_RequestLiveTick
| 訂閱與要求傳送即時成交明細。 / (本功能不會訂閱最佳五檔，亦不包含歷史Ticks) |
|---|
| 宣告 | Long SKQuoteLib_RequestLiveTick([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo); |
| 參數 | psPageNo | 請從0開始。 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 即時Tick由OnNotifyTicksLONG事件通知。 |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，再進行商品檔查詢。 / 與SKQuoteLib_RequestTicks不同，不會回補歷史成交明細資料，請擇一使用;因無回補歷史成交明細資料，SKQuoteLib_GetTickLONG()僅查閱，訂閱後之即時成交明細資料。 |

4-4-19 SKQuoteLib_IsConnected
| 檢查目前報價的連線狀態。 |
|---|
| 宣告 | Long SKQuoteLib_IsConnected(); |
| 參數 | 無 |
| 回傳值 | 0表示斷線。1表示連線中。2表示下載中。 / 錯誤代碼可參考對照表。 |
| 備註 | 請同時接收通知事件4-4-a OnConnection |

4-4-20 SKQuoteLib_GetMarketPriceTS
| 取得證券市場逐筆交易價格欄位為市價時之特殊值。 |
|---|
| 宣告 | Long SKQuoteLib_GetMarketPriceTS(); |
| 參數 | 無 |
| 回傳值 | 市價:-2147483647 |
| 備註 | 證券即時報價，凡買價、賣價為「市價」時判斷依據 / 如下圖範例所示： |

4-4-21 SKQuoteLib_CancelRequestStocks
| 取消訂閱SKQuoteLib_RequestStocks的報價通知，並停止更新商品報價。 |
|---|
| 宣告 | Long SKQuoteLib_CancelRequestStocks([in] BSTR bstrStockNos); |
| 參數 | bstrStockNos | 欲解除訂閱的商品代號，一筆以上的資料時，每檔股票代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 不支援盤中零股。 |

4-4-22 SKQuoteLib_CancelRequestTicks
| 取消訂閱RequestTicks的成交明細及五檔。 |
|---|
| 宣告 | Long SKQuoteLib_CancelRequestTicks([in] BSTR bstrStockNo); |
| 參數 | bstrStockNo | 欲解除訂閱的商品代號，一次僅能解訂閱一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 不支援盤中零股。 |

4-4-23 SKQuoteLib_GetQuoteStatus
| 查詢報價連線狀態(是否超過報價連線限制,連線數資訊)。 |
|---|
| 宣告 | Long SKQuoteLib_GetQuoteStatus( / [in,out] LONG* pnConnectionCount, / [in,out] VARIANT_BOOL* pbIsOutLimit ); |
| 參數 | pnConnectionCount | 連線數: / 當pbIsOutLimit連線數超過限制為true , 連線數為目前限制最大可使用連線數 / 當pbIsOutLimit連線數超過限制為false, 連線數為先前已使用連線數(不含當次新連線) / 當pnConnectionCount 帶入任一數值，函式庫會回傳連線數，並回傳給呼叫端。 |
|  | pbIsOutLimit | 報價連線數是超過限制 / 當pbIsOutLimit  帶入false，函式庫會回傳是否超過連線數布林值，並回傳給呼叫端。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 限當次報價連線成功後，查詢用 / ＊EX：若最大連線數為2, 且目前連線超過限制,則回傳:2,True. |

4-4-24 SKQuoteLib_RequestKLineAMByDate
| （僅提供歷史資料）向報價伺服器提出，取得單一商品技術分析資訊需求，可選AM盤或全盤，可指定日期區間，分K時可指定幾分K。 |
|---|
| 宣告 | Long SKQuoteLib_RequestKLineAMByDate([in] BSTR bstrStockNo, [in] SHORT sKLineType, [in] SHORT sOutType, [in] SHORT sTradeSession, [in] BSTR bstrStartDate, [in] BSTR bstrEndDate, [in] SHORT sMinuteNumber); |
| 參數 | bstrStockNo | 商品代號，例如 6005。 |
|  | sKLineType | 0 = 分線。 / 4 = 日線。 / 5 = 週線。 / 6 = 月線。 |
|  | sOutType | 0 = 舊版輸出格式。 / 1 = 新版輸出格式。 |
|  | sTradeSession | 僅國內期權技術分析有效。 / 0 = 全盤技術分析 / 1 = AM盤技術分析 |
|  | bstrStartDate | 起始日期，格式為：YYYYMMDD (ex:20201001) |
|  | bstrEndDate | 結束日期，格式為：YYYYMMDD (ex:20201010) |
|  | sMinuteNumber | 指定幾分K (ex : 1=1分K, 3=3分K) / sKLineType = 0時, 此參數才有意義 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyKLineData |
| 備註 | 不支援盤中零股。 / 證券:該查詢以整股為主計算，不包含「鉅額交易、盤中零股、盤後零股」 / 國內期選—全盤: / EX: 指定日期區間為2023/9/5全盤K LINE，將包含2023/9/4 17:25開盤後~2023/9/5 05:00，及2023/9/5 8:45~16:15。 / 國內期選—AM盤:當日T盤 / *若您未開立證券帳戶，無法訂閱或取得證券上市櫃、興櫃市場商品及上市櫃指數資料(含歷史K線)。 / *若您未開立期貨帳戶，無法訂閱或取得期貨選擇權相關市場商品資料(含歷史K線)。 |

比較表:
| 修改前 | 說明 | V2.13.45 |
|---|---|---|
| N/A | 備註 / 新增說明—證券市場 | 該查詢以整股為主計算，不包含「鉅額交易、盤中零股、盤後零股」 |
| N/A | 新增說明—國內期選盤別 | 全盤及AM盤 |

4-4-25 SKQuoteLib_EnterMonitorLONG
| (LONG index)與報價伺服器建立連線。（含盤中零股市場商品） |
|---|
| 宣告 | Long SKQuoteLib_EnterMonitorLONG(); |
| 參數 | 無 |  |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 連線狀態由 OnConnection 事件回傳。 / 需先簽署證券或期貨API下單聲明書，方可使用 / SKQuoteLib_EnterMonitor 跟 SKQuoteLib_EnterMonitorLONG僅能夠擇一使用。 / 使用此函式登入後，相同性質的事件，系統僅會回傳LONG index類型的事件 / 不會被觸發： / OnNotifyQuote / OnNotifyHistoryTicks / OnNotifyTicks / OnNotifyBest5 / OnNotifyBoolTunel / OnNotifyMACD / OnNotifyFutureTradeInfo / 會被觸發： / OnNotifyQuoteLONG / OnNotifyHistoryTicksLONG / OnNotifyTicksLONG / OnNotifyBest5LONG / OnNotifyBoolTunelLONG / OnNotifyMACDLONG / OnNotifyFutureTradeInfoLONG |

4-4-26 SKQuoteLib_GetStockByIndexLONG
| 請先訂閱即時報價(SKQuoteLib_ReqeustStocks),方可取得商品報價 / 未訂閱即時報價,僅可取得商品基本資料 / (LONG index)根據市場別編號與系統所編的索引代碼，取回商品報價的及商品相關資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetStockByIndexLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in,out] struct SKSTOCKLONG* pSKStock); |
| 參數 | sMarketNo | 市場別代碼。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | pSKStock | SKCOM元件中的 SKSTOCKLONG物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 市場編號分別為：與SKSTOCK物件內之BSTRbstrMarketNo 型態不同。 / ＊須等收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行。 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 / ＊依國內或海外市場，可能需搭配MarketNo / ＊未訂閱即時報價，EX:只先訂成交明細SKQuoteLib_RequestTicks時，執行此功能，僅可取得商品基本資料(商品名稱、昨收價，非即時性質欄位) |

4-4-27 SKQuoteLib_GetStockByNoLONG
| (LONG index)根據商品代號，取回商品報價的相關資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetStockByNoLONG ([in] BSTR bstrStockNo, [in,out] struct SKSTOCKLONG* pSKStock); |
| 參數 | bstrStockNo | 商品代號，例如 6005。 |
|  | pSKStock | SKCOM元件中的 SKSTOCKLONG物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 僅適用現股，不適用盤中零股-上市及盤中零股-上櫃 / ＊須等收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行。 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 / 未訂閱即時報價，EX:只先訂成交明細SKQuoteLib_RequestTicks時，執行此功能，僅可取得商品基本資料(商品名稱、昨收價，非即時性質欄位) |

4-4-28 SKQuoteLib_GetTickLONG
| (LONG index)(需先訂閱即時成交明細RequestTicks)取得成交明細資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetTickLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in] LONG nPtr, [in,out] struct SKTICK* pSKTick); |
| 參數 | sMarketNo | 市場別代碼。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | nPtr | 第幾筆成交明細，又稱成交明細順序。可以根據此 Ptr 取得指定第幾筆成交明細資訊。 |
|  | pSKTick | SKCOM元件中的 SKTICK物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 避免在SKQuoteLib_OnNotifyHistoryTicksLONG、SKQuoteLib_OnNotifyTicksLONG通知事件裡進行SKQuoteLib_GetTickLONG() / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 / *Ptr: 第幾筆成交明細，由0開始 |

4-4-29 SKQuoteLib_GetBest5LONG
| (LONG index)(需先訂閱最佳五檔RequestTicks)取得最佳五檔價格資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetBest5LONG([in] SHORT sMarketNo, [in] LONG nIndex, [in,out] struct SKBEST5* pSKBest5); |
| 參數 | sMarketNo | 市場別代碼。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | pSKStock | SKCOM元件中的 SKBEST5物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 避免在SKQuoteLib_OnNotifyBest5LONG通知事件裡進行SKQuoteLib_GetBest5LONG() / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 |

4-4-30SKQuoteLib_GetMACDLONG
| (LONG index)取得商品技術指標MACD資訊。(平滑異同平均線) |
|---|
| 宣告 | Long SKQuoteLib_GetMACDLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in,out] struct SKMACD* pSKMACD); |
| 參數 | sMarketNo | 市場別代碼。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | pSKStock | SKCOM元件中的 SKMACD物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 目前只提供證券市場 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 |

4-4-31 SKQuoteLib_GetBoolTunelLONG
| (LONG index)取得商品BoolTunel資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetBoolTunelLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in,out] struct SKBoolTunel* pSKBoolTunel); |
| 參數 | sMarketNo | 市場別代碼。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | pSKBoolTunel | SKCOM元件中的 SKBoolTunel物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 目前只提供證券市場 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 |

4-4-32 SKQuoteLib_RequestStocksWithMarketNo
| 訂閱指定市場別及指定商品即時報價 / 要求伺服器針對sMarketNo市場別、 bstrStockNos 內的商品代號訂閱商品報價通知動作。 |
|---|
| 宣告 | Long SKQuoteLib_RequestStocksWithMarketNo([in,out] SHORT* psPageNo, [in] SHORT sMarketNo, [in] BSTR bstrStockNos); |
| 參數 | psPageNo | 請固定帶1。 |
|  | sMarketNo | (目前提供) / 盤中零股-上市(5)、盤中零股- 上櫃(6)市場代碼 / 客製化期貨-9、客製化選擇權-10 |
|  | bstrStockNos | 欲訂閱的商品代號，一筆以上的資料時，每檔商品代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 4-4-k OnNotifyQuoteLONG |
| 備註 | 適用盤中零股 / ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品報價。 / *因應檔數限制，一個SKQuoteLib物件，僅可擇一使用一個即時報價訂閱(SKQuoteLib_RequestStocks功能或 SKQuoteLib_RequestStocksWithMarketNo功能)。 / 可重新連線即還原限制與設定。 |

4-4-33 SKQuoteLib_GetStockByMarketAndNo
| (LONG index)根據市場別編號與商品代號，取回商品報價的相關資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetStockByMarketAndNo ([in]SHORT sMarketNo,[in] BSTR bstrStockNo, [in,out] struct SKSTOCKLONG* pSKStock); |
| 參數 | sMarketNo | 市場別編號 |
|  | bstrStockNo | 商品代號，例如 6005。 |
|  | pSKStock | SKCOM元件中的 SKSTOCKLONG物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 適用盤中零股 / ＊須等收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行。 / 未訂閱即時報價SKQuoteLib_RequestStocksWithMarketNo / ，執行此功能，僅可取得商品基本資料(商品名稱、昨收價，非即時性質欄位) |

4-4-34 SKQuoteLib_RequestTicksWithMarketNo
| 訂閱要求傳送成交明細以及五檔。 |
|---|
| 宣告 | Long SKQuoteLib_RequestTicksWithMarketNo([in,out] SHORT* psPageNo, [in] SHORT sMarketNo ,[in] BSTR bstrStockNo); |
| 參數 | psPageNo | 請從0開始。 |
|  | sMarketNo | 盤中零股-上市(5)、盤中零股- 上櫃(6)市場代碼 / 客製化期貨-9、客製化選擇權-10 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 即時Tick由OnNotifyTicksLONG事件通知。 / Tick回補由 OnNotifyHistoryTicksLONG事件通知。 / 最佳五檔由OnNotifyBest5LONG事件通知。 |
| 備註 | 適用盤中零股 / ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品成交明細及最佳五檔。 |

4-4-35 SKQuoteLib_GetLiveKLineLONG
| (LONG index)(需先訂閱即時分K RequestLiveKLine)取得分K資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetLiveKLineLONG([in] BSTR bstrStockNo, [in,out] struct SKKLINE* pSKKLine); |
| 參數 | bstrStockNo | 商品代號，例如 6005。 |
|  | pSKKLine | SKCOM元件中的 SKKLINE物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 避免在OnNotifyLiveKLineData通知事件裡進行SKQuoteLib_GetLiveKLineLONG。 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 |

4-4-36 SKQuoteLib_EnterMonitorLONGByMarket
| (LONG index)與報價伺服器建立連線(指定要訂閱的市場)。 |
|---|
| 宣告 | Long SKQuoteLib_EnterMonitorLONGByMarket([in] LONG nMarketType); |
| 參數 | nMarketType | 0:只訂閱國內證券市場 / 1:只訂閱國內期貨市場 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 與SKQuoteLib_EnterMonitorLONG，請擇一使用。 |

4-4-a OnConnection
| 接收連線狀態。 |
|---|
| 宣告 | void OnConnection([in] LONG nKind, [in] LONG nCode); |
| 參數 | nKind | SK_SUBJECT_CONNECTION_CONNECTED 表示為連線事件。 / SK_SUBJECT_CONNECTION_DISCONNECT 表示為斷線事件。 / SK_SUBJECT_CONNECTION_STOCKS_READY 表示為商品基本資料已下載完成。 / 其他錯誤代碼，可參考6代碼定義表 |
|  | nCode | 表示事件所獲得的代碼，代碼為0值即表示執行正確，非0表示例外事件。 |
| 備註 | 避免在此處直接進行EnterMonitor、LeaveMonitor 、RequestStocks and RequestTicks 等報價連線、斷線、訂閱，若全市場商品未下載完成，即無法進行商品訂閱。 |

4-4-b OnNotifyKLineData
| 事件回傳技術分析資訊。 |
|---|
| 宣告 | void OnNotifyKLineData([in] BSTR bstrStockNo, [in] BSTR bstrData); |
| 參數 | bstrStockNo | 商品代號。 |
|  | bstrData | 回傳字串，技術分析資料。傳回字串格式分成四種: / (1.) 舊版輸出格式，1分鐘線。以逗號分開資料。 / ( 月/日/年, 時:分, 開盤價, 最高價, 最低價, 收盤價, 成交量 ) / 例如，06/18/2009, 09:05, 1365, 1385, 1365, 1380, 656 / (2.) 舊版輸出格式，日線288天，完整日線，週線，月線。以逗號分開資料。 / ( 月/日/年, 開盤價, 最高價, 最低價, 收盤價, 成交量 ) / 例如，06/18/2009, 1365, 1385, 1365, 1380, 656 / 舊版輸出函式中所取回的價格都未經過小數點處理，例如1101 價格為「36.50」，則函式所傳回的價格為「3650」，因此使用者所開發的程式則必須接手處理。 / 特別注意舊版輸出格式中的期匯率商品(TypeNo=209)小位數有四位須特別再處理，例如RH1704價格為「6.8712」，則函式所傳回的價格為「68712」 / (3.) 新版輸出格式，1分鐘線。以逗號分開資料。 / (年/月/日, 時:分, 開盤價, 最高價, 最低價, 收盤價, 成交量) / 例如，商品代號(1108) / 2021/8/2 09:01 12.95,12.95,12.95.12.95,5 / (4.) 新版輸出格式，日線288天，完整日線，週線，月線。以逗號分開資料。 / (年/月/日, 開盤價, 最高價, 最低價, 收盤價, 成交量 ) / 例如，商品代號(1108) / 2021/8/2, 12.95,13.00,12.80,12.95,291 / 新版輸出函式中所取回的價格已進行過小數點處理，且由Solace K線主機提供． |
| 備註 | 證券-日K:成交量為整股交易，不含【鉅額交易、盤中零股、盤後零股】 |

比較表:
| 修改前 | 說明 | V2.13.45 |
|---|---|---|
| 上市股票(商品)代號。 | bstrStockNo | 商品代號 |
| N/A | 備註 | 日K:成交量為整股交易，不含【鉅額交易、盤中零股、盤後零股】 |

4-4-c OnNotifyServerTime
| 事件回傳查詢主機時間的結果。 |
|---|
| 宣告 | void OnNotifyServerTime([in] SHORT sHour, [in] SHORT sMinute, [in] SHORT sSecond, [in] LONG nTotal); |
| 參數 | sHour | 時 |
|  | sMinute | 分 |
|  | sSecond | 秒 |
|  | nTotal | 總秒數 |
| 備註 |  |

4-4-d OnNotifyMarketTot
| 透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後， / 事件回傳大盤成交張筆資料。 |
|---|
| 宣告 | void OnNotifyMarketTot([in] SHORT sMarketNo, [in] SHORT sPtr, [in] LONG nTime, [in] LONG nTotv, [in] LONG nTots, [in] LONG nTotc); |
| 參數 | sMarketNo | 市場別代號 (0x00 上市, 0x01 上櫃) |
|  | sPtr | 目前第幾筆資料。 / 可以根據此 index 取得該筆資訊。 |
|  | nTime | 大盤成交時間。Ex. lTime = 92000 ，為早上九點20分(09:20)。 |
|  | nTotv | 大盤成交值(億)。Ex. lTotv = 88542，為885.42 (億)。 |
|  | nTots | 大盤成交張數。 |
|  | nTotc | 大盤成交筆數。 |
| 備註 |  |

4-4-e OnNotifyMarketBuySell
| 透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後， / 事件回傳大盤成交買賣張筆數資料。 |
|---|
| 宣告 | void OnNotifyMarketBuySell([in] SHORT sMarketNo, [in] SHORT sPtr, [in] LONG nTime, [in] LONG nBc, [in] LONG nSc, [in] LONG nBs, [in] LONG nSs); |
| 參數 | sMarketNo | 市場別代號 (0x00 上市, 0x01 上櫃) |
|  | sPtr | 目前第幾筆資料。 / 可以根據此 index 取得該筆資訊。 |
|  | nTime | 大盤成交時間。Ex. lTime = 92000 ，為早上九點20分(09:20)。 |
|  | nBc | 大盤成交買進筆數 |
|  | nSc | 大盤成交賣出筆數 |
|  | nBs | 大盤成交買進張數 |
|  | nSs | 大盤成交賣出張數 |
| 備註 |  |

4-4-f OnNotifyMarketHighLow
| 透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後， / 事件回傳大盤成交上漲下跌家數資料。 |
|---|
| 宣告 | void OnNotifyMarketHighLow([in] SHORT sMarketNo, [in] SHORT sPtr, [in] LONG nTime, [in] SHORT sUp, [in] SHORT sDown, [in] SHORT sHigh, [in] SHORT sLow, [in] SHORT sNoChange); |
| 參數 | sMarketNo | 市場別代號 (0x00 上市, 0x01 上櫃) |
|  | sPtr | 目前第幾筆資料。 / 可以根據此 index 取得該筆資訊。 |
|  | nTime | 大盤成交時間。Ex. lTime = 92000 ，為早上九點20分(09:20)。 |
|  | sUp | 大盤成交上漲家數 |
|  | sDown | 大盤成交下跌家數 |
|  | sHigh | 大盤成交漲停家數 |
|  | sLow | 大盤成交跌停家數 |
|  | sNoChange | 大盤平盤家數 |
| 備註 |  |

4-4-g OnNotifyStrikePrices
| 選擇權資訊。透過呼叫 GetStrikePrices 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnNotifyStrikePrices([in] BSTR bstrOptionData); |
| 參數 | bstrData | 回傳的字串內容，以「,」分隔每一個欄位，欄位依序為： / [選擇權商品代碼],[選擇權中文名],[Call商品買賣權代碼],[Put商品買賣權代碼],[履約價],[年+月],[最後交易日] / ex: TXO,台選,TXO11000H8,TXO11000T8,1100000,201808,20180815 |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

4-4-h OnNotifyStockList
| 事件回傳指定國內市場－各類股商品清單。 |
|---|
| 宣告 | void OnNotifyStockList([in] SHORT sMarketNo, [in] BSTR bstrStockData); |
| 參數 | sMarketNo | 市場別代號 |
|  | bstrStockData | 商品資料(商品代碼及商品中文名稱) / 以上市0 – 水泥類股為例，以「,」分隔每一欄，欄位依序為： / [商品代碼],[商名名稱],[最後交易日],[交易所商品代碼], [跳動點],[幣別]; / 另以「；」做為下一筆資料分隔， / 而各類股結尾（水泥、食品、塑膠等）則以「換行」做分隔。 / 1101,台泥,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;1102,亞泥,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;….. / 1201,味全,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;1203,味王,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD; …… |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 價格跳動點顯示範例 / 例： 50\|0.05  50元(不含)以下,0.05以“\|“管線隔開  以“/“隔開每筆資料 / 10\| 0.01 \|/ 50\| 0.05 \| / ＜＝代表10~50 之間價格跳動點為0.05 / 10\| 0.01 \| / <=代表０～10之間，最小價格跳動點為0.01 / 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \| |

4-4-i OnNotifyMarketHighLowNoWarrant
| 透過呼叫 SKQuoteLib_GetMarketBuySellUpDown 後， / 事件回傳大盤成交上漲下跌家數資料(包含『含權證家數』、 『不含權證家數』)。 |
|---|
| 宣告 | void OnNotifyMarketHighLowNoWarrant([in] SHORT sMarketNo, [in] LONG nPtr, [in] LONG nTime, [in] LONG nUp, [in] LONG nDown, [in] LONG nHigh, [in] LONG nLow, [in] LONG nUnChange, [in] LONG nUpNoW, [in] LONG nDownNoW, [in] LONG nHighNoW, [in] LONG nLowNoW, [in] LONG nUnChangeNoW); |
| 參數 | sMarketNo | 市場別代號 (0x00 上市, 0x01 上櫃) |
|  | sPtr | 目前第幾筆資料。 / 可以根據此 index 取得該筆資訊。 |
|  | nTime | 大盤成交時間。Ex. lTime = 92000 ，為早上九點20分(09:20)。 |
|  | nUp | 大盤成交上漲家數 |
|  | nDown | 大盤成交下跌家數 |
|  | nHigh | 大盤成交漲停家數 |
|  | nLow | 大盤成交跌停家數 |
|  | nNoChange | 大盤平盤家數 |
|  | nUpNoW | (不含權證)大盤成交上漲家數 |
|  | nDownNoW | (不含權證)大盤成交下跌家數 |
|  | nHighNoW | (不含權證)大盤成交漲停家數 |
|  | nLowNoW | (不含權證)大盤成交跌停家數 |
|  | nUnChangeNoW | (不含權證)大盤平盤家數 |
| 備註 |  |

4-4-j OnNotifyCommodityListWithTypeNo
| 事件回傳指定國內市場－含類別代碼及類別中文名稱之商品清單。 |
|---|
| 宣告 | void OnNotifyCommodityListWithTypeNo([in] SHORT sMarketNo, [in] BSTR bstrCommodityData); |
| 參數 | sMarketNo | 市場別代號 |
|  | bstrCommodityData | 開頭含類別代碼，格式為「%」、類別代碼「%」類別中文名稱，結尾為「%」。 / EX: %1%水泥% / 接著為商品資料(商品代碼及商品中文名稱) / 以上市0 – 水泥類股為例，以「,」分隔每一欄，欄位依序為： / [商品代碼],[商名名稱],[最後交易日],[交易所商品代碼], [跳動點],[幣別]; / 另以「；」做為下一筆資料分隔， / 而各類別結尾（水泥、食品、塑膠等）則以「換行」做分隔。 / 以上市商品-代碼0 / %1%水泥%1101,台泥,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;1102,亞泥,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;1103,嘉泥,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;….. / ;%2%食品%1201,味全,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD;1203,味王,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD; 1210,大成,0, 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \|,NTD; …… |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 / 價格跳動點顯示範例 / 例： 50\|0.05  50元(不含)以下,0.05以“\|“ 隔開  以“/“隔開每筆資料 / 10\| 0.01 \|/ 50\| 0.05 \| / ＜＝代表10~50 之間價格跳動點為0.05 / 10\| 0.01 \| / <=代表０～10之間，最小價格跳動點為0.01 / 10\| 0.01 \|/ 50\| 0.05 \|/ 100 \| 0.1 \| / 500\| 0.5 \|/ 1000\| 1 \|/ 10000\| 5 \| |

4-4-k OnNotifyQuoteLONG
| (LONG index)當有索取的個股報價異動時，將透過此事件通知應用程式處理。 |
|---|
| 宣告 | void OnNotifyQuoteLONG([in] SHORT sMarketNo, [in] LONG nIndex); |
| 參數 | sMarketNo | 報價有異動的商品市場別。 |
|  | nIndex | 系統所編的索引代碼。 |
| 備註 | 透過以上兩個參數，可以再使用SKQuoteLib_GetStockByIndexLONG 取出報價的內容。 / *開發人員可於OnUpDateDataRow(SKSTOCK pStock) 取得Stock揭示值，另外自行接手處理 / *依國內或海外市場，可能需搭配MarketNo / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-l OnNotifyHistoryTicksLONG
| (LONG index)當首次索取個股成交明細，此事件會回補當天Tick。 |
|---|
| 宣告 | void OnNotifyHistoryTicksLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in] LONG nPtr, [in] LONG nDate, [in] LONG nTimehms, [in] LONG nTimemillismicros, [in] LONG nBid, [in] LONG nAsk, [in] LONG nClose, [in] LONG nQty, [in] LONG nSimulate); |
| 參數 | sMarketNo | 報價有異動的商品市場別。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | nPtr | 表示資料的位址(Key) |
|  | nDate | 交易日期。(YYYYMMDD) |
|  | nTimehms | 時間1。(時：分：秒’毫秒"微秒) |
|  | nTimemillismicros | 時間2。(’毫秒"微秒) |
|  | nBid | 買價。 |
|  | nAsk | 賣價。 |
|  | nClose | 成交價。 |
|  | nQty | 成交量。 |
|  | nSimulate | 0:一般揭示 1:試算揭示 |
| 備註 | nTimehms提供單位：時分秒hh:mm:ss。 / nTimemillismicros提供單位：毫秒微秒’millissecond’’micorsecond，預設提供毫秒微秒，開發人員自行決定是否顯示。(目前solace只提供證券商品) / 未進行揭示處理，開發人員需自行接手處理(判斷收的tick 為一般或試算揭示)。 / T盤切換T+1盤，不保留前一盤資料，開發者需自行清除前一盤資料。 / 避免在SKQuoteLib_OnNotifyHistoryTicksLONG通知事件裡進行SKQuoteLib_GetTickLONG()、SKQuoteLib_GetStockByIndexLONG(只可取得基本資料) / 盤中零股: 因盤中仍會進入試撮，請留意是否收取『試撮資料』，即試算揭示. / *依國內或海外市場，可能需搭配MarketNo / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-m OnNotifyTicksLONG
| (LONG index)當有索取的個股成交明細有所異動，即透過向此註冊事件回傳所異動的個股成交明細。 |
|---|
| 宣告 | void OnNotifyTicksLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in] LONG nPtr, [in] LONG nDate, [in] LONG nTimehms, [in] LONG nTimemillismicros, [in] LONG nBid, [in] LONG nAsk, [in] LONG nClose, [in] LONG nQty, [in] LONG nSimulate); |
| 參數 | sMarketNo | 報價有異動的商品市場別。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | nPtr | 表示資料的位址(Key) |
|  | nDate | 交易日期。(YYYYMMDD) |
|  | nTimehms | 時間1。(時：分：秒) |
|  | nTimemillismicros | 時間2。(‘毫秒"微秒) |
|  | nBid | 買價。 |
|  | nAsk | 賣價。 |
|  | nClose | 成交價。 |
|  | nQty | 成交量。 |
|  | nSimulate | 0:一般揭示 1:試算揭示 |
| 備註 | nTimehms提供單位：時分秒hh:mm:ss。 / nTimemillismicros提供單位：毫秒微秒’millissecond’’micorsecond，預設提供毫秒微秒，開發人員自行決定是否顯示。(目前solace只提供證券商品) / 未進行揭示處理，開發人員需自行接手處理(判斷收的tick 為一般或試算揭示)。 / T盤切換T+1盤，不保留前一盤資料。開發者需自行清除前一盤資料。 / 避免在SKQuoteLib_OnNotifyTicksLONG通知事件裡進行SKQuoteLib_GetTickLONG()、SKQuoteLib_GetStockByIndexLONG(只可取得基本資料) / *依國內或海外市場，可能需搭配MarketNo / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-n OnNotifyBest5LONG
| (LONG index)當有索取的個股五檔價格有所異動，即透過向此註冊的事件進行處理。此函式會回傳所異動的個股五檔價格。 |
|---|
| 事件宣告 | void  OnNotifyBest5LONG([in] SHORT sMarketNo, [in] LONG nStockidx, [in] LONG nBestBid1, [in] LONG nBestBidQty1, [in] LONG nBestBid2, [in] LONG nBestBidQty2, [in] LONG nBestBid3, [in] LONG nBestBidQty3, [in] LONG nBestBid4, [in] LONG nBestBidQty4, [in] LONG nBestBid5, [in] LONG nBestBidQty5, [in] LONG nExtendBid, [in] LONG nExtendBidQty, [in] LONG nBestAsk1, [in] LONG nBestAskQty1 , [in] LONG nBestAsk2, [in] LONG nBestAskQty2, [in] LONG nBestAsk3, [in] LONG nBestAskQty3, [in] LONG nBestAsk4, [in] LONG nBestAskQty4, [in] LONG nBestAsk5, [in] LONG nBestAskQty5, [in] LONG nExtendAsk, [in] LONG nExtendAskQty, [in] LONG nSimulate); |
| 參數 | sMarketNo | 市埸代碼。 |
|  | nStockidx | 系統所編的索引代碼。 |
|  | nBestBid1 | 第一檔買價。 |
|  | nBestBidQty1 | 第一檔買量。 |
|  | nBestBid2 | 第二檔買價。 |
|  | nBestBidQty2 | 第二檔買量。 |
|  | nBestBid3 | 第三檔買價。 |
|  | nBestBidQty3 | 第三檔買量。 |
|  | nBestBid4 | 第四檔買價。 |
|  | nBestBidQty4 | 第四檔買量。 |
|  | nBestBid5 | 第五檔買價。 |
|  | nBestBidQty5 | 第五檔買量。 |
|  | nExtendBid | 衍生一檔買價。 |
|  | nExtendBidQty | 衍生一檔買量。 |
|  | nBestAsk1 | 第一檔賣價。 |
|  | nBestAskQty1 | 第一檔賣量。 |
|  | nBestAsk2 | 第二檔賣價。 |
|  | nBestAskQty2 | 第二檔賣量。 |
|  | nBestAsk3 | 第三檔賣價。 |
|  | nBestAskQty3 | 第三檔賣量。 |
|  | nBestAsk4 | 第四檔賣價。 |
|  | nBestAskQty4 | 第四檔賣量。 |
|  | nBestAsk5 | 第五檔賣價。 |
|  | nBestAskQty5 | 第五檔賣量。 |
|  | nExtendAsk | 衍生一檔賣價。 |
|  | nExtendAskQty | 衍生一檔賣量。 |
|  | nSimulate | 0:一般揭示 1:試算揭示 |
| 相關函式 | SKQuote_RequestTicks 相關通知事件。 |
| 備註 | 未進行揭示處理，開發人員需自行接手處理(判斷接收的五檔報價為一般或試算揭示)。 / ＊盤中零股: 因盤中仍會進入試撮，請留意是否收取『試撮資料』，即試算揭示。 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-o OnNotifyBoolTunelLONG
| (LONG index)事件回傳技術分析－布林通道。（日線－完整） |
|---|
| 宣告 | void OnNotifyBoolTunelLONG([in] SHORT sMarketNo, [in] LONG nStockidx, [in] BSTR bstrAVG, [in] BSTR bstrUBT, [in] BSTR bstrLBT); |
| 參數 | sMarketNo | 市場別代號 |
|  | nStockidx | 系統所編的索引代碼。 |
|  | bstrAVG | 均線 |
|  | bstrUBT | 通道上端 |
|  | bstrLBT | 通道下端 |
| 備註 | 目前只提供證券市場(不含盤中零股) / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-p OnNotifyMACDLONG
| (LONG index)事件回傳證券市場－技術分析平滑異同平均線MACD數值。（日線－完整） |
|---|
| 宣告 | void OnNotifyMACDLONG([in] SHORT sMarketNo, [in] LONG nStockidx, [in] BSTR bstrMACD, [in] BSTR bstrDIF, [in] BSTR bstrOSC); |
| 參數 | sMarketNo | 市場別代號 |
|  | nStockidx | 系統所編的索引代碼。 |
|  | bstrMACD | MACD平滑異同平均線 |
|  | bstrDIF | DIF |
|  | bstrOSC | DIF-MACD |
| 備註 | 目前只提供證券市場(不含盤中零股) / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-q OnNotifyFutureTradeInfoLONG
| (LONG index)事件回傳接收期貨商品的交易資訊。 |
|---|
| 宣告 | void OnNotifyFutureTradeInfoLONG([in]BSTR bstrStockNo, [in] SHORT sMarketNo, [in] LONG nStockidx, [in] LONG nBuyTotalCount,  [in] LONG nSellTotalCount, [in] LONG nBuyTotalQty, [in] LONG nSellTotalQty, [in] LONG nBuyDealTotalCount, [in] LONG nSellDealTotalCount / ); |
| 參數 | bstrStockNo | 商品代號 |
|  | sMarketNo | 市場別代號 |
|  | nStockidx | 系統所編的索引代碼。 |
|  | nBuyTotalCount | 總委託買進筆數 |
|  | nSellTotalCount | 總委託賣出筆數 |
|  | nBuyTotalQty | 總委託買進口數 |
|  | nSellTotalQty | 總委託賣出口數 |
|  | nBuyDealTotalCount | 總成交買進筆數 |
|  | nSellDealTotalCount | 總成交賣出筆數 |
| 備註 | nStockidx系統所編的索引代碼，透過MarketNo與 Stockidx ，可以使用 SKQuoteLib_GetStockByIndexLONG取得商品基本資訊 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-r OnNotifyOddLotSpreadDeal
| 事件回傳證券市場－整零價差即時行情 |
|---|
| 宣告 | void OnNotifyOddLotSpreadDeal([in] SHORT sMarketNo, [in] BSTR bstrStockNo, [in] LONG nDealPrice, [in] LONG nDigit); |
| 參數 | sMarketNo | 市場別代號 |
|  | bstrStockNo | 商品代碼。 |
|  | nDealPrice | 整零成交價差(負數則含-負號) |
|  | nDigit | 小數位數 |
| 備註 | 目前只提供證券市場；實際價差，需再透過小數位數自行計算。 / *公式: / 整零價差=整股成交價-零股成交價。 / 若整股當日無成交價，以昨收價計算。 / 若零股當日無成交價，不計算整零價差。 |

V2.13.46 修正文件宣告內容敘述 (移除[in] CHAR cSignDeal)
4-4-s OnNotifyBest5Emerging
| (LONG index)當有索取的個股五檔價格有所異動，即透過向此註冊的事件進行處理。此函式會回傳所異動的個股五檔價格。 / ※此事件索取的興櫃商品五檔單位為”股” |
|---|
| 事件宣告 | void  OnNotifyBest5Emerging([in] SHORT sMarketNo, [in] LONG nStockidx, [in] LONG nBestBid1, [in] LONG nBestBidQty1, [in] LONG nBestBid2, [in] LONG nBestBidQty2, [in] LONG nBestBid3, [in] LONG nBestBidQty3, [in] LONG nBestBid4, [in] LONG nBestBidQty4, [in] LONG nBestBid5, [in] LONG nBestBidQty5, [in] LONG nExtendBid, [in] LONG nExtendBidQty, [in] LONG nBestAsk1, [in] LONG nBestAskQty1 , [in] LONG nBestAsk2, [in] LONG nBestAskQty2, [in] LONG nBestAsk3, [in] LONG nBestAskQty3, [in] LONG nBestAsk4, [in] LONG nBestAskQty4, [in] LONG nBestAsk5, [in] LONG nBestAskQty5, [in] LONG nExtendAsk, [in] LONG nExtendAskQty, [in] LONG nSimulate); |
| 參數 | sMarketNo | 市埸代碼。 |
|  | nStockidx | 系統所編的索引代碼。 |
|  | nBestBid1 | 第一檔買價。 |
|  | nBestBidQty1 | 第一檔買量。 |
|  | nBestBid2 | 第二檔買價。 |
|  | nBestBidQty2 | 第二檔買量。 |
|  | nBestBid3 | 第三檔買價。 |
|  | nBestBidQty3 | 第三檔買量。 |
|  | nBestBid4 | 第四檔買價。 |
|  | nBestBidQty4 | 第四檔買量。 |
|  | nBestBid5 | 第五檔買價。 |
|  | nBestBidQty5 | 第五檔買量。 |
|  | nExtendBid | 衍生一檔買價。 |
|  | nExtendBidQty | 衍生一檔買量。 |
|  | nBestAsk1 | 第一檔賣價。 |
|  | nBestAskQty1 | 第一檔賣量。 |
|  | nBestAsk2 | 第二檔賣價。 |
|  | nBestAskQty2 | 第二檔賣量。 |
|  | nBestAsk3 | 第三檔賣價。 |
|  | nBestAskQty3 | 第三檔賣量。 |
|  | nBestAsk4 | 第四檔賣價。 |
|  | nBestAskQty4 | 第四檔賣量。 |
|  | nBestAsk5 | 第五檔賣價。 |
|  | nBestAskQty5 | 第五檔賣量。 |
|  | nExtendAsk | 衍生一檔賣價。 |
|  | nExtendAskQty | 衍生一檔賣量。 |
|  | nSimulate | 0:一般揭示 1:試算揭示 |
| 相關函式 | SKQuote_RequestTicks 相關通知事件。 |
| 備註 | 未進行揭示處理，開發人員需自行接手處理(判斷接收的五檔報價為一般或試算揭示)。 / ＊盤中零股: 因盤中仍會進入試撮，請留意是否收取『試撮資料』，即試算揭示。 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 |

4-4-t OnKLineComplete
| 收完歷史KLine，等收到此事件通知後表示回補完成。 |
|---|
| 宣告 | void OnKLineComplete ([in] BSTR bstrEndString); |
| 參數 | bstrEndString | 結尾字串 “##” |
| 備註 | 當全部資料已經全部回傳完畢，將回傳一筆以「##」開頭的內容，表示查詢結束。 |

4-4-u OnNotifyLiveKLineData
| 事件回傳即時技術分析資訊(即時分K)。 |
|---|
| 宣告 | void OnNotifyLiveKLineData([in] LONG nType, [in] BSTR bstrStockNo, [in] LONG nDate, [in] LONG nTimehm , [in] LONG nOpen, [in] LONG nHigh, [in] LONG nLow , [in] LONG nClose, [in] LONG nQty); |
| 參數 | nType | 0:資料重置，需要捨棄目前已收到資料 ，本筆資料價格都給 0 (清盤通知)1:即時分K(包含回補當日分K)2:該分鐘每筆Tick |
|  | bstrStockNo | 商品代號 |
|  | nDate | 日期YYYYMMDD |
|  | nTimehm | 成交時間HHmm (Ex: 904，代表09點04分) |
|  | nOpen | 開盤價 |
|  | nHigh | 最高價 |
|  | nLow | 最低價 |
|  | nClose | 目前價 |
|  | nQty | 目前量 |
| 備註 | ＊須使用SKQuoteLib_EnterMonitorLONG登入，該事件才會被觸發。 / *請注意價格為原始價格，需自行除以小數位數還原價格， / 例如：收到群益證為3815，需自行處理成38.15 / ；收到台指期為4557900，需自行處理成45579.00 / nType = 1，會先收到當日回補分K，並且訂閱即時分K(1分鐘給一次) / nType = 2，僅訂閱即時分K(該分鐘每筆Tick) / 不支援盤中零股。 / 證券:該查詢以整股為主計算，不包含「鉅額交易、盤中零股、盤後零股」 / *若您未開立證券帳戶，無法訂閱或取得證券上市櫃、興櫃市場商品及上市櫃指數資料(含即時分K)。 / 不支援價差商品。 / *若您未開立期貨帳戶，無法訂閱或取得期貨選擇權相關市場商品資料(含即時分K)。 |

4-7 SKOrderLib (proxy server下單)
函式
| 功能 | 函式名稱 | 備註 |
|---|---|---|
| 初始 | SKOrderLib_InitialProxyByID | 需先執行才可執行相關Proxy下單函式。 / (會先連線proxy server並做登入) |
| 證券下單 | SendStockProxyOrder |  |
| 證券刪改單 | SendStockProxyAlter |  |
| 期貨委託含倉位盤別 | SendFutureProxyOrderCLR | 須選倉別、盤別 |
| 期貨刪改單 | SendFutureProxyAlter |  |
| 選擇權委託 | SendOptionProxyOrder |  |
| 選擇權複式下單 | SendDuplexProxyOrder |  |
| 選擇權刪改單 | SendOptionProxyAlter |  |
| 海期委託 | SendOverseaFutureProxyOrder |  |
| 海選委託 | SendOverseaOptionProxyOrder |  |
| 海期刪改單 | SendOverseaFutureProxyAlter |  |
| 複委託下單 | SendForeignStockProxyOrder |  |
| 複委託刪單 | SendForeignStockProxyCancel |  |
| 海期價差委託 | SendOverseaFutureSpreadProxyOrder | V2.13.54後，不再提供此函式 |
| 海期價差委託New | SendOverseaFutureSpreadProxyOrder2 |  |
| 證券特殊刪改單 | SendStockProxyPreAlter |  |
| 斷線 | ProxyDisconnectByID | 主動斷線proxy server |
| 重新連線 | ProxyReconnectByID | 重新連線proxy server |

事件
| 功能 | 事件名稱 | 備註 |
|---|---|---|
| Proxy委託結果 | OnProxyOrder |  |
| 連線、登入狀態 | OnProxyStatus |  |

4-7-1　SKOrderLib_InitialProxyByID
| 以使用者的id，初使化proxy 下單連線 |
|---|
| 宣告 | Long SKOrderLib_InitialProxyByID ([in] BSTR bstrLogInID) |
| 參數 | bstrLogInID | 登入ID。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 | 若回傳值為0，需同時確認OnProxyStatus 收到5001，代表Proxy下單連線且登入成功 / 若回傳非0，請重新執行SKOrderLib_InitialProxyByID。 |

4-7-2　ProxyDisconnectByID
| 以使用者的id，主動斷線proxy server的連線 |
|---|
| 宣告 | Long ProxyDisconnectByID ([in] BSTR bstrLogInID) |
| 參數 | bstrLogInID | 登入ID。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。 |
| 備註 |  |

4-7-3　 ProxyReconnectByID
| 以使用者的id，重新連線之前主動斷線的proxy server連線，並做login |
|---|
| 宣告 | Long ProxyReconnectByID ([in] BSTR bstrLogInID) |
| 參數 | bstrLogInID | 登入ID。 |
| 回傳值 | 0表示連線成功，其餘非0數值都表示失敗。 |
| 備註 |  |

4-7-4　 SendStockProxyOrder
| 經由proxy server送出證券委託。 |
|---|
| 宣告 | long SendStockProxyOrder([in]BSTR bstrLogInID, [in]struct STOCKPROXYORDER* pSTOCKPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSTOCKPROXYORDER | SKCOM元件中的 STOCKPROXYORDER 物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-5　 SendStockProxyAlter
| 經由proxy server送出證券刪改單。 |
|---|
| 宣告 | long SendStockProxyAlter([in]BSTR bstrLogInID, [in]struct STOCKPROXYORDER* pSTOCKPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSTOCKPROXYORDER | SKCOM元件中的STOCKPROXYORDER 物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-6　SendFutureProxyOrderCLR
| 經由proxy server送出期貨委託，需設倉別與盤別。 |
|---|
| 宣告 | Long SendFutureProxyOrderCLR ([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-7　 SendFutureProxyAlter
| 經由proxy server送出期貨刪改單。 |
|---|
| 宣告 | Long SendFutureProxyAlter ([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-8　 SendOptionProxyOrder
| 經由proxy server送出選擇權委託 |
|---|
| 宣告 | Long SendOptionProxyOrder([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-9　 SendOptionProxyAlter
| 經由proxy server送出選擇權刪改單。 |
|---|
| 宣告 | Long SendOptionProxyAlter([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-10　SendDuplexProxyOrder
| 經由proxy server送出選擇權複式下單。 |
|---|
| 宣告 | Long SendDuplexProxyOrder ([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-11 SendOverseaFutureProxyOrder
| 經由proxy server送出海期下單。 |
|---|
| 宣告 | Long SendOverseaFutureProxyOrder([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-12 SendOverseaFutureSpreadProxyOrder
| 經由proxy server送出海期價差單下單。 |
|---|
| 宣告 | Long SendOverseaFutureSpreadProxyOrder ([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / V2.13.54後，不再提供此函示，請改使用SendOverseaFutureSpreadProxyOrder2，謝謝。 / 注意SendOverseaFutureSpreadProxyOrder2買賣別判斷有更動。請參考 2-10 海期價差單買賣別判斷更動說明。 |

4-7-13 SendOverseaFutureProxyAlter
| 經由proxy server送出海期選刪改單。 |
|---|
| 宣告 | Long SendOverseaFutureProxyAlter([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將刪單或改單條件填入該物件後，再帶入此欄位中。 / Proxy 改單功能物件說明 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-14 SendOverseaOptionProxyOrder
| 經由proxy server送出海選下單。 |
|---|
| 宣告 | Long SendOverseaOptionProxyOrder ([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-15 SendForeignStockProxyOrder
| 經由proxy server送出複委託下單。 |
|---|
| 宣告 | Long SendForeignStockProxyOrder([in] BSTR bstrLogInID,  [in] struct OSSTOCKPROXYORDER* pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pAsyncOrder | SKCOM元件中的OSSTOCKPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-16 SendForeignStockProxyCancel
| 經由proxy server送出複委託刪單。 |
|---|
| 宣告 | Long SendForeignStockProxyCancel([in] BSTR bstrLogInID,  [in] struct OSSTOCKPROXYORDER * pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pAsyncOrder | SKCOM元件中的OSSTOCKPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-12 SendOverseaFutureSpreadProxyOrder2
| 經由proxy server送出海期價差單下單。 / 使用此函式時，帶入0:買，如果是以近月買賣別為主之商品-買進賣遠；以遠月買賣別為主之商品-買遠賣近 |
|---|
| 宣告 | Long SendOverseaFutureSpreadProxyOrder2 ([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-17 SendStockProxyPreAlter
| 經由proxy server送出證券特殊刪改單(帶入序號)。 |
|---|
| 宣告 | long SendStockProxyPreAlter([in] BSTR bstrLogInID, [in]struct STOCKPROXYORDER* pSTOCKPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSTOCKPROXYORDER | SKCOM元件中的STOCKPROXYORDER 物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 非同步委託：0表示已完成處理，將 Request 送出中，結果請由 OnProxyOrder進行確認。 / 當錯誤代碼為4碼時，可參考6代碼定義表，所屬委託物件亦有欄位值說明供參。 / 其他錯誤則由「交易主機」回傳錯誤代碼及錯誤原因。若仍有疑問請洽詢您所屬營業員。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 / *此處委託成功，是指成功送至交易所，交易所回覆結果請由回報確認 |

4-7-a OnProxyOrder
| Proxy委託結果。 |
|---|
| 宣告 | void OnProxyOrder([in] LONG nStampID [in] LONG nCode, [in] BSTR bstrMessage); |
| 參數 | nStampID | StampID |
|  | nCode | Proxy收單結果代碼。 |
|  | bstrMessage | Proxy收單回傳訊息。 |
| 備註 | 送出委託後會回傳二筆通知 |

*v2.13.47異動：補回傳參數nStampID文字說明
*v2.13.48異動：新增證券下單回傳欄位
4-7-b OnProxyStatus
| 一個使用者id會與proxy server建一條連線，此事件回傳此條連線的連線狀態。 / 透過呼叫 SKOrderLib_Initialize 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnProxyStatus ([in] BSTR bstrUserId, [in]LONG nCode); |
| 參數 | bstrUserId | 回傳內容：使用者id |
|  | nCode | 回傳內容： / 請參考代碼定義表5001~5006、5011 |
| 備註 |  |

5、Struct 結構物件
5-1 OVERSEAFUTUREORDER( 海外期權下單物件 )
海期委託
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrYearMonth2;//遠月商品年月( YYYYMM) 6碼 {價差下單使用}
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrTrigger;//觸發價。
BSTRbstrTriggerNumerator;//觸發價分子。
SHORTsBuySell;//0:買進 1:賣出
{價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
SHORTsNewClose;//新平倉，0:新倉  {目前海期僅新倉可選}
SHORTsDayTrade;//當沖0:否 1:是；{海期價差單不提供當沖}　　　　　　　　　　　　　　//可當沖商品請參考交易所規定。
SHORTsTradeType;//0:ROD    1:FOK      2:IOC
//{限價單LMT可選ROD/IOC/FOK，市價單依交易所實際提供為主，其餘單別則固定ROD}
SHORTsSpecialTradeType;
//0:LMT 限價單 1:MKT市價單  2:STL停損限價  3.STP 停損市價
LONGnQty;//交易口數。
};
海期委託 SGX DMA 專線
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrYearMonth2;//遠月商品年月( YYYYMM) 6碼 {價差下單使用}
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrTrigger;//觸發價。
BSTRbstrTriggerNumerator;//觸發價分子。
SHORTsBuySell;//0:買進 1:賣出
{價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
SHORTsNewClose;//新平倉，0:新倉  {目前海期僅新倉可選}
SHORTsDayTrade;//當沖0:否 1:是；{海期價差單不提供當沖}　　　　　　　　　　　　　　//可當沖商品請參考交易所規定。
SHORTsTradeType;//0:ROD     1:IOC   2:FOK
//{限價單LMT可選ROD/IOC/FOK，其餘單別固定ROD}
SHORTsSpecialTradeType;//0:LMT限價單; 1:MKT市價單; 2:STL停損限價; 3.STP停損市價
LONGnQty;//交易口數。
};
海選委託
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrYearMonth2;//遠月商品年月( YYYYMM) 6碼 {價差下單使用}
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrTrigger;//觸發價。
BSTRbstrTriggerNumerator;//觸發價分子。
SHORTsBuySell;//0:買進 1:賣出{價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
SHORTsNewClose;//新平倉，0:新倉 1:平倉 {目前海選可使用新、平倉}
SHORTsDayTrade;//當沖0:否 1:是　　　　　　　　　　　　　　//可當沖商品請參考交易所規定。
SHORTsTradeType;//0:ROD  {目前海選均固定ROD}
SHORTsSpecialTradeType;//0:LMT 限價單; 1:MKT市價單; 2:STL停損限價; 3.STP停損市價
BSTRbstrStrikePrice;//履約價。{選擇權使用}
SHORTsCallPut;//0:CALL  1:PUT {選擇權使用}
LONGnQty;//交易口數。
};
OVERSEAFUTUREORDER(海期智慧單下單物件)
海期智慧單OCO
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司四碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼 (EX: CME)
BSTRbstrStockNo;//商品代碼 (EX: ES)
BSTRbstrYearMonth;//商品年月(YYYYMM, EX: 202206)
BSTRbstrTrigger;//第一隻腳觸發價(大於)
BSTRbstrTriggerNumerator;//第一隻腳觸發價格分子
BSTRbstrTriggerDenominator;//第一隻腳觸發價格分母
SHORTsBuySell;//第一隻腳買賣別(0:買進, 1:賣出)
LONGnOrderPriceType;//第一隻腳價格委託方式(1:市價,  2:限價)
BSTRbstrOrder;//第一隻腳委託價格
BSTRbstrOrderNumerator;//第一隻腳委託價格分子
BSTRbstrOrderDenominator;//第一隻腳委託價格分母
LONGsTradeType;//第一隻腳委託時效(0:ROD, 3:IOC, 4:FOK)
BSTRbstrTrigger2;//第二隻腳觸發價(小於)
BSTRbstrTriggerNumerator2;//第二隻腳觸發價格分子
BSTRbstrTriggerDenominator2;//第二隻腳觸發價格分母
LONGnBuySell2;//第二隻腳買賣別(0:買進, 1:賣出)
BSTRbstrOrder2;//第二隻腳委託價格
BSTRbstrOrderNumerator2;//第二隻腳委託價格分子
BSTRbstrOrderDenominator2;//第二隻腳委託價格分母
LONGnQty;//委託口數
SHORTsDayTrade;//當沖(0:否, 1:是)
LONGnReserved; //預約單(0:否, 1:是)
LONGnTimeFlag; //預約盤別(1:T盤, 2:T+1盤, 3:全盤(T盤、T+1盤))
LONGnLongActionFlag;//是否為長效單(0:否, 1:是)
BSTRbstrLongEndDate//長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
LONGnLAType//觸發結束條件(1:效期內觸發即失效, 3:短期內完全成交即失效)
)
*v2.13.47修正nTimeFlag參數1:T盤, 2:T+1盤
海期智慧單AB
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo2;//A商品商品代號
LONGnMarketNo;//市場編號(1:國內證, 2:國內期, 3:國外證, 4:國外期)
BSTRbstrExchangeNo;//A商品交易所代碼 (EX: CME)
SHORTsSpecialTradeType;   //1:上市, 2:上櫃(不開放權證、興櫃商品)，非證券商品請填0
BSTR    bstrOrder2;//A商品市價
LONG    nTriggerDirection;  //觸價方向(1:GTE大於等於,2:LTE小於等於)
BSTRbstrTrigger;//觸發價
BSTRbstrTriggerNumerator;//觸發價分子(若無請填0)
BSTRbstrTriggerDenominator;//觸發價分母(若無請填0)
BSTRbstrStockNo;//B商品商品代碼 (EX: ES)
BSTRbstrExchangeNo2;//B商品交易所代碼 (EX: CME)
SHORT   sCallPut;             //是否為選擇權(0:否, 1:Call, 2:Put)
BSTRbstrStrikePrice;//履約價格(非選擇權商品請填0)
LONG   nSpreadFlag;       //是否委託價差商品(0:否, 1:是)
BSTRbstrYearMonth;//商品契約月份(YYYYMM共6碼, EX: 202206)
BSTRbstrYearMonth2;//商品契約月份2(YYYYMM共6碼, EX: 202206)，非價差商品請填0
SHORTsDayTrade;//是否為當沖(0:否, 1:是)
SHORTsNewClose;//新平倉(0:新倉, 1:平倉) 選擇權僅新、平倉，期貨商品僅新倉
LONGsTradeType;//委託時效(0:ROC, 3:IOC, 4:FOK, 委託海期範圍市價、海選市限價僅有ROD)
SHORTsBuySell;//買賣別(0:買進, 1:賣出)
LONGnBuySell2;//買賣別2(0:買進, 1:賣出) ，非價差商品請填0
LONGnQty;//委託口數
LONGnOrderPriceType;//委託價類別(1:市價,2:限價)
BSTRbstrOrder; //委託價
BSTRbstrOrderNumerator;//委託價分子(若無請填0)
BSTRbstrOrderDenominator;//委託價分母(若無請填0)
LONG    nTimeFlag;//是否為預約單(0:否, 1:是)A商品為國內期選市場時可選擇預約單
)
Proxy Server海期
Proxy Server海期下單
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrTrigger;//觸發價。
BSTRbstrTriggerNumerator;//觸發價分子。
SHORTsBuySell;//0:買進 1:賣出
{價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
SHORTsNewClose;//新平倉，0:新倉  {目前海期僅新倉可選}
SHORTsDayTrade;//當沖0:否 1:是；{海期價差單不提供當沖}　　　　　　　　　　　　　　//可當沖商品請參考交易所規定。
SHORTsTradeType;//0:ROD     1:IOC   2:FOK
//{限價單LMT可選ROD/IOC/FOK，其餘單別固定ROD}
SHORTsSpecialTradeType;//0:LMT限價單; 1:MKT市價單; 2:STL停損限價; 3.STP
停損市價
LONGnQty;//交易口數。
};
Proxy Server海期價差下單
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrYearMonth2;//遠月商品年月( YYYYMM) 6碼 {價差下單使用}
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrTrigger;//觸發價。
BSTRbstrTriggerNumerator;//觸發價分子。
SHORTsBuySell;//0:買進 1:賣出
{價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
SHORTsNewClose;//新平倉，0:新倉  {目前海期僅新倉可選}
SHORTsDayTrade;//當沖0:否 1:是；{海期價差單不提供當沖}　　　　　　　　　　　　　　//可當沖商品請參考交易所規定。
SHORTsTradeType;//0:ROD     1:IOC   2:FOK
//{限價單LMT可選ROD/IOC/FOK，其餘單別固定ROD}
SHORTsSpecialTradeType;//0:LMT限價單; 1:MKT市價單; 2:STL停損限價; 3.STP停損市價
LONGnQty;//交易口數。
};
Proxy Server海選下單
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrOrderDenominator;//委託價分母。
BSTRbstrTrigger;//觸發價。
BSTRbstrTriggerNumerator;//觸發價分子。
SHORTsBuySell;//0:買進 1:賣出{價差商品，需留意是否為特殊商品－近遠月前的「+、-」符號}
SHORTsNewClose;//新平倉，0:新倉 1:平倉 {目前海選可使用新、平倉}
SHORTsDayTrade;//當沖0:否 1:是　　　　　　　　　　　　　　//可當沖商品請參考交易所規定。
SHORTsTradeType;//0:ROD  {目前海選均固定ROD}
SHORTsSpecialTradeType;//0:LMT 限價單; 1:MKT市價單; 2:STL停損限價; 3.STP停損市價
BSTRbstrStrikePrice;//履約價。{選擇權使用}
SHORTsCallPut;//0:CALL  1:PUT {選擇權使用}
LONGnQty;//交易口數。
};
Proxy Server海期選刪改單
struct OVERSEAFUTUREORDER
{
BSTRbstrFullAccount;//海期帳號，分公司代碼＋帳號7碼
BSTRbstrExchangeNo;//交易所代碼。
BSTRbstrStockNo;//海外期權代號。
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼
BSTRbstrYearMonth2;//遠月商品年月( YYYYMM) 6碼 {價差刪改單使用}
BSTRbstrOrder;//委託價。
BSTRbstrOrderNumerator;//委託價分子。
BSTRbstrOrderDenominator;//委託價分母。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 {目前海選可使用新、平倉}
SHORTsTradeType;//0:ROD  {目前海選均固定ROD}
SHORTsSpecialTradeType;//0:LMT限價單; 1:MKT市價單; 2:STL停損限價; 3.STP停損市價
LONGnQty;//交易口數。
BSTRbstrBookNo;//書號。
BSTRbstrSeqNo;//原始13碼序號。
LONGnSpreadFlag; // 0 :OF海期 ; 1: OF-spread 海期價差;  2: OO 海選
LONGnAlterType;         //0: Cancel 刪單;1: Decrease 減量; 2: Correct 改價
BSTRbstrStrikePrice;//選擇權履約價 {海選使用}
SHORTsCallPut; //0:CALL ; 1:PUT {海選使用}
};
5-2 FUTUREORDER ( 期權下單物件 )
一般期選委託▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託期權代號
SHORTsTradeType;//0:ROD  1:IOC  2:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動{新期貨、選擇權使用}
BSTRbstrPrice;//委託價格(IOC and FOK，可用「M」表示市價，「P」表示範圍市價)
LONGnQty;//交易口數
SHORTsReserved;//{期貨委託SendFutureOrderCLR適用}盤別，0:盤中(T盤及T+1盤)；1:T盤預約
BSTRbstrCIDTandem;      //委託期權代號2(例如FITX、FIMTX)
BSTRbstrSettlementMonth;//第一腳年月(yyyymm)
BSTRbstrSettlementMonth2; //第二腳年月(yyyymm)
};
| 備註 | 如果要下價差單，bstrStockNo請帶入：”近月/遠月”， sBuySell買賣別為近月 / 範例：買TX12/01，bstrStockNo = “TX12/01”、 sBuySell = 1 (賣出) / 下單方法: / bstrStockNo帶入TX03 (注意：如果想委託今年TX03但已過期，則會自動委託隔年TX03。) / 2、bstrCIDTandem帶入FITX，bstrSettlementMonth帶入202503 |
|---|---|

全拆組單；全拆組平單委託▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
LONGnFunType;//功能 1：全拆組 2：全拆組平
};
5-2-1 期、選智慧單▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託期權商品代號
SHORTsTradeType;//0:ROD 3:IOC 4:FOK
//sTradeType為ROD時，nOrderPriceType僅可指定限價
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動
BSTRbstrPrice;//委託價格，(指定限價時，需填此欄)、[OCO]停利委託價。
//請設nOrderPriceType代表範圍市價，不可用特殊價代碼「P」代表範圍市價
//{移動停損不須填委託價格}
BSTRbstrPrice2;    //[OCO]停損委託價。
LONGnQty;//交易口數
BSTRbstrTrigger;//觸發價，觸發基準價、[OCO]停利觸發價。
//{期貨停損、移動停損、選擇權停損、MIT、OCO下單使用：不可0、不可給特殊價代碼P}
BSTRbstrTrigger2;//[OCO]停損觸發價。
BSTRbstrMovingPoint;//{僅移動停損下單使用}移動點數。
SHORTsReserved;//盤別，0:盤中(T盤及T+1盤)；1:T盤預約{MIT 單不須填盤別}
BSTR  bstrDealPrice;
//成交價 {限MIT下單使用：不可0、不可給特殊價代碼, 需自行取得委託當下成交價}
BSTR bstrSettlementMonth; // 委託商品年月，YYYYMM共6碼(EX: 202206)
LONG  nOrderPriceType;      // 委託價類別  2: 限價; 3:範圍市價 （不支援市價）
//sTradeType為ROD時，nOrderPriceType僅可指定限價
LONG  nTriggerDirection;  //{限MIT下單使用} 觸發方向1:GTE, 2:LTE
};
註: 若委託期貨商品代號為TX00、 MTX00等近月商品，則可忽略bstrSettlementMonth商品年月
期貨智慧單STP ▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託期權商品代號
SHORTsTradeType;//0:ROD 3:IOC 4:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動
BSTRbstrPrice;//委託價格，(指定限價時，需填此欄)
//請設nOrderPriceType代表特殊價格「P」範圍市價
LONGnQty;//交易口數
BSTRbstrTrigger;//觸發價，觸發基準價。
//{期貨停損、選擇權停損、不可0、不可給特殊價代碼}
SHORTsReserved;//盤別，0:盤中(T盤及T+1盤)；1:T盤預約
BSTR  bstrSettlementMonth; // 委託商品年月，YYYYMM共6碼(EX: 202206)
LONG  nOrderPriceType; // 2: 限價; 3:範圍市價 （不支援市價）
LONGnLongActionFlag;//是否為長效單(0:否, 1:是)
BSTRbstrLongEndDate;//長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
LONGnLAType;//觸發結束條件(1:效期內觸發即失效, 3:短期內完全成交即失效)
};
比較表
| 異動後V2.13.45 |
|---|
| 期貨STP下單新增長效單功能：是否為長效單(0:否, 1:是)、長效單結束日期(YYYYMMDD共8碼)、觸發結束條件 |

期貨智慧單MST ▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託期權商品代號
SHORTsTradeType;// 3:IOC ;  4:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動
LONGnQty;//交易口數
BSTRbstrTrigger;//觸價基準。
BSTRbstrMovingPoint;//移動點數。{僅移動停損下單使用}
SHORTsReserved;//盤別，0:盤中(T盤及T+1盤)；1:T盤預約
BSTR   bstrSettlementMonth; // 委託商品年月，YYYYMM共6碼(EX: 202206)
// bstrStockNo指定月份，需填商品契約年月
LONG  nOrderPriceType;    // 2: 限價; 3:範圍市價 （不支援市價）
};
期貨、選擇權智慧單MIT ▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託期權代號
SHORTsTradeType;//0:ROD  3:IOC  4:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動
LONGnQty;//交易口數
BSTRbstrTrigger;//觸發價。//{ MIT下單使用：不可0、不可給特殊價代碼}
BSTR  bstrDealPrice;      //成交價 {限MIT下單使用：不可0、不可給特殊價代碼}
BSTRbstrPrice;//委託價格(指定限價時，需填此欄)
BSTR   bstrSettlementMonth;   // 委託商品年月，YYYYMM共6碼(EX: 202206)
//若 bstrStockNo指定月份，需填商品契約年月
LONG  nOrderPriceType; // 委託價類別 2: 限價; 3:範圍市價
LONG  nTriggerDirection;  //觸發方向1:GTE(大於), 2:LTE(小於)
};
期貨智慧單OCO ▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託期權代號
BSTRbstrPrice;//第一腳委託價格
BSTRbstrTrigger;//第一腳觸發價，當市價大於觸發價1時觸發。
BSTRbstrPrice2;//第二腳委託價格
BSTRbstrTrigger2;//第二腳觸發價，當市價小於觸發價2時觸發。
SHORTsTradeType;//0:ROD 3:IOC  4:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動
LONGnQty;//交易口數
SHORTsBuySell2;//0:買進 1:賣出
SHORTsReserved;//盤別0:盤中; 1:預約
SHORTnTimeFlag;//預約單盤別，預設為盤中1:T盤 2: T+1盤 3:全盤(T盤、T+1盤皆有效)
BSTRbstrSettlementMonth;// 指定月份商品契約年月，共6碼EX:202212
// bstrStockNo指定月份，需填商品契約年月
LONGnOrderPriceType;// 第一腳及第二腳委託價格類型。2限價;3範圍市價    （不支援市價）
LONGnLongActionFlag;//是否為長效單(0:否, 1:是)
BSTRbstrLongEndDate;//長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
LONGnLAType;//觸發結束條件(1:效期內觸發即失效, 3:短期內完全成交即失效)};
比較表
| 異動後V2.13.45 |
|---|
| 期貨OCO下單新增長效單功能：是否為長效單(0:否, 1:是)、長效單結束日期(YYYYMMDD共8碼)、觸發結束條件 |

期貨智慧單AB ▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo2;//A商品 商品代號
LONGnMarketNo;//市場編號(1:國內證, 2:國內期, 3:國外證, 4:國外期) ，請輸入A商品市場別
BSTRbstrCIDTandem;//交易所代碼(EX: TSE、TAIFEX、CME)
LONG    nTimeFlag;//1:上市, 2:上櫃(不開放權證、興櫃商品)，非證券商品請填0
BSTR    bstrDealPrice;//成交價
LONG    nTriggerDirection;  //觸價方向(1:GTE大於等於,2:LTE小於等於)
BSTRbstrTrigger//觸發價
BSTRbstrStockNo;//B商品 委託商品代號
LONG   nCallPut;             //是否為選擇權(0:否, 1:Call, 2:Put)
BSTR bstrStrikePrice//履約價(非選擇權商品請填0)
LONG   nFlag;             //是否委託價差商品(0:否, 1:是)
BSTRbstrSettlementMonth;//商品契約月份(YYYYMM共6碼, EX: 202206)
BSTRbstrSettlementMonth2;//商品契約月份2(YYYYMM共6碼, EX: 202206)，非價差商品請填0
SHORTsDayTrade;//是否為當沖(0:否, 1:是)
SHORTsNewClose;//新平倉(0:新倉, 1:平倉, 2:自動)
LONGsTradeType;//委託時效(0:ROC, 3:IOC, 4:FOK, 委託範圍市價無ROD)
SHORTsBuySell;//買賣別(0:買進, 1:賣出)
SHORTsBuySell2;//買賣別2(0:買進, 1:賣出)，非價差商品請填0
LONGnQty;//委託口數
LONGnOrderPriceType;//委託價類別(2:限價, 3:範圍市價)
BSTRbstrPrice;//委託價格
SHORTsReserved;//是否為預約單(0:否, 1:是)
};
5-2-2 選擇權複式單▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託選擇權代號1
BSTRbstrStockNo2;//委託選擇權代號2{複式單}
SHORTsTradeType;// 1:IOC  2:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsBuySell2;//0:買進 1:賣出{複式單}
SHORTsNewClose;//新平倉，0:新倉 1:平倉
BSTRbstrPrice;//委託價格
LONGnQty;//交易口數
};
5-2-3 組合部位、複式單拆解、雙邊部位了結▼
組合部位▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託選擇權代號1{組合部位}
BSTRbstrStockNo2;//委託選擇權代號2{組合部位}
SHORTsBuySell;//0:買進 1:賣出{組合部位}
SHORTsBuySell2;//0:買進 1:賣出{組合部位}
LONGnQty;//交易口數{組合部位}
};
複式單拆解(單邊或雙邊)▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//複式單商品代碼{複式單拆解}
SHORTsBuySell;//0:買進 1:賣出{複式單拆解}
LONGnQty;//交易口數{複式單拆解}
};
複式單拆解(單邊或雙邊)
| 異動前 | 欄位 | 異動後 |
|---|---|---|
| 複式單拆解(單邊或雙邊) / struct FUTUREORDER / { / BSTRbstrFullAccount; / BSTRbstrStockNo; / BSTRbstrStockNo2; / SHORTsBuySell; / LONGnQty; / }; | 刪除 / 雙邊 / bstrStockNo2; / //委託選擇權代號2 | 複式單拆解(單邊) / struct FUTUREORDER / { / BSTRbstrFullAccount; / BSTRbstrStockNo; / SHORTsBuySell; / LONGnQty; / }; |

雙邊部位了結▼
struct FUTUREORDER
{
BSTRbstrFullAccount;//期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託選擇權代號1{雙邊部位了結}
LONGnQty;//交易口數{雙邊部位了結}
};
5-2-4 Proxy Server 期選
Proxy Server期貨下單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;   //期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託商品代號 ex:FITX
BSTR   bstrSettleYM;     // 指定月份商品契約年月，共6碼EX:202212
LONG  nBuySell;         //0:買進 1:賣出
LONGnPriceFlag;//0:市價  1:限價 2:範圍市價
LONG  nDayTrade;  //當沖0:否 1:是，可當沖商品請參考交易所規定。
BSTR   bstrOrderType;    //0:新倉 1:平倉 2:自動
LONG   nReserved;  //0:盤中單 1:預約單
LONGnQty;            //交易口數{組合部位}
BSTRbstrPrice;      //委託價格
LONGnTradeType;    // 0:ROD 1:IOC 2:FOK
};
| 備註 |
|---|
| 欲下單近月商品，EX: TX00，請於bstrStockNo輸入FITX、bstrSettleYM輸入(近月月份)202402 |

Proxy Server選擇權下單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;   //期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託商品代號 ex:TXO
BSTRbstrPrice;       //委託價格
BSTR   bstrSettleYM;   // 指定月份商品契約年月，共6碼EX:202212
BSTR   bstrStrike;      //履約價1
BSTR   bstrOrderType;  //0:新倉 1:平倉 2:自動
LONG   nReserved;  //0:盤中單 1:預約單
LONGnQty;            //交易口數{組合部位}
LONG  nCP;            //0:CALL  1:PUT
LONG  nBuySell;            //0:買進 1:賣出
LONGnPriceFlag;//0:市價  1:限價 2:範圍市價
LONGnTradeType;// 0:ROD 1:IOC 2:FOK
LONG  nDayTrade;  //當沖0:否 1:是，可當沖商品請參考交易所規定。
};
*下單範例：2024年3月第4週選擇權商品，請於bstrStockNo輸入TX4、bstrSettleYM輸入202403。2024年4月第1週選擇權商品，請於bstrStockNo輸入TX1、bstrSettleYM輸入202404。
Proxy Server選擇權複式單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;   //期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTRbstrStockNo;//委託商品代號 ex:TXO
BSTRbstrPrice; //委託價格
BSTR   bstrSettleYM;//指定月份商品契約年月，共6碼EX:202212
BSTR   bstrStrike;//履約價1
BSTR   bstrSettleYM2;//指定月份商品契約年月2，共6碼EX:202301
BSTR   bstrStrike2;//履約價2
BSTR   bstrOrderType;//0:新倉 1:平倉 2:自動
LONGnReserved;//0:盤中單 1:預約單
LONGnQty; //交易口數{組合部位}
LONGnCP; //買賣權1 ( 0:CALL  1:PUT )
LONGnBuySell;//買賣別1 ( 0:買進 1:賣出)
BSTRbstrStockNo2;//委託商品代號2
LONGnCP2; //買賣權2 ( 0:CALL  1:PUT )
LONGnBuySell2;//買賣別2 ( 0:買進 1:賣出)
LONGnPriceFlag;  // 0:市價  1:限價 2:範圍市價
LONGnTradeType// 1:IOC 2:FOK
LONG  nDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
};
Proxy Server期選刪改單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;    //期貨帳號，Broker id (例如: F020000)＋帳號7碼
BSTR   bstrOrderType;     //0:刪單 1:減量 2:改價
BSTRbstrPrice;         //委託價格
(改量:帶0，也可以帶""。改價:帶需要改的價。刪單:帶原始價，也可帶"")
LONG   nReserved;       //0:盤中單 1:預約單
LONGnQty;            //交易口數
(改量 : 帶要減的量。改價 : 帶0也可帶""。刪單 : 帶原始量)
LONGnTradeType;    // 0:ROD 1:IOC 2:FOK
BSTR   bstrBookNo;      //委託書號
BSTR   bstrSeqNo;       //委託序號
};
5-3 FOREIGNORDER（複委託下單物件）
struct FOREIGNORDER
{
BSTRbstrFullAccount;//複委託帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTRbstrExchangeNo;//交易所代碼，US：美股， HK：港股，JP：日股， SP：新加坡，SG：新(幣)加坡股，SA: 滬股，HA: 深股
BSTRbstrPrice;//委託價格
BSTRbstrCurrency1;//扣款幣別，幣別順序1
BSTRbstrCurrency2;//扣款幣別，幣別順序2
BSTRbstrCurrency3;//扣款幣別，幣別順序3
(幣別可輸入 : HKD、NTD、USD、JPY、SGD、EUR、AUD、CNY、GBP)
BSTR    bstrSeqNo;//[刪單時需填入]
BSTR    bstrBookNo;      //[刪單時需填入]
LONGnAccountType;//專戶別種類，1:外幣專戶 2:台幣專戶
LONGnQty;//委託量
LONGnOrderType;//1:買 2:賣 4:刪單
LONGnTradeType;//庫存別 ，賣出美股時必填
//1:[美股]一般/定股(CITI) 2:定額(VIEWTRADE)
};
比較表
| 異動後V2.13.46 |
|---|
| 下單市場新增：HK：港股，JP：日股， SP：新加坡，SG：新(幣)坡股，SA: 滬股，HA: 深股 / 下單幣別新增：CNY、GBP |

5-3-2 複委託刪單物件
struct FOREIGNORDER
{
BSTRbstrFullAccount;//複委託帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTRbstrExchangeNo;//交易所代碼，美股：US， HK：港股，JP：日股， SP：新加坡，SG：新(幣)加坡股，SA: 滬股，HA: 深股
BSTR    bstrSeqNo;//[刪單時需填入]
BSTR    bstrBookNo;      //[刪單時需填入]
LONGnOrderType;// 4:刪單
};
5-3-3 Proxy Server複委託物件
Proxy Server複委託下單
struct OSSTOCKPROXYORDER
{
BSTRbstrFullAccount;//複委託帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTRbstrExchangeNo;//交易所代碼，US：美股， HK：港股，JP：日股， SP：新加坡，SG：新(幣)坡股，SA: 滬股，HA: 深股
BSTRbstrPrice;//委託價格
BSTRbstrCurrency1;//扣款幣別，幣別順序1
BSTRbstrCurrency2;//扣款幣別，幣別順序2
BSTRbstrCurrency3;//扣款幣別，幣別順序3
(幣別可輸入 : HKD、NTD、USD、JPY、SGD、EUR、AUD、CNY、GBP)
BSTRbstrProxyQty;//委託量 (股數。只有賣出美股且庫存別為定額(VIEWTRADE)時，股數才能有小數位數，其餘都必須為整數)
LONGnAccountType;//專戶別種類，1:外幣專戶 2:台幣專戶
LONGnOrderType;//1:買 2:賣
LONGnTradeType;//庫存別 ，賣出美股時必填
//1:[美股]一般/定股(CITI) 2:定額(VIEWTRADE) 3:其他股市(一般)
非賣出美股可輸入0 :表示填空值
};
Proxy Server複委託刪單
struct OSSTOCKPROXYORDER
{
BSTRbstrFullAccount;//複委託帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTRbstrExchangeNo;//交易所代碼，美股：US
BSTR    bstrSeqNo;//[刪單時需填入]
BSTR    bstrBookNo;      //[刪單時需填入]
LONGnOrderType;// 4:刪單
};
5-4 STOCKORDER ( 證券下單物件 )
struct STOCKORDER
{
BSTRbstrFullAccount;//證券帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
SHORTsPrime;//0:上市上櫃1:興櫃
SHORTsPeriod;//0:盤中 1:盤後 2:零股
SHORTsFlag;//0:現股 1:融資 2:融券 3:無券
SHORTsBuySell;//0:買進 1:賣出
BSTRbstrPrice;//委託價格，「M」表示參考價（昨收價）、「H」表示漲停價、「L」表示跌停價
LONGnQty;//整股交易為張數，如果是零股則為股數
LONGnTradeType;//[證券逐筆交易]0:ROD ; 1:IOC ; 2:FOK
LONGnSpecialTradeType;//[證券逐筆交易]1:市價; 2:限價
(市價單之委託價格Price請給0; 限價單之委託價格Price 不可為0)
};
5-4-2盤中零股
struct STOCKORDER
{
BSTRbstrFullAccount;//證券帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
SHORTsPeriod;// 4:盤中零股
SHORTsFlag;//0:現股
SHORTsBuySell;//0:買進 1:賣出
BSTRbstrPrice;//委託價格
LONGnQty;//零股數量為股數1~999股
};
5-4-3 Proxy Server 證券
Proxy Server 證券下單
struct STOCKPROXYORDER
{
BSTRbstrFullAccount;//證券帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTR   bstrOrderType;    //下單類別
(1: 現股買進。2: 現股賣出。3: 融資買進。4: 融資賣出。5: 融券買進。6: 融券賣出。7: 無券賣出)
LONG  nSpecialTradeType;       // 1:市價  2:限價
LONGnPeriod;//0:盤中 1:零股 2:盤後交易 3:盤中零股
BSTRbstrPrice;//委託價格
LONGnQty;//股數
LONGnTradeType;//0:ROD ; 1:IOC ; 2:FOK
LONG  nPriceMark;  //價格旗標 0:一般定價 1:前日收盤價 2:漲停 3:跌停
};
Proxy Server 證券刪改單
struct STOCKPROXYORDER
{
BSTRbstrFullAccount;//證券帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTR   bstrOrderType;    //下單類別 (0:刪單。1:改量。2:改價。)
LONG  nSpecialTradeType;       // 1:市價 2:限價
LONGnPeriod;//0:盤中 1:零股 2:盤後交易 3:盤中零股
BSTRbstrPrice;//委託價格
(改價:必填，帶””表示市價。減量:帶0，刪單:帶””或是原始價)
LONGnQty;//股數
(改價:帶0。減量:帶要減的量。刪單:帶原始量)
LONGnTradeType;//0:ROD ; 1:IOC ; 2:FOK
LONG  nPriceMark;  //價格旗標 0:一般定價 1:前日收盤價 2:漲停 3:跌停
BSTR  bstrBookNo;       //委託書號
BSTR  bstrSeqNo;//委託序號
};
Proxy Server 證券特殊刪改單(帶入序號刪單)
struct STOCKPROXYORDER
{
BSTRbstrFullAccount;//證券帳號，分公司代碼＋帳號7碼
BSTR   bstrOrderType;    //下單類別，0:刪單1:改量2:改價
BSTR  bstrSeqNo;//委託序號
(序號帶入 ”” 或 不帶入，會刪除證券市場所有委託)
};
Proxy Server 證券特殊刪改單
struct STOCKPROXYORDER
{
BSTRbstrFullAccount;//證券帳號，分公司代碼＋帳號7碼
BSTR   bstrOrderType;    //下單類別，0:刪單1:改量2:改價
BSTRbstrPrice;//委託價格
(改價 : 帶要改的價，改量:帶0，也可以帶""，刪單 : 帶原始價, 也可帶"")
LONG  nPriceMark; //價格旗標(改價可用) 0:一般定價 1:前日收盤價 2:漲停 3:跌停
LONGnQty; //股數(改量 : 帶要減的量)
BSTR  bstrBookNo;       //委託書號
BSTR  bstrSeqNo;//委託序號
(書號、序號、商品代號擇一填寫，都帶入””或不帶入，會刪除證券市場所有委託)
BSTRbstrStockNo; //委託股票代號(用於商品代號刪單情境)
BSTRbstrPrice_forD; //原委託單價格(用於商品代號刪單情境)
BSTRbstrBuySell_forD; //原委託單買賣別(用於商品代號刪單情境) 0是BUY ; 1 是 SELL
};
| 支持股號刪單，可刪除此id委託此股號的尚未成交的所有委託單 / 支持刪除股號+指定刪特定價格和買賣別的委託單 / 支持刪除股號+指定買賣別的委託單 |
|---|
| 股號刪單：帶入 bstrStockNo |
| 刪除股號+指定刪特定價格和買賣別：帶入bstrStockNo + bstrPrice_forD + bstrBuySell_forD |
| 刪除股號+指定買賣別：帶入bstrStockNo + bstrBuySell_forD |

5-4-4 證期委託查詢回報格式
[Format] : [1]全部 [2] 有效 [3]可消 [4]已消 [5]已成 [6]失敗 [9]預約
| 編號 | 屬性名稱 | 備註 |
|---|---|---|
| 1 | 市場別 | TW:國內證期選 TS：國內證券 TF：國內期選 OS:複委託 OF:海期選 |
| 2 | 商品別 | STO：股票 FUT：期貨 OPT：選擇權 ASO:資產交換選擇權 |
| 3 | 交易所別 | TSEA：上市 TSEB：上櫃 OTC：興櫃 TAIFEX:期交所 HK:港股 HKEX港期 |
| 4 | 分公司代號 | 例如 證券：9101 期選：F020000 |
| 5 | IBNO |
| 6 | 交易帳號 |
| 7 | 子帳號 |
| 8 | 委託書號 |
| 9 | 13碼電子流水號 |
| 10 | 原始13碼電子流水號 |
| 11 | 委託狀態 | 0：預約 2：全部成交 3：全部取消 4：部分成交，剩餘已取消 5：部分成交，剩餘可取消 6：委託失敗 7:委託成功 8:取消失敗 F:動態退單 9：取消中 / 【交易所動態退單為F開頭編碼】 / F1：動態退單-全部取消 F2：動態退單-部分成交，剩餘已取消 F3：動態退單-部分委託成功 F4：否決 |
| 12 | 委託日期 | 格式yyyyMMdd |
| 13 | 委託時間 | 格式hhmmss，證券逐筆搓合為hhmmssfff |
| 14 | 交易歸屬日 | 格式yyyyMMdd |
| 15 | 委託有效日 | 格式yyyyMMdd |
| 16 | 商品代號 |
| 17 | Tandem商品代號1 |
| 18 | Tandem契約年月1 |
| 19 | Tandem履約價1 |
| 20 | Tandem商品代號2 |
| 21 | Tandem契約年月2 |
| 22 | Tandem履約價2 |
| 23 | 買賣別 | B:買 S:賣/出借 |
| 24 | 盤別 | A：一般 B：盤後 C：零股 D：拍賣 E：鉅額 F：盤中零股 G:標借 / H:標購 I:證金標購 |
| 25 | 國內證券委託條件 | 0:現股 1:代資 2:代券 3:融資 4:融券 5:一般策略性借券賣出 6:策略性交易借券賣出 8:無券賣出 |
| 26 | 委託條件 | 0：ROD(DAY) (證券逐筆)1：GOOD_TILL_CANCEL 2：AT_THE_OPENING / 3：IOC(證券逐筆) 4：FOK (證券逐筆) 5：GOOD_TILL_GROSSING 6：GOOD_TILL_DATE 7：AT_THE_CLOSE |
| 27 | 委託方式 | 1：市價單(MKT) (證券逐筆) 2：限價單(LMT) (證券逐筆) / 3：國內期貨-範圍市價 海外期貨-停損單(STP)4：停損限價單(STP LIMIT) 5：收市價(MOC) / P.S.策略王拿到3時會備註”範圍市價”。 |
| 28 | *委託價 |
| 29 | 原始委託價格 |
| 30 | 有效委託數量 | (1)原委託量-已改量的數量-刪單數量(2)動態退單：為餘量 |
| 31 | 原始委託數量 |
| 32 | 成交數量 |
| 33 | 剩餘數量 |
| 34 | 當沖註記 | Y：當沖 N：新倉 O：平倉 A：自動 |
| 35 | 判別T+1盤 | A:T盤 B:T+1盤 |
| 36 | 是否錯誤回報 | Y:錯誤回報 N:非錯誤回報 |
| 37 | 下單來源別 |
| 38 | 交易單位股數 |
| 39 | 證券預約單價格註記 | 證券只有預約單才有此欄位 空白：用委託價格 0：close price(前日收盤價) 1：bull price (漲停) 2：bear price (跌停) h：漲停下一檔 l(小寫L)：跌停上一檔 C：漲 1/2 c：跌 1/2 |
| 40 | 營業員代號 |
| 41 | Call/Put欄位 | (僅選擇權) Call:C Put:P 非選擇權：空白 |
| 42 | 委託價-分子 | (僅海期選) |
| 43 | 委託價-分母 | (僅海期選) |
| 44 | 成交均價-價格 | (僅海期選) 債券130'05(130) |
| 45 | 成交均價-分子 | (僅海期選) 債券130'05(05) |
| 46 | 成交均價-分母 | (僅海期選) 債券130'05(32) |
| 47 | 觸發價-價格 | (僅海期選、複委託) |
| 48 | 觸發價-分子 | (僅海期選) |
| 49 | 觸發價-分母 | (僅海期選) |
| 50 | 交易所回覆訊息 | 交易所回覆訊息 (回報格式 ExchMessage tag 1081 ) |
| 51 | 動態退單基準價 | 退單基準價 |
| 52 | 取消總量 | (僅證券)其它交易給0 |
| 53 | 成交日期 | 格式yyyyMMdd |
| 54 | 成交時間 |
| 55 | 專戶註記 | (僅複委託) |
| 56 | 多幣別註記 | (僅複委託) |
| 57 | 交割幣別1 | (僅複委託) |
| 58 | 交割幣別2 | (僅複委託) |
| 59 | 交割幣別3 | (僅複委託) |
| 60 | 6碼委託序號 | (僅複委託) |
| 61 | 上手書號 | 交易所原始委託序號DMA ORDER ID (目前適用SGX DMA) |
| 62 | 庫存別 | (僅複委託) 庫存別 1：[美股]一般/定股(CITI)2：[美股]定額(VIEWTRADE) 3：[其它股市] 一般 4：[基金] 一般/定額 |
| 63 | 有效委託數量 | (複委託碎股小數) / (1)原委託量-已改量的數量-刪單數量(2)動態退單：為餘量 |
| 64 | 原始委託數量 | (複委託碎股小數) |
| 65 | 成交數量 | (複委託碎股小數) |
| 66 | 剩餘數量 | (複委託碎股小數) |
| 67 | 取消總量 | (複委託碎股小數) |
| 68 | 委託時間 | 格式HHmmssfff，證券秒下三位(毫秒)。 |
| 69 | 交易註記 | 100000：智慧單 / 100001：智慧單拆單 / 200000：一般拆單 |
| 70 | 客製化商品註記 | Y：是 / 空白：否 |

[Format]: [7]合併同價格
| 編號 | 屬性名稱 | 備註 |
|---|---|---|
| 1 | 市場別 | TW:國內證期選 TS：國內證券 TF：國內期選 OS:複委託 OF:海期選 |
| 2 | 商品別 | STO：股票 FUT：期貨 OPT：選擇權 ASO:資產交換選擇權 |
| 3 | 交易所別 | TSEA：上市 TSEB：上櫃 OTC：興櫃 TAIFEX:期交所 |
| 4 | 分公司代號 | 例如 證券：9101 期選：F020000 |
| 5 | IBNO |
| 6 | 交易帳號 |
| 7 | 子帳號 |
| 8 | 委託日期 / 交易歸屬日 | 格式yyyyMMdd / 格式yyyyMMdd(交易歸屬日僅限複委託) |
| 9 | 商品代號 |
| 11 | Tandem商品代號1 |
| 12 | Tandem契約年月1 |
| 13 | Tandem履約價1 |
| 14 | Tandem商品代號2 |
| 15 | Tandem契約年月2 |
| 16 | Tandem履約價2 |
| 17 | 買賣別 | B:買 S:賣/出借 |
| 18 | 盤別 | A：一般 B：盤後 C：零股 D：拍賣 E：鉅額 G:標借 / H:標購 I:證金標購 |
| 19 | 國內證券委託條件 | 0:現股 1:代資 2:代券 3:融資 4:融券 5:一般策略性借券賣出 6:策略性交易借券賣出 8:無券賣出 |
| 20 | 委託條件 | 0：ROD(DAY) (證券逐筆)1：GOOD_TILL_CANCEL 2：AT_THE_OPENING / 3：IOC(證券逐筆) 4：FOK (證券逐筆) 5：GOOD_TILL_GROSSING 6：GOOD_TILL_DATE 7：AT_THE_CLOSE P.S.策略王拿到3時會備註”範圍市價”。 |
| 21 | 委託價 | 僅[Format]: [7] 有 |
| 22 | 有效委託數量 | 原委託量扣除已改量的數量 |
| 23 | 原始委託數量 |
| 24 | 成交量 |
| 25 | 當沖註記 | Y：當沖 N：新倉 O：平倉 A：自動 |
| 26 | 營業員代號 |
| 27 | 委託價-分子 | (僅海期選) |
| 28 | 委託價-分母 | (僅海期選) |
| 29 | 交易單位股數 |
| 30 | 有效委託數量 | (複委託碎股小數) / 原委託量扣除已改量的數量 |
| 31 | 原始委託數量 | (複委託碎股小數) |
| 32 | 成交數量 | (複委託碎股小數) |

[Format]: [8]合併同商品
| 編號 | 屬性名稱 | 備註 |
|---|---|---|
| 1 | 市場別 | TW:國內證期選 TS：國內證券 TF：國內期選 OS:複委託 OF:海期選 |
| 2 | 商品別 | STO：股票 FUT：期貨 OPT：選擇權 ASO:資產交換選擇權 |
| 3 | 交易所別 | TSEA：上市 TSEB：上櫃 OTC：興櫃 TAIFEX:期交所 |
| 4 | 分公司代號 | 例如 證券：9101 期選：F020000 |
| 5 | IBNO |
| 6 | 交易帳號 |
| 7 | 子帳號 |
| 8 | 委託日期 / 交易歸屬日 | 格式yyyyMMdd / 格式yyyyMMdd(交易歸屬日僅限複委託) |
| 9 | 商品代號 |
| 10 | 商品名稱 |
| 11 | Tandem商品代號1 |
| 12 | Tandem契約年月1 |
| 13 | Tandem履約價1 |
| 14 | Tandem商品代號2 |
| 15 | Tandem契約年月2 |
| 16 | Tandem履約價2 |
| 17 | 買賣別 | B:買 S:賣/出借 |
| 18 | 盤別 | A：一般 B：盤後 C：零股 D：拍賣 E：鉅額 G:標借 / H:標購 I:證金標購 |
| 19 | 國內證券委託條件 | 0:現股 1:代資 2:代券 3:融資 4:融券 5:一般策略性借券賣出 6:策略性交易借券賣出 8:無券賣出 |
| 20 | 委託條件 | 0：ROD(DAY) (證券逐筆)1：GOOD_TILL_CANCEL 2：AT_THE_OPENING / 3：IOC(證券逐筆) 4：FOK (證券逐筆) 5：GOOD_TILL_GROSSING 6：GOOD_TILL_DATE 7：AT_THE_CLOSE P.S.策略王拿到3時會備註”範圍市價”。 |
| 21 | 委託價 | 僅[Format]: [7] 有 |
| 22 | 有效委託數量 | 原委託量扣除已改量的數量 |
| 23 | 原始委託數量 |
| 24 | 成交量 |
| 25 | 當沖註記 | Y：當沖 N：新倉 O：平倉 A：自動 |
| 26 | 營業員代號 |
| 27 | 交易單位股數 |
| 28 | 有效委託數量 | (複委託碎股小數) / 原委託量扣除已改量的數量 |
| 29 | 原始委託數量 | (複委託碎股小數) |
| 30 | 成交量 | (複委託碎股小數) |

5-4-5 證期成交查詢回報格式
[Format] : [1]完整 [5]T+1成交回報
| 編號 | 屬性名稱 | 備註 |
|---|---|---|
| 1 | 市場別 | TW:國內證期選 TS：國內證券 TF：國內期選 OS:複委託 OF:海期選 |
| 2 | 商品別 | STO：股票 FUT：期貨 OPT：選擇權 ASO:資產交換選擇權 |
| 3 | 交易所別 | TSEA：上市 TSEB：上櫃 OTC：興櫃 TAIFEX:期交所 |
| 4 | 分公司代號 | 例如 證券：9101 期選：F020000 |
| 5 | IBNO |
| 6 | 交易帳號 |
| 7 | 子帳號 |
| 8 | 委託書號 |
| 9 | 成交序號 |
| 10 | 成交日期 | 格式yyyyMMdd |
| 11 | 成交時間 | 格式hhmmss，證券逐筆搓合為hhmmssfff |
| 12 | 交易歸屬日 | 格式yyyyMMdd |
| 13 | 商品代號 |
| 14 | 商品名稱 |
| 15 | Tandem商品代號1 |
| 16 | Tandem契約年月1 |
| 17 | Tandem履約價1 |
| 18 | 成交價1 |
| 19 | Tandem商品代號2 |
| 20 | Tandem契約年月2 |
| 21 | Tandem履約價2 |
| 22 | 成交價2 |
| 23 | 買賣別 | B:買 S:賣/出借 |
| 24 | 盤別 | A：一般 B：盤後 C：零股 D：拍賣 E：鉅額 G:標借 / H:標購 I:證金標購 |
| 25 | 國內證券委託條件 | 0:現股 1:代資 2:代券 3:融資 4:融券 5:一般策略性借券賣出 6:策略性交易借券賣出 8:無券賣出 |
| 26 | 委託條件 | 0：ROD(DAY) (證券逐筆)1：GOOD_TILL_CANCEL 2：AT_THE_OPENING / 3：IOC(證券逐筆) 4：FOK (證券逐筆) 5：GOOD_TILL_GROSSING 6：GOOD_TILL_DATE 7：AT_THE_CLOSE |
| 27 | 成交價 |
| 28 | 成交量 |
| 29 | 當沖註記 | Y：當沖 N：新倉 O：平倉 A：自動 |
| 30 | 判別T+1盤 | A:T盤 B:T+1盤 |
| 31 | 下單來源別 |
| 32 | 委託方式 |
| 33 | 委託有效日 |
| 34 | 營業員代號 |
| 35 | 手續費 | (僅證券、複委託) / 證券：預估手續費 千分之1.425 / 複委託：TANDEM提供 |
| 36 | 交易稅 | (僅證券)預估交易稅 千分之1 或千分之3 |
| 37 | 幣別 | 提供給複委託使用的幣別，其餘皆代入空白。 |
| 38 | 委託日期 |
| 39 | 委託時間 |
| 40 | 成交價-分子 |
| 41 | 成交價-分母 |
| 42 | 交易單位股數 |
| 43 | 專戶註記 | (僅複委託) |
| 44 | 多幣別註記 | (僅複委託) |
| 45 | 總成交價金 |
| 46 | 6碼委託書號 | (僅複委託) |
| 47 | 上手書號 | 交易所原始委託序號DMA ORDER ID (目前適用SGX DMA) |
| 48 | 庫存別 | 庫存別 1：[美股]一般/定股(CITI)2：[美股]定額(VIEWTRADE) 3：[其它股市] 一般 4：[基金] 一般/定額 |
| 49 | 成交量 | (複委託碎股小數) |
| 50 | 前手息 | (複委託海外債前手息) |
| 51 | 成交時間 | 格式HHmmssfff，證券秒下三位(毫秒)。 |
| 52 | 客製化商品註記 | Y:是 / 空白:否 |

[Format]:[2]合併同書號
| 編號 | 屬性名稱 | 備註 |
|---|---|---|
| 1 | 市場別 | TW:國內證期選 TS：國內證券 TF：國內期選 OS:複委託 OF:海期選 |
| 2 | 商品別 | STO：股票 FUT：期貨 OPT：選擇權 ASO:資產交換選擇權 |
| 3 | 交易所別 | TSEA：上市 TSEB：上櫃 OTC：興櫃 TAIFEX:期交所 |
| 4 | 分公司代號 | 例如 證券：9101 期選：F020000 |
| 5 | IBNO |
| 6 | 交易帳號 |
| 7 | 子帳號 |
| 8 | 委託書號 |
| 9 | 交易歸屬日 | 格式yyyyMMdd |
| 10 | 商品代號 |
| 11 | 商品名稱 |
| 12 | Tandem商品代號1 |
| 13 | Tandem契約年月1 |
| 14 | Tandem履約價1 |
| 15 | 成交價1 |
| 16 | Tandem商品代號2 |
| 17 | Tandem契約年月2 |
| 18 | Tandem履約價2 |
| 19 | 成交價2 |
| 20 | 買賣別 | B:買 S:賣/出借 |
| 21 | 盤別 | A：一般 B：盤後 C：零股 D：拍賣 E：鉅額 G:標借 / H:標購 I:證金標購 |
| 22 | 國內證券委託條件 | 0:現股 1:代資 2:代券 3:融資 4:融券 5:一般策略性借券賣出 6:策略性交易借券賣出 8:無券賣出 |
| 23 | 委託條件 | 0：ROD(DAY) (證券逐筆)1：GOOD_TILL_CANCEL 2：AT_THE_OPENING / 3：IOC(證券逐筆) 4：FOK (證券逐筆) 5：GOOD_TILL_GROSSING 6：GOOD_TILL_DATE 7：AT_THE_CLOSE |
| 24 | 成交價 |
| 25 | 成交量 |
| 26 | 當沖註記 | Y：當沖 N：新倉 O：平倉 A：自動 |
| 27 | 判別T+1盤 | A:T盤 B:T+1盤 |
| 28 | 委託有效日 |
| 29 | 營業員代號 |
| 30 | 幣別 | 提供給複委託使用的幣別，其餘皆代入空白。 |
| 31 | 成交價-分子 |
| 32 | 成交價-分母 |
| 33 | 交易單位股數 |
| 34 | 手續費 | (僅證券、複委託) / 證券：預估手續費 千分之1.425 / 複委託：TANDEM提供 |
| 35 | 總成交價金 | (僅複委託) |
| 36 | 六碼委託書號 | (僅複委託) |
| 37 | 成交量 | (複委託碎股小數) |

[Format]:[3]合併同價格 [4]合併同商品
| 編號 | 屬性名稱 | 備註 |
|---|---|---|
| 1 | 市場別 | TW:國內證期選 TS：國內證券 TF：國內期選 OS:複委託 OF:海期選 |
| 2 | 商品別 | STO：股票 FUT：期貨 OPT：選擇權 ASO:資產交換選擇權 |
| 3 | 交易所別 | TSEA：上市 TSEB：上櫃 OTC：興櫃 TAIFEX:期交所 |
| 4 | 分公司代號 | 例如 證券：9101 期選：F020000 |
| 5 | IBNO |
| 6 | 交易帳號 |
| 7 | 子帳號 |
| 8 | 交易歸屬日 | 格式yyyyMMdd |
| 9 | 商品代號 |
| 10 | 商品名稱 |
| 11 | Tandem商品代號1 |
| 12 | Tandem契約年月1 |
| 13 | Tandem履約價1 |
| 14 | 成交價1 | 僅[Format]: [3] 有 |
| 15 | Tandem商品代號2 |
| 16 | Tandem契約年月2 |
| 17 | Tandem履約價2 |
| 18 | 成交價2 | 僅[Format]: [3] 有 |
| 19 | 買賣別 | B:買 S:賣/出借 |
| 20 | 盤別 | A：一般 B：盤後 C：零股 D：拍賣 E：鉅額 G:標借 / H:標購 I:證金標購 |
| 21 | 國內證券委託條件 | 0:現股 1:代資 2:代券 3:融資 4:融券 5:一般策略性借券賣出 6:策略性交易借券賣出 8:無券賣出 |
| 22 | 委託條件 | 0：ROD(DAY) (證券逐筆)1：GOOD_TILL_CANCEL 2：AT_THE_OPENING / 3：IOC(證券逐筆) 4：FOK (證券逐筆) 5：GOOD_TILL_GROSSING 6：GOOD_TILL_DATE 7：AT_THE_CLOSE |
| 23 | 成交價 | [Format]: [4] 呈現平均價 |
| 24 | 成交量 |
| 25 | 當沖註記 | Y：當沖 N：新倉 O：平倉 A：自動 |
| 26 | 判別T+1盤 | A:T盤 B:T+1盤 |
| 27 | 委託有效日 |
| 28 | 營業員代號 |
| 29 | 幣別 | 提供給複委託使用的幣別，其餘皆代入空白。 |
| 30 | 成交價-分子 | [Format]: [4] 呈現平均價 |
| 31 | 成交價-分母 |
| 32 | 交易單位股數 |
| 33 | 預估手續費 | (僅複委託) |
| 34 | 成交總價金 | (僅複委託) |
| 35 | 專戶註記 | (僅複委託) |
| 36 | 成交量 | (複委託碎股小數) |

Data source: https://www.capital.com.tw/event/stock/oddLot/default.asp
5-5 FUTUREOCOORDER (二擇一OCO下單物件 )
struct FUTUREOCOORDER
{
BSTRbstrFullAccount;//期貨帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託期權代號
BSTRbstrPrice;//第一腳委託價格
BSTRbstrTrigger;//第一腳觸發價，當市價大於觸發價1時觸發。
BSTRbstrPrice2;//第二腳委託價格
BSTRbstrTrigger2;//第二腳觸發價，當市價小於觸發價2時觸發。
SHORTsTradeType;//3:IOC  4:FOK
SHORTsBuySell;//0:買進 1:賣出
SHORTsDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
SHORTsNewClose;//新平倉，0:新倉 1:平倉 2:自動
LONGnQty;//交易口數
LONG sOrderPriceType1;
// 第一腳及第二腳委託價格類型。2限價;3範圍市價（不支援市價）
SHORTsBuySell2;//0:買進 1:賣出
SHORT   sReserved;      //盤別0:盤中; 1:預約
};
5-6 SKTICK( 國內報價TICK物件 )
struct SKTICK
{
LONG nPtr;            //第幾筆成交明細, 由0開始
LONG nTimehms;//時間(時分秒hh:mm:ss)
LONG nTimemillismicros;      //時間(毫秒微秒“ms”μs)
LONG nBid;//買價
LONG nAsk;//賣價
LONG nClose;//成交價
LONG nQty;//量
LONG nSimulate;//揭示 0:一般 1:試算（試撮）＊
LONG nDate;//交易日(YYYYMMDD)
};
＊盤中零股: 因盤中仍會進入試撮，請留意是否收取『試撮資料』，即試算揭示。
＊時間單位：毫秒（ms）：10-3秒 ；微秒（µs）：10-6秒，當nTimemillismicros資料是996886，前三位數字為（ms）10-3秒，後三位數為　（µs）10-6秒。
▼( MS EXCEL x86版本) 請使用5-9 SKFOREIGNTICK、5-10 SKBEST5
5-9 SKBEST5( 五檔價格物件 )
typedef struct BEST5
{
LONG  nBid1;// 第一檔買價
LONG  nBidQty1;// 第一檔買量
LONG  nBid2;// 第二檔買價
LONG  nBidQty2;// 第二檔買量
LONG  nBid3;// 第三檔買價
LONG  nBidQty3;// 第三檔買量
LONG  nBid4;// 第四檔買價
LONG  nBidQty4;// 第四檔買量
LONG  nBid5;// 第五檔買價
LONG  nBidQty5;// 第五檔買量
LONG  nExtendBid;// 衍生一檔買價{海外無衍生一檔}
LONG  nExtendBidQty;// 衍生一檔買量{海外無衍生一檔}
LONG  nAsk1;// 第一檔賣價
LONG  nAskQty1;// 第一檔賣量
LONG  nAsk2;// 第二檔賣價
LONG  nAskQty2;// 第二檔賣量
LONG  nAsk3;// 第三檔賣價
LONG  nAskQty3;// 第三檔賣量
LONG  nAsk4;// 第四檔賣價
LONG  nAskQty4;// 第四檔買量
LONG  nAsk5;// 第五檔賣價
LONG  nAskQty5;// 第五檔賣量
LONG  nExtendAsk;// 衍生一檔賣價{海外無衍生一檔}
LONG  nExtendAskQty;// 衍生一檔賣量{海外無衍生一檔}
LONG nSimulate;// 揭示 0:一般 1:試算(試撮*)
}
*國內行情—盤中零股：盤中仍有試撮行情，請自行判斷及揭示。
//［海期選］以上價格欄，需自行處理小數，參考SKFOREIGN『報價小數位數sDecimal』
5-10 SKMACD( 國內報價SKMACD物件 )
typedef struct SKMACD
{
BSTR  bstrStockNo;// 商品代碼
BSTR  bstrMACD;// 技術指標MACD平滑異同平均線
BSTR  bstrDIF;                  // 技術指標DIF
BSTR  bstrOSC                  // 技術指標OSC
}
5-11 SKBoolTunel (國內報價SKBoolTunel物件)
typedef struct SKBoolTunel
{
BSTR  bstrStockNo; // 商品代碼
BSTR  bstrAVG;     // 技術指標布林通道 AVG均線
BSTR  bstrUBT; // 技術指標 UBT通道上端
BSTR  bstr LBT; // 技術指標 LBT通道下端
}
▼( MS EXCEL x64版本) 請使用擴充SKFOREIGNTICK_9、SKBEST5_9
5-13 SKBEST5_9( 五檔價格物件-九位擴充 )
typedef struct BEST5_9
{
LONG LONGnBid1;// 第一檔買價
LONGnBidQty1;// 第一檔買量
LONG LONGnBid2;// 第二檔買價
LONGnBidQty2;// 第二檔買量
LONG LONGnBid3;// 第三檔買價
LONGnBidQty3;// 第三檔買量
LONG LONGnBid4;// 第四檔買價
LONGnBidQty4;// 第四檔買量
LONG LONGnBid5;// 第五檔買價
LONGnBidQty5;// 第五檔買量
LONG LONGnExtendBid;// 衍生一檔買價{海外無衍生一檔}
LONGnExtendBidQty;// 衍生一檔買量{海外無衍生一檔}
LONG LONGnAsk1;// 第一檔賣價
LONGnAskQty1;// 第一檔賣量
LONG LONGnAsk2;// 第二檔賣價
LONGnAskQty2;// 第二檔賣量
LONG LONGnAsk3;// 第三檔賣價
LONGnAskQty3;// 第三檔賣量
LONG LONGnAsk4;// 第四檔賣價
LONGnAskQty4;// 第四檔買量
LONG LONGnAsk5;// 第五檔賣價
LONGnAskQty5;// 第五檔賣量
LONG LONGnExtendAsk;// 衍生一檔賣價{海外無衍生一檔}
LONGnExtendAskQty;// 衍生一檔賣量{海外無衍生一檔}
LONGnSimulate;// 揭示 0:一般 1:試算
}
//以上價格欄，需自行處理小數，參考SKFOREIGN『報價小數位數sDecimal』
5-14 STOCKSTRATEGYORDER(證券智慧單物件-當沖)
證券當沖
struct STOCKSTRATEGYORDER
{
BSTRbstrFullAccount;        //證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;        //委託股票代號
LONG   nBuySell;//當沖進場  0:現股買;1:無券賣出
LONG    nQty;//整股(委託張數)
BSTRbstrOrderPrice;//當沖單進場委託價
LONG       nOrderPriceCond;//0:ROD ; 3:IOC ; 4:FOK
LONGnOrderPriceType;//委託價類別 1:市價 ; 2:限價
LONGnInnerOrderIsMIT;//進場是否MIT( 0: N; 1:Y)
LONGnMITDir;//進場MIT觸價方向(若未啟用MIT,請填0)
//1: 向上觸發(大於等於);2:向下觸發(小於等於)
BSTRbstrMITTriggerPrice; //進場MIT觸發價(若未啟用MIT,請填0)
BSTRbstrMITDealPrice;//進場MIT當下市價(若未啟用MIT,請填0)
//當沖出場條件//
| LONG    nClearAllFlag;//執行時間出清0:否；1:是 / BSTR       bstrClearCancelTime;//出清時間-時＋分（hhmm） / //{每日13:20截止} / LONG　　nClearAllPriceType;//出清委託價方式= 1:市價;  2:限價 / BSTR       bstrClearAllOrderPrice;  //出清委託價 |
|---|

| LONG    nFinalClearFlag;//盤後定盤交易 0:否；1:是 |
|---|

LONG   nTakeProfitFlag;    //出場停利條件 0:否；1:是
LONG   nRDOTPPercent;//停利類型 0:觸發價；1:漲幅
BSTRbstrTPPercent;//停利百分比
BSTRbstrTPTrigger;//停利觸發價
LONG   nRDTPMarketPriceType;//停利委託價方式1:市價;  2:限價
BSTRbstrTPOrderPrice;//停利委託價
LONG   nStopLossFlag;//出場停損條件0:否；1:是
LONG   nRDOSLPercent;//停損類型 0:觸發價；1:漲跌幅
BSTR    bstrSLPercent;//停損百分比
BSTRbstrSLTrigger;//停損觸發價
LONGnRDSLMarketPriceType;///停損委託價方式1:市價;   2:限價
BSTRbstrSLOrderPrice;//停損委託價
LONGnTakeProfitOrderCond;    //停利出場時效 0:ROD;3:IOC;4:FOK
LONGnStopLossOrderCond;//停損出場時效 0:ROD;3:IOC;4:FOK
LONGnClearOrderCond;//出清出場時效 0:ROD;3:IOC;4:FOK
}
證券自組單(CB)
struct STOCKSTRATEGYORDER
{
BSTRbstrFullAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
LONGnOrderType; //委託交易別(0:現股, 3:融資, 4:融券, 8:無券普賣)
LONGnOrderPriceCond;//委託時效(0:ROD,  3:IOC,  4:FOK)
LONGnBuySell;// 0:買; 1:賣
LONGnQty;//委託張數
LONGnOrderPriceType;//委託價類別 1:市價 ; 2:限價
BSTRbstrOrderPrice;//委託價
LONG    nInnerOrderIsMIT;    //觸發條件是否為成交價 (0:否, 1:是)
BSTRbstrMITTriggerPrice;//成交價
LONG    nMITDir;    //成交價觸價方向 (0:None, 1:GTE大於等於, 2:LTE小於等於)
LONGnTakeProfitFlag;                     //觸發條件是否為委買價(0:否, 1:是)
LONG nTPDir;//委買價觸價方向 (0:None, 1:GTE大於等於, 2:LTE小於等於)
BSTRbstrTPOrderPrice;                 //委買價
LONGnStopLossFlag; //是否為委賣價 (0:否, 1:是)
LONGnSLDir;//委賣價觸價方向(0:None, 1:GTE大於等於, 2:LTE小於等於)
BSTRbstrSLOrderPrice;                   //委賣價
LONGnTickFlag;//是否為tick觸發  (0:否, 1:是)
LONGnTickDir; //tick觸發方向 (0:None, 1:GTE大於等於, 2:LTE小於等於)
BSTRbstrTick;//tick數
LONGnUpDownFlag;//是否為漲跌幅 (0:否, 1:是)
LONGnUpDownDir;//漲跌幅觸價方向(0:None, 1:GTE大於等於, 2:LTE小於等於)
BSTRbstrSLPercent;//漲跌幅數
LONGnPreQtyFlag;//是否執行單量 (0:否, 1:是)
LONGnPreQtyDir; //單量觸價方向(0:None, 1:GTE大於等於, 2:LTE小於等於)
BSTRbstrPreQty; //單量
LONGnSumQtyFlag;//觸發條件是否為總量  (0:否, 1:是)
LONGnSumQtyDir;//總量觸價方向(0:None, 1:GTE大於等於, 2:LTE小於等於)
BSTRbstrSumQty;//總量
LONGnClearAllFlag;//是否為自訂啟動時間 (0:立即, 1:自訂)
BSTRbstrClearCancelTime;//自訂啟動時間(14天內)，格式：hhmmss (時分秒，EX：130909)
LONGnFinalClearFlag;//條件是否為全部成立 (0:任一成立, 1:全部成立)
)
5-15 STOCKSTRATEGYORDEROUT(證券智慧單物件-出清)
struct STOCKSTRATEGYORDEROUT
{
BSTRbstrFullAccount;        //證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;        //委託股票代號
LONG    nBuySell;//出清  0:買；1:賣
LONG    nQty;//整股(委託張數)
//出清條件//
| LONG    nClearAllFlag;//執行時間出清0:否；1:是 / BSTR       bstrClearCancelTime;//出清時間-時＋分（hhmm） / //{每日13:20截止} / LONG    nClearAllPriceType;//出清委託價方式=1:市價;  2:限價 / BSTR       bstrClearAllOrderPrice;  //出清委託價 |
|---|

| LONG    nFinalClearFlag;//盤後定盤交易 0:否；1:是 |
|---|

LONG    nOrderType;
//出清進場委託別0:現股;3: (自)融資 ;4: (自)融券  {現賣或現買-資賣-券買}
LONGnLTEFlag;//出場成交價小於指定價後觸發0:否；1:是
BSTRbstrLTETriggerPrice;//小於指定觸發價
BSTR   bstrLTEOrderPrice;//出場指定委託價
LONG    nLTEMarketPrice;//(小於條件)以1:市價;  2:限價 做為出場價格
LONG    nGTEFlag;//出場成交價大於指定價後觸發0:否；1:是
BSTRbstrGTETriggerPrice;//大於指定觸發價0:否；1:是
BSTR   bstrGTEOrderPrice;//出場指定委託價
LONG    nGTEMarketPrice;//(大於條件)以1:市價;  2:限價 做為出場價格
LONGnGTEOrderCond;//大於條件出場時效0:ROD;3:IOC;4:FOK
LONG   nLTEOrderCond;//小於條件出場時效0:ROD;3:IOC;4:FOK
LONGnClearOrderCond;//出清出場時效0:ROD;3:IOC;4:FOK
}
比較表
| 異動後V2.13.49 |
|---|
| 新增商品上市、上櫃參數選項 |

5-16 OVERSEAFUTUREORDERFORGW(海期委託GW格式物件)
struct OVERSEAFUTUREORDERFORGW
{
BSTRbstrFullAccount;    //海期帳號，分公司四碼＋帳號7碼{改價使用}
BSTRbstrExchangeNo;//交易所代碼。{改價使用}
BSTRbstrStockNo;    //期權代號。{改價使用}
BSTR   bstrStockNo2;//期價差商品代號{價差改價使用}
BSTRbstrYearMonth;//近月商品年月( YYYYMM) 6碼{改價使用}
BSTRbstrYearMonth2;//遠月商品年月( YYYYMM) 6碼 {價差改價使用}
BSTRbstrOrderPrice;//新委託價。{改價使用}
BSTRbstrOrderNumerator;//新委託價分子。{改價使用}
BSTRbstrOrderDenominator;    //新委託價分母。{改價使用}
BSTRbstrTriggerPrice;    //觸發價。{目前未使用可忽略}
BSTRbstrTriggerNumerator;    //觸發價分子。{目前未使用可忽略}
BSTRbstrTriggerDenominator;//觸發價分母。{目前未使用可忽略}
BSTRbstrStrikePrice;    //履約價。{選擇權改價使用}
BSTRbstrBookNo;//委託書號{改價使用}
BSTRbstrSeqNo;//委託序號{改價使用}
LONGnBuySell;    //0:買進 1:賣出－第一腳{改價可忽略}
LONGnBuySell2;//0:買進 1:賣出－第二腳{改價可忽略}
LONGnNewClose;//新平倉，0:新倉 1:平倉 2:自動
{目前海期僅有新倉，海選有新倉或平倉}
LONGnDayTrade;//當沖0:否 1:是，可當沖商品請參考交易所規定。
{改價可忽略}
LONGnTradeType;//0:ROD  3:IOC  4:FOK
{{改價使用}目前海期、海選均固定ROD}
LONGnSpecialTradeType;// 0:LMT {{改價使用}目前僅提供限價單改限價}
LONGnCallPut;//0:CALL  1:PUT {選擇權改價使用}
LONGnQty;//交易口數。{改價可忽略}
LONG  nQty2;//交易口數，複式單使用欄位{改價可忽略}
};
5-17STOCKSTRATEGYORDERMIT(證券智慧單物件-MIT/MST)
MIT單
(新版本MIT， V2.13.30 含以上版本適用)，並自V2.13.30 (含)以上版本起，不再支援舊版下單)
struct STOCKSTRATEGYORDERMIT
{
BSTRbstrFullAccount;        //證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;        //委託股票代號
BSTR   bstrOrderPrice;//委託價  (若為市價單, 委託價請填0)
BSTRbstrTriggerPrice;    //觸發價
BSTRbstrDealPrice; //成交價  (當下市價)，洗價機留存用
LONG   nOrderType;//委託 0:現股;3(自)融資;4: (自)融券;8:無券賣出
LONG   nBuySell;//0買;1賣出
LONG   nQty;//張數
LONG nOrderPriceType;//1:市價;2:限價
LONG nOrderCond;//0:ROD;3:IOC;4:FOK
LONG  nTriggerDir;//觸價方向1: >=大於等於 ;2:<=小於等於
LONG  nPreRiskFlag;//預風控功能 0:關閉預風控 ;1:開啟預風控
(不支援信用交易)
LONG nLongActionFlag;//是否為長效單(0:否, 1:是)
BSTR bstrLongEndDate//長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
LONG nLAType//觸發結束條件(1:效期內觸發即失效, 3:效期內完全成交即失效)
};
比較表
| 異動後V2.13.45 |
|---|
| MIT下單新增長效單功能：是否為長效單(0:否, 1:是)、長效單結束日期(YYYYMMDD共8碼)、觸發結束條件 / MIT下單新增預風控欄位 |

MST單
struct STOCKSTRATEGYORDERMIT
{
BSTRbstrFullAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
LONGnPrime;//[MST only] (0:上市; 1:上櫃)
LONGnOrderType; //委託交易別(0:現股, 3:融資, 4:融券, 8:無券普賣)
LONGnOrderCond;//委託時效(0:ROD, 3:IOC, 4:FOK)
LONGnBuySell;// 0:買; 1:賣
LONG nTriggerMethod; //啟動條件(0：以市價立即啟動, 1：以自訂價格啟動)
LONGnTriggerDir//觸價方向 (1:GTE大於等於,
2:LTE小於等於)
BSTRbstrStartPrice;//[自訂價格啟動適用]觸發價
BSTRbstrDealPrice; //[市價啟動適用]請填市價
BSTRbstrMovePoint;//移動點數
LONGnQty;//委託張數
LONGnOrderPriceType;//委託價類別 1:市價 ; 2:限價
BSTRbstrOrderPrice;//委託價
LONGnLongActionFlag;//是否為長效單(0:否, 1:是)
BSTRbstrLongEndDate//長效單結束日期(YYYYMMDD共8碼, EX: 20220630)
LONGnLAType//觸發結束條件(0:非長效單, 1:效期內觸發即失效)
};
AB單
struct STOCKSTRATEGYORDERMIT
{
BSTRbstrFullAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo2;//A商品 商品代號
LONGnMarketNo;//A商品 市場編號(1:國內證, 2:國內期, 3:國外證, 4:國外期)
BSTRbstrExchangeNo;// A商品交易所代碼(EX: TSE、TAIFEX、CME)
BSTRbstrStartPrice;           // A商品 成交價
LONGnTriggerDir;// A商品 觸價方向 (1:GTE大於等於,
2:LTE小於等於)
BSTRbstrTriggerPrice;// A商品 觸發價
BSTRbstrStockNo;// B商品 委託股票代號
LONGnOrderType; // B商品 委託交易別(0:現股, 3:融資, 4:融券, 8:無券普賣)
LONGnOrderCond;// B商品 委託時效(0:ROD, 3:IOC, 4:FOK)
LONGnBuySell;// B商品 買賣別(0:買進, 1:賣出)
LONGnQty;// B商品 委託張數
LONGnOrderPriceType;// B商品 委託價類別(1:市價 ; 2:限價)
BSTRbstrOrderPrice;// B商品 委託價
LONGnPrime;// B商品  0:上市, 1:上櫃(不開放權證、興櫃商品)
LONGnReserved;//是否為預約單(0:否, 1:是)，A商品為國內期選商品時可選預約單，其餘市場為非預約單
}
5-18 STOCKSTRATEGYORDEROCO(證券智慧單物件-OCO)
struct STOCKSTRATEGYORDEROCO
{
BSTRbstrFullAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTRbstrOrderPrice;//第一腳委託價格//
BSTRbstrTriggerUp;//第一腳觸發價
BSTRbstrOrderPrice2;//第二腳委託價格//
BSTRbstrTriggerDown;//第二腳觸發價。
LONGnBuySellUp;//第一腳買賣// 0:買進 1:賣出
LONGnBuySellDown;//第二腳買賣// 0:買進 1:賣出
LONGnOrderCondUp;//第一腳 0:ROD 3:IOC  4:FOK
LONG  nOrderCondDown;//第二腳 0:ROD 3:IOC  4:FOK
LONG    nOrderTypeUp;
//第一腳委託別 0:現; 3: (自)融資;4(自)融券; 8:無券普賣
LONG    nOrderTypeDown;
//第二腳委託別 0:現;3: (自)融資;4(自)融券;8:無券普賣
LONG  nOrderPriceTypeUp;//第一腳委託價 1:市價; 2:限價
LONG  nOrderPriceTypeDown;//第二腳委託價 1:市價; 2:限價
LONG  nQty;//交易張數
};
比較表
| 異動後v2.13.48 |
|---|
| 新增無券普賣委託別 |

5-19 TSPROFITLOSSGWQUERY(新損益試算查詢物件)
未實現:
struct TSPROFITLOSSGWQUERY
{
BSTRbstrFullAccount;
LONG   nTPQueryType;//損益類別 0: 未實現損益；1: 已實現損益 2:現股當沖
LONGnFunc;//損益回傳格式 //(未實現) 0:彙總 ；1:明細
BSTR   bstrStockNo;//當nFunc 為 1:明細 時,指定商品代碼
BSTRbstrTradeType; //當nFunc 為 1:明細 時,指定交易類別
//0:現股 3:融資(自) 4:融券(自) 8:券差 9:無券賣出
};
已實現:
struct TSPROFITLOSSGWQUERY
{
BSTRbstrFullAccount;
LONG   nTPQueryType;//損益類別 1: 已實現損益
LONGnFunc;//損益回傳格式
// 0:彙總；1:明細; 2:投資總額; 3: 彙總(依股票代號)
BSTR   bstrStockNo;　//當nFunc 為 1:明細 及3: 彙總依股票代號 時，指定商品代碼
BSTRbstrTradeType; //當nFunc 為 1:明細，指定交易類別
//0:現股 1:融資(代) 2:融券(代) 3:融資(自) 4:融券(自)  6:現沖 7.信用當沖 8:券差
BSTR    bstrStartDate;
//當nFunc 為 0:彙總 時，起始日西元年月日//YYYYMMDD
//當nFunc 為 1:明細 及 3:彙總依股票代號 時，起始日民國年月日
BSTR bstrEndDate;//當nFunc 為 0:彙總 時，結束日西元年月日//YYYYMMDD
//當nFunc 為 1:明細格式時,不需填結束日
//當nFunc 3:彙總依股票代號 時，結束日 民國年月日
BSTR    bstrBookNo;//當nFunc 為 1:明細 時,指定委託書號
BSTR    bstrSeqNo;//當nFunc 為 1:明細 時,指定序號
};
現股當沖:(限查閱當日交易)
struct TSPROFITLOSSGWQUERY
{
BSTRbstrFullAccount;
LONG   nTPQueryType;//損益類別 2:現股當沖
LONGnFunc;//損益回傳格式 //(現股當沖) 1: 彙總 2: 明細
BSTR   bstrStockNo;//當nFunc 為 2時:明細時,需指定商品代碼
};
5-20STOCKSTRATEGYORDERMIOC(證券智慧單物件-多次IOC)
struct STOCKSTRATEGYORDERMIOC
{
BSTRbstrFullAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrStockNo;//委託商品代號
LONGnPrime;          //0上市; 1上櫃
LONGnBuySell;// 0:買進 1:賣出
LONG    nOrderType;
//委託別 0:現;3: (自)融資; 4: (自)融券;8無券賣
LONGnOrderPriceType;//1:市價; 2: (買單)委賣價或(賣單)委買價;
//委買、賣價實際價格由中台決定
BSTRbstrOrderPriceUp;//委託價上限。
BSTRbstrOrderPriceDown;//委託價下限。
LONGnOneceQtyLimit;//單次交易張數上限
LONGnTotalQty;//總委張數
};
*v2.13.48 買賣別參數值修正
5-21 SKSTOCKLONG ( 國內報價商品物件 LONG index )
struct SKSTOCKLONG
{
LONGnStockidx;// 商品自定索引代號
SHORTsDecimal;// 小數位數
SHORTsTypeNo;//  EX: (證券)類股別 1 水泥 , 2 食品…etc.
BSTRbstrMarketNo;// 市埸代碼
BSTRbstrStockNo;// 商品代碼EX: 1101 台泥, TX12 台指期12月…etc.
BSTRbstrStockName;// 商品名稱
LONGnHigh;// 最高價
LONGnOpen;// 開盤價
LONGnLow;// 最低價
LONGnClose;// 成交價
LONGnTickQty;// 單量
LONGnRef;// 昨收、參考價
LONGnBid;// 買價
LONGnBc;// 買量
LONGnAsk;// 賣價
LONGnAc;// 賣量
LONGnTBc;// 買盤量(即外盤量)
LONGnTAc;// 賣盤量(即內盤量)
LONGnFutureOI;// 期貨未平倉 OI
LONGnTQty;// 總量
LONGnYQty;// 昨量
LONGnUp;// 漲停價
LONGnDown;// 跌停價
LONGnSimulate;// 揭示 0:一般 1:試算　＊
// [證券逐筆]盤中出現『1:試算』代表行情劇動，
觸發價格穩定措施狀態
LONGnDayTrade// [限證券整股商品]可否當沖 0:一般 1:可先買後賣現股當沖
2:可先買後賣和先賣後買現股當沖
LONGnTradingDay// 交易日(YYYYMMDD)
備註 :當日非交易日時, 資料為前一交易日
LONGnDealTime//成交時間 (hhmmss)
LONGnOddLotBid;    // 盤後零股(14:25~14:30)買價
LONGnOddLotAsk;    // 盤後零股(14:25~14:30)賣價
LONGnOddLotClose;  // 盤後零股(14:25~14:30)成交價
LONGnOddLotQty;    // 盤後零股(14:25~14:30)成交量
};
5-21-2盤中零股
＊因盤中仍會進入試撮，請留意是否收取『試撮資料』，即試算揭示.
struct SKSTOCKLONG
{
LONGnStockidx;// 商品自定索引代號
SHORTsDecimal;// 小數位數
SHORTsTypeNo;//  EX: (證券)類股別 1 水泥 , 2 食品…etc.
BSTRbstrMarketNo;// 市埸代碼 (0:上市, 1:上櫃, 2:期貨, 3:選擇權, 4:盤中零股-上市, 6:盤中零股-上櫃)
BSTRbstrStockNo;// 商品代碼EX: 1101 台泥, TX12 台指期12月…etc.
BSTRbstrStockName;// 商品名稱
LONGnHigh;// 最高價
LONGnOpen;// 開盤價
LONGnLow;// 最低價
LONGnClose;// 成交價
LONGnTickQty;// 單量
LONGnRef;// 昨收、參考價
LONGnBid;// 買價
LONGnBc;// 買量
LONGnAsk;// 賣價
LONGnAc;// 賣量
LONGnTBc;// 買盤量(即外盤量)
LONGnTAc;// 賣盤量(即內盤量)
LONGnTQty;// 總量
LONGnYQty;// 昨量
LONGnUp;// 漲停價
LONGnDown;// 跌停價
LONGnSimulate;// 揭示 0:一般 1:試算(試撮)　＊
// [證券逐筆]盤中出現『1:試算』代表行情劇動，
觸發價格穩定措施狀態;
//[證券盤中零股] 盤中會進入試撮，請留意是否收取『試撮資料』
LONGnDayTrade// [限證券商品]可否當沖 0:一般
LONGnTradingDay// 交易日(YYYYMMDD)
備註 :當日非交易日時, 資料為前一交易日
LONGnTradingLotFlag//[證券] 整股、盤中零股揭示註記   0:現股 ; 1:盤中零股
LONGnDealTime//成交時間 (hhmmss)
};
5-24 CANCELSTRATEGYORDER(智慧單刪單物件 )
證券當沖:
struct CANCELSTRATEGYORDER
{
BSTRbstrFullAccount;        //證券帳號，分公司四碼＋帳號7碼
LONG   nMarket;// 市場別1:國內證券
BSTR   bstrParentSmartKey;                  // 智慧母單號;
BSTR   bstrSmartKey;                  // 智慧單號; 出場單則給出場單號
LONG  nTradeKind;                  //11:當沖;
LONG nDeleteType;//刪單類型 1: 全部; 2: 進場單 ; 3:出場單
BSTR bstrSeqNo;//委託序號(1~3三種類型均需提供, 預約單可忽略)
BSTR   bstrOrderNo;//((刪進場/出場單提供))委託書號，
（若觸發，需給書號）
BSTR   bstrSmartKeyOut;//出場單號(刪全部時提供，刪進場單及出場單時不需提供出場單號) //委託量大於1時，會不止一筆出場單，出場單號以逗號「, 」區隔
}
＊若被動查詢回報結果，當無書號時，則不需提供；若無尚無出場單號，則可不填。
＊刪全部及刪進場單時，智慧單母單號與智慧單號相同。
證券出清:
struct CANCELSTRATEGYORDER
{
BSTRbstrFullAccount;        //證券帳號，分公司四碼＋帳號7碼
LONG   nMarket;// 市場別1:國內證券
BSTR   bstrSmartKey;                  // 智慧單號; 出場單則給出場單號
LONG  nTradeKind;                  //17:出清
BSTR bstrSeqNo;//委託序號 (預約單可忽略)
BSTR   bstrOrderNo;//委託書號（若觸發，需給書號）
}
證券二擇一OCO、MIT、MST、AB、CB:
struct CANCELSTRATEGYORDER
{
BSTRbstrFullAccount;        //證券帳號，分公司四碼＋帳號7碼
LONG   nMarket;// 市場別1:國內證券(AB單需選欲刪單之A商品市場別，1:國內證, 2:國內期, 3:國外證, 4:國外期)
BSTR   bstrSmartKey;            // 智慧單號
LONG  nTradeKind;              //3:OCO、8:MIT、9:MST、10:AB、27：CB
BSTR bstrSeqNo;//委託序號 (預約單可忽略)
BSTR   bstrOrderNo;//委託書號（若觸發，需給書號）
BSTRbstrLongActionKey;//長效單號(非長效單請輸入0)
}
比較表
| 異動後V2.13.45 |
|---|
| 智慧單刪單物件新增長效單號、新增刪單別：非長效單之刪單，長效單號可忽略 |
| 異動後V2.13.48 |
| 移除MBA、LLS刪單。 / 非長效單請於長效單號輸入0。 |

證券智慧單—可刪單狀態參考:
| 代碼 | 是否可刪單 | 當沖(進場單) | 當沖(出場單) | MIOC單 | 當沖(刪全) | 出清 |
|---|---|---|---|---|---|---|
| 32 | Y | Y | Y | Y | Y | Y |
| 34 | Y | Y | Y | Y | Y | Y |
| 35* | Y |  |  |  |  |  |
| 37 | Y | Y | Y | Y | Y | Y |
| 38 | Y | Y | Y | Y | Y | Y |
| 42 |  | Y |  |  | Y |  |
| 43 |  | Y |  |  | Y |  |

代碼參考表
| 代碼 | 說明+前台顯示訊息 | Message |
|---|---|---|
| 32 | 中台收單成功 | [智慧單號][股票代號]收單 ex. [375][1101]收單 |
| 34 | 洗價中 | [智慧單號][股票代號] 已加入洗價 ex.  [375][1101]已加入洗價 |
| 35 | 洗價中-觸發價更新(移動停損單) | [智慧單號][股票代號] 觸發價更新 |
| 37 | 洗價觸發 | [智慧單號][股票代號]觸發條件觸發ex.[375][1101]成交價觸發 |
| 38 | 觸發下單 | [智慧單號][股票代號]Tandem回覆訊息 ex. [353][1101]委託資料傳送交易所中! |
| 42 | 進場單委託中(當沖) | MIT進場->[智慧單號][股票代號]進場單-MIT觸發，送出委託 |
| 43 | 進場單成交，執行出場條件(當沖) | [智慧單號][股票代號]進場單成交，執行出場條件 |

期貨智慧單刪單STP、MST、MIT、OCO、AB:
struct CANCELSTRATEGYORDER
{
BSTR   bstrLogInID;//登入ID
BSTRbstrFullAccount;        //期貨帳號
LONG   nMarket;// 市場別2:國內期貨選擇權
BSTR   bstrSmartKey;                  // 智慧單號
LONG  nTradeKind;                  //3:OCO、5:STP、8:MIT、9:MST、10：AB
BSTR bstrSeqNo;//委託序號 (預約單可忽略)
BSTR   bstrOrderNo;//委託書號
（若欲刪除之委託已產生書號則需填入；預約單可忽略）
BSTRbstrLongActionKey;//長效單號(非長效單請輸入0)
}
*請留意，未填書號將影響解除保證金等風控
比較表
| 異動後V2.13.45 |
|---|
| 智慧單刪單物件新增長效單號、新增刪單別：非長效單之刪單，長效單號可忽略 |

海期智慧單刪單OCO、AB:
struct CANCELSTRATEGYORDER
{
BSTRbstrFullAccount;    //帳號，分公司四碼＋帳號7碼
LONGnMarket;//市場別4:國外期 (AB單需選欲刪單之A商品市場別，1:國內證, 2:國內期, 3:國外證, 4:國外期)
BSTRbstrSmartKey;           //智慧單號
LONG  nTradeKind;             //3：OCO長效單、10：AB單
BSTR bstrSeqNo;//委託序號 (預約單可忽略)
BSTRbstrOrderNo;//委託書號（若觸發，需給書號）
BSTRbstrLongActionKey;//長效單號(非長效單可忽略)
}
智慧單多筆刪單:
struct CANCELSTRATEGYORDER
{
BSTRbstrFullAccount;        //帳號
LONG   nMarket;//刪單市場別，1:國內證, 2:國內期, 3:國外證, 4:國外期 (AB單刪單請選擇A商品之市場別)
BSTR   bstrSmartKey;             //多筆智慧單號 (以逗號分隔，EX：682020,682021)
}
5-25SKAVGCOST (昨日未沖銷的證券明細查詢物件)
struct SKAVGCOST
{
BSTRbstrAccount;//證券帳號，分公司四碼＋帳號7碼
BSTRbstrFunc;//功能(0:查詢昨日未沖銷明細)
BSTRbstrStockNo;//股票代號(空白:所有股票代號)
}
5-25-2查詢昨日未沖銷明細回報格式
| 編號 | 欄位名稱 | 備註 |
|---|---|---|
| 1 | 成交日期(民國年) |  |
| 2 | 單位 | 分公司四碼 |
| 3 | 帳號 |  |
| 4 | 股票代號 |  |
| 5 | 成交序號 |  |
| 6 | 庫存種類 | 現股/融資/融券 |
| 7 | 剩餘股數 |  |
| 8 | 單價 |  |
| 9 | 手續費 |  |
| 10 | 融資自備款/融券保證金 |  |
| 11 | 融資金額/擔保品金額 |  |
| 12 | 融資/融券利息 |  |
| 13 | 融券手續費 |  |

5-26 SKKLINE(國內報價KLINE物件)
struct SKKLINE
{
LONG nDate;//交易日(YYYYMMDD)
LONG nTimehm;//成交時間HHmm (Ex: 904，代表09點04分)
LONG nOpen;//開盤價
LONG nHigh;//最高價
LONG nLow;//最低價
LONG nClose;//目前價
LONG nQty;//目前量
};
6、代碼定義表
| 代號 | 名稱 | 說明 |
|---|---|---|
| 1000 | SK_ERROR_LOGIN_FIRST | 請先由Center 進行登入動作 / *請注意登入帳號是否為大寫 |
| 1001 | SK_ERROR_INITIALIZE_FAIL | 登入失敗，請由LOG查看失敗原因。 |
| 1002 | SK_ERROR_ACCOUNT_NOT_EXIST | 交易帳號不存在。 |
| 1003 | SK_ERROR_ACCOUNT_MARKET_NOT_MATCH | 下單帳號類型不正確，是否有證券帳號下期貨之情行。 |
| 1004 | SK_ERROR_PERIOD_OUT_OF_RANGE | PERIOD 超出選擇範圍。 |
| 1005 | SK_ERROR_FLAG_OUT_OF_RANGE | FLAG 超出選擇範圍。 |
| 1006 | SK_ERROR_BUYSELL_OUT_OF_RANGE | BUYSELL 超出選擇範圍。 |
| 1007 | SK_ERROR_ORDER_SERVER_INVALID | 下單主機不存在。 |
| 1008 |  |  |
| 1009 | SK_ERROR_TRADE_TYPR_OUT_OF_RANGE | TRADE TYPE 超出選擇範圍。 |
| 1010 | SK_ERROR_DAY_TRADE_OUT_OF_RANGE | DAY TRADE超出選擇範圍。 |
| 1011 | SK_ERROR_ORDER_SIGN_INVALID |  |
| 1012 | SK_ERROR_NEW_CLOSE_OUT_OF_RANGE | NEW CLOSE 超出選擇範圍。 |
| 1013 | SK_ERROR_PRODUCT_INVALID | 下單商品不存在。 |
| 1014 | SK_ERROR_QTY_INVALID | 單量錯誤，請確認是否為數字。 |
| 1015 | SK_ERROR_DAYTRADE_DENIED | 商品不支援當沖 |
| 1016 | SK_ERROR_SPCIAL_TRADE_TYPE_INVALID | 商品不支援該下單類型。 |
| 1017 | SK_ERROR_PRICE_INVALID | 下單價格錯誤。 |
| 1018 | SK_ERROR_INDEX_OUT_OF_RANGE | INDEX 超出選擇範圍。 |
| 1019 | SK_ERROR_QUERY_IN_PROCESSING | 查詢尚未結束請稍後再試。 |
| 1020 | SK_ERROR_LOGIN_INVALID |  |
| 1021 | SK_ERROR_REGISTER_CALLBACK |  |
| 1022 | SK_ERROR_FUNCTION_PERMISSION_DENIED |  |
| 1023 | SK_ERROR_MARKET_OUT_OF_RANGE | MARKET 超出選擇範圍。 |
|  |  |  |
| 1025 | SK_ERROR_FOREIGNSTOCK_PRICE_OUT_OF_RANGE |  |
| 1026 | SK_ERROR_FOREIGNSTOCK_UNDEFINE_COINTYPE |  |
| 1027 | SK_ERROR_FOREIGNSTOCK_SAME_COINSTYPE |  |
| 1028 | SK_ERROR_FOREIGNSTOCK_SALE_SHOULD_ORIGINAL_COIN |  |
| 1029 | SK_ERROR_FOREIGNSTOCK_TRADE_UNIT_INVALID |  |
| 1030 | SK_ERROR_FOREIGNSTOCK_STOCKNO_INVALID |  |
| 1031 | SK_ERROR_FOREIGNSTOCK_ACCOUNTTYPE_INVALID |  |
| 1032 | SK_ERROR_FOREIGNSTOCK_INITIALIZE_FAIL |  |
| 1033 | SK_ERROR_TS_INITIALIZE_FAIL |  |
| 1034 | SK_ERROR_OVERSEA_TRADE_PRODUCT_FAIL |  |
| 1035 | SK_ERROR_OVERSEA_TRADE_DATA_NOT_COMPLETE | 海期交易商品尚未初使完成，請先執行SKOrderLib_LoadOSCommodity |
| 1036 | SK_ERROR_CERT_VERIFY_CN_INVALID | 憑證CN錯誤， |
| 1037 | SK_ERROR_CERT_VERIFY_SERVER_REJECT | 憑證驗章失敗，請確認憑證有效性 |
| 1038 | SK_ERROR_CERT_NOT_VERIFIED | 憑證尚未驗章，請先執行ReadCertByID |
| 1039 | SK_ERROR_OVERSEA_ACCOUNT_NOT_EXIST | 海期帳號不存在。 |
| 1040 | SK_ERROR_ORDER_LOCK | 下單上鎖，請先執行UnlockOrder進行解鎖。 |
| 1041 | SK_ERROR_SERVER_NOT_CONNECTED | 與主機尚未連線。 |
| 1042 | SK_ERROR_OVERSEA_KLINE_DATA_TYPE_NOT_FOUND | KLINE TYPE 超出選擇範圍。 |
| 1043 | SK_ERROR_STRIKEPRICE_INVALID | 履約價不正確。 |
| 1044 | SK_ERROR_CALLPUT_INVALID | CALL PUT 不正確。 |
| 1045 | SK_ERROR_CERT_NOT_FOUND | 憑證不存在，請先匯入憑證到IE 中。 |
| 1046 | SK_ERROR_RESERVED_OUT_OF_RANGE | 委託盤別超出範圍 |
| 1047 | SK_ERROR_SMART_TRADE_ORDER_SERVER_INVALID | 智慧單中台主機不存在 |
| 1048 | SK_ERROR_CANCEL_ORDER_SMARTKEY_INVALID | 智慧單(停損單)序號為無效值 |
| 1049 | SK_ERROR_OVERSEA_FUTURE_SPREAD_ORDER | 海期價差委託時，（價格為非０數值）出現分子填入負號之錯誤 |
| 1050 | SK_ERROR_MIT_ORDER_EXCLUDE_SPREAD_PRODUCT | 智慧單MIT不可委託價差商品 |
| 1051 | SK_ERROR_SGX_API_ORDER_SEQNO_CONFILICT | SGX API 委託序號重覆 |
| 1052 | SK_ERROR_OVERSEA_FUTURE_SPREAD_ORDER_NO_DAYTRADE | 海期價差委託不可帶當沖 |
| 1053 | SK_ERROR_SGX_API_LOGON_FAIL | SGX API登入失敗 |
| 1054 | SK_ERROR_MIT_ORDER_MUST_GIVE_TRIGGER_PRICE | MIT委託內容需含觸發價 |
| 1056 | SK_ERROR_STRATEGY_ORDER_MUST_GIVE_DEAL_PRICE | 智慧單委託內容需含成交價 |
| 1057 | SK_ERROR_WITHDRAW_WITHOUT_PASSWORD | 未輸入出入金密碼 |
| 1058 | SK_ERROR_WITHDRAWINOUT_TYPE_OUT_OF_RANGE | 出入金互轉類別錯誤 |
| 1059 | SK_ERROR_CURRENCY_OUT_OF_RANGE | 幣別錯誤 |
| 1060 | SK_ERROR_AP_GW_SERVER_INVALID | AP GW主機位址錯誤 |
| 1061 | SK_ERROR_STRATEGY_ORDER_BUYSELL_INVALID | 證券智慧單買賣別錯誤 |
| 1062 | SK_ERROR_STRATEGY_ORDER_SMARTKEY_TYPE_INVALID | 證券智慧單號錯誤 |
| 1063 | SK_ERROR_STRATEGY_ORDER_MARKET_TYPE_INVALID | 證券智慧單市場代碼錯誤 |
| 1064 | SK_ERROR_STRATEGY_ORDER_TRADE_TYPE_INVALID | 證券智慧單單別錯誤 |
| 1065 | SK_ERROR_CORRECT_PRICE_ONLY_LMT_RANGE | 海期改價僅限限價單委託 |
| 1066 | SK_ERROR_TF_OFFSET_COMMODITY_OUT_OF_RANGE | 期貨互抵商品別無效 / (限定為0或1) |
| 1067 | SK_ERROR_SPECIAL_TRADE_TYPE_OUT_OF_RANGE | (逐筆交易)委託價格類型有誤 |
| 1068 | SK_ERROR_SPECIAL_TRADE_TYPE_IS_MARKETPRICE_AND_ORDERPRICE_SHOULD_BE_ZERO | (逐筆交易)凡市價單之委託價應為0 |
| 1069 | SK_ERROR_MARKET_TYPE_INVALID | 市場別有誤 |
| 1070 | SK_ERROR_SUB_CERT_DIDNOT_BELONG_TO_LOGINID_AP | 雙因子登入相關 / 子帳不屬於該AP群組帳號 |
| 1071 | SK_ERROR_CORRECT_PRICE_ONLY_LMT_RANGE | 海期改價單委託類型限LMT |
| 1072 | SK_ERROR_SGX_DMA_IS_NOT_ALLOW_CORRECTPRICE_BYBOOKNO | SGX專線無書號改價 |
| 1073 | SK_ERROR_SGX_API_ORDER_SEQNO_ERROR | SGX專線改價-委託序號錯誤 |
| 1074 | SK_ERROR_QUERY_AGREEMENT | 商品或功能需要同意書 / 但同意書狀態未簽暑 |
| 1075 | SK_ERROR_ALL_AGREEMENT_SIGNED | 全部同意書已簽署，停止執行查詢 |
| 1076 | SK_ERROR_PROFITLOSSGWQUERY_DATE_INVALID | 損益查詢日期有誤 |
| 1077 | SK_ERROR_PROFITLOSSGWQUERY _BOOKNO_IS_INVALID | 損益查詢書號有誤 |
| 1078 | SK_ERROR_PROFITLOSSGWQUERY_SEQNO_IS_INVALID | 損益查詢序號有誤 |
| 1079 | SK_ERROR_LOGIN_WITHOUT_PASSWORD | 登入未輸入個人密碼 |
| 1080 | SK_ERROR_LOGIN_WITHOUT_LOGINID | 登入未輸入登入ID帳號 |
| 1081 | SK_ERROR_LOGIN_WITHOUT_SETQUOTE | 登入未設定開啟行情功能 |
| 1082 | SK_ERROR_THERE_IS_NO_OVERSEA_FUTURE_ACCOUNT | 登入ID查無海期帳號 |
| 1083 | SK_ERROR_MOVE_POINT_IS_INVALID | 移動點數有誤 |
| 1084 | SK_ERROR_TRADE_KIND_IS_INVALID | 無效委託類別 |
| 1085 | SK_ERROR_PRICE_TYPE_IS_INVALID | 無效價格型別 |
| 1086 | SK_ERROR_STRATEGY_OUTORDER_PRICE_TYPE_INVALID | 無效的出場單委託價格類別 |
| 1087 | SK_ERROR_DATE_TYPE_IS_INVALID | 無效的日期格式 |
| 1088 | SK_ERROR_STRATEGY_ORDER_PRIME_TYPE_INVALID | 無效的股票市場類別 |
| 1089 | SK_ERROR_TRIGGER_PRICE_IS_EQUAL_TO_DEAL | 指定觸發價等於成交價格無法觸發,請改觸發條件 |
| 1090 | SK_ERROR_SEQNO_IS_INVALID | 委託序號有誤 |
| 1091 | SK_ERROR_BOOKNO_IS_INVALID | 委託書號有誤 |
| 1092 | SK_ERROR_ORDER_TYPE_IS_INVALID | 委託別有誤(EX:現股等) |
| 1093 | SK_ERROR_TRIGGER_DIRECTION_IS_INVALID | 指定觸價方向有誤 |
| 1094 | SK_ERROR_TRIGGER_BASE_IS_INVALID | 無效觸價基準；或未給觸價基準 |
| 1095 | SK_ERROR_QUOTE_CONNECT_FIRST | 報價尚未連線，請先連線 |
| 1096 | SK_ERROR_TRIGGER_METHOD_IS_INVALID | 選擇之觸發方式有誤 |
| 1097 | SK_ERROR_TELNET_LOGINSERVER_FAIL | Telnet登入主機失敗，請確認您的環境 / (Firewall及hosts…等) |
| 1098 | SK_ERROR_TELNET_AGREEMENTSERVER_FAIL | Telnet同意書查詢主機失敗，請確認您的環境 / (Firewall及hosts…等) |
| 1099 | SK_ERROR_LOGUPLOAD_FAIL | 上傳LOG失敗 |
| 1100 | SK_ERROR_STRATEGYORDER_OUT_CONDICTION_INVALID | 智慧單:未選擇至少一個出場條件 |
| 1101 | SK_ERROR_CANCEL_ORDER_FIELD_INVALID | 智慧單刪單欄位值無效 |
| 1102 | SK_ERROR_ORDER_FIELD_INVALID | 智慧單出場條件或設定值無效 |
| 1103 | SK_ERROR_AP_APH_GENERATEKEY_INVALID_BEFORE_LOGIN | ［群組身份］ / 雙因子登入相關 / AP_APH GenerateKey初使失敗或未在登入前執行SKCenterLib_GenerateKeyCert |
| 1104 | SK_ERROR_CANNOT_GET_DID_FROM_REGISTRY |  |
| 1105 | SK_ERROR_PRODUCT_INVALID_IN_SGXTABLE | （SGX DMA專線用） / 因專線商品清單查無對應商品，請確認該商品代碼是否有效或洽交易室確認是否可網路交易 |
| 1106 | SK_ERROR_GET_TANDEMINFO_FAIL_SRVKLINE | 無法轉後台商品代碼等資料， / 可先確認委託商品代碼是否正確 |
| 1107 | SK_ERROR_LIMIT_NEAR_BY_YEARMONTH | 限制近月商品代碼 / EX: TX00、 MTX00 |
| 1108 | SK_ERROR_TELNET_SERVER_FAIL | 因網路異常，無法連線 |
| 1109 | SK_ERROR_THERE_IS_NO_TOKEN | 因登入未取得時效token，請重新登入 |
| 1110 | SK_ERROR_FOREIGNSTOCK_ORDERTYPE_INVALID | 複委託下單或刪單時，請填入委託類別：買、賣或刪單 |
| 1111 | SK_ERROR_FOREIGNSTOCK_TRADETYPE_INVALID | 複委託下單，請填入下單庫存類別 |
| 1112 | SK_ERROR_QUERY_FORMAT_INVALID | 需指定查詢格式 / EX:新國內未平倉功能GW需指定格式 |
| 1121 | SK_ERROR_STRATEGY_ORDER_PRERISK_INVALID | 無效預風控設定 |
| 1124 | SK_ERROR_BEST_QTY_IS_INVALID | MIT多條件單，內外盤委託量條件時未給內外盤委託量 |
| 1125 | SK_ERROR_TRIGGER_CONDITION_IS_INVALID | 無效觸價條件；或未給觸價條件 |
| 1126 | SK_ERROR_EXCHANGE_ID_IS_INVALID | 查無商品交易所代碼 |
| 1127 | SK_ERROR_QUERY_IS_OVER_TEN_TIMES | 一個account在1分鐘內不可超過10次 |
| 1128 | SK_ERROR_CERT_INITIALIZE_FAIL | WebCA憑證_初始失敗 |
| 1129 | SK_ERROR_LOGIN_FAIL_IN_PROCESSING | 登入失敗，請稍等五秒後再試 |
| 2001 | SK_WARNING_OF_COM_DATA_MISSING | 海期交易商品檔下載失敗 |
| 2002 | SK_WARNING_TS_READY |  |
| 2003 | SK_WARNING_LOGIN_ALREADY | ID已登入，無需重覆登入。 |
| 2004 | SK_WARNING_LOGIN_SPECIAL_ALREADY |  |
| 2005 | SK_WARNING_CERT_VERIFIED_ALREADY | 驗章已過，無需重覆驗章。 |
| 2006 | SK_WARNING_ORDER_DID_NOT_LOCKED | 下單市埸未上鎖。 |
| 2007 | SK_WARNING_OO_COM_QUOTEDATA_MISSING | 海選商品檔下載失敗 |
| 2008 | SK_WARNING_OO_COM_ORDERDATA_MISSING | 海選交易商品檔下載失敗 |
| 2009 | SK_WARNING_SIGN_TS_SMARTTRADE_RISK_NOITICE_FIRST | 提醒簽署證券智慧單風險預告書 |
| 2010 | SK_WARNING_SIGN_TF_SMARTTRADE_RISK_NOITICE_FIRST | 提醒簽署期貨智慧單風險預告書 |
| 2011 |  |  |
| 2012 | SK_WARNING_OSQUOTECENTER_IS_NOT_EXIST | 下單:下載海期/海選元件不存在 |
| 2013 | SK_WARNING_INITIALIZE_OSQUOTECENTER_CONNECTION_FAIL | 下單:下載海期商品檔元件連線失敗 |
| 2014 | SK_WARNING_INITIALIZE_OSQUOTECENTER_OO_CONNECTION_FAIL | 下單:下載海選商品檔元件連線失敗 |
| 2015 | SK_WARNING_DOWNLOAD_OF_COM_DATA_IS_TIMEOUT | 下單:下載海期商品檔未完成 |
| 2016 | SK_WARNING_DOWNLOAD_OO_COM_DATA_IS_TIMEOUT | 下單:下載海選商品檔未完成 |
| 2017 | SK_WARNING_REGISTER_REPLYLIB_ONREPLYMESSAGE_FIRST | 請（註冊）接收公告再登入 / 請參考單元3-2 |
| 2018 | SK_WARNING_SIGN_STOCK_OR_FUTURE_API_AGREEMENT_FIRST | 1.確認為證券或期貨網路戶 / 2.確認未簽署證券API下單聲明書或期貨API下單聲明書 / 3.或無法取得聲明書狀態，EX:確認Internet設定是否支援 TLS1.2） |
| 2019 | SK_WARNING_SIGN_FUTURE_API_AGREEMENT_FIRST | 1.確認是否為期貨網路戶 / 2.確認未簽署期貨API下單聲明書 / 3.或無法取得聲明書狀態，EX:確認Internet 設定是否支援TLS1.2） |
| 2020 | SK_WARNING_PRECHECK_RESULT_FAIL | 取得行情主機或回報主機連線資訊失敗(EX:RCode) |
| 2021 | SK_WARNING_PRECHECK_RESULT_EMPTY | 取得行情主機或回報主機連線資訊結果為空值 |
| 2022 | SK_WARNING_MORDER_STOP_SERVICE | 停止模擬平台服務通知 |
| 2023 | SK_WARNING_QUOTE_MUST_SKQUOTELIB_ENTERMONITORLONG_FIRST | 請先執行國內報價SKQuoteLib_EnterMonitorLONG連線，再使用目前功能 |
| 2024 | SK_WARNING_QUOTE_MUST_SKQUOTELIB_ENTERMONITOR_FIRST | 請先執行國內報價SKQuoteLib_EnterMonitor連線，再使用目前功能 |
| 2025 | SK_WARNING_OSQUOTE_MUST_SKOSQUOTELIB_ENTERMONITORLONG_FIRST | 請先執行海期報價SKOSQuoteLib_EnterMonitorLONG連線，再使用目前功能 |
| 2026 | SK_WARNING_OOQUOTE_MUST_SKOOQUOTELIB_ENTERMONITORLONG_FIRST | 請先執行海選報價 / SKOOQuoteLib_EnterMonitorLONG連線，再使用目前功能 |
|  |  |  |
| 2028 | SK_WARNING_SHOULD_IGNORE_GENERATEKEYCERT_STEP | 雙因子登入相關 / 目前身份不需執行此功能-雙因子群組GenerateKey |
| 2029 | SK_WARNING_SHOULD_CHECK_NETWORK |  |
| 2030 | SK_WARNING_SHOULD_FILLED_YEARMONTH_WHEN_NOT_NEAR_MONTH | (非近月) / 指定月份商品需填入商品契約年月共6碼 |
| 3001 | SK_SUBJECT_CONNECTION_CONNECTED | 連線 |
| 3002 | SK_SUBJECT_CONNECTION_DISCONNECT | 斷線 |
| 3003 | SK_SUBJECT_CONNECTION_STOCKS_READY | 報價商品載入完成 |
| 3004 | SK_SUBJECT_CONNECTION_CLEAR |  |
| 3005 | SK_SUBJECT_CONNECTION_RECONNECT |  |
| 3006 | SK_SUBJECT_QUOTE_PAGE_EXCEED | PageNo 大於上限 |
| 3007 | SK_SUBJECT_QUOTE_PAGE_INCORRECT |  |
| 3008 | SK_SUBJECT_TICK_PAGE_EXCEED | Tick PageNo 大於上限 |
| 3009 | SK_SUBJECT_TICK_PAGE_INCORRECT |  |
| 3010 | SK_SUBJECT_TICK_STOCK_NOT_FOUND | Tick 商品不存在 |
| 3011 | SK_SUBJECT_BEST5_DATA_NOT_FOUND | 商品五檔不存在 |
| 3012 | SK_SUBJECT_QUOTEREQUEST_NOT_FOUND |  |
| 3013 | SK_SUBJECT_SERVER_TIME_NOT_FOUND |  |
| 3015 | SK_SUBJECT_ALL_MARKET_OK |  |
| 3016 | SK_SUBJECT_MARKET_INFO_READY |  |
| 3017 | SK_SUBJECT_CATALOG_LIST_READY |  |
| 3018 | SK_SUBJECT_INITIALIZESTOCKS |  |
| 3019 | SK_SUBJECT_MACD_DATA_NOT_FOUND | MACD不存在 |
| 3020 | SK_SUBJECT_BOOLTUNEL_DATA_NOT_FOUND | BOOL通道不存在 |
| 3021 | SK_SUBJECT_CONNECTION_FAIL_WITHOUTNETWORK | 連線失敗(網路異常等) |
| 3022 | SK_SUBJECT_CONNECTION_SOLCLIENTAPI_FAIL | Solace底層連線錯誤 |
| 3023 | SK_SUBJECT_STOCKNO_IS_INVALID | 商品代碼無效 |
| 3024 | SK_SUBJECT_MARKET_NO_IS_OUT_OF_RANGE | 國內市場代碼超出範圍 (0~4) |
| 3025 | SK_SUBJECT_CANT_ACCEPT_SPREAD_COMMODITY | 不可委託價差商品 |
| 3026 | SK_SUBJECT_CONNECTION_SGX_API_READY | SGX API專線建立完成 |
| 3027 | SK_SUBJECT_TICK_LIMIT_EXCEED | 超過可訂閱TICK_Best5_Best10商品檔數 |
| 3028 | SK_SUBJECT_QUOTE_LIMIT_EXCEED_IN_ONE_PAGE | 超過可訂閱單頁即時報價商品檔數 |
| 3029 | SK_SUBJECT_QUOTE_STRING_EXIST_NULL | 查詢即時報價含空白、空值 |
| 3030 | SK_SUBJECT_NO_QUOTE_SUBSCRIBE | 行情連線超過限制時，無法訂閱行情通知 |
| 3031 | SK_SUBJECT_NO_RELATED_MARKET_STOCKS | 未下載相關市場商品基本資料 / (原因:可確認證券或期貨下單聲明書簽署狀態) |
| 3032 | SK_SUBJECT_INITIALIZESTOCKS_FAIL |  |
| 3033 | SK_SUBJECT_SOLACE_SESSION_EVENT_ERROR | Solace Session down錯誤 / (因AP與主機連線異常) |
|  |  |  |
| 4001 | SK_KLINE_DATA_TYPE_NOT_FOUND | KLINE TYPE 超出選擇範圍。 |
|  |  |  |
| 151 | SK_ERROR_LOGIN_WRONG_PASSWORD | 僅適用V2.13.43(含)以下版本 / 密碼錯誤 |
| 152 | SK_ERROR_LOGIN_WRONG_PASSWORD_OVER_LIMIT | 僅適用V2.13.43(含)以下版本 / 密碼輸入錯誤次數超過上限 / 請至群益金融網-密碼專區解鎖 |
| 153 | SK_ERROR_LOGIN_WRONG_ID | 僅適用V2.13.43(含)以下版本 / 您輸入的資料錯誤！請輸入正確的身份證字號 |
| 155 | SK_ERROR_CHANGE_PASSWORD_IN_FIRST_TIME | 僅適用V2.13.43(含)以下版本首次使用，請先更改密碼 / （可使用群益策略王更改） |
| 156 | SK_ERROR_LOGIN_NO_SERVER_LIST | 僅適用V2.13.45(含)以上版本 / GW)未能取得主機清單 |
| 157 | SK_ERROR_LOGIN_NO_ACCONTS | 僅適用V2.13.45(含)以上版本 / 未能取得交易帳號 |
| 158 | SK_ERROR_LOGIN_NO_SPEC_AUTHORITY | 僅適用V2.13.45(含)以上版本 / 未能取得特殊權限（SGX 密碼權限、DMA etc.） |
| 500~599 | 說明僅適用V2.13.43(含)以前版本 / 請先確認是否安裝「有效憑證」，可至群益金融網->憑證專區確認及申請。 / https://www.capital.com.tw/web/#/certificate/ap / *新申請憑證，可能需使用RAWinApp.exe開通 |
| 9996 | SK_ERROR_UPDATE_API_REQUIRED | 此版本已無法登入，請更新版本。 |
| 9997 | SK_ERROR_LOGIN_FAIL_LIMIT | 登入失敗已達五次，請重啟API。 |
| 9998 | SK_THIS_FUNCTION_NOT_SUPPORTED | 此函式目前不提供 |
| 9999 | SK_FAIL | *報價部分 / 若您未開立證券或期貨帳戶，無法訂閱或取得相關市場商品資料。 / 若您未簽署證券API下單聲明書或期貨API下單聲明書，將無法訂閱或取得相關市場商品資料。 |
|  |  |  |
|  |  |  |
| Proxy Server 下單錯誤代碼 |
| 5001 | SK_PROXY_SERVER_LOGIN_SUCCESS | Proxy handler連線中且狀態為登入成功 |
| 5002 | SK_PROXY_SERVER_LOGIN_FAIL | Proxy handler連線中且狀態為登入失敗 / 請重新連線ProxyReconnectByID |
| 5003 | SK_PROXY_SERVER_DISCONNECT | Proxy handler斷線中 / 請重新連線 / ProxyReconnectByID |
| 5004 | SK_PROXY_SERVER_SCHEDULE_DIALY_DISCONNECT | Proxy SERVER送出每日斷線通知 / 請等待1分鐘後重新連線 / ProxyReconnectByID |
| 5005 | SK_PROXY_SERVER_SWITCH_MODE | Proxy SERVER送出切轉主機通知 |
| 5006 | SK_PROXY_SERVER_HANDLER_IS_EXIST | SKOrderLib_InitialProxyByID時發生該ID連線HANDLER已存在 / 請重新連線 / ProxyReconnectByID |
| 5009 | SK_PROXY_SERVER_LOGIN_SEND_DATA_SUCCESS | Proxy handler已連線且登入，等待Server回傳通知5001 / ＊請確認收到5001再送proxy下單 |
| 5010 | SK_PROXY_SERVER_CONNECT_SUCCESS | Proxy handler連線中尚未登入 / 需斷線ProxyDisconnectByID成功，再重新連線ProxyReconnectByID |
| 5011 | SK_PROXY_SERVER_CONNECT_FAIL | Proxy handler連線失敗 / 請重新連線 / ProxyReconnectByID / 或初使化 / SKOrderLib_InitialProxyByID |
| 5012 | SK_PROXY_SERVER_DISCONNECT_FAIL | Proxy handler斷線失敗 |
| 5013 | SK_PROXY_LOAD_PROXYLIB_FAIL | 載入ProxyLIB DLL失敗 / 請先確認Proxy初使化 / SKOrderLib_InitialProxyByID |
| 5014 | SK_PROXY_SERVER_CONNECT_FAIL_WITHOUT_HANDLER | Proxy handler連線失敗 / 請重新連線 / ProxyReconnectByID |
| 5015 | SK_PROXY_INITIALIZE_FAIL | Proxy 初使化失敗 / 請重新連線 / ProxyReconnectByID / 或初使化 / SKOrderLib_InitialProxyByID |
| 5016 | SK_PROXY_ERROR_THE_ID_IS_CONNECTED_ALREADY | 重新連線時發生重覆連線通知。 / 目前Proxy handler連線中且狀態為登入成功。 |
| 5017 | SK_PROXY_ERROR_THE_ID_IS_DISCONNECTED_ALREADY | 斷線時，發生重覆斷線通知 |
| 5018 | SK_PROXY_SEND_ORDER_FAIL | 送出ProxyOrder異常 / 請確認SKOrderLib event : OnProxyStatus 為SK_PROXY_SERVER_LOGIN_SUCCESS / 並重新連線 / ProxyReconnectByID |
| 5019 | SK_PROXY_SERVER_ACCOUNT_RESTRICTED | 此帳號已被限制連線，請洽詢營業員 |

V2.13.45 版本登入失敗錯誤代碼(由密碼平台回覆)
| 代碼 | 說明 | 處理方式 |
|---|---|---|
| 101 | 登入token異常 | 請重新登入 |
| 300 | 密碼驗證失敗 | 您輸入的密碼錯誤，請重新輸入！ |
| 306 | 身分證字號錯誤 | 您輸入的身份證字號錯誤，請重新輸入！ |
| 307 | 密碼被鎖定 | 請至群益金融網-密碼專區解鎖 |
| 321 | 測試未完成：請先至API下載專區 完成測試後再進行下一步。 | 至API下載專區，下載驗證小工具，完成驗證測試 |
| 502 | [特殊身份]帳戶錯誤 | 群組身份欲指定子帳錯誤 |
| 507 | 驗證裝置碼失敗 | 無效裝置碼或未綁定裝置碼，可透過策略王綁定。 |
| 511 | [特殊身份]群組未申請權限 | AP群組帳號登入異常:請確認是否有申請登入權限 |
| 600 | 驗證憑證錯誤 | 請確認是否安裝「有效憑證」 / 若電腦同時存在二張憑證，請先刪除無效或過期憑證 |
| 602 | 雙因子登入失敗 | 請確認是否安裝登入ID有效憑證 |
| 603 | 雙因子登入失敗(無第二因子) | 請確認是否安裝登入ID有效憑證 |
| 604 | 憑證過期或已註銷 | 請至群益金融網-憑證專區申請 |

strErrorMsg
7、Microsoft其他錯誤代碼定義
當您在登入、下單、讀取同意書發生的錯誤，其代碼無法自
第6代碼定義表確認時,可至以下link確認原因：
Windows Sockets Error Codes  <997;10004~10112;11001~11031
https://docs.microsoft.com/zh-tw/windows/win32/winsock/windows-sockets-error-codes-2?redirectedfrom=MSDN
WinINet functions return error codes 12002~12054;12111~12174
https://docs.microsoft.com/zh-tw/windows/win32/wininet/wininet-errors
Microsoft Windows HTTP error code服務失敗
https://docs.microsoft.com/zh-tw/windows/win32/winhttp/error-messages
Appendix A. 註冊API時出現的錯誤碼及處理
Microsoft Windows Server 使用RegSvr32 說明:
當Windows Server註冊dll出現以下訊息:
無法載入模組：
RegSvr32 The module "SKCOM.dll" failed to load. Make sure the binary is stored at the specified path or debug it to check for problems with the binary or dependent .DLL files.The specified module could not be found.
Step 1請確定註冊SKCOM.dll 時，以下相關憑證等(＊.dll) DLL檔案是否完整。
若上述檔案列表與您的資料夾相同，再進行▼step 2.
Step 2請注意是否遺失或缺少mfc100u.dll，並安裝下面所列套件
▼請依微軟Microsoft Visual C++ 可轉散發套件版本為主，下載URL僅供參考
| Microsoft Visual C++ 2010 Service Pack 1 MFC 可轉散發套件的安全性更新 / https://www.microsoft.com/zh-tw/download/details.aspx?id=26999 / Visual C++2015、2017 和 2019 共用相同的可轉散發檔案https://www.visualstudio.com/downloads/ / https://aka.ms/vs/16/release/vc_redist.x86.exe / https://aka.ms/vs/16/release/vc_redist.x64.exe / Microsoft Visual C++可轉散發套件的最新支援下載 (2022/5/11) / 最新支援的C++ 可轉散發套件下載 \| Microsoft Docs |
|---|

x86位元:直接註冊即可
x64位元:透過SysWow64的regsvr32.exe註冊
EX:Windows Server 2012 R2系統註冊成功畫面
呼叫DllRegisterServer失敗:
錯誤碼＿0x8002801c：要用Administrator身分（即系統管理員身分）登入電腦後註冊API
錯誤碼＿0x80070005：執行install.bat要按右鍵以系統管理員身分執行註冊API（如下圖）
Microsoft 如何使用 Regsvr32 工具和疑難排解
Regsvr32 錯誤訊息https://0rz.tw/Fd9NF
