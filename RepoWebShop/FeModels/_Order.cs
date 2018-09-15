using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _Order
    {
		public int OrderId { get; set; }

		public string RegistrationId { get; set; }
		public int? EmailId { get; set; }
		public int? DeliveryAddressId { get; set; }
		public int? DiscountId { get; set; }

		public string PhoneNumber { get; set; }
		public string CustomerComments { get; set; }
		public decimal OrderTotal { get; set; }
		public DateTime OrderPlaced { get; set; }
		public bool Finished { get; set; }
		public bool Returned { get; set; }
		public bool Cancelled { get; set; }
		public bool PickedUp { get; set; }
		public bool Refunded { get; set; }
		public bool PaymentReceived { get; set; }

		public DateTime? PickUpTime { get; set; }
		public DateTime? Payout { get; set; }
		public int PreparationTime { get; set; }

		public string ManagementComments { get; set; }
		public string OrderHistory { get; set; }
		public string BookingId { get; set; }
		public string Status { get; set; }
		public string MercadoPagoMail { get; set; }
		public string MercadoPagoName { get; set; }
		public string MercadoPagoUsername { get; internal set; }
		public string MercadoPagoTransaction { get; set; }
		public string FriendlyBookingId { get; set; }
		public string StatusSpanish { get; set; }
		public string CustomerNumbers { get; set; }
		public DateTime? PickUpTimeFrom { get; set; }
		public decimal TotalInStore { get; set; }
		public string PayerIdType { get; set; }
		public string PayerIdNumber { get; set; }
		public string CardHolderName { get; set; }
		public string CardHolderType { get; set; }
		public string CardHolderNumber { get; set; }
		public string Cuit { get; set; }
	}
}
