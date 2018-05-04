using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class EmailNotificationViewModel
    {
        public string OrderType { get; set; }
        public string OrderId { get; set; }
        public string MercadoPagoTransaction { get; set; }
        public DateTime? OrderReady { get; set; }
        public Decimal OrderTotal { get; set; }
        public IEnumerable<OrderDetail> OrderItems { get; set; }
        public string AbsoluteUrl { get; set; }
        public string Comments { get; set; }
        public int PreparationTime { get; set; }
        public string FriendlyBookingId { get; internal set; }
        public string CustomarAlias { get; set; }
        public DeliveryAddress Delivery { get; internal set; }
        public Discount Discount { get; internal set; }
        public TimeSpan? TimeLeftUntilStoreCloses { get; internal set; }
    }
}
