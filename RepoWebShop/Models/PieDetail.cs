using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PieDetail
    {
        public int PieDetailId { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Breve descripcion")]
        public string ShortDescription { get; set; }
        [Required]
        [Display(Name = "Descripcion completa")]
        public string LongDescription { get; set; }
        public string AllergyInformation { get; set; }
        [Required]
        [Display(Name = "Url calidad alta")]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name = "Url calidad baja")]
        public string ImageThumbnailUrl { get; set; }
        public bool IsPieOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Horas de preparacion")]
        public int PreparationTime { get; set; }
        [Display(Name = "Categoria")]
        public virtual Category Category { get; set; }

        public bool IsActive { get; set; }
    }
}
