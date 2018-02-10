using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace RepoWebShop.Controllers
{
    public class WebhooksController : Controller
    {
        public WebhooksController()
        {
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult MercadoPago()
        {
            var Notification = ((JToken)new JsonSerializer().Deserialize<Object>(new JsonTextReader(new StreamReader(Request.Body)))).Root;
            var topic = ((JValue)Notification["topic"])?.Value.ToString();
            var notificationId = ((JValue)Notification["resource"])?.Value.ToString().Split('/').LastOrDefault();

            Task.Run(async () =>
            {
                var apicall = $"http://{Request.Host.ToString()}/api/WebhooksData/OnPaymentNotified/{notificationId}";
                await new HttpClient().GetAsync(apicall);
            });

            return Ok();
        }
    }
}
