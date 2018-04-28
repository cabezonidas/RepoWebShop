using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartLunch
    {
        public int ShoppingCartLunchId { get; set; }
        public string BookingId { get; set; }
        public Lunch Lunch { get; set; }
    }
}
