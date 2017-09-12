using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PaymentNotification
    {
        public int Id { get; set; }

        public int MercadoPagoId { get; set; }

        public bool Live_Mode { get; set; }
        public string Type { get; set; }
        public DateTime Date_Created { get; set; }
        public string User_Id { get; set; }
        public string Api_Version { get; set; }
        public string Action { get; set; }

        [NotMapped]
        public Dictionary<string, string> Data { get; set; }

        public string PaymentId
        {
            get
            {
                return Data["id"];
                //return Data.ContainsKey("id") ? Data["id"] : string.Empty;
            }
            private set
            {
                PaymentId = Data["id"];
                //PaymentId = Data.ContainsKey("id") ? Data["id"] : string.Empty;
            }
        }
    }
}
