using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PaymentNoticeRepository : IPaymentNoticeRepository
    {
        private readonly AppDbContext _appDbContext;
        public PaymentNoticeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CreatePayment(PaymentNotice paymentNotification)
        {
            _appDbContext.PaymentNotices.Add(paymentNotification);
            _appDbContext.SaveChanges();
        }
    }
}
