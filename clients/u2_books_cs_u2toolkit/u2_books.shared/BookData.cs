using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books.shared
{
    public class BookData
    {
        protected String _bookId = String.Empty;
        protected String _shortTitle = String.Empty;
        protected String _authorID = String.Empty;
        protected string _authorName = String.Empty;
        protected String _publisherId = String.Empty;
        protected String _publisherName = String.Empty;
        protected string _supplierId = String.Empty;
        protected String _supplierName = String.Empty;
        protected String _isbn = String.Empty;
        protected string _dept = String.Empty;
        protected string _genre = String.Empty;
        protected String _media = String.Empty;
        protected String _taxCode = String.Empty;
        protected int _stockLevel = 0;
        protected Double _salesPrice = 0.0;
        protected int _minOrder = 0;
        protected Double _purchPrice = 0.0;
        protected String _longDescription = String.Empty;

        public String BookId {
            get { return _bookId; }
            set { _bookId = value; }
        }
        public String ShortTitle {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }
        public String AuthorId {
            get { return _authorID; }
            set { _authorID = value; }
        }
        public String AuthorName {
            get { return _authorName; }
            set { _authorName = value; }
        }
        public String PublisherId {
            get { return _publisherId; }
            set { _publisherId = value; }
        }
        public String PublisherName {
            get { return _publisherName; }
            set { _publisherName = value; }
        }
        public String SupplierId {
            get { return _supplierId; }
            set { _supplierId = value; }
        }
        public String SupplierName {
            get { return _supplierName; }
            set { _supplierName = value; }
        }
        public String ISBN {
            get { return _isbn; }
            set { _isbn = value; }
        }
        public String Department {
            get { return _dept; }
            set { _dept = value; }
        }
        public String Genre {
            get { return _genre; }
            set { _genre = value; }
        }
        public String Media {
            get { return _media; }
            set { _media = value; }
        }
        public string TaxCode {
            get { return _taxCode; }
            set { _taxCode = value; }
        }
        public int StockLevel {
            get { return _stockLevel; }
            set { _stockLevel = value; }
        }
        public Double SalesPrice {
            get { return _salesPrice; }
            set { _salesPrice = value; }
        }
        public int MinOrderQty {
            get { return _minOrder; }
            set { _minOrder = value; }
        }
        public Double PurchasePrice {
            get { return _purchPrice; }
            set { _purchPrice = value; }
        }
        public String LongDescription {
            get { return _longDescription; }
            set { _longDescription = value; }
        }

    }
}
