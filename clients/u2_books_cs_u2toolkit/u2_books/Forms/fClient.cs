using System;
using System.Windows.Forms;
using U2.Data.Client.UO;
using u2_books.shared;

namespace u2_books.Forms
{
    public partial class fClient : Form
    {
        protected String _clientId = String.Empty;
        protected ClientData _clientData = new ClientData();
        Boolean _locked = false;

        public event ShowOrderReportEvent onShowOrder = null;

        public fClient() {
            InitializeComponent();
        }

        /// <summary>
        /// readClient: read the client details
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="forEdit"></param>
        /// <returns></returns>
        public Boolean readClient(String clientId, Boolean forEdit) {
            String errText = String.Empty;
            if (forEdit) {
                if (Server.Instance.lockRecord("U2_CLIENTS", clientId) == false) {
                    return false;
                }
                _locked = true;
            }

            ClientData cd = new ClientData();
            if (Server.Instance.getClientData(clientId, ref cd, ref errText) == false) {
                return false;
            }
            _clientId = clientId;
            _clientData = cd;
            showClientData();
            cmdSave.Visible = _locked;
            saveToolStripButton.Enabled = _locked;
            return true;
        }

        protected void showClientData() {
            txtClientId.Text = _clientData.ClientId;
            txtForename.Text = _clientData.Forename;
            txtSurname.Text = _clientData.Surname;
            txtJoinDate.Text = _clientData.JoinDate;
            txtAddress.Text = _clientData.Address;
            
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.DataSource = _clientData.OrderList;

            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.DataSource = _clientData.PaymentList;
        }

        /// <summary>
        /// dosave: this does the build and save directly from the client.
        /// </summary>
        /// <remarks>
        /// This should only be used for single file or simple updates.
        /// </remarks>
        protected void doSave() {
            // read and update the current record
            UniDynArray clientRec = null;
            if (Server.Instance.readRecord("U2_CLIENTS", _clientData.ClientId, ref clientRec) == false) {
                clientRec = Server.Instance.createArray();
            }
            clientRec.Replace(BookConst.CLIENTS_FORENAME, txtForename.Text);
            clientRec.Replace(BookConst.CLIENTS_SURNAME, txtSurname.Text);
            String iDate = String.Empty;
            if (Server.Instance.iconv(txtJoinDate.Text, "D", ref iDate)) {
                clientRec.Replace(BookConst.CLIENTS_JOIN_DATE, iDate);
            }
            clientRec.Replace(BookConst.CLIENTS_ADDRESS, txtAddress.Text.Replace(Server.CRLF, Server.VM_STR));

            if (Server.Instance.writeRecord("U2_CLIENTS", _clientId, clientRec)) {
                MessageBox.Show("Client details updated");
            }

        }

        protected void doClose() {
            Close();

        }
        protected void doRelease() {
            _locked = false;
            
        }
        private void cmdFetch_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtClientId.Text)) {
                return;
            }
            readClient(txtClientId.Text, false);
        }

        private void label6_Click(object sender, EventArgs e) {

        }

        private void txtJoinDate_TextChanged(object sender, EventArgs e) {

        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            String orderId = dgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (onShowOrder != null) {
                onShowOrder(this, orderId, "", this.Modal);
            }
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            String orderId = dgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (onShowOrder != null) {
                onShowOrder(this, orderId, "", this.Modal);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e) {
            doSave();
        }

        private void cmdClose_Click(object sender, EventArgs e) {
            doClose();
        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void saveToolStripButton_Click(object sender, EventArgs e) {
            doSave();
        }

        private void fClient_FormClosed(object sender, FormClosedEventArgs e) {
            if (_locked) {
                doRelease();
            }            
        }

        

    }
}
