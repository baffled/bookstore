using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using u2_books.shared;

namespace u2_books
{
    public class StaticData
    {
        private static volatile StaticData _instance = null;
        private static Object syncRoot = new Object();

        protected SalesTaxDict _salesTaxDict = new SalesTaxDict();

        public static StaticData Instance {
            get {
                if (_instance == null) {
                    lock (syncRoot) {
                        _instance = new StaticData();
                    }
                }
                return _instance;
            }
        }

        public SalesTaxDict salesTaxDict {
            get { return _salesTaxDict; }
            set { _salesTaxDict = value; }
        }

    }
}
