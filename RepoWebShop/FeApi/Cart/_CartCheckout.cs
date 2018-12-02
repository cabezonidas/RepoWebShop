using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Authorize]
	[Route("api/[controller]")]
	public class _CartCheckoutController : Controller
	{
		private readonly IAccountRepository _account;
		private readonly IOrderRepository _order;
		private readonly IShoppingCartRepository _cart;

		public _CartCheckoutController(IOrderRepository order, IAccountRepository account, IShoppingCartRepository cart)
		{
			_cart = cart;
			_order = order;
			_account = account;
		}

		[HttpPost]
		[Route("ConfirmReservation")]
		public async Task<string> ConfirmReservation()
		{
			if (_cart.GetTotal(null) == 0)
				throw new Exception("Intento de reserva con carrito vacío");

			var user = await _account.Current();
			Order order = new Order
			{
				Status = "reservation",
				Registration = user
			};
			order = await _order.CreateOrderAsync(order);
			await _order.AfterOrderConfirmedAsync(order);
			return order.FriendlyBookingId;
		}
	}
}
