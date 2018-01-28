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
    }
}
