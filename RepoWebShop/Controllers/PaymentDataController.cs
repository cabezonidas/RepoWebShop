using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PaymentDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;

        public PaymentDataController(IPieDetailRepository pieDetailRepository, IPieRepository pieRepository)
        {
            _pieDetailRepository = pieDetailRepository;
            _pieRepository = pieRepository;
        }

        [HttpGet]
        [Route("Process/{status}/{orderId}")]
        public IActionResult Index(string status, int orderId)
        {
            //Write payments in table

            /*if (json.collection_status == 'approved') {
                alert('Pago acreditado');
            } else if (json.collection_status == 'pending') {
                alert('El usuario no completó el pago');
            } else if (json.collection_status == 'in_process') {
                alert('El pago está siendo revisado');
            } else if (json.collection_status == 'rejected') {
                alert('El pago fué rechazado, el usuario puede intentar nuevamente el pago');
            } else if (json.collection_status == null) {
                alert('El usuario no completó el proceso de pago, no se ha generado ningún pago');
            }*/

            return Ok();
        }
    }
}
