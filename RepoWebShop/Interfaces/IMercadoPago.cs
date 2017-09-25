using System;
using System.Collections;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface IMercadoPago
    {
        bool SandboxMode();
        bool SandboxMode(bool enable);
        String GetAccessToken();
        Hashtable GetPayment(String id);
        Hashtable GetPaymentInfo(String id);
        Hashtable GetAuthorizedPayment(String id);
        Hashtable RefundPayment(String id);
        Hashtable CancelPayment(String id);
        Hashtable CancelPreapprovalPayment(String id);
        Hashtable SearchPayment(Dictionary<String, String> filters, long offset = 0, long limit = 0);
        Hashtable CreatePreference(String preference);
        Hashtable CreatePreference(Hashtable preference);
        Hashtable UpdatePreference(String id, String preference);
        Hashtable UpdatePreference(String id, Hashtable preference);
        Hashtable GetPreference(String id);
        Hashtable CreatePreapprovalPayment(String preapprovalPayment);
        Hashtable CreatePreapprovalPayment(Hashtable preapprovalPayment);
        Hashtable GetPreapprovalPayment(String id);
        Hashtable Get(String uri, Dictionary<String, String> parameters, bool authenticate);
        Hashtable Get(String uri, bool authenticate);
        Hashtable Get(String uri, Dictionary<String, String> parameters);
        Hashtable Post(String uri, String data);
        Hashtable Post(String uri, String data, Dictionary<String, String> parameters);
        Hashtable Post(String uri, Hashtable data);
        Hashtable Post(String uri, Hashtable data, Dictionary<String, String> parameters);
        Hashtable Put(String uri, String data);
        Hashtable Put(String uri, String data, Dictionary<String, String> parameters);
        Hashtable Put(String uri, Hashtable data);
        Hashtable Put(String uri, Hashtable data, Dictionary<String, String> parameters);
        Hashtable Delete(String uri);
        Hashtable Delete(String uri, Dictionary<String, String> parameters);
        string GetRepoPaymentLink(decimal total, string bookingId, string friendlyBookingId, string host, string title);
        Hashtable GetMerchantOrder(String merchantOrderId);
    }
}
