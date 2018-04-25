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

        [HttpPost]
        [Route("Inflation/{percentage}/{roundTo}")]
        public IActionResult AdjustToInflation(int percentage, int roundTo)
        {
            _catalogRepository.ApplyPriceRise(percentage, roundTo);
            return Ok();
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _catalogRepository.Deactivate(id);
            return Ok();
        }

        [HttpPost]
        [Route("Restore/{id}")]
        public IActionResult Restore(int id)
        {
            _catalogRepository.Activate(id);
            return Ok();
        }

        [HttpPost]
        [Route("RestorePrices")]
        public IActionResult RestorePrices()
        {
            _catalogRepository.RestorePrices();
            return Ok();
        }
    }
}
