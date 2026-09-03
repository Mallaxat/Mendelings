using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.Core;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mendelings.Views
{
    public partial class FamilyTreePage : UserControl
    {

        public FamilyTreePage()
        {
            InitializeComponent();
        }
        public FamilyTreePage(FamilyTreeModel _model) : this()
        {
            DataContext = _model;
        }

        private async void SelectButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SelectButton.IsEnabled = false;
            Window? mainWindow = TopLevel.GetTopLevel(this) as Window;
            PetSelectionPage page = App.Services.GetRequiredService<PetSelectionPage>();
            if (page.DataContext is PetSelectionViewModel viewModel)
            {
                await viewModel.LoadItemsPetsAsync();
            }

            if (mainWindow == null) return;
            Pet? resultSelect = await page.ShowDialog<Pet?>(mainWindow);

            if (resultSelect != null && DataContext is FamilyTreeModel MyViewModel)
            {
                if (MyViewModel.CurrentPet == null) MyViewModel.CurrentPet = new();
                MyViewModel.CurrentPet.ItemPets = resultSelect;
                await MyViewModel.LoadPets();

            }
            SelectButton.IsEnabled = true;
        }
    }
}