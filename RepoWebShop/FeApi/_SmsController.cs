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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _SmsController : Controller
	{
		private readonly ISmsRepository _sms;

		public _SmsController(ISmsRepository smsRepository)
		{
			_sms = smsRepository;
		}

		[HttpGet]
		[Route("IsValidMobile/{number}")]
		public async Task<bool> IsValidMobile(string number) => await _sms.IsValidNumberAsync(number);
	}
}
