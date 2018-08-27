using RepoWebShop.Interfaces;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509.Store;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Pkcs;
using Microsoft.Extensions.Caching.Distributed;
using RepoWebShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Extensions;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Crypto;

namespace RepoWebShop.Repositories
{
    public class ElectronicBillingRepository : IElectronicBillingRepository
    {
        private readonly AppDbContext _dbCtx;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ICalendarRepository _calendar;
        private readonly IDistributedCache _serverCache;
        private readonly IMapper _mapper;
        private readonly bool _isProd;
        private const string AfipLoginTicketWsFe = "AfipLoginTicketWsFe";
        private const string AfipSignTicketWsFe = "AfipSignTicketWsFe";
        private const string AfipLoginTicketWsPersona = "AfipLoginTicketWsPersona";
        private const string AfipSignTicketWsPersona = "AfipSignTicketWsPersona";
        private const string AfipWsPersona = "ws_sr_constancia_inscripcion";
        private const string AfipWsFe = "wsfe";

        private XmlDocument LoginTicketRequestForElectronicBilling(string ws, bool isProd)
        {
            string XmlStrLoginTicketRequestTemplate = 
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                "<loginTicketRequest version=\"1.0\">" +
                    "<header>" +
                        "<destination></destination>" +
                        "<uniqueId></uniqueId>" +
                        "<generationTime></generationTime>" +
                        "<expirationTime></expirationTime>" +
                    "</header>" +
                    "<service></service>" +
                "</loginTicketRequest>".Trim();
            XmlDocument XmlLoginTicketRequest = new XmlDocument();
            XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);
            XmlLoginTicketRequest.SelectSingleNode("//destination").InnerText = $"CN=wsaa{(!isProd ? "homo" : string.Empty)}, O=AFIP, C=AR, SERIALNUMBER=CUIT 33693450239";
            var id = string.Concat(_calendar.LocalTime().ToUniversalTime().Ticks.ToString().ToCharArray().TakeLast(7));
            XmlLoginTicketRequest.SelectSingleNode("//uniqueId").InnerText = id;
            XmlLoginTicketRequest.SelectSingleNode("//generationTime").InnerText = _calendar.LocalTime().AddMinutes(-10).ToString("s");
            XmlLoginTicketRequest.SelectSingleNode("//expirationTime").InnerText = _calendar.LocalTime().AddMinutes(+10).ToString("s");
            XmlLoginTicketRequest.SelectSingleNode("//service").InnerText = ws; // "wsfe";
            return XmlLoginTicketRequest;
        }

        public ElectronicBillingRepository(AppDbContext dbCtx, IMapper mapper, IDistributedCache serverCache, IHostingEnvironment env, IConfiguration config, ICalendarRepository calendar)
        {
            _dbCtx = dbCtx;
            _mapper = mapper;
            _calendar = calendar;
            _env = env;
            _config = config;
            _serverCache = serverCache;
            _isProd = _env.IsProduction();
        }

        private async Task<FECAEResponse> FECAESolicitarAsync(Order order)
        {
            var payerData = new PayerDataRevenue(order, _config.GetValue<int>("LimiteFacturaB"));

            var requestInfo = new FECAERequestInfo(payerData, _calendar.LocalTime(), _config.GetValue<int>("Iva"), _config.GetValue<long>("CUIT"), await GetSignTicket(AfipWsFe, _isProd), await GetTokenTicket(AfipWsFe, _isProd), _config.GetValue<int>("PtoVtaAfip"));

            FECAEResponse result = await FECAESolicitarAsync(requestInfo);

            return result;
        }

