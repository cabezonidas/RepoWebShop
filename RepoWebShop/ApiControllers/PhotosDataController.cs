using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ApiControllers
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

        [HttpGet]
        [Route("SmallCarrouselDynamic/{Id}")]
        public ActionResult SmallCarrouselDynamic(long Id)
        {
            var album = _photosetAlbums.GetAlbumPictures(Id);
            if (album == null)
                return NotFound();
            else
            {
                var result = PartialView("~/Views/Photos/SmallCarrouselDynamic.cshtml", album);
                return result;
            }
        }

        [HttpGet]
        [Route("GetAlbumStaff/{Id}")]
        public ActionResult GetAlbumStaff(long Id)
        {
            var album = _photosetAlbums.GetAlbumPictures(Id);
            if (album == null)
                return NotFound();
            else
            {
                var result = PartialView("AlbumCarouselStaff", album);
                return result;
            }
        }
    }
}
