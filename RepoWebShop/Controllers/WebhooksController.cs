using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    public class WebhooksController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Ok();
        }

        /*private void SeedWebhooks(APIContext apiContext)
        {
            var callBackUrl = Url.Action("Receive", "WebhookEvents", null, Request.Url.Scheme);
            //Request.Host.ToString()??

            if(Request.Url.Host == "localhost")
            {
                callBackUrl = "https://443tregf.ngrok.io/WebhookEvents/Receive";
            }
            var everythingWebhook = new Webhook()
            {
                url = callBackUrl,
                event_types = new List<WebhookEventType>
                {
                    new WebhookEventType
                    {
                        name = "PAYMENT.SALE.REFUNDED"
                    },
                    new WebhookEventType
                    {
                        name = "PAYMENT.SALE.REVERSED"
                    }
                };
             }
        }*/
    }
}
