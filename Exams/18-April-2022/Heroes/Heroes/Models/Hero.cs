using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;

namespace Heroes.Models
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;
        protected Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);
                }

                name = value;
            }
        }
        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);
                }

                health = value;
            }
        }

        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                }

                armour = value;
            }
        }

        public IWeapon Weapon => weapon;

        public bool IsAlive => Health > 0;
        
        public void TakeDamage(int points)
        {
            if (Armour >= points)
            {
                Armour -= points;
            }
            else 
            {
               int remainder = points - Armour;
               Armour = 0;
               if (Health - remainder <= 0)
               {
                   Health = 0;
               }
               else
               {
               Health -= remainder;
               }
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapon = weapon;
        }
    }
}
