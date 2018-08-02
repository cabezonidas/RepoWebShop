using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _ProductsController : Controller
	{
		private readonly ICatalogRepository _catalogRepo;
		private readonly IPieDetailRepository _pieRepo;
		private readonly IMapper _mapper;

		public _ProductsController(IMapper mapper, ICatalogRepository catalogRepo, IPieDetailRepository pieRepo)
		{
			_pieRepo = pieRepo;
			_mapper = mapper;
			_catalogRepo = catalogRepo;
		}


		[HttpGet]
		[Route("All")]
		public IEnumerable<_Product> All()
		{
			var _items = _catalogRepo.GetAvailableToBuyOnline().Select(x => _mapper.Map<Product, _Item>(x));
			IEnumerable<_Product> _products = _pieRepo.PieDetails.Where(x => x.IsActive).ToArray().Select(x => _mapper.Map<PieDetail, _Product>(x));

			var result = _products.Select(x =>
			{
				x.Items = _items.Where(item => item.PieDetailId == x.PieDetailId);
				return x;
			}).ToArray();

			return result;
		}

		[HttpGet]
		[Route("CateringItems")]
		public IEnumerable<_Item> CateringItems()
		{
			var _items = _catalogRepo.GetAll(x => x.IsActive).Select(x => _mapper.Map<Product, _Item>(x));
			return _items;
		}
	}
}
