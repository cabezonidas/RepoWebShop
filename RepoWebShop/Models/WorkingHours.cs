using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class WorkingHours
    {
        [Required]
        public TimeSpan StartingAt { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        [Range(0, 6)]
        public int DayId { get; set; }
    }
}
