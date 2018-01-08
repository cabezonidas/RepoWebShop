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
        private readonly IFlickrRepository _photosetAlbums;
        private readonly IGalleryRepository _galleryRepository;
        public PhotosDataController(IFlickrRepository photosetAlbums, IGalleryRepository galleryRepository)
        {
            _photosetAlbums = photosetAlbums;
            _galleryRepository = galleryRepository;
        }

        [HttpGet]
        [Route("GetAlbum/{Id}")]
        public ActionResult GetAlbum(long Id)
        {
            var album = _photosetAlbums.GetAlbumPictures(Id);
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
