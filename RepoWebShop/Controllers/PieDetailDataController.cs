using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDetailDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly IPhotosetAlbums _photosetAlbums;

        public PieDetailDataController(IPieDetailRepository pieDetailRepository, IPieRepository pieRepository, IPhotosetAlbums photosetAlbums)
        {
            _pieDetailRepository = pieDetailRepository;
            _pieRepository = pieRepository;
            _photosetAlbums = photosetAlbums;
        }

        [HttpGet]
        [Route("GetPieDetailFotos/{setId}")]
        public IActionResult GetPieDetailFotos(string setId)
        {
            try
            {
                var photos = _photosetAlbums.GetPhotos(Int64.Parse(setId));               
                return Ok(photos.Photoset.Photo.Select(x => $"https://farm{x.Farm}.staticflickr.com/{x.Server}/{x.Id}_{x.Secret}_"));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("LoadMorePieDetails")]
        public IEnumerable<PieDetailViewModel> LoadAnyPieDetails(string category)
        {
            return LoadMorePieDetails(string.Empty);
        }

        [HttpGet]
        [Route("GetPieById/{pieDetailId}")]
        public PieDetail GetPieById(int pieDetailId)
        {
            return _pieDetailRepository.GetPieDetailById(pieDetailId);
        }

        [HttpGet]
        [Route("LoadMorePieDetails/{category}")]
        public IEnumerable<PieDetailViewModel> LoadMorePieDetails(string category)
        {
            IEnumerable<PieDetail> dbPieDetails = null;


            if (string.IsNullOrEmpty(category))
            {
                dbPieDetails = _pieDetailRepository.PieDetailsWithChildren.OrderBy(p => p.PieDetailId);
            }
            else
            {
                dbPieDetails = _pieDetailRepository.PieDetailsWithChildren.OrderBy(p => p.PieDetailId).Where(p => p.Category.CategoryName == category);
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
                Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == dbPieDetail.PieDetailId)
            };
        }
    }
}
