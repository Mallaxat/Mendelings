using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using Mendelings.Core;
using Mendelings.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.UI
{
    public partial class AppearancePetService

    {
        private string eyesClosePath = "avares://Mendelings/Assets/Eyes/EyesClose.png";
        private GeneticsService _geneticsService;
        private PetRepository _petRepository;
        private GeneticsRepository _geneticsRepository;

        private Pet? СurrentPet { get; set; }

        private AppearancePet? appearancePet;

        public string BodyAssetPath { get; set; } = string.Empty;
        public string HeadAssetPath { get; set; } = string.Empty;
        public string TailAssetPath { get; set; } = string.Empty;
        public string EyesAssetPath { get; set; } = string.Empty;
        public string EarsAssetPath { get; set; } = string.Empty;
        public string EarsLeftAssetPath { get; set; } = string.Empty;
        public string EarsRightAssetPath { get; set; } = string.Empty;
        public string HornsAssetPath { get; set; } = string.Empty;


        public AppearancePetService(GeneticsRepository geneticsRepository, PetRepository petRepository, 
            GeneticsService geneticsService)
        {
            _geneticsRepository = geneticsRepository;
            _geneticsService = geneticsService;
            _petRepository = petRepository;
        }

        public async Task<AppearancePet?> LoadAppearancePet(Pet pet)
        {
            СurrentPet = pet;
            await LoadGenetic();


            EarsLeftAssetPath = EarsAssetPath;
                EarsRightAssetPath = EarsAssetPath;

           appearancePet = new AppearancePet()
                {
                BodyImage = LoadImage(BodyAssetPath),
                HeadImage = LoadImage(HeadAssetPath),
                TailImage = LoadImage(TailAssetPath),
                EyesImage = LoadImage(EyesAssetPath),
                EyesCloseImage = LoadImage(eyesClosePath),
                EarsLeftImage = LoadImage(EarsLeftAssetPath.Replace(".png", "Left.png")),
                EarsRightImage = LoadImage(EarsRightAssetPath.Replace(".png", "Right.png")),
                HornsImage = LoadImage(HornsAssetPath) };
            return appearancePet;

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

        public async Task? LoadGenetic()
        {
            GeneticTrait? bodyTrait = await _geneticsRepository.GetByCodeAsync("BODY");
            GeneticTrait? headTrait = await _geneticsRepository.GetByCodeAsync("HEAD");
            GeneticTrait? tailTrait = await _geneticsRepository.GetByCodeAsync("TAIL");
            GeneticTrait? eyesTrait = await _geneticsRepository.GetByCodeAsync("EYES");
            GeneticTrait? earsTrait = await _geneticsRepository.GetByCodeAsync("EARS");
            GeneticTrait? hornsTrait = await _geneticsRepository.GetByCodeAsync("HORNS");

            if (bodyTrait != null)
                BodyAssetPath = _geneticsService.GetAssetPath(bodyTrait, СurrentPet.BodyGene);
            if (headTrait != null)
                HeadAssetPath = _geneticsService.GetAssetPath(headTrait, СurrentPet.HeadGene);
            if (tailTrait != null)
                TailAssetPath = _geneticsService.GetAssetPath(tailTrait, СurrentPet.TailGene);
            if (eyesTrait != null)
                EyesAssetPath = _geneticsService.GetAssetPath(eyesTrait, СurrentPet.EyesGene);
            if (earsTrait != null)
                EarsAssetPath = _geneticsService.GetAssetPath(earsTrait, СurrentPet.EarsGene);
            if (hornsTrait != null)
                HornsAssetPath = _geneticsService.GetAssetPath(hornsTrait, СurrentPet.HornsGene);
        }

    }
}
