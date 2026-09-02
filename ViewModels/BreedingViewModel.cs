using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
using Mendelings.Data;
using Mendelings.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.ViewModels
{
    public partial class BreedingViewModel : ViewModelBase
    {
        //Ассетная часть
        private string eyesClosePath = "avares://Mendelings/Assets/Eyes/EyesClose.png";
        [ObservableProperty]
        private Bitmap? eyesCloseImage;

        [ObservableProperty]
        private Pet? currentFemalePet;
        [ObservableProperty]
        private Pet? currentMalePet;

        //Средства для загрузок
        private readonly PetRepository _petRepository;
        private readonly PetService _petService;
        private readonly GeneticsRepository _geneticsRepository;
        private readonly GeneticsService _geneticsService;
        private readonly AppearancePetService _appearancePetService;
       
        [ObservableProperty]
        private AppearancePet? appearanceFemale=new AppearancePet();
        
        [ObservableProperty]
        private AppearancePet? appearanceMale = new AppearancePet();

        public BreedingViewModel(PetRepository petRepository, PetService petService,
            GeneticsRepository geneticsRepository, GeneticsService geneticsService, AppearancePetService appearancePetService)
        {
            _geneticsRepository = geneticsRepository;
            _geneticsService = geneticsService;
            _petRepository = petRepository;
            _petService = petService;
            _appearancePetService = appearancePetService;
        }
        [RelayCommand]
        public async Task LoadPetAsync()
        {
            int FemaleId = 2;
            int MaleId = 1;
            CurrentFemalePet = await _petRepository.GetByIdAsync(FemaleId);
            CurrentMalePet = await _petRepository.GetByIdAsync(MaleId);
            
            if (CurrentFemalePet == null || CurrentMalePet == null)
                return;
            
            AppearanceFemale = await _appearancePetService.LoadAppearancePet(CurrentFemalePet);
            AppearanceMale = await _appearancePetService.LoadAppearancePet(CurrentMalePet);

            await _petRepository.UpdateAsync(CurrentFemalePet);
            await _petRepository.UpdateAsync(CurrentMalePet);
        }

    }
}
