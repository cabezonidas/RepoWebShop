using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Repositories
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
        public void CreatePayment(PaymentNotice paymentNotification, string hostUrl)
        {
            _appDbContext.PaymentNotices.Add(paymentNotification);
            _appDbContext.SaveChanges();

            /*
            pending         - The user has not yet completed the payment process
            approved        - The payment has been approved and accredited
            authorized      - The payment has been authorized but not captured yet
            in_process      - Payment is being reviewed
            in_mediation    - Users have initiated a dispute
            rejected        - Payment was rejected. The user may retry payment.
            cancelled       - Payment was cancelled by one of the parties or because time for payment has expired
            refunded        - Payment was refunded to the user
            charged_back    - Was made a chargeback in the buyer’s credit card
            */

            if (paymentNotification.Status == "approved")
            {
                Order orderApproved = _orderRespository.OrderApproved(paymentNotification);
                _emailRespository.Send(orderApproved, hostUrl, paymentNotification);
            }

            else if (paymentNotification.Status == "in_process")
            {
                Order order = _orderRespository.OrderInProcess(paymentNotification);
            }

            else
            {
                _orderRespository.UpdateOrder(paymentNotification);
            }

        }

        public IEnumerable<PaymentNotice> GetPayments()
        {
            return _appDbContext.PaymentNotices.OrderByDescending(x => x.Id).ToList();
        }

        public PaymentNotice GetPayment(int id)
        {
            return _appDbContext.PaymentNotices.First(x => x.Id == id);
        }
    }
}
