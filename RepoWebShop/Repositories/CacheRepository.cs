using System;
using System.Linq;
using RepoWebShop.Interfaces;
using System.Collections.Generic;
using RepoWebShop.FeModels;
using RepoWebShop.ViewModels;
using RepoWebShop.Models;

namespace RepoWebShop.Repositories
{
    public class CacheRepository : ICacheRepository
	{
		private CalendarCache _calendarCache;
		private ProductsCache _productsCache;

		public IEnumerable<_Product> GetProducts() => _productsCache != null && !_productsCache.Expired ? _productsCache.Products : null;
		public void SetProducts(IEnumerable<_Product> Products) => _productsCache = new ProductsCache(Products);

		public Calendar GetPublicCalendar() => _calendarCache != null && !_calendarCache.Expired ? _calendarCache.Calendar : null;
		public void SetPublicCalendar(Calendar Calendar) => _calendarCache = new CalendarCache(Calendar);

		private class ProductsCache
		{
			public ProductsCache(IEnumerable<_Product> products)
			{
				Products = products;
			}

			public IEnumerable<_Product> Products { get; set; }
			public DateTime Saved { get; set; }
			public bool Expired
			{
				get => DateTime.Now.Subtract(Saved) > new TimeSpan(2, 0, 0);
			}
		}

		private class CalendarCache
		{
			public CalendarCache(Calendar calendar)
			{
				Calendar = calendar;
				Saved = DateTime.Now;
			}
			public Calendar Calendar { get; set; }
			public DateTime Saved { get; set; }
			public bool Expired
			{
				get => DateTime.Now.Subtract(Saved) > new TimeSpan(12, 0, 0);
			}
		}
	}
}
