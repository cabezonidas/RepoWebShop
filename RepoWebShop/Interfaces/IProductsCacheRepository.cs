using RepoWebShop.FeModels;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IProductsCacheRepository
	{
		Task<IEnumerable<Product>> AllProducts();
		Task<IEnumerable<_Product>> ProductsGroupedByParent();
	}
}
