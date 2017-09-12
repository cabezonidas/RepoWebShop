namespace RepoWebShop.Models
{
    public interface IPaymentNotificationRepository
    {
        void CreatePayment(PaymentNotification paymentWebhook);
    }
}