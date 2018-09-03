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
	public class _CateringsController : Controller
	{
		private readonly ILunchRepository _catering;
		private readonly IMapper _mapper;
		private readonly ICalendarRepository _calendar;
		private readonly IShoppingCartRepository _cart;

		public _CateringsController(IMapper mapper, ILunchRepository catering, ICalendarRepository calendar, IShoppingCartRepository cart)
		{
			_cart = cart;
			_calendar = calendar;
			_mapper = mapper;
			_catering = catering;
		}


		[HttpGet]
		[Route("AvailableCaterings")]
		public async Task<IEnumerable<_Catering>> AvailableCaterings()
		{
			IEnumerable<Lunch> caterings = await _catering.GetAllLunchesAsync(x => x.IsCombo);
			IEnumerable<_Catering> result = caterings.Select(c =>
			{
				var cat = _cart.Map(c);
				cat.Estimation = _calendar.GetPickupEstimate(cat.PreparationTime);
				cat.Price = _catering.GetTotal(c);
				cat.PriceInStore = _catering.GetLunchTotalInStore(c);
				return cat;
			});

			return result;
		}
	}
}
