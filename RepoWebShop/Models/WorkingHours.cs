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

        public static IEnumerable<KeyValuePair<DateTime, TimeSpan>> GetOpenSlots(DateTime orderReady, IEnumerable<IWorkingHours> openHours, IEnumerable<PublicHoliday> holidays, IEnumerable<Vacation> vacations)
        {
            List<DateTime> days = new List<DateTime>();
            for (int i = 0; i < 365; i++)
                days.Add(orderReady.Date.AddDays(i));
            
            List<DateTime> daysCountingVacation = new List<DateTime>();
            foreach(var day in days)
            {
                if (vacations.Count(x => x.StartDate.Date <= day && x.EndDate >= day) == 0)
                    daysCountingVacation.Add(day);
            }

            List<KeyValuePair<DateTime, TimeSpan>> preliminarResults = new List<KeyValuePair<DateTime, TimeSpan>>();
            foreach (var day in daysCountingVacation)
            {
                var openHoursSlotsInHolidays = holidays.Where(x => x.Date.Date == day.Date);
                if(openHoursSlotsInHolidays.Count() > 0)
                {
                    var holidayHours = openHoursSlotsInHolidays.Select(x => new KeyValuePair<DateTime, TimeSpan>(day.Add(x.OpenHours.StartingAt), x.OpenHours.Duration));
                    preliminarResults.AddRange(holidayHours);
                }
                else
                {
                    var ordinaryHours = openHours.Where(x => x.DayId == (int)day.DayOfWeek)
                        .Select(x => new KeyValuePair<DateTime, TimeSpan>(day.Add(x.StartingAt), x.Duration));
                    preliminarResults.AddRange(ordinaryHours);
                }
            }

            var onlyAfterOrIncludingOrderReady = preliminarResults.Where(x => x.Key.Add(x.Value) > orderReady);

            var results = new List<KeyValuePair<DateTime, TimeSpan>>();
            foreach(var item in onlyAfterOrIncludingOrderReady)
                if (item.Key <= orderReady)
                {
                    TimeSpan offset = orderReady.Subtract(item.Key);
                    results.Add(new KeyValuePair<DateTime, TimeSpan>(orderReady, item.Value.Subtract(offset)));
                }
                else
                    results.Add(item);

            return results;
        }

        public static IEnumerable<KeyValuePair<DateTime, TimeSpan>> GetCompatibleOpenSlots(IEnumerable<KeyValuePair<DateTime, TimeSpan>> openSlots, Discount discount, DateTime today)
        {
            var slots = openSlots.ToArray();
            var results = new List<KeyValuePair<DateTime, TimeSpan>>();
            for(int i = 0; i < slots.Length; i++)
            {
                DateTime result = slots[i].Key;
                if (i == 0 && slots[i].Key > today.Date) //Esto es por si alguien usa un descuento a ultima hora, para que lo pueda usar, aunque se entregue al dia sgte.
                    result = today;

                if(discount == null || Discount.ApplyDiscount(result, 1, discount) < 0)
                    results.Add(slots[i]);

                if (discount != null && Discount.ApplyDiscount(result, 1, discount) == 0)
                    break;
            }

            return results;
        }

        public static bool ContainsDateFrame(IEnumerable<KeyValuePair<DateTime, TimeSpan>> compatibleSlots, KeyValuePair<DateTime, TimeSpan> timeframe)
        {
            foreach(var compatibleSlot in compatibleSlots)
            {
                var slotEndsAfterTimeframe = compatibleSlot.Key.Add(compatibleSlot.Value) >= timeframe.Key.Add(timeframe.Value);
                var slotAndTimeframeAreSamedate = timeframe.Key >= compatibleSlot.Key;
                if (slotEndsAfterTimeframe && slotAndTimeframeAreSamedate)
                    return true;
            }
            return false;
        }

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

            //Adjust to 15'
            offset -= offset % 15;

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
