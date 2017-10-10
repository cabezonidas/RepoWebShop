using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class SpecialDatesViewModel
    {
        public IEnumerable<Vacation> Vacations { get; set; }
        public IEnumerable<PublicHoliday> Holidays { get; set; }
    }
}
