using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using u2_books.shared;
using u2_books;

namespace u2_books.Forms
{
    /// <summary>
    /// The Book Maintenance demonstrates simple record based operations.
    /// Each book is a single U2 record. The details are returned through a stored procedure that also resolves
    /// some of the related information, including the names of the author, publisher and supplier.
    /// 
    /// For transactional operations using permenently connected clients like this, U2 systems typically
    /// use pessmisitic locking. When you edit a book the database places an exclusive update lock on the
    /// record to prevent others from editing it at the same time. This only prevents updates and does not
    /// block reads. In view mode, the lock is not set.
    /// 
    /// A second stored procedure is used to save any changes to the book.
    /// </summary>
    public partial class fBook : Form
    {
        protected BookData _data = null;
        protected Boolean _locked = false;

        public event ShowOrderHistoryEvent onShowOrderHistory = null;
        public event ShowPurchaseHistoryEvent onShowPurchaseHistory = null;


        public fBook() {
            InitializeComponent();
        }


        /// <summary>
        /// bindAll : bind book details class to the UI
        /// </summary>
        /// <remarks>
        /// There are many ways to render a class to the UI. Using data binding is one of the more convenient.
        /// </remarks>
        protected void bindAll() {
            // use data binding to the class to handle updates            
            txtAuthor.DataBindings.Add("Text", _data, "AuthorName");
            txtTitle.DataBindings.Add("Text", _data, "ShortTitle");
            txtISBN.DataBindings.Add("Text", _data, "ISBN");
            cboDept.DataBindings.Add("Text", _data, "Department");
            cboGenre.DataBindings.Add("Text", _data, "Genre");
            txtMedia.DataBindings.Add("Text", _data, "Media");
            txtPublisher.DataBindings.Add("Text", _data, "PublisherName");
            txtSupplier.DataBindings.Add("Text", _data, "SupplierName");
            txtSynopsis.DataBindings.Add("Text", _data, "LongDescription");

            txtStockLevel.DataBindings.Add("Text", _data, "StockLevel");
            txtSalePrice.DataBindings.Add("Text", _data, "SalesPrice");
            txtMinQty.DataBindings.Add("Text", _data, "MinOrderQty");
            txtPurchPrice.DataBindings.Add("Text", _data, "PurchasePrice");
            txtTaxCode.DataBindings.Add("Text", _data, "TaxCode");            
        }

        /// <summary>
        /// clearAll : clear all data bindings
        /// </summary>
        protected void clearAll() {
            txtAuthor.DataBindings.Clear();
            txtTitle.DataBindings.Clear() ;
            txtISBN.DataBindings.Clear();
            cboDept.DataBindings.Clear();
            cboGenre.DataBindings.Clear();
            txtMedia.DataBindings.Clear();
            txtPublisher.DataBindings.Clear();
            txtSupplier.DataBindings.Clear();
            txtSynopsis.DataBindings.Clear();

            txtStockLevel.DataBindings.Clear();
            txtSalePriceOld.DataBindings.Clear();
            txtMinQty.DataBindings.Clear();
            txtPurchPrice.DataBindings.Clear();
            txtTaxCode.DataBindings.Clear();
        }

        /// <summary>
        /// doAuthorSearch: run an author search
        /// </summary>
        protected void doAuthorSearch() {
            fSelection f = new fSelection();
            String query = BookConst.SEL_AUTHORS_QUERY;
            List<String> result = new List<String>();
            if (f.showSelection(query, "Author Search", "All Authors",  result) == false) {
                return;
            }            
            _data.AuthorId = result[0];
            txtAuthor.Text = result[1];
        }

        /// <summary>
        /// doClear: clear the current record and set up for a new one
        /// </summary>
        protected void doClear() {
            if (_locked) {
                doRelease();
            }
            clearAll();
            cmdSearch.Visible = true;
        }

