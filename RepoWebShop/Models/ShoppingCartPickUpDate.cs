using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartPickUpDate
    {
        public int ShoppingCartPickUpDateId { get; set; }
        public string BookingId { get; set; }
        public DateTime From { get; set; }
        public TimeSpan To { get; set; }
        public bool UserSubmitted { get; set; }
        public string Message { get; set; }
    }
}
