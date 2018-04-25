using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class LunchDataController : Controller
    {
        private readonly ILunchRepository _lunchRepository;
        private readonly ICatalogRepository _catalogRepository;

        public LunchDataController(ILunchRepository lunchRepository, ICatalogRepository catalogRepository)
        {
            _lunchRepository = lunchRepository;
            _catalogRepository = catalogRepository;
        }

        [HttpPost]
        [Route("AddProductInstance/{productId}")]
        public IActionResult AddProductInstance(int productId)
        {
            var result = _lunchRepository.AddItemInstance(null, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [Route("AddProduct/{productId}")]
        public IActionResult AddProduct(int productId)
        {
            var result = _lunchRepository.AddItem(null, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            var result = _catalogRepository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLunchItem/{itemId}")]
        public IActionResult GetLunchItem(int itemId)
        {
            var result = _lunchRepository.GetLunch(null).Items.First(x => x.ShoppingCartLunchItemId == itemId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpGet]
        [Route("GetLunch")]
        public IActionResult GetLunch()
        {
            var result = _lunchRepository.GetLunch(null);
            var response = Json(result);
            return response;
        }

        [HttpDelete]
        [Route("RemoveProductInstance/{productId}")]
        public IActionResult RemoveProductInstance(int productId)
        {
            var result = _lunchRepository.RemoveItemInstance(null, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [Route("RemoveProduct/{productId}")]
        public IActionResult RemoveProduct(int productId)
        {
            var result = _lunchRepository.RemoveItem(null, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }
    }
}
