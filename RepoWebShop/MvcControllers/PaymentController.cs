using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    [Authorize(Roles = "Administrator")]
    public class PaymentController : Controller
    {

        private readonly IPaymentNoticeRepository _paymentsRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentController(IPaymentNoticeRepository paymentsRepository, IOrderRepository orderRepository)
        {
            _paymentsRepository = paymentsRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Payments()
        {
			var orders = await _orderRepository.GetAllAsync();
			var result = _paymentsRepository.GetPayments().Select(x =>
			{
				var item = new PaymentViewModel()
				{
					PaymentNotice = x,
					Order = orders.FirstOrDefault(y => x.BookingId == y.BookingId)
				};
				return item;
			}).ToArray();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Payment(int id)
        {
            var pn = _paymentsRepository.GetPayment(id);
            var result = new PaymentViewModel()
            {
                PaymentNotice = pn,
                Order = await _orderRepository.GetOrderByBookingIdAsync(pn.BookingId) ?? new Order()
            };
            return View(result);
        }
    }
}
