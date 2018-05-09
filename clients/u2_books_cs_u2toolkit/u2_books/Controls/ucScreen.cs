using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using U2.Data.Client.UO;

using u2_books.shared;
namespace u2_books
{
    public partial class ucScreen : UserControl
    {
        public ucScreen() {
            InitializeComponent();
        }

        private String _id = String.Empty;
        private UniDynArray _rec = null;
        private ScreenDefn _defn = null;
        private String _origRec = String.Empty;

        private Double _unitWidth = 0;
        private Int32 _lineHeight = 22;
        private String[] _keyParts = new String[10];
        private Int32 _noKeys = 0;
        private UniDynArray _image = null;
        private Boolean _refreshing = false;
        private Boolean _locked = false;
        private Boolean _changed = false;
        private Control _firstKeyField = null;
        private Boolean _closing = false;
        private String _newRefs = String.Empty;
        private Boolean _useInternalDateSelect = false;
        private Int32 _lastKeyPart = 0;
        private TabControl _tabControl = null;
        
        private Char VM = Convert.ToChar(253);
        private String VMS = String.Format("{0}", Convert.ToChar(253));


        private Dictionary<String, Control> _entryControls = new Dictionary<string, Control>();
        private Dictionary<String, FieldDefn> _entryDefs = new Dictionary<string, FieldDefn>();

        public event DisplayScreenEvent onDisplay = null;
        public event ConvDataEvent onIConv = null;
        public event ConvDataEvent onOConv = null;
        public event ReadEvent onRead = null;
        public event WriteEvent onWrite = null;
        public event DeleteEvent onDelete = null;
        public event ReleaseEvent onRelease = null;
        public event ShowStatusEvent onShowStatus = null;
        public event ShowCanBrowseEvent onShowCanBrowse = null;
        public event showHintEvent onShowHint = null;
        public event CheckRelationEvent onCheckRelation = null;
        public event FileSelectEvent onFileSelect = null;
        public event DateSelectEvent onDateSelect = null;
        public event ReadEvent onReadRemote = null;
        public event AcculatorEvent onAccmulator = null;
        public event GetDefaultValueEvent onGetDefaultValue = null;
        public event UpdateReferenceEvent onUpdateReference = null;

        public event ScreenCallEvent ScreenOnReadEvent = null;
        public event ScreenCallEvent ScreenOnWriteEvent = null;
        public event ScreenCallEvent ScreenOnDeleteEvent = null;
        public event ScreenCallEvent ScreenOnAfterReadEvent = null;
        public event ScreenCallEvent ScreenOnAfterWriteEvent = null;
        public event ScreenCallEvent ScreenOnAfterDeleteEvent = null;
        public event ScreenCallEvent ScreenOnReleaseEvent = null;
        public event ScreenCallEvent ScreenOnValidEvent = null;
        public event ScreenCallEvent ScreenOnClearEvent = null;
        public event ScreenCallEvent ScreenOnDisplayEvent = null;
        public event ScreenCallEvent ScreenOnNewEvent = null;
        public event ScreenCallEvent ScreenOnDefaultEvent = null;
        public event ScreenCallEvent ScreenOnEnterEvent = null;
        public event ScreenCallEvent ScreenOnCloseEvent = null;
        

        public event FieldCallEvent FieldOnChangeEvent = null;
        public event FieldCallEvent FieldOnEntryEvent = null;
        public event FieldCallEvent FieldOnValidEvent = null;
        public event FieldCallEvent FieldOnDefaultEvent = null;

        const int DEF_ROWHEIGHT = 22;
        const int COMBO_WIDTH = 25;
        const int CHECKBOX_WIDTH = 16;

        private Int32 cx(Double coord) {
            return (int)coord;
        }

        private Int32 cy(Double coord) {
            return Convert.ToInt32(coord);            
        }

        /// <summary>
        /// Add a Column
        /// </summary>
        /// <param name="g"></param>
        /// <param name="f"></param>
        private void addColumn(DataGridView g, FieldDefn f) {
            DataGridViewColumn col = new DataGridViewColumn();
            
            if (String.IsNullOrEmpty(f.list) == false) {
                DataGridViewComboBoxCell cbcell = new DataGridViewComboBoxCell();
                cbcell.Items.AddRange(f.list.Split(','));
                col.CellTemplate = cbcell;
            } else {
                // TBD handle boolean
                DataGridViewTextBoxCell tbcell = new DataGridViewTextBoxCell();
                col.CellTemplate = tbcell;
                // TBD handle read only cells
            }
            if (f.entryType != FieldEntryType.Enter) {
                // TBDcol.CellTemplate.ReadOnly = true;
            }
            col.CellTemplate.Style.BackColor = getColor(f);
            

            col.Name = "__" + f.name.Replace(".", "_");
            col.HeaderText = f.colHead;
            col.Width = cx(f.length);
            if (String.IsNullOrEmpty(f.list) == false) {
                col.Width = col.Width + COMBO_WIDTH;
            }
            g.Columns.Add(col);

            _entryDefs.Add(col.Name, f);

            
        }
        /// <summary>
        /// addElement: add a visual non-bound element to the screen
        /// </summary>
        /// <param name="f"></param>
        private void addElement(FieldDefn f) {
            Control parent = null;
            switch (f.toolType) {
                case FieldToolType.Frame:
                    GroupBox g = new GroupBox();
                    g.Text = f.colHead;
                    parent = getControlParent(f.group);
                    g.Location = new Point(cx(f.left) - parent.Left, cy(f.top) - parent.Top);
                    g.Width = cx(f.width);
                    g.Height = cy(f.height);
                    parent.Controls.Add(g);
                    break;
                case FieldToolType.TabSet:
                    if (_tabControl != null) {
                        // can't have more than one
                        return;
                    }
                    TabControl c = new TabControl();
                    String[] tabs = f.colHead.Split(',');
                    for (int i = 0; i < tabs.Length; i++) {
                        c.TabPages.Add(tabs[i]);
                    }
                    c.Location = new Point(cx(f.left), cy(f.top));
                    c.Width = cx(f.width);
                    c.Height = cy(f.height);
                    _tabControl = c;
                    Controls.Add(c);
                    break;
            }
        }

