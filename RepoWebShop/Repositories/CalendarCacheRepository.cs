using System;
using System.Linq;
using RepoWebShop.Interfaces;
using System.Collections.Generic;

namespace RepoWebShop.Repositories
{
    public class CalendarCacheRepository : ICalendarCacheRepository
	{
		private List<PickupCache> _cache = new List<PickupCache>();

		private TimeSpan Cache = new TimeSpan(0, 5, 0);

		public DateTime? GetPickup(int hours)
		{
			var result = _cache.FirstOrDefault(x => x.Hours == hours);
			if(result != null)
			{
				if (DateTime.Now.Subtract(result.Saved) > Cache)
					_cache.Remove(result);
				else
					return result.PickupEstimate;
			}
			return null;
		}

		public void SetPickup(int hours, DateTime pickupEstimate)
		{
			_cache.Add(new PickupCache { Hours = hours, PickupEstimate = pickupEstimate, Saved = DateTime.Now });
		}

		private class PickupCache
		{
			public int Hours { get; set; }
			public DateTime Saved { get; set; }
			public DateTime PickupEstimate { get; set; }
		}
	}
}
