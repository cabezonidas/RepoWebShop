using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _OrdersController : Controller
	{
		private readonly IOrderRepository _order;
		private readonly ILazyOrderRepository _lazyOrder;

		public _OrdersController(IOrderRepository order, ILazyOrderRepository lazyOrder)
		{
			_order = order;
			_lazyOrder = lazyOrder;
		}


		[HttpGet]
		[Route("InProgress")]
		public async Task<IEnumerable<_Order>> InProgress()
		{
			var result = await _lazyOrder.GetOrdersInProgressAsync();
			return result;
		}


		[HttpGet]
		[Route("Caterings/{id}")]
		public async Task<IEnumerable<_OrderCatering>> Caterings(int id)
		{
			var result = await _lazyOrder.GetOrderCateringsAsync(id);
			return result;
		}


		[HttpGet]
		[Route("Items/{id}")]
		public async Task<IEnumerable<_OrderItem>> Items(int id)
		{
			var result = await _lazyOrder.GetOrderItemsAsync(id);
			return result;
		}
	}
}
