using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Controllers
{
    public class PieDetailController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieDetailController(IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
        }

        public ViewResult List(string category)
        {
            IEnumerable<PieDetail> pieDetails;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pieDetails = _pieDetailRepository.PieDetails.OrderBy(p => p.Name);
                currentCategory = "Ver todos";
            }
            else
            {
                pieDetails = _pieDetailRepository.PieDetails.Where(p => p.Category.CategoryName == category)
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
                        Pies = _pieRepository.Pies.Where(x => x.PieDetail.PieDetailId == pieDetail.PieDetailId)
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

            var result = new PieDetailViewModel() { PieDetail = pieDetail, Pies = _pieRepository.Pies.Where(x => x.PieDetail.PieDetailId == pieDetail.PieDetailId) };

            return View(result);
        }
    }
}
