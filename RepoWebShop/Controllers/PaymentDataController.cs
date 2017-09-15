﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PaymentDataController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public PaymentDataController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        [Route("Management")]
        public IActionResult Management()
        {
            var orders = _orderRepository.GetAll();

            return Ok(orders);
        }

        [HttpGet]
        [Route("SaveDraft/{bookingId}")]
        public IActionResult SaveDraft(string bookingId)
        {
            Order order = _orderRepository.GetDraftOrderByBookingId(bookingId);
            if (order != null)
            {
                return Ok();
            }

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            order = new Order();
            order.OrderTotal = _shoppingCart.ShoppingCartItems.Select(x => x.Amount * x.Pie.Price).Sum();
            order.CustomerComments = _shoppingCart.GetShoppingCartComments();
            order.PhoneNumber = "0";
            order.Status = "draft";
            order.BookingId = bookingId;
            _orderRepository.CreateOrder(order);
            return Ok();
        }

        [HttpGet]
        [Route("Process/{status}")]
        public IActionResult Process(string status)
        {
            var data = new { result = "" };
            switch (status)
            {
                case "approved":
                case "pending":
                case "in_process":
                    _shoppingCart.ClearCart();
                    return Ok(new { status = status });
                case "rejected":
                default:
                    break;
            }
            return BadRequest(new { status = status });
        }
    }
}
