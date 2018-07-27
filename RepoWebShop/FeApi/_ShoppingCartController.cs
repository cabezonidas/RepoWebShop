using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _ShoppingCartController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		public _ShoppingCartController(IShoppingCartRepository shoppingCart)
		{
			_cart = shoppingCart;
		}

		[HttpGet]
		[Route("GetProductItems")]
		public IEnumerable<ShoppingCartCatalogItem> GetProductItems() => _cart.GetCatalogItems(null);

		[HttpPost]
		[Route("AddProductItem")]
		public IEnumerable<ShoppingCartCatalogItem> AddProductItem([FromBody] int id)
		{
			_cart.AddCatalogItemToCart(id);
			return _cart.GetCatalogItems(null);
		}

		[HttpPost]
		[Route("RemoveProductItem")]
		public IEnumerable<ShoppingCartCatalogItem> RemoveProductItem([FromBody] int id)
		{
			_cart.RemoveCatalogItemFromCart(id);
			return _cart.GetCatalogItems(null);
		}

		[HttpPost]
		[Route("ClearProductItem")]
		public IEnumerable<ShoppingCartCatalogItem> ClearProductItem([FromBody] int id)
		{
			_cart.ClearCatalogItemFromCart(id);
			return _cart.GetCatalogItems(null);
		}
	}
}
