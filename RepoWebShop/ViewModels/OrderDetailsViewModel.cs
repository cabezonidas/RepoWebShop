using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RepoWebShop.ViewModels
{
    public class OrderDetailsViewModel
    {
        private Order _order;
        private IEnumerable<OrderDetail> _items;

        public OrderDetailsViewModel(Order order, IEnumerable<OrderDetail> items)
        {
            _order = order;
            _items = items;
        }

        public Order Order
        {
            get
            {
                return _order;
            }
        }
        public IEnumerable<OrderDetail> Items
        {
            get
            {
                return _items;
            }
        }

        public IEnumerable<string> Estados
        {
            get
            {
                var result = new List<string>();
                if(Order != null)
                {
                    if (!String.IsNullOrEmpty(Order.MercadoPagoTransaction))
                        result.Add($"Transacción de MercadoPago {Order.MercadoPagoTransaction}");
                    if (Order.Status == "in_process")
                        result.Add("Esperando aprobación de pago");
                    if (Order.PaymentReceived)
                        result.Add("Pago recibido");
                    if (Order.Finished)
                        result.Add("Orden lista");
                    if (Order.PickedUp)
                        result.Add("Retirado");
                    if (Order.Cancelled)
                        result.Add("Cancelado");
                    if (Order.Refunded)
                        result.Add("Reembolsado");
                    if (Order.Returned)
                        result.Add("Retornado");
                }
                return result;
            }
        }
    }
}
