using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Interfaces;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using RepoWebShop.Models;
using System.Collections;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Filters;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    public class ServicesController : Controller
    {
        private readonly IGalleryRepository _photosRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IFlickrRepository _photosetAlbums;
        private readonly IConfiguration _config;
        private readonly ICatalogRepository _catalog;

        public ServicesController(ICatalogRepository catalog, AppDbContext appDbContext, IGalleryRepository photosRepository, IFlickrRepository photosetAlbums, IConfiguration config)
        {
            _catalog = catalog;
            _config = config;
            _photosRepository = photosRepository;
            _photosetAlbums = photosetAlbums;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Birthdays()
        {
            return View(_photosRepository.GetGalleryPictures());
        }

        [HttpGet]
        public ViewResult Breakfast()
        {
            return View();
        }

        private async Task<string> GetSharePointAccessTokenAsync()
        {
            var tenantId = _config.GetSection("SharePointTenantId").Value;
            var uri = $"https://accounts.accesscontrol.windows.net/{tenantId}/tokens/OAuth/2";

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            nvc.Add(new KeyValuePair<string, string>("client_id", $"{_config.GetSection("SharePointAppId").Value}@{tenantId}"));
            nvc.Add(new KeyValuePair<string, string>("client_secret", $"{_config.GetSection("SharePointSecret").Value}"));
            nvc.Add(new KeyValuePair<string, string>("resource", $"00000003-0000-0ff1-ce00-000000000000/lareposteria.sharepoint.com@{tenantId}"));

            var result = await new HttpClient().PostAsync(uri, new FormUrlEncodedContent(nvc));

            var body = await result.Content.ReadAsStreamAsync();

            var Response = ((JToken)new JsonSerializer().Deserialize<Object>(new JsonTextReader(new StreamReader(body)))).Root;
            var access_token = ((JValue)Response["access_token"])?.Value.ToString();
            return access_token;
        }

        [HttpGet]
        public ViewResult Catering()
        {
            //var access_token = await GetSharePointAccessTokenAsync();

            //var uri = "https://lareposteria.sharepoint.com/_api/Web/Lists(guid'b10fac5f-d656-49ca-bb9a-589b9362bb94')/items?$select=Title,Precio,CantidadMinima&$filter=Categoria eq 'Lunch'&$orderby=Title asc";
            //var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            //requestMessage.Headers.Add("Accept", "application/json;odata=verbose");

            //var result = await new HttpClient().SendAsync(requestMessage);
            //var body = await result.Content.ReadAsStreamAsync();
            //var Response = (new JsonSerializer().Deserialize<Object>((new JsonTextReader(new StreamReader(body)))));
            //return View("Catering", access_token);

            var result = _catalog.GetAll(x => x.Category.ToLower().Trim() == "lunch");

            return View("Catering", result);
        }

        [HttpGet]
        public ViewResult SpecialCakes()
        {
            return View(_photosRepository.GetGalleryPictures());
        }

        [HttpGet]
        public ViewResult FullCatalog()
        {
            //var access_token = await GetSharePointAccessTokenAsync();
            //return View("FullCatalog", access_token);

            var result = _catalog.GetActive().Where(x => x.Category.ToLower().Trim() != "lunch").OrderBy(x => x.DisplayName);
            return View("FullCatalog", result);
        }

        [HttpGet]
        public ViewResult SweetTable()
        {
            return View();
        }
    }
}
