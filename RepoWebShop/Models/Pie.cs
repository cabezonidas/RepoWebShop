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
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SizeDescription { get; set; }

        public int PieDetailId { get; set; }

        public virtual PieDetail PieDetail { get; set; }
    }
}
