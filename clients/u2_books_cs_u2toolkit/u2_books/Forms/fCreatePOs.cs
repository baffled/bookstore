using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace u2_books
{
    /// <summary>
    /// fCreatePOs: call purchase order creation.
    /// </summary>
    /// <remarks>
    /// In this demonstration system, purchase orders are generated automatically to cover any stock
    /// reductions arising from the sales orders placed during a day.
    /// UniVerse and UniData both support background processing, so a routine like this would normally
    /// be run automatically at the end of the days' trading with no operator assitance.
    /// </remarks>
    public partial class fCreatePOs : Form
    {
        public event ShowPOEvent OnShowPO = null;
        public fCreatePOs()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        }

        private void cmdRun_Click(object sender, EventArgs e)
        {
            String intDate = String.Empty;
            String POList = String.Empty;
            String errText = String.Empty;

            Server.Instance.iconv(lblDate.Text, "D4", ref intDate);
            if (Server.Instance.createPOs(intDate, ref POList, ref errText)) {
                lstPOs.Items.Clear();
                lstPOs.Items.AddRange(POList.Split(Server.VM));
            }
            if (string.IsNullOrEmpty(errText) == false) {
                MessageBox.Show(errText);
                return;
            }
            if (String.IsNullOrEmpty(POList)) {
                MessageBox.Show("No purchase orders required");
                return;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            String orderId = String.Empty;
            bool forEdit = false;
            bool isModal = false;

            if (OnShowPO != null) {
                if (lstPOs.SelectedIndex >= 0) {
                    orderId = lstPOs.SelectedItem.ToString();
                    OnShowPO(this, orderId, forEdit, isModal);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
    }
}
