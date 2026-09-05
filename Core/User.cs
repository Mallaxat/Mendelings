using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        //подтверждение почты
        public bool IsEmailConfirmed { get; set; }
        public List<Pet> Pets { get; set; } = new();
    }
}
