# 策略王COM元件使用說明_ProxyServer下單元件

> 來源：Source_code/CapitalAPI_2.13.59_CExample/策略王COM元件使用說明_ProxyServer下單元件.docx

Proxy Server下單
文件版本：V2.13.47
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
| 海期價差委託 | SendOverseaFutureSpreadProxyOrder |  |
|  |  |  |
| 斷線 | ProxyDisconnectByID | 主動斷線proxy server |
| 重新連線 | ProxyReconnectByID | 重新連線proxy server |

事件
| 功能 | 事件名稱 | 備註 |
|---|---|---|
| Proxy委託結果 | OnProxyOrder |  |
| 連線、登入狀態 | OnProxyStatus |  |

4-7-1　 SKOrderLib_InitialProxyByID
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
| 回傳值 | 0表示連線成功，其餘非0數值都表示失敗。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-5　 SendStockProxyAlter
| 經由proxy server送出證券刪改單。 |
|---|
| 宣告 | long SendStockProxyAlter([in]BSTR bstrLogInID, [in]struct STOCKPROXYORDER* pSTOCKPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSTOCKPROXYORDER | SKCOM元件中的STOCKPROXYORDER 物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-6　SendFutureProxyOrderCLR
| 經由proxy server送出期貨委託，需設倉別與盤別。 |
|---|
| 宣告 | Long SendFutureProxyOrderCLR ([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-7　 SendFutureProxyAlter
| 經由proxy server送出期貨刪改單。 |
|---|
| 宣告 | Long SendFutureProxyAlter ([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-8　 SendOptionProxyOrder
| 經由proxy server送出選擇權委託 |
|---|
| 宣告 | Long SendOptionProxyOrder([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-9　 SendOptionProxyAlter
| 經由proxy server送出選擇權刪改單。 |
|---|
| 宣告 | Long SendOptionProxyAlter([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-10　SendDuplexProxyOrder
| 經由proxy server送出選擇權複式下單。 |
|---|
| 宣告 | Long SendDuplexProxyOrder ([in]BSTR bstrLogInID, [in]struct FUTUREPROXYORDER* pFUTUREPROXYORDER, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pFUTUREPROXYORDER | SKCOM元件中的FUTUREPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-11 SendOverseaFutureProxyOrder
| 經由proxy server送出海期下單。 |
|---|
| 宣告 | Long SendOverseaFutureProxyOrder([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-12 SendOverseaFutureSpreadProxyOrder
| 經由proxy server送出海期價差單下單。 |
|---|
| 宣告 | Long SendOverseaFutureSpreadProxyOrder ([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-13 SendOverseaFutureProxyAlter
| 經由proxy server送出海期選刪改單。 |
|---|
| 宣告 | Long SendOverseaFutureProxyAlter([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將刪單或改單條件填入該物件後，再帶入此欄位中。 / Proxy 改單功能物件說明 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-14 SendOverseaOptionProxyOrder
| 經由proxy server送出海選下單。 |
|---|
| 宣告 | Long SendOverseaOptionProxyOrder ([in] BSTR bstrLogInID, [in] struct OVERSEAFUTUREORDER* pSKProxyOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pSKProxyOrder | SKCOM元件中的OVERSEAFUTUREORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-15 SendForeignStockProxyOrder
| 經由proxy server送出複委託下單。 |
|---|
| 宣告 | Long SendForeignStockProxyOrder([in] BSTR bstrLogInID,  [in] struct OSSTOCKPROXYORDER* pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pAsyncOrder | SKCOM元件中的OSSTOCKPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 透過Proxy Server委託，委託結果請由OnProxyOrder取得。 / 連線且成功登入，通知OnProxyStatus為5001時，送至proxy server進行下單。 |

4-7-16 SendForeignStockProxyCancel
| 經由proxy server送出複委託刪單。 |
|---|
| 宣告 | Long SendForeignStockProxyCancel([in] BSTR bstrLogInID,  [in] struct OSSTOCKPROXYORDER * pAsyncOrder, [out] BSTR* bstrMessage); |
| 參數 | bstrLogInID | 登入ID。 |
|  | pAsyncOrder | SKCOM元件中的OSSTOCKPROXYORDER物件，將下單條件填入該物件後，再帶入此欄位中。 |
|  | bstrMessage | 委託送出成功：ORKEY / 委託送出失敗：錯誤訊息 |
| 回傳值 | 0表示成功，其餘非0數值都表示失敗。錯誤代碼可參考對照表。 |
| 備註 | 使用非同步委託，委託結果請由OnAsyncOrder取得。 / 在有連上且成功登入proxy server的狀態下，會由proxy server進行非同步下單。 |

4-7-a OnProxyOrder
| Proxy委託結果。 |
|---|
| 宣告 | void OnProxyOrder([in] LONG nCode, [in] BSTR bstrMessage  ); |
| 參數 | nStampID | StampID |
|  | nCode | Proxy收單結果代碼。 |
|  | bstrMessage | Proxy收單回傳訊息。 |
| 備註 | 送出委託後會回傳二筆通知 |

*v2.13.47異動：補回傳參數nStampID文字說明
4-7-b OnProxyStatus
| 一個使用者id會與proxy server建一條連線，此事件回傳此條連線的連線狀態。 / 透過呼叫 SKOrderLib_Initialize 後，資訊由該事件回傳。 |
|---|
| 宣告 | void OnProxyStatus ([in] BSTR bstrUserId, [in]LONG nCode); |
| 參數 | bstrUserId | 回傳內容：使用者id |
|  | nCode | 回傳內容： / 請參考代碼定義表5001~5006、5011 |
| 備註 |  |

5、Struct 結構物件
5-1 OVERSEAFUTUREORDER( 海外期權下單物件 )
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
SHORTsSpecialTradeType;//0:LMT 限價單 1:MKT  2:STL  3.STP
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
SHORTsSpecialTradeType;//0:LMT 限價單 1:MKT  2:STL  3.STP
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
SHORTsSpecialTradeType;//0:LMT 限價單 1:MKT  2:STL  3.STP
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
SHORTsSpecialTradeType;//0:LMT 限價單 1:MKT  2:STL  3.STP
LONGnQty;//交易口數。
BSTRbstrBookNo;//書號。
BSTRbstrSeqNo;//原始13碼序號。
LONG nSpreadFlag; // 0 :OF海期 ; 1: OF-spread 海期價差;  2: OO 海選
LONG nAlterType;         //0: Cancel 刪單;1: Decrease 減量; 2: Correct 改價
}
;
5-2 FUTUREORDER ( 期權下單物件 )
5-2-4 Proxy Server 期選
Proxy Server期貨下單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;   //期貨帳號，分公司四碼＋帳號7碼
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
Proxy Server選擇權下單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;   //期貨帳號，分公司四碼＋帳號7碼
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
Proxy Server選擇權複式單
struct FUTUREPROXYORDER
{
BSTRbstrFullAccount;   //期貨帳號，分公司四碼＋帳號7碼
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
BSTRbstrFullAccount;    //期貨帳號，分公司四碼＋帳號7碼
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
5-3-3 Proxy Server複委託物件
Proxy Server複委託下單
struct OSSTOCKPROXYORDER
{
BSTRbstrFullAccount;//複委託帳號，分公司代碼＋帳號7碼
BSTRbstrStockNo;//委託股票代號
BSTRbstrExchangeNo;//交易所代碼，US：美股， HK：港股，JP：日股， SP：新加坡，SG：新(幣)坡股，HA: 滬股，SA: 深股
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
6、代碼定義表
| 代號 | 名稱 | 說明 |
|---|---|---|
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

