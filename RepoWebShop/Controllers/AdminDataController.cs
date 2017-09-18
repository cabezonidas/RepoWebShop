using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
