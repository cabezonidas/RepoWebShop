using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class OrderCatalogItem
    {
        public int OrderCatalogItemId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; internal set; }
        public int OrderId { get; internal set; }
    }
}
