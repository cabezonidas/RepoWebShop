using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Registration : IdentityUser
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public override string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

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
        [Display(Name = "Dirección 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Dirección 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su código postal")]
        [Display(Name = "Codigo Postal")]
        [StringLength(10, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Por favor complete la ciudad")]
        [Display(Name = "Ciudad")]
        [StringLength(50)]
        public string City { get; set; }
        [Display(Name = "Provincia")]
        [StringLength(10)]
        public string State { get; set; }

        [Required(ErrorMessage = "Por favor complete el país")]
        [StringLength(50)]
        [Display(Name = "País")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Por favor complete el número de teléfono")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public override string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "El email ingresado no está en un formato correcto")]
        public override string Email { get; set; }
    }
}
