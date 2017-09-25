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
            //The week starts on Sundays and it's 0
            var dayId = (int)DateTime.Now.DayOfWeek;

            var enoughtime = false;
            for(int i = dayId; !enoughtime; i++)
            {
                //15:00
                var timeFrames = _appDbContext.ProcessingHours.Where(x => x.DayId == i % 7);

            }

            return DateTime.Now;
            
        }
    }
}
