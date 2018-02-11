using RepoWebShop.Models;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<int> GetDistanceAsync(string addressLine1);
        void AddDelivery(DeliveryAddress deliveryAddress);
    }
}
