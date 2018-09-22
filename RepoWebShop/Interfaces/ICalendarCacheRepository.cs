using System;

namespace RepoWebShop.Interfaces
{
    public interface ICalendarCacheRepository
    {
		DateTime? GetPickup(int hours);
		void SetPickup(int hours, DateTime pickupEstimate);
	}
}
