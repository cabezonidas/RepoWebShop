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
        public IActionResult Payments()
        {
            var result = _paymentsRepository.GetPayments().Select(async x =>
                new PaymentViewModel()
                {
                    PaymentNotice = x,
                    Order = await _orderRepository.GetOrderByBookingIdAsync(x.BookingId) ?? new Order()
                });
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
