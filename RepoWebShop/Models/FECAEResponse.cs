using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class FECAEResponse
    {
        public IEnumerable<CodeMessage> Errs { get; }
        public IEnumerable<CodeMessage> Events { get; }
        public FECAECabResponse FeCabResp { get; }
        public IEnumerable<FECAEDetResponse> FeDetResp { get; }

        public FECAEResponse(IEnumerable<CodeMessage> errs, IEnumerable<CodeMessage> events, FECAECabResponse feCabResp, IEnumerable<FECAEDetResponse> feDetResp)
        {
            Errs = errs;
            Events = events;
            FeCabResp = feCabResp;
            FeDetResp = feDetResp;
        }

        public class FECAEDetResponse
        {
            public int Concepto { get; set; }
            public int DocTipo { get; set; }
            public long DocNro { get; set; }
            public long CbteDesde { get; set; }
            public long CbteHasta { get; set; }
            public string CbteFch { get; set; }
            public string Resultado { get; set; }
            public string CAE { get; set; }
            public string CAEFchVto { get; set; }
            public double ImpTotal { get; set; }
            public IEnumerable<CodeMessage> Observaciones { get; set; }
        }

        public class FECAECabResponse
        {
            public long Cuit { get; set; }
            public int PtoVta { get; set; }
            public int CbteTipo { get; set; }
            public string FchProceso { get; set; }
            public int CantReg { get; set; }
            public string Resultado { get; set; }
            public string Reproceso { get; set; }
        }

        public class CodeMessage
        {
            public int Code { get; set; }
            public string Msg { get; set; }
        }
    }
}
