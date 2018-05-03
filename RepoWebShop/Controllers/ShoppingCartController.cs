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
using Microsoft.AspNetCore.Http;

namespace RepoWebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCartRepository _cartRepository;
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
        private readonly int _deliveryRadius;

        private readonly IHttpContextAccessor _contextAccessor;


        public ShoppingCartController(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, ICalendarRepository calendarRepository, IPieRepository pieRepository, IShoppingCartRepository shoppingCart, IMercadoPago mp)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _pieRepository = pieRepository;
            _cartRepository = shoppingCart;
            _bookingId = shoppingCart.GetSessionCartId();
            _friendlyBookingId = _bookingId.Length >= 6 ? _bookingId?.Substring(_bookingId.Length - 6, 6) ?? string.Empty : String.Empty;
            _calendarRepository = calendarRepository;
            _mp = mp;
            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
            _minimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            _costByBlock = _config.GetValue<int>("DeliveryCostByBlock");
            _deliveryRadius = _config.GetValue<int>("DeliveryRadius");
        }

        public IActionResult TrolleyIcon()
        {
            var result = new ShoppingCartViewModel
            {
                Items = _cartRepository.GetItems(null).ToList()
            };
            return View(result);
        }

        public async Task<ViewResult> Index()
        {

            var highestPrepTime = _cartRepository.GetPreparationTime(null);

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                Items = _cartRepository.GetItems(null).ToList(),
                PickupDate = _calendarRepository.GetPickupEstimate(highestPrepTime),
                ShoppingCartTotal = _cartRepository.GetTotal(null),
                PreparationTime = highestPrepTime,
                FriendlyBookingId = _friendlyBookingId,
                ShopingCartTotalWithoutDiscount = _cartRepository.GetTotalWithoutDiscount(null),
                Comments = _cartRepository.GetComments(null)?.Comments,
                MercadoPagoId = _config.GetSection("MercadoPagoClientId").Value,
                User = await _userManager.GetUser(_signInManager),
                DeliveryAddress = _cartRepository.GetDelivery(null),
                MaxArsForReservation = _maxArsForReservation,
                MinArsForDelivery = _minimumArsForOrderDelivery,
                MinimumDeliveryCharge = _minimumCharge,
                DeliveryCostByBlock = _costByBlock,
                DeliveryRadius = _deliveryRadius,
                PickupTime = _cartRepository.GetTimeSlots(null),
                Discount = _cartRepository.GetDiscount(null)
        };
            
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.ActivePies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _cartRepository.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveDelivery()
        {
            _cartRepository.RemoveDelivery();

            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveDiscount()
        {
            _cartRepository.RemoveShoppingDiscount();

            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _cartRepository.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearFromShoppingCart(int pieId)
        {
            _cartRepository.ClearFromCart(pieId);

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
