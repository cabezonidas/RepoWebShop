using RepoWebShop.Models;

namespace RepoWebShop.Interfaces
{
    public interface IPaymentNoticeRepository
    {
        void CreatePayment(PaymentNotice paymentWebhook);
    }
}
