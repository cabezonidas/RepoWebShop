using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Controllers
{
    public class PieDetailController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PieDetailController(IMapper mapper, AppDbContext appDbContext, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public ViewResult List(string category)
        {
            IEnumerable<PieDetail> pieDetails;
            string currentCategory = string.Empty;

            var piesWithPrice = _pieRepository.ActivePies.Select(x => x.PieDetail.PieDetailId).Distinct();

            bool test = piesWithPrice.Contains(111);

            if (string.IsNullOrEmpty(category))
            {
                pieDetails = _pieDetailRepository.PieDetailsWithChildren.OrderBy(p => p.Name);
                currentCategory = "Ver todos";
            }
            else
            {
                pieDetails = _pieDetailRepository.PieDetailsWithChildren.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.Name);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }


            var PieDetailsVM = new List<PieDetailViewModel>();
            foreach (var pieDetail in pieDetails)
            {
                PieDetailsVM.Add(
                    new PieDetailViewModel()
                    {
                        PieDetail = pieDetail,
                        Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == pieDetail.PieDetailId)
                    }
                );
            }

            return View(new PieDetailsListViewModel
            {
                PieDetails = PieDetailsVM,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var pieDetail = _pieDetailRepository.GetPieDetailById(id);
            if (pieDetail == null)
                return NotFound();

            var result = new PieDetailViewModel() { PieDetail = pieDetail, Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == pieDetail.PieDetailId) };

            return View(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PieDetailCreateViewModel pieDetail)
        {
            if (ModelState.IsValid)
            {
                await _pieDetailRepository.Add(_mapper.Map<PieDetailCreateViewModel, PieDetail>(pieDetail));
                return RedirectToAction("AllProducts", "Admin");
            }

            return View(pieDetail);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ViewResult Create()
        {
            var categories = _appDbContext.Categories.Select(x => new SelectListItem() { Value = x.CategoryId.ToString(), Text = x.CategoryName });
            var pieDetailCreateViewModel = new PieDetailCreateViewModel()
            {
                Categories = categories.ToList()
            };
            return View(pieDetailCreateViewModel);
        }
    }
}
