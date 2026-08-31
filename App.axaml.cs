using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Mendelings.Core;
using Mendelings.Data;
using Mendelings.ViewModels;
using Mendelings.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mendelings;

public partial class App : Application
{
    public static ServiceProvider Services { get; private set; } = null!;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    //метод Avalonia, который вызывается после того, все уже  инициализировано
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
        services.AddDbContext<MendelingsDbContext>(options =>options.UseSqlServer(connectionString));
        
        services.AddTransient<PetRepository>();
        services.AddTransient<GeneticsRepository>();

        services.AddTransient<PetService>();
        services.AddTransient<GeneticsService>();

        services.AddTransient<MainViewModel>();
        // На основе зарегистрированных зависимостей
        // создаём DI-контейнер
        Services = services.BuildServiceProvider();

        // Создаём главное окно Avalonia
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                //получить уже зарегестрированное окно
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

 
}