using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PaymentDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;

        public PaymentDataController(IPieDetailRepository pieDetailRepository, IPieRepository pieRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _pieRepository = pieRepository;
        }

        [HttpGet]
        [Route("Process/{status}/{orderId}")]
        public IActionResult Index(string status, int orderId)
        {
            return Ok();
        }
    }
}
