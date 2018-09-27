using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
		[Route("Totals")]
		public async Task<_Totals> Totals()
		{
			// var result1 = _cart.GetTotals(null);
			var result2 = await _cart.GetTotalsAsync(null);
			return result2;
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
		[Route("CateringsTotal")]
		public decimal CateringsTotal() => _cart.GetCateringsTotal(null);

		[HttpGet]
		[Route("CateringsTotalInStore")]
		public decimal CateringsTotalInStore() => _cart.GetCateringsTotalInStore(null);

		[HttpGet]
		[Route("CateringsTotalSavings")]
		public decimal CateringsTotalSavings() => _cart.GetCateringsTotalSavings(null);

		[HttpGet]
		[Route("CustomCateringTotal")]
		public decimal CustomCateringTotal() => _cart.GetCustomCateringTotals(null);

		[HttpGet]
		[Route("CustomCateringTotalInStore")]
		public decimal CustomCateringTotalInStore() => _cart.GetCustomCateringTotalsInStore(null);
	}
}
