using Microsoft.AspNetCore.Mvc.Rendering;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PieDetailCreateViewModel
    {
        public int PieDetailId { get; set; }

        [Required]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Descripcion breve")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Descripcion larga")]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "Imagen calidad alta")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Imagen calidad media")]
        public string ImageThumbnailUrl { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Tiempo de preparacion (hs)")]
        public int PreparationTime { get; set; }
        
        public List<SelectListItem> Categories { set; get; }
    }
}
