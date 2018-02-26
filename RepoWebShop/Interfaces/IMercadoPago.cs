using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IMercadoPago
    {
        bool SandboxMode();
        bool SandboxMode(bool enable);
        Task<String> GetAccessTokenAsync();
        Task<Hashtable> GetPaymentAsync(String id);
        Task<Hashtable> GetPaymentInfoAsync(String id);
        Task<Hashtable> GetAuthorizedPaymentAsync(String id);
        Task<Hashtable> RefundPaymentAsync(String id);
        Task<Hashtable> CancelPaymentAsync(String id);
        Task<Hashtable> CancelPreapprovalPaymentAsync(String id);
        Task<Hashtable> SearchPaymentAsync(Dictionary<String, String> filters, long offset = 0, long limit = 0);
        Task<Hashtable> CreatePreferenceAsync(String preference);
        Task<Hashtable> CreatePreferenceAsync(Hashtable preference);
        Task<Hashtable> UpdatePreferenceAsync(String id, String preference);
        Task<Hashtable> UpdatePreferenceAsync(String id, Hashtable preference);
        Task<Hashtable> GetPreferenceAsync(String id);
        Task<Hashtable> CreatePreapprovalPaymentAsync(String preapprovalPayment);
        Task<Hashtable> CreatePreapprovalPaymentAsync(Hashtable preapprovalPayment);
        Task<Hashtable> GetPreapprovalPaymentAsync(String id);
        Task<Hashtable> GetAsync(String uri, Dictionary<String, String> parameters, bool authenticate);
        Task<Hashtable> GetAsync(String uri, bool authenticate);
        Task<Hashtable> GetAsync(String uri, Dictionary<String, String> parameters);
        Task<Hashtable> PostAsync(String uri, String data);
        Task<Hashtable> PostAsync(String uri, String data, Dictionary<String, String> parameters);
        Task<Hashtable> PostAsync(String uri, Hashtable data);
        Task<Hashtable> PostAsync(String uri, Hashtable data, Dictionary<String, String> parameters);
        Task<Hashtable> PutAsync(String uri, String data);
        Task<Hashtable> PutAsync(String uri, String data, Dictionary<String, String> parameters);
        Task<Hashtable> PutAsync(String uri, Hashtable data);
        Task<Hashtable> PutAsync(String uri, Hashtable data, Dictionary<String, String> parameters);
        Task<Hashtable> DeleteAsync(String uri);
        Task<Hashtable> DeleteAsync(String uri, Dictionary<String, String> parameters);
        Task<Hashtable> GetMerchantOrderAsync(String merchantOrderId);
        string BuildPreference(decimal total, string bookingId, string friendlyBookingId, string host, string title, string userId);
    }
}
