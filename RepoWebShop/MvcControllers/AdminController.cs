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
using RepoWebShop.Filters;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IGalleryRepository _galleryRepository;
        private readonly IShoppingCartRepository _cart;
        private readonly IFlickrRepository _flickrRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICalendarRepository _calendar;        

        public AdminController(ICalendarRepository calendar, IShoppingCartRepository cart, IGalleryRepository galleryRepository, IFlickrRepository flickrRepository, IMapper mapper, AppDbContext appDbContext, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _calendar = calendar;
            _cart = cart;
            _galleryRepository = galleryRepository;
            _flickrRepository = flickrRepository;
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
		public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            var result = _appDbContext.Contacts;
            return View(result);
        }

        [HttpGet]
        public IActionResult NewContact()
        {
            var result = new Contact();
            return View(result);
        }

        [HttpPost]
        public IActionResult NewContact(Contact contact)
        {
            if(ModelState.IsValid)
            {
                _appDbContext.Contacts.Add(contact);
                _appDbContext.SaveChanges();
                return RedirectToAction("Contacts");
            }
            return View(contact);
        }

        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var result = _appDbContext.Contacts.FirstOrDefault(x => x.ContactId == id);
            if (result == null)
                return NotFound();
            return View(result);
        }

        [HttpPost]
        public IActionResult EditContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Contacts.Update(contact);
                _appDbContext.SaveChanges();
                return RedirectToAction("Contacts");
            }
            return View(contact);
        }

        [HttpGet]
        public IActionResult Visits()
        {
            //var visits = _appDbContext.PageVisits.OrderByDescending(x => x.PageVisitId).Take(100);

            //var visitsByPath = visits.GroupBy(x => x.Path).Select(group => new KeyValuePair<string, int>(group.Key, group.Count())).OrderByDescending(x => x.Value).Take(100);

            //var visitsByIp = visits.GroupBy(x => x.Ip).Select(group => new KeyValuePair<string, int>(group.Key, group.Count())).OrderByDescending(x => x.Value).Take(100);

            VisitsViewModel model = new VisitsViewModel
            {
                Visits = new List<PageVisit>(),
                ByPath = new List<KeyValuePair<string, int>>(),
                ByIp = new List<KeyValuePair<string, int>>()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Notifications()
        {
            var result = _appDbContext.AdminNotifications;
            return View(result);
        }

        [HttpGet]
        public IActionResult Errors()
        {
            var exceptions = _appDbContext.Exceptions.OrderByDescending(x => x.SiteExceptionId);
            //var bookingRecords = _appDbContext.BookingRecords.Where(x => x.Created > _calendar.LocalTime().AddDays(-7)).Take(100);

            SessionDetailsViewModel sessionDetails = _cart.SessionsDetails();

            ErrorsViewModel result = new ErrorsViewModel()
            {
                Exceptions = exceptions,
                SessionDetails = sessionDetails,
                BookingRecords = new List<BookingRecord>()
            };

            return View(result);
        }

        [HttpGet]
        public IActionResult SessionsActivities(string bookingId)
        {
            SessionDetailsViewModel sessionDetails = _cart.SessionsDetails(bookingId);

            return View(sessionDetails);
        }

        [HttpGet]
        public IActionResult AllProducts()
        {
            return View(_pieDetailRepository.PieDetails);
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
				var albumesSelect = new List<SelectListItem>();
				albumesSelect.Add(new SelectListItem { Value = "0", Text = "Sin album" });
				albumesSelect.AddRange(_flickrRepository.Albums.OrderBy(x => x.Title._Content).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title._Content }));
                
                pieDetailCreateViewModel.Albumes = albumesSelect.AsEnumerable();
                pieDetailCreateViewModel.Children = _pieDetailRepository.GetCatalogChildren(pieDetailCreateViewModel.PieDetailId);
                return View(pieDetailCreateViewModel);
            }
        }


        [HttpGet]
        public IActionResult Galleries()
        {
            var result = new List<GalleryFlickrAlbumViewModel>();
            var savedAlbums = _galleryRepository.GetFlickrAlbums();

            foreach(var set in _flickrRepository.Albums)
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

                //return View(pieDetailCreateViewModel);
            }
            
            var albumes = _flickrRepository.Albums.OrderBy(x => x.Title._Content).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title._Content });
            pieDetailCreateViewModel.Albumes = albumes;
            pieDetailCreateViewModel.Children = _pieDetailRepository.GetChildren(pieDetailCreateViewModel.PieDetailId);

            return View(pieDetailCreateViewModel);
        }
    }
}
