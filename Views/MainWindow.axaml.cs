using Avalonia.Controls;
using Avalonia.Interactivity;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mendelings.Views;

public partial class MainWindow : Window
{
    PetModelPage _PetModelPage;
    BreedingPage _BreedingPage;
    public MainWindow()
    {
        InitializeComponent();
        _PetModelPage = new PetModelPage
        {
            DataContext = App.Services.GetRequiredService<PetViewModel>()
        };
        _BreedingPage = new BreedingPage
        {
            DataContext = App.Services.GetRequiredService<BreedingViewModel>()
        };
        PageContent.Content = _PetModelPage;
        MainButton.IsEnabled = false;
    }

    private void BreedingButton_Click(object? sender, RoutedEventArgs e)
    {
        MainButton.IsEnabled = true;
  
        PageContent.Content = _BreedingPage;
        BreedingButton.IsEnabled = false;
    }

    private void MainButton_Click(object? sender, RoutedEventArgs e)
    {
        PageContent.Content = _PetModelPage;
        BreedingButton.IsEnabled = true;
    }
}