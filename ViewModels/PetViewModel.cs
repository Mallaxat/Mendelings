using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
using Mendelings.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Mendelings.ViewModels
{
    public partial class PetViewModel : ViewModelBase
    {
        private readonly PetRepository _petRepository;
        private readonly PetService _petService;

        [ObservableProperty]
        private Pet? currentPet;

        [ObservableProperty]
        private int age;

        public PetViewModel(PetRepository petRepository,PetService petService)
        {
            _petRepository = petRepository;
            _petService = petService;
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

            await _petRepository.UpdateAsync(CurrentPet);
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
