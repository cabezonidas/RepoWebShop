using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class WorkingHours : IWorkingHours
    {
        public int Id { get; set; }
        public TimeSpan StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        [Range(0, 8)]
        public int DayId { get; set; }

        public static DateTime GetPickUpDate(DateTime orderAccredited, int estimationHs, IEnumerable<IWorkingHours> processingHours, IEnumerable<IWorkingHours> openHours, IEnumerable<PublicHoliday> holidays, IEnumerable<Vacation> vacations)
        {
            var orderFinished = GetOrderReady(orderAccredited, estimationHs, processingHours, holidays, vacations, true);
            return GetOrderReady(orderFinished, 0, openHours, holidays, vacations, false);
        }

        public static DateTime GetOrderReady(DateTime orderAccreditted, int estimationHs, IEnumerable<IWorkingHours> workingHours, IEnumerable<PublicHoliday> holidays, IEnumerable<Vacation> vacations, bool isProcessing)
        {
            int minutesEstimation = estimationHs * 60;
            int dayOfWeekSubmitted = (int)orderAccreditted.DayOfWeek;
            bool orderReady = false;
            var offset = 0;
            var availableMinutes = 0;
            var preparationDays = 0;

            for (int i = dayOfWeekSubmitted, days = 0; !orderReady; i++, days++)
            {
                var workingslots = GetWorkingSlots(workingHours, i % 7, isProcessing, orderAccreditted, vacations, holidays, days);
                offset = orderAccreditted.Hour * 60 + orderAccreditted.Minute;
                if (i == dayOfWeekSubmitted)
                    workingslots.RemoveWhere(x => x < offset);
                availableMinutes += workingslots.Count;

                if (availableMinutes >= minutesEstimation)
                {
                    if (workingslots.Count > 0)
                        offset = workingslots.TakeLast(availableMinutes - minutesEstimation + 1).First();
                    preparationDays = i - dayOfWeekSubmitted;
                    orderReady = true;
                }
            }
            return orderAccreditted.Date.AddDays(preparationDays).AddMinutes(offset);
        }

        private static int getminutes(TimeSpan time)
        {
            return time.Hours * 60 + time.Minutes;
        }

        private static SortedSet<int> GetWorkingSlots(IEnumerable<IWorkingHours> workingHours, int dayId, bool isProcessing, DateTime orderAccreditted, IEnumerable<Vacation> vacations, IEnumerable<PublicHoliday> holidays, int days)
        {
            SortedSet<int> result = new SortedSet<int>();

            var workingDay = orderAccreditted.Date.AddDays(days);
            int? isVacationDay = vacations?.Where(x => workingDay >= x.StartDate.Date && workingDay <= x.EndDate.Date).Count();

            if (isVacationDay.HasValue && isVacationDay.Value > 0)
                return result;

            var workingSlots = workingHours.Where(x => x.DayId == dayId);

            var matchingHolidays = holidays?.Where(x => x.Date.Date == workingDay);

            if (matchingHolidays != null && matchingHolidays.Count() > 0)
                if(isProcessing)
                    workingSlots = matchingHolidays.Where(x => x.ProcessingHours != null).Select(x => x.ProcessingHours);
                else
                    workingSlots = matchingHolidays.Where(x => x.OpenHours != null).Select(x => x.OpenHours);

            foreach (var processingSlot in workingSlots)
            {
                var startingAt = getminutes(processingSlot.StartingAt);
                var duration = getminutes(processingSlot.Duration);
                result.UnionWith(new SortedSet<int>(Enumerable.Range(startingAt, duration)));
            }

            result.RemoveWhere(x => x >= 1440);

            return result;
        }
    }
}
