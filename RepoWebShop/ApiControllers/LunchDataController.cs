using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RepoWebShop.ApiControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class LunchDataController : Controller
    {
        private readonly ILunchRepository _lunchRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IPrinterRepository _printer;

        public LunchDataController(IPrinterRepository printer, IShoppingCartRepository cartRepository, SignInManager<ApplicationUser> signIn, UserManager<ApplicationUser> userMana, IMapper mapper, ILunchRepository lunchRepository, ICatalogRepository catalogRepository)
        {
            _printer = printer;
            _cartRepository = cartRepository;
            _userManager = userMana;
            _signIn = signIn;
            _mapper = mapper;
            _lunchRepository = lunchRepository;
            _catalogRepository = catalogRepository;
        }

        [Route("PrintInStoreCatering/{id}")]
        [HttpPost]
        public async Task<IActionResult> PrintInStoreCatering(int id)
        {
            var lunch = await _lunchRepository.GetLunchByIdAsync(id);
            var lunchVm = _mapper.Map<Lunch, LunchTicketViewModel>(lunch);
            lunchVm.InStore = true;
            var view = "TicketCatering";
            var result = await this.RenderViewAsync(view, lunchVm, true);

            _printer.AddToQueue(result);
            return Ok();
        }

        [Route("PrintOnlineCatering/{id}")]
        [HttpPost]
        public async Task<IActionResult> PrintOnlineCatering(int id)
        {
            var lunch = await _lunchRepository.GetLunchByIdAsync(id);
            var lunchVm = _mapper.Map<Lunch, LunchTicketViewModel>(lunch);
            lunchVm.InStore = false;
            var view = "TicketCatering";
            var result = await this.RenderViewAsync(view, lunchVm, true);

            _printer.AddToQueue(result);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddProductInstance/{productId}")]
        public async Task<IActionResult> AddProductInstance(int productId)
        {
            var sessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.AddItemInstanceAsync(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddProduct/{productId}")]
        public async Task<IActionResult> AddProduct(int productId)
        {
            var sessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.AddItemAsync(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            var result = _catalogRepository.GetAll();
            result = result.OrderBy(x => x.DisplayName.TrimStart());
            result = result.OrderBy(x => x.Category);
            result = result.ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLunchItem/{itemId}")]
        public IActionResult GetLunchItem(int itemId)
        {
            var sessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var result = sessionLunch.Lunch.Items.First(x => x.LunchItemId == itemId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpGet]
        [Route("GetLunch")]
        public IActionResult GetLunch()
        {
            var result = _cartRepository.GetSessionLunch();
            var response = Json(result.Lunch);
            return response;
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveProductInstance/{productId}")]
        public async Task<IActionResult> RemoveProductInstance(int productId)
        {
            var sessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.RemoveItemInstanceAsync(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveProduct/{productId}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var sessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.RemoveItemAsync(sessionLunch.Lunch.LunchId, productId);
            var route = "~/Views/Lunch/ItemDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [Route("SaveLunch")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveLunch()
        {
            var user = await _userManager.GetUser(_signIn);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Administrator"))
                return Ok();
            else
                return Ok(_lunchRepository.SaveLunch());
        }

        [HttpPost]
        [Route("AddMiscellaneous/{description}/{price}")]
        public async Task<IActionResult> AddMiscellaneous(string description, decimal price)
        {
            description = description.Replace("__", "/");
            var lunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.AddMiscellaneousAsync(lunch.Lunch.LunchId, description, price);
            return Ok(result.LunchMiscellaneousId);
        }

        [HttpGet]
        [Route("GetMiscellaneous/{id}")]
        public IActionResult GetMiscellaneous(int id)
        {
            LunchMiscellaneous result = _lunchRepository.GetMiscellaneous(id);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpPost]
        [Route("AddMiscellaneousInstance/{miscellaneousId}")]
        public async Task<IActionResult> AddMiscellaneousInstance(int miscellaneousId)
        {
            var lunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.AddMiscellaneousInstanceAsync(lunch.Lunch.LunchId, miscellaneousId);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [Route("RemoveMiscellaneousInstance/{miscellaneousId}")]
        public async Task<IActionResult> RemoveMiscellaneousInstance(int miscellaneousId)
        {
            var lunch = _cartRepository.GetOrCreateSessionLunch();
            var result = await _lunchRepository.RemoveMiscellaneousInstanceAsync(lunch.Lunch.LunchId, miscellaneousId);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, result);
        }

        [HttpDelete]
        [Route("RemoveMiscellaneous/{miscellaneousId}")]
        public async Task<IActionResult> RemoveMiscellaneous(int miscellaneousId)
        {
            var lunch = _cartRepository.GetOrCreateSessionLunch();
            await _lunchRepository.RemoveMiscellaneousAsync(lunch.Lunch.LunchId, miscellaneousId);
            var route = "~/Views/Lunch/MiscellaneousDetail.cshtml";
            return PartialView(route, null);
        }

        [HttpGet]
        [Route("GetTotals")]
        [AllowAnonymous]
        public IActionResult GetTotals()
        {
            var lunch = _cartRepository.GetOrCreateSessionLunch();
            var route = "~/Views/Lunch/LunchTotals.cshtml";
            return PartialView(route, lunch.Lunch);
        }
    }
}
