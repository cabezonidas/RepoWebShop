using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;

namespace RepoWebShop.ViewModels
{
    public class ErrorsViewModel
    {
        public List<SiteException> Exceptions { get; internal set; }
        public SessionDetailsViewModel SessionDetails { get; internal set; }
        public List<BookingRecord> BookingRecords { get; internal set; }
    }
}
