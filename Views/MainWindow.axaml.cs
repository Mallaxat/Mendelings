using Avalonia.Controls;
using Avalonia.Interactivity;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mendelings.Views;

public partial class MainWindow : Window
{
    PetModelPage _PetModelPage;
    BreedingPage _BreedingPage;
    FamilyTreePage _FamilyTreePage;
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
        _FamilyTreePage = new FamilyTreePage
        {
            DataContext = App.Services.GetRequiredService<FamilyTreeModel>()
        };
        PageContent.Content = _PetModelPage;
        MainButton.IsEnabled = false;
        FamilyButton.IsEnabled = true;
        BreedingButton.IsEnabled = true;
    }

    private async void BreedingButton_Click(object? sender, RoutedEventArgs e)
    {
        PageContent.Content = _BreedingPage;

        MainButton.IsEnabled = true;
        BreedingButton.IsEnabled = false;
        FamilyButton.IsEnabled = true;
    }

    private void MainButton_Click(object? sender, RoutedEventArgs e)
    {
        PageContent.Content = _PetModelPage;
        MainButton.IsEnabled = false;
        BreedingButton.IsEnabled = true;
        FamilyButton.IsEnabled = true;
    }

    private void FamilyButton_Click(object? sender, RoutedEventArgs e)
    {
        PageContent.Content = _FamilyTreePage;
        MainButton.IsEnabled = true;
        BreedingButton.IsEnabled = true;
        FamilyButton.IsEnabled = false;

    }
}