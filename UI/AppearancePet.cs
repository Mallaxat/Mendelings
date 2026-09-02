using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.UI
{
    public partial class AppearancePet

    {
        public Bitmap? BodyImage { get; set; }
        public Bitmap? HeadImage { get; set; }
        public Bitmap? TailImage { get; set; }
        public Bitmap? EyesImage { get; set; }
        public Bitmap? EyesCloseImage { get; set; }
        public Bitmap? EarsLeftImage { get; set; }
        public Bitmap? EarsRightImage { get; set; }
        public Bitmap? HornsImage { get; set; }
    }
}
