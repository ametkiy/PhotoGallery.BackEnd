using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Utils
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Photo Gallery site", "admin@photogallery.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            var emailServerConfig = new EmailServerConfig(_config);

            if (emailServerConfig == null) throw new Exception();

            using (var client = new SmtpClient())
            {
                client.MessageSent += OnMessageSent;
                await client.ConnectAsync(emailServerConfig.SmtpServer, emailServerConfig.Port, emailServerConfig.UseSSL);
                await client.AuthenticateAsync(emailServerConfig.MailUserName, emailServerConfig.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        private void OnMessageSent(object sender, MessageSentEventArgs e)
        {
            Debug.Write(e.Message);
        }
    }

    public class EmailServerConfig
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string MailUserName { get; set; }
        public string Password { get; set; }

        public EmailServerConfig(IConfiguration config)
        {
            SmtpServer = config.GetValue<string>("SmtpServer");
            Port = config.GetValue<int>("Port");
            UseSSL = config.GetValue<bool>("UseSSL");
            MailUserName = config.GetValue<string>("MailUserName");
            Password = config.GetValue<string>("Password");
        }
    }
}
