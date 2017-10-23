using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Pie
    {
    	public int PieId { get; set; }
        [Required]
        [Display(Name = "Tamaño")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Descripción del tamaño")]
        public string SizeDescription { get; set; }

        public bool IsActive { get; set; }
        public int PieDetailId { get; set; }

        public virtual PieDetail PieDetail { get; set; }
    }
}
