using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.ViewModels;
using System;
using RepoWebShop.Interfaces;
using Microsoft.Extensions.Configuration;

namespace RepoWebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly IMercadoPago _mp;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IConfiguration _config;
        private readonly string _bookingId;
        private readonly string _friendlyBookingId;


        public ShoppingCartController(IConfiguration config, ICalendarRepository calendarRepository, IOrderRepository orderRepository, IPieRepository pieRepository, IShoppingCartRepository shoppingCart, IMercadoPago mp)
        {
            _config = config;
            _orderRepository = orderRepository;
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
            _bookingId = shoppingCart.GetShoppingCartId();
            _friendlyBookingId = _bookingId.Length >= 6 ? _bookingId?.Substring(_bookingId.Length - 6, 6) ?? string.Empty : String.Empty;
            _calendarRepository = calendarRepository;
            _mp = mp;
        }

        public IActionResult TrolleyIcon()
        {
            var result = new ShoppingCartViewModel
            {
                Items = _shoppingCart.GetShoppingCartItems()
            };
            return View(result);
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            //_shoppingCart.ShoppingCartItems = items;
            var total = _shoppingCart.GetShoppingCartTotal();
            var highestPrepTime = _shoppingCart.GetShoppingCartPreparationTime();


            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                //ShoppingCart = _shoppingCart,
                Items = _shoppingCart.GetShoppingCartItems(),
                PickupDate = _calendarRepository.GetPickupEstimate(highestPrepTime),
                ShoppingCartTotal = total,
                Mercadolink = _mp.GetRepoPaymentLink(total, _bookingId, _friendlyBookingId, Request.Host.ToString(), "La Reposteria"),
                PreparationTime = highestPrepTime,
                FriendlyBookingId = _friendlyBookingId,
                Comments = _shoppingCart.GetShoppingCartComments(),
                MercadoPagoId = _config.GetSection("MercadoPagoClientId").Value
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
    }
}
