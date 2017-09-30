using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class OpenHoursViewModel : CalendarFormatViewModel
    {
        public IEnumerable<OpenHours> OpenHours { get; set; }
        public IEnumerable<PublicHoliday> PublicHolidays { get; set; }
        public IEnumerable<Vacation> Vacations { get; set; }
    }
}
