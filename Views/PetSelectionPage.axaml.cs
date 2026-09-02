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
        public PetSelectionPage(PetSelectionViewModel context)
        {
            InitializeComponent();
            DataContext= context;
        }


    }
}