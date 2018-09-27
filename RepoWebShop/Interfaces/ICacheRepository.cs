using RepoWebShop.FeModels;
using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface ICacheRepository
    {
		IEnumerable<_Product> GetProducts();
		void SetProducts(IEnumerable<_Product> Products);

		Calendar GetPublicCalendar();
		void SetPublicCalendar(Calendar Calendar);

		IEnumerable<Product> GetCatalogItems();
		void SetCatalogItems(IEnumerable<Product> products);

		IEnumerable<LunchMiscellaneous> GetMiscellanea();
		void SetMiscellanea(IEnumerable<LunchMiscellaneous> miscellanea);

		IEnumerable<LunchItem> GetCateringItems();
		void SetCateringItems(IEnumerable<LunchItem> items);
	}
}
