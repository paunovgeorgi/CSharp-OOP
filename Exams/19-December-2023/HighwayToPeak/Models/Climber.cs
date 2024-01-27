using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private readonly List<string> conqueredPeaks = new List<string>();
        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }

                name = value;
            }
        }

        public int Stamina { get; protected set; }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks;
        public void Climb(IPeak peak)
        {
            
            if (peak.DifficultyLevel == "Extreme")
            {
                Stamina -= 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                Stamina -= 4;
            }
            else
            {
                Stamina -= 2;
            }

            if (Stamina < 0)
            {
                Stamina = 0;
            }
            else if (!conqueredPeaks.Contains(peak.Name) && Stamina >= 0)
            {
                conqueredPeaks.Add(peak.Name);
            }


        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            if (conqueredPeaks.Any())
            {
                sb.AppendLine($"Peaks conquered: {conqueredPeaks.Count}");
            }
            else
            {
                sb.AppendLine("Peaks conquered: no peaks conquered");
            }

            return sb.ToString().TrimEnd();

        }
    }
}
