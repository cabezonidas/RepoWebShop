using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Cuit
    {
        public int CuitId { get; set; }
        public long Number { get; set; }
        public bool Valid { get; set; }
        public DateTime Created { get; set; }
    }
}
