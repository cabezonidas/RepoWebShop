using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.ViewModels;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public ViewResult Index()
        {
            var result = new List<PieDetailViewModel>();

            foreach(var pieOfTheWeek in _pieRepository.PiesOfTheWeek.Take(3))
            {
                var p = new PieDetailViewModel()
                {
                    PieDetail = pieOfTheWeek,
                    Pies = _pieRepository.Pies.Where(x => x.PieDetail.PieDetailId == pieOfTheWeek.PieDetailId)
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
