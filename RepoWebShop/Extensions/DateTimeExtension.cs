﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime Zoned(this DateTime time, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }
		public static bool WithinRange(this DateTime date, DateTime dateFrom, int daysDuration) =>
				dateFrom <= date && dateFrom.AddDays(daysDuration) >= date;
	}
}
