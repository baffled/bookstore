using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class SalesOrderData
    {
        protected List<SalesOrderLine> _lines = new List<SalesOrderLine>();

        public String OrderId { get; set; }
        public String OrderDate { get; set; } // TBD change type
        public String ClientId { get; set; }
        public String Fullname { get; set; }
        public String ShipCode { get; set; }
        public double ShipCost { get; set; }
        public String DespatchDate { get; set; } // TBD change type
        public List<SalesOrderLine> Lines {
            get { return _lines; }
            set { _lines = value; }
        }
    }

    public class SalesOrderLine
    {
        public String BookId { get; set; }
        public String Title { get; set; }
        public String AuthorName { get; set; }
        public Double Price { get; set; }
        public int Qty { get; set; }
        public String TaxCode { get; set; }
        public Double GoodsAmt { get; set; }
        public Double TaxAmt { get; set; }
    }
    public class SalesOrderLineList : List<SalesOrderLine>{};
    public class SalesOrderDataList : List<SalesOrderData> { };
}
