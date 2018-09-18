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

namespace RepoWebShop.ApiControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class CatalogDataController : Controller
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IPieDetailRepository _pieDetailRepository;

        public CatalogDataController(ICatalogRepository catalogRepository, IPieDetailRepository pieDetailRepository)
        {
            _pieDetailRepository = pieDetailRepository;
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

        [HttpGet]
        [Route("GetByParent/{id}")]
        public IActionResult GetByParent(int id)
        {
            IEnumerable<Product> products = _catalogRepository.GetByParentForCatalog(id);
            return Ok(new { products });
        }

        [HttpPost]
        [Route("RestorePrices")]
        public IActionResult RestorePrices()
        {
            _catalogRepository.RestorePrices();
            return Ok();
        }
        
        [HttpPost]
        [Route("QuickUpdate/{productId}/{price}/{priceInStore}/{onSale}/{category}/{temp}/{minAmountOrder}/{increments}/{portions}/{prepTime}")]
        public IActionResult QuickUpdate(int productId, decimal price, decimal priceInStore, int onSale, string category, string temp, int minAmountOrder, int increments, int portions, int prepTime)
        {
            try
            {
                _catalogRepository.QuickUpdate(productId, price, priceInStore, onSale == 0 ? false : true, category, temp, minAmountOrder, increments, portions, prepTime);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
