using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PaymentDataController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _cartSession;

        public PaymentDataController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _cartSession = shoppingCart;
        }

        [HttpGet]
        [Route("Management")]
        public IActionResult Management()
        {
            var orders = _orderRepository.GetAll();
            return Ok(orders);
        }
    }
}
