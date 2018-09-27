using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartCatalogItem
    {
        public int ShoppingCartCatalogItemId { get; set; }
        public Product Product { get; set; }
        public string ShoppingCartId { get; set; }
        public DateTime Created { get; set; }
        public int Amount { get; internal set; }
		public int ProductId { get; set; }
	}
}
