using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CatalogController : Controller
    {
        private readonly ICatalogRepository _catalogRepo;
        public CatalogController(ICatalogRepository catalogRepo)
        {
            _catalogRepo = catalogRepo;
        }

        public IActionResult Index()
        {
            return View(_catalogRepo.GetAll());
        }

        [HttpGet]
        public IActionResult New()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult New(Product product)
        {
            if(ModelState.IsValid)
            {
                _catalogRepo.Create(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _catalogRepo.GetById(id);
            if (result != null)
                return View(result);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _catalogRepo.Edit(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        [Route("[controller]/Inflation/{percentage}/{roundTo}")]
        public IActionResult Inflation(int percentage, int roundTo)
        {
            var vm = new InflationEstimateViewModel
            {
                Products = _catalogRepo.InflationEstimate(percentage, roundTo),
                Inflation = percentage,
                RoundTo = roundTo
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Inflation()
        {
            return Inflation(10, 5);
        }
    }
}
