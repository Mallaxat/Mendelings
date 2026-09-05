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
    PetUnlockCollectionPage _PetUnlockCollectionPage;
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
        _PetUnlockCollectionPage = new PetUnlockCollectionPage
        {
            DataContext = App.Services.GetRequiredService<PetUnlockCollectionModel>()
        };
        PageContent.Content = _PetModelPage;
        MainButton.IsEnabled = false;
        FamilyButton.IsEnabled = true;
        BreedingButton.IsEnabled = true;
        CollectionButton.IsEnabled = true;
    }

    private async void BreedingButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_PetUnlockCollectionPage.DataContext is PetUnlockCollectionModel viewModel)
        {
            await viewModel.LoadCollectionAsync();
        }
        PageContent.Content = _BreedingPage;

        MainButton.IsEnabled = true;
        BreedingButton.IsEnabled = false;
        FamilyButton.IsEnabled = true;
        CollectionButton.IsEnabled = true;
    }

    private void MainButton_Click(object? sender, RoutedEventArgs e)
    {
        PageContent.Content = _PetModelPage;
        MainButton.IsEnabled = false;
        BreedingButton.IsEnabled = true;
        FamilyButton.IsEnabled = true;
        CollectionButton.IsEnabled = true;
    }

    private void FamilyButton_Click(object? sender, RoutedEventArgs e)
    {
        PageContent.Content = _FamilyTreePage;
        MainButton.IsEnabled = true;
        BreedingButton.IsEnabled = true;
        FamilyButton.IsEnabled = false;
        CollectionButton.IsEnabled = true;

    }

    private async void CollectionButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_PetUnlockCollectionPage.DataContext is PetUnlockCollectionModel viewModel)
        {
            await viewModel.LoadCollectionAsync();
        }

        PageContent.Content = _PetUnlockCollectionPage;
        CollectionButton.IsEnabled = false;
        MainButton.IsEnabled = true;
        BreedingButton.IsEnabled = true;
        FamilyButton.IsEnabled = true;
    }
}