using System;
using System.Windows.Forms;

using U2.Data.Client.UO;
using u2_books.shared;

namespace u2_books.Forms
{
    /// <summary>
    /// Sales Order Entry Demo
    /// </summary>
    /// <remarks>
    /// This demonstrates a 'hard coded' approach to building a form in which all the logic is directly
    /// coded - see the maintenance and purchase order entry for a template-driven alternative.
    /// New sales orders can be created and are stored against an internally generated key. Saving the
    /// order calls a server side subroutine to ensure all the secondary updates are handled.
    /// Likewise any changes to the order lines need to pass back to a server routine to recalculate the
    /// prices, sales taxes and to pick up any sales promotions in effect.
    /// </remarks>
    public partial class fSalesOrder : Form
    {
        protected const int COL_BOOK_ID = 0;
        protected const int COL_TITLE = 1;
        protected const int COL_AUTHOR = 2;
        protected const int COL_QTY = 3;
        protected const int COL_PRICE = 4;
        protected const int COL_TAX = 5;
        protected const int COL_GOODSAMT = 6;
        protected const int COL_TAXAMT = 7;
        protected const int COL_PROMO = 8;

        protected ShippingCostList _shippingCosts = new ShippingCostList();

        protected String _orderId = String.Empty;
        protected UniDynArray _orderRec = null;

        public event SelectClientEvent onSelectClient = null;
        public event SelectBookEvent onSelectBook = null;
        public event ShowBookEvent onShowBook = null;
        
        protected bool _changed = false;

        public fSalesOrder() {
            InitializeComponent();            
        }

        private void fSalesOrder_Load(object sender, EventArgs e)
        {
            initData();
        }

        #region Public Methods

        /// <summary>
        /// createNewOrder: Set some standard defaults
        /// </summary>
        public void createNewOrder() {
            _orderId = String.Empty ;
            _orderRec = Server.Instance.createArray();
            _orderRec.Replace(BookConst.ORDERS_ORDER_STATUS, "NEW");
            _orderRec.Replace(BookConst.ORDERS_ORIGIN_CODE, "PHONE");
            _orderRec.Replace(BookConst.ORDERS_SHIP_ID, "FREE");
            showOrder();
            _changed = false;
        }

        /// <summary>
        /// readOrder: read and display a sales order
        /// </summary>
        /// <param name="orderId"></param>
        public void readOrder(String orderId) {
            _orderId = orderId;
            if (Server.Instance.readRecord("U2_ORDERS", orderId, ref _orderRec) == false) {
                createNewOrder();
            } else {
                showOrder();
            }
            _changed = false;
        }

        #endregion

        #region Local functions
        /// <summary>
        /// addLine: add a new line to the order.
        /// </summary>
        /// <remarks>
        /// This updates the order array but must call the recalc before it can be displayed.
        /// </remarks>
        /// <param name="bookId"></param>
        /// <param name="qty"></param>
        protected void addLine(String bookId, int qty) {
            int noLines = 0;
            if (String.IsNullOrEmpty(_orderRec.Extract(BookConst.ORDERS_BOOK_ID).StringValue) == false) {
                noLines = _orderRec.Dcount(BookConst.ORDERS_BOOK_ID);
            }
            _orderRec.Replace(BookConst.ORDERS_BOOK_ID, noLines + 1, bookId);
            _orderRec.Replace(BookConst.ORDERS_QTY, noLines + 1, qty.ToString());

            recalcOrderLines();
            showOrderLines();
            _changed = true;
        }

