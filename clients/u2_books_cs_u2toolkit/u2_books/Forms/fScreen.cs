using System;
using System.Collections.Generic;
using System.Windows.Forms;

using U2.Data.Client.UO;

namespace u2_books.Forms
{
    public partial class fScreen : Form
    {
        protected ScreenDefn _defn = null;
        protected UniDynArray _fieldList = null;

        public fScreen() {
            InitializeComponent();
        }

        protected void paintDefinition() {
            // open the file from the definition
            if (Server.Instance.openFile(_defn.mainFile) == false) {
                throw new Exception("Cannot open file " + _defn.mainFile);
            }

            Text = _defn.heading;
            if (_defn.displayOnly) {
                cmdSave.Visible = false;
            }
            if (_defn.screenHeight > 0) {
                Height = _defn.screenHeight;
            }
            if (_defn.screenWidth > 0) {
                Width = _defn.screenWidth;
            }

            // build the display list
            ucScreen1.Screen = _defn;
            _fieldList = Server.Instance.createArray();
            for (int i = 1; i <= _defn.fieldList.Count; i++) {
                _fieldList.Replace(i, _defn.fieldList[i - 1].name);
            }
            
        }

        public ScreenDefn Defn {
            set {
                _defn = value;
                paintDefinition();
            }
            get { return _defn; }
        }

        #region actions

        protected void doNewRecord() {
            ucScreen1.Clear();
        }

        protected void doSave() {
            if (ucScreen1.Write()) {
                MessageBox.Show("Record written");
            }
        }

        protected void doClose() {
            Close();
        }

        #endregion


        #region Wire up screen events

        private bool ucScreen1_onDelete(object sender, string fileName, string key) {
            return Server.Instance.deleteRecord(fileName, key);
        }

        private bool ucScreen1_onIConv(object sender, string origValue, string code, ref string newValue) {
            return Server.Instance.iconv(origValue, code, ref newValue);
        }

        private bool ucScreen1_onOConv(object sender, string origValue, string code, ref string newValue) {
            return Server.Instance.oconv(origValue, code, ref newValue);
        }

        private bool ucScreen1_onRead(object sender, string fileName, string key, bool locking, ref bool isNew, ref UniDynArray rec) {
            isNew = false;
            if (locking) {
                if (Server.Instance.lockRecord(fileName, key) == false) {
                    if (Server.Instance.lastErrorCode != "30001") {
                        MessageBox.Show("Cannot get record lock on " + key);
                        return false;
                    }
                }
            }
            if (Server.Instance.readRecord(fileName, key, ref rec) == false) {
                if (Server.Instance.lastErrorCode != "30001") {
                    MessageBox.Show("Error reading record " + key);
                    return false;
                } else {
                    isNew = true;
                    rec = Server.Instance.createArray();
                }
            }
            return true;
        }

        private bool ucScreen1_onRelease(object sender, string fileName, string key) {
            return Server.Instance.releaseRecord(fileName, key);
        }

        private bool ucScreen1_onWrite(object sender, string fileName, string key, UniDynArray rec) {
            return Server.Instance.writeRecord(fileName, key, rec);
        }

        private void ucScreen1_onDisplay(object sender, ref UniDynArray image) {
            if(String.IsNullOrEmpty(_defn.mainDict)){
                _defn.mainDict = _defn.mainFile;
            }
            Server.Instance.getScreenImage(_defn.mainDict, _fieldList.StringValue, ucScreen1.id, ucScreen1.rec, ref image);
        }

        private bool ucScreen1_onDateSelect(object sender, string inputDate, ref string newValue) {
            fDateSelect f = new fDateSelect();
            return f.showDateSelect(inputDate, ref newValue);
        }

        private bool ucScreen1_onCheckRelation(object sender, string fileName, string key) {
            UniDynArray rec = Server.Instance.createArray();
            if (Server.Instance.openFile(fileName) == false) {
                return false;
            }
            if (Server.Instance.readRecord(fileName, key, ref rec)) {
                return true;
            }
            return false;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

        }

        private void ucScreen1_onShowHint(object sender, string hint) {
            lblHint.Text = hint;
        }

        #endregion

        #region Wire up Field Events

        protected bool callFieldEvent(string subrName, string fieldName, string eventName, ref UniDynArray outData, ref UniDynArray actions)
        {
            try {
                List<String> args = new List<String>();
                args.Add(fieldName);
                args.Add(eventName);
                args.Add(ucScreen1.id);
                args.Add(ucScreen1.rec.StringValue);
                args.Add(outData.StringValue);
                args.Add(string.Empty);
                Server.Instance.callSub(subrName, args);

                outData = new UniDynArray(Server.Instance.Session, args[4]);
                actions = new UniDynArray(Server.Instance.Session, args[5]);
                if (args[3] != ucScreen1.rec.StringValue) {
                    ucScreen1.setRecord( new UniDynArray(Server.Instance.Session, args[3]));
                }
            }
            catch (Exception ex) {
                return false;
            }
            return true;
        }
        
        #endregion

        protected Boolean getprompts(ref string query)
        {
            if (query.IndexOf("<<") < 0) {
                return true;
            }

            fInlinePrompts f = new fInlinePrompts();
            return f.showPrompts(ref query);
            return true;
        }

        #region toolbars and buttons

        private void newToolStripButton_Click(object sender, EventArgs e) {
            doNewRecord();
        }

        private void cmdSave_Click(object sender, EventArgs e) {
            doSave();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e) {
            doSave();
        }

        private void openToolStripButton_Click(object sender, EventArgs e) {

        }

        private void cmdClose_Click(object sender, EventArgs e) {
            doClose();
        }

        #endregion

        private void ucScreen1_Load(object sender, EventArgs e) {

        }

        

        private void fScreen_FormClosing(object sender, FormClosingEventArgs e) {
            ucScreen1.Closing = true;
        }

        private bool ucScreen1_onFileSelect(object sender, string fieldName, string fileName, string selection, ref string newValue) {            
            
            String query = selection;
            if (getprompts(ref query) == false) {
                return false;
            }

            fSelection f = new fSelection();
            List<String> result = new List<String>();
            if (f.showSelection(query, "Browse " + fieldName, "", result) == false) {
                return false;
            }
            newValue = result[0];
            return true;
        }

        private bool ucScreen1_FieldOnEntryEvent(object sender, string subrName, FieldDefn f, ref UniDynArray outData, ref UniDynArray actions)
        {
            return callFieldEvent(subrName, f.name, "ONENTRY", ref outData, ref actions);
        }

        #region Public Properties

        public string Id
        {
            get { return ucScreen1.id; }
            set { ucScreen1.id = value; }
        }
        public string Record
        {
            get { return ucScreen1.rec.StringValue; }
        }
        #endregion
    }
}
