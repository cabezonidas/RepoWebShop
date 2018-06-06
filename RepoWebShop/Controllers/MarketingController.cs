using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.ViewModels;
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
        private readonly ISmsRepository _smsRepo;
        private readonly IHostingEnvironment _env;

        public MarketingController (IMarketingRepository marketingRepo, ISmsRepository smsRepo, IHostingEnvironment env)
        {
            _smsRepo = smsRepo;
            _marketingRepo = marketingRepo;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PromoEmail()
        {
            return View();
        }

        public IActionResult PromoSms()
        {
            return View(new PromoSmsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PromoSms(PromoSmsViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            else
            {
                var mobiles = _marketingRepo.GetAllMobiles();
                var resources = await _smsRepo.GetFormattedNumbers(mobiles);
                if(_env.IsProduction())
                    foreach (var phone in resources)
                        await _smsRepo.SendSms(phone, vm.Body);

                return RedirectToAction("Index", "Admin");
            }
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
