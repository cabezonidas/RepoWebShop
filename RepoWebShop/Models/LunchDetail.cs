using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class LunchDetail
    {
        public string Title { get; set; }
        public Decimal Precio { get; set; }
        public int CantidadMinima { get; set; }
    }
}
