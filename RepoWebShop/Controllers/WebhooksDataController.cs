using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;
using System.Threading.Tasks;

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
        public async Task<IActionResult> OnPaymentNotified(string notificationId)
        {
            var payment = await _mp.GetPaymentAsync(notificationId);
            if (payment["status"]?.ToString() == "200")
            {
                PaymentNotice paymentInfo = new PaymentNotice(((payment["response"] as Hashtable)["collection"] as Hashtable));
                var merchantOrder = await _mp.GetMerchantOrderAsync(paymentInfo.Merchant_Order_Id);
                if(merchantOrder["status"]?.ToString() == "200")
                    paymentInfo.User_Id = (merchantOrder["response"] as Hashtable)["additional_info"]?.ToString();
                if (!String.IsNullOrWhiteSpace(paymentInfo.BookingId))
                    await _paymentNoticeRepository.CreatePayment(paymentInfo, Request.HostUrl());
            }
            return Ok();
        }
    }
}
