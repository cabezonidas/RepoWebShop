using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Filters;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [PageVisitAsync]
    [Authorize(Roles = "Administrator")]
    public class CalendarController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendarRepository;

        public CalendarController(AppDbContext appDbContext, IMapper mapper, ICalendarRepository calendarRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _calendarRepository = calendarRepository;
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
        public async Task<IActionResult> DeleteVacation(int id)
        {
            var vacation = _appDbContext.Vacations.FirstOrDefault(w => w.VacationId == id);
            _appDbContext.Vacations.Remove(vacation);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("SpecialDates", "Calendar");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteHoliday(int id)
        {
            var holiday = _appDbContext.Holidays.Include(x => x.ProcessingHours).Include(x => x.OpenHours).FirstOrDefault(w => w.PublicHolidayId == id);
            if(holiday.ProcessingHours != null)
                _appDbContext.ProcessingHours.Remove(holiday.ProcessingHours);
            if (holiday.OpenHours != null)
                _appDbContext.OpenHours.Remove(holiday.OpenHours);
            _appDbContext.Holidays.Remove(holiday);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("SpecialDates", "Calendar");
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult OpenHours()
        {
            var result = new OpenHoursViewModel()
            {
                OpenHours = _appDbContext.OpenHours.Where(x => x.DayId >= 0 && x.DayId <= 7).OrderBy(x => x.StartingAt).ToList(),
                PublicHolidays = _appDbContext.Holidays.Where(x => x.Date >= _calendarRepository.LocalTime().Date).Include(x => x.OpenHours),
                Vacations = _appDbContext.Vacations.Where(x => x.EndDate >= _calendarRepository.LocalTime().Date)
            };
            return View(result);
        }

        [HttpGet]
        public ViewResult AddVacations()
        {
            return View(new Vacation() { StartDate = _calendarRepository.LocalTime().Date, EndDate = _calendarRepository.LocalTime().Date });
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
            return View(new AddSpecialDateViewModel() { Date = _calendarRepository.LocalTime() });
        }

        [HttpPost]
        public async Task<IActionResult> AddPublicHoliday(AddSpecialDateViewModel specialDateViewModel)
        {
            if (ModelState.IsValid && specialDateViewModel.IsValid)
            {
                _appDbContext.Holidays.Add(_mapper.Map<AddSpecialDateViewModel, PublicHoliday>(specialDateViewModel));
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("SpecialDates", "Calendar");
            }
            if (!specialDateViewModel.IsValid)
                ModelState.AddModelError("InvalidValues", "Fecha debe ser mayor a hoy. El par de horas de produccion o de atencion debe estar vacio, o populados ambos valores con horario desde menor a horario hasta.");
            return View(specialDateViewModel);
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
