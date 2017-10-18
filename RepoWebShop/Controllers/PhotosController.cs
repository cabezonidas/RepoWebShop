using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RepoWebShop.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl = "https://api.flickr.com/services/rest/";

        public PhotosController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            try
            {
                List<PhotosetPhotos> result = new List<PhotosetPhotos>();
                var photosets = GetPhotosets();
                foreach(var photoset in photosets.Photosets.Photoset)
                {
                    var photoSetid = photoset.Id;
                    var queryString = $"?method=flickr.photosets.getPhotos&api_key={_config["FlickrClientId"]}&photoset_id={photoSetid}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
                    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    request.Accept = "application/json";
                    request.Method = "GET";

                    HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
                    result.Add(new JsonSerializer().Deserialize<PhotosetPhotos>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream()))));
                }
                return View(result);
            }
            catch
            {
                return RedirectToAction("Index", "AppException");
            }

            //var photoSetid = "72157631388626826";
            //var queryString = $"?method=flickr.photosets.getPhotos&api_key={_config["FlickrClientId"]}&photoset_id={photoSetid}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
            //request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            //request.Accept = "application/json";
            //request.Method = "GET";
            //try
            //{
            //    HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
            //    return View(new JsonSerializer().Deserialize<PhotosetPhotos>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream()))));
            //    //Asi obtengo las descripciones de las fotos (lo puedo hacer en JS tmb)
            //    //https://api.flickr.com/services/rest/?method=flickr.photos.getInfo&api_key=d097bef2694e1f6fea5d594b19967deb&secret=f6c592763d4a8387&photo_id=7923298054
            //}
            //catch
            //{
            //    return RedirectToAction("Index", "AppException");
            //}
        }

        private PhotosetList GetPhotosets()
        {
            var queryString = $"?method=flickr.photosets.getList&api_key={_config["FlickrClientId"]}&format=json&user_id={_config["FlickrUserId"]}&nojsoncallback=?";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + queryString);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.Accept = "application/json";
            request.Method = "GET";

            HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
            return new JsonSerializer().Deserialize<PhotosetList>(new JsonTextReader(new StreamReader(apiResult.GetResponseStream())));
        }
    }
}
