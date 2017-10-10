using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Vacation
    {
        public int VacationId { get; set; }
        [Display(Name = "Comienzo")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Fin")]
        public DateTime EndDate { get; set; }
    }
}
