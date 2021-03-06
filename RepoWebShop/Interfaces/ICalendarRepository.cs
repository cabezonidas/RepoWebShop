﻿using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface ICalendarRepository
    {
        string dayToSpanish(string day);
        string spanishMonth(int month);
        DateTime GetPickupEstimate(int hours);
        string GetSoonestPickupEstimateForUsers(int hours);
        DateTime LocalTime();
        string LocalTimeAsString();
        string SuperFriendlyDate(DateTime? date);
        string FriendlyDate(DateTime? date);
        IEnumerable<KeyValuePair<DateTime, TimeSpan>> GetPickUpOption(int preparationTime, Discount discount);
        DateTime ToLocalTime(DateTime dateTime);
		OpenHoursViewModel PublicCalendar();
	}
}
