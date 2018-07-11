using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RepoWebShop.Filters;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    [Authorize(Roles = "Administrator")]
    public class StatsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRepository;

        public StatsController(AppDbContext appDbContext, IOrderRepository orderRepository)
        {
            _appDbContext = appDbContext;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
    }
}
