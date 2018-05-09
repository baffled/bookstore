using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class BookSummary
    {
        protected String _bookId = String.Empty;
        protected String _title = String.Empty;
        protected string _author = String.Empty;
        protected String _isbn = String.Empty;
        protected double _salePrice = 0;
        protected string _media = String.Empty;

        public String BookId {
            get { return _bookId; }
            set { _bookId = value; }
        }
        public String Title {
            get { return _title; }
            set { _title = value; }
        }
        public String AuthorName {
            get { return _author; }
            set { _author = value; }
        }
        public String ISBN {
            get { return _isbn; }
            set { _isbn = value; }
        }
        public Double SalesPrice {
            get { return _salePrice; }
            set { _salePrice = value; }
        }
        public String Media {
            get { return _media; }
            set { _media = value; }
        }
    }

    public class BookSummaryList : List<BookSummary>{};
}
