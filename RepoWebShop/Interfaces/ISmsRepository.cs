using Twilio.Rest.Api.V2010.Account;

namespace RepoWebShop.Interfaces
{
    public interface ISmsRepository
    {
        MessageResource SendSms(string phone, string body);
        void NotifyAdmins(string v);
    }
}
