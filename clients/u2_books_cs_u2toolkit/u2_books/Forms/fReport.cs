using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace u2_books.Forms
{
    /// <summary>
    /// The report form simply captures a server generated report.
    /// This is a quick enquiry only and does not include any formatting, and is therefore just a 
    /// simple demonstration of the enquiry syntax.
    /// 
    /// These 'reports' are enquiry commands whose output is captured as text and displayed. 
    /// </summary>
    public partial class fReport : Form
    {
        protected List<String> _pages = new List<String>();
        protected int _currentPage = 0;

        public fReport() {
            InitializeComponent();
        }

        public void showReport(String reportData, char pageDelim, Boolean isModal) {
            String[] pages = reportData.Split(pageDelim);
            _pages.AddRange(pages);
            if (_pages.Count > 0) {
                showPage(1);
            }
            if (isModal) {
                ShowDialog();
            } else {
                Show();
            }
        }

        protected void showPage(int pageNumber) {
            if (pageNumber > _pages.Count) {
                return;
            }
            lblPage.Text = String.Format("Page {0} of {1}", pageNumber, _pages.Count);
            _currentPage = pageNumber;
            txtText.Text = _pages[_currentPage - 1];
            txtText.SelectionLength = 0; // stop it showing the text as selected
        }

        private void fReport_Load(object sender, EventArgs e) {

        }

        private void tsbFirst_Click(object sender, EventArgs e) {
            showPage(1);
        }

        private void tsbPrevious_Click(object sender, EventArgs e) {
            if(_currentPage > 1){
                showPage( _currentPage - 1);
            }
        }

        private void tsbNext_Click(object sender, EventArgs e) {
            if (_currentPage < _pages.Count) {
                showPage(_currentPage + 1);
            }
        }

        private void tsbLast_Click(object sender, EventArgs e) {
            showPage(_pages.Count);
        }

        
    }
}
