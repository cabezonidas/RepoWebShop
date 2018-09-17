using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepoWebShop.FeApi.Invoice;
using RepoWebShop.FeModels;
using RepoWebShop.Models;

namespace RepoWebShop.Interfaces
{
    public interface ILazyOrderRepository
    {
		Task<IEnumerable<_Order>> AllAsync(Func<Order, bool> condition = null);
		Task<IEnumerable<_Order>> GetOrdersInProgressAsync();
		Task<IEnumerable<_Order>> GetOrdersCancelledAsync();
		Task<IEnumerable<_Order>> GetOrdersCompletedAsync();
		Task<IEnumerable<_OrderCatering>> GetOrderCateringsAsync(int id);
		Task<IEnumerable<_OrderItem>> GetOrderItemsAsync(int id);
		Task<IEnumerable<_OrderPie>> GetOrderPiesAsync(int id);
		string CustomerName(int id);
		string CustomerEmails(int id);
		string CustomerPhoneNumbers(int id);
		Task<IEnumerable<_AmountTitle>> DetailsBreakDown(int id);
		Task<_InvoiceData> InvoiceData(int id);
	}
}
