using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using u2_books.shared;

namespace u2_books
{
    public partial class fDateSelect : Form
    {

        public fDateSelect() {
            InitializeComponent();
        }

        private void fDateSelect_Load(object sender, EventArgs e) {

        }

        public Boolean showDateSelect(String origDate, ref String newDate) {
            DateTime thisDate = DateTime.Today;
            if (String.IsNullOrEmpty(origDate) == false) {
                thisDate = Utils.fromU2Date(Convert.ToInt32(origDate));
            }
            monthCalendar1.SelectionStart = thisDate;
            monthCalendar1.MaxSelectionCount = 1;
            if (ShowDialog() == DialogResult.OK) {
                newDate = Utils.toU2Date(monthCalendar1.SelectionStart).ToString();
                return true;
            }
            return false;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e) {
            DialogResult = DialogResult.OK;
        }
    }
}
