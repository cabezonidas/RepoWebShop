using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.ViewModels
{
    public class ResetPasswordNewPasswordViewModel
    {
        [Required(ErrorMessage = "Por favor ingrese su nueva contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password1 { get; set; }

        [Required(ErrorMessage = "Por favor repita su nueva contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Repetir contraseña")]
        public string Password2 { get; set; }

        public string Hash { get; set; }
        public string UserId { get; set; }
    }
}
