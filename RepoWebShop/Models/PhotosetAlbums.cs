using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace RepoWebShop.Repositories
{
    public class PhotosetAlbums : IPhotosetAlbums
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl = "https://api.flickr.com/services/rest/";

        private PhotosetList photosets;
        private DateTime phosetsRefresh;

        private List<PhotosetPhotosRefresh> pictures = new List<PhotosetPhotosRefresh>();

        public PhotosetAlbums(IConfiguration config)
        {
            _config = config;
            photosets = GetPhotosets();
        }

        public PhotosetList Photosets
        {
            get
            {
                if(phosetsRefresh == null || DateTime.Now.Subtract(phosetsRefresh) > new TimeSpan(1, 0, 0))
                {
                    photosets = GetPhotosets();
                }
                return photosets;
            }
        }

        private PhotosetPhotos Photos(long id)
        {
            if (id == 0)
                return null;

            var albumRefresh = pictures.FirstOrDefault(x => x.PhotosetPhotos.Photoset.Id == id);

            if(albumRefresh == null)// || )
            {
                albumRefresh = new PhotosetPhotosRefresh(GetPics(id));
                if(albumRefresh.PhotosetPhotos.Photoset != null)
                    pictures.Add(albumRefresh);
            }
            else
            {
                if(albumRefresh.LastRefresh == null || DateTime.Now.Subtract(albumRefresh.LastRefresh) > new TimeSpan(1, 0, 0))
                {
                    albumRefresh.PhotosetPhotos = GetPics(id);
                    albumRefresh.LastRefresh = DateTime.Now;
                }
            }

            return albumRefresh.PhotosetPhotos;
        }

        public IEnumerable<string> GetPieDetailFotos(long flickrAlbumId)
        {
            var photos = Photos(flickrAlbumId);
            var result = new List<string>();
            try
            {
                result.AddRange(photos.Photoset.Photo.Select(x => $"https://farm{x.Farm}.staticflickr.com/{x.Server}/{x.Id}_{x.Secret}_"));
            }
            catch { }
            return result; 
        }


        /***************/
        private PhotosetList GetPhotosets()
        {
            var queryString = $"?method=flickr.photosets.getList&api_key={_config["FlickrClientId"]}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.Accept = "application/json";
            request.Method = "GET";

            HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();

            var result = new JsonSerializer().Deserialize<PhotosetList>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
            phosetsRefresh = DateTime.Now;
            return result;
        }

        private PhotosetPhotos GetPics(long id)
        {
            var queryString = $"?method=flickr.photosets.getPhotos&api_key={_config["FlickrClientId"]}&photoset_id={id}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.Accept = "application/json";
            request.Method = "GET";
            HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
            var result = new JsonSerializer().Deserialize<PhotosetPhotos>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
            return result;
        }

        public string GetPrimaryPicture(long flickrAlbumId)
        {
            string result = string.Empty;
            var photos = Photos(flickrAlbumId);
            try
            {
                var photo = photos.Photoset.Photo.First(x => x.Id == photos.Photoset.Primary);
                result = $"https://farm{photo.Farm}.staticflickr.com/{photo.Server}/{photo.Id}_{photo.Secret}_";
            }
            catch { }
            return result;
        }

        private class PhotosetPhotosRefresh
        {
            public PhotosetPhotos PhotosetPhotos;
            public DateTime LastRefresh;
            public PhotosetPhotosRefresh(PhotosetPhotos p)
            {
                PhotosetPhotos = p;
                LastRefresh = DateTime.Now;
            }
        }
    }
}
