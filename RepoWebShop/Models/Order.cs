using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public List<OrderDetail> OrderLines { get; set; }

        [Required(ErrorMessage = "Por favor complete el número de teléfono")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Comentarios")]
        [StringLength(250)]
        public string Comments { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }

        public virtual Registration Registration { get; set; }
        public string BookingId { get; set; }
        public string Status { get; set; }
    }
}
