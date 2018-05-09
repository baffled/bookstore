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
    public partial class fOrderSearch : Form
    {
        public fOrderSearch() {
            InitializeComponent();
            init();
        }

        public event ShowOrderReportEvent onShowOrder = null;
        public event ShowOrderEvent onEditOrder = null;

        protected SalesOrderDataList _list = new SalesOrderDataList();
        protected ClientSummaryList _clientList = new ClientSummaryList();

        protected void doSearch()
        {
            String orderId = string.Empty;
            if ((rbAllClients.Checked)
                && String.IsNullOrEmpty(txtSearchAfter.Text)
                && String.IsNullOrEmpty(txtSearchBefore.Text)) {
                MessageBox.Show("Please select a client or enter a date range");
                return;
            }


            string clientId = rbAllClients.Checked ? string.Empty  : cboClients.SelectedValue.ToString();
            string surname = string.Empty;
            string forename = string.Empty;
            string dateBefore = txtSearchBefore.Text;
            string dateAfter = txtSearchAfter.Text;
            SalesOrderDataList newList = new SalesOrderDataList();

            if (Server.Instance.searchOrders(orderId, clientId, surname, forename, dateAfter, dateBefore, newList) == false) {
                return;
            }
            if (newList.Count == 0) {
                MessageBox.Show("No results found");
                return;
            }

            _list = newList;
            
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.DataBindings.Clear();
            dgvOrders.DataSource = _list;
            dgvLines.DataSource = _list;
            dgvLines.DataMember = "Lines";

            dgvOrders.Refresh();
            
        }

        protected void editOrder(String orderId) {
            if (onEditOrder != null) {
                onEditOrder(this, orderId, true, this.Modal);
            }
        }

        protected void getClientList()
        {
            
            if (Server.Instance.searchClient(string.Empty, 0, string.Empty, 0, _clientList)) {
                cboClients.DataSource = _clientList;
                cboClients.DisplayMember = "FullName";
                cboClients.ValueMember = "ClientId";                
            }
        }

        protected void init()
        {
            getClientList();
        }

        protected void viewOrder(String orderId) {
            if (onShowOrder != null) {
                onShowOrder(this, orderId, "", this.Modal);
            }
        }
        
        private void button1_Click(object sender, EventArgs e) {
            doSearch();
            
        }

        private void cmdView_Click(object sender, EventArgs e) {
            String orderId = String.Empty;
            if (dgvOrders.SelectedRows.Count <= 0) {
                return;
            }
            orderId = dgvOrders.SelectedRows[0].Cells[0].Value.ToString();
            if (String.IsNullOrEmpty(orderId) == false) {
                viewOrder(orderId);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e) {
            String orderId = String.Empty;
            if (dgvOrders.SelectedRows.Count <= 0) {
                return;
            }
            orderId = dgvOrders.SelectedRows[0].Cells[0].Value.ToString();
            if (String.IsNullOrEmpty(orderId) == false) {
                editOrder(orderId);
            }
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            String orderId = String.Empty;
            if (dgvOrders.SelectedRows.Count <= 0) {
                return;
            }
            orderId = dgvOrders.SelectedRows[0].Cells[0].Value.ToString();
            if (String.IsNullOrEmpty(orderId) == false) {
                editOrder(orderId);
            }
        }

        private bool selectDate(string origDate, ref string newDate)
        {
            fDateSelect f = new fDateSelect();
            
            if (String.IsNullOrEmpty(origDate) == false) {
                Server.Instance.iconv(origDate, "D4",ref origDate);
            }

            if (f.showDateSelect(origDate, ref newDate)) {
                Server.Instance.oconv(newDate, "D4", ref newDate);
                return true;
            } 
            
            return false;
        }

        private void cmdSelBefore_Click(object sender, EventArgs e)
        {
            string origDate = txtSearchBefore.Text;
            string newDate = string.Empty;
            if (selectDate(origDate, ref newDate)) {
                txtSearchBefore.Text = newDate;
            }
        }

        private void cmdSelAfter_Click(object sender, EventArgs e)
        {
            string origDate = txtSearchAfter.Text;
            if (string.IsNullOrEmpty(origDate)) origDate = txtSearchBefore.Text;
            string newDate = string.Empty;
            if (selectDate(origDate, ref newDate)) {
                txtSearchAfter.Text = newDate;
            }
        }
    }
}
