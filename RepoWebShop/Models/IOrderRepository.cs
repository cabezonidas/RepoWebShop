using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        void UpdateOrderStatus(int orderId, string status);
        Order GetDraftOrderByBookingId(string bookingId);
        Order GetOrderByBookingId(string bookingId);
        IEnumerable<Order> GetAll();
        Order GetOrder(int id);
        IEnumerable<OrderDetail> GetOrderDetails(int id);
        void UpdateManagementComments(int orderId, string comments);
        void UpdatePickUpDate(int orderId, DateTime pickUp);
        bool InvertPickedUpStatus(int orderId);
    }
}
