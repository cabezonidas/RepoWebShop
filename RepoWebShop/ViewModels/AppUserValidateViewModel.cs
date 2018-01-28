using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class AppUserValidateViewModel
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Por favor, ingresa el código que recibiste en el celular")]
        public string Token { get; set; }

        public string PhoneNumber { get; set; }
        public string Controller { get; internal set; }
        public string Action { get; internal set; }
    }
}
