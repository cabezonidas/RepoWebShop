using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _DeliveryAddress
	{
		public string AddressLine1 { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string ZipCode { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public int Distance { get; set; }
		public string DeliveryInstructions { get; set; }
		public decimal DeliveryCost { get; set; }
		public int DeliveryAddressId { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
	}
}
