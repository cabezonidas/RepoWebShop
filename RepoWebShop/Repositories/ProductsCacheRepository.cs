using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using RepoWebShop.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.FeModels;
using AutoMapper;

namespace RepoWebShop.Repositories
{
    public class ProductsCacheRepository : IProductsCacheRepository
	{
		private readonly AppDbContext _ctx;
		private readonly ICalendarRepository _calendar;
		private readonly IDistributedCache _serverCache;
		private readonly IMapper _mapper;
		private IEnumerable<Product> cachedProducts;
		private IEnumerable<_Product> cachedProductsGrouped;
		private DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = new TimeSpan(0, 10, 0)
		};

		public ProductsCacheRepository(AppDbContext ctx, IDistributedCache serverCache, ICalendarRepository calendar, IMapper mapper)
        {
			_ctx = ctx;
			_serverCache = serverCache;
			_calendar = calendar;
			_mapper = mapper;
		}

		public async Task<IEnumerable<Product>> AllProducts()
		{
			var cacheFlag = await _serverCache.GetStringAsync("productsCache");
			if(cachedProducts == null || string.IsNullOrEmpty(cacheFlag))
			{
				cachedProducts = await _ctx.Products.Include(x => x.PieDetail).ToArrayAsync();
				await _serverCache.SetStringAsync("productsCache", _calendar.LocalTime().ToString(), cacheOptions);
			}
			return cachedProducts;
		}

		public async Task<IEnumerable<_Product>> ProductsGroupedByParent()
		{
			var cacheFlag = await _serverCache.GetStringAsync("productsGroupedCache");
			if (cachedProductsGrouped == null || string.IsNullOrEmpty(cacheFlag))
			{
				cachedProductsGrouped = await GroupedByParent();
				await _serverCache.SetStringAsync("productsGroupedCache", _calendar.LocalTime().ToString(), cacheOptions);
			}

			return cachedProductsGrouped;
		}

		private async Task<IEnumerable<_Product>> GroupedByParent()
		{
			var allProds = await AllProducts();
			IEnumerable<_Item> _items = new List<_Item>().AsEnumerable();

			foreach (var item in allProds.Where(x => x.IsActive && x.IsOnSale).OrderBy(x => x.DisplayName))
			{
				var _item = _mapper.Map<Product, _Item>(item);
				_item.Estimation = _calendar.GetPickupEstimate(item.PreparationTime);
				_items = _items.Append(_item);
			}

			IEnumerable<_Product> _products = allProds.Select(x => x.PieDetail).Distinct()
				.Select(x => _mapper.Map<PieDetail, _Product>(x)).OrderBy(x => x.Name.TrimStart());

			var result = _products.Select(x =>
			{
				x.Items = _items.Where(item => item.PieDetailId == x.PieDetailId);
				return x;
			}).ToArray();

			return result;
		}
	}
}
