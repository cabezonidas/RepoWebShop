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

namespace RepoWebShop.Controllers
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

        public ViewResult Index()
        {
            var TotalReservations = _orderRepository.GetAll().Where(x => string.IsNullOrEmpty(x.MercadoPagoUsername)).Sum(x => x.OrderTotal);
            var TotalMercadoPago = _orderRepository.GetAll().Where(x => !string.IsNullOrEmpty(x.MercadoPagoUsername)).Sum(x => x.OrderTotal);
            var TotalIncome = TotalReservations + TotalMercadoPago;

            var ProductsMostAddedToTrolley = _appDbContext.ShoppingCartItems.Include(x => x.Pie.PieDetail).GroupBy(info => info.Pie.PieDetail)
                .Select(group => new
                {
                    PieDetail = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(x => x.Count).Select(x => new KeyValuePair<PieDetail, int>(x.PieDetail, x.Count));

            var ItemsMostAddedToTrolley = _appDbContext.ShoppingCartItems.GroupBy(info => info.Pie)
            .Select(group => new
            {
                Pie = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count).Select(x => new KeyValuePair<Pie, int>(x.Pie, x.Count));

            var ProductsMostPurchased = _appDbContext.OrderDetails.GroupBy(info => info.Pie.PieDetail)
                .Select(group => new
                {
                    PieDetail = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(x => x.Count).Select(x => new KeyValuePair<PieDetail, int>(x.PieDetail, x.Count));


            var ItemsMostPurchased = _appDbContext.OrderDetails.Include(x => x.Pie.PieDetail).GroupBy(info => info.Pie)
                .Select(group => new
                {
                    Pie = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(x => x.Count).Select(x => new KeyValuePair<Pie, int>(x.Pie, x.Count));


            var vm = new StatsIndexViewModel
            {
                TotalReservations = TotalReservations,
                TotalMercadoPago = TotalMercadoPago,
                TotalIncome = TotalIncome,
                ProductsMostAddedToTrolley = ProductsMostAddedToTrolley,
                ItemsMostAddedToTrolley = ItemsMostAddedToTrolley,
                ProductsMostPurchased = ProductsMostPurchased,
                ItemsMostPurchased = ItemsMostPurchased
            };

            return View(vm);
        }
    }
}
