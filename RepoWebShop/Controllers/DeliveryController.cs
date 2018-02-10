using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoWebShop.Extensions;
using RepoWebShop.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    public class DeliveryController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(DeliveryAddress deliveryAddres)
        {
            if(!ModelState.IsValid)
            {
                return View(deliveryAddres);
            }

            var apicall = $"{Request.HostUrl()}/api/DeliveryData/GetDistance/{deliveryAddres.AddressLine1}";
            var result = await new HttpClient().GetAsync(apicall);

            var body = await result.Content.ReadAsStreamAsync();
            var DistanceObject = ((JToken)new JsonSerializer().Deserialize<Object>(new JsonTextReader(new StreamReader(body)))).Root;
            var distance = ((JProperty)((JContainer)DistanceObject.Root).First).Value;



            return View();
        }
    }
}
