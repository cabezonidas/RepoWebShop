using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Linq;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class AdminDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;

        public AdminDataController(AppDbContext appDbContext, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            return Ok(_pieDetailRepository.PieDetails);
        }

        [HttpGet]
        [Route("GetPrices")]
        public IActionResult GetPrices()
        {
            return Ok(_pieRepository.AllPies.OrderBy(x=>x.PieDetail.Name + x.Name));
        }

        [HttpPut]
        [Route("UpdatePrice/{pieId}/{price}")]
        public IActionResult UpdatePrice(int pieId, int price)
        {
            try
            {
                _pieRepository.UpdatePrice(pieId, price);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPies/{id}")]
        public IActionResult GetPies(int id)
        {
            return Ok(_pieRepository.AllPies.Where(x => x.PieDetail.PieDetailId == id));
        }

        [HttpGet]
        [Route("PieDetailHasChildren/{id}")]
        public IActionResult PieDetailHasChildren(int id)
        {
            return Ok(_pieRepository.AllPies.Where(x => x.PieDetail.PieDetailId == id).Count() > 0);
        }


        [HttpDelete]
        [Route("DeletePie/{pieId}")]
        public IActionResult DeletePie(int pieId)
        {
            try
            {
                _pieRepository.Delete(pieId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RestorePie/{pieId}")]
        public IActionResult RestorePie(int pieId)
        {
            try
            {
                _pieRepository.Restore(pieId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePieDetail/{pieDetailId}")]
        public IActionResult DeletePieDetail(int pieDetailId)
        {
            try
            {
                _pieDetailRepository.Delete(pieDetailId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RestorePieDetail/{pieDetailId}")]
        public IActionResult RestorePieDetail(int pieDetailId)
        {
            try
            {
                _pieDetailRepository.Restore(pieDetailId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
