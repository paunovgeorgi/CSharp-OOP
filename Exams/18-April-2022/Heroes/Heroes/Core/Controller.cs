using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Heroes.Core.Contracts;
using Heroes.Models;
using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using Heroes.Utilities.Messages;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private IRepository<IHero> heroes = new HeroRepository();
        private IRepository<IWeapon> weapons = new WeaponRepository();

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.Models.Any(w=>w.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            if (type != "Claymore" && type != "Mace")
            {
                throw new InvalidOperationException(OutputMessages.WeaponTypeIsInvalid);
            }

            IWeapon weapon = null;
            if (type == "Claymore")
            {
                weapon = new Claymore(name, durability);
            }
            else
            {
                weapon = new Mace(name, durability);
            }
            weapons.Add(weapon);

            return String.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.Models.Any(h=>h.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroAlreadyExist, name));
            }

            if (type != "Knight" && type != "Barbarian")
            {
                throw new InvalidOperationException(OutputMessages.HeroTypeIsInvalid);
            }

            IHero hero = null;
            if (type == "Knight")
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
                return String.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }

            hero = new Barbarian(name, health, armour);
            heroes.Add(hero);
            return String.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (!heroes.Models.Any(h => h.Name == heroName))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroDoesNotExist, heroName));
            }
            if (!weapons.Models.Any(w => w.Name == weaponName))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            IHero hero = heroes.FindByName(heroName);

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }

            IWeapon weapon = weapons.FindByName(weaponName);
            hero.AddWeapon(weapon);

            return String.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());


        }

        public string StartBattle()
        {
            IMap map = new Map();
            return map.Fight(heroes.Models.ToList());
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IHero hero in heroes.Models.OrderBy(h=>h.GetType().Name).ThenByDescending(h=>h.Health).ThenBy(h=>h.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                if (hero.Weapon == null)
                {
                    sb.AppendLine($"--Weapon: Unarmed");
                }
                else
                {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
