﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _CateringController : Controller
	{
		private readonly ILunchRepository _catering;
		private readonly ICatalogRepository _catalog;
		private readonly IMapper _mapper;
		private readonly IShoppingCartRepository _cart;

		public _CateringController(IShoppingCartRepository cart, IMapper mapper, ILunchRepository catering, ICatalogRepository catalog)
		{
			_cart = cart;
			_mapper = mapper;
			_catering = catering;
			_catalog = catalog;
		}


		[HttpGet]
		[Route("CateringItems")]
		public IEnumerable<_Item> CateringItems()
		{
			var _items = _catalog.GetAll(x => x.IsActive).Select(x => _mapper.Map<Product, _Item>(x));
			return _items;
		}

		[HttpGet]
		[Route("LoadSessionCatering")]
		public _Catering LoadSessionCatering()
		{
			var catering = _cart.SessionCatering();
			return catering;
		}

		[HttpPost]
		[Route("AddItem/{id}")]
		public async Task<_Catering> AddItem(int id)
		{
			var catering = _cart.SessionCatering();
			if(catering.Items.FirstOrDefault(i => i.Item.ProductId == id) != null)
				await _catering.AddItemInstanceAsync(catering.LunchId, id);
			else
				await _catering.AddItemAsync(catering.LunchId, id);
			catering = _cart.SessionCatering();
			return catering;
		}

		[HttpDelete]
		[Route("RemoveItem/{id}")]
		public async Task<_Catering> RemoveItem(int id)
		{
			var catering = _cart.SessionCatering();
			await _catering.RemoveItemInstanceAsync(catering.LunchId, id);
			catering = _cart.SessionCatering();
			return catering;
		}

		[HttpDelete]
		[Route("ClearItem/{id}")]
		public async Task<_Catering> ClearItem(int id)
		{
			var catering = _cart.SessionCatering();
			await _catering.RemoveItemAsync(catering.LunchId, id);
			catering = _cart.SessionCatering();
			return catering;
		}

		[HttpGet]
		[Route("AvailableCaterings")]
		public async Task<IEnumerable<_Catering>> AvailableCaterings()
		{
			IEnumerable<Lunch> caterings = await _catering.GetAllLunchesAsync(x => x.IsCombo);
			IEnumerable<_Catering> result = caterings.Select(c => _cart.Map(c));
			return result;
		}

		[HttpPost]
		[Route("AddToCart")]
		public async Task<IActionResult> AddToCart(int id)
		{
			var catering = await _catering.GetLunchByIdAsync(id);
			_cart.AddCateringToCart(catering, 1);
			return Ok();
		}
	}
}