using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class ShippingCost
    {
        public String ShippingId { get; set; }
        public String Description { get; set; }
        public Double Cost { get; set; }
    }
    public class ShippingCostList : List<ShippingCost> { };
}
