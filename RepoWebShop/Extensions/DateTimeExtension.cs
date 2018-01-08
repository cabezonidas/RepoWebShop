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
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }
    }
}
