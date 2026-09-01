using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.ViewModels;

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
        }
        private void WakeUpButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            HealButton.IsEnabled = true;
            FeedButton.IsEnabled = true;
            PlayButton.IsEnabled = true;
        }
    }
}