        /// <summary>
        /// checkCanClose: check whether content has changed and offer a choice to cancel the action.
        /// </summary>
        /// <returns></returns>
        protected bool checkCanClose()
        {
            if (_changed == false) return true;

            return (MessageBox.Show("Record has changed. Are you sure you want to quit ?", "U2_Books", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes);
        }
        
        /// <summary>
        /// doSave: raise an event to save the order.
        /// </summary>
        /// <returns></returns>
        protected Boolean doSave()
        {
            if (Server.Instance.saveSalesOrder(ref _orderId, ref _orderRec)) {
                MessageBox.Show(String.Format("Order {0} Saved", _orderId));
                _changed = false;
                return true;
            }
            return false;
        }

        
        /// <summary>
        /// doSelectClient: select a new client for the order
        /// </summary>
        protected void doSelectClient() {
            if(onSelectClient != null){
                String clientId = String.Empty;
                if (onSelectClient(this, ref clientId)) {
                    txtClientId.Text = clientId;
                    getClient(clientId);
                    _changed = true;
                }
            }
        }

        /// <summary>
        /// getClient : get and display the details for the order client.
        /// </summary>
        /// <param name="clientId"></param>
        protected void getClient(string clientId) {
            // read client name
            UniDynArray clientRec = null;
            if (String.IsNullOrEmpty(clientId)) {
                clientRec = Server.Instance.createArray();
            } else {
                Server.Instance.readRecord("U2_CLIENTS", clientId, ref clientRec);
            }
            
            lblClientName.Text = clientRec.Extract(BookConst.CLIENTS_FORENAME).StringValue + " " + clientRec.Extract(BookConst.CLIENTS_SURNAME).StringValue;
            lblAddress.Text = clientRec.Extract(BookConst.CLIENTS_ADDRESS).StringValue.Replace(Server.VM_STR, Server.CRLF);

            // Check client is not on hold
            if( Utils.safeBool(clientRec.Extract(BookConst.CLIENTS_ACCOUNT_HELD).StringValue)){
                Server.Instance.ShowError("Client account is on hold");
            }
        }

        /// <summary>
        /// getShippingCost : get the cost for the selected shipping code
        /// </summary>
        /// <param name="shipCode"></param>
        protected void getShippingCost(String shipCode) {
            foreach (ShippingCost cost in _shippingCosts) {
                if (cost.ShippingId == shipCode) {
                    txtShipCost.Text = cost.Cost.ToString();
                    recalcTotals();
                }
            }
        }

        /// <summary>
        /// initData: initialize lists
        /// </summary>
        protected void initData() {

            Server.Instance.getShippingCosts(_shippingCosts);
            cboShipCode.DataSource = _shippingCosts;
            cboShipCode.DisplayMember = "Description";
            cboShipCode.ValueMember = "ShippingId";

            if (Server.Instance.dateFormat == u2_const.DATE_SENSIBLE) {
                dtpOrderDate.CustomFormat = "dd/MM/yyyy";
            } else {
                dtpOrderDate.CustomFormat = "MM/dd/yyyy";
            }
            
        }

        /// <summary>
        /// recalcOrderLines; get the pricing for the order lines
        /// </summary>
        /// <remarks>
        /// Combinations of books may be part of a marketing promotion (in U2_PROMOTIONS)
        /// so any changes need to recalculate all the prices on the order.
        /// </remarks>
        protected void recalcOrderLines() {
            Server.Instance.recalcOrder(ref _orderRec);            
        }
        
        /// <summary>
        /// recalctotals: recalculate the order totals
        /// </summary>
        protected void recalcTotals() {
            double total = 0;
            total = total + Utils.safeDouble(txtShipCost.Text);
            total = total + Utils.safeDouble(txtTaxCost.Text);
            total = total + Utils.safeDouble(txtGoodCost.Text);

            txtTotal.Text = String.Format("{0:N2}", total);
        }

        /// <summary>
        /// showOrder: parse the order into the form.
        /// </summary>
        protected void showOrder() {
            
            // order date is coded into the first part of the key
            if (String.IsNullOrEmpty(_orderId)) {
                dtpOrderDate.Value = DateTime.Today;
            } else {
                int date = Utils.safeInt(Utils.field(_orderId, "*", 1, 1));
                dtpOrderDate.Value = Utils.fromU2Date(date);
            }

            // client id, name and address
            String clientId = _orderRec.Extract(BookConst.ORDERS_CLIENT_ID).StringValue;
            txtClientId.Text = clientId;
            getClient(clientId);
           
            cboStatus.Text = _orderRec.Extract(BookConst.ORDERS_ORDER_STATUS).StringValue;            
            cboOrigin.Text = _orderRec.Extract(BookConst.ORDERS_ORIGIN_CODE).StringValue;
            cboShipCode.SelectedValue = _orderRec.Extract(BookConst.ORDERS_SHIP_ID).StringValue;
            getShippingCost(_orderRec.Extract(BookConst.ORDERS_SHIP_ID).StringValue);
            showOrderLines();
        }

        
        /// <summary>
        /// removeRow: remove a title from the order and recalculate the totals.
        /// </summary>
        /// <param name="row"></param>
        protected void removeRow(int row)
        {
            _orderRec.Delete(BookConst.ORDERS_BOOK_ID, row);
            _orderRec.Delete(BookConst.ORDERS_PRICE, row);
            _orderRec.Delete(BookConst.ORDERS_QTY, row);
            _orderRec.Delete(BookConst.ORDERS_PROMO_ID, row);
            _orderRec.Delete(BookConst.ORDERS_TAX_CODE, row);
            showOrderLines();
            _changed = true;
        }

        /// <summary>
        /// showOrderLines: show the order lines the hard way, going back to the server for additional data.
        /// </summary>
        protected void showOrderLines()
        {

            UniDynArray bookRec = null;
            UniDynArray authorRec = null;

            double totalGoods = 0.0;
            double totalTax = 0.0;

            int noLines = _orderRec.Dcount(BookConst.ORDERS_BOOK_ID);
            if (String.IsNullOrEmpty(_orderRec.Extract(BookConst.ORDERS_BOOK_ID).StringValue)) {
                noLines = 0;
            }
            dgvLines.RowCount = noLines;
            for (int line = 1; line <= noLines; line++) {

                String bookId = _orderRec.Extract(BookConst.ORDERS_BOOK_ID, line).StringValue;
                int qty = Utils.safeInt(_orderRec.Extract(BookConst.ORDERS_QTY, line).StringValue);
                double price = Utils.safeDouble(_orderRec.Extract(BookConst.ORDERS_PRICE, line).StringValue) / 100;
                String taxCode = _orderRec.Extract(BookConst.ORDERS_TAX_CODE, line).StringValue;
                String promotion = _orderRec.Extract(BookConst.ORDERS_PROMO_ID, line).StringValue;
                double taxrate = Utils.safeDouble(_orderRec.Extract(BookConst.ORDERS_TAX_RATE, line).StringValue);

                // get the book and author details
                String authorName = "Unknown";
                if (Server.Instance.readRecord("U2_BOOKS", bookId, ref bookRec)) {
                    if (Server.Instance.readRecord("U2_AUTHORS", bookRec.Extract(BookConst.BOOKS_AUTHOR_ID).StringValue, ref authorRec)) {
                        authorName = authorRec.Extract(BookConst.AUTHORS_FULLNAME).StringValue;
                    }
                }
                else {
                    bookRec = Server.Instance.createArray();
                }

                DataGridViewRow r = dgvLines.Rows[line - 1];

                r.Cells[COL_BOOK_ID].Value = bookId;
                r.Cells[COL_TITLE].Value = bookRec.Extract(BookConst.BOOKS_TITLE).StringValue;
                r.Cells[COL_AUTHOR].Value = authorName;
                r.Cells[COL_QTY].Value = qty;
                r.Cells[COL_PRICE].Value = price;
                r.Cells[COL_TAX].Value = taxCode;
                r.Cells[COL_GOODSAMT].Value = (price * qty);
                r.Cells[COL_TAXAMT].Value = (price * qty * taxrate) / 100;
                r.Cells[COL_PROMO].Value = promotion;

                totalGoods = totalGoods + (price * qty);
                totalTax = totalTax + (price * qty * taxrate) / 100;

            }

            txtGoodCost.Text = String.Format("{0:N2}", totalGoods);
            txtTaxCost.Text = String.Format("{0:N2}", totalTax);
            recalcTotals();
        }

        
        /// <summary>
        /// validate: validate and update the order before saving
        /// </summary>
        /// <returns></returns>
        protected Boolean validate()
        {
            if (String.IsNullOrEmpty(txtClientId.Text)) {
                Server.Instance.ShowError("Please select a client");
                return false;
            }
            _orderRec.Replace(BookConst.ORDERS_CLIENT_ID, txtClientId.Text);
            _orderRec.Replace(BookConst.ORDERS_ORDER_STATUS, cboStatus.Text);
            _orderRec.Replace(BookConst.ORDERS_ORIGIN_CODE, cboOrigin.Text);
            _orderRec.Replace(BookConst.ORDERS_SHIP_ID, cboShipCode.SelectedValue.ToString());
            int shipCost = (int)(Utils.safeDouble(txtShipCost.Text) * 100);
            _orderRec.Replace(BookConst.ORDERS_SHIP_COST, shipCost.ToString());

            return true;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// cmdFindClient_Click: handle the Find title operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFindClient_Click(object sender, EventArgs e) {
            doSelectClient();
        }

        /// <summary>
        /// cboShipCode_SelectedIndexChanged: handle change to shipping code and therefore cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboShipCode_SelectedIndexChanged(object sender, EventArgs e) {
            getShippingCost(cboShipCode.SelectedValue.ToString());
            _changed = true;
        }

