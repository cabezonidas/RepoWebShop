using RepoWebShop.Interfaces;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using RepoWebShop.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509.Store;
using System.Collections.Generic;
using System.Collections;
using Org.BouncyCastle.Asn1.Pkcs;

namespace RepoWebShop.Repositories
{
    public class ElectronicBillingRepository : IElectronicBillingRepository
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ICalendarRepository _calendar;
        private readonly bool prod;

        private string LoginTicketRequestForElectronicBilling
        {
            get
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
                XmlLoginTicketRequest.SelectSingleNode("//destination").InnerText = $"CN=wsaa{(!prod ? "homo" : string.Empty)}, O=AFIP, C=AR, SERIALNUMBER=CUIT 33693450239";
                var id = string.Concat(_calendar.LocalTime().ToUniversalTime().Ticks.ToString().ToCharArray().TakeLast(7));
                XmlLoginTicketRequest.SelectSingleNode("//uniqueId").InnerText = id;
                XmlLoginTicketRequest.SelectSingleNode("//generationTime").InnerText = _calendar.LocalTime().AddMinutes(-10).ToString("s");
                XmlLoginTicketRequest.SelectSingleNode("//expirationTime").InnerText = _calendar.LocalTime().AddMinutes(+10).ToString("s");
                XmlLoginTicketRequest.SelectSingleNode("//service").InnerText = "wsfe";
                var result = XmlLoginTicketRequest.OuterXml;
                return result;
            }
        }

        public ElectronicBillingRepository(IHostingEnvironment env, IConfiguration config, ICalendarRepository calendar)
        {
            _calendar = calendar;
            _env = env;
            _config = config;
            prod = _env.IsProduction();
        }

        public async Task<string> GetLoginTicket()
        {
          var sample = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<loginTicketResponse version=\"1.0\">" +
                "<header>" +
                    "<source>CN=wsaahomo, O=AFIP, C=AR, SERIALNUMBER=CUIT 33693450239</source>" +
                    "<destination>SERIALNUMBER=CUIT 27148090330, CN=repotest</destination>" +
                    "<uniqueId>1147735260</uniqueId>" +
                    "<generationTime>2018-06-11T06:34:51.858-03:00</generationTime>" +
                    "<expirationTime>2018-06-11T18:34:51.858-03:00</expirationTime>" +
                "</header>" +
                "<credentials>" +
                    "<token>PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMzY5MzQ1MDIzOSIgZHN0PSJDTj13c2ZlLCBPPUFGSVAsIEM9QVIiIHVuaXF1ZV9pZD0iMTgxMDg4OTc1OSIgZ2VuX3RpbWU9IjE1Mjg3MDk2MzEiIGV4cF90aW1lPSIxNTI4NzUyODkxIi8+CiAgICA8b3BlcmF0aW9uIHR5cGU9ImxvZ2luIiB2YWx1ZT0iZ3JhbnRlZCI+CiAgICAgICAgPGxvZ2luIGVudGl0eT0iMzM2OTM0NTAyMzkiIHNlcnZpY2U9IndzZmUiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjcxNDgwOTAzMzAsIENOPXJlcG90ZXN0IiBhdXRobWV0aG9kPSJjbXMiIHJlZ21ldGhvZD0iMjIiPgogICAgICAgICAgICA8cmVsYXRpb25zPgogICAgICAgICAgICAgICAgPHJlbGF0aW9uIGtleT0iMjcxNDgwOTAzMzAiIHJlbHR5cGU9IjQiLz4KICAgICAgICAgICAgPC9yZWxhdGlvbnM+CiAgICAgICAgPC9sb2dpbj4KICAgIDwvb3BlcmF0aW9uPgo8L3Nzbz4K</token>" +
                    "<sign>JTPsX/KPyfl2sw35eqEdHQKX/HsCA43iJqUW7hrZdr5V/bwY9TaMRT9LNUKYrHrzG7vYszHWJbMYTo2xlex8NqvHtdPZqAOmeDA+kqwg9oVHxvZyh2nu/hRycGMtaYnnHkA9WetQ88wwHsrH9VsYH2hVTxGlNTnZBWKZgmINLb8=</sign>" +
                "</credentials>" +
            "</loginTicketResponse>";


            XmlDocument xml = new XmlDocument();
            xml.LoadXml(sample);
            var source = xml.SelectSingleNode("//source").InnerText;
            var destination = xml.SelectSingleNode("//destination").InnerText;
            var uniqueId = xml.SelectSingleNode("//uniqueId").InnerText;

            //var generationTime = TimeZoneInfo.ConvertTime(DateTime.Parse(xml.SelectSingleNode("//generationTime").InnerText),
            //    TimeZoneInfo.FindSystemTimeZoneById(_config.GetSection("LocalZone").Value));
            //var expirationTime = TimeZoneInfo.ConvertTime(DateTime.Parse(xml.SelectSingleNode("//generationTime").InnerText),
            //    TimeZoneInfo.FindSystemTimeZoneById(_config.GetSection("LocalZone").Value));

            var token = xml.SelectSingleNode("//token").InnerText;
            var sign = xml.SelectSingleNode("//sign").InnerText;




            var msg = SignMessage(LoginTicketRequestForElectronicBilling);
            try
            {
                if (prod)
                {
                    var LoginCMS = new LoginCMSProd.LoginCMSClient();
                    var response = await LoginCMS.loginCmsAsync(msg);
                    return response.loginCmsReturn;
                }
                else
                {
                    var LoginCMS = new LoginCMSTest.LoginCMSClient();
                    var response = await LoginCMS.loginCmsAsync(msg);
                    return response.loginCmsReturn;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
        }

        private string SignMessage(string msg)
        {
            var gen = new CmsSignedDataGenerator();
            X509Certificate2 certificate = GetCertificate();
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

        private X509Certificate2 GetCertificate()
        {
            var certPath = $"{_env.ContentRootPath}\\Certs\\{(prod ? "RepoProd.p12" : "RepoTest.pfx")}" ;
            var key = _config.GetValue<string>("AfipKeyCert");
            
            var cert = new X509Certificate2(File.ReadAllBytes(certPath), key, X509KeyStorageFlags.Exportable);
            return cert;
        }
    }
}
