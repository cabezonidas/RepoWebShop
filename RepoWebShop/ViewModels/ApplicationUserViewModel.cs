using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string ErrorMsg { get; set; }
        [Required(ErrorMessage = "Por favor ingrese un nombre de usuario")]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su nombre")]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su apellido")]
        [Display(Name = "Apellido")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su dirección")]
        [StringLength(100)]
        [Display(Name = "Dirección")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la altura en su dirección")]
        [StringLength(100)]
        [Display(Name = "Altura")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su calle o avenida")]
        [StringLength(100)]
        [Display(Name = "Calle")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su código postal")]
        [Display(Name = "Codigo Postal")]
        [StringLength(50, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [Display(Name = "Provincia")]
        [StringLength(50)]
        public string State { get; set; }

        //[Required(ErrorMessage = "Por favor complete el número de teléfono")]
        //[StringLength(25)]
        //[DataType(DataType.PhoneNumber)]
        //[Display(Name = "Teléfono")]
        //public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Por favor ingrese su email")]
        //[StringLength(50)]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
        //    ErrorMessage = "El email ingresado no está en un formato correcto")]
        //public string Email { get; set; }
    }
}
