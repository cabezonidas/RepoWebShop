using RepoWebShop.FeModels;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<int> GetDistanceAsync(string addressLine1);
        Task<int> GetExactDistanceAsync(string addressLine1);
        void AddOrUpdateDelivery(DeliveryAddress deliveryAddress);
        Decimal GetDeliveryEstimate(decimal distance);
        Task<string> GuessPlaceIdAsync(string address);
        Task<AddressViewModel> GetPlaceAsync(string placeId);
		bool CanDelivery();
		int DistanceFromStore(string lat, string lng, char v);
		int DistanceFromDeliveryCenter(string lat, string lng, char v);
		bool IsValidDistance(string lat, string lng);
		_DeliveryAddress SaveAddress(_DeliveryAddress address);
		_DeliveryAddress UpdateInstructions(_DeliveryAddress deliveryAddress);
	}
}
