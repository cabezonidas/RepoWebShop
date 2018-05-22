using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDetailDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly IFlickrRepository _flickrRepository;

        public PieDetailDataController(IPieDetailRepository pieDetailRepository, IPieRepository pieRepository, IFlickrRepository flickrRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _pieRepository = pieRepository;
            _flickrRepository = flickrRepository;
        }

        [HttpGet]
        [Route("GetPieDetailFotos/{setId}")]
        public IActionResult GetPieDetailFotos(string setId)
        {
            try
            {
                var albumId = Int64.Parse(setId);
                return Ok(_flickrRepository.GetAlbumPictures(albumId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPieDetail/{name}")]
        public async Task<IActionResult> AddPieDetail(string name)
        {
            var result = new PieDetail { Name = name.ToTitleCase() };
            try
            {
                var pieDetail = await _pieDetailRepository.Add(result);
                return Ok(new { pieDetail } );
            }
            catch(Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPieOfTheWeek/{pieDetailId}")]
        public IActionResult AddPieOfTheWeek(int pieDetailId)
        {
            try
            {
                var pieDetail = _pieDetailRepository.GetPieDetailById(pieDetailId);
                pieDetail.IsPieOfTheWeek = true;
                _pieDetailRepository.Update(pieDetail);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("RemovePieOfTheWeek/{pieDetailId}")]
        public async Task<IActionResult> RemovePieOfTheWeek(int pieDetailId)
        {
            try
            {
                var pieDetail = _pieDetailRepository.GetPieDetailById(pieDetailId);
                pieDetail.IsPieOfTheWeek = false;
                await _pieDetailRepository.Update(pieDetail);
                return Ok();
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
        [Route("Ingredients/{id}")]
        public IActionResult Ingredients(int id)
        {
            var result = _pieDetailRepository.PieDetails.FirstOrDefault(x => x.PieDetailId == id)?.Ingredients ?? "";
            return Ok(result);
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

            dbPieDetails = _pieDetailRepository.PieDetailsWithChildren.OrderBy(p => p.PieDetailId);

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
                IsMobile = this.Request.IsMobile(),
                PrimaryPicture = _flickrRepository.GetAlbumPictures(dbPieDetail.FlickrAlbumId).PrimaryPicture,
                PieDetail = dbPieDetail,
                Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == dbPieDetail.PieDetailId)
            };
        }
    }
}
