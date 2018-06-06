using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Lookups.V1;
using Twilio.Types;

namespace RepoWebShop.Repositories
{
    public class SmsRepository : ISmsRepository
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _sender;
        private readonly IConfiguration _config;
        private readonly AppDbContext _appDbContext;
        private readonly IHostingEnvironment _env;
        private readonly ICalendarRepository _calendar;

        public SmsRepository(IHostingEnvironment env, IConfiguration config, AppDbContext appDbContext, ICalendarRepository calendar)
        {
            _calendar = calendar;
            _env = env;
            _appDbContext = appDbContext;
            _accountSid = config.GetSection("TwilioAccoundSid").Value;
            _authToken = config.GetSection("TwilioAuthToken").Value;
            _sender = config.GetSection("TwilioSender").Value;
            _config = config;
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task NotifyAdminsAsync(string v)
        {
            //var numbers = _config.GetSection("AdminMobiles").GetChildren().Select(x => x.Value);
            if(_env.IsProduction())
            {
                var numbers = _appDbContext.AdminNotifications.Select(x => x.Phone);
                foreach(var number in numbers)
                    await SendSms(number, v);
            }
        }

        public async Task<MessageResource> SendSms(string phone, string body)
        {
            var result = MessageResource.Create(
                from: new PhoneNumber(_sender),
                to: new PhoneNumber(await ParseNumberAsync(phone)),
                body: body);

            _appDbContext.SmsHistory.Add(new SmsHistory { Body = body, Destintation = phone, Date = _calendar.LocalTime() });
            _appDbContext.SaveChanges();
            return result;
        }


        public async Task<IEnumerable<string>> GetFormattedNumbers(IEnumerable<string> numbers)
        {
            var results = new List<string>();
            foreach(var number in numbers)
            {
                try
                {
                    var result = await ParseNumberAsync(number);
                    results.Add(result);
                }
                catch
                { }
            }
            return results.Distinct();
        }

        private static async Task<string> ParseNumberAsync(string number)
        {
            number = new string(number.Where(c => char.IsDigit(c)).ToArray());
            if (number.Length == 10 && number.Substring(0, 2) == "15")
                number = "11" + number.Substring(2, 8);

            var phoneNumber = await PhoneNumberResource.FetchAsync(
                countryCode: "AR",
                pathPhoneNumber: new PhoneNumber(number));

            var result = phoneNumber.PhoneNumber.ToString();

            if (result.Length >= 5 && result.Substring(0, 5) == "+5411")
                result = "+54911" + result.Substring(5, result.Length - 5);

            return result;
        }
    }
}
