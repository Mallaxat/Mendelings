using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Core.Email
{
    public interface IEmailSenderStrategy
    {
        Task SendAsync(string recipientEmail,string subject,string message);
    }
}
