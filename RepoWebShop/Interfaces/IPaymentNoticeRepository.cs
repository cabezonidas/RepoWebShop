using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface IPaymentNoticeRepository
    {
        void CreatePayment(PaymentNotice paymentWebhook, string hostUrl);
        IEnumerable<PaymentNotice> GetPayments();
        PaymentNotice GetPayment(int id);
    }
}
