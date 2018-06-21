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
            var payerData = new PayerDataRevenue(order);

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
                        cae.InvoiceData = invoiceData; return cae;}));
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

        private FECAEResponse ParseFeCaeResponse(ElectronicInvoiceTest.FECAESolicitarResponse input)
        {
            var errs = input.Body.FECAESolicitarResult.Errors?.Select(x => _mapper.Map<ElectronicInvoiceTest.Err, FECAEResponse.CodeMessage>(x));
            var events = input.Body.FECAESolicitarResult.Events?.Select(x => _mapper.Map<ElectronicInvoiceTest.Evt, FECAEResponse.CodeMessage>(x));
            var feCaeCabResponse = _mapper.Map<ElectronicInvoiceTest.FECAECabResponse, FECAEResponse.FECAECabResponse>(input.Body.FECAESolicitarResult.FeCabResp);
            var feDetResp = input.Body.FECAESolicitarResult.FeDetResp?.Select(x => _mapper.Map <ElectronicInvoiceTest.FECAEDetResponse, FECAEResponse.FECAEDetResponse>(x));

            return new FECAEResponse(errs, events, feCaeCabResponse, feDetResp);
        }

        private FECAEResponse ParseFeCaeResponse(ElectronicInvoiceProd.FECAESolicitarResponse input)
        {
            var errs = input.Body.FECAESolicitarResult.Errors?.Select(x => _mapper.Map<ElectronicInvoiceProd.Err, FECAEResponse.CodeMessage>(x));
            var events = input.Body.FECAESolicitarResult.Events?.Select(x => _mapper.Map<ElectronicInvoiceProd.Evt, FECAEResponse.CodeMessage>(x));
            var feCaeCabResponse = _mapper.Map<ElectronicInvoiceProd.FECAECabResponse, FECAEResponse.FECAECabResponse>(input.Body.FECAESolicitarResult.FeCabResp);
            var feDetResp = input.Body.FECAESolicitarResult.FeDetResp?.Select(x => _mapper.Map<ElectronicInvoiceProd.FECAEDetResponse, FECAEResponse.FECAEDetResponse>(x));

            return new FECAEResponse(errs, events, feCaeCabResponse, feDetResp);
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
            var certPath = $"{_env.ContentRootPath}\\Certs\\{(isProd ? "RepoProd.p12" : "RepoTest.pfx")}" ;
            var key = _config.GetValue<string>("AfipKeyCert");
            
            var cert = new X509Certificate2(File.ReadAllBytes(certPath), key, X509KeyStorageFlags.Exportable);
            return cert;
        }

        private async Task<FECAEResponse> FECAESolicitarAsync(FECAERequestInfo requestInfo)
        {
            if (_isProd)
            {
                var config = new ElectronicInvoiceProd.ServiceSoapClient.EndpointConfiguration();
                var soapClient = new ElectronicInvoiceProd.ServiceSoapClient(config);

                var fECompUltimo = await soapClient.FECompUltimoAutorizadoAsync(
                    _mapper.Map<FEAuthRequest, ElectronicInvoiceProd.FEAuthRequest>(requestInfo.AuthRequest),
                    requestInfo.FeCabReq.PtoVta, requestInfo.FeCabReq.CbteTipo);

                requestInfo.CbteDesde = fECompUltimo.Body.FECompUltimoAutorizadoResult.CbteNro + 1;
                requestInfo.CbteHasta = requestInfo.CbteDesde;

                var feCaeRequest = new ElectronicInvoiceProd.FECAERequest();
                feCaeRequest.FeCabReq = _mapper.Map<FECAECabRequest, ElectronicInvoiceProd.FECAECabRequest>(requestInfo.FeCabReq);
                feCaeRequest.FeDetReq = new List<ElectronicInvoiceProd.FECAEDetRequest> {
                    _mapper.Map<FECAEDetRequest, ElectronicInvoiceProd.FECAEDetRequest>(new FECAEDetRequest(requestInfo))
                }.ToArray();

                var response = await soapClient.FECAESolicitarAsync(_mapper.Map<FEAuthRequest, ElectronicInvoiceProd.FEAuthRequest>(requestInfo.AuthRequest), feCaeRequest);

                return ParseFeCaeResponse(response);
            }
            else
            {
                var config = new ElectronicInvoiceTest.ServiceSoapClient.EndpointConfiguration();
                var soapClient = new ElectronicInvoiceTest.ServiceSoapClient(config);

                var fECompUltimo = await soapClient.FECompUltimoAutorizadoAsync(
                    _mapper.Map<FEAuthRequest, ElectronicInvoiceTest.FEAuthRequest>(requestInfo.AuthRequest),
                    requestInfo.FeCabReq.PtoVta, requestInfo.FeCabReq.CbteTipo);

                requestInfo.CbteDesde = fECompUltimo.Body.FECompUltimoAutorizadoResult.CbteNro + 1;
                requestInfo.CbteHasta = requestInfo.CbteDesde;

                var feCaeRequest = new ElectronicInvoiceTest.FECAERequest();
                feCaeRequest.FeCabReq = _mapper.Map<FECAECabRequest, ElectronicInvoiceTest.FECAECabRequest>(requestInfo.FeCabReq);
                feCaeRequest.FeDetReq = new List<ElectronicInvoiceTest.FECAEDetRequest> {
                    _mapper.Map<FECAEDetRequest, ElectronicInvoiceTest.FECAEDetRequest>(new FECAEDetRequest(requestInfo))
                }.ToArray();

                var response = await soapClient.FECAESolicitarAsync(_mapper.Map<FEAuthRequest, ElectronicInvoiceTest.FEAuthRequest>(requestInfo.AuthRequest), feCaeRequest);

                return ParseFeCaeResponse(response);
            }
        }

        public async Task<bool> ValidPersonaAsync(long id)
        {
            Cuit cuit = new Cuit
            {
                Created = _calendar.LocalTime(),
                Number = id,
                Valid = true
            };
            //Puedo omitir el ambiente de homologacion y hacerlo siempre en prod.
            var endpoint = new PadronProd.PersonaServiceA5Client.EndpointConfiguration();
            var client = new PadronProd.PersonaServiceA5Client(endpoint);
            try
            {
                var getPersonaResponse = await client.getPersonaAsync(await GetTokenTicket(AfipWsPersona, true), await GetSignTicket(AfipWsPersona, true), _config.GetValue<long>("CUIT"), id);
            }
            catch
            {
                cuit.Valid = false;
            }
            _dbCtx.Cuits.Add(cuit);
            _dbCtx.SaveChanges();

            return cuit.Valid;
        }

        public async Task<IEnumerable<InvoiceData>> GetAll(Func<InvoiceData, bool> condition = null)
        {
            return await _dbCtx.InvoiceData.Where(x => condition == null || condition(x))
                    .Include(x => x.Caes)
                    .Include(x => x.InvoiceDetails)
                    .Include(x => x.Order)
                    .ToListAsync();
        }

        public async Task<InvoiceData> GetById(int id)
        {
            return (await GetAll(x => x.InvoiceDataId == id))?.FirstOrDefault();
        }
    }
}
