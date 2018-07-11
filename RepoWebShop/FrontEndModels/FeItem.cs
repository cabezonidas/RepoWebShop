using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FrontEndModels
{
    public class FeItem
    {
		public int ProductId { get; set; }

		public decimal Price { get; set; } = 100;

		public int MultipleAmount { get; set; } = 1;

		public string SizeDescription { get; set; }

		public string Flavour { get; set; }

		public decimal PriceInStore { get; set; } = 100;

		public string Category { get; set; }

		public string Temperature { get; set; }

		public int MinOrderAmount { get; set; } = 1;

		public int Portions { get; set; } = 1;

		public int PreparationTime { get; set; } = 6;

		[BindNever]
		public string DisplayName { get; set; }

		[BindNever]
		public string DisplayDescription { get; set; }
		public string PickUpAsHtml { get; internal set; }
	}
}
