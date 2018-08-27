using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly AppDbContext _appDbContext;

        public DiscountRepository(ICalendarRepository calendar, AppDbContext appDbContext)
        {
            _calendarRepository = calendar;
            _appDbContext = appDbContext;
        }

        public Discount Add(Discount discount)
        {
            if (FindByCode(discount.Phrase) == null)
            {
                discount.Comments = TermsAndConditions(discount);
                _appDbContext.Discounts.Add(discount);
                _appDbContext.SaveChanges();
                return discount;
            }
            else
                throw new Exception("Código existente");
        }

        public void Delete(int id)
        {
            var discount = _appDbContext.Discounts.FirstOrDefault(x => x.DiscountId == id);
            if(discount != null)
            {
                discount.IsActive = false;
                _appDbContext.Discounts.Update(discount);
                _appDbContext.SaveChanges();
            }
        }

        public IEnumerable<Discount> GetActives() => _appDbContext.Discounts.Where(x => x.IsActive).ToArray();

        public Discount FindByCode(string code) => GetActives().FirstOrDefault(x => x.Phrase.Trim().ToLower() == code.Trim().ToLower());

        public string TermsAndConditions(Discount discount)
        {
            var terms = new List<string>();
            var dateframe = $"El descuento es válido a partir del {_calendarRepository.FriendlyDate(discount.ValidFrom)} de {discount.ValidFrom.Year}.";
            terms.Add(dateframe);

            var enddate = string.Empty;
            if(discount.Weekly)
            {
                if(discount.DurationDays < 7)
                {
                    var daysofWeek = new List<string>();
                    for(int i = 0; i < discount.DurationDays; i++)
                    {
                        daysofWeek.Add(
                            _calendarRepository.dayToSpanish(
                                discount.ValidFrom.AddDays(i).DayOfWeek.ToString()));
                    }
                    var dayspromo = daysofWeek.Count == 1 ? daysofWeek.First() :
                        string.Join(", ", daysofWeek.Take(daysofWeek.Count() - 1)) + $" y {daysofWeek.Last()}";
                    terms.Add($"Válido solamente los días {dayspromo}.");
                }
                terms.Add("El descuento puede desactivarse sin previo aviso.");
            }
            else
            {
                terms.Add($"El descuento expira el {_calendarRepository.FriendlyDate(discount.ValidTo)} de {discount.ValidTo.Year}.");
            }

            if(discount.InstancesLeft.HasValue)
            {
                var discountLimit = $"El descuento se puede usar {(discount.InstancesLeft.Value == 1 ? "una sola vez" : "hasta " + discount.InstancesLeft.Value + " veces")}.";
                terms.Add(discountLimit);
            }

            if(discount.Percentage == 100)
            {
                var voucher = $"El voucher de ${discount.Roof} de descuento aplica para compras mayores a ${discount.Base}.";
                terms.Add(voucher);
            }
            else
            {
                terms.Add($"El descuento del {discount.Percentage}% aplica para compras mayores a ${discount.Base}.");
                terms.Add($"El monto máximo de descuento es ${discount.Roof}.");
            }

            terms.Add($"La promoción puede desactivarse o modificarse sin previo aviso.");
            terms.Add($"Sólo válido para compras online en www.delasartes.com.ar.");
            terms.Add($"La compra debe realizarse el día que la promoción sea válida.");
            terms.Add($"La entrega y/o envío debe estar dentro de las opciones que ofrece el sitio web al finalizar la compra.");
            

            return string.Join(" ", terms);
        }

		public bool IsValid(string code)
		{
			var discount = FindByCode(code);
			var error = "";
			if (discount != null)
				return Math.Abs(Discount.ApplyDiscount(
					_calendarRepository.LocalTime(), Decimal.MaxValue, discount, out error)) > 0;
			else
				return false;
		}

		public Discount AddQuickDiscount(decimal value)
		{
			var code = Guid.NewGuid().ToString("D").ToUpper().Ending(6);
			var disc = FindByCode(code);

			while (disc != null)
			{
				code = Guid.NewGuid().ToString("D").ToUpper().Ending(6);
				disc = FindByCode(code);
			}

			disc = new Discount
			{
				Base = value * 4,
				DurationDays = 15,
				InstancesLeft = 1,
				IsActive = true,
				Percentage = 100,
				Phrase = code,
				Roof = value,
				ValidFrom = _calendarRepository.LocalTime(),
				Weekly = false
			};

			return Add(disc);
		}
	}
}
