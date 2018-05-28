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

namespace RepoWebShop.Controllers
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

        public IActionResult Index()
        {
            return View(_catalogRepo.GetAll());
        }

        public IActionResult OnlyPrices()
        {
            return View(_catalogRepo.GetAll().Where(x => x.IsActive));
        }

        [HttpGet]
        [Route("[controller]/AddPieDetailChild/{pieDetailId}")]
        public IActionResult AddPieDetailChild(int pieDetailId)
        {
            var pieDetail = _pieDetailRepo.GetPieDetailById(pieDetailId);
            ProductViewModel vm = new ProductViewModel();
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
        public IActionResult Product(int id)
        {
            var result = _catalogRepo.GetById(id);
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
                return RedirectToAction("Product", "Catalog", vm.ProductId);
            }
            vm.PieDetails = _pieDetailRepo.PieDetails;
            return View(vm);
        }

        [HttpGet]
        [Route("[controller]/New")]
        public IActionResult New()
        {
            return View(new Product());
        }

        [HttpGet]
        [Route("[controller]/New/{pieDetailId}")]
        public IActionResult New(int pieDetailId)
        {
            var result = new Product { PieDetailId = pieDetailId };
            result.PieDetail = _pieDetailRepo.PieDetails.First(x => x.PieDetailId == pieDetailId);
            var vm = _mapper.Map<Product, ProductViewModel>(result);
            vm.PieDetails = _pieDetailRepo.PieDetails;
            vm.Ingredients = result.PieDetail?.Ingredients ?? result.Description;
            vm.IsAdding = true;
            return View("Product", vm);
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
