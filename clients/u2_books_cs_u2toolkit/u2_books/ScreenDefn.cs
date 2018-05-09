using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using u2_books.shared;
namespace u2_books
{
    public class ScreenDefn
    {
        private String _mainFile = String.Empty;
        private String _mainDict = String.Empty;
        private String _heading = String.Empty;
        private int _screenHeight = 0;
        private int _screenWidth = 0;

        private Boolean _noClear = false;
        private Boolean _displayOnly = false;

        private String _onRead = String.Empty;
        private String _onWrite = String.Empty;
        private String _onDelete = String.Empty;
        private String _onAfterRead = String.Empty;
        private String _onAfterWrite = String.Empty;
        private String _onAfterDelete = String.Empty;
        private String _onRelease = String.Empty;
        private String _onValid = String.Empty;
        private String _onDisplay = String.Empty;
        private String _onClear = String.Empty;
        private String _onNew = String.Empty;
        private String _onDefault = String.Empty;
        private String _onKey = String.Empty; //not required
        private String _onEnter = String.Empty;
        private String _onClose = String.Empty;

        private String _keyDelim = String.Empty;


        private List<PromptDefn> _promptList = null;
        private List<FieldDefn> _fieldList = null;
        private List<FieldDefn> _elementList = null;

        public void addElement(FieldDefn f) {
            if (_elementList == null) _elementList = new List<FieldDefn>();
            _elementList.Add(f);
        }
        public void addField(FieldDefn f) {
            if (_fieldList == null) _fieldList = new List<FieldDefn>();
            _fieldList.Add(f);
        }

        public void addPrompt(PromptDefn p) {
            if (_promptList == null) _promptList = new List<PromptDefn>();
            _promptList.Add(p);
        }

        public String mainFile {
            get { return _mainFile; }
            set { _mainFile = value; }
        }
        public String mainDict {
            get { return _mainDict; }
            set { _mainDict = value; }
        }

        public Boolean noClear {
            get { return _noClear; }
            set { _noClear = value; }
        }
        public Boolean displayOnly {
            get { return _displayOnly; }
            set { _displayOnly = value; }
        }

        public String heading {
            get { return _heading; }
            set { _heading = value; }
        }

        public int screenHeight
        {
            get { return _screenHeight; }
            set { _screenHeight = value; }
        }
        public int screenWidth
        {
            get { return _screenWidth; }
            set { _screenWidth = value; }
        }
        public String onRead {
            get { return _onRead; }
            set { _onRead = value; }
        }
        public String onWrite {
            get { return _onWrite; }
            set { _onWrite = value; }
        }
        public String onDelete {
            get { return _onDelete; }
            set { _onDelete = value; }
        }
        public String onAfterRead {
            get { return _onAfterRead; }
            set { _onAfterRead = value; }
        }
        public String onAfterWrite {
            get { return _onAfterWrite; }
            set { _onAfterWrite = value; }
        }
        public String onAfterDelete {
            get { return _onAfterDelete; }
            set { _onAfterDelete = value; }
        }
        public String onRelease {
            get { return _onRelease; }
            set { _onRelease = value; }
        }
        public String onValid {
            get { return _onValid ; }
            set { _onValid = value; }
        }
        public String onDisplay {
            get { return _onDisplay; }
            set { _onDisplay = value; }
        }
        public String onClear {
            get { return _onClear; }
            set { _onClear = value; }
        }
        public String onNew {
            get { return _onNew; }
            set { _onNew = value; }
        }
        public String onDefault {
            get { return _onDefault; }
            set { _onDefault = value; }
        }
        public String onKey {
            get { return _onKey; }
            set { _onKey = value; }
        }
        public String onEnter {
            get { return _onEnter; }
            set { _onEnter = value; }
        }
        public String onClose {
            get { return _onClose; }
            set { _onClose = value; }
        }
        public String keyDelim {
            get { return _keyDelim; }
            set { _keyDelim = value; }
        }
        public List<PromptDefn> promptList {
            get { return _promptList; }
            set{ _promptList = value; }
        }
        public List<FieldDefn> fieldList{
            get { return _fieldList; }
            set{ _fieldList = value; }
        }
        public List<FieldDefn> elementList {
            get { return _elementList; }
        }

