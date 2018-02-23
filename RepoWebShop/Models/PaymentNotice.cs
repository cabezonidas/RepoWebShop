using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepoWebShop.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PaymentNotice
    {
        public PaymentNotice() { }

        public PaymentNotice(Hashtable paymentInfo, string localZone)
        {
            BookingId = paymentInfo.GetValue("external_reference", typeof(string)); //Mapped
            MercadoPagoTransaction = paymentInfo.GetValue("id", typeof(string)); //Mapped
            Payment_Type = paymentInfo.GetValue("payment_type", typeof(string));
            Reason = paymentInfo.GetValue("reason", typeof(string));
            Status = paymentInfo.GetValue("status", typeof(string)); //Mapped
            Order_Id = paymentInfo.GetValue("order_id", typeof(string));
            Merchant_Order_Id = paymentInfo.GetValue("merchant_order_id", typeof(string));
            Currency_Id = paymentInfo.GetValue("currency_id", typeof(string));
            Status_Detail = paymentInfo.GetValue("status_detail", typeof(string));

            OrderTotal = paymentInfo.GetValue("total_paid_amount", typeof(Decimal)); //Mapped
            Net_Received_Amount = paymentInfo.GetValue("net_received_amount", typeof(Decimal));
            Installment_Amount = paymentInfo.GetValue("installment_amount", typeof(Decimal));
            
            Date_Created = ((DateTime)paymentInfo.GetValue("date_created", typeof(DateTime))).Zoned(localZone); //Mapped
            Date_Approved = ((DateTime)paymentInfo.GetValue("date_approved", typeof(DateTime))).Zoned(localZone);
            Money_Release_Date = ((DateTime)paymentInfo.GetValue("money_release_date", typeof(DateTime))).Zoned(localZone);
            
            Installments = paymentInfo.GetValue("installments", typeof(int));

            var payerInfo = paymentInfo["payer"] as Hashtable;
            MercadoPagoName = $"{payerInfo.GetValue("first_name", typeof(string))} {payerInfo.GetValue("last_name", typeof(string))}";
            User_Id = payerInfo.GetValue("id", typeof(string));
            MercadoPagoMail = payerInfo.GetValue("email", typeof(string)); //Mapped
            MercadoPagoUsername = payerInfo.GetValue("nickname", typeof(string)); //Mapped

            var payerPhoneInfo = payerInfo["phone"] as Hashtable;
            Area_Code = payerPhoneInfo.GetValue("area_code", typeof(string));
            Extension = payerPhoneInfo.GetValue("extension", typeof(string));
            PhoneNumber = payerPhoneInfo.GetValue("number", typeof(string)); //Mapped
            PhoneNumber = string.IsNullOrEmpty(PhoneNumber) ? "-" : PhoneNumber;

            PaymentReceived = Status == "approved" || Status_Detail == "accredited"; //Revisar con refunds // Puede ser 'acreditted' en status_detail
            Payout = Date_Approved;
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
        public bool PaymentReceived { get; set; }
        public string Currency_Id { get; set; }
        public int Installments { get; set; }
        public DateTime? Money_Release_Date { get; set; }
        public string Operation_Type { get; set; }
        public decimal OrderTotal { get; private set; }
        public string MercadoPagoTransaction { get; private set; }

        [BindNever]
        public string PayerAsHtml
        {
            get
            {
                var payer = new List<string>() { MercadoPagoUsername, MercadoPagoName, MercadoPagoMail, PhoneNumber };
                return string.Join("<br />", payer);
            }
        }

            [BindNever]
        public string StatusSpanish
        {
            get
            {
                switch (Status)
                {
                    case "approved":
                        return "Aprobado";
                    case "pending":
                        return "Pendiente";
                    case "in_process":
                        return "En proceso";
                    case "rejected":
                        return "Rechazado";
                    case "draft":
                        return "Sin confirmar";
                    case "reservation":
                        return "Reserva";
                    case "refunded":
                        return "Reembolsado";
                    default:
                        return "Inválido";
                }
            }
        }
    }
}
