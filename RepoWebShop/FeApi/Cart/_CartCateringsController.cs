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
	public class _CartCateringsController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		private readonly ILunchRepository _catering;
		public _CartCateringsController(IShoppingCartRepository shoppingCart, ILunchRepository catering)
		{
			_cart = shoppingCart;
			_catering = catering;
		}

		[HttpGet]
		[Route("All")]
		public IEnumerable<ShoppingCartComboCatering> All() => _cart.GetShoppingCaterings(null);

		[HttpPost]
		[Route("Add")]
		public async Task<IEnumerable<ShoppingCartComboCatering>> Add([FromBody] int id)
		{
			var catering = await _catering.GetLunchByIdAsync(id);
			_cart.AddCateringToCart(catering, 1);
			return _cart.GetShoppingCaterings(null);
		}

		[HttpPost]
		[Route("Remove")]
		public async Task<IEnumerable<ShoppingCartComboCatering>> Remove([FromBody] int id)
		{
			var catering = await _catering.GetLunchByIdAsync(id);
			_cart.RemoveLunchFromCart(catering);
			return _cart.GetShoppingCaterings(null);
		}

		[HttpPost]
		[Route("ClearCatering")]
		public IEnumerable<ShoppingCartComboCatering> Clear([FromBody] int id)
		{
			_cart.ClearCateringFromCart(id);
			return _cart.GetShoppingCaterings(null);
		}
	}
}
