using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //MercadoPago
                var orderdetails = _orderRepository.GetOrderDetails(order.OrderId);
                var comments = order.CustomerComments;

                Email email = new Email()
                {
                    To = "info@lareposteria.com.ar",
                    Subject = "subject test",
                    Body = "body test"
                };

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Seba Cabe", "sebastian.scd@gmail.com"));
                message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "sebastian.cabeza@outlook.com"));
                message.Subject = email.Subject;

                message.Body = new TextPart("plain")
                {
                    Text = email.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587);


                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("sebastian.scd@gmail.com", "palmera561");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}
