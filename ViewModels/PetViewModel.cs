using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
using Mendelings.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Mendelings.ViewModels
{
    public partial class PetViewModel : ViewModelBase
    {
        private readonly PetRepository _petRepository;
        private readonly PetService _petService;
        private readonly GeneticsRepository _geneticsRepository;
        private readonly GeneticsService _geneticsService;


        [ObservableProperty]
        private Pet? currentPet;

        [ObservableProperty]
        private int age;

        //Ассетная часть
        //путь
        [ObservableProperty]
        private string bodyAssetPath = string.Empty;
        //сама картинка
        [ObservableProperty]
        private Bitmap? bodyImage;

        [ObservableProperty]
        private string headAssetPath = string.Empty;
        [ObservableProperty]
        private Bitmap? headImage;
        
        [ObservableProperty]
        private string tailAssetPath = string.Empty;
        [ObservableProperty]
        private Bitmap? tailImage;

        [ObservableProperty]
        private string eyesAssetPath = string.Empty;
        [ObservableProperty]
        private Bitmap? eyesImage;

        private string eyesClosePath = "avares://Mendelings/Assets/Eyes/EyesClose.png";
        [ObservableProperty]
        private Bitmap? eyesCloseImage;

        [ObservableProperty]
        private string earsAssetPath = string.Empty;

        [ObservableProperty]
        private string earsLeftAssetPath;
        [ObservableProperty]
        private Bitmap? earsLeftImage;

        [ObservableProperty]
        private string earsRightAssetPath;
        [ObservableProperty]
        private Bitmap? earsRightImage;

        [ObservableProperty]
        private string hornsAssetPath = string.Empty;
        [ObservableProperty]
        private Bitmap? hornsImage;

        public PetViewModel(PetRepository petRepository,PetService petService, 
            GeneticsRepository geneticRepository, GeneticsService geneticService)
        {
            _petRepository = petRepository;
            _petService = petService;
            _geneticsRepository = geneticRepository;
            _geneticsService = geneticService;
        }
        //загружаем пета и обновляем
        [RelayCommand]
        public async Task LoadPetAsync()
        {
            int id = 2;
            CurrentPet = await _petRepository.GetByIdAsync(id);

            if (CurrentPet == null) return;

            _petService.UpdateState(CurrentPet);
            Age=_petService.GetAge(CurrentPet);

            await LoadGenetic();
            EarsLeftAssetPath = EarsAssetPath;
            EarsRightAssetPath = EarsAssetPath;

            BodyImage = LoadImage(BodyAssetPath);
            EyesCloseImage = LoadImage(eyesClosePath);
            HeadImage = LoadImage(HeadAssetPath);
            TailImage = LoadImage(TailAssetPath);
            EyesImage = LoadImage(EyesAssetPath);
            EarsLeftImage = LoadImage(EarsLeftAssetPath.Replace(".png", "Left.png"));
            EarsRightImage = LoadImage(EarsRightAssetPath.Replace(".png", "Right.png"));
            HornsImage = LoadImage(HornsAssetPath);

            await _petRepository.UpdateAsync(CurrentPet);
        }
        //Метод для преобразования строки в изображение
        public Bitmap LoadImage(string path)
        {
            if (String.IsNullOrEmpty(path)) 
                return null;
            Uri pathUri = new Uri(path);
            using Stream stream = AssetLoader.Open(pathUri);
            Bitmap bmp = new Bitmap(stream);
            return bmp;
        }
        //Метод для подгрузки генетики
        public async Task LoadGenetic()
        {
            GeneticTrait? bodyTrait = await _geneticsRepository.GetByCodeAsync("BODY");
            GeneticTrait? headTrait = await _geneticsRepository.GetByCodeAsync("HEAD");
            GeneticTrait? tailTrait = await _geneticsRepository.GetByCodeAsync("TAIL");
            GeneticTrait? eyesTrait = await _geneticsRepository.GetByCodeAsync("EYES");
            GeneticTrait? earsTrait = await _geneticsRepository.GetByCodeAsync("EARS");
            GeneticTrait? hornsTrait = await _geneticsRepository.GetByCodeAsync("HORNS");

            if (bodyTrait != null)
                BodyAssetPath = _geneticsService.GetAssetPath(bodyTrait, CurrentPet.BodyGene);
            if (headTrait != null)
                HeadAssetPath = _geneticsService.GetAssetPath(headTrait, CurrentPet.HeadGene);
            if (tailTrait != null)
                TailAssetPath = _geneticsService.GetAssetPath(tailTrait, CurrentPet.TailGene);
            if (eyesTrait != null)
                EyesAssetPath = _geneticsService.GetAssetPath(eyesTrait, CurrentPet.EyesGene);
            if (earsTrait != null)
                EarsAssetPath = _geneticsService.GetAssetPath(earsTrait, CurrentPet.EarsGene);
            if (hornsTrait != null)
                HornsAssetPath = _geneticsService.GetAssetPath(hornsTrait, CurrentPet.HornsGene);
        }



            //обновление текущего питомца
            [RelayCommand]
        public async Task RefreshStateAsync()
        {
            if(CurrentPet == null) return;
            _petService.UpdateState(CurrentPet);

            Age = _petService.GetAge(CurrentPet);

            await _petRepository.UpdateAsync(CurrentPet);
        }
        [RelayCommand]
        public async Task FeedAsync()
        {
            if(CurrentPet == null) return;
            _petService.Feed(CurrentPet);
            await _petRepository.UpdateAsync(CurrentPet);

        }
        [RelayCommand]
        public async Task PlayAsync()
        {
            if (CurrentPet == null) return;
            _petService.Play(CurrentPet);
            await _petRepository.UpdateAsync(CurrentPet);
        }
        [RelayCommand]
        public async Task StartSleepAsync()
        {
            if (CurrentPet == null) return;
            _petService.StartSleep(CurrentPet);
            await _petRepository.UpdateAsync(CurrentPet);
        }
        [RelayCommand]
        public async Task WakeUpAsync()
        {
            if (CurrentPet == null) return;
            _petService.WakeUp(CurrentPet);
            await _petRepository.UpdateAsync(CurrentPet);
        }
        [RelayCommand]
        public async Task HealAsync()
        {
            if (CurrentPet == null) return;
            _petService.Heal(CurrentPet);
            await _petRepository.UpdateAsync(CurrentPet);
        }
    }
}
