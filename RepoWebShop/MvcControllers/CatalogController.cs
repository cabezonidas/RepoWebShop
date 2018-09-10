using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.MvcControllers
{
    [Authorize(Roles = "Administrator")]
    public class CatalogController : Controller
    {
        private readonly ICatalogRepository _catalogRepo;
        private readonly IPieDetailRepository _pieDetailRepo;
        private readonly IMapper _mapper;

        public CatalogController(IMapper mapper, ICatalogRepository catalogRepo, IPieDetailRepository pieDetailRepo)
        {
            _mapper = mapper;
            _pieDetailRepo = pieDetailRepo;
            _catalogRepo = catalogRepo;
        }

        [HttpGet]
        public IActionResult Index() => View(_catalogRepo.GetAll());

        [HttpGet]
        public IActionResult QuickEdit() => View(_catalogRepo.GetAll(x => x.IsActive));

        [HttpGet]
        public IActionResult OnlyPrices() => View(_catalogRepo.GetAll(x => x.IsActive));

        [HttpGet]
        [Route("[controller]/AddPieDetailChild/{pieDetailId}")]
        public IActionResult AddPieDetailChild(int pieDetailId)
        {
            var pieDetail = _pieDetailRepo.GetPieDetailById(pieDetailId);
            ProductViewModel vm = new ProductViewModel();
            var baseProduct = _pieDetailRepo.GetChildren(pieDetailId).FirstOrDefault();
            if(baseProduct != null)
            {
                vm.Category = baseProduct.Category;
                vm.IsActive = baseProduct.IsActive;
                vm.IsOnSale = baseProduct.IsOnSale;
                vm.MinOrderAmount = baseProduct.MinOrderAmount;
                vm.MultipleAmount = baseProduct.MultipleAmount;
                vm.PreparationTime = baseProduct.PreparationTime;
                vm.Price = baseProduct.Price;
                vm.PriceInStore = baseProduct.PriceInStore;
                vm.Temperature = baseProduct.Temperature;
            }
            vm.PieDetails = _pieDetailRepo.PieDetails;
            vm.PieDetail = pieDetail;
            vm.PieDetailId = pieDetail.PieDetailId;
            vm.Ingredients = pieDetail.Ingredients;
            return View("Product", vm);
        }

        [HttpGet]
        [Route("[controller]/Product")]
        public IActionResult Product()
        {
            ProductViewModel vm = new ProductViewModel();
            vm.PieDetails = _pieDetailRepo.PieDetails;
            return View(vm);
        }

        [HttpGet]
        [Route("[controller]/Product/{id}")]
        public async Task<IActionResult> Product(int id)
        {
            var result = await _catalogRepo.GetById(id);
            result.MultipleAmount = result.MultipleAmount == 0 ? result.MinOrderAmount : result.MultipleAmount;
            var vm = _mapper.Map<Product, ProductViewModel>(result);
            vm.PieDetails = _pieDetailRepo.PieDetails;
            vm.Ingredients = result.PieDetail?.Ingredients ?? result.Description;
            vm.IsAdding = false;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Product(ProductViewModel vm)
        {
            if(ModelState.IsValid)
            {
                _pieDetailRepo.UpdateIngredients(vm);
                var product = _catalogRepo.CreateOrUpdate(vm);
                return RedirectToAction("Product", "Catalog", new { id = product.ProductId });
            }
            vm.PieDetails = _pieDetailRepo.PieDetails;
            return View(vm);
        }

        [HttpGet]
        [Route("[controller]/Inflation/{percentage}/{roundTo}")]
        public async Task<IActionResult> Inflation(int percentage, int roundTo)
        {
            var vm = new InflationEstimateViewModel
            {
                Products = await _catalogRepo.InflationEstimate(percentage, roundTo),
                Inflation = percentage,
                RoundTo = roundTo
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Inflation()
        {
            return await Inflation(10, 5);
        }
    }
}
