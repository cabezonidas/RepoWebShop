using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using RepoWebShop.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using RepoWebShop.FeModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RepoWebShop.Repositories
{
    public class FlickrRepository : IFlickrRepository
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

		private readonly AlbumPhotos AlbumImages;
		private readonly AlbumsRefresh AlbumsList;


		public FlickrRepository(IConfiguration config, IMapper mapper)
        {
            _config = config;
			_mapper = mapper;

			AlbumImages = new AlbumPhotos();
			AlbumsList = new AlbumsRefresh();
		}

        public IEnumerable<PhotosetMetadata> Albums
        {
			get => AlbumsList?.AllAlbums?.Photosets?.Photoset ?? new List<PhotosetMetadata>().AsEnumerable();
        }

        public IEnumerable<SelectListItem> AlbumsOptions()
        {
			List<SelectListItem> albumesSelect = new List<SelectListItem>();
			albumesSelect.Add(new SelectListItem { Value = "0", Text = "Sin álbum" });
			albumesSelect.AddRange(Albums.OrderBy(x => x.Title._Content).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title._Content }));
            return albumesSelect.AsEnumerable();
        }

        public IEnumerable<AlbumPictures> GetAlbumsPictures(IEnumerable<long> albums)
        {
            List<AlbumPictures> result = new List<AlbumPictures>();
            foreach (var album in albums)
                result.Add(GetAlbumPictures(album));
            return result.AsEnumerable();
        }

		public AlbumPictures GetAlbumPictures(long id) => AlbumImages[id];

		public _Album GetFeAlbumBy(long id) => _mapper.Map<AlbumPictures, _Album>(GetAlbumPictures(id));


		private class AlbumPhotos
		{
			private readonly List<AlbumPhotosRefresh> Albums;
			public AlbumPhotos()
			{
				Albums = new List<AlbumPhotosRefresh>();
			}

			public AlbumPictures this[long albumId]
			{
				get
				{
					var albumPictures = Albums.FirstOrDefault(a => a.PhotosetPhotos.Photoset.Id == albumId);
					if (albumPictures == null || TimeToRefresh(albumPictures.LastRefresh))
						//try
						{
							var newPictures = new AlbumPhotosRefresh(Api_GetPictures(albumId));
							if (albumPictures != null)
								Albums.Remove(albumPictures);
							Albums.Add(newPictures);
							albumPictures = newPictures;
						} 
						//catch { }
					return albumPictures?.PhotosetPhotos;
				}
			}
		}

		/***************/
		private class AlbumPhotosRefresh
		{
			public AlbumPictures PhotosetPhotos;
			public DateTime LastRefresh;
			public AlbumPhotosRefresh(AlbumPictures p)
			{
				if (p != null)
				{
					PhotosetPhotos = p;
					LastRefresh = DateTime.Now;
				}
			}
		}

		private class AlbumsRefresh
		{
			private PhotosetList Albums;
			public DateTime LastRefresh;

			public PhotosetList AllAlbums 
			{
				get
				{
					if (Albums == null || TimeToRefresh(LastRefresh))
						//try
						//{
							Albums = Api_GetAlbums();
							LastRefresh = DateTime.Now;
						//}
						//catch { }
					return Albums;
				}
			}
		}

		/***************/
		private static PhotosetList Api_GetAlbums()
		{
			var _flickrClientId = "d097bef2694e1f6fea5d594b19967deb";
			var _flickrUserId = "85024949%40N08";
			string apiUrl = "https://api.flickr.com/services/rest/";

			PhotosetList albums;
			var queryString = $"?method=flickr.photosets.getList&api_key={_flickrClientId}&format=json&user_id={_flickrUserId}&nojsoncallback=?";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
			request.Proxy.Credentials = CredentialCache.DefaultCredentials;
			request.Accept = "application/json";
			request.Method = "GET";
			HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
			albums = new JsonSerializer().Deserialize<PhotosetList>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
			return albums;
		}

		private static AlbumPictures Api_GetPictures(long id)
		{
			var _flickrClientId = "d097bef2694e1f6fea5d594b19967deb";
			var _flickrUserId = "85024949%40N08";
			string apiUrl = "https://api.flickr.com/services/rest/";

			AlbumPictures photos;
			var queryString = $"?method=flickr.photosets.getPhotos&api_key={_flickrClientId}&photoset_id={id}&format=json&user_id={_flickrUserId}&nojsoncallback=?";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
			request.Proxy.Credentials = CredentialCache.DefaultCredentials;
			request.Accept = "application/json";
			request.Method = "GET";
			HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
			photos = new JsonSerializer().Deserialize<AlbumPictures>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
			return photos;
		}

		private static bool TimeToRefresh(DateTime lastRefresh) => lastRefresh == null || DateTime.Now.Subtract(lastRefresh) > new TimeSpan(0, 4, 0);
	}
}
