using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;

using u2_books.shared;

namespace u2_books.Forms
{
    public partial class fConnect : Form
    {
        
        public fConnect() {
            InitializeComponent();
        }
        /// <summary>
        /// fConnect_Load
        /// </summary>
        /// <remarks>
        /// get the culture information from Windows to tell the database how we want dates
        /// and monetary values to be displayed. UniVerse defaults to a US date format, so
        /// for the rest of the world we need to send a command down to switch this:
        /// DATE.FORMAT ON
        /// UniVerse also typically uses US monetary formats, i.e. 1,234.56
        /// For European users this needs to switch to 1.234,56.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fConnect_Load(object sender, EventArgs e)
        {
            // get the current locale so we can set the date and monetary formats correctly.
            CultureInfo thisCulture = System.Globalization.CultureInfo.CurrentCulture;
            if (thisCulture.NumberFormat.CurrencyDecimalSeparator == ",") {
                Server.Instance.moneyFormat = u2_const.FORMAT_EU;
            } else {
                Server.Instance.moneyFormat = u2_const.FORMAT_UK;
            }
            if (thisCulture.DateTimeFormat.ShortDatePattern.StartsWith("MM")) {
                Server.Instance.dateFormat = u2_const.DATE_US;
            } else {
                Server.Instance.dateFormat = u2_const.DATE_SENSIBLE;
            }
            // also default the user name to the current Windows user.
            txtUser.Text = Environment.UserName;
        }

        /// <summary>
        /// ShowConnection : entry point to show the connection dialog.
        /// </summary>
        /// <returns>true on successful connection</returns>
        public Boolean ShowConnection() {
            
            cboDBType.SelectedIndex = 0;
            
            return (this.ShowDialog() == DialogResult.OK);
        }

        /// <summary>
        /// cmdConnect_Click: connection button handler
        /// </summary>
        /// <remarks>
        /// This calls the Server class to attempt a connection to the database.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConnect_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtHost.Text)) {
                MessageBox.Show("Please enter the host name of your server or localhost for a local connection");
                return;
            }
            if (String.IsNullOrEmpty(txtPath.Text)) {
                MessageBox.Show("Please enter the directory holding the u2_books database");
                return;
            }
            if(String.IsNullOrEmpty(txtUser.Text)){
                MessageBox.Show("Please enter your Windows user name to connect");
                return;
            }
            if(String.IsNullOrEmpty(txtPass.Text)){
                MessageBox.Show("Please enter a password for your Windows user name");
                return;
            }
            if (cboDBType.SelectedIndex < 0) {
                MessageBox.Show("Please select the database type");
                return;
            }

            String errText = String.Empty;
            String serviceName = (cboDBType.SelectedIndex == 1 ? "udcs" : "uvcs");
            if(Server.Instance.connect(txtHost.Text,txtUser.Text, txtPath.Text, txtPass.Text, serviceName, ref errText) == false){
                MessageBox.Show(errText);
                return;
            }

            

            if (Server.Instance.dateFormat != u2_const.DATE_US) {
                Server.Instance.Execute("DATE.FORMAT ON"); // turn on sensible DD/YY/MMMM date formatting
            }

            // now also run the init script: this ensures that all indexes have been build
            Server.Instance.Execute("INIT_BOOKS");

            DialogResult = DialogResult.OK;
        }


    }
}
