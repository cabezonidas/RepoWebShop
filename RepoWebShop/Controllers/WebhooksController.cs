using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Controllers
{
    public class WebhooksController : Controller
    {
        private readonly IMercadoPago _mp;
        private readonly IPaymentNoticeRepository _paymentWebhookRepository;

        public WebhooksController(IMercadoPago mp, IPaymentNoticeRepository paymentNotificationRepository)
        {
            _mp = mp;
            _paymentWebhookRepository = paymentNotificationRepository;
        }

        public void OnPaymentNotified(string notificationId)
        {
            var payment = _mp.GetPayment(notificationId);
            if (payment["status"]?.ToString() == "200")
            {
                PaymentNotice paymentInfo = new PaymentNotice(((payment["response"] as Hashtable)["collection"] as Hashtable));
                if(!String.IsNullOrWhiteSpace(paymentInfo.BookingId))
                    _paymentWebhookRepository.CreatePayment(paymentInfo);
            }
        }

        public IActionResult Index()
        {
            //try
            //{
                var Notification = ((JToken)new JsonSerializer().Deserialize<Object>(new JsonTextReader(new StreamReader(Request.Body)))).Root;
                var topic = ((JValue)Notification["topic"])?.Value.ToString();
                var notificationId = ((JValue)Notification["resource"])?.Value.ToString().Split('/').LastOrDefault();
                OnPaymentNotified(notificationId);
            //}
            //catch
            //{
            //    return BadRequest();
            //}
            return Ok();
        }
    }
}
