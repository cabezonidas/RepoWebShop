using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Components
{
    public class PhotosMenu : ViewComponent
    {
        private readonly IPhotosetAlbums _photosetAlbums;
        public PhotosMenu(IPhotosetAlbums photosetAlbums)
        {
            _photosetAlbums = photosetAlbums;
        }

        public IViewComponentResult Invoke()
        {
            return View(_photosetAlbums.Photosets);
        }
    }
}
