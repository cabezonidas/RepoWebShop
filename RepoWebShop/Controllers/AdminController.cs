using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPhotosGalleryRepository _galleryRepository;
        private readonly IPhotosetAlbums _photosetAlbums;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly List<SelectListItem> _categories;

        

        public AdminController(IPhotosGalleryRepository galleryRepository, IPhotosetAlbums photosetAlbums, IMapper mapper, AppDbContext appDbContext, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _galleryRepository = galleryRepository;
            _photosetAlbums = photosetAlbums;
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _categories = _appDbContext.Categories.Select(x => new SelectListItem() { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Prices()
        {
            return View();
        }
        public IActionResult AllProducts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditPieDetail(int id)
        {
            var pieDetail = _pieDetailRepository.PieDetails.First(x => x.PieDetailId == id);

            if (pieDetail == null)
                return NotFound();
            else
            {
                var pieDetailCreateViewModel = _mapper.Map<PieDetail, PieDetailCreateViewModel>(pieDetail);
                pieDetailCreateViewModel.Categories = _categories;
                return View(pieDetailCreateViewModel);
            }
        }

        [HttpGet]
        public IActionResult Galleries()
        {
            var result = new List<GalleryFlickrAlbumViewModel>();
            var savedAlbums = _galleryRepository.GetFlickrAlbums();

            foreach(var set in _photosetAlbums.Albums.Photosets.Photoset)
            {
                GalleryFlickrAlbumViewModel album = new GalleryFlickrAlbumViewModel();
                album.FlickrSetId = set.Id;
                album.FlickrSetTitle = set.Title._Content;
                album.InFlickr = true;
                var savedAlbum = savedAlbums.FirstOrDefault(x => x.FlickrSetId == set.Id);
                if(savedAlbum == null)
                    album.InGallery = false;
                else
                {
                    album.InGallery = savedAlbum.InGallery;
                    album.GalleryFlickrAlbumId = savedAlbum.GalleryFlickrAlbumId;
                }
                result.Add(album);
            }

            foreach(var orphanAlbum in savedAlbums.Where(x => !result.Select(y => y.FlickrSetId).Contains(x.FlickrSetId)))
            {
                GalleryFlickrAlbumViewModel album = new GalleryFlickrAlbumViewModel();
                album.GalleryFlickrAlbumId = orphanAlbum.GalleryFlickrAlbumId;
                album.FlickrSetId = orphanAlbum.FlickrSetId;
                album.InFlickr = false;
                album.InGallery = orphanAlbum.InGallery;
                result.Add(album);
            }
            
            return View(result.OrderBy(x => x.FlickrSetTitle));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPieDetail(PieDetailCreateViewModel pieDetailCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                
                await _pieDetailRepository.Update(_mapper.Map<PieDetailCreateViewModel, PieDetail>(pieDetailCreateViewModel));

                return RedirectToAction("AllProducts");
            }

            pieDetailCreateViewModel.Categories = _categories;

            return View(pieDetailCreateViewModel);
        }

        [HttpGet]
        [Route("[Controller]/EditPieDetail/AddPie/{id}")]
        public IActionResult AddPieToPieDetail(int id)
        {
            var pieDetail = _pieDetailRepository.PieDetails.First(x => x.PieDetailId == id);
            Pie model = new Pie()
            {
                PieDetail = pieDetail,
                PieDetailId = pieDetail.PieDetailId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[Controller]/EditPieDetail/AddPie/{id}")]
        public IActionResult AddPieToPieDetail(Pie pie)
        {
            if (ModelState.IsValid)
            {
                var pieId = _pieRepository.Add(pie);

                return RedirectToAction("EditPieDetail/" + pieId.PieDetailId);
            }
            return View(pie);
        }
    }
}
