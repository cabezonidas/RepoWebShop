using System;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.Models
{
    public class PublicHoliday
    {
        public int PublicHolidayId { get; set; }
        public DateTime Date { get; set; }
        public OpenHours OpenHours { get; set; }
        public ProcessingHours ProcessingHours { get; set; }
    }
}
