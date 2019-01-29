using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace RepoWebShop.MvcControllers
{
    public class WebhooksController : Controller
    {
        public WebhooksController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult MercadoPago([FromBody] MercadoPagoWebhook notification)
        {
			//var Notification = ((JToken)new JsonSerializer().Deserialize<Object>(new JsonTextReader(new StreamReader(Request.Body)))).Root;
			//var topic = ((JValue)Notification["topic"])?.Value.ToString();
			//var notificationId = ((JValue)Notification["resource"])?.Value.ToString().Split('/').LastOrDefault();

			var notificationId = notification?.Data?.Id;

			if (string.IsNullOrEmpty(notificationId))
				return BadRequest();

            Task.Run(async () =>
            {
                var apicall = $"http://{Request.Host.ToString()}/api/WebhooksData/OnPaymentNotified/{notificationId}";
                await new HttpClient().GetAsync(apicall);
            });

            return Ok();
        }
    }
}
