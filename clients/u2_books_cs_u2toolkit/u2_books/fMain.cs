using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using u2_books.Forms;
namespace u2_books
{
    public partial class fMain : Form
    {
        protected StaticData _staticData;
        protected Server _server;

        public fMain() {
            InitializeComponent();
            setConnected(false);
        }

        #region Operations

        /// <summary>
        /// doBookSearch: show the book search form.
        /// </summary>
        /// <remarks>
        /// this form is used both as a launch for book maintenance and for selections.
        /// </remarks>
        protected void doBookSearch() {
            fBookSearch f = new fBookSearch();
            f.MdiParent = this;
            f.onShowBook += new ShowBookEvent(showBookHandler);
            f.showBookSearch();
        }

        /// <summary>
        /// doClientSearch: show the client search form.
        /// </summary>
        /// <remarks>
        /// this form is used both for client maintenance and for selections.
        /// </remarks>
        protected void doClientSearch() {           
            fClientSearch f = new fClientSearch();
            f.onShowClient += new ShowClientEvent(showClientHandler);
            f.MdiParent = this;

            f.Show();
        }

        /// <summary>
        /// doConnect: connect to a U2 server using the credentials supplied.
        /// </summary>
        /// <returns></returns>
        protected Boolean doConnect() {
            fConnect f = new fConnect();
            if (f.ShowConnection()) {
                _server = Server.Instance;
                initStaticData();
                setConnected(true);
                return true;
            } else {
                setConnected(false);
                return false;
            }
        }

        /// <summary>
        /// doCreatePOs: call routine to generate daily purchase orders
        /// </summary>
        protected void doCreatePOs()
        {
            fCreatePOs f = new fCreatePOs();
            f.OnShowPO += new ShowPOEvent(f_OnShowPO);
            f.MdiParent = this;
            f.Show();
        }

        void f_OnShowPO(object sender, string orderId, bool forEdit, bool isModal)
        {
            doPurchaseOrderEntry(orderId);
        }

        /// <summary>
        /// doHelpAbout: show the about form
        /// </summary>
        protected void doHelpAbout()
        {
            AboutBox1 f = new AboutBox1();
            f.Show();
        }

        /// <summary>
        /// doMaintainPromotions
        /// </summary>
        /// <remarks>
        /// This runs a template-driven screen.
        /// The template is held in the parameter file on the server.
        /// </remarks>
        protected void doMaintainPromotions() {
            string screenXML = String.Empty;
            if (Server.Instance.readParameter("PROMOTIONS_SCREEN", ref screenXML) == false) {
                return;
            }
            ScreenDefn defn = ScreenDefn.fromXML(screenXML.Replace(Server.FM_STR, Server.CRLF));
            if (defn == null) {
                return;
            }
            fScreen f = new fScreen();
            f.MdiParent = this;
            f.Show();
            f.Defn = defn;
        }


        /// <summary>
        /// doMaintainShippingCosts
        /// </summary>
        /// <remarks>
        /// This runs a template-driven screen to maintain shipping costs.
        /// The template is held in the parameter file on the server.
        /// </remarks>
        protected void doMaintainShippingCosts() {
            string screenXML = String.Empty;
            String screenName = (Server.Instance.moneyFormat == 1 ? "SHIPPING_SCREEN_INTL" : "SHIPPING_SCREEN");
            if (Server.Instance.readParameter(screenName, ref screenXML) == false) {
                return;
            }
            ScreenDefn defn = ScreenDefn.fromXML(screenXML.Replace(Server.FM_STR, Server.CRLF));
            if (defn == null) {
                return;
            }
            fScreen f = new fScreen();
            f.MdiParent = this;
            f.Show();
            f.Defn = defn;
        }

        /// <summary>
        /// doMaintainSupplier
        /// </summary>
        /// <remarks>
        /// This runs a template-driven screen to maintain shipping costs.
        /// The template is held in the parameter file on the server.
        /// </remarks>
        protected void doMaintainSupplier() {
            string screenXML = String.Empty;
            String screenName = (Server.Instance.moneyFormat == 1 ? "SUPPLIERS_SCREEN_INTL" : "SUPPLIERS_SCREEN");
            if (Server.Instance.readParameter(screenName, ref screenXML) == false) {
                return;
            }
            ScreenDefn defn = ScreenDefn.fromXML(screenXML.Replace(Server.FM_STR, Server.CRLF));
            if (defn == null) {
                return;
            }
            fScreen f = new fScreen();
            f.MdiParent = this;
            f.Show();
            f.Defn = defn;
        }
        /// <summary>
        /// doMaintainClients
        /// </summary>
        /// <remarks>
        /// This runs a template-driven screen to maintain shipping costs.
        /// The template is held in the parameter file on the server.
        /// </remarks>
        protected void doMaintainClients()
        {
            string screenXML = String.Empty;
            if (Server.Instance.readParameter("CLIENTS_SCREEN", ref screenXML) == false) {
                return;
            }
            ScreenDefn defn = ScreenDefn.fromXML(screenXML.Replace(Server.FM_STR, Server.CRLF));
            if (defn == null) {
                return;
            }
            fScreen f = new fScreen();
            f.MdiParent = this;
            f.Show();
            f.Defn = defn;
        }

