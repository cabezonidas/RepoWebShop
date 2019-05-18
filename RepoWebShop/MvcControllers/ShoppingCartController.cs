using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.ViewModels;
using System;
using RepoWebShop.Interfaces;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Models;
using Microsoft.AspNetCore.Identity;
using RepoWebShop.Extensions;
using RepoWebShop.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RepoWebShop.MvcControllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMercadoPago _mp;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IConfiguration _config;
        private readonly ILunchRepository _lunchRep;
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


        public ShoppingCartController(ILunchRepository lunchRep, ICatalogRepository catalogRepository, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, ICalendarRepository calendarRepository, IPieRepository pieRepository, IShoppingCartRepository shoppingCart, IMercadoPago mp)
        {
            _lunchRep = lunchRep;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _pieRepository = pieRepository;
            _catalogRepository = catalogRepository;
            _cartRepository = shoppingCart;
            _bookingId = shoppingCart.GetSessionCartId();
            _friendlyBookingId = _bookingId.Ending(6);
            _calendarRepository = calendarRepository;
            _mp = mp;
            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
            _minimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            _costByBlock = _config.GetValue<int>("DeliveryCostByBlock");
            _deliveryRadius = _config.GetValue<int>("DeliveryRadius");
        }

        [HttpGet]
        public IActionResult TrolleyIcon()
        {
            var result = new ShoppingCartViewModel
            {
                Items = _cartRepository.GetItems(null)
            };
            return View(result);
        }

        [HttpGet]
        
        public async Task<ViewResult> Index()
        {
			ShoppingCartViewModel res = await _cartRepository.GetShoppingCartViewModel();


			//var shoppingCartViewModel = new ShoppingCartViewModel
			//{
			//	Cuit = _Cuit,
			//	Items = _Items,
			//	CatalogItems = _CatalogItems,
			//	CustomCatering = _CustomCatering,
			//	Caterings = _Caterings,
			//	PickupDate = _PickupDate,
			//	ShoppingCartTotal = _ShoppingCartTotal,
			//	PreparationTime = _PreparationTime,
			//	FriendlyBookingId = _FriendlyBookingId,
			//	ShopingCartTotalWithoutDiscount = _ShopingCartTotalWithoutDiscount,
			//	Comments = _Comments,
			//	MercadoPagoId = _MercadoPagoId,
			//	User = _User,
			//	DeliveryAddress = _DeliveryAddress,
			//	MaxArsForReservation = _MaxArsForReservation,
			//	MinArsForDelivery = _MinArsForDelivery,
			//	MinimumDeliveryCharge = _MinimumDeliveryCharge,
			//	DeliveryCostByBlock = _DeliveryCostByBlock,
			//	DeliveryRadius = _DeliveryRadius,
			//	PickupTime = _PickupTime,
			//	Discount = _Discount
			//};
            return View(res);
        }

        [HttpGet]
        
        public RedirectResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.ActivePies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _cartRepository.AddToCart(selectedPie, 1);
            }
            return Redirect("/cart");
        }

        [HttpGet]
        
        public async Task<RedirectResult> AddProductToShoppingCart(int id)
        {
            var product = (await _catalogRepository.GetAvailableToBuyOnline()).FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                _cartRepository.AddCatalogItemToCart(product, 1);
            }
            return Redirect("/cart");
		}

        [HttpGet]
        
        public async Task<RedirectResult> AddCateringToShoppingCart(int id)
        {
            var catering = await _lunchRep.GetLunchByIdAsync(id);
            if (catering != null)
                _cartRepository.AddCateringToCart(catering, amount: 1);
            return Redirect("/cart");
        }

        [HttpGet]
        
        public RedirectToActionResult RemoveDelivery()
        {
            _cartRepository.RemoveDelivery();

            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public RedirectToActionResult RemoveDiscount()
        {
            _cartRepository.RemoveShoppingDiscount();

            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public RedirectToActionResult ClearCustomCateringFromCart()
        {
            _cartRepository.ClearCustomCateringFromCart();
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _cartRepository.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public async Task<RedirectToActionResult> RemoveCatalogProductFromShoppingCart(int productId)
        {
            var selectedProduct = await _catalogRepository.GetById(productId);
            _cartRepository.RemoveProductFromCart(selectedProduct);
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public async Task<RedirectToActionResult> RemoveCateringFromCart(int lunchId)
        {
            var lunch = await _lunchRep.GetLunchByIdAsync(lunchId);
            _cartRepository.RemoveLunchFromCart(lunch);
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public RedirectToActionResult ClearFromShoppingCart(int pieId)
        {
            _cartRepository.ClearFromCart(pieId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public RedirectToActionResult ClearCatalogProductFromShoppingCart(int productId)
        {
            _cartRepository.ClearCatalogItemFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public RedirectToActionResult ClearCateringFromCart(int cateringId)
        {
            _cartRepository.ClearCateringFromCart(cateringId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Comments() => View();

        [HttpGet]
        public IActionResult CheckDelivery(ShoppingCartViewModel vm) => View(vm);

        [HttpGet]
        public IActionResult CheckMobile(ShoppingCartViewModel vm) => View(vm);

        [HttpGet]
        public IActionResult CheckOutOptions(ShoppingCartViewModel vm) => View(vm);

        [HttpGet]
        public IActionResult Faq(ShoppingCartViewModel vm) => View(vm);

        [HttpGet]
        public IActionResult ShoppingCartItems(ShoppingCartViewModel vm) => View(vm);
    }
}
