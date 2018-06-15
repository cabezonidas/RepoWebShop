using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Management()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(orders);
        }
    }
}
