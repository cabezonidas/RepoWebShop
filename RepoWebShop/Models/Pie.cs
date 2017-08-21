using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Pie
    {
    	public int PieId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual PieDetail PieDetail { get; set; }
    }
}
