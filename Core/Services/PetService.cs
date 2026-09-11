using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core.Services
{
    public class PetService
    {
        //время раз в которое значение меняться будет
        const int TIME_STATE_HUNGER= 5;
        const int TIME_STATE_MOOD = 1;
        const int TIME_STATE_ENERGY = 5;
        const int TIME_STATE_SLEEP = 1;
        const int TIME_STATE_HEALTH = 5;

        const int STATE_APPEND_ENERGY = 5;
        const int STATE_APPEND = 25;
        const int FOOD_HEALTH = 5;

        public void UpdateState(Pet pet)
        {
            //if (pet.IsDead) return;

            DateTime now = DateTime.Now;

            int minutesPassed = (int)(now - pet.LastUpdate).TotalMinutes;
            pet.HungerMinutes += minutesPassed;

            // Логика, что сытость уменьшается
            int hungerLoss = (pet.HungerMinutes / TIME_STATE_HUNGER)*10;
            pet.Hunger = Math.Max(0, pet.Hunger - hungerLoss);
            pet.HungerMinutes %= TIME_STATE_HUNGER;


            pet.MoodMinutes += minutesPassed;
            pet.EnergyMinutes += minutesPassed;

            int moodLoss = pet.MoodMinutes / TIME_STATE_MOOD;
            int energyLoss = pet.EnergyMinutes / TIME_STATE_ENERGY;

            pet.Mood = Math.Max(0, pet.Mood - moodLoss);
            pet.Energy = Math.Max(0, pet.Energy - energyLoss);

            pet.MoodMinutes %= TIME_STATE_MOOD;
            pet.EnergyMinutes %= TIME_STATE_ENERGY;



            // Потеря здоровья если голодный или не спал
            if (pet.Hunger == 0 || (pet.Energy == 0 && !pet.IsSleeping))
            {
                pet.HealthMinutes += minutesPassed;
                int healthLoss = pet.HealthMinutes / TIME_STATE_HEALTH;
                pet.Health = Math.Max(0, pet.Health - healthLoss);
                pet.HealthMinutes%= TIME_STATE_HEALTH;
            }
            else pet.HealthMinutes = 0;
            
            // Если здоровье закончилось — питомец умирает
            if (pet.Health == 0) pet.IsDead = true;

            pet.LastUpdate = now;
        }
        public void Feed(Pet pet)
        {
            pet.Hunger = Math.Min(pet.Hunger + STATE_APPEND,100);
            pet.Health = Math.Min(pet.Health + FOOD_HEALTH, 100);
        }
        public void Play(Pet pet)
        {
            if(pet.Energy>= STATE_APPEND_ENERGY && pet.Mood<100)
            {
                pet.Energy = Math.Max(0, pet.Energy - STATE_APPEND_ENERGY);
                pet.Mood = Math.Min(pet.Mood + STATE_APPEND, 100);
            }

        }
        public void StartSleep(Pet pet)
        {
            pet.IsSleeping = true;
            pet.SleepStarted=DateTime.Now;
        }

        public void WakeUp(Pet pet)
        {
            pet.IsSleeping=false;
            if (pet.SleepStarted == null) return;

            int time = (int)(DateTime.Now - pet.SleepStarted.Value).TotalMinutes;
            int addEnergy = STATE_APPEND * (time / TIME_STATE_SLEEP);
            pet.Energy = Math.Min(pet.Energy+ addEnergy, 100);
            pet.SleepStarted = null;
        }
        public void Heal(Pet pet)
        {
            pet.Health = Math.Min(pet.Health + STATE_APPEND, 100);
        }
        public int GetAge(Pet pet)
        {
            int result = (DateTime.Now - pet.BirthDate).Days;
            return result;

        }
        public bool IsAlive(Pet pet)
        {
            return !pet.IsDead;
        }
    }
}
