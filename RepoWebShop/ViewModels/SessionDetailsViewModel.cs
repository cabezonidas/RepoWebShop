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
        public List<ShoppingCartLunch> Lunches { get; internal set; }
    }
}
