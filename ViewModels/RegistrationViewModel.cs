using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
using Mendelings.Core.Services;
using Mendelings.Data;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace Mendelings.ViewModels
{
    public partial class RegistrationViewModel : ViewModelBase
    {
        private readonly EmailService _emailService;
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;
        private readonly UserService _userService;
        private readonly CurrentUserService _currentUserService;

        private string? _verifiedEmail;

        [ObservableProperty]
        private string email = "Admin";

        [ObservableProperty]
        private string password = "123456";

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string code = string.Empty;

        [ObservableProperty]
        private string message = string.Empty;

        //Показать дополнительные поля регистрации
        [ObservableProperty]
        private bool isRegistrationMode;

        //Код был отправлен
        [ObservableProperty]
        private bool codeSent;

        //Почта подтверждена
        [ObservableProperty]
        private bool emailVerified;

        public event Action? AuthSuccess;

        public RegistrationViewModel(EmailService emailService, UserRepository userRepository, PasswordService passwordService, UserService userService, CurrentUserService currentUserService)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _userService = userService;
            _currentUserService = currentUserService;
        }

        //Открыть дополнительные поля регистрации
        [RelayCommand]
        private void ShowRegistration()
        {
            IsRegistrationMode = true;
            Message = string.Empty;
        }

        //Вернуться к обычному входу
        [RelayCommand]
        private void CancelRegistration()
        {
            IsRegistrationMode = false;
            CodeSent = false;
            EmailVerified = false;

            Username = string.Empty;
            Code = string.Empty;

            _verifiedEmail = null;

            Message = string.Empty;
        }

        //Вход по почте или логину
        [RelayCommand]
        private async Task LoginAsync()
        {
            Message = string.Empty;

            string login = Email.Trim();

            if (string.IsNullOrWhiteSpace(login))
            {
                Message = "Введите почту или логин";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                Message = "Введите пароль";
                return;
            }

            User? user = await _userRepository.GetByEmailOrUsernameAsync(login);

            if (user == null)
            {
                Message = "Пользователь не найден";
                return;
            }

            bool passwordCorrect = _passwordService.VerifyPassword(Password, user.PasswordHash);

            if (!passwordCorrect)
            {
                Message = "Неверный пароль";
                return;
            }

            if (!user.IsEmailConfirmed)
            {
                Message = "Почта пользователя не подтверждена";
                return;
            }

            _currentUserService.SetUser(user);

            Message = string.Empty;

            AuthSuccess?.Invoke();
        }

        //Отправить код подтверждения
        [RelayCommand]
        private async Task SendCodeAsync()
        {
            Message = string.Empty;

            string currentEmail = Email.Trim();

            if (string.IsNullOrWhiteSpace(currentEmail))
            {
                Message = "Введите почту";
                return;
            }

            if (!IsValidEmail(currentEmail))
            {
                Message = "Некорректный адрес электронной почты";
                return;
            }

            User? existingUser = await _userRepository.GetByEmailAsync(currentEmail);

            if (existingUser != null)
            {
                Message = "Пользователь с такой почтой уже существует";
                return;
            }

            try
            {
                await _emailService.SendVerificationCodeAsync(currentEmail);

                Email = currentEmail;
                Code = string.Empty;

                CodeSent = true;
                EmailVerified = false;

                _verifiedEmail = null;

                Message = "Код отправлен на почту";
            }
            catch
            {
                Message = "Не удалось отправить код на почту";
            }
        }

        //Проверить код
        [RelayCommand]
        private void VerifyCode()
        {
            Message = string.Empty;

            if (string.IsNullOrWhiteSpace(Code))
            {
                Message = "Введите код из письма";
                return;
            }

            string currentEmail = Email.Trim();

            bool result = _emailService.VerifyCode(currentEmail, Code.Trim());

            if (!result)
            {
                Message = "Неверный код или срок его действия истёк";
                return;
            }

            _verifiedEmail = currentEmail;

            EmailVerified = true;
            CodeSent = false;

            Message = "Почта подтверждена";
        }

        //Регистрация
        [RelayCommand]
        private async Task RegisterAsync()
        {
            Message = string.Empty;

            if (!EmailVerified || _verifiedEmail == null)
            {
                Message = "Сначала подтвердите почту";
                return;
            }

            if (Email.Trim() != _verifiedEmail)
            {
                EmailVerified = false;
                _verifiedEmail = null;

                Message = "Почта была изменена. Подтвердите её заново";
                return;
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                Message = "Введите логин";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                Message = "Введите пароль";
                return;
            }

            if (Password.Length < 6)
            {
                Message = "Пароль должен содержать минимум 6 символов";
                return;
            }

            User? existingUsername = await _userRepository.GetByUsernameAsync(Username.Trim());

            if (existingUsername != null)
            {
                Message = "Такой логин уже занят";
                return;
            }

            User? existingEmail = await _userRepository.GetByEmailAsync(_verifiedEmail);

            if (existingEmail != null)
            {
                Message = "Пользователь с такой почтой уже существует";
                return;
            }

            string passwordHash = _passwordService.HashPassword(Password);

            User user = new User
            {
                Email = _verifiedEmail,
                Username = Username.Trim(),
                PasswordHash = passwordHash,
                IsEmailConfirmed = true
            };

            try
            {
                await _userRepository.AddAsync(user);

                await _userService.CreateStarterPetsAsync(user);

                _currentUserService.SetUser(user);

                Message = string.Empty;

                AuthSuccess?.Invoke();
            }
            catch
            {
                Message = "Не удалось завершить регистрацию";
            }
        }

        //Проверка формата почты
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
