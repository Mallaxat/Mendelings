using CommunityToolkit.Mvvm.ComponentModel;
using Mendelings.Core;
using Mendelings.Data;
using Mendelings.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.ViewModels
{
   public partial class FamilyTreeModel:ViewModelBase
    {
        private PetRepository _petRepository;
        private AppearancePetService _appearancePetService;

        public FamilyTreeModel(PetRepository petRepository, AppearancePetService appearancePetService)
        {
            _petRepository = petRepository;
            _appearancePetService = appearancePetService;
        }

        [ObservableProperty]
        private PetSelectionItem? currentPet=new();

        [ObservableProperty]
        private PetSelectionItem? motherPet = new();

        [ObservableProperty]
        private PetSelectionItem? fatherPet =new ();

        [ObservableProperty]
        private List<PetSelectionItem>? childrenPet = new();

        public async Task LoadPets()
        {
            if (CurrentPet?.ItemPets == null) return;

            int currentPetId = CurrentPet.ItemPets.Id;

            //Загружаем текущего питомца
            Pet? loadedPet = await _petRepository.GetByIdAsync(currentPetId);

            if (loadedPet == null) return;

            //Загружаем родителей и детей
            List<Pet> parents = await _petRepository.GetParentsAsync(currentPetId);

            List<Pet> children =  await _petRepository.GetChildrenAsync(currentPetId);

            CurrentPet = new PetSelectionItem
            {
                ItemPets = loadedPet,
                ItemAppearancePets = await _appearancePetService.LoadAppearancePet(loadedPet)
            };

            Pet? mother = parents.FirstOrDefault(x => x.Sex == PetSex.Female);
            if (mother != null)
            {
                MotherPet = new PetSelectionItem
                {
                    ItemPets = mother,
                    ItemAppearancePets = await _appearancePetService.LoadAppearancePet(mother)
                };
            }
            else
                MotherPet = new PetSelectionItem();
            

            Pet? father = parents.FirstOrDefault(x => x.Sex == PetSex.Male);

            if (father != null)
            {
                FatherPet = new PetSelectionItem
                {
                    ItemPets = father,
                    ItemAppearancePets = await _appearancePetService.LoadAppearancePet(father)
                };
            }
            else
                FatherPet = new PetSelectionItem();
            

            List<PetSelectionItem> childrenItems = new List<PetSelectionItem>();

            foreach (Pet child in children)
            {
                childrenItems.Add(new PetSelectionItem
                {
                    ItemPets = child,
                    ItemAppearancePets =await _appearancePetService.LoadAppearancePet(child)
                });
            }
            ChildrenPet = childrenItems;
        }

    }
}
