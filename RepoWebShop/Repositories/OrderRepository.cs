using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Interfaces;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICalendarRepository _calendarRepository;

        public OrderRepository(AppDbContext appDbContext, ICalendarRepository calendarRepository, ShoppingCart shoppingCart, IShoppingCartRepository shoppingCartRepository)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
            _shoppingCartRepository = shoppingCartRepository;
            _calendarRepository = calendarRepository;
        }


        public Order UpdateOrder(PaymentNotice paymentNotice)
        {
            Order order = this.GetOrderByBookingId(paymentNotice.BookingId);
            if (order != null)
            {
                order.MercadoPagoMail = paymentNotice.MercadoPagoMail;
                order.MercadoPagoName = paymentNotice.MercadoPagoName;
                order.MercadoPagoUsername = paymentNotice.MercadoPagoUsername;
                order.MercadoPagoTransaction = paymentNotice.Payment_Id;
                order.Status = paymentNotice.Status;
                _appDbContext.SaveChanges();
            }
            return order;
        }

        public void UpdateManagementComments(int orderId, string comments)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).ManagementComments = comments;
            _appDbContext.SaveChanges();
        }

        public void UpdatePickUpDate(int orderId, DateTime pickUp)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).PickUp = pickUp;
            _appDbContext.SaveChanges();
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).Status = status;
            _appDbContext.SaveChanges();
        }

        public bool InvertPickedUpStatus(int orderId)
        {
            Order order = _appDbContext.Orders.First(x => x.OrderId == orderId);
            order.PickedUp = !order.PickedUp;
            _appDbContext.SaveChanges();
            return order.PickedUp;
        }

        public Order GetDraftOrderByBookingId(string friendlyBookingId)
        {
            return _appDbContext.Orders.Include(x => x.Registration).OrderByDescending(x => x.OrderId).FirstOrDefault(x => x.BookingId.EndsWith(friendlyBookingId) && x.Status == "draft");
        }

        public Order GetOrderByBookingId(string friendlyBookingId)
        {
            return _appDbContext.Orders.Include(x => x.Registration).OrderByDescending(x => x.OrderId).FirstOrDefault(x => x.BookingId.EndsWith(friendlyBookingId));
        }

        public Order GetOrder(int id)
        {
            return _appDbContext.Orders.Include(x => x.Registration).FirstOrDefault(x => x.OrderId == id);
        }



        public IEnumerable<OrderDetail> GetOrderDetails(int id)
        {
            return _appDbContext.OrderDetails.Include(x => x.Order.Email).Include(x => x.Pie).ThenInclude(x => x.PieDetail).Where(x => x.OrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _appDbContext.Orders.Include(x => x.Registration).Where(o => o.Status != "draft").ToList();
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = _calendarRepository.LocalTime();
            order.PickedUp = false;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            order.PreparationTime = _shoppingCart.GetShoppingCartPreparationTime();

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Pie.Price
                };

                _appDbContext.OrderDetails.Add(orderDetail);
            }
            _appDbContext.SaveChanges();
        }


        public Order CreateOrderByPayment(PaymentNotice paymentNotice)
        {
            return _shoppingCartRepository.CreateOrderByPayment(paymentNotice);
        }

        public EmailNotificationViewModel GetEmailData(int id, string absoluteUrl)
        {
            var orderDetails = GetOrderDetails(id);
            var order = GetOrder(id);

            var emailData = new EmailNotificationViewModel();

            emailData.AbsoluteUrl = absoluteUrl;
            emailData.Comments = order.CustomerComments;
            emailData.MercadoPagoTransaction = order.MercadoPagoTransaction;
            emailData.OrderItems = orderDetails;
            emailData.OrderReady = order.PickUp;
            emailData.OrderTotal = order.OrderTotal; //Without MP interests
            emailData.OrderType = String.IsNullOrEmpty(order.MercadoPagoTransaction) ? "Reserva" : "Compra";
            emailData.PreparationTime = order.PreparationTime;
            emailData.FriendlyBookingId = order.FriendlyBookingId;
            emailData.OrderId = order.OrderId.ToString();

            emailData.CustomarAlias = order.Registration == null ? order.MercadoPagoName : order.Registration.FirstName;
            emailData.CustomarAlias = Regex.Replace(emailData.CustomarAlias.ToLower(), @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            //TextInfo textInfo = new CultureInfo("es-AR", false).TextInfo;
            //emailData.CustomarAlias = textInfo.ToTitleCase(emailData.CustomarAlias.ToLower());

            return emailData;
        }
    }
}
