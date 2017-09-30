﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

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

        public void Send(Order order, PaymentNotice payment = null)
        {
            if (order != null)
            {
                var orderdetails = _orderRepository.GetOrderDetails(order.OrderId);
                var comments = order.CustomerComments;

                var mercadopagomail = !_env.IsProduction() ? _config.GetSection("MercadoPagoTestEmail").Value : order.MercadoPagoMail;

                Email email = new Email()
                {
                    To = payment == null ? order.Registration.Email : mercadopagomail,
                    Bcc = "info@lareposteria.com.ar",
                    Subject = "La Reposteria - Confirmacion de " + (payment == null ? "Reserva" : "Compra"),
                    Body = buildHTML(orderdetails, order, comments)
                };

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("De las Artes", "info@lareposteria.com.ar"));
                message.To.Add(new MailboxAddress(email.To));
                message.Bcc.Add(new MailboxAddress(email.Bcc));
                message.Subject = email.Subject;

                message.Body = (new BodyBuilder() { HtmlBody = email.Body }).ToMessageBody();

                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
                        client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP
                        client.Authenticate("info@lareposteria.com.ar", "alamaula10");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                finally
                {
                    _appDbContext.Emails.Add(email);
                    _appDbContext.SaveChanges();
                }

            }
        }

        private string buildHTML(IEnumerable<OrderDetail> orderDetails, Order order, string comment)
        {
            string details = "";

            foreach(OrderDetail od in orderDetails)
            {
                details +=
                    "<tr>" +
                        $"<td style='padding:2px; text-align:center;'>{od.Amount}</td>" +
                        $"<td style='padding:2px; text-align:left;'>{od.Pie.PieDetail.Name} {od.Pie.Name}</td>" +
                        $"<td style='padding:2px; text-align:right;'>{od.Price}</td>" +
                        $"<td style='padding:2px; text-align:right;'>{od.Price * od.Amount}</td>" +
                    "</tr>";
            }

            var result =
            @"<body>
                <div style='text-align:center; items-align:center; font-family: ""Arial"", Georgia, Serif;'>
                  <section style=' border-radius: 25px; border: 2px solid #73AD21; padding: 20px; max-width:600px; min-width:400px;'>" +
                      $"<h2><strong>¡Gracias por tu reciente {(String.IsNullOrEmpty(order.MercadoPagoTransaction) ? "Reserva" : "Compra")}!</strong></h2>" +
                      @"<p>
                        Ya estamos trabajando para que puedas disfrutar de tu compra. Mientras tanto te mandamos los detalles.
                      </p>
                      <p style='font-size:10px;'>Codigo de Reserva </p>" +
                      $"<h1>{order.FriendlyBookingId}</h1>" +
                      (String.IsNullOrEmpty(order.MercadoPagoTransaction) ? "" : $"<p>Comprobante MercadoPago <strong>{order.MercadoPagoTransaction}</strong></p>") +
                      $"<p>Tiempo de Elaboracion <strong>{order.PreparationTime}hs</strong></p>" +
                      $"<p><strong>Detalle de Compra ${order.OrderTotal}</strong></p>" +

                      @"<table style='margin: auto;'>
                        <tr>
                          <th style='border-bottom: 1px solid #ddd;'>Cantidad</th>
                          <th style='border-bottom: 1px solid #ddd;'>Producto</th>
                          <th style='border-bottom: 1px solid #ddd;'>Precio U.</th>
                          <th style='border-bottom: 1px solid #ddd;'>Subtotal</th>
                        </tr>" +
                      details +
                      "</table>" +
                      (!String.IsNullOrWhiteSpace(comment) ? $"<p><strong>Comentarios</strong></p><p>{comment}</p>" : string.Empty) +
                  @"</section> 
                  <div style='text-align:left;'>
                    <p><strong>Importante</strong></p>
                    <p>Antes de pasar a buscar tu pedido, fijate nuestros </p><a href='www.lareposteria.com.ar/Calendar/OpenHours'>horarios de atencion</a>
                    <p>
                      Si queres retirar tu pedido en otro momento, ponete en contacto con nosotros y vamos a tratar de hacer lo posible por adaptarnos. Si estas muy apresurado, a veces podemos hacer las entregas antes del tiempo calculado, pero no siempre podremos.
                    </p>
                  </div>
                </div>
            </body>";

            return result;
        }
    }
}