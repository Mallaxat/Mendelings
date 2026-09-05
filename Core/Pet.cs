using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class Pet : ObservableObject
    {
        //Связь пользователем
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public bool IsDead { get; set; }

        //Связи для бд
        public int Id { get; set; }
        public int? MotherId { get; set; }
        public Pet? Mother { get; set; }
        public int? FatherId { get; set; }
        public Pet? Father { get; set; }

        [ObservableProperty]
        private string name = string.Empty;
        public PetSex Sex { get; set; }
        
        //поколение
        public int Generation { get; set; }

        [ObservableProperty]
        private int health;

        [ObservableProperty]
        private int hunger;

        [ObservableProperty]
        private int mood;

        [ObservableProperty]
        private int energy;

        //состояния
        [ObservableProperty]
        private bool isSleeping = false;
        public DateTime? SleepStarted { get; set; }
        public int HungerMinutes { get; set; }
        public int MoodMinutes { get; set; }
        public int EnergyMinutes { get; set; }
        public int HealthMinutes { get; set; }

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
