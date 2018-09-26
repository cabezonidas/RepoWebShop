using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class Calendar
    {
		public IEnumerable<ProcessingHours> ProcessingHours { get; set; }
		public IEnumerable<OpenHours> OpenHours { get; set; }
		public IEnumerable<PublicHoliday> Holidays { get; set; }
		public IEnumerable<Vacation> Vacations { get; set; }

		public Calendar(IEnumerable<ProcessingHours> processingHours, IEnumerable<OpenHours> openHours, IEnumerable<PublicHoliday> holidays, IEnumerable<Vacation> vacations)
		{
			ProcessingHours = processingHours;
			OpenHours = openHours;
			Holidays = holidays;
			Vacations = vacations;
		}
	}
}
