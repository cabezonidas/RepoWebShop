//------------------------------------------------------------------------------
// <generado automáticamente>
//     Este código fue generado por una herramienta.
//     //
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </generado automáticamente>
//------------------------------------------------------------------------------

namespace ElectronicInvoiceTest
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
        
        private ElectronicInvoiceTest.FECAECabRequest FeCabReqField;
        
        private ElectronicInvoiceTest.FECAEDetRequest[] FeDetReqField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECAECabRequest FeCabReq
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
        public ElectronicInvoiceTest.FECAEDetRequest[] FeDetReq
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
    public partial class FECAECabRequest : ElectronicInvoiceTest.FECabRequest
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEDetRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECompConsResponse))]
    public partial class FECAEDetRequest : ElectronicInvoiceTest.FEDetRequest
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECabRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEACabRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAECabRequest))]
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
    public partial class FECAEACabRequest : ElectronicInvoiceTest.FECabRequest
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEDetRequest", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEADetRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEDetRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECompConsResponse))]
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
        
        private ElectronicInvoiceTest.CbteAsoc[] CbtesAsocField;
        
        private ElectronicInvoiceTest.Tributo[] TributosField;
        
        private ElectronicInvoiceTest.AlicIva[] IvaField;
        
        private ElectronicInvoiceTest.Opcional[] OpcionalesField;
        
        private ElectronicInvoiceTest.Comprador[] CompradoresField;
        
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
        public ElectronicInvoiceTest.CbteAsoc[] CbtesAsoc
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
        public ElectronicInvoiceTest.Tributo[] Tributos
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
        public ElectronicInvoiceTest.AlicIva[] Iva
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
        public ElectronicInvoiceTest.Opcional[] Opcionales
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
        public ElectronicInvoiceTest.Comprador[] Compradores
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
    public partial class FECAEADetRequest : ElectronicInvoiceTest.FEDetRequest
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
    public partial class FECompConsResponse : ElectronicInvoiceTest.FECAEDetRequest
    {
        
        private string ResultadoField;
        
        private string CodAutorizacionField;
        
        private string EmisionTipoField;
        
        private string FchVtoField;
        
        private string FchProcesoField;
        
        private ElectronicInvoiceTest.Obs[] ObservacionesField;
        
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
        public ElectronicInvoiceTest.Obs[] Observaciones
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
        
        private ElectronicInvoiceTest.FECAECabResponse FeCabRespField;
        
        private ElectronicInvoiceTest.FECAEDetResponse[] FeDetRespField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECAECabResponse FeCabResp
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
        public ElectronicInvoiceTest.FECAEDetResponse[] FeDetResp
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
        public ElectronicInvoiceTest.Evt[] Events
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
        public ElectronicInvoiceTest.Err[] Errors
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
    public partial class FECAECabResponse : ElectronicInvoiceTest.FECabResponse
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FECAEDetResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    public partial class FECAEDetResponse : ElectronicInvoiceTest.FEDetResponse
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
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEACabResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAECabResponse))]
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
    public partial class FECAEACabResponse : ElectronicInvoiceTest.FECabResponse
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FEDetResponse", Namespace="http://ar.gov.afip.dif.FEV1/")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEADetResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEDetResponse))]
    public partial class FEDetResponse : object
    {
        
        private int ConceptoField;
        
        private int DocTipoField;
        
        private long DocNroField;
        
        private long CbteDesdeField;
        
        private long CbteHastaField;
        
        private string CbteFchField;
        
        private string ResultadoField;
        
        private ElectronicInvoiceTest.Obs[] ObservacionesField;
        
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
        public ElectronicInvoiceTest.Obs[] Observaciones
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
    public partial class FECAEADetResponse : ElectronicInvoiceTest.FEDetResponse
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
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private int PtoVtaField;
        
        private int CbteTipoField;
        
        private int CbteNroField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.Err[] Errors
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.FECompConsResponse ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECompConsResponse ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.FECAEACabRequest FeCabReqField;
        
        private ElectronicInvoiceTest.FECAEADetRequest[] FeDetReqField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECAEACabRequest FeCabReq
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
        public ElectronicInvoiceTest.FECAEADetRequest[] FeDetReq
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
        
        private ElectronicInvoiceTest.FECAEACabResponse FeCabRespField;
        
        private ElectronicInvoiceTest.FECAEADetResponse[] FeDetRespField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECAEACabResponse FeCabResp
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
        public ElectronicInvoiceTest.FECAEADetResponse[] FeDetResp
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
        public ElectronicInvoiceTest.Evt[] Events
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
        public ElectronicInvoiceTest.Err[] Errors
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
        
        private ElectronicInvoiceTest.FECAEAGet ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECAEAGet ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.Obs[] ObservacionesField;
        
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
        public ElectronicInvoiceTest.Obs[] Observaciones
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
        
        private ElectronicInvoiceTest.FECAEASinMov[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.FECAEASinMov[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ElectronicInvoiceTest.FECAEASinMovResponse))]
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
    public partial class FECAEASinMovResponse : ElectronicInvoiceTest.FECAEASinMov
    {
        
        private string ResultadoField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.Cotizacion ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.Cotizacion ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.TributoTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.TributoTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.Moneda[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.Moneda[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.IvaTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.IvaTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.OpcionalTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.OpcionalTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.ConceptoTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.ConceptoTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.PtoVenta[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.PtoVenta[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.CbteTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.CbteTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.DocTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.DocTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
        
        private ElectronicInvoiceTest.PaisTipo[] ResultGetField;
        
        private ElectronicInvoiceTest.Err[] ErrorsField;
        
        private ElectronicInvoiceTest.Evt[] EventsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ElectronicInvoiceTest.PaisTipo[] ResultGet
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
        public ElectronicInvoiceTest.Err[] Errors
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
        public ElectronicInvoiceTest.Evt[] Events
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ar.gov.afip.dif.FEV1/", ConfigurationName="ElectronicInvoiceTest.ServiceSoap")]
    public interface ServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAESolicitar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAESolicitarResponse> FECAESolicitarAsync(ElectronicInvoiceTest.FECAESolicitarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECompTotXRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompTotXRequestResponse> FECompTotXRequestAsync(ElectronicInvoiceTest.FECompTotXRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEDummy", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEDummyResponse> FEDummyAsync(ElectronicInvoiceTest.FEDummyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECompUltimoAutorizado", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompUltimoAutorizadoResponse> FECompUltimoAutorizadoAsync(ElectronicInvoiceTest.FECompUltimoAutorizadoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECompConsultar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompConsultarResponse> FECompConsultarAsync(ElectronicInvoiceTest.FECompConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEARegInformativo", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEARegInformativoResponse> FECAEARegInformativoAsync(ElectronicInvoiceTest.FECAEARegInformativoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEASolicitar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASolicitarResponse> FECAEASolicitarAsync(ElectronicInvoiceTest.FECAEASolicitarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEASinMovimientoConsultar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASinMovimientoConsultarResponse> FECAEASinMovimientoConsultarAsync(ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEASinMovimientoInformar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASinMovimientoInformarResponse> FECAEASinMovimientoInformarAsync(ElectronicInvoiceTest.FECAEASinMovimientoInformarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FECAEAConsultar", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEAConsultarResponse> FECAEAConsultarAsync(ElectronicInvoiceTest.FECAEAConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetCotizacion", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetCotizacionResponse> FEParamGetCotizacionAsync(ElectronicInvoiceTest.FEParamGetCotizacionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposTributos", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposTributosResponse> FEParamGetTiposTributosAsync(ElectronicInvoiceTest.FEParamGetTiposTributosRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposMonedas", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposMonedasResponse> FEParamGetTiposMonedasAsync(ElectronicInvoiceTest.FEParamGetTiposMonedasRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposIva", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposIvaResponse> FEParamGetTiposIvaAsync(ElectronicInvoiceTest.FEParamGetTiposIvaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposOpcional", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposOpcionalResponse> FEParamGetTiposOpcionalAsync(ElectronicInvoiceTest.FEParamGetTiposOpcionalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposConcepto", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposConceptoResponse> FEParamGetTiposConceptoAsync(ElectronicInvoiceTest.FEParamGetTiposConceptoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetPtosVenta", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetPtosVentaResponse> FEParamGetPtosVentaAsync(ElectronicInvoiceTest.FEParamGetPtosVentaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposCbte", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposCbteResponse> FEParamGetTiposCbteAsync(ElectronicInvoiceTest.FEParamGetTiposCbteRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposDoc", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposDocResponse> FEParamGetTiposDocAsync(ElectronicInvoiceTest.FEParamGetTiposDocRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ar.gov.afip.dif.FEV1/FEParamGetTiposPaises", ReplyAction="*")]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposPaisesResponse> FEParamGetTiposPaisesAsync(ElectronicInvoiceTest.FEParamGetTiposPaisesRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FECAESolicitarRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FECAESolicitar", Namespace="http://ar.gov.afip.dif.FEV1/", Order=0)]
        public ElectronicInvoiceTest.FECAESolicitarRequestBody Body;
        
        public FECAESolicitarRequest()
        {
        }
        
        public FECAESolicitarRequest(ElectronicInvoiceTest.FECAESolicitarRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceTest.FECAERequest FeCAEReq;
        
        public FECAESolicitarRequestBody()
        {
        }
        
        public FECAESolicitarRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, ElectronicInvoiceTest.FECAERequest FeCAEReq)
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
        public ElectronicInvoiceTest.FECAESolicitarResponseBody Body;
        
        public FECAESolicitarResponse()
        {
        }
        
        public FECAESolicitarResponse(ElectronicInvoiceTest.FECAESolicitarResponseBody Body)
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
        public ElectronicInvoiceTest.FECAEResponse FECAESolicitarResult;
        
        public FECAESolicitarResponseBody()
        {
        }
        
        public FECAESolicitarResponseBody(ElectronicInvoiceTest.FECAEResponse FECAESolicitarResult)
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
        public ElectronicInvoiceTest.FECompTotXRequestRequestBody Body;
        
        public FECompTotXRequestRequest()
        {
        }
        
        public FECompTotXRequestRequest(ElectronicInvoiceTest.FECompTotXRequestRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FECompTotXRequestRequestBody()
        {
        }
        
        public FECompTotXRequestRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FECompTotXRequestResponseBody Body;
        
        public FECompTotXRequestResponse()
        {
        }
        
        public FECompTotXRequestResponse(ElectronicInvoiceTest.FECompTotXRequestResponseBody Body)
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
        public ElectronicInvoiceTest.FERegXReqResponse FECompTotXRequestResult;
        
        public FECompTotXRequestResponseBody()
        {
        }
        
        public FECompTotXRequestResponseBody(ElectronicInvoiceTest.FERegXReqResponse FECompTotXRequestResult)
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
        public ElectronicInvoiceTest.FEDummyRequestBody Body;
        
        public FEDummyRequest()
        {
        }
        
        public FEDummyRequest(ElectronicInvoiceTest.FEDummyRequestBody Body)
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
        public ElectronicInvoiceTest.FEDummyResponseBody Body;
        
        public FEDummyResponse()
        {
        }
        
        public FEDummyResponse(ElectronicInvoiceTest.FEDummyResponseBody Body)
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
        public ElectronicInvoiceTest.DummyResponse FEDummyResult;
        
        public FEDummyResponseBody()
        {
        }
        
        public FEDummyResponseBody(ElectronicInvoiceTest.DummyResponse FEDummyResult)
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
        public ElectronicInvoiceTest.FECompUltimoAutorizadoRequestBody Body;
        
        public FECompUltimoAutorizadoRequest()
        {
        }
        
        public FECompUltimoAutorizadoRequest(ElectronicInvoiceTest.FECompUltimoAutorizadoRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int PtoVta;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int CbteTipo;
        
        public FECompUltimoAutorizadoRequestBody()
        {
        }
        
        public FECompUltimoAutorizadoRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, int PtoVta, int CbteTipo)
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
        public ElectronicInvoiceTest.FECompUltimoAutorizadoResponseBody Body;
        
        public FECompUltimoAutorizadoResponse()
        {
        }
        
        public FECompUltimoAutorizadoResponse(ElectronicInvoiceTest.FECompUltimoAutorizadoResponseBody Body)
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
        public ElectronicInvoiceTest.FERecuperaLastCbteResponse FECompUltimoAutorizadoResult;
        
        public FECompUltimoAutorizadoResponseBody()
        {
        }
        
        public FECompUltimoAutorizadoResponseBody(ElectronicInvoiceTest.FERecuperaLastCbteResponse FECompUltimoAutorizadoResult)
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
        public ElectronicInvoiceTest.FECompConsultarRequestBody Body;
        
        public FECompConsultarRequest()
        {
        }
        
        public FECompConsultarRequest(ElectronicInvoiceTest.FECompConsultarRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceTest.FECompConsultaReq FeCompConsReq;
        
        public FECompConsultarRequestBody()
        {
        }
        
        public FECompConsultarRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, ElectronicInvoiceTest.FECompConsultaReq FeCompConsReq)
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
        public ElectronicInvoiceTest.FECompConsultarResponseBody Body;
        
        public FECompConsultarResponse()
        {
        }
        
        public FECompConsultarResponse(ElectronicInvoiceTest.FECompConsultarResponseBody Body)
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
        public ElectronicInvoiceTest.FECompConsultaResponse FECompConsultarResult;
        
        public FECompConsultarResponseBody()
        {
        }
        
        public FECompConsultarResponseBody(ElectronicInvoiceTest.FECompConsultaResponse FECompConsultarResult)
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
        public ElectronicInvoiceTest.FECAEARegInformativoRequestBody Body;
        
        public FECAEARegInformativoRequest()
        {
        }
        
        public FECAEARegInformativoRequest(ElectronicInvoiceTest.FECAEARegInformativoRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ElectronicInvoiceTest.FECAEARequest FeCAEARegInfReq;
        
        public FECAEARegInformativoRequestBody()
        {
        }
        
        public FECAEARegInformativoRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, ElectronicInvoiceTest.FECAEARequest FeCAEARegInfReq)
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
        public ElectronicInvoiceTest.FECAEARegInformativoResponseBody Body;
        
        public FECAEARegInformativoResponse()
        {
        }
        
        public FECAEARegInformativoResponse(ElectronicInvoiceTest.FECAEARegInformativoResponseBody Body)
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
        public ElectronicInvoiceTest.FECAEAResponse FECAEARegInformativoResult;
        
        public FECAEARegInformativoResponseBody()
        {
        }
        
        public FECAEARegInformativoResponseBody(ElectronicInvoiceTest.FECAEAResponse FECAEARegInformativoResult)
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
        public ElectronicInvoiceTest.FECAEASolicitarRequestBody Body;
        
        public FECAEASolicitarRequest()
        {
        }
        
        public FECAEASolicitarRequest(ElectronicInvoiceTest.FECAEASolicitarRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int Periodo;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public short Orden;
        
        public FECAEASolicitarRequestBody()
        {
        }
        
        public FECAEASolicitarRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, int Periodo, short Orden)
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
        public ElectronicInvoiceTest.FECAEASolicitarResponseBody Body;
        
        public FECAEASolicitarResponse()
        {
        }
        
        public FECAEASolicitarResponse(ElectronicInvoiceTest.FECAEASolicitarResponseBody Body)
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
        public ElectronicInvoiceTest.FECAEAGetResponse FECAEASolicitarResult;
        
        public FECAEASolicitarResponseBody()
        {
        }
        
        public FECAEASolicitarResponseBody(ElectronicInvoiceTest.FECAEAGetResponse FECAEASolicitarResult)
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
        public ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequestBody Body;
        
        public FECAEASinMovimientoConsultarRequest()
        {
        }
        
        public FECAEASinMovimientoConsultarRequest(ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string CAEA;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int PtoVta;
        
        public FECAEASinMovimientoConsultarRequestBody()
        {
        }
        
        public FECAEASinMovimientoConsultarRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, string CAEA, int PtoVta)
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
        public ElectronicInvoiceTest.FECAEASinMovimientoConsultarResponseBody Body;
        
        public FECAEASinMovimientoConsultarResponse()
        {
        }
        
        public FECAEASinMovimientoConsultarResponse(ElectronicInvoiceTest.FECAEASinMovimientoConsultarResponseBody Body)
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
        public ElectronicInvoiceTest.FECAEASinMovConsResponse FECAEASinMovimientoConsultarResult;
        
        public FECAEASinMovimientoConsultarResponseBody()
        {
        }
        
        public FECAEASinMovimientoConsultarResponseBody(ElectronicInvoiceTest.FECAEASinMovConsResponse FECAEASinMovimientoConsultarResult)
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
        public ElectronicInvoiceTest.FECAEASinMovimientoInformarRequestBody Body;
        
        public FECAEASinMovimientoInformarRequest()
        {
        }
        
        public FECAEASinMovimientoInformarRequest(ElectronicInvoiceTest.FECAEASinMovimientoInformarRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int PtoVta;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string CAEA;
        
        public FECAEASinMovimientoInformarRequestBody()
        {
        }
        
        public FECAEASinMovimientoInformarRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, int PtoVta, string CAEA)
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
        public ElectronicInvoiceTest.FECAEASinMovimientoInformarResponseBody Body;
        
        public FECAEASinMovimientoInformarResponse()
        {
        }
        
        public FECAEASinMovimientoInformarResponse(ElectronicInvoiceTest.FECAEASinMovimientoInformarResponseBody Body)
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
        public ElectronicInvoiceTest.FECAEASinMovResponse FECAEASinMovimientoInformarResult;
        
        public FECAEASinMovimientoInformarResponseBody()
        {
        }
        
        public FECAEASinMovimientoInformarResponseBody(ElectronicInvoiceTest.FECAEASinMovResponse FECAEASinMovimientoInformarResult)
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
        public ElectronicInvoiceTest.FECAEAConsultarRequestBody Body;
        
        public FECAEAConsultarRequest()
        {
        }
        
        public FECAEAConsultarRequest(ElectronicInvoiceTest.FECAEAConsultarRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int Periodo;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public short Orden;
        
        public FECAEAConsultarRequestBody()
        {
        }
        
        public FECAEAConsultarRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, int Periodo, short Orden)
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
        public ElectronicInvoiceTest.FECAEAConsultarResponseBody Body;
        
        public FECAEAConsultarResponse()
        {
        }
        
        public FECAEAConsultarResponse(ElectronicInvoiceTest.FECAEAConsultarResponseBody Body)
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
        public ElectronicInvoiceTest.FECAEAGetResponse FECAEAConsultarResult;
        
        public FECAEAConsultarResponseBody()
        {
        }
        
        public FECAEAConsultarResponseBody(ElectronicInvoiceTest.FECAEAGetResponse FECAEAConsultarResult)
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
        public ElectronicInvoiceTest.FEParamGetCotizacionRequestBody Body;
        
        public FEParamGetCotizacionRequest()
        {
        }
        
        public FEParamGetCotizacionRequest(ElectronicInvoiceTest.FEParamGetCotizacionRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string MonId;
        
        public FEParamGetCotizacionRequestBody()
        {
        }
        
        public FEParamGetCotizacionRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth, string MonId)
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
        public ElectronicInvoiceTest.FEParamGetCotizacionResponseBody Body;
        
        public FEParamGetCotizacionResponse()
        {
        }
        
        public FEParamGetCotizacionResponse(ElectronicInvoiceTest.FEParamGetCotizacionResponseBody Body)
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
        public ElectronicInvoiceTest.FECotizacionResponse FEParamGetCotizacionResult;
        
        public FEParamGetCotizacionResponseBody()
        {
        }
        
        public FEParamGetCotizacionResponseBody(ElectronicInvoiceTest.FECotizacionResponse FEParamGetCotizacionResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposTributosRequestBody Body;
        
        public FEParamGetTiposTributosRequest()
        {
        }
        
        public FEParamGetTiposTributosRequest(ElectronicInvoiceTest.FEParamGetTiposTributosRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposTributosRequestBody()
        {
        }
        
        public FEParamGetTiposTributosRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposTributosResponseBody Body;
        
        public FEParamGetTiposTributosResponse()
        {
        }
        
        public FEParamGetTiposTributosResponse(ElectronicInvoiceTest.FEParamGetTiposTributosResponseBody Body)
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
        public ElectronicInvoiceTest.FETributoResponse FEParamGetTiposTributosResult;
        
        public FEParamGetTiposTributosResponseBody()
        {
        }
        
        public FEParamGetTiposTributosResponseBody(ElectronicInvoiceTest.FETributoResponse FEParamGetTiposTributosResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposMonedasRequestBody Body;
        
        public FEParamGetTiposMonedasRequest()
        {
        }
        
        public FEParamGetTiposMonedasRequest(ElectronicInvoiceTest.FEParamGetTiposMonedasRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposMonedasRequestBody()
        {
        }
        
        public FEParamGetTiposMonedasRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposMonedasResponseBody Body;
        
        public FEParamGetTiposMonedasResponse()
        {
        }
        
        public FEParamGetTiposMonedasResponse(ElectronicInvoiceTest.FEParamGetTiposMonedasResponseBody Body)
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
        public ElectronicInvoiceTest.MonedaResponse FEParamGetTiposMonedasResult;
        
        public FEParamGetTiposMonedasResponseBody()
        {
        }
        
        public FEParamGetTiposMonedasResponseBody(ElectronicInvoiceTest.MonedaResponse FEParamGetTiposMonedasResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposIvaRequestBody Body;
        
        public FEParamGetTiposIvaRequest()
        {
        }
        
        public FEParamGetTiposIvaRequest(ElectronicInvoiceTest.FEParamGetTiposIvaRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposIvaRequestBody()
        {
        }
        
        public FEParamGetTiposIvaRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposIvaResponseBody Body;
        
        public FEParamGetTiposIvaResponse()
        {
        }
        
        public FEParamGetTiposIvaResponse(ElectronicInvoiceTest.FEParamGetTiposIvaResponseBody Body)
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
        public ElectronicInvoiceTest.IvaTipoResponse FEParamGetTiposIvaResult;
        
        public FEParamGetTiposIvaResponseBody()
        {
        }
        
        public FEParamGetTiposIvaResponseBody(ElectronicInvoiceTest.IvaTipoResponse FEParamGetTiposIvaResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposOpcionalRequestBody Body;
        
        public FEParamGetTiposOpcionalRequest()
        {
        }
        
        public FEParamGetTiposOpcionalRequest(ElectronicInvoiceTest.FEParamGetTiposOpcionalRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposOpcionalRequestBody()
        {
        }
        
        public FEParamGetTiposOpcionalRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposOpcionalResponseBody Body;
        
        public FEParamGetTiposOpcionalResponse()
        {
        }
        
        public FEParamGetTiposOpcionalResponse(ElectronicInvoiceTest.FEParamGetTiposOpcionalResponseBody Body)
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
        public ElectronicInvoiceTest.OpcionalTipoResponse FEParamGetTiposOpcionalResult;
        
        public FEParamGetTiposOpcionalResponseBody()
        {
        }
        
        public FEParamGetTiposOpcionalResponseBody(ElectronicInvoiceTest.OpcionalTipoResponse FEParamGetTiposOpcionalResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposConceptoRequestBody Body;
        
        public FEParamGetTiposConceptoRequest()
        {
        }
        
        public FEParamGetTiposConceptoRequest(ElectronicInvoiceTest.FEParamGetTiposConceptoRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposConceptoRequestBody()
        {
        }
        
        public FEParamGetTiposConceptoRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposConceptoResponseBody Body;
        
        public FEParamGetTiposConceptoResponse()
        {
        }
        
        public FEParamGetTiposConceptoResponse(ElectronicInvoiceTest.FEParamGetTiposConceptoResponseBody Body)
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
        public ElectronicInvoiceTest.ConceptoTipoResponse FEParamGetTiposConceptoResult;
        
        public FEParamGetTiposConceptoResponseBody()
        {
        }
        
        public FEParamGetTiposConceptoResponseBody(ElectronicInvoiceTest.ConceptoTipoResponse FEParamGetTiposConceptoResult)
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
        public ElectronicInvoiceTest.FEParamGetPtosVentaRequestBody Body;
        
        public FEParamGetPtosVentaRequest()
        {
        }
        
        public FEParamGetPtosVentaRequest(ElectronicInvoiceTest.FEParamGetPtosVentaRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetPtosVentaRequestBody()
        {
        }
        
        public FEParamGetPtosVentaRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetPtosVentaResponseBody Body;
        
        public FEParamGetPtosVentaResponse()
        {
        }
        
        public FEParamGetPtosVentaResponse(ElectronicInvoiceTest.FEParamGetPtosVentaResponseBody Body)
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
        public ElectronicInvoiceTest.FEPtoVentaResponse FEParamGetPtosVentaResult;
        
        public FEParamGetPtosVentaResponseBody()
        {
        }
        
        public FEParamGetPtosVentaResponseBody(ElectronicInvoiceTest.FEPtoVentaResponse FEParamGetPtosVentaResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposCbteRequestBody Body;
        
        public FEParamGetTiposCbteRequest()
        {
        }
        
        public FEParamGetTiposCbteRequest(ElectronicInvoiceTest.FEParamGetTiposCbteRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposCbteRequestBody()
        {
        }
        
        public FEParamGetTiposCbteRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposCbteResponseBody Body;
        
        public FEParamGetTiposCbteResponse()
        {
        }
        
        public FEParamGetTiposCbteResponse(ElectronicInvoiceTest.FEParamGetTiposCbteResponseBody Body)
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
        public ElectronicInvoiceTest.CbteTipoResponse FEParamGetTiposCbteResult;
        
        public FEParamGetTiposCbteResponseBody()
        {
        }
        
        public FEParamGetTiposCbteResponseBody(ElectronicInvoiceTest.CbteTipoResponse FEParamGetTiposCbteResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposDocRequestBody Body;
        
        public FEParamGetTiposDocRequest()
        {
        }
        
        public FEParamGetTiposDocRequest(ElectronicInvoiceTest.FEParamGetTiposDocRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposDocRequestBody()
        {
        }
        
        public FEParamGetTiposDocRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposDocResponseBody Body;
        
        public FEParamGetTiposDocResponse()
        {
        }
        
        public FEParamGetTiposDocResponse(ElectronicInvoiceTest.FEParamGetTiposDocResponseBody Body)
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
        public ElectronicInvoiceTest.DocTipoResponse FEParamGetTiposDocResult;
        
        public FEParamGetTiposDocResponseBody()
        {
        }
        
        public FEParamGetTiposDocResponseBody(ElectronicInvoiceTest.DocTipoResponse FEParamGetTiposDocResult)
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
        public ElectronicInvoiceTest.FEParamGetTiposPaisesRequestBody Body;
        
        public FEParamGetTiposPaisesRequest()
        {
        }
        
        public FEParamGetTiposPaisesRequest(ElectronicInvoiceTest.FEParamGetTiposPaisesRequestBody Body)
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
        public ElectronicInvoiceTest.FEAuthRequest Auth;
        
        public FEParamGetTiposPaisesRequestBody()
        {
        }
        
        public FEParamGetTiposPaisesRequestBody(ElectronicInvoiceTest.FEAuthRequest Auth)
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
        public ElectronicInvoiceTest.FEParamGetTiposPaisesResponseBody Body;
        
        public FEParamGetTiposPaisesResponse()
        {
        }
        
        public FEParamGetTiposPaisesResponse(ElectronicInvoiceTest.FEParamGetTiposPaisesResponseBody Body)
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
        public ElectronicInvoiceTest.FEPaisResponse FEParamGetTiposPaisesResult;
        
        public FEParamGetTiposPaisesResponseBody()
        {
        }
        
        public FEParamGetTiposPaisesResponseBody(ElectronicInvoiceTest.FEPaisResponse FEParamGetTiposPaisesResult)
        {
            this.FEParamGetTiposPaisesResult = FEParamGetTiposPaisesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface ServiceSoapChannel : ElectronicInvoiceTest.ServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<ElectronicInvoiceTest.ServiceSoap>, ElectronicInvoiceTest.ServiceSoap
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
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAESolicitarResponse> ElectronicInvoiceTest.ServiceSoap.FECAESolicitarAsync(ElectronicInvoiceTest.FECAESolicitarRequest request)
        {
            return base.Channel.FECAESolicitarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAESolicitarResponse> FECAESolicitarAsync(ElectronicInvoiceTest.FEAuthRequest Auth, ElectronicInvoiceTest.FECAERequest FeCAEReq)
        {
            ElectronicInvoiceTest.FECAESolicitarRequest inValue = new ElectronicInvoiceTest.FECAESolicitarRequest();
            inValue.Body = new ElectronicInvoiceTest.FECAESolicitarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.FeCAEReq = FeCAEReq;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECAESolicitarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompTotXRequestResponse> ElectronicInvoiceTest.ServiceSoap.FECompTotXRequestAsync(ElectronicInvoiceTest.FECompTotXRequestRequest request)
        {
            return base.Channel.FECompTotXRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompTotXRequestResponse> FECompTotXRequestAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FECompTotXRequestRequest inValue = new ElectronicInvoiceTest.FECompTotXRequestRequest();
            inValue.Body = new ElectronicInvoiceTest.FECompTotXRequestRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECompTotXRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEDummyResponse> ElectronicInvoiceTest.ServiceSoap.FEDummyAsync(ElectronicInvoiceTest.FEDummyRequest request)
        {
            return base.Channel.FEDummyAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEDummyResponse> FEDummyAsync()
        {
            ElectronicInvoiceTest.FEDummyRequest inValue = new ElectronicInvoiceTest.FEDummyRequest();
            inValue.Body = new ElectronicInvoiceTest.FEDummyRequestBody();
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEDummyAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompUltimoAutorizadoResponse> ElectronicInvoiceTest.ServiceSoap.FECompUltimoAutorizadoAsync(ElectronicInvoiceTest.FECompUltimoAutorizadoRequest request)
        {
            return base.Channel.FECompUltimoAutorizadoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompUltimoAutorizadoResponse> FECompUltimoAutorizadoAsync(ElectronicInvoiceTest.FEAuthRequest Auth, int PtoVta, int CbteTipo)
        {
            ElectronicInvoiceTest.FECompUltimoAutorizadoRequest inValue = new ElectronicInvoiceTest.FECompUltimoAutorizadoRequest();
            inValue.Body = new ElectronicInvoiceTest.FECompUltimoAutorizadoRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.PtoVta = PtoVta;
            inValue.Body.CbteTipo = CbteTipo;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECompUltimoAutorizadoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompConsultarResponse> ElectronicInvoiceTest.ServiceSoap.FECompConsultarAsync(ElectronicInvoiceTest.FECompConsultarRequest request)
        {
            return base.Channel.FECompConsultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECompConsultarResponse> FECompConsultarAsync(ElectronicInvoiceTest.FEAuthRequest Auth, ElectronicInvoiceTest.FECompConsultaReq FeCompConsReq)
        {
            ElectronicInvoiceTest.FECompConsultarRequest inValue = new ElectronicInvoiceTest.FECompConsultarRequest();
            inValue.Body = new ElectronicInvoiceTest.FECompConsultarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.FeCompConsReq = FeCompConsReq;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECompConsultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEARegInformativoResponse> ElectronicInvoiceTest.ServiceSoap.FECAEARegInformativoAsync(ElectronicInvoiceTest.FECAEARegInformativoRequest request)
        {
            return base.Channel.FECAEARegInformativoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEARegInformativoResponse> FECAEARegInformativoAsync(ElectronicInvoiceTest.FEAuthRequest Auth, ElectronicInvoiceTest.FECAEARequest FeCAEARegInfReq)
        {
            ElectronicInvoiceTest.FECAEARegInformativoRequest inValue = new ElectronicInvoiceTest.FECAEARegInformativoRequest();
            inValue.Body = new ElectronicInvoiceTest.FECAEARegInformativoRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.FeCAEARegInfReq = FeCAEARegInfReq;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECAEARegInformativoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASolicitarResponse> ElectronicInvoiceTest.ServiceSoap.FECAEASolicitarAsync(ElectronicInvoiceTest.FECAEASolicitarRequest request)
        {
            return base.Channel.FECAEASolicitarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASolicitarResponse> FECAEASolicitarAsync(ElectronicInvoiceTest.FEAuthRequest Auth, int Periodo, short Orden)
        {
            ElectronicInvoiceTest.FECAEASolicitarRequest inValue = new ElectronicInvoiceTest.FECAEASolicitarRequest();
            inValue.Body = new ElectronicInvoiceTest.FECAEASolicitarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.Periodo = Periodo;
            inValue.Body.Orden = Orden;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECAEASolicitarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASinMovimientoConsultarResponse> ElectronicInvoiceTest.ServiceSoap.FECAEASinMovimientoConsultarAsync(ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequest request)
        {
            return base.Channel.FECAEASinMovimientoConsultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASinMovimientoConsultarResponse> FECAEASinMovimientoConsultarAsync(ElectronicInvoiceTest.FEAuthRequest Auth, string CAEA, int PtoVta)
        {
            ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequest inValue = new ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequest();
            inValue.Body = new ElectronicInvoiceTest.FECAEASinMovimientoConsultarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.CAEA = CAEA;
            inValue.Body.PtoVta = PtoVta;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECAEASinMovimientoConsultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASinMovimientoInformarResponse> ElectronicInvoiceTest.ServiceSoap.FECAEASinMovimientoInformarAsync(ElectronicInvoiceTest.FECAEASinMovimientoInformarRequest request)
        {
            return base.Channel.FECAEASinMovimientoInformarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEASinMovimientoInformarResponse> FECAEASinMovimientoInformarAsync(ElectronicInvoiceTest.FEAuthRequest Auth, int PtoVta, string CAEA)
        {
            ElectronicInvoiceTest.FECAEASinMovimientoInformarRequest inValue = new ElectronicInvoiceTest.FECAEASinMovimientoInformarRequest();
            inValue.Body = new ElectronicInvoiceTest.FECAEASinMovimientoInformarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.PtoVta = PtoVta;
            inValue.Body.CAEA = CAEA;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECAEASinMovimientoInformarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEAConsultarResponse> ElectronicInvoiceTest.ServiceSoap.FECAEAConsultarAsync(ElectronicInvoiceTest.FECAEAConsultarRequest request)
        {
            return base.Channel.FECAEAConsultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FECAEAConsultarResponse> FECAEAConsultarAsync(ElectronicInvoiceTest.FEAuthRequest Auth, int Periodo, short Orden)
        {
            ElectronicInvoiceTest.FECAEAConsultarRequest inValue = new ElectronicInvoiceTest.FECAEAConsultarRequest();
            inValue.Body = new ElectronicInvoiceTest.FECAEAConsultarRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.Periodo = Periodo;
            inValue.Body.Orden = Orden;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FECAEAConsultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetCotizacionResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetCotizacionAsync(ElectronicInvoiceTest.FEParamGetCotizacionRequest request)
        {
            return base.Channel.FEParamGetCotizacionAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetCotizacionResponse> FEParamGetCotizacionAsync(ElectronicInvoiceTest.FEAuthRequest Auth, string MonId)
        {
            ElectronicInvoiceTest.FEParamGetCotizacionRequest inValue = new ElectronicInvoiceTest.FEParamGetCotizacionRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetCotizacionRequestBody();
            inValue.Body.Auth = Auth;
            inValue.Body.MonId = MonId;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetCotizacionAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposTributosResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposTributosAsync(ElectronicInvoiceTest.FEParamGetTiposTributosRequest request)
        {
            return base.Channel.FEParamGetTiposTributosAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposTributosResponse> FEParamGetTiposTributosAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposTributosRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposTributosRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposTributosRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposTributosAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposMonedasResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposMonedasAsync(ElectronicInvoiceTest.FEParamGetTiposMonedasRequest request)
        {
            return base.Channel.FEParamGetTiposMonedasAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposMonedasResponse> FEParamGetTiposMonedasAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposMonedasRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposMonedasRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposMonedasRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposMonedasAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposIvaResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposIvaAsync(ElectronicInvoiceTest.FEParamGetTiposIvaRequest request)
        {
            return base.Channel.FEParamGetTiposIvaAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposIvaResponse> FEParamGetTiposIvaAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposIvaRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposIvaRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposIvaRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposIvaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposOpcionalResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposOpcionalAsync(ElectronicInvoiceTest.FEParamGetTiposOpcionalRequest request)
        {
            return base.Channel.FEParamGetTiposOpcionalAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposOpcionalResponse> FEParamGetTiposOpcionalAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposOpcionalRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposOpcionalRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposOpcionalRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposOpcionalAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposConceptoResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposConceptoAsync(ElectronicInvoiceTest.FEParamGetTiposConceptoRequest request)
        {
            return base.Channel.FEParamGetTiposConceptoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposConceptoResponse> FEParamGetTiposConceptoAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposConceptoRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposConceptoRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposConceptoRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposConceptoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetPtosVentaResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetPtosVentaAsync(ElectronicInvoiceTest.FEParamGetPtosVentaRequest request)
        {
            return base.Channel.FEParamGetPtosVentaAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetPtosVentaResponse> FEParamGetPtosVentaAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetPtosVentaRequest inValue = new ElectronicInvoiceTest.FEParamGetPtosVentaRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetPtosVentaRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetPtosVentaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposCbteResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposCbteAsync(ElectronicInvoiceTest.FEParamGetTiposCbteRequest request)
        {
            return base.Channel.FEParamGetTiposCbteAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposCbteResponse> FEParamGetTiposCbteAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposCbteRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposCbteRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposCbteRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposCbteAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposDocResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposDocAsync(ElectronicInvoiceTest.FEParamGetTiposDocRequest request)
        {
            return base.Channel.FEParamGetTiposDocAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposDocResponse> FEParamGetTiposDocAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposDocRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposDocRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposDocRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposDocAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposPaisesResponse> ElectronicInvoiceTest.ServiceSoap.FEParamGetTiposPaisesAsync(ElectronicInvoiceTest.FEParamGetTiposPaisesRequest request)
        {
            return base.Channel.FEParamGetTiposPaisesAsync(request);
        }
        
        public System.Threading.Tasks.Task<ElectronicInvoiceTest.FEParamGetTiposPaisesResponse> FEParamGetTiposPaisesAsync(ElectronicInvoiceTest.FEAuthRequest Auth)
        {
            ElectronicInvoiceTest.FEParamGetTiposPaisesRequest inValue = new ElectronicInvoiceTest.FEParamGetTiposPaisesRequest();
            inValue.Body = new ElectronicInvoiceTest.FEParamGetTiposPaisesRequestBody();
            inValue.Body.Auth = Auth;
            return ((ElectronicInvoiceTest.ServiceSoap)(this)).FEParamGetTiposPaisesAsync(inValue);
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
                return new System.ServiceModel.EndpointAddress("https://wswhomo.afip.gov.ar/wsfev1/service.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://wswhomo.afip.gov.ar/wsfev1/service.asmx");
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
