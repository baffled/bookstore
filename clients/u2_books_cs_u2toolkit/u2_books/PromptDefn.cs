using System;
using System.Collections.Generic;
using System.Text;

namespace u2_books
{
    public class PromptDefn
    {
        Double _left = 0;
        Double _top = 0;
        String _text = String.Empty;
        Int32 _group = 0;

        public Double left {
            get { return _left; }
            set { _left = value; }
        }

        public Double top {
            get { return _top; }
            set { _top = value; }
        }

        public String text {
            get { return _text; }
            set { _text = value; }
        }

        public Int32 group {
            get { return _group; }
            set { _group = value; }
        }

    }
}
