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
        public CalendarDataController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("OpenHoursAddTimeFrame/{dayId}/{startingAt}/{finishAt}")]
        public IActionResult OpenHoursAddTimeFrame(int dayId, string startingAt, string finishAt)
        {
            var openHours = new OpenHours();
            if(TryGetOpenHours(startingAt, finishAt, dayId, out openHours))
            {
                _appDbContext.OpenHours.Add(openHours);
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
            var processingHours = new ProcessingHours();
            if (TryGetProcessingHours(startingAt, finishAt, dayId, out processingHours))
            {
                _appDbContext.ProcessingHours.Add(processingHours);
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

        private bool TryGetOpenHours(string startingAt, string finishingAt, int dayId, out OpenHours workingHours)
        {
            workingHours = new OpenHours();
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

        private bool TryGetProcessingHours(string startingAt, string finishingAt, int dayId, out ProcessingHours workingHours)
        {
            workingHours = new ProcessingHours();
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
