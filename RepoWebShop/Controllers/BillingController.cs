using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
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
            IEnumerable<InvoiceData> result = await _billing.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            InvoiceData result = await _billing.GetById(id);
            return View(result);
        }
    }
}
