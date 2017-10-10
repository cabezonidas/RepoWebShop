using System;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.ViewModels
{
    public class AddSpecialDateViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Produccion desde")]
        public TimeSpan? ProcessingHoursStartingAt { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Produccion hasta")]
        public TimeSpan? ProcessingHoursFinishingAt { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Abierto desde")]
        public TimeSpan? OpenHoursStartingAt { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Abierto hasta")]
        public TimeSpan? OpenHoursFinishingAt { get; set; }
    }
}
