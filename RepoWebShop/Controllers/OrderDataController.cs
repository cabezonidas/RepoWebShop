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

        [Route("InvertPickedUpStatus/{orderId}")]
        [HttpPost]
        public IActionResult InvertPickedUpStatus(int orderId)
        {
            return Ok(_orderRepository.InvertPickedUpStatus(orderId));
        }

        [Route("FinishOrder/{orderId}")]
        [HttpPost]
        public IActionResult FinishOrder(int orderId)
        {
            try
            {
                _orderRepository.OrderFinished(orderId, true);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("OrderPickedUp/{orderId}")]
        [HttpPost]
        public IActionResult OrderPickedUp(int orderId)
        {
            try
            {
                _orderRepository.OrderPickedUp(orderId, true);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("PayOrder/{orderId}")]
        [HttpPost]
        public IActionResult PayOrder(int orderId)
        {
            _orderRepository.PayOrder(orderId);
            return Ok();
        }

        [Route("RefundOrder/{orderId}")]
        [HttpPost]
        public IActionResult RefundOrder(int orderId)
        {
            //this.Request.Body;
            var reason = "This is a test.";
            _orderRepository.RefundOrder(orderId, reason);
            return Ok();
        }
    }
}
