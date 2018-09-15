using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IElectronicBillingRepository _billing;
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ILunchRepository _lunchRep;
        private readonly IEmailRepository _emailRepository;
        private readonly ISmsRepository _smsRepository;
        private readonly IMercadoPago _mp;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly IPrinterRepository _printer;
        private readonly string host;

        public OrderRepository(IElectronicBillingRepository billing, IHostingEnvironment env, IPrinterRepository printer, ILunchRepository lunchRep, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IEmailRepository emailRepository, ISmsRepository smsRepository, AppDbContext appDbContext, IMercadoPago mp, ICalendarRepository calendarRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _billing = billing;
            _printer = printer;
            _env = env;
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
            result.TimeLeftUntilStoreCloses = null;

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

        public async Task<IEnumerable<Order>> GetAllAsync(Func<Order, bool> condition = null)
        {
            IEnumerable<Order> orders = await _appDbContext.Orders
                .Where(x => (condition == null || condition(x)) && x.Status != "draft")
                .Include(x => x.Factura)
                .Include(x => x.Factura.InvoiceDetails)
                .Include(x => x.Factura.Caes)
                .Include(x => x.Email)
                .Include(x => x.Registration)
                .Include(x => x.OrderLines)
                .Include(x => x.DeliveryAddress)
                .Include(x => x.Discount)
                .Include(x => x.OrderCatalogItems)
                .Include(x => x.OrderCaterings)
                .ToArrayAsync();

            foreach (var order in orders)
            {
                foreach (var orderLine in order.OrderLines)
                    orderLine.Pie = _appDbContext.Pies.Include(x => x.PieDetail).First(x => x.PieId == orderLine.PieId);

                foreach (var orderLine in order.OrderCatalogItems)
                    orderLine.Product = _appDbContext.Products.Include(x => x.PieDetail).First(x => x.ProductId == orderLine.ProductId);

                foreach (var catering in order.OrderCaterings)
                    catering.Lunch = (await _lunchRep.GetAllLunchesAsync(x => x.LunchId == catering.LunchId)).FirstOrDefault();
            }

            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersInProgressAsync() =>
            await GetAllAsync(x => x.OrderProgressState.GetType() == typeof(OrderInProgress) || x.OrderProgressState.GetType() == typeof(OrderComplete));

        public async Task<IEnumerable<Order>> GetOrdersCancelledAsync() => await GetAllAsync(x => x.OrderProgressState.GetType() == typeof(OrderCancelled));

        public async Task<IEnumerable<Order>> GetOrdersCompletedAsync() => await GetAllAsync(x => x.OrderProgressState.GetType() == typeof(OrderComplete));

        public async Task<IEnumerable<Order>> GetOrdersPickedUpAsync() => await GetAllAsync(x => x.OrderProgressState.GetType() == typeof(OrderPickedUp));

        public async Task<IEnumerable<Order>> GetOrdersReturnedAsync() => await GetAllAsync(x => x.OrderProgressState.GetType() == typeof(OrderReturned));

        public async Task<IEnumerable<Order>> GetOrdersRefundedAsync() => await GetAllAsync(x => x.Refunded);

        public async Task<IEnumerable<Order>> GetOrdersPickedUpWithPendingPaymentAsync() =>
            await GetAllAsync(x => x.OrderProgressState.GetType() == typeof(OrderPickedUp)
                && (x.OrderPaymentStatus.GetType() == typeof(OrderMercadoPagoNotPaid) || x.OrderPaymentStatus.GetType() == typeof(OrderReservationNotPaid)));

        public Order CreateOrder(Order order)
        {
            var customLunch = _cartRepository.GetSessionLunchIfNotEmpty(order.BookingId);
            if (customLunch != null)
            {
                _lunchRep.SaveLunch(order.BookingId);
                _cartRepository.AddCateringToOrder(customLunch);
            }

            order.BookingId = order.BookingId ?? _cartRepository.GetSessionCartId();

            order.Cuit = _cartRepository.GetCuit(order.BookingId);

            order.PhoneNumber = order.Registration?.PhoneNumber;
            order.OrderTotal = _cartRepository.GetTotal(order.BookingId);
            order.TotalInStore = _cartRepository.GetTotalInStore(order.BookingId);
            order.Discount = _cartRepository.GetDiscount(order.BookingId);
            order.DeliveryAddress = _cartRepository.GetDelivery(order.BookingId);
            order.CustomerComments = _cartRepository.GetComments(order.BookingId)?.Comments;
            order.PreparationTime = _cartRepository.GetPreparationTime(order.BookingId);
            order.OrderPlaced = _calendarRepository.LocalTime();
            order.PickedUp = false;

            var pickUpTime = _cartRepository.GetPickUpDate(order.BookingId);
            order.PickUpTimeFrom = pickUpTime.From;
            order.TimeLeftUntilStoreCloses = pickUpTime.To;
            order.PickUpTime = order.PickUpTimeFrom;// _calendarRepository.GetPickupEstimate(order.PreparationTime);

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

            var shoppingLunches = _cartRepository.GetShoppingCaterings(order.BookingId).Select(x => _mapper.Map<ShoppingCartComboCatering, OrderCatering>(x)).ToArray();
            foreach (var lunch in shoppingLunches)
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

        public async Task<EmailNotificationViewModel> GetEmailDataAsync(int id)
        {
            var order = (await GetAllAsync(x => x.OrderId == id)).FirstOrDefault();
            EmailNotificationViewModel emailData = ToEmailNotification(order);
            return emailData;
        }

        public async Task<Order> GetOrderByIdAsync(int id) => (await GetAllAsync(x => x.OrderId == id)).FirstOrDefault();

        public async Task<Order> GetOrderByBookingIdAsync(string id) => (await GetAllAsync(x => x.BookingId == id)).FirstOrDefault();

        public async Task<Order> GetOrderByFriendlyBookingId(string friendlyId) => (await GetAllAsync(x => x.BookingId.EndsWith(friendlyId))).LastOrDefault();

        public EmailNotificationViewModel ToEmailNotification(Order order)
        {
            var emailData = new EmailNotificationViewModel();
            emailData.AbsoluteUrl = host;
            emailData.Comments = order.CustomerComments;
            emailData.MercadoPagoTransaction = order.MercadoPagoTransaction;
            emailData.OrderItems = order.OrderLines;
            emailData.OrderCatalogItems = order.OrderCatalogItems;
            emailData.OrderCaterings = order.OrderCaterings;
            emailData.OrderReady = order.PickUpTimeFrom ?? order.PickUpTime;
            emailData.TimeLeftUntilStoreCloses = order.TimeLeftUntilStoreCloses;
            emailData.OrderTotal = order.OrderTotal; //Without MP interests
            emailData.TotalInStore = order.TotalInStore;
            emailData.OrderType = String.IsNullOrEmpty(order.MercadoPagoTransaction) ? "Reserva" : "Compra";
            emailData.PreparationTime = order.PreparationTime;
            emailData.FriendlyBookingId = order.FriendlyBookingId;
            emailData.OrderId = order.OrderId.ToString();
            emailData.Delivery = order.DeliveryAddress;
            emailData.Discount = order.Discount;
            emailData.Factura = order.Factura;

            emailData.CustomarAlias = order.Registration == null ? order.MercadoPagoName : order.Registration.FirstName;
            emailData.CustomarAlias = Regex.Replace(emailData.CustomarAlias.ToLower(), @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            return emailData;
        }

        public async Task CompleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            order.OrderProgressState.Complete(() =>
            {
                order.Finished = true;
                var eventDesc = $"Orden lista.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}";
                _appDbContext.SaveChanges();
            },
            async () =>
            {
                if (_env.IsProduction())
                {
                    if (order.Registration != null && order.Registration.PhoneNumberConfirmed)
                    {
                        var user = order.Registration;
                        await _smsRepository.SendSms(user.PhoneNumber,
                            $"{user.FirstName}, ¡Tu pedido {order.FriendlyBookingId} ya está listo! De las Artes.");
                    }
                    await _emailRepository.NotifyOrderCompleteAsync(order);
                }
            });
        }

        public async Task PickUpOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            order.OrderProgressState.PickUp(() =>
            {
                order.Finished = true;
                order.PickedUp = true;
                var eventDesc = $"Orden retirada.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}";
                _appDbContext.SaveChanges();
            });
        }

        public async Task CancelOrderAsync(int orderId, string reason)
        {
            var order = await GetOrderByIdAsync(orderId);
            order.OrderProgressState.Cancel(() =>
            {
                order.Cancelled = true;
                var eventDesc = $"Orden cancelada.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}. Motivo: {reason}";
                _appDbContext.SaveChanges();
                //Send mail or text
            });
        }

        public async Task ReturnOrderAsync(int orderId, string reason)
        {
            var order = await GetOrderByIdAsync(orderId);
            order.OrderProgressState.Return(() =>
            {
                order.Returned = true;
                var eventDesc = $"Orden retornada.";
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - {eventDesc}. Motivo: {reason}";
                _appDbContext.SaveChanges();
            });
        }

        public async Task RefundOrderAsync(int orderId, string reason)
        {
            var order = await GetOrderByIdAsync(orderId);
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

        public async Task CancelPaymentOrderAsync(int orderId, string reason)
        {
            var order = await GetOrderByIdAsync(orderId);
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

        public async Task PayOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            order.OrderPaymentStatus.Pay(() =>
            {
                order.OrderHistory += $"\r\n{_calendarRepository.LocalTimeAsString()} - Dinero recibido.";
                order.Refunded = false;
                order.PaymentReceived = true;
                _appDbContext.SaveChanges();
            });
        }

        public async Task<Order> PaymentNotifiedAsync(PaymentNotice payment)
        {
            Order order = await GetOrderByBookingIdAsync(payment.BookingId);

            var orderStatusBefore = order?.Status;

            if (order == null)
                if (payment.Status == "approved" || payment.Status == "in_process")
                {
                    order = _mapper.Map<PaymentNotice, Order>(payment);
                    order.Registration = await _userManager.FindByIdAsync(payment.User_Id ?? string.Empty);
                    order = CreateOrder(order);
                }

            if (order != null && orderStatusBefore != "approved" && (order.Status == "approved" || payment.Status == "approved"))
            {
                order.PickUpTime = _calendarRepository.GetPickupEstimate(order.PreparationTime);
                order.Status = "approved";
                order.Payout = _calendarRepository.LocalTime();
                order.Factura = await _billing.Facturar(order);
                _appDbContext.Orders.Update(order);
                _appDbContext.SaveChanges();
                await AfterOrderConfirmedAsync(order);
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

        public async Task<IEnumerable<Order>> GetByUserOrdersAsync(ApplicationUser user) => await GetAllAsync(x => x.Registration == user);

        public bool ValidBookingId(string bookingId)
        {
            return _cartRepository.GetTotal(bookingId) > 0
                || _appDbContext.Orders.Any(x => x.BookingId == bookingId);
        }

        public async Task AfterOrderConfirmedAsync(Order order)
        {
            await _printer.AddToQueueAsync(order.OrderId);
            await _emailRepository.SendOrderConfirmationAsync(order);
            await _smsRepository.NotifyAdminsAsync($"¡Pedido nuevo! Código {order.FriendlyBookingId}");
        }
	}
}
