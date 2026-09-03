using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.UI
{
    public partial class AppearancePet : ObservableObject

    {
        [ObservableProperty]
        private Bitmap? bodyImage;

        [ObservableProperty]
        private Bitmap? headImage;

        [ObservableProperty]
        private Bitmap? tailImage;

        [ObservableProperty]
        private Bitmap? eyesImage;

        [ObservableProperty]
        private Bitmap? eyesCloseImage;

        [ObservableProperty]
        private Bitmap? earsLeftImage;

        [ObservableProperty]
        private Bitmap? earsRightImage;

        [ObservableProperty]
        private Bitmap? hornsImage;
    }
}
