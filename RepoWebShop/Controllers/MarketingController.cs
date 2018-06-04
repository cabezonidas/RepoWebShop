using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MarketingController : Controller
    {
        private readonly IMarketingRepository _marketingRepo;

        public MarketingController (IMarketingRepository marketingRepo)
        {
            _marketingRepo = marketingRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PromoEmail()
        {
            return View();
        }

        public IActionResult EmailTemplate()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Marketing/Unsubscribe/{email}")]
        public IActionResult Unsubscribe(string email)
        {
            _marketingRepo.Unsubscribe(email);
            return View("Unsubscribe", email);
        }
    }
}
