using Avalonia.Controls;
using Avalonia.Interactivity;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mendelings.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var page = new PetModelPage
        {
            DataContext = App.Services.GetRequiredService<PetViewModel>()
        };

        PageContent.Content = page;

    }
}