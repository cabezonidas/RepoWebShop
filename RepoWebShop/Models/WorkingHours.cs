using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class WorkingHours : IWorkingHours
    {
        public int Id { get; set; }
        public TimeSpan StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public int DayId { get; set; }
    }
}
