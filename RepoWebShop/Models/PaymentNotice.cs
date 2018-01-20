using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PaymentNotice
    {
        public PaymentNotice(Hashtable paymentInfoResponse)
        {
            BookingId = paymentInfoResponse["external_reference"]?.ToString(); //Mapped
            MercadoPagoTransaction = paymentInfoResponse["id"]?.ToString(); //Mapped
            Payment_Type = paymentInfoResponse["payment_type"]?.ToString();
            OrderTotal = Decimal.Parse(paymentInfoResponse["total_paid_amount"]?.ToString()); //Mapped
            Order_Id = paymentInfoResponse["order_id"]?.ToString();
            Reason = paymentInfoResponse["reason"]?.ToString();
            Date_Created = DateTime.Parse(paymentInfoResponse["date_created"]?.ToString());
            Payout = DateTime.Parse(paymentInfoResponse["date_approved"]?.ToString());
            Status = paymentInfoResponse["status"]?.ToString(); //Mapped

            Merchant_Order_Id = paymentInfoResponse["merchant_order_id"]?.ToString();
            Net_Received_Amount = Decimal.Parse(paymentInfoResponse["net_received_amount"]?.ToString());
             
            var payerInfoResponse = paymentInfoResponse["payer"] as Hashtable;
            MercadoPagoName = payerInfoResponse["first_name"]?.ToString() + " " + payerInfoResponse["last_name"]?.ToString(); //Mapped
            User_Id = payerInfoResponse["id"]?.ToString();
            MercadoPagoMail = payerInfoResponse["email"]?.ToString(); //Mapped
            MercadoPagoUsername = payerInfoResponse["nickname"]?.ToString(); //Mapped

            var payerPhoneInfoResponse = payerInfoResponse["phone"] as Hashtable;
            Area_Code = payerPhoneInfoResponse["area_code"]?.ToString();
            Extension = payerPhoneInfoResponse["extension"]?.ToString();
            PhoneNumber = payerPhoneInfoResponse["number"]?.ToString(); //Mapped
            PhoneNumber = String.IsNullOrEmpty(PhoneNumber) ? "-" : PhoneNumber;
        }
            
        public string MercadoPagoName { get; set; }
        public string User_Id { get; set; }
        public string MercadoPagoMail { get; set; }
        public string MercadoPagoUsername { get; set; }

        public string Area_Code { get; set; }
        public string Extension { get; set; }
        public string PhoneNumber { get; set; }

        public int Id { get; set; }
        public string Payment_Id { get; set; }
        public string Payment_Type { get; set; }
        public Decimal Total_Paid_Amount { get; set; }

        public string Order_Id { get; set; }
        public int Transaction_Order_Id { get; set; }
        public string Reason { get; set; }
        public string BookingId { get; set; }

        public DateTime? Date_Created { get; set; }
        public DateTime? Payout { get; set; }
        public DateTime? Date_Approved { get; set; }
        public Decimal Concept_Amount { get; set; }
        public Decimal Transaction_Amount { get; set; }
        public Decimal Net_Received_Amount { get; set; }
        public string Merchant_Order_Id { get; set; }
        public Decimal? Installment_Amount { get; set; }

        public string Status_Detail { get; set; }
        public string Site_Id { get; set; }
        public string Status { get; set; }
        public string Currency_Id { get; set; }
        public int Installments { get; set; }
        public DateTime? Money_Release_Date { get; set; }
        public string Operation_Type { get; set; }
        public decimal OrderTotal { get; private set; }
        public string MercadoPagoTransaction { get; private set; }
    }
}
