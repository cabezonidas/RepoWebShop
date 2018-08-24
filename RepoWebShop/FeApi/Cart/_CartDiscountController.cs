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
		public Discount Get() => _cart.GetDiscount(null);

		[HttpGet]
		[Route("Exists/{code}")]
		public bool Exists(string code) => _discount.FindByCode(code ?? string.Empty) != null;

		[HttpGet]
		[Route("IsActive/{code}")]
		public bool IsActive(string code) => _discount.FindByCode(code ?? string.Empty)?.IsActive ?? true;

		[HttpGet]
		[Route("IsAvailable/{code}")]
		public bool IsAvailable(string code)
		{
			var discount = _discount.FindByCode(code ?? string.Empty);
			return discount != null ?
				(discount.InstancesLeft.HasValue && discount.InstancesLeft.Value > 0) ||
				(discount.Weekly):
			true;
		}

		[HttpGet]
		[Route("MinOrderReached/{code}")]
		public bool MinOrderReached(string code)
		{
			var total = _cart.GetTotalWithoutDiscount(null);
			var discount = _discount.FindByCode(code ?? string.Empty);
			return discount != null ? discount.Base < total : true;
		}

		[HttpGet]
		[Route("IsValidToday/{code}")]
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
		[Route("NotExpired/{code}")]
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
		[Route("NotPending/{code}")]
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
			if(_discount.IsValid(code))
			{
				_cart.AddDiscount(discount);
				return discount;
			}
			else
				return null;
		}

		[HttpGet]
		[Route("IsValid/{code}")]
		public bool IsValid(string code)
		{
			var discount = _discount.FindByCode(code);
			return _discount.IsValid(code);
		}

		[HttpDelete]
		[Route("Remove")]
		public void Remove(string code) =>	_cart.RemoveShoppingDiscount();
	}
}
