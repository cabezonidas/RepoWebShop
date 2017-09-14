using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

            Object Notification = s.Deserialize<Object>(
                new JsonTextReader(body)
            );

            var topic = ((JValue)((JContainer)((JToken)Notification).Root)["topic"])?.Value.ToString();

            if(topic == "payment")
            {
                var resource = ((JValue)((JContainer)((JToken)Notification).Root)["resource"])?.Value.ToString();
                var notificationApi = "https://api.mercadolibre.com/collections/notifications/";
                if(resource.StartsWith(notificationApi))
                {
                    var notificationId = resource.Split('/').Last();
                    var payment = _mp.GetPayment(notificationId);
                    if (payment["status"]?.ToString() == "200")
                    {
                        //_paymentWebhookRepository.CreatePayment(Notification);
                        var paymentInfoResponse = ((payment["response"] as Hashtable)["collection"] as Hashtable);
                        PaymentInfo paymentInfo = new PaymentInfo(paymentInfoResponse);
                    }
                }
            }
            

            //"{\"topic\":\"payment\",\"resource\":\"https://api.mercadolibre.com/collections/notifications/2986651030\"}"
            //["net_received_account"]

            //if(Notification != null)
            //{
            //    Notification.MercadoPagoId = Notification.Id;
            //    Notification.Id = 0;

            //    if (!String.IsNullOrEmpty(Notification.PaymentId))
            //    {
            //        var payment = _mp.GetPayment(Notification.PaymentId);
            //        if (payment["status"]?.ToString() == "200")
            //        {
            //            _paymentWebhookRepository.CreatePayment(Notification);
            //            var paymentInfoResponse = ((payment["response"] as Hashtable)["collection"] as Hashtable);
            //            PaymentInfo paymentInfo = new PaymentInfo(paymentInfoResponse);
            //        }
            //    }
            //}



            //var payment = _mp.GetPayment("2506822618");
            //payment.ContainsKey("response");





            return Ok();
        }
    }
}
