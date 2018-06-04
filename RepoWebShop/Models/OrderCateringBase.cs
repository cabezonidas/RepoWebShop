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
    }
}