        public async Task<InvoiceData> Facturar(Order order)
        {
            var invoiceData = new InvoiceData();
            try
            {
                FECAEResponse factura = await FECAESolicitarAsync(order);
                invoiceData = _mapper.Map<FECAEResponse.FECAECabResponse, InvoiceData>(factura.FeCabResp);

                if (factura.Errs != null)
                    invoiceData.AddInvoiceDetailRange(factura.Errs.Select(x => new InvoiceDetail("Error" , invoiceData, x)));
                if (factura.Events != null)
                    invoiceData.AddInvoiceDetailRange(factura.Errs.Select(x => new InvoiceDetail("Evento", invoiceData, x)));

                if (factura.FeDetResp != null)
                {
                    var obs = new List<FECAEResponse.CodeMessage>();
                    foreach (var ob in factura.FeDetResp.Where(o => o.Observaciones != null))
                        obs.AddRange(ob.Observaciones);
                    invoiceData.AddInvoiceDetailRange(obs.Select(x => new InvoiceDetail("Observacion", invoiceData, x)));
                    invoiceData.AddCaeRange(factura.FeDetResp.Select(x => {var cae = _mapper.Map<FECAEResponse.FECAEDetResponse, Cae>(x);
                        cae.InvoiceData = invoiceData;
                        return cae;}));
                }
            }
            catch (Exception ex)
            {
                var errs = new List<FECAEResponse.CodeMessage>
                {
                    new FECAEResponse.CodeMessage { Code = ex.HResult, Msg = ex.Message }
                };
                invoiceData.AddInvoiceDetailRange(errs.Select(x => new InvoiceDetail("Excepción", invoiceData, x)));
            }
            finally
            {
                invoiceData.Order = order;
                invoiceData.OrderId = order.OrderId;
                invoiceData.Created = _calendar.LocalTime();
            }
            await _dbCtx.InvoiceData.AddAsync(invoiceData);
            await _dbCtx.SaveChangesAsync();
            return invoiceData;
        }

        private async Task<FECAEResponse> ParseFeCaeResponseAsync(ElectronicInvoiceTest.FECAESolicitarResponse input, 
            ElectronicInvoiceTest.FEAuthRequest _auth,
            ElectronicInvoiceTest.ServiceSoapClient soapClient,  
            FECAERequestInfo requestInfo)
        {
            var errs = input.Body.FECAESolicitarResult.Errors?.Select(x => _mapper.Map<ElectronicInvoiceTest.Err, FECAEResponse.CodeMessage>(x));
            var events = input.Body.FECAESolicitarResult.Events?.Select(x => _mapper.Map<ElectronicInvoiceTest.Evt, FECAEResponse.CodeMessage>(x));
            var feCaeCabResponse = _mapper.Map<ElectronicInvoiceTest.FECAECabResponse, FECAEResponse.FECAECabResponse>(input.Body.FECAESolicitarResult.FeCabResp);
            var feDetResp = await Task.WhenAll(input.Body.FECAESolicitarResult.FeDetResp?.Select(x => GetComprobanteTest(input, _auth, soapClient, requestInfo, x, feCaeCabResponse)));
            return new FECAEResponse(errs, events, feCaeCabResponse, feDetResp);
        }

        private async Task<FECAEResponse> ParseFeCaeResponseAsync(ElectronicInvoiceProd.FECAESolicitarResponse input,
            ElectronicInvoiceProd.FEAuthRequest _auth,
            ElectronicInvoiceProd.ServiceSoapClient soapClient,
            FECAERequestInfo requestInfo)
        {
            var errs = input.Body.FECAESolicitarResult.Errors?.Select(x => _mapper.Map<ElectronicInvoiceProd.Err, FECAEResponse.CodeMessage>(x));
            var events = input.Body.FECAESolicitarResult.Events?.Select(x => _mapper.Map<ElectronicInvoiceProd.Evt, FECAEResponse.CodeMessage>(x));
            var feCaeCabResponse = _mapper.Map<ElectronicInvoiceProd.FECAECabResponse, FECAEResponse.FECAECabResponse>(input.Body.FECAESolicitarResult.FeCabResp);
            var feDetResp = await Task.WhenAll(input.Body.FECAESolicitarResult.FeDetResp?.Select(x => GetComprobanteProd(input, _auth, soapClient, requestInfo, x, feCaeCabResponse)));

            return new FECAEResponse(errs, events, feCaeCabResponse, feDetResp);
        }

        private async Task<FECAEResponse.FECAEDetResponse> GetComprobanteTest(ElectronicInvoiceTest.FECAESolicitarResponse input, ElectronicInvoiceTest.FEAuthRequest _auth, ElectronicInvoiceTest.ServiceSoapClient soapClient, FECAERequestInfo requestInfo, ElectronicInvoiceTest.FECAEDetResponse x, FECAEResponse.FECAECabResponse feCaeCabResponse)
        {
            var result = _mapper.Map<ElectronicInvoiceTest.FECAEDetResponse, FECAEResponse.FECAEDetResponse>(x);
            if (feCaeCabResponse.Resultado == "A")
            {
                if (input.Body.FECAESolicitarResult.FeDetResp.Count() == 1)
                    result.ImpTotal = Convert.ToDouble(requestInfo.Invoices.Sum());
                else
                {
                    var consultar = new ElectronicInvoiceTest.FECompConsultaReq
                    {
                        CbteNro = x.CbteDesde,
                        CbteTipo = feCaeCabResponse.CbteTipo,
                        PtoVta = feCaeCabResponse.PtoVta
                    };
                    try
                    {
                        var comprobate = await soapClient.FECompConsultarAsync(_auth, consultar);
                        result.ImpTotal = comprobate.Body.FECompConsultarResult.ResultGet.ImpTotal;
                    }
                    catch { }
                }
            }
            return result;
        }

