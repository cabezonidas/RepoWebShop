using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartDiscount
    {
        public int ShoppingCartDiscountId { get; set; }
        public string ShoppingCartId { get; set; }
        public Discount Discount { get; set; }
    }
}
