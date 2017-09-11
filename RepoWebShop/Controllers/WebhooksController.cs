using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using RepoWebShop.Models;
using Newtonsoft.Json;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    public class WebhooksController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (Request.Body.CanSeek)
            {
                // Reset the position to zero to read from the beginning.
                Request.Body.Position = 0;
            }

            //var input = new StreamReader(Request.Body).ReadToEnd();



            JsonSerializer s = new JsonSerializer();
            var test = s.Deserialize<Webhook>(new JsonTextReader(new StreamReader(Request.Body)));

            


            /*
             * 
             * "{\"id\":123,\"live_mode\":false,\"type\":\"test\",\"date_created\":\"2017-09-09T21:12:32.706-04:00\",\"user_id\":\"58959397\",\"api_version\":\"v1\",\"action\":\"test.created\",\"data\":{\"id\":\"56456123212\"}}"
             * 
             * 
             * con esto, tengo que ir a preguntar la data del payment
             * */
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
