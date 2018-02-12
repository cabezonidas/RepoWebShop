using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class WebhooksDataController : Controller
    {
        private readonly IPaymentNoticeRepository _paymentNoticeRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMercadoPago _mp;

        public WebhooksDataController(IPaymentNoticeRepository paymentNoticeRepository, IMercadoPago mp, AppDbContext appDbContext)
        {
            _paymentNoticeRepository = paymentNoticeRepository;
            _mp = mp;
            _appDbContext = appDbContext;
        }

        [Route("OnPaymentNotNotified")]
        public async Task<IActionResult> OnPaymentNotNotified()
        {
            var pendings = _appDbContext.ShoppingCartItems.Select(x => x.ShoppingCartId).Distinct();
            var payments = await _mp.SearchPaymentAsync(new System.Collections.Generic.Dictionary<string, string>(), 10, 500);
            try
            {
                var results = ((payments["response"] as Hashtable)["results"] as ArrayList);
                foreach(Hashtable result in results)
                {
                    string bookingId = (result["collection"] as Hashtable)["external_reference"]?.ToString();
                    string id = (result["collection"] as Hashtable)["id"]?.ToString();
                    if(pendings.Contains(bookingId))
                    {
                        await OnPaymentNotified(id);
                        return Ok();
                    }
                }
                return Ok();
            }
            catch
            {
                return Ok();
            }
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
