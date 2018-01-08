using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.ViewModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;

namespace RepoWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IFlickrRepository _flickrRepository;

        public HomeController(IPieRepository pieRepository, IFlickrRepository flickrRepository)
        {
            _pieRepository = pieRepository;
            _flickrRepository = flickrRepository;
        }

        public ViewResult Index()
        {
            var result = new List<PieDetailViewModel>();

            foreach(var pieOfTheWeek in _pieRepository.PiesOfTheWeek.Take(3))
            {
                var p = new PieDetailViewModel()
                {
                    IsMobile = this.Request.IsMobile(),
                    PrimaryPicture = _flickrRepository.GetAlbumPictures(pieOfTheWeek.FlickrAlbumId).PrimaryPicture,
                    PieDetail = pieOfTheWeek,
                    Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == pieOfTheWeek.PieDetailId)
                };
                result.Add(p);
            };
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = result
            };
            
            return View(homeViewModel);
        }
    }
}
