using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;
using System;

namespace RepoWebShop.Controllers
{
    public class WebhooksController : Controller
    {
        private readonly IMercadoPago _mp;
        private readonly IPaymentNotificationRepository _paymentWebhookRepository;
        public WebhooksController(IMercadoPago mp, IPaymentNotificationRepository paymentNotificationRepository)
        {
            _mp = mp;
            _paymentWebhookRepository = paymentNotificationRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var body = new StreamReader(Request.Body);
            var s = new JsonSerializer();
            PaymentNotification Notification = s.Deserialize<PaymentNotification>(
                new JsonTextReader(body)
            );

            Notification.MercadoPagoId = Notification.Id;
            Notification.Id = 0;

            if (!String.IsNullOrEmpty(Notification.PaymentId))
                _paymentWebhookRepository.CreatePayment(Notification);


            //Cambiar Development Variable to 
            var payment = _mp.GetPayment("2506822618");

            








            return Ok();
        }
    }
}
