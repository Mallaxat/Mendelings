using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Mendelings.Core;
using Mendelings.Core.Email;
using Mendelings.Core.Services;
using Mendelings.Data;
using Mendelings.UI;
using Mendelings.ViewModels;
using Mendelings.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Mendelings;

public partial class App : Application
{
    public static ServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    //метод Avalonia, который вызывается после того, все уже инициализировано
    public override void OnFrameworkInitializationCompleted()
    {
        //ConfigurationBuilder() класс для настройки приложений
        var configuration = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)//искать базовый путь
                            .AddJsonFile("appsettings.json")//наши настройки джесона
                            .Build();//сконструировать

        //ключ для настройки подключения
        var connectionString = configuration.GetConnectionString("MendelingsDb");

        //коллекция сервисов технологии зависиомстей
        var services = new ServiceCollection();

        //регестрация MendelingsDbContext
        services.AddDbContext<MendelingsDbContext>(
            options => options.UseSqlServer(connectionString),
            contextLifetime: ServiceLifetime.Transient);

        //Окна который будет уметь создавать
        services.AddTransient<PetViewModel>();
        services.AddTransient<PetModelPage>();

        services.AddTransient<BreedingViewModel>();
        services.AddTransient<BreedingPage>();

        services.AddTransient<MainViewModel>();

        services.AddTransient<PetSelectionViewModel>();
        services.AddTransient<PetSelectionPage>();

        services.AddTransient<FamilyTreeModel>();
        services.AddTransient<FamilyTreePage>();

        services.AddTransient<PetUnlockCollectionPage>();
        services.AddTransient<PetUnlockCollectionModel>();
        services.AddTransient<PetUnlockCollection>();

        services.AddTransient<RegistrationViewModel>();
        services.AddTransient<RegistrationWindow>();

        // Репозитории
        services.AddTransient<UserRepository>();
        services.AddTransient<PetRepository>();
        services.AddTransient<GeneticsRepository>();

        // Сервисы
        services.AddTransient<UserService>();
        services.AddTransient<PetService>();
        services.AddTransient<GeneticsService>();
        services.AddTransient<AppearancePetService>();
        services.AddTransient<EmailService>();
        services.AddTransient<PasswordService>();

        services.AddTransient<AppearancePet>();

        // Email
        services.AddTransient<IEmailSenderStrategy, MailRuEmailStrategy>();

        // Singleton — один экземпляр на всё приложение
        services.AddSingleton<EmailVerificationService>();
        services.AddSingleton<CurrentUserService>();

        // На основе зарегистрированных зависимостей
        // создаём DI-контейнер
        Services = services.BuildServiceProvider();


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            //Получаем ViewModel окна входа и регистрации
            RegistrationViewModel registrationViewModel = Services.GetRequiredService<RegistrationViewModel>();

            //Создаем окно входа и регистрации
            RegistrationWindow registrationWindow = Services.GetRequiredService<RegistrationWindow>();

            registrationWindow.DataContext = registrationViewModel;

            //Если вход или регистрация прошли успешно
            registrationViewModel.AuthSuccess += () =>
            {
                //Создаем главное окно
                MainWindow mainWindow = new MainWindow
                {
                    DataContext = Services.GetRequiredService<MainViewModel>()
                };

                //Теперь главное окно приложения - MainWindow
                desktop.MainWindow = mainWindow;

                //Показываем MainWindow
                mainWindow.Show();

                //Закрываем окно регистрации
                registrationWindow.Close();
            };

            //При запуске приложения главным является окно регистрации
            desktop.MainWindow = registrationWindow;
        }

        base.OnFrameworkInitializationCompleted();

        if (!Design.IsDesignMode)
        {
            _ = InitializeDatabaseAsync();
        }
    }

    private async Task InitializeDatabaseAsync()
    {
        using var scope = Services.CreateScope();

        var geneticsRepository =
            scope.ServiceProvider.GetRequiredService<GeneticsRepository>();

        //При запуске создаем только стандартные генетические признаки
        await geneticsRepository.InitializeDefaultTraitsAsync();
    }
}
