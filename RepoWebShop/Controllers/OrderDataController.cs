using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class OrderDataController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDataController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [Route("AddComments/{orderId}/{comments}")]
        public IActionResult AddComments(int orderId, string comments)
        {
            _orderRepository.UpdateManagementComments(orderId, comments);
            return Ok();
        }

        [Route("UpdatePickUpDate/{orderId}/{pickUp}")]
        public IActionResult UpdatePickUpDate(int orderId, DateTime pickUp)
        {
            _orderRepository.UpdatePickUpDate(orderId, pickUp);
            return Ok();
        }

        [Route("InvertPickedUpStatus/{orderId}")]
        public IActionResult InvertPickedUpStatus(int orderId)
        {
            return Ok(_orderRepository.InvertPickedUpStatus(orderId));
        }
    }
}
