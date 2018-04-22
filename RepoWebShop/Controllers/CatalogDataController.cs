using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
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
    [Route("api/[controller]")]
    public class CatalogDataController : Controller
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogDataController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        [Route("InflationEstimate/{percentage}")]
        public IActionResult InflationEstimate(int percentage)
        {
            var result = _catalogRepository.InflationEstimate(percentage, 5);
            return View(result);
        }

        [HttpPost]
        [Route("Inflation/{percentage}")]
        public IActionResult Inflation(int percentage)
        {
            _catalogRepository.ApplyPriceRise(percentage, 5);

            return Ok();
        }


        [HttpPost]
        public IActionResult Restore()
        {
            _catalogRepository.RestorePrices();

            return Ok();
        }
    }
}
