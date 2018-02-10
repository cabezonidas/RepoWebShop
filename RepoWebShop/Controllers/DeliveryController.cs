using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var apicall = $"http://{Request.Host.ToString()}/api/GetDistance/{deliveryAddres.AddressLine1}";
            var result = await new HttpClient().GetAsync(apicall);


            return View();
        }
    }
}
