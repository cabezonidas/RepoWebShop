using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    public class LunchController : Controller
    {
        private readonly ILunchRepository _lunchRepository;
        public LunchController(ILunchRepository lunchRepository)
        {
            _lunchRepository = lunchRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Estimate()
        {
            return View(_lunchRepository.GetLunch(null));
        }
    }
}
