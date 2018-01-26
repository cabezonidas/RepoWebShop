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

        [Route("FindOrderApproved/{bookingId}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult FindOrderApproved(string bookingId)
        {
            var order = _orderRepository.GetOrderByBookingId(bookingId);
            if (order != null && order.Status == "approved")
                return Ok();
            else
                return NotFound();
        }

        [Route("AddComments/{orderId}")]
        [HttpPost]
        public IActionResult AddComments(int orderId)
        {
            _orderRepository.UpdateManagementComments(orderId, string.Empty);
            return Ok();
        }

        [Route("AddComments/{orderId}/{comments}")]
        [HttpPost]
        public IActionResult AddComments(int orderId, string comments)
        {
            _orderRepository.UpdateManagementComments(orderId, comments);
            return Ok();
        }

        [Route("UpdatePickUpDate/{orderId}/{dd}/{mm}/{yyyy}/{hh}/{min}")]
        [HttpPost]
        public IActionResult UpdatePickUpDate(int orderId, int dd, int mm, int yyyy, int hh, int min)
        {
            try
            {
                DateTime date = new DateTime(yyyy, mm, dd, hh, mm, 0);
                _orderRepository.UpdatePickUpDate(orderId, date);
                return Ok();
            }catch
            {
                return BadRequest();
            }
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
