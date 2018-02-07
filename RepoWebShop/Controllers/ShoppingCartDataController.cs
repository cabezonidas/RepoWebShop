using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartDataController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly IMercadoPago _mp;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ShoppingCartDataController(IShoppingCartRepository shoppingCart, IMercadoPago mp, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _mp = mp;
            _userManager = userManager;
            _signInManager = signInManager;
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

        [HttpGet]
        [Route("GetMercadoPagoLink")]
        public async Task<IActionResult> GetMercadoPagoLink()
        {
            var _total = _shoppingCart.GetShoppingCartTotal();
            var _bookingId = _shoppingCart.GetShoppingCartId();
            var _friendlyBookingId = _bookingId.Length >= 6 ? _bookingId?.Substring(_bookingId.Length - 6, 6) ?? String.Empty : String.Empty;
            var _user = await _userManager.GetUser(_signInManager);

            return Ok(new { link = _mp.GetRepoPaymentLink(_total, _bookingId, _friendlyBookingId, Request.Host.ToString(), "La Reposteria", _user?.Id) });
        }
    }
}
