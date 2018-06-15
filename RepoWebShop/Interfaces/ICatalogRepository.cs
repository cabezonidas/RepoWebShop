using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Interfaces
{
    public interface ICatalogRepository
    {
        IEnumerable<Product> GetAll(Func<Product, bool> condition = null);
        IEnumerable<Product> GetAvailableToBuyOnline();
        Product GetById(int id);
        IEnumerable<Product> GetActive();
        void Deactivate(int productId);
        void Activate(int productId);
        void Create(Product product);
        void Edit(Product product);
        void RestorePrices();
        void ApplyPriceRise(decimal percentage, int roundTo);
        IEnumerable<ProductInflationEstimateViewModel> InflationEstimate(decimal percentage, int roundTo);
        Product CreateOrUpdate(ProductViewModel vm);
        IEnumerable<Product> GetByParent(int id);
        void QuickUpdate(int productId, decimal price, decimal priceInStore, bool onlineSale, string category, string temp, int minAmountOrder, int increments, int portions, int prepTime);
    }
}
