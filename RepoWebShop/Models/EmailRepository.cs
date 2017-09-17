using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRepository;

        public EmailRepository(AppDbContext appDbContext, IOrderRepository orderRepository)
        {
            _appDbContext = appDbContext;
            _orderRepository = orderRepository;
        }

        public void Send(Order order)
        {
            if (order != null)
            {
                var orderdetails = _orderRepository.GetOrderDetails(order.OrderId);
                var comments = order.CustomerComments;

                Email email = new Email()
                {
                    To = "info@lareposteria.com.ar",
                    Subject = "subject test",
                    Body = "body test"
                };

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Seba Cabe", "info@lareposteria.com.ar"));
                message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "sebastian.cabeza@outlook.com"));
                message.Subject = email.Subject;

                message.Body = new TextPart("plain")
                {
                    Text = email.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
                    client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP
                    client.Authenticate("info@lareposteria.com.ar", "alamaula10");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}
