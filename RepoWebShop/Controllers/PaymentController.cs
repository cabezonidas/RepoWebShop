using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
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
            var result = _paymentsRepository.GetPayments().Select(x =>
                new PaymentViewModel()
                {
                    PaymentNotice = x,
                    Order = _orderRepository.GetOrderByBookingId(x.BookingId) ?? new Order()
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Payment(int id)
        {
            var pn = _paymentsRepository.GetPayment(id);
            var result = new PaymentViewModel()
            {
                PaymentNotice = pn,
                Order = _orderRepository.GetOrderByBookingId(pn.BookingId) ?? new Order()
            };
            return View(result);
        }
    }
}
