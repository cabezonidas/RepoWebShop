using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime Zoned(this DateTime time, string timeZoneId)
        {
            try
            {
                return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            } catch
            {
                // Unix systems don't have Argentina time zone
                return time;
            }
        }
		public static bool WithinRange(this DateTime date, DateTime dateFrom, int daysDuration) =>
				dateFrom <= date && dateFrom.AddDays(daysDuration) >= date;
	}
}
