using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

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

            foreach(var pieOfTheWeek in _pieRepository.PiesOfTheWeek)
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
