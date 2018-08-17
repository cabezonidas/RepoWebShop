using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _CartCatering
    {

		public int ShoppingCartComboCateringId { get; set; }
		public int Amount { get; set; }
		public int LunchId { get; set; }
		public string BookingId { get; set; }
		public DateTime Created { get; set; }
		public int PreparationTime { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal PriceInStore { get; set; }
		public int Attendants { get; set; }
		public int EventDuration { get; set; }

	}
}
