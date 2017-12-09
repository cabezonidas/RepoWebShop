using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Photo
    {
        public Int64 Id { get; set; }
        public string Secret { get; set; }
        public int Server { get; set; }
        public int Farm { get; set; }
        public string Title { get; set; }
        public string IsPrimary { get; set; }
        //public bool IsPublic { get; set; }
        //public bool IsFriend { get; set; }
        //public bool IsFamily { get; set; }
    }
}
