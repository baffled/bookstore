using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using u2_books.shared;

namespace u2_books.Forms
{
    /// <summary>
    /// The search form for the books demonstrates a very traditional search technique.
    /// The search calls server side stored procedures that return data in the UniVerse internal format
    /// known as a dynamic array. This is powerful but can be slower to extract data.
    /// See the other search forms for alternate techniques.
    /// </summary>
    public partial class fBookSearch : Form
    {
        // protected members
        Boolean _selecting = false;
        String _bookId = String.Empty;

        protected DataTable dtAuthors;
        protected DataTable dtPublishers;
        protected BookSummaryList _list = new BookSummaryList();

        // events
        public event ShowBookEvent onShowBook = null;
        

        public fBookSearch() {
            InitializeComponent();
        }
        /// <summary>
        /// showBookSearch : show the search as a launch form
        /// </summary>
        public void showBookSearch() {
            initForm();
            Show();
        }
        /// <summary>
        /// showBookSearch: show the search as a selection form
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        public Boolean showBookSearch(ref string bookID) {
            initForm();
            _selecting = true;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;

            cmdOk.Visible = true;
            this.AcceptButton = cmdOk;

            if (ShowDialog() == DialogResult.OK) {
                bookID = _bookId;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// showBookSearch: show the book search with a specific search pre-loaded.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Boolean showBookSearch( String searchText, ref string bookId){            
            _selecting =true;
            gbSearch.Visible = false;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            this.AcceptButton = cmdOk;

            txtKeywords.Text = searchText;
            rbKeyword.Checked = true;
            runSearch();
            cmdOk.Visible = true;

            if (ShowDialog() == DialogResult.OK) {
                bookId = _bookId;
                return true;
            }
            return false;
        }


        protected void initForm() {
            getAuthors();
            getPublishers();
        }

        protected void getAuthors() {
            if (Server.Instance.getAuthorsAsTable(ref dtAuthors)) {
                cboAuthors.DataSource = dtAuthors;
                cboAuthors.ValueMember = dtAuthors.Columns[0].ColumnName;
                cboAuthors.DisplayMember = dtAuthors.Columns[1].ColumnName;
            }
        }

        protected void getPublishers() {
            if (Server.Instance.getPublishersAsTable(ref dtPublishers)) {
                cboPublishers.DataSource = dtPublishers;
                cboPublishers.ValueMember = dtPublishers.Columns[0].ColumnName;
                cboPublishers.DisplayMember = dtPublishers.Columns[1].ColumnName;
            }
        }

        protected void runSearch() {
            int searchType = 0;
            String searchData = String.Empty;
            String errText = String.Empty;
            
            
            if (rbAuthor.Checked) {
                searchType = 0;
                searchData = cboAuthors.SelectedValue.ToString();
            }
            if (rbPublisher.Checked) {
                searchType = 1;
                searchData = cboPublishers.SelectedValue.ToString();
            }
            if (rbKeyword.Checked) {
                searchType = 2;
                searchData = txtKeywords.Text;
            }
            BookSummaryList newList = new BookSummaryList();
            if(Server.Instance.searchBooks(searchType, searchData, newList)){
                // clear the existing content
                dgvBooks.DataBindings.Clear();                
                dgvBooks.ClearSelection();
                // show the new content
                dgvBooks.AutoGenerateColumns = false;
                // sadly we have to assign a new list each time or the grid will not refresh correctly.
                dgvBooks.DataSource = newList;
                _list = newList;
                if (_list.Count == 0) {
                    MessageBox.Show("Sorry no matching books found");    
                }
                try {
                    dgvBooks.Refresh();
                }
                catch { };
                
            }
            
        }

        private void cmdSearch_Click(object sender, EventArgs e) {
            runSearch();
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            String bookId = dgvBooks.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (string.IsNullOrEmpty(bookId) == false)
            {                
                if (e.ColumnIndex == dgvBooks.ColumnCount - 1) {
                    editBook(bookId);
                }
                if (e.ColumnIndex == dgvBooks.ColumnCount - 2)
                {
                    viewBook(bookId);
                }
            }
        }

        protected void editBook(String bookId) {
            if (onShowBook != null)
            {
                onShowBook(this, bookId, true, _selecting);
            }
        }

        protected void viewBook(String bookId)
        {
            if (onShowBook != null)
            {
                onShowBook(this, bookId, false, _selecting);
            }
        }

        private void fBookSearch_Load(object sender, EventArgs e) {

        }

        private void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {            
            String bookId = dgvBooks.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (_selecting) {
                _bookId = bookId;
                DialogResult = DialogResult.OK;
            } else {
                editBook(bookId);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void cmdOk_Click(object sender, EventArgs e) {
            if (dgvBooks.SelectedRows.Count > 0) {
                String bookId = dgvBooks.SelectedRows[0].Cells[0].Value.ToString();
                _bookId = bookId;
                DialogResult = DialogResult.OK;
            }
        }

        // emulate a grid that knows what it's doing...
        private void dgvBooks_KeyDown(object sender, KeyEventArgs e) {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Space)) {
                cmdOk_Click(this, null);
            }
        }

        private void dgvBooks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // placeholder required to prevent the data grid throwing unnecessary errors when clearing content.
        }

        private void txtKeywords_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKeywords.Text) == false) {
                rbKeyword.Checked = true;
            }
        }
    }
}
