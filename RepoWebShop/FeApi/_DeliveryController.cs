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
	public class _DeliveryController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IDeliveryRepository _delivery;
		private readonly IShoppingCartRepository _cart;

		public _DeliveryController(IMapper mapper, IDeliveryRepository delivery, IShoppingCartRepository cart)
		{
			_mapper = mapper;
			_delivery = delivery;
			_cart = cart;
		}

		[HttpGet]
		[Route("CanDeliver")]
		public bool CanDeliver() => _delivery.CanDelivery();

		[HttpGet]
		[Route("Get")]
		public _DeliveryAddress Get()
		{
			var delivery = _mapper.Map<_DeliveryAddress>(_cart.GetDelivery(null));
			return delivery;
		}

		[HttpGet]
		[Route("IsValidDistance/{lat}/{lng}")]
		public bool IsValidDistance(string lat, string lng)
		{
			var result = _delivery.IsValidDistance(lat, lng);
			return result;
		}
		[HttpDelete]
		[Route("Remove")]
		public void Remove() => _cart.RemoveDelivery();

		[HttpGet]
		[Route("SaveDelivery")]
		public void SaveDelivery()
		{
			var _address = Request.ParseBody<_DeliveryAddress[]>().FirstOrDefault();
			var address = _mapper.Map<DeliveryAddress>(_address);

			// _delivery.AddOrUpdateDelivery(address);
			return;
		}
	}
}
