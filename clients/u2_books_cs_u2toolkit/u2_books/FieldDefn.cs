using System;
using System.Collections.Generic;
using System.Text;

namespace u2_books
{
    public class FieldDefn
    {
        private String _name = String.Empty;
        private Int32 _seq = 0;
        private String _colHead = String.Empty;
        private int _entryType = FieldEntryType.Enter;
        private int _toolType = FieldToolType.Text;
        private String _dicttype = String.Empty;
        private Int32 _fno = 0;
        private Int32 _vno = 0;
        private Int32 _keypart = 0;
        private Int32 _group = 0;
        
        private Double _left = 0;
        private Double _top = 0;
        private Double _width = 0;
        private Double _height = 0; // screen elements only

        private Int32 _length = 0;
        public String _depends = String.Empty;
        private Int32 _depth = 0;
        private Int32 _depNo = 0;
        private FieldDefn _controller = null;

        private String _onEntry = String.Empty;
        private String _onChange = String.Empty;
        private String _onValid = String.Empty;
        private String _onDefault = String.Empty;

        private String _allocField = String.Empty;
        private String _conv = String.Empty;
        private String _default = String.Empty;

        private Boolean _mandatory = false;
        private Boolean _sequential = false;

        private String _list = String.Empty;
        private Boolean _codeList = false;
        private int _listType = FieldListType.Valid;
       
        
        private String _rangeFrom = String.Empty;
        private String _rangeTo = String.Empty;        
        private String _keySelect = String.Empty;
        private List<String> _keyShowing = null;
        private Int32 _maxDepth = 0;
        private Int32 _maxLength = 0;
        private Boolean _mvUnique = false;
        private Boolean _numeric = false;
        private Int32 _padded = 0;
        private String _relates = String.Empty;
        private RelationType _reltype = RelationType.Valid;
        private String _selection = String.Empty;
        private String _showing = null;
        private String _selList = String.Empty;
        private String _hint = String.Empty;
        private List<FieldUpdates> _updateList = null;

        private List<FieldDefn> _dependents = null;
        


        public Boolean isKeyField() {
            return ((fno == 0) && (_entryType == FieldEntryType.Enter));
        }
                
        public void addKeyShowing(String s) {
            if (_keyShowing == null) _keyShowing = new List<String>();
            _keyShowing.Add(s);
        }
              
        public void addDependent(FieldDefn f) {
            if (_dependents == null) _dependents = new List<FieldDefn>();
            _dependents.Add(f);
        }

        public void addUpdate(FieldUpdates u) {
            if (_updateList == null) _updateList = new List<FieldUpdates>();
            _updateList.Add(u);
        }

        public String name {
            get { return _name; }
            set { _name = value; }
        }
        public int entryType {
            get { return _entryType; }
            set { _entryType = value; }
        }
        public Int32 fno {
            get { return _fno; }
            set { _fno = value; }
        }
        public Int32 vno {
            get { return _vno; }
            set { _vno = value; }
        }
        public Int32 keypart {
            get { return _keypart; }
            set { _keypart = value; }
        }

        public Double top {
            get { return _top; }
            set { _top = value; }
        }
        public Double left {
            get { return _left; }
            set { _left = value; }
        }
        public Double width {
            get { return _width; }
            set { _width = value; }
        }
        public Double height {
            get { return _height; }
            set { _height = value; }
        }
       

        public Int32 length {
            get { return _length; }
            set { _length = value; }
        }

        public Int32 depth {
            get { return _depth; }
            set { _depth = value; }
        }

        public Int32 group {
            get { return _group; }
            set { _group = value; }
        }

        public Int32 depno {
            get { return _depNo; }
            set { _depNo = value; }
        }
        public String onEntry {
            get { return _onEntry; }
            set { _onEntry = value; }
        }

