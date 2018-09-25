using RepoWebShop.FeModels;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface ICacheRepository
    {
		DateTime? GetPickup(int hours);
		void SetPickup(int hours, DateTime pickupEstimate);
		IEnumerable<_Product> GetProducts();
		void SetProducts(IEnumerable<_Product> Products);
	}
}
