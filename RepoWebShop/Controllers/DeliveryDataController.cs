using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class DeliveryDataController : Controller
    {
        private readonly IDeliveryRepository _deliveryRepository;
        public DeliveryDataController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }
        
        [HttpGet]
        [Route("GetDistance/{deliveryAddress}")]
        public async Task<IActionResult> GetDistance(string deliveryAddress)
        {
            return Ok(new { distance = await _deliveryRepository.GetDistanceAsync(deliveryAddress) });
        }
    }
}
