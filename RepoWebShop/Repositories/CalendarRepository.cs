using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;
using Microsoft.Extensions.Configuration;

namespace RepoWebShop.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;

        public CalendarRepository(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
        }

        public DateTime LocalTime()
        {
            return DateTime.Now.Zoned(_config.GetSection("LocalZone").Value);
        }

        public DateTime GetPickupEstimate(int hours)
        {
            return WorkingHours.GetPickUpDate(
                LocalTime(),
                hours,
                _appDbContext.ProcessingHours.ToList(),
                _appDbContext.OpenHours.ToList(),
                _appDbContext.Holidays.ToList(),
                _appDbContext.Vacations.ToList());            
        }
    }
}
