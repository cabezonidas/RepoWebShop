using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class SiteException
    {
        public int SiteExceptionId { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public string BookingId { get; set; }
        public string Ip { get; set; }
        public string Error { get; set; }
        public ApplicationUser User { get; set; }
    }
}
