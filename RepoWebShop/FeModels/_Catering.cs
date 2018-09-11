using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
	public class _Catering
	{
		public int LunchId { get; set; }

		public IEnumerable<_CateringItem> Items { get; set; }

		public IEnumerable<_CateringMiscellaneous> Miscellanea { get; set; }

		public string Comments { get; set; }

		public bool IsCombo { get; set; }

		public bool IsGeneric { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string ThumbnailUrl { get; set; }

		public int PreparationTime { get; set; }

		public int EventDuration { get; set; }

		public int Attendants { get; set; }

		public decimal Price { get; set; }

		public decimal PriceInStore { get; set; }

		// public DateTime Estimation { get; set; }
	}
}
