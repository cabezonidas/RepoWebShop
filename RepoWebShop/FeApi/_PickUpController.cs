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
	public class _PickUpController : Controller
	{
		private readonly IShoppingCartRepository _cart;

		public _PickUpController(IShoppingCartRepository cart)
		{
			_cart = cart;
		}

		[HttpGet]
		[Route("PickUpOptionsByDay")]
		public IEnumerable<_PickUpOptions> PickUpOptionsByDay() => _cart.PickUpOptionsByDay();
	}
}
