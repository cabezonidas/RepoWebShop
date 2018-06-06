using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RepoWebShop.Interfaces
{
    public interface ISmsRepository
    {
        Task<MessageResource> SendSms(string phone, string body);
        Task NotifyAdminsAsync(string v);
        Task<IEnumerable<string>> GetFormattedNumbers(IEnumerable<string> numbers);
    }
}
