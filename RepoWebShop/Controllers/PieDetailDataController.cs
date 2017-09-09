using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDetailDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;

        public PieDetailDataController(IPieDetailRepository pieDetailRepository, IPieRepository pieRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _pieRepository = pieRepository;
        }

        [HttpGet]
        [Route("LoadMorePieDetails")]
        public IEnumerable<PieDetailViewModel> LoadAnyPieDetails(string category)
        {
            return LoadMorePieDetails(string.Empty);
        }

        [HttpGet]
        [Route("LoadMorePieDetails/{category}")]
        public IEnumerable<PieDetailViewModel> LoadMorePieDetails(string category)
        {
            IEnumerable<PieDetail> dbPieDetails = null;


            if (string.IsNullOrEmpty(category))
            {
                dbPieDetails = _pieDetailRepository.PieDetails.OrderBy(p => p.PieDetailId);
            }
            else
            {
                dbPieDetails = _pieDetailRepository.PieDetails.OrderBy(p => p.PieDetailId).Where(p => p.Category.CategoryName == category);
            }

            List<PieDetailViewModel> pieDetails = new List<PieDetailViewModel>();

            foreach (var dbPiDetail in dbPieDetails)
            {
                pieDetails.Add(MapDbPieDetailToPieDetailViewModel(dbPiDetail));
            }

            return pieDetails;
        }

        [HttpGet]
        [Route("GetOverview/{Id}")]
        public ActionResult GetOverview(int Id)
        {
            var pieDetail = _pieDetailRepository.PieDetails.FirstOrDefault(x => x.PieDetailId == Id);
            if (pieDetail == null)
                return NotFound();
            else
            {
                var objectView = MapDbPieDetailToPieDetailViewModel(pieDetail);
                var result = PartialView("PieOverviewSummary", objectView);
                return result;
            }
        }

        private PieDetailViewModel MapDbPieDetailToPieDetailViewModel(PieDetail dbPieDetail)
        {
            return new PieDetailViewModel()
            {
                PieDetail = dbPieDetail,
                Pies = _pieRepository.Pies.Where(x => x.PieDetail.PieDetailId == dbPieDetail.PieDetailId)
            };
        }
    }
}
