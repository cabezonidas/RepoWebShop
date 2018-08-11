using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _CartDiscountController : Controller
	{
		private readonly IShoppingCartRepository _cart;
		private readonly IDiscountRepository _discount;
		private readonly ICalendarRepository _calendar;
		public _CartDiscountController(IShoppingCartRepository shoppingCart, IDiscountRepository discount, ICalendarRepository calendar)
		{
			_cart = shoppingCart;
			_discount = discount;
			_calendar = calendar;
		}

		[HttpGet]
		[Route("Get")]
		public Discount Get(string code) => _discount.FindByCode(code);

		[HttpGet]
		[Route("Exists")]
		public bool Exists(string code) => _discount.FindByCode(code ?? string.Empty) != null;

		[HttpGet]
		[Route("IsActive")]
		public bool IsActive(string code) => _discount.FindByCode(code ?? string.Empty)?.IsActive ?? true;

		[HttpGet]
		[Route("IsAvailable")]
		public bool IsAvailable(string code)
		{
			var discount = _discount.FindByCode(code ?? string.Empty);
			return discount != null ?
				discount.InstancesLeft.HasValue && discount.InstancesLeft.Value <= 0 : true;
		}

		[HttpGet]
		[Route("MinOrderReached")]
		public bool MinOrderReached(string code)
		{
			var total = _cart.GetTotalWithoutDiscount(null);
			var discount = _discount.FindByCode(code ?? string.Empty);
			return discount != null ? total < discount.Base : true;
		}

		[HttpGet]
		[Route("IsValidToday")]
		public bool IsValidToday(string code)
		{
			var discount = _discount.FindByCode(code ?? string.Empty);
			if (discount == null)
				return true;

			var today = _calendar.LocalTime();
			if (today.WithinRange(discount.ValidFrom, discount.DurationDays))
				return true;
			else
			{
				if (discount.Weekly)
				{
					DateTime loopDate = discount.ValidFrom;
					for (; loopDate.AddDays(discount.DurationDays) <= today; loopDate = loopDate.AddDays(7)) ;
					if (today.WithinRange(loopDate, discount.DurationDays))
						return true;
				}
			}
			return false;
		}

		[HttpGet]
		[Route("NotExpired")]
		public bool NotExpired(string code)
		{
			var discount = _discount.FindByCode(code ?? string.Empty);
			if (discount == null)
				return true;

			var today = _calendar.LocalTime();
			if (today.WithinRange(discount.ValidFrom, discount.DurationDays))
				return true;
			else
				return (!discount.Weekly && today > discount.ValidFrom) ? false : true;
		}

		[HttpGet]
		[Route("NotPending")]
		public bool NotPending(string code)
		{
			var discount = _discount.FindByCode(code ?? string.Empty);
			if (discount == null)
				return true;

			var today = _calendar.LocalTime();
			if (today.WithinRange(discount.ValidFrom, discount.DurationDays))
				return true;
			else
				return (!discount.Weekly && today < discount.ValidFrom) ? false : true;
		}

		[HttpPost]
		[Route("Apply/{code}")]
		public Discount Apply(string code)
		{
			var discount = _discount.FindByCode(code);
			_cart.AddDiscount(discount);
			return discount;
		}
	}
}
