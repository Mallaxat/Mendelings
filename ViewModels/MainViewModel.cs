using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;

namespace Mendelings.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private string pathMain = "avares://Mendelings/Assets/Level1/Main_Fone.png";
    private string pathMain2 = "avares://Mendelings/Assets/Level1/Main_Fone2.png";
    [ObservableProperty]
    public Bitmap? fone;


    public MainViewModel()
    {
        Fone = LoadImage(pathMain);
    }
    [RelayCommand]
    public void ChangeFone1()
    {
        Fone = LoadImage(pathMain);
    }

    [RelayCommand]
    public void ChangeFone2()
    {
        Fone = LoadImage(pathMain2);
    }

    public Bitmap? LoadImage(string path)
    {
        if (String.IsNullOrEmpty(path))
            return null;
        Uri pathUri = new Uri(path);
        using Stream stream = AssetLoader.Open(pathUri);
        Bitmap bmp = new Bitmap(stream);
        return bmp;
    }
}
