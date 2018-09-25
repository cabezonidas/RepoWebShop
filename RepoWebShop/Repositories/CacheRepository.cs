using System;
using System.Linq;
using RepoWebShop.Interfaces;
using System.Collections.Generic;
using RepoWebShop.FeModels;

namespace RepoWebShop.Repositories
{
    public class CacheRepository : ICacheRepository
	{
		private List<PickupCache> _pickupCache = new List<PickupCache>();
		private ProductsCache _productsCache;

		private TimeSpan Cache = new TimeSpan(0, 10, 0);

		public DateTime? GetPickup(int hours)
		{
			var result = _pickupCache.FirstOrDefault(x => x.Hours == hours);
			if(result != null)
			{
				if (DateTime.Now.Subtract(result.Saved) > Cache)
					_pickupCache.Remove(result);
				else
					return result.PickupEstimate;
			}
			return null;
		}

		public void SetPickup(int hours, DateTime pickupEstimate) =>
			_pickupCache.Add(new PickupCache { Hours = hours, PickupEstimate = pickupEstimate, Saved = DateTime.Now });

		public IEnumerable<_Product> GetProducts() =>
			_productsCache != null && DateTime.Now.Subtract(_productsCache.Saved) < Cache ? _productsCache.Products : null;

		public void SetProducts(IEnumerable<_Product> Products) =>
			_productsCache = new ProductsCache { Products = Products, Saved = DateTime.Now };

		private class PickupCache
		{
			public int Hours { get; set; }
			public DateTime Saved { get; set; }
			public DateTime PickupEstimate { get; set; }
		}

		private class ProductsCache
		{
			public IEnumerable<_Product> Products { get; set; }
			public DateTime Saved { get; set; }
		}
	}
}
