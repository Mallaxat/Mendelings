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
        private async Task SendPetButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window? mainWindow= TopLevel.GetTopLevel(this) as Window;
            //Создаем окно
            PetSelectionPage page = App.Services.GetRequiredService<PetSelectionPage>();
            if (page.DataContext is PetSelectionViewModel viewModel)
            {
                await viewModel.Load
            }
            await page.ShowDialog<Pet>(mainWindow);



        }
    }
}