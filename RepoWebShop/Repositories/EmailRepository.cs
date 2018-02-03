using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using RepoWebShop.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Linq;

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

        public EmailRepository(AppDbContext appDbContext, IHostingEnvironment env, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _env = env;
            _config = config;

            _sender = _config.GetSection("EmailSender").Value;
            _serviceAccount = _config.GetSection("EmailServiceAccount").Value;
            _serviceAccountPrivateKey = _config.GetSection("EmailPrivateKey").Value;
            _zone = _config.GetSection("LocalZone").Value;
        }

        public void SendEmailActivationAsync(ApplicationUser appUser, string hostUrl)
        {
            string hash = SHA256.Create().FromString(appUser.ValidationMailToken.ToString());
            
            var apicall = $"{hostUrl}/Account/EmailVerificationBodyEmail/{appUser.UserName}/{hash}";
            Task<HttpResponseMessage> responseTask = new HttpClient().GetAsync(apicall);
            responseTask.Wait();

            Email email = new Email()
            {
                To = appUser.Email,
                Bcc = _sender,
                Subject = "De las Artes - Confirmación de cuenta",
                Body = responseTask.Result.Content.ReadAsStringAsync().Result
            };

            try
            {
                SendMail(GetMimeMessage(email));
            }
            finally
            {
                var emailsaved = _appDbContext.Emails.Add(email);
                _appDbContext.SaveChanges();
            }
        }

        public void NotifyOrderComplete(Order order, string hostUrl)
        {
            string userEmail = order.Registration?.Email;
            string orderEmail = order.Email?.To;

            string principalEmail;
            string secondaryEmail;

            if (orderEmail == null && userEmail == null)
                return;

            principalEmail = userEmail ?? orderEmail;
            secondaryEmail = principalEmail == userEmail ? orderEmail : null;

            var apicall = $"{hostUrl}/Order/OrderComplete/{order.OrderId}/";
            Task<HttpResponseMessage> responseTask = new HttpClient().GetAsync(apicall);
            responseTask.Wait();

            Email email = new Email()
            {
                To = principalEmail,
                //Bcc = _sender,
                Subject = $"De las Artes - ¡Pedido {order.FriendlyBookingId} listo!",
                Body = responseTask.Result.Content.ReadAsStringAsync().Result
            };

            try
            {
                SendMail(GetMimeMessage(email, secondaryEmail));
            }
            finally
            {
                var emailsaved = _appDbContext.Emails.Add(email);
                _appDbContext.SaveChanges();
            }
        }

        public void SendOrderConfirmation(Order order, string hostUrl, PaymentNotice payment = null)
        {
            if (order != null)
            {
                var comments = order.CustomerComments;

                var mercadopagomail = !_env.IsProduction() ? _config.GetSection("MercadoPagoTestEmail").Value : order.MercadoPagoMail;

                var apicall = $"{hostUrl}/Order/EmailNotification/{order.OrderId}";
                Task<HttpResponseMessage> responseTask = new HttpClient().GetAsync(apicall);
                responseTask.Wait();
                
                Email email = new Email()
                {
                    To = payment == null ? order.Registration.NormalizedEmail.ToLower() : mercadopagomail,
                    Bcc = _sender,
                    Subject = "De las Artes - Confirmación de " + (payment == null ? "reserva" : "compra"),
                    Body = responseTask.Result.Content.ReadAsStringAsync().Result
                };
                
                try
                {
                    SendMail(GetMimeMessage(email));
                }
                finally
                {
                    var emailsaved = _appDbContext.Emails.Add(email);
                    order.Email = emailsaved.Entity;
                    _appDbContext.SaveChanges();
                }

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

        private void SendMail(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new ServiceAccountCredential(new ServiceAccountCredential
                    .Initializer(_serviceAccount)
                {
                    Scopes = new[] { "https://mail.google.com/" },
                    User = _sender
                }.FromPrivateKey(_serviceAccountPrivateKey));

                credential.RequestAccessTokenAsync(CancellationToken.None).GetAwaiter().GetResult();

                var oauth2 = new SaslMechanismOAuth2(_sender, credential.Token.AccessToken);

                client.Connect("smtp.gmail.com", 587);
                client.Authenticate(oauth2);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
