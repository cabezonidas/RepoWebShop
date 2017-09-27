using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Models;
using RepoWebShop.Helpers;
namespace RepoWebShop.ViewModels
{
    public class CalendarViewModel
    {
        public IEnumerable<ProcessingHours> ProcessingHoures { get; internal set; }
        public IEnumerable<OpenHours> OpenHours { get; internal set; }


        public string DayToString(int dayId)
        {
            string day;
            switch (dayId)
            {
                case 1:
                    day = "Lunes";
                    break;
                case 2:
                    day = "Martes";
                    break;
                case 3:
                    day = "Miercoles";
                    break;
                case 4:
                    day = "Jueves";
                    break;
                case 5:
                    day = "Viernes";
                    break;
                case 6:
                    day = "Sabado";
                    break;
                case 0:
                    day = "Domingo";
                    break;
                default:
                    day = "Error";
                    break;
            }
            return day;
        }
    }
}
