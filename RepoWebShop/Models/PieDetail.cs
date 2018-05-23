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

        [Display(Name = "Breve descripcion")]
        public string ShortDescription { get; set; }

        [Display(Name = "Descripcion completa")]
        public string LongDescription { get; set; }

        [Display(Name = "Album Id Flick")]
        public long FlickrAlbumId { get; set; }

        [Display(Name = "Destacado")]
        public bool IsPieOfTheWeek { get; set; } = false;

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = false;

        [Display(Name = "Ingredientes")]
        public string Ingredients { get; internal set; }
    }
}
