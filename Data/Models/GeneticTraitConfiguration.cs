using Mendelings.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Data.Models
{
    //Описывает для БД признаки
    public class GeneticTraitConfiguration : IEntityTypeConfiguration<GeneticTrait>
    {
        public void Configure(EntityTypeBuilder<GeneticTrait> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(g => g.Code)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(g => g.Code).IsUnique();
        }
    }
}
