using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public abstract class OrderCateringBase
    {
        public int Amount { get; set; }
        public int LunchId { get; set; }
        public Lunch Lunch { get; set; }
        public string BookingId { get; set; }
        public DateTime Created { get; set; }
        [BindNever]
        public decimal Price
        {
            get
            {
                if (Lunch.ComboPrice > 0)
                    return Lunch.ComboPrice;
                if (Lunch == null || (Lunch.Items == null && Lunch.Miscellanea == null))
                    return 0;
                var itemsPrice = Lunch.Items.Sum(x => x.Product.MinOrderAmount * x.Quantity * x.Product.Price);
                var miscellaneaPrice = Lunch.Miscellanea.Sum(x => x.Price * x.Quantity);
                return itemsPrice + miscellaneaPrice;
            }
        }
    }
}
