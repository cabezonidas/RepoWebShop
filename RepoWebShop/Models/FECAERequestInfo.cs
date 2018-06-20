using System;
using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class FECAERequestInfo
    {
        public FECAERequestInfo(PayerDataRevenue payerData, DateTime cbteFecha, int iva, long cuit, string sign, string token, int ptoVta)
        {
            DocTipo = payerData.DocTipo;
            DocNro = payerData.DocNro; 
            ImpTotal = Convert.ToDouble(Decimal.Round(payerData.OrderTotal, 2));
            Ivapercentage = Decimal.Round(iva / 100m, 2);
            BaseImpositiva = Convert.ToDouble(Decimal.Round(payerData.OrderTotal / (1 + Ivapercentage), 2));
            ImporteIva = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(ImpTotal - BaseImpositiva), 2));
            CbteFecha = cbteFecha.ToString("yyyyMMdd");

            AuthRequest = new FEAuthRequest { Cuit = cuit, Sign = sign, Token = token };
            FeCabReq = new FECAECabRequest { PtoVta = ptoVta, CantReg = 1, CbteTipo = payerData.CbteTipo };
            Alicuotas = new List<FECAEDetRequest.AlicIva> { new FECAEDetRequest.AlicIva { Id = 5 /*21%*/, BaseImp = BaseImpositiva, Importe = ImporteIva } };
        }


        public decimal Ivapercentage { get; }
        public double BaseImpositiva { get; }
        public double ImporteIva { get; }
        public string CbteFecha { get; }
        public FEAuthRequest AuthRequest { get; }
        public FECAECabRequest FeCabReq { get; }
        public List<FECAEDetRequest.AlicIva> Alicuotas { get; }
        public int DocTipo { get; }
        public long DocNro { get; }
        public long CbteDesde { get; internal set; }
        public long CbteHasta { get; internal set; }
        public double ImpTotal { get; internal set; }
    }
}
