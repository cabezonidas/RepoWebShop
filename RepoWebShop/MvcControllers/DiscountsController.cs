using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.MvcControllers
{
    [Authorize(Roles = "Administrator")]
    public class DiscountsController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IShoppingCartRepository _cartRepository;

        public DiscountsController(ICalendarRepository calendarRepository, IDiscountRepository discountRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _calendarRepository = calendarRepository;
            _discountRepository = discountRepository;
            _cartRepository = shoppingCartRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_discountRepository.GetActives());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new Discount { ValidFrom = _calendarRepository.LocalTime().Date };
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Discounts/Details/{code}")]
        public IActionResult Details(string code)
        {
            var result = _discountRepository.FindByCode(code);
            if (result != null)
                return View(result);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Discount discount)
        {
            discount.IsActive = true;
            discount.ValidFrom = discount.ValidFrom.Date;
            discount.Phrase = discount.Phrase.ToUpper();
            if (!ModelState.IsValid)
                return View(discount);
            try
            {
                var result = _discountRepository.Add(discount);
                return Redirect($"/Discounts/Details/{result.Phrase}");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.Message);
                return View(discount);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _discountRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [PageVisitAsync]
        [AllowAnonymous]
        public IActionResult ApplyDiscount() => View();
    }
}
