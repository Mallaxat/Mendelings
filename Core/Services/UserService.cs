using Mendelings.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Core.Services
{
    public class UserService
    {
        private readonly PetRepository _petRepository;
        private readonly GeneticsRepository _geneticsRepository;

        public UserService(
            PetRepository petRepository,
            GeneticsRepository geneticsRepository)
        {
            _petRepository = petRepository;
            _geneticsRepository = geneticsRepository;
        }
        public async Task CreateStarterPetsAsync(User user)
        {
            List<GeneticTrait> traits =
                await _geneticsRepository.GetAllAsync();

            GeneticTrait body = traits.First(x => x.Code == "BODY");
            GeneticTrait head = traits.First(x => x.Code == "HEAD");
            GeneticTrait tail = traits.First(x => x.Code == "TAIL");
            GeneticTrait eyes = traits.First(x => x.Code == "EYES");
            GeneticTrait ears = traits.First(x => x.Code == "EARS");
            GeneticTrait horns = traits.First(x => x.Code == "HORNS");

            Pet female = new PetBuilder()
                .SetUser(user.Id)
                .SetName("Luna")
                .SetSex(PetSex.Female)
                .SetGeneration(0)
                .SetState(100, 100, 100, 100)
                .SetDates(DateTime.Now, DateTime.Now)
                .SetBodyGene($"{body.DominantAllele}{body.DominantAllele}")
                .SetHeadGene($"{head.DominantAllele}{head.DominantAllele}")
                .SetTailGene($"{tail.DominantAllele}{tail.DominantAllele}")
                .SetEyesGene($"{eyes.DominantAllele}{eyes.DominantAllele}")
                .SetEarsGene($"{ears.DominantAllele}{ears.DominantAllele}")
                .SetHornsGene($"{horns.DominantAllele}{horns.DominantAllele}")
                .Build();


            Pet male = new PetBuilder()
                .SetUser(user.Id)
                .SetName("Max")
                .SetSex(PetSex.Male)
                .SetGeneration(0)
                .SetState(100, 100, 100, 100)
                .SetDates(DateTime.Now, DateTime.Now)
                .SetBodyGene($"{body.RecessiveAllele}{body.RecessiveAllele}")
                .SetHeadGene($"{head.RecessiveAllele}{head.RecessiveAllele}")
                .SetTailGene($"{tail.RecessiveAllele}{tail.RecessiveAllele}")
                .SetEyesGene($"{eyes.RecessiveAllele}{eyes.RecessiveAllele}")
                .SetEarsGene($"{ears.RecessiveAllele}{ears.RecessiveAllele}")
                .SetHornsGene($"{horns.RecessiveAllele}{horns.RecessiveAllele}")
                .Build();


            await _petRepository.AddAsync(female);
            await _petRepository.AddAsync(male);
        }

    }
}
