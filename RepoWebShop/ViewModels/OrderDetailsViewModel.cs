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
        private IEnumerable<OrderCatalogItem> _catalogItems;
        private IEnumerable<OrderCatering> _cateringItems;

        public OrderDetailsViewModel(Order order, IEnumerable<OrderDetail> items, IEnumerable<OrderCatalogItem> catalogItems, IEnumerable<OrderCatering> cateringItems)
        {
            _order = order;
            _items = items;
            _catalogItems = catalogItems;
            _cateringItems = cateringItems;
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
        public IEnumerable<OrderCatalogItem> CatalogItems
        {
            get
            {
                return _catalogItems;
            }
        }
        public IEnumerable<OrderCatering> Caterings
        {
            get
            {
                return _cateringItems;
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
