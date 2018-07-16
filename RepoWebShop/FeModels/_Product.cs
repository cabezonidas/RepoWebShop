using System.Collections.Generic;

namespace RepoWebShop.FeModels
{
	public class _Product
	{
		public int PieDetailId { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public bool IsActive { get; set; }
		public bool IsPieOfTheWeek { get; set; }
		public string Ingredients { get; set; }
		public string FlickrAlbumId { get; set; }
		public IEnumerable<_Item> Items { get; set; }
	}
}
