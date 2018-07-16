using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _Album
    {
		public long AlbumId { get; set; }
		public string PrimaryPicture { get; set; }
		public string Title { get; set; }
		public IEnumerable<Photo> Photos { get; set; }
	}
}
