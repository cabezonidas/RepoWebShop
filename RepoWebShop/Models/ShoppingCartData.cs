using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ShoppingCartData
    {
        public int ShoppingCartDataId { get; set; }
        public string BookingId { get; set; }
        public DateTime Created { get; set; }
        public string MercadoPagoPreferenceId { get; set; }
        public DateTime LastUpdate { get; internal set; }
    }
}
