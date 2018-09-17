using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	[Authorize(Roles = "Administrator")]
	public class _DeliveryController : Controller
	{
		private readonly IDeliveryRepository _delivery;

		public _DeliveryController(IDeliveryRepository delivery)
		{
			_delivery = delivery;
		}

		[HttpGet]
		[Route("Get/{id}")]
		public _DeliveryAddress IsValidMobile(int id) => _delivery.GetDeliveryById(id);
	}
}
