using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class RegisterViewModel : ApplicationUser
    {
        [Required(ErrorMessage = "Por favor ingrese una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Por favor ingrese su dirección")]
        [StringLength(100)]
        [Display(Name = "Dirección")]
        public override string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la altura en su dirección")]
        [StringLength(100)]
        [Display(Name = "Altura")]
        public override string StreetNumber { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su calle o avenida")]
        [StringLength(100)]
        [Display(Name = "Calle")]
        public override string StreetName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su código postal")]
        [Display(Name = "Codigo Postal")]
        [StringLength(50, MinimumLength = 4)]
        public override string ZipCode { get; set; }
    }
}
