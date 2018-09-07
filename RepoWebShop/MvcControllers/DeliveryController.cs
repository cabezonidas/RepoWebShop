using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoWebShop.Extensions;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IConfiguration _config;

        public DeliveryController(IConfiguration config, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDeliveryRepository deliveryRepository, IMapper mapper, IShoppingCartRepository shoppingCart)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
            _cartRepository = shoppingCart;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _cartRepository.GetDelivery(null);

            var viewModel = _mapper.Map<DeliveryAddress, DeliveryAddressViewModel>(model);

            if (viewModel == null)
                viewModel = new DeliveryAddressViewModel();

            viewModel.MinimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            viewModel.CostByBlock = _config.GetValue<int>("DeliveryCostByBlock");
            viewModel.DeliveryRadius = _config.GetValue<int>("DeliveryRadius");

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Estimate() => View();

        [HttpPost]
        public async Task<IActionResult> Index(DeliveryAddressViewModel deliveryAddres)
        {
			var originalRequest = deliveryAddres.AddressLine1;
            deliveryAddres.MinimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            deliveryAddres.CostByBlock = _config.GetValue<int>("DeliveryCostByBlock");
            deliveryAddres.DeliveryRadius = _config.GetValue<int>("DeliveryRadius");

            try
            {
                var potentialPlace = await _deliveryRepository.GuessPlaceIdAsync(deliveryAddres.AddressLine1);
                var placeConfirmed = await _deliveryRepository.GetPlaceAsync(potentialPlace);
                deliveryAddres.AddressLine1 = placeConfirmed.StreetName + " " + placeConfirmed.StreetNumber + ", " + placeConfirmed.PostalCode;
                deliveryAddres.ZipCode = placeConfirmed.PostalCode;
                deliveryAddres.StreetName = placeConfirmed.StreetName;
                deliveryAddres.StreetNumber = placeConfirmed.StreetNumber;
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("InvalidAddress", ex.Message);
            }

            if(!ModelState.IsValid)
            {
                return View(deliveryAddres);
            }

            var distance = await _deliveryRepository.GetDistanceAsync(originalRequest);
            if(distance > deliveryAddres.DeliveryRadius)
            {
                if(distance > 0)
					// ModelState.AddModelError("DistanceNotCovered", $"La distancia debe ser menor a {deliveryAddres.DeliveryRadius/1000}kms. Tu ubicación está a {(distance / 1000.0).ToString("#.##")} kms.");
					ModelState.AddModelError("DistanceNotCovered", $"Lamentablemente no nuestro envío por ahora no llega hasta tu dirección. Llamanos al 4925-0262 para coordinar.");
				else
                    ModelState.AddModelError("DistanceError", "No pudimos calcular la distancia.");

                return View(deliveryAddres);
            }

            var delivery = _mapper.Map<DeliveryAddressViewModel, DeliveryAddress>(deliveryAddres);
            delivery.ShoppingCartId = _cartRepository.GetSessionCartId();
            delivery.DeliveryCost = _deliveryRepository.GetDeliveryEstimate(distance);
            delivery.Distance = distance;
            

            var user = await _userManager.GetUser(_signInManager);
            if (user != null)
                delivery.User = user;

            try
            {
                _deliveryRepository.AddOrUpdateDelivery(delivery);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(ex.InnerException.Message, ex.Message);
                return View(deliveryAddres);
            }

            return RedirectToAction("Index", "ShoppingCart", null);
        }
    }
}
