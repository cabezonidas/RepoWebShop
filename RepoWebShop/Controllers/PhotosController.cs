using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RepoWebShop.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IFlickrRepository _photosetAlbums;
        private readonly IGalleryRepository _photosGalleryRepository;

        public PhotosController(IFlickrRepository photosetAlbums, IGalleryRepository photosGalleryRepository)
        {
            _photosetAlbums = photosetAlbums;
            _photosGalleryRepository = photosGalleryRepository;
        }

        public IActionResult Index()
        {
            //Asi obtengo las descripciones de las fotos (lo puedo hacer en JS tmb)
            //https://api.flickr.com/services/rest/?method=flickr.photos.getInfo&api_key=d097bef2694e1f6fea5d594b19967deb&secret=f6c592763d4a8387&photo_id=7923298054

            var view = _photosGalleryRepository.GetGalleryPictures();
            return View(view);
        }

        [HttpGet]
        [Route("[controller]/AlbumFullScreen/{Id}")]
        public ActionResult AlbumFullScreen(long Id)
        {
            var album = _photosetAlbums.GetAlbumPictures(Id);
            if (album == null)
                return NotFound();
            else
                return View(album);
        }
    }
}
