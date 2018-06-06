using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PromoSmsViewModel
    {
        [Required]
        [MaxLength(160)]
        [Display(Name = "Cuerpo del SMS")]
        public string Body { get; set; }
    }
}
