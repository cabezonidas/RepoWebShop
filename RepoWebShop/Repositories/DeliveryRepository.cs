using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;
        public DeliveryRepository(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
        }

        public void AddOrUpdateDelivery(DeliveryAddress deliveryAddress)
        {
            var existingAddress = _appDbContext.DeliveryAddresses.OrderByDescending(x => x.DeliveryAddressId).FirstOrDefault(x => x.ShoppingCartId == deliveryAddress.ShoppingCartId);

            if (existingAddress != null)
            {
                existingAddress.AddressLine1 = deliveryAddress.AddressLine1;
                existingAddress.Country = deliveryAddress.Country;
                existingAddress.DeliveryCost = deliveryAddress.DeliveryEstimate;
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
    }
}
