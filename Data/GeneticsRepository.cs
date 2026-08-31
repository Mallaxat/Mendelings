using Mendelings.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;


namespace Mendelings.Data
{
    //Вся работа с таблицами признаков
    public class GeneticsRepository
    {
        //свойства и переменные
        private readonly MendelingsDbContext _context;

        //конструктор
        public GeneticsRepository(MendelingsDbContext context)
        {
            _context = context;
        }
        //методы
        //Метод, который заполнит бд если она пустая
        public async Task InitializeDefaultTraitsAsync()
        {
            bool hasTraits = await _context.GeneticTraits.AnyAsync();

            if (hasTraits)return;

            List<GeneticTrait> defaultTraits = new()
            {
                new GeneticTrait
                {
                    Name = "Тело",
                    Code = "BODY",
                    DominantAllele = 'B',
                    RecessiveAllele = 'b',
                    DominantPhenotype = "Крупное тело",
                    RecessivePhenotype = "Маленькое тело",
                    DominantAssetPath = "Assets/Body/body_big.png",
                    RecessiveAssetPath = "Assets/Body/body_small.png"
                },

                new GeneticTrait
                {
                    Name = "Голова",
                    Code = "HEAD",
                    DominantAllele = 'H',
                    RecessiveAllele = 'h',
                    DominantPhenotype = "Круглая голова",
                    RecessivePhenotype = "Вытянутая голова",
                    DominantAssetPath = "Assets/Head/head_round.png",
                    RecessiveAssetPath = "Assets/Head/head_long.png"
                },

                new GeneticTrait
                {
                    Name = "Хвост",
                    Code = "TAIL",
                    DominantAllele = 'T',
                    RecessiveAllele = 't',
                    DominantPhenotype = "Пушистый хвост",
                    RecessivePhenotype = "Гладкий хвост",
                    DominantAssetPath = "Assets/Tail/tail_fluffy.png",
                    RecessiveAssetPath = "Assets/Tail/tail_smooth.png"
                },

                new GeneticTrait
                {
                    Name = "Глаза",
                    Code = "EYES",
                    DominantAllele = 'E',
                    RecessiveAllele = 'e',
                    DominantPhenotype = "Красные глаза",
                    RecessivePhenotype = "Голубые глаза",
                    DominantAssetPath = "Assets/Eyes/eyes_red.png",
                    RecessiveAssetPath = "Assets/Eyes/eyes_blue.png"
                },

                new GeneticTrait
                {
                    Name = "Уши",
                    Code = "EARS",
                    DominantAllele = 'A',
                    RecessiveAllele = 'a',
                    DominantPhenotype = "Длинные уши",
                    RecessivePhenotype = "Короткие уши",
                    DominantAssetPath = "Assets/Ears/ears_long.png",
                    RecessiveAssetPath = "Assets/Ears/ears_short.png"
                },

                new GeneticTrait
                {
                    Name = "Рога",
                    Code = "HORNS",
                    DominantAllele = 'G',
                    RecessiveAllele = 'g',
                    DominantPhenotype = "Рога есть",
                    RecessivePhenotype = "Рогов нет",
                    DominantAssetPath = "Assets/Horns/horns.png",
                    RecessiveAssetPath = string.Empty
                }
            };

            await _context.GeneticTraits.AddRangeAsync(defaultTraits);
            await _context.SaveChangesAsync();
        }
        public async Task<List<GeneticTrait>> GetAllAsync()
        {
            List<GeneticTrait> result = await _context.GeneticTraits.ToListAsync();
            return result;
        }

        public async Task<GeneticTrait?> GetByCodeAsync(string code)
        {
            GeneticTrait? result = await _context.GeneticTraits.FirstOrDefaultAsync(x=>x.Code==code);
            return  result;
        }

        public async Task AddAsync(GeneticTrait trait)
        {
            await _context.GeneticTraits.AddAsync(trait);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(GeneticTrait trait)
        {
            _context.GeneticTraits.Update(trait);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            await _context.GeneticTraits.Where(x => x.Id == id).ExecuteDeleteAsync();
        
        }
        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _context.GeneticTraits.AnyAsync(x=>x.Code == code);
        }

    }
}
