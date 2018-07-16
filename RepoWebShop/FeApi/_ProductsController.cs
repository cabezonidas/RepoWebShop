using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using System.Collections.Generic;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _ProductsController : Controller
	{
		private readonly ICatalogRepository _catalogRepo;
		private readonly IMapper _mapper;

		public _ProductsController(IMapper mapper, ICatalogRepository catalogRepo)
		{
			_mapper = mapper;
			_catalogRepo = catalogRepo;
		}

		[HttpGet]
		[Route("All")]
		public IEnumerable<_Product> All()
		{
			IEnumerable<_Product> _products = _catalogRepo.GroupByParent(_catalogRepo.GetAvailableToBuyOnline());
			return _products;
		}
	}
}
