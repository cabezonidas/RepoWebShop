using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _CateringItem
	{
		public _Item Item { get; set; }
		public int ItemCount { get; set; }
		public int Quantity { get; set; }
		public decimal SubTotal { get; set; }
		public decimal SubTotalInStore { get; set; }
	}
}
