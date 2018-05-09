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
    public partial class fSelection : Form
    {
        public fSelection() {
            InitializeComponent();
        }
        protected DataTable results = null;
        protected List<String> _result = new List<String>();

        private void fSelection_Load(object sender, EventArgs e) {

        }

        protected void doSelection() {
            setResult();
            if (_result.Count > 0) {
                DialogResult = DialogResult.OK;
            }
        }

        public Boolean showSelection(String query, String title, String subtitle, List<String> result){
            _result = result;

            if(Server.Instance.getQueryAsTable(query, ref results) == false){
                Server.Instance.ShowError("No data selected");
                return false;
            }
            
            dataGridView1.DataSource = results;
            if (ShowDialog() == DialogResult.OK) {               
                return true;
            }
            return false;
        }

        private void cmdOk_Click(object sender, EventArgs e) {
            doSelection();
        }

        protected void setResult() {
            if (dataGridView1.SelectedRows.Count <= 0) {
                _result.Clear();
                return;
            }
            
            for (int i = 0; i < dataGridView1.ColumnCount; i++) {
                _result.Add(dataGridView1.SelectedRows[0].Cells[i].Value.ToString());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            doSelection();
        }
        
    }
}
