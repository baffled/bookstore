using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace u2_books.Forms
{
    public partial class fSalesCharts : Form
    {
        public fSalesCharts() {
            InitializeComponent();
        }

        private void cmdRun_Click(object sender, EventArgs e) {
            string result = string.Empty;
            string indata = string.Empty;
            string errtext = string.Empty;
            if( Server.Instance.callSub("u2_Visualize",indata, out result, out errtext) == false) {
                MessageBox.Show("cannot run the charts (sorry)");
                return;
            }
            if (string.IsNullOrEmpty(errtext) == false) {
                MessageBox.Show(errtext);
            } else {
                if (webBrowser1.Document == null) {
                    webBrowser1.DocumentText = result;
                } else {
                    webBrowser1.Document.OpenNew(true);
                    webBrowser1.Document.Write(result);
                }
            }
        }
    }
}
