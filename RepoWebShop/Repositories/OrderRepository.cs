using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IShoppingCartRepository _cartRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ILunchRepository _lunchRep;
        private readonly IEmailRepository _emailRepository;
        private readonly ISmsRepository _smsRepository;
        private readonly IMercadoPago _mp;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string host;

        public OrderRepository(ILunchRepository lunchRep, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IEmailRepository emailRepository, ISmsRepository smsRepository, AppDbContext appDbContext, IMercadoPago mp, ICalendarRepository calendarRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _lunchRep = lunchRep;
            host = "http://" + contextAccessor.HttpContext?.Request.Host.ToString();
            _userManager = userManager;
            _emailRepository = emailRepository;
            _smsRepository = smsRepository;
            _mapper = mapper;
            _mp = mp;
            _appDbContext = appDbContext;
            _cartRepository = shoppingCartRepository;
            _calendarRepository = calendarRepository;
        }

        public void UpdateManagementComments(int orderId, string comments)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).ManagementComments = comments;
            _appDbContext.SaveChanges();
        }

        public void UpdatePickUpDate(int orderId, DateTime pickUp)
        {
            var result = _appDbContext.Orders.First(x => x.OrderId == orderId);
            result.PickUpTime = pickUp;
            result.PickUpTimeFrom = pickUp;
            _appDbContext.Orders.Update(result);
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
            if (friendlyBookingId == null)
                return null;
            return _appDbContext.Orders.Include(x => x.Registration).OrderByDescending(x => x.OrderId).FirstOrDefault(x => x.BookingId.EndsWith(friendlyBookingId));
        }

        public Order GetOrder(int id)
        {
            return _appDbContext.Orders.Include(x => x.Registration).Include(x => x.Discount).Include(x => x.Email).Include(x => x.DeliveryAddress).FirstOrDefault(x => x.OrderId == id);
        }

        public IEnumerable<OrderDetail> GetOrderDetails(int id)
        {
            return _appDbContext.OrderDetails.Include(x => x.Order.Email).Include(x => x.Pie).ThenInclude(x => x.PieDetail).Where(x => x.OrderId == id);
        }

        public IEnumerable<OrderCatalogItem> GetOrderCatalogItems(int id)
        {
            return _appDbContext.OrderCatalogItems.Include(x => x.Order).Include(x => x.Product).Where(x => x.Order.OrderId == id);
        }

        public IEnumerable<OrderCatering> GetOrderCaterings(int id)
        {
            return _appDbContext.OrderCaterings.Include(x => x.Order).Include(x => x.Lunch).Where(x => x.Order.OrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _appDbContext.Orders
                .Include(x => x.Registration)
                .Include(x => x.OrderLines)
                .Include(x => x.OrderCatalogItems)
                .Include(x => x.OrderCaterings)
                .Include(x => x.DeliveryAddress)
                .Include(x => x.Discount)
                .Where(o => o.Status != "draft").ToList();

            foreach (var order in orders)
            {
                foreach (var orderLine in order.OrderLines)
                {
                    orderLine.Pie = _appDbContext.Pies.First(x => x.PieId == orderLine.PieId);
                    orderLine.Pie.PieDetail = _appDbContext.PieDetails.First(x => x.PieDetailId == orderLine.Pie.PieDetailId);
                }
                foreach (var orderLine in order.OrderCatalogItems)
                {
                    orderLine.Product = _appDbContext.Products.First(x => x.ProductId == orderLine.ProductId);
                }
                foreach (var catering in order.OrderCaterings)
                {
                    catering.Lunch = _appDbContext.Lunch.First(x => x.LunchId == catering.LunchId);
                }
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersInProgress()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderInProgress)
            || x.OrderProgressState.GetType() == typeof(OrderComplete));
        }

        public IEnumerable<Order> GetOrdersCancelled() => GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderCancelled));

        public IEnumerable<Order> GetOrdersCompleted() => GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderComplete));

        public IEnumerable<Order> GetOrdersPickedUp() => GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderPickedUp));

        public IEnumerable<Order> GetOrdersReturned() => GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderReturned));

        public IEnumerable<Order> GetOrdersRefunded() => GetAll().Where(x => x.Refunded);

        public IEnumerable<Order> GetOrdersPickedUpWithPendingPayment()
        {
            return GetAll().Where(x => x.OrderProgressState.GetType() == typeof(OrderPickedUp)
                    && (x.OrderPaymentStatus.GetType() == typeof(OrderMercadoPagoNotPaid)
                        || x.OrderPaymentStatus.GetType() == typeof(OrderReservationNotPaid))
            );
        }

        public Order CreateOrder(Order order)
        {
            order.BookingId = order.BookingId ?? _cartRepository.GetSessionCartId();
            order.PhoneNumber = order.Registration?.PhoneNumber;
            order.OrderTotal = _cartRepository.GetTotal(order.BookingId);
            order.Discount = _cartRepository.GetDiscount(order.BookingId);
            order.DeliveryAddress = _cartRepository.GetDelivery(order.BookingId);
            order.CustomerComments = _cartRepository.GetComments(order.BookingId)?.Comments;
            order.PreparationTime = _cartRepository.GetPreparationTime(order.BookingId);
            order.PickUpTime = _calendarRepository.GetPickupEstimate(order.PreparationTime);
            order.OrderPlaced = _calendarRepository.LocalTime();
            order.PickedUp = false;

            var pickUpTime = _cartRepository.GetPickUpDate(order.BookingId);
            order.PickUpTimeFrom = pickUpTime.From;
            order.TimeLeftUntilStoreCloses = pickUpTime.To;

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();
         
            /**********************/
            
            var shoppingItems = _cartRepository.GetItems(order.BookingId);
            _appDbContext.OrderDetails.AddRange(shoppingItems.Select(x => new OrderDetail()
            {
                Amount = x.Amount,
                PieId = x.Pie.PieId,
                OrderId = order.OrderId,
                Price = x.Pie.Price
            }));
            _appDbContext.SaveChanges();

            /**********************/

            var shoppingProductItems = _cartRepository.GetCatalogItems(order.BookingId);
            _appDbContext.OrderCatalogItems.AddRange(shoppingProductItems.Select(x => new OrderCatalogItem()
            {
                Amount = x.Amount,
                Price = x.Product.Price,
                OrderId = order.OrderId,
                ProductId = x.Product.ProductId,
                Product = x.Product
            }));
            _appDbContext.SaveChanges();

            /**********************/

            var shoppingLunches = _cartRepository.GetShoppingCaterings(order.BookingId).Select(x => _mapper.Map<ShoppingCartComboCatering, OrderCatering>(x)).ToList();
            foreach(var lunch in shoppingLunches)
            {
                lunch.Order = order;
                lunch.OrderId = order.OrderId;
            }
            _appDbContext.OrderCaterings.AddRange(shoppingLunches);
            _appDbContext.SaveChanges();

            /**********************/

            _cartRepository.ClearCart(order.BookingId);

            return order;
        }

        public EmailNotificationViewModel GetEmailData(int id)
        {
            var order = GetOrder(id);
            EmailNotificationViewModel emailData = ToEmailNotification(order);
            return emailData;
        }

        public EmailNotificationViewModel ToEmailNotification(Order order)
        {
            var orderDetails = GetOrderDetails(order.OrderId);
            var orderCatalogItems = GetOrderCatalogItems(order.OrderId);
            var orderCaterings = GetOrderCaterings(order.OrderId);
            var emailData = new EmailNotificationViewModel();
            emailData.AbsoluteUrl = host;
            emailData.Comments = order.CustomerComments;
            emailData.MercadoPagoTransaction = order.MercadoPagoTransaction;
            emailData.OrderItems = orderDetails;
            emailData.OrderCatalogItems = orderCatalogItems;
            emailData.OrderCaterings = orderCaterings;
            emailData.OrderReady = order.PickUpTimeFrom ?? order.PickUpTime;
            emailData.TimeLeftUntilStoreCloses = order.TimeLeftUntilStoreCloses;
            emailData.OrderTotal = order.OrderTotal; //Without MP interests
            emailData.OrderType = String.IsNullOrEmpty(order.MercadoPagoTransaction) ? "Reserva" : "Compra";
            emailData.PreparationTime = order.PreparationTime;
            emailData.FriendlyBookingId = order.FriendlyBookingId;
            emailData.OrderId = order.OrderId.ToString();
            emailData.Delivery = order.DeliveryAddress;
            emailData.Discount = order.Discount;

            emailData.CustomarAlias = order.Registration == null ? order.MercadoPagoName : order.Registration.FirstName;
            emailData.CustomarAlias = Regex.Replace(emailData.CustomarAlias.ToLower(), @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            return emailData;
        }

        public void CompleteOrder(int orderId)
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
                        $"{user.FirstName}, ¡Tu pedido {order.FriendlyBookingId} ya está listo! De las Artes.");
                }
                _emailRepository.NotifyOrderCompleteAsync(order);
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

        private int GetPreparationTime(int orderId)
        {
            int preparationTime = 0;
            var piesIds = _appDbContext.OrderDetails.Where(x => x.OrderId == orderId).Select(x => x.PieId).Distinct();
            var pieDetailIds = _appDbContext.Pies.Where(x => piesIds.Contains(x.PieId)).Select(x => x.PieDetailId).Distinct();
            preparationTime = _appDbContext.PieDetails.Where(x => pieDetailIds.Contains(x.PieDetailId)).Select(x => x.PreparationTime).OrderByDescending(x => x).FirstOrDefault();

            return preparationTime;
        }
       
        public async Task<Order> PaymentNotified(PaymentNotice payment)
        {
            Order order = GetOrderByBookingId(payment.BookingId);

            var orderStatusBefore = order?.Status;

            if (order == null)
                if(payment.Status == "approved" || payment.Status == "in_process")
                {
                    order = _mapper.Map<PaymentNotice, Order>(payment);
                    order.Registration = await _userManager.FindByIdAsync(payment.User_Id ?? string.Empty);
                    order = CreateOrder(order);
                }

            if(order != null && orderStatusBefore != "approved" && (order.Status == "approved" || payment.Status == "approved"))
            {
                order.PickUpTime = _calendarRepository.GetPickupEstimate(order.PreparationTime);
                order.Status = "approved";
                order.Payout = _calendarRepository.LocalTime();
                _appDbContext.Orders.Update(order);
                _appDbContext.SaveChanges();
                await _emailRepository.SendOrderConfirmationAsync(order,
                () => _smsRepository.NotifyAdmins($"¡Pedido nuevo! Código {order.FriendlyBookingId}"));
            }

            return order;
        }

        public Order LatestReservationInProgress(ApplicationUser currentUser)
        {
            return _appDbContext.Orders.Include(x => x.Registration).FirstOrDefault(x => 
                    x.Registration == currentUser 
                    && x.OrderProgressState.GetType() == typeof(OrderInProgress)
                    && x.OrderPaymentStatus.GetType() == typeof(OrderReservationNotPaid));
        }

        public IEnumerable<Order> GetByUserOrders(ApplicationUser user) => _appDbContext.Orders.Where(x => x.Registration == user).Include(x => x.Email);

        public bool ValidBookingId(string bookingId)
        {
            return _appDbContext.ShoppingCartItems.Any(x => x.ShoppingCartId == bookingId)
                || _appDbContext.ShoppingCartCaterings.Any(x => x.BookingId == bookingId)
                || _appDbContext.ShoppingCartCatalogProducts.Any(x => x.ShoppingCartId == bookingId)
                || _appDbContext.Orders.Any(x => x.BookingId == bookingId);
        }
    }
}
