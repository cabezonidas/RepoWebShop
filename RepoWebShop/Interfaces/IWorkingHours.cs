using System;
using System.Collections.Generic;
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

        //DateTime GetPickUpDate(DateTime orderAccredited, int estimationHs, IEnumerable<IWorkingHours> processingHours, IEnumerable<IWorkingHours> openHours);

        //DateTime GetOrderReady(DateTime orderAccreditted, int estimationHs, IEnumerable<IWorkingHours> workingHours);
    }
}
