using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class GalleryFlickrAlbum
    {
        public int GalleryFlickrAlbumId { get; set; }
        public long FlickrSetId { get; set; }
        public bool InGallery { get; set; }
    }
}
