using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mendelings.Core.Services
{
    public class PasswordService
    {
        private const int SaltSize = 16; //случайные байты которые к паролю добавим
        private const int HashSize = 32; // размер хеширования
        private const int Iterations = 100000; //усложнение пароля

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            //Pbkdf2- получить защищенное значение из пароля
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password,salt,Iterations,HashAlgorithmName.SHA256,HashSize);

            return $"{Convert.ToBase64String(salt)}|{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            //Разделяем хеш и соль
            string[] parts = passwordHash.Split('|');

            if (parts.Length != 2) return false;

            try
            {
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] savedHash = Convert.FromBase64String(parts[1]);

                byte[] enteredHash = Rfc2898DeriveBytes.Pbkdf2(password,salt,Iterations,HashAlgorithmName.SHA256,HashSize);

                return CryptographicOperations.FixedTimeEquals(savedHash,enteredHash);
            }
            catch
            {
                return false;
            }
        }
    
    
    
    
    }
}
