using System;
using System.Collections.Generic;

namespace u2_books
{
    public delegate void ShowBookEvent( object sender, string bookId, Boolean forEdit, Boolean isModal);
    public delegate void ShowClientEvent(object sender, string clientId, Boolean forEdit, Boolean isModal);
    public delegate void ShowOrderEvent(object sender, string orderId, Boolean forEdit, Boolean isModal);
    public delegate void ShowPOEvent(object sender, string orderId, Boolean forEdit, Boolean isModal);
    
    public delegate void ShowOrderHistoryEvent(object sender, string bookId, Boolean isModal);
    public delegate void ShowOrderReportEvent(object sender, string orderId, string clientId, Boolean isModal);
    public delegate void ShowPurchaseHistoryEvent(object sender, string bookId, Boolean isModal);
    
    public delegate Boolean SelectClientEvent( object sender, ref string clientId);
    public delegate Boolean SelectBookEvent( object sender, string searchText, ref string bookId);

    public delegate void CreatePOEvent( object sender, List<String> pos);

}