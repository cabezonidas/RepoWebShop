using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ApiControllers
{
    [Route("api/[controller]")]
    public class ShoppingCartDataController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMercadoPago _mp;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        private readonly IElectronicBillingRepository _billing;

        public ShoppingCartDataController(IElectronicBillingRepository billing, ICalendarRepository calendarRepository, AppDbContext appDbContext, IShoppingCartRepository shoppingCart, IMercadoPago mp, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _billing = billing;
            _calendarRepository = calendarRepository;
            _appDbContext = appDbContext;
            _mp = mp;
            _userManager = userManager;
            _signInManager = signInManager;
            _cartRepository = shoppingCart;
        }

		[HttpGet]
		[Route("GetProductItems")]
		public IEnumerable<ShoppingCartCatalogItem> GetProductItems() => _cartRepository.GetCatalogItems(null);

		[HttpGet]
		[Route("GetCaterings")]
		public IEnumerable<ShoppingCartComboCatering> GetCaterings() => _cartRepository.GetShoppingCaterings(null);

		[HttpPost]
		[Route("AddProductItem")]
		public IActionResult AddProductItem([FromBody] int id)
		{
			_cartRepository.AddCatalogItemToCart(id);
			return Ok();
		}

		[HttpPost]
        [Route("AcknowledgeSystemTime")]
        public IActionResult AcknowledgeSystemTime()
        {
            _cartRepository.AcknowledgeSystemTime(null);
            
            return Ok();
        }

        [HttpGet]
        [Route("GetCuit/{cuit}")]
        public async Task<IActionResult> GetCuit(long cuit)
        {
            var validCuit = await _billing.ValidPersonaAsync(cuit);
            if (validCuit.Valid)
            {
                _cartRepository.AddCuitToCart(null, cuit);
                return Ok(validCuit.CuitDetails.Select(x => new { detail = x.Property, value = x.Value }));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("ClearCuit")]
        public IActionResult ClearCuit()
        {
            _cartRepository.RemoveCuitFromCart(null);
            return Ok();
        }

        [HttpPost]
        [Route("SetPickUpTime/{datefrom}")]
        public IActionResult SetPickUpTime(long datefrom)
        {
            var pickUpDate = new DateTime(datefrom);
            string error = "";
            bool validPickUpDate = _cartRepository.TrySetPickUpDate(null, pickUpDate, out error);
            if (validPickUpDate)
                return Ok();
            else
                return BadRequest(new { error });
        }

        [HttpGet]
        [Route("GetTimeSlots")]
        public IActionResult GetTimeSlots()
        {
            PickUpTimeViewModel result = _cartRepository.GetTimeSlots(null);
            var route = "~/Views/ShoppingCart/PickUpTime.cshtml";
            return PartialView(route, result);
        }

        [HttpGet]
        [Route("AddComments")]
        public IActionResult AddComments()
        {
            _cartRepository.AddComments(string.Empty);
            return Ok();
        }

        [HttpGet]
        [Route("AddComments/{*comments}")]
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
            return Ok(new { comments = _cartRepository.GetComments(null)?.Comments ?? string.Empty });
        }

        [HttpGet]
        [Route("CountTrolleyObjects")]
        public IActionResult CountTrolleyObjects()
        {
            return Ok(new { items = _cartRepository.CountTrolleyObjects(null) });
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
            
            var _friendlyBookingId = bookingId.Ending(6);

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
                if (response != null && response["id"] != null)
                    _cartRepository.SetMpPreference(response["id"].ToString());
                else
                    return BadRequest();
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
