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

namespace RepoWebShop.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _sender;
        private readonly string _serviceAccount;
        private readonly string _serviceAccountPrivateKey;
        private readonly string _zone;

        public EmailRepository(UserManager<ApplicationUser> userManager, AppDbContext appDbContext, IOrderRepository orderRepository, IHostingEnvironment env, IConfiguration config)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _orderRepository = orderRepository;
            _env = env;
            _config = config;

            _sender = _config.GetSection("EmailSender").Value;
            _serviceAccount = _config.GetSection("EmailServiceAccount").Value;
            _serviceAccountPrivateKey = _config.GetSection("EmailPrivateKey").Value;
            _zone = _config.GetSection("LocalZone").Value;
        }

        public void SendEmailActivationAsync(ApplicationUser appUser, string hostUrl)
        {
            appUser.ValidationMailToken = DateTime.Now.Zoned(_zone);
            _userManager.UpdateAsync(appUser);

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

        public void SendOrderConfirmation(Order order, string hostUrl, PaymentNotice payment = null)
        {
            if (order != null)
            {
                var orderdetails = _orderRepository.GetOrderDetails(order.OrderId);
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

        private MimeMessage GetMimeMessage(Email email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("De las Artes", _sender));
            message.To.Add(new MailboxAddress(email.To));
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
