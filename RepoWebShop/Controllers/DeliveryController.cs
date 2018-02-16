using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCartRepository _shoppingCart;

        public DeliveryController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDeliveryRepository deliveryRepository, IMapper mapper, IShoppingCartRepository shoppingCart)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var model = _shoppingCart.GetShoppingCartDeliveryAddress();

            var viewModel = _mapper.Map<DeliveryAddress, DeliveryAddressViewModel>(model);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DeliveryAddressViewModel deliveryAddres)
        {
            if(!ModelState.IsValid)
                return View(deliveryAddres);

            var distance = await _deliveryRepository.GetDistanceAsync(deliveryAddres.AddressLine1);
            if(distance > 3000)
            {
                if(distance > 0)
                    ModelState.AddModelError("DistanceNotCovered", $"La distancia debe ser menor a 3k. Tu ubicación está a {(distance / 1000.0).ToString("#.##")} kms.");
                else
                    ModelState.AddModelError("DistanceError", "No pudimos calcular la distancia.");

                return View(deliveryAddres);
            }

            var delivery = _mapper.Map<DeliveryAddressViewModel, DeliveryAddress>(deliveryAddres);
            delivery.ShoppingCartId = _shoppingCart.GetShoppingCartId();
            delivery.DeliveryCost = delivery.DeliveryEstimate;
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
