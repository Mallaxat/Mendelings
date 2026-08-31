using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;

namespace Mendelings.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenPetWindow_Click(object? sender, RoutedEventArgs e)
    {
        var window = App.Services.GetRequiredService<PetModelWindow>();
        window.Show();
    }
}