using Mendelings.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Data
{
   //Класс отвечает за работу с питомцами в бд
    public class PetRepository
    {
        private readonly MendelingsDbContext _context;

        public PetRepository(MendelingsDbContext context)
        {
            _context = context;
        }

        public async Task InitializeDefaultPetsAsync()
        {
            bool hasPets = await _context.Pets.AnyAsync();

            if (hasPets) return;

            DateTime now = DateTime.Now;

            Pet female = new PetBuilder()
                .SetName("Luna")
                .SetSex(PetSex.Female)
                .SetParents(null, null)
                .SetGeneration(1)
                .SetState(100, 100, 100, 100)
                .SetDates(now, now)
                .SetBodyGene("BB")
                .SetHeadGene("HH")
                .SetTailGene("TT")
                .SetEyesGene("EE")
                .SetEarsGene("AA")
                .SetHornsGene("GG")
                .Build();

            Pet male = new PetBuilder()
                .SetName("Max")
                .SetSex(PetSex.Male)
                .SetParents(null, null)
                .SetGeneration(1)
                .SetState(100, 100, 100, 100)
                .SetDates(now, now)
                .SetBodyGene("bb")
                .SetHeadGene("hh")
                .SetTailGene("tt")
                .SetEyesGene("ee")
                .SetEarsGene("aa")
                .SetHornsGene("gg")
                .Build();

            await _context.Pets.AddRangeAsync(female, male);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pet>> GetAllAsync() 
        {
            //Так тут сразу будет грузить с объектами родителей
            List<Pet> result = await _context.Pets
                            .Include(x => x.Mother)
                            .Include(x => x.Father).ToListAsync();
            return result;
        }

        public async Task<Pet?> GetByIdAsync(int id) 
        {
            return await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Pet pet) 
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            await _context.Pets.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Pet>> GetChildrenAsync(int parentId)
        {
            List<Pet> result= await _context.Pets
                .Where(p => p.FatherId == parentId || p.MotherId == parentId)
                .ToListAsync();
            return result;
        }
        public async Task<List<Pet>> GetParentsAsync(int petId) 
        {
            Pet? pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null) return new List<Pet>();

            List<Pet> parents = await _context.Pets
                .Where(p => p.Id == pet.MotherId || p.Id == pet.FatherId)
                .ToListAsync();

            return parents;

        }
        public async Task<List<Pet>> GetByGenerationAsync(int generation)
        {
            return await _context.Pets
                .Where(p => p.Generation == generation)
                .ToListAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Pets.AnyAsync(p => p.Id == id);
        }
    }
}
