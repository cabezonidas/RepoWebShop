using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
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

        public IActionResult Index()
        {
            return View(_discountRepository.GetActives());
        }

        public IActionResult Create()
        {
            var vm = new Discount { ValidFrom = _calendarRepository.LocalTime().Date };
            return View(vm);
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
                _discountRepository.Add(discount);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.Message);
                return View(discount);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _discountRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult ApplyDiscount() => View();
    }
}
