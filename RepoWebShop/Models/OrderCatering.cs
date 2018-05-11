using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepoWebShop.Interfaces;
using System;
using System.Linq;

namespace RepoWebShop.Models
{
    public class OrderCatering : OrderCateringBase
    {
        public int OrderCateringId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
