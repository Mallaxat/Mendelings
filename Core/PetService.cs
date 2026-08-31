using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    public class PetService
    {
        const int  TIME_STATE_HUNGER= 10;
        const int TIME_STATE_MOOD = 15;
        const int TIME_STATE_ENERGY = 15;
        public void UpdateState(Pet pet)
        {
            DateTime now = DateTime.Now;

            double minutesPassed = (now - pet.LastUpdate).TotalMinutes;

            int hungerLoss = (int)(minutesPassed / TIME_STATE_HUNGER);
            int moodLoss = (int)(minutesPassed / TIME_STATE_MOOD);
            int energyLoss = (int)(minutesPassed / TIME_STATE_ENERGY);

            pet.Hunger = Math.Max(0, pet.Hunger - hungerLoss);
            pet.Mood = Math.Max(0, pet.Mood - moodLoss);
            pet.Energy = Math.Max(0, pet.Energy - energyLoss);

            //Если энергия и еда упали в ноль будет здоровье падать
            if (pet.Hunger == 0 || pet.Energy == 0)
            {
                int healthLoss = (int)(minutesPassed / 30);

                pet.Health = Math.Max(0, pet.Health - healthLoss);
            }

            pet.LastUpdate = now;
        }
        public void Feed(Pet pet)
        {
        }
        public void Play(Pet pet)
        {
        }
        public void Sleep(Pet pet)
        {
        }
        public void Heal(Pet pet)
        {
        }
        public TimeSpan GetAge(Pet pet)
        {
            throw new NotImplementedException();
        }
        public bool IsAlive(Pet pet)
        {
            return !pet.IsDead;
        }
    }
}
