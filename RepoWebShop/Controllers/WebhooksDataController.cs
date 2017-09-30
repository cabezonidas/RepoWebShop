using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class WebhooksDataController : Controller
    {
        private readonly IPaymentNoticeRepository _paymentNoticeRepository;
        private readonly IMercadoPago _mp;

        public WebhooksDataController(IPaymentNoticeRepository paymentNoticeRepository, IMercadoPago mp)
        {
            _paymentNoticeRepository = paymentNoticeRepository;
            _mp = mp;
        }

        [Route("OnPaymentNotified/{notificationId}")]
        public IActionResult OnPaymentNotified(string notificationId)
        {
            var payment = _mp.GetPayment(notificationId);
            if (payment["status"]?.ToString() == "200")
            {
                PaymentNotice paymentInfo = new PaymentNotice(((payment["response"] as Hashtable)["collection"] as Hashtable));
                if (!String.IsNullOrWhiteSpace(paymentInfo.BookingId))
                    _paymentNoticeRepository.CreatePayment(paymentInfo);
            }
            return Ok();
        }
    }
}
