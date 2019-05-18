using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.MvcControllers
{
    
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

        [HttpGet]
        public async Task<IActionResult> Index() => View((await _lunchRepository.GetAllLunchesAsync()).Where(x => _lunchRepository.GetTotal(x) > 0));


        [HttpGet]
        [Route("[controller]/ComboDetail/{id}")]
        public async Task<IActionResult> ComboDetail(int id) => View(await _lunchRepository.GetLunchByIdAsync(id));

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/CopyLunch/{id}")]
        public async Task<IActionResult> CopyLunch(int id)
        {
            await _lunchRepository.CopyLunchAsync(id);
			return Redirect($"{Request.HostUrl()}/new-catering");
        }

        [HttpGet]
        [Route("[controller]/EditCombo/{lunchId}")]
        public async Task<IActionResult> EditCombo(int lunchId)
        {
            var combo = await _lunchRepository.GetLunchByIdAsync(lunchId);
            var newCombo = _mapper.Map<Lunch, LunchComboViewModel>(combo);
            return View(newCombo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCombo(LunchComboViewModel lunchComboVM)
        {
            if (!ModelState.IsValid)
            {
                return View(lunchComboVM);
            }
            var lunch = await _lunchRepository.GetLunchByIdAsync(lunchComboVM.LunchId);
            lunch = _mapper.Map(lunchComboVM, lunch);

            _appDbContext.Lunch.Update(lunch);
            await _appDbContext.SaveChangesAsync();
            if(lunchComboVM.IsGeneric)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Special", new { id = lunch.LunchId });
        }

        [HttpGet]
        [Route("[controller]/ModifyLunch/{id}")]
        public async Task<IActionResult> ModifyLunch(int id)
        {
            await _lunchRepository.ModifyLunchAsync(id);
            return RedirectToAction("Estimate");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Estimate()
        {
            var result = _cartRepository.GetOrCreateSessionLunch().Lunch;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id) => View(await _lunchRepository.GetLunchByIdAsync(id));

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Special(int id) => View(await _lunchRepository.GetLunchByIdAsync(id));

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/Combos/")]
        public async Task<IActionResult> Combos() => View(await _lunchRepository.GetAllLunchesAsync());
    }
}
