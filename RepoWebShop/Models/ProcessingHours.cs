using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.Models
{
    public class ProcessingHours : WorkingHours
    {
        public int ProcessingHoursId { get; set; }
    }
}
