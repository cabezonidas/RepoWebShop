using System;
namespace RepoWebShop.Interfaces
{
    public interface ICalendarRepository
    {
        DateTime GetPickupEstimate(int hours);
        DateTime LocalTime();
    }
}
