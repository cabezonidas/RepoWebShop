using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.FeApi.Invoice;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.States;

namespace RepoWebShop.Models
{
    public class LazyOrderRepository : ILazyOrderRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public LazyOrderRepository(AppDbContext appDbContext, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _ctx = appDbContext;
			_userManager = userManager;
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
			var cartItems = await _ctx.OrderCaterings.Where(x => x.OrderId == id).Include(x => x.Lunch)
				.ToArrayAsync();
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

		public async Task<IEnumerable<_OrderPie>> GetOrderPiesAsync(int id)
		{
			var pies = await _ctx.OrderDetails.Where(x => x.OrderId == id)
				.Include(x => x.Pie)
				.Include(x => x.Pie.PieDetail)
				.ToArrayAsync();
			return pies.Select(x => new _OrderPie { Amount = x.Amount, Title = $"{x.Pie.PieDetail.Name} {x.Pie.Name}" });
		}

		public string CustomerName(int id)
		{
			var order = _ctx.Orders.Include(x => x.Registration).FirstOrDefault(x => x.OrderId == id);
			if (order == null)
				return string.Empty;
			if (string.IsNullOrEmpty(order.RegistrationId) || order.Registration == null)
				return order.MercadoPagoName;
			else
				return $"{order.Registration.FirstName} {order.Registration.LastName}".Trim();
		}

		public string CustomerEmails(int id)
		{
			var emails = new List<string>();
			var order = _ctx.Orders.Include(x => x.Registration).FirstOrDefault(x => x.OrderId == id);
			if (order == null)
				return string.Empty;
			emails.Add(order.MercadoPagoMail);
			emails.Add(order.Registration?.Email ?? "");
			return string.Join(" / ", emails.Distinct().Where(x => !string.IsNullOrEmpty(x)));
		}

		public string CustomerPhoneNumbers(int id)
		{
			var numbers = new List<string>();
			var order = _ctx.Orders.Include(x => x.Registration).FirstOrDefault(x => x.OrderId == id);
			if (order == null)
				return string.Empty;
			numbers.Add(order.PhoneNumber);
			numbers.Add(order.Registration?.PhoneNumber ?? "");
			numbers.Add(order.Registration?.PhoneNumberDeclared ?? "");
			return string.Join(" / ", numbers.Distinct().Where(x => !string.IsNullOrEmpty(x)));
		}

		public async Task<IEnumerable<_AmountTitle>> DetailsBreakDown(int id)
		{
			var allItems = new List<_AmountTitle>();
			allItems.AddRange((await GetOrderPiesAsync(id)).Select(x => new _AmountTitle { Amount = x.Amount, Title = x.Title }));

			var catItems = (await GetOrderItemsAsync(id)).Select(x => new _AmountTitle { Amount = x.Amount, Title = x.Item.DisplayName });
			allItems.AddRange(catItems);

			var orderCaterings = await GetOrderCateringsAsync(id);
			if (orderCaterings != null)
				foreach (var cat in orderCaterings)
				{
					var items = await _ctx.LunchItems.Include(x => x.Lunch).Include(x => x.Product).Include(x => x.Product.PieDetail)
						.Where(x => x.Lunch.LunchId == cat.Catering.LunchId).ToArrayAsync();
					var misce = await _ctx.LunchMiscellanea.Include(x => x.Lunch).Where(x => x.Lunch.LunchId == cat.Catering.LunchId).ToArrayAsync();
					allItems.AddRange(items.Select(x => new _AmountTitle { Amount = x.ItemCount * cat.Amount, Title = x.Product.DisplayName.Trim() }));
					allItems.AddRange(misce.Select(x => new _AmountTitle { Amount = x.Quantity * cat.Amount, Title = x.Description }));
				}

			var result = allItems.GroupBy(x => x.Title).Select(group => new _AmountTitle { Amount = group.Sum(x => x.Amount), Title = group.Key });
			return result.OrderByDescending(x => x.Title).OrderByDescending(x => x.Amount);
		}

		public async Task<_InvoiceData> InvoiceData(int id)
		{
			var invoice = _ctx.InvoiceData.FirstOrDefault(x => x.OrderId == id);
			if (invoice == null)
				return null;

			var result = _mapper.Map<_InvoiceData>(invoice);

			var caes = await _ctx.Caes.Include(x => x.InvoiceData).Where(x => x.InvoiceData.InvoiceDataId == result.InvoiceDataId).ToArrayAsync();
			var details = await _ctx.InvoiceDetails.Include(x => x.InvoiceData).Where(x => x.InvoiceData.InvoiceDataId == result.InvoiceDataId).ToArrayAsync();

			result.Caes = caes.Select(x => _mapper.Map<_Cae>(x));
			result.InvoiceDetails = details.Select(x => _mapper.Map<_InvoiceDetail>(x));

			return result;
		}
	}
}
