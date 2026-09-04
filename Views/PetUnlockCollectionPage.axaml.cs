using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mendelings.ViewModels;

namespace Mendelings.Views
{
    public partial class PetUnlockCollectionPage : UserControl
    {
        public PetUnlockCollectionPage()
        {
            InitializeComponent();
        }
        public PetUnlockCollectionPage(PetUnlockCollectionModel model) :this() 
        {
           DataContext = model;
        }
    }
}