        /// <summary>
        /// cmdFinditle_Click: raise an event to search for a title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFindTitle_Click(object sender, EventArgs e) {
            String searchText = txtBookSearch.Text;
            if (onSelectBook != null) {
                String bookId = String.Empty;
                if (onSelectBook(this, searchText, ref bookId)) {
                    addLine(bookId, 1);             
                    txtBookSearch.Text = String.Empty;
                    txtBookSearch.Focus();
                }
            }          
        }

        /// <summary>
        /// dgvLines_CellEndedit: handle the edited event for a quantity o recalc totals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLines_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == COL_QTY) {
                string newQty = dgvLines.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                _orderRec.Replace(BookConst.ORDERS_QTY, e.RowIndex + 1, newQty);
                showOrderLines();
            }
        }

        /// <summary>
        /// cmdSave_click: handle the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e) {
            if (validate()) {
                doSave();
            }
        }

        /// <summary>
        /// deleteToolStripMenuItem_Click: handle the Deleet item context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLines.Rows.Count == 0) {
                return;
            }
            if (dgvLines.CurrentCell == null) return;

            int row = dgvLines.CurrentCell.RowIndex + 1;
            removeRow(row);            
        }
        
        /// <summary>
        /// Handle the View context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLines.Rows.Count == 0) {
                return;
            }
            if (dgvLines.CurrentCell == null) return;
            int row = dgvLines.CurrentCell.RowIndex;
            string id = dgvLines.Rows[row].Cells[0].Value.ToString();
            // show the title details...
            if (onShowBook != null) {
                onShowBook(this, id, false, true);
            }
        }

        /// <summary>
        /// handle change of order date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpOrderDate_ValueChanged(object sender, EventArgs e)
        {
            _changed = true;
        }

        /// <summary>
        /// handle change of order status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            _changed = true;
        }

        /// <summary>
        /// handle change of order origin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrigin_SelectedValueChanged(object sender, EventArgs e)
        {
            _changed = true;
        }

        /// <summary>
        /// handle the close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close(); // this raises the Closing event to check for changes
        }

        /// <summary>
        /// handle form closing to check if record has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSalesOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkCanClose() == false) {
                e.Cancel = true;
            }
        }
        #endregion

        

        
    }
}
