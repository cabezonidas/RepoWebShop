using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class CuitDetail
    {
        public int CuitDetailId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
        public Cuit Cuit { get; set; }
    }
}
