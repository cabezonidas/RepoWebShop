using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Linq;
using System.Net;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class LunchDataController : Controller
    {
        private readonly ILunchRepository _lunchRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public LunchDataController(IMapper mapper, ILunchRepository lunchRepository, ICatalogRepository catalogRepository)
        {
            _mapper = mapper;
            _lunchRepository = lunchRepository;
            _catalogRepository = catalogRepository;
        }

        [HttpPost]
        [Route("AddProductInstance/{productId}")]
        public IActionResult AddProductInstance(int productId)
        {
            var sessionLunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.AddItemInstance(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [Route("AddProduct/{productId}")]
        public IActionResult AddProduct(int productId)
        {
            var sessionLunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.AddItem(sessionLunch.Lunch.LunchId, productId);
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
            var sessionLunch = _lunchRepository.GetSessionLunch();
            var result = sessionLunch.Lunch.Items.First(x => x.LunchItemId == itemId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpGet]
        [Route("GetLunch")]
        public IActionResult GetLunch()
        {
            var result = _lunchRepository.GetSessionLunch();
            var response = Json(result.Lunch);
            return response;
        }

        [HttpDelete]
        [Route("RemoveProductInstance/{productId}")]
        public IActionResult RemoveProductInstance(int productId)
        {
            var sessionLunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.RemoveItemInstance(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [Route("RemoveProduct/{productId}")]
        public IActionResult RemoveProduct(int productId)
        {
            var sessionLunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.RemoveItem(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [Route("SaveLunch")]
        public IActionResult SaveLunch()
        {
            return Ok(_lunchRepository.SaveLunch());
        }

        [HttpPost]
        [Route("AddMiscellaneous/{description}/{price}")]
        public IActionResult AddMiscellaneous(string description, decimal price)
        {
            description = description.Replace("__", "/");
            var lunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.AddMiscellaneous(lunch.Lunch.LunchId, description, price);
            return Ok(result.LunchMiscellaneousId);
        }

        [HttpGet]
        [Route("GetMiscellaneous/{id}")]
        public IActionResult GetMiscellaneous(int id)
        {
            var lunch = _lunchRepository.GetSessionLunch();
            LunchMiscellaneous result = _lunchRepository.GetMiscellaneous(id);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [Route("AddMiscellaneousInstance/{miscellaneousId}")]
        public IActionResult AddMiscellaneousInstance(int miscellaneousId)
        {
            var lunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.AddMiscellaneousInstance(lunch.Lunch.LunchId, miscellaneousId);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [Route("RemoveMiscellaneousInstance/{miscellaneousId}")]
        public IActionResult RemoveMiscellaneousInstance(int miscellaneousId)
        {
            var lunch = _lunchRepository.GetSessionLunch();
            var result = _lunchRepository.RemoveMiscellaneousInstance(lunch.Lunch.LunchId, miscellaneousId);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [Route("RemoveMiscellaneous/{miscellaneousId}")]
        public IActionResult RemoveMiscellaneous(int miscellaneousId)
        {
            var lunch = _lunchRepository.GetSessionLunch();
            _lunchRepository.RemoveMiscellaneous(lunch.Lunch.LunchId, miscellaneousId);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, null);
        }

        [HttpGet]
        [Route("GetTotals")]
        public IActionResult GetTotals()
        {
            var lunch = _lunchRepository.GetSessionLunch();
            var route = "~/Views/Lunch/LunchTotals.cshtml";
            return PartialView(route, lunch.Lunch);
        }
    }
}
