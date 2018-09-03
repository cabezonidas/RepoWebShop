using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _CalendarController : Controller
	{
		private readonly ICalendarRepository _calendar;

		public _CalendarController(ICalendarRepository calendar)
		{
			_calendar = calendar;
		}

		[HttpGet]
		[Route("PublicCalendar")]
		public OpenHoursViewModel PublicCalendar() => _calendar.PublicCalendar();

		[HttpGet]
		[Route("ReadyFor/{hours}")]
		public DateTime ReadyFor(int hours) => _calendar.GetPickupEstimate(hours);
	}
}
