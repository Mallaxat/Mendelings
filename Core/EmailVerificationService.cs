using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    public class EmailVerificationService
    {
        private string? _email;
        private string? _code;
        private DateTime? _expiresAt;

        public string GenerateCode(string email)
        {
            Random random = new Random();
            _code = random.Next(100000, 1000000).ToString();
            _email = email;

            // время действие кода
            _expiresAt = DateTime.Now.AddMinutes(10);
            return _code;
        }

        public bool VerifyCode(string email, string code)
        {
            if (_email == null || _code == null || _expiresAt == null)
                return false;

            if (DateTime.Now > _expiresAt)
                return false;

            if (_email != email)
                return false;

            return _code == code;
        }

        public void Clear()
        {
            _email = null;
            _code = null;
            _expiresAt = null;
        }
    }
}
