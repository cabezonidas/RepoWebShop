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
using RepoWebShop.Extensions;
using RepoWebShop.Filters;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    public class PieDetailController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly ICalendarRepository _calendar;
        private readonly IFlickrRepository _flickrRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PieDetailController(ICalendarRepository calendar, ICatalogRepository catalogRepository, IMapper mapper, IFlickrRepository flickrRepository, AppDbContext appDbContext, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _calendar = calendar;
            _catalogRepository = catalogRepository;
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _flickrRepository = flickrRepository;
        }

        [HttpGet]
        [Route("[Controller]/List")]
        public async Task<ViewResult> List()
        {
            var viewProducts = _pieDetailRepository.PieDetailsWithChildren.OrderBy(p => p.Name).Select(x => (_pieDetailRepository.MapDbPieDetailToPieDetailViewModel(x)));

            var products = await _catalogRepository
                .GetAll(x => x.IsActive && x.IsOnSale && x.Category.ToLower() != "lunch" && x.Category.ToLower() != "appetizer");

            // Dictionary<int, string> times = _pieDetailRepository.TimeEstimations(products);

            return View(new PieDetailsListViewModel
            {
                PieDetails = viewProducts,
                CurrentCategory = "Todos los productos",
                CatalogProducts = products.Select(x => _mapper.Map<Product, ProductEstimationViewModel>(x)).Select(x => { x.Estimation = "" /*times[x.PreparationTime]*/; return x; })
            });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var pieDetail = _pieDetailRepository.GetPieDetailById(id);
            if (pieDetail == null)
                return NotFound();

            var result = new PieDetailViewModel() { PieDetail = pieDetail, Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == pieDetail.PieDetailId) };
            result.PrimaryPicture = _flickrRepository.GetAlbumPictures(pieDetail.FlickrAlbumId).PrimaryPicture;
            result.AlbumPitures = _flickrRepository.GetAlbumPictures(pieDetail.FlickrAlbumId);
            result.RequestAbsoluteUrl = Request.AbsoluteUrl();

            return View(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PieDetailCreateViewModel pieDetail)
        {
            if (ModelState.IsValid)
            {
                var pieDetailId = await _pieDetailRepository.Add(_mapper.Map<PieDetailCreateViewModel, PieDetail>(pieDetail));
                return RedirectToAction("New", "Catalog", pieDetailId);
            }

            return View(pieDetail);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ViewResult Create()
        {
            var albumes = _flickrRepository.Albums.OrderBy(x => x.Title._Content).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title._Content });
            var pieDetailCreateViewModel = new PieDetailCreateViewModel()
            {
                Albumes = albumes
            };
            return View(pieDetailCreateViewModel);
        }
    }
}
