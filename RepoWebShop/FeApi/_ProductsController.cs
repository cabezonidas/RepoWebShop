using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _ProductsController : Controller
	{
		private readonly ICatalogRepository _catalogRepo;

		public _ProductsController(ICatalogRepository catalogRepo)
		{
			_catalogRepo = catalogRepo;
		}


		[HttpGet]
		[Route("All")]
		public async Task<IEnumerable<_Product>> All()
		{
			var result = await _catalogRepo.ProductsGroupedByParent();
			return result;
		}
	}
}
