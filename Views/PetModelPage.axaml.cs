using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.ViewModels;
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
        }

        private void SleepButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            HealButton.IsEnabled = false;
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
        private async void WakeUpButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            HealButton.IsEnabled = true;
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
    }
}