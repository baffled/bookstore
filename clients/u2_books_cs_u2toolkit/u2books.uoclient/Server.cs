using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Xml.Linq;
using System.Drawing;
using System.IO;

using U2.Data.Client;
using U2.Data.Client.UO;
using u2_books.shared;

namespace u2_books
{
    public class Server
    {
        private static volatile Server _instance = null;
        private static Object syncRoot = new Object();

        // regular system markers
        public const String FM_STR = "\xFE";
        public const String VM_STR = "\xFD";
        public const String SVM_STR = "\xFC";

        public const char FM = '\xFE';
        public const char VM = '\xFD';
        public const char SVM = '\xFC';

        public const String CRLF = "\r\n";
        public const String TAB = "\t";
        public const String FF = "\f";
        
        private UniSession _sess = null;
        private U2Connection conn = null;

        private String _lastError = String.Empty;
        private Object _syncCall = new Object();
        private Dictionary<String, UniFile> _openFiles = new Dictionary<string, UniFile>();

        // money formatting
        private int _moneyFormat = u2_const.FORMAT_UK;
        private int _dateFormat = u2_const.DATE_SENSIBLE;

        /// <summary>
        /// Instance: singleton pattern
        /// </summary>
        public static Server Instance {
            get {
                if (_instance == null) {
                    lock (syncRoot) {
                        _instance = new Server();
                    }
                }
                return _instance;
            }
        }

        #region Database connection

        public Boolean connect(string host, String user, String path, String pass, String service, ref String errText) {
            if (service == String.Empty) service = "uvcs";
            conn = new U2Connection();
            U2ConnectionStringBuilder connBuilder = new U2ConnectionStringBuilder();
            connBuilder.Server = host;
            connBuilder.UserID = user;
            connBuilder.Password = pass;
            connBuilder.Database = path;
            connBuilder.ServerType = "UNIVERSE";
            connBuilder.AccessMode = "Native";   // FOR UO
            connBuilder.RpcServiceType = service; // FOR UO
            connBuilder.Pooling = false;

            try {
                conn.ConnectionString = connBuilder.ToString();
                conn.Open();
                _sess = conn.UniSession;

            } catch (Exception ex) {
                _sess = null;
                errText = ex.Message;
                return false;
            }
            return true;
        }

        public void disconnect() {
            if(conn != null) {
                conn.Close();
                conn = null;
                _sess = null;
            }            
        }

        #endregion

        #region Database Primitives

       

