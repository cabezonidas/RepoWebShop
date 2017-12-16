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
        private readonly IPhotosetAlbums _photosetAlbums;
        private readonly IPhotosGalleryRepository _photosGalleryRepository;

        public PhotosController(IPhotosetAlbums photosetAlbums, IPhotosGalleryRepository photosGalleryRepository)
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
    }
}
