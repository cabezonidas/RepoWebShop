using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.States;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly ISmsRepository _smsRepository;
        private readonly IMercadoPago _mp;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderRepository(UserManager<ApplicationUser> userManager, IEmailRepository emailRepository, ISmsRepository smsRepository, AppDbContext appDbContext, IMercadoPago mp, ICalendarRepository calendarRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _userManager = userManager;
            _emailRepository = emailRepository;
            _smsRepository = smsRepository;
            _mapper = mapper;
            _mp = mp;
            _appDbContext = appDbContext;
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
            _appDbContext.Orders.First(x => x.OrderId == orderId).PickUpTime = pickUp;
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
            return _appDbContext.Orders.Include(x => x.Registration).Include(x => x.Email).Include(x => x.DeliveryAddress).FirstOrDefault(x => x.OrderId == id);
        }



        public IEnumerable<OrderDetail> GetOrderDetails(int id)
        {
            return _appDbContext.OrderDetails.Include(x => x.Order.Email).Include(x => x.Pie).ThenInclude(x => x.PieDetail).Where(x => x.OrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _appDbContext.Orders
                .Include(x => x.Registration)
                .Include(x => x.OrderLines)
                .Where(o => o.Status != "draft").ToList();

            foreach (var order in orders)
            {
                foreach (var orderLine in order.OrderLines)
                {
                    orderLine.Pie = _appDbContext.Pies.First(x => x.PieId == orderLine.PieId);
                    orderLine.Pie.PieDetail = _appDbContext.PieDetails.First(x => x.PieDetailId == orderLine.Pie.PieDetailId);
                }
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersInProgress()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderInProgress)
            || x.OrderProgressState.GetType() == typeof(OrderComplete));
        }

        public IEnumerable<Order> GetOrdersCancelled()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderCancelled));
        }

        public IEnumerable<Order> GetOrdersCompleted()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderComplete));
        }

        public IEnumerable<Order> GetOrdersPickedUp()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderPickedUp));
        }

        public IEnumerable<Order> GetOrdersReturned()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderReturned));
        }

        public IEnumerable<Order> GetOrdersRefunded()
        {
            return GetAll().Where(x => x.Refunded);
        }

        public IEnumerable<Order> GetOrdersPickedUpWithPendingPayment()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderPickedUp)
                    && (x.OrderPaymentStatus.GetType() == typeof(OrderMercadoPagoNotPaid)
                        || x.OrderPaymentStatus.GetType() == typeof(OrderReservationNotPaid))
            );
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = _calendarRepository.LocalTime();
            order.PickedUp = false;

            var shoppingCartItems = _shoppingCartRepository.GetShoppingCartItems();

            order.PreparationTime = _shoppingCartRepository.GetShoppingCartPreparationTime();

            order.DeliveryAddress = _shoppingCartRepository.GetShoppingCartDeliveryAddress();

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

        public EmailNotificationViewModel GetEmailData(int id, string hostUrl)
        {
            var order = GetOrder(id);
            EmailNotificationViewModel emailData = ToEmailNotification(order, hostUrl);
            return emailData;
        }

        public EmailNotificationViewModel ToEmailNotification(Order order, string absolutUrl)
        {
            var orderDetails = GetOrderDetails(order.OrderId);
            var emailData = new EmailNotificationViewModel();
            emailData.AbsoluteUrl = absolutUrl;
            emailData.Comments = order.CustomerComments;
            emailData.MercadoPagoTransaction = order.MercadoPagoTransaction;
            emailData.OrderItems = orderDetails;
            emailData.OrderReady = order.PickUpTime;
            emailData.OrderTotal = order.OrderTotal; //Without MP interests
            emailData.OrderType = String.IsNullOrEmpty(order.MercadoPagoTransaction) ? "Reserva" : "Compra";
            emailData.PreparationTime = order.PreparationTime;
            emailData.FriendlyBookingId = order.FriendlyBookingId;
            emailData.OrderId = order.OrderId.ToString();
            emailData.Delivery = order.DeliveryAddress;

            emailData.CustomarAlias = order.Registration == null ? order.MercadoPagoName : order.Registration.FirstName;
            emailData.CustomarAlias = Regex.Replace(emailData.CustomarAlias.ToLower(), @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            return emailData;
        }

        public void CompleteOrder(int orderId, string absoluteUrl)
        {
            var order = GetOrder(orderId);
            order.OrderProgressState.Complete(() =>
            {
                order.Finished = true;
                var eventDesc = $"Orden lista.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}";
                _appDbContext.SaveChanges();
                //Send mail
            },
            () => {
                if (order.Registration != null && order.Registration.PhoneNumberConfirmed)
                {
                    var user = order.Registration;
                    _smsRepository.SendSms(user.PhoneNumber,
                        $"{user.FirstName}, tu pedido {order.FriendlyBookingId} ya está listo para ser retirado. Recordá traer DNI. De las Artes.");
                }
                _emailRepository.NotifyOrderCompleteAsync(order, absoluteUrl);
            });
        }

        public void PickUpOrder(int orderId)
        {
            var order = GetOrder(orderId);
            order.OrderProgressState.PickUp(() =>
            {
                order.Finished = true;
                order.PickedUp = true;
                var eventDesc = $"Orden retirada.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}";
                _appDbContext.SaveChanges();
            });
        }

        public void CancelOrder(int orderId, string reason)
        {
            var order = GetOrder(orderId);
            order.OrderProgressState.Cancel(() =>
            {
                order.Cancelled = true;
                var eventDesc = $"Orden cancelada.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}. Motivo: {reason}";
                _appDbContext.SaveChanges();
                //Send mail or text
            });
        }

        public void ReturnOrder(int orderId, string reason)
        {
            var order = GetOrder(orderId);
            order.OrderProgressState.Return(() =>
            {
                order.Returned = true;
                var eventDesc = $"Orden retornada.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}. Motivo: {reason}";
                _appDbContext.SaveChanges();
                //Send mail or text
            });
        }

        public void RefundOrder(int orderId, string reason)
        {
            var order = GetOrder(orderId);
            order.OrderPaymentStatus.Refund(() =>
            {
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - Devolución del dinero. Motivo: {reason}";
                order.Refunded = true;
                order.PaymentReceived = false;
                order.Cancelled = true;
                _appDbContext.SaveChanges();
            }, 
            () =>
            {
                _mp.RefundPaymentAsync(order.MercadoPagoTransaction);
                //Send email
            });
        }

        public void CancelPaymentOrder(int orderId, string reason)
        {
            var order = GetOrder(orderId);
            order.OrderPaymentStatus.Cancel(() =>
            {
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - Pago cancelado. Motivo: {reason}";
                order.PaymentReceived = false;
                order.Cancelled = true;
                _appDbContext.SaveChanges();
            },
            async () =>
            {
                await _mp.CancelPaymentAsync(order.MercadoPagoTransaction);
                //Send email
            });
        }

        public void PayOrder(int orderId)
        {
            var order = GetOrder(orderId);
            order.OrderPaymentStatus.Pay(() =>
            {
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - Dinero recibido.";
                order.Refunded = false;
                order.PaymentReceived = true;
                _appDbContext.SaveChanges();
            });
        }

        private Order CreateOrder(PaymentNotice payment)
        {
            Order order = this.GetOrderByBookingId(payment.BookingId);
            if (order != null)
                return order;

            order = _mapper.Map<PaymentNotice, Order>(payment);
            if(!String.IsNullOrEmpty(payment.User_Id))
            {
                var user = _userManager.FindByIdAsync(payment.User_Id);
                user.Wait();
                if (user.IsCompletedSuccessfully && user.Result != null)
                    order.Registration = user.Result;
            }

            order.OrderPlaced = _calendarRepository.LocalTime();
            order.CustomerComments = _shoppingCartRepository.ClearComments(payment.BookingId);

            order.DeliveryAddress = _shoppingCartRepository.GetDelivery(payment.BookingId);

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var shoppingItems = _shoppingCartRepository.EmptyItems(payment.BookingId);
            _appDbContext.OrderDetails.AddRange(shoppingItems.Select(x => new OrderDetail()
            {
                Amount = x.Amount,
                PieId = x.Pie.PieId,
                OrderId = order.OrderId,
                Price = x.Pie.Price
            }));
            _appDbContext.SaveChanges();

            order.PreparationTime = GetPreparationTime(order.OrderId);
            _appDbContext.SaveChanges();
            return order;
        }

        private int GetPreparationTime(int orderId)
        {
            int preparationTime = 0;
            var piesIds = _appDbContext.OrderDetails.Where(x => x.OrderId == orderId).Select(x => x.PieId).Distinct();
            var pieDetailIds = _appDbContext.Pies.Where(x => piesIds.Contains(x.PieId)).Select(x => x.PieDetailId).Distinct();
            preparationTime = _appDbContext.PieDetails.Where(x => pieDetailIds.Contains(x.PieDetailId)).Select(x => x.PreparationTime).OrderByDescending(x => x).FirstOrDefault();

            return preparationTime;
        }

        public Order OrderInProcess(PaymentNotice payment)
        {
            return CreateOrder(payment);
        }

        public Order OrderApproved(PaymentNotice payment)
        {
            Order order = OrderInProcess(payment);
            order.Status = payment.Status;
            order.PickUpTime = _calendarRepository.GetPickupEstimate(order.PreparationTime);
            _appDbContext.SaveChanges();
            return order;
        }

        public Order LatestReservationInProgress(ApplicationUser currentUser)
        {
            return _appDbContext.Orders.Include(x => x.Registration).FirstOrDefault(x => 
                    x.Registration == currentUser 
                    && x.OrderProgressState.GetType() == typeof(OrderInProgress)
                    && x.OrderPaymentStatus.GetType() == typeof(OrderReservationNotPaid));
        }
    }
}
