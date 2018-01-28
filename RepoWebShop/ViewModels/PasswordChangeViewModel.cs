using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Por favor ingrese una contraseña nueva")]
        [Display(Name = "Contraseña nueva")]
        [DataType(DataType.Password)]
        public string New { get; set; }

        [Required(ErrorMessage = "Por favor repita la nueva contraseña")]
        [Display(Name = "Repetir contraseña")]
        [DataType(DataType.Password)]
        public string NewRepeated { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su contraseña nueva")]
        [Display(Name = "Contraseña actual")]
        [DataType(DataType.Password)]
        public string Current { get; set; }

        public string ErrorMsg { get; set; }
    }
}
