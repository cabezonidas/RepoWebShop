using RepoWebShop.Models;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface ICalendarRepository
    {
        DateTime GetPickupEstimate(int hours);
        string GetSoonestPickupEstimateForUsers(int hours);
        DateTime LocalTime();
        string LocalTimeAsString();
        string SuperFriendlyDate(DateTime? date);
        string FriendlyDate(DateTime? date);
        IEnumerable<KeyValuePair<DateTime, TimeSpan>> GetPickUpOption(int preparationTime, Discount discount);
    }
}
