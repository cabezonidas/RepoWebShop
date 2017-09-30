using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class CalendarFormatViewModel
    {
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
