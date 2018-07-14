using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;

namespace RepoWebShop.ViewModels
{
    public class SessionDetailsViewModel
    {
        public IEnumerable<ShoppingCartItem> Items { get; internal set; }
        public IEnumerable<ShoppingCartComment> Comments { get; internal set; }
        public IEnumerable<ShoppingCartDiscount> Discounts { get; internal set; }
        public IEnumerable<ShoppingCartPickUpDate> PickUpDates { get; internal set; }
        public IEnumerable<ShoppingCartLunch> CustomLunches { get; internal set; }
        public IEnumerable<ShoppingCartComboCatering> Caterings { get; internal set; }
        public IEnumerable<ShoppingCartCatalogItem> Products { get; internal set; }
        public string FriendlyBookingId { get; internal set; }
        public IEnumerable<DeliveryAddress> Delivery { get; internal set; }
        public IEnumerable<PageVisit> Visits { get; internal set; }
        public IEnumerable<BookingRecord> Ips { get; internal set; }
        public IEnumerable<Order> Orders { get; internal set; }
        public IEnumerable<ApplicationUser> Users { get; internal set; }
    }
}
