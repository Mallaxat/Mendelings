using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Core.Email
{
    public class GmailEmailStrategy : IEmailSenderStrategy
    {
        public Task SendAsync(string recipientEmail, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}


