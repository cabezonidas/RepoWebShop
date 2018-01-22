using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
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

        public PaymentController(IPaymentNoticeRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        [HttpGet]
        public IActionResult Payments()
        {
            return View(_paymentsRepository.GetPayments());
        }

        [HttpGet]
        public IActionResult Payment(int id)
        {
            return View(_paymentsRepository.GetPayment(id));
        }
    }
}
