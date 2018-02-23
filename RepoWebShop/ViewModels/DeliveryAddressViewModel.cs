using RepoWebShop.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.ViewModels
{
    public class DeliveryAddressViewModel : DeliveryAddress
    {
        [Display(Name = "Distancia")]
        [Range(0, int.MaxValue)]
        public override int Distance { get; set; }

        [Display(Name = "Costo de envío")]
        public override decimal DeliveryCost { get; set; }
        
        public override string ShoppingCartId { get; set; }
        
        public override DateTime Created { get; set; }
        public int MinimumCharge { get; set; }
        public int CostByBlock { get; set; }
    }
}
