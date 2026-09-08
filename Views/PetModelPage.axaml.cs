using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Azure;
using Mendelings.Core;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Mendelings.Views
{
    public partial class PetModelPage : UserControl
    {
        PetViewModel? _petViewModel;
        public PetModelPage()
        {
            InitializeComponent();
        }
        public PetModelPage(PetViewModel petViewModel) : this()
        {
            _petViewModel = petViewModel;
            DataContext = petViewModel;

        }

        private async void SleepButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Sleep();
            SleepButton.IsEnabled = false;
            WakeUpButton.IsEnabled = true;
        }
        private async void WakeUpButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await WakeUp();
            SleepButton.IsEnabled = true;
            WakeUpButton.IsEnabled = false;
        }

        private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

            await LoadDate();
            await SleepState();
            if ( DataContext is PetViewModel MyViewModel)
            {
                if (MyViewModel.CurrentPet == null)
                {
                    FeedButton.IsEnabled = false;
                    PlayButton.IsEnabled = false;
                    SleepButton.IsEnabled = false;
                    WakeUpButton.IsEnabled = false;
                }
                else
                {
                    FeedButton.IsEnabled = true;
                    PlayButton.IsEnabled = true;
                    SleepButton.IsEnabled = true;
                    WakeUpButton.IsEnabled = true;
                }

            }

        }
        private async Task LoadDate()
        {

            Window? mainWindow = TopLevel.GetTopLevel(this) as Window;
            PetSelectionPage page = App.Services.GetRequiredService<PetSelectionPage>();
            if (page.DataContext is PetSelectionViewModel viewModel)
            {
                await viewModel.LoadItemsPetsAsync();
            }

            if (mainWindow == null) return;
            Pet? resultSelect = await page.ShowDialog<Pet?>(mainWindow);

            if (resultSelect != null && DataContext is PetViewModel MyViewModel)
            {
                if (MyViewModel.CurrentPet != null) MyViewModel.CurrentPet = null;
                MyViewModel.CurrentPet = resultSelect;
                await MyViewModel.LoadPetAsync(MyViewModel.CurrentPet);
            }


        }
        private async Task SleepState()
        {
            if (this.DataContext is PetViewModel viewModel)
            {
                if (viewModel.CurrentPet == null) return;
                if(viewModel.CurrentPet.IsSleeping==true)
                    await Sleep();
                else
                    await WakeUp();
            }
     
        }
        private async Task WakeUp()
        {
            FeedButton.IsEnabled = true;
            PlayButton.IsEnabled = true;

            EyesImage.Classes.Remove("AutoBlink");
            EyesImage.Classes.Remove("EyesClosed");

            EyesImage.Classes.Add("EyesOpen");

            // Ждём завершения анимации открытия
            await Task.Delay(220);
            EyesImage.Classes.Remove("EyesOpen");

            // Снова запускаем обычное моргание
            EyesImage.Classes.Add("AutoBlink");

            TailImage.Classes.Add("TailMove");
            EarsImageLeft.Classes.Add("EarLeftMove");
            EarsImageRight.Classes.Add("EarRightMove");
        }
        private async Task Sleep()
        {
            FeedButton.IsEnabled = false;
            PlayButton.IsEnabled = false;

            // Останавливаем обычное моргание
            EyesImage.Classes.Remove("AutoBlink");
            EyesImage.Classes.Remove("EyesOpen");

            // Закрываем глаза
            EyesImage.Classes.Add("EyesClosed");
            TailImage.Classes.Remove("TailMove");
            EarsImageLeft.Classes.Remove("EarLeftMove");
            EarsImageRight.Classes.Remove("EarRightMove");
        }

    }
}