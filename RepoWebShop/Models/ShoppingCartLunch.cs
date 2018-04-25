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
        public IEnumerable<ShoppingCartLunchItem> Items { get; set; }
        public decimal MiscellaneousPrice { get; set; }
        public string MiscellaneousDescription { get; set; }
        public string Comments { get; set; }
    }
}
