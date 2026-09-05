using Mendelings.Core.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Core.Services
{
    public class EmailService
    {
        private readonly IEmailSenderStrategy _emailSender;
        private readonly EmailVerificationService _verificationService;

        public EmailService(IEmailSenderStrategy emailSender,EmailVerificationService verificationService)
        {
            _emailSender = emailSender;
            _verificationService = verificationService;
        }

        // Создать код и отправить его пользователю
        public async Task SendVerificationCodeAsync(string email)
        {
            string code = _verificationService.GenerateCode(email);

            await _emailSender.SendAsync(email,
                "Код подтверждения Mendelings",
                $"Ваш код подтверждения: {code}\n\nКод действует 10 минут.");
        }

        // Проверить введенный пользователем код
        public bool VerifyCode(string email, string code)
        {
            return _verificationService.VerifyCode(email, code);
        }

    }
}
