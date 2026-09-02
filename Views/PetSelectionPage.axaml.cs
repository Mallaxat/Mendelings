using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.Core;
using Mendelings.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Mendelings.Views
{
    public partial class PetSelectionPage : Window
    {
        public PetSelectionPage()
        {
            InitializeComponent();
        }
        public PetSelectionPage(PetSelectionViewModel context) :this ()
        {
            DataContext = context;
        }

        private void SendPetButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }
}