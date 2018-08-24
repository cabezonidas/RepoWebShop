using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using System.Collections.Generic;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _CartCommentsController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		public _CartCommentsController(IShoppingCartRepository shoppingCart)
		{
			_cart = shoppingCart;
		}

		[HttpGet]
		[Route("Get")]
		public string Get()
		{
			var comments = _cart.GetComments(null);
			var result = comments?.Comments ?? string.Empty;
			return result;
		}

		[HttpPost]
		[Route("Add")]
		public string Add([FromBody] string comments)
		{
			_cart.AddComments(comments);
			var result = _cart.GetComments(null)?.Comments ?? string.Empty;

			return result;
		}
	}
}
