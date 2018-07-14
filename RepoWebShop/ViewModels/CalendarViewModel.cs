using System.Collections.Generic;
using RepoWebShop.Models;
namespace RepoWebShop.ViewModels
{
    public class CalendarViewModel : CalendarFormatViewModel
    {
        public IEnumerable<ProcessingHours> ProcessingHours { get; internal set; }
        public IEnumerable<OpenHours> OpenHours { get; internal set; }
    }
}