        public static ScreenDefn fromXML(String xml) {
            try {
                XDocument doc = XDocument.Parse(xml);
                var query = from e in doc.Descendants("SCREEN")
                            select new ScreenDefn
                            {
                                mainFile = (String)e.Element("MAINFILE"),
                                mainDict = (String)e.Element("MAINDICT"),
                                heading = Utils.safeString((string)e.Element("HEADING")),
                                displayOnly = Utils.safeBool((string)e.Attribute("displayOnly")),
                                keyDelim = Utils.safeString((string)e.Attribute("keyDelim")),
                                noClear = (bool)Utils.safeBool((string)e.Attribute("noClear")),
                                screenHeight = Utils.safeInt((string)e.Attribute("height")),
                                screenWidth = Utils.safeInt((string)e.Attribute("width")),
                                promptList = (
                                from p in e.Element("PROMPTS").Descendants("PROMPT")
                                select new PromptDefn
                                {
                                    text = (string)p.Attribute("text"),
                                    left = (int)Utils.safeInt((string)p.Attribute("x")),
                                    top = (int)Utils.safeInt((string)p.Attribute("y")),
                                }).ToList(),
                                fieldList = (
                                from f in e.Element("FIELDS").Descendants("FIELD")
                                select new FieldDefn
                                {
                                    left = Utils.safeInt((string)f.Attribute("x")),
                                    top = Utils.safeInt((string)f.Attribute("y")),
                                    length = Utils.safeInt((String)f.Attribute("length")),
                                    height = Utils.safeInt((String)f.Attribute("height")),
                                    maxLength = Utils.safeInt((String)f.Attribute("maxLength")),
                                    maxDepth = Utils.safeInt((string)f.Attribute("maxDepth")),
                                    mandatory = Utils.safeBool((String)f.Attribute("mandatory")),
                                    numeric = Utils.safeBool((String)f.Attribute("numeric")),
                                    sequential = Utils.safeBool((String)f.Attribute("sequential")),
                                    relates = Utils.safeString((string)f.Attribute("relates")),
                                    entryTypeText = Utils.safeString((string)f.Attribute("entryType")),
                                    rangeFrom = Utils.safeString((string)f.Attribute("rangeFrom")),
                                    rangeTo = Utils.safeString((String)f.Attribute("rangeTo")),
                                    colHead = Utils.safeString((string)f.Attribute("colHead")),
                                    conv = Utils.safeString((string)f.Attribute("conv")),
                                    allocField = Utils.safeString((string)f.Attribute("allocField")),
                                    defaultValue = Utils.safeString((string)f.Attribute("default")),
                                    depth = Utils.safeInt((string)f.Attribute("depth")),
                                    hint = Utils.safeString((string)f.Attribute("hint")),
                                    keypart = Utils.safeInt((string)f.Attribute("keyPart")),
                                    list = Utils.safeString((String)f.Attribute("list")),
                                    name = (string)f.Attribute("name"),
                                    fno = Utils.safeInt((string)f.Attribute("fno")),
                                    mvUnique = Utils.safeBool((string)f.Attribute("mvUnique")),
                                    padded = Utils.safeInt((string)f.Attribute("padded")),                                   
                                    vno = Utils.safeInt((String)f.Attribute("vno")),
                                    selection = Utils.safeString((string)f.Attribute("selection")),                                 
                                    toolType = FieldToolType.Parse((string)f.Attribute("tool")),
                                    depends = Utils.safeString((string)f.Attribute("depends")),
                                    onEntry= Utils.safeString((string)f.Attribute("onEntry"))
                                }).ToList()
                            };
                List<ScreenDefn> l = new List<ScreenDefn>();
                foreach (var defn in query) {
                    l.Add(defn);

                    // resolve controllers and dependents
                    for (int i = 0; i < defn.fieldList.Count;  i++) {
                        if (String.IsNullOrEmpty(defn.fieldList[i].depends) == false) {
                            Boolean found = false;
                            for (int j = 0; (j < defn.fieldList.Count) && (found == false); j++) {
                                if (defn.fieldList[j].name == defn.fieldList[i].depends) {
                                    found = true;
                                    defn.fieldList[i].controller = defn.fieldList[j];
                                    defn.fieldList[j].addDependent(defn.fieldList[i]);
                                    defn.fieldList[i].depno = defn.fieldList[j].dependents.Count;
                                }
                            }
                        }
                    }
                    // remove the dependents now they have been attached to their controllers?
                    //for (int i = defn.fieldList.Count; i > 0; i--) {
                    //    if (String.IsNullOrEmpty(defn.fieldList[i].depends) == false) {
                    //        defn.fieldList.RemoveAt(i - 1);
                    //    }
                    //}

                    return defn;
                }
            } catch (Exception ex) {
                // TBD message it
                return null;
            }
            return null;
                        
        }
    }
}
