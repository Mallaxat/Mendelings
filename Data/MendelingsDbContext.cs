using Mendelings.Core;
using Mendelings.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Data
{
    public class MendelingsDbContext :DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Pet> Pets {get; set;}
        public DbSet<GeneticTrait> GeneticTraits { get; set; }

        public MendelingsDbContext(DbContextOptions<MendelingsDbContext> options)
            : base(options)
        {
        }
        //Настраиваем конфигурацию создания наших объектов через переназначение метода
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new GeneticTraitConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
