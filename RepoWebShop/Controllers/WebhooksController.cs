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
                        var paymentInfoResponse = ((payment["response"] as Hashtable)["collection"] as Hashtable);
                        PaymentInfo paymentInfo = new PaymentInfo(paymentInfoResponse);

                        var merchantOrderInfo = _mp.GetMerchantOrder(paymentInfo.Merchant_Order_Id);
                        if (merchantOrderInfo["status"]?.ToString() == "200")
                        {
                            paymentInfo.Order_Code = (merchantOrderInfo["response"] as Hashtable)["additional_info"]?.ToString();
                            //Guardar notificacion en base de datos, y cambiar el estado en las ordenes :)
                            //Manegar error por si viene un codigo que no esta en la base de datos.
                            //Revisar que el codigo de pedido sea siempre diferente
                        }
                        else
                            return NotFound();
                    }
                }
            }
            return Ok();
        }
    }
}
