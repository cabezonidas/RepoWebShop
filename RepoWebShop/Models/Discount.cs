using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        [StringLength(20)]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Ingresa un código")]
        public string Phrase { get; set; }
        [Display(Name = "Fecha de comienzo de descuento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ingresa una fecha de comienzo")]
        public DateTime ValidFrom { get; set; }
        [Display(Name = "Duración (días)")]
        [Range(1, 1000)]
        [Required(ErrorMessage = "Ingresa cuántos días dura la promoción")]
        public int DurationDays { get; set; }
        [Display(Name = "¿Repetir promoción? (cada X días)")]
        public int? Loop { get; set; }
        [Display(Name = "¿Cuántas veces se puede usar?")]
        public int? InstancesLeft { get; set; }
        [Display(Name = "Porcentaje de descuento")]
        [Required]
        [Range(1, 99, ErrorMessage = "El valor tiene que estar entre 1 y 99")]
        public int Percentage { get; set; }
        [Required]
        [Display(Name = "Tope máximo de la oferta $")]
        [Range(1, 1000, ErrorMessage = "El valor tiene que estar entre 1 y 1000")]
        public Decimal Roof { get; set; }
        [Display(Name = "Comentarios")]
        [StringLength(50)]
        public string Comments { get; set; }
        public bool IsActive { get; set; }

        private static bool IsWithinRange(DateTime date, DateTime dateFrom, int daysDuration) =>
                dateFrom <= date && dateFrom.AddDays(daysDuration) >= date;

        public static decimal ApplyDiscount(DateTime dateTime, decimal orderTotal, Discount discount)
        {
            string error = string.Empty;
            return ApplyDiscount(dateTime, orderTotal, discount, out error);
        }

        public static decimal ApplyDiscount(DateTime dateTime, decimal orderTotal, Discount discount, out string error)
        {
            error = string.Empty;
            //Sacar el descuento de otras ordenes y updatear todas las preferencias de pago
            if (discount == null)
            {
                error = "No se pudo encontrar el código";
                return 0;
            }

            if (!discount.IsActive)
            {
                error = "El código no está inactivo";
                return 0;
            }

            if (discount.InstancesLeft.HasValue && discount.InstancesLeft.Value <= 0)
            {
                error = "El código ya fue usado";
                return 0;
            }

            if (!IsWithinRange(dateTime, discount.ValidFrom, discount.DurationDays))
                if (discount.Loop.HasValue)
                {
                    DateTime loopDate = discount.ValidFrom;
                    for (; loopDate.AddDays(discount.DurationDays) <= dateTime; loopDate = loopDate.AddDays(discount.Loop.Value)) ;
                    if (!IsWithinRange(dateTime, loopDate, discount.DurationDays))
                    {
                        error = "El código no es válido hoy.";
                        return 0;
                    }
                }
                else
                {
                    error = "El código de descuento expiró.";
                    return 0;
                }

            var potencialDiscount = orderTotal * (discount.Percentage / 100m);
            return -1 * (potencialDiscount > discount.Roof ? discount.Roof : potencialDiscount);
        }
    }
}
