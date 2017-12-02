using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class CakeType
    {
        public int CakeTypeId { get; set; }
        public string Description { get; set; }
        public Decimal AddUpPrice { get; set; }
    }
}
