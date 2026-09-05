using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.ViewModels;

namespace Mendelings.Views
{
    public partial class RegistrationWindow : Window
    {

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        public RegistrationWindow(RegistrationViewModel viewModel) : this()
        {
            DataContext = viewModel;

            viewModel.AuthSuccess += AuthSuccess;
        }

        private void AuthSuccess()
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            Close();
        }
    }
}