        /// <summary>
        /// doMaintainSalesTax
        /// </summary>
        /// <remarks>
        /// This runs a template-driven screen to maintain shipping costs.
        /// The template is held in the parameter file on the server.
        /// </remarks>
        protected void doMaintainSalesTax() {
            string screenXML = String.Empty;
            if (Server.Instance.readParameter("SALESTAX_SCREEN", ref screenXML) == false) {
                return;
            }
            ScreenDefn defn = ScreenDefn.fromXML(screenXML.Replace(Server.FM_STR, Server.CRLF));
            if (defn == null) {
                return;
            }
            fScreen f = new fScreen();
            f.MdiParent = this;
            f.Show();
            f.Defn = defn;
        }

        /// <summary>
        /// doPurchaseOrderEntry
        /// </summary>
        /// <remarks>
        /// This runs a template-driven screen.
        /// The template is held in the parameter file on the server.
        /// </remarks>
        protected void doPurchaseOrderEntry(String orderId)
        {
            string screenXML = String.Empty;
            String screenName = (Server.Instance.moneyFormat == 1 ? "PURCHASE_SCREEN_INTL" : "PURCHASE_SCREEN");
            if (Server.Instance.readParameter(screenName, ref screenXML) == false) {
                return;
            }
            ScreenDefn defn = ScreenDefn.fromXML(screenXML.Replace(Server.FM_STR, Server.CRLF));
            if (defn == null) {
                return;
            }
            fScreen f = new fScreen();
            
            f.MdiParent = this;
            f.Show();
            f.Defn = defn;
            if (String.IsNullOrEmpty(orderId) == false) {
                f.Id = orderId;
            }
            
        }

        /// <summary>
        /// doSalesOrderEntry
        /// </summary>
        /// <remarks>
        /// This runs the sales order entry screen, for hard coded data entry.
        /// </remarks>
        protected void doSalesOrderEntry() {
            fSalesOrder f = new fSalesOrder();
            f.onSelectClient += new SelectClientEvent(selectClientHandler);
            f.onSelectBook += new SelectBookEvent(selectBookHandler);
            f.onShowBook += new ShowBookEvent(showBookHandler);
            f.MdiParent = this;
            f.Show();
            f.createNewOrder();
        }

        /// <summary>
        /// doSalesOrderSearch
        /// </summary>
        /// <remarks>
        /// Show the sales order search form.
        /// </remarks>
        protected void doSalesOrderSearch() {
            fOrderSearch f = new fOrderSearch();
            f.MdiParent = this;
            f.onEditOrder += new ShowOrderEvent(showOrderHandler);
            f.onShowOrder += new ShowOrderReportEvent(showOrderReport);
            f.Show();
        }

      

        /// <summary>
        /// doSelectClient: show the client search in selection mode
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        protected Boolean doSelectClient(ref string clientId) {
            fClientSearch f = new fClientSearch();
            f.onShowClient += new ShowClientEvent(showClientHandler);
            return f.selectClient(ref clientId);
        }

        /// <summary>
        /// doSelectBook: show the book search in selection mode
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        protected Boolean doSelectBook(string searchText, ref string bookId) {
            fBookSearch f = new fBookSearch();
            f.onShowBook += new ShowBookEvent(showBookHandler);
            if (String.IsNullOrEmpty(searchText)) {
                return f.showBookSearch(ref bookId);
            } else {
                return f.showBookSearch(searchText, ref bookId);
            }
        }


        /// <summary>
        /// initStaticData: initialize static data
        /// </summary>
        /// <remarks>
        /// This retrieves static data that may be useful to have around.
        /// </remarks>
        protected void initStaticData() {

            showStatus("Fetching static data please wait...");
            _staticData = StaticData.Instance;
            _server.getSalesTaxes(_staticData.salesTaxDict);

            showStatus("");

        }

        /// <summary>
        /// setConnected: set the connected status
        /// </summary>
        /// <remarks>
        /// This changes captions and enables or disables menu controls to show if enabled.
        /// </remarks>
        /// <param name="isConnected"></param>
        protected void setConnected(Boolean isConnected) {
            actionsToolStripMenuItem.Enabled = isConnected;
            maintainToolStripMenuItem.Enabled = isConnected;
            connectToolStripMenuItem.Enabled = !isConnected;
            if (isConnected) {
                Text = BookConst.Title + " [" + _server.Session.HostName + ' ' + _server.Session.Account + "]";
            } else {
                Text = BookConst.Title + " [no connection]";
            }
        }

        protected void showStatus(String text) {

        }

