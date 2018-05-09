using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class ClientSummary
    {
        public String ClientId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Country { get; set; }
        public String JoinDate { get; set; }
        public String AccountStatus { get; set; }

        public String FullName
        {
            get { return String.Format("{0}, {1}", LastName, FirstName); }
        }


    }
    public class ClientSummaryList : List<ClientSummary> { };
}
