using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class CalendarDataController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _cartRepository;

        public CalendarDataController(AppDbContext appDbContext, IShoppingCartRepository sCart, IMapper mapper, ICalendarRepository calendarRepository)
        {
            _cartRepository = sCart;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _calendarRepository = calendarRepository;
        }

        [HttpGet]
        [Route("GetPickupDate")]
        public IActionResult GetPickupDate()
        {
            var hours = _cartRepository.GetPreparationTime(null);
            DateTime result = _calendarRepository.GetPickupEstimate(hours);
            
            return PartialView("PickupDate", result);
        }

        [HttpGet]
        [Route("GetProductPickupDate/{hours}")]
        public IActionResult GetProductPickupDate(int hours)
        {
            DateTime result = _calendarRepository.GetPickupEstimate(hours);
            return PartialView("ProductPickupDate", result);
        }

        [HttpPost]
        [Route("OpenHoursAddTimeFrame/{dayId}/{startingAt}/{finishAt}")]
        public IActionResult OpenHoursAddTimeFrame(int dayId, string startingAt, string finishAt)
        {
            var openHours = new WorkingHours();
            if (TryGetWorkingHours(startingAt, finishAt, dayId, out openHours))
            {
                _appDbContext.OpenHours.Add(_mapper.Map<WorkingHours, OpenHours>(openHours));
                _appDbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("OpenHoursDeleteTimeFrame/{id}")]
        public IActionResult OpenHoursDeleteTimeFrame(int id)
        {
            var openhours = _appDbContext.OpenHours.FirstOrDefault(x => x.Id == id);
            _appDbContext.OpenHours.Remove(openhours);
            _appDbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("ProcessingHoursAddTimeFrame/{dayId}/{startingAt}/{finishAt}")]
        public IActionResult ProcessingHoursAddTimeFrame(int dayId, string startingAt, string finishAt)
        {
            var processingHours = new WorkingHours();
            if (TryGetWorkingHours(startingAt, finishAt, dayId, out processingHours))
            {
                _appDbContext.ProcessingHours.Add(_mapper.Map<WorkingHours, ProcessingHours>(processingHours));
                _appDbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("ProcessingHoursDeleteTimeFrame/{id}")]
        public IActionResult ProcessingHoursDeleteTimeFrame(int id)
        {
            var processinghours = _appDbContext.ProcessingHours.FirstOrDefault(x => x.Id == id);
            _appDbContext.ProcessingHours.Remove(processinghours);
            _appDbContext.SaveChanges();
            return Ok();
        }

        private bool TryGetWorkingHours(string startingAt, string finishingAt, int dayId, out WorkingHours workingHours)
        {
            workingHours = new WorkingHours();
            try
            {
                var StartingAt = TimeSpan.Parse(startingAt);
                var FinishingAt = TimeSpan.Parse(finishingAt);
                if (FinishingAt > StartingAt)
                {
                    workingHours.DayId = dayId;
                    workingHours.StartingAt = StartingAt;
                    workingHours.Duration = new TimeSpan(FinishingAt.Ticks - StartingAt.Ticks);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
