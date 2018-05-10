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

        public LunchController(AppDbContext appDbContext, IMapper mapper, ILunchRepository lunchRepository)
        {
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
        [Route("[controller]/CopyLunch/{id}")]
        public IActionResult CopyLunch(int id)
        {
            _lunchRepository.CopyLunch(id);
            return RedirectToAction("Estimate");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/Combos/")]
        public IActionResult Combos() => View(_lunchRepository.GetAllLunches().Where(x => x.IsCombo));

        [HttpGet]
        [Route("[controller]/CreateCombo/{lunchId}")]
        public IActionResult CreateCombo(int lunchId) {
            var combo = _lunchRepository.GetAllLunches().FirstOrDefault(x => !x.IsCombo && x.LunchId == lunchId);
            var newCombo = _mapper.Map<Lunch, LunchComboViewModel>(combo);
            return View(newCombo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCombo(LunchComboViewModel lunchComboVM)
        {
            if (!ModelState.IsValid)
            {
                return View(lunchComboVM);
            }
            var lunch = _lunchRepository.GetAllLunches().First(x => !x.IsCombo && x.LunchId == lunchComboVM.LunchId);
            lunch = _mapper.Map(lunchComboVM, lunch);

            _appDbContext.Lunch.Update(lunch);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[controller]/EditCombo/{lunchId}")]
        public IActionResult EditCombo(int lunchId)
        {
            var combo = _lunchRepository.GetAllLunches().FirstOrDefault(x => x.IsCombo && x.LunchId == lunchId);
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
            var lunch = _lunchRepository.GetAllLunches().First(x => x.IsCombo && x.LunchId == lunchComboVM.LunchId);
            lunch = _mapper.Map(lunchComboVM, lunch);

            _appDbContext.Lunch.Update(lunch);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[controller]/ModifyLunch/{id}")]
        public IActionResult ModifyLunch(int id)
        {
            _lunchRepository.ModifyLunch(id);
            return RedirectToAction("Estimate");
        }

        public IActionResult Estimate()
        {
            var result = _lunchRepository.GetSessionLunch().Lunch;
            return View(result);
        }

        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            Lunch lunch = _lunchRepository.GetLunch(id);
            return View(lunch);
        }
    }
}
