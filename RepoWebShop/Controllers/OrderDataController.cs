using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Interfaces;

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
        [HttpPost]
        public IActionResult AddComments(int orderId, string comments)
        {
            _orderRepository.UpdateManagementComments(orderId, comments);
            return Ok();
        }

        [Route("UpdatePickUpDate/{orderId}/{pickUp}")]
        [HttpPost]
        public IActionResult UpdatePickUpDate(int orderId, DateTime pickUp)
        {
            _orderRepository.UpdatePickUpDate(orderId, pickUp);
            return Ok();
        }

        [Route("PickUpOrder/{orderId}")]
        [HttpPost]
        public IActionResult PickUpOrder(int orderId)
        {
            _orderRepository.PickUpOrder(orderId);
            return Ok();
        }

        [Route("CompleteOrder/{orderId}")]
        [HttpPost]
        public IActionResult CompleteOrder(int orderId)
        {
            _orderRepository.CompleteOrder(orderId);
            return Ok();
        }

        [Route("PayOrder/{orderId}")]
        [HttpPost]
        public IActionResult PayOrder(int orderId)
        {
            _orderRepository.PayOrder(orderId);
            return Ok();
        }
    }
}
