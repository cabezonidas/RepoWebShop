using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class GalleryFlickrAlbumViewModel
    {
        public int? GalleryFlickrAlbumId { get; set; }
        public long FlickrSetId { get; set; }
        public string FlickrSetTitle { get; set; }
        public bool InGallery { get; set; }
        public bool InFlickr { get; internal set; }
    }
}
