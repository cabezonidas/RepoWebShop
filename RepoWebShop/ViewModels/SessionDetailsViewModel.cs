using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;

namespace RepoWebShop.ViewModels
{
    public class SessionDetailsViewModel
    {
        public List<ShoppingCartItem> Items { get; internal set; }
        public List<ShoppingCartComment> Comments { get; internal set; }
        public List<ShoppingCartDiscount> Discounts { get; internal set; }
        public List<ShoppingCartPickUpDate> PickUpDates { get; internal set; }
        public List<ShoppingCartLunch> CustomLunches { get; internal set; }
        public List<ShoppingCartComboCatering> Caterings { get; internal set; }
        public List<ShoppingCartCatalogItem> Products { get; internal set; }
        public string FriendlyBookingId { get; internal set; }
        public List<DeliveryAddress> Delivery { get; internal set; }
        public List<PageVisit> Visits { get; internal set; }
        public IOrderedEnumerable<BookingRecord> Ips { get; internal set; }
        public List<Order> Orders { get; internal set; }
        public List<ApplicationUser> Users { get; internal set; }
    }
}
