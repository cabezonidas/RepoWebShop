using RepoWebShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> Items { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public int CountTrolleyObjects { get; set; }
        public string Mercadolink { get; set; }
        public int PreparationTime { get; set; }
        public string FriendlyBookingId { get; set; }
        public string Comments { get; set; }
        public DateTime PickupDate { get; set; }
        public PickUpTimeViewModel PickupTime { get; set; }
        public string MercadoPagoId { get; set; }
        public ApplicationUser User { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public int MaxArsForReservation { get; internal set; }
        public int MinArsForDelivery { get; internal set; }
        public int MinimumDeliveryCharge { get; internal set; }
        public int DeliveryCostByBlock { get; internal set; }
        public int DeliveryRadius { get; internal set; }
        public Discount Discount { get; internal set; }
        public decimal ShopingCartTotalWithoutDiscount { get; internal set; }
        public List<ShoppingCartCatalogItem> CatalogItems { get; internal set; }
        public List<ShoppingCartComboCatering> Caterings { get; internal set; }
        public ShoppingCartLunch CustomCatering { get; internal set; }
    }
}
