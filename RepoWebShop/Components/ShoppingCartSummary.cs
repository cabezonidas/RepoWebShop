using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartRepository _shoppingCart;

        public ShoppingCartSummary(IShoppingCartRepository shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            //_shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                //ShoppingCart = _shoppingCart,
                Items = _shoppingCart.GetShoppingCartItems(),
                ShoppingCartTotal = _shoppingCart.GetShoppingCartItemsTotal()
            };
            return View(shoppingCartViewModel);
        }
    }
}
