using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System.Linq;

namespace RepoWebShop.Controllers
{
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
        public ViewResult OpenHours()
        {
            var calendar = new CalendarViewModel()
            {
                ProcessingHours = _appDbContext.ProcessingHours.OrderBy(x => x.StartingAt).ToList(),
                OpenHours = _appDbContext.OpenHours.OrderBy(x => x.StartingAt).ToList()
            };

            return View(calendar);
        }
    }
}