        /// <summary>
        /// asCheckBoxValue
        /// </summary>
        /// <param name="f"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Boolean asCheckBoxValue(FieldDefn f, String value) {
            if (String.IsNullOrEmpty(f.list) == false) {
                if (value == Utils.field(f.list, ",", 1, 1)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (Utils.safeInt(value) == 0) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        /// <summary>
        /// assignField
        /// </summary>
        /// <param name="f"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        private void assignField(FieldDefn f, Int32 row, String value) {
            // assume here the value has been iconved
            if (f.keypart > 0) {
                _keyParts[f.keypart - 1] = value;
                if (gotKeyParts()) {
                    id = makeKey();
                }
                return;
            }

            if (f.fno == 0) {
                id = value;
                return;
            }
            if (rec.Extract(f.fno, row).StringValue != value) {
                rec.Replace(f.fno, row, value);
                Changed = true;                
            }
        }

        /// <summary>
        /// buildScreen
        /// </summary>
        private void buildScreen() {
            
            if (_defn == null) return;

            Control ctl;
            Control parent;

            this.SuspendLayout();

            // draw elements first since the prompts may need to go on top
            if (_defn.elementList != null) {
                foreach (FieldDefn f in _defn.elementList) {
                    addElement(f);
                }
            }

            // draw the prompts first
            if (_defn.promptList != null) {
                foreach (PromptDefn p in _defn.promptList) {

                    Label l = new Label();
                    l.Location = new Point(cx(p.left), cy(p.top));
                    l.AutoSize = true;
                    l.Height = _lineHeight;
                    l.Text = p.text;
                    parent = getControlParent(p.group);
                    parent.Controls.Add(l);
                }
            }

            // now the entry controls

            // dependent fields won't be here..
            if(_defn.fieldList != null){
                foreach (FieldDefn f in _defn.fieldList) {
                    ctl = null;
                    // controller field
                    if ((f.depth > 0) && (f.toolType != FieldToolType.Memo)) {
                        // it's a grid
                        DataGridView g = new DataGridView();
                        g.RowHeadersVisible = false;
                        g.AllowUserToAddRows = false;

                        ctl = g;
                        int fullWidth = 10;
                        g.Location = new Point(cx(f.left), cy(f.top));
                        ctl.Name = "_grid_" + f.name.Replace(".", "_");
                        g.Height = (f.depth + 2) * _lineHeight; // space for column headers and additional
                        fullWidth = fullWidth + cx(f.length);
                        if (String.IsNullOrEmpty(f.list) == false) {
                            fullWidth = fullWidth + COMBO_WIDTH;
                        }

                        addColumn(g, f);
                        _entryControls.Add(f.name, g);
                        _entryDefs.Add(g.Name, f);

                        // now any dependents
                        if (f.dependents != null) {
                            foreach (FieldDefn dep in f.dependents) {
                                addColumn(g, dep);
                                fullWidth = fullWidth + cx(dep.length);

                            }
                        }

                        g.Width = fullWidth;
                        if (f.entryType == FieldEntryType.Enter) {
                            g.CellValidated += new DataGridViewCellEventHandler(g_CellValidated);
                            g.CellEndEdit += new DataGridViewCellEventHandler(g_CellEndEdit);
                            g.CellValidating += new DataGridViewCellValidatingEventHandler(g_CellValidating);
                            g.CellEnter += new DataGridViewCellEventHandler(g_CellEnter);
                            g.KeyDown += new KeyEventHandler(g_KeyDown);

                        }

                        parent = getControlParent(f.group);
                        parent.Controls.Add(g);

                    };

                    // single valued field
                    if ((f.depth == 0) || (f.toolType == FieldToolType.Memo)) {

                        if (f.toolType == FieldToolType.Memo) {
                            TextBox m = new TextBox();
                            m.Multiline = true;
                            m.Height = f.depth * _lineHeight;
                            ctl = m;
                        } else {
                            if (f.toolType == FieldToolType.Check) {
                                CheckBox cb = new CheckBox();
                                ctl = cb;
                            } else {
                                if (String.IsNullOrEmpty(f.list) == false) {
                                    // it's a combo box
                                    ComboBox c = new ComboBox();
                                    ctl = c;
                                    c.Items.AddRange(f.list.Split(','));
                                    if (f.listType == FieldListType.NonExclusive) {
                                        c.DropDownStyle = ComboBoxStyle.DropDown;
                                    } else {
                                        c.DropDownStyle = ComboBoxStyle.DropDownList;
                                    }
                                } else {
                                    TextBox t = new TextBox();
                                    ctl = t;
                                }
                            }
                        }


                        if (f.entryType == FieldEntryType.Enter) {
                            ctl.Validated += new EventHandler(ctl_Validated);
                            ctl.GotFocus += new EventHandler(ctl_GotFocus);
                            ctl.KeyDown += new KeyEventHandler(ctl_KeyDown);
                            if (f.toolType == FieldToolType.Check) {
                                ((CheckBox)ctl).CheckedChanged += new EventHandler(ctl_CheckedChanged);
                            }
                        }

                        ctl.Location = new Point(cx(f.left), cy(f.top));
                        ctl.Name = "__" + f.name.Replace(".", "_");
                        ctl.Width = cx(f.length);
                        if (ctl is ComboBox) ctl.Width = ctl.Width + COMBO_WIDTH; // room for drop down button
                        if (ctl is CheckBox) {
                            ctl.Width = CHECKBOX_WIDTH;
                            ctl.Height = CHECKBOX_WIDTH;
                        }

                        if (f.entryType == FieldEntryType.Calc || f.entryType == FieldEntryType.ReadOnly) {

                            ctl.TabStop = false;
                        }
                        ctl.BackColor = getColor(f);


                        _entryControls.Add(f.name, ctl);
                        _entryDefs.Add(ctl.Name, f);
                        parent = getControlParent(f.group);
                        parent.Controls.Add(ctl);

                        if (f.isKeyField() || (String.IsNullOrEmpty(f.relates) == false)) {
                            if (_firstKeyField == null) {
                                _firstKeyField = ctl;
                            }
                            if (f.keypart > _lastKeyPart) {
                                _lastKeyPart = f.keypart;
                            }
                            Button btn = new Button();
                            btn.Name = "browse" + ctl.Name;
                            btn.Text = "...";
                            btn.Location = new Point(ctl.Left + ctl.Width + 1, ctl.Top);
                            btn.Width = 20;
                            btn.Height = ctl.Height;
                            btn.Click += new EventHandler(btn_Click);

                            parent.Controls.Add(btn);
                        }
                    }
                }
                
            }
            this.ResumeLayout();

        }

        /// <summary>
        /// CellEndEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void g_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (_refreshing) return;
            if (_closing) return;

            String ctlName = ((Control)sender).Name;

            FieldDefn f = _entryDefs[ctlName]; // controller
            if (f.entryType != FieldEntryType.Enter) {
                return;
            }

            if (e.ColumnIndex > 0) {
                f = f.dependents[e.ColumnIndex - 1];
            }

            String value = String.Empty;
            if (((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) {
                value = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            
            updateRecord(f, e.RowIndex + 1, value);
          
        }

        /// <summary>
        /// Grid KeyDown - handle lookups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void g_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F3) {
                doBrowse();
            }           
        }

        /// <summary>
        /// CellEnter: handle focus for a multivalued cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void g_CellEnter(object sender, DataGridViewCellEventArgs e) {
            if (_refreshing) return;
            String ctlName = ((Control)sender).Name;
            FieldDefn f = _entryDefs[ctlName]; // always the controller
            if (e.ColumnIndex > 0) {
                f = f.dependents[e.ColumnIndex - 1];
            }
            showFocussed(f);            
        }

