using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CalendarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public DateTime GetPickupEstimate(int hours)
        {
            return WorkingHours.GetPickUpDate(
                DateTime.Now,
                hours,
                _appDbContext.ProcessingHours.ToList(),
                _appDbContext.OpenHours.ToList(),
                _appDbContext.Holidays.ToList(),
                _appDbContext.Vacations.ToList());            
        }
    }
}
