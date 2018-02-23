using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;
using RepoWebShop.Extensions;
using Microsoft.Extensions.Configuration;

namespace RepoWebShop.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;

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
            return WorkingHours.GetPickUpDate(
                LocalTime(),
                hours,
                _appDbContext.ProcessingHours.ToList(),
                _appDbContext.OpenHours.ToList(),
                _appDbContext.Holidays.ToList(),
                _appDbContext.Vacations.ToList());            
        }

        public string SuperFriendlyDate(DateTime? date)
        {
            if (date.HasValue)
            {
                var dia = "";
                var day = date.Value.DayOfWeek.ToString();
                switch (day)
                {
                    case "Monday":
                        dia = "Lunes";
                        break;
                    case "Tuesday":
                        dia = "Martes";
                        break;
                    case "Wednesday":
                        dia = "Miercoles";
                        break;
                    case "Thursday":
                        dia = "Jueves";
                        break;
                    case "Friday":
                        dia = "Viernes";
                        break;
                    case "Saturday":
                        dia = "Sábado";
                        break;
                    case "Sunday":
                        dia = "Domingo";
                        break;
                    default:
                        break;
                }

                var mes = "";
                var month = date.Value.Month;
                switch (month)
                {
                    case 1:
                        mes = "Enero";
                        break;
                    case 2:
                        mes = "Febrero";
                        break;
                    case 3:
                        mes = "Marzo";
                        break;
                    case 4:
                        mes = "Abril";
                        break;
                    case 5:
                        mes = "Mayo";
                        break;
                    case 6:
                        mes = "Junio";
                        break;
                    case 7:
                        mes = "Julio";
                        break;
                    case 8:
                        mes = "Agosto";
                        break;
                    case 9:
                        mes = "Septiembre";
                        break;
                    case 10:
                        mes = "Octubre";
                        break;
                    case 11:
                        mes = "Noviembre";
                        break;
                    case 12:
                        mes = "Diciembre";
                        break;
                    default:
                        break;
                }

                var horas = date.Value.Hour <= 9 ? $"0{date.Value.Hour}" : date.Value.Hour.ToString();
                var minutos = date.Value.Minute <= 9 ? $"0{date.Value.Minute}" : date.Value.Minute.ToString();

                return $"{dia} {date.Value.Day} <br/>{mes} {horas}:{minutos}";
            }
            return string.Empty;
        }
    }
}
