using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartDataController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCart;

        public ShoppingCartDataController(IShoppingCartRepository shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        [Route("AddComments")]
        public IActionResult AddComments()
        {
            _shoppingCart.AddComments(string.Empty);
            return Ok();
        }

        [HttpGet]
        [Route("AddComments/{comments}")]
        public IActionResult AddComments(string comments)
        {
            _shoppingCart.AddComments(comments);
            return Ok();
        }

        [HttpGet]
        [Route("GetComments")]
        public IActionResult GetComments()
        {
            return Ok(new { comments = _shoppingCart.GetShoppingCartComments() });
        }

        [HttpGet]
        [Route("GetItemsCount")]
        public IActionResult GetItemsCount()
        {
            return Ok(new { items = _shoppingCart.GetShoppingCartItems().Count });
        }
    }
}
