using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class RefundOrderViewModel
    {
        [Display(Name = "Motivo")]
        [Required]
        public string Reason { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
