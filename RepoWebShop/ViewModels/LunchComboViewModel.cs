using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class LunchComboViewModel
    {
        public int LunchId { get; set; }

        [Display(Name = "Combo Activo")]
        [Required]
        public bool IsCombo { get; set; }

        [Display(Name = "Título")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Precio del combo")]
        [Required]
        public string ComboPrice { get; set; }

        [Display(Name = "Tiempo de preparación (hs)")]
        [Required]
        public int PreparationTime { get; set; }

        [Display(Name = "Duración del Evento (hs)")]
        [Required]
        public int EventDuration { get; set; }

        [Display(Name = "Invitados")]
        [Required]
        public int Attendants { get; set; }
    }
}
