using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ApiControllers
{
    [Route("api/[controller]")]
    public class DiscountsDataController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IPrinterRepository _printerRepository;
        private readonly IShoppingCartRepository _cartRepository;

        public DiscountsDataController(IPrinterRepository printerRepository, ICalendarRepository calendarRepository, IDiscountRepository discountRepository, IShoppingCartRepository shoppingCartRepository)
        {
			_printerRepository = printerRepository;
			_calendarRepository = calendarRepository;
            _discountRepository = discountRepository;
            _cartRepository = shoppingCartRepository;
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]
		[Route("QuickDiscount/{value}")]
		public async void QuickDiscount(decimal value)
		{
			var disc = _discountRepository.AddQuickDiscount(value);

			var view = "VoucherPrint";
			var result = await this.RenderViewAsync(view, disc, true);

			_printerRepository.AddToQueue(result);
		}

		[HttpPost]
		[Route("ApplyDiscount")]
		public Discount ApplyDiscount() => null;

		[HttpPost]
        [Route("ApplyDiscount/{code}")]
        public Discount ApplyDiscount(string code)
        {
            var discount = _discountRepository.FindByCode(code ?? string.Empty);
            var error = "";
            Discount.ApplyDiscount(_calendarRepository.LocalTime(), _cartRepository.GetTotalWithoutDiscount(null), discount, out error);

            if (!string.IsNullOrEmpty(error))
            {
				return null;
            }
            else
            {
                _cartRepository.AddDiscount(discount);
				return discount;
            }
        }
    }
}
