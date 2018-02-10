using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class DeliveryAddress
    {
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
    }
}
