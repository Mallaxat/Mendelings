using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.ViewModels;

namespace Mendelings.Views
{
    public partial class PetModelWindow : Window
    {

        public PetModelWindow()
        {
            InitializeComponent();
          
        }
        public PetModelWindow(PetViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}