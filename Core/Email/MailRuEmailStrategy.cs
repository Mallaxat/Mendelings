using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Core.Email
{
    public class MailRuEmailStrategy : IEmailSenderStrategy
    {
        private const string SmtpHost = "smtp.mail.ru";
        private const int SmtpPort = 465;

        // Почта, с которой Mendelings будет отправлять коды
        private const string SenderEmail = "alina-lapkina@mail.ru";

        // Пароль для внешнего приложения Mail.ru
        private const string SenderPassword = "hgLsyX6NlRrvvQlvm6Ix";

        public async Task SendAsync(string recipientEmail,string subject,string message)
        {
            MimeMessage email = new MimeMessage();

            email.From.Add(new MailboxAddress("Mendelings", SenderEmail));

            email.To.Add(MailboxAddress.Parse(recipientEmail));

            email.Subject = subject;

            email.Body = new TextPart("plain")
            {
                Text = message
            };

            using SmtpClient client = new SmtpClient();

            await client.ConnectAsync(SmtpHost,SmtpPort,SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(SenderEmail,SenderPassword);

            await client.SendAsync(email);

            await client.DisconnectAsync(true);
        }
    }
}
