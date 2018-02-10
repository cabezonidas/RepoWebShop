using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    public class DeliveryController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(DeliveryAddress deliveryAddres)
        {

            return View();
        }
    }
}
