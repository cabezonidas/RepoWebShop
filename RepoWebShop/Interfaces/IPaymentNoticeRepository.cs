using RepoWebShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IPaymentNoticeRepository
    {
        Task CreatePayment(PaymentNotice paymentWebhook);
        IEnumerable<PaymentNotice> GetPayments();
        PaymentNotice GetPayment(int id);
    }
}
