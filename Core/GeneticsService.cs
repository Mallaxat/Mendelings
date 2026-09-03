using Mendelings.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Core
{
    public class GeneticsService
    {
        public string GetPhenotype(GeneticTrait trait, string genotype) 
        {
            if (genotype.Contains(trait.DominantAllele))
            {
                return trait.DominantPhenotype;
            }
            return trait.RecessivePhenotype;
        }
        //Какой возможный генотип у ребенка будет
        public List<string> GetPossibleGenotypes(string parent1Gene, string parent2Gene)
        {
            List<string> result = new List<string>();

            foreach (char allele1 in parent1Gene)
            {
                foreach (char allele2 in parent2Gene)
                {
                    string genotype;

                    if (char.IsUpper(allele1))
                        genotype = $"{allele1}{allele2}";
                    else
                        genotype = $"{allele2}{allele1}";

                    result.Add(genotype);
                }
            }
            return result;
        }
        public Dictionary<string, double> CalculateProbabilities(string parent1Gene,string parent2Gene)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            List<string> genes = GetPossibleGenotypes(parent1Gene, parent2Gene);

            foreach (string genotype in genes)
            {
                if (!result.ContainsKey(genotype))
                    result[genotype] = 0;

                result[genotype]++;
            }
            //подсчитываем процент и вероятность
            foreach (string genotype in result.Keys.ToList())
            {
                result[genotype] = result[genotype] / genes.Count * 100;
            }

            return result;
        }
        //Взять рандомный ген от родителя Tt -T или t
        public string GetRandomChildGene(string parent1Gene, string parent2Gene)
        {
            char allele1 = parent1Gene[Random.Shared.Next(0, 2)];
            char allele2 = parent2Gene[Random.Shared.Next(0, 2)];

            if (char.IsUpper(allele1))
                return $"{allele1}{allele2}";

            return $"{allele2}{allele1}";
        }
        //создает нового питомца на основе родительских генов
        public async Task<Pet> BreedPet(Pet mother, Pet father)
        {
            PetSex childSex = Random.Shared.Next(0, 2) == 0 ? PetSex.Male: PetSex.Female;
            string Name =await GetRandomName(childSex);
            //Определить самое большое поколение и добавить 1
            int generation = Math.Max(mother.Generation, father.Generation) + 1;

            DateTime now = DateTime.Now;

            Pet child = new PetBuilder()
                .SetSex(childSex)
                .SetParents(mother.Id, father.Id)
                .SetGeneration(generation)
                .SetState(100, 100, 100, 100)
                .SetDates(now, now)
                .SetTailGene(GetRandomChildGene(mother.TailGene, father.TailGene))
                .SetEarsGene(GetRandomChildGene(mother.EarsGene, father.EarsGene))
                .SetEyesGene(GetRandomChildGene(mother.EyesGene, father.EyesGene))
                .SetBodyGene(GetRandomChildGene(mother.BodyGene, father.BodyGene))
                .SetHeadGene(GetRandomChildGene(mother.HeadGene, father.HeadGene))
                .SetHornsGene(GetRandomChildGene(mother.HornsGene, father.HornsGene))
                .SetName(Name)
                .Build();
                

            return child;
        }
        private async Task<string> GetRandomName(PetSex childSex)
        {
            string path = FileService.FindPath(childSex);

            string result = await FileService.GetRandomStringAsync(path);

            return result;
        }
        public string GetAssetPath(GeneticTrait trait, string genotype)
        {
            if (genotype.Contains(trait.DominantAllele))
            {
                return trait.DominantAssetPath;
            }

            return trait.RecessiveAssetPath;
        }
    }
}
