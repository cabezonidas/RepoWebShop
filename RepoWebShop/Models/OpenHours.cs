using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class OpenHours : IWorkingHours
    {
        public int OpenHoursId { get; set; }
        public TimeSpan StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public int DayId { get; set; }
    }
}
