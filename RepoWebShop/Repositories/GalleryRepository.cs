using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFlickrRepository _photosetAlbums;

        public GalleryRepository(AppDbContext appDbContext, IFlickrRepository photosetAlbums)
        {
            _photosetAlbums = photosetAlbums;
            _appDbContext = appDbContext;
        }

        public void AddFlickrAlbum(string setId)
        {
            long setIdLong = Int64.Parse(setId);
            var item = _appDbContext.GalleryFlickrAlbums.FirstOrDefault(x => x.FlickrSetId == setIdLong);

            if (item != null)
                item.InGallery = true;
            else
            {
                item = new GalleryFlickrAlbum()
                {
                    FlickrSetId = setIdLong,
                    InGallery = true
                };
                _appDbContext.GalleryFlickrAlbums.Add(item);
            }
            _appDbContext.SaveChanges();
        }

        public IEnumerable<GalleryFlickrAlbum> GetFlickrAlbums()
        {
            return _appDbContext.GalleryFlickrAlbums.ToArray();
        }

        public IEnumerable<AlbumPictures> GetGalleryPictures()
        {
            var albums = _appDbContext.GalleryFlickrAlbums.Where(x => x.InGallery).Select(x => x.FlickrSetId).ToArray().AsEnumerable();
            return _photosetAlbums.GetAlbumsPictures(albums);
        }

        public IEnumerable<AlbumPictures> GetAllAlbums()
        {
            var albums = _appDbContext.GalleryFlickrAlbums.Select(x => x.FlickrSetId).ToArray().AsEnumerable();
            
            return _photosetAlbums.GetAlbumsPictures(albums);
        }

        public void HideFlickrAlbum(string setId)
        {
            long setIdLong = Int64.Parse(setId);
            var item = _appDbContext.GalleryFlickrAlbums.FirstOrDefault(x => x.FlickrSetId == setIdLong);
            if(item != null)
            {
                item.InGallery = false;
                _appDbContext.SaveChanges();
            }
        }

        public void RemoveFlickrAlbum(string setId)
        {
            long setIdLong = Int64.Parse(setId);
            var item = _appDbContext.GalleryFlickrAlbums.FirstOrDefault(x => x.FlickrSetId == setIdLong);
            if (item != null)
            {
                _appDbContext.GalleryFlickrAlbums.Remove(item);
                _appDbContext.SaveChanges();
            }
        }
    }
}
