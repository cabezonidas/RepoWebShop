using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IPhotosGalleryRepository _photosRepository;
        private readonly IPhotosetAlbums _photosetAlbums;

        public ServicesController(IPhotosGalleryRepository photosRepository, IPhotosetAlbums photosetAlbums)
        {
            _photosRepository = photosRepository;
            _photosetAlbums = photosetAlbums;
        }


        public ViewResult Birthdays()
        {
            return View(_photosRepository.GetGalleryPictures());
        }

        public ViewResult Breakfast()
        {
            return View();
        }

        public ViewResult Catering()
        {
            return View();
        }

        public ViewResult SpecialCakes()
        {
            return View(_photosRepository.GetGalleryPictures());
        }

        public ViewResult SweetTable()
        {
            return View();
        }
    }
}
