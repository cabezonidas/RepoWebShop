using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace RepoWebShop.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        private void GetCredentialForApiAsync()
        {

            // Create an explicit ServiceAccountCredential credential
            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer("calendar@endless-splice-180407.iam.gserviceaccount.com")
            {
                Scopes = new[] {CalendarService.Scope.CalendarReadonly }
            }.FromPrivateKey("-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDGTEmQBXXS7Kp5\n4TijKAeKhsdbHXW4R8iDpqy4D3I2qj9HPHD6BG9IKTftHYzMwJLKKYayT8naGL3B\n7olm6QDP+0SBeWndHFzBkXdm+0PsWG5+XE1OejA37RwCZFdtuYiaH3GjH7Vb7eE9\nUhPkVVyp/RXXw0VfH5j+KjbVkYk9/FyqiyCNRIXIiUrn/q18G1bSEUhLhim3OJki\n0OUhbUVJ0gRLF+3AAk/a/ma83MqVQ99qBljVkemhswwrJ1tjoCgk1g5O+kymKgJf\njXpwwBVvkdXn5rzzoul0n3uhtvyv4M7SLm/6+tPV7okEAnybxQFlYoCOciilok1q\nliP0+wMRAgMBAAECggEAEwH5Ay8izicVaD1oems53dNxhJBcWl+3Hrd5UuNYJVlI\nW4G1qqGGMTqSO85bUr0a1PoKCUmQpbE9bzt4PD8Vbto9BNQoi6NOWNeixGJNbnhD\njyU129HmkVXPG4OWxdPE/lEJSYOelMNGleQqN/bevQSfdWEoYyfaOHK/ctVSfHhq\n7lS4CcAc0FlHE8+swJ8K8ffUnDAy+VCLBRQ7Zy0fmr/wwwpb74uBE3OiuQ1yOSeA\nOppX1S1KpwrtTaGcB/yJeSIqFnbK7bdoe4x4iS8OanaZ4pXVoU/VIIkEQs6h/JUd\nou58/nB6sw7dkGrrYnV189ZDB4naEv5OHVQBpXNo6QKBgQDzhk1Zrh3efah0xHwz\n9lHpY7G03jdOYWu2HZ7nmDN9aPBHWG4O2rF7Z78gzFbZ4/i191aB8gdsWX6f0J8X\nPtdP4HrTa/CvffyqIEWfxyNTfioRzAiqzvrjUIHK6Q5qvfhWmwnsFYfKZECLQoUm\nUO0SZ9turKZIaMYKANJloiYplwKBgQDQdNycTuKTYxSyEfS38Q1/+PpTclQGjSkN\nf6SQQ9hvSkiIa5FhTdM1ex+5e3p6U37HO6Y0T3x6VZaesxaqSHWnDYUSmVK/H3FP\nUwwHGHj+hOJAG42JlFZCRmw5+qOlyDccXCPI5CqylL5ZEWyOOoItCuKkwg5RAMcH\nCNpcity9lwKBgQDIWLx4RFwEeILHlgLMySB4j3Fa6Nq9XD6+ND5QMJJj4ZqUR7c7\n90W+zgqbFCF73yb1pYgBHjpnNHWNS5gnCqce6wKBv7Li3hapbCV7ntx8SzaXL7IN\nvH0B2HD/m58lyCZwMdsG5Guz4aiMKWzbqgZwdws7UUT4G2KaRjTSMoWM/wKBgGeV\nioKJmIFQvNn3njlBKUosOIf6ydIvlvTENswRSp2SthRED4vgllF/CzaqKyRaFSd/\ndomL7fkESsI1j8+yDfSi/TtOwRWD1FRsmg90k7s14Q+mIWD6OxrXz7PH3kXlBa2s\nA4QyDR2MWEH+LIlIfaNQQ2tHmsGZt0zBk3Bk+GelAoGAfH1qAFYQG1HhQn7WuFye\nqyAWn+/ElOO4Je1uD5EpSqpKnNJPRgzWapzMUUoel7Gw+dX/O23fL1btwo4VoArI\nxTQ0cCiT+c2J+QzfI8+dR3TiGqHoTlrO+crIZ85CG3e6VVwo9V7u6tegSgtn56IN\nYJGDHvABJ/pNR7HTatcTRgg=\n-----END PRIVATE KEY-----\n"));

            // Create the service
            CalendarService service = new CalendarService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                }
            );

            var test = service.Calendars.Get("primary").Execute();
            var test2 = service.Calendars.Get("info@lareposteria.com.ar").Execute();

            // some calls to Google API
            var act1 = service.CalendarList.List().Execute();

            return;
        }

        public ViewResult Index()
        {
            GetCredentialForApiAsync();
            return View();
        }
    }
}
