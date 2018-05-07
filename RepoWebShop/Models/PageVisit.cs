using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PageVisit
    {
        public int PageVisitId { get; set; }
        public string Ip { get; set; }
        public DateTime Visited { get; set; }
        public string Path { get; set; }
        public string BookingId { get; set; }
        public ApplicationUser User { get; internal set; }
    }
}
