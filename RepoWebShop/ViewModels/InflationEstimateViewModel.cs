using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class InflationEstimateViewModel
    {
        public IEnumerable<ProductInflationEstimateViewModel> Products { get; internal set; }
        public int Inflation { get; internal set; }
        public int RoundTo { get; internal set; }
    }
}
