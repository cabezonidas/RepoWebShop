using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using System.Collections.Generic;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _CartTotalsController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		public _CartTotalsController(IShoppingCartRepository shoppingCart)
		{
			_cart = shoppingCart;
		}

		[HttpGet]
		[Route("Total")]
		public decimal Total() => _cart.GetTotal(null);

		[HttpGet]
		[Route("TotalInStore")]
		public decimal TotalInStore() => _cart.GetTotalInStore(null);

		[HttpGet]
		[Route("TotalWithoutDiscount")]
		public decimal TotalWithoutDiscount() => _cart.GetTotalWithoutDiscount(null);

		[HttpGet]
		[Route("ProductsTotalInStore")]
		public decimal ProductsTotalInStore() => _cart.GetProductsTotalInStore(null);

		[HttpGet]
		[Route("ProductsTotal")]
		public decimal ProductsTotal() => _cart.GetProductsTotal(null);

		[HttpGet]
		[Route("CustomCateringTotal")]
		public decimal CustomCateringTotal() => _cart.GetLunchTotal(null);

		[HttpGet]
		[Route("CustomCateringTotalInStore")]
		public decimal CustomCateringTotalInStore() => _cart.GetLunchTotalInStore(null);

		[HttpGet]
		[Route("CateringsTotal")]
		public decimal CateringsTotal() => _cart.GetCateringsTotal(null);

		[HttpGet]
		[Route("CateringsTotalInStore")]
		public decimal CateringsTotalInStore() => _cart.GetCateringsTotalInStore(null);

		[HttpGet]
		[Route("CateringsTotalSavings")]
		public decimal CateringsTotalSavings() => _cart.GetCateringsTotalSavings(null);
	}
}
