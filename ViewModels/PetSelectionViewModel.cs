using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
using Mendelings.Core.Services;
using Mendelings.Data;
using Mendelings.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.ViewModels
{
    public partial class PetSelectionViewModel : ViewModelBase
    {

        private readonly PetRepository? _petRepository;
        private readonly AppearancePetService? _apearancePetService;
        private readonly CurrentUserService _currentUserService;


        private List<Pet> PetsList;

        [ObservableProperty]
        private List<PetSelectionItem> petsItemsList;
        
        [ObservableProperty]
        private PetSelectionItem? selectPet;


        public PetSelectionViewModel(PetRepository petRepository, AppearancePetService apearancePetService, CurrentUserService currentUserService)
        {
            _petRepository = petRepository;
            _apearancePetService = apearancePetService;
            _currentUserService = currentUserService;
        }

        public async Task LoadItemsPetsAsync(PetSex sex)
        {

            if (PetsItemsList == null) PetsItemsList = new List<PetSelectionItem>();
            
            int userId = _currentUserService.UserId.Value;

            PetsList = await _petRepository.GetByUserIdAsync(userId);

            PetsList = PetsList.Where(x => x.Sex == sex).ToList();

            PetsItemsList.Clear();

            foreach (Pet pet in PetsList)
            {
                PetsItemsList.Add(new PetSelectionItem
                {
                    ItemPets = pet,
                    ItemAppearancePets = await _apearancePetService.LoadAppearancePet(pet)
                });
            }
        }
        public async Task LoadItemsPetsAsync()
        {
            int userId = _currentUserService.UserId.Value;
            if (PetsItemsList == null) PetsItemsList = new List<PetSelectionItem>();
            PetsList = await _petRepository.GetByUserIdAsync(userId);

            PetsItemsList.Clear();
            foreach (Pet pet in PetsList)
            {
                PetsItemsList.Add(new PetSelectionItem
                {
                    ItemPets = pet,
                    ItemAppearancePets = await _apearancePetService.LoadAppearancePet(pet)
                });
            }
        }





    }
}
