using CommunityToolkit.Mvvm.ComponentModel;
using Mendelings.Core;
using Mendelings.Data;
using Mendelings.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.ViewModels
{
    public partial class PetUnlockCollectionModel:ObservableObject
    {
        private readonly PetRepository _petRepository;
        private readonly AppearancePetService _appearancePetService;

        const int OPTIONS = 64;
        [ObservableProperty]
        private ObservableCollection<PetUnlockCollection>? petCollection = new();

        public PetUnlockCollectionModel(PetRepository petRepository, AppearancePetService appearancePetService)
        {
            _appearancePetService = appearancePetService;
            _petRepository = petRepository;
        }


        public void GetOption()
        {
            PetCollection.Clear();
            for(int i = 0;i< OPTIONS; i++)
            {
                PetUnlockCollection option = new PetUnlockCollection
                {
                    BodyDominant = (i & (1 << 0)) != 0,
                    HeadDominant = (i & (1 << 1)) != 0,
                    EarsDominant = (i & (1 << 2)) != 0,
                    EyesDominant = (i & (1 << 3)) != 0,
                    HornsDominant = (i & (1 << 4)) != 0,
                    TailDominant = (i & (1 << 5)) != 0,

                    IsUnlocked = false,
                    AppearancePet = null
                };

                PetCollection.Add(option);

            }
        }
        public async Task CheckUnlockedAsync()
        {
            List<Pet> pets = await _petRepository.GetAllAsync();

            foreach (PetUnlockCollection option in PetCollection)
            {
                Pet? foundPet = pets.FirstOrDefault(pet =>
                    IsDominant(pet.BodyGene) == option.BodyDominant &&
                    IsDominant(pet.HeadGene) == option.HeadDominant &&
                    IsDominant(pet.EarsGene) == option.EarsDominant &&
                    IsDominant(pet.EyesGene) == option.EyesDominant &&
                    IsDominant(pet.HornsGene) == option.HornsDominant &&
                    IsDominant(pet.TailGene) == option.TailDominant);

                if (foundPet != null)
                {
                    option.IsUnlocked = true;
                    option.AppearancePet =
                        await _appearancePetService.LoadAppearancePet(foundPet);
                }
                else
                {
                    option.IsUnlocked = false;
                    option.AppearancePet = null;
                }
            }
        }

        private bool IsDominant(string genotype)
        {
            return genotype.Any(char.IsUpper);
        }

    }
}
