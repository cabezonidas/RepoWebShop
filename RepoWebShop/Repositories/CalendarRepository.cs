using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RepoWebShop.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;

        public string dayToSpanish(string day)
        {
            switch (day)
            {
                case "Monday":
                    return "Lunes";
                case "Tuesday":
                    return "Martes";
                case "Wednesday":
                    return "Miércoles";
                case "Thursday":
                    return "Jueves";
                case "Friday":
                    return "Viernes";
                case "Saturday":
                    return "Sábado";
                case "Sunday":
                    return "Domingo";
                default:
                    break;
            }
            return "";
        }

        public string spanishMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    break;
            }
            return "";
        }

        public CalendarRepository(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
        }

        public DateTime LocalTime()
        {
            return DateTime.Now.Zoned(_config.GetSection("LocalZone").Value);
        }

        public string LocalTimeAsString()
        {
            return LocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
        }

        public DateTime GetPickupEstimate(int hours)
        {
            var result = WorkingHours.GetPickUpDate(
                LocalTime(),
                hours,
                _appDbContext.ProcessingHours.ToList(),
                _appDbContext.OpenHours.ToList(),
                _appDbContext.Holidays.ToList(),
                _appDbContext.Vacations.ToList());

            var localTime = LocalTime();
            var offsetMinutes = 15 - (localTime.Minute % 15);
            localTime = localTime.AddMinutes(offsetMinutes);

            return result >= localTime ? result : localTime;
        }

        public string SuperFriendlyDate(DateTime? date)
        {
            if (date.HasValue)
            {
                var dia = dayToSpanish(date.Value.DayOfWeek.ToString());

                var mes = spanishMonth(date.Value.Month);
                
                var horas = date.Value.Hour <= 9 ? $"0{date.Value.Hour}" : date.Value.Hour.ToString();
                var minutos = date.Value.Minute <= 9 ? $"0{date.Value.Minute}" : date.Value.Minute.ToString();

                return $"{dia} {date.Value.Day} <br/>{mes} {horas}:{minutos}";
            }
            return string.Empty;
        }

        public string FriendlyDate(DateTime? date)
        {
            if (date.HasValue)
            {
                var dia = dayToSpanish(date.Value.DayOfWeek.ToString());
                var mes = spanishMonth(date.Value.Month);
                return $"{dia} {date.Value.Day} de {mes}";
            }
            return string.Empty;
        }

        public IEnumerable<KeyValuePair<DateTime, TimeSpan>> GetPickUpOption(int preparationTime, Discount discount)
        {
            var validDiscount = Discount.IsValid(LocalTime(), discount);
            if (!validDiscount)
                discount = null;

            var pickUpTime = GetPickupEstimate(preparationTime);
            var openHours = _appDbContext.OpenHours.ToList();
            var holidays = _appDbContext.Holidays.ToList();
            var vacations = _appDbContext.Vacations.ToList();

            var openSlots = WorkingHours.GetOpenSlots(pickUpTime, openHours, holidays, vacations);
            var pickUpOptions = WorkingHours.GetCompatibleOpenSlots(openSlots, discount, LocalTime());

            return pickUpOptions;
        }

        public string GetSoonestPickupEstimateForUsers(int prepTime)
        {
                var date = GetPickupEstimate(prepTime);
                var isToday = date.Date == LocalTime().Date;
                var datevalue = isToday ? "<span style=\"color: green;\">hoy</span> a partir de las" : $"a partir del {FriendlyDate(date)} a las";
                var text = $"Disponible {datevalue} {date.ToString("HH:ss")} hs.";
                return text;
        }
    }
}
