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
        public IEnumerable<ProductEstimationViewModel> Products { get; set; }
        public string PrimaryPicture { get; set; }
        public bool IsMobile { get; internal set; }
        public string RequestAbsoluteUrl { get; set; }
        public AlbumPictures AlbumPitures { get; internal set; }
    }
}
