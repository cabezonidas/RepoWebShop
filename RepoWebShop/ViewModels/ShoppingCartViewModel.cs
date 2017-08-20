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
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public string Mercadolink { get; set; }
        public int PreparationTime { get; set; }
        
        [Display(Name = "Comentarios")]
        [StringLength(250)]
        public string Comments { get; set; }
    }
}
