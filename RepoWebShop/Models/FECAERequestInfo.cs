using System;
using System.Collections.Generic;
using ElectronicInvoiceProd;

namespace RepoWebShop.Models
{
    public class FECAERequestInfo
    {
        public FECAERequestInfo(PayerDataRevenue payerData, DateTime cbteFecha, int iva, long cuit, string sign, string token, int ptoVta)
        {

            DocTipo = payerData.DocTipo;
            DocNro = payerData.DocNro; 
            Ivapercentage = Decimal.Round(iva / 100m, 2);
            CbteFecha = cbteFecha.ToString("yyyyMMdd");

            Invoices = payerData.SplitInvoices;

            AuthRequest = new FEAuthRequest { Cuit = cuit, Sign = sign, Token = token };
            FeCabReq = new FECAECabRequest { PtoVta = ptoVta, CantReg = payerData.SplitInvoices.Length, CbteTipo = payerData.CbteTipo };
        }

        internal List<FECAEDetRequest> ToFECAEDetRequestList()
        {
            var result = new List<FECAEDetRequest>();
            for (int i = 0; i < Invoices.Length; i++)
            {
                var impTotal = Convert.ToDouble(Decimal.Round(Invoices[i], 2));
                var baseImpositiva = Convert.ToDouble(Decimal.Round(Invoices[i] / (1 + Ivapercentage), 2));
                var importeIva = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(impTotal - baseImpositiva), 2));
                var alicuotas = new List<FECAEDetRequest.AlicIva> { new FECAEDetRequest.AlicIva { Id = 5 /*21%*/, BaseImp = baseImpositiva, Importe = importeIva } };
                var detInfo = new FECAEDetRequest
                {
                    Concepto = 1,
                    /*
                        1 Productos
                        2 Servicios
                        3 Productos y Servicios
                     */
                    DocTipo = DocTipo,
                    DocNro = DocNro,
                    CbteDesde = CbteDesde + i,
                    CbteHasta = CbteDesde + i,
                    ImpTotal = Convert.ToDouble(Decimal.Round(Invoices[i], 2)),
                    ImpTotConc = 0,
                    ImpNeto = baseImpositiva,
                    ImpOpEx = 0,
                    ImpTrib = 0,
                    CbteFch = CbteFecha,
                    ImpIVA = importeIva,
                    MonId = "PES",
                    MonCotiz = 1,
                    Iva = alicuotas.ToArray(),
                };
                result.Add(detInfo); 
            }
            return result;
        }
        public string CbteFecha { get; }
        public decimal[] Invoices { get; }
        public FEAuthRequest AuthRequest { get; }
        public FECAECabRequest FeCabReq { get; }
        public List<FECAEDetRequest.AlicIva> Alicuotas { get; }
        public int DocTipo { get; }
        public long DocNro { get; }
        public decimal Ivapercentage { get; }
        public long CbteHasta { get; internal set; }
        public long CbteDesde { get; internal set; }
    }
}
