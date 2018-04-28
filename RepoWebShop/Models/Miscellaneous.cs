using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class LunchMiscellaneous
    {
        public int LunchMiscellaneousId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Lunch Lunch { get; set; }
    }
}
