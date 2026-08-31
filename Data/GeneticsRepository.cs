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
