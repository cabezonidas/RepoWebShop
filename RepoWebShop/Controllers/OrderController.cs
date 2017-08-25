﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RepoWebShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, UserManager<IdentityUser> userManager)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Tu carrito no puede estar vacío, agrega algunos productos.");
            }

            if (ModelState.IsValid)
            {
                order.OrderTotal = _shoppingCart.ShoppingCartItems.Select(x => x.Amount * x.Pie.Price).Sum();
                order.Registration = GetCurrentUser();
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);

        }
        
        [Authorize]
        public IActionResult CheckoutComplete()
        {
            var firstName = GetCurrentUser().FirstName;
            ViewBag.CheckoutCompleteMessage = firstName + ", gracias por tu reserva. Falta poco para que disfrutes de nuestras delicias!";

            return View();
        }

        private Registration GetCurrentUser()
        {
            return (_userManager.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name) as Registration);
        }
    }
}
