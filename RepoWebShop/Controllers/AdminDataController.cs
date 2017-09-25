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
        [Route("GetPies/{id}")]
        public IActionResult GetPies(int id)
        {
            return Ok(_pieRepository.Pies.Where(x => x.PieDetail.PieDetailId == id));
        }

        [HttpGet]
        [Route("PieDetailHasChildren/{id}")]
        public IActionResult PieDetailHasChildren(int id)
        {
            return Ok(_pieRepository.Pies.Where(x => x.PieDetail.PieDetailId == id).Count() > 0);
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
    }
}
