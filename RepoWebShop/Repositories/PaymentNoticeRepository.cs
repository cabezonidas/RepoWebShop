using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class PaymentNoticeRepository : IPaymentNoticeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRespository;
        private readonly IEmailRepository _emailRespository;

        public PaymentNoticeRepository(AppDbContext appDbContext, IOrderRepository orderRepository, IEmailRepository emailRespository)
        {
            _appDbContext = appDbContext;
            _orderRespository = orderRepository;
            _emailRespository = emailRespository;
        }

        public async Task CreatePayment(PaymentNotice paymentNotification)
        {
            _appDbContext.PaymentNotices.Add(paymentNotification);
            _appDbContext.SaveChanges();

            if(_orderRespository.ValidBookingId(paymentNotification.BookingId))
                await _orderRespository.PaymentNotifiedAsync(paymentNotification);

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
        }

        public IEnumerable<PaymentNotice> GetPayments()
        {
            return _appDbContext.PaymentNotices.OrderByDescending(x => x.Id).ToArray();
        }

        public PaymentNotice GetPayment(int id)
        {
            return _appDbContext.PaymentNotices.First(x => x.Id == id);
        }
    }
}