        public String onChange {
            get { return _onChange; }
            set { _onChange = value; }
        }
        public String onValid {
            get { return _onValid; }
            set { _onValid = value; }
        }
        public String onDefault {
            get { return _onDefault; }
            set { _onDefault = value; }
        }
        public String allocField {
            get { return _allocField; }
            set { _allocField = value; }
        }
        public String conv {
            get { return _conv; }
            set { _conv = value; }
        }
        public String defaultValue {
            get { return _default; }
            set { _default = value; }
        }
        public Boolean mandatory {
            get { return _mandatory; }
            set { _mandatory = value; }
        }
        public Boolean sequential {
            get { return _sequential; }
            set { _sequential = value; }
        }
        public String list {
            get { return _list; }
            set { _list = value; }
        }
        public int listType {
            get { return _listType; }
            set { _listType = value; }
        }
        public Boolean codeList {
            get { return _codeList; }
            set { _codeList = value; }
        }
        public String rangeFrom {
            get { return _rangeFrom; }
            set { _rangeFrom = value; }
        }
        public String rangeTo {
            get { return _rangeTo; }
            set { _rangeTo = value; }
        }
        public String keySelect {
            get { return _keySelect; }
            set { _keySelect = value; }
        }
        public List<String> keyShowing {
            get { return _keyShowing; }
        }
        public Int32 maxDepth {
            get { return _maxDepth; }
            set { _maxDepth = value; }
        }
        public Int32 maxLength {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
        public Boolean mvUnique {
            get { return _mvUnique; }
            set { _mvUnique = value; }
        }
        public Boolean numeric {
            get { return _numeric; }
            set { _numeric = value; }
        }
        public Int32 padded {
            get { return _padded; }
            set { _padded = value; }
        }
       
        public String relates {
            get { return _relates; }
            set { _relates = value; }
        }
        public RelationType relationType {
            get { return _reltype; }
            set { _reltype = value; }
        }

        public String selection {
            get { return _selection; }
            set { _selection = value; }
        }
        
        public String showing {
            get { return _showing; }
            set { _showing = value; }
        }
        
        public String selList {
            get { return _selList; }
            set { _selList = value; }
        }
        
        public String hint {
            get { return _hint; }
            set { _hint = value; }
        }
        
        public String dictType {
            get { return _dicttype; }
            set { _dicttype = value; }
        }
        
        public String colHead {
            get { return _colHead; }
            set { _colHead = value; }
        }
        public String depends {
            get { return _depends; }
            set { _depends = value; }
        }

        public List<FieldUpdates> updateList {
            get { return _updateList; }
        }
        public List<FieldDefn> dependents {
            get { return _dependents; }
        }

        public int toolType {
            get { return _toolType; }
            set { _toolType = value; }
        }

        public FieldDefn controller {
            get { return _controller; }
            set { _controller = value; }
        }
        public Int32 sequence {
            get { return _seq; }
            set { _seq = value; }
        }
        public string entryTypeText{
            set{ _entryType = FieldEntryType.Parse(value);}
        }
    }

   
    public static class FieldEntryType{
        public const int Enter = 0;
        public const int ReadOnly = 1;
        public const int Calc = 2;
        public const int Element = 3;

        public static int Parse(String text){
            if(String.IsNullOrEmpty(text)) return Enter;
            if(text.ToUpper() == "ENTER") return Enter;
            if(text.ToUpper() == "READONLY") return ReadOnly;
            if(text.ToUpper() == "CALC") return Calc;
            if(text.ToUpper() == "ELEMENT") return Element;
            return Enter;
        }
    }

    public static class FieldListType
    {
        public const int Valid = 0;
        public const int NonExclusive = 1;
    }

    public enum RelationType
    {
        Valid = 0,
        NonExclusive = 1,
        Reference = 2
    }

    public static class FieldToolType
    {
        public const int Text = 1;
        public const int Combo = 2;
        public const int Check = 3;
        public const int Memo = 4;
        public const int Frame = 5;
        public const int TabSet = 6;
        public const int Button = 7;

        public static int Parse(string text) {
            if (String.IsNullOrEmpty(text)) return 1;
            if (text.ToUpper() == "TEXT") return 1;
            if (text.ToUpper() == "COMBO") return Combo;
            if (text.ToUpper() == "CHECK") return Check;
            if (text.ToUpper() == "MEMO") return Memo;
            if (text.ToUpper() == "FRAME") return Frame;
            if (text.ToUpper() == "TAB") return TabSet;
            if (text.ToUpper() == "BUTTON") return Button;
            return Text;
        }
    }

    public class FieldUpdates
    {
        private String _target = String.Empty;
        private String _source = String.Empty;
        private int _sourceFNo = 0;
        private int _targetFNo = 0;

        public String target {
            get { return _target; }
            set { _target = value; }
        }
        public String source {
            get { return _source; }
            set { _source = value; }
        }
        public int sourceFNo {
            get { return _sourceFNo; }
            set { _sourceFNo = value; }
        }
        public int targetFNo {
            get { return _targetFNo; }
            set { _targetFNo = value; }
        }
    }

}
