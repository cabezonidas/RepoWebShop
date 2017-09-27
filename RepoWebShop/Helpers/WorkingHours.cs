using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Helpers
{
    public static class WorkingHours
    {
        public static DateTime GetPickUpDate(DateTime orderAccredited, int estimationHs, IEnumerable<IWorkingHours> processingHours, IEnumerable<IWorkingHours> openHours)
        {
            return GetOrderReady(GetOrderReady(orderAccredited, estimationHs, processingHours), 0, openHours);
        }

        public static DateTime GetOrderReady(DateTime orderAccreditted, int estimationHs, IEnumerable<IWorkingHours> workingHours)
        {
            int minutesEstimation = estimationHs * 60;
            int dayOfWeekSubmitted = (int)orderAccreditted.DayOfWeek;
            bool orderReady = false;
            var offset = 0;
            var availableMinutes = 0;
            var preparationDays = 0;

            for (int i = dayOfWeekSubmitted; !orderReady; i++)
            {
                var workingslots = GetWorkingSlots(workingHours, i % 7);
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

        private static SortedSet<int> GetWorkingSlots(IEnumerable<IWorkingHours> workingHours, int dayId)
        {
            // Check public holidays and special dates
            var processingSlots = workingHours.Where(x => x.DayId == dayId);

            SortedSet<int> result = new SortedSet<int>();

            foreach (var processingSlot in processingSlots)
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
