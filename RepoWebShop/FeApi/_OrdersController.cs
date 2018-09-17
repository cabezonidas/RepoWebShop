using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Extensions;
using RepoWebShop.FeApi.Invoice;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	[Authorize(Roles = "Administrator")]
	public class _OrdersController : Controller
	{
		private readonly IOrderRepository _order;
		private readonly ILazyOrderRepository _lazyOrder;
		private readonly IDiscountRepository _disc;

		public _OrdersController(IOrderRepository order, ILazyOrderRepository lazyOrder, IDiscountRepository disc)
		{
			_order = order;
			_lazyOrder = lazyOrder;
			_disc = disc;
		}


		[HttpGet]
		[Route("InProgress")]
		public async Task<IEnumerable<_Order>> InProgress() => await _lazyOrder.GetOrdersInProgressAsync();

		[HttpGet]
		[Route("All")]
		public async Task<IEnumerable<_Order>> All() => await _lazyOrder.AllAsync();

		[HttpGet]
		[Route("Caterings/{id}")]
		public async Task<IEnumerable<_OrderCatering>> Caterings(int id) => await _lazyOrder.GetOrderCateringsAsync(id);

		[HttpGet]
		[Route("Items/{id}")]
		public async Task<IEnumerable<_OrderItem>> Items(int id) => await _lazyOrder.GetOrderItemsAsync(id);

		[HttpGet]
		[Route("Pies/{id}")]
		public async Task<IEnumerable<_OrderPie>> Pies(int id) => await _lazyOrder.GetOrderPiesAsync(id);

		[HttpGet]
		[Route("CustomerName/{id}")]
		public string CustomerName(int id) => _lazyOrder.CustomerName(id);

		[HttpGet]
		[Route("CustomerNumbers/{id}")]
		public string CustomerNumbers(int id) => _lazyOrder.CustomerPhoneNumbers(id);

		[HttpGet]
		[Route("CustomerEmails/{id}")]
		public string CustomerEmails(int id) => _lazyOrder.CustomerEmails(id);

		[HttpGet]
		[Route("DetailsBreakDown/{id}")]
		public async Task<IEnumerable<_AmountTitle>> DetailsBreakDown(int id) => await _lazyOrder.DetailsBreakDown(id);

		[HttpGet]
		[Route("InvoiceData/{id}")]
		public async Task<_InvoiceData> InvoiceData(int id) => await _lazyOrder.InvoiceData(id);

		[HttpGet]
		[Route("Discount/{discountId}")]
		public Discount Discount(int discountId) => _disc.GetById(discountId);
	}
}
