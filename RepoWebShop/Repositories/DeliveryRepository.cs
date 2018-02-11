using Newtonsoft.Json;
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
        public DeliveryRepository()
        {

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
