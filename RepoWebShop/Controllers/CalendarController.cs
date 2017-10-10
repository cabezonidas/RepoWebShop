using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public ViewResult AddVacations()
        {
            return View(new Vacation() { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date });
        }

        [HttpPost]
        public async Task<IActionResult> AddVacations(Vacation vacation)
        {
            bool AreDatesOk = vacation.EndDate.Subtract(vacation.StartDate).Ticks > 0;
            if (ModelState.IsValid && AreDatesOk)
            {
                _appDbContext.Vacations.Add(vacation);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("SpecialDates", "Calendar");
            }
            if (!AreDatesOk)
                ModelState.AddModelError("InvalidDates", "Las fechas deben ser validas y la fecha de comienzo debe ser mayor a la de fin.");
            return View(vacation);
        }

        [HttpGet]
        public ViewResult AddPublicHoliday()
        {
            return View(new PublicHoliday() {  });
        }

        [HttpPost]
        public async Task<IActionResult> AddPublicHoliday(PublicHoliday holiday)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult SpecialDates(int id)
        {
            var holidays = _appDbContext.Holidays.Include(x => x.OpenHours).Include(x => x.ProcessingHours).ToList();
            var vacations = _appDbContext.Vacations.ToList();
            SpecialDatesViewModel model = new SpecialDatesViewModel()
            {
                Holidays = holidays,
                Vacations = vacations
            };
            return View(model);
        }
    }
}
