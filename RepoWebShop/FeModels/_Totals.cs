using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
	public class _Totals
	{
		public decimal Total { get; internal set; }
		public decimal TotalInStore { get; internal set; }
		public decimal TotalWithoutDiscount { get; internal set; }
		public decimal Items { get; internal set; }
		public decimal ItemsInStore { get; internal set; }
		public decimal CustomCatering { get; internal set; }
		public decimal CustomCateringInStore { get; internal set; }
		public decimal Caterings { get; internal set; }
		public decimal CateringsInStore { get; internal set; }
		public decimal CateringsSavings { get; internal set; }
	}
}
