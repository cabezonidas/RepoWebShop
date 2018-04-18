using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Interfaces
{
    public interface IOrderRepository
    {
        EmailNotificationViewModel ToEmailNotification(Order order, string absoluteUrl);
        Order CreateOrder(Order order);
        void UpdateOrderStatus(int orderId, string status);
        Order GetDraftOrderByBookingId(string bookingId);
        Order GetOrderByBookingId(string bookingId);
        IEnumerable<Order> GetAll();
        Order GetOrder(int id);
        IEnumerable<OrderDetail> GetOrderDetails(int id);
        void UpdateManagementComments(int orderId, string comments);
        void UpdatePickUpDate(int orderId, DateTime pickUp);
        bool InvertPickedUpStatus(int orderId);
        EmailNotificationViewModel GetEmailData(int id, string v);
        void CompleteOrder(int orderId, string hostUrl);
        void PickUpOrder(int orderId);
        void CancelOrder(int orderId, string reason);
        void RefundOrder(int orderId, string reason);
        void ReturnOrder(int orderId, string reason);
        IEnumerable<Order> GetOrdersInProgress();
        IEnumerable<Order> GetOrdersCancelled();
        IEnumerable<Order> GetOrdersCompleted();
        IEnumerable<Order> GetOrdersPickedUp();
        IEnumerable<Order> GetOrdersReturned();
        IEnumerable<Order> GetOrdersRefunded();
        IEnumerable<Order> GetOrdersPickedUpWithPendingPayment();
        void PayOrder(int orderId);
        Task<Order> PaymentNotified(PaymentNotice paymentNotification, string hostUrl);
        void CancelPaymentOrder(int orderId, string reason);
        Order LatestReservationInProgress(ApplicationUser currentUser);
        bool ValidBookingId(string bookingId);
        IEnumerable<Order> GetByUserOrders(ApplicationUser user);
    }
}