        public Boolean callSub(string subrName, List<String> args) {
            lock (_syncCall) {
                try {
                    UniSubroutine s = _sess.CreateUniSubroutine(subrName, args.Count);
                    for (int i = 0; i < args.Count; i++) {
                        s.SetArg(i, args[i]);
                    }
                    s.Call();
                    for (int i = 0; i < args.Count; i++) {
                        args[i] = s.GetArg(i);
                    }
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
                return true;
            }
        }

        public Boolean callSub(string subrName, string[] args) {
            lock (_syncCall) {
                try {
                    UniSubroutine s = _sess.CreateUniSubroutine(subrName, args.Length);
                    for (int i = 0; i < args.Length; i++) {
                        s.SetArg(i, args[i]);
                    }
                    s.Call();
                    for (int i = 0; i < args.Length; i++) {
                        args[i] = s.GetArg(i);
                    }
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
                return true;
            }
        }
        public Boolean callSub(string subrName, string indata, out string outdata, out string errtext) {
            lock (_syncCall) {
                try {
                    UniSubroutine s = _sess.CreateUniSubroutine(subrName, 3);
                    s.SetArg(0, indata);
                    s.SetArg(1, string.Empty);
                    s.SetArg(2, string.Empty);
                    s.Call();
                    outdata = s.GetArg(1);
                    errtext = s.GetArg(2);                    
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    outdata = string.Empty;
                    errtext = string.Empty;
                    return false;
                }
                return true;
            }
        }
        public UniDynArray createArray() {
            return _sess.CreateUniDynArray();
        }

        public String getErrorCode(String mess) {
            int ix = mess.IndexOf("[ErrorCode");
            String temp;
            if (ix < 0) {
                return String.Empty;
            }
            temp = mess.Substring(ix + 11);
            ix = temp.IndexOf("]");
            if (ix > 0) {
                temp = temp.Substring(0, ix);
            }
            return temp;
        }

        private UniFile getFile(String fileName) {
            if (_openFiles.ContainsKey(fileName)) {
                return _openFiles[fileName];
            } else {
                if (openFile(fileName)) {
                    return _openFiles[fileName];
                }
            }

            return null;
        }


        public Boolean iconv(String value, String code, ref String newValue) {
            lock (_syncCall) {
                newValue = _sess.Iconv(value, code);
                return (_sess.Status == 0);
            }
        }
        public Boolean oconv(String value, String code, ref String newValue) {
            lock (_syncCall) {
                newValue = _sess.Oconv(value, code);
                return (_sess.Status == 0);
            }
        }
        public Boolean openDict(String fileName) {
            _lastError = String.Empty;
            if (_openFiles.ContainsKey("DICT " + fileName)) {
                return true;
            }
            lock (_syncCall) {
                try {
                    UniFile f = _sess.CreateUniDictionary(fileName);
                    _openFiles.Add("DICT " + fileName, f);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
                return true;
            }
        }
        public Boolean openFile(String fileName) {
            _lastError = String.Empty;
            if (_openFiles.ContainsKey(fileName)) {
                return true;
            }
            lock (_syncCall) {
                try {
                    UniFile f = _sess.CreateUniFile(fileName);
                    _openFiles.Add(fileName, f);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }

        public Boolean deleteRecord(String fileName, String id) {
            _lastError = String.Empty;
            UniFile f = getFile(fileName);
            if (f == null) {
                return false;
            }
            lock (_syncCall) {
                try {
                    f.DeleteRecord(id);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }


        public Boolean readRecord(String fileName, String id, ref UniDynArray rec) {
            _lastError = String.Empty;
            UniFile f = getFile(fileName);
            if (f == null) {
                return false;
            }
            lock (_syncCall) {
                try {
                    rec = f.Read(id);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }

        
        public Boolean writeRecord(String fileName, String id, UniDynArray rec) {
            _lastError = String.Empty;
            UniFile f = getFile(fileName);
            if (f == null) {
                return false;
            }
            lock (_syncCall) {
                try {
                    f.Write(id, rec.StringValue);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }

        public Boolean lockRecord(String fileName, string id) {
            _lastError = String.Empty;
            UniFile f = getFile(fileName);
            if (f == null) {
                return false;
            }
            lock (_syncCall) {
                try {
                    f.LockRecord(id, 1);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }

        public Boolean releaseRecord(String fileName, string id) {
            _lastError = String.Empty;
            UniFile f = getFile(fileName);
            if (f == null) {
                return false;
            }
            lock (_syncCall) {
                try {
                    f.UnlockRecord(id);
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }

        public Boolean Execute(String cmd) {
            lock (_syncCall) {
                try {
                    UniCommand c = _sess.CreateUniCommand();
                    c.Command = cmd;
                    c.Execute();
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
            }
            return true;
        }
        
        public Boolean Execute(String cmd, ref string result) {
            lock (_syncCall) {
                try {
                    UniCommand c = _sess.CreateUniCommand();
                    c.Command = cmd;
                    c.Execute();
                    result = c.Response;
                } catch (Exception ex) {
                    _lastError = ex.Message;
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region Public Members

        public UniSession Session {
            get { return _sess; }
        }
        public Boolean Connected{
            get{ return (_sess != null);}
        }
        public void ShowError(String text) {
            MessageBox.Show(text);
        }
        #endregion


        #region Application Routines

        /// <summary>
        /// getBookImage: get the book title image
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="image"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public Boolean createPOs(String forDate, ref String POList, ref String errText)
        {
            String imageData = String.Empty;
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_createDailyPOs", 3);
                    s.SetArg(0, forDate);
                    s.Call();
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                    POList = s.GetArg(1);
                }
            }
            catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }


        /// <summary>
        /// getAuthorsAsTable ; get a data table of the author keys and names
        /// </summary>
        /// <remarks>
        /// This uses the UniXML utility class to return an enquiry command as a data table
        /// for easy data binding.
        /// </remarks>
        /// <param name="authors"></param>
        /// <returns></returns>
        public Boolean getAuthorsAsTable(ref DataTable authors) {
            try {
                lock (_syncCall) {
                    UniXML ux = _sess.CreateUniXML();
                    String cmd = BookConst.SEL_AUTHORS_QUERY;
                    ux.GenerateXML(cmd);
                    DataSet ds = ux.GetDataSet();
                    authors = ds.Tables[0];
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;            
        }

        /// <summary>
        /// getPublishersAsTable : get a data table of publisher keys and names
        /// </summary>
        /// <remarks>
        /// See above.
        /// </remarks>
        /// <param name="publishers"></param>
        /// <returns></returns>
        public Boolean getPublishersAsTable(ref DataTable publishers) {
            try {
                lock (_syncCall) {
                    UniXML ux = _sess.CreateUniXML();
                    String cmd = BookConst.SEL_PUBLISHERS_QUERY;
                    ux.GenerateXML(cmd);
                    DataSet ds = ux.GetDataSet();
                    publishers = ds.Tables[0];
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            } 
            return true;
        }

        /// <summary>
        /// getQueryAstTable : run a query and return a data table
        /// </summary>
        /// <param name="query"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public Boolean getQueryAsTable(String query, ref DataTable table) {
            try {
                lock (_syncCall) {
                    UniXML ux = _sess.CreateUniXML();
                    String cmd = query;
                    ux.GenerateXML(cmd);
                    DataSet ds = ux.GetDataSet();
                    table = ds.Tables[0];
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// getBookData: get full set of extended data for a book
        /// </summary>
        /// <!--
        /// Note that prices are held as whole pence/cents. These are descaled locally to ensure they
        /// fit the locale of the PC.
        /// -->
        /// <param name="bookId"></param>
        /// <param name="withLock"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean getBookData(String bookId, Boolean withLock, BookData data) {
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_getBookData", 4);
                    s.SetArg(0, bookId);
                    s.SetArg(1, withLock ? "1" : "0");
                    s.Call();
                    String errText = s.GetArg(3);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                    UniDynArray da = s.GetArgDynArray(2);

                    data.BookId = bookId;
                    data.ShortTitle = da.Extract(BookConst.BOOKDATA_TITLE).StringValue;
                    data.AuthorId = da.Extract(BookConst.BOOKDATA_AUTHOR_ID).StringValue;
                    data.AuthorName = da.Extract(BookConst.BOOKDATA_AUTHOR_NAME).StringValue;
                    data.ISBN = da.Extract(BookConst.BOOKDATA_ISBN).StringValue;
                    data.Department = da.Extract(BookConst.BOOKDATA_DEPT).StringValue;
                    data.Genre = da.Extract(BookConst.BOOKDATA_GENRE).StringValue;
                    data.Media = da.Extract(BookConst.BOOKDATA_MEDIA).StringValue;
                    data.PublisherId = da.Extract(BookConst.BOOKDATA_PUBLISHER_ID).StringValue;
                    data.PublisherName = da.Extract(BookConst.BOOKDATA_PUBLISHER_NAME).StringValue;
                    data.SalesPrice = Utils.safeInt(da.Extract(BookConst.BOOKDATA_SALE_PRICE).StringValue); 
                    data.SalesPrice = data.SalesPrice / 100; //note held as whole pence/cents.
                    data.StockLevel = Utils.safeInt(da.Extract(BookConst.BOOKDATA_STOCK_LEVEL).StringValue); 
                    data.MinOrderQty = Utils.safeInt(da.Extract(BookConst.BOOKDATA_MIN_ORDER).StringValue);
                    data.TaxCode = da.Extract(BookConst.BOOKDATA_TAX_CODE).StringValue;
                    data.SupplierId = da.Extract(BookConst.BOOKDATA_SUPPLIER_ID).StringValue;
                    data.SupplierName = da.Extract(BookConst.BOOKDATA_SUPPLIER_NAME).StringValue;
                    data.PurchasePrice = Utils.safeInt(da.Extract(BookConst.BOOKDATA_PURCH_PRICE).StringValue);
                    data.PurchasePrice = data.PurchasePrice / 100; 
                    data.LongDescription = da.Extract(BookConst.BOOKDATA_LONG_DESCRIPTION).StringValue.Replace(VM_STR, CRLF);
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            } 
            return true;
        }

        /// <summary>
        /// getBookImage: get the book title image
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="image"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public Boolean getBookImage(String titleId, ref Image image, ref String errText)
        {
            String imageData = String.Empty;
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_getBookImage", 3);
                    s.SetArg(0, titleId);
                    s.Call();
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                    imageData = s.GetArg(1);
                }
            }
            catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            if (string.IsNullOrEmpty(imageData)) {
                return false;
            }
            MemoryStream m = new MemoryStream(Encoding.Default.GetBytes(imageData));
            image = Image.FromStream(m);
            return true;

        }

        /// <summary>
        /// getClientData: get a full set of data for a single client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientData"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public Boolean getClientData(String clientId, ref ClientData clientData, ref String errText) {
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_getClientData", 3);
                    s.SetArg(0, clientId);
                    s.Call();
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }

                    UniDynArray da = s.GetArgDynArray(1);

                    clientData.ClientId = da.Extract(BookConst.CLIENTDATA_CLIENTID).StringValue;
                    clientData.Surname = da.Extract(BookConst.CLIENTDATA_SURNAME).StringValue;
                    clientData.Forename = da.Extract(BookConst.CLIENTDATA_FORENAME).StringValue;
                    clientData.Address = da.Extract(BookConst.CLIENTDATA_ADDRESS).StringValue.Replace(VM_STR, CRLF);
                    clientData.Country = da.Extract(BookConst.CLIENTDATA_COUNTRY).StringValue;
                    clientData.JoinDate = da.Extract(BookConst.CLIENTDATA_JOINDATE).StringValue;

                    clientData.OrderList.Clear();
                    clientData.PaymentList.Clear();

                    int noOrders = da.Dcount(BookConst.CLIENTDATA_ORDERIDS);
                    for (int i = 1; i <= noOrders; i++) {
                        ClientOrder co = new ClientOrder();
                        co.OrderId = da.Extract(BookConst.CLIENTDATA_ORDERIDS, i).StringValue;
                        co.OrderStatus = da.Extract(BookConst.CLIENTDATA_ORDERSTATUS, i).StringValue;
                        co.OrderDate = da.Extract(BookConst.CLIENTDATA_ORDERDATE, i).StringValue;
                        co.OrderTotal = Utils.safeDouble(da.Extract(BookConst.CLIENTDATA_ORDERTOTAL, i).StringValue);
                        co.OrderTotal = co.OrderTotal / 100;
                        clientData.OrderList.Add(co);
                    }
                    int noPayments = da.Dcount(BookConst.CLIENTDATA_PAYMENTIDS);
                    for (int i = 1; i <= noPayments; i++) {
                        ClientPayment cp = new ClientPayment();
                        cp.PaymentId = da.Extract(BookConst.CLIENTDATA_PAYMENTIDS, i).StringValue;
                        cp.PaymentDate = da.Extract(BookConst.CLIENTDATA_PAYMENTDATE, i).StringValue;
                        cp.PaymentAmount = Utils.safeInt(da.Extract(BookConst.CLIENTDATA_PAYMENTAMT, i).StringValue);
                        cp.PaymentAmount = cp.PaymentAmount / 100;
                        clientData.PaymentList.Add(cp);
                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// getSalesTaxes: get a list of sales taxes
        /// </summary>
        /// <param name="salesTaxDict"></param>
        /// <returns></returns>
        public Boolean getSalesTaxes(SalesTaxDict salesTaxDict) {
            try {
                
                salesTaxDict.Clear();
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_getSalesTaxes", 2);
                    s.Call();
                    UniDynArray da = s.GetArgDynArray(0);
                    int noTaxes = da.Dcount(1);
                    for (int i = 1; i <= noTaxes; i++) {
                        SalesTax st = new SalesTax();
                        st.TaxCode = da.Extract(1, i).StringValue;
                        st.ShortDescription = da.Extract(2, i).StringValue;
                        st.Rate = Convert.ToDouble(da.Extract(3, i).StringValue);
                        salesTaxDict.Add(st.TaxCode, st);
                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// getScreenImage; get the display image for a screen data
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fieldList"></param>
        /// <param name="id"></param>
        /// <param name="record"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Boolean getScreenImage(string fileName, string fieldList, string id, UniDynArray record, ref UniDynArray image) {
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_getScreenImage", 6);
                    s.SetArg(0, fileName);
                    s.SetArg(1, fieldList);
                    s.SetArg(2, id);
                    s.SetArg(3, record);
                    s.SetArg(4, string.Empty);
                    s.SetArg(5, string.Empty);

                    s.Call();

                    String errtext = s.GetArg(5);
                    if (String.IsNullOrEmpty(errtext) == false) {
                        ShowError(errtext);
                        return false;
                    }
                    image = s.GetArgDynArray(4);
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
            
        }

        /// <summary>
        /// getShippingCosts: get the shipping descriptions and fixed costs
        /// </summary>
        /// <param name="shippingCosts"></param>
        /// <returns></returns>
        public Boolean getShippingCosts(ShippingCostList shippingCosts) {
            shippingCosts.Clear();
                try{
                    lock (_syncCall) {
                        UniSubroutine s = _sess.CreateUniSubroutine("u2_getShippingRates", 3);
                        String inData = String.Empty;
                        if (_moneyFormat == 1) {
                            inData = "1";
                        }
                        s.SetArg(0, inData);
                        s.Call();
                        UniDynArray da = s.GetArgDynArray(1);
                        int noTaxes = da.Dcount(2);
                        for (int i = 1; i <= noTaxes; i++) {
                            ShippingCost sc = new ShippingCost();
                            sc.ShippingId = da.Extract(1, i).StringValue;
                            sc.Description = da.Extract(2, i).StringValue;
                            sc.Cost = Utils.safeDouble(da.Extract(3, i).StringValue);
                            shippingCosts.Add(sc);
                        }
                    }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;        
        }

        /// <summary>
        /// readParameter: read a parameter record from the U2_PARAMS file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public Boolean readParameter(String id, ref String result) {
            if (openFile("U2_PARAMS") == false) {
                ShowError("Cannot open parameter file");
                return false;
            }
            lock (_syncCall) {
                UniDynArray rec = null;
                if (readRecord("U2_PARAMS", id, ref rec) == false) {
                    result = String.Empty;
                } else {
                    result = rec.StringValue;
                }
            }
            return true;
        }


        /// <summary>
        /// recalcOrder : recalculate an order by applying any current promotions and pricing.
        /// </summary>
        /// <param name="orderRec"></param>
        /// <returns></returns>
        public Boolean recalcOrder(ref UniDynArray orderRec) {
            String errText = String.Empty;
            try{
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_promos", 2);
                    s.SetArg(0, orderRec);
                    s.Call();
                    errText = s.GetArg(1);
                    orderRec = s.GetArgDynArray(0);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
           
            return true;

        }

        /// <summary>
        /// u2_releaseBook : release a record lock held on a book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Boolean releaseBook(String bookId) {
            String errText = String.Empty;
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_releaseBook", 2);
                    s.SetArg(0, bookId);
                    s.Call();
                    errText = s.GetArg(1);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
            }
            catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;

        }

        /// <summary>
        /// reportOrders : execute an order report
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="clientId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public Boolean reportOrders(string orderId, string clientId, ref string result) {
            try {
                lock (_syncCall) {
                    if (string.IsNullOrEmpty(orderId)) orderId = "ALL";
                    string intl = (_moneyFormat == u2_const.FORMAT_EU ? "INTL" : "UK");

                    String cmd = "u2_reportOrder " + intl + " " + orderId + " " + clientId;
                    UniCommand c = _sess.CreateUniCommand();
                    c.Command = cmd;
                    c.Execute();
                    result = c.Response;
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            } 
            return true;

                   
        }

        /// <summary>
        /// reportOrderHistory: show an order history for a given book.
        /// </summary>
        /// <remarks>
        /// This leverages the built in reporting language to capture a report as text.
        /// </remarks>
        /// <param name="bookId"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public Boolean reportOrderHistory(String bookId, ref String results) {
            String errText = String.Empty;
            String searchData = bookId + FM_STR + moneyFormat.ToString();
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_reportOrderHistory", 3);
                    s.SetArg(0, searchData);
                    s.Call();
                    results = s.GetArg(1).Replace(FM_STR, CRLF);
                    errText = s.GetArg(2);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// reportPurchaeHistory: show the purchase history for a given book.
        /// </summary>
        /// <remarks>
        /// This leverages the reporting language to capture an equiry command as text.
        /// </remarks>
        /// <param name="bookId"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public Boolean reportPurchaseHistory(String bookId, ref String results) {
            String errText = String.Empty;
            String searchData = bookId + FM_STR + moneyFormat.ToString();
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_reportPurchaseHistory", 3);
                    s.SetArg(0, searchData);
                    s.Call();
                    results = s.GetArg(1).Replace(FM_STR, CRLF);
                    errText = s.GetArg(2);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// saveSalesOrder : save new or existing sales order
        /// </summary>
        /// <remarks>
        /// This calls a routine to save the order, to generate new order keys and to update relevant indices.
        /// </remarks>
        /// <param name="orderId"></param>
        /// <param name="orderRec"></param>
        /// <returns></returns>
        public Boolean saveSalesOrder(ref String orderId, ref UniDynArray orderRec) {

            String errText = String.Empty;
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_saveSalesOrder", 3);
                    s.SetArg(0, orderId);
                    s.SetArg(1, orderRec);
                    s.Call();
                    orderId = s.GetArg(0);
                    orderRec = s.GetArgDynArray(1);
                    errText = s.GetArg(2);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// searchBooks: search for books (author, publisher or keyword)
        /// </summary>
        /// <remarks>
        /// This peforms a traditional search returning the results as a dynamic array.
        /// The results are parsed into a list of book summaries using regular extractions.
        /// </remarks>
        /// <param name="searchType"></param>
        /// <param name="searchData"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Boolean searchBooks(int searchType, String searchData, BookSummaryList list) {
            String subrName = String.Empty;
            String errText = String.Empty;
            UniDynArray results = null;

            list.Clear();
            
            switch(searchType){
                case 0: // author
                    subrName = "u2_getBooksForAuthor";
                    break;
                case 1: // publisher
                    subrName = "u2_getBooksForPublisher";
                    break;
                case 2:
                     subrName = "u2_getBooks";
                     break;
                default:
                     ShowError("Not a recognized search type");
                     return false;
            }

            searchData = searchData + FM_STR + moneyFormat.ToString();

            try {
            
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine(subrName, 3);
                    s.SetArg(0, searchData);
                    s.Call();
                    errText = s.GetArg(2);
                    results = s.GetArgDynArray(1);

                    if (String.IsNullOrEmpty(results.StringValue) == false) {
                        int dc = results.Dcount(BookConst.BOOKSUMMARY_BOOKID);
                        for (int i = 1; i <= dc; i++) {
                            BookSummary b = new BookSummary();
                            b.BookId = results.Extract(BookConst.BOOKSUMMARY_BOOKID, i).StringValue;
                            b.Title = results.Extract(BookConst.BOOKSUMMARY_TITLE, i).StringValue;
                            b.AuthorName = results.Extract(BookConst.BOOKSUMMARY_AUTHORNAME, i).StringValue;
                            b.ISBN = results.Extract(BookConst.BOOKSUMMARY_ISBN, i).StringValue;
                            b.SalesPrice = Convert.ToDouble(results.Extract(BookConst.BOOKSUMMARY_SALESPRICE, i).StringValue); // TBD
                            b.Media = results.Extract(BookConst.BOOKSUMMARY_MEDIA, i).StringValue;
                            list.Add(b);
                        }

                    }
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            } 
            return true;
        }

        /// <summary>
        /// searchClient: this performs a client search returning XML data.
        /// </summary>
        /// <remarks>
        /// This differs from the book search in using XML as a vehicle for the results.
        /// These are parsed into a client summary list using LINQ to XML.
        /// </remarks>
        /// <param name="clientId"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Boolean searchClient(string forename, int forenameSearchType, string surname, int surnameSearchType, ClientSummaryList list) {
            String results = String.Empty;
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_searchClients", 3);

                    String searchData = surname + FM_STR + surnameSearchType.ToString() + FM_STR + forename + FM_STR + forenameSearchType.ToString();
                    String errText = String.Empty;
                    

                    s.SetArg(0, searchData);
                    s.Call();
                    errText = s.GetArg(2);
                    results = s.GetArg(1).Replace(FM_STR, CRLF);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
                
                System.Diagnostics.Debug.Print("Start of text");
                System.Diagnostics.Debug.Print(results);
                System.Diagnostics.Debug.Print("end of text");

                XDocument doc = XDocument.Parse(results);
                var query = from e in doc.Element("ROOT").Descendants("U2_CLIENTS")
                            select new ClientSummary
                            {
                                ClientId = (string) e.Element("ID"),
                                FirstName = (string) e.Element("FORENAME"),
                                LastName = (string) e.Element("SURNAME"),
                                Address = (string) e.Element("DELIM_ADDRESS"),
                                JoinDate = (string) e.Element("JOIN_DATE"),
                                AccountStatus = (string) e.Element("ACCOUNT_STATUS")
                            };
                foreach (ClientSummary c in query) {
                    list.Add(c);
                }

                            

            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// searchOrders this performs an orders search returning nested XML data.
        /// </summary>
        /// <remarks>
        /// This differs from the book search in using XML as a vehicle for the results.
        /// These are parsed into a order summary list using LINQ to XML.
        /// </remarks>
        /// <param name="clientId"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Boolean searchOrders(String orderId, String clientId, string surname, string forename, string startDate, string endDate, SalesOrderDataList list) {
            String results = String.Empty;
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_searchOrders", 3);

                    String searchData = orderId + FM_STR + clientId + FM_STR + surname + FM_STR + forename + FM_STR + startDate + FM_STR + endDate + FM_STR + _moneyFormat.ToString();
                    String errText = String.Empty;
                    

                    s.SetArg(0, searchData);
                    s.Call();
                    errText = s.GetArg(2);
                    results = s.GetArg(1).Replace(FM_STR, CRLF);
                    if (String.IsNullOrEmpty(errText) == false) {
                        ShowError(errText);
                        return false;
                    }
                }
                System.Diagnostics.Debug.Print(results);


                XDocument doc = XDocument.Parse(results);
                var query = from e in doc.Element("ROOT").Descendants("ORDER")
                            select new SalesOrderData
                            {
                                OrderId = Utils.safeString((string)e.Element("ID")),
                                OrderDate = Utils.safeString((string)e.Element("ORDER_DATE")),
                                ClientId = Utils.safeString ((string)e.Element("CLIENT_ID")),
                                Fullname = Utils.safeString((string)e.Element("FULLNAME")),                                
                                ShipCode = Utils.safeString((string)e.Element("SHIP_CODE")),
                                ShipCost = Utils.safeDouble((String)e.Element("SHIP_COST")),
                                Lines = (e.Element("LINES").HasElements == false ?
                                 null:
                                 (from e2 in e.Element("LINES").Elements("LINE")
                                         select new SalesOrderLine
                                         {
                                             BookId = Utils.safeString((string)e2.Element("BOOK_ID")),
                                             Title = Utils.safeString((string)e2.Element("TITLE")),
                                             AuthorName = Utils.safeString((string)e2.Element("AUTHOR_NAME")),
                                             Price = Utils.safeDouble((string)e2.Element("PRICE")),
                                             TaxCode = Utils.safeString((string)e2.Element("TAX_CODE")),
                                             GoodsAmt = Utils.safeDouble((String)e2.Element("GOODS_AMT")),
                                             TaxAmt = Utils.safeDouble((string)e2.Element("TAX_AMT")),
                                             Qty = Utils.safeInt((string)e2.Element("QTY"))
                                         }).ToList<SalesOrderLine>())

                            };
            

                foreach (SalesOrderData c in query) {
                    list.Add(c);
                }


            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// setBookData; set the book data back to the server to be saved
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="data"></param>
        /// <param name="lockFlag"></param>
        /// <returns></returns>
        public Boolean setBookData(string bookId, BookData data, bool lockFlag) {
            UniDynArray da = Server.Instance.createArray();
            String errText = string.Empty;

            da.Replace(BookConst.BOOKDATA_AUTHOR_ID, data.AuthorId);
            da.Replace(BookConst.BOOKDATA_AUTHOR_NAME, data.AuthorName);
            da.Replace(BookConst.BOOKDATA_DEPT, data.Department);
            da.Replace(BookConst.BOOKDATA_GENRE, data.Genre);
            da.Replace(BookConst.BOOKDATA_ISBN, data.ISBN);
            da.Replace(BookConst.BOOKDATA_LONG_DESCRIPTION, data.LongDescription);
            da.Replace(BookConst.BOOKDATA_MEDIA, data.Media);
            da.Replace(BookConst.BOOKDATA_MIN_ORDER, data.MinOrderQty.ToString());
            da.Replace(BookConst.BOOKDATA_PUBLISHER_ID, data.PublisherId);
            da.Replace(BookConst.BOOKDATA_PUBLISHER_NAME, data.PublisherName);
            da.Replace(BookConst.BOOKDATA_PURCH_PRICE, (data.PurchasePrice * 100).ToString());
            da.Replace(BookConst.BOOKDATA_SALE_PRICE, (data.SalesPrice * 100).ToString());
            da.Replace(BookConst.BOOKDATA_STOCK_LEVEL, data.StockLevel.ToString());
            da.Replace(BookConst.BOOKDATA_SUPPLIER_ID, data.SupplierId);
            da.Replace(BookConst.BOOKDATA_SUPPLIER_NAME, data.SupplierName);
            da.Replace(BookConst.BOOKDATA_TAX_CODE, data.TaxCode);
            da.Replace(BookConst.BOOKDATA_TITLE, data.ShortTitle);

            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_setBookData", 4);
                    s.SetArg(0, bookId);
                    s.SetArg(1, da);
                    s.SetArg(2, "1");
                    s.SetArg(3, String.Empty);

                    s.Call();

                    errText = s.GetArg(3);
                }
                if (String.IsNullOrEmpty(errText) == false) {
                    ShowError(errText);
                    return false;
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// showSalesOrder
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderText"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public Boolean showSalesOrder(String orderId, ref String orderText, ref String errText) {
            try {
                lock (_syncCall) {
                    UniSubroutine s = _sess.CreateUniSubroutine("u2_showSalesOrder", 3);
                    s.SetArg(0, orderId);
                    s.Call();
                    orderText = s.GetArg(1);
                    errText = s.GetArg(2);
                }
            } catch (Exception ex) {
                ShowError(ex.Message);
                return false;
            }
            return true;
        }

        #endregion

        public String lastError {
            get { return _lastError; }
        }

        public String lastErrorCode {
            get { return getErrorCode(_lastError); }
        }

        public int moneyFormat
        {
            get { return _moneyFormat; }
            set { _moneyFormat = value; }
        }

        public int dateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; }
        }
    }
}
