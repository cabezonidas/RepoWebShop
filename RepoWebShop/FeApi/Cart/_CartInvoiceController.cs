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
	public class _CartInvoiceController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		private readonly IElectronicBillingRepository _billing;
		public _CartInvoiceController(IShoppingCartRepository shoppingCart, IElectronicBillingRepository billing)
		{
			_cart = shoppingCart;
			_billing = billing;
		}

		[HttpGet]
		[Route("IsCuitValid/{cuit}")]
		public async Task<bool> IsCuitValid(string cuit)
		{
			try
			{
				var cuitNumber = Convert.ToInt64(cuit.Replace("-", ""));
				var afipCuit = await _billing.ValidPersonaAsync(cuitNumber);
				return afipCuit.Valid;
			}
			catch
			{
				return false;
			}
		}

		[HttpPost]
		[Route("AddCuit/{cuit}")]
		public async Task<IEnumerable<string>> AddCuit(string cuit)
		{
			try
			{
				var cuitNumber = Convert.ToInt64(cuit.Replace("-", ""));
				var afipCuit = await _billing.ValidPersonaAsync(cuitNumber);
				if(afipCuit.Valid)
				{
					_cart.AddCuitToCart(null, cuitNumber);
					return afipCuit.CuitDetails.Select(x => $"{x.Property} {x.Value}");
				}
			}
			catch { }
			return null;
		}

		[HttpDelete]
		[Route("ClearCuit")]
		public IActionResult ClearCuit()
		{
			_cart.RemoveCuitFromCart(null);
			return Ok();
		}
	}
}
