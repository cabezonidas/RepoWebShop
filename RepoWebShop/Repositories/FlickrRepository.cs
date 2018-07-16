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
using RepoWebShop.FeModels;
using AutoMapper;

namespace RepoWebShop.Repositories
{
    public class FlickrRepository : IFlickrRepository
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl = "https://api.flickr.com/services/rest/";
        private readonly IMapper _mapper;


		private PhotosetList albums;
        private DateTime lastRefresh;

        private List<PhotosetPhotosRefresh> picturesUrls = new List<PhotosetPhotosRefresh>();
        //Lists of albums with corresponding img urls. This is a cache.       

        public FlickrRepository(IConfiguration config, IMapper mapper)
        {
            _config = config;
			_mapper = mapper;
            albums = Api_GetAlbums();
        }

        public IEnumerable<PhotosetMetadata> Albums
        {
            get
            {
                if (lastRefresh == null || DateTime.Now.Subtract(lastRefresh) > new TimeSpan(0, 0, 30))
                {
                    Task.Run(() =>
                    {
                        albums = Api_GetAlbums();
                    });
                }
                return albums.Photosets.Photoset;
            }
        }

        public IEnumerable<AlbumPictures> GetAlbumsPictures(IEnumerable<long> albums)
        {
            List<AlbumPictures> result = new List<AlbumPictures>();
            foreach (var album in albums)
                result.Add(GetAlbumPictures(album));
            return result.AsEnumerable();
        }

        public AlbumPictures GetAlbumPictures(long id)
        {
            if (id == 0)
                return new AlbumPictures();

            var albumPictures = picturesUrls.FirstOrDefault(x => x.PhotosetPhotos.Photoset.Id == id);

            if (albumPictures == null)
            {
                albumPictures = new PhotosetPhotosRefresh(Api_GetPictures(id));
                if (albumPictures.PhotosetPhotos.Photoset != null)
                    picturesUrls.Add(albumPictures);
            }
            else
            {
                if (albumPictures.LastRefresh == null || DateTime.Now.Subtract(albumPictures.LastRefresh) > new TimeSpan(0, 0, 30))
                {
                    Task.Run(() =>
                    {
                        albumPictures.PhotosetPhotos = Api_GetPictures(id);
                        albumPictures.LastRefresh = DateTime.Now;
                    });
                }
            }

            return albumPictures.PhotosetPhotos;
        }

		public _Album GetFeAlbumBy(long id) => _mapper.Map<AlbumPictures, _Album>(GetAlbumPictures(id));

        /***************/
        private PhotosetList Api_GetAlbums()
        {
            var queryString = $"?method=flickr.photosets.getList&api_key={_config["FlickrClientId"]}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.Accept = "application/json";
            request.Method = "GET";

            HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();

            var result = new JsonSerializer().Deserialize<PhotosetList>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
            lastRefresh = DateTime.Now;
            return result;
        }
        private AlbumPictures Api_GetPictures(long id)
        {
            var queryString = $"?method=flickr.photosets.getPhotos&api_key={_config["FlickrClientId"]}&photoset_id={id}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.Accept = "application/json";
            request.Method = "GET";
            HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
            var result = new JsonSerializer().Deserialize<AlbumPictures>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
            return result;
        }


		private class PhotosetPhotosRefresh
        {
            public AlbumPictures PhotosetPhotos;
            public DateTime LastRefresh;
            public PhotosetPhotosRefresh(AlbumPictures p)
            {
                PhotosetPhotos = p;
                LastRefresh = DateTime.Now;
            }
        }
    }
}
