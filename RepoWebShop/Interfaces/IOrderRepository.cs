using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Interfaces
{
    public interface IOrderRepository
    {
        void UpdateManagementComments(int orderId, string comments);
        void UpdatePickUpDate(int orderId, DateTime pickUp);
        void UpdateOrderStatus(int orderId, string status);
        bool InvertPickedUpStatus(int orderId);
        Order GetDraftOrderByBookingId(string friendlyBookingId);
        Task<IEnumerable<Order>> GetAllAsync(Func<Order, bool> condition = null);
        Task<IEnumerable<Order>> GetOrdersInProgressAsync();
        Task<IEnumerable<Order>> GetOrdersCancelledAsync();
        Task<IEnumerable<Order>> GetOrdersCompletedAsync();
        Task<IEnumerable<Order>> GetOrdersPickedUpAsync();
        Task<IEnumerable<Order>> GetOrdersReturnedAsync();
        Task<IEnumerable<Order>> GetOrdersRefundedAsync();
        Task<IEnumerable<Order>> GetOrdersPickedUpWithPendingPaymentAsync();
        Order CreateOrder(Order order);
        Task<EmailNotificationViewModel> GetEmailDataAsync(int id);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetOrderByBookingIdAsync(string id);
        Task<Order> GetOrderByFriendlyBookingId(string friendlyId);
        EmailNotificationViewModel ToEmailNotification(Order order);
        Task CompleteOrderAsync(int orderId);
        Task PickUpOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId, string reason);
        Task ReturnOrderAsync(int orderId, string reason);
        Task RefundOrderAsync(int orderId, string reason);
        Task CancelPaymentOrderAsync(int orderId, string reason);
        Task PayOrderAsync(int orderId);
        Task<Order> PaymentNotifiedAsync(PaymentNotice payment);
        Order LatestReservationInProgress(ApplicationUser currentUser);
        Task<IEnumerable<Order>> GetByUserOrdersAsync(ApplicationUser user);
        bool ValidBookingId(string bookingId);
        Task AfterOrderConfirmedAsync(Order order);
    }
}
