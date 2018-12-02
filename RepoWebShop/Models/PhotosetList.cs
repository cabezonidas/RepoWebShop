using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class PhotosetList
    {
        public Photosets Photosets { get; set; }
        public string Stat { get; set; }
		public PhotosetList()
		{
			Photosets = new Photosets { Photoset = new List<PhotosetMetadata>() };
		}
	}
}
