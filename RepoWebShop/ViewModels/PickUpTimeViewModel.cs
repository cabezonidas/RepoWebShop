using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PickUpTimeViewModel
    {
        public KeyValuePair<DateTime, TimeSpan> SelectedTime { get; set; }
        public IEnumerable<KeyValuePair<DateTime, TimeSpan>> TimeSlots { get; set; }
        public bool UserSubmitted { get; set; }
        public string Message { get; set; }

        public static string SelectedDay(DateTime date)
        {
            var dia = "";
            var day = date.DayOfWeek.ToString();
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
            return dia;
        }
        public static string SelectedMonth(DateTime date)
        {
            var mes = "";
            var month = date.Month;
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
            return mes;
        }
    }
}
