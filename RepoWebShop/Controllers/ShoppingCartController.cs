using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using mercadopago;
using RepoWebShop.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.IO;
namespace RepoWebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IHostingEnvironment _env;
        private readonly MP _mp;
        private readonly string _bookingId;


        public ShoppingCartController(IOrderRepository orderRepository, IPieRepository pieRepository, ShoppingCart shoppingCart, IHostingEnvironment env)
        {
            _orderRepository = orderRepository;
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
            _env = env;
            _bookingId = Path.GetRandomFileName().Replace(".", "").Substring(0, 6).ToUpper();
            _mp = new MP("8551380243694935", "xCQbHtu06Y3vBZvYY2wTg1zJ4qf0dRBd");
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var total = _shoppingCart.GetShoppingCartTotal();
            var highestPrepTime = _shoppingCart.GetShoppingCartPreparationTime();


            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = total,
                Mercadolink = _mp.GetPaymentLink(total, _bookingId, _env.IsProduction(), Request.Host.ToString()),
                PreparationTime = highestPrepTime,
                BookingId = _bookingId
            };
            
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}
