using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartDataController : Controller
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMercadoPago _mp;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public ShoppingCartDataController(AppDbContext appDbContext, IShoppingCartRepository shoppingCart, IMercadoPago mp, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _appDbContext = appDbContext;
            _mp = mp;
            _userManager = userManager;
            _signInManager = signInManager;
            _cartRepository = shoppingCart;
        }

        [HttpGet]
        [Route("AddComments")]
        public IActionResult AddComments()
        {
            _cartRepository.AddComments(string.Empty);
            return Ok();
        }

        [HttpGet]
        [Route("AddComments/{comments}")]
        public IActionResult AddComments(string comments)
        {
            comments = (string.IsNullOrEmpty(comments) || comments == "undefined") ? string.Empty : comments;
            _cartRepository.AddComments(comments);
            return Ok();
        }

        [HttpGet]
        [Route("GetComments")]
        public IActionResult GetComments()
        {
            return Ok(new { comments = _cartRepository.GetComments()?.Comments ?? string.Empty });
        }

        [HttpGet]
        [Route("GetItemsCount")]
        public IActionResult GetItemsCount()
        {
            return Ok(new { items = _cartRepository.GetItems().Count() });
        }

        [HttpGet]
        [Route("GetMercadoPagoLink")]
        public async Task<IActionResult> GetMercadoPagoLink() => await GetMercadoPagoLink(null);

        [HttpGet]
        [Route("GetMercadoPagoLink/{bookingId}")]
        public async Task<IActionResult> GetMercadoPagoLink(string bookingId)
        {
            var _user = string.IsNullOrEmpty(bookingId) ? await _userManager.GetUser(_signInManager) : null; 
            bookingId = string.IsNullOrEmpty(bookingId) ? _cartRepository.GetSessionCartId() : bookingId;

            var _total = _cartRepository.GetTotal(bookingId);
            
            var _friendlyBookingId = bookingId.Length >= 6 ? bookingId?.Substring(bookingId.Length - 6, 6) ?? String.Empty : String.Empty;

            var preference = _mp.BuildPreference(_total, bookingId, _friendlyBookingId, Request.Host.ToString(), "De las Artes", _user?.Id);

            string preferenceId = _cartRepository.GetMpPreference(bookingId);
            bool validPreference = true;
            string init_point;
            Hashtable response;

            if (!String.IsNullOrEmpty(preferenceId))
            {
                response = ((await _mp.GetPreferenceAsync(preferenceId))["response"] as Hashtable);
                var bookId = response["external_reference"].ToString();
                //validPreference = _appDbContext.ShoppingCartItems.Any(x => x.ShoppingCartId == bookingId);
                validPreference = bookId == bookingId;
            }

            if(String.IsNullOrEmpty(preferenceId) || !validPreference)
            {
                response = ((await _mp.CreatePreferenceAsync(preference))["response"] as Hashtable);
                _cartRepository.SetMpPreference(response["id"].ToString());
            }
            else
            {
                response = ((await _mp.UpdatePreferenceAsync(preferenceId, preference))["response"] as Hashtable);
            }

            init_point = response["init_point"].ToString();

            return Ok(new { link = init_point });
        }
    }
}
