using AutoMapper;
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
	public class _CartCustomCateringController : Controller
	{
		private readonly ILunchRepository _catering;
		private readonly ICatalogRepository _catalog;
		private readonly IMapper _mapper;
		private readonly IShoppingCartRepository _cart;

		public _CartCustomCateringController(IShoppingCartRepository cart, IMapper mapper, ILunchRepository catering, ICatalogRepository catalog)
		{
			_cart = cart;
			_mapper = mapper;
			_catering = catering;
			_catalog = catalog;
		}


		[HttpGet]
		[Route("CateringItems")]
		public async Task<IEnumerable<_Item>> CateringItems()
		{
			var _items = (await _catalog.GetAll(x => x.IsActive)).Select(x => _mapper.Map<Product, _Item>(x));
			return _items;
		}


		[HttpPost]
		[Route("SaveLocalCatering")]
		public async Task<_Catering> SaveLocalCatering([FromBody] IEnumerable<_CateringItem> catItems)
		{
			_cart.ClearCustomCateringFromCart();
			var catering = _cart.SessionCatering();
			foreach (var catItem in catItems)
			{
				await _catering.AddItemAsync(catering.LunchId, catItem.Item.ProductId, catItem.Quantity);
			}
			return _cart.SessionCatering();
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
			if (catering != null && 
				catering.Items != null && 
				catering.Items.FirstOrDefault(i => i.Item != null && i.Item.ProductId == id) != null)
				await _catering.AddItemInstanceAsync(catering.LunchId, id);
			else
				await _catering.AddItemAsync(catering.LunchId, id);
			catering = _cart.SessionCatering();
			return catering;
		}

		[HttpPost]
		[Route("CopyCatering/{id}")]
		public async Task<_Catering> CopyCatering(int id)
		{
			_cart.ClearCustomCateringFromCart();
			await _catering.CopyLunchAsync(id);
			return _cart.SessionCatering();
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

		[HttpPost]
		[Route("AddToCart")]
		public async Task<IActionResult> AddToCart(int id)
		{
			var catering = await _catering.GetLunchByIdAsync(id);
			_cart.AddCateringToCart(catering, 1);
			return Ok();
		}

		[HttpDelete]
		[Route("ClearSessionCatering")]
		public void ClearSessionCatering() => _cart.ClearCustomCateringFromCart();
	}
}
