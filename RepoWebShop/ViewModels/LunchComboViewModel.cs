﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class LunchComboViewModel
    {
        public int LunchId { get; set; }

        [Display(Name = "¿Se puede comprar por internet?")]
        [Required]
        public bool IsCombo { get; set; }

        [Display(Name = "Tipo")]
        [Required]
        public bool IsGeneric { get; set; }

        [Display(Name = "Título")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }
        
        [Display(Name = "Duración del Evento (hs)")]
        [Required]
        [Range(1, int.MaxValue)]
        public int EventDuration { get; set; }

        [Display(Name = "Invitados")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Attendants { get; set; }

        [Display(Name = "Imagen preliminar")]
        public string ThumbnailUrl { get; set; }

    }
}
