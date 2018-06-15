using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [PageVisitAsync]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(AppDbContext appDbContext, IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<ViewResult> Index()
        {
            var users = _appDbContext.Users.ToList();
            var orders = await _orderRepository.GetAllAsync();

            var ordersWithoutUsers = orders.Where(x => x.Registration == null);
            var ordersWithUsers = orders.Where(x => x.Registration != null);

            var usersThatOrdered = ordersWithUsers.Select(x => x.Registration).Distinct();

            List<ApplicationUser> nonAdminUsers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                var isNonAdmin = !await _userManager.IsInRoleAsync(user, "Administrator");
                if (isNonAdmin)
                    nonAdminUsers.Add(user);
            }

            var usersThatDidntOrder = nonAdminUsers.Where(x => usersThatOrdered.Count(y => y == x) == 0);

            var vm = new UsersIndexViewModel
            {
                OrdersWithoutUsers = ordersWithoutUsers,
                OrdersWithUsers = ordersWithUsers,
                UsersThatOrdered = usersThatOrdered,
                UsersThatDidntOrder = usersThatDidntOrder
            };
            
            return View(vm);
        }
    }
}
