using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
using Mendelings.Core.Services;
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
        [ObservableProperty]
        private Bitmap? eyesCloseImage;
        [ObservableProperty]
        private Pet? currentFemalePet;
        [ObservableProperty]
        private Pet? currentMalePet;
        [ObservableProperty]
        private Pet? childPet;

        //Средства для загрузок
        private readonly PetRepository _petRepository;
        private readonly PetService _petService;
        private readonly GeneticsRepository _geneticsRepository;
        private readonly GeneticsService _geneticsService;
        private readonly AppearancePetService _appearancePetService;
        private readonly CurrentUserService _currentUserService;

        [ObservableProperty]
        private AppearancePet? appearanceFemale=new AppearancePet();  
        [ObservableProperty]
        private AppearancePet? appearanceMale = new AppearancePet();
        [ObservableProperty]
        private AppearancePet? appearanceChild = new AppearancePet();



        public BreedingViewModel(PetRepository petRepository, PetService petService,
            GeneticsRepository geneticsRepository, GeneticsService geneticsService, 
            AppearancePetService appearancePetService, CurrentUserService currentUserService)
        {
            _geneticsRepository = geneticsRepository;
            _geneticsService = geneticsService;
            _petRepository = petRepository;
            _petService = petService;
            _appearancePetService = appearancePetService;
            _currentUserService= currentUserService;
        }
        //Загрузка пета(ов)
        [RelayCommand]
        public async Task LoadPetAsync(Pet CurrentPet)
        {
            CurrentPet = await _petRepository.GetByIdAsync(CurrentPet.Id);
           
            if (CurrentPet == null )
                return;
            if(CurrentPet.Sex==PetSex.Female)
                AppearanceFemale = await _appearancePetService.LoadAppearancePet(CurrentPet);
            else
                AppearanceMale = await _appearancePetService.LoadAppearancePet(CurrentPet);

        }

        [RelayCommand]
        public async Task BreedAsync()
        {
            if(CurrentFemalePet == null || CurrentMalePet==null) return;

            ChildPet = await _geneticsService.BreedPet(CurrentFemalePet, CurrentMalePet);

            ChildPet.UserId = _currentUserService.UserId.Value;

            await _petRepository.AddAsync(ChildPet);
            AppearanceChild = await _appearancePetService.LoadAppearancePet(ChildPet);

        }


    }
}
