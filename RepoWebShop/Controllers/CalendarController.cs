using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Linq;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CalendarController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public CalendarController(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var calendar = new CalendarViewModel()
            {
                ProcessingHours = _appDbContext.ProcessingHours.OrderBy(x => x.StartingAt).ToList(),
                OpenHours = _appDbContext.OpenHours.OrderBy(x => x.StartingAt).ToList()
            };

            return View(calendar);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult OpenHours()
        {
            var result = new OpenHoursViewModel()
            {
                OpenHours = _appDbContext.OpenHours.Where(x => x.DayId >= 0 && x.DayId <= 7).OrderBy(x => x.StartingAt).ToList(),
                PublicHolidays = _appDbContext.Holidays.Where(x => x.Date >= DateTime.Now.Date).Include(x => x.OpenHours),
                Vacations = _appDbContext.Vacations.Where(x => x.EndDate >= DateTime.Now.Date)
            };
            return View(result);
        }
    }
}
