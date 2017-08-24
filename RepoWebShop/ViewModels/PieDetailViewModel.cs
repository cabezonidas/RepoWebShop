using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PieDetailViewModel
    {

        public PieDetail PieDetail { get; set; }
        public IEnumerable<Pie> Pies { get; set; }
    }
}
