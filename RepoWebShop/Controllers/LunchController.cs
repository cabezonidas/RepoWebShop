using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LunchController : Controller
    {
        private readonly ILunchRepository _lunchRepository;
        public LunchController(ILunchRepository lunchRepository)
        {
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