        /// <summary>
        ///  CellValidating - validate a multivalued cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void g_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (_refreshing) return;
            if (_closing) return;
            return;

        }

        /// <summary>
        /// CellValidated: process a multivalued cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void g_CellValidated(object sender, DataGridViewCellEventArgs e) {
            if (_refreshing) return;
            if (_closing) return;
            return; // to many bloody bugs in the grid.          
        }

        /// <summary>
        /// ctl_KeyDown: check for browse key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void ctl_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F3) {
                doBrowse();
            }
        }
        

        

        void btn_Click(object sender, EventArgs e) {
            String name = (sender as Button).Name;
            if (name.StartsWith("browse")) {
                name = name.Substring(6);
                FieldDefn f = _entryDefs[name];
                String newValue = String.Empty;
                Boolean isKey = false;
                if (lookupEntry(f, 0, ref newValue, ref isKey)) {
                    if (isKey) {
                        id = newValue;
                    } else {
                        updateRecord(f, 0, newValue);
                    }
                }
            }
               
        }

        /// <summary>
        /// checkChanged: check if the record has changed
        /// </summary>
        /// <returns></returns>
        private Boolean checkChanged(){
            if(_changed){
                DialogResult ok = MessageBox.Show("Record has changed. Do you want to save?", Application.ProductName, MessageBoxButtons.YesNoCancel);
                if (ok == DialogResult.Yes) {
                    return doWrite();
                }
                if (ok == DialogResult.No) {
                    Changed = false;
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// checkDefaultKey: see if the key field is fixed using a default value
        /// </summary>
        public void checkDefaultKey() {
            if (_noKeys > 0) return;
            foreach(FieldDefn f in _defn.fieldList){
                if(f.isKeyField() && (f.keypart == 0)){
                    if(String.IsNullOrEmpty(f.defaultValue) == false){
                        id = getDefault(f);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Boolean checkRelation(String fileName, string key) {
            if (onCheckRelation == null) {
                return false;
            }
            return onCheckRelation(this, fileName, key);
        }

        /// <summary>
        /// clear: clear current record content (does not release etc).
        /// </summary>
        private void clearContent() {
            _id = String.Empty;
            _rec = Server.Instance.createArray() ;
            _origRec = String.Empty;
            _locked = false;
            for (int i = 0; i < _lastKeyPart; i++) {
                _keyParts[i] = string.Empty;
            }
            _image = Server.Instance.createArray();
            refreshScreen();
            _firstKeyField.Focus();
        }

        /// <summary>
        /// doBrowse
        /// </summary>
        private void doBrowse() {
            FieldDefn f = null;
            Control ctl = null;
            Int32 col = 0;
            Int32 row = 0;
            if (getActiveControl(ref ctl, ref col, ref row, ref f) == false) {
                return;
            }
            
            String newValue = String.Empty;
            Boolean isKey = false;
            if (lookupEntry(f, row, ref newValue, ref isKey)) {
                if (isKey) {
                    id = newValue;
                } else {
                    updateRecord(f, row, newValue);                    
                }
            }
        }

        /// <summary>
        /// doClear: run the full clear from outside
        /// </summary>
        /// <returns></returns>
        private Boolean doClear() {
            UniDynArray outdata = null;
            UniDynArray actions = null;
            if (checkChanged() == false) {
                return false;
            }
            doRelease();
            
            if (String.IsNullOrEmpty(_defn.onClear) == false) {
                if (ScreenOnClearEvent != null) {
                    if (ScreenOnClearEvent(this, _defn.onClear, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            }

            clearContent();
            setKeyEntry(true);
            return true;
        }

        /// <summary>
        /// doDelete
        /// </summary>
        /// <returns></returns>
        private Boolean doDelete() {
            UniDynArray outdata = null;
            UniDynArray actions = null;
            if (String.IsNullOrEmpty(_defn.onDelete) == false) {
                if (ScreenOnDeleteEvent != null) {
                    if (ScreenOnWriteEvent(this, _defn.onDelete, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            } else {

                if (onDelete == null) {
                    return false;
                }
                if (onDelete(this, _defn.mainFile, _id) == false) {
                    MessageBox.Show("Error deleting record");
                    return false;
                }
            }
            Changed = false;
            // TBD do reference lists

            if (String.IsNullOrEmpty(_defn.onAfterDelete) == false) {
                if (ScreenOnAfterDeleteEvent != null) {
                    if (ScreenOnAfterDeleteEvent(this, _defn.onAfterDelete, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            }
            return true;
        }

        /// <summary>
        /// doEnter
        /// </summary>
        /// <param name="f"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        private void doEnter(FieldDefn f, Int32 row, String value) {
            
            updateRecord(f, 0, value);
        }
        /// <summary>
        /// doDefaults: set any defaults on a new record
        /// </summary>
        private void doNewRecordDefaults() {
            UniDynArray outdata = null;
            UniDynArray actions = null;

            if (String.IsNullOrEmpty(_defn.onDefault) == false) {
                if (ScreenOnDefaultEvent != null) {
                    if (ScreenOnDefaultEvent(this, _defn.onDefault, ref outdata, ref actions) != false)
                        doSubrActions(actions);
                }
            }

            foreach (FieldDefn f in _defn.fieldList) {
                if (f.defaultValue != String.Empty) {
                    if (f.depno == 0) { // dependents only calc when new line added
                        String value = getDefault(f);
                        assignField(f, 0, value);
                    }
                }
            }
        }

        /// <summary>
        /// doRelease: release the old record
        /// </summary>
        /// <returns></returns>
        private Boolean doRelease() {
            
            UniDynArray outdata = null;
            UniDynArray actions = null;

            if (String.IsNullOrEmpty(_id)) {
                return true;
            }
            if (_locked == false) {
                return true;
            }
            if (String.IsNullOrEmpty(_defn.onRelease) == false) {
                if (ScreenOnReleaseEvent != null) {
                    if (ScreenOnReleaseEvent(this, _defn.onRelease, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            } else {
                if (onRelease(this, _defn.mainFile, _id) == false) {
                    return false;
                }
            }
            _locked = false;
            return true;
        }

        /// <summary>
        /// doRead
        /// </summary>
        /// <returns></returns>
        private Boolean doRead() {
            
            if (String.IsNullOrEmpty(_id)) {
                return false;
            }

            Boolean locking = true;
            Boolean isNew = false;
            
            UniDynArray outData = null;
            UniDynArray actions = null;

            if(_defn.displayOnly) locking = false;
            if (String.IsNullOrEmpty(_defn.onRead) == false) {
                if (ScreenOnReadEvent != null) {
                   
                    if (ScreenOnReadEvent(this, _defn.onRead, ref outData, ref actions) == false) {
                        return false;
                    }
                }
            } else {
                if (onRead(this, _defn.mainFile, _id, locking, ref isNew, ref _rec) == false) {
                    MessageBox.Show("Error reading " + _id);
                    return false;
                }
            }
            _origRec = _rec.StringValue;
            if (isNew) doNewRecordDefaults();
            
            doSubrActions(actions);

            if (_defn.displayOnly == false) _locked = true;

            if (String.IsNullOrEmpty(_defn.onAfterRead) == false) {
                if (ScreenOnAfterReadEvent != null) {
                    if (ScreenOnAfterReadEvent(this, _defn.onAfterRead, ref outData, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            }
            
            getScreenImage();
            Changed = false;
                
            return true;
        }

        /// <summary>
        /// doSubrActions: handle actions returned from handler
        /// </summary>
        /// <param name="actions"></param>
        private void doSubrActions(UniDynArray actions) {
            if (actions == null) return;
            // TBD actions
        }

        /// <summary>
        /// doWrite
        /// </summary>
        /// <returns></returns>
        private Boolean doWrite() {
            UniDynArray outdata = null;
            UniDynArray actions = null;
            if (String.IsNullOrEmpty(_defn.onWrite) == false) {
                if (ScreenOnWriteEvent != null) {
                    if (ScreenOnWriteEvent(this, _defn.onWrite, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            } else {

                if (onWrite == null) {
                    return false;
                }
                if (onWrite(this, _defn.mainFile, _id, _rec) == false) {
                    MessageBox.Show("Error writing record");
                    return false;
                }
            }
            Changed = false;
            // do reference lists
            updateReferences();

            // and call the after write routine
            if (String.IsNullOrEmpty(_defn.onAfterWrite) == false) {
                if (ScreenOnAfterWriteEvent != null) {
                    if (ScreenOnAfterWriteEvent(this, _defn.onAfterWrite, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                }
            }
            return true;
        }

        /// <summary>
        /// defaultDependents
        /// </summary>
        /// <param name="f"></param>
        /// <param name="row"></param>
        private void defaultDependents(FieldDefn f, Int32 row) {
            foreach (FieldDefn d in f.dependents) {
                if (String.IsNullOrEmpty(d.defaultValue) == false) {
                    String value = getDefault(d);
                    if (String.IsNullOrEmpty(getControlRowValue(d, row))) {
                        assignField(d, row, value);
                    }
                }
            }
        }

        private Boolean dateSelect(String fieldName, String origDate, ref String newValue) {
            if (_useInternalDateSelect == false) {
                if (onDateSelect == null) {
                    return false;
                }
                return onDateSelect(this, origDate, ref newValue);
            } else {

            }
            return true;
        }
        /// <summary>
        /// fileSelect
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="selection"></param>
        /// <param name="fieldList"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Boolean fileSelect(String fieldName, String fileName, String selection, ref String value) {
            if (onFileSelect == null) {
                return false;
            }
            return onFileSelect(this, fieldName, fileName, selection, ref value);
        }


        /// <summary>
        /// getAccumulator
        /// </summary>
        /// <param name="f"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Boolean getAccumulator(FieldDefn f, ref String value) {
            UniDynArray outdata = null;
            UniDynArray actions = null;
            if (String.IsNullOrEmpty(_defn.onNew) == false) {
                if (ScreenOnNewEvent != null) {
                    if (ScreenOnNewEvent(this, _defn.onNew, ref outdata, ref actions) == false) {
                        return false;
                    }
                    value = outdata.StringValue;
                    return true;
                }

            } else {

                if (onAccmulator == null) {
                    return false;
                }
                return onAccmulator(this, _defn.mainFile, f.name, ref value);
            }
            return false;
        }

        /// <summary>
        /// getActiveControl
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="col"></param>
        /// <param name="f"></param>
        private Boolean getActiveControl(ref Control ctl, ref Int32 col, ref Int32 row, ref FieldDefn f) {
            ctl = this.ActiveControl;
            col = 0; 
            row = 0;
            
            // get from the cell editor back to the grid. What a fucking mess.
            if (ctl is DataGridViewTextBoxEditingControl) {
                ctl = ((DataGridViewTextBoxEditingControl)ctl).Parent;
                if (!(ctl is DataGridView)) ctl = ctl.Parent;
            }
            
            DataGridViewColumn c = null;
            if (ctl is DataGridView) {
                c = ((DataGridView)ctl).CurrentCell.OwningColumn;
                col = c.Index;
                row = ((DataGridView)ctl).CurrentRow.Index + 1;
                if (_entryDefs.ContainsKey(c.Name)) {
                    f = _entryDefs[c.Name];
                    return true;
                }
            } else {
                if (_entryDefs.ContainsKey(ctl.Name)) {
                    f = _entryDefs[ctl.Name];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// getCheckBoxValue: get the value for a checkbox
        /// </summary>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private String getCheckBoxValue(FieldDefn f, Boolean isChecked) {
            if (String.IsNullOrEmpty(f.list) == false) {
                if (isChecked) {
                    return Utils.field(f.list, ",", 1, 1);
                } else {
                    return Utils.field(f.list, ",", 2, 1);
                }
            } else {
                if (isChecked) {
                    return "1";
                } else {
                    return "0";
                }
            }
        }

        /// <summary>
        /// getColor: change cell background based on entry type
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private Color getColor(FieldDefn f){
            if(f.entryType != FieldEntryType.Enter){
                return Color.Silver;
            }
            if (f.mandatory) {
                return Color.Blue;
            }
            if ((f.selection != String.Empty)
                || (f.relates != String.Empty)
                || (f.fno == 0)) {
                return Color.Yellow;
            }
            return Color.White;
        }
        /// <summary>
        /// getColumnRowValue
        /// </summary>
        /// <param name="g"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private String getColumnRowValue(DataGridView g, Int32 col, Int32 row){
            if (g.Rows[row].Cells[col].Value == null) {
                return String.Empty;
            }
            return g.Rows[row].Cells[col].Value.ToString();
        }

        /// <summary>
        /// getColumnValue
        /// </summary>
        /// <param name="g"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private String getColumnValue(DataGridView g, Int32 col) {
            StringBuilder s = new StringBuilder(String.Empty);
            for(int i = 0; i < g.Rows.Count; i++){
                if(i > 0) s.Append(VM);
                if(g.Rows[i].Cells[col].Value != null){
                    s.Append(g.Rows[i].Cells[col].Value.ToString());
                } 
            }
             return s.ToString();   
                   
        }

        /// <summary>
        /// getControlValue
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private String getControlData(FieldDefn f, int row) {
            if (f.fno > 0) {
                return _rec.Extract(f.fno, row).StringValue;
            }
            if (f.entryType == FieldEntryType.Calc) {
                return getControlRowValue(f, row);
            }
            if (f.keypart > 0) {
                return _keyParts[f.keypart];
            };
            return _id;
        }

        /// <summary>
        /// getControlParent: get parent for placing objects
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private Control getControlParent( Int32 group){
            if(group == 0){
                return this;
            }
            if(_tabControl == null){
                return this;
            }
            if(_tabControl.TabCount < group){
                return this;
            }
            return _tabControl.TabPages[group-1];
        }

        /// <summary>
        /// getControlValue
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private String getControlRowValue(FieldDefn f, int row) {
            String value = String.Empty;
            if (f.depth >= 0) {
                Control ctl = _entryControls[f.name];

                if (ctl is TextBox) {
                    if (((TextBox)ctl).Multiline) {
                        value = ctl.Text.Replace("\r\n", VMS);
                    } else {
                        value = ctl.Text;
                    }
                } else {
                    if (ctl is ComboBox) {
                        value = ctl.Text;
                    } else {
                        if (ctl is CheckBox) {
                            value = getCheckBoxValue(f, ((CheckBox)ctl).Checked);
                        } else {
                            if (ctl is DataGridView) {
                                value = getColumnRowValue((DataGridView)ctl, 0, row);
                            }
                        }
                    }
                }
            } else {
                DataGridView g = (DataGridView)_entryControls[f.controller.name];
                value = getColumnRowValue(g, f.depno, row);
            }
            return value;
        }

        /// <summary>
        /// getControlValue
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private String getControlValue(FieldDefn f) {
            String value = String.Empty;
            if (f.depth >= 0) {
                Control ctl = _entryControls[f.name];

                if (ctl is TextBox) {
                    if (((TextBox)ctl).Multiline) {
                        value = ctl.Text.Replace("\r\n", VMS);
                    } else {
                        value = ctl.Text;
                    }
                } else {
                    if (ctl is ComboBox) {
                        value = ctl.Text;
                    } else {
                        if (ctl is CheckBox) {
                            value = getCheckBoxValue(f, ((CheckBox)ctl).Checked);
                        } else {
                            if (ctl is DataGridView) {
                                value = getColumnValue((DataGridView)ctl, 0);
                            }
                        }
                    }
                }
            } else {
                DataGridView g = (DataGridView) _entryControls[f.controller.name];
                value = getColumnValue(g, f.depno);
            }
            return value;
        }

        private String getDefault(FieldDefn f){
            UniDynArray outdata = null;
            UniDynArray actions = null;

            if (String.IsNullOrEmpty(f.onDefault) == false) {
                if (FieldOnDefaultEvent != null) {
                    FieldOnDefaultEvent(this, f.onDefault, f, ref outdata, ref actions);
                    if (outdata != null) {
                        return outdata.StringValue;
                    }
                }
            }

            return f.defaultValue;
            
        }

        /// <summary>
        /// getHint
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private String getHint(FieldDefn f) {
            if (String.IsNullOrEmpty(f.hint) == false) {
                return f.hint;
            }
            
            // TBD generate automated hints from list etc.
            if (String.IsNullOrEmpty(f.list) == false) {
                return f.list;
            }
            // relates
            if (String.IsNullOrEmpty(f.relates) == false) {
                return "Record on " + f.relates;
            }

            return String.Empty;
        }

        /// <summary>
        /// getScreenImage: get and refresh the screen
        /// </summary>
        private void getScreenImage() {
            if (onDisplay != null) {
                onDisplay(this, ref _image);
                // now populate each field
                refreshScreen();                
            }
        }

        /// <summary>
        /// gotKeyParts
        /// </summary>
        /// <returns></returns>
        private Boolean gotKeyParts() {
            for (int i = 0; i < _lastKeyPart; i++) {
                if (String.IsNullOrEmpty(_keyParts[i])) {
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// iconv
        /// </summary>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private Boolean iconv(String value, String code, ref string newValue) {
            return onIConv(this, value, code, ref newValue);
        }

        /// <summary>
        /// lookupEntry
        /// </summary>
        /// <param name="f"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private Boolean lookupEntry(FieldDefn f, Int32 row, ref String newValue, ref Boolean isKey) {

            isKey = false; // distinguish between key part selection and whole key selection
            
            // first check key field part
            if ((f.keypart > 0) && (String.IsNullOrEmpty(f.keySelect) == false)) {
                return fileSelect(f.name, _defn.mainFile, f.keySelect, ref newValue);
            }
            
            // now key field
            if (f.isKeyField()) {
                isKey = true;
                return fileSelect(f.name, _defn.mainFile, f.selection, ref newValue);                
            }

            // now related fields
            if (String.IsNullOrEmpty(f.relates) == false) {
                return fileSelect(f.name, f.relates, f.selection, ref newValue);                                
            }
            // is it a date?
            if (f.conv.StartsWith("D")) {
                String origDate = getControlData(f, row);
                if (dateSelect(f.name, origDate, ref newValue)) {
                    oconv(newValue, f.conv, ref newValue);
                    return true;
                } else {
                    return false;
                }
            }
            // okay what about a code list?
            if (String.IsNullOrEmpty(f.list) == false) {
                if (f.codeList) {
                    // TBD show code list
                } else {
                    // TBD show a list
                }
            }
            // nothing else comes to mind...
            return false;
        }

        /// <summary>
        /// makeKey
        /// </summary>
        /// <returns></returns>
        private String makeKey() {
            String key = String.Empty;
            for (int i = 0; i < _lastKeyPart; i++) {
                if (i > 0) key = key + _defn.keyDelim;
                key = key + _keyParts[i];
            }
            return key;
        }

        /// <summary>
        /// oconv
        /// </summary>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private Boolean oconv(String value, String code, ref string newValue) {
            return onOConv(this, value, code, ref newValue);
        }


        /// <summary>
        /// padValue: pad value out with zeros
        /// </summary>
        /// <param name="value"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        private String padValue(string value, Int32 padding) {
            if (value.Length >= padding) {
                return value;
            }
            string temp = "";
            for (int i = 0; i < padding; i++) {
                temp = temp + "0";
            }
            return (temp + value).Substring(value.Length);
        }

        /// <summary>
        /// refreshScreen: repaint content of display image
        /// </summary>
        private void refreshScreen() {
            _refreshing = true;

            for(int i = 0; i < _defn.fieldList.Count; i++){
                FieldDefn f = _defn.fieldList[i];                
                String origValue = getControlValue(f);
                String newValue = _image.Extract(i + 1).StringValue;
                if (f.keypart == 0) { // can't do key parts since they are held separately
                    setControlValue(f, newValue);
                }
                //}
            }
            _refreshing = false;
        }

        /// <summary>
        /// rollback: put back the old content of the field following an error
        /// </summary>
        /// <param name="f"></param>
        /// <param name="row"></param>
        private void rollback(FieldDefn f, Int32 row) {
            String old = getControlData(f, row);
            //assignField(f, row, old);
            refreshScreen();
        }


        /// <summary>
        /// setColumnValue
        /// </summary>
        /// <param name="g"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        private void setColumnValue(DataGridView g, FieldDefn f, String value) {
            
            int col = f.depno;
            // clear down the grid
            if((col == 0) && (String.IsNullOrEmpty(value))){
                g.Rows.Clear();
                if(_defn.displayOnly == false) g.Rows.Add(); // for entry
                return;
            }

            String[] values = value.Split(VM);
            int ct = values.Length;
            int excess = 1; // assume an extra line for adding..
            
            int rowCount = g.Rows.Count - excess;
            
            
            // adjust the row numbers
            if (col == 0) {

                // strip any excess rows
                if (ct < rowCount) {
                    for (int i = rowCount; i > ct; i--) {
                        g.Rows.RemoveAt(i - 1);
                    }
                }                
            }

            // add any new rows (may not be fired by first column)
            if (ct > rowCount) {
                for (int i = rowCount; i < ct; i++) {
                    g.Rows.Add();
                }
            }
            // now adjust the values to fit the row count for dependents
            rowCount = g.Rows.Count - excess;
            if(ct > rowCount) ct = rowCount;
            // and update the grid
            for (int i = 0; i < ct; i++) {
                g.Rows[i].Cells[col].Value = values[i];
            }                
        }

        /// <summary>
        /// setControlRowValue
        /// </summary>
        /// <param name="f"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        private void setControlRowValue(FieldDefn f, int col, String value) {
            if ((f.depth == 0) || (f.toolType == FieldToolType.Memo)) {
                setControlValue(f, value);
                return;
            }
            DataGridView g;
            if(f.depth > 0){
                g = (DataGridView) _entryControls[f.name];
            } else {
                g = (DataGridView) _entryControls[f.controller.name];
            }
            g.CurrentRow.Cells[col].Value = value;
        }
        
        /// <summary>
        /// setControlValue
        /// </summary>
        /// <param name="f"></param>
        /// <param name="value"></param>
        private void setControlValue(FieldDefn f, String value) {

            if (f.depth >= 0) {
                Control ctl = _entryControls[f.name];

                if (ctl is TextBox) {
                    if (((TextBox)ctl).Multiline) {
                        ctl.Text = value.Replace(VMS, "\r\n");
                    } else {
                        ctl.Text = value;
                    }
                } else {
                    if (ctl is ComboBox) {
                        ctl.Text = value;
                    } else {
                        if (ctl is CheckBox) {
                            ((CheckBox)ctl).Checked = asCheckBoxValue(f, value);
                        } else {
                            if (ctl is DataGridView) {
                                setColumnValue((DataGridView)ctl, f, value);
                            }
                        }
                    }
                }
            } else {
                DataGridView ctl = (DataGridView)_entryControls[f.controller.name];
                setColumnValue(ctl, f, value);
            }
        }

        
        /// <summary>
        /// setDefn
        /// </summary>
        /// <param name="value"></param>
        private void setDefn(ScreenDefn value) {
            if (value == null) return;
            _defn = value;
            buildScreen();
            setKeyEntry(true);
        }

        /// <summary>
        /// setId
        /// </summary>
        /// <param name="value"></param>
        private void setId(String value) {
            if (String.IsNullOrEmpty(value)) {
                return;
            }
            //  read and display
            _id = value;
            if (doRead() == false) {
                return;
            }

            // show new reord

            getScreenImage();
            if (!_defn.displayOnly) setKeyEntry(false);
        }

        // go through each control and set to noentry unless key field
        private void setKeyEntry(Boolean isKeyEntry) {
            foreach (KeyValuePair<string, Control> kvp in _entryControls) {
                Control c = kvp.Value;
                FieldDefn f = _entryDefs[c.Name];
                if (f.isKeyField()) {
                    setReadOnly(c, !isKeyEntry);
                } else {
                    if (f.entryType == FieldEntryType.Enter) {
                        setReadOnly(c, isKeyEntry);
                    }
                }
            }
        }

        /// <summary>
        /// setReadOnly
        /// </summary>
        /// <param name="c"></param>
        /// <param name="readOnly"></param>
        private void setReadOnly(Control c, Boolean readOnly) {
            if (c is TextBox) {
                ((TextBox)c).Enabled = !readOnly;
                c.TabStop = !readOnly;
            }
            if (c is ComboBox) {
                ((ComboBox)c).Enabled = !readOnly;
                c.TabStop = !readOnly;
            }
            if (c is CheckBox) {
                ((CheckBox)c).Enabled = !readOnly;
                c.TabStop = !readOnly;
            }
            if (c is DataGridView) {
                ((DataGridView) c).Enabled = !readOnly;
            }
        }

        /// <summary>
        ///  update the record e.g. after a call to a handler
        /// </summary>
        /// <param name="newrec"></param>
        public void setRecord(UniDynArray newrec) {
            _rec = newrec;
            if (rec != null) {
                getScreenImage();
            }
        }

        /// <summary>
        /// showFocussed: show hint and browse message when field gets focus
        /// </summary>
        /// <param name="f"></param>
        private void showFocussed(FieldDefn f) {
            String hint = getHint(f);
            showHint(hint);
            String canBrowse = "";
            if (f.isKeyField()
                || (String.IsNullOrEmpty(f.relates) == false)
                || (String.IsNullOrEmpty(f.selection) == false)) {
                canBrowse = "Browse";
            }
            showCanBrowse(canBrowse);
        }
        /// <summary>
        /// showHint
        /// </summary>
        /// <param name="hint"></param>
        private void showHint(String hint) {
            if (onShowHint == null) return;
            onShowHint(this, hint);
        }

        /// <summary>
        /// showCanBrowse
        /// </summary>
        /// <param name="canBrowse"></param>
        private void showCanBrowse(String canBrowse) {
            if (onShowCanBrowse == null) return;
            onShowCanBrowse(this, canBrowse);
        }

        /// <summary>
        /// showStatus
        /// </summary>
        /// <param name="status"></param>
        private void showStatus(String status) {
            if (onShowStatus == null) return;
            onShowStatus(this, status);
        }

        /// <summary>
        /// update the record
        /// </summary>
        /// <param name="f"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        private void updateRecord(FieldDefn f, Int32 row, String value) {
            UniDynArray outdata = Server.Instance.createArray();
            UniDynArray actions = null;

            if ((value.ToUpper() == "NEW") && (f.sequential)) {
                getAccumulator(f, ref value);
            }
            
            if (f.padded > 0) {
                value = padValue(value, f.padded);
            }

            //onChange called before update, OnEntry called after
            if(String.IsNullOrEmpty(f.onChange) == false){
                if(FieldOnChangeEvent != null){
                    outdata.Replace(1, value);
                    FieldOnChangeEvent(this, f.onChange, f, ref outdata, ref actions);
                    value = outdata.StringValue;
                }
            }


            if (validateEntry(f, row, value) == false) {
                rollback(f, row);
                return;
            }
            
            if (String.IsNullOrEmpty(f.conv) == false) {
                String newValue = string.Empty;
                iconv(value, f.conv, ref newValue);
                value = newValue;
            }
            assignField(f, row, value);

            // the following caters for key part fields
            if (rec == null) {
                return;
            }

            updateUsing(f, row, value);
            if (f.dependents != null) {
                defaultDependents(f, row);
            }

            if(String.IsNullOrEmpty(f.onEntry) == false){
                if(FieldOnEntryEvent != null){
                    FieldOnEntryEvent(this, f.onEntry, f, ref outdata, ref actions);
                    doSubrActions(actions);
                }
            }

            if (String.IsNullOrEmpty(_defn.onEnter) == false) {
                if (ScreenOnEnterEvent != null) {
                    ScreenOnEnterEvent(this, _defn.onEnter, ref outdata, ref actions);
                    doSubrActions(actions);
                }
            }

            if (rec != null) {
                getScreenImage();
            }
        }

        /// <summary>
        /// updateUsing: handle UPDATES .. USING clauses
        /// </summary>
        /// <param name="f"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        private void updateUsing(FieldDefn f, int row, String value) {
            Boolean isNew = false;
            if (f.updateList == null) return;
            if (f.updateList.Count == 0) return;            
            if (String.IsNullOrEmpty(value)) return;
            if (onReadRemote == null) return;

            UniDynArray remoteRec = null;
            // read the remote record
            onReadRemote(this, f.relates, value, false, ref isNew, ref remoteRec);
            foreach (FieldUpdates fup in f.updateList) {
                if (rec.Extract(fup.targetFNo, row).StringValue == "") {
                    rec.Replace(fup.targetFNo, row, remoteRec.Extract(fup.sourceFNo).StringValue);
                    Changed = true;
                }
            } 

        }

        private void updateReferences() {
            foreach (FieldDefn f in _defn.fieldList) {
                if ((String.IsNullOrEmpty(f.relates) == false)
                    && (f.relationType == RelationType.Reference)) {
                    String temp = getControlValue(f);
                    if (onUpdateReference != null) {
                        onUpdateReference(this, f.relates, temp);
                    }
                }
            }
        }

        /// <summary>
        /// validateEntry
        /// </summary>
        /// <param name="f"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Boolean validateEntry(FieldDefn f, Int32 row, String value) {
            Boolean found = false;
            String cValue = String.Empty;
            Double iValue = 0;
            Double iTest = 0;
           UniDynArray outdata = null;
            UniDynArray actions = null;
            if (String.IsNullOrEmpty(value)) {
                return true;
            }

            // special case is sequential with NEW
            if ((value == "NEW") && f.sequential) {
                return true;
            }

            // check conversion code
            if (String.IsNullOrEmpty(f.conv) == false) {
                if (iconv(value, f.conv, ref cValue) == false) {
                    MessageBox.Show("Not a suitable value for conversion " + f.conv);
                    return false;
                }
            } else {
                cValue = value;
            }

            // numeric
            if (f.numeric) {
                if(Double.TryParse(cValue, out iValue) == false){
                    MessageBox.Show("Value must be numeric");
                    return false;
                }
            }

            // length is external
            if ((f.maxLength > 0) && (value.Length > f.maxLength)) {
                MessageBox.Show("This must be " + f.maxLength.ToString() + " characters or less");
                return false;
            }

            // List is also external for now ...
            if (String.IsNullOrEmpty(f.list) == false) {
                if (f.listType == FieldListType.Valid) {
                    String[] da = f.list.Split(',');
                    foreach (String s in da) {
                        if (f.codeList) {                            
                            if ((s.Split('|')[0]) == value) {
                                found = true;
                                break;
                            }
                        } else {
                            if (s == value) {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found) {
                        MessageBox.Show("Must be one of " + f.list);
                        return false;
                    }
                }
            }
            
            

            // test range from and to (internal to capture e.g. dates)
            if (String.IsNullOrEmpty(f.rangeFrom) == false) {
                try {
                    iValue = Convert.ToDouble(cValue);
                    if (String.IsNullOrEmpty(f.conv) == false) {
                        String temp = String.Empty;
                        iconv(f.rangeFrom, f.conv, ref temp);
                        iTest = Convert.ToDouble(temp);
                    } else {
                        iTest = Convert.ToDouble(f.rangeFrom);
                    }
                    if (iValue < iTest) {
                        MessageBox.Show("Cannot be less than " + f.rangeFrom);
                        return false;
                    }
                } catch { };                
            }
            if(String.IsNullOrEmpty(f.rangeTo) == false){
                try{
                    iValue = Convert.ToDouble(cValue);
                    if (String.IsNullOrEmpty(f.conv) == false) {
                        String temp = String.Empty;
                        iconv(f.rangeTo, f.conv, ref temp);
                        iTest = Convert.ToDouble(temp);
                    } else {
                        iTest = Convert.ToDouble(f.rangeTo);
                    }
                    if (iValue > iTest) {
                        MessageBox.Show("Cannot be more than " + f.rangeTo);
                        return false;
                    }
                } catch { };
            }
            
            
            // MV Unique
            if (f.mvUnique) {
                String allValues = getControlValue(f);
                Int32 dc = Utils.DCount(allValues, Server.FM_STR);
                for (int i = 1; i < dc; i++) {
                    if (i != row) {
                        String temp = Utils.field(allValues, Server.VM_STR, 1, i);
                        if (temp == value) {
                            MessageBox.Show(value + " has already been entered");
                            return false;
                        }
                    }
                }
            }

            // now relation
            if ((String.IsNullOrEmpty(f.relates) == false) && (f.relationType == RelationType.Valid)) {
                if (checkRelation(f.relates, cValue) == false) {
                    MessageBox.Show("Must be a record on " + f.relates);
                    return false;
                }
            }

            // local validation: note this is a different call format.
            if (String.IsNullOrEmpty(f.onValid) == false) {
                if (FieldOnValidEvent != null) {
                    if (FieldOnValidEvent(this, f.onValid, f, ref outdata, ref actions) == false) {
                        return false;
                    }
                    doSubrActions(actions);
                    if (Utils.safeBool(outdata.StringValue) == false) {
                        return false;
                    }
                }
            }

            
            return true;
        }

        #region Control Event Handlers

        /// <summary>
        /// ctl_CheckedChanged: checkbox value has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctl_CheckedChanged(object sender, EventArgs e) {
            if (_refreshing) return;
            String ctlName = ((Control)sender).Name;
            Boolean isChecked = ((CheckBox)sender).Checked;            
            FieldDefn f = _entryDefs[ctlName];
            String value = getCheckBoxValue(f, isChecked);
            if (f.entryType != FieldEntryType.Enter) {
                return;
            }
            doEnter(f, 0, value);            
        }

        /// <summary>
        /// ctl_GotFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctl_GotFocus(object sender, EventArgs e) {
            if (_refreshing) return;
            String ctlName = ((Control)sender).Name;
            String value = ((Control)sender).Text;
            FieldDefn f = _entryDefs[ctlName];
            showFocussed(f);
            
        }

        

        /// <summary>
        /// ctl_Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctl_Validated(object sender, EventArgs e) {
            String ctlName = ((Control)sender).Name;
            String value = ((Control)sender).Text;
            FieldDefn f = _entryDefs[ctlName];
            if (f.entryType != FieldEntryType.Enter) {
                return;
            }
            doEnter(f, 0, value);            
        }

        #endregion


        

        #region Public Properties

        public String id {
            get { return _id; }
            set { setId(value); }
        }

        public UniDynArray rec {
            get { return _rec; }
        }

        public string origRec {
            get { return _origRec; }
        }

        public ScreenDefn Screen {
            get { return _defn; }
            set { setDefn(value); }
        }
        public Double UnitWidth {
            get { return _unitWidth; }
            set { _unitWidth = value; }
        }

        public Boolean Changed {
            get { return _changed; }
            set {
                _changed = value;
                showStatus(value ? "Changed" : "");
            }
        }

        public Boolean Closing {
            get { return _closing; }
            set { 
                _closing = value;
                if(value) doClear();
            }
        }

        #endregion

        #region Public Methods

        public Boolean Clear() {
            return doClear();
        }

        public Boolean Delete() {
            return doDelete();
        }

        public Boolean Write() {
            return doWrite();
        }

        public void Browse() {
            doBrowse();
        }
        #endregion
    }

    // events
    public delegate void DisplayScreenEvent(Object sender, ref UniDynArray image);
    public delegate Boolean ConvDataEvent(object sender, String origValue, String code, ref String newValue);
    public delegate Boolean ReadEvent(object sender, String fileName, String key, Boolean locking, ref bool isNew, ref UniDynArray rec);
    public delegate Boolean WriteEvent(object sender, String fileName, String key, UniDynArray rec);
    public delegate Boolean DeleteEvent(object sender, String fileName, String key);
    public delegate Boolean ReleaseEvent(object sender, String fileName, String key);
    public delegate Boolean AcculatorEvent(object sender, String fileName, string fieldName, ref String key);
    public delegate void ShowStatusEvent(object sender, String status);
    public delegate void ShowCanBrowseEvent(object sender, String canBrowse);
    public delegate void showHintEvent(object sender, String hint);
    public delegate Boolean CheckRelationEvent(object sender, String fileName, String key);
    public delegate Boolean FileSelectEvent(object sender, String fieldName, String fileName, String selection, ref String newValue);
    public delegate Boolean DateSelectEvent(object sender, String inputDate, ref String newValue);
    public delegate Boolean GetDefaultValueEvent(object sender, string defaultValue, ref string newValue);
    // field and screen defined events
    public delegate Boolean ScreenCallEvent(object sender, String subrName, ref UniDynArray outData, ref UniDynArray actions);
    public delegate Boolean FieldCallEvent(object sender, String subrName, FieldDefn f, ref UniDynArray outData, ref UniDynArray actions);
    public delegate void UpdateReferenceEvent(object sender, String relates, String value);
   
}
