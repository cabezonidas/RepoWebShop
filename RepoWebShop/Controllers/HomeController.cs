using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.ViewModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;
using RepoWebShop.Filters;

namespace RepoWebShop.Controllers
{
    [PageVisitAsync]
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IFlickrRepository _flickrRepository;

        public HomeController(IPieRepository pieRepository, IFlickrRepository flickrRepository, IPieDetailRepository pieDetailRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _pieRepository = pieRepository;
            _flickrRepository = flickrRepository;
        }

        public ViewResult Index()
        {
            var viewProducts = _pieRepository.PiesOfTheWeek.Take(3).OrderBy(p => p.Name).Select(x => (_pieDetailRepository.MapDbPieDetailToPieDetailViewModel(x)));

            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = viewProducts,
                HostUrl = this.Request.HostUrl(),
                IsMobile = this.Request.IsMobile()
            };
            
            return View(homeViewModel);
        }
    }
}
