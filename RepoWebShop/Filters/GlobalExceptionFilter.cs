using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendar;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GlobalExceptionFilter(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, ICalendarRepository calendar, ILogger<GlobalExceptionFilter> exceptionLogger, AppDbContext appDbContext, IShoppingCartRepository shoppingCartRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _calendar = calendar;
            _shoppingCartRepository = shoppingCartRepository;
            _logger = exceptionLogger;
            _appDbContext = appDbContext;
        }

        public void OnException(ExceptionContext context)
        {
            // log the exception
            _logger.LogError(0, context.Exception.GetBaseException(), "Exception occurred.");

            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var path = context.HttpContext.Request.Path;
            var user = _userManager.GetUser(_signInManager);
            user.Wait();

            var exception = new SiteException
            {
                BookingId = _shoppingCartRepository.GetSessionCartId(),
                Date = _calendar.LocalTime(),
                Error = context.Exception.Message,
                Ip = ip,
                Path = path.HasValue ? path.Value : "",
                User = user.Result
            };
            _appDbContext.Exceptions.Add(exception);
            _appDbContext.SaveChanges();
        }
    }
}
