namespace RepoWebShop.Models
{
    public class PaymentNotificationRepository : IPaymentNotificationRepository
    {
        private readonly AppDbContext _appDbContext;
        public PaymentNotificationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CreatePayment(PaymentNotification paymentNotification)
        {
            _appDbContext.PaymentNotifications.Add(paymentNotification);
            _appDbContext.SaveChanges();
        }
    }
}