        private async Task<FECAEResponse.FECAEDetResponse> GetComprobanteProd(ElectronicInvoiceProd.FECAESolicitarResponse input, ElectronicInvoiceProd.FEAuthRequest _auth, ElectronicInvoiceProd.ServiceSoapClient soapClient, FECAERequestInfo requestInfo, ElectronicInvoiceProd.FECAEDetResponse x, FECAEResponse.FECAECabResponse feCaeCabResponse)
        {
            var result = _mapper.Map<ElectronicInvoiceProd.FECAEDetResponse, FECAEResponse.FECAEDetResponse>(x);
            if (feCaeCabResponse.Resultado == "A")
            {
                if (input.Body.FECAESolicitarResult.FeDetResp.Count() == 1)
                    result.ImpTotal = Convert.ToDouble(requestInfo.Invoices.Sum());
                else
                {
                    var consultar = new ElectronicInvoiceProd.FECompConsultaReq
                    {
                        CbteNro = x.CbteDesde,
                        CbteTipo = feCaeCabResponse.CbteTipo,
                        PtoVta = feCaeCabResponse.PtoVta
                    };
                    try
                    {
                        var comprobate = await soapClient.FECompConsultarAsync(_auth, consultar);
                        result.ImpTotal = comprobate.Body.FECompConsultarResult.ResultGet.ImpTotal;
                    }
                    catch { }
                }
            }
            return result;
        }

        private async Task<string> GetTokenTicket(string ws, bool isProd)
        {
            var tokenName = "";
            switch (ws)
            {
                case AfipWsFe:
                    tokenName = AfipLoginTicketWsFe;
                    break;
                case AfipWsPersona:
                    tokenName = AfipLoginTicketWsPersona;
                    break;
            }

            var loginTicket = await _serverCache.GetStringAsync(tokenName);
            if (!string.IsNullOrEmpty(loginTicket))
                return loginTicket;

            await FetchAfipAuthorizationTokens(ws, isProd);

            return await _serverCache.GetStringAsync(tokenName);
        }

        private async Task<string> GetSignTicket(string ws, bool isProd)
        {
            var signName = "";
            switch (ws)
            {
                case AfipWsFe:
                    signName = AfipSignTicketWsFe;
                    break;
                case AfipWsPersona:
                    signName = AfipSignTicketWsPersona;
                    break;
            }

            var loginTicket = await _serverCache.GetStringAsync(signName);
            if (!string.IsNullOrEmpty(loginTicket))
                return loginTicket;

            await FetchAfipAuthorizationTokens(ws, isProd);
            return await _serverCache.GetStringAsync(signName);
        }

