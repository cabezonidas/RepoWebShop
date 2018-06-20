using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class FEAuthRequest
    {
        public long Cuit { get; internal set; }
        public string Sign { get; internal set; }
        public string Token { get; internal set; }
    }
}
