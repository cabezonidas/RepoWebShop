using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;
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

            PaymentNotification Notification = s.Deserialize<PaymentNotification>(
                new JsonTextReader(body)
            );

            Notification.MercadoPagoId = Notification.Id;
            Notification.Id = 0;

            if (!String.IsNullOrEmpty(Notification.PaymentId))
                _paymentWebhookRepository.CreatePayment(Notification);


            //Cambiar Development Variable to 
            var payment = _mp.GetPayment("2506822618");

            //payment.ContainsKey("response");
            var paymentInfoResponse = ((payment["response"] as Hashtable)["collection"] as Hashtable);



            //PaymentInfo paymentInfo = new PaymentInfo()
            //{
            //    MercadoPagoPaymentId = paymentInfoResponse["id"],
            //    Payment_Type = paymentInfoResponse["payment_type"]?.ToString(),
            //    Total_Paid_Amount = (Decimal)paymentInfoResponse["payment_type"],
            //    Order_Id = paymentInfoResponse["order_id"]?.ToString(),
            //    Reason = paymentInfoResponse["reason"]?.ToString(),
            //    Date_Created = (DateTime)paymentInfoResponse["date_created"],
            //    Status = paymentInfoResponse["status"]?.ToString()
            //};








            return Ok();
        }
    }
}
