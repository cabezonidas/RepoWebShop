using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RepoWebShop.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;
        private readonly int _minimumCharge;
        private readonly int _costByBlock;
        public DeliveryRepository(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
            _minimumCharge = _config.GetValue<int>("LowestDeliveryCost");
            _costByBlock = _config.GetValue<int>("DeliveryCostByBlock");
        }

        public void AddOrUpdateDelivery(DeliveryAddress deliveryAddress)
        {
            var existingAddress = _appDbContext.DeliveryAddresses.OrderByDescending(x => x.DeliveryAddressId).FirstOrDefault(x => x.ShoppingCartId == deliveryAddress.ShoppingCartId);

            if (existingAddress != null)
            {
                existingAddress.AddressLine1 = deliveryAddress.AddressLine1;
                existingAddress.Country = deliveryAddress.Country;
                existingAddress.DeliveryCost = GetDeliveryEstimate(deliveryAddress.Distance);
                existingAddress.DeliveryInstructions = deliveryAddress.DeliveryInstructions;
                existingAddress.Distance = deliveryAddress.Distance;
                existingAddress.State = deliveryAddress.State;
                existingAddress.StreetName = deliveryAddress.StreetName;
                existingAddress.StreetNumber = deliveryAddress.StreetNumber;
                existingAddress.ZipCode = deliveryAddress.ZipCode;
                existingAddress.User = deliveryAddress.User;

                _appDbContext.DeliveryAddresses.Update(existingAddress);
            }
            else
            {
                deliveryAddress.Created = DateTime.Now.Zoned(_config.GetSection("LocalZone").Value);
                _appDbContext.DeliveryAddresses.Add(deliveryAddress);
            }
            _appDbContext.SaveChanges();
        }

        public async Task<int> GetDistanceAsync(string addressLine1)
        {
            var destination = await GetGeoLocationAsync(addressLine1);
            var res1 = distance(destination.Key, destination.Value, 'K');

            return res1;
        }

        public async Task<int> GetDrivingDistanceAsync(string addressLine1)
        {
            var httpClient = new HttpClient();
            var distanceApi = "maps.googleapis.com/maps/api/distancematrix";
            var metrics = "units=metric";
            var origins = "origins=place_id:EjVBdi4gSm9zw6kgTWFyw61hIE1vcmVubyA1NjEsIEMxNDI0QUFGIENBQkEsIEFyZ2VudGluYQ"; //Store location
            var apiKey = "key=AIzaSyBxKMKlaxoM9x9Jv-r4KXxhvgHBsKbAmEk";
            var result = await httpClient.GetAsync($"https://{distanceApi}/json?{metrics}&{origins}&destinations={addressLine1}&{apiKey}");
            var body = await result.Content.ReadAsStringAsync();

            var Distance = new JsonSerializer().Deserialize<DistanceMatrix>(new JsonTextReader(new StringReader(body)));
            var distanceMts = Distance.Rows.FirstOrDefault()?.Elements?.FirstOrDefault()?.Distance?.Value ?? 0;
            return distanceMts;
        }

        private async Task<KeyValuePair<string, string>> GetGeoLocationAsync(string addressLine1)
        {
            var requestUri = $"https://maps.googleapis.com/maps/api/geocode/xml?address={(Uri.EscapeDataString(addressLine1))}&key=AIzaSyBxKMKlaxoM9x9Jv-r4KXxhvgHBsKbAmEk&sensor=false";
            KeyValuePair<string, string> result;;
            using (var client = new HttpClient())
            {
                var request = await client.GetAsync(requestUri);
                var content = await request.Content.ReadAsStringAsync();
                var xmlDocument = XDocument.Parse(content);
                var location = xmlDocument.Element("GeocodeResponse").Element("result").Element("geometry").Element("location");
                result = new KeyValuePair<string, string>(location.Element("lat").Value, location.Element("lng").Value);
            }
            return result;
        }

        private int distance(string lat, string lon, char unit)
        {
            var lat1 = Double.Parse(lat, CultureInfo.InvariantCulture);
            var lon1 = Double.Parse(lon, CultureInfo.InvariantCulture);


            var lat2 = -34.625265;
            var lon2 = -58.434483;
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (int)(dist * 1000);
        }
        
        private double deg2rad(double deg) => (deg * Math.PI / 180.0);

        private double rad2deg(double rad) => (rad / Math.PI * 180.0);

        public decimal GetDeliveryEstimate(decimal distance)
        {
            var result = ((int)(distance / 100)) * _costByBlock;
            if (result < _minimumCharge)
                result = _minimumCharge;
            return result;
        }
    }
}
