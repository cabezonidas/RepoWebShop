namespace RepoWebShop.Models
{
    public class PaymentNoticeRepository : IPaymentNoticeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRespository;
        private readonly IEmailRepository _emailRespository;
        private readonly ShoppingCart _shoppingCart;

        public PaymentNoticeRepository(AppDbContext appDbContext, IOrderRepository orderRepository, IEmailRepository emailRespository, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _orderRespository = orderRepository;
            _emailRespository = emailRespository;
            _shoppingCart = shoppingCart;
        }
        public void CreatePayment(PaymentNotice paymentNotification)
        {
            _appDbContext.PaymentNotices.Add(paymentNotification);
            _appDbContext.SaveChanges();

            //_shoppingCart.ClearCart();
            Order order = _orderRespository.UpdateOrder(paymentNotification);
            _emailRespository.Send(order, paymentNotification);
        }
    }
}
