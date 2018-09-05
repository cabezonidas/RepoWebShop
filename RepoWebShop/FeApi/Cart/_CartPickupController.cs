using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
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

		[HttpPost]
		[Route("SetPickupOption/{ticksId}")]
		public ShoppingCartPickUpDate SetPickupOption(string ticksId)
		{
			var ticksNumber = Convert.ToInt64(ticksId);
			string error;
			var result = _cart.TrySetPickUpDate(null, new DateTime(ticksNumber), out error);
			if (!result)
				throw new Exception("Invalid pickup date");
			return _cart.GetPickUpDate(null);
		}

		[HttpGet]
		[Route("GetPickupOption")]
		public ShoppingCartPickUpDate GetPickupOption() => _cart.GetPickUpDate(null);

		[HttpGet]
		[Route("PreparationTime")]
		public int PreparationTime() => _cart.GetPreparationTime(null);
	}
}
