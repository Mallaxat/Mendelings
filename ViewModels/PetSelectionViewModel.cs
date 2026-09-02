using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mendelings.Core;
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

        private readonly PetRepository _petRepository;
        private readonly AppearancePetService _apearancePetService;

        private List<Pet> PetsList;

        [ObservableProperty]
        private List<PetSelectionItem> petsItemsList;
        
        [ObservableProperty]
        private PetSelectionItem? selectPet;


        public PetSelectionViewModel(PetRepository petRepository, AppearancePetService apearancePetService)
        {
            _petRepository = petRepository;
            _apearancePetService = apearancePetService;
        }

        public async Task LoadItemsPetsAsync(PetSex sex)
        {

            if (PetsItemsList == null) PetsItemsList = new List<PetSelectionItem>();            
            PetsList = await _petRepository.GetAllAsync();

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





    }
}
