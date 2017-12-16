using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PhotosDataController : Controller
    {
        private readonly IPhotosetAlbums _photosetAlbums;
        private readonly IPhotosGalleryRepository _galleryRepository;
        public PhotosDataController(IPhotosetAlbums photosetAlbums, IPhotosGalleryRepository galleryRepository)
        {
            _photosetAlbums = photosetAlbums;
            _galleryRepository = galleryRepository;
        }

        [HttpGet]
        [Route("GetAlbum/{Id}")]
        public ActionResult GetAlbum(long Id)
        {
            var album = _photosetAlbums.GetGalleryPictures(Id);
            if (album == null)
                return NotFound();
            else
            {
                var result = PartialView("AlbumCarousel", album);
                return result;
            }
        }
    }
}
