using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IEnumerable<PieDetailViewModel> LoadMorePieDetails()
        {
            IEnumerable<PieDetail> dbPieDetails = null;

            dbPieDetails = _pieDetailRepository.PieDetails.OrderBy(p => p.PieDetailId).Take(10);

            List<PieDetailViewModel> pieDetails = new List<PieDetailViewModel>();

            foreach (var dbPiDetail in dbPieDetails)
            {
                pieDetails.Add(MapDbPieDetailToPieDetailViewModel(dbPiDetail));
            }
            return pieDetails;
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
