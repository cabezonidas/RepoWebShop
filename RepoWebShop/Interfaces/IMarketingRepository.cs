using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Types;

namespace RepoWebShop.Interfaces
{
    public interface IMarketingRepository
    {
        void Unsubscribe(string email);
        void SaveTemplate(string subject, string htmlBody);
        Task SendPromoEmailsAsync(int templateId, string email);
        IEnumerable<EmailMarketingTemplate> GetTemplates();
        EmailMarketingTemplate GetTemplatesById(int templateId);
        IEnumerable<string> GetAllMobiles();
    }
}
