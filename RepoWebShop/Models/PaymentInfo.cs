using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public int MercadoPagoPaymentId { get; set; }
        public string Payment_Type { get; set; }
        public MercadoPagoPayer MercadoPagoPayer { get; set;}
        public Decimal Total_Paid_Amount { get; set; }

        public int Order_Id { get; set; } //Important
        public int Transaction_Order_Id { get; set; } //important
        public string Reason { get; set; } //check if it's the title

        public DateTime Date_Created { get; set; }
        public DateTime Date_Approved { get; set; }
        public Decimal Concept_Amount { get; set; }
        public Decimal Transaction_Amount { get; set; }
        public Decimal Net_Received_Amount { get; set; }
        public Decimal Installment_Amount { get; set; }

        public string Status_Detail { get; set; }
        public string Site_Id { get; set; }
        public string Status { get; set; }
        public string Currency_Id { get; set; }
        public int Installments { get; set; }
        public DateTime Money_Release_Date { get; set; }
        public string Operation_Type { get; set; }
    }
}
