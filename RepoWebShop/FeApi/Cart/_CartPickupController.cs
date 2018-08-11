using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using System.Collections.Generic;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _CartPickupController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		public _CartPickupController(IShoppingCartRepository shoppingCart)
		{
			_cart = shoppingCart;
		}

		[HttpGet]
		[Route("PickUpOptionsByDay")]
		public IEnumerable<_PickUpOptions> PickUpOptionsByDay() => _cart.PickUpOptionsByDay();
	}
}
