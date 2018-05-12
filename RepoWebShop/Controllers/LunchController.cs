using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [PageVisitAsync]
    [Authorize(Roles = "Administrator")]
    public class LunchController : Controller
    {
        private readonly ILunchRepository _lunchRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _cartRepository;

        public LunchController(IShoppingCartRepository cartRepository, AppDbContext appDbContext, IMapper mapper, ILunchRepository lunchRepository)
        {
            _cartRepository = cartRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _lunchRepository = lunchRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Lunch> lunches =  _lunchRepository.GetAllLunches();
            return View(lunches);
        }


        [HttpGet]
        [Route("[controller]/ComboDetail/{id}")]
        public IActionResult ComboDetail(int id) => View(_lunchRepository.GetLunch(id));

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/CopyLunch/{id}")]
        public IActionResult CopyLunch(int id)
        {
            _lunchRepository.CopyLunch(id);
            return RedirectToAction("Estimate");
        }

        [HttpGet]
        [Route("[controller]/EditCombo/{lunchId}")]
        public IActionResult EditCombo(int lunchId)
        {
            var combo = _lunchRepository.GetAllLunches().FirstOrDefault(x => x.LunchId == lunchId);
            var newCombo = _mapper.Map<Lunch, LunchComboViewModel>(combo);
            return View(newCombo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCombo(LunchComboViewModel lunchComboVM)
        {
            if (!ModelState.IsValid)
            {
                return View(lunchComboVM);
            }
            var lunch = _lunchRepository.GetAllLunches().First(x => x.LunchId == lunchComboVM.LunchId);
            lunch = _mapper.Map(lunchComboVM, lunch);

            _appDbContext.Lunch.Update(lunch);
            _appDbContext.SaveChanges();
            if(lunchComboVM.IsGeneric)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Special", new { id = lunch.LunchId });
        }

        [HttpGet]
        [Route("[controller]/ModifyLunch/{id}")]
        public IActionResult ModifyLunch(int id)
        {
            _lunchRepository.ModifyLunch(id);
            return RedirectToAction("Estimate");
        }

        [AllowAnonymous]
        public IActionResult Estimate()
        {
            var result = _cartRepository.GetOrCreateSessionLunch().Lunch;
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            Lunch lunch = _lunchRepository.GetLunch(id);
            return View(lunch);
        }

        [AllowAnonymous]
        public IActionResult Special(int id)
        {
            Lunch lunch = _lunchRepository.GetLunch(id);
            return View(lunch);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/Combos/")]
        public IActionResult Combos() => View(_lunchRepository.GetAllLunches());
    }
}
