using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Por favor ingrese un nombre de usuario")]
        [Display(Name = "Nombre de usuario")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su nombre")]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su apellido")]
        [Display(Name = "Apellido")]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Dirección")]
        public virtual string AddressLine1 { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Altura")]
        public virtual string StreetNumber { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Calle")]
        public virtual string StreetName { get; set; }
        
        [Display(Name = "Codigo Postal")]
        [StringLength(50, MinimumLength = 4)]
        public virtual string ZipCode { get; set; }

        [Display(Name = "Provincia")]
        [StringLength(50)]
        public string State { get; set; }

        [Display(Name = "País")]
        [StringLength(50)]
        public string Country { get; set; }

        [Display(Name = "Género")]
        [StringLength(50)]
        public string Gender { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [StringLength(50)]
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }
        
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public override string PhoneNumber { get; set; }
        
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "El email ingresado no está en un formato correcto")]
        public override string Email { get; set; }

        public string ValidationPhoneToken { get; set; }
        public DateTime? ValidationMailToken { get; set; }
        public string PhoneNumberDeclared { get; set; }
        public string FacebookNameIdentifier { get; set; }
        public string GoogleNameIdentifier { get; set; } 

        public IEnumerable<DeliveryAddress> DeliveryAddresses { get; set; }
    }
}
