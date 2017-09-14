using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PaymentInfo
    {
        public PaymentInfo(Hashtable paymentInfoResponse)
        {
            MercadoPagoPaymentId = paymentInfoResponse["id"]?.ToString();
            Payment_Type = paymentInfoResponse["payment_type"]?.ToString();
            Total_Paid_Amount = Decimal.Parse(paymentInfoResponse["total_paid_amount"]?.ToString());
            Order_Id = paymentInfoResponse["order_id"]?.ToString();
            Reason = paymentInfoResponse["reason"]?.ToString();
            Date_Created = DateTime.Parse(paymentInfoResponse["date_created"]?.ToString());
            Status = paymentInfoResponse["status"]?.ToString();

            var payerInfoResponse = paymentInfoResponse["payer"] as Hashtable;
            First_Name = payerInfoResponse["first_name"]?.ToString();
            Last_Name = payerInfoResponse["last_name"]?.ToString();
            User_Id = payerInfoResponse["id"]?.ToString();
            Email = payerInfoResponse["email"]?.ToString();
            Nickname = payerInfoResponse["nickname"]?.ToString();

            var payerPhoneInfoResponse = payerInfoResponse["phone"] as Hashtable;
            var Area_Code = payerInfoResponse["area_code"];
            var Extension = payerInfoResponse["extension"];
            var Number = payerInfoResponse["number"];
        }
            
        public string First_Name { get; set; }
        public string User_Id { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }

        public string Area_Code { get; set; }
        public string Extension { get; set; }
        public string Number { get; set; }

        public int Id { get; set; }
        public string MercadoPagoPaymentId { get; set; }
        public string Payment_Type { get; set; }
        public Decimal Total_Paid_Amount { get; set; }

        public string Order_Id { get; set; } //Important
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
