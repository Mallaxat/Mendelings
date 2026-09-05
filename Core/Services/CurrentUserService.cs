using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core.Services
{//Отвечает за то какой пользователь сейчас работает
    public class CurrentUserService
    {
        public int? UserId { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public bool IsLoggedIn => UserId != null;

        public void SetUser(User user)
        {
            UserId = user.Id;
            Username = user.Username;
        }

        public void Logout()
        {
            UserId = null;
            Username = string.Empty;
        }
    }
}
