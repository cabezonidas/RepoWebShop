using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RepoWebShop.ViewModels;
using AutoMapper;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;
using System.Threading.Tasks;
using RepoWebShop.States;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Filters;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailRepository _emailRespository;
        private readonly IPrinterRepository _printer;
        private readonly IMapper _mp;
        private readonly ICalendarRepository _calendarRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ISmsRepository _smsRepository;
        private int _minimumArsForOrderDelivery;
        private int _maxArsForReservation;

        public OrderController(IPrinterRepository printer, ISmsRepository smsRepository, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ICalendarRepository calendarRepository, IMapper mp, IEmailRepository emailRespository, IOrderRepository orderRepository, IPieDetailRepository pieDetailRepository, IShoppingCartRepository shoppingCart, IAccountRepository accountRepository)
        {
            _printer = printer;
            _signInManager = signInManager;
            _userManager = userManager;
            _pieDetailRepository = pieDetailRepository;
            _orderRepository = orderRepository;
            _cartRepository = shoppingCart;
            _accountRepository = accountRepository;
            _emailRespository = emailRespository;
            _mp = mp;
            _calendarRepository = calendarRepository;
            _config = config;
            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
            _smsRepository = smsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EmailNotification(int id)
        {
            var viewModel = await _orderRepository.GetEmailDataAsync(id);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EmailSentNotification(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return View(order.Email);
        }

        [HttpGet]
        public async Task<IActionResult> Status(string id)
        {
            if (Request.QueryString.HasValue)
            {
                if(Request.Query.ContainsKey("preference_id"))
                    if(Request.Query["preference_id"].ToString() == _cartRepository.GetMpPreference(_cartRepository.GetSessionCartId()))
                        _cartRepository.RenewId();
            }

            Order order = await _orderRepository.GetOrderByFriendlyBookingId(id);
            OrderStatusViewModel orderstatus;
            if (order == null)
            {
                orderstatus = new OrderStatusViewModel()
                {
                    BookingId = id ?? string.Empty,
                    Notification = null,
                    Payment = null,
                    Progress = null
                };
            }
            else
            {
                var not = await _orderRepository.GetEmailDataAsync(order.OrderId);
                orderstatus = new OrderStatusViewModel()
                {
                    BookingId = id ?? string.Empty,
                    Notification = not,
                    Payment = order.OrderPaymentStatus,
                    Progress = order.OrderProgressState
                };
            }
            return View(orderstatus);
        }

        [HttpGet]
        [Route("[Controller]/Pending/{id}")]
        public async Task<IActionResult> Pending(string id)
        {
            if(Request.QueryString.HasValue)
            {
                _cartRepository.RenewId();
                await Task.Run(async () =>
                {
                    var apicall = $"http://{Request.Host.ToString()}/api/WebhooksData/OnPaymentNotNotified";
                    await new HttpClient().GetAsync(apicall);
                });
            }

            return View("Pending", id);
        }

        [HttpGet]
        [Route("[Controller]/OrderComplete/{id}")]
        public async Task<IActionResult> OrderComplete(int id)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            else
            {
                EmailNotificationViewModel viewModel = _orderRepository.ToEmailNotification(order);
                return View(viewModel);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("[Controller]/UpdateOrderWithReason/{subaction}/{id}")]
        public async Task<IActionResult> UpdateOrderWithReason(string subaction, int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            var orderViewModel = new UpdateOrderWithReasonViewModel();

            orderViewModel.Order = order;
            orderViewModel.OrderId = id;
            orderViewModel.Action = subaction;

            return View(orderViewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Cancel(UpdateOrderWithReasonViewModel orderCancelled)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.CancelOrderAsync(orderCancelled.OrderId, orderCancelled.Reason);
                return Redirect("/Order/Cancelled");
            }
            orderCancelled.Order = await _orderRepository.GetOrderByIdAsync(orderCancelled.OrderId);
            return View(orderCancelled);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CancelPayment(UpdateOrderWithReasonViewModel orderPaymentCancelled)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.CancelPaymentOrderAsync(orderPaymentCancelled.OrderId, orderPaymentCancelled.Reason);
                return Redirect("/Order/Cancelled");
            }
            orderPaymentCancelled.Order = await _orderRepository.GetOrderByIdAsync(orderPaymentCancelled.OrderId);
            return View(orderPaymentCancelled);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Refund(UpdateOrderWithReasonViewModel orderRefunded)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.RefundOrderAsync(orderRefunded.OrderId, orderRefunded.Reason);
                return Redirect("/Order/Refunded");
            }
            orderRefunded.Order = await _orderRepository.GetOrderByIdAsync(orderRefunded.OrderId);
            return View(orderRefunded);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Return(UpdateOrderWithReasonViewModel orderReturned)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.ReturnOrderAsync(orderReturned.OrderId, orderReturned.Reason);
                return Redirect("/Order/Returned");
            }
            orderReturned.Order = await _orderRepository.GetOrderByIdAsync(orderReturned.OrderId);
            return View(orderReturned);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InProgress() => View(await _orderRepository.GetOrdersInProgressAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Cancelled() => View(await _orderRepository.GetOrdersCancelledAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Completed() => View(await _orderRepository.GetOrdersCompletedAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PickedUp() => View(await _orderRepository.GetOrdersPickedUpAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Returned() => View(await _orderRepository.GetOrdersReturnedAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Refunded() => View(await _orderRepository.GetOrdersRefundedAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> NotYetPaid() => View(await _orderRepository.GetOrdersPickedUpWithPendingPaymentAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AllOrders() => View(await _orderRepository.GetAllAsync());

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Detail(int id)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id);
            if (order != null)
            {
                var items = order.OrderLines;
                var catalogItems = order.OrderCatalogItems;
                var caterings = order.OrderCaterings;
                var orderDetails = new OrderDetailsViewModel(order, items, catalogItems, caterings);
                return View(orderDetails);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUser(_signInManager);
            if (_cartRepository.GetTotal(null) > 0)
                if(user.PhoneNumberConfirmed)
                    return View(user);
                else
                    return Redirect($"/Account/VerifyNumber/Order/Checkout/");
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(Order order)
        {
 
            if (_cartRepository.GetTotal(null) <= 0)
            {
                ModelState.AddModelError("EmptyTrolley", "Tu carrito no puede estar vacío, agrega algunos productos.");
            }

            if (_cartRepository.GetTotal(null) > _maxArsForReservation)
                ModelState.AddModelError("MaxValueReached", "El monto de la reserva supera el límite admitido. Deberás usar Mercado Pago.");

            var _currentUser = await _userManager.GetUser(_signInManager);
            if(!_currentUser.PhoneNumberConfirmed)
            {
                ModelState.AddModelError("InvalidPhone", "El número de teléfono no esta verificado.");
                Redirect($"/Account/VerifyNumber/Order/Checkout/");
            }

            Order anotherReservationInProgress = _orderRepository.LatestReservationInProgress(_currentUser);
            if (anotherReservationInProgress != null)
            {
                var link = $"<a target='_blank' href='{Request.HostUrl()}/Order/Status/{anotherReservationInProgress.FriendlyBookingId}'>{anotherReservationInProgress.FriendlyBookingId}</a>";
                ModelState.AddModelError("ReservationInProgress", $"Deberás retirar el pedido {link} para poder reservar otra vez. Alternativamente, podés comprar con Mercado Pago.");
            }

            if (ModelState.IsValid)
            {
                order.Status = "reservation";
                order.Registration = _currentUser;
                order = await _orderRepository.CreateOrderAsync(order);
                await _orderRepository.AfterOrderConfirmedAsync(order);
                return Redirect($"/Order/Status/{order.FriendlyBookingId}");
            }
            return View(_currentUser);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CheckoutComplete()
        {
            var _currentUser = await _userManager.GetUser(_signInManager);
            var firstName = _currentUser?.FirstName ?? "Estimado/a";
            ViewBag.CheckoutCompleteMessage = firstName + ", gracias por tu reserva. Falta poco para que disfrutes de nuestras delicias!";

            return View();
        }
    }
}
