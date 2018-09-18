using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepoWebShop.FeModels;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Interfaces
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Product>> GetAll(Func<Product, bool> condition = null);
        Task<IEnumerable<Product>> GetAvailableToBuyOnline();
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetActive();
        Task Deactivate(int productId);
        Task Activate(int productId);
        void Create(Product product);
        void Edit(Product product);
        void RestorePrices();
        void ApplyPriceRise(decimal percentage, int roundTo);
        Task<IEnumerable<ProductInflationEstimateViewModel>> InflationEstimate(decimal percentage, int roundTo);
		Task<IEnumerable<_Product>> ProductsGroupedByParent();
		Product CreateOrUpdate(ProductViewModel vm);
        IEnumerable<Product> GetByParent(int id);
        IEnumerable<Product> GetByParentForCatalog(int id);
        void QuickUpdate(int productId, decimal price, decimal priceInStore, bool onlineSale, string category, string temp, int minAmountOrder, int increments, int portions, int prepTime);
    }
}
