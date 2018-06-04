using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        [Display(Name = "¿Repetir promoción semanalmente?")]
        public bool Weekly { get; set; } = false;

        [Display(Name = "¿Cuántas veces se puede usar?")]
        public int? InstancesLeft { get; set; }

        [Display(Name = "Porcentaje de descuento")]
        [Required]
        [Range(1, 100, ErrorMessage = "El valor tiene que estar entre 1 y 100")]
        public int Percentage { get; set; } = 20;

        [Required]
        [Display(Name = "Tope máximo de la oferta $")]
        [Range(1, 10000, ErrorMessage = "El valor tiene que estar entre 1 y 10000")]
        public Decimal Roof { get; set; } = 200;

        [Required]
        [Display(Name = "Valor mínimo de la órden $")]
        [Range(1, 10000, ErrorMessage = "El valor tiene que estar entre 1 y 10000")]
        public Decimal Base { get; set; } = 1;

        [Display(Name = "Términos y condiciones")]
        [StringLength(5000)]
        public string Comments { get; set; }

        public bool IsActive { get; set; } = true;

        [BindNever]
        public DateTime ValidTo { get => ValidFrom.AddDays(DurationDays - 1); }

        private static bool IsWithinRange(DateTime date, DateTime dateFrom, int daysDuration) =>
                dateFrom <= date && dateFrom.AddDays(daysDuration) >= date;
        
        public static bool IsValid(DateTime dateTime, Discount discount)
        {
            string error = string.Empty;
            return ApplyDiscount(dateTime, Decimal.MaxValue, discount, out error) < 0;
        }

        public static bool IsValid(DateTime dateTime, Discount discount, out string error)
        {
            return ApplyDiscount(dateTime, Decimal.MaxValue, discount, out error) < 0;
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
                error = "El código no está activo";
                return 0;
            }

            if (discount.InstancesLeft.HasValue && discount.InstancesLeft.Value <= 0)
            {
                error = "El código ya fue usado";
                return 0;
            }

            if (orderTotal < discount.Base)
            {
                error = $"El descuento es válido sólo para órdenes mayores a ${discount.Base}";
                return 0;
            }

            if (!IsWithinRange(dateTime, discount.ValidFrom, discount.DurationDays))
                if (discount.Weekly)
                {
                    DateTime loopDate = discount.ValidFrom;
                    for (; loopDate.AddDays(discount.DurationDays) <= dateTime; loopDate = loopDate.AddDays(7)) ;
                    if (!IsWithinRange(dateTime, loopDate, discount.DurationDays))
                    {
                        error = "El código no es válido hoy.";
                        return 0;
                    }
                }
                else
                {
                    if (dateTime < discount.ValidFrom)
                        error = "El código de descuento todavía no es válido.";
                    else
                        error = "El código de descuento ya expiró.";
                    return 0;
                }

            var result = 0m;
            var potencialDiscount = 0m;
            if(orderTotal >= discount.Base)
            {
                potencialDiscount = orderTotal * (discount.Percentage / 100m);
                result = -1 * (potencialDiscount > discount.Roof ? discount.Roof : potencialDiscount);
            }
            return result;
        }
    }
}
