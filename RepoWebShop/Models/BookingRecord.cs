using System;

namespace RepoWebShop.Models
{
    public class BookingRecord
    {
        public int BookingRecordId { get; set; }
        public DateTime Created { get; set; }
        public string BookingId { get; set; }
        public string Ip { get; set; }
    }
}
