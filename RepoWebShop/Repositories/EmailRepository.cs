using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.Security.Cryptography;
using RepoWebShop.Extensions;
using Microsoft.AspNetCore.Http;

namespace RepoWebShop.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _appDbContext;
        //private readonly IOrderRepository _orderRepository; MAKES CIRCULAR DEPENDENCY
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly string _sender;
        private readonly string _serviceAccount;
        private readonly string _serviceAccountPrivateKey;
        private readonly string _zone;
        private readonly string host;


        public EmailRepository(AppDbContext appDbContext, IHttpContextAccessor contextAccessor, IHostingEnvironment env, IConfiguration config)
        {
            host = "http://" + contextAccessor.HttpContext?.Request.Host.ToString();
            _appDbContext = appDbContext;
            _env = env;
            _config = config;

            _sender = _config.GetSection("EmailSender").Value;
            _serviceAccount = _config.GetSection("EmailServiceAccount").Value;
            _serviceAccountPrivateKey = _config.GetSection("EmailPrivateKey").Value;
            _zone = _config.GetSection("LocalZone").Value;
        }

        public async Task SendEmailActivationAsync(ApplicationUser appUser)
        {
            string hash = SHA256.Create().FromString(appUser.ValidationMailToken.ToString());
            
            var apicall = $"{host}/Account/EmailVerificationBodyEmail/{appUser.UserName}/{hash}";
            HttpResponseMessage responseTask = await new HttpClient().GetAsync(apicall);

            Email email = new Email()
            {
                To = appUser.Email,
                Bcc = _sender,
                Subject = "De las Artes - Confirmación de cuenta",
                Body = await responseTask.Content.ReadAsStringAsync()
            };

            try
            {
                await SendMailAsync(GetMimeMessage(email));
            }
            finally
            {
                var emailsaved = _appDbContext.Emails.Add(email);
                _appDbContext.SaveChanges();
            }
        }

        public async Task NotifyOrderCompleteAsync(Order order)
        {
            string principalEmail, secondaryEmail;
            GetOrderEmails(order, out principalEmail, out secondaryEmail);

            var apicall = $"{host}/Order/OrderComplete/{order.OrderId}/";
            HttpResponseMessage responseTask = await new HttpClient().GetAsync(apicall);

            Email email = new Email()
            {
                To = principalEmail,
                Subject = $"De las Artes - ¡Pedido {order.FriendlyBookingId} listo!",
                Body = await responseTask.Content.ReadAsStringAsync()
            };

            try
            {
                await SendMailAsync(GetMimeMessage(email, secondaryEmail));
            }
            finally
            {
                var emailsaved = _appDbContext.Emails.Add(email);
                _appDbContext.SaveChanges();
            }
        }
        
        public async Task SendOrderConfirmationAsync(Order order, Action notifyAdmins)
        {
            if (order != null)
            {
                var comments = order.CustomerComments;

                string principalEmail, secondaryEmail;
                GetOrderEmails(order, out principalEmail, out secondaryEmail);

                var apicall = $"{host}/Order/EmailNotification/{order.OrderId}";
                HttpResponseMessage responseTask = await new HttpClient().GetAsync(apicall);
                
                Email email = new Email()
                {
                    To = principalEmail,
                    Bcc = _sender,
                    Subject = "De las Artes - Confirmación de orden",
                    Body = await responseTask.Content.ReadAsStringAsync()
                };
                
                try
                {
                    await SendMailAsync(GetMimeMessage(email, secondaryEmail));
                }
                finally
                {
                    var emailsaved = _appDbContext.Emails.Add(email);
                    order.Email = emailsaved.Entity;
                    _appDbContext.SaveChanges();
                }

            }
            notifyAdmins();
        }
        
        public async Task SendEmailResetPasswordAsync(ApplicationUser foundUser)
        {
            string hash = SHA256.Create().FromString(foundUser.Id.ToString());

            var apicall = $"{host}/Account/ResetPasswordBodyEmail/{foundUser.Id}/{hash}";
            HttpResponseMessage responseTask = await new HttpClient().GetAsync(apicall);

            Email email = new Email()
            {
                To = foundUser.Email,
                Subject = "De las Artes - Reestablecer contraseña",
                Body = await responseTask.Content.ReadAsStringAsync()
            };

            try
            {
                await SendMailAsync(GetMimeMessage(email));
            }
            finally
            {
                var emailsaved = _appDbContext.Emails.Add(email);
                _appDbContext.SaveChanges();
            }
        }

        private async Task SendMailAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new ServiceAccountCredential(new ServiceAccountCredential
                    .Initializer(_serviceAccount)
                {
                    Scopes = new[] { "https://mail.google.com/" },
                    User = _sender
                }.FromPrivateKey(_serviceAccountPrivateKey));

                await credential.RequestAccessTokenAsync(CancellationToken.None);

                var oauth2 = new SaslMechanismOAuth2(_sender, credential.Token.AccessToken);

                await client.ConnectAsync("smtp.gmail.com", 587);
                await client.AuthenticateAsync(oauth2);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private void GetOrderEmails(Order order, out string principalEmail, out string secondaryEmail)
        {
            if(_env.IsProduction())
            {
                principalEmail = order.Registration?.Email ?? order.MercadoPagoMail;
                secondaryEmail = principalEmail == order.MercadoPagoMail ? null : order.MercadoPagoMail;
            }
            else
            {
                var mpMail = _config.GetSection("MercadoPagoTestEmail").Value;
                principalEmail = order.Registration?.Email ?? mpMail;
                secondaryEmail = principalEmail == mpMail ? null : mpMail;
            }
        }
        private MimeMessage GetMimeMessage(Email email, string secondaryEmail = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("De las Artes", _sender));
            message.To.Add(new MailboxAddress(email.To));
            if(!String.IsNullOrEmpty(secondaryEmail))
                message.To.Add(new MailboxAddress(secondaryEmail));
            if (!String.IsNullOrEmpty(email.Bcc))
                message.Bcc.Add(new MailboxAddress(email.Bcc));
            message.Subject = email.Subject;
            message.Body = (new BodyBuilder() { HtmlBody = email.Body }).ToMessageBody();
            return message;
        }

    }
}
