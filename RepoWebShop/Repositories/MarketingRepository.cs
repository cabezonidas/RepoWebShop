using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class MarketingRepository : IMarketingRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendar;
        private readonly IEmailRepository _emailRepo;

        public MarketingRepository(AppDbContext appDbContext, ICalendarRepository calendar, IEmailRepository emailRepo)
        {
            _calendar = calendar;
            _emailRepo = emailRepo;
            _appDbContext = appDbContext;
        }

        public void Unsubscribe(string email)
        {
            if (!_appDbContext.Unsubscribed.Any(x => x.Email == email))
            {
                _appDbContext.Unsubscribed.Add(new Unsubscribe { Email = email, Unsubscribed = _calendar.LocalTime() });
                _appDbContext.SaveChanges();
            }
        }

        public IEnumerable<string> GetAllEmails()
        {
            var emails = new List<string>();
            emails.AddRange(_appDbContext.Orders.Select(x => x.MercadoPagoMail));
            emails.AddRange(_appDbContext.Users.Select(x => x.Email));
            emails.AddRange(_appDbContext.PaymentNotices.Select(x => x.MercadoPagoMail));
            emails = emails.Where(x => x.IsValidEmail()).Select(x => x.ToLower().Trim()).Distinct().ToList();

            var unsubscribed = _appDbContext.Unsubscribed.Select(x => x.Email.ToLower().Trim());
            var result = emails.Where(x => x.IsValidEmail() && !unsubscribed.Contains(x)).ToList();

            return result;
        }

        public IEnumerable<EmailMarketingTemplate> GetTemplates()
        {
            return _appDbContext.EmailMarketingTemplates.OrderByDescending(x => x.Created).ToList();
        }



        public EmailMarketingTemplate GetTemplatesById(int templateId)
        {
            var result = GetTemplates().First(x => x.EmailMarketingTemplateId == templateId);
            return result;
        }

        public async Task SendPromoEmailsAsync(int templateId, string email = null)
        {
            var template = GetTemplates().First(x => x.EmailMarketingTemplateId == templateId);
            var allEmails = new List<string>();

            if (email != null)
                allEmails.Add(email);
            else
                allEmails.AddRange(GetAllEmails());

            await _emailRepo.SendPromoAsync(template.Title, template.EmailBody, allEmails);
            var history = allEmails.Select(x => new EmailMarketingHistory { Email = x, EmailTemplate = template, Sent = _calendar.LocalTime() });
            await _appDbContext.EmailMarketingHistory.AddRangeAsync(history);
            await _appDbContext.SaveChangesAsync();
        }

        public void SaveTemplate(string subject, string bodyHtml)
        {
            var result = new EmailMarketingTemplate
            {
                Created = _calendar.LocalTime(),
                EmailBody = bodyHtml,
                Title = subject
            };
            _appDbContext.EmailMarketingTemplates.Add(result);
            _appDbContext.SaveChanges();
        }

    }
}