        #endregion

       
        #region Form Control
        protected void showBookForm(String bookId, Boolean forEdit, Boolean isModal)
        {
            fBook f = new fBook();
            f.onShowOrderHistory += new ShowOrderHistoryEvent(showOrderHistory);
            f.onShowPurchaseHistory += new ShowPurchaseHistoryEvent(showPurchaseHistory);
            
            f.readTitle(bookId, forEdit);
            
            if (isModal) {
                f.ShowDialog();
            } else {
                f.MdiParent = this;
                f.Show();
            }            
        }

        protected void showClientForm(String clientId, Boolean forEdit, Boolean isModal) {
            fClient f = new fClient();
            
            f.onShowOrder += new ShowOrderReportEvent(showOrderReport);
            f.readClient(clientId, forEdit);
            if (isModal) {
                f.ShowDialog();
            } else {
                f.MdiParent = this;
                f.Show();
            }                       
        }

        
        void showOrderHistory(object sender, string bookId, bool isModal) {
            showOrderHistoryForm(bookId, isModal);
        }

        void showPurchaseHistory(object sender, string bookId, bool isModal) {
            showPurchaseHistoryForm(bookId, isModal);
        }

        void showOrderReport(object sender, string orderId, string clientId, bool isModal) {
            showOrderReportForm(orderId, clientId, isModal);
        }

        protected void showOrderForm(String orderId, Boolean isModal) {
            fSalesOrder f = new fSalesOrder();
            f.onSelectClient += new SelectClientEvent(selectClientHandler);
            f.onSelectBook += new SelectBookEvent(selectBookHandler);

            f.readOrder(orderId);

            if (isModal == false) {
                f.MdiParent = this;
                f.Show();
            } else {
                f.ShowDialog();
            }            
        }

        protected void showOrderHistoryForm(String bookId, Boolean isModal) {
            String results = String.Empty;
            if (_server.reportOrderHistory(bookId, ref results) == false) {
                return;
            }

            fReport f = new fReport();
            if (isModal == false) {
                f.MdiParent = this;
            }
            f.showReport(results, '\f', isModal);            
        }

        protected void showOrderReportForm(String orderId, string clientId, Boolean isModal) {
            String results = String.Empty;
            if (_server.reportOrders(orderId, clientId, ref results) == false) {
                return;
            }

            fReport f = new fReport();
            if (isModal == false) {
                f.MdiParent = this;
            }
            f.showReport(results, '\f', isModal);
        }

         
        protected void showPurchaseHistoryForm(String bookId, Boolean isModal) {
            String results = String.Empty;
            if (_server.reportPurchaseHistory(bookId, ref results) == false) {
                return;
            }

            fReport f = new fReport();
            if (isModal == false) {
                f.MdiParent = this;
            }
            f.showReport(results, '\f', isModal);
        }
        
        #endregion

        

        #region Local Controls

        private void bookSearchToolStripMenuItem_Click(object sender, EventArgs e) {
            doBookSearch();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e) {
            doConnect();
        }

        private void clientSearchToolStripMenuItem_Click(object sender, EventArgs e) {
            doClientSearch();
        }
        #endregion

        #region Event Handlers

        bool selectBookHandler(object sender, string searchText, ref string bookId) {
            return doSelectBook(searchText, ref bookId);
        }

        bool selectClientHandler(object sender, ref string clientId) {
            return doSelectClient(ref clientId);
        }

        protected void showBookHandler(object sender, string bookId, Boolean forEdit, Boolean isModal)
        {
            showBookForm(bookId, forEdit, isModal);
        }

        void showClientHandler(object sender, string clientId, bool forEdit, bool isModal) {
            showClientForm(clientId, forEdit, isModal);
        }
        void showOrderHandler(object sender, string orderId, bool forEdit, bool isModal) {
            showOrderForm(orderId, isModal);
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (_server != null) {
                if (_server.Connected) {
                    _server.disconnect();
                }
            }
        }

        private void salesOrderEntryToolStripMenuItem_Click(object sender, EventArgs e) {
            doSalesOrderEntry();
        }

        private void orderSearchToolStripMenuItem_Click(object sender, EventArgs e) {
            doSalesOrderSearch();
        }

        private void shippingCostsToolStripMenuItem_Click(object sender, EventArgs e) {
            doMaintainShippingCosts();
        }

        private void supplierDetailsToolStripMenuItem_Click(object sender, EventArgs e) {
            doMaintainSupplier();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            doMaintainSalesTax();    
        }

        private void maintainToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void promotionsToolStripMenuItem_Click(object sender, EventArgs e) {
            doMaintainPromotions();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doHelpAbout();
        }

        private void purchaseOrderEntryMenuItem_Click(object sender, EventArgs e)
        {
            doPurchaseOrderEntry(String.Empty);
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doMaintainClients();
        }

        private void generatePurchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doCreatePOs();
        }

        private void salesChartsToolStripMenuItem_Click(object sender, EventArgs e) {
            new fSalesCharts().Show();
        }
    }
}
