using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using u2_books;
using u2_books.shared;

namespace u2_books.Forms
{
    public partial class fClientSearch : Form
    {
        protected ClientSummaryList _list = null;
        protected Boolean _selecting = false;
        protected String _clientId = String.Empty;

        public event ShowClientEvent onShowClient = null;

        public fClientSearch() {
            InitializeComponent();
        }

        /// <summary>
        /// showClientSearch: run the client search form as a selection
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Boolean selectClient(ref String clientId) {
            _selecting = true;
            if (ShowDialog() == DialogResult.OK) {
                clientId = _clientId;
                return true;
            } 
            return false;
        }

        /// <summary>
        /// doSearch: run a search
        /// </summary>
        protected void doSearch() {           
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            int surnameType = cboSurnameType.SelectedIndex;
            int forenameType = cboForenameType.SelectedIndex;

            dgvClients.DataBindings.Clear();
            _list = new ClientSummaryList();

            if (Server.Instance.searchClient(firstName, forenameType, lastName, surnameType,_list)) {
                dgvClients.AutoGenerateColumns = false;
                dgvClients.DataSource = _list;
                dgvClients.Refresh();
            }
            if (_selecting) {
                cmdOk.Visible = (_list.Count > 0);
            }
        }

        protected void editClient(String clientId) {
            if (onShowClient != null) {
                onShowClient(this, clientId, true, _selecting);
            }
        }
        protected void viewClient(String clientId) {
            if (onShowClient != null) {
                onShowClient(this, clientId, false, _selecting);
            }
        }
        private void cmdSearch_Click(object sender, EventArgs e) {
            doSearch();
        }

        private void dgvClients_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            String clientId = dgvClients.Rows[e.RowIndex].Cells[0].Value.ToString();

            if (e.ColumnIndex == dgvClients.ColumnCount - 1) {
                editClient(clientId);
            }
            if (e.ColumnIndex == dgvClients.ColumnCount - 2) {
                viewClient(clientId);
            }            
        }

        private void dgvClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            String clientId = dgvClients.Rows[e.RowIndex].Cells[0].Value.ToString();
            if(_selecting){
                _clientId = clientId;
                DialogResult = DialogResult.OK;
            } else{
                editClient(clientId);            
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void cmdClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void fClientSearch_Load(object sender, EventArgs e) {
            cboForenameType.SelectedIndex = 0;
            cboSurnameType.SelectedIndex = 0;
        }

        private void cmdOk_Click(object sender, EventArgs e) {
            if (dgvClients.RowCount <= 0) {
                return;
            }
            if (dgvClients.SelectedRows.Count == 0) {
                return;
            }
            _clientId = dgvClients.SelectedRows[0].Cells[0].Value.ToString();
            DialogResult = DialogResult.OK;
        }

        
    }
}
