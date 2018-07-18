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
		public IActionResult AddProductItem([FromBody] int id)
		{
			_cart.AddCatalogItemToCart(id);
			return Ok();
		}

		[HttpPost]
		[Route("RemoveProductItem")]
		public IActionResult RemoveProductItem([FromBody] int id)
		{
			_cart.RemoveCatalogItemFromCart(id);
			return Ok();
		}

		[HttpPost]
		[Route("ClearProductItem")]
		public IActionResult ClearProductItem([FromBody] int id)
		{
			_cart.ClearCatalogItemFromCart(id);
			return Ok();
		}
	}
}
