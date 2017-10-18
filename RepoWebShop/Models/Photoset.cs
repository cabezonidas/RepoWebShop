using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Photoset
    {
        public Int64 Id { get; set; }
        public Int64 Primary { get; set; }
        public string Owner { get; set; }
        public string OwnerName { get; set; }
        public IEnumerable<Photo> Photo { get; set; }
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Perpage { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
        public string Title { get; set; }
    }
}
