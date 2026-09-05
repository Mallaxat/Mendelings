using CommunityToolkit.Mvvm.ComponentModel;
using Mendelings.Core;
using Mendelings.Core.Services;
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
    public partial class PetUnlockCollectionModel : ObservableObject
    {
        private readonly PetRepository _petRepository;
        private readonly AppearancePetService _appearancePetService;
        private readonly CurrentUserService _currentUserService;

        const int OPTIONS = 64;

        [ObservableProperty]
        private ObservableCollection<PetUnlockCollection> petCollection = new();

        public PetUnlockCollectionModel(PetRepository petRepository, AppearancePetService appearancePetService, CurrentUserService currentUserService)
        {
            _petRepository = petRepository;
            _appearancePetService = appearancePetService;
            _currentUserService = currentUserService;
        }

        public async Task LoadCollectionAsync()
        {
            if (_currentUserService.UserId == null)
                return;

            int userId = _currentUserService.UserId.Value;

            //Каждый раз получаем свежих питомцев пользователя из БД
            List<Pet> pets = await _petRepository.GetByUserIdAsync(userId);

            PetCollection.Clear();

            for (int i = 0; i < OPTIONS; i++)
            {
                bool bodyDominant = (i & (1 << 0)) != 0;
                bool headDominant = (i & (1 << 1)) != 0;
                bool earsDominant = (i & (1 << 2)) != 0;
                bool eyesDominant = (i & (1 << 3)) != 0;
                bool hornsDominant = (i & (1 << 4)) != 0;
                bool tailDominant = (i & (1 << 5)) != 0;

                Pet? foundPet = pets.FirstOrDefault(pet =>
                    IsDominant(pet.BodyGene) == bodyDominant &&
                    IsDominant(pet.HeadGene) == headDominant &&
                    IsDominant(pet.EarsGene) == earsDominant &&
                    IsDominant(pet.EyesGene) == eyesDominant &&
                    IsDominant(pet.HornsGene) == hornsDominant &&
                    IsDominant(pet.TailGene) == tailDominant);

                PetUnlockCollection option = new PetUnlockCollection
                {
                    BodyDominant = bodyDominant,
                    HeadDominant = headDominant,
                    EarsDominant = earsDominant,
                    EyesDominant = eyesDominant,
                    HornsDominant = hornsDominant,
                    TailDominant = tailDominant,

                    IsUnlocked = foundPet != null,
                    AppearancePet = foundPet != null
                        ? await _appearancePetService.LoadAppearancePet(foundPet)
                        : null
                };

                PetCollection.Add(option);
            }
        }

        private bool IsDominant(string genotype)
        {
            return genotype.Any(char.IsUpper);
        }
    }
}
