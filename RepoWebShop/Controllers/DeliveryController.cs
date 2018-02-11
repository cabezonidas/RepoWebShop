using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(DeliveryAddressViewModel deliveryAddres)
        {
            if(!ModelState.IsValid)
            {
                return View(deliveryAddres);
            }

            var distance = await _deliveryRepository.GetDistanceAsync(deliveryAddres.AddressLine1);
            if(distance > 3000)
            {
                if(distance > 0)
                    ModelState.AddModelError("DistanceNotCovered", $"La distancia debe ser menor a 3k. Tu ubicación está a {(distance / 1000.0).ToString("#.##")} kms.");
                else
                    ModelState.AddModelError("DistanceError", "No pudimos calcular la distancia.");

                return View(deliveryAddres);
            }

            


            return View();
        }
    }
}
