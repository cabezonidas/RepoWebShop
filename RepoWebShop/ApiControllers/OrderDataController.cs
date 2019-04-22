using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;
using System.Threading.Tasks;
using System.Net.Http;
using RepoWebShop.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.ApiControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class OrderDataController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPrinterRepository _printer;

        public OrderDataController(IOrderRepository orderRepository, IPrinterRepository printer)
        {
            _printer = printer;
            _orderRepository = orderRepository;
        }

        [Route("FindOrderApproved/{bookingId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FindOrderApproved(string bookingId)
        {
            var order = await _orderRepository.GetOrderByFriendlyBookingId(bookingId);
            if (order != null && order.Status == "approved")
                return Ok(new { found = true });
            else
            {
                return Ok(new { found = false });
            }
        }


		[HttpGet]
		[Authorize(Roles = "Administrator")]
		[Route("InProgress")]
		public async Task<IEnumerable<Order>> InProgress()
		{
			var result = await _orderRepository.GetOrdersInProgressAsync();

			return result;

		}


		//[Route("InvoiceOrder/{id}")]
		//[HttpPost]
		//public async Task<IActionResult> InvoiceOrder(int id)
		//{
		//    Order order = await _orderRepository.GetOrderByIdAsync(id);

		//    var result = await _orderRepository.FECAESolicitarAsync(order);

		//    return Ok();
		//}

		[Route("PrintOnlineOrder/{id}")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PrintOnlineOrder(int id)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id);
            var view = "OrderPrint";
            var result = await this.RenderViewAsync(view, order, true);

            _printer.AddToQueue(result);
            return Ok();
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
                DateTime date = new DateTime(yyyy, mm, dd, hh, min, 0);
                _orderRepository.UpdatePickUpDate(orderId, date);
                return Ok();
            }catch
            {
                return BadRequest();
            }
        }

        [Route("PickUpOrder/{orderId}")]
        [HttpPost]
        public async Task<IActionResult> PickUpOrder(int orderId)
        {
            await _orderRepository.PickUpOrderAsync(orderId);
            return Ok();
        }

        [Route("CompleteOrder/{orderId}")]
        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            await _orderRepository.CompleteOrderAsync(orderId);
            return Ok();
        }

        [Route("PayOrder/{orderId}")]
        [HttpPost]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            await _orderRepository.PayOrderAsync(orderId);
            return Ok();
        }
    }
}
