using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private readonly List<IMilitaryUnit> army = new List<IMilitaryUnit>();
        private readonly List<IWeapon> weapons = new List<IWeapon>();
        private string name;
        private double budget;
        //private double militaryPower;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }

                budget = value;
            }
        }

        public double MilitaryPower => MPower();
       
        public IReadOnlyCollection<IMilitaryUnit> Army => army.AsReadOnly();
        public IReadOnlyCollection<IWeapon> Weapons => weapons.AsReadOnly();
        public void AddUnit(IMilitaryUnit unit)
        {
            army.Add(unit);
        }

        private double MPower()
        {
            double value = value = army.Sum(u => u.EnduranceLevel) + weapons.Sum(w => w.DestructionLevel);
            if (army.Any(a => a.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                value *= 1.30;
            }

            if (weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                value *= 1.45;
            }

            return Math.Round(value, 3);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.Add(weapon);
        }

        public void TrainArmy()
        {
            foreach (IMilitaryUnit militaryUnit in army)
            {
                militaryUnit.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            if (!army.Any())
            {
                sb.AppendLine("--Forces: No units");
            }
            else
            {
                sb.AppendLine($"--Forces: {string.Join(", ", army.Select(a => a.GetType().Name))}");
            }

            if (!weapons.Any())
            {
                sb.AppendLine("--Combat equipment: No weapons");
            }
            else
            {
                sb.AppendLine($"--Combat equipment: {string.Join(", ", weapons.Select(a => a.GetType().Name))}");
            }

            sb.AppendLine($"--Military Power: {MilitaryPower}");
            return sb.ToString().TrimEnd();

        }
    }
}
