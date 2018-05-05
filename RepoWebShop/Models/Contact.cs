using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Display(Name = "Apellido")]
        public string Lastname { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Empresa")]
        public string Company { get; set; }

        [Display(Name = "Teléfono de trabajo")]
        public string WorkPhone { get; set; }

        [Display(Name = "Teléfono particular")]
        public string PrivatePhone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Palabras clave")]
        public string Keywords { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Vínculo")]
        public string Connection { get; set; }

        [Display(Name = "Información extra")]
        public string Description { get; set; }
    }
}
