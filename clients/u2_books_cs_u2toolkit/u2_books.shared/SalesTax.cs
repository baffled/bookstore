using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class SalesTax
    {
        public String TaxCode { get; set; }
        public String ShortDescription { get; set; }
        public Double Rate { get; set; }
    }

    public class SalesTaxDict : Dictionary<String, SalesTax> { };
}
