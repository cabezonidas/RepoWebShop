using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.FrontEndModels
{
	public class FeProduct
	{
		public PieDetail PieDetail { get; set; }
		public string PrimaryPicture { get; set; }
		public IEnumerable<FeItem> Items { get; set; }
	}
}
