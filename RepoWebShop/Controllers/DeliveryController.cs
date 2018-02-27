using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly IConfiguration _config;

        public DeliveryController(IConfiguration config, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDeliveryRepository deliveryRepository, IMapper mapper, IShoppingCartRepository shoppingCart)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config; 
        }

        public IActionResult Index()
        {
            var model = _shoppingCart.GetShoppingCartDeliveryAddress();

            var viewModel = _mapper.Map<DeliveryAddress, DeliveryAddressViewModel>(model);

            if (viewModel == null)
                viewModel = new DeliveryAddressViewModel();

            viewModel.MinimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            viewModel.CostByBlock = _config.GetValue<int>("DeliveryCostByBlock");

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DeliveryAddressViewModel deliveryAddres)
        {
            deliveryAddres.MinimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            deliveryAddres.CostByBlock = _config.GetValue<int>("DeliveryCostByBlock");

            if (!ModelState.IsValid)
                return View(deliveryAddres);

            var distance = await _deliveryRepository.GetDistanceAsync(deliveryAddres.AddressLine1);
            if(distance > 3000)
            {
                if(distance > 0)
                    ModelState.AddModelError("DistanceNotCovered", $"La distancia debe ser menor a 3kms. Tu ubicación está a {(distance / 1000.0).ToString("#.##")} kms.");
                else
                    ModelState.AddModelError("DistanceError", "No pudimos calcular la distancia.");

                return View(deliveryAddres);
            }

            var delivery = _mapper.Map<DeliveryAddressViewModel, DeliveryAddress>(deliveryAddres);
            delivery.ShoppingCartId = _shoppingCart.GetShoppingCartId();
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
