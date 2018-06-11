using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    public class BillingController : Controller
    {
        private readonly IElectronicBillingRepository _billing;

        public BillingController(IElectronicBillingRepository billing)
        {
            _billing = billing;
        }

        public async Task<IActionResult> Index()
        {
            await _billing.GetLoginTicket();


            return View();
        }
    }
}
