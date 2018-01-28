using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepoWebShop.Interfaces;
using RepoWebShop.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepoWebShop.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        //Puedo usar esto mismo en PieDetail, en vez de traer siempre un producto, y despues traer los relacionados
        public ICollection<OrderDetail> OrderLines { get; set; }

        //[Required(ErrorMessage = "Por favor complete el número de teléfono")]
        [StringLength(25)]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Comentarios")]
        public string CustomerComments { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }

        public bool Finished { get; set; }

        public bool Returned { get; set; }

        public bool Cancelled { get; set; }

        public bool PickedUp { get; set; }

        public bool Refunded { get; set; }

        public bool PaymentReceived { get; set; }

        public DateTime? PickUpTime { get; set; } 
        
        public DateTime? Payout { get; set; }

        public int PreparationTime { get; set; }

        public virtual ApplicationUser Registration { get; set; }

        public string ManagementComments { get; set; }

        public string OrderHistory { get; set; }

        public string BookingId { get; set; }

        public string Status { get; set; }

        public string MercadoPagoMail { get; set; }

        public string MercadoPagoName { get; set; }

        public string MercadoPagoUsername { get; internal set; }

        public string MercadoPagoTransaction { get; set; }

        public virtual Email Email { get; set; }
        
        [BindNever]
        public string FriendlyBookingId
        {
            get
            {
                try
                {
                    return BookingId?.Substring(BookingId.Length - 6, 6) ?? string.Empty;
                }
                catch
                {
                    return String.Empty;
                }
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
                        return "Esperando Acreditación";
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

        [BindNever]
        public string ContactDataAsHtml
        {
            get
            {
                var contactHtml = Registration != null ?
                    $"<div>{Registration.LastName}, {Registration.FirstName}</div>" +
                    $"<div><a href='mailto: {Registration?.NormalizedEmail}'>{Registration?.NormalizedEmail.ToLower()}</a></div>" :
                    $"<div>{MercadoPagoName}, {MercadoPagoUsername}</div>" +
                    $"<div><a href='mailto: {MercadoPagoMail}'>{MercadoPagoMail?.ToLower()}</a></div>";
                contactHtml += $"<div><a href='tel:{PhoneNumber}'>{PhoneNumber}</a></div>";

                return contactHtml;
            }
        }

        [BindNever]
        public IOrderPaymentStatus OrderPaymentStatus
        {
            get
            {
                if (Status == "reservation")
                    if (PaymentReceived)
                        return new OrderReservationPaid();
                    else
                        return new OrderReservationNotPaid();
                if (Status == "approved")
                    if (Refunded)
                        return new OrderMercadoPagoNotPaid();
                    else
                        return new OrderMercadoPagoPaid();
                if (Status == "in_process")
                    return new OrderMercadoPagoPaymentPending();

                return new OrderPaymentNotKnown();
            }
        }

        [BindNever]
        public IOrderProgressState OrderProgressState
        {
            get
            {
                if (Returned)
                    return new OrderReturned();

                else if (Cancelled)
                    return new OrderCancelled();

                else if (PickedUp)
                    return new OrderPickedUp();

                else if (Finished)
                    return new OrderComplete();

                else if (Status == "reservation" || PaymentReceived)
                    return new OrderInProgress();

                return new OrderOnHold();
            }
        }
    }
}
