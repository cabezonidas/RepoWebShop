using System;
using System.ComponentModel.DataAnnotations;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Models
{
    public class ProcessingHours : IWorkingHours
    {
        public int ProcessingHoursId { get; set; }
        public TimeSpan StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public int DayId { get; set; }
    }
}
