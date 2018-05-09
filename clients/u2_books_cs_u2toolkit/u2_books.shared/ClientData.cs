using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class ClientData
    {
        protected ClientOrderList _orderList = new ClientOrderList();
        protected ClientPaymentList _paymentList = new ClientPaymentList();

        public String ClientId { get; set; }
        public String Surname { get; set; }
        public String Forename { get; set; }
        public String Address { get; set; }
        public String Country { get; set; }
        public String JoinDate { get; set; }

        public ClientOrderList OrderList {
            get { return _orderList; }
        }
        public ClientPaymentList PaymentList {
            get { return _paymentList; }
        }
    }

    public class ClientOrder
    {
        public String OrderId { get; set; }
        public String OrderDate { get; set; }
        public Double OrderTotal { get; set; }
        public String OrderStatus { get; set; }
    }
    public class ClientPayment
    {
        public String PaymentId { get; set; }
        public String PaymentDate { get; set; }
        public Double PaymentAmount { get; set; }
    }
    public class ClientOrderList : List<ClientOrder> { };
    public class ClientPaymentList : List<ClientPayment> { };

}
