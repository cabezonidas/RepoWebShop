using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class FECAEDetRequest
    {
        public int Concepto { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public long CbteDesde { get; set; }
        public long CbteHasta { get; set; }
        public double ImpTotal { get; set; }
        public double ImpTotConc { get; set; }
        public double ImpNeto { get; set; }
        public double ImpOpEx { get; set; }
        public double ImpTrib { get; set; }
        public string CbteFch { get; set; }
        public double ImpIVA { get; set; }
        public string MonId { get; set; }
        public double MonCotiz { get; set; }
        public AlicIva[] Iva { get; set; }
        public class AlicIva
        {
            public int Id { get; set; }
            public double BaseImp { get; set; }
            public double Importe { get; set; }
        }
    }
}
