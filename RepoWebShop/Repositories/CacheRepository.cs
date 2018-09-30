using System;
using RepoWebShop.Interfaces;
using System.Collections.Generic;
using RepoWebShop.FeModels;
using RepoWebShop.Models;

namespace RepoWebShop.Repositories
{
    public class CacheRepository : ICacheRepository
	{
		private CalendarCache _calendarCache;
		private ProductsCache _productsCache;
		private ProductsCatalogCache _productsCatalogCache;
		private MiscellaneaCache _miscellaneaCache;
		private CateringItemsCache _cateringItemsCache;

		public IEnumerable<_Product> GetProducts()
		{
			return _productsCache != null && !_productsCache.Expired ? _productsCache.Products : null;
		}
		public void SetProducts(IEnumerable<_Product> Products) => _productsCache = new ProductsCache(Products);

		public Calendar GetPublicCalendar() => _calendarCache != null && !_calendarCache.Expired ? _calendarCache.Calendar : null;
		public void SetPublicCalendar(Calendar Calendar) => _calendarCache = new CalendarCache(Calendar);

		public IEnumerable<Product> GetCatalogItems() => _productsCatalogCache != null && !_productsCatalogCache.Expired? _productsCatalogCache.Products : null;
		public void SetCatalogItems(IEnumerable<Product> products) => _productsCatalogCache = new ProductsCatalogCache(products);

		public IEnumerable<LunchItem> GetCateringItems() => _cateringItemsCache != null && !_cateringItemsCache.Expired ? _cateringItemsCache.Items : null;
		public void SetCateringItems(IEnumerable<LunchItem> items) => _cateringItemsCache = new CateringItemsCache(items);

		public IEnumerable<LunchMiscellaneous> GetMiscellanea() => _miscellaneaCache != null && !_miscellaneaCache.Expired ? _miscellaneaCache.Miscellanea : null;
		public void SetMiscellanea(IEnumerable<LunchMiscellaneous> miscellanea) => _miscellaneaCache = new MiscellaneaCache(miscellanea);

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

		private class ProductsCatalogCache
		{
			public ProductsCatalogCache(IEnumerable<Product> products)
			{
				Products = products;
			}

			public IEnumerable<Product> Products { get; set; }
			public DateTime Saved { get; set; }
			public bool Expired
			{
				get => DateTime.Now.Subtract(Saved) > new TimeSpan(2, 0, 0);
			}
		}

		private class MiscellaneaCache
		{
			public MiscellaneaCache(IEnumerable<LunchMiscellaneous> miscellanea)
			{
				Miscellanea = miscellanea;
			}

			public IEnumerable<LunchMiscellaneous> Miscellanea { get; set; }
			public DateTime Saved { get; set; }
			public bool Expired
			{
				get => DateTime.Now.Subtract(Saved) > new TimeSpan(2, 0, 0);
			}
		}

		private class CateringItemsCache
		{
			public CateringItemsCache(IEnumerable<LunchItem> items)
			{
				Items = items;
			}

			public IEnumerable<LunchItem> Items { get; set; }
			public DateTime Saved { get; set; }
			public bool Expired
			{
				get => DateTime.Now.Subtract(Saved) > new TimeSpan(2, 0, 0);
			}
		}
	}
}
