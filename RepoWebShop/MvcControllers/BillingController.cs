using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.MvcControllers
{
	public class BillingController : Controller
    {
        private readonly IElectronicBillingRepository _billing;

        public BillingController(IElectronicBillingRepository billing)
        {
            _billing = billing;
        }

        [HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Index()
        {
            IEnumerable<InvoiceData> result = await _billing.GetAll();
            return View(result);
        }

        [HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Detail(int id)
        {
            InvoiceData result = await _billing.GetById(id);
            return View(result);
		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public IActionResult SummaryByMonth()
		{
			IEnumerable<Cae> result = _billing.AllCaes();
			return View(result);
		}
		[HttpGet]
		[Route("[controller]/Period/{yearmonth}")]
		[Authorize(Roles = "Administrator")]
		public IActionResult Period(string yearmonth)
		{
			IEnumerable<Cae> result = _billing.AllCaes().Where(x => x.CbteFch.Substring(0, 6) == yearmonth);
			return View(result);
		}
	}
}
