using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class Photosets
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Perpage { get; set; }
        public int Total { get; set; }
        public IEnumerable<PhotosetMetadata> Photoset { get; set; }
    }
}