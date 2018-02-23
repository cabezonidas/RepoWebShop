using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.ViewModels;
using System;
using RepoWebShop.Interfaces;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Models;
using Microsoft.AspNetCore.Identity;
using RepoWebShop.Extensions;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly IMercadoPago _mp;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IConfiguration _config;
        private readonly string _bookingId;
        private readonly string _friendlyBookingId;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly int _minimumArsForOrderDelivery;
        private readonly int _maxArsForReservation;
        private readonly int _minimumCharge;
        private readonly int _costByBlock;


        public ShoppingCartController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, ICalendarRepository calendarRepository, IPieRepository pieRepository, IShoppingCartRepository shoppingCart, IMercadoPago mp)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
            _bookingId = shoppingCart.GetShoppingCartId();
            _friendlyBookingId = _bookingId.Length >= 6 ? _bookingId?.Substring(_bookingId.Length - 6, 6) ?? string.Empty : String.Empty;
            _calendarRepository = calendarRepository;
            _mp = mp;
            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
            _minimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            _costByBlock = _config.GetValue<int>("DeliveryCostByBlock");
        }

        public IActionResult TrolleyIcon()
        {
            var result = new ShoppingCartViewModel
            {
                Items = _shoppingCart.GetShoppingCartItems()
            };
            return View(result);
        }

        public async Task<ViewResult> Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var totalItems = _shoppingCart.GetShoppingCartTotal();
            var highestPrepTime = _shoppingCart.GetShoppingCartPreparationTime();

            var user = await _userManager.GetUser(_signInManager);
            var delivery = _shoppingCart.GetShoppingCartDeliveryAddress();
            var deliveryCost = (totalItems >= 200 && delivery != null) ? delivery.DeliveryCost : 0;

            var total = totalItems + deliveryCost;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                Items = _shoppingCart.GetShoppingCartItems(),
                PickupDate = _calendarRepository.GetPickupEstimate(highestPrepTime),
                ShoppingCartTotal = total,
                //Mercadolink = await _mp.GetRepoPaymentLinkAsync(total, _bookingId, _friendlyBookingId, Request.Host.ToString(), "La Reposteria", user?.Id),
                PreparationTime = highestPrepTime,
                FriendlyBookingId = _friendlyBookingId,
                Comments = _shoppingCart.GetShoppingCartComments(),
                MercadoPagoId = _config.GetSection("MercadoPagoClientId").Value,
                User = user,
                DeliveryAddress = delivery,
                MaxArsForReservation = _maxArsForReservation,
                MinArsForDelivery = _minimumArsForOrderDelivery,
                MinimumDeliveryCharge = _minimumCharge,
                DeliveryCostByBlock = _costByBlock
            };
            
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.ActivePies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveDelivery()
        {
            _shoppingCart.RemoveDelivery();

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearFromShoppingCart(int pieId)
        {
            _shoppingCart.ClearFromCart(pieId);

            return RedirectToAction("Index");
        }

        public IActionResult Comments() => View();
        public IActionResult CheckDelivery(ShoppingCartViewModel vm) => View(vm);
        public IActionResult CheckMobile(ShoppingCartViewModel vm) => View(vm);
        public IActionResult CheckOutOptions(ShoppingCartViewModel vm) => View(vm);
        public IActionResult Faq(ShoppingCartViewModel vm) => View(vm);
        public IActionResult ShoppingCartItems(ShoppingCartViewModel vm) => View(vm);
    }
}
