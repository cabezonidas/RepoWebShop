using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System.Linq;

namespace RepoWebShop.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartSummary(IShoppingCartRepository shoppingCart)
        {
            _cartRepository = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _cartRepository.GetItems(null);
            //_shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                //ShoppingCart = _shoppingCart,
                Items = _cartRepository.GetItems(null).ToList(),
                ShoppingCartTotal = _cartRepository.GetItemsTotal(null)
            };
            return View(shoppingCartViewModel);
        }
    }
}
