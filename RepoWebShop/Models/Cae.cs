using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Cae
    {
        public int CaeId { get; set; }
        public double ImpTotal { get; set; }
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

        [BindNever]
        public string FriendlyConcepto
        {
            get
            {
                if (Concepto == 1)
                    return "Productos";
                if (Concepto == 2)
                    return "Servicios";
                if (Concepto == 3)
                    return "Productos y Servicios";
                return Concepto.ToString();
            }
        }
        [BindNever]
        public string Documento
        {
            get {
                if (DocTipo == 80)
                    return "CUIT";
                if (DocTipo == 86)
                    return "CUIL";
                if (DocTipo == 87)
                    return "CDI";
                if (DocTipo == 96)
                    return "DNI";
                return DocTipo.ToString();
            }
        }

        [BindNever]
        public string FriendlyResultado
        {
            get
            {
                switch (Resultado)
                {
                    case "A":
                        return "A - Aprobado";
                    case "P":
                        return "P - Parcialmente Aprobado";
                    case "R":
                        return "R - Rechazado";
                    default:
                        return Resultado;
                }
            }
        }
    }
}
