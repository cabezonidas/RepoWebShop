using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.States;

namespace RepoWebShop.Models
{
    public class LazyOrderRepository : ILazyOrderRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public LazyOrderRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _ctx = appDbContext;
        }


		public async Task<IEnumerable<_Order>> AllAsync(Func<Order, bool> condition = null)
		{
			IEnumerable<_Order> orders = await _ctx.Orders
				.Where(x => (condition == null || condition(x)) && x.Status != "draft")
				.Select(x => _mapper.Map<Order, _Order>(x))
				.ToArrayAsync();

			return orders;
		}


		public async Task<IEnumerable<_Order>> GetOrdersInProgressAsync() =>
			await AllAsync(x => x.OrderProgressState.GetType() == typeof(OrderInProgress) || x.OrderProgressState.GetType() == typeof(OrderComplete));

		public async Task<IEnumerable<_Order>> GetOrdersCancelledAsync() => await AllAsync(x => x.OrderProgressState.GetType() == typeof(OrderCancelled));

		public async Task<IEnumerable<_Order>> GetOrdersCompletedAsync() => await AllAsync(x => x.OrderProgressState.GetType() == typeof(OrderComplete));

		public async Task<IEnumerable<_OrderCatering>> GetOrderCateringsAsync(int id)
		{
			var cartItems = await _ctx.OrderCaterings.Where(x => x.OrderId == id).Include(x => x.Lunch).ToArrayAsync();
			var result = cartItems.Select(x =>
			{
				var orderCatering = new _OrderCatering
				{
					Amount = x.Amount,
					Catering = _mapper.Map<_Catering>(x.Lunch)
				};
				return orderCatering;
			});
			return result;
		}

		public async Task<IEnumerable<_OrderItem>> GetOrderItemsAsync(int id)
		{
			var cartItems = await _ctx.OrderCatalogItems.Where(x => x.OrderId == id)
				.Include(x => x.Product)
				.Include(x => x.Product.PieDetail)
				.ToArrayAsync();
			var result = cartItems.Select(x =>
			{
				var orderCatering = new _OrderItem
				{
					Amount = x.Amount,
					Item = _mapper.Map<_Item>(x.Product)
				};
				return orderCatering;
			});
			return result;
		}
	}
}
