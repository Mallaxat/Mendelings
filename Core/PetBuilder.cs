using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    //Реализация паттерна строителя, чтобы собирать питомцев по кускам
    public class PetBuilder
    {
        private Pet readyPet = new Pet();
        public PetBuilder SetName(string name)
        {
            readyPet.Name= name;
            return this;
        }

        public PetBuilder SetSex(PetSex sex)
        {
            readyPet.Sex= sex; 
            return this;
        }

        public PetBuilder SetParents(int? motherId, int? fatherId)
        {
            readyPet.MotherId = motherId;
            readyPet.FatherId = fatherId;
            return this;
        }

        public PetBuilder SetGeneration(int generation)
        {
            readyPet.Generation = generation;
            return this;
        }

        public PetBuilder SetState(int health, int hunger, int mood, int energy)
        {
            readyPet.Health = health;
            readyPet.Hunger = hunger;
            readyPet.Mood = mood;
            readyPet.Energy = energy;
            return this;
        }

        public PetBuilder SetDates(DateTime birthDate, DateTime lastUpdate)
        {
            readyPet.BirthDate = birthDate;
            readyPet.LastUpdate = lastUpdate;
            return this;
        }

        public PetBuilder SetTailGene(string gene)
        {
            readyPet.TailGene = gene;
            return this;
        }

        public PetBuilder SetEarsGene(string gene)
        {
            readyPet.EarsGene = gene;
            return this;
        }

        public PetBuilder SetEyesGene(string gene)
        {
            readyPet.EyesGene = gene;
            return this;
        }

        public PetBuilder SetBodyGene(string gene)
        {
            readyPet.BodyGene = gene;
            return this;
        }

        public PetBuilder SetHeadGene(string gene)
        {
            readyPet.HeadGene = gene;
            return this;
        }

        public PetBuilder SetHornsGene(string gene)
        {
            readyPet.HornsGene = gene;
            return this;
        }

        public Pet Build()
        {
            Pet result = readyPet;
            readyPet = new Pet();
            return result;
        }
    }
}
