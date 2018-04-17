using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class AddDiscountViewModel
    {
        [Display(Name = "Descuento")]
        public string Code { get; set; }
    }
}
