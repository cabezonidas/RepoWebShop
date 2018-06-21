using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Cae
    {
        public int CaeId { get; set; }
        public InvoiceData InvoiceData { get; set; }
        public int Concepto { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public long CbteDesde { get; set; }
        public long CbteHasta { get; set; }
        public string CbteFch { get; set; }
        public string Resultado { get; set; }
        public string CAE { get; set; }
        public string CAEFchVto { get; set; }
    }
}
