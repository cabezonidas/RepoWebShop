using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IO;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderRepository orderRepository, IPieDetailRepository pieDetailRepository, ShoppingCart shoppingCart, UserManager<IdentityUser> userManager)
        {
            _pieDetailRepository = pieDetailRepository;
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        public IActionResult Status(string id)
        {
            Order order = _orderRepository.GetOrderByBookingId(id);

            string statusCode = order?.Status;

            string status = string.Empty;
            string description = string.Empty;

            switch (statusCode)
            {
                case "approved":
                    status = "Aprobado";
                    description = "Gracias! Tu compra ya fue aceptada y ya estamos trabajando para que disfrutes de nuestras mejores delicias.";
                    break;
                case "pending":
                    status = "Pendiente";
                    description = "Tu compra todavía no fue aceptada. Está pendiente.";
                    break;
                case "in_process":
                    status = "En proceso";
                    description = "La acreditacion aún está en proceso.";
                    break;
                case "rejected":
                    status = "Rechazado";
                    description = "Lamentablemente la transacción fue rechazada. Podrías intentar con otros medios de pago.";
                    break;
                case "draft":
                    status = "Sin confirmación";
                    description = "Lamentablemente no hemos recibido detalles de pago de esta reserva.";
                    break;
                default:
                    status = "No encontrado";
                    description = "El código que buscas no se encuentra en nuestra base de datos.";
                    break;
            }

            OrderStatusViewModel orderstatus = new OrderStatusViewModel()
            {
                BookingId = id ?? string.Empty,
                Status = status,
                Description = description
            };

            return View(orderstatus);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Management()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Detail(int id)
        {
            Order order = _orderRepository.GetOrder(id);
            if (order != null)
            {
                var items = _orderRepository.GetOrderDetails(order.OrderId);

                OrderDetailsViewModel orderDetails = new OrderDetailsViewModel()
                {
                    Order = order,
                    Items = items
                };

                return View(orderDetails);
            }
            else
                return NotFound();
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Tu carrito no puede estar vacío, agrega algunos productos.");
            }

            if (ModelState.IsValid)
            {
                order.OrderTotal = _shoppingCart.ShoppingCartItems.Select(x => x.Amount * x.Pie.Price).Sum();
                order.Registration = GetCurrentUser();
                order.CustomerComments = _shoppingCart.GetShoppingCartComments();
                order.BookingId = Path.GetRandomFileName().Substring(0,6).ToUpper();
                order.Status = "reservation";
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();

                //Mandar mail al cliente con el codigo y los detalles
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }
        
        [Authorize]
        public IActionResult CheckoutComplete()
        {
            var firstName = GetCurrentUser()?.FirstName ?? "Estimado/a";
            ViewBag.CheckoutCompleteMessage = firstName + ", gracias por tu reserva. Falta poco para que disfrutes de nuestras delicias!";

            return View();
        }

        private Registration GetCurrentUser()
        {
            return (_userManager.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name) as Registration);
        }
    }
}
