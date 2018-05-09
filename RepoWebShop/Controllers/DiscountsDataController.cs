using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class DiscountsDataController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IShoppingCartRepository _cartRepository;

        public DiscountsDataController(ICalendarRepository calendarRepository, IDiscountRepository discountRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _calendarRepository = calendarRepository;
            _discountRepository = discountRepository;
            _cartRepository = shoppingCartRepository;
        }

        [HttpPost]
        [Route("ApplyDiscount")]
        public IActionResult ApplyDiscount() => ApplyDiscount(string.Empty);

        [HttpPost]
        [Route("ApplyDiscount/{code}")]
        public IActionResult ApplyDiscount(string code)
        {
            var discount = _discountRepository.FindByCode(code ?? string.Empty);
            var error = "";
            Discount.ApplyDiscount(_calendarRepository.LocalTime(), _cartRepository.GetItemsAndCatalogProductsTotal(null), discount, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            else
            {
                _cartRepository.AddDiscount(discount);
                return Ok();
            }
        }
    }
}
