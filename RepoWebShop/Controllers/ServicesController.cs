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
        private readonly IGalleryRepository _photosRepository;
        private readonly IFlickrRepository _photosetAlbums;

        public ServicesController(IGalleryRepository photosRepository, IFlickrRepository photosetAlbums)
        {
            _photosRepository = photosRepository;
            _photosetAlbums = photosetAlbums;
        }


        public ViewResult Index()
        {
            return View();
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
