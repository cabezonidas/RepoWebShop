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
		public _DeliveryAddress Get() => _mapper.Map<_DeliveryAddress>(_cart.GetDelivery(null));

		[HttpDelete]
		[Route("Remove")]
		public void Remove() => _cart.RemoveDelivery();

		[HttpPost]
		[Route("SaveDelivery")]
		public _DeliveryAddress SaveDelivery() => _delivery.SaveAddress(Request.ParseBody<_DeliveryAddress>());

		[HttpPost]
		[Route("UpdateInstructions")]
		public _DeliveryAddress UpdateInstructions() => _delivery.UpdateInstructions(Request.ParseBody<_DeliveryAddress>());
	}
}
