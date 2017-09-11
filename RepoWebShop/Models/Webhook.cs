using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Webhook
    {
        public int Id { get; set; }
        public bool Live_Mode { get; set; }
        public string Type { get; set; }
        public DateTime Date_Created { get; set; }
        public string User_Id { get; set; }
        public string Api_Version { get; set; }
        public string Action { get; set; }
        public Dictionary<string, string> Data { get; set; }
        /* /"{\"id\":123,\
        "live_mode\":false,
        \"type\":\"test\",\"
        date_created\":\"2017-09-09T21:12:32.706-04:00\",
        \"user_id\":\"58959397\",
        \"api_version\":\"v1\",
        \"action\":\"test.created\",
        \"data\":{\"id\":\"56456123212\"}}"
        */
    }
}
