using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.Core;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Mendelings.Views
{
    public partial class BreedingPage : UserControl
    {
        public BreedingPage()
        {
            InitializeComponent();

        }
        public BreedingPage(BreedingViewModel context) : this()
        {
            DataContext = context;
        }
        private async void SelectFemaleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window?  mainWindow = TopLevel.GetTopLevel(this) as Window;
            PetSelectionPage page = App.Services.GetRequiredService<PetSelectionPage>();
            if (page.DataContext is PetSelectionViewModel viewModel)
            {
                await viewModel.LoadItemsPetsAsync(PetSex.Female);
            }

            if (mainWindow == null) return;
            Pet? resultSelect = await page.ShowDialog<Pet?>(mainWindow);

            if (resultSelect != null && DataContext is BreedingViewModel MyViewModel)
            {
                if (MyViewModel.CurrentFemalePet != null) MyViewModel.CurrentFemalePet = null;
                MyViewModel.CurrentFemalePet = resultSelect;
                await MyViewModel.LoadPetAsync(MyViewModel.CurrentFemalePet);

            }

        }

        private async void SelectMaleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window? mainWindow = TopLevel.GetTopLevel(this) as Window;
            PetSelectionPage page = App.Services.GetRequiredService<PetSelectionPage>();
            if (page.DataContext is PetSelectionViewModel viewModel)
            {
                await viewModel.LoadItemsPetsAsync(PetSex.Male);
            }

            if (mainWindow == null) return;
            Pet? resultSelect = await page.ShowDialog<Pet?>(mainWindow);

            if (resultSelect != null && DataContext is BreedingViewModel MyViewModel)
            {
                if (MyViewModel.CurrentMalePet != null) MyViewModel.CurrentMalePet = null;
                MyViewModel.CurrentMalePet = resultSelect;
                await MyViewModel.LoadPetAsync(MyViewModel.CurrentMalePet);

            }
        }
    }
}