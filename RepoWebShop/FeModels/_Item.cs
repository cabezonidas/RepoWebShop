using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RepoWebShop.FeModels
{
    public class _Item
	{
		public int? PieDetailId { get; set; }

		public int ProductId { get; set; }

		public decimal Price { get; set; }

		public int MultipleAmount { get; set; }

		public string SizeDescription { get; set; }

		public string Flavour { get; set; }

		public decimal PriceInStore { get; set; }

		public string Category { get; set; }

		public string Temperature { get; set; }

		public int MinOrderAmount { get; set; }

		public int Portions { get; set; }

		public int PreparationTime { get; set; }

		[BindNever]
		public string DisplayName { get; set; }

		[BindNever]
		public string DisplayDescription { get; set; }
		public DateTime Estimation { get; internal set; }
	}
}