        /// <summary>
        /// doRelease: release update lock held on the current record
        /// </summary>
        protected void doRelease() {
            _locked = false;
            Server.Instance.releaseBook(_data.BookId);
        }

        /// <summary>
        /// doSave: save the current book record without releasing the record lock
        /// </summary>
        protected void doSave() {
            if (Server.Instance.setBookData(_data.BookId, _data, true)) {
                MessageBox.Show("Book details updated");
            }
        }

        protected void doOrderHistory() {
            if (onShowOrderHistory != null) {
                onShowOrderHistory(this, _data.BookId, false);
            }        
        }

        protected void doPurchaseHistory() {
            if (onShowPurchaseHistory != null) {
                onShowPurchaseHistory(this, _data.BookId, false);
            }
        }

        protected void doPublisherSearch() {
            fSelection f = new fSelection();
            String query = BookConst.SEL_PUBLISHERS_QUERY;
            List<String> result = new List<String>();
            if (f.showSelection(query, "Publisher Search", "All Publishers", result) == false) {
                return;
            }            
            _data.PublisherId = result[0];
            txtPublisher.Text = result[1];
        }

        protected void doSupplierSearch() {
            fSelection f = new fSelection();
            String query = BookConst.SEL_SUPPLIERS_QUERY;
            List<String> result = new List<String>();
            if (f.showSelection(query, "Supplier Search", "All Suppliers", result) == false) {
                return;
            }
            _data.SupplierId = result[0];
            txtSupplier.Text = result[1];
        }

        protected void doTaxSearch() {
            fSelection f = new fSelection();
            String query = BookConst.SEL_TAXCODE_QUERY;
            List<String> result = new List<String>();
            if (f.showSelection(query, "Tax Code Search", "All Tax Codes", result) == false) {
                return;
            }
            _data.TaxCode = result[0];
            txtTaxCode.Text = result[1];
        }


        protected void doClose() {
            if (_locked) {
                doRelease();
            }
        }
        
        public Boolean readTitle(String bookId, Boolean withLock) {
            BookData tempData = new BookData();

            // only allow updating if locking
            cmdUpdate.Visible = withLock;
            saveToolStripButton.Visible = withLock;
            
            if (withLock == false) {
                Text = "View Book Details";
            }
            else {
                Text = "Edit Book Details";
            }

    
            if (Server.Instance.getBookData(bookId, withLock, tempData) == false) {
                return false;
            }

            txtBookId.Text = bookId;
            _data = tempData;
            _locked = withLock;
            bindAll();
            getImage();
            return true;
        }

        protected void getImage()
        {
            Image newImage = null;
            string errText = String.Empty;
            string bookId = txtBookId.Text;
            if (Server.Instance.getBookImage(bookId, ref newImage, ref errText) == false) {
                return;
            }
            picImage.Image = newImage;
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fBook_FormClosing(object sender, FormClosingEventArgs e) {
            doClose();
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e) {

        }

        private void fBook_Load(object sender, EventArgs e) {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e) {

        }

        private void cmdSearchAuthor_Click(object sender, EventArgs e) {
            doAuthorSearch();
        }

        private void cmdSearchPublisher_Click(object sender, EventArgs e) {
            doPublisherSearch();
        }

        private void cmdSearchSupplier_Click(object sender, EventArgs e) {
            doSupplierSearch();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void cmdSearchTaxCode_Click(object sender, EventArgs e) {
            doTaxSearch();
        }

        private void cmdOrderHistory_Click(object sender, EventArgs e) {
            doOrderHistory();
        }

        private void cmdPurchaseHistory_Click(object sender, EventArgs e) {
            doPurchaseHistory();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e) {

        }

        private void saveToolStripButton_Click(object sender, EventArgs e) {
            doSave();
        }

        private void cmdUpdate_Click(object sender, EventArgs e) {
            doSave();
        }

        private void newToolStripButton_Click(object sender, EventArgs e) {
            doClear();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {

        }

       

       
       
    }
}
