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

namespace RepoWebShop.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;

        public EmailRepository(AppDbContext appDbContext, IOrderRepository orderRepository, IHostingEnvironment env, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _orderRepository = orderRepository;
            _env = env;
            _config = config;
        }

        public void Send(Order order, string hostUrl, PaymentNotice payment = null)
        {
            var sender = _config.GetSection("EmailSender").Value;
            var serviceAccount = _config.GetSection("EmailServiceAccount").Value;

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
                    Bcc = sender,
                    Subject = "La Reposteria - Confirmacion de " + (payment == null ? "reserva" : "compra"),
                    Body = responseTask.Result.Content.ReadAsStringAsync().Result
                };

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("De las Artes", sender));
                message.To.Add(new MailboxAddress(email.To));
                message.Bcc.Add(new MailboxAddress(email.Bcc));
                message.Subject = email.Subject;

                message.Body = (new BodyBuilder() { HtmlBody = email.Body }).ToMessageBody();

                try
                {
                    using (var client = new SmtpClient())
                    {
                        var certificate = new X509Certificate2(@"MailCertificate.p12", "notasecret", X509KeyStorageFlags.Exportable);
                        var credential = new ServiceAccountCredential(new ServiceAccountCredential
                            .Initializer(serviceAccount)
                        {
                            Scopes = new[] { "https://mail.google.com/" },
                            User = sender
                        }.FromCertificate(certificate));

                        credential.RequestAccessTokenAsync(CancellationToken.None).GetAwaiter().GetResult();

                        var oauth2 = new SaslMechanismOAuth2(sender, credential.Token.AccessToken);

                        client.Connect("smtp.gmail.com", 587);
                        client.AuthenticateAsync(oauth2);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                finally
                {
                    var emailsaved = _appDbContext.Emails.Add(email);
                    order.Email = emailsaved.Entity;
                    _appDbContext.SaveChanges();
                }

            }
        }
    }
}
