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
        private readonly IPaymentNoticeRepository _paymentWebhookRepository;
        private readonly IOrderRepository _orderRespository;
        private readonly IEmailRepository _emailRespository;
        public WebhooksController(IEmailRepository emailRespository, IMercadoPago mp, IPaymentNoticeRepository paymentNotificationRepository, IOrderRepository orderRespository)
        {
            _mp = mp;
            _paymentWebhookRepository = paymentNotificationRepository;
            _orderRespository = orderRespository;
            _emailRespository = emailRespository;
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
                        var paymentInfoResponse = ((payment["response"] as Hashtable)["collection"] as Hashtable);
                        PaymentNotice paymentInfo = new PaymentNotice(paymentInfoResponse);

                        var merchantOrderInfo = _mp.GetMerchantOrder(paymentInfo.Merchant_Order_Id);
                        if (merchantOrderInfo["status"]?.ToString() == "200")
                            paymentInfo.Order_Code = (merchantOrderInfo["response"] as Hashtable)["additional_info"]?.ToString();

                        _paymentWebhookRepository.CreatePayment(paymentInfo);
                        Order order = _orderRespository.UpdateOrder(paymentInfo);

                        _emailRespository.Send(order, paymentInfo);
                        //order?.CustomerComments
                        //_orderRespository.GetOrderDetails(6021).CustomerComments
                    }
                }
            }
            return Ok();
        }
    }
}