        private async Task FetchAfipAuthorizationTokens(string ws, bool isProd)
        {
            var loginCms = await GetLoginCmsAsync(ws, isProd);

            var token = loginCms.SelectSingleNode("//token").InnerText;
            var sign = loginCms.SelectSingleNode("//sign").InnerText;
            var expirationTime = loginCms.SelectSingleNode("//expirationTime").InnerText;
            DateTime Expiration = _calendar.ToLocalTime(DateTime.ParseExact(expirationTime, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK", null));

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = Expiration - _calendar.LocalTime()
            };

            switch(ws)
            {
                case AfipWsFe:
                    await _serverCache.SetStringAsync(AfipSignTicketWsFe, sign, options);
                    await _serverCache.SetStringAsync(AfipLoginTicketWsFe, token, options);
                    break;
                case AfipWsPersona:
                    await _serverCache.SetStringAsync(AfipSignTicketWsPersona, sign, options);
                    await _serverCache.SetStringAsync(AfipLoginTicketWsPersona, token, options);
                    break;
            }
        }

        private string SignMessage(string msg, bool isProd)
        {
            var gen = new CmsSignedDataGenerator();
            X509Certificate2 certificate = GetCertificate(isProd);
            var privKey = DotNetUtilities.GetRsaKeyPair(certificate.GetRSAPrivateKey()).Private;
            var cert = DotNetUtilities.FromX509Certificate(certificate);
            
            gen.AddSigner(privKey, cert, CmsSignedDataGenerator.DigestSha1);
            var certX509 = DotNetUtilities.ToX509Certificate(cert.CertificateStructure);
            var certList = new List<Org.BouncyCastle.X509.X509Certificate>();
            certList.Add(cert);
            X509CollectionStoreParameters PP = new X509CollectionStoreParameters(certList);
            IX509Store st1 = X509StoreFactory.Create("CERTIFICATE/COLLECTION", PP);
            gen.AddCertificates(st1);
            Encoding EncodedMsg = Encoding.UTF8;
            byte[] dataToSign = EncodedMsg.GetBytes(msg);
            CmsProcessable data = new CmsProcessableByteArray(dataToSign);
            CmsSignedData signed = gen.Generate(PkcsObjectIdentifiers.Pkcs7, data, true);
            var result = signed.GetEncoded();
            return Convert.ToBase64String(result);
        }

        private async Task<XmlDocument> GetLoginCmsAsync(string ws, bool isProd)
        {
            XmlDocument xml = new XmlDocument();
            var msg = SignMessage(LoginTicketRequestForElectronicBilling(ws, isProd).OuterXml, isProd);
            if (isProd)
            {
                var LoginCMS = new LoginCMSProd.LoginCMSClient();
                var response = await LoginCMS.loginCmsAsync(msg);
                xml.LoadXml(response.loginCmsReturn);
            }
            else
            {
                var LoginCMS = new LoginCMSTest.LoginCMSClient();
                var response = await LoginCMS.loginCmsAsync(msg);
                xml.LoadXml(response.loginCmsReturn);
            }
            return xml;
        }

        private X509Certificate2 GetCertificate(bool isProd)
        {
            var certPath = $"{_env.WebRootPath}\\Certs\\{(isProd ? "RepoProd.p12" : "RepoTest.pfx")}";
            var key = _config.GetValue<string>("AfipKeyCert");
            Byte[] file = File.ReadAllBytes(certPath);
            X509Certificate2 cert = new X509Certificate2(file, key,
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet |
                X509KeyStorageFlags.Exportable);

            return cert;
        }

        private async Task<FECAEResponse> FECAESolicitarAsync(FECAERequestInfo requestInfo)
        {
            if (_isProd)
            {
                var config = new ElectronicInvoiceProd.ServiceSoapClient.EndpointConfiguration();
                var soapClient = new ElectronicInvoiceProd.ServiceSoapClient(config);
                var _auth = _mapper.Map<FEAuthRequest, ElectronicInvoiceProd.FEAuthRequest>(requestInfo.AuthRequest);

                var fECompUltimo = await soapClient.FECompUltimoAutorizadoAsync(
                    _mapper.Map<FEAuthRequest, ElectronicInvoiceProd.FEAuthRequest>(requestInfo.AuthRequest),
                    requestInfo.FeCabReq.PtoVta, requestInfo.FeCabReq.CbteTipo);

                requestInfo.CbteDesde = fECompUltimo.Body.FECompUltimoAutorizadoResult.CbteNro + 1;
                requestInfo.CbteHasta = fECompUltimo.Body.FECompUltimoAutorizadoResult.CbteNro + requestInfo.FeCabReq.CantReg;

                var feCaeRequest = new ElectronicInvoiceProd.FECAERequest();
                feCaeRequest.FeCabReq = _mapper.Map<FECAECabRequest, ElectronicInvoiceProd.FECAECabRequest>(requestInfo.FeCabReq);

                feCaeRequest.FeDetReq = requestInfo.ToFECAEDetRequestList().Select(x => _mapper.Map<FECAEDetRequest, ElectronicInvoiceProd.FECAEDetRequest>(x)).ToArray();

                var response = await soapClient.FECAESolicitarAsync(_auth, feCaeRequest);

                return await ParseFeCaeResponseAsync(response, _auth, soapClient, requestInfo);
            }
            else
            {
                var config = new ElectronicInvoiceTest.ServiceSoapClient.EndpointConfiguration();
                var soapClient = new ElectronicInvoiceTest.ServiceSoapClient(config);
                var _auth = _mapper.Map<FEAuthRequest, ElectronicInvoiceTest.FEAuthRequest>(requestInfo.AuthRequest);

                var fECompUltimo = await soapClient.FECompUltimoAutorizadoAsync(
                    _mapper.Map<FEAuthRequest, ElectronicInvoiceTest.FEAuthRequest>(requestInfo.AuthRequest),
                    requestInfo.FeCabReq.PtoVta, requestInfo.FeCabReq.CbteTipo);

                requestInfo.CbteDesde = fECompUltimo.Body.FECompUltimoAutorizadoResult.CbteNro + 1;
                requestInfo.CbteHasta = requestInfo.CbteDesde;

                var feCaeRequest = new ElectronicInvoiceTest.FECAERequest();
                feCaeRequest.FeCabReq = _mapper.Map<FECAECabRequest, ElectronicInvoiceTest.FECAECabRequest>(requestInfo.FeCabReq);
                feCaeRequest.FeDetReq = requestInfo.ToFECAEDetRequestList().Select(x => _mapper.Map<FECAEDetRequest, ElectronicInvoiceTest.FECAEDetRequest>(x)).ToArray();

                var response = await soapClient.FECAESolicitarAsync(_auth, feCaeRequest);

                return await ParseFeCaeResponseAsync(response, _auth, soapClient, requestInfo);

            }
        }

        public async Task<Cuit> ValidPersonaAsync(long id)
        {
            Cuit cuit = new Cuit
            {
                Created = _calendar.LocalTime(),
                Number = id,
                Valid = false,
                CuitDetails = new Collection<CuitDetail>()
            };
            //Puedo omitir el ambiente de homologacion y hacerlo siempre en prod.
            var endpoint = new PadronProd.PersonaServiceA5Client.EndpointConfiguration();
            var client = new PadronProd.PersonaServiceA5Client(endpoint);
            try
            {
                var getPersonaResponse = await client.getPersonaAsync(await GetTokenTicket(AfipWsPersona, true), await GetSignTicket(AfipWsPersona, true), _config.GetValue<long>("CUIT"), id);
                cuit.Valid = true;
                var parse1 = getPersonaResponse.personaReturn.datosGenerales;
                var parse2 = getPersonaResponse.personaReturn.datosGenerales.domicilioFiscal;
                var personaTypeProperties = parse1.GetType().GetProperties();
                var domicilioFiscalProperties = parse2.GetType().GetProperties();

                var props1 = personaTypeProperties.Where(x => x.CanRead && x.CanWrite && x.PropertyType == typeof(string) && !x.Name.EndsWith("Specified")).Select(x => new KeyValuePair<string, string>(x.Name.CamelCaseString(), x.GetValue(parse1)?.ToString().ToLower().ToTitleCase()));
                var props2 = domicilioFiscalProperties.Where(x => x.CanRead && x.CanWrite && x.PropertyType == typeof(string) && !x.Name.EndsWith("Specified")).Select(x => new KeyValuePair<string, string>(x.Name.CamelCaseString(), x.GetValue(parse2)?.ToString().ToLower().ToTitleCase()));

                var allProps = new List<KeyValuePair<string, string>>();
                allProps.AddRange(props1.Where(x => !string.IsNullOrEmpty(x.Value)));
                allProps.AddRange(props2.Where(x => !string.IsNullOrEmpty(x.Value)));
                foreach (var property in allProps)
                    cuit.CuitDetails.Add(new CuitDetail { Property = property.Key, Value = property.Value, Cuit = cuit } );
            }
            catch
            { }
            _dbCtx.Cuits.Add(cuit);
            _dbCtx.SaveChanges();

            return cuit;
        }
        public async Task<IEnumerable<InvoiceData>> GetAll(Func<InvoiceData, bool> condition = null)
        {
			return await _dbCtx.InvoiceData.Where(x => condition == null || condition(x))
					.Include(x => x.Caes)
					.Include(x => x.InvoiceDetails)
					.Include(x => x.Order)
					.ToArrayAsync();
        }

        public async Task<InvoiceData> GetById(int id)
        {
            return (await GetAll(x => x.InvoiceDataId == id))?.FirstOrDefault();
        }

        public IEnumerable<Cuit> CuitInfo(InvoiceData invoice)
        {
            var result = new List<Cuit>();
            var cuits = invoice.Caes.Where(x => x.DocTipo == 80).Select(x => x.DocNro);
            foreach (var cuit in cuits)
            {
                var cuitfound = _dbCtx.Cuits.Where(x => x.Number == cuit).Include(x => x.CuitDetails).LastOrDefault();
                if (cuitfound != null)
                    result.Add(cuitfound);
            }

            return result.AsEnumerable();
        }

		public IEnumerable<Cae> AllCaes() => _dbCtx.Caes.ToList().AsEnumerable();
    }
}
