# 策略王COM元件使用說明_期貨新制商品報價元件

> 來源：Source_code/CapitalAPI_2.13.59_CExample/策略王COM元件使用說明_期貨新制商品報價元件.docx

期貨新制商品報價
文件版本：V2.13.45
4-4 SKQuoteLib (期貨新制報價異動函式)
函式
| 功能 | 函式名稱 | 備註 |
|---|---|---|
| 國內商品清單查詢 | SKQuoteLib_RequestStockList | 盤中零股上市5、盤中零股上櫃 6 / 客製化期貨-9、客製化選擇權-10 |
| 取得商品物件 | SKQuoteLib_GetStockByIndexLONG | LONG index 對應 |
| 訂閱指定市場別指定商品 | SKQuoteLib_RequestStocksWithMarketNo |  |
| 成交明細與五檔資料 | SKQuoteLib_RequestTicksWithMarketNo |  |

4-4-17 SKQuoteLib_RequestStockList
| 根據市場別編號，取得國內各市場代碼所包含的商品基本資料相關資訊。 |
|---|
| 宣告 | Long SKQuoteLib_RequestStockList([in] SHORT sMarketNo); |
| 參數 | sMarketNo | 市場別代碼。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | OnNotifyStockList |
| 備註 | ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，再進行商品檔查詢。 / 市場編號分別為： / 上市 0、上櫃 1、期貨 2、選擇權 3、興櫃 4、 / 盤中零股-上市5、盤中零股-上櫃6 / 客製化期貨-9、客製化選擇權-10 / 若未開立證券帳戶或未簽署證券API下單同意書，將無法查詢 / ”上市 0、上櫃 1、興櫃 4”、盤中零股-上市5、盤中零股-上櫃6。（錯誤代碼3031） / 若未開立期貨帳戶或未簽署期貨API下單同意書，將無法查詢”期貨 2、選擇權 3”。（錯誤代碼3031） |

4-4-26 SKQuoteLib_GetStockByIndexLONG
| 請先訂閱即時報價(SKQuoteLib_ReqeustStocks),方可取得商品報價 / 未訂閱即時報價,僅可取得商品基本資料 / (LONG index)根據市場別編號與系統所編的索引代碼，取回商品報價的及商品相關資訊。 |
|---|
| 宣告 | Long SKQuoteLib_GetStockByIndexLONG([in] SHORT sMarketNo, [in] LONG nIndex, [in,out] struct SKSTOCKLONG* pSKStock); |
| 參數 | sMarketNo | 市場別代碼。 |
|  | nIndex | 系統所編的索引代碼。 |
|  | pSKStock | SKCOM元件中的 SKSTOCKLONG物件，將物件帶入此欄位中。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 市場編號分別為：與SKSTOCK物件內之BSTRbstrMarketNo 型態不同。 / ＊須等收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行。 / ＊須使用SKQuoteLib_EnterMonitorLONG登入，方可執行此函式。 / ＊依國內或海外市場，可能需搭配MarketNo / ＊未訂閱即時報價，EX:只先訂成交明細SKQuoteLib_ReqeustTicks時，執行此功能，僅可取得商品基本資料(商品名稱、昨收價，非即時性質欄位) |

4-4-32 SKQuoteLib_RequestStocksWithMarketNo
| 訂閱指定市場別及指定商品即時報價 / 要求伺服器針對sMarketNo市場別、 bstrStockNos 內的商品代號訂閱商品報價通知動作。 |
|---|
| 宣告 | Long SKQuoteLib_RequestStocksWithMarketNo([in,out] SHORT* psPageNo, [in] SHORT sMarketNo, [in] BSTR bstrStockNos); |
| 參數 | psPageNo | psPageNo 每一次送出要求報價的動作都需要向報價伺服器指定一個特定的 Page 編號，伺服器會以編號當作Key值維護每一次送出不同的商品代號。 / 當要變更某一 Page 索取報價的內容，即重複使用相同的 Page 編號，即可取消之前索取的內容，以新的內容取代。當psPageNo=-1時帶入，函式庫會指定一個新的編號，並回傳給呼叫端。 |
|  | sMarketNo | (目前提供) / 盤中零股-上市(5)、盤中零股- 上櫃(6)市場代碼 / 客製化期貨-9、客製化選擇權-10 |
|  | bstrStockNos | 欲訂閱的商品代號，一筆以上的資料時，每檔商品代號以”,”做區隔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 4-4-r OnNotifyQuoteLONG |
| 備註 | 適用盤中零股 / ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品報價。 / *因應檔數限制，一個SKQuoteLib物件，僅可擇一使用一個即時報價訂閱(SKQuoteLib_RequestStocks功能或 SKQuoteLib_RequestStocksWithMarketNo功能)。 / 可重新連線即還原限制與設定。 |

4-4-34 SKQuoteLib_RequestTicksWithMarketNo
| 訂閱要求傳送成交明細以及五檔。 |
|---|
| 宣告 | Long SKQuoteLib_RequestTicksWithMarketNo([in,out] SHORT* psPageNo, [in] SHORT sMarketNo ,[in] BSTR bstrStockNo); |
| 參數 | psPageNo | 每一次送出要求成交明細的動作都需要向報價伺服器指定一個特定的 Page 編號 (請參考RequestStocks觀念說明)。 |
|  | sMarketNo | 盤中零股-上市(5)、盤中零股- 上櫃(6)市場代碼 / 客製化期貨-9、客製化選擇權-10 |
|  | bstrStockNo | 索取的商品代號，一個Page僅能索取一檔。 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 相關通知事件 | 即時Tick由OnNotifyTicksLONG事件通知。 / Tick回補由 OnNotifyHistoryTicksLONG事件通知。 / 最佳五檔由OnNotifyBest5LONG事件通知。 |
| 備註 | 適用盤中零股 / ＊請先SKQuoteLib_EnterMonitorLONG，須等OnConnection收到SK_SUBJECT_CONNECTION_STOCKS_READY後，方可進行訂閱商品成交明細及最佳五檔。 |

