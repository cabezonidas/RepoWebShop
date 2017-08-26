using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartDataController : Controller
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartDataController(ShoppingCart shoppingCart)
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
    }
}
