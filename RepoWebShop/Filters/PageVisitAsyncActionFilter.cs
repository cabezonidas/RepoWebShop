using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;

namespace RepoWebShop.Filters
{
    public class PageVisitAsyncAttribute : TypeFilterAttribute
    {
        public PageVisitAsyncAttribute() : base(typeof(PageVisitAsyncActionFilter))
        {
        }

        private class PageVisitAsyncActionFilter : IAsyncActionFilter
        {
            private readonly ICalendarRepository _calendar;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly AppDbContext _appDbContext;

            public PageVisitAsyncActionFilter(ICalendarRepository calendar, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
            {
                _calendar = calendar;
                _httpContextAccessor = httpContextAccessor;
                _userManager = userManager;
                _signInManager = signInManager;
                _appDbContext = context;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // do something before the action executes
                var resultContext = await next();
                // do something after the action executes; resultContext.Result will be set

                var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var path = context.HttpContext.Request.Path;

                var user = await _userManager.GetUser(_signInManager);
                if (user != null && await _userManager.IsInRoleAsync(user, "Administrator"))
                    return;
                if (!path.HasValue)
                    return;

                var record = new PageVisit
                {
                    Ip = ip,
                    Path = path.Value,
                    Visited = _calendar.LocalTime(),
                    User = user
                };
                await _appDbContext.PageVisits.AddAsync(record);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}