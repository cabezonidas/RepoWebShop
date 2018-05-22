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

namespace RepoWebShop.Controllers
{
    [PageVisitAsync]
    public class PieDetailController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IFlickrRepository _flickrRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PieDetailController(ICatalogRepository catalogRepository, IMapper mapper, IFlickrRepository flickrRepository, AppDbContext appDbContext, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _catalogRepository = catalogRepository;
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _flickrRepository = flickrRepository;
        }

        public ViewResult List(string category)
        {

            IEnumerable<PieDetail> pieDetails;
            string currentCategory = string.Empty;

            var piesWithPrice = _pieRepository.ActivePies.Select(x => x.PieDetail.PieDetailId).Distinct();
            
            pieDetails = _pieDetailRepository.PieDetailsWithChildren.OrderBy(p => p.Name);
            currentCategory = "Ver todos";


            var viewProducts = pieDetails.Select(x => (MapDbPieDetailToPieDetailViewModel(x)));

            return View(new PieDetailsListViewModel
            {
                PieDetails = viewProducts,
                CurrentCategory = currentCategory,
                CatalogProducts = _catalogRepository.GetAvailableToBuyOnline().Where(x => x.Category.ToLower() != "lunch")
            });
        }

        private PieDetailViewModel MapDbPieDetailToPieDetailViewModel(PieDetail dbPieDetail)
        {
            return new PieDetailViewModel()
            {
                IsMobile = this.Request.IsMobile(),
                PrimaryPicture = _flickrRepository.GetAlbumPictures(dbPieDetail.FlickrAlbumId).PrimaryPicture,
                PieDetail = dbPieDetail,
                Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == dbPieDetail.PieDetailId)
            };
        }

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
                Albumes = albumes.ToList()
            };
            return View(pieDetailCreateViewModel);
        }
    }
}
