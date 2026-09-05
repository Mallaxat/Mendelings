using Mendelings.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Data.Models
{
    //Описывает для БД питомца
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        //IEntityTypeConfiguration<Pet> - настройки Pet
       //Тут мы по сути настраиваем свойства наших объектов
        public void Configure(EntityTypeBuilder<Pet> builder)
        {

            builder.HasKey(p => p.Id); //ID первичный ключ

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Sex).HasConversion<string>().IsRequired();

            builder.Property(p => p.TailGene).HasMaxLength(2);

            builder.Property(p => p.EarsGene).HasMaxLength(2);

            builder.Property(p => p.EyesGene).HasMaxLength(2);

            builder.Property(p => p.BodyGene).HasMaxLength(2);

            builder.Property(p => p.HeadGene).HasMaxLength(2);

            builder.Property(p => p.HornsGene).HasMaxLength(2);

            //Настройка связи с родителями
            //у одного пета один родитель у одного родителя много детей
            builder.HasOne(p => p.Mother).WithMany()
                .HasForeignKey(p => p.MotherId) //связь храниться будет через айди
                .OnDelete(DeleteBehavior.Restrict); // нельзя удалить пока есть дети

            builder.HasOne(p => p.Father)
                .WithMany()
                .HasForeignKey(p => p.FatherId)
                .OnDelete(DeleteBehavior.Restrict);
            
           //связь с пользователем
            builder.HasOne(p => p.User)
                .WithMany(u => u.Pets)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
