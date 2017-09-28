using System;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.Interfaces
{
    public interface IWorkingHours
    {
        [Required]
        int Id { get; set; }
        [Required]
        TimeSpan StartingAt { get; set; }
        [Required]
        TimeSpan Duration { get; set; }
        [Required]
        [Range(0, 6)]
        int DayId { get; set; }
    }
}
