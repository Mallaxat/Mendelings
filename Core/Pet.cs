using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    public enum PetSex
    {
        Male,
        Female
    }
    //Класс хранит данные о питомце
    public class Pet
    {
        public bool IsDead { get; set; }

        //Связи для бд
        public int Id { get; set; }
        public int? MotherId { get; set; }
        public Pet? Mother { get; set; }
        public int? FatherId { get; set; }
        public Pet? Father { get; set; }

        public string Name { get; set; } = string.Empty;
        public PetSex Sex { get; set; }
        
        //поколение
        public int Generation { get; set; }


        public int Health { get; set; }
        public int Hunger { get; set; }
        public int Mood { get; set; }
        public int Energy { get; set; }

        //Дата рождения и последнее обновление
        public DateTime BirthDate { get; set; }
        public DateTime LastUpdate { get; set; }


        // Генетические признаки
        public string TailGene { get; set; } = string.Empty;
        public string EarsGene { get; set; } = string.Empty;
        public string EyesGene { get; set; } = string.Empty;
        public string BodyGene { get; set; } = string.Empty; 
        public string HeadGene { get; set; } = string.Empty;
        public string HornsGene { get; set; } = string.Empty;
    }
}
