using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;

namespace RepoWebShop.ViewModels
{
    public class ProductInflationEstimateViewModel
    {
        public Product Product { get; internal set; }
        public decimal EstimateOnline { get; internal set; }
        public decimal EstimateInStore { get; internal set; }
    }
}
