//------------------------------------------------------------------------------
// <generado automáticamente>
//     Este código fue generado por una herramienta.
//     //
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </generado automáticamente>
//------------------------------------------------------------------------------

namespace ElectronicInvoiceProd
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEAuthRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEAuthRequest : object
    {
        
        private string TokenField;
        
        private string SignField;
        
        private long CuitField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Token
        {
            get
            {
                return this.TokenField;
            }
            set
            {
                this.TokenField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Sign
        {
            get
            {
                return this.SignField;
            }
            set
            {
                this.SignField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public long Cuit
        {
            get
            {
                return this.CuitField;
            }
            set
            {
                this.CuitField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAERequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAERequest : object
    {
        
        private ElectronicInvoiceProd.FECAECabRequest FeCabReqField;
        
        private ElectronicInvoiceProd.FECAEDetRequest[] FeDetReqField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAECabRequest FeCabReq
        {
            get
            {
                return this.FeCabReqField;
            }
            set
            {
                this.FeCabReqField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEDetRequest[] FeDetReq
        {
            get
            {
                return this.FeDetReqField;
            }
            set
            {
                this.FeDetReqField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAECabRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAECabRequest : ElectronicInvoiceProd.FECabRequest
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEDetRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECompConsResponse))]
    public partial class FECAEDetRequest : ElectronicInvoiceProd.FEDetRequest
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECabRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEACabRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAECabRequest))]
    public partial class FECabRequest : object
    {
        
        private int CantRegField;
        
        private int PtoVtaField;
        
        private int CbteTipoField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int CantReg
        {
            get
            {
                return this.CantRegField;
            }
            set
            {
                this.CantRegField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int CbteTipo
        {
            get
            {
                return this.CbteTipoField;
            }
            set
            {
                this.CbteTipoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEACabRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEACabRequest : ElectronicInvoiceProd.FECabRequest
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEDetRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEADetRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEDetRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECompConsResponse))]
    public partial class FEDetRequest : object
    {
        
        private int ConceptoField;
        
        private int DocTipoField;
        
        private long DocNroField;
        
        private long CbteDesdeField;
        
        private long CbteHastaField;
        
        private string CbteFchField;
        
        private double ImpTotalField;
        
        private double ImpTotConcField;
        
        private double ImpNetoField;
        
        private double ImpOpExField;
        
        private double ImpTribField;
        
        private double ImpIVAField;
        
        private string FchServDesdeField;
        
        private string FchServHastaField;
        
        private string FchVtoPagoField;
        
        private string MonIdField;
        
        private double MonCotizField;
        
        private ElectronicInvoiceProd.CbteAsoc[] CbtesAsocField;
        
        private ElectronicInvoiceProd.Tributo[] TributosField;
        
        private ElectronicInvoiceProd.AlicIva[] IvaField;
        
        private ElectronicInvoiceProd.Opcional[] OpcionalesField;
        
        private ElectronicInvoiceProd.Comprador[] CompradoresField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Concepto
        {
            get
            {
                return this.ConceptoField;
            }
            set
            {
                this.ConceptoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int DocTipo
        {
            get
            {
                return this.DocTipoField;
            }
            set
            {
                this.DocTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public long DocNro
        {
            get
            {
                return this.DocNroField;
            }
            set
            {
                this.DocNroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public long CbteDesde
        {
            get
            {
                return this.CbteDesdeField;
            }
            set
            {
                this.CbteDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public long CbteHasta
        {
            get
            {
                return this.CbteHastaField;
            }
            set
            {
                this.CbteHastaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string CbteFch
        {
            get
            {
                return this.CbteFchField;
            }
            set
            {
                this.CbteFchField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public double ImpTotal
        {
            get
            {
                return this.ImpTotalField;
            }
            set
            {
                this.ImpTotalField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=7)]
        public double ImpTotConc
        {
            get
            {
                return this.ImpTotConcField;
            }
            set
            {
                this.ImpTotConcField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=8)]
        public double ImpNeto
        {
            get
            {
                return this.ImpNetoField;
            }
            set
            {
                this.ImpNetoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=9)]
        public double ImpOpEx
        {
            get
            {
                return this.ImpOpExField;
            }
            set
            {
                this.ImpOpExField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=10)]
        public double ImpTrib
        {
            get
            {
                return this.ImpTribField;
            }
            set
            {
                this.ImpTribField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=11)]
        public double ImpIVA
        {
            get
            {
                return this.ImpIVAField;
            }
            set
            {
                this.ImpIVAField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string FchServDesde
        {
            get
            {
                return this.FchServDesdeField;
            }
            set
            {
                this.FchServDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string FchServHasta
        {
            get
            {
                return this.FchServHastaField;
            }
            set
            {
                this.FchServHastaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string FchVtoPago
        {
            get
            {
                return this.FchVtoPagoField;
            }
            set
            {
                this.FchVtoPagoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=15)]
        public string MonId
        {
            get
            {
                return this.MonIdField;
            }
            set
            {
                this.MonIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=16)]
        public double MonCotiz
        {
            get
            {
                return this.MonCotizField;
            }
            set
            {
                this.MonCotizField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=17)]
        public ElectronicInvoiceProd.CbteAsoc[] CbtesAsoc
        {
            get
            {
                return this.CbtesAsocField;
            }
            set
            {
                this.CbtesAsocField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=18)]
        public ElectronicInvoiceProd.Tributo[] Tributos
        {
            get
            {
                return this.TributosField;
            }
            set
            {
                this.TributosField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=19)]
        public ElectronicInvoiceProd.AlicIva[] Iva
        {
            get
            {
                return this.IvaField;
            }
            set
            {
                this.IvaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=20)]
        public ElectronicInvoiceProd.Opcional[] Opcionales
        {
            get
            {
                return this.OpcionalesField;
            }
            set
            {
                this.OpcionalesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=21)]
        public ElectronicInvoiceProd.Comprador[] Compradores
        {
            get
            {
                return this.CompradoresField;
            }
            set
            {
                this.CompradoresField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEADetRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEADetRequest : ElectronicInvoiceProd.FEDetRequest
    {
        
        private string CAEAField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CAEA
        {
            get
            {
                return this.CAEAField;
            }
            set
            {
                this.CAEAField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECompConsResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompConsResponse : ElectronicInvoiceProd.FECAEDetRequest
    {
        
        private string ResultadoField;
        
        private string CodAutorizacionField;
        
        private string EmisionTipoField;
        
        private string FchVtoField;
        
        private string FchProcesoField;
        
        private ElectronicInvoiceProd.Obs[] ObservacionesField;
        
        private int PtoVtaField;
        
        private int CbteTipoField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Resultado
        {
            get
            {
                return this.ResultadoField;
            }
            set
            {
                this.ResultadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string CodAutorizacion
        {
            get
            {
                return this.CodAutorizacionField;
            }
            set
            {
                this.CodAutorizacionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string EmisionTipo
        {
            get
            {
                return this.EmisionTipoField;
            }
            set
            {
                this.EmisionTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchVto
        {
            get
            {
                return this.FchVtoField;
            }
            set
            {
                this.FchVtoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string FchProceso
        {
            get
            {
                return this.FchProcesoField;
            }
            set
            {
                this.FchProcesoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public ElectronicInvoiceProd.Obs[] Observaciones
        {
            get
            {
                return this.ObservacionesField;
            }
            set
            {
                this.ObservacionesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=7)]
        public int CbteTipo
        {
            get
            {
                return this.CbteTipoField;
            }
            set
            {
                this.CbteTipoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CbteAsoc", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class CbteAsoc : object
    {
        
        private int TipoField;
        
        private int PtoVtaField;
        
        private long NroField;
        
        private string CuitField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Tipo
        {
            get
            {
                return this.TipoField;
            }
            set
            {
                this.TipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public long Nro
        {
            get
            {
                return this.NroField;
            }
            set
            {
                this.NroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Cuit
        {
            get
            {
                return this.CuitField;
            }
            set
            {
                this.CuitField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Tributo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Tributo : object
    {
        
        private short IdField;
        
        private string DescField;
        
        private double BaseImpField;
        
        private double AlicField;
        
        private double ImporteField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public short Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public double BaseImp
        {
            get
            {
                return this.BaseImpField;
            }
            set
            {
                this.BaseImpField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public double Alic
        {
            get
            {
                return this.AlicField;
            }
            set
            {
                this.AlicField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public double Importe
        {
            get
            {
                return this.ImporteField;
            }
            set
            {
                this.ImporteField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlicIva", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class AlicIva : object
    {
        
        private int IdField;
        
        private double BaseImpField;
        
        private double ImporteField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public double BaseImp
        {
            get
            {
                return this.BaseImpField;
            }
            set
            {
                this.BaseImpField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public double Importe
        {
            get
            {
                return this.ImporteField;
            }
            set
            {
                this.ImporteField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Opcional", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Opcional : object
    {
        
        private string IdField;
        
        private string ValorField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Valor
        {
            get
            {
                return this.ValorField;
            }
            set
            {
                this.ValorField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Comprador", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Comprador : object
    {
        
        private int DocTipoField;
        
        private long DocNroField;
        
        private double PorcentajeField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int DocTipo
        {
            get
            {
                return this.DocTipoField;
            }
            set
            {
                this.DocTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public long DocNro
        {
            get
            {
                return this.DocNroField;
            }
            set
            {
                this.DocNroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public double Porcentaje
        {
            get
            {
                return this.PorcentajeField;
            }
            set
            {
                this.PorcentajeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Obs", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Obs : object
    {
        
        private int CodeField;
        
        private string MsgField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                this.CodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Msg
        {
            get
            {
                return this.MsgField;
            }
            set
            {
                this.MsgField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEResponse : object
    {
        
        private ElectronicInvoiceProd.FECAECabResponse FeCabRespField;
        
        private ElectronicInvoiceProd.FECAEDetResponse[] FeDetRespField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAECabResponse FeCabResp
        {
            get
            {
                return this.FeCabRespField;
            }
            set
            {
                this.FeCabRespField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEDetResponse[] FeDetResp
        {
            get
            {
                return this.FeDetRespField;
            }
            set
            {
                this.FeDetRespField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAECabResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAECabResponse : ElectronicInvoiceProd.FECabResponse
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEDetResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEDetResponse : ElectronicInvoiceProd.FEDetResponse
    {
        
        private string CAEField;
        
        private string CAEFchVtoField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CAE
        {
            get
            {
                return this.CAEField;
            }
            set
            {
                this.CAEField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CAEFchVto
        {
            get
            {
                return this.CAEFchVtoField;
            }
            set
            {
                this.CAEFchVtoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Evt", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Evt : object
    {
        
        private int CodeField;
        
        private string MsgField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                this.CodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Msg
        {
            get
            {
                return this.MsgField;
            }
            set
            {
                this.MsgField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Err", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Err : object
    {
        
        private int CodeField;
        
        private string MsgField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                this.CodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Msg
        {
            get
            {
                return this.MsgField;
            }
            set
            {
                this.MsgField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECabResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEACabResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAECabResponse))]
    public partial class FECabResponse : object
    {
        
        private long CuitField;
        
        private int PtoVtaField;
        
        private int CbteTipoField;
        
        private string FchProcesoField;
        
        private int CantRegField;
        
        private string ResultadoField;
        
        private string ReprocesoField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long Cuit
        {
            get
            {
                return this.CuitField;
            }
            set
            {
                this.CuitField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int CbteTipo
        {
            get
            {
                return this.CbteTipoField;
            }
            set
            {
                this.CbteTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchProceso
        {
            get
            {
                return this.FchProcesoField;
            }
            set
            {
                this.FchProcesoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public int CantReg
        {
            get
            {
                return this.CantRegField;
            }
            set
            {
                this.CantRegField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Resultado
        {
            get
            {
                return this.ResultadoField;
            }
            set
            {
                this.ResultadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string Reproceso
        {
            get
            {
                return this.ReprocesoField;
            }
            set
            {
                this.ReprocesoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEACabResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEACabResponse : ElectronicInvoiceProd.FECabResponse
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEDetResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEADetResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEDetResponse))]
    public partial class FEDetResponse : object
    {
        
        private int ConceptoField;
        
        private int DocTipoField;
        
        private long DocNroField;
        
        private long CbteDesdeField;
        
        private long CbteHastaField;
        
        private string CbteFchField;
        
        private string ResultadoField;
        
        private ElectronicInvoiceProd.Obs[] ObservacionesField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Concepto
        {
            get
            {
                return this.ConceptoField;
            }
            set
            {
                this.ConceptoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int DocTipo
        {
            get
            {
                return this.DocTipoField;
            }
            set
            {
                this.DocTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public long DocNro
        {
            get
            {
                return this.DocNroField;
            }
            set
            {
                this.DocNroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public long CbteDesde
        {
            get
            {
                return this.CbteDesdeField;
            }
            set
            {
                this.CbteDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public long CbteHasta
        {
            get
            {
                return this.CbteHastaField;
            }
            set
            {
                this.CbteHastaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string CbteFch
        {
            get
            {
                return this.CbteFchField;
            }
            set
            {
                this.CbteFchField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string Resultado
        {
            get
            {
                return this.ResultadoField;
            }
            set
            {
                this.ResultadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public ElectronicInvoiceProd.Obs[] Observaciones
        {
            get
            {
                return this.ObservacionesField;
            }
            set
            {
                this.ObservacionesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEADetResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEADetResponse : ElectronicInvoiceProd.FEDetResponse
    {
        
        private string CAEAField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CAEA
        {
            get
            {
                return this.CAEAField;
            }
            set
            {
                this.CAEAField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FERegXReqResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FERegXReqResponse : object
    {
        
        private int RegXReqField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int RegXReq
        {
            get
            {
                return this.RegXReqField;
            }
            set
            {
                this.RegXReqField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DummyResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class DummyResponse : object
    {
        
        private string AppServerField;
        
        private string DbServerField;
        
        private string AuthServerField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string AppServer
        {
            get
            {
                return this.AppServerField;
            }
            set
            {
                this.AppServerField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string DbServer
        {
            get
            {
                return this.DbServerField;
            }
            set
            {
                this.DbServerField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string AuthServer
        {
            get
            {
                return this.AuthServerField;
            }
            set
            {
                this.AuthServerField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FERecuperaLastCbteResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FERecuperaLastCbteResponse : object
    {
        
        private int PtoVtaField;
        
        private int CbteTipoField;
        
        private int CbteNroField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int CbteTipo
        {
            get
            {
                return this.CbteTipoField;
            }
            set
            {
                this.CbteTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int CbteNro
        {
            get
            {
                return this.CbteNroField;
            }
            set
            {
                this.CbteNroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECompConsultaReq", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompConsultaReq : object
    {
        
        private int CbteTipoField;
        
        private long CbteNroField;
        
        private int PtoVtaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int CbteTipo
        {
            get
            {
                return this.CbteTipoField;
            }
            set
            {
                this.CbteTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public long CbteNro
        {
            get
            {
                return this.CbteNroField;
            }
            set
            {
                this.CbteNroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECompConsultaResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompConsultaResponse : object
    {
        
        private ElectronicInvoiceProd.FECompConsResponse ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECompConsResponse ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEARequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEARequest : object
    {
        
        private ElectronicInvoiceProd.FECAEACabRequest FeCabReqField;
        
        private ElectronicInvoiceProd.FECAEADetRequest[] FeDetReqField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEACabRequest FeCabReq
        {
            get
            {
                return this.FeCabReqField;
            }
            set
            {
                this.FeCabReqField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEADetRequest[] FeDetReq
        {
            get
            {
                return this.FeDetReqField;
            }
            set
            {
                this.FeDetReqField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEAResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEAResponse : object
    {
        
        private ElectronicInvoiceProd.FECAEACabResponse FeCabRespField;
        
        private ElectronicInvoiceProd.FECAEADetResponse[] FeDetRespField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEACabResponse FeCabResp
        {
            get
            {
                return this.FeCabRespField;
            }
            set
            {
                this.FeCabRespField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEADetResponse[] FeDetResp
        {
            get
            {
                return this.FeDetRespField;
            }
            set
            {
                this.FeDetRespField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEAGetResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEAGetResponse : object
    {
        
        private ElectronicInvoiceProd.FECAEAGet ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEAGet ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEAGet", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEAGet : object
    {
        
        private string CAEAField;
        
        private int PeriodoField;
        
        private short OrdenField;
        
        private string FchVigDesdeField;
        
        private string FchVigHastaField;
        
        private string FchTopeInfField;
        
        private string FchProcesoField;
        
        private ElectronicInvoiceProd.Obs[] ObservacionesField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CAEA
        {
            get
            {
                return this.CAEAField;
            }
            set
            {
                this.CAEAField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Periodo
        {
            get
            {
                return this.PeriodoField;
            }
            set
            {
                this.PeriodoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public short Orden
        {
            get
            {
                return this.OrdenField;
            }
            set
            {
                this.OrdenField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchVigDesde
        {
            get
            {
                return this.FchVigDesdeField;
            }
            set
            {
                this.FchVigDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string FchVigHasta
        {
            get
            {
                return this.FchVigHastaField;
            }
            set
            {
                this.FchVigHastaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string FchTopeInf
        {
            get
            {
                return this.FchTopeInfField;
            }
            set
            {
                this.FchTopeInfField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string FchProceso
        {
            get
            {
                return this.FchProcesoField;
            }
            set
            {
                this.FchProcesoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public ElectronicInvoiceProd.Obs[] Observaciones
        {
            get
            {
                return this.ObservacionesField;
            }
            set
            {
                this.ObservacionesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEASinMovConsResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASinMovConsResponse : object
    {
        
        private ElectronicInvoiceProd.FECAEASinMov[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.FECAEASinMov[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEASinMov", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceProd.FECAEASinMovResponse))]
    public partial class FECAEASinMov : object
    {
        
        private string CAEAField;
        
        private string FchProcesoField;
        
        private int PtoVtaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CAEA
        {
            get
            {
                return this.CAEAField;
            }
            set
            {
                this.CAEAField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string FchProceso
        {
            get
            {
                return this.FchProcesoField;
            }
            set
            {
                this.FchProcesoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int PtoVta
        {
            get
            {
                return this.PtoVtaField;
            }
            set
            {
                this.PtoVtaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEASinMovResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASinMovResponse : ElectronicInvoiceProd.FECAEASinMov
    {
        
        private string ResultadoField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Resultado
        {
            get
            {
                return this.ResultadoField;
            }
            set
            {
                this.ResultadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECotizacionResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECotizacionResponse : object
    {
        
        private ElectronicInvoiceProd.Cotizacion ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.Cotizacion ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Cotizacion", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Cotizacion : object
    {
        
        private string MonIdField;
        
        private double MonCotizField;
        
        private string FchCotizField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string MonId
        {
            get
            {
                return this.MonIdField;
            }
            set
            {
                this.MonIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public double MonCotiz
        {
            get
            {
                return this.MonCotizField;
            }
            set
            {
                this.MonCotizField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchCotiz
        {
            get
            {
                return this.FchCotizField;
            }
            set
            {
                this.FchCotizField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FETributoResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FETributoResponse : object
    {
        
        private ElectronicInvoiceProd.TributoTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.TributoTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TributoTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class TributoTipo : object
    {
        
        private short IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public short Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MonedaResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class MonedaResponse : object
    {
        
        private ElectronicInvoiceProd.Moneda[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.Moneda[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Moneda", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class Moneda : object
    {
        
        private string IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="IvaTipoResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class IvaTipoResponse : object
    {
        
        private ElectronicInvoiceProd.IvaTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.IvaTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="IvaTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class IvaTipo : object
    {
        
        private string IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OpcionalTipoResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class OpcionalTipoResponse : object
    {
        
        private ElectronicInvoiceProd.OpcionalTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.OpcionalTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OpcionalTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class OpcionalTipo : object
    {
        
        private string IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConceptoTipoResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class ConceptoTipoResponse : object
    {
        
        private ElectronicInvoiceProd.ConceptoTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.ConceptoTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConceptoTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class ConceptoTipo : object
    {
        
        private int IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEPtoVentaResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEPtoVentaResponse : object
    {
        
        private ElectronicInvoiceProd.PtoVenta[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.PtoVenta[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PtoVenta", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class PtoVenta : object
    {
        
        private short NroField;
        
        private string EmisionTipoField;
        
        private string BloqueadoField;
        
        private string FchBajaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public short Nro
        {
            get
            {
                return this.NroField;
            }
            set
            {
                this.NroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string EmisionTipo
        {
            get
            {
                return this.EmisionTipoField;
            }
            set
            {
                this.EmisionTipoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Bloqueado
        {
            get
            {
                return this.BloqueadoField;
            }
            set
            {
                this.BloqueadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchBaja
        {
            get
            {
                return this.FchBajaField;
            }
            set
            {
                this.FchBajaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CbteTipoResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class CbteTipoResponse : object
    {
        
        private ElectronicInvoiceProd.CbteTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.CbteTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CbteTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class CbteTipo : object
    {
        
        private int IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocTipoResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class DocTipoResponse : object
    {
        
        private ElectronicInvoiceProd.DocTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.DocTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class DocTipo : object
    {
        
        private int IdField;
        
        private string DescField;
        
        private string FchDesdeField;
        
        private string FchHastaField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FchDesde
        {
            get
            {
                return this.FchDesdeField;
            }
            set
            {
                this.FchDesdeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string FchHasta
        {
            get
            {
                return this.FchHastaField;
            }
            set
            {
                this.FchHastaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEPaisResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEPaisResponse : object
    {
        
        private ElectronicInvoiceProd.PaisTipo[] ResultGetField;
        
        private ElectronicInvoiceProd.Err[] ErrorsField;
        
        private ElectronicInvoiceProd.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceProd.PaisTipo[] ResultGet
        {
            get
            {
                return this.ResultGetField;
            }
            set
            {
                this.ResultGetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.Err[] Errors
        {
            get
            {
                return this.ErrorsField;
            }
            set
            {
                this.ErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public ElectronicInvoiceProd.Evt[] Events
        {
            get
            {
                return this.EventsField;
            }
            set
            {
                this.EventsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PaisTipo", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class PaisTipo : object
    {
        
        private short IdField;
        
        private string DescField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public short Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Desc
        {
            get
            {
                return this.DescField;
            }
            set
            {
                this.DescField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/", ConfigurationName="ElectronicInvoiceProd.ServiceSoap")]
    public interface ServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAESolicitar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAESolicitarResponse> FECAESolicitarAsync(ElectronicInvoiceProd.FECAESolicitarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECompTotXRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompTotXRequestResponse> FECompTotXRequestAsync(ElectronicInvoiceProd.FECompTotXRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEDummy", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEDummyResponse> FEDummyAsync(ElectronicInvoiceProd.FEDummyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECompUltimoAutorizado", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompUltimoAutorizadoResponse> FECompUltimoAutorizadoAsync(ElectronicInvoiceProd.FECompUltimoAutorizadoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECompConsultar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompConsultarResponse> FECompConsultarAsync(ElectronicInvoiceProd.FECompConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEARegInformativo", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEARegInformativoResponse> FECAEARegInformativoAsync(ElectronicInvoiceProd.FECAEARegInformativoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEASolicitar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASolicitarResponse> FECAEASolicitarAsync(ElectronicInvoiceProd.FECAEASolicitarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEASinMovimientoConsultar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASinMovimientoConsultarResponse> FECAEASinMovimientoConsultarAsync(ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEASinMovimientoInformar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASinMovimientoInformarResponse> FECAEASinMovimientoInformarAsync(ElectronicInvoiceProd.FECAEASinMovimientoInformarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEAConsultar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEAConsultarResponse> FECAEAConsultarAsync(ElectronicInvoiceProd.FECAEAConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetCotizacion", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetCotizacionResponse> FEParamGetCotizacionAsync(ElectronicInvoiceProd.FEParamGetCotizacionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposTributos", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposTributosResponse> FEParamGetTiposTributosAsync(ElectronicInvoiceProd.FEParamGetTiposTributosRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposMonedas", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposMonedasResponse> FEParamGetTiposMonedasAsync(ElectronicInvoiceProd.FEParamGetTiposMonedasRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposIva", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposIvaResponse> FEParamGetTiposIvaAsync(ElectronicInvoiceProd.FEParamGetTiposIvaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposOpcional", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposOpcionalResponse> FEParamGetTiposOpcionalAsync(ElectronicInvoiceProd.FEParamGetTiposOpcionalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposConcepto", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposConceptoResponse> FEParamGetTiposConceptoAsync(ElectronicInvoiceProd.FEParamGetTiposConceptoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetPtosVenta", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetPtosVentaResponse> FEParamGetPtosVentaAsync(ElectronicInvoiceProd.FEParamGetPtosVentaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposCbte", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposCbteResponse> FEParamGetTiposCbteAsync(ElectronicInvoiceProd.FEParamGetTiposCbteRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposDoc", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposDocResponse> FEParamGetTiposDocAsync(ElectronicInvoiceProd.FEParamGetTiposDocRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposPaises", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposPaisesResponse> FEParamGetTiposPaisesAsync(ElectronicInvoiceProd.FEParamGetTiposPaisesRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAESolicitarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAESolicitar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAESolicitarRequestBody Body;
        
        public FECAESolicitarRequest()
        {
        }
        
        public FECAESolicitarRequest(ElectronicInvoiceProd.FECAESolicitarRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAESolicitarRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.FECAERequest FeCAEReq;
        
        public FECAESolicitarRequestBody()
        {
        }
        
        public FECAESolicitarRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, ElectronicInvoiceProd.FECAERequest FeCAEReq)
        {
            this.Auth = Auth;
            this.FeCAEReq = FeCAEReq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAESolicitarResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAESolicitarResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAESolicitarResponseBody Body;
        
        public FECAESolicitarResponse()
        {
        }
        
        public FECAESolicitarResponse(ElectronicInvoiceProd.FECAESolicitarResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAESolicitarResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECAEResponse FECAESolicitarResult;
        
        public FECAESolicitarResponseBody()
        {
        }
        
        public FECAESolicitarResponseBody(ElectronicInvoiceProd.FECAEResponse FECAESolicitarResult)
        {
            this.FECAESolicitarResult = FECAESolicitarResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECompTotXRequestRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECompTotXRequest", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECompTotXRequestRequestBody Body;
        
        public FECompTotXRequestRequest()
        {
        }
        
        public FECompTotXRequestRequest(ElectronicInvoiceProd.FECompTotXRequestRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompTotXRequestRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FECompTotXRequestRequestBody()
        {
        }
        
        public FECompTotXRequestRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECompTotXRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECompTotXRequestResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECompTotXRequestResponseBody Body;
        
        public FECompTotXRequestResponse()
        {
        }
        
        public FECompTotXRequestResponse(ElectronicInvoiceProd.FECompTotXRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompTotXRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FERegXReqResponse FECompTotXRequestResult;
        
        public FECompTotXRequestResponseBody()
        {
        }
        
        public FECompTotXRequestResponseBody(ElectronicInvoiceProd.FERegXReqResponse FECompTotXRequestResult)
        {
            this.FECompTotXRequestResult = FECompTotXRequestResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEDummyRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEDummy", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEDummyRequestBody Body;
        
        public FEDummyRequest()
        {
        }
        
        public FEDummyRequest(ElectronicInvoiceProd.FEDummyRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class FEDummyRequestBody
    {
        
        public FEDummyRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEDummyResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEDummyResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEDummyResponseBody Body;
        
        public FEDummyResponse()
        {
        }
        
        public FEDummyResponse(ElectronicInvoiceProd.FEDummyResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEDummyResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.DummyResponse FEDummyResult;
        
        public FEDummyResponseBody()
        {
        }
        
        public FEDummyResponseBody(ElectronicInvoiceProd.DummyResponse FEDummyResult)
        {
            this.FEDummyResult = FEDummyResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECompUltimoAutorizadoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECompUltimoAutorizado", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECompUltimoAutorizadoRequestBody Body;
        
        public FECompUltimoAutorizadoRequest()
        {
        }
        
        public FECompUltimoAutorizadoRequest(ElectronicInvoiceProd.FECompUltimoAutorizadoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompUltimoAutorizadoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int PtoVta;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int CbteTipo;
        
        public FECompUltimoAutorizadoRequestBody()
        {
        }
        
        public FECompUltimoAutorizadoRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, int PtoVta, int CbteTipo)
        {
            this.Auth = Auth;
            this.PtoVta = PtoVta;
            this.CbteTipo = CbteTipo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECompUltimoAutorizadoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECompUltimoAutorizadoResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECompUltimoAutorizadoResponseBody Body;
        
        public FECompUltimoAutorizadoResponse()
        {
        }
        
        public FECompUltimoAutorizadoResponse(ElectronicInvoiceProd.FECompUltimoAutorizadoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompUltimoAutorizadoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FERecuperaLastCbteResponse FECompUltimoAutorizadoResult;
        
        public FECompUltimoAutorizadoResponseBody()
        {
        }
        
        public FECompUltimoAutorizadoResponseBody(ElectronicInvoiceProd.FERecuperaLastCbteResponse FECompUltimoAutorizadoResult)
        {
            this.FECompUltimoAutorizadoResult = FECompUltimoAutorizadoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECompConsultarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECompConsultar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECompConsultarRequestBody Body;
        
        public FECompConsultarRequest()
        {
        }
        
        public FECompConsultarRequest(ElectronicInvoiceProd.FECompConsultarRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompConsultarRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.FECompConsultaReq FeCompConsReq;
        
        public FECompConsultarRequestBody()
        {
        }
        
        public FECompConsultarRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, ElectronicInvoiceProd.FECompConsultaReq FeCompConsReq)
        {
            this.Auth = Auth;
            this.FeCompConsReq = FeCompConsReq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECompConsultarResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECompConsultarResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECompConsultarResponseBody Body;
        
        public FECompConsultarResponse()
        {
        }
        
        public FECompConsultarResponse(ElectronicInvoiceProd.FECompConsultarResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECompConsultarResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECompConsultaResponse FECompConsultarResult;
        
        public FECompConsultarResponseBody()
        {
        }
        
        public FECompConsultarResponseBody(ElectronicInvoiceProd.FECompConsultaResponse FECompConsultarResult)
        {
            this.FECompConsultarResult = FECompConsultarResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEARegInformativoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEARegInformativo", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEARegInformativoRequestBody Body;
        
        public FECAEARegInformativoRequest()
        {
        }
        
        public FECAEARegInformativoRequest(ElectronicInvoiceProd.FECAEARegInformativoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEARegInformativoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceProd.FECAEARequest FeCAEARegInfReq;
        
        public FECAEARegInformativoRequestBody()
        {
        }
        
        public FECAEARegInformativoRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, ElectronicInvoiceProd.FECAEARequest FeCAEARegInfReq)
        {
            this.Auth = Auth;
            this.FeCAEARegInfReq = FeCAEARegInfReq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEARegInformativoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEARegInformativoResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEARegInformativoResponseBody Body;
        
        public FECAEARegInformativoResponse()
        {
        }
        
        public FECAEARegInformativoResponse(ElectronicInvoiceProd.FECAEARegInformativoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEARegInformativoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECAEAResponse FECAEARegInformativoResult;
        
        public FECAEARegInformativoResponseBody()
        {
        }
        
        public FECAEARegInformativoResponseBody(ElectronicInvoiceProd.FECAEAResponse FECAEARegInformativoResult)
        {
            this.FECAEARegInformativoResult = FECAEARegInformativoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEASolicitarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEASolicitar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEASolicitarRequestBody Body;
        
        public FECAEASolicitarRequest()
        {
        }
        
        public FECAEASolicitarRequest(ElectronicInvoiceProd.FECAEASolicitarRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASolicitarRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int Periodo;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public short Orden;
        
        public FECAEASolicitarRequestBody()
        {
        }
        
        public FECAEASolicitarRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, int Periodo, short Orden)
        {
            this.Auth = Auth;
            this.Periodo = Periodo;
            this.Orden = Orden;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEASolicitarResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEASolicitarResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEASolicitarResponseBody Body;
        
        public FECAEASolicitarResponse()
        {
        }
        
        public FECAEASolicitarResponse(ElectronicInvoiceProd.FECAEASolicitarResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASolicitarResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECAEAGetResponse FECAEASolicitarResult;
        
        public FECAEASolicitarResponseBody()
        {
        }
        
        public FECAEASolicitarResponseBody(ElectronicInvoiceProd.FECAEAGetResponse FECAEASolicitarResult)
        {
            this.FECAEASolicitarResult = FECAEASolicitarResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEASinMovimientoConsultarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEASinMovimientoConsultar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequestBody Body;
        
        public FECAEASinMovimientoConsultarRequest()
        {
        }
        
        public FECAEASinMovimientoConsultarRequest(ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASinMovimientoConsultarRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string CAEA;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int PtoVta;
        
        public FECAEASinMovimientoConsultarRequestBody()
        {
        }
        
        public FECAEASinMovimientoConsultarRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, string CAEA, int PtoVta)
        {
            this.Auth = Auth;
            this.CAEA = CAEA;
            this.PtoVta = PtoVta;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEASinMovimientoConsultarResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEASinMovimientoConsultarResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEASinMovimientoConsultarResponseBody Body;
        
        public FECAEASinMovimientoConsultarResponse()
        {
        }
        
        public FECAEASinMovimientoConsultarResponse(ElectronicInvoiceProd.FECAEASinMovimientoConsultarResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASinMovimientoConsultarResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECAEASinMovConsResponse FECAEASinMovimientoConsultarResult;
        
        public FECAEASinMovimientoConsultarResponseBody()
        {
        }
        
        public FECAEASinMovimientoConsultarResponseBody(ElectronicInvoiceProd.FECAEASinMovConsResponse FECAEASinMovimientoConsultarResult)
        {
            this.FECAEASinMovimientoConsultarResult = FECAEASinMovimientoConsultarResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEASinMovimientoInformarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEASinMovimientoInformar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEASinMovimientoInformarRequestBody Body;
        
        public FECAEASinMovimientoInformarRequest()
        {
        }
        
        public FECAEASinMovimientoInformarRequest(ElectronicInvoiceProd.FECAEASinMovimientoInformarRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASinMovimientoInformarRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int PtoVta;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string CAEA;
        
        public FECAEASinMovimientoInformarRequestBody()
        {
        }
        
        public FECAEASinMovimientoInformarRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, int PtoVta, string CAEA)
        {
            this.Auth = Auth;
            this.PtoVta = PtoVta;
            this.CAEA = CAEA;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEASinMovimientoInformarResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEASinMovimientoInformarResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEASinMovimientoInformarResponseBody Body;
        
        public FECAEASinMovimientoInformarResponse()
        {
        }
        
        public FECAEASinMovimientoInformarResponse(ElectronicInvoiceProd.FECAEASinMovimientoInformarResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEASinMovimientoInformarResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECAEASinMovResponse FECAEASinMovimientoInformarResult;
        
        public FECAEASinMovimientoInformarResponseBody()
        {
        }
        
        public FECAEASinMovimientoInformarResponseBody(ElectronicInvoiceProd.FECAEASinMovResponse FECAEASinMovimientoInformarResult)
        {
            this.FECAEASinMovimientoInformarResult = FECAEASinMovimientoInformarResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEAConsultarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEAConsultar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEAConsultarRequestBody Body;
        
        public FECAEAConsultarRequest()
        {
        }
        
        public FECAEAConsultarRequest(ElectronicInvoiceProd.FECAEAConsultarRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEAConsultarRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int Periodo;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public short Orden;
        
        public FECAEAConsultarRequestBody()
        {
        }
        
        public FECAEAConsultarRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, int Periodo, short Orden)
        {
            this.Auth = Auth;
            this.Periodo = Periodo;
            this.Orden = Orden;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAEAConsultarResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAEAConsultarResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FECAEAConsultarResponseBody Body;
        
        public FECAEAConsultarResponse()
        {
        }
        
        public FECAEAConsultarResponse(ElectronicInvoiceProd.FECAEAConsultarResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEAConsultarResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECAEAGetResponse FECAEAConsultarResult;
        
        public FECAEAConsultarResponseBody()
        {
        }
        
        public FECAEAConsultarResponseBody(ElectronicInvoiceProd.FECAEAGetResponse FECAEAConsultarResult)
        {
            this.FECAEAConsultarResult = FECAEAConsultarResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetCotizacionRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetCotizacion", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetCotizacionRequestBody Body;
        
        public FEParamGetCotizacionRequest()
        {
        }
        
        public FEParamGetCotizacionRequest(ElectronicInvoiceProd.FEParamGetCotizacionRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetCotizacionRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string MonId;
        
        public FEParamGetCotizacionRequestBody()
        {
        }
        
        public FEParamGetCotizacionRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth, string MonId)
        {
            this.Auth = Auth;
            this.MonId = MonId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetCotizacionResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetCotizacionResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetCotizacionResponseBody Body;
        
        public FEParamGetCotizacionResponse()
        {
        }
        
        public FEParamGetCotizacionResponse(ElectronicInvoiceProd.FEParamGetCotizacionResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetCotizacionResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FECotizacionResponse FEParamGetCotizacionResult;
        
        public FEParamGetCotizacionResponseBody()
        {
        }
        
        public FEParamGetCotizacionResponseBody(ElectronicInvoiceProd.FECotizacionResponse FEParamGetCotizacionResult)
        {
            this.FEParamGetCotizacionResult = FEParamGetCotizacionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposTributosRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposTributos", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposTributosRequestBody Body;
        
        public FEParamGetTiposTributosRequest()
        {
        }
        
        public FEParamGetTiposTributosRequest(ElectronicInvoiceProd.FEParamGetTiposTributosRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposTributosRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposTributosRequestBody()
        {
        }
        
        public FEParamGetTiposTributosRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposTributosResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposTributosResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposTributosResponseBody Body;
        
        public FEParamGetTiposTributosResponse()
        {
        }
        
        public FEParamGetTiposTributosResponse(ElectronicInvoiceProd.FEParamGetTiposTributosResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposTributosResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FETributoResponse FEParamGetTiposTributosResult;
        
        public FEParamGetTiposTributosResponseBody()
        {
        }
        
        public FEParamGetTiposTributosResponseBody(ElectronicInvoiceProd.FETributoResponse FEParamGetTiposTributosResult)
        {
            this.FEParamGetTiposTributosResult = FEParamGetTiposTributosResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposMonedasRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposMonedas", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposMonedasRequestBody Body;
        
        public FEParamGetTiposMonedasRequest()
        {
        }
        
        public FEParamGetTiposMonedasRequest(ElectronicInvoiceProd.FEParamGetTiposMonedasRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposMonedasRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposMonedasRequestBody()
        {
        }
        
        public FEParamGetTiposMonedasRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposMonedasResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposMonedasResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposMonedasResponseBody Body;
        
        public FEParamGetTiposMonedasResponse()
        {
        }
        
        public FEParamGetTiposMonedasResponse(ElectronicInvoiceProd.FEParamGetTiposMonedasResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposMonedasResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.MonedaResponse FEParamGetTiposMonedasResult;
        
        public FEParamGetTiposMonedasResponseBody()
        {
        }
        
        public FEParamGetTiposMonedasResponseBody(ElectronicInvoiceProd.MonedaResponse FEParamGetTiposMonedasResult)
        {
            this.FEParamGetTiposMonedasResult = FEParamGetTiposMonedasResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposIvaRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposIva", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposIvaRequestBody Body;
        
        public FEParamGetTiposIvaRequest()
        {
        }
        
        public FEParamGetTiposIvaRequest(ElectronicInvoiceProd.FEParamGetTiposIvaRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposIvaRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposIvaRequestBody()
        {
        }
        
        public FEParamGetTiposIvaRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposIvaResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposIvaResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposIvaResponseBody Body;
        
        public FEParamGetTiposIvaResponse()
        {
        }
        
        public FEParamGetTiposIvaResponse(ElectronicInvoiceProd.FEParamGetTiposIvaResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposIvaResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.IvaTipoResponse FEParamGetTiposIvaResult;
        
        public FEParamGetTiposIvaResponseBody()
        {
        }
        
        public FEParamGetTiposIvaResponseBody(ElectronicInvoiceProd.IvaTipoResponse FEParamGetTiposIvaResult)
        {
            this.FEParamGetTiposIvaResult = FEParamGetTiposIvaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposOpcionalRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposOpcional", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposOpcionalRequestBody Body;
        
        public FEParamGetTiposOpcionalRequest()
        {
        }
        
        public FEParamGetTiposOpcionalRequest(ElectronicInvoiceProd.FEParamGetTiposOpcionalRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposOpcionalRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposOpcionalRequestBody()
        {
        }
        
        public FEParamGetTiposOpcionalRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposOpcionalResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposOpcionalResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposOpcionalResponseBody Body;
        
        public FEParamGetTiposOpcionalResponse()
        {
        }
        
        public FEParamGetTiposOpcionalResponse(ElectronicInvoiceProd.FEParamGetTiposOpcionalResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposOpcionalResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.OpcionalTipoResponse FEParamGetTiposOpcionalResult;
        
        public FEParamGetTiposOpcionalResponseBody()
        {
        }
        
        public FEParamGetTiposOpcionalResponseBody(ElectronicInvoiceProd.OpcionalTipoResponse FEParamGetTiposOpcionalResult)
        {
            this.FEParamGetTiposOpcionalResult = FEParamGetTiposOpcionalResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposConceptoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposConcepto", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposConceptoRequestBody Body;
        
        public FEParamGetTiposConceptoRequest()
        {
        }
        
        public FEParamGetTiposConceptoRequest(ElectronicInvoiceProd.FEParamGetTiposConceptoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposConceptoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposConceptoRequestBody()
        {
        }
        
        public FEParamGetTiposConceptoRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposConceptoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposConceptoResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposConceptoResponseBody Body;
        
        public FEParamGetTiposConceptoResponse()
        {
        }
        
        public FEParamGetTiposConceptoResponse(ElectronicInvoiceProd.FEParamGetTiposConceptoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposConceptoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.ConceptoTipoResponse FEParamGetTiposConceptoResult;
        
        public FEParamGetTiposConceptoResponseBody()
        {
        }
        
        public FEParamGetTiposConceptoResponseBody(ElectronicInvoiceProd.ConceptoTipoResponse FEParamGetTiposConceptoResult)
        {
            this.FEParamGetTiposConceptoResult = FEParamGetTiposConceptoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetPtosVentaRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetPtosVenta", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetPtosVentaRequestBody Body;
        
        public FEParamGetPtosVentaRequest()
        {
        }
        
        public FEParamGetPtosVentaRequest(ElectronicInvoiceProd.FEParamGetPtosVentaRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetPtosVentaRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetPtosVentaRequestBody()
        {
        }
        
        public FEParamGetPtosVentaRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetPtosVentaResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetPtosVentaResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetPtosVentaResponseBody Body;
        
        public FEParamGetPtosVentaResponse()
        {
        }
        
        public FEParamGetPtosVentaResponse(ElectronicInvoiceProd.FEParamGetPtosVentaResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetPtosVentaResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEPtoVentaResponse FEParamGetPtosVentaResult;
        
        public FEParamGetPtosVentaResponseBody()
        {
        }
        
        public FEParamGetPtosVentaResponseBody(ElectronicInvoiceProd.FEPtoVentaResponse FEParamGetPtosVentaResult)
        {
            this.FEParamGetPtosVentaResult = FEParamGetPtosVentaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposCbteRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposCbte", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposCbteRequestBody Body;
        
        public FEParamGetTiposCbteRequest()
        {
        }
        
        public FEParamGetTiposCbteRequest(ElectronicInvoiceProd.FEParamGetTiposCbteRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposCbteRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposCbteRequestBody()
        {
        }
        
        public FEParamGetTiposCbteRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposCbteResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposCbteResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposCbteResponseBody Body;
        
        public FEParamGetTiposCbteResponse()
        {
        }
        
        public FEParamGetTiposCbteResponse(ElectronicInvoiceProd.FEParamGetTiposCbteResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposCbteResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.CbteTipoResponse FEParamGetTiposCbteResult;
        
        public FEParamGetTiposCbteResponseBody()
        {
        }
        
        public FEParamGetTiposCbteResponseBody(ElectronicInvoiceProd.CbteTipoResponse FEParamGetTiposCbteResult)
        {
            this.FEParamGetTiposCbteResult = FEParamGetTiposCbteResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposDocRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposDoc", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposDocRequestBody Body;
        
        public FEParamGetTiposDocRequest()
        {
        }
        
        public FEParamGetTiposDocRequest(ElectronicInvoiceProd.FEParamGetTiposDocRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposDocRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposDocRequestBody()
        {
        }
        
        public FEParamGetTiposDocRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposDocResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposDocResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposDocResponseBody Body;
        
        public FEParamGetTiposDocResponse()
        {
        }
        
        public FEParamGetTiposDocResponse(ElectronicInvoiceProd.FEParamGetTiposDocResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposDocResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.DocTipoResponse FEParamGetTiposDocResult;
        
        public FEParamGetTiposDocResponseBody()
        {
        }
        
        public FEParamGetTiposDocResponseBody(ElectronicInvoiceProd.DocTipoResponse FEParamGetTiposDocResult)
        {
            this.FEParamGetTiposDocResult = FEParamGetTiposDocResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposPaisesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposPaises", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposPaisesRequestBody Body;
        
        public FEParamGetTiposPaisesRequest()
        {
        }
        
        public FEParamGetTiposPaisesRequest(ElectronicInvoiceProd.FEParamGetTiposPaisesRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposPaisesRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEAuthRequest Auth;
        
        public FEParamGetTiposPaisesRequestBody()
        {
        }
        
        public FEParamGetTiposPaisesRequestBody(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            this.Auth = Auth;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FEParamGetTiposPaisesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FEParamGetTiposPaisesResponse", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceProd.FEParamGetTiposPaisesResponseBody Body;
        
        public FEParamGetTiposPaisesResponse()
        {
        }
        
        public FEParamGetTiposPaisesResponse(ElectronicInvoiceProd.FEParamGetTiposPaisesResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FEParamGetTiposPaisesResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ElectronicInvoiceProd.FEPaisResponse FEParamGetTiposPaisesResult;
        
        public FEParamGetTiposPaisesResponseBody()
        {
        }
        
        public FEParamGetTiposPaisesResponseBody(ElectronicInvoiceProd.FEPaisResponse FEParamGetTiposPaisesResult)
        {
            this.FEParamGetTiposPaisesResult = FEParamGetTiposPaisesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface ServiceSoapChannel : ElectronicInvoiceProd.ServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<ElectronicInvoiceProd.ServiceSoap>, ElectronicInvoiceProd.ServiceSoap
    {
        
    /// <summary>
    /// Implemente este método parcial para configurar el punto de conexión de servicio.
    /// </summary>
    /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
    /// <param name="clientCredentials">Credenciales de cliente</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), ServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAESolicitarResponse> ElectronicInvoiceProd.ServiceSoap.FECAESolicitarAsync(ElectronicInvoiceProd.FECAESolicitarRequest request)
        {
            return base.Channel.FECAESolicitarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAESolicitarResponse> FECAESolicitarAsync(ElectronicInvoiceProd.FEAuthRequest Auth, ElectronicInvoiceProd.FECAERequest FeCAEReq)
        {
            ElectronicInvoiceProd.FECAESolicitarRequest inValue = new ElectronicInvoiceProd.FECAESolicitarRequest();
            inValue.Body = new ElectronicInvoiceProd.FECAESolicitarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.FeCAEReq = FeCAEReq;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECAESolicitarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompTotXRequestResponse> ElectronicInvoiceProd.ServiceSoap.FECompTotXRequestAsync(ElectronicInvoiceProd.FECompTotXRequestRequest request)
        {
            return base.Channel.FECompTotXRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompTotXRequestResponse> FECompTotXRequestAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FECompTotXRequestRequest inValue = new ElectronicInvoiceProd.FECompTotXRequestRequest();
            inValue.Body = new ElectronicInvoiceProd.FECompTotXRequestRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECompTotXRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEDummyResponse> ElectronicInvoiceProd.ServiceSoap.FEDummyAsync(ElectronicInvoiceProd.FEDummyRequest request)
        {
            return base.Channel.FEDummyAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEDummyResponse> FEDummyAsync()
        {
            ElectronicInvoiceProd.FEDummyRequest inValue = new ElectronicInvoiceProd.FEDummyRequest();
            inValue.Body = new ElectronicInvoiceProd.FEDummyRequestBody();
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEDummyAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompUltimoAutorizadoResponse> ElectronicInvoiceProd.ServiceSoap.FECompUltimoAutorizadoAsync(ElectronicInvoiceProd.FECompUltimoAutorizadoRequest request)
        {
            return base.Channel.FECompUltimoAutorizadoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompUltimoAutorizadoResponse> FECompUltimoAutorizadoAsync(ElectronicInvoiceProd.FEAuthRequest Auth, int PtoVta, int CbteTipo)
        {
            ElectronicInvoiceProd.FECompUltimoAutorizadoRequest inValue = new ElectronicInvoiceProd.FECompUltimoAutorizadoRequest();
            inValue.Body = new ElectronicInvoiceProd.FECompUltimoAutorizadoRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.PtoVta = PtoVta;
            inValue.Body.CbteTipo = CbteTipo;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECompUltimoAutorizadoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompConsultarResponse> ElectronicInvoiceProd.ServiceSoap.FECompConsultarAsync(ElectronicInvoiceProd.FECompConsultarRequest request)
        {
            return base.Channel.FECompConsultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECompConsultarResponse> FECompConsultarAsync(ElectronicInvoiceProd.FEAuthRequest Auth, ElectronicInvoiceProd.FECompConsultaReq FeCompConsReq)
        {
            ElectronicInvoiceProd.FECompConsultarRequest inValue = new ElectronicInvoiceProd.FECompConsultarRequest();
            inValue.Body = new ElectronicInvoiceProd.FECompConsultarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.FeCompConsReq = FeCompConsReq;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECompConsultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEARegInformativoResponse> ElectronicInvoiceProd.ServiceSoap.FECAEARegInformativoAsync(ElectronicInvoiceProd.FECAEARegInformativoRequest request)
        {
            return base.Channel.FECAEARegInformativoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEARegInformativoResponse> FECAEARegInformativoAsync(ElectronicInvoiceProd.FEAuthRequest Auth, ElectronicInvoiceProd.FECAEARequest FeCAEARegInfReq)
        {
            ElectronicInvoiceProd.FECAEARegInformativoRequest inValue = new ElectronicInvoiceProd.FECAEARegInformativoRequest();
            inValue.Body = new ElectronicInvoiceProd.FECAEARegInformativoRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.FeCAEARegInfReq = FeCAEARegInfReq;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECAEARegInformativoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASolicitarResponse> ElectronicInvoiceProd.ServiceSoap.FECAEASolicitarAsync(ElectronicInvoiceProd.FECAEASolicitarRequest request)
        {
            return base.Channel.FECAEASolicitarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASolicitarResponse> FECAEASolicitarAsync(ElectronicInvoiceProd.FEAuthRequest Auth, int Periodo, short Orden)
        {
            ElectronicInvoiceProd.FECAEASolicitarRequest inValue = new ElectronicInvoiceProd.FECAEASolicitarRequest();
            inValue.Body = new ElectronicInvoiceProd.FECAEASolicitarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.Periodo = Periodo;
            inValue.Body.Orden = Orden;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECAEASolicitarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASinMovimientoConsultarResponse> ElectronicInvoiceProd.ServiceSoap.FECAEASinMovimientoConsultarAsync(ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequest request)
        {
            return base.Channel.FECAEASinMovimientoConsultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASinMovimientoConsultarResponse> FECAEASinMovimientoConsultarAsync(ElectronicInvoiceProd.FEAuthRequest Auth, string CAEA, int PtoVta)
        {
            ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequest inValue = new ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequest();
            inValue.Body = new ElectronicInvoiceProd.FECAEASinMovimientoConsultarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.CAEA = CAEA;
            inValue.Body.PtoVta = PtoVta;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECAEASinMovimientoConsultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASinMovimientoInformarResponse> ElectronicInvoiceProd.ServiceSoap.FECAEASinMovimientoInformarAsync(ElectronicInvoiceProd.FECAEASinMovimientoInformarRequest request)
        {
            return base.Channel.FECAEASinMovimientoInformarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEASinMovimientoInformarResponse> FECAEASinMovimientoInformarAsync(ElectronicInvoiceProd.FEAuthRequest Auth, int PtoVta, string CAEA)
        {
            ElectronicInvoiceProd.FECAEASinMovimientoInformarRequest inValue = new ElectronicInvoiceProd.FECAEASinMovimientoInformarRequest();
            inValue.Body = new ElectronicInvoiceProd.FECAEASinMovimientoInformarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.PtoVta = PtoVta;
            inValue.Body.CAEA = CAEA;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECAEASinMovimientoInformarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEAConsultarResponse> ElectronicInvoiceProd.ServiceSoap.FECAEAConsultarAsync(ElectronicInvoiceProd.FECAEAConsultarRequest request)
        {
            return base.Channel.FECAEAConsultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FECAEAConsultarResponse> FECAEAConsultarAsync(ElectronicInvoiceProd.FEAuthRequest Auth, int Periodo, short Orden)
        {
            ElectronicInvoiceProd.FECAEAConsultarRequest inValue = new ElectronicInvoiceProd.FECAEAConsultarRequest();
            inValue.Body = new ElectronicInvoiceProd.FECAEAConsultarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.Periodo = Periodo;
            inValue.Body.Orden = Orden;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FECAEAConsultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetCotizacionResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetCotizacionAsync(ElectronicInvoiceProd.FEParamGetCotizacionRequest request)
        {
            return base.Channel.FEParamGetCotizacionAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetCotizacionResponse> FEParamGetCotizacionAsync(ElectronicInvoiceProd.FEAuthRequest Auth, string MonId)
        {
            ElectronicInvoiceProd.FEParamGetCotizacionRequest inValue = new ElectronicInvoiceProd.FEParamGetCotizacionRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetCotizacionRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.MonId = MonId;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetCotizacionAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposTributosResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposTributosAsync(ElectronicInvoiceProd.FEParamGetTiposTributosRequest request)
        {
            return base.Channel.FEParamGetTiposTributosAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposTributosResponse> FEParamGetTiposTributosAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposTributosRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposTributosRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposTributosRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposTributosAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposMonedasResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposMonedasAsync(ElectronicInvoiceProd.FEParamGetTiposMonedasRequest request)
        {
            return base.Channel.FEParamGetTiposMonedasAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposMonedasResponse> FEParamGetTiposMonedasAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposMonedasRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposMonedasRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposMonedasRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposMonedasAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposIvaResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposIvaAsync(ElectronicInvoiceProd.FEParamGetTiposIvaRequest request)
        {
            return base.Channel.FEParamGetTiposIvaAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposIvaResponse> FEParamGetTiposIvaAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposIvaRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposIvaRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposIvaRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposIvaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposOpcionalResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposOpcionalAsync(ElectronicInvoiceProd.FEParamGetTiposOpcionalRequest request)
        {
            return base.Channel.FEParamGetTiposOpcionalAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposOpcionalResponse> FEParamGetTiposOpcionalAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposOpcionalRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposOpcionalRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposOpcionalRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposOpcionalAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposConceptoResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposConceptoAsync(ElectronicInvoiceProd.FEParamGetTiposConceptoRequest request)
        {
            return base.Channel.FEParamGetTiposConceptoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposConceptoResponse> FEParamGetTiposConceptoAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposConceptoRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposConceptoRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposConceptoRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposConceptoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetPtosVentaResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetPtosVentaAsync(ElectronicInvoiceProd.FEParamGetPtosVentaRequest request)
        {
            return base.Channel.FEParamGetPtosVentaAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetPtosVentaResponse> FEParamGetPtosVentaAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetPtosVentaRequest inValue = new ElectronicInvoiceProd.FEParamGetPtosVentaRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetPtosVentaRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetPtosVentaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposCbteResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposCbteAsync(ElectronicInvoiceProd.FEParamGetTiposCbteRequest request)
        {
            return base.Channel.FEParamGetTiposCbteAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposCbteResponse> FEParamGetTiposCbteAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposCbteRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposCbteRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposCbteRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposCbteAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposDocResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposDocAsync(ElectronicInvoiceProd.FEParamGetTiposDocRequest request)
        {
            return base.Channel.FEParamGetTiposDocAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposDocResponse> FEParamGetTiposDocAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposDocRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposDocRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposDocRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposDocAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposPaisesResponse> ElectronicInvoiceProd.ServiceSoap.FEParamGetTiposPaisesAsync(ElectronicInvoiceProd.FEParamGetTiposPaisesRequest request)
        {
            return base.Channel.FEParamGetTiposPaisesAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceProd.FEParamGetTiposPaisesResponse> FEParamGetTiposPaisesAsync(ElectronicInvoiceProd.FEAuthRequest Auth)
        {
            ElectronicInvoiceProd.FEParamGetTiposPaisesRequest inValue = new ElectronicInvoiceProd.FEParamGetTiposPaisesRequest();
            inValue.Body = new ElectronicInvoiceProd.FEParamGetTiposPaisesRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceProd.ServiceSoap)(this)).FEParamGetTiposPaisesAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("https://servicios1.afip.gov.ar/wsfev1/service.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://servicios1.afip.gov.ar/wsfev1/service.asmx");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ServiceSoap,
            
            ServiceSoap12,
        }
    }
}
