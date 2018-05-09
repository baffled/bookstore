using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace u2_books
{
    public class BookConst
    {
        public static String Title = "U2 Bookstore Demonstration";

        public static String SEL_PUBLISHERS_QUERY = "SORT U2_PUBLISHERS FULLNAME BY FULLNAME";
        public static String SEL_AUTHORS_QUERY = "SORT U2_AUTHORS FULLNAME BY SORTNAME";
        public static String SEL_SUPPLIERS_QUERY = "SORT U2_SUPPLIERS FULLNAME";
        public static String SEL_TAXCODE_QUERY = "SORT U2_SALESTAX SHORT_DESCRIPTION RATE";

        // U2_AUTHORS layout
        public const int AUTHORS_FULLNAME = 1;
        public const int AUTHORS_BOOK_IDS = 2;

        // U2_BOOKS layout
        public const int BOOKS_TITLE = 1;
        public const int BOOKS_AUTHOR_ID = 2;
        public const int BOOKS_ISBN = 3;
        public const int BOOKS_DEPT = 4;
        public const int BOOKS_GENRE = 5;
        public const int BOOKS_MEDIA = 6;
        public const int BOOKS_PUBLISHER_ID = 7;
        public const int BOOKS_SALE_PRICE = 8;
        public const int BOOKS_STOCK_LEVEL = 9;
        public const int BOOKS_MIN_ORDER = 10;
        public const int BOOKS_ON_ORDER = 11;
        public const int BOOKS_TAX_CODE = 12;
        public const int BOOKS_SUPPLIER_ID = 13;
        public const int BOOKS_PURCH_PRICE = 14;
        public const int BOOKS_SUPPLIER_CODE = 16;
        public const int BOOKS_LONG_DESCRIPTION = 20;
        
        // U2_CLIENTS layout
        public const int CLIENTS_SURNAME = 1;
        public const int CLIENTS_FORENAME = 2;
        public const int CLIENTS_ADDRESS = 3;
        public const int CLIENTS_COUNTRY = 4;
        public const int CLIENTS_TERRITORY_ID = 5;
        public const int CLIENTS_JOIN_DATE = 6;
        public const int CLIENTS_ACCOUNT_HELD = 7;

        // U2_ORDERS layout
        public const int ORDERS_CLIENT_ID =1;
        public const int ORDERS_ORDER_STATUS =2;
        public const int ORDERS_DELIV_ADDR =3;
        public const int ORDERS_INSTRUCTIONS =3;
        public const int ORDERS_ORIGIN_CODE =4;
        public const int ORDERS_SHIP_ID =5;
        public const int ORDERS_SHIP_COST =6;
        public const int ORDERS_DESPATCH_DATE =7;
        public const int ORDERS_SALES_ID =8;
        public const int ORDERS_BOOK_ID =10;
        public const int ORDERS_QTY =11;
        public const int ORDERS_PRICE =12;
        public const int ORDERS_TAX_CODE =13;
        public const int ORDERS_DESPATCHED =14;
        public const int ORDERS_PROMO_ID =15 ;
        public const int ORDERS_TAX_RATE = 16;

        // U2_PAYMENTS layout
        public const int PAYMENTS_PAY_DATE =1;
        public const int PAYMENTS_CLIENT_ID =2;
        public const int PAYMENTS_AMOUNT =3 ;

        // U2_PUBLISHERS layout
        public const int PUBLISHERS_FULLNAME =1;
        public const int PUBLISHERS_BOOK_IDS =2 ;


        // U2_PURCHASES layout
        public const int PURCHASES_ORDER_DATE =1;
        public const int PURCHASES_SUPPLIER_ID =2;
        public const int PURCHASES_SHIP_COST =3;
        public const int PURCHASES_DELIV_DATE =4;
        public const int PURCHASES_BOOK_ID =10;
        public const int PURCHASES_ISBN =11;
        public const int PURCHASES_SUPPLIER_CODE =11;
        public const int PURCHASES_PRICE =12;
        public const int PURCHASES_TAX_CODE =13;
        public const int PURCHASES_ORDER_QTY =14;       
        public const int PURCHASES_REJECT_QTY =15; 

        // U2_SALESTAX layout
        public const int SALESTAX_SHORT_DESCRIPTION =1;
        public const int SALESTAX_RATE =2 ;

        // U2_SHIPPING Layout
        public const int SHIPPING_SHORT_DESCRIPTION =1;
        public const int SHIPPING_COST =2;

        // U2_SUPPLIERS Layout
        public const int SUPPLIERS_FULLNAME =1;
        public const int SUPPLIERS_ADDRESS =2;
        public const int SUPPLIERS_PHONE =3;
        public const int SUPPLIERS_FAX =4;
        public const int SUPPLIERS_EMAIL =5;
        public const int SUPPLIERS_SHIP_COST =10;

        // BOOK DATA Array
        public const int BOOKDATA_TITLE =1;
        public const int BOOKDATA_AUTHOR_ID =2;
        public const int BOOKDATA_AUTHOR_NAME =3;
        public const int BOOKDATA_ISBN =4;
        public const int BOOKDATA_DEPT =5;
        public const int BOOKDATA_GENRE =6;
        public const int BOOKDATA_MEDIA =7;
        public const int BOOKDATA_PUBLISHER_ID =8;
        public const int BOOKDATA_PUBLISHER_NAME =9;
        public const int BOOKDATA_SALE_PRICE =10;
        public const int BOOKDATA_STOCK_LEVEL =11;
        public const int BOOKDATA_MIN_ORDER =12;
        public const int BOOKDATA_ON_ORDER =13;
        public const int BOOKDATA_RESERVED =14;
        public const int BOOKDATA_TAX_CODE =15;
        public const int BOOKDATA_TAX_RATE =16;
        public const int BOOKDATA_SUPPLIER_ID =17;
        public const int BOOKDATA_SUPPLIER_NAME =18;
        public const int BOOKDATA_PURCH_PRICE =19;
        public const int BOOKDATA_LONG_DESCRIPTION =20  ;

        // BOOK SUMMARY Array
        public const int BOOKSUMMARY_BOOKID = 1;
        public const int BOOKSUMMARY_TITLE = 2;
        public const int BOOKSUMMARY_AUTHORNAME = 3;
        public const int BOOKSUMMARY_ISBN = 4;
        public const int BOOKSUMMARY_SALESPRICE = 5;
        public const int BOOKSUMMARY_MEDIA = 6;

        // CLIENT DATA Array
        public const int CLIENTDATA_CLIENTID    =1;
        public const int CLIENTDATA_FORENAME    =2;
        public const int CLIENTDATA_SURNAME     =3;
        public const int CLIENTDATA_ADDRESS     =4;
        public const int CLIENTDATA_COUNTRY     =5;
        public const int CLIENTDATA_JOINDATE    =6;
        public const int CLIENTDATA_ORDERIDS    =10;
        public const int CLIENTDATA_ORDERSTATUS =11;
        public const int CLIENTDATA_ORDERDATE   =12;
        public const int CLIENTDATA_ORDERTOTAL  =13;
        public const int CLIENTDATA_PAYMENTIDS  =15;
        public const int CLIENTDATA_PAYMENTAMT  =16;
        public const int CLIENTDATA_PAYMENTDATE = 17;





        
        
    }
}
